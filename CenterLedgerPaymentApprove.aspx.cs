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

public partial class CenterLedgerPaymentApprove :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    string  labcode_main = "";
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Ledgrmst_Supp_Bal_C ObjLSB = new Ledgrmst_Supp_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {

        //  Page.SetFocus(fromdate);

        if (!IsPostBack)
        {

            try
            {
            
                FillddlCenter();

                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                if (Session["usertype"] != null)
                {
                    if (Session["usertype"].ToString() == "CollectionCenter")
                    {
                        createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        ddlCenter.SelectedValue = (ui.CenterCode.Trim());
                        ddlCenter.Enabled = false;
                    }
                    else
                    {
                        ddlCenter.Enabled = true;

                    }
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


   
    private void FillddlCenter()
    {
        try
        {
            ddlCenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Session["LabCode"], Convert.ToInt32(Session["Branchid"]));
            ddlCenter.DataTextField = "Name";
            ddlCenter.DataValueField = "DoctorCode";
            ddlCenter.DataBind();
            ddlCenter.Items.Insert(0, "Select " + Convert.ToString(Session["CenterName"]));
            ddlCenter.SelectedIndex = 1;

            ddlUserName.DataSource = createuserlogic_Bal_C.get_All_Users(Convert.ToInt32(Session["Branchid"]));
            ddlUserName.DataTextField = "username";
            ddlUserName.DataValueField = "username";
            ddlUserName.DataBind();
            ddlUserName.Items.Insert(0, "Select UserName");
            ddlUserName.SelectedIndex = -1;
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
            Alterview();

            string labcode = Convert.ToString(HttpContext.Current.Session["LabCode"]);
            if (labcode != null && labcode != "")
            {
                labcode_main = labcode;
            }



            dt = ObjLSB.GetCenterLedgerApprovePayment(DateTimeConvesion.getDateFromString(fromdate.Text.Trim()), DateTimeConvesion.getDateFromString(todate.Text.Trim()), ddlCenter.SelectedItem.Text.ToString(), rblApprove.SelectedValue);
            GV_CenterLedger.DataSource = dt;
            GV_CenterLedger.DataBind();
            float Charges = 0, DrAmt = 0, DisAmt = 0, BalAmt = 0;

            for (int i = 0; i < GV_CenterLedger.Rows.Count; i++)
            {

                string txt_17 = (GV_CenterLedger.Rows[i].Cells[07].Text);
                if (txt_17 != "")
                {
                    Charges = Charges + Convert.ToSingle(txt_17);

                }
            }
            lblMsg.Text = "Total Receive Amt:" + Charges + "";

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
            if ((e.Row.FindControl("hdnIsPaymentApprove") as HiddenField).Value != "")
            {
                int PaymentApprove = Convert.ToInt32((e.Row.FindControl("hdnIsPaymentApprove") as HiddenField).Value);
                if (PaymentApprove == 0)
                {
                    e.Row.Cells[09].Text = "<span class='btn btn-xs btn-danger' >Not Appro</span>";
                }
                else if (PaymentApprove == 1)
                {
                    e.Row.Cells[09].Text = "<span class='btn btn-xs btn-success' >Appro</span>";
                    e.Row.Cells[15].Enabled = false;
                    e.Row.Cells[13].Enabled = false;
                }
                else
                {
                    e.Row.Cells[09].Text = "<span class='btn btn-xs btn-danger' >Reject</span>";
                    e.Row.Cells[15].Enabled = false;
                    e.Row.Cells[13].Enabled = false;
                }
            }
            else
            {
                e.Row.Cells[09].Text = "<span class='btn btn-xs btn-danger' >Not Appro</span>";
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // string item = e.Row.Cells[0].Text;
                foreach (Button button in e.Row.Cells[15].Controls.OfType<Button>())
                {
                    if (button.CommandName == "Select")
                    {
                        button.Attributes["onclick"] = "if(!confirm('Are you sure you want to approve payment ?')){ return false; };";
                    }
                }
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

    protected void GV_CenterLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_CenterLedger.PageIndex = e.NewPageIndex;
        btnList_Click1(null, null);
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
        string FileName = " Center Ledger " + DateTime.Now + ".xls";
        // string FileName = " Center Ledger " + DateTime.Now + ".pdf";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        // Response.ContentType = "application.pdf";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GV_CenterLedger.GridLines = GridLines.Both;
        GV_CenterLedger.HeaderStyle.Font.Bold = true;
        GV_CenterLedger.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
        // ===============================================


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
            //GridView HeaderGrid = (GridView)sender;
            //GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            //TableCell HeaderCell = new TableCell();
            //HeaderCell.Text = "Center Ledger";
            //// HeaderCell.Font = System.Drawing.Color.Black;

            //HeaderCell.ColumnSpan = 9;
            //HeaderGridRow.Cells.Add(HeaderCell);
            //GV_CenterLedger.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }


    protected void btnPdf_Click(object sender, EventArgs e)
    {
        ExportGridToPDF();
    }
    private void ExportGridToPDF()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        // string FileName = " Center Ledger " + DateTime.Now + ".xls";
        string FileName = " Center Ledger " + DateTime.Now + ".pdf";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        // Response.ContentType = "application/vnd.ms-excel";
        Response.ContentType = "application.pdf";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GV_CenterLedger.GridLines = GridLines.Both;
        GV_CenterLedger.HeaderStyle.Font.Bold = true;
        GV_CenterLedger.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
        // ===============================================


    }

    protected void GV_CenterLedger_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            string Message = "";
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GV_CenterLedger.Rows[rowIndex];
            int ApproveId = Convert.ToInt32(GV_CenterLedger.DataKeys[rowIndex].Values["Id"]);

            // string PatRegId = GV_CenterLedger.Rows[rowIndex].Cells[0].Text;
            // string IPDNO = GV_CenterLedger.Rows[rowIndex].Cells[1].Text;
            string AproRemark = "";
            int Status = 0;



            AproRemark = Convert.ToString((GV_CenterLedger.Rows[rowIndex].Cells[1].FindControl("txtApproveRemarks") as TextBox).Text);
            Status = Convert.ToInt32((GV_CenterLedger.Rows[rowIndex].Cells[1].FindControl("Status") as DropDownList).SelectedValue);

            if (Status == 3)
            {
                lblMsg.Text = "select Status.";
                return;
            }


            int Branchid = Convert.ToInt32(Session["Branchid"]);
            string UserName = Convert.ToString(Session["username"]);
            int FID = Convert.ToInt32(Session["FId"]);
            ObjLSB.Accept_RejetPayment(ApproveId, Convert.ToInt32(Status), Convert.ToString(AproRemark), Branchid, UserName);
            lblMsg.Text = "Payment Action successfully!.";
            btnList_Click1(null, null);

        }
    }
    public void Alterview()
    {
        #region Myregion2
        SqlConnection conn21 = DataAccess.ConInitForDC();
        SqlCommand sc21 = conn21.CreateCommand();
        sc21.CommandText = "ALTER VIEW [dbo].[VW_CenterPaymentReceiveCheck] AS (select *, Case when isnull( IsPaymentApprove,0)=0 then 'Not Approve' when isnull( IsPaymentApprove,0)=1 then 'Approve' else 'Reject' end As PaymentApprove from CPReceive  "; // Monthlybill=1  and
        //  " and  (CAST(CAST(YEAR( DateOfEntry) AS varchar(4)) + '/' + CAST(MONTH( DateOfEntry) AS varchar(2)) + '/' + CAST(DAY( DateOfEntry) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(dfrom).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(dto).ToString("MM/dd/yyyy") + "') and  centername =@CenterCode  " +
        // " group by DateOfEntry,Centercode,centername ,testname,RegNo,BillNo ,FirstName,VW_csmst1vw.username  " +
        //  "Where 1=1";
        if (fromdate.Text != "")
        {
            sc21.CommandText += " where     (CAST(CAST(YEAR(Receivedate) AS varchar(4)) + '/' + CAST(MONTH(Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// IsPaymentApprove<>2 and
        }
        if (ddlCenter.SelectedItem.Text.ToString() != "Select ")
        {
            sc21.CommandText += " and  Centercode ='" + ddlCenter.SelectedItem.Text.Trim() + "'";
        }

        if (rblApprove.SelectedValue == "0")
        {
            sc21.CommandText += " and  IsPaymentApprove=0";
        }
        if (rblApprove.SelectedValue == "1")
        {
            sc21.CommandText += " and  IsPaymentApprove=1";
        }
        if (rblApprove.SelectedValue == "2")
        {
            sc21.CommandText += " and  IsPaymentApprove=2";
        }
        if (ddlUserName.SelectedItem.Text.ToString() != "Select UserName")
        {
            sc21.CommandText += " and  Username ='" + ddlUserName.SelectedItem.Text.Trim() + "'";
        }

        sc21.CommandText += ")";

        conn21.Open();
        try
        {
            sc21.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn21.Close(); conn21.Dispose();
        #endregion
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GV_CenterLedger.Rows.Count; i++)
        {
            // int ApproveId = Convert.ToInt32(GV_CenterLedger.DataKeys[rowIndex].Values["Id"]);
            int ApproveId = Convert.ToInt32(GV_CenterLedger.DataKeys[i].Value.ToString().Trim());
            if ((GV_CenterLedger.Rows[i].FindControl("ChkApprove") as CheckBox).Checked == true)
            {
                string AproRemark = (GV_CenterLedger.Rows[i].Cells[14].Text);
                int Branchid = Convert.ToInt32(Session["Branchid"]);
                string UserName = Convert.ToString(Session["username"]);
                int FID = Convert.ToInt32(Session["FId"]);
                ObjLSB.Accept_RejetPayment(ApproveId, Convert.ToInt32(1), Convert.ToString(AproRemark), Branchid, UserName);

            }
            //string txt_17 = (GV_CenterLedger.Rows[i].Cells[07].Text);
            //if (txt_17 != "")
            //{
            //   // Charges = Charges + Convert.ToSingle(txt_17);

            //}
        }
        lblMsg.Text = "Payment Approve successfully!.";
        btnList_Click1(null, null);
    }

}