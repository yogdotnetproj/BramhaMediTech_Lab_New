using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for DBFunctions
/// </summary>
public class DBFunctions
{
	public DBFunctions()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public SqlDataSource GetSqlDataSource()
    {        
        SqlDataSource sqlSource = new SqlDataSource();
        sqlSource.ConnectionString = ConfigurationManager.ConnectionStrings["myconnection"].ToString();
        sqlSource.ProviderName = "System.Data.SqlClient";
        return sqlSource;

    }
}
