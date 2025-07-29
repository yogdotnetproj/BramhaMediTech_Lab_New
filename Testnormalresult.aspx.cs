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
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class Testnormalresult :BasePage
{
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string selectonFormula = "", regno = "", FID = "";
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //DescRept();
            string RepName = Request.QueryString["RepName"].ToString();
            Response.Redirect(RepName);
        }
    }
    public void DescRept()
    {

        ReportDocument CR = new ReportDocument();

        SqlConnection con = DataAccess.ConInitForDC();

        SqlDataAdapter sda1 = null;
        DataTable dt1 = new DataTable();
       
       
        sda1 = new SqlDataAdapter("select * from VW_desfiledata_org where PatRegID='" + Request.QueryString["$"].ToString() + "'  and FID='" + Request.QueryString["_"] + "' ", con);
        sda1.Fill(dt1);
        if (dt1.Rows.Count != 0)
        {
            //if (Convert.ToString( Request.QueryString["rtt"]) == "Lhead")
            //{
            //    CR.Load(Server.MapPath("~/Pateintreportnondescriptive_email.rpt"));
            //}
            //else
            //{
            //    CR.Load(Server.MapPath("~/Pateintreportnondescriptive.rpt"));
            //}
            CR.Load(Server.MapPath("~/Pateintreportnondescriptive.rpt"));
            CR.SetDataSource(dt1);
            CVTest.ReportSource = CR;
            Td1.Visible = true;
            lblmsg.Text = "";
        }
        else
        {
            Td1.Visible = false;
            lblmsg.Text = "Report Not Generated, Please Generate Once Again ";

        }     
      
        Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();
        string filename = Server.MapPath("rpts");
      
    }
    protected void CVTest_Init(object sender, EventArgs e)
    {
        // DescRept();
    }
    protected void CVTest_PreRender(object sender, EventArgs e)
    {
        DescRept();
    }
}