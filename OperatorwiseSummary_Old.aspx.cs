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

public partial class OperatorwiseSummary : System.Web.UI.Page
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
                LUNAME.Text = Convert.ToString(Session["username"]);
                LblDCName.Text = Convert.ToString(Session["Bannername"]);
                LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                this.PopulateTreeView(dt, 0, null);
                Bindddl();
               
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
    private void Bindddl()
    {
        try
        {
            if (Session["usertype"].ToString() == "Admin" || Session["usertype"].ToString().ToLower() == "administrator")
            {
                ddlusername.DataSource = createuserlogic_Bal_C.getAllUsers(Convert.ToInt32(Session["Branchid"]));
                ddlusername.DataTextField = "username";
                ddlusername.DataValueField = "username";
                ddlusername.DataBind();
                ddlusername.Items.Insert(0, "Select UserName");
                ddlusername.SelectedIndex = -1;
            }
            else
            {
                ddlusername.DataSource = createuserlogic_Bal_C.getAllUsers_username(Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"]));
                ddlusername.DataTextField = "username";
                ddlusername.DataValueField = "username";
                ddlusername.DataBind();
                ddlusername.Items.Insert(0, "Select UserName");
                ddlusername.SelectedIndex = 1;
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

    protected void btnlist_Click(object sender, EventArgs e)
    {
        BindGrid();
    }



    void BindGrid()
    {
        try
        {
            string  Center = "";
            object fromDate = null;
            object Todate = null;

            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );

            if (labcode != null && labcode != "")
            {
                this.labcode_main = labcode;
            }

            if (fromdate.Text != "")
            {
                fromDate = DateTimeConvesion.getDateFromString(fromdate.Text.Trim()).ToString();
            }
            if (todate.Text != "")
            {
                Todate = DateTimeConvesion.getDateFromString(todate.Text.Trim()).ToString();
            }
           
            if (Session["usertype"].ToString() == "CollectionCenter")
            {
                Center = Session["CenterCode"].ToString();
            }

            string username = "";           

            GV_OpSummary.DataSource = Cshmst_supp_Bal_C.Get_DPRRepotData(Todate, fromDate, username, Center, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), this.labcode_main);
            GV_OpSummary.DataBind();
            
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

   
    protected void btnreport_Click(object sender, EventArgs e)
    {
        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"] );

        #region Myregion2
        SqlConnection conn2 = DataAccess.ConInitForDC();
        SqlCommand sc2 = conn2.CreateCommand();
        sc2.CommandText = "ALTER VIEW [dbo].[VW_dsbill] AS (SELECT DISTINCT dbo.Cshmst.CenterCode as CCode, dbo.Cshmst.RecDate, dbo.Cshmst.Paymenttype AS Expr2, dbo.Cshmst.Patname, "+
                  "  dbo.Cshmst.Balance, dbo.Cshmst.branchid, dbo.patmst.Patregdate, dbo.Cshmst.DisFlag,  "+
                  "  dbo.Cshmst.Discount, dbo.Cshmst.NetPayment, dbo.Cshmst.BillNo, dbo.Cshmst.AmtPaid, dbo.patmst.PatRegID,  dbo.patmst.intial, "+
                  "  dbo.patmst.Patname as FirstName,  dbo.patmst.DrName,  dbo.VW_drdt.DoctorName AS LabName, dbo.VW_drdt.Doctorcode AS DoctorName, " +
                  "  dbo.patmst.RegistratonDateTime, dbo.patmst.CenterCode ,  dbo.Cshmst.Othercharges, dbo.Cshmst.DigModule,  "+
                  "  dbo.patmst.RefDr, CAST(dbo.patmst.Tests AS varchar(255)) AS Test1,  dbo.patmst.testname, dbo.patmst.TelNo, "+
                  "  dbo.VW_rtcrgdata.TestCharges, dbo.patmst.FID,  dbo.RecM.AmtPaid AS rec_amt, dbo.RecM.Paymenttype as Modeofpay,  "+
                  "  convert(date,RecM.transdate)as transdate , dbo.RecM.username ,dbo.patmst.UnitCode,dbo.patmst.UnitCode AS LabCode,BillAmt, RecM.DisAmt ,RecM.BalAmt, "+
                  "  dbo.patmst.Phrecdate,RecM.PrevBal  , Cshmst.TaxAmount, Cshmst.TaxPer  , patmst.IsbillBH , case when patmst.IsbillBH=0 then "+
                  "  RecM.PrevBal else 0 end as Mainbalance,  case when patmst.IsbillBH=1 then RecM.PrevBal else 0 end as BTHbalance ,  "+
                  "  case when RecM.Paymenttype='Cash' then (RecM.AmtPaid) else  0 end as cashrec ,  case when RecM.Paymenttype='Cash' then 0 else  "+
                  "  (RecM.AmtPaid) end as Cardrec ,Cshmst.Comment as DisRemark FROM dbo.Cshmst INNER JOIN dbo.patmst ON dbo.Cshmst.PID = dbo.patmst.PID AND  "+
                  "  dbo.Cshmst.branchid = dbo.patmst.branchid INNER JOIN  dbo.VW_rtcrgdata ON dbo.patmst.PID = dbo.VW_rtcrgdata.PID INNER JOIN "+
                  "  dbo.RecM ON dbo.Cshmst.BillNo = dbo.RecM.BillNo AND  dbo.Cshmst.branchid = dbo.RecM.branchid LEFT OUTER JOIN  dbo.VW_drdt ON "+
                  "  dbo.VW_rtcrgdata.branchid = dbo.VW_drdt.branchid AND dbo.patmst.Doctorcode = dbo.VW_drdt.DoctorName  " +
                      " Where 1=1";
        if (fromdate.Text != "")
        {
            sc2.CommandText += " and (CAST(CAST(YEAR(transdate) AS varchar(4)) + '/' + CAST(MONTH(transdate) AS varchar(2)) + '/' + CAST(DAY(transdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
        }
        if (ddlusername.SelectedItem.Text != "Select UserName")
        {
            sc2.CommandText += " and dbo.RecM.username='" + ddlusername.SelectedItem.Text + " ' ";// 
        }
        sc2.CommandText += "   union all SELECT DISTINCT dbo.Cshmst.CenterCode as CCode, dbo.Cshmst.RecDate, dbo.Cshmst.Paymenttype AS Expr2, dbo.Cshmst.Patname,  "+    
                  "  dbo.Cshmst.Balance, dbo.Cshmst.branchid, dbo.patmst.Patregdate, dbo.Cshmst.DisFlag,  "+
                  "  -CAST(Cshmst.Discount as numeric(9,2))as Discount,    -dbo.Cshmst.NetPayment, dbo.Cshmst.BillNo, -dbo.Cshmst.AmtPaid, dbo.patmst.PatRegID,   "+
                  "  dbo.patmst.intial, dbo.patmst.Patname as FirstName,  dbo.patmst.DrName,     dbo.VW_drdt.DoctorName AS LabName, dbo.VW_drdt.Doctorcode AS DoctorName,  " +
                  "  dbo.patmst.RegistratonDateTime, dbo.patmst.CenterCode ,  dbo.Cshmst.Othercharges,     dbo.Cshmst.DigModule, dbo.patmst.RefDr,  "+
                  "  CAST(dbo.patmst.Tests AS varchar(255)) AS Test1,  dbo.patmst.testname, dbo.patmst.TelNo,      -dbo.VW_rtcrgdata.TestCharges, dbo.patmst.FID,  "+
                  "  -dbo.RecM.AmtPaid AS rec_amt, dbo.RecM.Paymenttype as Modeofpay ,  convert(date,RecM.transdate)as transdate ,     dbo.RecM.username , "+
                  "  dbo.patmst.UnitCode ,dbo.patmst.UnitCode AS LabCode,-BillAmt, -RecM.DisAmt ,RecM.BalAmt, dbo.patmst.Phrecdate,-RecM.PrevBal  ,     "+
                  "  - Cshmst.TaxAmount, Cshmst.TaxPer  , patmst.IsbillBH , case when patmst.IsbillBH=0 then RecM.PrevBal else 0 end as Mainbalance,       "+
                  "  case when patmst.IsbillBH=1 then RecM.PrevBal else 0 end as BTHbalance , case when RecM.Paymenttype='Cash' then -(RecM.AmtPaid) else  0 end as cashrec , "+
                  "  case when RecM.Paymenttype='Cash' then 0 else  -(RecM.AmtPaid) end as Cardrec ,Cshmst.Comment     "+
                  "  FROM dbo.Cshmst INNER JOIN dbo.patmst ON dbo.Cshmst.PID = dbo.patmst.PID AND  dbo.Cshmst.branchid = dbo.patmst.branchid INNER JOIN      "+
                  "  dbo.VW_rtcrgdata ON dbo.patmst.PID = dbo.VW_rtcrgdata.PID INNER JOIN  dbo.RecM ON dbo.Cshmst.BillNo = dbo.RecM.BillNo AND      "+
                  "  dbo.Cshmst.branchid = dbo.RecM.branchid LEFT OUTER JOIN  dbo.VW_drdt ON dbo.VW_rtcrgdata.branchid = dbo.VW_drdt.branchid AND    " +
                  "  dbo.patmst.Doctorcode = dbo.VW_drdt.DoctorName  "+
                    " Where 1=1";
        if (fromdate.Text != "")
        {
            sc2.CommandText += " and patmst.IsFreeze=1 and (CAST(CAST(YEAR(transdate) AS varchar(4)) + '/' + CAST(MONTH(transdate) AS varchar(2)) + '/' + CAST(DAY(transdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";// 
        }
        if (ddlusername.SelectedItem.Text != "Select UserName")
        {
            sc2.CommandText += " and dbo.RecM.username='" + ddlusername.SelectedItem.Text + " ' ";// 
        }

        sc2.CommandText += ")";

        conn2.Open();
        try
        {
            sc2.ExecuteNonQuery();
        }
        catch (Exception) { }
        conn2.Close(); conn2.Dispose();
        #endregion

        string sql = "";
        ReportParameterClass.ReportType = "DailyCashSum";
        string username = "";
       
        if (Session["usertype"].ToString() == "CollectionCenter")
        {
            ReportParameterClass.SelectionFormula = "{VW_dsbillpr.transdate} in DateTime ('" + Convert.ToDateTime(fromdate.Text).ToString("dd/MM/yyyy") + "') to DateTime ('" + Convert.ToDateTime(todate.Text).AddDays(+1).ToString("dd/MM/yyyy") + "') and {VW_dsbillpr.Branchid}=" + Convert.ToInt32(Session["Branchid"]) + " and {VW_dsbillpr.maindeptid}=" + Convert.ToInt32(Session["DigModule"]) + " and {VW_dsbillpr.Centercode}='" + Session["CenterCode"].ToString() + "'";            
        }
        else
        {           
            if (labcode != "" && labcode != null)
            {
                ReportParameterClass.SelectionFormula = "{VW_dsbillpr.transdate} in DateTime ('" + Convert.ToDateTime(fromdate.Text).ToString("dd/MM/yyyy") + "') to DateTime ('" + Convert.ToDateTime(todate.Text).AddDays(+1).ToString("dd/MM/yyyy") + "') and {VW_dsbillpr.Branchid}=" + Convert.ToInt32(Session["Branchid"]) + " and {VW_dsbillpr.maindeptid}=" + Convert.ToInt32(Session["DigModule"]) + " and {VW_dsbillpr.LabCode} = '" + labcode + "'";
            }
            else
            {
                ReportParameterClass.SelectionFormula = "{VW_dsbillpr.transdate} in DateTime ('" + Convert.ToDateTime(fromdate.Text).ToString("dd/MM/yyyy") + "') to DateTime ('" + Convert.ToDateTime(todate.Text).AddDays(+1).ToString("dd/MM/yyyy") + "') and {VW_dsbillpr.Branchid}=" + Convert.ToInt32(Session["Branchid"]) + " ";
            }            
        }
       
        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_dailycashierDet.rpt");
        Session["reportname"] = "DailyCashSummary";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

    }

    protected void GV_OpSummary_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GV_OpSummary_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string username = GV_OpSummary.Rows[e.NewEditIndex].Cells[10].Text;
            ViewState["username"] = username;
            ViewState["tdate"] = GV_OpSummary.Rows[e.NewEditIndex].Cells[0].Text;

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

    protected void GV_OpSummary_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void GV_OpSummary_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_OpSummary.PageIndex = e.NewPageIndex;
        BindGrid();
    }

}