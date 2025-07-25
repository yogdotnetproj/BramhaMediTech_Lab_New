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

public partial class Collcode_Patient : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string Center = "", Patregid = "", CenterCode = "", labcode_main = "", DocCode = "";
    DateTime stDate = Date.getMinDate(), endDate = Date.getMinDate();
    Patmst_New_Bal_C PNBC = new Patmst_New_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
       
      
        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("Collcode_Patient.aspx");
                    }
                }
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
                Session["CenterCode"] = DrMT_sign_Bal_C.Get_CenterDefault(Convert.ToString(Session["UnitCode"] ), Convert.ToInt32(Session["Branchid"]));

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
            int paidamt = 0;
            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
            if (labcode != null && labcode != "")
            {
                this.labcode_main = labcode;
            }
            if (Session["usertype"].ToString() == "CollectionCenter")
            {

                CenterCode = Session["CenterCode"].ToString();

            }
            if (txtCenter.Text != "")
            {
                Center = txtCenter.Text;
            }
            if (txtregno.Text != "")
            {
                Patregid = txtregno.Text;

            }
            else if (fromdate.Text != "" && todate.Text != "")
            {
                stDate = DateTimeConvesion.getDateFromString(fromdate.Text);
                endDate = DateTimeConvesion.getDateFromString(todate.Text);


            }
            if (Convert.ToString(ViewState["DocCode"]) != "")
            {
                DocCode = Convert.ToString(ViewState["DocCode"]);
            }

            GVBillFHosp.DataSource = PNBC.GetPatientforBillCollcode(Center, stDate, endDate, Patregid, Convert.ToInt32(Session["Branchid"]), 0, CenterCode, ddlfyear.SelectedValue.PadLeft(2, '0') + "-", this.labcode_main, DocCode, 0);
            GVBillFHosp.DataBind();
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

    protected void GVBillFHosp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVBillFHosp.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] Getcenter(string SearchDate, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + SearchDate + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + SearchDate + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
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
  protected void GVBillFHosp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {

        }
    }




    protected void btnreport_Click(object sender, EventArgs e)
    {
        string sql = "";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[VW_bth] AS (SELECT * from VW_csmst1vw " +
        " where IsbillBH=1  and Monthlybill=0  and RegNo<>'' and branchid=" + Convert.ToInt32(Session["Branchid"]) + "";

        if (Convert.ToString(ViewState["DocCode"]) != "")
        {
            query += " and DocCode='" + Convert.ToString(ViewState["DocCode"]).Trim() + "'";
        }
       
        if (fromdate.Text != "" && todate.Text != "")
        {

            query += " and Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }



        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_Billforhosp.rpt");
        Session["reportname"] = "Billforhospital";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
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