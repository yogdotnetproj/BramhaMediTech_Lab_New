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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class TestWiseDoctorPerformReport :BasePage
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
                        checkexistpageright("TestWiseDoctorPerformReport.aspx");
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
        //if (txtPerformDr.Text == "")
        //{
        //  //  lblmsg.Text = "Select Dr Name";
        //}
        //else
        //{
            lblmsg.Text = "";
            SqlConnection con = DataAccess.ConInitForDC();
            SqlCommand cmd1 = con.CreateCommand();

            //cmd1.CommandText = "ALTER VIEW [dbo].[VW_TestwiseDailyCount] AS (SELECT   distinct top 99.99 percent  convert(char(12),patmstd.Patregdate,105)as EntryDate,patmstd.Patregdate, "+
            //           " case when [PackMst ].PackageName IS NULL then  MainTest.Maintestname  else  [PackMst ].PackageName end TestName "+

            //           " FROM         patmstd INNER JOIN "+
            //           " MainTest ON patmstd.MTCode = MainTest.MTCode LEFT OUTER JOIN "+
            //           " [PackMst ] ON patmstd.PackageCode = [PackMst ].PackageCode " +
            //          " WHERE   patmstd.Branchid=" + Convert.ToInt32(Session["Branchid"]) + " ";

            cmd1.CommandText = "ALTER VIEW [dbo].[VW_TestwiseDailyCount] AS (SELECT DISTINCT  " +
                       " TOP (99.99) PERCENT patmstd.PatRegID, patmstd.Patregdate as Patregdate, SubDepartment.subdeptName, MainTest.Maintestname, patmstd.PackageCode, patmstd.TestRate,  " +
                       " patmstd.AunticateSignatureId AS DoctorCode ,patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.MDY, patmst.Patphoneno, " +
                       " patmst.CenterCode, patmst.Drname, patmst.Username ,patmstd.MTCode" +
                       " FROM         patmstd INNER JOIN " +
                       " SubDepartment ON patmstd.SDCode = SubDepartment.SDCode INNER JOIN " +
                       " MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN " +
                       " patmst ON patmstd.PatRegID = patmst.PatRegID AND patmstd.PID = patmst.PID " +
                      " WHERE   patmstd.Patauthicante='Authorized' and  patmstd.Branchid=" + Convert.ToInt32(Session["Branchid"]) + " and (dbo.patmstd.PackageCode = N'') ";
            if (fromdate.Text != "" && todate.Text != "")
            {
                cmd1.CommandText = cmd1.CommandText + " and  (CAST(CAST(YEAR(patmstd.Patregdate) AS varchar(4)) + '/' + CAST(MONTH(patmstd.Patregdate) AS varchar(2)) + '/' + CAST(DAY(patmstd.Patregdate) AS varchar(2))  AS datetime) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') )";
            }
            cmd1.CommandText = cmd1.CommandText + ")";

            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close(); con.Dispose();

            SqlConnection con5 = DataAccess.ConInitForDC();
            SqlCommand cmd15 = con5.CreateCommand();


            cmd15.CommandText = "ALTER VIEW [dbo].[VW_GetPackageCount] AS (SELECT DISTINCT " +
                     "   dbo.patmstd.PatRegID, CONVERT(char(12), dbo.patmstd.Patregdate, 105) AS Entrydate, dbo.patmstd.Branchid, dbo.PackmstD.PackageName, " +
                     "   dbo.patmstd.PackageCode,patmstd.TestRate,patmstd.AunticateSignatureId as DoctorCode " +
                     " ,patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.MDY, patmst.Patphoneno, " +
                     "   patmst.CenterCode, patmst.Drname, patmst.Username " +
                     "   FROM         patmstd INNER JOIN " +
                     "   PackmstD ON patmstd.PackageCode = PackmstD.PackageCode INNER JOIN " +
                      "  patmst ON patmstd.PatRegID = patmst.PatRegID AND patmstd.PID = patmst.PID " +
                      " WHERE   patmstd.Branchid=" + Convert.ToInt32(Session["Branchid"]) + " and (dbo.patmstd.PackageCode <> '') ";
            if (fromdate.Text != "" && todate.Text != "")
            {
                cmd15.CommandText = cmd15.CommandText + " and  (CAST(CAST(YEAR(patmstd.Patregdate) AS varchar(4)) + '/' + CAST(MONTH(patmstd.Patregdate) AS varchar(2)) + '/' + CAST(DAY(patmstd.Patregdate) AS varchar(2))  AS datetime) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') )";
            }
            cmd15.CommandText = cmd15.CommandText + ")";

            con5.Open();
            cmd15.ExecuteNonQuery();
            con5.Close(); con5.Dispose();

            DataTable dt = new DataTable();
            dt = ObjTB.Get_TestwiseTotalDr_Perform_Amount(txtdoctor.Text, Convert.ToInt32(RblTestCount.SelectedValue), txtPerformDr.Text);
            if (dt.Rows.Count > 0)
            {
                ReportDocument RD = new ReportDocument();
                RD.Load(Server.MapPath("~//DiagnosticReport//Rpt_TestwiseDailyDrPerformReport.rpt"));
                RD.SetDataSource(dt);
                RD.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "DrPerformReport");
            }

       // }
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTest(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select distinct TestName from VW_GetAllPAckage_Test where TestName like '%" + prefixText + "%' ", con);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] doctors = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {

            doctors.SetValue(dr["TestName"], i);
            i++;
        }
        return doctors;
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] GetDr(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select distinct Name from CTuser where Usertype='Main Doctor' and Name like '%" + prefixText + "%' ", con);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] doctors = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {

            doctors.SetValue(dr["Name"], i);
            i++;
        }
        return doctors;
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