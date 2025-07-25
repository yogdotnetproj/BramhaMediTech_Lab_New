using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

  public  class Stformmst_Main_Bal_C
    {
        public Stformmst_Main_Bal_C()
        {          
          
            this.Description = "";
          
            this.Shortform = "";
            this.branchid = 0;
        }



        public bool Update(string Shortform, string Description, string Category, int shid, string testname, string MTCode, string ParaCode)
        { 
            SqlConnection conn = DataAccess.ConInitForDC();            
            SqlCommand sc = new SqlCommand("" +
                "Update stformmst  " +
                "set shortform=@shortform,Description=@Description,MTCode=@MTCode,ParaCode=@ParaCode " +
                "where ShortFormID=@ShortFormID", conn);

            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@shortform", SqlDbType.NVarChar, 255)).Value = Shortform;
            sc.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 255)).Value = Description;

            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 255)).Value = MTCode;
            sc.Parameters.Add(new SqlParameter("@ParaCode", SqlDbType.NVarChar, 255)).Value = ParaCode;
           
            sc.Parameters.Add(new SqlParameter("@ShortFormID", SqlDbType.Int, 9)).Value = shid;

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
                    conn.Close();
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
    
      public bool delete(string Testname, object branchid)
        {
           
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("delete from stformmst  " +
                "where Testname=@Testname and branchid=@branchid", conn);
                     
            sc.Parameters.Add(new SqlParameter("@Testname", SqlDbType.NVarChar, 255)).Value = (string)Testname;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
            try
            {
                conn.Open();
                sc.ExecuteNonQuery();
            }
            finally
            {
                try
                {
                    conn.Close();
                }
                catch (SqlException)
                {
                    // Log an event in the Application Event Log.
                    throw;
                }
            }
            
            return true;
        } 

        public bool deleteGenShortForm(object branchid,int shid)
        {
           
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("delete from stformmst  " +
                "where ShortFormID=@ShortFormID", conn);
                        
            sc.Parameters.Add(new SqlParameter("@ShortFormID", SqlDbType.Int, 9)).Value = shid;
           
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@shortform", SqlDbType.NVarChar, 255)).Value = Shortform;
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
                    conn.Close();
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

      public static string cheak(string Testname, object branchid)
      {
          
          SqlConnection conn = DataAccess.ConInitForDC();
          SqlCommand sc = new SqlCommand("select Testname from stformmst  where Testname=@Testname and branchid=@branchid", conn);
          // Add the employee ID parameter and set its value.

          sc.Parameters.Add(new SqlParameter("@Testname", SqlDbType.NVarChar, 250)).Value = (string)(Testname);
          sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
          string flag;
          try
          {
              conn.Open();
              string str = Convert.ToString(sc.ExecuteScalar());
              if (str != "")
                  flag = "true";
              else
                  flag = "false";

              // This is not a while loop. It only loops once.
          }
          finally
          {
              try
              {
                  conn.Close();
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
          return flag;
      }



    

      public bool Insert()
      {

          SqlConnection conn = DataAccess.ConInitForDC();         

          SqlCommand sc = new SqlCommand("" +
          "INSERT INTO stformmst  (shortform,Description, branchid,MTCode,ParaCode)" +
          "values(@shortform,@Description, @branchid,@MTCode,@ParaCode)", conn);

          // Add the employee ID parameter and set its value.

          sc.Parameters.Add(new SqlParameter("@shortform", SqlDbType.NVarChar, 255)).Value = (string)(this.Shortform);
          sc.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 255)).Value = (string)(this.Description);
          
          sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = this.branchid;
          sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 255)).Value = (string)(this.MainTest);
          sc.Parameters.Add(new SqlParameter("@ParaCode", SqlDbType.NVarChar, 255)).Value = (string)(this.SubTest);

          
          
          SqlDataReader sdr = null;

          try
          {
              conn.Open();
              try
              {
                  sc.ExecuteNonQuery();
              }
              catch { throw; }

          }
          finally
          {
              try
              {
                  if (sdr != null) sdr.Close();
                  conn.Close();
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

      public bool Insert1()
      {
          SqlConnection conn = DataAccess.ConInitForDC();          
          SqlCommand sc = new SqlCommand("" +
          "INSERT INTO stformmst  (shortform,Description,branchid)" +
          "values(@shortform,@Description,@branchid)", conn);
          
          sc.Parameters.Add(new SqlParameter("@shortform", SqlDbType.NVarChar, 255)).Value = (string)(this.Shortform);
          sc.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, 255)).Value = (string)(this.Description);
          
          sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = this.branchid;
        
          SqlDataReader sdr = null;

          try
          {
              conn.Open();
              try
              {
                  sc.ExecuteNonQuery();
              }
              catch { throw; }

          }
          finally
          {
              try
              {
                  if (sdr != null) sdr.Close();
                  conn.Close();
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


        //All properties

        internal class ShortformsTableException : Exception
        {
            public ShortformsTableException(string msg) : base(msg) { }
        }

        private string shortform;
        public string Shortform
        {
            get { return shortform; }
            set { shortform = value; }
        }

        private string _MainTest;
        public string MainTest
        {
            get { return _MainTest; }
            set { _MainTest = value; }
        }
        private string _SubTest;
        public string SubTest
        {
            get { return _SubTest; }
            set { _SubTest = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        

       
        private int shortFormID;

        public int ShortFormID
        {
            get { return shortFormID; }
            set { shortFormID = value; }
        }
      private int _branchid;
      public int branchid
      {
          get { return _branchid; }
          set { _branchid = value; }
      }
      private string username;
      public string P_username
      {
          get { return username; }
          set { username = value; }
      }
    
        
    }//class DrMT_Bal_C

