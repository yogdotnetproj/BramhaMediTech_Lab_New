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
using System.Data.SqlClient;
public partial class Login :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    DataTable dtderial = new DataTable();
    financialYear_b finyr = new financialYear_b();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            //checAuthincation();
        }

        txtUName.Focus();
        if (Convert.ToString( Request.QueryString["Activation"]) == "Yes")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Input string was not in correct format...');", true);
        }
         string Currentdate = Date.getdate().ToString("dd/MM/yyyy");
         if (Convert.ToDateTime(Currentdate) >= Convert.ToDateTime("06 /11/ 2025") && Convert.ToDateTime(Currentdate) < Convert.ToDateTime("26 /11/ 2025"))
         {
             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Input string was not in correct format....');", true);
         }
         //if (Convert.ToDateTime(Currentdate) >= Convert.ToDateTime("28 /04/ 2017") )
         //{
         //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
             
         //}
        //string BCount = Patmst_New_Bal_C.PatientCountBanner(1);
        //if (Convert.ToInt32(BCount) > 210 && Convert.ToInt32(BCount) < 310)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Software will expire few days,please contact to system administrator.');", true);
        //}
    }
    protected void btnChangepw_Click(object sender, EventArgs e)
    {
        // Response.Redirect("ChangePasswordNew.aspx");
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
       // PAtValidate();
        //    string Currentdate = Date.getdate().ToString("dd/MM/yyyy");
        //if (Convert.ToDateTime(Currentdate) >= Convert.ToDateTime("27 /11/ 2025"))
        //{
        //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Input string was not in correct format...');", true);
        //    //return;
        //}
      DataTable  dtban = ObjTB.Bindbanner();
      Session["Bannername"] = Convert.ToString(dtban.Rows[0]["BannerName"]).Trim();
      Session["BannerCode"] = Convert.ToString(dtban.Rows[0]["BannerName"]).Trim();

      //if (Convert.ToInt32(dtban.Rows[0]["ActivateSerialKey"]) == 0)
      //{
      //    return;
      //}
        bool finflag = true;
        if (txtUName.Text == "")
        {
            return;
        }
        bool login = false;
        Session["usertype"] = null;
        Session["username"] = null;
       
        Session["UnitCode"] = null;      

       
        try
        {
          //  Session["LoginTime"] = Date.getTime();
           // Session["LoginDate"] = Date.getdate().ToString("dd/MM/yyyy");
            createuserTable_Bal_C u = new createuserTable_Bal_C(txtUName.Text.Trim(), txtPassword.Text.Trim());
            Session["password"] = txtPassword.Text.Trim();
            Session["Branchid"] = u.P_branchid;
            Session["DRid"] = u.Drid;
           bool ISRegNoInt= createuserTable_Bal_C.Check_IsInterfaceRegNo(1);
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
                Session["DigModule"] ="0";
            }
            else
            {
                Session["DigModule"] = u.DigModule;
            }
            Session["Parameter"] = "NO";
          
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
                lblerrorLogin.Visible = true;
                lblerrorLogin.Text = "Username and password does not match.";
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
                    Session["CenterCode"] = DrMT_sign_Bal_C.getdefault_Collcenter(Convert.ToString(Session["UnitCode"] ), Convert.ToInt32(Session["Branchid"]), u.Username.Trim());
                    string ISCashbill = DrMT_sign_Bal_C.getcheckmonthlybill(Convert.ToString(Session["UnitCode"] ), Convert.ToInt32(Session["Branchid"]), u.Username.Trim());
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
                    Session["CenterCode"] = DrMT_sign_Bal_C.Get_CenterDefault(Convert.ToString(Session["UnitCode"] ), Convert.ToInt32(Session["Branchid"]));
                    Session["Monthlybill"] = "NO";
                }

            }
        }
        catch
        {
            lblerrorLogin.Visible = true;
            lblerrorLogin.Text = "Username and password does not match.";
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
                   
                    if (Session["usertype"].ToString() == "Administrator")
                    {
                        Server.Transfer("~/Home.aspx", true);
                    }
                    else
                    {
                        Server.Transfer("~/Home1.aspx", true);
                    }
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


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["usertype"] = null;
        Session["username"] = null;
        
        Session["UnitCode"] = null;

        txtUName.Text = "";
        txtPassword.Text = "";
    }


    public void checAuthincation()
    {

        string MenuSQL = "";
        DataTable MenuDt = new DataTable();
        MenuSQL = String.Format(@"select *, case when DateOfBirth is null then  convert(varchar, Patregdate, 112)*55 else convert(varchar, DateOfBirth, 112)*55 end as SerialKey from patmt  where (PPID = '200')  ");



        string connectionString1 = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString1);

        SqlCommand cmd = new SqlCommand(MenuSQL, con);

        SqlDataAdapter Adp = new SqlDataAdapter(cmd);

        Adp.Fill(MenuDt);
        if (MenuDt.Rows.Count != 0)
        {
            // Response.Redirect("Login.aspx", false);
            string macAddress = "";
            string mac_src = "";
            int SrLength = Convert.ToInt32(MenuDt.Rows[0]["SerialKey"].ToString().Length);
            mac_src = MenuDt.Rows[0]["SerialKey"].ToString();
            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }
            for (int i = 0; i < 12; i++)
            {
                if (0 == (i % 3))
                {
                    if (i == 0)
                    {
                        macAddress = macAddress.Insert(macAddress.Length, mac_src.Substring(i, 3)) + "-"; ;
                    }
                    else
                    {
                        macAddress = macAddress.Insert(macAddress.Length, mac_src.Substring(i, 3)) + "-";
                    }
                }
            }

            macAddress = macAddress.Remove(15);
            UpdateSerialKey(macAddress);
        }
        con.Close();
        con.Dispose();
    }
    public void PAtValidate()
    {
        DataTable dtLDP = new DataTable();
        dtLDP = ObjTB.Get_TotalPatientVal();
        if (dtLDP.Rows.Count > 0)
        {

        }
        else
        {
            string BCount = Patmst_New_Bal_C.PatientCountBanner(1);
            if (Convert.ToInt32(BCount) > 1111)
            {
               // Server.Transfer("~/Login.aspx", true);
            }
        }
    }

    public void UpdateSerialKey(string SerialKey)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc = new SqlCommand("" +
             "update BannerTable set SerialKEY='" + SerialKey + "' ", conn);




        conn.Open();
        sc.ExecuteNonQuery();
        conn.Close();
        conn.Dispose();
        dtderial = ObjTB.Get_SerialKey();

        if (Convert.ToInt32(dtderial.Rows[0]["Patientcount"]) > 4500)
        {
            if (Convert.ToString("001-111-583-385").Trim() == SerialKey.Trim())
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Contact to system administrator.');", true);
                // Label10.Text = AA;

                SqlConnection conn1 = DataAccess.ConInitForDC();
                SqlCommand sc1 = new SqlCommand();
                sc1 = new SqlCommand("" +
                     "update BannerTable set SerialKEY='" + SerialKey + "', ActivateSerialKey=1  ", conn1);




                conn1.Open();
                sc1.ExecuteNonQuery();
                conn1.Close();
                conn1.Dispose();
            }
            else
            {
                string NewSerial = " Your Serial Key:  " + SerialKey.ToString();
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + NewSerial.ToString() + "');</script>", false);
                if (dtderial.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtderial.Rows[0]["Patientcount"]) > 4500 && Convert.ToInt32(dtderial.Rows[0]["Patientcount"]) < 6500)
                    {
                        string NewSerialMsg = " Your license will expire soon.You need to activate software.Your Serial Key:  " + SerialKey.ToString();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + NewSerialMsg.ToString() + "');</script>", false);
                    }

                    if (Convert.ToInt32(dtderial.Rows[0]["Patientcount"]) > 7000)
                    {
                        SqlConnection conn1 = DataAccess.ConInitForDC();
                        SqlCommand sc1 = new SqlCommand();
                        sc1 = new SqlCommand("" +
                             "update BannerTable set SerialKEY='" + SerialKey + "', ActivateSerialKey=0  ", conn1);




                        conn1.Open();
                        sc1.ExecuteNonQuery();
                        conn1.Close();
                        conn1.Dispose();
                    }
                }

            }

        }
    }

}