using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public partial class Dept_salereport : System.Web.UI.Page
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
                        checkexistpageright("Dept_salereport.aspx");
                    }
                }
                filldept();                
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().AddDays(1).ToString("dd/MM/yyyy");
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
   

    protected void btnlist_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void CashGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void CashGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void CashGrid_Sorting(object sender, GridViewSortEventArgs e)
    {       
        BindGrid();
    }

    void BindGrid()
    {
        try
        {
            object fromDate = null;
            object Todate = null;
            string ddldeptnam = "0";
            if (fromdate.Text != "")
            {
                fromDate = DateTimeConvesion.getDateFromString(fromdate.Text.Trim()).ToString();
            }
            if (todate.Text != "")
            {
                Todate = DateTimeConvesion.getDateFromString(todate.Text.Trim()).ToString();
            }
            if (ddldeptname.SelectedIndex != 0)
            {
                ddldeptnam = ddldeptname.SelectedValue;
            }

            SqlConnection con = DataAccess.ConInitForDC();
            SqlCommand cmd1 = con.CreateCommand();

            string query = "ALTER VIEW [dbo].[VW_DepartmentTestcount] AS (SELECT        FID,count( SDCode) as DepCount, SDCode ,CONVERT(char(12), Patregdate, 105) AS Patregdate, sum(TestRate) AS TestRate FROM patmstd " +
                    "   where    patmstd.isactive=1 and (CAST(CAST(YEAR( patmstd.Patregdate) AS varchar(4)) + '/' + CAST(MONTH( patmstd.Patregdate) AS varchar(2)) + '/' + CAST(DAY( patmstd.Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') and patmstd.branchid=" + Convert.ToInt32(Session["Branchid"]) + " ";


            query += " GROUP BY FID,  CONVERT(char(12), Patregdate, 105), Branchid,SDCode  ";



            cmd1.CommandText = query + ")";

            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close(); con.Dispose();

            GV_SaleDept.DataSource = DeptsaleandIncome_Bal_C.GetDeptsalereport(Todate, fromDate, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ddldeptnam));
            GV_SaleDept.DataBind();
            
            float sum = 0.0f;
            for (int i = 0; i < GV_SaleDept.Rows.Count; i++)
            {
                sum += Convert.ToSingle(GV_SaleDept.Rows[i].Cells[2].Text);
            }
           // lbltotal.Text = sum.ToString();
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

    protected void GV_SaleDept_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

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

    protected void GV_SaleDept_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
        //if (e.Row.Cells[2].Text != "")
        //{
        //    string date = e.Row.Cells[0].Text;
        //    date = Convert.ToDateTime(date).ToShortDateString();
        //    e.Row.Cells[0].Text = date;
        //}
    }

    protected void GV_SaleDept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_SaleDept.PageIndex = e.NewPageIndex;
        BindGrid();
    }


    private void filldept()
    {
        try
        {
            ddldeptname.DataSource = DeptsaleandIncome_Bal_C.Get_Allsubdept(Convert.ToInt32(Session["Branchid"]));
            ddldeptname.DataTextField = "subdeptname";
            ddldeptname.DataValueField = "subdeptid";
            ddldeptname.DataBind();
            ddldeptname.Items.Insert(0, "Select Dept");
            ddldeptname.SelectedIndex = -1;
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


    protected void btnreport1_Click(object sender, EventArgs e)
    {


        SqlConnection con1 = DataAccess.ConInitForDC();
        SqlCommand cmd11 = con1.CreateCommand();

        string query1 = "ALTER VIEW [dbo].[VW_DepartmentTestcount] AS (SELECT        FID,count( SDCode) as DepCount, SDCode ,CONVERT(char(12), Patregdate, 105) AS Patregdate, sum(TestRate) AS TestRate FROM patmstd " +
                "   where    patmstd.isactive=1 and (CAST(CAST(YEAR( patmstd.Patregdate) AS varchar(4)) + '/' + CAST(MONTH( patmstd.Patregdate) AS varchar(2)) + '/' + CAST(DAY( patmstd.Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') and patmstd.branchid=" + Convert.ToInt32(Session["Branchid"]) + " ";


        query1 += " GROUP BY FID,  CONVERT(char(12), Patregdate, 105), Branchid,SDCode  ";



        cmd11.CommandText = query1 + ")";

        con1.Open();
        cmd11.ExecuteNonQuery();
        con1.Close(); con1.Dispose();

        string sql = "";

        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"] );
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        //string query = "ALTER VIEW [dbo].[VW_dpsalIR] AS (SELECT      COUNT(patmstd.SDCode) as FID, patmstd.SDCode,  " +
        //        "  convert(char(12),patmstd.Patregdate,105) as DateOfEntry  ,  SUM(patmstd.TestRate) AS TestRate, SubDepartment.subdeptName  " +
        //        "  FROM         patmstd INNER JOIN   SubDepartment ON patmstd.SDCode = SubDepartment.SDCode AND " +
        //        "  patmstd.Branchid = SubDepartment.Branchid " +
        //        "   where    (CAST(CAST(YEAR( patmstd.Patregdate) AS varchar(4)) + '/' + CAST(MONTH( patmstd.Patregdate) AS varchar(2)) + '/' + CAST(DAY( patmstd.Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') and patmstd.branchid=" + Convert.ToInt32(Session["Branchid"]) + " ";

        //if (ddldeptname.SelectedIndex != 0)
        //{
        //    int ddldept = Convert.ToInt32(ddldeptname.SelectedValue);
        //    query += " and SubDepartment.subdeptid=" + ddldept + "";
        //}
        //query += "   GROUP BY patmstd.FID, patmstd.SDCode,     convert(char(12),patmstd.Patregdate,105) , SubDepartment.subdeptName,    patmstd.Branchid  ";
        string query = "ALTER VIEW [dbo].[VW_dpsalIR] AS (SELECT DISTINCT  " +
                  "  SubDepartment.subdeptName, SubDepartment.SDCode, SubDepartment.SDOrderNo, SubDepartment.DeptID, convert(char(12),VW_DepartmentTestcount.Patregdate,105) as DateOfEntry ,  " +
                  "  ISNULL(VW_DepartmentTestcount.DepCount, 0) AS FID,  " +
                  "  ISNULL(VW_DepartmentTestcount.TestRate, 0) AS TestRate,  SubDepartment.subdeptid " +
                  "  FROM            SubDepartment LEFT OUTER JOIN " +
                  "  VW_DepartmentTestcount ON SubDepartment.SDCode = VW_DepartmentTestcount.SDCode ";
           
        if (ddldeptname.SelectedIndex != 0)
        {
            int ddldept = Convert.ToInt32(ddldeptname.SelectedValue);
            query += " and SubDepartment.subdeptid=" + ddldept + "";
        }
     //   query += "   GROUP BY patmstd.FID, patmstd.SDCode,     convert(char(12),patmstd.Patregdate,105) , SubDepartment.subdeptName,    patmstd.Branchid  ";





        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_groupwisesale_I.rpt");
        Session["reportname"] = "Deptwisesalereport_IR";
        Session["RPTFORMAT"] = "pdf";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
        //Server.Transfer("~/CrystalReportViewerForm.aspx");

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