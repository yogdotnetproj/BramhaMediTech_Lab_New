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


public class SampleType_Bal_C
{
    public SampleType_Bal_C()
    {
        this.Sampletype = "";
    }
    public SampleType_Bal_C(int id, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SELECT * from SCMsT where sampleid=@sampleid and branchid=" + branchid + " ", conn);//'"+uname+"'",conn);// +

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@sampleid", SqlDbType.Int)).Value = id;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {
                this.Sampleid = Convert.ToInt32(sdr["sampleid"]);
                this.Sampletype = sdr["Sampletype"].ToString();
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

    public bool Insert(int branchid)
    {        
        SqlConnection conn = DataAccess.ConInitForDC(); 


        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO SCMsT(Sampletype,branchid,username)" +
        "VALUES(@Sampletype,@branchid,@username)", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@Sampletype", SqlDbType.NVarChar,200)).Value = this.Sampletype;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
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
        // Implement Update logic.
        return true;
    } 
    public bool Update(int rid, string sample, int branchid)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "UPDATE SCMsT " +
            "SET Sampletype=@Sampletype WHERE sampleid=@sampleid and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@sampleid", SqlDbType.Int)).Value = rid;

        if (this.Sampletype != null)
            sc.Parameters.Add(new SqlParameter("@Sampletype", SqlDbType.NVarChar,200)).Value =sample;
        else
            sc.Parameters.Add(new SqlParameter("@Sampletype", SqlDbType.NVarChar,200)).Value = "";

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
        

        return true;
    } 

    public void delete(int rrid, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand("delete from SCMsT where sampleid='" + rrid + "' and branchid=" + branchid + "", conn);

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
            //catch ()
            //{

            //}
        }
    }

    public static bool isSampletypeeExists(string sampletype, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM SCMsT " +
                         " WHERE Sampletype=@Sampletype and branchid=" + branchid + " ", conn);      

        sc.Parameters.Add(new SqlParameter("@Sampletype", SqlDbType.NVarChar,200)).Value =sampletype;

        int cnt = 0;

        try
        {
            conn.Open();
            cnt = Convert.ToInt32(sc.ExecuteScalar());

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
            catch
            {
                throw new Exception("Record not found");
            }
        }
        if (cnt != 0)
            return true;
        else
            return false;
    }
    public static ICollection getSampleType(int branchid)
    {
        
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        SqlDataReader sdr = null;

        sc.Connection = conn;
        sc.CommandText = "select * from SCMsT order by sampletype";

        try
        {
            conn.Open();

            sdr = sc.ExecuteReader();

            if (sdr != null)
            {
                while (sdr.Read())
                {
                    SampleType_Bal_C sm = new SampleType_Bal_C();
                    sm.Sampletype = sdr["sampletype"].ToString();
                    sm.Sampleid = Convert.ToInt32(sdr["sampleid"]);
                    tl.Add(sm);
                   
                }
            }
            // This is not a while loop. It only loops once.
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
    }
    #region Properties
    private string sampletype;
    public string Sampletype
    {
        get { return sampletype; }
        set { sampletype = value; }
    }
    private int sampleid;
    public int Sampleid
    {
        get { return sampleid; }
        set { sampleid = value; }
    }
    

private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }
    #endregion
}
