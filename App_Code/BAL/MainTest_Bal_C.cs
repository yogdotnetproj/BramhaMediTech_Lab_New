using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
public class MainTest_Bal_C
{
    public MainTest_Bal_C()
    {
        

        this.Maintestname = "";
        this.MTCode = "";
        this.Amount = 0;
        this.Percentage = 0;
        this.Emergency = 0;
                
        this.Maintestid = 0;
        this.SDCode = "";
        this.Singleformat = "";

        this.Samecontain = 0f;
        this.TextDesc = "";  
       
        this.DefaultResult = "";
       
        this.Testordno = 0;
       
        this.DefaultTestMethod = "";

        this.Shortcode = "";
      
        this.Patregdate = Date.getdate();
        this.SampleType = "";   


    }

    public MainTest_Bal_C(string MTCode)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT * FROM MainTest  WHERE MTCode = @MTCode  ", conn);//

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                #region comment

                this.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                this.Maintestname = sdr["Maintestname"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.Singleformat = sdr["Singleformat"].ToString();
                this.Samecontain = Convert.ToSingle(sdr["Samecontain"]);



                this.DefaultResult = sdr["DefaultResult"].ToString();



                this.Shortcode = sdr["Shortcode"].ToString();
                this.TextDesc = sdr["TextDesc"].ToString();

                this.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();

                this.Patregdate = Convert.ToDateTime(sdr["Dateofentry"]);
                this.Testordno = Convert.ToInt32(sdr["Testordno"]);

                if (!string.IsNullOrEmpty(sdr["Sampletype"].ToString()))
                    this.SampleType = sdr["Sampletype"].ToString();
                else
                    this.SampleType = "";

                #endregion
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
                // Log an event in the Application Event Log.
                throw;
            }

        }
    }

    public MainTest_Bal_C(string MTCode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT * FROM MainTest  WHERE MTCode = @MTCode and branchid=" + branchid + "  ", conn);//and ISTestActive=1

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                #region comment

                this.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                this.Maintestname = sdr["Maintestname"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.Singleformat = sdr["Singleformat"].ToString();
                this.Samecontain = Convert.ToSingle(sdr["Samecontain"]);



                this.DefaultResult = sdr["DefaultResult"].ToString();



                this.Shortcode = sdr["Shortcode"].ToString();
                this.TextDesc = sdr["TextDesc"].ToString();

                this.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();

                this.Patregdate = Convert.ToDateTime(sdr["Dateofentry"]);
                this.Testordno = Convert.ToInt32(sdr["Testordno"]);

                if (!string.IsNullOrEmpty(sdr["Sampletype"].ToString()))
                    this.SampleType = sdr["Sampletype"].ToString();
                else
                    this.SampleType = "";

                #endregion
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
                // Log an event in the Application Event Log.
                throw;
            }

        }
    }


    public MainTest_Bal_C(string a, string b, string MTCode, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT * FROM MainTest  WHERE MTCode = @MTCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
       

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                #region comment
               
                this.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                this.Maintestname = sdr["Maintestname"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.Singleformat = sdr["Singleformat"].ToString();

                if (sdr["samecontain"] is DBNull)
                    this.Samecontain = 0.0f;
                else
                    this.Samecontain = Convert.ToSingle(sdr["samecontain"]);
             
                this.DefaultResult = sdr["DefaultResult"].ToString();

                this.Shortcode = sdr["Shortcode"].ToString();
                this.TextDesc = sdr["TextDesc"].ToString();
              
                this.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();

                this.Patregdate = Convert.ToDateTime(sdr["DateofEntry"]);
                this.Testordno = Convert.ToInt32(sdr["Testordno"]);
               

                if (!string.IsNullOrEmpty(sdr["Sampletype"].ToString()))
                    this.SampleType = sdr["Sampletype"].ToString();
                else
                    this.SampleType = "";
                #endregion
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
                // Log an event in the Application Event Log.
                throw;
            }

        }
    }

    public MainTest_Bal_C(string a, string MTCode, int branchid)
    {
      
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT     MainTest.*,TestCharges.Amount as TestRate1 " +
                                       " FROM         MainTest LEFT OUTER JOIN " +
                                       " TestCharges ON MainTest.MTCode = TestCharges.STCODE AND MainTest.branchid = TestCharges.branchid " +
                                       " WHERE     (TestCharges.Amount=(select max(amount) from TestCharges where STCODE = @MTCode and branchid=" + branchid + ")) and (MainTest.MTCode = @MTCode) and MainTest.branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
      
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                #region comment
               
                this.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                this.Maintestname = sdr["Maintestname"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.Singleformat = sdr["Singleformat"].ToString();
                this.Samecontain = Convert.ToSingle(sdr["samecontain"]);
               
                this.DefaultResult = sdr["DefaultResult"].ToString();

                this.Shortcode = sdr["Shortcode"].ToString();
                this.TextDesc = sdr["TextDesc"].ToString();
               
                this.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();
              
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.Testordno = Convert.ToInt32(sdr["Testordno"]);
                

               
               
                if (!string.IsNullOrEmpty(sdr["Sampletype"].ToString()))
                    this.SampleType = sdr["Sampletype"].ToString();
                else
                    this.SampleType = "";

                #endregion
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
                // Log an event in the Application Event Log.
                throw;
            }

        }
    }
    public MainTest_Bal_C(int Maintest_id, int i, object branchid)
    {
                
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT * FROM MainTest  WHERE Maintestid = @Maintestid and branchid=@branchid", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@Maintestid", SqlDbType.Int, 4)).Value = Maintest_id;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
               
                this.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                this.Maintestname = sdr["Maintestname"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.Singleformat = sdr["Singleformat"].ToString();
                this.Samecontain = Convert.ToSingle(sdr["samecontain"]);
               
                this.DefaultResult = sdr["DefaultResult"].ToString();

                this.Shortcode = sdr["Shortcode"].ToString();
                this.TextDesc = sdr["TextDesc"].ToString();
                
                this.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();

                this.Patregdate = Convert.ToDateTime(sdr["DateofEntry"]);
                this.Testordno = Convert.ToInt32(sdr["Testordno"]);
               
               
                if (!string.IsNullOrEmpty(sdr["Sampletype"].ToString()))
                    this.SampleType = sdr["Sampletype"].ToString();
                else
                    this.SampleType = "";
            }
            else
            {
                throw new MainTest_Bal_CTableException("No Record Fetched.");
            }
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
            catch (MainTest_Bal_CTableException)
            {
                throw new MainTest_Bal_CTableException("Record not found");
            }
        }
    } 

   
    public MainTest_Bal_C(string Maintestname, string SDCode)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT * FROM MainTest  WHERE Maintestname = @Maintestname and SDCode=@SDCode", conn);                
        sc.Parameters.Add(new SqlParameter("@Maintestname", SqlDbType.NVarChar, 200)).Value = (string)(Maintestname);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = (string)(SDCode);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                this.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                this.Maintestname = sdr["Maintestname"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.Singleformat = sdr["Singleformat"].ToString();
                if (sdr["samecontain"] is DBNull)
                    this.Samecontain = 0.0f;
                else
                    this.Samecontain = Convert.ToSingle(sdr["samecontain"]);

                this.DefaultResult = sdr["DefaultResult"].ToString();


                this.Shortcode = sdr["Shortcode"].ToString();
                this.TextDesc = sdr["TextDesc"].ToString();
              
                this.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();

                this.Patregdate = Convert.ToDateTime(sdr["DateofEntry"]);
                this.Testordno = Convert.ToInt32(sdr["Testordno"]);                                 

                if (!string.IsNullOrEmpty(sdr["Sampletype"].ToString()))
                    this.SampleType = sdr["Sampletype"].ToString();
                else
                    this.SampleType = "";
            }
            else
            {
                throw new MainTest_Bal_CTableException("No Record Fetched.");
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
                // Log an event in the Application Event Log.
                throw;
            }

        }
    }
    public MainTest_Bal_C(int Maintest_id, int i)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT * FROM MainTest WHERE Maintestid = @Maintestid", conn);
        sc.Parameters.Add(new SqlParameter("@Maintestid", SqlDbType.Int, 4)).Value = Maintest_id;
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                this.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                this.Maintestname = sdr["Maintestname"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.Singleformat = sdr["Singleformat"].ToString();
                this.Shortform = sdr["Shortcode"].ToString();
                this.Samecontain = Convert.ToSingle(sdr["samecontain"]);
                

               
                this.DefaultResult = sdr["DefaultResult"].ToString();

                this.Shortcode = sdr["Shortcode"].ToString();
                this.TextDesc = sdr["TextDesc"].ToString();
               
                this.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();

                this.Patregdate = Convert.ToDateTime(sdr["DateofEntry"]);
                if (sdr["Testordno"] != DBNull.Value)
                    this.Testordno = Convert.ToInt32(sdr["Testordno"]);
               
               
               

                if (!string.IsNullOrEmpty(sdr["Sampletype"].ToString()))
                    this.SampleType = sdr["Sampletype"].ToString();
                else
                    this.SampleType = "";               

                this.P_TatName = sdr["TatName"].ToString();
                this.P_TatDuration = sdr["TatDuration"].ToString();
                P_Is_TestActive = Convert.ToBoolean( sdr["ISTestActive"]);
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
                // Log an event in the Application Event Log.
                throw;
            }

        }
    }
 
    public bool Update(int Maintestid, object branchid)
    {      
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            " UPDATE MainTest SET Maintestname = @Maintestname,MTCode = @MTCode,SDCode =@SDCode ,SingleFormat = @SingleFormat,Shortcode=@Shortcode," +
            " samecontain=@samecontain ,DefaultTestMethod =@DefaultTestMethod ,TextDesc = @TextDesc," +
            "  DefaultResult=@DefaultResult," +
            " DateofEntry = @Patregdate,Testordno = @Testordno ,Sampletype=@Sampletype" +
            " ,TatName=@TatName,TatDuration=@TatDuration,ISTestActive=@ISTestActive where Maintestid=@Maintestid and branchid=@branchid", conn);

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@Maintestid", SqlDbType.Int, 4)).Value = Maintestid;
        sc.Parameters.Add(new SqlParameter("@Maintestname", SqlDbType.NVarChar, 200)).Value = (string)(this.Maintestname);
        sc.Parameters.Add(new SqlParameter("@Shortcode", SqlDbType.NVarChar, 50)).Value = (string)(this.Shortcode);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = (string)(this.SDCode);
        sc.Parameters.Add(new SqlParameter("@SingleFormat", SqlDbType.NVarChar, 12)).Value = (string)(this.Singleformat);
        sc.Parameters.Add(new SqlParameter("@samecontain", SqlDbType.Float, 8)).Value = Convert.ToSingle(this.Samecontain);
     
        sc.Parameters.Add(new SqlParameter("@DefaultResult", SqlDbType.NText, 16)).Value = (string)(this.DefaultResult);
       
        sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.NVarChar, 50)).Value = (string)(this.TextDesc);
       
        sc.Parameters.Add(new SqlParameter("@DefaultTestMethod", SqlDbType.NVarChar, 50)).Value = (string)(this.DefaultTestMethod);
      
        sc.Parameters.Add(new SqlParameter("@Patregdate", SqlDbType.DateTime, 4)).Value = (DateTime)(this.Patregdate);
        sc.Parameters.Add(new SqlParameter("@Testordno", SqlDbType.Int, 4)).Value = Convert.ToInt32(this.Testordno);
       
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = this.MTCode;
        sc.Parameters.Add(new SqlParameter("@Sampletype", SqlDbType.NVarChar, 50)).Value = (this.SampleType);
     
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
       
        sc.Parameters.Add(new SqlParameter("@TatName", SqlDbType.NVarChar, 50)).Value = this.P_TatName;
        sc.Parameters.Add(new SqlParameter("@TatDuration", SqlDbType.NVarChar, 50)).Value = this.P_TatDuration;
        sc.Parameters.Add(new SqlParameter("@ISTestActive", SqlDbType.Bit)).Value = P_Is_TestActive;
        conn.Close();

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
    } //update End


    public bool Update(string MTCode)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            " UPDATE MainTest SET  Maintestname = @Maintestname, SDCode =@SDCode ,SingleFormat = @SingleFormat,Shortcode=@Shortcode," +
            " samecontain =@samecontain ,DefaultTestMethod =@DefaultTestMethod ,TextDesc = @TextDesc," +
            "  DefaultResult=@DefaultResult," +
            " DateofEntry = @Patregdate,Testordno = @Testordno " +
            " where MTCode = @MTCode", conn);

        sc.Parameters.Add(new SqlParameter("@Maintestid", SqlDbType.Int, 4)).Value = Convert.ToInt32(this.Maintestid);
        sc.Parameters.Add(new SqlParameter("@Maintestname", SqlDbType.NVarChar, 200)).Value = (string)(this.Maintestname);
        sc.Parameters.Add(new SqlParameter("@Shortcode", SqlDbType.NVarChar, 50)).Value = (string)(this.Shortcode);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = (string)(this.SDCode);
        sc.Parameters.Add(new SqlParameter("@SingleFormat", SqlDbType.NVarChar, 12)).Value = (string)(this.Singleformat);
        sc.Parameters.Add(new SqlParameter("@samecontain", SqlDbType.Float, 8)).Value = Convert.ToSingle(this.Samecontain);
       
        sc.Parameters.Add(new SqlParameter("@DefaultResult", SqlDbType.NText)).Value = (string)(this.DefaultResult);
       
        sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.NVarChar, 50)).Value = (string)(this.TextDesc);
       
        sc.Parameters.Add(new SqlParameter("@DefaultTestMethod", SqlDbType.NVarChar, 50)).Value = (string)(this.DefaultTestMethod);
      
        sc.Parameters.Add(new SqlParameter("@Patregdate", SqlDbType.DateTime, 4)).Value = (DateTime)(this.Patregdate);
        sc.Parameters.Add(new SqlParameter("@Testordno", SqlDbType.Int, 4)).Value = Convert.ToInt32(this.Testordno);
       
      
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (string)(MTCode);
       
        conn.Close();

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
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

   
    public bool Delete(object branchid)
    {       
       
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SP_phserdelmst", conn);       
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (string)(this.MTCode);
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        sc.CommandType = CommandType.StoredProcedure;
        bool flag = true;
        try
        {
            
            conn.Open();
            Object i=sc.ExecuteScalar();
            if ((int)i == 0)
            {flag=true; }
            else
            { flag = false; }
        }
            catch(Exception)
        {}
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
        return flag;
    } 
    public bool Insert(int branchid)
    {        
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO MainTest (Maintestname,MTCode,SDCode,SingleFormat,samecontain,DefaultTestMethod, " +
                     " TextDesc,DefaultResult, " +
                     " DateofEntry,Testordno,Sampletype,branchid,username,Shortcode,TatName,TatDuration,ISTestActive)" +
                     " values(@Maintestname,@MTCode,@SDCode,@SingleFormat,@samecontain,@DefaultTestMethod, " +
                     " @TextDesc,@DefaultResult," +
                     " @Patregdate,@Testordno,@Sampletype,@branchid,@username,@Shortcode,@TatName,@TatDuration,@ISTestActive)", conn);


        sc.Parameters.Add(new SqlParameter("@Maintestname", SqlDbType.NVarChar, 200)).Value = (string)(this.Maintestname);
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (string)(this.MTCode);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = (string)(this.SDCode);
        sc.Parameters.Add(new SqlParameter("@SingleFormat", SqlDbType.NVarChar, 12)).Value = (string)(this.Singleformat);
        sc.Parameters.Add(new SqlParameter("@samecontain", SqlDbType.Float, 8)).Value = Convert.ToSingle(this.Samecontain);
       
        sc.Parameters.Add(new SqlParameter("@DefaultResult", SqlDbType.NText, 16)).Value = (string)(this.DefaultResult);
        sc.Parameters.Add(new SqlParameter("@DefaultTestMethod", SqlDbType.NText, 16)).Value = (string)(this.DefaultTestMethod);
        sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.NVarChar, 50)).Value = (string)(this.TextDesc);
       
        sc.Parameters.Add(new SqlParameter("@Patregdate", SqlDbType.DateTime, 4)).Value = (DateTime)(this.Patregdate);
        sc.Parameters.Add(new SqlParameter("@Testordno", SqlDbType.Int, 4)).Value = Convert.ToInt32(this.Testordno);
       
        sc.Parameters.Add(new SqlParameter("@sampletype", SqlDbType.NVarChar, 200)).Value = (this.SampleType);
        sc.Parameters.Add(new SqlParameter("@Shortcode", SqlDbType.NVarChar, 200)).Value = (this.Shortcode);
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;      
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_username;
        sc.Parameters.Add(new SqlParameter("@TatName", SqlDbType.NVarChar, 50)).Value = this.P_TatName;
        sc.Parameters.Add(new SqlParameter("@TatDuration", SqlDbType.NVarChar, 50)).Value = this.P_TatDuration;
        sc.Parameters.Add(new SqlParameter("@ISTestActive", SqlDbType.Bit)).Value = P_Is_TestActive;
        
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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;        
    }


    public static int MaxOrder(string SD_Code, object branchid)
    {
        int no = 0;
       
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select count(Testordno) from MainTest where SDCode=@SDCode and branchid=@branchid ", conn);

        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SD_Code;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr.Read())
            {
                no = Convert.ToInt32(sdr.GetValue(0).ToString());

            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return no;
    }
  
  
    public static int MaxFieldOrder(string MTCode, object branchid)
    {
        int Fno = 0;
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("select count(Testordno) from SubTest where MTCode=@MTCode and branchid=@branchid", conn);
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr.Read())
            {
                Fno = Convert.ToInt32(sdr.GetValue(0).ToString());

            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return Fno;
    }


    public static int Get_TotalTestCount(string MTCode, int branchid)
    {
        int i;

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "Select count(*) FROM patmstd where patmstd.MTCode='" + MTCode + "' and patmstd.branchid=" + branchid + "";
        try
        {
            conn.Open();
            i = Convert.ToInt32(sc.ExecuteScalar());
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
        return i;
    }
    internal class MainTest_Bal_CTableException : Exception
    {
        public MainTest_Bal_CTableException(string msg) : base(msg) { }
    }

   

    private string sdCode;
    public string SDCode
    {
        get { return sdCode; }
        set { sdCode = value; }
    }
    private string mTCode;
    public string MTCode
    {
        get { return mTCode; }
        set { mTCode = value; }
    }
    private string maintestname;
    public string Maintestname
    {
        get { return maintestname; }
        set { maintestname = value; }
    }

    private int amount;

    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }

    private float percentage;

    public float Percentage
    {
        get { return percentage; }
        set { percentage = value; }
    }
    private int emergency;

    public int Emergency
    {
        get { return emergency; }
        set { emergency = value; }
    }

    
    private int maintestid;
    public int Maintestid
    {
        get { return maintestid; }
        set { maintestid = value; }
    }

    private string shortcode;
    public string Shortcode
    {
        get { return shortcode; }
        set { shortcode = value; }
    }

    private string singleformat;
    public string Singleformat
    {
        get { return singleformat; }
        set { singleformat = value; }
    }
    private string shortform;
    public string Shortform
    {
        get { return shortform; }
        set { shortform = value; }
    }




    private float samecontain;
    public float Samecontain
    {
        get { return samecontain; }
        set { samecontain = value; }
    }

    private string textDesc;
    public string TextDesc
    {
        get { return textDesc; }
        set { textDesc = value; }
    }

   
    private DateTime _Patregdate;
    public DateTime Patregdate
    {
        get { return _Patregdate; }
        set { _Patregdate = value; }
    }

   

    

   


    private string defaultresult;
    public string DefaultResult
    {
        get { return defaultresult; }
        set { defaultresult = value; }
    }

   

    private int testordno;
    public int Testordno
    {
        get { return testordno; }
        set { testordno = value; }
    }

   

    private string defaulttestmethod;
    public string DefaultTestMethod
    {
        get { return defaulttestmethod; }
        set { defaulttestmethod = value; }
    }

  

    private string sampleType;
    public string SampleType
    {
        get { return sampleType; }
        set { sampleType = value; }
    }

  
    private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }
    private string testname;
    public string P_testname
    {
        get { return testname; }
        set { testname = value; }
    }
   

    private bool istableformat;
    public bool P_istableformat
    {
        get { return istableformat; }
        set { istableformat = value; }
    }
    private string TatName;
    public string P_TatName
    {
        get { return TatName; }
        set { TatName = value; }
    }
    private string TatDuration;
    public string P_TatDuration
    {
        get { return TatDuration; }
        set { TatDuration = value; }
    }

    private bool Is_TestActive;
    public bool P_Is_TestActive
    {
        get { return Is_TestActive; }
        set { Is_TestActive = value; }
    }

    public override string ToString()
    {
        return this.Maintestname;
    }

    public string GetTestformat(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string TestFormat = "";

        try
        {
            object obj;
            SqlCommand cmd = new SqlCommand(" SELECT TextDesc FROM dbo.MainTest where MTCode='" + this.MTCode + "' and branchid=" + branchid + "", conn);
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != DBNull.Value)
            {
                TestFormat = Convert.ToString(obj);
            }
        }

        catch (Exception ex) { throw; }
        finally { conn.Close(); conn.Dispose(); }
        return TestFormat;
    }

    public static bool ISTestCodeExist(string TCode, int branchid, string SDCode)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand(" SELECT COUNT(dbo.patmstd.MTCode) AS total " +
                       " FROM dbo.patmstd " +
                       " where dbo.patmstd.MTCode=@MTCode and " +
                       " dbo.patmstd.branchid=@branchid ", conn);

        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 255)).Value = SDCode;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = TCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

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

    public static bool ISTestCodeExistGroupDetails(string T_Code, int branchid, string SDCode)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand(" SELECT COUNT(dbo.PackmstD.MTCode) AS total " +
                       " FROM dbo.PackmstD " +
                       " where dbo.PackmstD.MTCode=@MTCode and " +
                       " dbo.PackmstD.branchid=@branchid ", conn);

        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 255)).Value = SDCode;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = T_Code;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

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

