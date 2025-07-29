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
using System.Drawing;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Management;


using System.Diagnostics;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Text.RegularExpressions;
using Microsoft;
using System.Collections.Specialized;
using System.Text;

using ZXing;
using ZXing.QrCode;
using ZXing.Common;

public partial class Back_ClickSample :BasePage
{
    API_DataTransfer_C Obj_Adt = new API_DataTransfer_C();
    string maintestshort = "";
    TreeviewBind_C ObjTB = new TreeviewBind_C();

    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Get_taxValue_C Objtax = new Get_taxValue_C();
    int bno = 0, SocialMedia = 0,DiscAllowed=0;
    public static string BarcodeGenerate = "";
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string Date2 = DateTime.Now.AddDays(-1).ToString("ddMMyyyy");
    Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();
    public void Bindbanner()
    {
        DataTable dtban = new DataTable();
        DataTable dtchk = new DataTable();
        dtban = ObjTB.Bindbanner();
        if (dtban.Rows.Count > 0)
        {
            if (Convert.ToString(dtban.Rows[0]["PaymentTypeDefaultActive"]) == "1")
            {
                ViewState["PaymentType"] = "YES";
            }
            else
            {
                ViewState["PaymentType"] = "NO";
            }
            if (Convert.ToString(dtban.Rows[0]["Type"]) == "0")
            {
                ViewState["VALIDATE"] = "YES";
            }
            else
            {
                ViewState["VALIDATE"] = "NO";
              //  rblPaymenttype.Items[0].Selected = true;
            }
            if (Convert.ToBoolean(dtban.Rows[0]["BarCodeImageReq"]) == false)
            {
                ViewState["BarCodeImageReq"] = "NO";
            }
            else
            {
                ViewState["BarCodeImageReq"] = "YES";
            }
            
            string Currentdate = Date.getdate().ToString("dd/MM/yyyy");
            //if (Convert.ToDateTime(Currentdate) >= Convert.ToDateTime("15 /09/ 2020"))
            //{
            //    Response.Redirect("~/Login.aspx?Activation=Yes");
            //}
            string BName = " Diagnostic Center";
            string BCount = Patmst_New_Bal_C.PatientCountBanner(Convert.ToInt32(Session["Branchid"]));
            //  lblDemoHospitalName.Text = Convert.ToString(dtban.Rows[0]["BannerName"]).Trim();
            //if (Convert.ToString(dtban.Rows[0]["BannerName"]).Trim() == BName)
            //{
            //    if (Convert.ToInt32(BCount) > 310)
            //    {
            //        string Currentdate = Date.getdate().ToString("dd/MM/yyyy");
            //        if (Convert.ToDateTime(Currentdate) >= Convert.ToDateTime("12 /12/ 2017") && Convert.ToDateTime(Currentdate) < Convert.ToDateTime("14 / 12 / 2017"))
            //        {
            //            ObjTB.UpdateAuthincate(Convert.ToInt32(Session["Branchid"]));
            //        }

            //        dtchk = ObjTB.Checkflag();
            //        if (dtchk.Rows.Count == 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);

            //            Response.Redirect("~/Login.aspx?Activation=Yes");
            //        }
            //    }
            //}
            //else
            //{

            //    if (Convert.ToInt32(BCount) > 310)
            //    {
            //        string  Currentdate= Date.getdate().ToString("dd/MM/yyyy");
            //        if (Convert.ToDateTime(Currentdate) >= Convert.ToDateTime("12 /12/ 2017") && Convert.ToDateTime(Currentdate) < Convert.ToDateTime("14 / 12 / 2017"))
            //        {
            //            ObjTB.UpdateAuthincate(Convert.ToInt32(Session["Branchid"]));
            //        }

            //        dtchk = ObjTB.Checkflag();
            //        if (dtchk.Rows.Count == 0)
            //        {
            //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
            //            Response.Redirect("~/Login.aspx?Activation=Yes");
            //            Response.Redirect("~/Login.aspx");
            //        }
            //    }

            //}

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
            Response.Redirect("~/Login.aspx?Activation=Yes");

        }
    }
    public void BindTime()
    {
        DateTime dt = Convert.ToDateTime("06:00:00"); //for adding start time
        for (int i = 0; i <= 215; i++) //Set up every 30 minute interval
        {
            ListItem li2 = new ListItem(dt.ToShortTimeString(), dt.ToShortTimeString());
            li2.Selected = false;
            ddlFromTime.Items.Add(li2);

            dt = dt.AddMinutes(05);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

       // Label2.Text = "Register No Is :5555";
        grdTests.EmptyDataText = null;

        Bindbanner();
        if (!Page.IsPostBack)
        {
            BindTime();
            fromdate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
            string BackEntry = "Back Date Entry " + fromdate.Text;
          //  ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + BackEntry.ToString() + "');</script>", false);
            if (Convert.ToString(Session["HMS"]) != "Yes")
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("Back_ClickSample.aspx");
                }
            }
           
            cmbInitial.Focus();
            try
            {
                if (ViewState["PaymentType"] == "YES")
                {
                }
                else
                {
                     rblPaymenttype.Items[0].Selected = true;
                }
                if (Convert.ToString(Request.QueryString["PatSave"]) == "'Yes'")
                {

                    string AA = "Record Saved. Reg No is " + Request.QueryString["PatRegID"] + " / " + Request.QueryString["Fname"] + " ";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);
                    Label10.Text = AA;
                    ViewState["PID"] = Request.QueryString["PID"];
                   // string fID = FinancialYearTableLogic.getCurrentFinancialYear().FinancialYearId.PadLeft(2, '0');
                    string fID = Convert.ToString(FinancialYearTableLogic.getCurrentFinancialYear(Convert.ToInt32(Session["Branchid"])).FinancialYearId);
                    ViewState["Fid0"] = fID; ;
                    ViewState["RegN0"] = Request.QueryString["PatRegID"];
                    ViewState["receipt"] = "No";
                    btnbarcodeEntry.Enabled=true;
                    btnpatientcard.Enabled=true;
                    btnbprint.Enabled = true;
                }
               

                BindShortcut_test();
                if (Convert.ToString(Session["ISDemography"]) == "YES")
                {
                    D1.Visible = true;
                    D2.Visible = true;
                    D3.Visible = true;
                    D4.Visible = true;
                    D5.Visible = true;
                    D6.Visible = true;
                    D7.Visible = true;
                    rblretecate.Visible = true;
                    RadioButtonList1.Visible = false;
                }

                if (Session["usertype"].ToString() == "CollectionCenter" || Session["usertype"].ToString() == "Collection Center")
                {

                    txtCenter.Enabled = false;
                    string Centercode = Session["CenterCode"].ToString();
                    string Center = Patmst_Bal_C.getname(Centercode, Convert.ToInt32(Session["Branchid"]));
                    txtCenter.Text = Center;
                }
                else
                {
                    txtCenter.Text = DrMT_sign_Bal_C.GetCenterwithName(Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]));
                    Session["CenterCode"] = DrMT_sign_Bal_C.Get_CenterDefault(Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]));
                    string ISCashbill = DrMT_sign_Bal_C.getcheckmonthlybill(Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["CenterCode"]));
                    if (ISCashbill == "False")
                    {
                        Session["Monthlybill"] = "YES";

                    }
                    else
                    {
                        Session["Monthlybill"] = "No";
                    }
                }
                Session["CenterCodeTemp"] = Patmst_Bal_C.GetConttpersonac(txtCenter.Text, Convert.ToInt32(Session["Branchid"]));


                ViewState["DiscAmt"] = "0";

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
                    // Server.Transfer("~/ErrorMessage.aspx");
                }
            }
            try
            {
                ViewState["btnsave"] = "";
                ViewState["barcodeempty"] = "";

                if (Session["usertype"] != null && Session["username"] != null)
                {

                    bool cashflag = DrMT_sign_Bal_C.checkcashflag(Session["CenterCode"].ToString(), Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]));

                }

                cmbInitial.DataSource = PatientinitialLogic_Bal_C.getInitial();
                cmbInitial.DataTextField = "prefixName";
                cmbInitial.DataValueField = "prefixName";
                cmbInitial.DataBind();
                cmbInitial.Items.Insert(0, new ListItem("Select Initial", "0"));




                DataTable TableTestWise = new DataTable();
                DataColumn column;
                column = new DataColumn("MTCode", Type.GetType("System.String"));
                TableTestWise.Columns.Add(column);
                column = new DataColumn("Maintestname", Type.GetType("System.String"));
                TableTestWise.Columns.Add(column);
                column = new DataColumn("Amount", Type.GetType("System.String"));
                TableTestWise.Columns.Add(column);
                column = new DataColumn("Testdiscount", Type.GetType("System.String"));
                TableTestWise.Columns.Add(column);
                column = new DataColumn("Type", Type.GetType("System.String"));
                TableTestWise.Columns.Add(column);

                ViewState["TableTestWise"] = TableTestWise;
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
                    // Server.Transfer("~/ErrorMessage.aspx");
                }
            }
        }

        //if (txtCenter.Text == "OPD")
        //{
        //    chkIsBH.Enabled = false;
        //    chkIsBH.Checked = false;
        //    Rbldisctype.Enabled = true;
        //    rblPaymenttype.Enabled = true;
        //    txtpaidamount.Enabled = true;
        //    txtDisamount.Enabled = true;
        //}
        //if (txtCenter.Text == "IPD")
        //{
        //    rblPaymenttype.Enabled = false;
        //    txtpaidamount.Enabled = false;
        //    txtDisamount.Enabled = false;
        //    chkIsBH.Checked = true; ;
        //    Rbldisctype.Enabled = false;
        //}
        if (grdPackage.Rows.Count != 0)
        {
            btnSubmit.Enabled = true;
            //grdTests
            txttests.Focus();

        }
        else
        {
            btnSubmit.Enabled = false;

        }
        if (Convert.ToString(Session["Monthlybill"]) == "YES")
        {
            txtpaidamount.Enabled = false;
            rblPaymenttype.Enabled = false;
            // IPD//
            rblPaymenttype.Enabled = false;
            txtpaidamount.Enabled = false;
            txtDisamount.Enabled = false;
            chkIsBH.Checked = true; ;
            Rbldisctype.Enabled = false;
        }
        else
        {
            txtpaidamount.Enabled = true;
            rblPaymenttype.Enabled = true;
            //OPD//
            chkIsBH.Enabled = false;
            chkIsBH.Checked = false;
           // Rbldisctype.Enabled = true;
            rblPaymenttype.Enabled = true;
            txtpaidamount.Enabled = true;
           // txtDisamount.Enabled = true;
        }
        if (grdTests.Rows.Count > 0)
        {
            txtCenter.Enabled = false;
        }
        else
        {

        }
        if (Convert.ToString(Session["Monthlybill"]) == "YES")
        {
            txtpaidamount.Enabled = false;
            rblPaymenttype.Enabled = false;
            // IPD//
            rblPaymenttype.Enabled = false;
            txtpaidamount.Enabled = false;
            txtDisamount.Enabled = false;
            chkIsBH.Checked = true; ;
            Rbldisctype.Enabled = false;
        }
        else
        {
            txtpaidamount.Enabled = true;
            rblPaymenttype.Enabled = true;
            //OPD//
            chkIsBH.Enabled = false;
            chkIsBH.Checked = false;
            // Rbldisctype.Enabled = true;
            rblPaymenttype.Enabled = true;
            txtpaidamount.Enabled = true;
           // txtDisamount.Enabled = true;
        }
        if (grdTests.Rows.Count > 0)
        {
            txtCenter.Enabled = false;
        }
        else
        {

        }
       
    }
    public void BindShortcut_test()
    {

        dt = new DataTable();
        dt = ObjTB.Bind_Shortcuttest();
        if (dt.Rows.Count > 0)
        {
            Chkmaintestshort.DataSource = dt;
            Chkmaintestshort.DataTextField = "Maintestname";
            Chkmaintestshort.DataValueField = "RoutinTestCode";
            Chkmaintestshort.DataBind();

        }
        else
        {

        }
        dt = new DataTable();
        dt = ObjTB.Get_DefaultDoctor();
        if (dt.Rows.Count > 0)
        {
             txtDoctorName.Text = Convert.ToString(  dt.Rows[0]["name"]);
        }

    }
    void GetRecords(int PID)
    {
        ArrayList al = (ArrayList)Patmst_New_Bal_C.Get_patmst_againsPIDID(PID, Convert.ToInt32(Session["Branchid"]));
        IEnumerator ie = al.GetEnumerator();
        while (ie.MoveNext())
        {
            Patmst_Bal_C PBC = (ie.Current as Patmst_Bal_C);
            cmbInitial.SelectedValue = PBC.Initial.Trim();
            txtFname.Text = PBC.Patname;

            ddlsex.SelectedValue = PBC.Sex; ;

            txtAge.Text = PBC.Age.ToString();
            cmdYMD.SelectedItem.Text = PBC.MYD;

            txtemail.Text = PBC.Email;


            txtDoctorName.Text = PBC.Drname + "=" + PBC.DoctorCode;

            this.txtAge_TextChanged(null, null);

        }
    }

    public ICollection DoctorFill(string Drname)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        sc = new SqlCommand("SELECT DoctorCode,Rtrim(DrInitial)+' '+Drname as name from  DrMT where DrType='DR' order by Drname", conn);

        SqlDataReader dr = null;
        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    DrMT_Bal_C DrMT = new DrMT_Bal_C();
                    DrMT.DoctorCode = dr["DoctorCode"].ToString();
                    DrMT.Name = dr["name"].ToString();
                    al.Add(DrMT);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }

        finally
        {
            try
            {
                if (dr != null) dr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }

        }

        return al;
    }
    public DataSet DoctorFill(string Drname, int branchid, string s)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+Drname as name from  DrMT where DrType='DR'and branchid=" + branchid + " order by Drname", conn);//and CC_code='" + Drname + "'
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            sda.Fill(ds);
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }

        return ds;
    }
 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        
        // System.Threading.Thread.Sleep(3000);
        //  lblText.Text = "Processing completed";
        if (validation() == true)
        {
         string CenterCode = DrMT_sign_Bal_C.Get_C_Code(txtCenter.Text.Trim(), Convert.ToInt32(Session["Branchid"]));
         if (CenterCode == "" || CenterCode == null)
         {
             // Label8.Visible = true;
             txttests.Enabled = false;
             txtCenter.Focus();
             txtCenter.BackColor = Color.Red;
             txtCenter.ForeColor = Color.White;
             btnSubmit.Enabled = false;
             BtnReceipt.Enabled = false;
             string AA = "Select Valid center name";
             Label10.Text = AA;
             ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);
             return;
         }
         else
         {
             btnSubmit.Enabled = false;
             BtnReceipt.Enabled = false;           
             txtCenter.BackColor = Color.White;
             txtCenter.ForeColor = Color.Black;
         }
        try
        {
            if (txtpaidamount.Text == "")
            {
                txtpaidamount.Text = "0";
            }
            if (txtDisamount.Text == "")
            {
                txtDisamount.Text = "0";
            }
            if (txtotherAmt.Text == "")
            {
                txtotherAmt.Text = "0";
            }

           
                ViewState["barcodeempty"] = "";
                Patmst_Bal_C OBJ_PBC = new Patmst_Bal_C();
                Patmstd_Bal_C PBCL = new Patmstd_Bal_C();
                int mobcnt = txttelno.Text.Length;
                string CCount = PBCL.GetSMSString_CountryCode("Registration", Convert.ToInt16(Session["Branchid"]));
                int CountryC = 0;
                if (CCount.Length == 2)
                {
                    CountryC = 2;
                }
                else
                {
                    CountryC = 3;
                }
               // if (OBJ_PBC.getallreadyRegister(DateTime.Now.AddMinutes(-2), txtFname.Text.Trim(), int.Parse(txtAge.Text), CountryC))
             if (OBJ_PBC.getallreadyRegister(Convert.ToDateTime( fromdate.Text).AddMinutes(-2), txtFname.Text.Trim(), int.Parse(txtAge.Text), CountryC))
                {

                    string[] DoctorCode = new string[] { "", "" };

                    if (txtDoctorName.Text != "")
                    {
                        if (!txtDoctorName.Text.Contains("="))
                        {

                        }
                        DoctorCode = txtDoctorName.Text.Split('=');
                        if (DoctorCode.Length.ToString() != "2")
                        {

                            //return;
                        }
                        else
                        {

                        }
                    }
                    if (txtCenter.Text == "")
                    {

                        return;
                    }
                    if (txtDoctorName.Text == "")
                    {

                        return;
                    }
                    else
                    {

                    }
                    try
                    {
                        string testCodes = "";
                        string sampletypes = "";
                        string BarcodeId = "";
                        string testname = "";
                        int PID = Patmst_New_Bal_C.PIDAutoGenerateLogic(Convert.ToInt32(Session["Branchid"]));
                        if (PID > 0)
                        {
                            if (PID == 0)
                            {
                                return;
                            }
                            ViewState["PID"] = PID;

                            foreach (GridViewRow gvr in grdTests.Rows)
                            {
                                if (testCodes == "")
                                {
                                    testCodes = gvr.Cells[1].Text.Trim();
                                    testname = gvr.Cells[2].Text.Trim();
                                }
                                else
                                {
                                    testCodes = testCodes + ", " + gvr.Cells[1].Text.Trim();
                                    testname = testname + ", " + gvr.Cells[2].Text.Trim();
                                }
                            }

                            if (chkIsBH.Checked == true)
                            {
                                PBCL.BHFlag = true;
                            }
                            else
                            {
                                PBCL.BHFlag = false;
                            }
                            if (Convert.ToString(Session["Monthlybill"]) == "YES")
                            {
                                PBCL.Monthlybill = true;
                            }
                            else
                            {
                                PBCL.Monthlybill = false;
                            }
                            PBCL.Initial = cmbInitial.SelectedItem.Text.Trim();
                            PBCL.Patname = txtFname.Text;
                            PBCL.Sex = ddlsex.SelectedItem.Text;
                            PBCL.Age = Convert.ToInt32(txtAge.Text);
                            PBCL.Phone = txttelno.Text;

                            PBCL.Address1 = txt_address.Text;

                            PBCL.P_remark = "";
                            PBCL.RefDr = txtDoctorName.Text.Trim();
                            PBCL.MYD = cmdYMD.SelectedItem.Text;
                            PBCL.PatientcHistory = "";
                            PBCL.Email = txtemail.Text;
                            PBCL.UnitCode = Convert.ToString(Session["UnitCode"]);

                            PBCL.Initial = cmbInitial.SelectedItem.Text.Trim();
                            PBCL.Patname = txtFname.Text.Trim();
                            PBCL.Sex = ddlsex.SelectedItem.Text;
                            string CounCode = PBCL.GetSMSString_CountryCode("Registration", Convert.ToInt32(Session["Branchid"]));
                            if (CounCode.Length == 2)
                            {
                               // PBCL.TelNo = CounCode + "" + txttelno.Text;
                                PBCL.TelNo = "91" + txttelno.Text;
                            }
                            else
                            {
                               // PBCL.TelNo = CounCode + "" + txttelno.Text;
                                PBCL.TelNo = "91" + txttelno.Text;
                            }

                            //if (CounCode == "91")
                            //{
                            //    PBCL.TelNo = "91" + txttelno.Text;
                            //}
                            //else
                            //{
                            //    PBCL.TelNo = "964" + txttelno.Text;
                            //}
                            PBCL.Age = int.Parse(txtAge.Text);
                            PBCL.MYD = cmdYMD.SelectedItem.Text;
                            PBCL.Drname = txtDoctorName.Text.Trim();
                            PBCL.Email = txtemail.Text;
                            //PBCL.CollDate = DateTimeConvesion.getDateFromString(Date.getdate().Date.ToString("dd/MM/yyyy")).Date;
                            PBCL.CollDate = Convert.ToDateTime( fromdate.Text);


                            try
                            {
                                string Str1 = Convert.ToString(System.DateTime.Now);
                               // PBCL.CollTime = Convert.ToDateTime(Str1);
                                PBCL.CollTime = Convert.ToDateTime(fromdate.Text);

                            }
                            catch (Exception ex) {
                                WriteErrorLog(ex, "Date Convert");
                            }

                            PBCL.Tests = testCodes;
                            PBCL.TestName = testname;
                            PBCL.TestCharges = Convert.ToSingle(lblTotTestAmt.Text);
                            PBCL.SampleType = sampletypes;

                            //PBCL.RegistratonDateTime = System.DateTime.Now;
                            PBCL.RegistratonDateTime = Convert.ToDateTime(fromdate.Text);
                            PBCL.FinancialYearID = Convert.ToString(FinancialYearTableLogic.getCurrentFinancialYear(Convert.ToInt32(Session["Branchid"])).FinancialYearId);
                            PBCL.Username = Session["username"].ToString();
                            PBCL.Usertype = "patient";
                            if (txtFname.Text.Length > 3)
                            {
                                PBCL.P_PUserName = txtFname.Text.Substring(0, 3) + PID;
                                PBCL.P_PPassword = txtFname.Text.Substring(0, 3) + PID;
                            }
                            else
                            {
                                PBCL.P_PUserName = txtFname.Text.Trim() + PID;
                                PBCL.P_PPassword = txtFname.Text.Trim() + PID;
                            }
                            PBCL.P_PUserName = PBCL.P_PUserName.Replace(" ", "");
                            PBCL.P_PPassword = PBCL.P_PPassword.Replace(" ", "");
                            PBCL.Address1 = txt_address.Text;
                            PBCL.PatientcHistory = "";

                            PBCL.CenterName = txtCenter.Text;
                            Session["CenterCode"] = DrMT_sign_Bal_C.Get_C_Code(txtCenter.Text, Convert.ToInt32(Session["Branchid"]));
                            PBCL.CenterCode = Session["CenterCode"].ToString();
                            {

                            }

                            if (DoctorCode.Length > 1)
                            {
                                PBCL.DoctorCode = DoctorCode[1].ToString().Trim();
                            }
                            PBCL.Drname = DoctorCode[0].ToString();


                            PBCL.ContBarcodeid = BarcodeId;
                            //PBCL.RegistratonDateTime = DateTime.Now;
                            PBCL.RegistratonDateTime = Convert.ToDateTime(fromdate.Text);
                            PBCL.UnitCode = Convert.ToString(Session["UnitCode"]);
                            PBCL.P_DigModule = Convert.ToInt32(Session["DigModule"]);

                            // PBCL.OtherRefDoctor = txt_remark.Text;
                            if (txtBirthdate.Text != "")
                            {
                                PBCL.DateOfBirth = Convert.ToDateTime(txtBirthdate.Text);
                                PBCL.AccDateofBirth = Convert.ToInt32(ViewState["AccDate"]);
                            }
                            else
                            {
                                //PBCL.DateOfBirth = DateTime.Now;
                                PBCL.AccDateofBirth = Convert.ToInt32(ViewState["AccDate"]);
                                if (DateTime.Now.Day == 29 && DateTime.Now.Month == 2)
                                {
                                    ViewState["Today"] = DateTime.Now.Day - 1 + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                                    int age = Convert.ToInt32(txtAge.Text);

                                    ViewState["Year"] = Convert.ToString(DateTime.Now.Year - age);
                                    ViewState["Today"] = DateTime.Now.Day - 1 + "/" + DateTime.Now.Month.ToString("00") + "/" + ViewState["Year"];
                                }
                                else
                                {
                                    ViewState["Today"] = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                                    int age = Convert.ToInt32(txtAge.Text);

                                    ViewState["Year"] = Convert.ToString(DateTime.Now.Year - age);
                                    ViewState["Today"] = DateTime.Now.Day + "/" + DateTime.Now.Month.ToString("00") + "/" + ViewState["Year"];
                                }
                                txtBirthdate.Text = Convert.ToString(ViewState["Today"].ToString());//Date Format- dd/MM/yyyy 
                                PBCL.DateOfBirth = Convert.ToDateTime(txtBirthdate.Text);
                            }

                            if (Convert.ToString(ViewState["VALIDATE"]) == "YES")
                            {
                                //  PBCL.OtherRefDoctor = txt_remark.Text;
                            }
                            else
                            {
                                PBCL.P_remark = txt_remark.Text;
                                PBCL.OtherRefDoctor = "";
                            }
                            PBCL.P_remark = txt_remark.Text;
                            PBCL.PatientcHistory = txt_remark.Text;
                            PBCL.OtherRefDoctor = txt_remark.Text;
                            if (ChkEmergency.Checked == true)
                            {
                                PBCL.Emergencyflag = true;
                            }
                            else
                            {
                                PBCL.Emergencyflag = false;
                            }


                            if (txtWeight.Text != "")
                            {
                                PBCL.P_Weights = txtWeight.Text;
                            }
                            else
                            {
                                PBCL.P_Weights = "";
                            }
                            if (txtHeight.Text != "")
                            {
                                PBCL.P_Heights = txtHeight.Text;
                            }
                            else
                            {
                                PBCL.P_Heights = "";
                            }
                            if (txtDieses.Text != "")
                            {
                                PBCL.P_Disease = txtDieses.Text;
                            }
                            else
                            {
                                PBCL.P_Disease = "";
                            }
                            if (txtLastPeriod.Text != "")
                            {
                                PBCL.P_LastPeriod = txtLastPeriod.Text;
                            }
                            else
                            {
                                PBCL.P_LastPeriod = "";
                            }
                            if (txtSymptoms.Text != "")
                            {
                                PBCL.P_Symptoms = txtSymptoms.Text;
                            }
                            else
                            {
                                PBCL.P_Symptoms = "";
                            }
                            if (txtFSTime.Text != "")
                            {
                                PBCL.P_FSTime = txtFSTime.Text;
                            }
                            else
                            {
                                PBCL.P_FSTime = "";
                            }
                            if (txtTherapy.Text != "")
                            {
                                PBCL.P_Therapy = txtTherapy.Text;
                            }
                            else
                            {
                                PBCL.P_Therapy = "";
                            }
                            string RepStatus = rblretecate.SelectedItem.Text;
                            if (RepStatus == "General")
                            {
                                PBCL.P_SocialMedia = 0;
                            }
                            else if (RepStatus == "Viber")
                            {
                                PBCL.P_SocialMedia = 1;
                            }
                            else
                            {
                                PBCL.P_SocialMedia = 2;
                            }
                            PBCL.P_PatientCardNo = txtpatcardno.Text;
                            PBCL.P_PatientCardExpNo = txtcardexpdate.Text;
                            int count = 0;
                            if (grdTests.Rows.Count > 0)
                            {
                                for (int p = 0; p < grdTests.Rows.Count; p++)
                                {
                                    count = count + 1;
                                    if (grdTests.Rows[p].Cells[3].Text.Trim() == "P")
                                    {
                                        SqlConnection conn = DataAccess.ConInitForDC();
                                        SqlCommand cmd = new SqlCommand("select MTCode from PackmstD where PackageCode='" + grdTests.Rows[p].Cells[1].Text.Trim() + "'", conn);

                                        conn.Open();
                                        SqlDataReader dr = cmd.ExecuteReader();
                                        while (dr.Read())
                                        {
                                            try
                                            {
                                                DrMT_Bal_C DrMT = new DrMT_Bal_C(Session["CenterCode"].ToString(), "CC", Convert.ToInt32(Session["Branchid"]));
                                                MainTest_Bal_C MTB = new MainTest_Bal_C(dr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));

                                                PBCL.TestRate = Convert.ToSingle(grdTests.Rows[p].Cells[4].Text);

                                                PBCL.TestRate = Convert.ToSingle(grdTests.Rows[p].Cells[4].Text);
                                                PBCL.MTCode = dr["MTCode"].ToString();
                                                PBCL.SDCode = MTB.SDCode;

                                                PBCL.UnitCode = Convert.ToString(Session["UnitCode"]);
                                                PBCL.PackageCode = grdTests.Rows[p].Cells[1].Text.Trim();
                                                PBCL.CodeTes = "1";
                                                if (DoctorCode.Length > 1)
                                                {
                                                    PBCL.DoctorCode = DoctorCode[1].ToString().Trim();
                                                }
                                                PBCL.Drname = DoctorCode[0].ToString();
                                                // }

                                                if (PBCL.Drname.ToString() == "")
                                                { PBCL.P_doctoramount = 0; }
                                                else
                                                {
                                                    PBCL.P_doctoramount = PBCL.Get_ShareMst_amount(PBCL.Drname, grdTests.Rows[p].Cells[1].Text.Trim(), Session["Branchid"].ToString());
                                                    if (PBCL.P_perdis > 1 && PBCL.P_doctoramount < 1)
                                                    {
                                                        PBCL.P_doctoramount = (PBCL.TestRate * PBCL.P_perdis / 100);
                                                    }
                                                }
                                                break;


                                            }
                                            catch (Exception exc)
                                            {
                                                WriteErrorLog(exc, "Get Package Rate");
                                                if (exc.Message.Equals("Exception aborted."))
                                                {
                                                    return;
                                                }
                                                else
                                                {
                                                    Response.Cookies["error"].Value = exc.Message;
                                                }
                                            }
                                        }
                                        dr.Close();
                                        conn.Close(); conn.Dispose();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            DrMT_Bal_C DrMT = new DrMT_Bal_C(Session["CenterCode"].ToString(), "CC", Convert.ToInt32(Session["Branchid"]));

                                            MainTest_Bal_C MTB = new MainTest_Bal_C(grdTests.Rows[p].Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));

                                            PBCL.MTCode = grdTests.Rows[p].Cells[1].Text.Trim();

                                            PBCL.TestRate = Convert.ToSingle(grdTests.Rows[p].Cells[4].Text);

                                            PBCL.SDCode = MTB.SDCode;
                                            PBCL.UnitCode = Convert.ToString(Session["UnitCode"]);


                                            PBCL.CodeTes = "2";
                                            if (DoctorCode.Length > 1)
                                            {
                                                PBCL.DoctorCode = DoctorCode[1].ToString().Trim();
                                            }
                                            PBCL.Drname = DoctorCode[0].ToString();

                                            if (PBCL.Drname.ToString() == "")
                                            { PBCL.P_doctoramount = 0; }
                                            else
                                            {
                                                PBCL.P_doctoramount = PBCL.Get_ShareMst_amount(PBCL.Drname, grdTests.Rows[p].Cells[1].Text.Trim(), Session["Branchid"].ToString());
                                                if (PBCL.P_perdis > 1 && PBCL.P_doctoramount < 1)
                                                {
                                                    PBCL.P_doctoramount = (PBCL.TestRate * PBCL.P_perdis / 100);
                                                }
                                            }


                                        }
                                        catch (Exception exc)
                                        {
                                            WriteErrorLog(exc, "Get Test Rate");
                                            if (exc.Message.Equals("Exception aborted."))
                                            {
                                                return;
                                            }
                                            else
                                            {
                                                Response.Cookies["error"].Value = exc.Message;

                                            }
                                        }
                                    }

                                    DataTable tableSam;
                                    if (ViewState["TableTestSample"] != null)
                                    {
                                        tableSam = (DataTable)ViewState["TableTestSample"];

                                        if (tableSam.Rows.Count != 0)
                                        {

                                            PBCL.VCodeTes = "3";

                                            PBCL.Vsampletype = "";
                                            PBCL.Vtestcodes = "";
                                            PBCL.Vtestnames = "";
                                            PBCL.MTCodeNew = grdTests.Rows[p].Cells[1].Text.Trim();
                                            PBCL.BarcodeIDForBar = "";
                                            PBCL.Count = count;
                                            // --------------- RecM Parameter --------------
                                            if (txtpaidamount.Text == "")
                                            {
                                                txtpaidamount.Text = "0";
                                            }
                                              
                                                  PBCL.RecDate = Convert.ToDateTime(Date.getdate().Date.ToString("dd/MM/yyyy"));
                                                  PBCL.BillType = "Cash Bill";
                                                  PBCL.AmtReceived = Convert.ToSingle(txtpaidamount.Text);
                                                  if (txtDisamount.Text != "")
                                                  {
                                                      PBCL.NetPayment = Convert.ToSingle(lbltotalpayment.Text);

                                                      if (Rbldisctype.SelectedValue == "Amt")
                                                      {
                                                          PBCL.Discount = Convert.ToString(txtDisamount.Text);
                                                      }
                                                      else
                                                      {
                                                          PBCL.Discount = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) * Convert.ToSingle(txtDisamount.Text) / 100);
                                                      }
                                                      if (RblDiscgiven.SelectedValue == "1")
                                                      {
                                                          PBCL.P_DrGiven = Convert.ToSingle(0);
                                                          if (txtDisLabgiven.Text != "")
                                                          {
                                                              PBCL.P_LabGiven = Convert.ToSingle(txtDisamount.Text);
                                                          }
                                                          else
                                                          {
                                                              PBCL.P_LabGiven = Convert.ToSingle(0);
                                                          }
                                                          PBCL.P_DiscountPerformTo = 1;
                                                      }
                                                      else if (RblDiscgiven.SelectedValue == "2")
                                                      {
                                                          if (txtDisdrgiven.Text != "")
                                                          {
                                                              PBCL.P_DrGiven = Convert.ToSingle(txtDisamount.Text);
                                                          }
                                                          else
                                                          {
                                                              PBCL.P_DrGiven = Convert.ToSingle(0);
                                                          }

                                                          PBCL.P_LabGiven = Convert.ToSingle(0);
                                                          PBCL.P_DiscountPerformTo = 2;
                                                      }
                                                      if (RblDiscgiven.SelectedValue == "3")
                                                      {
                                                          if (txtDisdrgiven.Text != "" && txtDisLabgiven.Text != "")
                                                          {
                                                              PBCL.P_DrGiven = Convert.ToSingle(txtDisdrgiven.Text);
                                                              PBCL.P_LabGiven = Convert.ToSingle(txtDisLabgiven.Text);
                                                          }
                                                          else
                                                          {
                                                              PBCL.P_DrGiven = Convert.ToSingle(0);
                                                              PBCL.P_LabGiven = Convert.ToSingle(0);
                                                          }

                                                          PBCL.P_DiscountPerformTo = 3;
                                                      }
                                                  }
                                                  else
                                                  {
                                                      PBCL.P_DiscountPerformTo = 0;
                                                      PBCL.P_DrGiven = Convert.ToSingle(0);
                                                      PBCL.P_LabGiven = Convert.ToSingle(0);
                                                      txtDisamount.Text = "0";
                                                      PBCL.NetPayment = Convert.ToSingle(lbltotalpayment.Text);
                                                      PBCL.Discount = "0";
                                                  }
                                                  //if (Convert.ToInt32(ViewState["PID"]) == 0)
                                                  //{
                                                  //    return;
                                                  //}
                                                  //string Patientregno = Cshmst_Bal_C.get_RegNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["PID"]));
                                                  //PBCL.patRegID = Convert.ToString(Patientregno);
                                                  //PBCL.Patientname = txtFname.Text;

                                                  string Patienttest = "";

                                                  PBCL.Patienttest = Patienttest;
                                                  PBCL.P_Disremark = txtdiscountremark.Text;
                                                  //if (Convert.ToInt32(ViewState["PID"]) == 0)
                                                  //{
                                                  //    return;
                                                  //}
                                                 // Obj_CBC.PID = Convert.ToInt32(ViewState["PID"]);
                                                  if (txtpaidamount.Text != "")
                                                      PBCL.AmtPaid = Convert.ToSingle(txtpaidamount.Text);
                                                  else
                                                      PBCL.AmtPaid = 0;
                                                  if (txtBalance.Text != "")
                                                      PBCL.Balance = Convert.ToSingle(txtBalance.Text);
                                                  else
                                                      PBCL.Balance = Convert.ToSingle(lbltotalpayment.Text);
                                                  PBCL.Paymenttype = rblPaymenttype.SelectedItem.Text;
                                                  PBCL.P_OtherChargeRemark = otherchargeRemark.Text;
                                                  if (rblPaymenttype.Items[1].Selected)
                                                  {
                                                      if (txtchequenumber.Text.Trim() != "" && txtbankname.Text.Trim() != "" && txtchequedate.Text.Trim() != "")
                                                      {
                                                          PBCL.ChqNo = txtchequenumber.Text;
                                                          PBCL.ChqDate = DateTimeConvesion.getDateFromString(txtchequedate.Text);
                                                          PBCL.BankName = txtbankname.Text;

                                                      }
                                                      else
                                                      {

                                                          // return;
                                                      }

                                                  }
                                                  else if (rblPaymenttype.Items[2].Selected)
                                                  {
                                                      PBCL.CardNo = txtcardnumber.Text;
                                                      PBCL.CardTransID = txtCardtransactionID.Text;
                                                  }
                                                  else if (rblPaymenttype.Items[3].Selected)
                                                  {
                                                      PBCL.OnlineType = txtonlineType.Text;
                                                      PBCL.OnlinetransID = txtonlinetransid.Text;
                                                  }
                                                 // PBCL.username = Session["username"].ToString();
                                                  PBCL.Othercharges = Convert.ToSingle(txtotherAmt.Text);

                                                  if (rblPaymenttype.Items[1].Selected)
                                                  {
                                                      PBCL.DisFlag = false;
                                                  }
                                                  else
                                                  {
                                                      PBCL.DisFlag = true;
                                                  }
                                                  PBCL.P_DigModule = Convert.ToInt32(Session["DigModule"]);

                                                  PBCL.BillNo = bno;

                                                  PBCL.P_Hstper = Convert.ToSingle(Session["Taxper"]);
                                                  PBCL.P_Hstamount = Convert.ToSingle(txthstamount.Text);
                                              

                                            //-------------- End RecM ======================
                                                  string[] TTime = ddlFromTime.SelectedValue.Split(':');
                                                  string Hr = TTime[0];
                                                  string mint = TTime[1];
                                                  string BDate = Convert.ToString(fromdate.Text);
                                                  BDate = BDate + " " + Hr + ":" + mint + ":" + "00"; ;
                                                  PBCL.Patregdate = Convert.ToDateTime(BDate);
                                                  PBCL.ReportDate = Convert.ToDateTime(BDate);
                                           
                                           // ===============================================
                                            int PID1 = PBCL.Insert_Update_ForPmst_BackDate(Convert.ToInt32(Session["Branchid"]));
                                            PBCL.PIDNew = PID1;
                                            ViewState["PID"] = PBCL.PIDNew;
                                            Session["PID_report"] = Convert.ToInt32(ViewState["PID"]);
                                            Session["RecNo_report"] = 0;
                                            if (PID1 == 0)
                                            {
                                                return;
                                            }


                                        }

                                    }
                                }
                            }


                            sendSMSRegistration(PBCL);



                            //if (txtpaidamount.Text == "")
                            //{
                            //    txtpaidamount.Text = "0";
                            //}
                            //if (txtpaidamount.Text != "")
                            //{
                            //    if ((Convert.ToSingle(txtpaidamount.Text)) > (Convert.ToSingle(lblTotTestAmt.Text) + Convert.ToSingle(txtotherAmt.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)))
                            //    {

                            //        //Label12.Visible = false;
                            //        return;
                            //    }

                            //    // Ledgrmst_Bal_C led = new Ledgrmst_Bal_C();
                            //    PatientBillTransactionTableLogic_Bal_C objBilltrans = new PatientBillTransactionTableLogic_Bal_C();
                            //    if (objBilltrans.billnowithPID(Convert.ToInt32(ViewState["PID"])))
                            //    {

                            //        Cshmst_Bal_C Obj_CBC = new Cshmst_Bal_C();
                            //        Obj_CBC.P_Centercode = Session["CenterCode"].ToString();
                            //        Obj_CBC.RecDate = Convert.ToDateTime(Date.getdate().Date.ToString("dd/MM/yyyy"));
                            //        Obj_CBC.BillType = "Cash Bill";
                            //        Obj_CBC.AmtReceived = Convert.ToSingle(txtpaidamount.Text);
                            //        if (txtDisamount.Text != "")
                            //        {
                            //            Obj_CBC.NetPayment = Convert.ToSingle(lbltotalpayment.Text);

                            //            if (Rbldisctype.SelectedValue == "Amt")
                            //            {
                            //                Obj_CBC.Discount = Convert.ToString(txtDisamount.Text);
                            //            }
                            //            else
                            //            {
                            //                Obj_CBC.Discount = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) * Convert.ToSingle(txtDisamount.Text) / 100);
                            //            }
                            //            if (RblDiscgiven.SelectedValue == "1")
                            //            {
                            //                Obj_CBC.P_DrGiven = Convert.ToSingle(0);
                            //                if (txtDisLabgiven.Text != "")
                            //                {
                            //                    Obj_CBC.P_LabGiven = Convert.ToSingle(txtDisamount.Text);
                            //                }
                            //                else
                            //                {
                            //                    Obj_CBC.P_LabGiven = Convert.ToSingle(0);
                            //                }
                            //                Obj_CBC.P_DiscountPerformTo = 1;
                            //            }
                            //            else if (RblDiscgiven.SelectedValue == "2")
                            //            {
                            //                if (txtDisdrgiven.Text != "")
                            //                {
                            //                    Obj_CBC.P_DrGiven = Convert.ToSingle(txtDisamount.Text);
                            //                }
                            //                else
                            //                {
                            //                    Obj_CBC.P_DrGiven = Convert.ToSingle(0);
                            //                }

                            //                Obj_CBC.P_LabGiven = Convert.ToSingle(0);
                            //                Obj_CBC.P_DiscountPerformTo = 2;
                            //            }
                            //            if (RblDiscgiven.SelectedValue == "3")
                            //            {
                            //                if (txtDisdrgiven.Text != "" && txtDisLabgiven.Text != "")
                            //                {
                            //                    Obj_CBC.P_DrGiven = Convert.ToSingle(txtDisdrgiven.Text);
                            //                    Obj_CBC.P_LabGiven = Convert.ToSingle(txtDisLabgiven.Text);
                            //                }
                            //                else
                            //                {
                            //                    Obj_CBC.P_DrGiven = Convert.ToSingle(0);
                            //                    Obj_CBC.P_LabGiven = Convert.ToSingle(0);
                            //                }

                            //                Obj_CBC.P_DiscountPerformTo = 3;
                            //            }
                            //        }
                            //        else
                            //        {
                            //            Obj_CBC.P_DiscountPerformTo = 0;
                            //            Obj_CBC.P_DrGiven = Convert.ToSingle(0);
                            //            Obj_CBC.P_LabGiven = Convert.ToSingle(0);
                            //            txtDisamount.Text = "0";
                            //            Obj_CBC.NetPayment = Convert.ToSingle(lbltotalpayment.Text);
                            //            Obj_CBC.Discount = "0";
                            //        }
                            //        if (Convert.ToInt32(ViewState["PID"]) == 0)
                            //        {
                            //            return;
                            //        }
                            //        string Patientregno = Cshmst_Bal_C.get_RegNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["PID"]));
                            //        Obj_CBC.patRegID = Convert.ToString(Patientregno);
                            //        Obj_CBC.Patientname = txtFname.Text;

                            //        string Patienttest = "";

                            //        Obj_CBC.Patienttest = Patienttest;
                            //        Obj_CBC.Remark = txtdiscountremark.Text;
                            //        if (Convert.ToInt32(ViewState["PID"]) == 0)
                            //        {
                            //            return;
                            //        }
                            //        Obj_CBC.PID = Convert.ToInt32(ViewState["PID"]);
                            //        if (txtpaidamount.Text != "")
                            //            Obj_CBC.AmtPaid = Convert.ToSingle(txtpaidamount.Text);
                            //        else
                            //            Obj_CBC.AmtPaid = 0;
                            //        if (txtBalance.Text != "")
                            //            Obj_CBC.Balance = Convert.ToSingle(txtBalance.Text);
                            //        else
                            //            Obj_CBC.Balance = Convert.ToSingle(lbltotalpayment.Text);
                            //        Obj_CBC.Paymenttype = rblPaymenttype.SelectedItem.Text;
                            //        Obj_CBC.P_OtherChargeRemark = otherchargeRemark.Text;
                            //        if (rblPaymenttype.Items[1].Selected)
                            //        {
                            //            if (txtchequenumber.Text.Trim() != "" && txtbankname.Text.Trim() != "" && txtchequedate.Text.Trim() != "")
                            //            {
                            //                Obj_CBC.ChqNo = txtchequenumber.Text;
                            //                Obj_CBC.ChqDate = DateTimeConvesion.getDateFromString(txtchequedate.Text);
                            //                Obj_CBC.BankName = txtbankname.Text;

                            //            }
                            //            else
                            //            {

                            //                // return;
                            //            }

                            //        }
                            //        else if (rblPaymenttype.Items[2].Selected)
                            //        {
                            //            Obj_CBC.CardNo = txtcardnumber.Text;
                            //            Obj_CBC.CardTransID = txtCardtransactionID.Text;
                            //        }
                            //        else if (rblPaymenttype.Items[3].Selected)
                            //        {
                            //            Obj_CBC.OnlineType = txtonlineType.Text;
                            //            Obj_CBC.OnlinetransID = txtonlinetransid.Text;
                            //        }
                            //        Obj_CBC.username = Session["username"].ToString();
                            //        Obj_CBC.Othercharges = Convert.ToSingle(txtotherAmt.Text);

                            //        if (rblPaymenttype.Items[1].Selected)
                            //        {
                            //            Obj_CBC.DisFlag = false;
                            //        }
                            //        else
                            //        {
                            //            Obj_CBC.DisFlag = true;
                            //        }
                            //        Obj_CBC.P_DigModule = Convert.ToInt32(Session["DigModule"]);

                            //        Obj_CBC.BillNo = bno;

                            //        Obj_CBC.P_Hstper = Convert.ToSingle(Session["Taxper"]);
                            //        Obj_CBC.P_Hstamount = Convert.ToSingle(txthstamount.Text);

                            //        try
                            //        {
                            //        Obj_CBC.Insert(Convert.ToInt32(Session["Branchid"]));
                            //        }
                            //        catch (Exception exc)
                            //        {
                            //            WriteErrorLog(exc, "Insert Transaction");
                                        
                            //           Response.Cookies["error"].Value = exc.Message;                                        
                            //        }

                            //    }

                            //    Session["PID_report"] = Convert.ToInt32(ViewState["PID"]);
                            //    Session["RecNo_report"] = 0;

                            //}
                        }
                        string PatRegID1 = Cshmst_Bal_C.get_RegNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["PID"]));
                        ObjTB.P_Patregno = PatRegID1;
                        ObjTB.P_FormName = "Patient Registration";
                        ObjTB.P_EventName = "PatientRegistration";
                        ObjTB.P_UserName = Convert.ToString(Session["username"]);
                        ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                        ObjTB.Insert_DailyActivity();

                        if (txtDisamount.Text != "0")
                        {
                            ObjTB.P_Patregno = PatRegID1;
                            ObjTB.P_FormName = "Patient Registration";
                            ObjTB.P_EventName = "Discount Given";
                            ObjTB.P_UserName = Convert.ToString(Session["username"]);
                            ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                            ObjTB.Insert_DailyActivity();
                        }
                        if (txtotherAmt.Text != "0")
                        {
                            ObjTB.P_Patregno = PatRegID1;
                            ObjTB.P_FormName = "Patient Registration";
                            ObjTB.P_EventName = "Other Charges";
                            ObjTB.P_UserName = Convert.ToString(Session["username"]);
                            ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                            ObjTB.Insert_DailyActivity();
                        }

                        // END............
                        Cshmst_supp_Bal_C ObjCSB = new Cshmst_supp_Bal_C();
                        if (Convert.ToString(Session["Phlebotomist"]) == "YES")
                        {
                            // ObjCSB.Insert_Update_Barcode_Patmstd(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), grdTests.Rows[j].Cells[1].Text.Trim(), PatRegID, FID);

                            try
                            {
                            ObjCSB.Insert_Update_Barcode_PhlebotomistReq(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), "");
                            // Patmstd_Main_Bal_C.UpdateStatusByLab_directresult_barcode_Pat(Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), BarcodeGenerate);
                            }
                            catch (Exception exc)
                            {
                                WriteErrorLog(exc, "Phobo Accepted");
                                if (exc.Message.Equals("Exception aborted."))
                                {
                                    return;
                                }
                                else
                                {
                                    Response.Cookies["error"].Value = exc.Message;
                                }
                            }
                        }


                        ViewState["RegN0"] = PatRegID1;
                        // this.btnupload_Click(null, null);
                        // VW_Prescription
                        string AA = "Record Saved. Reg No is " + PatRegID1 + "";
                        Label10.Text = "Record Saved. Reg No is " + PatRegID1 + "/" + txtFname.Text + "";
                        ViewState["Fname"] = txtFname.Text;
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record saved.');", true);
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);

                        Label10.Visible = true;
                        //Label10.Text = "Registration Save Successfully.";
                        ViewState["btnsave"] = "true";
                        ClearControls();
                        grdPackage.DataBind();
                        grdTests.DataBind();
                        lblTotTestAmt.Text = "0";
                        txtFname.Text = "";
                        txtAge.Text = "";

                        txtDisamount.Text = "0";
                        lbltotalpayment.Text = "";
                        txtpaidamount.Text = "";
                        txtBalance.Text = "";
                        txtbankname.Text = "";
                        txtcardnumber.Text = "";
                        txtotherAmt.Text = "0";
                        ViewState["DiscAmt"] = "0";
                        btnSubmit.Enabled = false;

                        lblAmtText.Visible = false;
                        lblTotTestAmt.Visible = false;

                        rblPaymenttype.Items[0].Selected = true;
                        rblPaymenttype.Items[0].Value = "Cash";
                        txtBirthdate.Text = "";
                        txtDoctorName.Text = "";

                        txtDisLabgiven.Text = "";
                        txtDisdrgiven.Text = "";
                        otherchargeRemark.Text = "";
                        txtcardnumber.Text = "";
                        txtcardexpdate.Text = "";
                        btnbarcodeEntry.Enabled = true;
                        btnpatientcard.Enabled = true;
                        btnbprint.Enabled = true;
                        btncapturephoto.Enabled = true;
                        if (Session["usertype"].ToString() == "CollectionCenter" || Session["usertype"].ToString() == "Collection Center")
                        {

                            txtCenter.Enabled = false;
                        }
                        else
                        {

                            txtCenter.Enabled = true;
                        }
                        BindShortcut_test();
                        //for (int i = 0; i < Chkmaintestshort.Items.Count; i++)
                        //{
                        //    Chkmaintestshort.Items[i].Selected = false;
                        //    //if (Chkmaintestshort.Items[i].Selected)
                        //    //{
                        //    //}
                        //}
                        // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'AddPrescription.aspx?PID=" + ViewState["PID"] + "&PatRegID=" + ViewState["RegN0"] + "&Branchid=" + Session["Branchid"].ToString() + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

                        //if (Convert.ToString(ViewState["receipt"]) != "Yes")
                        //{
                        //    Response.Redirect("~/ClickSample.aspx?PatSave='Yes'&PatRegID=" + ViewState["RegN0"] + " &PID=" + ViewState["PID"] + "&FName=" + ViewState["Fname"] + "");
                        //    //TransferAPI_Data();
                        //}
                    }
                    catch (Exception exc)
                    {
                        WriteErrorLog(exc, "Insert Records");
                        if (exc.Message.Equals("Exception aborted."))
                        {
                            return;
                        }
                        else
                        {
                            //  string AA = "Record Saved. Reg No is " + ViewState["RegN0"] + "";
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);
                            // ClearControls();
                            Response.Cookies["error"].Value = exc.Message;
                            // Server.Transfer("~/ErrorMessage.aspx");
                        }
                    }
                }
                else
                {
                    string AA = "Patient already register before 1 min.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);
                }


            
        }        
        catch (Exception ex)
        {
            WriteErrorLog(ex,"Click Sample");
           

        }
    }
    }

    protected void grdPackage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex == -1)
                return;



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
                // Server.Transfer("~/ErrorMessage.aspx");
            }
        }
    }


    void ClearControls()
    {
        txtDisLabgiven.Text = "";
        txtDisdrgiven.Text = "";
        otherchargeRemark.Text = "";
        txtcardnumber.Text = "";
        txtcardexpdate.Text = "";

        cmbInitial.SelectedIndex = 0;
        rblPaymenttype.Items[0].Selected = true;
        txtFname.Text = "";
        txtAge.Text = "";
        txttelno.Text = "";
        cmdYMD.SelectedIndex = 0;

        txtDoctorName.Text = "";
        txt_address.Text = "";

        txt_remark.Text = "";
        txtDisamount.Text = "0";

        ViewState["TableTestSample"] = null;
        ViewState["TableTestWise"] = null;
        txtemail.Text = "";
        chkIsBH.Checked = false;
        BindShortcut_test();

        grdPackage.DataBind();
        grdTests.DataBind();
        lblTotTestAmt.Text = "0";
        txtFname.Text = "";
        txtAge.Text = "";

        txtDisamount.Text = "0";
        lbltotalpayment.Text = "";
        txtpaidamount.Text = "";
        txtBalance.Text = "";
        LblNetAmt.Text = "0";
        txtbankname.Text = "";
        txtcardnumber.Text = "";
        txtdiscountremark.Text = "";
        txtotherAmt.Text = "0";
        ChkEmergency.Checked = false;
        chkIsBH.Checked = false;

        btnSubmit.Enabled = false;

        lblAmtText.Visible = false;
        lblTotTestAmt.Visible = false;
        txtBirthdate.Text = "";
        txtDoctorName.Text = "";
        txtpatcardno.Text = "";

        txtWeight.Text = "";
        txtHeight.Text = "";
        txtDieses.Text = "";
        txtLastPeriod.Text = "";
        txtSymptoms.Text = "";
        txtFSTime.Text = "";
        txtTherapy.Text = "";
        //BindShortcut_test();
    }

    protected void grdTests_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex == -1)
            return;

        DataTable TableTestWise = (DataTable)ViewState["TableTestWise"];
        foreach (DataRow dr in TableTestWise.Rows)
        {
            if (dr["MTCode"].ToString() == grdTests.DataKeys[e.RowIndex].Value.ToString())
            {
                dr.Delete();
                break;
            }
        }
        grdTests.DataSource = TableTestWise;
        grdTests.DataBind();
        lblTotTestAmt.Text = "0";
        ViewState["TableTestWise"] = TableTestWise;
        float amtTestTot = 0f;
        if (grdTests.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                if (gvr.Cells[4].Text.Trim() != "")
                {
                    amtTestTot = amtTestTot + Convert.ToSingle(gvr.Cells[4].Text.Trim());
                }
            }
        }
        lblTotTestAmt.Text = Math.Round(amtTestTot, 0).ToString();
        lbltotalpayment.Text = Math.Round(amtTestTot, 0).ToString();
        txtBalance.Text = Math.Round(amtTestTot, 0).ToString();

        float Bal = Convert.ToSingle(lblTotTestAmt.Text) * Convert.ToSingle(Session["Taxper"]) / 100;
        txthstamount.Text = Convert.ToString(Bal);

        if (ViewState["VALIDATE"] == "NO")
        {
            txtBalance.Text = "0";
            txtpaidamount.Text = Convert.ToString(Math.Round(Convert.ToSingle(lblTotTestAmt.Text) + Convert.ToSingle(txthstamount.Text), 0));
            LblNetAmt.Text = txtBalance.Text;
        }
        else
        {
            //txtpaidamount.Text = "0";
            txtBalance.Text = Convert.ToString(Math.Round(Convert.ToSingle(txtBalance.Text) + Convert.ToSingle(txthstamount.Text), 0));
            LblNetAmt.Text = txtBalance.Text;
        }
        this.txtotherAmt_TextChanged(null, null);
    }

    void AddTestToTable(string MTCode,string TType)
    {
        DataTable TableTestWise = (DataTable)ViewState["TableTestWise"];
        DataRow row;
        row = TableTestWise.NewRow();

        if (TType.Trim() == "P")
        {
            try
            {
                PackageName_Bal_C gn = new PackageName_Bal_C(MTCode);
                row["MTCode"] = gn.PackageCode;
                row["Maintestname"] = gn.PackageName;
                row["Testdiscount"] = "0";
                row["Type"] = "P";

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
                }
            }

        }
        else
        {
            try
            {
                MainTest_Bal_C Bal_C = new MainTest_Bal_C(MTCode, Convert.ToInt32(Session["Branchid"]));
                row["MTCode"] = Bal_C.MTCode;
                row["Maintestname"] = Bal_C.Maintestname;
                row["Testdiscount"] = "0";
                row["Type"] = "T";

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
                }
            }

        }
        if (Session["CenterCode"] != null)
        {
            SpeCh_Bal_C spTable = null;


            DrMT_Bal_C DrMT = new DrMT_Bal_C(Session["CenterCode"].ToString().Trim(), "CC", Convert.ToInt32(Session["Branchid"]));
            try
            {
                spTable = new SpeCh_Bal_C(MTCode, DrMT.ratetypeid, Convert.ToInt32(Session["Branchid"]));
            }
            catch { }
            if (spTable != null)
            {
                if (spTable.Amount == 0)
                {
                    string AA = "Add Test Rate ";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);
                    return;
                }
                else
                {
                    row["Amount"] = spTable.Amount.ToString();
                }
            }
            else
            {

                row["Amount"] = "0";
            }
        }
        else
        {

            row["Amount"] = "0";
        }

        if (row.ItemArray[0].ToString() != "" && row.ItemArray[1].ToString() != "")
            TableTestWise.Rows.Add(row);
        ViewState["TableTestWise"] = TableTestWise;
        // txttests.Focus();
        txtAge.Focus();
    }


    protected void grdTests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;

       
        txtCenter.Enabled = false;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTests(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"].ToString() != null && HttpContext.Current.Session["DigModule"].ToString() != "0")
            sda = new SqlDataAdapter("select PackageCode as MTCode,PackageName as Maintestname, 'P' as Tes from PackMst where (PackageCode like '%" + prefixText + "%' or PackageName like '%" + prefixText + "%') and PackageCode in (select PackageCode from PackmstD where SDCode in (select SDCode from SubDepartment where DigModule ='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "')) UNION " +
                               " select MTCode, Maintestname  ,'T' as Tes from MainTest WHERE ISTestActive=1 and  (MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%') and SDCode in (select SDCode from SubDepartment where DigModule='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "') order by Maintestname ", con);
        else
            sda = new SqlDataAdapter("select PackageCode as MTCode, PackageName as Maintestname , 'P' as Tes from PackMst WHERE PackageCode like '%" + prefixText + "%' or PackageName like '%" + prefixText + "%' UNION " +
                                " select MTCode, Maintestname , 'T' as Tes from MainTest WHERE ISTestActive=1 and (MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%') order by Maintestname ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["MTCode"] + " - " + dr["Maintestname"] + " - " + dr["Tes"], i);
            i++;
        }

        return tests;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] GetCenter(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like N'%" + prefixText + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like N'%" + prefixText + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
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
    public static string[] FillDoctor(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();

        string CNAME = Convert.ToString(HttpContext.Current.Session["CenterCodeTemp"]);
        // string dd = HttpContext.Current.s(txtCenter.Text);
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
            sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR' AND (contactperson=N'" + CNAME + "' or contactperson='') and ( DoctorName like N'%" + prefixText + "%' or DoctorCode like N'%" + prefixText + "%' ) ", con);
        else
            sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR' AND (contactperson=N'" + CNAME + "' or contactperson='') and ( DoctorName like N'%" + prefixText + "%' or DoctorCode like N'%" + prefixText + "%' ) ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["name"] + " = " + dr["DoctorCode"], i);
            i++;
        }

        return tests;
    }

   
    protected void ddlCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string CenterCode = DrMT_sign_Bal_C.Get_C_Code(txtCenter.Text.Trim(), Convert.ToInt32(Session["Branchid"]));
            if (CenterCode == "" || CenterCode == null)
            {
                // Label8.Visible = true;
                txttests.Enabled = false;
                txtCenter.Focus();
                txtCenter.BackColor = Color.Red;
                txtCenter.ForeColor = Color.White;
                string AA = "Select center name";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);
                return;
            }
            else
            {
                //Label8.Visible = false;
                txttests.Enabled = true;
                txtCenter.BackColor = Color.White;
                txtCenter.ForeColor = Color.Black;
            }
            bool cashflag = DrMT_sign_Bal_C.checkcashflag(CenterCode, Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]));
            if (cashflag == false)
            {
                Session["Monthlybill"] = "YES";
                BtnReceipt.Visible = false;
                btnresultentry.Visible = false;
                txtpaidamount.Enabled = false;
                txtDisamount.Enabled = false;
                txtDisamount.Width = 100;

            }
            else
            {
                Session["Monthlybill"] = "No";
            }
            Session["CenterCode"] = CenterCode;

            Session["CenterCodeTemp"] = Patmst_Bal_C.GetConttpersonac(txtCenter.Text, Convert.ToInt32(Session["Branchid"]));

            grdTests.DataSource = null;
            DataTable table1 = new DataTable();

            // Declare DataColumn and DataRow variables.
            DataColumn column1;


            column1 = new DataColumn("MTCode", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Maintestname", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Amount", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Testdiscount", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Type", Type.GetType("System.String"));
            table1.Columns.Add(column1);

            ViewState["TableTestWise"] = table1;

            foreach (GridViewRow gvr in grdTests.Rows)
            {
                AddTestToTable(gvr.Cells[1].Text.Trim(),"T");
                grdTests.DataSource = (DataTable)ViewState["TableTestWise"];
                grdTests.DataBind();
            }
            int tottestamt = 0;
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                tottestamt += Convert.ToInt32(gvr.Cells[4].Text);
            }
            lblTotTestAmt.Text = tottestamt.ToString();

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
                // Server.Transfer("~/ErrorMessage.aspx");
            }
        }
    }
    protected void cmbInitial_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlsex.Items.Clear();
        if (cmbInitial.SelectedItem.Text.Trim() != "Dr." && cmbInitial.SelectedItem.Text.Trim() != "Baby" && cmbInitial.SelectedItem.Text.Trim() != "Baby Of" && cmbInitial.SelectedItem.Text.Trim() != "Doctor" && cmbInitial.SelectedItem.Text.Trim() != "Dr" && cmbInitial.SelectedItem.Text.Trim() != "doctor" && cmbInitial.SelectedItem.Text.Trim() != "dr" && cmbInitial.SelectedItem.Text.Trim() != "baby" && cmbInitial.SelectedItem.Text.Trim() != "baby of" && cmbInitial.SelectedItem.Text.Trim() != "dr.")
        {
            ddlsex.Items.Add(PatientinitialLogic_Bal_C.SelectSex(cmbInitial.SelectedItem.Text));

        }
        else
        {
            ddlsex.Items.Add("Male");
            ddlsex.Items.Add("Female");
        }
         ScriptManager1.SetFocus(cmbInitial);
    }


    protected void txttests_TextChanged(object sender, EventArgs e)
    {
        txtotherAmt.Text = "0";
        txtDisamount.Text = "0";

        DataTable TableTestWise;
        if (ViewState["TableTestWise"] != null)
        {
            TableTestWise = (DataTable)ViewState["TableTestWise"];
        }
        else
        {

            DataTable table1 = new DataTable();
            DataColumn column1;

            column1 = new DataColumn("MTCode", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Maintestname", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Amount", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Testdiscount", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Type", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            ViewState["TableTestWise"] = table1;

            TableTestWise = (DataTable)ViewState["TableTestWise"];
        }
        string[] SplitTestCode;
        string[] TempsplitTestcode;
        TempsplitTestcode = txttests.Text.Split('-');

        if (TempsplitTestcode.Length == 1)
        {
            txttests.Text = txttests.Text + "-" + "-" + "T";
        }
              SplitTestCode = txttests.Text.Split('-');
         if (SplitTestCode[0].ToString().Trim() != "" && SplitTestCode[2].ToString().Trim() != "")
        {

            bool alreadyExist = false;
            foreach (DataRow dr in TableTestWise.Rows)
            {
                if (dr["MTCode"].ToString() == SplitTestCode[0].ToString().ToUpper().Trim())
                {
                    alreadyExist = true;
                    break;
                }

                if (SplitTestCode[2].ToString().Trim() == "P")
                {
                    Package_Bal_C PBJ_PB = new Package_Bal_C();
                    DataTable dt = new DataTable();
                    dt = PBJ_PB.getAllTestCodesPAck(dr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["MTCode"].ToString() == SplitTestCode[0].ToString().ToUpper().Trim())
                        {
                            alreadyExist = true;
                            break;
                        }
                    }
                }
                if (SplitTestCode[2].ToString().Trim() != "P")
                {
                    Package_Bal_C PBJ_PB = new Package_Bal_C();
                    DataTable dt = new DataTable();
                    dt = PBJ_PB.getAllTestCodes(dr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                   
                   // dt = PBJ_PB.getAllTestCodes(SplitTestCode[0].ToString().Trim(), Convert.ToInt32(Session["Branchid"]));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                      // if (dt.Rows[i]["MTCode"].ToString() == dr["MTCode"].ToString().Trim())
                      if (dt.Rows[i]["MTCode"].ToString() == SplitTestCode[0].ToString().ToUpper().Trim())
                        {
                            alreadyExist = true;
                            break;
                        }
                    }
                }
            }

            if (alreadyExist)
            {
                string AA = "Test already added.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);
            }
            else
            {
                SplitTestCode = txttests.Text.Split('-');
                AddTestToTable(SplitTestCode[0].ToString().Trim(), SplitTestCode[2].ToString().Trim());

            }
            grdTests.DataSource = (DataTable)ViewState["TableTestWise"];
            grdTests.DataBind();
        }

        float amtTestTot = 0f;
        if (grdTests.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                if (gvr.Cells[4].Text.Trim() != "")
                {
                    float amt = 0f;
                    try
                    {

                        amt = Convert.ToSingle(gvr.Cells[4].Text.Trim());
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
                            // Server.Transfer("~/ErrorMessage.aspx");
                        }
                    }
                    amtTestTot = amtTestTot + amt;
                }
            }
        }
        lblTotTestAmt.Text = amtTestTot.ToString();
        txttests.Text = "";
        lbltotalpayment.Text = amtTestTot.ToString();
        float Bal = Convert.ToSingle(lbltotalpayment.Text) * Convert.ToSingle(Session["Taxper"]) / 100;
        txthstamount.Text = Convert.ToString(Bal);
        LblNetAmt.Text = Convert.ToString(Math.Round(amtTestTot, 0) + Math.Round(Bal, 0));

        if (ViewState["VALIDATE"] == "YES")
        {
            txtBalance.Text = Convert.ToString(Math.Round(amtTestTot, 0) + Math.Round(Bal, 0));
            // txtpaidamount.Text = "0";
        }
        else
        {
            if (Session["Monthlybill"] == "YES")
            {
                txtpaidamount.Text = "0";
                LblNetAmt.Text = Convert.ToString(Math.Round(amtTestTot, 0) + Math.Round(Bal, 0));
                txtBalance.Text = Convert.ToString(Math.Round(amtTestTot, 0) + Math.Round(Bal, 0));
            }
            else
            {
                LblNetAmt.Text = Convert.ToString(Math.Round(amtTestTot, 0) + Math.Round(Bal, 0));
                txtpaidamount.Text = Convert.ToString(Math.Round(amtTestTot, 0) + Math.Round(Bal, 0));
                txtBalance.Text = "0";

            }
        }
      //  LblNetAmt.Text = txtBalance.Text;
        DataTable TableTestSample = new DataTable();
        DataColumn column;


        column = new DataColumn("SampleType", Type.GetType("System.String"));
        TableTestSample.Columns.Add(column);
        column = new DataColumn("STCODE", Type.GetType("System.String"));
        TableTestSample.Columns.Add(column);
        column = new DataColumn("TestName", Type.GetType("System.String"));
        TableTestSample.Columns.Add(column);
        column = new DataColumn("Testcharges", Type.GetType("System.String"));
        TableTestSample.Columns.Add(column);
        column = new DataColumn("Testdiscount", Type.GetType("System.String"));
        TableTestSample.Columns.Add(column);

        ViewState["TableTestSample"] = TableTestSample;

        if (grdTests.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                if (gvr.Cells[3].Text.Trim() == "P")
                {
                    SqlConnection conn = DataAccess.ConInitForDC();
                    SqlCommand cmd = new SqlCommand("select MTCode,TestName from PackmstD where PackageCode='" + gvr.Cells[1].Text.Trim() + "'", conn);
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {

                        MainTest_Bal_C MTB = new MainTest_Bal_C(sdr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        string sampleTypeOfTest = MTB.SampleType;
                        bool sampleAlreadyExist = false;
                        string sampleTypesString = "";
                        foreach (DataRow dr in TableTestSample.Rows)
                        {
                            if (sampleTypesString == "")
                            {
                                sampleTypesString = dr["SampleType"].ToString();
                            }
                            else
                            {
                                sampleTypesString = sampleTypesString + "," + dr["SampleType"].ToString();
                            }
                        }
                        string[] sampleTypes = sampleTypesString.Split(',');
                        if (sampleTypes.Length > 0)
                        {
                            foreach (string sample in sampleTypes)
                            {
                                if (sample == sampleTypeOfTest)
                                    sampleAlreadyExist = true;
                            }
                        }
                        if (sampleAlreadyExist)
                        {
                            foreach (DataRow dr in TableTestSample.Rows)
                            {
                                if (dr["SampleType"].ToString() == sampleTypeOfTest)
                                {
                                    if (dr["STCODE"].ToString() == "")
                                    {
                                        dr["STCODE"] = sdr["MTCode"].ToString();
                                    }
                                    else
                                    {
                                        if (dr["STCODE"].ToString() != sdr["MTCode"].ToString())
                                        {
                                            dr["STCODE"] = dr["STCODE"] + "," + sdr["MTCode"].ToString();
                                        }
                                    }
                                    if (dr["TestName"].ToString() == "")
                                    {
                                        dr["TestName"] = sdr["TestName"].ToString();
                                    }
                                    else
                                    {
                                        if (dr["TestName"].ToString() != sdr["TestName"].ToString())
                                        {
                                            dr["TestName"] = dr["TestName"] + "," + sdr["TestName"].ToString();
                                        }
                                    }
                                }
                            }

                        }
                        else
                        {
                            DataRow dr = TableTestSample.NewRow();

                            dr["SampleType"] = sampleTypeOfTest;
                            dr["STCODE"] = sdr["MTCode"].ToString();
                            dr["TestName"] = sdr["TestName"].ToString();

                            TableTestSample.Rows.Add(dr);
                        }

                    }
                    sdr.Close();
                    conn.Close(); conn.Dispose();
                }
                else
                {

                    MainTest_Bal_C MTB = new MainTest_Bal_C(gvr.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));
                    string sampleTypeOfTest = MTB.SampleType;
                    bool sampleAlreadyExist = false;
                    string sampleTypesString = "";
                    foreach (DataRow dr in TableTestSample.Rows)
                    {
                        if (sampleTypesString == "")
                        {
                            sampleTypesString = dr["SampleType"].ToString();
                        }
                        else
                        {
                            sampleTypesString = sampleTypesString + "," + dr["SampleType"].ToString();
                        }
                    }
                    string[] sampleTypes = sampleTypesString.Split(',');
                    if (sampleTypes.Length > 0)
                    {
                        foreach (string sample in sampleTypes)
                        {
                            if (sample == sampleTypeOfTest)
                                sampleAlreadyExist = true;//
                            //sampleAlreadyExist = false;
                        }
                    }
                    if (sampleAlreadyExist)
                    {
                        foreach (DataRow dr in TableTestSample.Rows)
                        {
                            if (dr["SampleType"].ToString() == sampleTypeOfTest)
                            {
                                if (dr["STCODE"].ToString() == "")
                                {
                                    dr["STCODE"] = gvr.Cells[1].Text.Trim();
                                }
                                else
                                {
                                    dr["STCODE"] = dr["STCODE"] + "," + gvr.Cells[1].Text.Trim();
                                }
                                if (dr["TestName"].ToString() == "")
                                {
                                    dr["TestName"] = gvr.Cells[2].Text.Trim();
                                }
                                else
                                {
                                    dr["TestName"] = dr["TestName"] + "," + gvr.Cells[2].Text.Trim();
                                }
                            }
                        }

                    }
                    else
                    {
                        DataRow dr = TableTestSample.NewRow();

                        dr["SampleType"] = sampleTypeOfTest;
                        dr["STCODE"] = gvr.Cells[1].Text.Trim();
                        dr["TestName"] = gvr.Cells[2].Text.Trim();
                        dr["Testcharges"] = gvr.Cells[4].Text.Trim();
                        dr["Testdiscount"] = "0";

                        TableTestSample.Rows.Add(dr);
                    }

                }
            }
        }

        grdTests.DataSource = (DataTable)ViewState["TableTestWise"];
        grdTests.DataBind();

        grdPackage.DataSource = (DataTable)ViewState["TableTestSample"];
        grdPackage.DataBind();

        btnSubmit.Enabled = true;

        lblAmtText.Visible = true;
        lblTotTestAmt.Visible = true;
         ScriptManager1.SetFocus(txttests);
        txttests.Focus();

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        //TransferAPI_Data();
        ClearControls();
      //  Response.Redirect("ClickSample.aspx",false);
    }



    protected void ddlDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void txtDoctorName_TextChanged(object sender, EventArgs e)
    {

    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

            StreamReader sr = new StreamReader(httpres.GetResponseStream());

            string results = sr.ReadToEnd();

            sr.Close();
            return results;
        }
        catch
        {
            return "0";
        }
    }

    public void sendSMSRegistration(Patmstd_Bal_C PBC)
    {

        string p_mobileno = Convert.ToString(PBC.TelNo);
        string p_fname = PBC.Initial.Trim() + " " + PBC.Patname + " " + "";
        string PID = Convert.ToString(PBC.PID);
        string Branchid = Convert.ToString(Session["Branchid"]);
        string msg = PBC.GetSMSString("Registration", Convert.ToInt16(Branchid));
        string CounCode = PBC.GetSMSString_CountryCode("Registration", Convert.ToInt16(Branchid));
        if (msg.Trim() != "")
        {
            if (msg.Contains("#Name#"))
            {
                msg = msg.Replace("#Name#", p_fname);
            }
            string PatRegID = PBC.GetRegno(PBC.PIDNew, Convert.ToInt16(Branchid));

            if (msg.Contains("#PatRegID#"))
            {
                msg = msg.Replace("#PatRegID#", PatRegID);
            }
            if (msg.Contains("#UserName#"))
            {
                msg = msg.Replace("#UserName#", PBC.P_PUserName);
                msg = msg.Replace("#Password#", PBC.P_PPassword);

            }
            if (msg.Contains("#Amount#"))
            {
                msg = msg.Replace("#Amount#", lbltotalpayment.Text);   
            }
            if (CounCode.Length == 2)
            {
                if (p_mobileno != CounCode && p_mobileno != "")
                {
                    p_mobileno = p_mobileno.Substring(2, 10);
                    createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                    aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                    string Labname = aut.P_LabSmsName;
                    string SMSapistring = aut.P_LabSmsString;
                    string Labwebsite = aut.P_LabWebsite;

                    SMSapistring = SMSapistring.ToString().Replace("#message#", msg);//msg
                    SMSapistring = SMSapistring.Replace("#Labname#", Labname);
                    SMSapistring = SMSapistring.Replace("#phone#", p_mobileno);
                    try
                    {
                        string url = apicall(SMSapistring);
                        if (url != "0")
                        {
                            // smsevent = true;                        
                            // lblsmsError.Text = "SMS Sended To Patient " + p_fname + "  Mobile Number Is " + p_mobileno + "";
                            // lblsmsError.Visible = true;
                        }
                        else
                        {
                            // lblsmsError.Visible = true;
                            //lblsmsError.Text = "Unable To Send SMS For Patient " + p_fname + " Mobile Number Is " + p_mobileno + "";
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    //lblsmsError.Visible = true;
                    //lblsmsError.Text = "Patient " + p_fname + " Mobile Number Not available ";
                }
            }
            else
            {
                if (p_mobileno != CounCode && p_mobileno != "")
                {
                    p_mobileno = p_mobileno.Substring(3, 10);
                    createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                    aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                    string Labname = aut.P_LabSmsName;
                    string SMSapistring = aut.P_LabSmsString;
                    string Labwebsite = aut.P_LabWebsite;

                    SMSapistring = SMSapistring.ToString().Replace("#message#", msg);
                    SMSapistring = SMSapistring.Replace("#Labname#", Labname);
                    SMSapistring = SMSapistring.Replace("#phone#", p_mobileno);
                    try
                    {
                        string url = apicall(SMSapistring);
                        if (url != "0")
                        {
                            // smsevent = true;                        
                            // lblsmsError.Text = "SMS Sended To Patient " + p_fname + "  Mobile Number Is " + p_mobileno + "";
                            // lblsmsError.Visible = true;
                        }
                        else
                        {
                            // lblsmsError.Visible = true;
                            //lblsmsError.Text = "Unable To Send SMS For Patient " + p_fname + " Mobile Number Is " + p_mobileno + "";
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    //lblsmsError.Visible = true;
                    //lblsmsError.Text = "Patient " + p_fname + " Mobile Number Not available ";
                }
            }
        }

    }

    [WebMethod]
    [ScriptMethod]
    public static string[] GetPatientInfo(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        string collectioncode = HttpContext.Current.Session["CenterCode"].ToString();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
        {
            sda = new SqlDataAdapter("select PPID,MobileNo,Patname from PatMT where (MobileNo like '" + prefixText + "%')  or (Patname like N'%" + prefixText + "%')  or (PPID like '" + prefixText + "%') ", con);
        }
        else
        {
            sda = new SqlDataAdapter("select PPID,MobileNo,Patname from PatMT where (MobileNo like '" + prefixText + "%')  or (Patname like N'%" + prefixText + "%')  or (PPID like '" + prefixText + "%') ", con);
        }
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["PPID"] + " = " + dr["MobileNo"] + " = " + dr["Patname"], i);
            i++;
        }

        return tests;
    }
    protected void rblPaymenttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblPaymenttype.SelectedValue == "Cash")
        {
            CCard.Visible = false;
            CCard1.Visible = false;
            CCheq.Visible = false;//
            CCheq1.Visible = false;
            COnline.Visible = false;
            //CCheq2.Visible = false;


            Rbldisctype.Enabled = true;
            txtpaidamount.Enabled = true;
            txtBalance.Enabled = true;
        }

        else if (rblPaymenttype.SelectedValue == "Cheque")
        {
            CCard.Visible = false;
            COnline.Visible = false;
            CCard1.Visible = false;
            CCheq.Visible = true;//
            CCheq1.Visible = true;
            //CCheq2.Visible = true;

            Rbldisctype.Enabled = true;
            txtpaidamount.Enabled = true;
            txtBalance.Enabled = true;
        }
        else if (rblPaymenttype.SelectedValue == "Card")
        {
            CCard.Visible = true;
            CCard1.Visible = true;
            CCheq.Visible = false;//
            COnline.Visible = false;
            Rbldisctype.Enabled = true;
            txtpaidamount.Enabled = true;
            txtBalance.Enabled = true;
        }
        else if (rblPaymenttype.SelectedValue == "Online")
        {
            COnline.Visible = true;
            CCard.Visible = false;
            CCard1.Visible = false;
            CCheq.Visible = false;//
            Rbldisctype.Enabled = true;
            txtpaidamount.Enabled = true;
            txtBalance.Enabled = true;
        }
        else
        {
            //CCard.Visible = true;
            //CCard1.Visible = true;
            //CCheq.Visible = false;//
            // CCheq1.Visible = false;
            // CCheq2.Visible = false;
            COnline.Visible = false;
            CCard.Visible = false;
            CCard1.Visible = false;
            CCheq.Visible = false;//
            CCheq1.Visible = false;
            Rbldisctype.Enabled = true;
            txtpaidamount.Enabled = true;
            txtBalance.Enabled = true;

        }

    }
    protected void Rbldisctype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lbltotalpayment.Text.Trim() != "")
        {
            if (txtDisamount.Text == "")
            {
                txtDisamount.Text = "0";
            }
            if (txthstamount.Text == "")
            {
                txthstamount.Text = "0";
            }
            txthstamount.Text = "0";
            if (txtpaidamount.Text == "")
            {
                txtpaidamount.Text = "0";
            }
            if (Rbldisctype.SelectedValue == "Amt")
            {
                if (Convert.ToSingle(txtDisamount.Text) > 0)
                {
                   
                    txthstamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) * Convert.ToSingle(Session["Taxper"]) / 100);
                    // txtpaidamount.Text = Convert.ToString((Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(txtDisamount.Text)) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));
                    txtBalance.Text = Convert.ToString((Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(txtDisamount.Text)) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));

                }
                else
                {
                    txthstamount.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) * Convert.ToSingle(Session["Taxper"]) / 100);
                    // txtpaidamount.Text = Convert.ToString((Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(txtDisamount.Text)) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));
                    txtBalance.Text = Convert.ToString((Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(txtDisamount.Text)) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));

                }
                LblNetAmt.Text = txtBalance.Text;
            }
            else
            {
                if (Convert.ToSingle(txtDisamount.Text) > 0)
                {
                    // txtpaidamount.TexttxtBalance = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) * Convert.ToSingle(txtDisamount.Text) / 100);                    
                    // txtpaidamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));

                    txtBalance.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) * Convert.ToSingle(txtDisamount.Text) / 100);
                    txthstamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) * Convert.ToSingle(Session["Taxper"]) / 100);
                    txtBalance.Text = Convert.ToString(Convert.ToSingle(txtBalance.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));
                }
                else
                {
                    txthstamount.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) * Convert.ToSingle(Session["Taxper"]) / 100);
                    // txtpaidamount.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));
                    txtBalance.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));

                }
                LblNetAmt.Text = txtBalance.Text;
            }
            // txtpaidamount.Text = "0";
            //txtBalance.Text = "0";
        }
    }
    protected void txtDisamount_TextChanged(object sender, EventArgs e)
    {
        if (txtDisamount.Text != "")
        {
            txtDisdrgiven.Text = "0";
            txtDisLabgiven.Text = "0";
            txtpaidamount.Text = "0";
           
            RblDiscgiven.Items[0].Selected = true;
            RblDiscgiven.Items[0].Value = "Lab";
            if (txtDisamount.Text != "0")
            {
                RblDiscgiven.Visible = true;
            }
            else
            {
                RblDiscgiven.Visible = false;
            }
            if (lbltotalpayment.Text.Trim() != "")
            {

                if (Rbldisctype.SelectedValue == "Amt")
                {
                    // txtpaidamount.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(txtDisamount.Text));
                    txtBalance.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(txtDisamount.Text));

                    if (txtpaidamount.Text != "")
                    {
                        txthstamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) * Convert.ToSingle(Session["Taxper"]) / 100);
                    }
                    txtBalance.Text = Convert.ToString(Convert.ToSingle(txtBalance.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));

                    // txtpaidamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) + Math.Round( Convert.ToSingle(txthstamount.Text),0));
                    ViewState["DiscAmt"] = Convert.ToSingle(txtDisamount.Text);
                    LblNetAmt.Text = txtBalance.Text;
                }
                else
                {
                    if (Convert.ToSingle(txtDisamount.Text) > 100)
                    {
                        Label10.Visible = true;
                        Label10.Text = "Discount(per) not greater than 100%";
                        txtDisamount.Text = "0";
                    }
                    else
                    {
                        float txtpaidamountT = (Convert.ToSingle(lbltotalpayment.Text) * Convert.ToSingle(txtDisamount.Text) / 100);
                        // txtpaidamount.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(txtpaidamountT));
                        txtBalance.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(txtpaidamountT));
                        if (txtpaidamount.Text != "")
                        {
                            txthstamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) * Convert.ToSingle(Session["Taxper"]) / 100);
                        }
                        //txtpaidamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));

                        txtBalance.Text = Convert.ToString(Convert.ToSingle(txtBalance.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));
                        ViewState["DiscAmt"] = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) * Convert.ToSingle(txtDisamount.Text) / 100);
                    }
                    LblNetAmt.Text = txtBalance.Text;
                }
                disrema.Visible = true;
                // txtBalance.Text = "0";
                // txtpaidamount.Text = "0";
            }
        }

    }
    protected void txtpaidamount_TextChanged(object sender, EventArgs e)
    {
        if (txtpaidamount.Text != "")
        {
            txtBalance.Text = Convert.ToString(((Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(ViewState["DiscAmt"])) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(txtpaidamount.Text));

        }
        else
        {
            txtBalance.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));
        }
        if (Convert.ToSingle(txtBalance.Text) < 0)
        {
            txtpaidamount.Text = "0";
            txtDisamount.Text = "0";
            txtBalance.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) + Math.Round(Convert.ToSingle(txtotherAmt.Text), 0));
        }
       // LblNetAmt.Text = txtBalance.Text;
    }

    protected void txtTestdiscount_TextChanged(object sender, EventArgs e)
    {
        txtDisamount.Text = "0";
        for (int j = 0; j < grdPackage.Rows.Count; j++)
        {
            string disam = (grdPackage.Rows[j].FindControl("txtTestdiscount") as TextBox).Text.ToString();

            if (disam != "")
            {
                if (txtDisamount.Text == "")
                {
                    txtDisamount.Text = "0";
                }
                txtDisamount.Text = Convert.ToString(Convert.ToSingle(txtDisamount.Text) + Convert.ToSingle(disam));
            }
        }
        txtpaidamount.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(txtDisamount.Text));
        if (txtpaidamount.Text != "")
        {
            txthstamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) * Convert.ToSingle(Session["Taxper"]) / 100);
            txtpaidamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));
        }
        ViewState["DiscAmt"] = Convert.ToSingle(txtDisamount.Text);
        txtBalance.Text = "0";
        LblNetAmt.Text = txtBalance.Text;
        Rbldisctype.Enabled = false;
        Rbldisctype.SelectedIndex = 0;
    }
    protected void txtFname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtFname.Text != "")
            {
                string[] PatientMNo = txtFname.Text.Split('=');
                if (PatientMNo.Length > 1)
                {
                    string PermanentID = PatientMNo[0].ToString().Trim();
                    Patmst_Bal_C PBC = new Patmst_Bal_C();
                    DrMT_Bal_C dr = new DrMT_Bal_C();
                    PBC.P_PPID = Convert.ToInt32(PermanentID);
                    PBC.getPermentId();
                    cmbInitial.SelectedItem.Text = PBC.Initial;
                    cmbInitial.DataValueField = PBC.Initial;
                    if (PBC.AccDateofBirth == 1)
                    {
                        int age = 0;
                        age = DateTime.Now.Year - PBC.DateOfBirth.Year;
                        if (DateTime.Now.DayOfYear < PBC.DateOfBirth.DayOfYear)
                            age = age - 1;
                        txtAge.Text = Convert.ToString(age);
                        txtBirthdate.Text = Convert.ToString(PBC.DateOfBirth.ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        txtAge.Text = PBC.Age.ToString();
                    }
                    txtFname.Text = PBC.Patname;
                    dr.GetDoctorName(PBC.RefDr, Convert.ToInt32(Session["Branchid"]));

                    txtDoctorName.Text = dr.Prefix.Trim() + ' ' + dr.Name + '=' + dr.DoctorCode;

                    txtemail.Text = PBC.Email;
                    cmdYMD.SelectedValue = PBC.MYD;
                    txttelno.Text = PBC.Phone;
                    txt_address.Text = PBC.Pataddress;


                    if (cmbInitial.SelectedItem.Text.Trim() != "Dr." && cmbInitial.SelectedItem.Text.Trim() != "Baby" && cmbInitial.SelectedItem.Text.Trim() != "Baby Of" && cmbInitial.SelectedItem.Text.Trim() != "Doctor" && cmbInitial.SelectedItem.Text.Trim() != "Dr" && cmbInitial.SelectedItem.Text.Trim() != "doctor" && cmbInitial.SelectedItem.Text.Trim() != "dr" && cmbInitial.SelectedItem.Text.Trim() != "baby" && cmbInitial.SelectedItem.Text.Trim() != "baby of" && cmbInitial.SelectedItem.Text.Trim() != "dr.")
                    {
                        ddlsex.Items.Add(PatientinitialLogic_Bal_C.SelectSex(cmbInitial.SelectedItem.Text));
                        Cshmst_Bal_C ObjCBC = new Cshmst_Bal_C();
                        dt = new DataTable();
                        dt = ObjCBC.Get_Initial(cmbInitial.SelectedItem.Text, 1);
                        ddlsex.DataSource = dt;

                        ddlsex.DataTextField = "sex";
                        ddlsex.DataValueField = "sex";
                        ddlsex.DataBind();
                        ddlsex.SelectedIndex = 1;
                        // cmbInitial.SelectedIndex = 1;
                        hdnstatus.Value = "1";
                    }
                    else
                    {
                        ddlsex.Items.Add("Male");
                        ddlsex.Items.Add("Female");
                    }
                }
            }
            txtAge.Focus();

        }
        catch (Exception exc)
        { }
    }
    protected void grdPackage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex == -1)
            return;

        DataTable TableTestWise = (DataTable)ViewState["TableTestWise"];
        foreach (DataRow dr in TableTestWise.Rows)
        {
            if (dr["MTCODE"].ToString() == grdPackage.DataKeys[e.RowIndex].Value.ToString())
            {
                dr.Delete();
                break;
            }
        }
        grdTests.DataSource = TableTestWise;
        grdTests.DataBind();
        lblTotTestAmt.Text = "0";
        ViewState["TableTestWise"] = TableTestWise;

    }
    protected void txtTestprofdiscount_TextChanged(object sender, EventArgs e)
    {
        txtDisamount.Text = "0";
        for (int j = 0; j < grdTests.Rows.Count; j++)
        {
            string disam = (grdTests.Rows[j].FindControl("txtTestprofdiscount") as TextBox).Text.ToString();
            string TRate = grdTests.Rows[j].Cells[4].Text;

            if (disam != "")
            {
                if (Convert.ToSingle(disam) > Convert.ToSingle(TRate))
                {
                    (grdTests.Rows[j].FindControl("txtTestprofdiscount") as TextBox).Text = "0";
                }
                else
                {
                    if (txtDisamount.Text == "")
                    {
                        txtDisamount.Text = "0";
                    }
                    txtDisamount.Text = Convert.ToString(Convert.ToSingle(txtDisamount.Text) + Convert.ToSingle(disam));
                }
            }
        }
        txtpaidamount.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(txtDisamount.Text));
        txthstamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) * Convert.ToSingle(Session["Taxper"]) / 100);
        txtpaidamount.Text = Convert.ToString(Convert.ToSingle(txtpaidamount.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0));
        ViewState["DiscAmt"] = Convert.ToSingle(txtDisamount.Text);
        txtBalance.Text = "0";
        Rbldisctype.Enabled = false;
        Rbldisctype.SelectedIndex = 0;
        LblNetAmt.Text = txtBalance.Text;
    }
   

    protected void Chkmaintestshort_SelectedIndexChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < Chkmaintestshort.Items.Count; i++)
        {
            if (Chkmaintestshort.Items[i].Selected)
            {

                //  maintestshort= Chkmaintestshort.Items[i].Text ;
                maintestshort = Chkmaintestshort.Items[i].Value;
                BindShortTest();
            }

        }
        BindShortcut_test();
        
    }
    public void BindShortTest()
    {
        DataTable TableTestWise;
        if (ViewState["TableTestWise"] != null)
        {
            TableTestWise = (DataTable)ViewState["TableTestWise"];
        }
        else
        {

            DataTable table1 = new DataTable();
            DataColumn column1;

            column1 = new DataColumn("MTCode", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Maintestname", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Amount", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Testdiscount", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Type", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            ViewState["TableTestWise"] = table1;

            TableTestWise = (DataTable)ViewState["TableTestWise"];
        }
        string[] SplitTestCode;
        //SplitTestCode = txttests.Text.Split('-');
        SplitTestCode = maintestshort.Split('-');

        if (SplitTestCode[0].ToString().Trim() != "" && SplitTestCode[1].ToString().Trim() != "")
        {

            bool alreadyExist = false;
            foreach (DataRow dr in TableTestWise.Rows)
            {
                if (dr["MTCode"].ToString() == SplitTestCode[0].ToString().Trim())
                {
                    alreadyExist = true;
                    break;
                }

                if (SplitTestCode[1].ToString().Trim() == "P")
                {
                    Package_Bal_C PBJ_PB = new Package_Bal_C();
                    DataTable dt = new DataTable();
                    dt = PBJ_PB.getAllTestCodes(dr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["MTCode"].ToString() == SplitTestCode[0].ToString().Trim())
                        {
                            alreadyExist = true;
                            break;
                        }
                    }
                }
            }

            if (alreadyExist)
            {

            }
            else
            {
                // SplitTestCode = txttests.Text.Split('-');
                SplitTestCode = maintestshort.Split('-');
                AddTestToTable(SplitTestCode[0].ToString().Trim(), SplitTestCode[1].ToString().Trim());

            }
            grdTests.DataSource = (DataTable)ViewState["TableTestWise"];
            grdTests.DataBind();
        }

        float amtTestTot = 0f;
        if (grdTests.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                if (gvr.Cells[4].Text.Trim() != "")
                {
                    float amt = 0f;
                    try
                    {
                        amt = Convert.ToSingle(gvr.Cells[4].Text.Trim());
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
                            // Server.Transfer("~/ErrorMessage.aspx");
                        }
                    }
                    amtTestTot = amtTestTot + amt;
                }
            }
        }
        lblTotTestAmt.Text = amtTestTot.ToString();
        txttests.Text = "";
        lbltotalpayment.Text = amtTestTot.ToString();
        float Bal = Convert.ToSingle(lbltotalpayment.Text) * Convert.ToSingle(Session["Taxper"]) / 100;
        txthstamount.Text = Convert.ToString(Bal);
        txtBalance.Text = Convert.ToString(Math.Round(amtTestTot, 0) + Math.Round(Bal, 0));
        // txtpaidamount.Text = "0";
        if (ViewState["VALIDATE"] == "YES")
        {
            txtBalance.Text = Convert.ToString(Math.Round(amtTestTot, 0) + Math.Round(Bal, 0));
            //txtpaidamount.Text = "0";
        }
        else
        {
            if (Session["Monthlybill"] == "YES")
            {
                txtpaidamount.Text = "0";
                txtBalance.Text = Convert.ToString(Math.Round(amtTestTot, 0) + Math.Round(Bal, 0));
            }
            else
            {
                txtpaidamount.Text = Convert.ToString(Math.Round(amtTestTot, 0) + Math.Round(Bal, 0));
                txtBalance.Text = "0";
            }
        }
        LblNetAmt.Text = txtBalance.Text;
        DataTable TableTestSample = new DataTable();
        DataColumn column;


        column = new DataColumn("SampleType", Type.GetType("System.String"));
        TableTestSample.Columns.Add(column);
        column = new DataColumn("STCODE", Type.GetType("System.String"));
        TableTestSample.Columns.Add(column);
        column = new DataColumn("TestName", Type.GetType("System.String"));
        TableTestSample.Columns.Add(column);
        column = new DataColumn("Testcharges", Type.GetType("System.String"));
        TableTestSample.Columns.Add(column);
        column = new DataColumn("Testdiscount", Type.GetType("System.String"));
        TableTestSample.Columns.Add(column);

        ViewState["TableTestSample"] = TableTestSample;

        if (grdTests.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                if (gvr.Cells[3].Text.Trim() == "P")
                {
                    SqlConnection conn = DataAccess.ConInitForDC();
                    SqlCommand cmd = new SqlCommand("select MTCode,TestName from PackmstD where PackageCode='" + gvr.Cells[1].Text.Trim() + "'", conn);
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {

                        MainTest_Bal_C MTB = new MainTest_Bal_C(sdr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        string sampleTypeOfTest = MTB.SampleType;
                        bool sampleAlreadyExist = false;
                        string sampleTypesString = "";
                        foreach (DataRow dr in TableTestSample.Rows)
                        {
                            if (sampleTypesString == "")
                            {
                                sampleTypesString = dr["SampleType"].ToString();
                            }
                            else
                            {
                                sampleTypesString = sampleTypesString + "," + dr["SampleType"].ToString();
                            }
                        }
                        string[] sampleTypes = sampleTypesString.Split(',');
                        if (sampleTypes.Length > 0)
                        {
                            foreach (string sample in sampleTypes)
                            {
                                if (sample == sampleTypeOfTest)
                                    sampleAlreadyExist = true;
                            }
                        }
                        if (sampleAlreadyExist)
                        {
                            foreach (DataRow dr in TableTestSample.Rows)
                            {
                                if (dr["SampleType"].ToString() == sampleTypeOfTest)
                                {
                                    if (dr["STCODE"].ToString() == "")
                                    {
                                        dr["STCODE"] = sdr["MTCode"].ToString();
                                    }
                                    else
                                    {
                                        dr["STCODE"] = dr["STCODE"] + "," + sdr["MTCode"].ToString();
                                    }
                                    if (dr["TestName"].ToString() == "")
                                    {
                                        dr["TestName"] = sdr["TestName"].ToString();
                                    }
                                    else
                                    {
                                        dr["TestName"] = dr["TestName"] + "," + sdr["TestName"].ToString();
                                    }
                                }
                            }

                        }
                        else
                        {
                            DataRow dr = TableTestSample.NewRow();

                            dr["SampleType"] = sampleTypeOfTest;
                            dr["STCODE"] = sdr["MTCode"].ToString();
                            dr["TestName"] = sdr["TestName"].ToString();

                            TableTestSample.Rows.Add(dr);
                        }

                    }
                    sdr.Close();
                    conn.Close(); conn.Dispose();
                }
                else
                {

                    MainTest_Bal_C MTB = new MainTest_Bal_C(gvr.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));
                    string sampleTypeOfTest = MTB.SampleType;
                    bool sampleAlreadyExist = false;
                    string sampleTypesString = "";
                    foreach (DataRow dr in TableTestSample.Rows)
                    {
                        if (sampleTypesString == "")
                        {
                            sampleTypesString = dr["SampleType"].ToString();
                        }
                        else
                        {
                            sampleTypesString = sampleTypesString + "," + dr["SampleType"].ToString();
                        }
                    }
                    string[] sampleTypes = sampleTypesString.Split(',');
                    if (sampleTypes.Length > 0)
                    {
                        foreach (string sample in sampleTypes)
                        {
                            if (sample == sampleTypeOfTest)
                                //sampleAlreadyExist = true;//
                                sampleAlreadyExist = false;
                        }
                    }
                    if (sampleAlreadyExist)
                    {
                        foreach (DataRow dr in TableTestSample.Rows)
                        {
                            if (dr["SampleType"].ToString() == sampleTypeOfTest)
                            {
                                if (dr["STCODE"].ToString() == "")
                                {
                                    dr["STCODE"] = gvr.Cells[1].Text.Trim();
                                }
                                else
                                {
                                    dr["STCODE"] = dr["STCODE"] + "," + gvr.Cells[1].Text.Trim();
                                }
                                if (dr["TestName"].ToString() == "")
                                {
                                    dr["TestName"] = gvr.Cells[2].Text.Trim();
                                }
                                else
                                {
                                    dr["TestName"] = dr["TestName"] + "," + gvr.Cells[2].Text.Trim();
                                }
                            }
                        }

                    }
                    else
                    {
                        DataRow dr = TableTestSample.NewRow();

                        dr["SampleType"] = sampleTypeOfTest;
                        dr["STCODE"] = gvr.Cells[1].Text.Trim();
                        dr["TestName"] = gvr.Cells[2].Text.Trim();
                        dr["Testcharges"] = gvr.Cells[4].Text.Trim();
                        dr["Testdiscount"] = "0";

                        TableTestSample.Rows.Add(dr);
                    }

                }
            }
        }

        grdTests.DataSource = (DataTable)ViewState["TableTestWise"];
        grdTests.DataBind();

        grdPackage.DataSource = (DataTable)ViewState["TableTestSample"];
        grdPackage.DataBind();

        btnSubmit.Enabled = true;

        lblAmtText.Visible = true;
        lblTotTestAmt.Visible = true;
        // ScriptManager1.SetFocus(txttests);
        // txttests.Focus();

    }
    protected void btnresultentry_Click(object sender, EventArgs e)
    {
        Response.Redirect("Addresult.aspx?PatRegID=" + ViewState["RegN0"] + "&FID=" + ViewState["Fid0"], false);
    }
    public bool validation()
    {
        if (Convert.ToString(Session["ISDemography"]) == "YES")
        {
            if (ViewState["PaymentType"] == "YES")
            {
                // if (txtCenter.Text != "IPD")
                // {
                if (Convert.ToString(Session["Monthlybill"]) != "YES")
                {
                    if (rblPaymenttype.Items[0].Selected || rblPaymenttype.Items[1].Selected || rblPaymenttype.Items[2].Selected || rblPaymenttype.Items[3].Selected)
                    {
                    }
                    else
                    {
                        rblPaymenttype.Focus();
                        rblPaymenttype.BackColor = Color.Red;
                        string AA = "Please select payment type ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);

                        return false;
                    }
                }
                else
                {
                    rblPaymenttype.Items[0].Selected = true;
                }
            }
            if (cmbInitial.SelectedItem.Text == "Select Initial")
            {
                cmbInitial.Focus();
                cmbInitial.BackColor = Color.Red;
                return false;
            }
            else
            {
                cmbInitial.BackColor = Color.White;
                cmbInitial.ForeColor = Color.Black;
            }
            if (txt_address.Text == "")
            {
                txt_address.Focus();
                txt_address.BackColor = Color.Red;
                return false;
            }
            else
            {
                txt_address.BackColor = Color.White;
                txt_address.ForeColor = Color.Black;
            }
            if (txtWeight.Text == "")
            {
                txtWeight.Focus();
                txtWeight.BackColor = Color.Red;
                return false;
            }
            else
            {
                txtWeight.BackColor = Color.White;
                txtWeight.ForeColor = Color.Black;
            }
            if (txtHeight.Text == "")
            {
                txtHeight.Focus();
                txtHeight.BackColor = Color.Red;
                return false;
            }
            else
            {
                txtHeight.BackColor = Color.White;
                txtHeight.ForeColor = Color.Black;
            }
            if (txtDieses.Text == "")
            {
                txtDieses.Focus();
                txtDieses.BackColor = Color.Red;
                return false;
            }
            else
            {
                txtDieses.BackColor = Color.White;
                txtDieses.ForeColor = Color.Black;
            }
            if (txtLastPeriod.Text == "")
            {
                txtLastPeriod.Focus();
                txtLastPeriod.BackColor = Color.Red;
                return false;
            }
            else
            {
                txtLastPeriod.BackColor = Color.White;
                txtLastPeriod.ForeColor = Color.Black;
            }
            if (txtSymptoms.Text == "")
            {
                txtSymptoms.Focus();
                txtSymptoms.BackColor = Color.Red;
                return false;
            }
            else
            {
                txtSymptoms.BackColor = Color.White;
                txtSymptoms.ForeColor = Color.Black;
            }
            if (txtFSTime.Text == "")
            {
                txtFSTime.Focus();
                txtFSTime.BackColor = Color.Red;
                return false;
            }
            else
            {
                txtFSTime.BackColor = Color.White;
                txtFSTime.ForeColor = Color.Black;
            }
            if (txtTherapy.Text == "")
            {
                txtTherapy.Focus();
                txtTherapy.BackColor = Color.Red;
                return false;
            }
            else
            {
                txtTherapy.BackColor = Color.White;
                txtTherapy.ForeColor = Color.Black;
            }


            if (txtFname.Text == "")
            {
                //Label11.Visible = true;
                txtFname.Focus();
                txtFname.BackColor = Color.Red;
                //Label11.Text = "Enter coll code";
                return false;
            }
            else
            {
                txtFname.BackColor = Color.White;
                txtFname.ForeColor = Color.Black;
            }
            if (txtAge.Text == "")
            {
                txtAge.Text = "0";
            }
            if (txtAge.Text == "")
            {
                txtAge.Focus();
                // Label11.Visible = true;
                txtAge.BackColor = Color.Red;
                //Label11.Text = "Enter name";
                return false;
            }
            else
            {
                txtAge.BackColor = Color.White;
                txtAge.ForeColor = Color.Black;
            }
            if (txttelno.Text == "")
            {
                txttelno.Text = "0000000000";
            }
            if (txttelno.Text == "")
            {
                // Label11.Visible = true;
                txttelno.Focus();
                txttelno.BackColor = Color.Red;

                //Label11.Text = "Enter mobile no";
                return false;
            }
            else
            {
                txttelno.BackColor = Color.White;
                txttelno.ForeColor = Color.Black;
            }
            if (txttelno.Text.Length !=10)
            {
                txttelno.Focus();
                txttelno.BackColor = Color.Red;
                //Label11.Text = "Enter mobile no";
                return false;
            }
            else
            {
                txttelno.BackColor = Color.White;
                txttelno.ForeColor = Color.Black;
            }
            if (txtDoctorName.Text == "")
            {
                // Label11.Visible = true;
                txtDoctorName.Focus();
                txtDoctorName.BackColor = Color.Red;
                //Label11.Text = "Enter mobile no";
                return false;
            }
            else
            {
                txtDoctorName.BackColor = Color.White;
                txtDoctorName.ForeColor = Color.Black;
            }
            if (txtDisamount.Text != "0")
            {
                if (txtdiscountremark.Text == "")
                {
                    txtdiscountremark.Focus();
                    txtdiscountremark.BackColor = Color.Red;
                    return false;
                }
                else
                {
                    txtdiscountremark.BackColor = Color.White;
                    txtdiscountremark.ForeColor = Color.Black;
                }
            }
            if (ReportBy.SelectedValue == "2" || ReportBy.SelectedValue == "1")
            {
                if (txtemail.Text == "")
                {
                    txtemail.Focus();
                    txtemail.BackColor = Color.Red;
                    return false;
                }
                else
                {
                    txtemail.BackColor = Color.White;
                    txtemail.ForeColor = Color.Black;
                }
            }
            if (txtemail.Text != "")
            {
                try
                {
                    var emailChecked = new System.Net.Mail.MailAddress(txtemail.Text);
                    txtemail.BackColor = Color.White;
                    txtemail.ForeColor = Color.Black;
                    return true;
                }
                catch
                {
                    txtemail.Focus();
                    txtemail.BackColor = Color.Red;
                    return false;
                }
            }
            if ( Convert.ToSingle( txtBalance.Text) <0)
            {
                txtpaidamount.Text = "0";
                txtpaidamount.Focus();
                txtpaidamount.BackColor = Color.Red;
                return false;
            }
        }
        else
        {
            if (ViewState["PaymentType"] == "YES")
            {
                // if (txtCenter.Text != "IPD")
                // {
                if (Convert.ToString(Session["Monthlybill"]) != "YES")
                {
                    if (rblPaymenttype.Items[0].Selected || rblPaymenttype.Items[1].Selected || rblPaymenttype.Items[2].Selected || rblPaymenttype.Items[3].Selected)
                    {
                    }
                    else
                    {
                        rblPaymenttype.Focus();
                        rblPaymenttype.BackColor = Color.Red;
                        string AA = "Please select payment type ";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);

                        return false;
                    }
                }
                else
                {
                    rblPaymenttype.Items[0].Selected = true;
                }
            }
            if (cmbInitial.SelectedItem.Text == "Select Initial")
            {
                cmbInitial.Focus();
                cmbInitial.BackColor = Color.Red;
                return false;
            }
            else
            {
                cmbInitial.BackColor = Color.White;
                cmbInitial.ForeColor = Color.Black;
            }
            if (txtFname.Text == "")
            {
                //Label11.Visible = true;
                txtFname.Focus();
                txtFname.BackColor = Color.Red;
                //Label11.Text = "Enter coll code";
                return false;
            }
            else
            {
                txtFname.BackColor = Color.White;
                txtFname.ForeColor = Color.Black;
            }
            if (txtAge.Text == "")
            {
                txtAge.Text = "0";
            }
            if (txtAge.Text == "")
            {
                txtAge.Focus();
                // Label11.Visible = true;
                txtAge.BackColor = Color.Red;
                //Label11.Text = "Enter name";
                return false;
            }
            else
            {
                txtAge.BackColor = Color.White;
                txtAge.ForeColor = Color.Black;
            }
            if (txttelno.Text == "")
            {
                txttelno.Text = "0000000000";
            }
            if (txttelno.Text == "")
            {
                // Label11.Visible = true;
                txttelno.Focus();
                txttelno.BackColor = Color.Red;
                //Label11.Text = "Enter mobile no";
                return false;
            }
            else
            {
                txttelno.BackColor = Color.White;
                txttelno.ForeColor = Color.Black;
            }
            if (txttelno.Text.Length != 10)
            {
                txttelno.Focus();
                txttelno.BackColor = Color.Red;
                //Label11.Text = "Enter mobile no";
                return false;
            }
            else
            {
                txttelno.BackColor = Color.White;
                txttelno.ForeColor = Color.Black;
            }
            if (txtDoctorName.Text == "")
            {
                // Label11.Visible = true;
                txtDoctorName.Focus();
                txtDoctorName.BackColor = Color.Red;
                //Label11.Text = "Enter mobile no";
                return false;
            }
            else
            {
                txtDoctorName.BackColor = Color.White;
                txtDoctorName.ForeColor = Color.Black;
            }
            if (txtDisamount.Text != "0")
            {
                if (txtdiscountremark.Text == "")
                {
                    txtdiscountremark.Focus();
                    txtdiscountremark.BackColor = Color.Red;
                    return false;
                }
                else
                {
                    txtdiscountremark.BackColor = Color.White;
                    txtdiscountremark.ForeColor = Color.Black;
                }
            }
            if (ReportBy.SelectedValue == "2" || ReportBy.SelectedValue == "1")
            {
                if (txtemail.Text == "")
                {
                    txtemail.Focus();
                    txtemail.BackColor = Color.Red;
                    return false;
                }
                else
                {
                    txtemail.BackColor = Color.White;
                    txtemail.ForeColor = Color.Black;
                }
            }
            if (txtemail.Text != "")
            {
                try
                {
                    var emailChecked = new System.Net.Mail.MailAddress(txtemail.Text);
                    txtemail.BackColor = Color.White;
                    txtemail.ForeColor = Color.Black;
                    return true;
                }
                catch
                {
                    txtemail.Focus();
                    txtemail.BackColor = Color.Red;
                    return false;
                }
            }
            if (Convert.ToSingle(txtBalance.Text) < 0)
            {
                txtpaidamount.Text = "0";
                txtpaidamount.Focus();
                txtpaidamount.BackColor = Color.Red;
                return false;
            }
        }

        return true;
    }
    protected void BtnReceipt_Click(object sender, EventArgs e)
    {
        ViewState["receipt"] = "Yes";
        this.btnSubmit_Click(null, null);
        string sql1 = "";

        SqlConnection conn1 = DataAccess.ConInitForDC();
        SqlCommand sc1 = conn1.CreateCommand();
        PatSt_Bal_C PBC = new PatSt_Bal_C();
        sc1.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT        RecM.BillNo, patmst.Patregdate AS RecDate, 'Cash Bill' AS BillType, RecM.AmtPaid AS AmtReceived, RecM.DisAmt AS Discount, patmst.TestCharges AS NetPayment, RecM.AmtPaid, "+
                         "   dbo.FUN_GetReceiveAmt_Balance(1, RecM.PID) AS Balance, RecM.username, RecM.OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, " +
                          "  RecM.CardNo AS DoctorName, MainTest.Maintestname, MainTest.MTCode, patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag, patmst.Patusername, patmst.Patpassword, RecM.Comment, "+
                          "  patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, RecM.PaymentType AS UnitCode, RecM.TaxAmount, RecM.TaxPer, dbo.FUN_GetPrintcount(RecM.branchid, RecM.PID) AS PrintCount,  "+
                          "  patmst.OtherRefDoctor, RecM.ReceiptNo, RecM.OtherChargeRemark, RecM.PID "+
                          "  FROM            patmst INNER JOIN "+
                          "  DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN "+
                          "  MainTest INNER JOIN "+
                          "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN "+
                          "  RecM ON patmst.PID = RecM.PID LEFT OUTER JOIN "+
                          "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode " +
                 "   where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and patmstd.PID='" + ViewState["PID"] + "' )";// and Cshmst.BillNo=" + bno + "
        conn1.Open();
        sc1.ExecuteNonQuery();
        conn1.Close(); conn1.Dispose();

        string RID = FinancialYearTableLogic.getPatregister(Convert.ToInt32(Session["Branchid"]));
        if (RID != "0")
        {
            btnbarcodeEntry.Enabled = true;
            btnpatientcard.Enabled = true;
            btnbprint.Enabled = true;

            Session.Add("rptsql", sql1);
            Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PayReceipt.rpt");
            Session["reportname"] = "CashReceipt";
            Session["RPTFORMAT"] = "pdf";

            ReportParameterClass.SelectionFormula = sql1;
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
            Session["PID_report"] = Convert.ToInt32(ViewState["PID"]);
            Session["RecNo_report"] = 0;
            ViewState["receipt"] = "No";
        }
        //  PBC.Update_PrintCount(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32( ViewState["PID"]));
        // Response.Redirect("~/ClickSample.aspx?PatSave='Yes'&PatRegID=" + ViewState["RegN0"] + "");
    }

    public void TransferAPI_Data()
    {
        DataTable dtAPI = new DataTable();
        DataTable dtAPI1 = new DataTable();

        dtAPI1 = Obj_Adt.Get_IsIRD_required();
        if (Convert.ToBoolean(dtAPI1.Rows[0]["IsIRDApprove"]) == true)
        {
            dtAPI = Obj_Adt.Get_Materialize_Data();
            if (dtAPI.Rows.Count > 0)
            {
                for (int j = 0; j < dtAPI.Rows.Count; j++)
                {
                    Obj_Adt.username = "Test_C1";
                    Obj_Adt.password = "test@321";
                    Obj_Adt.seller_pan = Convert.ToString(dtAPI1.Rows[0]["PanNumber"]);
                    Obj_Adt.buyer_pan = "";
                    Obj_Adt.buyer_name = Convert.ToString(dtAPI.Rows[j]["Customer_Name"]);
                    Obj_Adt.fiscal_year = "2074.075";
                    Obj_Adt.total_sales = Math.Round(Convert.ToSingle(dtAPI.Rows[j]["Total_Amount"]), 2); ;
                    Obj_Adt.taxable_sales_hst = Math.Round(Convert.ToSingle(dtAPI.Rows[j]["Taxable_Amount"]), 2);
                    Obj_Adt.taxable_sales_vat = 0;
                    Obj_Adt.vat = 0;
                    Obj_Adt.excisable_amount = 0;
                    Obj_Adt.excise = 0;
                    Obj_Adt.invoice_number = Convert.ToString(dtAPI.Rows[j]["Bill_No"]);
                    Obj_Adt.invoice_date = Convert.ToString(dtAPI.Rows[j]["Bill_Date"]);
                    Obj_Adt.invoice_date = Obj_Adt.invoice_date.Replace('-', '.');
                    Obj_Adt.hst = Math.Round(Convert.ToSingle(dtAPI.Rows[j]["Tax_Amount"]), 2);
                    Obj_Adt.amount_for_esf = 0;
                    Obj_Adt.esf = 0;
                    Obj_Adt.export_sales = 0;
                    Obj_Adt.tax_exempted_sales = 0;
                    Obj_Adt.isrealtime = true;
                    Obj_Adt.datetimeClient = DateTime.Now;
                    Obj_Adt.Sr_no = Convert.ToInt32(dtAPI.Rows[j]["Sr_no"]);
                    SendAPI(Obj_Adt);
                }

            }
        }
    }
    public void SendAPI(API_DataTransfer_C P)
    {
        try
        {
            // Obj_Adt.Update_APITransfer_status(Convert.ToInt32(Obj_Adt.Sr_no), "");
            string url = "http://202.166.207.75:9050/api/bill";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json;charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json;charset=utf-8";
            using (var streamwriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string loginjson = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(P);
                streamwriter.Write(loginjson);
                streamwriter.Flush();
                streamwriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Obj_Adt.Update_APITransfer_status(Convert.ToInt32(Obj_Adt.Sr_no), result);

                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btnbarcodeEntry_Click(object sender, EventArgs e)
    {
        try
        {
            
            Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
            PatSt_Bal_C objPrintStatus = new PatSt_Bal_C();
            int PID = Convert.ToInt32(ViewState["PID"]);
            int branchid = Convert.ToInt32(Session["Branchid"].ToString());
            string FID = Convert.ToString(ViewState["Fid0"]);
            string PatRegID = Convert.ToString(ViewState["RegN0"]);
            Cshmst_supp_Bal_C ObjCSB = new Cshmst_supp_Bal_C();
            
            ObjCSB.Insert_Update_Barcode_PhlebotomistReq(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), "");
           

            string BarCode_ID = "";
            string subdept = "";
            string CDate = DateTime.Now.ToShortDateString();
            dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
            if (dt.Rows.Count > 0)
            {
                subdept = Convert.ToString(dt.Rows[0]["subdept"]);
            }
           // objPrintStatus.AlterView_Barcode_Temp_Deptwise(subdept, PID);

          //  objPrintStatus.AlterViewPrintBarcode_deptwise_Registration(branchid, PatRegID, FID, BarCode_ID, PID);
            //=======================DeptBarCode=========
            if (Convert.ToString(ViewState["BarCodeImageReq"]) == "YES")
            {
                BarCodeImage(PID);
            }
            objPrintStatus.AlterView_Barcode_Temp_Deptwise_Clicksample(subdept, PID, CDate);

            objPrintStatus.AlterViewPrintBarcode_deptwise_Registration_SampleClick(branchid, PatRegID, FID, BarCode_ID, PID, CDate);
            //===========================================

            string formula = "", selectonFormula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();

            CR.Load(Server.MapPath("~//DiagnosticReport//Rpt_PrintBarcode_deptwise.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dtB = new DataTable();
            //  DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_patstkvw_Deptwise where PID='" + PID + "'   ", con);

            sda.Fill(dtB);
            CR.SetDataSource((System.Data.DataTable)dtB);

            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "DeptBarCode" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patstkvw_Deptwise.PID}='" + PID + "' ";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }

            exportedpath = filename1;
            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

            CR.Close();
            CR.Dispose();



            GC.Collect();

            if (dtB.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "DeptBarCode" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label10.ForeColor = System.Drawing.Color.Red;
                Label10.Text = "Barcode Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "DeptBarCode" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "DeptBarCode" + ".pdf");

            string[] FilePathSplitOrg = OrgFile.Split('$');
            string[] FilePathSplitDup = DupFile.Split('$');

            if (FilePathSplitOrg[1] != FilePathSplitDup[1])
            {

                foreach (string file in Directory.GetFiles(path))
                {
                    string[] NewFile = file.Split('$');
                    if (FilePathSplitOrg[1] != NewFile[1])
                    {
                        File.Delete(file);
                    }
                }
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('ReportTransfer.aspx?PID=" + PID + "&TypeB=DeptBarCode','_newtab');", true);
     
        //try
        //{
            
        //    Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
        //    PatSt_Bal_C objPrintStatus = new PatSt_Bal_C();
        //    int PID = Convert.ToInt32(ViewState["PID"]);
        //    int branchid = Convert.ToInt32(Session["Branchid"].ToString());
        //    string FID = Convert.ToString(ViewState["Fid0"]);
        //    string PatRegID = Convert.ToString(ViewState["RegN0"]);
        //    Cshmst_supp_Bal_C ObjCSB = new Cshmst_supp_Bal_C();
        //    //if (Convert.ToString(Session["Phlebotomist"]) == "YES")
        //    //{
        //    // ObjCSB.Insert_Update_Barcode_Patmstd(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), grdTests.Rows[j].Cells[1].Text.Trim(), PatRegID, FID);

        //    ObjCSB.Insert_Update_Barcode_PhlebotomistReq(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), "");
        //    // Patmstd_Main_Bal_C.UpdateStatusByLab_directresult_barcode_Pat(Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), BarcodeGenerate);

        //    // }

        //    ObjTB.P_Patregno = Convert.ToString(ViewState["RegN0"]); ;
        //    ObjTB.P_FormName = "Patient Registration";
        //    ObjTB.P_EventName = "Dept wisp Barcode Print";
        //    ObjTB.P_UserName = Convert.ToString(Session["username"]);
        //    ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
        //    ObjTB.Insert_DailyActivity();

        //    string BarCode_ID = "";
        //    string subdept = "";
        //    dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        //    if (dt.Rows.Count > 0)
        //    {
        //        subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        //    }
        //    objPrintStatus.AlterView_Barcode_Temp_Deptwise(subdept, PID);

        //    objPrintStatus.AlterViewPrintBarcode_deptwise_Registration(branchid, PatRegID, FID, BarCode_ID, PID);

        //    Session.Add("rptsql", "");
        //    Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PrintBarcode_deptwise.rpt");
        //    Session["reportname"] = "PrintBarcode_dept";
        //    Session["RPTFORMAT"] = "pdf";

        //    ReportParameterClass.SelectionFormula = "";
        //    string close = "<script language='javascript'>javascript:OpenReport();</script>";
        //    Type title1 = this.GetType();
        //    Page.ClientScript.RegisterStartupScript(title1, "", close);
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

    protected void txtotherAmt_TextChanged(object sender, EventArgs e)
    {
        if (txtotherAmt.Text != "")
        {
            if (txtDisamount.Text == "0")
            {
                ViewState["DiscAmt"] = "0";
            }
            if (Convert.ToSingle(txtotherAmt.Text) > 0)
            {
                lbltotalpayment.Text = Convert.ToString(Convert.ToSingle(lblTotTestAmt.Text) + Convert.ToSingle(txtotherAmt.Text));
                if (txtpaidamount.Text != "")
                {
                    txtBalance.Text = Convert.ToString(((Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(ViewState["DiscAmt"])) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(txtpaidamount.Text));
                }
                else
                {
                    txtBalance.Text = Convert.ToString(((Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(ViewState["DiscAmt"])) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)));

                }
              //  LblNetAmt.Text = txtBalance.Text;
                LblNetAmt.Text =Convert.ToString( Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(ViewState["DiscAmt"]));
                otherchargeRemark.Focus();
            }
            else
            {
                lbltotalpayment.Text = Convert.ToString(Convert.ToSingle(lblTotTestAmt.Text));
                LblNetAmt.Text = Convert.ToString(Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(ViewState["DiscAmt"]));
            if (txtpaidamount.Text != "")
            {
                txtBalance.Text = Convert.ToString(((Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(ViewState["DiscAmt"])) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(txtpaidamount.Text));
            }
            else
            {
                txtBalance.Text = Convert.ToString(((Convert.ToSingle(lbltotalpayment.Text) - Convert.ToSingle(ViewState["DiscAmt"])) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)));

            }
           // LblNetAmt.Text = txtBalance.Text;
            }
        }
    }

    protected void btnbprint_Click(object sender, EventArgs e)
    {
        Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
        DataTable dt = new DataTable();
        PatSt_Bal_C objPrintStatus = new PatSt_Bal_C();
        int PID = Convert.ToInt32(ViewState["PID"]);
        int branchid = Convert.ToInt32(Session["Branchid"].ToString());
        string FID = Convert.ToString(ViewState["Fid0"]);
        string PatRegID = Convert.ToString(ViewState["RegN0"]);
        string BarCode_ID = "";
        string subdept = "";
        dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        }

        Cshmst_supp_Bal_C ObjCSB = new Cshmst_supp_Bal_C();
        ObjCSB.Insert_Update_Barcode_PhlebotomistReq(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), "");

       
       //==================================================
             
        objPrintStatus.AlterView_Barcode_Temp(subdept, PID);
        dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        }

        //objPrintStatus.AlterView_Barcode_Temp_Direct(subdept, PID);
        
       // objPrintStatus.AlterViewPrintBarcode_Direct(branchid, PatRegID, FID, BarCode_ID, PID);
        //============================================
        if (Convert.ToString( ViewState["BarCodeImageReq"]) == "YES")
        {
            BarCodeImage(PID);
        }
        VW_DescriptiveViewLogic.SP_GetAlterView_BarCode(Convert.ToInt32(Session["Branchid"]), Convert.ToString(ViewState["PID"]), "0", subdept, BarCode_ID);

        string formula = "", selectonFormula = "";
        selectonFormula = ReportParameterClass.SelectionFormula;
        ReportDocument CR = new ReportDocument();

        CR.Load(Server.MapPath("~//DiagnosticReport//Rpt_PrintBarcode.rpt"));
        SqlConnection con = DataAccess.ConInitForDC();

        SqlDataAdapter sda = null;
        DataTable dtB = new DataTable();
        //  DataSet1 dst = new DataSet1();
        sda = new SqlDataAdapter("select * from VW_patstkvw where PID='" + PID + "'   ", con);

        sda.Fill(dtB);
        CR.SetDataSource((System.Data.DataTable)dtB);

        string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
        string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "BarCode" + ".pdf");
        System.IO.File.WriteAllText(filename1, "");
        string exportedpath = "", selectionFormula = "";
        ReportParameterClass.SelectionFormula = "{VW_patstkvw.PID}='" + PID + "' ";
        ReportDocument crReportDocument = null;
        if (CR != null)
        {
            crReportDocument = (ReportDocument)CR;
        }

        exportedpath = filename1;
        cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

        CR.Close();
        CR.Dispose();



        GC.Collect();

        if (dtB.Rows.Count == 0)
        {
            string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "BarCode" + ".pdf");
            FileInfo fi = new FileInfo(filepath11);
            fi.Delete();
            Label10.ForeColor = System.Drawing.Color.Red;
            Label10.Text = "BarCode Not Generated, Please Generate Once Again ";
            return;
        }
        string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "BarCode" + ".pdf");
        string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "BarCode" + ".pdf");

        string[] FilePathSplitOrg = OrgFile.Split('$');
        string[] FilePathSplitDup = DupFile.Split('$');

        if (FilePathSplitOrg[1] != FilePathSplitDup[1])
        {

            foreach (string file in Directory.GetFiles(path))
            {
                string[] NewFile = file.Split('$');
                if (FilePathSplitOrg[1] != NewFile[1])
                {
                    File.Delete(file);
                }
            }
        }
        Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('ReportTransfer.aspx?PID=" + PID + "&TypeB=BarCode','_newtab');", true);
     
        //Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
        //DataTable dt = new DataTable();
        //PatSt_Bal_C objPrintStatus = new PatSt_Bal_C();
        //int PID = Convert.ToInt32(ViewState["PID"]);
        //int branchid = Convert.ToInt32(Session["Branchid"].ToString());
        //string FID = Convert.ToString(ViewState["Fid0"]);
        //string PatRegID = Convert.ToString(ViewState["RegN0"]);
        //string BarCode_ID = "";
        //string subdept = "";
        //dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        //if (dt.Rows.Count > 0)
        //{
        //    subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        //}

        //ObjTB.P_Patregno = Convert.ToString(ViewState["RegN0"]); ;
        //ObjTB.P_FormName = "Patient Registration";
        //ObjTB.P_EventName = "Sample Barcode Print";
        //ObjTB.P_UserName = Convert.ToString(Session["username"]);
        //ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
        //ObjTB.Insert_DailyActivity();
        //Cshmst_supp_Bal_C ObjCSB = new Cshmst_supp_Bal_C();
        ////if (Convert.ToString(Session["Phlebotomist"]) == "YES")
        ////{
        //// ObjCSB.Insert_Update_Barcode_Patmstd(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), grdTests.Rows[j].Cells[1].Text.Trim(), PatRegID, FID);

        //ObjCSB.Insert_Update_Barcode_PhlebotomistReq(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), "");
        //// Patmstd_Main_Bal_C.UpdateStatusByLab_directresult_barcode_Pat(Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), BarcodeGenerate);

        //// }
        //objPrintStatus.AlterView_Barcode_Temp(subdept, PID);


        //dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        //if (dt.Rows.Count > 0)
        //{
        //    subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        //}

        //objPrintStatus.AlterView_Barcode_Temp_Direct(subdept, PID);
        ////}

        ////objPrintStatus.AlterViewPrintBarcode_Temp(branchid, PatRegID, FID, BarCode_ID, PID);
        //objPrintStatus.AlterViewPrintBarcode_Direct(branchid, PatRegID, FID, BarCode_ID, PID);

        //Session.Add("rptsql", "");
        //Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PrintBarcode.rpt");
        //Session["reportname"] = "PrintBarcode";
        //Session["RPTFORMAT"] = "pdf";

        //ReportParameterClass.SelectionFormula = "";
        //string close = "<script language='javascript'>javascript:OpenReport();</script>";
        //Type title1 = this.GetType();
        //Page.ClientScript.RegisterStartupScript(title1, "", close);
        // }
    }

    protected void btnpatientcard_Click(object sender, EventArgs e)
    {
        string sql1 = "";
        SqlConnection conn1 = DataAccess.ConInitForDC();
        SqlCommand sc1 = conn1.CreateCommand();
        Patmst_Bal_C PBC = new Patmst_Bal_C();
        //sc1.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT dbo.Cshmst.BillNo, dbo.Cshmst.BillDate, dbo.Cshmst.BillType, dbo.Cshmst.AmtReceived, " +
        //                  " dbo.Cshmst.Discount, dbo.Cshmst.NetPayment, dbo.Cshmst.AmtPaid, dbo.Cshmst.Balance, " +
        //                  " dbo.Cshmst.username,dbo.Cshmst.OtherCharges,dbo.patmst.RegNo, dbo.patmst.intial, dbo.patmst.FirstName, " +
        //                  "  dbo.patmst.sex, dbo.patmst.Age, dbo.patmst.DocName, " +
        //                  " dbo.patmst.TelNo, dbo.DrMT.DoctorCode, dbo.DrMT.DoctorName, dbo.MainTest.Maintestname, dbo.MainTest.MTCode, " +
        //                  " dbo.patmstd.TestRate, dbo.PackMst.PackageName, dbo.patmstd.PackageCode, dbo.Cshmst.DisFlag, " +
        //                  " dbo.patmst.PUserName, dbo.patmst.PPassWord, dbo.Cshmst.Remark,dbo.patmst.MDY,dbo.patmst.Remark AS PatientRemark,dbo.patmst.patient_addr,dbo.patmst.PPID ,dbo.patmst.LBcode , Cshmst.TaxAmount, Cshmst.TaxPer, 0 as PrintCount FROM dbo.patmst INNER JOIN " +
        //                  " dbo.DrMT ON dbo.patmst.CenterCode = dbo.DrMT.DoctorCode AND  " +
        //                  " dbo.patmst.branchid = dbo.DrMT.branchid INNER JOIN " +
        //                  " dbo.Cshmst INNER JOIN dbo.MainTest INNER JOIN " +
        //                  " dbo.patmstd ON dbo.MainTest.MTCode = dbo.patmstd.MTCode AND " +
        //                  " dbo.MainTest.branchid = dbo.patmstd.branchid ON dbo.Cshmst.PID = dbo.patmstd.PID AND " +
        //                  " dbo.Cshmst.branchid = dbo.patmstd.branchid ON dbo.patmst.PID = dbo.patmstd.PID AND " +
        //                  " dbo.patmst.branchid = dbo.patmstd.branchid LEFT OUTER JOIN " +
        //                  " dbo.PackMst ON dbo.patmstd.branchid = dbo.PackMst.branchid AND " +
        //                  " dbo.patmstd.PackageCode = dbo.PackMst.PackageCode where DrMT.DrCheck_flag='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and patmstd.PID='" + ViewState["PID"] + "' )";// and Cshmst.BillNo=" + bno + "


        // PBC.P_PPID = Convert.ToInt32(PermanentID);



        PBC.PID = Convert.ToInt32(ViewState["PID"]);
        PBC.get_PermentId();

        ViewState["PPID"] = PBC.P_PPID;

        ObjTB.P_Patregno = Convert.ToString(ViewState["RegN0"]); ;
        ObjTB.P_FormName = "Patient Registration";
        ObjTB.P_EventName = "Card Print";
        ObjTB.P_UserName = Convert.ToString(Session["username"]);
        ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
        ObjTB.Insert_DailyActivity();

        //sc1.CommandText = "ALTER VIEW [dbo].[VW_PatientCard] AS SELECT top(99.99) percent   dbo.Cshmst.BillNo, dbo.Cshmst.RecDate, dbo.Cshmst.BillType,   dbo.Cshmst.AmtReceived,  " +
        //      "  dbo.Cshmst.Discount, dbo.Cshmst.NetPayment, RecM.AmtPaid AS AmtPaid, dbo.Cshmst.Balance,  dbo.Cshmst.username,  " +
        //      "  dbo.Cshmst.OtherCharges,dbo.patmst.PatRegID, dbo.patmst.intial, dbo.patmst.Patname,   dbo.patmst.sex,  dbo.patmst.Age, " +
        //      "  dbo.patmst.Drname,  dbo.patmst.TelNo, dbo.DrMT.DoctorCode, dbo.DrMT.DoctorName, dbo.MainTest.Maintestname as Maintestname,    " +
        //      "  dbo.MainTest.MTCode,  dbo.patmstd.TestRate, dbo.PackMst.PackageName, dbo.patmstd.PackageCode, dbo.Cshmst.DisFlag, " +
        //      "  dbo.patmst.Patusername, dbo.patmst.Patpassword, dbo.Cshmst.Comment,dbo.patmst.MDY,dbo.patmst.Remark AS PatientRemark, " +
        //      "  dbo.patmst.Pataddress,dbo.patmst.PPID ,dbo.patmst.UnitCode , Cshmst.TaxAmount, Cshmst.TaxPer,   " +
        //      "  RecM.PrintCount as PrintCount,patmst.Email as EmailID FROM         patmst INNER JOIN    DrMT ON patmst.CenterCode = DrMT.DoctorCode AND   " +
        //      "  patmst.Branchid = DrMT.Branchid INNER JOIN   Cshmst INNER JOIN   MainTest INNER JOIN   patmstd ON  " +
        //      "  MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON Cshmst.PID = patmstd.PID AND   " +
        //      "  Cshmst.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid  " +
        //      "  INNER JOIN    RecM ON Cshmst.PID = RecM.PID AND Cshmst.BillNo = RecM.BillNo LEFT OUTER JOIN    " +
        //      "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode where  patmst.branchid=" + Session["Branchid"].ToString() + " and patmst.PPID='" + ViewState["PPID"] + "' and patmst.PatRegID='" + ViewState["RegN0"] + "'  order by Cshmst.billno desc  ";// Request.QueryString["PatRegID"]


        sc1.CommandText = "ALTER VIEW [dbo].[VW_PatientCard] AS SELECT        TOP (99.99) PERCENT RecM.BillNo, dbo.RecM.billdate as RecDate, dbo.RecM.PaymentType as BillType, RecM.AmtPaid AS AmtReceived, RecM.DisAmt AS Discount, dbo.patmst.TestCharges AS NetPayment, RecM.AmtPaid, RecM.BalAmt AS Balance, " +
                       "  RecM.username, RecM.OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, DrMT.DoctorName, MainTest.Maintestname, MainTest.MTCode, " +
                       "  patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag, patmst.Patusername, patmst.Patpassword, RecM.Comment, patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, " +
                       "  patmst.UnitCode, RecM.TaxAmount, RecM.TaxPer, RecM.PrintCount, patmst.Email AS EmailID " +
                       "  FROM            RecM INNER JOIN " +
                       "  patmst INNER JOIN " +
                       "  DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN " +
                       "  MainTest INNER JOIN " +
                       "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid ON RecM.PID = patmst.PID AND " +
                       "  RecM.branchid = patmst.Branchid LEFT OUTER JOIN " +
                       "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode where  patmst.branchid=" + Session["Branchid"].ToString() + " and patmst.PPID='" + ViewState["PPID"] + "'  and patmst.PatRegID='" + ViewState["RegN0"] + "'  order by RecM.billno desc  ";// and Cshmst.BillNo=" + bno + " DrMT.DrCheck_flag='CC' and

        conn1.Open();
        sc1.ExecuteNonQuery();
        conn1.Close(); conn1.Dispose();


        Session.Add("rptsql", sql1);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PatientCard.rpt");
        Session["reportname"] = "PatientCard";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql1;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
        Session["PID_report"] = Convert.ToInt32(ViewState["PID"]);
        Session["RecNo_report"] = 0;
    }
    protected void txtAge_TextChanged(object sender, EventArgs e)
    {
        ViewState["Today"] = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
        int age = Convert.ToInt32(txtAge.Text);

        if (DateTime.Now.Day == 29 && DateTime.Now.Month == 2)
        {
            ViewState["Year"] = Convert.ToString(DateTime.Now.Year - age);
            ViewState["Today"] = DateTime.Now.Day-1 + "/" + DateTime.Now.Month.ToString("00") + "/" + ViewState["Year"];
        }
        else
        {
            ViewState["Year"] = Convert.ToString(DateTime.Now.Year - age);
            ViewState["Today"] = DateTime.Now.Day + "/" + DateTime.Now.Month.ToString("00") + "/" + ViewState["Year"];
        }

        // txtBirthdate.Text = Convert.ToString(ViewState["Today"].ToString());//Date Format- dd/MM/yyyy 
        ViewState["AccDate"] = 0;
       // txtBirthdate.Focus();
        txtDoctorName.Focus();
    }
    protected void txtBirthdate_TextChanged(object sender, EventArgs e)
    {
        int intYear, intMonth, intDays;
        DateTime Birthday = Convert.ToDateTime(txtBirthdate.Text);
        intYear = Birthday.Year;
        intMonth = Birthday.Month;
        intDays = Birthday.Day;

        DateTime dtt = Convert.ToDateTime(txtBirthdate.Text);

        DateTime td = DateTime.Now;
        int Leap_Year = 0;
        for (int i = dtt.Year; i < td.Year; i++)
        {
            if (DateTime.IsLeapYear(i))
            {
                ++Leap_Year;
            }
        }
        TimeSpan timespan = td.Subtract(Birthday);
        intDays = timespan.Days - Leap_Year;
        int intResult = 0;
        intYear = Math.DivRem(intDays, 365, out intResult);
        intMonth = Math.DivRem(intResult, 30, out intResult);
        intDays = intResult;
        if (intYear > 0 && intDays > 0)
        {
            txtAge.Text = intYear.ToString();
            cmdYMD.SelectedIndex = 0;
        }
        else if (intMonth > 0)
        {
            txtAge.Text = intMonth.ToString();
            cmdYMD.SelectedIndex = 1;
        }
        else if (intDays > 0)
        {
            txtAge.Text = intDays.ToString();
            cmdYMD.SelectedIndex = 2;
        }
        ViewState["AccDate"] = 1;
        txtDoctorName.Focus();

    }
    protected void RblDiscgiven_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RblDiscgiven.SelectedValue == "3")
        {
            txtDisLabgiven.Visible = true;
            txtDisdrgiven.Visible = true;
        }
        else
        {
            txtDisLabgiven.Visible = false;
            txtDisdrgiven.Visible = false;
        }
    }

    protected void txtDisLabgiven_TextChanged(object sender, EventArgs e)
    {
        if (txtDisLabgiven.Text != "")
        {

            txtDisdrgiven.Text = Convert.ToString(Convert.ToSingle(txtDisamount.Text) - Convert.ToSingle(txtDisLabgiven.Text));

        }
    }
    protected void otherchargeRemark_TextChanged(object sender, EventArgs e)
    {
        if (otherchargeRemark.Text != "")
        {
            txtpaidamount.Focus();
        }
    }
    protected void txtDisdrgiven_TextChanged(object sender, EventArgs e)
    {
        if (txtDisdrgiven.Text != "")
        {

            txtDisLabgiven.Text = Convert.ToString(Convert.ToSingle(txtDisamount.Text) - Convert.ToSingle(txtDisdrgiven.Text));

        }
    }
    protected void txtemail_TextChanged(object sender, EventArgs e)
    {
        if (ReportBy.SelectedValue == "2" || ReportBy.SelectedValue == "1")
        {
            if (txtemail.Text == "")
            {
                txtemail.Focus();
                txtemail.BackColor = Color.Orange;
                txtemail.ForeColor = Color.White;

            }
            else
            {
                txtemail.BackColor = Color.White;
                txtemail.ForeColor = Color.Black;
            }
        }
        if (txtemail.Text != "")
        {
            try
            {
                var emailChecked = new System.Net.Mail.MailAddress(txtemail.Text);
                txtemail.BackColor = Color.White;
                txtemail.ForeColor = Color.Black;

            }
            catch
            {
               
                txtemail.Focus();
                txtemail.BackColor = Color.Orange;
                txtemail.ForeColor = Color.White;

            }
        }
        if (txtemail.Text != "")
        {
            ValidateEmail();
        }
        txt_address.Focus();
    }
    private void ValidateEmail()
    {
        string email = txtemail.Text;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (match.Success)
        {
            txtemail.BackColor = Color.White;
            txtemail.ForeColor = Color.Black;
        }
        else
        {
            txtemail.Focus();
            txtemail.BackColor = Color.Orange;
            txtemail.ForeColor = Color.White;
        }
            
    }
    protected void txttelno_TextChanged(object sender, EventArgs e)
    {
        if (txttelno.Text.Length <= 9)
        {
            txttelno.Focus();
            txttelno.BackColor = Color.Orange;
            txttelno.ForeColor = Color.White;
            txttelno.Focus();
            //Label11.Text = "Enter mobile no";

        }
        else
        {
            txttelno.BackColor = Color.White;
            txttelno.ForeColor = Color.Black;
        }
        if (txttelno.Text.Length > 10)
        {
            txttelno.Focus();
            txttelno.BackColor = Color.Orange;
            txttelno.ForeColor = Color.White;
            txttelno.Focus();
            //Label11.Text = "Enter mobile no";

        }
        txtemail.Focus();

    }
    protected void txtSearchCardNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchCardNo.Text != "")
            {
                string[] PatientMNo = txtSearchCardNo.Text.Split('=');
                if (PatientMNo.Length > 1)
                {
                    txtSearchCardNo.Text = PatientMNo[3].ToString().Trim();
                    Patmst_Bal_C PBC = new Patmst_Bal_C();
                    DrMT_Bal_C dr = new DrMT_Bal_C();
                    PBC.P_PPID = Convert.ToInt32(PatientMNo[0].ToString().Trim());
                    PBC.getPermentCardId(txtSearchCardNo.Text);
                    cmbInitial.SelectedItem.Text = PBC.Initial;
                    cmbInitial.DataValueField = PBC.Initial;
                    if (PBC.AccDateofBirth == 1)
                    {
                        int age = 0;
                        age = DateTime.Now.Year - PBC.DateOfBirth.Year;
                        if (DateTime.Now.DayOfYear < PBC.DateOfBirth.DayOfYear)
                            age = age - 1;
                        txtAge.Text = Convert.ToString(age);
                        txtBirthdate.Text = Convert.ToString(PBC.DateOfBirth.ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        txtAge.Text = PBC.Age.ToString();
                    }
                    txtFname.Text = PBC.Patname;
                    dr.GetDoctorName(PBC.RefDr, Convert.ToInt32(Session["Branchid"]));

                    txtDoctorName.Text = dr.Prefix.Trim() + ' ' + dr.Name + '=' + dr.DoctorCode;

                    txtemail.Text = PBC.Email;
                    cmdYMD.SelectedValue = PBC.MYD;
                    txttelno.Text = PBC.Phone;
                    txt_address.Text = PBC.Pataddress;


                    if (cmbInitial.SelectedItem.Text.Trim() != "Dr." && cmbInitial.SelectedItem.Text.Trim() != "Baby" && cmbInitial.SelectedItem.Text.Trim() != "Baby Of" && cmbInitial.SelectedItem.Text.Trim() != "Doctor" && cmbInitial.SelectedItem.Text.Trim() != "Dr" && cmbInitial.SelectedItem.Text.Trim() != "doctor" && cmbInitial.SelectedItem.Text.Trim() != "dr" && cmbInitial.SelectedItem.Text.Trim() != "baby" && cmbInitial.SelectedItem.Text.Trim() != "baby of" && cmbInitial.SelectedItem.Text.Trim() != "dr.")
                    {
                        ddlsex.Items.Add(PatientinitialLogic_Bal_C.SelectSex(cmbInitial.SelectedItem.Text));
                        Cshmst_Bal_C ObjCBC = new Cshmst_Bal_C();
                        dt = new DataTable();
                        dt = ObjCBC.Get_Initial(cmbInitial.SelectedItem.Text, 1);
                        ddlsex.DataSource = dt;

                        ddlsex.DataTextField = "sex";
                        ddlsex.DataValueField = "sex";
                        ddlsex.DataBind();
                        ddlsex.SelectedIndex = 1;
                        // cmbInitial.SelectedIndex = 1;
                        hdnstatus.Value = "1";
                    }
                    else
                    {
                        ddlsex.Items.Add("Male");
                        ddlsex.Items.Add("Female");
                    }
                }
            }
            txtAge.Focus();

        }
        catch (Exception exc)
        { }
    }
    
    [WebMethod]
    [ScriptMethod]
    public static string[] GetCardInfo(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        string collectioncode = HttpContext.Current.Session["CenterCode"].ToString();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
        {
            sda = new SqlDataAdapter("select PPID,PatientCardNo,Patname,Mobileno from PatMT where (PatientCardNo like N'%" + prefixText + "%') or (Mobileno like N'%" + prefixText + "%')  ", con);
        }
        else
        {
            sda = new SqlDataAdapter("select PPID,PatientCardNo,Patname,Mobileno from PatMT where (PatientCardNo like N'%" + prefixText + "%') or (Mobileno like N'%" + prefixText + "%')   ", con);
        }
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["PPID"] + " = " + dr["PatientCardNo"] + " = " + dr["Patname"] + " = " + dr["Mobileno"], i);
            i++;
        }

        return tests;
    }

    protected void btnupload_Click(object sender, EventArgs e)
    {
        if (FUBrowsePresc.HasFile)
        {
            try
            {

                //string Pname = txtFname.Text + "/" + DateTime.Now.ToString("ddMMyyyy");
                string PDate = DateTime.Now.ToString("ddMMyyyy");
                //string ViewP = "~/ViewPrescription/" + Pname;
                //FUBrowsePresc.SaveAs(Server.MapPath(ViewP));
                string DefaultFileName = "ViewPrescription/";
                if (FUBrowsePresc.HasFile)
                {
                    FUBrowsePresc.SaveAs(Server.MapPath(DefaultFileName) + "/" + ViewState["RegN0"] + "_" + PDate + "_" + FUBrowsePresc.FileName);
                    LblFilename.Text = ViewState["RegN0"] + "_" + PDate + "_" + FUBrowsePresc.FileName;
                    Cshmst_Bal_C Obj_CBC = new Cshmst_Bal_C();
                    Obj_CBC.P_UploadPrescription = DefaultFileName+""+LblFilename.Text;
                    Obj_CBC.PID = Convert.ToInt32(ViewState["PID"]);
                    Obj_CBC.PatRegID = Convert.ToString(ViewState["RegN0"]);

                    Obj_CBC.Insert_Update_Prescription( Convert.ToInt32( Session["Branchid"]));
                }
            }
            catch (Exception ex)
            {
                //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
    }
    protected void btnquickaddT_Click(object sender, EventArgs e)
    {
        this.Chkmaintestshort_SelectedIndexChanged(null, null);
    }
    protected void txtcardexpdate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["ISDemography"]) == "YES")
        {
            //txtWeight.TabIndex = 100;
            txtWeight.Focus();
        }
        else
        {
            txttests.Focus();
        }
    }

    protected void btncapturephoto_Click(object sender, EventArgs e)
    {
        Session["PID_Img"] = ViewState["PID"];
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'CS.aspx?PID=" + ViewState["PID"] + "&PatRegID=" + ViewState["RegN0"] + "&Branchid=" + Session["Branchid"].ToString() + "&FID=" + ViewState["Fid0"] + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

    }
    protected void btnbrowsphoto_Click(object sender, EventArgs e)
    {
        if (FUUploadPhoto.HasFile)
        {
            try
            {

                //string Pname = txtFname.Text + "/" + DateTime.Now.ToString("ddMMyyyy");
                string PDate = DateTime.Now.ToString("ddMMyyyy");
                //string ViewP = "~/ViewPrescription/" + Pname;
                //FUBrowsePresc.SaveAs(Server.MapPath(ViewP));
                string DefaultFileName = "Captures/";
                if (FUUploadPhoto.HasFile)
                {
                    FUUploadPhoto.SaveAs(Server.MapPath(DefaultFileName) + "/" + ViewState["RegN0"] + "_" + PDate + "_" + FUUploadPhoto.FileName);
                    LblUploadph.Text = ViewState["RegN0"] + "_" + PDate + "_" + FUUploadPhoto.FileName;
                    //Cshmst_Bal_C Obj_CBC = new Cshmst_Bal_C();
                    //Obj_CBC.P_UploadPrescription = DefaultFileName+""+LblFilename.Text;
                    //Obj_CBC.PID = Convert.ToInt32(ViewState["PID"]);
                    //Obj_CBC.PatRegID = Convert.ToString(ViewState["RegN0"]);

                    //Obj_CBC.Insert_Update_Prescription( Convert.ToInt32( Session["Branchid"]));

                    //byte[] imageBytes = Convert.FromBase64String(FUBrowsePresc.FileName);
                    //Byte[] imgByte1 = null;
                    //Save the Byte A


                    byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath("~/Captures/" + LblUploadph.Text + ""));

                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                    SqlConnection conn = DataAccess.ConInitForDC();
                    SqlCommand sc = new SqlCommand();
                    sc = new SqlCommand("" +
                         "update patmst set  Image1=@Image1  ,ImagePath='" + LblUploadph.Text + "' where  PID=@PID ", conn);


                    sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = ViewState["PID"];


                    sc.Parameters.AddWithValue("@Image1", base64ImageRepresentation);


                    conn.Open();
                    sc.ExecuteNonQuery();
                    conn.Dispose();

                }
            }
            catch (Exception ex)
            {
                //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
        }
    }


    public void WriteErrorLog(Exception ex, string EventName)
    {
        string webPageName = Path.GetFileName(Request.Path);
        string UserName = Convert.ToString(Session["username"]);
        string errorLogFilename = "ErrorLog_" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
        string path = Server.MapPath("~/ErrorLogFiles/" + errorLogFilename);
        if (File.Exists(path))
        {
            using (StreamWriter stwriter = new StreamWriter(path, true))
            {
                stwriter.WriteLine("-------------------Error Log Start-----------as on " + DateTime.Now.ToString("hh:mm tt"));
                stwriter.WriteLine("WebPage Name :" + webPageName);
                stwriter.WriteLine("Event Name :" + EventName);
                stwriter.WriteLine("User Name :" + UserName);
                stwriter.WriteLine("Message:" + ex.ToString());
                stwriter.WriteLine("-------------------End----------------------------");
            }
        }
        else
        {
            StreamWriter stwriter = File.CreateText(path);
            stwriter.WriteLine("-------------------Error Log Start-----------as on " + DateTime.Now.ToString("hh:mm tt"));
            stwriter.WriteLine("WebPage Name :" + webPageName);
            stwriter.WriteLine("Event Name :" + EventName);
            stwriter.WriteLine("User Name :" + UserName);
            stwriter.WriteLine("Message: " + ex.ToString());
            stwriter.WriteLine("-------------------End----------------------------");
            stwriter.Close();
        }
    }
    protected void btnWhatapp_Click(object sender, ImageClickEventArgs e)
    {
        string sql1 = "";

        SqlConnection conn1 = DataAccess.ConInitForDC();
        SqlCommand sc1 = conn1.CreateCommand();
        PatSt_Bal_C PBC = new PatSt_Bal_C();

        sc1.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT        RecM.BillNo, patmst.Patregdate AS RecDate, 'Cash Bill' AS BillType, RecM.AmtPaid AS AmtReceived, RecM.DisAmt AS Discount, patmst.TestCharges AS NetPayment, RecM.AmtPaid, " +
                         "   dbo.FUN_GetReceiveAmt_Balance(1, RecM.PID) AS Balance, RecM.username, RecM.OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, " +
                          "  RecM.CardNo AS DoctorName, MainTest.Maintestname, MainTest.MTCode, patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag, patmst.Patusername, patmst.Patpassword, RecM.Comment, " +
                          "  patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, RecM.PaymentType AS UnitCode, RecM.TaxAmount, RecM.TaxPer, dbo.FUN_GetPrintcount(RecM.branchid, RecM.PID) AS PrintCount,  " +
                          "  patmst.OtherRefDoctor, RecM.ReceiptNo, RecM.OtherChargeRemark, RecM.PID " +
                          "  FROM            patmst INNER JOIN " +
                          "  DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN " +
                          "  MainTest INNER JOIN " +
                          "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN " +
                          "  RecM ON patmst.PID = RecM.PID LEFT OUTER JOIN " +
                          "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode " +
                 "   where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and patmstd.PID='" + ViewState["PID"] + "' )";// and Cshmst.BillNo=" + bno + "
        conn1.Open();
        sc1.ExecuteNonQuery();
        conn1.Close(); conn1.Dispose();

        string RID = FinancialYearTableLogic.getPatregister(Convert.ToInt32(Session["Branchid"]));
        if (RID != "0")
        {

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "", selectonFormula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~//DiagnosticReport//Rpt_PayReceipt.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_cshbill where PID='" + ViewState["PID"] + "'  ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
            // string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string filename1 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + ViewState["PID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Receipt" + ".pdf");

            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_cshbill.PID}='" + ViewState["PID"] + "' ";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            exportedpath = filename1;

            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

            CR.Close();
            CR.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + ViewState["PID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Receipt" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label101.Text = "Receipt Not Generated, Please Generate Once Again ";
                return;
            }
            Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(ViewState["RegN0"], Convert.ToString(Session["financialyear"]), Convert.ToInt32(Session["Branchid"]));
            string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
            string mobile = Obj_PBC_C.telNo;
            string email = Obj_PBC_C.Email;
            string msg1 = "";
            createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
            aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
            string WhatAppUrl = aut.P_WhatAppUrl;
            string WhatApp_Api = aut.P_WhatApp_Api;
            WhatAppReport(mobile, filename1, WhatAppUrl, WhatApp_Api);

        }
    }


    public void WhatAppReport(string MobNo, string FilePath, string WhatAppUrl, string WhatApp_Api)
    {
        try
        {
            // user will change below 3 variables only 
            //var filepath = "/var/www/test.jpg"; // absolute path of file on local drive
            //string mm2 = Page.ResolveUrl("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            var filepath = FilePath; // absolute path of file on local drive
            // var key = "ft1dcT8j16fIOknh"; // your api key
            var key = WhatAppUrl.Trim(); // your api key
            // var number = "9199XXXXXXXX"; // target mobile number, including country code
            var number = MobNo; // target mobile number, including country code
            var caption = "Test Report"; // caption is optional parameter


            // do not change below this line
            byte[] AsBytes = File.ReadAllBytes(@filepath);
            String filedata = Convert.ToBase64String(AsBytes);

            var filename = new FileInfo(filepath).Name;
            var wb = new WebClient();
            var data = new NameValueCollection();

            data["data"] = filedata;
            data["filename"] = filename;
            data["key"] = key;
            data["number"] = number;
            data["caption"] = caption;

            var response = wb.UploadValues(WhatApp_Api, "POST", data);
            string responseInString = Encoding.UTF8.GetString(response);

            Label101.Text = "Receipt Send Successfully..!";
        }
        catch (Exception ex)
        {
            // Label44.Text = "Report Not Send Successfully..!";
        }
    }
    public void checkexistpageright(string PageName)
    {

        string MenuSQL = "";
        DataTable MenuDt = new DataTable();
        MenuSQL = String.Format(@"SELECT        Roleright.Rightid, Roleright.Usertypeid, Roleright.FormId, Roleright.FormName, Roleright.Branchid, usr.ROLENAME, " +
              "  TBL_SubMenuMaster.SubMenuNavigateURL, TBL_MenuMaster.MenuName, TBL_MenuMaster.MenuID,   TBL_SubMenuMaster.SubMenuName, TBL_MenuMaster.Icon, " +
              "  TBL_SubMenuMaster.SubMenuID , CTuser.FrontDiskDisc  " +
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
        else
        {
            if (Convert.ToBoolean(MenuDt.Rows[0]["FrontDiskDisc"]) == true)
            {
                txtDisamount.Enabled = true;
                DiscAllowed = 1;
            }
            else
            {
                txtDisamount.Enabled = false;
                DiscAllowed = 0;
            }
        }
        con.Close();
        con.Dispose();

    }


    public void BarCodeImage(int PID)
    {
        //BarcodeWriter writer = new BarcodeWriter()
        //{
        //    Format = BarcodeFormat.CODE_128,
        //    Options = new EncodingOptions
        //    {
        //        Height = 400,
        //        Width = 800,
        //        PureBarcode = false,
        //        Margin = 10,
        //    },
        //};

        //var bitmap = writer.Write("test text");
        //bitmap.Save(HttpContext.Response.Body, System.Drawing.Imaging.ImageFormat.Png);
        //return; // there's no need to return a `FileContentResult` by `File(...);`

        SqlConnection con = DataAccess.ConInitForDC();

        SqlDataAdapter sda = null;
        DataTable dtbI = new DataTable();
        // DataSet1 dst = new DataSet1();
        sda = new SqlDataAdapter("select distinct case when isnull(patmstd.PhlebotomistRejectremark,'') <>'' then patmstd.BarcodeID+'-'+'1' else patmstd.BarcodeID end as BarcodeID,MTCode from Patmstd where PID='" + Convert.ToString(PID) + "'   ", con);
      //  sda = new SqlDataAdapter("select distinct PatRegID as BarcodeID,MTCode from Patmstd where PID='" + Convert.ToString(PID) + "'   ", con);

        sda.Fill(dtbI);
        if (dtbI.Rows.Count > 0)
        {
            for (int p = 0; p < dtbI.Rows.Count; p++)
            {
                // string Code = "AAAAAAA";
                QrCodeEncodingOptions Option = new QrCodeEncodingOptions();
                BarcodeWriter writer = new BarcodeWriter()
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Height = 400,
                        Width = 800,
                        PureBarcode = false,
                        Margin = 10,
                    },
                };
                string Code = Convert.ToString(dtbI.Rows[p]["BarcodeID"]);
                var qrWrite = new BarcodeWriter(); ;
                qrWrite.Format = BarcodeFormat.CODE_128;
                int THeight = Convert.ToInt32(ViewState["BarCodeHeight"]), TWidth = Convert.ToInt32(ViewState["BarCodeWidth"]), TPureMar = Convert.ToInt32(ViewState["BarCodeMargin"]);
                qrWrite.Options = new EncodingOptions()
                {

                    Height = THeight,
                    Width = TWidth,
                    Margin = TPureMar,
                    PureBarcode = false,
                };

                // var result = new Bitmap(qrWrite.Write(Code));
                byte[] byteImage;
                using (Bitmap bitMap = new Bitmap(qrWrite.Write(Convert.ToString(dtbI.Rows[p]["BarcodeID"]))))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {

                        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        {
                            byteImage = ms.ToArray();
                        }
                    }
                }

                SqlConnection conn = DataAccess.ConInitForDC();
                SqlCommand sc = new SqlCommand("" +
                "Update Patmstd " +
                "Set BarCodeImage=@SignImage " +
                " Where PID=@id and MTcode =@MTcode ", conn);
                SqlDataReader sdr = null;
                if (byteImage != null)
                {
                    sc.Parameters.AddWithValue("@SignImage", byteImage);
                }
                else
                {
                    SqlParameter imageParameter = new SqlParameter("@SignImage", SqlDbType.Image);
                    imageParameter.Value = DBNull.Value;
                    sc.Parameters.Add(imageParameter);
                }

                sc.Parameters.AddWithValue("@id", PID);
                sc.Parameters.AddWithValue("@MTcode", Convert.ToString(dtbI.Rows[p]["MTCode"]));
                try
                {
                    conn.Open();
                    sc.ExecuteNonQuery();
                }
                finally
                {
                    try
                    {
                        if (sdr != null) sdr.Close();
                        conn.Close(); conn.Dispose();
                    }
                    catch (SqlException)
                    {
                        throw;
                    }
                }
            }
        }
    }

}