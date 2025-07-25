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
public partial class AddUsertype : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Userright_Bal_C ObjAT = new Userright_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.SetFocus(txtuserType);
        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("AddUsertype.aspx");
                    }
                }
                //LUNAME.Text = Convert.ToString(Session["username"]);
                //LblDCName.Text = Convert.ToString(Session["Bannername"]);
                //LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                //dt = new DataTable();
                //dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                //this.PopulateTreeView(dt, 0, null);
                Label2.Visible = false;
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
  
    private void bindgrid()
    {
        try
        {
            dt = ObjAT.getuserType();
            GV_UserType.DataSource = dt;
            GV_UserType.DataBind();
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtuserType.Text = "";
        Label2.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ViewState["Editflag"]) == 1)
            {
                ObjAT.Update(Convert.ToInt32(ViewState["rid"]), txtuserType.Text, Convert.ToInt32(Session["Branchid"]));
                Label2.Visible = true;
                Label2.Text = "Record Updated successfully.";
                bindgrid();
                ViewState["Editflag"] = null;

            }
            else
            {
                if (Userright_Bal_C.isUsertypepeeExists(txtuserType.Text, Convert.ToInt32(Session["Branchid"])))
                {
                    Label2.Visible = true;
                    Label2.Text = "user already exist.";
                    txtuserType.Focus();
                    return;
                }
                else
                {
                    Label2.Visible = false;
                }

                ObjAT.Usertype = txtuserType.Text;
                ObjAT.P_username = Convert.ToString(Session["username"]);
                ObjAT.Insert(Convert.ToInt32(Session["Branchid"]));
                Label2.Visible = true;
                Label2.Text = "Record save successfully.";
                bindgrid();

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
    protected void GV_UserType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ViewState["rid"] = GV_UserType.DataKeys[e.NewEditIndex].Value;
            Userright_Bal_C samp = new Userright_Bal_C(Convert.ToInt32(ViewState["rid"]), Convert.ToInt32(Session["Branchid"]));
            txtuserType.Text = samp.Usertype;
            ViewState["Editflag"] = 1;

            Label2.Visible = false;
            e.Cancel = true;
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
    protected void GV_UserType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GV_UserType.PageIndex = e.NewPageIndex;
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
    protected void GV_UserType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int reaid = Convert.ToInt32(GV_UserType.DataKeys[e.RowIndex].Value);

            ObjAT.delete(reaid, Convert.ToInt32(Session["Branchid"]));
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