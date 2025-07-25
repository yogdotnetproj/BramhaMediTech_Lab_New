using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;


public class DataAccess
{

    public static SqlConnection ConInitForHMS()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["HMSDB"].ConnectionString);
        return con;
    }
    public  SqlConnection ConInitForDC1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
        return con;    
    }
    public static SqlConnection ConInitForDC()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
        return con;

    }
    public static SqlConnection ConOutsource1()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Outsource1"].ConnectionString);
        return con;
    }
    public static SqlConnection ConOutsource2()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Outsource2"].ConnectionString);
        return con;
    }
    public static SqlConnection ConInitForHM()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBHospitalI"].ConnectionString);
        return con;

    }
    public static SqlConnection ConOutsource3()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Outsource3"].ConnectionString);
        return con;
    }
    public static SqlConnection ConOutsource4()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Outsource4"].ConnectionString);
        return con;
    }
    public static SqlConnection ConOutsource5()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Outsource5"].ConnectionString);
        return con;
    }
   
}
