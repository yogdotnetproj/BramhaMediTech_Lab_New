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
using System.Net;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Net.Http;

public partial class Appoinment_Desk : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    Appointment_C ObjApp = new Appointment_C();
    DataTable dtBeds = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            try
            {
               
                ViewState["Shift"] = "";
                ViewState["PatRegId"] = "";
                ViewState["IpdNo"] = "";
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                FillddlCenter();
                BindRoomType();
                
                
               
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
        this.RegisterPostBackControl();
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
               // ddlCenter.SelectedItem.Text = (ui.CenterCode.Trim());
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
    public void BindRoomType()
    {
        dt = ObjApp.ReadDataSlot(ddlCenter.SelectedValue, todate.Text, "2020-09-01", "2020-09-01");
        BedDataList.DataSource = dt;
        BedDataList.DataBind();
    }
    protected void RdbRoomType_SelectedIndexChanged(object sender, EventArgs e)
    {

        //DataTable dtRooms = objDalIpdDesk.BindRooms(Convert.ToInt32(RdbRoomType.SelectedValue));
        //if (dtRooms.Rows.Count > 0)
        //{
        //    RoomsDataList.DataSource = dtRooms;
        //    RoomsDataList.DataBind();
        //}
        //else
        //{
        //    RoomsDataList.DataSource = null;
        //    RoomsDataList.DataBind();
        //}

    }
   
    protected void BedDataList_EditCommand(object source, DataListCommandEventArgs e)
    {
        DataList BedDataList = e.Item.NamingContainer as DataList;
        //DataList BedDataList = e.Item.FindControl("BedDataList") as DataList;
        if (ViewState["Shift"].ToString() == "1")
        {
           

        }
        else
        {

           
        }

    }
    protected void BedDataList_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
        
            ImageButton btnFrontSheet = e.Item.FindControl("btnAdmit") as ImageButton;
            ImageButton btnBooked = e.Item.FindControl("btnBooked") as ImageButton;
            if (((HiddenField)e.Item.FindControl("hdnstatus")).Value == "Booked")
            {
                btnBooked.Visible = true;
                btnFrontSheet.Visible = false;

            }
            else
            {
                btnBooked.Visible = false;
                btnFrontSheet.Visible = true;
            }
            if (Convert.ToDateTime(todate.Text) < Convert.ToDateTime(Date.getdate().ToString("dd/MM/yyyy")))
            {
                btnBooked.Visible = true;
                btnFrontSheet.Visible = false;
            }
            if (Convert.ToDateTime(todate.Text) == Convert.ToDateTime(Date.getdate().ToString("dd/MM/yyyy")))
            {
                if (((HiddenField)e.Item.FindControl("hdnstatus")).Value == "Booked1")
                {
                    btnBooked.Visible = true;
                    btnFrontSheet.Visible = false;

                }
            }
            if (Convert.ToDateTime(todate.Text) > Convert.ToDateTime(Date.getdate().ToString("dd/MM/yyyy")))
            {
                if (((HiddenField)e.Item.FindControl("hdnstatus")).Value == "Booked1")
                {
                    btnBooked.Visible = false;
                    btnFrontSheet.Visible = true;

                }
            }
        }

    }

    protected void btnDischarge_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallConfirmBox", "CallConfirmBox();", true);


    }
    
    private void RegisterPostBackControl()
    {

        //foreach (DataListItem row in RoomsDataList.Items)
        //{
        //    DataList BedDataList = row.FindControl("BedDataList") as DataList;
            //foreach (DataListItem row1 in BedDataList.Items)
            //{
            //    ImageButton btnFrontSheet = row1.FindControl("btnFrontSheet") as ImageButton;
            //    ScriptManager.GetCurrent(this).RegisterPostBackControl(btnFrontSheet);
            //}
        //}
    }
    protected void BedDataList_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
           // DataList BedDataList = e.Item.NamingContainer as DataList;
            //ShiftingDetails.Visible = true;
            //ViewState["Shift"] = "1";
            //int BedId = Convert.ToInt32(BedDataList.DataKeys[e.Item.ItemIndex].ToString());
            string  SloatName1 = Convert.ToString(((Label)BedDataList.Items[e.Item.ItemIndex].FindControl("lblBedName")).Text);
            string SloatName = Convert.ToString(((HiddenField)BedDataList.Items[e.Item.ItemIndex].FindControl("hdnStartTime")).Value);
            string[] SloatNameN;
            SloatNameN = SloatName.Split(' ');
            //int IpdNo = Convert.ToInt32(((HiddenField)BedDataList.Items[e.Item.ItemIndex].FindControl("hdnIpdNo")).Value);
            string  Centername = Convert.ToString( ddlCenter.SelectedItem.Text);
            int CenterID =  Convert.ToInt32( ddlCenter.SelectedValue);
            string BDate = Convert.ToDateTime(todate.Text).ToString("dd/MM/yyyy");
            //ViewState["PatRegId"] = PatRegId;
            //ViewState["BedIdOld"] = BedId;
            //ViewState["IpdNo"] = IpdNo;
            Response.Redirect("~/BookAppoinment.aspx?SlotTime=" + SloatNameN[0] + "&Centername=" + Centername + "&BookDate=" + BDate + " &CenterID=" + CenterID + "", false);
            //Response.Redirect("~/PatientDischarge.aspx?PatRegID=" + PatRegId + "&IpdNo=" + IpdNo + "&FID=" + Convert.ToInt32(Session["FId"]) + "&BedId=" + BedId + "", false);

        }
       
    }

    [WebMethod]
    [ScriptMethod]
    public static string[] FillCenter(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"]);
        
            sda = new SqlDataAdapter("SELECT * FROM DrMT where DoctorName like '" + prefixText + "%' and DrType='CC' and cashbill=0  order by DoctorName", con);
        

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {
            tests.SetValue(dr["DoctorName"], i);
            i++;
        }
        return tests;
    }
  
  

    protected void btnSearch_Click(object sender, EventArgs e)
    {
       // sc.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime)).Value = dfrom.Date;
        dt = ObjApp.ReadDataSlot(ddlCenter.SelectedValue,todate.Text, "2020-09-01", "2020-09-01");
        BedDataList.DataSource = dt;
        BedDataList.DataBind();
       

    }
}