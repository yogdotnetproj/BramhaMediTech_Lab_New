using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Centersale :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    string CenterName = "", CenterCode = "", labcode_main = "";
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
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
                        checkexistpageright("Centersale.aspx");
                    }
                }

                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                CenterName = Convert.ToString(Session["CenterName"]);

                FillCenter();


                if (Session["usertype"].ToString().Trim() == "CollectionCenter")
                {
                    createuserTable_Bal_C Ctb = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                    ddlCenter.SelectedValue = (Ctb.CenterCode.Trim());
                    ddlCenter.Enabled = false;
                    ddlCenter.Width = 240;
                    ddlCenter.Height = 30;
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


    private void FillCenter()
    {
        try
        {
            ddlCenter.Visible = true;
            ddlCenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Session["UnitCode"], Convert.ToInt32(Session["Branchid"]));
            ddlCenter.DataTextField = "Name";
            ddlCenter.DataValueField = "DoctorCode";
            ddlCenter.DataBind();
            ddlCenter.Items.Insert(0, "All " + CenterName);
            ddlCenter.Items[0].Value = "0";
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




    protected void centerdetail_Click(object sender, EventArgs e)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        //string query = "ALTER VIEW [dbo].[VW_dsduedept] AS (SELECT DISTINCT RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS PatientName, dbo.patmst.PatRegID, dbo.patmst.Phrecdate, " +
        //" (patmst.TestCharges) as TestCharges, dbo.Cshmst.Othercharges, dbo.Cshmst.AmtPaid, CONVERT(FLOAT, dbo.Cshmst.Discount) AS Discount, " +
        //" dbo.Cshmst.RecDate, case when patmst.isactive=0 then 'CancelBill' else ''end  as CenterCode, dbo.patmst.Centername, dbo.patmst.Drname, " +
        //" dbo.patmst.PID, dbo.patmst.branchid, dbo.patmst.testname, dbo.Cshmst.BillNo, dbo.patmst.UnitCode, " +
        //    //" dbo.GetXRayAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalXRayAmount, " +
        //    //" dbo.GetSonographyAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalSonographyAmount, " +
        //    //" dbo.GetECGAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalECGAmount, " +
        //    // dbo.GetPathologyAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalPathologyAmount ,
        // " 0 AS TotalXRayAmount, " +
        //" 0 AS TotalSonographyAmount, " +
        //" 0 AS TotalECGAmount, " +
        //" 0  AS TotalPathologyAmount , Cshmst.TaxAmount,Cshmst.Billcancelno" +
        string query = "ALTER VIEW [dbo].[VW_dsduedept] AS SELECT distinct TOP (99.99) percent   RTRIM(patmst.intial) + ' ' + patmst.Patname AS PatientName, patmst.PatRegID, patmst.Phrecdate,   " +
                " case when patmst.isactive=0 then -patmst.TestCharges else  patmst.TestCharges+(sum(RecM.RefundAmt)+ ISNULL(sum(RecM.Othercharges), 0))end  as TestCharges,  "+
                //"    case when patmst.isactive=0 then -patmst.TestCharges else patmst.TestCharges+sum(RecM.RefundAmt) end as TestCharges, "+
                "    case when patmst.isactive=0 then -sum(RecM.Othercharges) else sum(RecM.Othercharges) end as Othercharges,   "+
                "    case when patmst.isactive=0 then -sum(RecM.AmtPaid) else sum(RecM.AmtPaid) end as AmtPaid,   "+
                "    case when patmst.isactive=0 then -CONVERT(FLOAT, sum(RecM.DisAmt)) else CONVERT(FLOAT,sum( RecM.DisAmt))end AS Discount,   "+
                "    convert(varchar, patmst.Phrecdate, 103)  as RecDate, " +
                "    case when patmst.isactive=0 then 'CancelBill' else ''end  as CenterCode, dbo.patmst.Centername, dbo.patmst.Drname,  dbo.patmst.PID, dbo.patmst.branchid, "+
                "    dbo.patmst.testname, dbo.RecM.BillNo, dbo.patmst.UnitCode,  0 AS TotalXRayAmount,  0 AS TotalSonographyAmount,  0 AS TotalECGAmount,  0  AS TotalPathologyAmount , "+
                "    RecM.TaxAmount,RecM.Billcancelno "+
                "    FROM dbo.RecM RIGHT OUTER JOIN dbo.patmst ON dbo.RecM.branchid = dbo.patmst.branchid "+
                "    AND dbo.RecM.PID = dbo.patmst.PID" +
        " where patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";
        if (fromdate.Text != "" && todate.Text != "")
        {
            query += " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }
        if (ddlCenter.SelectedIndex > 0)
        {
            query += " and patmst.CenterCode='" + ddlCenter.SelectedValue.Trim() + "'";
        }
        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"]);
        if (labcode != "" && labcode != null)
        {
            query = query + " and dbo.patmst.UnitCode = '" + labcode + "' ";
        }
        if (txtreferdoctor.Text.Trim() != "")
        {
            string DocCode = "";
            string[] data = txtreferdoctor.Text.Trim().Split('=');
            if (data.Length > 1)
            {
                DocCode = data[1].Trim();
            }

            query += " and patmst.DoctorCode='" + DocCode.Trim() + "'";
        }

        query += " group by patmst.intial, patmst.Patname,patmst.PatRegID, patmst.Phrecdate,patmst.isactive,patmst.TestCharges, "+
                 "   dbo.patmst.Centername, dbo.patmst.Drname,  dbo.patmst.PID, dbo.patmst.branchid, "+
                 "   dbo.patmst.testname, dbo.RecM.BillNo, dbo.patmst.UnitCode,RecM.TaxAmount,RecM.Billcancelno  order by  PID asc ";
      //  cmd1.CommandText = query + ")";
        cmd1.CommandText = query ;
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        //ReportParameterClass.ReportType = "CenterWiseIncomeDetailWithDept";//
        ReportParameterClass.SelectionFormula = "";

        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_CenInsummD.rpt");
        Session["reportname"] = "Rpt_CenterWiseIncomeDetailWithDept";
        Session["RPTFORMAT"] = "pdf";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);


        // ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

    }

    protected void btnexcel_Click(object sender, EventArgs e)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        //string query = "ALTER VIEW [dbo].[VW_dsduedept] AS (SELECT DISTINCT RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS PatientName, dbo.patmst.PatRegID, dbo.patmst.Phrecdate, " +
        //" (patmst.TestCharges) as TestCharges, dbo.Cshmst.Othercharges, dbo.Cshmst.AmtPaid, CONVERT(float, dbo.Cshmst.Discount) AS Discount, " +
        //" dbo.Cshmst.RecDate, dbo.patmst.CenterCode, dbo.patmst.Centername, dbo.patmst.Drname, " +
        //" dbo.patmst.PID, dbo.patmst.branchid, dbo.patmst.testname, dbo.Cshmst.BillNo, dbo.patmst.UnitCode, " +
        //    //" dbo.GetXRayAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalXRayAmount, " +
        //    //" dbo.GetSonographyAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalSonographyAmount, " +
        //    //" dbo.GetECGAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalECGAmount, " +
        //    //" dbo.GetPathologyAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalPathologyAmount ,
        //   " 0 AS TotalXRayAmount, " +
        //" 0 AS TotalSonographyAmount, " +
        //" 0 AS TotalECGAmount, " +
        //" 0  AS TotalPathologyAmount ,Cshmst.TaxAmount,Cshmst.Billcancelno" +
        //" FROM dbo.Cshmst RIGHT OUTER JOIN dbo.patmst ON dbo.Cshmst.branchid = dbo.patmst.branchid AND " +
        //" dbo.Cshmst.CenterCode = dbo.patmst.CenterCode AND dbo.Cshmst.PID = dbo.patmst.PID " +
        //" where patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";
        string query = "ALTER VIEW [dbo].[VW_dsduedept] AS SELECT distinct TOP (99.99) percent   RTRIM(patmst.intial) + ' ' + patmst.Patname AS PatientName, patmst.PatRegID, patmst.Phrecdate,   " +
              // "    case when patmst.isactive=0 then -patmst.TestCharges else patmst.TestCharges+sum(RecM.RefundAmt) end as TestCharges, " +
              " case when patmst.isactive=0 then -patmst.TestCharges else patmst.TestCharges+(sum(RecM.RefundAmt)+ ISNULL(sum(RecM.Othercharges), 0))end  as TestCharges, "+
              "    case when patmst.isactive=0 then -sum(RecM.Othercharges) else sum(RecM.Othercharges) end as Othercharges,   " +
               "    case when patmst.isactive=0 then -sum(RecM.AmtPaid) else sum(RecM.AmtPaid) end as AmtPaid,   " +
               "    case when patmst.isactive=0 then -CONVERT(FLOAT, sum(RecM.DisAmt)) else CONVERT(FLOAT,sum( RecM.DisAmt))end AS Discount,   " +
               "    convert(varchar, patmst.Phrecdate, 103)  as RecDate, " +
               "    case when patmst.isactive=0 then 'CancelBill' else ''end  as CenterCode, dbo.patmst.Centername, dbo.patmst.Drname,  dbo.patmst.PID, dbo.patmst.branchid, " +
               "    dbo.patmst.testname, dbo.RecM.BillNo, dbo.patmst.UnitCode,  0 AS TotalXRayAmount,  0 AS TotalSonographyAmount,  0 AS TotalECGAmount,  0  AS TotalPathologyAmount , " +
               "    RecM.TaxAmount,RecM.Billcancelno " +
               "    FROM dbo.RecM RIGHT OUTER JOIN dbo.patmst ON dbo.RecM.branchid = dbo.patmst.branchid " +
               "    AND dbo.RecM.PID = dbo.patmst.PID" +
       " where patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";
        if (fromdate.Text != "" && todate.Text != "")
        {
            query += " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }
        if (ddlCenter.SelectedIndex > 0)
        {
            query += " and patmst.CenterCode='" + ddlCenter.SelectedValue.Trim() + "'";
        }
        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"]);
        if (labcode != "" && labcode != null)
        {
            query = query + " and dbo.patmst.UnitCode = '" + labcode + "' ";
        }
        if (txtreferdoctor.Text.Trim() != "")
        {
            string DocCode = "";
            string[] data = txtreferdoctor.Text.Trim().Split('=');
            if (data.Length > 1)
            {
                DocCode = data[1].Trim();
            }

            query += " and patmst.DoctorCode='" + DocCode.Trim() + "'";
        }

        query += " group by patmst.intial, patmst.Patname,patmst.PatRegID, patmst.Phrecdate,patmst.isactive,patmst.TestCharges, " +
                 "   dbo.patmst.Centername, dbo.patmst.Drname,  dbo.patmst.PID, dbo.patmst.branchid, " +
                 "   dbo.patmst.testname, dbo.RecM.BillNo, dbo.patmst.UnitCode,RecM.TaxAmount,RecM.Billcancelno  order by  PID asc ";
       // cmd1.CommandText = query + ")";
        cmd1.CommandText = query ;
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();


        ReportParameterClass.SelectionFormula = "";

        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_CenInsummD.rpt");
        Session["reportname"] = "Rpt_CenterWiseIncomeDetailWithDept";
        Session["RPTFORMAT"] = "EXCEL";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);


        // ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
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
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like  N'" + prefixText + "%' and DrType='DR' and branchid=" + branchid + " order by DoctorName", con);
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
    protected void btndoctorincome_Click(object sender, EventArgs e)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        //string query = "ALTER VIEW [dbo].[VW_dsduedept] AS (SELECT DISTINCT RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS PatientName, dbo.patmst.PatRegID, dbo.patmst.Phrecdate, " +
        //" (patmst.TestCharges) as TestCharges, dbo.Cshmst.Othercharges, dbo.Cshmst.AmtPaid, CONVERT(FLOAT, dbo.Cshmst.Discount) AS Discount, " +
        //" dbo.Cshmst.RecDate, case when patmst.isactive=0 then 'CancelBill' else ''end  as CenterCode, dbo.patmst.Centername, dbo.patmst.Drname, " +
        //" dbo.patmst.PID, dbo.patmst.branchid, dbo.patmst.testname, dbo.Cshmst.BillNo, dbo.patmst.UnitCode, " +
        //    //" dbo.GetXRayAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalXRayAmount, " +
        //    //" dbo.GetSonographyAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalSonographyAmount, " +
        //    //" dbo.GetECGAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalECGAmount, " +
        //    // dbo.GetPathologyAmountCal(dbo.patmst.PID, dbo.patmst.branchid) AS TotalPathologyAmount ,
        // " 0 AS TotalXRayAmount, " +
        //" 0 AS TotalSonographyAmount, " +
        //" 0 AS TotalECGAmount, " +
        //" 0  AS TotalPathologyAmount , Cshmst.TaxAmount,Cshmst.Billcancelno" +
        string query = "ALTER VIEW [dbo].[VW_dsduedept] AS SELECT distinct TOP (99.99) percent   RTRIM(patmst.intial) + ' ' + patmst.Patname AS PatientName, patmst.PatRegID, patmst.Phrecdate,   " +
                 //"    case when patmst.isactive=0 then -patmst.TestCharges else patmst.TestCharges+sum(RecM.RefundAmt) end as TestCharges, " +
                " case when patmst.isactive=0 then -patmst.TestCharges else   patmst.TestCharges+(sum(RecM.RefundAmt)+ ISNULL(sum(RecM.Othercharges), 0))end  as TestCharges,  "+
                 "    case when patmst.isactive=0 then -sum(RecM.Othercharges) else sum(RecM.Othercharges) end as Othercharges,   " +
                 "    case when patmst.isactive=0 then -sum(RecM.AmtPaid) else sum(RecM.AmtPaid) end as AmtPaid,   " +
                 "    case when patmst.isactive=0 then -CONVERT(FLOAT, sum(RecM.DisAmt)) else CONVERT(FLOAT,sum( RecM.DisAmt))end AS Discount,   " +
                 "    convert(varchar, patmst.Phrecdate, 103)  as RecDate, " +
                 "    case when patmst.isactive=0 then 'CancelBill' else ''end  as CenterCode, dbo.patmst.Centername, dbo.patmst.Drname,  dbo.patmst.PID, dbo.patmst.branchid, " +
                 "    dbo.patmst.testname, dbo.RecM.BillNo, dbo.patmst.UnitCode,  0 AS TotalXRayAmount,  0 AS TotalSonographyAmount,  0 AS TotalECGAmount,  0  AS TotalPathologyAmount , " +
                 "    RecM.TaxAmount,RecM.Billcancelno " +
                 "    FROM dbo.RecM RIGHT OUTER JOIN dbo.patmst ON dbo.RecM.branchid = dbo.patmst.branchid " +
                 "    AND dbo.RecM.PID = dbo.patmst.PID" +
         " where patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";
        if (fromdate.Text != "" && todate.Text != "")
        {
            query += " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }
        if (ddlCenter.SelectedIndex > 0)
        {
            query += " and patmst.CenterCode='" + ddlCenter.SelectedValue.Trim() + "'";
        }
        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"]);
        if (labcode != "" && labcode != null)
        {
            query = query + " and dbo.patmst.UnitCode = '" + labcode + "' ";
        }
        if (txtreferdoctor.Text.Trim() != "")
        {
            string DocCode = "";
            string[] data = txtreferdoctor.Text.Trim().Split('=');
            if (data.Length > 1)
            {
                DocCode = data[1].Trim();
            }

            query += " and patmst.DoctorCode='" + DocCode.Trim() + "'";
        }

        query += " group by patmst.intial, patmst.Patname,patmst.PatRegID, patmst.Phrecdate,patmst.isactive,patmst.TestCharges, " +
                 "   dbo.patmst.Centername, dbo.patmst.Drname,  dbo.patmst.PID, dbo.patmst.branchid, " +
                 "   dbo.patmst.testname, dbo.RecM.BillNo, dbo.patmst.UnitCode,RecM.TaxAmount,RecM.Billcancelno  order by  PID asc ";
        //cmd1.CommandText = query + ")";
        cmd1.CommandText = query;
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

        //ReportParameterClass.ReportType = "CenterWiseIncomeDetailWithDept";//
        ReportParameterClass.SelectionFormula = "";

        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_DrIncomeReport.rpt");
        Session["reportname"] = "Rpt_DrIncomeReport";
        Session["RPTFORMAT"] = "pdf";

        Session["Parameter"] = "Yes";
        Session["rptDate"] = fromdate.Text + "  To  " + todate.Text;
        Session["rptusername"] = Convert.ToString(Session["username"]);


        // ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
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