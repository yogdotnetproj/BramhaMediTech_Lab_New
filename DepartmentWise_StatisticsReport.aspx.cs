using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class DepartmentWise_StatisticsReport : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Expence_Bal_C ObjEBC = new Expence_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                ddlDeptName.DataSource = SubdepartmentLogic_Bal_C.getSubDepartment(Convert.ToInt32(Session["Branchid"]), 0, 0);
                ddlDeptName.DataTextField = "subdeptName";
                ddlDeptName.DataValueField = "SDCode";
                ddlDeptName.DataBind();
                ddlDeptName.Items.Insert(0, "Select Department");
                ddlDeptName.SelectedIndex = -1;
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("DepartmentWise_StatisticsReport.aspx");
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

    }
   
  

    protected void btndailytrans_Click(object sender, EventArgs e)
    {
        string username = "", ExpenceName = "";
        //if (ddlDeptName.SelectedItem.Text == "Select Department")
        //{
        //    ddlDeptName.Focus();
        //    lblMsg.Text = "Select department";
        //    return;
        //}
        //if (ddlYear.SelectedValue == "Year")
        //{
        //    lblMsg.Text = "Select Year";
        //    ddlYear.Focus();
        //    return;
        //}
        //if (DdlMonth.SelectedValue == "Month")
        //{
        //    lblMsg.Text = "Select Year";
        //    DdlMonth.Focus();
        //    return;
        //}
        string sql = "";

        string query1 = "ALTER VIEW [dbo].[VW_LabStatistics_Report_Child] AS (SELECT     distinct   patmst.CenterName, (patmst.PID) AS NumberofExams,patmst.Patregdate,SubDepartment.subdeptName,patmst.TestCharges as TestChargesAll, TestCharges.Amount as TestCharges, patmst.Drname " +
                   " FROM   patmst INNER JOIN "+
                   " patmstd ON patmst.PID = patmstd.PID INNER JOIN "+
                   " SubDepartment ON patmstd.SDCode = SubDepartment.SDCode INNER JOIN "+
                   " TestCharges ON patmstd.MTCode = TestCharges.STCODE " +
                    "  where patmst.IsActive=1 ";//and YEAR(patmstd.Patregdate)=" + ddlYear.SelectedValue + " and Month(patmstd.Patregdate)=" + DdlMonth.SelectedValue + " ";
                  if (ddlDeptName.SelectedItem.Text != "Select Department")
                  {
                      query1 += " and  SubDepartment.SDCode='" + ddlDeptName.SelectedValue + "'";
                  }
                  if (ddlYear.SelectedValue != "Year")
                  {
                      query1 += " and   YEAR(patmstd.Patregdate)=" + ddlYear.SelectedValue + " ";
                  }
                  if (DdlMonth.SelectedValue != "Month")
                  {
                      query1 += " and   Month(patmstd.Patregdate)=" + DdlMonth.SelectedValue + " ";
                  }

        SqlConnection con1 = DataAccess.ConInitForDC();
        SqlCommand cmd11 = con1.CreateCommand();
        cmd11.CommandText = query1 + ")";
        con1.Open();
        cmd11.ExecuteNonQuery();
        con1.Close(); con1.Dispose();

        SqlConnection con1H = DataAccess.ConInitForHM();
       // con1H.Open();
        string HMSDB = con1H.Database;
        string query12 = "ALTER VIEW [dbo].[VW_LabStatistics_Report_Child_OPD] AS (SELECT     distinct   patmst.CenterName, (patmst.PID) AS NumberofExams,  patmst.Patregdate, " + HMSDB + ".dbo.LabServiceDetails.MTCode, " +
             "   " + HMSDB + ".dbo.LabServiceDetails.BillServiceCharges as TestChargesAll," + HMSDB + ".dbo.LabServiceDetails.BillServiceCharges as TestCharges,  SubDepartment.subdeptName " +
             "   FROM    " + HMSDB + ".dbo.LabServiceDetails INNER JOIN " +
             "   patmst ON " + HMSDB + ".dbo.LabServiceDetails.PatRegId = patmst.PPID INNER JOIN " +
             "   patmstd ON patmst.PatRegID = patmstd.PatRegID AND patmst.PID = patmstd.PID AND " + HMSDB + ".dbo.LabServiceDetails.MTCode = patmstd.MTCode INNER JOIN " +
             "   SubDepartment ON patmstd.SDCode = SubDepartment.SDCode INNER JOIN "+
             "   " + HMSDB + ".dbo.LabRegistration ON " + HMSDB + ".dbo.LabServiceDetails.PatRegId = " + HMSDB + ".dbo.LabRegistration.PatRegId AND " +
             "   " + HMSDB + ".dbo.LabServiceDetails.BillNo = " + HMSDB + ".dbo.LabRegistration.BillNo AND " + HMSDB + ".dbo.LabServiceDetails.LabNo = " + HMSDB + ".dbo.LabRegistration.LabNo " +
             "  where patmst.IsActive=1 and " + HMSDB + ".dbo.LabRegistration.CancelBill=0 and  patmst.CenterName='OPD' ";//and YEAR(patmstd.Patregdate)=" + ddlYear.SelectedValue + " and Month(patmstd.Patregdate)=" + DdlMonth.SelectedValue + " ";
        if (ddlDeptName.SelectedItem.Text != "Select Department")
        {
            query12 += " and  SubDepartment.SDCode='" + ddlDeptName.SelectedValue + "'";
        }
        if (ddlYear.SelectedValue != "Year")
        {
            query12 += " and   YEAR(patmstd.Patregdate)=" + ddlYear.SelectedValue + " ";
        }
        if (DdlMonth.SelectedValue != "Month")
        {
            query12 += " and   Month(patmstd.Patregdate)=" + DdlMonth.SelectedValue + " ";
        }

        SqlConnection con12 = DataAccess.ConInitForDC();
        SqlCommand cmd112 = con12.CreateCommand();
        cmd112.CommandText = query12 + ")";
        con12.Open();
        cmd112.ExecuteNonQuery();
        con12.Close(); con12.Dispose();


        string query = "ALTER VIEW [dbo].[VW_LabStatistics_Report] AS (SELECT        patmst.CenterName, COUNT(patmstd.mtcode) AS NumberofExams, SubDepartment.subdeptName, ";
                     //"   dbo.FUN_GetStatistic_count( patmst.CenterName,SubDepartment.subdeptName, Month(patmstd.Patregdate) , YEAR(patmstd.Patregdate))as NoOfPAtients, " +
                     //"   dbo.FUN_GetStatistic_Revenue( patmst.CenterName,SubDepartment.subdeptName, Month(patmstd.Patregdate) , YEAR(patmstd.Patregdate))as NoOfRevenue,patmst.Branchid " +
            if (ddlDeptName.SelectedItem.Text != "Select Department" && ddlYear.SelectedValue != "Year" && DdlMonth.SelectedValue != "Month")
            {
                query += "    dbo.FUN_GetStatistic_count( patmst.CenterName,'" + ddlDeptName.SelectedItem.Text + "','" + DdlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "')as NoOfPAtients, " +
       "   dbo.FUN_GetStatistic_Revenue( patmst.CenterName,'" + ddlDeptName.SelectedItem.Text + "','" + DdlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "')as NoOfRevenue ";
         }
         if (ddlDeptName.SelectedItem.Text != "Select Department" && ddlYear.SelectedValue == "Year" && DdlMonth.SelectedValue == "Month")
         {
             query += "    dbo.FUN_GetStatistic_count( patmst.CenterName,'"+ddlDeptName.SelectedItem.Text+"',0, 0)as NoOfPAtients, " +
        "   dbo.FUN_GetStatistic_Revenue( patmst.CenterName,'"+ddlDeptName.SelectedItem.Text+"',0,0)as NoOfRevenue ";
         }
         if (ddlDeptName.SelectedItem.Text != "Select Department" && ddlYear.SelectedValue != "Year" && DdlMonth.SelectedValue == "Month")
         {
          query += "    dbo.FUN_GetStatistic_count( patmst.CenterName,'" + ddlDeptName.SelectedItem.Text + "', 0,'" + ddlYear.SelectedValue + "')as NoOfPAtients, " +
        "   dbo.FUN_GetStatistic_Revenue( patmst.CenterName,'" + ddlDeptName.SelectedItem.Text + "',0,'" + ddlYear.SelectedValue + "')as NoOfRevenue ";

         }
         if (ddlDeptName.SelectedItem.Text == "Select Department" && ddlYear.SelectedValue != "Year" && DdlMonth.SelectedValue == "Month")
         {
             query += "    dbo.FUN_GetStatistic_count( patmst.CenterName,SubDepartment.subdeptName,0, '" + ddlYear.SelectedValue + "')as NoOfPAtients, " +
        "   dbo.FUN_GetStatistic_Revenue( patmst.CenterName,SubDepartment.subdeptName,0,'" + ddlYear.SelectedValue + "')as NoOfRevenue ";
         }

        // if (ddlYear.SelectedValue == "Year" && DdlMonth.SelectedValue != "Month" && ddlDeptName.SelectedItem.Text != "Select Department")
        // {
        //     query += "    dbo.FUN_GetStatistic_count( patmst.CenterName,SubDepartment.subdeptName,Month(patmstd.Patregdate), YEAR(patmstd.Patregdate))as NoOfPAtients, " +
        //"   dbo.FUN_GetStatistic_Revenue( patmst.CenterName,SubDepartment.subdeptName,Month(patmstd.Patregdate),YEAR(patmstd.Patregdate))as NoOfRevenue ";
        // }
         if (ddlYear.SelectedValue != "Year" && DdlMonth.SelectedValue != "Month" && ddlDeptName.SelectedItem.Text == "Select Department")
         {
             query += "    dbo.FUN_GetStatistic_count( patmst.CenterName,SubDepartment.subdeptName,'" + DdlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "')as NoOfPAtients, " +
        "   dbo.FUN_GetStatistic_Revenue( patmst.CenterName,SubDepartment.subdeptName,'" + DdlMonth.SelectedValue + "','" + ddlYear.SelectedValue + "')as NoOfRevenue ";
         }
         if (ddlYear.SelectedValue == "Year" && DdlMonth.SelectedValue == "Month" && ddlDeptName.SelectedItem.Text == "Select Department")
         {
             query += "    dbo.FUN_GetStatistic_count( patmst.CenterName,SubDepartment.subdeptName,0, 0)as NoOfPAtients, " +
        "   dbo.FUN_GetStatistic_Revenue( patmst.CenterName,SubDepartment.subdeptName,0,0)as NoOfRevenue ";
         }
         query += " ,patmst.Branchid FROM            patmst INNER JOIN " +
                      "  patmstd ON patmst.PID = patmstd.PID INNER JOIN "+
                      "  SubDepartment ON patmstd.SDCode = SubDepartment.SDCode "+
                      "  where  patmst.IsActive=1   ";//and YEAR(patmstd.Patregdate)=" + ddlYear.SelectedValue + " and Month(patmstd.Patregdate)=" + DdlMonth.SelectedValue + " and   SubDepartment.SDCode='" + ddlDeptName.SelectedValue + "' " +
        if (ddlDeptName.SelectedItem.Text != "Select Department")
        {
            query += " and  SubDepartment.SDCode='" + ddlDeptName.SelectedValue + "'";
        }
        if (ddlYear.SelectedValue != "Year")
        {
            query += " and   YEAR(patmstd.Patregdate)=" + ddlYear.SelectedValue + " ";
        }
        if (DdlMonth.SelectedValue != "Month")
        {
            query += " and   Month(patmstd.Patregdate)=" + DdlMonth.SelectedValue + " ";
        }
        query += " GROUP BY patmst.CenterName, SubDepartment.subdeptName,patmst.Branchid ";

               SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        cmd1.CommandText = query + ")";
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        string queryC = "ALTER VIEW [dbo].[VW_AllTestCount] AS (SELECT DISTINCT "+
              "  patmst.CenterName, count(patmstd.PID) AS NumberofExams, "+
              "  MainTest.Maintestname, SubDepartment.subdeptName,patmst.Branchid "+
              "  FROM            patmst INNER JOIN "+
              "  patmstd ON patmst.PID = patmstd.PID INNER JOIN "+
              "  TestCharges ON patmstd.MTCode = TestCharges.STCODE INNER JOIN "+
              "  MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode INNER JOIN "+
              "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode " +
                    "  where patmst.IsActive=1";// and YEAR(patmstd.Patregdate)=" + ddlYear.SelectedValue + " and Month(patmstd.Patregdate)=" + DdlMonth.SelectedValue + " and   SubDepartment.SDCode='" + ddlDeptName.SelectedValue + "' " +
        if (ddlDeptName.SelectedItem.Text != "Select Department")
        {
            queryC += " and  SubDepartment.SDCode='" + ddlDeptName.SelectedValue + "'";
        }
        if (ddlYear.SelectedValue != "Year")
        {
            queryC += " and   YEAR(patmstd.Patregdate)=" + ddlYear.SelectedValue + " ";
        }
        if (DdlMonth.SelectedValue != "Month")
        {
            queryC += " and   month(patmstd.Patregdate)=" + DdlMonth.SelectedValue + " ";
        }
        queryC += " GROUP BY  MainTest.Maintestname, SubDepartment.subdeptName,patmst.CenterName,patmst.Branchid ";

        SqlConnection conC = DataAccess.ConInitForDC();
        SqlCommand cmd1C = conC.CreateCommand();
        cmd1C.CommandText = queryC + ")";
        conC.Open();
        cmd1C.ExecuteNonQuery();
        conC.Close(); conC.Dispose();

        ReportParameterClass.SelectionFormula = "";


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_LabStatisticReport.rpt");
        Session["reportname"] = "LabStatisticReport";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
        // ExportGridToExcel();
        // ExportGridToPDF();
    }
    //private void ExportGridToPDF()
    //{

    //    Response.ContentType = "application/pdf";
    //    Response.AddHeader("content-disposition", "attachment;filename=Vithal_Wadje.pdf");
    //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);
    //    GV_ExpenceEntry.RenderControl(hw);
    //    StringReader sr = new StringReader(sw.ToString());
    //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
    //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
    //    pdfDoc.Open();
    //    htmlparser.Parse(sr);
    //    pdfDoc.Close();
    //    Response.Write(pdfDoc);
    //    Response.End();
    //    GV_ExpenceEntry.AllowPaging = true;
    //    GV_ExpenceEntry.DataBind();
    //}  

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