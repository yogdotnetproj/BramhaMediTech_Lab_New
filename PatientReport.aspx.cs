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

public partial class PatientReport : System.Web.UI.Page
{
    int g;
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string Date2 = DateTime.Now.AddDays(-1).ToString("ddMMyyyy");
    string rptname = "", path = "", selectonFormula = "";
    Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();
    Patmst_New_Bal_C PatNBC = new Patmst_New_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
            Button2.Visible = false;
            PatientOnline_Report();
            SqlConnection conn = DataAccess.ConInitForDC(); 
            conn.Open();
            SqlCommand cmd = new SqlCommand(" SELECT c.*,dbo.FUN_GetReceiveAmt(1, c.PID) as AmtPaid ,dbo.FUN_GetReceiveAmt_Discount(1, c.PID) as Discount, 0 as DisFlag, c.intial + ' ' + c.Patname AS FullName  FROM dbo.patmst c LEFT OUTER JOIN dbo.RecM cm  ON c.PID = cm.PID where c.Patusername=  N'" + Session["username"].ToString() + "' ", conn);
            try
            {
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    lblpscname.Text = sdr["CenterName"].ToString(); ;
                    lblname.Text = sdr["intial"].ToString().Trim() + " " + sdr["Patname"].ToString().Trim();
                    lblregno.Text = sdr["PatRegID"].ToString();
                
                    ViewState["FID"] = sdr["FID"].ToString();
                    ViewState["PID"] = sdr["PID"].ToString();
                    lblage.Text = sdr["Age"].ToString();
                 
                    lbltestcharges.Text = sdr["TestCharges"].ToString();
                    
                    lblsex.Text = sdr["sex"].ToString();
                    LblRefDoc.Text = sdr["RefDr"].ToString();
                    lbldate.Text = sdr["Patregdate"].ToString();

                   
                }
            }
            catch { }
            finally
            {
                conn.Close(); conn.Dispose();
            }
          
        }
    }
    public void PatientOnline_Report()
    {
        DataTable dton = new DataTable();
        dton = PatNBC.Get_PatientOnline_Report(Session["username"].ToString());
        if (dton.Rows.Count > 0)
        {
            GVTestentry.DataSource = dton;
            GVTestentry.DataBind();
        }

    }
    public void bindtreeview( string PatRegID, string FID)
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
            ArrayList Obj_Arr_L_Test = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_notingroup(PatRegID, Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
            for (int ID_Test = 0; ID_Test < Obj_Arr_L_Test.Count; ID_Test++)
            {

                ICollection icol = SubdepartmentLogic_Bal_C.GetSubdepartment_byCode((Obj_Arr_L_Test[ID_Test] as PatSt_Bal_C).SDCode, Convert.ToInt32(Session["Branchid"])); //12
                IEnumerator ie = icol.GetEnumerator();
                while (ie.MoveNext())
                {


                    Subdepartment_Bal_C Obj_SBC = (Subdepartment_Bal_C)ie.Current;
                    TreeNode Obj_TN = tvGroupTree.FindNode(Obj_SBC.SDCode);
                    if (Obj_TN == null)
                    {
                        Obj_TN = new TreeNode(Obj_SBC.SubdeptName);
                        Obj_TN.Checked = true;

                        Obj_TN.Value = Obj_SBC.SDCode;
                        Obj_TN.NavigateUrl = "#";

                        tvGroupTree.Nodes.Add(Obj_TN);
                    }

                    ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((Obj_Arr_L_Test[ID_Test] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                    IEnumerator IE_Test = IC_Test.GetEnumerator();

                    while (IE_Test.MoveNext())
                    {

                        MainTest_Bal_C Obj_MTB = IE_Test.Current as MainTest_Bal_C;
                        TreeNode Obj_TNV = new TreeNode(Obj_MTB.Maintestname);
                        Obj_TNV.Value = Obj_MTB.MTCode;

                        bool PStatus = (Obj_Arr_L_Test[ID_Test] as PatSt_Bal_C).Patrepstatus;
                        if (PStatus == true)
                        {
                            Obj_TNV.Text = Obj_MTB.Maintestname + "    <span class='btn btn-sm btn-primary'>(Printed" + ")</sapn>";
                            //Obj_TNV.Checked = false;
                            Obj_TNV.Checked = true;
                        }
                        else
                        {
                            Obj_TNV.Text = Obj_MTB.Maintestname;
                            Obj_TNV.Checked = true;
                        }
                        Obj_TNV.ToolTip = "Main test";
                        Obj_TNV.NavigateUrl = "#";


                        Obj_TN.ChildNodes.Add(Obj_TNV);

                    }

                    Obj_TN.Expand();
                }
            }

            ArrayList Obj_Test_ARL = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_ingroup_new(PatRegID, Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
            for (int ID_Test = 0; ID_Test < Obj_Test_ARL.Count; ID_Test++)
            {

                string Package_Name = Packagenew_Bal_C.getPNameByCode((Obj_Test_ARL[ID_Test] as PatSt_Bal_C).PackageCode, Convert.ToInt32(Session["Branchid"])); //12

                TreeNode Obj_TN = tvGroupTree.FindNode((Obj_Test_ARL[ID_Test] as PatSt_Bal_C).PackageCode);
                if (Obj_TN == null)
                {
                    Obj_TN = new TreeNode(Package_Name);

                    Obj_TN.Checked = true;
                    Obj_TN.Value = (Obj_Test_ARL[ID_Test] as PatSt_Bal_C).PackageCode;
                    Obj_TN.NavigateUrl = "#";

                    tvGroupTree.Nodes.Add(Obj_TN);
                }

                ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((Obj_Test_ARL[ID_Test] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                IEnumerator IE_Test = IC_Test.GetEnumerator();
                while (IE_Test.MoveNext())
                {
                    MainTest_Bal_C Obj_MTB = IE_Test.Current as MainTest_Bal_C;
                    TreeNode Obj_TNV = new TreeNode(Obj_MTB.Maintestname);
                    Obj_TNV.Value = Obj_MTB.MTCode;
                    Obj_TNV.Text = Obj_MTB.Maintestname;
                    Obj_TNV.ToolTip = "Main test";
                    Obj_TNV.NavigateUrl = "#";

                    Obj_TNV.Checked = true;
                    Obj_TN.ChildNodes.Add(Obj_TNV);
                }

                Obj_TN.Expand();
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
        #region FillTree
        TreeviewBind_C ObjTB = new TreeviewBind_C();
        DataTable dtban = new DataTable();
       
        dtban = ObjTB.Bindbanner();
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
        Cshmst_Bal_C Cshmst = new Cshmst_Bal_C();
        Cshmst.getBalance(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]).Trim(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(ViewState["PID"]));
        float BAL_Amount = Cshmst.Balance;
        if (ViewState["VALIDATE"] == "YES")
        {
            BAL_Amount = 0;
        }
      
          if (BAL_Amount > 0)
         {
            
             Button2.Visible = false;
             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
             return;
         }
        tvGroupTree.Nodes.Clear();

        ArrayList Obj_Arr_L_Test = (ArrayList)PatSt_new_Bal_C.Get_Patst_Authorized_(Convert.ToString( ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]).Trim(), Convert.ToInt32(Session["Branchid"]));
        if (Obj_Arr_L_Test.Count == 0)
        {
            Label44.Text = "test is not  authorized...";
            Label44.Visible = true;
            return;
        }
        for (int ID_Test = 0; ID_Test < Obj_Arr_L_Test.Count; ID_Test++)
        {
            ICollection icol = SubdepartmentLogic_Bal_C.GetSubdepartment_byCode((Obj_Arr_L_Test[ID_Test] as PatSt_Bal_C).SDCode, Convert.ToInt32(Session["Branchid"])); //12
            IEnumerator ie = icol.GetEnumerator();
            while (ie.MoveNext())
            {
                Subdepartment_Bal_C Obj_SBC = (Subdepartment_Bal_C)ie.Current;
                TreeNode Obj_TN = tvGroupTree.FindNode(Obj_SBC.SDCode);
                if (Obj_TN == null)
                {
                    Obj_TN = new TreeNode(Obj_SBC.SubdeptName);
                    Obj_TN.Checked = true;
                    Obj_TN.Value = Obj_SBC.SDCode;
                    Obj_TN.NavigateUrl = "#";
                    tvGroupTree.Nodes.Add(Obj_TN);
                }
                ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((Obj_Arr_L_Test[ID_Test] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                IEnumerator IE_Test = IC_Test.GetEnumerator();
                while (IE_Test.MoveNext())
                {
                    MainTest_Bal_C Obj_MTB = IE_Test.Current as MainTest_Bal_C;
                    TreeNode Obj_TNV = new TreeNode(Obj_MTB.Maintestname);
                    Obj_TNV.Value = Obj_MTB.MTCode;
                    Obj_TNV.Text = Obj_MTB.Maintestname;
                    Obj_TNV.ToolTip = "Main test";
                    Obj_TNV.NavigateUrl = "#";
                    Obj_TNV.Checked = true;
                    Obj_TN.ChildNodes.Add(Obj_TNV);
                }
                Obj_TN.Expand();
            }
        }

        ArrayList Obj_Test_ARL = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_ingroup(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]).Trim(), Convert.ToInt32(Session["Branchid"]));
        for (int ID_Test = 0; ID_Test < Obj_Test_ARL.Count; ID_Test++)
        {
            string Package_Name = Packagenew_Bal_C.getPNameByCode((Obj_Test_ARL[ID_Test] as PatSt_Bal_C).PackageCode, Convert.ToInt32(Session["Branchid"])); //12
            TreeNode Obj_TN = tvGroupTree.FindNode((Obj_Test_ARL[ID_Test] as PatSt_Bal_C).PackageCode);
            if (Obj_TN == null)
            {
                Obj_TN = new TreeNode(Package_Name);
                Obj_TN.Checked = true;
                Obj_TN.Value = (Obj_Test_ARL[ID_Test] as PatSt_Bal_C).PackageCode;
                Obj_TN.NavigateUrl = "#";
                tvGroupTree.Nodes.Add(Obj_TN);
            }
            ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((Obj_Test_ARL[ID_Test] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
            IEnumerator IE_Test = IC_Test.GetEnumerator();
            while (IE_Test.MoveNext())
            {
                MainTest_Bal_C Obj_MTB = IE_Test.Current as MainTest_Bal_C;
                TreeNode Obj_TNV = new TreeNode(Obj_MTB.Maintestname);
                Obj_TNV.Value = Obj_MTB.MTCode;
                Obj_TNV.Text = Obj_MTB.Maintestname;
                Obj_TNV.ToolTip = "Main test";
                Obj_TNV.NavigateUrl = "#";
                                     
                Obj_TNV.Checked = true;
                Obj_TN.ChildNodes.Add(Obj_TNV);
            }           
            Obj_TN.Expand();          
        }
        if (tvGroupTree.Nodes.Count == 0)
        {
            Button2.Enabled = false;
        }
        #endregion

        string T_Code = "";
        string TextDesc = "";
        string View_Code = "";
        string View_TestDesc = "";
        string ViewTestCode = "";
        if (lblregno.Text != "" &&  ViewState["FID"]  != "")
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
                        string id = tvGroupTree.Nodes[i].ChildNodes[j].Value;

                        MainTest_Bal_C Obj_MTBC = new MainTest_Bal_C(id, Convert.ToInt32(Session["Branchid"]));
                        T_Code = Obj_MTBC.MTCode;
                        TextDesc = Obj_MTBC.TextDesc;
                        View_Code = Obj_MTBC.SDCode;
                        PatSt_Bal_C pt = new PatSt_Bal_C();
                        pt.UpdatePrintstatus(Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]).Trim(), Obj_MTBC.MTCode, Convert.ToInt32(Session["Branchid"]),Convert.ToString(Session["username"].ToString()));


                        //if (TextDesc == "DescType")
                        //{
                        //    if (View_TestDesc == "")
                        //    {
                        //        View_TestDesc = " (MTCode = '" + T_Code;
                        //    }
                        //    else
                        //    {
                        //        View_TestDesc = View_TestDesc + "' OR MTCode = '" + T_Code;
                        //    }
                        //}
                        //else
                        //    if (ViewTestCode == "")
                        //    {
                        //        ViewTestCode = " (MTCode = '" + T_Code;
                        //    }
                        //    else
                        //        ViewTestCode = ViewTestCode + "' OR MTCode = '" + T_Code;

                        if (TextDesc == "DescType")
                        {
                            if (View_TestDesc == "")
                            {
                                View_TestDesc = " (VW_desfiledata.MTCode = '" + T_Code;
                            }
                            else
                            {
                                View_TestDesc = View_TestDesc + "' OR VW_desfiledata.MTCode = '" + T_Code;
                            }
                        }
                        else
                            if (ViewTestCode == "")
                            {
                                ViewTestCode = " (VW_patdatarutinevw.MTCode = '" + T_Code;
                            }
                            else
                                ViewTestCode = ViewTestCode + "' OR VW_patdatarutinevw.MTCode = '" + T_Code;

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
            VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), View_TestDesc,Convert.ToString(ViewState["PatRegID"]), Convert.ToString(ViewState["FID"]));

            ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_desfiledata_org.FID}='" + Convert.ToString(ViewState["FID"]).Trim() + "'";
            DescFlag = true;
        }

        if (ViewTestCode != "")
        {
            ViewTestCode = ViewTestCode + "')";
            AlterView_VE_GetLabNo(lblregno.Text);
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
            CR.Load(Server.MapPath("~/Pateintreportnondescriptive_email.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
           // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con);

            sda.Fill(dt);
            
            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"] + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
           // int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());

            //int line = 10;
            //pm.topMargin = 200 * line;
            //CR.PrintOptions.ApplyPageMargins(pm);

            exportedpath = filename1;

            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

            CR.Close();
            CR.Dispose();
            GC.Collect();

            ////////////

            path = "";
            rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            //RptTestResultMemo_Email objRptTestResultMemo_Email = new RptTestResultMemo_Email();
            //PdfExportOptions pdfOptions = objRptTestResultMemo_Email.ExportOptions.Pdf;

           // int line1 = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());
            int line1 = 10;
            int topMargin = 14 * line1;
          //  objRptTestResultMemo_Email.Margins = new System.Drawing.Printing.Margins(50, 0, topMargin, 0);
             string filename22 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");
           // objRptTestResultMemo_Email.ExportToPdf(filename22);


            SqlConnection con1 = DataAccess.ConInitForDC();
            SqlDataAdapter sda1 = null;
            DataTable dt1 = new DataTable();
           // DataSet1 dst1 = new DataSet1();
            sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con1);
            sda1.Fill(dt1);

            ReportParameterClass.ReportType = "";

            rep.Close();
            rep.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                 string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            if (dt1.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");

                FileInfo fi1 = new FileInfo(filepath11);
                fi1.Delete();
                Label44.Text = " Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
            urlArr[arrtlCodes.Length] = "Testnormalresult.aspx?rt=NonDesc&RepName=PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf";

            targetArr[arrtlCodes.Length] = "1";
            featuresArr[arrtlCodes.Length] = "";
            string OrgFileMemo = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");
            string DupFileMemo = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf");

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
          
            urlArr[0] = "Redirect.aspx?rt=DescType&RepName=PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descr" + ".pdf";

            ResponseHelper.Redirect(urlArr, targetArr, featuresArr);

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
            //DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where PatRegID='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con);

            sda.Fill(dt);
           
            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.PatRegID}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"] + "'";
            ReportDocument crReportDocument = null;
            if (CR != null)
            {
                crReportDocument = (ReportDocument)CR;
            }
            CrystalDecisions.Shared.PageMargins pm = CR.PrintOptions.PageMargins;
            //int line = Uniquemethod_Bal_C.getnooflines(Session["Branchid"].ToString());

            //int line = 0;
            //pm.topMargin = 200 * line;
            //CR.PrintOptions.ApplyPageMargins(pm);

            exportedpath = filename1;

            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

            CR.Close();
            CR.Dispose();
            GC.Collect();

            if (dt.Rows.Count == 0)
            {
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = " Report Not Generated, Please Generate Once Again !!";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

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
            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" +  ViewState["FID"]  + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Nondescriptive" + ".pdf");

            #endregion
        }
        else
        {
            
            #region Only Nondescriptive

            string formula = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
            ReportDocument CR = new ReportDocument();
            CR.Load(Server.MapPath("~/DiagnosticReport/Pateintreportdescriptive_Email.rpt"));
            SqlConnection con = DataAccess.ConInitForDC();

            SqlDataAdapter sda = null;
            DataTable dt = new DataTable();
            // DataSet1 dst = new DataSet1();
            sda = new SqlDataAdapter("select * from VW_desfiledata_org where Patregid='" + ViewState["PatRegID"] + "'  and FID='" + ViewState["FID"] + "' ", con);

            sda.Fill(dt);

            CR.SetDataSource((System.Data.DataTable)dt);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + ViewState["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.Patregid}='" + ViewState["PatRegID"] + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"].ToString() + "'";
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
                string filepath11 = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + ViewState["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
                FileInfo fi = new FileInfo(filepath11);
                fi.Delete();
                Label44.Text = "Report Not Generated, Please Generate Once Again ";
                return;
            }
            string OrgFile = Server.MapPath("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + ViewState["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");
            string DupFile = Server.MapPath("PrintReport//" + "$" + Date2 + "$" + ViewState["PatRegID"] + "_" + ViewState["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

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
            Response.Redirect("PrintReport//" + "$" + Date1 + "$" + ViewState["PatRegID"] + "_" + ViewState["FID"].ToString() + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "Descriptive" + ".pdf");

            #endregion
        }
    }
    public void AlterView_VE_GetLabNo(string PatRegID)
    {
        int i;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "ALTER VIEW [dbo].[VW_GetLabNo]AS (select  LabNo,PatRegID,MTCode,Branchid from patmstd  where  PatRegID='" + PatRegID + "'and  FID ='" + Convert.ToString(ViewState["FID"]) + "'  )";
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
    protected void GVTestentry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GVTestentry_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GVTestentry_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (e.NewEditIndex == -1)
                return;
            int i = e.NewEditIndex;
            tvGroupTree.Nodes.Clear();
            
            string rno = (GVTestentry.Rows[e.NewEditIndex].FindControl("hdnRegNo") as HiddenField).Value;
            string FID = (GVTestentry.Rows[e.NewEditIndex].FindControl("hdnFid") as HiddenField).Value;
                  ViewState["PatRegID"] = rno;
            ViewState["FID"] = FID;
            bindtreeview(rno, FID);
            this.Button2_Click(null, null);
            if (tvGroupTree.Nodes.Count == 0)
            {
                Button2.Visible = false;
                Label44.Text = "test is not  authorized...";
                Label44.Visible = true;
                e.Cancel = true;
                return;
            }
            else
            {
                Label44.Text = "";
                //Button2.Visible = true;
                e.Cancel = true;
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
    protected void GVTestentry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx",false);
        //ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.open('close.html', '_self', null);", true);

    }
}