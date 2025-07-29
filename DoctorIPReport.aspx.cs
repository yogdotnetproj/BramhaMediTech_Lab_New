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
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;

public partial class DoctorIPReport :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string CenterName = "", PatRegID = "", Centercode = "", labcode_main = "", Doctorcode = "";
    DateTime stDate = Date.getMinDate(), endDate = Date.getMinDate();
    Patmst_New_Bal_C contact = new Patmst_New_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("DoctorIPReport.aspx");
                }
                if (Session["usertype"].ToString() == "CollectionCenter")
                {

                    txtCenter.Enabled = false;
                    string Centercode = Session["CenterCode"].ToString();
                    string collectionname = Patmst_Bal_C.getname(Centercode, Convert.ToInt32(Session["Branchid"]));
                    txtCenter.Text = collectionname;

                }
                else
                {
                    txtCenter.Text = DrMT_sign_Bal_C.GetCenterwithName(Convert.ToString(Session["UnitCode"] ), Convert.ToInt32(Session["Branchid"]));
                    Session["CenterCode"] = DrMT_sign_Bal_C.Get_CenterDefault(Convert.ToString(Session["UnitCode"] ), Convert.ToInt32(Session["Branchid"]));


                }

               // Label10.Text = Session["CenterName"].ToString();
                ddlfyear.DataSource = FinancialYearTableLogic.getFinancialYearsList_New(Convert.ToInt32(Session["Branchid"]));
                ddlfyear.DataTextField = "Yearname";
                ddlfyear.DataValueField = "FinancialYearId";
                ddlfyear.DataBind();
                ddlfyear.SelectedValue = Session["financialyear"].ToString().Trim();
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
                fromdate.Text = DateTime.Now.ToShortDateString();
                todate.Text = DateTime.Now.ToShortDateString();
                txtCenter.Text = "All";
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
            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
            if (labcode != null && labcode != "")
            {
                this.labcode_main = labcode;
            }
            if (Session["usertype"].ToString() == "CollectionCenter")
            {

                Centercode = Session["CenterCode"].ToString();

            }
            if (txtCenter.Text != "")
            {
                CenterName = txtCenter.Text;
            }
            if (txtregno.Text != "")
            {
                PatRegID = txtregno.Text;

            }
            else if (fromdate.Text != "" && todate.Text != "")
            {
                stDate = DateTimeConvesion.getDateFromString(fromdate.Text);
                endDate = DateTimeConvesion.getDateFromString(todate.Text);
            }
            if (Convert.ToString(ViewState["Doctorcode"]) != "")
            {
                Doctorcode = Convert.ToString(ViewState["Doctorcode"]);
            }
            GridView1.DataSource = contact.GetPatientforBillForIPH(CenterName, stDate, endDate, PatRegID, Convert.ToInt32(Session["Branchid"]), 0, Centercode, ddlfyear.SelectedValue.PadLeft(2, '0') + "-", this.labcode_main, Doctorcode);
            GridView1.DataBind();
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
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
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
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + prefixText + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + prefixText + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["DoctorName"], i);
            i++;
        }
        return tests;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {

        }
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] FillDoctor(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();

        string Centercode = HttpContext.Current.Session["CenterCode"].ToString();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
            sda = new SqlDataAdapter("SELECT Doctorcode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR' and DoctorName like '" + prefixText + "%' ", con);
        else
            sda = new SqlDataAdapter("SELECT Doctorcode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR'and DoctorName like '" + prefixText + "%' ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["name"] + " = " + dr["Doctorcode"], i);
            i++;
        }

        return tests;
    }
    protected void txtDoctorName_TextChanged(object sender, EventArgs e)
    {
        if (txtDoctorName.Text != "")
        {
            string[] Doc_code;
            Doc_code = txtDoctorName.Text.Split('=');
            if (Doc_code.Length > 1)
            {
                txtDoctorName.Text = Doc_code[0].ToString();
                ViewState["Doctorcode"] = Doc_code[1].ToString();
            }

        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
       // BindGrid();
        string query = "";

        string sql = "";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        query = "ALTER VIEW [dbo].[VW_drdatavw] AS (SELECT     TOP (100) PERCENT patmst.PID, patmst.PatRegID, patmst.FID, patmst.Patregdate, patmst.intial,   " +
             "   patmst.intial +' '+patmst.Patname as Patname,  patmst.sex, patmst.Age, patmst.MDY, patmst.RefDr,   " +
             "   patmst.Tests, patmst.Phrecdate, patmst.Branchid, patmst.Doctorcode,    " +
             "   patmst.CenterCode,  patmst.FinancialYearID, patmst.EmailID, patmst.Drname,   " +
             "   patmst.CenterName, patmst.Username, patmst.Usertype,  patmst.PPID,   " +
             "   patmst.testname, Cshmst.AmtPaid, Cshmst.Discount, patmst.UnitCode,   " +
             "   (CASE WHEN ((dbo.Cshmst.AmtReceived IS NULL) OR   " +
             "   (patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END) >= dbo.Cshmst.AmtReceived))   " +
             "   THEN patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END) ELSE dbo.Cshmst.AmtReceived END)   " +
             "   AS TestCharges, (CASE WHEN disflag = 0 THEN (((((CASE WHEN (dbo.Cshmst.AmtReceived IS NULL OR  " +
             "   patmst.TestCharges >= (CASE WHEN AmtReceived IS NULL THEN 0 ELSE AmtReceived END))   " +
             "   THEN (patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END)) ELSE (CASE WHEN AmtReceived IS NULL  " +
             "   THEN 0 ELSE AmtReceived END) END)) - (CASE WHEN amtpaid IS NULL THEN 0 ELSE amtpaid END)) - (((CASE WHEN (dbo.Cshmst.AmtReceived IS NULL OR  " +
             "   patmst.TestCharges >= (CASE WHEN AmtReceived IS NULL THEN 0 ELSE AmtReceived END))   " +
             "   THEN (patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END)) ELSE (CASE WHEN AmtReceived IS NULL  " +
             "   THEN 0 ELSE AmtReceived END) END)) * discount / 100))) ELSE ((((CASE WHEN (dbo.Cshmst.AmtReceived IS NULL OR  " +
             "   patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END) >= (CASE WHEN AmtReceived IS NULL   " +
             "   THEN 0 ELSE AmtReceived END)) THEN (patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END))  " +
             "   ELSE (CASE WHEN AmtReceived IS NULL THEN 0 ELSE AmtReceived END) END)) - (CASE WHEN amtpaid IS NULL THEN 0 ELSE amtpaid END))   " +
             "   - (CASE WHEN discount IS NULL THEN 0 ELSE discount END)) END) AS balance, patmst.IsbillBH, ISNULL(Cshmst.BillNo, 0) AS BillNo,   " +
             "   patmst.Patphoneno, patmst.IsActive  " +
             "   FROM         patmst LEFT OUTER JOIN  " +
             "   Cshmst ON patmst.PID = Cshmst.PID " +
             " where   patmst.PatRegID<>'' and patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " and IsbillBH=1 and (CASE WHEN disflag = 0 THEN (((((CASE WHEN (dbo.Cshmst.AmtReceived IS NULL OR  " +
             "   patmst.TestCharges >= (CASE WHEN AmtReceived IS NULL THEN 0 ELSE AmtReceived END))   " +
             "   THEN (patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END))  " +
             "   ELSE (CASE WHEN AmtReceived IS NULL   " +
             "   THEN 0 ELSE AmtReceived END) END)) - (CASE WHEN amtpaid IS NULL THEN 0 ELSE amtpaid END)) -  " +
             "   (((CASE WHEN (dbo.Cshmst.AmtReceived IS NULL OR  " +
             "   patmst.TestCharges >= (CASE WHEN AmtReceived IS NULL THEN 0 ELSE AmtReceived END))  " +
             "   THEN (patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END))  " +
             "   ELSE (CASE WHEN AmtReceived IS NULL   " +
             "   THEN 0 ELSE AmtReceived END) END)) * discount / 100))) ELSE ((((CASE WHEN (dbo.Cshmst.AmtReceived  " +
             "   IS NULL OR   " +
             "   patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END) >=  " +
             "   (CASE WHEN AmtReceived IS NULL    " +
             "   THEN 0 ELSE AmtReceived END)) THEN (patmst.TestCharges +  " +
             "   (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END))    " +
             "   ELSE (CASE WHEN AmtReceived IS NULL THEN 0 ELSE AmtReceived END) END)) -  " +
             "   (CASE WHEN amtpaid IS NULL THEN 0 ELSE amtpaid END))    " +
             "   - (CASE WHEN discount IS NULL THEN 0 ELSE discount END)) END)>0";
        if (txtregno.Text != "")
        {
            PatRegID = txtregno.Text;
            query += " and patmst.PatRegID='" + PatRegID + "'";
        }

        if (Convert.ToString(ViewState["Doctorcode"]) != "")
        {
            Doctorcode = Convert.ToString(ViewState["Doctorcode"]);
            query += " and patmst.Doctorcode='" + Doctorcode.Trim() + "'";
        }
        if (fromdate.Text != "" && todate.Text != "")
        {
            query += "   and patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }



        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        ReportParameterClass.ReportType = "DoctorIPReport";//  
        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_DoctorIPReport.rpt");
        Session["reportname"] = "DoctorIPReport";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = "";
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
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