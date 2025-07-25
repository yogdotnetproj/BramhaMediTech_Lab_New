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
using System.Web.Services;
using System.Web.Script.Services;

public partial class DrReport : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string drcode = "",PRO="";
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
                        checkexistpageright("DrReport.aspx");
                    }
                }
                fromdate.Text = System.DateTime.Now.ToShortDateString();
                todate.Text = System.DateTime.Now.ToShortDateString();

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


  
    protected void btnreport_Click(object sender, EventArgs e)
    {
        string sql = "";
        //if (Request.QueryString["frm"].ToString() == "Compliment")
        //{
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        //cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT     dbo.Doctor_CalculateAmount.DoctorAmount AS Expr1, patmst.intial+' '+dbo.Doctor_CalculateAmount.Patname as fname,  dbo.patmst.Drname, dbo.patmst.Tests, " +
        //          " dbo.patmst.RegistratonDateTime, dbo.Doctor_CalculateAmount.TotalAmount, dbo.patmst.branchid, " +
        //          " CAST(dbo.Cshmst.Discount AS numeric) AS Discount,  dbo.Cshmst.DisFlag, " +
        //          " dbo.Cshmst.Othercharges, dbo.patmst.TestCharges, dbo.patmst.PID, dbo.patmst.PatRegID, " +
        //          " dbo.patmst.Centername, dbo.Cshmst.Balance, dbo.patmst.TestName , dbo.patmst.UnitCode,dbo.patmst.DoctorCode " +
        //          " FROM dbo.Doctor_CalculateAmount INNER JOIN    dbo.patmst ON dbo.Doctor_CalculateAmount.PatRegID = dbo.patmst.PatRegID AND dbo.Doctor_CalculateAmount.FID = dbo.patmst.FID AND " +
        //          " dbo.Doctor_CalculateAmount.branchid = dbo.patmst.branchid INNER JOIN " +
        //          " dbo.Cshmst ON dbo.patmst.PatRegID = dbo.Cshmst.PatRegID AND dbo.patmst.PID = dbo.Cshmst.PID " +
        //          " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " AND (dbo.Cshmst.Discount <> '')"; //patmst.IsbillBH=0 and

        //string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        //if (labcode != null && labcode != "")
        //{
        //    cmd1.CommandText = cmd1.CommandText + " and patmst.UnitCode='" + labcode + "'";
        //}
        //if (fromdate.Text != "" && todate.Text != "")
        //{
        //    cmd1.CommandText = cmd1.CommandText + " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        //}
        //if (txtdoctor.Text != "")
        //{
        //    string str = Convert.ToString(txtdoctor.Text);
        //    string[] s = str.Split('=');
        //    drcode = s[1];

        //    cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode='" + drcode.Trim() + "'";
        //}
        //cmd1.CommandText = cmd1.CommandText + ")";

        //==================================

       // cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT   distinct    case when Cshmst.balance>0 then  0 else patmstd.dramt end as Expr1, patmst.intial + ' ' + Doctor_CalculateAmount.PatName AS Fname, patmst.Drname, '' AS Tests, patmst.RegistratonDateTime, " +
       //         "  Doctor_CalculateAmount.TotalAmount, patmst.Branchid, CAST(Cshmst.Discount AS numeric) AS Discount, Cshmst.DisFlag, Cshmst.Othercharges, patmstd.TestRate AS TestCharges, patmst.PID, patmst.PatRegID, " +
       //         " patmst.CenterName, Cshmst.Balance, case when [PackMst].PackageName is null  then  MainTest.Maintestname else [PackMst].PackageName end   AS testname, " +
       //         " patmst.UnitCode, patmst.DoctorCode, " +
       //         "  SUM(RecM.LabGiven) AS LabGiven,SUM(RecM.DrGiven) AS DrGiven      " +
       //" FROM            Doctor_CalculateAmount INNER JOIN " +
       // " patmst ON Doctor_CalculateAmount.PatRegID = patmst.PatRegID AND Doctor_CalculateAmount.FID = patmst.FID AND Doctor_CalculateAmount.Branchid = patmst.Branchid INNER JOIN " +
       // " Cshmst ON patmst.PatRegID = Cshmst.PatRegID AND patmst.PID = Cshmst.PID INNER JOIN " +
       // " RecM ON Cshmst.BillNo = RecM.BillNo AND patmst.PID = RecM.PID INNER JOIN " +
       // " patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN " +
       // " MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode LEFT OUTER JOIN " +
       // " [PackMst] ON patmstd.PackageCode = [PackMst].PackageCode AND patmstd.Branchid = [PackMst].branchid " +
       //" WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " AND (dbo.Cshmst.Discount <> '')"; //patmst.IsbillBH=0 and

       // string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
       // if (labcode != null && labcode != "")
       // {

       //     cmd1.CommandText = cmd1.CommandText + " and patmst.UnitCode='" + labcode + "'";
       // }
       // if (fromdate.Text != "" && todate.Text != "")
       // {
       //     cmd1.CommandText = cmd1.CommandText + " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
       // }
       // if (txtdoctor.Text != "")
       // {
       //     string str = Convert.ToString(txtdoctor.Text);
       //     string[] s = str.Split('=');
       //     drcode = s[1];

       //     cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode='" + drcode.Trim() + "'";
       // }
       // cmd1.CommandText = cmd1.CommandText + " GROUP BY patmstd.dramt,Doctor_CalculateAmount.DoctorAmount, patmst.intial, Doctor_CalculateAmount.PatName, patmst.Drname,  patmst.RegistratonDateTime, " +
       //       "  Doctor_CalculateAmount.TotalAmount, patmst.Branchid, Cshmst.Discount, Cshmst.DisFlag, Cshmst.Othercharges, patmstd.TestRate, patmst.PID, patmst.PatRegID, patmst.CenterName, Cshmst.Balance, " +
       //        " patmst.UnitCode, patmst.DoctorCode, MainTest.Maintestname, [PackMst].PackageName ";

       // cmd1.CommandText = cmd1.CommandText + ")";

       // con.Open();
       // cmd1.ExecuteNonQuery();
       // con.Close(); con.Dispose();

       // ReportParameterClass.ReportType = "ComplimentReport";
       // sql = "{VW_drspmain.RegistratonDateTime} In DateTime('" + Convert.ToDateTime(fromdate.Text).AddDays(-1).ToString("dd/MM/yyyy") + "') To DateTime ('" + Convert.ToDateTime(todate.Text).AddDays(+1).ToString("dd/MM/yyyy") + "') and {VW_drspmain.branchid}=" + Convert.ToInt32(Session["Branchid"]) + "";
       // if (txtdoctor.Text != "")


       //     sql += " and {VW_drspmain.DoctorCode}='" + drcode.Trim() + "'";

       // ReportParameterClass.SelectionFormula = sql;
        // }

        cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT DISTINCT   dbo.FUN_GetCompAmt(patmst.Branchid, patmst.PID, CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END) AS CompAmt, " +
             "  dbo.FUN_Getpcountcomp(patmst.Branchid, patmst.PID)   AS TAmt, CASE WHEN Patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) > 0 THEN 0 ELSE patmstd.dramt END AS Expr1, " +
             "  patmst.intial + ' ' + Doctor_CalculateAmount.PatName AS Fname, patmst.Drname,    '' AS Tests, patmst.RegistratonDateTime, Doctor_CalculateAmount.TotalAmount, " +
             "  patmst.Branchid, SUM(RecM.DisAmt) AS Discount, 0 AS DisFlag, SUM(RecM.OtherCharges) as OtherCharges, patmstd.TestRate AS TestCharges, patmst.PID,    patmst.PatRegID, patmst.CenterName, " +
             "  patmst.TestCharges - ((SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) - SUM(RecM.OtherCharges)) AS Balance, " +
             "  CASE WHEN [PackMst].PackageName IS NULL    THEN MainTest.Maintestname ELSE [PackMst].PackageName END AS testname, patmst.UnitCode, patmst.DoctorCode, " +
             "  SUM(RecM.LabGiven) AS LabGiven, SUM(RecM.DrGiven) AS DrGiven,    CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END AS TestCCode, " +
             "  CTuser.Name ,patmst.IsbillBH " +
             "  FROM            CTuser RIGHT OUTER JOIN   DrMT INNER JOIN   Doctor_CalculateAmount INNER JOIN   patmst ON Doctor_CalculateAmount.PatRegID = patmst.PatRegID AND " +
             "  Doctor_CalculateAmount.FID = patmst.FID AND Doctor_CalculateAmount.Branchid = patmst.Branchid INNER JOIN   RecM ON  patmst.PID = RecM.PID INNER JOIN  " +
             "  patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN   MainTest ON " +
             "  patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode ON DrMT.DoctorCode = patmst.DoctorCode LEFT OUTER JOIN   " +
             "  PackMst ON patmstd.PackageCode = PackMst.PackageCode AND patmstd.Branchid = PackMst.branchid ON CTuser.CUId = DrMT.PRO   " +
        " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " "; //patmst.IsbillBH=0 and

        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {

            cmd1.CommandText = cmd1.CommandText + " and patmst.UnitCode='" + labcode + "'";
        }
        if (fromdate.Text != "" && todate.Text != "")
        {
            cmd1.CommandText = cmd1.CommandText + " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }
        if (txtdoctor.Text != "")
        {
            string str = Convert.ToString(txtdoctor.Text);
            string[] s = str.Split('=');
            drcode = s[1];

            cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode='" + drcode.Trim() + "'";
        }
        if (txtPro.Text != "")
        {
            string str = Convert.ToString(txtPro.Text);
            string[] s = str.Split('=');
            PRO = s[1];

            cmd1.CommandText = cmd1.CommandText + " and  DrMT.PRO='" + PRO.Trim() + "'";
        }
        cmd1.CommandText = cmd1.CommandText + " GROUP BY patmstd.Dramt, Doctor_CalculateAmount.DoctorAmount, patmst.intial, Doctor_CalculateAmount.PatName, patmst.Drname, patmst.RegistratonDateTime, " +
               " Doctor_CalculateAmount.TotalAmount, patmst.Branchid,     patmstd.TestRate, patmst.PID, patmst.PatRegID, patmst.CenterName, " +
               " patmst.UnitCode, patmst.DoctorCode, MainTest.Maintestname, PackMst.PackageName,  " +
               " CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END, CTuser.Name,patmst.TestCharges,patmst.IsbillBH  ";

        cmd1.CommandText = cmd1.CommandText + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        ReportParameterClass.ReportType = "PDFComplimentReport";
        sql = "{VW_drspmain.RegistratonDateTime} In DateTime('" + Convert.ToDateTime(fromdate.Text).AddDays(-1).ToString("dd/MM/yyyy") + "') To DateTime ('" + Convert.ToDateTime(todate.Text).AddDays(+1).ToString("dd/MM/yyyy") + "') and {VW_drspmain.branchid}=" + Convert.ToInt32(Session["Branchid"]) + "";
        if (txtdoctor.Text != "")


            sql += " and {VW_drspmain.DoctorCode}='" + drcode.Trim() + "'";

        ReportParameterClass.SelectionFormula = sql;

        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_docshareRep.rpt");
        Session["reportname"] = "ComplimentReport";
        Session["RPTFORMAT"] = "EXCEL";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

      
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillPRO(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select CUId,Name as DoctorName from ctuser where ( Name like N'%" + prefixText + "%' or CUId like N'%" + prefixText + "%' ) and usertype='PRO' and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + "", con);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] doctors = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {

            doctors.SetValue(dr["DoctorName"] + "=" + dr["CUId"], i);
            i++;
        }
        return doctors;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillDoctor(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select DoctorCode,rtrim(DrInitial)+' '+DoctorName as DoctorName from DrMT where ( DoctorName like N'%" + prefixText + "%' or DoctorCode like N'%" + prefixText + "%' ) and DrType='DR' and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + "", con);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] doctors = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {

            doctors.SetValue(dr["DoctorName"] + "=" + dr["DoctorCode"], i);
            i++;
        }
        return doctors;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] GetCenter(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select DoctorCode,rtrim(DrInitial)+' '+DoctorName as DoctorName from DrMT where ( DoctorName like N'%" + prefixText + "%' or DoctorCode like N'%" + prefixText + "%' ) and DrType='CC' and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + "", con);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] doctors = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {

            doctors.SetValue(dr["DoctorName"] + "=" + dr["DoctorCode"], i);
            i++;
        }
        return doctors;
    }

    protected void btnPdfReport_Click(object sender, EventArgs e)
    {
        string sql = "";

        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        //cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT     dbo.Doctor_CalculateAmount.DoctorAmount AS Expr1, patmst.intial+' '+dbo.Doctor_CalculateAmount.Patname as fname, dbo.patmst.Drname, dbo.patmst.Tests, " +
        //          " dbo.patmst.RegistratonDateTime, dbo.Doctor_CalculateAmount.TotalAmount, dbo.patmst.branchid, " +
        //          " CAST(dbo.Cshmst.Discount AS numeric) AS Discount,  dbo.Cshmst.DisFlag, " +
        //          " dbo.Cshmst.Othercharges, dbo.patmst.TestCharges, dbo.patmst.PID, dbo.patmst.PatRegID, " +
        //          " dbo.patmst.CenterName, dbo.Cshmst.Balance, dbo.patmst.TestName , dbo.patmst.UnitCode,dbo.patmst.DoctorCode " +
        //          " FROM dbo.Doctor_CalculateAmount INNER JOIN    dbo.patmst ON dbo.Doctor_CalculateAmount.PatRegID = dbo.patmst.PatRegID AND dbo.Doctor_CalculateAmount.FID = dbo.patmst.FID AND " +
        //          " dbo.Doctor_CalculateAmount.branchid = dbo.patmst.branchid INNER JOIN " +
        //          " dbo.Cshmst ON dbo.patmst.PatRegID = dbo.Cshmst.PatRegID AND dbo.patmst.PID = dbo.Cshmst.PID " +
        //          " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " AND (dbo.Cshmst.Discount <> '')"; //patmst.IsbillBH=0 and

        //string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        //if (labcode != null && labcode != "")
        //{

        //    cmd1.CommandText = cmd1.CommandText + " and patmst.UnitCode='" + labcode + "'";
        //}
        //if (fromdate.Text != "" && todate.Text != "")
        //{
        //    cmd1.CommandText = cmd1.CommandText + " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        //}
        //if (txtdoctor.Text != "")
        //{
        //    string str = Convert.ToString(txtdoctor.Text);
        //    string[] s = str.Split('=');
        //    drcode = s[1];

        //    cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode='" + drcode.Trim() + "'";
        //}
        //cmd1.CommandText = cmd1.CommandText + ")";

        cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT DISTINCT   dbo.FUN_GetCompAmt(patmst.Branchid, patmst.PID, CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END) AS CompAmt, "+
              "  dbo.FUN_Getpcountcomp(patmst.Branchid, patmst.PID)   AS TAmt, CASE WHEN Patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) > 0 THEN 0 ELSE patmstd.dramt END AS Expr1, "+
              "  patmst.intial + ' ' + Doctor_CalculateAmount.PatName AS Fname, patmst.Drname,    '' AS Tests, patmst.RegistratonDateTime, Doctor_CalculateAmount.TotalAmount, "+
              "  patmst.Branchid, SUM(RecM.DisAmt) AS Discount, 0 AS DisFlag,  SUM(RecM.OtherCharges) as OtherCharges, patmstd.TestRate AS TestCharges, patmst.PID,    patmst.PatRegID, patmst.CenterName, " +
              "  patmst.TestCharges - ((SUM(RecM.AmtPaid) + SUM(RecM.DisAmt))- SUM(RecM.OtherCharges)) AS Balance, " +
              "  CASE WHEN [PackMst].PackageName IS NULL    THEN MainTest.Maintestname ELSE [PackMst].PackageName END AS testname, patmst.UnitCode, patmst.DoctorCode, "+
              "  SUM(RecM.LabGiven) AS LabGiven, SUM(RecM.DrGiven) AS DrGiven,    CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END AS TestCCode, "+
              "  CTuser.Name ,patmst.IsbillBH " +
              "  FROM            CTuser RIGHT OUTER JOIN   DrMT INNER JOIN   Doctor_CalculateAmount INNER JOIN   patmst ON Doctor_CalculateAmount.PatRegID = patmst.PatRegID AND "+
              "  Doctor_CalculateAmount.FID = patmst.FID AND Doctor_CalculateAmount.Branchid = patmst.Branchid INNER JOIN   RecM ON  patmst.PID = RecM.PID INNER JOIN  "+
              "  patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN   MainTest ON "+
              "  patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode ON DrMT.DoctorCode = patmst.DoctorCode LEFT OUTER JOIN   "+
              "  PackMst ON patmstd.PackageCode = PackMst.PackageCode AND patmstd.Branchid = PackMst.branchid ON CTuser.CUId = DrMT.PRO   "+
          " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + ""; // AND (dbo.Cshmst.Discount <> '') patmst.IsbillBH=0 and

        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {

            cmd1.CommandText = cmd1.CommandText + " and patmst.UnitCode='" + labcode + "'";
        }
        if (fromdate.Text != "" && todate.Text != "")
        {
            cmd1.CommandText = cmd1.CommandText + " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }
        if (txtdoctor.Text != "")
        {
            string str = Convert.ToString(txtdoctor.Text);
            string[] s = str.Split('=');
            drcode = s[1];

            cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode='" + drcode.Trim() + "'";
        }
        if (txtPro.Text != "")
        {
            string str = Convert.ToString(txtPro.Text);
            string[] s = str.Split('=');
            PRO = s[1];

            cmd1.CommandText = cmd1.CommandText + " and  DrMT.PRO='" + PRO.Trim() + "'";
        }
        cmd1.CommandText = cmd1.CommandText + " GROUP BY patmstd.Dramt, Doctor_CalculateAmount.DoctorAmount, patmst.intial, Doctor_CalculateAmount.PatName, patmst.Drname, patmst.RegistratonDateTime, "+
               " Doctor_CalculateAmount.TotalAmount, patmst.Branchid,    patmstd.TestRate, patmst.PID, patmst.PatRegID, patmst.CenterName, "+
               " patmst.UnitCode, patmst.DoctorCode, MainTest.Maintestname, PackMst.PackageName,  "+
               " CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END, CTuser.Name,patmst.TestCharges ,patmst.IsbillBH ";

        cmd1.CommandText = cmd1.CommandText + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        ReportParameterClass.ReportType = "PDFComplimentReport";
        sql = "{VW_drspmain.RegistratonDateTime} In DateTime('" + Convert.ToDateTime(fromdate.Text).AddDays(-1).ToString("dd/MM/yyyy") + "') To DateTime ('" + Convert.ToDateTime(todate.Text).AddDays(+1).ToString("dd/MM/yyyy") + "') and {VW_drspmain.branchid}=" + Convert.ToInt32(Session["Branchid"]) + "";
        if (txtdoctor.Text != "")


            sql += " and {VW_drspmain.DoctorCode}='" + drcode.Trim() + "'";

        ReportParameterClass.SelectionFormula = sql;

        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_docshareRep.rpt");
        Session["reportname"] = "ComplimentReport";
        Session["RPTFORMAT"] = "pdf";


        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

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


            SqlConnection con = DataAccess.ConInitForDC();
            SqlCommand cmd1 = con.CreateCommand();
            //cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT     dbo.Doctor_CalculateAmount.DoctorAmount AS Expr1, patmst.intial+' '+dbo.Doctor_CalculateAmount.Patname as Fname,  dbo.patmst.Drname, dbo.patmst.Tests, " +
            //          " dbo.patmst.RegistratonDateTime, dbo.Doctor_CalculateAmount.TotalAmount,  dbo.patmst.branchid, " +
            //          " CAST(dbo.Cshmst.Discount AS numeric) AS Discount, dbo.Cshmst.DisFlag,  " +
            //          " dbo.Cshmst.Othercharges, dbo.patmst.TestCharges, dbo.patmst.PID, dbo.patmst.PatRegID, " +
            //          " dbo.patmst.CenterName, dbo.Cshmst.Balance, dbo.patmst.TestName , dbo.patmst.UnitCode,dbo.patmst.DoctorCode " +
            //          " FROM dbo.Doctor_CalculateAmount INNER JOIN    dbo.patmst ON dbo.Doctor_CalculateAmount.PatRegID = dbo.patmst.PatRegID AND dbo.Doctor_CalculateAmount.FID = dbo.patmst.FID AND " +
            //          " dbo.Doctor_CalculateAmount.branchid = dbo.patmst.branchid INNER JOIN " +
            //          " dbo.Cshmst ON dbo.patmst.PatRegID = dbo.Cshmst.PatRegID AND dbo.patmst.PID = dbo.Cshmst.PID " +
            //          " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " AND (dbo.Cshmst.Discount <> '')"; //patmst.IsbillBH=0 and

            //string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
            //if (labcode != null && labcode != "")
            //{

            //    cmd1.CommandText = cmd1.CommandText + " and patmst.UnitCode='" + labcode + "'";
            //}
            //if (fromdate.Text != "" && todate.Text != "")
            //{
            //    cmd1.CommandText = cmd1.CommandText + " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
            //}
            //if (txtdoctor.Text != "")
            //{
            //    string str = Convert.ToString(txtdoctor.Text);
            //    string[] s = str.Split('=');
            //    drcode = s[1];

            //    cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode='" + drcode.Trim() + "'";
            //}
            //cmd1.CommandText = cmd1.CommandText + ")";

            cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT DISTINCT   dbo.FUN_GetCompAmt(patmst.Branchid, patmst.PID, CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END) AS CompAmt, "+
                   " dbo.FUN_Getpcountcomp(patmst.Branchid, patmst.PID)   AS TAmt, CASE WHEN Patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) > 0 THEN 0 ELSE patmstd.dramt END AS Expr1, "+
                   " patmst.intial + ' ' + Doctor_CalculateAmount.PatName AS Fname, patmst.Drname,    '' AS Tests, patmst.RegistratonDateTime, Doctor_CalculateAmount.TotalAmount, "+
                   " patmst.Branchid, SUM(RecM.DisAmt) AS Discount, 0 AS DisFlag, RecM.OtherCharges, patmstd.TestRate AS TestCharges, patmst.PID,    patmst.PatRegID, patmst.CenterName, "+
                   " patmst.TestCharges - ((SUM(RecM.AmtPaid) + SUM(RecM.DisAmt))- SUM(RecM.OtherCharges)) AS Balance, " +
                   " CASE WHEN [PackMst].PackageName IS NULL    THEN MainTest.Maintestname ELSE [PackMst].PackageName END AS testname, patmst.UnitCode, patmst.DoctorCode, "+
                   " SUM(RecM.LabGiven) AS LabGiven, SUM(RecM.DrGiven) AS DrGiven,    CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END AS TestCCode, "+
                   " CTuser.Name ,patmst.IsbillBH " +
                   " FROM            CTuser RIGHT OUTER JOIN   DrMT INNER JOIN   Doctor_CalculateAmount INNER JOIN   patmst ON Doctor_CalculateAmount.PatRegID = patmst.PatRegID AND "+
                   " Doctor_CalculateAmount.FID = patmst.FID AND Doctor_CalculateAmount.Branchid = patmst.Branchid INNER JOIN   RecM ON  patmst.PID = RecM.PID INNER JOIN  "+
                   " patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN   MainTest ON "+
                   " patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode ON DrMT.DoctorCode = patmst.DoctorCode LEFT OUTER JOIN   "+
                   " PackMst ON patmstd.PackageCode = PackMst.PackageCode AND patmstd.Branchid = PackMst.branchid ON CTuser.CUId = DrMT.PRO " +
       " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + ""; //patmst.IsbillBH=0 and

            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
            if (labcode != null && labcode != "")
            {

                cmd1.CommandText = cmd1.CommandText + " and patmst.UnitCode='" + labcode + "'";
            }
            if (fromdate.Text != "" && todate.Text != "")
            {
                cmd1.CommandText = cmd1.CommandText + " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
            }
            if (txtdoctor.Text != "")
            {
                string str = Convert.ToString(txtdoctor.Text);
                string[] s = str.Split('=');
                drcode = s[1];

                cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode='" + drcode.Trim() + "'";
            }
            if (txtPro.Text != "")
            {
                string str = Convert.ToString(txtPro.Text);
                string[] s = str.Split('=');
                PRO = s[1];

                cmd1.CommandText = cmd1.CommandText + " and  DrMT.PRO='" + PRO.Trim() + "'";
            }
            cmd1.CommandText = cmd1.CommandText + " GROUP BY patmstd.Dramt, Doctor_CalculateAmount.DoctorAmount, patmst.intial, Doctor_CalculateAmount.PatName, patmst.Drname, patmst.RegistratonDateTime, "+
                  "  Doctor_CalculateAmount.TotalAmount, patmst.Branchid,    RecM.Othercharges, patmstd.TestRate, patmst.PID, patmst.PatRegID, patmst.CenterName, "+
                  "  patmst.UnitCode, patmst.DoctorCode, MainTest.Maintestname, PackMst.PackageName,  "+
                  "   CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END, CTuser.Name,patmst.TestCharges,patmst.IsbillBH ";

            cmd1.CommandText = cmd1.CommandText + ")";

            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close(); con.Dispose();

            GV_Drcomp.DataSource = Cshmst_supp_Bal_C.GetDoctorcomplimentData();
            GV_Drcomp.DataBind();
            float sum = 0.0f, Charges = 0, DrAmt = 0, DrDis = 0, CnDis=0,TDis=0,TBal=0,FDrAm=0;

            for (int i = 0; i < GV_Drcomp.Rows.Count; i++)
            {
               // sum += Convert.ToSingle(GV_Drcomp.Rows[i].Cells[8].Text);
                if (i > 0)
                {
                    if (GV_Drcomp.DataKeys[i].Value.ToString().Trim() == GV_Drcomp.DataKeys[i - 1].Value.ToString().Trim())
                    {
                        GV_Drcomp.Rows[i].Cells[0].Text = "";
                        GV_Drcomp.Rows[i].Cells[1].Text = "";
                        GV_Drcomp.Rows[i].Cells[2].Text = "";
                        GV_Drcomp.Rows[i].Cells[3].Text = "";
                        GV_Drcomp.Rows[i].Cells[4].Text = "";

                       // GV_Drcomp.Rows[i].Cells[7].Text = "";
                        GV_Drcomp.Rows[i].Cells[8].Text = "";
                        GV_Drcomp.Rows[i].Cells[9].Text = "";
                        GV_Drcomp.Rows[i].Cells[10].Text = "";
                        GV_Drcomp.Rows[i].Cells[11].Text = "";
                        GV_Drcomp.Rows[i].Cells[12].Text = "";
                        GV_Drcomp.Rows[i].Cells[13].Text = "";
                    }
                }
                string  txt_17 = (GV_Drcomp.Rows[i].Cells[6].Text);
                if (txt_17 != "")
                {
                    Charges = Charges + Convert.ToSingle(txt_17);
                   
                }
                string DrAmtT = (GV_Drcomp.Rows[i].Cells[7].Text);
                if (DrAmtT != "")
                {
                    DrAmt = DrAmt + Convert.ToSingle(DrAmtT);
                }
                string DrDisT = (GV_Drcomp.Rows[i].Cells[8].Text);
                if (DrDisT != "")
                {
                    DrDis = DrDis + Convert.ToSingle(DrDisT);
                }
                string CnDisT = (GV_Drcomp.Rows[i].Cells[9].Text);
                if (CnDisT != "")
                {
                    CnDis = CnDis + Convert.ToSingle(CnDisT);
                }
                string TDisT = (GV_Drcomp.Rows[i].Cells[10].Text);
                if (TDisT != "")
                {
                    TDis = TDis + Convert.ToSingle(TDisT);
                }
                string TBalT = (GV_Drcomp.Rows[i].Cells[11].Text);
                if (TBalT != "")
                {
                    TBal = TBal + Convert.ToSingle(TBalT);
                }
                string FDrAmT = (GV_Drcomp.Rows[i].Cells[12].Text);
                if (FDrAmT != "")
                {
                    FDrAm = FDrAm + Convert.ToSingle(FDrAmT);
                }
            }

            lblcharges.Text = Convert.ToString( Charges);
            lbldramt.Text = Convert.ToString(DrAmt);
            lbldrdisc.Text = Convert.ToString(DrDis);
            lblcendisc.Text = Convert.ToString(CnDis);
            lbltotdisc.Text = Convert.ToString(TDis);
            lblbalance.Text = Convert.ToString(TBal);
            lblfdramt.Text = Convert.ToString(FDrAm);
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
    protected void GV_Drcomp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Drcomp.PageIndex = e.NewPageIndex;
        BindGrid();

    }
    protected void GV_Drcomp_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GV_Drcomp_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void btnlist_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btncalculate_Click(object sender, EventArgs e)
    {
        Cshmst_Bal_C CSHB = new Cshmst_Bal_C();
        CSHB.username = Session["username"].ToString();
        CSHB.P_DigModule = Convert.ToInt32(Session["DigModule"]);
        if (txtdoctor.Text != "")
        {
            string str = Convert.ToString(txtdoctor.Text);
            string[] s = str.Split('=');
            drcode = s[1];
        }
        CSHB.ReCalculate(drcode.Trim(), fromdate.Text, todate.Text, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["financialyear"].ToString()));

    }
    protected void txtPro_TextChanged(object sender, EventArgs e)
    {

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