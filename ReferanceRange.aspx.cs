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
using System.Web.Services;
using System.Web.Script.Services;
using System.Net;
using System.IO;
using System.Web.Security;
public partial class ReferanceRange :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!Page.IsPostBack)
        {

            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    //checkexistpageright("ReferanceRange.aspx");
                }
                // FillDept();
                Label1.Visible = false;
                FillMaintest();
                BindOutLabName();
                // ddlParametercode.Visible = false;
                ViewState["EditFlag"] = "Save";
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
    public void BindOutLabName()
    {
        Patmst_New_Bal_C PatNB = new Patmst_New_Bal_C();
        dt = new DataTable();
        dt = PatNB.GetoutsourceLab(Convert.ToInt32(Session["Branchid"]));
        if (dt.Rows.Count > 0)
        {
            ddlOutLabNAme.DataSource = dt;
            ddlOutLabNAme.DataTextField = "OutsourceLabName";
            ddlOutLabNAme.DataValueField = "Id";
            ddlOutLabNAme.DataBind();
            ddlOutLabNAme.Items.Insert(0, "-Out Lab-");
            ddlOutLabNAme.SelectedIndex = 0;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string test = ddlParametercode.SelectedValue.ToString();
            int upper = 0;
            int lower = 0;
            //if (txtmaintestcode.Text != "")
            //{

            if (ddlYear.SelectedItem.Text == "Days")
            {
                try
                {
                    lower = Convert.ToInt32(txtLower.Text);
                    lblErrorlwr.Visible = false;
                }
                catch
                {
                    lblErrorlwr.Visible = true;
                    lblErrorlwr.Text = " enter numeric value";
                    return;
                }
                try
                {
                    upper = Convert.ToInt32(txtUpper.Text);
                    lblErrorupper.Visible = false;
                }
                catch
                {
                    lblErrorupper.Visible = true;
                    lblErrorupper.Text = " enter numeric value";
                    return;
                }

            }
            else
                if (ddlYear.SelectedItem.Text == "Month")
                {

                    try
                    {
                        lower = (Convert.ToInt32(txtLower.Text)) * 30;
                        lblErrorlwr.Visible = false;
                    }
                    catch
                    {
                        lblErrorlwr.Visible = true;
                        lblErrorlwr.Text = " enter numeric value";
                        return;
                    }
                    try
                    {
                        upper = (Convert.ToInt32(txtUpper.Text)) * 30;
                        lblErrorupper.Visible = false;
                    }
                    catch
                    {
                        lblErrorupper.Visible = true;
                        lblErrorupper.Text = " enter numeric value";
                        return;
                    }
                }
                else
                    if (ddlYear.SelectedItem.Text == "Year")
                    {

                        try
                        {
                            lower = (Convert.ToInt32(txtLower.Text)) * 365;
                            lblErrorlwr.Visible = false;
                        }
                        catch
                        {
                            lblErrorlwr.Visible = true;
                            lblErrorlwr.Text = " enter numeric value";
                            return;
                        }
                        try
                        {
                            upper = (Convert.ToInt32(txtUpper.Text)) * 365;
                            lblErrorupper.Visible = false;
                        }
                        catch
                        {
                            lblErrorupper.Visible = true;
                            lblErrorupper.Text = " enter numeric value";
                            return;
                        }

                    }
            // }
            if (ViewState["EditFlag"].ToString() == "Edit")
            {
                Refrangemst_Bal_C Obj_RBC = new Refrangemst_Bal_C();
                if (ddlsex.SelectedItem.Text == "Both")
                {
                    for (int i = 0; i < 2; i++)
                    {
                       // Obj_RBC.MTCode = ddMaintest.SelectedValue.ToString();
                        Obj_RBC.MTCode = Convert.ToString(ViewState["MTCode"] );
                        Obj_RBC.Unit = txtUnit.Text;
                        //Obj_RBC.TestName = ddMaintest.SelectedItem.Text;
                        Obj_RBC.TestName = Convert.ToString(ViewState["TestName"] );
                       // Obj_RBC.Sex = ddlsex.SelectedItem.Text;
                        if (i == 0)
                        {
                            Obj_RBC.Sex = "Male";
                        }
                        else
                        {
                            Obj_RBC.Sex = "Female";
                        }
                        Obj_RBC.DescretiveRange = txtNormalRange.Text;
                        Obj_RBC.GreaterThanDays = upper;
                        Obj_RBC.LessThanDays = lower;
                        Obj_RBC.STCODE = ddlParametercode.SelectedValue;
                        Obj_RBC.LowerRange = txtLowerRange.Text;
                        Obj_RBC.UpperRange = txtUpperRange.Text;
                        Obj_RBC.PanicLowerRange = txtPanicLowerRange.Text;
                        Obj_RBC.PanicUpperRange = txtPanicUpperRange.Text;
                        int nid = Convert.ToInt32(ViewState["norid"]);
                        Obj_RBC.P_username = Convert.ToString(Session["username"]);

                        Obj_RBC.Patregdate = Date.getOnlydate();
                        if (ddlOutLabNAme.SelectedIndex == 0)
                        {
                            Obj_RBC.P_OutLabName = "0";
                        }
                        else
                        {
                            Obj_RBC.P_OutLabName = ddlOutLabNAme.SelectedValue;
                        }
                        Obj_RBC.Update(nid, Convert.ToInt32(Session["Branchid"]));
                    }
                }
                else
                {
                   // Obj_RBC.MTCode = ddMaintest.SelectedValue.ToString();
                    Obj_RBC.MTCode = Convert.ToString(ViewState["MTCode"]);
                    Obj_RBC.Unit = txtUnit.Text;
                   // Obj_RBC.TestName = ddMaintest.SelectedItem.Text;
                    Obj_RBC.TestName = Convert.ToString(ViewState["TestName"]);
                    Obj_RBC.Sex = ddlsex.SelectedItem.Text;

                    Obj_RBC.DescretiveRange = txtNormalRange.Text;
                    Obj_RBC.GreaterThanDays = upper;
                    Obj_RBC.LessThanDays = lower;
                    Obj_RBC.STCODE = ddlParametercode.SelectedValue;
                    Obj_RBC.LowerRange = txtLowerRange.Text;
                    Obj_RBC.UpperRange = txtUpperRange.Text;
                    Obj_RBC.PanicLowerRange = txtPanicLowerRange.Text;
                    Obj_RBC.PanicUpperRange = txtPanicUpperRange.Text;
                    int nid = Convert.ToInt32(ViewState["norid"]);
                    Obj_RBC.P_username = Convert.ToString(Session["username"]);

                    Obj_RBC.Patregdate = Date.getOnlydate();
                    if (ddlOutLabNAme.SelectedIndex == 0)
                    {
                        Obj_RBC.P_OutLabName = "0";
                    }
                    else
                    {
                        Obj_RBC.P_OutLabName = ddlOutLabNAme.SelectedValue;
                    }
                    Obj_RBC.Update(nid, Convert.ToInt32(Session["Branchid"]));
                }
                Label1.Visible = true;
                Label1.Text = "Record Updated Successfully...";
                ViewState["norid"] = null;
            }
            else
            {
                if (GVtestnormalvaluegrid.Columns[3].Visible == false)
                {
                    if (RefrangemstNew_Bal_C.IS_Exist_refrangemst(upper, lower, ddlsex.SelectedItem.Text, ddMaintest.SelectedValue.ToString(), Convert.ToInt32(Session["Branchid"])))
                    {
                        Label1.Visible = true;
                        Label1.Text = "ObjRBC Range  already exist";
                        ddMaintest.Focus();
                        return;
                    }
                    else
                    {
                        Label1.Visible = false;
                    }
                }
                else
                {
                    if (RefrangemstNew_Bal_C.IS_refrangemst_Parameter(upper, lower, ddlsex.SelectedItem.Text, ddMaintest.SelectedValue.ToString(), ddlParametercode.SelectedValue.ToString(), Convert.ToInt32(Session["Branchid"])))
                    {
                        Label1.Visible = true;
                        Label1.Text = "Normal Range  already exist";
                        ddlParametercode.Focus();
                        return;
                    }
                    else
                    {
                        Label1.Visible = false;
                    }
                }
                Refrangemst_Bal_C ObjRBC = new Refrangemst_Bal_C();
                if (ddlsex.SelectedItem.Text == "Both")
                {
                    for (int i = 0; i < 2; i++)
                    {
                       // if (ddMaintest.SelectedValue.ToString() == "")
                         if(txttests.Text=="")
                        {
                            Label1.Visible = true;
                            Label1.Text = " select TestName";
                            return;
                        }
                        else
                        {
                            Label1.Visible = false;
                            Label1.Text = "";
                        }
                        //ObjRBC.MTCode = ddMaintest.SelectedValue.ToString();
                        ObjRBC.MTCode = Convert.ToString(ViewState["MTCode"]);
                        ObjRBC.Unit = txtUnit.Text;
                       // ObjRBC.TestName = ddMaintest.SelectedItem.Text;
                        ObjRBC.TestName = Convert.ToString(ViewState["TestName"]);
                        if (i == 0)
                        {
                            ObjRBC.Sex = "Male";
                        }
                        else
                        {
                            ObjRBC.Sex ="Female" ;
                        }
                        
                        ObjRBC.DescretiveRange = txtNormalRange.Text;
                        ObjRBC.GreaterThanDays = upper;
                        ObjRBC.LessThanDays = lower;
                        ObjRBC.STCODE = ddlParametercode.SelectedValue;
                        ObjRBC.LowerRange = txtLowerRange.Text;
                        ObjRBC.UpperRange = txtUpperRange.Text;
                        ObjRBC.PanicLowerRange = txtPanicLowerRange.Text;
                        ObjRBC.PanicUpperRange = txtPanicUpperRange.Text;
                        ObjRBC.P_username = Convert.ToString(Session["username"]);
                        if (ddlOutLabNAme.SelectedIndex == 0)
                        {
                            ObjRBC.P_OutLabName = "0";
                        }
                        else
                        {
                            ObjRBC.P_OutLabName = ddlOutLabNAme.SelectedValue;
                        }
                        ObjRBC.Insert(Convert.ToInt32(Session["Branchid"]));
                    }
                }
                else
                {

                   // if (ddMaintest.SelectedValue.ToString() == "")
                    if(txttests.Text=="")
                    {
                        Label1.Visible = true;
                        Label1.Text = " select TestName";
                        return;
                    }
                    else
                    {
                        Label1.Visible = false;
                        Label1.Text = "";
                    }
                   // ObjRBC.MTCode = ddMaintest.SelectedValue.ToString();
                    ObjRBC.MTCode = Convert.ToString(ViewState["MTCode"]);
                    ObjRBC.Unit = txtUnit.Text;
                   // ObjRBC.TestName = ddMaintest.SelectedItem.Text;
                    ObjRBC.TestName = Convert.ToString(ViewState["TestName"]);
                    ObjRBC.Sex = ddlsex.SelectedItem.Text;
                    ObjRBC.DescretiveRange = txtNormalRange.Text;
                    ObjRBC.GreaterThanDays = upper;
                    ObjRBC.LessThanDays = lower;
                    ObjRBC.STCODE = ddlParametercode.SelectedValue;
                    ObjRBC.LowerRange = txtLowerRange.Text;
                    ObjRBC.UpperRange = txtUpperRange.Text;
                    ObjRBC.PanicLowerRange = txtPanicLowerRange.Text;
                    ObjRBC.PanicUpperRange = txtPanicUpperRange.Text;
                    ObjRBC.P_username = Convert.ToString(Session["username"]);
                    if (ddlOutLabNAme.SelectedIndex == 0)
                    {
                        ObjRBC.P_OutLabName = "0";
                    }
                    else
                    {
                        ObjRBC.P_OutLabName = ddlOutLabNAme.SelectedValue;
                    }
                    ObjRBC.Insert(Convert.ToInt32(Session["Branchid"]));
                }
                Label1.Visible = true;
                Label1.Text = "Record Save Successfully.";

            }
            BindGrid();
            Clearcontrols();
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
    void BindGrid()
    {
        try
        {
            string SingFormat = MainTestLog_Bal_C.GetSingleFormat(Convert.ToString(ViewState["MTCode"]), Convert.ToInt32(Session["Branchid"]));//;ddMaintest.SelectedValue.ToString()

            if (SingFormat == "Single Value")
            {                                                          //Get_ALLDetailsForrange_Code
                GVtestnormalvaluegrid.DataSource = RefrangemstNew_Bal_C.Get_ALLDetailsForCode11(Convert.ToString(ViewState["MTCode"]), Convert.ToInt32(Session["Branchid"]));//ddMaintest.SelectedValue.ToString()
            }
            else
            {
                GVtestnormalvaluegrid.Columns[3].Visible = true;
                if (ddlParametercode.SelectedValue != "")
                {
                    GVtestnormalvaluegrid.DataSource = RefrangemstNew_Bal_C.Get_ALLDetailsForCode_New(ddlParametercode.SelectedValue, Convert.ToString(ViewState["MTCode"]), Convert.ToInt32(Session["Branchid"]));//ddMaintest.SelectedValue.ToString()
                }
                else
                {
                    GVtestnormalvaluegrid.DataSource = RefrangemstNew_Bal_C.Get_ALLDetailsForCode(Convert.ToString(ViewState["MTCode"]), Convert.ToInt32(Session["Branchid"]));//ddMaintest.SelectedValue.ToString()
                }
            }
            GVtestnormalvaluegrid.DataBind();
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
    void Clearcontrols()
    {

        txtNormalRange.Text = "";
        txtLower.Text = "";
        txtUnit.Text = "";
        txtUpper.Text = "";
        txtLowerRange.Text = "";
        txtUpperRange.Text = "";
        txtPanicLowerRange.Text = "";
        txtPanicUpperRange.Text = "";
    }
    private void fillTestCode()
    {
        try
        {
            ddlParametercode.DataSource = SubTestLog_Bal_C.Get_AllSubTest(Convert.ToString(ViewState["MTCode"]), Convert.ToInt32(Session["Branchid"]));// ddMaintest.SelectedValue.ToString()
            ddlParametercode.DataTextField = "TestName";
            ddlParametercode.DataValueField = "STCODE";

            ddlParametercode.DataBind();
            ddlParametercode.Items.Insert(0, "");
            ddlParametercode.Items[0].Value = "";
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
    protected void ddMaintest_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddMaintest_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            //txtmaintestcode.Text = ddMaintest.SelectedValue.ToString();

            string SingFormat = MainTestLog_Bal_C.GetSingleFormat(Convert.ToString(ViewState["MTCode"]), Convert.ToInt32(Session["Branchid"]));// ddMaintest.SelectedValue.ToString()
            BindGrid();
            if (SingFormat == "Single Value")
            {

                ddlParametercode.Visible = false;
                GVtestnormalvaluegrid.Columns[3].Visible = false;

            }
            else
            {

                ddlParametercode.Visible = true;
                GVtestnormalvaluegrid.Columns[3].Visible = true;
                fillTestCode();
            }
            txtUpper.Focus();
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
    protected void GVtestnormalvaluegrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {

            ViewState["EditFlag"] = "Edit";

            ViewState["norid"] = (GVtestnormalvaluegrid.Rows[e.NewEditIndex].FindControl("hdnnormalid") as HiddenField).Value;
            Refrangemst_Bal_C OBJRB = new Refrangemst_Bal_C(Convert.ToInt32(ViewState["norid"]), Convert.ToInt32(Session["Branchid"]));
           // ddMaintest.SelectedValue = OBJRB.MTCode;
            ViewState["MTCode"] = OBJRB.MTCode;
            //  txtmaintestcode.Text = norm.MTCode;
            ddMaintest.Enabled = false;
            txttests.Width = 200;
            txttests.Enabled = false;
            if (OBJRB.Sex == "Male" || OBJRB.Sex == "M")
                ddlsex.SelectedValue = "Male";
            else
                ddlsex.SelectedValue = "Female";
            txtUnit.Text = OBJRB.Unit;
            txtNormalRange.Text = OBJRB.DescretiveRange;
            try
            {
                ddlParametercode.SelectedValue = OBJRB.STCODE;
            }
            catch (Exception exx)
            { }
            ddlParametercode.Enabled = false;
            ddlYear.SelectedValue = "Days";

            txtLower.Text = OBJRB.LessThanDays.ToString();
            txtUpper.MaxLength = 6;
            txtUpper.Text = OBJRB.GreaterThanDays.ToString();
            txtLowerRange.Text = OBJRB.LowerRange;
            txtUpperRange.Text = OBJRB.UpperRange;
            if (OBJRB.P_OutLabName != "0")
            {
                ddlOutLabNAme.SelectedValue = OBJRB.P_OutLabName;
            }
            e.Cancel = true;
            //BindGrid();
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
    protected void GVtestnormalvaluegrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void GVtestnormalvaluegrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {


    }
    protected void GVtestnormalvaluegrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            Refrangemst_Bal_C OBJRB = new Refrangemst_Bal_C();
            int normid = Convert.ToInt32((GVtestnormalvaluegrid.Rows[e.RowIndex].FindControl("hdnnormalid") as HiddenField).Value);
            OBJRB.Delete(normid, Convert.ToInt32(Session["Branchid"]));
            BindGrid();
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
            ddMaintest.Enabled = true;

            ddlParametercode.Enabled = true;
            Label1.Visible = false;
            FillMaintest();
            this.ddMaintest_SelectedIndexChanged(null, null);
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
    private void FillMaintest()
    {
        try
        {
            ddMaintest.DataSource = MainTestLog_Bal_C.GetMaintest_SDCode("", Convert.ToInt32(Session["Branchid"]));
            ddMaintest.DataTextField = "Maintestname";
            ddMaintest.DataValueField = "MTCode";
            ddMaintest.Items.Insert(0, "Select Maintestname");
            ddMaintest.DataBind();
            ddMaintest.SelectedIndex = -1;
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
    protected void txtNormalRange_TextChanged(object sender, EventArgs e)
    {
        if (txtNormalRange.Text != "")
        {
            txtLowerRange.Enabled = false;
            txtUpperRange.Enabled = false;
            txtNormalRange.Enabled = true;
        }
        else
        {
            txtNormalRange.Enabled = false;
            txtLowerRange.Enabled = true;
            txtUpperRange.Enabled = true;
        }
    }
    protected void txtLowerRange_TextChanged(object sender, EventArgs e)
    {
        if (txtLowerRange.Text != "" && txtUpperRange.Text != "")
        {
            txtNormalRange.Enabled = false;
            txtLowerRange.Enabled = true;
            txtUpperRange.Enabled = true;

        }
        if (txtLowerRange.Text == "" && txtUpperRange.Text == "")
        {
            txtLowerRange.Enabled = false;
            txtUpperRange.Enabled = false;
            txtNormalRange.Enabled = true;
        }
        if (txtLowerRange.Text != "")
        {
            funAllowLowerRange(txtLowerRange.Text);
        }
        if (txtUpperRange.Text != "")
        {
            funAllowUpperRange(txtUpperRange.Text);
        }
        txtUpperRange.Focus();

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

                        Label13.Visible = false;
                        ok.Add("ok");

                    }

                }
            }
            if (ok.Count == str.Length)
            {

                Label13.Visible = false;
            }
            else
            {
                Label13.Visible = true;
                Label13.Text = "Plz Enter Valid Data.";
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

                        Label14.Visible = false;
                        ok.Add("ok");

                    }

                }
            }
            if (ok.Count == str.Length)
            {

                Label14.Visible = false;
            }
            else
            {
                Label14.Visible = true;
                Label14.Text = "Plz Enter Valid Data.";
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
    protected void GVtestnormalvaluegrid_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {

            GVtestnormalvaluegrid.EditIndex = -1;
            BindGrid();
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
    protected void GVtestnormalvaluegrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVtestnormalvaluegrid.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void ddlParametercode_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();


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

    protected void txttests_TextChanged(object sender, EventArgs e)
    {
        if (txttests.Text != "")
        {
            string[] SplitTestCode;
            string[] TempsplitTestcode;
            TempsplitTestcode = txttests.Text.Split('-');
            ViewState["MTCode"] = TempsplitTestcode[0];
            ViewState["TestName"] = TempsplitTestcode[1];

            try
            {
                //txtmaintestcode.Text = ddMaintest.SelectedValue.ToString();

                string SingFormat = MainTestLog_Bal_C.GetSingleFormat(Convert.ToString(ViewState["MTCode"]), Convert.ToInt32(Session["Branchid"]));// ddMaintest.SelectedValue.ToString()
                BindGrid();
                if (SingFormat == "Single Value")
                {

                    ddlParametercode.Visible = false;
                    GVtestnormalvaluegrid.Columns[3].Visible = false;

                }
                else
                {

                    ddlParametercode.Visible = true;
                    GVtestnormalvaluegrid.Columns[3].Visible = true;
                    fillTestCode();
                }
                txtUpper.Focus();
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
     [WebMethod]
    [ScriptMethod]
    public static string[] FillTests(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
       
            sda = new SqlDataAdapter(" select MTCode, Maintestname  from MainTest WHERE ISTestActive=1 and (MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%') order by Maintestname ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["MTCode"] + " - " + dr["Maintestname"] , i);
            i++;
        }

        return tests;
    }
   
}