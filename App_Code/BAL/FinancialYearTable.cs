using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
public class FinancialYearTable
{
    internal class FinancialYearException : Exception
    {
        public FinancialYearException(string msg) : base(msg) { }
    }
    public FinancialYearTable()
    {
    }
    public FinancialYearTable(string sFY, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * from FIYR where financialYearId=@FinancialYearId and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.NVarChar, 50)).Value = sFY;

        // Add the employee ID parameter and set its value.

        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                this.FinancialYearId = sdr["FinancialYearId"].ToString();
                this.StartDate = Convert.ToDateTime(sdr["StartDate"]);
                this.EndDate = Convert.ToDateTime(sdr["EndDate"]);
                this.branchid = Convert.ToInt16(sdr["branchid"]);
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

    }

    #region Properties
    private string _FinancialYearId = "";
    public string FinancialYearId
    {
        get { return _FinancialYearId; }
        set { _FinancialYearId = value; }
    }
    private DateTime _StartDate;
    public DateTime StartDate
    {
        get { return _StartDate; }
        set { _StartDate = value; }
    }
    private DateTime _EndDate;
    public DateTime EndDate
    {
        get { return _EndDate; }
        set { _EndDate = value; }
    }
    private int _branchid = 1;
    public int branchid
    {
        get { return _branchid; }
        set { _branchid = value; }
    }
    private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }
    private string _Yearname = "";
    public string Yearname
    {
        get { return _Yearname; }
        set { _Yearname = value; }
    }
    #endregion

    public bool Insert(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO FIYR " +
        "(FinancialYearId,StartDate,EndDate, branchid,username) " +
        "VALUES (@FinancialYearId, @StartDate, @EndDate,@branchid,@username)", conn);

        sc.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.NVarChar, 50)).Value = (string)(this.FinancialYearId);
        sc.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime)).Value = this.StartDate;
        sc.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime)).Value = this.EndDate;
      
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 9)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_username;
        SqlDataReader sdr = null;
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
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        return true;
    }//End Insert

    public bool Delete(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            " Delete from FIYR" +
            " Where FinancialYearId=@FinancialYearId and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.NVarChar, 50)).Value = (string)(this.FinancialYearId);
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
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
        }
        return true;
    }//End Delete

    public bool update(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
        "Update FIYR " +
        "Set startdate=@startdate , EndDate=@EndDate,branchid=@branchid " +
        "Where FinancialYearId=@FinancialYearId and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@FinancialYearId", SqlDbType.NVarChar, 50)).Value = (string)(this.FinancialYearId);
        sc.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime, 4)).Value = (DateTime)(this.StartDate);
        sc.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime, 4)).Value = (DateTime)(this.EndDate);
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 9)).Value = branchid;
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception)
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
                throw;
            }
        }
        return true;
    }//End Update 

    public override string ToString()
    {
        return this.StartDate.Day.ToString() + "/" + this.StartDate.Month.ToString() + "/" + this.StartDate.Year.ToString() + " To " + this.EndDate.Day.ToString() + "/" + this.EndDate.Month.ToString() + "/" + this.EndDate.Year.ToString();
    }

    public bool isfinyrpresent(int branchid)
    {
        FinancialYearId = "0";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select FinancialYearId from FIYR where branchid=" + branchid + " and getdate() between StartDate and EndDate'", con);
        SqlDataReader sdr = null;

        try
        {
            con.Open();
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                FinancialYearId = sdr["FinancialYearId"].ToString();
            }

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
        if (FinancialYearId == "0")
            return false;
        else
            return true;
    }

  
}
