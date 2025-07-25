using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using BAL;

public partial class User_Master : System.Web.UI.Page
{
    ClsAddUser objuser = new ClsAddUser();
    UserAddBranch_Bal_C objbranch = new UserAddBranch_Bal_C();
    DataSet dss;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadbranch();
            loadgrid();
        }
    }
    private void loadgrid()
    {
        dss = new DataSet();
        dss = objuser.user(3);
        if (dss.Tables[0].Rows.Count != 0)
        {
            GVUserMaster.DataSource = dss.Tables[0];
            GVUserMaster.DataBind();
        }
        dss.Dispose();
    }
    private void loadbranch()
    {
        dss = objbranch.branch(2);
        txtbranch.DataSource = dss;
        txtbranch.DataTextField = "SZBRANCHNAME";
        txtbranch.DataValueField = "NUMBRANCHID";
        txtbranch.DataBind();
        dt = objuser.BindCountry();
        if (dt.Rows.Count >0)
        {
            ddlCountry.DataSource = dt;
            ddlCountry.DataTextField = "SZCOUNTRYNAME";
            ddlCountry.DataValueField = "NUMCOUNTRYID";
            ddlCountry.DataBind();
            ddlCountry.Items.Add(new ListItem("Select Country","-1"));
            ddlCountry.SelectedValue = "-1";
        }

    }

    public bool IsValid()
    {
        bool Isvalid = true;
        if (txtinitial.Text == "")
        {
            Isvalid = false;
            txtinitial.BackColor = Color.Red;
            LBLValidation.Text = "Please Enter Initial";
            txtinitial.Focus();
            LbMsg.Visible = true;
            return false;

        }
        if (txtempname.Text == "")
        {
            Isvalid = false;
            txtempname.BackColor = Color.Red;
            //MessageBox("Please Enter Employee Name");
            LBLValidation.Text = "Please Enter First Name";
            txtempname.Focus();
            LbMsg.Visible = true;
            return false;
        }
        
        if (txtlastname.Text == "")
        {
            Isvalid = false;
            txtmidname.BackColor = Color.Red;
            LBLValidation.Text = "Please Enter Last Name";
            txtlastname.Focus();
            LbMsg.Visible = true;
            return false;
        }
        
        if (txtusername.Text == "")
        {
            Isvalid = false;
            txtusername.BackColor = Color.Red;
            LBLValidation.Text = "Please Enter User Name";
            txtusername.Focus();
            LbMsg.Visible = true;
            return false;
        }
        if (txtpassword.Text == "")
        {
            Isvalid = false;
            txtpassword.BackColor = Color.Red;
            LBLValidation.Text = "Please Enter Password";
            txtpassword.Focus();
            LbMsg.Visible = true;
            return false;
        }
        if (txtconpass.Text == "")
        {
            Isvalid = false;
            txtconpass.BackColor = Color.Red;
            LBLValidation.Text = "Please Enter Confirm Password";
            txtpassword.Focus();
            LbMsg.Visible = true;
            return false;
        }
        if (txtpassword.Text != txtconpass.Text)
        {
            Isvalid = false;
            txtpassword.BackColor = Color.Red;
            txtconpass.BackColor = Color.Red;
         //   MessageBox("Password and Confirm Password Does Not Match");
            LBLValidation.Text = "Password and Confirm Password Does Not Match";
            txtpassword.Focus();
            LbMsg.Visible = true;
            return false;
        }
        if (txtdob.Text == "")
        {
            Isvalid = false;
            txtdob.BackColor = Color.Red;
           // MessageBox("Please Enter Date");
            LBLValidation.Text = "Please Enter Birth Date";
            txtdob.Focus();
            LbMsg.Visible = true;
            return false;
        }
       
        if (txtaddress.Text == "")
        {
            Isvalid = false;
            txtaddress.BackColor = Color.Red;
            MessageBox("Please Enter Address");
            return false;
        }
        //if (txtcountry.Text == "")
        //{
        //    Isvalid = false;
        //    txtcountry.BackColor = Color.Red;
        //    MessageBox("Please Enter Country Name");
        //    return false;
        //}
        //if (txtstate.Text == "")
        //{
        //    Isvalid = false;
        //    txtstate.BackColor = Color.Red;
        //    MessageBox("Please Enter State Name"); return false;

        //}
        if (txtcity.Text == "")
        {
            Isvalid = false;
            txtcity.BackColor = Color.Red;
            MessageBox("Please Enter City Name"); return false;

        }
        if (txtmobileno.Text == "")
        {
            Isvalid = false;
            txtmobileno.BackColor = Color.Red;
            MessageBox("Please Enter Mobile No");
            return false;
        }
        if (txttelno.Text == "")
        {
            Isvalid = false;
            txttelno.BackColor = Color.Red;
            MessageBox("Please Enter Phone No");
            return false;
        }
        if (txtemailid.Text == "")
        {
            Isvalid=false;
            txtemailid.BackColor=Color.Red;
            MessageBox("Please ENter Valid Email Id");
            return false;
        }
        if (txtzipcode.Text == "")
        {
            Isvalid = false;
            txtzipcode.BackColor = Color.Red;
            MessageBox("Please Enter Zip No");
            return false;
        }
        if (txtbranch.Text == "")
        {
            Isvalid = false;
            txtbranch.BackColor = Color.Red;
            MessageBox("Please Select Branch");
            return false;
        }
        return true;
    }

   

    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid())
            {
                tblCreate.Visible = false;
                tblShow.Visible = true;

                if (btnsave.Text == "Update")
                {

                    GridViewRow row = GVUserMaster.SelectedRow;
                    Label id = row.FindControl("lblempid") as Label;
                    objuser.getempid = id.Text;

                    objuser.getempname = txtempname.Text;
                    objuser.getempmidname = txtmidname.Text;
                    objuser.getemplastname = txtlastname.Text;
                    objuser.getinitial = txtinitial.Text;
                    objuser.getusername = txtusername.Text;
                    objuser.getpassword = txtpassword.Text;
                    objuser.getdob = txtdob.Text;
                    objuser.getgender = RblGender.SelectedValue;
                    objuser.getaddress = txtaddress.Text;
                    objuser.getcountry = ddlCountry.SelectedValue;
                    objuser.getstate = ddlState.SelectedValue;
                    objuser.getcity = txtcity.Text;
                    objuser.getmobileno = txtmobileno.Text;
                    objuser.gettelno = txttelno.Text;
                    objuser.getemailid = txtemailid.Text;
                    objuser.getzipcode = txtzipcode.Text;
                    objuser.getbranchid = txtbranch.Text;
                    objuser.getstatus = "Active";

                    objuser.user(4);
                    MessageBox("Update Data Successfully");
                    clear();
                    loadgrid();
                    tblCreate.Visible = false;
                    tblShow.Visible = true;
                }
                else
                {
                    objuser.getempname = txtempname.Text;
                    objuser.getempmidname = txtmidname.Text;
                    objuser.getemplastname = txtlastname.Text;
                    objuser.getinitial = txtinitial.Text;
                    objuser.getusername = txtusername.Text;
                    objuser.getpassword = txtpassword.Text;
                    objuser.getdob = txtdob.Text;
                    objuser.getgender = RblGender.SelectedValue;
                    objuser.getaddress = txtaddress.Text;
                    objuser.getcountry = ddlCountry.SelectedValue;
                    objuser.getstate = ddlState.SelectedValue;
                    objuser.getcity = txtcity.Text;
                    objuser.getmobileno = txtmobileno.Text;
                    objuser.gettelno = txttelno.Text;
                    objuser.getemailid = txtemailid.Text;
                    objuser.getzipcode = txtzipcode.Text;
                    objuser.getbranchid = txtbranch.Text;
                    objuser.getstatus = "Active";

                    objuser.user(1);
                    MessageBox("Save Data Successfully");
                    clear();
                    loadgrid();
                    tblCreate.Visible = false;
                    tblShow.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void MessageBox(string p)
    {
        Label lblMessageBox = new Label();

        lblMessageBox.Text =
            "<script language='javascript'>" + Environment.NewLine +
            "window.alert('" + p + "')</script>";

        Page.Controls.Add(lblMessageBox);
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        clear();
        btnsave.Text = "Save";
        tblCreate.Visible = true;
        tblShow.Visible = false;
    }
    protected void GVUserMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        tblCreate.Visible = true;
        tblShow.Visible = false;

        GridViewRow row = GVUserMaster.SelectedRow;
        txtempname.Text = (row.FindControl("lblempname") as Label).Text;
        txtmidname.Text = (row.FindControl("lblmidname") as Label).Text;
        txtlastname.Text = (row.FindControl("lbllastname") as Label).Text;
        txtinitial.Text = (row.FindControl("lblinitial") as Label).Text;
        txtusername.Text = (row.FindControl("lblusername") as Label).Text;
        txtpassword.Text = (row.FindControl("lblpassword") as Label).Text;
        txtdob.Text = (row.FindControl("lbldob") as Label).Text;
        RblGender.SelectedValue = (row.FindControl("lblgender") as Label).Text;
        txtaddress.Text = (row.FindControl("lbladd") as Label).Text;
        ddlCountry.SelectedValue = (row.FindControl("lblcountry") as Label).Text;
       // txtstate.Text = (row.FindControl("lblstate") as Label).Text;
        txtcity.Text = (row.FindControl("lblcity") as Label).Text;
        txtmobileno.Text = (row.FindControl("lblmobileno") as Label).Text;
        txttelno.Text = (row.FindControl("lbltelno") as Label).Text;
        txtemailid.Text = (row.FindControl("lblemailid") as Label).Text;
        txtzipcode.Text = (row.FindControl("lblzipcode") as Label).Text;
        //txtbranch.Text = (row.FindControl("lblbranchid") as Label).Text;
        
        btnsave.Text = "Update";
    }
    protected void GVUserMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVUserMaster.PageIndex = e.NewPageIndex;
        loadgrid();
    }
    protected void GVUserMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            objuser.getempid= Convert.ToInt32(GVUserMaster.DataKeys[e.RowIndex].Value.ToString()).ToString();
            objuser.user(5);
            loadgrid();
            MessageBox("Delete Successfully");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
       
        tblCreate.Visible = false;
        tblShow.Visible = true;
    }

    private void clear()
    {
        LBLValidation.Text = "";
        LblMsg.Text = "";
        txtempname.Text = "";
        txtmidname.Text = "";
        txtlastname.Text = "";
        txtinitial.Text = "";
        txtusername.Text = "";
        txtpassword.Text = "";
        txtdob.Text = "";
        txtaddress.Text = "";
        txtcity.Text = "";
        txtmobileno.Text = "";
        txttelno.Text = "";
        txtemailid.Text = "";
        txtzipcode.Text = "";
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedValue != "-1")
        {
            objuser.getCountryId = Convert.ToInt32( ddlCountry.SelectedValue);
            dt = objuser.BindStateWithCountry();
            if (dt.Rows.Count > 0)
            {
                ddlState.DataSource = dt;
                ddlState.DataTextField = "SZSTATENAME";
                ddlState.DataValueField = "NUMSTATEID";
                ddlState.DataBind();
                ddlState.Items.Add(new ListItem("Select State", "-1"));
                ddlState.SelectedValue = "-1";
            }
        }
    }
    protected void GVUserMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string dd = GVUserMaster.DataKeys[e.NewEditIndex]["SZEMPID"].ToString();
        tblCreate.Visible = false;
        tblShow.Visible = true;
        DataTable dt = new DataTable();
        objuser.getempid = dd;
        dt = objuser.EditUserDetails(6);

        if (dt.Rows.Count != 0)
        {
            txtempname.Text = dt.Rows[0]["SZEMPNAME"].ToString();
            txtmidname.Text = dt.Rows[0]["SZEMPMIDNAME"].ToString();
            txtlastname.Text = dt.Rows[0]["SZLASTNAME"].ToString();
            txtinitial.Text = dt.Rows[0]["SZINITIAL"].ToString();
            txtusername.Text = dt.Rows[0]["SZUSERNAME"].ToString();
            txtpassword.Text = dt.Rows[0]["SZPASSWORD"].ToString();
            txtconpass.Text = dt.Rows[0]["SZPASSWORD"].ToString();
            txtdob.Text = Convert.ToDateTime(dt.Rows[0]["DTPDOB"]).ToShortDateString();
            RblGender.SelectedValue = dt.Rows[0]["SZGENDER"].ToString();
            txtaddress.Text = dt.Rows[0]["SZADDRESS"].ToString();
            //ddlCountry.SelectedValue = dt.Rows[0][""].ToString();
            //ddlState.SelectedValue = dt.Rows[0][""].ToString();
            //txtcity.Text = dt.Rows[0][""].ToString();
            txtmobileno.Text = dt.Rows[0]["NUMMOBILENO"].ToString();
            txttelno.Text = dt.Rows[0]["NUMTELNO"].ToString();
            txtemailid.Text = dt.Rows[0]["SZEMAILID"].ToString();
            txtzipcode.Text = dt.Rows[0]["NUMZIPCODE"].ToString();
            btnsave.Text = "Update";
            tblCreate.Visible = true;
            tblShow.Visible = false;
        }
        e.Cancel = true;
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        tblCreate.Visible = false;
        tblShow.Visible = true;
        btnsave.Text = "Save";
        clear();
        
    }
}