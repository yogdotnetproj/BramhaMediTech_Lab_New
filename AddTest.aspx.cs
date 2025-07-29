using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class AddTest :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
   
    int g;
    Patmst_Bal_C PB_C = new Patmst_Bal_C();
    Barcode_C BAR = new Barcode_C();
    Cshmst_Bal_C ccm = new Cshmst_Bal_C();
    CalculateSet_Bal_C CB_C = new CalculateSet_Bal_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.SetFocus(ddlsubdept);

        Label1.Visible = false;
        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    //checkexistpageright("AddTest.aspx");
                }
               
                ddlsubdept.DataSource = SubdepartmentLogic_Bal_C.getSubDepartment(Convert.ToInt32(Session["Branchid"]), 0, 0);
                ddlsubdept.DataTextField = "subdeptName";
                ddlsubdept.DataValueField = "SDCode";
                ddlsubdept.DataBind();
                ddlsubdept.Items.Insert(0, "Select Department");
                ddlsubdept.SelectedIndex = -1;
                ViewState["headingindex"] = ddlsubdept.SelectedIndex;
                ViewState["UpdateSDCode"] = "false";


                ddlSampleType.DataSource = SampleType_Bal_C.getSampleType(Convert.ToInt32(Session["Branchid"]));
                ddlSampleType.DataTextField = "sampletype";
                ddlSampleType.DataValueField = "sampletype";
                ddlSampleType.DataBind();
                ddlSampleType.Items.Insert(0, "Select Sample Type");
                ddlSampleType.SelectedIndex = -1;

                if (Request.QueryString["SDCode"] != null)
                {
                    ddlsubdept.SelectedValue = Request.QueryString["SDCode"].Trim();
                }
               

                radioSingle_CheckedChanged(this, null);
                ViewState["descriptive"] = "";
                if (Request.QueryString["Maintestid"] != null)
                {
                    ViewState["seflag"] = 1;
                    int MAintestid = Convert.ToInt32(Request.QueryString["Maintestid"]);

                    MainTest_Bal_C MTBC = new MainTest_Bal_C(MAintestid, Convert.ToInt32(Session["Branchid"]));
                    ViewState["iOrdNo"] = MTBC.Testordno;
                    txtName.Text = MTBC.Maintestname;
                    txtOrder.Text = Convert.ToString(MTBC.Testordno);

                    ddlsubdept.SelectedValue = MTBC.SDCode;
                    ViewState["SDCode"] = MTBC.SDCode;


                    ddlSampleType.SelectedValue = MTBC.SampleType;
                    string titlecd = MTBC.MTCode;
                    ViewState["MTCode"] = MTBC.MTCode;
                    ViewState["oldMTCode"] = MTBC.MTCode;
                    ViewState["Maintestname"] = MTBC.Maintestname;

                    txtCode.Text = MTBC.MTCode;
                    txtshortform.Text = MTBC.Shortform;
                    txtMethod.Text = MTBC.DefaultTestMethod;
                    //= txtshortform.Text = MTBC.P_TatName;
                    if (MTBC.P_TatName == "Day")
                    {
                        rblTat.SelectedValue = "Day";
                    }
                    if (MTBC.P_TatName == "Hrs")
                    {
                        rblTat.SelectedValue = "Hrs";
                    }
                    if (MTBC.P_TatName == "Min")
                    {
                        rblTat.SelectedValue = "Min";
                    }
                    txtTattime.Text = MTBC.P_TatDuration;
                    if (MTBC.Singleformat == "Single Value")
                    {
                        radioSingle.Checked = true;
                        radioFormat.Checked = false;
                        radioSingle_CheckedChanged(this, null);
                        if (MTBC.TextDesc == "TextField")
                        {
                            radioText.Checked = true;
                            radioMemo.Checked = false;

                            radioText_CheckedChanged(this, null);
                        }
                        else
                            if (MTBC.TextDesc == "DescType")
                            {

                                radioMemo.Checked = true;
                                radioText.Checked = false;
                                radioMemo_CheckedChanged(this, null);
                            }
                    }
                    else
                    {
                        radioFormat.Checked = true;
                        radioSingle.Checked = false;
                        radioSingle_CheckedChanged(this, null);

                    }


                    if (MTBC.P_Is_TestActive == true)
                    {
                        chkactive.Checked = true;
                    }
                    else
                    {
                        chkactive.Checked = false;
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
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (txtName.Text.Trim() == "")
        {
            Label1.Visible = true;
            Label1.Text = " Enter Test Name.";
            Label1.ForeColor = Color.Red;
            txtName.Focus();
            return;
        }
        else
        {
            Label1.Visible = false;
        }
        if (txtCode.Text.Trim() == "")
        {
            Label1.Visible = true;
            Label1.Text = "Enter code  ";
            Label1.ForeColor = Color.Red;
            txtCode.Focus();
            return;
        }
        else
        {
            Label1.Visible = false;
        }
        if (txtCode.Text.Length== 4)
        {
            Label1.Visible = true;
            Label1.Text = "Enter code greater than or less than 4 digits";
            Label1.ForeColor = Color.Red;
            txtCode.Focus();
            return;
        }
        else
        {
            Label1.Visible = false;
        }
       
        if (ddlsubdept.SelectedItem.Text == "Select sub dept")
        {
            Label1.Visible = true;
            Label1.Text = "Select sub dept For Test";
            Label1.ForeColor = Color.Red;
            txtName.Focus();
            return;
        }
        else
        {
            Label1.Visible = false;
        }
        try
        {
            if (Convert.ToInt32(ViewState["seflag"]) == 1)
            {
                if (MainTestLog_Bal_C.isMTCodeExists(txtCode.Text.Trim(), Convert.ToInt32(Request.QueryString["Maintestid"].ToString().Trim()), Convert.ToInt32(Session["Branchid"])))
                {
                    Label1.Visible = true;
                    Label1.Text = " Code already exist";
                    txtName.Focus();
                    return;
                }
                else
                {
                    Label1.Visible = false;
                }
                if (ViewState["SDCode"].ToString() != ddlsubdept.SelectedValue.ToString())
                {
                    
                        Label1.Visible = false;
                        MainTestLog_Bal_C.UpdateSDCode(txtCode.Text.Trim(), Convert.ToInt32(Session["Branchid"]), ddlsubdept.SelectedValue.ToString());
                    
                }


                MainTest_Bal_C ObjMTBC = new MainTest_Bal_C(Convert.ToInt32(Request.QueryString["Maintestid"]), Convert.ToInt32(Session["Branchid"]));
                string Maintestname = ObjMTBC.Maintestname;

                ObjMTBC.Maintestname = txtName.Text;
                ObjMTBC.MTCode = txtCode.Text;
                ObjMTBC.SDCode = ddlsubdept.SelectedValue;
                ObjMTBC.Testordno = Convert.ToInt32(txtOrder.Text);

                ObjMTBC.DefaultTestMethod = txtMethod.Text;


                ObjMTBC.SampleType = ddlSampleType.SelectedItem.Text;
                ObjMTBC.Shortcode = txtshortform.Text;

                ViewState["MTCode"] = txtCode.Text;

                if (chkactive.Checked == true)
                {
                    ObjMTBC.P_Is_TestActive = true;
                }
                else
                {
                    ObjMTBC.P_Is_TestActive = false;
                }
                if (radioSingle.Checked == true)
                {
                    ObjMTBC.Singleformat = "Single Value";
                    if (radioMemo.Checked == false)
                    {
                        ObjMTBC.TextDesc = "TextField";
                        ViewState["descriptive"] = "NonDes";
                    }
                    else
                    {
                        ObjMTBC.TextDesc = "DescType";
                        ViewState["descriptive"] = "Des";
                    }
                }
                else
                {
                    ObjMTBC.Singleformat = "Format";
                    ObjMTBC.TextDesc = "TextField";



                }
                if (rblTat.SelectedValue == "Day")
                {
                    ObjMTBC.P_TatName = rblTat.SelectedValue;
                    ObjMTBC.P_TatDuration = txtTattime.Text;
                }
                if (rblTat.SelectedValue == "Hrs")
                {
                    ObjMTBC.P_TatName = rblTat.SelectedValue;
                    ObjMTBC.P_TatDuration = txtTattime.Text;
                }
                if (rblTat.SelectedValue == "Min")
                {
                    ObjMTBC.P_TatName = rblTat.SelectedValue;
                    ObjMTBC.P_TatDuration = txtTattime.Text;
                }
                if (Convert.ToInt32(ViewState["iOrdNo"]) != Convert.ToInt32(txtOrder.Text))
                {
                    MainTestLog_Bal_C.Update_Packageno(ObjMTBC, Convert.ToInt32(ViewState["iOrdNo"]), Convert.ToInt32(Session["Branchid"]));
                }
                if (Convert.ToString(ViewState["oldMTCode"]) != Convert.ToString(txtCode.Text))
                {
                    g = (int)MainTest_Bal_C.Get_TotalTestCount(ViewState["oldMTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                    if (g <= 0)
                    {
                        if (ObjMTBC.Update(Convert.ToInt32(Request.QueryString["Maintestid"]), Convert.ToInt32(Session["Branchid"])))
                        {

                            //============================== end==========================
                            Stformmst_Bal_C SFBC = new Stformmst_Bal_C();
                            SFBC.UpdateTestnameandOrdNo(ViewState["MTCode"].ToString(), ViewState["MTCode"].ToString(), txtName.Text, Convert.ToInt32(txtOrder.Text), ViewState["Maintestname"].ToString(), Convert.ToInt32(Session["Branchid"]));

                            Label1.Visible = true;
                            Label1.Text = "Record Updated";
                            Label1.ForeColor = Color.Blue;


                        }

                        if (ViewState["UpdateSDCode"].ToString() == "true")
                            MainTestLog_Bal_C.updatetablesforSDCode(ddlsubdept.SelectedValue, txtCode.Text, Convert.ToInt32(Session["Branchid"]));
                        if (ViewState["oldMTCode"].ToString() != txtCode.Text)
                        {
                            DataSet testsds = MainTestLog_Bal_C.Get_Testformpatient(ViewState["oldMTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                            for (int i = 0; i < testsds.Tables[0].Rows.Count; i++)
                            {
                                PB_C.Tests = testsds.Tables[0].Rows[i].ItemArray[1].ToString();
                                PB_C.Tests = PB_C.Tests.Replace(ViewState["oldMTCode"].ToString(), txtCode.Text);
                                PB_C.UpdateTest(Convert.ToInt32(testsds.Tables[0].Rows[i].ItemArray[0]), PB_C.Tests, Convert.ToInt32(Session["Branchid"]));
                            }


                            DataSet formds = CalculateSet_Bal_C.getexp(ViewState["oldMTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                            for (int i = 0; i < formds.Tables[0].Rows.Count; i++)
                            {
                                CB_C.Formula = formds.Tables[0].Rows[i].ItemArray[1].ToString();
                                CB_C.Formula = CB_C.Formula.Replace(ViewState["oldMTCode"].ToString(), txtCode.Text);
                                CB_C.updateexp(Convert.ToInt32(formds.Tables[0].Rows[i].ItemArray[0]), CB_C.Formula, Convert.ToInt32(Session["Branchid"]));
                            }
                            //----------------
                            MainTestLog_Bal_C.updatetablesforMTCode(ViewState["oldMTCode"].ToString(), txtCode.Text, Convert.ToInt32(Session["Branchid"]));
                            Label1.Text = "Record Saved Successfully";
                            Label1.Visible = true;
                            Label1.ForeColor = Color.Blue;
                        }
                    }
                    else
                    {
                        Label1.Text = " can't change the Code .";
                        Label1.Visible = true;
                        Label1.ForeColor = Color.Blue;
                    }
                }
                else
                {
                    if (ObjMTBC.Update(Convert.ToInt32(Request.QueryString["Maintestid"]), Convert.ToInt32(Session["Branchid"])))
                    {

                        Stformmst_Bal_C SFBC = new Stformmst_Bal_C();
                        SFBC.UpdateTestnameandOrdNo(ViewState["MTCode"].ToString(), ViewState["MTCode"].ToString(), txtName.Text, Convert.ToInt32(txtOrder.Text), ViewState["Maintestname"].ToString(), Convert.ToInt32(Session["Branchid"]));



                        clear();
                        Label1.Visible = true;
                        Label1.Text = "Record Updated Successfully";
                        Label1.ForeColor = Color.Blue;
                    }
                }



            }
            else
            {
                if (MainTestLog_Bal_C.isMTCodeExists(txtCode.Text.Trim(), Convert.ToInt32(Session["Branchid"])))
                {
                    Label1.Visible = true;
                    Label1.Text = "Test code already exist";
                    Label1.ForeColor = Color.Blue;
                    txtName.Focus();
                    return;
                }
                else
                {
                    Label1.Visible = false;
                }


                MainTest_Bal_C ObjMTBC = new MainTest_Bal_C();
                ObjMTBC.Maintestname = txtName.Text;
                ObjMTBC.MTCode = txtCode.Text;
                ObjMTBC.SDCode = ddlsubdept.SelectedValue;
                ObjMTBC.Testordno = Convert.ToInt32(txtOrder.Text);

                ObjMTBC.DefaultTestMethod = txtMethod.Text;

                ObjMTBC.Shortform = txtshortform.Text;

                ObjMTBC.SampleType = ddlSampleType.SelectedItem.Text;

                ObjMTBC.P_username = Convert.ToString(Session["username"]);

                ViewState["MTCode"] = txtCode.Text;

                if (chkactive.Checked == true)
                {
                    ObjMTBC.P_Is_TestActive = true;
                }
                else
                {
                    ObjMTBC.P_Is_TestActive = true;
                }

                if (rblTat.SelectedValue == "Day")
                {
                    ObjMTBC.P_TatName = rblTat.SelectedValue;
                    ObjMTBC.P_TatDuration = txtTattime.Text;
                }
                if (rblTat.SelectedValue == "Hrs")
                {
                    ObjMTBC.P_TatName = rblTat.SelectedValue;
                    ObjMTBC.P_TatDuration = txtTattime.Text;
                }
                if (rblTat.SelectedValue == "Min")
                {
                    ObjMTBC.P_TatName = rblTat.SelectedValue;
                    ObjMTBC.P_TatDuration = txtTattime.Text;
                }
                if (radioSingle.Checked == true)
                {
                    ObjMTBC.Singleformat = "Single Value";
                    if (radioMemo.Checked == false)
                    {
                        ObjMTBC.TextDesc = "TextField";
                        ViewState["descriptive"] = "NonDes";
                    }
                    else
                    {
                        ObjMTBC.TextDesc = "DescType";
                        ViewState["descriptive"] = "Des";
                    }
                }
                else
                {
                    ObjMTBC.Singleformat = "Format";
                    ObjMTBC.TextDesc = "TextField";

                }
                if (ObjMTBC.Insert(Convert.ToInt32(Session["Branchid"])))
                {
                    Label1.Visible = true;
                    clear();
                    Label1.Text = "Record is Saved";
                    Label1.ForeColor = Color.Blue;
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

    public void BiochemInsert()
    {
        try
        {
            MainTest_Bal_C ObjMTBC = new MainTest_Bal_C();
            ObjMTBC.MTCode = txtCode.Text;

            ObjMTBC.Maintestname = "";


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

    protected void radioSingle_CheckedChanged(object sender, EventArgs e)
    {
        if (radioSingle.Checked == true)
        {
            radioMemo.Visible = true;
           // radioText.Visible = true;

            radioFormat.Checked = false;
        }
        else
        {
            radioMemo.Visible = false;
            radioText.Visible = false;


        }
    }
    protected void radioFormat_CheckedChanged(object sender, EventArgs e)
    {
        if (radioFormat.Checked == true)
        {
            radioSingle.Checked = false;
            radioMemo.Visible = false;
            radioText.Visible = false;


        }
        else
        {
            radioMemo.Visible = true;
            radioText.Visible = true;
        }
    }


    protected void radioText_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            TestHelp_Bal_C Teshel = new TestHelp_Bal_C();
            Teshel.P_Testcode = txtCode.Text;
            Teshel.P_tlcode = "";
            Teshel.P_branchid = Convert.ToInt32(Session["Branchid"]);
            radioMemo.Checked = false;

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

    protected void radioMemo_CheckedChanged(object sender, EventArgs e)
    {

        radioText.Checked = false;
    }



    protected void Btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowTest.aspx?SDCode=" + ddlsubdept.SelectedValue);
    }

    protected void ddlsubdept_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void ddlsubdept_SelectedIndexChanged1(object sender, EventArgs e)
    {
        if (ddlsubdept.SelectedIndex != Convert.ToInt32(ViewState["headingindex"]))
            ViewState["UpdateSDCode"] = "true";
        else
            ViewState["UpdateSDCode"] = "false";

        int fnum = MainTest_Bal_C.MaxOrder(ddlsubdept.SelectedValue, Convert.ToInt32(Session["Branchid"]));
        int tordno = fnum + 1;
        txtOrder.Text = tordno.ToString();

    }

    protected void txtCode_TextChanged(object sender, EventArgs e)
    {
        
    }
    public void clear()
    {
        txtCode.Text = "";
        txtName.Text = "";
        txtRate.Text = "";
        txtOrder.Text = "";
        txtMethod.Text = "";

    }

    protected void btntestformat_Click(object sender, EventArgs e)
    {
        string AA = "Page under construction!! ";
        string[] urlArr = new string[1];
        string[] targetArr = new string[1];
        string[] featuresArr = new string[1];
        targetArr[0] = "1";
        featuresArr[0] = "";
        urlArr[0] = "AddFutureFormat.aspx?STCode=" + txtCode.Text + "&Branchid=" + Session["Branchid"].ToString() + " ";
        ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
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