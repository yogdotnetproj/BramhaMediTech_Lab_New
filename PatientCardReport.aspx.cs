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


public partial class PatientCardReport :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    string labcode_main = "";
    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["HMS"]) != "Yes")
                {
                    if (Convert.ToString(Session["usertype"]) != "Administrator")
                    {
                        checkexistpageright("PatientCardReport.aspx");
                    }
                }
                //FullyQualifiedApplicationPath();
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


  

   




    protected void btnreport1_Click(object sender, EventArgs e)
    {
        string sql1 = "";
        SqlConnection conn1 = DataAccess.ConInitForDC();
        SqlCommand sc1 = conn1.CreateCommand();
        Patmst_Bal_C PBC = new Patmst_Bal_C();
       
       

        //sc1.CommandText = "ALTER VIEW [dbo].[VW_PatientCard] AS SELECT top(1) percent  dbo.Cshmst.BillNo, dbo.Cshmst.RecDate, dbo.Cshmst.BillType,   dbo.Cshmst.AmtReceived, "+
        //       " dbo.Cshmst.Discount, dbo.Cshmst.NetPayment, RecM.AmtPaid AS AmtPaid, dbo.Cshmst.Balance,  dbo.Cshmst.username,    "+
        //       " dbo.Cshmst.OtherCharges,dbo.patmst.PatRegID, dbo.patmst.intial, dbo.patmst.Patname,   dbo.patmst.sex,  "+
        //       " dbo.patmst.Age,   dbo.patmst.Drname,  dbo.patmst.TelNo, dbo.DrMT.DoctorCode, dbo.DrMT.DoctorName, "+
        //       " dbo.MainTest.Maintestname,      dbo.MainTest.MTCode,  dbo.patmstd.TestRate, dbo.PackMst.PackageName, "+
        //       " dbo.patmstd.PackageCode, dbo.Cshmst.DisFlag,   dbo.patmst.Patusername, dbo.patmst.Patpassword, dbo.Cshmst.Comment, "+
        //       " dbo.patmst.MDY,dbo.patmst.Remark AS PatientRemark,   dbo.patmst.Pataddress,dbo.patmst.PPID ,dbo.patmst.UnitCode , "+
        //       " Cshmst.TaxAmount, Cshmst.TaxPer,     RecM.PrintCount as PrintCount ,patmst.Email as EmailID " +
        //       " FROM         patmst INNER JOIN    DrMT ON patmst.CenterCode = DrMT.DoctorCode AND     patmst.Branchid = DrMT.Branchid "+
        //       " INNER JOIN   Cshmst INNER JOIN   MainTest INNER JOIN   patmstd ON    MainTest.MTCode = patmstd.MTCode AND "+
        //       " MainTest.Branchid = patmstd.Branchid ON Cshmst.PID = patmstd.PID AND     Cshmst.Branchid = patmstd.Branchid ON "+
        //       " patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid    INNER JOIN "+
        //       " RecM ON Cshmst.PID = RecM.PID AND Cshmst.BillNo = RecM.BillNo LEFT OUTER JOIN  "+    
        //       " PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode where  patmst.branchid=" + Session["Branchid"].ToString() + " and patmst.PPID='" + ViewState["PPID"] + "'  order by Cshmst.billno desc  ";// and Cshmst.BillNo=" + bno + " DrMT.DrCheck_flag='CC' and

        sc1.CommandText = "ALTER VIEW [dbo].[VW_PatientCard] AS SELECT        TOP (99.99) PERCENT RecM.BillNo, dbo.RecM.billdate as RecDate, dbo.RecM.PaymentType as BillType, RecM.AmtPaid AS AmtReceived, RecM.DisAmt AS Discount, dbo.patmst.TestCharges AS NetPayment, RecM.AmtPaid, RecM.BalAmt AS Balance, " +
                        "  RecM.username, RecM.OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.Drname, patmst.TelNo, DrMT.DoctorCode, DrMT.DoctorName, MainTest.Maintestname, MainTest.MTCode, " +
                        "  patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag, patmst.Patusername, patmst.Patpassword, RecM.Comment, patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, " +
                        "  patmst.UnitCode, RecM.TaxAmount, RecM.TaxPer, RecM.PrintCount, patmst.Email AS EmailID " +
                        "  FROM            RecM INNER JOIN " +
                        "  patmst INNER JOIN " +
                        "  DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN " +
                        "  MainTest INNER JOIN " +
                        "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid ON RecM.PID = patmst.PID AND " +
                        "  RecM.branchid = patmst.Branchid LEFT OUTER JOIN " +
                        "  PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode where  patmst.branchid=" + Session["Branchid"].ToString() + " and patmst.PPID='" + ViewState["PPID"] + "'  order by RecM.billno desc  ";// and Cshmst.BillNo=" + bno + " DrMT.DrCheck_flag='CC' and



        

        conn1.Open();
        sc1.ExecuteNonQuery();
        conn1.Close(); conn1.Dispose();

        Session.Add("rptsql", sql1);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PatientCard.rpt");
        Session["reportname"] = "PatientCard";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql1;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);

    }
    [WebMethod]
    [ScriptMethod]
    public static string[] GetPatientName(string prefixText, int count)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = null;
        DataTable dt = new DataTable();
        int branchid = Convert.ToInt32(HttpContext.Current.Session["Branchid"]);
        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        if (labcode != null && labcode != "")
        {
            sda = new SqlDataAdapter("SELECT * FROM patmst where (Patname like '%" + prefixText + "%' )  or (PPID like N'%" + prefixText + "%')  and branchid=" + branchid + " order by Patname ", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT * FROM patmst where (Patname like '%" + prefixText + "%' ) or (PPID like N'%" + prefixText + "%')  and branchid=" + branchid + " order by Patname ", con);
        }

        sda.Fill(dt);
        string[] tests = new String[dt.Rows.Count + 1];
        int i = 0;
        tests.SetValue("All", i); i = i + 1;
        foreach (DataRow dr in dt.Rows)
        {

            tests.SetValue(dr["Patname"] + " = " + dr["PPID"], i);
            i++;
        }
        return tests;
    }
    protected void txtPatientname_TextChanged(object sender, EventArgs e)
    {
        if (txtPatientname.Text != "")
        {
            string[] PatientMNo = txtPatientname.Text.Split('=');
            if (PatientMNo.Length > 1)
            {
                string PermanentID = PatientMNo[1].ToString().Trim();
                ViewState["PPID"] = PatientMNo[1].ToString().Trim();
            }
        }
    }

    public void  FullyQualifiedApplicationPath()
    {
        //get
        //{
            //Return variable declaration
            var appPath = string.Empty;

            //Getting the current context of HTTP request
            var context = HttpContext.Current;
            string MyUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            //Checking the current context content
            if (context != null)
            {
                //Formatting the fully qualified website url/name
                appPath = string.Format("{0}://{1}{2}{3}",
                                        context.Request.Url.Scheme,
                                        context.Request.Url.Host,
                                        context.Request.Url.Port == 80
                                            ? string.Empty
                                            : ":" + context.Request.Url.Port,
                                        context.Request.ApplicationPath);
            }

            if (!appPath.EndsWith("/"))
                appPath += "/";

            //return appPath;
       // }
    }
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

}