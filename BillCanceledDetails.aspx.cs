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

public partial class BillCanceledDetails : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string Cname = "", Patregid = "", collectioncode = "", labcode_main = "";
    DateTime stDate = Date.getMinDate(), endDate = Date.getMinDate();
    PatientBillCancel_C ObjPBC = new PatientBillCancel_C();
    Patmst_New_Bal_C contact = new Patmst_New_Bal_C();
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
                        checkexistpageright("BillCanceledDetails.aspx");
                    }
                }

                
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
            try
            {
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

                collectioncode = Session["CenterCode"].ToString();

            }
            if (txtCenter.Text != "")
            {
                Cname = txtCenter.Text;
            }
            if (txtregno.Text != "")
            {
                Patregid = txtregno.Text;

            }
            else if (fromdate.Text != "" && todate.Text != "")
            {
                stDate = DateTimeConvesion.getDateFromString(fromdate.Text);
                endDate = DateTimeConvesion.getDateFromString(todate.Text);


            }
            GV_BillCancelDet.DataSource = contact.getpatientbillcanceled(Cname, stDate, endDate, Patregid, Convert.ToInt32(Session["Branchid"]), 0, collectioncode, "", this.labcode_main);
            GV_BillCancelDet.DataBind();
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
    protected void GV_BillCancelDet_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (e.NewEditIndex == -1)
            return;
        int PID = Convert.ToInt32(GV_BillCancelDet.DataKeys[e.NewEditIndex].Value);

        ObjPBC.P_PID = PID;


    }
    protected void GV_BillCancelDet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_BillCancelDet.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] Getcenter(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + prefixText + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + prefixText + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["DoctorName"], i);
            i++;
        }
        return tests;
    }
    protected void GV_BillCancelDet_RowDataBound(object sender, GridViewRowEventArgs e)
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

                int balance = Convert.ToInt32(e.Row.Cells[11].Text);                
                string CenterCode = (e.Row.FindControl("hdnCollcode") as HiddenField).Value;
               
            }
            catch (Exception ex)
            { }
        }
    }
    protected void GV_BillCancelDet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex == -1)
            return;
        int PID = Convert.ToInt32(GV_BillCancelDet.DataKeys[e.RowIndex].Value);

       

        #region MyRegion
        string sql = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();

        sc.CommandText = "ALTER VIEW [dbo].[VW_billCancelInvoice] AS (SELECT    VW_csmst1vw.PID, VW_csmst1vw.PatRegID, VW_csmst1vw.FID, SaleCancelDetails.tdate as Patregdate, VW_csmst1vw.intial, "+
              "  VW_csmst1vw.sex, VW_csmst1vw.Age, VW_csmst1vw.FName, VW_csmst1vw.MDY, VW_csmst1vw.RefDr, VW_csmst1vw.Tests, VW_csmst1vw.Phrecdate, VW_csmst1vw.Branchid, "+
              "  VW_csmst1vw.DoctorCode, VW_csmst1vw.CenterCode, VW_csmst1vw.FinancialYearID, VW_csmst1vw.EmailID, VW_csmst1vw.Drname, "+
              "  VW_csmst1vw.CenterName, VW_csmst1vw.Username, VW_csmst1vw.Usertype, VW_csmst1vw.PPID, VW_csmst1vw.testname, VW_csmst1vw.AmtPaid,  "+
              "  VW_csmst1vw.Discount, VW_csmst1vw.UnitCode, VW_csmst1vw.TestCharges, VW_csmst1vw.balance, VW_csmst1vw.IsbillBH, VW_csmst1vw.BillNo,  "+
              "  VW_csmst1vw.Patphoneno, VW_csmst1vw.IsActive,  VW_csmst1vw.TestCharges-VW_csmst1vw.Discount as Taxper, VW_csmst1vw.TaxAmount, VW_csmst1vw.Monthlybill,  "+
              "  VW_csmst1vw.intial + ' ' + VW_csmst1vw.FirstName AS Fullname, SaleCancelDetails.CancelReceiptNo "+
              "  FROM         VW_csmst1vw INNER JOIN  "+
              "  SaleCancelDetails ON VW_csmst1vw.PID = SaleCancelDetails.PID where (VW_csmst1vw.IsbillBH = 0) AND (VW_csmst1vw.Monthlybill = 0) AND (VW_csmst1vw.IsActive = 0) AND (VW_csmst1vw.PatRegID <> '') AND   VW_csmst1vw.branchid=" + Session["Branchid"].ToString() + " and VW_csmst1vw.PID=" + PID + ")";// 


        conn.Open();
        sc.ExecuteNonQuery();
        conn.Close(); conn.Dispose();
        #endregion


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_CancelReceipt.rpt");
        Session["reportname"] = "CancelReceipt";
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