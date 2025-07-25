using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

public class TestspecialnoteLogic_Bal_C
{
    
    public static IList<Testspecialnote_Bal_C> getFormatnamebyTest(int branchid)
    {
        //ArrayList al = new ArrayList();
        List<Testspecialnote_Bal_C> al = new List<Testspecialnote_Bal_C>();
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT * from inpmst where branchid=" + branchid + " order by MTCode", conn);

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
                    Testspecialnote_Bal_C cnt = new Testspecialnote_Bal_C();


                    cnt.SpecialNote = sdr["SpecialNote"].ToString();

                    if (sdr["SNFlag"] != DBNull.Value)
                        cnt.SNFlag = Convert.ToBoolean(sdr["SNFlag"]);
                    cnt.MTCode = sdr["MTCode"].ToString().Trim();
                    if ((sdr["MTCode"].ToString().Trim().Length) > 4)
                    {
                        cnt.Name = cnt.MTCode.Trim() + " - " + MainTestLog_Bal_C.GET_Maintest_name(cnt.MTCode.Trim(), branchid);
                        al.Add(cnt);
                    }
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
        //al.Sort();
        return al;
    }
       
    public static IList<Testspecialnote_Bal_C> getFormatnamebyProfile(int branchid)
    {
        //ArrayList al = new ArrayList();
        List<Testspecialnote_Bal_C> al = new List<Testspecialnote_Bal_C>();             

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * from inpmst where branchid=" + branchid + " ", conn);

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
                    Testspecialnote_Bal_C cnt = new Testspecialnote_Bal_C();

                    cnt.SpecialNote = sdr["SpecialNote"].ToString();

                    if (sdr["SNFlag"] != DBNull.Value)
                        cnt.SNFlag = Convert.ToBoolean(sdr["SNFlag"]);
                    cnt.MTCode = sdr["MTCode"].ToString().Trim();
                    if ((sdr["MTCode"].ToString().Trim().Length) == 4)
                    {
                        cnt.Name = cnt.MTCode.Trim() + " - " + Packagenew_Bal_C.getPNameByCode(cnt.MTCode.Trim(), branchid);
                        al.Add(cnt);
                    }
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
        //al.Sort();
        return al;
    }
  

    public static bool is_MTCodeExists(string MTCode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM inpmst " +
                         " WHERE MTCode=@MTCode and branchid=" + branchid + "", conn);


        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 5)).Value = MTCode;

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
    }

