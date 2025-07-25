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

public partial class DPRReport : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string labcode_main = "";
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
                        checkexistpageright("DPRReport.aspx");
                    }
                }
                Binddropdown();
                FillddlCenter();

                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
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

  

    private void Binddropdown()
    {
        try
        {

            if (Session["usertype"].ToString() == "Admin" || Session["usertype"].ToString().ToLower() == "administrator" || Session["usertype"].ToString().ToLower() == "accountant")
            {
                ddlusername.DataSource = createuserlogic_Bal_C.getAllUsers(Convert.ToInt32(Session["Branchid"]));
                ddlusername.DataTextField = "username";
                ddlusername.DataValueField = "username";
                ddlusername.DataBind();
                ddlusername.Items.Insert(0, "Select UserName");
                ddlusername.SelectedIndex = -1;
            }
            else
            {
                ddlusername.DataSource = createuserlogic_Bal_C.getAllUsers_username(Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"]));
                ddlusername.DataTextField = "username";
                ddlusername.DataValueField = "username";
                ddlusername.DataBind();
                ddlusername.Items.Insert(0, "Select UserName");
                ddlusername.SelectedIndex = 1;
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
    private void FillddlCenter()
    {
        try
        {
            ddlCenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Session["UnitCode"], Convert.ToInt32(Session["Branchid"]));
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
            string Center = "";
            object fromDate = null;
            object Todate = null;

            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);

            if (labcode != null && labcode != "")
            {
                this.labcode_main = labcode;
            }

            if (fromdate.Text != "")
            {
                fromDate = DateTimeConvesion.getDateFromString(fromdate.Text.Trim()).ToString();
            }
            if (todate.Text != "")
            {
                Todate = DateTimeConvesion.getDateFromString(todate.Text.Trim()).ToString();
            }

            if (Session["usertype"].ToString() == "CollectionCenter")
            {
                Center = Session["CenterCode"].ToString();
            }

            string username = "";
           
            if (ddlCenter.SelectedItem.Text != "Select")
            {
                username = ddlCenter.SelectedItem.Text;
            }
            GV_DprReport.DataSource = Cshmst_supp_Bal_C.Get_DPRRepotData(Todate, fromDate, username, Center, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), this.labcode_main);
            GV_DprReport.DataBind();

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
    protected void GV_DprReport_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GV_DprReport_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string username = GV_DprReport.Rows[e.NewEditIndex].Cells[9].Text;
            ViewState["username"] = username;
            ViewState["tdate"] = GV_DprReport.Rows[e.NewEditIndex].Cells[0].Text;

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

    protected void GV_DprReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
        if (e.Row.Cells[2].Text != "")
        {
            string date = e.Row.Cells[0].Text;
            date = Convert.ToDateTime(date).ToShortDateString();
            e.Row.Cells[0].Text = date;
        }
    }

    protected void GV_DprReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DprReport.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void btndailytrans_Click(object sender, EventArgs e)
    {
        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"]);

        #region Myregion2
        SqlConnection conn2 = DataAccess.ConInitForDC();
        SqlCommand sc2 = conn2.CreateCommand();
        //sc2.CommandText = "ALTER VIEW [dbo].[VW_dpr] AS SELECT DISTINCT  top 99.99 percent " +
        //          "  dbo.patmst.PatRegID, " +
        //          "  RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS PatientName,  " +
        //          "  dbo.patmst.testname, " +
        //          "  dbo.patmst.Phrecdate,  " +
        //          "  case when patmst.IsFreeze=1 then 'CancelBill' else ''end  as CenterCode, dbo.patmst.CenterName, " +
        //          "  dbo.patmst.Drname, dbo.patmst.PID,  " +
        //          "  dbo.patmst.branchid,  convert(date, dbo.RecM.transdate, 103)as transdate, dbo.RecM.BillNo,  " +
        //          "  convert(date, dbo.RecM.billdate, 103)as billdate,  " +
        //          "  dbo.patmst.TestCharges, dbo.Cshmst.Othercharges, " +
        //          "  dbo.Cshmst.BilltoHospital, " +
        //          "  dbo.FUN_GetTodayBillAmt(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) AS BillAmt, " +
        //          "  dbo.FUN_GetTodayCashRecAmount(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) AS TodayCashAmtpaid,  " +
        //          "  dbo.FUN_GetTodayCardRecAmount(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) AS TodayCardAmtpaid, " +
        //         "  dbo.FUN_GetTodayOnlineRecAmount(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) AS TodayOnlineAmtpaid,  " +
        //          "  dbo.FUN_GetTodayDisAmt(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) AS DisAmt, " +
        //         "  dbo.FUN_GetTodayBalanceAmount(patmst.Branchid, RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) AS Balance " +
        //    // " , case when patmst.IsbillBH=0 then (Cshmst.NetPayment) - (Cshmst.AmtPaid + Cshmst.Discount)  else 0 end as Mainbalance, " +
        //          " , case when patmst.IsbillBH=0 then (Cshmst.NetPayment) - (Cshmst.AmtPaid + dbo.FUN_GetTodayDisAmt(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)))  else 0 end as Mainbalance, " +
        //          "  case when patmst.IsbillBH=1 then (Cshmst.NetPayment) - (Cshmst.AmtPaid + Cshmst.Discount)  else 0 end as BTHbalance , patmst.IsbillBH , patmst.username " +
        //    //  "  dbo.Cshmst.Balance " +
        //          "  FROM         dbo.Cshmst INNER JOIN " +
        //          "  dbo.patmst ON dbo.Cshmst.branchid = dbo.patmst.branchid AND  " +
        //          "  dbo.Cshmst.CenterCode = dbo.patmst.CenterCode AND dbo.Cshmst.PID = dbo.patmst.PID INNER JOIN " +
        //          "  dbo.RecM ON dbo.patmst.branchid = dbo.RecM.branchid AND dbo.patmst.PID = dbo.RecM.PID AND " +
        //          "  CONVERT(date, dbo.RecM.billdate, 103) = CONVERT(date, dbo.RecM.transdate, 103) and Cshmst.BillNo = RecM.BillNo " +
        //              "Where 1=1";
        sc2.CommandText = "ALTER VIEW [dbo].[VW_dpr] AS SELECT DISTINCT "+
                         "   TOP (99.99) PERCENT patmst.PatRegID, RTRIM(patmst.intial) + ' ' + patmst.Patname AS PatientName, patmst.testname, patmst.Phrecdate, CASE WHEN patmst.isactive = 0 THEN 'CancelBill' ELSE '' END AS CenterCode, "+
                         "   patmst.CenterName, patmst.Drname, patmst.PID, patmst.Branchid, CONVERT(date, RecM.transdate, 103) AS transdate, RecM.BillNo, CONVERT(date, RecM.billdate, 103) AS billdate, "+
                        // "   CASE WHEN patmst.isactive = 0 THEN - dbo.patmst.TestCharges ELSE dbo.patmst.TestCharges END AS TestCharges,
                         " case when patmst.isactive=0 then -patmst.TestCharges else case when convert(varchar, RecM.billdate, 103) <>convert(varchar, dbo.patmst.Phrecdate, 103)  then'0' else  patmst.TestCharges+(sum(RecM.RefundAmt)+ ISNULL(sum(RecM.Othercharges), 0))end end as TestCharges, "+

                         " CASE WHEN patmst.isactive = 0 THEN - SUM(dbo.RecM.Othercharges) ELSE SUM(dbo.RecM.Othercharges)  "+
                         "   END AS Othercharges, 0 AS BilltoHospital, CASE WHEN patmst.isactive = 0 THEN - isnull(SUM(Billamt), 0) ELSE isnull(SUM(Billamt), 0) END AS BillAmt, "+
                         "   CASE WHEN patmst.isactive = 0 THEN - dbo.FUN_GetTodayCashRecAmount(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) ELSE dbo.FUN_GetTodayCashRecAmount(dbo.patmst.branchid, "+
                         "   dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) END AS TodayCashAmtpaid, CASE WHEN patmst.isactive = 0 THEN - dbo.FUN_GetTodayCardRecAmount(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date,  "+
                         "   dbo.RecM.transdate, 103)) ELSE dbo.FUN_GetTodayCardRecAmount(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) END AS TodayCardAmtpaid,  "+
                         "   CASE WHEN patmst.isactive = 0 THEN - dbo.FUN_GetTodayOnlineRecAmount(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) ELSE dbo.FUN_GetTodayOnlineRecAmount(dbo.patmst.branchid,  "+
                         "   dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) END AS TodayOnlineAmtpaid, CASE WHEN patmst.isactive = 0 THEN - dbo.FUN_GetTodayDisAmt(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date,  "+
                         "   dbo.RecM.transdate, 103)) ELSE dbo.FUN_GetTodayDisAmt(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) END AS DisAmt,  "+
                         "   CASE WHEN patmst.isactive = 0 THEN - dbo.FUN_GetTodayBalanceAmount(patmst.Branchid, RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) ELSE dbo.FUN_GetTodayBalanceAmount(patmst.Branchid, RecM.BillNo, "+
                         "   CONVERT(date, dbo.RecM.transdate, 103)) END AS Balance, CASE WHEN patmst.IsbillBH = 0 AND RecM.isRefund = 0 THEN (dbo.patmst.TestCharges) - (SUM(RecM.AmtPaid) + dbo.FUN_GetTodayDisAmt(dbo.patmst.branchid,  "+
                         "   dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103))) ELSE 0 END AS Mainbalance, CASE WHEN patmst.IsbillBH = 1 THEN (dbo.patmst.TestCharges) - (SUM(RecM.AmtPaid) + sum(RecM.DisAmt)) ELSE 0 END AS BTHbalance,  "+
                         "   patmst.IsbillBH, patmst.Username "+
                         "   FROM            RecM INNER JOIN "+
                         "   patmst ON RecM.branchid = patmst.Branchid AND RecM.PID = patmst.PID " +
                   "Where 1=1";
        if (fromdate.Text != "")
        {
            sc2.CommandText += "  and (CAST(CAST(YEAR(RecM.transdate) AS varchar(4)) + '/' + CAST(MONTH(RecM.transdate) AS varchar(2)) + '/' + CAST(DAY(RecM.transdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
        }
        //if (ddlusername.SelectedIndex > 0)
        //{
        //    sc2.CommandText += " and Cshmst.username='" + ddlusername.SelectedItem.Text.Trim() + "'";
        //}
        if (RblCashType.SelectedItem.Text == "Cash")
        {
            //username = ddlusername.SelectedItem.Text;
            sc2.CommandText += " and  dbo.FUN_GetTodayCashRecAmount(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103)) >0";
        }
        if (RblCashType.SelectedItem.Text == "Card")
        {
            //username = ddlusername.SelectedItem.Text;
            sc2.CommandText += " and dbo.FUN_GetTodayCardRecAmount(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103))>0";
        }
        if (RblCashType.SelectedItem.Text == "Online Gateway")
        {
            //username = ddlusername.SelectedItem.Text;
            sc2.CommandText += " and dbo.FUN_GetTodayOnlineRecAmount(dbo.patmst.branchid, dbo.RecM.BillNo, CONVERT(date, dbo.RecM.transdate, 103))>0";
        }
        if (ddlCenter.SelectedItem.Text != "Select")
        {
            //username = ddlusername.SelectedItem.Text;
            sc2.CommandText += " and patmst.CenterCode='" + ddlCenter.SelectedValue + "' ";
        }

        sc2.CommandText += " group by patmst.PatRegID, RTRIM(patmst.intial) , patmst.Patname , patmst.testname, patmst.Phrecdate, patmst.isactive, "+
                         "   patmst.CenterName, patmst.Drname, patmst.PID, patmst.Branchid, CONVERT(date, RecM.transdate, 103) , "+
                         "   RecM.BillNo, CONVERT(date, RecM.billdate, 103),patmst.TestCharges,patmst.IsbillBH,RecM.IsRefund,patmst.Username,convert(varchar, RecM.BillDate, 103) ";

        sc2.CommandText += " order by PID asc   ";

        conn2.Open();
        try
        {
            sc2.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn2.Close(); conn2.Dispose();
        #endregion

        #region Myregion3
        SqlConnection conn21 = DataAccess.ConInitForDC();
        SqlCommand sc21 = conn21.CreateCommand();
        sc21.CommandText = "ALTER VIEW [dbo].[VW_dprdt] AS (SELECT DISTINCT  " +
                  " dbo.patmst.PatRegID, " +
                  "     RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS PatientName, " +
                   "     dbo.patmst.testname, " +
                    "    dbo.patmst.Phrecdate, " +
                     "   dbo.patmst.Drname, " +
                      "  dbo.patmst.PID,  dbo.patmst.branchid, " +
                       " convert(date, RecM.transdate, 103) as transdate, dbo.RecM.BillNo, dbo.RecM.billdate , " +
                       " dbo.patmst.CenterCode, dbo.patmst.CenterName " +
                       " ,dbo.[FUN_GetGrandBalance](dbo.patmst.branchid, dbo.RecM.BillNo,'" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') AS FinalBal  " +
                       " FROM   patmst INNER JOIN RecM ON patmst.Branchid = RecM.branchid AND patmst.PID = RecM.PID  " +
                      "Where 1=1";
        if (fromdate.Text != "")
        {
            sc21.CommandText += " and (CAST(CAST(YEAR(RecM.transdate) AS varchar(4)) + '/' + CAST(MONTH(RecM.transdate) AS varchar(2)) + '/' + CAST(DAY(RecM.transdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
        }

        if (ddlusername.SelectedIndex > 0)
        {
            sc21.CommandText += " and RecM.username='" + ddlusername.SelectedItem.Text.Trim() + "'";
        }
        sc21.CommandText += ")";

        conn21.Open();
        try
        {
            sc21.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn21.Close(); conn21.Dispose();
        #endregion

        string sql = "";
        ReportParameterClass.ReportType = "DailyCashSum";


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_DailyAllDetails.rpt");
        Session["reportname"] = "DailyAllDetails";
        Session["RPTFORMAT"] = "pdf";
        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);


        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

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