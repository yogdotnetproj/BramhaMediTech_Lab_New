using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReportTransfer : System.Web.UI.Page
{
    string Date1 = DateTime.Now.ToString("ddMMyyyy");
    string Date2 = DateTime.Now.AddDays(-1).ToString("ddMMyyyy");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["PID"] != null && Request.QueryString["Type"] == "Rep")
            {
                Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Convert.ToString(Request.QueryString["PID"]) + "Report" + ".pdf", false);
            }
            if (Request.QueryString["TypeB"] == "BarCode")
            {
                //Response.Redirect(Convert.ToString(Request.QueryString["BarCodeRep"]), false);
                Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Convert.ToString(Request.QueryString["PID"]) + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "BarCode" + ".pdf", false);
               // "PrintReport//" + "$" + Date1 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "BarCode" + ".pdf"
            }
            if (Request.QueryString["TypeB"] == "DeptBarCode")
            {
                //Response.Redirect(Convert.ToString(Request.QueryString["BarCodeRep"]), false);
                Response.Redirect("PrintReport//" + "$" + Date1 + "$" + Convert.ToString(Request.QueryString["PID"]) + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "DeptBarCode" + ".pdf", false);
                // "PrintReport//" + "$" + Date1 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "BarCode" + ".pdf"
            }
            if (Request.QueryString["TypeB"] == "PayReceipt")
            {
                //Response.Redirect(Convert.ToString(Request.QueryString["BarCodeRep"]), false);
                Response.Redirect("PayReceipt//" + "$" + Date1 + "$" + Convert.ToString(Request.QueryString["PID"]) + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "PayReceipt" + ".pdf", false);
                // "PrintReport//" + "$" + Date1 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "BarCode" + ".pdf"
            }

            if (Request.QueryString["TypeB"] == "CenterLedger_Client")
            {
                //Response.Redirect(Convert.ToString(Request.QueryString["BarCodeRep"]), false);
                Response.Redirect("PrintReport//" + "$" + Date1 + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "CenterLedger_Client" + ".pdf", false);
                // "PrintReport//" + "$" + Date1 + "$" + PID + "_" + Convert.ToInt32(Session["Branchid"]) + "_" + "BarCode" + ".pdf"
            }
            
        }
    }
}