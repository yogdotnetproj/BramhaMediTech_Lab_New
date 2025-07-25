using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
public class TestDescriptiveResultPrint_b
{
	public TestDescriptiveResultPrint_b()
	{
        this.TextDId = 0;
        this.PatRegID = "";
        this.FID = "";
        this.MTCode = "";
        this.STCODE = "";
        this.TextDesc = "";
        this.SignID = 0;
        this.ComputerName = "";
	}
    public TestDescriptiveResultPrint_b(string PatRegID, string FID, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT * FROM VW_desfiledata_org  WHERE PatRegID=@PatRegID and FID=@FID  and branchid=" + branchid + "", conn);//and MTCode=@MTCode

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
      //  sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {

               

                if (!string.IsNullOrEmpty(sdr["TextDesc"].ToString()))
                    this.TextDesc = Convert.ToString(sdr["TextDesc"]);
                else
                    this.TextDesc = "";

             
            }
            else
            {
                
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

    private int textDId;
    public int TextDId
    {
        get { return textDId; }
        set { textDId = value; }
    }

    private string patregid;
    public string PatRegID
    {
        get { return patregid; }
        set { patregid = value; }
    }

    private int signID;
    public int SignID
    {
        get { return signID; }
        set { signID = value; }
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

    private string textDesc;
    public string TextDesc
    {
        get { return textDesc; }
        set { textDesc = value; }
    }

    private string computerName;
    public string ComputerName
    {
        get { return computerName; }
        set { computerName = value; }
    }

    private bool flag;
    public bool P_flag
    {
        get { return flag; }
        set { flag = value; }
    }

    private int _PID;
    public int PID
    {
        get { return _PID; }
        set { _PID = value; }
    }
    private string FormatName;
    public string P_FormatName
    {
        get { return FormatName; }
        set { FormatName = value; }
    }

    #endregion
}