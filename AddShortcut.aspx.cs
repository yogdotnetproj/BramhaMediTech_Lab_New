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
using System.Configuration;
public partial class AddShortcut : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        lblnote.Visible = false;

        if (!IsPostBack)
        {
            try
            {
                FillMaintest();

                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                   // checkexistpageright("AddShortcut.aspx");
                }
                
                if (Request.QueryString["Shortformid"] != null)
                {

                    int shid = Convert.ToInt32(Request.QueryString["Shortformid"].ToString());
                    ViewState["shid"] = shid;
                    IEnumerator icol = Shformmst_Bal_C.getGrideValuebyshortform(shid, Session["Branchid"]).GetEnumerator();
                    if (icol.MoveNext())
                    {
                        Stformmst_Main_Bal_C sft = (Stformmst_Main_Bal_C)icol.Current;
                        txtShortform.Text = sft.Shortform;
                        txtDescription.Text = sft.Description;
                       ddMaintest.SelectedValue= sft.MainTest;
                       fillTestCode();   
                       ddlParametercode.SelectedValue = sft.SubTest;

                    }
                    if (Request.QueryString["Description"] != null)
                    {


                        txtShortform.Enabled = false;
                        txtDescription.Enabled = false;
                        btnsave.Visible = false;
                    }
                    else
                    {
                        ViewState["saveedit"] = "Edit";
                        btnsave.Text = "Update";
                        btnsave.ToolTip = "Update";

                    }
                }

                if (Request.QueryString["Shortformid"] == null)
                {
                    ViewState["saveedit"] = "Save";

                    btnsave.ToolTip = "Save";

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
    }
 


    public void clear()
    {
        txtShortform.Text = "";
        txtDescription.Text = "";
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {
            if (txtShortform.Text.Trim() == "")
            {
                lblnote.Visible = true;

                lblnote.ForeColor = System.Drawing.Color.Red;
                lblnote.Text = "Enter short form.";

            }
            else
            {
                if (ViewState["saveedit"].ToString() == "Edit")
                {
                    Stformmst_Main_Bal_C sft = new Stformmst_Main_Bal_C();
                    string testName = "";

                    sft.Update(txtShortform.Text.Trim(), txtDescription.Text.Trim(), "", Convert.ToInt32(ViewState["shid"]), testName,ddMaintest.SelectedValue,ddlParametercode.SelectedValue);
                    lblnote.Visible = true;
                    lblnote.ForeColor = System.Drawing.Color.Green;

                    lblnote.Text = "Record is Updated  Successfully";
                    clear();
                    Response.Redirect("ShortCut.aspx");

                }
                else
                {

                    Stformmst_Main_Bal_C sft = new Stformmst_Main_Bal_C();
                    sft.Shortform = txtShortform.Text;
                    sft.Description = txtDescription.Text;
                    sft.MainTest = ddMaintest.Text;
                    sft.SubTest = ddlParametercode.Text;
                    if (Session["Branchid"] != null)
                        sft.branchid = Convert.ToInt32(Session["Branchid"]);

                    if (MainTestLog_Bal_C.isExistsShortCut(txtShortform.Text.Trim(), Convert.ToInt32(Session["Branchid"])))
                    {
                        lblnote.ForeColor = System.Drawing.Color.Red;
                        lblnote.Visible = true;
                        lblnote.Text = " Short Form already exist";
                        txtShortform.Focus();
                        return;
                    }
                    else
                    {
                        sft.Insert();
                        lblnote.ForeColor = System.Drawing.Color.Green;
                        lblnote.Visible = true;
                        lblnote.Text = " Record is Inserted Successfully";
                        clear();
                    }

                    Response.Redirect("ShortCut.aspx");
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShortCut.aspx");
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

    private void FillMaintest()
    {
        try
        {
            ddMaintest.DataSource = MainTestLog_Bal_C.GetAllMaintest_SDCode("", Convert.ToInt32(Session["Branchid"]));
            ddMaintest.DataTextField = "Maintestname";
            ddMaintest.DataValueField = "MTCode";
           
            ddMaintest.DataBind();
            ddMaintest.Items.Insert(0, "Select Maintestname");
           // ddMaintest.SelectedIndex = -1;
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

    private void fillTestCode()
    {
        try
        {
            ddlParametercode.DataSource = SubTestLog_Bal_C.Get_AllSubTest(ddMaintest.SelectedValue.ToString(), Convert.ToInt32(Session["Branchid"]));
            ddlParametercode.DataTextField = "TestName";
            ddlParametercode.DataValueField = "STCODE";

            ddlParametercode.DataBind();
            ddlParametercode.Items.Insert(0, "SelectTest");
           // ddlParametercode.Items[0].Value = "";
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
    protected void ddMaintest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {            
                fillTestCode();           
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