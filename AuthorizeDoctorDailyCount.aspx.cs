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

public partial class AuthorizeDoctorDailyCount : System.Web.UI.Page
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
                        checkexistpageright("AuthorizeDoctorDailyCount.aspx");
                    }
                }
                fromdate.Text = System.DateTime.Now.ToShortDateString();
                todate.Text = System.DateTime.Now.ToShortDateString();
                Alterview();
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
    protected void btnreport_Click(object sender, EventArgs e)
    {

        Alterview();
        BindGrid();
        DataTable dt = new DataTable();
        dt = ObjTB.Get_AuthorizeDoctorCount();
        if (dt.Rows.Count > 0)
        {
            ReportDocument RD = new ReportDocument();
            RD.Load(Server.MapPath("~//DiagnosticReport//Rpt_AuthorizeDocDailyCount.rpt"));
            RD.SetDataSource(dt);
            RD.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "AuthorizeDocDailyCount");
        }


    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTest(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select distinct Drsignature,signatureid from DRST where Drsignature like '%" + prefixText + "%' ", con);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] doctors = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {

            doctors.SetValue(dr["Drsignature"] + " = " + dr["signatureid"], i);
            i++;
        }
        return doctors;
    }

    public void Alterview()
    {
        string sql = "";

        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        cmd1.CommandText = "ALTER VIEW [dbo].[VW_TestwiseDailyAuthorizeCount] AS (SELECT DISTINCT  " +
                     "   TOP (99.99) PERCENT patmstd.Patauthicante, CONVERT(char(12), patmstd.TestedDate, 105) AS AuthoDate, patmstd.TestedDate, DRST.Drsignature, DRST.drsign3, " +
                     "   DRST.drsign2 , MainTest.Maintestname " +
                     "   FROM         patmstd INNER JOIN " +
                     "   DRST ON patmstd.AunticateSignatureId = DRST.signatureid INNER JOIN " +
                     "   MainTest ON patmstd.MTCode = MainTest.MTCode " +
                  " WHERE    patmstd.Patauthicante='Authorized' and  MainTest.Branchid=" + Convert.ToInt32(Session["Branchid"]) + " ";


        if (fromdate.Text != "" && todate.Text != "")
        {
            cmd1.CommandText = cmd1.CommandText + " and  (CAST(CAST(YEAR(patmstd.TestedDate) AS varchar(4)) + '/' + CAST(MONTH(patmstd.TestedDate) AS varchar(2)) + '/' + CAST(DAY(patmstd.TestedDate) AS varchar(2))  AS datetime) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') )";
        }
        if (txtdoctor.Text != "")
        {
            cmd1.CommandText = cmd1.CommandText + " and  DRST.signatureid=" + ViewState["DoctorId"] + " ";

        }
        cmd1.CommandText = cmd1.CommandText + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
    }
    public void BindGrid()
    {
        DataTable dt = new DataTable();
        dt = ObjTB.Get_AuthorizeDoctorCount();
        if (dt.Rows.Count > 0)
        {
            GVBillFHosp.DataSource = dt;
            GVBillFHosp.DataBind();
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        Alterview();
        BindGrid();
    }
    protected void GVBillFHosp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void txtdoctor_TextChanged(object sender, EventArgs e)
    {
        if (txtdoctor.Text != "")
        {
            string[] Split_TestName;
            Split_TestName = txtdoctor.Text.Split('=');
            ViewState["DoctorId"] = Split_TestName[1].Trim();

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