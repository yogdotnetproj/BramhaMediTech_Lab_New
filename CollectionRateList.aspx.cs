using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;
using System.Net;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CollectionRateList : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    string labcode_main = "";
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    CollectionRateList_C OBjCRL = new CollectionRateList_C();
    protected void Page_Load(object sender, EventArgs e)
    {

        //  Page.SetFocus(fromdate);

        if (!IsPostBack)
        {

            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("CollectionRateList.aspx");
                    }
                }
                FillddlCenter();

                // fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
               // todate.Text = Date.getdate().ToString("dd/MM/yyyy");  
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


  
    private void FillddlCenter()
    {
        try
        {
           dt=OBjCRL.Getdrname();
           ddlCenter.DataSource = dt;
           ddlCenter.DataTextField = "RateName";
           ddlCenter.DataValueField = "RatID";
            ddlCenter.DataBind();
            ddlCenter.Items.Insert(0, "--Select--");
            ddlCenter.SelectedIndex = 1;
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



    
    protected void btnList_Click1(object sender, EventArgs e)
    {
        try
        {  
            dt = OBjCRL.GetdrnameList(ddlCenter.SelectedItem.Value);
            GV_CenterLedger.DataSource = dt;
            GV_CenterLedger.DataBind();

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


    protected void GV_CenterLedger_PreRender(object sender, EventArgs e)
    {

    }
    protected void GV_CenterLedger_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex == -1)
                return;



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


    protected void GV_CenterLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_CenterLedger.PageIndex = e.NewPageIndex;
        this.btnList_Click1(null, null);
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {

        ExportGridToExcel();
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = " Collection Rate List " + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
       // Response.ContentType = "application/vnd.pdf";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GV_CenterLedger.GridLines = GridLines.Both;
        GV_CenterLedger.HeaderStyle.Font.Bold = true;
        GV_CenterLedger.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }

    protected void GV_CenterLedger_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Collection Rate List";
            // HeaderCell.Font = System.Drawing.Color.Black;

            HeaderCell.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell);
            GV_CenterLedger.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void btnpdfreport_Click(object sender, EventArgs e)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        string sql = "";
        string query = "ALTER VIEW [dbo].[VW_CeneterrateList] AS (SELECT DISTINCT "+
                      "  RatT.RatID, RatT.RateName, TestCharges.STCODE, TestCharges.Amount, TestCharges.Percentage, TestCharges.Emergency, TestCharges.username, "+
                      "  MainTest.Sampletype, MainTest.samecontain, MainTest.TatDuration, MainTest.Maintestname, TestCharges.DrCode "+
                      "  FROM         TestCharges INNER JOIN "+
                      "  RatT ON TestCharges.DrCode = RatT.RatID INNER JOIN "+
                      "  MainTest ON TestCharges.STCODE = MainTest.MTCode  " +
                     " where TestCharges.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";


        if (ddlCenter.SelectedItem.Text != "--Select--")
        {
            query += " and RatT.RatID='" + ddlCenter.SelectedItem.Value + "'";

        }     



        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_CenetrRateList.rpt");
        Session["reportname"] = "Rpt_CenetrRateList";
        Session["RPTFORMAT"] = "pdf";

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