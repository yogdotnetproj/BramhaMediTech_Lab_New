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

public partial class ActivityDetails : System.Web.UI.Page
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

               
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                Binddropdown();
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
            string Patname = "";
            if (txtPatientname.Text.Trim() != "")
            {
                string[] data = txtPatientname.Text.Trim().Split('=');
                if (data.Length > 1)
                {
                    Patname = data[0].Trim();
                }
            }
            GridView1.DataSource = Cshmst_supp_Bal_C.Get_ActivityDetailsReport(todate1, fromDate1, ddlusername.SelectedValue, Patname, Convert.ToInt32(Session["Branchid"]));
            GridView1.DataBind();

            //float sum = 0.0f;
            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    sum += Convert.ToSingle(GridView1.Rows[i].Cells[4].Text);
            //}
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


    private void Binddropdown()
    {
        try
        {

           
                ddlusername.DataSource = createuserlogic_Bal_C.getAllUsers(Convert.ToInt32(Session["Branchid"]));
                ddlusername.DataTextField = "username";
                ddlusername.DataValueField = "username";
                ddlusername.DataBind();
                ddlusername.Items.Insert(0, "Select UserName");
                ddlusername.SelectedIndex = -1;
                
            
            

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
            //string username = GridView1.Rows[e.NewEditIndex].Cells[9].Text;
            //ViewState["username"] = username;
            //ViewState["tdate"] = GridView1.Rows[e.NewEditIndex].Cells[0].Text;

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
        //if (e.Row.Cells[2].Text != "")
        //{
        //    string date = e.Row.Cells[0].Text;
        //    date = Convert.ToDateTime(date).ToShortDateString();
        //    e.Row.Cells[0].Text = date;
        //}
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid();
    }




    protected void btnreport1_Click(object sender, EventArgs e)
    {
        string sql = "";
        string Patname = "";
        if (txtPatientname.Text.Trim() != "")
        {
            string[] data = txtPatientname.Text.Trim().Split('=');
            if (data.Length > 1)
            {
                Patname = data[0].Trim();
            }
        }
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[VW_ActivityDetails] AS (SELECT       ActivityDetails.Id, ActivityDetails.Patregno, ActivityDetails.FormName, ActivityDetails.EventName, ActivityDetails.CreatedBy, ActivityDetails.CreatedOn, "+
                      "  convert(varchar(20),ActivityDetails.CreatedOn,103)+' '+convert(varchar(20),convert(time,ActivityDetails.CreatedOn),100) as ActivityDateTime, ActivityDetails.Branchid, patmst.intial, patmst.Patname, patmst.sex, "+
                      "  patmst.Age, patmst.Patregdate, patmst.Patphoneno, patmst.Isemergency, patmst.Reportdate, patmst.RefDr, patmst.MDY, patmst.DoctorCode, patmst.CenterCode, patmst.CenterName, patmst.RegistratonDateTime, "+
                      "  patmst.TelNo,patmst.intial+' '+ patmst.Patname as PatientName "+
                      "  FROM            ActivityDetails INNER JOIN "+
                      "  patmst ON ActivityDetails.Patregno = patmst.PatRegID  " +
        " where ActivityDetails.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";
 
        if (fromdate.Text != "" && todate.Text != "")
        {
            query += " and (CAST(CAST(YEAR( ActivityDetails.CreatedOn) AS varchar(4)) + '/' + CAST(MONTH( ActivityDetails.CreatedOn) AS varchar(2)) + '/' + CAST(DAY(ActivityDetails.CreatedOn) AS varchar(2)) AS datetime)) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }

        if (Patname != "" && Patname != "0" && Patname != null)
        {
            query += " and ActivityDetails.Patregno= '" + Patname + "'";
        }
        if (ddlusername.SelectedValue != "Select UserName" && ddlusername.SelectedValue != "0" && ddlusername.SelectedValue != null)
        {
            query += " and  ActivityDetails.CreatedBy= '" + ddlusername.SelectedValue + "'";
        }


        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_ActivityDetails.rpt");
        Session["reportname"] = "Rpt_ActivityDetails";
        Session["RPTFORMAT"] = "pdf";

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
            tests.SetValue(dr["PatRegID"] + " = " + dr["Patname"], i);
            i++;
        }
        return tests;
    }
}