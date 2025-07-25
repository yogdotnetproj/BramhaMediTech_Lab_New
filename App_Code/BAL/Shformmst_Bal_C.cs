using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


  public class Shformmst_Bal_C
    {


      public static ICollection get_stformmstValue(object compid, int branchid)
      {

          ArrayList al = new ArrayList();
          SqlConnection conn = DataAccess.ConInitForDC();

          SqlCommand sc = new SqlCommand("" +
              "SELECT        stformmst.ShortFormID, stformmst.MTCode, stformmst.ParaCode, stformmst.shortform, stformmst.Description, stformmst.Branchid, stformmst.username, stformmst.Createdby, stformmst.Createdon, stformmst.Updatedby, "+
               " stformmst.Updatedon, MainTest.Maintestname, SubTest.TestName "+
               " FROM            stformmst INNER JOIN MainTest ON stformmst.MTCode = MainTest.MTCode INNER JOIN SubTest ON stformmst.ParaCode = SubTest.STCODE where stformmst.branchid=" + branchid + " order by shortform", conn);
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
                      Stformmst_Main_Bal_C sft = new Stformmst_Main_Bal_C();
                      sft.Shortform = sdr["shortform"].ToString();
                      sft.Description = sdr["Description"].ToString();
                      sft.ShortFormID = Convert.ToInt32(sdr["ShortFormID"]);
                      sft.MainTest = sdr["Maintestname"].ToString();
                      sft.SubTest = sdr["TestName"].ToString();
                      al.Add(sft);
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
              catch (Exception)
              {
                  throw new Exception("Record not found");
              }
          }
          return al;
      }

      public static ICollection getGrideValuebyshortform(int Shortformid, object branchid)
      {

          ArrayList al = new ArrayList();

          SqlConnection conn = DataAccess.ConInitForDC();
          SqlCommand sc = new SqlCommand("" +
              "SELECT * from stformmst  where ShortFormID=@ShortFormID", conn);

          sc.Parameters.Add(new SqlParameter("@ShortFormID", SqlDbType.Int)).Value = Shortformid;
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
                      Stformmst_Main_Bal_C sft = new Stformmst_Main_Bal_C();
                      sft.Shortform = sdr["shortform"].ToString();
                      sft.Description = sdr["Description"].ToString();
                      sft.MainTest = sdr["MTCode"].ToString();
                      sft.SubTest = sdr["ParaCode"].ToString();



                      al.Add(sft);
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
          return al;
      }

      public static ICollection getShortFormentry(object branchid)
        {            
            ArrayList al = new ArrayList();           
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand("SELECT Maintestname,MTCode FROM MainTest where textmemo ='DescType' and branchid=@branchid", conn);
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
                        MainTest_Bal_C tn = new MainTest_Bal_C();
                        tn.Maintestname = sdr["MTCode"].ToString() + "-" + sdr["Maintestname"].ToString();
                        tn.MTCode = sdr["MTCode"].ToString();

                        
                        al.Add(tn);
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
            return al;
        }
    

    }

