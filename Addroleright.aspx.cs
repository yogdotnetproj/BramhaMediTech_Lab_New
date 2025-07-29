using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;
public partial class Addroleright :BasePage
{
    dbconnection dc = new dbconnection();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["HMS"]) != "Yes")
            {
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    checkexistpageright("Addroleright.aspx");
                }
            }
            bindusertype();
            dt = new DataTable();
            dt = ObjTB.BindMainMenu_treeview();
            this.PopulateTreeView(dt, 0, null);
        }
    }
    public void bindusertype()
    {
        ddlUsertype.DataSource = dc.FillUserroles(Session["Branchid"].ToString(), "2");
        ddlUsertype.DataValueField = "ROLLID";
        ddlUsertype.DataTextField = "ROLENAME";
        ddlUsertype.DataBind();
        ddlUsertype.Items.Insert(0, "Select UserType");
        ddlUsertype.Items[0].Value = "0";
        ddlUsertype.SelectedIndex = -1;
    }
    private void GetMenuITem()
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
        SqlDataAdapter da = new SqlDataAdapter("SP_phmnuds", conn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        ds.Relations.Add("childRows", ds.Tables[0].Columns["MenuID"], ds.Tables[1].Columns["MenuID"]);
        foreach (DataRow level1dataRow in ds.Tables[0].Rows)
        {
            MenuItem item = new MenuItem();
            item.Text = level1dataRow["MenuName"].ToString();
            item.NavigateUrl = level1dataRow["NavigateURL"].ToString();
            DataRow[] level2DataRows = level1dataRow.GetChildRows("childRows");
            foreach (DataRow level2DataRow in level2DataRows)
            {

                MenuItem ChildItem = new MenuItem();
                ChildItem.Text = level2DataRow["SubMenuName"].ToString();
                ChildItem.NavigateUrl = level2DataRow["SubMenuNavigateURL"].ToString();
                item.ChildItems.Add(ChildItem);
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
                TR_RoleRight.Nodes.Add(child);
                DataTable dtchild = new DataTable();
                dtchild = ObjTB.BindChildMenu_treeview(child.Value);
                PopulateTreeView(dtchild, int.Parse(child.Value), child);

            }
            else
            {
                treeNode.ChildNodes.Add(child);
                if (chkall.Checked == true)
                {
                    child.Checked = true;
                }
                //treeNode.Checked = true;
            }

        }
    }


    protected void TR_RoleRight_SelectedNodeChanged(object sender, EventArgs e)
    {
        int tId = Convert.ToInt32(TR_RoleRight.SelectedValue);
        DataTable dtform = new DataTable();
        dtform = ObjTB.BindForm(tId);
        if (dtform.Rows.Count > 0)
        {
            Response.Redirect(dtform.Rows[0]["SubMenuNavigateURL"].ToString());
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        foreach (TreeNode tn in TR_RoleRight.CheckedNodes)
        {
            string V = tn.Value;
            string M = tn.Text;
           
            ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"].ToString());
            ObjTB.P_FormId = Convert.ToInt32(tn.Value);
            ObjTB.P_FormName = tn.Text;
            ObjTB.P_Usertypeid = Convert.ToInt32(ddlUsertype.SelectedValue);
            ObjTB.Insert_Roleright();

        }
        BindGrid();
    }
    protected void ddlUsertype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    public void BindGrid()
    {
        DataTable dtgv = new DataTable();
        dtgv = ObjTB.Get_Rollright(ddlUsertype.SelectedValue);
        GV_userrollright.DataSource = dtgv;
        GV_userrollright.DataBind();
    }
    protected void GV_userrollright_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GV_userrollright.PageIndex = e.NewPageIndex;
            BindGrid();
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
   

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        dt = new DataTable();
        dt = ObjTB.BindMainMenu_treeview();
        if (chkall.Checked == true)
        {
           // this.PopulateTreeView_all(dt, 0, null);
            //this.PopulateTreeView(null, 0, null);
            this.PopulateTreeView_All(dt, 0, null);
            //foreach (TreeNode tn in TR_RoleRight)
            //{
            //    tn.Checked = true;
            //}
        }
        else
        {
            this.PopulateTreeView(dt, 0, null);
        }
    }
    private void PopulateTreeView_all(DataTable dtparent, int Parentid, TreeNode treeNode)
    {
        foreach (DataRow row in dtparent.Rows)
        {
            TreeNode child = new TreeNode
            {

               // Text = row["MenuName"].ToString(),
               // Value = row["MenuID"].ToString()


            };
           // child.Checked = true;

            
           // treeNode.Checked = true;
            //if (Parentid == 0)
            //{
            //    TR_RoleRight.Nodes.Add(child);
            //    DataTable dtchild = new DataTable();
            //    dtchild = ObjTB.BindChildMenu_treeview(child.Value);
            //    PopulateTreeView_all(dtchild, int.Parse(child.Value), child);

            //}
            //else
            //{
            //    treeNode.ChildNodes.Add(child);
            //    child.Checked = true;
            //    treeNode.Checked = true;
            //}
           

        }
    }
    protected void GV_userrollright_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Userright_Bal_C ObjUBC = new Userright_Bal_C();
        string Id = GV_userrollright.DataKeys[e.RowIndex].Value.ToString();

        ObjUBC.deleteUsers_Rights(Id);

        BindGrid();
    }
    private void PopulateTreeView_All(DataTable dtparent, int Parentid, TreeNode treeNode)
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
                TR_RoleRight.Nodes.Add(child);
                DataTable dtchild = new DataTable();
                dtchild = ObjTB.BindChildMenu_treeview(child.Value);
                PopulateTreeView_All(dtchild, int.Parse(child.Value), child);

            }
            else
            {
                treeNode.ChildNodes.Add(child);
                                
                    child.Checked = true;
                
                //treeNode.Checked = true;
            }

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