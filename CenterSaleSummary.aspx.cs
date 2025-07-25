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

public partial class CenterSaleSummary : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
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
                        checkexistpageright("CenterSaleSummary.aspx");
                    }
                }
     

                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");

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



 

    protected void btncensale_Click(object sender, EventArgs e)
    {
        string query = "ALTER VIEW [dbo].[VW_dssumm] AS (SELECT DISTINCT RTRIM(dbo.patmst.intial) + ' ' + dbo.patmst.Patname AS PatientName, dbo.patmst.PatRegID,  dbo.patmst.Phrecdate,  "+
               //" case when patmst.isactive=0 then -patmst.TestCharges else patmst.TestCharges+sum(RecM.RefundAmt) end as TestCharges, "+
             " case when patmst.isactive=0 then -patmst.TestCharges else case when convert(varchar, RecM.BillDate, 103) <>convert(varchar, dbo.patmst.Phrecdate, 103)  then'0' else  patmst.TestCharges+(sum(RecM.RefundAmt)+ ISNULL(sum(RecM.Othercharges), 0))end end as TestCharges,  "+
               " case when patmst.isactive=0 then -sum(RecM.Othercharges) else sum(RecM.Othercharges) end as Othercharges,   "+
               " case when patmst.isactive=0 then -sum(RecM.AmtPaid) else sum(RecM.AmtPaid) end as AmtPaid,   "+
               " case when patmst.isactive=0 then -CONVERT(FLOAT, sum(RecM.DisAmt)) else CONVERT(FLOAT,sum( RecM.DisAmt))end AS Discount,     "+
               " convert(varchar, RecM.BillDate, 103)  as RecDate, dbo.patmst.CenterCode, dbo.patmst.Centername, dbo.patmst.Drname, dbo.patmst.PID, dbo.patmst.branchid, dbo.patmst.TestName,  "+
               " dbo.RecM.BillNo , dbo.patmst.UnitCode, RecM.Billcancelno, RecM.TaxAmount "+
               " FROM         dbo.patmst LEFT OUTER JOIN dbo.RecM ON dbo.patmst.branchid = dbo.RecM.branchid AND  "+
               " dbo.patmst.PID = dbo.RecM.PID  where  patmst.isactive=1 and patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + "";
        if (fromdate.Text != "" && todate.Text != "")
        {
            query += " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }

        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"]);
        if (labcode != "" && labcode != null)
        {
            query = query + " and dbo.patmst.UnitCode = '" + labcode + "' ";
        }
        query += " group by patmst.intial, patmst.Patname,patmst.PatRegID, patmst.Phrecdate,patmst.isactive,patmst.TestCharges, " +
                "   dbo.patmst.Centername, dbo.patmst.Drname,  dbo.patmst.PID, dbo.patmst.branchid, " +
                "   dbo.patmst.testname, dbo.RecM.BillNo, dbo.patmst.UnitCode,RecM.TaxAmount,RecM.Billcancelno , convert(varchar, RecM.BillDate, 103),dbo.patmst.CenterCode ";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        cmd1.CommandText = query + ")";
        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
        ReportParameterClass.SelectionFormula = "";

        Session.Add("rptsql", "");
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_CenInsumm.rpt");
        Session["reportname"] = "Rpt_CenterWiseIncomeSummary";
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