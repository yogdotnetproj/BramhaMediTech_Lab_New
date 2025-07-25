
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public  class SubTest_Bal_C
    {

        public SubTest_Bal_C()
        {
            this.TestID = 0;
            this.TestName = "";
            this.STCODE = "";
            
            this.MTCode = "";
            this.SDCode = "";
            this.TextDesc = "";
            this.Patregdate = Date.getdate();
            this.Testordno = 0;
           
            this.DefaultResult = "";
           
            this.TestMethod = "";
            

        }


        public SubTest_Bal_C(object testname)
        {
            this.TestName = Convert.ToString(testname);            
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand(" select * from SubTest " +
                             "WHERE Testname=@testname ", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 200)).Value = this.TestName;

            SqlDataReader sdr = null;
            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    // The IEnumerable contains DataRowView objects.
                    //22
                    this.SDCode = sdr["SDCode"].ToString();
                    this.MTCode = sdr["MTCode"].ToString();
                    this.STCODE = sdr["STCODE"].ToString();
                    this.TestName = sdr["TestName"].ToString();
                   
                    if (!(sdr["TextDesc"] is DBNull))
                        this.TextDesc = Convert.ToString(sdr["TextDesc"]);

                   
                    if (!(sdr["DefaultResult"] is DBNull))
                        this.DefaultResult = sdr["DefaultResult"].ToString();
                    else
                        this.Patregdate = Date.getdate();

                    if (!(sdr["Patregdate"] is DBNull))
                        this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);

                    if (!(sdr["Testordno"] is DBNull))
                        this.Testordno = Convert.ToInt32(sdr["Testordno"]);

                   
                    if (!(sdr["TestMethod"] is DBNull))
                        this.TestMethod = sdr["TestMethod"].ToString();                    

                    this.TestID = Convert.ToInt32(sdr["TestID"]);
                    this.TestName = sdr["TestName"].ToString();
                    this.STCODE = sdr["STCODE"].ToString();
                    
                }
                else
                {
                    throw new SubTest_Bal_CTableException("No Record Fetched.");
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
                catch (SubTest_Bal_CTableException)
                {
                    throw new SubTest_Bal_CTableException("Record not found");
                }
            }
        } 


        public SubTest_Bal_C(int testid)
        {
            
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand(" select * from SubTest " +
                             "WHERE Testid=@testID ", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@testID", SqlDbType.Int)).Value = testid;

            SqlDataReader sdr = null;
            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    // The IEnumerable contains DataRowView objects.
                    //22
                    this.SDCode = sdr["SDCode"].ToString();
                    this.MTCode = sdr["MTCode"].ToString();
                    this.STCODE = sdr["STCODE"].ToString();
                    this.TestName = sdr["TestName"].ToString();
                    
                    if (!(sdr["TextDesc"] is DBNull))
                        this.TextDesc = Convert.ToString(sdr["TextDesc"]);

                   
                    if (!(sdr["DefaultResult"] is DBNull))
                        this.DefaultResult = sdr["DefaultResult"].ToString();
                    else
                        this.Patregdate = Date.getdate();

                    if (!(sdr["DateofEntry"] is DBNull))
                        this.Patregdate = Convert.ToDateTime(sdr["DateofEntry"]);

                    if (!(sdr["Testordno"] is DBNull))
                        this.Testordno = Convert.ToInt32(sdr["Testordno"]);

                   
                    if (!(sdr["TestMethod"] is DBNull))
                        this.TestMethod = sdr["TestMethod"].ToString();
                    this.TestID = Convert.ToInt32(sdr["TestID"]);
                    this.TestName = sdr["TestName"].ToString();
                    this.STCODE = sdr["STCODE"].ToString();
                   
                    if (!(sdr["Shortform"] is DBNull))
                        this.P_shortform = sdr["Shortform"].ToString();
                    else
                        P_shortform = "";
                }
                else
                {
                    throw new SubTest_Bal_CTableException("No Record Fetched.");
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
      
        public SubTest_Bal_C(string MTCode, bool flag)
        {
            
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand(" select * from SubTest " +
                             "WHERE MTCode=@MTCode", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 200)).Value = MTCode;

            SqlDataReader sdr = null;
            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    // The IEnumerable contains DataRowView objects.
                    //22
                    this.SDCode = sdr["SDCode"].ToString();
                    this.MTCode = sdr["MTCode"].ToString();
                    this.STCODE = sdr["STCODE"].ToString();
                    this.TestName = sdr["TestName"].ToString();
                    
                    if (!(sdr["TextDesc"] is DBNull))
                        this.TextDesc = Convert.ToString(sdr["TextDesc"]);


                    if (!(sdr["DefaultResult"] is DBNull))
                        this.DefaultResult = sdr["DefaultResult"].ToString();
                    else
                        this.Patregdate = Date.getdate();

                    if (!(sdr["DateOfEntry"] is DBNull))
                        this.Patregdate = Convert.ToDateTime(sdr["DateOfEntry"]);

                    if (!(sdr["Testordno"] is DBNull))
                        this.Testordno = Convert.ToInt32(sdr["Testordno"]);

                   

                    if (!(sdr["TestMethod"] is DBNull))
                        this.TestMethod = sdr["TestMethod"].ToString();



                    this.TestID = Convert.ToInt32(sdr["TestID"]);
                    this.TestName = sdr["TestName"].ToString();
                    this.STCODE = sdr["STCODE"].ToString();
                }
                else
                {
                    throw new SubTest_Bal_CTableException("No Record Fetched.");
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
                catch (SubTest_Bal_CTableException)
                {
                    throw new SubTest_Bal_CTableException("Record not found");
                }
            }
        } 

   

        public SubTest_Bal_C(object SDCode, object MTCode, object Testordno, object TestName)
        {
            this.SDCode = Convert.ToString(SDCode);
            this.MTCode = Convert.ToString(MTCode);
            this.Testordno = Convert.ToInt32(Testordno);
            this.TestName = Convert.ToString(TestName);
          
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand(" select * from SubTest " +
                             "WHERE SDCode= @SDCode and MTCode=@MTCode and Testordno=@Testordno and TestName=@TestName", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = this.SDCode;
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar,50)).Value = this.MTCode;
            sc.Parameters.Add(new SqlParameter("@Testordno", SqlDbType.Int, 5)).Value = Convert.ToInt32(this.Testordno);
            sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 200)).Value = this.TestName;


            SqlDataReader sdr = null;
            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    // The IEnumerable contains DataRowView objects.
                    //22
                    this.TestID = (int)sdr["TestID"];
                    this.TestName = sdr["TestName"].ToString();
                    this.STCODE = sdr["STCODE"].ToString();
                  
                    this.MTCode = sdr["MTCode"].ToString();
                    this.SDCode = sdr["SDCode"].ToString();
                    this.TextDesc = sdr["TextDesc"].ToString();
                    this.Patregdate = Date.getdate();
                    this.Testordno = (int)sdr["Testordno"];
                   
                    this.DefaultResult = sdr["DefaultResult"].ToString();
                 
                    this.TestMethod = sdr["TestMethod"].ToString();
                  
                }
                else
                {
                    throw new SubTest_Bal_CTableException("No Record Fetched.");
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
                catch (SubTest_Bal_CTableException)
                {
                    throw new SubTest_Bal_CTableException("Record not found");
                }
            }
        } 



      
        public bool Update()
        {
           
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "UPDATE SubTest " +
                "SET TestName=@TestName,STCODE=@STCODE,MTCode=@MTCode,SDCode=@SDCode,TextDesc=@TextDesc,DateOfEntry=@DateOfEntry,Testordno=@Testordno,DefaultResult=@DefaultResult,TestMethod=@TestMethod " +
               " WHERE TestID=@TestID and TestName=@TestName", conn);
            
            sc.Parameters.Add(new SqlParameter("@TestID", SqlDbType.Int, 9)).Value = (int)(this.TestID);
            sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 200)).Value = (string)(this.TestName);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);
           
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar,50)).Value = Convert.ToString(this.MTCode);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = (string)(this.SDCode);
            sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.NVarChar, 10)).Value = (string)(this.TextDesc);
            sc.Parameters.Add(new SqlParameter("@DateOfEntry", SqlDbType.DateTime)).Value = (DateTime)(this.Patregdate);
            sc.Parameters.Add(new SqlParameter("@Testordno", SqlDbType.Int, 5)).Value = (int)(this.Testordno);
          
            sc.Parameters.Add(new SqlParameter("@DefaultResult", SqlDbType.NText)).Value = (string)(this.DefaultResult);            
            sc.Parameters.Add(new SqlParameter("@TestMethod", SqlDbType.NVarChar, 50)).Value = (string)(this.TestMethod);
          

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
        } 


        public bool Update(string sTestName)
        {
            
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "UPDATE SubTest " +
                "SET STCODE =@STCODE, TestName=@TestName,MTCode=@MTCode,SDCode=@SDCode,TextDesc=@TextDesc,DateOfEntry=@DateOfEntry,Testordno=@Testordno,DefaultResult=@DefaultResult,TestMethod=@TestMethod,Shortform=@Shortform" +
               " WHERE TestName=@prevTestName", conn);
            // Add the employee ID parameter and set its value. 22

            sc.Parameters.Add(new SqlParameter("@prevTestName", SqlDbType.NVarChar, 200)).Value = (string)(sTestName);
            sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 200)).Value = (string)(this.TestName);
           
        
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar,50)).Value = Convert.ToString(this.MTCode);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.SDCode);

            if (this.TextDesc != null)
                sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.NVarChar, 10)).Value = (string)(this.TextDesc);
            else
                sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.NVarChar, 10)).Value = DBNull.Value;

            sc.Parameters.Add(new SqlParameter("@DateOfEntry", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.Patregdate);
            sc.Parameters.Add(new SqlParameter("@Testordno", SqlDbType.Int, 5)).Value = (int)(this.Testordno);
           

            if (this.DefaultResult != null)
                sc.Parameters.Add(new SqlParameter("@DefaultResult", SqlDbType.NText)).Value = (string)(this.DefaultResult);
            else
                sc.Parameters.Add(new SqlParameter("@DefaultResult", SqlDbType.NText)).Value = DBNull.Value;


           
            if (this.TestMethod != null)
                sc.Parameters.Add(new SqlParameter("@TestMethod", SqlDbType.NVarChar, 50)).Value = (string)(this.TestMethod);
            else
                sc.Parameters.Add(new SqlParameter("@TestMethod", SqlDbType.NVarChar, 50)).Value = DBNull.Value;

           
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(STCODE);

          
           
         
            if (this.P_shortform != null)
                sc.Parameters.Add(new SqlParameter("@Shortform", SqlDbType.NVarChar, 50)).Value = P_shortform;
            else
                sc.Parameters.Add(new SqlParameter("@Shortform", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
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
    public bool Update(int TestID)
        {
            
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "UPDATE SubTest " +
                "SET STCODE =@STCODE, TestName=@TestName,MTCode=@MTCode,SDCode=@SDCode,TextDesc=@TextDesc,DateOfEntry=@DateOfEntry,Testordno=@Testordno,DefaultResult=@DefaultResult,TestMethod=@TestMethod,Shortform=@Shortform" +
               " WHERE Testid=@testid", conn);
            // Add the employee ID parameter and set its value. 22
            sc.Parameters.Add(new SqlParameter("@testid", SqlDbType.Int)).Value = TestID;
            sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 200)).Value = (string)(this.TestName);
           
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar,50)).Value = Convert.ToString(this.MTCode);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.SDCode);

            if (this.TextDesc != null)
                sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.NVarChar, 10)).Value = (string)(this.TextDesc);
            else
                sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.NVarChar, 10)).Value = DBNull.Value;

            sc.Parameters.Add(new SqlParameter("@DateOfEntry", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.Patregdate);
            sc.Parameters.Add(new SqlParameter("@Testordno", SqlDbType.Int)).Value = (int)(this.Testordno);
           

            if (this.DefaultResult != null)
                sc.Parameters.Add(new SqlParameter("@DefaultResult", SqlDbType.NText)).Value = (string)(this.DefaultResult);
            else
                sc.Parameters.Add(new SqlParameter("@DefaultResult", SqlDbType.NText)).Value = DBNull.Value;


           

            if (this.TestMethod != null)
                sc.Parameters.Add(new SqlParameter("@TestMethod", SqlDbType.NVarChar, 50)).Value = (string)(this.TestMethod);
            else
                sc.Parameters.Add(new SqlParameter("@TestMethod", SqlDbType.NVarChar, 50)).Value = DBNull.Value;

         
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(STCODE);
           

            if (this.P_shortform != null)
                sc.Parameters.Add(new SqlParameter("@Shortform", SqlDbType.NVarChar, 50)).Value = P_shortform;
            else
                sc.Parameters.Add(new SqlParameter("@Shortform", SqlDbType.NVarChar, 50)).Value = DBNull.Value;

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
                    if (sdr != null) 
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
        public bool Delete()
        {
           
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("DELETE FROM SubTest " +
                             "WHERE TestID=@TestID and STCODE=@STCODE", conn);

            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.STCODE);
            sc.Parameters.Add(new SqlParameter("@TestID", SqlDbType.Int, 9)).Value = Convert.ToInt32(this.TestID);

            try
            {
                conn.Open();
                sc.ExecuteNonQuery();
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
            "INSERT INTO SubTest(TestName,STCODE,MTCode,SDCode,TextDesc,DateOfEntry,Testordno,DefaultResult,TestMethod,branchid,Shortform)" +
            "VALUES(@TestName,@STCODE,@MTCode,@SDCode,@TextDesc,@DateOfEntry,@Testordno,@DefaultResult,@TestMethod,@branchid,@Shortform)", conn);
                       // Add the employee ID parameter and set its value.

            sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 200)).Value = (string)(this.TestName);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);
           
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.MTCode);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 20)).Value = (string)(this.SDCode);
            sc.Parameters.Add(new SqlParameter("@TextDesc", SqlDbType.NVarChar, 10)).Value = (string)(this.TextDesc);
            sc.Parameters.Add(new SqlParameter("@DateOfEntry", SqlDbType.DateTime)).Value = (DateTime)(this.Patregdate);
            sc.Parameters.Add(new SqlParameter("@Testordno", SqlDbType.Int, 5)).Value = (int)(this.Testordno);
            
            sc.Parameters.Add(new SqlParameter("@DefaultResult", SqlDbType.NText)).Value = (string)(this.DefaultResult);
           
            sc.Parameters.Add(new SqlParameter("@TestMethod", SqlDbType.NVarChar, 50)).Value = (string)(this.TestMethod);
           
            sc.Parameters.Add(new SqlParameter("@TResult", SqlDbType.Bit, 1)).Value = true;
            //
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            
            if (this.P_shortform != null)
                sc.Parameters.Add(new SqlParameter("@Shortform", SqlDbType.NVarChar, 50)).Value = this.P_shortform;
            else
                sc.Parameters.Add(new SqlParameter("@Shortform", SqlDbType.NVarChar, 50)).Value = "";
           
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


  
    public string Filltxtresult(string txtres ,int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string prefixTextNew = txtres.Replace("'", "'+char(39)+'");
        string query = "select Description from stformmst  where shortform='" + prefixTextNew + "' and branchid='" + branchid + "'";
        
        SqlCommand cmd = new SqlCommand(query,conn);
        SqlDataReader sdr = null;
        conn.Open();
        sdr = cmd.ExecuteReader();
        
        try
        {
            while (sdr.Read())
            {
                 txtres= sdr["Description"].ToString();
          
            }
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close(); conn.Dispose();
        }
        return txtres;
    }

        //All properties

        internal class SubTest_Bal_CTableException : Exception
        {
            public SubTest_Bal_CTableException(string msg) : base(msg) { }
        }

        //properties
        #region properties

        private int testID;
        public int TestID
        {
            get { return testID; }
            set { testID = value; }
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

      

        private string mTCode;
        public string MTCode
        {
            get { return mTCode; }
            set { mTCode = value; }
        }

        private string sDCode;
        public string SDCode
        {
            get { return sDCode; }
            set { sDCode = value; }
        }

        private string textdesc;
        public string TextDesc
        {
            get { return textdesc; }
            set { textdesc = value; }
        }

        private DateTime _Patregdate;
        public DateTime Patregdate
        {
            get { return _Patregdate; }
            set { _Patregdate = value; }
        }

        private int testordno;
        public int Testordno
        {
            get { return testordno; }
            set { testordno = value; }
        }

        private string defaultResult;
        public string DefaultResult
        {
            get { return defaultResult; }
            set { defaultResult = value; }
        }
        
        private string testMethod;

        public string TestMethod
        {
            get { return testMethod; }
            set { testMethod = value; }
        }
       
        private string shortform; 
        public string P_shortform
            {
             get { return shortform; }
              set { shortform = value; }
             }
        #endregion

      
    }
