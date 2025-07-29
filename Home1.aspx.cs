using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home1 :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToString(Request.QueryString["HMS"]) == "Yes")
        {
            // Username = Convert.ToString(Request.QueryString["username"]);
            Session["username"] = Convert.ToString(Request.QueryString["username"]).Trim();
            Session["usertype"] = Convert.ToString(Request.QueryString["UserType"]).Trim();
           Session["LabName"] = Convert.ToString(Request.QueryString["LabName"]).Trim();
            Session["BranchId"] = 1;
            Session["FId"] = 1;
            Session["HMS"] = "Yes";
        }
        if (Request.QueryString["UserType"] != null)
        {
            Session["UserType"] = Request.QueryString["UserType"].Trim();

        }
        if (Request.QueryString["Password"] != null)
        {
            Session["password"] = Request.QueryString["Password"].Trim();

        }
        if (Request.QueryString["FID"] != null)
        {
            Session["FID"] = Request.QueryString["FID"].Trim();

        }
        if (Request.QueryString["username"] != null)
        {
            Session["username"] = Request.QueryString["username"].Trim();

        }
        bool ISRegNoInt = createuserTable_Bal_C.Check_IsInterfaceRegNo(1);
        if (ISRegNoInt == true)
        {
            Session["ISRegNoInt"] = "YES";
        }
        else
        {
            Session["ISRegNoInt"] = "No";
        }
        bool ISPhlebotomist = createuserTable_Bal_C.Get_Phlebotomist_Required(1);
        if (ISPhlebotomist == true)
        {
            Session["Phlebotomist"] = "YES";
        }
        else
        {
            Session["Phlebotomist"] = "No";
        }
        bool ISDemography = createuserTable_Bal_C.Get_Demography_Required(1);
        if (ISDemography == true)
        {
            Session["ISDemography"] = "YES";
        }
        else
        {
            Session["ISDemography"] = "No";
        }
        Session["Branchid"] =1;
        Session["UnitCode"] = "";
        if (Convert.ToString( Session["usertype"]) == "Administrator")
        {
            Session["DigModule"] = "0";
        }
        else
        {
          
        }
        if (Convert.ToString(Request.QueryString["HMS"]) == "Yes")
        {
            Session["DigModule"] = 1;
            Session["Parameter"] = "NO";
            Session["CenterCode"] = DrMT_sign_Bal_C.Get_CenterDefault("", Convert.ToInt32(Session["Branchid"]));
            Session["Monthlybill"] = "NO";
        }
    }
}