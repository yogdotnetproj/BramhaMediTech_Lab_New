using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using BAL;

public partial class UserManagement_MenuMaster : System.Web.UI.Page
{
    ClsSubMenuMaster objsub = new ClsSubMenuMaster();
    ClsMENUMASTER objmenu = new ClsMENUMASTER();
    DataSet dss;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadgrid();
            loadmenu();
        }
    }
    private void loadmenu()
    {
        dss = objmenu.menumaster(6);
        txtmenuname.DataSource = dss;
        txtmenuname.DataTextField = "MenuName";
        txtmenuname.DataValueField = "MenuID";
        txtmenuname.DataBind();
        txtmenuname.Items.Add(new ListItem("Select Menu", "-1"));
        txtmenuname.SelectedValue = "-1";
    }

    private void loadgrid()
    {
        dss = new DataSet();
        dss = objsub.submenu(2);
        if (dss.Tables[0].Rows.Count != 0)
        {
            GvSubMenuMaster.DataSource = dss.Tables[0];
            GvSubMenuMaster.DataBind();
        }
        dss.Dispose();
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

                    objsub.getsubmenuid = Convert.ToString(ViewState["submenuid"]);
                    objsub.getmenuid = txtmenuname.Text;
                    objsub.getsubmenuname = txtsubmenuname.Text;
                    objsub.getsubnevigateurl = txtnavigateurl.Text;


                    objsub.submenu(3);
                    clear();
                    loadgrid();
                    LblMsg.Text = "Record Update Successfully";
                    //MessageBox("Data Update Successfully");
                    tblShow.Visible = false;
                    tblDisp.Visible = true;
                }
                else
                {
                    objsub.getmenuid = txtmenuname.Text;
                    objsub.getsubmenuname = txtsubmenuname.Text;
                    objsub.getsubnevigateurl = txtnavigateurl.Text;

                    objsub.submenu(1);
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
        if (txtmenuname.Text == "-1")
        {
            isvalid = false;
            txtmenuname.BackColor = Color.Red;
            LbMsg.Visible = true;
            LBLValidation.Text = "Please select Menu Name";
            return false;
        }
        else
        {
            txtmenuname.BackColor = Color.White;
        }
        if (txtsubmenuname.Text == "")
        {
            isvalid = false;
            txtsubmenuname.BackColor = Color.Red;
            LbMsg.Visible = true;
            LBLValidation.Text = "Please Enter Sub Menu Name";
            return false;
        }
        else
        {
            txtsubmenuname.BackColor = Color.White;
        }

        if (txtnavigateurl.Text == "")
        {
            isvalid = false;
            txtnavigateurl.BackColor = Color.Red;
            LbMsg.Visible = true;
            LBLValidation.Text = "Please Enter Navigate URL";
            return false;
        }
        else
        {
            txtnavigateurl.BackColor = Color.White;
        }

        return true;
    }

    private void clear()
    {
        txtmenuname.Text = "-1";
        txtsubmenuname.Text = "";
        txtnavigateurl.Text = "";
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        LBLValidation.Text = "";
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        clear();
        LBLValidation.Text = "";
        tblDisp.Visible = true;
        tblShow.Visible = false;
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        clear();
        btnsave.Text = "Save";
        tblShow.Visible = true;
        tblDisp.Visible = false;
    }
    protected void GvSubMenuMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvSubMenuMaster.PageIndex = e.NewPageIndex;
        loadgrid();
    }
    protected void GvSubMenuMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            objsub.getsubmenuid= Convert.ToInt32(GvSubMenuMaster.DataKeys[e.RowIndex].Value.ToString()).ToString();
            objsub.submenu(4);
            loadgrid();
            LblMsg.Text = "Delete Successfully";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GvSubMenuMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string dd = GvSubMenuMaster.DataKeys[e.NewEditIndex]["SubMenuID"].ToString();
        ViewState["submenuid"] = dd;
        tblDisp.Visible = false;
        tblShow.Visible = true;
        DataTable dt = new DataTable();
        objsub.getsubmenuid= dd;
        dt = objsub.submenuSp(5);

        if (dt.Rows.Count != 0)
        {
            txtmenuname.Text = dt.Rows[0]["MenuID"].ToString();
            txtsubmenuname.Text = dt.Rows[0]["SubMenuName"].ToString();
            txtnavigateurl.Text = dt.Rows[0]["SubMenuNavigateURL"].ToString();

            btnsave.Text = "Update";
            tblDisp.Visible = false;
            tblShow.Visible = true;
        }
        e.Cancel = true;
    }
    protected void GvSubMenuMaster_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}