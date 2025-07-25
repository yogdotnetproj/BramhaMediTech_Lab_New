using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.Management;
using System.Data;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;

public partial class Home : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    GrapReport_C ObjGR = new GrapReport_C();
    DataTable dt = new DataTable();
    string Username = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Username = Convert.ToString(Session["username"]);
           // LblDCName.Text = Convert.ToString(Session["Bannername"]);
            //LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
            //dt = new DataTable();
            //dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
            //this.PopulateTreeView(dt, 0, null);
            //if (Session["usertype"].ToString() == "Administrator")
            //{
            //    TTTTT.Visible = true;
            //    ddlSelectGraph_SelectedIndexChanged(null, null);

            //    dddlpatcount_SelectedIndexChanged(null, null);
            //}
            if (Convert.ToString(Session["usertype"]) == "Administrator")
            {
                LoadSlideBar(Username);
            }
            else
            {
                Response.Redirect("Login.aspx", false);

            }

        }
        //else 
        //{
        //    LoadSlideBar(LblUtype.Text)
        //}
    }

    private void LoadSlideBar(string UserNm)
    {
        string SlideBarHtml = "";


       // UsernameLB.Text = UserNm;
        UsernameLB2.Text = UserNm;
        DataTable MenuDt = new DataTable();

       // string MenuSQL = String.Format(@"SELECT Distinct MenuID,MenuName,icon from [dbo].[SlidBarVeiw] WHERE  username = '{0}' ORDER BY MenuID", UserNm);
        string MenuSQL = String.Format(@"SELECT Distinct MenuID,MenuName,icon from [dbo].[TBL_MenuMaster] ORDER BY MenuID", UserNm); //  WHERE  username = '{0}' SlidBarVeiw
 

        string connectionString = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand(MenuSQL, con);

        SqlDataAdapter Adp = new SqlDataAdapter(cmd);

        Adp.Fill(MenuDt);

        foreach (DataRow MainDr in MenuDt.Rows)
        {
            DataTable SubMenuDt = new DataTable();
            string MainMenuName = MainDr["MenuName"].ToString();
            string icon = MainDr["icon"].ToString();
            SlideBarHtml += "<li>";
            SlideBarHtml += "<a href=" + (char)34 + "javascript:;" + (char)34 + "><i class=" + (char)34 + icon + (char)34 + "></i>";
            SlideBarHtml += "<span class=" + (char)34 + "nav-label" + (char)34 + ">" + MainMenuName + "</span><i class=" + (char)34 + "fa fa-angle-down arrow" + (char)34 + "></i></a>";
            SlideBarHtml += "<ul class=" + (char)34 + "nav-2-level collapse" + (char)34 + ">";

           // string SubMenuSQL = String.Format(@"SELECT Distinct SubMenuName,SubMenuNavigateURL,SubMenuID from [dbo].[SlidBarVeiw] WHERE  username = '{0}' and MenuName = '{1}' ORDER BY SubMenuID", UserNm, MainMenuName);
            string SubMenuSQL = String.Format(@"SELECT Distinct SubMenuName,SubMenuNavigateURL,SubMenuID from TBL_SubMenuMaster INNER JOIN TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID WHERE TBL_SubMenuMaster.Isvisable=1  and  MenuName = '{1}' ORDER BY SubMenuID", UserNm, MainMenuName);//username = '{0}' and

            SqlCommand cmd2 = new SqlCommand(SubMenuSQL, con);
            SqlDataAdapter Adp2 = new SqlDataAdapter(cmd2);
            Adp2.Fill(SubMenuDt);
            //SubMenuPart--------------------------------------------->
            foreach (DataRow SubMainDr in SubMenuDt.Rows)
            {
                string SubMenuName = SubMainDr["SubMenuName"].ToString();
                string SubMenuUrl = SubMainDr["SubMenuNavigateURL"].ToString();

                SlideBarHtml += "<li>";
                SlideBarHtml += "<a href=" + (char)34 + SubMenuUrl + (char)34 + ">" + SubMenuName.ToString() + "</a>";
                SlideBarHtml += "</li>";
            }
            //SubMenuPart----------------------------------------------

            SlideBarHtml += "</ul>";
            SlideBarHtml += "</li>";

        }


        SlidBarHolder.Controls.Clear();
        SlidBarHolder.Controls.Add(new Literal { Text = SlideBarHtml });
    }


    //private void PopulateTreeView(DataTable dtparent, int Parentid, TreeNode treeNode)
    //{
    //    foreach (DataRow row in dtparent.Rows)
    //    {
    //        TreeNode child = new TreeNode
    //        {
    //            Text = row["MenuName"].ToString(),
    //            Value = row["MenuID"].ToString()

    //        };
    //        if (Parentid == 0)
    //        {
    //            TrMenu.Nodes.Add(child);
    //            DataTable dtchild = new DataTable();
    //            dtchild = ObjTB.BindChildMenu(child.Value, Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
    //            PopulateTreeView(dtchild, int.Parse(child.Value), child);

    //        }
    //        else
    //        {
    //            treeNode.ChildNodes.Add(child);
    //        }

    //    }
    //}


    //protected void TrMenu_SelectedNodeChanged(object sender, EventArgs e)
    //{
    //    int tId = Convert.ToInt32(TrMenu.SelectedValue);
    //    DataTable dtform = new DataTable();
    //    dtform = ObjTB.BindForm(tId);
    //    if (dtform.Rows.Count > 0)
    //    {
    //        Response.Redirect(dtform.Rows[0]["SubMenuNavigateURL"].ToString());
    //    }
    //}
}