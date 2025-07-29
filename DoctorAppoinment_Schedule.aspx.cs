 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;

public partial class DoctorAppoinment_Schedule :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Appointment_C ObjApp = new Appointment_C();
    protected void Page_Load(object sender, EventArgs e)
    {

        //Page.SetFocus(txtMachinCode);

        if (!IsPostBack)
        {
            try
            {
                //LUNAME.Text = Convert.ToString(Session["username"]);
                //LblDCName.Text = Convert.ToString(Session["Bannername"]);
                //LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                //dt = new DataTable();
                //dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
                //this.PopulateTreeView(dt, 0, null);
                Label2.Visible = false;
                BindTime();
                FillddlCenter();
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
   
    public void BindTime()
    {
         DateTime dt = Convert.ToDateTime("07:00:00"); //for adding start time
         for (int i = 0; i <= 30; i++) //Set up every 30 minute interval
         {
             ListItem li2 = new ListItem(dt.ToShortTimeString(), dt.ToShortTimeString());
             li2.Selected = false;
             ddlFromTime.Items.Add(li2);
             ddlToTime.Items.Add(li2);
             dt = dt.AddMinutes(30);
         }
    }

   
    private void FillddlCenter()
    {
        try
        {
            ddlCenter.DataSource = ObjApp.GetAllDr_Centert();
            ddlCenter.DataTextField = "DoctorCode";
            ddlCenter.DataValueField = "dr_codeid";
            ddlCenter.DataBind();
            ddlCenter.Items.Insert(0, "Select " + Convert.ToString(Session["CenterName"]));
            ddlCenter.SelectedIndex = 1;
            if (Session["usertype"].ToString().Trim() == "CollectionCenter")
            {
                createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                createuserTable_Bal_C uii = new createuserTable_Bal_C(ui.CenterCode.Trim());
                ddlCenter.SelectedItem.Text = (uii.CenterCode.Trim());
                ddlCenter.SelectedValue = Convert.ToString(uii.Drid);
                ddlCenter.Enabled = false;

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
    private void bindgrid()
    {
        try
        {
            dt = ObjApp.GetAllDr_Schedule();
            RoutinTest_Grid.DataSource = dt;
            RoutinTest_Grid.DataBind();
           
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
    protected void Button2_Click(object sender, EventArgs e)
    {
       // txtMachinCode.Text = "";
      //  txtMachinename.Text = "";
        Label2.Visible = false;
       // ttttt();
    }
    
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            //if (txtMachinCode.Text != "")
            //{               

            if (Convert.ToInt32(ViewState["Editflag"]) == 1)
            {
                ObjApp.SlotTime = ddlFromTime.SelectedValue;
                ObjApp.SlotEnd = ddlToTime.SelectedValue;
                ObjApp.CenterCode = ddlCenter.SelectedItem.Text;
                ObjApp.CenterID = Convert.ToInt32(ddlCenter.SelectedValue);
                ObjApp.CreatedBy = Convert.ToString(Session["username"]);

                ObjApp.Patregdate = Convert.ToString(fromdate.Text);
                ObjApp.PatregdateEnd = Convert.ToString(todate.Text);
                ObjApp.SlotId = Convert.ToInt32(ViewState["rid"]);
                ObjApp.Update_Doctorschedule(Convert.ToInt32(Session["Branchid"]));

               
                Label2.Visible = true;
                Label2.Text = "record Updated successfully.";
                bindgrid();
                ViewState["Editflag"] = null;

            }
            else
            {

                ObjApp.SlotTime = ddlFromTime.SelectedValue;
                ObjApp.SlotEnd = ddlToTime.SelectedValue;
                ObjApp.CenterCode = ddlCenter.SelectedItem.Text;
                ObjApp.CenterID = Convert.ToInt32(ddlCenter.SelectedValue);
                ObjApp.CreatedBy = Convert.ToString(Session["username"]);

                ObjApp.Patregdate = Convert.ToString(fromdate.Text);
                ObjApp.PatregdateEnd = Convert.ToString(todate.Text);
                ObjApp.Insert_Doctorschedule(Convert.ToInt32(Session["Branchid"]));
                Label2.Visible = true;
                Label2.Text = "Record Saved successfully.";
                bindgrid();

            }
            //    txtMachinCode.Text = "";
            //    txtMachinename.Text = "";
            //}
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
    protected void RoutinTest_Grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            ViewState["rid"] = RoutinTest_Grid.DataKeys[e.NewEditIndex].Value;
            ObjApp.Get_DrSchedule(Convert.ToInt32(ViewState["rid"]), Convert.ToInt32(Session["Branchid"]));
            //txtMachinCode.Text = samp.P_Instumentcode;
         // ddlFromTime.SelectedValue = ObjApp.SlotTime;
         // ddlToTime.SelectedValue = ObjApp.SlotEnd;
          fromdate.Text = ObjApp.Patregdate;
          todate.Text = ObjApp.PatregdateEnd;
         
            ddlCenter.SelectedValue = Convert.ToString(ObjApp.CenterID);
            ViewState["Editflag"] = 1;

            Label2.Visible = false;
            e.Cancel = true;
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
    protected void RoutinTest_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            RoutinTest_Grid.PageIndex = e.NewPageIndex;
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
    protected void RoutinTest_Grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int reaid = Convert.ToInt32(RoutinTest_Grid.DataKeys[e.RowIndex].Value);
            ObjApp.delete_Drschedule(reaid, Convert.ToInt32(Session["Branchid"]));
            bindgrid();
            Label2.Visible = true;
            Label2.Text = "record Deleted successfully.";
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
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow gvr in RoutinTest_Grid.Rows)
            {
                CheckBox chk = gvr.FindControl("chk") as CheckBox;
                if (chk.Checked)
                {
                    int reaid = Convert.ToInt32(RoutinTest_Grid.DataKeys[gvr.RowIndex].Value);
                    RoutinTest_C sampl = new RoutinTest_C();
                    sampl.delete_Machine(reaid, Convert.ToInt32(Session["Branchid"]));
                }
            }
            bindgrid();
            Label2.Visible = true;
            Label2.Text = "record Deleted successfully.";
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