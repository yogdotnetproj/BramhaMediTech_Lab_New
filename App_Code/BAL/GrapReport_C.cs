using System.Collections.Generic;
using System.Linq;
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
using System.Text.RegularExpressions;
public class GrapReport_C
{
    DataAccess data = new DataAccess();
    private int branchid; public int P_branchid { get { return branchid; } set { branchid = value; } }
	public GrapReport_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable GetDetailsToBindGraphForG()
    {
        SqlConnection con = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_phgsc", con);

        //  sc.Parameters.AddWithValue("@branchid", P_branchid);
        sc.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(sc);
        DataTable ds = new DataTable();
        con.Open();
        try
        {
            da.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close(); con.Dispose();
        }
        return ds;
    }
    public DataTable GetDetailsToBindGraphFor_Pcount()
    {
        SqlConnection con = data.ConInitForDC1();

        SqlCommand sc = new SqlCommand("SP_phpatinfoc", con);

        //  sc.Parameters.AddWithValue("@branchid", P_branchid);
        sc.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter da = new SqlDataAdapter(sc);
        DataTable ds = new DataTable();
        con.Open();
        try
        {
            da.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close(); con.Dispose();
        }
        return ds;
    }

    

}