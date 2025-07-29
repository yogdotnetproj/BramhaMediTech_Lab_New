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

public partial class BillforHospital :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
   
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string CenterName = "", PatRegID = "", CenterCode = "", labcode_main = "", DoctorCode = "";
    DateTime stDate = Date.getMinDate(), endDate = Date.getMinDate();
    Patmst_New_Bal_C PNB_C = new Patmst_New_Bal_C();
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
                        checkexistpageright("BillforHospital.aspx");
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
            if (Convert.ToString(ViewState["DoctorCode"]) != "")
            {
                DoctorCode = Convert.ToString(ViewState["DoctorCode"]);
            }
            if (Rbltype.SelectedValue == "Paid")
            {
                paidamt = 1;
            }
            else
            {
                paidamt = 0;
            }
            GVBillFHosp.DataSource = PNB_C.GetPatientforBillBH(CenterName, stDate, endDate, PatRegID, Convert.ToInt32(Session["Branchid"]), 0, CenterCode, ddlfyear.SelectedValue.PadLeft(2, '0') + "-", this.labcode_main, DoctorCode, paidamt);
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
    public static string[] FillCenter(string prefixText, int count)
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
    [WebMethod]
    [ScriptMethod]
    public static string[] FillDoctor(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + prefixText + "%' and DrType='DR' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + prefixText + "%' and DrType='DR' and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            //tests.SetValue(dr["DoctorName"], i);
            tests.SetValue(dr["DoctorName"] + " = " + dr["DoctorCode"], i);
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


    protected void btnsave_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < GVBillFHosp.Rows.Count; i++)
        {
            string Patname = GVBillFHosp.Rows[i].Cells[3].Text;
            string TestName = ((GVBillFHosp.Rows[i].FindControl("lbltestname") as Label).Text.ToString());
            string AmountPaid = GVBillFHosp.Rows[i].Cells[7].Text;
            string Balance = GVBillFHosp.Rows[i].Cells[10].Text;
            string Discount = GVBillFHosp.Rows[i].Cells[8].Text;
            string BillAmount = GVBillFHosp.Rows[i].Cells[6].Text;
            string txtpayment = ((GVBillFHosp.Rows[i].FindControl("txtpayment") as TextBox).Text.ToString());
            string hdn_PID = ((GVBillFHosp.Rows[i].FindControl("hdn_PID") as HiddenField).Value.ToString());
            string Hdn_BillNo = ((GVBillFHosp.Rows[i].FindControl("Hdn_BillNo") as HiddenField).Value.ToString());
            string Hdn_FID = ((GVBillFHosp.Rows[i].FindControl("Hdn_FID") as HiddenField).Value.ToString());
            string PatRegID = GVBillFHosp.Rows[i].Cells[1].Text;
            ViewState["PID"] = hdn_PID;
            bool ischeck = ((GVBillFHosp.Rows[i].FindControl("Chkpayment") as CheckBox).Checked);
            if (ischeck == true)
            {
                PNB_C.UpdateHospitaltocash(Hdn_FID, PatRegID, Convert.ToInt32(hdn_PID), Convert.ToInt32(Session["Branchid"]));
                txtpayment = "";
                BindGrid();
            }

            if (txtpayment != "")
            {

                if ((Convert.ToSingle(txtpayment)) > Convert.ToSingle(BillAmount))
                {
                    //lblmsg.Visible = true;
                    Label12.Visible = false;
                    return;
                }

                Ledgrmst_Bal_C led = new Ledgrmst_Bal_C();
                PatientBillTransactionTableLogic_Bal_C objBilltrans = new PatientBillTransactionTableLogic_Bal_C();
               
                float txtBalance = 0;
                if (BillAmount != "")
                {
                    txtBalance = Convert.ToSingle(BillAmount) - Convert.ToSingle(txtpayment);
                }
                Cshmst_Bal_C Dircash = new Cshmst_Bal_C();
                Dircash.P_Centercode = Session["CenterCode"].ToString();
                Dircash.RecDate = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy"));
                Dircash.BillType = "Cash Bill";
                Dircash.AmtReceived = Convert.ToSingle(txtpayment);
                if (Discount != "")
                {
                    Dircash.NetPayment = Convert.ToSingle(BillAmount);
                    Dircash.Discount = Discount;
                }
                else
                {

                    Dircash.NetPayment = Convert.ToSingle(BillAmount);
                    Dircash.Discount = "0";
                }

                string Patientregno = Cshmst_Bal_C.get_RegNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["PID"]));
                Dircash.patRegID = Convert.ToString(Patientregno);
                Dircash.Patientname = Patname;

                Dircash.Patienttest = TestName;
                Dircash.Remark = "";
                Dircash.PID = Convert.ToInt32(ViewState["PID"]);
                if (txtpayment != "")
                    Dircash.AmtPaid = Convert.ToSingle(txtpayment);
                else
                    Dircash.AmtPaid = 0;
                if (Balance != "")
                    Dircash.Balance = Convert.ToSingle(txtBalance);
                else
                    Dircash.Balance = Convert.ToSingle(0);
                Dircash.Paymenttype = "Cash";


                if (txtchequeno.Text.Trim() != "" && txtbankno.Text.Trim() != "" && chequedate.Text.Trim() != "")
                {
                    Dircash.ChqNo = txtchequeno.Text;
                    Dircash.ChqDate = DateTimeConvesion.getDateFromString(chequedate.Text);
                    Dircash.BankName = txtbankno.Text;
                }
                else
                {

                }

                Dircash.CardNo = "";

                Dircash.username = Session["username"].ToString();
                Dircash.Othercharges = 0;

                if (txtchequeno.Text != "")
                {
                    Dircash.DisFlag = false;
                }
                else
                {
                    Dircash.DisFlag = true;
                }
                Dircash.P_DigModule = Convert.ToInt32(Session["DigModule"]);
                int mno = Cshmst_Bal_C.getMaxNumber(Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["financialyear"]));
                               
                int bno = Convert.ToInt32(Hdn_BillNo);
                Dircash.BillNo = Convert.ToInt32(Hdn_BillNo);
                if (objBilltrans.billnowithPID(Convert.ToInt32(ViewState["PID"])))
                {
                    Dircash.Insert(Convert.ToInt32(Session["Branchid"]));
                }
                else
                {
                    DataTable dtdis = new DataTable();
                    dtdis = Dircash.GetDiscountExist(Convert.ToInt32(bno), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["financialyear"]));

                  
                    float ChangedBillAmt = 0;
                    if (Convert.ToString(ViewState["TotalBillAmt"]) != null && Convert.ToString(ViewState["TotalBillAmt"]) != "")
                    {
                        float CurrentBillAmt = Convert.ToSingle(ViewState["TotalBillAmt"]);
                        float PaidBillAmt = led.GetAmtReceived(Convert.ToInt32(bno), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["financialyear"]));
                        float OtherCharges = led.GetOtherCharges(Convert.ToInt32(bno), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["financialyear"]));
                        if (OtherCharges != 0)
                        {
                            PaidBillAmt = PaidBillAmt - OtherCharges;
                        }
                        if (CurrentBillAmt > PaidBillAmt)
                        {
                            ChangedBillAmt = CurrentBillAmt - PaidBillAmt;

                        }
                    }

                  //  Dircash.Update(Convert.ToInt32(bno), Convert.ToInt32(Session["Branchid"]), false, Convert.ToString(Session["financialyear"]));

                }

                string BillFormatstr = "";
                BillFormatstr = "" + System.Configuration.ConfigurationManager.AppSettings["compInitials"].Trim() + "/" + bno + "/" + FinancialYearTableLogic.getCurrentFinancialYear().StartDate.Year;


                if (txtpayment != "")
                    led.CreditAmt = Convert.ToSingle(txtpayment);
                else
                    led.CreditAmt = 0;
                led.DebitAmt = 0;
               
                led.CenterCode = Session["CenterCode"].ToString();
                led.BillNo = bno;
                led.BillFormat = BillFormatstr.ToString();

               
                string Paymenttype = "";
                if (txtchequeno.Text != "")
                {
                    Paymenttype = "Cheque";
                }
                else
                {
                    Paymenttype = "Cash";
                }
                DataTable dtgap = new DataTable();
                dtgap = led.GetAmountPaid(Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(bno));
                float Billamount = 0;
                if (dtgap.Rows.Count > 0)
                {
                    led.UpdateBillPreviewBal(Convert.ToInt32(bno), Convert.ToInt32(Session["Branchid"]), Session["username"].ToString(), Convert.ToString(Session["financialyear"]));

                    Billamount = 0;
                }
                else
                {
                    Billamount = Convert.ToSingle(txtBalance);
                }
                SqlConnection conn = DataAccess.ConInitForDC();
                SqlCommand sc = new SqlCommand("insert into RecM(BillNo,billdate,AmtPaid,Paymenttype,BankName,branchid,transdate,username,BillAmt,DisAmt,BalAmt,PID,PrevBal)" +
                "values(@BillNo,@billdate,@AmtPaid,@Paymenttype,@BankName,@branchid,@transdate,@username,@BillAmt,@DisAmt,@BalAmt,@PID,@PrevBal)", conn);
                sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;// Convert.ToInt32(lblBillNo.Text);
                if (txtpayment != "")
                    sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = Convert.ToSingle(txtpayment);
                else
                    sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0;
                sc.Parameters.Add(new SqlParameter("@billdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy"));
                sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = Paymenttype;
                sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 50)).Value = txtbankno.Text;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                sc.Parameters.Add(new SqlParameter("@transdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy"));//Convert.ToDateTime(System.DateTime.Now);
                sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = Session["username"].ToString();
                sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = Convert.ToSingle(Billamount);
                sc.Parameters.Add(new SqlParameter("@DisAmt", SqlDbType.Float)).Value = Convert.ToDouble(Discount);
                sc.Parameters.Add(new SqlParameter("@BalAmt", SqlDbType.Float)).Value = Convert.ToDouble(txtBalance);//txtBalance
                sc.Parameters.AddWithValue("@PID", Convert.ToInt32(ViewState["PID"]));

                sc.Parameters.Add(new SqlParameter("@PrevBal", SqlDbType.Float)).Value = Convert.ToDouble(txtBalance);
                conn.Open();
                sc.ExecuteNonQuery();
                BindGrid();
                //  }


                // ==========
            }
        }
    }

    protected void GVBillFHosp_SelectedIndexChanged(object sender, EventArgs e)
    {
        // BillNo
        string BillNo = GVBillFHosp.Rows[GVBillFHosp.SelectedIndex].Cells[2].Text;
        string sql1 = "";
        SqlConnection conn1 = DataAccess.ConInitForDC();
        SqlCommand sc1 = conn1.CreateCommand();

        sc1.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT dbo.Cshmst.BillNo, dbo.Cshmst.RecDate, dbo.Cshmst.BillType, dbo.Cshmst.AmtReceived, " +
                          " dbo.Cshmst.Discount, dbo.Cshmst.NetPayment, dbo.Cshmst.AmtPaid, dbo.Cshmst.Balance, " +
                          " dbo.Cshmst.username,dbo.Cshmst.OtherCharges,dbo.patmst.PatRegID, dbo.patmst.intial, dbo.patmst.Patname, " +
                          "  dbo.patmst.sex, dbo.patmst.Age, dbo.patmst.Drname, " +
                          " dbo.patmst.TelNo, dbo.DrMT.DoctorCode, dbo.DrMT.DoctorName, dbo.MainTest.Maintestname, dbo.MainTest.MTCode, " +
                          " dbo.patmstd.TestRate, dbo.PackMst.PackageName, dbo.patmstd.PackageCode, dbo.Cshmst.DisFlag, " +
                          " dbo.patmst.Patusername, dbo.patmst.Patpassword, dbo.Cshmst.Comment,dbo.patmst.MDY,dbo.patmst.Remark AS PatientRemark,dbo.patmst.Pataddress,dbo.patmst.PPID ,dbo.patmst.UnitCode FROM dbo.patmst INNER JOIN " +
                          " dbo.DrMT ON dbo.patmst.CenterCode = dbo.DrMT.DoctorCode AND  " +
                          " dbo.patmst.branchid = dbo.DrMT.branchid INNER JOIN " +
                          " dbo.Cshmst INNER JOIN dbo.MainTest INNER JOIN " +
                          " dbo.patmstd ON dbo.MainTest.MTCode = dbo.patmstd.MTCode AND " +
                          " dbo.MainTest.branchid = dbo.patmstd.branchid ON dbo.Cshmst.PID = dbo.patmstd.PID AND " +
                          " dbo.Cshmst.branchid = dbo.patmstd.branchid ON dbo.patmst.PID = dbo.patmstd.PID AND " +
                          " dbo.patmst.branchid = dbo.patmstd.branchid LEFT OUTER JOIN " +
                          " dbo.PackMst ON dbo.patmstd.branchid = dbo.PackMst.branchid AND " +
                          " dbo.patmstd.PackageCode = dbo.PackMst.PackageCode where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and Cshmst.BillNo=" + BillNo + ")";// 


        conn1.Open();
        sc1.ExecuteNonQuery();
        conn1.Close(); conn1.Dispose();

        Session.Add("rptsql", sql1);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PayReceipt.rpt");
        Session["reportname"] = "CashReceipt";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql1;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);


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
                ViewState["DoctorCode"] = Doc_code[1].ToString();
            }

        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        string sql = "";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[VW_bth] AS (SELECT * from VW_csmst1vw " +
        " where IsbillBH=1  and Monthlybill=0  and PatRegID<>'' and branchid=" + Convert.ToInt32(Session["Branchid"]) + "";
        if (Rbltype.SelectedValue == "Paid")
        {
            query += " and Amtpaid >0  ";
        }
        else
        {
            query += " and Amtpaid = 0  ";

        }
        if (Convert.ToString(ViewState["DoctorCode"]) != "")
        {
            query += " and DoctorCode='" + Convert.ToString(ViewState["DoctorCode"]).Trim() + "'";
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