using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.Management;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Configuration;
public partial class useradd : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    createuserTable_Bal_C Objcutb = new createuserTable_Bal_C();
    createuserTable_Bal_C Obj_CTB_C = new createuserTable_Bal_C();
    string errst;
    protected void Page_Load(object sender, EventArgs e)
    {

        txtuname.Attributes.Add("onkeyup", "GetPwd();");
        if (!this.IsPostBack)
        {
            if (Convert.ToString(Session["usertype"]) != "Administrator")
            {
               // checkexistpageright("useradd.aspx");
            }
            filldrop();
            filllab();
            ViewState["Save"] = "save";
            if (Request.QueryString["id"] != null)
            {
                ViewState["Save"] = "Edit";
                EditData();
            }
        }
    }
 
    [WebMethod]
    [ScriptMethod]
    public static string[] FillInfo(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();

        string collectioncode = HttpContext.Current.Session["CenterCode"].ToString();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
        {

            sda = new SqlDataAdapter("select DoctorCode,DoctorName from DrMT where (DoctorName like N'" + prefixText + "%')  or (DoctorCode like N'%" + prefixText + "%')   ", con);
        }
        else
        {
            sda = new SqlDataAdapter("select DoctorCode,DoctorName from DrMT where (DoctorName like N'" + prefixText + "%')  or (DoctorCode like N'%" + prefixText + "%')   ", con);
        }
            DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["DoctorCode"] + " = " + dr["DoctorName"], i);
            i++;
        }

        return tests;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Server.Transfer("~/Adduser.aspx");
    }
    public void EditData()
    {
        DataTable dt = new DataTable();
        dt = Obj_CTB_C.Get_AlluserDetails(Convert.ToInt32(Request.QueryString["id"]), Convert.ToInt32(Session["Branchid"].ToString()));
        if (dt.Rows.Count > 0)
        {
            txtmobile.Text = Convert.ToString(dt.Rows[0]["MobileNo"]);
            txtphone.Text = Convert.ToString(dt.Rows[0]["PhoneNumber"]);
            txtemail.Text = Convert.ToString(dt.Rows[0]["Email"]);
            txtuname.Text = Convert.ToString(dt.Rows[0]["username"]);
            txtpwd.Text = Convert.ToString(dt.Rows[0]["password"]);
            ddltype.SelectedValue = Convert.ToString(dt.Rows[0]["RollId"]);
            ddltype.SelectedItem.Text = Convert.ToString(dt.Rows[0]["Usertype"]);
            ddldept.SelectedValue = Convert.ToString(dt.Rows[0]["DigModule"]);
            if (Convert.ToInt32(dt.Rows[0]["DRid"]) > 0)
            {
                ddlempname.Text = Convert.ToString(dt.Rows[0]["Name"]);
                ddlempname.Visible = true;
                ddlempname1.Visible = false;
                RblDType.SelectedValue = "1";
                DocDeg.Visible = true;
                txtDoctorDegree.Text = Convert.ToString(dt.Rows[0]["Degree"]);
                ViewState["DoctorId"] = Convert.ToString(dt.Rows[0]["DRid"]);
                //Image1.ImageAlign=
            }
            else
            {
                ddlempname1.Text = Convert.ToString(dt.Rows[0]["Name"]);
                ddlempname.Visible = false;
                ddlempname1.Visible = true;
                RblDType.SelectedValue = "0";
                ViewState["DoctorId"] = "0";
            }
            if (Convert.ToString(dt.Rows[0]["CenterCode"]) !="0")
            {
                ddlCentercode.SelectedItem.Text = Convert.ToString(dt.Rows[0]["CenterCode"]);
                Collid.Visible = true;
            }
            else
            {
                ddlCentercode.SelectedItem.Text = Convert.ToString(dt.Rows[0]["CenterCode"]);
                //Collid.Visible = false;
            }
            if (Convert.ToBoolean(dt.Rows[0]["FrontDiskDisc"]) ==true)
            {
                ChkFronddisk.Checked = true;
            }
            else
            {
                ChkFronddisk.Checked = false;
            }
            if (Convert.ToBoolean(dt.Rows[0]["BillDiskDisc"]) == true)
            {
                ChkBillDesk.Checked = true;
            }
            else
            {
                ChkBillDesk.Checked = false;
            }
            txtuname.Enabled = false;
            ViewState["Save"] = "Edit";
        }

    }
    protected void CmdSave_Click(object sender, EventArgs e)
    {
        try
        {
           
                if (txtuname.Text.Trim() == "" && txtpwd.Text.Trim() == "")
                {
                    LBLMsg.Text = "Enter user name or password";
                    return;
                }
                else
                { errst = ""; }
                int mx = 0, CheckUsertype = 0;
                dt = dc.ReadTable("select * from CTUser where username='" + txtuname.Text + "'");
                if (dt.Rows.Count > 5)
                {
                    LBLMsg.Text = "User Already Exists";
                }
                else
                {
                    if (RblDType.SelectedValue == "1")
                    {
                        CheckUsertype = 1;
                    }
                    else
                    {
                        ddlempname.Text = ddlempname1.Text;
                        CheckUsertype = 0;
                    }
                    //if (ddldept.SelectedIndex != 0 && ddlempname.Text != "")
                    //{
                        string Coolcode = "";
                        if (ddlCentercode.SelectedIndex > 0)
                        {
                            Coolcode = ddlCentercode.SelectedValue;
                        }
                        else
                        {
                            Coolcode = "0";
                        }

                        string UName = "";
                        if (Convert.ToString(ViewState["Save"]) == "save")
                        {
                            UName = txtuname.Text;
                        }
                        else
                        {
                            UName = "NNNNN";
                        }
                        if (createuserTable_Bal_C.isUserNameExists(UName))
                        {
                           
                            LBLMsg.Text = "User Already Exists.";
                            return; 
                        }
                        else
                        {                          
                            Obj_CTB_C.Name = txtuname.Text;
                            Obj_CTB_C.Username = txtuname.Text;
                            Obj_CTB_C.Password = txtpwd.Text;
                            if (ddltype.SelectedItem.Text != "Select UserType")
                            { Obj_CTB_C.Usertype = ddltype.SelectedItem.Text;
                            Obj_CTB_C.P_RoleId = Convert.ToInt32( ddltype.SelectedValue); 
                            }
                            else
                            { Obj_CTB_C.Usertype = "";
                            Obj_CTB_C.P_RoleId = 0;
                            }

                            Obj_CTB_C.P_branchid = Convert.ToInt32(Session["Branchid"].ToString());
                          
                            if (ddlCentercode.SelectedIndex > 0)
                            {
                                Obj_CTB_C.UnitCode = ddlCentercode.SelectedValue;
                            }
                            else
                            {
                                Obj_CTB_C.UnitCode = ddlLab.SelectedValue;
                            }

                            if (ddldept.SelectedItem.Text.ToUpper() == "RADIOLOGY")
                            {
                                Obj_CTB_C.maindeptid = 1;
                            }
                            if (ddldept.SelectedItem.Text.ToUpper() == "PATHOLOGY")
                            {
                                Obj_CTB_C.maindeptid = 2;
                            }
                            if (ddldept.SelectedItem.Text == "Select Department")
                            {
                                Obj_CTB_C.maindeptid = 0;
                            }
                            Obj_CTB_C.P_Email = txtemail.Text;
                            Obj_CTB_C.P_PhoneNo = txtphone.Text;
                            Obj_CTB_C.P_MobileNo = txtmobile.Text;
                            if (ChkFronddisk.Checked == true)
                            {
                                Obj_CTB_C.P_FrontDesk = 1;
                            }
                            else
                            {
                                Obj_CTB_C.P_FrontDesk = 0;
                            }
                            if (ChkBillDesk.Checked == true)
                            {
                                Obj_CTB_C.P_BillDesk = 1;
                            }
                            else
                            {
                                Obj_CTB_C.P_BillDesk = 0;
                            }
                            if (RblDType.SelectedValue == "0")
                            {
                                Obj_CTB_C.CenterCode = Convert.ToString(ddlCentercode.SelectedValue);
                                Obj_CTB_C.Name = ddlempname.Text;
                                if (Convert.ToString(ViewState["Save"]) != "save")
                                {

                                    Obj_CTB_C.Insert_UpdateCTUser(Convert.ToInt32(Request.QueryString["id"]));
                                }
                                else
                                {
                                    Obj_CTB_C.Insert();//
                                }
                            }
                            else
                            {
                                int mno = Obj_CTB_C.getMaxNumber_signid(Convert.ToInt32(Session["Branchid"]));
                                Obj_CTB_C.Drid = mno;
                                Obj_CTB_C.Drid = mno;
                                Obj_CTB_C.CenterCode = Convert.ToString("");
                                if (ddltype.SelectedItem.Text == "Reference Doctor")
                                {
                                    Obj_CTB_C.Name = Convert.ToString( ViewState["DocCode"]);
                                }
                                else
                                {
                                    Obj_CTB_C.Name = ddlempname.Text;
                                }
                                Obj_CTB_C.Username = Convert.ToString(Session["username"]);
                                Obj_CTB_C.Username = Convert.ToString(txtuname.Text);
                                Obj_CTB_C.CenterCode = "0";
                                
                                if (Convert.ToString(ViewState["Save"]) != "save")
                                {

                                    Obj_CTB_C.Insert_UpdateCTUser(Convert.ToInt32(Request.QueryString["id"]));
                                    Obj_CTB_C.Degree = txtDoctorDegree.Text;
                                    FileUpload img = (FileUpload)FUFileUpload;
                                    Byte[] imgByte = null;
                                    if (img.HasFile && img.PostedFile != null)
                                    {
                                        //To create a PostedFile
                                        HttpPostedFile File = FUFileUpload.PostedFile;
                                        //Create byte Array with file len
                                        imgByte = new Byte[File.ContentLength];
                                        //force the control to load data in array
                                        File.InputStream.Read(imgByte, 0, File.ContentLength);
                                        SqlConnection conn = DataAccess.ConInitForDC();
                                        SqlCommand sc = new SqlCommand("update   DRST set signatureid=@signatureid,Drsignature=@Drsignature,[username]=@username,[branchid]=@branchid,Degree=@Degree,signImage=@signImage,DrSign2=@Degree where signatureid= '" + ViewState["DoctorId"] + "'  ", conn);
                                       
                                        sc.Parameters.AddWithValue("@signatureid", ViewState["DoctorId"]);
                                        sc.Parameters.AddWithValue("@Drsignature", Obj_CTB_C.Name);
                                        sc.Parameters.AddWithValue("@username", Obj_CTB_C.Username);
                                        sc.Parameters.AddWithValue("@branchid", Obj_CTB_C.P_branchid);
                                        sc.Parameters.AddWithValue("@Degree", Obj_CTB_C.Degree);
                                        sc.Parameters.AddWithValue("@signImage", imgByte);

                                        conn.Open();
                                        sc.ExecuteNonQuery();
                                        conn.Dispose();
                                    }
                                    else
                                    {
                                        // Obj_CTB_C.SignPic = imgByte;
                                        SqlConnection conn = DataAccess.ConInitForDC();
                                        SqlCommand sc = new SqlCommand("update   DRST set signatureid=@signatureid,Drsignature=@Drsignature,[username]=@username,[branchid]=@branchid,Degree=@Degree,DrSign2=@Degree where signatureid= '" + ViewState["DoctorId"] + "'  ", conn);
                                        
                                        sc.Parameters.AddWithValue("@signatureid", ViewState["DoctorId"]);
                                        sc.Parameters.AddWithValue("@Drsignature", Obj_CTB_C.Name);
                                        sc.Parameters.AddWithValue("@username", Obj_CTB_C.Username);
                                        sc.Parameters.AddWithValue("@branchid", Obj_CTB_C.P_branchid);
                                        sc.Parameters.AddWithValue("@Degree", Obj_CTB_C.Degree);
                                      //  sc.Parameters.AddWithValue("@SignPicture", imgByte);

                                        conn.Open();
                                        sc.ExecuteNonQuery();
                                        conn.Dispose();
                                    }
                                    
                                }
                                else
                                {
                                    Obj_CTB_C.Degree = txtDoctorDegree.Text;
                                   

                                    FileUpload img = (FileUpload)FUFileUpload;
                                    Byte[] imgByte = null;
                                    if (img.HasFile && img.PostedFile != null)
                                    {
                                        //To create a PostedFile
                                        HttpPostedFile File = FUFileUpload.PostedFile;
                                        //Create byte Array with file len
                                        imgByte = new Byte[File.ContentLength];
                                        //force the control to load data in array
                                        File.InputStream.Read(imgByte, 0, File.ContentLength);
                                        SqlConnection conn = DataAccess.ConInitForDC();
                                        SqlCommand sc = new SqlCommand("Insert into DRST (signatureid,Drsignature,[username],[branchid],Degree,signImage,DrSign2) " +
                                      " values(@signatureid,@Drsignature,@username,@branchid,@Degree,@signImage,@Degree)", conn);
                                        sc.Parameters.AddWithValue("@signatureid", Obj_CTB_C.Drid);
                                        sc.Parameters.AddWithValue("@Drsignature", Obj_CTB_C.Name);
                                        sc.Parameters.AddWithValue("@username", Obj_CTB_C.Username);
                                        sc.Parameters.AddWithValue("@branchid", Obj_CTB_C.P_branchid);
                                        sc.Parameters.AddWithValue("@Degree", Obj_CTB_C.Degree);

                                        sc.Parameters.AddWithValue("@signImage", imgByte);

                                        conn.Open();
                                        sc.ExecuteNonQuery();
                                        conn.Dispose();
                                    }
                                    else
                                    {
                                        // Obj_CTB_C.SignPic = imgByte;
                                        SqlConnection conn = DataAccess.ConInitForDC();
                                        SqlCommand sc = new SqlCommand("Insert into DRST (signatureid,Drsignature,[username],[branchid],Degree,DrSign2) " +
                                      " values(@signatureid,@Drsignature,@username,@branchid,@Degree,@Degree)", conn);
                                        sc.Parameters.AddWithValue("@signatureid", Obj_CTB_C.Drid);
                                        sc.Parameters.AddWithValue("@Drsignature", Obj_CTB_C.Name);
                                        sc.Parameters.AddWithValue("@username", Obj_CTB_C.Username);
                                        sc.Parameters.AddWithValue("@branchid", Obj_CTB_C.P_branchid);
                                        sc.Parameters.AddWithValue("@Degree", Obj_CTB_C.Degree);

                                       // sc.Parameters.AddWithValue("@SignPicture", imgByte);

                                        conn.Open();
                                        sc.ExecuteNonQuery();
                                        conn.Dispose();
                                    }
                                   
                                    Obj_CTB_C.Insert();//
                                }
                               
                            }
                            clear();
                           // Server.Transfer("~/Adduser.aspx");
                            Response.Redirect("~/Adduser.aspx",false);
                        }
                   
                }
          
        }
        catch (Exception ex)
        {
            if (ex.Message.Equals("Exception aborted."))
            {
                return;
            }
            else
            {
                Response.Cookies["error"].Value = ex.Message;
                Server.Transfer("~/ErrorMessage.aspx");
            }
        }
    }
    public void filldrop()
    {
       
        ddldept.DataSource = dc.FillDept_New(Session["Branchid"].ToString(), "2");
        ddldept.DataValueField = "deptid";
        ddldept.DataTextField = "deptname";
        ddldept.DataBind();
        ddldept.Items.Insert(0, "Select Department");
        ddldept.Items[0].Value = "0";
        ddldept.SelectedIndex = 0;

        ddlCentercode.DataSource = dc.Fill_CenterCode(Session["Branchid"].ToString(), "2");
        ddlCentercode.DataValueField = "DoctorCode";
        ddlCentercode.DataTextField = "DoctorName";
        ddlCentercode.DataBind();
        ddlCentercode.Items.Insert(0, "Select Center");
        ddlCentercode.Items[0].Value = "0";
        ddlCentercode.SelectedIndex = 0;


        ddltype.DataSource = dc.FillUserroles(Session["Branchid"].ToString(), "2");
        ddltype.DataValueField = "ROLLID";
        ddltype.DataTextField = "ROLENAME";
        ddltype.DataBind();
        ddltype.Items.Insert(0, "Select UserType");
        ddltype.Items[0].Value = "0";
        ddltype.SelectedIndex = -1;
    }
    public void filllab()
    {
        ddlLab.DataSource = dc.FillLab(Session["Branchid"].ToString());
        ddlLab.DataValueField = "DoctorCode";
        ddlLab.DataTextField = "DoctorName";
        ddlLab.DataBind();
        ddlLab.Items.Insert(0, "select");
        ddlLab.Items[0].Value = "";
        ddlLab.SelectedIndex = 0;


    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
    }


    protected void lnkavail_Click(object sender, EventArgs e)
    {
        dt = dc.ReadTable("select * from CTuser where username='" + txtuname.Text + "'");
        if (dt.Rows.Count > 0)
        {
            lbluser.Visible = true;
            txtuname.Text = "";
            txtpwd.Text = "";
        }
        else
        {
            lbluser.Visible = false;
        }
    }
    public void clear()
    {
        txtpwd.Text = "";
        txtuname.Text = "";
        txtphone.Text = "";
        txtmobile.Text = "";
        txtemail.Text = "";
       // txtaddress.Text = "";

    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddltype.SelectedItem.Text == "Collection Center" || ddltype.SelectedItem.Text == "CollectionCenter")
        {
            Collid.Visible = true;
            DocDeg.Visible = false;
          //  DocDegsig.Visible = false;
        }
        if (ddltype.SelectedItem.Text == "Main Doctor" || ddltype.SelectedItem.Text == "MainDoctor" || ddltype.SelectedItem.Text=="Technician")
        {
            DocDeg.Visible = true;
           // DocDegsig.Visible = true;
           // Collid.Visible = false;
        }
    }
    protected void ddlempname_TextChanged(object sender, EventArgs e)
    {
        if (ddlempname.Text != "")
        {
            string empnameT = "";
            string[] empname = ddlempname.Text.Split('=');
            if (RblDType.SelectedValue != "0")
            {
                if (empname.Length > 1)
                {
                    ViewState["DocCode"] = empname[0];
                    ViewState["DocName"] = empname[1];
                    ddlempname.Text = empname[1];
                }
                else
                {
                    ViewState["DocCode"] = "";
                    ViewState["DocName"] = "";
                }
            }
            else
            {
                ViewState["DocCode"] = "";
                ViewState["DocName"] = "";

            }
        }

    }
    protected void RblDType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RblDType.SelectedValue == "1")
        {
            ddlempname.Visible = true;
            ddlempname1.Visible = false;
        }
        else
        {
            ddlempname.Visible = false;
            ddlempname1.Visible = true;
        }
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        string sql = "";
        ReportParameterClass.ReportType = "UserDetaila";


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_UserDetaila.rpt");
        Session["reportname"] = "UserDetaila";
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