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
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;

public partial class LabQRCode :BasePage
{
    Expence_Bal_C ObjEB = new Expence_Bal_C();
    DataTable dt = new DataTable();
    DataTable dtQ = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void txtbirthYear_TextChanged(object sender, EventArgs e)
    {
        if (txtbirthYear.Text.Trim() != "")
        {
            string RegNo = Convert.ToString(Request.QueryString["RegNo"]);
            string LabType = Convert.ToString(Request.QueryString["LabType"]);
           dt= ObjEB.ValidateBirthYear_Data(txtbirthYear.Text.Trim(), RegNo);
           if (dt.Rows.Count > 0)
           {
               dtQ = ObjEB.GetSMSString_CountryCode_Covid("URL", Convert.ToInt16(1), "");
             //  if (LabType == "P")
              // {
                   Response.Redirect(""+dtQ.Rows[0]["CountryCode"] + "UrlReport//" + dtQ.Rows[0]["smsString"] + RegNo.Trim() +".pdf",false);
               //}
               //if (LabType == "R")
               //{
               //    Response.Redirect("http://190.108.200.84:10/Laboratory/UrlReport//" + RegNo + ".pdf", false);
               //}
               //if (LabType == "M")
               //{
               //    Response.Redirect("http://190.108.200.84:10/Laboratory/UrlReport//" + RegNo + ".pdf", false);
               //}
           }
           else
           {
               txtbirthYear.BorderColor = System.Drawing.Color.Red;
               LblMsg.Text = "Enter valid year of Birth";
           }
        }
    }
}