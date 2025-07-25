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


public class main_Bal_C
{
	public main_Bal_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataSet fillddlmaindept()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select deptid,deptname from maindepartment order by deptname", con);
        DataSet ds = new DataSet();
        try
        {
            sda.Fill(ds);

        }
        catch
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }
}
