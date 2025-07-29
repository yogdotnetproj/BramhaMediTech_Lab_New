using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class RefSetting :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Uniquemethod_Bal_C.isSessionSet())
        {
            string ss = System.Configuration.ConfigurationManager.AppSettings["LogOutURL"].Trim();
            Response.Redirect(ss);
        }
       
        Page.SetFocus(ddldoctor);

        if (!IsPostBack)
        {
            fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
            todate.Text = Date.getdate().ToString("dd/MM/yyyy");
            LUNAME.Text = Convert.ToString(Session["username"]);
            LblDCName.Text = Convert.ToString(Session["Bannername"]);
            LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
            dt = new DataTable();
            dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
            this.PopulateTreeView(dt, 0, null);
            BindDoctor();
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
    public void BindDoctor()
    {
        ddldoctor.DataSource = DrMT_sign_Bal_C.doctorInformation("", "", Convert.ToInt32(Session["Branchid"]));
        ddldoctor.DataTextField = "DoctorName";
        ddldoctor.DataValueField = "DoctorCode";
        ddldoctor.DataBind();
        ddldoctor.Items.Insert(0, new ListItem("All", ""));
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
           
            Cshmst_Bal_C Dircash = new Cshmst_Bal_C();
            Dircash.username = Session["username"].ToString();
            Dircash.P_DigModule = Convert.ToInt32(Session["DigModule"]);
            Dircash.ReCalculate(ddldoctor.SelectedValue, fromdate.Text, todate.Text, Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["financialyear"].ToString()));
            Label4.Visible = true;
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