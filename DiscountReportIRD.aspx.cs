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

public partial class DiscountReportIRD : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string labcode_main = "";
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
                        checkexistpageright("DiscountReportIRD.aspx");
                    }
                }
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
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


  

    protected void btnlist_Click(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void CashGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void CashGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

    protected void CashGrid_Sorting(object sender, GridViewSortEventArgs e)
    {

        BindGrid();
    }

    void BindGrid()
    {
        try
        {
           
            object fromDate1 = null;
            object todate1 = null;

            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);

            if (labcode != null && labcode != "")
            {
                this.labcode_main = labcode;
            }

            if (fromdate.Text != "")
            {
                fromDate1 = DateTimeConvesion.getDateFromString(fromdate.Text.Trim()).ToString();
            }
            if (todate.Text != "")
            {
                todate1 = DateTimeConvesion.getDateFromString(todate.Text.Trim()).ToString();
            }
            string username = "";


            GridView1.DataSource = Cshmst_supp_Bal_C.GetDiscountwisereport(todate1, fromDate1, username, txtPatientname.Text, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), this.labcode_main);
            GridView1.DataBind();

            float sum = 0.0f;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                sum += Convert.ToSingle(GridView1.Rows[i].Cells[4].Text);
            }
            
            //lbltotal.Text = sum.ToString();
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





    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string username = GridView1.Rows[e.NewEditIndex].Cells[9].Text;
            ViewState["username"] = username;
            ViewState["tdate"] = GridView1.Rows[e.NewEditIndex].Cells[0].Text;

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

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
        if (e.Row.Cells[2].Text != "")
        {
            string date = e.Row.Cells[0].Text;
            date = Convert.ToDateTime(date).ToShortDateString();
            e.Row.Cells[0].Text = date;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
    }




    protected void btnreport1_Click(object sender, EventArgs e)
    {
        string sql = "";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[VW_disdatavw] AS (SELECT        CONVERT(char(12), RecM.TransDate, 105) AS BillDate, Patmst.Testcharges as NetPayment, RecM.username, patmst.PatRegID, patmst.Patphoneno, RecM.BillNo, sum(RecM.Amtpaid) as AmtReceived, RecM.Paymenttype, RecM.Disamt as Discount,  " +
                  "  patmst.intial + ' ' + patmst.Patname AS Patientname,sum( RecM.AmtPaid) as AmtPaid, Patmst.Testcharges - (sum( RecM.AmtPaid)+sum( RecM.Disamt))as Balance, patmst.Drname, CAST(patmst.Age AS varchar(255)) + ' ' + patmst.MDY + '/' + patmst.sex AS Page, RecM.Comment as DisRemark, SUM(RecM.LabGiven)  " +
                  "  AS LabGiven, SUM(RecM.DrGiven) AS DrGiven ,RecM.PID " +
        "   FROM             patmst INNER JOIN RecM ON patmst.PID = RecM.PID  " +
        " where RecM.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";
 
        if (fromdate.Text != "" && todate.Text != "")
        {
            query += " and CAST( RecM.Disamt AS float)>0  and  (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime)) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }

        if (txtPatientname.Text != "" && txtPatientname.Text != "0" && txtPatientname.Text != null)
        {
            query += " and RecM.Comment like '%" + txtPatientname.Text + "%'";
        }

        query += "  group by   CONVERT(char(12),  RecM.TransDate, 105), Patmst.Testcharges, recm.username, patmst.PatRegID, patmst.Patphoneno, recm.BillNo,  RecM.Paymenttype,RecM.Disamt, "+
                 "  patmst.intial + ' ' + Patmst.Patname,   patmst.Drname, CAST(patmst.Age AS varchar(255)), patmst.MDY, patmst.sex, RecM.Comment,RecM.PID ";
        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_Discountreport.rpt");
        Session["reportname"] = "Discountreport";
        Session["RPTFORMAT"] = "pdf";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

    }
    [WebMethod]
    [ScriptMethod]
    public static string[] GetPatientName(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM patmst where Patname like '%" + prefixText + "%'  and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by Patname ", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM patmst where Patname like '%" + prefixText + "%'  and branchid=" + branchid + " order by Patname", con);
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