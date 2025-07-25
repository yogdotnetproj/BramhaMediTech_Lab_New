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
using System.Drawing;


using System.Net.Http;

public partial class TestPrinting : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    public bool list = false;
    string T_Code = "";
    public string FID = "01";
    private string testnametm;
    public string Testnametm
    {
        get { return testnametm; }
        set { testnametm = value; }
    }
    private string _STCodeT;
    public string STCodeT
    {
        get { return _STCodeT; }
        set { _STCodeT = value; }
    }
    private string fid;
    public string Fid
    {
        get { return fid; }
        set { fid = value; }
    }
    private string _PatRegID;
    public string PatRegID
    {
        get { return _PatRegID; }
        set { _PatRegID = value; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Request.QueryString["MTCode"] != null && Request.QueryString["PatRegID"] == null && Request.QueryString["FID"] == null)
        {
            STCodeT = Request.QueryString["MTCode"];
            Session["STCodeT"] = Request.QueryString["MTCode"];
            btnsave.Visible = false;
            T_Code = Request.QueryString["MTCode"].ToString();

            chkAuthorize.Visible = false;


        }
        else
        {

            PatRegID = Request.QueryString["PatRegID"];
            fid = Request.QueryString["FID"];
            STCodeT = Request.QueryString["MTCode"];
            T_Code = Request.QueryString["MTCode"].ToString();

            chkAuthorize.Visible = true;

        }

        Fill_Labels();
        try
        {
            if (!Page.IsPostBack)
            {
                Editor1.Height = 2500;
                LUNAME.Text = Convert.ToString(Session["username"]);
                LblDCName.Text = Convert.ToString(Session["Bannername"]);
                LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                this.PopulateTreeView(dt, 0, null);
                FillListFormat();
                BindDoc();
                if (Request.QueryString["Signatureid"] != null && Request.QueryString["Signatureid"] != "0")
                {
                    ddlDoctor_new.SelectedValue = Convert.ToString(Request.QueryString["Signatureid"]);
                }
                TestDescriptiveResult_b ttm = new TestDescriptiveResult_b(PatRegID, fid, STCodeT, Convert.ToInt32(Session["Branchid"]));

                if (ttm.TextDesc != "")
                {
                    string SavedString = ttm.TextDesc;
                    string mhtml = ttm.TextDesc.Replace("<b>", "<strong>").Replace("</b>", "</strong>").Replace("<b>", "<STRONG>").Replace("</b>", "</STRONG>").Replace("#FFFFFF", "#000000").Replace("#ffffff", "#000000");

                    Editor1.Text = mhtml;
                }

                if (ttm.P_ResultTemplate != "")
                {
                    CmbFormatName.SelectedValue = ttm.P_ResultTemplate;
                    txtTestcode.Text = ttm.P_ResultTemplate;
                }



                string sRegno = PatRegID.ToString().Trim();

                PatSt_Bal_C pst = new PatSt_Bal_C(sRegno, fid, T_Code, 0, Convert.ToInt32(Session["Branchid"]));

                if (pst.Patauthicante == "Authorized")
                {
                    chkAuthorize.Checked = true;
                    chkAuthorize.Enabled = false;

                }




            }

        }
        catch
        {


        }
        if (Session["usertype"].ToString() == "Lab Technician")
        {
            chkAuthorize.Enabled = false;
        }
        if (Request.QueryString["FID"] != null)
            FID = Request.QueryString["FID"];
        string labcode1 = "";
        if (Session["UnitCode"] != null)
        {
            labcode1 = Session["UnitCode"].ToString();
        }

    }
    public void BindDoc()
    {
        Patmstd_Main_Bal_C ObjPCB = new Patmstd_Main_Bal_C();
        DataTable dt = new DataTable();
        dt = ObjPCB.GetallMaindoctor_addresult(Convert.ToString(Session["DigModule"]), Request.QueryString["subdeptid"]);
        ddlDoctor_new.DataSource = dt;
        ddlDoctor_new.DataTextField = "Drsignature";
        ddlDoctor_new.DataValueField = "Signatureid";
        ddlDoctor_new.DataBind();
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillFormats(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;

        sda = new SqlDataAdapter("SELECT * from dfrmst  WHERE STCODE='" + HttpContext.Current.Session["STCodeT"] + "' and Name like '%" + prefixText + "%'   ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["Name"], i);
            i++;
        }

        return tests;
    }
    private void FillListFormat()
    {

        try
        {
            DataTable dt = new DataTable();
            TestDescriptiveResult_b ttm = new TestDescriptiveResult_b();

            dt = ttm.getDefaultResults(STCodeT, Convert.ToInt32(Session["Branchid"]),"");
            CmbFormatName.DataSource = dt;

            CmbFormatName.DataTextField = "Name";
            CmbFormatName.DataValueField = "Name";
            CmbFormatName.DataBind();

            CmbFormatName.Items.Insert(0, new ListItem("-- Select --", "0"));
            CmbFormatName.SelectedIndex = 0;
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

    protected void cmdList_Click(object sender, EventArgs e)
    {
        FillListFormat();

    }

    protected void cmdClose_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["MTCode"] != null && Request.QueryString["PatRegID"] == null && Request.QueryString["FID"] == null)
        {
            Response.Redirect("ShowTest.aspx");
        }
        else
        {
            Response.Redirect("Addresult.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&formname=TextMemo" + "&form=" + Request.QueryString["form"].ToString() + "&user=" + Session["usertype"].ToString());
        }
    }

    protected void cmdSaveClose_Click(object sender, EventArgs e)
    {

        if (Editor1.Text == "")
        {
            lblValidate.Text = "Please enter the Result";
            lblValidate.Visible = true;

            Editor1.Focus();
            return;
        }

        AssignDoctor();
        MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(Request.QueryString["MTCode"], Convert.ToInt32(Session["Branchid"]));

        string sRegno = PatRegID.ToString().Trim();

        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C(sRegno, fid, Obj_MTB_C.MTCode, 0, Convert.ToInt32(Session["Branchid"]));
        int PID = Obj_PBC.PID;

        if (Editor1.Visible)
        {
            Obj_PBC.Testdate = Date.getdate();
            Obj_PBC.TestUser = Session["username"].ToString();
            Obj_PBC.Patauthicante = "Tested";

        }

        if (chkAuthorize.Checked)
        {
            string navURL = "Addresult.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&Patname=textmemo";
            if (Request.QueryString["Signatureid"] != null)
            {
                if (Request.QueryString["Signatureid"].Trim() != "")
                {
                    if (((Session["usertype"].ToString() == "Main Doctor") || (Session["usertype"].ToString() == "Administrator") || (Session["usertype"].ToString() == "Admin") || (Session["usertype"].ToString() == "Super Admin")))
                    {
                        Obj_PBC.TestUser = Session["username"].ToString();
                    }
                    Obj_PBC.TestUser = Session["username"].ToString();
                    Obj_PBC.AunticateSignatureId = Convert.ToInt32(Request.QueryString["Signatureid"].Trim());
                    Obj_PBC.AunticateSignatureId = Convert.ToInt32(ViewState["Signatureid"]);
                    Obj_PBC.Patauthicante = "Authorized";
                }
                else
                {
                    lblSelectDocError.Text = " authorize not done.";
                    return;
                }
            }
            else
            {
                lblSelectDocError.Text = " authorize not done.";
                return;
            }
        }
        else
        {
            Obj_PBC.Patauthicante = "Tested";
        }
        Obj_PBC.Testdate = Date.getdate();
        Obj_PBC.Update(Convert.ToInt32(Session["Branchid"]));



        if ((chkAuthorize.Checked && chkAuthorize.Enabled))
        {
            TAT_C at = new TAT_C();
            at.PatRegID = PatRegID.ToString();
            at.MTCode = Request.QueryString["MTCode"];
            at.STCODE = null;
            at.DateOfRecord = Date.getdate();
            at.FID = fid;
            at.Status = "Authorized";
            at.UserName = Session["username"].ToString();

            // at.TestResults = Editor.Text.Replace("<br />", "</p><p   line-height=10%>").Replace("<br/>", "</p><p   line-height=10%>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            at.TestResults = Editor1.Text.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            // at.TestResults = at.TestResults.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            at.Remarks = "";
            //  at.Insert(Convert.ToInt32(Session["Branchid"]));
        }
        try
        {


            TestDescriptiveResult_b Obj_TDR_C = new TestDescriptiveResult_b(PatRegID, fid, Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]));

            Obj_TDR_C.FID = fid;

            if (Request.QueryString["Signatureid"] != null)
            {
                Obj_TDR_C.Signatureid = Convert.ToInt32(Request.QueryString["Signatureid"].Trim());
            }
            else
            {
                Obj_TDR_C.Signatureid = 0;
            }
            Obj_TDR_C.Signatureid = Convert.ToInt32(ViewState["Signatureid"]);

            Obj_TDR_C.PatRegID = PatRegID.ToString();

            Obj_TDR_C.MTCode = Request.QueryString["MTCode"];
            Obj_TDR_C.STCODE = Request.QueryString["MTCode"];

            string Mhtml = Editor1.Text;//.Replace(pp, "");                    
            // Obj_TDR_C.TextMemo = Mhtml.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            Obj_TDR_C.TextDesc = Mhtml.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            //Obj_TDR_C.TextDesc = Obj_TDR_C.TextDesc.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            Obj_TDR_C.P_ResultTemplate = CmbFormatName.SelectedValue;
            Obj_TDR_C.update(Convert.ToInt32(Session["Branchid"]));
        }
        catch
        {
            TestDescriptiveResult_b Obj_TDR_C = new TestDescriptiveResult_b();
            Obj_TDR_C.FID = fid;

            if (Request.QueryString["Signatureid"] != null)
            {
                Obj_TDR_C.Signatureid = Convert.ToInt32(Request.QueryString["Signatureid"].Trim());
            }
            else
            {
                Obj_TDR_C.Signatureid = 0;
            }
            Obj_TDR_C.Signatureid = Convert.ToInt32(ViewState["Signatureid"]);
            //Obj_TDR_C.PatRegID = PatRegID.ToString().PadLeft(7, '0');
            Obj_TDR_C.PatRegID = PatRegID.ToString();
            Obj_TDR_C.MTCode = Request.QueryString["MTCode"];
            Obj_TDR_C.STCODE = Request.QueryString["MTCode"];
            string Mhtml1 = Editor1.Text;
            //Obj_TDR_C.TextMemo = Mhtml1.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            Obj_TDR_C.TextDesc = Mhtml1.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            // Obj_TDR_C.TextDesc = Obj_TDR_C.TextDesc.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            Obj_TDR_C.PID = PID;
            Obj_TDR_C.P_ResultTemplate = CmbFormatName.SelectedValue;
            Obj_TDR_C.Insert(Convert.ToInt32(Session["Branchid"]));
        }
        if (Convert.ToString(Request.QueryString["subDeptName"]) == "MICROBIOLOGY")
        {
            PatSt_Bal_C PBC = new PatSt_Bal_C();
            PBC.MTCode = Request.QueryString["MTCode"];
            //  PBC.Update_Microbiologyreport_Rev(1);
        }
        Response.Redirect("Addresult.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&form=" + Request.QueryString["form"].ToString() + "&user=" + Session["usertype"].ToString() + "&formname=TextMemo");

    }

    protected void cmdClear_Click(object sender, EventArgs e)
    {

        if (Editor1.Text == "")
        {
            lblValidate.Text = " enter the Result";
            lblValidate.Visible = true;
            Editor1.Focus();
            return;
        }
        try
        {
            Dfrmst_Bal_C Obj_DB_C = new Dfrmst_Bal_C(T_Code, txtTestcode.Text);

            Obj_DB_C.STCODE = T_Code;

            // Obj_DB_C.Result = Editor.Text.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            Obj_DB_C.Result = Editor1.Text.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            // Obj_DB_C.Result = Obj_DB_C.Result.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            if (CmbFormatName.Text != "")
                Obj_DB_C.update(CmbFormatName.Text, Convert.ToInt32(Session["Branchid"]));
            else
                Obj_DB_C.update(txtTestcode.Text, Convert.ToInt32(Session["Branchid"]));

        }
        catch
        {
            Dfrmst_Bal_C Obj_DB_C = new Dfrmst_Bal_C();
            Obj_DB_C.Name = txtTestcode.Text;
            Obj_DB_C.STCODE = T_Code;

            string ffff = Editor.Text;
            // Obj_DB_C.Result = Editor.Text.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            Obj_DB_C.Result = Editor1.Text.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            //  Obj_DB_C.Result = Obj_DB_C.Result.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            Obj_DB_C.Insert(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["Signatureid"]));
        }
    }



    protected void CmbFormatName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbFormatName.SelectedValue != "")
        {

            Editor1.Text = new Dfrmst_Bal_C(T_Code, CmbFormatName.SelectedValue).Result.Replace("#FFFFFF", "#000000").Replace("#ffffff", "#000000");
            txtTestcode.Text = new Dfrmst_Bal_C(T_Code, CmbFormatName.SelectedValue).Name;
            txtTestcode.ReadOnly = true;
        }
    }


    public void AssignDoctor()
    {
        try
        {
            string MTCode = null;
            string SDCode1 = null;

            MTCode = Request.QueryString["MTCode"];
            MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(MTCode, Convert.ToInt32(Session["Branchid"]));

            string sRegno = Request.QueryString["PatRegID"].ToString();

            SDCode1 = Obj_MTB_C.SDCode;



            string SDCode = Obj_MTB_C.MTCode.Substring(0, 2);
            PatSt_Bal_C Obj_PBC = new PatSt_Bal_C(sRegno, Request.QueryString["FID"], SDCode, Convert.ToInt32(Session["Branchid"]));
            Obj_PBC.MTCode = MTCode;
            Obj_PBC.PatRegID = sRegno;
            Obj_PBC.FID = Request.QueryString["FID"].ToString();
            Obj_PBC.AunticateSignatureId = Convert.ToInt32(ddlDoctor_new.SelectedValue);
            ViewState["Signatureid"] = Convert.ToInt32(ddlDoctor_new.SelectedValue);
            Obj_PBC.UpdateNew(Convert.ToInt32(Session["Branchid"]));
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

    public void Fill_Labels()
    {
        #region  Information Of Patient
        Patmst_Bal_C CIT = null;
        try
        {

            CIT = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));

            lblRegNo.Text = Convert.ToString(CIT.PatRegID);
            lbldate.Text = CIT.FID;

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
            lblRegNo.Text = "Record not foud";
        }
        #endregion
    }
    private void PopulateTreeView(DataTable dtparent, int Parentid, TreeNode treeNode)
    {
        foreach (DataRow row in dtparent.Rows)
        {
            TreeNode child = new TreeNode
            {
                Text = row["MenuName"].ToString(),
                Value = row["MenuID"].ToString()

            };
            if (Parentid == 0)
            {
                TrMenu.Nodes.Add(child);
                DataTable dtchild = new DataTable();
                dtchild = ObjTB.BindChildMenu(child.Value, Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                PopulateTreeView(dtchild, int.Parse(child.Value), child);

            }
            else
            {
                treeNode.ChildNodes.Add(child);
            }

        }
    }


    protected void TrMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        int tId = Convert.ToInt32(TrMenu.SelectedValue);
        DataTable dtform = new DataTable();
        dtform = ObjTB.BindForm(tId);
        if (dtform.Rows.Count > 0)
        {
            Response.Redirect(dtform.Rows[0]["SubMenuNavigateURL"].ToString());
        }
    }
    protected void txtFormatName_TextChanged(object sender, EventArgs e)
    {
        if (txtFormatName.Text != "")
        {

            DataTable dt = new DataTable();
            TestDescriptiveResult_b OBJ_TD = new TestDescriptiveResult_b();

            dt = OBJ_TD.getDefaultResults_search(STCodeT, Convert.ToInt32(Session["Branchid"]), txtFormatName.Text);
            CmbFormatName.DataSource = dt;

            CmbFormatName.DataTextField = "Name";
            CmbFormatName.DataValueField = "Name";
            CmbFormatName.DataBind();

            CmbFormatName.Items.Insert(0, new ListItem("-- Select --", "0"));
            CmbFormatName.SelectedIndex = 0;
        }
    }
}