 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using DAL;
using System.Text;

public partial class MasterPage : System.Web.UI.MasterPage
{
    clsDbDatabase ObjDBCon = new clsDbDatabase();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // GetMenuITem();
            DataTable dt=new DataTable ();
            dt = ObjTB.BindMainMenu(Convert.ToString( Session["username"]),Convert.ToString( Session["password"]));
            this.PopulateTreeView(dt, 0, null);
            
        }
        lblusername.Text = Convert.ToString(Session["username"]);
        GetMenuITem();
        Label44.Text = "Todays Patient Count:" + Patmst_New_Bal_C.PatientCount(Session["CenterCode"], Convert.ToInt32(Session["Branchid"]), Date.getdate().ToString("dd/MM/yyyy"), Date.getdate().ToString("dd/MM/yyyy"), "", "");
        Bindbanner();
    }
    public void Bindbanner()
    {
        DataTable dtban = new DataTable();
        dtban = ObjTB.Bindbanner();
        if (dtban.Rows.Count > 0)
        {
            string BName = "SHREEYASSH DIAGNOSTICS";
            lblDemoHospitalName.Text = Convert.ToString(dtban.Rows[0]["BannerName"]).Trim();
            if (Convert.ToString(dtban.Rows[0]["BannerName"]).Trim() == BName)
            {
            }
            else
            {
                //string BCount = Patmst_New_Bal_C.PatientCountBanner(Convert.ToInt32(Session["Branchid"]));
                //if (Convert.ToInt32(BCount) > 209)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
                //    Response.Redirect("~/Login.aspx");
                //}

            }

        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
            Response.Redirect("~/Login.aspx");

        }
    }
    
   
    private void GetMenuITem()
    {
        // objDBCon.OpenConnection(Data Source=dip-PC\sqlexpress;Initial Catalog=RealEstate_20;Integrated Security=True
       // string conn = @"Data Source=dip-PC\sqlexpress;Initial Catalog=RealEstate_20;Integrated Security=True";
      
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
      // objDBCon.OpenConnection();
       // SqlDataAdapter da=new SqlDataAdapter ("SP_phmnuds",conn);

        SqlCommand cmd = new SqlCommand("SP_phmnuds", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 50).Value = Session["username"];
        cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = Session["password"];

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
      
            ad.Fill(ds);
       
      
      //  da.Fill(ds);
        ds.Relations.Add("childRows", ds.Tables[0].Columns["MenuID"], ds.Tables[1].Columns["MenuID"]);
        foreach (DataRow level1dataRow in ds.Tables[0].Rows)
        {
            MenuItem item = new MenuItem();
            item.Text = level1dataRow["MenuName"].ToString();
            item.NavigateUrl = level1dataRow["NavigateURL"].ToString();


            //TreeNode tn1 = new TreeNode();
            //tn1.Value = Convert.ToString(level1dataRow["MenuName"].ToString());
            //tn1.Text = level1dataRow["MenuName"].ToString();
            //// tn1.ToolTip = "Sitecode I";
            ////tn1.Checked = false;
            //TrMenu.Nodes.Add(tn1);


            DataRow[] level2DataRows = level1dataRow.GetChildRows("childRows");
            foreach (DataRow level2DataRow in level2DataRows)
            {

                MenuItem ChildItem = new MenuItem();
                ChildItem.Text = level2DataRow["SubMenuName"].ToString();
                ChildItem.NavigateUrl = level2DataRow["SubMenuNavigateURL"].ToString();
                item.ChildItems.Add(ChildItem);
            }
            RealMenu.Items.Add(item);
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
        int tId = Convert.ToInt32( TrMenu.SelectedValue);
        DataTable dtform = new DataTable();
        dtform = ObjTB.BindForm(tId);

        Response.Redirect(dtform.Rows[0]["SubMenuNavigateURL"].ToString());
    }
    protected void ibLogout_Click(object sender, ImageClickEventArgs e)
    {

    }
}
