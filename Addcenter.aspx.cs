using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using BAL;
using System.Data.SqlClient;
using System.Configuration;

public partial class Addcenter : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();    
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!Page.IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("Addcenter.aspx");
                    }
                }

                Label11.Visible = false;                
                if (Session["UnitCode"] != null)
                {
                    ddlLab.SelectedValue = Session["UnitCode"] .ToString().Trim();
                    ddlLab.Enabled = false;
                }

                if (Request.QueryString["drcd"] != null)
                {
                    txtcentercode.Text = Request.QueryString["drcd"].ToString();
                }
                if (Request.QueryString["tcode"] != null)
                {

                    try
                    {
                        
                        object drcodetemp = Request.QueryString["tcode"];
                        DrMT_Bal_C dr = new DrMT_Bal_C(drcodetemp, Convert.ToInt32(Session["Branchid"]));

                        txtcentername.Text = dr.Name;
                        txtcentercode.Text = dr.DoctorCode;
                        txtcenterEmail.Text = dr.Email.Trim();
                        string tel = dr.Phone;

                        txtcenterMobileno.Text = tel;                        

                        if (dr.ChkIsCenter == true)
                            ChkIsCenter.Checked = true;
                        else
                            ChkIsCenter.Checked = false;
                        if (dr.Cashbill == true)
                            Chkdirectcash.Checked = true;
                        else
                            Chkdirectcash.Checked = false;
                        txtcenterConper.Text = dr.Contact_person;
                       

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
                else if (Request.QueryString["DoctorCode"] != null)
                {
                    try
                    {

                        DrMT_Bal_C dr = new DrMT_Bal_C(Request.QueryString["DoctorCode"].ToString(), "CC", Convert.ToInt32(Session["Branchid"]));
                        txtcentercode.ReadOnly = true;
                        try
                        {
                            createuserTable_Bal_C au = new createuserTable_Bal_C(dr.DoctorCode, 1, Convert.ToInt32(Session["Branchid"]));
                            ViewState["uName"] = au.Username;
                           
                        }
                        catch
                        {
                            ViewState["uName"] = null;
                        }

                        txtcentername.Text = dr.Name;
                        ViewState["drcode"] = dr.DoctorCode;
                        txtcentercode.Text = dr.DoctorCode;
                        txtcentername.Text = dr.Name;
                        txtcenterEmail.Text = dr.Email;
                        string tel = dr.Phone;

                        txtcenterMobileno.Text = dr.Phone;

                       
                        if (dr.ChkIsCenter == true)
                            ChkIsCenter.Checked = true;
                        else
                            ChkIsCenter.Checked = false;

                        txtcenterConper.Text = dr.Contact_person;

                        if (dr.ratetypeid != 0)
                        {
                            ddlRateType.SelectedValue = dr.ratetypeid.ToString();
                        }

                        if (Session["usertype"].ToString() == "Administrator")
                        {
                            flagid.Visible = true;
                           
                            if (dr.Cashbill == true)
                            {
                                Chkdirectcash.Checked = true;
                            }
                            else
                            {
                                Chkdirectcash.Checked = false;
                            }
                        }

                    }
                    catch
                    {
                        txtcentercode.Text = Request.QueryString["DoctorCode"].ToString();
                    }
                }
                else
                {

                    if (Session["usertype"].ToString() == "Administrator")
                    {
                        flagid.Visible = true;                       
                    }
                }

                txtcentercode.Focus();
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


 

    protected void cmdSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (validation() == true)
            {
                DrMT_Bal_C dr = null;
                #region Edit At 'tcode'
                if (Request.QueryString["tcode"] != null)
                {
                    try
                    {
                        dr = new DrMT_Bal_C();
                        if (DrMT_sign_Bal_C.isDrCodeExists(txtcentercode.Text, Convert.ToInt32(Session["Branchid"])))
                        {
                            Label11.Visible = true;
                            Label11.Text = "Code already exist.";
                            txtcentercode.Focus();
                            return;
                        }

                        dr.DoctorCode = txtcentercode.Text.Trim();
                       // dr.Name = txtcentername.Text;
                        dr.Name = txtcentercode.Text.Trim();
                        dr.Email = txtcenterEmail.Text;
                        dr.Phone = txtcenterMobileno.Text;//"91" +                       
                        dr.City = "";

                        
                        dr.Address = txtcenteraddress.Text;
                     
                        if (ChkIsCenter.Checked == true)
                            dr.ChkIsCenter = true;
                        else
                            dr.ChkIsCenter = false;

                     
                       // dr.Contact_person = txtcenterConper.Text;
                        dr.Contact_person = txtcentercode.Text.Trim();

                        dr.DrType = "CC";
                       
                        if (ddlRateType.SelectedValue != "0")
                        {
                            dr.ratetypeid = Convert.ToInt32(ddlRateType.SelectedValue);
                        }
                        else
                        {
                            dr.ratetypeid = 0;
                        }
                       
                        if (Session["usertype"].ToString() == "Administrator")
                        {
                            flagid.Visible = true;
                            
                            if (Chkdirectcash.Checked == true)
                            {
                                dr.Cashbill = true;
                            }
                            else
                            {
                                dr.Cashbill = false;
                            }
                        }
                        else
                        {
                            dr.Cashbill = false;
                        }

                       
                        object drcdtemp = Request.QueryString["tcode"];
                      
                        dr.Update(drcdtemp, Convert.ToInt32(Session["Branchid"]));

                        #region addUserUpdate

                        createuserTable_Bal_C au = new createuserTable_Bal_C();
                        au.Username = "";
                        au.Password = "";
                        au.Usertype = "CollectionCenter";
                     
                        au.CenterCode = txtcentercode.Text;
                        if (ddlLab.SelectedValue != "0")
                        {
                            au.UnitCode = ddlLab.SelectedValue;
                        }                      
                      

                        #endregion
                        Label11.Visible = true;
                        Label11.Text = "Record save successfully.";                        

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
                #endregion
                #region Edit at 'code'
                else if (Request.QueryString["DoctorCode"] != null)
                {
                    try
                    {
                        dr = new DrMT_Bal_C();


                        dr.DoctorCode = txtcentercode.Text;
                       // dr.Name = txtcentername.Text;
                        dr.Name = txtcentercode.Text;
                        dr.Email = txtcenterEmail.Text;
                        dr.Phone = txtcenterMobileno.Text;//"91"
                        dr.Address = txtcenteraddress.Text;
                      
                        dr.City = "";
                    
                        if (ChkIsCenter.Checked == true)
                        {
                            dr.ChkIsCenter = true;
                           
                        }
                        else
                        { dr.ChkIsCenter = false; }

                       // dr.Contact_person = txtcenterConper.Text;
                        dr.Contact_person = txtcentercode.Text;

                        dr.DrType = "CC";

                       
                        if (ddlRateType.SelectedValue != "0")
                        {
                            dr.ratetypeid = Convert.ToInt32(ddlRateType.SelectedValue);
                        }
                        else
                        {
                            dr.ratetypeid = 0;
                        }

                       
                        if (Session["usertype"].ToString() == "Administrator")
                        {
                            flagid.Visible = true;
                           
                            if (Chkdirectcash.Checked == true)
                            {
                                dr.Cashbill = true;
                            }
                            else
                            {
                                dr.Cashbill = false;
                            }
                        }
                        else
                        {
                            dr.Cashbill = false;
                        }
                   
                        dr.Update(Convert.ToInt32(Session["Branchid"]));

                        #region addUserUpdate

                        try
                        {
                            createuserTable_Bal_C au = new createuserTable_Bal_C(txtcentercode.Text.Trim(), 1, Convert.ToInt32(Session["Branchid"]));
                            au.Username = "";
                            au.Password = "";
                            au.Usertype = "CollectionCenter";                        
                            au.CenterCode = txtcentercode.Text;
                            if (ddlLab.SelectedValue != "0")
                            {
                                au.UnitCode = ddlLab.SelectedValue;
                            }
                            else
                            {
                                au.UnitCode = null;
                            }                           
                           
                        }
                        catch
                        {                          

                            createuserTable_Bal_C au = new createuserTable_Bal_C();
                            au.Username = "";
                            au.Password = "";
                            au.Usertype = "CollectionCenter";
                         
                            au.CenterCode = txtcentercode.Text;
                        
                            if (ddlLab.SelectedValue != "0")
                            {
                                au.UnitCode = ddlLab.SelectedValue;
                            }                          
                         
                        }
                        #endregion
                        Label11.Visible = true;
                        Label11.Text = "Record save successfully.";
                        
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
                #endregion
                #region Save
                else
                {
                    try
                    {

                        dr = new DrMT_Bal_C();
                        if (DrMT_sign_Bal_C.isDrCodeExists(txtcentercode.Text, Convert.ToInt32(Session["Branchid"])))
                        {
                            Label11.Visible = true;
                            Label11.Text = "Please enter center code.";
                            txtcentercode.Focus();
                            return;
                        }

                        dr.DoctorCode = txtcentercode.Text;
                        //dr.Name = txtcentername.Text;
                        dr.Name = txtcentercode.Text;
                        dr.Email = txtcenterEmail.Text;
                        dr.Phone = txtcenterMobileno.Text;//"91" + 

                        dr.City = "";
                        dr.Address = txtcenteraddress.Text;
                        if (ChkIsCenter.Checked == true)
                        {
                            dr.ChkIsCenter = true;
                           
                        }
                        else
                        {
                            dr.ChkIsCenter = false;
                        }
                        dr.Contact_person = txtcentercode.Text;
                       // dr.Contact_person = txtcenterConper.Text;
                        dr.DrType = "CC";

                        if (ddlRateType.SelectedValue != "0")
                        {
                            dr.ratetypeid = Convert.ToInt32( ddlRateType.SelectedValue);
                        }
                        else
                        {
                            dr.ratetypeid = 0;
                        }

                      
                        if (Session["usertype"].ToString() == "Administrator")
                        {
                            flagid.Visible = true;                            
                            if (Chkdirectcash.Checked == true)
                            {
                                dr.Cashbill = true;
                            }
                            else
                            {
                                dr.Cashbill = false;
                            }
                        }
                        else
                        {
                            dr.Cashbill = false;
                        }

                       
                        dr.P_username = Convert.ToString(Session["username"]);
                        if (dr.Insert(Convert.ToInt32(Session["Branchid"])))
                        {
                            #region addUserInsert

                            createuserTable_Bal_C au = new createuserTable_Bal_C();
                            au.Username = "";
                            au.Password = "";
                            au.Usertype = "CollectionCenter";
                          
                            au.CenterCode = txtcentercode.Text;
                            
                            if (ddlLab.SelectedValue != "0")
                            {
                                au.UnitCode = ddlLab.SelectedValue;
                            }
                            au.Username = Convert.ToString(Session["username"]);
                         
                          
                            Label11.Visible = true;
                            Label11.Text = "Record save successfully.";
                            #endregion
                        }
                        ViewState["drcode"] = txtcentercode.Text;
                        ViewState["clapstatus"] = "";
                    }
                    catch
                    {
                    }

                    Session["msg"] = "You have successfully registered " + Convert.ToString( Session["CenterName"]);
                    Label11.Visible = true;

                    Label11.Text = "You have successfully registered " + Convert.ToString(Session["CenterName"]);
                }
                #endregion
                txtcenteraddress.Text = "";
                txtcenterLocation.Text = "";
                txtcentercode.Text = "";
                txtcenterConper.Text = "";
                txtcenterEmail.Text = "";
                txtcentername.Text = "";
                txtcenterMobileno.Text = "";
                txtDepAmt.Text = "";
               
                txtcenterPhoneno.Text = "";
                txtcenterPincode.Text = "";
                //Label11.Text = "";

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
    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }
    protected void ddlLab_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtcenterMobileno_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtcenterConper_TextChanged(object sender, EventArgs e)
    {

    }

    protected void Btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("Create_Center.aspx");
    }

    public bool validation()
    {
        if (txtcentercode.Text == "")
        {
            Label11.Visible = true;
            txtcentercode.Focus();
            txtcentercode.BackColor = Color.Red;
            Label11.Text = "Enter coll code";
            return false;
        }
        else
        {
            txtcentercode.BackColor = Color.White;
        }
        //if (txtcentername.Text == "")
        //{
        //    txtcentername.Focus();
        //    Label11.Visible = true;
        //    txtcentername.BackColor = Color.Red;
        //    Label11.Text = "Enter name";
        //    return false;
        //}
        //else
        //{
        //    txtcentername.BackColor = Color.White;
        //}
        if (txtcenterMobileno.Text == "")
        {
            Label11.Visible = true;
            txtcenterMobileno.Focus();
            txtcenterMobileno.BackColor = Color.Red;
            Label11.Text = "Enter mobile no";
            return false;
        }
        else
        {
            txtcenterMobileno.BackColor = Color.White;
        }
        if (ddlRateType.SelectedItem.Text == "Select RateType")
        {
            Label11.Visible = true;
            Label11.Text = "Select RateType";
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