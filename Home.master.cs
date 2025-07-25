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

public partial class SiteMaster : System.Web.UI.MasterPage
{
    public static string Username = "", UYype = "";
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dtderial = new DataTable();
    string[] path;
    string[] patho;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // checAuthincation();
           // GetMACAddress();
       
           //patho = HttpContext.Current.Request.Url.AbsolutePath.Split('.');
           // path = patho[0].ToString().Split('/');

            LblUtype.Text = Convert.ToString(Session["username"]);
            Username = Convert.ToString(Session["username"]);
            //if (Convert.ToString(Session["era"]) != "YES")
            //{
            //    DataTable dt = new DataTable();
            //    dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
            //    this.PopulateTreeView(dt, 0, null);
            //}
            //else
            //{
            //    DataTable dt = new DataTable();
            //    dt = ObjTB.BindMainMenuHMS(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
            //    this.PopulateTreeView_HMS(dt, 0, null);

            //}
            if (Convert.ToString(Session["usertype"]) != "Administrator")
            {
               //checkexistpageright();
            }
            UYype = Convert.ToString(Session["usertype"]);
            LoadSlideBar(Username);


        }
        else 
        {
            LoadSlideBar(LblUtype.Text);
        }
        if (Convert.ToString(Session["usertype"]) == "Administrator")
        {
            DId.Visible = true;
        }
        else
        {
            DId.Visible = false;
        }
        }
    

    private void LoadSlideBar(string UserNm)
    {
        string SlideBarHtml = "";


        //UsernameLB.Text = UserNm;
       // UsernameLB2.Text = UserNm;
        string MenuSQL = "", SubMenuSQL="";
        DataTable MenuDt = new DataTable();
        if (Convert.ToString(Session["usertype"]) == "Administrator")
        {
            MenuSQL = String.Format(@"SELECT Distinct MenuID,MenuName,icon from [dbo].[TBL_MenuMaster]  ORDER BY MenuID ", UserNm); //  WHERE  username = '{0}' SlidBarVeiw
        }
        else
        {
            MenuSQL = String.Format(@"SELECT DISTINCT         TBL_MenuMaster.MenuID ,TBL_MenuMaster.MenuName,TBL_MenuMaster.Icon  " +
                               " FROM         Roleright INNER JOIN      "+
                               " TBL_SubMenuMaster ON Roleright.FormId = TBL_SubMenuMaster.SubMenuID INNER JOIN  "+
                               " TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID INNER JOIN   "+
                               " CTuser ON Roleright.Usertypeid = CTuser.RollId  where (CTuser.USERNAME = '" + Convert.ToString(Session["username"]) + "') AND (CTuser.password = '" + Convert.ToString(Session["password"]) + "') and  TBL_SubMenuMaster.Isvisable=1  " +
                               " order by MenuID  ", UserNm);

        }

        string connectionString = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString);

        SqlCommand cmd = new SqlCommand(MenuSQL, con);

        SqlDataAdapter Adp = new SqlDataAdapter(cmd);

        Adp.Fill(MenuDt);

        foreach (DataRow MainDr in MenuDt.Rows)
        {
            DataTable SubMenuDt = new DataTable();
            string MainMenuName = MainDr["MenuName"].ToString();
            string Menuid = MainDr["MenuID"].ToString();
            string icon = MainDr["icon"].ToString();
            SlideBarHtml += "<li>";
            SlideBarHtml += "<a href=" + (char)34 + "javascript:;" + (char)34 + "><i class=" + (char)34 + icon + (char)34 + "></i> ";
            SlideBarHtml +=     "                     ";
            SlideBarHtml += "<span class=" + (char)34 + "nav-label" + (char)34 + ">" + MainMenuName + "</span><i class=" + (char)34 + "fa fa-angle-down arrow" + (char)34 + "></i></a>";
            SlideBarHtml += "<ul class=" + (char)34 + "nav-2-level collapse" + (char)34 + ">";

            if (Convert.ToString(Session["usertype"]) == "Administrator")
            {
                SubMenuSQL = String.Format(@"SELECT Distinct SubMenuName,SubMenuNavigateURL,SubMenuID from TBL_SubMenuMaster INNER JOIN TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID WHERE    TBL_SubMenuMaster.Isvisable=1  and MenuName = '{1}' ORDER BY SubMenuID", UserNm, MainMenuName);//username = '{0}' and
            }
            else
            {
                SubMenuSQL = String.Format(@"SELECT        Roleright.Rightid, Roleright.Usertypeid, Roleright.FormId, Roleright.FormName, Roleright.Branchid, usr.ROLENAME, TBL_SubMenuMaster.SubMenuNavigateURL, TBL_MenuMaster.MenuName, TBL_MenuMaster.MenuID, "+
                      "  TBL_SubMenuMaster.SubMenuName, TBL_MenuMaster.Icon, TBL_SubMenuMaster.SubMenuID "+
                      "  FROM            Roleright INNER JOIN "+
                      "  usr ON Roleright.Usertypeid = usr.ROLLID AND Roleright.Branchid = usr.branchid INNER JOIN "+
                      "  TBL_SubMenuMaster ON Roleright.FormId = TBL_SubMenuMaster.SubMenuID INNER JOIN "+
                      "  TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID INNER JOIN "+
                      "  CTuser ON Roleright.Usertypeid = CTuser.Rollid where (CTuser.USERNAME = '" + Convert.ToString(Session["username"]) + "') AND (CTuser.password = '" + Convert.ToString(Session["password"]) + "') and TBL_SubMenuMaster.MenuID='" + Menuid + "' and  TBL_SubMenuMaster.Isvisable=1  " +
                      "  order by MenuID  ", UserNm, MainMenuName);

            }
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

        con.Close();
        con.Dispose();

        SlidBarHolder.Controls.Clear();
        SlidBarHolder.Controls.Add(new Literal { Text = SlideBarHtml });
    }

    //private void PopulateTreeView_HMS(DataTable dtparent, int Parentid, TreeNode treeNode)
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
    //            dtchild = ObjTB.BindChildMenu_HMS(child.Value, Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
    //            PopulateTreeView_HMS(dtchild, int.Parse(child.Value), child);

    //        }
    //        else
    //        {
    //            treeNode.ChildNodes.Add(child);
    //        }

    //    }
    //}


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
    //    if (Convert.ToString(Session["era"]) != "YES")
    //    {
    //        dtform = ObjTB.BindForm(tId);
    //        if (dtform.Rows.Count > 0)
    //        {
    //            Response.Redirect(dtform.Rows[0]["SubMenuNavigateURL"].ToString());
    //        }
    //    }
    //    else
    //    {

    //    }
    //}

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

    public void checAuthincation()
    {

        string MenuSQL = "";
        DataTable MenuDt = new DataTable();
        MenuSQL = String.Format(@"select *, case when DateOfBirth is null then  convert(varchar, Patregdate, 112)*55 else convert(varchar, DateOfBirth, 112)*55 end as SerialKey from patmt  where (PPID = '200')  ");



        string connectionString1 = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString1);

        SqlCommand cmd = new SqlCommand(MenuSQL, con);

        SqlDataAdapter Adp = new SqlDataAdapter(cmd);

        Adp.Fill(MenuDt);
        if (MenuDt.Rows.Count != 0)
        {
           // Response.Redirect("Login.aspx", false);
            string macAddress = "";
            string mac_src = "";
            int SrLength = Convert.ToInt32( MenuDt.Rows[0]["SerialKey"].ToString().Length);
            mac_src = MenuDt.Rows[0]["SerialKey"].ToString();
            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }
            for (int i = 0; i < 12; i++)
            {
                if (0 == (i % 3))
                {
                    if (i == 0)
                    {
                        macAddress = macAddress.Insert(macAddress.Length, mac_src.Substring(i, 3)) + "-"; ;
                    }
                    else
                    {
                        macAddress = macAddress.Insert(macAddress.Length, mac_src.Substring(i, 3)) + "-";
                    }
                }
            }

            macAddress = macAddress.Remove(15);
            UpdateSerialKey(macAddress);
        }
        con.Close();
        con.Dispose();
    }

    public string GetMACAddress()
    {
        string mac_src = "";
        string macAddress = "";

        foreach (System.Net.NetworkInformation.NetworkInterface nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
            {
                mac_src += nic.GetPhysicalAddress().ToString();
                break;
            }
        }

        while (mac_src.Length < 12)
        {
            mac_src = mac_src.Insert(0, "0");
        }

        for (int i = 0; i < 11; i++)
        {
            if (0 == (i % 2))
            {
                if (i == 10)
                {
                    macAddress = macAddress.Insert(macAddress.Length, mac_src.Substring(i, 2));
                }
                else
                {
                    macAddress = macAddress.Insert(macAddress.Length, mac_src.Substring(i, 2)) + "-";
                }
            }
        }
        return macAddress;
    }

    public void UpdateSerialKey( string SerialKey)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc = new SqlCommand("" +
             "update BannerTable set SerialKEY='" + SerialKey + "' ", conn);


       

        conn.Open();
        sc.ExecuteNonQuery();
        conn.Close();
        conn.Dispose();
      dtderial=  ObjTB.Get_SerialKey();
      if (dtderial.Rows.Count > 0)
      {
          if (Convert.ToString( dtderial.Rows[0]["SerialKEY"]).Trim() == SerialKey.Trim())
          {
              //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
             // Label10.Text = AA;
              string NewSerial =" Your Serial Key:  "+ SerialKey.ToString();
              ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + NewSerial.ToString() + "');</script>", false);
          }
          else
          {

          }
      }
    }

}
