using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
//using Excel;
using System.Xml;

public static class ConnectionString
{

    private static string _Connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["myconnection"].ToString();
    private static string _PDConnectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["myconnection"].ToString();
    public static string Connectionstring
    {
        get
        {
            return _Connectionstring;
            
        }
        set
        {
            _Connectionstring = value;
        }
    }
    public static string PDConnectionstring
    {
        get
        {
            return _PDConnectionstring;

        }
        set
        {
            _PDConnectionstring = value;
        }
    }
    public static string ConnectionstringWithoutCheck
    {
        get
        {
            return _Connectionstring;
        }
        set
        {
            _Connectionstring = value;
        }
    }
}

