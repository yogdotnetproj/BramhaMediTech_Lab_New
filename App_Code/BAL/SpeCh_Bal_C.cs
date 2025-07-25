using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public class SpeCh_Bal_C
{
    public SpeCh_Bal_C()
    {
        this.Amount = 0;
        this.DrCode = "";
        this.DrName = "";
        this.Emergency = 0;
        this.Percentage = 0;
        this.STCODE = "";
        this.TestName = "";
        this.RateType = "";
    }
   
    public SpeCh_Bal_C(object testcode_check, object DocCode, int branchid)
    {
        this.STCODE = Convert.ToString(testcode_check);
        this.DrCode = Convert.ToString(DocCode);
        
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("Select * from TestCharges WHERE STCODE=@STCODE and drcode=@drcode  and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);
        sc.Parameters.Add(new SqlParameter("@drcode", SqlDbType.NVarChar, 50)).Value = (string)(this.DrCode);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();

            sdr = sc.ExecuteReader();
           
            if (sdr != null && sdr.Read())
            {
                // The IEnumerable contains DataRowView objects.
                if (sdr["Amount"] != DBNull.Value)
                    this.Amount = Convert.ToInt32(sdr["Amount"]);
                this.DrCode = Convert.ToString(sdr["DrCode"]);
                this.DrName = Convert.ToString(sdr["DrName"]);
                if (sdr["Emergency"] != DBNull.Value)
                    this.Emergency = Convert.ToInt32(sdr["Emergency"]);
                if (sdr["Percentage"] != DBNull.Value)
                    this.Percentage = Convert.ToSingle(sdr["Percentage"]);
                this.STCODE = Convert.ToString(sdr["STCODE"]);
                this.TestName = Convert.ToString(sdr["TestName"]);

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
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }

        }
    }
    public SpeCh_Bal_C(object testcode_check, object DocCode, int branchid,int Client)
    {
        this.STCODE = Convert.ToString(testcode_check);
        this.DrCode = Convert.ToString(DocCode);

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("Select * from TestCharges WHERE STCODE=@STCODE and drcode=@drcode  and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);
        sc.Parameters.Add(new SqlParameter("@drcode", SqlDbType.NVarChar, 50)).Value = (string)(this.DrCode);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();

            sdr = sc.ExecuteReader();

            if (sdr != null && sdr.Read())
            {
                // The IEnumerable contains DataRowView objects.
                if (sdr["Amount"] != DBNull.Value)
                    this.Amount = Convert.ToInt32(sdr["Amount"]);
                this.DrCode = Convert.ToString(sdr["DrCode"]);
                this.DrName = Convert.ToString(sdr["DrName"]);
                if (sdr["Emergency"] != DBNull.Value)
                    this.Emergency = Convert.ToInt32(sdr["Emergency"]);
                if (sdr["Percentage"] != DBNull.Value)
                    this.Percentage = Convert.ToSingle(sdr["Percentage"]);
                this.STCODE = Convert.ToString(sdr["STCODE"]);
                this.TestName = Convert.ToString(sdr["TestName"]);

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
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }

        }
    } 
  
  
    public bool InsertORUpdate(int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select * from TestCharges where DrCode=@DrCode and STCODE=@STCODE and branchid=" + branchid + "", conn);
        cmd.Parameters.Add(new SqlParameter("@DrCode", SqlDbType.NVarChar, 50)).Value = (string)(this.DrCode);
        cmd.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);

        SqlDataReader sdr = null;
        SqlCommand sc = null;
        try
        {
            conn.Open();
            sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                sc = new SqlCommand("" +
                "UPDATE TestCharges SET DrCode =@DrCode,drname =@drname,testname =@testname,Amount =@Amount,percentage =@percentage,STCODE=@STCODE,emergency=@emergency ,updatedby=@username,updatedon=@updatedon  " +
                " where STCODE =@STCODE and drcode =@DrCode  and branchid=" + branchid + "", conn);
            }
            else
            {
                sc = new SqlCommand("insert into TestCharges(drname,drcode,testname,STCODE,amount,percentage,emergency,branchid,username,Createdby)values(@drname,@drcode,@testname,@STCODE,@amount,@percentage,@emergency,@branchid,@username,@username)", conn);
            }
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@DrCode", SqlDbType.NVarChar, 50)).Value = (string)(this.DrCode);
            sc.Parameters.Add(new SqlParameter("@drname", SqlDbType.NVarChar, 255)).Value = (string)(this.DrName);
            sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = (string)(this.TestName);
            sc.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int, 4)).Value = (int)(this.Amount);
            sc.Parameters.Add(new SqlParameter("@percentage", SqlDbType.Float, 8)).Value = (float)(this.Percentage);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);
            sc.Parameters.Add(new SqlParameter("@emergency", SqlDbType.Int, 9)).Value = (int)(this.Emergency);
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_username;
            sc.Parameters.Add(new SqlParameter("@Updatedon", SqlDbType.DateTime)).Value = this.Patregdate; ;
            try
            {
                sdr.Close();
                sc.ExecuteNonQuery();
            }
            catch { throw; }

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
        // Implement Update logic.

        return true;
    } 
    public bool Delete(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("Delete from TestCharges where drcode=@drcode and STCODE=@STCODE  and branchid=" + branchid + "", conn);
        
        sc.Parameters.Add(new SqlParameter("@drcode", SqlDbType.NVarChar, 30)).Value = (this.DrCode);
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = this.STCODE;

        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { throw; }
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
        // Implement Update logic.
        return true;
    } 

   
    //for insert
    public bool Insert(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
        "Insert Into TestCharges(DrCode,drname,STCODE,testname,Amount,percentage,emergency,branchid,Createdby)" +
        " VALUES(@DrCode,@drname,@STCODE,@testname,@Amount,@percentage,@emergency,@branchid,@username)", conn);

        sc.Parameters.Add(new SqlParameter("@DrCode", SqlDbType.NVarChar, 30)).Value = (string)(this.DrCode);
        sc.Parameters.Add(new SqlParameter("@drname", SqlDbType.NVarChar, 255)).Value = (string)(this.DrName);
        sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = (string)(this.TestName);
        sc.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int, 9)).Value = (int)(this.Amount);
        sc.Parameters.Add(new SqlParameter("@percentage", SqlDbType.Float, 8)).Value = (float)(this.Percentage);
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);
        sc.Parameters.Add(new SqlParameter("@emergency", SqlDbType.Int, 9)).Value = (int)(this.Emergency);
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_username;
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
        // Implement Update logic.
        return true;
    }

    public bool Insert_PerformTest(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
        "Insert Into DailyPerformTestCharges(DrCode,drname,STCODE,testname,Amount,percentage,emergency,branchid,Createdby)" +
        " VALUES(@DrCode,@drname,@STCODE,@testname,@Amount,@percentage,@emergency,@branchid,@username)", conn);

        sc.Parameters.Add(new SqlParameter("@DrCode", SqlDbType.NVarChar, 30)).Value = (string)(this.DrCode);
        sc.Parameters.Add(new SqlParameter("@drname", SqlDbType.NVarChar, 255)).Value = (string)(this.DrName);
        sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = (string)(this.TestName);
        sc.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int, 9)).Value = (int)(this.Amount);
        sc.Parameters.Add(new SqlParameter("@percentage", SqlDbType.Float, 8)).Value = (float)(this.Percentage);
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);
        sc.Parameters.Add(new SqlParameter("@emergency", SqlDbType.Int, 9)).Value = (int)(this.Emergency);
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_username;
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
        // Implement Update logic.
        return true;
    }


    public bool InsertORUpdatef_PerformTest(int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select * from DailyPerformTestCharges where DrCode=@DrCode and STCODE=@STCODE and branchid=" + branchid + "", conn);
        cmd.Parameters.Add(new SqlParameter("@DrCode", SqlDbType.NVarChar, 30)).Value = (string)(this.DrCode);
        cmd.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);

        SqlDataReader sdr = null;
        SqlCommand sc = null;
        try
        {
            conn.Open();
            sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                sc = new SqlCommand("" +
                "UPDATE DailyPerformTestCharges SET DrCode =@DrCode,drname =@drname,testname =@testname,Amount =@Amount,percentage =@percentage,STCODE=@STCODE,emergency=@emergency ,updatedby=@username,updatedon=@updatedon  " +
                " where STCODE =@STCODE and drcode =@DrCode  and branchid=" + branchid + "", conn);
            }
            else
            {
                sc = new SqlCommand("insert into DailyPerformTestCharges(drname,drcode,testname,STCODE,amount,percentage,emergency,branchid,username,Createdby)values(@drname,@drcode,@testname,@STCODE,@amount,@percentage,@emergency,@branchid,@username,@username)", conn);
            }
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@DrCode", SqlDbType.NVarChar, 50)).Value = (string)(this.DrCode);
            sc.Parameters.Add(new SqlParameter("@drname", SqlDbType.NVarChar, 255)).Value = (string)(this.DrName);
            sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = (string)(this.TestName);
            sc.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int, 4)).Value = (int)(this.Amount);
            sc.Parameters.Add(new SqlParameter("@percentage", SqlDbType.Float, 8)).Value = (float)(this.Percentage);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);
            sc.Parameters.Add(new SqlParameter("@emergency", SqlDbType.Int, 9)).Value = (int)(this.Emergency);
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_username;
            sc.Parameters.Add(new SqlParameter("@Updatedon", SqlDbType.DateTime)).Value = this.Patregdate; ;
            try
            {
                sdr.Close();
                sc.ExecuteNonQuery();
            }
            catch { throw; }

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
        // Implement Update logic.

        return true;
    }

    public bool Delete_Testperform(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("Delete from DailyPerformTestCharges where drcode=@drcode and STCODE=@STCODE  and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@drcode", SqlDbType.NVarChar, 30)).Value = (this.DrCode);
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = this.STCODE;

        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { throw; }
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
        // Implement Update logic.
        return true;
    } 

    internal class TestSpecialCharge_Bal_CException : Exception
    {
        public TestSpecialCharge_Bal_CException(string msg) : base(msg) { }
    }


    //properties
    DateTime? _DateofEntry = null;
    public DateTime? Patregdate
    {
        get
        {
            return _DateofEntry;
        }
        set
        {
            _DateofEntry = value;
        }
    }
    private int emergency;
    public int Emergency
    {
        get { return emergency; }
        set { emergency = value; }
    }

    private float percentage;
    public float Percentage
    {
        get { return percentage; }
        set { percentage = value; }
    }

    private int amount;
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    private string testName;
    public string TestName
    {
        get { return testName; }
        set { testName = value; }
    }

    private string sTCODE;
    public string STCODE
    {
        get { return sTCODE; }
        set { sTCODE = value; }
    }

    private string drCode;
    public string DrCode
    {
        get { return drCode; }
        set { drCode = value; }
    }

    private string drName;
    public string DrName
    {
        get { return drName; }
        set { drName = value; }
    }
    private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }

    private string rateType;
    public string RateType
    {
        get { return rateType; }
        set { rateType = value; }
    }
    public override string ToString()
    {
        return this.DrName;
    }
}
