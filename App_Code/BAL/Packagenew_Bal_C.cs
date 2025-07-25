using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;


public class Packagenew_Bal_C
{
	public Packagenew_Bal_C()
	{
		
	}
    public static string getGroupNameByCode(string PackageCode, int branchid)
    {
        string PackageName = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;

        sc = new SqlCommand("select PackageName from PackMst where PackageCode=@PackageCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 500)).Value = PackageCode.Trim();



        try
        {
            conn.Open();
            if (sc.ExecuteScalar() != null)
            {
                PackageName = sc.ExecuteScalar().ToString();
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
        return PackageName;
    }
    public static string getPNameByCode(string PackageCode, int branchid)
    {
        string PackageName = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        sc = new SqlCommand("select PackageName from PackMst where PackageCode=@PackageCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 500)).Value = PackageCode.Trim();



        try
        {
            conn.Open();
            if (sc.ExecuteScalar() != null)
            {
                PackageName = sc.ExecuteScalar().ToString();
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
        return PackageName;
    }

    public static ICollection<PackageName_Bal_C> getPackGroups(int branchid, int maindeptid)
    {
        List<PackageName_Bal_C> al = new List<PackageName_Bal_C>();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        if (maindeptid == 0)
        {
            sc = new SqlCommand(" SELECT * from PackMst where PackageCode in (select PackageCode from PackmstD where SDCode in (select SDCode from SubDepartment )) and branchid=" + branchid + "", conn);
        }
        else
        {
            sc = new SqlCommand(" SELECT * from PackMst where PackageCode in (select PackageCode from PackmstD where SDCode in (select SDCode from SubDepartment where DigModule = " + maindeptid + ")) and branchid=" + branchid + "", conn);
        }
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
                    PackageName_Bal_C gm = new PackageName_Bal_C();
                    gm.Patregdate = Convert.ToDateTime(sdr["dateofentry"]);
                    gm.PackageCode = sdr["PackageCode"].ToString();
                    gm.PackageName = sdr["PackageName"].ToString();

                    gm.PackageRateAmount = Convert.ToInt32(sdr["PackageRateAmount"]);
                    if (!(sdr["Flag"] is DBNull))
                        gm.Flag = sdr["Flag"].ToString();

                    al.Add(gm);
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
        return al;
    }


    public static ICollection GetPackageandCode(int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * from PackMst where   branchid=" + branchid + "", conn);

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
                    PackageName_Bal_C gm = new PackageName_Bal_C();
                    gm.Patregdate = Convert.ToDateTime(sdr["dateofentry"]);
                    gm.PackageCode = sdr["PackageCode"].ToString();
                    gm.PackageName = sdr["PackageCode"].ToString() + " - " + sdr["PackageName"].ToString();
                    gm.PackageRateAmount = Convert.ToInt32(sdr["PackageRateAmount"]);
                    if (!(sdr["Flag"] is DBNull))
                        gm.Flag = sdr["Flag"].ToString();
                    
                    al.Add(gm);
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
        return al;
    }
    public static bool CheckPackagecode_exist(string PackageCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT count(*) from PackMst where PackageCode=@PackageCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 500)).Value = PackageCode;

        // Add the employee ID parameter and set its value.
        
        try
        {
            conn.Open();
            Object o = sc.ExecuteScalar();
            short s = 0;
            if (o is DBNull)
                return false;
            if (Convert.ToInt16(o) > 0)
                return true;
            else
                return false;
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
    }

    public static bool CheckPackagename_exist(string PackageName, string PackageCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT count(*) from PackMst where PackageName=@PackageName and PackageCode=@PackageCode and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PackageName", SqlDbType.NVarChar, 500)).Value = PackageName;
        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 500)).Value = PackageCode;

        // Add the employee ID parameter and set its value.
       
        try
        {
            conn.Open();
            Object o = sc.ExecuteScalar();
            short s = 0;
            if (o is DBNull)
                return false;
            if (Convert.ToInt16(o) > 0)
                return true;
            else
                return false;
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
    }
}
