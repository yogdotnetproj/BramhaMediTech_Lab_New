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

public partial class ShortCut :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                   // checkexistpageright("ShortCut.aspx");
                }

                Label1.Visible = false;
                GVShortcut.AutoGenerateColumns = false;
                bindgrid();
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
  
    void bindgrid()
    {
        try
        {
            GVShortcut.DataSource = Shformmst_Bal_C.get_stformmstValue(Session["Branchid"], Convert.ToInt32(Session["Branchid"]));
            GVShortcut.DataBind();
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/AddShortcut.aspx");
    }
    protected void GVShortcut_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;

    }
    protected void GVShortcut_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int sid1 = Convert.ToInt32(GVShortcut.DataKeys[e.RowIndex].Value);
            Stformmst_Main_Bal_C sft = new Stformmst_Main_Bal_C();

            sft.deleteGenShortForm(Session["Branchid"], Convert.ToInt32(sid1));
            bindgrid();
            Label1.Text = "Record Deleted successfully";

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
    protected void GVShortcut_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVShortcut.PageIndex = e.NewPageIndex;
            bindgrid();
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
    protected void Btn_Add_Dept_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubDeptAdd.aspx", false);
    }
    protected void Btn_Add_Test_Click(object sender, EventArgs e)
    {

        // Response.Redirect("ShowTest.aspx", false);
        Response.Redirect("~/AddTest.aspx", false);
    }
    protected void Btn_Add_NR_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReferanceRange.aspx", false);
    }
    protected void Btn_Add_PK_Click(object sender, EventArgs e)
    {
        Response.Redirect("Showpackage.aspx", false);
    }
    protected void Btn_Add_Sample_Click(object sender, EventArgs e)
    {
        Response.Redirect("SampleType.aspx", false);

    }
    protected void Btn_Add_ShortCut_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShortCut.aspx", false);
    }
    protected void Btn_Add_Formula_Click(object sender, EventArgs e)
    {
        Response.Redirect("TestFormulasetting.aspx", false);
    }
    protected void Btn_Add_RN_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportNote.aspx", false);
    }
    protected void btnedittest_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowTest.aspx", false);
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