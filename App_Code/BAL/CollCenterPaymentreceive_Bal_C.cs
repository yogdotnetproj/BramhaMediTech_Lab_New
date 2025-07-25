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

public class CollCenterPaymentreceive_Bal_C
{
	public CollCenterPaymentreceive_Bal_C()
	{
		//
		// TODO: Add constructor logic here
		//
          this.P_chequedate = Date.getMinDate();
          this.P_Entrydate = Date.getMinDate();
	}

    private string Username; public string P_Username { get { return Username; } set { Username = value; } }
    private DateTime chequedate; public DateTime P_chequedate { get { return chequedate; } set { chequedate = value; } }
    private float discount; public float P_discount { get { return discount; } set { discount = value; } }
    private float Receiveamount; public float P_Receiveamount { get { return Receiveamount; } set { Receiveamount = value; } }

    private string Centercode; public string P_Centercode { get { return Centercode; } set { Centercode = value; } }
    private string Patmenttype; public string P_Patmenttype { get { return Patmenttype; } set { Patmenttype = value; } }
    private string Chequeno; public string P_Chequeno { get { return Chequeno; } set { Chequeno = value; } }
    private string bankname; public string P_bankname { get { return bankname; } set { bankname = value; } }
    private string Remark; public string P_Remark { get { return Remark; } set { Remark = value; } }
    private string Fid; public string P_Fid { get { return Fid; } set { Fid = value; } }
    private DateTime Entrydate; public DateTime P_Entrydate { get { return Entrydate; } set { Entrydate = value; } }
   
    public bool Insert_CPReceive(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
        "insert into CPReceive([Centercode],[PaymentType],[Chequeno],[bankname],[chequedate],[discount],[Receiveamount],[Remark],[branchid],[Fid],[Username],Receivedate)" +
        " values(@Centercode ,@PaymentType ,@Chequeno ,@bankname ,@chequedate ,@discount ,@Receiveamount ,@Remark ,@branchid ,@Fid ,@Username,@Receivedate)", conn);

        sc.Parameters.Add(new SqlParameter("@Centercode", SqlDbType.NVarChar, 50)).Value = P_Centercode;
        sc.Parameters.Add(new SqlParameter("@PaymentType", SqlDbType.NVarChar, 50)).Value = P_Patmenttype;
        sc.Parameters.Add(new SqlParameter("@Chequeno", SqlDbType.NVarChar, 50)).Value = P_Chequeno;
        sc.Parameters.Add(new SqlParameter("@bankname", SqlDbType.NVarChar, 50)).Value = P_bankname;

        if (P_chequedate == Date.getMinDate())
        {
            sc.Parameters.Add(new SqlParameter("@chequedate", SqlDbType.DateTime)).Value = DBNull.Value;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@chequedate", SqlDbType.DateTime)).Value = P_chequedate;
        }
        sc.Parameters.Add(new SqlParameter("@discount", SqlDbType.Float)).Value = P_discount;
        sc.Parameters.Add(new SqlParameter("@Receiveamount", SqlDbType.Float)).Value = P_Receiveamount;
        sc.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 500)).Value = P_Remark;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.NVarChar, 50)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@Fid", SqlDbType.NVarChar, 50)).Value = P_Fid;
        sc.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50)).Value = P_Username;
        sc.Parameters.Add(new SqlParameter("@Receivedate", SqlDbType.DateTime)).Value = P_Entrydate;

       

        conn.Close();
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();

        }
        catch (Exception exx)
        { }

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
        return true;
    }


    public static string Get_Center_Amount(string collname, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("SELECT     SUM((CASE WHEN ((dbo.RecM.AmtPaid IS NULL) OR " +
                          "  (patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END) >= dbo.RecM.AmtPaid))  " +
                          "  THEN patmst.TestCharges + (CASE WHEN Othercharges IS NULL THEN 0 ELSE Othercharges END) ELSE patmst.TestCharges END)) AS TestCharges "+
                          "  FROM         patmst LEFT OUTER JOIN "+
                          "  RecM ON patmst.PID = RecM.PID " +
                          "  GROUP BY patmst.FID, patmst.centercode, patmst.FinancialYearID, patmst.CenterName ,patmst.Monthlybill   having  patmst.CenterName=N'" + collname + "'  and patmst.Monthlybill =1 ", conn);
                            object obj = null;
                            string drcode = "";
                            try
                            {
                                conn.Open();
                                obj = cmd.ExecuteScalar();
                                if (obj != null)
                                    drcode = obj.ToString();
                            }
                            catch (SqlException)
                            {
                                throw;
                            }
                            finally
                            {
                                conn.Close();
                                conn.Dispose();
                            }
                            return drcode;
    }
    public static string Get_Center_Amount_Date(string collname, int branchid,object startDate)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand cmd = new SqlCommand("SELECT  sum(TestCharges)as TestCharges from  VW_CenteramountDatewise ", conn);

        object obj = null;
        string drcode = "";
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                drcode = obj.ToString();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return drcode;
    }
    public static string getpaidamountcollamount(string Centername, int FID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("SELECT     SUM(isnull( Receiveamount,0))as Receiveamount FROM  CPReceive group by centercode,Fid   having  CPReceive.centercode='" + Centername + "'  and CPReceive.FID =" + FID + " ", conn);
        object obj = null;
        string drcode = "";
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                drcode = obj.ToString();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return drcode;
    }
    public static string getpaidamountcollamount_datewise(string Centername, int FID,object startDate)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("SELECT     SUM(isnull( Receiveamount,0))as Receiveamount FROM  CPReceive where  CPReceive.centercode='" + Centername + "'  and CPReceive.FID =" + FID + " and (CAST(CAST(YEAR( CPReceive.Receivedate) AS varchar(4)) + '/' + CAST(MONTH( CPReceive.Receivedate) AS varchar(2)) + '/' + CAST(DAY( CPReceive.Receivedate) AS varchar(2)) AS datetime))  <= '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' group by centercode,Fid    ", conn);
        object obj = null;
        string drcode = "";
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                drcode = obj.ToString();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return drcode;
    }
}