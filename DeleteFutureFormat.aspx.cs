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
using System.IO;

public partial class DeleteFutureFormat :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        //Page.SetFocus(txtMachinCode);

        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("DeleteFutureFormat.aspx");
                }    
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

            dt = ObjTB.GetAllFutureFormat(Convert.ToInt32( Session["DRid"]));
            RoutinTest_Grid.DataSource = dt;
            RoutinTest_Grid.DataBind();
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
  
    protected void RoutinTest_Grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ViewState["rid"] = RoutinTest_Grid.DataKeys[e.NewEditIndex].Value;
            RoutinTest_C RTC = new RoutinTest_C();
            RTC.GetMAchinData(Convert.ToInt32(ViewState["rid"]), Convert.ToInt32(Session["Branchid"]));
           // txtMachinCode.Text = RTC.P_Instumentcode;
           // txtMachinename.Text = RTC.P_Instumentname;
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
    protected void RoutinTest_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            RoutinTest_Grid.PageIndex = e.NewPageIndex;
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
    protected void RoutinTest_Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int reaid = Convert.ToInt32(RoutinTest_Grid.DataKeys[e.RowIndex].Value);
            RoutinTest_C RTC = new RoutinTest_C();
            RTC.DeleteFutureFormat(reaid, Convert.ToInt32(Session["Branchid"]), Convert.ToString( Session["username"]));
            bindgrid();
            Label2.Visible = true;
            Label2.Text = "Record Deleted successfully.";
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvr in RoutinTest_Grid.Rows)
            {
                CheckBox chk = gvr.FindControl("chk") as CheckBox;
                if (chk.Checked)
                {
                    int reaid = Convert.ToInt32(RoutinTest_Grid.DataKeys[gvr.RowIndex].Value);
                    RoutinTest_C RTC = new RoutinTest_C();
                    //RTC.delete_Machine(reaid, Convert.ToInt32(Session["Branchid"]));
                    RTC.DeleteFutureFormat(reaid, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"]));
                }
            }
            bindgrid();
            Label2.Visible = true;
            Label2.Text = "Record Deleted successfully.";
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