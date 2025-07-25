using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using BAL;

public partial class UserManagement_UserType : System.Web.UI.Page
{
    ClsUserType objusty = new ClsUserType();
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadgrid();
        }
    }
    private void loadgrid()
    {
        tblShow.Visible = true;
        ds = new DataSet();
        ds = objusty.usertypeSP(3);
        if (ds.Tables[0].Rows.Count != 0)
        {
            GVUserType.DataSource = ds.Tables[0];
            GVUserType.DataBind();
        }
        ds.Dispose();
    }
   
    protected void GVUserType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            objusty.getuserid= Convert.ToInt32(GVUserType.DataKeys[e.RowIndex].Value.ToString()).ToString();
            objusty.usertypeSP(4);
            loadgrid();
            //MessageBox("Delete Successfully");
            LblMsg.Text = "Record Deleted Successfully";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GVUserType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string dd = GVUserType.DataKeys[e.NewEditIndex]["USERID"].ToString();
        ViewState["id"] = dd;
        tblCreate.Visible = true;
        tblShow.Visible = false;
        DataTable dt = new DataTable();
        objusty.getuserid= dd;
        dt = objusty.usertypetest(5);

        if (dt.Rows.Count != 0)
        {
            txtusertype.Text = dt.Rows[0]["USERTYPE"].ToString();
            txtdescription.Text = dt.Rows[0]["DESCRIPTION"].ToString();
            
            btnSave.Text = "Update";
        }
        e.Cancel = true;
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        tblCreate.Visible = true;
        tblShow.Visible = false;
        btnSave.Text = "Save";
        cancel();
        LblMsg.Text = "";
        LBLValidation.Text = "";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (IsValid())
        {
            try
            {
                tblCreate.Visible = false;
                tblShow.Visible = true;
                
                if (btnSave.Text == "Update")
                {
                    GridViewRow row = GVUserType.SelectedRow;
                    //GridViewRow row1 = GVUserType.Rows[0];
                    //Label id = row1.FindControl("") as Label;

                    objusty.getuserid= Convert.ToString(ViewState["id"]);
                    objusty.getusertype= txtusertype.Text;
                    objusty.getdescription= txtdescription.Text;

                    objusty.usertypeSP(2);
                    LblMsg.Text = "Data Updated Successfully";
                    loadgrid();
                    cancel();
                    tblCreate.Visible = false;
                    tblShow.Visible = true;
                }
                else
                {

                    objusty.getusertype = txtusertype.Text;
                    objusty.getdescription = txtdescription.Text;

                    objusty.usertypeSP(1);
                    LblMsg.Text = "Data Insert Successfully";
                    loadgrid();
                    cancel();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    private bool IsValid()
    {
        bool isvalid = true;
        if (txtusertype.Text == "")
        {
            LblMsg.Visible = true;
            isvalid = false;
            txtusertype.BackColor = Color.Red;
            txtusertype.Focus();
            LbMsg.Visible = true;
            LBLValidation.Text = "Please Enter User Type";
            LBLValidation.ForeColor = Color.Red;
            return false;
        }
        else
        {
            txtusertype.BackColor = Color.White;
        }
        
        return true;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        cancel();
    }

    private void cancel()
    {
        txtusertype.Text = "";
        txtdescription.Text = "";
        btnSave.Text = "Save";
        LblMsg.Text = "";
        LBLValidation.Text = "";
    }
    protected void GVUserType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVUserType.PageIndex = e.NewPageIndex;
        loadgrid();
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        tblCreate.Visible = false;
        tblShow.Visible = true;
        cancel();
        btnSave.Text = "Save";
        
    }
}