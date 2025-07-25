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
using System.Collections.Generic;
using System.Collections;


public class ratetype_Bal_C
{
	public ratetype_Bal_C()
	{
        this.RatID = "";
        this.RateName = "";
	}
    public ratetype_Bal_C(string id, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SELECT * from RatT where RatID=@RatID and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@RatID", SqlDbType.NVarChar,3)).Value = id;      

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            while(sdr.Read())
            {
                this.RatID = sdr["RatID"].ToString();
                this.RateName = sdr["RateName"].ToString();
            }
            
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            
                conn.Close(); conn.Dispose();
           
            
        }
    }

    public static bool isRatetypeCodeExists(string RatID, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM RatT " +
                         " WHERE RatID=@RatID and branchid=" + branchid + " ", conn);

        sc.Parameters.Add(new SqlParameter("@RatID", SqlDbType.NVarChar, 50)).Value = RatID;

        int cnt = 0;

        try
        {
            conn.Open();
            cnt = Convert.ToInt32(sc.ExecuteScalar());

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
        if (cnt != 0)
            return true;
        else
            return false;
    }
    public bool Insert(int branchid)
    {       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO RatT(RateName,branchid,RateFlag,username)" +
        "VALUES(@RateName,@branchid,@RateFlag,@username)", conn);

        sc.Parameters.Add(new SqlParameter("@RateName", SqlDbType.NVarChar, 50)).Value = this.RateName;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@RateFlag", SqlDbType.Char,10)).Value = this.RateFlag;
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_username;
 

        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
       
        return true;
    }

    public bool Update(string RatID, string ratetype, int branchid)
    {        
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "UPDATE RatT " +
            "SET RateName=@RateName  WHERE RatID=@RatID and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@RatID", SqlDbType.NVarChar, 3)).Value = RatID;
        sc.Parameters.Add(new SqlParameter("@RateName", SqlDbType.NVarChar,200)).Value =ratetype;
       
        SqlDataReader sdr = null;

        try
        {
            conn.Close();
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.

        return true;
    } 

    public void delete(string rrid, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from RatT where RatID='" + rrid + "' and branchid=" + branchid + "", conn);
        
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
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
                throw;
            }
          
        }
    }
    #region properties    

    private string _RatID;

    public string RatID
    {
        get { return _RatID; }
        set { _RatID = value; }
    }

    private string _RateName;

    public string RateName
    {
        get { return _RateName; }
        set { _RateName = value; }
    }
    private char _RateFlag;

    public char RateFlag
    {
        get { return _RateFlag; }
        set { _RateFlag = value; }
    }

    private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }
    private string ratecate;
    public string P_ratecate
    {
        get { return ratecate; }
        set { ratecate = value; }
    }
    
    #endregion

    public static bool getcode(string rmaster, int branchid, string RateFlag)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand(" SELECT COUNT(RateName) AS total " +
                       " FROM dbo.RatT " +
                       " where dbo.RatT.RateName=@rmater and dbo.RatT.RateFlag=@RateFlag and" +
                       " dbo.RatT.branchid=@branchid ", conn);

        sc.Parameters.Add(new SqlParameter("@rmater", SqlDbType.NVarChar, 255)).Value = rmaster;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@RateFlag", SqlDbType.Char, 10)).Value = RateFlag;

        int cnt = 0;
        try
        {
            conn.Open();
            cnt = Convert.ToInt32(sc.ExecuteScalar());
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
        if (cnt != 0)
            return true;
        else
            return false;
    }
}
