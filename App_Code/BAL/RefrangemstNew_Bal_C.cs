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


public class RefrangemstNew_Bal_C
{
	public RefrangemstNew_Bal_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static Refrangemst_Bal_C Get_refrangemst(string sex, int noofdays, string MTCode,string STCODE)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select * from refrangemst where @noofdays between LessThanDays and GreaterThanDays and sex=@sex and MTCode=@MTCode and STCODE=@STCODE", conn);
        sc.Parameters.Add(new SqlParameter("@noofdays", SqlDbType.Int)).Value = noofdays;
        sc.Parameters.Add(new SqlParameter("@sex", SqlDbType.NVarChar,50)).Value = sex;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
        Refrangemst_Bal_C RBC = new Refrangemst_Bal_C();
        SqlDataReader sdr=null;
        try
        {
            //if (!conn.Open())
            conn.Open();
            sdr = sc.ExecuteReader();           
            if (sdr.Read())
            {
                RBC.Unit = sdr["Unit"].ToString();
                RBC.DescretiveRange = sdr["DescretiveRange"].ToString();
                RBC.LowerRange = sdr["LowerRange"].ToString();
                RBC.UpperRange = sdr["UpperRange"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            conn.Close(); conn.Dispose();
        }
        return RBC;
    }


    public static DataSet Get_ALLDetailsForCode(string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT dbo.SubTest.TestName as STestName, dbo.refrangemst.* FROM  dbo.refrangemst LEFT OUTER JOIN dbo.SubTest ON dbo.refrangemst.STCODE = dbo.SubTest.STCODE where dbo.refrangemst.MTCode='" + MTCode + "'and dbo.SubTest.branchid=" + branchid + "", conn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
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
        return ds;
    }

    public static DataSet Get_ALLDetailsForCode_New(string STCODE, string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT dbo.SubTest.TestName as STestName, dbo.refrangemst.* FROM  dbo.refrangemst LEFT OUTER JOIN dbo.SubTest ON dbo.refrangemst.STCODE = dbo.SubTest.STCODE where dbo.refrangemst.MTCode='" + MTCode + "' and dbo.SubTest.STCODE='" + STCODE + "' and dbo.SubTest.branchid=" + branchid + "", conn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
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
        return ds;
    }
    public static ICollection Get_ALLDetailsForrange_Code(string MT_Code, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;
        sc = new SqlCommand("SELECT dbo.SubTest.TestName as STestName, dbo.refrangemst.* FROM  "+
                        " dbo.refrangemst LEFT OUTER JOIN dbo.SubTest ON dbo.refrangemst.STCODE = dbo.SubTest.STCODE  where refrangemst.MTCode=@MTCode and refrangemst.branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MT_Code;
        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    Refrangemst_Bal_C RB_C = new Refrangemst_Bal_C();
                    RB_C.LessThanDays = Convert.ToInt32(dr["LessThanDays"].ToString());
                    RB_C.GreaterThanDays = Convert.ToInt32(dr["GreaterThanDays"].ToString());
                    RB_C.DescretiveRange = dr["DescretiveRange"].ToString();
                    RB_C.TestName = dr["TestName"].ToString();
                    RB_C.Unit = dr["Unit"].ToString();
                    RB_C.Sex = dr["Sex"].ToString();
                    RB_C.MTCode = dr["MTCode"].ToString();
                    RB_C.STCODE = dr["STCODE"].ToString();
                    RB_C.ID = Convert.ToInt32(dr["ID"].ToString());
                    RB_C.UpperRange = dr["UpperRange"].ToString();
                    RB_C.LowerRange = dr["LowerRange"].ToString();
                    RB_C.TestName = dr["TestName"].ToString();
                    RB_C.TestName = dr["STestName"].ToString();
                    al.Add(RB_C);
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
                if (dr != null) dr.Close();

                conn.Close(); conn.Dispose();

            }
            catch (SqlException)
            {
                throw;
            }
           
        }

        return al;

    }

    public static DataSet Get_ALLDetailsForCode11(string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT dbo.SubTest.TestName as STestName, dbo.refrangemst.* FROM  " +
                        " dbo.refrangemst LEFT OUTER JOIN dbo.SubTest ON dbo.refrangemst.STCODE = dbo.SubTest.STCODE  where dbo.refrangemst.MTCode='" + MTCode + "'and dbo.refrangemst.branchid=" + branchid + "", conn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
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
        return ds;
    }

    public static bool IS_Exist_refrangemst(int up, int low, string sex, string MTCode, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("select count(*) from refrangemst where GreaterThanDays=@GreaterThanDays and LessThanDays=@LessThanDays and MTCode=@MTCode and sex=@sex and branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@GreaterThanDays",SqlDbType.Int)).Value =up;
        sc.Parameters.Add(new SqlParameter("@LessThanDays", SqlDbType.Int)).Value = low;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@sex",SqlDbType.NVarChar,50)).Value = sex;

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

    public static bool IS_refrangemst_Parameter(int up, int low, string sex, string MTCode, string STCODE, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("select count(*) from refrangemst where GreaterThanDays=@GreaterThanDays and LessThanDays=@LessThanDays and MTCode=@MTCode and sex=@sex and STCODE=@STCODE  and branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@GreaterThanDays", SqlDbType.Int)).Value = up;
        sc.Parameters.Add(new SqlParameter("@LessThanDays", SqlDbType.Int)).Value = low;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@sex", SqlDbType.NVarChar, 50)).Value = sex;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value =STCODE;
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
