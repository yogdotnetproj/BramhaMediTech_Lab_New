using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;

public partial class DrReport_Summary : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string drcode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("DrReport_Summary.aspx");
                }
              
                fromdate.Text = System.DateTime.Now.ToShortDateString();
                todate.Text = System.DateTime.Now.ToShortDateString();
                
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


 
    protected void btnreport_Click(object sender, EventArgs e)
    {
        string sql = "";

        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain_summary] AS (SELECT    sum( dbo.Doctor_CalculateAmount.DoctorAmount) AS Drcompamt,  dbo.patmst.DrName,  sum(dbo.Doctor_CalculateAmount.TotalAmount)as TotalAmount, " +
                  "  dbo.patmst.branchid,  sum(CAST(dbo.RecM.DisAmt AS numeric)) AS Discount,dbo.patmst.DoctorCode " +
                  "  FROM dbo.Doctor_CalculateAmount INNER JOIN    dbo.patmst ON dbo.Doctor_CalculateAmount.PatRegID = dbo.patmst.PatRegID AND  " +
                  "  dbo.Doctor_CalculateAmount.FID = dbo.patmst.FID AND  dbo.Doctor_CalculateAmount.branchid = dbo.patmst.branchid INNER JOIN " +
                  "  dbo.ReCM ON  dbo.patmst.PID = dbo.RecM.PID  " +
                  " WHERE  patmst.IsbillBH=0 and patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " "; //AND (dbo.Cshmst.Discount <> '')

        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        if (labcode != null && labcode != "")
        {

            cmd1.CommandText = cmd1.CommandText + " and patmst.UnitCode='" + labcode + "'";
        }
        if (fromdate.Text != "" && todate.Text != "")
        {
            cmd1.CommandText = cmd1.CommandText + " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }
        if (txtdoctor.Text != "")
        {
            string str = Convert.ToString(txtdoctor.Text);
            string[] s = str.Split('=');
            drcode = s[1];

            cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode=N'" + drcode.Trim() + "'";
        }
        cmd1.CommandText = cmd1.CommandText + "   group by dbo.patmst.DrName, dbo.patmst.branchid,dbo.patmst.DoctorCode ";
        cmd1.CommandText = cmd1.CommandText + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

    
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_docshareRep_summary.rpt");
        Session["reportname"] = "ComplimentReport_Summary1";
        Session["RPTFORMAT"] = "EXCEL";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

        // Response.Redirect("~/CrystalReportViewerForm.aspx");
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] FillDoctor(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select DoctorCode,rtrim(DrInitial)+' '+DoctorName as DoctorName from DrMT where ( DoctorName like N'%" + prefixText + "%' or DoctorCode like N'%" + prefixText + "%' ) and DrType='DR' and branchid=" + Convert.ToInt32(HttpContext.Current.Session["Branchid"]) + "", con);

        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] doctors = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {

            doctors.SetValue(dr["DoctorName"] + " = " + dr["DoctorCode"], i);
            i++;
        }
        return doctors;
    }

  
    protected void btnPdfReport_Click(object sender, EventArgs e)
    {
        string sql = "";
      
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain_summary] AS (SELECT    sum( dbo.Doctor_CalculateAmount.DoctorAmount) AS Drcompamt,  dbo.patmst.DrName,  sum(dbo.Doctor_CalculateAmount.TotalAmount)as TotalAmount, "+
                  "  dbo.patmst.branchid,  sum(CAST(dbo.RecM.DisAmt AS numeric)) AS Discount,dbo.patmst.DoctorCode "+
                  "  FROM dbo.Doctor_CalculateAmount INNER JOIN    dbo.patmst ON dbo.Doctor_CalculateAmount.PatRegID = dbo.patmst.PatRegID AND  "+
                  "  dbo.Doctor_CalculateAmount.FID = dbo.patmst.FID AND  dbo.Doctor_CalculateAmount.branchid = dbo.patmst.branchid INNER JOIN "+
                  "  dbo.ReCM ON  dbo.patmst.PID = dbo.RecM.PID  " +
                  " WHERE  patmst.IsbillBH=0 and patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " "; //AND (dbo.Cshmst.Discount <> '')

        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {

            cmd1.CommandText = cmd1.CommandText + " and patmst.UnitCode='" + labcode + "'";
        }
        if (fromdate.Text != "" && todate.Text != "")
        {
            cmd1.CommandText = cmd1.CommandText + " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
        }
        if (txtdoctor.Text != "")
        {
            string str = Convert.ToString(txtdoctor.Text);
            string[] s = str.Split('=');
            drcode = s[1];

            cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode=N'" + drcode.Trim() + "'";
        }
        cmd1.CommandText = cmd1.CommandText + "   group by dbo.patmst.DrName, dbo.patmst.branchid,dbo.patmst.DoctorCode ";
        cmd1.CommandText = cmd1.CommandText + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();

    
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_docshareRep_summary.rpt");
        Session["reportname"] = "ComplimentReport_Summary1";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

    }
    void BindGrid()
    {
        try
        {
           
            object fromDate = null;
            object Todate = null;

            if (fromdate.Text != "")
            {

                fromDate = DateTimeConvesion.getDateFromString(fromdate.Text.Trim()).ToString();

            }
            if (todate.Text != "")
            {
                Todate = DateTimeConvesion.getDateFromString(todate.Text.Trim()).ToString();
            }


            SqlConnection con = DataAccess.ConInitForDC();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandText = "ALTER VIEW [dbo].[VW_drspmain_summary] AS (SELECT    sum( dbo.Doctor_CalculateAmount.DoctorAmount) AS Drcompamt,  dbo.patmst.Drname,  sum(dbo.Doctor_CalculateAmount.TotalAmount)as TotalAmount, dbo.patmst.branchid, " +
                       " sum(CAST(dbo.RecM.DisAmt AS numeric)) AS Discount,dbo.patmst.DoctorCode " +
                      " FROM dbo.Doctor_CalculateAmount INNER JOIN    dbo.patmst ON dbo.Doctor_CalculateAmount.PatRegID = dbo.patmst.PatRegID AND "+
                      "  dbo.Doctor_CalculateAmount.FID = dbo.patmst.FID AND  dbo.Doctor_CalculateAmount.branchid = dbo.patmst.branchid INNER JOIN "+
                      "  dbo.ReCM ON  dbo.patmst.PID = dbo.RecM.PID " +
                      " WHERE  patmst.IsbillBH=0 and patmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " "; //AND (dbo.Cshmst.Discount <> '')

            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
            if (labcode != null && labcode != "")
            {

                cmd1.CommandText = cmd1.CommandText + " and patmst.UnitCode='" + labcode + "'";
            }
            if (fromdate.Text != "" && todate.Text != "")
            {
                cmd1.CommandText = cmd1.CommandText + " and dbo.patmst.Phrecdate between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
            }
            if (txtdoctor.Text != "")
            {
                string str = Convert.ToString(txtdoctor.Text);
                string[] s = str.Split('=');
                drcode = s[1];

                cmd1.CommandText = cmd1.CommandText + " and  patmst.DoctorCode=N'" + drcode.Trim() + "'";
            }
            cmd1.CommandText = cmd1.CommandText + "   group by dbo.patmst.drname, dbo.patmst.branchid,dbo.patmst.DoctorCode ";
            cmd1.CommandText = cmd1.CommandText + ")";

            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close(); con.Dispose();

            GV_Drcomp.DataSource = Cshmst_supp_Bal_C.GetDoctorcomplimentData_summary();
            GV_Drcomp.DataBind();
            float sum = 0.0f;

            for (int i = 0; i < GV_Drcomp.Rows.Count; i++)
            {
                sum += Convert.ToSingle(GV_Drcomp.Rows[i].Cells[3].Text);
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
    protected void GV_Drcomp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Drcomp.PageIndex = e.NewPageIndex;
        BindGrid();

    }
    protected void GV_Drcomp_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GV_Drcomp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
       
    }
    protected void btnlist_Click(object sender, EventArgs e)
    {
        BindGrid();
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