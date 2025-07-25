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

public partial class DemographicEdit : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    Patmst_New_Bal_C contact = new Patmst_New_Bal_C();
    DateTime fromDate = Date.getMinDate(), toDate = Date.getMinDate();
    string[] patient = new string[] { "", "" };
    string PateintName = "", RegNo = "", CenterName = "", labcode_main = "", Barcode = "", Mno = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        //Page.SetFocus(txtCentername);
        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("DemographicEdit.aspx");
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
            try
            {

                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                if (Session["usertype"].ToString() == "CollectionCenter")
                {
                    string CenterCode = Session["CenterCode"].ToString();
                    txtCentername.Text = Patmst_Bal_C.getname(CenterCode, Convert.ToInt32(Session["Branchid"]));
                    txtCentername.Enabled = false;
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
                cmd1.CommandText = "ALTER VIEW [dbo].[VW_patmstvwdt]AS (SELECT        patmst.SrNo, patmst.PID, patmst.PatRegID, patmst.FID, patmst.Patregdate, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.MDY, patmst.RefDr, cast(patmst.Tests as nvarchar(4000)) as Tests, patmst.PF, patmst.Reportdate, patmst.Phrecdate,  "+
                                  "  patmst.flag, patmst.Patphoneno, patmst.Pataddress, patmst.Isemergency, patmst.Branchid, patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.EmailID, patmst.Drname,  "+
                                  "  patmst.TestCharges + sum(ISNULL(RecM.OtherCharges, 0)) AS TestCharges, patmst.SampleID, patmst.CenterName, patmst.Username, patmst.Usertype, patmst.SampleType, patmst.SampleStatus, patmst.Remark,  "+
                                  "  cast(patmst.PatientcHistory as nvarchar(4000)) as PatientcHistory , patmst.RegistratonDateTime, patmst.TelNo, patmst.Email, patmst.Patusername, patmst.Patpassword, patmst.PPID, "+
                                  "  cast(patmst.testname  as nvarchar(4000)) as testname, patmst.UnitCode, patmst.IsActive "+
                                  "  FROM            patmst INNER JOIN "+
                                  "  RecM ON patmst.PID = RecM.PID AND patmst.Branchid = RecM.branchid   where Phrecdate between ('" + Convert.ToDateTime(fromDate.ToString()).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(toDate.ToString()).ToString("MM/dd/yyyy") + "') or patmst.PatRegID='" + txtRegNo.Text.Trim().ToString() + "'  group by patmst.SrNo, patmst.PID, patmst.PatRegID, patmst.FID, patmst.Patregdate, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.MDY, patmst.RefDr, "+
                                  "  cast(patmst.Tests as nvarchar(4000)), "+
                                  "   patmst.PF, patmst.Reportdate, patmst.Phrecdate,  "+
                                  "  patmst.flag, patmst.Patphoneno, patmst.Pataddress, patmst.Isemergency, patmst.Branchid, patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.EmailID, patmst.Drname,  "+
                                  "  patmst.TestCharges , patmst.SampleID, patmst.CenterName, patmst.Username, patmst.Usertype, patmst.SampleType, patmst.SampleStatus, patmst.Remark, "+
                                  "  cast(patmst.PatientcHistory as nvarchar(4000)), "+
                                  "   patmst.RegistratonDateTime, patmst.TelNo, patmst.Email, patmst.Patusername, patmst.Patpassword, patmst.PPID, "+
                                  "  cast(patmst.testname  as nvarchar(4000)), "+
                                  "  patmst.UnitCode, patmst.IsActive )";

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
                   // PateintName = txtPatientName.Text.Trim();
                    PateintName = txtPatientName.Text.Trim().Replace("'", "'+char(39)+'"); ;
                   // string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
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

            GridEditTest.DataSource = contact.Get_patmst_Edittest(PateintName, RegNo, Convert.ToDateTime(fromdate.Text), Convert.ToDateTime(todate.Text), Convert.ToInt32(Session["Branchid"]), CenterName, labcode_main, "", Barcode, Mno);
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
            string Patregid = GridEditTest.Rows[GridEditTest.SelectedIndex].Cells[1].Text;
            string FID = (GridEditTest.Rows[GridEditTest.SelectedIndex].FindControl("hdnFID") as HiddenField).Value;
            Patmst_Bal_C Patmst_Bal = new Patmst_Bal_C(Patregid, FID, Convert.ToInt32(Session["Branchid"]));
            int PID = Convert.ToInt32(GridEditTest.DataKeys[GridEditTest.SelectedIndex].Value);
           // Response.Redirect("EditClickSample.aspx?PID=" + PID + "&Center=" + Patmst_Bal.CenterCode + "&FType=Edit&PatRegID=" + Patregid + "&FID=" + FID + "");
            Response.Redirect("showDemographic.aspx?PID=" + PID + "&Center=" + Patmst_Bal.CenterCode + "&FType=Edit&PatRegID=" + Patregid + "&FID=" + FID + "",false);

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
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixTextNew + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixTextNew + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
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
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT distinct rtrim(intial)+' '+Patname as Patname FROM patmst where Patname like  N'%" + prefixTextNew + "%'and branchid=" + branchid + " order by Patname", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT rtrim(intial)+' '+Patname as Patname FROM patmst where Patname like  N'%" + prefixTextNew + "%' and branchid=" + branchid + " order by Patname", con);
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