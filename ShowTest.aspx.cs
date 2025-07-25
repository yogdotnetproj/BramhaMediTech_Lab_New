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

public partial class ShowTest : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    Hashtable ht = new Hashtable();
    Hashtable ht1 = new Hashtable();
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.SetFocus(ddlsubdept);
        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                   // checkexistpageright("ShowTest.aspx");
                }
               
                ViewState["hashtable"] = "";

                ViewState["pageindexchangedfirst"] = "true";
                ViewState["maxordno"] = 0;
                ddlsubdept.DataSource = SubdepartmentLogic_Bal_C.getSubDepartment(Convert.ToInt32(Session["Branchid"]), 0, Convert.ToInt32(Session["DigModule"]));
                ddlsubdept.DataTextField = "subdeptName";
                ddlsubdept.DataValueField = "SDCode";
                ddlsubdept.DataBind();
                ddlsubdept.Items.Insert(0, "Select Department");
                ddlsubdept.SelectedIndex = -1;
                btnseq.Visible = false;
                if (Request.QueryString["SDCode"] != null)
                {
                    ddlsubdept.SelectedValue = Request.QueryString["SDCode"];
                    btnList_Click(this, null);
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
    }


  
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (ddlsubdept.SelectedIndex != 0)
        {

            Response.Redirect("~/AddTest.aspx?SDCode=" + ddlsubdept.SelectedValue);
        }
        else
        {
            Response.Redirect("~/AddTest.aspx");
        }

    }
    protected void GVMainTest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex == -1)
                return;
            if (e.Row.Cells[7].Text.Trim() == "TextField")
            {
                e.Row.Cells[7].Text = "Non Descriptive".ToString();
            }
            else if (e.Row.Cells[7].Text.Trim() == "DescType")
            {
                e.Row.Cells[7].Text = "Descriptive".ToString();
            }
            else
            {
                e.Row.Cells[7].Text = "";
            }

            if (e.Row.Cells[5].Text.Trim() == "Format")
            {
                e.Row.Cells[5].Text = "Group";
            }

            if (e.Row.Cells[5].Text.Trim() == "Group")
            {
                string tlc1 = e.Row.Cells[4].Text;


                (e.Row.FindControl("lnkshowparam") as HyperLink).Text = "Display Parameters";
                (e.Row.FindControl("lnkshowparam") as HyperLink).NavigateUrl = "AddTestParameter.aspx?Code=" + tlc1 + "&SDCode='" + ddlsubdept.SelectedValue + "'";
                ViewState["Testcode"] = tlc1;
                ViewState["SDCode"] = ddlsubdept.SelectedValue;
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
    protected void GVMainTest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVMainTest.PageIndex = e.NewPageIndex;
            GVMainTest.DataSource = (ArrayList)MainTestLog_Bal_C.GetMaintest_SDCode(ddlsubdept.SelectedValue, Convert.ToInt32(Session["Branchid"]));
            GVMainTest.DataBind();

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
    protected void btnList_Click(object sender, EventArgs e)
    {
        try
        {
            txttestname.Text = "";
            ViewState["maxordno"] = 0;
            GVMainTest.DataSource = (ArrayList)MainTestLog_Bal_C.GetMaintest_SDCode(ddlsubdept.SelectedValue, Convert.ToInt32(Session["Branchid"]));
            GVMainTest.DataBind();
            btnseq.Visible = true;
            btnseq.Enabled = true;


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
    protected void GVMainTest_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string tlc = "";
        try
        {
            tlc = GVMainTest.DataKeys[e.NewEditIndex].Value.ToString().Trim();
            Session["subdeptName"] = ddlsubdept.SelectedValue;
            Response.Redirect("AddTest.aspx?Maintestid=" + tlc);
        }
        catch (Exception exc)
        {
            if (exc.Message.Equals("Exception aborted."))
            {
                return;
            }
            else
            {
                Response.Redirect("AddTest.aspx?Maintestid=" + tlc);
                // Response.Cookies["error"].Value = exc.Message;
                // Server.Transfer("~/ErrorMessage.aspx");
            }
        }

    }
    protected void GVMainTest_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string TEST = GVMainTest.Rows[e.RowIndex].Cells[4].Text;
            MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C();
            Obj_MTB_C.MTCode = TEST.ToString();
            string SDCode = GVMainTest.Rows[e.RowIndex].Cells[2].Text;
            Obj_MTB_C.MTCode = TEST.ToString();
            if (MainTest_Bal_C.ISTestCodeExist(TEST, Convert.ToInt32(Session["Branchid"]), SDCode))
            {
                lblerror.Visible = true;
                lblerror.Text = "Test already in use";
                GVMainTest.DataSource = (ArrayList)MainTestLog_Bal_C.GetMaintest_SDCode(ddlsubdept.SelectedValue, Convert.ToInt32(Session["Branchid"]));
                GVMainTest.DataBind();
            }
            else if (MainTest_Bal_C.ISTestCodeExistGroupDetails(TEST, Convert.ToInt32(Session["Branchid"]), SDCode))
            {
                lblerror.Visible = true;
                lblerror.Text = "Test already in use";
                GVMainTest.DataSource = (ArrayList)MainTestLog_Bal_C.GetMaintest_SDCode(ddlsubdept.SelectedValue, Convert.ToInt32(Session["Branchid"]));
                GVMainTest.DataBind();
            }
            else
            {
                Obj_MTB_C.Delete(Convert.ToInt32(Session["Branchid"]));
                GVMainTest.DataSource = (ArrayList)MainTestLog_Bal_C.GetMaintest_SDCode(ddlsubdept.SelectedValue, Convert.ToInt32(Session["Branchid"]));
                GVMainTest.DataBind();
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
    protected void GVMainTest_Sorting(object sender, GridViewSortEventArgs e)
    {

        bool sort = true;
        try
        {
            if (ViewState["hashtable"].ToString() != "")
                ht1 = (Hashtable)ViewState["hashtable"];
            if (ht1.Count > 0)

                if (ht1[GVMainTest.PageIndex].ToString() == "true")
                {
                    sort = false;

                }
        }
        catch
        {

        }
        if (sort == true)
        {
            for (int i = 0; i < GVMainTest.Rows.Count; i++)
            {
                GVMainTest.Rows[i].Cells[8].Text = Convert.ToString(Convert.ToInt32(ViewState["maxordno"]) + 1);
                ViewState["maxordno"] = Convert.ToInt32(ViewState["maxordno"]) + 1;
                MainTestLog_Bal_C.updateordnowhesorted(Convert.ToInt32(GVMainTest.DataKeys[i].Value), GVMainTest.Rows[i].Cells[3].Text, Convert.ToInt32(GVMainTest.Rows[i].Cells[8].Text), Convert.ToInt32(Session["Branchid"]));

            }
            ht1.Add(GVMainTest.PageIndex, "true");
            ViewState["hashtable"] = ht1;
        }
        sort = true;
    }
    protected void btnseq_Click(object sender, EventArgs e)
    {
        GVMainTest.AllowPaging = false;
        ViewState["maxordno"] = 0;
        GVMainTest.DataSource = (ArrayList)MainTestLog_Bal_C.GetMaintest_SDCode(ddlsubdept.SelectedValue, Convert.ToInt32(Session["Branchid"]));
        GVMainTest.DataBind();

        for (int i = 0; i < GVMainTest.Rows.Count; i++)
        {
            GVMainTest.Rows[i].Cells[8].Text = Convert.ToString(Convert.ToInt32(ViewState["maxordno"]) + 1);
            ViewState["maxordno"] = Convert.ToInt32(ViewState["maxordno"]) + 1;
            MainTestLog_Bal_C.updateordnowhesorted(Convert.ToInt32(GVMainTest.DataKeys[i].Value), GVMainTest.Rows[i].Cells[3].Text, Convert.ToInt32(GVMainTest.Rows[i].Cells[8].Text), Convert.ToInt32(Session["Branchid"]));
            ShformmstL_Bal_C.Update_ResMSt_whenSort(GVMainTest.Rows[i].Cells[3].Text, Convert.ToInt32(GVMainTest.Rows[i].Cells[8].Text), Convert.ToInt32(Session["Branchid"]));

        }
        GVMainTest.AllowPaging = true;
        GVMainTest.DataSource = (ArrayList)MainTestLog_Bal_C.GetMaintest_SDCode(ddlsubdept.SelectedValue, Convert.ToInt32(Session["Branchid"]));
        GVMainTest.DataBind();

    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (ddlsubdept.SelectedIndex != 0)
        {
            ViewState["maxordno"] = 0;
            GVMainTest.DataSource = (ArrayList)MainTestLog_Bal_C.GetMaintest_SDCode(ddlsubdept.SelectedValue, Convert.ToInt32(Session["Branchid"]));
            GVMainTest.DataBind();
            btnseq.Visible = true;
            btnseq.Enabled = true;
        }
        if (txttestname.Text != "")
        {
            GVMainTest.DataSource = MainTestLog_Bal_C.Get_MainTest(txttestname.Text, Convert.ToInt32(Session["Branchid"]), ddlsubdept.SelectedValue);
            GVMainTest.DataBind();
        }
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTests(string prefixText, int count)
    {
        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlDataAdapter sda = new SqlDataAdapter("select MTCode from MainTest where MTCode like '" + prefixText + "%' and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + " ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["MTCode"], i);
            i++;
        }

        return tests;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTestName(string prefixText, int count)
    {
        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlDataAdapter sda = new SqlDataAdapter("select Maintestname from MainTest where  MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%'   and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + " ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] testname = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            testname.SetValue(dr["Maintestname"], i);
            i++;
        }

        return testname;
    }
    protected void ddlsubdept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlsubdept_TextChanged(object sender, EventArgs e)
    {

    }
    // =========================== Add Logic Test Parameter ======================================
    protected void Btn_Add_Dept_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubDeptAdd.aspx", false);
    }
    protected void Btn_Add_Test_Click(object sender, EventArgs e)
    {

        // Response.Redirect("ShowTest.aspx", false);
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