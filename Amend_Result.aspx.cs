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

public partial class Amend_Result :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    object fromDate = null, toDate = null;
    string status = "", MTCode = "", patientName = "", Pat_Regid = "", BarcodeID = "", CenterCode = "", Mno = "", CenterCodeNew = "";
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    private static int PageSize = 20;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("Testresultentry.aspx?type=Test");
                    }
                }
               
                if (Request.QueryString["Type"] != null)
                {
                    string info = Request.QueryString["Type"].ToString();
                    Session["Type"] = info;
                }
                if (Request.QueryString["PatRegID"] != null)
                {
                    //txtregno.Text = Request.QueryString["PatRegID"].Trim();

                }
                //if (Request.QueryString["Maindept"] != null)
                //{
                //  Session["DigModule"] = Request.QueryString["Maindept"].Trim();

                //}
                DataTable dtban = new DataTable();
                dtban = ObjTB.Bindbanner();
                if (dtban.Rows.Count > 0)
                {
                    if (Convert.ToString(dtban.Rows[0]["Type"]) == "0")
                    {
                        ViewState["VALIDATE"] = "YES";
                        //ddlStatus.Items[9].Selected = true;
                       // ddlStatus.Items[0].Selected = true;
                        ChkPending.Checked = true;
                        //fromdate.Text = Date.getdate().AddMonths(-1).ToString("dd/MM/yyyy");
                       fromdate.Text = Date.getdate().AddDays(-2).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        ViewState["VALIDATE"] = "NO";
                       // ddlStatus.Items[0].Selected = true;
                        ChkPending.Checked = true;
                        fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                    }
                }
               
                if (Request.QueryString["CenterCode"] != null)
                {
                    if (Request.QueryString["CenterCode"].Trim() != "")
                    {

                    }
                }
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                if (Convert.ToString( Session["usertype"]) == "CollectionCenter" || Convert.ToString( Session["usertype"]) == "Collection Center")
                {

                   // txtcentername_new.Enabled = false;
                    string Centercode = Session["CenterCode"].ToString();
                    string Center = Patmst_Bal_C.getname(Centercode, Convert.ToInt32(Session["Branchid"]));
                    txtcentername_new.Text = Center;
                    txtcentername_new.Enabled = false;
                }
                if (Convert.ToString(Request.QueryString["TS"]) != "" && Convert.ToString(Request.QueryString["TS"]) != null)
                {
                   string  Pstatus = Request.QueryString["TS"].Trim();
                   if (Pstatus == "Pending")
                   {
                      // ddlStatus.Items[0].Selected = true;
                       ChkPending.Checked = true;
                   }
                   else if (Pstatus == "Completed")
                   {
                      // ddlStatus.Items[1].Selected = true;
                       ChkCompleted.Checked = true;
                   }
                   else if (Pstatus == "Tested")
                   {
                       //ddlStatus.Items[2].Selected = true;
                       ChkTested.Checked = true;
                   }
                   else if (Pstatus == "Authorized")
                   {
                      // ddlStatus.Items[3].Selected = true;
                       ChkAuthorizs.Checked = true;
                   }
                   else if (Pstatus == "Emergency")
                   {
                       //ddlStatus.Items[4].Selected = true;
                       ChkEmergency.Checked = true;
                   }
                   else if (Pstatus == "IntRece")
                   {
                       //ddlStatus.Items[5].Selected = true;
                       ChkIntRece.Checked = true;
                   }
                   else if (Pstatus == "IntNotRece")
                   {
                      // ddlStatus.Items[6].Selected = true;
                       ChkIntNotReceive.Checked = true;
                   }
                   else if (Pstatus == "Outsource")
                   {
                       //ddlStatus.Items[7].Selected = true;
                       ChkOutsource.Checked = true;
                   }
                   else if (Pstatus == "Abnormal")
                   {
                      // ddlStatus.Items[8].Selected = true;
                       ChkAbnormal.Checked = true;
                   }
                   else
                   {
                       //ddlStatus.Items[9].Selected = true;
                       ChkAll.Checked = true;
                   }
                   fromdate.Text = Request.QueryString["FDate"].Trim();
                   todate.Text = Request.QueryString["TDate"].Trim();

                   if (Request.QueryString["pname"] != null)
                   {
                       if (Request.QueryString["pname"].Trim() != "")
                       {
                           txtPatientName.Text = Request.QueryString["pname"].Trim();
                       }
                   }
                   if (Request.QueryString["sid"] != null)
                   {
                       if (Request.QueryString["sid"].Trim() != "")
                       {
                           txtregno.Text = Request.QueryString["sid"].Trim();
                       }
                   }
                }
                //BindDummyRow();
                btnList_Click(this, null);
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

    [WebMethod]
    [ScriptMethod]
    public static string[] Getcenter(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where  ( DoctorName like N'%" + prefixTextNew + "%' or DoctorCode like N'%" + prefixTextNew + "%' )  and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where ( DoctorName like N'%" + prefixTextNew + "%' or DoctorCode like N'%" + prefixTextNew + "%' ) and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            //tests.SetValue(dr["DoctorName"], i);
            tests.SetValue(dr["DoctorName"] + " = " + dr["DoctorCode"], i);
            i++;
        }
        return tests;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] GetReportDone(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM CTuser where  ( username like N'%" + prefixTextNew + "%' )  and branchid=" + branchid + " order by username", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM CTuser where ( username like N'%" + prefixTextNew + "%' ) and branchid=" + branchid + " order by username", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            //tests.SetValue(dr["DoctorName"], i);
            tests.SetValue(dr["username"] , i);
            i++;
        }
        return tests;
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnList_Click(this, null);
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] GetDeptName(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM subdepartment where subdeptName like  N'" + prefixTextNew + "%'  order by subdeptName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM subdepartment where subdeptName like  N'" + prefixTextNew + "%'  order by subdeptName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            //tests.SetValue(dr["DoctorName"], i);
            tests.SetValue(dr["subdeptName"], i);
            i++;
        }
        return tests;
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] GetDoctor(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        string prefixTextNew = prefixText.Replace("'", "'+char(39)+'");
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where  ( DoctorName like N'%" + prefixTextNew + "%' or DoctorCode like N'%" + prefixTextNew + "%' ) and DrType='DR' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where  ( DoctorName like N'%" + prefixTextNew + "%' or DoctorCode like N'%" + prefixTextNew + "%' ) and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            //tests.SetValue(dr["DoctorName"], i);
            tests.SetValue(dr["DoctorCode"] + " = " + dr["DoctorName"], i);
            i++;
        }
        return tests;
    }
  

    protected void GVTestentry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVTestentry.PageIndex = e.NewPageIndex;
        //BindGrid();
        btnList_Click(this, null);
    }

    protected void GVTestentry_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (e.NewEditIndex == -1)
                return;
            int i = e.NewEditIndex;
            string rno = (GVTestentry.Rows[e.NewEditIndex].FindControl("hdnPatRegID") as HiddenField).Value;
            string FID = (GVTestentry.Rows[e.NewEditIndex].FindControl("hdnFID1") as HiddenField).Value;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'TestReportprinting.aspx?PatRegID=" + rno + " &FID=" + FID + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
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

    protected void GVTestentry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GVTestentry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex == -1)
                return;
          
           
            string Pat_Regid = (e.Row.FindControl("hdnPatRegID") as HiddenField).Value.Trim();
            if (Pat_Regid != "")
            {
                if (e.Row.RowIndex > 0)
                {
                    //if ((e.Row.FindControl("hdnPatRegID") as HiddenField).Value.Trim() == (e.Row.FindControl("hdnPatRegID") as HiddenField).Value.Trim())
                    //{
                    //    string Pat_Regid = (e.Row.FindControl("hdnPatRegID") as HiddenField).Value.Trim();
                    //}
                }
                string FID = (e.Row.FindControl("hdnFID1") as HiddenField).Value.Trim();
                int PID = Convert.ToInt32((e.Row.FindControl("HDPID") as HiddenField).Value.Trim());
                int hdnMaindept = Convert.ToInt32((e.Row.FindControl("hdnMaindept") as HiddenField).Value.Trim());
                string hdn_Maintestname = Convert.ToString((e.Row.FindControl("hdn_Maintestname") as HiddenField).Value.Trim());
                bool Emg1 = Convert.ToBoolean((e.Row.FindControl("hdnis_emergency") as HiddenField).Value);
                string hdn_MTcode = Convert.ToString((e.Row.FindControl("hdn_MTcode") as HiddenField).Value.Trim());
                string hdnopid = Convert.ToString((e.Row.FindControl("hdnopid") as HiddenField).Value.Trim());
                bool hdnORT = Convert.ToBoolean((e.Row.FindControl("hdnORT") as HiddenField).Value.Trim());
                int Percent = Convert.ToInt32((e.Row.FindControl("HdnPercent") as HiddenField).Value.Trim());
                string Mailstatus = Convert.ToString(e.Row.Cells[15].Text);
                string PEDate = Convert.ToString((e.Row.FindControl("hdn_PEDate") as HiddenField).Value.Trim());
                if (Percent > 30)
                {
                    e.Row.Cells[23].Text = "<span class='btn btn-xs btn-danger' >" + e.Row.Cells[23].Text + "</span>";
                }
                if (Percent > 0 && Percent < 30)
                {
                    e.Row.Cells[23].Text = "<span class='btn btn-xs btn-warning' >" + e.Row.Cells[23].Text + "</span>";
                }
                //if (Percent > 0 && Percent < 50)
                //{
                //    e.Row.Cells[23].Text = "<span class='btn btn-xs btn-primary' >" + e.Row.Cells[23].Text + "</span>";
                //}
                if (Percent==0)
                {
                    e.Row.Cells[23].Text = "<span class='btn btn-xs btn-success' >" + e.Row.Cells[23].Text + "</span>";
                }
                if (Mailstatus == "Send Email")
                {
                    e.Row.Cells[18].Text = "<span class='btn btn-xs btn-success' >send</span>";

                }
                else
                {
                    e.Row.Cells[18].Text = "<span class='btn btn-xs btn-danger' >No</span>";
                }
                //string PanicResult = Convert.ToString(e.Row.Cells[19].Text);
                string PanicResult = (e.Row.FindControl("lblpanic") as Label).Text;
                if (PanicResult == "NA")
                {
                    (e.Row.FindControl("lblpanic") as Label).Text = "<span class='btn btn-xs btn-success' >NA</span>";
                    (e.Row.FindControl("btnpanic") as ImageButton).Visible = false;

                }
                else
                {
                    (e.Row.FindControl("lblpanic") as Label).Text = "<span class='btn btn-xs btn-danger' >Panic</span>";
                    (e.Row.FindControl("btnpanic") as ImageButton).Visible = true;
                }
                string DrMailstatus = Convert.ToString(e.Row.Cells[18].Text);
                //if (DrMailstatus == "Send DrEmail")
                //{
                //    e.Row.Cells[18].Text = "<span class='btn btn-xs btn-success' >Yes</span>";

                //}
                //else
                //{
                //    e.Row.Cells[18].Text = "<span class='btn btn-xs btn-danger' >No</span>";
                //}
                //bool Outsource = Convert.ToBoolean(e.Row.Cells[19].Text);
                //if (Outsource == true)
                //{
                //    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-success' >Yes</span>";

                //}              

                //else if (hdnopid != "0" && hdnORT == false)
                //{
                //    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-danger' >O-Pat</span>";

                //}
                //else if (hdnORT == true)
                //{
                //    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-success' >Transfer</span>";

                //}
                //else
                //{
                //    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-danger' >NA</span>";
                //}
                string IntResult = Convert.ToString(e.Row.Cells[19].Text);
                if (IntResult == "Yes")
                {
                    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-success' >Yes</span>";

                }
                else
                {
                    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-danger' >No</span>";
                }

                
                if (Emg1 == true)
                {
                    (e.Row.FindControl("btnEmergency") as ImageButton).Visible = true;
                }
                else
                {
                    (e.Row.FindControl("btnEmergency") as ImageButton).Visible = false;
                }

                if (Session["Type"] != null)
                {

                    if (Session["Type"].ToString() == "Cyto")
                    {
                       // (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntryCyto" + "&Maindept=" + hdnMaindept;

                    }
                    else if (Session["Type"].ToString() == "Histo")
                    {
                       // (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID +  "&RepType=TestResultEntryHisto" + "&Maindept=" + hdnMaindept;

                    }
                    else
                    {
                       // (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntry" + "&Maindept=" + hdnMaindept + "&CN=" + CenterCodeNew + "&TS=" + status + "&FrDate=" + fromdate.Text + "&ToDate=" + todate.Text + "&DeptName=" + txttestname.Text + "&EDate=" + PEDate + "&pname=" + txtPatientName.Text + "&sid=" + txtregno.Text.Trim();

                        //(e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntry" + "&Maindept=" + hdnMaindept + "&CN=" + CenterCodeNew + "&TS=" + status + "&FrDate=" + fromdate.Text + "&ToDate=" + todate.Text + "&EDate=" + PEDate + "&DeptName=" + txttestname.Text;
                        (e.Row.Cells[10].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntry" + "&Maindept=" + hdnMaindept + "&CN=" + CenterCodeNew + "&TS=" + status + "&FrDate=" + fromdate.Text + "&ToDate=" + todate.Text + "&EDate=" + PEDate + "&DeptName=" + txttestname.Text + "&TestCode=" + hdn_MTcode + "&pname=" + txtPatientName.Text + "&sid=" + txtregno.Text.Trim() + "&Amend=YES";

                    }
                }
                else
                {
                   // (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntry" + "&Maindept=" + hdnMaindept + "&CN=" + CenterCodeNew + "&TS=" + status + "&FrDate=" + fromdate.Text + "&ToDate=" + todate.Text + "&DeptName=" + txttestname.Text + "&EDate=" + PEDate + "&pname=" + txtPatientName.Text + "&sid=" + txtregno.Text.Trim();

                   // (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntry" + "&Maindept=" + hdnMaindept + "&CN=" + CenterCodeNew + "&TS=" + status + "&FrDate=" + fromdate.Text + "&ToDate=" + todate.Text + "&EDate=" + PEDate;
                    (e.Row.Cells[10].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntry" + "&Maindept=" + hdnMaindept + "&CN=" + CenterCodeNew + "&TS=" + status + "&FrDate=" + fromdate.Text + "&ToDate=" + todate.Text + "&EDate=" + PEDate + "&TestCode=" + hdn_MTcode + "&pname=" + txtPatientName.Text + "&sid=" + txtregno.Text.Trim()+"&Amend=YES";

                }
                if (Pat_Regid != "" && FID != "")
                {
                    //TestCode = getTestCodes(Pat_Regid, FID, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), Convert.ToString(Session["usertype"]), Convert.ToString(Session["UserName"]));

                    //=====================================
                    e.Row.Cells[11].Text = PatSt_new_Bal_C.getStatus_Testwise(Pat_Regid, FID, Convert.ToInt32(Session["Branchid"]), hdn_MTcode);
                    string status = e.Row.Cells[11].Text.Trim();
                    if (status == "Authorized")
                    {
                        int Totalcount = PatSt_new_Bal_C.GetTotalCount(Pat_Regid, FID, Convert.ToInt32(Session["Branchid"]));
                        int Printcount = PatSt_new_Bal_C.GetPrintCount(Pat_Regid, FID, Convert.ToInt32(Session["Branchid"]));
                        if ((Printcount < Totalcount) && (Printcount != 0))
                        {
                            e.Row.Cells[11].Text = "Partially Printed";
                            status = "Partially Printed";
                            //e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >Par Prin</span>";
                        }
                        else if (Printcount == Totalcount)
                        {
                            e.Row.Cells[11].Text = "Printed";
                            status = "Printed";
                            //e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >Prin</span>";
                        }
                        else
                        {
                            e.Row.Cells[11].Text = "Authorized";
                            status = "Authorized";
                            // e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >Auth</span>";
                        }
                    }
                    if (e.Row.Cells[11].Text == "Tested")
                    {

                        e.Row.Cells[11].Text = "<span class='btn btn-xs btn-secondary'>Tes</span>";
                    }
                    else if (e.Row.Cells[11].Text == "Partial Tested")
                    {

                        e.Row.Cells[11].Text = "<span class='btn btn-xs btn-muted' >Par Test</span>";
                    }
                    else if (e.Row.Cells[11].Text == "Partially Printed")
                    {

                        e.Row.Cells[11].Text = "<span class='btn btn-xs btn-yellow' >Pr Prin</span>";
                    }
                    else if (e.Row.Cells[11].Text == "Partial Authorized")
                    {

                        e.Row.Cells[11].Text = "<span class='btn btn-xs btn-warning' >Pr Auth</span>";
                    }
                    else if (e.Row.Cells[11].Text == "Authorized")
                    {

                        e.Row.Cells[11].Text = "<span class='btn btn-xs btn-primary' >Auth</span>";
                    }
                    else if (e.Row.Cells[11].Text == "Printed")
                    {

                        e.Row.Cells[11].Text = "<span class='btn btn-xs btn-success' >Prin</span>";
                    }
                    else
                    {

                        e.Row.Cells[11].Text = "<span class='btn btn-xs btn-danger' >Reg</span>";

                    }

                    //==============================================
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
    void BindGrid()
    {
        try
        { 
            int cnt = 0;
            object labCode = null;
            string RepDoneBy = "";
            if (Convert.ToString(Session["usertype"]) == "Administrator")
            {
                Session["DigModule"] = "0";
            }
            if (fromdate.Text.Trim() != "" && todate.Text.Trim() != "")
            {
                fromDate = fromdate.Text.Trim();
                toDate = todate.Text.Trim();
            }
            if (txtbarcodeNo.Text != "")
            {
                BarcodeID = txtbarcodeNo.Text.Trim();
                //BarcodeID = BarcodeID.Substring(2); 
            }
            if (txtreportDoneBy.Text != "")
            {
               // RepDoneBy = txtreportDoneBy.Text;
                string[] data = txtreportDoneBy.Text.Trim().Split('=');
                if (data.Length > 1)
                {
                    RepDoneBy = data[1].Trim();
                }
                else
                {
                    RepDoneBy = data[0].Trim();
                }
            }
            if (txtcentername_new.Text.Trim() == "")
            {
                if (Convert.ToString(Request.QueryString["CN"]) != "" && Convert.ToString(Request.QueryString["CN"]) != null)
                {
                    CenterCodeNew = Request.QueryString["CN"].Trim();

                }
            }
           
            #region AlterViewvw_GroupByLabcode_New
            Patmst_New_Bal_C.AlterView_VW_Countstatus(Pat_Regid, fromDate, toDate);
            dt = new DataTable();
            if (txttestname.Text == "NA")
            {
                Patmst_New_Bal_C.AlterViewvw_GroupByLabcode_New(Session["UserType"].ToString(), Session["UserName"].ToString());
                dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_11(labCode, "0", fromDate, toDate, status, MTCode, patientName, Pat_Regid, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno);

            }
            else
            {
                Patmst_New_Bal_C.AlterViewvw_VW_Result_Patmstd(Session["UserType"].ToString(), Session["UserName"].ToString(), fromDate, toDate);
                Patmst_New_Bal_C.AlterViewvw_VW_Result_ResMst(Session["UserType"].ToString(), Session["UserName"].ToString(), fromDate, toDate);
                Patmst_New_Bal_C.AlterViewvw_GroupByLabcode_New_testwise(Session["UserType"].ToString(), Session["UserName"].ToString(), fromDate, toDate, patientName, Pat_Regid, BarcodeID, CenterCode, Session["UserName"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew, Convert.ToInt32(Session["DRid"]));

                if (Request.QueryString["frname"] == "Histo")
                {
                    dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_11_testwise_Histo(labCode, "0", fromDate, toDate, status, MTCode, patientName, Pat_Regid, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew, RepDoneBy);

                }
                else if (Request.QueryString["frname"] == "Cyto")
                {
                    dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_11_testwise_Cyto(labCode, "0", fromDate, toDate, status, MTCode, patientName, Pat_Regid, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew,RepDoneBy);

                }
                else
                {
                    dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_11_testwise_Amedent(labCode, "0", fromDate, toDate, status, MTCode, patientName, Pat_Regid, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew, RepDoneBy);
                }

            }

            #endregion

            // GVTestentry.DataSource = (ArrayList)Patmst_New_Bal_C.GetPatmstForTeamL_new(labCode, "0", fromDate, toDate, status, MTCode, patientName, Pat_Regid, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, "", Mno);


            GVTestentry.DataSource = dt;
            GVTestentry.DataBind();
            for (int i = 0; i < GVTestentry.Rows.Count; i++)
            {
                bool Emg = Convert.ToBoolean((GVTestentry.Rows[i].FindControl("hdnis_emergency") as HiddenField).Value);
                if (Emg == true)
                {
                    (GVTestentry.Rows[i].FindControl("btnEmergency") as ImageButton).Visible = true;
                }
                else
                {
                    (GVTestentry.Rows[i].FindControl("btnEmergency") as ImageButton).Visible = false;
                }
                if (i > 0)
                {

                    if (GVTestentry.DataKeys[i].Value.ToString().Trim() == GVTestentry.DataKeys[i - 1].Value.ToString().Trim())
                    {
                        GVTestentry.Rows[i].Cells[0].Text = "";
                        GVTestentry.Rows[i].Cells[1].Text = "";
                        GVTestentry.Rows[i].Cells[2].Text = "";
                        GVTestentry.Rows[i].Cells[3].Text = "";
                        GVTestentry.Rows[i].Cells[4].Text = "";
                        GVTestentry.Rows[i].Cells[5].Text = "";
                        GVTestentry.Rows[i].Cells[6].Text = "";
                        GVTestentry.Rows[i].Cells[7].Text = "";
                        GVTestentry.Rows[i].Cells[8].Text = "";
                        GVTestentry.Rows[i].Cells[9].Text = "";
                       
                      
                        GVTestentry.Rows[i].Cells[22].Text = "";
                        GVTestentry.Rows[i].Cells[23].Text = "";
                       // (GVTestentry.Rows[i].FindControl("btnEmergency") as ImageButton).Visible = false;
                    }
                }
            }
            //GVTestentry.DataSource = null;
            //GVTestentry.DataBind();
            //BindDummyRow();
            // int Pcount = dt.Rows.Count;
            // int Pcount = dt.AsEnumerable().Distinct().Count();
            int Pcount = dt
    .AsEnumerable()
    .Select(r => r.Field<string>("PatRegID"))
    .Distinct()
    .Count();
            lblpcount.Text = Convert.ToString(Pcount);

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
    protected void btnList_Click(object sender, EventArgs e)
    {
        try
        {
            if (fromdate.Text.Trim() != "" && todate.Text.Trim() != "")
            {
                fromDate = fromdate.Text.Trim();
                toDate = todate.Text.Trim();
            }
            //if (ddlStatus.SelectedValue != "0")
            //{
            //    status = ddlStatus.SelectedItem.Text.Trim();
            //}
            //==============================
            if (ChkPending.Checked == true)
            {
                status = "Pending";
            }
            else if (ChkCompleted.Checked == true)
            {
                status = "Completed";
            }
            else if (ChkTested.Checked == true)
            {
                status = "Tested";
            }
            else if (ChkAuthorizs.Checked == true)
            {
                status = "Authorized";
            }
            else if (ChkEmergency.Checked == true)
            {
                status = "Emergency";
            }
            else if (ChkIntRece.Checked == true)
            {
                status = "IntRece";
            }
            else if (ChkIntNotReceive.Checked == true)
            {
                status = "IntNotRece";
            }
            else if (ChkOutsource.Checked == true)
            {
                status = "Outsource";
            }
            else if (ChkAbnormal.Checked == true)
            {
                status = "Abnormal";
            }
            else
            {
                status = "All";
            }
            //==========================
            if (txtPatientName.Text.Trim() != "")
            {
                patientName = txtPatientName.Text.Trim().Replace("'", "'+char(39)+'");;
            }
            if (txtregno.Text.Trim() != "")
            {
                Pat_Regid = txtregno.Text.Trim();

            }

            //if (txtCollCode.Text.Trim() != "")
            //{

            //    CenterCode = txtCollCode.Text;
            //}
            //if (txtmobileno.Text.Trim() != "")
            //{
            //    Mno = txtmobileno.Text.Trim();
            //}
            if (txtmobileno.Text.Trim() != "")
            {
                //  Mno = txtmobileno.Text.Trim();
                string[] data = txtmobileno.Text.Trim().Split('=');
                if (data.Length > 1)
                {
                    Mno = data[0].Trim();
                }
                else
                {
                    Mno = data[1].Trim();
                }
            }
            if (txtPPID.Text.Trim() != "")
            {
                //string[] data = txtPPID.Text.Trim().Split('=');
                //if (data.Length > 1)
                //{
                //    CenterCode = data[1].Trim();
                //}
                //else
                //{
                //    CenterCode = data[0].Trim();
                //}
                CenterCode = txtPPID.Text;
            }
            if (txtcentername_new.Text.Trim() != "")
            {
                string[] data = txtcentername_new.Text.Trim().Split('=');
                if (data.Length > 1)
                {
                    CenterCodeNew = data[1].Trim();
                }
                else
                {
                    CenterCodeNew = data[0].Trim();
                }
                //  CenterCode = txtCollCode.Text;
            }
            if (Convert.ToString(Request.QueryString["TS"]) != "" && Convert.ToString(Request.QueryString["TS"]) != null)
            {
               // status = Request.QueryString["TS"].Trim();

            }
           
            BindGrid();
            if (GVTestentry.Rows.Count == 1)
            {
                //if (txtregno.Text != "" && txtregno.Text.Length == 7)
                //{
                //    Server.Transfer((GVTestentry.Rows[0].Cells[0].Controls[0] as HyperLink).NavigateUrl,false);
                //}
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

    protected void btnpatientcard_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < GVTestentry.Rows.Count; i++)
            {
                ImageButton DelOk = (ImageButton)GVTestentry.Rows[i].FindControl("btnpatientcard");
                if (DelOk == (ImageButton)sender)
                {
                    int PID = Convert.ToInt32((GVTestentry.Rows[i].FindControl("HDPPID") as HiddenField).Value);
                    //int PID1 = Convert.ToInt32(GVTestentry.DataKeys[i].Values["HDPID"].ToString().Trim());
                    string sql1 = "";
                    SqlConnection conn1 = DataAccess.ConInitForDC();
                    SqlCommand sc1 = conn1.CreateCommand();
                    Patmst_Bal_C PBC = new Patmst_Bal_C();

                    //PBC.PID = Convert.ToInt32(ViewState["PID"]);
                    //PBC.get_PermentId();

                    //ViewState["PPID"] = PBC.P_PPID;

                    sc1.CommandText = "ALTER VIEW [dbo].[VW_PatientCard] AS SELECT top(1) percent  RecM.BillNo, dbo.RecM.billdate as RecDate, dbo.RecM.PaymentType as BillType, RecM.AmtPaid AS AmtReceived, RecM.DisAmt AS Discount, "+
                        "    dbo.patmst.TestCharges AS NetPayment, RecM.AmtPaid, RecM.BalAmt AS Balance,   RecM.username, RecM.OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, "+
                        "    patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, DrMT.DoctorName, MainTest.Maintestname, MainTest.MTCode,   patmstd.TestRate, "+
                        "    PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag, patmst.Patusername, patmst.Patpassword, RecM.Comment, patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress,  "+
                        "    patmst.PPID,   patmst.UnitCode, RecM.TaxAmount, RecM.TaxPer, RecM.PrintCount, patmst.Email AS EmailID  "+
                        "    FROM            RecM INNER JOIN   patmst INNER JOIN   DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN   MainTest INNER JOIN  "+
                        "    patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid ON RecM.PID = patmst.PID "+
                        "    AND   RecM.branchid = patmst.Branchid LEFT OUTER JOIN   PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode where  patmst.branchid=" + Session["Branchid"].ToString() + " and patmst.PPID='" + PID + "'  order by RecM.billno desc  ";// and Cshmst.BillNo=" + bno + " DrMT.DrCheck_flag='CC' and


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
                }
            }
        }
        catch (Exception exc)
        {
            Response.Cookies["error"].Value = exc.Message;
            Server.Transfer("~/ErrorMessage.aspx");
        }
    }
    protected void GVTestentry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("save"))
        {
            //GridView Row Index
            int rowIndex = int.Parse(e.CommandArgument.ToString().Trim());

            // ID of the Current Row
            String ID = GVTestentry.DataKeys[rowIndex].Values["PatRegID"].ToString().Trim();

            //File Upload Instance of the Current Row
            FileUpload fileUpload = (FileUpload)GVTestentry.Rows[rowIndex].FindControl("FileUpload1");
            Label LblFilename = (Label)GVTestentry.Rows[rowIndex].FindControl("LblFilename");
            HiddenField PID = (HiddenField)GVTestentry.Rows[rowIndex].FindControl("HDPID");
            HiddenField MTCode = (HiddenField)GVTestentry.Rows[rowIndex].FindControl("hdn_MTcode");
            string PDate = DateTime.Now.ToString("ddMMyyyy");
            string DefaultFileName = "ViewPrescription/";
            if (fileUpload.HasFile)
            {
                fileUpload.SaveAs(Server.MapPath(DefaultFileName) + "/" + ID + "_" + PDate + "_" + fileUpload.FileName);
                LblFilename.Text = ID + "_" + PDate + "_" + fileUpload.FileName;
                Cshmst_Bal_C Obj_CBC = new Cshmst_Bal_C();
                Obj_CBC.P_UploadPrescription = DefaultFileName + "" + LblFilename.Text;
                Obj_CBC.PID = Convert.ToInt32(PID.Value);
                Obj_CBC.PatRegID = Convert.ToString(ID);
                Obj_CBC.MTCode = Convert.ToString(MTCode.Value);
                Obj_CBC.Insert_Update_Prescription(Convert.ToInt32(Session["Branchid"]));
            }

        }
    }
    protected void Imgrerun_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < GVTestentry.Rows.Count; i++)
        {
            HiddenField ltlc = (GVTestentry.Rows[i].FindControl("hdn_MTcode") as HiddenField);
            HiddenField HdnUP = (GVTestentry.Rows[i].FindControl("hdnUploadPrescription") as HiddenField);
            HiddenField PatRegid = (GVTestentry.Rows[i].FindControl("hdnPatRegID") as HiddenField);
            string txtshort1 = "";
            ImageButton DelOk = (ImageButton)GVTestentry.Rows[i].FindControl("Imgrerun");
            if (DelOk == (ImageButton)sender)
            {
                string STCODE = DelOk.CommandArgument;
                //Label lblTestCode = (GVTestentry.Rows[i].FindControl("lblTestCode") as Label);
                //Label lblsubdeptid1 = (GVTestentry.Rows[i].FindControl("lblsubdeptid1") as Label);
                //Label lblMTCodesub = (GVTestentry.Rows[i].FindControl("lblMTCodesub") as Label);

                PatSt_Bal_C PBC = new PatSt_Bal_C();
                PBC.MTCode = ltlc.Value;
                // PBC.InsertUpdateDESC_Nondesc(lblMTCodesub.Text);  Request.QueryString["PatRegID"].ToString();

                // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1250/2);var Mtop = (screen.height/2)-(700/2);window.open( 'UploadFileView.aspx?UploaPres=" + HdnUP.Value + "&Branchid=" + Session["Branchid"].ToString() + " ', null, 'height=700,width=1250,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1250/2);var Mtop = (screen.height/2)-(700/2);window.open( 'UploadFileView.aspx?UploaPres=" + HdnUP.Value + "&PatRegID=" + PatRegid.Value + "&Branchid=" + Session["Branchid"].ToString() + " ', null, 'height=700,width=1250,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

            }
        }
    }

}