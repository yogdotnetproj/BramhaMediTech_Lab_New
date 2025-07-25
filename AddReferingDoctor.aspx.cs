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

public partial class AddReferingDoctor : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();  
    string CenterCode = "";
    dbconnection dc = new dbconnection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Page.SetFocus(txtdoctorcode);
        lblnote.Visible = false;  

        if (!Page.IsPostBack)
        {
            if (Convert.ToString(Session["usertype"]) != "Administrator")
            {
               // checkexistpageright("AddReferingDoctor.aspx");
            }
            

            FillCenter();
            FillCatagory();

            cmbInitial.DataSource = PatientinitialLogic_Bal_C.getInitial();
            cmbInitial.DataTextField = "prefixName";
            cmbInitial.DataBind();
            cmbInitial.Items.Insert(0, new ListItem("Select Initial", "0"));
            if (Session["usertype"] != null && Session["username"] != null)
            {
                createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                Label2.Text = ui.CenterCode;
            }
            if (Session["usertype"] != null && Session["username"] != null)
            {
                if (Session["usertype"].ToString() == "CollectionCenter")
                {
                    ddlCenter.SelectedValue = Label2.Text;
                    ddlCenter.Enabled = false;
                }
            }
            if (Request.QueryString["DoctorCode"] != null)
            {
                try
                {
                    #region Bind Details


                    DrMT_Bal_C dr = new DrMT_Bal_C(Request.QueryString["DoctorCode"].ToString(), "DR", Convert.ToInt32(Session["Branchid"]));
                    txtdoctorcode.ReadOnly = true;
                    ViewState["drName"] = dr.Name;
                    ViewState["SaveEdit"] = "Edit";
                    btnsave.ToolTip = "Update";
                    txtdoctorcode.Text = dr.DoctorCode;
                    txtdoctorcode.Enabled = false;
                    cmbInitial.SelectedValue = dr.Prefix;
                    txtdoctorname.Text = dr.Name;
                    TxtEmail.Text = dr.Email;
                    txtdoctorphoneno.Text = dr.Phone;
                    txtAddress.Text = dr.Address;
                    txtCity.Text = dr.City;
                    ddlCenter.SelectedValue = dr.Contact_person;
                    ddlPRO.SelectedValue = dr.PRO.ToString();
                

                    #region Get Login Details For Doctor
                    try
                    {
                        createuserTable_Bal_C au = new createuserTable_Bal_C(dr.DoctorCode, 1, Convert.ToInt32(Session["Branchid"]));
                        ViewState["uName"] = au.Username;

                    }
                    catch
                    {
                        ViewState["uName"] = null;
                    }
                    #endregion

                    ddldrrefratetype.SelectedValue = dr.ratetypeid.ToString();
                   
                    #endregion
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
            else
            {
                ViewState["SaveEdit"] = "Save";

            }

            txtdoctorcode.Focus();
        }
    }
  
    private void FillCenter()
    {
        try
        {
            #region Bind Collection Center

            ddlCenter.DataSource = DrMT_sign_Bal_C.Get_CenterDetails(Session["UnitCode"] , Convert.ToInt32(Session["Branchid"]));
            ddlCenter.DataTextField = "Name";
            ddlCenter.DataValueField = "DoctorCode";
            ddlCenter.DataBind();         
            ddlCenter.SelectedIndex = -1;
            ddlCenter.Items.Insert(0, new ListItem(" ", ""));

            ddlPRO.DataSource = DrMT_sign_Bal_C.GetPro(Convert.ToInt32(Session["Branchid"]));
            ddlPRO.DataTextField = "Name";
            ddlPRO.DataValueField = "DoctorCode";
            ddlPRO.DataBind();
            ddlPRO.Items.Insert(0, new ListItem("Select Pro", "0"));
           // ddlPRO.SelectedIndex = -1;

            

            #endregion
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

    private void FillCatagory()
    {
        try
        {
            ddldrrefratetype.DataSource = DrMT_sign_Bal_C.getSelectrateCompliment(Convert.ToInt32(Session["Branchid"]), 'R');
            ddldrrefratetype.DataTextField = "RateName";
            ddldrrefratetype.DataValueField = "RatID";
            ddldrrefratetype.DataBind();
            ddldrrefratetype.Items.Insert(0, "Select Rate Type");
            ddldrrefratetype.SelectedIndex = -1;
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

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (validation() == true)
            {
                if (ViewState["SaveEdit"].ToString() == "Save")
                {
                    if (DrMT_sign_Bal_C.isDrCodeExists(txtdoctorcode.Text.Trim(), Convert.ToInt32(Session["Branchid"])))
                    {
                        lblCodeError.Text = "Code already exist.";
                        txtdoctorcode.Focus();
                        return;
                    }
                    else
                    {
                        lblCodeError.Visible = false;
                    }

                    if (ddlCenter.SelectedIndex != -1)
                    {
                        DrMT_Bal_C dr1 = new DrMT_Bal_C();

                        dr1.DoctorCode = Convert.ToString(txtdoctorcode.Text.Trim());
                        if (cmbInitial.SelectedIndex == 0)
                        {
                            dr1.Prefix = "";
                        }
                        else
                        {
                            dr1.Prefix = cmbInitial.SelectedItem.Text;
                        }
                        dr1.Contact_person = Convert.ToString(ddlCenter.SelectedValue);
                        dr1.Name = Convert.ToString(txtdoctorname.Text);
                        dr1.Email = Convert.ToString(TxtEmail.Text);
                        dr1.Phone = Convert.ToString(txtdoctorphoneno.Text);

                        dr1.Address = txtAddress.Text.ToString();
                        dr1.City = txtCity.Text.ToString();

                        dr1.DrType = "DR";
                        if (ddldrrefratetype.SelectedIndex != 0)
                            dr1.ratetypeid = Convert.ToInt32(ddldrrefratetype.SelectedValue);

                        if (ddlPRO.SelectedIndex != 0)
                        {
                            dr1.PRO = Convert.ToInt32(ddlPRO.SelectedValue);
                        }
                        else
                        {
                            dr1.PRO = 0;
                        }

                        dr1.P_username = Convert.ToString(Session["username"]);
                        dr1.Insert(Convert.ToInt32(Session["Branchid"]));

                        #region addUserUpdate

                        createuserTable_Bal_C au = new createuserTable_Bal_C();
                        au.Username = "";
                        au.Password = "";

                        au.Usertype = "Reference Doctor";
                       // au.Email = TxtEmail.Text;
                        au.CenterCode = txtdoctorcode.Text;
                        au.Username = Convert.ToString(Session["username"]);

                       // au.Insert(Convert.ToInt32(Session["Branchid"]));

                        #endregion

                        Session["msg"] = "Reference Doctor save successfully.";


                        Response.Redirect("Referingdoctor.aspx");

                    }
                    else
                    {

                    }
                }
                if (ViewState["SaveEdit"].ToString() == "Edit")
                {
                    DrMT_Bal_C dr1 = new DrMT_Bal_C();
                    dr1.DoctorCode = txtdoctorcode.Text.Trim();
                    if (cmbInitial.SelectedIndex == 0)
                    {
                        dr1.Prefix = "";
                    }
                    else
                    {
                        dr1.Prefix = cmbInitial.SelectedItem.Text;
                    }
                    dr1.Name = txtdoctorname.Text;
                    dr1.Email = TxtEmail.Text;
                    dr1.Phone = txtdoctorphoneno.Text;
                    dr1.Address = txtAddress.Text;
                    dr1.City = txtCity.Text;
                    dr1.Contact_person = Convert.ToString(ddlCenter.SelectedValue);
                   
                    dr1.DrType = "DR";
                    if (ddldrrefratetype.SelectedIndex != 0)
                        dr1.ratetypeid = Convert.ToInt32(ddldrrefratetype.SelectedValue);

                    if (ddlPRO.SelectedIndex != 0)
                    {
                        dr1.PRO = Convert.ToInt32(ddlPRO.SelectedValue);
                    }
                    else
                    {
                        dr1.PRO = 0;
                    }
                    
                    dr1.Update(Convert.ToInt32(Session["Branchid"]));
                    lblnote.Visible = true;
                    lblnote.Text = "Record update successfully.";

                    #region addUserUpdate
                    try
                    {
                        string LoginUser = Convert.ToString(Session["username"]);

                        createuserTable_Bal_C aut = new createuserTable_Bal_C();
                        if (aut.DoctorRecordExistChk(txtdoctorcode.Text.Trim(), 1, Convert.ToInt32(Session["Branchid"])))
                        {
                            aut.Username = "";
                            aut.Password = "";
                            aut.Usertype = "Reference Doctor";
                          
                            aut.CenterCode = txtdoctorcode.Text;
                            aut.Username = LoginUser;

                            //aut.AFlag = true;
                           // aut.Update(txtdoctorcode.Text, Convert.ToInt32(Session["Branchid"]));
                        }
                        else
                        {

                            createuserTable_Bal_C au = new createuserTable_Bal_C();
                            au.Username = "";
                            au.Password = "";
                            au.Usertype = "Reference Doctor";
                          
                            au.CenterCode = txtdoctorcode.Text;
                        
                            au.Username = LoginUser;

                         
                           // au.Insert(Convert.ToInt32(Session["Branchid"]));
                            Response.Redirect("Referingdoctor.aspx");
                        }
                    }
                    catch
                    {

                    }
                    #endregion

                    Session["msg"] = "Record Updated successfully.";
                   
                    txtdoctorcode.Focus();
                }
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

    protected void ddlLab_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public bool validation()
    {
        if (txtdoctorcode.Text == "")
        {
            lblnote.Visible = true;
            txtdoctorcode.Focus();
            lblnote.Text = "Enter doctor code";
            return false;
        }
        if (txtdoctorname.Text == "")
        {
            txtdoctorname.Focus();
            lblnote.Visible = true;
            lblnote.Text = "Enter doctor name";
            return false;
        }
        if (txtdoctorphoneno.Text == "")
        {
            lblnote.Visible = true;
            txtdoctorphoneno.Focus();
            lblnote.Text = "Enter contact no";
            return false;
        }


        return true;
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