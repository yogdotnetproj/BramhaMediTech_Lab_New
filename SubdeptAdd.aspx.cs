using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using BAL;
using System.Data.SqlClient;

using System.Configuration;
public partial class SubdeptAdd : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    main_Bal_C main = new main_Bal_C();
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.SetFocus(txtsubdeptName);
        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("SubdeptAdd.aspx");
                    }
                }
                show.Visible = false;
                List.Visible = true;

                ddlmaindept.DataSource = main.fillddlmaindept();
                ddlmaindept.DataTextField = "deptname";
                ddlmaindept.DataValueField = "deptid";
                ddlmaindept.DataBind();
                ddlmaindept.Items.Insert(0, new ListItem("Select Main Dept", "0"));
                ddlmaindept.SelectedIndex = -1;
                if (Session["DigModule"] != null)
                {
                    if (Session["DigModule"].ToString() != "0")
                    {
                        ddlmaindept.SelectedValue = Convert.ToString(Session["DigModule"]);
                    }
                    else
                    {

                    }
                    // ddlmaindept.Enabled = false;
                }

                bindgrid();
                Label2.Visible = false;
                ViewState["DeptName"] = "";
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
        int num1 = SubdepartmentLogic_Bal_C.MaxHeadOrder(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]));
        int ono = num1 + 1;
        txtSDOrderNo.Text = ono.ToString();
        txtSDOrderNo.ReadOnly = true;
    }
  
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSDCode.Value.Trim() == "")
            {

                Label2.Visible = true;
                Label2.Text = "Please enter code";
                return;
            }
            if (txtsubdeptName.Text.Trim() == "")
            {


                Label2.Visible = true;
                Label2.Text = "Please enter sub dept name ";
                return;
            }
            else
            {
                Label2.Visible = false;
                Label2.Text = "";
            }
            if (Convert.ToInt32(ViewState["Editflag"]) == 1)
            {
                Subdepartment_Bal_C Obj_SB_C = new Subdepartment_Bal_C();
                Obj_SB_C.SubdeptName = txtsubdeptName.Text;
                Obj_SB_C.sDOrderNo = Convert.ToInt32(txtSDOrderNo.Text);
                Obj_SB_C.Remark = txtDescription.Text;
                Obj_SB_C.SDCode = txtSDCode.Value;
                Obj_SB_C.P_DigModule = Convert.ToInt32(ddlmaindept.SelectedValue);

                SubdepartmentLogic_Bal_C.Update_Packageno(Obj_SB_C, Convert.ToInt32(ViewState["Testordno"]), Convert.ToInt32(Session["Branchid"]));


                Obj_SB_C.update(ViewState["PrevSDCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                Label2.Visible = true;
                Label2.Text = "Record Updated successfully";
                bindgrid();
                clearcontrols();
                ViewState["Editflag"] = 0;

            }
            else
            {
                if (SubdepartmentLogic_Bal_C.isSDCodeExists(txtSDCode.Value.Trim(), Convert.ToInt32(Session["Branchid"])))
                {
                    Label2.Visible = true;
                    Label2.Text = "sub dept Code already exist.";
                    txtSDCode.Focus();
                    return;
                }
                else
                {
                    Label2.Visible = false;
                }

                Subdepartment_Bal_C Obj_SB_C = new Subdepartment_Bal_C();
                Obj_SB_C.SubdeptName = txtsubdeptName.Text;
                Obj_SB_C.sDOrderNo = Convert.ToInt32(txtSDOrderNo.Text);
                Obj_SB_C.Remark = txtDescription.Text;
                Obj_SB_C.SDCode = txtSDCode.Value;
                Obj_SB_C.P_ID= Convert.ToInt32(ddlmaindept.SelectedValue);
                Obj_SB_C.P_DigModule = Convert.ToInt32(1);

                Obj_SB_C.P_username = Convert.ToString(Session["username"]);

                Obj_SB_C.Insert(Convert.ToInt32(Session["Branchid"]));
                Label2.Visible = true;
                Label2.Text = "Record Saved successfully";
                bindgrid();
                clearcontrols();
            }
            show.Visible = false;
            List.Visible = true;

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

    private void bindgrid()
    {
        try
        {
            GVSubdept.DataSource = SubdepartmentLogic_Bal_C.getSubDepartment(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]));
            GVSubdept.DataBind();
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

    protected void GVSubdept_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string sd = GVSubdept.Rows[e.NewEditIndex].Cells[1].Text;
            Subdepartment_Bal_C Obj_SB_C = new Subdepartment_Bal_C(sd, Convert.ToInt32(Session["Branchid"]));
            ViewState["PrevSDCode"] = sd;

            txtSDCode.Value = sd;
            if (sd == "PD")
                txtSDCode.Disabled = true;
            else
                txtSDCode.Disabled = false;
            txtsubdeptName.Text = Obj_SB_C.SubdeptName;
            ViewState["Testordno"] = Convert.ToInt32(Obj_SB_C.sDOrderNo);
            ViewState["DeptName"] = Obj_SB_C.SubdeptName;
            txtSDOrderNo.Text = Convert.ToString(Obj_SB_C.sDOrderNo);
            txtSDOrderNo.ReadOnly = true;
            ddlmaindept.SelectedValue = Obj_SB_C.P_DigModule.ToString();
            txtDescription.Text = Obj_SB_C.Remark;
            ViewState["Editflag"] = 1;

            List.Visible = false;
            show.Visible = true;
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

    private void clearcontrols()
    {
        txtsubdeptName.Text = "";
        txtDescription.Text = "";
        txtSDCode.Value = "";
        txtSDOrderNo.Text = "";

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            int num1 = SubdepartmentLogic_Bal_C.MaxHeadOrder(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]));
            int ono = num1 + 1;
            clearcontrols();
            txtSDOrderNo.Text = ono.ToString();
            txtSDOrderNo.ReadOnly = true;
            Label2.Visible = false;

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

    protected void GVSubdept_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string SDCode1 = GVSubdept.Rows[e.RowIndex].Cells[1].Text;
            Subdepartment_Bal_C hn1 = new Subdepartment_Bal_C();
            hn1.SDCode = SDCode1.Trim();
            hn1.Delete(Convert.ToInt32(Session["Branchid"]));
            hn1 = null;
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



    protected void GVSubdept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVSubdept.PageIndex = e.NewPageIndex;
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

    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        show.Visible = true;
        List.Visible = false;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        show.Visible = false;
        List.Visible = true;
    }

    protected void Btn_Add_Dept_Click(object sender, EventArgs e)
    {
        DAdd.Visible = true;
        DAdd1.Visible = true;
    }
    protected void Btn_Add_Test_Click(object sender, EventArgs e)
    {

        //  Response.Redirect("ShowTest.aspx", false);
        Response.Redirect("~/AddTest.aspx", false);
    }
    protected void Btn_Add_NR_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReferanceRange.aspx", false);
    }
    protected void Btn_Add_PK_Click(object sender, EventArgs e)
    {
        Response.Redirect("Showpackage.aspx", false);
    }
    protected void Btn_Add_Sample_Click(object sender, EventArgs e)
    {
        Response.Redirect("SampleType.aspx", false);

    }
    protected void Btn_Add_ShortCut_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShortCut.aspx", false);
    }
    protected void Btn_Add_Formula_Click(object sender, EventArgs e)
    {
        Response.Redirect("TestFormulasetting.aspx", false);
    }
    protected void Btn_Add_RN_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportNote.aspx", false);
    }
    protected void btnedittest_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowTest.aspx", false);
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