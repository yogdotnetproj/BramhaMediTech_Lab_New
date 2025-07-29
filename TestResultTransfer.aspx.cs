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
public partial class TestResultTransfer :BasePage
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

                if (Request.QueryString["Type"] != null)
                {
                    string info = Request.QueryString["Type"].ToString();
                    Session["Type"] = info;
                }
                if (Request.QueryString["PatRegID"] != null)
                {
                    //txtregno.Text = Request.QueryString["PatRegID"].Trim();

                }
                if (Request.QueryString["Maindept"] != null)
                {
                    Session["DigModule"] = Request.QueryString["Maindept"].Trim();

                }
                DataTable dtban = new DataTable();
                dtban = ObjTB.Bindbanner();
                if (dtban.Rows.Count > 0)
                {
                    if (Convert.ToString(dtban.Rows[0]["Type"]) == "0")
                    {
                        ViewState["VALIDATE"] = "YES";
                        ddlStatus.Items[8].Selected = true;

                        fromdate.Text = Date.getdate().AddMonths(-1).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        ViewState["VALIDATE"] = "NO";
                        ddlStatus.Items[0].Selected = true;
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
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixText + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixText + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
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
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM subdepartment where subdeptName like  N'" + prefixText + "%'  order by subdeptName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM subdepartment where subdeptName like  N'" + prefixText + "%'  order by subdeptName", con);
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
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixText + "%' and DrType='DR' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixText + "%' and Drtype='DR' and branchid=" + branchid + " order by DoctorName", con);
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
            PatSt_Bal_C ObjPBC = new PatSt_Bal_C();
            if (e.NewEditIndex == -1)
                return;
            int i = e.NewEditIndex;
            string rno = (GVTestentry.Rows[e.NewEditIndex].FindControl("hdnPatRegID") as HiddenField).Value;
            string FID = (GVTestentry.Rows[e.NewEditIndex].FindControl("hdnFID1") as HiddenField).Value;
            string MTCo = (GVTestentry.Rows[e.NewEditIndex].FindControl("hdn_MTcode") as HiddenField).Value;
            
            ObjPBC.PatRegID =rno;
            ObjPBC.MTCode = Convert.ToString(MTCo);
            ObjPBC.FID = FID;
            ObjPBC.InsertUpdate_AddResult_outsource_1(Convert.ToInt32(Session["Branchid"]), 0);

           // ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'TestReportprinting.aspx?PatRegID=" + rno + " &FID=" + FID + " ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
            btnList_Click(this, null);
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
                string FID = (e.Row.FindControl("hdnFID1") as HiddenField).Value.Trim();
                int PID = Convert.ToInt32((e.Row.FindControl("HDPID") as HiddenField).Value.Trim());
                int hdnMaindept = Convert.ToInt32((e.Row.FindControl("hdnMaindept") as HiddenField).Value.Trim());
                string hdn_Maintestname = Convert.ToString((e.Row.FindControl("hdn_Maintestname") as HiddenField).Value.Trim());
                bool Emg1 = Convert.ToBoolean((e.Row.FindControl("hdnis_emergency") as HiddenField).Value);
                string hdn_MTcode = Convert.ToString((e.Row.FindControl("hdn_MTcode") as HiddenField).Value.Trim());
                string hdnopid = Convert.ToString((e.Row.FindControl("hdnopid") as HiddenField).Value.Trim());
                bool hdnORT = Convert.ToBoolean((e.Row.FindControl("hdnORT") as HiddenField).Value.Trim());

                string Mailstatus = Convert.ToString(e.Row.Cells[15].Text);
                if (Mailstatus == "Send Email")
                {
                    e.Row.Cells[15].Text = "<span class='btn btn-xs btn-success' >send</span>";

                }
                else
                {
                    e.Row.Cells[15].Text = "<span class='btn btn-xs btn-danger' >No</span>";
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
                if (DrMailstatus == "Send DrEmail")
                {
                    e.Row.Cells[18].Text = "<span class='btn btn-xs btn-success' >Yes</span>";

                }
                else
                {
                    e.Row.Cells[18].Text = "<span class='btn btn-xs btn-danger' >No</span>";
                }
                bool Outsource = Convert.ToBoolean(e.Row.Cells[19].Text);
                if (Outsource == true)
                {
                    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-success' >Yes</span>";

                }
                //else
                //{
                //    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-danger' >No</span>";
                //}

                else if (hdnopid != "0" && hdnORT == false)
                {
                    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-danger' >O-Pat</span>";

                }
                else if (hdnORT == true)
                {
                    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-success' >Transfer</span>";

                }
                else
                {
                    e.Row.Cells[19].Text = "<span class='btn btn-xs btn-danger' >NA</span>";
                }
                string IntResult = Convert.ToString(e.Row.Cells[16].Text);
                if (IntResult == "Yes")
                {
                    e.Row.Cells[16].Text = "<span class='btn btn-xs btn-success' >Yes</span>";

                }
                else
                {
                    e.Row.Cells[16].Text = "<span class='btn btn-xs btn-danger' >No</span>";
                }

                // bool Emg = Convert.ToBoolean((e.Row.FindControl("hdnis_emergency") as HiddenField).Value);
                if (Emg1 == true)
                {
                    (e.Row.FindControl("btnEmergency") as ImageButton).Visible = true;
                }
                else
                {
                    (e.Row.FindControl("btnEmergency") as ImageButton).Visible = false;
                }

                //if (Session["Type"] != null)
                //{

                //    if (Session["Type"].ToString() == "Cyto")
                //    {
                //        (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntryCyto" + "&Maindept=" + hdnMaindept;

                //    }
                //    else if (Session["Type"].ToString() == "Histo")
                //    {
                //        (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntryHisto" + "&Maindept=" + hdnMaindept;

                //    }
                //    else
                //    {
                //        (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntry" + "&Maindept=" + hdnMaindept;
                //    }
                //}
                //else
                //{
                //    (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + Pat_Regid + "&FID=" + FID + "&RepType=TestResultEntry" + "&Maindept=" + hdnMaindept;
                //}
                if (Pat_Regid != "" && FID != "")
                {
                    //TestCode = getTestCodes(Pat_Regid, FID, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), Convert.ToString(Session["usertype"]), Convert.ToString(Session["UserName"]));

                    //=====================================
                    e.Row.Cells[09].Text = PatSt_new_Bal_C.getStatus_Testwise(Pat_Regid, FID, Convert.ToInt32(Session["Branchid"]), hdn_MTcode);
                    string status = e.Row.Cells[09].Text.Trim();
                    if (status == "Authorized")
                    {
                        int Totalcount = PatSt_new_Bal_C.GetTotalCount(Pat_Regid, FID, Convert.ToInt32(Session["Branchid"]));
                        int Printcount = PatSt_new_Bal_C.GetPrintCount(Pat_Regid, FID, Convert.ToInt32(Session["Branchid"]));
                        if ((Printcount < Totalcount) && (Printcount != 0))
                        {
                            e.Row.Cells[09].Text = "Partially Printed";
                            status = "Partially Printed";
                            //e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >Par Prin</span>";
                        }
                        else if (Printcount == Totalcount)
                        {
                            e.Row.Cells[09].Text = "Printed";
                            status = "Printed";
                            //e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >Prin</span>";
                        }
                        else
                        {
                            e.Row.Cells[09].Text = "Authorized";
                            status = "Authorized";
                            // e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >Auth</span>";
                        }
                    }
                    if (e.Row.Cells[09].Text == "Tested")
                    {

                        e.Row.Cells[09].Text = "<span class='badge'>Tes</span>";
                    }
                    else if (e.Row.Cells[09].Text == "Partial Tested")
                    {

                        e.Row.Cells[09].Text = "<span class='btn btn-xs btn-muted' >Par Test</span>";
                    }
                    else if (e.Row.Cells[09].Text == "Partially Printed")
                    {

                        e.Row.Cells[09].Text = "<span class='btn btn-xs btn-yellow' >Pr Prin</span>";
                    }
                    else if (e.Row.Cells[09].Text == "Partial Authorized")
                    {

                        e.Row.Cells[09].Text = "<span class='btn btn-xs btn-warning' >Pr Auth</span>";
                    }
                    else if (e.Row.Cells[09].Text == "Authorized")
                    {

                        e.Row.Cells[09].Text = "<span class='btn btn-xs btn-primary' >Auth</span>";
                    }
                    else if (e.Row.Cells[09].Text == "Printed")
                    {

                        e.Row.Cells[09].Text = "<span class='btn btn-xs btn-success' >Prin</span>";
                    }
                    else
                    {

                        e.Row.Cells[09].Text = "<span class='btn btn-xs btn-danger' >Reg</span>";

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
                BarcodeID = BarcodeID.Substring(2);
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
                    dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_11_testwise_Histo(labCode, "0", fromDate, toDate, status, MTCode, patientName, Pat_Regid, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew,"");

                }
                else if (Request.QueryString["frname"] == "Cyto")
                {
                    dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_11_testwise_Cyto(labCode, "0", fromDate, toDate, status, MTCode, patientName, Pat_Regid, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew,"");

                }
                else
                {
                    dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_TestResultTransfer(labCode, "0", fromDate, toDate, status, MTCode, patientName, Pat_Regid, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew);
                }

            }

            #endregion

            // GVTestentry.DataSource = (ArrayList)Patmst_New_Bal_C.GetPatmstForTeamL_new(labCode, "0", fromDate, toDate, status, MTCode, patientName, Pat_Regid, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, "", Mno);


            GVTestentry.DataSource = dt;
            GVTestentry.DataBind();
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
            if (ddlStatus.SelectedValue != "0")
            {
                status = ddlStatus.SelectedItem.Text.Trim();
            }

            if (txtPatientName.Text.Trim() != "")
            {
                patientName = txtPatientName.Text.Trim();
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
            BindGrid();
            if (GVTestentry.Rows.Count == 1)
            {
                if (txtregno.Text != "" && txtregno.Text.Length == 7)
                {
                    Server.Transfer((GVTestentry.Rows[0].Cells[0].Controls[0] as HyperLink).NavigateUrl);
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