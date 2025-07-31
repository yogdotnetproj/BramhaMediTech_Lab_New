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

public partial class Teststatus :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    object status = null;
    string patientName = "", subdeptName = "", Pat_Regid = "", labcode_main = "", Barcode = "", Mno = "";
    string[] patient = new string[] { "", "" };
    DateTime stDate = Date.getMinDate(), endDate = Date.getMinDate();
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    Patmst_New_Bal_C Obj_PNBC = new Patmst_New_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {

        //Page.SetFocus(fromdate);

        if (!Page.IsPostBack)
        {
            if (Convert.ToString(Session["HMS"]) != "Yes")
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("Teststatus.aspx");
                }
            }
            fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
            todate.Text = Date.getdate().ToString("dd/MM/yyyy");

            if (Session["usertype"] != null && Session["username"] != null)
            {
                drp_deparment.DataSource = Obj_PNBC.FilldeptDrop();
                drp_deparment.DataTextField = "subdeptName";
                drp_deparment.DataValueField = "SDCode";
                drp_deparment.DataBind();
                drp_deparment.Items.Insert(0, "All Department");
                drp_deparment.Items[0].Value = "0";
                if (Session["usertype"].ToString() == "patient")
                {
                    if (drp_deparment.SelectedValue != "0")
                    {
                        subdeptName = drp_deparment.SelectedValue;
                    }
                    // tblsearch.Visible = false;
                    gridreport.DataSource = Obj_PNBC.ReportDownload(Session["CenterCode"], stDate, endDate, status, "", Session["regno"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdeptName, "", "", "", "");
                    gridreport.DataBind();
                }
                else
                {

                    ddlcenter.Visible = true;
                    // Label4.Visible = true;
                    ddlcenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Convert.ToString( Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]));
                    ddlcenter.DataTextField = "Name";
                    ddlcenter.DataValueField = "DoctorCode";
                    ddlcenter.DataBind();
                    ddlcenter.Items.Insert(0, "All Center");
                    ddlcenter.Items[0].Value = "0";
                    ddlcenter.SelectedIndex = -1;
                    // Session["CenterCode"] = "0";

                    if (Session["usertype"].ToString() == "CollectionCenter" && Session["username"] != null)
                    {
                        ddlcenter.Visible = true;
                        // Label4.Visible = false;
                        createuserTable_Bal_C Obj_CTB = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));

                        DrMT_Bal_C DrMT = new DrMT_Bal_C(Obj_CTB.CenterCode, "CC", Convert.ToInt32(Session["Branchid"]));
                        Session["CenterCode"] = Obj_CTB.CenterCode;

                        ddlcenter.SelectedValue = Obj_CTB.CenterCode;
                        ddlcenter.Enabled = false;
                        fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                        todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                        ddlStatus.SelectedValue = "Registered";
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
        if (txtPatientName.Text.Trim() != "")
        {
            try
            {
                if (txtPatientName.Text == "All")
                {
                    patientName = txtPatientName.Text;
                }
                else
                {
                    //patient = txtPatientName.Text.Split(' ');
                    //string name = patient[1];
                    //patientName = name.Trim();
                    //patientName = txtPatientName.Text;
                    patientName = txtPatientName.Text.Trim().Replace("'", "'+char(39)+'");
                }
            }
            catch
            {
                txtPatientName.Text = "";
                return;
            }
        }
        if (txtDoctorName.Text.Trim() != "")
        {
            string[] DocCode = new string[] { "", "" };
            DocCode = txtDoctorName.Text.Split('=');
            txtDoctorName.Text = DocCode[0];
        }
        if ((fromdate.Text != "" && todate.Text != "") || txtregno.Text.Trim() != "")
        {
            stDate = DateTimeConvesion.getDateFromString(fromdate.Text);
            endDate = DateTimeConvesion.getDateFromString(todate.Text);

            #region AlterView
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = conn.CreateCommand();
            sc.CommandText = "ALTER VIEW VW_patrprtvwd AS  SELECT distinct    dbo.patmst.PatRegID, RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS Name, dbo.patmst.sex,    dbo.patmst.Age, dbo.patmst.MDY, ''as PatientcHistory, "+
                          "  dbo.patmst.Drname,  dbo.patmst.Centername,   dbo.MainTest.Maintestname,  dbo.patmst.Centercode, dbo.patmst.Phrecdate, dbo.patmst.Patname, dbo.patmst.branchid,    "+
                          "  dbo.patmst.FID, patmst.TestCharges + SUM(ISNULL(RecM.OtherCharges, 0)) - (sum(dbo.RecM.AmtPaid) + sum(dbo.RecM.DisAmt)) AS OutStAmt, dbo.MainTest.SDCode,  " +
                          "  dbo.patmst.TestCharges +sum( ISNULL(RecM.Othercharges, 0))as TestCharges ,   "+
                          "  convert(varchar(20),dbo.patmst.Patregdate,103)+' '+convert(varchar(20),convert(time,dbo.patmst.Patregdate),100) as Patregdate,  "+
                          "  dbo.patmst.PPID , patmst.UnitCode ,ISNULL( patmstd.Patrepstatus,0)as Patrepstatus,    ISNULL( patmstd.Patauthicante, 'Registered')as status, "+
                          "  patmstd.BarcodeID, patmst.Patphoneno  ,   patmst.Isemergency,case when PatientEmail=1 then 'Send Email' else 'Not Send' end as MailStatus ,  "+
                          "  isnull(patmstd.SpecimanNo ,0)as SpecimanNo, patmstd.isbarcodeprint, patmstd.PhlebotomistCollect,  patmstd.PhlebotomistRejectremark, "+
                          "  patmstd.SampleAcceptDate,patmst.ISCallPatient,patmstd.ISpheboAccept,patmst.UploadPrescription ,sum(dbo.RecM.AmtPaid) as PaidAmt, sum(dbo.RecM.DisAmt) as DiscAmt , Patmst.UserName as CreatedBy  ,patmst.Pataddress, patmst.EmailID, patmst.Remark, patmst.Email, patmst.OtherRefDoctor , case when isnull(WhatAppReport,0)=1 then 'Whatapp Send' else 'NA' end as WhatAppReport " +
                            " FROM         dbo.patmst INNER JOIN   dbo.patmstd ON dbo.patmst.PID = dbo.patmstd.PID INNER JOIN   dbo.MainTest ON dbo.patmstd.MTCode = dbo.MainTest.MTCode  "+
                         "   LEFT OUTER JOIN   dbo.RecM ON dbo.patmst.PID = dbo.RecM.PID   " +
                             "  where (patmst.IsActive = 1) and patmstd.TestDeActive=0  and patmst.Phrecdate between '" + Convert.ToDateTime(stDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' group by  "+
                         "   dbo.patmst.PatRegID, RTRIM(dbo.patmst.intial) , dbo.patmst.Patname , dbo.patmst.sex,    dbo.patmst.Age, dbo.patmst.MDY,  "+
                         "   dbo.patmst.Drname,  dbo.patmst.Centername,   dbo.MainTest.Maintestname,  dbo.patmst.Centercode, dbo.patmst.Phrecdate, dbo.patmst.Patname, dbo.patmst.branchid,   "+
                         "   dbo.patmst.FID, dbo.patmst.TestCharges , dbo.MainTest.SDCode,     "+
                         "   convert(varchar(20),dbo.patmst.Patregdate,103),convert(varchar(20),convert(time,dbo.patmst.Patregdate),100) ,  "+
                         "   dbo.patmst.PPID , patmst.UnitCode ,ISNULL( patmstd.Patrepstatus,0),    ISNULL( patmstd.Patauthicante, 'Registered'), "+
                         "   patmstd.BarcodeID, patmst.Patphoneno  ,   patmst.Isemergency,case when PatientEmail=1 then 'Send Email' else 'Not Send' end , "+
                         "   isnull(patmstd.SpecimanNo ,0), patmstd.isbarcodeprint, patmstd.PhlebotomistCollect,  patmstd.PhlebotomistRejectremark, "+
                         "   patmstd.SampleAcceptDate,patmst.ISCallPatient,patmstd.ISpheboAccept,patmst.UploadPrescription,Patmst.UserName  ,patmst.Pataddress, patmst.EmailID, patmst.Remark, patmst.Email, patmst.OtherRefDoctor,case when isnull(WhatAppReport,0)=1 then 'Whatapp Send' else 'NA' end ";
            conn.Open();
            sc.ExecuteNonQuery();
            conn.Close(); conn.Dispose();
            #endregion

        }
        //if (ddlStatus.SelectedValue != "0")
        //{
        //    status = ddlStatus.SelectedItem.Text.Trim();
        //}

        //if (ddlStatusAll.SelectedValue != "0")
        //    {
        //        status = ddlStatusAll.SelectedItem.Text.Trim();
        //    }
        if (ChkRegistered.Checked == true)
        {
            status = "Registered";
        }
        if (ChkSamCollect.Checked == true)
        {
            status = "SamCollect";
        }
        if (ChkSamAccept.Checked == true)
        {
            status = "SamAccept";
        }
        if (ChkSamReject.Checked == true)
        {
            status = "SamReject";
        }
        if (ChkTested.Checked == true)
        {
            status = "Tested";
        }
        if (ChkAuthorized.Checked == true)
        {
            status = "Authorized";
        }
        if (ChkCompleted.Checked == true)
        {
            status = "Completed";
        }
        if (ChkDispatch.Checked == true)
        {
            status = "Dispatch";
        }
        if (ChkAll.Checked == true)
        {
            status = "All";
        }
        if (txtregno.Text.Trim() != "")
        {
            Pat_Regid = txtregno.Text.Trim();

        }
        if (drp_deparment.SelectedValue != "0")
        {
            subdeptName = drp_deparment.SelectedValue;
        }

        Barcode = "";

        if (txtmobileno.Text.Trim() != "")
        {
            Mno = txtmobileno.Text.Trim();
        }
        else
        {
            Mno = "";
        }


        ViewState["regno"] = "0";
        string CenCode = "";
        if (ddlcenter.SelectedItem.Text != "All Center")
        {
            
            string[] CenCodeN = ddlcenter.SelectedItem.Text.Split('=');
            CenCode = CenCodeN[0];
        }

        gridreport.DataSource = Obj_PNBC.PatientTeststatus(CenCode, stDate, endDate, status, patientName, Pat_Regid, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), subdeptName, "", labcode_main, Barcode, Mno, txtDoctorName.Text, txttests.Text);
        gridreport.DataBind();
        int PCount = 0;
        for (int i = 0; i < gridreport.Rows.Count; i++)
        {

            if (i == 0)
            {
                PCount = PCount + 1;
            }
            if (i > 0)
            {
                if (gridreport.Rows[i].Cells[1].Text.Trim() == gridreport.Rows[i - 1].Cells[1].Text.Trim())
                {
                    ViewState["regno"] = gridreport.Rows[i].Cells[1].Text.Trim();
                    gridreport.Rows[i].Cells[0].Text = "";
                    gridreport.Rows[i].Cells[1].Text = "";
                    gridreport.Rows[i].Cells[2].Text = "";
                    gridreport.Rows[i].Cells[3].Text = "";
                    gridreport.Rows[i].Cells[4].Text = "";
                    gridreport.Rows[i].Cells[5].Text = "";
                    gridreport.Rows[i].Cells[6].Text = "";
                    gridreport.Rows[i].Cells[7].Text = "";
                    gridreport.Rows[i].Cells[11].Text = "";
                    gridreport.Rows[i].Cells[10].Text = "";
                    gridreport.Rows[i].Cells[12].Text = "";
                    gridreport.Rows[i].Cells[13].Text = "";

                }
                if (ViewState["regno"].ToString() == gridreport.Rows[i].Cells[1].Text.Trim())
                {
                    gridreport.Rows[i].Cells[0].Text = "";
                    gridreport.Rows[i].Cells[1].Text = "";
                    gridreport.Rows[i].Cells[2].Text = "";
                    gridreport.Rows[i].Cells[3].Text = "";
                    gridreport.Rows[i].Cells[4].Text = "";
                    gridreport.Rows[i].Cells[5].Text = "";
                    gridreport.Rows[i].Cells[6].Text = "";
                    gridreport.Rows[i].Cells[7].Text = "";
                    gridreport.Rows[i].Cells[11].Text = "";
                    gridreport.Rows[i].Cells[10].Text = "";
                    gridreport.Rows[i].Cells[12].Text = "";
                    gridreport.Rows[i].Cells[13].Text = "";
                }
                if (gridreport.Rows[i].Cells[0].Text != "")
                {

                    PCount = PCount + 1;
                }
            }
            try
            {
                //if (gridreport.Rows[i].Cells[10].Text.Trim() == "&nbsp;" ||gridreport.Rows[i].Cells[10].Text.Trim() =="")
                //{
                //    gridreport.Rows[i].Cells[10].Text = (gridreport.Rows[i].FindControl("hdntestcharges") as HiddenField).Value.ToString();
                //}
               // if (Convert.ToInt32(gridreport.Rows[i].Cells[10].Text.Trim()) > 1)
               // {
                    // gridreport.Rows[i].BackColor = System.Drawing.Color.Red;
                    // gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
                    // gridreport.Rows[i].Font.Bold = true;
                //}
            }
            catch (Exception ee)
            { }

         int isphebocoll= Convert.ToInt32((gridreport.Rows[i].FindControl("hdnisphebocollect") as HiddenField).Value);
         int isSpecimanNo = Convert.ToInt32((gridreport.Rows[i].FindControl("hdnSpecimanNo") as HiddenField).Value);
         int ISpheboAccept = Convert.ToInt32((gridreport.Rows[i].FindControl("hdn_ISpheboAccept") as HiddenField).Value);
         string PatTesStatus = Convert.ToString((gridreport.Rows[i].FindControl("hdn_PatStatus") as HiddenField).Value);
         bool printstatus = Convert.ToBoolean((gridreport.Rows[i].FindControl("hdprintstatus") as HiddenField).Value);

         if (Convert.ToString((gridreport.Rows[i].FindControl("hdn_UploadPrescription") as HiddenField).Value) == "")
         {
             
             (gridreport.Rows[i].FindControl("Hyp_viewPres") as HyperLink).Visible = false;
             
         }
         if (isphebocoll == 1)
         {
             gridreport.Rows[i].Cells[9].Text = "<span class='btn btn-xs btn-warning'>SamColl</span>";
         }
         if (isphebocoll == 2)
         {
             gridreport.Rows[i].Cells[9].Text = "<span class='btn btn-xs btn-danger'>SamRej</span>";
         }
         if (ISpheboAccept == 1)
         {
             gridreport.Rows[i].Cells[9].Text = "<span class='btn btn-xs btn-success'>SamAcc</span>";
         }
         if (ISpheboAccept == 1 && Convert.ToString(PatTesStatus) != "Registered")
         {
             if (Convert.ToBoolean(printstatus) == true)
             {
                 // gridreport.Rows[i].BackColor = System.Drawing.Color.Green;
                 // gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
                 gridreport.Rows[i].Cells[9].Text = "<span class='btn btn-xs btn-success' >Prin</span>";
                 //gridreport.Rows[i].Font.Bold = true;
             }
             else if (Convert.ToString(PatTesStatus) == "Registered")
             {
                 // gridreport.Rows[i].BackColor = System.Drawing.Color.Red;
                 // gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
                 gridreport.Rows[i].Cells[9].Text = "<span class='btn btn-xs btn-danger' >Reg</span>";
                 //gridreport.Rows[i].Font.Bold = true;
             }
             else if (Convert.ToString(PatTesStatus) == "Tested")
             {
                 //gridreport.Rows[i].BackColor = System.Drawing.Color.Orange;
                 // gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
                 //gridreport.Rows[i].Font.Bold = true;
                 gridreport.Rows[i].Cells[9].Text = "<span class='btn btn-xs btn-warning' >Tes</span>";
             }
             else if (Convert.ToString(PatTesStatus) == "Authorized")
             {
                 //gridreport.Rows[i].BackColor = System.Drawing.Color.Blue;
                 // gridreport.Rows[i].ForeColor = System.Drawing.Color.White;
                 //gridreport.Rows[i].Font.Bold = true;
                 gridreport.Rows[i].Cells[9].Text = "<span class='btn btn-xs btn-primary' >Auth</span>";
             }
         }
         if (ISpheboAccept == 0 && Convert.ToString(PatTesStatus) == "Registered" && isphebocoll == 0)
         {
             gridreport.Rows[i].Cells[9].Text = "<span class='btn btn-xs btn-danger' >Reg</span>";
         }
            
        }
        LblPcount.Text = Convert.ToString("Patients found with selected criteria -: " + PCount);
    }

    protected void btnList_Click(object sender, EventArgs e)
    {
        BindGrid();
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
            sda = new SqlDataAdapter("SELECT rtrim(intial)+' '+Patname as Patname FROM patmst where Patname like  N'%" + prefixTextNew + "%' and branchid=" + branchid + " order by Patname", con);

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

    protected void ddlcenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CenterCode"] = ddlcenter.SelectedValue;
    }

    protected void gridreport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
        // int i=e.Row.RowIndex;
        //if (e.Row.RowIndex == 0)
        //    {
        //        //PCount = PCount + 1;
        //    }
        //if (i > 0)
        //{
        //    if (gridreport.Rows[i].Cells[1].Text.Trim() == gridreport.Rows[i - 1].Cells[1].Text.Trim())
        //    {
        //        ViewState["regno"] = gridreport.Rows[i].Cells[1].Text.Trim();
        //        gridreport.Rows[i].Cells[0].Text = "";
        //        gridreport.Rows[i].Cells[1].Text = "";
        //        gridreport.Rows[i].Cells[2].Text = "";
        //        gridreport.Rows[i].Cells[3].Text = "";
        //        gridreport.Rows[i].Cells[4].Text = "";
        //        gridreport.Rows[i].Cells[5].Text = "";
        //        gridreport.Rows[i].Cells[6].Text = "";
        //        gridreport.Rows[i].Cells[7].Text = "";
        //        gridreport.Rows[i].Cells[11].Text = "";
        //        gridreport.Rows[i].Cells[10].Text = "";

        //    }
        //    if (ViewState["regno"].ToString() == gridreport.Rows[i].Cells[1].Text.Trim())
        //    {
        //        gridreport.Rows[i].Cells[0].Text = "";
        //        gridreport.Rows[i].Cells[1].Text = "";
        //        gridreport.Rows[i].Cells[2].Text = "";
        //        gridreport.Rows[i].Cells[3].Text = "";
        //        gridreport.Rows[i].Cells[4].Text = "";
        //        gridreport.Rows[i].Cells[5].Text = "";
        //        gridreport.Rows[i].Cells[6].Text = "";
        //        gridreport.Rows[i].Cells[7].Text = "";
        //        gridreport.Rows[i].Cells[11].Text = "";
        //        gridreport.Rows[i].Cells[10].Text = "";
        //    }
        //}
        string status = e.Row.Cells[8].Text.Trim();
        HiddenField hd = (e.Row.FindControl("hdprintstatus") as HiddenField);
        bool PrintStatus = Convert.ToBoolean(hd.Value);
        if (PrintStatus == true)
        {
            e.Row.Cells[9].Text = "Printed";
        }

        try
        {
            // e.Row.Cells[0].Text = Convert.ToDateTime(e.Row.Cells[0].Text.Trim()).ToShortDateString();
            string Wstatus = Convert.ToString((e.Row.FindControl("hdnWhStatus") as HiddenField).Value);
            if (Convert.ToString(Wstatus) != "NA")
            {
                e.Row.Cells[21].Text = "<span class='btn btn-xs btn-success' >WhatApp Sent</span>";
            }
            else
            {
                e.Row.Cells[21].Text = "<span class='btn btn-xs btn-danger' >WhatApp Not Sent</span>";
            }
            string Mailstatus = Convert.ToString(e.Row.Cells[14].Text);
            if (Mailstatus == "Send Email")
            {
                e.Row.Cells[14].Text = "<span class='btn btn-xs btn-success' >send mail</span>";

            }
            else
            {
                e.Row.Cells[14].Text = "<span class='btn btn-xs btn-danger' >Not send mail</span>";
            }

            bool Emg = Convert.ToBoolean((e.Row.FindControl("isemergency") as HiddenField).Value);
            if (Emg == true)
            {
                (e.Row.FindControl("btnEmergency") as ImageButton).Visible = true;
            }
            else
            {
                (e.Row.FindControl("btnEmergency") as ImageButton).Visible = false;
            }
        }

        catch (Exception exx) { }
        if (e.Row.Cells[8].Text.Trim() == "Printed")
        {
        }
    }

    protected void gridreport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridreport.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillTests(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
            sda = new SqlDataAdapter("select PackageCode as MTCode,PackageName as Maintestname from PackMst where (PackageCode like '%" + prefixTextNew + "%' or PackageName like '%" + prefixTextNew + "%') and PackageCode in (select PackageCode from PackmstD where SDCode in (select SDCode from SubDepartment where DigModule ='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "')) UNION " +
                               " select MTCode, Maintestname from MainTest WHERE (MTCode like '%" + prefixTextNew + "%' or Maintestname like '%" + prefixTextNew + "%') and SDCode in (select SDCode from SubDepartment where DigModule='" + Convert.ToInt32(HttpContext.Current.Session["DigModule"]) + "') order by Maintestname ", con);
        else
            sda = new SqlDataAdapter("select PackageCode as MTCode, PackageName as Maintestname from PackMst WHERE PackageCode like '%" + prefixTextNew + "%' or PackageName like '%" + prefixTextNew + "%' UNION " +
                                " select MTCode, Maintestname from MainTest WHERE MTCode like '%" + prefixTextNew + "%' or Maintestname like '%" + prefixTextNew + "%' order by Maintestname ", con);
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

        string collectioncode = HttpContext.Current.Session["CenterCode"].ToString();
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
            sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR' and ( DoctorName like N'" + prefixTextNew + "%' or DoctorCode like N'" + prefixTextNew + "%' ) ", con);
        else
            sda = new SqlDataAdapter("SELECT DoctorCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR'and ( DoctorName like N'" + prefixTextNew + "%' or DoctorCode like N'" + prefixTextNew + "%' ) ", con);
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
    protected void txttests_TextChanged(object sender, EventArgs e)
    {
        if (txttests.Text.Trim() != "")
        {
            string[] TestCode = new string[] { "", "" };
            TestCode = txttests.Text.Split('-');
            if (TestCode.Length == 2)
            {
                txttests.Text = TestCode[1];
            }
            if (TestCode.Length == 3)
            {
                txttests.Text = TestCode[1]+"-"+TestCode[2];
            }
        }
    }
    protected void txtDoctorName_TextChanged(object sender, EventArgs e)
    {
        if (txtDoctorName.Text.Trim() != "")
        {
            string[] DocCode = new string[] { "", "" };
            DocCode = txtDoctorName.Text.Split('=');
            txtDoctorName.Text = DocCode[0];
        }
    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            labcode_main = labcode;
        }
        if (txtPatientName.Text.Trim() != "")
        {
            try
            {
                if (txtPatientName.Text == "All")
                {
                    patientName = txtPatientName.Text;
                }
                else
                {
                    //patient = txtPatientName.Text.Split(' ');
                    //string name = patient[1];
                    //patientName = name.Trim();
                    //patientName = txtPatientName.Text;
                    patientName = txtPatientName.Text.Trim().Replace("'", "'+char(39)+'");
                }
            }
            catch
            {
                txtPatientName.Text = "";
                return;
            }
        }
        if (txtDoctorName.Text.Trim() != "")
        {
            string[] DocCode = new string[] { "", "" };
            DocCode = txtDoctorName.Text.Split('=');
            txtDoctorName.Text = DocCode[0];
        }
        if ((fromdate.Text != "" && todate.Text != "") || txtregno.Text.Trim() != "")
        {
            stDate = DateTimeConvesion.getDateFromString(fromdate.Text);
            endDate = DateTimeConvesion.getDateFromString(todate.Text);

            #region AlterView
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = conn.CreateCommand();
            sc.CommandText = "ALTER VIEW VW_patrprtvwd AS  SELECT distinct    dbo.patmst.PatRegID, RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS Name, dbo.patmst.sex,    dbo.patmst.Age, dbo.patmst.MDY, ''as PatientcHistory, " +
                          "  dbo.patmst.Drname,  dbo.patmst.Centername,   dbo.MainTest.Maintestname,  dbo.patmst.Centercode, dbo.patmst.Phrecdate, dbo.patmst.Patname, dbo.patmst.branchid,    " +
                          "  dbo.patmst.FID, patmst.TestCharges + SUM(ISNULL(RecM.OtherCharges, 0)) - (sum(dbo.RecM.AmtPaid) + sum(dbo.RecM.DisAmt)) AS OutStAmt, dbo.MainTest.SDCode,  " +
                          "  dbo.patmst.TestCharges +sum( ISNULL(RecM.Othercharges, 0))as TestCharges ,   " +
                          "  convert(varchar(20),dbo.patmst.Patregdate,103)+' '+convert(varchar(20),convert(time,dbo.patmst.Patregdate),100) as Patregdate,  " +
                          "  dbo.patmst.PPID , patmst.UnitCode ,ISNULL( patmstd.Patrepstatus,0)as Patrepstatus,    ISNULL( patmstd.Patauthicante, 'Registered')as status, " +
                          "  patmstd.BarcodeID, patmst.Patphoneno  ,   patmst.Isemergency,case when PatientEmail=1 then 'Send Email' else 'Not Send' end as MailStatus ,  " +
                          "  isnull(patmstd.SpecimanNo ,0)as SpecimanNo, patmstd.isbarcodeprint, patmstd.PhlebotomistCollect,  patmstd.PhlebotomistRejectremark, " +
                          "  patmstd.SampleAcceptDate,patmst.ISCallPatient,patmstd.ISpheboAccept,patmst.UploadPrescription ,sum(dbo.RecM.AmtPaid) as PaidAmt, sum(dbo.RecM.DisAmt) as DiscAmt , Patmst.UserName as CreatedBy ,patmst.Pataddress, patmst.EmailID, patmst.Remark, patmst.Email, patmst.OtherRefDoctor " +
                            " FROM         dbo.patmst INNER JOIN   dbo.patmstd ON dbo.patmst.PID = dbo.patmstd.PID INNER JOIN   dbo.MainTest ON dbo.patmstd.MTCode = dbo.MainTest.MTCode  " +
                         "   LEFT OUTER JOIN   dbo.RecM ON dbo.patmst.PID = dbo.RecM.PID   " +
                             "  where (patmst.IsActive = 1) and patmstd.TestDeActive=0  and patmst.Phrecdate between '" + Convert.ToDateTime(stDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' group by  " +
                         "   dbo.patmst.PatRegID, RTRIM(dbo.patmst.intial) , dbo.patmst.Patname , dbo.patmst.sex,    dbo.patmst.Age, dbo.patmst.MDY,  " +
                         "   dbo.patmst.Drname,  dbo.patmst.Centername,   dbo.MainTest.Maintestname,  dbo.patmst.Centercode, dbo.patmst.Phrecdate, dbo.patmst.Patname, dbo.patmst.branchid,   " +
                         "   dbo.patmst.FID, dbo.patmst.TestCharges , dbo.MainTest.SDCode,     " +
                         "   convert(varchar(20),dbo.patmst.Patregdate,103),convert(varchar(20),convert(time,dbo.patmst.Patregdate),100) ,  " +
                         "   dbo.patmst.PPID , patmst.UnitCode ,ISNULL( patmstd.Patrepstatus,0),    ISNULL( patmstd.Patauthicante, 'Registered'), " +
                         "   patmstd.BarcodeID, patmst.Patphoneno  ,   patmst.Isemergency,case when PatientEmail=1 then 'Send Email' else 'Not Send' end , " +
                         "   isnull(patmstd.SpecimanNo ,0), patmstd.isbarcodeprint, patmstd.PhlebotomistCollect,  patmstd.PhlebotomistRejectremark, " +
                         "   patmstd.SampleAcceptDate,patmst.ISCallPatient,patmstd.ISpheboAccept,patmst.UploadPrescription,Patmst.UserName ,patmst.Pataddress, patmst.EmailID, patmst.Remark, patmst.Email, patmst.OtherRefDoctor ";
            conn.Open();
            sc.ExecuteNonQuery();
            conn.Close(); conn.Dispose();


            if (ddlStatusAll.SelectedValue != "0")
            {
                status = ddlStatusAll.SelectedItem.Text.Trim();
            }
            if (txtregno.Text.Trim() != "")
            {
                Pat_Regid = txtregno.Text.Trim();

            }
            if (drp_deparment.SelectedValue != "0")
            {
                subdeptName = drp_deparment.SelectedValue;
            }

            Barcode = "";

            if (txtmobileno.Text.Trim() != "")
            {
                Mno = txtmobileno.Text.Trim();
            }
            else
            {
                Mno = "";
            }


            ViewState["regno"] = "0";
            string CenCode = "";
            if (ddlcenter.SelectedItem.Text != "All Center")
            {

                string[] CenCodeN = ddlcenter.SelectedItem.Text.Split('=');
                CenCode = CenCodeN[0];
            }

            SqlConnection conn1 = DataAccess.ConInitForDC();
            SqlCommand sc1 = conn1.CreateCommand();
            sc1.CommandText = "ALTER VIEW VW_PatientTestStatusReport AS  SELECT distinct   * from VW_patrprtvwd " +

                             "  where  1=1    ";

            //if (fromdate.Text != "")
            //{
            //                }


            if (Pat_Regid != "")
            {
                sc1.CommandText += "  and PatRegID='" + Pat_Regid + "'";
                if (labcode_main != null && labcode_main != "")
                {
                    sc1.CommandText += " and UnitCode='" + labcode_main + "'";

                }
            }
            else
            {
                if (patientName != "")
                {
                    sc1.CommandText += " and Patname like  N'%" + patientName + "%'";
                    if (labcode_main != null && labcode_main != "")
                    {
                        sc1.CommandText += " and UnitCode='" + labcode_main + "'";

                    }
                }
                else
                {
                    if (status != "All" && status != "" && status != null)
                    {
                        if (status == "Authorized")
                        {
                            sc1.CommandText += " and status='" + status + "'";
                        }
                        if (status == "Registered")
                        {
                            sc1.CommandText += " and status='" + status + "' and PhlebotomistCollect=0 and SpecimanNo=0 and ISpheboAccept=0";
                        }
                        else if (status == "Completed")
                        {
                            sc1.CommandText += "  and Patrepstatus=1";
                        }
                        else if (status == "Dispatch")
                        {
                            sc1.CommandText += "  and ISCallPatient=1";
                        }
                        else if (status == "SamCollect")
                        {
                            sc1.CommandText += "  and PhlebotomistCollect=1 and  ISpheboAccept=0";
                        }
                        else if (status == "SamAccept")
                        {
                            sc1.CommandText += "  and ISpheboAccept=1";
                        }
                        else if (status == "SamReject")
                        {
                            sc1.CommandText += "  and PhlebotomistCollect=2";
                        }
                        else
                        {
                            sc1.CommandText += " and status='" + status + "' and Patrepstatus=0";
                        }
                    }
                    if (subdeptName != "")
                    {
                        sc1.CommandText += " and SDCode='" + subdeptName + "'";
                    }
                    if (fromdate.Text != null && todate.Text != null)
                    {
                        sc1.CommandText += "  and (CAST(CAST(YEAR(Phrecdate) AS varchar(4)) + '/' + CAST(MONTH(Phrecdate) AS varchar(2)) + '/' + CAST(DAY(Phrecdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
                    }
                    if (labcode_main != null && labcode_main != "")
                    {
                        sc1.CommandText += " and UnitCode='" + labcode_main + "'";

                    }
                }
            }
            if (CenCode != null && CenCode.ToString() != "0" && CenCode != "")
            {
                sc1.CommandText += " and CenterCode=N'" + CenCode.ToString() + "'";
            }
            if (Barcode != "")
            {
                sc1.CommandText += " AND BarcodeID ='" + Barcode + "' ";
            }
            if (Mno != "")
            {
                sc1.CommandText += " AND Patphoneno ='" + Mno + "' ";
            }
            if (txtDoctorName.Text != null && txtDoctorName.Text.ToString() != "0" && txtDoctorName.Text != "")
            {
                sc1.CommandText += " and LTRIM(Drname) =N'" + txtDoctorName.Text.Trim().ToString() + "'";
            }
            if (txttests.Text != null && txttests.Text.ToString() != "0" && txttests.Text != "")
            {
                sc1.CommandText += " and Maintestname='" + txttests.Text.Trim().ToString() + "'";
            }
            //sc1.CommandText += " order by phrecdate desc";

            conn1.Open();
            sc1.ExecuteNonQuery();
            conn1.Close(); conn1.Dispose();
            #endregion


            string sql = "";
            ReportParameterClass.ReportType = "PatientTestStatusReport";


            Session.Add("rptsql", sql);
            Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PatientTestStatusReport.rpt");
            Session["reportname"] = "Rpt_PatientTestStatusReport";
            Session["RPTFORMAT"] = "EXCEL";

            Session["Parameter"] = "Yes";
            Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
            Session["rptusername"] = Convert.ToString(Session["username"]);

            ReportParameterClass.SelectionFormula = sql;
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
        }

    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Test Status" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        gridreport.GridLines = GridLines.Both;
        gridreport.HeaderStyle.Font.Bold = true;
        gridreport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void gridreport_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Test status";
            // HeaderCell.Font = System.Drawing.Color.Black;

            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);
           // gridreport.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
    protected void ddlStatusAll_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnPdf_Click(object sender, EventArgs e)
    {
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            labcode_main = labcode;
        }
        if (txtPatientName.Text.Trim() != "")
        {
            try
            {
                if (txtPatientName.Text == "All")
                {
                    patientName = txtPatientName.Text;
                }
                else
                {
                    //patient = txtPatientName.Text.Split(' ');
                    //string name = patient[1];
                    //patientName = name.Trim();
                    patientName =   txtPatientName.Text.Trim().Replace("'", "'+char(39)+'");//txtPatientName.Text;
                }
            }
            catch
            {
                txtPatientName.Text = "";
                return;
            }
        }
        if (txtDoctorName.Text.Trim() != "")
        {
            string[] DocCode = new string[] { "", "" };
            DocCode = txtDoctorName.Text.Split('=');
            txtDoctorName.Text = DocCode[0];
        }
        if ((fromdate.Text != "" && todate.Text != "") || txtregno.Text.Trim() != "")
        {
            stDate = DateTimeConvesion.getDateFromString(fromdate.Text);
            endDate = DateTimeConvesion.getDateFromString(todate.Text);

            #region AlterView
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = conn.CreateCommand();
            sc.CommandText = "ALTER VIEW VW_patrprtvwd AS  SELECT distinct    dbo.patmst.PatRegID, RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS Name, dbo.patmst.sex,    dbo.patmst.Age, dbo.patmst.MDY, ''as PatientcHistory, " +
                          "  dbo.patmst.Drname,  dbo.patmst.Centername,   dbo.MainTest.Maintestname,  dbo.patmst.Centercode, dbo.patmst.Phrecdate, dbo.patmst.Patname, dbo.patmst.branchid,    " +
                          "  dbo.patmst.FID, patmst.TestCharges + SUM(ISNULL(RecM.OtherCharges, 0)) - (sum(dbo.RecM.AmtPaid) + sum(dbo.RecM.DisAmt)) AS OutStAmt, dbo.MainTest.SDCode,  " +
                          "  dbo.patmst.TestCharges +sum( ISNULL(RecM.Othercharges, 0))as TestCharges ,   " +
                          "  convert(varchar(20),dbo.patmst.Patregdate,103)+' '+convert(varchar(20),convert(time,dbo.patmst.Patregdate),100) as Patregdate,  " +
                          "  dbo.patmst.PPID , patmst.UnitCode ,ISNULL( patmstd.Patrepstatus,0)as Patrepstatus,    ISNULL( patmstd.Patauthicante, 'Registered')as status, " +
                          "  patmstd.BarcodeID, patmst.Patphoneno  ,   patmst.Isemergency,case when PatientEmail=1 then 'Send Email' else 'Not Send' end as MailStatus ,  " +
                          "  isnull(patmstd.SpecimanNo ,0)as SpecimanNo, patmstd.isbarcodeprint, patmstd.PhlebotomistCollect,  patmstd.PhlebotomistRejectremark, " +
                          "  patmstd.SampleAcceptDate,patmst.ISCallPatient,patmstd.ISpheboAccept,patmst.UploadPrescription ,sum(dbo.RecM.AmtPaid) as PaidAmt, sum(dbo.RecM.DisAmt) as DiscAmt , Patmst.UserName as CreatedBy ,patmst.Pataddress, patmst.EmailID, patmst.Remark, patmst.Email, patmst.OtherRefDoctor " +
                            " FROM         dbo.patmst INNER JOIN   dbo.patmstd ON dbo.patmst.PID = dbo.patmstd.PID INNER JOIN   dbo.MainTest ON dbo.patmstd.MTCode = dbo.MainTest.MTCode  " +
                         "   LEFT OUTER JOIN   dbo.RecM ON dbo.patmst.PID = dbo.RecM.PID   " +
                             "  where (patmst.IsActive = 1) and patmstd.TestDeActive=0  and patmst.Phrecdate between '" + Convert.ToDateTime(stDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' group by  " +
                         "   dbo.patmst.PatRegID, RTRIM(dbo.patmst.intial) , dbo.patmst.Patname , dbo.patmst.sex,    dbo.patmst.Age, dbo.patmst.MDY,  " +
                         "   dbo.patmst.Drname,  dbo.patmst.Centername,   dbo.MainTest.Maintestname,  dbo.patmst.Centercode, dbo.patmst.Phrecdate, dbo.patmst.Patname, dbo.patmst.branchid,   " +
                         "   dbo.patmst.FID, dbo.patmst.TestCharges , dbo.MainTest.SDCode,     " +
                         "   convert(varchar(20),dbo.patmst.Patregdate,103),convert(varchar(20),convert(time,dbo.patmst.Patregdate),100) ,  " +
                         "   dbo.patmst.PPID , patmst.UnitCode ,ISNULL( patmstd.Patrepstatus,0),    ISNULL( patmstd.Patauthicante, 'Registered'), " +
                         "   patmstd.BarcodeID, patmst.Patphoneno  ,   patmst.Isemergency,case when PatientEmail=1 then 'Send Email' else 'Not Send' end , " +
                         "   isnull(patmstd.SpecimanNo ,0), patmstd.isbarcodeprint, patmstd.PhlebotomistCollect,  patmstd.PhlebotomistRejectremark, " +
                         "   patmstd.SampleAcceptDate,patmst.ISCallPatient,patmstd.ISpheboAccept,patmst.UploadPrescription,Patmst.UserName ,patmst.Pataddress, patmst.EmailID, patmst.Remark, patmst.Email, patmst.OtherRefDoctor ";
            conn.Open();
            sc.ExecuteNonQuery();
            conn.Close(); conn.Dispose();


            if (ddlStatusAll.SelectedValue != "0")
            {
                status = ddlStatusAll.SelectedItem.Text.Trim();
            }
            if (txtregno.Text.Trim() != "")
            {
                Pat_Regid = txtregno.Text.Trim();

            }
            if (drp_deparment.SelectedValue != "0")
            {
                subdeptName = drp_deparment.SelectedValue;
            }

            Barcode = "";

            if (txtmobileno.Text.Trim() != "")
            {
                Mno = txtmobileno.Text.Trim();
            }
            else
            {
                Mno = "";
            }


            ViewState["regno"] = "0";
            string CenCode = "";
            if (ddlcenter.SelectedItem.Text != "All Center")
            {

                string[] CenCodeN = ddlcenter.SelectedItem.Text.Split('=');
                CenCode = CenCodeN[0];
            }

            SqlConnection conn1 = DataAccess.ConInitForDC();
            SqlCommand sc1 = conn1.CreateCommand();
            sc1.CommandText = "ALTER VIEW VW_PatientTestStatusReport AS  SELECT distinct   * from VW_patrprtvwd " +

                             "  where  1=1    ";
                      
            //if (fromdate.Text != "")
            //{
            //                }


            if (Pat_Regid  != "")
            {
                sc1.CommandText += "  and PatRegID='" + Pat_Regid + "'";
                if (labcode_main != null && labcode_main != "")
                {
                    sc1.CommandText += " and UnitCode='" + labcode_main + "'";

                }
            }
            else
            {
                if (patientName != "")
                {
                    sc1.CommandText += " and Patname like  N'%" + patientName + "%'";
                    if (labcode_main != null && labcode_main != "")
                    {
                        sc1.CommandText += " and UnitCode='" + labcode_main + "'";

                    }
                }
                else
                {
                    if (status != "All" && status != "" && status != null)
                    {
                        if (status == "Authorized")
                        {
                            sc1.CommandText += " and status='" + status + "'";
                        }
                        if (status == "Registered")
                        {
                            sc1.CommandText += " and status='" + status + "' and PhlebotomistCollect=0 and SpecimanNo=0 and ISpheboAccept=0";
                        }
                        else if (status == "Completed")
                        {
                            sc1.CommandText += "  and Patrepstatus=1";
                        }
                        else if (status == "Dispatch")
                        {
                            sc1.CommandText += "  and ISCallPatient=1";
                        }
                        else if (status == "SamCollect")
                        {
                            sc1.CommandText += "  and PhlebotomistCollect=1 and  ISpheboAccept=0";
                        }
                        else if (status == "SamAccept")
                        {
                            sc1.CommandText += "  and ISpheboAccept=1";
                        }
                        else if (status == "SamReject")
                        {
                            sc1.CommandText += "  and PhlebotomistCollect=2";
                        }
                        else
                        {
                            sc1.CommandText += " and status='" + status + "' and Patrepstatus=0";
                        }
                    }
                    if (subdeptName != "")
                    {
                        sc1.CommandText += " and SDCode='" + subdeptName + "'";
                    }
                    if (fromdate.Text != null && todate.Text != null)
                    {
                        sc1.CommandText += "  and (CAST(CAST(YEAR(Phrecdate) AS varchar(4)) + '/' + CAST(MONTH(Phrecdate) AS varchar(2)) + '/' + CAST(DAY(Phrecdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
                    }
                    if (labcode_main != null && labcode_main != "")
                    {
                        sc1.CommandText += " and UnitCode='" + labcode_main + "'";

                    }
                }
            }
            if (CenCode != null && CenCode.ToString() != "0" && CenCode != "")
            {
                sc1.CommandText += " and CenterCode=N'" + CenCode.ToString() + "'";
            }
            if (Barcode != "")
            {
                sc1.CommandText += " AND BarcodeID ='" + Barcode + "' ";
            }
            if (Mno != "")
            {
                sc1.CommandText += " AND Patphoneno ='" + Mno + "' ";
            }
            if (txtDoctorName.Text != null && txtDoctorName.Text.ToString() != "0" && txtDoctorName.Text != "")
            {
                sc1.CommandText += " and LTRIM(Drname) =N'" + txtDoctorName.Text.Trim().ToString() + "'";
            }
            if (txttests.Text != null && txttests.Text.ToString() != "0" && txttests.Text != "")
            {
                sc1.CommandText += " and Maintestname='" + txttests.Text.Trim().ToString() + "'";
            }
            //sc1.CommandText += " order by phrecdate desc";

            conn1.Open();
            sc1.ExecuteNonQuery();
            conn1.Close(); conn1.Dispose();
            #endregion


            string sql = "";
            ReportParameterClass.ReportType = "PatientTestStatusReport";


            Session.Add("rptsql", sql);
            Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PatientTestStatusReport.rpt");
            Session["reportname"] = "Rpt_PatientTestStatusReport";
            Session["RPTFORMAT"] = "pdf";

            Session["Parameter"] = "Yes";
            Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
            Session["rptusername"] = Convert.ToString(Session["username"]);

            ReportParameterClass.SelectionFormula = sql;
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
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