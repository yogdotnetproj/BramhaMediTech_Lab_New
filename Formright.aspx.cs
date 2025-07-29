 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;

public partial class Formright :BasePage
{
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Userright_Bal_C ObjAT = new Userright_Bal_C();
    TreeviewBind_C ObjTB = new TreeviewBind_C(); 
    protected void Page_Load(object sender, EventArgs e)
    {   
        if (!IsPostBack)
        {

            try
            {
                LUNAME.Text = Convert.ToString(Session["username"]);
                LblDCName.Text = Convert.ToString(Session["Bannername"]);
                LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                dt = new DataTable();
                dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                this.PopulateTreeView(dt, 0, null);

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
        if (dtform.Rows.Count > 0)
        {
            Response.Redirect(dtform.Rows[0]["SubMenuNavigateURL"].ToString());
        }
    }
    public void bindgrid()
    {
        try
        {
            dt = ObjAT.getformright();
            GV_UserType.DataSource = dt;
            GV_UserType.DataBind();
            for (int i = 0; i < GV_UserType.Rows.Count; i++)
            {
                //bool ischeckt = Convert.ToBoolean((GV_UserType.Rows[i].Cells[2].FindControl("chkisvisable") as CheckBox).Checked);
                bool ischeck = Convert.ToBoolean((GV_UserType.Rows[i].Cells[2].FindControl("lblisvisable") as Label).Text);
                if (ischeck == true)
                {
                    ((GV_UserType.Rows[i].Cells[2].FindControl("chkisvisable") as CheckBox).Checked) = true;
                }
                else
                {
                    ((GV_UserType.Rows[i].Cells[2].FindControl("chkisvisable") as CheckBox).Checked) = false;
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
    protected void GV_UserType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgrid();
    }
    protected void GV_UserType_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GV_UserType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ViewState["rid"] = GV_UserType.DataKeys[e.NewEditIndex].Value;
    }
    protected void GV_UserType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == 1)
            return;
        //  string isvis=   (e.Row.FindControl("lblisvisable") as Label).Text;
        // Label ch = e.Row.FindControl("lblisvisable") as Label;

        //  string isvis=   e.Row.Cells[2].FindControl("lblisvisable") as Label
        //  (userlist.Rows[i].Cells[8].FindControl("lblisvisable") as Label).Text)
    }
    protected void chkisvisable_CheckedChanged(object sender, EventArgs e)
    {
        //  int tt = GV_UserType.DataKeys[0]["SubMenuID"].ToString();

        this.GV_UserType_RowEditing(null, null);

    }

}