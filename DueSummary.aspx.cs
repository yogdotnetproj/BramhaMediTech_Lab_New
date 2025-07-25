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

public partial class DueSummary : System.Web.UI.Page
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
                        checkexistpageright("DueSummary.aspx");
                    }
                }
                Filldropdown();
               
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                BindGrid();
                if (Session["usertype"] != null)
                {
                    if (Session["usertype"].ToString() == "CollectionCenter")
                    {
                        createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        DdlCenter.SelectedValue = ui.CenterCode;
                        DdlCenter.Enabled = false;

                    }
                    else
                    {
                        DdlCenter.Enabled = true;

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
        }
    }
   
    private void Filldropdown()
    {
        try
        {
            DdlCenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            DdlCenter.DataTextField = "Name";
            DdlCenter.DataValueField = "DoctorCode";
            DdlCenter.DataBind();
            DdlCenter.Items.Insert(0, "Select Center Name");
            DdlCenter.SelectedIndex = -1;

           
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
            if (Session["usertype"].ToString() == "Admin" || Session["usertype"].ToString().ToLower() == "administrator")
            {
                GV_Duesummary.DataSource = Cshmst_supp_Bal_C.getLoginfoDataForsummaryRemBalance(fromDate, Todate, username, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]));
                GV_Duesummary.DataBind();
            }
            else
            {
                GV_Duesummary.DataSource = Cshmst_supp_Bal_C.getLoginfoDataForsummaryRemBalance_Center(fromDate, Todate, username, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), Convert.ToString(Session["username"]));
                GV_Duesummary.DataBind();
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

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        string query = " ALTER VIEW [dbo].[VW_dssumm] AS (SELECT DISTINCT RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS PatientName, dbo.patmst.PatRegID,  dbo.patmst.Phrecdate,   "+
                  "  case when patmst.isactive=0 then -patmst.TestCharges else patmst.TestCharges+sum(RecM.RefundAmt) end as TestCharges,  "+
                  "  case when patmst.isactive=0 then -sum(RecM.Othercharges) else sum(RecM.Othercharges) end as Othercharges,   "+
                  "  case when patmst.isactive=0 then -sum(RecM.AmtPaid) else sum(RecM.AmtPaid) end as AmtPaid,    "+
                  "  case when patmst.isactive=0 then -CONVERT(FLOAT, sum(RecM.DisAmt)) else CONVERT(FLOAT,sum( RecM.DisAmt))end AS Discount,      "+
                  "  convert(varchar, RecM.BillDate, 103)  as RecDate, dbo.patmst.CenterCode, dbo.patmst.Centername, dbo.patmst.Drname, dbo.patmst.PID, dbo.patmst.branchid, "+
                  "  dbo.patmst.TestName,   dbo.RecM.BillNo , dbo.patmst.UnitCode, RecM.Billcancelno, RecM.TaxAmount  "+
                  "  FROM         dbo.patmst LEFT OUTER JOIN dbo.RecM ON dbo.patmst.branchid = dbo.RecM.branchid AND   dbo.patmst.PID = dbo.RecM.PID  where patmst.isactive=1  and patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";
        if (fromdate.Text != "" && todate.Text != "")
        {
            query += " and (CAST(CAST(YEAR( patmst.Phrecdate) AS varchar(4)) + '/' + CAST(MONTH( patmst.Phrecdate) AS varchar(2)) + '/' + CAST(DAY( patmst.Phrecdate) AS varchar(2)) AS datetime)) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }
        if (DdlCenter.SelectedIndex > 0)
        {
            query += " and patmst.CenterCode='" + DdlCenter.SelectedValue.Trim() + "' ";
        }
       
         query += "  group by patmst.intial, patmst.Patname,patmst.PatRegID, patmst.Phrecdate,patmst.isactive,patmst.TestCharges,    dbo.patmst.Centername, dbo.patmst.Drname, "+
                     "   dbo.patmst.PID, dbo.patmst.branchid,    dbo.patmst.testname, dbo.RecM.BillNo, dbo.patmst.UnitCode,RecM.TaxAmount,RecM.Billcancelno ,  "+
                     "   convert(varchar, RecM.BillDate, 103),dbo.patmst.CenterCode ";

        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        cmd1.CommandText = query + "  )";
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
        ReportParameterClass.ReportType = "CenterWiseIncomeSummary_coll";//

        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_displayreport.rpt");
        Session["reportname"] = "CenterWiseIncomeSummary";
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


    protected void GV_Duesummary_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GV_Duesummary_RowEditing(object sender, GridViewEditEventArgs e)
    {


    }
    protected void GV_Duesummary_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void GV_Duesummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Duesummary.PageIndex = e.NewPageIndex;
        BindGrid();
        GV_Duesummary.DataBind();
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