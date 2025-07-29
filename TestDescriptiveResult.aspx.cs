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


using System.Drawing.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

using System.Drawing.Imaging;


public partial class TestDescriptiveResult :BasePage
{
    DataTable dt = new DataTable();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    PatSt_Bal_C psnew = new PatSt_Bal_C();
    public bool list = false;
    string TTCode = "";
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
            Session["STCodeT"]=Request.QueryString["MTCode"];
            btnsave.Visible = false;
            TTCode = Request.QueryString["MTCode"].ToString();

            chkAuthorize.Visible = false;
           


        }
        else
        {
            
            PatRegID = Request.QueryString["PatRegID"];
            fid = Request.QueryString["FID"];
            STCodeT = Request.QueryString["MTCode"];
            TTCode = Request.QueryString["MTCode"].ToString();

            chkAuthorize.Visible = true;
           // Editor1.Enabled = false;
        }
       
        Fill_Labels();
        try
        {
            if (!Page.IsPostBack)
            {
               // Editor1.Height = 5500;
               
               // FillListFormat();
                BindDoc();
                if (Request.QueryString["Signatureid"] != null && Request.QueryString["Signatureid"] != "0")
                {
                    ddlDoctor_new.SelectedValue = Convert.ToString(Request.QueryString["Signatureid"]);
                }
                TestDescriptiveResult_b ObjTD = new TestDescriptiveResult_b();
                dt = new DataTable();
                dt = ObjTD.getMaindepartment(STCodeT, Convert.ToInt32(Session["Branchid"]));
                ViewState["SDCode"] = Convert.ToString(dt.Rows[0]["SDCode"]);
                FillListFormat();
                TestDescriptiveResult_b ttm = new TestDescriptiveResult_b(PatRegID, fid, STCodeT, Convert.ToInt32(Session["Branchid"]));
                
               
                GetDate(PatRegID, fid, STCodeT);
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
                //Byte[] bytes = (Byte[])ttm.Image01;
                

                string sRegno = PatRegID.ToString().Trim();

                PatSt_Bal_C pst = new PatSt_Bal_C(sRegno, fid, TTCode, 0, Convert.ToInt32(Session["Branchid"]));

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
        if (Convert.ToString( Session["usertype"]) == "Lab Technician")
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
    public void GetDate(string PatRegID, string fid, string STCodeT)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        conn.Open();

        SqlCommand cmd = new SqlCommand("select * from radmst where PatRegID='" + PatRegID + "' and FID='" + fid + "' and MTCode='" + STCodeT + "' and branchid=" + Convert.ToInt32(Session["Branchid"]) + " ", conn);
            try
            {

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //byte[] img_arr1 = (byte[])dr["image"];
                    //byte[] sig_arr2 = (byte[])dr["signature"];
                    //MemoryStream ms1 = new MemoryStream(img_arr1);
                    //MemoryStream ms2 = new MemoryStream(sig_arr2);
                    //ms1.Seek(0, SeekOrigin.Begin);
                    //ms2.Seek(0, SeekOrigin.Begin);
                    if (Convert.ToString(dr["Image1"]) != "")
                    {
                        Image1.Visible = true;
                        byte[] imagem = (byte[])(dr["Image1"]);
                        string PROFILE_PIC = Convert.ToBase64String(imagem);

                        Image1.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC);
                    }
                    if (Convert.ToString( dr["Image2"]) != "")
                    {
                        Image2.Visible = true;
                        byte[] imagem2 = (byte[])(dr["Image2"]);
                        string PROFILE_PIC2 = Convert.ToBase64String(imagem2);

                        Image2.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC2);
                    }
                    if (Convert.ToString(dr["Image3"]) != "")
                    {
                        Image3.Visible = true;
                        byte[] imagem3 = (byte[])(dr["Image3"]);
                        string PROFILE_PIC3 = Convert.ToBase64String(imagem3);

                        Image3.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC3);
                    }
                    if (Convert.ToString(dr["Image4"]) != "")
                    {
                        Image4.Visible = true;
                        byte[] imagem4 = (byte[])(dr["Image4"]);
                        string PROFILE_PIC4 = Convert.ToBase64String(imagem4);

                        Image4.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC4);
                    }
                    if (Convert.ToString(dr["Image5"]) != "")
                    {
                        Image5.Visible = true;
                        byte[] imagem5 = (byte[])(dr["Image5"]);
                        string PROFILE_PIC5 = Convert.ToBase64String(imagem5);

                        Image5.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC5);
                    }
                   // Image1= (ms1);
                   // pictureBox2.Image = Image.FromStream(ms2);
                }
                else
                {
            // MessageBox.Show("Your Data is not inserted in database try again");
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }    
    
    public void BindDoc()
    {
        Patmstd_Main_Bal_C ObjPCB = new Patmstd_Main_Bal_C();
        DataTable dt = new DataTable();
        if (Convert.ToString(Session["usertype"]) == "Main Doctor")
        {
            dt = ObjPCB.GetallMaindoctor_addresult_Doctor(Convert.ToString(Session["DigModule"]), Convert.ToString(Request.QueryString["subdeptid"]), Convert.ToString(Session["username"]));

        }
        else
        {
            dt = ObjPCB.GetallMaindoctor_addresult(Convert.ToString(Session["DigModule"]), Convert.ToString(Request.QueryString["subdeptid"]));
        }
       // dt = ObjPCB.GetallMaindoctor_addresult(Convert.ToString(Session["DigModule"]), Request.QueryString["subdeptid"]);
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
            tests.SetValue(dr["Name"] , i);
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

            string SDCode = "";
            if (Request.QueryString["subDeptName"] == "Cytology")
            {
                SDCode = "cl";
            }
            if (Request.QueryString["subDeptName"] == "Histopathology")
            {
                SDCode = "HP";
            }
            dt = ttm.getDefaultResults(STCodeT, Convert.ToInt32(Session["Branchid"]), Convert.ToString(ViewState["SDCode"]));
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
            Response.Redirect("Addresult.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["fid"]);
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
        PatSt_Bal_C ObjPBC = new PatSt_Bal_C();
        DataTable dttechid = new DataTable();
        AssignDoctor();
        MainTest_Bal_C tnt = new MainTest_Bal_C(Request.QueryString["MTCode"], Convert.ToInt32(Session["Branchid"]));

        string sRegno = PatRegID.ToString().Trim();

        PatSt_Bal_C ps = new PatSt_Bal_C(sRegno, fid, tnt.MTCode, 0, Convert.ToInt32(Session["Branchid"]));
        int PID = ps.PID;
        ViewState["TechFirst"] = ps.TechnicanFirst;
        if (Editor1.Visible)
        {
            ps.Testdate = Date.getdate();
            ps.TestUser = Session["username"].ToString();
            ps.Patauthicante = "Tested";
            
        }
        dttechid = ObjPBC.GetallTechnican(Convert.ToString(Session["username"]), Convert.ToString(Session["usertype"]));
        if (dttechid.Rows.Count > 0)
        {
            ps.TechnicanFirst = Convert.ToInt32(dttechid.Rows[0]["DRid"]);
        }
        ConvertHtmlToImage();
        if (chkAuthorize.Checked)
        {
            string navURL = "Addresult.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&Patname=Desc";
            if (Request.QueryString["Signatureid"] != null)
            {
                if (Request.QueryString["Signatureid"].Trim() != "")
                {
                    if (((Session["usertype"].ToString() == "Main Doctor") || (Session["usertype"].ToString() == "Administrator") || (Session["usertype"].ToString() == "Admin") || (Session["usertype"].ToString() == "Super Admin")))
                    {
                        ps.TestUser = Session["username"].ToString();
                    }
                    ps.TestUser = Session["username"].ToString();
                    ps.AunticateSignatureId = Convert.ToInt32(Request.QueryString["Signatureid"].Trim());
                    ps.AunticateSignatureId = Convert.ToInt32(ViewState["Signatureid"]);
                    ps.Patauthicante = "Authorized";
                    if (dttechid.Rows.Count > 0)
                    {
                        ps.TechnicanSecond = Convert.ToInt32(dttechid.Rows[0]["DRid"]);
                    }
                    if (Request.QueryString["Signatureid"].Trim() != "0")
                    {
                        ps.AbandantOn = Date.getdate();
                        ps.AbandantBy = Convert.ToString(Session["username"]);
                    }
                    else
                    {
                        ps.AbandantOn = Convert.ToDateTime("01/01/1753");
                        ps.AbandantBy = "";
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
                lblSelectDocError.Text = " authorize not done.";
                return;
            }
        }
        else
        {
            ps.Patauthicante = "Tested";
            ps.AbandantOn = Convert.ToDateTime("01/01/1753");
            ps.AbandantBy = "";
        }
        DataTable dtexistaut = new DataTable();
        dtexistaut = psnew.Check_Authorised_Test(lblRegNo.Text, tnt.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
        if (dtexistaut.Rows[0]["Technicanby"] != "")
        {
            ps.P_Technicanby = Convert.ToString(dtexistaut.Rows[0]["Technicanby"]);
        }
        else
        {

            ps.P_Technicanby = Session["username"].ToString();
        }
        ps.P_DescImagePath = Convert.ToString(ViewState["DescImagePath"]);
        //ps.P_Technicanby = Session["username"].ToString();
        ps.Testdate = Date.getdate();
      //  ps.Update(Convert.ToInt32(Session["Branchid"]));
        if (Convert.ToInt32(ViewState["TechFirst"]) == 0)
        {
            ps.Update(Convert.ToInt32(Session["Branchid"]));
        }
        else
        {
            ps.Update_Second(Convert.ToInt32(Session["Branchid"]));

        }
        
       
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
            at.TestResults = Editor1.Text;//.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
           // at.TestResults = at.TestResults.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

                at.Remarks = "";
          //  at.Insert(Convert.ToInt32(Session["Branchid"]));
        }
        try
        {


            TestDescriptiveResult_b Obj_TDR_C = new TestDescriptiveResult_b(PatRegID, fid, tnt.MTCode, Convert.ToInt32(Session["Branchid"]));

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
            Obj_TDR_C.TextDesc = Editor1.Text; //Mhtml.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            //Obj_TDR_C.TextDesc = Obj_TDR_C.TextDesc.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            Obj_TDR_C.P_ResultTemplate = CmbFormatName.SelectedValue;
            Obj_TDR_C.update(Convert.ToInt32(Session["Branchid"]));
            int IMGCount = 0;
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
                Obj_TDR_C.Image1 = imgByte1;
                // Image1 = imgByte1;
                IMGCount = 1;
            }
            else
            {
                Obj_TDR_C.Image1 = null;
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
                Obj_TDR_C.Image2 = imgByte2;
                IMGCount = 1;
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
                Obj_TDR_C.Image3 = imgByte3;
                IMGCount = 1;
            }
            else
            {
                Obj_TDR_C.Image3 = null;
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
                Obj_TDR_C.Image4 = imgByte4;
                IMGCount = 1;
            }
            else
            {
                Obj_TDR_C.Image4 = null;
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
                Obj_TDR_C.Image5 = imgByte5;
                IMGCount = 1;
            }
            else
            {
                Obj_TDR_C.Image5 = null;
                imgByte5 = null;
            }
            Obj_TDR_C.Image6 = null;

            if (IMGCount > 0)
            {
                SqlConnection conn = DataAccess.ConInitForDC();
                SqlCommand sc = new SqlCommand("" +
                "Update radmst " +
                "Set TextDesc=@TextDesc,Signatureid=@Signatureid,ResultTemplate=@ResultTemplate, Image1=@Image1,Image2=@Image2,Image3=@Image3,Image4=@Image4,Image5=@Image5 " +
                " Where PatRegID=@PatRegID and FID=@FID and MTCode=@MTCode and branchid=" + Convert.ToInt32(Session["Branchid"]) + "", conn);
                SqlDataReader sdr = null;

                //sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
                //sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
                //sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = this.MTCode;
                //sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = this.TextDesc;
                //sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = this.Signatureid;
                //sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.ResultTemplate;
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.PatRegID;
                sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = Obj_TDR_C.Signatureid;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.FID;
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.MTCode;
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.STCODE;
                sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = Obj_TDR_C.TextDesc;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = Obj_TDR_C.P_ResultTemplate;
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
           
            Obj_TDR_C.PatRegID = PatRegID.ToString();
            Obj_TDR_C.MTCode = Request.QueryString["MTCode"];
            Obj_TDR_C.STCODE = Request.QueryString["MTCode"];
            string Mhtml1 = Editor1.Text;

            Obj_TDR_C.TextDesc = Editor1.Text;// Mhtml1.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
           // Obj_TDR_C.TextDesc = Obj_TDR_C.TextDesc.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            Obj_TDR_C.PID = PID;
            Obj_TDR_C.P_ResultTemplate = CmbFormatName.SelectedValue;
          //  Obj_TDR_C.Insert(Convert.ToInt32(Session["Branchid"]));//
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
                Obj_TDR_C.Image1 = imgByte1;
                // Image1 = imgByte1;

            }
            else
            {
                Obj_TDR_C.Image1 = null;
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
                Obj_TDR_C.Image2 = imgByte2;
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
                Obj_TDR_C.Image3 = imgByte3;
            }
            else
            {
                Obj_TDR_C.Image3 = null;
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
                Obj_TDR_C.Image4 = imgByte4;
            }
            else
            {
                Obj_TDR_C.Image4 = null;
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
                Obj_TDR_C.Image5 = imgByte5;
            }
            else
            {
                Obj_TDR_C.Image5 = null;
                imgByte5 = null;
            }
            Obj_TDR_C.Image6 = null;

           

            Patmst_Bal_C ObjPatmst = new Patmst_Bal_C();
            if (ObjPatmst.getallreadyRegister_RadMst(Obj_TDR_C.PatRegID, Obj_TDR_C.MTCode))
            {
                sc = new SqlCommand("" +
              "INSERT INTO radmst " +
              "(PatRegID,FID,MTCode,STCODE,TextDesc,Signatureid,branchid,PID,ResultTemplate,Image1,Image2,Image3,Image4,Image5) " +
              "VALUES (@PatRegID,@FID,@MTCode,@STCODE,@TextDesc,@Signatureid,@branchid,@PID,@ResultTemplate,@Image1,@Image2,@Image3,@Image4,@Image5)", conn);

                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.PatRegID;
                sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = Obj_TDR_C.Signatureid;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.FID;
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.MTCode;
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.STCODE;
                sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = Obj_TDR_C.TextDesc;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = Obj_TDR_C.P_ResultTemplate;
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
                //string Mhtml12 = Editor1.Text;

                //string text = Mhtml12.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");

                //Bitmap bitmap = new Bitmap(1, 1);
                //Font font = new Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Pixel);
                //Graphics graphics = Graphics.FromImage(bitmap);
                //int width = (int)graphics.MeasureString(text, font).Width;
                //int height = (int)graphics.MeasureString(text, font).Height;
                //bitmap = new Bitmap(bitmap, new Size(width, height));
                //graphics = Graphics.FromImage(bitmap);
                //graphics.Clear(Color.White);
                //graphics.SmoothingMode = SmoothingMode.AntiAlias;
                //graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                //graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(255, 0, 0)), 0, 0);
                //graphics.Flush();
                //graphics.Dispose();
                //string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";
               
                //bitmap.Save(Server.MapPath("~/FilesImage/") + fileName, ImageFormat.Jpeg);
                //sc.Parameters.AddWithValue("@Image1", fileName);
                //// imgText.ImageUrl = "~/images/" & fileName
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
                // sc.Parameters.AddWithValue("@Image6", Obj_TDR_C.Image6);
                sc.Parameters.AddWithValue("@PID", PID);

                conn.Open();
                sc.ExecuteNonQuery();
                conn.Dispose();
            }
        }
        lblSelectDocError.Text = " Record Save Successfully.";
        if (Convert.ToString( Request.QueryString["subDeptName"]) == "MICROBIOLOGY")
        {
            PatSt_Bal_C PBC = new PatSt_Bal_C();
            PBC.MTCode = Request.QueryString["MTCode"];
          //  PBC.Update_Microbiologyreport_Rev(1);
        }
       // Response.Redirect("Addresult.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&form=" + Request.QueryString["form"].ToString() + "&user=" + Session["usertype"].ToString() + "&formname=TextMemo");

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
            if (Request.QueryString["subDeptName"] == "Cytology")
            {
                TTCode = "cl";
            }
            if (Request.QueryString["subDeptName"] == "Histopathology")
            {
                TTCode = "HP";
            }
            Dfrmst_Bal_C drt = new Dfrmst_Bal_C(TTCode, txtTestcode.Text);

            drt.STCODE = TTCode;
            string SDCode = "";
            if (Request.QueryString["subDeptName"] == "Cytology")
            {
                drt.STCODE = "cl";
            }
            if (Request.QueryString["subDeptName"] == "Histopathology")
            {
                drt.STCODE = "HP";
            }
            drt.SDCODE = Convert.ToString(Request.QueryString["SDCODE"]);
           // drt.Result = Editor.Text.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            drt.Result = Editor1.Text;//.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
           // drt.Result = drt.Result.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            if (Convert.ToString(drt.Name) != "")
            {
                if (CmbFormatName.Text != "")
                {
                    string FormatName = "";
                    if (CmbFormatName.Text == "0")
                    {
                        FormatName = txtTestcode.Text.Trim();
                    }
                    else
                    {
                        FormatName = CmbFormatName.Text.Trim();
                    }

                    drt.update(FormatName, Convert.ToInt32(Session["Branchid"]));
                }
                else
                    drt.update(txtTestcode.Text.Trim(), Convert.ToInt32(Session["Branchid"]));
            }
            else
            {
              //  Dfrmst_Bal_C drt = new Dfrmst_Bal_C();
                drt.Name = txtTestcode.Text;
                drt.STCODE = TTCode;
                drt.SDCODE = Convert.ToString(Request.QueryString["SDCODE"]);
               // string SDCode = "";
                if (Request.QueryString["subDeptName"] == "Cytology")
                {
                    drt.SDCODE = "cl";
                }
                if (Request.QueryString["subDeptName"] == "Histopathology")
                {
                    drt.SDCODE = "HP";
                }
                // string ffff = Editor.Text;
                // drt.Result = Editor.Text.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
                drt.Result = Editor1.Text.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
                //  drt.Result = drt.Result.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

                drt.Insert(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ddlDoctor_new.SelectedValue));
            }
        }
        catch
        {
            Dfrmst_Bal_C drt = new Dfrmst_Bal_C();
            drt.Name = txtTestcode.Text;
            drt.STCODE = TTCode;
            drt.SDCODE = Convert.ToString(Request.QueryString["SDCODE"]);
            string SDCode = "";
            if (Request.QueryString["subDeptName"] == "Cytology")
            {
                drt.SDCODE = "cl";
            }
            if (Request.QueryString["subDeptName"] == "Histopathology")
            {
                drt.SDCODE = "HP";
            }
           // string ffff = Editor.Text;
           // drt.Result = Editor.Text.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            drt.Result = Editor1.Text;//.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
          //  drt.Result = drt.Result.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            drt.Insert(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ddlDoctor_new.SelectedValue));
        }
    }



    protected void CmbFormatName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbFormatName.SelectedValue != "0")
        {
            string SDCode = "";
            if (Request.QueryString["subDeptName"] == "Cytology")
            {
                TTCode = "cl";
            }
            if (Request.QueryString["subDeptName"] == "Histopathology")
            {
                TTCode = "HP";
            }
            Editor1.Text = new Dfrmst_Bal_C(Convert.ToString(ViewState["SDCode"]), CmbFormatName.SelectedValue).Result.Replace("#FFFFFF", "#000000").Replace("#ffffff", "#000000");
            txtTestcode.Text = new Dfrmst_Bal_C(Convert.ToString(ViewState["SDCode"]), CmbFormatName.SelectedValue).Name;
            txtTestcode.ReadOnly = true;
        }
        else
        {
            Editor1.Text = "";
        }
    }
   

    public void AssignDoctor()
    {
        try
        {
            string MTCode = null;
            string SDCode1 = null;

            MTCode = Request.QueryString["MTCode"];
            MainTest_Bal_C tnt = new MainTest_Bal_C(MTCode, Convert.ToInt32(Session["Branchid"]));

            string sRegno = Request.QueryString["PatRegID"].ToString();

            SDCode1 = tnt.SDCode;

            

            string SDCode = tnt.MTCode.Substring(0, 2);
            PatSt_Bal_C ps = new PatSt_Bal_C(sRegno, Request.QueryString["FID"], SDCode, Convert.ToInt32(Session["Branchid"]));
            ps.MTCode = MTCode;
            ps.PatRegID = sRegno;
            ps.FID = Request.QueryString["FID"].ToString();
            ps.AunticateSignatureId = Convert.ToInt32(ddlDoctor_new.SelectedValue);
            ViewState["Signatureid"] = Convert.ToInt32(ddlDoctor_new.SelectedValue);
            ps.UpdateNew(Convert.ToInt32(Session["Branchid"]));
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
           // lblage.Text = Convert.ToString(CIT.Age) + "/" + CIT.MYD;
            lblage.Text = Convert.ToString(CIT.DOB) + " / " + Convert.ToString(CIT.Age) + "/" + CIT.MYD;
            lblSex.Text = CIT.Sex;

            LblMobileno.Text = CIT.Phone;
            Lblcenter.Text = CIT.CenterName;
            lbldate.Text = Convert.ToString(CIT.Patregdate1);
            LblRefDoc.Text = CIT.Drname;
            Label6.Text = CIT.PatientcHistory;
        }
        catch
        {
            lblRegNo.Visible = true;
            lblRegNo.Text = "Record not foud";
        }
        #endregion
    }
   
    protected void txtFormatName_TextChanged(object sender, EventArgs e)
    {
        if (txtFormatName.Text != "")
        {
           
            DataTable dt = new DataTable();
            TestDescriptiveResult_b ttm = new TestDescriptiveResult_b();

            dt = ttm.getDefaultResults_search(Convert.ToString(ViewState["SDCode"]), Convert.ToInt32(Session["Branchid"]), txtFormatName.Text);
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
        this.cmdSaveClose_Click(null,null);
       // Response.Redirect("TestReportprinting.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&form=" + Request.QueryString["form"].ToString() + "&user=" + Session["usertype"].ToString() + "&formname=TextMemo");

        //int PID = Convert.ToInt32(ViewState["PID"]);
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'DirectTestReportPrinting.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"]+ " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

        ViewState["btnsave"] = "";
       // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'DirectTestReportPrinting.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&form=" + Request.QueryString["form"].ToString() + "&formname=" + Request.QueryString["formname"].ToString() + "&user=" + Session["usertype"].ToString() + "&formname=TextMemo  ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
       // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'DirectTestReportPrinting.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&form=" + Request.QueryString["form"].ToString() + "&formname=" + Request.QueryString["formname"].ToString() + "&user=" + Session["usertype"].ToString() + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/FilesImage/");

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        FileUpload2.SaveAs(folderPath + Path.GetFileName(FileUpload2.FileName));

        //Display the Picture in Image control.
        Image2.ImageUrl = "~/FilesImage/" + Path.GetFileName(FileUpload2.FileName);
    }
    protected void ImageButton2_Click(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/FilesImage/");

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        FileUpload3.SaveAs(folderPath + Path.GetFileName(FileUpload3.FileName));

        //Display the Picture in Image control.
        Image3.ImageUrl = "~/FilesImage/" + Path.GetFileName(FileUpload3.FileName);
    }
    protected void ImageButton3_Click(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/FilesImage/");

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        FileUpload4.SaveAs(folderPath + Path.GetFileName(FileUpload4.FileName));

        //Display the Picture in Image control.
        Image4.ImageUrl = "~/FilesImage/" + Path.GetFileName(FileUpload4.FileName);

    }
    protected void ImageButton4_Click(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/FilesImage/");

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        FileUpload5.SaveAs(folderPath + Path.GetFileName(FileUpload5.FileName));

        //Display the Picture in Image control.
        Image5.ImageUrl = "~/FilesImage/" + Path.GetFileName(FileUpload5.FileName);
    }
    protected void Imgb1_Click(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/FilesImage/");

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

        //Display the Picture in Image control.
        Image1.ImageUrl = "~/FilesImage/" + Path.GetFileName(FileUpload1.FileName);
    }

    public void ConvertHtmlToImage()
    {

        Bitmap bitmap1 = new Bitmap(1, 1);
        string Mhtml1 = Editor1.Text;
        string PDate = DateTime.Now.ToString("ddMMyyyy");
        // string text = Mhtml1.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");

        //      var text =@"<!DOCTYPE html>  
        //                     <html>  
        //                        <head>  
        //                            <style>  
        //                                table {  
        //                                  font-family: arial, sans-serif;  
        //                                  border-collapse: collapse;  
        //                                  width: 100%;  
        //                                }  
        //                                  
        //                                td, th {  
        //                                  border: 1px solid #dddddd;  
        //                                  text-align: left;  
        //                                  padding: 8px;  
        //                                }  
        //                                  
        //                                tr:nth-child(even) {  
        //                                  background-color: #dddddd;  
        //                                }  
        //                          </style>  
        //                         </head>  
        //                    <body>  
        //                      
        //                        <h2>HTML Table</h2>  
        //                          
        //                        <table>  
        //                          <tr>  
        //                            <th>Contact</th>  
        //                            <th>Country</th>  
        //                          </tr>  
        //                          <tr>  
        //                            <td>Kaushik</td>  
        //                            <td>India</td>  
        //                          </tr>  
        //                          <tr>  
        //                            <td>Bhavdip</td>  
        //                            <td>America</td>  
        //                          </tr>  
        //                          <tr>  
        //                            <td>Faisal</td>  
        //                            <td>Australia</td>  
        //                          </tr>  
        //                        </table>  
        //                     </body>  
        //                    </html>";
        //        Bitmap bitmap = new Bitmap(1, 1);
        //        Font font = new Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Pixel);
        //        Graphics graphics = Graphics.FromImage(bitmap);
        //        int width = (int)graphics.MeasureString(text, font).Width;
        //        int height = (int)graphics.MeasureString(text, font).Height;
        //        bitmap = new Bitmap(bitmap, new Size(width, height));
        //        graphics = Graphics.FromImage(bitmap);
        //        graphics.Clear(Color.White);
        //        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        //        graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
        //       // graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(255, 0, 0)), 0, 0);
        //        graphics.DrawString(text, font, new SolidBrush(Color.FromKnownColor(System.Drawing.KnownColor.Black)), 0, 0);
        //        graphics.Flush();
        //        graphics.Dispose();
        //       // string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";
        //        string DFile = "";
        //        string path = Server.MapPath("/" + Request.ApplicationPath + "/DescImageReport/");
        //        DFile = path + "/" + lblRegNo.Text.Trim() + "_" + PDate; ;
        //        string fileName = Path.GetFileNameWithoutExtension(DFile) + ".jpg";
        //        bitmap.Save(Server.MapPath("~/DescImageReport/") + fileName, ImageFormat.Jpeg);



        //===================================
        string DFile = "";
        string html = Editor1.Text.Trim();
        var htmlToImageConv = new NReco.ImageGenerator.HtmlToImageConverter();
        var jpegBytes = htmlToImageConv.GenerateImage(html, "Jpeg");
        //System.IO.File.WriteAllBytes(@"E:\", jpegBytes);
        //string filePath = @"E:\" + "test12.jpeg";
        string pathNew = Server.MapPath("/" + Request.ApplicationPath + "/DescImageReport/");
        DFile = pathNew + "/" + lblRegNo.Text.Trim() +" _" +Convert.ToString(Request.QueryString["MTCode"]) + "_" + PDate + ".jpeg"; ;
        ViewState["DescImagePath"] = DFile;
        File.WriteAllBytes(DFile, jpegBytes);



    }
    protected void FileUpload1_DataBinding(object sender, EventArgs e)
    {
        string folderPath = Server.MapPath("~/FilesImage/");

        //Check whether Directory (Folder) exists.
        if (!Directory.Exists(folderPath))
        {
            //If Directory (Folder) does not exists Create it.
            Directory.CreateDirectory(folderPath);
        }

        //Save the File to the Directory (Folder).
        FileUpload1.SaveAs(folderPath + Path.GetFileName(FileUpload1.FileName));

        //Display the Picture in Image control.
        //Image1.ImageUrl = "~/FilesImage/" + Path.GetFileName(FileUpload1.FileName);

        //byte[] imagem5 = (byte[])(dr["Image5"]);
        //string PROFILE_PIC5 = Convert.ToBase64String(imagem5);

        //Image5.ImageUrl = String.Format("data:image/jpg;base64,{0}", PROFILE_PIC5);
    }
    protected void btnPRints_Click(object sender, EventArgs e)
    {
        if (Editor1.Text == "")
        {
            lblValidate.Text = "Please enter the Result";
            lblValidate.Visible = true;

            Editor1.Focus();
            return;
        }
        PatSt_Bal_C ObjPBC = new PatSt_Bal_C();
        DataTable dttechid = new DataTable();
        AssignDoctor();
        MainTest_Bal_C tnt = new MainTest_Bal_C(Request.QueryString["MTCode"], Convert.ToInt32(Session["Branchid"]));

        string sRegno = PatRegID.ToString().Trim();

        PatSt_Bal_C ps = new PatSt_Bal_C(sRegno, fid, tnt.MTCode, 0, Convert.ToInt32(Session["Branchid"]));
        int PID = ps.PID;
        ViewState["TechFirst"] = ps.TechnicanFirst;
        if (Editor1.Visible)
        {
            ps.Testdate = Date.getdate();
            ps.TestUser = Session["username"].ToString();
            ps.Patauthicante = "Tested";

        }
        dttechid = ObjPBC.GetallTechnican(Convert.ToString(Session["username"]), Convert.ToString(Session["usertype"]));
        if (dttechid.Rows.Count > 0)
        {
            ps.TechnicanFirst = Convert.ToInt32(dttechid.Rows[0]["DRid"]);
        }
        ConvertHtmlToImage();
        if (chkAuthorize.Checked)
        {
            string navURL = "Addresult.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&Patname=Desc";
            if (Request.QueryString["Signatureid"] != null)
            {
                if (Request.QueryString["Signatureid"].Trim() != "")
                {
                    if (((Session["usertype"].ToString() == "Main Doctor") || (Session["usertype"].ToString() == "Administrator") || (Session["usertype"].ToString() == "Admin") || (Session["usertype"].ToString() == "Super Admin")))
                    {
                        ps.TestUser = Session["username"].ToString();
                    }
                    ps.TestUser = Session["username"].ToString();
                    ps.AunticateSignatureId = Convert.ToInt32(Request.QueryString["Signatureid"].Trim());
                    ps.AunticateSignatureId = Convert.ToInt32(ViewState["Signatureid"]);
                    ps.Patauthicante = "Authorized";
                    if (dttechid.Rows.Count > 0)
                    {
                        ps.TechnicanSecond = Convert.ToInt32(dttechid.Rows[0]["DRid"]);
                    }
                    if (Request.QueryString["Signatureid"].Trim() != "0")
                    {
                        ps.AbandantOn = Date.getdate();
                        ps.AbandantBy = Convert.ToString(Session["username"]);
                    }
                    else
                    {
                        ps.AbandantOn = Convert.ToDateTime("01/01/1753");
                        ps.AbandantBy = "";
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
                lblSelectDocError.Text = " authorize not done.";
                return;
            }
        }
        else
        {
            ps.Patauthicante = "Tested";
            ps.AbandantOn = Convert.ToDateTime("01/01/1753");
            ps.AbandantBy = "";
        }
        DataTable dtexistaut = new DataTable();
        dtexistaut = psnew.Check_Authorised_Test(lblRegNo.Text, tnt.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
        if (dtexistaut.Rows[0]["Technicanby"] != "")
        {
            ps.P_Technicanby = Convert.ToString(dtexistaut.Rows[0]["Technicanby"]);
        }
        else
        {

            ps.P_Technicanby = Session["username"].ToString();
        }
        ps.P_DescImagePath = Convert.ToString(ViewState["DescImagePath"]);
        //ps.P_Technicanby = Session["username"].ToString();
        ps.Testdate = Date.getdate();
        //  ps.Update(Convert.ToInt32(Session["Branchid"]));
        if (Convert.ToInt32(ViewState["TechFirst"]) == 0)
        {
            ps.Update(Convert.ToInt32(Session["Branchid"]));
        }
        else
        {
            ps.Update_Second(Convert.ToInt32(Session["Branchid"]));

        }


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
            at.TestResults = Editor1.Text;//.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            // at.TestResults = at.TestResults.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            at.Remarks = "";
            //  at.Insert(Convert.ToInt32(Session["Branchid"]));
        }
        try
        {


            TestDescriptiveResult_b Obj_TDR_C = new TestDescriptiveResult_b(PatRegID, fid, tnt.MTCode, Convert.ToInt32(Session["Branchid"]));

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
            Obj_TDR_C.TextDesc = Editor1.Text; //Mhtml.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            //Obj_TDR_C.TextDesc = Obj_TDR_C.TextDesc.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            Obj_TDR_C.P_ResultTemplate = CmbFormatName.SelectedValue;
            Obj_TDR_C.update(Convert.ToInt32(Session["Branchid"]));
            int IMGCount = 0;
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
                Obj_TDR_C.Image1 = imgByte1;
                // Image1 = imgByte1;
                IMGCount = 1;
            }
            else
            {
                Obj_TDR_C.Image1 = null;
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
                Obj_TDR_C.Image2 = imgByte2;
                IMGCount = 1;
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
                Obj_TDR_C.Image3 = imgByte3;
                IMGCount = 1;
            }
            else
            {
                Obj_TDR_C.Image3 = null;
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
                Obj_TDR_C.Image4 = imgByte4;
                IMGCount = 1;
            }
            else
            {
                Obj_TDR_C.Image4 = null;
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
                Obj_TDR_C.Image5 = imgByte5;
                IMGCount = 1;
            }
            else
            {
                Obj_TDR_C.Image5 = null;
                imgByte5 = null;
            }
            Obj_TDR_C.Image6 = null;

            if (IMGCount > 0)
            {
                SqlConnection conn = DataAccess.ConInitForDC();
                SqlCommand sc = new SqlCommand("" +
                "Update radmst " +
                "Set TextDesc=@TextDesc,Signatureid=@Signatureid,ResultTemplate=@ResultTemplate, Image1=@Image1,Image2=@Image2,Image3=@Image3,Image4=@Image4,Image5=@Image5 " +
                " Where PatRegID=@PatRegID and FID=@FID and MTCode=@MTCode and branchid=" + Convert.ToInt32(Session["Branchid"]) + "", conn);
                SqlDataReader sdr = null;

                //sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
                //sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
                //sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = this.MTCode;
                //sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = this.TextDesc;
                //sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = this.Signatureid;
                //sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.ResultTemplate;
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.PatRegID;
                sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = Obj_TDR_C.Signatureid;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.FID;
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.MTCode;
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.STCODE;
                sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = Obj_TDR_C.TextDesc;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = Obj_TDR_C.P_ResultTemplate;
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

            Obj_TDR_C.PatRegID = PatRegID.ToString();
            Obj_TDR_C.MTCode = Request.QueryString["MTCode"];
            Obj_TDR_C.STCODE = Request.QueryString["MTCode"];
            string Mhtml1 = Editor1.Text;

            Obj_TDR_C.TextDesc = Editor1.Text;// Mhtml1.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            // Obj_TDR_C.TextDesc = Obj_TDR_C.TextDesc.Replace("<p style=\"text-align:center\">", "<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");

            Obj_TDR_C.PID = PID;
            Obj_TDR_C.P_ResultTemplate = CmbFormatName.SelectedValue;
            //  Obj_TDR_C.Insert(Convert.ToInt32(Session["Branchid"]));//
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
                Obj_TDR_C.Image1 = imgByte1;
                // Image1 = imgByte1;

            }
            else
            {
                Obj_TDR_C.Image1 = null;
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
                Obj_TDR_C.Image2 = imgByte2;
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
                Obj_TDR_C.Image3 = imgByte3;
            }
            else
            {
                Obj_TDR_C.Image3 = null;
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
                Obj_TDR_C.Image4 = imgByte4;
            }
            else
            {
                Obj_TDR_C.Image4 = null;
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
                Obj_TDR_C.Image5 = imgByte5;
            }
            else
            {
                Obj_TDR_C.Image5 = null;
                imgByte5 = null;
            }
            Obj_TDR_C.Image6 = null;



            Patmst_Bal_C ObjPatmst = new Patmst_Bal_C();
            if (ObjPatmst.getallreadyRegister_RadMst(Obj_TDR_C.PatRegID, Obj_TDR_C.MTCode))
            {
                sc = new SqlCommand("" +
              "INSERT INTO radmst " +
              "(PatRegID,FID,MTCode,STCODE,TextDesc,Signatureid,branchid,PID,ResultTemplate,Image1,Image2,Image3,Image4,Image5) " +
              "VALUES (@PatRegID,@FID,@MTCode,@STCODE,@TextDesc,@Signatureid,@branchid,@PID,@ResultTemplate,@Image1,@Image2,@Image3,@Image4,@Image5)", conn);

                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.PatRegID;
                sc.Parameters.Add(new SqlParameter("@Signatureid", SqlDbType.Int)).Value = Obj_TDR_C.Signatureid;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.FID;
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.MTCode;
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Obj_TDR_C.STCODE;
                sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.Text)).Value = Obj_TDR_C.TextDesc;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = Obj_TDR_C.P_ResultTemplate;
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
                //string Mhtml12 = Editor1.Text;

                //string text = Mhtml12.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");

                //Bitmap bitmap = new Bitmap(1, 1);
                //Font font = new Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Pixel);
                //Graphics graphics = Graphics.FromImage(bitmap);
                //int width = (int)graphics.MeasureString(text, font).Width;
                //int height = (int)graphics.MeasureString(text, font).Height;
                //bitmap = new Bitmap(bitmap, new Size(width, height));
                //graphics = Graphics.FromImage(bitmap);
                //graphics.Clear(Color.White);
                //graphics.SmoothingMode = SmoothingMode.AntiAlias;
                //graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                //graphics.DrawString(text, font, new SolidBrush(Color.FromArgb(255, 0, 0)), 0, 0);
                //graphics.Flush();
                //graphics.Dispose();
                //string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";

                //bitmap.Save(Server.MapPath("~/FilesImage/") + fileName, ImageFormat.Jpeg);
                //sc.Parameters.AddWithValue("@Image1", fileName);
                //// imgText.ImageUrl = "~/images/" & fileName
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
                // sc.Parameters.AddWithValue("@Image6", Obj_TDR_C.Image6);
                sc.Parameters.AddWithValue("@PID", PID);

                conn.Open();
                sc.ExecuteNonQuery();
                conn.Dispose();
            }
        }
        lblSelectDocError.Text = " Record Save Successfully.";
        if (Convert.ToString(Request.QueryString["subDeptName"]) == "MICROBIOLOGY")
        {
            PatSt_Bal_C PBC = new PatSt_Bal_C();
            PBC.MTCode = Request.QueryString["MTCode"];
            //  PBC.Update_Microbiologyreport_Rev(1);
        }
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'DirectTestReportPrinting_Img.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&fid=" + Request.QueryString["fid"] + "&MTCode=" + Request.QueryString["MTCode"] + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

    }
}