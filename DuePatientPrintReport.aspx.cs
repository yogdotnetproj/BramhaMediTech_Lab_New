using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public partial class DuePatientPrintReport :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
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
                        checkexistpageright("DuePatientPrintReport.aspx");
                    }
                }
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                FillddlCenter();
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



    private void FillddlCenter()
    {
        try
        {
            ddlCenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            ddlCenter.DataTextField = "Name";
            ddlCenter.DataValueField = "DoctorCode";
            ddlCenter.DataBind();
            ddlCenter.Items.Insert(0, "Select");
            //ddlCenter.SelectedIndex = 1;
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

    protected void btnlist_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    void BindGrid()
    {
        try
        {          
            object fromDate = null;
            object Todate = null;

            if (fromdate.Text != "")
            {

                fromDate = DateTimeConvesion.getDateFromString(fromdate.Text.Trim()).ToString();

            }
            if (todate.Text != "")
            {
                Todate = DateTimeConvesion.getDateFromString(todate.Text.Trim()).ToString();
            }

            string username = "";

            if (ddlCenter.SelectedIndex > 0)
            {
            }
            //if (Session["usertype"].ToString() == "Admin" || Session["usertype"].ToString().ToLower() == "administrator" || Session["usertype"].ToString().ToLower() == "accountant")
            //{
            //    if (ddlCenter.SelectedIndex > 0)
            //    {
            //        GV_DueReport.DataSource = Cshmst_supp_Bal_C.getLoginfoDataForsummaryRemBalance_CenterDue(fromDate, Todate, username, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]),Convert.ToString(ddlCenter.SelectedValue));
            //    }
            //    else
            //    {
            //        GV_DueReport.DataSource = Cshmst_supp_Bal_C.getLoginfoDataForsummaryRemBalance(fromDate, Todate, username, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]));

            //    }
            //    GV_DueReport.DataBind();
            //}
            //else
            //{
                if (ddlCenter.SelectedIndex > 0)
                {
                    GV_DueReport.DataSource = Cshmst_supp_Bal_C.getLoginfoDataForsummaryRemBalance_Center_outs_report(fromDate, Todate, username, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), Convert.ToString(Session["username"]), ddlCenter.SelectedValue);
                }
                else
                {
                    GV_DueReport.DataSource = Cshmst_supp_Bal_C.getLoginfoDataForsummaryRemBalance_Center_Report(fromDate, Todate, username, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), Convert.ToString(Session["username"]));
                }
                    GV_DueReport.DataBind();
           // }
              
           

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

    protected void Button1_Click(object sender, EventArgs e)
    {


        string query = "ALTER VIEW [dbo].[VW_dsdue] AS SELECT DISTINCT top 99.99 percent RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS PatientName, dbo.patmst.PatRegID,dbo.patmst.Phrecdate, dbo.patmst.TestCharges, "+
                      "  sum(dbo.RecM.AmtPaid) as AmtPaid, sum(RecM.DisAmt) AS Discount, convert(char(12),RecM.TransDate,105) as RecDate, dbo.patmst.Centercode, dbo.patmst.CenterName, "+
                      "  dbo.patmst.DrName, dbo.patmst.PID,   dbo.patmst.branchid, dbo.patmst.testname, dbo.RecM.BillNo, dbo.patmst.Patphoneno, dbo.patmst.TelNo   ,  "+
                      "  case when patmst.IsbillBH=0 then  Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt) ) else 0 end as Mainbalance,   "+
                      "  case when patmst.IsbillBH=1 then Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt) ) else 0 end as BTHbalance , "+
                      "  RecM.username   "+
                      "  FROM         dbo.patmst LEFT OUTER JOIN dbo.RecM ON dbo.patmst.branchid = dbo.RecM.branchid AND   dbo.patmst.PID = dbo.RecM.PID   "+
                      "  group by RTRIM(dbo.patmst.intial) , dbo.patmst.Patname , dbo.patmst.PatRegID,dbo.patmst.Phrecdate, dbo.patmst.TestCharges, "+
                      "  patmst.IsActive,dbo.patmst.Branchid   , convert(char(12),RecM.TransDate,105),dbo.patmst.CenterCode,dbo.patmst.CenterName,dbo.patmst.Drname,dbo.patmst.PID, "+
                      "  dbo.patmst.testname,dbo.RecM.BillNo,dbo.patmst.Patphoneno,dbo.patmst.TelNo   ,dbo.patmst.IsbillBH,dbo.RecM.username, "+
                      "  (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime))   having   patmst.isactive=1  and dbo.patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt) )>0  and patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";
        if (todate.Text != "" && fromdate.Text != "")
        {
            query += " and (CAST(CAST(YEAR( dbo.patmst.Phrecdate) AS varchar(4)) + '/' + CAST(MONTH( dbo.patmst.Phrecdate) AS varchar(2)) + '/' + CAST(DAY( dbo.patmst.Phrecdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }

        //if (Convert.ToString(Session["usertype"]) != "Admin" && Convert.ToString(Session["usertype"]) != "Administrator" && Convert.ToString(Session["usertype"]).ToLower() != "accountant")
        //{
        //    query = query + " and dbo.Cshmst.username = '" + Convert.ToString(Session["username"]) + "' ";
        //}
        if (ddlCenter.SelectedIndex>0)
        {
            query = query + " and dbo.patmst.Centercode = '" + Convert.ToString(ddlCenter.SelectedValue) + "' ";
        }
        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"] );
       


        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        cmd1.CommandText = query + " order by patmst.PID asc ";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        ReportParameterClass.ReportType = "CenterWiseIncomeDetail_coll";//  
        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_Outstandingprintreport.rpt");
        Session["reportname"] = "Outstandingprintreport";
        Session["RPTFORMAT"] = "pdf";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);

        ReportParameterClass.SelectionFormula = "";
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
        // Server.Transfer("~/CrystalReportViewerForm.aspx");

    }
    protected void GV_DueReport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GV_DueReport_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }
    protected void GV_DueReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
        if (e.Row.Cells[0].Text != "")
        {
            string date = e.Row.Cells[0].Text;
            date = Convert.ToDateTime(date).ToShortDateString();
            e.Row.Cells[0].Text = date;
        }
    }
    protected void GV_DueReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DueReport.PageIndex = e.NewPageIndex;
        BindGrid();
        
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