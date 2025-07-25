using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

    static public class Date
    {
       
        public static DateTime getdate()
        {
            DateTime d1 = System.DateTime.Now;           
            return d1;

        } 
        public static DateTime getOnlydate()
        {
            DateTime d1;
            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = new SqlCommand("SELECT getdate()", conn);
            conn.Open();

            // Add the employee ID parameter and set its value.
            d1 = (DateTime)sc.ExecuteScalar();
            d1 = d1.Date;
            conn.Close(); conn.Dispose();
            return d1;

        }
         public static string getTime()
        {
            string t = "";
            DateTime d1;
            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = new SqlCommand("SELECT getdate()", conn);
            conn.Open();

            // Add the employee ID parameter and set its value.
            d1 = (DateTime)sc.ExecuteScalar();
            t = d1.ToLongTimeString();
            conn.Close(); conn.Dispose();
            return t;

        }

        public static string getYear()
        {
            string t = "";
            DateTime d1;
            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = new SqlCommand("SELECT getdate()", conn);
            conn.Open();

            // Add the employee ID parameter and set its value.
            d1 = (DateTime)sc.ExecuteScalar();
            t = d1.Year.ToString();
            conn.Close(); conn.Dispose();
            return t;

        }
        public static DateTime getMinDate()
        {
            DateTime d1;
            d1 = Convert.ToDateTime("01/01/1753");
            return d1;

        }
          
               
    }

