 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using System.Data.Odbc;

using System.Text.RegularExpressions;
using Microsoft;
using System.Collections.Specialized;
using System.Text;

using System.Web.Services;
using System.Web.Script.Services;
using System.Net;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Management;
public partial class BarcodePrint_First :BasePage
{
    string MTCode;
    Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
    DataTable dt = new DataTable();
    PatSt_Bal_C objPrintStatus = new PatSt_Bal_C();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        int PID = Convert.ToInt32(Request.QueryString["PID"].ToString());
        int Branchid = Convert.ToInt32(Request.QueryString["Branchid"].ToString());
        string TestCode = Convert.ToString(Request.QueryString["TestCode"]);
        string SampleType = Convert.ToString(Request.QueryString["SampleType"]);

        if (!IsPostBack)
        {
            try
            {
                BindBanner();
                string subdept = "";
                dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
                if (dt.Rows.Count > 0)
                {
                    subdept = Convert.ToString(dt.Rows[0]["subdept"]);
                }
                if (TestCode != null && SampleType != null)
                {
                    objPrintStatus.AlterView_Barcode_Temp(subdept, PID);
                    GVBarcode.DataSource = BarcodeLogic_C.Get_barcodelist_sampletypewise(PID, Branchid, SampleType, TestCode);
                    GVBarcode.DataBind();
                }
                else
                {
                    objPrintStatus.AlterView_Barcode_Temp(subdept, PID);

                    GVBarcode.DataSource = BarcodeLogic_C.Get_barcodelist(PID, Branchid, subdept);
                    GVBarcode.DataBind();
                }
                for (int i = 0; i < GVBarcode.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        if (GVBarcode.DataKeys[i].Value.ToString().Trim() == GVBarcode.DataKeys[i - 1].Value.ToString().Trim())
                        {
                            GVBarcode.Rows[i].Cells[1].Text = "";
                        }
                        
                    }
                    (GVBarcode.Rows[i].FindControl("chkflag") as CheckBox).Checked = true;
                }
                Fill_Labels();
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
    public void Fill_Labels() 
    {
        #region Information Of Patient
        Patmst_Bal_C PB_C = null;
        try
        {

            PB_C = new Patmst_Bal_C(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Convert.ToInt32(Session["Branchid"]));

            lblRegNo.Text = Convert.ToString(PB_C.PatRegID);
           
            ViewState["PID"] = PB_C.PID;
            lblName.Text = PB_C.Initial.Trim() + "." + PB_C.Patname;
            lblage.Text = Convert.ToString(PB_C.Age) + "/" + PB_C.MYD;
            lblSex.Text = PB_C.Sex;

            LblMobileno.Text = PB_C.Phone;
            Lblcenter.Text = PB_C.CenterName;
            
            LblRefDoc.Text = PB_C.Drname;

        }
        catch
        {
            lblRegNo.Visible = true;
            lblRegNo.Text = "Record not found";
        }
        #endregion
    }

    protected void Button1_Click(object sender, EventArgs e)
    {


        int PID = Convert.ToInt32(Request.QueryString["PID"].ToString());
        int branchid = Convert.ToInt32(Request.QueryString["Branchid"].ToString());
        string FID = Convert.ToString(Request.QueryString["FID"].ToString());
        string PatRegID = Convert.ToString(Request.QueryString["PatRegID"].ToString());
       // string TestCodesT = Convert.ToString(Request.QueryString["TestCode"].ToString());

        string BarCode_ID = "", BarCode_IDPrint = "";
        string subdept = "", TestCodesT="";
        dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        }
        //=====================================================
        if (Convert.ToString( ViewState["BarCodeImageReq"]) == "YES")
        {
            BarCodeImage(PID);
        }
        //=====================================================
        objPrintStatus.AlterView_Barcode_Temp(subdept, PID);
        Barcode_C ObjBCC = new Barcode_C();
        for (int i = 0; i < GVBarcode.Rows.Count; i++)
        {
            CheckBox chkver = (GVBarcode.Rows[i].FindControl("chkflag") as CheckBox);
            if (chkver.Checked == true)
            {
                if (BarCode_ID == "")
                {
                    BarCode_ID = "'" + Convert.ToString(GVBarcode.Rows[i].Cells[1].Text) + "'";
                }
                else
                {
                    BarCode_ID = BarCode_ID + ',' + "'" + Convert.ToString(GVBarcode.Rows[i].Cells[1].Text) + "'";
                }
                BarCode_IDPrint = Convert.ToString(GVBarcode.Rows[i].Cells[1].Text);
                TestCodesT = Convert.ToString(GVBarcode.Rows[i].Cells[2].Text);
                if (Convert.ToString(Request.QueryString["TwoWayAccept"]) == "Yes")
                {
                    ObjBCC.UpdateIspheboAccept_TestWise_New_Barcode(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 1, BarCode_IDPrint, Convert.ToString(Session["username"]), "");

                }
                else
                {
                    ObjBCC.UpdateIspheboAccept_TestWise_Firstway_New(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 1, TestCodesT, Convert.ToString(Session["username"]));

                }
            }

        }
        if (BarCode_ID == "")
        {
            for (int i = 0; i < GVBarcode.Rows.Count; i++)
            {

                if (BarCode_ID == "")
                {
                    BarCode_ID = "'" + Convert.ToString(GVBarcode.Rows[i].Cells[1].Text) + "'";
                }
                else
                {
                    BarCode_ID = BarCode_ID + ',' + "'" + Convert.ToString(GVBarcode.Rows[i].Cells[1].Text) + "'";
                }
            }
        }
        if (BarCode_ID != "")
        {
           
            objPrintStatus.AlterViewPrintBarcode(branchid, PatRegID, FID, BarCode_ID, PID);

           
            Session.Add("rptsql", "");
            Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PrintBarcode.rpt");
            Session["reportname"] = "PrintBarcode";
            Session["RPTFORMAT"] = "pdf";

            ReportParameterClass.SelectionFormula = "";
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
        }
    }
    public void BindBanner()
    {
         DataTable dtban = new DataTable();
        DataTable dtchk = new DataTable();
        dtban = ObjTB.Bindbanner();
        if (dtban.Rows.Count > 0)
        {
            if (Convert.ToString(dtban.Rows[0]["PaymentTypeDefaultActive"]) == "1")
            {
                ViewState["PaymentType"] = "YES";
            }
            else
            {
                ViewState["PaymentType"] = "NO";
            }
            if (Convert.ToString(dtban.Rows[0]["Type"]) == "0")
            {
                ViewState["VALIDATE"] = "YES";
            }
            else
            {
                ViewState["VALIDATE"] = "NO";
                //  rblPaymenttype.Items[0].Selected = true;
            }
            if (Convert.ToBoolean(dtban.Rows[0]["BarCodeImageReq"]) == false)
            {
                ViewState["BarCodeImageReq"] = "NO";
            }
            else
            {
                ViewState["BarCodeImageReq"] = "YES";
            }
        }
    }

    public void BarCodeImage(int PID)
    {
        //BarcodeWriter writer = new BarcodeWriter()
        //{
        //    Format = BarcodeFormat.CODE_128,
        //    Options = new EncodingOptions
        //    {
        //        Height = 400,
        //        Width = 800,
        //        PureBarcode = false,
        //        Margin = 10,
        //    },
        //};

        //var bitmap = writer.Write("test text");
        //bitmap.Save(HttpContext.Response.Body, System.Drawing.Imaging.ImageFormat.Png);
        //return; // there's no need to return a `FileContentResult` by `File(...);`

        SqlConnection con = DataAccess.ConInitForDC();

        SqlDataAdapter sda = null;
        DataTable dtbI = new DataTable();
        // DataSet1 dst = new DataSet1();
       // sda = new SqlDataAdapter("select distinct case when isnull(patmstd.PhlebotomistRejectremark,'') <>'' then patmstd.BarcodeID+'-'+'1' else patmstd.BarcodeID end as BarcodeID,MTCode from Patmstd where PID='" + Convert.ToString(PID) + "'   ", con);
        sda = new SqlDataAdapter("select distinct PatRegID as BarcodeID,MTCode from Patmstd where PID='" + Convert.ToString(PID) + "'   ", con);

        sda.Fill(dtbI);
        if (dtbI.Rows.Count > 0)
        {
            for (int p = 0; p < dtbI.Rows.Count; p++)
            {
                // string Code = "AAAAAAA";
                QrCodeEncodingOptions Option = new QrCodeEncodingOptions();
                BarcodeWriter writer = new BarcodeWriter()
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Height = 400,
                        Width = 800,
                        PureBarcode = false,
                        Margin = 10,
                    },
                };
                string Code = Convert.ToString(dtbI.Rows[p]["BarcodeID"]);
                var qrWrite = new BarcodeWriter(); ;
                qrWrite.Format = BarcodeFormat.CODE_128;
                int THeight = Convert.ToInt32(ViewState["BarCodeHeight"]), TWidth = Convert.ToInt32(ViewState["BarCodeWidth"]), TPureMar = Convert.ToInt32(ViewState["BarCodeMargin"]);
                qrWrite.Options = new EncodingOptions()
                {

                    Height = THeight,
                    Width = TWidth,
                    Margin = TPureMar,
                    PureBarcode = false,
                };

                // var result = new Bitmap(qrWrite.Write(Code));
                byte[] byteImage;
                using (Bitmap bitMap = new Bitmap(qrWrite.Write(Convert.ToString(dtbI.Rows[p]["BarcodeID"]))))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {

                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        {
                            byteImage = ms.ToArray();
                        }
                    }
                }

                SqlConnection conn = DataAccess.ConInitForDC();
                SqlCommand sc = new SqlCommand("" +
                "Update Patmstd " +
                "Set BarCodeImage=@SignImage " +
                " Where PID=@id and MTcode =@MTcode ", conn);
                SqlDataReader sdr = null;
                if (byteImage != null)
                {
                    sc.Parameters.AddWithValue("@SignImage", byteImage);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@SignImage", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    sc.Parameters.Add(imageParameter);
                }

                sc.Parameters.AddWithValue("@id", PID);
                sc.Parameters.AddWithValue("@MTcode", Convert.ToString(dtbI.Rows[p]["MTCode"]));
                try
                {
                    conn.Open();
                    sc.ExecuteNonQuery();
                }
                finally
                {
                    try
                    {
                        if (sdr != null) sdr.Close();
                        conn.Close(); conn.Dispose();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                }
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int PID = Convert.ToInt32(Request.QueryString["PID"].ToString());
        int branchid = Convert.ToInt32(Request.QueryString["Branchid"].ToString());
        string FID = Convert.ToString(Request.QueryString["FID"].ToString());
        string PatRegID = Convert.ToString(Request.QueryString["PatRegID"].ToString());
        //string TestCodesT = Convert.ToString(Request.QueryString["TestCode"].ToString());

        string BarCode_ID = "";
        string subdept = "", TestCodesT="";
        dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        }
        if (Convert.ToString(ViewState["BarCodeImageReq"]) == "YES")
        {
            BarCodeImage(PID);
        }
        Barcode_C ObjBCC = new Barcode_C();
        objPrintStatus.AlterView_Barcode_Temp_Deptwise(subdept, PID);

        for (int i = 0; i < GVBarcode.Rows.Count; i++)
        {
            CheckBox chkver = (GVBarcode.Rows[i].FindControl("chkflag") as CheckBox);
            if (chkver.Checked == true)
            {
                if (BarCode_ID == "")
                {
                    BarCode_ID = "'" + Convert.ToString(GVBarcode.Rows[i].Cells[1].Text) + "'";
                }
                else
                {
                    BarCode_ID = BarCode_ID + ',' + "'" + Convert.ToString(GVBarcode.Rows[i].Cells[1].Text) + "'";
                }
                TestCodesT = Convert.ToString(GVBarcode.Rows[i].Cells[2].Text);

                if (Convert.ToString(Request.QueryString["TwoWayAccept"]) == "Yes")
                {
                    ObjBCC.UpdateIspheboAccept_TestWise_New(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 1, TestCodesT, Convert.ToString(Session["username"]), "");

                }
                else
                {
                    ObjBCC.UpdateIspheboAccept_TestWise_Firstway_New(Convert.ToInt32(PID), Convert.ToInt32(Session["Branchid"]), 1, TestCodesT, Convert.ToString(Session["username"]));

                }
            }
        }
        if (BarCode_ID == "")
        {
            for (int i = 0; i < GVBarcode.Rows.Count; i++)
            {

                if (BarCode_ID == "")
                {
                    BarCode_ID = "'" + Convert.ToString(GVBarcode.Rows[i].Cells[1].Text) + "'";
                }
                else
                {
                    BarCode_ID = BarCode_ID + ',' + "'" + Convert.ToString(GVBarcode.Rows[i].Cells[1].Text) + "'";
                }
            }
        }
        if (BarCode_ID != "")
        {

            objPrintStatus.AlterViewPrintBarcode_deptwise(branchid, PatRegID, FID, BarCode_ID, PID);

            Session.Add("rptsql", "");
            Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PrintBarcode_deptwise.rpt");
            Session["reportname"] = "PrintBarcode_dept";
            Session["RPTFORMAT"] = "pdf";

            ReportParameterClass.SelectionFormula = "";
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
        }
    }
}