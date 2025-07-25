using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;

    public class PatientinitialLogic_Bal_C
    {
        public static ICollection getInitial()
        {
            
            ArrayList al = new ArrayList();
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand(" SELECT prefix as prefix from initial order by initialcode", conn);           

            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null)
                {
                    while (sdr.Read())
                    {
                        Patientinitial_Bal_C PBC = new Patientinitial_Bal_C();                       
                        PBC.prefixName = sdr["prefix"].ToString();                        
                        al.Add(PBC);
                    }                    
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
                catch (Exception)
                {
                    throw new Exception("Record not found");
                }
            }
            return al;
        }

        public static ICollection getSex(int branchid)
        {
            ArrayList S = new ArrayList();

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand(" SELECT distinct Sex from initial where branchid=" + branchid + "", conn);

            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null)
                {

                    while (sdr.Read())
                    {
                        Patientinitial_Bal_C PBC = new Patientinitial_Bal_C();                       
                        PBC.Sex = sdr["Sex"].ToString();
                        S.Add(PBC);
                    }
                    
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
                catch (Exception)
                {
                    throw new Exception("Record not found");
                }
            }
            return S;
        }

        /////
        public static ICollection getInitialSex(string prefixval, int branchid)
        {

            ArrayList al = new ArrayList();

            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand(" SELECT prefix as prefix,sex from initial where prefix=@prefix and branchid=" + branchid + "", conn);

            sc.Parameters.Add(new SqlParameter("@prefix", SqlDbType.NVarChar, 50)).Value =(string) prefixval;

            // Add the employee ID parameter and set its value.

            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null)
                {

                    while (sdr.Read())
                    {
                        Patientinitial_Bal_C PBC = new Patientinitial_Bal_C();                       
                        PBC.Sex = sdr["Sex"].ToString();
                        PBC.prefixName = sdr["prefix"].ToString(); 
                        
                        al.Add(PBC);
                    }
                    
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
                catch (Exception)
                {
                    throw new Exception("Record not found");
                }
            }
            return al;
        }

     
        public static string SelectSex(string prefix)
        {
            string se = null;
            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = new SqlCommand("SELECT sex from initial" +
                             " WHERE prefix = @prefix", conn);

            sc.Parameters.Add(new SqlParameter("@prefix", SqlDbType.NVarChar, 50)).Value = prefix;
           
            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();
                if (sdr.Read())
                {
                   se = Convert.ToString(sdr.GetValue(0).ToString());

                }
            }
            catch
            {

            }
            return se;
        }
    }

