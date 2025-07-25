using System;
using System.Collections.Generic;
//using System.Data.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
public partial class TestWiseStatusDashboard : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    AdminSettings_C ObjAs = new AdminSettings_C();
    DataTable dt = new DataTable();
    object fromDate = null, toDate = null;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {

            try
            {
              //  LUNAME.Text = Convert.ToString(Session["username"]);
              //  LblDCName.Text = Convert.ToString(Session["Bannername"]);
              //  LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
               // dt = new DataTable();
               // dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
               // this.PopulateTreeView(dt, 0, null);
                BindTestStatus();

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
  

   
    protected void RefreshGridView(object sender, EventArgs e)
    {
        BindTestStatus();
    }
    public void BindTestStatus()
    { 
       // todate.Text = Date.getdate().ToString("dd/MM/yyyy");
        toDate = Date.getdate().ToString("dd/MM/yyyy");
        ObjAs.AlterViewvw_VW_Countstatus(toDate);
        ObjAs.AlterViewvw_VW_TestStatus_DailyDisp(toDate);
        ObjAs.AlterViewvw_VW_TestStatus(toDate);
        DataTable dt=new DataTable ();
        dt = ObjAs.Get_TestStatus_Details();
        DLTestStatus.DataSource = dt;
        DLTestStatus.DataBind();
    }
    protected void DLTestStatus_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        string TestStatus = Convert.ToString((e.Item.FindControl("LblTestStatus") as Label).Text);
        bool Emg = Convert.ToBoolean((e.Item.FindControl("isemergency") as HiddenField).Value);
        // or
        //DateTime currentdate = Convert.ToDateTime((e.Item.FindControl("lblCurrentdate") as Label).Text);

       // bool Emg = Convert.ToBoolean((GV_Phlebotomist.Rows[i].FindControl("isemergency") as HiddenField).Value);
        if (Emg == true)
        {
            (e.Item.FindControl("btnEmergency") as ImageButton).Visible = true;
        }
        else
        {
            (e.Item.FindControl("btnEmergency") as ImageButton).Visible = false;
        }
        if (TestStatus == "Completed")
        {
           // (e.Item.FindControl("tbl") as System.Web.UI.HtmlControls.HtmlTable).BgColor = "Green";
           // (e.Item.FindControl("LblTestStatus") as Label) = System.Drawing.Color.Green;
           // e.Item.BackColor = System.Drawing.Color.Green;

            (e.Item.FindControl("LblTestStatusN") as Label).Text = "<span class='btn btn-xs btn-success' >Prin</span>";
        }
        if (TestStatus == "Registered")
        {
          //  (e.Item.FindControl("tbl") as System.Web.UI.HtmlControls.HtmlTable).BgColor = "Red";
           // e.Item.BackColor = System.Drawing.Color.Red;
            (e.Item.FindControl("LblTestStatusN") as Label).Text = "<span class='btn btn-xs btn-danger' >Reg</span>";
        }
        if (TestStatus == "Partial Authorized")
        {
           // (e.Item.FindControl("tbl") as System.Web.UI.HtmlControls.HtmlTable).BgColor = "Red";
           // e.Item.BackColor = System.Drawing.Color.Chocolate;
            (e.Item.FindControl("LblTestStatusN") as Label).Text = "<span class='btn btn-xs btn-warning' >Pr Auth</span>";
        }
        if (TestStatus == "Authorized")
        {
            //(e.Item.FindControl("tbl") as System.Web.UI.HtmlControls.HtmlTable).BgColor = "Blue";
           // e.Item.BackColor = System.Drawing.Color.Gray;
            (e.Item.FindControl("LblTestStatusN") as Label).Text = "<span class='btn btn-xs btn-primary' >Auth</span>";
        }
        if (TestStatus == "Tested")
        {
            //(e.Item.FindControl("tbl") as System.Web.UI.HtmlControls.HtmlTable).BgColor = "Blue";
           // e.Item.BackColor = System.Drawing.Color.Blue;
            (e.Item.FindControl("LblTestStatusN") as Label).Text = "<span class='badge'>Test</span>";
        }
    }
}