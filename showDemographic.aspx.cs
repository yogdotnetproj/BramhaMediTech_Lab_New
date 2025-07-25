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
using System.Text.RegularExpressions;
using System.Drawing;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Management;

using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

using System.Collections.Specialized;
using System.Text;


using ZXing.QrCode;
using ZXing.Common;
using ZXing.Maxicode;
using ZXing;

public partial class showDemographic : System.Web.UI.Page
{
    string maintestshort = "";
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    Patmstd_Bal_C ObjPBC1 = new Patmstd_Bal_C();
    int bno = 0;
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string Date2 = DateTime.Now.AddDays(-1).ToString("ddMMyyyy");
    Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();
    public void Bindbanner()
    {
        DataTable dtban = new DataTable();
       
        dtban = ObjTB.Bindbanner();
        if (dtban.Rows.Count > 0)
        {
            
            if (Convert.ToBoolean(dtban.Rows[0]["BarCodeImageReq"]) == false)
            {
                ViewState["BarCodeImageReq"] = "NO";
            }
            else
            {
                ViewState["BarCodeImageReq"] = "YES";
            }

           
           

        }
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        ViewState["btnsave"] = "";
        lbl_mobile.Visible = false;
        if (!Page.IsPostBack)
        {
            try
            {
                Bindbanner();
               
                BindShortcut_test();
                if (Session["usertype"] != null && Session["username"] != null)
                {                    

                }
                if (Convert.ToString(Session["ISDemography"]) == "YES")
                {
                    D1.Visible = true;
                    D2.Visible = true;
                    D3.Visible = true;
                    D4.Visible = true;
                    D5.Visible = true;
                    D6.Visible = true;
                    D7.Visible = true;
                    D8.Visible = true;
                    btnpayment.Visible = true;
                   
                }
                cmbInitial.DataSource = PatientinitialLogic_Bal_C.getInitial();
                cmbInitial.DataTextField = "prefixName";
                cmbInitial.DataValueField = "prefixName";
                cmbInitial.DataBind();
                txtsex.Text = "Male";
                
                #region CreateTable

                DataTable table = new DataTable();
                // Declare DataColumn and DataRow variables.
                DataColumn column;
                column = new DataColumn("MTCode", Type.GetType("System.String"));
                table.Columns.Add(column);
                column = new DataColumn("Maintestname", Type.GetType("System.String"));
                table.Columns.Add(column);
                column = new DataColumn("Amount", Type.GetType("System.String"));
                table.Columns.Add(column);
                column = new DataColumn("ClientAmount", Type.GetType("System.String"));
                table.Columns.Add(column);

                ViewState["table"] = table;

                #endregion

                ViewState["PID"] = Request.QueryString["PID"].ToString();
                if (Request.QueryString["PID"] != null && Request.QueryString["FType"] != null)
                {
                    if (Request.QueryString["FType"].ToString() == "Edit")
                    {
                        ViewState["barid"] = null;

                        GetRecords(Convert.ToInt32(Request.QueryString["PID"]));
                        SqlCommand cmd = null;
                        SqlConnection conn = DataAccess.ConInitForDC();
                        if (Session["DigModule"] != null && Session["DigModule"] != "0")
                            cmd = new SqlCommand("SELECT DISTINCT PackageCode as code FROM  patmstd WHERE  PackageCode<>'' and PID = " + Convert.ToInt32(Request.QueryString["PID"]) + "  UNION " +
                                               " SELECT DISTINCT MTCode as code FROM  patmstd WHERE" +
                                               " (PackageCode IS NULL OR PackageCode = '') AND PID = " + Convert.ToInt32(Request.QueryString["PID"]) + " ", conn);
                        else
                            cmd = new SqlCommand("SELECT DISTINCT PackageCode as code FROM  patmstd WHERE PackageCode<>'' and PID = " + Convert.ToInt32(Request.QueryString["PID"]) + " AND (PatRegID <> '') UNION " +
                                                " SELECT DISTINCT MTCode as code FROM  patmstd WHERE" +
                                                " (PackageCode IS NULL OR PackageCode = '') AND PID = " + Convert.ToInt32(Request.QueryString["PID"]) + " ", conn);
                        conn.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            if (!string.IsNullOrEmpty(sdr["code"].ToString()))
                            {
                                AddTestToTable(sdr["code"].ToString());
                            }
                        }
                        sdr.Close();
                        conn.Close(); conn.Dispose();
                        grdTests.DataSource = (DataTable)ViewState["table"];
                        grdTests.DataBind();

                        float amtTestTot = 0f, amtClientTestTot = 0f;
                        if (grdTests.Rows.Count > 0)
                        {
                            foreach (GridViewRow gvr in grdTests.Rows)
                            {
                                if (gvr.Cells[4].Text.Trim() != "")
                                {
                                    string amt = gvr.Cells[4].Text.Trim();
                                    amtTestTot = amtTestTot + Convert.ToSingle(amt);
                                }
                                if (gvr.Cells[5].Text.Trim() != "")
                                {
                                    string Clientamt = gvr.Cells[5].Text.Trim();
                                    amtClientTestTot = amtClientTestTot + Convert.ToSingle(Clientamt);
                                }
                            }
                        }
                        lblTotTestAmt.Text = amtTestTot.ToString();
                        ViewState["clientAmount"] = amtClientTestTot.ToString();
                        
                        ViewState["Previousamount"] = lblTotTestAmt.Text;

                        #region Code for adding to grid

                        DataTable tableSample = new DataTable();
                        //Declare DataColumn and DataRow variables.
                        DataColumn columnP;
                        columnP = new DataColumn("SampleType", Type.GetType("System.String"));
                        tableSample.Columns.Add(columnP);
                        columnP = new DataColumn("STCODE", Type.GetType("System.String"));
                        tableSample.Columns.Add(columnP);
                        columnP = new DataColumn("TestName", Type.GetType("System.String"));
                        tableSample.Columns.Add(columnP);

                        ViewState["tableSample"] = tableSample;

                        if (grdTests.Rows.Count > 0)
                        {
                            foreach (GridViewRow gvr in grdTests.Rows)
                            {
                                if (gvr.Cells[1].Text.Trim().Length == 4)
                                {
                                    SqlConnection connP = DataAccess.ConInitForDC();
                                    SqlCommand cmdP = new SqlCommand("select MTCode,TestName from PackmstD where PackageCode='" + gvr.Cells[1].Text.Trim() + "'", connP);
                                    connP.Open();
                                    SqlDataReader sdrP = cmdP.ExecuteReader();
                                    while (sdrP.Read())
                                    {
                                        #region Code to Add Test to table

                                        MainTest_Bal_C tt = new MainTest_Bal_C(sdrP["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                                        string sampleTypeOfTest = tt.SampleType;
                                        bool sampleAlreadyExist = false;
                                        string sampleTypesString = "";
                                        foreach (DataRow dr in tableSample.Rows)
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
                                                    // sampleAlreadyExist = true;
                                                    sampleAlreadyExist = false;
                                            }
                                        }
                                        if (sampleAlreadyExist)
                                        {
                                            foreach (DataRow dr in tableSample.Rows)
                                            {
                                                if (dr["SampleType"].ToString() == sampleTypeOfTest)
                                                {
                                                    if (dr["STCODE"].ToString() == "")
                                                    {
                                                        dr["STCODE"] = sdrP["MTCode"].ToString();
                                                    }
                                                    else
                                                    {
                                                        dr["STCODE"] = dr["STCODE"] + "," + sdrP["MTCode"].ToString();
                                                    }
                                                    if (dr["TestName"].ToString() == "")
                                                    {
                                                        dr["TestName"] = sdrP["TestName"].ToString();
                                                    }
                                                    else
                                                    {
                                                        dr["TestName"] = dr["TestName"] + "," + sdrP["TestName"].ToString();
                                                    }
                                                }
                                            }

                                        }
                                        else
                                        {
                                            DataRow dr = tableSample.NewRow();

                                            dr["SampleType"] = sampleTypeOfTest;
                                            dr["STCODE"] = sdrP["MTCode"].ToString();
                                            dr["TestName"] = sdrP["TestName"].ToString();

                                            tableSample.Rows.Add(dr);
                                        }

                                        #endregion
                                    }
                                    sdrP.Close();
                                    connP.Close();
                                }
                                else
                                {
                                    #region Code to Add Test to table

                                    MainTest_Bal_C tt = new MainTest_Bal_C(gvr.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));
                                    string sampleTypeOfTest = tt.SampleType;
                                    bool sampleAlreadyExist = false;
                                    string sampleTypesString = "";
                                    foreach (DataRow dr in tableSample.Rows)
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
                                                // sampleAlreadyExist = true;
                                                sampleAlreadyExist = false;
                                        }
                                    }
                                    if (sampleAlreadyExist)
                                    {
                                        foreach (DataRow dr in tableSample.Rows)
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
                                        DataRow dr = tableSample.NewRow();

                                        dr["SampleType"] = sampleTypeOfTest;
                                        dr["STCODE"] = gvr.Cells[1].Text.Trim();
                                        dr["TestName"] = gvr.Cells[2].Text.Trim();

                                        tableSample.Rows.Add(dr);
                                    }

                                    #endregion
                                }
                            }
                        }

                        grdTests.DataSource = (DataTable)ViewState["table"];
                        grdTests.DataBind();

                        GrdPackage.DataSource = (DataTable)ViewState["tableSample"];
                        GrdPackage.DataBind();

                        #endregion
                    }

                    //================================================


                     dt = new DataTable();
                     dt = ObjTB.AithorizedTestCount(Convert.ToInt32(Request.QueryString["PID"]));
                        if (dt.Rows.Count > 0)
                        {
                            if (Convert.ToString(Session["usertype"]) != "Administrator")
                            {
                                txtDoctorName.Enabled = false;
                                txtFname.Enabled = false;
                            }
                        }
                    //================================================
                   
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
    }

    void GetRecords(int PID)
    {
        ArrayList al = (ArrayList)Patmst_New_Bal_C.Get_patmst_againsPIDID(PID, Convert.ToInt32(Session["Branchid"]));
        IEnumerator ie = al.GetEnumerator();
        while (ie.MoveNext())
        {
            Patmst_Bal_C Patmst = (ie.Current as Patmst_Bal_C);

          //  cmbInitial.SelectedItem.Text = Patmst.Initial;
            cmbInitial.SelectedValue = Patmst.Initial;
            txtFname.Text = Patmst.Patname;
            string tel = Patmst.Phone;
             string CounCode = ObjPBC1.GetSMSString_CountryCode("Registration", Convert.ToInt32(Session["Branchid"]));
             //if (CounCode.Length == 2)
             //{
             //    if (tel != CounCode)
             //    {
             //        tel = tel.Substring(2);
             //        txttelno.Text = tel;
             //    }
             //    else
             //    {
             //        txttelno.Text = "";
             //    }
             //}
             //else
             //{
             //    if (tel != CounCode)
             //    {
             //        tel = tel.Substring(3);
             //        txttelno.Text = tel;
             //    }
             //    else
             //    {
             //        txttelno.Text = "";
             //    }
             //}
             txttelno.Text = tel;
            if (Patmst.DoctorCode != "" || Patmst.DoctorCode != null)
            {
                txtDoctorName.Text = Patmst.Drname + "=" + Patmst.DoctorCode;
            }

            txtsex.Text = Patmst.Sex;
            txtemail.Text = Patmst.Email;
            ViewState["CenterCode"] = Patmst.CenterCode.Trim();
            ViewState["CenterName"] = Patmst.CenterName;
            txtAge.Text = Patmst.Age.ToString();
            cmdYMD.SelectedValue = Patmst.MYD;
            txt_remark.Text = Patmst.OtherRefDoctor;
          
            try
            {
              
            }
            catch (Exception) { }
            txt_address.Text = Patmst.Pataddress;
            txt_clinicalhistory.Text = Patmst.PatientcHistory;
            txt_remark.Text = Patmst.Remarks;
            txtWeight.Text= Patmst.P_Weights;
            txtHeight.Text= Patmst.P_Heights;
            txtDieses.Text= Patmst.P_Disease;
            txtLastPeriod.Text= Patmst.P_LastPeriod;
            txtSymptoms.Text= Patmst.P_Symptoms;
            txtFSTime.Text= Patmst.P_FSTime;
            txtTherapy.Text = Patmst.P_Therapy;
            txtBirthdate.Text = Convert.ToString( Patmst.DOB);//.DateConvert.ToString(Patmst.DateOfBirth);

            if (Patmst.P_SocialMedia == 0)
            {
                //rblretecate.SelectedItem.Text = "General";
                rblretecate.SelectedValue = "0";
            }
            else if (Patmst.P_SocialMedia == 1)
            {
               // rblretecate.SelectedItem.Text = "Viber";
                rblretecate.SelectedValue = "1";
            }
            else
            {
                //rblretecate.SelectedItem.Text = "Whatsapp";
                rblretecate.SelectedValue = "2";
            }
           // txt_remark.Text = Patmst.P_remark;

            string folderPath = Server.MapPath("~/Captures/");

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Image1show.ImageUrl = "~/Images/Hello.jpg";
            Image1show.ImageUrl = "~/Captures/" + Path.GetFileName(Patmst.ImagePath);

        }
    }

  
  
  
 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
        ViewState["barcodeempty"] = "";
        int mobcnt = txttelno.Text.Length; 

            ViewState["btnsave"] = "true";
            string[] DoctorCode = new string[] { "", "" };

            if (txtDoctorName.Text != "")
            {
                if (!txtDoctorName.Text.Contains("="))
                {

                }
                DoctorCode = txtDoctorName.Text.Split('=');
               
            }

            if (Request.QueryString["PID"] != null && Request.QueryString["FType"] != null)
            {
                if (Request.QueryString["FType"].ToString() == "Edit")
                {
                    string[] vialArr1 = ViewState["barid"].ToString().Split(',');

                    #region For Check if  exists & Barcode length
                    if (GrdPackage.Rows.Count != 0)
                    {
                        ViewState["flag"] = "";
                        bool flag = false;
                        int count = 0;
                        foreach (GridViewRow gvr in GrdPackage.Rows)
                        {
                            count = count + 1;
                            int i = (gvr.RowIndex);

                            TextBox tb = gvr.FindControl("txtbarcodeid") as TextBox;
                            Label lblError = gvr.FindControl("lblError") as Label;
                            Label lblRequiredField = gvr.FindControl("lblRequiredField") as Label;

                           
                                if (tb.Text == "")
                                {

                                 //   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('barcode is required');", true);
                                    flag = false;
                                    ViewState["barcodeempty"] = "1";
                                }
                                else
                                {
                                    lblError.Visible = false;
                                    if (ViewState["flag"].ToString() == "1")
                                    {
                                        flag = true;
                                    }
                                    else
                                    {
                                        flag = false;
                                    }

                                    if (tb.Text != "")
                                    {
                                        bool aa = false;
                                        int vialcount = 0;
                                        foreach (string vi in vialArr1)
                                        {
                                            vialcount = vialcount + 1;
                                            if (vi == tb.Text.Trim())
                                            {
                                                aa = false;
                                            }
                                            else
                                            {
                                                aa = true;
                                                if (count == vialcount)
                                                {
                                                    if (vi != tb.Text.Trim())
                                                    {
                                                        if (Barcode_C.IsbarcodeIdExist(tb.Text, Convert.ToInt32(Session["Branchid"])) == true)
                                                        {
                                                            lblRequiredField.Visible = true;
                                                            lblRequiredField.Text = "Barcode already exists";
                                                            flag = true;
                                                            ViewState["flag"] = "1";
                                                        }
                                                    }
                                                }
                                            }

                                        }

                                    }
                                    else
                                    {
                                        lblRequiredField.Visible = false;
                                        flag = false;
                                    }
                                }
                            //}//

                        }

                        //if (flag)
                        //{
                        //    return;
                        //}
                    }
                    #endregion

                    #region Comment For Check if Barcode exists

                    
                    #endregion

                    string STCODE = "";
                    string sampletypes = "";
                    string Barid = "";
                    string testname = "";
                    foreach (GridViewRow gvr in GrdPackage.Rows)
                    {
                        if ((gvr.FindControl("txtbarcodeid") as TextBox).Text != "")
                        {
                            if (Barid == "")
                            {
                                Barid = (gvr.FindControl("txtbarcodeid") as TextBox).Text;
                            }
                            else
                            {
                                Barid = Barid + "," + (gvr.FindControl("txtbarcodeid") as TextBox).Text;
                            }
                        }
                        if (sampletypes == "")
                        {
                            sampletypes = gvr.Cells[0].Text.Trim();
                        }
                        else
                        {
                            sampletypes = sampletypes + ", " + gvr.Cells[0].Text.Trim();
                        }
                    }
                    foreach (GridViewRow gvr in grdTests.Rows)
                    {
                        if (STCODE == "")
                        {
                            STCODE = gvr.Cells[1].Text.Trim();
                            testname = gvr.Cells[2].Text.Trim();
                        }
                        else
                        {
                            STCODE = STCODE + ", " + gvr.Cells[1].Text.Trim();
                            testname = testname + ", " + gvr.Cells[2].Text.Trim();
                        }
                    }
                    int PID = Convert.ToInt32(Request.QueryString["PID"]);
                    ViewState["PID"] = Request.QueryString["PID"].ToString();

                    Patmst_Bal_C Patmst = new Patmst_Bal_C(Request.QueryString["PatRegID"].Trim(), Request.QueryString["FID"].Trim(), Convert.ToInt32(Session["Branchid"]));

                    Patmst.Initial = cmbInitial.SelectedItem.Text.Trim();
                    Patmst.Patname = txtFname.Text;
                    string CounCode = ObjPBC1.GetSMSString_CountryCode("Registration", Convert.ToInt32(Session["Branchid"]));
                    if (CounCode.Length == 2)
                    {
                        Patmst.Phone = CounCode + "" + txttelno.Text;
                       // PBCL.TelNo = CounCode + "" + txttelno.Text;
                    }
                    else
                    {
                        Patmst.Phone = CounCode + "" + txttelno.Text;
                    }
                    Patmst.telNo =  txttelno.Text;
                    Patmst.Email = txtemail.Text;
                    Patmst.Email = txtemail.Text;
                    Patmst.Sex = txtsex.Text;
                    Patmst.Age = int.Parse(txtAge.Text);
                    Patmst.MYD = cmdYMD.SelectedItem.Text;
                    Patmst.DoctorCode = txtDoctorName.Text.Trim();
                    
                    Patmst.Phrecdate = DateTimeConvesion.getDateFromString(Date.getdate().Date.ToString("dd/MM/yyyy")).Date;
                   
                    Patmst.Pataddress = txt_address.Text;
                    Patmst.PatientcHistory = txt_clinicalhistory.Text;
                  
                    Patmst.TestCharges = Convert.ToSingle(lblTotTestAmt.Text);
                    Patmst.ClientTestCharges = Convert.ToSingle(ViewState["clientAmount"]);
                    Patmst.Tests = STCODE;
                    ViewState["TCode"] = STCODE;
                    Patmst.TestName = testname;
                    Patmst.SampleType = sampletypes;
                    Patmst.Username = Session["username"].ToString();



                    Patmst.DoctorCode = DoctorCode[1].ToString().Trim();
                    Patmst.Drname = DoctorCode[0].ToString();
                    Patmst.Usertype = "patient";
                    Patmst.OtherRefDoctor = txt_remark.Text;
                    Patmst.Remarks = txt_remark.Text;
                    if (txtFname.Text.Length > 4)
                    {
                        Patmst.P_Patusername = txtFname.Text.Substring(0, 4) + PID;
                        Patmst.P_Patpassword = txtFname.Text.Substring(0, 4) + PID;
                    }
                    else
                    {
                        Patmst.P_Patusername = txtFname.Text.Trim() + PID;
                        Patmst.P_Patpassword = txtFname.Text.Trim() + PID;
                    }
                    Patmst.P_Patusername = Patmst.P_Patusername.Replace(" ", "");
                    Patmst.P_Patpassword = Patmst.P_Patpassword.Replace(" ", "");

                    if (txtWeight.Text != "")
                    {
                        Patmst.P_Weights = txtWeight.Text;
                    }
                    else
                    {
                        Patmst.P_Weights = "";
                    }
                    if (txtHeight.Text != "")
                    {
                        Patmst.P_Heights = txtHeight.Text;
                    }
                    else
                    {
                        Patmst.P_Heights = "";
                    }
                    if (txtDieses.Text != "")
                    {
                        Patmst.P_Disease = txtDieses.Text;
                    }
                    else
                    {
                        Patmst.P_Disease = "";
                    }
                    if (txtLastPeriod.Text != "")
                    {
                        Patmst.P_LastPeriod = txtLastPeriod.Text;
                    }
                    else
                    {
                        Patmst.P_LastPeriod = "";
                    }
                    if (txtSymptoms.Text != "")
                    {
                        Patmst.P_Symptoms = txtSymptoms.Text;
                    }
                    else
                    {
                        Patmst.P_Symptoms = "";
                    }
                    if (txtFSTime.Text != "")
                    {
                        Patmst.P_FSTime = txtFSTime.Text;
                    }
                    else
                    {
                        Patmst.P_FSTime = "";
                    }
                    if (txtTherapy.Text != "")
                    {
                        Patmst.P_Therapy = txtTherapy.Text;
                    }
                    else
                    {
                        Patmst.P_Therapy = "";
                    }

                    string RepStatus = rblretecate.SelectedItem.Text;
                    if (RepStatus == "General")
                    {
                        Patmst.P_SocialMedia = 0;
                    }
                    else if (RepStatus == "Viber")
                    {
                        Patmst.P_SocialMedia = 1;
                    }
                    else
                    {
                        Patmst.P_SocialMedia = 2;
                    }
                    Patmst.DOB = txtBirthdate.Text;
                    Patmst.Update(Convert.ToInt32(Session["Branchid"]));

                    Patmstd_Bal_C ciTL1 = new Patmstd_Bal_C();
                   // ciTL1.Delete(PID, Convert.ToInt32(Session["Branchid"]));
                   

                    if (grdTests.Rows.Count > 0)
                    {
                        foreach (GridViewRow gvr in grdTests.Rows)
                        {
                            if (Patmstd_Main_Bal_C.IsexistsforPidCodeexists(PID, gvr.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"])))
                            {
                                #region For Update
                                if (gvr.Cells[1].Text.Trim().Length == 4)
                                {
                                    #region For package
                                    SqlConnection conn = DataAccess.ConInitForDC();
                                    SqlCommand cmd = new SqlCommand("select MTCode from PackmstD where PackageCode='" + gvr.Cells[1].Text.Trim() + "'", conn);
                                    conn.Open();
                                    SqlDataReader dr = cmd.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        try
                                        {
                                            DrMT_Bal_C drTable = new DrMT_Bal_C(Request.QueryString["Center"].Trim(), "CC", Convert.ToInt32(Session["Branchid"]));

                                            Patmstd_Bal_C CITL = new Patmstd_Bal_C();

                                            MainTest_Bal_C tt = new MainTest_Bal_C(dr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));

                                            //foreach (GridViewRow gvr1 in GrdPackage.Rows)
                                            //{
                                            //    if (gvr1.Cells[0].Text == tt.SampleType)
                                            //    {
                                            //        CITL.Barcodeid = (gvr1.FindControl("txtbarcodeid") as TextBox).Text;
                                            //        CITL.UnitCode = Convert.ToString(Session["UnitCode"] );
                                                   
                                            //    }
                                            //}
                                            if (BarcodeLogic_C.RecordExistsforsample(PID, tt.SampleType, Convert.ToInt32(Session["Branchid"])))
                                            {
                                                string BarID = BarcodeLogic_C.getbarcodeIDBySampleType(tt.SampleType, PID, Convert.ToInt32(Session["Branchid"]));
                                                CITL.Barcodeid = BarID.ToString();
                                            }
                                            else
                                            {
                                                string SAID = BarcodeLogic_C.getSampleType_ID(tt.SampleType, Convert.ToInt32(Session["Branchid"]));
                                                CITL.Barcodeid = Convert.ToString( Request.QueryString["PatRegID"].Trim() + "" + SAID.ToString());
                                             

                                            }
                                            CITL.SampleType = Convert.ToString(tt.SampleType);
                                            CITL.UnitCode = Convert.ToString(Session["UnitCode"]);
                                            CITL.MTCode = dr["MTCode"].ToString();
                                            CITL.PID = PID;
                                            CITL.PatRegID = Request.QueryString["PatRegID"].Trim();
                                            CITL.FID = Request.QueryString["FID"].Trim();
                                            CITL.TestRate = Convert.ToSingle(gvr.Cells[4].Text);
                                            CITL.ClientTestRate = Convert.ToSingle(gvr.Cells[5].Text);
                                            TextBox txtModifyRate = gvr.FindControl("txtModifyRate") as TextBox;
                                            if (txtModifyRate.Text.Trim() == "")
                                            {
                                                txtModifyRate.Text = "0";
                                            }
                                            if (txtModifyRate.Text != "0")
                                            {
                                                CITL.TestRate = Convert.ToSingle(txtModifyRate.Text);
                                                CITL.RateModifyBy = Session["username"].ToString();
                                            }
                                            else
                                            {
                                                CITL.RateModifyBy = "";
                                            }
                                            CITL.UnitCode = "";
                                          // Label lbltest = GV_Phlebotomist.Rows[i].FindControl("lblTest") as Label;
                                            if (DoctorCode[1].ToString() == "")
                                            { CITL.P_doctoramount = 0; }
                                            else
                                            {
                                                CITL.P_doctoramount = CITL.Get_ShareMst_amount(DoctorCode[1].ToString(), gvr.Cells[1].Text.Trim(), Session["Branchid"].ToString());
                                                if (CITL.P_perdis > 1 && CITL.P_doctoramount < 1)
                                                {
                                                    CITL.P_doctoramount = (CITL.TestRate * CITL.P_perdis / 100);
                                                }
                                            }
                                            CITL.Update(CITL.PID, CITL.MTCode, Convert.ToInt32(Session["Branchid"]), 0);

                                        }
                                        catch { }
                                    }
                                    dr.Close();
                                    conn.Close(); conn.Dispose();
                                    #endregion
                                }

                                else
                                {
                                    #region For Test
                                    try
                                    {
                                        DrMT_Bal_C drTable = new DrMT_Bal_C(Request.QueryString["Center"].Trim(), "CC", Convert.ToInt32(Session["Branchid"]));
                                        Patmstd_Bal_C CITL = new Patmstd_Bal_C();

                                        MainTest_Bal_C tt = new MainTest_Bal_C(gvr.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));

                                        //foreach (GridViewRow gvr1 in GrdPackage.Rows)
                                        //{
                                        //    if (gvr1.Cells[0].Text == tt.SampleType)
                                        //    {
                                        //        CITL.Barcodeid = (gvr1.FindControl("txtbarcodeid") as TextBox).Text;
                                        //        CITL.UnitCode = Convert.ToString(Session["UnitCode"] );
                                             
                                        //    }
                                        //}
                                       // if (BarcodeLogic_C.RecordExistsforsample(PID, gvr.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"])))
                                        if (BarcodeLogic_C.RecordExistsforsample(PID, tt.SampleType, Convert.ToInt32(Session["Branchid"])))
                                        {
                                            string BIDD = BarcodeLogic_C.getbarcodeIDBySampleType(tt.SampleType.Trim(), PID, Convert.ToInt32(Session["Branchid"]));
                                            CITL.Barcodeid = BIDD.ToString();
                                        }
                                        else
                                        {
                                            string BIDD = BarcodeLogic_C.getSampleType_ID(tt.SampleType, Convert.ToInt32(Session["Branchid"]));
                                            CITL.Barcodeid = Convert.ToString(Request.QueryString["PatRegID"].Trim() + "" + BIDD.ToString());
                                           // CITL.Barcodeid = Convert.ToString(Request.QueryString["PatRegID"].Trim() + "" + vi.ToString()).PadLeft(10, '0');

                                        }
                                        CITL.SampleType = Convert.ToString(tt.SampleType);
                                        ViewState["PatRegID"] = Patmst.PatRegID.ToString();
                                        ViewState["io"] = Patmst.FID.ToString();
                                        CITL.PatRegID = "";
                                        CITL.FID = "";
                                        CITL.UnitCode = "";
                                        CITL.MTCode = gvr.Cells[1].Text.Trim();
                                        CITL.SDCode = tt.SDCode;
                                        CITL.TestRate = Convert.ToSingle(gvr.Cells[4].Text);
                                        CITL.ClientTestRate = Convert.ToSingle(gvr.Cells[5].Text);
                                        CITL.PID = PID;
                                        CITL.PatRegID = Request.QueryString["PatRegID"].Trim();
                                        CITL.FID = Request.QueryString["FID"].Trim();
                                        TextBox txtModifyRate = gvr.FindControl("txtModifyRate") as TextBox;
                                        if (txtModifyRate.Text.Trim() == "")
                                        {
                                            txtModifyRate.Text = "0";
                                        }
                                        if (txtModifyRate.Text != "0")
                                        {
                                            CITL.TestRate = Convert.ToSingle(txtModifyRate.Text);
                                            CITL.RateModifyBy = Session["username"].ToString();
                                        }
                                        else
                                        {
                                            CITL.RateModifyBy = "";
                                        }
                                        if (DoctorCode[1].ToString() == "")
                                        { CITL.P_doctoramount = 0; }
                                        else
                                        {
                                            CITL.P_doctoramount = CITL.Get_ShareMst_amount(DoctorCode[1].ToString(), gvr.Cells[1].Text.Trim(), Session["Branchid"].ToString());
                                            if (CITL.P_perdis > 1 && CITL.P_doctoramount < 1)
                                            {
                                                CITL.P_doctoramount = (CITL.TestRate * CITL.P_perdis / 100);
                                            }
                                        }
                                        CITL.Update(CITL.PID, CITL.MTCode, Convert.ToInt32(Session["Branchid"]), 0);

                                    }
                                    catch { }
                                    #endregion
                                }
                                #endregion
                            }
                            else
                            {
                                #region For Insert
                                if (gvr.Cells[1].Text.Trim().Length == 4)
                                {
                                    SqlConnection conn = DataAccess.ConInitForDC();
                                    SqlCommand cmd = new SqlCommand("select MTCode from PackmstD where PackageCode='" + gvr.Cells[1].Text.Trim() + "'", conn);
                                    conn.Open();
                                    SqlDataReader dr = cmd.ExecuteReader();
                                    while (dr.Read())
                                    {
                                        try
                                        {
                                            DrMT_Bal_C drTable = new DrMT_Bal_C(Request.QueryString["Center"].Trim(), "CC", Convert.ToInt32(Session["Branchid"]));

                                            Patmstd_Bal_C CITL = new Patmstd_Bal_C();

                                            MainTest_Bal_C tt = new MainTest_Bal_C(dr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));

                                            //foreach (GridViewRow gvr1 in GrdPackage.Rows)
                                            //{
                                            //    if (gvr1.Cells[0].Text == tt.SampleType)
                                            //    {
                                            //        CITL.Barcodeid = (gvr1.FindControl("txtbarcodeid") as TextBox).Text;
                                            //        CITL.UnitCode = Convert.ToString(Session["UnitCode"] );
                                                  
                                            //    }
                                            //}
                                            if (BarcodeLogic_C.RecordExistsforsample(PID, tt.SampleType, Convert.ToInt32(Session["Branchid"])))
                                            {
                                                string BIDD = BarcodeLogic_C.getbarcodeIDBySampleType(tt.SampleType, PID, Convert.ToInt32(Session["Branchid"]));
                                                CITL.Barcodeid = BIDD.ToString();
                                            }
                                            else
                                            {
                                                string BIDD = BarcodeLogic_C.getSampleType_ID(tt.SampleType, Convert.ToInt32(Session["Branchid"]));
                                               // CITL.Barcodeid = Request.QueryString["PatRegID"].Trim() + "" + BIDD.ToString();
                                                CITL.Barcodeid = Convert.ToString( Request.QueryString["PatRegID"].Trim() + "" + BIDD.ToString());

                                            }
                                            CITL.SampleType = Convert.ToString(tt.SampleType);
                                            CITL.UnitCode = Convert.ToString(Session["UnitCode"]);
                                            CITL.MTCode = dr["MTCode"].ToString();
                                            CITL.SDCode = tt.SDCode;
                                            CITL.TestRate = Convert.ToSingle(gvr.Cells[4].Text);
                                            CITL.ClientTestRate = Convert.ToSingle(gvr.Cells[5].Text);
                                            CITL.PackageCode = gvr.Cells[1].Text.Trim();
                                            CITL.PID = PID;
                                            CITL.PatRegID = Request.QueryString["PatRegID"].Trim();
                                            CITL.FID = Request.QueryString["FID"].Trim();
                                            TextBox txtModifyRate = gvr.FindControl("txtModifyRate") as TextBox;
                                            if (txtModifyRate.Text.Trim() == "")
                                            {
                                                txtModifyRate.Text = "0";
                                            }
                                            if (txtModifyRate.Text != "0")
                                            {
                                                CITL.TestRate = Convert.ToSingle(txtModifyRate.Text);
                                                CITL.RateModifyBy = Session["username"].ToString();
                                            }
                                            else
                                            {
                                                CITL.RateModifyBy = "";
                                            }
                                            if (DoctorCode[1].ToString() == "")
                                            { CITL.P_doctoramount = 0; }
                                            else
                                            {
                                                CITL.P_doctoramount = CITL.Get_ShareMst_amount(DoctorCode[1].ToString(), gvr.Cells[1].Text.Trim(), Session["Branchid"].ToString());
                                                if (CITL.P_perdis > 1 && CITL.P_doctoramount < 1)
                                                {
                                                    CITL.P_doctoramount = (CITL.TestRate * CITL.P_perdis / 100);
                                                }
                                            }
                                            if (BarcodeLogic_C.RecordExistsforTest(PID, dr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"])))
                                            {
                                                
                                            }
                                            else
                                            {
                                                CITL.Username = Session["username"].ToString();
                                                CITL.Insert(Convert.ToInt32(Session["Branchid"]));
                                            }

                                        }
                                        catch { }
                                    }
                                    dr.Close();
                                    conn.Close(); conn.Dispose();
                                }
                                else
                                {
                                    try
                                    {
                                        DrMT_Bal_C drTable = new DrMT_Bal_C(Request.QueryString["Center"].Trim(), "CC", Convert.ToInt32(Session["Branchid"]));
                                        Patmstd_Bal_C CITL = new Patmstd_Bal_C();

                                        MainTest_Bal_C tt = new MainTest_Bal_C(gvr.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));

                                        //foreach (GridViewRow gvr1 in GrdPackage.Rows)
                                        //{
                                        //    if (gvr1.Cells[0].Text == tt.SampleType)
                                        //    {
                                        //        CITL.Barcodeid = (gvr1.FindControl("txtbarcodeid") as TextBox).Text;
                                        //        CITL.UnitCode = Convert.ToString(Session["UnitCode"] );
                                              
                                        //        break;
                                        //    }

                                        //    if (BarcodeLogic_C.RecordExistsforsample(PID, gvr1.Cells[0].Text.Trim(), Convert.ToInt32(Session["Branchid"])))
                                        //    {
                                        //        string vi = BarcodeLogic_C.getbarcodeIDBySampleType(gvr1.Cells[0].Text, PID, Convert.ToInt32(Session["Branchid"]));
                                        //        CITL.Barcodeid = vi.ToString();
                                        //    }
                                        //    else
                                        //    {
                                        //        CITL.Barcodeid = "";
                                        //    }
                                        //}

                                        if (BarcodeLogic_C.RecordExistsforsample(PID, tt.SampleType, Convert.ToInt32(Session["Branchid"])))
                                        {
                                            string BIDD = BarcodeLogic_C.getbarcodeIDBySampleType(tt.SampleType, PID, Convert.ToInt32(Session["Branchid"]));
                                            CITL.Barcodeid = BIDD.ToString();
                                        }
                                        else
                                        {
                                            string BIDD = BarcodeLogic_C.getSampleType_ID(tt.SampleType, Convert.ToInt32(Session["Branchid"]));
                                           // CITL.Barcodeid = Request.QueryString["PatRegID"].Trim() + "" + BIDD.ToString();
                                            CITL.Barcodeid = Convert.ToString( Request.QueryString["PatRegID"].Trim() + "" + BIDD.ToString());

                                        }
                                        CITL.SampleType = Convert.ToString(tt.SampleType);
                                       
                                        CITL.UnitCode = Convert.ToString(Session["UnitCode"]);
                                        ViewState["PatRegID"] = Patmst.PatRegID.ToString();
                                        ViewState["io"] = Patmst.FID.ToString();
                                        CITL.PatRegID = "";
                                        CITL.FID = "";

                                        CITL.MTCode = gvr.Cells[1].Text.Trim();
                                        CITL.SDCode = tt.SDCode;
                                        CITL.TestRate = Convert.ToSingle(gvr.Cells[4].Text);
                                        CITL.ClientTestRate = Convert.ToSingle(gvr.Cells[5].Text);
                                        CITL.PID = PID;
                                        CITL.PatRegID = Request.QueryString["PatRegID"].Trim();
                                        CITL.FID = Request.QueryString["FID"].Trim();
                                        TextBox txtModifyRate = gvr.FindControl("txtModifyRate") as TextBox;
                                        if (txtModifyRate.Text.Trim() == "")
                                        {
                                            txtModifyRate.Text = "0";
                                        }
                                        if (txtModifyRate.Text != "0")
                                        {
                                            CITL.TestRate = Convert.ToSingle(txtModifyRate.Text);
                                            CITL.RateModifyBy = Session["username"].ToString();
                                        }
                                        else
                                        {
                                            CITL.RateModifyBy = "";
                                        }
                                        if (DoctorCode[1].ToString() == "")
                                        { CITL.P_doctoramount = 0; }
                                        else
                                        {
                                            CITL.P_doctoramount = CITL.Get_ShareMst_amount(DoctorCode[1].ToString(), gvr.Cells[1].Text.Trim(), Session["Branchid"].ToString());
                                            if (CITL.P_perdis > 1 && CITL.P_doctoramount < 1)
                                            {
                                                CITL.P_doctoramount = (CITL.TestRate * CITL.P_perdis / 100);
                                            }
                                        }
                                       // CITL.Insert(Convert.ToInt32(Session["Branchid"]));
                                        if (BarcodeLogic_C.RecordExistsforTest(PID, CITL.MTCode, Convert.ToInt32(Session["Branchid"])))
                                        {

                                        }
                                        else
                                        {
                                            CITL.Username = Session["username"].ToString();
                                            CITL.Insert(Convert.ToInt32(Session["Branchid"]));
                                        }

                                    }
                                    catch { }
                                }
                                #endregion
                            }
                        }
                    }

                  //  Barcode_C vm1 = new Barcode_C();

                    string fID = Convert.ToString(FinancialYearTableLogic.getCurrentFinancialYear(Convert.ToInt32(Session["Branchid"])).FinancialYearId);
                  
                    string FID = fID ;
                    string PatRegID = Cshmst_Bal_C.get_RegNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["PID"]));
                    string BarcodeGenerate = "";
                 
                    Patmstd_Main_Bal_C psM = new Patmstd_Main_Bal_C();
                    PatSt_Bal_C ps = new PatSt_Bal_C();
                    int M = 0;
                  

                    Ledgrmst_Bal_C led = new Ledgrmst_Bal_C();
                    PatientBillTransactionTableLogic_Bal_C objBilltrans = new PatientBillTransactionTableLogic_Bal_C();
                  
                        Cshmst_Bal_C Obj_CSH_C = new Cshmst_Bal_C();
                        Obj_CSH_C.P_Centercode = Session["CenterCode"].ToString();
                        Obj_CSH_C.RecDate = Convert.ToDateTime(Date.getdate().Date.ToString("dd/MM/yyyy"));
                        Obj_CSH_C.BillType = "Cash Bill";
                        Obj_CSH_C.AmtReceived = Convert.ToSingle(0);
                       
                           
                            Obj_CSH_C.NetPayment = Convert.ToSingle(lblTotTestAmt.Text);
                            Obj_CSH_C.Discount = "0";
                        
                        if (Convert.ToInt32(ViewState["PID"]) == 0)
                        {
                            return;
                        }
                        string Patientregno = Cshmst_Bal_C.get_RegNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["PID"]));
                        Obj_CSH_C.patRegID = Convert.ToString(Patientregno);
                        Obj_CSH_C.Patientname = txtFname.Text;

                        string Patienttest = "";
                       
                        Obj_CSH_C.Remark = "";
                        if (Convert.ToInt32(ViewState["PID"]) == 0)
                        {
                            return;
                        }
                        Obj_CSH_C.PID = Convert.ToInt32(ViewState["PID"]);                      
                        Obj_CSH_C.AmtPaid = 0;                       
                        Obj_CSH_C.Balance = Convert.ToSingle(lblTotTestAmt.Text);
                        Obj_CSH_C.Paymenttype = "Cash";

                       
                        Obj_CSH_C.username = Session["username"].ToString();
                        Obj_CSH_C.Othercharges = 0;
                       
                        Obj_CSH_C.DisFlag = true;

                        Obj_CSH_C.P_DigModule = Convert.ToInt32(Session["DigModule"]);
                      
                        int mno = Cshmst_Bal_C.get_Existbillno(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["PID"]));
                        bno = mno ;
                        Obj_CSH_C.BillNo = bno;

                        Obj_CSH_C.P_Hstper = Convert.ToSingle(Session["Taxper"]);
                        Obj_CSH_C.P_Hstamount = Convert.ToSingle(0);


                        string BillFormatstr = "";
                        led.CreditAmt = 0;
                        led.DebitAmt = 0;
                     

                        led.CenterCode = Session["CenterCode"].ToString();
                        led.BillNo = bno;
                        led.BillFormat = BillFormatstr.ToString();
                  
                        DataTable dtgap = new DataTable();
                        dtgap = led.GetAmountPaid(Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), bno);
                        float Billamount = 0, BillamountPrev=0;
                    
                        if (Convert.ToSingle(dtgap.Rows.Count) > 0)
                        {
                            led.UpdateBillPreviewBal_Editdemo(Convert.ToInt32(bno), Convert.ToInt32(Session["Branchid"]), Session["username"].ToString(), Convert.ToSingle(lblTotTestAmt.Text));
                            Billamount = 0;
                            Billamount = Convert.ToSingle(lblTotTestAmt.Text) - Convert.ToSingle(dtgap.Rows[0]["Amtpaid"]);
                            BillamountPrev = Convert.ToSingle(dtgap.Rows[0]["BillAmt"]);
                        }
                        else
                        {
                            Billamount = Convert.ToSingle(lblTotTestAmt.Text);

                            
                        }
                       
                            if (Convert.ToSingle(ViewState["Previousamount"]) != BillamountPrev)
                            {

                                SqlConnection conn1 = DataAccess.ConInitForDC();
                                SqlCommand sc = new SqlCommand("insert into RecM(ReceiptNo,BillNo,billdate,AmtPaid,Paymenttype,BankName,branchid,transdate,username,BillAmt,DisAmt,BalAmt,PID,PrevBal,TaxPer,TaxAmount)" +
                                "values(@ReceiptNo,@BillNo,@billdate,@AmtPaid,@Paymenttype,@BankName,@branchid,@transdate,@username,@BillAmt,@DisAmt,@BalAmt,@PID,@PrevBal,@TaxPer,@TaxAmount)", conn1);
                                sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;// Convert.ToInt32(lblBillNo.Text);

                                sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0;
                                sc.Parameters.Add(new SqlParameter("@billdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(Date.getdate().Date.ToString("dd/MM/yyyy"));
                                sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = "Cash";
                                sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 50)).Value = "";
                                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                                sc.Parameters.Add(new SqlParameter("@transdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy"));//Convert.ToDateTime(System.DateTime.Now);
                                sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = Session["username"].ToString();
                                sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = Convert.ToSingle(0);
                                sc.Parameters.Add(new SqlParameter("@DisAmt", SqlDbType.Float)).Value = Convert.ToDouble(0);
                                sc.Parameters.Add(new SqlParameter("@BalAmt", SqlDbType.Float)).Value = Convert.ToDouble(Billamount);
                                sc.Parameters.AddWithValue("@PID", Convert.ToInt32(ViewState["PID"]));

                                sc.Parameters.Add(new SqlParameter("@PrevBal", SqlDbType.Float)).Value = Convert.ToDouble(Billamount);
                                sc.Parameters.Add(new SqlParameter("@TaxPer", SqlDbType.Float)).Value = Convert.ToDouble(Session["Taxper"]);
                                sc.Parameters.Add(new SqlParameter("@TaxAmount", SqlDbType.Float)).Value = Convert.ToDouble(0);
                                if (Convert.ToSingle(dtgap.Rows.Count) > 0)
                                {
                                    sc.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.Int)).Value = Convert.ToInt32(dtgap.Rows[0]["ReceiptNo"]);
                                }
                                else
                                {
                                    sc.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.Int)).Value = Convert.ToInt32(0);
                                }
                                conn1.Open();
                                sc.ExecuteNonQuery();
                            }
                        
                    //}

                    //==================***=================================
                }
                ObjTB.P_Patregno = Request.QueryString["PatRegID"].Trim(); ;
                ObjTB.P_FormName = "Edit Registration";
                ObjTB.P_EventName = "Update Test/Demography";
                ObjTB.P_UserName = Convert.ToString(Session["username"]);
                ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                ObjTB.Insert_DailyActivity();
            }
        //    Barcode_C vmT = new Barcode_C();
        //    DataTable dtbal=new DataTable ();
        //   dtbal = vmT.UpdateAddNewTestBalance(Convert.ToString(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]));
        //   float PBal = 0;   
        //  if (dtbal.Rows.Count > 0)
        //   {
        //       PBal = Convert.ToSingle(lblTotTestAmt.Text) - Convert.ToSingle(dtbal.Rows[0]["recamount"]);
        //       if (PBal < 0)
        //       {
        //          // PBal = PBal * -1;
        //       }

        //   }
          
       //vmT.Update_Cshmst(Convert.ToInt32(ViewState["PID"]), Convert.ToString(ViewState["TCode"]), Convert.ToSingle(lblTotTestAmt.Text),Convert.ToSingle(PBal));
 
        //string regNo1 = Request.QueryString["PatRegID"].Trim();
           
        //    if (Request.QueryString["formname"] != null)
        //    {
        //        string ff = Request.QueryString["formname"].ToString();
        //    }
        //    btnSubmit.Enabled = false;
        //    if (Convert.ToSingle(dtbal.Rows.Count) > 0)
        //    {
        //        if (Convert.ToSingle(lblTotTestAmt.Text) != Convert.ToSingle(dtbal.Rows[0]["NetPayment"]))
        //        {
        //            Patmstd_Bal_C PBCL = new Patmstd_Bal_C();
        //            string CCount = PBCL.GetSMSString_CountryCode("Registration", Convert.ToInt16(Session["Branchid"]));
        //            int CountryC = 0;
        //            if (CCount.Length == 2)
        //            {
        //                PatientBillCancel_C ObjPBC = new PatientBillCancel_C();
        //                ObjPBC.P_PID = Convert.ToInt32(ViewState["PID"]);
        //                ObjPBC.P_NetAmount = Convert.ToSingle(lblTotTestAmt.Text);
        //                ObjPBC.BillDisactive_SingleTest();
        //            }
        //        }
        //    }
        //----------------------------- Report Comment ---------------------------
           // SqlConnection conn12 = DataAccess.ConInitForDC();
           // SqlCommand sc1 = conn12.CreateCommand();
           //// PatSt_Bal_C PBC = new PatSt_Bal_C();
           // sc1.CommandText = "ALTER VIEW [dbo].[VW_cshbill_ClickSample] AS (SELECT        RecM.BillNo, patmst.Patregdate AS RecDate, 'Cash Bill' AS BillType, RecM.AmtPaid AS AmtReceived, RecM.DisAmt AS Discount, patmst.TestCharges AS NetPayment, RecM.AmtPaid, " +
           //                  "   dbo.FUN_GetReceiveAmt_Balance(1, RecM.PID) AS Balance, RecM.username, RecM.OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, " +
           //                   "  RecM.CardNo AS DoctorName, MainTest.Maintestname, MainTest.MTCode, patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag, patmst.Patusername, patmst.Patpassword, RecM.Comment, " +
           //                   "  patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, RecM.PaymentType AS UnitCode, RecM.TaxAmount, RecM.TaxPer, 1 AS PrintCount,  " +
           //                   "  patmst.OtherRefDoctor, RecM.ReceiptNo, RecM.OtherChargeRemark, RecM.PID  , DrMT.address1 as CenterAddress,patmst.QRImage, patmst.QRImageD,Patmst.PatientcHistory,CONVERT(varchar, patmst.DOB, 105) AS DOB, CONVERT(varchar,  patmst.PatDatOfBirth, 105) AS PatDatOfBirth,patmst.UploadPrescription " +
           //                   "  FROM            patmst INNER JOIN " +
           //                   "  DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN " +
           //                   "  MainTest INNER JOIN " +
           //                   "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN " +
           //                   "  RecM ON patmst.PID = RecM.PID LEFT OUTER JOIN " +
           //                   "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode " +
           //          "   where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and patmstd.PID='" + ViewState["PID"] + "' )";// and Cshmst.BillNo=" + bno + "
           // conn12.Open();
           // sc1.ExecuteNonQuery();
           // conn12.Close(); conn12.Dispose();

            
                btnbarcodeEntry.Enabled = true;
                btnpatientcard.Enabled = true;
                btnbprint.Enabled = true;

                //Session.Add("rptsql", "");
                //Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PayReceipt_clickSample.rpt");
                //Session["reportname"] = "CashReceipt_ClickSample";
                //Session["RPTFORMAT"] = "pdf";

                //ReportParameterClass.SelectionFormula = "";
                //string close = "<script language='javascript'>javascript:OpenReport();</script>";
                //Type title1 = this.GetType();
                //Page.ClientScript.RegisterStartupScript(title1, "", close);
                Session["PID_report"] = Convert.ToInt32(ViewState["PID"]);
                Session["RecNo_report"] = 0;
                ViewState["receipt"] = "No";
                // ViewState["PID"] = "";
            
            ClearControls();
            GrdPackage.DataBind();
            grdTests.DataSource = null;
            grdTests.DataBind();

            lblText.Text = "Record Update Successfully.";
    }

    protected void ddlProfile_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GrdPackage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;

        if (Request.QueryString["PID"] != null && Request.QueryString["FType"] != null)
        {
            if (Request.QueryString["FType"].ToString() == "Edit")
            {
                if (e.Row.Cells[0].Text != "")
                {
                    (e.Row.FindControl("txtbarcodeid") as TextBox).Text = BarcodeLogic_C.getbarcodeIDBySampleType(e.Row.Cells[0].Text.Trim(), Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]));
                }
                if (ViewState["barid"] == null)
                {
                    ViewState["barid"] = (e.Row.FindControl("txtbarcodeid") as TextBox).Text;
                }
                else
                {
                    ViewState["barid"] = ViewState["barid"].ToString() + "," + (e.Row.FindControl("txtbarcodeid") as TextBox).Text;
                }
                int PID = Convert.ToInt32(Request.QueryString["PID"].ToString());
                string[] MTCode = (e.Row.Cells[1].Text).Split(',');
              
            }
        }
        if (Request.QueryString["Center"] != null && Request.QueryString["PID"] != null)
        {
            DropDownList ddlprocessunit = e.Row.FindControl("ddllab") as DropDownList;
            ddlprocessunit.DataSource = DrMT_sign_Bal_C.getLab(Convert.ToInt32(Session["Branchid"]));
            ddlprocessunit.DataTextField = "Name";
            ddlprocessunit.DataValueField = "DoctorCode";
            ddlprocessunit.DataBind();
            TextBox vial = e.Row.FindControl("txtbarcodeid") as TextBox;
            if (vial.Text != "")
            {
                string tcode = e.Row.Cells[1].Text;
                string labcd = Patmstd_Main_Bal_C.Get_Labcode_by_ID(Convert.ToInt32(Request.QueryString["PID"]), tcode, Convert.ToInt32(Session["Branchid"]));
                ddlprocessunit.SelectedValue = labcd;
            }
            else
            {
                ddlprocessunit.SelectedValue = Patmst_New_Bal_C.GetCenter_Labcode(Request.QueryString["Center"].Trim(), Convert.ToInt32(Session["Branchid"]));
            }
        }

    }


 
    void ClearControls()
    {
        cmbInitial.SelectedIndex = -1;
        txtFname.Text = "";
        txtsex.Text = "Male";
       
        txtAge.Text = "";
        cmdYMD.SelectedIndex = -1;       
        txtDoctorName.Text = "";
        txtsex.Text = "";
        txt_address.Text = "";
        txt_clinicalhistory.Text = "";
        txt_remark.Text = "";
       
        txtemail.Text = "";
        ViewState["tableSample"] = null;
        ViewState["table"] = null;
        txttelno.Text = "";
        txtemail.Text = "";

    }
    protected void grdTests_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex == -1)
            return;
        Stformmst_Bal_C obTemp = new Stformmst_Bal_C();
        PatSt_Bal_C objPrintTbl = new PatSt_Bal_C();
        DataTable table = (DataTable)ViewState["table"];
        DataTable tableSample = (DataTable)ViewState["tableSample"];
        ViewState["barid"] = "";
        objPrintTbl.Update_isRefund(Convert.ToInt32(Session["Branchid"]), Request.QueryString["PID"]);
        foreach (DataRow dr in table.Rows)
        {
           
            if (dr["MTCode"].ToString() == grdTests.DataKeys[e.RowIndex].Value.ToString())
            {
                objPrintTbl.Insert_utd_Table(Convert.ToInt32(Session["Branchid"]), grdTests.DataKeys[e.RowIndex].Value.ToString(), Convert.ToString(Session["username"]), "", Convert.ToString(Request.QueryString["PatRegID"]));

                dr.Delete();
               string MTCode = grdTests.DataKeys[e.RowIndex].Value.ToString();
                string PatRegID = Convert.ToString(Request.QueryString["PatRegID"]);
                string FID = Convert.ToString(Request.QueryString["FID"]);
                 if (MTCode.Length !=4)
                {
                    obTemp.Delete_ResMst_Code(PatRegID, FID, Convert.ToInt32(Session["Branchid"]), MTCode);
                    objPrintTbl.Delete_Patst_Code(PatRegID, FID, MTCode, Convert.ToInt32(Session["Branchid"]));
                }
                else
                {
                    DataTable dt = new DataTable();
                    Package_Bal_C obGroup = new Package_Bal_C();
                    dt = obGroup.getAllTestCodes_ForPAckage(MTCode, Convert.ToInt32(Session["Branchid"]));
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string mtCode = Convert.ToString(dt.Rows[i]["MTCode"]);
                            obTemp.Delete_ResMst_Code(PatRegID, FID, Convert.ToInt32(Session["Branchid"]), mtCode);
                            objPrintTbl.Delete_Patst_Code(PatRegID, FID, mtCode, Convert.ToInt32(Session["Branchid"]));
                        }
                    }
                }
                break;
            }
        }
        foreach (DataRow dr in tableSample.Rows)
        {
            if (dr["STCode"].ToString() == grdTests.DataKeys[e.RowIndex].Value.ToString())
            {
                dr.Delete();
                break;
            }
            
        }

        grdTests.DataSource = table;
        grdTests.DataBind();
        GrdPackage.DataSource = tableSample;
        GrdPackage.DataBind();

        ViewState["table"] = table;
     
        ViewState["barid"] = "";
        foreach (GridViewRow gvr in GrdPackage.Rows)
        {
            if ((gvr.FindControl("txtbarcodeid") as TextBox).Text != "")
            {
                if (ViewState["barid"] == "")
                {
                    ViewState["barid"] = (gvr.FindControl("txtbarcodeid") as TextBox).Text;
                }
                else
                {
                    ViewState["barid"] = ViewState["barid"].ToString() + "," + (gvr.FindControl("txtbarcodeid") as TextBox).Text;
                }
            }
        }
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
        ObjTB.P_Patregno = Request.QueryString["PatRegID"].Trim();
        ObjTB.P_FormName = "Edit Registration";
        ObjTB.P_EventName = "Delete Test";
        ObjTB.P_UserName = Convert.ToString(Session["username"]);
        ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
        ObjTB.Insert_DailyActivity();

        lblTotTestAmt.Text = amtTestTot.ToString();

    }

   
    protected void grdSelectedProfiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
   
   
    protected void radioCode_CheckedChanged(object sender, EventArgs e)
    {

    }
    void AddTestToTable(string MTCode)
    {
        try
        {
            DataTable table = (DataTable)ViewState["table"];
            DataRow row;
            row = table.NewRow();
            row["MTCode"] = MTCode;
            if (MTCode.Trim().Length == 4)
            {
                row["Maintestname"] = Packagenew_Bal_C.getPNameByCode(row["MTCode"].ToString().Trim(), Convert.ToInt32(Session["Branchid"]));
            }
            else
            {
                row["Maintestname"] = TestAssignLog_Bal_C.Get_Maintest_Code(row["MTCode"].ToString().Trim(), Convert.ToInt32(Session["Branchid"]));
                if (row["Maintestname"].ToString() == "")
                    return;
            }
            if (Session["username"] != null)
            {
                double Rate = 0.0, ClientRate = 0.0;
                SpeCh_Bal_C spTable = null;

                DrMT_Bal_C drTable = new DrMT_Bal_C(Request.QueryString["Center"].Trim(), "CC", Convert.ToInt32(Session["Branchid"]));
                if (Patmstd_Bal_C.Get_ExistTestCode(Convert.ToInt32(Request.QueryString["PID"].ToString()), MTCode, Convert.ToInt32(Session["Branchid"])) == true)
                {
                    Rate = Patmst_New_Bal_C.Get_TestRate_ForCode(Convert.ToInt32(Request.QueryString["PID"].ToString()), MTCode, Convert.ToInt32(Session["Branchid"]));
                    ClientRate = Patmst_New_Bal_C.Get_ClientTestRate_ForCode(Convert.ToInt32(Request.QueryString["PID"].ToString()), MTCode, Convert.ToInt32(Session["Branchid"]));
                }
                else
                {
                    SpeCh_Bal_C sp = new SpeCh_Bal_C(MTCode, drTable.ratetypeid, Convert.ToInt32(Session["Branchid"]));                 
                    Rate = Convert.ToDouble(sp.Amount.ToString().Trim());

                    SpeCh_Bal_C sp1 = new SpeCh_Bal_C(MTCode, drTable.ratetypeid, Convert.ToInt32(Session["Branchid"]));
                    ClientRate = Convert.ToDouble(sp1.Amount.ToString().Trim());

                }
                if (Rate != 0.0)
                {
                    row["Amount"] = Rate;
                }
                else
                {
                    row["Amount"] = "0";
                }
                if (ClientRate != 0.0)
                {
                    row["clientAmount"] = ClientRate;
                }
                else
                {
                    row["clientAmount"] = "0";
                }
            }
            else
            {
                row["Amount"] = "0";
                row["clientAmount"] = "0";
            }

            table.Rows.Add(row);
            ViewState["table"] = table;
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

    protected void grdTests_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;

        if (e.Row.Cells[1].Text.Trim().Length == 4)
        {
            (e.Row.FindControl("lblPorT") as Label).Text = "Package";
            IEnumerator ie = PackageL_Bal_C.Get_PackageDetails(e.Row.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"])).GetEnumerator();
            while (ie.MoveNext())
            {
                MainTest_Bal_C tt = new MainTest_Bal_C((ie.Current as Package_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                try
                {
                    PatSt_Bal_C ps = new PatSt_Bal_C(Request.QueryString["PatRegID"].Trim(), Request.QueryString["FID"].Trim(), (ie.Current as Package_Bal_C).PackageCode, 0, 0, Convert.ToInt32(Session["Branchid"]));
                  
                  if ( ps.Patauthicante == "Authorized")
                    {
                        if (e.Row.Cells[0].HasControls())
                        {
                            (e.Row.Cells[0].Controls[0] as LinkButton).Text = "";
                            break;
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
        else
        {
            (e.Row.FindControl("lblPorT") as Label).Text = "Test";
            MainTest_Bal_C tt = new MainTest_Bal_C(e.Row.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));
            try
            {
                PatSt_Bal_C ps = new PatSt_Bal_C(Request.QueryString["PatRegID"].Trim(), Request.QueryString["FID"].Trim(), e.Row.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));
               
            
              if ( ps.Patauthicante == "Authorized")
                {
                    if (e.Row.Cells[0].HasControls())
                    {
                        (e.Row.Cells[0].Controls[0] as LinkButton).Text = "";
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
       // e.Row.Cells[0].Text = "";

    }

    [AjaxPro.AjaxMethod()]
    public string GetItemByName(string sTLCode, string sType)
    {
        string str = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc;
        if (Session["DigModule"] != null)
        {
            if (sType == "Code")
                sc = new SqlCommand("select PackageCode as MTCode,PackageName as Maintestname from PackMst where PackageName like @MTCode and PackageCode in (select PackageCode from PackmstD  where SDCode in (select SDCode from SubDepartment where DigModule ='" + Session["DigModule"] + "')) UNION  select MTCode, Maintestname from MainTest WHERE MTCode like @MTCode and SDCode in (select SDCode from SubDepartment where DigModule='" + Session["DigModule"] + "')", conn);
            else
                sc = new SqlCommand("select PackageCode as MTCode,PackageName as Maintestname from PackMst where PackageName like @MTCode and PackageCode in (select PackageCode from PackmstD where SDCode in (select SDCode from SubDepartment where DigModule ='" + Session["DigModule"] + "')) UNION  select MTCode, Maintestname from MainTest WHERE Maintestname like @MTCode and SDCode in (select SDCode from SubDepartment where DigModule='" + Session["DigModule"] + "')", conn);
        }
        else
        {
            if (sType == "Code")
                sc = new SqlCommand(" select PackageCode as MTCode, PackageName as Maintestname from PackMst WHERE PackageCode like @MTCode UNION " +
                                   " select MTCode, Maintestname from MainTest WHERE MTCode like @MTCode", conn);
            else
                sc = new SqlCommand("select PackageCode as MTCode, PackageName as Maintestname from PackMst WHERE PackageCode like @MTCode UNION " +
                                   " select MTCode, Maintestname from MainTest WHERE Maintestname like @MTCode", conn);
        }


        if (sType == "Code")
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 5)).Value = sTLCode + "%";
        else
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 200)).Value = sTLCode + "%";

        conn.Open();
        SqlDataReader dr = sc.ExecuteReader();
        while (dr.Read())
        {
            if (sType == "Code")
            {
                if (str == "")
                {
                    str = dr["MTCode"] + "#" + dr["MTCode"] + " - " + dr["Maintestname"] + "#";
                }
                else
                {
                    str = str + dr["MTCode"] + "#" + dr["MTCode"] + " - " + dr["Maintestname"] + "#";
                }
            }
            else
            {
                if (str == "")
                {
                    str = dr["MTCode"] + "#" + dr["Maintestname"] + " - " + dr["MTCode"] + "#";
                }
                else
                {
                    str = str + dr["MTCode"] + "#" + dr["Maintestname"] + " - " + dr["MTCode"] + "#";
                }
            }

        }
        dr.Close();
        return str;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTests(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        //if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
        //    sda = new SqlDataAdapter("select PackageCode as MTCode,PackageName as Maintestname from PackMst where (PackageCode like '%" + prefixText + "%' or PackageName like '%" + prefixText + "%')  UNION " + //and PackageCode in (select PackageCode from PackmstD where SDCode in (select SDCode from SubDepartment where DigModule ='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "')
        //                      " select MTCode, Maintestname from MainTest WHERE (MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%') order by Maintestname ", con);//and SDCode in (select SDCode from SubDepartment where DigModule='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "') 
        //else
            sda = new SqlDataAdapter("select PackageCode as MTCode, PackageName as Maintestname from PackMst WHERE PackageCode like '%" + prefixText + "%' or PackageName like '%" + prefixText + "%' UNION " +
                               " select MTCode, Maintestname from MainTest WHERE MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%' order by Maintestname ", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["MTCode"] + " - " + dr["Maintestname"], i);
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
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
            sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR' and ( DoctorName like N'%" + prefixText + "%' or DoctorCode like N'%" + prefixText + "%' ) order by DoctorName", con);
        else
            sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR' and ( DoctorName like N'%" + prefixText + "%' or DoctorCode like N'%" + prefixText + "%' ) order by DoctorName", con);
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
  
    protected void cmbInitial_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtsex.Text = PatientinitialLogic_Bal_C.SelectSex(cmbInitial.SelectedItem.Text);
        //  ScriptManager1.SetFocus(txtFname);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            string regNo1 = Request.QueryString["PatRegID"].Trim();
           
            if (Request.QueryString["Patname"] != null)
            {
                Response.Redirect("DemographicEdit.aspx", false);
            }
            else
            {
                Response.Redirect("DemographicEdit.aspx",false);
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






    protected void txttests_TextChanged(object sender, EventArgs e)
    {
        DataTable table;
        if (ViewState["table"] != null)
        {
            table = (DataTable)ViewState["table"];
        }
        else
        {
            #region CreateTable

            DataTable table1 = new DataTable();

            // Declare DataColumn and DataRow variables.
            DataColumn column1;


            column1 = new DataColumn("MTCode", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Maintestname", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Amount", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("clientAmount", Type.GetType("System.String"));
            table1.Columns.Add(column1);

            ViewState["table"] = table1;

            #endregion
            table = (DataTable)ViewState["table"];
        }
        string[] SP_TestCode;
        SP_TestCode = txttests.Text.Split('-');

        if (SP_TestCode[0].ToString().Trim() != "")
        {
            bool alreadyExist = false;
            foreach (DataRow dr in table.Rows)
            {
                if (dr["MTCode"].ToString() == SP_TestCode[0].ToString().Trim())
                {
                    alreadyExist = true;
                    break;
                }
                if (dr["MTCode"].ToString().Length == 4)
                {
                    Package_Bal_C gd = new Package_Bal_C();
                    DataTable dt = new DataTable();
                    dt = gd.getAllTestCodes(dr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                       // if (dt.Rows[i]["MTCode"].ToString() == SP_TestCode[0].ToString().Trim())
                        if (dt.Rows[i]["MTCode"].ToString() == dr["MTCode"].ToString())
                        {
                            alreadyExist = true;
                            break;
                        }
                    }
                }


                if (dr["MTCode"].ToString().Length != 4)
                {
                    Package_Bal_C gd = new Package_Bal_C();
                    DataTable dt = new DataTable();
                    // dt = gd.getAllTestCodes(dr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                    dt = gd.getAllTestCodes(SP_TestCode[0].ToString().Trim(), Convert.ToInt32(Session["Branchid"]));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["MTCode"].ToString() == dr["MTCode"].ToString().Trim())
                        {
                            alreadyExist = true;
                            break;
                        }
                    }
                }
            }
            if (alreadyExist)
            {
                lblExist.Visible = true;
                lblExist.Text = "Test already added.";
            }
            else
            {
                lblExist.Visible = false;
                AddTestToTable(SP_TestCode[0].ToString().Trim());
            }
            grdTests.DataSource = (DataTable)ViewState["table"];
            grdTests.DataBind();
        }

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
        lblTotTestAmt.Text = amtTestTot.ToString();
        txttests.Text = "";
        ScriptManager1.SetFocus(txttests);
        #region Code for adding to grid

        DataTable tableSample = new DataTable();
        //Declare DataColumn and DataRow variables.
        DataColumn columnP;


        columnP = new DataColumn("SampleType", Type.GetType("System.String"));
        tableSample.Columns.Add(columnP);
        columnP = new DataColumn("STCODE", Type.GetType("System.String"));
        tableSample.Columns.Add(columnP);
        columnP = new DataColumn("TestName", Type.GetType("System.String"));
        tableSample.Columns.Add(columnP);

        ViewState["tableSample"] = tableSample;

        if (grdTests.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                if (gvr.Cells[1].Text.Trim().Length == 4)
                {
                    SqlConnection connP = DataAccess.ConInitForDC();
                    SqlCommand cmdP = new SqlCommand("select MTCode,TestName from PackmstD where PackageCode='" + gvr.Cells[1].Text.Trim() + "'", connP);
                    connP.Open();
                    SqlDataReader sdrP = cmdP.ExecuteReader();
                    while (sdrP.Read())
                    {
                        #region Code to Add Test to table

                        MainTest_Bal_C tt = new MainTest_Bal_C(sdrP["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        string sampleTypeOfTest = tt.SampleType;
                        bool sampleAlreadyExist = false;
                        string sampleTypesString = "";
                        foreach (DataRow dr in tableSample.Rows)
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
                            foreach (DataRow dr in tableSample.Rows)
                            {
                                if (dr["SampleType"].ToString() == sampleTypeOfTest)
                                {
                                    if (dr["STCODE"].ToString() == "")
                                    {
                                        dr["STCODE"] = sdrP["MTCode"].ToString();
                                    }
                                    else
                                    {
                                        dr["STCODE"] = dr["STCODE"] + "," + sdrP["MTCode"].ToString();
                                    }
                                    if (dr["TestName"].ToString() == "")
                                    {
                                        dr["TestName"] = sdrP["TestName"].ToString();
                                    }
                                    else
                                    {
                                        dr["TestName"] = dr["TestName"] + "," + sdrP["TestName"].ToString();
                                    }
                                }
                            }

                        }
                        else
                        {
                            DataRow dr = tableSample.NewRow();

                            dr["SampleType"] = sampleTypeOfTest;
                            dr["STCODE"] = sdrP["MTCode"].ToString();
                            dr["TestName"] = sdrP["TestName"].ToString();

                            tableSample.Rows.Add(dr);
                        }

                        #endregion
                    }
                    sdrP.Close();
                    connP.Close();
                }
                else
                {
                    #region Code to Add Test to table

                    MainTest_Bal_C tt = new MainTest_Bal_C(gvr.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));
                    string sampleTypeOfTest = tt.SampleType;
                    bool sampleAlreadyExist = false;
                    string sampleTypesString = "";
                    foreach (DataRow dr in tableSample.Rows)
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
                                sampleAlreadyExist = false;
                        }
                    }
                    if (sampleAlreadyExist)
                    {
                        foreach (DataRow dr in tableSample.Rows)
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
                        DataRow dr = tableSample.NewRow();

                        dr["SampleType"] = sampleTypeOfTest;
                        dr["STCODE"] = gvr.Cells[1].Text.Trim();
                        dr["TestName"] = gvr.Cells[2].Text.Trim();

                        tableSample.Rows.Add(dr);
                    }

                    #endregion
                }
            }
        }

        grdTests.DataSource = (DataTable)ViewState["table"];
        grdTests.DataBind();

        GrdPackage.DataSource = (DataTable)ViewState["tableSample"];
        GrdPackage.DataBind();

        #endregion
    }
    protected void Chkmaintestshort_SelectedIndexChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < Chkmaintestshort.Items.Count; i++)
        {
            if (Chkmaintestshort.Items[i].Selected)
            {

               // maintestshort = Chkmaintestshort.Items[i].Text;
                maintestshort = Chkmaintestshort.Items[i].Value;
                BindShortTest();
            }

        }
    }
    public void BindShortTest()
    {
        DataTable table;
        if (ViewState["table"] != null)
        {
            table = (DataTable)ViewState["table"];
        }
        else
        {
            #region CreateTable

            DataTable table1 = new DataTable();

            // Declare DataColumn and DataRow variables.
            DataColumn column1;


            column1 = new DataColumn("MTCode", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Maintestname", Type.GetType("System.String"));
            table1.Columns.Add(column1);
            column1 = new DataColumn("Amount", Type.GetType("System.String"));
            table1.Columns.Add(column1);

            ViewState["table"] = table1;

            #endregion
            table = (DataTable)ViewState["table"];
        }
        string[] SP_TestCode;
        //SP_TestCode = txttests.Text.Split('-');
        SP_TestCode = maintestshort.Split('-');
        

        if (SP_TestCode[0].ToString().Trim() != "")
        {
            bool alreadyExist = false;
            foreach (DataRow dr in table.Rows)
            {
                if (dr["MTCode"].ToString() == SP_TestCode[0].ToString().Trim())
                {
                    alreadyExist = true;
                    break;
                }
                if (dr["MTCode"].ToString().Length == 4)
                {
                    Package_Bal_C gd = new Package_Bal_C();
                    DataTable dt = new DataTable();
                    dt = gd.getAllTestCodes(dr["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["MTCode"].ToString() == SP_TestCode[0].ToString().Trim())
                        {
                            alreadyExist = true;
                            break;
                        }
                    }
                }
            }
            if (alreadyExist)
            {
                lblExist.Visible = true;
                lblExist.Text = "Test already added.";
            }
            else
            {
                lblExist.Visible = false;
                AddTestToTable(SP_TestCode[0].ToString().Trim());
            }
            grdTests.DataSource = (DataTable)ViewState["table"];
            grdTests.DataBind();
        }

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
        lblTotTestAmt.Text = amtTestTot.ToString();
      
      

        DataTable tableSample = new DataTable();
        //Declare DataColumn and DataRow variables.
        DataColumn columnP;


        columnP = new DataColumn("SampleType", Type.GetType("System.String"));
        tableSample.Columns.Add(columnP);
        columnP = new DataColumn("STCODE", Type.GetType("System.String"));
        tableSample.Columns.Add(columnP);
        columnP = new DataColumn("TestName", Type.GetType("System.String"));
        tableSample.Columns.Add(columnP);

        ViewState["tableSample"] = tableSample;

        if (grdTests.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                if (gvr.Cells[1].Text.Trim().Length == 4)
                {
                    SqlConnection connP = DataAccess.ConInitForDC();
                    SqlCommand cmdP = new SqlCommand("select MTCode,TestName from PackmstD where PackageCode='" + gvr.Cells[1].Text.Trim() + "'", connP);
                    connP.Open();
                    SqlDataReader sdrP = cmdP.ExecuteReader();
                    while (sdrP.Read())
                    {
                      

                        MainTest_Bal_C tt = new MainTest_Bal_C(sdrP["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        string sampleTypeOfTest = tt.SampleType;
                        bool sampleAlreadyExist = false;
                        string sampleTypesString = "";
                        foreach (DataRow dr in tableSample.Rows)
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
                            foreach (DataRow dr in tableSample.Rows)
                            {
                                if (dr["SampleType"].ToString() == sampleTypeOfTest)
                                {
                                    if (dr["STCODE"].ToString() == "")
                                    {
                                        dr["STCODE"] = sdrP["MTCode"].ToString();
                                    }
                                    else
                                    {
                                        dr["STCODE"] = dr["STCODE"] + "," + sdrP["MTCode"].ToString();
                                    }
                                    if (dr["TestName"].ToString() == "")
                                    {
                                        dr["TestName"] = sdrP["TestName"].ToString();
                                    }
                                    else
                                    {
                                        dr["TestName"] = dr["TestName"] + "," + sdrP["TestName"].ToString();
                                    }
                                }
                            }

                        }
                        else
                        {
                            DataRow dr = tableSample.NewRow();

                            dr["SampleType"] = sampleTypeOfTest;
                            dr["STCODE"] = sdrP["MTCode"].ToString();
                            dr["TestName"] = sdrP["TestName"].ToString();

                            tableSample.Rows.Add(dr);
                        }

                       
                    }
                    sdrP.Close();
                    connP.Close();
                }
                else
                {
                    

                    MainTest_Bal_C tt = new MainTest_Bal_C(gvr.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));
                    string sampleTypeOfTest = tt.SampleType;
                    bool sampleAlreadyExist = false;
                    string sampleTypesString = "";
                    foreach (DataRow dr in tableSample.Rows)
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
                                sampleAlreadyExist = false;
                        }
                    }
                    if (sampleAlreadyExist)
                    {
                        foreach (DataRow dr in tableSample.Rows)
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
                        DataRow dr = tableSample.NewRow();

                        dr["SampleType"] = sampleTypeOfTest;
                        dr["STCODE"] = gvr.Cells[1].Text.Trim();
                        dr["TestName"] = gvr.Cells[2].Text.Trim();

                        tableSample.Rows.Add(dr);
                    }

                    
                }
            }
        }

        grdTests.DataSource = (DataTable)ViewState["table"];
        grdTests.DataBind();

        GrdPackage.DataSource = (DataTable)ViewState["tableSample"];
        GrdPackage.DataBind();

        
    }
    public void UUUUU()
    {
        ViewState["barid"] = null;
        grdTests.DataSource = null;
        grdTests.DataBind();
        GrdPackage.DataSource = null;
        GrdPackage.DataBind();
        GetRecords(Convert.ToInt32(Request.QueryString["PID"]));
        SqlCommand cmd = null;
        SqlConnection conn = DataAccess.ConInitForDC();
        if (Session["DigModule"] != null)
            cmd = new SqlCommand("SELECT DISTINCT PackageCode as code FROM  patmstd WHERE  PID = " + Convert.ToInt32(Request.QueryString["PID"]) + " and SDCode in (select SDCode from SubDepartment where DigModule=" + Convert.ToInt32(Session["DigModule"]) + ") UNION " +
                               " SELECT DISTINCT MTCode as code FROM  patmstd WHERE" +
                               " (PackageCode IS NULL OR PackageCode = '') AND PID = " + Convert.ToInt32(Request.QueryString["PID"]) + " and SDCode in (select SDCode from SubDepartment where DigModule=" + Convert.ToInt32(Session["DigModule"]) + ")", conn);
        else
            cmd = new SqlCommand("SELECT DISTINCT PackageCode as code FROM  patmstd WHERE  PID = " + Convert.ToInt32(Request.QueryString["PID"]) + " AND (PatRegID <> '') UNION " +
                                " SELECT DISTINCT MTCode as code FROM  patmstd WHERE" +
                                " (PackageCode IS NULL OR PackageCode = '') AND PID = " + Convert.ToInt32(Request.QueryString["PID"]) + " ", conn);
        conn.Open();
        SqlDataReader sdr = cmd.ExecuteReader();
        while (sdr.Read())
        {
            if (!string.IsNullOrEmpty(sdr["code"].ToString()))
            {
                AddTestToTable(sdr["code"].ToString());
            }
        }
        sdr.Close();
        conn.Close(); conn.Dispose();
        grdTests.DataSource = (DataTable)ViewState["table"];
        grdTests.DataBind();

        float amtTestTot = 0f;
        if (grdTests.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                if (gvr.Cells[4].Text.Trim() != "")
                {
                    string amt = gvr.Cells[4].Text.Trim();
                    amtTestTot = amtTestTot + Convert.ToSingle(amt);
                }
            }
        }
        lblTotTestAmt.Text = amtTestTot.ToString();

       

        DataTable tableSample = new DataTable();
        //Declare DataColumn and DataRow variables.
        DataColumn columnP;
        columnP = new DataColumn("SampleType", Type.GetType("System.String"));
        tableSample.Columns.Add(columnP);
        columnP = new DataColumn("STCODE", Type.GetType("System.String"));
        tableSample.Columns.Add(columnP);
        columnP = new DataColumn("TestName", Type.GetType("System.String"));
        tableSample.Columns.Add(columnP);

        ViewState["tableSample"] = tableSample;

        if (grdTests.Rows.Count > 0)
        {
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                if (gvr.Cells[1].Text.Trim().Length == 4)
                {
                    SqlConnection connP = DataAccess.ConInitForDC();
                    SqlCommand cmdP = new SqlCommand("select MTCode,TestName from PackmstD where PackageCode='" + gvr.Cells[1].Text.Trim() + "'", connP);
                    connP.Open();
                    SqlDataReader sdrP = cmdP.ExecuteReader();
                    while (sdrP.Read())
                    {
                       

                        MainTest_Bal_C tt = new MainTest_Bal_C(sdrP["MTCode"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        string sampleTypeOfTest = tt.SampleType;
                        bool sampleAlreadyExist = false;
                        string sampleTypesString = "";
                        foreach (DataRow dr in tableSample.Rows)
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
                                    // sampleAlreadyExist = true;
                                    sampleAlreadyExist = false;
                            }
                        }
                        if (sampleAlreadyExist)
                        {
                            foreach (DataRow dr in tableSample.Rows)
                            {
                                if (dr["SampleType"].ToString() == sampleTypeOfTest)
                                {
                                    if (dr["STCODE"].ToString() == "")
                                    {
                                        dr["STCODE"] = sdrP["MTCode"].ToString();
                                    }
                                    else
                                    {
                                        dr["STCODE"] = dr["STCODE"] + "," + sdrP["MTCode"].ToString();
                                    }
                                    if (dr["TestName"].ToString() == "")
                                    {
                                        dr["TestName"] = sdrP["TestName"].ToString();
                                    }
                                    else
                                    {
                                        dr["TestName"] = dr["TestName"] + "," + sdrP["TestName"].ToString();
                                    }
                                }
                            }

                        }
                        else
                        {
                            DataRow dr = tableSample.NewRow();

                            dr["SampleType"] = sampleTypeOfTest;
                            dr["STCODE"] = sdrP["MTCode"].ToString();
                            dr["TestName"] = sdrP["TestName"].ToString();

                            tableSample.Rows.Add(dr);
                        }

                       
                    }
                    sdrP.Close();
                    connP.Close();
                }
                else
                {
                   

                    MainTest_Bal_C tt = new MainTest_Bal_C(gvr.Cells[1].Text.Trim(), Convert.ToInt32(Session["Branchid"]));
                    string sampleTypeOfTest = tt.SampleType;
                    bool sampleAlreadyExist = false;
                    string sampleTypesString = "";
                    foreach (DataRow dr in tableSample.Rows)
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
                                // sampleAlreadyExist = true;
                                sampleAlreadyExist = false;
                        }
                    }
                    if (sampleAlreadyExist)
                    {
                        foreach (DataRow dr in tableSample.Rows)
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
                        DataRow dr = tableSample.NewRow();

                        dr["SampleType"] = sampleTypeOfTest;
                        dr["STCODE"] = gvr.Cells[1].Text.Trim();
                        dr["TestName"] = gvr.Cells[2].Text.Trim();

                        tableSample.Rows.Add(dr);
                    }

                   
                }
            }
        }

        grdTests.DataSource = (DataTable)ViewState["table"];
        grdTests.DataBind();

        GrdPackage.DataSource = (DataTable)ViewState["tableSample"];
        GrdPackage.DataBind();
    }
    protected void btnpayment_Click(object sender, EventArgs e)
    {
        Response.Redirect("Paybilldesk.aspx?PID=" + Request.QueryString["PID"], false);

    }
    protected void btnbprint_Click(object sender, EventArgs e)
    {
        Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
        DataTable dt = new DataTable();
        PatSt_Bal_C objPrintStatus = new PatSt_Bal_C();
        int PID = Convert.ToInt32(Request.QueryString["PID"]);
        int branchid = Convert.ToInt32(Session["Branchid"].ToString());
        string FID = Convert.ToString(Request.QueryString["FID"].Trim());
        string PatRegID = Convert.ToString(Request.QueryString["PatRegID"].Trim());
        string BarCode_ID = "";
        string subdept = "";
        dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        }

        Cshmst_supp_Bal_C ObjCSB = new Cshmst_supp_Bal_C();
        //if (Convert.ToString(Session["Phlebotomist"]) == "YES")
        //{
        // ObjCSB.Insert_Update_Barcode_Patmstd(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), grdTests.Rows[j].Cells[1].Text.Trim(), PatRegID, FID);

        ObjCSB.Insert_Update_Barcode_PhlebotomistReq("", Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), "");
        // Patmstd_Main_Bal_C.UpdateStatusByLab_directresult_barcode_Pat(Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), BarcodeGenerate);

        // }
        if (Convert.ToString(ViewState["BarCodeImageReq"]) == "YES")
        {
            BarCodeImage(PID);
        }
        objPrintStatus.AlterView_Barcode_Temp(subdept, PID);


        dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        }

        objPrintStatus.AlterView_Barcode_Temp_Direct(subdept, PID);
        
        objPrintStatus.AlterViewPrintBarcode_Direct(branchid, PatRegID, FID, BarCode_ID, PID);

       
       // VW_DescriptiveViewLogic.SP_GetAlterView_BarCode(Convert.ToInt32(Session["Branchid"]), Convert.ToString(ViewState["PID"]), "0", subdept, BarCode_ID);

        // ======================================= Bar Code =============================
        string formula = "", selectonFormula = "";
        selectonFormula = ReportParameterClass.SelectionFormula;
        ReportDocument CR = new ReportDocument();

        CR.Load(Server.MapPath("~//DiagnosticReport//Rpt_PrintBarcode.rpt"));
        SqlConnection con = DataAccess.ConInitForDC();

        SqlDataAdapter sda = null;
        DataTable dtB = new DataTable();
        //  DataSet1 dst = new DataSet1();
        sda = new SqlDataAdapter("select * from VW_patstkvw  where PID='" + PID + "'   ", con);

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
            lblText.ForeColor = System.Drawing.Color.Red;
            lblText.Text = "BarCode Not Generated, Please Generate Once Again ";
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
        //int PID = Convert.ToInt32(Request.QueryString["PID"]);
        //int branchid = Convert.ToInt32(Session["Branchid"].ToString());
        //string FID = Convert.ToString(Request.QueryString["FID"].Trim());
        //string PatRegID = Convert.ToString(Request.QueryString["PatRegID"].Trim());
        //string BarCode_ID = "";
        //string subdept = "";
        //dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
        //if (dt.Rows.Count > 0)
        //{
        //    subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        //}

        //Cshmst_supp_Bal_C ObjCSB = new Cshmst_supp_Bal_C();
        ////if (Convert.ToString(Session["Phlebotomist"]) == "YES")
        ////{
        //// ObjCSB.Insert_Update_Barcode_Patmstd(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), grdTests.Rows[j].Cells[1].Text.Trim(), PatRegID, FID);

        //ObjCSB.Insert_Update_Barcode_PhlebotomistReq("", Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), "");
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
        PBC.PID = Convert.ToInt32(Request.QueryString["PID"]);
        PBC.get_PermentId();

        ViewState["PPID"] = PBC.P_PPID;

        sc1.CommandText = "ALTER VIEW [dbo].[VW_PatientCard] AS SELECT        TOP (99.99) PERCENT RecM.BillNo, dbo.RecM.billdate as RecDate, dbo.RecM.PaymentType as BillType, RecM.AmtPaid AS AmtReceived, RecM.DisAmt AS Discount, dbo.patmst.TestCharges AS NetPayment, RecM.AmtPaid, RecM.BalAmt AS Balance, "+
                      "  RecM.username, RecM.OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, DrMT.DoctorName, MainTest.Maintestname, MainTest.MTCode, "+
                      "  patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag, patmst.Patusername, patmst.Patpassword, RecM.Comment, patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, "+
                      "  patmst.UnitCode, RecM.TaxAmount, RecM.TaxPer, RecM.PrintCount, patmst.Email AS EmailID "+
                      "  FROM            RecM INNER JOIN "+
                      "  patmst INNER JOIN "+
                      "  DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN "+
                      "  MainTest INNER JOIN "+
                      "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid ON RecM.PID = patmst.PID AND "+
                      "  RecM.branchid = patmst.Branchid LEFT OUTER JOIN "+
                      "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode where  patmst.branchid=" + Session["Branchid"].ToString() + " and patmst.PPID='" + ViewState["PPID"] + "' and patmst.PatRegID='" + Request.QueryString["PatRegID"] + "'  order by RecM.billno desc  ";// and Cshmst.BillNo=" + bno + " DrMT.DrCheck_flag='CC' and


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
        Session["PID_report"] = Convert.ToInt32(Request.QueryString["PID"]);
        Session["RecNo_report"] = 0;
    }
    protected void btnbarcodeEntry_Click(object sender, EventArgs e)
    {
        try
        {
            Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
            PatSt_Bal_C objPrintStatus = new PatSt_Bal_C();
            int PID = Convert.ToInt32(Request.QueryString["PID"]);
            int branchid = Convert.ToInt32(Session["Branchid"].ToString());
            string FID = Convert.ToString(Request.QueryString["FID"].Trim());
            string PatRegID = Convert.ToString(Request.QueryString["PatRegID"].Trim());
            Cshmst_supp_Bal_C ObjCSB = new Cshmst_supp_Bal_C();
            //if (Convert.ToString(Session["Phlebotomist"]) == "YES")
            //{
            // ObjCSB.Insert_Update_Barcode_Patmstd(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), grdTests.Rows[j].Cells[1].Text.Trim(), PatRegID, FID);

            ObjCSB.Insert_Update_Barcode_PhlebotomistReq("", Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), "");
            // Patmstd_Main_Bal_C.UpdateStatusByLab_directresult_barcode_Pat(Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), BarcodeGenerate);

            // }

            string BarCode_ID = "";
            string subdept = "";
            dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
            if (dt.Rows.Count > 0)
            {
                subdept = Convert.ToString(dt.Rows[0]["subdept"]);
            }
            if (Convert.ToString(ViewState["BarCodeImageReq"]) == "YES")
            {
                BarCodeImage(PID);
            }
            objPrintStatus.AlterView_Barcode_Temp_Deptwise(subdept, PID);

            objPrintStatus.AlterViewPrintBarcode_deptwise_Registration(branchid, PatRegID, FID, BarCode_ID, PID);
            //=============================================
            //var content = "123456789012345678";
            //var writer = new BarcodeWriter
            //{
            //    Format = BarcodeFormat.CODE_128
            //};
            //var bitmap = writer.Write(content);
            ////CrystalDecisions.ReportAppServer.ReportDefModel.PictureObject boPictureObject = new CrystalDecisions.ReportAppServer.ReportDefModel.PictureObject(); ;
            //FileStream FilStr = new FileStream(this.openFileDialog1.FileName, FileMode.Open);
            //BinaryReader BinRed = new BinaryReader(FilStr);
            //DataRow dr = this.DsImages.Tables["images"].NewRow();
            //dr["path"] = this.openFileDialog1.FileName;

            //dr["image"] = BinRed.ReadBytes((int)BinRed.BaseStream.Length);
            //this.DsImages.Tables["images"].Rows.Add(dr);
            //FilStr.Close();
            //BinRed.Close();
            //DynamicImageExample DyImg = new DynamicImageExample();
            //=============================================
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
                lblText.ForeColor = System.Drawing.Color.Red;
                lblText.Text = "BarCode Not Generated, Please Generate Once Again ";
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
     

            //Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
            //PatSt_Bal_C objPrintStatus = new PatSt_Bal_C();
            //int PID = Convert.ToInt32(Request.QueryString["PID"]);
            //int branchid = Convert.ToInt32(Session["Branchid"].ToString());
            //string FID = Convert.ToString(Request.QueryString["FID"].Trim());
            //string PatRegID = Convert.ToString(Request.QueryString["PatRegID"].Trim());
            //Cshmst_supp_Bal_C ObjCSB = new Cshmst_supp_Bal_C();
            ////if (Convert.ToString(Session["Phlebotomist"]) == "YES")
            ////{
            //// ObjCSB.Insert_Update_Barcode_Patmstd(BarcodeGenerate, Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), grdTests.Rows[j].Cells[1].Text.Trim(), PatRegID, FID);

            //ObjCSB.Insert_Update_Barcode_PhlebotomistReq("", Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), "");
            //// Patmstd_Main_Bal_C.UpdateStatusByLab_directresult_barcode_Pat(Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), BarcodeGenerate);

            //// }

            //string BarCode_ID = "";
            //string subdept = "";
            //dt = ObjPNBC.Get_subdept(Convert.ToString(Session["username"]));
            //if (dt.Rows.Count > 0)
            //{
            //    subdept = Convert.ToString(dt.Rows[0]["subdept"]);
            //}
            //objPrintStatus.AlterView_Barcode_Temp_Deptwise(subdept, PID);

            //objPrintStatus.AlterViewPrintBarcode_deptwise_Registration(branchid, PatRegID, FID, BarCode_ID, PID);

            //Session.Add("rptsql", "");
            //Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PrintBarcode_deptwise.rpt");
            //Session["reportname"] = "PrintBarcode_dept";
            //Session["RPTFORMAT"] = "pdf";

            //ReportParameterClass.SelectionFormula = "";
            //string close = "<script language='javascript'>javascript:OpenReport();</script>";
            //Type title1 = this.GetType();
            //Page.ClientScript.RegisterStartupScript(title1, "", close);
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
    protected void btnbilldesk_Click(object sender, EventArgs e)
    {
       // this.btnSubmit_Click(null,null);
        Response.Redirect("Paybilldesk.aspx?PID=" + Request.QueryString["PID"] + "&FID=" + Request.QueryString["FID"].Trim() + "", false);
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
                    string DefaultFileName1 = Server.MapPath("/" + Request.ApplicationPath + "/ViewPrescription/");
                    FUBrowsePresc.SaveAs(Server.MapPath(DefaultFileName) + "/" + Request.QueryString["PatRegID"].Trim() + "_" + PDate + "_" + FUBrowsePresc.FileName);
                    LblFilename.Text = Request.QueryString["PatRegID"].Trim() + "_" + PDate + "_" + FUBrowsePresc.FileName;
                    Cshmst_Bal_C Obj_CBC = new Cshmst_Bal_C();
                    Obj_CBC.P_UploadPrescription = DefaultFileName1 + "" + LblFilename.Text;
                    Obj_CBC.PID = Convert.ToInt32(Request.QueryString["PID"]);
                    Obj_CBC.PatRegID = Convert.ToString(Request.QueryString["PatRegID"].Trim());

                    Obj_CBC.Insert_Update_Prescription(Convert.ToInt32(Session["Branchid"]));
                }
            }
            catch (Exception ex)
            {
                //StatusLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
            }
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
    protected void txtemail_TextChanged(object sender, EventArgs e)
    {
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

    protected void btncapturephoto_Click(object sender, EventArgs e)
    {
        Session["PID_Img"] = Request.QueryString["PID"].Trim();
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
                    FUUploadPhoto.SaveAs(Server.MapPath(DefaultFileName) + "/" + Request.QueryString["PatRegID"].Trim() + "_" + PDate + "_" + FUUploadPhoto.FileName);
                    LblUploadph.Text = Request.QueryString["PatRegID"].Trim() + "_" + PDate + "_" + FUUploadPhoto.FileName;
                    //Cshmst_Bal_C Obj_CBC = new Cshmst_Bal_C();
                    //Obj_CBC.P_UploadPrescription = DefaultFileName + "" + LblFilename.Text;
                    //Obj_CBC.PID = Convert.ToInt32(Request.QueryString["PID"]);
                    //Obj_CBC.PatRegID = Convert.ToString(Request.QueryString["PatRegID"].Trim());

                    //Obj_CBC.Insert_Update_Prescription(Convert.ToInt32(Session["Branchid"]));

                    byte[] imageArray = System.IO.File.ReadAllBytes(Server.MapPath("~/Captures/" + LblUploadph.Text + ""));
                    string base64ImageRepresentation = Convert.ToBase64String(imageArray);

                    SqlConnection conn = DataAccess.ConInitForDC();
                    SqlCommand sc = new SqlCommand();
                    sc = new SqlCommand("" +
                         "update patmst set  Image1=@Image1 ,ImagePath='" + LblUploadph.Text + "' where  PID=@PID ", conn);


                    sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = Convert.ToInt32(Request.QueryString["PID"]);


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
       // sda = new SqlDataAdapter("select distinct PatRegID as BarcodeID,MTCode from Patmstd where PID='" + Convert.ToString(PID) + "'   ", con);

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
    protected void txtModifyRate_TextChanged(object sender, EventArgs e)
    {
       
        float amtTestTot = 0f;
        if (grdTests.Rows.Count > 0)
        {
           
            //if (txtModifyRate.Text.Trim() == "")
            //{
            //    txtModifyRate.Text = "0";
            //}
            //if (txtModifyRate.Text != "0")
            foreach (GridViewRow gvr in grdTests.Rows)
            {
                TextBox txtModifyRate = gvr.FindControl("txtModifyRate") as TextBox;
                if (txtModifyRate.Text.Trim() == "")
                {
                    txtModifyRate.Text = "0";
                }
                if (gvr.Cells[4].Text.Trim() != "" && txtModifyRate.Text != "0")
                {
                    amtTestTot = amtTestTot + Convert.ToSingle(txtModifyRate.Text.Trim());
                }
                else
                {
                    amtTestTot = amtTestTot + Convert.ToSingle(gvr.Cells[4].Text.Trim());
                }
            }
        }
        lblTotTestAmt.Text = amtTestTot.ToString();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        SqlConnection conn12 = DataAccess.ConInitForDC();
        SqlCommand sc1 = conn12.CreateCommand();
        // PatSt_Bal_C PBC = new PatSt_Bal_C();
        sc1.CommandText = "ALTER VIEW [dbo].[VW_cshbill_ClickSample] AS (SELECT        RecM.BillNo, patmst.Patregdate AS RecDate, 'Cash Bill' AS BillType, RecM.AmtPaid AS AmtReceived, RecM.DisAmt AS Discount, patmst.TestCharges AS NetPayment, RecM.AmtPaid, " +
                         "   dbo.FUN_GetReceiveAmt_Balance(1, RecM.PID) AS Balance, RecM.username, RecM.OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, " +
                          "  RecM.CardNo AS DoctorName, MainTest.Maintestname, MainTest.MTCode, patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag, patmst.Patusername, patmst.Patpassword, RecM.Comment, " +
                          "  patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, RecM.PaymentType AS UnitCode, RecM.TaxAmount, RecM.TaxPer, 1 AS PrintCount,  " +
                          "  patmst.OtherRefDoctor, RecM.ReceiptNo, RecM.OtherChargeRemark, RecM.PID  , DrMT.address1 as CenterAddress,patmst.QRImage, patmst.QRImageD,Patmst.PatientcHistory,CONVERT(varchar, patmst.DOB, 105) AS DOB, CONVERT(varchar,  patmst.PatDatOfBirth, 105) AS PatDatOfBirth,patmst.UploadPrescription " +
                          "  FROM            patmst INNER JOIN " +
                          "  DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN " +
                          "  MainTest INNER JOIN " +
                          "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN " +
                          "  RecM ON patmst.PID = RecM.PID LEFT OUTER JOIN " +
                          "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode " +
                 "   where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and patmstd.PID='" + ViewState["PID"] + "' )";// and Cshmst.BillNo=" + bno + "
        conn12.Open();
        sc1.ExecuteNonQuery();
        conn12.Close(); conn12.Dispose();


        btnbarcodeEntry.Enabled = true;
        btnpatientcard.Enabled = true;
        btnbprint.Enabled = true;

        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PayReceipt_clickSample.rpt");
        Session["reportname"] = "CashReceipt_ClickSample";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = "";
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
        Session["PID_report"] = Convert.ToInt32(ViewState["PID"]);
        Session["RecNo_report"] = 0;
        ViewState["receipt"] = "No";
    }
}