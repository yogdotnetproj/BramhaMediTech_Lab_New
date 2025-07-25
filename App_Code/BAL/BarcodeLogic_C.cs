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

public class BarcodeLogic_C
{
	public BarcodeLogic_C()
	{
		
	}


  
    public static bool isRecordForPIDExists(int PID, string status,int maindept)
    {        
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;
        if (maindept != 0)
        {
            sc = new SqlCommand("SELECT COUNT(*) FROM patmstd INNER JOIN BarM ON dbo.patmstd.PID = dbo.BarM.PID AND  patmstd.branchid = dbo.BarM.branchid INNER JOIN SubDepartment ON dbo.patmstd.SDCode = dbo.SubDepartment.SDCode AND  patmstd.branchid = dbo.SubDepartment.branchid where BarM.PID=@PID and SampleStatus=@SampleStatus and dbo.SubDepartment.MainDeptid =" + maindept + " GROUP BY dbo.SubDepartment.MainDeptid", conn);
        }
        else
        {
            sc = new SqlCommand("SELECT count(*) from BarM where  PID=@PID and SampleStatus=@SampleStatus", conn);
        }
        
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@SampleStatus", SqlDbType.NVarChar,50)).Value = status;

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

  
    public static string getbarcodeIDBySampleType(string sampleType, int PID, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" select BarcodeID from Patmstd where SampleType='" + sampleType + "' and PID=" + PID + " and branchid=" + branchid + "", conn);

        object drName = null;

        try
        {
            conn.Open();
            drName = sc.ExecuteScalar();
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
        if(drName==null)
            return "";
        else
            return drName.ToString();
    }
 
    public static bool RecordExistsforsample(int PID, string sampletype, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(*) from Patmstd where  PID=@PID and SampleType=@SampleType and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@SampleType", SqlDbType.NVarChar, 200)).Value = sampletype;

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

  
    public static ICollection Get_barcodelist_sampletypewise(int PID, int branchid, string sampletype,string Testcode)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = null;

        sc = new SqlCommand("SELECT     patmstd.MTCode AS STCode, patmstd.BarcodeID, patmstd.SampleType, MainTest.Maintestname as TestNames , patmstd.PID " +
               " FROM   patmstd INNER JOIN "+
                " MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode   WHERE patmstd.PID = @PID and patmstd.branchid=" + branchid + " and patmstd.SampleType='" + sampletype + "'   ", conn);
       
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
                    Barcode_C vm = new Barcode_C();
                    vm.BarcodeID = dr["BarcodeID"].ToString();
                    vm.SampleType = dr["SampleType"].ToString();
                    vm.STCODE = dr["STCODE"].ToString();
                    vm.TestNames = dr["TestNames"].ToString();
                    if (dr["PID"] != DBNull.Value)
                        vm.PID = Convert.ToInt32(dr["PID"]);
                    else
                        vm.PID = 0;
                  
                    al.Add(vm);
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

        return al;
    }
    public static ICollection Get_barcodelist_Direct(int PID, int branchid, string subdept)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = null;
        if (subdept == "")
        {
            sc = new SqlCommand("SELECT     patmstd.MTCode AS STCode, patmstd.BarcodeID, patmstd.SampleType, MainTest.Maintestname as TestNames , patmstd.PID " +
               " FROM   patmstd INNER JOIN " +
                " MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode  WHERE PID = @PID and patmstd.branchid=" + branchid + " ", conn);
        }
        else
        {
            sc = new SqlCommand("SELECT  distinct PID, BarcodeID,Sampletype ,STCode = STUFF(    (SELECT ',' + Mtcode    FROM VW_GetBarcode t1   " +
                              "  WHERE t1.barcodeid = t2.barcodeid and t1.Sampletype = t2.Sampletype   FOR XML PATH (''))   , 1, 1, '') , " +

                              "  TestNames = STUFF(    (SELECT ',' + testname    FROM VW_GetBarcode t3   " +
                              "  WHERE t3.barcodeid = t2.barcodeid  and t3.Sampletype = t2.Sampletype   FOR XML PATH (''))   , 1, 1, '') " +
                              "  from VW_GetBarcode t2  " +
                              "  group by barcodeid,PID,Sampletype having PID= @PID and BarcodeID<>''", conn);
        }
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
                    Barcode_C vm = new Barcode_C();
                    vm.BarcodeID = dr["BarcodeID"].ToString();
                    vm.SampleType = dr["SampleType"].ToString();
                    vm.STCODE = dr["STCODE"].ToString();
                    vm.TestNames = dr["TestNames"].ToString();
                    if (dr["PID"] != DBNull.Value)
                        vm.PID = Convert.ToInt32(dr["PID"]);
                    else
                        vm.PID = 0;
                  
                    al.Add(vm);
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

        return al;
    }


    public static ICollection Get_barcodelist(int PID, int branchid,string subdept)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = null;
        if (subdept == "")
        {
            sc = new SqlCommand("SELECT     patmstd.MTCode AS STCode, patmstd.BarcodeID, patmstd.SampleType, MainTest.Maintestname as TestNames , patmstd.PID " +
               " FROM   patmstd INNER JOIN " +
                " MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode  WHERE PID = @PID and patmstd.branchid=" + branchid + "  ", conn);//IspheboAccept=1 and PhlebotomistCollect=1
        }
        else
        {
            sc = new SqlCommand("SELECT  distinct PID, BarcodeID,Sampletype ,STCode = STUFF(    (SELECT ',' + Mtcode    FROM VW_GetBarcode t1   " +
                              "  WHERE t1.barcodeid = t2.barcodeid and t1.Sampletype = t2.Sampletype   FOR XML PATH (''))   , 1, 1, '') , " +

                              "  TestNames = STUFF(    (SELECT ',' + testname    FROM VW_GetBarcode t3   " +
                              "  WHERE t3.barcodeid = t2.barcodeid  and t3.Sampletype = t2.Sampletype   FOR XML PATH (''))   , 1, 1, '') " +
                              "  from VW_GetBarcode t2  " +
                              "  group by barcodeid,PID,Sampletype having PID= @PID and BarcodeID<>''", conn);
        }
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
                    Barcode_C vm = new Barcode_C();
                    vm.BarcodeID = dr["BarcodeID"].ToString();
                    vm.SampleType = dr["SampleType"].ToString();
                    vm.STCODE = dr["STCODE"].ToString();
                    vm.TestNames = dr["TestNames"].ToString();
                    if (dr["PID"] != DBNull.Value)
                        vm.PID = Convert.ToInt32(dr["PID"]);
                    else
                        vm.PID = 0;
                 
                    al.Add(vm);
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

        return al;
    }

    public static string getSampleType_ID(string sampleType,  int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" select sampleid from SCMsT where SampleType='" + sampleType + "'  and branchid=" + branchid + "", conn);

        object drName = null;

        try
        {
            conn.Open();
            drName = sc.ExecuteScalar();
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
        if (drName == null)
            return "";
        else
            return drName.ToString();
    }
    public static bool RecordExistsforTest(int PID, string MTCode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(*) from Patmstd where  PID=@PID and MTCode=@MTCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 200)).Value = MTCode;

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

 
}
