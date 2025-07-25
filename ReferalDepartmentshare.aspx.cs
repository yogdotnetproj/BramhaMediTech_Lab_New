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

public partial class ReferalDepartmentshare : System.Web.UI.Page
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
                        checkexistpageright("ReferalDepartmentshare.aspx");
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

        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
      
        cmd1.CommandText = "ALTER VIEW [dbo].[VW_ReferalDepartmentShare] AS (SELECT DISTINCT  " +
              "  patmst.Drname,MainTest.Maintestname, SubDepartment.ID,  " +
              "  CASE WHEN SubDepartment.ID = 1 THEN 'Pathology' ELSE SubDepartment.subdeptName END AS Subdeptname, " +
              "  CASE WHEN SubDepartment.ID = 1 THEN 'Pathology' ELSE 'Radiology' END AS Department, " +
              "  patmstd.TestRate, patmstd.Dramt " +
              "  ,count(CASE WHEN SubDepartment.ID = 1 THEN 'Pathology' ELSE SubDepartment.subdeptName END)as TestCount " +
              "  FROM         Doctor_CalculateAmount INNER JOIN " +
              "  patmst ON Doctor_CalculateAmount.PatRegID = patmst.PatRegID AND Doctor_CalculateAmount.FID = patmst.FID AND Doctor_CalculateAmount.Branchid = patmst.Branchid INNER JOIN " +
              "  Cshmst ON patmst.PatRegID = Cshmst.PatRegID AND patmst.PID = Cshmst.PID INNER JOIN " +
              "  patmstd ON patmst.PID = patmstd.PID INNER JOIN " +
              "  MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode INNER JOIN " +
              "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode " +
                  " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " AND (dbo.Cshmst.Discount <> '')"; //patmst.IsbillBH=0 and

        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
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
        cmd1.CommandText = cmd1.CommandText + " group by patmst.DrName,MainTest.Maintestname, SubDepartment.ID,SubDepartment.subdeptName,patmstd.TestRate,patmstd.Dramt ";
        cmd1.CommandText = cmd1.CommandText + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

      
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_ReferalDeptshare_Summery.rpt");
        Session["reportname"] = "ReferalDeptsharesum";
        Session["RPTFORMAT"] = "pdf";

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
        SqlDataAdapter sda = new SqlDataAdapter("select DoctorCode,rtrim(DrInitial)+' '+DoctorName as DoctorName from DrMT where DoctorName like '%" + prefixText + "%' and DrType='DR' and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + "", con);

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
       
        cmd1.CommandText = "ALTER VIEW [dbo].[VW_ReferalDepartmentShare] AS (SELECT DISTINCT  " +
              "  patmst.drname,MainTest.Maintestname, SubDepartment.ID,  " +
              "  CASE WHEN SubDepartment.ID = 1 THEN 'Pathology' ELSE SubDepartment.subdeptName END AS Subdeptname, " +
              "  CASE WHEN SubDepartment.ID = 1 THEN 'Pathology' ELSE 'Radiology' END AS Department, " +
              "  patmstd.TestRate, patmstd.Dramt " +
              "  ,count(CASE WHEN SubDepartment.ID = 1 THEN 'Pathology' ELSE SubDepartment.subdeptName END)as TestCount " +
              "  FROM         Doctor_CalculateAmount INNER JOIN " +
              "  patmst ON Doctor_CalculateAmount.PatRegID = patmst.PatRegID AND Doctor_CalculateAmount.FID = patmst.FID AND Doctor_CalculateAmount.Branchid = patmst.Branchid INNER JOIN " +
              "  Cshmst ON patmst.PatRegID = Cshmst.PatRegID AND patmst.PID = Cshmst.PID INNER JOIN " +
              "  patmstd ON patmst.PID = patmstd.PID INNER JOIN " +
              "  MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode INNER JOIN " +
              "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode " +
                  " WHERE   patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " AND (dbo.Cshmst.Discount <> '')"; //patmst.IsbillBH=0 and

        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
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
        cmd1.CommandText = cmd1.CommandText + " group by patmst.drname,MainTest.Maintestname, SubDepartment.ID,SubDepartment.subdeptName,patmstd.TestRate,patmstd.Dramt ";
        cmd1.CommandText = cmd1.CommandText + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

       

        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_ReferalDeptshare.rpt");
        Session["reportname"] = "ReferalDeptshare";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

    }
   
   
    protected void btncalculate_Click(object sender, EventArgs e)
    {
        Cshmst_Bal_C Dircash = new Cshmst_Bal_C();
        Dircash.username = Session["username"].ToString();
        Dircash.P_DigModule = Convert.ToInt32(Session["DigModule"]);
        if (txtdoctor.Text != "")
        {
            string str = Convert.ToString(txtdoctor.Text);
            string[] s = str.Split('=');
            drcode = s[1];
        }
        Dircash.ReCalculate(drcode.Trim(), fromdate.Text, todate.Text, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["financialyear"].ToString()));

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