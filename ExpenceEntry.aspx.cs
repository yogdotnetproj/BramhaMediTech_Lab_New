 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class ExpenceEntry :BasePage
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
                    checkexistpageright("ExpenceEntry.aspx");
                }
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                if (Request.QueryString["ExpID"] != null)
                {
                    string ExpID = Request.QueryString["ExpID"].ToString();
                     dt = new DataTable();
                     dt = ObjEBC.Bind_ExpenceEntry_ID(Convert.ToString(Session["username"]), ExpID);
                     if (dt.Rows.Count > 0)
                     {
                         fromdate.Text = Convert.ToString(dt.Rows[0]["ExpenceDate"]);
                         txtparticular.Text = Convert.ToString(dt.Rows[0]["Particular"]);
                         txtexpenceamount.Text = Convert.ToString(dt.Rows[0]["ExpenceAmount"]);
                         txtparticularDet.Text = Convert.ToString(dt.Rows[0]["ExpenceDetails"]);
                         btnsave.Visible = true;
                     }
                     else
                     {
                         LblMsg.Text = "U are not authorised user..";
                         btnsave.Visible = false;
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
 
    protected void btnsave_Click(object sender, EventArgs e)
    {

        ObjEBC.Particular = Convert.ToString( txtparticular.Text);
        ObjEBC.ExpenceDetails = Convert.ToString(txtparticularDet.Text);
        ObjEBC.ExpenceAmount = Convert.ToSingle(txtexpenceamount.Text);
        ObjEBC.ExpenceDate = Convert.ToDateTime(fromdate.Text);
        ObjEBC.Branchid = Convert.ToInt32(Session["Branchid"]);
        ObjEBC.UserName = Convert.ToString(Session["username"]);
        if (Convert.ToString(Request.QueryString["ExpID"]) != null)
        {
            ObjEBC.ID = Convert.ToInt32(Request.QueryString["ExpID"]);
            ObjEBC.Update_ExpenceCode();
        }
        else
        {
            ObjEBC.Insert_ExpenceCode();
        }

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
            if (Session["usertype"].ToString().Trim() == "Administrator")
            {
                ObjEBC.delete_ExpenceEntry(Expid);
                LblMsg.Text = "Record deleted successfully.";
            }
            else
            {
                LblMsg.Text = "u can't deleted expence entry.";
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
    public void BindGrid()
    {
        dt = new DataTable();
        dt = ObjEBC.Bind_ExpenceEntry(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            GV_ExpenceEntry.DataSource = dt;
            GV_ExpenceEntry.DataBind();
            float sum = 0.0f, Charges = 0;
            for (int i = 0; i < GV_ExpenceEntry.Rows.Count; i++)
            {
                string txt_17 = (GV_ExpenceEntry.Rows[i].Cells[2].Text);
                if (txt_17 != "")
                {
                    Charges = Charges + Convert.ToSingle(txt_17);
                    a.Visible = true;
                }
            }
            lblcharges.Text = Convert.ToString(Charges);
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