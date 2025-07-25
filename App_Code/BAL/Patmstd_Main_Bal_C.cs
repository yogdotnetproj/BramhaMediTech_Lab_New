using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
public class Patmstd_Main_Bal_C
{



    public DataTable Getall_CalculateserviceAmt( int Pid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter("select * from patmstd where Pid=" + Pid + " ", conn);
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

   

    public string GetBCCode(int PID, string MTCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[sp_getBCCode]";

        
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 250)).Value = Convert.ToString(MTCode);
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        object BCCode;
        try
        {
            sc.CommandTimeout = 5000;
            conn.Open();
            BCCode = sc.ExecuteScalar();
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
        return Convert.ToString( BCCode);
    }

    public DataTable GetallMaindoctor(string maindept)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   DRST.signatureid, DRST.Drsignature FROM CTuser INNER JOIN DRST ON CTuser.Drid = DRST.signatureid INNER JOIN Deptwiseuser ON CTuser.username = Deptwiseuser.username INNER JOIN SubDepartment ON Deptwiseuser.DeptCodeID = SubDepartment.DeptID where CTuser.maindeptid='" + maindept + "'  ", conn);
        DataTable  ds = new DataTable();

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

    public DataTable GetallMaindoctor_addresult_Doctor(string maindept, string Subdeptid,string DocName)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   DRST.signatureid, DRST.Drsignature FROM CTuser INNER JOIN DRST ON CTuser.Drid = DRST.signatureid INNER JOIN Deptwiseuser ON CTuser.username = Deptwiseuser.username INNER JOIN SubDepartment ON Deptwiseuser.DeptCodeID = SubDepartment.subdeptid where (CTuser.Usertype='Main Doctor' or CTuser.Usertype='MainDoctor')  and subdeptid='" + Subdeptid + "'  and CTuser.username='" + DocName + "' ", conn);//and CTuser.DigModule='" + maindept + "'
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


    public DataTable GetallMaindoctor_addresult(string maindept, string Subdeptid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   DRST.signatureid, DRST.Drsignature FROM CTuser INNER JOIN DRST ON CTuser.Drid = DRST.signatureid INNER JOIN Deptwiseuser ON CTuser.username = Deptwiseuser.username INNER JOIN SubDepartment ON Deptwiseuser.DeptCodeID = SubDepartment.subdeptid where (CTuser.Usertype='Main Doctor' or CTuser.Usertype='MainDoctor') and  subdeptid='" + Subdeptid + "' ", conn);//CTuser.DigModule='" + maindept + "' and
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
    public DataTable GetallMaindoctor_addresult_Technican(string maindept, string Subdeptid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   DRST.signatureid, DRST.Drsignature FROM CTuser INNER JOIN DRST ON CTuser.Drid = DRST.signatureid INNER JOIN Deptwiseuser ON CTuser.username = Deptwiseuser.username INNER JOIN SubDepartment ON Deptwiseuser.DeptCodeID = SubDepartment.subdeptid where CTuser.Usertype='Technician' and CTuser.DigModule='" + maindept + "' and subdeptid='" + Subdeptid + "' ", conn);
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

    public DataTable GetallResult_ShortForm(string MTCode, string ParaCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter("select distinct cast( Description as nvarchar(4000)) as Description FROM stformmst WHERE MTCode='" + MTCode + "' and ParaCode='" + ParaCode + "' ", conn);
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

  
    public static void UpdateStatusByLab_directresult(int PID, string PatRegID, string FID, int branchid, string BarcodeID, string MTCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" UPDATE patmstd SET  PatRegID=@PatRegID, FID=@FID ,BarcodeID=@BarcodeID " +
            " where PID=" + PID + " and branchid=" + branchid + " and MTCode='" + MTCode + "' ", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
        sc.Parameters.Add(new SqlParameter("@BarcodeID", SqlDbType.NVarChar, 250)).Value = BarcodeID;

        short cnt;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }
  
    public static void UpdateStatusByLab_Registerno(int PID, string PatRegID, string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" UPDATE patmstd SET  PatRegID=@PatRegID, FID=@FID  " +
            " where PID=" + PID + "and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
      
        short cnt;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }
   

   
   

    public static ICollection getRecords(int PID, string BarcodeID, int branchid)
    {
        
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandText = "select * from patmstd where PID=@PID and branchid=" + branchid + "";

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            while (sdr != null && sdr.Read())
            {
                Patmstd_Bal_C PB_C = new Patmstd_Bal_C();
                PB_C.PatRegID = (sdr["PatRegID"].ToString());
                PB_C.FID = sdr["FID"].ToString();
                PB_C.MTCode = sdr["MTCode"].ToString();
                PB_C.SDCode = sdr["SDCode"].ToString();
                if (sdr["TestRate"] != DBNull.Value)
                    PB_C.TestRate = Convert.ToSingle(sdr["TestRate"]);

                if (!(sdr["Patregdate"] is DBNull))
                    PB_C.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                if (!(sdr["PackageCode"] is DBNull))
                    PB_C.PackageCode = sdr["PackageCode"].ToString();

                al.Add(PB_C);
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }

        return al;
    }
    public static void UpdateStatusByLab(string BarcodeID, string PatRegID, string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" UPDATE patmstd SET  PatRegID=@PatRegID, FID=@FID" +
            " where BarcodeID=@BarcodeID and branchid="+branchid+"", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
        sc.Parameters.Add(new SqlParameter("@BarcodeID", SqlDbType.NVarChar, 50)).Value = BarcodeID;

        short cnt;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }
    public static void UpdateStatusByLab(int PID, string PatRegID, string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" UPDATE patmstd SET  PatRegID=@PatRegID, FID=@FID" +
            " where PID=" + PID + "and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
        
        short cnt;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }
   

  
   
    public static bool IsexistsforPidCodeexists(int PID, string MTCode, int branchid)
    {
      
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        if (MTCode.Trim().Length != 4)
        {
            sc.CommandText = "SELECT count(*) from patmstd where PID=@PID and MTCode=@MTCode";
        }
        else
        {
            sc.CommandText = "SELECT count(*) from patmstd where PID=@PID and PackageCode=@MTCode";
        }

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

        int cnt = 0;

        try
        {
            conn.Open();
            cnt = Convert.ToInt32(sc.ExecuteScalar());

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
            catch
            {
                throw new Exception("Record not found");
            }
        }
        if (cnt != 0)
            return true;
        else
            return false;
    }

   
    public static ICollection Get_Barcode_Byid(int PID, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandText = "Select * from patmstd where PID=" + PID + "";
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Patmstd_Bal_C PB_C = new Patmstd_Bal_C();
                    PB_C.PatRegID = (sdr["PatRegID"].ToString());
                    PB_C.FID = (string)sdr["FID"];
                    PB_C.MTCode = (string)sdr["MTCode"];
                    PB_C.SDCode = (string)sdr["SDCode"];
                    if (sdr["PackageCode"] != DBNull.Value)
                        PB_C.PackageCode = Convert.ToString(sdr["PackageCode"]);

                    PB_C.Barcodeid = (string)sdr["BarcodeID"];
                    al.Add(PB_C);
                }
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }

        return al;
    }

    public static string Get_Labcode_by_ID(int PID, string MTCode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" select UnitCode from patmstd where PID=" + PID + " and MTCode='" + MTCode + "'", conn);
        string labcode = "";

        try
        {
            conn.Open();
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                labcode = "";
            else
                labcode = Convert.ToString(o);
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
        return labcode;
    }

  
    public static void UpdatstatusbyPatmstd(string MTCode, string PatRegID, string FID, int PID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" UPDATE patmstd SET  PatRegID=@PatRegID, FID=@FID" +
            " where MTCode='"+ MTCode +"' and PID="+ PID +"", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
       
        // Add the employee ID parameter and set its value.
        short cnt;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }

   
    public static DataSet Get_DoctorSignature(string username)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter sc = new SqlDataAdapter("SELECT dbo.DRST.signid, dbo.SubDepartment.SDCode FROM dbo.CTuser INNER JOIN dbo.DRST ON dbo.CTuser.signid = dbo.DRST.signid INNER JOIN dbo.Deptwiseuser ON dbo.CTuser.username = dbo.Deptwiseuser.username INNER JOIN dbo.SubDepartment ON dbo.Deptwiseuser.DeptCodeID = dbo.SubDepartment.DeptID where CTuser.username='" + username + "'", conn);
        DataSet ds = new DataSet();

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

    public static ICollection getRecords(string BarcodeID, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandText = "select * from patmstd where BarcodeID=@BarcodeID and branchid=" + branchid + "";

        sc.Parameters.Add(new SqlParameter("@BarcodeID", SqlDbType.NVarChar, 50)).Value = BarcodeID;
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            while (sdr != null && sdr.Read())
            {
                Patmstd_Bal_C PB_C = new Patmstd_Bal_C();
                PB_C.PatRegID = (sdr["PatRegID"].ToString());
                PB_C.FID = sdr["FID"].ToString();
                PB_C.MTCode = sdr["MTCode"].ToString();
                PB_C.SDCode = sdr["SDCode"].ToString();
                if (sdr["TestRate"] != DBNull.Value)
                    PB_C.TestRate = Convert.ToSingle(sdr["TestRate"]);

                if (!(sdr["Patregdate"] is DBNull))
                    PB_C.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                if (!(sdr["PackageCode"] is DBNull))
                    PB_C.PackageCode = sdr["PackageCode"].ToString();

                al.Add(PB_C);
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }

        return al;
    }

    public static ICollection getRecordsNew(string BarcodeID, int branchid, string FID)
    {
        
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandText = "select * from patmstd where BarcodeID=@BarcodeID and FID='" + FID + "' and branchid=" + branchid + "";      
        sc.Parameters.Add(new SqlParameter("@BarcodeID", SqlDbType.NVarChar, 50)).Value = BarcodeID;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            while (sdr != null && sdr.Read())
            {
                Patmstd_Bal_C PB_C = new Patmstd_Bal_C();
                PB_C.PatRegID = (sdr["PatRegID"].ToString());
                PB_C.FID = sdr["FID"].ToString();
                PB_C.MTCode = sdr["MTCode"].ToString();
                PB_C.SDCode = sdr["SDCode"].ToString();
                if (sdr["TestRate"] != DBNull.Value)
                    PB_C.TestRate = Convert.ToSingle(sdr["TestRate"]);

                if (!(sdr["Patregdate"] is DBNull))
                    PB_C.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                if (!(sdr["PackageCode"] is DBNull))
                    PB_C.PackageCode = sdr["PackageCode"].ToString();

                al.Add(PB_C);
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }

        return al;
    }


    public static ICollection Get_CodebyFID(string PatRegID, string FID, string labcd, string username, int branchid,int maindept)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
       
        sc = new SqlCommand("SELECT DISTINCT patmstd.SDCode " +
                          " FROM   patmstd INNER JOIN SubDepartment ON patmstd.SDCode = SubDepartment.SDCode INNER JOIN Deptwiseuser ON SubDepartment.DeptID = Deptwiseuser.DeptCodeID INNER JOIN " +
                          " PatSt ON patmstd.PatRegID = PatSt.PatRegID AND patmstd.FID = PatSt.FID AND " +
                          " patmstd.MTCode = PatSt.MTCode INNER JOIN BarM ON patmstd.PID = BarM.PID AND patmstd.BarcodeID = BarM.BarcodeID where (patmstd.PatRegID = @PatRegID) and dbo.BarM.SampleStatus = 'Registered' AND (patmstd.FID = @FID) and (patmstd.branchid=" + branchid + ") and SubDepartment.MainDeptid='" + maindept + "'", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = username;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Patmstd_Bal_C PB_C = new Patmstd_Bal_C();
                    PB_C.SDCode = sdr["SDCode"].ToString();                   
                    al.Add(PB_C);
                }
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
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }

        return al;
    }//

    public static void Update_TestWiseAmount(int PID, int branchid, string Code, double Testrecamt,double DispaidAmt)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" UPDATE patmstd SET  Testrecamt=@Testrecamt,Testdisc=@DispaidAmt " +
            " where PID=" + PID + " and branchid=" + branchid + " and MTCode ='" + Code + "' ", conn);


        sc.Parameters.Add(new SqlParameter("@Testrecamt", SqlDbType.Float)).Value = Testrecamt;
        sc.Parameters.Add(new SqlParameter("@DispaidAmt", SqlDbType.Float)).Value = DispaidAmt;
        short cnt;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }

   
 
}

