using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

 public class TestFormulaLogic_Bal_c
    {

     public static ArrayList getAllFormulaTbl1TableValues(object branchid, int DigModule)
     {        
         ArrayList al = new ArrayList();
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlDataAdapter sda = new SqlDataAdapter();
         if (DigModule == 0)
         {
              sda = new SqlDataAdapter("select STCODE from formst where STCODE in (select STCODE from SubTest where MTCode in (select MTCode from MainTest where SDCode in (select SDCode from SubDepartment where  branchid=" + branchid + "))) union select STCODE from formst where STCODE in (select MTCode from MainTest where SDCode in (select SDCode from SubDepartment where branchid=" + branchid + "))", conn);
         }
         else
         {
              sda = new SqlDataAdapter("select STCODE from formst where STCODE in (select STCODE from SubTest where MTCode in (select MTCode from MainTest where SDCode in (select SDCode from SubDepartment where DigModule=" + DigModule + " and branchid=" + branchid + "))) union select STCODE from formst where STCODE in (select MTCode from MainTest where SDCode in (select SDCode from SubDepartment where DigModule=" + DigModule + " and branchid=" + branchid + "))", conn);
         }
             DataSet ds = new DataSet();
         try
         {
             sda.Fill(ds);
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
         SqlDataReader sdr = null;
         conn = DataAccess.ConInitForDC(); 
         for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
         {
             conn.Open();
            // SqlCommand sc = new SqlCommand("select * from formst where STCODE='" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "'", conn);
             SqlCommand sc = new SqlCommand("SELECT     formst.ID, formst.STCODE, formst.SDCode, formst.Formula, formst.Branchid, formst.username, formst.Createdby, formst.Createdon, formst.Updatedby, formst.Updatedon, " +
                                         "   SubTest.TestName "+
                                         "   FROM         formst INNER JOIN "+
                                         "   SubTest ON formst.STCODE = SubTest.STCODE "+
                                          "  where formst.STCODE='" + ds.Tables[0].Rows[i].ItemArray[0].ToString() + "'", conn);
             try
             {
                 
                 sdr = sc.ExecuteReader();
                 if (sdr != null)
                 {
                     // The IEnumerable contains DataRowView objects.

                     while (sdr.Read())
                     {
                         CalculateSet_Bal_C ft1 = new CalculateSet_Bal_C();

                         ft1.STCODE = sdr["STCODE"].ToString();
                         ft1.TestName = sdr["TestName"].ToString();
                         ft1.Formula = sdr["Formula"].ToString();
                         ft1.ID = (int)sdr["ID"];                        
                                               

                         al.Add(ft1);
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
    

     public static ICollection SearchCodeFromformulaTable(string STCODE, object branchid)
        {
           
            ArrayList al = new ArrayList();
            STCODE = '%' + STCODE + '%';
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("select * from formst where codes like '" + STCODE + "' and STCODE <> '' and branchid=@branchid", conn);

            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar,50)).Value = STCODE;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null)
                {
                    // The IEnumerable contains DataRowView objects.

                    while (sdr.Read())
                    {
                        CalculateSet_Bal_C ft1 = new CalculateSet_Bal_C();

                        ft1.STCODE = sdr["STCODE"].ToString();
                        ft1.Formula = sdr["Formula"].ToString();
                        ft1.ID = (int)sdr["ID"];
                      

                        al.Add(ft1);
                    }
                }
                else
                {
                    throw new Exception("No Record Fetched.");
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
            return al;
        }


     public static ICollection getGrideValue(string Testname_check, object branchid)
        {
            
            ArrayList al = new ArrayList();                   

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;
            if (Testname_check == "General")
            {
                sc = new SqlCommand(" SELECT * from stformmst  where TestName=@testname or TestName is null and branchid=@branchid", conn);
                sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 50)).Value = Testname_check;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
            }
            else
            {
                sc = new SqlCommand(" SELECT * from stformmst  where TestName=@testname and branchid=@branchid", conn);
                sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 50)).Value = Testname_check;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
            }

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
                        Stformmst_Main_Bal_C sft = new Stformmst_Main_Bal_C();
                        sft.Shortform = sdr["shortform"].ToString();
                        sft.Description = sdr["Description"].ToString();
                       sft.ShortFormID = Convert.ToUInt16(sdr["ShortFormID"]);
                      //  sft.Testname = sdr["Testname"].ToString();

                        al.Add(sft);
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
            return al;
        }
     public static ICollection getShortFormentry(object branchid)
        {
            
            ArrayList al = new ArrayList();
           
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand(" SELECT distinct(TestName)as TestName FROM stformmst  where branchid=@branchid", conn);
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
                        if (!string.IsNullOrEmpty(Convert.ToString(sdr["TestName"])))
                            al.Add(sdr["TestName"].ToString());
                        al.Sort();
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
            return al;
        }

     public static ICollection getformulaHcodewise(string SDCode, object branchid)
        {
           
            ArrayList al = new ArrayList();           
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("SELECT * from formst WHERE SDCode=@SDCode and branchid=@branchid", conn);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SDCode;
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
                        CalculateSet_Bal_C ft1 = new CalculateSet_Bal_C();
                        ft1.STCODE = sdr["STCODE"].ToString();
                        ft1.Formula = sdr["Formula"].ToString();
                       
                        ft1.ID = (int)sdr["ID"];
                        al.Add(ft1);
                        
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
            return al;
        }
     public static string getcodes(string SDCode, int branchid)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlCommand sc = new SqlCommand("SELECT codes FROM formst WHERE STCODE='" + SDCode + "' and branchid=" + branchid + "", conn);

         object obj = null;
         string codes = "";
         try
         {
             conn.Open();
             obj = sc.ExecuteScalar();
             if (obj != DBNull.Value)
                 codes = obj.ToString();
             else
                 codes = "";
         }
         catch
         {

         }
         finally
         {
             conn.Close();
             conn.Dispose();
         }
         return codes;
     }
     public static string getformula(string SDCode, int branchid)
     {
         SqlConnection conn = DataAccess.ConInitForDC();

         SqlCommand sc = new SqlCommand("SELECT Formula FROM formst WHERE STCODE='" + SDCode + "' and branchid=" + branchid + "", conn);   
         object obj = null;
         string formula = "";
         try
         {
             conn.Open();
             obj = sc.ExecuteScalar();
             if (obj != DBNull.Value)
                 formula = Convert.ToString( obj);
             else
                 formula = "";
         }
         catch
         {
             
         }
         finally
         {
             conn.Close();
             conn.Dispose();
         }
         return formula;
     }
     public static string gettestcode(string SDCode, int branchid)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlCommand sc = new SqlCommand("SELECT STCODE FROM formst WHERE SDCode='" + SDCode + "' and branchid=" + branchid + "", conn);

         object obj = null;
         string STCODE = "";
         try
         {
             conn.Open();
             obj = sc.ExecuteScalar();
             if (obj != DBNull.Value)
                 STCODE = obj.ToString();
             else
                 STCODE = "";
         }
         catch
         {
             
         }
         finally
         {
             conn.Close();
             conn.Dispose();
         }
         return STCODE;
     }
     public static bool isFormulaExists(string TestCode,object branchid)
     {
         
         SqlConnection conn = DataAccess.ConInitForDC(); 
         SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                          " FROM formst " +
                          " WHERE STCODE=@STCODE and branchid=@branchid ", conn);         

         sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar,50)).Value = TestCode;
         sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

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
             catch
             {
                 throw new Exception("Record not found");
             }
         }
         if (cnt != 0)
             return true;
         else
             return false;
     }
     public static string GetTest(string STCODE, int branchid)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlCommand sc = new SqlCommand("SELECT TestName FROM SubTest WHERE STCODE='" + STCODE + "' and branchid=" + branchid + "", conn);

         object obj = null;
         string codes = "";
         try
         {
             conn.Open();
             obj = sc.ExecuteScalar();
             if (obj != DBNull.Value)
                 codes = obj.ToString();
             else
                 codes = "";
         }
         catch
         {

         }
         finally
         {
             conn.Close();
             conn.Dispose();
         }
         return codes;
     }
    
    }//class
