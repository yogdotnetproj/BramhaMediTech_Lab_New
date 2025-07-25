using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

/// <summary>
/// Summary description for AdminSettings_C
/// </summary>
public class AdminSettings_C
{
	public AdminSettings_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}



    public DataTable Get_PhlebotomistReq()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select * from PhlebotomistRequired  ";



        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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

    public DataTable Get_RegisterNoBarcodeInterface()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select * from IsInterfaceRegNo  ";



        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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


    public bool Update_PhlebotomistReq()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update PhlebotomistRequired " +
            "set ISRequired=1 ", conn);


        conn.Close();
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

    public bool Update_PhlebotomistNotReq()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update PhlebotomistRequired " +
            "set ISRequired=0 ", conn);


        conn.Close();
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

    public bool Update_IsInterfaceRegNo()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update IsInterfaceRegNo " +
            "set IsRegNo=1 ", conn);


        conn.Close();
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

    public bool Update_IsInterfaceBarcode()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update IsInterfaceRegNo " +
            "set IsRegNo=0 ", conn);


        conn.Close();
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

    public bool InsertLabDetails(string LabEmailID, string LabEmailPassword, string LabEmailDisplayName, string LabSmsString, string LabSmsName, string LabWebsite, int Port)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_InsertLabDetails]";

        sc.Parameters.Add(new SqlParameter("@LabEmailID", SqlDbType.NVarChar, 50)).Value = LabEmailID;
        sc.Parameters.Add(new SqlParameter("@LabEmailPassword", SqlDbType.NVarChar, 50)).Value = LabEmailPassword;
        sc.Parameters.Add(new SqlParameter("@LabEmailDisplayName", SqlDbType.NVarChar, 50)).Value = LabEmailDisplayName;
        sc.Parameters.Add(new SqlParameter("@LabSmsString", SqlDbType.NVarChar, 4000)).Value = LabSmsString;
        sc.Parameters.Add(new SqlParameter("@LabSmsName", SqlDbType.NVarChar, 50)).Value = LabSmsName;
        sc.Parameters.Add(new SqlParameter("@LabWebsite", SqlDbType.NVarChar, 50)).Value = LabWebsite;
        sc.Parameters.Add(new SqlParameter("@Port", SqlDbType.Int)).Value = Port;
        
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

    public DataTable Get_LabDetails()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select * from stmst  ";



        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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


    public void AlterViewvw_VW_TestStatus1( object startDate)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = conn.CreateCommand();


        sc.CommandText = "   Alter View VW_TestStatus as    SELECT DISTINCT "+
              "  patmstd.PatRegID,patmstd.PID, patmstd.FID, patmst.intial + ' ' + patmst.Patname AS PatientName, dbo.Get_Teststatus_Patientwise(patmstd.PID, patmstd.PatRegID, " +
              "  patmstd.Branchid, patmstd.FID) AS TestStatus,patmst.Isemergency " +
              "  FROM         patmstd INNER JOIN "+
              "  MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode INNER JOIN "+
              "  patmst ON patmstd.PatRegID = patmst.PatRegID AND patmstd.PID = patmst.PID " +
             " where patmstd.patrepstatus=0 and patmstd.Patregdate >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and patmstd.Patregdate <'" + DateTimeConvesion.getDateFromString(startDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "'   ";


        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { }

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


    }

    public DataTable Get_TestStatus_Details1()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select * from VW_TestStatus  order by CAST(Pid AS int) ";



        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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


    public void AlterViewvw_VW_Countstatus1(object startDate)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = conn.CreateCommand();


        sc.CommandText = "   Alter View VW_Countstatus_TestStatus as    SELECT DISTINCT  case when patmstd.Patrepstatus=1 then 'Printed' else patmstd.Patauthicante end as Patauthicante, "+
              "  patmstd.PatRegID FROM         patmstd INNER JOIN SubDepartment ON "+
              " patmstd.SDCode = SubDepartment.SDCode AND patmstd.branchid = SubDepartment.Branchid    " +
             " where  patmstd.Patregdate >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and patmstd.Patregdate <'" + DateTimeConvesion.getDateFromString(startDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "'   ";


        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { }

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


    }

    public void AlterViewvw_VW_Countstatus(object startDate)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = conn.CreateCommand();


        sc.CommandText = "   Alter View VW_Countstatus_TestStatus as    SELECT DISTINCT  case when patmstd.Patrepstatus=1 then 'Printed' else patmstd.Patauthicante end as Patauthicante, " +
              "  patmstd.PatRegID FROM         patmstd INNER JOIN SubDepartment ON " +
              " patmstd.SDCode = SubDepartment.SDCode AND patmstd.branchid = SubDepartment.Branchid    " +
             " where  patmstd.Patregdate >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and patmstd.Patregdate <'" + DateTimeConvesion.getDateFromString(startDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "'   ";


        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { }

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


    }

    public void AlterViewvw_VW_TestStatus_DailyDisp(object startDate)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = conn.CreateCommand();


        sc.CommandText = "   Alter View VW_TestStatus_DailyDisp as    SELECT DISTINCT " +
              " patmstd.PatRegID, patmstd.FID, patmst.Patname AS PatientName,case when   patmstd.Patrepstatus=1  then 'Completed' else  Patauthicante end AS TestStatus, patmst.Isemergency, patmst.DailySeqNo, " +// dbo.Get_Teststatus_Patientwise(patmstd.PID, patmstd.PatRegID, patmstd.Branchid, patmstd.FID,patmstd.MTCode)
              "  CASE WHEN patmstd.UpdatedOn IS NULL THEN CONVERT(varchar(20), CONVERT(time, RegistratonDateTime), 100) ELSE CONVERT(varchar(20), CONVERT(time,  " +
              "  patmstd.UpdatedOn), 100) END AS PheboAccTime, patmst.PID, CAST(patmst.Tests AS nvarchar(4000)) AS TestCode, MainTest.Shortcode,case when Patrepstatus='1' then 3 when Patauthicante='Registered' then 0 when Patauthicante='Tested' then 1    when Patauthicante='Authorized' then 2 else 4 end as ordseq " +
              " FROM            patmstd INNER JOIN " +
              "  MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode INNER JOIN " +
              "  patmst ON patmstd.PatRegID = patmst.PatRegID AND patmstd.PID = patmst.PID " +
             " where patmstd.MTCode not in('PLVI2','CO19A') and patmstd.Patregdate >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and patmstd.Patregdate <'" + DateTimeConvesion.getDateFromString(startDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "'   ";


        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { }

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


    }

    public void AlterViewvw_VW_TestStatus(object startDate)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = conn.CreateCommand();


        //sc.CommandText = "   Alter View VW_TestStatus as    SELECT DISTINCT "+
        //      "  patmstd.PatRegID, patmstd.FID, patmst.Patname AS PatientName, dbo.Get_Teststatus_Patientwise(patmstd.PID, patmstd.PatRegID, patmstd.Branchid, patmstd.FID) AS TestStatus, patmst.Isemergency, patmst.DailySeqNo, "+
        //      "  +'  '+convert(varchar(20),patmst.DailySeqNo) +'     '+case when patmstd.UpdatedOn is null then convert(varchar(20),convert(time,RegistratonDateTime),100) " +
        //      "  else convert(varchar(20),convert(time,patmstd.UpdatedOn),100) end as PheboAccTime ,	 "+				 
        //      "  patmst.PID, cast( patmst.Tests as nvarchar(4000)) as TestCode " +
        //      " FROM            patmstd INNER JOIN "+
        //      "  MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode INNER JOIN "+
        //      "  patmst ON patmstd.PatRegID = patmst.PatRegID AND patmstd.PID = patmst.PID "+
        //     " where  patmstd.Patregdate >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and patmstd.Patregdate <'" + DateTimeConvesion.getDateFromString(startDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "'   ";

        sc.CommandText = "   Alter View VW_TestStatus as    SELECT DISTINCT top(99.99) percent PatRegID, FID, PatientName, TestStatus, Isemergency, DailySeqNo, PheboAccTime, PID,ordseq, " +
                         "   ShortCode = STUFF(    (SELECT ',' + Shortcode    FROM VW_TestStatus_DailyDisp t1    WHERE " +
                         "   t1.PID = t2.PID   FOR XML PATH (''))   , 1, 1, '') " +
                         "   FROM            VW_TestStatus_DailyDisp t2  order by ordseq,TestStatus ";

        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { }

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


    }

    public DataTable Get_TestStatus_Details()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select * from VW_TestStatus  order by ordseq,TestStatus ";



        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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

}