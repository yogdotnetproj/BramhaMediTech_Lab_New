using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

/// <summary>
/// Summary description for API_DataTransfer_C
/// </summary>
public class API_DataTransfer_C
{
	public API_DataTransfer_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}
  



public string username { get; set; }
public string password { get; set; }
public string seller_pan { get; set; }
public string buyer_pan { get; set; } //9 digit or empty
public string fiscal_year { get; set; }
public string buyer_name { get; set; }
public string invoice_number { get; set; }
public string invoice_date { get; set; }

public double total_sales { get; set; }
public double taxable_sales_vat { get; set; }
public double vat { get; set; }
public double excisable_amount { get; set; }
public double excise { get; set; }
public double taxable_sales_hst { get; set; }
public double hst { get; set; } //health service tax
public double amount_for_esf { get; set; }
public double esf { get; set; } //education service fee
public double export_sales { get; set; }
public double tax_exempted_sales { get; set; } public bool isrealtime { get; set; } 
public DateTime datetimeClient { get; set; }
public int Sr_no { get; set; }



    public DataTable Get_Materialize_Data()
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_Get_Materialize_Data", conn);
        
        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            conn.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        finally
        {

            conn.Close(); conn.Dispose();

        }

    }
    public bool Update_APITransfer_status(int Sr_no, string TransferStatus)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[SP_Update_APITransfer_status]";

        sc.Parameters.Add(new SqlParameter("@Sr_no", SqlDbType.Int)).Value = Sr_no;
        sc.Parameters.Add(new SqlParameter("@TransferStatus", SqlDbType.NVarChar,50)).Value = Convert.ToString(TransferStatus);
       

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
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
        return true;
    }
    public DataTable Get_Materialize_Data_Canel()
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_Get_Materialize_Data_cancel", conn);

        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            conn.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        finally
        {

            conn.Close(); conn.Dispose();

        }

    }
    public DataTable Get_IsIRD_required()
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_IsIRDRequired", conn);

        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            conn.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        finally
        {

            conn.Close(); conn.Dispose();

        }

    }
}