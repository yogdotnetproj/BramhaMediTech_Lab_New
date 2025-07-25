using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public class Package_Bal_C
{
    internal class Package_Bal_CException : Exception
    {
        public Package_Bal_CException(string msg) : base(msg) { }
    }

  
    #region Properties
    string _PackageCode = "";
    public String PackageCode
    {
        get
        {
            return _PackageCode;
        }
        set
        {
            _PackageCode = value;
        }
    }
    string _PackageName = "";
    public String PackageName
    {
        get
        {
            return _PackageName;
        }
        set
        {
            _PackageName = value;
        }
    }
    DateTime? _dateofentry = null;
    public DateTime? Patregdate
    {
        get
        {
            return _dateofentry;
        }
        set
        {
            _dateofentry = value;
        }
    }
    string _Flag = null;
    public String Flag
    {
        get
        {
            return _Flag;
        }
        set
        {
            _Flag = value;
        }
    }
    string _SDCode = "";
    public String SDCode
    {
        get
        {
            return _SDCode;
        }
        set
        {
            _SDCode = value;
        }
    }
    string _MTCode = "";
    public String MTCode
    {
        get
        {
            return _MTCode;
        }
        set
        {
            _MTCode = value;
        }
    }
    string _TestName = null;
    public String TestName
    {
        get
        {
            return _TestName;
        }
        set
        {
            _TestName = value;
        }
    }
    float? _TestRate;
    public float? TestRate
    {
        get
        {
            return _TestRate;
        }
        set
        {
            _TestRate = value;
        }
    }
    int _Testordno;
    public int Testordno
    {
        get
        {
            return _Testordno;
        }
        set
        {
            _Testordno = value;
        }
    }
    private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }
    #endregion
    #region Constructors
    public Package_Bal_C()
    {
        this.PackageCode = "";
        this.PackageName = "";
        //Flag	nvarchar	3
        this.SDCode = "";
        this.MTCode = "";
        this.TestName = "";
        this.TestRate = 0f;
        this.Patregdate = Date.getOnlydate();
        this.Testordno = 0;
    }
    public Package_Bal_C(String PackageCode, String MTCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT * from PackmstD" +
                         " WHERE PackageCode = @PackageCode and MTCode=@MTCode ", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 4)).Value = PackageCode;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 500)).Value = MTCode;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                // The IEnumerable contains DataRowView objects. 
                this.PackageCode = sdr["PackageCode"].ToString();
                this.PackageName = sdr["PackageName"].ToString();
                this.TestRate = Convert.ToSingle(sdr["TestRate"]);
                this.SDCode = sdr["SDCode"].ToString();
                this.TestName = sdr["TestName"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                if (!(sdr["Flag"] is DBNull))
                    this.Flag = sdr["Flag"].ToString();
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                if (!(sdr["Testordno"] is DBNull))
                    this.Testordno = Convert.ToInt32(sdr["Testordno"]);
                else
                    this.Testordno = 0;
            }
            else
            {
                throw new Package_Bal_CException("No Record Fetched.");
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
            catch (Package_Bal_CException)
            {
                throw new Package_Bal_CException("Record not found");
            }
        }
    }


    public Package_Bal_C(String PackageCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT * from PackmstD" +
                         " WHERE PackageCode = @PackageCode ", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 4)).Value = PackageCode;
       

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                // The IEnumerable contains DataRowView objects.
                this.PackageCode = sdr["PackageCode"].ToString();
                this.PackageName = sdr["PackageName"].ToString();
                this.TestRate = Convert.ToSingle(sdr["TestRate"]);
                this.SDCode = sdr["SDCode"].ToString();
                this.TestName = sdr["TestName"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                if (!(sdr["Flag"] is DBNull))
                    this.Flag = sdr["Flag"].ToString();
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                if (!(sdr["Testordno"] is DBNull))
                    this.Testordno = Convert.ToInt32(sdr["Testordno"]);
                else
                    this.Testordno = 0;
            }
            else
            {
                throw new Package_Bal_CException("No Record Fetched.");
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
            catch (Package_Bal_CException)
            {
                throw new Package_Bal_CException("Record not found");
            }
        }
    }

    #endregion

    public bool Insert(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO PackmstD " +
        "(PackageCode, PackageName, Flag, dateofentry,SDCode,MTCode,TestName,TestRate,Testordno,branchid,Createdby) " +
        "VALUES (@PackageCode, @PackageName, @flag, @dateofentry,@SDCode,@MTCode,@TestName,@TestRate,@Testordno,@branchid,@username)", conn);

        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 50)).Value = this.PackageCode;
        sc.Parameters.Add(new SqlParameter("@PackageName", SqlDbType.NVarChar, 255)).Value = this.PackageName;
        if (this.Flag == null)
            sc.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar, 3)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar, 3)).Value = this.Flag;
        sc.Parameters.Add(new SqlParameter("@dateofentry", SqlDbType.DateTime)).Value = this.Patregdate;
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.VarChar, 500)).Value = this.SDCode;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 500)).Value = this.MTCode;
        sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.VarChar, 500)).Value = this.TestName;
        sc.Parameters.Add(new SqlParameter("@TestRate", SqlDbType.Float)).Value = this.TestRate;
        sc.Parameters.Add(new SqlParameter("@Testordno", SqlDbType.Int,9)).Value = this.Testordno;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
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
            " Delete from PackmstD" +
            " Where PackageCode=@PackageCode and MTCode=@MTCode  where branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 50)).Value = this.PackageCode;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 500)).Value = this.MTCode;
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
    }
    public bool DeleteByPack_Code(string PackageCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            " Delete from PackmstD" +
            " Where PackageCode=@PackageCode  and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 50)).Value = PackageCode;
       
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


    public DataTable getAllTestCodesPAck(string PackageCode, int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select MTCode from PackmstD where PackageCode='" + PackageCode + "' and branchid=" + branchid + "", con);
            da.Fill(dt);
        }
        catch (Exception ex) { throw; }
        finally { con.Close(); con.Dispose(); }
        return dt;
    }

    public DataTable getAllTestCodes(string PackageCode,int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC(); 
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select MTCode from PackmstD where MTCode='" + PackageCode + "' and branchid=" + branchid + "", con);
            da.Fill(dt);
        }
        catch (Exception ex) { throw; }
        finally { con.Close(); con.Dispose(); }
        return dt;
    }

    public DataTable getAllTestCodes_ForPAckage(string PackageCode, int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        DataTable dt = new DataTable();
        try
        {
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(" select MTCode from PackmstD where PackageCode='" + PackageCode + "' and branchid=" + branchid + "", con);
            da.Fill(dt);
        }
        catch (Exception ex) { throw; }
        finally { con.Close(); con.Dispose(); }
        return dt;
    }


}
