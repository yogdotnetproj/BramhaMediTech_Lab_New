using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class BarcodeDirectPrint :BasePage
{
    string MTCode;
    Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
    DataTable dt = new DataTable();
    PatSt_Bal_C objPrintStatus = new PatSt_Bal_C();
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
                   // objPrintStatus.AlterView_Barcode_Temp(subdept, PID);
                    objPrintStatus.AlterView_Barcode_Temp_Direct(subdept, PID);

                    //GVBarcode.DataSource = BarcodeLogic_C.Get_barcodelist(PID, Branchid, subdept);
                    GVBarcode.DataSource = BarcodeLogic_C.Get_barcodelist_Direct(PID, Branchid, subdept);
                    GVBarcode.DataBind();

                   // GVBarcode.DataBind();
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
        #region  Information Of Patient
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
            
            LblRefDoc.Text = PB_C.RefDr;

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
     
        string BarCode_ID = "";
        string subdept = "";
        dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        }
        objPrintStatus.AlterView_Barcode_Temp(subdept, PID);

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
    protected void Button2_Click(object sender, EventArgs e)
    {
        int PID = Convert.ToInt32(Request.QueryString["PID"].ToString());
        int branchid = Convert.ToInt32(Request.QueryString["Branchid"].ToString());
        string FID = Convert.ToString(Request.QueryString["FID"].ToString());
        string PatRegID = Convert.ToString(Request.QueryString["PatRegID"].ToString());

        string BarCode_ID = "";
        string subdept = "";
        dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        }
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