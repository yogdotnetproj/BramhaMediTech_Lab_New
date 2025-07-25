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

using System.Web.Management;


using System.Collections.Specialized;
using System.Text;


public partial class BookAppoinment : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();

    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Appointment_C ObjApp = new Appointment_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindCollectionperson();
          
            fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
           
            cmbInitial.DataSource = PatientinitialLogic_Bal_C.getInitial();
            cmbInitial.DataTextField = "prefixName";
            cmbInitial.DataValueField = "prefixName";
            cmbInitial.DataBind();
            cmbInitial.Items.Insert(0, new ListItem("Select Initial", "0"));
            BindTime();
            if (Request.QueryString["SlotTime"] != null)
            {
                Slo.Visible = false;
                Slo1.Visible = false;
            }
            else
            {
                Slo.Visible = true;
                Slo1.Visible = true;
            }
        }
    }
    public void BindCollectionperson()
    {
        DataTable dtcolper = new DataTable();
        dtcolper = ObjApp.GetAll_CollectionPErson();
        if (dtcolper.Rows.Count > 0)
        {
            ddlCollectionReceiver.DataSource = dtcolper;
            ddlCollectionReceiver.DataTextField = "Name";
            ddlCollectionReceiver.DataValueField = "Cuid";
            ddlCollectionReceiver.DataBind();
            ddlCollectionReceiver.Items.Insert(0, new ListItem("-Select-", "0"));
        }

    }

    public void BindTime()
    {
        DateTime dt = Convert.ToDateTime("09:00:00"); //for adding start time
        for (int i = 0; i <= 25; i++) //Set up every 30 minute interval
        {
            ListItem li2 = new ListItem(dt.ToShortTimeString(), dt.ToShortTimeString());
            li2.Selected = false;
            
            ddlToTime.Items.Add(li2);
            dt = dt.AddMinutes(30);
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
        ScriptManager1.SetFocus(txtFname);
    }

    protected void txtDoctorName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (validation() == true)
        {
            if (cmbInitial.SelectedIndex > 0)
            {
                ObjApp.Initial = cmbInitial.SelectedItem.Text.Trim();
            }

            ObjApp.Patname = txtFname.Text.Trim();
            ObjApp.Gender = ddlsex.SelectedItem.Text.Trim();
            ObjApp.Age = txtAge.Text.Trim();
            ObjApp.AgeType = cmdYMD.SelectedItem.Text.Trim();
            ObjApp.Phone = txttelno.Text.Trim();
            ObjApp.Email = txtemail.Text.Trim();
            ObjApp.Pataddress = txt_address.Text.Trim();
            ObjApp.State = txtstate.Text.Trim();
            ObjApp.District = txtDistrict.Text.Trim();
            ObjApp.City = txtcity.Text.Trim();
            ObjApp.RefDr = txtDoctorName.Text.Trim();
            ObjApp.Note = txt_remark.Text.Trim();

            ObjApp.HospitalName = txtadmitpatient.Text.Trim();
            ObjApp.HospitalID = txthospID.Text.Trim();
            if (ChkILI.Checked == true)
            {
                ObjApp.ILI = true;
            }
            else
            {
                ObjApp.ILI = false;
            }
            if (ChlFever.Checked == true)
            {
                ObjApp.Fever = true;
            }
            else
            {
                ObjApp.Fever = false;
            }
            ObjApp.FeverDuration = txtfeverduration.Text.Trim();
            if (chkcough.Checked == true)
            {
                ObjApp.Cough = true;
            }
            else
            {
                ObjApp.Cough = false;
            }
            ObjApp.CoughDuration = txtcoughduration.Text.Trim();
            if (chksari.Checked == true)
            {
                ObjApp.SARI = true;
            }
            else
            {
                ObjApp.SARI = false;
            }
            ObjApp.CoMorbidity = txtComorbidity.Text.Trim();
            ObjApp.Tempetrature = txttempreco.Text.Trim();

            ObjApp.Symptom = ddlsputum.SelectedItem.Text.Trim();
            ObjApp.SymptomAddition = txtadditionalsymptoms.Text.Trim();

            ObjApp.TravelLast = rblVisites.SelectedItem.Text.Trim();
            ObjApp.TravelLastVisit = txtcountryvisit.Text.Trim();

            ObjApp.PatientAdmited = rblisolation.SelectedItem.Text.Trim();
            ObjApp.HomeCollectionPerson = Convert.ToInt32( ddlCollectionReceiver.SelectedValue);

            if (Request.QueryString["SlotTime"] != null)
            {

                ObjApp.CenterID = Convert.ToInt32(Request.QueryString["CenterID"]);
                ObjApp.CenterCode = Request.QueryString["Centername"];
                ObjApp.SlotTime = Request.QueryString["SlotTime"];
                ObjApp.Patregdate = Convert.ToString(Request.QueryString["BookDate"]); //Convert.ToDateTime(Request.QueryString["BookDate"]).ToString("dd/MM/yyyy");// Convert.ToDateTime(Request.QueryString["BookDate"]);
                ObjApp.DirectApp = 0;
            }
            else
            {
                ObjApp.SlotTime = ddlToTime.SelectedValue;
                createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                // ddlCenter.SelectedItem.Text = (ui.CenterCode.Trim());
                createuserTable_Bal_C uii = new createuserTable_Bal_C(ui.CenterCode.Trim());
                ObjApp.CenterCode = (uii.CenterCode.Trim());
                ObjApp.Patregdate = Convert.ToDateTime(fromdate.Text).ToString("dd/MM/yyyy");
                ObjApp.CenterID = 0;
                ObjApp.DirectApp = 1;
            }
                ObjApp.Insert_RegisterAppoinment(1);
                this.btnwhatapp_Click(null,null);

            Label10.Text = "Appoinment Save Successfully..!";
        }
    }
    public bool validation()
    {
        
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
            }
            //if (txtAge.Text == "")
            //{
            //    txtAge.Text = "0";
            //}
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
            }
            if (txttelno.Text.Length <= 9)
            {
                txttelno.Focus();
                txttelno.BackColor = Color.Red;
                //Label11.Text = "Enter mobile no";
                return false;
            }
            else
            {
                txttelno.BackColor = Color.White;
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
            }
                      
          
           
            //if (txtDoctorName.Text == "")
            //{
            //    // Label11.Visible = true;
            //    txtDoctorName.Focus();
            //    txtDoctorName.BackColor = Color.Red;
            //    //Label11.Text = "Enter mobile no";
            //    return false;
            //}
            //else
            //{
            //    txtDoctorName.BackColor = Color.White;
            //}
            
       
        

        return true;
    }
    protected void btnwhatapp_Click(object sender, ImageClickEventArgs e)
    {
        createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
        aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
        string WhatAppUrl = aut.P_WhatAppUrl;
        string WhatApp_Api = aut.P_WhatApp_Api;
        WhatAppReport(txttelno.Text, WhatAppUrl,WhatApp_Api);
    }

    //public void sendSMSRegistration(Patmstd_Bal_C PBC)
    //{

    //    string p_mobileno = Convert.ToString(txttelno);
    //    string p_fname = cmbInitial.SelectedItem.Text + " " + txtFname.Text + " " + "";
    //    //string PID = Convert.ToString(PBC.PID);
    //    string Branchid = Convert.ToString(Session["Branchid"]);
    //    string msg = PBC.GetSMSString("Appoinment", Convert.ToInt16(Branchid));
    //    string CounCode = PBC.GetSMSString_CountryCode("Appoinment", Convert.ToInt16(Branchid));
    //    if (msg.Trim() != "")
    //    {
    //        if (msg.Contains("#Name#"))
    //        {
    //            msg = msg.Replace("#Name#", p_fname);
    //        }
    //        //string PatRegID = PBC.GetRegno(PBC.PIDNew, Convert.ToInt16(Branchid));

    //        //if (msg.Contains("#PatRegID#"))
    //        //{
    //        //    msg = msg.Replace("#PatRegID#", PatRegID);
    //        //}
    //        //if (msg.Contains("#UserName#"))
    //        //{
    //        //    msg = msg.Replace("#UserName#", PBC.P_PUserName);
    //        //    msg = msg.Replace("#Password#", PBC.P_PPassword);

    //        //}
    //        //if (msg.Contains("#Amount#"))
    //        //{
    //        //    msg = msg.Replace("#Amount#", lbltotalpayment.Text);
    //        //}
    //        if (CounCode.Length == 2)
    //        {
    //            if (p_mobileno != CounCode && p_mobileno != "")
    //            {
    //                p_mobileno = p_mobileno.Substring(2, 10);
    //                createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
    //                aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
    //                string Labname = aut.P_LabSmsName;
    //                string SMSapistring = aut.P_LabSmsString;
    //                string Labwebsite = aut.P_LabWebsite;

    //                SMSapistring = SMSapistring.ToString().Replace("#message#", msg);//msg
    //                SMSapistring = SMSapistring.Replace("#Labname#", Labname);
    //                SMSapistring = SMSapistring.Replace("#phone#", p_mobileno);
    //                try
    //                {
    //                    string url = apicall(SMSapistring);
    //                    if (url != "0")
    //                    {
    //                        // smsevent = true;                        
    //                        // lblsmsError.Text = "SMS Sended To Patient " + p_fname + "  Mobile Number Is " + p_mobileno + "";
    //                        // lblsmsError.Visible = true;
    //                    }
    //                    else
    //                    {
    //                        // lblsmsError.Visible = true;
    //                        //lblsmsError.Text = "Unable To Send SMS For Patient " + p_fname + " Mobile Number Is " + p_mobileno + "";
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                }
    //            }
    //            else
    //            {
    //                //lblsmsError.Visible = true;
    //                //lblsmsError.Text = "Patient " + p_fname + " Mobile Number Not available ";
    //            }
    //        }
    //        else
    //        {
    //            if (p_mobileno != CounCode && p_mobileno != "")
    //            {
    //                p_mobileno = p_mobileno.Substring(3, 10);
    //                createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
    //                aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
    //                string Labname = aut.P_LabSmsName;
    //                string SMSapistring = aut.P_LabSmsString;
    //                string Labwebsite = aut.P_LabWebsite;

    //                SMSapistring = SMSapistring.ToString().Replace("#message#", msg);
    //                SMSapistring = SMSapistring.Replace("#Labname#", Labname);
    //                SMSapistring = SMSapistring.Replace("#phone#", p_mobileno);
    //                try
    //                {
    //                    string url = apicall(SMSapistring);
    //                    if (url != "0")
    //                    {
    //                        // smsevent = true;                        
    //                        // lblsmsError.Text = "SMS Sended To Patient " + p_fname + "  Mobile Number Is " + p_mobileno + "";
    //                        // lblsmsError.Visible = true;
    //                    }
    //                    else
    //                    {
    //                        // lblsmsError.Visible = true;
    //                        //lblsmsError.Text = "Unable To Send SMS For Patient " + p_fname + " Mobile Number Is " + p_mobileno + "";
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                }
    //            }
    //            else
    //            {
    //                //lblsmsError.Visible = true;
    //                //lblsmsError.Text = "Patient " + p_fname + " Mobile Number Not available ";
    //            }
    //        }
    //    }

    //}


    public void WhatAppReport(string MobNo, string WhatAppUrl, string WhatApp_Api)
    {
        try
        {
            // user will change below 3 variables only 
            //var filepath = "/var/www/test.jpg"; // absolute path of file on local drive
            //string mm2 = Page.ResolveUrl("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

           // var filepath = ""; // absolute path of file on local drive
           // // var key = "ft1dcT8j16fIOknh"; // your api key
           // var key = WhatAppUrl.Trim(); // your api key
           // // var number = "9199XXXXXXXX"; // target mobile number, including country code
           // var number = MobNo; // target mobile number, including country code
           // var caption = "Test Report"; // caption is optional parameter


           // // do not change below this line
           // byte[] AsBytes = File.ReadAllBytes(@filepath);
           // String filedata = Convert.ToBase64String(AsBytes);

           // var filename = new FileInfo(filepath).Name;
           // var wb = new WebClient();
           // var data = new NameValueCollection();

           // data["data"] = filedata;
           // data["filename"] = filename;
           // data["number"] = number;
           // data["caption"] = caption;

           // var response = wb.UploadValues("http://send.wabapi.com/postfile.php", "POST", data);
           //// var response = wb.UploadValues("http://node4.wabapi.com/v1/text.php", "POST", data);
           // string responseInString = Encoding.UTF8.GetString(response);

          

            //======================================================

            var wb = new WebClient();
            var data = new NameValueCollection();

           //===============================================
            Patmstd_Bal_C Obj_PBC = new Patmstd_Bal_C();
            string p_mobileno = Convert.ToString(txttelno);
            string p_fname = cmbInitial.SelectedItem.Text + " " + txtFname.Text + " " + "";
            string AppAccept = ddlCollectionReceiver.SelectedItem.Text;
            //string PID = Convert.ToString(PBC.PID);
            string Branchid = Convert.ToString(Session["Branchid"]);
            string msg = Obj_PBC.GetSMSString("Appoinment", Convert.ToInt16(Branchid));
            string msgDet = Obj_PBC.GetSMSString("AppointAttend", Convert.ToInt16(Branchid));
            dt = new DataTable();
            dt = ObjApp.GetAll_CollectionPErson_details(Convert.ToInt32(ddlCollectionReceiver.SelectedValue));
             if (msg.Contains("#Name#"))
            {
                msg = msg.Replace("#Name#", p_fname);
            }
             if (msgDet.Contains("#Name#"))
             {
                 msgDet = msgDet.Replace("#Name#", AppAccept);
             }
            //================================================
            var key = WhatAppUrl.Trim(); // your api key
            var number = +91+MobNo;
            var message = "test message";

            data["key"] = key;
           

            data["number"] = +91 + Convert.ToString(dt.Rows[0]["MobileNo"]);
            data["message"] = msgDet + "" + p_fname + ".Mobile No:" + MobNo + ",Address:" + txt_address.Text + ",Appoinment Date and Time Is" + Convert.ToString(Request.QueryString["BookDate"]) + Request.QueryString["SlotTime"];

            var responseText1 = wb.UploadValues("http://send.wabapi.com/posttext.php", "POST", data);
            string responseInStringText1 = Encoding.UTF8.GetString(responseText1);

            data["number"] = number;
            // data["message"] = message;
            data["message"] = msg;
            var responseText = wb.UploadValues("http://send.wabapi.com/posttext.php", "POST", data);
            string responseInStringText = Encoding.UTF8.GetString(responseText);

            

            //======================================================
            Label10.Text = "Appoinment Details Send Successfully..!";
        }
        catch (Exception ex)
        {
            Label10.Text = "Report Not Send Successfully..!";
        }
    }
}