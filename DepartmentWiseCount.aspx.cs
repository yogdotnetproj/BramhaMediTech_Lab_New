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

public partial class DepartmentWiseCount :BasePage
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
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("DepartmentWiseCount.aspx");
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

        if (ddlYear.SelectedValue == "Year")
        {
            return;
        }
       
        string sql = "";
        string query = "ALTER VIEW [dbo].[VW_GetDepartmentwiseCount] AS (SELECT * FROM   "+
                      "  ( "+
                      "  SELECT      patmstd.MTCode, patmstd.SDCode, MainTest.Maintestname, "+
                      "  SubDepartment.subdeptName, "+
                      "  datename(MONTH,patmstd.Createdon) as CreatedByMonth,YEAR(patmstd.Createdon)as CreatedbyYear "+
                      "  FROM     patmstd INNER JOIN "+
                      "  MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN "+
                      "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode "+
                      "  where YEAR(patmstd.Createdon)=" + ddlYear.SelectedValue + " " +
                      "  ) t "+
                      "  PIVOT( "+
                      "  COUNT(MTCode) "+
                      "  FOR CreatedByMonth IN ( "+
                       " [January],  "+
                       " [February], "+
                       " [March], "+
                       " [April], "+
                       " [May], "+
                       " [June], "+
                       " [July], "+
                       " [August], "+
                       " [September], "+ 
                       " [October], "+
                       " [November], "+
                       " [December] "+
		
                      "  ) "+
                      "  ) AS pivot_table";

       





        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        cmd1.CommandText = query + ")";
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
        ReportParameterClass.SelectionFormula = "";


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_DepartmentwiseTestCount.rpt");
        Session["reportname"] = "DepartmentwiseTestCount";
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