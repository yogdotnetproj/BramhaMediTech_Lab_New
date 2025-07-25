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

public partial class OperatorwiseSummary : System.Web.UI.Page
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
        sc2.CommandText = "ALTER VIEW [dbo].[VW_dsbill_Old] AS (SELECT DISTINCT  " +
                  "  patmst.CenterCode AS CCode, RecM.transdate AS RecDate, RecM.PaymentType AS Expr2, patmst.Patname, 0 AS Balance, RecM.branchid, patmst.Patregdate, 0 AS DisFlag, RecM.DisAmt AS Discount, " +
                  "  patmst.TestCharges AS NetPayment, RecM.BillNo, RecM.AmtPaid - RecM.RefundAmt AS AmtPaid, patmst.PatRegID, patmst.intial, patmst.Patname AS FirstName, patmst.Drname, VW_drdt.DoctorName AS LabName,  " +
                  "  VW_drdt.DoctorCode AS UnitCode, patmst.RegistratonDateTime, patmst.CenterCode, RecM.OtherCharges, 0 AS DigModule, patmst.RefDr, CAST(patmst.Tests AS varchar(255)) AS Test1, patmst.testname, patmst.TelNo, " +
                  "  CASE WHEN RecM.IsActive = 0 THEN Patmst.TestCharges * - 1 ELSE Patmst.TestCharges END AS TestCharges, patmst.FID, RecM.AmtPaid AS rec_amt, RecM.PaymentType, CONVERT(date, RecM.transdate) AS transdate, " +
                  "  RecM.username, patmst.UnitCode AS LabCode, patmst.TestCharges AS BillAmt, RecM.DisAmt, patmst.TestCharges - (RecM.AmtPaid + RecM.DisAmt) AS BalAmt, patmst.Phrecdate, RecM.PrevBal, RecM.TaxAmount,  " +
                  "  RecM.TaxPer, patmst.IsbillBH, CASE WHEN patmst.IsbillBH = 0 THEN dbo.Patmst.TestCharges - ((dbo.RecM.AmtPaid) + (RecM.DisAmt)) ELSE 0 END AS Mainbalance,  " +
                  "  CASE WHEN patmst.IsbillBH = 1 THEN Patmst.TestCharges - ((dbo.RecM.AmtPaid) + (RecM.DisAmt)) ELSE 0 END AS BTHbalance, CASE WHEN ltrim(RecM.Paymenttype) = 'Cash' THEN (RecM.AmtPaid) ELSE 0 END AS cashrec,  " +
                  "  CASE WHEN ltrim(RecM.Paymenttype) = 'Cash' THEN 0 ELSE (RecM.AmtPaid) END AS Cardrec, patmst.IsFreeze, RecM.Comment, RecM.ReceiptNo, RecM.IsActive " +
                  "  FROM            patmst INNER JOIN " +
                  "  VW_rtcrgdata ON patmst.PID = VW_rtcrgdata.PID INNER JOIN " +
                  "  RecM ON patmst.PID = RecM.PID AND VW_rtcrgdata.PID = RecM.PID LEFT OUTER JOIN " +
                  "  VW_drdt ON patmst.DoctorCode = VW_drdt.DoctorName  " +

                     " Where 1=1 ";


        if (ddlusername.SelectedItem.Text != "Select UserName")
        {
            //username = ddlusername.SelectedItem.Text;
            sc2.CommandText += " and dbo.RecM.username='" + ddlusername.SelectedItem.Text + "'";
        }
        if (fromdate.Text != "")
        {
            sc2.CommandText += " and (CAST(CAST(YEAR(transdate) AS varchar(4)) + '/' + CAST(MONTH(transdate) AS varchar(2)) + '/' + CAST(DAY(transdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') )";

            //"  group by dbo.Cshmst.CenterCode, dbo.Cshmst.RecDate, dbo.Cshmst.Paymenttype, dbo.Cshmst.Patname,   "+
            //"   dbo.Cshmst.Balance, dbo.Cshmst.branchid, dbo.patmst.Patregdate,    dbo.Cshmst.DisFlag,   "+
            //"   dbo.Cshmst.Discount, dbo.Cshmst.NetPayment, dbo.Cshmst.BillNo,   dbo.Cshmst.AmtPaid, dbo.patmst.PatRegID,  "+
            //"   dbo.patmst.intial, dbo.patmst.Patname,     dbo.patmst.DrName,  dbo.VW_drdt.DoctorName , dbo.VW_drdt.DoctorCode ,  "+ 
            //"   dbo.patmst.RegistratonDateTime, dbo.patmst.CenterCode,  dbo.Cshmst.Othercharges, dbo.Cshmst.DigModule,   "+ 
            //"   dbo.patmst.RefDr, CAST(dbo.patmst.Tests AS varchar(255)) ,  dbo.patmst.testname, dbo.patmst.TelNo,   "+
            //"   dbo.VW_rtcrgdata.TestCharges, dbo.patmst.FID,  dbo.RecM.Paymenttype ,    convert(date,RecM.transdate) , "+
            //"   dbo.RecM.username ,dbo.patmst.UnitCode ,     RecM.DisAmt ,RecM.BalAmt, dbo.patmst.Phrecdate,RecM.PrevBal , "+
            //"   Cshmst.TaxAmount, Cshmst.TaxPer,patmst.IsbillBH,dbo.RecM.AmtPaid ,patmst.IsFreeze,Cshmst.Comment ,RecM.ReceiptNo,RecM.IsActive,RecM.IsActive,dbo.Cshmst.RefundAmt )";// 
        }
        conn2.Open();
        try
        {
            sc2.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn2.Close(); conn2.Dispose();
        #endregion
        #endregion

        ReportParameterClass.ReportType = "DailyCash";
        string username = "";
        string CenterCode = "";
        if (Session["usertype"].ToString() == "CollectionCenter")
        {
            ReportParameterClass.SelectionFormula = "{VW_dsbillpr.transdate} in DateTime ('" + Convert.ToDateTime(fromdate.Text).ToString("dd/MMM/yyyy") + "') to DateTime ('" + Convert.ToDateTime(todate.Text).ToString("dd/MMM/yyyy") + "') and {VW_dsbillpr.branchid}=" + Convert.ToInt32(Session["Branchid"]) + " and {VW_dsbillpr.maindeptid}=" + Convert.ToInt32(Session["DigModule"]) + " and {VW_dsbillpr.CenterCode}='" + Session["CenterCode"].ToString() + "'";
        }
        else
        {

            if (labcode != "" && labcode != null)
            {
                ReportParameterClass.SelectionFormula = "{VW_dsbillpr.transdate} in DateTime ('" + Convert.ToDateTime(fromdate.Text).ToString("dd/MMM/yyyy") + "') to DateTime ('" + Convert.ToDateTime(todate.Text).AddDays(1).ToString("dd/MMM/yyyy") + "') and {VW_dsbillpr.branchid}=" + Convert.ToInt32(Session["Branchid"]) + " and {VW_dsbillpr.maindeptid}=" + Convert.ToInt32(Session["DigModule"]) + " and {VW_dsbillpr.LabCode}='" + labcode + "'";
            }
            else
            {
                ReportParameterClass.SelectionFormula = "{VW_dsbillpr.transdate} in DateTime ('" + Convert.ToDateTime(fromdate.Text).ToString("dd/MMM/yyyy") + "') to DateTime ('" + Convert.ToDateTime(todate.Text).AddDays(1).ToString("dd/MMM/yyyy") + "') and {VW_dsbillpr.branchid}=" + Convert.ToInt32(Session["Branchid"]) + " and {VW_dsbillpr.maindeptid}=" + Convert.ToInt32(Session["DigModule"]) + "";
            }

        }



        ObjCSB.Insertcashierreport();
        ObjCSB.Insertcashierreport_ref();

        SqlCommand sc22 = conn22.CreateCommand();
        sc22.CommandText = "ALTER VIEW [dbo].[VW_dsbill] AS SELECT        TOP (99.99) PERCENT patmst.CenterCode AS CCode, CasherReport.BillDate AS RecDate, CasherReport.Modeofpay AS Expr2, patmst.Patname AS FirstName, CasherReport.Patienttest AS Test1, CasherReport.Balance, " +
                          "  CasherReport.Branchid, CasherReport.Patregdate, CasherReport.DisFlag, CasherReport.Discount, CasherReport.NetPayment, CasherReport.BillNo, CasherReport.AmtPaid, CasherReport.Patregid, patmst.intial, " +
                          "  patmst.Patname, patmst.DrName as DoctorName, CasherReport.LabName, CasherReport.RegistratonDateTime, patmst.CenterCode, CasherReport.Othercharges, CasherReport.maindeptid AS DigModule, CasherReport.RefDr, " +
                          "  CasherReport.testname, CasherReport.TelNo, CasherReport.TestCharges, CasherReport.FID, CasherReport.rec_amt, CasherReport.Modeofpay, CasherReport.transdate, CasherReport.username, CasherReport.BillAmt, " +
                          "  CasherReport.DisAmt, CasherReport.BalAmt, CasherReport.Phrecdate, CasherReport.PrevBal, CasherReport.TaxAmount, CasherReport.TaxPer, CasherReport.IsbillBH, CasherReport.Mainbalance, CasherReport.BTHbalance, " +
                          "  CasherReport.cashrec, CasherReport.Cardrec, CasherReport.DisRemark, '' AS UnitCode, '' AS LabCode, '' AS DrName, CasherReport.IsActive " +
                          "  FROM            CasherReport INNER JOIN " +
                          "  patmst ON CasherReport.Patregid = patmst.PatRegID  ";//INNER JOIN   Cshmst ON patmst.PID = Cshmst.PID
        //if (RblCashType.SelectedItem.Text == "Cash")
        //{
        //    //username = ddlusername.SelectedItem.Text;
        //    sc22.CommandText += " where Modeofpay ='Cash'";
        //}
        //if (RblCashType.SelectedItem.Text == "Card")
        //{
        //    //username = ddlusername.SelectedItem.Text;
        //    sc22.CommandText += " where Modeofpay ='Credit Card'";
        //}
        //if (RblCashType.SelectedItem.Text == "Online Gateway")
        //{
        //    //username = ddlusername.SelectedItem.Text;
        //    sc22.CommandText += " where Modeofpay ='Online Gateway '";
        //}
        sc22.CommandText += " union all select 'Expence Entry', ExpenceDate,'Cash',Particular,'',0,Branchid,currentdate,1,0,-ExpenceAmount,0,-ExpenceAmount " +
              "  ,'0' as Patregid,'',Particular,'','',currentdate,'Expence Entry',0,0,'','','0',-ExpenceAmount,0,-ExpenceAmount,'Cash',ExpenceDate,UserName, " +
              "  -ExpenceAmount,0,0,ExpenceDate,0,0,0,0,0,0,-ExpenceAmount,0,'','','','',1 from DailyExpenceDetails where   (CAST(CAST(YEAR(ExpenceDate) AS varchar(4)) + '/' + CAST(MONTH(ExpenceDate) AS varchar(2)) + '/' + CAST(DAY(ExpenceDate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') ";
        if (ddlusername.SelectedItem.Text != "Select UserName")
        {
            sc22.CommandText += " and UserName='" + ddlusername.SelectedItem.Text + "' ";
        }

        sc22.CommandText += " Union all select 'Lender',RequestDate ,'Cash','Money Request','',0,Branchid,RequestDate,1,0,-ReceiveAmt,0,-ReceiveAmt,'0','','Monet Request', " +
              "  '','',RequestDate,'Monet Request',0,0,'','','0',-ReceiveAmt,0,-ReceiveAmt,'Cash',RequestDate,RequestFrom,-ReceiveAmt, " +
              "  0,0,RequestDate,0,0,0,0,0,0,-ReceiveAmt,0,'','','','',1 " +
              "  from UserCashExchangeRequest where RequestApprove=1 and (CAST(CAST(YEAR(RequestDate) AS varchar(4)) + '/' + CAST(MONTH(RequestDate) AS varchar(2)) + '/' + CAST(DAY(RequestDate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') ";
        if (ddlusername.SelectedItem.Text != "Select UserName")
        {
            sc22.CommandText += " and RequestTo='" + ddlusername.SelectedItem.Text + "' ";
        }
        sc22.CommandText += " union all " +
       "  select 'Borrower',RequestDate ,'Cash','Money Request','',0,Branchid,RequestDate,1,0,ReceiveAmt,0,ReceiveAmt,'0','','Monet Request', " +
       "  '','',RequestDate,'Monet Request',0,0,'','','0',ReceiveAmt,0,ReceiveAmt,'Cash',RequestDate,RequestTo,ReceiveAmt, " +
       "  0,0,RequestDate,0,0,0,0,0,0,ReceiveAmt,0,'','','','',1 " +
       "  from UserCashExchangeRequest where RequestApprove=1  and (CAST(CAST(YEAR(RequestDate) AS varchar(4)) + '/' + CAST(MONTH(RequestDate) AS varchar(2)) + '/' + CAST(DAY(RequestDate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') ";
        if (ddlusername.SelectedItem.Text != "Select UserName")
        {
            sc22.CommandText += " and RequestFrom='" + ddlusername.SelectedItem.Text + "' ";
        }

        // sc22.CommandText += " order by CAST(CasherReport.Patregid AS int) asc ";

        conn22.Open();
        try
        {
            sc22.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn22.Close(); conn22.Dispose();
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_dailycashier_Summary.rpt");
        Session["reportname"] = "DailyCashSummary";
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