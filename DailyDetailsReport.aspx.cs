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

public partial class DailyDetailsReport : System.Web.UI.Page
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
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("DailyDetailsReport.aspx");
                    }
                }
                Binddropdown();
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
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
   
    private void Binddropdown()
    {
        try
        {

            if (Session["usertype"].ToString() == "Admin" || Session["usertype"].ToString().ToLower() == "administrator")
            {
                ddlusername.DataSource = createuserlogic_Bal_C.getAllUsers(Convert.ToInt32(Session["Branchid"]));
                ddlusername.DataTextField = "username";
                ddlusername.DataValueField = "username";
                ddlusername.DataBind();
                ddlusername.Items.Insert(0, "Select UserName");
                ddlusername.SelectedIndex = -1;
            }
            else
            {
                ddlusername.DataSource = createuserlogic_Bal_C.getAllUsers_username(Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"]));
                ddlusername.DataTextField = "username";
                ddlusername.DataValueField = "username";
                ddlusername.DataBind();
                ddlusername.Items.Insert(0, "Select UserName");
                ddlusername.SelectedIndex = 1;
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

    protected void btnlist_Click(object sender, EventArgs e)
    {
        BindGrid();
    }



    void BindGrid()
    {
        try
        {

            object fromDate = null;
            object Todate = null;

            if (fromdate.Text != "")
            {
                fromDate = DateTimeConvesion.getDateFromString(fromdate.Text.Trim()).ToString();
            }
            if (todate.Text != "")
            {
                Todate = DateTimeConvesion.getDateFromString(todate.Text.Trim()).ToString();
            }

            string username = "", ExpenceName = "";

            if (ddlusername.SelectedItem.Text != "Select UserName")
            {
                username = ddlusername.SelectedItem.Text;
            }
            if (txtexpenceName.Text != "")
            {
                ExpenceName = txtexpenceName.Text;
            }
            GV_ExpenceEntry.DataSource = Expence_Bal_C.Get_ExpenceData(Todate, fromDate, username, Convert.ToInt32(Session["Branchid"]), ExpenceName);
            GV_ExpenceEntry.DataBind();
            float sum = 0.0f, Charges = 0;
            for (int i = 0; i < GV_ExpenceEntry.Rows.Count; i++)
            {
                string txt_17 = (GV_ExpenceEntry.Rows[i].Cells[2].Text);
                if (txt_17 != "")
                {
                    Charges = Charges + Convert.ToSingle(txt_17);
                    a.Visible = true;
                }
            }
            lblcharges.Text = Convert.ToString(Charges);
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

    protected void btndailytrans_Click(object sender, EventArgs e)
    {
        string username = "", ExpenceName = "";

        if (ddlusername.SelectedItem.Text != "Select UserName")
        {
            username = ddlusername.SelectedItem.Text;
        }
        if (txtexpenceName.Text != "")
        {
            ExpenceName = txtexpenceName.Text;
        }
        string sql = "";
       // string query = "ALTER VIEW [dbo].[VW_ExpenceReport] AS (SELECT  * FROM   DailyExpenceDetails where ExpenceDate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        string query = "ALTER VIEW [dbo].[VW_DailyDetailsReport] AS (select CONVERT(date,CurrentDate,103) as EntryDate,sum(ExpenceAmount)as ExpenceAmount,0 as CancelAmt,0 as DisAmount,0 as CashAmount,0 as RefundAmount , count(id)as ExpCount ,0 as CancelCount,0 as DisCount,0 as CashCount,0 as RefCount from DailyExpenceDetails " +
                                  " where  CONVERT(date,CurrentDate,103) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') "+
                               "  group by CONVERT(date,CurrentDate,103) "+
                               " union all "+
                              "  select  CONVERT(date,Canceldate,103)as EntryDate,0 as ExpenceAmount,sum(Charges)as CancelAmt,0 as DisAmount,0 as CashAmount,0 as RefundAmount  ,  0 as ExpCount, count(id)as CancelCount,0 as DisCount,0 as CashCount,0 as RefCount from Patientbillcancelation " +
                                " where  CONVERT(date,Canceldate,103) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') " +
                             "  group by CONVERT(date,Canceldate,103) " +
                             " union all " +
                            "  select CONVERT(date,tdate,103)as EntryDate,0 as ExpenceAmount ,0 as CancelAmt,sum(DisAmt)as DisAmount,0 as CashAmount,0 as RefundAmount , 0 as ExpCount, 0 as CancelCount,count(PID)as DisCount,0 as CashCount,0 as RefCount from recm " +
                             " where DisAmt>0 and  CONVERT(date,tdate,103) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') " +
                             " group by CONVERT(date,tdate,103) " +
                             " union all " +
                             " select CONVERT(date,tdate,103)as EntryDate,0 as ExpenceAmount ,0 as CancelAmt,0 as DisAmount,sum(Amtpaid)as CashAmount,0 as RefundAmount ,0 as ExpCount, 0 as CancelCount,0 as DisCount,count(PID) as CashCount,0 as RefCount from recm " +
                            "  where amtpaid>0 and CONVERT(date,tdate,103) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') " +
                             " group by CONVERT(date,tdate,103) " +
                             " union all " +
                             " select CONVERT(date,tdate,103)as EntryDate,0 as ExpenceAmount ,0 as CancelAmt,0 as DisAmount,0 as CashAmount,sum(Amtpaid)as RefundAmount , 0 as ExpCount, 0 as CancelCount,0 as DisCount,0 as CashCount,count(PID)  as RefCount from recm " +
                            "  where amtpaid<0 and CONVERT(date,tdate,103) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') " +
                             " group by CONVERT(date,tdate,103)";
        //where ExpenceDate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";

        //if (ExpenceName != "" && ExpenceName != null)
        //{
        //    query += " and Particular=N'" + ExpenceName + "'";
        //}

        //if (username != "" && username != null)
        //{
        //    query += " and username='" + username + "'";
        //}





        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        cmd1.CommandText = query + ")";
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
        ReportParameterClass.SelectionFormula = "";


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_DailyDetailsReport.rpt");
        Session["reportname"] = "DailyDetailsReport";
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
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Expence Report" + DateTime.Now + ".Pdf";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        // Response.ContentType = "application/vnd.ms-excel";
        Response.ContentType = "application/vnd.Pdf";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GV_ExpenceEntry.GridLines = GridLines.Both;
        GV_ExpenceEntry.HeaderStyle.Font.Bold = true;
        GV_ExpenceEntry.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void GV_ExpenceEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_ExpenceEntry.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void GV_ExpenceEntry_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Expence Report";
            // HeaderCell.Font = System.Drawing.Color.Black;

            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
            GV_ExpenceEntry.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void GV_ExpenceEntry_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

           string ExpId = Convert.ToString( GV_ExpenceEntry.DataKeys[e.NewEditIndex].Value);

           Response.Redirect("ExpenceEntry.aspx?ExpID=" + ExpId);
            e.Cancel = true;
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
    protected void GV_ExpenceEntry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            string sd = Convert.ToString(GV_ExpenceEntry.DataKeys[e.RowIndex].Value);


            e.Cancel = true;
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