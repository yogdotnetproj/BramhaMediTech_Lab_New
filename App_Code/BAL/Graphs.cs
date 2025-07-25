using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

/// <summary>
/// Summary description for Graphs
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class Graphs : System.Web.Services.WebService
{
    Dictionary<int, string> monthsList = new Dictionary<int, string>()
    {
        {1,"Jan" }, {2,"Feb" }, {3,"Mar" }, {4,"Apr" }, {5,"May" }, {6,"Jun" },
        {7,"Jul" }, {8,"Aug" }, {9,"Sep" }, {10,"Oct" },{11,"Nov" },{12,"Dec" },
    };

    public Graphs()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public void Get_TotalNoOfPatients_Month(string month, string year, string Center)
    {
        List<cls_1> list = new List<cls_1>();

        DataTable dt = GetDataFromDataTable5("usp_Graph2_NoOfPatients", month, year, Center);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cls_1 obj = new cls_1();
                obj.RegDate = dt.Rows[i]["RegDate"].ToString();
                obj.NoOfPatients = dt.Rows[i]["NoOfPatients"].ToString();
                list.Add(obj);
            }
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        Context.Response.Write(js.Serialize(list));

    }

    [WebMethod]
    public void Get_TotalAmountReceived()
    {
        List<cls_3> lists = new List<cls_3>();

        DataTable dt = GetDataFromDataTable("usp_Graph1_AmountCollected");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lists.Add(new cls_3()
                {
                    month = dt.Rows[i]["MonthName"].ToString(),
                    cash = dt.Rows[i]["Cash"].ToString(),
                    card = dt.Rows[i]["Card"].ToString(),
                    insurance = dt.Rows[i]["Insurance"].ToString(),
                });
            }
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        Context.Response.Write(js.Serialize(lists));
    }

    [WebMethod]
    public void Get_TotalPatientsByDept(string month, string year, string center)
    {
        List<cls_1> lists = new List<cls_1>();

        DataTable dt = GetDataFromDataTable4("usp_Graph1_NoOfPatients_ByDept", Convert.ToInt32(month), Convert.ToInt32(year), center);
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lists.Add(new cls_1()
                {
                    RegDate = dt.Rows[i]["Dept"].ToString(),
                    NoOfPatients = dt.Rows[i]["Count"].ToString(),
                });
            }
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        Context.Response.Write(js.Serialize(lists));
    }

    [WebMethod]
    public void Get_Top5DoctorsByPatientRefered(string month, string year, string center)
    {
        List<cls_1> lists = new List<cls_1>();

        DataTable dt = GetDataFromDataTable4("usp_GetTop5DoctorsByPatientRefered",
            Convert.ToInt32(month), Convert.ToInt32(year), center);

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                lists.Add(new cls_1()
                {
                    RegDate = dt.Rows[i]["DoctorName"].ToString(),
                    NoOfPatients = dt.Rows[i]["PatientCount"].ToString(),
                });
            }
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        Context.Response.Write(js.Serialize(lists));
    }

    [WebMethod]
    public void Get_TestStatus()
    {
        List<cls_2> lists = new List<cls_2>();

        AdminSettings_C obj = new AdminSettings_C();

        DataTable dt = obj.Get_TestStatus_Details();

        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string colors = "grey";
                switch (dt.Rows[i]["TestStatus"].ToString())
                {
                    case "Registered":
                        colors = "blueviolet";
                        break;
                    case "Partial Authorized":
                        colors = "olive";
                        break;
                    case "Authorized":
                        colors = "chartreuse";
                        break;
                    case "Tested":
                        colors = "gold";
                        break;
                    case "Completed":
                        colors = "forestgreen";
                        break;
                }
                lists.Add(new cls_2()
                {
                    RegId = dt.Rows[i]["PatRegID"].ToString(),
                    PatientName = string.Join("", dt.Rows[i]["PatientName"].ToString().Take(15)),
                    Status = dt.Rows[i]["TestStatus"].ToString(),
                    IsEmergency = dt.Rows[i]["Isemergency"].ToString(),
                    Color = colors
                });
            }
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        Context.Response.Write(js.Serialize(lists));
    }

    public DataTable GetDataFromDataTable(string procName)
    {
        using (SqlConnection conn = DataAccess.ConInitForDC())
        {
            using (SqlCommand cmd = new SqlCommand(procName))
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        try
                        {
                            sda.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }

                        return dt;
                    }
                }
            }
        }
    }

    public DataTable GetDataFromDataTable4(string procName, int month, int year, string center = null)
    {
        using (SqlConnection conn = DataAccess.ConInitForDC())
        {
            using (SqlCommand cmd = new SqlCommand(procName))
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("month", month);
                cmd.Parameters.AddWithValue("year", year);
                cmd.Parameters.AddWithValue("center", center);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        try
                        {
                            sda.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }

                        return dt;
                    }
                }
            }
        }
    }

    public DataTable GetDataFromDataTable5(string procName, string month, string year, string Center)
    {
        using (SqlConnection conn = DataAccess.ConInitForDC())
        {
            using (SqlCommand cmd = new SqlCommand(procName))
            {
                cmd.Connection = conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("StartDate", month);
                cmd.Parameters.AddWithValue("EndDate", year);
                cmd.Parameters.AddWithValue("Center", Center);

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        try
                        {
                            sda.Fill(dt);
                        }
                        catch (Exception ex)
                        {
                            ex.Message.ToString();
                        }

                        return dt;
                    }
                }
            }
        }
    }

    [WebMethod(EnableSession = true)]
    public void MenusBind()
    {
        DataTable dt = new DataTable();
        TreeviewBind_C ObjTB = new TreeviewBind_C();

        List<Menus> lists = new List<Menus>();
        List<ChildMenus> childLists = null;
        if (Convert.ToString(Session["era"]) != "YES")
        {
            dt = ObjTB.BindMainMenu(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
        }
        else
        {
            dt = ObjTB.BindMainMenuHMS(Convert.ToString(Session["username"]), Convert.ToString(Session["password"]));
        }
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Menus obMain = new Menus();

                obMain.MenuId = dt.Rows[i]["MenuID"].ToString();
                obMain.MenuName = dt.Rows[i]["MenuName"].ToString();

                if (!string.IsNullOrEmpty(dt.Rows[i]["MenuID"].ToString()))
                {
                    DataTable dtchild = new DataTable();
                    if (Convert.ToString(Session["era"]) != "YES")
                    {
                        dtchild = ObjTB.BindChildMenu(dt.Rows[i]["MenuID"].ToString(), Convert.ToString(Session["username"]),
                            Convert.ToString(Session["password"]));
                    }
                    else
                    {
                        dtchild = ObjTB.BindChildMenu_HMS(dt.Rows[i]["MenuID"].ToString(), Convert.ToString(Session["username"]),
                            Convert.ToString(Session["password"]));
                    }

                    if (dtchild.Rows.Count > 0)
                    {
                        childLists = new List<ChildMenus>();
                        for (int j = 0; j < dtchild.Rows.Count; j++)
                        {
                            ChildMenus obChild = new ChildMenus();

                            obChild.ChildMenuId = dtchild.Rows[j]["MenuID"].ToString();
                            obChild.ChildMenuName = dtchild.Rows[j]["MenuName"].ToString();

                            DataTable dtform = new DataTable();
                            if (Convert.ToString(Session["era"]) != "YES")
                            {
                                dtform = ObjTB.BindForm(Convert.ToInt32(obChild.ChildMenuId));
                            }
                            else
                            {
                                dtform = ObjTB.BindForm_HMS(Convert.ToInt32(obChild.ChildMenuId));
                            }

                            if (dtform.Rows.Count > 0)
                            {
                                for (int k = 0; k < dtform.Rows.Count; k++)
                                {
                                    obChild.Url = dtform.Rows[k]["SubMenuNavigateURL"].ToString().Remove(0, 2);
                                }
                            }
                            childLists.Add(obChild);
                        }
                    }
                }
                obMain.ChildMenus = childLists;
                lists.Add(obMain);
            }
        }
        JavaScriptSerializer js = new JavaScriptSerializer();
        Context.Response.Write(js.Serialize(lists));
    }

    [WebMethod]
    public string[] AutoCompleteAjaxRequest(string prefixText, int count)
    {
        List<string> ajaxDataCollection = new List<string>();
        DataTable _objdt = new DataTable();
        _objdt = GetDataFromDataBase(prefixText);
        if (_objdt.Rows.Count > 0)
        {
            for (int i = 0; i < _objdt.Rows.Count; i++)
            {
                ajaxDataCollection.Add(_objdt.Rows[i]["Description"].ToString());
            }
        }
        return ajaxDataCollection.ToArray();
    }
    ///
    /// Function for retriving data from database
    ///

    ///
    public DataTable GetDataFromDataBase(string prefixText)
    {
        TreeviewBind_C ObjTB = new TreeviewBind_C();
        //string connectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\bookstore.mdb;Persist Security Info=False;";
        //conn.ConnectionString = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        DataTable _objdt = new DataTable();
        //string querystring = "select * from ProLanguage where LanguageName like '%" + prefixText + "%';";
        //OleDbConnection _objcon = new OleDbConnection(connectionstring);
        //OleDbDataAdapter _objda = new OleDbDataAdapter(querystring, _objcon);

        _objdt = ObjTB.BindShortCut(Convert.ToString(prefixText), Convert.ToString(""));
        //_objcon.Open();
        // _objda.Fill(_objdt);
        return _objdt;
    }


    class Menus
    {
        public string MenuId { get; set; }
        public string MenuName { get; set; }
        public List<ChildMenus> ChildMenus { get; set; }
    }

    class ChildMenus
    {
        public string ChildMenuId { get; set; }
        public string ChildMenuName { get; set; }
        public string Url { get; set; }

    }

    class cls_1
    {
        public string RegDate { get; set; }
        public string NoOfPatients { get; set; }
    }

    class cls_2
    {
        public string RegId { get; set; }
        public string PatientName { get; set; }
        public string Status { get; set; }
        public string IsEmergency { get; set; }
        public string Color { get; set; }
    }

    class cls_3
    {
        public string month { get; set; }
        public string cash { get; set; }
        public string card { get; set; }
        public string insurance { get; set; }

    }

}

static class StringExtensions
{

}
