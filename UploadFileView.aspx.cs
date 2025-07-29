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

public partial class UploadFileView :BasePage
{
    string MTCode;
    Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
    DataTable dt = new DataTable();
    PatSt_Bal_C psnew = new PatSt_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Fill_Labels();

            Hyp_viewPres.NavigateUrl = Request.QueryString["UploaPres"].ToString();

        }
    }
    public void Fill_Labels()
    {
        #region  Patient info
        Patmst_Bal_C CIT = null;
        try
        {

            CIT = new Patmst_Bal_C(Request.QueryString["PatRegID"], Convert.ToString(18), Convert.ToInt32(Session["Branchid"]));

            lblRegNo.Text = Convert.ToString(CIT.PatRegID);

            ViewState["PID"] = CIT.PID;
            lblName.Text = CIT.Initial.Trim() + "." + CIT.Patname;
            lblage.Text = Convert.ToString(CIT.Age) + "/" + CIT.MYD;
            lblSex.Text = CIT.Sex;

            LblMobileno.Text = CIT.Phone;
            Lblcenter.Text = CIT.CenterName;
            lbldate.Text = Convert.ToString(CIT.Patregdate);
            LblRefDoc.Text = CIT.Drname;

        }
        catch
        {
            lblRegNo.Visible = true;
            lblRegNo.Text = "Record not found";
        }
        #endregion
    }

}