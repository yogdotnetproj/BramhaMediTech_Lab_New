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


public partial class SaleRegister :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string Center = "", Patregid = "", CenterCode = "", labcode_main = "", DocCode = "";
    DateTime stDate = Date.getMinDate(), endDate = Date.getMinDate();
    Patmst_New_Bal_C ObjPNB_C = new Patmst_New_Bal_C();
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
                        checkexistpageright("SaleRegister.aspx");
                    }
                }

                LUNAME.Text = Convert.ToString(Session["username"]);
                LblDCName.Text = Convert.ToString(Session["Bannername"]);
                LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                this.PopulateTreeView(dt, 0, null);

              
                ddlusername.DataSource = createuserlogic_Bal_C.getAllUsers_IRD(Convert.ToInt32(Session["Branchid"]));
                ddlusername.DataTextField = "username";
                ddlusername.DataValueField = "username";
                ddlusername.DataBind();
                ddlusername.Items.Insert(0, "Select UserName");
                ddlusername.SelectedIndex = -1;
                BindTime();
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
                DocCode = txtCenter.Text;
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
            dt = new DataTable();
            dt = ObjPNB_C.GetPatientforBill_Saleregister(Center, stDate, endDate, Patregid, Convert.ToInt32(Session["Branchid"]), 0, txtPatientname.Text, ddlusername.SelectedValue, this.labcode_main, DocCode, 0);
            GVBillFHosp.DataSource = dt;
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
    public static string[] Getcenter(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT  DoctorCode, rtrim(DrInitial)+' '+DoctorName as name FROM DrMT where ( DoctorName like '" + prefixText + "%' or DoctorCode like '" + prefixText + "%') and DrType='DR' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT  DoctorCode, rtrim(DrInitial)+' '+DoctorName as name FROM DrMT where  ( DoctorName like '" + prefixText + "%' or DoctorCode like '" + prefixText + "%')  and DrType='DR' and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
           // tests.SetValue(dr["DoctorName"], i);
            tests.SetValue(dr["name"] + " = " + dr["DoctorCode"], i);
            i++;
        }
        return tests;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] GetPatientName(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM patmst where Patname like '%" + prefixText + "%'  and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by Patname ", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM patmst where Patname like '%" + prefixText + "%'  and branchid=" + branchid + " order by Patname", con);
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

    float total = 0, DisAmt = 0, Taxable = 0, Taxamount = 0, NetAmount=0;
    bool IAct = false;
    protected void GVBillFHosp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
         // LblAmount.Text+=Convert.ToSingle( e.Row.Cells[6].Text.Trim());
          if (e.Row.RowType == DataControlRowType.DataRow)
          {
              if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "Isactive")) == true)
              {
                  total += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "TestCharges"));
                  DisAmt += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "Discount"));
                  Taxable += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "Taxable"));
                  Taxamount += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "Taxamount"));
                  NetAmount += Convert.ToSingle(DataBinder.Eval(e.Row.DataItem, "NetAmount"));
              }
          }
         
          LblAmount.Text = total.ToString();
          Lbldiscount.Text = DisAmt.ToString();
          Lbltaxable.Text = Taxable.ToString();
          Lbltaxontaxable.Text = Taxamount.ToString();
          Lblnetamount.Text = NetAmount.ToString();
        }
    }




    protected void btnreport_Click(object sender, EventArgs e)
    {
        #region MyRegion
        string sql = "";
        SqlConnection conn = DataAccess.ConInitForDC();
                        SqlCommand sc = conn.CreateCommand();

                        sc.CommandText = "ALTER VIEW [dbo].[VW_SaleReport_NP] AS (SELECT *,Testcharges-Discount as Taxable ,round((Testcharges-Discount)+TaxAmount,0) as NetAmount from VW_csmst1vw where  PatRegID<>'' and branchid=" + Session["Branchid"].ToString() + " ";  

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
            DocCode = txtCenter.Text;
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

        if (DocCode != null && DocCode != "All")
        {
            sc.CommandText += " and DrName='" + DocCode.Trim() + "'";

        }
        if (txtPatientname.Text != "" && txtPatientname.Text != null)
        {
            sc.CommandText += "and Fname='" + txtPatientname.Text + "'";

        }
        if (ddlusername.SelectedValue != "" && ddlusername.SelectedValue != "Select UserName")
        {
            sc.CommandText += "and Username='" + ddlusername.SelectedValue + "'";

        }

        if (Patregid != "" && Patregid != null)
        {
            sc.CommandText += " and PatRegID='" + Patregid + "'";
        }
        else
        {

            if (stDate != null && endDate != null)
            {
                sc.CommandText = sc.CommandText + " and Phrecdate between '" + Convert.ToDateTime(stDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
            }
        }
        sc.CommandText += " )";
        conn.Open();
        sc.ExecuteNonQuery();
        conn.Close(); conn.Dispose();
        #endregion

       
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[VW_BillCancel_Report] AS (SELECT   top (99.99) percent  SaleCancelDetails.CancelReceiptNo, SaleCancelDetails.PID, SaleCancelDetails.BillNo, SaleCancelDetails.billdate, "+
               " SaleCancelDetails.AmtPaid,   SaleCancelDetails.PaymentType, SaleCancelDetails.BankName, SaleCancelDetails.branchid,  SaleCancelDetails.transdate, "+
               " SaleCancelDetails.username,   SaleCancelDetails.BillAmt, SaleCancelDetails.DisAmt,  SaleCancelDetails.BalAmt, SaleCancelDetails.tdate, "+
               " SaleCancelDetails.PrevBal, SaleCancelDetails.IsActive,    SaleCancelDetails.TaxPer, SaleCancelDetails.TaxAmount,  "+
               " SaleCancelDetails.PrintCount, patmst.PatRegID, patmst.intial,   patmst.Patname,   patmst.sex,   patmst.intial+' '+ patmst.Patname as PatientName,  "+
               " patmst.CenterCode,   patmst.Drname, patmst.CenterName,   BillAmt-DisAmt as Taxable ,round((BillAmt-DisAmt)+TaxAmount,0) as NetAmount  "+
               " FROM         SaleCancelDetails INNER JOIN   patmst ON SaleCancelDetails.PID = patmst.PID   " +
                     " where PatRegID<>'' and  SaleCancelDetails.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";


        if (txtregno.Text != "")   
        {
            query += " and PatRegID='" + Convert.ToString(txtregno.Text).Trim() + "'";

        }

        if (fromdate.Text != "" && todate.Text != "")
        {

            query += " and SaleCancelDetails.billdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).AddDays(+1).ToString("MM/dd/yyyy") + "')";
        }



        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_SaleRegister_NP.rpt");
        Session["reportname"] = "Rpt_SaleRegister_NP";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

    }
    protected void txtCenter_TextChanged(object sender, EventArgs e)
    {
        string[] patient = new string[] { "", "" };
        patient = txtCenter.Text.Split('=');
        string name = patient[1];
        txtCenter.Text = patient[0];
        ViewState["DocCode"] = patient[0];
    }
    private void BindTime()
    {
        // Set the start time (00:00 means 12:00 AM)
        DateTime StartTime = DateTime.ParseExact("00:00", "HH:mm", null);
        // Set the end time (23:55 means 11:55 PM)
        DateTime EndTime = DateTime.ParseExact("23:55", "HH:mm", null);
        //Set 5 minutes interval
        TimeSpan Interval = new TimeSpan(0, 5, 0);
        //To set 1 hour interval
        //TimeSpan Interval = new TimeSpan(1, 0, 0);           
        ddlTimeFrom.Items.Clear();
        ddlTimeTo.Items.Clear();
        while (StartTime <= EndTime)
        {
            ddlTimeFrom.Items.Add(StartTime.ToShortTimeString());
            ddlTimeTo.Items.Add(StartTime.ToShortTimeString());
            StartTime = StartTime.Add(Interval);
        }
        ddlTimeFrom.Items.Insert(0, new ListItem("--Select Time--", "0"));
        ddlTimeTo.Items.Insert(0, new ListItem("--Select Time--", "0"));
    }

    protected void GVBillFHosp_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex == -1)
            return;
        int PID = Convert.ToInt32(GVBillFHosp.DataKeys[e.RowIndex].Value);

        #region MyRegion
        string sql = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();

        sc.CommandText = "ALTER VIEW [dbo].[VW_SaleReport_NP] AS (SELECT *,Testcharges-Discount as Taxable ,round((Testcharges-Discount)+TaxAmount,0) as NetAmount from VW_csmst1vw where  Patregid<>'' and branchid=" + Session["Branchid"].ToString() + "  and VW_csmst1vw.PID=" + PID + ")";// 


        conn.Open();
        sc.ExecuteNonQuery();
        conn.Close(); conn.Dispose();
        #endregion

        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[VW_BillCancel_Report] AS (SELECT   top (99.99) percent  SaleCancelDetails.CancelReceiptNo, SaleCancelDetails.PID, SaleCancelDetails.BillNo, SaleCancelDetails.billdate, " +
                      "  SaleCancelDetails.AmtPaid,   SaleCancelDetails.PaymentType, SaleCancelDetails.BankName, SaleCancelDetails.branchid, " +
                       " SaleCancelDetails.transdate, SaleCancelDetails.username,   SaleCancelDetails.BillAmt, SaleCancelDetails.DisAmt, " +
                       " SaleCancelDetails.BalAmt, SaleCancelDetails.tdate, SaleCancelDetails.PrevBal, SaleCancelDetails.IsActive,   " +
                       " SaleCancelDetails.TaxPer, SaleCancelDetails.TaxAmount, SaleCancelDetails.PrintCount, patmst.PatRegID, patmst.intial,  " +
                       " patmst.Patname,   patmst.sex,   patmst.intial+' '+ patmst.Patname as PatientName,    patmst.CenterCode,  " +
                       " patmst.DrName, patmst.CenterName,   BillAmt-DisAmt as Taxable ,round((BillAmt-DisAmt)+TaxAmount,0) as NetAmount   " +
                       " FROM         SaleCancelDetails INNER JOIN   patmst ON SaleCancelDetails.PID = patmst.PID  " +
                     " where Patregid<>'' and  SaleCancelDetails.branchid=" + Convert.ToInt32(Session["Branchid"]) + " and SaleCancelDetails.PID =" + PID + " ";


        if (txtregno.Text != "")
        {
            query += " and PatRegID='" + Convert.ToString(txtregno.Text).Trim() + "'";

        }

        if (fromdate.Text != "" && todate.Text != "")
        {

            query += " and SaleCancelDetails.billdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).AddDays(+1).ToString("MM/dd/yyyy") + "')";
        }



        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_SaleRegister_NP.rpt");
        Session["reportname"] = "Rpt_SaleRegister_NP";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
    }
    protected void btnexcreport_Click(object sender, EventArgs e)
    {
        #region MyRegion
        string sql = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();

        sc.CommandText = "ALTER VIEW [dbo].[VW_SaleReport_NP] AS (SELECT *,Testcharges-Discount as Taxable ,round((Testcharges-Discount)+TaxAmount,0) as NetAmount from VW_csmst1vw where  Patregid<>'' and branchid=" + Session["Branchid"].ToString() + " ";

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
            DocCode = txtCenter.Text;
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

        if (DocCode != null && DocCode != "All")
        {
            sc.CommandText += " and DocName='" + DocCode.Trim() + "'";

        }
        if (txtPatientname.Text != "" && txtPatientname.Text != null)
        {
            sc.CommandText += "and Patname='" + txtPatientname.Text + "'";

        }
        if (ddlusername.SelectedValue != "" && ddlusername.SelectedValue != "Select UserName")
        {
            sc.CommandText += "and Username='" + ddlusername.SelectedValue + "'";

        }

        if (Patregid != "" && Patregid != null)
        {
            sc.CommandText += " and Patregid='" + Patregid + "'";
        }
        else
        {

            if (stDate != null && endDate != null)
            {
                sc.CommandText = sc.CommandText + " and Phrecdate between '" + Convert.ToDateTime(stDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
            }
        }
        sc.CommandText += " )";
        conn.Open();
        sc.ExecuteNonQuery();
        conn.Close(); conn.Dispose();
        #endregion


        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[VW_BillCancel_Report] AS (SELECT   top (99.99) percent  SaleCancelDetails.CancelReceiptNo, SaleCancelDetails.PID, SaleCancelDetails.BillNo, SaleCancelDetails.billdate, " +
                      "  SaleCancelDetails.AmtPaid,   SaleCancelDetails.PaymentType, SaleCancelDetails.BankName, SaleCancelDetails.branchid, " +
                       " SaleCancelDetails.transdate, SaleCancelDetails.username,   SaleCancelDetails.BillAmt, SaleCancelDetails.DisAmt, " +
                       " SaleCancelDetails.BalAmt, SaleCancelDetails.tdate, SaleCancelDetails.PrevBal, SaleCancelDetails.IsActive,   " +
                       " SaleCancelDetails.TaxPer, SaleCancelDetails.TaxAmount, SaleCancelDetails.PrintCount, patmst.Patregid, patmst.intial,  " +
                       " patmst.Patname,   patmst.sex,   patmst.intial+' '+ patmst.Patname as PatientName,    patmst.CenterCode,  " +
                       " patmst.DrName, patmst.CenterName,   BillAmt-DisAmt as Taxable ,round((BillAmt-DisAmt)+TaxAmount,0) as NetAmount   " +
                       " FROM         SaleCancelDetails INNER JOIN   patmst ON SaleCancelDetails.PID = patmst.PID  " +
                     " where Patregid<>'' and  SaleCancelDetails.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";


        if (txtregno.Text != "")
        {
            query += " and Patregid='" + Convert.ToString(txtregno.Text).Trim() + "'";

        }

        if (fromdate.Text != "" && todate.Text != "")
        {

            query += " and SaleCancelDetails.billdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).AddDays(+1).ToString("MM/dd/yyyy") + "')";
        }



        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_SaleRegister_NP.rpt");
        Session["reportname"] = "Rpt_SaleRegister_NP";
        Session["RPTFORMAT"] = "EXCEL";

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