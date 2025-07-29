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
public partial class TestHistoResult :BasePage
{
    DataTable dt = new DataTable();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    TestDescriptiveResult_b OBjTDR = new TestDescriptiveResult_b();
    PatSt_Bal_C psnew = new PatSt_Bal_C();
    public bool list = false;
    string T_Code = "";
    public string FID = "01";
    private string testnametm;
    public string Testnametm { get { return testnametm; } set { testnametm = value; } }
    private string MTCode; public string P_MTcode { get { return MTCode; } set { MTCode = value; } }
    private string fid; public string Fid { get { return fid; } set { fid = value; } }
    private string _PatRegID; public string PatRegID { get { return _PatRegID; } set { _PatRegID = value; } }
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Request.QueryString["MTCode"] != null && Request.QueryString["PatRegID"] == null && Request.QueryString["FID"] == null)
        {
            MTCode = Request.QueryString["MTCode"];
            Session["MTCode"] = Request.QueryString["MTCode"];
            btnsave.Visible = false;
            T_Code = Request.QueryString["MTCode"].ToString();

            chkAuthorize.Visible = false;
        }
        else
        {
            PatRegID = Request.QueryString["PatRegID"];
            fid = Request.QueryString["FID"];
            MTCode = Request.QueryString["MTCode"];
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
                Fill_format();
                BindDoc();
              
                OBjTDR.TestDescriptiveResult_bHisto(PatRegID, fid, MTCode, Convert.ToInt32(Session["Branchid"]));
                if (OBjTDR.TextDesc != "")
                {
                    string SavedString = OBjTDR.TextDesc;
                    string mhtml = OBjTDR.TextDesc.Replace("<b>", "<strong>").Replace("</b>", "</strong>").Replace("<b>", "<STRONG>").Replace("</b>", "</STRONG>").Replace("#FFFFFF", "#000000").Replace("#ffffff", "#000000");
                    // Editor.Text = mhtml;
                    Editor1.Text = mhtml;
                }

                if (OBjTDR.P_ResultTemplate != "")
                {
                    CmbFormatName.SelectedValue = OBjTDR.P_ResultTemplate;
                    txtTestcode.Text = OBjTDR.P_ResultTemplate;
                }

                string sRegno = PatRegID.ToString().Trim();
                PatSt_Bal_C Obj_PBCt = new PatSt_Bal_C(sRegno, fid, T_Code, 0, Convert.ToInt32(Session["Branchid"]));
                if (Obj_PBCt.Patauthicante == "Authorized")
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
            labcode1 = Session["UnitCode"] .ToString();
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
        PatSt_Bal_C pst = new PatSt_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], MTCode, 0, Convert.ToInt32(Session["Branchid"]));
       
        ddlDoctor_new.SelectedValue = Convert.ToString(pst.AunticateSignatureId);
        
    }
    public void Fill_format()
    {
        DataTable dt = new DataTable();
        TestDescriptiveResult_b ttm = new TestDescriptiveResult_b();

        dt = ttm.getDefaultResults_search_Histo(MTCode, Convert.ToInt32(Session["Branchid"]), txtFormatName.Text);
        CmbFormatName.DataSource = dt;
        CmbFormatName.DataTextField = "Name";
        CmbFormatName.DataValueField = "Name";
        CmbFormatName.DataBind();

        CmbFormatName.Items.Insert(0, new ListItem("-- Select --", "0"));
        CmbFormatName.SelectedIndex = 0;
    }
    public void Fill_Labels()
    {
        #region Patient Info Of Patient
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
    protected void CmbFormatName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbFormatName.SelectedValue != "")
        {

            Editor1.Text = new Dfrmst_Bal_C(T_Code, CmbFormatName.SelectedValue, 0,0).Result.Replace("#FFFFFF", "#000000").Replace("#ffffff", "#000000");
            txtTestcode.Text = new Dfrmst_Bal_C(T_Code, CmbFormatName.SelectedValue, 0,0).Name;
            //txtTestcode.ReadOnly = true;
           
        }
    }
    public void AssignDoctor()
    {
        try
        {
            string TCode = null;
            string DCode = null;

            TCode = Request.QueryString["MTCode"];
            MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(TCode, Convert.ToInt32(Session["Branchid"]));

            string PatRegno = Request.QueryString["PatRegID"].ToString();

            DCode = Obj_MTB_C.SDCode;

            string SDCode = Obj_MTB_C.MTCode.Substring(0, 2);
            PatSt_Bal_C Obj_PBC = new PatSt_Bal_C(PatRegno, Request.QueryString["FID"], SDCode, Convert.ToInt32(Session["Branchid"]));
            Obj_PBC.MTCode = TCode;
            Obj_PBC.PatRegID = Request.QueryString["PatRegID"].ToString(); ;
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

        string PatRegno = PatRegID.ToString().Trim();

        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C(PatRegno, fid, Obj_MTB_C.MTCode, 0, Convert.ToInt32(Session["Branchid"]));
        int PID = Obj_PBC.PID;

        if (Editor1.Visible)
        {
            Obj_PBC.Testdate = Date.getdate();
            Obj_PBC.TestUser = Session["username"].ToString();
            Obj_PBC.Patauthicante = "Tested";

        }

        if (chkAuthorize.Checked)
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
            Obj_PBC.Patauthicante = "Tested";
        }
        Obj_PBC.Testdate = Date.getdate();
        DataTable dtexistaut = new DataTable();
        dtexistaut = psnew.Check_Authorised_Test(lblRegNo.Text, Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
        if (dtexistaut.Rows[0]["Technicanby"] != "")
        {
            Obj_PBC.P_Technicanby = Convert.ToString(dtexistaut.Rows[0]["Technicanby"]);
        }
        else
        {

            Obj_PBC.P_Technicanby = Session["username"].ToString();
        }
        //Obj_PBC.P_Technicanby = Session["username"].ToString();
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
            at.TestResults = Editor1.Text.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align:center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff").Replace("<p style=" + "text-align:center" + ">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
           // at.TestResults = at.TestResults.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            at.Remarks = "";
           // at.Insert(Convert.ToInt32(Session["Branchid"]));
        }
        try
        {


            OBjTDR.TestDescriptiveResult_bHisto(PatRegID, fid, Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]));

            OBjTDR.FID = fid;

            if (Request.QueryString["Signatureid"] != null)
            {
                OBjTDR.Signatureid = Convert.ToInt32(Request.QueryString["Signatureid"].Trim());
            }
            else
            {
                OBjTDR.Signatureid = 0;
            }
            OBjTDR.Signatureid = Convert.ToInt32(ViewState["Signatureid"]);

            OBjTDR.PatRegID = PatRegID.ToString();

            OBjTDR.MTCode = Request.QueryString["MTCode"];
            OBjTDR.STCODE = Request.QueryString["MTCode"];

            string Mhtml = Editor1.Text;//.Replace(pp, "");                    
                     OBjTDR.TextDesc = Mhtml.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            //OBjTDR.TextDesc= OBjTDR.TextDesc.Replace("<p style= "+"\""+"text-align:center"+""+"\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            OBjTDR.TextDesc = OBjTDR.TextDesc.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align:center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff").Replace("<p style=" + "text-align:center" + ">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
           // OBjTDR.TextDesc = OBjTDR.TextDesc.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            OBjTDR.P_ResultTemplate = CmbFormatName.SelectedValue;
            OBjTDR.update_Histo(Convert.ToInt32(Session["Branchid"]));
        }
        catch
        {
            TestDescriptiveResult_b tbl = new TestDescriptiveResult_b();
            tbl.FID = fid;

            if (Request.QueryString["Signatureid"] != null)
            {
                tbl.Signatureid = Convert.ToInt32(Request.QueryString["Signatureid"].Trim());
            }
            else
            {
                tbl.Signatureid = 0;
            }
            tbl.Signatureid = Convert.ToInt32(ViewState["Signatureid"]);

            tbl.PatRegID = PatRegID.ToString();
            tbl.MTCode = Request.QueryString["MTCode"];
            tbl.STCODE = Request.QueryString["MTCode"];
            string Mhtml1 = Editor1.Text;

            tbl.TextDesc = Mhtml1.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            tbl.TextDesc = tbl.TextDesc.Replace("<p style=" + "text-align:center" + ">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
           // tbl.TextDesc = tbl.TextDesc.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            tbl.PID = PID;
            tbl.P_ResultTemplate = CmbFormatName.SelectedValue;
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand();


            FileUpload img1 = (FileUpload)FileUpload1;
            Byte[] imgByte1 = null;
            if (img1.HasFile && img1.PostedFile != null)
            {
                //To create a PostedFile
                HttpPostedFile File1 = FileUpload1.PostedFile;
                //Create byte Array with file len
                imgByte1 = new Byte[File1.ContentLength];
                //force the control to load data in array
                File1.InputStream.Read(imgByte1, 0, File1.ContentLength);
                tbl.Image1 = imgByte1;

               // ===================================================


                Session["Image"] = FileUpload1.PostedFile;
                Stream fs = FileUpload1.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(fs);
                byte[] bytes = br.ReadBytes((Int32)fs.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                Image1.ImageUrl = "data:image/png;base64," + base64String;
               // =================================
            }
            else
            {
                tbl.Image1 = null;
            }
            FileUpload img2 = (FileUpload)FileUpload2;
            Byte[] imgByte2 = null;
            if (img2.HasFile && img2.PostedFile != null)
            {
                //To create a PostedFile
                HttpPostedFile File2 = FileUpload2.PostedFile;
                //Create byte Array with file len
                imgByte2 = new Byte[File2.ContentLength];
                //force the control to load data in array
                File2.InputStream.Read(imgByte2, 0, File2.ContentLength);
                tbl.Image2 = imgByte2;
            }
            else
            {
                imgByte2 = null;
            }

            FileUpload img3 = (FileUpload)FileUpload3;
            Byte[] imgByte3 = null;
            if (img3.HasFile && img3.PostedFile != null)
            {
                //To create a PostedFile
                HttpPostedFile File3 = FileUpload3.PostedFile;
                //Create byte Array with file len
                imgByte3 = new Byte[File3.ContentLength];
                //force the control to load data in array
                File3.InputStream.Read(imgByte3, 0, File3.ContentLength);
                tbl.Image3 = imgByte3;
            }
            else
            {
                tbl.Image3 = null;
            }
            FileUpload img4 = (FileUpload)FileUpload4;
            Byte[] imgByte4 = null;
            if (img4.HasFile && img4.PostedFile != null)
            {
                //To create a PostedFile
                HttpPostedFile File4 = FileUpload4.PostedFile;
                //Create byte Array with file len
                imgByte4 = new Byte[File4.ContentLength];
                //force the control to load data in array
                File4.InputStream.Read(imgByte4, 0, File4.ContentLength);
                tbl.Image4 = imgByte4;
            }
            else
            {
                tbl.Image4 = null;
            }
            FileUpload img5 = (FileUpload)FileUpload5;
            Byte[] imgByte5 = null;
            if (img5.HasFile && img5.PostedFile != null)
            {
                //To create a PostedFile
                HttpPostedFile File5 = FileUpload5.PostedFile;
                //Create byte Array with file len
                imgByte5 = new Byte[File5.ContentLength];
                //force the control to load data in array
                File5.InputStream.Read(imgByte5, 0, File5.ContentLength);
                tbl.Image5 = imgByte5;
            }
            else
            {
                tbl.Image5 = null;
                imgByte5 = null;
            }
            tbl.Image6 = null;

            // tbl.Insert_Cyto(Convert.ToInt32(Session["Branchid"]));
            Patmst_Bal_C objcontact = new Patmst_Bal_C();
            if (objcontact.getallreadyRegister_RadMst_Histo(tbl.PatRegID, tbl.MTCode))
            {
                sc = new SqlCommand("" +
              "INSERT INTO radmst_Histo " +
              "(PatRegID,FID,MTCode,STCODE,TextDesc,Signatureid,branchid,PID,ResultTemplate,Image1,Image2,Image3,Image4,Image5) " +
              "VALUES (@PatRegID,@FID,@MTCode,@STCODE,@TextDesc,@Signatureid,@branchid,@PID,@ResultTemplate,@Image1,@Image2,@Image3,@Image4,@Image5)", conn);

                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = tbl.PatRegID;
                sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = tbl.Signatureid;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = tbl.FID;
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = tbl.MTCode;
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = tbl.STCODE;
                sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = tbl.TextDesc;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = tbl.P_ResultTemplate;
                if (imgByte1 != null)
                {
                    sc.Parameters.AddWithValue("@Image1", imgByte1);
                }
                else
                {
                    //sc.Parameters.AddWithValue("@Image1", DBNull.Value);
                    SqlParameter imageParameter = new SqlParameter("@Image1", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    sc.Parameters.Add(imageParameter);
                }
                if (imgByte2 != null)
                {
                    sc.Parameters.AddWithValue("@Image2", imgByte2);
                }
                else
                {
                    //sc.Parameters.AddWithValue("@Image2", DBNull.Value);
                    SqlParameter imageParameter = new SqlParameter("@Image2", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    sc.Parameters.Add(imageParameter);
                }
                if (imgByte3 != null)
                {
                    sc.Parameters.AddWithValue("@Image3", imgByte3);
                }
                else
                {
                    // sc.Parameters.AddWithValue("@Image3", DBNull.Value);
                    SqlParameter imageParameter = new SqlParameter("@Image3", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    sc.Parameters.Add(imageParameter);
                }
                if (imgByte4 != null)
                {
                    sc.Parameters.AddWithValue("@Image4", imgByte4);
                }
                else
                {
                    //sc.Parameters.AddWithValue("@Image4", DBNull.Value);
                    SqlParameter imageParameter = new SqlParameter("@Image4", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    sc.Parameters.Add(imageParameter);
                }
                if (imgByte5 != null)
                {
                    sc.Parameters.AddWithValue("@Image5", imgByte5);
                }
                else
                {
                    // sc.Parameters.AddWithValue("@Image5", DBNull.Value);
                    SqlParameter imageParameter = new SqlParameter("@Image5", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    sc.Parameters.Add(imageParameter);
                }
                // sc.Parameters.AddWithValue("@Image6", tbl.Image6);
                sc.Parameters.AddWithValue("@PID", PID);

                conn.Open();
                sc.ExecuteNonQuery();
                conn.Dispose();
            }
        }

            Response.Redirect("Addresult.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&RepType=HistoReport");

    }
    protected void cmdClose_Click(object sender, EventArgs e)
    {

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
            Dfrmst_Bal_C  Dfrmst = new Dfrmst_Bal_C(T_Code, txtTestcode.Text, 0,0);

            Dfrmst.STCODE = T_Code;
            Dfrmst.SDCODE = Request.QueryString["subdeptid"];
            // Dfrmst.Result = Editor.Text.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            Dfrmst.Result = Editor1.Text.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
           // Dfrmst.Result = Dfrmst.Result.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            if (CmbFormatName.Text != "")
                Dfrmst.update_Histo(CmbFormatName.Text, Convert.ToInt32(Session["Branchid"]));
            else
                Dfrmst.update_Histo(txtTestcode.Text, Convert.ToInt32(Session["Branchid"]));

        }
        catch
        {
            Dfrmst_Bal_C Dfrmst = new Dfrmst_Bal_C();
            Dfrmst.Name = txtTestcode.Text;
            Dfrmst.STCODE = T_Code;
            Dfrmst.SDCODE = Request.QueryString["subdeptid"];
            string ffff = Editor.Text;
            // Dfrmst.Result = Editor.Text.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            Dfrmst.Result = Editor1.Text.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
          //  Dfrmst.Result = Dfrmst.Result.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            Dfrmst.Insert_Histo(Convert.ToInt32(Session["Branchid"]));
        }
    }
    protected void txtFormatName_TextChanged(object sender, EventArgs e)
    {
        if (txtFormatName.Text != "")
        {


            DataTable dt = new DataTable();
            TestDescriptiveResult_b ObjTD = new TestDescriptiveResult_b();

            dt = ObjTD.getDefaultResults_search_Histo(MTCode, Convert.ToInt32(Session["Branchid"]), txtFormatName.Text);
            CmbFormatName.DataSource = dt;
            CmbFormatName.DataTextField = "Name";
            CmbFormatName.DataValueField = "Name";
            CmbFormatName.DataBind();

            CmbFormatName.Items.Insert(0, new ListItem("-- Select --", "0"));
            CmbFormatName.SelectedIndex = 0;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       
        int PID = Convert.ToInt32(ViewState["PID"]);
        ViewState["btnsave"] = "";
              ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'TestReportprinting.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"] + "&user=" + Session["usertype"].ToString() + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);


    }
}