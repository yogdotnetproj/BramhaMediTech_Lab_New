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
using System.Collections;


public class ratetypeLogic_Bal_C
{
	public ratetypeLogic_Bal_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string rateTypeBycode(string ratetypeCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select RateName from RatT where RatID=@RatID and branchid=" + branchid + " ", conn);

        sc.Parameters.Add(new SqlParameter("@RatID", SqlDbType.NVarChar,50)).Value = ratetypeCode;

        object rateTypeName = null;

        try
        {
            conn.Open();
            rateTypeName = sc.ExecuteScalar();
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
        if (rateTypeName == null)
        {
            return "";
        }
        else
            return rateTypeName.ToString();
    }

    public static string rateTypeCodeByName(string ratetypeName, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("select RatID from RatT where RateName=@RateName and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@RateName", SqlDbType.NVarChar, 50)).Value = ratetypeName;

        object rateTypeName = null;

        try
        {
            conn.Open();
            rateTypeName = sc.ExecuteScalar();
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
        if (rateTypeName == null)
        {
            return "";
        }
        else
            return rateTypeName.ToString();
    }

    public static ICollection getRateType(int branchid,char flg)
    {
      
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        SqlDataReader sdr = null;

        sc.Connection = conn;
        string query = "select *  from RatT where branchid=" + branchid + "";
        if (flg != 0)
        {
            query+=" and RateFlag='"+ flg +"' ";
        }
        query += " Order by Ratename asc ";
        sc.CommandText = query;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    ratetype_Bal_C rm = new ratetype_Bal_C();
                    rm.RateName = sdr["RateName"].ToString();
                    rm.RatID = sdr["RatID"].ToString();
                   
                    tl.Add(rm);
                   
                }
            }
            
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
            
        }

        return tl;
    }//End Fill All 
}
