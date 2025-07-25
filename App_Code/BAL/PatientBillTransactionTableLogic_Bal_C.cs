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


public class PatientBillTransactionTableLogic_Bal_C
{
	public PatientBillTransactionTableLogic_Bal_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}

  

    public bool billnowithPID(int PID)
    {
        bool flag = true;
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand(" SELECT  COUNT(*)  FROM Recm where PID =@PID ", conn);


        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;

        object count;
        try
        {
            conn.Open();
            count = sc.ExecuteScalar();

            if (count != null)
            {
                if (Convert.ToInt32(count) > 0)
                {
                    flag = false;
                }
            }
        }
        catch (SqlException ex)
        { }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return flag;
    }
    public string GetSMSString_CountryCode(string SubjString, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string s = "";
        try
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand("SP_GetCountryCode", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@branchid", branchid);
            cmd.Parameters.AddWithValue("@SubjString", SubjString);

            s = Convert.ToString(cmd.ExecuteScalar());

        }
        catch (Exception ex)
        {
        }
        finally
        {
            conn.Close();
            conn.Dispose();

        }
        return s;
    }

    public bool billtransactionwithdate(DateTime transdate, int BillNo,int CountryCode)
    {
        bool flag = true;
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand();
        //if (CountryCode == 2)
        //{
        //     sc = new SqlCommand(" SELECT  COUNT(*)  FROM  RecM WHERE  transdate>@transdate and transdate< getdate()  AND  BillNo=@BillNo ", conn);
        //}
        //else
        //{
        //     sc = new SqlCommand(" SELECT  COUNT(*)  FROM  RecM WHERE  tdate  between convert(varchar,   DATEADD(MINUTE,-1,GETDATE()), 21) and  convert(varchar,  DATEADD(MINUTE,1,GETDATE()), 21) and transdate< getdate()   AND  BillNo=@BillNo and AmtPaid>0 ", conn);
        //}
        if (CountryCode == 2)
        {
            sc = new SqlCommand(" SELECT  COUNT(*)  FROM  RecM WHERE  tdate between convert(varchar,   DATEADD(MINUTE,-1,GETDATE()), 21) and  convert(varchar,  DATEADD(MINUTE,1,GETDATE()), 21)  and transdate< getdate()   AND  BillNo=@BillNo and AmtPaid>0 ", conn);

        }
        else
        {
            sc = new SqlCommand(" SELECT  COUNT(*)  FROM  RecM WHERE  tdate between convert(varchar,   DATEADD(second,-30,GETDATE()), 21) and  convert(varchar,  DATEADD(second,30,GETDATE()), 21)  and transdate< getdate()   AND  BillNo=@BillNo and AmtPaid>0 ", conn);


        }
        sc.Parameters.Add(new SqlParameter("@transdate", SqlDbType.DateTime)).Value = transdate;
        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = BillNo;

        object count;
        try
        {
            conn.Open();
            count = sc.ExecuteScalar();

            if (count != null)
            {
                if (Convert.ToInt32(count) > 0)
                {
                    flag = false;
                }
            }
        }
        catch (SqlException ex)
        { }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return flag;

    }



}
