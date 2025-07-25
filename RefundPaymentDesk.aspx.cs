using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;

public partial class RefundPaymentDesk : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string CName = "", regno = "", CenCode = "", labcode_main = "", Patname = "", Mobno = "";
    DateTime stDate = Date.getMinDate(), endDate = Date.getMinDate();
    Patmst_New_Bal_C ObjPNB_C = new Patmst_New_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("RefundPaymentDesk.aspx");
                    }
                }

                fromdate.Text = DateTime.Now.ToShortDateString();
                todate.Text = DateTime.Now.ToShortDateString();
                txtCenter.Text = "All";
                BindGrid();
            }
            catch (Exception exc)
            {
                if (exc.Message.Equals("Exception aborted."))
                {
                    return;
                }
                else
                {
                    Response.Cookies["error"].Value = exc.Message;
                    Server.Transfer("~/ErrorMessage.aspx");
                }
            }
        }
    }
   
    void BindGrid()
    {
        try
        {
            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
            if (labcode != null && labcode != "")
            {
                this.labcode_main = labcode;
            }
            if (Session["usertype"].ToString() == "CollectionCenter")
            {

                CenCode = Session["CenterCode"].ToString();

            }
            if (txtCenter.Text != "")
            {
                CName = txtCenter.Text;
            }
            if (txtregno.Text != "")
            {
                regno = txtregno.Text;

            }
            if (txtname.Text != "")
            {
                Patname = txtname.Text;
            }
            if (txtmobileno.Text != "")
            {
                Mobno = txtmobileno.Text;
            }
            if (fromdate.Text != "" && todate.Text != "")
            {
                stDate = DateTimeConvesion.getDateFromString(fromdate.Text);
                endDate = DateTimeConvesion.getDateFromString(todate.Text);


            }
            GV_Billdesk.DataSource = ObjPNB_C.GetPatientInformationnew_Refundpay(CName, stDate, endDate, regno, Convert.ToInt32(Session["Branchid"]), 0, CenCode, "", this.labcode_main, Patname, Mobno, CName);
            GV_Billdesk.DataBind();
        }
        catch (Exception exc)
        {
            if (exc.Message.Equals("Exception aborted."))
            {
                return;
            }
            else
            {
                Response.Cookies["error"].Value = exc.Message;
                Server.Transfer("~/ErrorMessage.aspx");
            }
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void GV_Billdesk_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (e.NewEditIndex == -1)
            return;
        int PID = Convert.ToInt32(GV_Billdesk.DataKeys[e.NewEditIndex].Value);
        string FID = (GV_Billdesk.Rows[e.NewEditIndex].FindControl("Hdnfid") as HiddenField).Value;      
        Response.Redirect("Paybilldesk.aspx?PID=" + PID + "&FID=" + FID+"&Refund=Yes", false);

    }
    protected void GV_Billdesk_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Billdesk.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void GV_Billdesk_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            try
            {
                string charges = (e.Row.FindControl("lblTestCharges") as Label).Text;
                string Discount = e.Row.Cells[10].Text;
                int total = 0;
                if (charges != "" && charges != null && charges != "&nbsp;")
                {
                    total = Convert.ToInt32(charges);
                }

                float Paidamt = Convert.ToSingle(e.Row.Cells[12].Text);
                float balance = Convert.ToSingle(e.Row.Cells[14].Text);
                
                string CenterCode = (e.Row.FindControl("hdnCencode") as HiddenField).Value;

                e.Row.Cells[16].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[17].ForeColor = System.Drawing.Color.Black;

                bool IS_REf = Convert.ToBoolean( (e.Row.FindControl("hdn_IsRefund") as HiddenField).Value);
                   if (IS_REf == true)
                    {
                        //e.Row.Cells[10].ForeColor = System.Drawing.Color.White;
                        // e.Row.Cells[10].BackColor = System.Drawing.Color.Green;
                        e.Row.Cells[15].Text = "<span class='btn btn-xs btn-success' >Ref Success</span>";
                        e.Row.Cells[16].Enabled = false;
                        e.Row.Cells[17].Visible = false;
                    }
                    else
                    {
                        // e.Row.Cells[10].ForeColor = System.Drawing.Color.Red;
                        // e.Row.Cells[10].BackColor = System.Drawing.Color.Red;
                        e.Row.Cells[15].Text = "<span class='btn btn-xs btn-danger' >Ref Pen</span>";
                        e.Row.Cells[17].Visible = false;

                    }
            }
            catch (Exception ex)
            { }
        }
    }
    protected void GV_Billdesk_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex == -1)
            return;
        int PID = Convert.ToInt32(GV_Billdesk.DataKeys[e.RowIndex].Value);

        #region MyRegion
        string sql = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();

        sc.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT dbo.Cshmst.BillNo, dbo.Cshmst.RecDate, dbo.Cshmst.BillType, dbo.Cshmst.AmtReceived,  dbo.Cshmst.Discount,  "+
                 "   dbo.Cshmst.NetPayment,   dbo.Cshmst.AmtPaid,   dbo.Cshmst.Balance,  dbo.Cshmst.username,   dbo.Cshmst.OtherCharges,  "+
                 "   dbo.patmst.PatRegID, dbo.patmst.intial, dbo.patmst.Patname,   dbo.patmst.sex, dbo.patmst.Age, dbo.patmst.Drname,   "+
                 "   dbo.patmst.TelNo, dbo.DrMT.DoctorCode, dbo.DrMT.DoctorName, dbo.MainTest.Maintestname,   dbo.MainTest.MTCode,  "+
                 "   dbo.patmstd.TestRate,   dbo.PackMst.PackageName, dbo.patmstd.PackageCode, dbo.Cshmst.DisFlag,   "+
                 "   dbo.patmst.Patusername,    dbo.patmst.Patpassword,    dbo.Cshmst.Comment,dbo.patmst.MDY, "+
                 "   dbo.patmst.Remark AS PatientRemark,dbo.patmst.Pataddress,   dbo.patmst.PPID ,dbo.patmst.UnitCode ,   "+ 
                 "   Cshmst.TaxAmount, Cshmst.TaxPer, 0 as PrintCount , patmst.OtherRefDoctor   "+
                 "   FROM         patmst INNER JOIN   DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid   "+
                 "   INNER JOIN   Cshmst INNER JOIN   MainTest INNER JOIN   patmstd ON MainTest.MTCode = patmstd.MTCode AND  "+
                 "   MainTest.Branchid = patmstd.Branchid ON Cshmst.PID = patmstd.PID AND   Cshmst.Branchid = patmstd.Branchid ON  "+
                 "   patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN   RecM ON Cshmst.BillNo = RecM.BillNo "+ 
                 "   AND   Cshmst.PID = RecM.PID AND Cshmst.Branchid = RecM.branchid LEFT OUTER JOIN     "+
                 "   PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode  where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and Cshmst.PID=" + PID + ")";// 


        conn.Open();
        sc.ExecuteNonQuery();
        conn.Close(); conn.Dispose();
        #endregion


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PayReceipt.rpt");
        Session["reportname"] = "CashReceipt";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);







       
    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

            StreamReader sr = new StreamReader(httpres.GetResponseStream());

            string results = sr.ReadToEnd();

            sr.Close();
            return results;
        }
        catch
        {
            return "0";
        }
    }

    public void checkexistpageright(string PageName)
    {

        string MenuSQL = "";
        DataTable MenuDt = new DataTable();
        MenuSQL = String.Format(@"SELECT        Roleright.Rightid, Roleright.Usertypeid, Roleright.FormId, Roleright.FormName, Roleright.Branchid, usr.ROLENAME, " +
              "  TBL_SubMenuMaster.SubMenuNavigateURL, TBL_MenuMaster.MenuName, TBL_MenuMaster.MenuID,   TBL_SubMenuMaster.SubMenuName, TBL_MenuMaster.Icon, " +
              "  TBL_SubMenuMaster.SubMenuID   " +
              "  FROM            Roleright INNER JOIN   usr ON Roleright.Usertypeid = usr.ROLLID AND Roleright.Branchid = usr.branchid INNER JOIN   " +
              "  TBL_SubMenuMaster ON Roleright.FormId = TBL_SubMenuMaster.SubMenuID INNER JOIN   TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID INNER JOIN  " +
              "  CTuser ON Roleright.Usertypeid = CTuser.Rollid  where (CTuser.USERNAME = '" + Convert.ToString(Session["username"]) + "') AND (CTuser.password = '" + Convert.ToString(Session["password"]) + "') and  TBL_SubMenuMaster.Isvisable=1  and TBL_SubMenuMaster.SubMenuNavigateURL='" + PageName + "'  " +
                               " order by MenuID  ");



        string connectionString1 = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString1);

        SqlCommand cmd = new SqlCommand(MenuSQL, con);

        SqlDataAdapter Adp = new SqlDataAdapter(cmd);

        Adp.Fill(MenuDt);
        if (MenuDt.Rows.Count == 0)
        {
            Response.Redirect("Login.aspx", false);
        }
        con.Close();
        con.Dispose();

    }

}