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

public partial class Salesummaryreport_IRD : System.Web.UI.Page
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
                        checkexistpageright("Saleregisterreport.aspx");
                    }
                }
                LUNAME.Text = Convert.ToString(Session["username"]);
                LblDCName.Text = Convert.ToString(Session["Bannername"]);
                LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                this.PopulateTreeView(dt, 0, null);
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
            string sortValue = "", Center = "";
            object fromDate1 = null;
            object todate1 = null;

            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );

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
           

            if (Session["usertype"].ToString() == "CollectionCenter")
            {
                Center = Session["CenterCode"].ToString();
            }

            string username = "";           

            GridView1.DataSource = Cshmst_supp_Bal_C.getLoginfoMainData_sale_summary(todate1, fromDate1, username, Center, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), this.labcode_main);
            GridView1.DataBind();
           
            float sum = 0.0f, amt = 0.0f, disc = 0.0f, tax = 0.0f, taxa = 0.0f;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                amt += Convert.ToSingle(GridView1.Rows[i].Cells[3].Text);
                disc += Convert.ToSingle(GridView1.Rows[i].Cells[4].Text);
                taxa += Convert.ToSingle(GridView1.Rows[i].Cells[5].Text);
                tax += Convert.ToSingle(GridView1.Rows[i].Cells[6].Text);
                sum += Convert.ToSingle(GridView1.Rows[i].Cells[7].Text);
            }
            LblAmount.Text = amt.ToString();
            Lbldiscount.Text = disc.ToString();
            Lbltaxable.Text = taxa.ToString();
            Lbltaxontaxable.Text = tax.ToString();
            Lblnetamount.Text = sum.ToString();
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
            sda = new SqlDataAdapter("SELECT  DoctorCode, rtrim(DrInitial)+' '+DoctorName as name FROM DrMT where ( DoctorName like '" + prefixText + "%' or DoctorCode like '" + prefixText + "%') and DrType='DR' and UntiCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT  DoctorCode, rtrim(DrInitial)+' '+DoctorName as name FROM DrMT where  ( DoctorName like '" + prefixText + "%' or DoctorCode like '" + prefixText + "%')  and DrType='DR' and branchid=" + branchid + " order by DoctorName", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            // tests.SetValue(dr["DoctorName"], i);
            tests.SetValue(dr["name"] + " = " + dr["DoctorCode"], i);
            i++;
        }
        return tests;
    }
    [WebMethod]
    [ScriptMethod]
    public static string[] GetPatientName(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string UntiCode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (UntiCode != null && UntiCode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM patmst where Patname like '%" + prefixText + "%'  and UnitCode='" + UntiCode.ToString().Trim() + "' and branchid=" + branchid + " order by Patname ", con);
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


    protected void btnreport1_Click(object sender, EventArgs e)
    {

        string sql = "";

        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"] );
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        //string query = "ALTER VIEW [dbo].[VW_SalesummaryReport_NP] AS (SELECT     COUNT(RecDate) AS TotInv,dbo.FUN_GetInvoicenumber(Branchid,0,RecDate)as InvoiceNo, " +
        //           " CONVERT(char(12), RecDate, 105) AS BillDate,sum( NetPayment)as amount,  sum(CAST(Discount AS float))as Discount, " +
        //           " sum(NetPayment) - (sum(CAST(Discount AS float))) AS Taxable,Round( sum(TaxAmount),2) AS Tax, " +
        //           " Round( sum(NetPayment) - (sum(CAST(Discount AS float))) + sum(TaxAmount),0) AS Net ,' 'as InvType  from Cshmst " +
        //        "   where  IsActive=1 and  (CAST(CAST(YEAR( Cshmst.RecDate) AS varchar(4)) + '/' + CAST(MONTH( Cshmst.RecDate) AS varchar(2)) + '/' + CAST(DAY( Cshmst.RecDate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') and Cshmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + "   GROUP BY RecDate,Branchid ";


        string query = "ALTER VIEW [dbo].[VW_SalesummaryReport_NP] AS (SELECT COUNT(Phrecdate) AS TotInv,dbo.FUN_GetInvoicenumber(Branchid,0,Phrecdate)as InvoiceNo, "+
               " CONVERT(char(12), Phrecdate, 105) AS BillDate,round(sum(Testcharges),0) as amount, "+
               " sum(CAST(Discount AS float))as Discount, round(sum(Testcharges)-sum(CAST(Discount AS float)),2) as Taxable ,Round( sum(TaxAmount),2) AS Tax, "+
               " round(sum(Testcharges)-sum(CAST(Discount AS float))+sum(TaxAmount),0) as Net,' 'as InvType  from VW_csmst1vw " +
               "   where  IsActive=1 and PatRegID<>'' and  (CAST(CAST(YEAR( Phrecdate) AS varchar(4)) + '/' + CAST(MONTH( Phrecdate) AS varchar(2)) + '/' + CAST(DAY( Phrecdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') and branchid=" + Convert.ToInt32(Session["Branchid"]) + "   GROUP BY Phrecdate,branchid ";
   
        // //" Union all "+

         // "  select  COUNT(BillDate) AS TotInv, dbo.FUN_GetInvoice_cancelnumber(Branchid,BillDate)as InvoiceNo, CONVERT(char(12), BillDate, 105) AS BillDate,sum(BillAmt) as amount,SUM(DisAmt)as Discount, "+
         // "  sum(BillAmt) - (sum(CAST(DisAmt AS float))) AS Taxable,Round( sum(TaxAmount),2) AS Tax, "+
         // "  Round( sum(BillAmt) - (sum(CAST(DisAmt AS float))) + sum(TaxAmount),0) AS Net ,'Sale Cancel'as InvType "+
         // "  from SaleCancelDetails "+
         // "  where (CAST(CAST(YEAR( SaleCancelDetails.BillDate) AS varchar(4)) + '/' + CAST(MONTH( SaleCancelDetails.BillDate) AS varchar(2)) + '/' + CAST(DAY( SaleCancelDetails.BillDate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')  " +
         // "  GROUP BY BillDate ,Branchid ";

        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_SaleSummaryRegister_NP.rpt");
        Session["reportname"] = "SaleSummaryRegister";
        Session["RPTFORMAT"] = "pdf";
        Session["Test"] = "Testing";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

    }
    protected void btnexcel_Click(object sender, EventArgs e)
    {
        string sql = "";      
        ExportGridToExcel();
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Materialize view" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Sale Summary Report";
            // HeaderCell.Font = System.Drawing.Color.Black;

            HeaderCell.ColumnSpan = 8;
            HeaderGridRow.Cells.Add(HeaderCell);
            GridView1.Controls[0].Controls.AddAt(0, HeaderGridRow);
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