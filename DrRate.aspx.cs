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

public partial class DrRate : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DrMT_sign_Bal_C obj = new DrMT_sign_Bal_C();
    sharemst_Bal_C complChr = new sharemst_Bal_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.SetFocus(ddlRate);

        if (!Page.IsPostBack)
        {
            if (Convert.ToString(Session["HMS"]) != "Yes")
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("DrRate.aspx");
                }
            }
            try
            {
                obj.flag_type = 'R';
                ddlRate.DataSource = DrMT_sign_Bal_C.getSelectrateCompliment(Convert.ToInt32(Session["Branchid"]), obj.flag_type);
                ddlRate.DataTextField = "RateName";
                ddlRate.DataValueField = "RatID";
                ddlRate.DataBind();
                ddlRate.Items.Insert(0, "Select Rate Type");
                ddlRate.SelectedIndex = -1;

                DdlTestname.DataSource = SubdepartmentLogic_Bal_C.getSubDepartment(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]));
                DdlTestname.DataTextField = "subdeptName";
                DdlTestname.DataValueField = "SDCode";
                DdlTestname.DataBind();
                DdlTestname.Items.Insert(0, "Select Department");
                DdlTestname.SelectedIndex = -1;
                btnsave.Visible = false;
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

  
    protected void Btndsplay_Click(object sender, EventArgs e)
    {
        try
        {
            if (DdlTestname.SelectedValue != "Select Department")
            {
                int selrate = Convert.ToInt32(ddlRate.SelectedValue);
                string SDCode = DdlTestname.SelectedValue;
                GridRate.DataSource = MainTestLog_Bal_C.getGridFill(SDCode, Convert.ToString(selrate), Convert.ToInt32(Session["Branchid"]), ddlRate.SelectedItem.Text);
                GridRate.DataBind();
                btnsave.Visible = true;
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

    protected void btnSave_Click(object sender, EventArgs e)
    {

        complChr.USERName = Session["username"].ToString();
        if (DdlTestname.SelectedItem != null && ddlRate.SelectedItem != null)
        {
            for (int i = 0; i < GridRate.Rows.Count; i++)
            {
                string STCODE = GridRate.Rows[i].Cells[0].Text;
                try
                {
                    sharemst_Bal_C SBC = new sharemst_Bal_C(STCODE, ddlRate.SelectedValue, Convert.ToInt32(Session["Branchid"]));

                    SBC.STCODE = STCODE;
                    SBC.TestName = GridRate.Rows[i].Cells[1].Text;
                    SBC.RateCode = ddlRate.SelectedValue;
                    SBC.RateName = ddlRate.SelectedItem.Text;
                    int amt = Convert.ToInt32((GridRate.Rows[i].Cells[2].FindControl("txtamount") as TextBox).Text);
                    SBC.Amount = amt;
                    float perc = Convert.ToSingle((GridRate.Rows[i].Cells[3].FindControl("txtPerc") as TextBox).Text);
                    SBC.Percentage = perc;
                    // int Emer = Convert.ToInt32((GridRate.Rows[i].Cells[4].FindControl("txtEmerg") as TextBox).Text);
                    SBC.Emergency = 0;
                    SBC.Patregdate = Date.getOnlydate();
                    try
                    {
                        SBC.USERName = Session["username"].ToString();
                        SBC.Update(Convert.ToInt32(Session["Branchid"]));
                        lblmsg.Text = "Record updated successfully";

                    }
                    catch
                    {
                    }
                }
                catch
                {
                    sharemst_Bal_C SBC = new sharemst_Bal_C();

                    SBC.USERName = Session["username"].ToString();
                    SBC.STCODE = STCODE;
                    SBC.TestName = GridRate.Rows[i].Cells[1].Text;
                    SBC.RateCode = ddlRate.SelectedValue;
                    SBC.RateName = ddlRate.SelectedItem.Text;
                    int Emer = 0;
                    int amt = Convert.ToInt32((GridRate.Rows[i].Cells[2].FindControl("txtamount") as TextBox).Text);
                    SBC.Amount = amt;
                    float perc = Convert.ToSingle((GridRate.Rows[i].Cells[3].FindControl("txtPerc") as TextBox).Text);
                    SBC.Percentage = perc;

                    SBC.Emergency = Emer;
                    try
                    {
                        SBC.USERName = Session["username"].ToString();
                        SBC.Insert(Convert.ToInt32(Session["Branchid"]));
                        lblmsg.Text = "Record save successfully";
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
        }
    }

    protected void GridRate_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridRate.PageIndex = e.NewPageIndex;
            int selrate = Convert.ToInt32(ddlRate.SelectedValue);
            string SDCode = DdlTestname.SelectedValue;
            GridRate.DataSource = MainTestLog_Bal_C.getGridFill(SDCode, Convert.ToString(selrate), Convert.ToInt32(Session["Branchid"]), ddlRate.SelectedItem.Text);
            GridRate.DataBind();
            btnsave.Visible = true;
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

    protected void GridRate_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            sharemst_Bal_C SBC = null;
            if (DdlTestname.SelectedItem.Text != null && ddlRate.SelectedItem.Text != null)
            {
                SBC = new sharemst_Bal_C();
                SBC.STCODE = GridRate.Rows[e.RowIndex].Cells[0].Text;
                SBC.RateCode = ddlRate.SelectedValue;
                SBC.Delete(Convert.ToInt32(Session["Branchid"]));
                int selrate = Convert.ToInt32(ddlRate.SelectedValue);
                string SDCode = DdlTestname.SelectedValue;
                GridRate.DataSource = MainTestLog_Bal_C.getGridFill(SDCode, Convert.ToString(selrate), Convert.ToInt32(Session["Branchid"]), ddlRate.SelectedItem.Text);
                GridRate.DataBind();
                btnsave.Visible = true;
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