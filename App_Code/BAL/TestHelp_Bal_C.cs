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


public class TestHelp_Bal_C
{
    
    private string username;
    public string P_UserName
    {
        get { return username; }
        set { username = value; }
    }

    private int branchid;
    public int P_branchid
    {
        get { return branchid; }
        set { branchid = value; }
    }
    private int Srno;
    public int P_Srno
    {
        get { return Srno; }
        set { Srno = value; }
    }
    private string Helptest;
    public string P_Helptest
    {
        get { return Helptest; }
        set { Helptest = value; }
    }
    private string MTCode;
    public string P_tlcode
    {
        get { return MTCode; }
        set { MTCode = value; }
    }
    private string STCODE;
    public string P_Testcode
    {
        get { return STCODE; }
        set { STCODE = value; }
    }
    public DataSet FilltestReslt()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string query = " select * from defmst where branchid=" + P_branchid + "";
        if(P_tlcode!="")
        {
            query += " and MTCode='" + P_tlcode + "' ";
        }
        if(P_Testcode!="")
        {
            query += " and STCODE='" + P_Testcode + "'";
        }
        SqlDataAdapter da = new SqlDataAdapter(query, conn);
        DataSet ds = new DataSet();
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

    public DataSet FillGrd()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string query = "select * from defmst where branchid='" + P_branchid + "'";
       
        SqlDataAdapter da = new SqlDataAdapter(query, conn);
        DataSet ds = new DataSet();
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
    public void InsertUpdTest()
    {         
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;

            sc = new SqlCommand(); 

            sc.CommandType = CommandType.StoredProcedure;
            sc.CommandText = "SP_InsertUpdHelpMaster";
            sc.Connection = conn;
            sc.Parameters.Add(new SqlParameter("@SrNo", SqlDbType.Int)).Value = P_Srno;
            sc.Parameters.Add(new SqlParameter("@HelpTest", SqlDbType.NVarChar,150)).Value = P_Helptest;
            sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar ,50)).Value = P_UserName;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = P_branchid;
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar,20)).Value = P_tlcode;
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar,20)).Value = P_Testcode;
            try
            {
                conn.Open();
                try
                {
                    sc.ExecuteNonQuery();
                }
                catch
                { throw; }

            }
            finally
            {
                try
                {
                    conn.Close(); conn.Dispose();
                }
                catch (SqlException)
                {   throw;
                }
            }
            
          
        }
   
    public void deleterec()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Delete from defmst where SrNo=" + P_Srno + "", con);

        try
        {
            con.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch
            { throw; }
             
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }

    public bool ExistsReason(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        //take id 

        SqlCommand sc = new SqlCommand(" select * from defmst where helptest=@helptest and branchid=@branchid", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@helptest", SqlDbType.NVarChar, 4000)).Value = this.Helptest;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
       
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr.HasRows)
            {
                return true;
            }
            else
            {
                return false;
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
                sdr.Close();
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
}

 
