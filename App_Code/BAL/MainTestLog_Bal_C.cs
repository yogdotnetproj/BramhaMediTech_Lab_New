using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public class MainTestLog_Bal_C
{    
    
    public static ICollection getAllSampleType()
    {
       
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        SqlDataReader sdr = null;

        sc.Connection = conn;
        sc.CommandText = "Select distinct SampleType from SubTest order by SampleType";


        try
        {
            conn.Open();

            sdr = sc.ExecuteReader();

            if (sdr != null)
            {
                while (sdr.Read())
                {
                    tl.Add(sdr["SampleType"].ToString());

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
   

   
    public static ICollection Get_MainTest(string Maintestname, int branchid, string SDCode)
    {
        ArrayList tl = new ArrayList();
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand("select * from MainTest where Maintestname='" + Maintestname.Trim() + "' and branchid=" + branchid + "", conn);
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
                    MainTest_Bal_C MBC = new MainTest_Bal_C();

                    MBC.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                    MBC.Maintestname = sdr["Maintestname"].ToString();
                    MBC.MTCode = sdr["MTCode"].ToString();
                    MBC.SDCode = sdr["SDCode"].ToString();
                    MBC.Testordno = Convert.ToInt32(sdr["Testordno"]);
                    MBC.TextDesc = sdr["TextDesc"].ToString();
                    MBC.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();

                    if (!(sdr["samecontain"] is DBNull))
                        MBC.Samecontain = Convert.ToInt32(sdr["samecontain"]);

                    if (!(sdr["Shortcode"] is DBNull))
                        MBC.Shortcode = sdr["Shortcode"].ToString();

                    MBC.Singleformat = sdr["SingleFormat"].ToString();
                    MBC.SampleType = sdr["SampleType"].ToString();




                    tl.Add(MBC);

                }

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

        return tl;
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

   
    public static bool isinmdept(string subdeptName,int mdept,int branchid)
    {
        if (mdept == 0)
            return true;

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select count(*) from SubDepartment where subdeptName='" + subdeptName.Trim() + "' and maindeptid=" + mdept + " and branchid=" + branchid + "", conn);
        int count = 0;
        try
        {
            conn.Open();
            count = Convert.ToInt32(cmd.ExecuteScalar());
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        if (count > 0)
            return true;
        else
            return false;
    }
     public static ICollection getparamnames(string MTCode, int branchid)
    {
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlDataReader sdr = null;
        SqlCommand sc = new SqlCommand("select * from SubTest where MTCode='" + MTCode + "' and branchid=" + branchid + "  order by Testordno", conn);
        try
        {
            
            conn.Open();
            try
            {
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null)
                {
                    while (sdr.Read())
                    {
                      
                        SubTest_Bal_C SBTC = new SubTest_Bal_C();

                        SBTC.TestID = Convert.ToInt32(sdr["TestID"]);
                        SBTC.TestName = sdr["TestName"].ToString();
                        SBTC.STCODE = sdr["STCODE"].ToString();

                       
                        if (!(sdr["TextDesc"] is DBNull))
                            SBTC.TextDesc = sdr["TextDesc"].ToString();


                        if (!(sdr["DateOfEntry"] is DBNull))
                            SBTC.Patregdate = Convert.ToDateTime(sdr["DateOfEntry"]);

                        if (!(sdr["DefaultResult"] is DBNull))
                            SBTC.DefaultResult = sdr["DefaultResult"].ToString();
                                               

                        if (!(sdr["Testordno"] is DBNull))
                            SBTC.Testordno = Convert.ToInt32(sdr["Testordno"]);
                        

                        SBTC.TestName = sdr["TestName"].ToString();

                        tl.Add(SBTC);

                    }
                   
                }
            }
            catch (Exception ex) { throw; }
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
        return tl;
    }

   
    public static ICollection gettestcode(string MTCode, int branchid, string SDCode)
    {
        ArrayList al = new ArrayList();
        SubTest_Bal_C SBT = new SubTest_Bal_C();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select STCODE,testname from SubTest where MTCode='" + MTCode + "' and SDCode='" + SDCode + "' and branchid=" + branchid + "", conn);
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = cmd.ExecuteReader();
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    SBT.STCODE = sdr["STCODE"].ToString();
                    SBT.TestName = sdr["testname"].ToString();
                    al.Add(SBT);
                }
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
        
        return al;
    }
    public static ICollection GetMaintest_SDCode(string SDCode, int branchid)
    {
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        sc = new SqlCommand();

        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "SP_phttsdwise";
        sc.Connection = conn;

        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 200)).Value = SDCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
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
                    MainTest_Bal_C MBC = new MainTest_Bal_C();

                    MBC.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                    MBC.Maintestname = sdr["Maintestname"].ToString();
                    MBC.MTCode = sdr["MTCode"].ToString();
                    MBC.SDCode = sdr["SDCode"].ToString();
                    if (sdr["Testordno"] != DBNull.Value)

                        MBC.Testordno = Convert.ToInt32(sdr["Testordno"]);
                    else
                        MBC.Testordno = 0;
                    MBC.TextDesc = sdr["TextDesc"].ToString();
                    MBC.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();
                   
                    if (!(sdr["samecontain"] is DBNull))
                        MBC.Samecontain = Convert.ToInt32(sdr["samecontain"]);
                   
                    if (!(sdr["Shortcode"] is DBNull))
                        MBC.Shortcode = sdr["Shortcode"].ToString();
                  
                    MBC.Singleformat = sdr["SingleFormat"].ToString();
                    MBC.SampleType = sdr["SampleType"].ToString();
                   

                    
                    tl.Add(MBC);

                }
                // The IEnumerable contains DataRowView objects.
                
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
    }


    public static ICollection GetAllMaintest_SDCode(string SDCode, int branchid)
    {
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;

        sc = new SqlCommand();

        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "SP_GetAllTest";
        sc.Connection = conn;

        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 200)).Value = SDCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
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
                    MainTest_Bal_C MBC = new MainTest_Bal_C();

                    MBC.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                    MBC.Maintestname = sdr["Maintestname"].ToString();
                    MBC.MTCode = sdr["MTCode"].ToString();
                    MBC.SDCode = sdr["SDCode"].ToString();
                    if (sdr["Testordno"] != DBNull.Value)

                        MBC.Testordno = Convert.ToInt32(sdr["Testordno"]);
                    else
                        MBC.Testordno = 0;
                    MBC.TextDesc = sdr["TextDesc"].ToString();
                    MBC.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();

                    if (!(sdr["samecontain"] is DBNull))
                        MBC.Samecontain = Convert.ToInt32(sdr["samecontain"]);

                    if (!(sdr["Shortcode"] is DBNull))
                        MBC.Shortcode = sdr["Shortcode"].ToString();

                    MBC.Singleformat = sdr["SingleFormat"].ToString();
                    MBC.SampleType = sdr["SampleType"].ToString();



                    tl.Add(MBC);

                }
                // The IEnumerable contains DataRowView objects.

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
    }

   
    public static ICollection GetMaintestBy_Code(string MTCode, object branchid)
    {
        if (branchid != null)
        {
            branchid = Convert.ToInt32(branchid);
        }
        else
        {
            branchid = 0;
        }
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;
        sc = new SqlCommand("select * from MainTest where MTCode=@MTCode and branchid=@branchid ORDER BY Testordno", conn);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 200)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

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
                    MainTest_Bal_C MBC = new MainTest_Bal_C();                                     

                    MBC.Maintestid = Convert.ToInt32(sdr["Maintestid"]);
                    MBC.Maintestname = sdr["Maintestname"].ToString();
                    
                    MBC.Singleformat = Convert.ToString(sdr["Singleformat"]);
                    MBC.SDCode = sdr["SDCode"].ToString();
                    MBC.MTCode = sdr["MTCode"].ToString();


                    

                    if (!(sdr["samecontain"] == DBNull.Value))
                        MBC.Samecontain = Convert.ToSingle(sdr["samecontain"]);

                    if (!(sdr["TextDesc"] == DBNull.Value))
                        MBC.TextDesc = sdr["TextDesc"].ToString();



                    if (!(sdr["DateOfEntry"] == DBNull.Value))
                        MBC.Patregdate = Convert.ToDateTime(sdr["DateOfEntry"]);

                    if (!(sdr["DefaultResult"] == DBNull.Value))
                        MBC.DefaultResult = sdr["DefaultResult"].ToString();

                   

                    if (!(sdr["Testordno"] == DBNull.Value))
                        MBC.Testordno = Convert.ToInt32(sdr["Testordno"]);


                    if (!(sdr["DefaultTestMethod"] == DBNull.Value))
                        MBC.DefaultTestMethod = sdr["DefaultTestMethod"].ToString();

                    if (!(sdr["Shortcode"] == DBNull.Value))
                        MBC.Shortcode = sdr["Shortcode"].ToString();

                                      

                    tl.Add(MBC);

                }
                // The IEnumerable contains DataRowView objects.
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
      
        return tl;
    }

  
    
    public static ICollection getGrideValue(string SDCode, string selecteddrcode, object branchid)
    {
        ArrayList al = new ArrayList();
        ArrayList al2 = new ArrayList();
        int c = 1;
       
        #region maintest
       

        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        if (SDCode.Trim() != "PD")
        {
            sc = new SqlCommand("select Maintestname,MTCode from MainTest where SDCode=@SDCode and branchid=@branchid ORDER BY Maintestname", conn);
            
        }
        else
        {
            sc = new SqlCommand("select PackageName as Maintestname,PackageCode as MTCode from PackMst where branchid=@branchid ORDER BY Maintestname", conn);
        }
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SDCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        // Add the employee ID parameter and set its value.

        SqlDataReader sdr = null;

        conn.Close();  

        MainTest_Bal_C sft = null;

        try
        {
            conn.Open();

            sdr = sc.ExecuteReader();

            if (sdr != null)
            {
                while (sdr.Read()) 
                {
                    sft = new MainTest_Bal_C();

                    sft.Maintestname = sdr["Maintestname"].ToString();
                    sft.MTCode = sdr["MTCode"].ToString();


                    al2.Add(sft);
                    c++;

                    
                }
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }              

        conn.Close();
        sdr.Close();
        #endregion

        for (int i = 0; i < al2.Count; i++)
        {
            try
            {

                conn.Open();
                sc = new SqlCommand("select * from TestCharges where STCODE=@arraytlcode and drcode=@selecteddrcode and branchid=@branchid order by Testname", conn);

                sc.Parameters.Add(new SqlParameter("@arraytlcode", SqlDbType.NVarChar, 5)).Value = (al2[i] as MainTest_Bal_C).MTCode;
                sc.Parameters.Add(new SqlParameter("@selecteddrcode", SqlDbType.NVarChar, 3)).Value = selecteddrcode;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
                try
                {
                    //record found then take from special charges
                    sdr = sc.ExecuteReader();

                    if (sdr != null)
                    {
                        if (sdr.Read())
                        {
                            //sft = new MainTest_Bal_C();
                            SpeCh_Bal_C SBC = new SpeCh_Bal_C();

                            if ((sdr["DrName"]) is DBNull)
                                SBC.RateType = "";
                            else
                                SBC.RateType = Convert.ToString(sdr["DrName"]);

                            if ((sdr["Amount"]) is DBNull)
                                SBC.Amount = 0;
                            else
                                SBC.Amount = Convert.ToInt32(sdr["Amount"]);

                            if ((sdr["Percentage"]) is DBNull)
                                SBC.Percentage = 0;
                            else
                                SBC.Percentage = Convert.ToSingle(sdr["Percentage"]);

                            if ((sdr["Emergency"]) is DBNull)
                                SBC.Emergency = 0;
                            else
                                SBC.Emergency = Convert.ToInt32(sdr["Emergency"]);
                                                     

                            SBC.TestName = (al2[i] as MainTest_Bal_C).Maintestname;
                            SBC.STCODE = (al2[i] as MainTest_Bal_C).MTCode;

                            al.Add(SBC);
                        }
                        else
                        {
                            SpeCh_Bal_C SB_C = new SpeCh_Bal_C();
                            DrMT_Bal_C dr1 = new DrMT_Bal_C();
                            //dr1.DrCheck_flag = "CC";

                            SB_C.Amount = 0;

                            SB_C.TestName = (al2[i] as MainTest_Bal_C).Maintestname;
                            SB_C.STCODE = (al2[i] as MainTest_Bal_C).MTCode;
                            al.Add(SB_C);
                           
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
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
                    conn.Close();
                }
                catch (SqlException)
                {
                    // Log an event in the Application Event Log.
                    throw;
                }
            }
        }

        return al;
    }


    public static ICollection getGridFill(string SDCode, string selecteddrcode, object branchid, string RateName)
    {
        ArrayList al = new ArrayList();
        ArrayList al2 = new ArrayList();
        int c = 1;
        
        #region maintest
       

        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        if (SDCode.Trim() != "PD")
        {
            sc = new SqlCommand("select Maintestname,MTCode from MainTest where SDCode=@SDCode and branchid=@branchid ORDER BY Maintestname", conn);
        }
        else
        {
            sc = new SqlCommand("select PackageName as Maintestname,PackageCode as MTCode from PackMst where branchid=@branchid ORDER BY Maintestname", conn);
        }
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SDCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
       

        SqlDataReader sdr = null;
        conn.Close();  
        MainTest_Bal_C sft = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            if (sdr != null)
            {
                while (sdr.Read()) 
                {
                    sft = new MainTest_Bal_C();

                    sft.Maintestname = sdr["Maintestname"].ToString();
                    sft.MTCode = sdr["MTCode"].ToString();

                    al2.Add(sft);
                    c++;                    
                }
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
        conn.Close();
        sdr.Close();
        #endregion
               
        for (int i = 0; i < al2.Count; i++)
        {
            try
            {

                conn.Open();
                sc = new SqlCommand("select * from sharemst where STCODE=@arraytlcode and RateCode=@selecteddrcode and branchid=@branchid order by TestName ", conn);//and  RateName=@RateName

                sc.Parameters.Add(new SqlParameter("@arraytlcode", SqlDbType.NVarChar, 5)).Value = (al2[i] as MainTest_Bal_C).MTCode;
                sc.Parameters.Add(new SqlParameter("@selecteddrcode", SqlDbType.NVarChar, 3)).Value = selecteddrcode;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
                sc.Parameters.Add(new SqlParameter("@RateName", SqlDbType.NVarChar, 50)).Value = RateName;
                try
                {
                    //record found then take from special charges
                    sdr = sc.ExecuteReader();

                    if (sdr != null)
                    {
                        if (sdr.Read())
                        {                            
                            sharemst_Bal_C comp = new sharemst_Bal_C();

                            if ((sdr["Amount"]) is DBNull)
                                comp.Amount = 0;
                            else
                                comp.Amount = Convert.ToInt32(sdr["Amount"]);

                            if ((sdr["Percentage"]) is DBNull)
                                comp.Percentage = 0;
                            else
                                comp.Percentage = Convert.ToSingle(sdr["Percentage"]);

                            if ((sdr["Emergency"]) is DBNull)
                                comp.Emergency = 0;
                            else
                                comp.Emergency = Convert.ToInt32(sdr["Emergency"]);
                                                       

                            comp.TestName = (al2[i] as MainTest_Bal_C).Maintestname;
                            comp.STCODE = (al2[i] as MainTest_Bal_C).MTCode;

                            al.Add(comp);
                        }
                        else
                        {
                            sharemst_Bal_C comp2 = new sharemst_Bal_C();
                            DrMT_Bal_C dr1 = new DrMT_Bal_C();                            

                            comp2.Amount = 0;

                            comp2.TestName = (al2[i] as MainTest_Bal_C).Maintestname;
                            comp2.STCODE = (al2[i] as MainTest_Bal_C).MTCode;
                            al.Add(comp2);

                        }
                    }
                } 
                catch (Exception ex)
                {
                    throw;
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
                    conn.Close();
                }
                catch (SqlException)
                {
                    // Log an event in the Application Event Log.
                    throw;
                }
            }
        }

        return al;
    }
 

    public static bool isMTCodeExists(string MTCode, int tlid, object branchid)
    {       
       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM MainTest " +
                         " WHERE MTCode=@MTCode and Maintestid <> @Maintestid and branchid=@branchid", conn);


        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 200)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@Maintestid", SqlDbType.Int, 9)).Value = tlid;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        int MBC = 0;

        try
        {
            conn.Open();
            MBC = Convert.ToInt32(sc.ExecuteScalar());

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
        if (MBC != 0)
            return true;
        else
            return false;
    }
    public static bool isMTCodeExists(string MTCode, object branchid)
    {        
        SqlConnection conn = DataAccess.ConInitForDC();
        if (branchid != null)
        {
            branchid = Convert.ToInt32(branchid);
        }
        else
        {
            branchid = 0;
        }

        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM MainTest " +
                         " WHERE MTCode=@MTCode and branchid=@branchid ", conn);

        // Add the employee ID parameter and set its value.
                
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 200)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

        int MBC = 0;

        try
        {
            conn.Open();
            MBC = Convert.ToInt32(sc.ExecuteScalar());

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
            catch
            {
                throw new Exception("Record not found");
            }
        }
        if (MBC != 0)
            return true;
        else
            return false;
    }
    public static void updateNewOrdno(int iOrdNo)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        string s = "";
        s = "Update MainTest Set Testordno = Testordno + 1 Where Testordno>=@Tordno ";
        SqlCommand sc = new SqlCommand(s, conn);
        sc.Parameters.Add(new SqlParameter("@Tordno", SqlDbType.Int, 9)).Value = iOrdNo;
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

    
    public static void Update_Packageno(MainTest_Bal_C tnt, int iOrdNo, object branchid)
    {
        if (branchid != null)
        {
            branchid = Convert.ToInt32(branchid);
        }
        else
        {
            branchid = 0;
        }

        SqlConnection conn = DataAccess.ConInitForDC(); 
        string s = "";
        if (tnt.Testordno < iOrdNo)
            s = "Update MainTest Set Testordno = Testordno + 1 Where Testordno>=@Tstordno and Testordno <@TstordnoCurr and branchid=@branchid and SDCode='" + tnt.SDCode + "'";
        else if (tnt.Testordno > iOrdNo)
            s = "Update MainTest Set Testordno = Testordno - 1 Where Testordno>@TstordnoCurr and Testordno <=@Tstordno and branchid=@branchid and SDCode='" + tnt.SDCode + "'";
        SqlCommand sc = new SqlCommand(s, conn);
        sc.Parameters.Add(new SqlParameter("@Tstordno", SqlDbType.Int, 9)).Value = tnt.Testordno;
        sc.Parameters.Add(new SqlParameter("@TstordnoCurr", SqlDbType.Int, 9)).Value = iOrdNo;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
       
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
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

  
    public static bool IS_MTCodeForm(string MTCode, object branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM MainTest " +
                         " WHERE MTCode=@MTCode and Singleformat='Format' and branchid=@branchid ", conn);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

        int MBC = 0;

        try
        {
            conn.Open();
            MBC = Convert.ToInt32(sc.ExecuteScalar());

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
        if (MBC != 0)
            return true;
        else
            return false;
    }
  

    public static string GetSingleFormat(string MTCode, object branchid)
    {
       
        string tcode = "";
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        sc = new SqlCommand("select Singleformat from MainTest where MTCode=@MTCode and branchid=@branchid", conn);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        try
        {
            conn.Open();
            tcode = Convert.ToString(sc.ExecuteScalar());
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
       
        return tcode;
    }

    public static string GET_Maintest_name(string MTCode, object branchid)
    {
       
        string Maintestname = "";
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        sc = new SqlCommand("select Maintestname from MainTest where MTCode=@MTCode and branchid=@branchid", conn);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode.Trim();
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        try
        {
            conn.Open();
            Maintestname = Convert.ToString(sc.ExecuteScalar());
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

    
    public static bool updatetablesforSDCode(string SDCode, string MTCode, int Branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        sc = new SqlCommand();

        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "SP_phgrpchags";
        sc.Connection = conn;

        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 200)).Value = SDCode;
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int, 4)).Value = Branchid;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

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
            conn.Close();
            conn.Dispose();
        }
        return true;
    }
    public static bool updatetablesforMTCode(string oldMTCode, string newMTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        sc = new SqlCommand();

        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "SP_phgrouprec";
        sc.Connection = conn;

        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@newMTCode", SqlDbType.NVarChar, 50)).Value = newMTCode;
        sc.Parameters.Add(new SqlParameter("@oldMTCode", SqlDbType.NVarChar, 50)).Value = oldMTCode;

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
            conn.Close();
            conn.Dispose();
        }
        return true;
    }
    public static DataSet Get_Testformpatient(string oldMTcode, int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select PID,tests from patmst where tests like '%" + oldMTcode + "%' and branchid=" + branchid + "", con);
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
    public static void updateordnowhesorted(int Testid, string MTCode, int Testordno, int branchid)
    {
        SqlConnection con=DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("update MainTest set Testordno=" + Testordno + " where Maintestid=" + Testid + " and MTCode='" + MTCode + "' and branchid=" + branchid + "", con);
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

    public static DataTable GetAllTestParam(string SDCode, string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        DataTable dt = new DataTable();
        SqlCommand sc = new SqlCommand("select STCODE,testname from SubTest where MTCode='" + MTCode + "' and branchid=" + branchid + "  and SDCode='" + SDCode + "' order by Testordno", conn);
        try
        {
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sc);
            da.Fill(dt);
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
                throw;
            }
        }
        return dt;
    }

    public static bool MTcodeInUse(string MTCode, int branchid)
    {
        
        SqlConnection connnew = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM patmstd " +
                         " WHERE MTCode=@MTCode and branchid=@branchid", connnew);


        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 200)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        int MBC = 0;

        try
        {
            connnew.Open();
            MBC = Convert.ToInt32(sc.ExecuteScalar());

        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                connnew.Close(); connnew.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }

        }
        if (MBC != 0)
            return true;
        else
            return false;
    }

    public static void UpdateSDCode(string MTCode, int branchid, string SDCode)
    {
        SqlConnection connnew = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SP_phserrecfrm", connnew);
        sc.Parameters.AddWithValue("@MTCode", MTCode);
        sc.Parameters.AddWithValue("@branchid", branchid);
        sc.Parameters.AddWithValue("@SDCode", SDCode);
        sc.CommandType = CommandType.StoredProcedure;

        try
        {
            connnew.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception)
        {

        }
        finally
        { connnew.Close(); connnew.Dispose(); }

    }

    public static ICollection getGrideValue_Testname(string SDCode, string selecteddrcode, object branchid)
    {
        ArrayList al = new ArrayList();
        ArrayList al2 = new ArrayList();
        int c = 1;

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        SqlDataReader sdr = null;
        


        //for (int i = 0; i < al2.Count; i++)
        //{
            try
            {

                conn.Open();
                if (selecteddrcode == "0")
                {
                    sc = new SqlCommand("select * from TestCharges where STCODE=@SDCode  and branchid=@branchid order by TestName ", conn);
                }
                else
                {
                    sc = new SqlCommand("select * from TestCharges where STCODE=@SDCode and drcode=@drcode and branchid=@branchid order by TestName ", conn);
                }

                sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SDCode.Trim();
                sc.Parameters.Add(new SqlParameter("@drcode", SqlDbType.NVarChar, 3)).Value = selecteddrcode;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
                try
                {
                    //record found then take from special charges
                    sdr = sc.ExecuteReader();

                    if (sdr != null)
                    {
                        if (sdr.Read())
                        {
                            //sft = new MainTest_Bal_C();
                            SpeCh_Bal_C SBC = new SpeCh_Bal_C();

                            if ((sdr["Amount"]) is DBNull)
                                SBC.Amount = 0;
                            else
                                SBC.Amount = Convert.ToInt32(sdr["Amount"]);

                            if ((sdr["Percentage"]) is DBNull)
                                SBC.Percentage = 0;
                            else
                                SBC.Percentage = Convert.ToSingle(sdr["Percentage"]);

                            if ((sdr["Emergency"]) is DBNull)
                                SBC.Emergency = 0;
                            else
                                SBC.Emergency = Convert.ToInt32(sdr["Emergency"]);


                            SBC.TestName = Convert.ToString(sdr["TestName"]);
                            SBC.STCODE = Convert.ToString(sdr["STCODE"]);

                            al.Add(SBC);
                        }
                        else
                        {
                            SpeCh_Bal_C SB_C = new SpeCh_Bal_C();
                            DrMT_Bal_C dr1 = new DrMT_Bal_C();

                            SqlConnection conn5 = DataAccess.ConInitForDC();
                            SqlCommand sc5 = null;
                            SqlDataReader sdr5 = null;

                            conn5.Open();
                            sc5 = new SqlCommand("select MTCode as STCODE,MaintestName as TestName  from Maintest where MTCode=@SDCode  and branchid=@branchid ", conn5);

                            sc5.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SDCode.Trim();
                            sc5.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
                             sdr5 = sc5.ExecuteReader();

                             if (sdr5 != null)
                             {
                                 if (sdr5.Read())
                                 {
                                     SB_C.Amount = 0;
                                     SB_C.TestName = Convert.ToString(sdr5["TestName"]);
                                     SB_C.STCODE = Convert.ToString(sdr5["STCODE"]);
                                     al.Add(SB_C);
                                 }
                             }
                             conn5.Close();
                             conn5.Dispose();

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
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
                    conn.Close();

                }
                catch (SqlException)
                {
                    // Log an event in the Application Event Log.
                    throw;
                }
            }
       // }

        return al;
    }


    public static ICollection getGrideValue_DoctorPAyment(string SDCode, string selecteddrcode, object branchid, string DrCode)
    {
        ArrayList al = new ArrayList();
        ArrayList al2 = new ArrayList();
        int c = 1;

        #region maintest


        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;

        if (SDCode.Trim() != "PD")
        {
            sc = new SqlCommand("select Maintestname,MTCode from MainTest where SDCode=@SDCode and branchid=@branchid ORDER BY Maintestname", conn);

        }
        else
        {
            sc = new SqlCommand("select PackageName as Maintestname,PackageCode as MTCode from PackMst where branchid=@branchid ORDER BY Maintestname", conn);
        }
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SDCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        // Add the employee ID parameter and set its value.

        SqlDataReader sdr = null;

        conn.Close();

        MainTest_Bal_C sft = null;

        try
        {
            conn.Open();

            sdr = sc.ExecuteReader();

            if (sdr != null)
            {
                while (sdr.Read())
                {
                    sft = new MainTest_Bal_C();

                    sft.Maintestname = sdr["Maintestname"].ToString();
                    sft.MTCode = sdr["MTCode"].ToString();


                    al2.Add(sft);
                    c++;


                }
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }

        conn.Close();
        sdr.Close();
        #endregion

        for (int i = 0; i < al2.Count; i++)
        {
            try
            {

                conn.Open();
                sc = new SqlCommand("select * from DailyPerformTestCharges where STCODE=@arraytlcode and DrCode=@DrCode   and branchid=@branchid order by Testname", conn);

                sc.Parameters.Add(new SqlParameter("@arraytlcode", SqlDbType.NVarChar, 5)).Value = (al2[i] as MainTest_Bal_C).MTCode;
                sc.Parameters.Add(new SqlParameter("@selecteddrcode", SqlDbType.NVarChar, 3)).Value = selecteddrcode;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
                sc.Parameters.Add(new SqlParameter("@DrCode", SqlDbType.Int, 4)).Value = DrCode;
                try
                {
                    //record found then take from special charges
                    sdr = sc.ExecuteReader();

                    if (sdr != null)
                    {
                        if (sdr.Read())
                        {
                            //sft = new MainTest_Bal_C();
                            SpeCh_Bal_C SBC = new SpeCh_Bal_C();


                            SBC.RateType = Convert.ToString(selecteddrcode);

                            if ((sdr["Amount"]) is DBNull)
                                SBC.Amount = 0;
                            else
                                SBC.Amount = Convert.ToInt32(sdr["Amount"]);

                            if ((sdr["Percentage"]) is DBNull)
                                SBC.Percentage = 0;
                            else
                                SBC.Percentage = Convert.ToSingle(sdr["Percentage"]);

                            if ((sdr["Emergency"]) is DBNull)
                                SBC.Emergency = 0;
                            else
                                SBC.Emergency = Convert.ToInt32(sdr["Emergency"]);


                            SBC.TestName = (al2[i] as MainTest_Bal_C).Maintestname;
                            SBC.STCODE = (al2[i] as MainTest_Bal_C).MTCode;

                            al.Add(SBC);
                        }
                        else
                        {
                            SpeCh_Bal_C SB_C = new SpeCh_Bal_C();
                            DrMT_Bal_C dr1 = new DrMT_Bal_C();
                            //dr1.DrCheck_flag = "CC";

                            SB_C.Amount = 0;

                            SB_C.TestName = (al2[i] as MainTest_Bal_C).Maintestname;
                            SB_C.STCODE = (al2[i] as MainTest_Bal_C).MTCode;
                            al.Add(SB_C);

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
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
                    conn.Close();
                }
                catch (SqlException)
                {
                    // Log an event in the Application Event Log.
                    throw;
                }
            }
        }

        return al;
    }
    public static ICollection getGrideValue_Testname_Doctorwise(string SDCode, string selecteddrcode, object branchid, string DrCode)
    {
        ArrayList al = new ArrayList();
        ArrayList al2 = new ArrayList();
        int c = 1;

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        SqlDataReader sdr = null;



        //for (int i = 0; i < al2.Count; i++)
        //{
        try
        {

            conn.Open();
            if (selecteddrcode == "0")
            {
                sc = new SqlCommand("select * from DailyPerformTestCharges where STCODE=@SDCode and drcode=@DrCode  and branchid=@branchid order by TestName ", conn);
            }
            else
            {
                sc = new SqlCommand("select * from DailyPerformTestCharges where STCODE=@SDCode and drcode=@DrCode  and branchid=@branchid order by TestName ", conn);
            }

            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SDCode;
            sc.Parameters.Add(new SqlParameter("@drcode", SqlDbType.NVarChar, 3)).Value = DrCode;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
            try
            {
                //record found then take from special charges
                sdr = sc.ExecuteReader();

                if (sdr != null)
                {
                    if (sdr.Read())
                    {
                        //sft = new MainTest_Bal_C();
                        SpeCh_Bal_C SBC = new SpeCh_Bal_C();

                        if ((sdr["Amount"]) is DBNull)
                            SBC.Amount = 0;
                        else
                            SBC.Amount = Convert.ToInt32(sdr["Amount"]);

                        if ((sdr["Percentage"]) is DBNull)
                            SBC.Percentage = 0;
                        else
                            SBC.Percentage = Convert.ToSingle(sdr["Percentage"]);

                        if ((sdr["Emergency"]) is DBNull)
                            SBC.Emergency = 0;
                        else
                            SBC.Emergency = Convert.ToInt32(sdr["Emergency"]);


                        SBC.TestName = Convert.ToString(sdr["TestName"]);
                        SBC.STCODE = Convert.ToString(sdr["STCODE"]);

                        al.Add(SBC);
                    }
                    else
                    {
                        SpeCh_Bal_C SB_C = new SpeCh_Bal_C();
                        DrMT_Bal_C dr1 = new DrMT_Bal_C();

                        SB_C.Amount = 0;
                        //SB_C.TestName = Convert.ToString(sdr["TestName"]);
                        //SB_C.STCODE = Convert.ToString(sdr["STCODE"]);
                         SB_C.TestName = Convert.ToString(selecteddrcode);
                         SB_C.STCODE = Convert.ToString(SDCode);
                        
                        al.Add(SB_C);

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
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
                conn.Close();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // }

        return al;
    }


    public static ICollection getGrideValue_Testname_Doctor(string SDCode, string selecteddrcode, object branchid)
    {
        ArrayList al = new ArrayList();
        ArrayList al2 = new ArrayList();
        int c = 1;

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        SqlDataReader sdr = null;



        //for (int i = 0; i < al2.Count; i++)
        //{
        try
        {

            conn.Open();
            if (selecteddrcode == "0")
            {
                sc = new SqlCommand("select * from TestCharges where STCODE=@SDCode  and branchid=@branchid order by TestName ", conn);
            }
            else
            {
                sc = new SqlCommand("select * from TestCharges where STCODE=@SDCode and drcode=@selecteddrcode and branchid=@branchid order by TestName ", conn);
            }

            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SDCode;
            sc.Parameters.Add(new SqlParameter("@drcode", SqlDbType.NVarChar, 3)).Value = selecteddrcode;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
            try
            {
                //record found then take from special charges
                sdr = sc.ExecuteReader();

                if (sdr != null)
                {
                    if (sdr.Read())
                    {
                        //sft = new MainTest_Bal_C();
                        SpeCh_Bal_C SBC = new SpeCh_Bal_C();

                        if ((sdr["Amount"]) is DBNull)
                            SBC.Amount = 0;
                        else
                            SBC.Amount = Convert.ToInt32(sdr["Amount"]);

                        if ((sdr["Percentage"]) is DBNull)
                            SBC.Percentage = 0;
                        else
                            SBC.Percentage = Convert.ToSingle(sdr["Percentage"]);

                        if ((sdr["Emergency"]) is DBNull)
                            SBC.Emergency = 0;
                        else
                            SBC.Emergency = Convert.ToInt32(sdr["Emergency"]);


                        SBC.TestName = Convert.ToString(sdr["TestName"]);
                        SBC.STCODE = Convert.ToString(sdr["STCODE"]);

                        al.Add(SBC);
                    }
                    else
                    {
                        SpeCh_Bal_C SB_C = new SpeCh_Bal_C();
                        DrMT_Bal_C dr1 = new DrMT_Bal_C();

                        SB_C.Amount = 0;
                        SB_C.TestName = Convert.ToString(sdr["TestName"]);
                        SB_C.STCODE = Convert.ToString(sdr["STCODE"]);
                        al.Add(SB_C);

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
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
                conn.Close();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // }

        return al;
    }

    public static bool isExistsShortCut(string shortform, object branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        if (branchid != null)
        {
            branchid = Convert.ToInt32(branchid);
        }
        else
        {
            branchid = 0;
        }

        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM stformmst " +
                         " WHERE shortform=@shortform and branchid=@branchid ", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@shortform", SqlDbType.NVarChar, 200)).Value = shortform;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

        int MBC = 0;

        try
        {
            conn.Open();
            MBC = Convert.ToInt32(sc.ExecuteScalar());

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
            catch
            {
                throw new Exception("Record not found");
            }
        }
        if (MBC != 0)
            return true;
        else
            return false;
    }

}

