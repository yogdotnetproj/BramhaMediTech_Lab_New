using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;

public partial class Assignsubdept :BasePage
{
    dbconnection dc = new dbconnection();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["HMS"]) != "Yes")
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("Assignsubdept.aspx");
                }
            }

            bindusertype();
            bindchecklist();
          
        }
    }
    public void bindchecklist()
    {
        chksubdept.DataSource = ObjTB.Get_subdepartment();
       
        chksubdept.DataTextField = "subdeptName";
        chksubdept.DataValueField = "subdeptid";
        chksubdept.DataBind();
    }
    public void bindusertype()
    {
        ddlUsertype.DataSource = ObjTB.Get_maindoctor();
        ddlUsertype.DataValueField = "username";
        ddlUsertype.DataTextField = "Name";
        ddlUsertype.DataBind();
        ddlUsertype.Items.Insert(0, "Select doctor");
        ddlUsertype.Items[0].Value = "0";
        ddlUsertype.SelectedIndex = -1;
    }
   
   
    protected void ddlUsertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }

  
    protected void btnsave_Click(object sender, EventArgs e)
    {
        ObjTB.deletesubdept(Convert.ToString(Session["username"]));
        foreach (ListItem li in chksubdept.Items)
        {

            if (li.Selected)
            {
                int id = Convert.ToInt32( li.Value) ;               
                ObjTB.Insert_Deptwiseuser(id, ddlUsertype.SelectedValue, Convert.ToString(Session["username"]), 1);
            }

        }
        BindGrid();
        bindchecklist();
    }
    protected void GV_userrollright_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GV_userrollright.PageIndex = e.NewPageIndex;
            BindGrid();
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
    public void BindGrid()
    {
        DataTable dt = new DataTable();
        dt = ObjTB.bindassignsubdept(ddlUsertype.SelectedValue);
        if (dt.Rows.Count > 0)
        {
            GV_userrollright.DataSource = dt;
            GV_userrollright.DataBind();
        }
        else
        {
            GV_userrollright.DataSource = null;
            GV_userrollright.DataBind();
        }

    }
   
    protected void GV_userrollright_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string DeptCodeID = GV_userrollright.Rows[e.RowIndex].Cells[1].Text;
            ObjTB.Delete(Convert.ToInt32(Session["Branchid"]), ddlUsertype.SelectedValue, DeptCodeID);
            BindGrid();
            GV_userrollright.Caption = "Record Deleted successfully.!";
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
    protected void chkAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAll.Checked == true)
        {
            foreach (ListItem li in chksubdept.Items)
            {
                li.Selected = true;
            }
        }
        else
        {
            foreach (ListItem li in chksubdept.Items)
            {
                li.Selected = false;
            }
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