using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Drawing;

public partial class CreatePackage : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    short shHeadCnt = 0;
    string sSampleType = "";
    PackageName_Bal_C gnm = null;
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.SetFocus(txtPackagename);


        if (!IsPostBack)
        {

            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        // checkexistpageright("CreatePackage.aspx");
                    }
                }
                if (Request.QueryString["PackageCode"] != null)
                {
                    string PakCode = Request.QueryString["PackageCode"].ToString();
                    PackageName_Bal_C PakC = new PackageName_Bal_C(PakCode);
                    txtPackagecode.Text = PakC.PackageCode;
                    txtPackagename.Text = PakC.PackageName;
                    txtrateamt.Text = Convert.ToString(PakC.PackageRateAmount);
                    IEnumerator ie = PackageL_Bal_C.Get_PackageDetails(PakCode, Convert.ToInt32(Session["Branchid"])).GetEnumerator();
                    while (ie.MoveNext())
                    {
                        bool flag = false;
                        foreach (ListItem li in chkselectedtest.Items)
                        {
                            if (li.Value == (ie.Current as Package_Bal_C).MTCode)
                                flag = true;
                        }
                        if (!flag)
                        {
                            ListItem li = new ListItem((ie.Current as Package_Bal_C).TestName, (ie.Current as Package_Bal_C).MTCode);
                            li.Selected = true;
                            chkselectedtest.Items.Add(li);
                        }

                    }

                    ViewState["upflag"] = 1;

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
  

    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtPackagecode.Text.Trim().Length != 4)
            {
                Label1.Visible = true;
                Label1.Text = "Enter code 4 digits code!";
                Label1.ForeColor = Color.Red;
                txtPackagecode.Focus();
                return;
            }
            else
            {
                Label1.Visible = false;
            }
            if (Convert.ToInt32(ViewState["upflag"]) != 1)
            {
                if (Packagenew_Bal_C.CheckPackagecode_exist(txtPackagecode.Text.Trim(), Convert.ToInt32(Session["Branchid"])))
                {
                    Label1.Visible = true;
                    Label1.Text = "Package Code already exist .";
                    txtPackagecode.Focus();
                    return;
                }
                else
                {
                    Label1.Visible = false;
                }

                if (Packagenew_Bal_C.CheckPackagename_exist(txtPackagename.Text.Trim(), txtPackagecode.Text.Trim(), Convert.ToInt32(Session["Branchid"])))
                {
                    Label1.Visible = true;
                    Label1.Text = "Package Name already exist.";
                    txtPackagename.Focus();
                    return;
                }
                else
                {
                    Label1.Visible = false;
                }

                PackageName_Bal_C Pak_C = new PackageName_Bal_C();
                Pak_C.PackageCode = txtPackagecode.Text.Trim();
                Pak_C.PackageName = txtPackagename.Text.Trim();
                if (txtrateamt.Text != "")
                {
                    Pak_C.PackageRateAmount = Convert.ToInt32(txtrateamt.Text);
                }
                else
                {
                    Pak_C.PackageRateAmount = 0;

                }
                Pak_C.Patregdate = Date.getOnlydate();


                Pak_C.P_username = Convert.ToString(Session["username"]);
                Pak_C.Insert(Convert.ToInt32(Session["Branchid"]));

                foreach (ListItem li in chkselectedtest.Items)
                {
                    if (li.Selected)
                    {
                        Package_Bal_C Obj_PBC = new Package_Bal_C();
                        Obj_PBC.Patregdate = Date.getOnlydate();
                        Obj_PBC.PackageCode = txtPackagecode.Text.Trim();
                        Obj_PBC.PackageName = txtPackagename.Text.Trim();

                        MainTest_Bal_C MTC = new MainTest_Bal_C("", "", li.Value, Convert.ToInt32(Session["Branchid"]));
                        Obj_PBC.SDCode = MTC.SDCode;
                        Obj_PBC.TestName = MTC.Maintestname;
                        Obj_PBC.TestRate = MTC.Samecontain;
                        Obj_PBC.MTCode = MTC.MTCode;
                        Obj_PBC.Testordno = MTC.Testordno;
                        Obj_PBC.P_username = Convert.ToString(Session["username"]);
                        Obj_PBC.Insert(Convert.ToInt32(Session["Branchid"]));
                    }
                }
                Label1.Visible = true;
                Label1.Text = "Record Saved Successfully";
            }
            else
            {
                PackageName_Bal_C Pak_C = new PackageName_Bal_C();
                Pak_C.PackageCode = txtPackagecode.Text.Trim();
                Pak_C.PackageName = txtPackagename.Text.Trim();
                if (txtrateamt.Text != "")
                {
                    Pak_C.PackageRateAmount = Convert.ToInt32(txtrateamt.Text);
                }
                else
                {
                    Pak_C.PackageRateAmount = 0;
                }
                Pak_C.Patregdate = Date.getOnlydate();

                Pak_C.P_username = Convert.ToString(Session["username"]);
                Pak_C.update(Request.QueryString["PackageCode"].ToString(), Convert.ToInt32(Session["Branchid"]));

                Package_Bal_C Pack_C = new Package_Bal_C();
                Pack_C.DeleteByPack_Code(Request.QueryString["PackageCode"].ToString(), Convert.ToInt32(Session["Branchid"]));

                foreach (ListItem li in chkselectedtest.Items)
                {
                    if (li.Selected)
                    {
                        Package_Bal_C Obj_PBC = new Package_Bal_C();
                        Obj_PBC.Patregdate = Date.getOnlydate();
                        Obj_PBC.PackageCode = txtPackagecode.Text.Trim();
                        Obj_PBC.PackageName = txtPackagename.Text.Trim();

                        MainTest_Bal_C MTC = new MainTest_Bal_C("", "", li.Value, Convert.ToInt32(Session["Branchid"]));
                        Obj_PBC.SDCode = MTC.SDCode;
                        Obj_PBC.TestName = MTC.Maintestname;
                        Obj_PBC.TestRate = MTC.Samecontain;
                        Obj_PBC.MTCode = MTC.MTCode;
                        Obj_PBC.P_username = Convert.ToString(Session["username"]);
                        Obj_PBC.Insert(Convert.ToInt32(Session["Branchid"]));

                    }
                }
                Label1.Visible = true;
                Label1.Text = "Record update Successfully";
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


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Showpackage.aspx");
    }
    protected void txtCode_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void txttests_TextChanged(object sender, EventArgs e)
    {
        string[] splittedtlcode;
        splittedtlcode = txttests.Text.Split('-');
        try
        {

            bool flag = false;
            foreach (ListItem li in chkselectedtest.Items)
            {
                if (li.Value == splittedtlcode[0])
                    flag = true;
            }
            if (!flag)
            {
                if (splittedtlcode.Length > 2)
                {
                    ListItem li1 = new ListItem(splittedtlcode[1] + ' ' + splittedtlcode[2], splittedtlcode[0]);
                    li1.Selected = true;
                    chkselectedtest.Items.Add(li1);
                }
                else
                {
                    ListItem li = new ListItem(splittedtlcode[1], splittedtlcode[0]);
                    li.Selected = true;
                    chkselectedtest.Items.Add(li);
                }

            }

            txttests.Text = "";
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
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTests(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
            sda = new SqlDataAdapter(" select MTCode, Maintestname from MainTest WHERE (MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%')  order by Maintestname ", con);
        else
            sda = new SqlDataAdapter("select MTCode, Maintestname from MainTest WHERE MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%' order by Maintestname ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["MTCode"] + " - " + dr["Maintestname"], i);
            i++;
        }

        return tests;
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