using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;


public class PatSt_Bal_C
{

    public PatSt_Bal_C()
    {
        this.SDCode = "";
        this.MTCode = "";
        this.Patrepstatus = false;
        this.STCODE = "";
        this.Reason = "";
        this.FID = "";
        this.PatRegID = "0";
      
        this.PPID = 0;
       
        this.Patregdate = Date.getMinDate();
        this.RegisterUser = "";
        this.Patauthicante = "";
       
        this.PackageCode = "";
        this.Testdate = Date.getMinDate();
        this.ReportDate = Date.getMinDate();
        this.AbandantOn = Date.getMinDate();
        this.SampleDate = Date.getMinDate();
    }

    public PatSt_Bal_C(string PPID, string PatRegID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "select top(1) PatRegID from patmst where  PPID =" + PPID + " and PatRegID < '" + PatRegID + "'   and branchid=" + branchid + " order by srno desc ";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (PatRegID);

        sc.Parameters.Add(new SqlParameter("@PPID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(PPID);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {

                this.PatRegID = (sdr["PatRegID"].ToString());


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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }

    }

    public void AlterView_PreviousResult(int branchid, string PatRegID, string FID, string MTCode)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" alter view VW_GetPreviousResult as SELECT     ResMst.STCODE, ResMst.ID, ResMst.MTCode, ResMst.ResultTemplate, ResMst.PatRegID, ResMst.FID, ResMst.ResDate, ResMst.Method, ResMst.TestNo, " +
                 "   ResMst.testname, ResMst.normalRange, ResMst.unit, ResMst.testorderno, ResMst.PID, ResMst.SDCode, ResMst.UnitCode, ResMst.FinancialYearID, " +
                 "   ResMst.Maintestname, ResMst.RangeFlag, ResMst.branchId, MainTest.TextDesc, SubDepartment.subdeptid, patmstd.AunticateSignatureId, patmstd.Patauthicante,  " +
                 "   SubDepartment.subdeptName ,patmstd.Patregdate " +
                 "   FROM         ResMst INNER JOIN " +
                 "   MainTest ON ResMst.MTCode = MainTest.MTCode AND ResMst.branchId = MainTest.Branchid INNER JOIN " +
                 "   SubDepartment ON MainTest.SDCode = SubDepartment.SDCode INNER JOIN " +
                 "   patmstd ON ResMst.MTCode = patmstd.MTCode AND ResMst.PatRegID = patmstd.PatRegID AND ResMst.FID = patmstd.FID   " +
                 "    WHERE     (patmstd.TestDeActive = 0) AND ResMst.MTCode in (" + MTCode + ") AND " +
                 "   ResMst.PatRegID = ('" + PatRegID + "') AND (ResMst.FID = '" + FID + "') AND (ResMst.STCODE <> 'S') AND  " +
                 "   (ResMst.branchId = " + branchid + ")  and StCode<>'H' ", con);

            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }

    public bool InsertUpdate_AddResult_Panic(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[SP_phdataupdatefrm_Panic]";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (this.PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.FID);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.MTCode); 
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
      
         sc.Parameters.Add(new SqlParameter("@PanicResult", SqlDbType.Int)).Value = P_PanicResult;
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


    public bool Update_PrintCount(int branchid, int PID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[SP_UpdatePrintCount]";

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToString(branchid);      
      
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
    public bool Update_PrintCount_ReceiptNo(int branchid, int PID, int ReceiptNo)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[SP_UpdatePrintCount_ReceiptNo]";

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToString(branchid);
        sc.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.Int)).Value = ReceiptNo;

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


    public DataTable GetallBarcode(string PatRegID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   BarcodeID from patmstd where  PatRegID ='" + PatRegID + "'  ", conn);
        DataTable ds = new DataTable();

        try
        {
            sc.Fill(ds);
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
        return ds;
    }

    public DataTable Get_RegNoLength(string Menuid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select SubMenuID as MenuID,SubMenuNavigateURL as MenuName from TBL_subMenuMAster where MenuID='" + Menuid + "' ", con);
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
    public DataTable GetallResultEntry_TestCode(string PatRegID, string Fid, int DigModule, int branchid, string UserType, string Username, string TestCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter();

        if (UserType == "Main Doctor" || UserType == "Technician" || UserType == "Reporting")
        {
            sc = new SqlDataAdapter("SELECT DISTINCT  " +
                      "  a.SDCode, c.subdeptName, a.MTCode, c.subdeptName + ' - ' + b.Maintestname AS MainTestName, b.TextDesc, patmstd.Patauthicante, patmstd.TestUser, " +
                      "  patmstd.AunticateSignatureId, patmstd.PackageCode, a.branchId, c.SDOrderNo, a.TestNo, c.subdeptid " +
                      "  FROM         ResMst AS a INNER JOIN " +
                      "  MainTest AS b ON a.MTCode = b.MTCode AND a.MTCode = b.MTCode AND a.branchId = b.Branchid AND a.MTCode = b.MTCode INNER JOIN " +
                      "  SubDepartment AS c ON b.SDCode = c.SDCode AND b.SDCode = c.SDCode AND b.Branchid = c.Branchid AND b.SDCode = c.SDCode INNER JOIN " +
                      "  patmstd ON patmstd.PatRegID = patmstd.PatRegID AND patmstd.FID = patmstd.FID AND patmstd.MTCode = patmstd.MTCode AND " +
                      "  patmstd.SDCode = patmstd.SDCode AND a.PatRegID = patmstd.PatRegID AND a.FID = patmstd.FID AND a.MTCode = patmstd.MTCode INNER JOIN " +
                      "  Deptwiseuser ON c.subdeptid = Deptwiseuser.DeptCodeID  where    Deptwiseuser.username='" + Username + "' and (a.FID = '" + Fid + "') AND (a.PatRegID = '" + PatRegID + "') and c.DigModule=" + DigModule + " and a.branchid=" + branchid + " AND (patmstd.MTCode = '" + TestCode + "') AND (patmstd.IspheboAccept = 1) order by c.SDOrderNo,patmstd.PackageCode, a.SDCode,a.TestNo ", conn);

        }
        else
        {
            sc = new SqlDataAdapter("SELECT DISTINCT  " +
                     "  a.SDCode, c.subdeptName, a.MTCode, c.subdeptName + ' - ' + b.Maintestname AS MainTestName, b.TextDesc, " +
                     "  patmstd.Patauthicante, " +
                     "  patmstd.TestUser, patmstd.AunticateSignatureId,  " +
                     "  patmstd.PackageCode, a.branchId, c.SDOrderNo, a.TestNo, c.subdeptid " +
                     " FROM         ResMst AS a INNER JOIN " +
                     "  MainTest AS b ON a.MTCode = b.MTCode AND a.MTCode = b.MTCode AND a.branchId = b.Branchid AND a.MTCode = b.MTCode INNER JOIN " +
                     "  SubDepartment AS c ON b.SDCode = c.SDCode AND b.SDCode = c.SDCode AND b.Branchid = c.Branchid AND b.SDCode = c.SDCode INNER JOIN " +
                     "  patmstd ON patmstd.PatRegID = patmstd.PatRegID AND patmstd.FID = patmstd.FID AND patmstd.MTCode = patmstd.MTCode AND " +
                     "  patmstd.SDCode = patmstd.SDCode AND a.PatRegID = patmstd.PatRegID AND a.FID = patmstd.FID AND a.MTCode = patmstd.MTCode  where   (a.FID = '" + Fid + "') AND (a.PatRegID = '" + PatRegID + "') and c.DigModule=" + DigModule + " and a.branchid=" + branchid + " AND (patmstd.MTCode = '" + TestCode + "') AND (patmstd.IspheboAccept = 1) order by c.SDOrderNo,patmstd.PackageCode, a.SDCode,a.TestNo ", conn);

        }
        DataTable ds = new DataTable();

        try
        {
            sc.Fill(ds);
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
        return ds;
    }

    public DataTable GetallResultEntry(string PatRegID, string Fid, int DigModule, int branchid, string UserType, string Username)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter();
        
       if (UserType == "Main Doctor" || UserType == "Technician" || UserType == "Reporting")
        {
            sc = new SqlDataAdapter("SELECT DISTINCT  "+
                      "  a.SDCode, c.subdeptName, a.MTCode, c.subdeptName + ' - ' + b.Maintestname AS MainTestName, b.TextDesc, patmstd.Patauthicante, patmstd.TestUser, "+
                      "  patmstd.AunticateSignatureId, patmstd.PackageCode, a.branchId, c.SDOrderNo, a.TestNo, c.subdeptid "+
                      "  FROM         ResMst AS a INNER JOIN "+
                      "  MainTest AS b ON a.MTCode = b.MTCode AND a.MTCode = b.MTCode AND a.branchId = b.Branchid AND a.MTCode = b.MTCode INNER JOIN "+
                      "  SubDepartment AS c ON b.SDCode = c.SDCode AND b.SDCode = c.SDCode AND b.Branchid = c.Branchid AND b.SDCode = c.SDCode INNER JOIN "+
                      "  patmstd ON patmstd.PatRegID = patmstd.PatRegID AND patmstd.FID = patmstd.FID AND patmstd.MTCode = patmstd.MTCode AND "+
                      "  patmstd.SDCode = patmstd.SDCode AND a.PatRegID = patmstd.PatRegID AND a.FID = patmstd.FID AND a.MTCode = patmstd.MTCode INNER JOIN "+
                      "  Deptwiseuser ON c.subdeptid = Deptwiseuser.DeptCodeID  where    Deptwiseuser.username='" + Username + "' and (a.FID = '" + Fid + "') AND (a.PatRegID = '" + PatRegID + "') and c.DigModule=" + DigModule + " and a.branchid=" + branchid + " AND (patmstd.IspheboAccept = 1) order by c.SDOrderNo,patmstd.PackageCode, a.SDCode,a.TestNo ", conn);

        }
        else
        {
         sc = new SqlDataAdapter("SELECT DISTINCT  "+
                  "  a.SDCode, c.subdeptName, a.MTCode, c.subdeptName + ' - ' + b.Maintestname AS MainTestName, b.TextDesc, "+
                  "  patmstd.Patauthicante, "+
                  "  patmstd.TestUser, patmstd.AunticateSignatureId,  "+
                  "  patmstd.PackageCode, a.branchId, c.SDOrderNo, a.TestNo, c.subdeptid "+
                  " FROM         ResMst AS a INNER JOIN "+
                  "  MainTest AS b ON a.MTCode = b.MTCode AND a.MTCode = b.MTCode AND a.branchId = b.Branchid AND a.MTCode = b.MTCode INNER JOIN "+
                  "  SubDepartment AS c ON b.SDCode = c.SDCode AND b.SDCode = c.SDCode AND b.Branchid = c.Branchid AND b.SDCode = c.SDCode INNER JOIN "+
                  "  patmstd ON patmstd.PatRegID = patmstd.PatRegID AND patmstd.FID = patmstd.FID AND patmstd.MTCode = patmstd.MTCode AND "+
                  "  patmstd.SDCode = patmstd.SDCode AND a.PatRegID = patmstd.PatRegID AND a.FID = patmstd.FID AND a.MTCode = patmstd.MTCode  where   (a.FID = '" + Fid + "') AND (a.PatRegID = '" + PatRegID + "') and c.DigModule=" + DigModule + " and a.branchid=" + branchid + " AND (patmstd.IspheboAccept = 1) order by c.SDOrderNo,patmstd.PackageCode, a.SDCode,a.TestNo ", conn);

        }
                DataTable ds = new DataTable();

        try
        {
            sc.Fill(ds);
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
        return ds;
    }

    public DataTable GetallResultEntry_subdept(int branchid, string MTCode,string PatRegID,string Fid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

                //SqlDataAdapter sc = new SqlDataAdapter("SELECT      ResMst.STCODE, ResMst.ID, ResMst.MTCode, ResMst.ResultTemplate, ResMst.PatRegID, ResMst.FID, ResMst.ResDate, ResMst.Method, ResMst.TestNo, "+
                //  "  ResMst.testname, ResMst.normalRange, ResMst.PanicRange, ResMst.unit, ResMst.testorderno, ResMst.PID, ResMst.SDCode, ResMst.UnitCode, ResMst.FinancialYearID, " +
                //  "  ResMst.Maintestname, ResMst.RangeFlag, ResMst.branchId, MainTest.TextDesc,SubDepartment.subdeptid, patmstd.AunticateSignatureId,patmstd.Patauthicante,SubDepartment.subdeptName " +
                //  "  FROM         ResMst INNER JOIN "+
                //  "  MainTest ON ResMst.MTCode = MainTest.MTCode AND ResMst.branchId = MainTest.Branchid INNER JOIN "+
                //  "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode INNER JOIN "+
                //  "  patmstd ON ResMst.MTCode = patmstd.MTCode AND ResMst.PatRegID = patmstd.PatRegID AND ResMst.FID = patmstd.FID WHERE patmstd.TestDeActive=0  and ResMst.MTCode in (" + MTCode + ") and (ResMst.PatRegID = '" + PatRegID + "' ) AND (ResMst.FID = '" + Fid + "' ) and ResMst.STCODE<>'S' and ResMst.branchid=" + branchid + " order by subdeptid ,TestNo,ResMst.testorderno   ", conn);


                SqlDataAdapter sc = new SqlDataAdapter("SELECT   distinct  ResMst.STCODE, ResMst.ID, ResMst.MTCode, ResMst.ResultTemplate, ResMst.PatRegID, ResMst.FID, ResMst.ResDate, ResMst.Method, ResMst.TestNo,  " +
                              "  ResMst.testname, ResMst.normalRange,ResMst.PanicRange, ResMst.unit, ResMst.testorderno, ResMst.PID, ResMst.SDCode, ResMst.UnitCode, ResMst.FinancialYearID,  " +
                              "  ResMst.Maintestname, ResMst.RangeFlag, ResMst.branchId, MainTest.TextDesc, SubDepartment.subdeptid, patmstd.AunticateSignatureId, patmstd.Patauthicante, " +
                              "  SubDepartment.subdeptName, VW_GetPreviousResult.ResultTemplate AS PReviousResult ,VW_GetPreviousResult.Patregdate,VW_GetPreviousResult.PatRegID as PrevRegNo, " +
                              " convert(varchar(20), VW_GetPreviousResult.Patregdate,103)+' '+convert(varchar(20),convert(time,  VW_GetPreviousResult.Patregdate),100)+': RegNo: '+VW_GetPreviousResult.PatRegID as PrevResultDate "+
                              " ,isnull( patmstd.QcCheck,0) as QcCheck FROM         ResMst INNER JOIN " +
                              "  MainTest ON ResMst.MTCode = MainTest.MTCode AND ResMst.branchId = MainTest.Branchid INNER JOIN " +
                              "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode INNER JOIN " +
                              "  patmstd ON ResMst.MTCode = patmstd.MTCode AND ResMst.PatRegID = patmstd.PatRegID AND ResMst.FID = patmstd.FID LEFT OUTER JOIN " +
                              "  VW_GetPreviousResult ON ResMst.STCODE = VW_GetPreviousResult.STCODE AND ResMst.MTCode = VW_GetPreviousResult.MTCode AND  " +
                              "  ResMst.SDCode = VW_GetPreviousResult.SDCode WHERE patmstd.TestDeActive=0  and ResMst.MTCode in (" + MTCode + ") and (ResMst.PatRegID = '" + PatRegID + "' ) AND (ResMst.FID = '" + Fid + "' ) and ResMst.STCODE<>'S' and ResMst.branchid=" + branchid + " order by subdeptid ,TestNo,ResMst.testorderno   ", conn);

        DataTable ds = new DataTable();

        try
        {
            sc.Fill(ds);
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
        return ds;
    }

    public bool UpdateEmailstatus(string PatRegID, string FID, string MTCode, int branchid, string EmailRepBy)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("UPDATE Patmstd SET PatientEmail=1,PatRepDate=@PatRepDate ,EmailRepBy='" + EmailRepBy + "'  WHERE PatRegID= '" + PatRegID + "' and FID= '" + FID.Trim() + "' and MTCode='" + MTCode + "' and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PatRepDate", SqlDbType.DateTime)).Value = Date.getdate();
        try
        {
            conn.Close();
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
                // Log an event in the Application Event Log.
                throw;
            }
        }

        return true;
    }

    public bool UpdateEmailstatus_Doc(string PatRegID, string FID, string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("UPDATE Patmstd SET DoctorEmail=1,PatRepDate=@PatRepDate WHERE PatRegID= '" + PatRegID + "' and FID= '" + FID.Trim() + "' and MTCode='" + MTCode + "' and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PatRepDate", SqlDbType.DateTime)).Value = Date.getdate();
        try
        {
            conn.Close();
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
                // Log an event in the Application Event Log.
                throw;
            }
        }

        return true;
    }

    public bool UpdateEmailstatus_testwise(string PatRegID, string FID, string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("UPDATE patmstd SET PatientMailSend=1 WHERE PatRegID= '" + PatRegID + "' and FID= '" + FID.Trim() + "' and MTCode='" + MTCode + "' and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PatRepDate", SqlDbType.DateTime)).Value = Date.getdate();
        try
        {
            conn.Close();
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
                // Log an event in the Application Event Log.
                throw;
            }
        }

        return true;
    }


  
    public void AlterVW_serialportdate_Old(string PatRegID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" alter view VW_serialportdate as SELECT     serialportdataID,   userID, machineName, medicalTestName, medicalTestResult, medicalTestResultUnit FROM serialportuserparseddata " +
                 " where userID ='" + PatRegID + "' ", con);
            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }

       public void AlterVW_serialportdate_NEWW(string PatRegID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        con.Open();
        SqlCommand cmd = new SqlCommand("Sp_VW_serialportdate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
           

            cmd.Parameters.AddWithValue("@PatRegID", PatRegID);
           

            cmd.ExecuteNonQuery();


        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Dispose();
            con.Close();
        }
    }


    public void AlterVW_serialportdate(string PatRegID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" alter view VW_serialportdate as SELECT     serialportdataID,  SUBSTRING(userID, 1 ,case when  CHARINDEX('-', userID ) = 0 then LEN(userID) else CHARINDEX('-', userID) -1 end) as userID, machineName, medicalTestName, medicalTestResult, medicalTestResultUnit FROM serialportuserparseddata " +
                 " where SUBSTRING(userID, 1 ,case when  CHARINDEX('-', userID ) = 0 then LEN(userID) else CHARINDEX('-', userID) -1 end) ='" + PatRegID + "' ", con);
            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }
    public void AlterVW_serialportdate_barcode(string barcode)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" alter view VW_serialportdate as SELECT      serialportdataID,  SUBSTRING(userID, 1 ,case when  CHARINDEX('-', userID ) = 0 then LEN(userID) else CHARINDEX('-', userID) -1 end) as userID, machineName, medicalTestName, medicalTestResult, medicalTestResultUnit FROM serialportuserparseddata " +
                 " where SUBSTRING(userID, 1 ,case when  CHARINDEX('-', userID ) = 0 then LEN(userID) else CHARINDEX('-', userID) -1 end) in (" + barcode + ")  ", con);
            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }
    public void AlterVW_Int_Barcode(int branchid, string Barcode, string FID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" alter view VW_Int as SELECT     patmstd.PID, patmst.Patname, patmst.PatRegID, patmstd.BarcodeID, patmstd.Patauthicante, patmstd.PatRegID AS Expr1, Machinecodemap.Instname, "+
                   "  Machinecodemap.ParameterCode, Machinecodemap.Mapcode, SubTest.STCODE, SubTest.SDCode, SubTest.TestName, MainTest.Singleformat, SubTest.MTCode, "+
                   "     patmst.FID " +
                  "  FROM         MainTest INNER JOIN "+
                  "  patmst INNER JOIN "+
                  "  patmstd ON patmst.PID = patmstd.PID AND patmst.FID = patmstd.FID ON MainTest.MTCode = patmstd.MTCode INNER JOIN "+
                  "  SubTest INNER JOIN "+
                  "  Machinecodemap ON SubTest.MTCode = Machinecodemap.MTCode AND SubTest.STCODE = Machinecodemap.ParameterCode ON "+
                  "  MainTest.MTCode = Machinecodemap.MTCode  " +
                  // " where patmstd.Patauthicante<>'Authorized' and dbo.patmstd.BarcodeID  in (" + Barcode + ") and dbo.patmst.FID='" + FID + "' and MainTest.SingleFormat<>'Single Value' " + //='Registered'
                 " where patmstd.Patauthicante='Registered' and dbo.patmstd.BarcodeID  in (" + Barcode + ") and dbo.patmst.FID='" + FID + "' and MainTest.SingleFormat<>'Single Value' " + //='Registered'

                   " Union all "+
                  " SELECT DISTINCT  "+
                  "  patmstd.PID, patmst.Patname, patmst.PatRegID, patmstd.BarcodeID, patmstd.Patauthicante, patmstd.PatRegID AS Expr1, Machinecodemap.Instname, "+
                   " Machinecodemap.ParameterCode, Machinecodemap.Mapcode, MainTest.MTCode AS STCode, MainTest.SDCode, MainTest.Maintestname AS TestName, "+
                   " MainTest.Singleformat, MainTest.MTCode, patmst.FID " +
              " FROM         MainTest INNER JOIN "+
               " patmst INNER JOIN "+
               " patmstd ON patmst.PID = patmstd.PID AND patmst.FID = patmstd.FID ON MainTest.MTCode = patmstd.MTCode INNER JOIN "+
               " Machinecodemap ON MainTest.MTCode = Machinecodemap.MTCode " +
           // " where patmstd.Patauthicante<>'Authorized' and dbo.patmstd.BarcodeID  in (" + Barcode + ") and dbo.patmst.FID='" + FID + "' and MainTest.SingleFormat='Single Value' ", con); //='Registered'
             " where patmstd.Patauthicante='Registered' and dbo.patmstd.BarcodeID  in (" + Barcode + ") and dbo.patmst.FID='" + FID + "' and MainTest.SingleFormat='Single Value' ", con); //='Registered'


            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }


    public void AlterVW_Int(int branchid, string PatRegID, string FID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
          

            cmd = new SqlCommand(" alter view VW_Int as SELECT     patmstd.PID, patmst.Patname, patmst.PatRegID, patmstd.BarcodeID, patmstd.Patauthicante, patmstd.PatRegID AS Expr1, "+
                            "  Machinecodemap.Instname,  "+
                            "  Machinecodemap.ParameterCode, Machinecodemap.Mapcode, SubTest.STCODE, SubTest.SDCode, SubTest.TestName, "+
                            "  MainTest.Singleformat, SubTest.MTCode,   patmst.FID  "+
                            "  FROM         MainTest INNER JOIN   patmst INNER JOIN   patmstd ON patmst.PID = patmstd.PID AND patmst.FID = patmstd.FID "+
                            "  ON MainTest.MTCode = patmstd.MTCode INNER JOIN   SubTest INNER JOIN   Machinecodemap ON "+
                            "  SubTest.MTCode = Machinecodemap.MTCode AND SubTest.STCODE = Machinecodemap.ParameterCode ON  "+
                            "  MainTest.MTCode = Machinecodemap.MTCode " +
                            " where  patmstd.Patauthicante='Registered' and dbo.patmst.PatRegID= '" + PatRegID + "' and dbo.patmst.FID='" + FID + "' and MainTest.SingleFormat<>'Single Value' " +
                            " Union all " +
                            " SELECT DISTINCT   patmstd.PID, patmst.Patname, patmst.PatRegID, patmstd.BarcodeID, patmstd.Patauthicante, "+
                            " patmstd.PatRegID AS Expr1, Machinecodemap.Instname,   Machinecodemap.ParameterCode,Machinecodemap.Mapcode, "+
                            " MainTest.MTCode as STCode, MainTest.SDCode,MainTest.Maintestname as TestName,  MainTest.Singleformat,  "+
                            " MainTest.MTCode , patmst.FID  "+
                            " FROM         MainTest INNER JOIN "+
                             "   patmst INNER JOIN "+
                             "   patmstd ON patmst.PID = patmstd.PID AND patmst.FID = patmstd.FID ON MainTest.MTCode = patmstd.MTCode INNER JOIN "+
                             "   Machinecodemap ON MainTest.MTCode = Machinecodemap.MTCode" +
                            " where patmstd.Patauthicante='Registered' and dbo.patmst.PatRegID= '" + PatRegID + "' and dbo.patmst.FID='" + FID + "' and MainTest.SingleFormat='Single Value' ", con);

            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }


    public static string getsdcode(string MTCODE, int branchid,int PID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        string s = "";

        try
        {

            conn.Open();
            sc = new SqlCommand("select distinct SDCODE from patmstd where MTCode=@MTCode and PID=@PID  and branchid=" + branchid + "", conn);
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.NVarChar, 50)).Value = PID;
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCODE;

            object o = sc.ExecuteScalar();
            string  i = Convert.ToString(o);
          
            s = i.ToString();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {

            conn.Close(); conn.Dispose();

        }
        return s;

    }
    public PatSt_Bal_C(string PatRegID, string FID, string SDCode, string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "select * from Patmstd where SDCode =@SDCode and MTCode =@MTCode and PatRegID =@PatRegID and FID =@FID and branchid=" + branchid + "";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(SDCode);
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(MTCode);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {

                this.PatRegID = (sdr["PatRegID"].ToString());
                this.FID = sdr["FID"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.STCODE = sdr["MTCode"].ToString();
               
                this.Reason = sdr["MTCode"].ToString();
               
                this.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
              
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.Patauthicante = sdr["Patauthicante"].ToString();
              
                this.PackageCode = sdr["PackageCode"].ToString();
                if (sdr["PatRepDate"] == DBNull.Value)
                    this.ReportDate = Date.getMinDate();
                else
                    this.ReportDate = Convert.ToDateTime(sdr["PatRepDate"]);
                if (sdr["TestedDate"] == DBNull.Value)
                    this.Testdate = Date.getMinDate();
                else
                    this.Testdate = Convert.ToDateTime(sdr["TestedDate"]);
                if (sdr["AunticateSignatureId"] is DBNull)
                    this.AunticateSignatureId = 0;
                else
                    this.AunticateSignatureId = Convert.ToInt32(sdr["AunticateSignatureId"]);
               
            }
            else
            {
                throw new Exception("Record not found");
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


    public PatSt_Bal_C(string PatRegID, string FID, string MTCode, int p, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = conn.CreateCommand();       
        sc.CommandText = "select * from patmstd where MTCode =@MTCode and PatRegID =@PatRegID and FID =@FID and branchid=" + branchid + "";
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
     
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(MTCode);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {

                this.PatRegID = (sdr["PatRegID"].ToString());
                this.FID = sdr["FID"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.STCODE = sdr["MTCode"].ToString();
               
                this.Reason = sdr["MTCode"].ToString();
              
                if (sdr["Patrepstatus"] == DBNull.Value)
                    this.Patrepstatus = false;
                else
                    this.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
               
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.Patauthicante = sdr["Patauthicante"].ToString();
             
                this.PackageCode = sdr["PackageCode"].ToString();
                if (sdr["PatRepDate"] == DBNull.Value)
                    this.ReportDate = Date.getMinDate();
                else
                    this.ReportDate = Convert.ToDateTime(sdr["PatRepDate"]);
                if (sdr["TestedDate"] == DBNull.Value)
                    this.Testdate = Date.getMinDate();
                else
                    this.Testdate = Convert.ToDateTime(sdr["TestedDate"]);
                if (sdr["AunticateSignatureId"] is DBNull)
                    this.AunticateSignatureId = 0;
                else
                    this.AunticateSignatureId = Convert.ToInt32(sdr["AunticateSignatureId"]);
              
                if (sdr["PID"] != DBNull.Value)
                    this.PID = Convert.ToInt32(sdr["PID"]);
                this.P_ReportRemark = sdr["ReportRemark"].ToString();
                if (sdr["LabTechnicianid"] is DBNull)
                    this.Technicianid = 0;
                else
                    this.Technicianid = Convert.ToInt32(sdr["LabTechnicianid"]);

                if (sdr["TechnicanFirst"] is DBNull)
                    this.TechnicanFirst = 0;
                else
                    this.TechnicanFirst = Convert.ToInt32(sdr["TechnicanFirst"]);
                if (sdr["TechnicanSecond"] is DBNull)
                    this.TechnicanSecond = 0;
                else
                    this.TechnicanSecond = Convert.ToInt32(sdr["TechnicanSecond"]);
            }
            else
            {
                throw new Exception("Record not found");
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
    public PatSt_Bal_C(string PatRegID, string FID, string grpcode, int p,int q, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = conn.CreateCommand();

        sc.CommandText = "select * from patmstd where PackageCode =@PackageCode and PatRegID =@PatRegID and FID =@FID and branchid=" + branchid + "";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);        
        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(grpcode);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {

                this.PatRegID = (sdr["PatRegID"].ToString());
                this.FID = sdr["FID"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.STCODE = sdr["MTCode"].ToString();
              
                this.Reason = sdr["MTCode"].ToString();
              
                this.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
             
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.Patauthicante = sdr["Patauthicante"].ToString();
              
                this.PackageCode = sdr["PackageCode"].ToString();
                if (sdr["PatRepDate"] == DBNull.Value)
                    this.ReportDate = Date.getMinDate();
                else
                    this.ReportDate = Convert.ToDateTime(sdr["PatRepDate"]);
                if (sdr["TestedDate"] == DBNull.Value)
                    this.Testdate = Date.getMinDate();
                else
                    this.Testdate = Convert.ToDateTime(sdr["TestedDate"]);
                if (sdr["AunticateSignatureId"] is DBNull)
                    this.AunticateSignatureId = 0;
                else
                    this.AunticateSignatureId = Convert.ToInt32(sdr["AunticateSignatureId"]);
               
               
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


    public PatSt_Bal_C(string PatRegID, string FID, string SDCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = conn.CreateCommand();


        sc.CommandText = "select * from Patmstd where MTCode=@SDCode and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(SDCode);        
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                if (!(sdr["AunticateSignatureId"] is DBNull))
                {
                    this.AunticateSignatureId = Convert.ToInt32(sdr["AunticateSignatureId"]);
                }
                this.PatRegID = (sdr["PatRegID"].ToString());
                this.FID = sdr["FID"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.STCODE = sdr["MTCode"].ToString();
              
                this.Reason = sdr["MTCode"].ToString();
               
                this.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
              
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.Patauthicante = sdr["Patauthicante"].ToString();
              
                this.PackageCode = sdr["PackageCode"].ToString();
                if (sdr["PatRepDate"] == DBNull.Value)
                    this.ReportDate = Date.getMinDate();
                else
                    this.ReportDate = Convert.ToDateTime(sdr["PatRepDate"]);
                if (sdr["TestedDate"] == DBNull.Value)
                    this.Testdate = Date.getMinDate();
                else
                    this.Testdate = Convert.ToDateTime(sdr["TestedDate"]);
               
               
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
    public PatSt_Bal_C(string PatRegID, string FID, string SDCode, int branchid,int i,int j,int k)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = conn.CreateCommand();


        sc.CommandText = "select * from Patmstd where SDCode=@SDCode and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(SDCode);
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                if (!(sdr["AunticateSignatureId"] is DBNull))
                {
                    this.AunticateSignatureId = Convert.ToInt32(sdr["AunticateSignatureId"]);
                }
                this.PatRegID = (sdr["PatRegID"].ToString());
                this.FID = sdr["FID"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.STCODE = sdr["MTCode"].ToString();
              
                this.Reason = sdr["MTCode"].ToString();
                this.PPID = Convert.ToInt32(sdr["PPID"]);
                this.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
              
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.Patauthicante = sdr["Patauthicante"].ToString();
               
                this.PackageCode = sdr["PackageCode"].ToString();
                if (sdr["PatRepDate"] == DBNull.Value)
                    this.ReportDate = Date.getMinDate();
                else
                    this.ReportDate = Convert.ToDateTime(sdr["PatRepDate"]);
                if (sdr["TestedDate"] == DBNull.Value)
                    this.Testdate = Date.getMinDate();
                else
                    this.Testdate = Convert.ToDateTime(sdr["TestedDate"]);
              
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

    public PatSt_Bal_C(string PatRegID, string FID, string SDCode, int branchid, int i, int j, int k,int l)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "select * from Patmstd where SDCode=@SDCode and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + " and AunticateSignatureId<>0";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(SDCode);
       
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                if (!(sdr["AunticateSignatureId"] is DBNull))
                {
                    this.AunticateSignatureId = Convert.ToInt32(sdr["AunticateSignatureId"]);
                }
                this.PatRegID = (sdr["PatRegID"].ToString());
                this.FID = sdr["FID"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.STCODE = sdr["MTCode"].ToString();
               
                this.Reason = sdr["MTCode"].ToString();
               
                this.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
              
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.Patauthicante = sdr["Patauthicante"].ToString();
              
                this.PackageCode = sdr["PackageCode"].ToString();
                if (sdr["PatRepDate"] == DBNull.Value)
                    this.ReportDate = Date.getMinDate();
                else
                    this.ReportDate = Convert.ToDateTime(sdr["PatRepDate"]);
                if (sdr["TestedDate"] == DBNull.Value)
                    this.Testdate = Date.getMinDate();
                else
                    this.Testdate = Convert.ToDateTime(sdr["TestedDate"]);
               
               
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

    public PatSt_Bal_C(string PatRegID, string FID, string SDCode, string MTCode, string STCODE, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "select * from Patmstd where SDCode =@SDCode and MTCode =@MTCode and PatRegID =@PatRegID and FID =@FID and STCODE=@STCODE and branchid=" + branchid + "";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(SDCode);
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(MTCode);
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Convert.ToString(STCODE);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {

                this.PatRegID = (sdr["PatRegID"].ToString());
                this.FID = sdr["FID"].ToString();
                this.SDCode = sdr["SDCode"].ToString();
                this.MTCode = sdr["MTCode"].ToString();
                this.STCODE = sdr["STCODE"].ToString();
              
                this.Reason = sdr["reason"].ToString();
                this.PPID = Convert.ToInt32(sdr["PPID"]);
                this.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
              
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.PackageCode = sdr["PackageCode"].ToString();
                if (sdr["PatRepDate"] == DBNull.Value)
                    this.ReportDate = Date.getMinDate();
                else
                    this.ReportDate = Convert.ToDateTime(sdr["PatRepDate"]);
                if (sdr["TestedDate"] == DBNull.Value)
                    this.Testdate = Date.getMinDate();
                else
                    this.Testdate = Convert.ToDateTime(sdr["TestedDate"]);
                                
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }

    }
    
   
    public bool Update(string PrintStatusCode, string PatRegID, string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("UPDATE Patmstd SET Patrepstatus = '1' WHERE" + " " + PrintStatusCode + "' " + "AND PatRegID= '" + PatRegID + "' AND FID= '" + FID + "'  AND TestStatus = '1'  and branchid=" + branchid + "", conn);

        try
        {
            conn.Close(); 
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { throw; }
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


    public bool UpdatePrintstatus(string PatRegID, string FID, string MTCode, int branchid, string Printedby)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("UPDATE Patmstd SET Patrepstatus=1,PatRepDate=@PatRepDate ,Printedby='" + Printedby + "' WHERE PatRegID= '" + PatRegID + "' and FID= '" + FID.Trim() + "' and MTCode='" + MTCode + "' and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PatRepDate", SqlDbType.DateTime)).Value = Date.getdate();
        try
        {
            conn.Close();
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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        
        return true;
    }

    public bool UpdateReportDate(string PatRegID, string FID, string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("UPDATE Patmstd SET PatRepDate=@PatRepDate WHERE PatRegID= '" + PatRegID + "' and FID= '" + FID.Trim() + "' and MTCode='" + MTCode + "' and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PatRepDate", SqlDbType.DateTime)).Value =Date.getdate();
        try
        {
            conn.Close();
            conn.Open();
            sc.ExecuteNonQuery();
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

   

    public bool Update(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();

        if ((this.TestUser != null) && (this.TestUser != ""))
        {

            sc.CommandText = "update Patmstd set SDCode=@SDCode,Patrepstatus=@Patrepstatus,Patauthicante=@Patauthicante,TestedDate=@TestedDate,TestUser=@TestUser,AunticateSignatureId=@AunticateSignatureId,PackageCode=@PackageCode,Technicanby=@Technicanby ,DescImagePath=@DescImagePath,TechnicanFirst=@TechnicanFirst,TechnicanSecond=@TechnicanSecond,AbandantOn=@AbandantOn,AbandantBy=@AbandantBy  where MTCode=@MTCode and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "";
            
        }
        else
        {

            sc.CommandText = "update Patmstd set SDCode=@SDCode,Patrepstatus=@Patrepstatus,Patauthicante=@Patauthicante,TestedDate=@TestedDate,AunticateSignatureId=@AunticateSignatureId,PackageCode=@PackageCode,Technicanby=@Technicanby  ,DescImagePath=@DescImagePath,TechnicanFirst=@TechnicanFirst,TechnicanSecond=@TechnicanSecond,AbandantOn=@AbandantOn,AbandantBy=@AbandantBy  where MTCode=@MTCode and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "";
            
        }
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (this.PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.FID);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.SDCode);
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.MTCode);
        sc.Parameters.Add(new SqlParameter("@Patrepstatus", SqlDbType.Bit)).Value = Convert.ToBoolean(this.Patrepstatus);
     
        if (this.Patauthicante == null)
            sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.Patauthicante);
        if (this.RegisterUser == null)
            sc.Parameters.Add(new SqlParameter("@RegisterUser", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@RegisterUser", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.RegisterUser);
        if (this.TestUser == null)
            sc.Parameters.Add(new SqlParameter("@TestUser", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TestUser", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.TestUser);
       
        if (this.Testdate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@TestedDate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TestedDate", SqlDbType.DateTime)).Value =Convert.ToDateTime(this.Testdate);
        if (this.AunticateSignatureId == null)
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = this.AunticateSignatureId;

        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 50)).Value = (this.PackageCode);
        sc.Parameters.Add(new SqlParameter("@Technicanby", SqlDbType.NVarChar, 50)).Value = (this.Technicanby);

        sc.Parameters.Add(new SqlParameter("@DescImagePath", SqlDbType.NVarChar, 550)).Value = (this.DescImagePath);

        if (this.TechnicanFirst == null)
            sc.Parameters.Add(new SqlParameter("@TechnicanFirst", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TechnicanFirst", SqlDbType.Int)).Value = this.TechnicanFirst;

        if (this.TechnicanSecond == null)
            sc.Parameters.Add(new SqlParameter("@TechnicanSecond", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TechnicanSecond", SqlDbType.Int)).Value = this.TechnicanSecond;


        if (this.AbandantOn == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@AbandantOn", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@AbandantOn", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.AbandantOn);

        sc.Parameters.Add(new SqlParameter("@AbandantBy", SqlDbType.NVarChar, 255)).Value = this.AbandantBy;


        SqlDataReader sdr = null;

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

        return true;
    }
    public bool Update_Second(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();

        if ((this.TestUser != null) && (this.TestUser != ""))
        {

            sc.CommandText = "update Patmstd set SDCode=@SDCode,Patrepstatus=@Patrepstatus,Patauthicante=@Patauthicante,TestedDate=@TestedDate,TestUser=@TestUser,AunticateSignatureId=@AunticateSignatureId,PackageCode=@PackageCode,Technicanby=@Technicanby ,DescImagePath=@DescImagePath,TechnicanSecond=@TechnicanSecond,AbandantOn=@AbandantOn,AbandantBy=@AbandantBy  where MTCode=@MTCode and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "";

        }
        else
        {

            sc.CommandText = "update Patmstd set SDCode=@SDCode,Patrepstatus=@Patrepstatus,Patauthicante=@Patauthicante,TestedDate=@TestedDate,AunticateSignatureId=@AunticateSignatureId,PackageCode=@PackageCode,Technicanby=@Technicanby  ,DescImagePath=@DescImagePath,TechnicanSecond=@TechnicanSecond,AbandantOn=@AbandantOn,AbandantBy=@AbandantBy  where MTCode=@MTCode and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "";

        }
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (this.PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.FID);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.SDCode);
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.MTCode);
        sc.Parameters.Add(new SqlParameter("@Patrepstatus", SqlDbType.Bit)).Value = Convert.ToBoolean(this.Patrepstatus);

        if (this.Patauthicante == null)
            sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.Patauthicante);
        if (this.RegisterUser == null)
            sc.Parameters.Add(new SqlParameter("@RegisterUser", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@RegisterUser", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.RegisterUser);
        if (this.TestUser == null)
            sc.Parameters.Add(new SqlParameter("@TestUser", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TestUser", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.TestUser);

        if (this.Testdate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@TestedDate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TestedDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.Testdate);
        if (this.AunticateSignatureId == null)
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = this.AunticateSignatureId;

        sc.Parameters.Add(new SqlParameter("@PackageCode", SqlDbType.NVarChar, 50)).Value = (this.PackageCode);
        sc.Parameters.Add(new SqlParameter("@Technicanby", SqlDbType.NVarChar, 50)).Value = (this.Technicanby);

        sc.Parameters.Add(new SqlParameter("@DescImagePath", SqlDbType.NVarChar, 550)).Value = (this.DescImagePath);

        //if (this.TechnicanFirst == null)
        //    sc.Parameters.Add(new SqlParameter("@TechnicanFirst", SqlDbType.Int)).Value = DBNull.Value;
        //else
        //    sc.Parameters.Add(new SqlParameter("@TechnicanFirst", SqlDbType.Int)).Value = this.TechnicanFirst;

        if (this.TechnicanSecond == null)
            sc.Parameters.Add(new SqlParameter("@TechnicanSecond", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TechnicanSecond", SqlDbType.Int)).Value = this.TechnicanSecond;


        if (this.AbandantOn == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@AbandantOn", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@AbandantOn", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.AbandantOn);

        sc.Parameters.Add(new SqlParameter("@AbandantBy", SqlDbType.NVarChar, 255)).Value = this.AbandantBy;


        SqlDataReader sdr = null;

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

        return true;
    }

    public bool UpdateNew(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();

        sc.CommandText = "update Patmstd set AunticateSignatureId=@AunticateSignatureId where MTCode=@MTCode and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (this.PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.FID);        
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.MTCode);
              
        if (this.AunticateSignatureId == null)
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = this.AunticateSignatureId;
        


        SqlDataReader sdr = null;

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

        return true;
    }

    public bool InsertUpdate_AddResult_WithoutAmend(int branchid, int Flag)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[SP_phdataupdatefrm_WithoutAmend]";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (this.PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.FID);
        
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.MTCode);

        sc.Parameters.Add(new SqlParameter("@Patrepstatus", SqlDbType.Bit)).Value = Convert.ToBoolean(this.Patrepstatus);
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.STCODE);
        
        sc.Parameters.Add(new SqlParameter("@PPID", SqlDbType.Int)).Value = Convert.ToInt32(this.PPID);
              if (this.Patauthicante == null)
            sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.Patauthicante);
    
        if (this.TestUser == null)
            sc.Parameters.Add(new SqlParameter("@TestUser", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TestUser", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.TestUser);

         if (this.Testdate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@TestedDate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TestedDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.Testdate);

        if (this.ReportDate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@ReportDate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@ReportDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.ReportDate);

        if (this.AunticateSignatureId == null)
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = this.AunticateSignatureId;

        if (this.Technicianid == null)
            sc.Parameters.Add(new SqlParameter("@LabTechnicianid", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@LabTechnicianid", SqlDbType.Int)).Value = this.Technicianid;

       
        if (this.ResultTemplate != null)
            sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 3555)).Value = this.ResultTemplate;
        else
            sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 3555)).Value = DBNull.Value;
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
              sc.Parameters.Add(new SqlParameter("@ReportRemark", SqlDbType.NVarChar, 500)).Value = P_ReportRemark;
              sc.Parameters.Add(new SqlParameter("@Technicanby", SqlDbType.NVarChar, 50)).Value = P_Technicanby;
            //  sc.Parameters.Add(new SqlParameter("@PanicResult", SqlDbType.Int)).Value = P_PanicResult;

              if (this.TechnicanFirst == null)
                  sc.Parameters.Add(new SqlParameter("@TechnicanFirst", SqlDbType.Int)).Value = DBNull.Value;
              else
                  sc.Parameters.Add(new SqlParameter("@TechnicanFirst", SqlDbType.Int)).Value = this.TechnicanFirst;

              if (this.TechnicanSecond == null)
                  sc.Parameters.Add(new SqlParameter("@TechnicanSecond", SqlDbType.Int)).Value = DBNull.Value;
              else
                  sc.Parameters.Add(new SqlParameter("@TechnicanSecond", SqlDbType.Int)).Value = this.TechnicanSecond;

              if (this.AbandantOn == Date.getMinDate())
                  sc.Parameters.Add(new SqlParameter("@AbandantOn", SqlDbType.DateTime)).Value = DBNull.Value;
              else
                  sc.Parameters.Add(new SqlParameter("@AbandantOn", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.AbandantOn);

              sc.Parameters.Add(new SqlParameter("@AbandantBy", SqlDbType.NVarChar, 255)).Value = this.AbandantBy;

              if (this.SampleDate == Date.getMinDate())
                  sc.Parameters.Add(new SqlParameter("@SampleDate", SqlDbType.DateTime)).Value = DBNull.Value;
              else
                  sc.Parameters.Add(new SqlParameter("@SampleDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.SampleDate);

            try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch(Exception ex)
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

    public bool InsertUpdate_AddResult(int branchid, int Flag)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[SP_phdataupdatefrm]";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (this.PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.FID);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.MTCode);

        sc.Parameters.Add(new SqlParameter("@Patrepstatus", SqlDbType.Bit)).Value = Convert.ToBoolean(this.Patrepstatus);
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.STCODE);

        sc.Parameters.Add(new SqlParameter("@PPID", SqlDbType.Int)).Value = Convert.ToInt32(this.PPID);
        if (this.Patauthicante == null)
            sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.Patauthicante);

        if (this.TestUser == null)
            sc.Parameters.Add(new SqlParameter("@TestUser", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TestUser", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.TestUser);

        if (this.Testdate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@TestedDate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TestedDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.Testdate);

        if (this.ReportDate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@ReportDate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@ReportDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.ReportDate);

        if (this.AunticateSignatureId == null)
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = this.AunticateSignatureId;

        if (this.Technicianid == null)
            sc.Parameters.Add(new SqlParameter("@LabTechnicianid", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@LabTechnicianid", SqlDbType.Int)).Value = this.Technicianid;


        if (this.ResultTemplate != null)
            sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 3555)).Value = this.ResultTemplate;
        else
            sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 3555)).Value = DBNull.Value;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@ReportRemark", SqlDbType.NVarChar, 500)).Value = P_ReportRemark;
        sc.Parameters.Add(new SqlParameter("@Technicanby", SqlDbType.NVarChar, 50)).Value = P_Technicanby;
        //  sc.Parameters.Add(new SqlParameter("@PanicResult", SqlDbType.Int)).Value = P_PanicResult;

        if (this.TechnicanFirst == null)
            sc.Parameters.Add(new SqlParameter("@TechnicanFirst", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TechnicanFirst", SqlDbType.Int)).Value = this.TechnicanFirst;

        if (this.TechnicanSecond == null)
            sc.Parameters.Add(new SqlParameter("@TechnicanSecond", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TechnicanSecond", SqlDbType.Int)).Value = this.TechnicanSecond;

        if (this.AbandantOn == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@AbandantOn", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@AbandantOn", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.AbandantOn);

        sc.Parameters.Add(new SqlParameter("@AbandantBy", SqlDbType.NVarChar, 255)).Value = this.AbandantBy;


        if (this.SampleDate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@SampleDate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@SampleDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.SampleDate);

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

   
    public string GetShortform(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[SP_phsrtdata]";
        
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (this.PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.FID);
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        object shortform;
        try
        {
            conn.Open();
            shortform = sc.ExecuteScalar();
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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        return shortform.ToString();
    }

    public void AlterView_Barcode_Temp_Direct(string TestCode, int PID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            if (TestCode != "")
            {
                cmd = new SqlCommand(" alter view VW_GetBarcode as SELECT DISTINCT  " +
                       " MainTest.Maintestname AS TestName, MainTest.Sampletype, patmstd.Patmstid, patmstd.PatRegID, patmstd.FID, patmstd.MTCode, patmstd.SDCode, " +
                       " patmstd.PackageCode, patmstd.PID, patmstd.UnitCode, patmstd.BarcodeID, patmstd.DigModule, patmstd.Patregdate, patmstd.TestRate, patmstd.Email, " +
                       " patmstd.dramt,  patmstd.cons, patmstd.MainRate, patmstd.Branchid, patmstd.Createdby, patmstd.Createdon, " +
                       " patmstd.Updatedby, patmstd.Updatedon, patmstd.IsActive, patmstd.Testdisc, patmstd.Testrecamt, patmstd.PatientFlag, patmst.RefDr, patmstd.IspheboAccept, " +
                       " patmst.intial + ' ' + patmst.Patname AS Patient_Name, patmst.CenterName, patmst.Age, patmst.MDY, patmst.sex , patmstd.Labno, patmst.LabRegMediPro , MainTest.Shortcode ,patmstd.SpecimanNo , CAST(patmstd.BarCodeImage AS VARBINARY(8000)) as BarCodeImage,PheboAcceptBy,replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) as DailyseqNo " +
                       " FROM         patmstd INNER JOIN " +
                       " MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN " +
                       " patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID  " +

                     " where  (patmst.IsActive = 1) and patmstd.TestDeActive=0  and dbo.patmstd.PID='" + PID + "'  and patmstd.SDCode in (" + TestCode + ")", con);
            }
            else
            {
                cmd = new SqlCommand(" alter view VW_GetBarcode as SELECT DISTINCT   MainTest.Maintestname AS TestName, MainTest.Sampletype, patmstd.Patmstid, patmstd.PatRegID, patmstd.FID, "+
                      "  patmstd.MTCode, patmstd.SDCode,  patmstd.PackageCode, patmstd.PID, patmstd.UnitCode, patmstd.BarcodeID, patmstd.DigModule, "+
                      "  patmstd.Patregdate, patmstd.TestRate, patmstd.Email,  patmstd.dramt,  "+
                      "  patmstd.cons, patmstd.MainRate, patmstd.Branchid, patmstd.Createdby, patmstd.Createdon,  patmstd.Updatedby, "+
                      "  patmstd.Updatedon, patmstd.IsActive, patmstd.Testdisc, patmstd.Testrecamt, patmstd.PatientFlag, patmst.RefDr, "+
                      "  patmstd.IspheboAccept,  patmst.intial + ' ' + patmst.Patname AS Patient_Name, patmst.CenterName, patmst.Age, patmst.MDY, "+
                      "  patmst.sex ,patmstd.Labno, patmst.LabRegMediPro ,MainTest.Shortcode ,patmstd.SpecimanNo , CAST(patmstd.BarCodeImage AS VARBINARY(8000)) as BarCodeImage,PheboAcceptBy,replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) as DailyseqNo " +
                      "  FROM         patmstd INNER JOIN  MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN  patmst ON  "+
                      "  patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID " +
                     " where (patmst.IsActive = 1) and patmstd.TestDeActive=0  and  dbo.patmstd.PID='" + PID + "'  ", con);
            }
            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }


    public void AlterView_Barcode_Temp( string TestCode, int PID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            if (TestCode != "")
            {
                cmd = new SqlCommand(" alter view VW_GetBarcode as SELECT DISTINCT  " +
                      "  MainTest.Maintestname AS TestName, MainTest.Sampletype, patmstd.Patmstid, patmstd.PatRegID, patmstd.FID, patmstd.MTCode, patmstd.SDCode, " +
                      "  patmstd.PackageCode, patmstd.PID, patmstd.UnitCode, case when isnull(patmstd.PhlebotomistRejectremark,'') <>'' then patmstd.BarcodeID+'-'+'1' else patmstd.BarcodeID end as BarcodeID, patmstd.DigModule, patmstd.Patregdate, patmstd.TestRate, patmstd.Email, " +
                      "  patmstd.Dramt, patmstd.cons, patmstd.MainRate, patmstd.Branchid, patmstd.Createdby, patmstd.Createdon, patmstd.Updatedby, patmstd.Updatedon, " +
                      "  patmstd.IsActive, patmstd.Testdisc, patmstd.Testrecamt, patmstd.PatientFlag, patmst.RefDr, patmstd.IspheboAccept,  " +
                      "  patmst.intial + ' ' + patmst.Patname AS Patient_Name, patmst.CenterName, patmst.Age, patmst.MDY, patmst.sex, patmstd.Labno, patmst.LabRegMediPro,MainTest.Shortcode,patmstd.SpecimanNo ,CAST(patmstd.BarCodeImage AS VARBINARY(8000)) as BarCodeImage,patmstd.PheboAcceptBy,replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) as DailyseqNo " +
                      "  FROM         patmstd INNER JOIN " +
                      "  MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN " +
                      "  patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID " +
                     " where (patmst.IsActive = 1) and patmstd.TestDeActive=0  and  dbo.patmstd.PID='" + PID + "'  and patmstd.SDCode in (" + TestCode + ")", con);
            }
            else
            {
                cmd = new SqlCommand(" alter view VW_GetBarcode as SELECT DISTINCT  "+
                      "  MainTest.Maintestname AS TestName, MainTest.Sampletype, patmstd.Patmstid, patmstd.PatRegID, patmstd.FID, patmstd.MTCode, patmstd.SDCode, "+
                      "  patmstd.PackageCode, patmstd.PID, patmstd.UnitCode, case when isnull(patmstd.PhlebotomistRejectremark,'') <>'' then patmstd.BarcodeID+'-'+'1' else patmstd.BarcodeID end as BarcodeID, patmstd.DigModule, patmstd.Patregdate, patmstd.TestRate, patmstd.Email, " +
                      "  patmstd.Dramt, patmstd.cons, patmstd.MainRate, patmstd.Branchid, patmstd.Createdby, patmstd.Createdon, patmstd.Updatedby, patmstd.Updatedon, "+
                      "  patmstd.IsActive, patmstd.Testdisc, patmstd.Testrecamt, patmstd.PatientFlag, patmst.RefDr, patmstd.IspheboAccept,  "+
                      "  patmst.intial + ' ' + patmst.Patname AS Patient_Name, patmst.CenterName, patmst.Age, patmst.MDY, patmst.sex, patmstd.Labno, patmst.LabRegMediPro, MainTest.Shortcode ,patmstd.SpecimanNo,CAST(patmstd.BarCodeImage AS VARBINARY(8000)) as BarCodeImage,patmstd.PheboAcceptBy ,replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) as DailyseqNo " +
                      "  FROM         patmstd INNER JOIN "+
                      "  MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN "+
                      "  patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID " +
                     " where (patmst.IsActive = 1) and patmstd.TestDeActive=0  and  dbo.patmstd.PID='" + PID + "' ", con);
            }
            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }

 
    public void AlterViewPrintBarcode(int branchid, string PatRegID, string FID, string BarcodeID, int PID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" alter view VW_patstkvw as SELECT  distinct BarcodeID,BarCodeImage,Sampletype ,Patient_Name,Centername,PatRegID,FID,Age,Sex,PID,MDY,''as scodes,branchid,RefDr,  ISNULL( Labno,0)as Labno ,LabRegMediPro,SpecimanNo, PackageCode,PheboAcceptBy,DailyseqNo,STCode = STUFF(    (SELECT ',' + Shortcode    FROM VW_GetBarcode t1   " +
                   " WHERE t1.barcodeid = t2.barcodeid  and t1.Sampletype = t2.Sampletype   FOR XML PATH (''))   , 1, 1, '') , " +

                   " testnames = STUFF(    (SELECT ',' + testname    FROM VW_GetBarcode t3   "+
                   " WHERE t3.barcodeid = t2.barcodeid and t3.Sampletype = t2.Sampletype   FOR XML PATH (''))   , 1, 1, '') " +
                   " from VW_GetBarcode t2  "+
                   " group by barcodeid,BarCodeImage,PID,Sampletype,Patient_Name,Centername ,PatRegID,FID,Age,sex,PID,MDY,branchid,RefDr, Labno,LabRegMediPro,PackageCode,PheboAcceptBy,SpecimanNo,DailyseqNo   having BarcodeID in (" + BarcodeID + ") ", con);

            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }

    public void AlterViewPrintBarcode_Direct(int branchid, string PatRegID, string FID, string BarcodeID, int PID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" alter view VW_patstkvw as SELECT  distinct BarcodeID,BarCodeImage,Sampletype ,Patient_Name,Centername,PatRegID,FID,Age,Sex,PID,MDY,''as scodes,branchid,RefDr,  ISNULL( Labno,0)as Labno ,LabRegMediPro,SpecimanNo, PackageCode,PheboAcceptBy,DailyseqNo,STCode = STUFF(    (SELECT ',' + Shortcode    FROM VW_GetBarcode t1   " +
                   " WHERE t1.barcodeid = t2.barcodeid  and t1.Sampletype = t2.Sampletype   FOR XML PATH (''))   , 1, 1, '') , " +

                   " testnames = STUFF(    (SELECT ',' + testname    FROM VW_GetBarcode t3   " +
                   " WHERE t3.barcodeid = t2.barcodeid and t3.Sampletype = t2.Sampletype   FOR XML PATH (''))   , 1, 1, '') " +
                   " from VW_GetBarcode t2  " +
                   " group by barcodeid,BarCodeImage,PID,Sampletype,Patient_Name,Centername ,PatRegID,FID,Age,sex,PID,MDY,branchid,RefDr, Labno,LabRegMediPro,PackageCode,PheboAcceptBy,SpecimanNo,DailyseqNo   having PID = (" + PID + ") ", con);

            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }




    public bool Check_existstestresult(string PatRegID, string FID, string MTCode,string STCODE,int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select ResultTemplate from ResMst where PatRegID='" + PatRegID + "' and MTCode='" + MTCode + "' and  STCODE='" + STCODE + "' and FID='" + FID + "' and branchid=" + branchid + " and ResultTemplate<>'' ", conn);
        SqlDataReader sdr = null;
        try
        {
            conn.Close();
            conn.Open();
            sdr = sc.ExecuteReader();

         
            while(sdr.Read())
            {
                _CheckTestResult = Convert.ToString(sdr[0]);
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
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        if (_CheckTestResult == "" || _CheckTestResult==null)
        {
          //  return false;
            return true;
        }
        else
        {
            return true;
         
        }
    }

    
    public void AlterView_Barcode_Temp_Deptwise(string TestCode, int PID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            if (TestCode != "")
            {
                cmd = new SqlCommand(" alter view VW_GetBarcode_Deptwise as SELECT DISTINCT  "+
                  "  MainTest.Maintestname AS TestName, MainTest.Sampletype, patmstd.Patmstid, patmstd.PatRegID, patmstd.FID, patmstd.MTCode, patmstd.SDCode, "+
                  "  patmstd.PackageCode, patmstd.PID, patmstd.UnitCode, case when isnull(patmstd.PhlebotomistRejectremark,'') <>'' then patmstd.BarcodeID+'-'+'1' else patmstd.BarcodeID end as BarcodeID, patmstd.DigModule, patmstd.Patregdate, patmstd.TestRate, patmstd.Email, " +
                  "  patmstd.dramt, patmstd.cons, patmstd.MainRate, patmstd.Branchid, patmstd.Createdby, patmstd.Createdon, "+
                  "  patmstd.Updatedby, patmstd.Updatedon, patmstd.IsActive, patmstd.Testdisc, patmstd.Testrecamt, patmstd.PatientFlag, patmst.RefDr, patmstd.IspheboAccept, "+
                  "  patmst.intial + ' ' + patmst.Patname AS Patient_Name, patmst.CenterName, patmst.Age, patmst.MDY, patmst.sex, patmstd.Labno, patmst.LabRegMediPro, "+
                  "  SubDepartment.subdeptName,patmstd.SpecimanNo " +
                  "  FROM         patmstd INNER JOIN "+
                  "  MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN  "+
                  "  patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID  INNER JOIN "+
                  "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode " +
                     " where   dbo.patmstd.PID='" + PID + "'  and patmstd.SDCode in (" + TestCode + ")", con);
            }
            else
            {
                cmd = new SqlCommand(" alter view VW_GetBarcode_Deptwise as SELECT DISTINCT  "+
                  "  MainTest.Maintestname AS TestName, MainTest.Sampletype, patmstd.Patmstid, patmstd.PatRegID, patmstd.FID, patmstd.MTCode, patmstd.SDCode, "+
                  "  patmstd.PackageCode, patmstd.PID, patmstd.UnitCode, case when isnull(patmstd.PhlebotomistRejectremark,'') <>'' then patmstd.BarcodeID+'-'+'1' else patmstd.BarcodeID end as BarcodeID, patmstd.DigModule, patmstd.Patregdate, patmstd.TestRate, patmstd.Email, " +
                  "  patmstd.dramt, patmstd.cons, patmstd.MainRate, patmstd.Branchid, patmstd.Createdby, patmstd.Createdon, "+
                  "  patmstd.Updatedby, patmstd.Updatedon, patmstd.IsActive, patmstd.Testdisc, patmstd.Testrecamt, patmstd.PatientFlag, patmst.RefDr, patmstd.IspheboAccept, "+
                  "  patmst.intial + ' ' + patmst.Patname AS Patient_Name, patmst.CenterName, patmst.Age, patmst.MDY, patmst.sex, patmstd.Labno, patmst.LabRegMediPro, "+
                  "  SubDepartment.subdeptName ,patmstd.SpecimanNo " +
                  "  FROM         patmstd INNER JOIN "+
                  "  MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN "+
                  "  patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID  INNER JOIN "+
                  "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode " +
                     " where   dbo.patmstd.PID='" + PID + "'  ", con);
            }
            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }
    public void AlterViewPrintBarcode_deptwise_Registration(int branchid, string PatRegID, string FID, string BarcodeID, int PID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" alter view VW_patstkvw_Deptwise as SELECT  distinct BarcodeID,Sampletype ,Patient_Name,Centername,PatRegID,FID,Age,Sex,PID,MDY,''as scodes,branchid,RefDr, " +
               "  ISNULL( Labno,0)as Labno ,LabRegMediPro, SpecimanNo,''as PackageCode,subdeptName, " +
               "  STCode = STUFF(    (SELECT ',' + Mtcode    FROM VW_GetBarcode_Deptwise t1   " +
               "  WHERE t1.barcodeid = t2.barcodeid  and t1.Sampletype = t2.Sampletype and t1.subdeptName=t2.subdeptName " +
               "  FOR XML PATH (''))   , 1, 1, '') ,  testnames = STUFF(    (SELECT ',' + testname    FROM VW_GetBarcode_Deptwise t3  " +
               "  WHERE t3.barcodeid = t2.barcodeid and t3.Sampletype = t2.Sampletype and t3.subdeptName=t2.subdeptName  " +
               "  FOR XML PATH (''))   , 1, 1, '')  from VW_GetBarcode_Deptwise t2  " +
               "  group by barcodeid,PID,Sampletype,Patient_Name,Centername ,PatRegID,FID,Age,sex,PID,MDY,branchid,RefDr, Labno,LabRegMediPro, " +
               "  SpecimanNo ,subdeptName   having PID ='" + PID + "'  ", con);

            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }

    public void AlterViewPrintBarcode_deptwise(int branchid, string PatRegID, string FID, string BarcodeID, int PID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
                       cmd = new SqlCommand(" alter view VW_patstkvw_Deptwise as SELECT  distinct BarcodeID,Sampletype ,Patient_Name,Centername,PatRegID,FID,Age,Sex,PID,MDY,''as scodes,branchid,RefDr, "+
                          "  ISNULL( Labno,0)as Labno ,LabRegMediPro,''as PackageCode,subdeptName,SpecimanNo, " +
                          "  STCode = STUFF(    (SELECT ',' + Mtcode    FROM VW_GetBarcode_Deptwise t1   "+
                          "  WHERE t1.barcodeid = t2.barcodeid  and t1.Sampletype = t2.Sampletype and t1.subdeptName=t2.subdeptName "+
                          "  FOR XML PATH (''))   , 1, 1, '') ,  testnames = STUFF(    (SELECT ',' + testname    FROM VW_GetBarcode_Deptwise t3  "+
                          "  WHERE t3.barcodeid = t2.barcodeid and t3.Sampletype = t2.Sampletype and t3.subdeptName=t2.subdeptName  "+
                          "  FOR XML PATH (''))   , 1, 1, '')  from VW_GetBarcode_Deptwise t2  "+
                          "  group by barcodeid,PID,Sampletype,Patient_Name,Centername ,PatRegID,FID,Age,sex,PID,MDY,branchid,RefDr, Labno,LabRegMediPro, "+
                          "  subdeptName,SpecimanNo   having BarcodeID in (" + BarcodeID + ") ", con);

            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }

    public DataTable Check_Authorised_Test(string PatRegID, string MTCode, int Branchid, string FID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   AunticateSignatureId,isnull(Technicanby,'') as Technicanby,isnull(Printedby,'') as Printedby from Patmstd where  PatRegID ='" + PatRegID + "' and MTCode='" + MTCode + "' and branchid='" + Branchid + "'  and FID='" + FID + "' ", conn);
        DataTable ds = new DataTable();

        try
        {
            sc.Fill(ds);
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
        return ds;
    }

    public bool Insert_utd_Table(int branchid, string TestCode, string UserName, string TestName, string PatRegID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();

        sc.CommandText = "Insert into utd_Table(RegNo,TestCode,TestName,UserName) values(@PatRegID,@TestCode,@TestName,@UserName)";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = (PatRegID);

        sc.Parameters.Add(new SqlParameter("@TestCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(TestCode);
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 50)).Value = Convert.ToString(UserName);

        sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 50)).Value = Convert.ToString(TestName);
       
        SqlDataReader sdr = null;

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
                if (sdr != null) sdr.Close();
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

    public bool Insert_utd_Table_status(int branchid, string PatRegID, string TestCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "update utd_Table set IsDeleted=@IsDeleted  where TestCode=@TestCode and PatRegID=@PatRegID and TestCode=@TestCode ";
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = (PatRegID);
       
        sc.Parameters.Add(new SqlParameter("@TestCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(TestCode);
        sc.Parameters.Add(new SqlParameter("@IsDeleted", SqlDbType.Bit)).Value = Convert.ToBoolean(0);
       
        SqlDataReader sdr = null;
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

        return true;
    }

    public bool Check_PrintedReport(string PatRegID, string FID, string MTCode, int branchid)
    {
        bool Reportprint=false;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select Patrepstatus from Patmstd where PatRegID='" + PatRegID + "' and MTCode='" + MTCode + "'  and branchid=" + branchid + "  and FID =" + FID + "  ", conn);
        SqlDataReader sdr = null;
        try
        {
            conn.Close();
            conn.Open();
            sdr = sc.ExecuteReader();

           
            while (sdr.Read())
            {
                Reportprint = Convert.ToBoolean(sdr[0]);
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
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        if (Reportprint ==true)
        {
            //  return false;
            return true;
        }
        else
        {
            return false;

        }
    }

    public bool Delete_Patst_Code(string PatRegID, string FID, string MTCode, int branchid)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand("delete from Patmstd " +
            "where PatRegID=@PatRegID and FID=@FID and branchid=@branchid and MTCode=@MTCode ", conn);
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
        sc.Parameters.AddWithValue("@MTCode", MTCode);
        sc.Parameters.AddWithValue("@branchid", branchid);
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

    public bool InsertUpdateDESC_Nondesc(string MTcode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[SP_UpdateDEsc_NonDesc]";

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (MTcode);

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

   

    public DataTable Check_Test_Status(string PatRegID, string MTCode, int Branchid,string FID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   Patauthicante from Patmstd where  PatRegID ='" + PatRegID + "' and MTCode='" + MTCode + "' and branchid='" + Branchid + "' and FID='" + FID + "' and Patauthicante<>'Registered' ", conn);
        DataTable ds = new DataTable();

        try
        {
            sc.Fill(ds);
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
        return ds;
    }

    public bool InsertUpdate_AddResult_outsource1(int branchid, int Flag)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[SP_phdataupdatefrm_outsource1 ]";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (this.PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.FID);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.MTCode);

        sc.Parameters.Add(new SqlParameter("@Patrepstatus", SqlDbType.Bit)).Value = Convert.ToBoolean(this.Patrepstatus);
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.STCODE);

        sc.Parameters.Add(new SqlParameter("@PPID", SqlDbType.Int)).Value = Convert.ToInt32(this.PPID);
        if (this.Patauthicante == null)
            sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.Patauthicante);

        if (this.TestUser == null)
            sc.Parameters.Add(new SqlParameter("@TestUser", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TestUser", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.TestUser);

        if (this.Testdate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@TestedDate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@TestedDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.Testdate);

        if (this.ReportDate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@ReportDate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@ReportDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.ReportDate);

        if (this.AunticateSignatureId == null)
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@AunticateSignatureId", SqlDbType.Int)).Value = this.AunticateSignatureId;

        if (this.Technicianid == null)
            sc.Parameters.Add(new SqlParameter("@LabTechnicianid", SqlDbType.Int)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@LabTechnicianid", SqlDbType.Int)).Value = this.Technicianid;


        if (this.ResultTemplate != null)
            sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.ResultTemplate;
        else
            sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = DBNull.Value;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@ReportRemark", SqlDbType.NVarChar, 500)).Value = P_ReportRemark;
        sc.Parameters.Add(new SqlParameter("@Technicanby", SqlDbType.NVarChar, 50)).Value = P_Technicanby;
        //  sc.Parameters.Add(new SqlParameter("@PanicResult", SqlDbType.Int)).Value = P_PanicResult;
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
    public bool InsertUpdate_AddResult_outsource_1(int branchid, int Flag)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[SP_phdataupdatefrm_outsource1 ]";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (this.PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.FID);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.MTCode);      
       // sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.STCODE);        
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
       
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


    public DataTable Check_OutsourcePatientPID(string PatRegID, string MTCode, int Branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   OutsourcePatientPID from Patmstd where  PatRegID ='" + PatRegID + "' and MTCode='" + MTCode + "' and branchid='" + Branchid + "'  ", conn);
        DataTable ds = new DataTable();

        try
        {
            sc.Fill(ds);
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
        return ds;
    }

    public bool Update_isRefund(int branchid, string PID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();
        sc.CommandText = "update RecM set IsRefund=0  where PID=@PID ";
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = (PID);

      
        SqlDataReader sdr = null;
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

        return true;
    }


    public void AlterView_Barcode_Temp_Deptwise_Clicksample(string TestCode, int PID, string CDate)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            if (TestCode != "")
            {
                cmd = new SqlCommand(" alter view VW_GetBarcode_Deptwise as SELECT DISTINCT  " +
                  "  MainTest.Maintestname AS TestName, MainTest.Sampletype, patmstd.Patmstid, patmstd.PatRegID, patmstd.FID, patmstd.MTCode, patmstd.SDCode, " +
                  "  patmstd.PackageCode, patmstd.PID, patmstd.UnitCode, case when isnull(patmstd.PhlebotomistRejectremark,'') <>'' then patmstd.BarcodeID+'-'+'1' else patmstd.BarcodeID end as BarcodeID, patmstd.DigModule, patmstd.Patregdate, patmstd.TestRate, patmstd.Email, " +
                  "  patmstd.dramt, patmstd.cons, patmstd.MainRate, patmstd.Branchid, patmstd.Createdby, patmstd.Createdon, " +
                  "  patmstd.Updatedby, patmstd.Updatedon, patmstd.IsActive, patmstd.Testdisc, patmstd.Testrecamt, patmstd.PatientFlag, patmst.RefDr, patmstd.IspheboAccept, " +
                  "   patmst.Patname AS Patient_Name, patmst.CenterName, patmst.Age, patmst.MDY, patmst.sex, patmstd.Labno, patmst.LabRegMediPro, " +
                  "  SubDepartment.subdeptName,patmstd.SpecimanNo ,CAST(patmstd.BarCodeImage AS VARBINARY(8000)) as BarCodeImage " +
                  "  FROM         patmstd INNER JOIN " +
                  "  MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN  " +
                  "  patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID  INNER JOIN " +
                  "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode " +
                     " where    (CAST(CAST(YEAR(patmstd.Patregdate) AS varchar(4)) + '/' + CAST(MONTH(patmstd.Patregdate) AS varchar(2)) + '/' + CAST(DAY(patmstd.Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(CDate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(CDate).ToString("MM/dd/yyyy") + "') and patmstd.SDCode in (" + TestCode + ")", con); // dbo.patmstd.PID='" + PID + "'  and
            }
            else
            {
                cmd = new SqlCommand(" alter view VW_GetBarcode_Deptwise as SELECT DISTINCT  " +
                  "  MainTest.Maintestname AS TestName, MainTest.Sampletype, patmstd.Patmstid, patmstd.PatRegID, patmstd.FID, patmstd.MTCode, patmstd.SDCode, " +
                  "  patmstd.PackageCode, patmstd.PID, patmstd.UnitCode, case when isnull(patmstd.PhlebotomistRejectremark,'') <>'' then patmstd.BarcodeID+'-'+'1' else patmstd.BarcodeID end as BarcodeID, patmstd.DigModule, patmstd.Patregdate, patmstd.TestRate, patmstd.Email, " +
                  "  patmstd.dramt, patmstd.cons, patmstd.MainRate, patmstd.Branchid, patmstd.Createdby, patmstd.Createdon, " +
                  "  patmstd.Updatedby, patmstd.Updatedon, patmstd.IsActive, patmstd.Testdisc, patmstd.Testrecamt, patmstd.PatientFlag, patmst.RefDr, patmstd.IspheboAccept, " +
                  "   patmst.Patname AS Patient_Name, patmst.CenterName, patmst.Age, patmst.MDY, patmst.sex, patmstd.Labno, patmst.LabRegMediPro, " +
                  "  SubDepartment.subdeptName ,patmstd.SpecimanNo ,CAST(patmstd.BarCodeImage AS VARBINARY(8000)) as BarCodeImage " +
                  "  FROM         patmstd INNER JOIN " +
                  "  MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN " +
                  "  patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID  INNER JOIN " +
                  "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode " +
                     " where (CAST(CAST(YEAR(patmstd.Patregdate) AS varchar(4)) + '/' + CAST(MONTH(patmstd.Patregdate) AS varchar(2)) + '/' + CAST(DAY(patmstd.Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(CDate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(CDate).ToString("MM/dd/yyyy") + "')   ", con);//  dbo.patmstd.PID='" + PID + "'
            }
            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }



    public void AlterViewPrintBarcode_deptwise_Registration_SampleClick(int branchid, string PatRegID, string FID, string BarcodeID, int PID, string CDate)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        try
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(" alter view VW_patstkvw_Deptwise as SELECT  distinct BarcodeID,BarCodeImage,Sampletype ,Patient_Name,Centername,PatRegID,FID,Age,Sex,PID,MDY,''as scodes,branchid,RefDr, " +
               "  ISNULL( Labno,0)as Labno ,LabRegMediPro, SpecimanNo,''as PackageCode,subdeptName,Patregdate,CreatedBy,  " +
               "  STCode = STUFF(    (SELECT ',' + Mtcode    FROM VW_GetBarcode_Deptwise t1   " +
               "  WHERE t1.barcodeid = t2.barcodeid  and t1.Sampletype = t2.Sampletype and t1.subdeptName=t2.subdeptName " +
               "  FOR XML PATH (''))   , 1, 1, '') ,  testnames = STUFF(    (SELECT ',' + testname    FROM VW_GetBarcode_Deptwise t3  " +
               "  WHERE t3.barcodeid = t2.barcodeid and t3.Sampletype = t2.Sampletype and t3.subdeptName=t2.subdeptName  " +
               "  FOR XML PATH (''))   , 1, 1, '')  from VW_GetBarcode_Deptwise t2  " +
               "  group by barcodeid,BarCodeImage,PID,Sampletype,Patient_Name,Centername ,PatRegID,FID,Age,sex,PID,MDY,branchid,RefDr, Labno,LabRegMediPro, " +
               "  SpecimanNo ,subdeptName,Patregdate ,CreatedBy  having (CAST(CAST(YEAR(Patregdate) AS varchar(4)) + '/' + CAST(MONTH(Patregdate) AS varchar(2)) + '/' + CAST(DAY(Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(CDate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(CDate).ToString("MM/dd/yyyy") + "')  ", con); //PID ='" + PID + "' 

            //con.Open();
            cmd.ExecuteNonQuery();
        }
        catch (Exception exc) { }
        finally { con.Close(); con.Dispose(); }
    }

    public bool UpdatePrintstatus_What_app(string PatRegID, string FID, string MTCode, int branchid, string Printedby)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("UPDATE Patmstd SET Patrepstatus=1,WhatAppReport=1,PatRepDate=@PatRepDate,WhatAppRepBy='" + Printedby + "' ,Printedby='" + Printedby + "' WHERE PatRegID= '" + PatRegID + "' and FID= '" + FID.Trim() + "' and MTCode='" + MTCode + "' and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PatRepDate", SqlDbType.DateTime)).Value = Date.getdate();
        try
        {
            conn.Close();
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
                // Log an event in the Application Event Log.
                throw;
            }
        }

        return true;
    }

    public DataTable Check_Test_Status(string PatRegID, string MTCode, int Branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        // SqlCommand sc = new SqlCommand("select Patrepstatus from Patmstd where PatRegID='" + PatRegID + "' and MTCode='" + MTCode + "'  and branchid=" + branchid + "  ", conn);
        SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   Patauthicante from Patmstd where  PatRegID ='" + PatRegID + "' and MTCode='" + MTCode + "' and branchid='" + Branchid + "'  ", conn);
        DataTable ds = new DataTable();

        try
        {
            sc.Fill(ds);
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
        return ds;
    }

    public DataTable GetallTechnican(string username, string Usertype)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   DRST.signatureid, DRST.Drsignature, CTuser.username, CTuser.DRid, CTuser.Usertype FROM CTuser INNER JOIN DRST ON CTuser.Drid = DRST.signatureid INNER JOIN Deptwiseuser ON CTuser.username = Deptwiseuser.username INNER JOIN SubDepartment ON Deptwiseuser.DeptCodeID = SubDepartment.subdeptid where CTuser.Usertype='Technician' and CTuser.username='" + username + "' and CTuser.Usertype='" + Usertype + "' ", conn);
        DataTable ds = new DataTable();

        try
        {
            sc.Fill(ds);
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
        return ds;
    }

    public bool InsertUpdate_QCStatus(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[Usp_UpdateQcStatus]";

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (this.PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.FID);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.MTCode);
        // sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = Convert.ToString(this.STCODE);        
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;

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

    #region properties

    private string _statusAudit;
    public string statusAudit
    {
        get { return _statusAudit; }
        set { _statusAudit = value; }
    }

    private string _ResultTemplate;
    public string ResultTemplate
    {
        get
        {
            return _ResultTemplate;
        }
        set
        {
            _ResultTemplate = value;
        }
    }

    private string _MTCode;
    public string MTCode
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

    private string _SDCode;
    public string SDCode
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

    private string reason;
    public string Reason
    {
        get
        {
            return reason;
        }
        set
        {
            reason = value;
        }
    }

    private string _FID;
    public string FID
    {
        get
        {
            return _FID;
        }
        set
        {
            _FID = value;
        }
    }

    private string _PatRegID;
    public string PatRegID
    {
        get
        {
            return _PatRegID;
        }
        set
        {
            _PatRegID = value;
        }
    }

    private bool _Patrepstatus;
    public bool Patrepstatus
    {
        get
        {
            return _Patrepstatus;
        }
        set
        {
            _Patrepstatus = value;
        }
    }
    private DateTime _AbandantOn;
    public DateTime AbandantOn
    {
        get { return _AbandantOn; }
        set { _AbandantOn = value; }
    }
    private string _AbandantBy;
    public string AbandantBy
    {
        get
        {
            return _AbandantBy;
        }
        set
        {
            _AbandantBy = value;
        }
    }
    private string ReportRemark;
    public string P_ReportRemark
    {
        get { return ReportRemark; }
        set { ReportRemark = value; }
    }
    private string Technicanby;
    public string P_Technicanby
    {
        get { return Technicanby; }
        set { Technicanby = value; }
    }
    private string DescImagePath;
    public string P_DescImagePath
    {
        get { return DescImagePath; }
        set { DescImagePath = value; }
    }
    private string EntryUsername;
    public string P_EntryUsername
    {
        get { return EntryUsername; }
        set { EntryUsername = value; }
    }
     private int PanicResult;
     public int P_PanicResult
    {
        get { return PanicResult; }
        set { PanicResult = value; }
    }
       //PanicResult = 1;
  

    private string sTCODE;
    public string STCODE
    {
        get { return sTCODE; }
        set { sTCODE = value; }
    }

    private string testname;
    public string P_testname
    {
        get { return testname; }
        set { testname = value; }
    }

   

    private int _PPID;
    public int PPID
    {
        get { return _PPID; }
        set { _PPID = value; }
    }

    private DateTime _Patregdate;
    public DateTime Patregdate
    {
        get { return _Patregdate; }
        set { _Patregdate = value; }
    }

    private string _RegisterUser;
    public string RegisterUser
    {
        get { return _RegisterUser; }
        set { _RegisterUser = value; }
    }

    private string _Patauthicante;
    public string Patauthicante
    {
        get { return _Patauthicante; }
        set { _Patauthicante = value; }
    }
    private string _UploadOutSourceReport;
    public string UploadOutSourceReport
    {
        get { return _UploadOutSourceReport; }
        set { _UploadOutSourceReport = value; }
    }

    private string _testUser;
    public string TestUser
    {
        get { return _testUser; }
        set { _testUser = value; }
    }

    private string _TempTestUser;
    public string TempTestUser
    {
        get { return _TempTestUser; }
        set { _TempTestUser = value; }
    }

    private DateTime _testdate;
    public DateTime Testdate
    {
        get { return _testdate; }
        set { _testdate = value; }
    }

  
    private int _AunticateSignatureId;
    public int AunticateSignatureId
    {
        get { return _AunticateSignatureId; }
        set { _AunticateSignatureId = value; }
    }
    private int technicianid;
    public int Technicianid
    {
        get { return technicianid; }
        set { technicianid = value; }
    }
    //private int technicianid;
    //public int Technicianid
    //{
    //    get { return technicianid; }
    //    set { technicianid = value; }
    //}
    private int _TechnicanSecond;
    public int TechnicanSecond
    {
        get { return _TechnicanSecond; }
        set { _TechnicanSecond = value; }
    }
    private int _TechnicanFirst;
    public int TechnicanFirst
    {
        get { return _TechnicanFirst; }
        set { _TechnicanFirst = value; }
    }
    private string _PackageCode;
    public string PackageCode
    {
        get { return _PackageCode; }
        set { _PackageCode = value; }
    }

    private DateTime _reportdate;
    public DateTime ReportDate
    {
        get { return _reportdate; }
        set { _reportdate = value; }
    }

    private DateTime _SampleDate;
    public DateTime SampleDate
    {
        get { return _SampleDate; }
        set { _SampleDate = value; }
    }
 
    private int _PID;
    public int PID
    {
        get { return _PID; }
        set { _PID = value; }
    }

    private string _CheckTestResult;
    public string CheckTestResult
    {
        get { return _CheckTestResult; }
        set { _CheckTestResult = value; }
    }
    #endregion

}
