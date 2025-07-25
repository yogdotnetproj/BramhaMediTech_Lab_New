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

public class TAT_C
{
    public TAT_C()
    {
        //
        // TODO: Add constructor logic here
        //
        PatRegID = "";
        FID = "";
        status = "";
        remarks = "";
        dateOfRecord = Date.getMinDate();
        userName = "";
        testResults = "";
    }
    public bool updateIntresult()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[sp_updateIntresult]";

        if (this.PatRegID != null)
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.VarChar, 250)).Value = (this.PatRegID);
        else
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.VarChar, 250)).Value = DBNull.Value;

        if (this.FID != null)
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = this.FID;
        else
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = DBNull.Value;

       

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
    }

    public bool updateIntresult_Barcode()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[sp_updateIntresult_Barcode]";

        if (this.PatRegID != null)
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.VarChar, 250)).Value = (this.PatRegID);
        else
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.VarChar, 250)).Value = DBNull.Value;

        if (this.FID != null)
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = this.FID;
        else
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = DBNull.Value;



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
    }
  
    internal class TAT_CTableException : Exception
    {
        public TAT_CTableException(string msg) : base(msg) { }
    }

    #region Properties
    
    private string _PatRegID;
    public string PatRegID
    {
        get { return _PatRegID; }
        set { _PatRegID = value; }
    }

    private string fID;
    public string FID
    {
        get { return fID; }
        set { fID = value; }
    }

    private string mTCode;
    public string MTCode
    {
        get { return mTCode; }
        set { mTCode = value; }
    }

    private string sTCODE;
    public string STCODE
    {
        get { return sTCODE; }
        set { sTCODE = value; }
    }

    private string status;
    public string Status
    {
        get { return status; }
        set { status = value; }
    }

    private string remarks;
    public string Remarks
    {
        get { return remarks; }
        set { remarks = value; }
    }

    private DateTime dateOfRecord;
    public DateTime DateOfRecord
    {
        get { return dateOfRecord; }
        set { dateOfRecord = value; }
    }

    private string userName;
    public string UserName
    {
        get { return userName; }
        set { userName = value; }
    }

    private string testResults;
    public string TestResults
    {
        get { return testResults; }
        set { testResults = value; }
    }

    private int _PID;
    public int PID
    {
        get { return _PID; }
        set { _PID = value; }
    }

    #endregion

}
