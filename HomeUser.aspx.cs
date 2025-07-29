using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class HomeUser :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();

    financialYear_b finyr = new financialYear_b();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["era"] != null)
        {
            Session["username"] = Request.QueryString["era"].Trim();
            Session["Branchid"] = 1;
            Session["era"] = "YES";

            DataTable dtban = ObjTB.Bindbanner();
            Session["Bannername"] = Convert.ToString(dtban.Rows[0]["BannerName"]).Trim();
            Session["BannerCode"] = Convert.ToString(dtban.Rows[0]["BannerName"]).Trim();
            bool finflag = true;
            if (Session["username"] == "")
            {
                return;
            }
            bool login = false;
            Session["usertype"] = null;
            //Session["username"] = null;

            Session["UnitCode"] = null;


            try
            {
                //  Session["LoginTime"] = Date.getTime();
                // Session["LoginDate"] = Date.getdate().ToString("dd/MM/yyyy");
                createuserTable_Bal_C u = new createuserTable_Bal_C(Convert.ToString( Session["username"]),1,1,1);
                Session["password"] =u.Password;
                Session["Branchid"] = u.P_branchid;
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
                if (u.Usertype == "Administrator")
                {
                    Session["DigModule"] = "0";
                }
                else
                {
                    Session["DigModule"] = "0";
                }


                Session["Taxper"] = createuserlogic_Bal_C.Get_Taxper();
                finflag = finyr.isfinyrpresent(u.P_branchid);
                string FinYear = FinancialYearTableLogic.getCurrentFinancialYear(Convert.ToInt32(Session["Branchid"])).FinancialYearId;
                Session["financialyear"] = FinYear;//"11";

                if (u.Usertype == "Patient" || u.Usertype == "patient")
                {
                    Session["usertype"] = u.Usertype;
                    Session["username"] = u.Username;
                    Session["regno"] = u.UnitCode;
                    Response.Redirect("PatientReport.aspx");

                }


                if (u.Usertype == "CollectionCenter1" || u.Usertype.ToString() == "Collection Center1" || u.Usertype.ToString() == "Collection  Center")
                {


                    Session["usertype"] = u.Usertype;
                    Session["username"] = u.Username.Trim();

                    try
                    {
                        if (u.UnitCode != "" && u.UnitCode != "0")
                        {
                            string drname = "AAA";
                            DrMT_Bal_C dr = new DrMT_Bal_C(drname, "CC", Convert.ToInt32(Session["Branchid"]));

                        }
                    }
                    catch
                    {
                        Session["UnitCode"] = null;
                    }
                    login = true;

                }

                else if (u.Usertype == "")
                {
                   
                    return;
                }
                else
                {
                    //if (u.AFlag == true)
                    // {
                    login = true;
                    // }
                    Session["usertype"] = u.Usertype;
                    Session["username"] = u.Username.Trim();
                    if (u.UnitCode != "" && u.UnitCode != "0")
                    {
                        Session["UnitCode"] = u.UnitCode;
                    }
                    if (u.Usertype == "CollectionCenter" || u.Usertype.ToString() == "Collection Center" || u.Usertype.ToString() == "CollectionCenter")
                    {
                        Session["CenterCode"] = DrMT_sign_Bal_C.getdefault_Collcenter(Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]), u.Username.Trim());
                        string ISCashbill = DrMT_sign_Bal_C.getcheckmonthlybill(Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]), u.Username.Trim());
                        if (ISCashbill == "False")
                        {
                            Session["Monthlybill"] = "YES";

                        }
                        else
                        {
                            Session["Monthlybill"] = "No";
                        }

                    }
                    else
                    {
                        Session["CenterCode"] = DrMT_sign_Bal_C.Get_CenterDefault(Convert.ToString(Session["UnitCode"]), Convert.ToInt32(Session["Branchid"]));
                        Session["Monthlybill"] = "NO";
                    }

                }
            }
            catch
            {
              
                return;
            }
            if (login)
            {
                if (!Uniquemethod_Bal_C.isSessionSet())
                {
                    return;
                }
                if (Session["usertype"].ToString() == "patient")
                {

                    Response.Redirect("PatientReport.aspx");

                }

                else if (Session["usertype"].ToString() == "Administrator")
                {
                    if (finflag == false)
                    {
                        Server.Transfer("AddFinanceYear.aspx");
                    }
                    else
                    {

                        //if (Session["usertype"].ToString() == "Administrator")
                        //{
                        //    Server.Transfer("~/Home.aspx", true);
                        //}
                        //else
                        //{
                        //    Server.Transfer("~/Home1.aspx", true);
                        //}
                    }
                }
                else
                {
                    //Server.Transfer("~/Home.aspx", true);
                    if (finflag == false)
                    {
                        Server.Transfer("AddFinanceYear.aspx");
                    }
                    else
                    {
                        Server.Transfer("~/Home1.aspx", true);

                    }
                }
            }




        }
        
        
       
    }
}