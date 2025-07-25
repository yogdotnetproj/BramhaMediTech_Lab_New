using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


    public class Patientinitial_Bal_C
    {
        internal class Patientinitial_Bal_CException : Exception
        {
            public Patientinitial_Bal_CException(string msg) : base(msg) { }
        }
        private string _prefix = "";
        private string _Sex = "";
        private int _AgeRange=0;

        public Patientinitial_Bal_C()
        {
            _prefix = "";
            _Sex = "";
            _AgeRange = 0;
        }
        public Patientinitial_Bal_C(String sprefix)
        {
            this.prefixName = sprefix;
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand(" SELECT prefix , sex from initial" +
                             " WHERE prefix = @prefix ", conn);

            // Add the employee ID parameter and set its value.

            sc.Parameters.Add(new SqlParameter("@prfix", SqlDbType.NVarChar, 10)).Value = (string)(sprefix);

            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    // The IEnumerable contains DataRowView objects.
                    this.prefixName= sdr["prefix"].ToString();
                    this.Sex = sdr["Sex"].ToString();
                    this._AgeRange = Convert.ToInt32(sdr["AgeRange"]);
                }
                else
                {
                    throw new Patientinitial_Bal_CException("No Record Fetched.");
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
                catch (Patientinitial_Bal_CException)
                {
                    throw new Patientinitial_Bal_CException("Record not found");
                }
            }
        }

        public string prefixName
        {
            get { return _prefix; }
            set { _prefix = value; }
        }

        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        public int AgeRange
        {
            get { return _AgeRange;}
            set { _AgeRange = value;}
        }

        public static short getinitialCount()
        {
            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = new SqlCommand(" SELECT count(prefix) from initial", conn);

            // Add the employee ID parameter and set its value.

            short cnt;
            try
            {
                conn.Open();
                cnt =Convert.ToInt16(sc.ExecuteScalar());
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
                catch (Patientinitial_Bal_CException)
                {
                    throw new Patientinitial_Bal_CException("Record not found");
                }
            }
            return cnt;
        }
        public bool Insert()
        {
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
            "insert into initial(prefix)" +
            "values(@prefix)", conn);

            // Add the employee ID parameter and set its value.

            sc.Parameters.Add(new SqlParameter("@prfix", SqlDbType.NVarChar, 10)).Value = (string)(this.prefixName);

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
            return true;
        } //insert End

        public bool Update(string sPrevprefix)
        {

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
            "update initial set prefix=@prefix where prefix=@sPrevprefix", conn);

            // Add the employee ID parameter and set its value.

            sc.Parameters.Add(new SqlParameter("@sPrevprefix", SqlDbType.NVarChar, 10)).Value = (sPrevprefix);
            sc.Parameters.Add(new SqlParameter("@prefix", SqlDbType.NVarChar, 10)).Value = (this.prefixName);

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
            return true;
        } //insert End

    }
//}
