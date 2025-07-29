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

public partial class UploadReportPatient :BasePage
{
    string MTCode;
    Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
    DataTable dt = new DataTable();
    PatSt_Bal_C psnew = new PatSt_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Fill_Labels();

            

        }
    }
    public void Fill_Labels()
    {
        #region  Patient info
        Patmst_Bal_C CIT = null;
        try
        {

            CIT = new Patmst_Bal_C(Request.QueryString["PatRegID"], Convert.ToString(18), Convert.ToInt32(Session["Branchid"]));

            lblRegNo.Text = Convert.ToString(CIT.PatRegID);

            ViewState["PID"] = CIT.PID;
            lblName.Text = CIT.Initial.Trim() + "." + CIT.Patname;
            lblage.Text = Convert.ToString(CIT.Age) + "/" + CIT.MYD;
            lblSex.Text = CIT.Sex;

            LblMobileno.Text = CIT.Phone;
            Lblcenter.Text = CIT.CenterName;
            lbldate.Text = Convert.ToString(CIT.Patregdate);
            LblRefDoc.Text = CIT.Drname;

        }
        catch
        {
            lblRegNo.Visible = true;
            lblRegNo.Text = "Record not found";
        }
        #endregion
    }
    protected void btnupload_Click(object sender, EventArgs e)
    {
        if (FUBrowsePresc.HasFile)
        {
            try
            {

                //string Pname = txtFname.Text + "/" + DateTime.Now.ToString("ddMMyyyy");
                string PDate = DateTime.Now.ToString("ddMMyyyy");
                //string ViewP = "~/ViewPrescription/" + Pname;
                //FUBrowsePresc.SaveAs(Server.MapPath(ViewP));
                string DefaultFileName = "UploadOutReport/";
                if (FUBrowsePresc.HasFile)
                {
                    FUBrowsePresc.SaveAs(Server.MapPath(DefaultFileName) + "/" + Request.QueryString["PatRegID"] + "_" + Request.QueryString["TestCode"] + "_" + PDate + "_" + FUBrowsePresc.FileName);
                    LblFilename.Text = Request.QueryString["PatRegID"] + "_" + Request.QueryString["TestCode"] + "_" + PDate + "_" + FUBrowsePresc.FileName;
                    Cshmst_Bal_C Obj_CBC = new Cshmst_Bal_C();
                    Obj_CBC.P_UploadPrescription = DefaultFileName + "" + LblFilename.Text;
                    Obj_CBC.PID = Convert.ToInt32(0);
                    Obj_CBC.PatRegID = Convert.ToString(Request.QueryString["PatRegID"]);
                    Obj_CBC.MTCode = Convert.ToString(Request.QueryString["TestCode"]);
                    if (ChkNorep.Checked == true)
                    {
                        Obj_CBC.OutSideReport = true;
                    }
                    else
                    {
                        Obj_CBC.OutSideReport = false;
                    }
                    Obj_CBC.Insert_Update_OutLabReport(Convert.ToInt32(Session["Branchid"]));
                   // btnreceipt.Visible = true;
                }
            }
            catch (Exception ex)
            {
                //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
    }
    protected void btnreceipt_Click(object sender, EventArgs e)
    {
        string sql1 = "";

        SqlConnection conn1 = DataAccess.ConInitForDC();
        SqlCommand sc1 = conn1.CreateCommand();
        PatSt_Bal_C PBC = new PatSt_Bal_C();
        sc1.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT  dbo.Cshmst.BillNo, dbo.patmst.Patregdate as RecDate, dbo.Cshmst.BillType, dbo.Cshmst.AmtReceived,  dbo.Cshmst.Discount, dbo.Cshmst.NetPayment, " +
                  "  dbo.Cshmst.AmtPaid,   dbo.Cshmst.Balance,  dbo.Cshmst.username,dbo.Cshmst.OtherCharges,dbo.patmst.PatRegID, dbo.patmst.intial, dbo.patmst.Patname, " +
                  "  dbo.patmst.sex, dbo.patmst.Age, dbo.patmst.Drname,  dbo.patmst.TelNo, dbo.DrMT.DoctorCode, dbo.Cshmst.CardNo as DoctorName, dbo.MainTest.Maintestname, " +
                  "  dbo.MainTest.MTCode,  dbo.patmstd.TestRate, dbo.PackMst.PackageName, dbo.patmstd.PackageCode, dbo.Cshmst.DisFlag,  " +
                  "  dbo.patmst.Patusername,   dbo.patmst.Patpassword, dbo.Cshmst.Comment,dbo.patmst.MDY,dbo.patmst.Remark AS PatientRemark,dbo.patmst.Pataddress, " +
                  "  dbo.patmst.PPID ,dbo.Cshmst.Paymenttype as UnitCode ,   Cshmst.TaxAmount, Cshmst.TaxPer,dbo.FUN_GetPrintcount(Cshmst.branchid,Cshmst.PID) as PrintCount , patmst.OtherRefDoctor,RecM.ReceiptNo, dbo.Cshmst.OtherChargeRemark " +
            // "  FROM dbo.patmst INNER JOIN  dbo.DrMT ON dbo.patmst.CenterCode = dbo.DrMT.DoctorCode AND    dbo.patmst.branchid = dbo.DrMT.branchid INNER JOIN "+
            // "  dbo.Cshmst INNER JOIN dbo.MainTest INNER JOIN  dbo.patmstd "+
            // "  ON   dbo.MainTest.MTCode = dbo.patmstd.MTCode AND  dbo.MainTest.branchid = dbo.patmstd.branchid ON dbo.Cshmst.PID = dbo.patmstd.PID AND  "+
            //"  dbo.Cshmst.branchid = dbo.patmstd.branchid ON dbo.patmst.PID = dbo.patmstd.PID AND  dbo.patmst.branchid = dbo.patmstd.branchid  "+
            //"   LEFT OUTER JOIN  dbo.PackMst ON dbo.patmstd.branchid = dbo.PackMst.branchid AND  dbo.patmstd.PackageCode = dbo.PackMst.PackageCode where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and patmstd.PID='" + ViewState["PID"] + "' )";// and Cshmst.BillNo=" + bno + "

                 "   FROM         patmst INNER JOIN " +
                 "   DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN " +
                 "   Cshmst INNER JOIN " +
                 "   MainTest INNER JOIN " +
                 "   patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON Cshmst.PID = patmstd.PID AND " +
                 "   Cshmst.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN " +
                 "   RecM ON Cshmst.BillNo = RecM.BillNo AND Cshmst.PID = RecM.PID AND Cshmst.FID = RecM.FID LEFT OUTER JOIN " +
                 "   PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode " +
                 "   where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and patmstd.PID='" + Request.QueryString["PID"] + "' )";// and Cshmst.BillNo=" + bno + "
        conn1.Open();
        sc1.ExecuteNonQuery();
        conn1.Close(); conn1.Dispose();

        string RID = FinancialYearTableLogic.getPatregister(Convert.ToInt32(Session["Branchid"]));
        if (RID != "0")
        {
            

            Session.Add("rptsql", sql1);
            Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PayReceipt.rpt");
            Session["reportname"] = "CashReceipt";
            Session["RPTFORMAT"] = "pdf";

            ReportParameterClass.SelectionFormula = sql1;
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
            Session["PID_report"] = Convert.ToInt32(Request.QueryString["PID"]);
            Session["RecNo_report"] = 0;
            ViewState["receipt"] = "No";
        }

    }
}