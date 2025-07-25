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

/// <summary>
/// Summary description for BankMasterTable_Bal_C
/// </summary>
public class BankMasterTable_Bal_C
{
	public BankMasterTable_Bal_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public BankMasterTable_Bal_C(string bankcode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("SELECT * from  BankMaster where BankCode=@BankCode and branchid=" + branchid + " ", conn);
        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@BankCode", SqlDbType.NVarChar,100)).Value = bankcode;
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                this.BankName = sdr["BankName"].ToString();
                this.City = sdr["City"].ToString();
                this.AccNo = sdr["AccNo"].ToString();
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
    public BankMasterTable_Bal_C(string bankcode)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("SELECT * from  BankMaster where BankCode=@BankCode", conn);//'"+uname+"'",conn);// +
        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@BankCode", SqlDbType.NVarChar, 100)).Value = bankcode;
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {
                this.BankName = sdr["BankName"].ToString();
                this.Branchcode = sdr["Branchcode"].ToString();
                this.City = sdr["City"].ToString();
                this.AccNo = sdr["AccNo"].ToString();
                this.AccName = sdr["accname"].ToString();
            }
            
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw new Exception("Record not found");
            }

        }
    }
    public bool Insert(int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC(); 

        //take id 

        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO BankMaster(BankCode,BankName,AccNo,City,branchid,username)" +
        "VALUES(@BankCode,@BankName,@AccNo,@City,@branchid,@username)", conn);

        sc.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar,100)).Value = this.City;
        sc.Parameters.Add(new SqlParameter("@BankCode", SqlDbType.NVarChar, 100)).Value = this.BankCode;
        sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar,255)).Value = this.BankName;
        sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = this.AccNo;
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
    } //insert End

    public bool Update(string bankcode, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "UPDATE BankMaster " +
            "SET BankName=@BankName,AccNo=@AccNo,City=@City where BankCode=@BankCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 100)).Value = this.City;
        sc.Parameters.Add(new SqlParameter("@BankCode", SqlDbType.NVarChar, 100)).Value =bankcode;
        sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 255)).Value = this.BankName;
        sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = this.AccNo;

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
    } //update End

    public void delete(string bankcode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from BankMaster where BankCode='" + bankcode + "' and branchid=" + branchid + "", conn);//'"+uname+"'",conn);// +

        // Add the employee ID parameter and set its value.

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

    public static ICollection getBank(int branchid)
    {
        //List<MainTest_Bal_C> tl = new List<MainTest_Bal_C>();
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        SqlDataReader sdr = null;
        sc.Connection = conn;
        sc.CommandText = "select * from BankMaster where branchid=" + branchid + " order by BankName";

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    BankMasterTable_Bal_C bank = new BankMasterTable_Bal_C();
                    bank.BankCode = sdr["BankCode"].ToString();
                    bank.BankName = sdr["BankName"].ToString();
                    bank.AccNo = sdr["AccNo"].ToString();
                    bank.City = sdr["City"].ToString();
                    tl.Add(bank);
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }

        return tl;
    }//End Fill All 
    public static ICollection getBank()
    {
        
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        SqlDataReader sdr = null;

        sc.Connection = conn;
        sc.CommandText = "select * from BankMaster where branchid=1 order by BankName";

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    BankMasterTable_Bal_C bank = new BankMasterTable_Bal_C();
                    bank.BankCode = sdr["BankCode"].ToString();
                    bank.BankName = sdr["BankName"].ToString();
                    bank.AccNo = sdr["AccNo"].ToString();
                    bank.City = sdr["City"].ToString();
                    tl.Add(bank);
                }
            }
            // This is not a while loop. It only loops once.
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

        return tl;
    }//End Fill All 
    public static bool isBankCodeExists(string BankCode, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SELECT count(*) from BankMaster where  BankCode=@BankCode and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@BankCode", SqlDbType.NVarChar, 100)).Value = BankCode;

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

    #region properties

    private string _BankCode;
    public string  BankCode
    {
        get { return _BankCode; }
        set { _BankCode = value; }
    }

    private string _BankName;
    public string BankName
    {
        get { return _BankName; }
        set { _BankName = value; }
    }
    private string _Branchcode;
    public string Branchcode
    {
        get { return _Branchcode; }
        set { _Branchcode = value; }
    }
    private string _AccName;
    public string AccName
    {
        get { return _AccName; }
        set { _AccName = value; }
    }
    
    private string _AccNo;
    public string AccNo
    {
        get { return _AccNo; }
        set { _AccNo = value; }
    }

    private string _City;
    public string City
    {
        get { return _City; }
        set { _City = value; }
    }
    private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }


    #endregion
}
