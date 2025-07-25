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
using System.Data.SqlClient;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Web.Management;


public partial class AppoinmentBookedPatient : System.Web.UI.Page
{
    int g;
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string Date2 = DateTime.Now.AddDays(-1).ToString("ddMMyyyy");
    string rptname = "", path = "", selectonFormula = "";
    Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();
    Patmst_New_Bal_C PatNBC = new Patmst_New_Bal_C();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    Appointment_C ObjApp = new Appointment_C();
   

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
            todate.Text = Date.getdate().ToString("dd/MM/yyyy");
           

          
            PatientOnline_Report();
           

        }
    }
    public void PatientOnline_Report()
    {
        string Call = "", CenterCode = ""; ;
        DataTable dton = new DataTable();
        if (RblCallStatus.SelectedItem.Text == "Done")
        {
            Call = "Done";
        }       
        else
        {
            Call = "NotDone";
        }
        if (Session["usertype"].ToString().Trim() == "CollectionCenter")
        {
            createuserTable_Bal_C ui = new createuserTable_Bal_C(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
            createuserTable_Bal_C uii = new createuserTable_Bal_C(ui.CenterCode.Trim());
            CenterCode = (uii.CenterCode.Trim());
           // ddlCenter.SelectedValue = Convert.ToString(uii.Drid);
           
        }
        dton = ObjApp.Get_Patient_Appointmentt(Txt_Patientname.Text, fromdate.Text, todate.Text, Call, CenterCode,txtappNo.Text);        
            GVTestentry.DataSource = dton;
            GVTestentry.DataBind();
        

    }
  


  
    protected void GVTestentry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVTestentry.PageIndex = e.NewPageIndex;
        PatientOnline_Report();
    }
    protected void GVTestentry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;
       bool Mailstatus = Convert.ToBoolean(e.Row.Cells[10].Text);
       if (Mailstatus == false)
       {
           e.Row.Cells[10].Text = "<span class='btn btn-xs btn-danger' >NA</span>";

       }
       else
       {
           e.Row.Cells[10].Text = "<span class='btn btn-xs btn-success' >Done</span>";
       }
        //else if (Mailstatus == "1")
        //{
        //    e.Row.Cells[10].Text = "<span  class='badge' >Viber</span>";

        //}
        //else
        //{
        //    e.Row.Cells[10].Text = "<span class='btn btn-xs btn-success' >Whatsapp</span>";
        //}
    }
    protected void GVTestentry_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (e.NewEditIndex == -1)
                return;
            int i = e.NewEditIndex;
           
             int Id =  Convert.ToInt32( GVTestentry.DataKeys[e.NewEditIndex].Value.ToString());
             ObjApp.AppId = Id;
             //ObjApp.Attend_RegisterAppoinment(1);
             int Appid = ObjApp.Attend_RegisterAppoinment(1);
             Label1.Text = "Appoinment Save Successfully..! Your Permanant Id is :-" + Appid + ".";
            try
            {
               
                 PatientOnline_Report();
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
    protected void GVTestentry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void btnclick_Click(object sender, EventArgs e)
    {
        PatientOnline_Report();
    }
}