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
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;
public partial class BillDesk :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string CName = "", Patregid = "", CenCode = "", labcode_main = "", Patname = "", Mobno = "";
    DateTime stDate = Date.getMinDate(), endDate = Date.getMinDate();
    Patmst_New_Bal_C PNB_C = new Patmst_New_Bal_C();
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
                        checkexistpageright("BillDesk.aspx");
                    }
                }

                fromdate.Text = DateTime.Now.ToShortDateString();
                todate.Text = DateTime.Now.ToShortDateString();
                txtCenter.Text = "All";
                Bind_Center();
                if (Session["usertype"].ToString() == "CollectionCenter" && Session["username"] != null)
                {
                    ddlcenter.Visible = true;
                    // Label4.Visible = false;
                    createuserTable_Bal_C Obj_CTB = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));

                    DrMT_Bal_C DrMT = new DrMT_Bal_C(Obj_CTB.CenterCode, "CC", Convert.ToInt32(Session["Branchid"]));
                    Session["CenterCode"] = Obj_CTB.CenterCode;

                    ddlcenter.SelectedValue = Obj_CTB.CenterCode;
                    ddlcenter.Enabled = false;
                    ddlcenter.Width = 240; 
                    ddlcenter.Height = 30;
                }
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
    public void Bind_Center()
    {
        ddlcenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]));
        ddlcenter.DataTextField = "Name";
        ddlcenter.DataValueField = "DoctorCode";
        ddlcenter.DataBind();
        ddlcenter.Items.Insert(0, "All Center");
        ddlcenter.Items[0].Value = "0";
        ddlcenter.SelectedIndex = -1;
    }
    void BindGrid()
    {
        try
        {
            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
            if (labcode != null && labcode != "")
            {
                this.labcode_main = labcode;
            }
            if (Session["usertype"].ToString() == "CollectionCenter")
            {

                CenCode = Session["CenterCode"].ToString();

            }
            //if (txtCenter.Text != "")
            //{
            //    CName = txtCenter.Text;
            //}
            if (ddlcenter.SelectedItem.Text != "All Center")
            {

                string[] CenCodeN = ddlcenter.SelectedItem.Text.Split('=');
                CName = CenCodeN[0];
            }
            if (txtregno.Text != "")
            {
                Patregid = txtregno.Text;

            }
            if (txtname.Text != "")
            {
                Patname = txtname.Text.Replace("'", "'+char(39)+'"); ;
            }
            if (txtmobileno.Text != "")
            {
                Mobno = txtmobileno.Text;
            }
            if (fromdate.Text != "" && todate.Text != "")
            {
                stDate = DateTimeConvesion.getDateFromString(fromdate.Text);
                endDate = DateTimeConvesion.getDateFromString(todate.Text);


            }
            DataTable dt = new DataTable();
            dt = PNB_C.GetPatientInformationnew_1(CName, stDate, endDate, Patregid, Convert.ToInt32(Session["Branchid"]), 0, CenCode, "", this.labcode_main, Patname, Mobno, CName);
            GV_Billdesk.DataSource = dt;//PNB_C.GetPatientInformationnew_1(CName, stDate, endDate, Patregid, Convert.ToInt32(Session["Branchid"]), 0, CenCode, "", this.labcode_main, Patname, Mobno, CName);
            GV_Billdesk.DataBind();
             float  Charges = 0, DrAmt = 0,DisAmt=0,BalAmt=0;
             LblTcharge.Text= "Total Patient is:"+GV_Billdesk.Rows.Count+ "";
             for (int i = 0; i < GV_Billdesk.Rows.Count; i++)
             {
                
                 string txt_17 = (GV_Billdesk.Rows[i].Cells[13].Text);
                 if (txt_17 != "")
                 {
                     Charges = Charges + Convert.ToSingle(txt_17);

                 }
                 string DrAmtT = (GV_Billdesk.Rows[i].Cells[14].Text);
                 if (DrAmtT != "")
                 {
                     DrAmt = DrAmt + Convert.ToSingle(DrAmtT);
                 }
                 string DisAmtT = (GV_Billdesk.Rows[i].Cells[15].Text);
                 if (DisAmtT != "")
                 {
                     DisAmt = DisAmt + Convert.ToSingle(DisAmtT);
                 }
                 string BalAmtT = (GV_Billdesk.Rows[i].Cells[16].Text);
                 if (BalAmtT != "")
                 {
                     BalAmt = BalAmt + Convert.ToSingle(BalAmtT);
                 }
             }
             lblcharges.Text = Convert.ToString(Charges);
             lbldramt.Text = Convert.ToString(DrAmt);
             lbldisamt.Text = Convert.ToString(DisAmt);
             lblbal.Text = Convert.ToString(BalAmt);
//            float Pcount = dt
//.AsEnumerable()
//.Select(r => r.Field<float>("TestCharges"))
//.Distinct().Sum();
//            // .Count();
//            LblTcharge.Text = Convert.ToString(Pcount);
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
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void GV_Billdesk_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (e.NewEditIndex == -1)
            return;
        int PID = Convert.ToInt32(GV_Billdesk.DataKeys[e.NewEditIndex].Value);
        string FID = (GV_Billdesk.Rows[e.NewEditIndex].FindControl("Hdnfid") as HiddenField).Value;
        Response.Redirect("Paybilldesk.aspx?PID=" + PID + "&FID=" + FID, false);

    }
    protected void GV_Billdesk_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Billdesk.PageIndex = e.NewPageIndex;
        BindGrid();
    }
 
    protected void GV_Billdesk_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            try
            {
                string charges = (e.Row.FindControl("lblTestCharges") as Label).Text;
                string Discount = e.Row.Cells[15].Text;
                string Tax = e.Row.Cells[16].Text;
                charges = Convert.ToString( Convert.ToSingle(charges) + Math.Round( Convert.ToSingle(Tax)));
                int total = 0;
                if (charges != "" && charges != null && charges != "&nbsp;")
                {
                    total = Convert.ToInt32(charges);
                }
                float PaidamtA = Convert.ToSingle(e.Row.Cells[14].Text);
                float  Paidamt = Convert.ToSingle(e.Row.Cells[15].Text);
                float balance = Convert.ToSingle(e.Row.Cells[16].Text);
                if (PaidamtA < Convert.ToSingle(0))
                {
                    
                    e.Row.Cells[03].Text = "<span class='btn btn-xs btn-warning' >BillCanele </span>";
                    //e.Row.Cells[01].Enabled = false;
                    e.Row.Cells[0].Enabled = false;
                }
                else if (balance < 0)
                {

                    e.Row.Cells[03].Text = "<span class='btn btn-xs btn-warning' >Refund</span>";
                   // e.Row.Cells[0].Enabled = true;
                }
                else  if (Paidamt > Convert.ToSingle(charges))
                {
                    
                    e.Row.Cells[03].Text = "<span class='btn btn-xs btn-warning' >Refund</span>";
                   // e.Row.Cells[01].Enabled = false;
                   // e.Row.Cells[0].Enabled = true;
                }
                else if (balance > 0)
                {
                  
                    e.Row.Cells[03].Text = "<span class='btn btn-xs btn-danger' >Pending</span>";
                   // e.Row.Cells[0].Enabled = true;
                }
                else 
                {
                  
                    e.Row.Cells[03].Text = "<span class='btn btn-xs btn-success' >Done</span>";
                    //e.Row.Cells[01].Enabled = false;
                   // e.Row.Cells[0].Enabled = false;
                }
                
                     string CenterCode = (e.Row.FindControl("hdnCentercode") as HiddenField).Value;
                     Boolean ISBTH = Convert.ToBoolean( (e.Row.FindControl("ISBTH") as HiddenField).Value);
                     if (ISBTH == true)
                     {
                        
                         e.Row.Cells[01].Enabled = false;
                         e.Row.Cells[0].Enabled = false;
                         e.Row.Cells[03].Text = "<span class='btn btn-xs btn-primary' >BTH/IPD</span>";
                     }

                e.Row.Cells[03].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[01].ForeColor = System.Drawing.Color.Black;
                e.Row.Cells[0].ForeColor = System.Drawing.Color.Black;
               
                DropDownList ddl_Receipt = e.Row.FindControl("ddlReceipt") as DropDownList;

                dt = new DataTable();
                dt = PNB_C.GetReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32( (e.Row.FindControl("hdnPID") as HiddenField).Value));
                if (dt.Rows.Count > 0)
                {
                    ddl_Receipt.DataSource = dt;
                    ddl_Receipt.DataTextField = "ReceiptNo";
                    ddl_Receipt.DataValueField = "ReceiptNo";
                    ddl_Receipt.DataBind();
                    ddl_Receipt.Items.Insert(0, "-Receipt-");
                    ddl_Receipt.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            { }
        }
    }
    protected void GV_Billdesk_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex == -1)
            return;
        int PID = Convert.ToInt32(GV_Billdesk.DataKeys[e.RowIndex].Value);
        
        #region MyRegion
        string sql = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();

               //sc.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT DISTINCT   RecM.BillNo, convert(date,dbo.RecM.Transdate,103 ) as RecDate, 'CashBill' as BillType, dbo.FUN_GetReceiveAmt(1, RecM.PID) AS AmtReceived, "+
               //      "   dbo.RecM.Disamt as Discount, patmst.TestCharges as NetPayment,   dbo.FUN_GetReceiveAmt(1, RecM.PID) AS AmtPaid,   "+
               //      "   dbo.FUN_GetReceiveAmt_Balance(1, RecM.PID) as Balance, " +
               //      "   'Admin' as username, dbo.FUN_GetReceiveOtherAmt(1, RecM.PID) as OtherCharges, "+
               //      "    patmst.PatRegID, patmst.intial, "+
               //      "   patmst.Patname, patmst.sex, patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, isnull(RecM.CardNo,'') AS DoctorName,   MainTest.Maintestname, MainTest.MTCode, "+
               //      "   patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 as DisFlag, patmst.Patusername, patmst.Patpassword, RecM.Comment, patmst.MDY,   patmst.Remark AS PatientRemark, "+
               //      "   patmst.Pataddress, patmst.PPID, RecM.PaymentType AS UnitCode, RecM.TaxAmount, patmst.IsActive AS TaxPer, 0 AS PrintCount, patmst.OtherRefDoctor "+
               //      "   , RecM.BillNo AS ReceiptNo "+
               //      "   ,dbo.FUN_GetReceiveOtherAmtRemark(1, RecM.PID) as OtherChargeRemark   "+
               //       "  FROM            patmst INNER JOIN "+
               //       "  DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN "+
               //       "  RecM INNER JOIN "+
               //       "  MainTest INNER JOIN "+
               //       "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON RecM.PID = patmstd.PID AND RecM.branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND "+
               //       "  patmst.Branchid = patmstd.Branchid LEFT OUTER JOIN "+
               //       "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode  where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and RecM.PID=" + PID + ")";// 
               sc.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT    distinct " +
                               "  RecM.BillNo, patmst.Patregdate AS RecDate, 'Cash Bill' AS BillType, dbo.FUN_GetReceiveAmt(1, RecM.PID)  AS AmtReceived, dbo.FUN_GetReceiveAmt_Discount(1, RecM.PID) AS Discount, " +
                               "  patmst.TestCharges AS NetPayment, dbo.FUN_GetReceiveAmt(1, RecM.PID) AS AmtPaid,  dbo.FUN_GetReceiveAmt_Balance(1,RecM.PID ) AS Balance, dbo.FUN_GetReceiveAmtUser(1, RecM.PID) as username,  " +
                               "  dbo.FUN_GetReceiveOtherAmt(1, RecM.PID)  AS OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age,   patmst.Drname, patmst.TelNo, DrMT.DoctorCode, " +
                               "  ISNULL(RecM.CardNo, N'') AS DoctorName, MainTest.Maintestname, MainTest.MTCode, patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag,   " +
                               "  patmst.Patusername, patmst.Patpassword, isnull(RecM.Comment,'') as Comment, patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, 'CASH' AS UnitCode, " +
                               "  RecM.TaxAmount, RecM.TaxPer,   0 AS PrintCount, patmst.OtherRefDoctor, RecM.BillNo as ReceiptNo, dbo.FUN_GetReceiveOtherAmtRemark(1, RecM.PID) as OtherChargeRemark, RecM.PID , DrMT.address1 as CenterAddress " +
                               " , cast(patmst.QRImage  AS VARBINARY(8000)) as QRImage, cast(patmst.QRImageD AS VARBINARY(8000)) as QRImageD ,cast(Patmst.PatientcHistory as nvarchar(2500))as   PatientcHistory "+
                               "  FROM            patmst INNER JOIN   DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN   MainTest INNER JOIN   " +
                               "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN   " +
                               "  RecM ON patmst.PID = RecM.PID LEFT OUTER JOIN   PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode  where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + "  and RecM.PID=" + PID + " )";//  "+

     

        conn.Open();
        sc.ExecuteNonQuery();
        conn.Close(); conn.Dispose();
        #endregion


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PayReceipt.rpt");
        Session["reportname"] = "CashReceipt";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
       
    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

            StreamReader sr = new StreamReader(httpres.GetResponseStream());

            string results = sr.ReadToEnd();

            sr.Close();
            return results;
        }
        catch
        {
            return "0";
        }
    }
  protected void ddlReceipt_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList dd
        int RowIndex1 = ((GridViewRow)((DropDownList)sender).NamingContainer).RowIndex;

        for (int i = 0; i < GV_Billdesk.Rows.Count; i++)
        {
            DropDownList ddl_Receipt = GV_Billdesk.Rows[i].FindControl("ddlReceipt") as DropDownList;
            HiddenField hdn_PID = GV_Billdesk.Rows[i].FindControl("hdnPID") as HiddenField;
            HiddenField hdnBillNo = GV_Billdesk.Rows[i].FindControl("hdn_BillNo") as HiddenField;
            HiddenField hdnFID = GV_Billdesk.Rows[i].FindControl("Hdnfid") as HiddenField;
            Patmstd_Bal_C PBC = new Patmstd_Bal_C();
           
            if (ddl_Receipt.SelectedIndex > 0)
            {
                if (ddl_Receipt.SelectedIndex > 1)
                {
                    if (RowIndex1 == i)
                    {
                        #region MyRegion
                        string sql = "";
                        SqlConnection conn = DataAccess.ConInitForDC();
                        SqlCommand sc = conn.CreateCommand();
                        string CounCode = PBC.GetSMSString_CountryCode("Registration", Convert.ToInt16(Session["Branchid"]));
                        
                             sc.CommandText = "ALTER VIEW [dbo].[VW_csrstvw] AS (SELECT     patmst.PatRegID, patmst.intial, patmst.Patname,  patmst.sex, patmst.Age,     patmst.Drname, patmst.TelNo, DrMT.DoctorCode, DrMT.DoctorName, " +
                              "   MainTest.Maintestname, MainTest.MTCode,     patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, patmst.Patusername,   " +
                              "   patmst.Patpassword, patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress,  " +
                              "   patmst.PPID, VW_billreceipt_new.PaymentType as UnitCode, VW_billreceipt_new.BillNo, VW_billreceipt_new.Paymenttype, VW_billreceipt_new.billdate,    " +
                              "   VW_billreceipt_new.AmtPaid AS AmtPaid, VW_billreceipt_new.DisAmt, VW_billreceipt_new.TaxAmount ,VW_billreceipt_new.ReceiptNo " +
                              "   FROM         patmst INNER JOIN    DrMT ON patmst.Centercode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN  " +
                              "   MainTest INNER JOIN " +
                              "   patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON     patmst.PID = patmstd.PID AND " +
                               "  patmst.Branchid = patmstd.Branchid INNER JOIN    VW_billreceipt_new ON patmstd.PID = VW_billreceipt_new.PID LEFT OUTER JOIN   " +
                               "  PackMst ON patmstd.Branchid = PackMst.branchid AND     patmstd.PackageCode = PackMst.PackageCode where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + "  and VW_billreceipt_new.PID=" + hdn_PID.Value + " and VW_billreceipt_new.FID=" + hdnFID.Value + " and VW_billreceipt_new.ReceiptNo=" + ddl_Receipt.SelectedValue + ")";// 
                         
                        conn.Open();
                        sc.ExecuteNonQuery();
                        conn.Close(); conn.Dispose();
                        #endregion
                        Session.Add("rptsql", sql);
                        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_Receipt.rpt");
                        Session["reportname"] = "CashBillReceipt";
                        Session["RPTFORMAT"] = "pdf";

                        ReportParameterClass.SelectionFormula = sql;
                        string close = "<script language='javascript'>javascript:OpenReport();</script>";
                        Type title1 = this.GetType();
                        Page.ClientScript.RegisterStartupScript(title1, "", close);
                    }
                }
                else
                {

                    if (RowIndex1 == i)
                    {
                        #region MyRegion
                        string sql = "";
                        SqlConnection conn = DataAccess.ConInitForDC();
                        SqlCommand sc = conn.CreateCommand();
                        string CounCode = PBC.GetSMSString_CountryCode("Registration", Convert.ToInt16(Session["Branchid"]));
                        if (CounCode.Length == 2)
                        {
                            sc.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT    distinct    RecM.BillNo, patmst.Patregdate AS RecDate, 'Cash Bill' AS BillType, SUM(RecM.AmtPaid) AS AmtReceived, SUM(RecM.DisAmt) AS Discount, patmst.TestCharges AS NetPayment, SUM(RecM.AmtPaid) AS AmtPaid, " +
                                  "  patmst.TestCharges + SUM(RecM.OtherCharges) - SUM(RecM.AmtPaid) AS Balance,dbo.FUN_GetReceiveAmtUser(1, RecM.PID) as username, SUM(RecM.OtherCharges) AS OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, " +
                                  "  patmst.Drname, patmst.TelNo, DrMT.DoctorCode, ISNULL(RecM.CardNo, N'') AS DoctorName, MainTest.Maintestname, MainTest.MTCode, patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag, " +
                                  "  patmst.Patusername, patmst.Patpassword, isnull(RecM.Comment,'') as Comment, patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, RecM.PaymentType AS UnitCode, RecM.TaxAmount, RecM.TaxPer, " +
                                  "  dbo.FUN_GetPrintcount(RecM.branchid, RecM.PID) AS PrintCount, patmst.OtherRefDoctor, RecM.ReceiptNo, RecM.OtherChargeRemark, RecM.PID , DrMT.address1 as CenterAddress " +
                                  "  , cast(patmst.QRImage  AS VARBINARY(8000)) as QRImage, cast(patmst.QRImageD AS VARBINARY(8000)) as QRImageD ,cast(Patmst.PatientcHistory as nvarchar(2500))as   PatientcHistory " +
                                  "  FROM            patmst INNER JOIN " +
                                  "  DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN " +
                                  "  MainTest INNER JOIN " +
                                  "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN " +
                                  "  RecM ON patmst.PID = RecM.PID LEFT OUTER JOIN " +
                                  "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode " +
                                   " where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and patmst.PID=" + hdn_PID.Value + " and ReceiptNo=" + ddl_Receipt.SelectedValue + " GROUP BY RecM.BillNo, patmst.Patregdate, patmst.TestCharges, dbo.FUN_GetReceiveAmtUser(1, RecM.PID), patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, ISNULL(RecM.CardNo, N''),  " +
                                   " MainTest.Maintestname, MainTest.MTCode, patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, patmst.Patusername, patmst.Patpassword, isnull(RecM.Comment,''), patmst.MDY, patmst.Remark, patmst.Pataddress,  " +
                                   " patmst.PPID, RecM.PaymentType, RecM.TaxAmount, RecM.TaxPer, dbo.FUN_GetPrintcount(RecM.branchid, RecM.PID), patmst.OtherRefDoctor, RecM.ReceiptNo, RecM.OtherChargeRemark, RecM.PID,DrMT.address1, cast(patmst.QRImage  AS VARBINARY(8000)), cast(patmst.QRImageD AS VARBINARY(8000))  ,cast(Patmst.PatientcHistory as nvarchar(2500)) )";// 

                        }
                        else
                        {

                            sc.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT    distinct    RecM.BillNo, patmst.Patregdate AS RecDate, 'Cash Bill' AS BillType, SUM(RecM.AmtPaid) AS AmtReceived, SUM(RecM.DisAmt) AS Discount, patmst.TestCharges AS NetPayment, SUM(RecM.AmtPaid) AS AmtPaid, " +
                                    "  patmst.TestCharges + SUM(RecM.OtherCharges) - SUM(RecM.AmtPaid) AS Balance, dbo.FUN_GetReceiveAmtUser(1, RecM.PID) as username, SUM(RecM.OtherCharges) AS OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, " +
                                    "  patmst.Drname, patmst.TelNo, DrMT.DoctorCode, ISNULL(RecM.CardNo, N'') AS DoctorName, MainTest.Maintestname, MainTest.MTCode, patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag, " +
                                    "  patmst.Patusername, patmst.Patpassword, isnull(RecM.Comment,'') as Comment, patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, RecM.PaymentType AS UnitCode, RecM.TaxAmount, RecM.TaxPer, " +
                                    "  dbo.FUN_GetPrintcount(RecM.branchid, RecM.PID) AS PrintCount, patmst.OtherRefDoctor, RecM.ReceiptNo, RecM.OtherChargeRemark, RecM.PID  ,DrMT.address1 as CenterAddress " +
                                    "  ,cast(patmst.QRImage  AS VARBINARY(8000)) as QRImage, cast(patmst.QRImageD AS VARBINARY(8000)) as QRImageD ,cast(Patmst.PatientcHistory as nvarchar(2500))as   PatientcHistory " +
                                    "  FROM            patmst INNER JOIN " +
                                    "  DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN " +
                                    "  MainTest INNER JOIN " +
                                    "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN " +
                                    "  RecM ON patmst.PID = RecM.PID LEFT OUTER JOIN " +
                                    "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode " +
                                     " where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and patmst.PID=" + hdn_PID.Value + " and ReceiptNo=" + ddl_Receipt.SelectedValue + " GROUP BY RecM.BillNo, patmst.Patregdate, patmst.TestCharges, dbo.FUN_GetReceiveAmtUser(1, RecM.PID), patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, ISNULL(RecM.CardNo, N''),  " +
                                     " MainTest.Maintestname, MainTest.MTCode, patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, patmst.Patusername, patmst.Patpassword, isnull(RecM.Comment,'') , patmst.MDY, patmst.Remark, patmst.Pataddress,  " +
                                     " patmst.PPID, RecM.PaymentType, RecM.TaxAmount, RecM.TaxPer, dbo.FUN_GetPrintcount(RecM.branchid, RecM.PID), patmst.OtherRefDoctor, RecM.ReceiptNo, RecM.OtherChargeRemark, RecM.PID, DrMT.address1 ,cast(patmst.QRImage  AS VARBINARY(8000)) , cast(patmst.QRImageD AS VARBINARY(8000))  ,cast(Patmst.PatientcHistory as nvarchar(2500)) )";// 

                          
                        }
                        conn.Open();
                        sc.ExecuteNonQuery();
                        conn.Close(); conn.Dispose();
                        #endregion


                        Session.Add("rptsql", sql);
                        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PayReceipt.rpt");
                        Session["reportname"] = "CashReceipt";
                        Session["RPTFORMAT"] = "pdf";

                        ReportParameterClass.SelectionFormula = sql;
                        string close = "<script language='javascript'>javascript:OpenReport();</script>";
                        Type title1 = this.GetType();
                        Page.ClientScript.RegisterStartupScript(title1, "", close);
                      //   PBC.Update_PrintCount_ReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(hdn_PID.Value), Convert.ToInt32( ddl_Receipt.SelectedValue));
                        Session["PID_report"] = Convert.ToInt32(hdn_PID.Value);
                        Session["RecNo_report"] = Convert.ToInt32(ddl_Receipt.SelectedValue);
                    }
                }
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