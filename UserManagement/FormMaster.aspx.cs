using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using BAL;

public partial class UserManagement_FormMaster :BasePage
{
    ClsMENUMASTER objmenu = new ClsMENUMASTER();
    DataSet dss;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadgrid();
        }
    }


    private void loadgrid()
    {
        dss = new DataSet();
        dss = objmenu.menumaster(2);
        if (dss.Tables[0].Rows.Count != 0)
        {
            GvFormMaster.DataSource = dss.Tables[0];
            GvFormMaster.DataBind();
        }
        dss.Dispose();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        clear();
        btnsave.Text = "Save";
        tblShow.Visible = true;
        tblDisp.Visible = false;
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (validation())
        {
            try
            {
                if (btnsave.Text == "Update")
                {
                    //GridViewRow row = GvCountryMaster.SelectedRow;
                    //Label id = row.FindControl("lblcountryid") as Label;

                    objmenu.getmenuid = Convert.ToString(ViewState["menuid"]);
                    objmenu.getmenuname= txtmenuname.Text;
                    objmenu.getnevigateurl = txtNavigateURL.Text;


                    objmenu.menumaster(3);
                    clear();
                    loadgrid();
                    LblMsg.Text = "Record Update Successfully";
                    //MessageBox("Data Update Successfully");
                    tblShow.Visible = false;
                    tblDisp.Visible = true;
                }
                else
                {
                    objmenu.getmenuname = txtmenuname.Text;
                    objmenu.getnevigateurl = txtNavigateURL.Text;
                    objmenu.menumaster(1);
                    LblMsg.Text = "Record Inserted Successfully";
                    loadgrid();
                    clear();
                    tblShow.Visible = false;
                    tblDisp.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    private bool validation()
    {
        bool isvalid = true;
        if (txtmenuname.Text == "")
        {
            isvalid = false;
            txtmenuname.BackColor = Color.Red;
            LbMsg.Visible = true;
            LBLValidation.Text = "Please Enter Menu Name";
            return false;
        }
        else
        {
            txtmenuname.BackColor = Color.White;
        }

        if (txtNavigateURL.Text == "")
        {
            isvalid = false;
            txtNavigateURL.BackColor = Color.Red;
            LbMsg.Visible = true;
            LBLValidation.Text = "Please Enter Navigate URL";
            return false;
        }
        else
        {
            txtNavigateURL.BackColor = Color.White;
        }
        return true;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        LBLValidation.Text = "";
    }

    private void clear()
    {
        txtmenuname.Text = "";
        txtNavigateURL.Text = "";

    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        clear();
        LBLValidation.Text = "";
        tblDisp.Visible = true;
        tblShow.Visible = false;
    }
    protected void GvFormMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvFormMaster.PageIndex = e.NewPageIndex;
        loadgrid();
    }
    protected void GvFormMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            objmenu.getmenuid= Convert.ToInt32(GvFormMaster.DataKeys[e.RowIndex].Value.ToString()).ToString();
            objmenu.menumaster(4);
            loadgrid();
            LblMsg.Text = "Delete Successfully";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GvFormMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string dd = GvFormMaster.DataKeys[e.NewEditIndex]["MenuID"].ToString();
        ViewState["menuid"] = dd;
        tblDisp.Visible = false;
        tblShow.Visible = true;
        DataTable dt = new DataTable();
        objmenu.getmenuid = dd;
        dt = objmenu.menumasterSP(5);

        if (dt.Rows.Count != 0)
        {
            txtmenuname.Text = dt.Rows[0]["MenuName"].ToString();
            txtNavigateURL.Text = dt.Rows[0]["NavigateURL"].ToString();
            
            btnsave.Text = "Update";
            tblDisp.Visible = false;
            tblShow.Visible = true;
        }
        e.Cancel = true;
    }
    protected void GvFormMaster_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtmenuname.Text = GvFormMaster.SelectedRow.Cells[3].Text;
        txtNavigateURL.Text = GvFormMaster.SelectedRow.Cells[4].Text;
        mp.Show();
    }
}