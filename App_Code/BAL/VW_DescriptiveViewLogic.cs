using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data;


public class VW_DescriptiveViewLogic
{

    public static int SP_GetAlterView_BarCode(int branchid, string PID, string FID, string TestCode, string BarCodeID)
    {
        int i = 0;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "SP_GetBarcode";
        sc.Parameters.AddWithValue("@branchid", branchid);

        sc.Parameters.AddWithValue("@PID", PID);
        sc.Parameters.AddWithValue("@FID", FID);
        sc.Parameters.AddWithValue("@TestCode", TestCode);
        sc.Parameters.AddWithValue("@BarCodeID", BarCodeID);
        sc.Parameters.AddWithValue("@RegDate", Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy")));
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return i;
    }

    public static int SP_GetAlterView_HISTO(int branchid, string Test_Code, string PatRegID, string FID)
    {
        int i = 0;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "SP_phraddata_HISTO";
        sc.Parameters.AddWithValue("@branchid", branchid);
        sc.Parameters.AddWithValue("@viewtestcode", Test_Code);
        sc.Parameters.AddWithValue("@PatRegID", PatRegID);
        sc.Parameters.AddWithValue("@FID", FID);
        try
        {

            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return i;
    }

    public static int SP_GetAlterView_CYTO(int branchid, string Test_Code, string PatRegID, string FID)
    {
        int i = 0;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "SP_phraddata_CYTO";
        sc.Parameters.AddWithValue("@branchid", branchid);
        sc.Parameters.AddWithValue("@viewtestcode", Test_Code);
        sc.Parameters.AddWithValue("@PatRegID", PatRegID);
        sc.Parameters.AddWithValue("@FID", FID);
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return i;
    }

    public static int SP_GetAlterView(int branchid, string Test_Code, string PatRegID, string FID)
    {
        int i = 0;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "SP_phraddata";
        sc.Parameters.AddWithValue("@branchid", branchid);
        sc.Parameters.AddWithValue("@viewtestcode", Test_Code);
        sc.Parameters.AddWithValue("@PatRegID", PatRegID);
        sc.Parameters.AddWithValue("@FID", FID);
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return i;
    }

    public static int SP_Getresultnondesc_Report(int branchid, string Test_Code, string PatRegID, string FID)
    {
        int i = 0;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "SP_phdatarecfrm";
        sc.Parameters.AddWithValue("@branchid", branchid);
        sc.Parameters.AddWithValue("@viewtestcode", Test_Code);
        sc.Parameters.AddWithValue("@PatRegID", PatRegID);
        sc.Parameters.AddWithValue("@FID", FID);
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();            
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return i;
    }

    public static int SP_AlterViewMicro(int branchid, string Test_Code, string PatRegID, string FID)
    {
        int i = 0;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.CommandText = "SP_phmcrecds";
        sc.Parameters.AddWithValue("@branchid", branchid);
        sc.Parameters.AddWithValue("@viewtestcode", Test_Code);
        sc.Parameters.AddWithValue("@PatRegID", PatRegID);
        sc.Parameters.AddWithValue("@FID", FID);
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return i;
    }


    public static int AlterView_DescRep(string Test_Code, string PatRegID, string FID, int branchid)
    {
        int i;

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "Alter View VW_desfiledata_org as (Select  *, VW_GetLabNo.LabNo " +
              "  FROM         VW_desfiledata INNER JOIN " +
              "  VW_GetLabNo ON VW_desfiledata.PatRegID = VW_GetLabNo.PatRegID AND VW_desfiledata.MTCode = VW_GetLabNo.MTCode AND  " +
              "  VW_desfiledata.Branchid = VW_GetLabNo.Branchid where" + Test_Code + " and VW_desfiledata.PatRegID='" + PatRegID + "' and VW_desfiledata.FID='" + FID + "' and VW_desfiledata.branchid=" + branchid + ")";

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();

        }
        finally
        {
            try
            {
                conn.Close(); //conn.Dispose();
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
        sc.CommandText = "Select count(*) from VW_desfiledata_org";//
        //sc.CommandType = CommandType.Text;
        try
        {
            conn.Open();
            i = Convert.ToInt32(sc.ExecuteScalar());

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
        return i;
    }

    public static int AlterView_NonDescRep(string Test_Code, string PatRegID, string FID, int branchid)
    {
        int i;

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "Alter View VW_patdatvwrecvwas (Select *, VW_GetLabNo.LabNo from FROM         VW_patdatarutinevw INNER JOIN " +
           " VW_GetLabNo ON VW_patdatarutinevw.PatRegID = VW_GetLabNo.PatRegID AND VW_patdatarutinevw.MTCode = VW_GetLabNo.MTCode AND " +
           " VW_patdatarutinevw.Branchid = VW_GetLabNo.Branchid where " + Test_Code + " and VW_patdatarutinevw.PatRegID='" + PatRegID + "' and VW_patdatarutinevw.FID='" + FID + "' and VW_patdatarutinevw.branchid=" + branchid + ")";

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

                conn.Close();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }

        }

        sc.CommandText = "Select count(*) from VW_patdatvwrecvw";
        //sc.CommandType = CommandType.Text;
        try
        {
            conn.Open();
            i = Convert.ToInt32(sc.ExecuteScalar());

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
        return i;
    }
   
 
   
    private static string comp;
    public static string Computername
    {
        get { return comp; }
        set { comp = value; }
    }

    private static string report;
    public static string Reportname
    {
        get { return report; }
        set { report = value; }
    }
}
