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
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;
using System.Net.Http;

public partial class BillPaymentRefund : System.Web.UI.Page
{
    BillReturnViewModel Obj_Adt = new BillReturnViewModel();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string collname = "", regno = "", collectioncode = "", labcode_main = "";
    DateTime stDate = Date.getMinDate(), endDate = Date.getMinDate();
    PatientBillCancel_C ObjPBC = new PatientBillCancel_C();
    Patmst_New_Bal_C contact = new Patmst_New_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("BillPaymentRefund.aspx");
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
            try
            {
                fromdate.Text = DateTime.Now.ToShortDateString();
                todate.Text = DateTime.Now.ToShortDateString();
                txtCenter.Text = "All";
                BindGrid();
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


    void BindGrid()
    {
        try
        {
            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
            if (labcode != null && labcode != "")
            {
                this.labcode_main = labcode;
            }
            if (Session["usertype"].ToString() == "CollectionCenter")
            {

                collectioncode = Session["CenterCode"].ToString();

            }
            if (txtCenter.Text != "")
            {
                collname = txtCenter.Text;
            }
            if (txtregno.Text != "")
            {
                regno = txtregno.Text;

            }
            else if (fromdate.Text != "" && todate.Text != "")
            {
                stDate = DateTimeConvesion.getDateFromString(fromdate.Text);
                endDate = DateTimeConvesion.getDateFromString(todate.Text);


            }
            contact.AlterVW_Getregisteredstatus(stDate, endDate.AddDays(1));

            GV_Billcancel.DataSource = contact.GetPatientInformationnew_2(collname, stDate, endDate, regno, Convert.ToInt32(Session["Branchid"]), 0, collectioncode, "", this.labcode_main);
            GV_Billcancel.DataBind();
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
    protected void btnshow_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void GV_Billcancel_RowEditing(object sender, GridViewEditEventArgs e)
    {
        if (e.NewEditIndex == -1)
            return;
        int PID = Convert.ToInt32(GV_Billcancel.DataKeys[e.NewEditIndex].Value);

        ObjPBC.P_PID = PID;
        ObjPBC.BillDisactive();
        //  TransferAPI_Data();
        BindGrid();
        e.Cancel = true;


    }
    protected void GV_Billcancel_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Billcancel.PageIndex = e.NewPageIndex;
        BindGrid();
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
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like N'%" + prefixText + "%' and DrCheck_flag='CC' and LBcode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like N'%" + prefixText + "%' and DrCheck_flag='CC' and branchid=" + branchid + " order by DoctorName", con);
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
    protected void GV_Billcancel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex != -1)
        {
            try
            {
                string charges = (e.Row.FindControl("lblTestCharges") as Label).Text;
                string Discount = e.Row.Cells[10].Text;
                int total = 0;
                if (charges != "" && charges != null && charges != "&nbsp;")
                {
                    total = Convert.ToInt32(charges);
                }

                int balance = Convert.ToInt32(e.Row.Cells[11].Text);

                string CenterCode = (e.Row.FindControl("hdnCollcode") as HiddenField).Value;

            }
            catch (Exception ex)
            { }
        }
    }
    protected void GV_Billcancel_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (e.RowIndex == -1)
            return;
        int PID = Convert.ToInt32(GV_Billcancel.DataKeys[e.RowIndex].Value);
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
    public void TransferAPI_Data()
    {
        DataTable dtAPI = new DataTable();
        DataTable dtAPI1 = new DataTable();

        dtAPI1 = Obj_Adt.Get_IsIRD_required();
        if (Convert.ToBoolean(dtAPI1.Rows[0]["IsIRDApprove"]) == true)
        {
            dtAPI = Obj_Adt.Get_Materialize_Data_Canel();
            if (dtAPI.Rows.Count > 0)
            {
                for (int j = 0; j < dtAPI.Rows.Count; j++)
                {
                    //Obj_Adt.username = "Test_CABC";
                    //Obj_Adt.password = "test@321";
                    //Obj_Adt.seller_pan = Convert.ToString(dtAPI1.Rows[0]["PanNumber"]);
                    //Obj_Adt.buyer_pan = "";
                    //Obj_Adt.buyer_name = Convert.ToString(dtAPI.Rows[j]["Customer_Name"]);
                    //Obj_Adt.fiscal_year = "2074.075";
                    //Obj_Adt.total_sales = Math.Round(Convert.ToSingle(dtAPI.Rows[j]["Total_Amount"]),2); ;
                    //Obj_Adt.taxable_sales_hst = Math.Round(Convert.ToSingle(dtAPI.Rows[j]["Taxable_Amount"]),2); ;
                    //Obj_Adt.taxable_sales_vat = 0;
                    //Obj_Adt.vat = 0;
                    //Obj_Adt.excisable_amount = 0;
                    //Obj_Adt.excise = 0;
                    //Obj_Adt.ref_invoice_number = Convert.ToString(dtAPI.Rows[j]["Bill_No"]);
                    //Obj_Adt.credit_note_date = Convert.ToString(dtAPI.Rows[j]["Bill_Date"]);
                    //Obj_Adt.credit_note_date = Obj_Adt.credit_note_date.Replace('-', '.');
                    //Obj_Adt.hst = Math.Round(Convert.ToSingle(dtAPI.Rows[j]["Tax_Amount"]), 2);
                    //Obj_Adt.amount_for_esf = 0;
                    //Obj_Adt.esf = 0;
                    //Obj_Adt.export_sales = 0;
                    //Obj_Adt.tax_exempted_sales = 0;
                    //Obj_Adt.isrealtime = true;
                    //Obj_Adt.datetimeClient = DateTime.Now;
                    //Obj_Adt.Sr_no = Convert.ToInt32(dtAPI.Rows[j]["Sr_no"]);
                    //Obj_Adt.reason_for_return = "defect in piece";



                    Obj_Adt.username = "Test_CABC";
                    Obj_Adt.password = "test@321";
                    Obj_Adt.seller_pan = "999999999";
                    Obj_Adt.buyer_pan = "123456789";
                    Obj_Adt.buyer_name = "";
                    Obj_Adt.fiscal_year = "2073.074";
                    Obj_Adt.ref_invoice_number = Convert.ToString(dtAPI.Rows[j]["Bill_No"]);
                    Obj_Adt.credit_note_date = Convert.ToString(dtAPI.Rows[j]["Bill_Date"]);
                    Obj_Adt.credit_note_date = Obj_Adt.credit_note_date.Replace('-', '.');
                    Obj_Adt.credit_note_number = "1";
                    Obj_Adt.reason_for_return = "defect in piece";
                    Obj_Adt.total_sales = Math.Round(Convert.ToSingle(dtAPI.Rows[j]["Total_Amount"]), 2); ;
                    Obj_Adt.taxable_sales_vat = 0;
                    Obj_Adt.vat = 0;
                    Obj_Adt.excisable_amount = 0;
                    Obj_Adt.excise = 0;
                    Obj_Adt.taxable_sales_hst = Math.Round(Convert.ToSingle(dtAPI.Rows[j]["Taxable_Amount"]), 2); ;
                    Obj_Adt.hst = Math.Round(Convert.ToSingle(dtAPI.Rows[j]["Tax_Amount"]), 2);
                    Obj_Adt.amount_for_esf = 0;
                    Obj_Adt.esf = 0;
                    Obj_Adt.export_sales = 0;
                    Obj_Adt.tax_exempted_sales = 0;
                    Obj_Adt.isrealtime = true;
                    Obj_Adt.datetimeClient = DateTime.Now;
                    Obj_Adt.Sr_no = Convert.ToInt32(dtAPI.Rows[j]["Sr_no"]);
                    SendAPI(Obj_Adt);
                }
            }
        }
    }
    public void SendAPI(BillReturnViewModel P)
    {
        try
        {
            // Obj_Adt.Update_APITransfer_status(Convert.ToInt32(Obj_Adt.Sr_no), "");
            string url = "http://202.166.207.75:9050/api/billreturn";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json;charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json;charset=utf-8";
            using (var streamwriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string loginjson = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(P);
                streamwriter.Write(loginjson);
                streamwriter.Flush();
                streamwriter.Close();

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Obj_Adt.Update_APITransfer_status(Convert.ToInt32(Obj_Adt.Sr_no), result);

                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void btndescresult_Click(object sender, ImageClickEventArgs e)
    {
        for (int i = 0; i < GV_Billcancel.Rows.Count; i++)
        {
            HiddenField ltlc = (GV_Billcancel.Rows[i].FindControl("hdnPID") as HiddenField);

            ImageButton DelOk = (ImageButton)GV_Billcancel.Rows[i].FindControl("btndescresult");
            if (DelOk == (ImageButton)sender)
            {
                string STCODE = DelOk.CommandArgument;
                if ((GV_Billcancel.Rows[i].FindControl("txtRefundAmt") as TextBox).Text!= "")
                {
                    Label lblap = (GV_Billcancel.Rows[i].FindControl("lblAmtPAid") as Label);
                    if (Convert.ToSingle((GV_Billcancel.Rows[i].FindControl("txtRefundAmt") as TextBox).Text) < Convert.ToSingle((GV_Billcancel.Rows[i].FindControl("lblAmtPAid") as Label).Text))
                    {
                        ObjPBC.P_PID = Convert.ToInt32(ltlc.Value);
                        ObjPBC.P_NetAmount = Convert.ToSingle((GV_Billcancel.Rows[i].FindControl("txtRefundAmt") as TextBox).Text);
                        ObjPBC.P_UserName = Convert.ToString(Session["username"]);
                        ObjPBC.BillRefund_Amt();
                    }
                    else
                    {
                        LblMSg.Text = "Refund Amt not greater than Receive Amt.";
                    }
                }
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

}