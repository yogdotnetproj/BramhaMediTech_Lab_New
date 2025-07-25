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

public partial class Operator_Summary : System.Web.UI.Page
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
                        checkexistpageright("OperatorwiseSummary.aspx");
                    }
                }
                Bindddl();
               
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


  
    private void Bindddl()
    {
        try
        {
            if (Session["usertype"].ToString() == "Admin" || Session["usertype"].ToString().ToLower() == "administrator" || Session["usertype"].ToString() == "Accountant")
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
                ddlusername.Enabled = false;
                ddlusername.Width = 240;
                ddlusername.Height = 30;
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

    protected void btnlist_Click(object sender, EventArgs e)
    {
        BindGrid();
    }



    void BindGrid()
    {
        try
        {
            string  Center = "";
            object fromDate = null;
            object Todate = null;

            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );

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

            GV_OpSummary.DataSource = Cshmst_supp_Bal_C.Get_DPRRepotData(Todate, fromDate, username, Center, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), this.labcode_main);
            GV_OpSummary.DataBind();
            
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

   
    protected void btnreport_Click(object sender, EventArgs e)
    {
        //string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"] );

        //#region Myregion2
        //SqlConnection conn2 = DataAccess.ConInitForDC();
        //SqlCommand sc2 = conn2.CreateCommand();
        //sc2.CommandText = "ALTER VIEW [dbo].[VW_dsbill] AS (SELECT DISTINCT dbo.Cshmst.CenterCode as CCode, dbo.Cshmst.RecDate, dbo.Cshmst.Paymenttype AS Expr2, dbo.Cshmst.Patname, "+
        //          "  dbo.Cshmst.Balance, dbo.Cshmst.branchid, dbo.patmst.Patregdate, dbo.Cshmst.DisFlag,  "+
        //          "  dbo.Cshmst.Discount, dbo.Cshmst.NetPayment, dbo.Cshmst.BillNo, dbo.Cshmst.AmtPaid, dbo.patmst.PatRegID,  dbo.patmst.intial, "+
        //          "  dbo.patmst.Patname as FirstName,  dbo.patmst.DrName,  dbo.VW_drdt.DoctorName AS LabName, dbo.VW_drdt.Doctorcode AS DoctorName, " +
        //          "  dbo.patmst.RegistratonDateTime, dbo.patmst.CenterCode ,  dbo.Cshmst.Othercharges, dbo.Cshmst.DigModule,  "+
        //          "  dbo.patmst.RefDr, CAST(dbo.patmst.Tests AS varchar(255)) AS Test1,  dbo.patmst.testname, dbo.patmst.TelNo, "+
        //          "  dbo.VW_rtcrgdata.TestCharges, dbo.patmst.FID,  dbo.RecM.AmtPaid AS rec_amt, dbo.RecM.Paymenttype as Modeofpay,  "+
        //          "  convert(date,RecM.transdate)as transdate , dbo.RecM.username ,dbo.patmst.UnitCode,dbo.patmst.UnitCode AS LabCode,BillAmt, RecM.DisAmt ,RecM.BalAmt, "+
        //          "  dbo.patmst.Phrecdate,RecM.PrevBal  , Cshmst.TaxAmount, Cshmst.TaxPer  , patmst.IsbillBH , case when patmst.IsbillBH=0 then "+
        //          "  RecM.PrevBal else 0 end as Mainbalance,  case when patmst.IsbillBH=1 then RecM.PrevBal else 0 end as BTHbalance ,  "+
        //          "  case when RecM.Paymenttype='Cash' then (RecM.AmtPaid) else  0 end as cashrec ,  case when RecM.Paymenttype='Cash' then 0 else  "+
        //          "  (RecM.AmtPaid) end as Cardrec ,Cshmst.Comment as DisRemark FROM dbo.Cshmst INNER JOIN dbo.patmst ON dbo.Cshmst.PID = dbo.patmst.PID AND  "+
        //          "  dbo.Cshmst.branchid = dbo.patmst.branchid INNER JOIN  dbo.VW_rtcrgdata ON dbo.patmst.PID = dbo.VW_rtcrgdata.PID INNER JOIN "+
        //          "  dbo.RecM ON dbo.Cshmst.BillNo = dbo.RecM.BillNo AND  dbo.Cshmst.branchid = dbo.RecM.branchid LEFT OUTER JOIN  dbo.VW_drdt ON "+
        //          "  dbo.VW_rtcrgdata.branchid = dbo.VW_drdt.branchid AND dbo.patmst.Doctorcode = dbo.VW_drdt.DoctorName  " +
        //              " Where 1=1";
        //if (fromdate.Text != "")
        //{
        //    sc2.CommandText += " and (CAST(CAST(YEAR(transdate) AS varchar(4)) + '/' + CAST(MONTH(transdate) AS varchar(2)) + '/' + CAST(DAY(transdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
        //}
        //if (ddlusername.SelectedItem.Text != "Select UserName")
        //{
        //    sc2.CommandText += " and dbo.RecM.username='" + ddlusername.SelectedItem.Text + " ' ";// 
        //}
        //sc2.CommandText += "   union all SELECT DISTINCT dbo.Cshmst.CenterCode as CCode, dbo.Cshmst.RecDate, dbo.Cshmst.Paymenttype AS Expr2, dbo.Cshmst.Patname,  "+    
        //          "  dbo.Cshmst.Balance, dbo.Cshmst.branchid, dbo.patmst.Patregdate, dbo.Cshmst.DisFlag,  "+
        //          "  -CAST(Cshmst.Discount as numeric(9,2))as Discount,    -dbo.Cshmst.NetPayment, dbo.Cshmst.BillNo, -dbo.Cshmst.AmtPaid, dbo.patmst.PatRegID,   "+
        //          "  dbo.patmst.intial, dbo.patmst.Patname as FirstName,  dbo.patmst.DrName,     dbo.VW_drdt.DoctorName AS LabName, dbo.VW_drdt.Doctorcode AS DoctorName,  " +
        //          "  dbo.patmst.RegistratonDateTime, dbo.patmst.CenterCode ,  dbo.Cshmst.Othercharges,     dbo.Cshmst.DigModule, dbo.patmst.RefDr,  "+
        //          "  CAST(dbo.patmst.Tests AS varchar(255)) AS Test1,  dbo.patmst.testname, dbo.patmst.TelNo,      -dbo.VW_rtcrgdata.TestCharges, dbo.patmst.FID,  "+
        //          "  -dbo.RecM.AmtPaid AS rec_amt, dbo.RecM.Paymenttype as Modeofpay ,  convert(date,RecM.transdate)as transdate ,     dbo.RecM.username , "+
        //          "  dbo.patmst.UnitCode ,dbo.patmst.UnitCode AS LabCode,-BillAmt, -RecM.DisAmt ,RecM.BalAmt, dbo.patmst.Phrecdate,-RecM.PrevBal  ,     "+
        //          "  - Cshmst.TaxAmount, Cshmst.TaxPer  , patmst.IsbillBH , case when patmst.IsbillBH=0 then RecM.PrevBal else 0 end as Mainbalance,       "+
        //          "  case when patmst.IsbillBH=1 then RecM.PrevBal else 0 end as BTHbalance , case when RecM.Paymenttype='Cash' then -(RecM.AmtPaid) else  0 end as cashrec , "+
        //          "  case when RecM.Paymenttype='Cash' then 0 else  -(RecM.AmtPaid) end as Cardrec ,Cshmst.Comment     "+
        //          "  FROM dbo.Cshmst INNER JOIN dbo.patmst ON dbo.Cshmst.PID = dbo.patmst.PID AND  dbo.Cshmst.branchid = dbo.patmst.branchid INNER JOIN      "+
        //          "  dbo.VW_rtcrgdata ON dbo.patmst.PID = dbo.VW_rtcrgdata.PID INNER JOIN  dbo.RecM ON dbo.Cshmst.BillNo = dbo.RecM.BillNo AND      "+
        //          "  dbo.Cshmst.branchid = dbo.RecM.branchid LEFT OUTER JOIN  dbo.VW_drdt ON dbo.VW_rtcrgdata.branchid = dbo.VW_drdt.branchid AND    " +
        //          "  dbo.patmst.Doctorcode = dbo.VW_drdt.DoctorName  "+
        //            " Where 1=1";
        //if (fromdate.Text != "")
        //{
        //    sc2.CommandText += " and patmst.IsFreeze=1 and (CAST(CAST(YEAR(transdate) AS varchar(4)) + '/' + CAST(MONTH(transdate) AS varchar(2)) + '/' + CAST(DAY(transdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
        //}
        //if (ddlusername.SelectedItem.Text != "Select UserName")
        //{
        //    sc2.CommandText += " and dbo.RecM.username='" + ddlusername.SelectedItem.Text + " ' ";// 
        //}

        //sc2.CommandText += ")";

        //conn2.Open();
        //try
        //{
        //    sc2.ExecuteNonQuery();
        //}
        //catch (Exception) { }
        //conn2.Close(); conn2.Dispose();
        //#endregion

        //string sql = "";
        //ReportParameterClass.ReportType = "DailyCashSum";
        //string username = "";
       
       
       
        //Session.Add("rptsql", sql);
        //Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_dailycashierDet.rpt");
        //Session["reportname"] = "DailyCashSummary";
        //Session["RPTFORMAT"] = "pdf";

        //ReportParameterClass.SelectionFormula = sql;
        //string close = "<script language='javascript'>javascript:OpenReport();</script>";
        //Type title1 = this.GetType();
        //Page.ClientScript.RegisterStartupScript(title1, "", close);

        string sql = "";
        Cshmst_supp_Bal_C ObjCSB = new Cshmst_supp_Bal_C();
        ObjCSB.Truncatecashierreport();




        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"]);

        #region Alter VW_dsbill
        #region Myregion2
        SqlConnection conn2 = DataAccess.ConInitForDC();
        SqlConnection conn22 = DataAccess.ConInitForDC();
        SqlCommand sc2 = conn2.CreateCommand();
        sc2.CommandText = "ALTER VIEW [dbo].[VW_Operator_Summary_withClient] AS select top(99.99) percent username, SUM(cash) as cash, SUM(card) as card, SUM(Cheque) as Cheque,SUM(Online) as Online, SUM(ClientPayment) as ClientPayment "+
                         "   from ( "+
                         "   select username,  "+
                         "   case when PaymentType='Cash'  then  Amtpaid else 0 end as Cash,  "+
                         "   case when PaymentType='Card'  then  Amtpaid else 0 end as Card,  "+
                         "   case when PaymentType='Cheque'  then  Amtpaid else 0 end as Cheque, "+ 
                         "   case when PaymentType='Online'  then  Amtpaid else 0 end as Online,  "+
                         "    PaymentType, receiveamount as ClientPayment "+
                         "   from ( "+
                         "   select a.username ,  ((b.PaymentType)) as PaymentType,ISNULL( b.amtpaid,0) as amtpaid, isnull(c.Receiveamount,0) as Receiveamount "+
                         "   from CTuser a left join "+
                         "   ( "+
                         "   select  sum(amtpaid) as amtpaid,username, sum(disamt) as disamt,PaymentType  From RecM " +

                     " Where 1=1 ";


        if (ddlusername.SelectedItem.Text != "Select UserName")
        {
            //username = ddlusername.SelectedItem.Text;
            sc2.CommandText += " and username='" + ddlusername.SelectedItem.Text + "'";
        }
        if (fromdate.Text != "")
        {
            sc2.CommandText += " and (CAST(CAST(YEAR(tdate) AS varchar(4)) + '/' + CAST(MONTH(tdate) AS varchar(2)) + '/' + CAST(DAY(tdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') ";

       }
        sc2.CommandText += " group by username ,PaymentType "+
                         "   ) b on a.username=b.username  "+
                         "   left join ( "+
                         "   select  Username, SUM(Receiveamount) as Receiveamount,PaymentType From CPReceive "+
                         " Where 1=1 ";
        if (ddlusername.SelectedItem.Text != "Select UserName")
        {
            //username = ddlusername.SelectedItem.Text;
            sc2.CommandText += " and CPReceive.username='" + ddlusername.SelectedItem.Text + "'";
        }
        if (fromdate.Text != "")
        {
            sc2.CommandText += " and (CAST(CAST(YEAR(Receivedate) AS varchar(4)) + '/' + CAST(MONTH(Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') ";

        }
        sc2.CommandText += " group by username,PaymentType "+
                          "  ) c on a.username=c.Username  "+
                          "  ) a  "+

                          "  where a.amtpaid<>0 or  Receiveamount<>0   "+
                          "   ) a group by username order by username ";

        conn2.Open();
        try
        {
            sc2.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn2.Close(); conn2.Dispose();
        #endregion
        #endregion

      



       
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_Operator_Summary_withClient.rpt");
        Session["reportname"] = "Operator_Summary_withClient";
        Session["RPTFORMAT"] = "pdf";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);
        //Session["Parameter"] = "Yes";
        //Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        //Session["rptusername"] = Convert.ToString(Session["username"]);

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

    }

    protected void GV_OpSummary_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GV_OpSummary_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string username = GV_OpSummary.Rows[e.NewEditIndex].Cells[10].Text;
            ViewState["username"] = username;
            ViewState["tdate"] = GV_OpSummary.Rows[e.NewEditIndex].Cells[0].Text;

            e.Cancel = true;
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

    protected void GV_OpSummary_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GV_OpSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_OpSummary.PageIndex = e.NewPageIndex;
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