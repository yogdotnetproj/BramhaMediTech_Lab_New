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
using System.Data.SqlClient;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Web.Management;


public partial class PatientCallSetup : System.Web.UI.Page
{
    int g;
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string Date2 = DateTime.Now.AddDays(-1).ToString("ddMMyyyy");
    string rptname = "", path = "", selectonFormula = "", PatRegID = "", FID="";
    Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();
    Patmst_New_Bal_C PatNBC = new Patmst_New_Bal_C();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
   
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Convert.ToString(Session["HMS"]) != "Yes")
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("PatientCallSetup.aspx");
                }
            }

          
            PatRegID = Convert.ToString(Request.QueryString["Regno"].ToString());
             FID = Convert.ToString(Request.QueryString["FID"].ToString());
            
             if (Convert.ToString(Request.QueryString["CallS"]) == "Done")
             {
                 chkiscall.Checked = true;
             }
             else
             {
                 chkiscall.Checked = false;
             }
             ViewState["PatRegID"] = PatRegID;
             patientdemo();
             bindtreeview(PatRegID, FID);
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
                 btnprintBalance.Visible = true;
                 Button2.Visible = false;
             }
             else
             {
                 btnprintBalance.Visible = false;
                 Button2.Visible = true;
             }
        }
    }
    
    public void bindtreeview(string PatRegID, string FID)
    {
        try
        {

            DataTable dt = new DataTable();
            string subdept = "";
            dt = PatNBC.Get_subdept(Convert.ToString(Session["username"]));
            if (dt.Rows.Count > 0)
            {
                subdept = Convert.ToString(dt.Rows[0]["subdept"]);
            }
            ArrayList TestCode_Array = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_notingroup_Call(PatRegID, Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
            for (int Tid = 0; Tid < TestCode_Array.Count; Tid++)
            {

                ICollection icol = SubdepartmentLogic_Bal_C.GetSubdepartment_byCode((TestCode_Array[Tid] as PatSt_Bal_C).SDCode, Convert.ToInt32(Session["Branchid"])); //12
                IEnumerator ie = icol.GetEnumerator();
                while (ie.MoveNext())
                {


                    Subdepartment_Bal_C Sdb_C = (Subdepartment_Bal_C)ie.Current;
                    TreeNode Obj_TrNO = tvGroupTree.FindNode(Sdb_C.SDCode);
                    if (Obj_TrNO == null)
                    {
                        Obj_TrNO = new TreeNode(Sdb_C.SubdeptName);
                        Obj_TrNO.Checked = true;

                        Obj_TrNO.Value = Sdb_C.SDCode;
                        Obj_TrNO.NavigateUrl = "#";

                        tvGroupTree.Nodes.Add(Obj_TrNO);
                    }

                    ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((TestCode_Array[Tid] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                    IEnumerator IE_Test = IC_Test.GetEnumerator();

                    while (IE_Test.MoveNext())
                    {

                        MainTest_Bal_C MTB_C = IE_Test.Current as MainTest_Bal_C;
                        TreeNode TVN_Test = new TreeNode(MTB_C.Maintestname);
                        TVN_Test.Value = MTB_C.MTCode;

                        bool PStatus = (TestCode_Array[Tid] as PatSt_Bal_C).Patrepstatus;
                        if (PStatus == true)
                        {
                            TVN_Test.Text = MTB_C.Maintestname + "    <span class='btn btn-sm btn-primary'>(Printed" + ")</sapn>";
                            TVN_Test.Checked = false;
                        }
                        else
                        {
                            TVN_Test.Text = MTB_C.Maintestname;
                            TVN_Test.Checked = true;
                        }
                        TVN_Test.ToolTip = "Main test";
                        TVN_Test.NavigateUrl = "#";


                        Obj_TrNO.ChildNodes.Add(TVN_Test);

                    }

                    Obj_TrNO.Expand();
                }
            }

            ArrayList MT_CodeTest = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_ingroup_new(PatRegID, Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
            for (int Tid = 0; Tid < MT_CodeTest.Count; Tid++)
            {

                string Package_Name = Packagenew_Bal_C.getPNameByCode((MT_CodeTest[Tid] as PatSt_Bal_C).PackageCode, Convert.ToInt32(Session["Branchid"])); //12

                TreeNode Obj_TrNO = tvGroupTree.FindNode((MT_CodeTest[Tid] as PatSt_Bal_C).PackageCode);
                if (Obj_TrNO == null)
                {
                    Obj_TrNO = new TreeNode(Package_Name);

                    Obj_TrNO.Checked = true;
                    Obj_TrNO.Value = (MT_CodeTest[Tid] as PatSt_Bal_C).PackageCode;
                    Obj_TrNO.NavigateUrl = "#";

                    tvGroupTree.Nodes.Add(Obj_TrNO);
                }

                ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((MT_CodeTest[Tid] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                IEnumerator IE_Test = IC_Test.GetEnumerator();
                while (IE_Test.MoveNext())
                {
                    MainTest_Bal_C MTB_C = IE_Test.Current as MainTest_Bal_C;
                    TreeNode TVN_Test = new TreeNode(MTB_C.Maintestname);
                    TVN_Test.Value = MTB_C.MTCode;
                    TVN_Test.Text = MTB_C.Maintestname;
                    TVN_Test.ToolTip = "Main test";
                    TVN_Test.NavigateUrl = "#";

                    TVN_Test.Checked = true;
                    Obj_TrNO.ChildNodes.Add(TVN_Test);
                }

                Obj_TrNO.Expand();
            }
            if (tvGroupTree.Nodes.Count == 0)
            {
                // cmdPrint.Visible = false;

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



    protected void Button2_Click(object sender, EventArgs e)
    {
        PReportWithBalance();
        //#region FillTree
        //tvGroupTree.Nodes.Clear();

        //ArrayList TestCode_Array = (ArrayList)PatSt_new_Bal_C.Get_Patst_Authorized_(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]).Trim(), Convert.ToInt32(Session["Branchid"]));
        //if (TestCode_Array.Count == 0)
        //{
        //    Label44.Text = "test is not  authorized...";
        //    Label44.Visible = true;
        //    return;
        //}
        //for (int Tid = 0; Tid < TestCode_Array.Count; Tid++)
        //{
        //    ICollection icol = SubdepartmentLogic_Bal_C.GetSubdepartment_byCode((TestCode_Array[Tid] as PatSt_Bal_C).SDCode, Convert.ToInt32(Session["Branchid"])); //12
        //    IEnumerator ie = icol.GetEnumerator();
        //    while (ie.MoveNext())
        //    {
        //        Subdepartment_Bal_C Sdb_C = (Subdepartment_Bal_C)ie.Current;
        //        TreeNode Obj_TrNO = tvGroupTree.FindNode(Sdb_C.SDCode);
        //        if (Obj_TrNO == null)
        //        {
        //            Obj_TrNO = new TreeNode(Sdb_C.SubdeptName);
        //            Obj_TrNO.Checked = true;
        //            Obj_TrNO.Value = Sdb_C.SDCode;
        //            Obj_TrNO.NavigateUrl = "#";
        //            tvGroupTree.Nodes.Add(Obj_TrNO);
        //        }
        //        ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((TestCode_Array[Tid] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
        //        IEnumerator IE_Test = IC_Test.GetEnumerator();
        //        while (IE_Test.MoveNext())
        //        {
        //            MainTest_Bal_C MTB_C = IE_Test.Current as MainTest_Bal_C;
        //            TreeNode TVN_Test = new TreeNode(MTB_C.Maintestname);
        //            TVN_Test.Value = MTB_C.MTCode;
        //            TVN_Test.Text = MTB_C.Maintestname;
        //            TVN_Test.ToolTip = "Main test";
        //            TVN_Test.NavigateUrl = "#";
        //            TVN_Test.Checked = true;
        //            Obj_TrNO.ChildNodes.Add(TVN_Test);
        //        }
        //        Obj_TrNO.Expand();
        //    }
        //}

        //ArrayList MT_CodeTest = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_ingroup(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]).Trim(), Convert.ToInt32(Session["Branchid"]));
        //for (int Tid = 0; Tid < MT_CodeTest.Count; Tid++)
        //{
        //    string Package_Name = Packagenew_Bal_C.getPNameByCode((MT_CodeTest[Tid] as PatSt_Bal_C).PackageCode, Convert.ToInt32(Session["Branchid"])); //12
        //    TreeNode Obj_TrNO = tvGroupTree.FindNode((MT_CodeTest[Tid] as PatSt_Bal_C).PackageCode);
        //    if (Obj_TrNO == null)
        //    {
        //        Obj_TrNO = new TreeNode(Package_Name);
        //        Obj_TrNO.Checked = true;
        //        Obj_TrNO.Value = (MT_CodeTest[Tid] as PatSt_Bal_C).PackageCode;
        //        Obj_TrNO.NavigateUrl = "#";
        //        tvGroupTree.Nodes.Add(Obj_TrNO);
        //    }
        //    ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((MT_CodeTest[Tid] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
        //    IEnumerator IE_Test = IC_Test.GetEnumerator();
        //    while (IE_Test.MoveNext())
        //    {
        //        MainTest_Bal_C MTB_C = IE_Test.Current as MainTest_Bal_C;
        //        TreeNode TVN_Test = new TreeNode(MTB_C.Maintestname);
        //        TVN_Test.Value = MTB_C.MTCode;
        //        TVN_Test.Text = MTB_C.Maintestname;
        //        TVN_Test.ToolTip = "Main test";
        //        TVN_Test.NavigateUrl = "#";

        //        TVN_Test.Checked = true;
        //        Obj_TrNO.ChildNodes.Add(TVN_Test);
        //    }
        //    Obj_TrNO.Expand();
        //}
        //if (tvGroupTree.Nodes.Count == 0)
        //{
        //    Button2.Enabled = false;
        //}
        //#endregion

        //string TT_Code = "";
        //string TextDesc = "";
        //string View_Code = "";
        //string View_TestDesc = "";
        //string ViewTestCode = "";
        

        //for (int i = 0; i <= tvGroupTree.Nodes.Count - 1; i++)
        //{
        //    if (tvGroupTree.Nodes[i].Checked && tvGroupTree.Nodes[i].Depth == 0)
        //    {
        //        for (int j = 0; j <= tvGroupTree.Nodes[i].ChildNodes.Count - 1; j++)
        //        {
        //            if (tvGroupTree.Nodes[i].ChildNodes[j].Checked && tvGroupTree.Nodes[i].ChildNodes[j].Depth == 1)
        //            {
        //                string h = tvGroupTree.Nodes[i].Value;
        //                string t = tvGroupTree.Nodes[i].ChildNodes[j].Value;

        //                MainTest_Bal_C MTB_C = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
        //                TT_Code = MTB_C.MTCode;
        //                TextDesc = MTB_C.TextDesc;
        //                View_Code = MTB_C.SDCode;
        //                PatSt_Bal_C PSB_C = new PatSt_Bal_C();
        //                PSB_C.UpdatePrintstatus(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]).Trim(), MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]),Convert.ToString(Session["username"].ToString()));


        //                //if (TextDesc == "DescType")
        //                //{
        //                //    if (View_TestDesc == "")
        //                //    {
        //                //        View_TestDesc = " (MTCode = '" + TT_Code;
        //                //    }
        //                //    else
        //                //    {
        //                //        View_TestDesc = View_TestDesc + "' OR MTCode = '" + TT_Code;
        //                //    }
        //                //}
        //                //else
        //                //    if (ViewTestCode == "")
        //                //    {
        //                //        ViewTestCode = " (MTCode = '" + TT_Code;
        //                //    }
        //                //    else
        //                //        ViewTestCode = ViewTestCode + "' OR MTCode = '" + TT_Code;

        //                if (TextDesc == "DescType")
        //                {
        //                    if (View_TestDesc == "")
        //                    {
        //                        View_TestDesc = " (VW_desfiledata.MTCode = '" + TT_Code;
        //                    }
        //                    else
        //                    {
        //                        View_TestDesc = View_TestDesc + "' OR VW_desfiledata.MTCode = '" + TT_Code;
        //                    }
        //                }
        //                else
        //                    if (ViewTestCode == "")
        //                    {
        //                        ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + TT_Code;
        //                    }
        //                    else
        //                        ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + TT_Code;

        //            }

        //        }
        //    }

        //}
        //bool DescFlag = false;
        //bool textflag = false;

        //if (View_TestDesc != "")
        //{
        //    View_TestDesc = View_TestDesc + "')";
        //    AlterView_VE_GetLabNo(Convert.ToString(ViewState["PatRegID"]));
        //    VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), View_TestDesc, Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]));

        //    ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_desfiledata_org.FID}='" + Convert.ToString(ViewState["FID"]).Trim() + "'";
        //    DescFlag = true;
        //}

        //if (ViewTestCode != "")
        //{
        //    ViewTestCode = ViewTestCode + "')";
        //    AlterView_VE_GetLabNo(lblRegNo.Text);
        //    VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]));

        //    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + Convert.ToString(ViewState["FID"]).Trim() + "'";
        //    textflag = true;
        //}

        //if (textflag == true && DescFlag == true)
        //{
        //    #region for Desc and Non desc

        //    string[] arrtlCodes = View_TestDesc.Split(',');
        //    string[] targetArr = new string[arrtlCodes.Length + 1];
        //    string[] urlArr = new string[arrtlCodes.Length + 1];
        //    string[] featuresArr = new string[arrtlCodes.Length + 1];

        //    CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        //    string formula = "", formula1 = "";
        //    selectonFormula = ReportParameterClass.SelectionFormula;
        //    ReportDocument CR = new ReportDocument();
        //    CR.Load(Server.MapPath("~/RptTestResultNoMemo_Patient.rpt"));
        //    SqlConnection con = DataAccess.ConInitForDC();

        //    SqlDataAdapter sda = null;
        //    DataTable dt = new DataTable();           
        //    sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con);

        //    sda.Fill(dt);

        //    CR.SetDataSource((System.Data.DataTable)dt);
        //    string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
        //    string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
        //    System.IO.File.WriteAllText(filename1, "");
        //    string exportedpath = "", selectionFormula = "";
        //    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"] + "'";
        //    ReportDocument crReportDocument = null;
        //    if (CR != null)
        //    {
        //        crReportDocument = (ReportDocument)CR;
        //    }
        //    CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
            
        //    int line = 10;
        //    pm.topMargin = 200 * line;
        //    CR.PrintOptions.ApplyPageMargins(pm);

        //    exportedpath = filename1;

        //    cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

        //    CR.Close();
        //    CR.Dispose();
        //    GC.Collect();

        //    ////////////

        //    path = "";
        //    rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
           
        //    int line1 = 10;
        //    int topMargin = 14 * line1;
         
        //    string filename22 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");
           
        //    SqlConnection con1 = DataAccess.ConInitForDC();
        //    SqlDataAdapter sda1 = null;
        //    DataTable dt1 = new DataTable();
           
        //    sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con1);
        //    sda1.Fill(dt1);

        //    ReportParameterClass.ReportType = "";

        //    rep.Close();
        //    rep.Dispose();
        //    GC.Collect();

        //    if (dt.Rows.Count == 0)
        //    {
        //        string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
        //        FileInfo fi = new FileInfo(filepath11);
        //        fi.Delete();
        //        Label44.Text = "Report Not Generated, Please Generate Once Again ";
        //        return;
        //    }
        //    if (dt1.Rows.Count == 0)
        //    {
        //        string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");

        //        FileInfo fi1 = new FileInfo(filepath11);
        //        fi1.Delete();
        //        Label44.Text = " Report Not Generated, Please Generate Once Again ";
        //        return;
        //    }
        //    string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
        //    string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

        //    string[] FilePathSplitOrg = OrgFile.Split('$');
        //    string[] FilePathSplitDup = DupFile.Split('$');

        //    if (FilePathSplitOrg[1] != FilePathSplitDup[1])
        //    {

        //        string pathh = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");

        //        foreach (string file in Directory.GetFiles(pathh))
        //        {
        //            string[] NewFile = file.Split('$');
        //            if (FilePathSplitOrg[1] != NewFile[1])
        //            {
        //                File.Delete(file);
        //            }
        //        }
        //    }
        //    urlArr[arrtlCodes.Length] = "Testnormalresult.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf";

        //    targetArr[arrtlCodes.Length] = "1";
        //    featuresArr[arrtlCodes.Length] = "";
        //    string OrgFileMemo = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");
        //    string DupFileMemo = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");

        //    string[] FilePathSplitOrgMemo = OrgFileMemo.Split('$');
        //    string[] FilePathSplitDupMemo = DupFileMemo.Split('$');

        //    if (FilePathSplitOrgMemo[1] != FilePathSplitDupMemo[1])
        //    {

        //        string pathh = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");

        //        foreach (string file in Directory.GetFiles(pathh))
        //        {
        //            string[] NewFile = file.Split('$');
        //            if (FilePathSplitOrg[1] != NewFile[1])
        //            {
        //                File.Delete(file);
        //            }
        //        }
        //    }

        //    urlArr[0] = "Redirect.aspx?rt=DescType&RepName=PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf";

        //    ResponseHelper.Redirect(urlArr, targetArr, featuresArr);

        //    #endregion
        //}
        //else if (textflag == true)
        //{
        //    #region Nondescriptive


        //    string formula = "";
        //    selectonFormula = ReportParameterClass.SelectionFormula;
        //    ReportDocument CR = new ReportDocument();
        //    CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt"));
        //    SqlConnection con = DataAccess.ConInitForDC();

        //    SqlDataAdapter sda = null;
        //    DataTable dt = new DataTable();
           
        //    sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con);

        //    sda.Fill(dt);

        //    CR.SetDataSource((System.Data.DataTable)dt);
        //    string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
        //    string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
        //    System.IO.File.WriteAllText(filename1, "");
        //    string exportedpath = "", selectionFormula = "";
        //    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"] + "'";
        //    ReportDocument crReportDocument = null;
        //    if (CR != null)
        //    {
        //        crReportDocument = (ReportDocument)CR;
        //    }
        //    CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
           
        //    int line = 10;
        //    pm.topMargin = 200 * line;
           
        //    exportedpath = filename1;

        //    cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

        //    CR.Close();
        //    CR.Dispose();
        //    GC.Collect();

        //    if (dt.Rows.Count == 0)
        //    {
        //        string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
        //        FileInfo fi = new FileInfo(filepath11);
        //        fi.Delete();
        //        Label44.Text = " Report Not Generated, Please Generate Once Again !!";
        //        return;
        //    }
        //    string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
        //    string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

        //    string[] FilePathSplitOrg = OrgFile.Split('$');
        //    string[] FilePathSplitDup = DupFile.Split('$');

        //    if (FilePathSplitOrg[1] != FilePathSplitDup[1])
        //    {

        //        foreach (string file in Directory.GetFiles(path))
        //        {
        //            string[] NewFile = file.Split('$');
        //            if (FilePathSplitOrg[1] != NewFile[1])
        //            {
        //                File.Delete(file);
        //            }
        //        }
        //    }
        //    Response.Redirect("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

        //    #endregion
        //}
        //else
        //{
            
        //    #region Only Nondescriptive

        //    string formula = "";
        //    selectonFormula = ReportParameterClass.SelectionFormula;
        //    ReportDocument CR = new ReportDocument();
        //    CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive.rpt"));
        //    SqlConnection con = DataAccess.ConInitForDC();

        //    SqlDataAdapter sda = null;
        //    DataTable dt = new DataTable();
         
        //    sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con);

        //    sda.Fill(dt);

        //    CR.SetDataSource((System.Data.DataTable)dt);
        //    string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
        //    string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
        //    System.IO.File.WriteAllText(filename1, "");
        //    string exportedpath = "", selectionFormula = "";
        //    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"].ToString() + "'";
        //    ReportDocument crReportDocument = null;
        //    if (CR != null)
        //    {
        //        crReportDocument = (ReportDocument)CR;
        //    }
        //    CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
         
        //    int line = 10;
        //    pm.topMargin = 200 * line;
           
        //    exportedpath = filename1;
        //    cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
        //    CR.Close();
        //    CR.Dispose();
        //    GC.Collect();

        //    if (dt.Rows.Count == 0)
        //    {
        //        string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
        //        FileInfo fi = new FileInfo(filepath11);
        //        fi.Delete();
        //        Label44.Text = "Report Not Generated, Please Generate Once Again ";
        //        return;
        //    }
        //    string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
        //    string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

        //    string[] FilePathSplitOrg = OrgFile.Split('$');
        //    string[] FilePathSplitDup = DupFile.Split('$');

        //    if (FilePathSplitOrg[1] != FilePathSplitDup[1])
        //    {
        //        foreach (string file in Directory.GetFiles(path))
        //        {
        //            string[] NewFile = file.Split('$');
        //            if (FilePathSplitOrg[1] != NewFile[1])
        //            {
        //                File.Delete(file);
        //            }
        //        }
        //    }
        //    Response.Redirect("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

        //    #endregion
        //}
    }
    public void AlterView_VE_GetLabNo(string PatRegID)
    {
        int i;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "ALTER VIEW [dbo].[VW_GetLabNo]AS (select  LabNo,PatRegID,MTCode,Branchid from patmstd  where  PatRegID='" + PatRegID + "' and  FID ='" + Convert.ToString(ViewState["FID"]) + "' )";
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

    public void patientdemo()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        conn.Open();
        SqlCommand cmd = new SqlCommand("SELECT c.*, cm.AmtPaid,cm.Discount, cm.DisFlag FROM dbo.patmst c LEFT OUTER JOIN dbo.Cshmst cm ON c.PID = cm.PID AND c.PatRegID = cm.PatRegID where c.PatRegID='" + Convert.ToString( ViewState["PatRegID"]) + "'", conn);
        try
        {
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lblpscname.Text = sdr["CenterName"].ToString(); ;
                lblname.Text = sdr["intial"].ToString().Trim() + " " + sdr["Patname"].ToString().Trim();
                lblRegNo.Text = sdr["PatRegID"].ToString();               
                ViewState["FID"] = sdr["FID"].ToString();
                lblage.Text = sdr["Age"].ToString();

                lbltestcharges.Text = sdr["TestCharges"].ToString();

                lblsex.Text = sdr["sex"].ToString();
                LblRefDoc.Text = sdr["RefDr"].ToString();
                lbldate.Text = sdr["Patregdate"].ToString();
                txtemark.Text = sdr["CallRemark"].ToString();
                ViewState["PID"] = sdr["PID"].ToString();
                txtpatientmail.Text = sdr["Email"].ToString();
               
            }
        }
        catch { }
        finally
        {
            conn.Close(); conn.Dispose();
        }
    }
    protected void btnclick_Click(object sender, EventArgs e)
    {
        //PatientOnline_Report();
    }
    protected void Chkemailtopatient_CheckedChanged(object sender, EventArgs e)
    {

        if (Chkemailtopatient.Checked == true)
        {
            Cshmst_Bal_C CSH_B = new Cshmst_Bal_C();
            int PID = -1;
            if (Convert.ToInt32(ViewState["PID"]) != -1)
            {
                PID = Convert.ToInt32(ViewState["PID"]);
            }
            CSH_B.getBalance( Convert.ToString( ViewState["PatRegID"]),Convert.ToString( ViewState["FID"]) , Convert.ToInt32(Session["Branchid"]), PID);
            if (CSH_B.Balance > 0)
            {
                Label6.Text = "Pending balance.";
                Label6.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
            }
            else
            {

                Patmst_Bal_C PB_C = new Patmst_Bal_C(ViewState["PatRegID"],Convert.ToString( ViewState["FID"]) , Convert.ToInt32(Session["Branchid"]));
                string pname = PB_C.Initial.Trim() + " " + PB_C.Patname;
                string mobile = PB_C.Phone;
                string email = PB_C.Email;
                string docemail = DrMT_Bal_C.GetEmaildrNameTable(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]));
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
                string TT_Code = "";
                string TextDesc = "";
                string View_Code = "";
                string View_TestDesc = "";
                string ViewTestCode = "";
                string testname = "";
                string shortname = "";
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
                                MainTest_Bal_C MTB_C = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
                                TT_Code = MTB_C.MTCode;
                                TextDesc = MTB_C.TextDesc;
                                View_Code = MTB_C.SDCode;
                                PatSt_Bal_C PSB_C = new PatSt_Bal_C();
                                PSB_C.UpdateEmailstatus(ViewState["PatRegID"].ToString(), ViewState["FID"].ToString(), MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"]));

                                //if (TextDesc == "DescType")
                                //{
                                //    if (View_TestDesc == "")
                                //    {
                                //        View_TestDesc = " (MTCode = '" + TT_Code;
                                //    }
                                //    else
                                //    {
                                //        View_TestDesc = View_TestDesc + "' OR MTCode = '" + TT_Code;
                                //    }
                                //}
                                //else
                                //    if (ViewTestCode == "")
                                //    {

                                //        ViewTestCode = " (MTCode = '" + TT_Code;
                                //    }
                                //    else
                                //        ViewTestCode = ViewTestCode + "' OR MTCode = '" + TT_Code;

                                if (TextDesc == "DescType")
                                {
                                    if (View_TestDesc == "")
                                    {
                                        View_TestDesc = " (VW_desfiledata.MTCode = '" + TT_Code;
                                    }
                                    else
                                    {
                                        View_TestDesc = View_TestDesc + "' OR VW_desfiledata.MTCode = '" + TT_Code;
                                    }
                                }
                                else
                                    if (ViewTestCode == "")
                                    {
                                        ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + TT_Code;
                                    }
                                    else
                                        ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + TT_Code;
                            }
                        }
                    }
                }
                bool DescFlag = false;
                bool textflag = false;

                if (View_TestDesc != "")
                {
                    View_TestDesc = View_TestDesc + "')";
                    VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), View_TestDesc, Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]));

                    ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + ViewState["PatRegID"].ToString() + "' and {VW_desfiledata_org.FID}='" + ViewState["FID"] .ToString() + "'";
                    DescFlag = true;
                }
                if (ViewTestCode != "")
                {
                    ViewTestCode = ViewTestCode + "')"; 
                    VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]));

                    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"] .ToString() + "'";
                    textflag = true;
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
                    sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + ViewState["PatRegID"].ToString() + "'  and FID='" + ViewState["FID"]  + "' ", con);
                    sda.Fill(dt);

                    CR.SetDataSource((System.Data.DataTable)dt);
                    string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                    string filename11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    System.IO.File.WriteAllText(filename11, "");
                    string exportedpath = "", selectionFormula = "";
                    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"] .ToString() + "'";
                    ReportDocument crReportDocument = null;
                    if (CR != null)
                    {
                        crReportDocument = (ReportDocument)CR;
                    }
                    CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                    //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                    int line = 10;
                    pm.topMargin = 200 * line;
                    exportedpath = filename11;
                    cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);


                    CR.Close();
                    CR.Dispose();
                    GC.Collect();

                    path = "";
                    rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                    
                    int line1 = 10;
                    int topMargin = 14 * line1;
                
                    SqlConnection con1 = DataAccess.ConInitForDC();
                    SqlDataAdapter sda1 = null;
                    DataTable dt1 = new DataTable();
                    //  DataSet1 dst1 = new DataSet1();
                    sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + ViewState["PatRegID"].ToString() + "'  and FID='" + ViewState["FID"]  + "' ", con1);

                    sda1.Fill(dt1);
                
                    string filename22 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                   
                    ReportParameterClass.ReportType = "";
                    rep.Close();
                    rep.Dispose();
                    GC.Collect();

                    if (dt.Rows.Count == 0)
                    {
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        Label44.Text = " Report Not Generated, Please Generate Once Again";
                        return;
                    }
                    if (dt1.Rows.Count == 0)
                    {
                        string filepath111 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));
                        Attachment att1 = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
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
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                            FileInfo fi = new FileInfo(filepath11);
                            fi.Delete();
                            string filepath111 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
                else if (textflag == true)
                {
                    #region Nondescriptive
                    string formula = "";
                    selectonFormula = ReportParameterClass.SelectionFormula;
                    ReportDocument CR = new ReportDocument();
                    CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                    SqlConnection con = DataAccess.ConInitForDC();

                    SqlDataAdapter sda = null;
                    DataTable dt = new DataTable();
                   
                    sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + ViewState["PatRegID"].ToString() + "'  and FID='" + ViewState["FID"]  + "' ", con);

                    sda.Fill(dt);
                    CR.SetDataSource((System.Data.DataTable)dt);
                    string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                    string filename11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    System.IO.File.WriteAllText(filename11, "");
                    string exportedpath = "", selectionFormula = "";
                    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"] .ToString() + "'";
                    ReportDocument crReportDocument = null;
                    if (CR != null)
                    {
                        crReportDocument = (ReportDocument)CR;
                    }
                    CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
                    //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
                    int line = 10;
                    pm.topMargin = 200 * line;
                  
                    exportedpath = filename11;
                    cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);
                    CR.Close();
                    CR.Dispose();
                    GC.Collect();

                    if (dt.Rows.Count == 0)
                    {
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));

                        

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
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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
                    CR.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive_Email.rpt"));
                    SqlConnection con = DataAccess.ConInitForDC();

                    SqlDataAdapter sda = null;
                    DataTable dt = new DataTable();
                   
                    sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + ViewState["PatRegID"].ToString() + "'  and FID='" + ViewState["FID"]  + "' ", con);

                    sda.Fill(dt);
                    string filename22 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                    CR.SetDataSource((System.Data.DataTable)dt);
                    string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                    string filename11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                    System.IO.File.WriteAllText(filename11, "");

                    ReportParameterClass.ReportType = "";

                    rep.Close();
                    rep.Dispose();
                    GC.Collect();
                    if (dt.Rows.Count == 0)
                    {
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
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
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
        }
    }
    public void DeleteFile(string folderName, string ReportType)
    {
        string OrgFile = Server.MapPath(folderName + "//" + "$" + Date1 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + ReportType + ".pdf");
        string DupFile = Server.MapPath(folderName + "//" + "$" + Date2 + "$" + ViewState["PatRegID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + ReportType + ".pdf");

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

    protected void txtpatientmail_TextChanged(object sender, EventArgs e)
    {
        if (txtpatientmail.Text != "")
        {
            Patmst_Bal_C Objpbc = new Patmst_Bal_C();
            Objpbc.Email = txtpatientmail.Text;
            Objpbc.Update_EmailId(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]));
            this.Chkemailtopatient_CheckedChanged(null, null);
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

    protected void btnaction_Click(object sender, EventArgs e)
    {
        if (chkiscall.Checked == true)
        {
            Patmst_New_Bal_C.Update_callStatus(Convert.ToString(ViewState["PatRegID"]), txtemark.Text, true);
        }
        else
        {
            Patmst_New_Bal_C.Update_callStatus(Convert.ToString(ViewState["PatRegID"]), txtemark.Text, false);

        }
        this.btnaction.Attributes.Add("onclick", "window.close();");
    }
    protected void chkiscall_CheckedChanged(object sender, EventArgs e)
    {
        if (chkiscall.Checked == true)
        {
            this.btnaction_Click(null, null);
        }
    }
    protected void btnprintBalance_Click(object sender, EventArgs e)
    {

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
        #region FillTree
        tvGroupTree.Nodes.Clear();

        ArrayList TestCode_Array = (ArrayList)PatSt_new_Bal_C.Get_Patst_Authorized_(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]).Trim(), Convert.ToInt32(Session["Branchid"]));
        if (TestCode_Array.Count == 0)
        {
            Label44.Text = "test is not  authorized...";
            Label44.Visible = true;
            return;
        }
        for (int Tid = 0; Tid < TestCode_Array.Count; Tid++)
        {
            ICollection icol = SubdepartmentLogic_Bal_C.GetSubdepartment_byCode((TestCode_Array[Tid] as PatSt_Bal_C).SDCode, Convert.ToInt32(Session["Branchid"])); //12
            IEnumerator ie = icol.GetEnumerator();
            while (ie.MoveNext())
            {
                Subdepartment_Bal_C Sdb_C = (Subdepartment_Bal_C)ie.Current;
                TreeNode Obj_TrNO = tvGroupTree.FindNode(Sdb_C.SDCode);
                if (Obj_TrNO == null)
                {
                    Obj_TrNO = new TreeNode(Sdb_C.SubdeptName);
                    Obj_TrNO.Checked = true;
                    Obj_TrNO.Value = Sdb_C.SDCode;
                    Obj_TrNO.NavigateUrl = "#";
                    tvGroupTree.Nodes.Add(Obj_TrNO);
                }
                ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((TestCode_Array[Tid] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                IEnumerator IE_Test = IC_Test.GetEnumerator();
                while (IE_Test.MoveNext())
                {
                    MainTest_Bal_C MTB_C = IE_Test.Current as MainTest_Bal_C;
                    TreeNode TVN_Test = new TreeNode(MTB_C.Maintestname);
                    TVN_Test.Value = MTB_C.MTCode;
                    TVN_Test.Text = MTB_C.Maintestname;
                    TVN_Test.ToolTip = "Main test";
                    TVN_Test.NavigateUrl = "#";
                    TVN_Test.Checked = true;
                    Obj_TrNO.ChildNodes.Add(TVN_Test);
                }
                Obj_TrNO.Expand();
            }
        }

        ArrayList MT_CodeTest = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_ingroup(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]).Trim(), Convert.ToInt32(Session["Branchid"]));
        for (int Tid = 0; Tid < MT_CodeTest.Count; Tid++)
        {
            string Package_Name = Packagenew_Bal_C.getPNameByCode((MT_CodeTest[Tid] as PatSt_Bal_C).PackageCode, Convert.ToInt32(Session["Branchid"])); //12
            TreeNode Obj_TrNO = tvGroupTree.FindNode((MT_CodeTest[Tid] as PatSt_Bal_C).PackageCode);
            if (Obj_TrNO == null)
            {
                Obj_TrNO = new TreeNode(Package_Name);
                Obj_TrNO.Checked = true;
                Obj_TrNO.Value = (MT_CodeTest[Tid] as PatSt_Bal_C).PackageCode;
                Obj_TrNO.NavigateUrl = "#";
                tvGroupTree.Nodes.Add(Obj_TrNO);
            }
            ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((MT_CodeTest[Tid] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
            IEnumerator IE_Test = IC_Test.GetEnumerator();
            while (IE_Test.MoveNext())
            {
                MainTest_Bal_C MTB_C = IE_Test.Current as MainTest_Bal_C;
                TreeNode TVN_Test = new TreeNode(MTB_C.Maintestname);
                TVN_Test.Value = MTB_C.MTCode;
                TVN_Test.Text = MTB_C.Maintestname;
                TVN_Test.ToolTip = "Main test";
                TVN_Test.NavigateUrl = "#";

                TVN_Test.Checked = true;
                Obj_TrNO.ChildNodes.Add(TVN_Test);
            }
            Obj_TrNO.Expand();
        }
        if (tvGroupTree.Nodes.Count == 0)
        {
            Button2.Enabled = false;
        }
        #endregion

        string TT_Code = "";
        string TextDesc = "";
        string View_Code = "";
        string View_TestDesc = "";
        string ViewTestCode = "";


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

                        MainTest_Bal_C MTB_C = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
                        TT_Code = MTB_C.MTCode;
                        TextDesc = MTB_C.TextDesc;
                        View_Code = MTB_C.SDCode;
                        PatSt_Bal_C PSB_C = new PatSt_Bal_C();
                        PSB_C.UpdatePrintstatus(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]).Trim(), MTB_C.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"].ToString()));


                        //if (TextDesc == "DescType")
                        //{
                        //    if (View_TestDesc == "")
                        //    {
                        //        View_TestDesc = " (MTCode = '" + TT_Code;
                        //    }
                        //    else
                        //    {
                        //        View_TestDesc = View_TestDesc + "' OR MTCode = '" + TT_Code;
                        //    }
                        //}
                        //else
                        //    if (ViewTestCode == "")
                        //    {
                        //        ViewTestCode = " (MTCode = '" + TT_Code;
                        //    }
                        //    else
                        //        ViewTestCode = ViewTestCode + "' OR MTCode = '" + TT_Code;

                        if (TextDesc == "DescType")
                        {
                            if (View_TestDesc == "")
                            {
                                View_TestDesc = " (VW_desfiledata.MTCode = '" + TT_Code;
                            }
                            else
                            {
                                View_TestDesc = View_TestDesc + "' OR VW_desfiledata.MTCode = '" + TT_Code;
                            }
                        }
                        else
                            if (ViewTestCode == "")
                            {
                                ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + TT_Code;
                            }
                            else
                                ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + TT_Code;

                    }

                }
            }

        }
        bool DescFlag = false;
        bool textflag = false;

        if (View_TestDesc != "")
        {
            View_TestDesc = View_TestDesc + "')";
            AlterView_VE_GetLabNo(Convert.ToString(ViewState["PatRegID"]));
            VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), View_TestDesc, Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]));

            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_desfiledata_org.FID}='" + Convert.ToString(ViewState["FID"]).Trim() + "'";
            DescFlag = true;
        }

        if (ViewTestCode != "")
        {
            ViewTestCode = ViewTestCode + "')";
            AlterView_VE_GetLabNo(lblRegNo.Text);
            VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]));

            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + Convert.ToString(ViewState["FID"]).Trim() + "'";
            textflag = true;
        }

        if (textflag == true && DescFlag == true)
        {
            #region for Desc and Non desc

            string[] arrtlCodes = View_TestDesc.Split(',');
            string[] targetArr = new string[arrtlCodes.Length + 1];
            string[] urlArr = new string[arrtlCodes.Length + 1];
            string[] featuresArr = new string[arrtlCodes.Length + 1];

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/RptTestResultNoMemo_Patient.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"] + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;

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

            int line1 = 10;
            int topMargin = 14 * line1;

            string filename22 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");

            SqlConnection con1 = DataAccess.ConInitForDC();
            SqlDataAdapter sda1 = null;
            DataTable dt1 = new DataTable();

            sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con1);
            sda1.Fill(dt1);

            ReportParameterClass.ReportType = "";

            rep.Close();
            rep.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            if (dt1.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");

                FileInfo fi1 = new FileInfo(filepath11);
                fi1.Delete();
                Label44.Text = " Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
            urlArr[arrtlCodes.Length] = "Testnormalresult.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf";

            targetArr[arrtlCodes.Length] = "1";
            featuresArr[arrtlCodes.Length] = "";
            string OrgFileMemo = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");
            string DupFileMemo = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");

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

            urlArr[0] = "Redirect.aspx?rt=DescType&RepName=PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf";

            ResponseHelper.Redirect(urlArr, targetArr, featuresArr);

            #endregion
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
            DataTable dt = new DataTable();

            sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"] + "'";
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

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = " Report Not Generated, Please Generate Once Again !!";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

            #endregion
        }
        else
        {

            #region Only Nondescriptive

            string formula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();

            sda = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"].ToString() + "'";
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

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

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
            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            #endregion
        }
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