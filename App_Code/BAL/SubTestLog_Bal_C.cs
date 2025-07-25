using System;
using System.Collections.Generic;
using System.Collections;
//using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public class SubTestLog_Bal_C
    {
    
    public static ICollection GetTestCode_SDCode(string SDCode, string MTCode, int branchid)
        {
            ArrayList tl = new ArrayList();
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;

            sc = new SqlCommand("select * from SubTest where SDCode =@SDCode and MTCode =@MTCode and branchid=" + branchid + " order by Testordno", conn);

            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 100)).Value = SDCode.Trim();
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode.Trim();
            
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
                        
                        SubTest_Bal_C tn = new SubTest_Bal_C();



                        //tn.MTCode = sdr["MTCode"].ToString();
                        tn.SDCode = sdr["SDCode"].ToString();

                       

                        if (!(sdr["TextDesc"] == DBNull.Value))
                            tn.TextDesc = Convert.ToString(sdr["TextDesc"]);

                        

                        if (!(sdr["DefaultResult"] == DBNull.Value))
                            tn.DefaultResult = sdr["DefaultResult"].ToString();



                        if (!(sdr["Dateofentry"] == DBNull.Value))
                            tn.Patregdate = Convert.ToDateTime(sdr["Dateofentry"]);

                        if (!(sdr["Testordno"] == DBNull.Value))
                            tn.Testordno = Convert.ToInt32(sdr["Testordno"]);

                     
                        if (!(sdr["TestMethod"] == DBNull.Value))
                            tn.TestMethod = sdr["TestMethod"].ToString();



                        tn.TestID = Convert.ToInt32(sdr["TestID"]);
                        tn.TestName = sdr["TestName"].ToString();
                        tn.STCODE = sdr["STCODE"].ToString();
                        tn.MTCode = sdr["MTCode"].ToString();

                        tl.Add(tn);

                    }
                    // The IEnumerable contains DataRowView objects.
                    //cn


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
            //tl.Sort();
            return tl;

        }//gettstnam 

   
        public static ICollection Get_AllSubTest(string MTCode, int branchid)
        {            
            ArrayList tl = new ArrayList();
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand();
            SqlDataReader sdr = null;

            sc.Connection = conn;
            sc.CommandText = "select * from SubTest where MTCode=@MTCode and branchid=" + branchid + " order by Testordno";
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {
                    while (sdr.Read())
                    {
                        SubTest_Bal_C test = new SubTest_Bal_C();
                        test.STCODE = sdr["STCODE"].ToString();
                        test.TestID =Convert.ToInt32(sdr["TestID"].ToString());
                        test.TestName = sdr["TestName"].ToString();
                        test.Testordno =Convert.ToInt32(sdr["Testordno"].ToString());
                        test.TestMethod = sdr["TestMethod"].ToString();
                       
                        test.MTCode = sdr["MTCode"].ToString();
                        tl.Add(test);
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
        }
      
       
        public static string GetTestName(string STCODE,int branchid)
        {
            string testname = "";
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;

            sc = new SqlCommand("select testname from SubTest where STCODE=@STCODE and branchid=" + branchid + "", conn);

            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;

            try
            {
                conn.Open();
                testname = Convert.ToString(sc.ExecuteScalar());
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
            return testname;
        }
        public static string GetTestName(string STCODE, string MTCode, string testname, int branchid, int p)
        {            
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;
            sc = new SqlCommand("select testname from SubTest where STCODE=@STCODE and testname=@testname and MTCode=@MTCode and branchid=" + branchid + "", conn);

            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
            sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 50)).Value = testname;
            try
            {
                conn.Open();
                testname = Convert.ToString(sc.ExecuteScalar());
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
            return testname;
        }
    public static void UpdateTest_againstsorting(string MTCode, int Testordno, int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("update SubTest set Testordno=" + Testordno + " where MTCode='" + MTCode + "' and branchid=" + branchid + "", con);
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
    public static void UpdateTest_againstsorting1(string MTCode, string pname, int Testordno, int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("update SubTest set Testordno=" + Testordno + " where STCODE='" + MTCode + "' and testname='" + pname + "' and branchid=" + branchid + "", con);
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
    public static string GetTestcodeforsubtest(string testname, int branchid, string SDCode)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select STCODE from SubTest where testname='" + testname.Trim() + "' and branchid=" + branchid + "", con);
        string STCODE = "";
        object obj = null;
        try
        {
            con.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                STCODE = obj.ToString();
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
        if (STCODE == "")
        {
            con = DataAccess.ConInitForDC();
            cmd = new SqlCommand("select MTCode from MainTest where Maintestname='" + testname.Trim() + "' and SDCode='" + SDCode + "' and branchid=" + branchid + "", con);
            try
            {
                con.Open();
                obj = cmd.ExecuteScalar();
                if (obj != null)
                    STCODE = obj.ToString();
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

        return STCODE;
    }

    public static string GetTestcodeforsubtest(string testname, int branchid, string SDCode, string MTCode)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select STCODE from SubTest where MTCode='" + MTCode + "' and testname='" + testname.Trim() + "' and branchid=" + branchid + "", con);
        string STCODE = "";
        object obj = null;
        try
        {
            con.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                STCODE = obj.ToString();
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
        if (STCODE == "")
        {
            con = DataAccess.ConInitForDC();
            cmd = new SqlCommand("select MTCode from MainTest where MTCode='" + MTCode + "' and Maintestname='" + testname.Trim() + "' and SDCode='" + SDCode + "' and branchid=" + branchid + "", con);
            try
            {
                con.Open();
                obj = cmd.ExecuteScalar();
                if (obj != null)
                    STCODE = obj.ToString();
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

        return STCODE;
    }


    public static bool Existsubtest_ST(string tname, string STCODE, string MTCode, int branchid)
    {
      
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SELECT count(*) FROM SubTest WHERE STCODE=@STCODE and MTCode=@MTCode and testname=@testname and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 200)).Value = tname;

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
    public static bool IS_SubtestExist(string T_Name, int testid, string MTCode, int branchid)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM SubTest " +
                         " WHERE TestName=@TestName and TestID <> @TestID and MTCode=@MTCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 200)).Value = T_Name;
        sc.Parameters.Add(new SqlParameter("@TestID", SqlDbType.Int, 9)).Value = testid;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

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

    public static void Update_Packageno(SubTest_Bal_C STBC, int iOrdNo, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string s = "";
        if (STBC.Testordno < iOrdNo)
            s = "Update SubTest Set Testordno = Testordno + 1 Where Testordno>=@Tstordno and ORDNO <@TstordnoCurr and MTCode='" + STBC.MTCode + "' and branchid=" + branchid + "";
        else if (STBC.Testordno > iOrdNo)
            s = "Update SubTest Set Testordno = Testordno - 1 Where Testordno>@TstordnoCurr and ORDNO <=@Tstordno and MTCode='" + STBC.MTCode + "'and branchid=" + branchid + " ";
        SqlCommand sc = new SqlCommand(s, conn);
        sc.Parameters.Add(new SqlParameter("@Tstordno", SqlDbType.Int, 5)).Value = STBC.Testordno;
        sc.Parameters.Add(new SqlParameter("@TstordnoCurr", SqlDbType.Int, 5)).Value = iOrdNo;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception)
        {

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
    public static string GetSDCode_AgainsMainTest(string MTCode, object branchid)
    {
     
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;

        sc = new SqlCommand("select SDCode from MainTest where MTCode=@MTCode and branchid=@branchid", conn);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

        try
        {
            conn.Open();
            MTCode = Convert.ToString(sc.ExecuteScalar());
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
        return MTCode;
    }
    public static bool IS_Subtestcode_Exists(string STCODE, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM SubTest " +
                         " WHERE STCODE=@STCODE and branchid=" + branchid + "", conn);


        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;

        int cnt = 0;

        try
        {
            conn.Open();
            cnt = Convert.ToInt32(sc.ExecuteScalar());

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

    public static bool IS_Subtestcode_Exists(string STCODE, int Testid, int branchid)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM SubTest " +
                         " WHERE STCODE=@STCODE and TestID <> @TestID and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
        sc.Parameters.Add(new SqlParameter("@TestID", SqlDbType.Int, 9)).Value = Testid;

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
    public static bool IS_SubtestExist(string T_Name, string MTCode, int branchid)
    {
      
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM SubTest " +
                         " WHERE TestName=@TestName and MTCode=@MTCode and branchid=" + branchid + "", conn);

       
        sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 200)).Value = T_Name;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

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


    public static bool Existsubtest_ST_Space(string tname, string STCODE, string MTCode, int branchid,int ONo)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SELECT count(*) FROM SubTest WHERE STCODE=@STCODE and MTCode=@MTCode and testname=@testname and Testordno=@Testordno  and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 200)).Value = tname;
        sc.Parameters.Add(new SqlParameter("@Testordno", SqlDbType.Int)).Value = ONo;
        

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

