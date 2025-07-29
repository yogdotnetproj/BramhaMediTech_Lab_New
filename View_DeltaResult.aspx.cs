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

public partial class View_DeltaResult :BasePage
{
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string selectonFormula = "", regno = "", FID = "";
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DescRept();
            
        }
    }
    public void DescRept()
    {

        ReportDocument CR = new ReportDocument();

        SqlConnection con = DataAccess.ConInitForDC();

        SqlDataAdapter sda1 = null;
        DataTable dt1 = new DataTable();


        sda1 = new SqlDataAdapter("select * from VW_DeltaResult  ", con);//where PatRegID='" + Request.QueryString["$"].ToString() + "'  and FID='" + Request.QueryString["_"] + "'
        sda1.Fill(dt1);
        if (dt1.Rows.Count != 0)
        {

            CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            string formula = "", formula1 = "";
            selectonFormula = ReportParameterClass.SelectionFormula;
           
            CR.Load(Server.MapPath("~//DiagnosticReport//Rpt_DeltaResult.rpt"));

            Uniquemethod_Bal_C cl = new Uniquemethod_Bal_C();

            CR.SetDataSource((System.Data.DataTable)dt1);
            string path = Server.MapPath("/" + Request.ApplicationPath + "/PrintReport/");
            string filename1 = Server.MapPath("PrintReport//" + Convert.ToInt32(Session["Branchid"]) + "_" + "DeltaReport" + ".pdf");
            System.IO.File.WriteAllText(filename1, "");
            string exportedpath = "", selectionFormula = "";
            cl.ExportandPrintr("pdf", path, exportedpath, formula, CR);

            CR.Close();
            CR.Dispose();
            GC.Collect();


            Td1.Visible = true;
            lblmsg.Text = "";
        }
        else

        {
            Td1.Visible = false;
            lblmsg.Text = "Report Not Generated, Please Generate Once Again ";

        }

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