using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;


  public class CalculateSet_Bal_C
    {       

        public CalculateSet_Bal_C()
        {
            this.STCODE = "";
            this.Formula = "";
            this.ID = 0;
          
            this.SDCode = "";
         }


        public CalculateSet_Bal_C(object STCODE,int branchid)
        {
            this.STCODE = Convert.ToString(STCODE);
                        
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand(" select * from formst " +
                             "WHERE STCODE=@STCODE and and branchid=" + branchid + "", conn);
                       

            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar,50)).Value = (string)(this.STCODE);
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = (int)branchid;

            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    // The IEnumerable contains DataRowView objects.

                    this.STCODE = sdr["STCODE"].ToString();
                    this.Formula = sdr["Formula"].ToString();
                    this.ID = (int)sdr["ID"];
                  
                    this.SDCode = sdr["SDCode"].ToString();
                    this.branchid =(int)sdr["branchid"];
                }
                else
                {
                    throw new FormulaTbl1TableException("No Record Fetched.");
                }
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
                catch (FormulaTbl1TableException)
                {
                    throw new FormulaTbl1TableException("Record not found");
                }
            }
        } 
      public bool Update(int branchid)
        {
           
            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = new SqlCommand("" +
                "UPDATE formst " +
                             "SET STCODE=@STCODE,Formula=@Formula,ID=@id,SDCode=@SDCode WHERE STCODE=@STCODE and branchid=" + branchid + "", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar,50)).Value = (string)(this.STCODE);
            sc.Parameters.Add(new SqlParameter("@Formula", SqlDbType.NVarChar, 255)).Value = (string)(this.Formula);
            sc.Parameters.Add(new SqlParameter("@id", SqlDbType.Int, 4)).Value = (int)(this.ID);
           
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = this.SDCode;
          
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
      public bool Delete(int branchid)
        {
                      
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand("DELETE FROM formst " +
                             "WHERE ID=@ID and branchid=" + branchid + "", conn);                  
            sc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 4)).Value = (int)(this.ID);

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
            // Implement Update logic.
            return true;
        } 
      public bool Insert(int branchid)
        {
            
            SqlConnection conn = DataAccess.ConInitForDC();             
            SqlCommand sc = new SqlCommand("" +
            "INSERT INTO formst(STCODE,Formula,SDCode,branchid,username) VALUES(@STCODE,@Formula,@SDCode,@branchid,@username)", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar,100)).Value = (string)(this.STCODE);
            sc.Parameters.Add(new SqlParameter("@Formula", SqlDbType.NVarChar, 255)).Value = (string)(this.Formula);          
            
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = this.SDCode;           
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_username;
            SqlDataReader sdr = null;
            conn.Close();
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
        } //insert End

        public static DataSet getexp(string TesCode, int branchid)
        {
            SqlConnection con = DataAccess.ConInitForDC();
            SqlDataAdapter sda = new SqlDataAdapter("select id,Formula from formst where Formula like '%" + TesCode + "%' and branchid=" + branchid + "", con);
            DataSet ds = new DataSet();
            try
            {
                sda.Fill(ds);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return ds;
        }
        public void updateexp(int id, string Formula, int branchid)
        {
            SqlConnection con = DataAccess.ConInitForDC();
            SqlCommand cmd = new SqlCommand("update formst set Formula='" + Formula + "' where id=" + id + " and branchid=" + branchid + "", con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }

        }
        internal class FormulaTbl1TableException : Exception
        {
            public FormulaTbl1TableException(string msg) : base(msg) { }
        }

        private string sTCODE;

        public string STCODE
        {
            get { return sTCODE; }
            set { sTCODE = value; }
        }
        private string testname;

        public string TestName
        {
            get { return testname; }
            set { testname = value; }
        }
        private string username;
        public string P_username
        {
            get { return username; }
            set { username = value; }
        }
      private string sDCode;
      public string SDCode
        {
            get { return sDCode; }
            set { sDCode = value; }
        }

      private string _Formula;

      public string Formula
        {
            get { return _Formula; }
            set { _Formula = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
        private int _branchid;
        public int branchid
        {
            get { return _branchid; }
            set { _branchid = value; }
        }

     
      
    }//class


