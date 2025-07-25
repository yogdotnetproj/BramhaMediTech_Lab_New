using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class Newtestparameter : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    SubTest_Bal_C tst = null;
    int uflag;
    SubTest_Bal_C STBC = null;
   
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                   // checkexistpageright("Newtestparameter.aspx");
                } 
                if (Request.QueryString["SDCode"] != null && Request.QueryString["MTCode"] != null)
                {
                    string TCode = Request.QueryString["SDCode"].ToString().Trim();

                    lblTestname.Text = MainTestLog_Bal_C.GET_Maintest_name(Request.QueryString["MTCode"].Trim(), Convert.ToInt32(Session["Branchid"]));
                    string MTCode = Request.QueryString["MTCode"].ToString().Trim();

                    int fnum = MainTest_Bal_C.MaxFieldOrder(Request.QueryString["MTCode"].Trim(), Convert.ToInt32(Session["Branchid"]));
                    int Fieldno = fnum + 1;
                    txtparaOrder.Text = Fieldno.ToString();
                }

                if (Request.QueryString["TestID"] != null)
                {
                    ViewState["uflag"] = 1;
                    txtparaOrder.ReadOnly = false;
                    int tename = Convert.ToInt32(Request.QueryString["TestID"]);
                    SubTest_Bal_C STBC = new SubTest_Bal_C(tename);

                    lblTestname.Text = MainTestLog_Bal_C.GET_Maintest_name(STBC.MTCode, Convert.ToInt32(Session["Branchid"]));

                   // ddlmname.SelectedValue = STBC.P_Machincode.ToString();
                    txtparaCode.Text = STBC.STCODE;
                    txtparaOrder.Text = Convert.ToString(STBC.Testordno);
                    ViewState["orderno"] = STBC.Testordno;
                    txtparaMeth.Text = STBC.TestMethod;
                    txtparaName.Text = STBC.TestName;
                    txtdefaultres.Text = STBC.DefaultResult;
                  
                    txtshortform.Text = STBC.P_shortform;

                    if (STBC.TextDesc == "TextField" || STBC.TextDesc == "Text Field")
                    {
                        RblNonDescriptive.Checked = true;
                        RblNonDescriptive_CheckedChanged(RblNonDescriptive, null);
                       // txtdefaultres.Text = STBC.DefaultResult;
                    }
                    else
                    {
                        RblDescriptive.Checked = true;
                        RblDescriptive_CheckedChanged(RblDescriptive, null);
                    }
                    Label8.Visible = false;

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
            
            Label8.Visible = true;
            string hhcode = null;
            if (Request.QueryString["SDCode"] != null)
            {
                hhcode = Request.QueryString["SDCode"].ToString().Trim();
            }
            SubTest_Bal_C SubTB_C = new SubTest_Bal_C();

            if (Convert.ToInt32(ViewState["uflag"]) == 1)
            {

                if (SubTestLog_Bal_C.IS_Subtestcode_Exists(txtparaCode.Text.Trim(), Convert.ToInt32(Request.QueryString["TestID"].ToString().Trim()), Convert.ToInt32(Session["Branchid"])))
                {
                    Label8.Visible = true;
                    Label8.Text = "STCODE already exist.";
                    txtparaCode.Focus();
                    return;
                }
                else
                {
                    Label8.Visible = false;
                }

                if (SubTestLog_Bal_C.IS_SubtestExist(txtparaName.Text.Trim(), Convert.ToInt32(Request.QueryString["TestID"].ToString().Trim()), Request.QueryString["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"])))
                {
                    Label8.Visible = true;
                    Label8.Text = "Test Name already exist.";
                    txtparaName.Focus();
                    return;
                }
                else
                {
                    Label8.Visible = false;
                }
                int tename1 = Convert.ToInt32(Request.QueryString["TestID"]);
                SubTest_Bal_C STBC = new SubTest_Bal_C(tename1);
                string oldtestName = STBC.TestName;

                if (RblNonDescriptive.Checked == true)
                {
                    SubTB_C.STCODE = txtparaCode.Text;
                    SubTB_C.TestName = txtparaName.Text;
                    SubTB_C.SDCode = STBC.SDCode;
                    SubTB_C.MTCode = STBC.MTCode;
                    SubTB_C.Testordno = Convert.ToInt32(txtparaOrder.Text);
                    SubTB_C.TextDesc = "TextField";
                    SubTB_C.TestMethod = txtparaMeth.Text;
                    SubTB_C.Patregdate = Date.getdate();
                  
                    SubTB_C.P_shortform = txtshortform.Text.Trim();
                 
                    if (txtdefaultres.Text != "")
                    {
                        SubTB_C.DefaultResult = txtdefaultres.Text;
                    }

                    if (Convert.ToInt32(ViewState["orderno"]) != Convert.ToInt32(txtparaOrder.Text))
                    {
                        SubTestLog_Bal_C.Update_Packageno(SubTB_C, Convert.ToInt32(ViewState["orderno"]), Convert.ToInt32(Session["Branchid"]));
                    }


                    if (SubTB_C.Update(Convert.ToInt32(Request.QueryString["TestID"])))
                    {
                       
                            Stformmst_Bal_C SFBC = new Stformmst_Bal_C();
                            SFBC.UpdateTestnameandOrdNo(STBC.MTCode, txtparaCode.Text, txtparaName.Text, Convert.ToInt32(txtparaOrder.Text), oldtestName, Convert.ToInt32(Session["Branchid"]));

                            Label8.Visible = true;
                            Label8.Text = "Record Updated successfully.";
                       

                    }

                    try
                    {

                        SubTB_C.MTCode = STBC.MTCode;
                        SubTB_C.STCODE = txtparaCode.Text;
                       
                    }
                    catch (Exception exc)
                    {

                    }
                }
                else
                {
                    if (SubTestLog_Bal_C.IS_Subtestcode_Exists(txtparaCode.Text.Trim(), Convert.ToInt32(Request.QueryString["TestID"].ToString().Trim()), Convert.ToInt32(Session["Branchid"])))
                    {
                        Label8.Visible = true;
                        Label8.Text = "STCODE already exist.";
                        txtparaCode.Focus();
                        return;
                    }
                    else
                    {
                        Label8.Visible = false;
                    }

                    if (SubTestLog_Bal_C.IS_SubtestExist(txtparaName.Text.Trim(), Convert.ToInt32(Request.QueryString["TestID"].ToString().Trim()), Request.QueryString["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"])))
                    {
                        Label8.Visible = true;
                        Label8.Text = "Test Name already exist.";
                        txtparaName.Focus();
                        return;
                    }
                    else
                    {
                        Label8.Visible = false;
                    }
                    SubTB_C.STCODE = txtparaCode.Text;
                    SubTB_C.TestName = txtparaName.Text;
                    SubTB_C.SDCode = STBC.SDCode;
                    SubTB_C.MTCode = STBC.MTCode;
                    SubTB_C.Testordno = Convert.ToInt32(txtparaOrder.Text);
                    SubTB_C.TextDesc = "DescType";
                    SubTB_C.TestMethod = txtparaMeth.Text;
                    SubTB_C.Patregdate = Date.getdate();
                   
                    SubTB_C.P_shortform = txtshortform.Text.Trim();
                 
                    if (txtdefaultres.Text != "")
                    {
                        SubTB_C.DefaultResult = txtdefaultres.Text;
                    }

                    SubTestLog_Bal_C.Update_Packageno(SubTB_C, Convert.ToInt32(ViewState["orderno"]), Convert.ToInt32(Session["Branchid"]));
                    if (SubTB_C.Update(Convert.ToInt32(Request.QueryString["TestID"])))
                    {
                       
                            Label8.Text = "Record Updated successfully";

                            Stformmst_Bal_C SFBC = new Stformmst_Bal_C();
                            SFBC.UpdateTestnameandOrdNo(STBC.MTCode, txtparaCode.Text, txtparaName.Text, Convert.ToInt32(txtparaOrder.Text), oldtestName, Convert.ToInt32(Session["Branchid"]));
                       
                    }
                }

                SubTest_Bal_C STB = new SubTest_Bal_C(Convert.ToInt32(Request.QueryString["TestID"].Trim()));
                try
                {

                    SubTB_C.MTCode = STBC.MTCode;
                    SubTB_C.STCODE = txtparaCode.Text;
                   
                }
                catch (Exception exc)
                {

                }
                this.btnAddRange_Click(null,null);
                Response.Redirect("AddTestParameter.aspx?Code=" + STB.MTCode);

            }
            else
            {
                if (SubTestLog_Bal_C.IS_Subtestcode_Exists(txtparaCode.Text.Trim(), Convert.ToInt32(Session["Branchid"])))
                {
                    Label8.Visible = true;
                    Label8.Text = "STCODE already exist.";
                    txtparaCode.Focus();
                    return;
                }
                else
                {
                    Label8.Visible = false;
                }
                string ss = Request.QueryString["MTCode"].ToString();

                if (SubTestLog_Bal_C.IS_SubtestExist(txtparaName.Text.Trim(), Request.QueryString["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"])))
                {
                    Label8.Visible = true;
                    Label8.Text = "Test Name already exist.";
                    txtparaName.Focus();
                    return;
                }
                else
                {
                    Label8.Visible = false;
                }
                ICollection icol = SubTestLog_Bal_C.GetTestCode_SDCode(hhcode, Request.QueryString["MTCode"].Trim(), Convert.ToInt32(Session["Branchid"]));
                if (RblNonDescriptive.Checked == true)
                {
                    SubTest_Bal_C SubTB = new SubTest_Bal_C();
                    SubTB.STCODE = txtparaCode.Text;
                    SubTB.TestName = txtparaName.Text;
                    SubTB.SDCode = hhcode.ToString();
                    SubTB.MTCode = Request.QueryString["MTCode"].Trim();
                    SubTB.Testordno = Convert.ToInt32(txtparaOrder.Text);
                    SubTB.TextDesc = "TextField";
                    SubTB.TestMethod = txtparaMeth.Text;
                    SubTB.Patregdate = Date.getdate();
                
                    SubTB.P_shortform = txtshortform.Text.Trim();
                    if (txtdefaultres.Text != "")
                    {
                        SubTB.DefaultResult = txtdefaultres.Text;
                    }
                    SubTB.Insert(Convert.ToInt32(Session["Branchid"]));
                    Label8.Text = "Record save successfully.";
                }
                else
                {
                    SubTest_Bal_C SubTB = new SubTest_Bal_C();
                    SubTB.STCODE = txtparaCode.Text;
                    SubTB.TestName = txtparaName.Text;
                    SubTB.SDCode = hhcode.ToString();
                    SubTB.MTCode = Request.QueryString["MTCode"].Trim();

                    SubTB.Testordno = Convert.ToInt32(txtparaOrder.Text);
                    SubTB.TextDesc = "DescType";
                    SubTB.TestMethod = txtparaMeth.Text;
                    SubTB.Patregdate = Date.getdate();
                    
                    SubTB.P_shortform = txtshortform.Text.Trim();
                    if (txtdefaultres.Text != "")
                    {
                        SubTB.DefaultResult = txtdefaultres.Text;
                    }
                    SubTB.Insert(Convert.ToInt32(Session["Branchid"]));
                    Label8.Text = "Record save successfully.";
                }

                try
                {

                    SubTB_C.MTCode = STBC.MTCode;
                    SubTB_C.STCODE = txtparaCode.Text;
              
                }
                catch (Exception exc)
                {

                }
                this.btnAddRange_Click(null, null);
                Response.Redirect("AddTestParameter.aspx?Code=" + Request.QueryString["MTCode"].Trim());
            }//MTCode=CP001


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
        try
        {
            if (Request.QueryString["TestID"] != null)
            {
                SubTest_Bal_C tnt = new SubTest_Bal_C(Convert.ToInt32(Request.QueryString["TestID"].Trim()));

                Response.Redirect("AddTestParameter.aspx?Code=" + tnt.MTCode);
            }
            else
            {
                Response.Redirect("AddTestParameter.aspx?Code=" + Request.QueryString["MTCode"].Trim());
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

    protected void RblNonDescriptive_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            TestHelp_Bal_C hel = new TestHelp_Bal_C();
            hel.P_tlcode = Request.QueryString["MTCode"].Trim();
            hel.P_Testcode = txtparaCode.Text;
            hel.P_branchid = Convert.ToInt32(Session["Branchid"]);
           
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

    protected void RblDescriptive_CheckedChanged(object sender, EventArgs e)
    {

      
    }
   
  

    protected void txtCode_TextChanged(object sender, EventArgs e)
    {
        
    }
    protected void txtparaName_TextChanged(object sender, EventArgs e)
    {
        if (txtparaName.Text != "")
        {
            int num1 = SubdepartmentLogic_Bal_C.MaxParameterOrder(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]));
            int ono = num1 + 1;
            if (txtparaName.Text.Length <= 2)
            {
                if (txtparaCode.Text.Trim() == "")
                {
                    txtparaCode.Text = ono + "" + txtparaName.Text;
                }
            }
            else
            {
                if (txtparaCode.Text.Trim() == "")
                {
                    txtparaCode.Text = ono + "" + txtparaName.Text.Substring(0, 3);
                }
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

    protected void btnAddRange_Click(object sender, EventArgs e)
    {
        Refrangemst_Bal_C ObjRBC = new Refrangemst_Bal_C();
        if (txtparaCode.Text.ToString() == "")
        {
            Label8.Visible = true;
            Label8.Text = " select TestName";
            return;
        }
        else
        {
            Label8.Visible = false;
            Label8.Text = "";
        }
        if (txtMalelowerrange.Text != "" && txtMalelowerrange.Text != "")
        {
            ObjRBC.MTCode = Request.QueryString["MTCode"].Trim();
            ObjRBC.Unit = txtMaleunit.Text;
            ObjRBC.TestName = lblTestname.Text;
            ObjRBC.Sex = "Male";
            ObjRBC.DescretiveRange = txtMalenormalRange.Text;
            ObjRBC.GreaterThanDays = 36500;
            ObjRBC.LessThanDays = 0; ;
            ObjRBC.STCODE = txtparaCode.Text;
            ObjRBC.LowerRange = txtMalelowerrange.Text;
            ObjRBC.UpperRange = txtMaleupperrange.Text;
            ObjRBC.PanicLowerRange = "";
            ObjRBC.PanicUpperRange = "";
            ObjRBC.P_username = Convert.ToString(Session["username"]);

            ObjRBC.P_OutLabName = "0";
            ObjRBC.Insert(Convert.ToInt32(Session["Branchid"]));
        }
        if (txtFemaleLowerrange.Text != "" && txtFemaleupperrange.Text != "")
        {
            ObjRBC.MTCode = Request.QueryString["MTCode"].Trim();
            ObjRBC.Unit = txtFemaleUnit.Text;
            ObjRBC.TestName = lblTestname.Text;
            ObjRBC.Sex = "Female";
            ObjRBC.DescretiveRange = txtfemaleNormalRange.Text;
            ObjRBC.GreaterThanDays = 36500;
            ObjRBC.LessThanDays = 0; ;
            ObjRBC.STCODE = txtparaCode.Text;
            ObjRBC.LowerRange = txtFemaleLowerrange.Text;
            ObjRBC.UpperRange = txtFemaleupperrange.Text;
            ObjRBC.PanicLowerRange = "";
            ObjRBC.PanicUpperRange = "";
            ObjRBC.P_username = Convert.ToString(Session["username"]);

            ObjRBC.P_OutLabName = "0";
            ObjRBC.Insert(Convert.ToInt32(Session["Branchid"]));
        }
       


    }

    private void funAllowLowerRange(string lowerrange)
    {
        try
        {
            string str = lowerrange;
            string dot = ".";
            string less = "<";
            string gr = ">";
            ArrayList al = new ArrayList();
            al.Add(1); al.Add(2); al.Add(3); al.Add(4); al.Add(5);
            al.Add(6); al.Add(7); al.Add(8); al.Add(9); al.Add(0);
            al.Add(dot); al.Add(less); al.Add(gr);
            ArrayList ok = new ArrayList();
            for (int i = 0; i < al.Count; i++)
            {
                for (int j = 0; j <= str.Length - 1; j++)
                {
                    if (al[i].ToString() == str.Substring(j, 1))
                    {

                        Label8.Visible = false;
                        ok.Add("ok");

                    }

                }
            }
            if (ok.Count == str.Length)
            {

                Label8.Visible = false;
            }
            else
            {
                Label8.Visible = true;
                Label8.Text = "Plz Enter Valid Data.";
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
    private void funAllowUpperRange(string upperrange)
    {
        try
        {
            string str = upperrange;
            string dot = ".";
            string less = "<";
            string gr = ">";
            ArrayList al = new ArrayList();
            al.Add(1); al.Add(2); al.Add(3); al.Add(4); al.Add(5);
            al.Add(6); al.Add(7); al.Add(8); al.Add(9); al.Add(0);
            al.Add(dot); al.Add(less); al.Add(gr);
            ArrayList ok = new ArrayList();
            for (int i = 0; i < al.Count; i++)
            {
                for (int j = 0; j <= str.Length - 1; j++)
                {
                    if (al[i].ToString() == str.Substring(j, 1))
                    {

                        Label8.Visible = false;
                        ok.Add("ok");

                    }

                }
            }
            if (ok.Count == str.Length)
            {

                Label8.Visible = false;
            }
            else
            {
                Label8.Visible = true;
                Label8.Text = "Plz Enter Valid Data.";
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
    protected void txtMalelowerrange_TextChanged(object sender, EventArgs e)
    {
        if (txtMalelowerrange.Text != "")
        {
            funAllowLowerRange(txtMalelowerrange.Text);
        }
        if (txtMaleupperrange.Text != "")
        {
            funAllowUpperRange(txtMaleupperrange.Text);
        }
      //  txtMaleupperrange.Focus();
    }
    protected void txtFemaleLowerrange_TextChanged(object sender, EventArgs e)
    {
        if (txtFemaleLowerrange.Text != "")
        {
            funAllowLowerRange(txtFemaleLowerrange.Text);
        }
        if (txtFemaleupperrange.Text != "")
        {
            funAllowUpperRange(txtFemaleupperrange.Text);
        }
       // txtFemaleupperrange.Focus();
    }
}