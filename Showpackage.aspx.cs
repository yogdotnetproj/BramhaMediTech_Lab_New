using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Showpackage :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Convert.ToString(Session["usertype"]) != "Administrator")
            {
               // checkexistpageright("Showpackage.aspx");
            }
            try
            {                
                bindgrid();
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
 
    private void bindgrid()
    {
        try
        {
            GV_ShowPackage.DataSource = Packagenew_Bal_C.getPackGroups(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["DigModule"]));
            GV_ShowPackage.DataBind();
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
    protected void GV_ShowPackage_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string gcode = GV_ShowPackage.Rows[e.NewEditIndex].Cells[1].Text;
            Response.Redirect("CreatePackage.aspx?PackageCode=" + gcode);
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
    protected void GV_ShowPackage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowIndex == -1)
                return;

            string groupcd = e.Row.Cells[1].Text.Trim();
            string MT_Code = "", testNames = "";

            int i = 0;
            IEnumerator ie = PackageL_Bal_C.Get_PackageDetails(groupcd, Convert.ToInt32(Session["Branchid"])).GetEnumerator();
            while (ie.MoveNext())
            {
                i++;

                if (MT_Code == "")
                {
                    MT_Code = (ie.Current as Package_Bal_C).MTCode;
                    testNames = (ie.Current as Package_Bal_C).TestName;
                }
                else
                {
                    MT_Code = MT_Code + "," + (ie.Current as Package_Bal_C).MTCode;
                    testNames = testNames + "," + (ie.Current as Package_Bal_C).TestName;
                }
                if (i % 3 == 0)
                {
                    MT_Code = MT_Code + "<br>";
                    //testNames = testNames + "<br>";
                }

            }

            (e.Row.FindControl("lbltestNames") as Label).Text = testNames;
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
    protected void GV_ShowPackage_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            PackageName_Bal_C group1 = new PackageName_Bal_C();
            group1.PackageCode = GV_ShowPackage.Rows[e.RowIndex].Cells[1].Text;
            group1.Delete(Convert.ToInt32(Session["Branchid"]));
            group1.Delete_Details(Convert.ToInt32(Session["Branchid"]));
            bindgrid();
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
    protected void GV_ShowPackage_PageIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GV_ShowPackage_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GV_ShowPackage.PageIndex = e.NewPageIndex;
            bindgrid();
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


    protected void btnaddnew_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreatePackage.aspx");
    }
    protected void Btn_Add_Dept_Click(object sender, EventArgs e)
    {
        Response.Redirect("SubDeptAdd.aspx", false);
    }
    protected void Btn_Add_Test_Click(object sender, EventArgs e)
    {

        // Response.Redirect("ShowTest.aspx", false);
        Response.Redirect("~/AddTest.aspx", false);
    }
    protected void Btn_Add_NR_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReferanceRange.aspx", false);
    }
    protected void Btn_Add_PK_Click(object sender, EventArgs e)
    {
        Response.Redirect("Showpackage.aspx", false);
    }
    protected void Btn_Add_Sample_Click(object sender, EventArgs e)
    {
        Response.Redirect("SampleType.aspx", false);

    }
    protected void Btn_Add_ShortCut_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShortCut.aspx", false);
    }
    protected void Btn_Add_Formula_Click(object sender, EventArgs e)
    {
        Response.Redirect("TestFormulasetting.aspx", false);
    }
    protected void Btn_Add_RN_Click(object sender, EventArgs e)
    {
        Response.Redirect("ReportNote.aspx", false);
    }
    protected void btnedittest_Click(object sender, EventArgs e)
    {
        Response.Redirect("ShowTest.aspx", false);
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