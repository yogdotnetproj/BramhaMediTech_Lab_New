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

public partial class WorksheetDetails : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
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
                        checkexistpageright("WorksheetDetails.aspx");
                    }
                }
                bindchecklist();
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                
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


    public void bindchecklist()
    {
        chksubdept.DataSource = ObjTB.Get_subdepartment();

        chksubdept.DataTextField = "subdeptName";
        chksubdept.DataValueField = "subdeptid";
        chksubdept.DataBind();
    }
    protected void btndailytrans_Click(object sender, EventArgs e)
    {
        string Deptid = "0";
        int i = 0;
        foreach (ListItem li in chksubdept.Items)
        {

            if (li.Selected)
            {
                
                string ss = li.Text;
                if (i > 0)
                {
                    Deptid =Deptid+ "," + Convert.ToString(li.Value); ;
                }
                else
                {
                    Deptid = Convert.ToString(li.Value);
                }
                i++;
            }

        }
        #region Myregion3
        SqlConnection conn21 = DataAccess.ConInitForDC();
        SqlCommand sc21 = conn21.CreateCommand();
        sc21.CommandText = "ALTER VIEW [dbo].[VW_Worklist] AS SELECT DISTINCT  TOP (99.99) PERCENT " +
                  "  patmst.PID, patmst.FID, patmst.Patregdate, patmst.intial, patmst.Patname, patmst.Age, patmst.MDY, patmst.sex, patmst.Drname, patmstd.TestRate, " +
                 "   patmstd.BarcodeID, "+
                 "   case when SubTest.MTCode IS NULL  then Maintest.MTCode  else SubTest.MTCode end AS MTCode, "+
                 "   MainTest.Maintestid, MainTest.SDCode, SubDepartment.subdeptName, MainTest.Shortcode, "+
                 "   MainTest.Sampletype,  "+
                 "   case when  SubTest.TestName IS NULL  then  MainTest.Sampletype  else SubTest.TestName end AS TestName, "+
                 "   SubTest.Testordno, SubDepartment.SDOrderNo, MainTest.Testordno AS Expr1, patmst.PatRegID, patmstd.Labno, SubDepartment.ID,patmst.LabRegMediPro, patmst.CenterCode, patmstd.PackageCode " +
                 "  , isnull( patmstd.SpecimanNo,0)as SpecimanNo  FROM         SubTest RIGHT OUTER JOIN " +
                  "  SubDepartment INNER JOIN "+
                  "  MainTest INNER JOIN "+
                  "  patmst INNER JOIN "+
                  "  patmstd ON patmst.PID = patmstd.PID ON MainTest.MTCode = patmstd.MTCode AND MainTest.SDCode = patmstd.SDCode ON "+
                  "  SubDepartment.SDCode = MainTest.SDCode ON SubTest.MTCode = MainTest.MTCode " +
                      " Where 1=1 ";
        if (fromdate.Text != "")
        {
            sc21.CommandText += " and (CAST(CAST(YEAR(patmst.Patregdate) AS varchar(4)) + '/' + CAST(MONTH(patmst.Patregdate) AS varchar(2)) + '/' + CAST(DAY(patmst.Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
        }
        if (txtfromregno.Text != "" && txttoregno.Text != "")
        {
             sc21.CommandText += " and (patmst.PatRegID >= "+txtfromregno.Text+" and patmst.PatRegID <= "+txttoregno.Text+") ";
        }
        if (Deptid != "0")
        {
            sc21.CommandText += " and SubDepartment.subdeptid in ( " + Deptid + ") ";
        }
        sc21.CommandText += " order by patmst.Patregdate desc ";

        conn21.Open();
        try
        {
            sc21.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn21.Close(); conn21.Dispose();
        #endregion

        string sql = "";
        ReportParameterClass.ReportType = "Worklist";


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_DailyWorklist.rpt");
        Session["reportname"] = "DailyWorklist";
        Session["RPTFORMAT"] = "pdf";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
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