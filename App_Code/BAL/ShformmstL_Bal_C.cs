using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;


    public class ShformmstL_Bal_C
    {

        public static ICollection getTemp_ShortTest()
        {
            ArrayList al = new ArrayList();
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand(" SELECT * from ResMst order by testorderno", conn);

            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                if (sdr != null)
                {
                    while (sdr.Read())
                    {
                        Stformmst_Bal_C SFBC = new Stformmst_Bal_C();

                        SFBC.STCODE = sdr["STCODE"].ToString();
                        SFBC.MTCode = sdr["MTCode"].ToString();
                        SFBC.TestResult_Format = sdr["TestResult_Format"].ToString();
                        SFBC.PatRegID = sdr["Patregid"].ToString();
                        SFBC.FID = sdr["FID"].ToString();
                        SFBC.EntryDate = (DateTime)(sdr["EntryDate"]);
                      
                        SFBC.TestNo = Convert.ToInt32(sdr["TestNo"]);
                        SFBC.testname = sdr["testname"].ToString();
                        SFBC.normalRange = sdr["normalRange"].ToString();
                        SFBC.unit = sdr["unit"].ToString();
                        SFBC.testorderno = Convert.ToInt32(sdr["testorderno"]);
                      
                        SFBC.FinancialYearID = sdr["FinancialYearID"].ToString();
                       
                        SFBC.Branchid = Convert.ToInt32(sdr["Branchid"]);
                      
                        SFBC.SDCode = sdr["SDCode"].ToString();
                      

                        al.Add(SFBC);
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
                    throw;
                }
                catch (Exception)
                {
                    throw new Exception("Record not found");
                }
            }
            return al;
        }



        public static void insertRemark(string MTCode, string Patregid, string FID, string remark, int branchid)
        {
            string Previous = "";

            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("UPDATE ResMst set Remarks='" + remark + "',RemarkFlag=1 WHERE MTCode='" + MTCode + "' and Patregid='" + Patregid + "' and FID='" + FID + "' and branchid=" + branchid + "", conn);

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
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static string GetRemark(string MTCode, string Patregid, string FID, int branchid)
        {
            string remark = "";

            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("Select Remarks from ResMst where MTCode='" + MTCode + "' and Patregid='" + Patregid + "' and FID='" + FID + "'  and branchid=" + branchid + "", conn);

            try
            {
                conn.Open();
                remark = sc.ExecuteScalar().ToString();
            }
            finally
            {
                try
                {
                    conn.Close(); conn.Dispose();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return remark;
        }



        public static string Get_Maintestfrom_ResMst(string MTCode, string Patregid, string FID, int branchid)
        {
            string Maintestname = "";
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = null;

            sc = new SqlCommand("SELECT Maintestname FROM ResMst WHERE Patregid=@Patregid AND FID=@FID and MTCode=@MTCode  and branchid=" + branchid + "", conn);

            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 50)).Value = MTCode;
            sc.Parameters.Add(new SqlParameter("@Patregid", SqlDbType.NVarChar, 50)).Value = Patregid;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = FID;
            SqlDataReader sdr = null;
            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                if (sdr != null)
                {
                    while (sdr.Read())
                    {
                        Maintestname = Convert.ToString(sdr["Maintestname"]);

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

            }
            //tl.Sort();
            return Maintestname;
        }
        public static void Update_ResMSt_whenSort(string MTCode, int Testordno, int branchid)
        {
            SqlConnection con = DataAccess.ConInitForDC();
            SqlCommand cmd = new SqlCommand("update ResMst set testno=" + Testordno + " where MTCode='" + MTCode + "' and branchid=" + branchid + "", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        public static void Update_testforResMst_sort(string STCODE, int ordno, int branchid)
        {
            SqlConnection con = DataAccess.ConInitForDC();
            SqlCommand cmd = new SqlCommand("update ResMst set testorderno=" + ordno + " where STCODE='" + STCODE + "' and branchid=" + branchid + "", con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }
   

    }
