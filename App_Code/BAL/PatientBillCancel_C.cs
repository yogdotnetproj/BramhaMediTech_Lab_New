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
public class PatientBillCancel_C
{
    private int PID; public int P_PID { get { return PID; } set { PID = value; } }
    private float NetAmount; public float P_NetAmount { get { return NetAmount; } set { NetAmount = value; } }
    private string UserName; public string P_UserName { get { return UserName; } set { UserName = value; } }
	public PatientBillCancel_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void BillDisactive()
    {
        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = con;
        sc.CommandText = "SP_phbilrecstats";
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = P_PID;
     
        try
        {
            con.Open();
            sc.ExecuteNonQuery();
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

    }
    public void BillDisactive_SingleTest()
    {

        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = con;
        sc.CommandText = "SP_InsertDeleteSingleTest";
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = P_PID;
        sc.Parameters.Add(new SqlParameter("@NetAmount", SqlDbType.Float)).Value = P_NetAmount;

        try
        {
            con.Open();
            sc.ExecuteNonQuery();
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

    }

    public void BillRefund_Amt()
    {

        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = con;
        sc.CommandText = "SP_InsertRefundAmt";
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = P_PID;
        sc.Parameters.Add(new SqlParameter("@NetAmount", SqlDbType.Float)).Value = P_NetAmount;
        sc.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar,250)).Value = P_UserName;

        try
        {
            con.Open();
            sc.ExecuteNonQuery();
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

    }


}