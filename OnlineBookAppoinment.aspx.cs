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

public partial class OnlineBookAppoinment :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();

    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Appointment_C ObjApp = new Appointment_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //LUNAME.Text = Convert.ToString(Session["username"]);
            //LblDCName.Text = Convert.ToString(Session["Bannername"]);
            //LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
            fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
            //dt = new DataTable();
            //dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
            //this.PopulateTreeView(dt, 0, null);
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
    //private void PopulateTreeView(DataTable dtparent, int Parentid, TreeNode treeNode)
    //{
    //    foreach (DataRow row in dtparent.Rows)
    //    {
    //        TreeNode child = new TreeNode
    //        {
    //            Text = row["MenuName"].ToString(),
    //            Value = row["MenuID"].ToString()

    //        };
    //        if (Parentid == 0)
    //        {
    //            TrMenu.Nodes.Add(child);
    //            DataTable dtchild = new DataTable();
    //            dtchild = ObjTB.BindChildMenu(child.Value, Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
    //            PopulateTreeView(dtchild, int.Parse(child.Value), child);

    //        }
    //        else
    //        {
    //            treeNode.ChildNodes.Add(child);
    //        }

    //    }
    //}
    //protected void TrMenu_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    int tId = Convert.ToInt32(TrMenu.SelectedValue);
    //    DataTable dtform = new DataTable();
    //    dtform = ObjTB.BindForm(tId);
    //    if (dtform.Rows.Count > 0)
    //    {
    //        Response.Redirect(dtform.Rows[0]["SubMenuNavigateURL"].ToString());
    //    }
    //}

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
                //createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                // ddlCenter.SelectedItem.Text = (ui.CenterCode.Trim());
               // createuserTable_Bal_C uii = new createuserTable_Bal_C(ui.CenterCode.Trim());
                ObjApp.CenterCode = "SATAV LABORATORY";
                ObjApp.Patregdate = Convert.ToDateTime(fromdate.Text).ToString("dd/MM/yyyy");
                ObjApp.CenterID = 0;
                ObjApp.DirectApp = 1;
            }
            dt = new DataTable();
            dt = ObjApp.Get_Patient_Appointmentt_Check("1", Convert.ToString(Request.QueryString["BookDate"]), Convert.ToString(Request.QueryString["BookDate"]), Request.QueryString["SlotTime"], Request.QueryString["Centername"]);
            if(dt.Rows.Count>0)
            {
                 Label10.Text = "Appoinment Already booked .Book another slot..!";
            }
            else
            {

                 int Appid= ObjApp.Insert_Register_Appoinment(1);
                 //int PID1 = PBCL.Insert_Update_ForPmst(Convert.ToInt32(Session["Branchid"]));
                 Label10.Text = "Appoinment Save Successfully..! Your Token Id is :-"+Appid+".";
            }

           
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
}