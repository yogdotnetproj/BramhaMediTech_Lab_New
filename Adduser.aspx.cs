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
using System.Data.SqlClient;
using System.Configuration;
public partial class Adduser : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();  
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (Convert.ToString(Session["HMS"]) != "Yes")
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("Adduser.aspx");
                }
            }

            ViewState["labcode"] = "";
            FillGrid();

        }
    }

 
    public void FillGrid()
    {
        ViewState["labcode"] = "";
        string UserName = "";
        if (txtusername.Text != "")
        {
            UserName = txtusername.Text;
        }
        userlist.DataSource = dc.FillUserMasterGrid(Session["Branchid"].ToString(), UserName, "", "2");
        userlist.DataBind();
       
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/useradd.aspx");
    }
    protected void ItemList_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("~/useradd.aspx?id=" + (userlist.SelectedRow.FindControl("hdnuid") as HiddenField).Value);
    }
    protected void cmdsearch_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            FillGrid();
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

    protected void userlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        ViewState["labcode"] = "";

        userlist.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void userlist_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex == 1)
            return;
        DataAccess dt = new DataAccess();
        for (int i = 0; i < userlist.Rows.Count; i++)
        {
            string username = userlist.Rows[i].Cells[1].Text.Trim();
            SqlConnection conn = dt.ConInitForDC1();
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT  dbo.DrMT.DoctorName FROM dbo.CTuser INNER JOIN dbo.DrMT ON dbo.CTuser.Unitcode = dbo.DrMT.DoctorCode where CTuser.username='" + username + "'", conn);
            sda.Fill(dt1);
            if (dt1.Rows.Count != 0)
            {
              
            }
            if (ViewState["labcode"].ToString() != "")
            {
                if (ViewState["labcode"].ToString() != (userlist.Rows[i].Cells[8].FindControl("Labname") as Label).Text)
                {
                    userlist.Rows[i].Visible = false;

                }
                else
                {
                    userlist.Rows[i].Visible = true;

                }
            }

        }
    }
    protected void userlist_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Userright_Bal_C ObjUBC=new Userright_Bal_C ();
        string Id = userlist.DataKeys[e.RowIndex].Value.ToString();

        ObjUBC.deleteUsers(Id);

        FillGrid();
       
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/useradd.aspx");
    }
    protected void btnList_Click(object sender, EventArgs e)
    {
        try
        {
            FillGrid();
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