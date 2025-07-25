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
public partial class Ratetypemaster : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    ratetype_Bal_C ramp = new ratetype_Bal_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("Ratetypemaster.aspx");
                    }
                }
                Label2.Visible = false;
                RbtnColl.Items[0].Selected = true;
                ramp.RateFlag = 'C';
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
            if (RbtnColl.Items[0].Selected)
            {
                ramp.RateFlag = 'C';

            }
            if (RbtnColl1.Items[0].Selected)
            {
                ramp.RateFlag = 'R';

            }

            RateTypeGrid.DataSource = ratetypeLogic_Bal_C.getRateType(Convert.ToInt32(Session["Branchid"]), Convert.ToChar(ramp.RateFlag));
            RateTypeGrid.DataBind();
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

        txtRateType.Text = "";
        Label2.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ViewState["Editflag"]) == 1)
            {
                ratetype_Bal_C ramp = new ratetype_Bal_C();
                ramp.P_ratecate = rblretecate.SelectedValue;
                ramp.Update(ViewState["rid"].ToString(), txtRateType.Text, Convert.ToInt32(Session["Branchid"]));
                Label2.Visible = true;
                Label2.Text = "RateType Updated successfully.";
                bindgrid();
                ViewState["Editflag"] = null;

            }
            else
            {
                ramp.RateName = txtRateType.Text;
                string rmaster = ramp.RateName;

                if (RbtnColl.Items[0].Selected)
                {
                    ramp.RateFlag = 'C';

                }
                if (RbtnColl1.Items[0].Selected)
                {
                    ramp.RateFlag = 'R';

                }

                ramp.P_ratecate = rblretecate.SelectedValue;
                string xtypeflag = Convert.ToString(ramp.RateFlag);
                if (ratetype_Bal_C.getcode(rmaster, Convert.ToInt32(Session["Branchid"]), xtypeflag))
                {
                    Label2.Visible = true;
                    Label2.Text = "Rate Type Name Already Exist";
                    bindgrid();
                }
                else
                {
                    ramp.P_username = Convert.ToString(Session["username"]);
                    ramp.Insert(Convert.ToInt32(Session["Branchid"]));
                    Label2.Visible = true;
                    Label2.Text = "RateType Saved successfully.";
                    bindgrid();
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

    protected void RateTypeGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        RateTypeGrid.PageIndex = e.NewPageIndex;
        bindgrid();
    }

    protected void RateTypeGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            ViewState["rid"] = RateTypeGrid.DataKeys[e.NewEditIndex]["RatID"].ToString();
            ratetype_Bal_C rate = new ratetype_Bal_C(ViewState["rid"].ToString(), Convert.ToInt32(Session["Branchid"]));

            txtRateType.Text = rate.RateName;
            ViewState["Editflag"] = 1;
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


    protected void RbtnColl_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtRateType.Text = "";
            RbtnColl1.Items[0].Selected = false;
            if (RbtnColl.Items[0].Selected)
            {
                ramp.RateFlag = 'C';

            }
            else
            {
                ramp.RateFlag = 'C';

            }
            RateTypeGrid.DataSource = ratetypeLogic_Bal_C.getRateType(Convert.ToInt32(Session["Branchid"]), Convert.ToChar(ramp.RateFlag));
            RateTypeGrid.DataBind();
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

    protected void RbtnColl1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            txtRateType.Text = "";
            RbtnColl.Items[0].Selected = false;
            if (RbtnColl.Items[0].Selected)
            {
                ramp.RateFlag = 'R';

            }
            else
            {
                ramp.RateFlag = 'R';

            }
            RateTypeGrid.DataSource = ratetypeLogic_Bal_C.getRateType(Convert.ToInt32(Session["Branchid"]), Convert.ToChar(ramp.RateFlag));
            RateTypeGrid.DataBind();
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

    protected void RateTypeGrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void RateTypeGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string reaid = Convert.ToInt32(RateTypeGrid.DataKeys[e.RowIndex].Value.ToString()).ToString();
        ratetype_Bal_C rate1 = new ratetype_Bal_C();
        rate1.delete(reaid, Convert.ToInt32(Session["Branchid"]));

        Label2.Visible = true;
        Label2.Text = "RateType Deleted successfully.";
        txtRateType.Text = "";
        bindgrid();
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