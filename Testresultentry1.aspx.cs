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
public partial class Testresultentry :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    object fromDate = null, toDate = null;
    string status = "", MTCode = "", patientName = "", regNo = "", BarcodeID = "", CenterCode = "", Mno = "", CenterCodeNew="";
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    private static int PageSize = 20;
    protected void Page_Load(object sender, EventArgs e)
    {
        

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
                if (Request.QueryString["frname"] != null)
                {
                    string info = Request.QueryString["frname"].ToString();
                    Session["selectform"] = info;
                }


                if (Session["usertype"].ToString() == "CollectionCenter" && Session["username"] != null)
                {
                    txtCollCode.Text = Convert.ToString( Session["CenterCode"]);
                }
               
                if (Request.QueryString["Labid"] != null)
                {
                    string ssss = Request.QueryString["Labid"].Trim();
                   
                }
                if (Request.QueryString["sdt"] != null)
                {
                    if (Request.QueryString["sdt"].Trim() != "")
                    {
                        fromdate.Text = Request.QueryString["sdt"].Trim();
                    }
                }
                else
                {
                    fromdate.Text = Date.getdate().AddMonths(-1).ToString("dd/MM/yyyy");
                }
                if (Request.QueryString["edt"] != null)
                {
                    if (Request.QueryString["edt"].Trim() != "")
                    {
                        todate.Text = Request.QueryString["edt"].Trim();
                    }
                }
                else
                {
                    todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                }
                
                if (Request.QueryString["stat"] != null)
                {
                    if (Request.QueryString["stat"].Trim() != "0")
                    {
                        ddlStatus.SelectedValue = Request.QueryString["stat"].Trim();
                    }
                }
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
               
                if (Request.QueryString["CenterCode"] != null)
                {
                    if (Request.QueryString["CenterCode"].Trim() != "")
                    {
                        txtCollCode.Text = Request.QueryString["CenterCode"].Trim();
                    }
                }
                if (txtregno.Text == "")
                {
                   // btnList_Click(this, null);
                }
                //BindDummyRow();
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
    private void BindDummyRow()
    {
        DataTable dummy = new DataTable();
        dummy.Columns.Add("RegistratonDateTime");
        dummy.Columns.Add("RegNo");
        dummy.Columns.Add("CenterCode");
        dummy.Columns.Add("BarcodeID");
        dummy.Columns.Add("FullName");
        dummy.Columns.Add("Sex");
        dummy.Columns.Add("MYD");
        dummy.Columns.Add("DrName");
        dummy.Columns.Add("TestName");

        dummy.Columns.Add("SampleStatusNew");
        dummy.Columns.Add("p_remark");
        dummy.Columns.Add("LabRegMediPro");
        dummy.Columns.Add("Labno");
        dummy.Columns.Add("FID");
        dummy.Columns.Add("PID");

        dummy.Columns.Add("maindeptid");
        dummy.Columns.Add("Maintestname");
        dummy.Columns.Add("MTCode");

        dummy.Rows.Add();
        GVTestentry.DataSource = dummy;
        GVTestentry.DataBind();
    }
    [WebMethod]
    public static string GetCustomers(int pageIndex)
    {
        string query = "[GetCustomers_Pager]";
        SqlCommand cmd = new SqlCommand(query);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
        cmd.Parameters.AddWithValue("@PageSize", PageSize);
        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
        return GetData(cmd, pageIndex).GetXml();
    }
    private static DataSet GetData(SqlCommand cmd, int pageIndex)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds, "Customers");
                    DataTable dt = new DataTable("Pager");
                    dt.Columns.Add("PageIndex");
                    dt.Columns.Add("PageSize");
                    dt.Columns.Add("RecordCount");
                    dt.Rows.Add();
                    dt.Rows[0]["PageIndex"] = pageIndex;
                    dt.Rows[0]["PageSize"] = PageSize;
                    dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
                    ds.Tables.Add(dt);
                    return ds;
                }
            }
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
    void BindGrid()
    {
        try
        {
            int cnt = 0;
            object labCode = null;
            if (Convert.ToString( Session["usertype"]) == "Administrator")
            {
                Session["DigModule"] = "0";
            }
            if (fromdate.Text.Trim() != "" && todate.Text.Trim() != "")
            {
                fromDate = fromdate.Text.Trim();
                toDate = todate.Text.Trim();
            }
            #region AlterViewvw_GroupByLabcode_New
            Patmst_New_Bal_C.AlterView_VW_Countstatus(regNo, fromDate, toDate);
            dt = new DataTable();
            if (txttestname.Text == "NA")
            {
                Patmst_New_Bal_C.AlterViewvw_GroupByLabcode_New(Session["UserType"].ToString(), Session["UserName"].ToString());
                dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_11(labCode, "0", fromDate, toDate, status, MTCode, patientName, regNo, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno);

            }
            else
            {
                Patmst_New_Bal_C.AlterViewvw_VW_Result_Patmstd(Session["UserType"].ToString(), Session["UserName"].ToString(), fromDate, toDate);
                Patmst_New_Bal_C.AlterViewvw_VW_Result_ResMst(Session["UserType"].ToString(), Session["UserName"].ToString(), fromDate, toDate);
                Patmst_New_Bal_C.AlterViewvw_GroupByLabcode_New_testwise(Session["UserType"].ToString(), Session["UserName"].ToString(), fromDate, toDate, patientName, regNo, BarcodeID, CenterCode, Session["UserName"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew,0);
               
                 if (Request.QueryString["frname"] == "Histo")
                {
                    dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_11_testwise_Histo(labCode, "0", fromDate, toDate, status, MTCode, patientName, regNo, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew,"");

                }
                 else if (Request.QueryString["frname"] == "Cyto")
                 {
                     dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_11_testwise_Cyto(labCode, "0", fromDate, toDate, status, MTCode, patientName, regNo, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew,"");

                 }
                 else
                 {
                     dt = Patmst_New_Bal_C.GetPatmstForTeamL_new_11_testwise(labCode, "0", fromDate, toDate, status, MTCode, patientName, regNo, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, txttestname.Text, Mno, CenterCodeNew,"");
                 }
             
            }

            #endregion

           // GVTestentry.DataSource = (ArrayList)Patmst_New_Bal_C.GetPatmstForTeamL_new(labCode, "0", fromDate, toDate, status, MTCode, patientName, regNo, BarcodeID, CenterCode, Session["UserName"].ToString(), Session["UserType"].ToString(), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), 0, "", Mno);


            GVTestentry.DataSource = dt;
            GVTestentry.DataBind();
            //GVTestentry.DataSource = null;
            //GVTestentry.DataBind();
            //BindDummyRow();
            int Pcount = dt.Rows.Count;
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
                regNo = txtregno.Text.Trim();

            }
           
            if (txtCollCode.Text.Trim() != "" )
            {
                
                CenterCode = txtCollCode.Text;
            }
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
            Patmst_Bal_C ci = new Patmst_Bal_C();
            
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

    protected void btnShow_Click(object sender, EventArgs e)
    {

        btnList_Click(this, null);
    }

    protected void GVTestentry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex == -1)
                return;
            string TestCode = "";
            string regno = (e.Row.FindControl("hdnPatRegID") as HiddenField).Value.Trim();
            if (regno != "")
            {
                string FID = (e.Row.FindControl("hdnFID1") as HiddenField).Value.Trim();
                int PID = Convert.ToInt32((e.Row.FindControl("HDPID") as HiddenField).Value.Trim());
                int hdnMaindept = Convert.ToInt32((e.Row.FindControl("hdnMaindept") as HiddenField).Value.Trim());
                string hdn_Maintestname = Convert.ToString((e.Row.FindControl("hdn_Maintestname") as HiddenField).Value.Trim());
                bool Emg1 = Convert.ToBoolean((e.Row.FindControl("hdnis_emergency") as HiddenField).Value);
                string hdn_MTcode = Convert.ToString((e.Row.FindControl("hdn_MTcode") as HiddenField).Value.Trim());

                string Mailstatus = Convert.ToString(e.Row.Cells[15].Text);
                if (Mailstatus == "Send Email")
                {
                    e.Row.Cells[15].Text = "<span class='btn btn-xs btn-success' >send mail</span>";

                }
                else
                {
                    e.Row.Cells[15].Text = "<span class='btn btn-xs btn-danger' >Not send mail</span>";
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

                if (Session["selectform"] != null)
                {

                    if (Session["selectform"].ToString() == "Cyto")
                    {
                        (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + regno + "&FID=" + FID + "&sdt=" + fromdate.Text + "&edt=" + todate.Text + "&stat=" + ddlStatus.SelectedValue + "&pname=" + txtPatientName.Text + "&sid=" + txtregno.Text.Trim() + "&CenterCode=" + txtCollCode.Text.Trim() + "&formname=TestResultEntryCyto" + "&form=Sample" + "&user=" + Session["usertype"].ToString() + "&Maindept=" + hdnMaindept;

                    }
                    else if (Session["selectform"].ToString() == "Histo")
                    {
                        (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + regno + "&FID=" + FID + "&sdt=" + fromdate.Text + "&edt=" + todate.Text + "&stat=" + ddlStatus.SelectedValue + "&pname=" + txtPatientName.Text + "&sid=" + txtregno.Text.Trim() + "&CenterCode=" + txtCollCode.Text.Trim() + "&formname=TestResultEntryHisto" + "&form=Sample" + "&user=" + Session["usertype"].ToString() + "&Maindept=" + hdnMaindept;

                    }
                    else
                    {
                        (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + regno + "&FID=" + FID + "&sdt=" + fromdate.Text + "&edt=" + todate.Text + "&stat=" + ddlStatus.SelectedValue + "&pname=" + txtPatientName.Text + "&sid=" + txtregno.Text.Trim() + "&CenterCode=" + txtCollCode.Text.Trim() + "&formname=TestResultEntry" + "&form=Sample" + "&user=" + Session["usertype"].ToString() + "&Maindept=" + hdnMaindept;
                    }
                }
                else
                {
                    (e.Row.Cells[0].Controls[0] as HyperLink).NavigateUrl = "Addresult.aspx?PatRegID=" + regno + "&FID=" + FID + "&sdt=" + fromdate.Text + "&edt=" + todate.Text + "&stat=" + ddlStatus.SelectedValue + "&pname=" + txtPatientName.Text + "&sid=" + txtregno.Text.Trim() + "&CenterCode=" + txtCollCode.Text.Trim() + "&formname=TestResultEntry" + "&form=Sample" + "&user=" + Session["usertype"].ToString() + "&Maindept=" + hdnMaindept;
                }
                if (regno != "" && FID != "")
                {
                    TestCode = getTestCodes(regno, FID, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), Convert.ToString(Session["usertype"]), Convert.ToString(Session["UserName"]));

                    //=====================================
                    e.Row.Cells[09].Text = PatSt_new_Bal_C.getStatus_Testwise(regno, FID, Convert.ToInt32(Session["Branchid"]), hdn_MTcode);
                    string status = e.Row.Cells[09].Text.Trim();
                    if (status == "Authorized")
                    {
                        int Totalcount = PatSt_new_Bal_C.GetTotalCount(regno, FID, Convert.ToInt32(Session["Branchid"]));
                        int Printcount = PatSt_new_Bal_C.GetPrintCount(regno, FID, Convert.ToInt32(Session["Branchid"]));
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

    string getTestCodes(string PatRegID, string Fid, int branchid, int maindeptid, string UserType, string username)
    {
        string testCodes = "";
        branchid = Convert.ToInt32(Session["Branchid"]);
        SqlCommand sc = null;        
        SqlConnection conn = DataAccess.ConInitForDC();
        if (UserType == "Main Doctor" || UserType == "Technician" || UserType == "Reporting")
        {
            sc = new SqlCommand("SELECT DISTINCT patmstd.PackageCode FROM patmstd INNER JOIN " +
                            " SubDepartment ON patmstd.SDCode = SubDepartment.SDCode AND patmstd.branchid = SubDepartment.branchid INNER JOIN " +
                            " Deptwiseuser ON SubDepartment.subdeptid = Deptwiseuser.Deptcodeid where patmstd.PatRegID=@PatRegID and patmstd.FID=@FID and patmstd.branchid=" + branchid + "  and Deptwiseuser.username = '" + username + "' and PackageCode<>''" //and SubDepartment.MainDeptid=@MainDept
                            + " UNION SELECT DISTINCT patmstd.MTCode FROM patmstd INNER JOIN " +
                            " SubDepartment ON patmstd.SDCode = SubDepartment.SDCode AND patmstd.branchid = SubDepartment.branchid INNER JOIN " +
                            " Deptwiseuser ON SubDepartment.subdeptid = Deptwiseuser.Deptcodeid where (patmstd.PackageCode='' OR patmstd.PackageCode IS NULL) and patmstd.PatRegID=@PatRegID and patmstd.FID=@FID and   Deptwiseuser.username = '" + username + "' and patmstd.branchid=" + branchid + "", conn); //SubDepartment.MainDeptid=@MainDept and
        }
        else
        {
            sc = new SqlCommand("SELECT DISTINCT patmstd.PackageCode FROM patmstd INNER JOIN " +
                                           " SubDepartment ON patmstd.SDCode = SubDepartment.SDCode AND patmstd.branchid = SubDepartment.branchid where patmstd.PatRegID=@PatRegID and patmstd.FID=@FID and patmstd.branchid=" + branchid + " and  PackageCode<>'' " //SubDepartment.MainDeptid=@MainDept and
                                           + " UNION SELECT DISTINCT patmstd.MTCode FROM patmstd INNER JOIN " +
                                            " SubDepartment ON patmstd.SDCode = SubDepartment.SDCode AND patmstd.branchid = SubDepartment.branchid where(patmstd.PackageCode='' or patmstd.PackageCode is null) and patmstd.PatRegID=@PatRegID and patmstd.FID=@FID and   patmstd.branchid=" + branchid + "", conn); //SubDepartment.MainDeptid=@MainDept and
        }

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Fid;
        sc.Parameters.Add(new SqlParameter("@MainDept", SqlDbType.Int)).Value = maindeptid;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            try
            {
                sdr = sc.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw;
            }

            if (sdr != null)
            {
                while (sdr.Read())
                {
                    if (testCodes == "")
                    {
                        testCodes = sdr[0].ToString().Trim();
                    }
                    else
                    {
                        if (testCodes.Length > 15)
                        {
                            testCodes = testCodes + ", " + sdr[0].ToString().Trim();
                        }
                        else
                        {
                            testCodes = testCodes + "," + sdr[0].ToString().Trim();
                        }
                    }
                    //}
                }
            }
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
                // Log an event in the Application Event Log.
                throw;
            }

        }

        return testCodes;
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
           ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(1000/2);var Mtop = (screen.height/2)-(500/2);window.open( 'TestReportprinting.aspx?PatRegID=" + rno + "&sdt=" + fromdate.Text + "&edt=" + todate.Text + " &FID=" + FID + "&source=sws ', null, 'height=500,width=1000,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
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

    [WebMethod]
    [ScriptMethod]
    public static string[] Getcenter(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
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
        this.btnList_Click(null, null);
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] GetDeptName(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
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
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
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

    

}