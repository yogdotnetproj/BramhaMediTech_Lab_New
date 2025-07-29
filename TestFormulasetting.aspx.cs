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
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections;
using System.Data.SqlClient;

public partial class TestFormulasetting :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    Subdepartment_Bal_C hnn = null;
    MainTest_Bal_C tnt = null;
    SubTest_Bal_C fild = null;
    string code = "";
    public int level;
    bool fcparam = false;
    int k = 0;
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            try
            { 
                int i, j;
                Label5.Visible = false;

                ViewState["code"] = "";
                ViewState["firstclick"] = "true";
                ViewState["testput"] = "false";
                ViewState["param"] = "false";
                ViewState["paramput"] = "false";
                btnPlus.Attributes.Add("onmouseover", "plusfocus()");
                btnPower.Attributes.Add("onmouseover", "powerfocus()");
                btnSave.Attributes.Add("onmouseover", "savefocus()");
                //btnshowformula.Attributes.Add("onmouseover", "sffocus()");
                btnDivide.Attributes.Add("onmouseover", "divfocus()");
                btnMinus.Attributes.Add("onmouseover", "minusfocus()");
                btnMultiply.Attributes.Add("onmouseover", "mulfocus()");
                btnOpenbracket.Attributes.Add("onmouseover", "obfocus()");
                btnCloseBracket.Attributes.Add("onmouseover", "cbfocus()");
                btnClear.Attributes.Add("onmouseover", "clrfocus()");
                btncheck.Attributes.Add("onmouseover", "chkfocus()");
                Gridbind();
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
            ViewState["count"] = 0;


        }
    }

   
    private bool Flag_check = false;
    private void cmdCheck_Click(object sender, EventArgs e)
    {
        try
        {
            Label5.Visible = false;
            check_Foemula();
            if (Flag_check)
            {
                Flag_check = true;

            }
            else
            {
                return;
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

    public string NodeName;
    public string NodeCodes = "";
    TreeNode tn_curr = null;
    public bool FirstSecondFlag = true;
    string f = "", s = "";
    string stests = "";


    public void check_Foemula()
    {
        try
        {
            int opening = 0, closing = 0, Mathcheck = 0;

            if (txtFirstHalf.Text == "" || rtSecondHalf.Text == "")
            {
                Flag_check = false;
                return;
            }
            if (rtSecondHalf.Text.Trim().Contains("-") && rtSecondHalf.Text.Trim().Contains("*") || rtSecondHalf.Text.Trim().Contains("-") && rtSecondHalf.Text.Trim().Contains("+") || rtSecondHalf.Text.Trim().Contains("-") && rtSecondHalf.Text.Trim().Contains("^"))
            {

                Flag_check = true;

            }

            for (int i = 0; i < rtSecondHalf.Text.Length; i++)
            {
                if (rtSecondHalf.Text.Substring(i, 1) == "(")
                {
                    opening++;
                }

                if (rtSecondHalf.Text.Substring(i, 1) == ")")
                {
                    closing++;
                }
            }
            //chcking last occurenc of string
            string[] mathopt = new string[5] { "+", "-", "*", "/", "^" };
            for (int i = 0; i < 5; i++)
            {
                if (rtSecondHalf.Text.Substring(rtSecondHalf.Text.Length - 1, 1) == mathopt[i])
                {
                    Mathcheck = 1;
                }
            }

            if (opening != closing || Mathcheck == 1)
            {
                Flag_check = false;
            }
            else
            {
                Flag_check = true;
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
        Label5.Visible = false;
        check_Foemula();

        if (Flag_check)
        {
            //Insert Record
            try
            {
                CalculateSet_Bal_C ft = new CalculateSet_Bal_C();

                int le = Convert.ToInt32(ViewState["level"]);
                ft.STCODE = ViewState["TesCode"].ToString();
                string hcd = ViewState["TesCode"].ToString();
                string hc = hcd.Substring(0, 2);
                ft.SDCode = hc;


                ft.Formula = ViewState["expression"].ToString();
                ft.Formula = "(" + ft.Formula + ")";
                ft.P_username = Convert.ToString(Session["username"]);

                ft.Insert(Convert.ToInt32(Session["Branchid"])); //insert record

                rtSecondHalf.Text = "";
                txtFirstHalf.Text = "";
                Label5.Text = "Record Saved successfully";
                Label5.Visible = true;

                chkparamlst.Items.Clear();

                ViewState["expression"] = "";
                ViewState["testput"] = "false";
                ViewState["param"] = "false";
                ViewState["paramput"] = "false";
                ViewState["code"] = "";
                // Response.Redirect("Testformulashow.aspx", false);
                // return;
                Gridbind();
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
        else
        {

        }

    }
    protected void btnPlus_Click(object sender, EventArgs e)
    {

        ViewState["expression"] += "+";
        rtSecondHalf.Text = rtSecondHalf.Text + '+';
    }
    protected void btnMinus_Click(object sender, EventArgs e)
    {

        ViewState["expression"] += "-";
        rtSecondHalf.Text = rtSecondHalf.Text + '-';
    }
    protected void btnDivide_Click(object sender, EventArgs e)
    {

        ViewState["expression"] += "/";
        rtSecondHalf.Text = rtSecondHalf.Text + '/';
    }
    protected void btnOpenbracket_Click(object sender, EventArgs e)
    {

        ViewState["expression"] += "{";
        rtSecondHalf.Text = rtSecondHalf.Text + '{';
    }
    protected void btnMultiply_Click(object sender, EventArgs e)
    {

        ViewState["expression"] += "*";
        rtSecondHalf.Text = rtSecondHalf.Text + '*';
    }
    protected void btnPower_Click(object sender, EventArgs e)
    {
        ViewState["expression"] += "^";
        rtSecondHalf.Text = rtSecondHalf.Text + '^';
    }
    protected void btnCloseBracket_Click(object sender, EventArgs e)
    {

        ViewState["expression"] += "}";
        rtSecondHalf.Text = rtSecondHalf.Text + '}';
    }
    protected void treeViewHeadinAndTitles_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {

        if (Convert.ToInt32(ViewState["count"]) > 0)
        {
            string STCODE = e.Node.Value;
            string expression = "{" + STCODE + "}";
            ViewState["expression"] += expression;
        }
        int count = Convert.ToInt32(ViewState["count"]);
        ViewState["count"] = ++count;


        try
        {
            if (e.Node.Checked)
            {
                if (txtFirstHalf.Text == "")
                {
                    if (TestFormulaLogic_Bal_c.isFormulaExists(e.Node.Value, Session["Branchid"]))
                    {
                        Label5.Visible = true;
                        Label5.Text = "Record already exists";
                        return;
                    }
                    Label5.Visible = false;
                    txtFirstHalf.Text = "{" + e.Node.Text + "}";
                    level = e.Node.Depth;
                    ViewState["TesCode"] = e.Node.Value;
                    tn_curr = e.Node;
                }
                else
                {
                    rtSecondHalf.Text = rtSecondHalf.Text + "{" + e.Node.Text + "}";
                    if (ViewState["code"] == null)
                        code = '@' + e.Node.Value;
                    else
                        code = ViewState["code"].ToString() + '@' + e.Node.Value;

                }
                ViewState["code"] = code;
            }
            else
            {
                rtSecondHalf.Text = rtSecondHalf.Text.ToString().Replace("{" + e.Node.Text + "}", "");
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
    protected void btnClear_Click(object sender, EventArgs e)
    {
        Label5.Visible = false;
        rtSecondHalf.Text = "";
        txtFirstHalf.Text = "";
        Label5.Text = " select sub department ";
        Label5.Visible = true;

        chkparamlst.Items.Clear();

    }
    protected void btncheck_Click(object sender, EventArgs e)
    {
        try
        {
            Label5.Visible = false;
            check_Foemula();

            if (Flag_check)
            {
                Flag_check = true;
            }
            else
            {
                return;
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

    protected void treeViewHeadinAndTitles_SelectedNodeChanged(object sender, EventArgs e)
    {

    }

    public void filltextboxes1()
    {
        bool flag = false;
        try
        {

            if (ViewState["param"].ToString() == "false")
            {
                if (txtFirstHalf.Text == "")
                {
                    if (TestFormulaLogic_Bal_c.isFormulaExists(Convert.ToString(ViewState["MTCode"]), Session["Branchid"]))
                    {
                        Label5.Visible = true;
                        Label5.Text = "Record already exist";

                        return;
                    }
                    Label5.Visible = false;
                    txtFirstHalf.Text = "{" + ViewState["TestName"] + "}";

                    int count = Convert.ToInt32(ViewState["count"]);
                    ViewState["count"] = ++count;
                    ViewState["TesCode"] = Convert.ToString(ViewState["MTCode"]);

                    ViewState["testput"] = "true";


                }
                else
                {

                    stests = ViewState["selectedtests"].ToString();
                    string[] selectedtests = stests.Split('#');

                    for (int j = 0; j < selectedtests.Length; j++)
                    {
                        if ("$" + ViewState["TestName"] + "$" == selectedtests[j].ToString())
                        {
                            flag = true;
                        }
                    }
                    if (flag != true)
                    {
                        if ("{" + txtFirstHalf.Text + "}" != ViewState["TestName"])
                            rtSecondHalf.Text = rtSecondHalf.Text + "{" + ViewState["TestName"] + "}";

                        if (Convert.ToInt32(ViewState["count"]) > 0)
                        {
                            string STCODE = Convert.ToString(ViewState["MTCode"]);
                            string expression = "{" + STCODE + "}";
                            ViewState["expression"] += expression;
                        }

                    }

                }

                flag = false;
            }
            //if (testschkboxlst.Items[i].Selected == true)
            //{
            bool tsel = false;
            stests = ViewState["selectedtests"].ToString();
            string[] selectedtests1 = stests.Split('#');
            for (int j = 0; j < selectedtests1.Length; j++)
            {
                if ("$" + ViewState["TestName"] + "$" == selectedtests1[j].ToString())
                {
                    tsel = true;
                }
            }
            if (tsel == false)
            {

                ViewState["selectedtests"] += "$" + ViewState["TestName"] + "$" + "#";
                if (ViewState["code"].ToString() == "")
                    ViewState["code"] = '@' + Convert.ToString(ViewState["MTCode"]);
                else
                    ViewState["code"] += '@' + Convert.ToString(ViewState["MTCode"]);
            }

            ViewState["param"] = "false";


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
    public void filltextboxeswhenparam()
    {
        bool flag = false;

        try
        {
            for (int i = 0; i < chkparamlst.Items.Count; i++)
            {
                if (chkparamlst.Items[i].Selected == true)
                {
                    if (ViewState["firstclick"].ToString() == "true" && ViewState["testput"].ToString() == "false" && txtFirstHalf.Text == "")
                    {
                        if (TestFormulaLogic_Bal_c.isFormulaExists(chkparamlst.Items[i].Value, Session["Branchid"]))
                        {
                            Label5.Visible = true;
                            Label5.Text = "Rocord already existsS";
                            chkparamlst.Items[i].Selected = false;
                            return;
                        }
                        Label5.Visible = false;
                        txtFirstHalf.Text = "{" + chkparamlst.Items[i].Text + "}";

                        int count = Convert.ToInt32(ViewState["count"]);
                        ViewState["count"] = ++count;
                        ViewState["TesCode"] = chkparamlst.Items[i].Value;

                        ViewState["firstclick"] = "false";
                        ViewState["param"] = "false";
                        fcparam = true;
                        ViewState["paramput"] = "true";

                    }
                    else
                    {

                        stests = ViewState["selectedtests"].ToString();
                        string[] selectedtests = stests.Split('#');

                        for (int j = 0; j < selectedtests.Length; j++)
                        {
                            if ("$" + chkparamlst.Items[i].Text + "$" == selectedtests[j].ToString())
                            {
                                flag = true;
                            }
                        }
                        if (flag != true)
                        {
                            if (txtFirstHalf.Text != chkparamlst.Items[i].Text)
                                rtSecondHalf.Text = rtSecondHalf.Text + "{" + chkparamlst.Items[i].Text + "}";

                            if (Convert.ToInt32(ViewState["count"]) > 0)
                            {
                                string STCODE = chkparamlst.Items[i].Value;
                                string expression = "{" + STCODE + "}";
                                ViewState["expression"] += expression;
                            }

                            ViewState["param"] = "false";

                        }

                    }

                    flag = false;
                }
                if (chkparamlst.Items[i].Selected == true)
                {
                    bool psel = false;
                    stests = ViewState["selectedtests"].ToString();
                    string[] selectedtests = stests.Split('#');

                    for (int j = 0; j < selectedtests.Length; j++)
                    {
                        if ("$" + chkparamlst.Items[i].Text + "$" == selectedtests[j].ToString())
                        {
                            psel = true;
                        }
                    }
                    if (psel == false)
                    {
                        ViewState["selectedtests"] += "$" + chkparamlst.Items[i].Text + "$" + "#";
                        if (ViewState["code"].ToString() == "")
                            ViewState["code"] = '@' + chkparamlst.Items[i].Value;
                        else
                            ViewState["code"] += '@' + chkparamlst.Items[i].Value;
                    }
                }
                else
                {
                    if (fcparam == true)
                    {
                        ViewState["firstclick"] = "true";
                        fcparam = false;
                    }

                    if (rtSecondHalf.Text == "")
                        txtFirstHalf.Text = txtFirstHalf.Text.ToString().Replace("{" + chkparamlst.Items[i].Text + "}", "");
                    if (txtFirstHalf.Text == "")
                    {
                        ViewState["testput"] = "false";
                        ViewState["paramput"] = "false";
                    }

                    bool replace = false;
                    string[] codes = ViewState["code"].ToString().Split('@');
                    for (int k = 0; k < codes.Length; k++)
                    {
                        if (chkparamlst.Items[i].Value == codes[k].ToString())
                        {
                            replace = true;
                            break;
                        }
                    }
                    if (replace == true)
                    {
                        rtSecondHalf.Text = rtSecondHalf.Text.ToString().Replace("{" + chkparamlst.Items[i].Text + "}", "");
                        ViewState["code"] = ViewState["code"].ToString().Replace("@" + chkparamlst.Items[i].Value, "");
                        ViewState["expression"] = ViewState["expression"].ToString().Replace("{" + chkparamlst.Items[i].Value + "}", "");
                        ViewState["selectedtests"] = ViewState["selectedtests"].ToString().Replace("$" + chkparamlst.Items[i].Text + "$" + "#", "");
                    }

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
    protected void chkparamlst_SelectedIndexChanged(object sender, EventArgs e)
    {

        filltextboxeswhenparam();
    }
    protected void rtSecondHalf_TextChanged(object sender, EventArgs e)
    {
        string Exp = rtSecondHalf.Text;
        string expfinal = "";
        string expfinalold = "";
        string tname = "";
        int i = 0, j = 0, k = 0;
        bool flag = false;
        while (i < Exp.Length)
        {
            if (Convert.ToInt32(Exp[i]) >= 97 && Convert.ToInt32(Exp[i]) <= 123 || Convert.ToInt32(Exp[i]) >= 65 && Convert.ToInt32(Exp[i]) <= 91)
            {
                if (Exp[i] != '}' && Exp[i] != '{')
                {
                    while (Exp[i] != '}')
                    {

                        tname += Exp[i].ToString();
                        i++;
                    }

                    string STCODE = SubTestLog_Bal_C.GetTestcodeforsubtest(tname, Convert.ToInt32(Session["Branchid"]), Convert.ToString(ViewState["SDCode"]), Convert.ToString(ViewState["MTCode"]));
                    if (STCODE != "")
                    {
                        Exp = Exp.Replace(tname, STCODE);
                        int li = Exp.LastIndexOf(STCODE);
                        i = li + 5;
                        tname = "";
                    }
                }
            }

            i++;

        }
        ViewState["expression"] = Exp;

    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTests(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
            sda = new SqlDataAdapter(" select MTCode, Maintestname from MainTest WHERE (MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%') and SDCode in (select SDCode from SubDepartment where DigModule='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "') order by Maintestname ", con);
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

    protected void txttests_TextChanged(object sender, EventArgs e)
    {
        string[] splittedtlcode;
        splittedtlcode = txttests.Text.Split('-');

        ViewState["MTCode"] = splittedtlcode[0];
        ViewState["TestName"] = splittedtlcode[1];
        ViewState["selectedtests"] = "";
        chkparamlst.DataSource = MainTestLog_Bal_C.getparamnames(splittedtlcode[0], Convert.ToInt32(Session["Branchid"]));
        chkparamlst.DataValueField = "STCODE";
        chkparamlst.DataTextField = "testname";
        chkparamlst.DataBind();
        string[] codes = ViewState["code"].ToString().Split('@');
        foreach (ListItem li in chkparamlst.Items)
        {
            for (k = 0; k < codes.Length; k++)
            {
                if (li.Value == codes[k].ToString())
                    li.Selected = true;
            }
        }



        if (chkparamlst.Items.Count > 0)
            ViewState["param"] = "true";
        filltextboxes1();
    }
    public void Gridbind()
    {
        Label1.Visible = false;
        GridView1.AutoGenerateColumns = false;
        GridView1.DataSource = TestFormulaLogic_Bal_c.getAllFormulaTbl1TableValues(Session["Branchid"], Convert.ToInt32(Session["DigModule"]));
        GridView1.DataBind();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string ID = Convert.ToString(GridView1.Rows[e.RowIndex].Cells[3].Text);
            int id = (int)GridView1.DataKeys[e.RowIndex].Value;
            CalculateSet_Bal_C ft = new CalculateSet_Bal_C();
            ft.ID = id;

            ft.Delete(Convert.ToInt32(Session["Branchid"]));
            GridView1.DataSource = TestFormulaLogic_Bal_c.getAllFormulaTbl1TableValues(Session["Branchid"], Convert.ToInt32(Session["DigModule"]));
            GridView1.DataBind();
            Label1.Visible = true;
            Label1.Text = "Record deleted successfully";
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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataSource = TestFormulaLogic_Bal_c.getAllFormulaTbl1TableValues(Session["Branchid"], Convert.ToInt32(Session["DigModule"]));
        GridView1.DataBind();
    }
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
}