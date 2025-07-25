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


public class Barcode_C
{
	public Barcode_C()
	{
        this.PID  = 0;
        this.BarcodeID  = "";
        this.SampleType  = "";
        this.STCODE  = "";
        this.TestNames  = "";
        this.SampleStatus = "";
        this.Remark = "";
      
        
	}
    public bool UpdateFirst_Signature(int PID, string MTCode, int branchid, int TechnicanFirst, int srno)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET TechnicanFirst=@TechnicanFirst " +
           " WHERE PID=@PID and MTCode=@STCODE  and branchid=" + branchid + " and Patmstid=" + srno + " ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@TechnicanFirst", SqlDbType.NVarChar, 50)).Value = TechnicanFirst;
        SqlDataReader sdr = null;

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
    public bool UpdateSecond_Signature(int PID, string MTCode, int branchid, int TechnicanSecond, int srno)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET TechnicanSecond=@TechnicanSecond " +
           " WHERE PID=@PID and MTCode=@STCODE  and branchid=" + branchid + " and Patmstid=" + srno + " ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@TechnicanSecond", SqlDbType.NVarChar, 50)).Value = TechnicanSecond;
        SqlDataReader sdr = null;

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
    public void Update_OutsourceLab1(string MTcode, string FID, int branchid, int TPID, int OutsourceID, string OutsourceLabName)
    {
        SqlConnection conn = DataAccess.ConOutsource1();
        try
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = conn;
            sc.CommandText = "SP_OutsourceLAB";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add(new SqlParameter("@MTcode", SqlDbType.NVarChar, 50)).Value = MTcode;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = FID;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@TPID", SqlDbType.Int)).Value = TPID;
            sc.Parameters.Add(new SqlParameter("@OutsourceID", SqlDbType.Int)).Value = OutsourceID;
            sc.Parameters.Add(new SqlParameter("@OutsourceLabName", SqlDbType.NVarChar, 755)).Value = OutsourceLabName;
            
            
            conn.Close();
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { throw; }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                //if (sdr!= null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }

    public void Update_OutsourceLab2(string MTcode, string FID, int branchid, int TPID, int OutsourceID)
    {
        SqlConnection conn = DataAccess.ConOutsource2();
        try
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = conn;
            sc.CommandText = "SP_OutsourceLAB";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add(new SqlParameter("@MTcode", SqlDbType.NVarChar, 50)).Value = MTcode;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = FID;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@TPID", SqlDbType.Int)).Value = TPID;
            sc.Parameters.Add(new SqlParameter("@OutsourceID", SqlDbType.Int)).Value = OutsourceID;
            conn.Close();
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { throw; }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                //if (sdr!= null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }
    public void Update_OutsourceLab3(string MTcode, string FID, int branchid, int TPID, int OutsourceID)
    {
        SqlConnection conn = DataAccess.ConOutsource3();
        try
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = conn;
            sc.CommandText = "SP_OutsourceLAB";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add(new SqlParameter("@MTcode", SqlDbType.NVarChar, 50)).Value = MTcode;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = FID;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@TPID", SqlDbType.Int)).Value = TPID;
            sc.Parameters.Add(new SqlParameter("@OutsourceID", SqlDbType.Int)).Value = OutsourceID;
            conn.Close();
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { throw; }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                //if (sdr!= null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }
    public void Update_OutsourceLab4(string MTcode, string FID, int branchid, int TPID, int OutsourceID)
    {
        SqlConnection conn = DataAccess.ConOutsource4();
        try
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = conn;
            sc.CommandText = "SP_OutsourceLAB";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add(new SqlParameter("@MTcode", SqlDbType.NVarChar, 50)).Value = MTcode;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = FID;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@TPID", SqlDbType.Int)).Value = TPID;
            sc.Parameters.Add(new SqlParameter("@OutsourceID", SqlDbType.Int)).Value = OutsourceID;
            conn.Close();
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { throw; }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                //if (sdr!= null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }
    public void Update_OutsourceLab5(string MTcode, string FID, int branchid, int TPID, int OutsourceID)
    {
        SqlConnection conn = DataAccess.ConOutsource5();
        try
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = conn;
            sc.CommandText = "SP_OutsourceLAB";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add(new SqlParameter("@MTcode", SqlDbType.NVarChar, 50)).Value = MTcode;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = FID;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@TPID", SqlDbType.Int)).Value = TPID;
            sc.Parameters.Add(new SqlParameter("@OutsourceID", SqlDbType.Int)).Value = OutsourceID;
            conn.Close();
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { throw; }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                //if (sdr!= null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }


    public bool UpdateIspheboReject2way(int PID, int branchid, int IspheboAccept, int srno, string MTCode, string UserName, string txtrejectremarks)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET IspheboAccept=@IspheboAccept,Updatedon=@GateDate,Updatedby=@UserName ,Patauthicante='Registered',SampleAcceptDate=@GateDate,PhlebotomistRejectremark=@PhlebotomistRejectremark,PhlebotomistCollect=2  " +
           " WHERE PID=@PID and MTCode=@STCODE  and branchid=" + branchid + " and Patmstid=" + srno + " ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@IspheboAccept", SqlDbType.Int, 10)).Value = IspheboAccept;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@GateDate", SqlDbType.DateTime)).Value = Date.getdate();
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 1000)).Value = UserName;
        sc.Parameters.Add(new SqlParameter("@PhlebotomistRejectremark", SqlDbType.NVarChar, 1000)).Value = txtrejectremarks;
        SqlDataReader sdr = null;

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

    public bool UpdateIspheboAccept_firstway(int PID, int branchid, int IspheboAccept, int srno, string UserName)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET PhlebotomistCollect=@PhlebotomistCollect ,Updatedon=@GateDate,Updatedby=@UserName ,IspheboAccept=0 " +
           " WHERE PID=@PID  and branchid=" + branchid + " ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@PhlebotomistCollect", SqlDbType.Int, 10)).Value = IspheboAccept;
        sc.Parameters.Add(new SqlParameter("@GateDate", SqlDbType.DateTime)).Value = Date.getdate();
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 1000)).Value = UserName;

        SqlDataReader sdr = null;

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
    public bool UpdateIspheboAccept_PrintAll_Firstway(int PID, int branchid, int IspheboAccept, int srno, string MTCode, string UserName)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET PhlebotomistCollect=@PhlebotomistCollect,Updatedon=@GateDate,Updatedby=@UserName   " +
           " WHERE PID=@PID and MTCode=@STCODE  and branchid=" + branchid + " ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@PhlebotomistCollect", SqlDbType.Int, 10)).Value = IspheboAccept;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@GateDate", SqlDbType.DateTime)).Value = Date.getdate();
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 1000)).Value = UserName;

        SqlDataReader sdr = null;

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

    public DataTable GetTestCodeSpecimanNo_FirstWay(string PID, int Branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select distinct MTCode,Patauthicante ,Patrepstatus,PhlebotomistCollect,IspheboAccept from patmstd where PID='" + PID + "' and Branchid=" + Branchid + "", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (Exception ex)
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

    public bool UpdateIspheboAccept_TestWise_Firstway_New(int PID, int branchid, int IspheboAccept,  string MTCode, string UserName)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET PhlebotomistCollect=@PhlebotomistCollect,Updatedon=@GateDate,Updatedby=@UserName,IspheboAccept=0 ,SampleAcceptDate1=@GateDate " +
           " WHERE Patauthicante='Registered' and PID=@PID and MTCode=@STCODE  and branchid=" + branchid + "  ", conn);//and Patmstid=" + srno + "

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@PhlebotomistCollect", SqlDbType.Int, 10)).Value = IspheboAccept;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@GateDate", SqlDbType.DateTime)).Value = Date.getdate();
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 1000)).Value = UserName;

        SqlDataReader sdr = null;

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

    public bool UpdateIspheboAccept_TestWise_Firstway(int PID, int branchid, int IspheboAccept, int srno, string MTCode, string UserName, string BarCodeId)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET PhlebotomistCollect=@PhlebotomistCollect,Updatedon=@GateDate,SampleAcceptDate1=@GateDate,Updatedby=@UserName,IspheboAccept=0 ,BarCodeId=@BarCodeId " +
           " WHERE PID=@PID and MTCode=@STCODE  and branchid=" + branchid + " and Patmstid=" + srno + " ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@PhlebotomistCollect", SqlDbType.Int, 10)).Value = IspheboAccept;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@GateDate", SqlDbType.DateTime)).Value = Date.getdate();
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 1000)).Value = UserName;
        sc.Parameters.Add(new SqlParameter("@BarCodeId", SqlDbType.NVarChar, 1000)).Value = BarCodeId;

        SqlDataReader sdr = null;

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

    public bool UpdateoutsourceLab(int PID, string MTCode, int branchid, int OutLabName,int srno)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET Isoutsource=@Isoutsource ,OutLabName=@OutLabName" +//,OutLabName=@OutLabName
           " WHERE PID=@PID and MTCode=@STCODE  and branchid=" + branchid + " and Patmstid=" + srno + " ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@Isoutsource", SqlDbType.Bit, 500)).Value = true;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@OutLabName", SqlDbType.NVarChar, 50)).Value = OutLabName;
        SqlDataReader sdr = null;

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
    public bool UpdateIspheboAccept(int PID, int branchid, int IspheboAccept, int srno, string UserName)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET IspheboAccept=@IspheboAccept ,Updatedon=@GateDate,Updatedby=@UserName, PheboAcceptBy=@UserName,Patauthicante='Registered',SampleAcceptDate=@GateDate " +
           " WHERE IspheboAccept=0 and PID=@PID  and branchid=" + branchid + " ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@IspheboAccept", SqlDbType.Int, 10)).Value = IspheboAccept;
        sc.Parameters.Add(new SqlParameter("@GateDate", SqlDbType.DateTime)).Value = Date.getdate();
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 1000)).Value = UserName;
       
        SqlDataReader sdr = null;

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
    public bool UpdateIspheboAccept_TestWise_New(int PID, int branchid, int IspheboAccept,  string MTCode, string UserName, string txtrejectremarks)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET IspheboAccept=@IspheboAccept,Updatedon=@GateDate,Updatedby=@UserName,PheboAcceptBy=@UserName ,Patauthicante='Registered',SampleAcceptDate=@GateDate,PhlebotomistRejectremark=@PhlebotomistRejectremark  " +
           " WHERE  Patauthicante='Registered' and PID=@PID and MTCode=@STCODE  and branchid=" + branchid + "  ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@IspheboAccept", SqlDbType.Int, 10)).Value = IspheboAccept;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@GateDate", SqlDbType.DateTime)).Value = Date.getdate();
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 1000)).Value = UserName;
        sc.Parameters.Add(new SqlParameter("@PhlebotomistRejectremark", SqlDbType.NVarChar, 1000)).Value = txtrejectremarks;
        SqlDataReader sdr = null;

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

    public bool UpdateIspheboAccept_TestWise(int PID, int branchid, int IspheboAccept, int srno, string MTCode, string UserName, string txtrejectremarks, string SpecimanRemark, string ReqbyDoc)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET IspheboAccept=@IspheboAccept,Updatedon=@GateDate,Updatedby=@UserName ,Patauthicante='Registered',PheboAcceptBy=@UserName,SampleAcceptDate=@GateDate,PhlebotomistRejectremark=@PhlebotomistRejectremark,SpeciamRemark=@SpeciamRemark,ReqbyDoc=@ReqbyDoc  " +
           " WHERE Patauthicante='Registered' and  PID=@PID and MTCode=@STCODE  and branchid=" + branchid + " and Patmstid=" + srno + " ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@IspheboAccept", SqlDbType.Int, 10)).Value = IspheboAccept;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@GateDate", SqlDbType.DateTime)).Value = Date.getdate();
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 1000)).Value = UserName;
        sc.Parameters.Add(new SqlParameter("@PhlebotomistRejectremark", SqlDbType.NVarChar, 1000)).Value = txtrejectremarks;

        sc.Parameters.Add(new SqlParameter("@SpeciamRemark", SqlDbType.NVarChar, 1000)).Value = SpecimanRemark;
        sc.Parameters.Add(new SqlParameter("@ReqbyDoc", SqlDbType.NVarChar, 1000)).Value = ReqbyDoc;

        SqlDataReader sdr = null;

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

    public bool UpdateIspheboAccept_PrintAll(int PID, int branchid, int IspheboAccept, int srno, string MTCode, string UserName)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET IspheboAccept=@IspheboAccept,Updatedon=@GateDate,Updatedby=@UserName ,Patauthicante='Registered',SampleAcceptDate=@GateDate  " +
           " WHERE PID=@PID and MTCode=@STCODE  and branchid=" + branchid + " ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@IspheboAccept", SqlDbType.Int, 10)).Value = IspheboAccept;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@GateDate", SqlDbType.DateTime)).Value = Date.getdate();
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 1000)).Value = UserName;

        SqlDataReader sdr = null;

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


    public static bool IsbarcodeIdExist(string BarcodeID, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(*) from Patmstd where  BarcodeID=@BarcodeID and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@BarcodeID", SqlDbType.NVarChar, 500)).Value = BarcodeID;

        int cnt = 0;

        try
        {
            conn.Open();
            cnt = Convert.ToInt32(sc.ExecuteScalar());

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
        if (cnt != 0)
            return true;
        else
            return false;
    }

    public static bool IsbarcodeIdExist_Previous_Patient(string BarcodeID, int branchid,int PID)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(*) from Patmstd where  BarcodeID=@BarcodeID and branchid=" + branchid + " and PID < " + PID + "", conn);
        sc.Parameters.Add(new SqlParameter("@BarcodeID", SqlDbType.NVarChar, 500)).Value = BarcodeID;

        int cnt = 0;

        try
        {
            conn.Open();
            cnt = Convert.ToInt32(sc.ExecuteScalar());

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
        if (cnt != 0)
            return true;
        else
            return false;
    }
  
   
    public static void Getreisternamewithbarcode(int PID, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = null;

        sc = new SqlCommand("SELECT  dbo.patmst.Patregid, RTRIM(dbo.patmst.intial) " +
                      " + ' ' + dbo.patmst.Patname + ' ' + dbo.patmst.LastName AS name from patmst WHERE PID = @PID and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        SqlDataReader dr = null;
        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    RegNo = dr["Patregid"].ToString();
                    Patname = dr["name"].ToString();
                }
            }
        }
        finally
        {
            try
            {
                if (dr != null) dr.Close();

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
    }

  
    public bool Updatetestdisc(int branchid, float Testdisc, float Testrecamt)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET Testdisc=@Testdisc,Testrecamt=@Testrecamt " +
           " WHERE PID =@PID and MTCODE=@MTCODE  and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@Testdisc", SqlDbType.Float)).Value = Testdisc;
        sc.Parameters.Add(new SqlParameter("@MTCODE", SqlDbType.NVarChar, 1000)).Value = STCODE;
        sc.Parameters.Add(new SqlParameter("@Testrecamt", SqlDbType.Float)).Value = Testrecamt;
        SqlDataReader sdr = null;

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

    public bool Update_Cshmst(int PID, string Patienttest, float Netpayment, float Balance)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE Cshmst " +
            "SET Netpayment=isnull(Othercharges,0)+@Netpayment,Balance=@Balance " +
           " WHERE PID =@PID   ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@Netpayment", SqlDbType.Float)).Value = Netpayment;
      
        sc.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Float)).Value = Balance;
        SqlDataReader sdr = null;

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

  

    public  DataTable UpdateAddNewTestBalance(string PID, int Branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select distinct isnull(NetPayment,0)as NetPayment,(AmtPaid+Discount) as recamount  from Cshmst where PID='" + PID + "' and Branchid=" + Branchid + "", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (Exception ex)
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
    public bool Insert_Update_SpecimanNo(int branchid, string MTCode,int PID)
    {

        SqlConnection con = DataAccess.ConInitForDC();
      
        SqlCommand cmd = new SqlCommand("SP_UpdateSpecimanNo", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@branchid", SqlDbType.NVarChar, 50)).Value = branchid;
        cmd.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        cmd.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;

        try
        {
            con.Open();
            cmd.CommandTimeout = 1200;

            cmd.ExecuteNonQuery();
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
        return true;
    }
    public DataTable GetTestCodeSpecimanNo(string PID, int Branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select distinct MTCode,Patauthicante ,Patrepstatus from patmstd where PID='" + PID + "' and Branchid=" + Branchid + "", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (Exception ex)
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

    public bool UpdateIspheboAccept_TestWise_New_Barcode(int PID, int branchid, int IspheboAccept, string BarCode, string UserName, string txtrejectremarks)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE patmstd " +
            "SET IspheboAccept=@IspheboAccept,Updatedon=@GateDate,Updatedby=@UserName,PheboAcceptBy=@UserName ,Patauthicante='Registered',SampleAcceptDate=@GateDate,PhlebotomistRejectremark=@PhlebotomistRejectremark  " +
           " WHERE Patauthicante='Registered' and PID=@PID and BarCodeID=@BarCode  and branchid=" + branchid + "  ", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@IspheboAccept", SqlDbType.Int, 10)).Value = IspheboAccept;
        sc.Parameters.Add(new SqlParameter("@BarCode", SqlDbType.NVarChar, 1000)).Value = BarCode;
        sc.Parameters.Add(new SqlParameter("@GateDate", SqlDbType.DateTime)).Value = Date.getdate();
        sc.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 1000)).Value = UserName;
        sc.Parameters.Add(new SqlParameter("@PhlebotomistRejectremark", SqlDbType.NVarChar, 1000)).Value = txtrejectremarks;
        SqlDataReader sdr = null;

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


    #region properties

    private int pID;
    public int PID
    {
        get { return pID; }
        set { pID = value; }
    }

    private string barcodeID;

    public string BarcodeID
    {
        get { return barcodeID; }
        set { barcodeID = value; }
    }

    private string sampletype;

    public string SampleType
    {
        get { return sampletype; }
        set { sampletype = value; }
    }

    private string sTCODE;

    public string STCODE
    {
        get { return sTCODE; }
        set { sTCODE = value; }
    }

    private string testnames;

    public string TestNames
    {
        get { return testnames; }
        set { testnames = value; }
    }
    private string samplestatus;

    public string SampleStatus
    {
        get { return samplestatus; }
        set { samplestatus = value; }
    }
   
    private static string _RegNo;
    public static string RegNo
    {
        get { return _RegNo; }
        set { _RegNo = value; }
    }
    private static string _Fname;
    public static string Patname
    {
        get { return _Fname; }
        set { _Fname = value; }
    }
    private string remark;
    public string Remark
    {
        get { return remark; }
        set { remark = value; }
    }
   
    #endregion

}
