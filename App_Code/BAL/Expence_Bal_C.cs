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

public class Expence_Bal_C
{
	public Expence_Bal_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private DateTime _ExpenceDate;  public DateTime ExpenceDate    {  get { return _ExpenceDate; } set { _ExpenceDate = value; }    }

    private DateTime _ExpenceTo; public DateTime ExpenceTo { get { return _ExpenceTo; } set { _ExpenceTo = value; } }
    private string _Particular; public string Particular { get { return _Particular; } set { _Particular = value; } }
    private string _ExpenceDetails; public string ExpenceDetails { get { return _ExpenceDetails; } set { _ExpenceDetails = value; } }
    private string _RequestFrom; public string RequestFrom { get { return _RequestFrom; } set { _RequestFrom = value; } }
    private string _RequestTo; public string RequestTo { get { return _RequestTo; } set { _RequestTo = value; } }
    
    private string _UserName; public string UserName { get { return _UserName; } set { _UserName = value; } }
    private float _ExpenceAmount; public float ExpenceAmount { get { return _ExpenceAmount; } set { _ExpenceAmount = value; } }
    private int _Branchid; public int Branchid { get { return _Branchid; } set { _Branchid = value; } }
    private int _ID; public int ID { get { return _ID; } set { _ID = value; } }


    public bool Update_ExpenceCode()
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("" +

         "Update DailyExpenceDetails set Particular=@Particular ,ExpenceAmount=@ExpenceAmount ,ExpenceDetails=@ExpenceDetails,ExpenceDate=@ExpenceDate where ID=@ID and branchid=@Branchid ", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@Particular", SqlDbType.NVarChar, 2500)).Value = Particular;
        sc.Parameters.Add(new SqlParameter("@ExpenceAmount", SqlDbType.NVarChar, 200)).Value = ExpenceAmount;
        sc.Parameters.Add(new SqlParameter("@ExpenceDetails", SqlDbType.NVarChar, 2550)).Value = ExpenceDetails;
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 200)).Value = UserName;
        sc.Parameters.Add(new SqlParameter("@ExpenceDate", SqlDbType.DateTime)).Value = ExpenceDate;
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int)).Value = Branchid;
        sc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;

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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    }
    public bool Insert_ExpenceCode()
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO DailyExpenceDetails(Particular,ExpenceAmount,ExpenceDetails,UserName,ExpenceDate,Branchid)" +
        "VALUES(@Particular,@ExpenceAmount,@ExpenceDetails,@UserName,@ExpenceDate,@Branchid)", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@Particular", SqlDbType.NVarChar, 2500)).Value = Particular;
        sc.Parameters.Add(new SqlParameter("@ExpenceAmount", SqlDbType.NVarChar, 200)).Value = ExpenceAmount;
        sc.Parameters.Add(new SqlParameter("@ExpenceDetails", SqlDbType.NVarChar, 2550)).Value = ExpenceDetails;
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 200)).Value = UserName;
        sc.Parameters.Add(new SqlParameter("@ExpenceDate", SqlDbType.DateTime)).Value = ExpenceDate;
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int)).Value = Branchid;

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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    }
    public DataTable Bind_ExpenceEntry(string UserName)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select * from DailyExpenceDetails where UserName='" + UserName + "' order by id desc ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public void delete_ExpenceEntry(int Expid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from DailyExpenceDetails where Id='" + Expid + "' ", conn);

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
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }

        }
    }

    public static DataSet Get_ExpenceData(object tdate, object fdate, string user, int branchid, string ExpenceName)//
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT    * from DailyExpenceDetails  " +
    " where   ExpenceDate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and branchid=" + branchid + "";

        if (ExpenceName != "" && ExpenceName != null)
        {
            query += " and Particular='" + ExpenceName + "'";
        }

        if (user != "" && user != null)
        {
            query += " and username='" + user + "'";
        }

        da = new SqlDataAdapter(query, conn);
        DataSet ds = new DataSet();
        try
        {
            da.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }

        finally
        {
            conn.Close(); conn.Dispose();
        }
        return ds;

    }

    public bool Insert_FinanceYear()
    {
        SqlConnection conn = DataAccess.ConInitForDC();


        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO FIYR(StartDate,EndDate,Branchid,UserName)" +
        "VALUES(@StartDate,@EndDate,@Branchid,@UserName)", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 200)).Value = UserName;
        sc.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime)).Value = ExpenceDate;
        sc.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime)).Value = ExpenceTo;
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int)).Value = Branchid;

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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    }
    public DataTable Get_UserName(string branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
       // SqlConnection con = data.ConInitForDC1();
        string sql = "select CUId,UserName from ctuser where Usertype='Reception' and username<>'" + UserName + "' and branchid=" + branchid + " ";

        sql += " order by UserName";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataTable ds = new DataTable();
        con.Open();
        try
        {
            da.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
        return ds;

    }

    public bool Insert_UserCashExchangeRequest()
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO UserCashExchangeRequest(RequestFrom,RequestTo,ExpectedAmt,RequestDate,Branchid,Remarks)" +
        "VALUES(@RequestFrom,@RequestTo,@ExpectedAmt,@RequestDate,@Branchid,@Remarks)", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@RequestFrom", SqlDbType.NVarChar, 250)).Value = RequestFrom;
        sc.Parameters.Add(new SqlParameter("@RequestTo", SqlDbType.NVarChar, 200)).Value = RequestTo;
        sc.Parameters.Add(new SqlParameter("@ExpectedAmt", SqlDbType.NVarChar, 50)).Value = ExpenceAmount;       
        sc.Parameters.Add(new SqlParameter("@RequestDate", SqlDbType.DateTime)).Value = ExpenceDate;
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int)).Value = Branchid;
        sc.Parameters.Add(new SqlParameter("@Remarks", SqlDbType.NVarChar, 50)).Value = ExpenceDetails;

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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    }
    public DataTable Bind_UserCashExchangeRequest(string UserName)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select * from UserCashExchangeRequest where RequestFrom='" + UserName + "' order by id desc ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public void delete_UserCashExchangeRequest(int Expid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from UserCashExchangeRequest where Id='" + Expid + "' ", conn);

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
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }

        }
    }
    public bool Update_UserCashExchangeRequest()
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("" +
        "Update UserCashExchangeRequest set RequestApprove=1 ,ReceiveAmt=@ReceiveAmt ,ReceiveDate=@ReceiveDate where ID=@ID and branchid=" + Branchid + " ", conn);
     

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = ID;
        sc.Parameters.Add(new SqlParameter("@ReceiveAmt", SqlDbType.NVarChar, 50)).Value = ExpenceAmount;
        sc.Parameters.Add(new SqlParameter("@ReceiveDate", SqlDbType.DateTime)).Value = ExpenceDate;
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int)).Value = Branchid;
        

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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    }
    public DataTable Bind_UserCashExchangeApproval(string UserName)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select * from UserCashExchangeRequest  where RequestTo='" + UserName + "' order by id desc ", con);//where RequestTo='" + UserName + "'

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }


    public DataTable Bind_ExpenceEntry_ID(string UserName,string EID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select * from DailyExpenceDetails where UserName='" + UserName + "' and ID='" + EID + "' order by id desc ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public DataTable ValidateBirthYear_Data(string BirthYear,string RegNo)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select year(DOB) as BirthYear from patmst where year(DOB)='" + BirthYear + "' and Patregid='" + RegNo + "' ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }
    public DataTable GetSMSString_CountryCode_Covid(string SubjString, int branchid, string PType)
    {
        DataAccess data = new DataAccess();
        SqlConnection con = new SqlConnection();
       
            con = DataAccess.ConInitForDC();
       
        // SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_GetCountryCode_Covid", con);



        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.NVarChar, 50)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@SubjString", SqlDbType.NVarChar, 550)).Value = SubjString;

        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            con.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        finally
        {

            con.Close(); con.Dispose();

        }

    }
}