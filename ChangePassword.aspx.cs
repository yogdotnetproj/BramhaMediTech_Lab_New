 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;

public partial class ChangePassword : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Userright_Bal_C ObjAT = new Userright_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {

        Page.SetFocus(txtuserType);
        if (!IsPostBack)
        {

            try
            {
                txtuserType.Text = Convert.ToString(Session["username"]);
                
               
                Label2.Visible = false;
              

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

  
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (Userright_Bal_C.isUserName_Exists(txtuserType.Text, Convert.ToInt32(Session["Branchid"])))
            {
                Label2.Text = "";
            }
            else
            {
                Label2.Visible = true;
                Label2.Text = "user name not exist.";
                txtuserType.Focus();
                return;
            }
            if (Userright_Bal_C.isUserNamePassword_Exists(txtuserType.Text, txtoldpassword.Text, Convert.ToInt32(Session["Branchid"])))
            {
                Label2.Text = "";
            }
            else
            {
                Label2.Visible = true;
                Label2.Text = "Old Password not match.";
                txtoldpassword.Focus();
                return;
            }
            ObjAT.Update_Password(txtuserType.Text, txtoldpassword.Text,txtnewpassword.Text);
            Label2.Visible = true;
            Label2.Text = "Password change successfully.";
            
            
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