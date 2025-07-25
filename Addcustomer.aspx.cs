using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;

public partial class Addcustomer : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();   
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("Addcustomer.aspx");
                    }
                }
                if (Session["usertype"] != null)
                {
                    if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                    {

                        FillCenter();
                        btnshow.Visible = false;
                        createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        ddlCenter.SelectedValue = ui.CenterCode;

                        ddlCenter.Visible = false;
                        Label2.Visible = false;

                    }
                    if (Session["usertype"].ToString().Trim() == "Administrator")
                    {
                        FillCenter();
                    }
                }
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
    }
  
    protected void CustomerGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsort.Value = e.SortExpression;
        BindGrid();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    private void FillCenter()
    {
        try
        {
            ddlCenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            ddlCenter.DataTextField = "Name";
            ddlCenter.DataValueField = "Code";
            ddlCenter.DataBind();
            ddlCenter.Items.Insert(0, "For All " + Session["CenterName"].ToString());
            ddlCenter.SelectedIndex = -1;
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
    void BindGrid()
    {
        try
        {
            if (ddlCenter.SelectedIndex != 0)
            {

                CustomerGrid.DataSource = DrMT_sign_Bal_C.getPSCCustomers(Convert.ToString(ddlCenter.SelectedValue.Trim()), hdnsort.Value, Convert.ToInt32(Session["Branchid"]));
                CustomerGrid.DataBind();

            }
            else
            {
                if (hdnsort.Value != null)
                {
                    CustomerGrid.DataSource = DrMT_sign_Bal_C.getAllCustomers(hdnsort.Value, Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
                }
                else
                {
                    CustomerGrid.DataSource = DrMT_sign_Bal_C.getAllCustomers("", Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
                }
            }
            CustomerGrid.DataBind();
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
    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void CustomerGrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CustomerEntry.aspx");
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