using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class AddTestParameter :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["usertype"]) != "Administrator")
            {
                //checkexistpageright("AddTestParameter.aspx");
            }
           
            if (Request.QueryString["Code"] != null)
            {
                string C_Code = (Request.QueryString["Code"].ToString());
                string tname = MainTestLog_Bal_C.GET_Maintest_name(C_Code, Convert.ToInt32(Session["Branchid"]));
                Label2.Text = tname.ToString();
                testgrid.DataSource = SubTestLog_Bal_C.Get_AllSubTest(C_Code, Convert.ToInt32(Session["Branchid"]));
                testgrid.DataBind();
            }
        }
    }
   

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        string SD_Code = MainTestLog_Bal_C.GetSDCode_AgainsMainTest(Request.QueryString["Code"].Trim(), Convert.ToInt32(Session["Branchid"]));
        Response.Redirect("Newtestparameter.aspx?SDCode=" + SD_Code + "&MTCode=" + Request.QueryString["Code"].Trim());
    }
    protected void testgrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex == -1)
            return;

        int ID = Convert.ToInt32(testgrid.DataKeys[e.RowIndex].Value);
        SubTest_Bal_C STB_C = new SubTest_Bal_C();
        STB_C.TestID = ID;
        STB_C.STCODE = testgrid.Rows[e.RowIndex].Cells[0].Text.Trim();
        STB_C.Delete();
        testgrid.DataSource = SubTestLog_Bal_C.Get_AllSubTest(Request.QueryString["Code"].ToString(), Convert.ToInt32(Session["Branchid"]));
        testgrid.DataBind();

    }
    protected void testgrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void testgrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;

    }



    protected void btnAddHead_Click(object sender, EventArgs e)
    {
        Response.Redirect("Addshowparameterspace.aspx?testcode=" + "H" + "&MTCode=" + Request.QueryString["Code"].Trim() + "&SDCode=" + MainTestLog_Bal_C.GetSDCode_AgainsMainTest(Request.QueryString["Code"].ToString(), Convert.ToInt32(Session["Branchid"])));
    }
    protected void testgrid_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
        string TestID = testgrid.DataKeys[e.Row.RowIndex].Value.ToString();
        (e.Row.Cells[3].Controls[0] as HyperLink).NavigateUrl = "Newtestparameter.aspx?TestID=" + TestID + "&MTCode=" + Request.QueryString["Code"].Trim();
        if (e.Row.Cells[0].Text.Trim() == "H" || e.Row.Cells[0].Text.Trim() == "S")
        {
        
            (e.Row.Cells[3].Controls[0] as HyperLink).NavigateUrl = "Addshowparameterspace.aspx?TestID=" + TestID + "&MTCode=" + Request.QueryString["Code"].Trim();
        }

    }
    protected void testgrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void testgrid_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void testgrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        testgrid.PageIndex = e.NewPageIndex;
        if (Request.QueryString["Code"] != null)
        {
            string C_Code = (Request.QueryString["Code"].ToString());
            string tname = MainTestLog_Bal_C.GET_Maintest_name(C_Code, Convert.ToInt32(Session["Branchid"]));

            testgrid.DataSource = SubTestLog_Bal_C.Get_AllSubTest(C_Code, Convert.ToInt32(Session["Branchid"]));
            testgrid.DataBind();
        }
    }
    protected void testgrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        Response.Redirect("Newtestparameter.aspx?TestID='" + Convert.ToInt32(testgrid.DataKeys[e.NewEditIndex].Value) + "'&MTcode='" + Request.QueryString["MTcode"] + "'");
    }
    
    protected void btnseq_Click(object sender, EventArgs e)
    {
        testgrid.AllowPaging = false;
        ViewState["maxordno"] = 0;
        string code = (Request.QueryString["Code"].ToString());
        testgrid.DataSource = SubTestLog_Bal_C.Get_AllSubTest(code, Convert.ToInt32(Session["Branchid"]));
        testgrid.DataBind();

        for (int i = 0; i < testgrid.Rows.Count; i++)
        {
            testgrid.Rows[i].Cells[2].Text = Convert.ToString(Convert.ToInt32(ViewState["maxordno"]) + 1);
            ViewState["maxordno"] = Convert.ToInt32(ViewState["maxordno"]) + 1;
            ShformmstL_Bal_C.Update_testforResMst_sort(testgrid.Rows[i].Cells[0].Text, Convert.ToInt32(testgrid.Rows[i].Cells[2].Text), Convert.ToInt32(Session["Branchid"]));
            SubTestLog_Bal_C.UpdateTest_againstsorting1(testgrid.Rows[i].Cells[0].Text, testgrid.Rows[i].Cells[1].Text, Convert.ToInt32(testgrid.Rows[i].Cells[2].Text), Convert.ToInt32(Session["Branchid"]));


        }
        testgrid.AllowPaging = true;
        testgrid.DataSource = SubTestLog_Bal_C.Get_AllSubTest(code, Convert.ToInt32(Session["Branchid"]));
        testgrid.DataBind();
    }
    protected void btnaddspace_Click(object sender, EventArgs e)
    {
        Response.Redirect("Addspace.aspx?testcode=" + "B" + "&MTCode=" + Request.QueryString["Code"].Trim() + "&SDCode=" + MainTestLog_Bal_C.GetSDCode_AgainsMainTest(Request.QueryString["Code"].ToString(), Convert.ToInt32(Session["Branchid"])));

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