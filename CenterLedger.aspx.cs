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

public partial class CenterLedger : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    string  labcode_main = "";
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
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
                        checkexistpageright("CenterLedger.aspx");
                    }
                }

                FillddlCenter();
              
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                if (Session["usertype"] != null)
                {
                    if (Session["usertype"].ToString() == "CollectionCenter")
                    {
                        createuserTable_Bal_C CTB = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        ddlCenter.SelectedValue = (CTB.CenterCode.Trim());
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
            ddlCenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            ddlCenter.DataTextField = "Name";
            ddlCenter.DataValueField = "DoctorCode";
            ddlCenter.DataBind();
            ddlCenter.Items.Insert(0, "Select " + Convert.ToString( Session["CenterName"]));
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

    public void alterview()
    {
        #region Myregion2
        SqlConnection conn2 = DataAccess.ConInitForDC();
        SqlCommand sc2 = conn2.CreateCommand();
        sc2.CommandText = "ALTER VIEW [dbo].[VW_Ledger1] AS (SELECT  convert(char(12),Patregdate,105)as RegDate,Centercode as Centercode1 ,centername as CenterCode,SUM( TestCharges)as DebitAmt,0 as CreditAmt,testname as ParticularField ,'' as ModeOfPayment,PatRegID,BillNo, FirstName ,VW_csmst1vw.username AS EntryuserName  from VW_csmst1vw where PatRegID<>'' and Monthlybill=1 and isactive=1  "; // Monthlybill=1  and
               
        if (fromdate.Text != "")
        {
            sc2.CommandText += " and (CAST(CAST(YEAR(Patregdate) AS varchar(4)) + '/' + CAST(MONTH(Patregdate) AS varchar(2)) + '/' + CAST(DAY(Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
        }
        if (ddlCenter.SelectedItem.Text.ToString() != "Select ")
        {
            sc2.CommandText += " and centername=N'" + ddlCenter.SelectedItem.Text.Trim() + "'";
        }

        sc2.CommandText += " group by Patregdate,Centercode,centername ,testname,PatRegID,BillNo ,FirstName,VW_csmst1vw.username ";
        sc2.CommandText += ")";

        conn2.Open();
        try
        {
            sc2.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn2.Close(); conn2.Dispose();
        #endregion

        #region Myregion2
        SqlConnection conn21 = DataAccess.ConInitForDC();
        SqlCommand sc21 = conn21.CreateCommand();
        sc21.CommandText = "ALTER VIEW [dbo].[VW_Ledger2] AS (select convert(char(12),Receivedate,105) as RegDate,''as Centercode1,Centercode as CenterCode, 0 as DebitAmt, sum(Receiveamount) as CreditAmt ,'' as ParticularField ,Paymenttype as ModeOfPayment ,'0' as PatRegID,0 as BillNo,''as FirstName,username AS EntryuserName from CPReceive  "; // Monthlybill=1  and
            
        if (fromdate.Text != "")
        {
            sc21.CommandText += " where (CAST(CAST(YEAR(Receivedate) AS varchar(4)) + '/' + CAST(MONTH(Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
        }
        if (ddlCenter.SelectedItem.Text.ToString() != "Select ")
        {
            sc21.CommandText += " and  Centercode =N'" + ddlCenter.SelectedItem.Text.Trim() + "'";
        }

        sc21.CommandText += " group by Receivedate,Centercode,Paymenttype,username  ";
        sc21.CommandText += ")";

        conn21.Open();
        try
        {
            sc21.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn21.Close(); conn21.Dispose();
        #endregion
        #region Myregion3
        SqlConnection conn213 = DataAccess.ConInitForDC();
        SqlCommand sc213 = conn213.CreateCommand();
        sc213.CommandText = "ALTER VIEW [dbo].[VW_FinalLedger] AS (select * from VW_Ledger2 union all select * from [VW_Ledger1]  "; // Monthlybill=1  and
       
        sc213.CommandText += ")";

        conn213.Open();
        try
        {
            sc213.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn213.Close(); conn213.Dispose();
        #endregion
    }
   
    protected void btnList_Click1(object sender, EventArgs e)
    {
        try
        {

           
            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
            if (labcode != null && labcode != "")
            {
                labcode_main = labcode;
            }    

            alterviewOpbal();
            alterview();
           // alterview_CenteralLedger();
            GV_CenterLedger.DataSource = Ledgrmst_Supp_Bal_C.getLedgerTransactionByFromTo_Led(DateTimeConvesion.getDateFromString(fromdate.Text.Trim()), DateTimeConvesion.getDateFromString(todate.Text.Trim()), ddlCenter.SelectedItem.Text.ToString(), Convert.ToInt32(Session["Branchid"]));

          
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

            int bno = Convert.ToInt32(GV_CenterLedger.DataKeys[e.Row.RowIndex].Value);
           
            //Closing balance
            if (e.Row.Cells[0].Text == "zzzzzzzz")
            {
                e.Row.Cells[0].Text = "";
                if (e.Row.Cells[1].Text == Date.getMinDate().ToString())
                {
                    e.Row.Cells[1].Text = "";
                }

                if (Convert.ToSingle(GV_CenterLedger.Rows[e.Row.RowIndex - 1].Cells[7].Text) >= 0)
                {
                    //Debit Side
                    e.Row.Cells[5].Text = GV_CenterLedger.Rows[e.Row.RowIndex - 1].Cells[7].Text;

                    e.Row.Cells[6].Text = "";
                }
                else
                {
                    //Credit side
                    e.Row.Cells[5].Text = "";
                    float minus = (-1) * Convert.ToSingle(GV_CenterLedger.Rows[e.Row.RowIndex - 1].Cells[7].Text);
                    e.Row.Cells[6].Text = minus.ToString();


                }

                return;
            }
            if (e.Row.Cells[1].Text.Trim() != "")
            {
                if (Convert.ToDateTime(e.Row.Cells[1].Text.Trim()) != Date.getMinDate())
                {
                    string date = e.Row.Cells[1].Text;
                    //date = Convert.ToDateTime(date).ToString("dd/MM/yyyy");

                    date = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
                    e.Row.Cells[1].Text = date;
                }
                else
                {
                    e.Row.Cells[1].Text = "";
                }
            }
            if (e.Row.RowIndex > 0)
            {
                if (Convert.ToSingle(e.Row.Cells[6].Text.Trim()) == 0)
                {
                    e.Row.Cells[6].Text = "";
                }
                if (Convert.ToSingle(e.Row.Cells[5].Text.Trim()) == 0)
                {
                    e.Row.Cells[5].Text = "";
                }
            }

            //Balance
            if (e.Row.RowIndex == 0)
            {
                if (e.Row.Cells[1].Text.Trim() != "")
                {
                    if (Convert.ToDateTime(e.Row.Cells[1].Text.Trim()) != Date.getMinDate())
                    {
                        e.Row.Cells[1].Text = "";
                    }
                }
                if (Convert.ToSingle(e.Row.Cells[5].Text) == 0)
                {
                    e.Row.Cells[5].Text = "";
                }
                else
                {
                    e.Row.Cells[7].Text = e.Row.Cells[5].Text; //Debit amt
                }
                if (Convert.ToSingle(e.Row.Cells[6].Text) == 0)
                {
                    e.Row.Cells[6].Text = "";
                }
                else
                {
                    e.Row.Cells[7].Text = e.Row.Cells[6].Text; //Debit amt
                }
                if ((e.Row.Cells[5].Text == "") && (e.Row.Cells[6].Text == ""))
                {
                    e.Row.Cells[7].Text = "0";
                }

            }

            if (e.Row.RowIndex > 0)
            {
                if (e.Row.Cells[5].Text == "")
                {
                    if (e.Row.Cells[6].Text == "")
                    {
                        e.Row.Cells[6].Text = "0";
                        e.Row.Cells[7].Text = Convert.ToString(Convert.ToSingle(GV_CenterLedger.Rows[e.Row.RowIndex - 1].Cells[7].Text) - 0);
                    }

                    //if (Convert.ToSingle(GV_CenterLedger.Rows[e.Row.RowIndex - 1].Cells[7].Text) != 0)
                    //{
                        e.Row.Cells[7].Text = Convert.ToString(Convert.ToSingle(GV_CenterLedger.Rows[e.Row.RowIndex - 1].Cells[7].Text) - Convert.ToSingle(e.Row.Cells[6].Text));
                    //}
                    //else
                    //{
                    //    e.Row.Cells[7].Text = Convert.ToString(Convert.ToSingle(GV_CenterLedger.Rows[e.Row.RowIndex - 1].Cells[7].Text)) ;
                    //}
                }
                else
                {
                    if (e.Row.Cells[6].Text == "")
                    {
                        //e.Row.Cells[3].Text = 0;
                        e.Row.Cells[7].Text = Convert.ToString(Convert.ToSingle(GV_CenterLedger.Rows[e.Row.RowIndex - 1].Cells[7].Text) + 0);
                    }

                    e.Row.Cells[7].Text = Convert.ToString(Convert.ToSingle(GV_CenterLedger.Rows[e.Row.RowIndex - 1].Cells[7].Text) + Convert.ToSingle(e.Row.Cells[5].Text));

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
    public void alterviewOpbal()
    {
        string sql = "";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        string query = "";
        if (ddlCenter.SelectedItem.Text != "Select ")
        {
             query = "ALTER VIEW [dbo].[VW_openingbal] AS (SELECT  SUM( TestCharges)as DebitAmt,sum(0) as CreditAmt ,Centername  " +
                 "   from VW_csmst1vw   where   PatRegID<>''  and centername= '" + ddlCenter.SelectedItem.Text + "' and  Monthlybill=1     " + //
                 "   and  (CAST(CAST(YEAR( Patregdate) AS varchar(4)) + '/' + CAST(MONTH( Patregdate) AS varchar(2)) + '/' + CAST(DAY( Patregdate) AS varchar(2)) AS datetime))  < ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "')   " +
                 "   group by Centercode,Centername  " +

                 "   union all    " +
                 "   select  sum(0) as DebitAmt, sum(Receiveamount) as CreditAmt,Centercode from CPReceive    " +
                  "  where  Centercode= '" + ddlCenter.SelectedItem.Text + "' and (CAST(CAST(YEAR( Receivedate) AS varchar(4)) + '/' + CAST(MONTH( Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  < ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "')   " +
                 "   group by  Centercode";


            cmd1.CommandText = query + ")";
        }
        else
        {
            query = "ALTER VIEW [dbo].[VW_openingbal] AS (SELECT  SUM( TestCharges)as DebitAmt,sum(0) as CreditAmt ,Centername  " +
                "   from VW_csmst1vw   where  PatRegID<>''  and  Monthlybill=1    " + //Monthlybill=1  and
                "   and  (CAST(CAST(YEAR( Patregdate) AS varchar(4)) + '/' + CAST(MONTH( Patregdate) AS varchar(2)) + '/' + CAST(DAY( Patregdate) AS varchar(2)) AS datetime))  <= ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "')   " +
                "   group by Centercode,Centername  " +

                "   union all    " +
                "   select  sum(0) as DebitAmt, sum(Receiveamount) as CreditAmt,Centercode from CPReceive    " +
                 "  where   (CAST(CAST(YEAR( Receivedate) AS varchar(4)) + '/' + CAST(MONTH( Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  <= ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "')   " +
                "   group by  Centercode";


            cmd1.CommandText = query + ")";
        }

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
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
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
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
            HeaderCell.Text = "Center Ledger";
            // HeaderCell.Font = System.Drawing.Color.Black;

            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);
            GV_CenterLedger.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }

    public void alterview_CenteralLedger()
    {
        string sql = "";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        string query = "";
        if (ddlCenter.SelectedItem.Text != "Select ")
        {
            query = "ALTER VIEW [dbo].[VW_Get_Centeral_ledger] AS (SELECT  convert(char(12),Patregdate,105)as RegDate,Centercode,centername as CenterCode,SUM( TestCharges)as DebitAmt,0 as CreditAmt,testname as ParticularField ,'' as ModeOfPayment,PatRegID,BillNo, Patname ,VW_csmst1vw.username AS EntryuserName  from VW_csmst1vw where PatRegID<>'' and Monthlybill=1   " + // Monthlybill=1  and
                 " and  (CAST(CAST(YEAR( Patregdate) AS varchar(4)) + '/' + CAST(MONTH( Patregdate) AS varchar(2)) + '/' + CAST(DAY( Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') and  centername = '" + ddlCenter.SelectedItem.Text + "' " +
                " group by Patregdate,Centercode,centername ,testname,PatRegID,BillNo ,Patname,VW_csmst1vw.username " +
                              "  union all " +
                               " select convert(char(12),Receivedate,105) as RegDate,''as Centercode,Centercode as CenterCode, 0 as DebitAmt, sum(Receiveamount) as CreditAmt ,'' as ParticularField ,Paymenttype as ModeOfPayment ,0 as PatRegID,0 as BillNo,''as Patname,username AS EntryuserName from CPReceive " +
                            "  where  (CAST(CAST(YEAR( Receivedate) AS varchar(4)) + '/' + CAST(MONTH( Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')  and  Centercode = '" + ddlCenter.SelectedItem.Text + "' " +
                               " group by Receivedate,Centercode,Paymenttype,username ";


            cmd1.CommandText = query + ")";
        }
        else
        {
            query = "ALTER VIEW [dbo].[VW_Get_Centeral_ledger] AS (SELECT  convert(char(12),Patregdate,105)as RegDate,Centercode,centername as CenterCode,SUM( TestCharges)as DebitAmt,0 as CreditAmt,testname as ParticularField ,'' as ModeOfPayment,PatRegID,BillNo, Patname ,VW_csmst1vw.username AS EntryuserName  from VW_csmst1vw where PatRegID<>'' and Monthlybill=1   " + // Monthlybill=1  and
                 " and  (CAST(CAST(YEAR( Patregdate) AS varchar(4)) + '/' + CAST(MONTH( Patregdate) AS varchar(2)) + '/' + CAST(DAY( Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')  " +
                " group by Patregdate,Centercode,centername ,testname,PatRegID,BillNo ,Patname,VW_csmst1vw.username " +
                              "  union all " +
                               " select convert(char(12),Receivedate,105) as RegDate,''as Centercode,Centercode as CenterCode, 0 as DebitAmt, sum(Receiveamount) as CreditAmt ,'' as ParticularField ,Paymenttype as ModeOfPayment ,0 as PatRegID,0 as BillNo,''as Patname,username AS EntryuserName from CPReceive " +
                            "  where  (CAST(CAST(YEAR( Receivedate) AS varchar(4)) + '/' + CAST(MONTH( Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') " +
                               " group by Receivedate,Centercode,Paymenttype,username ";



            cmd1.CommandText = query + ")";
        }

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
    }
    protected void btnPDf_Click(object sender, EventArgs e)
    {
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            labcode_main = labcode;
        }

        alterviewOpbal();
        alterview();
        // alterview_CenteralLedger();
        GV_CenterLedger.DataSource = Ledgrmst_Supp_Bal_C.getLedgerTransactionByFromTo_Led(DateTimeConvesion.getDateFromString(fromdate.Text.Trim()), DateTimeConvesion.getDateFromString(todate.Text.Trim()), ddlCenter.SelectedItem.Text.ToString(), Convert.ToInt32(Session["Branchid"]));


        GV_CenterLedger.DataBind();
        for (int i = 0; i < GV_CenterLedger.Rows.Count; i++)
        {

            if (GV_CenterLedger.Rows[i].Cells[3].Text.Trim() == "&nbsp;")
            {
                ObjTB.P_Patregno = "";
            }
            else
            {
                ObjTB.P_Patregno = GV_CenterLedger.Rows[i].Cells[3].Text.Trim();
            }
            if (GV_CenterLedger.Rows[i].Cells[0].Text.Trim() == "&nbsp;")
            {
                ObjTB.P_CenterName = "";
            }
            else
            {
                ObjTB.P_CenterName = GV_CenterLedger.Rows[i].Cells[0].Text.Trim();
            }
            ObjTB.P_RegDate = GV_CenterLedger.Rows[i].Cells[1].Text;
            if (GV_CenterLedger.Rows[i].Cells[2].Text.Trim() == "&nbsp;")
            {
                ObjTB.P_Particular = "";
            }
            else
            {
                ObjTB.P_Particular = GV_CenterLedger.Rows[i].Cells[2].Text;
            }
            ObjTB.P_BillNo = GV_CenterLedger.Rows[i].Cells[4].Text;
            if (GV_CenterLedger.Rows[i].Cells[5].Text != "")
            {
                ObjTB.P_DebitAmt = Convert.ToSingle(GV_CenterLedger.Rows[i].Cells[5].Text);
            }
            else
            {
                ObjTB.P_DebitAmt = 0;
            }

            if (GV_CenterLedger.Rows[i].Cells[6].Text != "")
            {
                ObjTB.P_CreditAmt = Convert.ToSingle(GV_CenterLedger.Rows[i].Cells[6].Text);
            }
            else
            {
                ObjTB.P_CreditAmt = 0;
            }
            if (GV_CenterLedger.Rows[i].Cells[7].Text != "")
            {
                ObjTB.P_Balance = Convert.ToSingle(GV_CenterLedger.Rows[i].Cells[7].Text);
            }
            else
            {
                ObjTB.P_Balance = 0;
            }
            if (GV_CenterLedger.Rows[i].Cells[8].Text.Trim() == "&nbsp;")
            {
                ObjTB.P_PaymentType = "";
            }
            else
            {
                ObjTB.P_PaymentType = GV_CenterLedger.Rows[i].Cells[8].Text;
            }
            if (GV_CenterLedger.Rows[i].Cells[9].Text.Trim() == "&nbsp;")
            {
                ObjTB.P_PatientName = "";
            }
            else
            {
                ObjTB.P_PatientName = GV_CenterLedger.Rows[i].Cells[9].Text;
            }
            if (GV_CenterLedger.Rows[i].Cells[10].Text.Trim() == "&nbsp;")
            {
                ObjTB.P_UserName= "";
            }
            else
            {
                ObjTB.P_UserName = GV_CenterLedger.Rows[i].Cells[10].Text;
            }
           
            ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
            ObjTB.Insert_CenterLedger_Report();
        }
           ObjTB.TruncateLedger();
           SqlConnection conn1 = DataAccess.ConInitForDC();
            SqlCommand sc1 = conn1.CreateCommand();
            sc1.CommandText = "ALTER VIEW VW_CenterLedger_Reportt AS  SELECT distinct   * from CenterLedger_Report ";

            conn1.Open();
            sc1.ExecuteNonQuery();
            conn1.Close(); conn1.Dispose();
        string sql = "";
        ReportParameterClass.ReportType = "CenterLedgerReport";
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_CenterLedger_Report.rpt");
        Session["reportname"] = "Rpt_CenterLedger_Report";
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