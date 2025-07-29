 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using BAL;

public partial class BankMaster :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    ClsBankName objbank = new ClsBankName();
    DataSet dss;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LUNAME.Text = Convert.ToString(Session["username"]);
            LblDCName.Text = Convert.ToString(Session["Bannername"]);
            LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
            dt = new DataTable();
            dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
            this.PopulateTreeView(dt, 0, null);
            loadgrid();
        }
    }

    private void loadgrid()
    {
        dss = new DataSet();
        dss = objbank.bank(3);
        if (dss.Tables[0].Rows.Count != 0)
        {
            GVBankMaster.DataSource = dss.Tables[0];
            GVBankMaster.DataBind();
        }
        dss.Dispose();
    }
    private void PopulateTreeView(DataTable dtparent, int Parentid, TreeNode treeNode)
    {
        foreach (DataRow row in dtparent.Rows)
        {
            TreeNode child = new TreeNode
            {
                Text = row["MenuName"].ToString(),
                Value = row["MenuID"].ToString()

            };
            if (Parentid == 0)
            {
                TrMenu.Nodes.Add(child);
                DataTable dtchild = new DataTable();
                dtchild = ObjTB.BindChildMenu(child.Value, Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                PopulateTreeView(dtchild, int.Parse(child.Value), child);

            }
            else
            {
                treeNode.ChildNodes.Add(child);
            }

        }
    }
    protected void TrMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        int tId = Convert.ToInt32(TrMenu.SelectedValue);
        DataTable dtform = new DataTable();
        dtform = ObjTB.BindForm(tId);

        Response.Redirect(dtform.Rows[0]["SubMenuNavigateURL"].ToString());
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (IsValid())
        {
            try
            {
                tblcreatebranch.Visible = false;
                tblShow.Visible = true;

                if (btnsave.Text == "Update")
                {
                    objbank.getbankid = Convert.ToString(ViewState["ID"]);
                    objbank.getbankname = txtbankname.Text;
                    objbank.getbranchname = txtbranchname.Text;
                    objbank.getifsccode = txtifsc.Text;
                    objbank.getdescription = txtdescription.Text;


                    objbank.bank(2);
                    //MessageBox("Update Data Successfully");
                    LBLValidation.Text = "Update Data Successfully";
                    clear();
                    loadgrid();
                    tblcreatebranch.Visible = false;
                    tblShow.Visible = true;
                }
                else
                {

                    objbank.getbankname = txtbankname.Text;
                    objbank.getbranchname = txtbranchname.Text;
                    objbank.getifsccode = txtifsc.Text;
                    objbank.getdescription = txtdescription.Text;


                    objbank.bank(1);
                    //MessageBox("Update Data Successfully");
                    LBLValidation.Text = "Save Data Successfully";
                    clear();
                    loadgrid();
                    tblcreatebranch.Visible = false;
                    tblShow.Visible = true;
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
        if (txtbranchname.Text == "")
        {
            LbMsg.Visible = true;
            isvalid = false;
            txtbranchname.BackColor = Color.Red;
            txtbranchname.Focus();
            //  MessageBox("Please Enter Branch Name");

            LBLValidation.Text = "Please Enter Branch Name";
            return false;
        }
        else
        {
            txtbranchname.BackColor = Color.White;
        }
        //if (txtbankname.Text == "")
        //{
        //    LbMsg.Visible = true;
        //    isvalid = false;
        //    txtbankname.BackColor = Color.Red;
        //    // MessageBox("Please Enter Branch Code");
        //    LBLValidation.Text = "Please Enter Bank Name";
        //    txtbankname.Focus();
        //    return false;
        //}
        //else
        //{
        //    txtbankname.BackColor = Color.White;
        //}

        if (txtifsc.Text == "")
        {
            LbMsg.Visible = true;
            isvalid = false;
            txtifsc.BackColor = Color.Red;
            LBLValidation.Text = "Please Enter IFSC Code";
            //MessageBox("Please Enter Branch Address");
            txtifsc.Focus();
            return false;
        }
        else
        {
            txtifsc.BackColor = Color.White;
        }


        LBLValidation.Text = "";
        return true;
    }

    private void clear()
    {
        txtbankname.Text = "";
        txtbranchname.Text = "";
        txtifsc.Text = "";
        txtdescription.Text = "";
    }
    protected void btnAddnew_Click(object sender, EventArgs e)
    {
        clear();
        tblcreatebranch.Visible = true;
        tblShow.Visible = false;
        btnsave.Text = "Save";
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        tblcreatebranch.Visible = false;
        tblShow.Visible = true;
        btnsave.Text = "Save";
        clear();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        LBLValidation.Text = "";
        LblMsg.Text = "";
    }
    protected void GVBankMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVBankMaster.PageIndex = e.NewPageIndex;
        loadgrid();
    }
    protected void GVBankMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            objbank.getbankid = Convert.ToInt32(GVBankMaster.DataKeys[e.RowIndex].Value.ToString()).ToString();
            objbank.bank(4);
            loadgrid();
            //MessageBox("Delete Successfully");
            LBLValidation.Text = "Record Delete Successfully";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GVBankMaster_RowEditing(object sender, GridViewEditEventArgs e)
    {
        string dd = GVBankMaster.DataKeys[e.NewEditIndex]["BankId"].ToString();
        ViewState["ID"] = dd;
        tblcreatebranch.Visible = true;
        tblShow.Visible = false;
        DataTable dt = new DataTable();
        objbank.getbankid = dd;
        dt = objbank.bankTest(5);

        if (dt.Rows.Count != 0)
        {
            txtbankname.Text = dt.Rows[0]["BankName"].ToString();
            txtbranchname.Text = dt.Rows[0]["BranchName"].ToString();
            txtifsc.Text = dt.Rows[0]["IFSCCode"].ToString();
            txtdescription.Text = dt.Rows[0]["Description"].ToString();

            btnsave.Text = "Update";
        }
        e.Cancel = true;
    }
}