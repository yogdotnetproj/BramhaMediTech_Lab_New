using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;

public partial class TAT :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    string Name = "", dt1 = "", rt = "";
    string turn_time = "";
    FindPatient_Bal_C sn = new FindPatient_Bal_C();
    object fromDate = null, toDate = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("TAT.aspx");
                    }
                }
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().AddDays(1).ToString("dd/MM/yyyy");

              

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

    [WebMethod]
    [ScriptMethod]
    public static string[] GetPatientName(string prefixText, int count)
    {
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );


        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;

        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("Select Rtrim(intial)+' '+Patname+' '+LastName as Name from patmst where Patname like'%" + prefixText + "%' and UnitCode= '" + labcode + "' ", con);
        }
        else
        {
            sda = new SqlDataAdapter("Select Rtrim(intial)+' '+Patname+' '+LastName as Name from patmst where Patname like'%" + prefixText + "%'", con);
        }



        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["Name"], i);
            i++;
        }

        return tests;
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] GetTestName(string prefixText, int count)
    {
        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlDataAdapter sda = new SqlDataAdapter("select Maintestname from MainTest where  MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%'   and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + " ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] testname = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            testname.SetValue(dr["Maintestname"], i);
            i++;
        }

        return testname;
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] FillDepartment(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM subdepartment where Subdeptname like  N'" + prefixText + "%'  and branchid=" + branchid + " order by Subdeptname", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM subdepartment where Subdeptname like  N'" + prefixText + "%'  and branchid=" + branchid + " order by Subdeptname", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["Subdeptname"], i);
            i++;
        }
        return tests;
    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = DataAccess.ConInitForDC();
            SqlCommand cmd1 = con.CreateCommand();

            turn_time = "ALTER VIEW [dbo].[VW_tatstvw]AS (SELECT dbo.patmst.intial,dbo.patmst.Patname,dbo.patmst.TestName,dbo.patmst.Patregdate,dbo.patmstd.PatRepDate,dbo.patmst.RefDr,dbo.patmst.Drname,dbo.patmst.PatRegID , dbo.patmst.UnitCode,patmst.Patphoneno , patmstd.PID, patmstd.MTCode as STCODE, patmstd.SDCode, patmstd.MTCode,patmstd.TestedDate,SubDepartment.subdeptName,patmstd.Updatedon FROM  patmst INNER JOIN patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.FID = patmstd.FID AND patmst.Branchid = patmstd.Branchid INNER JOIN SubDepartment ON patmstd.SDCode = SubDepartment.SDCode INNER JOIN maindepartment ON SubDepartment.ID = maindepartment.deptid  ";
            
            if (fromdate.Text.Trim() != "" && todate.Text.Trim() != "")
            {
                fromDate = fromdate.Text.Trim();
                toDate = todate.Text.Trim();
                turn_time += " and patmstd.TestedDate between '" + Convert.ToDateTime(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(toDate.ToString()).ToString("dd/MMM/yyyy") + "' ";

            }
            if (txtDepartment.Text != "")
            {
                turn_time += " and SubDepartment.subdeptName ='" + txtDepartment.Text.Trim() + "'";
            }
            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
            if (labcode != null && labcode != "")
            {
                turn_time += "and dbo.patmst.UnitCode='" + labcode + "'";
            }

            cmd1.CommandText = turn_time + ")";
            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            bind_grid();

        }
        catch (Exception exc)
        {
            Response.Cookies["error"].Value = exc.Message;
            Server.Transfer("~/ErrorMessage.aspx");
        }
    }
    public void bind_grid()
    {  
            GV_TAT.DataSource = sn.FillGridActualTatCalc();
            GV_TAT.DataBind();
        
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
      //  this.btnList_Click(null, null);

        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        //turn_time = "ALTER VIEW [dbo].[VW_tatstvw]AS (SELECT dbo.patmst.intial,dbo.patmst.Patname,dbo.patmst.TestName,dbo.patmst.Patregdate,dbo.patmstd.PatRepDate,dbo.patmst.RefDr,dbo.patmst.Drname,dbo.patmst.PatRegID , dbo.patmst.UnitCode,patmst.Patphoneno , patmstd.PID, patmstd.MTCode as STCODE, patmstd.SDCode, patmstd.MTCode,patmstd.TestedDate FROM dbo.patmst INNER JOIN dbo.patmstd ON dbo.patmst.PatRegID = dbo.patmstd.PatRegID AND dbo.patmst.FID = dbo.patmstd.FID AND dbo.patmst.branchid = dbo.patmstd.branchid ";

        //if (fromdate.Text.Trim() != "" && todate.Text.Trim() != "")
        //{
        //    fromDate = fromdate.Text.Trim();
        //    toDate = todate.Text.Trim();
        //    turn_time += " and patmstd.TestedDate between '" + Convert.ToDateTime(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(toDate.ToString()).ToString("dd/MMM/yyyy") + "' ";

        //}
        //string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        //if (labcode != null && labcode != "")
        //{
        //    turn_time += "and dbo.patmst.UnitCode='" + labcode + "'";
        //}
        turn_time = "ALTER VIEW [dbo].[VW_tatstvw]AS (SELECT dbo.patmst.intial,dbo.patmst.Patname,dbo.patmst.TestName,dbo.patmst.Patregdate,dbo.patmstd.PatRepDate,dbo.patmst.RefDr,dbo.patmst.Drname,dbo.patmst.PatRegID , dbo.patmst.UnitCode,patmst.Patphoneno , patmstd.PID, patmstd.MTCode as STCODE, patmstd.SDCode, patmstd.MTCode,patmstd.TestedDate,SubDepartment.subdeptName,patmstd.Updatedon FROM  patmst INNER JOIN patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.FID = patmstd.FID AND patmst.Branchid = patmstd.Branchid INNER JOIN SubDepartment ON patmstd.SDCode = SubDepartment.SDCode INNER JOIN maindepartment ON SubDepartment.ID = maindepartment.deptid  ";

        if (fromdate.Text.Trim() != "" && todate.Text.Trim() != "")
        {
            fromDate = fromdate.Text.Trim();
            toDate = todate.Text.Trim();
            turn_time += " and patmstd.TestedDate between '" + Convert.ToDateTime(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(toDate.ToString()).ToString("dd/MMM/yyyy") + "' ";

        }
        if (txtDepartment.Text != "")
        {
            turn_time += " and SubDepartment.subdeptName ='" + txtDepartment.Text.Trim() + "'";
        }
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            turn_time += "and dbo.patmst.UnitCode='" + labcode + "'";
        }


        cmd1.CommandText = turn_time + ")";
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_TATCalcReport.rpt");
        Session["reportname"] = "TATCalcReport";
        Session["RPTFORMAT"] = "pdf";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);

        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
    }
    public string Datefunction(string dt1, string rt1)
    {
        string DateDiff = "";
        try
        {
            //Patregdate
            string[] daydt = (dt1.Split('/'));
            string day = daydt[0];
            string month = daydt[1];
            string year = daydt[2];
            string[] yrdt = (year.Split(' '));
            string yr = yrdt[0];
            string time = yrdt[1];
            string[] timemain = time.Split(':');
            string hr = timemain[0];
            string mi = timemain[1];
            string se = timemain[2];
            int d = Convert.ToInt32(day);
            int m = Convert.ToInt32(month);
            int y = Convert.ToInt32(yr);

            if (rt1 == "")
            {
                DateDiff = "Report Date is not Available";
            }
            else
            {
                string[] daydt1 = (rt1.Split('/'));
                string day1 = daydt1[0];
                string month1 = daydt1[1];
                string year1 = daydt1[2];
                string[] yrdt1 = (year1.Split(' '));
                string yr1 = yrdt1[0];
                string time1 = yrdt1[1];
                string[] timemain1 = time1.Split(':');
                string hr1 = timemain1[0];
                string mi1 = timemain1[1];
                string se1 = timemain1[2];
                int d1 = Convert.ToInt32(day1);
                int m1 = Convert.ToInt32(month1);
                int y1 = Convert.ToInt32(yr1);

                int z = (d1 - d);
                int x = (m1 - m);
                int p = (y1 - y);

                DateTime dt12 = Convert.ToDateTime(dt1);
                DateTime rt12 = Convert.ToDateTime(rt1);
                TimeSpan sp = rt12 - dt12;
                string days12 = sp.Days.ToString();
                string h123 = sp.Hours.ToString();

                string mint = sp.Minutes.ToString();
                string second = sp.Seconds.ToString();

                string timediff1 = Convert.ToString(h123 + ":" + mint + ":" + second);

                string DateDiff1 = Convert.ToString(z + " Days/" + x + " Month/" + p + " Year");

                DateDiff = Convert.ToString(DateDiff1 + " " + timediff1);


            }
        }
        catch (Exception exe)
        {
            //throw;
        }
        return DateDiff;
    }
    protected void GV_TAT_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_TAT.PageIndex = e.NewPageIndex;
        bind_grid();

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

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        //turn_time = "ALTER VIEW [dbo].[VW_tatstvw]AS (SELECT dbo.patmst.intial,dbo.patmst.Patname,dbo.patmst.TestName,dbo.patmst.Patregdate,dbo.patmstd.PatRepDate,dbo.patmst.RefDr,dbo.patmst.Drname,dbo.patmst.PatRegID , dbo.patmst.UnitCode,patmst.Patphoneno , patmstd.PID, patmstd.MTCode as STCODE, patmstd.SDCode, patmstd.MTCode,patmstd.TestedDate FROM dbo.patmst INNER JOIN dbo.patmstd ON dbo.patmst.PatRegID = dbo.patmstd.PatRegID AND dbo.patmst.FID = dbo.patmstd.FID AND dbo.patmst.branchid = dbo.patmstd.branchid ";

        //if (fromdate.Text.Trim() != "" && todate.Text.Trim() != "")
        //{
        //    fromDate = fromdate.Text.Trim();
        //    toDate = todate.Text.Trim();
        //    turn_time += " and patmstd.TestedDate between '" + Convert.ToDateTime(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(toDate.ToString()).ToString("dd/MMM/yyyy") + "' ";

        //}
        //string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        //if (labcode != null && labcode != "")
        //{
        //    turn_time += "and dbo.patmst.UnitCode='" + labcode + "'";
        //}
        turn_time = "ALTER VIEW [dbo].[VW_tatstvw]AS (SELECT dbo.patmst.intial,dbo.patmst.Patname,dbo.patmst.TestName,dbo.patmst.Patregdate,dbo.patmstd.PatRepDate,dbo.patmst.RefDr,dbo.patmst.Drname,dbo.patmst.PatRegID , dbo.patmst.UnitCode,patmst.Patphoneno , patmstd.PID, patmstd.MTCode as STCODE, patmstd.SDCode, patmstd.MTCode,patmstd.TestedDate,SubDepartment.subdeptName,patmstd.Updatedon FROM  patmst INNER JOIN patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.FID = patmstd.FID AND patmst.Branchid = patmstd.Branchid INNER JOIN SubDepartment ON patmstd.SDCode = SubDepartment.SDCode INNER JOIN maindepartment ON SubDepartment.ID = maindepartment.deptid  ";

        if (fromdate.Text.Trim() != "" && todate.Text.Trim() != "")
        {
            fromDate = fromdate.Text.Trim();
            toDate = todate.Text.Trim();
            turn_time += " and patmstd.TestedDate between '" + Convert.ToDateTime(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(toDate.ToString()).ToString("dd/MMM/yyyy") + "' ";

        }
        if (txtDepartment.Text != "")
        {
            turn_time += " and SubDepartment.subdeptName ='" + txtDepartment.Text.Trim() + "'";
        }
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            turn_time += "and dbo.patmst.UnitCode='" + labcode + "'";
        }

        cmd1.CommandText = turn_time + ")";
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close();
        con.Dispose();
        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_TATCalcReport.rpt");
        Session["reportname"] = "TATCalcReport";
        Session["RPTFORMAT"] = "EXCEL";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);

        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
    }
}