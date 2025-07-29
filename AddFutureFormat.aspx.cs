using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using Winthusiasm.HtmlEditor;
using System.Web.Management;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Xml;
using System.Text;
using System.Threading;
using System.IO;

public partial class AddFutureFormat :BasePage
{
    DataTable dt = new DataTable();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    public bool list = false;
    string TestCode = "";
    public string FID = "18";
    private string testnametm;
    public string Testnametm
    {
        get { return testnametm; }
        set { testnametm = value; }
    }
   
    private string fid;
    public string Fid
    {
        get { return fid; }
        set { fid = value; }
    }
   
    protected void Page_Load(object sender, EventArgs e)
    {


        if (Request.QueryString["STCode"] != null )
        {
            TestCode = Convert.ToString( Request.QueryString["STCode"]);         

        }
       
       
        try
        {
            if (!Page.IsPostBack)
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    //checkexistpageright("AddFutureFormat.aspx");
                }
                
                FillListFormat();  
            }

        }
        catch
        {


        }      
       

    }
  
    private void FillListFormat()
    {

        try
        {
            DataTable dt = new DataTable();
            TestDescriptiveResult_b ttm = new TestDescriptiveResult_b();

            dt = ttm.getDefaultResults(TestCode, Convert.ToInt32(Session["Branchid"]),"");
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
            Dfrmst_Bal_C drt = new Dfrmst_Bal_C(TestCode, txtTestcode.Text);


            drt.STCODE = TestCode;
            string FName = drt.Name;
            if (FName != "")
            {
                // drt.Result = Editor.Text.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
                drt.Result = Editor1.Text.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
                if (CmbFormatName.Text != "")
                    drt.update(CmbFormatName.Text, Convert.ToInt32(Session["Branchid"]));
                else
                    drt.update(txtTestcode.Text, Convert.ToInt32(Session["Branchid"]));

                lblValidate.Text = "Format save successfully";
                lblValidate.Visible = true;
            }
            else
            {
                Dfrmst_Bal_C drt1 = new Dfrmst_Bal_C();
                drt1.Name = txtTestcode.Text;
                drt1.STCODE = TestCode;

                string ffff = Editor.Text;
                // drt.Result = Editor.Text.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
                drt1.Result = Editor1.Text.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");

                drt1.Insert(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["Signatureid"]));
                lblValidate.Text = "Format save successfully";
                lblValidate.Visible = true;
            }

        }
        catch
        {
            //Dfrmst_Bal_C drt = new Dfrmst_Bal_C();
            //drt.Name = txtTestcode.Text;
            //drt.STCODE = TestCode;

            //string ffff = Editor.Text;
            //// drt.Result = Editor.Text.Replace("</p>", "&nbsp;").Replace("<p>", "<br/>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");
            //drt.Result = Editor1.Text.Replace("<span style=" + "font-weight bold" + ">", "<b>").Replace("<p class=" + "MsoNormal" + " style=" + "text-align: center" + ">", "> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("style=" + "text-align: center" + ">", "&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;").Replace("<br />", "<br>").Replace("<br/>", "<br>").Replace("<strong>", "<b>").Replace("</strong>", "</b>").Replace("<STRONG>", "<b>").Replace("</STRONG>", "</b>").Replace("<em>", "<i>").Replace("</em>", "</i>").Replace("#000000", "#FFFFFF").Replace("#000000", "#ffffff");

            //drt.Insert(Convert.ToInt32(Session["Branchid"]));
            //lblValidate.Text = "Format save successfully";
            //lblValidate.Visible = true;
        }
    }



    protected void CmbFormatName_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmbFormatName.SelectedValue != "")
        {

            Editor1.Text = new Dfrmst_Bal_C(TestCode, CmbFormatName.SelectedValue).Result.Replace("#FFFFFF", "#000000").Replace("#ffffff", "#000000");
            txtTestcode.Text = new Dfrmst_Bal_C(TestCode, CmbFormatName.SelectedValue).Name;
            txtTestcode.ReadOnly = true;
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