using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class UserMoneyRequest : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    Expence_Bal_C ObjEBC = new Expence_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Convert.ToString(Session["usertype"]) != "Administrator")
            {
                checkexistpageright("UserMoneyRequest.aspx");
            }
            try
            {
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                txtparticular.Text = Convert.ToString(Session["username"]);
                Fill_UserName();
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
    public void Fill_UserName()
    {
        ObjEBC.UserName = Convert.ToString(Session["username"]);
        ddlRequestTo.DataSource = ObjEBC.Get_UserName(Session["Branchid"].ToString());
        ddlRequestTo.DataValueField = "CUId";
        ddlRequestTo.DataTextField = "username";
        ddlRequestTo.DataBind();
        ddlRequestTo.Items.Insert(0, "select");
        ddlRequestTo.Items[0].Value = "";
        ddlRequestTo.SelectedIndex = 0;


    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        ObjEBC.ExpenceDate = Convert.ToDateTime(fromdate.Text);
        ObjEBC.RequestFrom = Convert.ToString(Session["username"]);
        ObjEBC.RequestTo = Convert.ToString(ddlRequestTo.SelectedItem.Text);
        ObjEBC.ExpenceAmount = Convert.ToSingle(txtexpenceamount.Text);
        ObjEBC.ExpenceDetails = Convert.ToString(txtparticularDet.Text);
       
        
        ObjEBC.Branchid = Convert.ToInt32(Session["Branchid"]);
       
        ObjEBC.Insert_UserCashExchangeRequest();

        LblMsg.Text = "Record save successfully.";
        Clear();
        BindGrid();
    }
    public void Clear()
    {
        txtexpenceamount.Text = "0";
        txtparticular.Text = "";
        txtparticularDet.Text = "";
    }

    protected void GV_ExpenceEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_ExpenceEntry.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void GV_ExpenceEntry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int Expid = Convert.ToInt32(GV_ExpenceEntry.DataKeys[e.RowIndex].Value);
            int MStatus = Convert.ToInt32(GV_ExpenceEntry.Rows[e.RowIndex].Cells[6].Text);
            if (MStatus == 0)
            {
                ObjEBC.delete_UserCashExchangeRequest(Expid);
                LblMsg.Text = "Record deleted successfully.";
            }
            else
            {
                LblMsg.Text = "u can't deleted  entry.";
            }
            //if (Session["usertype"].ToString().Trim() == "Administrator")
            //{
            //    ObjEBC.delete_ExpenceEntry(Expid);
            //    LblMsg.Text = "Record deleted successfully.";
            //}
            //else
            //{
            //    LblMsg.Text = "u can't deleted  entry.";
            //}
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
        dt = new DataTable();
        dt = ObjEBC.Bind_UserCashExchangeRequest(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            GV_ExpenceEntry.DataSource = dt;
            GV_ExpenceEntry.DataBind();

        }
    }
    protected void GV_ExpenceEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
        bool Outsource = Convert.ToBoolean(e.Row.Cells[5].Text);
                if (Outsource == true)
                {        
            e.Row.Cells[5].Text = "<span class='btn btn-xs btn-success' >send</span>";

        }
        else
        {
            e.Row.Cells[5].Text = "<span class='btn btn-xs btn-danger' >No</span>";
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