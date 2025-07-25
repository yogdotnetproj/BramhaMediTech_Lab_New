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
using System.Data.SqlClient;
using System.Configuration;

public partial class Create_Center : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {        

        if (!Page.IsPostBack)
        {
            if (Convert.ToString(Session["HMS"]) != "Yes")
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("Create_Center.aspx");
                }
            }

            BindCenter();

        }
    }
 
    public void BindCenter()
    {
        try
        {

            if (hdnSort.Value != null)
            {
                GVCollcenter.DataSource = DrMT_sign_Bal_C.GetAll_Centers(hdnSort.Value, "", "", "", Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            }
            else
            {
                GVCollcenter.DataSource = DrMT_sign_Bal_C.GetAll_Centers("", "", "", "", Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            }
            GVCollcenter.DataBind();
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
    protected void GVCollcenter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;

    }
    protected void GVCollcenter_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            hdnSort.Value = e.SortExpression;
            BindCenter();
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
        try
        {
            if (hdnSort.Value != null)
            {
                GVCollcenter.DataSource = DrMT_sign_Bal_C.GetAll_Centers(hdnSort.Value, "", txtName.Text.Trim(), "", Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            }
            else
            {
                GVCollcenter.DataSource = DrMT_sign_Bal_C.GetAll_Centers("", "", txtName.Text.Trim(), "", Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            }
            GVCollcenter.DataBind();
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
    protected void GVCollcenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVCollcenter.PageIndex = e.NewPageIndex;
            if (hdnSort.Value != null)
            {
                GVCollcenter.DataSource = DrMT_sign_Bal_C.GetAll_Centers(hdnSort.Value, "", txtName.Text.Trim(), "", Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            }
            else
            {
                GVCollcenter.DataSource = DrMT_sign_Bal_C.GetAll_Centers("", "", txtName.Text.Trim(), "", Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            }
            GVCollcenter.DataBind();
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
        string selfor = "";
        ReportParameterClass.ReportType = "DoctorList";
        selfor += "{DrMT.DrType}='CC' and {DrMT.DoctorCode}<>''";
        if (Session["UnitCode"] != null)
        {
            selfor += " and {DrMT.UnitCode}=" + Session["UnitCode"] .ToString() + "";
        }

        if (txtName.Text != "")
        {
            selfor += " and {DrMT.DoctorName}='" + txtName.Text + "'";
        }

        Session.Add("rptsql", selfor);
        Session["rptname"] = Server.MapPath("~//DiagnosticReport//DoctorList.rpt");
        Session["reportname"] = "DoctorList";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = selfor;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
        
    }
    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("Addcenter.aspx");
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