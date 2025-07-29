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
using System.Drawing;

public partial class Outsourcepatient :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    Patmst_New_Bal_C PNBC = new Patmst_New_Bal_C();
    string CenterCode = "", patientName = "", Reg_no = "", labcode_main = "", Barcode = "";
    object fromDate = null, toDate = null;
    DataTable dt = new DataTable();

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
                        checkexistpageright("Outsourcepatient.aspx");
                    }
                }
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");               
                FillGrid();
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

    public void FillGrid()
    {
        try
        {
            string OutsourceLab = "";
            string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
            if (labcode != null && labcode != "")
            {
                labcode_main = labcode;
            }
            if (txtCenter.Text.Trim() != "")
            {
                CenterCode = Session["CenterCode"].ToString().Trim();
            }
            if (fromdate.Text.Trim() != "" && todate.Text.Trim() != "")
            {
                fromDate = fromdate.Text.Trim();
                toDate = todate.Text.Trim();
            }
            if (txtregno.Text.Trim() != "")
            {
                Reg_no = txtregno.Text.Trim();
            }
            if (txtPatientName.Text.Trim() != "")
            {
                patientName = txtPatientName.Text.Trim();
            }
            if (txtOutsourceLab.Text.Trim() != "")
            {
                OutsourceLab = txtOutsourceLab.Text.Trim();
            }
            
                Barcode = "";


                GV_Outcourcepat.DataSource = PNBC.Getoutsourcepatient(CenterCode, fromDate, toDate, patientName, Reg_no, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Session["DigModule"]), "", 0, Session["UserName"].ToString(), Session["UserType"].ToString(), Barcode, txtmobileno.Text, OutsourceLab);

            GV_Outcourcepat.DataBind();


        }
        catch (Exception exx)
        {

        }
        //e.Cancel = true;
    }

    protected void GV_Outcourcepat_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GV_Outcourcepat.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void txtCenter_TextChanged(object sender, EventArgs e)
    {
        string CCode = DrMT_sign_Bal_C.Get_C_Code(txtCenter.Text.Trim(), Convert.ToInt32(Session["Branchid"]));
        Session["CenterCode"] = CCode;
        CenterCode = Session["CenterCode"].ToString().Trim();
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