using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Addspace :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                   // checkexistpageright("Addspace.aspx");
                }
               
                if (Request.QueryString["testcode"] != null && Request.QueryString["MTCode"] != null && Request.QueryString["SDCode"] != null)
                {

                    //int fnum = MainTest_Bal_C.MaxFieldOrder(Request.QueryString["MTCode"].Trim(), Convert.ToInt32(Session["Branchid"]));
                    //int Fieldno = fnum + 1;
                    //txttestorder.Text = Fieldno.ToString();
                    lblTestname.Text = MainTestLog_Bal_C.GET_Maintest_name(Request.QueryString["MTCode"].Trim(), Convert.ToInt32(Session["Branchid"]));
                }
                if (Request.QueryString["TestID"] != null)
                {
                    ViewState["updateflag"] = 1;
                    int tename = Convert.ToInt32(Request.QueryString["TestID"]);
                    SubTest_Bal_C te = new SubTest_Bal_C(tename);

                    txtTestName.Text = te.TestName;
                    txttestorder.Text = Convert.ToString(te.Testordno);
                    ViewState["orderno"] = te.Testordno;
                    txttestorder.ReadOnly = false;
                    lblTestname.Text = MainTestLog_Bal_C.GET_Maintest_name(te.MTCode, Convert.ToInt32(Session["Branchid"]));
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt32(ViewState["updateflag"]) != 1)
            {
                //if (SubTestLog_Bal_C.Existsubtest_ST_Space(txtTestName.Text, Request.QueryString["testcode"].ToString(), Request.QueryString["MTCode"].Trim(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(txttestorder.Text))
                //{
                //    Label8.Visible = true;
                //    Label8.Text = "Test Name already exist.";
                //    txtTestName.Focus();
                //    return;
                //}
                //else
                //{
                //    Label8.Visible = false;
                //}
                SubTest_Bal_C tn = new SubTest_Bal_C();
                tn.STCODE = Request.QueryString["testcode"].ToString();
                tn.TestName = "Blank" + txttestorder.Text;
                tn.SDCode = Request.QueryString["SDCode"];
                tn.MTCode = Request.QueryString["MTCode"].Trim();
                tn.Testordno = Convert.ToInt32(txttestorder.Text);
                tn.Patregdate = Date.getdate();
                tn.Insert(Convert.ToInt32(Session["Branchid"]));
                Label8.Visible = true;
                Label8.Text = "Record is Saved";
            }
            else
            {
                //if (SubTestLog_Bal_C.IS_SubtestExist(txtTestName.Text.Trim(), Convert.ToInt32(Request.QueryString["TestID"].ToString().Trim()), Request.QueryString["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"])))
                //{
                //    Label8.Visible = true;
                //    Label8.Text = "Test Name already exist.";
                //    txtTestName.Focus();
                //    return;
                //}
                //int tename1 = Convert.ToInt32(Request.QueryString["TestID"]);
                //SubTest_Bal_C te = new SubTest_Bal_C(tename1);
                //SubTest_Bal_C tnew = new SubTest_Bal_C();
                //tnew.TestName = txtTestName.Text;
                //tnew.STCODE = te.STCODE;
                //tnew.MTCode = te.MTCode;
                //tnew.SDCode = te.SDCode;
                //tnew.Testordno = Convert.ToInt32(txttestorder.Text);
                //if (Convert.ToInt32(ViewState["orderno"]) != Convert.ToInt32(txttestorder.Text))
                //{
                //    SubTestLog_Bal_C.Update_Packageno(tnew, Convert.ToInt32(ViewState["orderno"]), Convert.ToInt32(Session["Branchid"]));
                //}
                //if (tnew.Update(Convert.ToInt32(Request.QueryString["TestID"])))
                //{
                //    Label8.Text = "Record Updated";
                //}
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
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["TestID"] != null)
        {
            SubTest_Bal_C STBC = new SubTest_Bal_C(Convert.ToInt32(Request.QueryString["TestID"].Trim()));

            Response.Redirect("AddTestParameter.aspx?Code=" + STBC.MTCode);
        }
        else
        {
            Response.Redirect("AddTestParameter.aspx?Code=" + Request.QueryString["MTCode"].Trim());
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