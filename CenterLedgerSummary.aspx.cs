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

public partial class CenterLedgerSummary :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    string labcode_main = "";
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
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
                        checkexistpageright("CenterLedgerSummary.aspx");
                    }
                }

              
                FillddlCenter();
                alterviewcenterwise_summary_Amount();
                alterviewcenterwise_summary_Receive();
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                if (Session["usertype"] != null)
                {
                    if (Session["usertype"].ToString() == "CollectionCenter")
                    {
                        createuserTable_Bal_C CTB = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        ddlCenter.SelectedValue = (CTB.CenterCode.Trim());
                        ddlCenter.Enabled = false;
                    }
                    else
                    {
                        ddlCenter.Enabled = true;

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

     
    private void FillddlCenter()
    {
        try
        {
            ddlCenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            ddlCenter.DataTextField = "Name";
            ddlCenter.DataValueField = "DoctorCode";
            ddlCenter.DataBind();
            ddlCenter.Items.Insert(0, "Select " + Convert.ToString(Session["CenterName"]));
            ddlCenter.SelectedIndex = 1;
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

    public void alterviewcenterwise_summary_Amount()
    {
        string sql = "";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        string query = "";
        if (ddlCenter.SelectedItem.Text != "Select ")
        {
            query = "ALTER VIEW [dbo].[VW_centerwise_summary_Amount] AS (SELECT     1 AS Received, SUM(TestCharges) AS DebitAmt, CenterCode FROM         VW_Getcentersummary_report  " +
                "     where   PatRegID<>''  and centername= '" + ddlCenter.SelectedItem.Text + "' and  Monthlybill=1     " + //
               
                "   group by Centercode  " ;

            


            cmd1.CommandText = query + ")";
        }
        else
        {
            query = "ALTER VIEW [dbo].[VW_centerwise_summary_Amount] AS (SELECT     1 AS Received, SUM(TestCharges) AS DebitAmt, CenterCode FROM         VW_Getcentersummary_report  " +
                "     where  PatRegID<>''  and  Monthlybill=1    " + //Monthlybill=1  and

                "   group by Centercode  ";

               


            cmd1.CommandText = query + ")";
        }

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
    }

    public void alterviewcenterwise_summary_Receive()
    {
        string sql = "";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        string query = "";
        if (ddlCenter.SelectedItem.Text != "Select ")
        {
            query = "ALTER VIEW [dbo].[VW_centerwise_summary_ReceivAmt] AS (SELECT     1 AS Received, SUM(Receiveamount) AS CreditAmt, Centercode FROM         CPReceive  " +
                "     where   Centercode= '" + ddlCenter.SelectedItem.Text + "'    " + //
                "   group by Centercode  ";
            cmd1.CommandText = query + ")";
        }
        else
        {
            query = "ALTER VIEW [dbo].[VW_centerwise_summary_ReceivAmt] AS (SELECT     1 AS Received, SUM(Receiveamount) AS CreditAmt, Centercode FROM         CPReceive  " +
             
                "   group by Centercode  ";

            cmd1.CommandText = query + ")";
        }

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
    }

    protected void btnList_Click1(object sender, EventArgs e)
    {
        alterviewcenterwise_summary_Amount();
        alterviewcenterwise_summary_Receive();
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[VW_Get_Ledgersummary_Report] AS (SELECT     VW_centerwise_summary_Amount.Received, VW_centerwise_summary_Amount.DebitAmt, VW_centerwise_summary_ReceivAmt.CreditAmt, "+
                      "  VW_centerwise_summary_Amount.DebitAmt - VW_centerwise_summary_ReceivAmt.CreditAmt AS PendingAmount, "+
                      "  VW_centerwise_summary_Amount.CenterCode "+
                      "  FROM         VW_centerwise_summary_Amount INNER JOIN "+
                      "  VW_centerwise_summary_ReceivAmt ON VW_centerwise_summary_Amount.Received = VW_centerwise_summary_ReceivAmt.Received ";
     
        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        
        ReportParameterClass.SelectionFormula = "";

        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_LedgerSummary.rpt");
        Session["reportname"] = "Rpt_LedgerSummary";
        Session["RPTFORMAT"] = "pdf";

        // ReportParameterClass.SelectionFormula = sql;
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