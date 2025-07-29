using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

using CrystalDecisions.Shared;


public partial class RateLISTReport :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Userright_Bal_C ObjAT = new Userright_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["usertype"]) != "Administrator")
            {
                checkexistpageright("RateLISTReport.aspx");
            }
        }
    }
 
    [WebMethod]
    [ScriptMethod]
    public static string[] GetRateType(string prefixText, int count)
    {
        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlDataAdapter sda = new SqlDataAdapter("select CAST(RatID AS nvarchar(50))+'='+ RateName as MainRatename from RatT where RateFlag='R' and (RatID like N'%" + prefixText + "%' or RateName like N'%" + prefixText + "%' )  and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + " ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] testname = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            testname.SetValue(dr["MainRatename"], i);
            i++;
        }

        return testname;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Alter_view();
        dt = new DataTable();
        dt = ObjTB.GetDrRateList();
        if (dt.Rows.Count > 0)
        {
            ReportDocument RD = new ReportDocument();
            RD.Load(Server.MapPath("~//DiagnosticReport//Rpt_RateTypeListReport.rpt"));
            RD.SetDataSource(dt);
            RD.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,Response, true, "RateTypeList");
        }
    }
    protected void txtRateType_TextChanged(object sender, EventArgs e)
    {
        if (txtRateType.Text != "")
        {
            string[] splittedtltextname;
            splittedtltextname = txtRateType.Text.Split('=');
            ViewState["ListCode"] = splittedtltextname[0];

        }
    }
    public void Alter_view()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[DrRateList] AS (SELECT        dbo.RatT.RateName, dbo.RatT.RatID, dbo.sharemst.STCODE, dbo.sharemst.TestName, dbo.sharemst.Amount, dbo.sharemst.Percentage  " +
               " FROM dbo.sharemst INNER JOIN  " +
               " dbo.RatT ON dbo.sharemst.RateCode = dbo.RatT.RatID  ";

        if (Convert.ToString(ViewState["ListCode"]) != "")
        {
            query += " and RatT.RatID='" + ViewState["ListCode"] + "' ";
        }

        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
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