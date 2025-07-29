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

public partial class Collectioncenter_Paymentreceive :BasePage
{
    CollCenterPaymentreceive_Bal_C ObjCPB = new CollCenterPaymentreceive_Bal_C();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    object fromDate = null;
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
                        checkexistpageright("Collectioncenter_Paymentreceive.aspx");
                    }
                }
                txtentrydate.Text = Date.getdate().ToString("dd/MM/yyyy");
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
                TxtCenter.Text = "All";
                Session["CenterCode"] = DrMT_sign_Bal_C.Get_CenterDefault(Convert.ToString(Session["UnitCode"] ), Convert.ToInt32(Session["Branchid"]));

                Lbltotalbillamount.Text = CollCenterPaymentreceive_Bal_C.Get_Center_Amount(TxtCenter.Text, Convert.ToInt32(Session["Branchid"]));
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
    public static string[] FillCenter(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like N'" + prefixText + "%' and DrType='CC' and unitcode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like N'" + prefixText + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
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
    protected void btnshow_Click(object sender, EventArgs e)
    {
        if (TxtCenter.Text == "All")
        {
            LblMsg.Text = "Select collection center";
        }
        else
        {
            if (txtdatecur.Text != "")
            {
                fromDate = txtdatecur.Text.Trim();
                SqlConnection con = DataAccess.ConInitForDC();
                SqlCommand cmd1 = con.CreateCommand();

                string query = "ALTER VIEW [dbo].[VW_CenteramountDatewise] AS (SELECT     SUM((CASE WHEN ((dbo.RecM.AmtPaid IS NULL) OR  "+
                           " (patmst.TestCharges + (CASE WHEN (Othercharges) IS NULL THEN 0 ELSE (Othercharges)  END) >=  (dbo.RecM.AmtPaid)))   THEN patmst.TestCharges + "+
                           " (CASE WHEN (Othercharges) IS NULL THEN 0 ELSE (Othercharges) END)  ELSE patmst.TestCharges END)) AS TestCharges ,patmst.Patregdate  "+
                           " FROM         patmst LEFT OUTER JOIN    RecM ON patmst.PID = RecM.PID  "+
                           " GROUP BY patmst.FID, patmst.centercode, patmst.FinancialYearID, patmst.CenterName ,patmst.Monthlybill,patmst.Patregdate  ";

              
                if (TxtCenter.Text != "All")
                {
                    query += " having patmst.CenterName='" + TxtCenter.Text.Trim() + "'  and  (CAST(CAST(YEAR(patmst.Patregdate) AS varchar(4)) + '/' + CAST(MONTH(patmst.Patregdate) AS varchar(2)) + '/' + CAST(DAY(patmst.Patregdate) AS varchar(2)) AS datetime)) <= '" + Convert.ToDateTime(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
                }

                cmd1.CommandText = query + ")";

                con.Open();
                cmd1.ExecuteNonQuery();
                con.Close(); con.Dispose();
                Lbltotalbillamount.Text = CollCenterPaymentreceive_Bal_C.Get_Center_Amount_Date(TxtCenter.Text, Convert.ToInt32(Session["Branchid"]), fromDate);//'" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "'

                Lblpaidamount.Text = CollCenterPaymentreceive_Bal_C.getpaidamountcollamount_datewise(TxtCenter.Text, Convert.ToInt32(ddlfyear.SelectedValue), fromDate);
        
            }
            else
            {
                Lbltotalbillamount.Text = CollCenterPaymentreceive_Bal_C.Get_Center_Amount(TxtCenter.Text, Convert.ToInt32(Session["Branchid"]));
            
            Lblpaidamount.Text = CollCenterPaymentreceive_Bal_C.getpaidamountcollamount(TxtCenter.Text, Convert.ToInt32(ddlfyear.SelectedValue));
            }
            if (Lblpaidamount.Text == "")
            {
                Lblpaidamount.Text = "0";
            }
            if (Lbltotalbillamount.Text == "")
            {
                Lbltotalbillamount.Text = "0";
            }

            txtbalanceamount.Text = Convert.ToString(Convert.ToSingle(Lbltotalbillamount.Text) - Convert.ToSingle(Lblpaidamount.Text));
            txtrecamount.Text = "0";
            txtremark.Text = "";
            txtdiscount.Text = "0";

        }


    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        if (TxtCenter.Text == "All")
        {
            LblMsg.Text = "Select collection center";
        }
        else
        {
            SqlConnection con = DataAccess.ConInitForDC();
            SqlCommand cmd1 = con.CreateCommand();

            string query = "ALTER VIEW [dbo].[VW_CenterPAymentReceive] AS (SELECT DISTINCT * from CPReceive " +
            " where CPReceive.branchid=" + Convert.ToInt32(Session["Branchid"]) + "  and dbo.CPReceive.Fid = '" + ddlfyear.SelectedValue + "'";
           
            if (TxtCenter.Text!="All")
            {
                query += " and CPReceive.CenterCode='" + TxtCenter.Text.Trim() + "'";
            }
          
            cmd1.CommandText = query + ")";

            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close(); con.Dispose();

         
            ReportParameterClass.SelectionFormula = "";

            Session.Add("rptsql", "");
            Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_CenterRecAmtreport.rpt");
            Session["reportname"] = "Rpt_CenterRecAmtreport";
            Session["RPTFORMAT"] = "pdf";

            // ReportParameterClass.SelectionFormula = sql;
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
        }
    }
    protected void rblpaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (rblpaymenttype.SelectedValue == "Cheque")
        {
            CH.Visible = true;
            CN.Visible = true;
            BN.Visible = true;
        }
        else
        {
            CH.Visible = false;
            CN.Visible = false;
            BN.Visible = false;
        }

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (TxtCenter.Text == "All")
        {
            LblMsg.Text = "Select collection center";
            return;
        }
        if (Convert.ToSingle(txtrecamount.Text) == 0)
        {
            LblMsg.Text = "Enter receive amount";
            return;
        }
        ObjCPB.P_Centercode = TxtCenter.Text;
        ObjCPB.P_Patmenttype = rblpaymenttype.SelectedValue;
        ObjCPB.P_Chequeno = txtchequeNo.Text;
        if (txtchequedate.Text == "")
        {

        }
        else
        {
            ObjCPB.P_chequedate = Convert.ToDateTime(txtchequedate.Text);
        }
        ObjCPB.P_bankname = txtBankName.Text;
        if (txtdiscount.Text == "")
        {
            ObjCPB.P_discount = 0;
        }
        else
        {
            ObjCPB.P_discount = Convert.ToSingle(txtdiscount.Text);
        }
        ObjCPB.P_Entrydate = Convert.ToDateTime(txtentrydate.Text);
        ObjCPB.P_Receiveamount = Convert.ToSingle(txtrecamount.Text);
        ObjCPB.P_Remark = txtremark.Text;
        ObjCPB.P_Fid = ddlfyear.SelectedValue;
        ObjCPB.P_Username = Convert.ToString(Session["Username"]);
        ObjCPB.Insert_CPReceive(Convert.ToInt32(Session["Branchid"]));
        LblMsg.Text = "Payment received successfully.";
       
    }
    protected void txtrecamount_TextChanged(object sender, EventArgs e)
    {
        if (txtrecamount.Text != "")
        {
            txtbalanceamount.Text = Convert.ToString(Convert.ToSingle(Lbltotalbillamount.Text) - (Convert.ToSingle(Lblpaidamount.Text) + Convert.ToSingle(txtrecamount.Text)));
        }

    }

    protected void txtdiscount_TextChanged(object sender, EventArgs e)
    {
        if (txtdiscount.Text != "")
        {

            txtbalanceamount.Text = Convert.ToString(Convert.ToSingle(Lbltotalbillamount.Text) - (Convert.ToSingle(Lblpaidamount.Text) + Convert.ToSingle(txtrecamount.Text) + Convert.ToSingle(txtdiscount.Text)));
        }
    }
    protected void TxtCenter_TextChanged(object sender, EventArgs e)
    {

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