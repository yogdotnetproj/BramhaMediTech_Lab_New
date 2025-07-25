using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class UserMoneyRequestApproval : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    Expence_Bal_C ObjEBC = new Expence_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("UserMoneyRequestApproval.aspx");
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
            float ExpAmt = Convert.ToSingle(GV_ExpenceEntry.Rows[e.RowIndex].Cells[2].Text);
            var ID = (TextBox)GV_ExpenceEntry.Rows[e.RowIndex].FindControl("txtApproveAmt");
          string aa=  ID.Text;
          if (ID.Text != "")
          {
              if (Convert.ToSingle(ID.Text) > ExpAmt)
              {
                  LblMsg.Text = " Approval Amt not greater than Req Amt.";
              }
              else
              {
                  ObjEBC.ID = Expid;
                  ObjEBC.ExpenceDate = Convert.ToDateTime(Date.getdate().ToString("dd/MM/yyyy"));
                  ObjEBC.ExpenceAmount = Convert.ToSingle(ID.Text);
                  ObjEBC.Branchid = Convert.ToInt32(Session["Branchid"]);

                    ObjEBC.Update_UserCashExchangeRequest();
                  LblMsg.Text = "Record Approval successfully.";
              }

             
          }
            //if (Session["usertype"].ToString().Trim() == "Administrator")
            //{
            //    ObjEBC.delete_ExpenceEntry(Expid);
            //    LblMsg.Text = "Record Approval successfully.";
            //}
            //else
            //{
            //    LblMsg.Text = "u can't Approval  entry.";
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
    public  void  BindGrid()
    {
        dt = new DataTable();
        dt = ObjEBC.Bind_UserCashExchangeApproval(Convert.ToString(Session["username"]));
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
        string ApproveAmt = (e.Row.FindControl("txtApproveAmt") as TextBox).Text;
        if (ApproveAmt != "0")
        {
            (e.Row.FindControl("txtApproveAmt") as TextBox).Enabled = false;
            e.Row.Cells[7].Enabled = false;
        }
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