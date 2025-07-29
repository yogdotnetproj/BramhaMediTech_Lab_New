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

public partial class DrReport_Summary :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string drcode = "";
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
                        checkexistpageright("DrReportSummary.aspx");
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
        //cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT DISTINCT "+
        //                  "  dbo.FUN_GetCompAmt(patmst.Branchid, patmst.PID, CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END) AS CompAmt, dbo.FUN_Getpcountcomp(patmst.Branchid, patmst.PID) "+
        //                  "  AS TAmt, CASE WHEN Patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) > 0 THEN 0 ELSE patmstd.dramt END AS Expr1, patmst.intial + ' ' + Doctor_CalculateAmount.PatName AS Fname, patmst.Drname, "+
        //                  "  '' AS Tests, patmst.RegistratonDateTime, Doctor_CalculateAmount.TotalAmount, patmst.Branchid, SUM(RecM.DisAmt) AS Discount, 0 AS DisFlag, RecM.OtherCharges, patmstd.TestRate AS TestCharges, patmst.PID,  "+
        //                  "  patmst.PatRegID, patmst.CenterName, patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) AS Balance, CASE WHEN [PackMst].PackageName IS NULL  "+
        //                  "  THEN MainTest.Maintestname ELSE [PackMst].PackageName END AS testname, patmst.UnitCode, patmst.DoctorCode, SUM(RecM.LabGiven) AS LabGiven, SUM(RecM.DrGiven) AS DrGiven,  "+
        //                  "  CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END AS TestCCode, CTuser.Name "+
        //                  "  FROM            CTuser RIGHT OUTER JOIN "+
        //                  "  DrMT INNER JOIN "+
        //                  "  Doctor_CalculateAmount INNER JOIN "+
        //                  "  patmst ON Doctor_CalculateAmount.PatRegID = patmst.PatRegID AND Doctor_CalculateAmount.FID = patmst.FID AND Doctor_CalculateAmount.Branchid = patmst.Branchid INNER JOIN "+
        //                  "  RecM ON  patmst.PID = RecM.PID INNER JOIN "+
        //                  "  patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN "+
        //                  "  MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode ON DrMT.DoctorCode = patmst.DoctorCode LEFT OUTER JOIN "+
        //                  "  PackMst ON patmstd.PackageCode = PackMst.PackageCode AND patmstd.Branchid = PackMst.branchid ON CTuser.CUId = DrMT.PRO " +
        //         " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " "; //   AND (dbo.Cshmst.Discount <> '')patmst.IsbillBH=0 and

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

        // cmd1.CommandText = cmd1.CommandText + "  GROUP BY patmstd.Dramt, Doctor_CalculateAmount.DoctorAmount, patmst.intial, Doctor_CalculateAmount.PatName, patmst.Drname, patmst.RegistratonDateTime, Doctor_CalculateAmount.TotalAmount, patmst.Branchid,  "+
        //  "  RecM.Othercharges, patmstd.TestRate, patmst.PID, patmst.PatRegID, patmst.CenterName,  patmst.UnitCode, patmst.DoctorCode, MainTest.Maintestname, PackMst.PackageName, "+
        //  "  CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END, CTuser.Name,patmst.TestCharges ";
        //cmd1.CommandText = cmd1.CommandText + ")";

      

        //con.Open();
        //cmd1.ExecuteNonQuery();
        //con.Close(); con.Dispose();

        cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT     dbo.Doctor_CalculateAmount.DoctorAmount AS Expr1, patmst.intial+' '+dbo.Doctor_CalculateAmount.Patname as fname, dbo.patmst.Drname, convert(nvarchar (4000), dbo.patmst.Tests)as Tests, " +
           "   dbo.patmst.RegistratonDateTime, dbo.Doctor_CalculateAmount.TotalAmount, dbo.patmst.branchid,  SUM(CAST(dbo.RecM.DisAmt AS numeric)) AS Discount,  0 as DisFlag, " +
           "   sum(dbo.RecM.Othercharges) as Othercharges, dbo.patmst.TestCharges, dbo.patmst.PID, dbo.patmst.PatRegID,  dbo.patmst.CenterName, " +
           "   patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) AS Balance, " +
           "   dbo.patmst.TestName , " +
           "   dbo.patmst.UnitCode,dbo.patmst.DoctorCode, SUM(RecM.LabGiven) AS LabGiven, SUM(RecM.DrGiven) AS DrGiven,  0 as CompAmt,0 as TAmt,''as TestCCode,''as Name , patmst.IsbillBH   " +
           "   FROM dbo.Doctor_CalculateAmount INNER JOIN    dbo.patmst ON dbo.Doctor_CalculateAmount.PatRegID = dbo.patmst.PatRegID AND " +
           "   dbo.Doctor_CalculateAmount.FID = dbo.patmst.FID AND  dbo.Doctor_CalculateAmount.branchid = dbo.patmst.branchid INNER JOIN " +
           "   dbo.RecM ON  dbo.patmst.PID = dbo.RecM.PID " +
               " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " "; //  AND (dbo.Cshmst.Discount <> '') patmst.IsbillBH=0 and

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

            cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode=N'" + drcode.Trim() + "'";
        }

        cmd1.CommandText = cmd1.CommandText + " group by dbo.Doctor_CalculateAmount.DoctorAmount , patmst.intial+' '+dbo.Doctor_CalculateAmount.Patname , dbo.patmst.Drname, convert(nvarchar (4000), dbo.patmst.Tests),  " +
                  "  dbo.patmst.RegistratonDateTime, dbo.Doctor_CalculateAmount.TotalAmount, dbo.patmst.branchid,   " +
                  "  dbo.patmst.TestCharges, dbo.patmst.PID, dbo.patmst.PatRegID,  dbo.patmst.CenterName,  " +
                  "  dbo.patmst.TestName ,dbo.patmst.UnitCode,dbo.patmst.DoctorCode, patmst.IsbillBH ";
        cmd1.CommandText = cmd1.CommandText + ")";



        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        ReportParameterClass.ReportType = "ComplimentReport";
        sql = "{VW_drspmain.RegistratonDateTime} In DateTime('" + Convert.ToDateTime(fromdate.Text).AddDays(-1).ToString("dd/MM/yyyy") + "') To DateTime ('" + Convert.ToDateTime(todate.Text).AddDays(+1).ToString("dd/MM/yyyy") + "') and {VW_drspmain.branchid}=" + Convert.ToInt32(Session["Branchid"]) + "";
        if (txtdoctor.Text != "")


            sql += " and {VW_drspmain.DoctorCode}='" + drcode.Trim() + "'";

        ReportParameterClass.SelectionFormula = sql;
        // }
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_docshare_Sum_Rept.rpt");
        Session["reportname"] = "Compliment_Report_Sum_Rep";
        Session["RPTFORMAT"] = "EXCEL";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);


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
        //cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT DISTINCT "+
        //          "  dbo.FUN_GetCompAmt(patmst.Branchid, patmst.PID, CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END) AS CompAmt, dbo.FUN_Getpcountcomp(patmst.Branchid, patmst.PID) "+
        //          "  AS TAmt, CASE WHEN Patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) > 0 THEN 0 ELSE patmstd.dramt END AS Expr1, patmst.intial + ' ' + Doctor_CalculateAmount.PatName AS Fname, patmst.Drname,  "+
        //          "  '' AS Tests, patmst.RegistratonDateTime, Doctor_CalculateAmount.TotalAmount, patmst.Branchid, SUM(RecM.DisAmt) AS Discount, 0 AS DisFlag, RecM.OtherCharges, patmstd.TestRate AS TestCharges, patmst.PID,  "+
        //          "  patmst.PatRegID, patmst.CenterName, patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) AS Balance, CASE WHEN [PackMst].PackageName IS NULL  "+
        //          "  THEN MainTest.Maintestname ELSE [PackMst].PackageName END AS testname, patmst.UnitCode, patmst.DoctorCode, SUM(RecM.LabGiven) AS LabGiven, SUM(RecM.DrGiven) AS DrGiven,  "+
        //          "  CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END AS TestCCode, CTuser.Name "+
        //          "  FROM            CTuser RIGHT OUTER JOIN "+
        //          "  DrMT INNER JOIN "+
        //          "  Doctor_CalculateAmount INNER JOIN "+
        //          "  patmst ON Doctor_CalculateAmount.PatRegID = patmst.PatRegID AND Doctor_CalculateAmount.FID = patmst.FID AND Doctor_CalculateAmount.Branchid = patmst.Branchid INNER JOIN "+
        //          "  RecM ON  patmst.PID = RecM.PID INNER JOIN "+
        //          "  patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN "+
        //          "  MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode ON DrMT.DoctorCode = patmst.DoctorCode LEFT OUTER JOIN "+
        //          "  PackMst ON patmstd.PackageCode = PackMst.PackageCode AND patmstd.Branchid = PackMst.branchid ON CTuser.CUId = DrMT.PRO " +
        //          " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " "; //  AND (dbo.Cshmst.Discount <> '') patmst.IsbillBH=0 and

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

        //    cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode=N'" + drcode.Trim() + "'";
        //}

        // cmd1.CommandText = cmd1.CommandText + " GROUP BY patmstd.Dramt, Doctor_CalculateAmount.DoctorAmount, patmst.intial, Doctor_CalculateAmount.PatName, patmst.Drname, patmst.RegistratonDateTime, Doctor_CalculateAmount.TotalAmount, patmst.Branchid,  "+
        //                  "  RecM.Othercharges, patmstd.TestRate, patmst.PID, patmst.PatRegID, patmst.CenterName,  patmst.UnitCode, patmst.DoctorCode, MainTest.Maintestname, PackMst.PackageName, "+
        //                  "  CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END, CTuser.Name,patmst.TestCharges " ;
        //cmd1.CommandText = cmd1.CommandText + ")";



        //con.Open();
        //cmd1.ExecuteNonQuery();
        //con.Close(); con.Dispose();

        cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT     dbo.Doctor_CalculateAmount.DoctorAmount AS Expr1, patmst.intial+' '+dbo.Doctor_CalculateAmount.Patname as fname, dbo.patmst.Drname, convert(nvarchar (4000), dbo.patmst.Tests)as Tests, "+
             "   dbo.patmst.RegistratonDateTime, dbo.Doctor_CalculateAmount.TotalAmount, dbo.patmst.branchid,  SUM(CAST(dbo.RecM.DisAmt AS numeric)) AS Discount,  0 as DisFlag, "+
             "   sum(dbo.RecM.Othercharges) as Othercharges, dbo.patmst.TestCharges, dbo.patmst.PID, dbo.patmst.PatRegID,  dbo.patmst.CenterName, "+
             "   patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) AS Balance, "+
             "   dbo.patmst.TestName , "+
             "   dbo.patmst.UnitCode,dbo.patmst.DoctorCode, SUM(RecM.LabGiven) AS LabGiven, SUM(RecM.DrGiven) AS DrGiven,  0 as CompAmt,0 as TAmt,''as TestCCode,''as Name, patmst.IsbillBH  " +
             "   FROM dbo.Doctor_CalculateAmount INNER JOIN    dbo.patmst ON dbo.Doctor_CalculateAmount.PatRegID = dbo.patmst.PatRegID AND "+
             "   dbo.Doctor_CalculateAmount.FID = dbo.patmst.FID AND  dbo.Doctor_CalculateAmount.branchid = dbo.patmst.branchid INNER JOIN "+
             "   dbo.RecM ON  dbo.patmst.PID = dbo.RecM.PID " +
                 " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " "; //  AND (dbo.Cshmst.Discount <> '') patmst.IsbillBH=0 and

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

            cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode=N'" + drcode.Trim() + "'";
        }

        cmd1.CommandText = cmd1.CommandText + " group by dbo.Doctor_CalculateAmount.DoctorAmount , patmst.intial+' '+dbo.Doctor_CalculateAmount.Patname , dbo.patmst.Drname, convert(nvarchar (4000), dbo.patmst.Tests),  " +
                  "  dbo.patmst.RegistratonDateTime, dbo.Doctor_CalculateAmount.TotalAmount, dbo.patmst.branchid,   "+
                  "  dbo.patmst.TestCharges, dbo.patmst.PID, dbo.patmst.PatRegID,  dbo.patmst.CenterName,  "+
                  "  dbo.patmst.TestName ,dbo.patmst.UnitCode,dbo.patmst.DoctorCode,patmst.IsbillBH ";
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
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_docshare_Sum_Rept.rpt");
        Session["reportname"] = "Compliment_Report_Sum_Rep";
        Session["RPTFORMAT"] = "pdf";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);

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
            cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain] AS (SELECT DISTINCT "+
                       " dbo.FUN_GetCompAmt(patmst.Branchid, patmst.PID, CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END) AS CompAmt, dbo.FUN_Getpcountcomp(patmst.Branchid, patmst.PID) "+
                       " AS TAmt, CASE WHEN Patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) > 0 THEN 0 ELSE patmstd.dramt END AS Expr1, patmst.intial + ' ' + Doctor_CalculateAmount.PatName AS Fname, patmst.Drname, "+
                       " '' AS Tests, patmst.RegistratonDateTime, Doctor_CalculateAmount.TotalAmount, patmst.Branchid, SUM(RecM.DisAmt) AS Discount, 0 AS DisFlag, RecM.OtherCharges, patmstd.TestRate AS TestCharges, patmst.PID, "+
                       " patmst.PatRegID, patmst.CenterName, patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) AS Balance, CASE WHEN [PackMst].PackageName IS NULL "+
                       " THEN MainTest.Maintestname ELSE [PackMst].PackageName END AS testname, patmst.UnitCode, patmst.DoctorCode, SUM(RecM.LabGiven) AS LabGiven, SUM(RecM.DrGiven) AS DrGiven, "+
                       " CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END AS TestCCode, CTuser.Name,patmst.IsbillBH " +
                       " FROM            CTuser RIGHT OUTER JOIN "+
                       " DrMT INNER JOIN "+
                       " Doctor_CalculateAmount INNER JOIN "+
                       " patmst ON Doctor_CalculateAmount.PatRegID = patmst.PatRegID AND Doctor_CalculateAmount.FID = patmst.FID AND Doctor_CalculateAmount.Branchid = patmst.Branchid INNER JOIN "+
                       " RecM ON  patmst.PID = RecM.PID INNER JOIN "+
                       " patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN "+
                       " MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode ON DrMT.DoctorCode = patmst.DoctorCode LEFT OUTER JOIN "+
                       " PackMst ON patmstd.PackageCode = PackMst.PackageCode AND patmstd.Branchid = PackMst.branchid ON CTuser.CUId = DrMT.PRO " +
                      " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " "; // AND (dbo.Cshmst.Discount <> '') patmst.IsbillBH=0 and

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

                cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode=N'" + drcode.Trim() + "'";
            }

            cmd1.CommandText = cmd1.CommandText + " GROUP BY patmstd.Dramt, Doctor_CalculateAmount.DoctorAmount, patmst.intial, Doctor_CalculateAmount.PatName, patmst.Drname, patmst.RegistratonDateTime, Doctor_CalculateAmount.TotalAmount, patmst.Branchid, "+ 
                              "  RecM.Othercharges, patmstd.TestRate, patmst.PID, patmst.PatRegID, patmst.CenterName,  patmst.UnitCode, patmst.DoctorCode, MainTest.Maintestname, PackMst.PackageName,  "+
                              "  CASE WHEN patmstd.PackageCode = '' THEN patmstd.MTCode ELSE patmstd.PackageCode END, CTuser.Name,patmst.TestCharges,patmst.IsbillBH ";
            cmd1.CommandText = cmd1.CommandText + ")";

   

            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close(); con.Dispose();

            GV_Drcomp.DataSource = Cshmst_supp_Bal_C.GetDoctorcomplimentData();
            GV_Drcomp.DataBind();
            float sum = 0.0f;

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