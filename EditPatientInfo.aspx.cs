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

public partial class EditPatientInfo : System.Web.UI.Page
{
    string maintestshort = "";
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    Patmstd_Bal_C ObjPBC1 = new Patmstd_Bal_C();
    int bno = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        ViewState["btnsave"] = "";
        lbl_mobile.Visible = false;
        if (!Page.IsPostBack)
        {
            try
            {
               

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


                }
                cmbInitial.DataSource = PatientinitialLogic_Bal_C.getInitial();
                cmbInitial.DataTextField = "prefixName";
                cmbInitial.DataBind();
                txtsex.Text = "Male";

                if (Request.QueryString["PID"] != null && Request.QueryString["FType"] != null)
                {
                    if (Request.QueryString["FType"].ToString() == "Edit")
                    {
                        ViewState["barid"] = null;

                        GetRecords(Convert.ToInt32(Request.QueryString["PID"]));
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


    void GetRecords(int PID)
    {
        ArrayList al = (ArrayList)Patmst_New_Bal_C.Get_patmst_againsPIDID(PID, Convert.ToInt32(Session["Branchid"]));
        IEnumerator ie = al.GetEnumerator();
        while (ie.MoveNext())
        {
            Patmst_Bal_C PatBC = (ie.Current as Patmst_Bal_C);

            cmbInitial.SelectedItem.Text = PatBC.Initial;
            txtFname.Text = PatBC.Patname;
            string tel = PatBC.Phone;
            string CounCode = ObjPBC1.GetSMSString_CountryCode("Registration", Convert.ToInt32(Session["Branchid"]));
            if (CounCode.Length == 2)
            {
                if (tel != CounCode)
                {
                    tel = tel.Substring(2);
                    txttelno.Text = tel;
                }
                else
                {
                    txttelno.Text = "";
                }
            }
            else
            {
                if (tel != CounCode)
                {
                    tel = tel.Substring(3);
                    txttelno.Text = tel;
                }
                else
                {
                    txttelno.Text = "";
                }
            }
            if (PatBC.DoctorCode != "" || PatBC.DoctorCode != null)
            {
                txtDoctorName.Text = PatBC.Drname + "=" + PatBC.DoctorCode;
            }

            txtsex.Text = PatBC.Sex;
            txtemail.Text = PatBC.Email;
            ViewState["CenterCode"] = PatBC.CenterCode.Trim();
            ViewState["CenterName"] = PatBC.CenterName;
            txtAge.Text = PatBC.Age.ToString();
            cmdYMD.SelectedValue = PatBC.MYD;
            txt_remark.Text = PatBC.OtherRefDoctor;

            try
            {

            }
            catch (Exception) { }
            txt_address.Text = PatBC.Pataddress;
            txt_clinicalhistory.Text = PatBC.PatientcHistory;

            txtWeight.Text = PatBC.P_Weights;
            txtHeight.Text = PatBC.P_Heights;
            txtDieses.Text = PatBC.P_Disease;
            txtLastPeriod.Text = PatBC.P_LastPeriod;
            txtSymptoms.Text = PatBC.P_Symptoms;
            txtFSTime.Text = PatBC.P_FSTime;
            txtTherapy.Text = PatBC.P_Therapy;

            if (PatBC.P_SocialMedia == 0)
            {
                //rblretecate.SelectedItem.Text = "General";
                rblretecate.SelectedValue = "0";
            }
            else if (PatBC.P_SocialMedia == 1)
            {
                // rblretecate.SelectedItem.Text = "Viber";
                rblretecate.SelectedValue = "1";
            }
            else
            {
                //rblretecate.SelectedItem.Text = "Whatsapp";
                rblretecate.SelectedValue = "2";
            }
            // txt_remark.Text = PatBC.P_remark;



        }
    }





    protected void btnSubmit_Click(object sender, EventArgs e)
    {
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
                // string[] vialArr1 = ViewState["barid"].ToString().Split(',');




                string STCODE = "";
                string sampletypes = "";
                string Barid = "";
                string testname = "";


                int PID = Convert.ToInt32(Request.QueryString["PID"]);
                ViewState["PID"] = Request.QueryString["PID"].ToString();

                Patmst_Bal_C PatBC = new Patmst_Bal_C(Request.QueryString["PatRegID"].Trim(), Request.QueryString["FID"].Trim(), Convert.ToInt32(Session["Branchid"]));

                PatBC.Initial = cmbInitial.SelectedItem.Text.Trim();
                PatBC.Patname = txtFname.Text;
                string CounCode = ObjPBC1.GetSMSString_CountryCode("Registration", Convert.ToInt32(Session["Branchid"]));
                if (CounCode.Length == 2)
                {
                    PatBC.Phone = CounCode + "" + txttelno.Text;
                    // PBCL.TelNo = CounCode + "" + txttelno.Text;
                }
                else
                {
                    PatBC.Phone = CounCode + "" + txttelno.Text;
                }
                PatBC.Email = txtemail.Text;
                PatBC.Email = txtemail.Text;
                PatBC.Sex = txtsex.Text;
                PatBC.Age = int.Parse(txtAge.Text);
                PatBC.MYD = cmdYMD.SelectedItem.Text;
                PatBC.DoctorCode = txtDoctorName.Text.Trim();

                PatBC.Phrecdate = DateTimeConvesion.getDateFromString(Date.getdate().Date.ToString("dd/MM/yyyy")).Date;

                PatBC.Pataddress = txt_address.Text;
                PatBC.PatientcHistory = txt_clinicalhistory.Text;

                PatBC.TestCharges = Convert.ToSingle(0);
                PatBC.Tests = STCODE;
                ViewState["TCode"] = STCODE;
                PatBC.TestName = testname;
                PatBC.SampleType = sampletypes;
                PatBC.Username = Session["username"].ToString();



                PatBC.DoctorCode = DoctorCode[1].ToString().Trim();
                PatBC.Drname = DoctorCode[0].ToString();
                PatBC.Usertype = "patient";
                PatBC.OtherRefDoctor = txt_remark.Text;
                if (txtFname.Text.Length > 4)
                {
                    PatBC.P_Patusername = txtFname.Text.Substring(0, 4) + PID;
                    PatBC.P_Patpassword = txtFname.Text.Substring(0, 4) + PID;
                }
                else
                {
                    PatBC.P_Patusername = txtFname.Text.Trim() + PID;
                    PatBC.P_Patpassword = txtFname.Text.Trim() + PID;
                }
                PatBC.P_Patusername = PatBC.P_Patusername.Replace(" ", "");
                PatBC.P_Patpassword = PatBC.P_Patpassword.Replace(" ", "");

                if (txtWeight.Text != "")
                {
                    PatBC.P_Weights = txtWeight.Text;
                }
                else
                {
                    PatBC.P_Weights = "";
                }
                if (txtHeight.Text != "")
                {
                    PatBC.P_Heights = txtHeight.Text;
                }
                else
                {
                    PatBC.P_Heights = "";
                }
                if (txtDieses.Text != "")
                {
                    PatBC.P_Disease = txtDieses.Text;
                }
                else
                {
                    PatBC.P_Disease = "";
                }
                if (txtLastPeriod.Text != "")
                {
                    PatBC.P_LastPeriod = txtLastPeriod.Text;
                }
                else
                {
                    PatBC.P_LastPeriod = "";
                }
                if (txtSymptoms.Text != "")
                {
                    PatBC.P_Symptoms = txtSymptoms.Text;
                }
                else
                {
                    PatBC.P_Symptoms = "";
                }
                if (txtFSTime.Text != "")
                {
                    PatBC.P_FSTime = txtFSTime.Text;
                }
                else
                {
                    PatBC.P_FSTime = "";
                }
                if (txtTherapy.Text != "")
                {
                    PatBC.P_Therapy = txtTherapy.Text;
                }
                else
                {
                    PatBC.P_Therapy = "";
                }

                string RepStatus = rblretecate.SelectedItem.Text;
                if (RepStatus == "General")
                {
                    PatBC.P_SocialMedia = 0;
                }
                else if (RepStatus == "Viber")
                {
                    PatBC.P_SocialMedia = 1;
                }
                else
                {
                    PatBC.P_SocialMedia = 2;
                }
                PatBC.Update_PatientInfo(Convert.ToInt32(Session["Branchid"]));

                LblMsg.Text = "Patient info update successfully.!!!";
                // ciTL1.Delete(PID, Convert.ToInt32(Session["Branchid"]));





                //}

                //==================***=================================
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

    }





    protected void radioCode_CheckedChanged(object sender, EventArgs e)
    {

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
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
            sda = new SqlDataAdapter("select PackageCode as MTCode,PackageName as Maintestname from PackMst where (PackageCode like '%" + prefixText + "%' or PackageName like '%" + prefixText + "%') and PackageCode in (select PackageCode from PackmstD where SDCode in (select SDCode from SubDepartment where DigModule ='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "')) UNION " +
                              " select MTCode, Maintestname from MainTest WHERE (MTCode like '%" + prefixText + "%' or Maintestname like '%" + prefixText + "%') and SDCode in (select SDCode from SubDepartment where DigModule='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "') order by Maintestname ", con);
        else
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
            sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR' and DoctorName like '" + prefixText + "%' order by DoctorName", con);
        else
            sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR' and DoctorName like '" + prefixText + "%' order by DoctorName", con);
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

            if (Request.QueryString["Patname"] != null)
            {
                Response.Redirect("PatientEdit.aspx", false);
            }
            else
            {
                Response.Redirect("PatientEdit.aspx", false);
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

        sc1.CommandText = "ALTER VIEW [dbo].[VW_PatientCard] AS SELECT top(99.99) percent   dbo.Cshmst.BillNo, dbo.Cshmst.RecDate, dbo.Cshmst.BillType,   dbo.Cshmst.AmtReceived,  " +
              "  dbo.Cshmst.Discount, dbo.Cshmst.NetPayment, RecM.AmtPaid AS AmtPaid, dbo.Cshmst.Balance,  dbo.Cshmst.username,  " +
              "  dbo.Cshmst.OtherCharges,dbo.patmst.PatRegID, dbo.patmst.intial, dbo.patmst.Patname,   dbo.patmst.sex,  dbo.patmst.Age, " +
              "  dbo.patmst.Drname,  dbo.patmst.TelNo, dbo.DrMT.DoctorCode, dbo.DrMT.DoctorName, dbo.MainTest.Maintestname as Maintestname,    " +
              "  dbo.MainTest.MTCode,  dbo.patmstd.TestRate, dbo.PackMst.PackageName, dbo.patmstd.PackageCode, dbo.Cshmst.DisFlag, " +
              "  dbo.patmst.Patusername, dbo.patmst.Patpassword, dbo.Cshmst.Comment,dbo.patmst.MDY,dbo.patmst.Remark AS PatientRemark, " +
              "  dbo.patmst.Pataddress,dbo.patmst.PPID ,dbo.patmst.UnitCode , Cshmst.TaxAmount, Cshmst.TaxPer,   " +
              "  RecM.PrintCount as PrintCount,patmst.Email as EmailID FROM         patmst INNER JOIN    DrMT ON patmst.CenterCode = DrMT.DoctorCode AND   " +
              "  patmst.Branchid = DrMT.Branchid INNER JOIN   Cshmst INNER JOIN   MainTest INNER JOIN   patmstd ON  " +
              "  MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON Cshmst.PID = patmstd.PID AND   " +
              "  Cshmst.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid  " +
              "  INNER JOIN    RecM ON Cshmst.PID = RecM.PID AND Cshmst.BillNo = RecM.BillNo LEFT OUTER JOIN    " +
              "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode where  patmst.branchid=" + Session["Branchid"].ToString() + " and patmst.PPID='" + ViewState["PPID"] + "' and patmst.PatRegID='" + Request.QueryString["PatRegID"] + "'  order by Cshmst.billno desc  ";// and Cshmst.BillNo=" + bno + " DrMT.DrCheck_flag='CC' and


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
}