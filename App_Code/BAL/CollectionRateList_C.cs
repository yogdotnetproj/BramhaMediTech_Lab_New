using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
public class CollectionRateList_C
{
	public CollectionRateList_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable Getdrname()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = "";
       
        SqlDataAdapter da = new SqlDataAdapter("select distinct RatID,RateName from RatT", con);
        DataTable ds = new DataTable();
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
      public  DataTable GetdrnameList(string drname)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "";
        if (drname == "--Select--")
        {
             query = "SELECT DISTINCT "+
               " RatT.RatID, RatT.RateName, TestCharges.STCODE, TestCharges.Amount, TestCharges.Percentage, TestCharges.Emergency, TestCharges.username, "+
               " MainTest.Sampletype, MainTest.samecontain, MainTest.TatDuration, MainTest.Maintestname "+
               " FROM         TestCharges INNER JOIN "+
               " RatT ON TestCharges.DrCode = RatT.RatID INNER JOIN "+
               " MainTest ON TestCharges.STCODE = MainTest.MTCode order by MainTest.Maintestname ";

        }
        else
        {
             query = "SELECT DISTINCT "+
              "  RatT.RatID, RatT.RateName, TestCharges.STCODE, TestCharges.Amount, TestCharges.Percentage, TestCharges.Emergency, TestCharges.username, "+
              "  MainTest.Sampletype, MainTest.samecontain, MainTest.TatDuration, MainTest.Maintestname "+
              "  FROM         TestCharges INNER JOIN "+
              "  RatT ON TestCharges.DrCode = RatT.RatID INNER JOIN "+
              "   MainTest ON TestCharges.STCODE = MainTest.MTCode where RatT.RatID='" + drname + "' order by MainTest.Maintestname ";
        }


        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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
            conn.Close(); conn.Dispose();
        }
        return ds;

    }
  
}