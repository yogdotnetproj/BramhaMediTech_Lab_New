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
using System.Web.Services;
using System.Web.Script.Services;
using System.Net;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class EditPatientTest : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    Patmst_New_Bal_C PNBC = new Patmst_New_Bal_C();
    DateTime fromDate = Date.getMinDate(), toDate = Date.getMinDate();
    string[] patient = new string[] { "", "" };
    string PateintName = "", RegNo = "", CenterName = "", labcode_main = "", Barcode = "", Mno = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.SetFocus(txtCentername);
        if (!IsPostBack)
        {
            try
            {
                LUNAME.Text = Convert.ToString(Session["username"]);
                LblDCName.Text = Convert.ToString(Session["Bannername"]);
                LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                this.PopulateTreeView(dt, 0, null);
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
            try
            {

                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                if (Session["usertype"].ToString() == "CollectionCenter")
                {
                    string CenterCode = Session["CenterCode"].ToString();
                    txtCentername.Text = Patmst_Bal_C.getname(CenterCode, Convert.ToInt32(Session["Branchid"]));
                }
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
    }
    private void PopulateTreeView(DataTable dtparent, int Parentid, TreeNode treeNode)
    {
        foreach (DataRow row in dtparent.Rows)
        {
            TreeNode child = new TreeNode
            {
                Text = row["MenuName"].ToString(),
                Value = row["MenuID"].ToString()

            };
            if (Parentid == 0)
            {
                TrMenu.Nodes.Add(child);
                DataTable dtchild = new DataTable();
                dtchild = ObjTB.BindChildMenu(child.Value, Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                PopulateTreeView(dtchild, int.Parse(child.Value), child);

            }
            else
            {
                treeNode.ChildNodes.Add(child);
            }

        }
    }


    protected void TrMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        int tId = Convert.ToInt32(TrMenu.SelectedValue);
        DataTable dtform = new DataTable();
        dtform = ObjTB.BindForm(tId);
        if (dtform.Rows.Count > 0)
        {
            Response.Redirect(dtform.Rows[0]["SubMenuNavigateURL"].ToString());
        }
    }
    void BindGrid()
    {
        try
        {
            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
            if (labcode != null && labcode != "")
            {
                labcode_main = labcode;
            }
            if (txtCentername.Text.Trim() != "")
            {
                CenterName = txtCentername.Text.Trim();
            }
            if ((fromdate.Text.Trim() != "" && todate.Text.Trim() != "") || txtRegNo.Text.Trim() != "")
            {
                fromDate = DateTimeConvesion.getDateFromString(fromdate.Text);
                toDate = DateTimeConvesion.getDateFromString(todate.Text);

                #region
                SqlConnection con = DataAccess.ConInitForDC();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandText = "ALTER VIEW [dbo].[VW_patmstvwdt]AS (SELECT  SrNo, PID, PatRegID, FID, Patregdate, intial, Patname,  sex, Age, MDY, RefDr,  Tests, " +
                          "  PF, Reportdate,  Phrecdate,  " +
                          "  flag, Patphoneno,  Pataddress,  Isemergency,  Branchid, DoctorCode,  CenterCode, " +
                          "  FinancialYearID, EmailID, Drname,TestCharges,   SampleID, CenterName, Username, " +
                          "  Usertype,SampleType, SampleStatus, Remark,   PatientcHistory, RegistratonDateTime,  " +
                          "  TelNo, Email,Patusername, Patpassword,  PPID, testname, UnitCode,IsActive FROM dbo.patmst where Phrecdate between ('" + Convert.ToDateTime(fromDate.ToString()).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(toDate.ToString()).ToString("MM/dd/yyyy") + "') or PatRegID='" + txtRegNo.Text.Trim().ToString() + "')";

                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close(); con.Dispose();


                #endregion
            }
            if (txtPatientName.Text.Trim() != "")
            {
                if (txtPatientName.Text.ToLower().Equals("all"))
                {
                    PateintName = "";
                }
                else
                {
                    //patient = txtPatientName.Text.Split(' ');
                    //string name = patient[1];
                    //PateintName = name.Trim();
                    PateintName = txtPatientName.Text.Trim();
                }

            }
            if (txtRegNo.Text.Trim() != "")
            {
                RegNo = txtRegNo.Text.Trim();
            }

            Barcode = "";

            if (txtmobileno.Text.Trim() != "")
            {
                Mno = txtmobileno.Text.Trim();
            }
            else
            {
                Mno = "";
            }
            Patmst_New_Bal_C.P_maindeptid = Convert.ToInt32(Session["DigModule"]);

            GridEditTest.DataSource = PNBC.Get_patmst_Edittest(PateintName, RegNo, Convert.ToDateTime(fromdate.Text), Convert.ToDateTime(todate.Text), Convert.ToInt32(Session["Branchid"]), CenterName, labcode_main, "", Barcode, Mno);
            GridEditTest.DataBind();
            //patientcnt.Text = Convert.ToString(GridEditTest.Rows.Count);

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
                //Server.Transfer("~/ErrorMessage.aspx");
            }
        }
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void GridEditTest_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridEditTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string regno = GridEditTest.Rows[GridEditTest.SelectedIndex].Cells[1].Text;
            string FID = (GridEditTest.Rows[GridEditTest.SelectedIndex].FindControl("hdnFID") as HiddenField).Value;
            Patmst_Bal_C ci = new Patmst_Bal_C(regno, FID, Convert.ToInt32(Session["Branchid"]));
            int PID = Convert.ToInt32(GridEditTest.DataKeys[GridEditTest.SelectedIndex].Value);
            Response.Redirect("EditPatientDetails.aspx?PID=" + PID + "&Center=" + ci.CenterCode + "&FType=Edit&PatRegID=" + regno + "&FID=" + FID + "&sdt=" + fromdate.Text + "&edt=" + todate.Text + "&Patname=EditTest");
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
    protected void GridEditTest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridEditTest.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] GetCenterName(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixText + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixText + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["DoctorName"], i);
            i++;
        }

        return tests;
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] FillPateintName(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT distinct rtrim(intial)+' '+Patname as Patname FROM patmst where Patname like  N'%" + prefixText + "%'and branchid=" + branchid + " order by Patname", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT rtrim(intial)+' '+Patname as Patname FROM patmst where Patname like  N'%" + prefixText + "%' and branchid=" + branchid + " order by Patname", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["Patname"], i);
            i++;
        }
        return tests;
    }


    protected void txtCentername_TextChanged(object sender, EventArgs e)
    {
        string CCode = DrMT_sign_Bal_C.Get_C_Code(txtCentername.Text.Trim(), Convert.ToInt32(Session["Branchid"]));
        Session["CenterCode"] = CCode;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Home.aspx", false);
    }
}