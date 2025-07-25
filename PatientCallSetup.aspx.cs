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
    string selectonFormula = "";
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
            
            fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
            todate.Text = Date.getdate().ToString("dd/MM/yyyy"); 
            Button2.Visible = false;
            PatientOnline_Report();


        }
    }
    public void PatientOnline_Report()
    {
        string Call = "";
        DataTable dton = new DataTable();
        if (RblCallStatus.SelectedItem.Text == "Call Done")
        {
            Call = "Done";
        }
        else if (RblCallStatus.SelectedItem.Text == "Viber")
        {
            Call = "Viber";
        }
        else if (RblCallStatus.SelectedItem.Text == "Whatsapp")
        {
            Call = "Whatsapp";
        }
        else
        {
            Call = "NotDone";
        }
        dton = PatNBC.Get_PatientCallDetaiis_Report(Txt_Patientname.Text, fromdate.Text, todate.Text, Call);
        GVTestentry.DataSource = dton;
        GVTestentry.DataBind();


    }
    public void bindtreeview(string RegNo, string FID)
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
            ArrayList Test_Code = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_notingroup(RegNo, Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
            for (int AL_ID = 0; AL_ID < Test_Code.Count; AL_ID++)
            {

                ICollection icol = SubdepartmentLogic_Bal_C.GetSubdepartment_byCode((Test_Code[AL_ID] as PatSt_Bal_C).SDCode, Convert.ToInt32(Session["Branchid"])); //12
                IEnumerator ie = icol.GetEnumerator();
                while (ie.MoveNext())
                {


                    Subdepartment_Bal_C SDB_C = (Subdepartment_Bal_C)ie.Current;
                    TreeNode ObjTN = tvGroupTree.FindNode(SDB_C.SDCode);
                    if (ObjTN == null)
                    {
                        ObjTN = new TreeNode(SDB_C.SubdeptName);
                        ObjTN.Checked = true;

                        ObjTN.Value = SDB_C.SDCode;
                        ObjTN.NavigateUrl = "#";

                        tvGroupTree.Nodes.Add(ObjTN);
                    }

                    ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((Test_Code[AL_ID] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                    IEnumerator IE_Test = IC_Test.GetEnumerator();

                    while (IE_Test.MoveNext())
                    {

                        MainTest_Bal_C MB_C = IE_Test.Current as MainTest_Bal_C;
                        TreeNode TV_Test = new TreeNode(MB_C.Maintestname);
                        TV_Test.Value = MB_C.MTCode;

                        bool PStatus = (Test_Code[AL_ID] as PatSt_Bal_C).Patrepstatus;
                        if (PStatus == true)
                        {
                            TV_Test.Text = MB_C.Maintestname + "    <span class='btn btn-sm btn-primary'>(Printed" + ")</sapn>";
                            TV_Test.Checked = false;
                        }
                        else
                        {
                            TV_Test.Text = MB_C.Maintestname;
                            TV_Test.Checked = true;
                        }
                        TV_Test.ToolTip = "Main test";
                        TV_Test.NavigateUrl = "#";


                        ObjTN.ChildNodes.Add(TV_Test);

                    }

                    ObjTN.Expand();
                }
            }

            ArrayList AL_Test = (ArrayList)PatSt_new_Bal_C.GetPatst_Authorized_ingroup_new(RegNo, Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdept);
            for (int AL_ID = 0; AL_ID < AL_Test.Count; AL_ID++)
            {

                string Package_Name = Packagenew_Bal_C.getPNameByCode((AL_Test[AL_ID] as PatSt_Bal_C).PackageCode, Convert.ToInt32(Session["Branchid"])); //12

                TreeNode ObjTN = tvGroupTree.FindNode((AL_Test[AL_ID] as PatSt_Bal_C).PackageCode);
                if (ObjTN == null)
                {
                    ObjTN = new TreeNode(Package_Name);

                    ObjTN.Checked = true;
                    ObjTN.Value = (AL_Test[AL_ID] as PatSt_Bal_C).PackageCode;
                    ObjTN.NavigateUrl = "#";

                    tvGroupTree.Nodes.Add(ObjTN);
                }

                ICollection IC_Test = MainTestLog_Bal_C.GetMaintestBy_Code((AL_Test[AL_ID] as PatSt_Bal_C).MTCode, Convert.ToInt32(Session["Branchid"]));
                IEnumerator IE_Test = IC_Test.GetEnumerator();
                while (IE_Test.MoveNext())
                {
                    MainTest_Bal_C MB_C = IE_Test.Current as MainTest_Bal_C;
                    TreeNode TV_Test = new TreeNode(MB_C.Maintestname);
                    TV_Test.Value = MB_C.MTCode;
                    TV_Test.Text = MB_C.Maintestname;
                    TV_Test.ToolTip = "Main test";
                    TV_Test.NavigateUrl = "#";

                    TV_Test.Checked = true;
                    ObjTN.ChildNodes.Add(TV_Test);
                }

                ObjTN.Expand();
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

        if (chkiscall.Checked == true)
        {
            Patmst_New_Bal_C.Update_callStatus(Convert.ToString(ViewState["RegNo"]), txtemark.Text, true);
        }
        else
        {
            Patmst_New_Bal_C.Update_callStatus(Convert.ToString(ViewState["RegNo"]), txtemark.Text, false);

        }

        PatientOnline_Report();
        isc.Visible = false;
        isc1.Visible = false;
        isc2.Visible = false;

    }
    public void AlterView_VE_GetLabNo(string regno)
    {
        int i;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "ALTER VIEW [dbo].[VW_GetLabNo]AS (select  LabNo,PatRegID,MTCode,Branchid from patmstd  where  PatRegID='" + regno + "' and  FID ='" + Convert.ToString(ViewState["FID"]) + "' )";
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
        GVTestentry.PageIndex = e.NewPageIndex;
        PatientOnline_Report();
    }
    protected void GVTestentry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
        string Mailstatus = Convert.ToString(e.Row.Cells[10].Text);
        if (Mailstatus == "0")
        {
            e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >Call</span>";

        }
        else if (Mailstatus == "1")
        {
            e.Row.Cells[10].Text = "<span  class='badge' >Viber</span>";

        }
        else
        {
            e.Row.Cells[10].Text = "<span class='btn btn-xs btn-success' >Whatsapp</span>";
        }
    }
    protected void GVTestentry_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (e.NewEditIndex == -1)
                return;
            int i = e.NewEditIndex;
            tvGroupTree.Nodes.Clear();
            //PatientOnline_Report();
            string rno = (GVTestentry.Rows[e.NewEditIndex].FindControl("hdnRegNo") as HiddenField).Value;
            string FID = (GVTestentry.Rows[e.NewEditIndex].FindControl("hdnFid") as HiddenField).Value;
            ViewState["RegNo"] = rno;
            ViewState["FID"] = FID;


            try
            {
                string CallS = "";
                string[] urlArr = new string[1];
                string[] targetArr = new string[1];
                string[] featuresArr = new string[1];
                targetArr[0] = "1";
                featuresArr[0] = "";
                if (RblCallStatus.SelectedItem.Text == "Call Done")
                {
                    CallS = "Done";
                }
                else
                {
                    CallS = "NotDone";
                }
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'CallSetUpDetailsNew.aspx?Branchid=" + Session["Branchid"].ToString() + "&RegNo=" + rno + "&FID=" + FID + "&CallS=" + CallS + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

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

            e.Cancel = true;
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

    protected void btnclick_Click(object sender, EventArgs e)
    {
        PatientOnline_Report();
    }
    protected void Chkemailtopatient_CheckedChanged(object sender, EventArgs e)
    {

        if (Chkemailtopatient.Checked == true)
        {
            Cshmst_Bal_C Cshmst = new Cshmst_Bal_C();
            int PID = -1;
            if (Convert.ToInt32(ViewState["PID"]) != -1)
            {
                PID = Convert.ToInt32(ViewState["PID"]);
            }
            Cshmst.getBalance(Convert.ToString(ViewState["RegNo"]), Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]), PID);
            if (Cshmst.Balance > 0)
            {
                Label6.Text = "Pending balance.";
                Label6.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pending balance.');", true);
            }
            else
            {

                Patmst_Bal_C ObjPBC = new Patmst_Bal_C(ViewState["RegNo"], Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]));
                string pname = ObjPBC.Initial.Trim() + " " + ObjPBC.Patname;
                string mobile = ObjPBC.Phone;
                string email = ObjPBC.Email;
                string docemail = DrMT_Bal_C.GetEmaildrNameTable(Convert.ToString(ViewState["RegNo"]), Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]));
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
                string T_Code = "";
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
                                MainTest_Bal_C tt = new MainTest_Bal_C(t, Convert.ToInt32(Session["Branchid"]));
                                T_Code = tt.MTCode;
                                TextDesc = tt.TextDesc;
                                View_Code = tt.SDCode;
                                PatSt_Bal_C pt = new PatSt_Bal_C();
                                pt.UpdateEmailstatus(ViewState["RegNo"].ToString(), ViewState["FID"].ToString(), tt.MTCode, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"]));

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
                    VW_DescriptiveViewLogic.SP_GetAlterView(Convert.ToInt32(Session["Branchid"]), View_TestDesc, Convert.ToString(ViewState["RegNo"]), Convert.ToString(ViewState["FID"]));

                    ReportParameterClass.SelectionFormula = "{VW_desfiledata_org.RegNo}='" + ViewState["RegNo"].ToString() + "' and {VW_desfiledata_org.FID}='" + ViewState["FID"].ToString() + "'";
                    DescFlag = true;
                }
                if (ViewTestCode != "")
                {
                    ViewTestCode = ViewTestCode + "')";
                    VW_DescriptiveViewLogic.SP_Getresultnondesc_Report(Convert.ToInt32(Session["Branchid"]), ViewTestCode, Convert.ToString(ViewState["RegNo"]), Convert.ToString(ViewState["FID"]));

                    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.RegNo}='" + ViewState["RegNo"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"].ToString() + "'";
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

                    sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where RegNo='" + ViewState["RegNo"].ToString() + "'  and FID='" + ViewState["FID"] + "' ", con);
                    sda.Fill(dt);

                    CR.SetDataSource((System.Data.DataTable)dt);
                    string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                    string filename11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    System.IO.File.WriteAllText(filename11, "");
                    string exportedpath = "", selectionFormula = "";
                    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.RegNo}='" + ViewState["RegNo"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"].ToString() + "'";
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

                    sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where RegNo='" + ViewState["RegNo"].ToString() + "'  and FID='" + ViewState["FID"] + "' ", con1);

                    sda1.Fill(dt1);

                    string filename22 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                    ReportParameterClass.ReportType = "";
                    rep.Close();
                    rep.Dispose();
                    GC.Collect();

                    if (dt.Rows.Count == 0)
                    {
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                        FileInfo fi = new FileInfo(filepath11);
                        fi.Delete();
                        Label44.Text = " Report Not Generated, Please Generate Once Again";
                        return;
                    }
                    if (dt1.Rows.Count == 0)
                    {
                        string filepath111 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));
                        Attachment att1 = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
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
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                            FileInfo fi = new FileInfo(filepath11);
                            fi.Delete();
                            string filepath111 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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

                    sda = new SqlDataAdapter("select * from VW_patdatvwrecvw where RegNo='" + ViewState["RegNo"].ToString() + "'  and FID='" + ViewState["FID"] + "' ", con);

                    sda.Fill(dt);
                    CR.SetDataSource((System.Data.DataTable)dt);
                    string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                    string filename11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
                    System.IO.File.WriteAllText(filename11, "");
                    string exportedpath = "", selectionFormula = "";
                    ReportParameterClass.SelectionFormula = "{VW_patdatvwrecvw.RegNo}='" + ViewState["RegNo"].ToString() + "' and {VW_patdatvwrecvw.FID}='" + ViewState["FID"].ToString() + "'";
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
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf"));



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
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Nondescriptive" + ".pdf");
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
                    DataTable dt = new DataTable();

                    sda = new SqlDataAdapter("select * from VW_desfiledata_org where RegNo='" + ViewState["RegNo"].ToString() + "'  and FID='" + ViewState["FID"] + "' ", con);

                    sda.Fill(dt);
                    string filename22 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");

                    CR.SetDataSource((System.Data.DataTable)dt);
                    string path = Server.MapPath("/" + Request.ApplicationPath + "/EmailReport/");
                    string filename11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
                    System.IO.File.WriteAllText(filename11, "");

                    ReportParameterClass.ReportType = "";

                    rep.Close();
                    rep.Dispose();
                    GC.Collect();
                    if (dt.Rows.Count == 0)
                    {
                        string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
                        Attachment att = new Attachment(Server.MapPath("~/EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf"));
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
                            string filepath11 = Server.MapPath("EmailReport//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + "Descriptive" + ".pdf");
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
        string OrgFile = Server.MapPath(folderName + "//" + "$" + Date1 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + ReportType + ".pdf");
        string DupFile = Server.MapPath(folderName + "//" + "$" + Date2 + "$" + ViewState["RegNo"].ToString() + "" + ViewState["FID"].ToString() + "" + Convert.ToInt32(Session["Branchid"]) + ReportType + ".pdf");

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
            Objpbc.Update_EmailId(Convert.ToString(ViewState["RegNo"]), Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]));
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

    protected void CVDesc_Init(object sender, EventArgs e)
    {

    }

    protected void CVDesc_PreRender(object sender, EventArgs e)
    {

    }
    protected void GVTestentry_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex == -1)
            return;
        int i = e.RowIndex;
        tvGroupTree.Nodes.Clear();
        //PatientOnline_Report();
        string rno = (GVTestentry.Rows[e.RowIndex].FindControl("hdnRegNo") as HiddenField).Value;
        string FID = (GVTestentry.Rows[e.RowIndex].FindControl("hdnFid") as HiddenField).Value;
        ViewState["RegNo"] = rno;
        ViewState["FID"] = FID;
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