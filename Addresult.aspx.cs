using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Text;
using System.Threading;
using System.IO;
using System.Web.Management;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing;

using System.Web.Services;
using System.Web.Script.Services;
public partial class Addresult :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    PatSt_Bal_C Obj_Patst = new PatSt_Bal_C();
    TAT_C ObjTA = new TAT_C();
    ArrayList al = new ArrayList();
    static Hashtable ht = new Hashtable();
    static Hashtable ht1 = new Hashtable();
    Patmstd_Main_Bal_C ObjPCB = new Patmstd_Main_Bal_C();
    public string PatRegID = "01";
    public string FID = "01";
    public string MTCode, singleValue;
    Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();
    string strr = "";
    short tabindex = 3;
    bool status = false;
    string rptname = "", path = "", selectonFormula = "", UserType = "", TestCode="";
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string Date2 = DateTime.Now.AddDays(-1).ToString("ddMMyyyy");
    string Barcode = "", MTCodeTemp = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Request.QueryString["EDate"]) != "" && Convert.ToString(Request.QueryString["EDate"]) != null)
        {
            //fromdate.Text = Request.QueryString["FDate"].Trim();
            //todate.Text = Date.getdate().ToString("dd/MM/yyyy");
            Patmst_New_Bal_C.AlterViewvw_VW_Result_ResMst(Session["UserType"].ToString(), Session["UserName"].ToString(), Request.QueryString["EDate"], Date.getdate().ToString("dd/MM/yyyy"));
        }


        if (Convert.ToString(Session["ISRegNoInt"]) == "YES")
        {
            //Obj_Patst.AlterVW_serialportdate_Old(Request.QueryString["PatRegID"].ToString());
           

           // Obj_Patst.AlterVW_serialportdate(Convert.ToString(Request.QueryString["PatRegID"]));
            Obj_Patst.AlterVW_serialportdate_NEWW(Convert.ToString(Request.QueryString["PatRegID"]));
            Obj_Patst.AlterVW_Int(Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["PatRegID"]), Convert.ToString(Request.QueryString["FID"]));
        }
        else
        {
            DataTable dtbarcode = new DataTable();
            dtbarcode = Obj_Patst.GetallBarcode(Convert.ToString( Request.QueryString["PatRegID"]));
            if (dtbarcode.Rows.Count > 0)
            {

                for (int b = 0; b < dtbarcode.Rows.Count; b++)
                {
                    if (Barcode == "")
                    {
                        Barcode = " '" + Convert.ToString(dtbarcode.Rows[b]["BarcodeID"]) + "' ";
                    }
                    else
                    {
                        Barcode = Barcode + "," + " '" + Convert.ToString(dtbarcode.Rows[b]["BarcodeID"]) + "' ";
                    }
                }
            }
            Obj_Patst.AlterVW_serialportdate_barcode(Barcode);
            Obj_Patst.AlterVW_Int_Barcode(Convert.ToInt32(Session["Branchid"]), Barcode, Convert.ToString(Request.QueryString["FID"]));

        }
    

        if (!IsPostBack)
        {
            try
            {
                try
                {
                  
                    //if (Convert.ToString(Session["usertype"]) != "Administrator")
                    //{
                    //    checkexistpageright("Addresult.aspx");
                    //}
                    ViewState["btnsave"] = "";
                    ViewState["sampleflag"] = "true";
                    if (Convert.ToString(Request.QueryString["Maindept"]) != null)
                    {
                        Session["DigModule"] = Convert.ToString(Request.QueryString["Maindept"]);
                    }
                    if (Session["DigModule"] == "0")
                    {
                        Session["DigModule"] = 2;
                    }
                    ViewState["PanicResult"] = 0;
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
                tabindex = 0;

                ViewState["Fyearid"] = FinancialYearTableLogic.getCurrentFinancialYear().FinancialYearId;


                if (Request.QueryString["PatRegID"] != null)
                    PatRegID = Request.QueryString["PatRegID"];
                if (Request.QueryString["FID"] != null)
                    FID = Request.QueryString["FID"];
                if (Convert.ToString(Session["usertype"]) != null)
                    UserType = Convert.ToString(Session["usertype"]);
                if (Convert.ToString(Request.QueryString["TestCode"]) != null)
                    TestCode = Request.QueryString["TestCode"];
                Fill_Labels();
                // Fill_Demography();
                if (Convert.ToString(Session["ISDemography"]) == "YES")
                {
                   // D1.Visible = true;
                    D2.Visible = true;
                    D3.Visible = true;
                    D4.Visible = true;
                    D5.Visible = true;
                    D6.Visible = true;
                   // D7.Visible = true;
                   // D8.Visible = true;
                    D9.Visible = true;
                    D10.Visible = true;
                    D11.Visible = true;
                    D12.Visible = true;
                    D13.Visible = true;
                   // D14.Visible = true;
                   // D15.Visible = true;
                    D16.Visible = true;
                }
                InsertUpdate();
                ObjTA.PatRegID = Convert.ToString( Request.QueryString["PatRegID"]);
                ObjTA.FID = Convert.ToString(Request.QueryString["FID"]);
                if (Convert.ToString(Session["ISRegNoInt"]) == "YES")
                {
                    ObjTA.updateIntresult();
                }
                else
                {
                    ObjTA.updateIntresult_Barcode();
                }
                ht = null;
                ht = (Hashtable)ViewState["ht"];

                string labcode1 = "";
                if (Session["UnitCode"] != null)
                {
                    labcode1 = Session["UnitCode"].ToString();
                }

                DataTable dtresult = new DataTable();
                DataTable dtresultsub = new DataTable();
                if (TestCode != "")
                {
                    dtresult = Obj_Patst.GetallResultEntry_TestCode(PatRegID, FID, Convert.ToInt32(Session["DigModule"]), Convert.ToInt32(Session["Branchid"]), UserType, Convert.ToString(Session["username"]), TestCode);

                }
                else
                {
                    dtresult = Obj_Patst.GetallResultEntry(PatRegID, FID, Convert.ToInt32(Session["DigModule"]), Convert.ToInt32(Session["Branchid"]), UserType, Convert.ToString(Session["username"]));
                }
                    // GVAAresultEntry.DataSource = dtresult;
                // GVAAresultEntry.DataBind();
                if (dtresult.Rows.Count > 0)
                {

                    for (int b = 0; b < dtresult.Rows.Count; b++)
                    {
                        if (MTCodeTemp == "")
                        {
                            MTCodeTemp = " '" + Convert.ToString(dtresult.Rows[b]["MTCode"]) + "' ";
                        }
                        else
                        {
                            MTCodeTemp = MTCodeTemp + "," + " '" + Convert.ToString(dtresult.Rows[b]["MTCode"]) + "' ";
                        }
                    }

                    PatSt_Bal_C PSBCL = new PatSt_Bal_C(Convert.ToString(ViewState["PPID"]), PatRegID, Convert.ToInt32(Session["Branchid"]));
                    // = PSBCL.PatRegID;
                    PSBCL.AlterView_PreviousResult(Convert.ToInt32(Session["Branchid"]), PSBCL.PatRegID, Request.QueryString["FID"], MTCodeTemp);


                    dtresultsub = Obj_Patst.GetallResultEntry_subdept(1, Convert.ToString(MTCodeTemp), Convert.ToString(Request.QueryString["PatRegID"]), Convert.ToString(Request.QueryString["FID"]));
                    GVAAresultEntrySub.DataSource = dtresultsub;
                    GVAAresultEntrySub.DataBind();
                }
                for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
                {

                    Label lblPrevResultDate = GVAAresultEntrySub.Rows[i].FindControl("lblPrevResultDate") as Label;
                    DropDownList ddldoctor = GVAAresultEntrySub.Rows[i].FindControl("ddldoctor") as DropDownList;
                    DropDownList ddlTechnician = GVAAresultEntrySub.Rows[i].FindControl("ddlTechnician") as DropDownList;
                    DropDownList DdlShortForm = GVAAresultEntrySub.Rows[i].FindControl("DdlShortForm") as DropDownList;
                    Label Lblsubdeptid = GVAAresultEntrySub.Rows[i].FindControl("Lblsubdeptid") as Label;
                    TextBox txtTestRemark = GVAAresultEntrySub.Rows[i].FindControl("txtTestRemark") as TextBox;
                    Label lblStatus = GVAAresultEntrySub.Rows[i].FindControl("lblStatus") as Label;
                    Label lblQcCheck = GVAAresultEntrySub.Rows[i].FindControl("lblQcCheck") as Label;
                    CheckBox cb = GVAAresultEntrySub.Rows[i].FindControl("chkautho") as CheckBox;
                    CheckBox Qc = GVAAresultEntrySub.Rows[i].FindControl("chkQc") as CheckBox;
                    string MTCode1 = ((GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label).Text);
                    string lblSDCode = ((GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label).Text);
                    TextBox txtSampDate = GVAAresultEntrySub.Rows[i].FindControl("txtSampDate") as TextBox;

                    txtSampDate.Text = Date.getdate().ToString("dd/MM/yyyy");
                    if (Convert.ToString(Session["usertype"]) == "Main Doctor")
                    {
                        dt = ObjPCB.GetallMaindoctor_addresult_Doctor(Convert.ToString(Session["DigModule"]), Convert.ToString(Lblsubdeptid.Text), Convert.ToString(Session["username"]));

                    }
                    else
                    {
                        dt = ObjPCB.GetallMaindoctor_addresult(Convert.ToString(Session["DigModule"]), Convert.ToString(Lblsubdeptid.Text));
                    }
                    if (dt.Rows.Count > 0)
                    {
                        ddldoctor.DataSource = dt;
                        ddldoctor.DataTextField = "Drsignature";
                        ddldoctor.DataValueField = "Signatureid";
                        ddldoctor.DataBind();

                        ddldoctor.SelectedIndex = 0;
                    }
                    dt = ObjPCB.GetallResult_ShortForm(Convert.ToString(MTCode1), Convert.ToString(lblSDCode));
                    if (dt.Rows.Count > 0)
                    {
                        DdlShortForm.DataSource = dt;
                        DdlShortForm.DataTextField = "Description";
                        DdlShortForm.DataValueField = "Description";
                        DdlShortForm.DataBind();
                        DdlShortForm.Items.Insert(0, "-ShortForm-");
                        DdlShortForm.SelectedIndex = 0;
                    }
                    dt = ObjPCB.GetallMaindoctor_addresult_Technican(Convert.ToString(Session["DigModule"]), Convert.ToString(Lblsubdeptid.Text));
                    if (dt.Rows.Count > 0)
                    {
                        ddlTechnician.DataSource = dt;
                        ddlTechnician.DataTextField = "Drsignature";
                        ddlTechnician.DataValueField = "Signatureid";
                        ddlTechnician.DataBind();
                        ddlTechnician.Items.Insert(0, "-Select Tech-");
                        ddlTechnician.SelectedIndex = 0;
                    }
                    PatSt_Bal_C Patst = new PatSt_Bal_C(Convert.ToString(Request.QueryString["PatRegID"]), Convert.ToString(Request.QueryString["FID"]), MTCode1, 0, Convert.ToInt32(Session["Branchid"]));
                    txtTestRemark.Text = Patst.P_ReportRemark;
                    ddldoctor.SelectedValue = Convert.ToString(Patst.AunticateSignatureId);
                    ddlTechnician.SelectedValue = Convert.ToString(Patst.Technicianid);
                    if (Patst.AunticateSignatureId > 0)
                    {
                        // Pagelloadcalc();
                    }

                    if (Convert.ToString(Session["usertype"]) == "Collection Center" || Convert.ToString(Session["usertype"]) == "CollectionCenter" || Convert.ToString(Session["usertype"]) == "Reporting" || Convert.ToString(Session["usertype"]) == "Main Doctor" || Convert.ToString(Session["usertype"]) == "Administrator" || Convert.ToString(Session["usertype"]) == "Technician")//Convert.ToString(Session["usertype"]) == "Technician" ||
                    {
                       // cb.Enabled = true;
                        cb.Enabled = false;
                        cb.BorderColor = System.Drawing.Color.Red;
                        cb.ForeColor = System.Drawing.Color.Green;
                        
                    }
                    else
                    {
                        cb.Enabled = false;
                    }
                    if (Convert.ToString(Session["usertype"]) == "Administrator")
                    {
                        if (Qc.Checked == true)
                        {
                            cb.Checked = true;
                            cb.BorderColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            cb.Enabled = false;
                        }
                    }

                    ImageButton ImgDescRes = GVAAresultEntrySub.Rows[i].FindControl("btndescresult") as ImageButton;
                    ImageButton btnImghelp = GVAAresultEntrySub.Rows[i].FindControl("Imghelp") as ImageButton;
                    TextBox txtTRF = GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox;
                    DropDownList DdlSForm = GVAAresultEntrySub.Rows[i].FindControl("DdlShortForm") as DropDownList;
                    string TName = GVAAresultEntrySub.Rows[i].Cells[0].Text;
                    string TT = (GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label).Text;
                    Label myLabel1 = (GVAAresultEntrySub.Rows[i].FindControl("lbltexdes") as Label);
                    Label mysubdept = (GVAAresultEntrySub.Rows[i].FindControl("lblsubdept") as Label);
                    ImageButton chkdes = (GVAAresultEntrySub.Rows[i].FindControl("ImgdescN") as ImageButton);
                    ImageButton ChkImgrerun = (GVAAresultEntrySub.Rows[i].FindControl("Imgrerun") as ImageButton);
                    TextBox txtEPR = GVAAresultEntrySub.Rows[i].FindControl("txt_EnterPrevResult") as TextBox;
                    Label LblTN = GVAAresultEntrySub.Rows[i].FindControl("lblTtestname") as Label;

                    if (lblQcCheck.Text == "True")
                    {
                        Qc.Checked = true;
                       // Qc.Enabled = false;
                    }
                    if (lblStatus.Text == "Authorized")
                    {
                        cb.Checked = true;
                        cb.Enabled = false;
                        Label LblMT = (GVAAresultEntrySub.Rows[i].FindControl("Lblmaintestname") as Label);


                        LblMT.ForeColor = System.Drawing.Color.Blue;

                       // if (Convert.ToString(Session["usertype"]) == "Technician")
                        if(Convert.ToString(Session["usertype"]) == "Main Doctor" || Convert.ToString(Session["usertype"]) == "Administrator")
                        {
                           
                            txtTRF.Enabled = true;
                            ChkImgrerun.Enabled = true;
                            DdlSForm.Enabled = true;
                        }
                        else
                        {
                            txtTRF.Enabled = false;
                            ChkImgrerun.Enabled = false ;
                            DdlSForm.Enabled = false;
                        }
                    }
                    if (myLabel1.Text == "TextField")
                    {
                        ImgDescRes.Visible = false;

                        txtTRF.Visible = true;
                        DdlSForm.Visible = true;
                        GVAAresultEntrySub.HeaderRow.Cells[2].Visible = true;
                        GVAAresultEntrySub.HeaderRow.Cells[3].Visible = true;

                    }
                    else
                    {
                        ImgDescRes.Visible = true;
                        btnImghelp.Visible = false;
                        txtTRF.Visible = false;
                        DdlSForm.Visible = false;
                        GVAAresultEntrySub.HeaderRow.Cells[3].Text = "";
                        GVAAresultEntrySub.HeaderRow.Cells[4].Text = "";
                        string NR = GVAAresultEntrySub.Rows[i].Cells[2].Text;


                    }


                    if (TT == "H")
                    {
                       // txtTRF.Text = "0";
                        txtTRF.Text = "";
                        txtTRF.Visible = false;
                        btnImghelp.Visible = false;
                        DdlSForm.Visible = false;

                    }
                    if (TT == "B")
                    {
                       // txtTRF.Visible = false;
                      //  txtTRF.Text = "0";
                        txtTRF.Text = "0";
                        txtTRF.Visible = false;
                        btnImghelp.Visible = false;
                        txtEPR.Visible = false;
                        LblTN.Visible = false;
                        DdlSForm.Visible = false;
                       // GVAAresultEntrySub.Rows[i].Width = "1px";
                        GVAAresultEntrySub.Rows[i].Visible = false;
                    }
                    if (mysubdept.Text == "MICROBIOLOGY")
                    {
                        //ImgDescRes.Visible = true;
                        chkdes.Visible = false;
                    }
                    else
                    {
                        chkdes.Visible = false;
                    }

                    if (i > 0)
                    {
                        Label Lblmaintestname_Main = GVAAresultEntrySub.Rows[i].FindControl("Lblmaintestname") as Label;
                        Label Lblmaintestname = GVAAresultEntrySub.Rows[i].FindControl("Lblmaintestname1") as Label;
                        Label Lblmaintestname1 = GVAAresultEntrySub.Rows[i - 1].FindControl("Lblmaintestname1") as Label;
                        DropDownList ddldoctor_Main = GVAAresultEntrySub.Rows[i].FindControl("ddldoctor") as DropDownList;
                        DropDownList ddlTechnician_Main = GVAAresultEntrySub.Rows[i].FindControl("ddlTechnician") as DropDownList;
                        CheckBox chkautho = GVAAresultEntrySub.Rows[i].FindControl("chkautho") as CheckBox;
                        TextBox txTestRemark = GVAAresultEntrySub.Rows[i].FindControl("txtTestRemark") as TextBox;
                        ImageButton Imgrep = GVAAresultEntrySub.Rows[i].FindControl("btnreport") as ImageButton;
                        ImageButton ChkImgre_run = (GVAAresultEntrySub.Rows[i].FindControl("Imgrerun") as ImageButton);
                        Button NORep1 = GVAAresultEntrySub.Rows[i].FindControl("NORep") as Button;
                        CheckBox chkQc = GVAAresultEntrySub.Rows[i].FindControl("chkQc") as CheckBox;
                        TextBox SampDate = GVAAresultEntrySub.Rows[i].FindControl("txtSampDate") as TextBox;
                            lblPrevResultDate.Text = "";
                       
                        if (Lblmaintestname.Text == Lblmaintestname1.Text)
                        {
                            NORep1.Visible = false;
                            SampDate.Visible = false;
                            Lblmaintestname_Main.Text = "";
                            ddldoctor_Main.Visible = false;
                            ddlTechnician_Main.Visible = false;
                            chkautho.Visible = false;
                            chkQc.Visible = false;
                            txTestRemark.Visible = false;
                            Imgrep.Visible = false;
                            ChkImgre_run.Visible = false;
                        }
                        if (mysubdept.Text == "MICROBIOLOGY")
                        {

                            if (Lblmaintestname.Text == Lblmaintestname1.Text)
                            {
                                ImgDescRes.Visible = false;
                                chkdes.Visible = false;
                            }

                        }
                    }
                    int GCount = GVAAresultEntrySub.Rows.Count;
                    if (GCount - 1 == i)
                    {
                        //  Autocalaulate();
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
            // Pagelloadcalc();
        }
        // checkabnormal();


    }

    public void InsertUpdate()
    {
        Stformmst_Bal_C Obj_SBC = new Stformmst_Bal_C();
        Obj_SBC.MTCode = "";
        Obj_SBC.PatRegID = Convert.ToString(lblRegNo.Text);
        if (Convert.ToString(Request.QueryString["FID"]) != null)
        {
            Obj_SBC.FID = Convert.ToString(Request.QueryString["FID"]);
            Obj_SBC.FinancialYearID = ViewState["Fyearid"].ToString();
        }
        else
        {
            Obj_SBC.FID = Convert.ToString(ViewState["FID"]);
            Obj_SBC.FinancialYearID = Convert.ToString(ViewState["FID"]);
        }
        Obj_SBC.Branchid = Convert.ToInt32(Session["Branchid"]);
        if (Session["UnitCode"] != null)
        {
            Obj_SBC.UnitCode = Session["UnitCode"].ToString();
        }
        else
        {
            Obj_SBC.UnitCode = "";
        }
        Obj_SBC.InsertUpdate_Result();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        //lblRegNo.Text = Request.QueryString["PatRegID"].ToString();

    }

    public void Fill_Labels()
    {
        #region  Patient info
        Patmst_Bal_C Patmst = null;
        try
        {
            if (Convert.ToString(Request.QueryString["PatRegID"]) != null)
            {
                Patmst = new Patmst_Bal_C(Convert.ToString(Request.QueryString["PatRegID"]), Convert.ToString(Request.QueryString["FID"]), Convert.ToInt32(Session["Branchid"]));

                lblRegNo.Text = Convert.ToString(Patmst.PatRegID);
                ViewState["PID"] = Patmst.PID;
                lblName.Text = Patmst.Initial.Trim() + "." + Patmst.Patname;
                lblage.Text = Convert.ToString(Patmst.DOB) + " / " +  Convert.ToString(Patmst.Age) + " / " + Patmst.MYD;
                lblSex.Text = Patmst.Sex;
                LblMobileno.Text = Patmst.Phone;
                Lblcenter.Text = Patmst.CenterName;
                lbldate.Text = Convert.ToString(Patmst.Patregdate1);
                LblRefDoc.Text = Patmst.Drname;

                lblweight.Text = Convert.ToString(Patmst.P_Weights);

                lblheight.Text = Patmst.P_Heights;
                lbldieses.Text = Convert.ToString(Patmst.P_Disease);
                lbllastperiod.Text = Patmst.P_LastPeriod;
                lblfstime.Text = Patmst.P_FSTime;
                lbltherpay.Text = Patmst.P_Therapy;

                lblsymptoms.Text = Patmst.P_Symptoms;

                lblPatusername.Text = Patmst.P_Patusername;

                ViewState["PPID"] = Patmst.P_PPID;
                Label4.Text = Patmst.PatientcHistory;
            }
            else
            {
                ViewState["PPID"] = 0;
            }
        }
        catch
        {
            lblRegNo.Visible = true;
            ViewState["PPID"] = 0;
            lblRegNo.Text = "Record not found";
        }
        #endregion
    }

    public void Fill_LabelsNew()
    {
        #region  Patient info
        Patmst_Bal_C Patmst = null;
        try
        {

            Patmst = new Patmst_Bal_C(txtregno.Text, Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]));

            lblRegNo.Text = Convert.ToString(Patmst.PatRegID);
            ViewState["PID"] = Patmst.PID;
            lblName.Text = Patmst.Initial.Trim() + "." + Patmst.Patname;
            lblage.Text = Convert.ToString(Patmst.Age) + "/" + Patmst.MYD;
            lblSex.Text = Patmst.Sex;
            LblMobileno.Text = Patmst.Phone;
            Lblcenter.Text = Patmst.CenterName;
            lbldate.Text = Convert.ToString(Patmst.Patregdate);
            LblRefDoc.Text = Patmst.Drname;

            lblweight.Text = Convert.ToString(Patmst.P_Weights);

            lblheight.Text = Patmst.P_Heights;
            lbldieses.Text = Convert.ToString(Patmst.P_Disease);
            lbllastperiod.Text = Patmst.P_LastPeriod;
            lblfstime.Text = Patmst.P_FSTime;
            lbltherpay.Text = Patmst.P_Therapy;

            lblsymptoms.Text = Patmst.P_Symptoms;
            ViewState["PPID"] = Patmst.P_PPID;

        }
        catch
        {
            ViewState["PPID"] = 0;
            lblRegNo.Visible = true;
            lblRegNo.Text = "Record not found";
        }
        #endregion
    }

    public void Fill_Demography()
    {
        #region  Patient info
        Patmst_Bal_C Patmst = null;
        try
        {

            Patmst = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), 1);

            lblweight.Text = Convert.ToString(Patmst.P_Weights);

            lblheight.Text = Patmst.P_Heights;
            lbldieses.Text = Convert.ToString(Patmst.P_Disease);
            lbllastperiod.Text = Patmst.P_LastPeriod;
            lblfstime.Text = Patmst.P_FSTime;
            lbltherpay.Text = Patmst.P_Therapy;

            lblsymptoms.Text = Patmst.P_Symptoms;

        }
        catch
        {
            lblRegNo.Visible = true;
            lblRegNo.Text = "Record not found";
        }
        #endregion
    }


   
    protected void btnSaveAll_Click(object sender, EventArgs e1)
    {
        bool isDocSel = false;
        PatSt_Bal_C ObjPBC = new PatSt_Bal_C();
        DataTable dttechid = new DataTable();
        bool CheckAuthoType = false;
        for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
        {
            CheckBox ch = (GVAAresultEntrySub.Rows[i].FindControl("chkautho") as CheckBox);
            Label Lblmaintestname = (GVAAresultEntrySub.Rows[i].FindControl("Lblmaintestname") as Label);
            Label myLabel1 = (GVAAresultEntrySub.Rows[i].FindControl("lbltexdes") as Label);
            if (Lblmaintestname.Text != "")
            {
                CheckAuthoType = false;
            }
            TextBox txtSampDate = (GVAAresultEntrySub.Rows[i].FindControl("txtSampDate") as TextBox);
            ObjPBC.PatRegID = Request.QueryString["PatRegID"];
            ObjPBC.MTCode = Convert.ToString((GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label).Text);
            bool CheckPrintRep;
            CheckPrintRep = ObjPBC.Check_PrintedReport(ObjPBC.PatRegID, Request.QueryString["FID"], ObjPBC.MTCode, 1);

            if (myLabel1.Text != "DescType")
            {
                if (Lblmaintestname.Text != "")
                {
                    dt = new DataTable();
                    dt = ObjPBC.Check_Test_Status(Request.QueryString["PatRegID"], ObjPBC.MTCode, 1);
                    if (dt.Rows.Count > 0)
                    {
                        // ObjPBC.Patauthicante = Convert.ToString(dt.Rows[0]["Patauthicante"]);
                        if (Convert.ToString(dt.Rows[0]["Patauthicante"]) == "Authorized")
                        {
                            ObjPBC.AbandantOn = Date.getdate();
                            ObjPBC.AbandantBy = Convert.ToString(Session["username"]);
                        }
                    }
                    else
                    {
                    }
                    bool bvalid = true;
                    bool bRemark = true;

                    Label l1 = (GVAAresultEntrySub.Rows[i].FindControl("LblErrMsg") as Label);

                    bool bTestsEntered = true;

                    bool atleastonetested = true;

                    if ((GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox).Text != "" && (GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox).Visible)
                        atleastonetested = true;

                    if ((GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox).Text == "" && (GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox).Visible)
                        bTestsEntered = false;



                    if (!bRemark)
                    {
                        l1.Text = "Please enter reason .";
                        return;
                    }
                    if (ch.Checked && ch.Enabled && !bvalid)
                    {
                        l1.Text = "Please enter test results.";
                        return;
                    }

                    string PatRegID = Request.QueryString["PatRegID"].ToString();


                    string lbl = (GVAAresultEntrySub.Rows[i].FindControl("lblSDCode") as Label).Text;
                    if ((bTestsEntered) && ((Session["usertype"].ToString() == "Main Doctor") || (Session["usertype"].ToString() == "Administrator") || (Session["usertype"].ToString() == "Admin") || (Session["usertype"].ToString() == "Super Admin")))
                    {
                        ObjPBC.TestUser = Session["username"].ToString();
                        ObjPBC.TempTestUser = null;
                    }
                    else
                    {
                        ObjPBC.TestUser = null;
                        ObjPBC.TempTestUser = Session["username"].ToString();
                    }

                    ObjPBC.Testdate = Date.getdate();
                    ObjPBC.SampleDate = Convert.ToDateTime( txtSampDate.Text);
                    if (CheckPrintRep == false)
                    {
                        ObjPBC.Patauthicante = "Tested";
                    }
                    else
                    {
                        ObjPBC.Patauthicante = "Authorized";
                    }

                    dttechid = ObjPBC.GetallTechnican(Convert.ToString(Session["username"]), Convert.ToString(Session["usertype"]));
                    if (dttechid.Rows.Count > 0)
                    {
                        ObjPBC.TechnicanFirst = Convert.ToInt32(dttechid.Rows[0]["DRid"]);
                    }
                    if (ch.Checked && atleastonetested)
                    {
                        isDocSel = false;

                        isDocSel = true;
                        (GVAAresultEntrySub.Rows[i].FindControl("lblsign") as Label).Text = (GVAAresultEntrySub.Rows[i].FindControl("ddldoctor") as DropDownList).SelectedValue;

                        if (isDocSel)
                        {
                            ObjPBC.Patauthicante = "Authorized";
                            status = true;
                        }
                        else
                        {
                            lblSelectDocError.Text = "select doctor .";
                            return;
                        }
                        ObjPBC.AunticateSignatureId = Convert.ToInt32((GVAAresultEntrySub.Rows[i].FindControl("lblsign") as Label).Text);
                        DropDownList ddltech = (GVAAresultEntrySub.Rows[i].FindControl("ddlTechnician") as DropDownList);
                        if (dttechid.Rows.Count > 0)
                        {
                            ObjPBC.TechnicanSecond = Convert.ToInt32(dttechid.Rows[0]["DRid"]);
                        }
                        else
                        {
                            ObjPBC.TechnicanSecond = 0;
                        }
                        if (ddltech.SelectedIndex > 0)
                        {
                            ObjPBC.Technicianid = Convert.ToInt32((GVAAresultEntrySub.Rows[i].FindControl("ddlTechnician") as DropDownList).SelectedValue);
                        }
                        else
                        {
                            ObjPBC.Technicianid = 0;
                        }
                        ObjPBC.P_ReportRemark = Convert.ToString((GVAAresultEntrySub.Rows[i].FindControl("txtTestRemark") as TextBox).Text);
                        ObjPBC.ReportDate = Date.getdate();
                    }
                    else if (bTestsEntered)
                    {
                        ObjPBC.Patauthicante = "Tested";
                        ObjPBC.ReportDate = Date.getdate();
                    }

                    if ((bTestsEntered) && ((Session["usertype"].ToString() == "Main Doctor") || (Session["usertype"].ToString() == "Administrator") || (Session["usertype"].ToString() == "Admin") || (Session["usertype"].ToString() == "Super Admin")))
                    {
                        ObjPBC.TestUser = Session["username"].ToString();
                        ObjPBC.TempTestUser = null;
                    }
                    else
                    {
                        ObjPBC.TestUser = null;
                        ObjPBC.TempTestUser = Session["username"].ToString();

                    }

                    ObjPBC.FID = Request.QueryString["FID"];

                    ObjPBC.PatRegID = Request.QueryString["PatRegID"];

                    ObjPBC.MTCode = Convert.ToString((GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label).Text);

                    //ObjPBC.P_ReportRemark = Session["username"].ToString();
                    ObjPBC.P_ReportRemark = Convert.ToString((GVAAresultEntrySub.Rows[i].FindControl("txtTestRemark") as TextBox).Text);
                    ObjPBC.P_EntryUsername = Convert.ToString((GVAAresultEntrySub.Rows[i].FindControl("txtTestRemark") as TextBox).Text);

                    if (ch.Checked == true)
                    {
                        CheckAuthoType = true;
                    }
                }


                if ((GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox).Text != "")
                {

                    ObjPBC.ResultTemplate = (GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox).Text;

                    ObjPBC.STCODE = (GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label).Text;

                    TextBox ttr = (GVAAresultEntrySub.Rows[i].FindControl("txtRemarks") as TextBox);
                    if (ttr.Visible)
                    {
                        ObjPBC.statusAudit = "Test Modified";
                        ObjPBC.Reason = ttr.Text;
                    }
                    else
                    {
                        ObjPBC.statusAudit = "Tested";
                        ObjPBC.Reason = ttr.Text;
                    }
                    if ((ch.Checked && ch.Enabled))
                    {
                        ObjPBC.statusAudit = "Authorized";
                        if (ttr.Visible)
                            ObjPBC.Reason = ttr.Text;
                        else
                            ObjPBC.Reason = "";
                    }

                    DataTable dtexistaut = new DataTable();
                    dtexistaut = Obj_Patst.Check_Authorised_Test(lblRegNo.Text, ObjPBC.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                    if (dtexistaut.Rows[0]["Technicanby"] != "")
                    {
                        ObjPBC.P_Technicanby = Convert.ToString(dtexistaut.Rows[0]["Technicanby"]);
                    }
                    else
                    {

                        ObjPBC.P_Technicanby = Session["username"].ToString();
                    }
                    ObjPBC.P_PanicResult = Convert.ToInt32(ViewState["PanicResult"]);
                    if (CheckAuthoType == true)
                    {
                        ObjPBC.statusAudit = "Authorized";
                        ObjPBC.Patauthicante = "Authorized";
                    }
                    int kk = (GVAAresultEntrySub.Rows.Count);
                    if (Convert.ToString(Request.QueryString["Amend"]) == "NO")
                    {
                        ObjPBC.InsertUpdate_AddResult_WithoutAmend(Convert.ToInt32(Session["Branchid"]), kk);
                    }
                    else
                    {
                        ObjPBC.InsertUpdate_AddResult(Convert.ToInt32(Session["Branchid"]), kk);
                    }
                }
                else
                {

                    string FID = Request.QueryString["FID"];

                    string PatRegID = lblRegNo.Text;

                    string MT_Code = ObjPBC.MTCode;

                    string _TestCode = (GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label).Text;

                    ObjPBC.ResultTemplate = (GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox).Text;

                    ObjPBC.STCODE = (GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label).Text;

                    TextBox ttr = (GVAAresultEntrySub.Rows[i].FindControl("txtRemarks") as TextBox);
                    bool check_exist;
                    check_exist = ObjPBC.Check_existstestresult(PatRegID, FID, MT_Code, _TestCode, Convert.ToInt32(Session["Branchid"]));
                    if (check_exist == true)
                    {
                        if (ttr.Visible)
                        {
                            ObjPBC.statusAudit = "Test Modified";
                            ObjPBC.Reason = ttr.Text;
                        }
                        else
                        {
                            ObjPBC.statusAudit = "Tested";
                            ObjPBC.statusAudit = "Registered";
                            //ObjPBC.Patauthicante = "Registered";
                            ObjPBC.Reason = ttr.Text;
                        }
                        dt = new DataTable();
                        dt = ObjPBC.Check_Test_Status(lblRegNo.Text, ObjPBC.MTCode, Convert.ToInt32(Session["Branchid"]), FID);
                        if (dt.Rows.Count > 0)
                        {
                            ObjPBC.Patauthicante = Convert.ToString(dt.Rows[0]["Patauthicante"]);
                        }
                        if ((ch.Checked && ch.Enabled))//&& ch.Enabled
                        {
                            ObjPBC.statusAudit = "Authorized";
                            ObjPBC.Patauthicante = "Authorized";
                            if (ttr.Visible)
                                ObjPBC.Reason = ttr.Text;
                            else
                                ObjPBC.Reason = "";
                        }
                        DataTable dtexistaut = new DataTable();
                        dtexistaut = Obj_Patst.Check_Authorised_Test(lblRegNo.Text, ObjPBC.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                        if (dtexistaut.Rows[0]["Technicanby"] != "")
                        {
                            ObjPBC.P_Technicanby = Convert.ToString(dtexistaut.Rows[0]["Technicanby"]);
                        }
                        else
                        {

                            ObjPBC.P_Technicanby = Session["username"].ToString();
                        }
                        // ObjPBC.P_Technicanby = Session["username"].ToString();
                        ObjPBC.P_PanicResult = Convert.ToInt32(ViewState["PanicResult"]);
                        int kk = (GVAAresultEntrySub.Rows.Count);
                       // if ((GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox).Text != "")
                       // {
                            if (CheckAuthoType == true)
                            {
                                ObjPBC.statusAudit = "Authorized";
                                ObjPBC.Patauthicante = "Authorized";
                            }
                            if (Convert.ToString(Request.QueryString["Amend"]) == "NO")
                            {
                                ObjPBC.InsertUpdate_AddResult_WithoutAmend(Convert.ToInt32(Session["Branchid"]), kk);
                            }
                            else
                            {
                                ObjPBC.InsertUpdate_AddResult(Convert.ToInt32(Session["Branchid"]), kk);
                            }
                        //}
                       
                    }
                }
                DataTable dtout = new DataTable();
                dtout = ObjPBC.Check_OutsourcePatientPID(lblRegNo.Text, ObjPBC.MTCode, Convert.ToInt32(Session["Branchid"]));
                if (Convert.ToString(dtout.Rows[0]["OutsourcePatientPID"]) != "0")
                {
                    ObjPBC.InsertUpdate_AddResult_outsource1(Convert.ToInt32(Session["Branchid"]), 0);
                }
                // }
            }
        }
        ObjTB.P_Patregno = Convert.ToString(lblRegNo.Text); ;
        ObjTB.P_FormName = "Add Result";
        ObjTB.P_EventName = "Result Save";
        ObjTB.P_UserName = Convert.ToString(Session["username"]);
        ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
        ObjTB.Insert_DailyActivity();
        Label3.Text = "Result save successfully.";
        sendSMSRegistration();
        if (Convert.ToString(ViewState["Reverse"]) != "False")
        {
            //Server.Transfer("~/Testresultentry.aspx");
        }

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

    public string makerequest(string url)
    {
        StreamWriter mywriter = null;
        string result = "";
        string strpost = "";
        HttpWebRequest objrequest = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            objrequest.Method = "POST";
            objrequest.ContentLength = (long)strpost.Length;
            objrequest.ContentType = "text/html; charset=utf-8";

            mywriter = new StreamWriter(objrequest.GetRequestStream());
            mywriter.Write(strpost);

            HttpWebResponse objresponse = (HttpWebResponse)objrequest.GetResponse();
            using (StreamReader sr = new StreamReader(objresponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
        }
        catch (Exception)
        { }
        finally
        {
            mywriter.Close();
        }
        return (result);
    }

    public void checkabnormal()
    {
    }

    public int priority(char ch)
    {
        switch (ch)
        {
            case '(': return 0;
            case '+':
            case '-': return 1;
            case '/':
            case '*': return 2;
            case '^': return 3;
        }
        return 0;
    }

  

    protected void btnreport_Click(object sender, EventArgs e)
    {
        if (ViewState["btnsave"].ToString() != "true")
        {
            ViewState["sampleflag"] = "false";
            ViewState["Reverse"] = "False";
            this.btnSaveAll_Click(btnSaveAll, null);
        }
        int PID = Convert.ToInt32(ViewState["PID"]);
        ViewState["btnsave"] = "";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'TestReportprinting.aspx?PatRegID=" + lblRegNo.Text + "&FID=" + Request.QueryString["FID"] + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

    }
   

   


    protected void GVAAresultEntrySub_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex == -1)
                return;

            bool bvalid = true; string rangeflag = "";
            if (e.Row.Cells[7].Text.Trim() != "" && e.Row.Cells[7].Text.Trim() != "&nbsp;")
            {
                TextBox EnterResult = (e.Row.FindControl("txt_EnterResult") as TextBox);
                Label LblMT_Code = (e.Row.FindControl("lbl_MT_Code") as Label);

                string NRange = (e.Row.FindControl("lblNormalRange1") as Label).Text.Trim();
                string[] NValue = NRange.Split('-');
                if (EnterResult.Text != "")
                {
                    double num;
                    //if (double.TryParse(EnterResult.Text, out num))
                    //{
                    //    if (Convert.ToSingle(EnterResult.Text) < Convert.ToSingle(NValue[0]))
                    //    {

                    //        EnterResult.ForeColor = System.Drawing.Color.Red;
                    //    }
                    //    if (Convert.ToSingle(EnterResult.Text) > Convert.ToSingle(NValue[1]))
                    //    {
                    //        EnterResult.ForeColor = System.Drawing.Color.Red;
                    //    }
                    //}


                    TextBox t = e.Row.FindControl("txt_EnterResult") as TextBox;
                    Label l2 = e.Row.FindControl("lblNormalRange") as Label;
                    Label l3 = e.Row.FindControl("lblPanicRange") as Label;
                    Label LBLTN = e.Row.FindControl("lblTtestname") as Label;

                    TextBox txtFlag = e.Row.FindControl("txtrange") as TextBox;
                    if ((t).Text == "" && t.Visible)
                    {
                        bvalid = false;
                    }

                    if (bvalid && t.Text != "")
                    {

                        if (l2.Text.Trim() != "")
                        {
                            if (l2.Text.Trim().Substring(0, 1) == ">")
                            {
                                try
                                {
                                    float txtVal = float.Parse(t.Text);
                                    if (txtVal <= float.Parse(l2.Text.Trim().Substring(1)))
                                    {
                                        t.BackColor = System.Drawing.Color.Orange;
                                        // t.BackColor = System.Drawing.Color.Red;
                                        EnterResult.BackColor = System.Drawing.Color.Orange;
                                        LBLTN.BackColor = System.Drawing.Color.Orange;

                                        rangeflag = "L";
                                        txtFlag.Text = "L";
                                    }
                                    else
                                    {
                                        t.ForeColor = System.Drawing.Color.Black;
                                        // LBLTN.BackColor = System.Drawing.Color.Red;
                                        // t.BackColor = System.Drawing.Color.Black;
                                        rangeflag = "N";
                                        txtFlag.Text = "N";
                                    }
                                }
                                catch (Exception exc)
                                {
                                    t.BackColor = System.Drawing.Color.Orange;
                                    LBLTN.BackColor = System.Drawing.Color.Orange;
                                    // t.BackColor = System.Drawing.Color.Red;
                                    EnterResult.BackColor = System.Drawing.Color.Orange;
                                    // continue;
                                }
                            }
                            else if (l2.Text.Trim().Substring(0, 1) == "<")
                            {
                                try
                                {
                                    float txtVal = float.Parse(t.Text);
                                    if (txtVal >= float.Parse(l2.Text.Trim().Substring(1)))
                                    {
                                        t.BackColor = System.Drawing.Color.Orange;
                                        //t.BackColor = System.Drawing.Color.Red;
                                        EnterResult.BackColor = System.Drawing.Color.Orange;
                                        LBLTN.BackColor = System.Drawing.Color.Orange;
                                        rangeflag = "H";
                                        txtFlag.Text = "H";
                                    }
                                    else
                                    {
                                        //  t.ForeColor = System.Drawing.Color.Black;
                                        t.BackColor = System.Drawing.Color.Orange;
                                        //EnterResult.ForeColor = System.Drawing.Color.Red;
                                        // LBLTN.BackColor = System.Drawing.Color.Red;
                                        rangeflag = "N";
                                        txtFlag.Text = "N";
                                    }
                                }
                                catch (Exception exc)
                                {
                                    t.BackColor = System.Drawing.Color.Orange;
                                    // t.BackColor = System.Drawing.Color.Red;
                                    EnterResult.BackColor = System.Drawing.Color.Orange;
                                    LBLTN.BackColor = System.Drawing.Color.Orange;
                                    // continue;
                                }
                            }
                            else if (char.Parse(l2.Text.Trim().Substring(0, 1)) >= '0' && char.Parse(l2.Text.Trim().Substring(0, 1)) <= '9')
                            {
                                try
                                {
                                    float txtVal = float.Parse(t.Text);
                                    if (!(txtVal >= float.Parse(l2.Text.Trim().Split('-')[0]) && txtVal <= float.Parse(l2.Text.Trim().Split('-')[1])))
                                    {
                                        if (!(txtVal >= float.Parse(l2.Text.Trim().Split('-')[0])))
                                        {
                                            t.BackColor = System.Drawing.Color.Orange;
                                            // t.BackColor = System.Drawing.Color.Red;
                                            EnterResult.BackColor = System.Drawing.Color.Orange;
                                            LBLTN.BackColor = System.Drawing.Color.Orange;
                                            rangeflag = "L";
                                            txtFlag.Text = "L";
                                        }
                                        else
                                        {
                                            t.BackColor = System.Drawing.Color.Orange;
                                            //t.BackColor = System.Drawing.Color.Red;
                                            EnterResult.BackColor = System.Drawing.Color.Orange;
                                            LBLTN.BackColor = System.Drawing.Color.Orange;
                                            rangeflag = "H";
                                            txtFlag.Text = "H";
                                        }
                                    }
                                    else
                                    {
                                        t.ForeColor = System.Drawing.Color.Black;

                                        //t.BackColor = System.Drawing.Color.Black;
                                        rangeflag = "N";
                                        txtFlag.Text = "N";

                                    }

                                }
                                catch (Exception exc)
                                {
                                    t.BackColor = System.Drawing.Color.Orange;
                                    // t.BackColor = System.Drawing.Color.Red;
                                    EnterResult.BackColor = System.Drawing.Color.Orange;
                                    LBLTN.BackColor = System.Drawing.Color.Orange;
                                    // continue;
                                }
                            }
                        }
                        // =============================Panic Value======================================

                        if (l3.Text.Trim() != "")
                        {
                            if (l3.Text.Trim().Substring(0, 1) == ">")
                            {
                                try
                                {
                                    float txtVal = float.Parse(t.Text);
                                    if (txtVal <= float.Parse(l3.Text.Trim().Substring(1)))
                                    {
                                        t.BackColor = System.Drawing.Color.Red;
                                        // t.BackColor = System.Drawing.Color.Red;
                                        EnterResult.BackColor = System.Drawing.Color.Red;
                                        LBLTN.BackColor = System.Drawing.Color.Red;
                                        ViewState["PanicResult"] = 1;
                                        rangeflag = "L";
                                        txtFlag.Text = "L";
                                    }
                                    else
                                    {
                                        t.ForeColor = System.Drawing.Color.Black;
                                        // LBLTN.BackColor = System.Drawing.Color.Red;
                                        // t.BackColor = System.Drawing.Color.Black;
                                        rangeflag = "N";
                                        txtFlag.Text = "N";
                                    }
                                }
                                catch (Exception exc)
                                {
                                    t.BackColor = System.Drawing.Color.Red;
                                    LBLTN.BackColor = System.Drawing.Color.Red;
                                    // t.BackColor = System.Drawing.Color.Red;
                                    EnterResult.BackColor = System.Drawing.Color.Red;
                                    ViewState["PanicResult"] = 1;
                                    // continue;
                                }
                            }
                            else if (l3.Text.Trim().Substring(0, 1) == "<")
                            {
                                try
                                {
                                    float txtVal = float.Parse(t.Text);
                                    if (txtVal >= float.Parse(l3.Text.Trim().Substring(1)))
                                    {
                                        t.BackColor = System.Drawing.Color.Red;
                                        //t.BackColor = System.Drawing.Color.Red;
                                        EnterResult.BackColor = System.Drawing.Color.Red;
                                        LBLTN.BackColor = System.Drawing.Color.Red;
                                        rangeflag = "H";
                                        txtFlag.Text = "H";
                                        ViewState["PanicResult"] = 1;
                                    }
                                    else
                                    {
                                        //  t.ForeColor = System.Drawing.Color.Black;
                                        t.BackColor = System.Drawing.Color.Red;
                                        //EnterResult.ForeColor = System.Drawing.Color.Red;
                                        // LBLTN.BackColor = System.Drawing.Color.Red;
                                        rangeflag = "N";
                                        txtFlag.Text = "N";
                                        //  ViewState["PanicResult"] = 1;
                                    }
                                }
                                catch (Exception exc)
                                {
                                    t.BackColor = System.Drawing.Color.Red;
                                    // t.BackColor = System.Drawing.Color.Red;
                                    EnterResult.BackColor = System.Drawing.Color.Red;
                                    LBLTN.BackColor = System.Drawing.Color.Red;
                                    // continue;
                                    ViewState["PanicResult"] = 1;
                                }
                            }
                            else if (char.Parse(l3.Text.Trim().Substring(0, 1)) >= '0' && char.Parse(l3.Text.Trim().Substring(0, 1)) <= '9')
                            {
                                try
                                {
                                    float txtVal = float.Parse(t.Text);
                                    if (!(txtVal >= float.Parse(l3.Text.Trim().Split('-')[0]) && txtVal <= float.Parse(l3.Text.Trim().Split('-')[1])))
                                    {
                                        if (!(txtVal >= float.Parse(l3.Text.Trim().Split('-')[0])))
                                        {
                                            t.BackColor = System.Drawing.Color.Red;
                                            // t.BackColor = System.Drawing.Color.Red;
                                            EnterResult.BackColor = System.Drawing.Color.Red;
                                            LBLTN.BackColor = System.Drawing.Color.Red;
                                            rangeflag = "L";
                                            txtFlag.Text = "L";
                                            ViewState["PanicResult"] = 1;
                                        }
                                        else
                                        {
                                            t.BackColor = System.Drawing.Color.Red;
                                            //t.BackColor = System.Drawing.Color.Red;
                                            EnterResult.BackColor = System.Drawing.Color.Red;
                                            LBLTN.BackColor = System.Drawing.Color.Red;
                                            rangeflag = "H";
                                            txtFlag.Text = "H";
                                            ViewState["PanicResult"] = 1;
                                        }
                                    }
                                    else
                                    {
                                        t.ForeColor = System.Drawing.Color.Black;

                                        //t.BackColor = System.Drawing.Color.Black;
                                        rangeflag = "N";
                                        txtFlag.Text = "N";

                                    }

                                }
                                catch (Exception exc)
                                {
                                    t.BackColor = System.Drawing.Color.Red;
                                    // t.BackColor = System.Drawing.Color.Red;
                                    EnterResult.BackColor = System.Drawing.Color.Red;
                                    LBLTN.BackColor = System.Drawing.Color.Red;
                                    ViewState["PanicResult"] = 1;
                                    // continue;
                                }
                            }
                            PatSt_Bal_C ObjPBC = new PatSt_Bal_C();
                            ObjPBC.MTCode = LblMT_Code.Text;
                            ObjPBC.FID = Request.QueryString["FID"];

                            ObjPBC.PatRegID = lblRegNo.Text;

                            ObjPBC.P_PanicResult = Convert.ToInt32(ViewState["PanicResult"]);
                            if (Convert.ToInt32(ViewState["PanicResult"]) == 1)
                            {
                                ObjPBC.InsertUpdate_AddResult_Panic(Convert.ToInt32(Session["Branchid"]));
                            }
                            else
                            {
                                ObjPBC.P_PanicResult = Convert.ToInt32(0);
                                ObjPBC.InsertUpdate_AddResult_Panic(Convert.ToInt32(Session["Branchid"]));
                            }
                            ViewState["PanicResult"] = 0;
                        }
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
    protected void txt_EnterResult_TextChanged(object sender, EventArgs e)
    {
        // string testnames = "";
        // bool negetiveflag = false;
        string formula = "";
        // float finalresult = 0.0f;
        string STCODE = "";
        Stack operatorstack = new Stack();
        Stack resultstack = new Stack();
        int top = -1;
        object obj1 = null;
        object obj2 = null;
        object o = null;
        string sval1 = "";
        string sval2 = "";
        float testval1 = 0.0f;
        float testval2 = 0.0f;
        float resval = 0.0f;
        float txtresvalue = 0.0f;
        // bool f1 = false;
        // bool f = false;
        bool f2 = false;
        // int count = 0;
        // int testresult = 0;
        int j = 0;
        string poststring = "";
        bool formulareplaced = true;
        bool displayres = true;
        // string[] codesarray = null;
        // string codes = "";
        bool f3 = false;


        int RowIndex = 0;


        bool bvalid = true;
        bool bRemark = true;
        string rangeflag = "";
        for (int p = 0; p < GVAAresultEntrySub.Rows.Count; p++)
        {
            Label ltlc = (GVAAresultEntrySub.Rows[p].FindControl("lbl_MT_Code") as Label);
            RowIndex = ((GridViewRow)((TextBox)sender).NamingContainer).RowIndex;

            Label LblSTCODE = GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label;
            formula = TestFormulaLogic_Bal_c.getformula(LblSTCODE.Text, Convert.ToInt32(Session["Branchid"]));

            if (formula != "" && (GVAAresultEntrySub.Rows[p].FindControl("txt_EnterResult") as TextBox).Text == "")
            {

                STCODE = (GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label).Text;

                for (int p1 = 0; p1 < GVAAresultEntrySub.Rows.Count; p1++)
                {
                    ltlc = (GVAAresultEntrySub.Rows[p1].FindControl("lbl_MT_Code") as Label);

                    if (ltlc.Text != STCODE)
                    {
                        try
                        {

                            if ((GVAAresultEntrySub.Rows[p1].FindControl("txt_EnterResult") as TextBox).Text != "")
                            {
                                string TT = (GVAAresultEntrySub.Rows[p1].FindControl("lblTestCode") as Label).Text;
                                txtresvalue = Convert.ToSingle((GVAAresultEntrySub.Rows[p1].FindControl("txt_EnterResult") as TextBox).Text);
                                if ((GVAAresultEntrySub.Rows[p1].FindControl("lblTestCode") as Label).Text.Length != 1 && (GVAAresultEntrySub.Rows[p1].FindControl("lblTestCode") as Label).Text.Length != 2 && (GVAAresultEntrySub.Rows[p1].FindControl("lblTestCode") as Label).Text.Length != 3)
                                    formula = formula.Replace((GVAAresultEntrySub.Rows[p1].FindControl("lblTestCode") as Label).Text, txtresvalue.ToString());
                                f2 = false;
                            }
                            else
                            {
                                displayres = false;
                                txtresvalue = 0.0f;
                                f3 = false;

                            }

                            // ==========================Check abnormal=======================================

                            TextBox t = GVAAresultEntrySub.Rows[p1].FindControl("txt_EnterResult") as TextBox;
                            Label l2 = GVAAresultEntrySub.Rows[p1].FindControl("lblNormalRange") as Label;

                            TextBox txtFlag = GVAAresultEntrySub.Rows[p1].FindControl("txtrange") as TextBox;
                            if ((t).Text == "" && t.Visible)
                            {
                                bvalid = false;
                            }

                            if (bvalid && t.Text != "")
                            {

                                if (l2.Text.Trim() != "")
                                {
                                    if (l2.Text.Trim().Substring(0, 1) == ">")
                                    {
                                        try
                                        {
                                            float txtVal = float.Parse(t.Text);
                                            if (txtVal <= float.Parse(l2.Text.Trim().Substring(1)))
                                            {
                                                //t.ForeColor = System.Drawing.Color.Red;
                                                // t.BackColor = System.Drawing.Color.Red;
                                                GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                                rangeflag = "L";
                                                txtFlag.Text = "L";
                                            }
                                            else
                                            {
                                                t.ForeColor = System.Drawing.Color.Black;
                                                // t.BackColor = System.Drawing.Color.Black;
                                                rangeflag = "N";
                                                txtFlag.Text = "N";
                                            }
                                        }
                                        catch (Exception exc)
                                        {
                                            t.ForeColor = System.Drawing.Color.Red;
                                            // t.BackColor = System.Drawing.Color.Red;
                                            GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                            continue;
                                        }
                                    }
                                    else if (l2.Text.Trim().Substring(0, 1) == "<")
                                    {
                                        try
                                        {
                                            float txtVal = float.Parse(t.Text);
                                            if (txtVal >= float.Parse(l2.Text.Trim().Substring(1)))
                                            {
                                                // t.ForeColor = System.Drawing.Color.Red;
                                                //t.BackColor = System.Drawing.Color.Red;
                                                GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                                rangeflag = "H";
                                                txtFlag.Text = "H";
                                            }
                                            else
                                            {
                                                t.ForeColor = System.Drawing.Color.Black;
                                                // t.BackColor = System.Drawing.Color.Black;
                                                GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                                rangeflag = "N";
                                                txtFlag.Text = "N";
                                            }
                                        }
                                        catch (Exception exc)
                                        {
                                            //t.ForeColor = System.Drawing.Color.Red;
                                            // t.BackColor = System.Drawing.Color.Red;
                                            GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                            continue;
                                        }
                                    }
                                    else if (char.Parse(l2.Text.Trim().Substring(0, 1)) >= '0' && char.Parse(l2.Text.Trim().Substring(0, 1)) <= '9')
                                    {
                                        try
                                        {
                                            float txtVal = float.Parse(t.Text);
                                            if (!(txtVal >= float.Parse(l2.Text.Trim().Split('-')[0]) && txtVal <= float.Parse(l2.Text.Trim().Split('-')[1])))
                                            {
                                                if (!(txtVal >= float.Parse(l2.Text.Trim().Split('-')[0])))
                                                {
                                                    //  t.ForeColor = System.Drawing.Color.Red;
                                                    // t.BackColor = System.Drawing.Color.Red;
                                                    GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                                    rangeflag = "L";
                                                    txtFlag.Text = "L";
                                                }
                                                else
                                                {
                                                    // t.ForeColor = System.Drawing.Color.Red;
                                                    //t.BackColor = System.Drawing.Color.Red;
                                                    GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                                    rangeflag = "H";
                                                    txtFlag.Text = "H";
                                                }
                                            }
                                            else
                                            {
                                                t.ForeColor = System.Drawing.Color.Black;
                                                //t.BackColor = System.Drawing.Color.Black;
                                                rangeflag = "N";
                                                txtFlag.Text = "N";

                                            }

                                        }
                                        catch (Exception exc)
                                        {
                                            // t.ForeColor = System.Drawing.Color.Red;
                                            // t.BackColor = System.Drawing.Color.Red;
                                            GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                            continue;
                                        }
                                    }
                                }
                            }


                            // ========================== End Check abnormal ========================================
                        }
                        catch { }
                    }
                }//
                //   }//

                for (j = 0; j < formula.Length; j++)
                {
                    if (formula[j] != '+' && formula[j] != '-' && formula[j] != '/' && formula[j] != '*' && formula[j] != '(' && formula[j] != ')' && formula[j] != '^')
                    {
                        poststring += formula[j];
                    }
                    if (formula[j] == '(')
                    {
                        operatorstack.Push(formula[j]);
                        top++;
                    }
                    if (formula[j] == '+' || formula[j] == '-' || formula[j] == '/' || formula[j] == '*' || formula[j] == '^')
                    {
                        if (top == -1 || priority(formula[j]) > priority(Convert.ToChar(operatorstack.Peek())))
                        {
                            operatorstack.Push(formula[j]);
                            top++;
                        }
                        else
                        {
                            while (priority(formula[j]) <= priority(Convert.ToChar(operatorstack.Peek())) && top != -1)
                            {
                                poststring += Convert.ToString(operatorstack.Pop());
                                top--;
                            }
                            operatorstack.Push(formula[j]);
                            top++;
                        }
                    }
                    if (formula[j] == ')')
                    {
                        object obj = operatorstack.Pop();
                        top--;
                        while (top != -1 && Convert.ToChar(obj) != '(')
                        {
                            if (Convert.ToChar(obj) != '(')
                                poststring += Convert.ToString(obj);
                            if (top != -1)
                            {
                                obj = operatorstack.Pop();
                                top--;
                            }
                        }
                    }
                }
                if (j == formula.Length)
                {
                    if (top != -1)
                    {
                        while (top != -1)
                        {
                            if (operatorstack.Count != 0)
                            {
                                o = operatorstack.Pop();
                            }
                            if (Convert.ToChar(o) != '(')
                                poststring += Convert.ToString(o);
                            top--;
                        }
                    }
                }
                top = -1;
                string stval = "";
                j = 0;
                try
                {
                    while (j != poststring.Length)
                    {
                        if (poststring[j] != '}' && poststring[j] != '{' && poststring[j] != '+' && poststring[j] != '-' && poststring[j] != '*' && poststring[j] != '/' && poststring[j] != '^')
                        {
                            while (poststring[j] != '}')
                            {
                                if (poststring[j] != '(' || poststring[j] != ')' || poststring[j] != '}' || poststring[j] != '{')
                                {
                                    if (Convert.ToInt32(poststring[j]) >= 97 && Convert.ToInt32(poststring[j]) <= 123 || Convert.ToInt32(poststring[j]) >= 65 && Convert.ToInt32(poststring[j]) <= 91)
                                    {
                                        formulareplaced = false;
                                        break;
                                    }
                                    stval += poststring[j].ToString();
                                    if (j == poststring.Length)
                                    { continue; }
                                    else
                                    {
                                        j++;
                                    }
                                }
                            }
                            if (formulareplaced != false)
                            {
                                resultstack.Push(stval);
                                top++;
                                stval = "";
                            }
                        }
                        if (formulareplaced == false)
                            break;
                        try
                        {
                            if (poststring[j] == '+' || poststring[j] == '-' || poststring[j] == '*' || poststring[j] == '/' || poststring[j] == '^')
                            {
                                if (top != -1)
                                {
                                    if (resultstack.Count != 0)
                                    {
                                        obj2 = resultstack.Pop();
                                        top--;
                                    }
                                    if (resultstack.Count != 0)
                                    {
                                        obj1 = resultstack.Pop();
                                        top--;
                                    }
                                }
                                sval1 = Convert.ToString(obj1);
                                sval2 = Convert.ToString(obj2);
                                try
                                {
                                    testval1 = Convert.ToSingle(Math.Round(Convert.ToDouble(sval1), 4));

                                    testval2 = Convert.ToSingle(Math.Round(Convert.ToDouble(sval2), 4));
                                }
                                catch (Exception exc)
                                {
                                }
                                if (poststring[j] == '+')
                                {
                                    resval = testval1 + testval2;
                                    resultstack.Push(resval);
                                    top++;
                                }
                                if (poststring[j] == '-')
                                {
                                    resval = testval1 - testval2;
                                    resultstack.Push(resval);
                                    top++;
                                }
                                if (poststring[j] == '*')
                                {
                                    resval = testval1 * testval2;
                                    resultstack.Push(resval);
                                    top++;
                                }
                                if (poststring[j] == '/')
                                {
                                    if (testval2 != 0f)
                                    {
                                        resval = testval1 / testval2;
                                        resultstack.Push(resval);
                                        top++;
                                    }
                                    else
                                    {
                                        while (top != -1)
                                        {
                                            if (resultstack.Count != 0)
                                                obj1 = resultstack.Pop();
                                            top--;
                                        }
                                        string close = "<script language='javascript'>javascript:divisionerror();</script>";
                                        Type title1 = this.GetType();
                                        Page.ClientScript.RegisterStartupScript(title1, "", close);
                                        return;
                                    }

                                }
                                if (poststring[j] == '^')
                                {
                                    resval = Convert.ToSingle(Math.Pow(Convert.ToDouble(testval1), Convert.ToDouble(testval2)));

                                    resultstack.Push(resval);
                                    top++;
                                }
                            }
                        }
                        catch (Exception exc) { }
                        j++;
                    }
                }

                catch (Exception exc) { }
                if (top == 0)
                    resval = Convert.ToSingle(resultstack.Pop());
                double finalres = Math.Round(Convert.ToDouble(resval), 2);

                bool flag = false;
                string txtshort = "";
                string lblTestCode1 = "";
                for (int k = 0; k < GVAAresultEntrySub.Rows.Count; k++)
                {
                    ltlc = (GVAAresultEntrySub.Rows[k].FindControl("lbl_MT_Code") as Label);
                    lblTestCode1 = (GVAAresultEntrySub.Rows[k].FindControl("lblTestCode") as Label).Text;

                    if ((GVAAresultEntrySub.Rows[k].FindControl("lblTestCode") as Label).Text == STCODE && formulareplaced == true)
                    {

                        (GVAAresultEntrySub.Rows[k].FindControl("txt_EnterResult") as TextBox).Text = finalres.ToString();

                        flag = true;
                        break;
                    }

                    txtshort = (GVAAresultEntrySub.Rows[k].FindControl("txt_EnterResult") as TextBox).Text;
                    if (txtshort != "")
                    {
                        if (char.Parse(txtshort.Trim().Substring(0, 1)) >= '0' && char.Parse(txtshort.Trim().Substring(0, 1)) <= '9')
                        {

                        }
                        else
                        {
                            SubTest_Bal_C Obj_SubTest = new SubTest_Bal_C();
                            int BRID = Convert.ToInt32(Session["Branchid"]);
                            (GVAAresultEntrySub.Rows[k].FindControl("txt_EnterResult") as TextBox).Text = Obj_SubTest.Filltxtresult(txtshort, BRID);
                        }
                    }
                    // =======================
                    if (RowIndex < GVAAresultEntrySub.Rows.Count - 1)
                    {
                        if (RowIndex == k)
                        {
                            lblTestCode1 = (GVAAresultEntrySub.Rows[k + 1].FindControl("lblTestCode") as Label).Text;
                            if (lblTestCode1 == "H")
                            {
                                (GVAAresultEntrySub.Rows[RowIndex + 2].FindControl("txt_EnterResult") as TextBox).Focus();
                            }
                            else
                            {
                                (GVAAresultEntrySub.Rows[RowIndex + 1].FindControl("txt_EnterResult") as TextBox).Focus();
                            }
                        }
                    }
                    //=============================
                    if (flag == true)
                        break;


                }


            } //////
            displayres = true;
            f2 = false;
            poststring = "";
            formulareplaced = true;

        }

    }

    protected void btndescresult_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
        {
            Label ltlc = (GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label);

            ImageButton DelOk = (ImageButton)GVAAresultEntrySub.Rows[i].FindControl("btndescresult");
            if (DelOk == (ImageButton)sender)
            {
                string STCODE = DelOk.CommandArgument;
                Label lblTestCode = (GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label);
                Label lblMTCodesub = (GVAAresultEntrySub.Rows[i].FindControl("lblMTCodesub") as Label);

                Label mysubdept = (GVAAresultEntrySub.Rows[i].FindControl("lblsubdept") as Label);

                Label lblsubdeptid1 = (GVAAresultEntrySub.Rows[i].FindControl("lblsubdeptid1") as Label);
                PatSt_Bal_C ObjPBC = new PatSt_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], lblMTCodesub.Text.Trim(), 0, Convert.ToInt32(Session["Branchid"]));
                string strSignID = ObjPBC.AunticateSignatureId.ToString();
                string SD_Code = ObjPBC.SDCode.ToString();

                if (Convert.ToString(Request.QueryString["formname"]) == "TestResultEntryCyto")
                {
                    Response.Redirect("~/TestCytoResult.aspx?subdeptid=" + lblsubdeptid1.Text + "&PatRegID=" + lblRegNo.Text + "&FID=" + Request.QueryString["FID"] + "&CenterCode=" + Convert.ToString(Request.QueryString["CenterCode"]) + "&MTCode=" + lblMTCodesub.Text.Trim() + "&Signatureid=" + strSignID + "&RepType=" + Request.QueryString["RepType"].ToString() + "&user=" + Session["usertype"].ToString(), false);
                }
                else if (Convert.ToString(Request.QueryString["formname"]) == "TestResultEntryHisto")
                {
                    Response.Redirect("~/TestHistoResult.aspx?subdeptid=" + lblsubdeptid1.Text + "&PatRegID=" + lblRegNo.Text + "&FID=" + Request.QueryString["FID"] + "&CenterCode=" + Convert.ToString(Request.QueryString["CenterCode"]) + "&MTCode=" + lblMTCodesub.Text.Trim() + "&Signatureid=" + strSignID + "&RepType=" + Request.QueryString["RepType"].ToString() + "&user=" + Session["usertype"].ToString(), false);
                }
                else if (Convert.ToString(Request.QueryString["formname"]) == "CytoReport")
                {
                    Response.Redirect("~/TestCytoResult.aspx?subdeptid=" + lblsubdeptid1.Text + "&PatRegID=" + lblRegNo.Text + "&FID=" + Request.QueryString["FID"] + "&CenterCode=" + Convert.ToString(Request.QueryString["CenterCode"]) + "&MTCode=" + lblMTCodesub.Text.Trim() + "&Signatureid=" + strSignID + "&RepType=" + Request.QueryString["RepType"].ToString() + "&user=" + Session["usertype"].ToString(), false);
                }
                else if (Convert.ToString(Request.QueryString["formname"]) == "HistoReport")
                {
                    Response.Redirect("~/TestHistoResult.aspx?subdeptid=" + lblsubdeptid1.Text + "&PatRegID=" + lblRegNo.Text + "&FID=" + Request.QueryString["FID"] + "&CenterCode=" + Convert.ToString(Request.QueryString["CenterCode"]) + "&MTCode=" + lblMTCodesub.Text.Trim() + "&Signatureid=" + strSignID + "&RepType=" + Request.QueryString["RepType"].ToString() + "&user=" + Session["usertype"].ToString(), false);
                }
                else
                {
                    Response.Redirect("~/TestDescriptiveResult.aspx?subdeptid=" + lblsubdeptid1.Text + "&PatRegID=" + lblRegNo.Text + "&FID=" + Request.QueryString["FID"] + "&CenterCode=" + Convert.ToString(Request.QueryString["CenterCode"]) + "&MTCode=" + lblMTCodesub.Text.Trim() + "&Signatureid=" + strSignID + "&RepType=" + Convert.ToString(Request.QueryString["RepType"]) + "&user=" + Session["usertype"].ToString() + "&subDeptName=" + mysubdept.Text + "&SDCODE=" +SD_Code, false);
                }
            }
            //}
        }

    }
    protected void GVAAresultEntrySub_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    public void Pagelloadcalc()
    {
        // string testnames = "";
        // bool negetiveflag = false;
        string formula = "";
        // float finalresult = 0.0f;
        string STCODE = "";
        Stack operatorstack = new Stack();
        Stack resultstack = new Stack();
        int top = -1;
        object obj1 = null;
        object obj2 = null;
        object o = null;
        string sval1 = "";
        string sval2 = "";
        float testval1 = 0.0f;
        float testval2 = 0.0f;
        float resval = 0.0f;
        float txtresvalue = 0.0f;
        bool f1 = false;
        bool f = false;
        bool f2 = false;
        int count = 0;
        //  int testresult = 0;
        int j = 0;
        string poststring = "";
        bool formulareplaced = true;
        bool displayres = true;
        // string[] codesarray = null;
        // string codes = "";
        bool f3 = false;


        bool bvalid = true;
        //bool bRemark = true;
        string rangeflag = "";
        for (int p = 0; p < GVAAresultEntrySub.Rows.Count; p++)
        {
            Label ltlc = (GVAAresultEntrySub.Rows[p].FindControl("lbl_MT_Code") as Label);

            Label LblSTCODE = GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label;
            formula = TestFormulaLogic_Bal_c.getformula(LblSTCODE.Text, Convert.ToInt32(Session["Branchid"]));

            if (formula != "")
            {

                STCODE = (GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label).Text;

                for (int p1 = 0; p1 < GVAAresultEntrySub.Rows.Count; p1++)
                {
                    ltlc = (GVAAresultEntrySub.Rows[p].FindControl("lbl_MT_Code") as Label);

                    if (ltlc.Text != STCODE)
                    {
                        try
                        {

                            if ((GVAAresultEntrySub.Rows[p].FindControl("txt_EnterResult") as TextBox).Text != "")
                            {
                                string TT = (GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label).Text;
                                txtresvalue = Convert.ToSingle((GVAAresultEntrySub.Rows[p].FindControl("txt_EnterResult") as TextBox).Text);
                                if ((GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label).Text.Length != 1 && (GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label).Text.Length != 2 && (GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label).Text.Length != 3)
                                    formula = formula.Replace((GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label).Text, txtresvalue.ToString());
                                f2 = false;
                            }
                            else
                            {
                                displayres = false;
                                txtresvalue = 0.0f;
                                f3 = false;

                            }

                            // ==========================Check abnormal=======================================




                            TextBox t = GVAAresultEntrySub.Rows[p].FindControl("txt_EnterResult") as TextBox;
                            Label l2 = GVAAresultEntrySub.Rows[p].FindControl("lblNormalRange") as Label;

                            TextBox txtFlag = GVAAresultEntrySub.Rows[p].FindControl("txtrange") as TextBox;
                            if ((t).Text == "" && t.Visible)
                            {
                                bvalid = false;
                            }

                            if (l2.Text.Trim() != "")
                            {
                                if (l2.Text.Trim().Substring(0, 1) == ">")
                                {
                                    try
                                    {
                                        float txtVal = float.Parse(t.Text);
                                        if (txtVal <= float.Parse(l2.Text.Trim().Substring(1)))
                                        {
                                            // t.ForeColor = System.Drawing.Color.Red;
                                            // t.BackColor = System.Drawing.Color.Red;
                                            GVAAresultEntrySub.Rows[p].Cells[1].BackColor = Color.Red;
                                            rangeflag = "L";
                                            txtFlag.Text = "L";
                                        }
                                        else
                                        {
                                            t.ForeColor = System.Drawing.Color.Black;
                                            // t.BackColor = System.Drawing.Color.Black;
                                            rangeflag = "N";
                                            txtFlag.Text = "N";
                                        }
                                    }
                                    catch (Exception exc)
                                    {
                                        // t.ForeColor = System.Drawing.Color.Red;
                                        // t.BackColor = System.Drawing.Color.Red;
                                        GVAAresultEntrySub.Rows[p].Cells[1].BackColor = Color.Red;
                                        continue;
                                    }
                                }
                                else if (l2.Text.Trim().Substring(0, 1) == "<")
                                {
                                    try
                                    {
                                        float txtVal = float.Parse(t.Text);
                                        if (txtVal >= float.Parse(l2.Text.Trim().Substring(1)))
                                        {
                                            // t.ForeColor = System.Drawing.Color.Red;
                                            //t.BackColor = System.Drawing.Color.Red;
                                            GVAAresultEntrySub.Rows[p].Cells[1].BackColor = Color.Red;
                                            rangeflag = "H";
                                            txtFlag.Text = "H";
                                        }
                                        else
                                        {
                                            t.ForeColor = System.Drawing.Color.Black;
                                            // t.BackColor = System.Drawing.Color.Black;
                                            GVAAresultEntrySub.Rows[p].Cells[1].BackColor = Color.Red;
                                            rangeflag = "N";
                                            txtFlag.Text = "N";
                                        }
                                    }
                                    catch (Exception exc)
                                    {
                                        //  t.ForeColor = System.Drawing.Color.Red;
                                        // t.BackColor = System.Drawing.Color.Red;
                                        GVAAresultEntrySub.Rows[p].Cells[1].BackColor = Color.Red;
                                        continue;
                                    }
                                }
                                else if (char.Parse(l2.Text.Trim().Substring(0, 1)) >= '0' && char.Parse(l2.Text.Trim().Substring(0, 1)) <= '9')
                                {
                                    try
                                    {
                                        float txtVal = float.Parse(t.Text);
                                        if (!(txtVal >= float.Parse(l2.Text.Trim().Split('-')[0]) && txtVal <= float.Parse(l2.Text.Trim().Split('-')[1])))
                                        {
                                            if (!(txtVal >= float.Parse(l2.Text.Trim().Split('-')[0])))
                                            {
                                                t.ForeColor = System.Drawing.Color.Red;
                                                // t.BackColor = System.Drawing.Color.Red;
                                                GVAAresultEntrySub.Rows[p].Cells[1].BackColor = Color.Red;
                                                rangeflag = "L";
                                                txtFlag.Text = "L";
                                            }
                                            else
                                            {
                                                // t.ForeColor = System.Drawing.Color.Red;
                                                //t.BackColor = System.Drawing.Color.Red;
                                                GVAAresultEntrySub.Rows[p].Cells[1].BackColor = Color.Red;
                                                rangeflag = "H";
                                                txtFlag.Text = "H";
                                            }
                                        }
                                        else
                                        {
                                            t.ForeColor = System.Drawing.Color.Black;
                                            //t.BackColor = System.Drawing.Color.Black;
                                            rangeflag = "N";
                                            txtFlag.Text = "N";

                                        }

                                    }
                                    catch (Exception exc)
                                    {

                                        continue;
                                    }
                                }
                            }



                            // ========================== End Check abnormal ========================================
                        }
                        catch { }
                    }
                } // p1


                for (j = 0; j < formula.Length; j++)
                {
                    if (formula[j] != '+' && formula[j] != '-' && formula[j] != '/' && formula[j] != '*' && formula[j] != '(' && formula[j] != ')' && formula[j] != '^')
                    {
                        poststring += formula[j];
                    }
                    if (formula[j] == '(')
                    {
                        operatorstack.Push(formula[j]);
                        top++;
                    }
                    if (formula[j] == '+' || formula[j] == '-' || formula[j] == '/' || formula[j] == '*' || formula[j] == '^')
                    {
                        if (top == -1 || priority(formula[j]) > priority(Convert.ToChar(operatorstack.Peek())))
                        {
                            operatorstack.Push(formula[j]);
                            top++;
                        }
                        else
                        {
                            while (priority(formula[j]) <= priority(Convert.ToChar(operatorstack.Peek())) && top != -1)
                            {
                                poststring += Convert.ToString(operatorstack.Pop());
                                top--;
                            }
                            operatorstack.Push(formula[j]);
                            top++;
                        }
                    }
                    if (formula[j] == ')')
                    {
                        object obj = operatorstack.Pop();
                        top--;
                        while (top != -1 && Convert.ToChar(obj) != '(')
                        {
                            if (Convert.ToChar(obj) != '(')
                                poststring += Convert.ToString(obj);
                            if (top != -1)
                            {
                                obj = operatorstack.Pop();
                                top--;
                            }
                        }
                    }
                }
                if (j == formula.Length)
                {
                    if (top != -1)
                    {
                        while (top != -1)
                        {
                            if (operatorstack.Count != 0)
                            {
                                o = operatorstack.Pop();
                            }
                            if (Convert.ToChar(o) != '(')
                                poststring += Convert.ToString(o);
                            top--;
                        }
                    }
                }
                top = -1;
                string stval = "";
                j = 0;
                try
                {
                    while (j != poststring.Length)
                    {
                        if (poststring[j] != '}' && poststring[j] != '{' && poststring[j] != '+' && poststring[j] != '-' && poststring[j] != '*' && poststring[j] != '/' && poststring[j] != '^')
                        {
                            while (poststring[j] != '}')
                            {
                                if (poststring[j] != '(' || poststring[j] != ')' || poststring[j] != '}' || poststring[j] != '{')
                                {
                                    if (Convert.ToInt32(poststring[j]) >= 97 && Convert.ToInt32(poststring[j]) <= 123 || Convert.ToInt32(poststring[j]) >= 65 && Convert.ToInt32(poststring[j]) <= 91)
                                    {
                                        formulareplaced = false;
                                        break;
                                    }
                                    stval += poststring[j].ToString();
                                    if (j == poststring.Length)
                                    { continue; }
                                    else
                                    {
                                        j++;
                                    }
                                }
                            }
                            if (formulareplaced != false)
                            {
                                resultstack.Push(stval);
                                top++;
                                stval = "";
                            }
                        }
                        if (formulareplaced == false)
                            break;
                        try
                        {
                            if (poststring[j] == '+' || poststring[j] == '-' || poststring[j] == '*' || poststring[j] == '/' || poststring[j] == '^')
                            {
                                if (top != -1)
                                {
                                    if (resultstack.Count != 0)
                                    {
                                        obj2 = resultstack.Pop();
                                        top--;
                                    }
                                    if (resultstack.Count != 0)
                                    {
                                        obj1 = resultstack.Pop();
                                        top--;
                                    }
                                }
                                sval1 = Convert.ToString(obj1);
                                sval2 = Convert.ToString(obj2);
                                try
                                {
                                    testval1 = Convert.ToSingle(Math.Round(Convert.ToDouble(sval1), 4));

                                    testval2 = Convert.ToSingle(Math.Round(Convert.ToDouble(sval2), 4));
                                }
                                catch (Exception exc)
                                {
                                }
                                if (poststring[j] == '+')
                                {
                                    resval = testval1 + testval2;
                                    resultstack.Push(resval);
                                    top++;
                                }
                                if (poststring[j] == '-')
                                {
                                    resval = testval1 - testval2;
                                    resultstack.Push(resval);
                                    top++;
                                }
                                if (poststring[j] == '*')
                                {
                                    resval = testval1 * testval2;
                                    resultstack.Push(resval);
                                    top++;
                                }
                                if (poststring[j] == '/')
                                {
                                    if (testval2 != 0f)
                                    {
                                        resval = testval1 / testval2;
                                        resultstack.Push(resval);
                                        top++;
                                    }
                                    else
                                    {
                                        while (top != -1)
                                        {
                                            if (resultstack.Count != 0)
                                                obj1 = resultstack.Pop();
                                            top--;
                                        }
                                        string close = "<script language='javascript'>javascript:divisionerror();</script>";
                                        Type title1 = this.GetType();
                                        Page.ClientScript.RegisterStartupScript(title1, "", close);
                                        return;
                                    }

                                }
                                if (poststring[j] == '^')
                                {
                                    resval = Convert.ToSingle(Math.Pow(Convert.ToDouble(testval1), Convert.ToDouble(testval2)));

                                    resultstack.Push(resval);
                                    top++;
                                }
                            }
                        }
                        catch (Exception exc) { }
                        j++;
                    }
                }

                catch (Exception exc) { }
                if (top == 0)
                    resval = Convert.ToSingle(resultstack.Pop());
                double finalres = Math.Round(Convert.ToDouble(resval), 2);

                bool flag = false;
                for (int k = 0; k < GVAAresultEntrySub.Rows.Count; k++)
                {
                    ltlc = (GVAAresultEntrySub.Rows[k].FindControl("lbl_MT_Code") as Label);


                    if ((GVAAresultEntrySub.Rows[k].FindControl("lblTestCode") as Label).Text == STCODE && formulareplaced == true)
                    {

                        (GVAAresultEntrySub.Rows[k].FindControl("txt_EnterResult") as TextBox).Text = finalres.ToString();

                        flag = true;
                        break;
                    }

                    if (flag == true)
                        break;


                }

            }
            displayres = true;
            f2 = false;
            poststring = "";
            formulareplaced = true;

        }

    }
    protected void btncalculate_Click(object sender, EventArgs e)
    {
        for (int p = 0; p < GVAAresultEntrySub.Rows.Count; p++)
        {
            Label ltlc = (GVAAresultEntrySub.Rows[p].FindControl("lbl_MT_Code") as Label);


            Label LblSTCODE = GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label;

            string lblTestCode11 = "";
            string txtshort1 = "";
            for (int k = 0; k < GVAAresultEntrySub.Rows.Count; k++)
            {
                ltlc = (GVAAresultEntrySub.Rows[k].FindControl("lbl_MT_Code") as Label);
                lblTestCode11 = (GVAAresultEntrySub.Rows[k].FindControl("lblTestCode") as Label).Text;



                txtshort1 = (GVAAresultEntrySub.Rows[k].FindControl("txt_EnterResult") as TextBox).Text;
                if (txtshort1 != "")
                {
                    if (k == 1)
                    {

                        Autocalaulate();
                    }

                    if (char.Parse(txtshort1.Trim().Substring(0, 1)) >= '0' && char.Parse(txtshort1.Trim().Substring(0, 1)) <= '9')
                    {

                    }
                    else
                    {
                        SubTest_Bal_C Obj_SubTest = new SubTest_Bal_C();
                        int BRID = Convert.ToInt32(Session["Branchid"]);
                        (GVAAresultEntrySub.Rows[k].FindControl("txt_EnterResult") as TextBox).Text = Obj_SubTest.Filltxtresult(txtshort1, BRID);
                        //GVAAresultEntrySub.Rows[k].FindControl("txt_EnterResult") as TextBox.Focus();
                        TextBox txtER = GVAAresultEntrySub.Rows[p].FindControl("txt_EnterResult") as TextBox;
                        if (txtER.Text != "")
                        {
                            txtER.Focus();
                        }
                    }
                }
            }


        }

    }
    protected void GVAAresultEntrySub_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnreport_Click1(object sender, ImageClickEventArgs e)
    {
        Cshmst_Bal_C Cshmst = new Cshmst_Bal_C();
        DataTable dtban = new DataTable();
        DataTable dtchk = new DataTable();
        dtban = ObjTB.Bindbanner();
        string ISCashbill = DrMT_sign_Bal_C.getcheckmonthlybill(Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]), Lblcenter.Text);
        if (ISCashbill == "False")
        {
            Session["Monthlybill"] = "YES";

        }
        else
        {
            Session["Monthlybill"] = "No";
        }

        if (dtban.Rows.Count > 0)
        {
            if (Convert.ToString(dtban.Rows[0]["BalanceMail"]) == "0")
            {
                ViewState["VALIDATE"] = "YES";

            }
            else
            {
                ViewState["VALIDATE"] = "NO";

            }
        }
        if (Convert.ToString(Session["Monthlybill"]) == "YES")
        {
            ViewState["VALIDATE"] = "YES";
        }
        if (Convert.ToString(Session["usertype"]) == "Administrator")
        {
            ViewState["VALIDATE"] = "YES";

        }
        if (Convert.ToString(Session["usertype"]) == "ReportView")
        {
            ViewState["VALIDATE"] = "YES";
        }
        Cshmst.getBalance(lblRegNo.Text, Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]),Convert.ToInt32( ViewState["PID"]));
        float BAL_Amount = Cshmst.Balance;
        if (ViewState["VALIDATE"] == "YES")
        {
            BAL_Amount = 0;
        }
        if (BAL_Amount > 0)
        {
           // Label6.Text = "Pending balance.";
            //Label6.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
        }
        else
        {
            string MTDummycode = "", subdept = "";
            for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
            {
                Label ltlc = (GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label);

                ImageButton DelOk = (ImageButton)GVAAresultEntrySub.Rows[i].FindControl("btnreport");
                if (DelOk == (ImageButton)sender)
                {
                    string STCODE = DelOk.CommandArgument;

                    Label lblTestCode = (GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label);
                    Label lblMTCodesub = (GVAAresultEntrySub.Rows[i].FindControl("lblMTCodesub") as Label);
                    Label lblsubdeptid1 = (GVAAresultEntrySub.Rows[i].FindControl("lblsubdeptid1") as Label);
                    MTDummycode = lblMTCodesub.Text;
                    Label mysubdept = (GVAAresultEntrySub.Rows[i].FindControl("lblsubdept") as Label);
                    subdept = mysubdept.Text;
                    DataTable dtexistaut = new DataTable();
                    PatSt_Bal_C Obj_Patst = new PatSt_Bal_C();
                    dtexistaut = Obj_Patst.Check_Authorised_Test(lblRegNo.Text, lblMTCodesub.Text, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                    if (dtexistaut.Rows.Count > 0)
                    {
                        if (Convert.ToString(dtexistaut.Rows[0]["AunticateSignatureId"]) == "0")
                        {
                            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1250/2);var Mtop = (screen.height/2)-(700/2);window.open( 'ReRunResult.aspx?MTCode=" + lblMTCodesub.Text + "&PatRegID=" + lblRegNo.Text.ToString() + "&Branchid=" + Session["Branchid"].ToString() + " ', null, 'height=700,width=1250,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                            string AA = "Please authorized Test.";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);


                            return;
                        }
                    }
                }
            }
            string MTCode = MTDummycode;
            MainTest_Bal_C Obj_MTB = new MainTest_Bal_C(MTCode, Convert.ToInt32(Session["Branchid"]));

            string TextDesc = Obj_MTB.TextDesc;


            string View_DescType = "";
            string ViewTestCode = "";

            PatSt_Bal_C Obj_Pat = new PatSt_Bal_C();
            Obj_Pat.UpdatePrintstatus(lblRegNo.Text.ToString(), Request.QueryString["FID"].ToString(), MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"].ToString()));

            if (TextDesc == "DescType")
            {

                if (View_DescType == "")
                {
                    View_DescType = " (VW_desfiledata.MTCode = '" + MTCode;
                }
                else
                {
                    View_DescType = View_DescType + "' OR VW_desfiledata.MTCode = '" + MTCode;
                }
            }
            else
                if (ViewTestCode == "")
                {

                    ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + MTCode;
                }
                else
                    ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + MTCode;

            bool DescFlag = false;
            bool textflag = false;
            if (View_DescType != "")
            {
                View_DescType = View_DescType + "')";
                AlterView_VE_GetLabNo(lblRegNo.Text);
                VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), View_DescType, lblRegNo.Text, Request.QueryString["FID"]);

                DescFlag = true;
            }

            if (ViewTestCode != "")
            {
                ViewTestCode = ViewTestCode + "')";
                AlterView_VE_GetLabNo(lblRegNo.Text);
                // ViewTestCode=
                VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, lblRegNo.Text, Request.QueryString["FID"]);


                textflag = true;
            }
            string[] targetArr;
            string[] urlArr;
            string[] featuresArr;
            if (textflag == true)
            {

                #region Only non desc

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();

                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + lblRegNo.Text.ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + lblRegNo.Text.ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "NonDesc" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + lblRegNo.Text.ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;

                int line = 10;
                //  pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);
                exportedpath = filename1;

                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + lblRegNo.Text.ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "NonDesc" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = " Report Not Generated.";
                    return;
                }
                string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + lblRegNo.Text.ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "NonDesc" + ".pdf");
                string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + lblRegNo.Text.ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "NonDesc" + ".pdf");

                string[] FilePathSplitOrg = OrgFile.Split('$');
                string[] FilePathSplitDup = DupFile.Split('$');

                if (FilePathSplitOrg[1] != FilePathSplitDup[1])
                {

                    foreach (string file in Directory.GetFiles(path))
                    {
                        string[] NewFile = file.Split('$');
                        if (FilePathSplitOrg[1] != NewFile[1])
                        {
                            File.Delete(file);
                        }
                    }
                }
                Response.Redirect("PrintReport//" + "$" + Date1 + "$" + lblRegNo.Text.ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "NonDesc" + ".pdf");
                //==============================================


                #endregion

            }
            else
            {


                //  =====================================================

                #region Only  desc

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();

                sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + lblRegNo.Text.ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + lblRegNo.Text.ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Desc" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + lblRegNo.Text.ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;

                int line = 10;
                pm.topMargin = 200 * line;
                CR.PrintOptions.ApplyPageMargins(pm);
                exportedpath = filename1;

                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + lblRegNo.Text.ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Desc" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = " Report Not Generated.";
                    return;
                }
                string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + lblRegNo.Text.ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Desc" + ".pdf");
                string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + lblRegNo.Text.ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Desc" + ".pdf");

                string[] FilePathSplitOrg = OrgFile.Split('$');
                string[] FilePathSplitDup = DupFile.Split('$');

                if (FilePathSplitOrg[1] != FilePathSplitDup[1])
                {


                    foreach (string file in Directory.GetFiles(path))
                    {
                        string[] NewFile = file.Split('$');
                        if (FilePathSplitOrg[1] != NewFile[1])
                        {
                            File.Delete(file);
                        }
                    }
                }

                Response.Redirect("PrintReport//" + "$" + Date1 + "$" + lblRegNo.Text.ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Desc" + ".pdf", false);

                #endregion
                //================================================  
            }
        }
    }
    protected void ImgdescN_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
        {
            Label ltlc = (GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label);

           
            ImageButton DelOk = (ImageButton)GVAAresultEntrySub.Rows[i].FindControl("ImgdescN");
            if (DelOk == (ImageButton)sender)
            {
                string STCODE = DelOk.CommandArgument;
                Label lblTestCode = (GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label);
                Label lblsubdeptid1 = (GVAAresultEntrySub.Rows[i].FindControl("lblsubdeptid1") as Label);
                Label lblMTCodesub = (GVAAresultEntrySub.Rows[i].FindControl("lblMTCodesub") as Label);

                PatSt_Bal_C PatSt = new PatSt_Bal_C();
                PatSt.MTCode = lblMTCodesub.Text;
                PatSt.InsertUpdateDESC_Nondesc(lblMTCodesub.Text);


            }
        }
    }
    public void AlterView_VE_GetLabNo(string PatRegID)
    {
        int i;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "ALTER VIEW [dbo].[VW_GetLabNo]AS (select  LabNo,PatRegID,MTCode,Branchid from patmstd  where  PatRegID='" + PatRegID + "' and branchid ='" + Convert.ToInt32(Session["Branchid"]) + "'  and  FID ='" + Convert.ToString(Request.QueryString["FID"]) + "' )";
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();

        }
        catch (Exception exx)
        { }
        finally
        {
            try
            {

                conn.Close();
                conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }

        }




    }

    protected void Imgrerun_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
        {
            Label ltlc = (GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label);

            ImageButton DelOk = (ImageButton)GVAAresultEntrySub.Rows[i].FindControl("Imgrerun");
            if (DelOk == (ImageButton)sender)
            {
                string STCODE = DelOk.CommandArgument;
                Label lblTestCode = (GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label);
                Label lblsubdeptid1 = (GVAAresultEntrySub.Rows[i].FindControl("lblsubdeptid1") as Label);
                Label lblMTCodesub = (GVAAresultEntrySub.Rows[i].FindControl("lblMTCodesub") as Label);

                PatSt_Bal_C PBC = new PatSt_Bal_C();
                PBC.MTCode = lblMTCodesub.Text;
                // PBC.InsertUpdateDESC_Nondesc(lblMTCodesub.Text);  lblRegNo.Text.ToString();

                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1250/2);var Mtop = (screen.height/2)-(700/2);window.open( 'ReRunResult.aspx?MTCode=" + lblMTCodesub.Text + "&PatRegID=" + Request.QueryString["PatRegID"].ToString() + "&Branchid=" + Session["Branchid"].ToString() + " ', null, 'height=700,width=1250,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

            }
        }
    }

    public void Autocalaulate()
    {

        string formula = "";

        string STCODE = "";
        Stack operatorstack = new Stack();
        Stack resultstack = new Stack();
        int top = -1;
        object obj1 = null;
        object obj2 = null;
        object o = null;
        string sval1 = "";
        string sval2 = "";
        float testval1 = 0.0f;
        float testval2 = 0.0f;
        float resval = 0.0f;
        float txtresvalue = 0.0f;
        bool f1 = false;
        bool f = false;
        bool f2 = false;
        int count = 0;

        int j = 0;
        string poststring = "";
        bool formulareplaced = true;
        bool displayres = true;

        bool f3 = false;

        int RowIndex = 0;


        bool bvalid = true;
        bool bRemark = true;
        string rangeflag = "";
        for (int p = 0; p < GVAAresultEntrySub.Rows.Count; p++)
        {
            Label ltlc = (GVAAresultEntrySub.Rows[p].FindControl("lbl_MT_Code") as Label);


            Label LblSTCODE = GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label;
            formula = TestFormulaLogic_Bal_c.getformula(LblSTCODE.Text, Convert.ToInt32(Session["Branchid"]));

            if (formula != "" && (GVAAresultEntrySub.Rows[p].FindControl("txt_EnterResult") as TextBox).Text == "")
            {
                STCODE = (GVAAresultEntrySub.Rows[p].FindControl("lblTestCode") as Label).Text;

                for (int p1 = 0; p1 < GVAAresultEntrySub.Rows.Count; p1++)
                {
                    ltlc = (GVAAresultEntrySub.Rows[p1].FindControl("lbl_MT_Code") as Label);

                    if (ltlc.Text != STCODE)
                    {
                        try
                        {

                            if ((GVAAresultEntrySub.Rows[p1].FindControl("txt_EnterResult") as TextBox).Text != "")
                            {
                                string TT = (GVAAresultEntrySub.Rows[p1].FindControl("lblTestCode") as Label).Text;
                                txtresvalue = Convert.ToSingle((GVAAresultEntrySub.Rows[p1].FindControl("txt_EnterResult") as TextBox).Text);
                                if ((GVAAresultEntrySub.Rows[p1].FindControl("lblTestCode") as Label).Text.Length != 1 && (GVAAresultEntrySub.Rows[p1].FindControl("lblTestCode") as Label).Text.Length != 2 && (GVAAresultEntrySub.Rows[p1].FindControl("lblTestCode") as Label).Text.Length != 3)
                                    formula = formula.Replace((GVAAresultEntrySub.Rows[p1].FindControl("lblTestCode") as Label).Text, txtresvalue.ToString());
                                f2 = false;
                            }
                            else
                            {
                                displayres = false;
                                txtresvalue = 0.0f;
                                f3 = false;

                            }

                            // ==========================Check abnormal=======================================




                            TextBox t = GVAAresultEntrySub.Rows[p1].FindControl("txt_EnterResult") as TextBox;
                            Label l2 = GVAAresultEntrySub.Rows[p1].FindControl("lblNormalRange") as Label;

                            TextBox txtFlag = GVAAresultEntrySub.Rows[p1].FindControl("txtrange") as TextBox;
                            if ((t).Text == "" && t.Visible)
                            {
                                bvalid = false;
                            }

                            if (bvalid && t.Text != "")
                            {

                                if (l2.Text.Trim() != "")
                                {
                                    if (l2.Text.Trim().Substring(0, 1) == ">")
                                    {
                                        try
                                        {
                                            float txtVal = float.Parse(t.Text);
                                            if (txtVal <= float.Parse(l2.Text.Trim().Substring(1)))
                                            {
                                                // t.ForeColor = System.Drawing.Color.Red;
                                                // t.BackColor = System.Drawing.Color.Red;
                                                GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                                rangeflag = "L";
                                                txtFlag.Text = "L";
                                            }
                                            else
                                            {
                                                t.ForeColor = System.Drawing.Color.Black;
                                                // t.BackColor = System.Drawing.Color.Black;
                                                rangeflag = "N";
                                                txtFlag.Text = "N";
                                            }
                                        }
                                        catch (Exception exc)
                                        {
                                            // t.ForeColor = System.Drawing.Color.Red;
                                            // t.BackColor = System.Drawing.Color.Red;
                                            GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                            continue;
                                        }
                                    }
                                    else if (l2.Text.Trim().Substring(0, 1) == "<")
                                    {
                                        try
                                        {
                                            float txtVal = float.Parse(t.Text);
                                            if (txtVal >= float.Parse(l2.Text.Trim().Substring(1)))
                                            {
                                                // t.ForeColor = System.Drawing.Color.Red;
                                                //t.BackColor = System.Drawing.Color.Red;
                                                GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                                rangeflag = "H";
                                                txtFlag.Text = "H";
                                            }
                                            else
                                            {
                                                t.ForeColor = System.Drawing.Color.Black;
                                                // t.BackColor = System.Drawing.Color.Black;
                                                GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                                rangeflag = "N";
                                                txtFlag.Text = "N";
                                            }
                                        }
                                        catch (Exception exc)
                                        {
                                            // t.ForeColor = System.Drawing.Color.Red;
                                            // t.BackColor = System.Drawing.Color.Red;
                                            GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                            continue;
                                        }
                                    }
                                    else if (char.Parse(l2.Text.Trim().Substring(0, 1)) >= '0' && char.Parse(l2.Text.Trim().Substring(0, 1)) <= '9')
                                    {
                                        try
                                        {
                                            float txtVal = float.Parse(t.Text);
                                            if (!(txtVal >= float.Parse(l2.Text.Trim().Split('-')[0]) && txtVal <= float.Parse(l2.Text.Trim().Split('-')[1])))
                                            {
                                                if (!(txtVal >= float.Parse(l2.Text.Trim().Split('-')[0])))
                                                {
                                                    // t.ForeColor = System.Drawing.Color.Red;
                                                    // t.BackColor = System.Drawing.Color.Red;
                                                    GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                                    rangeflag = "L";
                                                    txtFlag.Text = "L";
                                                }
                                                else
                                                {
                                                    // t.ForeColor = System.Drawing.Color.Red;
                                                    //t.BackColor = System.Drawing.Color.Red;
                                                    GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                                    rangeflag = "H";
                                                    txtFlag.Text = "H";
                                                }
                                            }
                                            else
                                            {
                                                t.ForeColor = System.Drawing.Color.Black;
                                                //t.BackColor = System.Drawing.Color.Black;
                                                rangeflag = "N";
                                                txtFlag.Text = "N";

                                            }

                                        }
                                        catch (Exception exc)
                                        {
                                            //  t.ForeColor = System.Drawing.Color.Red;
                                            // t.BackColor = System.Drawing.Color.Red;
                                            GVAAresultEntrySub.Rows[p1].Cells[1].BackColor = Color.Red;
                                            continue;
                                        }
                                    }
                                }
                            }


                            // ========================== End Check abnormal ========================================
                        }
                        catch { }
                    }
                }//
                //   }//

                for (j = 0; j < formula.Length; j++)
                {
                    if (formula[j] != '+' && formula[j] != '-' && formula[j] != '/' && formula[j] != '*' && formula[j] != '(' && formula[j] != ')' && formula[j] != '^')
                    {
                        poststring += formula[j];
                    }
                    if (formula[j] == '(')
                    {
                        operatorstack.Push(formula[j]);
                        top++;
                    }
                    if (formula[j] == '+' || formula[j] == '-' || formula[j] == '/' || formula[j] == '*' || formula[j] == '^')
                    {
                        if (top == -1 || priority(formula[j]) > priority(Convert.ToChar(operatorstack.Peek())))
                        {
                            operatorstack.Push(formula[j]);
                            top++;
                        }
                        else
                        {
                            while (priority(formula[j]) <= priority(Convert.ToChar(operatorstack.Peek())) && top != -1)
                            {
                                poststring += Convert.ToString(operatorstack.Pop());
                                top--;
                            }
                            operatorstack.Push(formula[j]);
                            top++;
                        }
                    }
                    if (formula[j] == ')')
                    {
                        object obj = operatorstack.Pop();
                        top--;
                        while (top != -1 && Convert.ToChar(obj) != '(')
                        {
                            if (Convert.ToChar(obj) != '(')
                                poststring += Convert.ToString(obj);
                            if (top != -1)
                            {
                                obj = operatorstack.Pop();
                                top--;
                            }
                        }
                    }
                }
                if (j == formula.Length)
                {
                    if (top != -1)
                    {
                        while (top != -1)
                        {
                            if (operatorstack.Count != 0)
                            {
                                o = operatorstack.Pop();
                            }
                            if (Convert.ToChar(o) != '(')
                                poststring += Convert.ToString(o);
                            top--;
                        }
                    }
                }
                top = -1;
                string stval = "";
                j = 0;
                try
                {
                    while (j != poststring.Length)
                    {
                        if (poststring[j] != '}' && poststring[j] != '{' && poststring[j] != '+' && poststring[j] != '-' && poststring[j] != '*' && poststring[j] != '/' && poststring[j] != '^')
                        {
                            while (poststring[j] != '}')
                            {
                                if (poststring[j] != '(' || poststring[j] != ')' || poststring[j] != '}' || poststring[j] != '{')
                                {
                                    if (Convert.ToInt32(poststring[j]) >= 97 && Convert.ToInt32(poststring[j]) <= 123 || Convert.ToInt32(poststring[j]) >= 65 && Convert.ToInt32(poststring[j]) <= 91)
                                    {
                                        formulareplaced = false;
                                        break;
                                    }
                                    stval += poststring[j].ToString();
                                    if (j == poststring.Length)
                                    { continue; }
                                    else
                                    {
                                        j++;
                                    }
                                }
                            }
                            if (formulareplaced != false)
                            {
                                resultstack.Push(stval);
                                top++;
                                stval = "";
                            }
                        }
                        if (formulareplaced == false)
                            break;
                        try
                        {
                            if (poststring[j] == '+' || poststring[j] == '-' || poststring[j] == '*' || poststring[j] == '/' || poststring[j] == '^')
                            {
                                if (top != -1)
                                {
                                    if (resultstack.Count != 0)
                                    {
                                        obj2 = resultstack.Pop();
                                        top--;
                                    }
                                    if (resultstack.Count != 0)
                                    {
                                        obj1 = resultstack.Pop();
                                        top--;
                                    }
                                }
                                sval1 = Convert.ToString(obj1);
                                sval2 = Convert.ToString(obj2);
                                try
                                {
                                    testval1 = Convert.ToSingle(Math.Round(Convert.ToDouble(sval1), 4));

                                    testval2 = Convert.ToSingle(Math.Round(Convert.ToDouble(sval2), 4));
                                }
                                catch (Exception exc)
                                {
                                }
                                if (poststring[j] == '+')
                                {
                                    resval = testval1 + testval2;
                                    resultstack.Push(resval);
                                    top++;
                                }
                                if (poststring[j] == '-')
                                {
                                    resval = testval1 - testval2;
                                    resultstack.Push(resval);
                                    top++;
                                }
                                if (poststring[j] == '*')
                                {
                                    resval = testval1 * testval2;
                                    resultstack.Push(resval);
                                    top++;
                                }
                                if (poststring[j] == '/')
                                {
                                    if (testval2 != 0f)
                                    {
                                        resval = testval1 / testval2;
                                        resultstack.Push(resval);
                                        top++;
                                    }
                                    else
                                    {
                                        while (top != -1)
                                        {
                                            if (resultstack.Count != 0)
                                                obj1 = resultstack.Pop();
                                            top--;
                                        }
                                        string close = "<script language='javascript'>javascript:divisionerror();</script>";
                                        Type title1 = this.GetType();
                                        Page.ClientScript.RegisterStartupScript(title1, "", close);
                                        return;
                                    }

                                }
                                if (poststring[j] == '^')
                                {
                                    resval = Convert.ToSingle(Math.Pow(Convert.ToDouble(testval1), Convert.ToDouble(testval2)));

                                    resultstack.Push(resval);
                                    top++;
                                }
                            }
                        }
                        catch (Exception exc) { }
                        j++;
                    }
                }

                catch (Exception exc) { }
                if (top == 0)
                    resval = Convert.ToSingle(resultstack.Pop());
                double finalres = Math.Round(Convert.ToDouble(resval), 2);

                bool flag = false;
                string txtshort = "";
                string lblTestCode1 = "";
                for (int k = 0; k < GVAAresultEntrySub.Rows.Count; k++)
                {
                    ltlc = (GVAAresultEntrySub.Rows[k].FindControl("lbl_MT_Code") as Label);
                    lblTestCode1 = (GVAAresultEntrySub.Rows[k].FindControl("lblTestCode") as Label).Text;


                    if ((GVAAresultEntrySub.Rows[k].FindControl("lblTestCode") as Label).Text == STCODE && formulareplaced == true)
                    {

                        (GVAAresultEntrySub.Rows[k].FindControl("txt_EnterResult") as TextBox).Text = finalres.ToString();

                        flag = true;
                        break;
                    }
                    // }
                    txtshort = (GVAAresultEntrySub.Rows[k].FindControl("txt_EnterResult") as TextBox).Text;
                    if (txtshort != "")
                    {
                        if (char.Parse(txtshort.Trim().Substring(0, 1)) >= '0' && char.Parse(txtshort.Trim().Substring(0, 1)) <= '9')
                        {

                        }
                        else
                        {
                            SubTest_Bal_C Obj_SubTest = new SubTest_Bal_C();

                            (GVAAresultEntrySub.Rows[k].FindControl("txt_EnterResult") as TextBox).Text = Obj_SubTest.Filltxtresult(txtshort, Convert.ToInt32(Session["Branchid"]));
                        }
                    }
                    // =======================

                    //=============================
                    if (flag == true)
                        break;


                }


            } //////
            displayres = true;
            f2 = false;
            poststring = "";
            formulareplaced = true;

        }
    }
    protected void chkautho_CheckedChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
        {
            CheckBox chkAuto = (GVAAresultEntrySub.Rows[i].FindControl("chkautho") as CheckBox);
            Label LblMT = (GVAAresultEntrySub.Rows[i].FindControl("Lblmaintestname") as Label);
            if (chkAuto.Checked == true)
            {
                LblMT.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                LblMT.ForeColor = System.Drawing.Color.Black;
            }
        }

    }
  

    protected void Imghelp_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
        {
            Label ltlc = (GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label);
            TextBox txtshort = (GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox);
            ImageButton DelOk = (ImageButton)GVAAresultEntrySub.Rows[i].FindControl("Imghelp");
            if (DelOk == (ImageButton)sender)
            {
                SubTest_Bal_C Obj_SubTest = new SubTest_Bal_C();
                int BID = Convert.ToInt32(Session["Branchid"]);
                (GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox).Text = Obj_SubTest.Filltxtresult(txtshort.Text, BID);

                if ((GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox).Text == "")
                {
                    Autocalaulate();
                }
            }
        }

    }

    public void sendSMSRegistration()
    {
        Patmstd_Bal_C PBC = new Patmstd_Bal_C();
        string p_mobileno = Convert.ToString(LblMobileno.Text);
        string p_fname = lblName.Text;
        //  string PID = Convert.ToString(PBC.PID);
        string Branchid = Convert.ToString(Session["Branchid"]);
        string msg = PBC.GetSMSString("TestReport", Convert.ToInt16(Branchid));
        string CounCode = PBC.GetSMSString_CountryCode("TestReport", Convert.ToInt16(Branchid));
        string TestStatus = PBC.GetSMSString_AuthorizedTest(Convert.ToInt32(ViewState["PID"]));
        int TestStatusSMS = PBC.GetSMSString_AuthorizedTestSMS(Convert.ToInt32(ViewState["PID"]));
        
        if (TestStatus == "Authorized")
        {
           // if(TestStatusSMS!=0)
           // {
            if (msg.Trim() != "")
            {
                if (msg.Contains("#Name#"))
                {
                    msg = msg.Replace("#Name#", p_fname);
                }
                string PatRegID = lblRegNo.Text;

                if (msg.Contains("#PatRegID#"))
                {
                    msg = msg.Replace("#PatRegID#", PatRegID);
                }
                if (msg.Contains("#UserInfo#"))
                {
                    msg = msg.Replace("#UserInfo#", lblPatusername.Text.Trim());
                }
                
                if (CounCode.Length == 2)
                {
                    if (p_mobileno != CounCode && p_mobileno != "")
                    {
                        // p_mobileno = p_mobileno.Substring(2, 10);
                        createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                        aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        string Labname = aut.P_LabSmsName;
                        string SMSapistring = aut.P_LabSmsString;
                        string Labwebsite = aut.P_LabWebsite;

                        SMSapistring = SMSapistring.ToString().Replace("#message#", msg);
                        SMSapistring = SMSapistring.Replace("#Labname#", Labname);
                        SMSapistring = SMSapistring.Replace("#phone#", p_mobileno);
                        try
                        {
                            string url = apicall(SMSapistring);
                            if (url != "0")
                            {
                                // smsevent = true;                        
                                // lblsmsError.Text = "SMS Sended To Patient " + p_fname + "  Mobile Number Is " + p_mobileno + "";
                                // lblsmsError.Visible = true;
                            }
                            else
                            {
                                // lblsmsError.Visible = true;
                                //lblsmsError.Text = "Unable To Send SMS For Patient " + p_fname + " Mobile Number Is " + p_mobileno + "";
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        //lblsmsError.Visible = true;
                        //lblsmsError.Text = "Patient " + p_fname + " Mobile Number Not available ";
                    }
                }
                else
                {
                    if (p_mobileno != CounCode && p_mobileno != "")
                    {
                        // p_mobileno = p_mobileno.Substring(3, 10);
                        createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                        aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        string Labname = aut.P_LabSmsName;
                        string SMSapistring = aut.P_LabSmsString;
                        string Labwebsite = aut.P_LabWebsite;

                        SMSapistring = SMSapistring.ToString().Replace("#message#", msg);
                        SMSapistring = SMSapistring.Replace("#Labname#", Labname);
                        SMSapistring = SMSapistring.Replace("#phone#", p_mobileno);
                        try
                        {
                            string url = apicall(SMSapistring);
                            if (url != "0")
                            {
                                // smsevent = true;                        
                                // lblsmsError.Text = "SMS Sended To Patient " + p_fname + "  Mobile Number Is " + p_mobileno + "";
                                // lblsmsError.Visible = true;
                            }
                            else
                            {
                                // lblsmsError.Visible = true;
                                //lblsmsError.Text = "Unable To Send SMS For Patient " + p_fname + " Mobile Number Is " + p_mobileno + "";
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    else
                    {
                        //lblsmsError.Visible = true;
                        //lblsmsError.Text = "Patient " + p_fname + " Mobile Number Not available ";
                    }
                }
            }
     //   }
        }
    }
    protected void btnaction_Click(object sender, EventArgs e)
    {
        string RegNo = "", Fid = "";
        if (txtregno.Text.Trim() != "")
        {
            string[] data = txtregno.Text.Trim().Split('=');
            if (data.Length > 1)
            {
                RegNo = data[0].Trim();
                Fid = data[3].Trim();
            }
        }
        if (txtPatientName.Text.Trim() != "")
        {
            string[] data1 = txtPatientName.Text.Trim().Split('=');
            if (data1.Length > 1)
            {
                RegNo = data1[0].Trim();
                Fid = data1[3].Trim();
            }
        }
        if (txtMobileNo.Text.Trim() != "")
        {
            string[] data2 = txtMobileNo.Text.Trim().Split('=');
            if (data2.Length > 1)
            {
                RegNo = data2[0].Trim();
                Fid = data2[3].Trim();
            }
        }
        Response.Redirect ("~/Addresult.aspx?PatRegID=" + txtregno.Text + "&FID=" + Convert.ToString(ViewState["FID"]) + "&RepType=TestResultEntry" + "&Maindept=" + 2,false);

       
       // GetPatientTestResult();
    }
    public void GetPatientTestResult()
    {

        if (Convert.ToString(Session["ISRegNoInt"]) == "YES")
        {
            //Obj_Patst.AlterVW_serialportdate_Old(Request.QueryString["PatRegID"].ToString());
            Obj_Patst.AlterVW_serialportdate(txtregno.Text.ToString());
            Obj_Patst.AlterVW_Int(Convert.ToInt32(Session["Branchid"]), txtregno.Text, Convert.ToString( ViewState["FID"]));
        }
        else
        {
            DataTable dtbarcode = new DataTable();
            dtbarcode = Obj_Patst.GetallBarcode(txtregno.Text);
            if (dtbarcode.Rows.Count > 0)
            {

                for (int b = 0; b < dtbarcode.Rows.Count; b++)
                {
                    if (Barcode == "")
                    {
                        Barcode = " '" + Convert.ToString(dtbarcode.Rows[b]["BarcodeID"]) + "' ";
                    }
                    else
                    {
                        Barcode = Barcode + "," + " '" + Convert.ToString(dtbarcode.Rows[b]["BarcodeID"]) + "' ";
                    }
                }
            }
            Obj_Patst.AlterVW_serialportdate_barcode(Barcode);
            Obj_Patst.AlterVW_Int_Barcode(Convert.ToInt32(Session["Branchid"]), Barcode, Convert.ToString(ViewState["FID"]));

        }


       
            try
            {
                try
                {

                    ViewState["btnsave"] = "";
                    ViewState["sampleflag"] = "true";
                    if (Convert.ToString(Request.QueryString["Maindept"]) != null)
                    {
                        Session["DigModule"] = Request.QueryString["Maindept"].ToString();
                    }
                    if (Session["DigModule"] == "0")
                    {
                        Session["DigModule"] = 2;
                    }
                    ViewState["PanicResult"] = 0;
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
                tabindex = 0;

                ViewState["Fyearid"] = FinancialYearTableLogic.getCurrentFinancialYear().FinancialYearId;


                if (txtregno.Text != null)
                    PatRegID = txtregno.Text;
                if (Convert.ToString(ViewState["FID"]) != null)
                    FID = Convert.ToString(ViewState["FID"]);
                if (Convert.ToString(Session["usertype"]) != null)
                    UserType = Convert.ToString(Session["usertype"]);
                Fill_LabelsNew();
                // Fill_Demography();
                if (Convert.ToString(Session["ISDemography"]) == "YES")
                {
                    // D1.Visible = true;
                    D2.Visible = true;
                    D3.Visible = true;
                    D4.Visible = true;
                    D5.Visible = true;
                    D6.Visible = true;
                    // D7.Visible = true;
                    // D8.Visible = true;
                    D9.Visible = true;
                    D10.Visible = true;
                    D11.Visible = true;
                    D12.Visible = true;
                    D13.Visible = true;
                    // D14.Visible = true;
                    // D15.Visible = true;
                    D16.Visible = true;
                }
                InsertUpdate();
                ObjTA.PatRegID = txtregno.Text;
                ObjTA.FID = Convert.ToString(ViewState["FID"]);
                if (Convert.ToString(Session["ISRegNoInt"]) == "YES")
                {
                    ObjTA.updateIntresult();
                }
                else
                {
                    ObjTA.updateIntresult_Barcode();
                }
                ht = null;
                ht = (Hashtable)ViewState["ht"];

                string labcode1 = "";
                if (Session["UnitCode"] != null)
                {
                    labcode1 = Session["UnitCode"].ToString();
                }

                DataTable dtresult = new DataTable();
                DataTable dtresultsub = new DataTable();
                dtresult = Obj_Patst.GetallResultEntry(PatRegID, FID, Convert.ToInt32(Session["DigModule"]), Convert.ToInt32(Session["Branchid"]), UserType, Convert.ToString(Session["username"]));
                // GVAAresultEntry.DataSource = dtresult;
                // GVAAresultEntry.DataBind();
                if (dtresult.Rows.Count > 0)
                {

                    for (int b = 0; b < dtresult.Rows.Count; b++)
                    {
                        if (MTCodeTemp == "")
                        {
                            MTCodeTemp = " '" + Convert.ToString(dtresult.Rows[b]["MTCode"]) + "' ";
                        }
                        else
                        {
                            MTCodeTemp = MTCodeTemp + "," + " '" + Convert.ToString(dtresult.Rows[b]["MTCode"]) + "' ";
                        }
                    }
                }
                PatSt_Bal_C PSBCL = new PatSt_Bal_C(Convert.ToString(ViewState["PPID"]), PatRegID, Convert.ToInt32(Session["Branchid"]));
                // = PSBCL.PatRegID;
                PSBCL.AlterView_PreviousResult(Convert.ToInt32(Session["Branchid"]), PSBCL.PatRegID, Convert.ToString(ViewState["FID"]), MTCodeTemp);


                dtresultsub = Obj_Patst.GetallResultEntry_subdept(1, Convert.ToString(MTCodeTemp), txtregno.Text, Convert.ToString(ViewState["FID"]));
                GVAAresultEntrySub.DataSource = dtresultsub;
                GVAAresultEntrySub.DataBind();
                for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
                {

                    Label lblPrevResultDate = GVAAresultEntrySub.Rows[i].FindControl("lblPrevResultDate") as Label;
                    DropDownList ddldoctor = GVAAresultEntrySub.Rows[i].FindControl("ddldoctor") as DropDownList;
                    DropDownList ddlTechnician = GVAAresultEntrySub.Rows[i].FindControl("ddlTechnician") as DropDownList;
                    Label Lblsubdeptid = GVAAresultEntrySub.Rows[i].FindControl("Lblsubdeptid") as Label;
                    TextBox txtTestRemark = GVAAresultEntrySub.Rows[i].FindControl("txtTestRemark") as TextBox;
                    Label lblStatus = GVAAresultEntrySub.Rows[i].FindControl("lblStatus") as Label;
                    CheckBox cb = GVAAresultEntrySub.Rows[i].FindControl("chkautho") as CheckBox;
                    string MTCode1 = ((GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label).Text);

                    if (Convert.ToString(Session["usertype"]) == "Main Doctor")
                    {
                        dt = ObjPCB.GetallMaindoctor_addresult_Doctor(Convert.ToString(Session["DigModule"]), Convert.ToString(Lblsubdeptid.Text), Convert.ToString(Session["username"]));

                    }
                    else
                    {
                        dt = ObjPCB.GetallMaindoctor_addresult(Convert.ToString(Session["DigModule"]), Convert.ToString(Lblsubdeptid.Text));
                    }
                    if (dt.Rows.Count > 0)
                    {
                        ddldoctor.DataSource = dt;
                        ddldoctor.DataTextField = "Drsignature";
                        ddldoctor.DataValueField = "Signatureid";
                        ddldoctor.DataBind();

                        ddldoctor.SelectedIndex = 0;
                    }
                    dt = ObjPCB.GetallMaindoctor_addresult_Technican(Convert.ToString(Session["DigModule"]), Convert.ToString(Lblsubdeptid.Text));
                    if (dt.Rows.Count > 0)
                    {
                        ddlTechnician.DataSource = dt;
                        ddlTechnician.DataTextField = "Drsignature";
                        ddlTechnician.DataValueField = "Signatureid";
                        ddlTechnician.DataBind();
                        ddlTechnician.Items.Insert(0, "-Select Tech-");
                        ddlTechnician.SelectedIndex = 0;
                    }
                    PatSt_Bal_C Patst = new PatSt_Bal_C(txtregno.Text, Convert.ToString(ViewState["FID"]), MTCode1, 0, Convert.ToInt32(Session["Branchid"]));
                    txtTestRemark.Text = Patst.P_ReportRemark;
                    ddldoctor.SelectedValue = Convert.ToString(Patst.AunticateSignatureId);
                    ddlTechnician.SelectedValue = Convert.ToString(Patst.Technicianid);
                    if (Patst.AunticateSignatureId > 0)
                    {
                        // Pagelloadcalc();
                    }

                    if (Convert.ToString(Session["usertype"]) == "Reporting" || Convert.ToString(Session["usertype"]) == "Main Doctor" || Convert.ToString(Session["usertype"]) == "Administrator")//Convert.ToString(Session["usertype"]) == "Technician" ||
                    {
                        cb.Enabled = true;

                    }
                    else
                    {
                        // cb.Enabled = false;
                    }
                    if (Convert.ToString(Session["usertype"]) == "Administrator")
                    {
                        cb.Checked = true;
                    }

                    ImageButton ImgDescRes = GVAAresultEntrySub.Rows[i].FindControl("btndescresult") as ImageButton;
                    ImageButton btnImghelp = GVAAresultEntrySub.Rows[i].FindControl("Imghelp") as ImageButton;
                    TextBox txtTRF = GVAAresultEntrySub.Rows[i].FindControl("txt_EnterResult") as TextBox;
                    string TName = GVAAresultEntrySub.Rows[i].Cells[0].Text;
                    string TT = (GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label).Text;
                    Label myLabel1 = (GVAAresultEntrySub.Rows[i].FindControl("lbltexdes") as Label);
                    Label mysubdept = (GVAAresultEntrySub.Rows[i].FindControl("lblsubdept") as Label);
                    ImageButton chkdes = (GVAAresultEntrySub.Rows[i].FindControl("ImgdescN") as ImageButton);
                    ImageButton ChkImgrerun = (GVAAresultEntrySub.Rows[i].FindControl("Imgrerun") as ImageButton);
                    if (lblStatus.Text == "Authorized")
                    {
                        cb.Checked = true;
                        cb.Enabled = false;
                        Label LblMT = (GVAAresultEntrySub.Rows[i].FindControl("Lblmaintestname") as Label);


                        LblMT.ForeColor = System.Drawing.Color.Blue;

                        if (Convert.ToString(Session["usertype"]) == "Technician")
                        {
                            txtTRF.Enabled = false;
                        }
                        else
                        {
                            txtTRF.Enabled = true;
                        }
                    }
                    if (myLabel1.Text == "TextField")
                    {
                        ImgDescRes.Visible = false;

                        txtTRF.Visible = true;
                        GVAAresultEntrySub.HeaderRow.Cells[2].Visible = true;
                        GVAAresultEntrySub.HeaderRow.Cells[3].Visible = true;

                    }
                    else
                    {
                        ImgDescRes.Visible = true;
                        btnImghelp.Visible = false;
                        txtTRF.Visible = false;

                        GVAAresultEntrySub.HeaderRow.Cells[3].Text = "";
                        GVAAresultEntrySub.HeaderRow.Cells[4].Text = "";
                        string NR = GVAAresultEntrySub.Rows[i].Cells[2].Text;


                    }


                    if (TT == "H")
                    {
                        txtTRF.Text = "0";
                        txtTRF.Visible = false;
                        btnImghelp.Visible = false;

                    }
                    if (mysubdept.Text == "MICROBIOLOGY")
                    {
                        //ImgDescRes.Visible = true;
                        chkdes.Visible = false;
                    }
                    else
                    {
                        chkdes.Visible = false;
                    }

                    if (i > 0)
                    {
                        Label Lblmaintestname_Main = GVAAresultEntrySub.Rows[i].FindControl("Lblmaintestname") as Label;
                        Label Lblmaintestname = GVAAresultEntrySub.Rows[i].FindControl("Lblmaintestname1") as Label;
                        Label Lblmaintestname1 = GVAAresultEntrySub.Rows[i - 1].FindControl("Lblmaintestname1") as Label;
                        DropDownList ddldoctor_Main = GVAAresultEntrySub.Rows[i].FindControl("ddldoctor") as DropDownList;
                        DropDownList ddlTechnician_Main = GVAAresultEntrySub.Rows[i].FindControl("ddlTechnician") as DropDownList;
                        CheckBox chkautho = GVAAresultEntrySub.Rows[i].FindControl("chkautho") as CheckBox;
                        TextBox txTestRemark = GVAAresultEntrySub.Rows[i].FindControl("txtTestRemark") as TextBox;
                        ImageButton Imgrep = GVAAresultEntrySub.Rows[i].FindControl("btnreport") as ImageButton;
                        ImageButton ChkImgre_run = (GVAAresultEntrySub.Rows[i].FindControl("Imgrerun") as ImageButton);

                        lblPrevResultDate.Text = "";

                        if (Lblmaintestname.Text == Lblmaintestname1.Text)
                        {
                            Lblmaintestname_Main.Text = "";
                            ddldoctor_Main.Visible = false;
                            ddlTechnician_Main.Visible = false;
                            chkautho.Visible = false;
                            txTestRemark.Visible = false;
                            Imgrep.Visible = false;
                            ChkImgre_run.Visible = false;
                        }
                        if (mysubdept.Text == "MICROBIOLOGY")
                        {

                            if (Lblmaintestname.Text == Lblmaintestname1.Text)
                            {
                                ImgDescRes.Visible = false;
                                chkdes.Visible = false;
                            }

                        }
                    }
                    int GCount = GVAAresultEntrySub.Rows.Count;
                    if (GCount - 1 == i)
                    {
                        //  Autocalaulate();
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
            // Pagelloadcalc();
        
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] GetPatientInfo(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        string collectioncode = HttpContext.Current.Session["CenterCode"].ToString();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
        {
            sda = new SqlDataAdapter("select Patregid,Patphoneno,Patname,FID from patmst where  (Patname like N'%" + prefixText + "%') order by FID desc   ", con);
        }
        else
        {
            sda = new SqlDataAdapter("select Patregid,Patphoneno,Patname,FID from patmst where  (Patname like N'%" + prefixText + "%') order by FID desc  ", con);
        }
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["Patregid"] + " = " + dr["Patphoneno"] + " = " + dr["Patname"] + " = " + dr["FID"], i);
            i++;
        }

        return tests;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] GetPatientInfo_Mobile(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        string collectioncode = HttpContext.Current.Session["CenterCode"].ToString();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
        {
            sda = new SqlDataAdapter("select Patregid,Patphoneno,Patname,FID from patmst where (Patphoneno like '" + prefixText + "%') order by FID desc  ", con);
        }
        else
        {
            sda = new SqlDataAdapter("select Patregid,Patphoneno,Patname,FID from patmst where (Patphoneno like '" + prefixText + "%') order by FID desc  ", con);
        }
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["Patregid"] + " = " + dr["Patphoneno"] + " = " + dr["Patname"] + " = " + dr["FID"], i);
            i++;
        }

        return tests;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] GetPatientInfo_regno(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        string collectioncode = HttpContext.Current.Session["CenterCode"].ToString();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
        {
            sda = new SqlDataAdapter("select Patregid,Patphoneno,Patname,FID from patmst where  (Patregid like N'%" + prefixText + "%')or (Patname like N'%" + prefixText + "%')  or (Patphoneno like '" + prefixText + "%') order by FID desc  ", con);
        }
        else
        {
            sda = new SqlDataAdapter("select Patregid,Patphoneno,Patname,FID from patmst where  (Patregid like N'%" + prefixText + "%')or (Patname like N'%" + prefixText + "%')  or (Patphoneno like '" + prefixText + "%') order by FID desc  ", con);
        }
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["Patregid"] + " = " + dr["Patphoneno"] + " = " + dr["Patname"] + " = " + dr["FID"], i);
            i++;
        }

        return tests;
    }
    protected void txtregno_TextChanged(object sender, EventArgs e)
    {
        if (txtregno.Text != "")
        {
            string[] data2 = txtregno.Text.Trim().Split('=');
            if (data2.Length > 1)
            {
                txtregno.Text = data2[0].Trim();
                txtPatientName.Text = data2[2].Trim();
                txtMobileNo.Text = data2[1].Trim();
                ViewState["FID"] = data2[3].Trim(); ;
                this.btnaction_Click(null, null);
            }
        }

    }
    protected void txtPatientName_TextChanged(object sender, EventArgs e)
    {
        if (txtPatientName.Text != "")
        {
            string[] data2 = txtPatientName.Text.Trim().Split('=');
            if (data2.Length > 1)
            {
                txtregno.Text = data2[0].Trim();
                txtPatientName.Text = data2[2].Trim();
                txtMobileNo.Text = data2[1].Trim();
                ViewState["FID"] = data2[3].Trim(); ;
            }
        }
    }
    protected void txtMobileNo_TextChanged(object sender, EventArgs e)
    {
        if (txtMobileNo.Text != "")
        {
            string[] data2 = txtMobileNo.Text.Trim().Split('=');
            if (data2.Length > 1)
            {
                txtregno.Text = data2[0].Trim();
                txtPatientName.Text = data2[2].Trim();
                txtMobileNo.Text = data2[1].Trim();
                ViewState["FID"] = data2[3].Trim(); ;
            }
        }
    }
    protected void btnbackbutton_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Testresultentry.aspx?type=Test" + "&CN=" + Request.QueryString["CN"] + "&TS=" + Request.QueryString["TS"] + "", false);
        Server.Transfer("~/Testresultentry.aspx?CN=" + Request.QueryString["CN"] + "&TS=" + Request.QueryString["TS"] + "&FDate=" + Request.QueryString["FrDate"] + "&TDate=" + Request.QueryString["ToDate"] + "&DeptN=" + Request.QueryString["DeptName"] + "", false);//DeptName=HEMATOLOGY

    } 
    protected void NORep_Click(object sender, EventArgs e)
    {

        string MTDummycode = "", subdept = "";
        for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
        {
            Label ltlc = (GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label);

            Button DelOk = (Button)GVAAresultEntrySub.Rows[i].FindControl("NORep");
            if (DelOk == (Button)sender)
            {
                string STCODE = DelOk.CommandArgument;

                Label lblTestCode = (GVAAresultEntrySub.Rows[i].FindControl("lblTestCode") as Label);
                Label lblMTCodesub = (GVAAresultEntrySub.Rows[i].FindControl("lblMTCodesub") as Label);
                Label lblsubdeptid1 = (GVAAresultEntrySub.Rows[i].FindControl("lblsubdeptid1") as Label);
                MTDummycode = lblMTCodesub.Text;
                Label mysubdept = (GVAAresultEntrySub.Rows[i].FindControl("lblsubdept") as Label);
                subdept = mysubdept.Text;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'UploadReportPatient.aspx?TestCode=" + ltlc.Text + "&PatRegID=" + Convert.ToString(Request.QueryString["PatRegID"]) + "&Branchid=" + Session["Branchid"].ToString() + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

            }
        }

    }
    protected void btnsaveClose_Click(object sender, EventArgs e)
    {
        this.btnSaveAll_Click(null, null);
        if (Convert.ToString(ViewState["Reverse"]) != "False")
        {
            Server.Transfer("~/Testresultentry.aspx?CN=" + Request.QueryString["CN"] + "&TS=" + Request.QueryString["TS"] + "&FDate=" + Request.QueryString["FrDate"] + "&TDate=" + Request.QueryString["ToDate"] + "&pname=" + Request.QueryString["pname"] + "", false);
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

    protected void DdlShortForm_SelectedIndexChanged(object sender, EventArgs e)
    {
        int RowIndex = 0;
        for (int p = 0; p < GVAAresultEntrySub.Rows.Count; p++)
        {
            Label ltlc = (GVAAresultEntrySub.Rows[p].FindControl("lbl_MT_Code") as Label);
            RowIndex = ((GridViewRow)((DropDownList)sender).NamingContainer).RowIndex;

            TextBox EnterResult = GVAAresultEntrySub.Rows[p].FindControl("txt_EnterResult") as TextBox;
            DropDownList ddlShortForm = GVAAresultEntrySub.Rows[p].FindControl("DdlShortForm") as DropDownList;
            if (ddlShortForm.SelectedValue != "-ShortForm-")
            {
                if (EnterResult.Text == "")
                {
                    EnterResult.Text = ddlShortForm.SelectedValue;
                   
                }
            }
        }
    }

    protected void btnprintprieve_Click(object sender, EventArgs e)
    {
        // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'TestReportprinting.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&form=" + Request.QueryString["form"].ToString() + "&formname=" + Request.QueryString["formname"].ToString() + "&user=" + Session["usertype"].ToString() + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        if (ViewState["btnsave"].ToString() != "true")
        {
            ViewState["sampleflag"] = "false";
            ViewState["Reverse"] = "False";
            this.btnSaveAll_Click(btnSaveAll, null);
        }
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'DirectTestReportPrinting.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&MTCode=" + Request.QueryString["MTCode"] + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

    }

    protected void chkQc_CheckedChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < GVAAresultEntrySub.Rows.Count; i++)
        {
            CheckBox chkAuto = (GVAAresultEntrySub.Rows[i].FindControl("chkautho") as CheckBox);
            CheckBox chkQc = (GVAAresultEntrySub.Rows[i].FindControl("chkQc") as CheckBox);
          //  Label LblMT = (GVAAresultEntrySub.Rows[i].FindControl("Lblmaintestname") as Label);
            if (chkQc.Checked == true)
            {
                if (chkAuto.Checked == false)
                {
                    chkAuto.Enabled = true;
                     Obj_Patst.PatRegID = Request.QueryString["PatRegID"];
                     Obj_Patst.MTCode = Convert.ToString((GVAAresultEntrySub.Rows[i].FindControl("lbl_MT_Code") as Label).Text);
                     Obj_Patst.FID =Convert.ToString( Request.QueryString["FID"]);
                    //bool CheckPrintRep;
                    //CheckPrintRep = ObjPBC.Check_PrintedReport(ObjPBC.PatRegID, Request.QueryString["FID"], ObjPBC.MTCode, 1);

                    Obj_Patst.InsertUpdate_QCStatus(Convert.ToInt32(Session["Branchid"]));
                }
            }
            else
            {
                chkAuto.Enabled = false;
            }
        }

    }
}