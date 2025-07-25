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

public partial class Phlebotomist_PendingReport : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    string labcode_main = "";
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    createuserTable_Bal_C ObjCTB = new createuserTable_Bal_C();

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
                        checkexistpageright("Phlebotomist_PendingReport.aspx");
                    }
                }
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                Alterview();
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



    public void BindGrid()
    {
        dt = ObjCTB.Get_VW_Phlebotomist_Pending(Convert.ToInt32(Session["Branchid"]));
        if (dt.Rows.Count > 0)
        {
            GV_DueReport.DataSource = dt;
            GV_DueReport.DataBind();
        }
    }

    public void Alterview()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();
        SqlConnection con1 = DataAccess.ConInitForDC();
        SqlCommand cmd11 = con1.CreateCommand();
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


        string query1 = "ALTER VIEW [dbo].[VW_Phlebotomist_PendingReport_Temp] AS (SELECT DISTINCT " +
           " patmstd.PID, patmstd.UnitCode, MainTest.Maintestname, MainTest.Sampletype, SubDepartment.subdeptName, patmst.intial, patmst.Patname, patmst.RefDr, " +
          "  patmst.DoctorCode, patmst.CenterCode, patmst.DrName, patmst.CenterName, SubDepartment.ID, patmst.PatRegID, patmst.LabRegMediPro " +
          " ,DATEDIFF(MINUTE, patmst.Patregdate, GETDATE()) AS DiffTimeMin,  DATEDIFF(HOUR, patmst.Patregdate, GETDATE()) AS DiffTimeHour, patmstd.PackageCode,  patmstd.Patregdate " +
          "  FROM         patmstd INNER JOIN   MainTest ON patmstd.MTCode = MainTest.MTCode " +
          "  AND patmstd.SDCode = MainTest.SDCode INNER JOIN  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode " +
          "  LEFT OUTER JOIN  patmst ON patmstd.PatRegID = patmst.PatRegID AND patmstd.PID = patmst.PID   " +
          "  WHERE     (patmstd.Patauthicante <> 'Authorized') AND  patmstd.Patregdate between '" + Convert.ToDateTime(fromDate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(Todate).AddDays(+1).ToString("MM/dd/yyyy") + "' and SubDepartment.ID= " + RblType.SelectedValue + " ";

        cmd11.CommandText = query1 + ")";

        con1.Open();
        cmd11.ExecuteNonQuery();
        con1.Close(); con1.Dispose();
    }

    protected void btnList_Click1(object sender, EventArgs e)
    {
        Alterview();
        BindGrid();

    }
    protected void GV_DueReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_DueReport.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    protected void GV_DueReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GV_DueReport_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnreport_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = " Phlebotomist Pending report  " + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GV_DueReport.GridLines = GridLines.Both;
        GV_DueReport.HeaderStyle.Font.Bold = true;
        GV_DueReport.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the run time error "  
        //Control 'GridView1' of type 'Grid View' must be placed inside a form tag with runat=server."  
    }
    protected void GV_DueReport_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();
            HeaderCell.Text = "Phlebotomist userwise report ";
            // HeaderCell.Font = System.Drawing.Color.Black;

            HeaderCell.ColumnSpan = 9;
            HeaderGridRow.Cells.Add(HeaderCell);
            GV_DueReport.Controls[0].Controls.AddAt(0, HeaderGridRow);
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