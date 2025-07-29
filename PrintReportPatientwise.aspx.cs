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
using System.Web.Services;
using System.Web.Script.Services;
public partial class PrintReportPatientwise :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    object status = null;
    object stDate = null, endDate = null;

    string PateintName = "", PatRegID = "", labcode_main = "", Barcode = "", MNo = "", CCode = "", Center = "";
    string[] patient = new string[] { "", "" };
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    Patmst_New_Bal_C ObjPNB_C = new Patmst_New_Bal_C();

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.SetFocus(ddlCenter);


        if (!Page.IsPostBack)
        {
           
            ViewState["PatRegID"] = "";
            ddlfyear.DataSource = FinancialYearTableLogic.getFinancialYearsList_New(Convert.ToInt32(Session["Branchid"]));
            ddlfyear.DataTextField = "Yearname";
            ddlfyear.DataValueField = "FinancialYearId";
            ddlfyear.DataBind();
            ddlfyear.SelectedValue = Session["financialyear"].ToString().Trim();

            if (Session["usertype"] != null && Session["username"] != null)
            {
                if (Session["usertype"].ToString() == "patient")
                {
                    status = "Authorized";
                    //  tblsearch.Visible = false;

                }
                else
                {

                    if (Request.QueryString["did"] != null)
                    {
                        ddlCenter.Text = Request.QueryString["did"];
                    }
                    if (Request.QueryString["sdt"] != null)
                    {
                        fromdate.Text = Request.QueryString["sdt"];
                    }
                    else
                    {
                        fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                    }
                    if (Request.QueryString["edt"] != null)
                    {
                        todate.Text = Request.QueryString["edt"];
                    }
                    else
                    {
                        todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                    }

                    if (Session["usertype"].ToString() == "Collection Center")
                    {

                        createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));

                        Session["CenterCode"] = ui.UnitCode;
                        if (Request.QueryString["sdt"] != null)
                        {
                            fromdate.Text = Request.QueryString["sdt"];
                        }
                        else
                        {
                            fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                        }
                        if (Request.QueryString["edt"] != null)
                        {
                            todate.Text = Request.QueryString["edt"];
                        }
                        else
                        {
                            todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                        }

                    }
                    else
                    {
                        CCode = Convert.ToString(Session["CenterCode"]);
                    }

                    if (Session["usertype"].ToString() == "Reference Doctor")
                    {

                        createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                        // Session["DrRefCode"] = " " + ui.Drid;
                        Session["DrRefCode"] = " " + ui.Name;

                        if (Request.QueryString["sdt"] != null)
                        {
                            fromdate.Text = Request.QueryString["sdt"];
                        }
                        else
                        {
                            fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                        }
                        if (Request.QueryString["edt"] != null)
                        {
                            todate.Text = Request.QueryString["edt"];
                        }
                        else
                        {
                            todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                        }

                    }
                    else
                    {
                        CCode = Convert.ToString(Session["CenterCode"]);
                    }

                    ddlCenter.Text = "All";
                    if (Session["CenterCode"] != null)
                    {
                        btnList_Click(this, null);
                    }
                    else
                    {
                        btnList_Click(this, null);
                    }
                }
            }
        }

    }

 
    void BindGrid()
    {
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            labcode_main = labcode;
        }
        if ((fromdate.Text != "" && todate.Text != "") || txtRegNo.Text.Trim() != "")
        {
            stDate = DateTimeConvesion.getDateFromString(fromdate.Text);
            endDate = DateTimeConvesion.getDateFromString(todate.Text);

            #region AlterView
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = conn.CreateCommand();
            sc.CommandText = " ALTER VIEW [dbo].[VW_Printingpatientwise] AS (SELECT     patmst.SrNo, patmst.PID, patmst.FID, patmst.PatRegID, patmst.Patregdate,  "+
              "  patmst.intial +' '+patmst.Patname as PatientName, patmst.sex, patmst.Age, patmst.MDY, patmst.RefDr,  "+
              "  patmst.Tests, patmst.PF, patmst.Reportdate, patmst.Phrecdate, patmst.flag, patmst.Patphoneno, patmst.Pataddress, patmst.Isemergency, patmst.Branchid, "+
              "  patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.EmailID, patmst.Drname, patmst.TestCharges  + ISNULL(Cshmst.Othercharges, 0) as TestCharges , patmst.SampleID, patmst.CenterName,  " +
              "  patmst.Username, patmst.Usertype, patmst.SampleType, patmst.SampleStatus, patmst.Remark, patmst.PatientcHistory, patmst.RegistratonDateTime, patmst.TelNo, "+ 
              "  patmst.Email, patmst.Patusername, patmst.Patpassword, patmst.PPID, patmst.testname, patmst.Smsevent, patmst.UnitCode, patmst.IsbillBH, patmst.IsActive, "+
              "  patmst.Monthlybill, patmst.cevent, patmst.LabRegMediPro, patmst.IsFreeze, patmst.ISCallPatient, patmst.CallRemark, patmst.OtherRefDoctor, patmst.Weights, "+
              "  patmst.Heights, patmst.Disease, patmst.LastPeriod, patmst.Symptoms, patmst.FSTime, patmst.Therapy, Cshmst.AmtPaid, Cshmst.NetPayment "+
              "  ,ISNULL(Cshmst.NetPayment, 0) - (ISNULL(Cshmst.Discount, 0)+ISNULL(Cshmst.AmtPaid, 0)) AS OutStAmt,ISNULL(Cshmst.Discount, 0) as Discount " +
              "  FROM         patmst INNER JOIN "+
              "  Cshmst ON patmst.PatRegID = Cshmst.PatRegID AND patmst.PID = Cshmst.PID where  patmst.IsActive=1  and patmst.Phrecdate between '" + Convert.ToDateTime(stDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "'  or dbo.patmst.PatRegID='" + txtRegNo.Text.Trim().ToString() + "' )"; //
            conn.Open();
            sc.ExecuteNonQuery();
            conn.Close(); conn.Dispose();
            #endregion
        }
        if (txtPatientName.Text.Trim() != "")
        {
           // patient = txtPatientName.Text.Split(' ');
           // string name = patient[1];
           // PateintName = name.Trim();
            PateintName = txtPatientName.Text.Trim();
        }
        if (txtRegNo.Text.Trim() != "")
        {
            PatRegID = txtRegNo.Text.Trim();
        }
        if (txtbarcode.Text.Trim() != "")
        {
            Barcode = txtbarcode.Text.Trim();
        }
        else
        {
            Barcode = "";
        }
        if (txtmobileno.Text.Trim() != "")
        {
            MNo = txtmobileno.Text.Trim();
        }
        else
        {
            MNo = "";
        }
        if (txtCenter.Text.Trim() != "")
        {
            CCode = txtCenter.Text.Trim();
        }
        else if (ddlCenter.Text.Trim() != "All")
        {
            CCode = ddlCenter.Text.Trim();
        }
        else
        {
            CCode = "";
        }
        status = "Authorized";
        status = "";
        ViewState["PatRegID"] = "0";
        string subdept = "";
        dt = ObjPNB_C.Get_subdept(Convert.ToString(Session["username"]));
        if (dt.Rows.Count > 0)
        {
            subdept = Convert.ToString(dt.Rows[0]["subdept"]);
        }
        string RepStatus = RblRepStatus.SelectedItem.Text;
        if (RepStatus == "Pending")
        {
            RepStatus = "0";
        }
        if (RepStatus == "Completed")
        {
            RepStatus = "1";
        }
        if (RepStatus == "Authorized")
        {
            RepStatus = "2";
        }
        if (RepStatus == "Tested")
        {
            RepStatus = "3";
        }
       
        if (Session["DrRefCode"] != null)
        {
            gridreport.DataSource = ObjPNB_C.ReportDownload_Patientwise(CCode, stDate, endDate, status, PateintName, PatRegID, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), "", ddlfyear.SelectedValue.PadLeft(2, '0'), labcode_main, Session["DrRefCode"].ToString(), Barcode, MNo, RepStatus);
            gridreport.DataBind();
        }
        else
        {
            gridreport.DataSource = ObjPNB_C.ReportDownloadModify_Patientwise(CCode, stDate, endDate, status, PateintName, PatRegID, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), "", ddlfyear.SelectedValue.PadLeft(2, '0'), labcode_main, Barcode, subdept, RepStatus);
            gridreport.DataBind();
        }

        for (int i = 0; i < gridreport.Rows.Count; i++)
        {
            if (i > 0)
            {
                if (gridreport.Rows[i].Cells[0].Text.Trim() == gridreport.Rows[i - 1].Cells[0].Text.Trim())
                {
                    ViewState["PatRegID"] = gridreport.Rows[i].Cells[0].Text.Trim();
                    gridreport.Rows[i].Cells[0].Text = "";
                    gridreport.Rows[i].Cells[1].Text = "";
                    gridreport.Rows[i].Cells[2].Text = "";
                    gridreport.Rows[i].Cells[3].Text = "";
                    gridreport.Rows[i].Cells[4].Text = "";
                    gridreport.Rows[i].Cells[5].Text = "";
                    gridreport.Rows[i].Cells[6].Text = "";
                    gridreport.Rows[i].Cells[10].Text = "";
                    gridreport.Rows[i].Cells[11].Text = "";
                    gridreport.Rows[i].Cells[9].Text = "";
                    gridreport.Rows[i].Cells[12].Text = "";
                    gridreport.Rows[i].Cells[13].Text = "";
                    gridreport.Rows[i].Cells[14].Text = "";
                    gridreport.Rows[i].Cells[15].Text = "";
                }
                if (ViewState["PatRegID"].ToString() == gridreport.Rows[i].Cells[0].Text.Trim())
                {
                    gridreport.Rows[i].Cells[0].Text = "";
                    gridreport.Rows[i].Cells[1].Text = "";
                    gridreport.Rows[i].Cells[2].Text = "";
                    gridreport.Rows[i].Cells[3].Text = "";
                    gridreport.Rows[i].Cells[4].Text = "";
                    gridreport.Rows[i].Cells[5].Text = "";
                    gridreport.Rows[i].Cells[6].Text = "";
                    gridreport.Rows[i].Cells[10].Text = "";
                    gridreport.Rows[i].Cells[11].Text = "";
                    gridreport.Rows[i].Cells[9].Text = "";
                    gridreport.Rows[i].Cells[12].Text = "";
                    gridreport.Rows[i].Cells[13].Text = "";
                    gridreport.Rows[i].Cells[14].Text = "";
                    gridreport.Rows[i].Cells[15].Text = "";
                }
            }
            try
            {
                if (gridreport.Rows[i].Cells[13].Text.Trim() == "&nbsp;")
                {
                    gridreport.Rows[i].Cells[13].Text = (gridreport.Rows[i].FindControl("hdntestcharges") as HiddenField).Value.ToString();
                }
                if (Convert.ToInt32(gridreport.Rows[i].Cells[13].Text.Trim()) > 1)
                {
                    //gridreport.Rows[i].BackColor = System.Drawing.Color.Red;
                    //gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
                    // gridreport.Rows[i].Font.Bold = true;
                }
                // gridreport.Rows[i].Font.Bold = true;

            }
            catch (Exception ee)
            { }
            

            //if (gridreport.Rows[i].Cells[08].Text == "Registered")
            //{
            //    gridreport.Rows[i].Cells[08].Text = "<span class='btn btn-xs btn-Blue' >Registered</span>";
            //    gridreport.Rows[i].Cells[14].Text = "";
            //}
            //else if (gridreport.Rows[i].Cells[08].Text == "Authorized")
            //{
            //    gridreport.Rows[i].Cells[08].Text = "<span class='btn btn-xs btn-success' >Authorized</span>";
            //}
            //else if (gridreport.Rows[i].Cells[08].Text == "Tested")
            //{
            //    gridreport.Rows[i].Cells[08].Text = "<span class='btn btn-xs btn-danger' >Tested</span>";
            //    gridreport.Rows[i].Cells[14].Text = "";
            //}
            //else
            //{
            //    gridreport.Rows[i].Cells[08].Text = "<span class='btn btn-xs btn-danger' >Pat Entry</span>";
            //  //  gridreport.Rows[i].Cells[14].Text = "";
            //}
          
            //bool Patrepstatus = Convert.ToBoolean((gridreport.Rows[i].FindControl("Hdn_printstatus") as HiddenField).Value.ToString());

            //if (Patrepstatus == true)
            //{
            //    //   // gridreport.Rows[i].BackColor = System.Drawing.Color.Green;
            //    //   // gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
            //    gridreport.Rows[i].Cells[08].Text = "<span class='btn btn-xs btn-success' >Printed</span>";
            //}
            //bool Pemail = Convert.ToBoolean((gridreport.Rows[i].FindControl("Hdn_PatientEmail") as HiddenField).Value.ToString());
            //if (Pemail == true)
            //{
            //    // gridreport.Rows[i].BackColor = System.Drawing.Color.Gray;
            //    // gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
            //    gridreport.Rows[i].Cells[08].Text = "<span class='btn btn-xs btn-muted' >Printed</span>";
            //}

        }
    }



    protected void btnList_Click(object sender, EventArgs e)
    {
        BindGrid();
    }


    protected void gridreport_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowIndex == -1)
            return;
        string regno = (e.Row.FindControl("hdnRegNo") as HiddenField).Value.Trim();
         string FID = (e.Row.FindControl("HdnFD") as HiddenField).Value.Trim();
         if (regno != "")
         {
             if (RblRepStatus.SelectedItem.Text == "All")
             {
                 int Totalcount = PatSt_new_Bal_C.GetTotalCount(regno, FID, Convert.ToInt32(Session["Branchid"]));
                 int Printcount = PatSt_new_Bal_C.GetPrintCount(regno, FID, Convert.ToInt32(Session["Branchid"]));
                 int StatuscountAu = PatSt_new_Bal_C.GetStatusCountAuto(regno, FID, Convert.ToInt32(Session["Branchid"]));
                 int StatuscountTe = PatSt_new_Bal_C.GetStatusCountTes(regno, FID, Convert.ToInt32(Session["Branchid"]));
                 int StatuscountReg = PatSt_new_Bal_C.GetStatusCountReg(regno, FID, Convert.ToInt32(Session["Branchid"]));
                 if (Totalcount == Printcount)
                 {
                     e.Row.Cells[09].Text = "<span class='btn btn-xs btn-success' >Printed</span>";
                 }
                 else if (Totalcount == StatuscountAu)
                 {

                     e.Row.Cells[09].Text = "<span class='btn btn-xs btn-primary' >Auth</span>";
                     //e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >Par Prin</span>";
                 }
                 else if (Totalcount == StatuscountTe)
                 {

                     e.Row.Cells[09].Text = "<span class='badge'>Tes</span>";
                     //e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >Par Prin</span>";
                     e.Row.Cells[14].Text = "";
                 }
                 else if (Totalcount == StatuscountReg)
                 {

                     e.Row.Cells[09].Text = "<span class='btn btn-xs btn-danger' >Regi</span>";
                     //e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >Par Prin</span>";
                     //e.Row.Cells[11].Text = "";
                     //e.Row.Cells[12].Text = "";
                     //e.Row.Cells[13].Text = "";
                     e.Row.Cells[14].Text = "";
                 }
                 else
                 {
                     e.Row.Cells[09].Text = "<span class='btn btn-xs btn-danger' >Regi</span>";
                     //e.Row.Cells[11].Text = "";
                     //e.Row.Cells[12].Text = "";
                     //e.Row.Cells[13].Text = "";
                     e.Row.Cells[14].Text = "";
                 }
             }
             else
             {
                 if (RblRepStatus.SelectedItem.Text == "Completed")
                 {
                     e.Row.Cells[09].Text = "<span class='btn btn-xs btn-success' >Printed</span>";
                 }
                 if (RblRepStatus.SelectedItem.Text == "Authorized")
                 {
                     e.Row.Cells[09].Text = "<span class='btn btn-xs btn-primary' >Auth</span>";
                 }
                 if (RblRepStatus.SelectedItem.Text == "Tested")
                 {
                     e.Row.Cells[09].Text = "<span class='badge'>Tes</span>";
                     e.Row.Cells[14].Text = "";
                 }
                 if (RblRepStatus.SelectedItem.Text == "Pending")
                 {
                     e.Row.Cells[09].Text = "<span class='btn btn-xs btn-danger' >Regi</span>";
                     e.Row.Cells[14].Text = "";
                 }
             }
         }


    }

    protected void gridreport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridreport.PageIndex = e.NewPageIndex;
        BindGrid();
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
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like N'" + prefixText + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like N'" + prefixText + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["DoctorName"], i);
            i++;
        }
        return tests;
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] FillPateintName(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {

            sda = new SqlDataAdapter("SELECT distinct rtrim(intial)+' '+Patname as Patname FROM patmst where Patname like N'%" + prefixText + "%'and branchid=" + branchid + " order by Patname", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT distinct rtrim(intial)+' '+Patname as Patname FROM patmst where Patname like N'%" + prefixText + "%' and branchid=" + branchid + " order by Patname", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["Patname"], i);
            i++;
        }
        return tests;
    }

    protected void ddlCenter_TextChanged(object sender, EventArgs e)
    {
        string Center = DrMT_sign_Bal_C.Get_C_Code(ddlCenter.Text.Trim(), Convert.ToInt32(Session["Branchid"]));
        Session["CenterCode"] = Center;
    }


    protected void gridreport_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (e.NewEditIndex == -1)
                return;
            int i = e.NewEditIndex;
            string PatRegID = (gridreport.Rows[e.NewEditIndex].FindControl("hdnRegNo") as HiddenField).Value;
            string FID = (gridreport.Rows[e.NewEditIndex].FindControl("HdnFD") as HiddenField).Value;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'TestReportprinting.aspx?PatRegID=" + PatRegID + "&FID=" + FID + "&pfrm=rfp" + "&did=" + ddlCenter.Text + "&sdt=" + fromdate.Text + "&edt=" + todate.Text + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
            // BindGrid();
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
    protected void RblRepStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid(); ;
    }
}