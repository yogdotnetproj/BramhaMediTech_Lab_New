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
using System.Web.Management;

using System.Web.Services;
using System.Web.Script.Services;
public partial class Saleregisterreport :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string labcode_main = "";
    string CenterName = "", regno = "",  DocCode = "";
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
    private void Binddropdown()
    {
        try
        {

            if (Session["usertype"].ToString() == "Admin" || Session["usertype"].ToString().ToLower() == "administrator")
            {
                ddluser.DataSource = createuserlogic_Bal_C.getAllUsers(Convert.ToInt32(Session["Branchid"]));
                ddluser.DataTextField = "username";
                ddluser.DataValueField = "username";
                ddluser.DataBind();
                ddluser.Items.Insert(0, "Select UserName");
                ddluser.SelectedIndex = -1;
            }
            else
            {
                ddluser.DataSource = createuserlogic_Bal_C.getAllUsers_username(Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["username"]));
                ddluser.DataTextField = "username";
                ddluser.DataValueField = "username";
                ddluser.DataBind();
                ddluser.Items.Insert(0, "Select UserName");
                ddluser.SelectedIndex = 1;
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
            string sortValue = "", Centercode = "";
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
                Centercode = Session["CenterCode"].ToString();
            }

            string username = "";
            if (ddluser.SelectedItem.Text != "Select UserName")
            {
                username = ddluser.SelectedItem.Text;
            }
            if (txtCenter.Text != "" && txtCenter.Text != "All")
            {
                CenterName = txtCenter.Text;
            }
            if (txtregno.Text != "")
            {
                regno = txtregno.Text;
            }
           
            if (Convert.ToString(ViewState["DocCode"]) != "")
            {
                DocCode = Convert.ToString(ViewState["DocCode"]);
            }
            GridView1.DataSource = Cshmst_supp_Bal_C.getLoginfoMainData_saleregister(todate1, fromDate1, username, Centercode, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]), this.labcode_main,regno,DocCode,CenterName );
            GridView1.DataBind();
            
            float sum = 0.0f, Tax = 0.0f, Dis = 0.0f, Bam = 0.0f;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                sum += Convert.ToSingle(GridView1.Rows[i].Cells[9].Text);
                Tax += Convert.ToSingle(GridView1.Rows[i].Cells[8].Text);
                Dis += Convert.ToSingle(GridView1.Rows[i].Cells[7].Text);
                Bam += Convert.ToSingle(GridView1.Rows[i].Cells[6].Text);
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


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string username = GridView1.Rows[e.NewEditIndex].Cells[10].Text;
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

        string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"] );
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[VW_salerpirvw] AS (SELECT convert(char(12),Cshmst.RecDate,105) as BillDate,Cshmst.NetPayment,Cshmst.username ,  patmst.PatRegID, patmst.Patphoneno, Cshmst.BillNo,"+
                   " Cshmst.AmtReceived, Cshmst.Paymenttype,  Cshmst.Discount,  patmst.intial+' '+Cshmst.Patname as Patientname, Cshmst.AmtPaid, Cshmst.Balance , "+
                   " Cshmst.TaxPer,Cshmst.TaxAmount , Cshmst.PID,  "+
                   " case when Cshmst.AmtPaid >0  then ((Cshmst.NetPayment -Cshmst.Discount)+Cshmst.TaxAmount) else Cshmst.AmtPaid end as NetAmount , "+
                   " Cshmst.Billcancelno ,case when Cshmst.Billcancelno=0 then 'Sale Register Report' else 'Sale Return Register Report' end as SaleRegRep, "+
                   " Doctorcode,Drname,Cshmst.Centercode,Centername,CONVERT(varchar(15),CAST(patmst.RegistratonDateTime AS TIME),100)as BillTime "+
                   " FROM  Cshmst INNER JOIN patmst ON Cshmst.PatRegID = patmst.PatRegID AND  Cshmst.PID = patmst.PID   " +
                  "   where    (CAST(CAST(YEAR( Cshmst.RecDate) AS varchar(4)) + '/' + CAST(MONTH( Cshmst.RecDate) AS varchar(2)) + '/' + CAST(DAY( Cshmst.RecDate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "') and Cshmst.branchid=" + Convert.ToInt32(Session["Branchid"]) + " ";
              

        string username = "";
        if (ddluser.SelectedItem.Text != "Select UserName")
        {
            username = ddluser.SelectedItem.Text;
            query += " and Cshmst.username='" + username + "'";
        }
        if (txtCenter.Text != "" && txtCenter.Text != "All")
        {
            CenterName = txtCenter.Text;
           
        query += " and Center='" + username + "'";
        }
        if (txtregno.Text != "")
        {
            regno = txtregno.Text;
            query += " and patmst.PatRegID='" + regno + "'";
        }

        if (Convert.ToString(ViewState["DocCode"]) != "")
        {
            DocCode = Convert.ToString(ViewState["DocCode"]);
            query += " and Doctorcode='" + DocCode + "'";
        }

        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();


        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/saleregisterreport_IR.rpt");
        Session["reportname"] = "saleregisterreport_IR";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);       

    }
    [WebMethod]
    [ScriptMethod]
    public static string[] GetCenterName(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + prefixText + "%' and DrType='CC' and UnitCode='" + labcode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + prefixText + "%' and DrType='CC' and branchid=" + branchid + " order by DoctorName", con);
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
    [WebMethod]
    [ScriptMethod]
    public static string[] FillDoctor(string prefixText, int count)
    {

        SqlConnection con = DataAccess.ConInitForDC();

        string Centercode = HttpContext.Current.Session["CenterCode"].ToString();
        SqlDataAdapter sda = null;
        if (HttpContext.Current.Session["DigModule"] != null && HttpContext.Current.Session["DigModule"] != "0")
            sda = new SqlDataAdapter("SELECT DocCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR' and DoctorName like '" + prefixText + "%' and (DoctorCode='" + Centercode + "' or DoctorCode='')", con);
        else
            sda = new SqlDataAdapter("SELECT DocCode, rtrim(DrInitial)+' '+DoctorName as name from  DrMT where DrType='DR'and DoctorName like '" + prefixText + "%' and (DoctorCode='" + Centercode + "' or DoctorCode='')", con);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["name"] + " = " + dr["DocCode"], i);
            i++;
        }

        return tests;
    }
    protected void txtDoctorName_TextChanged(object sender, EventArgs e)
    {
        if (txtDoctorName.Text != "")
        {
            string[] Doc_code;
            Doc_code = txtDoctorName.Text.Split('=');
            if (Doc_code.Length > 1)
            {
                txtDoctorName.Text = Doc_code[0].ToString();
                ViewState["DocCode"] = Doc_code[1].ToString();
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