using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public class FinancialYearTableLogic
{
    public static string StartMonth = "", EndMonth = "", StMonth = "", EdMonth = "", StartDate = "", EndDate = "";
    public static string lFinancialYearId = "";
    public static ICollection<FinancialYearTable> getFinancialYears(int branchid)
    {       
        List<FinancialYearTable> al = new List<FinancialYearTable>();        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT * from FIYR where branchid=" + branchid + "", conn);
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
                    FinancialYearTable fy = new FinancialYearTable();
                    fy.FinancialYearId = sdr["FinancialYearId"].ToString();
                    fy.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                    fy.EndDate = Convert.ToDateTime(sdr["EndDate"]);
                    
                    al.Add(fy);
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
        return al;
    }
    public static DateTime getMaxEndDate()
        {
            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = new SqlCommand(" SELECT max(enddate) from FIYR ", conn);

            DateTime dt = new DateTime();
            try
            {
                conn.Open();
                object o = sc.ExecuteScalar();
                if (o != DBNull.Value)
                    //DBNull
                    //DateTime d1 = ;
                    dt = Convert.ToDateTime((DateTime)o);
            }
            finally
            {
                try
                {
                    conn.Close(); conn.Dispose();
                }
                catch
                {
                    // Log an event in the Application Event Log.
                    throw;
                }
            }
            return dt;
        }
    public static FinancialYearTable getCurrentFinancialYear()
        {

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand(" SELECT * from FIYR where StartDate<=@date and EndDate >=@date ", conn);
            sc.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime)).Value = Convert.ToDateTime(Date.getOnlydate());            
            SqlDataReader sdr = null;
            FinancialYearTable fy = new FinancialYearTable();
            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    fy.FinancialYearId = sdr["FinancialYearId"].ToString();
                    fy.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                    fy.EndDate = Convert.ToDateTime(sdr["EndDate"]);
                    fy.branchid = Convert.ToInt16(sdr["branchid"]);
                }               

            }
            catch
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
            return fy;
        }
    public static string getNextFinancialYearId(int branchid)
        {

            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand(" SELECT max(financialYearId) from FIYR where branchid=" + branchid + "", conn);

            FinancialYearTable fy = new FinancialYearTable();
            short s = 0;
            try
            {
                conn.Open();
                object o = sc.ExecuteScalar();
                if (o!=DBNull.Value)
                {
                    s = Convert.ToInt16(o);
                    s++;
                }
                else
                    s = 1;
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
            return s.ToString();
        }

    public static string GetMaxfinId(int branchid)
    {
        string finid="";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select max(FinancialYearId) from FIYR where branchid=" + branchid + "", con);
        con.Open();
        try
        {
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                finid = "0";
            else
                finid = Convert.ToString(o);
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                con.Close(); con.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
            
        }
        //tl.Sort();
        return finid;
    }

    public static DataSet getFinancialYearsList(int branchid)
    {       

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter da = new SqlDataAdapter("SELECT  FinancialYearId,cast(year(StartDate) as nvarchar(5))+ '  TO  ' + cast(year(EndDate)  as nvarchar(5)) as Yearname from FIYR where branchid=" + branchid + "  ", conn);

        // Add the employee ID parameter and set its value.

        DataSet ds = new DataSet();

        try
        {
            conn.Open();
            da.Fill(ds);            
            
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
        return ds;
    }

    public static DataSet getFinancialYearsListExceptCurrent(int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();        
        DateTime dt = Convert.ToDateTime(Date.getdate().ToString("dd/MM/yyyy"));     
        SqlDataAdapter da = new SqlDataAdapter(
          " SELECT    case when len(convert(varchar(100),FinancialYearId))='1' then '0'+ (convert(varchar(100),FinancialYearId)) else (convert(varchar(100),FinancialYearId)) end as FinancialYearId,cast(year(StartDate) as nvarchar(5))+ '  TO  ' + cast(year(EndDate)  as nvarchar(5)) " +
          " as Yearname from FIYR where branchid=" + branchid + "  and FinancialYearId not in " +
          " ( SELECT FinancialYearId from FIYR where branchid=" + branchid + "  and StartDate<='" + Convert.ToDateTime(Date.getdate()).ToString("dd/MMM/yyyy") + "' and EndDate >='" + Convert.ToDateTime(Date.getdate()).ToString("dd/MMM/yyyy") + "')", conn);
         
        DataSet ds = new DataSet();

        try
        {
            conn.Open();
            da.Fill(ds);

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
        return ds;
    }

    public static FinancialYearTable getCurrentFinancialYear(int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT * from FIYR where StartDate<=@date and EndDate >=@date and branchid="+ branchid +"", conn);
        
         sc.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime)).Value = Convert.ToDateTime(Date.getOnlydate());

        SqlDataReader sdr = null;
        FinancialYearTable fy = new FinancialYearTable();
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                fy.FinancialYearId = sdr["FinancialYearId"].ToString();
                fy.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                fy.EndDate = Convert.ToDateTime(sdr["EndDate"]);
                fy.branchid = Convert.ToInt16(sdr["branchid"]);
            }


        }
        catch
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
        return fy;
    }
    public static ICollection<FinancialYearTable> getFinancialYearsList_New(int branchid)
    {
       
        List<FinancialYearTable> al = new List<FinancialYearTable>();        
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT  FinancialYearId,cast(month(StartDate) as nvarchar(5)) as StartMonth, cast(month(EndDate)  as nvarchar(5)) as EndMonth ,StartDate,EndDate from FIYR where branchid=" + branchid + "", conn);

        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            if (sdr != null)
            {
                while (sdr.Read())
                {

                    FinancialYearTable fy = new FinancialYearTable();
                    fy.FinancialYearId = sdr["FinancialYearId"].ToString();
                   
                    StartDate = Convert.ToString(sdr["StartDate"]);
                    EndDate = Convert.ToString(sdr["EndDate"]);

                    StartMonth = Convert.ToString(sdr["StartMonth"]);
                    EndMonth = Convert.ToString(sdr["EndMonth"]);
                   
                    fy.Yearname = Convert.ToString(Convert.ToDateTime(StartDate).ToShortDateString() + " TO " + Convert.ToDateTime(EndDate).ToShortDateString());
                    al.Add(fy);
                   
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

    public static string getPatregister(int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT count(billno) FROM VW_cshbill", conn);

        FinancialYearTable fy = new FinancialYearTable();
        short s = 0;
        try
        {
            conn.Open();
            object o = sc.ExecuteScalar();
            if (o != DBNull.Value)
            {
                s = Convert.ToInt16(o);
               
            }
            else
                s = 1;
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
        return s.ToString();
    }

}
