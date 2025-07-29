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


public partial class TestReportprinting :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    int g;
    string rptname = "", path = "", selectonFormula = "";
    Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();
    Patmst_New_Bal_C PatNBC = new Patmst_New_Bal_C();
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string Date2 = DateTime.Now.AddDays(-1).ToString("ddMMyyyy");
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

    protected void cmdPreview_Click(object sender, EventArgs e)
    {
        try
        {
            string Obj_TestCode = "";
            string TextDesc = "";
            string Obj_DepCode = "";
            string Obj_DescCode = "";
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
                           
                            string tid = tvGroupTree.Nodes[i].ChildNodes[j].Value;
                            MainTest_Bal_C Obj_MTB = new MainTest_Bal_C(tid, Convert.ToInt32(Session["Branchid"]));
                            Obj_TestCode = Obj_MTB.MTCode;
                            TextDesc = Obj_MTB.TextDesc;
                            Obj_DepCode = Obj_MTB.SDCode;

                            if (TextDesc == "DescType")
                            {
                                if (Obj_DescCode == "")
                                {
                                    Obj_DescCode = Obj_TestCode;
                                }
                                else
                                {
                                    Obj_DescCode = Obj_DescCode + "," + Obj_TestCode;
                                }
                            }
                            else
                            {
                                if (ViewTestCode == "")
                                {

                                    ViewTestCode = " (MTCode = '" + Obj_TestCode;
                                }
                                else
                                    ViewTestCode = ViewTestCode + "' OR MTCode = '" + Obj_TestCode;
                            }

                        }

                    }


                }
            }
            bool DescFlag = false;
            bool textflag = false;


            if (Obj_DescCode != "")
            {
                DescFlag = true;
            }

            if (ViewTestCode != "")
            {
                ViewTestCode = ViewTestCode + "')";
                g = (int)VW_DescriptiveViewLogic.AlterView_NonDescRep(ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                if (g <= 0)
                {
                    Label44.Text = "record not exist";
                    return;
                }

                ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
                textflag = true;
            }


            if (textflag == true && DescFlag == true)
            {
                string[] Arr_Tes_code = Obj_DescCode.Split(',');
                string[] targetArr = new string[Arr_Tes_code.Length + 1];
                string[] urlArr = new string[Arr_Tes_code.Length + 1];
                string[] featuresArr = new string[Arr_Tes_code.Length + 1];

                for (int i = 0; i < Arr_Tes_code.Length; i++)
                {
                    targetArr[i] = Arr_Tes_code[i];
                    featuresArr[i] = "";
                    if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                    {
                        urlArr[i] = "TestResultDescriptiveReport.aspx?MTCode=" + Arr_Tes_code[i] + "&PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"] + "&ut=psc";
                    }
                    else
                    {
                        urlArr[i] = "TestResultDescriptiveReport.aspx?MTCode=" + Arr_Tes_code[i] + "&PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"];
                    }
                }
                targetArr[Arr_Tes_code.Length] = "1";
                featuresArr[Arr_Tes_code.Length] = "";
                if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                {
                    string PatRegID = Request.QueryString["PatRegID"].ToString();
                    string FID = Request.QueryString["FID"].ToString();

                    // urlArr[Arr_Tes_code.Length] = "CrystalReport_New.aspx?rt=NonDesc&print=v&ut=psc&PatRegID=" + PatRegID + "&FID=" + FID;
                    Session.Add("rptsql", "");
                    Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt");
                    Session["reportname"] = "NonDesc";
                    Session["RPTFORMAT"] = "pdf";

                }
                else
                {
                    // urlArr[Arr_Tes_code.Length] = "CrystalReport_New.aspx?rt=NonDesc&print=v";
                    Session.Add("rptsql", "");
                    Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt");
                    Session["reportname"] = "NonDesc";
                    Session["RPTFORMAT"] = "pdf";
                }
                ReportParameterClass.SelectionFormula = "";
                string close = "<script language='javascript'>javascript:OpenReport();</script>";
                Type title1 = this.GetType();
                Page.ClientScript.RegisterStartupScript(title1, "", close);
                //ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
            }
            else if (textflag == true)
            {
                string[] targetArr;
                string[] urlArr;
                string[] featuresArr;

                targetArr = new string[] { "1" };
                featuresArr = new string[] { "" };
                if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                {
                    string PatRegID = Request.QueryString["PatRegID"].ToString();
                    string FID = Request.QueryString["FID"].ToString();

                    // urlArr = new string[] { "CrystalReport_New.aspx?rt=NonDesc&print=v&ut=psc&PatRegID=" + PatRegID + "&FID=" + FID };
                    Session.Add("rptsql", "");
                    Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt");
                    Session["reportname"] = "NonDesc";
                    Session["RPTFORMAT"] = "pdf";
                }
                else
                {
                    // urlArr = new string[] { "CrystalReport_New.aspx?rt=NonDesc&print=v" };
                    Session.Add("rptsql", "");
                    Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt");
                    Session["reportname"] = "NonDesc";
                    Session["RPTFORMAT"] = "pdf";
                }
                ReportParameterClass.SelectionFormula = "";
                string close = "<script language='javascript'>javascript:OpenReport();</script>";
                Type title1 = this.GetType();
                Page.ClientScript.RegisterStartupScript(title1, "", close);
                //  ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
            }
            else
            {
                string[] Arr_Tes_code = Obj_DescCode.Split(',');
                string[] targetArr = new string[Arr_Tes_code.Length];
                string[] urlArr = new string[Arr_Tes_code.Length];
                string[] featuresArr = new string[Arr_Tes_code.Length];

                for (int i = 0; i < Arr_Tes_code.Length; i++)
                {
                    targetArr[i] = Arr_Tes_code[i];
                    featuresArr[i] = "";
                    if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                    {
                        urlArr[i] = "TestResultDescriptiveReport.aspx?MTCode=" + Arr_Tes_code[i] + "&PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"] + "&ut=psc";
                    }
                    else
                    {
                        urlArr[i] = "TestResultDescriptiveReport.aspx?MTCode=" + Arr_Tes_code[i] + "&PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"];
                    }
                }
                ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
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
        //if (dtban.Rows.Count > 0)
        //{
        //    string BName = "Krishna Diagnostic Center";
        //    string BCount = Patmst_New_Bal_C.PatientCountBanner_result(Convert.ToInt32(Session["Branchid"]));
        //    //  lblDemoHospitalName.Text = Convert.ToString(dtban.Rows[0]["BannerName"]).Trim();
        //    if (Convert.ToString(dtban.Rows[0]["BannerName"]).Trim() == BName)
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
        //                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
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
            cmdPrint.Visible = false;

        }
        Bindbanner();

        if (!Page.IsPostBack)
        {
            try
            {


                LUNAME.Text = Convert.ToString(Session["username"]);
                LblDCName.Text = Convert.ToString(Session["Bannername"]);
                LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                this.PopulateTreeView(dt, 0, null); 

                Fill_Labels();
                Patmst_Bal_C contact = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                txtpatientmail.Text = contact.Email;
                tvGroupTree.Nodes.Clear();

                //bool IsBalanceFlag = Convert.ToBoolean(Patmst_New_Bal_C.GetIsEnabledFlag(Convert.ToInt32(Session["Branchid"])));
                //if (IsBalanceFlag == true)
                //{
                    //string Balance = Patmst_New_Bal_C.GetBalace(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString());
                    //if (Balance == "0")
                    //{
                    //    btnprint_letterhead.Enabled = true;
                    //    cmdPrint.Enabled = true;

                    //}
                    //else
                    //{
                    //    btnprint_letterhead.Enabled = false;
                    //    cmdPrint.Enabled = false;

                    //}
                //}
                //else
                //{
                //    btnprint_letterhead.Enabled = true;
                //    cmdPrint.Enabled = true;
                //}
                if (Convert.ToString(Session["usertype"]) == "Administrator")
                {
                    btnprint_letterhead.Enabled = true;

                    cmdPrint.Enabled = true;

                }
                string subdept = "";
                dt = PatNBC.Get_subdept(Convert.ToString(Session["username"]));
                if (dt.Rows.Count > 0)
                {
                    subdept = Convert.ToString(dt.Rows[0]["subdept"]);
                }
                ArrayList hcodedistinct = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_notingroup(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
                for (int hdist = 0; hdist < hcodedistinct.Count; hdist++)
                {

                    ICollection icol = SubdepartmentLogic_Bal_C.GetSubdepartment_byCode((hcodedistinct[hdist] as PatSt_Bal_C).SDCode, Convert.ToInt32(Session["Branchid"])); //12
                    IEnumerator ie = icol.GetEnumerator();
                    while (ie.MoveNext())
                    {


                        Subdepartment_Bal_C hnt = (Subdepartment_Bal_C)ie.Current;
                        TreeNode tn = tvGroupTree.FindNode(hnt.SDCode);
                        if (tn == null)
                        {
                            tn = new TreeNode(hnt.SubdeptName);
                            tn.Checked = true;

                            tn.Value = hnt.SDCode;
                            tn.NavigateUrl = "#";

                            tvGroupTree.Nodes.Add(tn);
                        }

                        ICollection ictitle = MainTestLog_Bal_C.GetMaintestBy_Code((hcodedistinct[hdist] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                        IEnumerator ietitle = ictitle.GetEnumerator();
                        
                        while (ietitle.MoveNext())
                        {
                            
                            MainTest_Bal_C ttn = ietitle.Current as MainTest_Bal_C;
                            TreeNode tntitle = new TreeNode(ttn.Maintestname);
                            tntitle.Value = ttn.MTCode;

                            string PTStatus = (hcodedistinct[hdist] as PatSt_Bal_C).Patauthicante;

                            
                            bool PStatus = (hcodedistinct[hdist] as PatSt_Bal_C).Patrepstatus;
                            if (PStatus == true)
                            {
                                tntitle.Text = ttn.Maintestname  +  "    <span class='btn btn-sm btn-primary'>(Printed"+")</sapn>" ;
                                tntitle.Checked = false;
                            }
                            else
                            {
                                tntitle.Text = ttn.Maintestname ;
                                tntitle.Checked = true;
                            }
                            //if (PStatus != true)
                            //{
                            //    if (PTStatus == "Authorized")
                            //    {
                            //        tntitle.Text = ttn.Maintestname + "    <span class='btn btn-sm btn-primary'>(Authorized" + ")</sapn>";
                            //        tntitle.Checked = true;
                            //    }
                            //    else
                            //    {
                            //        tntitle.Text = ttn.Maintestname + "    <span class='btn btn-sm btn-primary'>(Tested" + ")</sapn>";
                            //        tntitle.Checked = false;
                            //    }
                            //}
                            tntitle.ToolTip = "Main test";
                            tntitle.NavigateUrl = "#";

                          
                            tn.ChildNodes.Add(tntitle);

                        }

                        tn.Expand();
                    }
                }

                ArrayList hcodedistinctP = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_ingroup_new(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
                for (int hdist = 0; hdist < hcodedistinctP.Count; hdist++)
                {

                    string ProfileName = Packagenew_Bal_C.getPNameByCode((hcodedistinctP[hdist] as PatSt_Bal_C).PackageCode, Convert.ToInt32(Session["Branchid"])); //12

                    TreeNode tn = tvGroupTree.FindNode((hcodedistinctP[hdist] as PatSt_Bal_C).PackageCode);
                    if (tn == null)
                    {
                        tn = new TreeNode(ProfileName);

                        tn.Checked = true;
                        tn.Value = (hcodedistinctP[hdist] as PatSt_Bal_C).PackageCode;
                        tn.NavigateUrl = "#";

                        tvGroupTree.Nodes.Add(tn);
                    }

                    ICollection ictitle = MainTestLog_Bal_C.GetMaintestBy_Code((hcodedistinctP[hdist] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                    IEnumerator ietitle = ictitle.GetEnumerator();
                    while (ietitle.MoveNext())
                    {
                        MainTest_Bal_C ttn = ietitle.Current as MainTest_Bal_C;
                        TreeNode tntitle = new TreeNode(ttn.Maintestname);
                        tntitle.Value = ttn.MTCode;
                        tntitle.Text = ttn.Maintestname;
                        tntitle.ToolTip = "Main test";
                        tntitle.NavigateUrl = "#";

                        tntitle.Checked = true;
                        tn.ChildNodes.Add(tntitle);
                    }

                    tn.Expand();
                }
                if (tvGroupTree.Nodes.Count == 0)
                {
                    cmdPrint.Visible = false;
                   
                }

                tvGroupGram.Nodes.Clear();

                ArrayList hcodetestresult = (ArrayList)PatSt_new_Bal_C.getPrintStatusTableByAuthorizedhemogram(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), "HM");
                for (int hdist = 0; hdist < hcodetestresult.Count; hdist++)
                {
                    ICollection icolheading = SubdepartmentLogic_Bal_C.GET_subdeptName_bycode("HM", Convert.ToInt32(Session["Branchid"]));
                    IEnumerator ieheading = icolheading.GetEnumerator();

                    while (ieheading.MoveNext())
                    {
                        Subdepartment_Bal_C hntheading = (Subdepartment_Bal_C)ieheading.Current;
                        TreeNode tnheading = tvGroupTree.FindNode(hntheading.SDCode);
                        tnheading = new TreeNode(hntheading.SubdeptName);

                        tnheading.Checked = true; //subdeptName
                        tnheading.Value = hntheading.SDCode;
                        tnheading.NavigateUrl = "#";
                        tvGroupGram.Nodes.Add(tnheading);
                        //testname====================================================================================
                        ICollection igram = SubdepartmentLogic_Bal_C.GET_Hemetology_Test(Request.QueryString["PatRegID"], Request.QueryString["FID"], "HM");
                        IEnumerator ie1 = igram.GetEnumerator();
                        while (ie1.MoveNext())
                        {
                            Subdepartment_Bal_C hntg = (Subdepartment_Bal_C)ie1.Current;
                            TreeNode tn1 = new TreeNode(hntg.testname);

                            tn1.Value = hntg.MTCode;
                            tn1.Text = hntg.testname;

                            tn1.Checked = true;
                            tnheading.ChildNodes.Add(tn1);

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
        #region Contact Information Of Patient
        Patmst_Bal_C CIT = null;
        try
        {

            CIT = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));

            lblRegNo.Text = Convert.ToString(CIT.PatRegID);
            lbldate.Text = CIT.FID;

            lblName.Text = CIT.Initial.Trim() + "." + CIT.Patname;
            lblage.Text = Convert.ToString(CIT.Age) + "/" + CIT.MYD;
            lblSex.Text = CIT.Sex;

            LblMobileno.Text = CIT.Phone;
            Lblcenter.Text = CIT.CenterName;
            lbldate.Text = Convert.ToString(CIT.Patregdate);
            LblRefDoc.Text = CIT.Drname;
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
        if (Request.QueryString["pfrm"] != null)
        {
            if (Request.QueryString["pfrm"].Trim() == "rfp")
            {
              //  Response.Redirect("Patientreport.aspx?sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&did=" + Request.QueryString["did"]);
                Response.Redirect("Printreport.aspx?sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&did=" + Request.QueryString["did"]);

            }
        }
        else if (Request.QueryString["source"] != null)
        {
            Response.Redirect("Testresultentry.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"] + "&did=" + Request.QueryString["did"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"]);
        }
        else
        {
            Response.Redirect("Addresult.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"] + "&did=" + Request.QueryString["did"] + "&Labid=" + Request.QueryString["Labid"] + "&sdt=" + Request.QueryString["sdt"] + "&edt=" + Request.QueryString["edt"] + "&tcd=" + Request.QueryString["tcd"] + "&stat=" + Request.QueryString["stat"] + "&pname=" + Request.QueryString["pname"] + "&sid=" + Request.QueryString["sid"] + "&vid=" + Request.QueryString["vid"] + "&CenterCode=" + Request.QueryString["CenterCode"] + "&formname=" + Request.QueryString["formname"].ToString() + "&form=" + Request.QueryString["form"].ToString() + "&user=" + Session["usertype"].ToString());
        }

    }

    protected void cmdPrint_Click1(object sender, EventArgs e)
    {
        string Obj_TestCode = "";
        string TextDesc = "";
        string Obj_DepCode = "";
        string Obj_DescCode = "";
        string ViewTestCode = "";
        Label1.Text = Convert.ToString(hdReportno.Value);

        //PatRegID,FID
        if (Request.QueryString["PatRegID"] != null && Request.QueryString["FID"] != null)
        {
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

                        MainTest_Bal_C Obj_MTB = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
                        Obj_TestCode = Obj_MTB.MTCode;
                        TextDesc = Obj_MTB.TextDesc;
                        Obj_DepCode = Obj_MTB.SDCode;
                        PatSt_Bal_C pt = new PatSt_Bal_C();
                        pt.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"].ToString()));

                        if (TextDesc == "DescType")
                        {
                            if (Obj_DescCode == "")
                            {
                                Obj_DescCode = " (VW_desfiledata.MTCode = '" + Obj_TestCode;
                            }
                            else
                            {
                                Obj_DescCode = Obj_DescCode + "' OR VW_desfiledata.MTCode = '" + Obj_TestCode;
                            }
                        }
                        else
                            if (ViewTestCode == "")
                            {
                                ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + Obj_TestCode;
                            }
                            else
                                ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + Obj_TestCode;


                        TAT_C at = new TAT_C();
                        //if (MainTestLog_Bal_C.IS_MTCodeForm(Obj_TestCode, Convert.ToInt32(Session["Branchid"])))
                        //{

                        //    IEnumerator ie2 = SubTestLog_Bal_C.GetTestCode_SDCode(Obj_DepCode, Obj_TestCode, Convert.ToInt32(Session["Branchid"])).GetEnumerator();
                        //    while (ie2.MoveNext())
                        //    {
                        //        SubTest_Bal_C tn = (SubTest_Bal_C)ie2.Current;
                        //        at.PatRegID = Request.QueryString["PatRegID"];
                        //        at.MTCode = Obj_TestCode;
                        //        at.STCODE = tn.STCODE;
                        //        at.DateOfRecord = Date.getdate();
                        //        at.FID = Request.QueryString["FID"];
                        //        at.Status = "Printed";
                        //        at.UserName = Session["username"].ToString();

                        //        at.Insert(Convert.ToInt32(Session["Branchid"]));

                        //    }
                        //}
                        //else
                        //{
                        //    at.PatRegID = Request.QueryString["PatRegID"];
                        //    at.MTCode = Obj_TestCode;
                        //    at.DateOfRecord = Date.getdate();
                        //    at.FID = Request.QueryString["FID"];
                        //    at.Status = "Printed";
                        //    at.UserName = Session["username"].ToString();

                        //    at.Insert(Convert.ToInt32(Session["Branchid"]));
                        //}

                    }//for if level==1

                }//for j

            }//for if  level==0 

        }
        bool DescFlag = false;
        bool textflag = false;

        if (Obj_DescCode != "")
        {
            Obj_DescCode = Obj_DescCode + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            g = (int)VW_DescriptiveViewLogic.AlterView_DescRep(Obj_DescCode, Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            if (g <= 0)
            {
                Label44.Text = "Record not found";
                return;
            }
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
            g = (int)VW_DescriptiveViewLogic.AlterView_NonDescRep(ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            if (g <= 0)
            {
                Label44.Text = "Record not found";
                return;
            }

            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
            textflag = true;
        }

        if (textflag == true && DescFlag == true)
        {
            string[] Arr_Tes_code = Obj_DescCode.Split(',');
            string[] targetArr = new string[Arr_Tes_code.Length + 1];
            string[] urlArr = new string[Arr_Tes_code.Length + 1];
            string[] featuresArr = new string[Arr_Tes_code.Length + 1];

            for (int i = 0; i < Arr_Tes_code.Length; i++)
            {

                targetArr[Arr_Tes_code.Length] = "1";
                featuresArr[i] = "";
                if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                {
                    // urlArr[i] = "CrystalReport_New.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"] + "&ut=psc&rt=DescType";
                    Session.Add("rptsql", "");
                    Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt");
                    Session["reportname"] = "DescType";
                    Session["RPTFORMAT"] = "pdf";
                }
                else
                {
                    //urlArr[i] = "CrystalReport_New.aspx?rt=DescType";
                    Session.Add("rptsql", "");
                    Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt");
                    Session["reportname"] = "DescType";
                    Session["RPTFORMAT"] = "pdf";
                }
            }
            targetArr[Arr_Tes_code.Length] = "1";
            featuresArr[Arr_Tes_code.Length] = "";
            if (Session["usertype"].ToString().Trim() == "CollectionCenter")
            {
                string PatRegID = Request.QueryString["PatRegID"].ToString();

                string FID = Request.QueryString["FID"].ToString();

                // urlArr[Arr_Tes_code.Length] = "CrystalReport_New.aspx?rt=NonDesc&print=p&ut=psc&PatRegID=" + PatRegID + "&FID=" + FID;
                Session.Add("rptsql", "");
                Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt");
                Session["reportname"] = "DescType";
                Session["RPTFORMAT"] = "pdf";
            }
            else
            {
                //urlArr[Arr_Tes_code.Length] = "CrystalReport_New.aspx?rt=NonDesc&print=p";
                Session.Add("rptsql", "");
                Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt");
                Session["reportname"] = "DescType";
                Session["RPTFORMAT"] = "pdf";
            }
            ReportParameterClass.SelectionFormula = "";
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
            // ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
        }
        else if (textflag == true)
        {
            string[] targetArr;
            string[] urlArr;
            string[] featuresArr;

            targetArr = new string[] { "1" };
            featuresArr = new string[] { "" };
            if (Session["usertype"].ToString().Trim() == "CollectionCenter")
            {
                string PatRegID = Request.QueryString["PatRegID"].ToString();
                string FID = Request.QueryString["FID"].ToString();

                // urlArr = new string[] { "CrystalReport_New.aspx?rt=NonDesc&print=p&ut=psc&PatRegID=" + PatRegID + "&FID=" + FID };
                Session.Add("rptsql", "");
                Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt");
                Session["reportname"] = "NonDesc";
                Session["RPTFORMAT"] = "pdf";

            }
            else
            {
                // urlArr = new string[] { "CrystalReport_New.aspx?rt=NonDesc&print=p" };
                Session.Add("rptsql", "");
                Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt");
                Session["reportname"] = "NonDesc";
                Session["RPTFORMAT"] = "pdf";

            }
            ReportParameterClass.SelectionFormula = "";
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
            // ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
        }
        else
        {
            string[] Arr_Tes_code = Obj_DescCode.Split(',');
            string[] targetArr = new string[Arr_Tes_code.Length];
            string[] urlArr = new string[Arr_Tes_code.Length];
            string[] featuresArr = new string[Arr_Tes_code.Length];

            for (int i = 0; i < Arr_Tes_code.Length; i++)
            {

                featuresArr = new string[] { "" };
                targetArr = new string[] { "1" };
                if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                {
                    // urlArr[i] = "CrystalReport_New.aspx?MTCode=" + Arr_Tes_code[i] + "&PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"] + "&ut=psc&rt=DescType";
                    Session.Add("rptsql", "");
                    Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt");
                    Session["reportname"] = "DescType";
                    Session["RPTFORMAT"] = "pdf";
                }
                else
                {
                    // urlArr[i] = "CrystalReport_New.aspx?rt=DescType";
                    Session.Add("rptsql", "");
                    Session["rptname"] = Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt");
                    Session["reportname"] = "DescType";
                    Session["RPTFORMAT"] = "pdf";
                }
            }
            //ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
            ReportParameterClass.SelectionFormula = "";
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
        }

    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();
        string filename = Server.MapPath("rpts");
        string rptname = "", path = "", selectonFormula = "";
        string Obj_TestCode = "";
        string TextDesc = "";
        string Obj_DepCode = "";

        string Obj_DescCode = "";
        string ViewTestCode = "";

        Label1.Text = Convert.ToString(hdReportno.Value);

        if (Request.QueryString["PatRegID"] != null && Request.QueryString["FID"] != null)
        {

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
                        MainTest_Bal_C Obj_MTB = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
                        Obj_TestCode = Obj_MTB.MTCode;
                        TextDesc = Obj_MTB.TextDesc;
                        Obj_DepCode = Obj_MTB.SDCode;
                        PatSt_Bal_C pt = new PatSt_Bal_C();
                        pt.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"].ToString()));

                        if (TextDesc == "DescType")
                        {
                            if (Obj_DescCode == "")
                            {
                                Obj_DescCode = " (MTCode = '" + Obj_TestCode;
                            }
                            else
                            {
                                Obj_DescCode = Obj_DescCode + "' OR MTCode = '" + Obj_TestCode;
                            }
                        }
                        else
                            if (ViewTestCode == "")
                            {
                                ViewTestCode = " (MTCode = '" + Obj_TestCode;
                            }
                            else
                                ViewTestCode = ViewTestCode + "' OR MTCode = '" + Obj_TestCode;


                    }//for if level==1

                }//for j

            }//for if  level==0 

        }
        bool DescFlag = false;
        bool textflag = false;

        if (Obj_DescCode != "")
        {
            Obj_DescCode = Obj_DescCode + "')";
            g = (int)VW_DescriptiveViewLogic.AlterView_DescRep(Obj_DescCode, Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            if (g <= 0)
            {
                Label44.Text = "Record not found";
                return;
            }
            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + Request.QueryString["FID"].ToString() + "'";
            DescFlag = true;
        }
        else
        {

        }

        if (ViewTestCode != "")
        {
            ViewTestCode = ViewTestCode + "')";
            g = (int)VW_DescriptiveViewLogic.AlterView_DescRep(ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            if (g <= 0)
            {
                Label44.Text = "Record not found";
                return;
            }

            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + Request.QueryString["FID"].ToString() + "'";
            textflag = true;
        }
        CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        ReportDataSet Objrds = new ReportDataSet();
        if (textflag == true && DescFlag == true)
        {

            #region NonDesc

         
            ReportDataSet.VW_patdatvwrecvwDataTable Objtr1 = Objrds.VW_patdatvwrecvw;
            ReportDataSetTableAdapters.VW_patdatvwrecvwTableAdapter Objtr = new ReportDataSetTableAdapters.VW_patdatvwrecvwTableAdapter();
            Objtr.Fill(Objtr1);

          

            rep.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt"));
            rep.SetDataSource((System.Data.DataSet)Objrds);

            rptname = "Pateintreportnondescriptive.rpt";
            path = Server.MapPath(rptname);
            filename = filename + "\\exported.pdf";
            CrystalDecisions.Shared.PageMargins pm = rep.PrintOptions.PageMargins;
           // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            pm.topMargin = 200 * line;
            // rep.PrintOptions.ApplyPageMargins(pm);
            selectonFormula = ReportParameterClass.SelectionFormula;
            cl.ExportandPrint("pdf", path, filename, selectonFormula, rep);
            ReportParameterClass.ReportType = "";
            ReportParameterClass.SelectionFormula = "";
            try
            {
                rep.PrintToPrinter(1, true, 1, 0);
                Label6.Visible = false;
            }
            catch (Exception exxx)
            {
                Label6.Visible = true;
            }
            rep.Close();
            rep.Dispose();
            GC.Collect();
            #endregion

            #region DescType
            string formula = "";


            ReportDataSet.VW_desfiledata_orgDataTable vw_memo = Objrds.VW_desfiledata_org;
            ReportDataSetTableAdapters.VW_desfiledata_orgTableAdapter vw_memoad = new ReportDataSetTableAdapters.VW_desfiledata_orgTableAdapter();
            vw_memoad.Fill(vw_memo);
            rep.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt"));
            rep.SetDataSource((System.Data.DataSet)Objrds);

            rptname = "Pateintreportdescriptive.rpt";
            path = Server.MapPath(rptname);
            filename = filename + "\\rpttest.pdf";
            CrystalDecisions.Shared.PageMargins pm1 = rep.PrintOptions.PageMargins;
            //int line1 = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line1 = 10;
            pm.topMargin = 200 * line1;
            rep.PrintOptions.ApplyPageMargins(pm1);
            cl.ExportandPrint("pdf", path, filename, formula, rep);
            ReportParameterClass.ReportType = "";
            try
            {
                rep.PrintToPrinter(1, true, 1, 0);
                Label6.Visible = false;
            }
            catch (Exception exxx)
            {
                Label6.Visible = true;
            }
            rep.Close();
            rep.Dispose();
            GC.Collect();
            #endregion

        }
        else if (textflag == true)
        {
            #region NonDesc

            ReportDataSet.VW_patdatvwrecvwDataTable dtrpt = Objrds.VW_patdatvwrecvw;
            ReportDataSetTableAdapters.VW_patdatvwrecvwTableAdapter rptadp = new ReportDataSetTableAdapters.VW_patdatvwrecvwTableAdapter();
            rptadp.Fill(dtrpt);

            //DataSet1.SetFontsDataTable dtsfont = dst.SetFonts;
            //DataSet1TableAdapters.SetFontsTableAdapter adpset = new DataSet1TableAdapters.SetFontsTableAdapter();
            //adpset.Fill(dtsfont);

            //DataSet1.ComputerwSys_mDataTable dtcomp = dst.ComputerwSys_m;
            //DataSet1TableAdapters.ComputerwSys_mTableAdapter adpcomp = new DataSet1TableAdapters.ComputerwSys_mTableAdapter();
            //adpcomp.Fill(dtcomp);

            rep.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt"));
            rep.SetDataSource((System.Data.DataSet)Objrds);

            rptname = "Pateintreportnondescriptive.rpt";
            path = Server.MapPath(rptname);
            filename = filename + "\\exported.pdf";
            CrystalDecisions.Shared.PageMargins pm = rep.PrintOptions.PageMargins;
           // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            pm.topMargin = 200 * line;
            //rep.PrintOptions.ApplyPageMargins(pm);
            selectonFormula = ReportParameterClass.SelectionFormula;
            cl.ExportandPrint("pdf", path, filename, selectonFormula, rep);
            ReportParameterClass.ReportType = "";
            ReportParameterClass.SelectionFormula = "";
            try
            {
                rep.PrintToPrinter(1, true, 1, 0);
                Label6.Visible = false;
            }
            catch (Exception exxx)
            {
                Label6.Visible = true;
            }
            rep.Close();
            rep.Dispose();
            GC.Collect();
            #endregion
        }
        else
        {

            #region DescType
            string formula = "";

            ReportDataSet.VW_desfiledata_orgDataTable vw_memo = Objrds.VW_desfiledata_org;
            ReportDataSetTableAdapters.VW_desfiledata_orgTableAdapter vw_memoad = new ReportDataSetTableAdapters.VW_desfiledata_orgTableAdapter();
            vw_memoad.Fill(vw_memo);
            rep.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt"));
            rep.SetDataSource((System.Data.DataSet)Objrds);

            rptname = "Pateintreportdescriptive.rpt";
            path = Server.MapPath(rptname);
            filename = filename + "\\rpttest.pdf";
            CrystalDecisions.Shared.PageMargins pm = rep.PrintOptions.PageMargins;
           // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            pm.topMargin = 200 * line;
            rep.PrintOptions.ApplyPageMargins(pm);
            cl.ExportandPrint("pdf", path, filename, formula, rep);
            ReportParameterClass.ReportType = "";
            try
            {
                rep.PrintToPrinter(1, true, 1, 0);
                Label6.Visible = false;
            }
            catch (Exception exxx)
            {
                Label6.Visible = true;
            }
            rep.Close();
            rep.Dispose();
            GC.Collect();

            #endregion
        }
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
        string Obj_TestCode = "";
        string TextDesc = "";

        string micro = "";
        string Histo = "";
        string Cyto = "";
        string Obj_DepCode = "";
        string Obj_DescCode = "";
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
                        if(h=="AS")
                        {
                            Sundept = "MICROBIOLOGY";
                        }
                        MainTest_Bal_C Obj_MTB = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
                        Obj_TestCode = Obj_MTB.MTCode;
                        TextDesc = Obj_MTB.TextDesc;
                        Obj_DepCode = Obj_MTB.SDCode;
                        PatSt_Bal_C pt = new PatSt_Bal_C();
                        pt.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"].ToString()));

                        if (Obj_DepCode == "CM")
                        {
                            if (micro == "")
                            {
                                micro = "( MTCode = '" + Obj_TestCode;
                            }
                            else
                            {
                                micro = micro + "( MTCode = '" + Obj_TestCode;
                            }
                        }
                        else if (Obj_DepCode == "H1")
                        {
                            if (Histo == "")
                            {
                                Histo = "( VW_desfiledata_HISTO.MTCode = '" + Obj_TestCode;
                            }
                            else
                            {
                                // Histo = Histo + "( MTCode = '" + Obj_TestCode;
                                Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + Obj_TestCode;
                            }
                        }
                        else if (Obj_DepCode == "FN")//CY FN
                        {
                            //if (Cyto == "")
                            //{
                            //    Cyto = "( MTCode = '" + Obj_TestCode;
                            //}
                            //else
                            //{
                            //    Cyto = Cyto + "( MTCode = '" + Obj_TestCode;
                            //}
                            if (Cyto == "")
                            {
                                Cyto = " (VW_desfiledata_CYTO.MTCode = '" + Obj_TestCode;
                            }
                            else
                            {
                                Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + Obj_TestCode;
                            }
                        }
                        else if (TextDesc == "DescType")//&& Obj_DepCode != "FN" && Obj_DepCode != "H1"
                        {
                            if (Obj_DescCode == "")
                            {
                                Obj_DescCode = " (VW_desfiledata.MTCode = '" + Obj_TestCode;
                            }
                            else
                            {
                                Obj_DescCode = Obj_DescCode + "' OR VW_desfiledata.MTCode = '" + Obj_TestCode;
                            }
                        }
                        else
                            if (ViewTestCode == "")
                            {
                                ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + Obj_TestCode;
                            }
                            else
                                ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + Obj_TestCode;

                        TAT_C at = new TAT_C();
                        //if (MainTestLog_Bal_C.IS_MTCodeForm(Obj_TestCode, Convert.ToInt32(Session["Branchid"])))
                        //{

                        //    IEnumerator ie2 = SubTestLog_Bal_C.GetTestCode_SDCode(Obj_DepCode, Obj_TestCode, Convert.ToInt32(Session["Branchid"])).GetEnumerator();
                        //    while (ie2.MoveNext())
                        //    {
                        //        SubTest_Bal_C tn = (SubTest_Bal_C)ie2.Current;
                        //        at.PatRegID = Request.QueryString["PatRegID"];
                        //        at.MTCode = Obj_TestCode;
                        //        at.STCODE = tn.STCODE;
                        //        at.DateOfRecord = Date.getdate();
                        //        at.FID = Request.QueryString["FID"];
                        //        at.Status = "Printed";
                        //        at.UserName = Session["username"].ToString();
                        //        at.PID = Convert.ToInt32(ViewState["PID"]);
                        //        at.Insert(Convert.ToInt32(Session["Branchid"]));
                        //    }
                        //}
                        //else
                        //{
                        //    at.PatRegID = Request.QueryString["PatRegID"];
                        //    at.MTCode = Obj_TestCode;

                        //    at.DateOfRecord = Date.getdate();
                        //    at.FID = Request.QueryString["FID"];
                        //    at.Status = "Printed";
                        //    at.UserName = Session["username"].ToString();
                        //    at.PID = Convert.ToInt32(ViewState["PID"]);
                        //    at.Insert(Convert.ToInt32(Session["Branchid"]));
                        //}
                    }
                }
            }
        }
        bool DescFlag = false;
        bool textflag = false;
        bool microflag = false;
        bool Histoflag = false;
        bool Cytoflag = false;
        if (Obj_DescCode != "")
        {
            Obj_DescCode = Obj_DescCode + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), Obj_DescCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

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

        
        if (textflag == true && microflag == true)
        {
            #region For Nondescriptive and Micro

            string[] Arr_Tes_code = Obj_DescCode.Split(',');
            string[] targetArr = new string[Arr_Tes_code.Length + 1];
            string[] urlArr = new string[Arr_Tes_code.Length + 1];
            string[] featuresArr = new string[Arr_Tes_code.Length + 1];

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "";
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
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
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
            CR.PrintOptions.ApplyPageMargins(pm);

            exportedpath = filename1;
            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

            CR.Close();
            CR.Dispose();
            GC.Collect();

            ////////////

            path = "";
            rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR1 = new ReportDocument();
            CR1.Load(Server.MapPath("~/CSMain1.rpt"));
            SqlConnection con1 = DataAccess.ConInitForDC();

            SqlDataAdapter sda1 = null;
            DataTable dt1 = new DataTable();
            // DataSet1 dst1 = new DataSet1();
            sda1 = new SqlDataAdapter("select * from VW_repmf where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

            sda1.Fill(dt1);
            CR1.SetDataSource((System.Data.DataTable)dt1);
            string path1 = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

            System.IO.File.WriteAllText(filename11, "");
            string exportedpath1 = "", selectionFormula1 = "";
            ReportParameterClass.SelectionFormula = "{VW_repmf.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_repmf.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument1 = null;
            if (CR1 != null)
            {
                crReportDocument1 = (ReportDocument)CR1;
            }
            CrystalDecisions.Shared.PageMargins pm1 = CR1.PrintOptions.PageMargins;
           // int line1 = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line1 = 10;
            pm1.topMargin = 200 * line1;
            CR1.PrintOptions.ApplyPageMargins(pm1);

            exportedpath1 = filename11;
            cl.ExportandPrintr("pdf", path1, exportedpath1, formula1, CR1);
            CR1.Close();
            CR1.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = " Report Not Generated, Please Generate Once Again ";
                return;
            }
            if (dt1.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

                FileInfo fi1 = new FileInfo(filepath11);
                fi1.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
            urlArr[Arr_Tes_code.Length] = "Testnormalresult.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf";
            targetArr[Arr_Tes_code.Length] = "1";
            featuresArr[Arr_Tes_code.Length] = "";
            string OrgFileMemo = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");
            string DupFileMemo = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

            string[] FilePathSplitOrgMemo = OrgFileMemo.Split('$');
            string[] FilePathSplitDupMemo = DupFileMemo.Split('$');

            if (FilePathSplitOrgMemo[1] != FilePathSplitDupMemo[1])
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
            urlArr[0] = "ReportMicro.aspx?rt=Micro&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf";


            ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
            #endregion
        }
        if (DescFlag == true && microflag == true)
        {
            #region For Descriptive and Micro

            string[] Arr_Tes_code = Obj_DescCode.Split(',');
            string[] targetArr = new string[Arr_Tes_code.Length + 1];
            string[] urlArr = new string[Arr_Tes_code.Length + 1];
            string[] featuresArr = new string[Arr_Tes_code.Length + 1];

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "";
            // ok objok = new ok();
            // PdfExportOptions pdfOptions = objok.ExportOptions.Pdf;

           // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            int topMargin = 14 * line;
            // objok.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);


            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename22 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            // objok.ExportToPdf(filename22);

            SqlConnection con = DataAccess.ConInitForDC();
            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);
            sda.Fill(dt);

            ReportParameterClass.ReportType = "";

            rep.Close();
            rep.Dispose();
            GC.Collect();

            ////////////

            path = "";
            rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR1 = new ReportDocument();
            CR1.Load(Server.MapPath("~/CSMain1.rpt"));
            SqlConnection con1 = DataAccess.ConInitForDC();

            SqlDataAdapter sda1 = null;
            DataTable dt1 = new DataTable();
            //DataSet1 dst1 = new DataSet1();
            sda1 = new SqlDataAdapter("select * from VW_repmf where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

            sda1.Fill(dt1);
            CR1.SetDataSource((System.Data.DataTable)dt1);
            string path1 = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

            System.IO.File.WriteAllText(filename11, "");
            string exportedpath1 = "", selectionFormula1 = "";
            ReportParameterClass.SelectionFormula = "{VW_repmf.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_repmf.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument1 = null;
            if (CR1 != null)
            {
                crReportDocument1 = (ReportDocument)CR1;
            }
            CrystalDecisions.Shared.PageMargins pm1 = CR1.PrintOptions.PageMargins;
           // int line1 = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line1 = 10;
            pm1.topMargin = 200 * line1;
            CR1.PrintOptions.ApplyPageMargins(pm1);

            exportedpath1 = filename11;
            cl.ExportandPrintr("pdf", path1, exportedpath1, formula1, CR1);
            CR1.Close();
            CR1.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            if (dt1.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

                FileInfo fi1 = new FileInfo(filepath11);
                fi1.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

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
            urlArr[Arr_Tes_code.Length] = "Redirect.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf";

            targetArr[Arr_Tes_code.Length] = "1";
            featuresArr[Arr_Tes_code.Length] = "";
            string OrgFileMemo = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");
            string DupFileMemo = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

            string[] FilePathSplitOrgMemo = OrgFileMemo.Split('$');
            string[] FilePathSplitDupMemo = DupFileMemo.Split('$');

            if (FilePathSplitOrgMemo[1] != FilePathSplitDupMemo[1])
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
            urlArr[0] = "ReportMicro.aspx?rt=Micro&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf";
            ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
            #endregion
        }
        if (textflag == true && DescFlag == true)
        {
            #region for Descriptive and No-Descriptive

            string[] Arr_Tes_code = Obj_DescCode.Split(',');
            string[] targetArr = new string[Arr_Tes_code.Length + 1];
            string[] urlArr = new string[Arr_Tes_code.Length + 1];
            string[] featuresArr = new string[Arr_Tes_code.Length + 1];

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "";
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
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
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
            CR.PrintOptions.ApplyPageMargins(pm);

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

           // int line1 = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line1 = 10;
            int topMargin = 14 * line1;
            // objok.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);

            string filename22 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            System.IO.File.WriteAllText(filename22, "");
            // objok.ExportToPdf(filename22);
            // SqlConnection con1 = DataAccess.ConInitForDC();
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
           // int line11 = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line11 = 10;
            pm1.topMargin = 200 * line;
            CR1.PrintOptions.ApplyPageMargins(pm);
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
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            if (dt1.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                FileInfo fi1 = new FileInfo(filepath11);
                fi1.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
            urlArr[Arr_Tes_code.Length] = "Testnormalresult.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf";

            targetArr[Arr_Tes_code.Length] = "1";
            featuresArr[Arr_Tes_code.Length] = "";
            string OrgFileMemo = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            string DupFileMemo = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            string[] FilePathSplitOrgMemo = OrgFileMemo.Split('$');
            string[] FilePathSplitDupMemo = DupFileMemo.Split('$');

            if (FilePathSplitOrgMemo[1] != FilePathSplitDupMemo[1])
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

            urlArr[0] = "Redirect.aspx?rt=DescType&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf";

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
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
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
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
              Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

            #endregion
        }
        else if (microflag == true && textflag == false && DescFlag == false)
        {
            #region Only Micro

            string formula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/CSMain1.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_repmf where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

            sda.Fill(dt);
            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_repmf.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_repmf.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
           // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line = 10;
            pm.topMargin = 200 * line;
            CR.PrintOptions.ApplyPageMargins(pm);

            exportedpath = filename1;

            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

            CR.Close();
            CR.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

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
            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

            #endregion
        }
        else if (DescFlag == true && microflag == false && textflag == false)
        {
            //#region Only Descriptive

            //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            //int topMargin = 14 * line;


            //string formula = "";


            //selectonFormula = ReportParameterClass.SelectionFormula;
            //ReportDocument CR = new ReportDocument();
            //CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive.rpt"));
            //SqlConnection con1 = DataAccess.ConInitForDC();

            //SqlDataAdapter sda1 = null;
            //DataTable dt1 = new DataTable();
            //// DataSet1 dst = new DataSet1();
            //sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

            //sda1.Fill(dt1);

            //CR.SetDataSource((System.Data.DataTable)dt1);



            //string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            //string filename22 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            //CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //SqlConnection con = DataAccess.ConInitForDC();
            //SqlDataAdapter sda = null;
            //DataTable dt = new DataTable();

            //sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);
            //sda.Fill(dt);

            //ReportParameterClass.ReportType = "";

            //rep.Close();
            //rep.Dispose();
            //GC.Collect();

            //if (dt.Rows.Count == 0)
            //{
            //   string filename11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            //    FileInfo fi = new FileInfo(filename11);
            //    fi.Delete();
            //    Label44.Text = "Report Not Generated, Please Generate Once Again !!";
            //    return;
            //}
            //string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            //string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

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

            //Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            //#endregion

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
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
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
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

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
          
            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
           // Response.Redirect("TestDescriptiveResult_Print//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
           // Response.Redirect("~/TestDescriptiveResult_Print.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"]);

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
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
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
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");

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

            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
            // Response.Redirect("TestDescriptiveResult_Print//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            // Response.Redirect("~/TestDescriptiveResult_Print.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"]);

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
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
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
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");

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

            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
            // Response.Redirect("TestDescriptiveResult_Print//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            // Response.Redirect("~/TestDescriptiveResult_Print.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"]);

            #endregion
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

    protected void CVMemo_Init(object sender, EventArgs e)
    {

    }

    protected void CVMemo_PreRender(object sender, EventArgs e)
    {

    }

    protected void btnprint_letterhead_Click(object sender, EventArgs e)
    {
        string Obj_TestCode = "";
        string TextDesc = "";

        string micro = "";
        string Histo = "";
        string Cyto = "";
        string Obj_DepCode = "";
        string Obj_DescCode = "";
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
                        string h = tvGroupTree.Nodes[i].Value;
                        string t = tvGroupTree.Nodes[i].ChildNodes[j].Value;

                        MainTest_Bal_C Obj_MTB = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
                        Obj_TestCode = Obj_MTB.MTCode;
                        TextDesc = Obj_MTB.TextDesc;
                        Obj_DepCode = Obj_MTB.SDCode;
                        PatSt_Bal_C pt = new PatSt_Bal_C();
                        pt.UpdatePrintstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"].ToString()));

                       

                        if (Obj_DepCode == "CM")
                        {
                            if (micro == "")
                            {
                                micro = "( MTCode = '" + Obj_TestCode;
                            }
                            else
                            {
                                micro = micro + "( MTCode = '" + Obj_TestCode;
                            }
                        }
                        else if (Obj_DepCode == "H1")
                        {
                            if (Histo == "")
                            {
                                Histo = "( VW_desfiledata_HISTO.MTCode = '" + Obj_TestCode;
                            }
                            else
                            {
                                // Histo = Histo + "( MTCode = '" + Obj_TestCode;
                                Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + Obj_TestCode;
                            }
                        }
                        else if (Obj_DepCode == "FN")//CY FN
                        {
                            //if (Cyto == "")
                            //{
                            //    Cyto = "( MTCode = '" + Obj_TestCode;
                            //}
                            //else
                            //{
                            //    Cyto = Cyto + "( MTCode = '" + Obj_TestCode;
                            //}
                            if (Cyto == "")
                            {
                                Cyto = " (VW_desfiledata_CYTO.MTCode = '" + Obj_TestCode;
                            }
                            else
                            {
                                Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + Obj_TestCode;
                            }
                        }
                        else if (TextDesc == "DescType")
                        {
                            if (Obj_DescCode == "")
                            {
                                Obj_DescCode = " (VW_desfiledata.MTCode = '" + Obj_TestCode;
                            }
                            else
                            {
                                Obj_DescCode = Obj_DescCode + "' OR VW_desfiledata.MTCode = '" + Obj_TestCode;
                            }
                        }
                        else
                            if (ViewTestCode == "")
                            {
                                ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + Obj_TestCode;
                            }
                            else
                                ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + Obj_TestCode;

                        TAT_C at = new TAT_C();
                        //if (MainTestLog_Bal_C.IS_MTCodeForm(Obj_TestCode, Convert.ToInt32(Session["Branchid"])))
                        //{

                        //    IEnumerator ie2 = SubTestLog_Bal_C.GetTestCode_SDCode(Obj_DepCode, Obj_TestCode, Convert.ToInt32(Session["Branchid"])).GetEnumerator();
                        //    while (ie2.MoveNext())
                        //    {
                        //        SubTest_Bal_C tn = (SubTest_Bal_C)ie2.Current;
                        //        at.PatRegID = Request.QueryString["PatRegID"];
                        //        at.MTCode = Obj_TestCode;
                        //        at.STCODE = tn.STCODE;
                        //        at.DateOfRecord = Date.getdate();
                        //        at.FID = Request.QueryString["FID"];
                        //        at.Status = "Printed";
                        //        at.UserName = Session["username"].ToString();
                        //        at.PID = Convert.ToInt32(ViewState["PID"]);
                        //        at.Insert(Convert.ToInt32(Session["Branchid"]));
                        //    }
                        //}
                        //else
                        //{
                        //    at.PatRegID = Request.QueryString["PatRegID"];
                        //    at.MTCode = Obj_TestCode;
                        //    at.DateOfRecord = Date.getdate();
                        //    at.FID = Request.QueryString["FID"];
                        //    at.Status = "Printed";
                        //    at.UserName = Session["username"].ToString();
                        //    at.PID = Convert.ToInt32(ViewState["PID"]);
                        //    at.Insert(Convert.ToInt32(Session["Branchid"]));
                        //}
                    }
                }
            }
        }
        bool DescFlag = false;
        bool textflag = false;
        bool microflag = false;
        bool Histoflag = false;
        bool Cytoflag = false;
        if (Obj_DescCode != "")
        {
            Obj_DescCode = Obj_DescCode + "')";
            AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
            VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), Obj_DescCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

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

        if (textflag == true && microflag == true)
        {
            #region For Nondescriptive and Micro

            string[] Arr_Tes_code = Obj_DescCode.Split(',');
            string[] targetArr = new string[Arr_Tes_code.Length + 1];
            string[] urlArr = new string[Arr_Tes_code.Length + 1];
            string[] featuresArr = new string[Arr_Tes_code.Length + 1];

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "";
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
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
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


            path = "";
            rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR1 = new ReportDocument();
            CR1.Load(Server.MapPath("~/CSMain1.rpt"));
            SqlConnection con1 = DataAccess.ConInitForDC();

            SqlDataAdapter sda1 = null;
            DataTable dt1 = new DataTable();

            sda1 = new SqlDataAdapter("select * from VW_repmf where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

            sda1.Fill(dt1);

            CR1.SetDataSource((System.Data.DataTable)dt1);
            string path1 = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

            System.IO.File.WriteAllText(filename11, "");
            string exportedpath1 = "", selectionFormula1 = "";
            ReportParameterClass.SelectionFormula = "{VW_repmf.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_repmf.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument1 = null;
            if (CR1 != null)
            {
                crReportDocument1 = (ReportDocument)CR1;
            }


            exportedpath1 = filename11;
            cl.ExportandPrintr("pdf", path1, exportedpath1, formula1, CR1);
            CR1.Close();
            CR1.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = " Report Not Generated, Please Generate Once Again !!";
                return;
            }
            if (dt1.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

                FileInfo fi1 = new FileInfo(filepath11);
                fi1.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again !!";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
            urlArr[Arr_Tes_code.Length] = "Testnormalresult.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf";

            targetArr[Arr_Tes_code.Length] = "1";
            featuresArr[Arr_Tes_code.Length] = "";
            string OrgFileMemo = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");
            string DupFileMemo = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

            string[] FilePathSplitOrgMemo = OrgFileMemo.Split('$');
            string[] FilePathSplitDupMemo = DupFileMemo.Split('$');

            if (FilePathSplitOrgMemo[1] != FilePathSplitDupMemo[1])
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
            urlArr[0] = "ReportMicro.aspx?rt=Micro&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf";


            ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
            #endregion
        }
        if (DescFlag == true && microflag == true)
        {
            #region For Descriptive and Micro

            string[] Arr_Tes_code = Obj_DescCode.Split(',');
            string[] targetArr = new string[Arr_Tes_code.Length + 1];
            string[] urlArr = new string[Arr_Tes_code.Length + 1];
            string[] featuresArr = new string[Arr_Tes_code.Length + 1];

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "";



            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename22 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");


            SqlConnection con = DataAccess.ConInitForDC();
            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);
            sda.Fill(dt);
            ReportParameterClass.ReportType = "";

            rep.Close();
            rep.Dispose();
            GC.Collect();

            path = "";
            rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR1 = new ReportDocument();
            CR1.Load(Server.MapPath("~/CSMain1.rpt"));
            SqlConnection con1 = DataAccess.ConInitForDC();

            SqlDataAdapter sda1 = null;
            DataTable dt1 = new DataTable();
            // DataSet1 dst1 = new DataSet1();
            sda1 = new SqlDataAdapter("select * from VW_repmf where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

            sda1.Fill(dt1);
            CR1.SetDataSource((System.Data.DataTable)dt1);
            string path1 = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

            System.IO.File.WriteAllText(filename11, "");
            string exportedpath1 = "", selectionFormula1 = "";
            ReportParameterClass.SelectionFormula = "{VW_repmf.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_repmf.FID}='" + Request.QueryString["FID"].ToString() + "'";
            ReportDocument crReportDocument1 = null;
            if (CR1 != null)
            {
                crReportDocument1 = (ReportDocument)CR1;
            }

            exportedpath1 = filename11;
            cl.ExportandPrintr("pdf", path1, exportedpath1, formula1, CR1);
            CR1.Close();
            CR1.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = " Report Not Generated, Please Generate Once Again !!";
                return;
            }
            if (dt1.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

                FileInfo fi1 = new FileInfo(filepath11);
                fi1.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again !!";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

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
            urlArr[Arr_Tes_code.Length] = "Redirect.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf";

            targetArr[Arr_Tes_code.Length] = "1";
            featuresArr[Arr_Tes_code.Length] = "";
            string OrgFileMemo = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");
            string DupFileMemo = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

            string[] FilePathSplitOrgMemo = OrgFileMemo.Split('$');
            string[] FilePathSplitDupMemo = DupFileMemo.Split('$');

            if (FilePathSplitOrgMemo[1] != FilePathSplitDupMemo[1])
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
            urlArr[0] = "ReportMicro.aspx?rt=Micro&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf";
            ResponseHelper.Redirect(urlArr, targetArr, featuresArr);
            #endregion
        }
        if (textflag == true && DescFlag == true)
        {
            #region for Descriptive and No-Descriptive

            string[] Arr_Tes_code = Obj_DescCode.Split(',');
            string[] targetArr = new string[Arr_Tes_code.Length + 1];
            string[] urlArr = new string[Arr_Tes_code.Length + 1];
            string[] featuresArr = new string[Arr_Tes_code.Length + 1];

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "";
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
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
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

            path = "";
            rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

            string filename22 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            SqlConnection con1 = DataAccess.ConInitForDC();
            SqlDataAdapter sda1 = null;
            DataTable dt1 = new DataTable();
            sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);
            sda1.Fill(dt1);

            ReportParameterClass.ReportType = "";
            rep.Close();
            rep.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = " Report Not Generated, Please Generate Once Again ";
                return;
            }
            if (dt1.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                FileInfo fi1 = new FileInfo(filepath11);
                fi1.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again !!";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
            urlArr[Arr_Tes_code.Length] = "Testnormalresult.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf";
            targetArr[Arr_Tes_code.Length] = "1";
            featuresArr[Arr_Tes_code.Length] = "";
            string OrgFileMemo = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            string DupFileMemo = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            string[] FilePathSplitOrgMemo = OrgFileMemo.Split('$');
            string[] FilePathSplitDupMemo = DupFileMemo.Split('$');

            if (FilePathSplitOrgMemo[1] != FilePathSplitDupMemo[1])
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

            urlArr[0] = "Redirect.aspx?rt=DescType&RepName=PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf";

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
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
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
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

            #endregion
        }
        else if (microflag == true && textflag == false && DescFlag == false)
        {
            #region Only Micro

            string formula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/CSMain1.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_repmf where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

            sda.Fill(dt);
            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_repmf.PatRegID}='" + Request.QueryString["PatRegID"].ToString() + "' and {VW_repmf.FID}='" + Request.QueryString["FID"].ToString() + "'";
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
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

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
            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Micro" + ".pdf");

            #endregion
        }
        else if (DescFlag == true && microflag == false && textflag == false)
        {
            #region Only Descriptive

            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename22 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            SqlConnection con = DataAccess.ConInitForDC();
            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);
            sda.Fill(dt);

            ReportParameterClass.ReportType = "";

            rep.Close();
            rep.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filename11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

                FileInfo fi = new FileInfo(filename11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again !!";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

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

            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
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
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
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
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");

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

            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "HISTO_Desc" + ".pdf");
            // Response.Redirect("TestDescriptiveResult_Print//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            // Response.Redirect("~/TestDescriptiveResult_Print.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"]);

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
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
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
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");

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

            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CYTO_Desc" + ".pdf");
            // Response.Redirect("TestDescriptiveResult_Print//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "_" + Request.QueryString["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            // Response.Redirect("~/TestDescriptiveResult_Print.aspx?PatRegID=" + Request.QueryString["PatRegID"] + "&FID=" + Request.QueryString["FID"]);

            #endregion
        }
    }

    //public void MailHemogram(string Type)
    //{
    //    string reportname = "";
    //    string TestMachineName = "";
    //    string ViewTestCode = "";
    //    string Obj_TestCode = "";
    //    int PatRegID = Convert.ToInt32(Request.QueryString["PatRegID"]);
    //    string FID = Request.QueryString["FID"];
    //    for (int i = 0; i <= tvGroupGram.Nodes.Count - 1; i++)
    //    {
    //        if (tvGroupGram.Nodes[i].Checked && tvGroupGram.Nodes[i].Depth == 0)
    //        {
    //            for (int j = 0; j <= tvGroupGram.Nodes[i].ChildNodes.Count - 1; j++)
    //            {
    //                if (tvGroupGram.Nodes[i].ChildNodes[j].Checked && tvGroupGram.Nodes[i].ChildNodes[j].Depth == 1)
    //                {
    //                    string h = tvGroupGram.Nodes[i].Value;
    //                    string t = tvGroupGram.Nodes[i].ChildNodes[j].Value;
    //                    Obj_TestCode = t;
    //                    if (ViewTestCode == "")
    //                    {
    //                        ViewTestCode = " (MTCode = '" + Obj_TestCode;
    //                    }
    //                    else
    //                        ViewTestCode = ViewTestCode + "' OR MTCode = '" + Obj_TestCode;
    //                }
    //            }
    //        }
    //    }
    //    if (ViewTestCode != "")
    //    {
    //        ViewTestCode = ViewTestCode + "')";
    //        g = (int)VW_DescriptiveViewLogic.AlterViewNoMEmoField(ViewTestCode, Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
    //        if (g <= 0)
    //        {
    //            Label44.Text = "Record not found";
    //            return;
    //        }

    //    }

    //    if (VW_DescriptiveViewLogic.Computername != "" || VW_DescriptiveViewLogic.Computername != null)
    //    {
    //        TestMachineName = VW_DescriptiveViewLogic.Computername;
    //    }
    //    else
    //    {
    //        TestMachineName = "";
    //    }
    //    if (VW_DescriptiveViewLogic.Reportname != "" || VW_DescriptiveViewLogic.Reportname != null)
    //    {
    //        reportname = VW_DescriptiveViewLogic.Reportname;
    //    }
    //    else
    //    {
    //        reportname = "";
    //    }
    //    //#region reportWBC

    //    //SqlConnection con = DataAccess.ConInitForDC();
    //    //SqlCommand cmd1 = con.CreateCommand();


    //    //cmd1.CommandText = "ALTER VIEW [dbo].[vw_Test_wbc]AS (SELECT PatRegID, Srno, wbc, wbc1, branchid, FID, wbc2 FROM dbo.Test_wbc where PatRegID=" + PatRegID + " and FID='" + FID + "')";
    //    //con.Open();
    //    //cmd1.ExecuteNonQuery();
    //    //con.Close(); con.Dispose();
    //    //#endregion

    //    //#region reportRBC
    //    //SqlConnection con1 = DataAccess.ConInitForDC();
    //    //SqlCommand cmd2 = con1.CreateCommand();


    //    //cmd2.CommandText = "ALTER VIEW [dbo].[Vw_Test_rbc]AS(SELECT PatRegID, srno, rbc, rbc_region, branchid, FID FROM dbo.Test_rbc where PatRegID=" + PatRegID + " and FID='" + FID + "')";
    //    //con1.Open();
    //    //cmd2.ExecuteNonQuery();
    //    //con1.Close(); con1.Dispose();

    //    //#endregion

    //    //#region reportPLT
    //    //SqlConnection con2 = DataAccess.ConInitForDC();
    //    //SqlCommand cmd3 = con2.CreateCommand();


    //    //cmd3.CommandText = "ALTER VIEW [dbo].[VW_GetTest_plt] AS (SELECT PatRegID, srno, plt, plt_region, pltr, branchid, FID FROM dbo.Test_plt where PatRegID=" + PatRegID + " and FID='" + FID + "')";
    //    //con2.Open();
    //    //cmd3.ExecuteNonQuery();
    //    //con2.Close(); con2.Dispose();
    //    //#endregion

    //    ReportParameterClass.ReportType = "TestResultgraph";
    //    ReportParameterClass.SelectionFormula = "";
    //    string[] urlArr = new string[1];
    //    string[] targetArr = new string[1];
    //    string[] featuresArr = new string[1];
    //    targetArr[0] = "1";
    //    featuresArr[0] = "";
    //    string filename11 = "";
    //    if (Type == "Patient")
    //    {
    //        filename11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Hemogram" + ".pdf");
    //    }
    //    else
    //    {
    //        filename11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Hemogram" + ".pdf");
    //    }
    //    System.IO.File.WriteAllText(filename11, "");


    //   // #region
    //   // CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
    //   // ReportDataSet Rds = new ReportDataSet();
    //   // //ReportDataSet.vw_Test_wbcDataTable vw_Test_wbc = Rds.vw_Test_wbc;
    //   // //ReportDataSetTableAdapters.vw_Test_wbcTableAdapter vw_wbcad = new ReportDataSetTableAdapters.vw_Test_wbcTableAdapter();
    //   // //vw_wbcad.Fill(vw_Test_wbc);

    //   // ////rbc
    //   // //ReportDataSet.Vw_Test_rbcDataTable Vw_Test_rbc = Rds.Vw_Test_rbc;
    //   // //ReportDataSetTableAdapters.Vw_Test_rbcTableAdapter vw_rbcad = new ReportDataSetTableAdapters.Vw_Test_rbcTableAdapter();
    //   // //vw_rbcad.Fill(Vw_Test_rbc);

    //   // ////plt
    //   // //ReportDataSet.VW_GetTest_pltDataTable VW_GetTest_plt = Rds.VW_GetTest_plt;
    //   // //ReportDataSetTableAdapters.VW_GetTest_pltTableAdapter vw_pltad = new ReportDataSetTableAdapters.VW_GetTest_pltTableAdapter();
    //   // //vw_pltad.Fill(VW_GetTest_plt);

    //   // ReportDataSet.VW_patdatvwrecvwDataTable dtrpt = Rds.VW_patdatvwrecvw;
    //   // ReportDataSetTableAdapters.VW_patdatvwrecvwTableAdapter rptadp = new ReportDataSetTableAdapters.VW_patdatvwrecvwTableAdapter();
    //   // rptadp.Fill(dtrpt);

    //   // //ReportDataSet.SetFontsDataTable dtsfont = dst.SetFonts;
    //   // //ReportDataSetTableAdapters.SetFontsTableAdapter adpset = new ReportDataSetTableAdapters.SetFontsTableAdapter();
    //   // //adpset.Fill(dtsfont);

    //   // //ReportDataSet.ComputerwSys_mDataTable dtcomp = dst.ComputerwSys_m;
    //   // //ReportDataSetTableAdapters.ComputerwSys_mTableAdapter adpcomp = new ReportDataSetTableAdapters.ComputerwSys_mTableAdapter();
    //   // //adpcomp.Fill(dtcomp);

    //   // //ReportDataSet.VW_testresult_pltDataTable plt = Rds.VW_testresult_plt;
    //   // //ReportDataSetTableAdapters.VW_testresult_pltTableAdapter pltad = new ReportDataSetTableAdapters.VW_testresult_pltTableAdapter();
    //   // //pltad.Fill(plt);

    //   // //ReportDataSet.VW_testresult_rbcDataTable rbc = Rds.VW_testresult_rbc;
    //   // //ReportDataSetTableAdapters.VW_testresult_rbcTableAdapter rbcad = new ReportDataSetTableAdapters.VW_testresult_rbcTableAdapter();
    //   // //rbcad.Fill(rbc);

    //   // //ReportDataSet.VW_testresult_wbcDataTable wbc = Rds.VW_testresult_wbc;
    //   // //ReportDataSetTableAdapters.VW_testresult_wbcTableAdapter wbcad = new ReportDataSetTableAdapters.VW_testresult_wbcTableAdapter();
    //   // //wbcad.Fill(wbc);


    //   // //rptname = "Rpt_TestResultgraph_Email.rpt";
    //   // //rep.Load(Server.MapPath(rptname));

    //   // //rep.SetDataSource((System.Data.DataSet)Rds);
    //   // //path = Server.MapPath(rptname);

    //   // CrystalDecisions.Shared.PageMargins pm = rep.PrintOptions.PageMargins;
    //   //// int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
    //   // int line = 10;
    //   // pm.topMargin = 200 * line;
    //   // rep.PrintOptions.ApplyPageMargins(pm);
    //   // selectonFormula = ReportParameterClass.SelectionFormula;

    //   // cl.ExportandPrintr("pdf", path, filename11, selectonFormula, rep);

    //   // ReportParameterClass.ReportType = "";
    //   // ReportParameterClass.SelectionFormula = "";

    //   // rep.Close();
    //   // rep.Dispose();
    //   // GC.Collect();

    //   // #endregion


    //}

    public void DeleteFile(string folderName, string ReportType)
    {
        string OrgFile = Server.MapPath(folderName + "//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + ReportType + ".pdf");
        string DupFile = Server.MapPath(folderName + "//" + "$" + Date2 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + ReportType + ".pdf");

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
            cashmain.getBalance(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
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
                Patmst_Bal_C contact = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                string pname = contact.Initial.Trim() + " " + contact.Patname;
                string mobile = contact.Phone;
                string email = contact.Email;
                string msg = "";

                if (mobile != "91" && mobile != "")
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
    protected void Chkemailtopatient_CheckedChanged(object sender, EventArgs e)
    {

        if (Chkemailtopatient.Checked == true)
        {
            Cshmst_Bal_C cashmain = new Cshmst_Bal_C();
            int PID = -1;
            if (Convert.ToInt32(ViewState["PID"]) != -1)
            {
                PID = Convert.ToInt32(ViewState["PID"]);
            }
            cashmain.getBalance(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]), PID);
            if (cashmain.Balance > 0)
            {
                Label6.Text = "Pending balance.";
                Label6.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
            }
            else
            {

                Patmst_Bal_C contact = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
                string pname = contact.Initial.Trim() + " " + contact.Patname;
                string mobile = contact.Phone;
                string email = contact.Email;
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
                string Obj_TestCode = "";
                string TextDesc = "";
                string Obj_DepCode = "";
                string Obj_DescCode = "";
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
                                MainTest_Bal_C Obj_MTB = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
                                Obj_TestCode = Obj_MTB.MTCode;
                                TextDesc = Obj_MTB.TextDesc;
                                Obj_DepCode = Obj_MTB.SDCode;
                                PatSt_Bal_C pt = new PatSt_Bal_C();
                                pt.UpdateEmailstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString( Session["username"]));
                                //pt.UpdateEmailstatus_testwise(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB.MTCode, Convert.ToInt32(Session["Branchid"]));

                                if (Obj_DepCode == "CM")
                                {
                                    if (micro == "")
                                    {
                                        micro = "( MTCode = '" + Obj_TestCode;
                                    }
                                    else
                                    {
                                        micro = micro + "( MTCode = '" + Obj_TestCode;
                                    }
                                }
                                else if (Obj_DepCode == "H1")
                                {
                                    if (Histo == "")
                                    {
                                        Histo = "( VW_desfiledata_HISTO.MTCode = '" + Obj_TestCode;
                                    }
                                    else
                                    {
                                        // Histo = Histo + "( MTCode = '" + Obj_TestCode;
                                        Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + Obj_TestCode;
                                    }
                                }
                                else if (Obj_DepCode == "FN")//CY FN
                                {
                                    //if (Cyto == "")
                                    //{
                                    //    Cyto = "( MTCode = '" + Obj_TestCode;
                                    //}
                                    //else
                                    //{
                                    //    Cyto = Cyto + "( MTCode = '" + Obj_TestCode;
                                    //}
                                    if (Cyto == "")
                                    {
                                        Cyto = " (VW_desfiledata_CYTO.MTCode = '" + Obj_TestCode;
                                    }
                                    else
                                    {
                                        Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + Obj_TestCode;
                                    }
                                }
                                else if (TextDesc == "DescType")//&& Obj_DepCode != "FN" && Obj_DepCode != "H1"
                                {
                                    if (Obj_DescCode == "")
                                    {
                                        Obj_DescCode = " (VW_desfiledata.MTCode = '" + Obj_TestCode;
                                    }
                                    else
                                    {
                                        Obj_DescCode = Obj_DescCode + "' OR VW_desfiledata.MTCode = '" + Obj_TestCode;
                                    }
                                }
                                else
                                    if (ViewTestCode == "")
                                    {
                                        ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + Obj_TestCode;
                                    }
                                    else
                                        ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + Obj_TestCode;
                            }
                        }
                    }
                }
                bool DescFlag = false;
                bool textflag = false;
                bool microflag = false;
                bool Histoflag = false;
                bool Cytoflag = false;

                if (Obj_DescCode != "")
                {
                    Obj_DescCode = Obj_DescCode + "')";
                    AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                    VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), Obj_DescCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

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
                    string filename11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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
                    int line1 = 10;
                    int topMargin = 14 * line1;
                    // objRptTestResultMemo_Email.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);                              

                    SqlConnection con1 = DataAccess.ConInitForDC();
                    SqlDataAdapter sda1 = null;
                    DataTable dt1 = new DataTable();
                    //  DataSet1 dst1 = new DataSet1();
                    sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

                    sda1.Fill(dt1);

                    //filename = filename + "\\mailexported1.pdf";
                    string filename22 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                    //  objRptTestResultMemo_Email.ExportToPdf(filename22);
                    ReportParameterClass.ReportType = "";
                    rep.Close();
                    rep.Dispose();
                    GC.Collect();

                    if (dt.Rows.Count == 0)
                    {
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        Label44.Text = " Report Not Generated, Please Generate Once Again";
                        return;
                    }
                    if (dt1.Rows.Count == 0)
                    {
                        string filepath111 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));
                        Attachment att1 = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
                        msgmail.Attachments.Add(att);
                        msgmail.Attachments.Add(att1);

                        if (tvGroupGram.Nodes.Count > 0)
                        {
                            if (tvGroupGram.Nodes[0].ChildNodes.Count > 0 && tvGroupGram.Nodes[0].ChildNodes[0].Checked)
                            {
                                // MailHemogram("Patient");
                                // Attachment att2 = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Hemogram" + ".pdf"));
                                // msgmail.Attachments.Add(att2);
                            }
                        }

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
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                            FileInfo fi = new FileInfo(filepath11);
                            fi.Delete();
                            string filepath111 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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

                    string filename11C = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
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
                        string filepath11C = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
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
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf"));

                        if (tvGroupGram.Nodes.Count > 0)
                        {
                            if (tvGroupGram.Nodes[0].ChildNodes.Count > 0 && tvGroupGram.Nodes[0].ChildNodes[0].Checked)
                            {
                                //MailHemogram("Patient");
                                // Attachment att1 = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Hemogram" + ".pdf"));
                                // msgmail.Attachments.Add(att1);
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
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "CYTO_Desc" + ".pdf");
                            FileInfo fi = new FileInfo(filepath11);
                            fi.Delete();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                        }
                        else
                        {
                            // string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                            //  FileInfo fi = new FileInfo(filepath11);
                            // fi.Delete();
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
                        // DataSet1 dst = new DataSet1();
                        sdaH = new SqlDataAdapter("select * from VW_desfiledata_org_HISTO where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", conH);

                        sdaH.Fill(dt);

                        CRH.SetDataSource((System.Data.DataTable)dt);
                        string pathH = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");

                        string filename11H = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
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
                            string filepath11H = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
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
                            Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf"));

                            if (tvGroupGram.Nodes.Count > 0)
                            {
                                if (tvGroupGram.Nodes[0].ChildNodes.Count > 0 && tvGroupGram.Nodes[0].ChildNodes[0].Checked)
                                {
                                    //MailHemogram("Patient");
                                    // Attachment att1 = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Hemogram" + ".pdf"));
                                    // msgmail.Attachments.Add(att1);
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
                                string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "HISTO_Desc" + ".pdf");
                                FileInfo fi = new FileInfo(filepath11);
                                fi.Delete();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                            }
                            else
                            {
                                // string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                                //  FileInfo fi = new FileInfo(filepath11);
                                // fi.Delete();
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
                        string filename11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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
                            Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));

                            if (tvGroupGram.Nodes.Count > 0)
                            {
                                if (tvGroupGram.Nodes[0].ChildNodes.Count > 0 && tvGroupGram.Nodes[0].ChildNodes[0].Checked)
                                {
                                    //MailHemogram("Patient");
                                    // Attachment att1 = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Hemogram" + ".pdf"));
                                    // msgmail.Attachments.Add(att1);
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
                                string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                                FileInfo fi = new FileInfo(filepath11);
                                fi.Delete();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('E-mail send successfully.');", true);
                            }
                            else
                            {
                                // string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                                //  FileInfo fi = new FileInfo(filepath11);
                                // fi.Delete();
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
                        // RptTestResultMemo_Email objRptTestResultMemo_Email = new RptTestResultMemo_Email();
                        // PdfExportOptions pdfOptions = objRptTestResultMemo_Email.ExportOptions.Pdf;

                        // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
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
                        string filename22 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                        CR.SetDataSource((System.Data.DataTable)dt);
                        string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                        string filename11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                        System.IO.File.WriteAllText(filename11, "");

                        // objRptTestResultMemo_Email.ExportToPdf(filename22);
                        ReportParameterClass.ReportType = "";

                        rep.Close();
                        rep.Dispose();
                        GC.Collect();
                        if (dt.Rows.Count == 0)
                        {
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
                            Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
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
                                string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                                FileInfo fi = new FileInfo(filepath11);
                                fi.Delete();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Email send successfully.');", true);
                            }
                            else
                            {
                                //string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                                //FileInfo fi = new FileInfo(filepath11);
                                // fi.Delete();
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
            }
        
    }
    protected void Chkdocemail_CheckedChanged(object sender, EventArgs e)
    {
        if (Chkdocemail.Checked == true)
        {
            Patmst_Bal_C contact = new Patmst_Bal_C(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));


            string pname = contact.Initial.Trim() + " " + contact.Patname;
            string mobile = contact.Phone;
            string email = contact.Email;
            string docemail = DrMT_Bal_C.GetEmaildrNameTable(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));

            //string[] DoctorEmail = docemail.Split(',');
            //int EMailCount = DoctorEmail.Length;
            //for (int k = 0; k < EMailCount; k++)
            //{
            //}
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

            string Obj_TestCode = "";
            string TextDesc = "";
            string Obj_DepCode = "";
            string Obj_DescCode = "";
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
            #region Get MTCode And DescFlag
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
                            MainTest_Bal_C Obj_MTB = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
                            Obj_TestCode = Obj_MTB.MTCode;
                            TextDesc = Obj_MTB.TextDesc;
                            Obj_DepCode = Obj_MTB.SDCode;
                            PatSt_Bal_C pt = new PatSt_Bal_C();
                            pt.UpdateEmailstatus(Request.QueryString["PatRegID"].ToString(), Request.QueryString["FID"].ToString(), Obj_MTB.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"]));
                           
                            if (Obj_DepCode == "CM")
                            {
                                if (micro == "")
                                {
                                    micro = "( MTCode = '" + Obj_TestCode;
                                }
                                else
                                {
                                    micro = micro + "( MTCode = '" + Obj_TestCode;
                                }
                            }
                            else if (Obj_DepCode == "H1")
                            {
                                if (Histo == "")
                                {
                                    Histo = "( VW_desfiledata_HISTO.MTCode = '" + Obj_TestCode;
                                }
                                else
                                {
                                    // Histo = Histo + "( MTCode = '" + Obj_TestCode;
                                    Histo = Histo + "' OR VW_desfiledata_HISTO.MTCode = '" + Obj_TestCode;
                                }
                            }
                            else if (Obj_DepCode == "FN")//CY FN
                            {
                                //if (Cyto == "")
                                //{
                                //    Cyto = "( MTCode = '" + Obj_TestCode;
                                //}
                                //else
                                //{
                                //    Cyto = Cyto + "( MTCode = '" + Obj_TestCode;
                                //}
                                if (Cyto == "")
                                {
                                    Cyto = " (VW_desfiledata_CYTO.MTCode = '" + Obj_TestCode;
                                }
                                else
                                {
                                    Cyto = Cyto + "' OR VW_desfiledata_CYTO.MTCode = '" + Obj_TestCode;
                                }
                            }
                            else if (TextDesc == "DescType")//&& Obj_DepCode != "FN" && Obj_DepCode != "H1"
                            {
                                if (Obj_DescCode == "")
                                {
                                    Obj_DescCode = " (VW_desfiledata.MTCode = '" + Obj_TestCode;
                                }
                                else
                                {
                                    Obj_DescCode = Obj_DescCode + "' OR VW_desfiledata.MTCode = '" + Obj_TestCode;
                                }
                            }
                            else
                                if (ViewTestCode == "")
                                {
                                    ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + Obj_TestCode;
                                }
                                else
                                    ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + Obj_TestCode;
                        }
                    }
                }
            }
            #endregion

            bool DescFlag = false;
            bool textflag = false;
            bool Histoflag = false;
            bool Cytoflag = false;

            if (Obj_DescCode != "")
            {
                Obj_DescCode = Obj_DescCode + "')";
                AlterView_VE_GetLabNo(Request.QueryString["PatRegID"]);
                VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), Obj_DescCode, Request.QueryString["PatRegID"], Request.QueryString["FID"]);

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
                string filename11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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
                //RptTestResultMemo_Email objRptTestResultMemo_Email = new RptTestResultMemo_Email();
                //PdfExportOptions pdfOptions = objRptTestResultMemo_Email.ExportOptions.Pdf;

               // int line1 = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line1 = 10;
                int topMargin = 14 * line1;
                // objRptTestResultMemo_Email.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);

                SqlConnection con1 = DataAccess.ConInitForDC();
                SqlDataAdapter sda1 = null;
                DataTable dt1 = new DataTable();
                //DataSet1 dst1 = new DataSet1();
                sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con1);

                sda1.Fill(dt1);
                string filename22 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                // objRptTestResultMemo_Email.ExportToPdf(filename22);

                ReportParameterClass.ReportType = "";
                rep.Close();
                rep.Dispose();
                GC.Collect();
                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    FileInfo fi = new FileInfo(filepath11);
                    fi.Delete();
                    Label44.Text = "Report Not Generated, Please Generate Once Again ";
                    return;
                }
                if (dt1.Rows.Count == 0)
                {
                    string filepath111 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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

                    Attachment att = new Attachment(Server.MapPath("~/EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));
                    Attachment att1 = new Attachment(Server.MapPath("~/EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
                    msgmail.Attachments.Add(att);
                    msgmail.Attachments.Add(att1);

                    if (tvGroupGram.Nodes.Count > 0)
                    {
                        if (tvGroupGram.Nodes[0].ChildNodes.Count > 0 && tvGroupGram.Nodes[0].ChildNodes[0].Checked)
                        {
                            //MailHemogram("Doctor");
                           // Attachment att2 = new Attachment(Server.MapPath("~/EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Hemogram" + ".pdf"));
                            //msgmail.Attachments.Add(att2);
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
                        string filepath11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        string filepath111 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
                string filename11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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
                    string filepath11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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

                    Attachment att = new Attachment(Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));
                    if (tvGroupGram.Nodes.Count > 0)
                    {
                        if (tvGroupGram.Nodes[0].ChildNodes.Count > 0 && tvGroupGram.Nodes[0].ChildNodes[0].Checked)
                        {
                            //MailHemogram("Doctor");
                            //Attachment att1 = new Attachment(Server.MapPath("~/EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Hemogram" + ".pdf"));
                            //msgmail.Attachments.Add(att1);
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
                        string filepath11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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
            else
            {
                #region For MemoFields

                CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                //RptTestResultMemo_Email objRptTestResultMemo_Email = new RptTestResultMemo_Email();
                //PdfExportOptions pdfOptions = objRptTestResultMemo_Email.ExportOptions.Pdf;

               // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                int line = 10;
                int topMargin = 14 * line;
                // objRptTestResultMemo_Email.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);          

                SqlConnection con = DataAccess.ConInitForDC();
                SqlDataAdapter sda = null;
                DataTable dt = new DataTable();
                // DataSet1 dst = new DataSet1();
                sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["PatRegID"].ToString() + "'  and FID='" + Request.QueryString["FID"] + "' ", con);

                sda.Fill(dt);
                string filename22 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                // objRptTestResultMemo_Email.ExportToPdf(filename22);

                ReportParameterClass.ReportType = "";

                rep.Close();
                rep.Dispose();
                GC.Collect();
                if (dt.Rows.Count == 0)
                {
                    string filepath11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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

                    Attachment att = new Attachment(Server.MapPath("~/EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
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
                        string filepath11 = Server.MapPath("EmailDrReport//" + "$" + Date1 + "$" + Request.QueryString["PatRegID"].ToString() + "" + Request.QueryString["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
    protected void txtpatientmail_TextChanged(object sender, EventArgs e)
    {
        if (txtpatientmail.Text != "")
        {
            Patmst_Bal_C Objpbc = new Patmst_Bal_C();
            Objpbc.Email = txtpatientmail.Text;
            Objpbc.Update_EmailId(Request.QueryString["PatRegID"], Request.QueryString["FID"], Convert.ToInt32(Session["Branchid"]));
            this.Chkemailtopatient_CheckedChanged(null, null);
        }

    }
    public void AlterView_VE_GetLabNo(string PatRegID)
    {
        int i;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "ALTER VIEW [dbo].[VW_GetLabNo]AS (select  LabNo,PatRegID,MTCode,Branchid from patmstd  where  PatRegID='" + PatRegID + "' and  FID ='" + Convert.ToString(Request.QueryString["FID"]) + "'  )";
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
}