using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public static class PackageL_Bal_C
{
    public static IList<Package_Bal_C> Get_PackageDetails(string PCode, int branchid)
    {
        List<Package_Bal_C> al = new List<Package_Bal_C>();
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * from PackmstD where PackageCode=@PackageCode  and branchid=" + branchid + " ORDER BY Testordno", conn);
        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 50)).Value = PCode;       

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
                    Package_Bal_C gd = new Package_Bal_C();
                    gd.PackageCode = sdr["PackageCode"].ToString();
                    gd.PackageName = sdr["PackageName"].ToString();
                    gd.Patregdate = Convert.ToDateTime(sdr["dateofentry"]);
                    if (!(sdr["Flag"] is DBNull))
                        gd.Flag = sdr["Flag"].ToString();
                    gd.SDCode = sdr["SDCode"].ToString();
                    gd.MTCode = sdr["MTCode"].ToString();
                    gd.TestName = sdr["TestName"].ToString();
                    gd.TestRate = Convert.ToSingle(sdr["TestRate"]);
                    if (!(sdr["Testordno"] is DBNull))
                        gd.Testordno = Convert.ToInt32(sdr["Testordno"]);
                    else
                        gd.Testordno = 0;
                    al.Add(gd);
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
    public static void Delete_PackageDetails(string PCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" delete from PackmstD where PackageCode=@PackageCode and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 50)).Value = PCode;
        // Add the employee ID parameter and set its value.

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
    }
    public static void Update_O_No(string MT_Code, string SD_Code, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        string s = "";
        s = "Update PackmstD Set SDCode = @SDCode Where MTCode=@MTCode and branchid=" + branchid + "";
        SqlCommand sc = new SqlCommand(s, conn);
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 500)).Value = MT_Code;
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 500)).Value = SD_Code;
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

    public static ICollection<Package_Bal_C> GetPackage_Group(int branchid)
    {
        List<Package_Bal_C> al = new List<Package_Bal_C>();
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("select * from PackmstD where branchid=" + branchid + "", conn);

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
                    Package_Bal_C gm = new Package_Bal_C();
                    gm.PackageCode = sdr["PackageCode"].ToString();
                    gm.PackageName = sdr["PackageName"].ToString();
                    gm.Patregdate = Convert.ToDateTime(sdr["DateOfEntry"]);
                    gm.SDCode = sdr["SDCode"].ToString();
                    gm.MTCode = sdr["MTCode"].ToString();
                    gm.TestName = sdr["TestName"].ToString();
                    gm.TestRate=Convert.ToSingle(sdr["TestRate"]);
                    if (!(sdr["Testordno"] is DBNull))
                        gm.Testordno = Convert.ToInt32(sdr["Testordno"]);
                    else
                        gm.Testordno = 0;
                    
                    al.Add(gm);
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

    public static void SaveOrdno(int newOrdNo, string MTCode, string PackageCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        string s = "";
        s = "Update PackmstD Set Testordno = @Tstordno Where MTCode='" + MTCode + "' and PackageCode='" + PackageCode + "' and branchid=" + branchid + "";
        SqlCommand sc = new SqlCommand(s, conn);
        sc.Parameters.Add(new SqlParameter("@Tstordno", SqlDbType.Int, 9)).Value = newOrdNo;       

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

    public static void Update_Packageno(int newOrdNo, int oldOrdNo, string PackageCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        string s = "";
        if (newOrdNo < oldOrdNo)
            s = "Update PackmstD Set Testordno = Testordno + 1 Where Testordno>=@Tstordno and Testordno <@TstordnoCurr and PackageCode='" + PackageCode + "' and branchid=" + branchid + "";
        else if (newOrdNo > oldOrdNo)
            s = "Update PackmstD Set Testordno = Testordno - 1 Where Testordno>@TstordnoCurr and Testordno <=@Tstordno and PackageCode='" + PackageCode + "' and branchid=" + branchid + "";
        SqlCommand sc = new SqlCommand(s, conn);
        sc.Parameters.Add(new SqlParameter("@Tstordno", SqlDbType.Int, 9)).Value = newOrdNo;
        sc.Parameters.Add(new SqlParameter("@TstordnoCurr", SqlDbType.Int, 9)).Value = oldOrdNo;
       
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
    }//End Update 
}
