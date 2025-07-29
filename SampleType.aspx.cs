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
public partial class SampleType :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.SetFocus(txtSampleType);

        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                   // checkexistpageright("SampleType.aspx");
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
            SampleGrid.DataSource = SampleType_Bal_C.getSampleType(Convert.ToInt32(Session["Branchid"]));
            SampleGrid.DataBind();
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
        txtSampleType.Text = "";
        Label2.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ViewState["Editflag"]) == 1)
            {
                SampleType_Bal_C sam = new SampleType_Bal_C();
                sam.Update(Convert.ToInt32(ViewState["rid"]), txtSampleType.Text, Convert.ToInt32(Session["Branchid"]));
                Label2.Visible = true;
                Label2.Text = "record Updated successfully.";
                bindgrid();
                ViewState["Editflag"] = null;

            }
            else
            {
                if (SampleType_Bal_C.isSampletypeeExists(txtSampleType.Text, Convert.ToInt32(Session["Branchid"])))
                {
                    Label2.Visible = true;
                    Label2.Text = "sample Type already exist.";
                    txtSampleType.Focus();
                    return;
                }
                else
                {
                    Label2.Visible = false;
                }
                SampleType_Bal_C sam = new SampleType_Bal_C();
                sam.Sampletype = txtSampleType.Text;
                sam.P_username = Convert.ToString(Session["username"]);
                sam.Insert(Convert.ToInt32(Session["Branchid"]));
                Label2.Visible = true;
                Label2.Text = "Record Saved successfully.";
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
    protected void SampleGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ViewState["rid"] = SampleGrid.DataKeys[e.NewEditIndex].Value;
            SampleType_Bal_C samp = new SampleType_Bal_C(Convert.ToInt32(ViewState["rid"]), Convert.ToInt32(Session["Branchid"]));
            txtSampleType.Text = samp.Sampletype;
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
    protected void SampleGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
             SampleGrid.PageIndex = e.NewPageIndex;
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
    protected void SampleGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int reaid = Convert.ToInt32(SampleGrid.DataKeys[e.RowIndex].Value);
            SampleType_Bal_C sampl = new SampleType_Bal_C();
            sampl.delete(reaid, Convert.ToInt32(Session["Branchid"]));
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvr in SampleGrid.Rows)
            {
                CheckBox chk = gvr.FindControl("chk") as CheckBox;
                if (chk.Checked)
                {
                    int reaid = Convert.ToInt32(SampleGrid.DataKeys[gvr.RowIndex].Value);
                    SampleType_Bal_C sampl = new SampleType_Bal_C();
                    sampl.delete(reaid, Convert.ToInt32(Session["Branchid"]));
                }
            }
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
    protected void Btn_Add_Dept_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubDeptAdd.aspx", false);
    }
    protected void Btn_Add_Test_Click(object sender, EventArgs e)
    {

        //Response.Redirect("ShowTest.aspx", false);
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