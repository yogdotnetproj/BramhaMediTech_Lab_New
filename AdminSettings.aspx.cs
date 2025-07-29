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
using System.Configuration;

public partial class AdminSettings :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    AdminSettings_C ObjAs = new AdminSettings_C();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        lblnote.Visible = false;

        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("AdminSettings.aspx");
                    }
                }

               
                BindAdminsettings();
               
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
  

    public void BindAdminsettings()
    {
        dt = ObjAs.Get_PhlebotomistReq();
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["ISRequired"]) == true)
            {
                isPhlebotomistReq.SelectedValue = "Yes";
            }
            else
            {
                isPhlebotomistReq.SelectedValue = "No";
            }
        }
        dt = new DataTable();
        dt = ObjAs.Get_RegisterNoBarcodeInterface();
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dt.Rows[0]["IsRegNo"]) == true)
            {
                RblBarReg.SelectedValue = "RegNo";
            }
            else
            {
                RblBarReg.SelectedValue = "BarCode";
            }
        }
        dt=new DataTable ();
        dt = ObjAs.Get_LabDetails();
        if (dt.Rows.Count > 0)
        {
            txtLabEmailID.Text = Convert.ToString( dt.Rows[0]["LabEmailID"]);
            txtLabEmailPassword.Text = Convert.ToString(dt.Rows[0]["LabEmailPassword"]);
            txtLabEmailDisplayName.Text = Convert.ToString(dt.Rows[0]["LabEmailDisplayName"]);
            txtLabSmsString.Text = Convert.ToString(dt.Rows[0]["LabSmsString"]);
            txtLabSmsName.Text = Convert.ToString(dt.Rows[0]["LabSmsName"]);
            txtLabWebsite.Text = Convert.ToString(dt.Rows[0]["LabWebsite"]);
            txtPort.Text = Convert.ToString(dt.Rows[0]["Port"]);
        }
        

    }

    protected void isPhlebotomistReq_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isPhlebotomistReq.SelectedValue == "Yes")
        {
            ObjAs.Update_PhlebotomistReq();
        }
        else
        {
            ObjAs.Update_PhlebotomistNotReq();
        }
        BindAdminsettings();

    }
    protected void RblBarReg_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (isPhlebotomistReq.SelectedValue == "RegNo")
        {
            ObjAs.Update_IsInterfaceRegNo();
        }
        else
        {
            ObjAs.Update_IsInterfaceBarcode();
        }
        BindAdminsettings();

    }
    protected void IsReceiptMail_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtLabEmailID.Text != "")
        {
            ObjAs.InsertLabDetails(txtLabEmailID.Text, txtLabEmailPassword.Text, txtLabEmailDisplayName.Text, txtLabSmsString.Text, txtLabSmsName.Text, txtLabWebsite.Text, Convert.ToInt32(txtPort.Text));
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