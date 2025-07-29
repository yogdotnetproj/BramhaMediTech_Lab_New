using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
public partial class MachinMapCode :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Userright_Bal_C ObjAT = new Userright_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["HMS"]) != "Yes")
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("MachinMapCode.aspx");
                }
            }
            MachinBind();
        }
    }


    public void MachinBind()
    {
        dt = new DataTable();
        dt = ObjTB.Bind_MachinName();
        if (dt.Rows.Count > 0)
        {
            ddlMachinName.DataSource = dt;
            ddlMachinName.DataTextField = "Instumentname";
            ddlMachinName.DataValueField = "Instumentname";
            ddlMachinName.DataBind();
            ddlMachinName.Items.Insert(0, "Select Machin Name");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txttestname.Text != "")
        {
            string[] splittedtltextname;
            splittedtltextname = txttestname.Text.Split('=');
            dt = new DataTable();
            string ParMcode = "";
            if (txttestparameter.SelectedValue == "Select Test Parameter")
            {
                ParMcode = "";
            }
            else
            {
                ParMcode = txttestparameter.SelectedValue;
            }
            dt = ObjTB.Check_ExistMachinparameter(splittedtltextname[0], ParMcode, ddlMachinName.SelectedItem.Text);
            if (dt.Rows.Count > 0)
            {
                Label2.Text = "Machin Map parameter already exist.";
            }
            else
            {
                // string ParMcode = "";
                if (txttestparameter.SelectedValue == "Select Test Parameter")
                {
                    ParMcode = "";
                }
                else
                {
                    ParMcode = txttestparameter.SelectedValue;
                }
                ObjTB.Insert_MachinMapCode(ddlMachinName.SelectedItem.Text, splittedtltextname[0], ParMcode, txtMachintestparameter.Text);
                Label2.Text = "Record save successfully.";
                BindGrid();
            }
        }
    }
    protected void GV_UserType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_UserType.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void GV_UserType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int Mapid = Convert.ToInt32(GV_UserType.DataKeys[e.RowIndex].Value);

            ObjTB.delete_MapParameter(Mapid);
            BindGrid();
            Label2.Text = "Record deleted successfully.";
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
    protected void GV_UserType_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTestName(string prefixText, int count)
    {
        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlDataAdapter sda = new SqlDataAdapter("select MTCode+' = '+ Maintestname as Maintestname from MainTest where  MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%'   and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + " ", con);
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
    protected void txttestname_TextChanged(object sender, EventArgs e)
    {
        if (txttestname.Text != "")
        {
            string[] splittedtltextname;
            splittedtltextname = txttestname.Text.Split('=');
            ViewState["TestCode"] = splittedtltextname[0];
            BindGrid();
            Bind_TestParameter();
        }

    }
    public void Bind_TestParameter()
    {
        dt = new DataTable();
        dt = ObjTB.Bind_TestParameter("", Convert.ToString(ViewState["TestCode"]), Convert.ToInt32(Session["Branchid"]));
        if (dt.Rows.Count > 0)
        {
            txttestparameter.DataSource = dt;
            txttestparameter.DataTextField = "TestName";
            txttestparameter.DataValueField = "TestCode";
            txttestparameter.DataBind();
            txttestparameter.Items.Insert(0, "Select Test Parameter");
            btnsave.Visible = true;
        }
        else
        {
            txttestparameter.DataSource = null;
            txttestparameter.DataTextField = "TestName";
            txttestparameter.DataValueField = "TestCode";
            txttestparameter.DataBind();
            txttestparameter.Items.Insert(0, "Select Test Parameter");
            //btnsave.Visible = false;
            //string AA = "Descriptive Test not required Map code";
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);

        }
    }
    public void BindGrid()
    {
        dt = new DataTable();
        dt = ObjTB.Bind_Machinecodemap(Convert.ToString(ViewState["TestCode"]), ddlMachinName.SelectedItem.Text);
        if (dt.Rows.Count > 0)
        {
            GV_UserType.DataSource = dt;
            GV_UserType.DataBind();

        }
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTestCode(string prefixText, int count)
    {
        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlDataAdapter sda = new SqlDataAdapter("select STCODE as TestCode from SubTest where  STCODE like '%" + prefixText + "%' and MTCode='" + Convert.ToString(HttpContext.Current.Session["TestCode"]) + "'  and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + " ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] testname = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            testname.SetValue(dr["TestCode"], i);
            i++;
        }

        return testname;
        HttpContext.Current.Session["TestCode"] = "";
    }
    protected void ddlMachinName_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
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