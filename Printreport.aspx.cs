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

public partial class Printreport : System.Web.UI.Page
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

       // Page.SetFocus(ddlCenter);


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
                    txtCenter.Enabled = false;
                    txtCenter.Width = 460;
                    if (Session["usertype"].ToString() == "Collection Center" || Session["usertype"].ToString() == "CollectionCenter")
                    {

                        createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                       
                       // Session["CenterCode"] = ui.UnitCode;
                        Session["CenterCodePR"] = ui.CenterCode;
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
                        ddlCenter.Text = ui.CenterCode;
                        CCode = Convert.ToString(Session["CenterCode"]);
                        ddlCenter.Enabled = false;
                        ddlCenter.Width = 260;
                    }
                    else
                    {

                        CCode = Convert.ToString(Session["CenterCodePR"]);
                        ddlCenter.Text = "All";
                    }

                    if (Session["usertype"].ToString() == "Reference Doctor")
                    {
                        ddlCenter.Enabled = false;
                        ddlCenter.Width = 460;
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

                   // ddlCenter.Text = "All";
                    if (Session["CenterCodePR"] != null)
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
            sc.CommandText = " ALTER VIEW [dbo].[VW_patrprtvwd] AS (SELECT     patmst.PatRegID, RTRIM(patmst.intial) + ' ' + patmst.Patname AS Name, patmst.sex, patmst.DoctorCode, patmst.Age, patmst.MDY, CAST(patmst.PatientcHistory AS varchar(4000)) as PatientcHistory,  "+
                      "  patmst.Drname, patmst.CenterName, patmstd.Patauthicante, MainTest.Maintestname, patmst.CenterCode, patmst.Phrecdate, patmst.Patname, patmst.Branchid,   "+
                      "  patmst.FID, patmst.TestCharges  + sum(ISNULL(RecM.Othercharges, 0)) -  ( sum(ISNULL(RecM.AmtPaid, 0))+ sum(ISNULL(RecM.DisAmt, 0))) AS OutStAmt, MainTest.SDCode, " +
                      "  patmst.TestCharges  + sum(ISNULL(RecM.Othercharges, 0))as TestCharges , patmst.Patregdate, patmst.UnitCode,     patmst.PPID, patmstd.BarcodeID, "+
                     
                      "  patmstd.Patrepstatus, sum(ISNULL(RecM.AmtPaid, 0)) AS AmtReceived, sum(ISNULL(RecM.DisAmt, 0)) AS Discount,    patmstd.DoctorEmail, patmstd.PatientEmail,  "+
                      "  patmst.LabRegMediPro,patmst.Patphoneno as TelNo,patmstd.Printedby  , patmst.Monthlybill,Patmst.UserName as CreatedBy  ,patmst.Pataddress, patmst.EmailID, patmst.Remark, patmst.Email, patmst.OtherRefDoctor " +
                      "  FROM         patmst INNER JOIN    patmstd ON patmst.PID = patmstd.PID INNER JOIN    MainTest ON dbo.patmstd.MTCode = MainTest.MTCode LEFT OUTER JOIN   "+
                      "  RecM ON patmst.PID = RecM.PID where  patmst.IsActive=1 and  (patmstd.TestDeActive = 0) and patmst.Phrecdate between '" + Convert.ToDateTime(stDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "'  or dbo.patmst.PatRegID='" + txtRegNo.Text.Trim().ToString() + "' "+ 
                      "  group by "+
                      "  patmst.PatRegID, RTRIM(patmst.intial),patmst.Patname , "+
                      "  patmst.sex, patmst.DoctorCode, patmst.Age, patmst.MDY,  CAST(patmst.PatientcHistory AS varchar(4000)) ,  "+
                      "  patmst.Drname, patmst.CenterName, patmstd.Patauthicante, MainTest.Maintestname, patmst.CenterCode, patmst.Phrecdate, patmst.Patname, patmst.Branchid,  "+ 
                      "  patmst.FID,  MainTest.SDCode, "+
                      "  patmst.Patregdate, patmst.UnitCode,     patmst.PPID, patmstd.BarcodeID, "+
                      "  patmst.TestCharges   , patmst.Patregdate, patmst.UnitCode,     patmst.PPID, patmstd.BarcodeID, "+
                      "  patmstd.Patrepstatus,  patmstd.DoctorEmail, patmstd.PatientEmail,  "+
                      "  patmst.LabRegMediPro,patmst.Patphoneno ,patmstd.Printedby , patmst.Monthlybill,Patmst.UserName ,patmst.Pataddress, patmst.EmailID, patmst.Remark, patmst.Email, patmst.OtherRefDoctor) "; //
            conn.Open();
            sc.ExecuteNonQuery();
            conn.Close(); conn.Dispose();
            #endregion
        }
        if (txtPatientName.Text.Trim() != "")
        {
            //patient = txtPatientName.Text.Split(' ');
            //string name = patient[1];
            //PateintName = name.Trim();
            PateintName = txtPatientName.Text.Trim().Replace("'", "'+char(39)+'"); ;
            //string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");

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
        if (Convert.ToString( Session["usertype"]) != "CollectionCenter")
        {
            if (Convert.ToString(Session["CenterCodePR"]) == "All")
            {
                CCode = "";
            }
            else
            {
                CCode = Convert.ToString(Session["CenterCodePR"]);
            }
        }
        else
        {
            createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
             CCode = ui.CenterCode;
            if (CCode == "0")
            {
                CCode = "";
            }
            else
            {
                //CCode = Convert.ToString(Session["CenterCode"]);
                CCode = Convert.ToString(ui.CenterCode);
            }
        }
        if (txtCenter.Text.Trim() != "")
        {
            CCode = txtCenter.Text.Trim();
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
        if (RepStatus == "Completed")
        {
            RepStatus = "1";
        }
        if (RepStatus == "Pending")
        {
            RepStatus = "0";
        }
        if (Session["DrRefCode"] != null)
        {
            gridreport.DataSource = ObjPNB_C.ReportDownload(CCode, stDate, endDate, status, PateintName, PatRegID, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), "", ddlfyear.SelectedValue.PadLeft(2, '0'), labcode_main, Session["DrRefCode"].ToString(), Barcode, MNo, RepStatus);
            gridreport.DataBind();
        }
        else
        {
            gridreport.DataSource = ObjPNB_C.ReportDownloadModify(CCode, stDate, endDate, status, PateintName, PatRegID, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), "", ddlfyear.SelectedValue.PadLeft(2, '0'), labcode_main, Barcode, subdept, RepStatus, MNo);
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
                    // gridreport.Rows[i].Cells[10].Text = "";
                    gridreport.Rows[i].Cells[11].Text = "";
                    // gridreport.Rows[i].Cells[9].Text = "";
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
                    // gridreport.Rows[i].Cells[10].Text = "";
                    gridreport.Rows[i].Cells[11].Text = "";
                    // gridreport.Rows[i].Cells[9].Text = "";
                    gridreport.Rows[i].Cells[12].Text = "";
                    gridreport.Rows[i].Cells[13].Text = "";
                    gridreport.Rows[i].Cells[14].Text = "";
                    gridreport.Rows[i].Cells[15].Text = "";
                }
            }
            try
            {
                if (gridreport.Rows[i].Cells[14].Text.Trim() == "&nbsp;")
                {
                    gridreport.Rows[i].Cells[14].Text = (gridreport.Rows[i].FindControl("hdntestcharges") as HiddenField).Value.ToString();
                }
                if (Convert.ToInt32(gridreport.Rows[i].Cells[14].Text.Trim()) > 1)
                {
                    //gridreport.Rows[i].BackColor = System.Drawing.Color.Red;
                    //gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
                    // gridreport.Rows[i].Font.Bold = true;
                }
                // gridreport.Rows[i].Font.Bold = true;

            }
            catch (Exception ee)
            { }
            bool Patrepstatus = Convert.ToBoolean((gridreport.Rows[i].FindControl("Hdn_printstatus") as HiddenField).Value.ToString());
            bool MBill = Convert.ToBoolean((gridreport.Rows[i].FindControl("HdnMonthlybill") as HiddenField).Value.ToString());

            if (gridreport.Rows[i].Cells[09].Text == "Registered")
            {
                gridreport.Rows[i].Cells[09].Text = "<span class='btn btn-xs btn-danger' >Reg</span>";
                gridreport.Rows[i].Cells[10].Text = "";
            }
            else if (gridreport.Rows[i].Cells[09].Text == "Authorized")
            {
                gridreport.Rows[i].Cells[09].Text = "<span class='btn btn-xs btn-success' >Authorized</span>";
            }
            else if (gridreport.Rows[i].Cells[09].Text == "Tested")
            {
                gridreport.Rows[i].Cells[09].Text = "<span class='btn btn-xs btn-warning' >Tested</span>";
                gridreport.Rows[i].Cells[10].Text = "";
            }
            else
            {
                gridreport.Rows[i].Cells[09].Text = "<span class='btn btn-xs btn-danger' >Pat Entry</span>";
                gridreport.Rows[i].Cells[10].Text = "";
            }
            if (Patrepstatus == true)
            {
                //   // gridreport.Rows[i].BackColor = System.Drawing.Color.Green;
                //   // gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
                gridreport.Rows[i].Cells[09].Text = "<span class='btn btn-xs btn-success' >Printed</span>";
            }
            bool Pemail = Convert.ToBoolean((gridreport.Rows[i].FindControl("Hdn_PatientEmail") as HiddenField).Value.ToString());
            if (Pemail == true)
            {
                // gridreport.Rows[i].BackColor = System.Drawing.Color.Gray;
                // gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
                gridreport.Rows[i].Cells[09].Text = "<span class='btn btn-xs btn-muted' >Printed</span>";
            }
            //float  BAmt = Convert.ToSingle((gridreport.Rows[i].FindControl("hdnOutStAmt") as HiddenField).Value.ToString());
            //if (MBill == true)
            //{
            //   // gridreport.Rows[i].Cells[09].Text
            //}
            //else
            //{
            //    if (BAmt > 0)
            //    {
            //        if (Convert.ToString(Session["usertype"]) != "Administrator")
            //        {
            //            gridreport.Rows[i].Cells[10].Text = "";
            //        }
            //    }
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
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like N'" + prefixTextNew + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like N'" + prefixTextNew + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
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
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {

            sda = new SqlDataAdapter("SELECT distinct rtrim(intial)+' '+Patname as Patname FROM patmst where Patname like N'%" + prefixTextNew + "%'and branchid=" + branchid + " order by Patname", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT distinct rtrim(intial)+' '+Patname as Patname FROM patmst where Patname like N'%" + prefixTextNew + "%' and branchid=" + branchid + " order by Patname", con);
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
        Session["CenterCodePR"] = Center;


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