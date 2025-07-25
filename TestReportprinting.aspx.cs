using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.Management;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Management;

using System.Collections.Specialized;
using System.Text;
using QRCoder;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;

public partial class TestReportprinting : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    Patmstd_Bal_C ObjPBC = new Patmstd_Bal_C();
    PatSt_Bal_C psnew = new PatSt_Bal_C();
    DataTable dt = new DataTable();
    int g;
    string rptname = "", path = "", selectonFormula = "", PrintUserName = "";
    Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();
    Patmst_New_Bal_C PatNBC = new Patmst_New_Bal_C();
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string Date2 = DateTime.Now.AddDays(-1).ToString("ddMMyyyy");
    DataTable dtexistaut = new DataTable();
    protected void Page_Init(object sender, EventArgs e)
    {

        lblRegNo.Text = Request.QueryString["PatRegID"].ToString();

        Patmst_Bal_C Patmst = new Patmst_Bal_C();
        int PID = Patmst.getPID(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
        if (PID != -1)
        {
            ViewState["PID"] = PID;
        }
    }

  
    private void ShowReport(string ReportType)
    {


    }

    protected void cmdEmail_Click(object sender, EventArgs e)
    {

    }

    protected void cmdPrint_Click(object sender, EventArgs e)
    {

    }

    protected void cmdClose_Click(object sender, EventArgs e)
    {

    }

    public void Bindbanner()
    {
        DataTable dtban = new DataTable();
        DataTable dtchk = new DataTable();
        dtban = ObjTB.Bindbanner();
        string ISCashbill = DrMT_sign_Bal_C.getcheckmonthlybill(Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]), Lblcenter.Text);
        if (ISCashbill == "False")
        {
            Session["Monthlybill"] = "YES";

        }
        else
        {
            Session["Monthlybill"] = "No";
        }

        if (dtban.Rows.Count > 0)
        {
            if (Convert.ToString(dtban.Rows[0]["BalanceMail"]) == "0")
            {
                ViewState["VALIDATE"] = "YES";

            }
            else
            {
                ViewState["VALIDATE"] = "NO";

            }
        }
        if (Convert.ToString(Session["Monthlybill"]) == "YES")
        {
            ViewState["VALIDATE"] = "YES";
        }
        if (Convert.ToString(Session["usertype"]) == "Administrator")
        {            
            ViewState["VALIDATE"] = "YES";

        }
        if (Convert.ToString(Session["usertype"]) == "ReportView")
        {
            ViewState["VALIDATE"] = "YES";
        }
        //    string BName = " Diagnostic Center";
        //    string BCount = Patmst_New_Bal_C.PatientCountBanner_result(Convert.ToInt32(Session["Branchid"]));
        //    //  lblDemoHospitalName.Text = Convert.ToString(dtban.Rows[0]["BannerName"]).Trim();
        //    if (Convert.ToString(dtban.Rows[0]["BannerName"]).Trim() == BName)
        //    {
        //        if (Convert.ToInt32(BCount) > 610)
        //        {
        //            string Currentdate = Date.getdate().ToString("dd/MM/yyyy");
        //            if (Convert.ToDateTime(Currentdate) >= Convert.ToDateTime("12 /12/ 2019") && Convert.ToDateTime(Currentdate) < Convert.ToDateTime("14 / 12 / 2019"))
        //            {
        //                ObjTB.UpdateAuthincate_result(Convert.ToInt32(Session["Branchid"]));
        //            }

        //            dtchk = ObjTB.Checkflag_Result();
        //            if (dtchk.Rows.Count == 0)
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
        //                Response.Redirect("~/Login.aspx?Activation=Yes");
        //                Response.Redirect("~/Login.aspx");
        //            }
        //        }
        //    }
        //    else
        //    {

        //        if (Convert.ToInt32(BCount) > 610)
        //        {
        //            string Currentdate = Date.getdate().ToString("dd/MM/yyyy");
        //            if (Convert.ToDateTime(Currentdate) >= Convert.ToDateTime("12 /12/ 2017") && Convert.ToDateTime(Currentdate) < Convert.ToDateTime("14 / 12 / 2017"))
        //            {
        //                ObjTB.UpdateAuthincate_result(Convert.ToInt32(Session["Branchid"]));
        //            }

        //            dtchk = ObjTB.Checkflag_Result();
        //            if (dtchk.Rows.Count == 0)
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
        //                Response.Redirect("~/Login.aspx?Activation=Yes");
        //                Response.Redirect("~/Login.aspx");
        //            }
        //            else
        //            {
        //                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('contact to system administrator.');", true);
        //                //Response.Redirect("~/Login.aspx?Activation=Yes");
        //                //Response.Redirect("~/Login.aspx");
        //            }
        //        }

        //    }

        //}
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
        //    Response.Redirect("~/Login.aspx?Activation=Yes");
        //    Response.Redirect("~/Login.aspx");

        //}
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["usertype"]) == "Reference Doctor")
        {
            // btnemail.Visible = false;
            // btnemailToPatient.Visible = false;
            // btnsms.Visible = false;
            Chkdocemail.Visible = false;
            Chkemailtopatient.Visible = false;
            chkispatientsms.Visible = false;
            txtpatientmail.Visible = false;
            // cmdPrint.Visible = false;
            btnBack.Visible = false;
            btnduepay.Visible = false;
            BtnGenerateURL.Visible = false;
            btnWhatapp.Visible = false;
            txturl.Visible = false;
            chkcentermailLetter.Visible = false;
            chkcentermailwithotLetter.Visible = false;
        }
        else
        {
            Chkdocemail.Visible = true;
            Chkemailtopatient.Visible = true;
            chkispatientsms.Visible = true;
            txtpatientmail.Visible = true;

            btnBack.Visible = true;
            btnduepay.Visible = true;
            BtnGenerateURL.Visible = true;
            btnWhatapp.Visible = true;
            txturl.Visible = true;
            chkcentermailLetter.Visible = true;
            chkcentermailwithotLetter.Visible = true;
        }
     

        if (!Page.IsPostBack)
        {
            try
            {
                Cshmst_Bal_C Cshmst = new Cshmst_Bal_C();
                int PID = -1;
                if (Convert.ToInt32(ViewState["PID"]) != -1)
                {
                    PID = Convert.ToInt32(ViewState["PID"]);
                }
                ;
                Cshmst.getBalance(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), PID);
                float BAL_Amount = Cshmst.Balance;
                if (ViewState["VALIDATE"] == "YES")
                {
                    BAL_Amount = 0;
                }
                if (BAL_Amount > 0)
                {
                    cmdPrint_Balance.Visible = false;
                    cmdPrint.Visible = true;
                }
                else
                {
                    cmdPrint_Balance.Visible = false;
                    cmdPrint.Visible = true;
                }

                //LUNAME.Text = Convert.ToString(Session["username"]);
                // LblDCName.Text = Convert.ToString(Session["Bannername"]);
                // LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                // dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                //this.PopulateTreeView(dt, 0, null); 

                Fill_Labels();
                Bindbanner();
                Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                txtpatientmail.Text = Obj_PBC_C.Email;
                tvGroupTree.Nodes.Clear();
                tvGroupTree_Temp.Nodes.Clear();


                if (Convert.ToString(Session["usertype"]) == "Administrator")
                {
                    btnprint_letterhead.Enabled = true;

                    cmdPrint.Enabled = true;
                    ViewState["VALIDATE"] = "YES";

                }
                if (Convert.ToString(Session["usertype"]) == "ReportView")
                {
                    ViewState["VALIDATE"] = "YES";
                }
                string subdept = "";
                dt = PatNBC.Get_subdept(Convert.ToString(Session["username"]));
                if (dt.Rows.Count > 0)
                {
                    subdept = Convert.ToString(dt.Rows[0]["subdept"]);
                }

                //=============================Outsource Report=========================================

                ArrayList Obj_TestTest = (ArrayList)PatSt_new_Bal_C.GetPatst_OutLabReport(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
                for (int Obj_TID = 0; Obj_TID < Obj_TestTest.Count; Obj_TID++)
                {

                    ICollection icol = SubdepartmentLogic_Bal_C.GetSubdepartment_byCode((Obj_TestTest[Obj_TID] as PatSt_Bal_C).SDCode, Convert.ToInt32(Session["Branchid"])); //12
                    IEnumerator ie = icol.GetEnumerator();
                    while (ie.MoveNext())
                    {


                        Subdepartment_Bal_C Obj_SBC = (Subdepartment_Bal_C)ie.Current;
                        TreeNode Obj_TrN = tvGroupTree_Temp.FindNode(Obj_SBC.SDCode);
                        if (Obj_TrN == null)
                        {
                            Obj_TrN = new TreeNode(Obj_SBC.SubdeptName);
                            Obj_TrN.Checked = true;

                            Obj_TrN.Value = Obj_SBC.SDCode;
                            Obj_TrN.NavigateUrl = "#";

                            tvGroupTree_Temp.Nodes.Add(Obj_TrN);
                        }
                        ICollection Obj_ICTEST = MainTestLog_Bal_C.GetMaintestBy_Code((Obj_TestTest[Obj_TID] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                        IEnumerator Obj_IETEST = Obj_ICTEST.GetEnumerator();
                        PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();
                        while (Obj_IETEST.MoveNext())
                        {

                            MainTest_Bal_C Obj_MBC = Obj_IETEST.Current as MainTest_Bal_C;
                            TreeNode Obj_INTEST = new TreeNode(Obj_MBC.Maintestname);
                            Obj_INTEST.Value = Obj_MBC.MTCode;

                            string PTStatus = (Obj_TestTest[Obj_TID] as PatSt_Bal_C).Patauthicante;
                            string OutsourceRpt = (Obj_TestTest[Obj_TID] as PatSt_Bal_C).UploadOutSourceReport;


                            bool PStatus = (Obj_TestTest[Obj_TID] as PatSt_Bal_C).Patrepstatus;
                            if (PStatus == true)
                            {
                                Obj_INTEST.Text = Obj_MBC.Maintestname + "    <span class='btn btn-sm btn-primary'>(AttachFile Print" + ")</sapn>";
                                Obj_INTEST.Checked = false;
                                // Obj_INTEST.Checked = true;
                            }
                            else
                            {
                                Obj_INTEST.Text = Obj_MBC.Maintestname + "    <span class='btn btn-sm btn-primary'>(AttachFile Print" + ")</sapn>";
                                Obj_INTEST.Checked = true;
                            }

                            Obj_INTEST.ToolTip = "Main test";
                            Obj_INTEST.NavigateUrl = "~/" + PTStatus;// "~/UploadOutReport/1304_SLLPR_02052020_PrevResult.txt";

                            Obj_PB_C.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MBC.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(PrintUserName));

                            Obj_TrN.ChildNodes.Add(Obj_INTEST);

                        }

                        Obj_TrN.Expand();
                    }
                }
                //======================================================================
                ArrayList Obj_Test = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_notingroup(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
                for (int Obj_TID = 0; Obj_TID < Obj_Test.Count; Obj_TID++)
                {

                    ICollection icol = SubdepartmentLogic_Bal_C.GetSubdepartment_byCode((Obj_Test[Obj_TID] as PatSt_Bal_C).SDCode, Convert.ToInt32(Session["Branchid"])); //12
                    IEnumerator ie = icol.GetEnumerator();
                    while (ie.MoveNext())
                    {


                        Subdepartment_Bal_C Obj_SBC = (Subdepartment_Bal_C)ie.Current;
                        TreeNode Obj_TrN = tvGroupTree.FindNode(Obj_SBC.SDCode);
                        if (Obj_TrN == null)
                        {
                            Obj_TrN = new TreeNode(Obj_SBC.SubdeptName);
                            Obj_TrN.Checked = true;

                            Obj_TrN.Value = Obj_SBC.SDCode;
                            Obj_TrN.NavigateUrl = "#";

                            tvGroupTree.Nodes.Add(Obj_TrN);
                        }

                        ICollection Obj_ICTEST = MainTestLog_Bal_C.GetMaintestBy_Code((Obj_Test[Obj_TID] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                        IEnumerator Obj_IETEST = Obj_ICTEST.GetEnumerator();

                        while (Obj_IETEST.MoveNext())
                        {

                            MainTest_Bal_C Obj_MBC = Obj_IETEST.Current as MainTest_Bal_C;
                            TreeNode Obj_INTEST = new TreeNode(Obj_MBC.Maintestname);
                            Obj_INTEST.Value = Obj_MBC.MTCode;

                            string PTStatus = (Obj_Test[Obj_TID] as PatSt_Bal_C).Patauthicante;


                            bool PStatus = (Obj_Test[Obj_TID] as PatSt_Bal_C).Patrepstatus;
                            if (PStatus == true)
                            {
                                Obj_INTEST.Text = Obj_MBC.Maintestname + "    <span class='btn btn-sm btn-primary'>(Printed" + ")</sapn>";
                                Obj_INTEST.Checked = false;
                                // Obj_INTEST.Checked = true;
                            }
                            else
                            {
                                Obj_INTEST.Text = Obj_MBC.Maintestname;
                                Obj_INTEST.Checked = true;
                            }

                            Obj_INTEST.ToolTip = "Main test";
                            Obj_INTEST.NavigateUrl = "#";


                            Obj_TrN.ChildNodes.Add(Obj_INTEST);

                        }

                        Obj_TrN.Expand();
                    }
                }

                ArrayList Obj_Arr_Test = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_ingroup_new(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
                for (int Obj_TID = 0; Obj_TID < Obj_Arr_Test.Count; Obj_TID++)
                {

                    string PackageName = Packagenew_Bal_C.getPNameByCode((Obj_Arr_Test[Obj_TID] as PatSt_Bal_C).PackageCode, Convert.ToInt32(Session["Branchid"])); //12

                    TreeNode Obj_TrN = tvGroupTree.FindNode((Obj_Arr_Test[Obj_TID] as PatSt_Bal_C).PackageCode);
                    if (Obj_TrN == null)
                    {
                        Obj_TrN = new TreeNode(PackageName);

                        Obj_TrN.Checked = true;
                        Obj_TrN.Value = (Obj_Arr_Test[Obj_TID] as PatSt_Bal_C).PackageCode;
                        Obj_TrN.NavigateUrl = "#";

                        tvGroupTree.Nodes.Add(Obj_TrN);
                    }

                    ICollection Obj_ICTEST = MainTestLog_Bal_C.GetMaintestBy_Code((Obj_Arr_Test[Obj_TID] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                    IEnumerator Obj_IETEST = Obj_ICTEST.GetEnumerator();
                    while (Obj_IETEST.MoveNext())
                    {
                        MainTest_Bal_C Obj_MBC = Obj_IETEST.Current as MainTest_Bal_C;
                        TreeNode Obj_INTEST = new TreeNode(Obj_MBC.Maintestname);
                        Obj_INTEST.Value = Obj_MBC.MTCode;
                        Obj_INTEST.Text = Obj_MBC.Maintestname;
                        Obj_INTEST.ToolTip = "Main test";
                        Obj_INTEST.NavigateUrl = "#";

                        Obj_INTEST.Checked = true;
                        Obj_TrN.ChildNodes.Add(Obj_INTEST);
                    }

                    Obj_TrN.Expand();
                }
                if (tvGroupTree.Nodes.Count == 0)
                {
                    cmdPrint.Visible = false;

                }

                tvGroupGram.Nodes.Clear();

                ArrayList Obj_Test_AL = (ArrayList)PatSt_new_Bal_C.getPrintStatusTableByAuthorizedhemogram(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), "HM");
                for (int Obj_TID = 0; Obj_TID < Obj_Test_AL.Count; Obj_TID++)
                {
                    ICollection Obj_ICO_Test = SubdepartmentLogic_Bal_C.GET_subdeptName_bycode("HM", Convert.ToInt32(Session["Branchid"]));
                    IEnumerator Obj_IEO_Test = Obj_ICO_Test.GetEnumerator();

                    while (Obj_IEO_Test.MoveNext())
                    {
                        Subdepartment_Bal_C Obj_SB_C = (Subdepartment_Bal_C)Obj_IEO_Test.Current;
                        TreeNode Obj_TN_T = tvGroupTree.FindNode(Obj_SB_C.SDCode);
                        Obj_TN_T = new TreeNode(Obj_SB_C.SubdeptName);

                        Obj_TN_T.Checked = true; //subdeptName
                        Obj_TN_T.Value = Obj_SB_C.SDCode;
                        Obj_TN_T.NavigateUrl = "#";
                        tvGroupGram.Nodes.Add(Obj_TN_T);
                        //testname====================================================================================
                        ICollection igram = SubdepartmentLogic_Bal_C.GET_Hemetology_Test(Request.QueryString["PatRegID"], Request.QueryString["FID"], "HM");
                        IEnumerator ie1 = igram.GetEnumerator();
                        while (ie1.MoveNext())
                        {
                            Subdepartment_Bal_C Obj_SBC = (Subdepartment_Bal_C)ie1.Current;
                            TreeNode Obj_TN_TV = new TreeNode(Obj_SBC.testname);

                            Obj_TN_TV.Value = Obj_SBC.MTCode;
                            Obj_TN_TV.Text = Obj_SBC.testname;

                            Obj_TN_TV.Checked = true;
                            Obj_TN_T.ChildNodes.Add(Obj_TN_TV);

                        }//while

                    }//1st while close
                    if (tvGroupGram.Nodes[0].ChildNodes.Count > 0)
                    {

                    }
                    else
                    {

                        tvGroupGram.Visible = false;
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

    protected void tvGroupTree_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
    {

    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {

    }

    public void Fill_Labels()
    {
        #region  Information Of Patient
        Patmst_Bal_C Patmst = null;
        try
        {

            Patmst = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));

            lblRegNo.Text = Convert.ToString(Patmst.PatRegID);
            lbldate.Text = Patmst.FID;

            lblName.Text = Patmst.Patname;//Patmst.Initial.Trim() + "." +
            lblage.Text = Convert.ToString(Patmst.Age) + "/" + Patmst.MYD;
            lblSex.Text = Patmst.Sex;

            LblMobileno.Text = Patmst.Phone;
            Lblcenter.Text = Patmst.CenterName;
            lbldate.Text = Convert.ToString(Patmst.Patregdate);
            LblRefDoc.Text = Patmst.Drname;
        }
        catch
        {
            lblRegNo.Visible = true;
            lblRegNo.Text = "Record not foud";
        }
        #endregion
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

       // Response.Redirect("Addresult.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"],false);

       // Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = CloseWindow();");
        ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);
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


    protected void cmdPrint_Click2(object sender, EventArgs e)
    {
        
        GenerateQRCode();
        Cshmst_Bal_C Cshmst = new Cshmst_Bal_C();
        int PID = -1;
        if (Convert.ToInt32(ViewState["PID"]) != -1)
        {
            PID = Convert.ToInt32(ViewState["PID"]);
        }
        Cshmst.getBalance(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), PID);
        float BAL_Amount = Cshmst.Balance;
        if (Convert.ToString( ViewState["VALIDATE"]) == "YES")
        {
            BAL_Amount = 0;
        }
        if (BAL_Amount > 0)
        {
            Label6.Text = "Pending balance.";
            Label6.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
        }
        else
        {
            PReportWithBalance();
        }
        


    }

    protected void CVTest_Init(object sender, EventArgs e)
    {
        // Descrepo();
    }

    protected void CVTest_PreRender(object sender, EventArgs e)
    {
        Descrepo();
    }

    public void Descrepo()
    {
    }

   

    protected void btnprint_letterhead_Click(object sender, EventArgs e)
    {
        Cshmst_Bal_C Cshmst = new Cshmst_Bal_C();
        int PID = -1;
        if (Convert.ToInt32(ViewState["PID"]) != -1)
        {
            PID = Convert.ToInt32(ViewState["PID"]);
        }
        Cshmst.getBalance(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), PID);
        float BAL_Amount = Cshmst.Balance;
        if (Convert.ToString(ViewState["VALIDATE"]) == "YES")
        {
            BAL_Amount = 0;
        }
        if (BAL_Amount > 0)
        {
            Label6.Text = "Pending balance.";
            Label6.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
        }
        else
        {
            createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
            aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
            if (aut.P_QRCodeRequired == true)
            {
                GenerateQRCode();
                URLReportGenerate();
            }
            string R_Code = "";
            string TextDesc = "";

            string micro = "";
            string Histo = "";
            string Cyto = "";
            string DispTCode = "";
            string DispDespCode = "";
            string ViewTestCode = "";
            Label1.Text = Convert.ToString(hdReportno.Value);

            for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
            {
                if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
                {
                    for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                    {
                        if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
                        {

                            string IDT = tvGroupTree.Nodes[i].ChildNodes[j].Value;

                            MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(IDT);
                            R_Code = Obj_MTB_C.MTCode;
                            TextDesc = Obj_MTB_C.TextDesc;
                            DispTCode = Obj_MTB_C.SDCode;
                            PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();
                            dtexistaut = psnew.Check_Authorised_Test(lblRegNo.Text, Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                            if (dtexistaut.Rows[0]["Printedby"] != "")
                            {
                                PrintUserName = Convert.ToString(dtexistaut.Rows[0]["Printedby"]);
                            }
                            else
                            {

                                PrintUserName = Session["username"].ToString();
                            }
                            Obj_PB_C.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(PrintUserName));

                            //if (DispTCode == "CM")
                            //{
                            //    if (micro == "")
                            //    {
                            //        micro = "( MTCode = '" + R_Code;
                            //    }
                            //    else
                            //    {
                            //        micro = micro + "( MTCode = '" + R_Code;
                            //    }
                            //}
                            //else if (TextDesc == "DescType")
                            //{
                            //    if (DispDespCode == "")
                            //    {
                            //        DispDespCode = " (MTCode = '" + R_Code;
                            //    }
                            //    else
                            //    {
                            //        DispDespCode = DispDespCode + "' OR MTCode = '" + R_Code;
                            //    }
                            //}
                            //else
                            //    if (ViewTestCode == "")
                            //    {
                            //        ViewTestCode = " (MTCode = '" + R_Code;
                            //    }
                            //    else
                            //        ViewTestCode = ViewTestCode + "' OR MTCode = '" + R_Code;

                            if (DispTCode == "CM")
                            {
                                if (micro == "")
                                {
                                    micro = "( MTCode = '" + R_Code;
                                }
                                else
                                {
                                    micro = micro + "( MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "H1")
                            {
                                if (Histo == "")
                                {
                                    Histo = "( VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    // Histo = Histo + "( MTCode = '" + R_Code;
                                    Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "CY")//CY FN
                            {
                                //if (Cyto == "")
                                //{
                                //    Cyto = "( MTCode = '" + R_Code;
                                //}
                                //else
                                //{
                                //    Cyto = Cyto + "( MTCode = '" + R_Code;
                                //}
                                if (Cyto == "")
                                {
                                    Cyto = " (VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (TextDesc == "DescType")
                            {
                                if (DispDespCode == "")
                                {
                                    DispDespCode = " (VW_desfiledata.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    DispDespCode = DispDespCode + "' OR VW_desfiledata.MTCode = '" + R_Code;
                                }
                            }
                            else
                                if (ViewTestCode == "")
                                {
                                    ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + R_Code;
                                }
                                else
                                    ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + R_Code;

                            TAT_C at = new TAT_C();

                        }
                    }
                }
            }
            bool DescFlag = false;
            bool textflag = false;
            bool microflag = false;
            bool Histoflag = false;
            bool Cytoflag = false;
            if (DispDespCode != "")
            {
                DispDespCode = DispDespCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), DispDespCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                DescFlag = true;
            }
            else
            {

            }
            if (ViewTestCode != "")
            {
                ViewTestCode = ViewTestCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                textflag = true;
            }

            
            if (Histo != "")
            {
                Histo = Histo + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_HISTO(Convert.ToInt32(Session["Branchid"]), Histo, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Histoflag = true;
            }
            if (Cyto != "")
            {
                Cyto = Cyto + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_CYTO(Convert.ToInt32(Session["Branchid"]), Cyto, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Cytoflag = true;
            }

           
            if (textflag == true && DescFlag == true)
            {
                #region for Descriptive and No-Descriptive

                string[] Obj_A_Test = DispDespCode.Split(',');
                string[] targetArr = new string[Obj_A_Test.Length + 1];
                string[] urlArr = new string[Obj_A_Test.Length + 1];
                string[] featuresArr = new string[Obj_A_Test.Length + 1];

                CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                string formula = "", formula1 = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();

                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                string filename1 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;

                int line = 10;
                pm.topMargin = 200 * line;

                exportedpath = filename1;

                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

                CR.Close();
                CR.Dispose();
                GC.Collect();

                path = "";
                rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();


                string formula11 = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR1 = new ReportDocument();
                CR1.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_email.rpt"));
                SqlConnection con1 = DataAccess.ConInitForDC();

                int line1 = 10;
                int topMargin = 14 * line1;

                string filename22 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                System.IO.File.WriteAllText(filename22, "");

                SqlDataAdapter sda1 = null;
                DataTable dt1 = new DataTable();
                // DataSet1 dst1 = new DataSet1();
                sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);
                sda1.Fill(dt1);
                CR1.SetDataSource((System.Data.DataTable)dt1);
                ReportParameterClass.ReportType = "";

                ReportDocument crReportDocument1 = null;
                if (CR1 != null)
                {
                    crReportDocument1 = (ReportDocument)CR1;
                }
                CrystalDecisions.Shared.PageMargins pm1 = CR1.PrintOptions.PageMargins;

                int line11 = 10;
                pm1.topMargin = 200 * line;

                exportedpath = "";
                exportedpath = filename22;

                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR1);

                CR1.Close();
                CR1.Dispose();
                GC.Collect();

                rep.Close();
                rep.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = " Report Not Generated, Please Generate Once Again ";
                    return;
                }
                if (dt1.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                    FileInfo fi1 = new FileInfo(filepath11);
                    fi1.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again !!";
                    return;
                }
                string OrgFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                string DupFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

                string[] FilePathSplitOrg = OrgFile.Split('$');
                string[] FilePathSplitDup = DupFile.Split('$');

                if (FilePathSplitOrg[1] != FilePathSplitDup[1])
                {

                    string pathh = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");

                    foreach (string file in Directory.GetFiles(pathh))
                    {
                        string[] NewFile = file.Split('$');
                        if (FilePathSplitOrg[1] != NewFile[1])
                        {
                            File.Delete(file);
                        }
                    }
                }
                urlArr[Obj_A_Test.Length] = "Testnormalresult.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf";
                targetArr[Obj_A_Test.Length] = "1";
                featuresArr[Obj_A_Test.Length] = "";
                string OrgFileDesc = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                string DupFileDesc = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                string[] FilePathSplitOrgDesc = OrgFileDesc.Split('$');
                string[] FilePathSplitDupDesc = DupFileDesc.Split('$');

                if (FilePathSplitOrgDesc[1] != FilePathSplitDupDesc[1])
                {
                    string pathh = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                    foreach (string file in Directory.GetFiles(pathh))
                    {
                        string[] NewFile = file.Split('$');
                        if (FilePathSplitOrg[1] != NewFile[1])
                        {
                            File.Delete(file);
                        }
                    }
                }

                urlArr[0] = "Redirect.aspx?rt=DescType&RepName=PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf";

                ResponseHelper.Redirect(urlArr, targetArr, featuresArr);

                #endregion
            }
            else if (textflag == true && DescFlag == false && microflag == false)
            {
                #region Only Nondescriptive

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();

                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                //  DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                string filename1 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
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
                    string filepath11 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                string OrgFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                string DupFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
                Response.Redirect("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

                #endregion
            }

            else if (DescFlag == true && microflag == false && textflag == false)
            {
                #region Only Descriptive

               
                #region Only Nondescriptive

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                string filename1 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename1;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                string OrgFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                string DupFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

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
                // Sundept

                Response.Redirect("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                #endregion
                #endregion
            }
            if (Histoflag == true)
            {
                #region Only Histo Report

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_HISTO_Email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org_HISTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename1;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
                string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");

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
                // Sundept

                Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");

                #endregion
            }
            if (Cytoflag == true)
            {
                #region Only CYTO Report

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_CYTO_EMAIL.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org_CYTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename1;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
                string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");

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
                // Sundept

                Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");

                #endregion
            }
        }
    }


    public void DeleteFile(string folderName, string ReportType)
    {
        string OrgFile = Server.MapPath(folderName + "//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + ReportType + ".pdf");
        string DupFile = Server.MapPath(folderName + "//" + "$" + lblName.Text + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + ReportType + ".pdf");

        string[] FilePathSplitOrg = OrgFile.Split('$');
        string[] FilePathSplitDup = DupFile.Split('$');

        if (FilePathSplitOrg[1] != FilePathSplitDup[1])
        {
            string path = Server.MapPath("/" + Request.ApplicationPath + "/" + folderName + "/");

            foreach (string file in Directory.GetFiles(path))
            {
                string[] NewFile = file.Split('$');
                if (NewFile.Length > 1)
                {
                    if (FilePathSplitOrg[1] != NewFile[1])
                    {
                        File.Delete(file);
                    }
                }
            }
        }
    }
    protected void chkispatientsms_CheckedChanged(object sender, EventArgs e)
    {
        if (chkispatientsms.Checked == true)
        {
            Cshmst_Bal_C cashmain = new Cshmst_Bal_C();
            cashmain.getBalance(Convert.ToString(ViewState["PID"]), Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            if (cashmain.Balance > 0)
            {
                Label6.Text = "pending balance";
                Label6.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
            }
            else
            {
                PatSt_Bal_C ps = new PatSt_Bal_C();
                ps.PatRegID = lblRegNo.Text.Trim();
                ps.FID = Request.QueryString["FID"];
                string shortform = ps.GetShortform(Convert.ToInt32(Session["Branchid"]));
                Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
                string mobile = Obj_PBC_C.Phone;
                string email = Obj_PBC_C.Email;
                string msg = "";
                string CounCode = ObjPBC.GetSMSString_CountryCode("Registration", Convert.ToInt32(Session["Branchid"]));
                if (CounCode.Length == 2)
                {
                    if (mobile != CounCode && mobile != "")
                    {

                        createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                        aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        string Labname = aut.P_LabSmsName;
                        string SMSapistring = aut.P_LabSmsString;
                        string Labwebsite = aut.P_LabWebsite;

                        msg = "Result Of %3a " + pname + " %3bPatient ID%3a " + Request.QueryString["PatRegID"] + " is " + shortform;

                        SMSapistring = SMSapistring.ToString().Replace("#message#", msg);
                        SMSapistring = SMSapistring.Replace("#Labname#", Labname);
                        SMSapistring = SMSapistring.Replace("#phone#", mobile);
                        try
                        {
                            string url = apicall(SMSapistring);
                            Label44.Text = "SMS send successfully";
                            Label44.Visible = true;
                            Label6.Text = "SMS send successfully";
                            Label6.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('SMS send successfully.');", true);
                        }
                        catch (Exception exx)
                        { }

                    }
                }
                else
                {
                    if (mobile != CounCode && mobile != "")
                    {

                        createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                        aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        string Labname = aut.P_LabSmsName;
                        string SMSapistring = aut.P_LabSmsString;
                        string Labwebsite = aut.P_LabWebsite;

                        msg = "Result Of %3a " + pname + " %3bPatient ID%3a " + Request.QueryString["PatRegID"] + " is " + shortform;

                        SMSapistring = SMSapistring.ToString().Replace("#message#", msg);
                        SMSapistring = SMSapistring.Replace("#Labname#", Labname);
                        SMSapistring = SMSapistring.Replace("#phone#", mobile);
                        try
                        {
                            string url = apicall(SMSapistring);
                            Label44.Text = "SMS send successfully";
                            Label44.Visible = true;
                            Label6.Text = "SMS send successfully";
                            Label6.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('SMS send successfully.');", true);
                        }
                        catch (Exception exx)
                        { }

                    }
                }
            }
        }
    }
    protected void Chkemailtopatient_CheckedChanged(object sender, EventArgs e)
    {
        GenerateQRCode();
        if (Chkemailtopatient.Checked == true)
        {
            Cshmst_Bal_C Cshmst = new Cshmst_Bal_C();
            int PID = -1;
            if (Convert.ToInt32(ViewState["PID"]) != -1)
            {
                PID = Convert.ToInt32(ViewState["PID"]);
            }
            Cshmst.getBalance(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), PID);
            float BAL_Amount = Cshmst.Balance;
            if (Convert.ToString(ViewState["VALIDATE"]) == "YES")
            {
                BAL_Amount = 0;
            }
            //else
            //{
            //if (cashmain.Balance > 0)
            if (BAL_Amount > 0)
            {
                Label6.Text = "Pending balance.";
                Label6.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
            }

            else
            {

                Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
                string mobile = Obj_PBC_C.Phone;
                string email = Obj_PBC_C.Email;
                string docemail = DrMT_Bal_C.GetEmaildrNameTable(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                if (aut.P_email == "")
                {
                    Label6.Text = "Email id not found";
                    Label6.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email id not found.');", true);
                    return;

                }
                else if (email == "")
                {
                    {
                        Label6.Text = "Patient E-mail id not found";
                        Label6.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Patient E-mail id not found.');", true);
                        return;
                    }
                }
                else
                {
                    Label6.Visible = false;
                }



                #region sendemail
                string R_Code = "";
                string TextDesc = "";
                string DispTCode = "";
                string DispDespCode = "";
                string ViewTestCode = "";
                string testname = "";

                string micro = "";
                string Histo = "";
                string Cyto = "";


                Label1.Text = Convert.ToString(hdReportno.Value);
                string Sundept = "";
                for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
                {
                    for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                    {
                        if (tvGroupTree.Nodes[i].ChildNodes[j].Checked)
                        {
                            if (testname == "")
                            {
                                testname = tvGroupTree.Nodes[i].ChildNodes[j].Text;
                            }
                            else
                            {
                                testname += "," + tvGroupTree.Nodes[i].ChildNodes[j].Text;
                            }
                        }
                    }
                }
                for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
                {
                    if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
                    {
                        for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                        {
                            if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
                            {

                                string h = tvGroupTree.Nodes[i].Value;
                                string t = tvGroupTree.Nodes[i].ChildNodes[j].Value;
                                if (h == "AS")
                                {
                                    Sundept = "MICROBIOLOGY";
                                }
                                MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(t);
                                R_Code = Obj_MTB_C.MTCode;
                                TextDesc = Obj_MTB_C.TextDesc;
                                DispTCode = Obj_MTB_C.SDCode;
                                PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();
                                Obj_PB_C.UpdateEmailstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString( Session["username"]));

                                if (DispTCode == "CM")
                                {
                                    if (micro == "")
                                    {
                                        micro = "( MTCode = '" + R_Code;
                                    }
                                    else
                                    {
                                        micro = micro + "( MTCode = '" + R_Code;
                                    }
                                }
                                else if (DispTCode == "H1")
                                {
                                    if (Histo == "")
                                    {
                                        Histo = "( VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                    }
                                    else
                                    {
                                        // Histo = Histo + "( MTCode = '" + R_Code;
                                        Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                    }
                                }
                                else if (DispTCode == "CY")//CY FN
                                {
                                    //if (Cyto == "")
                                    //{
                                    //    Cyto = "( MTCode = '" + R_Code;
                                    //}
                                    //else
                                    //{
                                    //    Cyto = Cyto + "( MTCode = '" + R_Code;
                                    //}
                                    if (Cyto == "")
                                    {
                                        Cyto = " (VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                    }
                                    else
                                    {
                                        Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                    }
                                }
                                else if (TextDesc == "DescType")//&& DispTCode != "FN" && DispTCode != "H1"
                                {
                                    if (DispDespCode == "")
                                    {
                                        DispDespCode = " (VW_desfiledata.MTCode = '" + R_Code;
                                    }
                                    else
                                    {
                                        DispDespCode = DispDespCode + "' OR VW_desfiledata.MTCode = '" + R_Code;
                                    }
                                }
                                else
                                    if (ViewTestCode == "")
                                    {
                                        ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + R_Code;
                                    }
                                    else
                                        ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + R_Code;
                            }
                        }
                    }
                }
                bool DescFlag = false;
                bool textflag = false;
                bool microflag = false;
                bool Histoflag = false;
                bool Cytoflag = false;

                if (DispDespCode != "")
                {
                    DispDespCode = DispDespCode + "')";
                    AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                    VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), DispDespCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                    ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                    DescFlag = true;
                }
                if (ViewTestCode != "")
                {
                    ViewTestCode = ViewTestCode + "')";
                    AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                    VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                    textflag = true;
                }
                if (Histo != "")
                {
                    Histo = Histo + "')";
                    AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                    VW_DescriptiveViewLogic.SP_GetAlterView_HISTO(Convert.ToInt32(Session["Branchid"]), Histo, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                    ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                    Histoflag = true;
                }
                if (Cyto != "")
                {
                    Cyto = Cyto + "')";
                    AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                    VW_DescriptiveViewLogic.SP_GetAlterView_CYTO(Convert.ToInt32(Session["Branchid"]), Cyto, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                    ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                    Cytoflag = true;
                }
                string filename = Server.MapPath("rpts");
                string filename1 = Server.MapPath("rpts");
                if (textflag == true && DescFlag == true)
                {
                    #region Nondescriptive and Descriptive

                    CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    string formula = "";
                    selectonFormula = ReportParameterClass.SelectionFormula;
                    ReportDocument CR = new ReportDocument();
                    if (RepHType.SelectedValue == "0")
                    {
                        CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                    }
                    else
                    {
                        CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt"));
                    }

                    SqlConnection con = DataAccess.ConInitForDC();
                    SqlDataAdapter sda = null;
                    DataTable dt = new DataTable();
                    // DataSet1 dst = new DataSet1();
                    sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);
                    sda.Fill(dt);

                    CR.SetDataSource((System.Data.DataTable)dt);
                    string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                    string filename11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    System.IO.File.WriteAllText(filename11, "");
                    string exportedpath = "", selectionFormula = "";
                    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                    ReportDocument crReportDocument = null;
                    if (CR != null)
                    {
                        crReportDocument = (ReportDocument)CR;
                    }
                    CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                    //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                    int line = 10;
                    pm.topMargin = 200 * line;
                    // CR.PrintOptions.ApplyPageMargins(pm);

                    exportedpath = filename11;
                    cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);


                    CR.Close();
                    CR.Dispose();
                    GC.Collect();

                    path = "";
                    rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    //  RptTestResultMemo_Email objRptTestResultMemo_Email = new RptTestResultMemo_Email();
                    // PdfExportOptions pdfOptions = objRptTestResultMemo_Email.ExportOptions.Pdf;

                    // int line1 = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                  
                    // objRptTestResultMemo_Email.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);                              

                    CrystalDecisions.CrystalReports.Engine.ReportDocument repD = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                    selectonFormula = ReportParameterClass.SelectionFormula;
                    ReportDocument CRD = new ReportDocument();
                    if (RepHType.SelectedValue == "0")
                    {
                        CRD.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive_Email.rpt"));
                    }
                    else
                    {
                        CRD.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt"));
                    }

                    SqlConnection con1 = DataAccess.ConInitForDC();
                    SqlDataAdapter sda1 = null;
                    DataTable dt1 = new DataTable();
                    //  DataSet1 dst1 = new DataSet1();
                    sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

                    sda1.Fill(dt1);

                   
                    
                    CRD.SetDataSource((System.Data.DataTable)dt1);
                    string pathD = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                    string filenameD = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                    System.IO.File.WriteAllText(filenameD, "");
                    //filename = filename + "\\mailexported1.pdf";
                    string filename22 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                    string exportedpathD = "", selectionFormulaD = "";
                    ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                    ReportDocument crReportDocumentD = null;
                    if (CRD != null)
                    {
                        crReportDocumentD = (ReportDocument)CRD;
                    }
                    CrystalDecisions.Shared.PageMargins pmD = CRD.PrintOptions.PageMargins;
                    //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                    int lineD = 10;
                    pmD.topMargin = 200 * lineD;
                    // CR.PrintOptions.ApplyPageMargins(pm);

                    exportedpathD = filename22;
                    cl.ExportandPrintr("pdf", pathD, exportedpathD, formula, CRD);


                    CRD.Close();
                    CRD.Dispose();
                    GC.Collect();

                    //  objRptTestResultMemo_Email.ExportToPdf(filename22);
                    ReportParameterClass.ReportType = "";
                    repD.Close();
                    repD.Dispose();
                    GC.Collect();

                    if (dt.Rows.Count == 0)
                    {
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        Label44.Text = " Report Not Generated, Please Generate Once Again";
                        return;
                    }
                    if (dt1.Rows.Count == 0)
                    {
                        string filepath111 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                        FileInfo fi1 = new FileInfo(filepath111);
                        fi1.Delete();
                        Label44.Text = " Not Generated, Please Generate Once Again ";
                        return;
                    }

                    if (aut.P_email != "")
                    {
                        bool flag = true;

                        MailAddress to = new MailAddress(email.Trim());

                        if (email != "" && email != null)
                        {
                            to = new MailAddress(email.Trim());
                        }

                        MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                        MailMessage msgmail = new MailMessage(from, to);
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));
                        Attachment att1 = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
                        msgmail.Attachments.Add(att);
                        msgmail.Attachments.Add(att1);



                        msgmail.Subject = "Reg No :" + lblRegNo.Text + "-" + pname;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Port = aut.P_Port;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;

                        smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                        msgmail.Priority = MailPriority.High;
                        try
                        {
                            msgmail.IsBodyHtml = true;
                            msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You.";
                            smtp.Send(msgmail);

                            att.Dispose();
                            att1.Dispose();
                        }
                        catch (Exception)
                        {
                            flag = false;
                        }
                        if (flag == true)
                        {
                            if (aut.P_email == "")
                            {
                                return;
                            }
                            Label6.Text = "E-mail send successfully.";
                            Label6.Visible = true;
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                            FileInfo fi = new FileInfo(filepath11);
                            fi.Delete();
                            string filepath111 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                            FileInfo fi1 = new FileInfo(filepath111);
                            fi1.Delete();
                            Label44.Text = "E-mail send successfully";
                            Label44.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                        }
                        else
                        {

                            Label6.Text = "Error In E-mail Sending";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                            Label6.Visible = true;
                        }
                        DeleteFile("EmailReport", "Nondescriptive");
                        DeleteFile("EmailReport", "Descriptive");
                        DeleteFile("EmailReport", "Hemogram");
                    }

                    #endregion

                }
                else if (Cytoflag == true)
                {
                    #region Only CYTO Report

                    string formulaC = "";
                    selectonFormula = ReportParameterClass.SelectionFormula;
                    ReportDocument CRC = new ReportDocument();
                    if (RepHType.SelectedValue == "0")
                    {
                        CRC.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_CYTO_Email.rpt"));
                    }
                    else
                    {
                        CRC.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_CYTO.rpt"));
                    }
                    SqlConnection conC = DataAccess.ConInitForDC();

                    SqlDataAdapter sdaC = null;
                    DataTable dt = new DataTable();
                    // DataSet1 dst = new DataSet1();
                    sdaC = new SqlDataAdapter("select * from VW_desfiledata_org_CYTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", conC);

                    sdaC.Fill(dt);

                    CRC.SetDataSource((System.Data.DataTable)dt);
                    string pathC = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");

                    string filename11C = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
                    System.IO.File.WriteAllText(filename11C, "");
                    string exportedpathC = "", selectionFormulaC = "";
                    ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                    ReportDocument crReportDocumentC = null;

                    if (CRC != null)
                    {
                        crReportDocumentC = (ReportDocument)CRC;
                    }
                    CrystalDecisions.Shared.PageMargins pmC = CRC.PrintOptions.PageMargins;

                    int linec = 10;
                    pmC.topMargin = 200 * linec;
                    //CR.PrintOptions.ApplyPageMargins(pm);

                    exportedpathC = filename11C;
                    cl.ExportandPrintr("pdf", pathC, exportedpathC, formulaC, CRC);
                    CRC.Close();
                    CRC.Dispose();
                    GC.Collect();

                    if (dt.Rows.Count == 0)
                    {
                        string filepath11C = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11C);
                        fi.Delete();
                        Label44.Text = "Report Not Generated, Please Generate Once Again ";
                        return;
                    }

                    if (aut.P_email != "")
                    {
                        bool flag = true;

                        MailAddress to = new MailAddress(email.Trim());

                        if (email != "" && email != null)
                        {
                            to = new MailAddress(email.Trim());
                        }
                        MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                        MailMessage msgmail = new MailMessage(from, to);
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf"));



                        msgmail.Attachments.Add(att);
                        msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                        SmtpClient smtp = new SmtpClient();
                        //smtp.Port = 25;
                        smtp.Port = aut.P_Port;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                        msgmail.Priority = MailPriority.High;
                        try
                        {
                            msgmail.IsBodyHtml = true;
                            msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You";
                            smtp.Send(msgmail);
                            att.Dispose();
                        }
                        catch (Exception)
                        {
                            flag = false;
                        }
                        if (flag == true)
                        {
                            if (aut.P_email == "")
                            {
                                return;
                            }
                            Label6.Text = "E-mail send successfully.";
                            Label6.Visible = true;
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
                            FileInfo fi = new FileInfo(filepath11);
                            fi.Delete();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                        }
                        else
                        {

                            Label6.Text = "Error In E-mail Sending";
                            Label6.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                        }

                        DeleteFile("EmailReport", "CYTO_Desc");
                        DeleteFile("EmailReport", "Nondescriptive");
                        DeleteFile("EmailReport", "Hemogram");

                    #endregion
                    }
                }
                else if (Histoflag == true)
                {
                    #region Only HisTo Report

                    string formulaH = "";
                    selectonFormula = ReportParameterClass.SelectionFormula;
                    ReportDocument CRH = new ReportDocument();
                    if (RepHType.SelectedValue == "0")
                    {
                        CRH.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_HISTO_Email.rpt"));
                    }
                    else
                    {
                        CRH.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_HISTO.rpt"));
                    }
                    SqlConnection conH = DataAccess.ConInitForDC();

                    SqlDataAdapter sdaH = null;
                    dt = new DataTable();

                    sdaH = new SqlDataAdapter("select * from VW_desfiledata_org_HISTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", conH);

                    sdaH.Fill(dt);

                    CRH.SetDataSource((System.Data.DataTable)dt);
                    string pathH = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");

                    string filename11H = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
                    System.IO.File.WriteAllText(filename11H, "");
                    string exportedpathH = "", selectionFormulaH = "";
                    ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                    ReportDocument crReportDocumentH = null;

                    if (CRH != null)
                    {
                        crReportDocumentH = (ReportDocument)CRH;
                    }
                    CrystalDecisions.Shared.PageMargins pmH = CRH.PrintOptions.PageMargins;

                    int lineH = 10;
                    pmH.topMargin = 200 * lineH;
                    //CR.PrintOptions.ApplyPageMargins(pm);

                    exportedpathH = filename11H;
                    cl.ExportandPrintr("pdf", pathH, exportedpathH, formulaH, CRH);
                    CRH.Close();
                    CRH.Dispose();
                    GC.Collect();

                    if (dt.Rows.Count == 0)
                    {
                        string filepath11H = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11H);
                        fi.Delete();
                        Label44.Text = "Report Not Generated, Please Generate Once Again ";
                        return;
                    }

                    if (aut.P_email != "")
                    {
                        bool flag = true;

                        MailAddress to = new MailAddress(email.Trim());

                        if (email != "" && email != null)
                        {
                            to = new MailAddress(email.Trim());
                        }
                        MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                        MailMessage msgmail = new MailMessage(from, to);
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf"));


                        msgmail.Attachments.Add(att);
                        msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                        SmtpClient smtp = new SmtpClient();
                        //smtp.Port = 25;
                        smtp.Port = aut.P_Port;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                        msgmail.Priority = MailPriority.High;
                        try
                        {
                            msgmail.IsBodyHtml = true;
                            msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You";
                            smtp.Send(msgmail);
                            att.Dispose();
                        }
                        catch (Exception)
                        {
                            flag = false;
                        }
                        if (flag == true)
                        {
                            if (aut.P_email == "")
                            {
                                return;
                            }
                            Label6.Text = "E-mail send successfully.";
                            Label6.Visible = true;
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
                            FileInfo fi = new FileInfo(filepath11);
                            fi.Delete();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                        }
                        else
                        {

                            Label6.Text = "Error In E-mail Sending";
                            Label6.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                        }

                        DeleteFile("EmailReport", "HISTO_Desc");
                        DeleteFile("EmailReport", "Nondescriptive");
                        DeleteFile("EmailReport", "Hemogram");

                    #endregion
                    }
                }
                else if (textflag == true)
                {
                    #region Nondescriptive
                    string formula = "";
                    selectonFormula = ReportParameterClass.SelectionFormula;
                    ReportDocument CR = new ReportDocument();
                    if (RepHType.SelectedValue == "0")
                    {
                        CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                    }
                    else
                    {
                        CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt"));
                    }
                    SqlConnection con = DataAccess.ConInitForDC();

                    SqlDataAdapter sda = null;
                    dt = new DataTable();
                    // DataSet1 dst = new DataSet1();
                    sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                    sda.Fill(dt);
                    CR.SetDataSource((System.Data.DataTable)dt);
                    string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                    string filename11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    System.IO.File.WriteAllText(filename11, "");
                    string exportedpath = "", selectionFormula = "";
                    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                    ReportDocument crReportDocument = null;
                    if (CR != null)
                    {
                        crReportDocument = (ReportDocument)CR;
                    }
                    CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                    //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                    int line = 10;
                    pm.topMargin = 200 * line;
                    //CR.PrintOptions.ApplyPageMargins(pm);

                    exportedpath = filename11;
                    cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                    CR.Close();
                    CR.Dispose();
                    GC.Collect();

                    if (dt.Rows.Count == 0)
                    {
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        Label44.Text = "Report Not Generated, Please Generate Once Again ";
                        return;
                    }

                    if (aut.P_email != "")
                    {
                        bool flag = true;

                        MailAddress to = new MailAddress(email.Trim());

                        if (email != "" && email != null)
                        {
                            to = new MailAddress(email.Trim());
                        }
                        MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                        MailMessage msgmail = new MailMessage(from, to);
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));

                        if (tvGroupGram.Nodes.Count > 0)
                        {
                            if (tvGroupGram.Nodes[0].ChildNodes.Count > 0 && tvGroupGram.Nodes[0].ChildNodes[0].Checked)
                            {

                            }
                        }

                        msgmail.Attachments.Add(att);
                        msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                        SmtpClient smtp = new SmtpClient();
                        //smtp.Port = 25;
                        smtp.Port = aut.P_Port;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                        msgmail.Priority = MailPriority.High;
                        try
                        {
                            msgmail.IsBodyHtml = true;
                            msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You";
                            smtp.Send(msgmail);
                            att.Dispose();
                        }
                        catch (Exception)
                        {
                            flag = false;
                        }
                        if (flag == true)
                        {
                            if (aut.P_email == "")
                            {
                                return;
                            }
                            Label6.Text = "E-mail send successfully.";
                            Label6.Visible = true;
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                            FileInfo fi = new FileInfo(filepath11);
                            fi.Delete();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                        }
                        else
                        {

                            Label6.Text = "Error In E-mail Sending";
                            Label6.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                        }

                        DeleteFile("EmailReport", "Nondescriptive");
                        DeleteFile("EmailReport", "Hemogram");
                    }
                    #endregion
                }
                else
                {
                    #region Descriptive
                    string formula = "";
                    int line = 10;
                    int topMargin = 14 * line;
                    //  objRptTestResultMemo_Email.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);

                    CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                    selectonFormula = ReportParameterClass.SelectionFormula;
                    ReportDocument CR = new ReportDocument();
                    if (RepHType.SelectedValue == "0")
                    {
                        CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive_Email.rpt"));
                    }
                    else
                    {
                        CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt"));
                    }
                    SqlConnection con = DataAccess.ConInitForDC();


                    //SqlConnection con = DataAccess.ConInitForDC();
                    SqlDataAdapter sda = null;
                    dt = new DataTable();
                    // DataSet1 dst = new DataSet1();
                    sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                    sda.Fill(dt);
                    string filename22 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                    CR.SetDataSource((System.Data.DataTable)dt);
                    string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                    string filename11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                    System.IO.File.WriteAllText(filename11, "");

                   
                    ReportParameterClass.ReportType = "";

                    string exportedpath = "", selectionFormula = "";
                    ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                    ReportDocument crReportDocumentD = null;
                    if (CR != null)
                    {
                        crReportDocumentD = (ReportDocument)CR;
                    }
                    CrystalDecisions.Shared.PageMargins pmD = CR.PrintOptions.PageMargins;
                    //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                    int lineD = 10;
                    pmD.topMargin = 200 * lineD;
                    // CR.PrintOptions.ApplyPageMargins(pm);

                    exportedpath = filename11;
                    cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);


                    CR.Close();
                    CR.Dispose();
                    GC.Collect();

                    rep.Close();
                    rep.Dispose();
                    GC.Collect();
                    if (dt.Rows.Count == 0)
                    {
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        Label44.Text = "Report Not Generated, Please Generate Once Again ";
                        return;
                    }
                    if (aut.P_email != "")
                    {
                        bool flag = true;

                        MailAddress to = new MailAddress(email.Trim());

                        if (email != "" && email != null)
                        {
                            to = new MailAddress(email.Trim());
                        }

                        MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);

                        MailMessage msgmail = new MailMessage(from, to);
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
                        msgmail.Attachments.Add(att);

                        msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                        SmtpClient smtp = new SmtpClient();
                        //smtp.Port = 25;
                        smtp.Port = aut.P_Port;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;

                        smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                        msgmail.Priority = MailPriority.High;


                        try
                        {
                            msgmail.IsBodyHtml = true;
                            msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You.";
                            smtp.Send(msgmail);
                            att.Dispose();
                        }
                        catch (Exception)
                        {
                            flag = false;
                        }
                        if (flag == true)
                        {
                            if (aut.P_email == "")
                            {
                                return;
                            }
                            Label6.Text = "E-mail send successfully.";
                            Label6.Visible = true;
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                            FileInfo fi = new FileInfo(filepath11);
                            fi.Delete();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email send successfully.');", true);
                        }
                        else
                        {

                            Label6.Text = "Error In E-mail Sending";
                            Label6.Visible = true;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                        }
                    }

                    DeleteFile("EmailReport", "Descriptive");
                    #endregion
                }
                #endregion

            }
            //}
        }

    }
    protected void Chkdocemail_CheckedChanged(object sender, EventArgs e)
    {
        if (Chkdocemail.Checked == true)
        {
            Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));


            string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
            string mobile = Obj_PBC_C.Phone;
            string email = Obj_PBC_C.Email;
            string docemail = DrMT_Bal_C.GetEmaildrNameTable(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));


            createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
            aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
            if (aut.P_email == "")
            {
                Label6.Text = "Administrators E-mail not exist";
                Label6.Visible = true;
                return;
            }
            else if (docemail == "")
            {
                Label6.Text = "Doctor E-mail not exist";
                Label6.Visible = true;

                return;

            }
            else
            {
                Label6.Visible = false;
            }
            #region sendemail

            string R_Code = "";
            string TextDesc = "";
            string DispTCode = "";
            string DispDespCode = "";
            string ViewTestCode = "";
            string testname = "";
            string shortname = "";

            string micro = "";
            string Histo = "";
            string Cyto = "";
            for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
            {
                for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                {
                    if (tvGroupTree.Nodes[i].ChildNodes[j].Checked)
                    {
                        if (testname == "")
                        {
                            testname = tvGroupTree.Nodes[i].ChildNodes[j].Text;

                        }
                        else
                        {
                            testname += "," + tvGroupTree.Nodes[i].ChildNodes[j].Text;

                        }
                    }
                }
            }
            #region Get MTCode
            for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
            {
                if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
                {
                    for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                    {
                        if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
                        {
                            string h = tvGroupTree.Nodes[i].Value;
                            string t = tvGroupTree.Nodes[i].ChildNodes[j].Value;
                            MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
                            R_Code = Obj_MTB_C.MTCode;
                            TextDesc = Obj_MTB_C.TextDesc;
                            DispTCode = Obj_MTB_C.SDCode;
                            PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();
                            Obj_PB_C.UpdateEmailstatus_Doc(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]));

                            if (DispTCode == "CM")
                            {
                                if (micro == "")
                                {
                                    micro = "( MTCode = '" + R_Code;
                                }
                                else
                                {
                                    micro = micro + "( MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "H1")
                            {
                                if (Histo == "")
                                {
                                    Histo = "( VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    // Histo = Histo + "( MTCode = '" + R_Code;
                                    Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "CY")//CY FN
                            {
                                //if (Cyto == "")
                                //{
                                //    Cyto = "( MTCode = '" + R_Code;
                                //}
                                //else
                                //{
                                //    Cyto = Cyto + "( MTCode = '" + R_Code;
                                //}
                                if (Cyto == "")
                                {
                                    Cyto = " (VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (TextDesc == "DescType")//&& DispTCode != "FN" && DispTCode != "H1"
                            {
                                if (DispDespCode == "")
                                {
                                    DispDespCode = " (VW_desfiledata.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    DispDespCode = DispDespCode + "' OR VW_desfiledata.MTCode = '" + R_Code;
                                }
                            }
                            else
                                if (ViewTestCode == "")
                                {
                                    ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + R_Code;
                                }
                                else
                                    ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + R_Code;
                        }
                    }
                }
            }
            #endregion

            bool DescFlag = false;
            bool textflag = false;
            bool Histoflag = false;
            bool Cytoflag = false;

            if (DispDespCode != "")
            {
                DispDespCode = DispDespCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), DispDespCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                DescFlag = true;
            }
            if (ViewTestCode != "")
            {
                ViewTestCode = ViewTestCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                textflag = true;
            }
            if (Histo != "")
            {
                Histo = Histo + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_HISTO(Convert.ToInt32(Session["Branchid"]), Histo, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Histoflag = true;
            }
            if (Cyto != "")
            {
                Cyto = Cyto + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_CYTO(Convert.ToInt32(Session["Branchid"]), Cyto, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Cytoflag = true;
            }
            string filename = Server.MapPath("rpts");
            string filename1 = Server.MapPath("rpts");

            if (textflag == true && DescFlag == true)
            {
                #region For Descriptive And No Descriptive Both

                CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));

                SqlConnection con = DataAccess.ConInitForDC();
                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);
                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailDrReport/");
                string filename11 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                System.IO.File.WriteAllText(filename11, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename11;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);


                CR.Close();
                CR.Dispose();
                GC.Collect();

                path = "";
                rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                int line1 = 10;
                int topMargin = 14 * line1;
                // objRptTestResultMemo_Email.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);

                SqlConnection con1 = DataAccess.ConInitForDC();
                SqlDataAdapter sda1 = null;
                DataTable dt1 = new DataTable();
                //DataSet1 dst1 = new DataSet1();
                sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR5 = new ReportDocument();
                CR5.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive.rpt"));


                sda1.Fill(dt1);
                CR5.SetDataSource((System.Data.DataTable)dt1);
                string path22 = Server.MapPath("/" + Request.ApplicationPath + "/EmailDrReport/");
                string filename22 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                System.IO.File.WriteAllText(filename22, "");
                string exportedpath5 = "", selectionFormula5 = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument5 = null;
                if (CR5 != null)
                {
                    crReportDocument5 = (ReportDocument)CR5;
                }
                CrystalDecisions.Shared.PageMargins pm1 = CR5.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line15 = 10;
                pm1.topMargin = 200 * line15;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath5 = filename22;
                cl.ExportandPrintr("pdf", path22, exportedpath5, formula, CR5);


                ReportParameterClass.ReportType = "";
                CR5.Close();
                CR5.Dispose();
                GC.Collect();
                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                if (dt1.Rows.Count == 0)
                {
                    string filepath111 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                    FileInfo fi1 = new FileInfo(filepath111);
                    fi1.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                if (aut.P_email != "")
                {
                    #region Send Mail

                    bool flag = true;
                    MailAddress to = new MailAddress(docemail.Trim());
                    if (docemail != "" && docemail != null)
                    {
                        to = new MailAddress(docemail.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);

                    Attachment att = new Attachment(Server.MapPath("~/EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));
                    Attachment att1 = new Attachment(Server.MapPath("~/EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
                    msgmail.Attachments.Add(att);
                    msgmail.Attachments.Add(att1);

                    if (tvGroupGram.Nodes.Count > 0)
                    {
                        if (tvGroupGram.Nodes[0].ChildNodes.Count > 0 && tvGroupGram.Nodes[0].ChildNodes[0].Checked)
                        {

                        }
                    }

                    msgmail.Subject = lblRegNo.Text + "_" + pname;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = "Your report of <BR>" + testname.ToUpper() + "<BR> is ready,Please find it in the attachment.";
                        smtp.Send(msgmail);

                        att.Dispose();
                        att1.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        string filepath111 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                        FileInfo fi1 = new FileInfo(filepath111);
                        fi1.Delete();
                    }
                    else
                    {
                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                    }
                    DeleteFile("EmailDrReport", "Nondescriptive");
                    DeleteFile("EmailDrReport", "Descriptive");
                    DeleteFile("EmailDrReport", "Hemogram");
                    #endregion
                }
                #endregion
            }
            else if (textflag == true)
            {
                #region for Nondescriptive

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);
                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailDrReport/");
                string filename11 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                System.IO.File.WriteAllText(filename11, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                //  int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                // CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename11;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);


                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                if (aut.P_email != "")
                {
                    #region Mail Send
                    bool flag = true;

                    MailAddress to = new MailAddress(docemail.Trim());

                    if (docemail != "" && docemail != null)
                    {
                        to = new MailAddress(docemail.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);

                    Attachment att = new Attachment(Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));
                    if (tvGroupGram.Nodes.Count > 0)
                    {
                        if (tvGroupGram.Nodes[0].ChildNodes.Count > 0 && tvGroupGram.Nodes[0].ChildNodes[0].Checked)
                        {

                        }
                    }
                    msgmail.Attachments.Add(att);
                    msgmail.Subject = lblRegNo.Text + "_" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = "Your report of <BR>" + testname.ToUpper() + "<BR> is ready,Please find it in the attachment.";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                    }
                    else
                    {
                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                    }

                    DeleteFile("EmailDrReport", "Nondescriptive");
                    DeleteFile("EmailDrReport", "Hemogram");
                    #endregion
                }
                #endregion
            }
            else if (Histoflag == true)
            {
                #region Only histo Report

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_HISTO_Email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org_HISTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailDrReport/");
                string filename1H = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "" + "HISTO_Desc" + ".pdf");
                System.IO.File.WriteAllText(filename1H, "");
                string exportedpath = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename1H;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "" + "HISTO_Desc" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                if (aut.P_email != "")
                {
                    #region Mail Send Code

                    bool flag = true;
                    MailAddress to = new MailAddress(docemail.Trim());
                    if (docemail != "" && docemail != null)
                    {
                        to = new MailAddress(docemail.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);

                    Attachment att = new Attachment(Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "" + "HISTO_Desc" + ".pdf"));
                    msgmail.Attachments.Add(att);
                    msgmail.Subject = lblRegNo.Text + "_" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = "Your report of <BR>" + testname.ToUpper() + "<BR> is ready,Please find it in the attachment.";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "" + "HISTO_Desc" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                    }
                    else
                    {
                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                    }

                    DeleteFile("EmailDrReport", "HISTO_Desc");
                    #endregion
                }

                #endregion
            }
            else if (Cytoflag == true)
            {
                #region Only CYTO Report

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_CYTO_EMAIL.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org_CYTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailDrReport/");
                string filename1C = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "" + "CYTO_Desc" + ".pdf");
                System.IO.File.WriteAllText(filename1C, "");
                string exportedpath = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename1C;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "" + "CYTO_Desc" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }

                if (aut.P_email != "")
                {
                    #region Mail Send Code

                    bool flag = true;
                    MailAddress to = new MailAddress(docemail.Trim());
                    if (docemail != "" && docemail != null)
                    {
                        to = new MailAddress(docemail.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);

                    Attachment att = new Attachment(Server.MapPath("~/EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "" + "CYTO_Desc" + ".pdf"));
                    msgmail.Attachments.Add(att);
                    msgmail.Subject = lblRegNo.Text + "_" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = "Your report of <BR>" + testname.ToUpper() + "<BR> is ready,Please find it in the attachment.";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "" + "CYTO_Desc" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                    }
                    else
                    {
                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                    }

                    DeleteFile("EmailDrReport", "CYTO_Desc");
                    #endregion
                }


                #endregion
            }

            else
            {
                #region For

                //CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                //int line = 10;
                //int topMargin = 14 * line;

                //SqlConnection con = DataAccess.ConInitForDC();
                //SqlDataAdapter sda = null;
                //DataTable dt = new DataTable();
                //// DataSet1 dst = new DataSet1();
                //sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                //sda.Fill(dt);
                //string filename22 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");


                //ReportParameterClass.ReportType = "";

                //rep.Close();
                //rep.Dispose();
                //GC.Collect();
                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailDrReport/");
                string filename15 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "" + "Descriptive" + ".pdf");
                System.IO.File.WriteAllText(filename15, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename15;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();


                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                if (aut.P_email != "")
                {
                    #region Mail Send Code

                    bool flag = true;
                    MailAddress to = new MailAddress(docemail.Trim());
                    if (docemail != "" && docemail != null)
                    {
                        to = new MailAddress(docemail.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);

                    Attachment att = new Attachment(Server.MapPath("~/EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
                    msgmail.Attachments.Add(att);
                    msgmail.Subject = lblRegNo.Text + "_" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = "Your report of <BR>" + testname.ToUpper() + "<BR> is ready,Please find it in the attachment.";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailDrReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                    }
                    else
                    {
                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                    }

                    DeleteFile("EmailDrReport", "Descriptive");
                    #endregion
                }
                #endregion
            }
            #endregion
        }

    }

    private bool ValidateEmail()
    {
        string email = txtpatientmail.Text;
        Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        Match match = regex.Match(email);
        if (match.Success)
        {
            txtpatientmail.BackColor = Color.White;
            txtpatientmail.ForeColor = Color.Black;
        }
        else
        {
            txtpatientmail.Focus();
            txtpatientmail.BackColor = Color.Orange;
            txtpatientmail.ForeColor = Color.White;
            return false;

        }
        return true;

    }

    protected void txtpatientmail_TextChanged(object sender, EventArgs e)
    {
        if (txtpatientmail.Text != "")
        {
            if (ValidateEmail() == true)
            {
                Patmst_Bal_C Objpbc = new Patmst_Bal_C();
                Objpbc.Email = txtpatientmail.Text;
                Objpbc.Update_EmailId(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                this.Chkemailtopatient_CheckedChanged(null, null);
            }
        }

    }
    public void AlterView_VE_GetLabNo(string PatRegID)
    {
        int i;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "ALTER VIEW [dbo].[VW_GetLabNo]AS (select  LabNo,PatRegID,MTCode,Branchid,FID from patmstd  where  PatRegID='" + PatRegID + "'  and branchid ='" + Convert.ToInt32(Session["Branchid"]) + "' and  FID ='" + Convert.ToString(Request.QueryString["FID"]) + "' )";
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();

        }
        catch (Exception exx)
        { }
        finally
        {
            try
            {

                conn.Close();
                conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }

        }




    }
    protected void chkcentermailLetter_CheckedChanged(object sender, EventArgs e)
    {
        if (chkcentermailLetter.Checked == true)
        {
            Cshmst_Bal_C cashmain = new Cshmst_Bal_C();
            int PID = -1;
            if (Convert.ToInt32(ViewState["PID"]) != -1)
            {
                PID = Convert.ToInt32(ViewState["PID"]);
            }


            Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
            string mobile = Obj_PBC_C.Phone;
            string docemail = Obj_PBC_C.P_Doctoremail;
           // string docemail = DrMT_Bal_C.GetEmaildrNameTable(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            string email = DrMT_Bal_C.GetEmailCenterNameTable(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
            aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
            if (aut.P_email == "")
            {
                Label6.Text = "Email id not found";
                Label6.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email id not found.');", true);
                return;

            }
            else if (email == "")
            {
                {
                    Label6.Text = "Center E-mail id not found";
                    Label6.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Center E-mail id not found.');", true);
                    return;
                }
            }
            else
            {
                Label6.Visible = false;
            }



            #region sendemail
            string R_Code = "";
            string TextDesc = "";
            string DispTCode = "";
            string DispDespCode = "";
            string ViewTestCode = "";
            string testname = "";

            string micro = "";
            string Histo = "";
            string Cyto = "";


            Label1.Text = Convert.ToString(hdReportno.Value);
            string Sundept = "";
            for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
            {
                for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                {
                    if (tvGroupTree.Nodes[i].ChildNodes[j].Checked)
                    {
                        if (testname == "")
                        {
                            testname = tvGroupTree.Nodes[i].ChildNodes[j].Text;
                        }
                        else
                        {
                            testname += "," + tvGroupTree.Nodes[i].ChildNodes[j].Text;
                        }
                    }
                }
            }
            for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
            {
                if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
                {
                    for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                    {
                        if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
                        {

                            string h = tvGroupTree.Nodes[i].Value;
                            string t = tvGroupTree.Nodes[i].ChildNodes[j].Value;
                            if (h == "AS")
                            {
                                Sundept = "MICROBIOLOGY";
                            }
                            MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(t);
                            R_Code = Obj_MTB_C.MTCode;
                            TextDesc = Obj_MTB_C.TextDesc;
                            DispTCode = Obj_MTB_C.SDCode;
                            PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();
                            Obj_PB_C.UpdateEmailstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"]));

                            if (DispTCode == "CM")
                            {
                                if (micro == "")
                                {
                                    micro = "( MTCode = '" + R_Code;
                                }
                                else
                                {
                                    micro = micro + "( MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "H1")
                            {
                                if (Histo == "")
                                {
                                    Histo = "( VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    // Histo = Histo + "( MTCode = '" + R_Code;
                                    Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "CY")//CY FN
                            {
                                //if (Cyto == "")
                                //{
                                //    Cyto = "( MTCode = '" + R_Code;
                                //}
                                //else
                                //{
                                //    Cyto = Cyto + "( MTCode = '" + R_Code;
                                //}
                                if (Cyto == "")
                                {
                                    Cyto = " (VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (TextDesc == "DescType")//&& DispTCode != "FN" && DispTCode != "H1"
                            {
                                if (DispDespCode == "")
                                {
                                    DispDespCode = " (VW_desfiledata.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    DispDespCode = DispDespCode + "' OR VW_desfiledata.MTCode = '" + R_Code;
                                }
                            }
                            else
                                if (ViewTestCode == "")
                                {
                                    ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + R_Code;
                                }
                                else
                                    ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + R_Code;
                        }
                    }
                }
            }
            bool DescFlag = false;
            bool textflag = false;
            bool microflag = false;
            bool Histoflag = false;
            bool Cytoflag = false;

            if (DispDespCode != "")
            {
                DispDespCode = DispDespCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), DispDespCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                DescFlag = true;
            }
            if (ViewTestCode != "")
            {
                ViewTestCode = ViewTestCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                textflag = true;
            }
            if (Histo != "")
            {
                Histo = Histo + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_HISTO(Convert.ToInt32(Session["Branchid"]), Histo, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Histoflag = true;
            }
            if (Cyto != "")
            {
                Cyto = Cyto + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_CYTO(Convert.ToInt32(Session["Branchid"]), Cyto, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Cytoflag = true;
            }
            string filename = Server.MapPath("rpts");
            string filename1 = Server.MapPath("rpts");
            if (textflag == true && DescFlag == true)
            {
                #region Nondescriptive and Descriptive

                CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));

                SqlConnection con = DataAccess.ConInitForDC();
                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);
                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                string filename11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                System.IO.File.WriteAllText(filename11, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;

                int line = 10;
                pm.topMargin = 200 * line;
                // CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename11;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);


                CR.Close();
                CR.Dispose();
                GC.Collect();

                path = "";
                rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                int line1 = 10;
                int topMargin = 14 * line1;
                // objRptTestResultMemo_Email.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);

                SqlConnection con1 = DataAccess.ConInitForDC();
                SqlDataAdapter sda1 = null;
                DataTable dt1 = new DataTable();
                //DataSet1 dst1 = new DataSet1();
                sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR5 = new ReportDocument();
                CR5.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive.rpt"));


                sda1.Fill(dt1);
                CR5.SetDataSource((System.Data.DataTable)dt1);
                string path22 = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                string filename22 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                System.IO.File.WriteAllText(filename22, "");
                string exportedpath5 = "", selectionFormula5 = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument5 = null;
                if (CR5 != null)
                {
                    crReportDocument5 = (ReportDocument)CR5;
                }
                CrystalDecisions.Shared.PageMargins pm1 = CR5.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line15 = 10;
                pm1.topMargin = 200 * line15;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath5 = filename22;
                cl.ExportandPrintr("pdf", path22, exportedpath5, formula, CR5);


                ReportParameterClass.ReportType = "";
                CR5.Close();
                CR5.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = " Report Not Generated, Please Generate Once Again";
                    return;
                }
                if (dt1.Rows.Count == 0)
                {
                    string filepath111 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                    FileInfo fi1 = new FileInfo(filepath111);
                    fi1.Delete();
                    Label44.Text = " Not Generated, Please Generate Once Again ";
                    return;
                }

                if (aut.P_email != "")
                {
                    bool flag = true;

                    MailAddress to = new MailAddress(email.Trim());

                    if (email != "" && email != null)
                    {
                        to = new MailAddress(email.Trim());
                    }

                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);
                    Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));
                    Attachment att1 = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
                    msgmail.Attachments.Add(att);
                    msgmail.Attachments.Add(att1);



                    msgmail.Subject = "Reg No :" + lblRegNo.Text + "-" + pname;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;

                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You.";
                        smtp.Send(msgmail);

                        att.Dispose();
                        att1.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        string filepath111 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                        FileInfo fi1 = new FileInfo(filepath111);
                        fi1.Delete();
                        Label44.Text = "E-mail send successfully";
                        Label44.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                    }
                    else
                    {

                        Label6.Text = "Error In E-mail Sending";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                        Label6.Visible = true;
                    }
                    DeleteFile("EmailReport", "Nondescriptive");
                    DeleteFile("EmailReport", "Descriptive");
                    DeleteFile("EmailReport", "Hemogram");
                }

                #endregion

            }
            else if (Cytoflag == true)
            {
                #region Only CYTO Report

                string formulaC = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CRC = new ReportDocument();
                CRC.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_CYTO_Email.rpt"));
                SqlConnection conC = DataAccess.ConInitForDC();

                SqlDataAdapter sdaC = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sdaC = new SqlDataAdapter("select * from VW_desfiledata_org_CYTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", conC);

                sdaC.Fill(dt);

                CRC.SetDataSource((System.Data.DataTable)dt);
                string pathC = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");

                string filename11C = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
                System.IO.File.WriteAllText(filename11C, "");
                string exportedpathC = "", selectionFormulaC = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocumentC = null;

                if (CRC != null)
                {
                    crReportDocumentC = (ReportDocument)CRC;
                }
                CrystalDecisions.Shared.PageMargins pmC = CRC.PrintOptions.PageMargins;

                int linec = 10;
                pmC.topMargin = 200 * linec;
                //CR.PrintOptions.ApplyPageMargins(pm);

                exportedpathC = filename11C;
                cl.ExportandPrintr("pdf", pathC, exportedpathC, formulaC, CRC);
                CRC.Close();
                CRC.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11C = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11C);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }

                if (aut.P_email != "")
                {
                    bool flag = true;

                    MailAddress to = new MailAddress(email.Trim());

                    if (email != "" && email != null)
                    {
                        to = new MailAddress(email.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);
                    Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf"));



                    msgmail.Attachments.Add(att);
                    msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                    }
                    else
                    {

                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                    }

                    DeleteFile("EmailReport", "CYTO_Desc");
                    DeleteFile("EmailReport", "Nondescriptive");
                    DeleteFile("EmailReport", "Hemogram");

                #endregion
                }
            }
            else if (Histoflag == true)
            {
                #region Only HisTo Report

                string formulaH = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CRH = new ReportDocument();
                CRH.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_HISTO_Email.rpt"));
                SqlConnection conH = DataAccess.ConInitForDC();

                SqlDataAdapter sdaH = null;
                dt = new DataTable();

                sdaH = new SqlDataAdapter("select * from VW_desfiledata_org_HISTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", conH);

                sdaH.Fill(dt);

                CRH.SetDataSource((System.Data.DataTable)dt);
                string pathH = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");

                string filename11H = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
                System.IO.File.WriteAllText(filename11H, "");
                string exportedpathH = "", selectionFormulaH = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocumentH = null;

                if (CRH != null)
                {
                    crReportDocumentH = (ReportDocument)CRH;
                }
                CrystalDecisions.Shared.PageMargins pmH = CRH.PrintOptions.PageMargins;

                int lineH = 10;
                pmH.topMargin = 200 * lineH;
                //CR.PrintOptions.ApplyPageMargins(pm);

                exportedpathH = filename11H;
                cl.ExportandPrintr("pdf", pathH, exportedpathH, formulaH, CRH);
                CRH.Close();
                CRH.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11H = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11H);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }

                if (aut.P_email != "")
                {
                    bool flag = true;

                    MailAddress to = new MailAddress(email.Trim());

                    if (email != "" && email != null)
                    {
                        to = new MailAddress(email.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);
                    Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf"));


                    msgmail.Attachments.Add(att);
                    msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                    }
                    else
                    {

                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                    }

                    DeleteFile("EmailReport", "HISTO_Desc");
                    DeleteFile("EmailReport", "Nondescriptive");
                    DeleteFile("EmailReport", "Hemogram");

                #endregion
                }
            }
            else if (textflag == true)
            {
                #region Nondescriptive
                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);
                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                string filename11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                System.IO.File.WriteAllText(filename11, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename11;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }

                if (aut.P_email != "")
                {
                    bool flag = true;

                    MailAddress to = new MailAddress(email.Trim());

                    if (email != "" && email != null)
                    {
                        to = new MailAddress(email.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);
                    Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));


                    msgmail.Attachments.Add(att);
                    msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                    }
                    else
                    {

                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                    }

                    DeleteFile("EmailReport", "Nondescriptive");
                    DeleteFile("EmailReport", "Hemogram");
                }
                #endregion
            }
            else
            {
                #region Descriptive

                int line = 10;
                int topMargin = 14 * line;
                //  objRptTestResultMemo_Email.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);

                CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive_Email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();


                //SqlConnection con = DataAccess.ConInitForDC();
                SqlDataAdapter sda = null;
                dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);
                string filename22 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                string filename11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                System.IO.File.WriteAllText(filename11, "");

                ReportParameterClass.ReportType = "";

                rep.Close();
                rep.Dispose();
                GC.Collect();
                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                if (aut.P_email != "")
                {
                    bool flag = true;

                    MailAddress to = new MailAddress(email.Trim());

                    if (email != "" && email != null)
                    {
                        to = new MailAddress(email.Trim());
                    }

                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);

                    MailMessage msgmail = new MailMessage(from, to);
                    Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
                    msgmail.Attachments.Add(att);

                    msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;

                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;


                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You.";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email send successfully.');", true);
                    }
                    else
                    {

                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                    }
                }

                DeleteFile("EmailReport", "Descriptive");
                #endregion
            }
            #endregion

            //}
            //}
        }
    }
    protected void chkcentermailwithotLetter_CheckedChanged(object sender, EventArgs e)
    {
        if (chkcentermailwithotLetter.Checked == true)
        {
            Cshmst_Bal_C cashmain = new Cshmst_Bal_C();
            int PID = -1;
            if (Convert.ToInt32(ViewState["PID"]) != -1)
            {
                PID = Convert.ToInt32(ViewState["PID"]);
            }


            Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
            string mobile = Obj_PBC_C.Phone;
            string docemail = Obj_PBC_C.P_Doctoremail;
            //string docemail = DrMT_Bal_C.GetEmaildrNameTable(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            string email = DrMT_Bal_C.GetEmailCenterNameTable(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));

            createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
            aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
            if (aut.P_email == "")
            {
                Label6.Text = "Email id not found";
                Label6.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email id not found.');", true);
                return;

            }
            else if (email == "")
            {
                {
                    Label6.Text = "Center E-mail id not found";
                    Label6.Visible = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Center E-mail id not found.');", true);
                    return;
                }
            }
            else
            {
                Label6.Visible = false;
            }



            #region sendemail
            string R_Code = "";
            string TextDesc = "";
            string DispTCode = "";
            string DispDespCode = "";
            string ViewTestCode = "";
            string testname = "";

            string micro = "";
            string Histo = "";
            string Cyto = "";


            Label1.Text = Convert.ToString(hdReportno.Value);
            string Sundept = "";
            for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
            {
                for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                {
                    if (tvGroupTree.Nodes[i].ChildNodes[j].Checked)
                    {
                        if (testname == "")
                        {
                            testname = tvGroupTree.Nodes[i].ChildNodes[j].Text;
                        }
                        else
                        {
                            testname += "," + tvGroupTree.Nodes[i].ChildNodes[j].Text;
                        }
                    }
                }
            }
            for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
            {
                if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
                {
                    for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                    {
                        if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
                        {

                            string h = tvGroupTree.Nodes[i].Value;
                            string t = tvGroupTree.Nodes[i].ChildNodes[j].Value;
                            if (h == "AS")
                            {
                                Sundept = "MICROBIOLOGY";
                            }
                            MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(t);
                            R_Code = Obj_MTB_C.MTCode;
                            TextDesc = Obj_MTB_C.TextDesc;
                            DispTCode = Obj_MTB_C.SDCode;
                            PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();
                            Obj_PB_C.UpdateEmailstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"]));

                            if (DispTCode == "CM")
                            {
                                if (micro == "")
                                {
                                    micro = "( MTCode = '" + R_Code;
                                }
                                else
                                {
                                    micro = micro + "( MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "H1")
                            {
                                if (Histo == "")
                                {
                                    Histo = "( VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    // Histo = Histo + "( MTCode = '" + R_Code;
                                    Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "CY")//CY FN
                            {
                                //if (Cyto == "")
                                //{
                                //    Cyto = "( MTCode = '" + R_Code;
                                //}
                                //else
                                //{
                                //    Cyto = Cyto + "( MTCode = '" + R_Code;
                                //}
                                if (Cyto == "")
                                {
                                    Cyto = " (VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (TextDesc == "DescType")//&& DispTCode != "FN" && DispTCode != "H1"
                            {
                                if (DispDespCode == "")
                                {
                                    DispDespCode = " (VW_desfiledata.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    DispDespCode = DispDespCode + "' OR VW_desfiledata.MTCode = '" + R_Code;
                                }
                            }
                            else
                                if (ViewTestCode == "")
                                {
                                    ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + R_Code;
                                }
                                else
                                    ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + R_Code;
                        }
                    }
                }
            }
            bool DescFlag = false;
            bool textflag = false;
            bool microflag = false;
            bool Histoflag = false;
            bool Cytoflag = false;

            if (DispDespCode != "")
            {
                DispDespCode = DispDespCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), DispDespCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                DescFlag = true;
            }
            if (ViewTestCode != "")
            {
                ViewTestCode = ViewTestCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                textflag = true;
            }
            if (Histo != "")
            {
                Histo = Histo + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_HISTO(Convert.ToInt32(Session["Branchid"]), Histo, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Histoflag = true;
            }
            if (Cyto != "")
            {
                Cyto = Cyto + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_CYTO(Convert.ToInt32(Session["Branchid"]), Cyto, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Cytoflag = true;
            }
            string filename = Server.MapPath("rpts");
            string filename1 = Server.MapPath("rpts");
            if (textflag == true && DescFlag == true)
            {
                #region Nondescriptive and Descriptive

                CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt"));

                SqlConnection con = DataAccess.ConInitForDC();
                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);
                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                string filename11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                System.IO.File.WriteAllText(filename11, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                // CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename11;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);


                CR.Close();
                CR.Dispose();
                GC.Collect();

                path = "";
                rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                int line1 = 10;
                int topMargin = 14 * line1;
                // objRptTestResultMemo_Email.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);

                SqlConnection con1 = DataAccess.ConInitForDC();
                SqlDataAdapter sda1 = null;
                DataTable dt1 = new DataTable();
                //DataSet1 dst1 = new DataSet1();
                sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR5 = new ReportDocument();
                CR5.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive.rpt"));


                sda1.Fill(dt1);
                CR5.SetDataSource((System.Data.DataTable)dt1);
                string path22 = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                string filename22 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                System.IO.File.WriteAllText(filename22, "");
                string exportedpath5 = "", selectionFormula5 = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument5 = null;
                if (CR5 != null)
                {
                    crReportDocument5 = (ReportDocument)CR5;
                }
                CrystalDecisions.Shared.PageMargins pm1 = CR5.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line15 = 10;
                pm1.topMargin = 200 * line15;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath5 = filename22;
                cl.ExportandPrintr("pdf", path22, exportedpath5, formula, CR5);


                ReportParameterClass.ReportType = "";
                CR5.Close();
                CR5.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = " Report Not Generated, Please Generate Once Again";
                    return;
                }
                if (dt1.Rows.Count == 0)
                {
                    string filepath111 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                    FileInfo fi1 = new FileInfo(filepath111);
                    fi1.Delete();
                    Label44.Text = " Not Generated, Please Generate Once Again ";
                    return;
                }

                if (aut.P_email != "")
                {
                    bool flag = true;

                    MailAddress to = new MailAddress(email.Trim());

                    if (email != "" && email != null)
                    {
                        to = new MailAddress(email.Trim());
                    }

                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);
                    Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));
                    Attachment att1 = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
                    msgmail.Attachments.Add(att);
                    msgmail.Attachments.Add(att1);



                    msgmail.Subject = "Reg No :" + lblRegNo.Text + "-" + pname;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;

                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You.";
                        smtp.Send(msgmail);

                        att.Dispose();
                        att1.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        string filepath111 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                        FileInfo fi1 = new FileInfo(filepath111);
                        fi1.Delete();
                        Label44.Text = "E-mail send successfully";
                        Label44.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                    }
                    else
                    {

                        Label6.Text = "Error In E-mail Sending";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                        Label6.Visible = true;
                    }
                    DeleteFile("EmailReport", "Nondescriptive");
                    DeleteFile("EmailReport", "Descriptive");
                    DeleteFile("EmailReport", "Hemogram");
                }

                #endregion

            }
            else if (Cytoflag == true)
            {
                #region Only CYTO Report

                string formulaC = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CRC = new ReportDocument();
                CRC.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_CYTO.rpt"));
                SqlConnection conC = DataAccess.ConInitForDC();

                SqlDataAdapter sdaC = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sdaC = new SqlDataAdapter("select * from VW_desfiledata_org_CYTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", conC);

                sdaC.Fill(dt);

                CRC.SetDataSource((System.Data.DataTable)dt);
                string pathC = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");

                string filename11C = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
                System.IO.File.WriteAllText(filename11C, "");
                string exportedpathC = "", selectionFormulaC = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocumentC = null;

                if (CRC != null)
                {
                    crReportDocumentC = (ReportDocument)CRC;
                }
                CrystalDecisions.Shared.PageMargins pmC = CRC.PrintOptions.PageMargins;

                int linec = 10;
                pmC.topMargin = 200 * linec;
                //CR.PrintOptions.ApplyPageMargins(pm);

                exportedpathC = filename11C;
                cl.ExportandPrintr("pdf", pathC, exportedpathC, formulaC, CRC);
                CRC.Close();
                CRC.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11C = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11C);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }

                if (aut.P_email != "")
                {
                    bool flag = true;

                    MailAddress to = new MailAddress(email.Trim());

                    if (email != "" && email != null)
                    {
                        to = new MailAddress(email.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);
                    Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf"));



                    msgmail.Attachments.Add(att);
                    msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                    }
                    else
                    {

                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                    }

                    DeleteFile("EmailReport", "CYTO_Desc");
                    DeleteFile("EmailReport", "Nondescriptive");
                    DeleteFile("EmailReport", "Hemogram");

                #endregion
                }
            }
            else if (Histoflag == true)
            {
                #region Only HisTo Report

                string formulaH = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CRH = new ReportDocument();
                CRH.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_HISTO.rpt"));
                SqlConnection conH = DataAccess.ConInitForDC();

                SqlDataAdapter sdaH = null;
                dt = new DataTable();

                sdaH = new SqlDataAdapter("select * from VW_desfiledata_org_HISTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", conH);

                sdaH.Fill(dt);

                CRH.SetDataSource((System.Data.DataTable)dt);
                string pathH = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");

                string filename11H = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
                System.IO.File.WriteAllText(filename11H, "");
                string exportedpathH = "", selectionFormulaH = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocumentH = null;

                if (CRH != null)
                {
                    crReportDocumentH = (ReportDocument)CRH;
                }
                CrystalDecisions.Shared.PageMargins pmH = CRH.PrintOptions.PageMargins;

                int lineH = 10;
                pmH.topMargin = 200 * lineH;
                //CR.PrintOptions.ApplyPageMargins(pm);

                exportedpathH = filename11H;
                cl.ExportandPrintr("pdf", pathH, exportedpathH, formulaH, CRH);
                CRH.Close();
                CRH.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11H = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11H);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }

                if (aut.P_email != "")
                {
                    bool flag = true;

                    MailAddress to = new MailAddress(email.Trim());

                    if (email != "" && email != null)
                    {
                        to = new MailAddress(email.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);
                    Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf"));


                    msgmail.Attachments.Add(att);
                    msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                    }
                    else
                    {

                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                    }

                    DeleteFile("EmailReport", "HISTO_Desc");
                    DeleteFile("EmailReport", "Nondescriptive");
                    DeleteFile("EmailReport", "Hemogram");

                #endregion
                }
            }
            else if (textflag == true)
            {
                #region Nondescriptive
                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);
                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                string filename11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                System.IO.File.WriteAllText(filename11, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;

                int line = 10;
                pm.topMargin = 200 * line;

                exportedpath = filename11;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }

                if (aut.P_email != "")
                {
                    bool flag = true;

                    MailAddress to = new MailAddress(email.Trim());

                    if (email != "" && email != null)
                    {
                        to = new MailAddress(email.Trim());
                    }
                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);
                    MailMessage msgmail = new MailMessage(from, to);
                    Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));



                    msgmail.Attachments.Add(att);
                    msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;
                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                    }
                    else
                    {

                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                    }

                    DeleteFile("EmailReport", "Nondescriptive");
                    DeleteFile("EmailReport", "Hemogram");
                }
                #endregion
            }
            else
            {
                #region Descriptive

                int line = 10;
                int topMargin = 14 * line;

                CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();


                //SqlConnection con = DataAccess.ConInitForDC();
                SqlDataAdapter sda = null;
                dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);
                string filename22 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                string filename11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                System.IO.File.WriteAllText(filename11, "");

                ReportParameterClass.ReportType = "";

                rep.Close();
                rep.Dispose();
                GC.Collect();
                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                if (aut.P_email != "")
                {
                    bool flag = true;

                    MailAddress to = new MailAddress(email.Trim());

                    if (email != "" && email != null)
                    {
                        to = new MailAddress(email.Trim());
                    }

                    MailAddress from = new MailAddress("<" + aut.P_email + ">", aut.P_displayname);

                    MailMessage msgmail = new MailMessage(from, to);
                    Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
                    msgmail.Attachments.Add(att);

                    msgmail.Subject = "Reg No:" + lblRegNo.Text + "-" + pname;
                    SmtpClient smtp = new SmtpClient();
                    //smtp.Port = 25;
                    smtp.Port = aut.P_Port;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;

                    smtp.Credentials = new System.Net.NetworkCredential(aut.P_email, aut.P_Password);
                    msgmail.Priority = MailPriority.High;


                    try
                    {
                        msgmail.IsBodyHtml = true;
                        msgmail.Body = " Report of <BR>" + testname.ToUpper() + "<BR> is ready,Please see the attachment.Thanking You.";
                        smtp.Send(msgmail);
                        att.Dispose();
                    }
                    catch (Exception)
                    {
                        flag = false;
                    }
                    if (flag == true)
                    {
                        if (aut.P_email == "")
                        {
                            return;
                        }
                        Label6.Text = "E-mail send successfully.";
                        Label6.Visible = true;
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email send successfully.');", true);
                    }
                    else
                    {

                        Label6.Text = "Error In E-mail Sending";
                        Label6.Visible = true;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error In E-mail Sending.');", true);
                    }
                }

                DeleteFile("EmailReport", "Descriptive");
                #endregion
            }
            #endregion

            //}
            //}
        }
    }
    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //string message = "Selected Item: " + DropDownList1.SelectedItem.Text;
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('" + message + "');", true);
        if (DropDownList1.SelectedValue == "1")
        {
            PReportWithBalance();

        }
        else
        {
            mp1.Hide();
        }
    }
    public void PReportWithBalance()
    {

        createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
        aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
        if (aut.P_QRCodeRequired == true)
        {
            GenerateQRCode();
            URLReportGenerate();
        }
        string R_Code = "";
        string TextDesc = "";

        string micro = "";
        string Histo = "";
        string Cyto = "";
        string DispTCode = "";
        string DispDespCode = "";
        string ViewTestCode = "";

        Label1.Text = Convert.ToString(hdReportno.Value);
        string Sundept = "";
        for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
        {
            if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
            {
                for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                {
                    if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
                    {
                        string h = tvGroupTree.Nodes[i].Value;
                        string t = tvGroupTree.Nodes[i].ChildNodes[j].Value;
                        if (h == "AS")
                        {
                            Sundept = "MICROBIOLOGY";
                        }
                        MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(t);
                        R_Code = Obj_MTB_C.MTCode;
                        TextDesc = Obj_MTB_C.TextDesc;
                        DispTCode = Obj_MTB_C.SDCode;
                        PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();

                        dtexistaut = psnew.Check_Authorised_Test(lblRegNo.Text, Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                        if (dtexistaut.Rows[0]["Printedby"] != "")
                        {
                            PrintUserName = Convert.ToString(dtexistaut.Rows[0]["Printedby"]);
                        }
                        else
                        {

                            PrintUserName = Session["username"].ToString();
                        }
                        Obj_PB_C.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(PrintUserName));

                        if (DispTCode == "CM")
                        {
                            if (micro == "")
                            {
                                micro = "( MTCode = '" + R_Code;
                            }
                            else
                            {
                                micro = micro + "( MTCode = '" + R_Code;
                            }
                        }
                        else if (DispTCode == "H1")
                        {
                            if (Histo == "")
                            {
                                Histo = "( VW_desfiledata_HISTO.MTCode = '" + R_Code;
                            }
                            else
                            {
                                // Histo = Histo + "( MTCode = '" + R_Code;
                                Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + R_Code;
                            }
                        }
                        else if (DispTCode == "CY")//CY FN
                        {
                            //if (Cyto == "")
                            //{
                            //    Cyto = "( MTCode = '" + R_Code;
                            //}
                            //else
                            //{
                            //    Cyto = Cyto + "( MTCode = '" + R_Code;
                            //}
                            if (Cyto == "")
                            {
                                Cyto = " (VW_desfiledata_CYTO.MTCode = '" + R_Code;
                            }
                            else
                            {
                                Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + R_Code;
                            }
                        }
                        else if (TextDesc == "DescType")//&& DispTCode != "FN" && DispTCode != "H1"
                        {
                            if (DispDespCode == "")
                            {
                                DispDespCode = " (VW_desfiledata.MTCode = '" + R_Code;
                            }
                            else
                            {
                                DispDespCode = DispDespCode + "' OR VW_desfiledata.MTCode = '" + R_Code;
                            }
                        }
                        else
                            if (ViewTestCode == "")
                            {
                                ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + R_Code;
                            }
                            else
                                ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + R_Code;



                    }
                }
            }
        }
        bool DescFlag = false;
        bool textflag = false;
        bool microflag = false;
        bool Histoflag = false;
        bool Cytoflag = false;
        if (DispDespCode != "")
        {
            DispDespCode = DispDespCode + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), DispDespCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
            DescFlag = true;
        }
        else
        {

        }
        if (ViewTestCode != "")
        {
            ViewTestCode = ViewTestCode + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
            textflag = true;
        }

        
        if (Histo != "")
        {
            Histo = Histo + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_GetAlterView_HISTO(Convert.ToInt32(Session["Branchid"]), Histo, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
            Histoflag = true;
        }
        if (Cyto != "")
        {
            Cyto = Cyto + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_GetAlterView_CYTO(Convert.ToInt32(Session["Branchid"]), Cyto, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
            Cytoflag = true;
        }



        if (textflag == true && DescFlag == true)
        {
            #region for Descriptive and No-Descriptive

            string[] Obj_A_Test = DispDespCode.Split(',');
            string[] targetArr = new string[Obj_A_Test.Length + 1];
            string[] urlArr = new string[Obj_A_Test.Length + 1];
            string[] featuresArr = new string[Obj_A_Test.Length + 1];

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();

            sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;

            int line = 10;
            pm.topMargin = 200 * line;

            exportedpath = filename1;

            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

            CR.Close();
            CR.Dispose();
            GC.Collect();

            path = "";
            rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();


            string formula11 = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR1 = new ReportDocument();
            CR1.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive.rpt"));
            SqlConnection con1 = DataAccess.ConInitForDC();

            int line1 = 10;
            int topMargin = 14 * line1;

            string filename22 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            System.IO.File.WriteAllText(filename22, "");

            SqlDataAdapter sda1 = null;
            DataTable dt1 = new DataTable();
            // DataSet1 dst1 = new DataSet1();
            sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);
            sda1.Fill(dt1);
            CR1.SetDataSource((System.Data.DataTable)dt1);
            ReportParameterClass.ReportType = "";

            ReportDocument crReportDocument1 = null;
            if (CR1 != null)
            {
                crReportDocument1 = (ReportDocument)CR1;
            }
            CrystalDecisions.Shared.PageMargins pm1 = CR1.PrintOptions.PageMargins;

            int line11 = 10;
            pm1.topMargin = 200 * line;

            exportedpath = "";
            exportedpath = filename22;

            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR1);

            CR1.Close();
            CR1.Dispose();
            GC.Collect();

            rep.Close();
            rep.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            if (dt1.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                FileInfo fi1 = new FileInfo(filepath11);
                fi1.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

            string[] FilePathSplitOrg = OrgFile.Split('$');
            string[] FilePathSplitDup = DupFile.Split('$');

            if (FilePathSplitOrg[1] != FilePathSplitDup[1])
            {
                string pathh = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                foreach (string file in Directory.GetFiles(pathh))
                {
                    string[] NewFile = file.Split('$');
                    if (FilePathSplitOrg[1] != NewFile[1])
                    {
                        File.Delete(file);
                    }
                }
            }
            urlArr[Obj_A_Test.Length] = "Testnormalresult.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf";

            targetArr[Obj_A_Test.Length] = "1";
            featuresArr[Obj_A_Test.Length] = "";
            string OrgFileDesc = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            string DupFileDesc = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            string[] FilePathSplitOrgDesc = OrgFileDesc.Split('$');
            string[] FilePathSplitDupDesc = DupFileDesc.Split('$');

            if (FilePathSplitOrgDesc[1] != FilePathSplitDupDesc[1])
            {
                string pathh = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");

                foreach (string file in Directory.GetFiles(pathh))
                {
                    string[] NewFile = file.Split('$');
                    if (FilePathSplitOrg[1] != NewFile[1])
                    {
                        File.Delete(file);
                    }
                }
            }

            urlArr[0] = "Redirect.aspx?rt=DescType&RepName=PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf";

            ResponseHelper.Redirect(urlArr, targetArr, featuresArr);

            #endregion
        }
        else if (textflag == true && DescFlag == false && microflag == false)
        {
            #region Only Nondescriptive

            string formula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportnondescriptive.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
            //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            pm.topMargin = 200 * line;
            //  CR.PrintOptions.ApplyPageMargins(pm);

            exportedpath = filename1;
            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
            CR.Close();
            CR.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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


            //ResponseHelper.Redirect(urlArr, "_blank", "menubar=0,width=100,height=100");
            Response.Redirect("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

            #endregion
        }

        else if (DescFlag == true && microflag == false && textflag == false)
        {


            #region Only Nondescriptive

            string formula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
            // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            pm.topMargin = 200 * line;
            //  CR.PrintOptions.ApplyPageMargins(pm);

            exportedpath = filename1;
            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
            CR.Close();
            CR.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + lblName.Text + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

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
            // Sundept

            Response.Redirect("PrintReport//" + "$" + lblName.Text + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            #endregion
        }
        if (Histoflag == true)
        {
            #region Only CYTO Report

            string formula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_HISTO.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_desfiledata_org_HISTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
            // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            pm.topMargin = 200 * line;
            //  CR.PrintOptions.ApplyPageMargins(pm);

            exportedpath = filename1;
            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
            CR.Close();
            CR.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");

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
            // Sundept

            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");

            #endregion
        }
        if (Cytoflag == true)
        {
            #region Only CYTO Report

            string formula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_CYTO.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_desfiledata_org_CYTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
            // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            pm.topMargin = 200 * line;
            //  CR.PrintOptions.ApplyPageMargins(pm);

            exportedpath = filename1;
            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
            CR.Close();
            CR.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");

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
            // Sundept

            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");

            #endregion
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        mp1.Show();
    }
    protected void cmdPrint_Balance_Click(object sender, EventArgs e)
    {

    }

    protected void btnduepay_Click(object sender, EventArgs e)
    {
        // Response.Redirect("~/Testresultentry.aspx?type=Test", false);
        Response.Redirect("Paybilldesk.aspx?PID=" + Convert.ToInt32(ViewState["PID"]) + "&FID=" + Request.QueryString["FID"], false);
    }
    protected void BtnGenerateURL_Click(object sender, EventArgs e)
    {
        GenerateQRCode();
        Cshmst_Bal_C cashmain = new Cshmst_Bal_C();
        //cashmain.getBalance(Convert.ToString( ViewState["PID"]), Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
        //if (cashmain.Balance > 0)
        //{
        //    Label6.Text = "pending balance";
        //    Label6.Visible = true;
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
        //}
        //else
        //{
            string R_Code = "";
            string TextDesc = "";

            string micro = "";
            string Histo = "";
            string Cyto = "";
            string DispTCode = "";
            string DispDespCode = "";
            string ViewTestCode = "";
            Label1.Text = Convert.ToString(hdReportno.Value);

            for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
            {
                if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
                {
                    for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                    {
                        if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
                        {

                            string TID = tvGroupTree.Nodes[i].ChildNodes[j].Value;

                            MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(TID);
                            R_Code = Obj_MTB_C.MTCode;
                            TextDesc = Obj_MTB_C.TextDesc;
                            DispTCode = Obj_MTB_C.SDCode;
                            PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();
                            dtexistaut = psnew.Check_Authorised_Test(lblRegNo.Text, Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                            if (dtexistaut.Rows[0]["Printedby"] != "")
                            {
                                PrintUserName = Convert.ToString(dtexistaut.Rows[0]["Printedby"]);
                            }
                            else
                            {

                                PrintUserName = Session["username"].ToString();
                            }
                            Obj_PB_C.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(PrintUserName));

                            //if (DispTCode == "CM")
                            //{
                            //    if (micro == "")
                            //    {
                            //        micro = "( MTCode = '" + R_Code;
                            //    }
                            //    else
                            //    {
                            //        micro = micro + "( MTCode = '" + R_Code;
                            //    }
                            //}
                            //else if (TextDesc == "DescType")
                            //{
                            //    if (DispDespCode == "")
                            //    {
                            //        DispDespCode = " (MTCode = '" + R_Code;
                            //    }
                            //    else
                            //    {
                            //        DispDespCode = DispDespCode + "' OR MTCode = '" + R_Code;
                            //    }
                            //}
                            //else
                            //    if (ViewTestCode == "")
                            //    {
                            //        ViewTestCode = " (MTCode = '" + R_Code;
                            //    }
                            //    else
                            //        ViewTestCode = ViewTestCode + "' OR MTCode = '" + R_Code;

                            if (DispTCode == "CM")
                            {
                                if (micro == "")
                                {
                                    micro = "( MTCode = '" + R_Code;
                                }
                                else
                                {
                                    micro = micro + "( MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "H1")
                            {
                                if (Histo == "")
                                {
                                    Histo = "( VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    // Histo = Histo + "( MTCode = '" + R_Code;
                                    Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "CY")//CY FN
                            {
                                //if (Cyto == "")
                                //{
                                //    Cyto = "( MTCode = '" + R_Code;
                                //}
                                //else
                                //{
                                //    Cyto = Cyto + "( MTCode = '" + R_Code;
                                //}
                                if (Cyto == "")
                                {
                                    Cyto = " (VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (TextDesc == "DescType")
                            {
                                if (DispDespCode == "")
                                {
                                    DispDespCode = " (VW_desfiledata.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    DispDespCode = DispDespCode + "' OR VW_desfiledata.MTCode = '" + R_Code;
                                }
                            }
                            else
                                if (ViewTestCode == "")
                                {
                                    ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + R_Code;
                                }
                                else
                                    ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + R_Code;

                            TAT_C at = new TAT_C();

                        }
                    }
                }
            }
            bool DescFlag = false;
            bool textflag = false;
            bool microflag = false;
            bool Histoflag = false;
            bool Cytoflag = false;
            if (DispDespCode != "")
            {
                DispDespCode = DispDespCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), DispDespCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                DescFlag = true;
            }
            else
            {

            }
            if (ViewTestCode != "")
            {
                ViewTestCode = ViewTestCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                textflag = true;
            }

           
            if (Histo != "")
            {
                Histo = Histo + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_HISTO(Convert.ToInt32(Session["Branchid"]), Histo, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Histoflag = true;
            }
            if (Cyto != "")
            {
                Cyto = Cyto + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_CYTO(Convert.ToInt32(Session["Branchid"]), Cyto, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Cytoflag = true;
            }


            txturl.Text = "";

            if (textflag == true)
            {
                #region Only Nondescriptive

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();

                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                //  DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                // string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                //string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
               // string filename1 = Server.MapPath("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                string filename1 = Server.MapPath("UrlReport//" + Request.QueryString["PatRegID"].ToString().Trim() + ".pdf");

                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
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
                   // string filepath11 = Server.MapPath("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                    string filepath11 = Server.MapPath("UrlReport//" + Request.QueryString["PatRegID"].ToString().Trim() + ".pdf");

                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Url Not Generated, Please Generate Once Again !! ";
                    return;
                }
                // string OrgFile = Server.MapPath("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
               // string OrgFile = "UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf";
                string OrgFile = "UrlReport//" + Request.QueryString["PatRegID"].ToString() + ".pdf";

                // string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

                //string[] FilePathSplitOrg = OrgFile.Split('$');
                //string[] FilePathSplitDup = DupFile.Split('$');

                //if (FilePathSplitOrg[1] != FilePathSplitDup[1])
                //{

                //    foreach (string file in Directory.GetFiles(path))
                //    {
                //        string[] NewFile = file.Split('$');
                //        if (FilePathSplitOrg[1] != NewFile[1])
                //        {
                //            File.Delete(file);
                //        }
                //    }
                //}
                //Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                Patmstd_Bal_C PBC = new Patmstd_Bal_C();
                // sendSMSRegistration(PBCL);

                string p_fname = lblName.Text;

                string Branchid = Convert.ToString(Session["Branchid"]);
                string msg = PBC.GetSMSString("URL", Convert.ToInt16(Branchid));
                string CounCode = PBC.GetSMSString_CountryCode("URL", Convert.ToInt16(Branchid));
                if (msg.Trim() != "")
                {
                    if (msg.Contains("#Name#"))
                    {
                        msg = msg.Replace("#Name#", p_fname);
                    }
                    // txturl.Text = msg + "" + OrgFile;

                    string mm = Request.ServerVariables["HTTP_HOST"];
                    if (txturl.Text == "")
                    {
                        txturl.Text = msg + " " + CounCode + "" + OrgFile;
                    }
                    else
                    {
                        txturl.Text = txturl.Text + " " + msg + " " + CounCode + "" + OrgFile;
                    }

                    Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
                string mobile = Obj_PBC_C.Phone;
                string email = Obj_PBC_C.Email;
                string msg1 = "";
                string CounCode1 = ObjPBC.GetSMSString_CountryCode("Registration", Convert.ToInt32(Session["Branchid"]));
                if (CounCode1.Length == 2)
                {
                    if (mobile != CounCode && mobile != "")
                    {

                        createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                        aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        string Labname = aut.P_LabSmsName;
                        string SMSapistring = aut.P_LabSmsString;
                        string Labwebsite = aut.P_LabWebsite;

                       // msg = "Result Of %3a " + pname + " %3bPatient ID%3a " + Request.QueryString["PatRegID"] + " is " + shortform;

                        SMSapistring = SMSapistring.ToString().Replace("#message#", txturl.Text);
                        SMSapistring = SMSapistring.Replace("#Labname#", Labname);
                        SMSapistring = SMSapistring.Replace("#phone#", mobile);
                        try
                        {
                            string url = apicall(SMSapistring);

                        }
                        catch (Exception exx)
                        { }
                    }
                }
                    //Label1.Text = CounCode + "" + OrgFile;
                    // txturl.Text = msg + "" +  "http://"+mm+"  OrgFile;
                    // var url = HttpContext.Current.Response.Redirect("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");//.Request.GetDisplayUrl();
                    //string ddd=  "http://" Request.ServerVariables["HTTP_HOST"] + "/jsfolder/jsfilename.js";
                    // System.Web.HttpContext.Current.RewritePath(OrgFile);
                    string mm2 = Page.ResolveUrl("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                }
                #endregion
            }

            if (DescFlag == true)
            {
                #region Only Descriptive

                //string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                //string filename22 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                //CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //SqlConnection con = DataAccess.ConInitForDC();
                //SqlDataAdapter sda = null;
                //DataTable dt = new DataTable();
                //// DataSet1 dst = new DataSet1();
                //sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);
                //sda.Fill(dt);

                //ReportParameterClass.ReportType = "";

                //rep.Close();
                //rep.Dispose();
                //GC.Collect();

                //if (dt.Rows.Count == 0)
                //{
                //    string filename11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                //    FileInfo fi = new FileInfo(filename11);
                //    fi.Delete();
                //    Label44.Text = "Report Not Generated, Please Generate Once Again !!";
                //    return;
                //}
                //string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                //string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                //string[] FilePathSplitOrg = OrgFile.Split('$');
                //string[] FilePathSplitDup = DupFile.Split('$');

                //if (FilePathSplitOrg[1] != FilePathSplitDup[1])
                //{
                //    foreach (string file in Directory.GetFiles(path))
                //    {
                //        string[] NewFile = file.Split('$');
                //        if (FilePathSplitOrg[1] != NewFile[1])
                //        {
                //            File.Delete(file);
                //        }
                //    }
                //}

                //Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                #region Only Nondescriptive

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
                string filename1 = Server.MapPath("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename1;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Url Not Generated, Please Generate Once Again !!";
                    return;
                }
                // string OrgFile = Server.MapPath("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                string OrgFile = "UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf";

                //string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                //string[] FilePathSplitOrg = OrgFile.Split('$');
                //string[] FilePathSplitDup = DupFile.Split('$');

                //if (FilePathSplitOrg[1] != FilePathSplitDup[1])
                //{
                //    foreach (string file in Directory.GetFiles(path))
                //    {
                //        string[] NewFile = file.Split('$');
                //        if (FilePathSplitOrg[1] != NewFile[1])
                //        {
                //            File.Delete(file);
                //        }
                //    }
                //}
                //// Sundept

                //Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                Patmstd_Bal_C PBC = new Patmstd_Bal_C();
                // sendSMSRegistration(PBCL);

                string p_fname = lblName.Text;

                string Branchid = Convert.ToString(Session["Branchid"]);
                string msg = PBC.GetSMSString("URL", Convert.ToInt16(Branchid));
                string CounCode = PBC.GetSMSString_CountryCode("URL", Convert.ToInt16(Branchid));
                if (msg.Trim() != "")
                {
                    if (msg.Contains("#Name#"))
                    {
                        msg = msg.Replace("#Name#", p_fname);
                    }
                    // txturl.Text = msg + "" + OrgFile;

                    string mm = Request.ServerVariables["HTTP_HOST"];
                    if (txturl.Text != "")
                    {
                        txturl.Text = txturl.Text + " " + msg + " " + CounCode + "" + OrgFile;
                    }
                    else
                    {
                        txturl.Text = msg + " " + CounCode + "" + OrgFile;
                    }

                    Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                    string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
                    string mobile = Obj_PBC_C.Phone;
                    string email = Obj_PBC_C.Email;
                    string msg1 = "";
                    string CounCode1 = ObjPBC.GetSMSString_CountryCode("Registration", Convert.ToInt32(Session["Branchid"]));
                    if (CounCode1.Length == 2)
                    {
                        if (mobile != CounCode && mobile != "")
                        {

                            createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                            aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                            string Labname = aut.P_LabSmsName;
                            string SMSapistring = aut.P_LabSmsString;
                            string Labwebsite = aut.P_LabWebsite;

                            // msg = "Result Of %3a " + pname + " %3bPatient ID%3a " + Request.QueryString["PatRegID"] + " is " + shortform;

                            SMSapistring = SMSapistring.ToString().Replace("#message#", txturl.Text);
                            SMSapistring = SMSapistring.Replace("#Labname#", Labname);
                            SMSapistring = SMSapistring.Replace("#phone#", mobile);
                            try
                            {
                                string url = apicall(SMSapistring);

                            }
                            catch (Exception exx)
                            { }
                        }
                    }
                    // Label1.Text = CounCode + "" + OrgFile;
                    // txturl.Text = msg + "" +  "http://"+mm+"  OrgFile;
                    // var url = HttpContext.Current.Response.Redirect("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");//.Request.GetDisplayUrl();
                    //string ddd=  "http://" Request.ServerVariables["HTTP_HOST"] + "/jsfolder/jsfilename.js";
                    // System.Web.HttpContext.Current.RewritePath(OrgFile);
                    string mm2 = Page.ResolveUrl("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                #endregion
                #endregion
                }


            }
       // }
    }
    protected void btnWhatapp_Click(object sender, ImageClickEventArgs e)
    {
        Cshmst_Bal_C Cshmst = new Cshmst_Bal_C();
        int PID = -1;
        if (Convert.ToInt32(ViewState["PID"]) != -1)
        {
            PID = Convert.ToInt32(ViewState["PID"]);
        }
        Cshmst.getBalance(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), PID);
        float BAL_Amount = Cshmst.Balance;
        if (Convert.ToString(ViewState["VALIDATE"]) == "YES")
        {
            BAL_Amount = 0;
        }
        if (BAL_Amount > 0)
        {
            Label6.Text = "Pending balance.";
            Label6.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
        }
        else
        {
            createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
            aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
            if (aut.P_QRCodeRequired == true)
            {
                GenerateQRCode();
                URLReportGenerate();
            }
            string R_Code = "";
            string TextDesc = "";

            string micro = "";
            string Histo = "";
            string Cyto = "";
            string DispTCode = "";
            string DispDespCode = "";
            string ViewTestCode = "";

            Label1.Text = Convert.ToString(hdReportno.Value);
            string Sundept = "";
            for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
            {
                if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
                {
                    for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                    {
                        if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
                        {
                            string h = tvGroupTree.Nodes[i].Value;
                            string IDT = tvGroupTree.Nodes[i].ChildNodes[j].Value;
                            if (h == "AS")
                            {
                                Sundept = "MICROBIOLOGY";
                            }
                            MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(IDT);
                            R_Code = Obj_MTB_C.MTCode;
                            TextDesc = Obj_MTB_C.TextDesc;
                            DispTCode = Obj_MTB_C.SDCode;
                            PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();

                            dtexistaut = psnew.Check_Authorised_Test(lblRegNo.Text, Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                            if (dtexistaut.Rows[0]["Printedby"] != "")
                            {
                                PrintUserName = Convert.ToString(dtexistaut.Rows[0]["Printedby"]);
                            }
                            else
                            {

                                PrintUserName = Session["username"].ToString();
                            }
                           // Obj_PB_C.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(PrintUserName));
                            Obj_PB_C.UpdatePrintstatus_What_app(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(PrintUserName));

                            if (DispTCode == "CM")
                            {
                                if (micro == "")
                                {
                                    micro = "( MTCode = '" + R_Code;
                                }
                                else
                                {
                                    micro = micro + "( MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "H1")
                            {
                                if (Histo == "")
                                {
                                    Histo = "( VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    // Histo = Histo + "( MTCode = '" + R_Code;
                                    Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "CY")//CY FN
                            {
                                //if (Cyto == "")
                                //{
                                //    Cyto = "( MTCode = '" + R_Code;
                                //}
                                //else
                                //{
                                //    Cyto = Cyto + "( MTCode = '" + R_Code;
                                //}
                                if (Cyto == "")
                                {
                                    Cyto = " (VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (TextDesc == "DescType")//&& DispTCode != "FN" && DispTCode != "H1"
                            {
                                if (DispDespCode == "")
                                {
                                    DispDespCode = " (VW_desfiledata.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    DispDespCode = DispDespCode + "' OR VW_desfiledata.MTCode = '" + R_Code;
                                }
                            }
                            else
                                if (ViewTestCode == "")
                                {
                                    ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + R_Code;
                                }
                                else
                                    ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + R_Code;



                        }
                    }
                }
            }
            bool DescFlag = false;
            bool textflag = false;
            bool microflag = false;
            bool Histoflag = false;
            bool Cytoflag = false;
            if (DispDespCode != "")
            {
                DispDespCode = DispDespCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), DispDespCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                DescFlag = true;
            }
            else
            {

            }
            if (ViewTestCode != "")
            {
                ViewTestCode = ViewTestCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                textflag = true;
            }

           
            if (Histo != "")
            {
                Histo = Histo + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_HISTO(Convert.ToInt32(Session["Branchid"]), Histo, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Histoflag = true;
            }
            if (Cyto != "")
            {
                Cyto = Cyto + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_CYTO(Convert.ToInt32(Session["Branchid"]), Cyto, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Cytoflag = true;
            }



            if (textflag == true && DescFlag == true)
            {
                #region for Descriptive and No-Descriptive

                string[] Obj_A_Test = DispDespCode.Split(',');
                string[] targetArr = new string[Obj_A_Test.Length + 1];
                string[] urlArr = new string[Obj_A_Test.Length + 1];
                string[] featuresArr = new string[Obj_A_Test.Length + 1];

                CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                string formula = "", formula1 = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();

                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString().Trim() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
                string filename1 = Server.MapPath("UrlReport//" + "_" + lblName.Text + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString().Trim() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;

                int line = 10;
                pm.topMargin = 200 * line;

                exportedpath = filename1;

                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

                CR.Close();
                CR.Dispose();
                GC.Collect();

                path = "";
                rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();


                string formula11 = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR1 = new ReportDocument();
                CR1.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_Email.rpt"));
                SqlConnection con1 = DataAccess.ConInitForDC();

                int line1 = 10;
                int topMargin = 14 * line1;

                string filename22 = Server.MapPath("UrlReport//" + "_" + lblName.Text + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf");
                System.IO.File.WriteAllText(filename22, "");

                SqlDataAdapter sda1 = null;
                DataTable dt1 = new DataTable();
                // DataSet1 dst1 = new DataSet1();
                sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString().Trim() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);
                sda1.Fill(dt1);
                CR1.SetDataSource((System.Data.DataTable)dt1);
                ReportParameterClass.ReportType = "";

                ReportDocument crReportDocument1 = null;
                if (CR1 != null)
                {
                    crReportDocument1 = (ReportDocument)CR1;
                }
                CrystalDecisions.Shared.PageMargins pm1 = CR1.PrintOptions.PageMargins;

                int line11 = 10;
                pm1.topMargin = 200 * line;

                exportedpath = "";
                exportedpath = filename22;

                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR1);

                CR1.Close();
                CR1.Dispose();
                GC.Collect();

                rep.Close();
                rep.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("UrlReport//" + "_" + lblName.Text + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                if (dt1.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("UrlReport//" + "_" + lblName.Text + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf");

                    FileInfo fi1 = new FileInfo(filepath11);
                    fi1.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }


                Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
                string mobile = Obj_PBC_C.telNo;
                string email = Obj_PBC_C.Email;
                string msg1 = "";
                //  createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                string WhatAppUrl = aut.P_WhatAppUrl;
                string WhatApp_Api = aut.P_WhatApp_Api;

                WhatAppReport(mobile, filename1, WhatAppUrl, WhatApp_Api);
                WhatAppReport(mobile, filename22, WhatAppUrl, WhatApp_Api);


                #endregion
            }
            else if (textflag == true && DescFlag == false && microflag == false)
            {
                #region Only Nondescriptive

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportnondescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString().Trim() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
                string filename1 = Server.MapPath("UrlReport//" + "_" + lblName.Text + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString().Trim() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename1;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("UrlReport//" + "_" + lblName.Text + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
                string mobile = Obj_PBC_C.telNo;
                string email = Obj_PBC_C.Email;
                string msg1 = "";
                //  createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                string WhatAppUrl = aut.P_WhatAppUrl;
                string WhatApp_Api = aut.P_WhatApp_Api;
                WhatAppReport(mobile, filename1, WhatAppUrl, WhatApp_Api);

                #endregion
            }

            else if (DescFlag == true && microflag == false && textflag == false)
            {


                #region Only descriptive

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_Email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString().Trim() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
                string filename1 = Server.MapPath("UrlReport//" + "_" + lblName.Text + Date1 + "_" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString().Trim() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename1;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();


                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("UrlReport//" + "_" + lblName.Text + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
                string mobile = Obj_PBC_C.telNo;
                string email = Obj_PBC_C.Email;
                string msg1 = "";
                //   createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                string WhatAppUrl = aut.P_WhatAppUrl;
                string WhatApp_Api = aut.P_WhatApp_Api;
                WhatAppReport(mobile, filename1, WhatAppUrl, WhatApp_Api);

                #endregion
            }

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

        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
        //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //var response = wb.UploadValues("http://send.wabapi.com/postfile.php", "POST", data);
       // var response = wb.UploadValues("http://node4.wabapi.com/v4/postfile.php", "POST", data);
        var response = wb.UploadValues(WhatApp_Api, "POST", data);
        string responseInString = Encoding.UTF8.GetString(response);

        Label44.Text = "Report Send Successfully..!";
        }
        catch(Exception ex)
        {
             Label44.Text = "Report Not Send Successfully..!";
        }
    }
    protected void btnRefdrWhatapp_Click(object sender, ImageClickEventArgs e)
    {
        createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
        aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
        if (aut.P_QRCodeRequired == true)
        {
            GenerateQRCode();
        }
         string R_Code = "";
        string TextDesc = "";

        string micro = "";
        string Histo = "";
        string Cyto = "";
        string DispTCode = "";
        string DispDespCode = "";
        string ViewTestCode = "";

        Label1.Text = Convert.ToString(hdReportno.Value);
        string Sundept = "";
        for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
        {
            if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
            {
                for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                {
                    if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
                    {
                        string h = tvGroupTree.Nodes[i].Value;
                        string IDT = tvGroupTree.Nodes[i].ChildNodes[j].Value;
                        if (h == "AS")
                        {
                            Sundept = "MICROBIOLOGY";
                        }
                        MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(IDT);
                        R_Code = Obj_MTB_C.MTCode;
                        TextDesc = Obj_MTB_C.TextDesc;
                        DispTCode = Obj_MTB_C.SDCode;
                        PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();

                        dtexistaut = psnew.Check_Authorised_Test(lblRegNo.Text, Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                        if (dtexistaut.Rows[0]["Printedby"] != "")
                        {
                            PrintUserName = Convert.ToString(dtexistaut.Rows[0]["Printedby"]);
                        }
                        else
                        {

                            PrintUserName = Session["username"].ToString();
                        }
                        Obj_PB_C.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(PrintUserName));

                        if (DispTCode == "CM")
                        {
                            if (micro == "")
                            {
                                micro = "( MTCode = '" + R_Code;
                            }
                            else
                            {
                                micro = micro + "( MTCode = '" + R_Code;
                            }
                        }
                        else if (DispTCode == "H1")
                        {
                            if (Histo == "")
                            {
                                Histo = "( VW_desfiledata_HISTO.MTCode = '" + R_Code;
                            }
                            else
                            {
                                // Histo = Histo + "( MTCode = '" + R_Code;
                                Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + R_Code;
                            }
                        }
                        else if (DispTCode == "CY")//CY FN
                        {
                            //if (Cyto == "")
                            //{
                            //    Cyto = "( MTCode = '" + R_Code;
                            //}
                            //else
                            //{
                            //    Cyto = Cyto + "( MTCode = '" + R_Code;
                            //}
                            if (Cyto == "")
                            {
                                Cyto = " (VW_desfiledata_CYTO.MTCode = '" + R_Code;
                            }
                            else
                            {
                                Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + R_Code;
                            }
                        }
                        else if (TextDesc == "DescType")//&& DispTCode != "FN" && DispTCode != "H1"
                        {
                            if (DispDespCode == "")
                            {
                                DispDespCode = " (VW_desfiledata.MTCode = '" + R_Code;
                            }
                            else
                            {
                                DispDespCode = DispDespCode + "' OR VW_desfiledata.MTCode = '" + R_Code;
                            }
                        }
                        else
                            if (ViewTestCode == "")
                            {
                                ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + R_Code;
                            }
                            else
                                ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + R_Code;



                    }
                }
            }
        }
        bool DescFlag = false;
        bool textflag = false;
        bool microflag = false;
        bool Histoflag = false;
        bool Cytoflag = false;
        if (DispDespCode != "")
        {
            DispDespCode = DispDespCode + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), DispDespCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
            DescFlag = true;
        }
        else
        {

        }
        if (ViewTestCode != "")
        {
            ViewTestCode = ViewTestCode + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
            textflag = true;
        }

        if (micro != "")
        {
            micro = micro + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_AlterViewMicro(Convert.ToInt32(Session["Branchid"]), micro, Request.QueryString["PatRegID"], Request.QueryString["FID"]);
            microflag = true;
        }
        if (Histo != "")
        {
            Histo = Histo + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_GetAlterView_HISTO(Convert.ToInt32(Session["Branchid"]), Histo, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
            Histoflag = true;
        }
        if (Cyto != "")
        {
            Cyto = Cyto + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_GetAlterView_CYTO(Convert.ToInt32(Session["Branchid"]), Cyto, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
            Cytoflag = true;
        }



        if (textflag == true && DescFlag == true)
        {
            #region for Descriptive and No-Descriptive

            string[] Obj_A_Test = DispDespCode.Split(',');
            string[] targetArr = new string[Obj_A_Test.Length + 1];
            string[] urlArr = new string[Obj_A_Test.Length + 1];
            string[] featuresArr = new string[Obj_A_Test.Length + 1];

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();

            sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString().Trim() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
            string filename1 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString().Trim() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;

            int line = 10;
            pm.topMargin = 200 * line;

            exportedpath = filename1;

            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

            CR.Close();
            CR.Dispose();
            GC.Collect();

            path = "";
            rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();


            string formula11 = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR1 = new ReportDocument();
            CR1.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_Email.rpt"));
            SqlConnection con1 = DataAccess.ConInitForDC();

            int line1 = 10;
            int topMargin = 14 * line1;

            string filename22 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf");
            System.IO.File.WriteAllText(filename22, "");

            SqlDataAdapter sda1 = null;
            DataTable dt1 = new DataTable();
            // DataSet1 dst1 = new DataSet1();
            sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString().Trim() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);
            sda1.Fill(dt1);
            CR1.SetDataSource((System.Data.DataTable)dt1);
            ReportParameterClass.ReportType = "";

            ReportDocument crReportDocument1 = null;
            if (CR1 != null)
            {
                crReportDocument1 = (ReportDocument)CR1;
            }
            CrystalDecisions.Shared.PageMargins pm1 = CR1.PrintOptions.PageMargins;

            int line11 = 10;
            pm1.topMargin = 200 * line;

            exportedpath = "";
            exportedpath = filename22;

            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR1);

            CR1.Close();
            CR1.Dispose();
            GC.Collect();

            rep.Close();
            rep.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            if (dt1.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf");

                FileInfo fi1 = new FileInfo(filepath11);
                fi1.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }


            Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
            string mobile = Obj_PBC_C.P_RefDoctorPhoneno;
            string email = Obj_PBC_C.Email;
            string msg1 = "";
           // createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
            aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
            string WhatAppUrl = aut.P_WhatAppUrl;
            string WhatApp_Api = aut.P_WhatApp_Api;
            WhatAppReport(mobile, filename1, WhatAppUrl, WhatApp_Api);
            WhatAppReport(mobile, filename22, WhatAppUrl, WhatApp_Api);


            #endregion
        }
        else if (textflag == true && DescFlag == false && microflag == false)
        {
            #region Only Nondescriptive

            string formula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportnondescriptive_email.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString().Trim() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
            string filename1 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString().Trim() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
            //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            pm.topMargin = 200 * line;
            //  CR.PrintOptions.ApplyPageMargins(pm);

            exportedpath = filename1;
            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
            CR.Close();
            CR.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
            string mobile = Obj_PBC_C.P_RefDoctorPhoneno;
            string email = Obj_PBC_C.Email;
            string msg1 = "";
           // createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
            aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
            string WhatAppUrl = aut.P_WhatAppUrl;
            string WhatApp_Api = aut.P_WhatApp_Api;
            WhatAppReport(mobile, filename1, WhatAppUrl, WhatApp_Api);

            #endregion
        }

        else if (DescFlag == true && microflag == false && textflag == false)
        {


            #region Only descriptive

            string formula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_Email.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString().Trim() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
            string filename1 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString().Trim() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
            // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            pm.topMargin = 200 * line;
            //  CR.PrintOptions.ApplyPageMargins(pm);

            exportedpath = filename1;
            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
            CR.Close();
            CR.Dispose();
            GC.Collect();


            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
            string mobile = Obj_PBC_C.P_RefDoctorPhoneno;
            string email = Obj_PBC_C.Email;
            string msg1 = "";
           // createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
            aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
            string WhatAppUrl = aut.P_WhatAppUrl;
            string WhatApp_Api = aut.P_WhatApp_Api;
            WhatAppReport(mobile, filename1, WhatAppUrl, WhatApp_Api);

            #endregion
        }

    }

    public void GenerateQRCode1()
    {
        string Code = "";

        Patmstd_Bal_C PBC = new Patmstd_Bal_C();
        //==========================================
        // BarCode qrcode1 = new BarCode();
        // qrcode1.Symbology = KeepAutomation.Barcode.Symbology.QRCode;

        //===========================================
        // sendSMSRegistration(PBCL);
        string CounCode = "";
        string p_fname = lblName.Text;
        DataTable dtQ = new DataTable();
        string Branchid = Convert.ToString(Session["Branchid"]);
        string msg = PBC.GetSMSString("URL", Convert.ToInt16(Branchid));
        //string CounCode = PBC.GetSMSString_CountryCode("URL", Convert.ToInt16(Branchid));
        dtQ = PBC.GetSMSString_CountryCode_Covid("URL", Convert.ToInt16(Branchid));
        // CounCode = CounCode + "UrlReport//_"+Request.QueryString["PatRegID"].Trim()+".pdf";
        CounCode = dtQ.Rows[0]["CountryCode"] + "UrlReport//" + dtQ.Rows[0]["smsString"] + Request.QueryString["PatRegID"].Trim() + ".pdf";
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeGenerator.QRCode qrcode = qrGenerator.CreateQrCode(CounCode, QRCodeGenerator.ECCLevel.Q);
        //Image ImgQrCode = new Image();
        System.Web.UI.WebControls.Image ImgQrCode = new System.Web.UI.WebControls.Image();
        ImgQrCode.Height = 150;
        ImgQrCode.Width = 150;
        Byte[] imgByte5 = null;
        byte[] byteImage;
        using (Bitmap bitmap = qrcode.GetGraphic(20))
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                // byte[] byteImage = ms.ToArray();
                byteImage = ms.ToArray();
                ImgQrCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }

            //PHQRCode.Controls.Add(ImgQrCode);
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand("" +
            "Update Patmst " +
            "Set QRImage=@SignImage " +
            " Where PatRegID=@id ", conn);
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

            sc.Parameters.AddWithValue("@id", Request.QueryString["PatRegID"]);
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
    public void GenerateQRCode()
    {
        Patmstd_Bal_C PBC = new Patmstd_Bal_C();
        string CounCode = "";
        string p_fname = lblName.Text;
        DataTable dtQ = new DataTable();
        string Branchid = Convert.ToString(Session["Branchid"]);
        string msg = PBC.GetSMSString("URL", Convert.ToInt16(Branchid));
        //string CounCode = PBC.GetSMSString_CountryCode("URL", Convert.ToInt16(Branchid));
        dtQ = PBC.GetSMSString_CountryCode_Covid("URL", Convert.ToInt16(Branchid));
        // CounCode = CounCode + "UrlReport//_"+Request.QueryString["PatRegID"].Trim()+".pdf";
        CounCode = dtQ.Rows[0]["CountryCode"] + "UrlReport//" + dtQ.Rows[0]["smsString"] + Request.QueryString["PatRegID"].Trim() + ".pdf";

        string Code = CounCode;
        QrCodeEncodingOptions Option = new QrCodeEncodingOptions();
        var qrWrite = new BarcodeWriter(); ;
        qrWrite.Format = BarcodeFormat.QR_CODE;
        qrWrite.Options = new EncodingOptions() { Height = 100, Width = 100, Margin = 0 };

        var result = new Bitmap(qrWrite.Write(Code));
        byte[] byteImage;
        using (Bitmap bitMap = new Bitmap(qrWrite.Write(Code)))
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
        "Update Patmst " +
        "Set QRImage=@SignImage " +
        " Where PatRegID=@id ", conn);
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

        sc.Parameters.AddWithValue("@id", Request.QueryString["PatRegID"]);
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
        //return byteImage;

    }


    public void URLReportGenerate()
    {
        Cshmst_Bal_C cashmain = new Cshmst_Bal_C();
       // cashmain.getBalance(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
       
        //cashmain.getBalance(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["PID"]));
        //if (cashmain.Balance > 0)
        //{
        //    Label6.Text = "pending balance";
        //    Label6.Visible = true;
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
        //}
        //else
        //{
            string R_Code = "";
            string TextDesc = "";

            string micro = "";
            string Histo = "";
            string Cyto = "";
            string DispTCode = "";
            string DispDespCode = "";
            string ViewTestCode = "";
            Label1.Text = Convert.ToString(hdReportno.Value);

            for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
            {
                if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
                {
                    for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
                    {
                        if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
                        {

                            string CodeID = tvGroupTree.Nodes[i].ChildNodes[j].Value;

                            MainTest_Bal_C Obj_MTB_C = new MainTest_Bal_C(CodeID);
                            R_Code = Obj_MTB_C.MTCode;
                            TextDesc = Obj_MTB_C.TextDesc;
                            DispTCode = Obj_MTB_C.SDCode;
                            PatSt_Bal_C Obj_PB_C = new PatSt_Bal_C();
                            dtexistaut = psnew.Check_Authorised_Test(lblRegNo.Text, Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                            if (dtexistaut.Rows[0]["Printedby"] != "")
                            {
                                PrintUserName = Convert.ToString(dtexistaut.Rows[0]["Printedby"]);
                            }
                            else
                            {

                                PrintUserName = Session["username"].ToString();
                            }
                            Obj_PB_C.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(PrintUserName));

                            //if (DispTCode == "CM")
                            //{
                            //    if (micro == "")
                            //    {
                            //        micro = "( MTCode = '" + R_Code;
                            //    }
                            //    else
                            //    {
                            //        micro = micro + "( MTCode = '" + R_Code;
                            //    }
                            //}
                            //else if (TextDesc == "DescType")
                            //{
                            //    if (DispDespCode == "")
                            //    {
                            //        DispDespCode = " (MTCode = '" + R_Code;
                            //    }
                            //    else
                            //    {
                            //        DispDespCode = DispDespCode + "' OR MTCode = '" + R_Code;
                            //    }
                            //}
                            //else
                            //    if (ViewTestCode == "")
                            //    {
                            //        ViewTestCode = " (MTCode = '" + R_Code;
                            //    }
                            //    else
                            //        ViewTestCode = ViewTestCode + "' OR MTCode = '" + R_Code;

                            if (DispTCode == "CM")
                            {
                                if (micro == "")
                                {
                                    micro = "( MTCode = '" + R_Code;
                                }
                                else
                                {
                                    micro = micro + "( MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "H1")
                            {
                                if (Histo == "")
                                {
                                    Histo = "( VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    // Histo = Histo + "( MTCode = '" + R_Code;
                                    Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (DispTCode == "CY")//CY FN
                            {
                                //if (Cyto == "")
                                //{
                                //    Cyto = "( MTCode = '" + R_Code;
                                //}
                                //else
                                //{
                                //    Cyto = Cyto + "( MTCode = '" + R_Code;
                                //}
                                if (Cyto == "")
                                {
                                    Cyto = " (VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + R_Code;
                                }
                            }
                            else if (TextDesc == "DescType")
                            {
                                if (DispDespCode == "")
                                {
                                    DispDespCode = " (VW_desfiledata.MTCode = '" + R_Code;
                                }
                                else
                                {
                                    DispDespCode = DispDespCode + "' OR VW_desfiledata.MTCode = '" + R_Code;
                                }
                            }
                            else
                                if (ViewTestCode == "")
                                {
                                    ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + R_Code;
                                }
                                else
                                    ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + R_Code;

                            TAT_C at = new TAT_C();

                        }
                    }
                }
            }
            bool DescFlag = false;
            bool textflag = false;
            bool microflag = false;
            bool Histoflag = false;
            bool Cytoflag = false;
            if (DispDespCode != "")
            {
                DispDespCode = DispDespCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), DispDespCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                DescFlag = true;
            }
            else
            {

            }
            if (ViewTestCode != "")
            {
                ViewTestCode = ViewTestCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                textflag = true;
            }

            
            if (Histo != "")
            {
                Histo = Histo + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_HISTO(Convert.ToInt32(Session["Branchid"]), Histo, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_HISTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_HISTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Histoflag = true;
            }
            if (Cyto != "")
            {
                Cyto = Cyto + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView_CYTO(Convert.ToInt32(Session["Branchid"]), Cyto, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org_CYTO.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org_CYTO.FID}='" + Request.QueryString["FID"].ToString() + "'";
                Cytoflag = true;
            }


            txturl.Text = "";

            if (textflag == true)
            {
                #region Only Nondescriptive

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();

                CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                //  DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                // string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
                //string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
                string filename1 = Server.MapPath("UrlReport//" + Request.QueryString["PatRegID"].ToString().Trim() + ".pdf");

                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
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
                    string filepath11 = Server.MapPath("UrlReport//" + Request.QueryString["PatRegID"].ToString().Trim() + ".pdf");
                    // string filepath11 = Server.MapPath("UrlReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Url Not Generated, Please Generate Once Again !! ";
                    return;
                }
                // string OrgFile = Server.MapPath("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                string OrgFile = "UrlReport//" + Request.QueryString["PatRegID"].ToString() + ".pdf";

                // string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

                //string[] FilePathSplitOrg = OrgFile.Split('$');
                //string[] FilePathSplitDup = DupFile.Split('$');

                //if (FilePathSplitOrg[1] != FilePathSplitDup[1])
                //{

                //    foreach (string file in Directory.GetFiles(path))
                //    {
                //        string[] NewFile = file.Split('$');
                //        if (FilePathSplitOrg[1] != NewFile[1])
                //        {
                //            File.Delete(file);
                //        }
                //    }
                //}
                //Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                Patmstd_Bal_C PBC = new Patmstd_Bal_C();
                // sendSMSRegistration(PBCL);

                Patmst_Bal_C Obj_PBC_C = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                string pname = Obj_PBC_C.Initial.Trim() + " " + Obj_PBC_C.Patname;
                string mobile = Obj_PBC_C.telNo;
                string email = Obj_PBC_C.Email;
                createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                string Labname = aut.P_LabSmsName;
                string SMSapistring = aut.P_LabSmsString;
                string Labwebsite = aut.P_LabWebsite;
                string WhatAppUrl = aut.P_WhatAppUrl;
                // WhatAppReport(mobile, filename1, WhatAppUrl);


                #endregion
                // }

            }
            if (DescFlag == true)
            {
                #region Only Descriptive


                //Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                #region Only Nondescriptive

                string formula = "";
                selectonFormula = ReportParameterClass.SelectionFormula;
                ReportDocument CR = new ReportDocument();
                CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_email.rpt"));
                SqlConnection con = DataAccess.ConInitForDC();

                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);

                CR.SetDataSource((System.Data.DataTable)dt);
                string path = Server.MapPath("/" + Request.ApplicationPath + "/UrlReport/");
                string filename1 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf");
                System.IO.File.WriteAllText(filename1, "");
                string exportedpath = "", selectionFormula = "";
                ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString().Trim() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
                ReportDocument crReportDocument = null;
                if (CR != null)
                {
                    crReportDocument = (ReportDocument)CR;
                }
                CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                pm.topMargin = 200 * line;
                //  CR.PrintOptions.ApplyPageMargins(pm);

                exportedpath = filename1;
                cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                CR.Close();
                CR.Dispose();
                GC.Collect();

                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString().Trim() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Url Not Generated, Please Generate Once Again !!";
                    return;
                }
                // string OrgFile = Server.MapPath("UrlReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                string OrgFile = "UrlReport//" + "_" + Date1 + "_" + Request.QueryString["PatRegID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Report1" + ".pdf";


                #endregion
                #endregion
                //}


            }
        //}
    }

    protected void CVDesc_Init(object sender, EventArgs e)
    {

    }
    protected void CVDesc_PreRender(object sender, EventArgs e)
    {

    }
}