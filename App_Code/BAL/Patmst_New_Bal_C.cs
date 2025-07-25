using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


public class Patmst_New_Bal_C
{
    public static int reccnt = 0;
    public static int reccount
    {
        get { return reccnt; }
        set { reccnt = value; }
    }
    public static void Update_PanicRemark(string PanicRemark, string PanicInformToResult, string PatRegID,string MTCode, string FID,string UserName)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" UPDATE patmstd SET  PanicInformToResult=@PanicInformToResult, PanicRemark=@PanicRemark,InformUserName=@InformUserName ,PanicAction=1 " +
            " where PatRegID=@PatRegID and MTCode=@MTCode and FID=@FID ", conn);


        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;

        sc.Parameters.Add(new SqlParameter("@PanicInformToResult", SqlDbType.NVarChar, 500)).Value = PanicInformToResult;

        sc.Parameters.Add(new SqlParameter("@PanicRemark", SqlDbType.NVarChar, 500)).Value = PanicRemark;
        sc.Parameters.Add(new SqlParameter("@InformUserName", SqlDbType.NVarChar, 500)).Value = UserName;


        // Add the employee ID parameter and set its value.
        short cnt;
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
                conn.Close(); conn.Dispose();
            }
            catch
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }


    public static DataTable GetPatmstForPanicPatient(object UnitCode, object DeptID, object fromDate, object toDate, string Patauthicante, string MTCode, string patientName, string regnoID, string Barid, string CenterCode, string username, string usertype, int branchid, int maindept, int Number, string testname, string MNo, string CenterCodeNew)
    {

        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        string sql = "SELECT distinct PatRegID, Test1, PID, FID,  Patregdate, intial, Patname, sex, Age, RefDr, Reportdate, DoctorCode, CenterCode, " +
             "  FinancialYearID, TestCharges, Username, SampleType, " +
             "  SampleStatus, convert(varchar(20),RegistratonDateTime,103)+' '+convert(varchar(20),convert(time,RegistratonDateTime),100) as RegistratonDateTime,  Drname, MDY, cliniclahist, Branchid, DigModule, UnitCode, Remark, " +
             "  Patphoneno, IsActive, LabRegMediPro,Maintestname as TestName,Labno ,CenterCode as CenterCode,isnull(intial,'.') +' '+Patname as FullName,cast( Age as nvarchar(50)) +' '+MDY as MYD,Drname as DrName, ISReRun+'-'+MachinName+'-'+ReRunRemark as ReRunType, SpecimanNo , ";

       
        //if (Patauthicante == "Pending")
        //{
        //    sql = sql + " 'Pending' as SampleStatusNew, ";
        //}
        //if (Patauthicante == "Completed")
        //{
        //    sql = sql + " 'Completed' as SampleStatusNew, ";
        //}
        //if (Patauthicante == "Authorized")
        //{
        //    sql = sql + " 'Authorized' as SampleStatusNew, ";
        //}
        //if (Patauthicante == "Tested")
        //{
        //    sql = sql + " 'Tested' as SampleStatusNew, ";
        //}
        //if (Patauthicante == "IntNotRece")
        //{
        //    sql = sql + " 'Tested' as SampleStatusNew, ";
        //}
        //if (Patauthicante == "IntRece")
        //{
        //    sql = sql + " 'Tested' as SampleStatusNew, ";
        //}
        //if (Patauthicante == "All")
        //{
        //    sql = sql + " 'All' as SampleStatusNew, ";

        //}
        //if (Patauthicante == "Emergency")
        //{
        //    sql = sql + " 'All' as SampleStatusNew, ";

        //}
        sql = sql + "  ''as TestName,Remark as p_remark,LabNo,MTCode ,Isemergency,   case when PatientEmail=1 then 'Send Email' else 'Not Send' end as MailStatus,case when DoctorEmail=1 then 'Send DrEmail' else 'Not Send' end as DrMailStatus,InterfaceStatus,PPID , PanicResult  ,PanicRemark,PanicInformToResult,PanicAction,InformUserName " +

           " from VW_patlbvw where PanicResult='Panic' and IsActive=1 and  branchid=" + branchid + " and IspheboAccept =1 "; //and FID='" + FID + "'

        if (regnoID == "" && Barid == "")
        {
            if (fromDate != null && toDate != null)
            {
                sql = sql + " and " + "VW_patlbvw.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and VW_patlbvw.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(toDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "' ";
            }
        }
        if (MTCode != "")
        {
            sql = sql + " and " + " VW_patlbvw.Test1 like '%" + MTCode + "%'";
        }
        if (patientName != "")
        {
            sql = sql + " and " + " VW_patlbvw.Patname like N'%" + patientName + "%'";
        }


        if (CenterCode != "")
        {
            sql = sql + " and " + " VW_patlbvw.PPID ='" + CenterCode + "'";
        }
        if (CenterCodeNew != "")
        {
            sql = sql + " and " + " VW_patlbvw.CenterCode ='" + CenterCodeNew + "'";
        }
        if (Barid != "")
        {
            sql = sql + " and " + " VW_patlbvw.BarcodeID like '%" + Barid + "%'";
        }
        if (maindept != 0)
        {
            sql = sql + " and " + " VW_patlbvw.DigModule ='" + maindept + "'";
        }
        if (UnitCode != null)
        {
            sql = sql + " and " + " VW_patlbvw.UnitCode='" + UnitCode + "'";
        }
        if (usertype == "Main Doctor" || usertype == "MainDoctor" || usertype == "Technician" || usertype == "Reporting")
        {
            if (username != "")
            {
                // sql = sql + " and " + " VW_patlbvw.Username='" + username + "'";
                sql = sql + " and " + " VW_patlbvw.DeptUserName='" + username + "'";

            }
        }
        if (MNo != "")
        {
            sql = sql + " and " + " VW_patlbvw.RefDr like '%" + MNo + "%'";
        }
        if (Patauthicante != "All")
        {
            if (Patauthicante == "Pending")
            {
                sql = sql + " and " + "  (VW_patlbvw.PanicAction=0 ) ";//and  VW_patlbvw.Isemergency='True'
            }
            else if (Patauthicante == "Inform")
            {
                sql = sql + " and " + "  VW_patlbvw.PanicAction=1 ";
            }
            //else if (Patauthicante == "Emergency")
            //{
            //    sql = sql + " and " + "  VW_patlbvw.Isemergency='True' ";
            //}
            //else if (Patauthicante == "IntNotRece")
            //{
            //    sql = sql + " and " + "    InterfaceStatus ='No'  ";
            //}
            //else if (Patauthicante == "IntRece")
            //{
            //    sql = sql + " and " + "   InterfaceStatus ='Yes' and (VW_patlbvw.Patauthicante<>'Authorized') ";
            //}
            //else
            //{
            //    sql = sql + " and " + "  VW_patlbvw.Patauthicante='Authorized' ";
            //}
            //sql = sql + " and " + " dbo.Get_Teststatus(VW_patlbvw.PID,VW_patlbvw.PatRegID,VW_patlbvw.Branchid,VW_patlbvw.FID) ='" + Patauthicante + "'";
        }
        if (testname != "")
        {
            sql = sql + " and " + " VW_patlbvw.subdeptName = '" + testname + "'";
        }
        sql = sql + " order by  PID desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataTable ds = new DataTable();
        try
        {
            conn.Open();

            da.Fill(ds);
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
        return ds;

    }


    public DataSet Get_Phlebotomist_MainDoc_2Way(string UnitCode, object startDate, object endDate, string patientName, string Reg_no, int branchid, string maindept, string FID, int Number, string username, string UserType, object DoctorCode, string Barcode, string NewBarcode, string OutNewBarcode,string PPID)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);



        string sql = " Select * from VW_testmgr_n where PhlebotomistCollect=1 and branchid=" + branchid + " and dbo.Fun_GetUserAuthorizedTests(dbo.VW_testmgr_n.STCODE,1,'" + username + "')<>'' ";//FID='" + FID + "' and 

        if (PPID != "")
        {
            sql = sql + " and ( PPID='" + PPID + "'  )";//or LabRegMediPro='" + Reg_no + "'  or Barcodeid='" + Reg_no + "'
        }
        if (Reg_no != "")
        {
            sql = sql + " and ( PatRegID='" + Reg_no + "'  )";
        }
        else
        {
            if (patientName != "")
            {
                sql = sql + " and Patname like  N'%" + patientName + "%'";
            }
            else
            {
                if (DoctorCode != "" && DoctorCode.ToString() != "0")
                {
                    sql += " and CenterCode='" + DoctorCode.ToString() + "'";
                }
                if (startDate != null && endDate != null)
                {
                    sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
                }
            }
        }

        if (NewBarcode != "")
        {
            sql += " and Barcodeid = '" + NewBarcode + "'";
        }
        if (OutNewBarcode != "")
        {
            sql += " and OSBarcodeid = '" + OutNewBarcode + "'";
        }
        
        if (Barcode != "All")
        {
            sql += " and IspheboAccept = '" + Barcode + "'";
        }

        if (UnitCode != "" && UnitCode != null)
        {
            sql = sql + " and UnitCode='" + UnitCode + "'";
        }
        sql += " order by PatRegID desc,SrNo";
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            da.Fill(ds);
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
        return ds;
    }

    public DataSet Get_Phlebotomist_2Way(object DoctorCode, object startDate, object endDate, string patientName, string PatReg_ID, int branchid, string maindept, string FID, int Number, string username, string UserType, string Barcode, string Mobno, string NewBarcode, string OutNewBarcode,string PPID)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        string sql = "Select * from VW_testmgr where PhlebotomistCollect=1 and branchid=" + branchid + ""; //FID='" + FID + "' and

        if (PPID != "")
        {
            sql = sql + " and ( PPID='" + PPID + "'  )";//or LabRegMediPro='" + Reg_no + "'  or Barcodeid='" + Reg_no + "'
        }
        if (PatReg_ID != "")
        {
            sql = sql + " and (PatRegID='" + PatReg_ID + "'   )";//or LabRegMediPro='" + Reg_no + "'  or Barcodeid='" + Reg_no + "'
        }
        else
        {
            if (patientName != "")
            {
                sql = sql + " and Patname like  N'%" + patientName + "%'";
            }
            else
            {
                if (DoctorCode != "" && DoctorCode.ToString() != "0")
                {
                    sql += " and CenterCode='" + DoctorCode.ToString() + "'";
                }

            }
        }
        if (startDate != null && endDate != null)
        {
            sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
        }
        if (NewBarcode != "")
        {
            sql += " and  Barcodeid = '" + NewBarcode + "' ";
        }
        if (OutNewBarcode != "")
        {
            sql += " and OSBarcodeid = '" + OutNewBarcode + "'";
        }
        if (Barcode != "All")
        {

            sql += " and  IspheboAccept = '" + Barcode + "'";
        }
        if (Mobno != "")
        {
            sql += " and Patphoneno = '" + Mobno + "' ";
        }

        sql += " order by PID desc, SampleType desc ";//srno
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        try
        {

            conn.Open();
            da.Fill(ds);
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
        return ds;
    }

    public static void Update_callStatus(string PatRegID, string CallRemark,bool iscall)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" UPDATE patmst SET  ISCallPatient=@ISCallPatient, CallRemark=@CallRemark" +
            " where PatRegID=@PatRegID ", conn);

       
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@ISCallPatient", SqlDbType.Bit)).Value = iscall;

        sc.Parameters.Add(new SqlParameter("@CallRemark", SqlDbType.NVarChar, 500)).Value = CallRemark;


        // Add the employee ID parameter and set its value.
        short cnt;
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
                conn.Close(); conn.Dispose();
            }
            catch
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }

    public DataTable Get_PatientCallDetaiis_Report(string PatRegID, string fromdate, string Todate, string Call)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = " SELECT   DISTINCT c.PID, c.PatRegID, c.Patregdate, c.intial, c.Patname, c.sex, c.Age, c.MDY, c.RefDr, " +
           "  c.Patphoneno, c.CenterCode, c.FinancialYearID, c.EmailID, case when c.OtherRefDoctor='' then  c.Remark else c.OtherRefDoctor end  as CallRemark, " +
           "  c.Drname,  c.CenterName, c.Patphoneno as TelNo, c.Email,  c.testname,  c.intial + ' ' + c.Patname AS FullName, patmstd.Patauthicante,c.FID , C.SocialMedia " +
           "  FROM         patmst AS c INNER JOIN " +
           "  patmstd ON c.PID = patmstd.PID AND c.PatRegID = patmstd.PatRegID WHERE patmstd.Patauthicante='Authorized'   and dbo.Get_Teststatus_PatientCall(c.PatRegID,c.Branchid,c.FID)=0  ";
          if(fromdate!="")
          {
              sql = sql + " and  (CAST(CAST(YEAR(c.Patregdate) AS varchar(4)) + '/' + CAST(MONTH(c.Patregdate) AS varchar(2)) + '/' + CAST(DAY(c.Patregdate) AS varchar(2))  AS datetime) between ('" + Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(Todate).ToString("MM/dd/yyyy") + "') )";
          }
          if (PatRegID != "")
          {
              sql = sql + " and c.PatRegID ='" + PatRegID + "' ";
          }
          if (Call == "NotDone")
          {
              sql = sql + " and ISCallPatient=0 ";
          }
          else if (Call == "Viber")
          {
              sql = sql + " and c.SocialMedia=1 and ISCallPatient=0  ";
          }
          else if (Call == "Whatsapp")
          {
              sql = sql + " and c.SocialMedia=2  and ISCallPatient=0  ";
          }
          else
          {
              sql = sql + " and ISCallPatient=1 ";
          }
          sql = sql + "   ORDER BY c.PID DESC ";
          SqlDataAdapter da = new SqlDataAdapter(sql, con);
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
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public DataTable Get_PatientOnline_Report(string username)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter da = new SqlDataAdapter("  SELECT DISTINCT "+
              "  c.SrNo, c.PID, c.FID, c.PatRegID, c.Patregdate, c.intial, c.Patname, c.sex, c.Age, c.MDY, c.RefDr, c.PF, c.Reportdate, c.Phrecdate, c.flag, c.Patphoneno, c.Pataddress, c.Isemergency, c.Branchid, c.DoctorCode "+
              "  , c.CenterCode, c.FinancialYearID, c.EmailID, c.Drname, c.TestCharges, c.SampleID, c.CenterName, c.Username, c.Usertype, c.SampleType, c.SampleStatus, c.Remark,  c.RegistratonDateTime, c.TelNo, c.Email, "+
              "  c.Patusername, c.Patpassword, c.PPID, cast( c.testname as nvarchar(4000))as testname, "+
              "  c.Smsevent, c.UnitCode, c.IsbillBH, c.IsActive, c.Monthlybill, c.cevent, c.LabRegMediPro, c.IsFreeze, c.ISCallPatient, c.CallRemark, c.OtherRefDoctor, c.Weights, c.Heights, "+
              "  c.Disease, c.LastPeriod, c.Symptoms, c.FSTime, c.Therapy, c.SocialMedia, c.ReportAssignStatus, c.uploadPrescription, c.OutsourcePID, c.ReportingTime, c.ImagePath, dbo.FUN_GetReceiveAmt(1, c.PID) "+
              "  AS AmtPaid, dbo.FUN_GetReceiveAmt_Discount(1, c.PID) AS Discount, 0 AS DisFlag, c.intial + ' ' + c.Patname AS FullName "+
                        " FROM dbo.patmst c LEFT OUTER JOIN dbo.RecM cm  ON c.PID = cm.PID  where c.Patusername =  N'" + username + "'  order by c.SrNo desc ", con);//and  dbo.FUN_GetReceiveAmt_Balance(1, c.PID)=0 
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
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public void UpdateBarCode_Fix(string Barcode, string SrNo, int PID, string branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("UPDATE patmstd SET BarCodeID=@BarCodeID where Patmstid=@SrNo and branchid='" + branchid + "'", conn);
        sc.Parameters.Add(new SqlParameter("@BarCodeID", SqlDbType.NVarChar, 50)).Value = Barcode;
        sc.Parameters.Add(new SqlParameter("@SrNo", SqlDbType.NVarChar, 50)).Value = SrNo;
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;

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

    public DataTable GetReceiptNo(int branchid, int PID)
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_GetReceiptNo", conn);


        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;

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
    public DataTable GetoutsourceLab( int branchid)
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_GetoutsourceLab", conn);

       
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;     

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
    public DataSet Getoutsourcepatient(object DoctorCode, object startDate, object endDate, string patientName, string PatReg_ID, int branchid, string maindept, string FID, int Number, string username, string UserType, string Barcode, string Mobno, string OutsourceLab)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        string sql = "Select * from VW_Getoutsourcepatient where  branchid=" + branchid + ""; //FID='" + FID + "' and

        if (PatReg_ID != "")
        {
            sql = sql + " and PatRegID='" + PatReg_ID + "'";
        }
        else
        {
            if (patientName != "")
            {
                sql = sql + " and Patname like '%" + patientName + "%'";
            }
            else
            {
                if (DoctorCode != "" && DoctorCode.ToString() != "0")
                {
                    sql += " and CenterCode='" + DoctorCode.ToString() + "'";
                }

            }
        }
        if (startDate != null && endDate != null)
        {
            sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
        }
        if (Barcode != "")
        {
            sql += " and BarcodeID like '%" + Barcode + "%'";
        }
        if (OutsourceLab != "")
        {
            sql += " and OutsourceLabName like '%" + OutsourceLab + "%'";
        }
        if (Mobno != "")
        {
            sql += " and Patphoneno = '" + Mobno + "' ";
        }
        if (maindept != "" && maindept != null && maindept != "0")
        {
            sql = sql + " and MainDept =" + Convert.ToInt32(maindept) + "";
        }
        sql += " order by PatRegID desc,Patmstid";
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            da.Fill(ds);
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
        return ds;
    } 


    public  void UpdateHospitaltocash(string Fid,string PatRegID, int PID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("UPDATE patmst SET IsbillBH=0 where PID=@PID and fid=@FID and PatRegID=@PatRegID and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Fid;
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;       
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;

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

    public void UpdateCashtoHospital(string Fid, string PatRegID, int PID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("UPDATE patmst SET IsbillBH=1 where PID=@PID and fid=@FID and PatRegID=@PatRegID and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Fid;
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;

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


    public static string Get_MaxAutoRegno(string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        string s = "";

        try
        {

            conn.Open();
            sc = new SqlCommand("select isnull(max(cast(PatRegID as int)),0) from patmst where FID=@FID  and branchid=" + branchid + "", conn);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;

            object o = sc.ExecuteScalar();
            int i = Convert.ToInt32(o);
            i++;
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
    public static int Get_MaxAutoRegno(int maxnum, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        int i;
        try
        {
            
            conn.Open();
            
            i = maxnum;
            while (true)
            {
                sc = new SqlCommand("select PatRegID from patmst where PatRegID =@PatReg_ID and branchid=" + branchid + "", conn);
                sc.Parameters.Add(new SqlParameter("@PatReg_ID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(i);


                if (i == 100000)          
                {
                    throw new Exception("Limit Exceeded");
                    //break;
                }

                if (Convert.ToString(sc.ExecuteScalar()) != Convert.ToString(i))
                {
                    break;
                }
                i++;
            }
            return i;
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch (Exception) { throw new Exception("Data Not Found"); }
        }
        
    }

    public static int PIDAutoGenerateLogic(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        int i = 0;
        try
        {           
            conn.Open();
            sc = new SqlCommand("select max(PID) from patmst where branchid=" + branchid + " ", conn);

            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
            {
                o = 0;
            }
            i = Convert.ToInt32(o);
            i++;

        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {

            conn.Close(); conn.Dispose();

        }
        return i;

    }

    public static short Get_patmst_DataCount(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(srno) from patmst  where branchid=" + branchid + "", conn);

        short cnt;
        try
        {
            conn.Open();
            cnt = Convert.ToInt16(sc.ExecuteScalar());
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
        return cnt;
    } 

    public static ICollection Get_patmst_Data(string orderField, int branchid)
    {
        ArrayList al = new ArrayList();
               
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "Select * from patmst  where branchid=" + branchid + "";

        if (orderField != "")
        {
            sc.CommandText = sc.CommandText + " " + "order by " + orderField.ToString();
        }

        sc.Connection = conn;

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
                    Patmst_Bal_C Obj_PBC = new Patmst_Bal_C();

                    Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                    Obj_PBC.FID = (string)sdr["FID"];

                    Obj_PBC.Patname = sdr["Patname"].ToString();
                    Obj_PBC.Email = sdr["Email"].ToString();
                    Obj_PBC.Pataddress = sdr["Pataddress"].ToString();
                  
                    Obj_PBC.Phone = sdr["Patphoneno"].ToString();
                    Obj_PBC.branchid = Convert.ToInt32(sdr["branchid"]);
                    Obj_PBC.CenterCode = Convert.ToString(sdr["CenterCode"]);

                  
                    if (sdr["Phrecdate"] != DBNull.Value)
                        Obj_PBC.Phrecdate = Convert.ToDateTime(sdr["Phrecdate"]);
                    else
                        Obj_PBC.Phrecdate = Date.getMinDate();


                    al.Add(Obj_PBC);
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

  

    public DataSet FilldeptDrop()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter da = new SqlDataAdapter("Select SDCode,subdeptName from SubDepartment order by subdeptName", con);
        DataSet ds = new DataSet();
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
            con.Close();
        }
        return ds;
    }


    public static ICollection Get_patmst_againsPIDID(int PID, int branchid)
    {
        ArrayList al = new ArrayList();           

        SqlConnection conn = DataAccess.ConInitForDC();
        //SqlCommand sc = new SqlCommand("SELECT        patmst.SrNo, patmst.PID, patmst.FID, patmst.PatRegID, patmst.Patregdate, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.MDY, patmst.RefDr, patmst.Tests, patmst.PF, patmst.Reportdate, "+
        //  "  patmst.Phrecdate, patmst.flag, patmst.Patphoneno, patmst.Pataddress, patmst.Isemergency, patmst.Branchid, patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.EmailID, patmst.Drname, "+
        //  "  patmst.TestCharges, patmst.SampleID, patmst.CenterName, patmst.Username, patmst.Usertype, patmst.SampleType, patmst.SampleStatus, patmst.Remark, patmst.PatientcHistory, patmst.RegistratonDateTime, "+
        //  "  patmst.TelNo, patmst.Email, patmst.Patusername, patmst.Patpassword, patmst.PPID, patmst.testname, patmst.Smsevent, patmst.UnitCode, patmst.IsbillBH, patmst.IsActive, patmst.Monthlybill, patmst.cevent, "+
        //  "  patmst.LabRegMediPro, patmst.IsFreeze, patmst.ISCallPatient, patmst.CallRemark, patmst.OtherRefDoctor, patmst.Weights, patmst.Heights, patmst.Disease, patmst.LastPeriod, patmst.Symptoms, "+
        //  "  patmst.FSTime, patmst.Therapy, patmst.SocialMedia, patmst.ReportAssignStatus, PatMT.PatientCardNo, PatMT.PatientCardExpNo, PatMT.DateOfBirth, PatMT.AccDateofBirth ,patmst.UploadPrescription ,patmst.ImagePath,patmst.DOB " +
        //  "  FROM            patmst INNER JOIN "+
        //  "  PatMT ON patmst.PPID = PatMT.PPID where PID=@PID and branchid=" + branchid + "", conn);

        SqlCommand sc = new SqlCommand("Select   patmst.*,  CTuser.Name+'='+CAST( patmst.MainDocId as nvarchar(50)) as MainDoc FROM  patmst LEFT OUTER JOIN " +
                             " CTuser ON patmst.MainDocId = CTuser.DRid where PID=@PID and patmst.branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
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
                    Patmst_Bal_C Obj_PBC = new Patmst_Bal_C();
                                     
                    if (sdr["PID"] != DBNull.Value)
                        Obj_PBC.PID = Convert.ToInt32(sdr["PID"]);


                    Obj_PBC.Patname = sdr["Patname"].ToString();
                    Obj_PBC.Drname = sdr["Drname"].ToString();
                    Obj_PBC.Initial = sdr["intial"].ToString();
                    Obj_PBC.Email = sdr["Email"].ToString();
                    Obj_PBC.Sex = sdr["sex"].ToString();
                    Obj_PBC.Phone = sdr["TelNo"].ToString();
                    Obj_PBC.Age = int.Parse(sdr["Age"].ToString());
                    Obj_PBC.MYD = sdr["MDY"].ToString();
                    if (sdr["Phrecdate"] != DBNull.Value)
                        Obj_PBC.Phrecdate = Convert.ToDateTime(sdr["Phrecdate"]);
                   
                    Obj_PBC.Tests = sdr["Tests"].ToString();
                    Obj_PBC.SampleType = sdr["SampleType"].ToString();
                    Obj_PBC.Pataddress = sdr["Pataddress"].ToString();

                    Obj_PBC.PatientcHistory = sdr["PatientcHistory"].ToString();
                   
                    Obj_PBC.DoctorCode = sdr["DoctorCode"].ToString();
                   
                    Obj_PBC.CenterCode = sdr["CenterCode"].ToString();
                    Obj_PBC.CenterName = sdr["CenterName"].ToString();
                    Obj_PBC.OtherRefDoctor = sdr["OtherRefDoctor"].ToString();

                    Obj_PBC.P_Weights = sdr["Weights"].ToString();
                    Obj_PBC.P_Heights = sdr["Heights"].ToString();
                    Obj_PBC.P_Disease = sdr["Disease"].ToString();
                    Obj_PBC.P_LastPeriod = sdr["LastPeriod"].ToString();
                    Obj_PBC.P_Symptoms = sdr["Symptoms"].ToString();
                    Obj_PBC.P_FSTime = sdr["FSTime"].ToString();
                    Obj_PBC.P_Therapy = sdr["Therapy"].ToString();
                    Obj_PBC.P_SocialMedia = Convert.ToInt32(sdr["SocialMedia"]);
                   // Obj_PBC.P_PatCard = sdr["PatientCardNo"].ToString();
                   // Obj_PBC.P_PatCardExp = sdr["PatientCardExpNo"].ToString();
                    //Obj_PBC.P_AccBirthdate = Convert.ToInt32(sdr["AccDateofBirth"]);
                    //if (sdr["DateOfBirth"] != DBNull.Value)
                    //    Obj_PBC.DateOfBirth = Convert.ToDateTime(sdr["DateOfBirth"]);

                    if (sdr["DOB"] != DBNull.Value)
                        Obj_PBC.DOB = sdr["DOB"].ToString();
                    else
                        Obj_PBC.DOB = Convert.ToString(sdr["PatDatOfBirth"]); ;
                   // Obj_PBC.Phone = sdr["Patphoneno"].ToString();
                    Obj_PBC.UploadPrescription = sdr["UploadPrescription"].ToString();
                    Obj_PBC.Remarks = sdr["Remark"].ToString();

                  
                    Obj_PBC.ImagePath = sdr["ImagePath"].ToString();

                    al.Add(Obj_PBC);
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
   

    public DataSet Get_PatientInfo(int PID, int branchid)
    {
     
        ArrayList al = new ArrayList();               

        SqlConnection conn = DataAccess.ConInitForDC();
        
        SqlCommand sc = new SqlCommand("Select * from patmst where PID=@PID and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        SqlDataAdapter da = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        SqlDataReader sdr = null;

        try
        {
            da.Fill(ds);
            int cnt = ds.Tables[0].Rows.Count;
            ds.Tables[0].Columns.Add("testname_m");
            ds.Tables[0].Columns.Add("testrate");
            string Maintestname = null;

            string testcd = Convert.ToString(ds.Tables[0].Rows[0].ItemArray[11]);
            string C_Code = Convert.ToString(ds.Tables[0].Rows[0].ItemArray[28]);

            string[] tcd = testcd.Split(',');
            for (int j = 0; j < tcd.Length; j++)
            {
                DataRow row1 = ds.Tables[0].NewRow();
                string tename = tcd[j].Trim();

                if (tename.Length == 4)
                {
                    Maintestname = Packagenew_Bal_C.getPNameByCode(tename, branchid);
                }
                else
                {
                    Maintestname = MainTestLog_Bal_C.GET_Maintest_name(tename, branchid);
                }
                DrMT_Bal_C drTable = new DrMT_Bal_C(C_Code, "CC", branchid);
               
                row1["Tests"] = tename;
                row1["testname_m"] = Maintestname;
              
                row1["testrate"] = Patmst_New_Bal_C.Get_TestRate_ForCode(PID, tename, branchid);
                ds.Tables[0].Rows.Add(row1);
            }
            ds.Tables[0].Rows[0].Delete();

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
        return ds;
    }

   
    public static void  UpdateStatusByLabForProfile(int PID, string PatRegID, string FID, DateTime regDate, string Patauthicante, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" UPDATE patmst SET  PatRegID=@PatRegID, FID=@FID," +
            "RegistratonDateTime=@RegistratonDateTime, SampleStatus=@SampleStatus where PID=@PID and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@RegistratonDateTime", SqlDbType.DateTime)).Value = regDate;
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@SampleStatus", SqlDbType.NVarChar, 50)).Value = Patauthicante;


        // Add the employee ID parameter and set its value.
        short cnt;
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
                conn.Close(); conn.Dispose();
            }
            catch
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
    }

    public static void EnterRejectStatus(string BarcodeID, string Patauthicante, string reason, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" UPDATE patmst SET  SampleStatus=@SampleStatus, Remark=@Remark where BarcodeID=@BarcodeID and branchid=" + branchid + "", conn);
        
        sc.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 200)).Value = reason;
        sc.Parameters.Add(new SqlParameter("@BarcodeID", SqlDbType.NVarChar, 50)).Value = BarcodeID;
        sc.Parameters.Add(new SqlParameter("@SampleStatus", SqlDbType.NVarChar, 50)).Value = Patauthicante;
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

  
  
    public static bool isLimitExceed(int branchid)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.ConnectionstringWithoutCheck);

        SqlCommand sc = new SqlCommand(" SELECT count(*) from patmst where branchid=" + branchid + "", conn);

        int cnt = 0;
        if (conn.State == ConnectionState.Open)
            conn.Close();
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
        if (cnt > 8000)
            return true;
        else
            return false;
    }

    public static string GetCenter_Labcode(string Pcode, int branchid)
    {
        try
        {
            string labname = "";

            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = null;

            sc = new SqlCommand("Select UnitCode from DrMT where DoctorCode='" + Pcode + "' and branchid=" + branchid + "", conn);

            try
            {
                conn.Open();
                object o = sc.ExecuteScalar();
                if (o != DBNull.Value)
                    labname = Convert.ToString(o);
                else
                    labname = "";
                
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
                    throw;
                }

            }

            return labname;
        }
        catch { return null; }
    }

  
    public static ICollection getPrintedSamplesData(DateTime dfrom, DateTime dto, int branchid, int DigModule)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from VW_patrecprtvw where VW_patrecprtvw.Patregdate >=@fromdate and VW_patrecprtvw.Patregdate <=@todate and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime)).Value = dfrom;
        sc.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime)).Value = dto;
       
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
                    Patmst_Bal_C Obj_PBC = new Patmst_Bal_C();
                    Obj_PBC.SrNo = Convert.ToInt32(sdr["SrNo"]);
                    Obj_PBC.PID = Convert.ToInt32(sdr["PID"]);
                    Obj_PBC.PatRegID = Convert.ToString(sdr["PatRegID"]);
                    Obj_PBC.FID = (string)sdr["FID"];

                    Obj_PBC.Patname = sdr["Patname"].ToString();
                    Obj_PBC.TestCharges = Convert.ToSingle(sdr["TestCharges"]);
                    Obj_PBC.Tests = sdr["test"].ToString();

                    string rno = Convert.ToString(sdr["PatRegID"]);
                    string FID = (string)sdr["FID"];
                    int totcnt = PatSt_new_Bal_C.GetTotalCount(rno, FID, branchid);
                    
                        al.Add(Obj_PBC);
                    
                }
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
        return al;
    } 

 
  
    public float getTotalRate(int PID, int branchid)
    {
       
        float totamt = 0f;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        sc = new SqlCommand("select TestCharges FROM patmst where PID=@PID and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;

        try
        {
            conn.Open();
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                totamt = 0f;
            else
                totamt = Convert.ToSingle(o);
        }
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
        //tl.Sort();
        return totamt;
    }//end gettestcode by Maintestname and subdeptName

  
    private static int DigModule;
    public static int P_maindeptid
    {
        get { return DigModule; }
        set { DigModule = value; }
    }

    public static string PatientCountBanner(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string sql = "";
        sql = sql + "Select count(*) from patmst where branchid=" + branchid + "";
       
       

        SqlCommand sc = new SqlCommand(sql, conn);

        string patientcount = "0";
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            patientcount = Convert.ToString(sc.ExecuteScalar());
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
        return patientcount;
    }

    public static string PatientCount(object DoctorCode, int branchid, object fromDate, object toDate, string subdeptName,object Status)
    {   
        SqlConnection conn = DataAccess.ConInitForDC();
        string sql = "";
        sql = sql + "Select count(*) from patmst where branchid=" + branchid + "";
        if (subdeptName != "" && subdeptName != null)
        {
            sql += " and PatRegID in(SELECT dbo.patmstd.PatRegID FROM dbo.patmstd INNER JOIN dbo.MainTest ON dbo.patmstd.MTCode = dbo.MainTest.MTCode INNER JOIN dbo.SubDepartment ON dbo.MainTest.SDCode = dbo.SubDepartment.SDCode where dbo.SubDepartment.SDCode='"+subdeptName+"') ";
        } 
        if (fromDate != null && toDate != null)
        {
            sql = sql + " and Phrecdate between '" + DateTimeConvesion.getDateFromString(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + DateTimeConvesion.getDateFromString(toDate.ToString()).ToString("dd/MMM/yyyy") + "'";
        }
        if (DoctorCode != null && DoctorCode.ToString() != "0" &&  DoctorCode.ToString() != "")
        {
            sql += " and CenterCode='" + DoctorCode.ToString() + "'";
        }
        if (Status != "" && Status != null)
        {
            if (Status != "Authorized")
            {
                sql += " and PatRegID in(SELECT dbo.PatSt.PatRegID FROM dbo.PatSt where dbo.PatSt.Patauthicante='" + Status + "') ";
            }
            else if (Status == "Printed")
            {
                sql += " and PatRegID in(SELECT dbo.PatSt.PatRegID FROM dbo.PatSt where dbo.PatSt.Patauthicante='" + Status + "' and dbo.PatSt.Patrepstatus=1) ";
            }
            else
            {
                sql += " and PatRegID in(SELECT dbo.PatSt.PatRegID FROM dbo.PatSt where dbo.PatSt.Patauthicante='" + Status + "' and dbo.PatSt.Patrepstatus=0) ";
            }
        } 
       
        SqlCommand sc = new SqlCommand(sql, conn);
        
        string patientcount = "0";
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            patientcount = Convert.ToString(sc.ExecuteScalar());
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
        return patientcount;
    }

  
    public DataSet Get_patmst_Edittest(string PateintName, string PatRegID, DateTime dfrom, DateTime dto, int branchid, string CenterName, string labcode_main, string FID, string Barcode, string Mno)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        string sql = "Select * from VW_patmstvwdt where IsActive =1 and branchid=" + branchid + "";//FID='" + FID + "' and 
        if (CenterName != "")
        {
            sql = sql + " and CenterName= N'" + CenterName + "'";
            if (labcode_main != null && labcode_main != "")
            {
                sql = sql + " and UnitCode= '" + labcode_main + "'";
            }
        }
        if (PatRegID != "")
        {
            sql = sql + " and PatRegID= '" + PatRegID + "'";
            if (labcode_main != null && labcode_main != "")
            {
                sql = sql + " and UnitCode= '" + labcode_main + "'";
            }
        }        
        else
        {
            if (PateintName != "")
            {
                sql = sql + " and Patname like N'%" + PateintName + "%'";
                if (labcode_main != null && labcode_main != "")
                {
                    sql = sql + " and UnitCode= '" + labcode_main + "'";
                }
            }

            else
            {
                if (dfrom != null && dto != null)
                {
                    //sql = sql + " and Phrecdate between '" + Convert.ToDateTime(dfrom.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(dto.ToString()).ToString("dd/MMM/yyyy") + "' ";
                 
                    if (labcode_main != null && labcode_main != "")
                    {
                        sql = sql + " and UnitCode= '" + labcode_main + "'";
                    }
                }
            }
        }
        sql = sql + " and Phrecdate between '" + Convert.ToDateTime(dfrom.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(dto.ToString()).ToString("dd/MMM/yyyy") + "' ";

        if (Mno != "")
        {
            sql += " and Patphoneno like '%" + Mno + "%'";
        }
        sql += " order by PID desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);        
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            da.Fill(ds);
        }
        catch (Exception exx)
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
        return ds;
       
    }

    public DataSet PatientTeststatus(object DoctorCode, object startDate, object endDate, object Patauthicante, string patientName, string Reg_no, int branchid, int DigModule, string subdeptName, string FID, string labcode_main, string Barcode, string Mno,string RefDoc,string TestName)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string sql = "Select * from VW_patrprtvwd where  PatRegID<>'' and branchid=" + branchid + "";//FID='" + FID + "' and

        if (Reg_no != "")
        {
            sql = sql + " and PatRegID='" + Reg_no + "'";
            if (labcode_main != null && labcode_main != "")
            {
                sql = sql + " and UnitCode='" + labcode_main + "'";

            }
        }
        else
        {
            if (patientName != "")
            {
                sql = sql + " and Patname like  N'%" + patientName + "%'";
                if (labcode_main != null && labcode_main != "")
                {
                    sql = sql + " and UnitCode='" + labcode_main + "'";

                }
            }
            else
            {
                if (Patauthicante != "All" && Patauthicante != "" && Patauthicante != null)
                {
                    if (Patauthicante == "Authorized")
                    {
                        sql += " and status='" + Patauthicante + "'";
                    }
                    if (Patauthicante == "Registered")
                    {
                        sql += " and status='" + Patauthicante + "' and PhlebotomistCollect=0 and SpecimanNo=0 and ISpheboAccept=0";
                    }
                    else if (Patauthicante == "Completed")
                    {
                        sql += "  and Patrepstatus=1";
                    }
                    else if (Patauthicante == "Dispatch")
                    {
                        sql += "  and ISCallPatient=1";
                    }
                    else if (Patauthicante == "SamCollect")
                    {
                        sql += "  and PhlebotomistCollect=1 and  ISpheboAccept=0";
                    }
                    else if (Patauthicante == "SamAccept")
                    {
                        sql += "  and ISpheboAccept=1";
                    }
                    else if (Patauthicante == "SamReject")
                    {
                        sql += "  and PhlebotomistCollect=2";
                    }
                    else
                    {
                        sql += " and status='" + Patauthicante + "' and Patrepstatus=0";
                    }
                }
                if (subdeptName != "")
                {
                    sql += " and SDCode='" + subdeptName + "'";
                }
                if (startDate != null && endDate != null)
                {
                    sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
                }
                if (labcode_main != null && labcode_main != "")
                {
                    sql = sql + " and UnitCode='" + labcode_main + "'";

                }
            }
        }
        if (DoctorCode != null && DoctorCode.ToString() != "0" && DoctorCode != "")
        {
            sql += " and CenterCode=N'" + DoctorCode.ToString() + "'";
        }
        if (Barcode != "")
        {
            sql += " AND BarcodeID ='" + Barcode + "' ";
        }
        if (Mno != "")
        {
            sql += " AND Patphoneno ='" + Mno + "' ";
        }
        if (RefDoc != null && RefDoc.ToString() != "0" && RefDoc != "")
        {
            sql += " and LTRIM(Drname) =N'" + RefDoc.Trim().ToString() + "'";
        }
        if (TestName != null && TestName.ToString() != "0" && TestName != "")
        {
            sql += " and Maintestname='" + TestName.Trim().ToString() + "'";
        }
        sql += " order by phrecdate desc";//CAST(PatRegID AS )
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();

        try
        {
            conn.Open();
            da.Fill(ds);
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
        return ds;
    } 


    public DataSet ReportDownload(object DoctorCode, object startDate, object endDate, object Patauthicante, string patientName, string Reg_no, int branchid, int DigModule, string subdeptName, string FID, string labcode_main, string Barcode,string Mno)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string sql = "Select * from VW_patrprtvwd where  PatRegID<>'' and branchid=" + branchid + "";//FID='" + FID + "' and
       
        if (Reg_no != "")
        {
            sql = sql + " and PatRegID='" + Reg_no + "'";
            if (labcode_main != null && labcode_main != "")
            {
                sql = sql + " and UnitCode='" + labcode_main + "'";

            }
        }
        else
        {
            if (patientName != "")
            {
                sql = sql + " and Patname like '%" + patientName + "%'";
                if (labcode_main != null && labcode_main != "")
                {
                    sql = sql + " and UnitCode='" + labcode_main + "'";

                }
            }
            else
            {
                if (Patauthicante != "All" && Patauthicante != "" && Patauthicante != null)
                {
                    if (Patauthicante != "Authorized")
                    {
                        sql += " and Patauthicante='" + Patauthicante + "'";
                    }
                    else if (Patauthicante == "Printed")
                    {
                        sql += "  and Patrepstatus=1";
                    }
                    else
                    {
                        sql += " and Patauthicante='" + Patauthicante + "' and Patrepstatus=0";
                    }
                }
                if (subdeptName != "")
                {
                    sql += " and SDCode='" + subdeptName + "'";
                }
                if (startDate != null && endDate != null)
                {
                    sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
                }
                if (labcode_main != null && labcode_main != "")
                {
                    sql = sql + " and UnitCode='" + labcode_main + "'";

                }
            }
        }
        if (DoctorCode != null && DoctorCode.ToString() != "0" && DoctorCode!="")
        {
            sql += " and CenterCode=N'" + DoctorCode.ToString() + "'";
        }
        if (Barcode != "")
        {
            sql += " AND BarcodeID ='" + Barcode + "' ";
        }
        if (Mno != "")
        {
            sql += " AND Patphoneno ='" + Mno + "' ";
        }
        sql += " order by PatRegID,OutStAmt desc";
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
       
        try
        {
            conn.Open();
            da.Fill(ds);
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
        return ds;
    }

    public DataSet ReportDownloadModify(object DoctorCode, object startDate, object endDate, object Patauthicante, string patientName, string Reg_no, int branchid, int DigModule, string subdeptName, string FID, string labcode_main, string Barcode, string Subdept, string RepStatus,string MNO)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string sql = "Select  PatRegID, Name, sex, DoctorCode, Age, MDY,''as PatientcHistory, Drname, CenterName, Patauthicante, Maintestname, CenterCode, Phrecdate, Patname, branchid, FID, "+
           " OutStAmt, SDCode, TestCharges, Patregdate, PPID, UnitCode, BarcodeID, Patrepstatus, AmtReceived, Discount, DoctorEmail, PatientEmail,LabRegMediPro,TelNo,Printedby , Monthlybill ,CreatedBy , Pataddress, EmailID, Remark, Email, OtherRefDoctor " +
         " from VW_patrprtvwd where PatRegID<>'' and branchid=" + branchid + " "; //FID='" + FID + "' and 


        if (Reg_no != "")
        {
            sql = sql + " and PatRegID='" + Reg_no + "'";
            if (labcode_main != null && labcode_main != "")
            {
                sql = sql + " and UnitCode='" + labcode_main + "'";

            }
        }
        else
        {
            if (patientName != "")
            {
                sql = sql + " and Name like N'%" + patientName + "%'";
                if (labcode_main != null && labcode_main != "")
                {
                    sql = sql + " and UnitCode='" + labcode_main + "'";

                }
            }
            else
            {
                if (Patauthicante != "All" && Patauthicante != "" && Patauthicante != null)
                {
                    sql += " and Patauthicante='" + Patauthicante + "'";
                }
                if (subdeptName != "")
                {
                    sql += " and SDCode='" + subdeptName + "'";
                }
                //if (startDate != null && endDate != null)
                //{
                //    sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
                //}
                if (labcode_main != null && labcode_main != "")
                {
                    sql = sql + " and UnitCode='" + labcode_main + "'";

                }
            }
        }
        if (startDate != null && endDate != null)
        {
            sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
        }
        if (DoctorCode != null && DoctorCode.ToString() != "0" && DoctorCode != "")
        {
            sql += " and CenterCode=N'" + DoctorCode.ToString() + "'";
        }
        if (Barcode != "")
        {
            sql += " AND BarcodeID ='" + Barcode + "' ";
        }
        if (Subdept != "")
        {
            sql += " AND SDCode in (" + Subdept + " )";
        }
        if (RepStatus == "1")
        {
            sql += " AND Patrepstatus ='" + RepStatus + "' ";
        }
        if (RepStatus == "0")
        {
            sql += " AND Patrepstatus ='" + RepStatus + "' ";
        }
        if (MNO != "")
        {
            // sql += " AND Patphoneno ='" + Mno + "' ";
            sql += " AND TelNo ='" + MNO + "' ";
        }
         // and SDCode in ('ul','XR','SU')
        sql += " order by patregdate desc";//CAST( PatRegID AS int)
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        
        try
        {
            conn.Open();
            da.Fill(ds);
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
        return ds;
    }

    public DataSet getpatientbillcanceled(string Cname, object startDate, object endDate, string PatRegID, int branchid, int DigModule, string collectioncode, string FID, string UnitCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        DataSet ds = new DataSet();

        string query = "SELECT * ,intial+' '+ FirstName as Fullname FROM         VW_csmst1vw INNER JOIN "+
               " SaleCancelDetails ON VW_csmst1vw.PID = SaleCancelDetails.PID where SaleCancelDetails.isactive=0 and VW_csmst1vw.IsbillBH=0 and VW_csmst1vw.Monthlybill=0  and VW_csmst1vw.IsActive=0  and VW_csmst1vw.PatRegID<>'' and VW_csmst1vw.branchid=" + branchid + "";//and FID='" + FID + "'
        if (UnitCode != null && UnitCode != "")
        {
            query += " and UnitCode='" + UnitCode + "'";

        }
        if (collectioncode != "" && collectioncode != null)
        {
            query += "and CenterCode=N'" + collectioncode + "'";

        }
        if (PatRegID != "" && PatRegID != null)
        {
            query += " and VW_csmst1vw.PatRegID='" + PatRegID + "'";
        }
        else
        {
            if (Cname != "All" && Cname != "")
            {
                query += " and CenterName=N'" + Cname + "'";
            }
            if (startDate != null && endDate != null)
            {
                query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
            }
        }
        query += "order by PatRegID desc";
        da = new SqlDataAdapter(query, conn);
        ds = new DataSet();
        try
        {
            conn.Open();
            da.Fill(ds);
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
        return ds;
    }

    public DataSet GetPatientInformationnew_Refundpay(string Cname, object startDate, object endDate, string PatRegID, int branchid, int DigModule, string collectioncode, string FID, string UnitCode, string Patname, string Mobno, string Centername)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        DataSet ds = new DataSet();

        string query = "SELECT   PID, PatRegID, FID, Patregdate, intial, FirstName,  sex, Age, MDY, RefDr, Tests, Phrecdate, Branchid, DoctorCode,  CenterCode,  " +
         " FinancialYearID, EmailID, Drname, CenterName, Username, Usertype,  PPID, testname, AmtPaid, Discount, UnitCode, TestCharges, balance,  " +
         " IsbillBH, BillNo, Patphoneno, IsActive, TaxPer, TaxAmount, FirstName as Fullname ,IsRefund from  VW_csmst1vw  where  (IsActive=0 or balance<0 ) and  IsbillBH=0 and Monthlybill=0   and IsFreeze=0  and VW_csmst1vw.PatRegID<>'' and branchid=" + branchid + "";//and FID='" + FID + "'
        if (Centername != null && Centername != "All")
        {
            query += " and CenterName=N'" + Centername + "'";

        }
        if (UnitCode != null && UnitCode != "")
        {
            query += " and UnitCode='" + UnitCode + "'";

        }
        if (Patname != null && Patname != "")
        {
            query += " and Patname like N'%" + Patname + "%'";

        }
        if (Mobno != null && Mobno != "")
        {
            query += " and Patphoneno='" + Mobno + "'";

        }
        if (collectioncode != "" && collectioncode != null)
        {
            query += "and CenterCode=N'" + collectioncode + "'";

        }
        if (PatRegID != "" && PatRegID != null)
        {
            query += " and VW_csmst1vw.PatRegID='" + PatRegID + "'";
        }
        else
        {
            if (Cname != "All" && Cname != "")
            {
                query += " and CenterName=N'" + Cname + "'";
            }
            if (startDate != null && endDate != null)
            {
                query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
            }
        }
        query += "order by VW_csmst1vw.PatRegID desc";
        da = new SqlDataAdapter(query, conn);
        ds = new DataSet();
        try
        {
            conn.Open();
            da.Fill(ds);
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
        return ds;
    }

    public DataTable GetPatientInformationnew_1(string Cname, object startDate, object endDate, string PatRegID, int branchid, int DigModule, string collectioncode, string FID, string UnitCode,string Patname,string Mobno,string Centername)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        DataTable ds = new DataTable();

        string query = "SELECT   PID, PatRegID, FID, Patregdate, intial, FirstName,  sex, Age, MDY, RefDr, Tests, Phrecdate, Branchid, DoctorCode,  CenterCode,  " +
         " FinancialYearID, EmailID, Drname, CenterName, Username, Usertype,  PPID, testname,isnull( AmtPaid,0) as AmtPaid, isnull(Discount,0)as Discount, UnitCode, isnull(TestCharges,0)as TestCharges, ROUND(isnull( balance,0),0) as balance,  " +
         " IsbillBH, BillNo, Patphoneno, IsActive, isnull(TaxPer,0) as TaxPer, isnull(TaxAmount,0) as TaxAmount, FirstName as Fullname,IsbillBH from  VW_csmst1vw  where  IsActive=1  and  VW_csmst1vw.PatRegID<>'' and branchid=" + branchid + "";//IsbillBH=0 and Monthlybill=0  and
        if (Centername != null && Centername != "All" && Centername != "")
        {
            //query += " and CenterName like N'%" + Centername + "%'";
            query += " and CenterName = N'" + Centername + "'";

        }
        if (UnitCode != null && UnitCode != "")
        {
            query += " and UnitCode='" + UnitCode + "'";

        }
        if (Patname != null && Patname != "")
        {
            query += " and FirstName like N'%" + Patname + "%'";
            
        }
        if (Mobno != null && Mobno != "")
        {
            query += " and Patphoneno='" + Mobno + "'";

        }
        if (collectioncode != "" && collectioncode != null)
        {
            query += "and CenterCode=N'" + collectioncode + "'";

        }
        if (PatRegID != "" && PatRegID != null)
        {
            query += " and VW_csmst1vw.PatRegID='" + PatRegID + "'";
        }
        else
        {
            //if (Cname != "All" && Cname != "")
            //{
            //    query += " and CenterName='" + Cname + "'";
            //}
            //if (startDate != null && endDate != null)
            //{
            //    query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
            //}
        }
        if (startDate != null && endDate != null)
        {
            query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
        }

        query += " union all SELECT   PID, PatRegID, FID, Patregdate, intial, FirstName,  sex, Age, MDY, RefDr, Tests, Phrecdate, Branchid, DoctorCode,  CenterCode,  " +
        " FinancialYearID, EmailID, Drname, CenterName, Username, Usertype,  PPID, testname,isnull(AmtPaid,0)*-1 as AmtPaid, isnull(Discount,0) as Discount, UnitCode, TestCharges, isnull( ROUND( balance,0),0) as balance,  " +
        " IsbillBH, BillNo, Patphoneno, IsActive, isnull(TaxPer,0)as TaxPer, isnull(TaxAmount,0)as TaxAmount, FirstName as Fullname,IsbillBH from  VW_csmst1vw  where    IsActive=0  and VW_csmst1vw.PatRegID<>'' and branchid=" + branchid + "";//IsbillBH=0 and Monthlybill=0  and
        if (Centername != null && Centername != "All" && Centername != "")
        {
           // query += " and CenterName like N'%" + Centername.Trim() + "%'";
            query += " and CenterName = N'" + Centername.Trim() + "'";

        }
        if (UnitCode != null && UnitCode != "")
        {
            query += " and UnitCode='" + UnitCode + "'";

        }
        if (Patname != null && Patname != "")
        {
            query += " and FirstName like N'%" + Patname + "%'";

        }
        if (Mobno != null && Mobno != "")
        {
            query += " and Patphoneno='" + Mobno + "'";

        }
        if (collectioncode != "" && collectioncode != null)
        {
            query += "and CenterCode=N'" + collectioncode + "'";

        }
        if (PatRegID != "" && PatRegID != null)
        {
            query += " and VW_csmst1vw.PatRegID='" + PatRegID + "'";
        }
        else
        {
            //if (Cname != "All" && Cname != "")
            //{
            //    query += " and CenterName='" + Cname + "'";
            //}
            //if (startDate != null && endDate != null)
            //{
            //    query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
            //}
        }
        if (startDate != null && endDate != null)
        {
            query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
        }
        query += "order by VW_csmst1vw.PID desc";
        da = new SqlDataAdapter(query, conn);
        ds = new DataTable();       
        try
        {
            conn.Open();
            da.Fill(ds);
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
        return ds;
    }

    public DataSet GetPatientInformationnew_2(string Cname, object startDate, object endDate, string PatRegID, int branchid, int DigModule, string collectioncode, string FID, string UnitCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        DataSet ds = new DataSet();

        string query = "SELECT   VW_csmst1vw.PID, VW_csmst1vw.PatRegID, VW_csmst1vw.FID, VW_csmst1vw.Patregdate, VW_csmst1vw.intial, " +
                  "  VW_csmst1vw.FirstName,  VW_csmst1vw.sex, VW_csmst1vw.Age, VW_csmst1vw.MDY, VW_csmst1vw.RefDr, " +
                  "  VW_csmst1vw.Tests, VW_csmst1vw.Phrecdate, VW_csmst1vw.Branchid, VW_csmst1vw.DoctorCode, " +
                  "  VW_csmst1vw.CenterCode,  VW_csmst1vw.FinancialYearID, VW_csmst1vw.EmailID, VW_csmst1vw.Drname, " +
                  "  VW_csmst1vw.CenterName, VW_csmst1vw.Username, VW_csmst1vw.Usertype,  VW_csmst1vw.PPID, " +
                  "  VW_csmst1vw.testname, VW_csmst1vw.AmtPaid, VW_csmst1vw.Discount, VW_csmst1vw.UnitCode, VW_csmst1vw.TestCharges, " +
                  "  ROUND( VW_csmst1vw.balance,0) as balance, VW_csmst1vw.IsbillBH, VW_csmst1vw.BillNo, VW_csmst1vw.Patphoneno, VW_csmst1vw.IsActive, " +
                  "  VW_csmst1vw.TaxPer, VW_csmst1vw.TaxAmount, VW_patstdatavw.Patauthicante,intial+' '+ FirstName as Fullname from  VW_csmst1vw LEFT OUTER JOIN VW_patstdatavw ON VW_csmst1vw.PID = VW_patstdatavw.PID AND VW_csmst1vw.PatRegID = VW_patstdatavw.PatRegID where  VW_patstdatavw.Patauthicante ='Registered'   and IsActive=1  and VW_csmst1vw.PatRegID<>'' and  dbo.FUN_GetTest_autho(VW_csmst1vw.PatRegID,VW_csmst1vw.PID)=0 and branchid=" + branchid + "";//and FID='" + FID + "'and IsbillBH=0 and Monthlybill=0
        if (UnitCode != null && UnitCode != "")
        {
            query += " and UnitCode='" + UnitCode + "'";

        }
        if (collectioncode != "" && collectioncode != null)
        {
            query += "and CenterCode='" + collectioncode + "'";

        }
        if (PatRegID != "" && PatRegID != null)
        {
            query += " and VW_csmst1vw.PatRegID='" + PatRegID + "'";
        }
       
        
            if (Cname != "All" && Cname != "")
            {
                query += " and CenterName='" + Cname + "'";
            }
            if (startDate != null && endDate != null)
            {
                query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
            }
        
        query += "order by VW_csmst1vw.PatRegID desc";
        da = new SqlDataAdapter(query, conn);
        ds = new DataSet();
        try
        {
            conn.Open();
            da.Fill(ds);
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
        return ds;
    }

 
 
    public static float Get_TestRate_ForCode(int PID, string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand();
        string[] tcd = MTCode.Trim().Split(',');
        float FinalTestRate = 0;
        float TestAmount = 0;
        conn.Open();
        try
        {
            for (int j = 0; j < tcd.Length; j++)
            {
                object obj = null;
                string tename = tcd[j].Trim();
                if (tename.Length != 4)
                {
                    cmd = new SqlCommand("select TestRate from patmstd where PID=" + PID + " and branchid=" + branchid + "and MTCode='" + MTCode + "'", conn);
                    obj = cmd.ExecuteScalar();
                }
                else
                {
                    cmd = new SqlCommand("select distinct TestRate as TestRate from patmstd where PID=" + PID + " and branchid=" + branchid + " and PackageCode='" + MTCode + "'", conn);
                    obj = cmd.ExecuteScalar();
                }
                try
                {
                    if (obj != null)
                    {
                        TestAmount = 0;
                        TestAmount = Convert.ToSingle(obj);
                        if (TestAmount > 0)
                        {
                            FinalTestRate += TestAmount;
                            TestAmount = 0;
                        }
                        TestAmount = 0;
                    }
                    else
                    {

                    }
                       
                }
                catch
                {
                    throw;
                }
            }
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
        return FinalTestRate;
    }

    public static float Get_ClientTestRate_ForCode(int PID, string MTCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand();
        string[] tcd = MTCode.Trim().Split(',');
        float FinalTestRate = 0;
        float TestAmount = 0;
        conn.Open();
        try
        {
            for (int j = 0; j < tcd.Length; j++)
            {
                object obj = null;
                string tename = tcd[j].Trim();
                if (tename.Length != 4)
                {
                    cmd = new SqlCommand("select ClientTestRate from patmstd where PID=" + PID + " and branchid=" + branchid + "and MTCode='" + MTCode + "'", conn);
                    obj = cmd.ExecuteScalar();
                }
                else
                {
                    cmd = new SqlCommand("select distinct ClientTestRate as TestRate from patmstd where PID=" + PID + " and branchid=" + branchid + " and PackageCode='" + MTCode + "'", conn);
                    obj = cmd.ExecuteScalar();
                }
                try
                {
                    if (obj != null)
                    {
                        TestAmount = 0;
                        TestAmount = Convert.ToSingle(obj);
                        if (TestAmount > 0)
                        {
                            FinalTestRate += TestAmount;
                            TestAmount = 0;
                        }
                        TestAmount = 0;
                    }
                    else
                    {

                    }

                }
                catch
                {
                    throw;
                }
            }
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
        return FinalTestRate;
    }


    public DataSet ReportDownload(object DoctorCode, object startDate, object endDate, object Patauthicante, string patientName, string Reg_no, int branchid, int DigModule, string subdeptName, string FID, string labcode_main, string RefDr_code, string Barcode, string Mno, string RepStatus)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string sql = "Select * from VW_patrprtvwd where PatRegID<>'' and branchid=" + branchid + "";// FID='" + FID + "' and


        if (Reg_no != "")
        {
            sql = sql + " and PatRegID='" + Reg_no + "'";
            if (labcode_main != null && labcode_main != "")
            {
                sql = sql + " and UnitCode='" + labcode_main + "'";

            }
        }
        else
        {
            if (patientName != "")
            {
                sql = sql + " and Name like N'%" + patientName + "'";
                if (labcode_main != null && labcode_main != "")
                {
                    sql = sql + " and UnitCode='" + labcode_main + "'";

                }
            }
            else
            {
                if (Patauthicante != "All" && Patauthicante != "" && Patauthicante != null)
                {
                    sql += " and Patauthicante='" + Patauthicante + "'";
                }
                if (subdeptName != "")
                {
                    sql += " and SDCode='" + subdeptName + "'";
                }
                //if (startDate != null && endDate != null)
                //{
                //    sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
                //}
                if (labcode_main != null && labcode_main != "")
                {
                    sql = sql + " and UnitCode='" + labcode_main + "'";

                }
            }
        }
        if (startDate != null && endDate != null)
        {
            sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
        }
        if (RefDr_code != "" && RefDr_code != null)
        {
            sql += " AND DoctorCode ='" + RefDr_code.Trim() + "' ";
        }
        //if (DoctorCode != null && DoctorCode.ToString() != "0" && DoctorCode != "") 
        //{
        //    sql += " and CenterCode=N'" + DoctorCode.ToString() + "'";
        //}
        if (Barcode != "" )
        {
            sql += " AND BarcodeID ='" + Barcode + "' ";
        }
        if (Mno != "")
        {
           // sql += " AND Patphoneno ='" + Mno + "' ";
            sql += " AND TelNo ='" + Mno + "' ";
        }
        if (RepStatus == "1")
        {
            sql += " AND Patrepstatus ='" + RepStatus + "' ";
        }
        if (RepStatus == "0")
        {
            sql += " AND Patrepstatus ='" + RepStatus + "' ";
        }
        sql += "  order by patregdate desc"; //CAST( PatRegID AS int)
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        
        try
        {
            conn.Open();
            da.Fill(ds);
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
                throw;
            }
        }
        return ds;
    } 

    public DataTable get_PAtientInformationWithoutStatus(object CenterCode, object UnitCode, object Patauthicante, int branchid, int DigModule, string FID, int i)
    {
        SqlDataAdapter da = new SqlDataAdapter();
        DataTable dt = new DataTable();
        SqlConnection conn = DataAccess.ConInitForDC();

        string sql = "";
        if (UnitCode != null)
        {
            sql = "   alter view VW_patrejdatavw as SELECT distinct dbo.patmst.PID, dbo.patmst.PatRegID, dbo.patmst.FID, dbo.patmst.intial, dbo.patmst.Patname, " +
                  "    dbo.patmst.sex, dbo.patmst.Age, dbo.patmst.MDY, dbo.patmst.EmailID, " +
                  "   dbo.BarM.BarcodeID, dbo.patmst.CenterCode, dbo.BarM.SampleType,  " +
                  "   dbo.patmst.branchid, dbo.patmst.Drname as DrName, dbo.patmst.SampleStatus ,BarM.STCODE , BarM.testnames" +
                  "   FROM  dbo.patmst INNER JOIN dbo.BarM ON dbo.patmst.PID = dbo.BarM.PID INNER JOIN " +
                  "   dbo.DrMT_Bal_C ON dbo.patmst.CenterCode = dbo.DrMT_Bal_C.DoctorCode " +
                  "   WHERE  (dbo.BarM.SampleStatus = 'rejected') and  DrMT_Bal_C.UnitCode='" + UnitCode.ToString().Trim() + "' and patmst.branchid=" + branchid + "";
        }
        else
        {
            sql = " alter view VW_patrejdatavw as SELECT distinct  patmst.PID, patmst.PatRegID, patmst.FID, patmst.intial, patmst.Patname,  " +
                  "  patmst.sex, patmst.Age, patmst.MDY, patmst.EmailID, BarM.BarcodeID, patmst.CenterCode, " +
                  "  BarM.SampleType, patmst.branchid, patmst.Drname as DrName ,BarM.STCODE , BarM.testnames " +
                  "  FROM   patmst INNER JOIN BarM ON patmst.PID = BarM.PID " +
                  "  WHERE  (BarM.SampleStatus = 'rejected')";
        }
        if (CenterCode != null)
        {
            sql = sql + " and " + "patmst.CenterCode=N'" + CenterCode.ToString() + "'";
        }
        if (FID != null)
        {
            sql = sql + " and " + "patmst.FID='" + FID.ToString() + "'";
        }
       
        SqlCommand sc = new SqlCommand(sql, conn);
        try
        {
            try
            {
                conn.Open();
                sc.ExecuteNonQuery();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
            try
            {
                SqlConnection conn1 = DataAccess.ConInitForDC();
                da = new SqlDataAdapter("select * from VW_patrejdatavw", conn1);
                conn1.Open();
                da.Fill(dt);
                conn1.Close(); conn1.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
        }
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
        return dt;
    }

    public DataSet Get_Phlebotomist(object DoctorCode, object startDate, object endDate, string patientName, string Reg_no, int branchid, string maindept, string FID, int Number, string username, string UserType, string Barcode, string Mobno, string NewBarcode,string PPID)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        string sql = "Select * from VW_testmgr where  branchid=" + branchid + ""; //FID='" + FID + "' and

        if (PPID != "")
        {
            sql = sql + " and ( PPID='" + PPID + "'  )";//or LabRegMediPro='" + Reg_no + "'  or Barcodeid='" + Reg_no + "'
        }
        else if (Reg_no != "")
        {
            sql = sql + " and (PatRegID='" + Reg_no + "'   )";//or LabRegMediPro='" + Reg_no + "'  or Barcodeid='" + Reg_no + "'
        }
        else
        {
            if (patientName != "")
            {
                sql = sql + " and Patname like  N'%" + patientName + "%'";
            }
            else
            {
                if (DoctorCode != "" && DoctorCode.ToString() != "0")
                {
                    sql += " and CenterCode=N'" + DoctorCode.ToString() + "'";
                }

            }

            if (startDate != null && endDate != null)
            {
                sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
            }
            if (NewBarcode != "")
            {
                sql += " and Barcodeid = '" + NewBarcode + "'";
            }
            if (Barcode != "All")
            {
                if (Barcode == "0")
                {
                    sql += " and PhlebotomistCollect in (0,2) ";
                }
                else
                {

                    sql += " and PhlebotomistCollect = '" + Barcode + "'";
                }
            }
            if (Mobno != "")
            {
                sql += " and Patphoneno = '" + Mobno + "' ";
            }
        }
        sql += " order by PID desc, SampleType desc ";//srno
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        try
        {
            
            conn.Open();
            da.Fill(ds);
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
        return ds;
    }

    public DataSet Get_Phlebotomist_MainDoc(string UnitCode, object startDate, object endDate, string patientName, string Reg_no, int branchid, string maindept, string FID, int Number, string username, string UserType, object DoctorCode, string Barcode, string NewBarcode,string PPID)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);

       

        string sql = " Select * from VW_testmgr_n where branchid=" + branchid + " and dbo.Fun_GetUserAuthorizedTests(dbo.VW_testmgr_n.STCODE,1,'" + username + "')<>'' ";//FID='" + FID + "' and 

        if (PPID != "")
        {
            sql = sql + " and ( PPID='" + PPID + "' )";
        }
        if (Reg_no != "")
        {
            sql = sql + " and ( PatRegID='" + Reg_no + "'  )";
        }
        else
        {
            if (patientName != "")
            {
                sql = sql + " and Patname like  N'%" + patientName + "%'";
            }
            else
            {
                if (DoctorCode != "" && DoctorCode.ToString() != "0")
                {
                    sql += " and CenterCode=N'" + DoctorCode.ToString() + "'";
                }
                if (startDate != null && endDate != null)
                {
                    sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
                }
            }
        }

        if (NewBarcode != "")
        {
            sql += " and Barcodeid = '" + NewBarcode + "'";
        }
        if (Barcode != "All")
        {
            sql += " and PhlebotomistCollect = '" + Barcode + "'";
        }
       
        if (UnitCode != "" && UnitCode != null)
        {
            sql = sql + " and UnitCode='"+  UnitCode  +"'";
        }
        sql += " order by PatRegID desc,SrNo";
        SqlDataAdapter da = new SqlDataAdapter(sql, conn);
        DataSet ds = new DataSet();
        try
        {
            conn.Open();
            da.Fill(ds);
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
        return ds;
    } 

    public static void AlterViewvw_GroupByLabcode_New(string usertype,string Username)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = conn.CreateCommand();

        if (usertype == "Main Doctor" || usertype == "Technician" || usertype == "Reporting")
        {
            
            sc.CommandText = "   Alter View VW_patlbvw as    SELECT     patmst.PatRegID, CAST(patmst.Tests AS varchar(255)) AS Test1, patmst.PID, patmst.FID, patmst.Patregdate, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, " +
             "  patmst.RefDr, patmst.Reportdate, patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.TestCharges, patmst.Username, patmst.SampleType, " +
              " patmst.SampleStatus, patmst.RegistratonDateTime, patmst.BarcodeID, patmst.Drname, patmst.MDY, CAST(patmst.PatientcHistory AS varchar(255)) AS cliniclahist, " +
              " patmst.Branchid, SubDepartment.DigModule, patmstd.UnitCode, patmst.Remark, patmst.Patphoneno, patmst.IsActive,  " +
              " Deptwiseuser.username AS DeptUserName ,patmst.LabRegMediPro,SubDepartment.subdeptname , patmstd.LabNo "+
              " FROM         patmst INNER JOIN " +
              " patmstd ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN " +
              " MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.Branchid = MainTest.Branchid INNER JOIN " +
              " SubDepartment ON MainTest.SDCode = SubDepartment.SDCode AND MainTest.Branchid = SubDepartment.Branchid INNER JOIN " +
              " PatSt ON patmst.PatRegID = PatSt.PatRegID AND patmst.FID = PatSt.FID AND patmstd.MTCode = PatSt.MTCode INNER JOIN " +
              " Deptwiseuser ON SubDepartment.subdeptid = Deptwiseuser.DeptCodeID " +
              " GROUP BY patmst.PatRegID, CAST(patmst.Tests AS varchar(255)), patmst.PID, patmst.FID, patmst.Patregdate, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, " +
              " patmst.RefDr, patmst.Reportdate, patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.TestCharges, patmst.Username, patmst.SampleType, " +
              " patmst.SampleStatus, patmst.RegistratonDateTime, patmst.BarcodeID, patmst.Drname, patmst.MDY, CAST(patmst.PatientcHistory AS varchar(255)), " +
              " patmst.Branchid, SubDepartment.DigModule, patmstd.UnitCode, patmst.Remark, patmst.Patphoneno, patmst.IsActive, SubDepartment.subdeptid, " +
              " Deptwiseuser.username  ,patmst.LabRegMediPro    ,SubDepartment.subdeptname   , patmstd.LabNo             having Deptwiseuser.username ='" + Username + "' ";
        }
        else
        {
            sc.CommandText = "Alter View VW_patlbvw as ( SELECT     patmst.PatRegID, CAST(patmst.Tests AS varchar(255)) AS Test1, "+
                          "  patmst.PID, patmst.FID, patmst.Patregdate, patmst.intial, patmst.Patname, "+ 
                          "  patmst.sex, patmst.Age, patmst.RefDr, "+
                          "  patmst.Reportdate,  patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, "+
                          "  patmst.TestCharges, patmst.Username, patmst.SampleType, patmst.SampleStatus, "+
                          "  patmst.RegistratonDateTime, patmst.BarcodeID, patmst.Drname, patmst.MDY, "+
                          "  CAST(patmst.PatientcHistory AS varchar(255)) AS cliniclahist, "+
                          "  patmst.Branchid, SubDepartment.DigModule, patmstd.UnitCode, patmst.Remark, "+
                          "  patmst.Patphoneno, patmst.IsActive,patmst.LabRegMediPro,SubDepartment.subdeptname, patmstd.LabNo " +
                          "  FROM         patmst INNER JOIN "+
                          "  patmstd ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN "+
                          "  MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.Branchid = MainTest.Branchid INNER JOIN "+
                          "  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode AND MainTest.Branchid = SubDepartment.Branchid INNER JOIN "+
                          "  PatSt ON patmst.PatRegID = PatSt.PatRegID AND patmst.FID = PatSt.FID AND  "+
                          "  patmstd.MTCode = PatSt.MTCode "+
                          "  GROUP BY patmst.PatRegID, CAST(patmst.Tests AS varchar(255)),  patmst.PID, "+
                          "  patmst.FID, patmst.Patregdate, patmst.intial, patmst.Patname, "+
                          "  patmst.sex, patmst.Age, patmst.RefDr,  patmst.Reportdate, "+
                          "  patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.TestCharges, "+
                          "  patmst.Username, patmst.SampleType, patmst.SampleStatus, patmst.RegistratonDateTime, patmst.BarcodeID, "+
                          "  patmst.Drname, patmst.MDY, CAST(patmst.PatientcHistory AS varchar(255)),  "+
                          "  patmst.Branchid, SubDepartment.DigModule, patmstd.UnitCode, "+
                          "  patmst.Remark, patmst.Patphoneno, patmst.IsActive,patmst.LabRegMediPro,SubDepartment.subdeptname , patmstd.LabNo" +
                            " )";
        }
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

       public DataTable GetTestsForFilm(int PID, int branchid, string deptcode, string username)
     {
         DataAccess data = new DataAccess();
         SqlConnection conn = data.ConInitForDC1();
         SqlCommand sc = new SqlCommand("SP_phdatafrm", conn);

         sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
         sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
         sc.Parameters.Add(new SqlParameter("@deptcode", SqlDbType.NVarChar, 20)).Value = deptcode;
         sc.Parameters.AddWithValue("@username", username);

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

   
   
     public DataSet GetPatientforBillBH(string Cname, object startDate, object endDate, string PatRegID, int branchid, int DigModule, string collectioncode, string FID, string UnitCode, string DoctorCode,int paid)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlDataAdapter da;
         DataSet ds = new DataSet();

         string query = "SELECT * from VW_csmst1vw where IsbillBH=1 and Monthlybill=0  and PatRegID<>'' and branchid=" + branchid + "";//and FID='" + FID + "'
         if (UnitCode != null && UnitCode != "")
         {
             query += " and UnitCode='" + UnitCode + "'";

         }
         if (DoctorCode != null && DoctorCode != "")
         {
             query += " and DoctorCode=N'" + DoctorCode.Trim() + "'";

         }
         if (collectioncode != "" && collectioncode != null)
         {
             query += "and CenterCode=N'" + collectioncode + "'";

         }
         if (paid > 0)
         {
             query += " and Amtpaid >0 ";
         }
         if (paid == 0)
         {
             query += " and Amtpaid =0 ";
         }
         if (PatRegID != "" && PatRegID != null)
         {
             query += " and PatRegID='" + PatRegID + "'";
         }       
         else
         {
             if (Cname != "All" && Cname != "")
             {
                 query += " and CenterName='" + Cname + "'";
             }
             if (startDate != null && endDate != null)
             {
                 query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         query += "order by PatRegID desc";
         da = new SqlDataAdapter(query, conn);
         ds = new DataSet();
         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;
     }

     public DataSet GetPatientforBillForIPH(string Cname, object startDate, object endDate, string PatRegID, int branchid, int DigModule, string collectioncode, string FID, string UnitCode, string DoctorCode)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlDataAdapter da;
         DataSet ds = new DataSet();

         string query = "SELECT * from VW_csmst1vw where IsbillBH=1 and Monthlybill=0  and FID='" + FID + "' and PatRegID<>'' and branchid=" + branchid + ""; //and balance>0
         if (UnitCode != null && UnitCode != "")
         {
             query += " and UnitCode='" + UnitCode + "'";

         }
         if (DoctorCode != null && DoctorCode != "")
         {
             query += " and DoctorCode=N'" + DoctorCode.Trim() + "'";

         }
         if (collectioncode != "" && collectioncode != null)
         {
             query += "and CenterCode=N'" + collectioncode + "'";

         }
         if (PatRegID != "" && PatRegID != null)
         {
             query += " and PatRegID='" + PatRegID + "'";
         }
         else
         {
             if (Cname != "All" && Cname != "")
             {
                 query += " and CenterName=N'" + Cname + "'";
             }
             if (startDate != null && endDate != null)
             {
                 query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         query += "order by PatRegID desc";
         da = new SqlDataAdapter(query, conn);
         ds = new DataSet();
         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;
     }

     public  void AlterVW_Getregisteredstatus(object startDate, object endDate)
     {
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         SqlCommand sc = conn.CreateCommand();


         sc.CommandText = "Alter View VW_patstdatavw as (select distinct Patauthicante,PID,PatRegID from  Patmstd where  Patauthicante='Registered' and Patregdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "')";
        
       
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
     public DataSet GetPatientforBillCollcode(string Cname, object startDate, object endDate, string PatRegID, int branchid, int DigModule, string collectioncode, string FID, string UnitCode, string DoctorCode, int paid)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlDataAdapter da;
         DataSet ds = new DataSet();

         string query = "SELECT * from VW_csmst1vw where Monthlybill=1  and PatRegID<>'' and branchid=" + branchid + "";//and FID='" + FID + "'
         if (UnitCode != null && UnitCode != "")
         {
             query += " and UnitCode='" + UnitCode + "'";

         }
         if (DoctorCode != null && DoctorCode != "")
         {
             query += " and DoctorCode=N'" + DoctorCode.Trim() + "'";

         }
         if (collectioncode != "" && collectioncode != null)
         {
             query += "and CenterCode=N'" + collectioncode + "'";

         }
         
         if (PatRegID != "" && PatRegID != null)
         {
             query += " and PatRegID='" + PatRegID + "'";
         }
         else
         {
             if (Cname != "All" && Cname != "")
             {
                 query += " and CenterName=N'" + Cname + "'";
             }
             if (startDate != null && endDate != null)
             {
                 query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         query += "order by PatRegID desc";
         da = new SqlDataAdapter(query, conn);
         ds = new DataSet();
         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;
     }

     public static void AlterView_VW_Countstatus(string PatRegID, object fromDate, object toDate)
     {
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         SqlCommand sc = conn.CreateCommand();

         if (PatRegID != "")
         {
             sc.CommandText = "Alter View VW_Countstatus as ( SELECT DISTINCT  case when patmstd.Patrepstatus=1 then 'Printed' else patmstd.Patauthicante end as Patauthicante, patmstd.PatRegID , patmstd.MTCode FROM         patmstd INNER JOIN SubDepartment ON patmstd.SDCode = SubDepartment.SDCode AND patmstd.branchid = SubDepartment.Branchid   where  (patmstd.PatRegID = '" + PatRegID + "')    )";//WHERE     (PatSt.PatRegID = '" + PatRegID + "') 

         }
         else
         {
             sc.CommandText = "Alter View VW_Countstatus as ( SELECT DISTINCT  case when patmstd.Patrepstatus=1 then 'Printed' else patmstd.Patauthicante end as Patauthicante, patmstd.PatRegID , patmstd.MTCode FROM         patmstd INNER JOIN SubDepartment ON patmstd.SDCode = SubDepartment.SDCode AND patmstd.branchid = SubDepartment.Branchid   where patmstd.Patregdate >='" + DateTimeConvesion.getDateFromString(fromDate.ToString()).AddDays(-11).ToString("dd/MMM/yyyy") + "' and patmstd.Patregdate <'" + DateTimeConvesion.getDateFromString(toDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "'     )";//WHERE     (PatSt.PatRegID = '" + PatRegID + "') 
         }
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

     public DataTable Get_Testresultentrystatus(object UnitCode, object DeptID, object fromDate, object toDate, string Patauthicante, string MTCode, string patientName, string regnoID, string vial, string CenterCode, string username, string usertype, int branchid, int maindept, int Number, string FID, string MNo)
     {
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         string sql = "SELECT * ,dbo.Get_Teststatus(VW_patlbvw.PID,VW_patlbvw.PatRegID,VW_patlbvw.Branchid,VW_patlbvw.FID)as Patauthicante from VW_patlbvw where BarcodeID <>''  and IsActive=1 and  branchid=" + branchid + ""; //and FID='" + FID + "'

         if (regnoID == "" && vial == "")
         {
             if (fromDate != null && toDate != null)
             {
                 sql = sql + " and " + "VW_patlbvw.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and VW_patlbvw.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(toDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         if (MTCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.Test1 like '%" + MTCode + "%'";
         }
         if (patientName != "")
         {
             sql = sql + " and " + " VW_patlbvw.Patname like '" + patientName + "%'";
         }
         if (regnoID != "")
         {
             sql = sql + " and " + " VW_patlbvw.PatRegID ='" + regnoID + "'";

         }
         if (CenterCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.CenterCode =N'" + CenterCode + "'";
         }
         if (vial != "")
         {
             sql = sql + " and " + " VW_patlbvw.BarcodeID like '%" + vial + "%'";
         }
         if (maindept != 0)
         {
             sql = sql + " and " + " VW_patlbvw.DigModule ='" + maindept + "'";
         }
         if (UnitCode != null)
         {
             sql = sql + " and " + " VW_patlbvw.UnitCode='" + UnitCode + "'";
         }
         if (usertype == "Main Doctor" || usertype == "Lab Technician")
         {
             if (username != "")
             {
                 sql = sql + " and " + " VW_patlbvw.UserDeptUsername='" + username + "'";
             }
         }
         if (MNo != "")
         {
             sql = sql + " and " + " VW_patlbvw.Patphoneno like '%" + MNo + "%'";
         }
         sql = sql + " order by VW_patlbvw.PatRegID desc";
         SqlDataAdapter da = new SqlDataAdapter(sql, conn);
         DataTable ds = new DataTable();
         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;
     }

     public static DataTable GetPatmstForTeamL_new_11(object UnitCode, object DeptID, object fromDate, object toDate, string Patauthicante, string MTCode, string patientName, string regnoID, string BarID, string CenterCode, string username, string usertype, int branchid, int maindept, int Number, string testname, string MNo)
     {
       
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         string sql = "SELECT distinct PatRegID, Test1, PID, FID, Patregdate, intial, Patname, sex, Age, RefDr, Reportdate, DoctorCode, CenterCode, "+
           " FinancialYearID, TestCharges, Username, SampleType,  "+
           " SampleStatus, RegistratonDateTime, BarcodeID, Drname, MDY, cliniclahist, Branchid, DigModule, UnitCode, Remark, "+
           " Patphoneno, IsActive, LabRegMediPro ,CenterCode as CenterCode,intial +' '+Patname as FullName,cast( Age as nvarchar(50)) +' '+MDY as MYD,Drname as DrName, dbo.Get_Teststatus(VW_patlbvw.PID,VW_patlbvw.PatRegID,VW_patlbvw.Branchid,VW_patlbvw.FID)as   SampleStatusNew,''as TestName,Remark as p_remark,''as Maintestname from VW_patlbvw where BarcodeID <>''  and IsActive=1 and  branchid=" + branchid + ""; //and FID='" + FID + "'

         if (regnoID == "" && BarID == "")
         {
             if (fromDate != null && toDate != null)
             {
                 sql = sql + " and " + "VW_patlbvw.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and VW_patlbvw.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(toDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         if (MTCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.Test1 like '%" + MTCode + "%'";
         }
         if (patientName != "")
         {
             sql = sql + " and " + " VW_patlbvw.Patname like N'%" + patientName + "%'";
         }
         if (regnoID != "")
         {
             sql = sql + " and " + " (VW_patlbvw.PatRegID ='" + regnoID + "'  or LabRegMedipro  ='" + regnoID + "') ";

         }
         if (CenterCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.LabNo ='" + CenterCode + "'";
         }
         if (BarID != "")
         {
             sql = sql + " and " + " VW_patlbvw.BarcodeID like '%" + BarID + "%'";
         }
         if (maindept != 0)
         {
             sql = sql + " and " + " VW_patlbvw.DigModule ='" + maindept + "'";
         }
         if (UnitCode != null)
         {
             sql = sql + " and " + " VW_patlbvw.UnitCode='" + UnitCode + "'";
         }
         if (usertype == "Main Doctor" || usertype == "MainDoctor" || usertype == "Technician" || usertype == "Reporting")
         {
             if (username != "")
             {
                // sql = sql + " and " + " VW_patlbvw.Username='" + username + "'";
                 sql = sql + " and " + " VW_patlbvw.DeptUserName='" + username + "'";
                 
             }
         }
         if (MNo != "")
         {
             sql = sql + " and " + " VW_patlbvw.RefDr like '%" + MNo + "%'";
         }
         if (testname != "")
         {
             sql = sql + " and " + " VW_patlbvw.subdeptname = '" + testname + "'";
         }
         if (Patauthicante != "All")
         {
             sql = sql + " and " + " dbo.Get_Teststatus(VW_patlbvw.PID,VW_patlbvw.PatRegID,VW_patlbvw.Branchid,VW_patlbvw.FID) ='" + Patauthicante + "'";
         }
         sql = sql + " order by VW_patlbvw.PatRegID desc";
         SqlDataAdapter da = new SqlDataAdapter(sql, conn);
         DataTable ds = new DataTable();
         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;
         
     }

     public static string PatientCountBanner_result(int branchid)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         string sql = "";
         sql = sql + "Select count(*) from patmstd where branchid=" + branchid + "";



         SqlCommand sc = new SqlCommand(sql, conn);

         string patientcount = "0";
         SqlDataReader sdr = null;

         try
         {
             conn.Open();
             patientcount = Convert.ToString(sc.ExecuteScalar());
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
         return patientcount;
     }

     public DataTable GetPatientforBill_Saleregister(string Cname, object startDate, object endDate, string PatRegID, int branchid, int DigModule, string Patname, string UserName, string UnitCode, string DoctorCode, int paid)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlDataAdapter da;
         DataTable ds = new DataTable();

         string query = "SELECT     PID, PatRegID, FID, Patregdate, intial, FirstName, sex, Age, FName, MDY, RefDr, Tests, Phrecdate, Branchid, DoctorCode, CenterCode, FinancialYearID, EmailID, " +
                       " Drname, CenterName, Username, Usertype, PPID, testname, isnull(AmtPaid,0)as AmtPaid, isnull(Discount,0)as Discount, UnitCode, TestCharges, isnull(balance,0)as balance, "+
                       " IsbillBH, BillNo, Patphoneno, IsActive, isnull(Taxper,0)as Taxper, isnull(TaxAmount,0)as TaxAmount,  " +
                       "  Monthlybill, IsFreeze, TestCharges - isnull(Discount,0) AS Taxable, "+
                       " ROUND(TestCharges - isnull(Discount,0) + isnull(TaxAmount,0), 0) AS NetAmount from VW_csmst1vw where  PatRegID<>'' and branchid=" + branchid + "";//and FID='" + FID + "'
         if (UnitCode != null && UnitCode != "")
         {
             query += " and UnitCode='" + UnitCode + "'"; 

         }
         if (DoctorCode != null && DoctorCode != "All")
         {
             query += " and Drname='" + DoctorCode.Trim() + "'";

         }
         if (Patname != "" && Patname != null)
         {
             query += "and FName='" + Patname + "'";

         }
         if (UserName != "" && UserName != "Select UserName")
         {
             query += "and Username='" + UserName + "'";

         }

         if (PatRegID != "" && PatRegID != null)
         {
             query += " and PatRegID='" + PatRegID + "'";
         }
         else
         {
            
             if (startDate != null && endDate != null)
             {
                 query = query + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         query += "order by billno desc";
         da = new SqlDataAdapter(query, conn);
         ds = new DataTable();
         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;
     }
     public DataSet GetPatientforBill_CancelReport(string Cname, object startDate, object endDate, string PatRegID, int branchid, int DigModule, string collectioncode, string FID, string UnitCode, string DoctorCode, int paid)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlDataAdapter da;
         DataSet ds = new DataSet();

        // string query = "SELECT *,Testcharges-Discount as Taxable ,round((Testcharges-Discount)+TaxAmount,0) as NetAmount from VW_csmst1vw where  PatRegID<>'' and IsActive=0 and branchid=" + branchid + "";//and FID='" + FID + "'
         string query = "SELECT     SaleCancelDetails.CancelReceiptNo, SaleCancelDetails.PID, SaleCancelDetails.BillNo, SaleCancelDetails.billdate, SaleCancelDetails.AmtPaid, "+
              "  SaleCancelDetails.PaymentType, SaleCancelDetails.BankName, SaleCancelDetails.branchid, SaleCancelDetails.transdate, SaleCancelDetails.username, "+
              "  SaleCancelDetails.BillAmt, SaleCancelDetails.DisAmt, SaleCancelDetails.BalAmt, SaleCancelDetails.tdate, SaleCancelDetails.PrevBal, SaleCancelDetails.IsActive, "+
              "  SaleCancelDetails.TaxPer, SaleCancelDetails.TaxAmount, SaleCancelDetails.PrintCount, patmst.PatRegID, patmst.intial, patmst.Patname, "+
              "  patmst.sex, "+
              "  patmst.intial+' '+ patmst.Patname as PatientName,  "+
              "  patmst.CenterCode, patmst.Drname, patmst.CenterName, "+
              "  BillAmt-DisAmt as Taxable ,round((BillAmt-DisAmt)+TaxAmount,0) as NetAmount  "+
              "  FROM         SaleCancelDetails INNER JOIN "+
              "  patmst ON SaleCancelDetails.PID = patmst.PID where  PatRegID<>''  and SaleCancelDetails.branchid=" + branchid + "";//and FID='" + FID + "'
      

         if (PatRegID != "" && PatRegID != null)
         {
             query += " and patmst.PatRegID='" + PatRegID + "'";
         }
         else
         {
             if (Cname != "All" && Cname != "")
             {
                 query += " and CenterName=N'" + Cname + "'";
             }
             if (startDate != null && endDate != null)
             {
                 query = query + " and SaleCancelDetails.billdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).AddDays(+1).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         query += "order by PatRegID desc";
         da = new SqlDataAdapter(query, conn);
         ds = new DataSet();
         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;
     }

     public DataSet GetPatientfor_servicewise_saleReport(string Cname, object startDate, object endDate, string PatRegID, int branchid, int DigModule, string collectioncode, string FID, string UnitCode, string DoctorCode, int paid, string Maintestname)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlDataAdapter da;
         DataSet ds = new DataSet();

        

         //string query = "SELECT DISTINCT "+
         //              " COUNT(patmstd.MTCode) AS TestCode, "+
         //              " MainTest.Maintestname, SUM(patmstd.TestRate) AS amount, "+
         //              " SUM(patmstd.TestRate)-  dbo.get_discountwiseService_amt(patmstd.MTCode) AS Taxable, "+
         //              " (SUM(patmstd.TestRate)-  dbo.get_discountwiseService_amt(patmstd.MTCode)) * RecM.TaxPer / 100 AS Tax, " +
         //              " patmstd.MTCode,dbo.get_discountwiseService_amt(patmstd.MTCode) as Discount "+
         //              " ,SUM(patmstd.TestRate)-  dbo.get_discountwiseService_amt(patmstd.MTCode)+(SUM(patmstd.TestRate)-  dbo.get_discountwiseService_amt(patmstd.MTCode)) * RecM.TaxPer / 100 as Net " +
         //              " FROM         RecM INNER JOIN " +
         //              " patmstd ON  RecM.PID = patmstd.PID INNER JOIN " +
         //              " MainTest ON patmstd.MTCode = MainTest.MTCode "+
         //             "   where patmstd.isactive=1 and RecM.branchid=" + branchid + "";//
         string query = "SELECT DISTINCT " +
                      " COUNT(patmstd.MTCode) AS TestCode, " +
                      " MainTest.Maintestname, SUM(patmstd.TestRate) AS amount, " +
                      " SUM(patmstd.TestRate)- 0 AS Taxable, " +
                      " (SUM(patmstd.TestRate)-  0 ) * RecM.TaxPer / 100 AS Tax, " +
                      " patmstd.MTCode,0 as Discount " +
                      " ,SUM(patmstd.TestRate)-  0 +(SUM(patmstd.TestRate)- 0) * RecM.TaxPer / 100 as Net " +
                      " FROM         RecM INNER JOIN " +
                      " patmstd ON  RecM.PID = patmstd.PID INNER JOIN " +
                      " MainTest ON patmstd.MTCode = MainTest.MTCode " +
                     "   where patmstd.isactive=1 and RecM.branchid=" + branchid + "";//
         if (UnitCode != null && UnitCode != "")
         {
             query += " and UnitCode='" + UnitCode + "'";

         }
         if (DoctorCode != null && DoctorCode != "")
         {
             query += " and DoctorCode='" + DoctorCode.Trim() + "'";

         }
         if (Maintestname != "" && Maintestname != null)
         {
             query += "and Maintestname='" + Maintestname + "'";

         }
         if (collectioncode != "" && collectioncode != null)
         {
             query += "and CenterCode=N'" + collectioncode + "'";

         }

         if (PatRegID != "" && PatRegID != null)
         {
             query += " and patmstd.PatRegID='" + PatRegID + "'";
         }
         else
         {
             if (Cname != "All" && Cname != "")
             {
                 query += " and CenterCode=N'" + Cname + "'";
             }
             if (startDate != null && endDate != null)
             {
                 query = query + " and (CAST(CAST(YEAR(RecM.transdate) AS varchar(4)) + '/' + CAST(MONTH(RecM.transdate) AS varchar(2)) + '/' + CAST(DAY(RecM.transdate) AS varchar(2)) AS datetime)) between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         query += " GROUP BY  patmstd.isactive, MainTest.Maintestname, RecM.TaxPer, patmstd.MTCode order by MainTest.Maintestname ";
         da = new SqlDataAdapter(query, conn);
         ds = new DataSet();
         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;
     }
     public static string Get_Regno(string FID, int branchid,string PID)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlCommand sc = null;
         string s = "";

         try
         {

             conn.Open();
             sc = new SqlCommand("select isnull(cast(PatRegID as int),0) from patmst where  PID=@PID and FID=@FID  and branchid=" + branchid + "", conn);
             sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
             sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.NVarChar, 50)).Value = PID;

             object o = sc.ExecuteScalar();
             int i = Convert.ToInt32(o);
            // i++;
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
     public DataTable Get_subdept(string username)
     {
         SqlConnection con = DataAccess.ConInitForDC();
         SqlDataAdapter da = new SqlDataAdapter(" SELECT Deptwiseuser.branchid, "+
              "  subdept = STUFF( "+
              "  (SELECT  ',' +''''+ SubDepartment.SDCode+'''' FROM Deptwiseuser INNER JOIN "+
              "  SubDepartment ON Deptwiseuser.DeptCodeID = SubDepartment.subdeptid where Deptwiseuser.username = '"+username+"' "+
              "  FOR XML PATH ('')), 1, 1, '' "+
              "  )  "+
              "  FROM Deptwiseuser INNER JOIN "+
              "  SubDepartment ON Deptwiseuser.DeptCodeID = SubDepartment.subdeptid "+
              "  GROUP BY Deptwiseuser.branchid,Deptwiseuser.username "+
              "  having (Deptwiseuser.username = '" + username + "')", con);
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
             con.Close();
             con.Dispose();
         }
         return ds;
     }

     public static void AlterViewvw_GroupByLabcode_New_testwise(string usertype, string Username, object startDate, object endDate, string patientName, string regnoID, string Barid, string CenterCode, string username, int branchid, int maindept, int Number, string testname, string MNo, string CenterCodeNew, int MainDocId)
     {
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         SqlCommand sc = conn.CreateCommand();

         if (usertype == "Main Doctor" || usertype == "Technician" || usertype == "Reporting")
         {
            
             sc.CommandText = "   Alter View VW_patlbvw as   SELECT     patmst.PatRegID, CAST(patmstd.BarcodeID AS varchar(255)) AS Test1, patmst.PID, patmst.FID, patmst.Patregdate, patmst.intial, "+
                 "   patmst.Patname, patmst.sex,   patmst.Age, patmst.RefDr, patmst.Reportdate, patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.TestCharges, patmst.Username,  "+
                 "   patmst.SampleType,   patmst.SampleStatus, patmst.RegistratonDateTime,  patmst.Drname, patmst.MDY,  CAST(patmst.PatientcHistory AS varchar(255)) AS cliniclahist,  "+
                 "   patmst.Branchid, SubDepartment.DigModule, patmstd.UnitCode, patmst.Remark, patmst.Patphoneno,   patmst.IsActive, patmst.LabRegMediPro, SubDepartment.subdeptName,  "+
                 "   MainTest.Maintestname, patmstd.Labno, patmstd.MTCode, patmstd.Patauthicante, patmstd.ISReRun,  patmstd.ReRunRemark, patmstd.MachinName, patmst.Isemergency,  "+
                 "   patmstd.PatientEmail ,patmstd.IspheboAccept, patmst.PPID,   case when InterfaceStatus is null then 'No' else 'Yes' end as InterfaceStatus ,patmstd.DoctorEmail  , "+  
                 "   ISNULL(patmstd.SpecimanNo,0) as SpecimanNo,patmstd.BarcodeId,case when  ISNULL(patmstd.PanicResult,0) =0  then 'NA' else 'Panic' end  as PanicResult ,   "+
                 "   patmstd.PanicRemark,patmstd.PanicInformToResult,patmstd.PanicAction,patmstd.InformUserName,Patmstd.UploadPrescription,patmstd.Isoutsource ,patmstd.OutsourcePatientPID,  "+
                 "   patmstd.OutResTransfer, isnull( patmstd.Technicanby,'')as Technicanby, (sum(BillAmt)-(sum(isnull(AmtPaid,0))+ sum(isnull(DisAmt,0)))) as Balance   , "+
                 "   round( 100- 100*(sum(ISNULL(AmtPaid, 0)) + sum(ISNULL(DisAmt, 0)))/NULLIF(sum(ISNULL(BillAmt,0)),0),0) as PendingPer,replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) as DailyseqNo,Patmst.RepType " +
                 "   FROM            patmst INNER JOIN "+
                 "   patmstd ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN "+
                 "   MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.Branchid = MainTest.Branchid INNER JOIN "+
                 "   SubDepartment ON MainTest.SDCode = SubDepartment.SDCode AND MainTest.Branchid = SubDepartment.Branchid INNER JOIN "+
                 "   RecM ON patmst.PID = RecM.PID INNER JOIN "+
                 "   Deptwiseuser ON SubDepartment.Branchid = Deptwiseuser.branchid and SubDepartment.subdeptid = Deptwiseuser.DeptCodeID  "+
                " where (patmst.IsActive = 1) and patmstd.TestDeActive=0   ";//and patmst.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and patmst.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(endDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "'

             if (patientName != "")
             {
                 sc.CommandText = sc.CommandText + " and " + " patmst.Patname like N'%" + patientName + "%'";
             }
             if (regnoID != "")
             {
                 sc.CommandText = sc.CommandText + " and " + " (patmst.PatRegID ='" + regnoID + "' ) ";

             }
             //if (CenterCode != "")
             //{
             //    sc.CommandText = sc.CommandText + " and " + " patmstd.LabNo ='" + CenterCode + "'";
             //}
             if (CenterCodeNew != "")
             {
                 sc.CommandText = sc.CommandText + " and " + " patmst.CenterCode =N'" + CenterCodeNew + "'";
             }
             if (Barid != "")
             {
                 sc.CommandText = sc.CommandText + " and " + " patmstd.BarcodeID like '%" + Barid + "%'";
             }
             //if (maindept != 0)
             //{
             //    sc.CommandText = sc.CommandText + " and " + " SubDepartment.DigModule ='" + maindept + "'";
             //}

             if (usertype == "Main Doctor" || usertype == "MainDoctor" || usertype == "Technician" || usertype == "Reporting")
             {
                 if (username != "")
                 {
                     // sql = sql + " and " + " VW_patlbvw.Username='" + username + "'";
                     sc.CommandText = sc.CommandText + " and " + "  Deptwiseuser.username=N'" + username + "'";

                 }
                 if (usertype == "Main Doctor")
                 {
                     if (MainDocId != 0)
                     {
                         // sql = sql + " and " + " VW_patlbvw.Username='" + username + "'";
                         sc.CommandText = sc.CommandText + " and " + "  Patmst.MainDocId=" + MainDocId + "";

                     }
                 }
             }
             if (MNo != "")
             {
                 sc.CommandText = sc.CommandText + " and " + " patmst.RefDr like '%" + MNo + "%'";
             }
             if (testname != "")
             {
                 sc.CommandText = sc.CommandText + " and " + " SubDepartment.subdeptName = '" + testname + "'";
             }

             sc.CommandText = sc.CommandText + " GROUP BY patmst.IsActive  , patmstd.TestDeActive  , patmst.PatRegID, CAST(patmst.Tests AS varchar(255)), patmst.PID, patmst.FID, patmst.Patregdate, patmst.intial,  "+
                 "   patmst.Patname, patmst.sex, patmst.Age,    patmst.RefDr, patmst.Reportdate, patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.TestCharges,  "+
                 "   patmst.Username, patmst.SampleType,     patmst.SampleStatus, patmst.RegistratonDateTime,  patmst.Drname, patmst.MDY, CAST(patmst.PatientcHistory AS varchar(255)), "+
                 "   patmst.Branchid,     SubDepartment.DigModule, patmstd.UnitCode, patmst.Remark, patmst.Patphoneno, patmst.IsActive, patmst.LabRegMediPro, "+
                 "   SubDepartment.subdeptName,    patmstd.BarcodeID, MainTest.Maintestname, patmstd.Labno, patmstd.MTCode, patmstd.Patauthicante, patmstd.ISReRun, "+
                 "   patmstd.ReRunRemark, patmstd.MachinName,     patmst.Isemergency, patmstd.PatientEmail ,patmstd.IspheboAccept, patmst.PPID,InterfaceStatus, "+
                 "   patmstd.DoctorEmail ,ISNULL(patmstd.SpecimanNo,0),patmstd.BarcodeId ,   ISNULL(patmstd.PanicResult,0) ,patmstd.PanicRemark,patmstd.PanicInformToResult, "+
                 "   patmstd.PanicAction,patmstd.InformUserName,Patmstd.UploadPrescription ,patmstd.Isoutsource   ,patmstd.OutsourcePatientPID,patmstd.OutResTransfer , " +
                 "   isnull( patmstd.Technicanby,''),replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) ,Patmst.RepType    ";
           //  " having  patmst.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and patmst.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(endDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "' and  Deptwiseuser.username ='" + Username + "'  ";
       
         }
         else
         {
             sc.CommandText = "Alter View VW_patlbvw as ( SELECT     patmst.PatRegID, CAST(patmstd.BarcodeID AS varchar(255)) AS Test1, patmst.PID, patmst.FID, patmst.Patregdate, patmst.intial, patmst.Patname, patmst.sex,   patmst.Age, patmst.RefDr, patmst.Reportdate, patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.TestCharges, patmst.Username,   patmst.SampleType, "+
                  "  patmst.SampleStatus, patmst.RegistratonDateTime,  patmst.Drname, patmst.MDY,  CAST(patmst.PatientcHistory AS varchar(255)) AS cliniclahist, patmst.Branchid, SubDepartment.DigModule, patmstd.UnitCode, patmst.Remark, patmst.Patphoneno,   patmst.IsActive, patmst.LabRegMediPro, SubDepartment.subdeptName, MainTest.Maintestname, patmstd.Labno, patmstd.MTCode, patmstd.Patauthicante, patmstd.ISReRun,  patmstd.ReRunRemark, patmstd.MachinName, patmst.Isemergency, patmstd.PatientEmail ,patmstd.IspheboAccept, patmst.PPID, "+
                  "  case when InterfaceStatus is null then 'No' else 'Yes' end as InterfaceStatus ,patmstd.DoctorEmail  , "+
                  "  ISNULL(patmstd.SpecimanNo,0) as SpecimanNo,patmstd.BarcodeId,case when  ISNULL(patmstd.PanicResult,0) =0  then 'NA' else 'Panic' end  as PanicResult , "+
                  "  patmstd.PanicRemark,patmstd.PanicInformToResult,patmstd.PanicAction,patmstd.InformUserName,Patmstd.UploadPrescription,patmstd.Isoutsource ,patmstd.OutsourcePatientPID, " +
                  "  patmstd.OutResTransfer, isnull( patmstd.Technicanby,'')as Technicanby, (sum(BillAmt)-(sum(isnull(AmtPaid,0))+ sum(isnull(DisAmt,0)))) as Balance "+
                  "  ,round( 100- 100*(sum(ISNULL(AmtPaid, 0)) + sum(ISNULL(DisAmt, 0)))/NULLIF(sum(ISNULL(BillAmt,0)),0),0) as PendingPer, replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) as DailyseqNo,Patmst.RepType " +
                  "  FROM            patmst INNER JOIN  patmstd ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN  MainTest ON patmstd.MTCode = MainTest.MTCode "+
                  "  AND patmstd.Branchid = MainTest.Branchid INNER JOIN  SubDepartment ON MainTest.SDCode = SubDepartment.SDCode AND MainTest.Branchid = SubDepartment.Branchid INNER JOIN  "+
                  "  recm ON patmst.PID = recm.PID  "+
                 //  " where Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "'  " +
                 " where (patmst.IsActive = 1) and patmstd.TestDeActive=0   ";//and patmst.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and patmst.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(endDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "'

             if (regnoID != "")
             {
                 sc.CommandText = sc.CommandText + " and " + " (patmst.PatRegID ='" + regnoID + "'  ) ";

             }
             else  if (Barid != "")
             {
             }
             else
             {
                // sc.CommandText = sc.CommandText + " and " + " patmst.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and patmst.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(endDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "' ";
                 if (patientName != "")
                 {
                     sc.CommandText = sc.CommandText + " and " + " patmst.Patname like N'%" + patientName.Trim() + "%'";
                 }

                 //if (CenterCode != "")
                 //{
                 //    sc.CommandText = sc.CommandText + " and " + " patmstd.LabNo ='" + CenterCode + "'";
                 //}
                 if (CenterCodeNew != "")
                 {
                     sc.CommandText = sc.CommandText + " and " + " patmst.CenterCode =N'" + CenterCodeNew + "'";
                 }
                 if (Barid != "")
                 {
                     sc.CommandText = sc.CommandText + " and " + " patmstd.BarcodeID like '%" + Barid + "%'";
                 }
                 if (maindept != 0)
                 {
                     sc.CommandText = sc.CommandText + " and " + " SubDepartment.DigModule ='" + maindept + "'";
                 }

                 if (usertype == "Main Doctor" || usertype == "MainDoctor" || usertype == "Technician" || usertype == "Reporting")
                 {
                     if (username != "")
                     {
                         // sql = sql + " and " + " VW_patlbvw.Username='" + username + "'";
                         sc.CommandText = sc.CommandText + " and " + "  Deptwiseuser.username=N'" + username + "'";

                     }
                 }
                 if (MNo != "")
                 {
                     sc.CommandText = sc.CommandText + " and " + " patmst.RefDr like '%" + MNo + "%'";
                 }
                 if (testname != "")
                 {
                     sc.CommandText = sc.CommandText + " and " + " SubDepartment.subdeptName = '" + testname + "'";
                 }
             }
               // sc.CommandText = sc.CommandText + ""+; 
             sc.CommandText = sc.CommandText + " GROUP BY patmst.IsActive  , patmstd.TestDeActive  , patmst.PatRegID, CAST(patmst.Tests AS varchar(255)), patmst.PID, patmst.FID, patmst.Patregdate, patmst.intial, "+
                  "  patmst.Patname, patmst.sex, patmst.Age,    patmst.RefDr, patmst.Reportdate, patmst.DoctorCode, patmst.CenterCode, patmst.FinancialYearID, patmst.TestCharges,  "+
                  "  patmst.Username, patmst.SampleType,     patmst.SampleStatus, patmst.RegistratonDateTime,  patmst.Drname, patmst.MDY, CAST(patmst.PatientcHistory AS varchar(255)), "+
                  "  patmst.Branchid,     SubDepartment.DigModule, patmstd.UnitCode, patmst.Remark, patmst.Patphoneno, patmst.IsActive, patmst.LabRegMediPro, SubDepartment.subdeptName,  "+ 
                  "  patmstd.BarcodeID, MainTest.Maintestname, patmstd.Labno, patmstd.MTCode, patmstd.Patauthicante, patmstd.ISReRun, patmstd.ReRunRemark, patmstd.MachinName,   "+
                  "  patmst.Isemergency, patmstd.PatientEmail ,patmstd.IspheboAccept, patmst.PPID,InterfaceStatus,patmstd.DoctorEmail ,ISNULL(patmstd.SpecimanNo,0),patmstd.BarcodeId , "+
                  "  ISNULL(patmstd.PanicResult,0) ,patmstd.PanicRemark,patmstd.PanicInformToResult,patmstd.PanicAction,patmstd.InformUserName,Patmstd.UploadPrescription ,patmstd.Isoutsource " +
                  "  ,patmstd.OutsourcePatientPID,patmstd.OutResTransfer ,isnull( patmstd.Technicanby,''),replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) ,Patmst.RepType " +
                                             " )";
         }
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
     public static DataTable GetPatmstForTeamL_new_11_testwise_Cyto(object UnitCode, object DeptID, object fromDate, object toDate, string Patauthicante, string MTCode, string patientName, string regnoID, string BarID, string CenterCode, string username, string usertype, int branchid, int maindept, int Number, string testname, string MNo, string CenterCodeNew, string RepDoneBy)
     {

         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         string sql = "SELECT distinct PatRegID, Test1, PID, FID, convert(varchar(20),Patregdate,103)+' '+convert(varchar(20),convert(time,Patregdate),100) as Patregdate, intial, Patname, sex, Age, RefDr, Reportdate, DoctorCode, CenterCode, " +
              "  FinancialYearID, TestCharges, Username, SampleType, " +
              "  SampleStatus, convert(varchar(20),RegistratonDateTime,103)+' '+convert(varchar(20),convert(time,RegistratonDateTime),100) as RegistratonDateTime,  Drname, MDY, cliniclahist, Branchid, DigModule, UnitCode, Remark, " +
              "  Patphoneno, IsActive, LabRegMediPro,Maintestname as TestName ,Labno ,CenterCode as CenterCode,intial +' '+Patname as FullName,cast( Age as nvarchar(50)) +' '+MDY as MYD,Drname as DrName, ISReRun+'-'+MachinName+'-'+ReRunRemark as ReRunType, SpecimanNo , ";

         if (Patauthicante == "Pending")
         {
             sql = sql + " 'Pending' as SampleStatusNew, ";
         }
         if (Patauthicante == "Completed")
         {
             sql = sql + " 'Completed' as SampleStatusNew, ";
         }
         if (Patauthicante == "Authorized")
         {
             sql = sql + " 'Authorized' as SampleStatusNew, ";
         }
         if (Patauthicante == "Tested")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "All")
         {
             sql = sql + " 'All' as SampleStatusNew, ";
            
         }
         if (Patauthicante == "Emergency")
         {
             sql = sql + " 'All' as SampleStatusNew, ";
            
         }
         if (Patauthicante == "Abnormal")
         {
             sql = sql + " 'All' as SampleStatusNew, ";
         }
         sql = sql + "  ''as TestName,Remark as p_remark,LabNo,MTCode, Isemergency  ,  case when PatientEmail=1 then 'Send Email' else 'Not Send' end as MailStatus,case when DoctorEmail=1 then 'Send DrEmail' else 'Not Send' end as DrMailStatus,InterfaceStatus ,PPID,PanicResult,UploadPrescription,Isoutsource ,OutsourcePatientPID,OutResTransfer " +

            " from VW_patlbvw where    IsActive=1 and  branchid=" + branchid + " and subdeptname='CYTOLOGY' and IspheboAccept =1 "; //and FID='" + FID + "'

         if (regnoID == "" && BarID == "")
         {
             if (fromDate != null && toDate != null)
             {
                 sql = sql + " and " + "VW_patlbvw.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and VW_patlbvw.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(toDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         if (MTCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.Test1 like '%" + MTCode + "%'";
         }
         if (patientName != "")
         {
             sql = sql + " and " + " VW_patlbvw.Patname like N'%" + patientName + "%'";
         }
         if (regnoID != "")
         {
             sql = sql + " and " + " (VW_patlbvw.PatRegID ='" + regnoID + "'  or LabRegMedipro  ='" + regnoID + "') ";

         }
        

         if (CenterCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.PPID ='" + CenterCode + "'";
         }
         if (CenterCodeNew != "")
         {
             sql = sql + " and " + " VW_patlbvw.CenterCode =N'" + CenterCodeNew + "'";
         }
         if (BarID != "")
         {
             sql = sql + " and " + " VW_patlbvw.BarcodeID like '%" + BarID + "%'";
         }
         if (maindept != 0)
         {
             sql = sql + " and " + " VW_patlbvw.DigModule ='" + maindept + "'";
         }
         if (UnitCode != null)
         {
             sql = sql + " and " + " VW_patlbvw.UnitCode='" + UnitCode + "'";
         }
         if (usertype == "Main Doctor" || usertype == "MainDoctor" || usertype == "Technician" || usertype == "Reporting")
         {
             if (username != "")
             {
                 // sql = sql + " and " + " VW_patlbvw.Username='" + username + "'";
                 sql = sql + " and " + " VW_patlbvw.DeptUserName='" + username + "'";

             }
         }
         if (MNo != "")
         {
             sql = sql + " and " + " VW_patlbvw.RefDr like '%" + MNo + "%'";
         }
         if (Patauthicante != "All")
         {
             if (Patauthicante == "Pending")
             {
                 sql = sql + " and " + "  (isnull(VW_patlbvw.Patauthicante,'')<>'Authorized' ) ";//or  VW_patlbvw.Isemergency='True'
             }
             else if (Patauthicante == "Tested")
             {
                 sql = sql + " and " + "  isnull(VW_patlbvw.Patauthicante,'')='Tested' ";
             }
             else if (Patauthicante == "Emergency")
             {
                 sql = sql + " and " + "  VW_patlbvw.Isemergency='True' ";
             }
             else if (Patauthicante == "Abnormal")
             {
                 sql = sql + " and " + "  (PanicResult=1) ";
             }
             else
             {
                 sql = sql + " and " + "  isnull(VW_patlbvw.Patauthicante,'')='Authorized' ";
             }

           
         }
         if (testname != "")
         {
             sql = sql + " and " + " VW_patlbvw.subdeptName = '" + testname + "'";
         }
         if (RepDoneBy != "")
         {
           //  sql = sql + " and " + " VW_patlbvw.Technicanby = '" + RepDoneBy + "'";
             sql = sql + " and " + " dbo.Get_ReportBy(Technicanby,Branchid) = '" + RepDoneBy + "'";
         }
         sql = sql + " order by VW_patlbvw.PID desc";
         SqlDataAdapter da = new SqlDataAdapter(sql, conn);
         DataTable ds = new DataTable();
         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;

     }


     public static DataTable GetPatmstForTeamL_new_11_testwise_Histo(object UnitCode, object DeptID, object fromDate, object toDate, string Patauthicante, string MTCode, string patientName, string regnoID, string BarID, string CenterCode, string username, string usertype, int branchid, int maindept, int Number, string testname, string MNo, string CenterCodeNew, string RepDoneBy)
     {

         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         string sql = "SELECT distinct PatRegID, Test1, PID, FID, convert(varchar(20),Patregdate,103)+' '+convert(varchar(20),convert(time,Patregdate),100) as Patregdate, intial, Patname, sex, Age, RefDr, Reportdate, DoctorCode, CenterCode, " +
              "  FinancialYearID, TestCharges, Username, SampleType, " +
              "  SampleStatus, convert(varchar(20),RegistratonDateTime,103)+' '+convert(varchar(20),convert(time,RegistratonDateTime),100) as RegistratonDateTime,  Drname, MDY, cliniclahist, Branchid, DigModule, UnitCode, Remark, " +
              "  Patphoneno, IsActive, LabRegMediPro,Maintestname as TestName ,Labno ,CenterCode as CenterCode,intial +' '+Patname as FullName,cast( Age as nvarchar(50)) +' '+MDY as MYD,Drname as DrName,  ISReRun+'-'+MachinName+'-'+ReRunRemark as ReRunType,SpecimanNo, ";

        
         if (Patauthicante == "Pending")
         {
             sql = sql + " 'Pending' as SampleStatusNew, ";
         }
         if (Patauthicante == "Completed")
         {
             sql = sql + " 'Completed' as SampleStatusNew, ";
         }
         if (Patauthicante == "Authorized")
         {
             sql = sql + " 'Authorized' as SampleStatusNew, ";
         }
         if (Patauthicante == "Tested")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "All")
         {
             sql = sql + " 'All' as SampleStatusNew, ";
           
         }
         if (Patauthicante == "Emergency")
         {
             sql = sql + " 'All' as SampleStatusNew, ";
            
         }
         if (Patauthicante == "Abnormal")
         {
             sql = sql + " 'All' as SampleStatusNew, ";
         }
         sql = sql + "  ''as TestName,Remark as p_remark,LabNo,MTCode,Isemergency  ,  case when PatientEmail=1 then 'Send Email' else 'Not Send' end as MailStatus,case when DoctorEmail=1 then 'Send DrEmail' else 'Not Send' end as DrMailStatus,InterfaceStatus,PPID,PanicResult,UploadPrescription,Isoutsource,OutsourcePatientPID,OutResTransfer " +

            " from VW_patlbvw where IsActive=1 and  branchid=" + branchid + " and subdeptname='HISTOPATHOLOGY' and IspheboAccept =1 "; //and FID='" + FID + "'

         if (regnoID == "" && BarID == "")
         {
             if (fromDate != null && toDate != null)
             {
                 sql = sql + " and " + "VW_patlbvw.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and VW_patlbvw.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(toDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         if (MTCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.Test1 like '%" + MTCode + "%'";
         }
         if (patientName != "")
         {
             sql = sql + " and " + " VW_patlbvw.Patname like N'%" + patientName + "%'";
         }
         if (regnoID != "")
         {
             sql = sql + " and " + " (VW_patlbvw.PatRegID ='" + regnoID + "'  or LabRegMedipro  ='" + regnoID + "') ";

         }
         if (CenterCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.PPID ='" + CenterCode + "'";
         }
         if (CenterCodeNew != "")
         {
             sql = sql + " and " + " VW_patlbvw.CenterCode ='" + CenterCodeNew + "'";
         }
         if (BarID != "")
         {
             sql = sql + " and " + " VW_patlbvw.BarcodeID like '%" + BarID + "%'";
         }
         if (maindept != 0)
         {
             sql = sql + " and " + " VW_patlbvw.DigModule ='" + maindept + "'";
         }
         if (UnitCode != null)
         {
             sql = sql + " and " + " VW_patlbvw.UnitCode='" + UnitCode + "'";
         }
         if (usertype == "Main Doctor" || usertype == "MainDoctor" || usertype == "Technician" || usertype == "Reporting")
         {
             if (username != "")
             {
                 // sql = sql + " and " + " VW_patlbvw.Username='" + username + "'";
                 sql = sql + " and " + " VW_patlbvw.DeptUserName='" + username + "'";

             }
         }
         if (MNo != "")
         {
             sql = sql + " and " + " VW_patlbvw.RefDr like '%" + MNo + "%'";
         }
         if (Patauthicante != "All")
         {
             if (Patauthicante == "Pending")
             {
                 sql = sql + " and " + "  (isnull(VW_patlbvw.Patauthicante,'')<>'Authorized' ) ";//or  VW_patlbvw.Isemergency='True'
             }
             else if (Patauthicante == "Tested")
             {
                 sql = sql + " and " + "  isnull(VW_patlbvw.Patauthicante,'')='Tested' ";
             }
             else if (Patauthicante == "Emergency")
             {
                 sql = sql + " and " + "  VW_patlbvw.Isemergency='True' ";
             }
             else if (Patauthicante == "Abnormal")
             {
                 sql = sql + " and " + "  (PanicResult=1) ";
             }
             else
             {
                 sql = sql + " and " + "  isnull(VW_patlbvw.Patauthicante,'')='Authorized' ";
             }
         }
         if (testname != "")
         {
             sql = sql + " and " + " VW_patlbvw.subdeptName = '" + testname + "'";
         }
         if (RepDoneBy != "")
         {
            // sql = sql + " and " + " VW_patlbvw.Technicanby = '" + RepDoneBy + "'";
             sql = sql + " and " + " dbo.Get_ReportBy(Technicanby,Branchid) = '" + RepDoneBy + "'";
         }
         sql = sql + " order by VW_patlbvw.PID desc";
         SqlDataAdapter da = new SqlDataAdapter(sql, conn);
         DataTable ds = new DataTable();
         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;

     }

     public static DataTable GetPatmstForTeamL_new_11_testwise_Amedent(object UnitCode, object DeptID, object fromDate, object toDate, string Patauthicante, string MTCode, string patientName, string regnoID, string Barid, string CenterCode, string username, string usertype, int branchid, int maindept, int Number, string testname, string MNo, string CenterCodeNew, string RepDoneBy)
     {

         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         string sql = "SELECT distinct PatRegID, Test1, PID, FID,  Patregdate, intial, Patname, sex, Age, RefDr, Reportdate, DoctorCode, CenterCode, " +
              "  FinancialYearID, TestCharges, Username, SampleType, " +
              "  SampleStatus, convert(varchar(20),RegistratonDateTime,103)+' '+convert(varchar(20),convert(time,RegistratonDateTime),100) as RegistratonDateTime,  Drname, MDY, cliniclahist, Branchid, DigModule, UnitCode, Remark, " +
              "  Patphoneno, IsActive, LabRegMediPro,Maintestname as TestName,Labno ,CenterCode as CenterCode,isnull(intial,'.') +' '+Patname as FullName,cast( Age as nvarchar(50)) +' '+MDY as MYD,Drname as DrName, ISReRun+'-'+MachinName+'-'+ReRunRemark as ReRunType, SpecimanNo , ";

         //  dbo.Get_Teststatus(VW_patlbvw.PID,VW_patlbvw.PatRegID,VW_patlbvw.Branchid,VW_patlbvw.FID)as   SampleStatusNew,
         if (Patauthicante == "Pending")
         {
             sql = sql + " 'Pending' as SampleStatusNew, ";
         }

         if (Patauthicante == "Completed")
         {
             sql = sql + " 'Completed' as SampleStatusNew, ";
         }
         if (Patauthicante == "Authorized")
         {
             sql = sql + " 'Authorized' as SampleStatusNew, ";
         }
         if (Patauthicante == "Tested")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "IntNotRece")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "IntRece")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "All")
         {
             sql = sql + " 'All' as SampleStatusNew, ";

         }
         if (Patauthicante == "Emergency")
         {
             sql = sql + " 'All' as SampleStatusNew, ";

         }
         if (Patauthicante == "Outsource")
         {
             sql = sql + " 'All' as SampleStatusNew, ";

         }
         if (Patauthicante == "Abnormal")
         {
             sql = sql + " 'All' as SampleStatusNew, ";
         }
         sql = sql + "  ''as TestName,Remark as p_remark,LabNo,MTCode ,Isemergency,   case when PatientEmail=1 then 'Send Email' else 'Not Send' end as MailStatus,case when DoctorEmail=1 then 'Send DrEmail' else 'Not Send' end as DrMailStatus,InterfaceStatus,PPID , PanicResult ,UploadPrescription ,Isoutsource , OutsourcePatientPID,OutResTransfer,isnull(Balance,0)as Balance  ,isnull(PendingPer,0) as PendingPer,Convert(varchar(10),CONVERT(date,Patregdate,106),103) as PEDate, DailyseqNo,RepType " +

            "  from VW_patlbvw where  IsActive=1 and  branchid=" + branchid + " and IspheboAccept =1 "; //and FID='" + FID + "'

         if (CenterCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.PPID ='" + CenterCode + "'";
         }
         else if (regnoID != "")
         {
             sql = sql + " and " + " VW_patlbvw.PatregID ='" + regnoID + "'";
         }
         else if (Barid != "")
         {
             sql = sql + " and " + " VW_patlbvw.BarcodeID like '%" + Barid + "%'";
         }
         else
         {
             if (fromDate != null && toDate != null)
             {
                 sql = sql + " and " + "VW_patlbvw.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and VW_patlbvw.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(toDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "' ";
             }

             if (MTCode != "")
             {
                 sql = sql + " and " + " VW_patlbvw.Test1 like '%" + MTCode + "%'";
             }
             if (patientName != "")
             {
                 sql = sql + " and " + " VW_patlbvw.Patname like N'%" + patientName + "%'";
             }



             if (CenterCodeNew != "")
             {
                 sql = sql + " and " + " VW_patlbvw.CenterCode =N'" + CenterCodeNew + "'";
             }

             //if (maindept != 0)
             //{
             //    sql = sql + " and " + " VW_patlbvw.DigModule ='" + maindept + "'";
             //}
             if (UnitCode != null)
             {
                 sql = sql + " and " + " VW_patlbvw.UnitCode='" + UnitCode + "'";
             }
             if (usertype == "Main Doctor" || usertype == "MainDoctor" || usertype == "Technician" || usertype == "Reporting")
             {
                 //if (username != "")
                 //{
                 //    // sql = sql + " and " + " VW_patlbvw.Username='" + username + "'";
                 //    sql = sql + " and " + " VW_patlbvw.DeptUserName='" + username + "'";

                 //}
             }
             if (MNo != "")
             {
                 sql = sql + " and " + " VW_patlbvw.RefDr like '%" + MNo + "%'";
             }
             //if (Patauthicante != "All")
             //{
             //    if (Patauthicante == "Pending")
             //    {
             //        sql = sql + " and " + "  (VW_patlbvw.Patauthicante<>'Authorized' ) ";//and  VW_patlbvw.Isemergency='True'
             //    }
             //    else if (Patauthicante == "Tested")
             //    {
             //        sql = sql + " and " + "  VW_patlbvw.Patauthicante='Tested' ";
             //    }
             //    else if (Patauthicante == "Emergency")
             //    {
             //        sql = sql + " and " + "  VW_patlbvw.Isemergency='True' ";
             //    }
             //    else if (Patauthicante == "IntNotRece")
             //    {
             //        sql = sql + " and " + "    InterfaceStatus ='No'  ";
             //    }
             //    else if (Patauthicante == "IntRece")
             //    {
             //        sql = sql + " and " + "   InterfaceStatus ='Yes' and (VW_patlbvw.Patauthicante<>'Authorized') ";
             //    }
             //    else if (Patauthicante == "Outsource")
             //    {
             //        sql = sql + " and " + "  (isoutsource>0 or OutsourcePatientPID>0) ";
             //    }
             //    else if (Patauthicante == "Abnormal")
             //    {
             //        sql = sql + " and " + "  (PanicResult='1') ";
             //    }
             //    else
             //    {
             //        sql = sql + " and " + "  VW_patlbvw.Patauthicante='Authorized' ";
             //    }
                 
             //}

             sql = sql + " and " + "  VW_patlbvw.Patauthicante='Authorized' ";
             if (testname != "")
             {
                 sql = sql + " and " + " VW_patlbvw.subdeptName = '" + testname + "'";
             }
             if (RepDoneBy != "")
             {
                 sql = sql + " and " + " Remark like '" + RepDoneBy + "%'";
             }
         }
         sql = sql + " order by  PID desc ";
         SqlDataAdapter da = new SqlDataAdapter(sql, conn);
         DataTable ds = new DataTable();
         try
         {
             conn.Open();

             da.Fill(ds);
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
         return ds;

     }



     public static DataTable GetPatmstForTeamL_new_11_testwise(object UnitCode, object DeptID, object fromDate, object toDate, string Patauthicante, string MTCode, string patientName, string regnoID, string Barid, string CenterCode, string username, string usertype, int branchid, int maindept, int Number, string testname, string MNo, string CenterCodeNew,string RepDoneBy)
     {

         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         string sql = "SELECT distinct PatRegID, Test1, PID, FID,  Patregdate, intial, Patname, sex, Age, RefDr, Reportdate, DoctorCode, CenterCode, " +
              "  FinancialYearID, TestCharges, Username, SampleType, " +
              "  SampleStatus, convert(varchar(20),RegistratonDateTime,103)+' '+convert(varchar(20),convert(time,RegistratonDateTime),100) as RegistratonDateTime,  Drname, MDY, cliniclahist, Branchid, DigModule, UnitCode, Remark, " +
              "  Patphoneno, IsActive, LabRegMediPro,Maintestname as TestName,Labno ,CenterCode as CenterCode,isnull(intial,'.') +' '+Patname as FullName,cast( Age as nvarchar(50)) +' '+MDY as MYD,Drname as DrName, ISReRun+'-'+MachinName+'-'+ReRunRemark as ReRunType, SpecimanNo , ";
         
       //  dbo.Get_Teststatus(VW_patlbvw.PID,VW_patlbvw.PatRegID,VW_patlbvw.Branchid,VW_patlbvw.FID)as   SampleStatusNew,
         if (Patauthicante == "Pending")
          {
              sql = sql + " 'Pending' as SampleStatusNew, ";
          }
        
         if (Patauthicante == "Completed")
         {
             sql = sql + " 'Completed' as SampleStatusNew, ";
         }
         if (Patauthicante == "Authorized")
         {
             sql = sql + " 'Authorized' as SampleStatusNew, ";
         }
         if (Patauthicante == "Tested")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "IntNotRece")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "IntRece")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "All")
         {
             sql = sql + " 'All' as SampleStatusNew, ";
            
         }
         if (Patauthicante == "Emergency")
         {
             sql = sql + " 'All' as SampleStatusNew, ";
           
         }
         if (Patauthicante == "Outsource")
         {
             sql = sql + " 'All' as SampleStatusNew, ";

         }
         if (Patauthicante == "Abnormal")
         {
             sql = sql + " 'All' as SampleStatusNew, ";
         }
         sql = sql + "  ''as TestName,Remark as p_remark,LabNo,MTCode ,Isemergency,   case when PatientEmail=1 then 'Send Email' else 'Not Send' end as MailStatus,case when DoctorEmail=1 then 'Send DrEmail' else 'Not Send' end as DrMailStatus,InterfaceStatus,PPID , PanicResult ,UploadPrescription ,Isoutsource , OutsourcePatientPID,OutResTransfer,isnull(Balance,0)as Balance  ,isnull(PendingPer,0) as PendingPer,Convert(varchar(10),CONVERT(date,Patregdate,106),103) as PEDate, DailyseqNo,RepType " +

            "  from VW_patlbvw where  IsActive=1 and  branchid=" + branchid + " and IspheboAccept =1 "; //and FID='" + FID + "'

         if (CenterCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.PPID ='" + CenterCode + "'";
         }
         else  if (regnoID != "")
         {
             sql = sql + " and " + " VW_patlbvw.PatregID ='" + regnoID + "'";
         }
         else   if (Barid != "")
         {
             sql = sql + " and " + " VW_patlbvw.BarcodeID like '%" + Barid + "%'";
         }
         else
         {
             if (fromDate != null && toDate != null)
             {
                 sql = sql + " and " + "VW_patlbvw.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and VW_patlbvw.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(toDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "' ";
             }

             if (MTCode != "")
             {
                 sql = sql + " and " + " VW_patlbvw.Test1 like '%" + MTCode + "%'";
             }
             if (patientName != "")
             {
                 sql = sql + " and " + " VW_patlbvw.Patname like N'%" + patientName + "%'";
             }



             if (CenterCodeNew != "")
             {
                 sql = sql + " and " + " VW_patlbvw.CenterCode =N'" + CenterCodeNew + "'";
             }
             
             //if (maindept != 0)
             //{
             //    sql = sql + " and " + " VW_patlbvw.DigModule ='" + maindept + "'";
             //}
             if (UnitCode != null)
             {
                 sql = sql + " and " + " VW_patlbvw.UnitCode='" + UnitCode + "'";
             }
             if (usertype == "Main Doctor" || usertype == "MainDoctor" || usertype == "Technician" || usertype == "Reporting")
             {
                 //if (username != "")
                 //{
                 //    // sql = sql + " and " + " VW_patlbvw.Username='" + username + "'";
                 //    sql = sql + " and " + " VW_patlbvw.DeptUserName='" + username + "'";

                 //}
             }
             if (MNo != "")
             {
                 sql = sql + " and " + " VW_patlbvw.RefDr like '%" + MNo + "%'";
             }
             if (Patauthicante != "All")
             {
                 if (Patauthicante == "Pending")
                 {
                     sql = sql + " and " + "  (VW_patlbvw.Patauthicante<>'Authorized' ) ";//and  VW_patlbvw.Isemergency='True'
                 }
                 else if (Patauthicante == "Tested")
                 {
                     sql = sql + " and " + "  VW_patlbvw.Patauthicante='Tested' ";
                 }
                 else if (Patauthicante == "Emergency")
                 {
                     sql = sql + " and " + "  VW_patlbvw.Isemergency='True' ";
                 }
                 else if (Patauthicante == "IntNotRece")
                 {
                     sql = sql + " and " + "    InterfaceStatus ='No'  ";
                 }
                 else if (Patauthicante == "IntRece")
                 {
                     sql = sql + " and " + "   InterfaceStatus ='Yes' and (VW_patlbvw.Patauthicante<>'Authorized') ";
                 }
                 else if (Patauthicante == "Outsource")
                 {
                     sql = sql + " and " + "  (isoutsource>0 or OutsourcePatientPID>0) ";
                 }
                 else if (Patauthicante == "Abnormal")
                 {
                     sql = sql + " and " + "  (PanicResult='1') ";
                 }
                 else
                 {
                     sql = sql + " and " + "  VW_patlbvw.Patauthicante='Authorized' ";
                 }
                 //sql = sql + " and " + " dbo.Get_Teststatus(VW_patlbvw.PID,VW_patlbvw.PatRegID,VW_patlbvw.Branchid,VW_patlbvw.FID) ='" + Patauthicante + "'";
             }
             if (testname != "")
             {
                 sql = sql + " and " + " VW_patlbvw.subdeptName = '" + testname + "'";
             }
             if (RepDoneBy != "")
             {
                 sql = sql + " and " + " Remark like '" + RepDoneBy + "%'";
             }
         }
         sql = sql + " order by  PID desc ";
         SqlDataAdapter da = new SqlDataAdapter(sql, conn);
         DataTable ds = new DataTable();
         try
         {
             conn.Open();
             
             da.Fill(ds);
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
         return ds;

     }


     public void AlterViewvw_VW_testmgr_nNew(object startDate, object endDate)
     {
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         SqlCommand sc = conn.CreateCommand();

               SqlCommand cmd = new SqlCommand(" alter view VW_testmgr_n as SELECT DISTINCT "+
                  "  TOP (99.99) PERCENT '' AS MainDept,replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) as DailyseqNo  , RTRIM(patmst.intial) + ' ' + patmst.Patname AS Name, patmst.PID, patmst.PatRegID, patmst.FID, patmst.intial, patmst.Patname, " +
                  "  patmst.sex, patmst.Age, patmst.MDY, patmst.Phrecdate, patmst.Branchid, patmst.TestCharges, patmst.CenterCode, patmst.Drname, patmst.CenterName, "+
                  "  patmstd.BarcodeID, patmstd.SampleType, CAST(patmst.Tests AS nvarchar(1000)) AS Tests, patmstd.IspheboAccept, patmstd.MTCode AS vial_Code, "+
                  "  patmstd.MTCode AS STCODE, patmst.UnitCode, patmstd.Patmstid as SrNo, Ocl.OutsourceLabName, patmstd.OutLabName, patmst.LabRegMediPro, patmst.Isemergency, " +
                  "  CAST(MainTest.Maintestname AS nvarchar(2000)) AS Testname, ISNULL(patmstd.SpecimanNo,0) as SpecimanNo ,PhlebotomistCollect,PhlebotomistRejectremark,OSBarcodeID, patmst.PPID,CAST(patmst.PatientcHistory AS nvarchar(3000)) AS PatientcHistory,ReqbyDoc,SpeciamRemark " +
                  "  FROM         patmst INNER JOIN "+
                  "  patmstd ON patmst.PID = patmstd.PID INNER JOIN "+
                  "  MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode LEFT OUTER JOIN "+
                  "  Ocl ON patmstd.OutLabName = Ocl.Id "+
                 "  where (patmst.IsActive = 1) and patmstd.TestDeActive=0  and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "'  " +
         
               "  order by patmst.PatRegID DESC, patmstd.SampleType DESC ", conn);
         try
         {
             conn.Open();
             cmd.ExecuteNonQuery();
         }
         catch (Exception exe)
         {
             throw;
         }
         finally
         {
             conn.Close();
             conn.Dispose();
         }
         


     }

     public void AlterViewvw_VW_testmgr_nMain(object startDate, object endDate)
     {
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         SqlCommand sc = conn.CreateCommand();

    SqlCommand cmd = new SqlCommand(" alter view [VW_testmgr] as SELECT DISTINCT "+
           " Patmstid as SrNo, '' AS MainDept,replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) as DailyseqNo  , RTRIM(patmst.intial) + ' ' + patmst.Patname AS Name, patmst.PID, patmst.PatRegID, patmst.FID, patmst.intial, patmst.Patname, patmst.sex, " +
           " patmst.Age, patmst.MDY, patmst.Phrecdate, patmst.Branchid, patmst.TestCharges, patmst.CenterCode, patmst.Drname, patmst.CenterName, patmstd.BarcodeID, "+
           " patmstd.SampleType, patmstd.MTCode AS STCODE, CAST(MainTest.Maintestname AS nvarchar(2000)) AS Testname, patmst.IsActive, patmst.Patphoneno, patmstd.OutLabName, " +
           " Ocl.OutsourceLabName, patmst.LabRegMediPro, patmst.Isemergency, patmstd.IspheboAccept ,patmst.PPID ,ISNULL(patmstd.SpecimanNo,0) as SpecimanNo ,PhlebotomistCollect,PhlebotomistRejectremark,OSBarcodeID,CAST(patmst.PatientcHistory AS nvarchar(3000)) AS PatientcHistory,ReqbyDoc,SpeciamRemark " +
           " FROM         patmst INNER JOIN "+
           " patmstd ON patmst.PID = patmstd.PID INNER JOIN "+
           " MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode LEFT OUTER JOIN "+
           " Ocl ON patmstd.OutLabName = Ocl.Id  " +
           "  where (patmst.IsActive = 1) and patmstd.TestDeActive=0  and patmst.Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "'   ", conn);
         try
         {
             conn.Open();
             cmd.ExecuteNonQuery();
         }
         catch (Exception exe)
         {
             throw;
         }
         finally
         {
             conn.Close();
             conn.Dispose();
         }
        


     }

     public static void AlterViewvw_VW_Result_Patmstd(string usertype, string Username, object startDate, object endDate)
     {
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         SqlCommand sc = conn.CreateCommand();

       
         sc.CommandText = "   Alter View VW_Result_Patmstd as    SELECT    * from patmstd " +
              " where  Patregdate >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).AddDays(-11).ToString("dd/MMM/yyyy") + "' and Patregdate <'" + DateTimeConvesion.getDateFromString(endDate.ToString()).AddDays(5).ToString("dd/MMM/yyyy") + "'   ";
       
       
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
     public static void AlterViewvw_VW_Result_ResMst(string usertype, string Username, object startDate, object endDate)
     {
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         SqlCommand sc = conn.CreateCommand();
         sc.CommandText = "   Alter View VW_Result_ResMst as    SELECT    * from ResMst " +
              " where  ResDate >='" + DateTimeConvesion.getDateFromString(startDate.ToString()).AddDays(-11).ToString("dd/MMM/yyyy") + "' and ResDate <'" + DateTimeConvesion.getDateFromString(endDate.ToString()).AddDays(5).ToString("dd/MMM/yyyy") + "'   ";
   

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
     public DataTable GetRerunResult(string barcodeid)
     {
         DataAccess data = new DataAccess();
         SqlConnection conn = data.ConInitForDC1();
         SqlCommand sc = new SqlCommand("SP_ReRunResult", conn);
                  
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

     public DataTable Bind_RerunResult()
     {
         DataAccess data = new DataAccess();
         SqlConnection conn = data.ConInitForDC1();
         SqlCommand sc = new SqlCommand("SP_ReRunResult_Bind", conn);

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

     public DataTable GET_BarcodeId(string PatRegID,string MTCode)
     {
         DataAccess data = new DataAccess();
         SqlConnection conn = data.ConInitForDC1();
         SqlCommand sc = new SqlCommand("SP_Getbarcodeid", conn);


         sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar,50)).Value = PatRegID;
         sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar,50)).Value = MTCode;

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

     public void Update_rerun(string Barcode, string ReRunRemark, string PatRegID, string branchid, string MachinName, string MTCode)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         SqlCommand sc = new SqlCommand("UPDATE patmstd SET patrepstatus=0 ,Patauthicante=@Patauthicante,ISReRun=@ISReRun,ReRunRemark=@ReRunRemark,MachinName=@MachinName where BarCodeID=@BarCodeID and PatRegID=@PatRegID and MTCode=@MTCode and branchid='" + branchid + "'", conn);
         sc.Parameters.Add(new SqlParameter("@ISReRun", SqlDbType.NVarChar, 50)).Value = "YES";
         sc.Parameters.Add(new SqlParameter("@BarCodeID", SqlDbType.NVarChar, 50)).Value = Barcode;
         sc.Parameters.Add(new SqlParameter("@ReRunRemark", SqlDbType.NVarChar, 250)).Value = ReRunRemark;
         sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar,50)).Value = PatRegID;
         sc.Parameters.Add(new SqlParameter("@MachinName", SqlDbType.NVarChar, 50)).Value = MachinName;
         sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
         sc.Parameters.Add(new SqlParameter("@Patauthicante", SqlDbType.NVarChar, 50)).Value = "Registered";

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

     public static string PatientCountBanner_validation(int branchid)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         string sql = "";
         sql = sql + "Select (Type) from BannerTable ";



         SqlCommand sc = new SqlCommand(sql, conn);

         string patientcount = "0";
         SqlDataReader sdr = null;

         try
         {
             conn.Open();
             patientcount = Convert.ToString(sc.ExecuteScalar());
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
         return patientcount;
     }
     public DataSet ReportDownload_Patientwise(object DoctorCode, object startDate, object endDate, object Patauthicante, string patientName, string Reg_no, int branchid, int DigModule, string subdeptName, string FID, string labcode_main, string RefDr_code, string Barcode, string Mno, string RepStatus)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         string sql = "Select ''as TestStatus, * from VW_Printingpatientwise where  PatRegID<>'' and branchid=" + branchid + ""; //FID='" + FID + "' and


         if (Reg_no != "")
         {
             sql = sql + " and PatRegID='" + Reg_no + "'";
             if (labcode_main != null && labcode_main != "")
             {
                 sql = sql + " and UnitCode='" + labcode_main + "'";

             }
         }
         else
         {
             if (patientName != "")
             {
                 sql = sql + " and PatientName like N'%" + patientName + "'";
                 if (labcode_main != null && labcode_main != "")
                 {
                     sql = sql + " and UnitCode='" + labcode_main + "'";

                 }
             }
             else
             {
                 if (Patauthicante != "All" && Patauthicante != "" && Patauthicante != null)
                 {
                     sql += " and Patauthicante='" + Patauthicante + "'";
                 }
                 if (subdeptName != "")
                 {
                     sql += " and SDCode='" + subdeptName + "'";
                 }
                 //if (startDate != null && endDate != null)
                 //{
                 //    sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
                 //}
                 if (labcode_main != null && labcode_main != "")
                 {
                     sql = sql + " and UnitCode='" + labcode_main + "'";

                 }
             }
         }
         if (startDate != null && endDate != null)
         {
             sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
         }
         if (RefDr_code != "" && RefDr_code != null)
         {
             sql += " AND DoctorCode ='" + RefDr_code.Trim() + "' ";
         }
         else if (DoctorCode != null && DoctorCode.ToString() != "0" && DoctorCode != "")
         {
             sql += " and CenterCode=N'" + DoctorCode.ToString() + "'";
         }
         //if (Barcode != "")
         //{
         //    sql += " AND BarcodeID ='" + Barcode + "' ";
         //}
         if (Mno != "")
         {
             sql += " AND Patphoneno ='" + Mno + "' ";
         }
         if (RepStatus == "1")
         {
             sql += " AND  dbo.FUN_GetTest_Print_count(PatRegID,PID)=0 ";
         }
         if (RepStatus == "0")
         {
             sql += " AND dbo.FUN_GetTest_Pensing_count(PatRegID,PID)=0 ";
         }
         if (RepStatus == "2")
         {
             sql += " AND  dbo.FUN_GetTest_autho_count(PatRegID,PID)=0 ";
         }
         if (RepStatus == "3")
         {
             sql += " AND  dbo.FUN_GetTest_Tested_count(PatRegID,PID)=0 ";
         }
         sql += " order by PatRegID,OutStAmt desc";
         SqlDataAdapter da = new SqlDataAdapter(sql, conn);
         DataSet ds = new DataSet();

         try
         {
             conn.Open();
             da.Fill(ds);
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
                 throw;
             }
         }
         return ds;
     }

     public DataSet ReportDownloadModify_Patientwise(object DoctorCode, object startDate, object endDate, object Patauthicante, string patientName, string Reg_no, int branchid, int DigModule, string subdeptName, string FID, string labcode_main, string Barcode, string Subdept, string RepStatus)
     {
         SqlConnection conn = DataAccess.ConInitForDC();
         string sql = "Select  SrNo, PID, FID, PatRegID, Patregdate, PatientName, sex, Age, MDY, RefDr, Tests, PF, Reportdate, Phrecdate, flag, Patphoneno, Pataddress, Isemergency, Branchid, "+
           " DoctorCode, CenterCode, FinancialYearID, EmailID, Drname, TestCharges, SampleID, CenterName, Username, Usertype, SampleType, SampleStatus, Remark, "+
           " PatientcHistory, RegistratonDateTime, TelNo, Email, Patusername, Patpassword, PPID, testname, Smsevent, UnitCode, IsbillBH, IsActive, Monthlybill, cevent, "+
           " LabRegMediPro, IsFreeze, ISCallPatient, CallRemark, OtherRefDoctor, Weights, Heights, Disease, LastPeriod, Symptoms, FSTime, Therapy, AmtPaid as AmtReceived, NetPayment, OutStAmt,Discount,''as TestStatus " +
          " from VW_Printingpatientwise where  PatRegID<>'' and branchid=" + branchid + " "; //FID='" + FID + "' and


         if (Reg_no != "")
         {
             sql = sql + " and PatRegID='" + Reg_no + "'";
             if (labcode_main != null && labcode_main != "")
             {
                 sql = sql + " and UnitCode='" + labcode_main + "'";

             }
         }
         else
         {
             if (patientName != "")
             {
                 sql = sql + " and PatientName like N'%" + patientName + "%'";
                 if (labcode_main != null && labcode_main != "")
                 {
                     sql = sql + " and UnitCode='" + labcode_main + "'";

                 }
             }
             else
             {
                 if (Patauthicante != "All" && Patauthicante != "" && Patauthicante != null)
                 {
                     sql += " and Patauthicante='" + Patauthicante + "'";
                 }
                 if (subdeptName != "")
                 {
                     sql += " and SDCode='" + subdeptName + "'";
                 }
                 //if (startDate != null && endDate != null)
                 //{
                 //    sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
                 //}
                 if (labcode_main != null && labcode_main != "")
                 {
                     sql = sql + " and UnitCode='" + labcode_main + "'";

                 }
             }
         }
         if (startDate != null && endDate != null)
         {
             sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
         }
         if (DoctorCode != null && DoctorCode.ToString() != "0" && DoctorCode != "")
         {
             sql += " and CenterCode='" + DoctorCode.ToString() + "'";
         }
         if (Barcode != "")
         {
             sql += " AND BarcodeID ='" + Barcode + "' ";
         }
         if (Subdept != "")
         {
             sql += " AND SDCode in (" + Subdept + " )";
         }
         if (RepStatus == "1")
         {
             sql += " AND  dbo.FUN_GetTest_Print_count(PatRegID,PID)=0 ";
         }
         if (RepStatus == "0")
         {
             sql += " AND dbo.FUN_GetTest_Pensing_count(PatRegID,PID)=0 ";
         }
         if (RepStatus == "2")
         {
             sql += " AND  dbo.FUN_GetTest_autho_count(PatRegID,PID)=0 ";
         }
         if (RepStatus == "3")
         {
             sql += " AND  dbo.FUN_GetTest_Tested_count(PatRegID,PID)=0 ";
         }
        
         sql += " order by PatRegID desc";
         SqlDataAdapter da = new SqlDataAdapter(sql, conn);
         DataSet ds = new DataSet();

         try
         {
             conn.Open();
             da.Fill(ds);
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
         return ds;
     }

     public static DataTable GetPatmstForTeamL_new_TestResultTransfer(object UnitCode, object DeptID, object fromDate, object toDate, string Patauthicante, string MTCode, string patientName, string regnoID, string Barid, string CenterCode, string username, string usertype, int branchid, int maindept, int Number, string testname, string MNo, string CenterCodeNew)
     {

         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         string sql = "SELECT distinct PatRegID, Test1, PID, FID,  Patregdate, intial, Patname, sex, Age, RefDr, Reportdate, DoctorCode, CenterCode, " +
              "  FinancialYearID, TestCharges, Username, SampleType, " +
              "  SampleStatus, convert(varchar(20),RegistratonDateTime,103)+' '+convert(varchar(20),convert(time,RegistratonDateTime),100) as RegistratonDateTime,  Drname, MDY, cliniclahist, Branchid, DigModule, UnitCode, Remark, " +
              "  Patphoneno, IsActive, LabRegMediPro,Maintestname as TestName,Labno ,CenterCode as CenterCode,isnull(intial,'.') +' '+Patname as FullName,cast( Age as nvarchar(50)) +' '+MDY as MYD,Drname as DrName, ISReRun+'-'+MachinName+'-'+ReRunRemark as ReRunType, SpecimanNo , ";

         //  dbo.Get_Teststatus(VW_patlbvw.PID,VW_patlbvw.PatRegID,VW_patlbvw.Branchid,VW_patlbvw.FID)as   SampleStatusNew,
         if (Patauthicante == "Pending")
         {
             sql = sql + " 'Pending' as SampleStatusNew, ";
         }
         if (Patauthicante == "Completed")
         {
             sql = sql + " 'Completed' as SampleStatusNew, ";
         }
         if (Patauthicante == "Authorized")
         {
             sql = sql + " 'Authorized' as SampleStatusNew, ";
         }
         if (Patauthicante == "Tested")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "IntNotRece")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "IntRece")
         {
             sql = sql + " 'Tested' as SampleStatusNew, ";
         }
         if (Patauthicante == "All")
         {
             sql = sql + " 'All' as SampleStatusNew, ";

         }
         if (Patauthicante == "Emergency")
         {
             sql = sql + " 'All' as SampleStatusNew, ";

         }
         if (Patauthicante == "Outsource")
         {
             sql = sql + " 'All' as SampleStatusNew, ";

         }
         sql = sql + "  ''as TestName,Remark as p_remark,LabNo,MTCode ,Isemergency,   case when PatientEmail=1 then 'Send Email' else 'Not Send' end as MailStatus,case when DoctorEmail=1 then 'Send DrEmail' else 'Not Send' end as DrMailStatus,InterfaceStatus,PPID , PanicResult ,UploadPrescription ,Isoutsource , OutsourcePatientPID,OutResTransfer " +

            " from VW_patlbvw where OutsourcePatientPID >0 and IsActive=1 and  branchid=" + branchid + " and IspheboAccept =1 "; //and FID='" + FID + "'

         if (regnoID == "" && Barid == "")
         {
             if (fromDate != null && toDate != null)
             {
                 sql = sql + " and " + "VW_patlbvw.RegistratonDateTime >='" + DateTimeConvesion.getDateFromString(fromDate.ToString()).ToString("dd/MMM/yyyy") + "' and VW_patlbvw.RegistratonDateTime <'" + DateTimeConvesion.getDateFromString(toDate.ToString()).AddDays(1).ToString("dd/MMM/yyyy") + "' ";
             }
         }
         if (MTCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.Test1 like '%" + MTCode + "%'";
         }
         if (patientName != "")
         {
             sql = sql + " and " + " VW_patlbvw.Patname like N'%" + patientName + "%'";
         }


         if (CenterCode != "")
         {
             sql = sql + " and " + " VW_patlbvw.PPID ='" + CenterCode + "'";
         }
         if (CenterCodeNew != "")
         {
             sql = sql + " and " + " VW_patlbvw.CenterCode ='" + CenterCodeNew + "'";
         }
         if (Barid != "")
         {
             sql = sql + " and " + " VW_patlbvw.BarcodeID like '%" + Barid + "%'";
         }
         if (maindept != 0)
         {
             sql = sql + " and " + " VW_patlbvw.DigModule ='" + maindept + "'";
         }
         if (UnitCode != null)
         {
             sql = sql + " and " + " VW_patlbvw.UnitCode='" + UnitCode + "'";
         }
         if (usertype == "Main Doctor" || usertype == "MainDoctor" || usertype == "Technician" || usertype == "Reporting")
         {
             if (username != "")
             {
                 // sql = sql + " and " + " VW_patlbvw.Username='" + username + "'";
                 sql = sql + " and " + " VW_patlbvw.DeptUserName='" + username + "'";

             }
         }
         if (MNo != "")
         {
             sql = sql + " and " + " VW_patlbvw.RefDr like '%" + MNo + "%'";
         }
         if (Patauthicante != "All")
         {
             if (Patauthicante == "Pending")
             {
                 sql = sql + " and " + "  (VW_patlbvw.Patauthicante<>'Authorized' ) ";//and  VW_patlbvw.Isemergency='True'
             }
             else if (Patauthicante == "Tested")
             {
                 sql = sql + " and " + "  VW_patlbvw.Patauthicante='Tested' ";
             }
             else if (Patauthicante == "Emergency")
             {
                 sql = sql + " and " + "  VW_patlbvw.Isemergency='True' ";
             }
             else if (Patauthicante == "IntNotRece")
             {
                 sql = sql + " and " + "    InterfaceStatus ='No'  ";
             }
             else if (Patauthicante == "IntRece")
             {
                 sql = sql + " and " + "   InterfaceStatus ='Yes' and (VW_patlbvw.Patauthicante<>'Authorized') ";
             }
             else if (Patauthicante == "Outsource")
             {
                 sql = sql + " and " + "  (isoutsource>0 or OutsourcePatientPID>0) ";
             }
             else
             {
                 sql = sql + " and " + "  VW_patlbvw.Patauthicante='Authorized' ";
             }
             //sql = sql + " and " + " dbo.Get_Teststatus(VW_patlbvw.PID,VW_patlbvw.PatRegID,VW_patlbvw.Branchid,VW_patlbvw.FID) ='" + Patauthicante + "'";
         }
         if (testname != "")
         {
             sql = sql + " and " + " VW_patlbvw.subdeptName = '" + testname + "'";
         }
         sql = sql + " order by  PID desc";
         SqlDataAdapter da = new SqlDataAdapter(sql, conn);
         DataTable ds = new DataTable();
         try
         {
             conn.Open();

             da.Fill(ds);
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
         return ds;

     }
     public void AlterViewvw_VW_SignatureChange(object startDate, object endDate)
     {
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         SqlCommand sc = conn.CreateCommand();

         SqlCommand cmd = new SqlCommand(" alter view [VW_SignatureChange] as SELECT DISTINCT replace(convert(varchar, phrecdate,11),'/','')+CAST('0'+DailySeqNo AS varchar(255)) as DailyseqNo  , " +
           " Patmstid as SrNo, '' AS MainDept, RTRIM(patmst.intial) + ' ' + patmst.Patname AS Name, patmst.PID, patmst.PatRegID, patmst.FID, patmst.intial, patmst.Patname, patmst.sex, " +
           " patmst.Age, patmst.MDY, patmst.Phrecdate, patmst.Branchid, patmst.TestCharges, patmst.CenterCode, patmst.Drname, patmst.CenterName, patmstd.BarcodeID, " +
           " patmstd.SampleType, patmstd.MTCode AS STCODE, CAST(MainTest.Maintestname AS nvarchar(2000)) AS Testname, patmst.IsActive, patmst.Patphoneno, patmstd.OutLabName, " +
           " Ocl.OutsourceLabName, patmst.LabRegMediPro, patmst.Isemergency, patmstd.IspheboAccept ,patmst.PPID,patmstd.SpeciamRemark ,patmstd.ReqbyDoc,CAST(patmst.PatientcHistory AS nvarchar(3000)) AS PatientcHistory " +
           " ,patmstd.TechnicanFirst, " +
           " patmstd.TechnicanSecond, isnull(DRST.username,'') as FirstTechnican, isnull(DRST_1.username,'') AS SecondTechnican " +
           " FROM            patmst INNER JOIN " +
           " patmstd ON patmst.PID = patmstd.PID INNER JOIN " +
           " MainTest ON patmstd.MTCode = MainTest.MTCode AND patmstd.SDCode = MainTest.SDCode LEFT OUTER JOIN " +
           " DRST AS DRST_1 ON patmstd.TechnicanSecond = DRST_1.signatureid LEFT OUTER JOIN " +
           " DRST ON patmstd.TechnicanFirst = DRST.signatureid LEFT OUTER JOIN " +
           " Ocl ON patmstd.OutLabName = Ocl.Id  " +
           "  where patmstd.Patauthicante <>'Registered' and (patmst.IsActive = 1)  and patmst.Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "'   ", conn);
         try
         {
             conn.Open();
             cmd.ExecuteNonQuery();
         }
         catch (Exception exe)
         {
             throw;
         }
         finally
         {
             conn.Close();
             conn.Dispose();
         }



     }

     public DataSet Get_SignatureChange(object DoctorCode, object startDate, object endDate, string patientName, string Reg_no, int branchid, string maindept, string FID, int Number, string username, string UserType, string Barcode, string Mobno)
     {
         SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
         string sql = "Select * from VW_SignatureChange where  branchid=" + branchid + ""; //FID='" + FID + "' and ,convert(varchar, Phrecdate, 112)+''+CONVERT(nvarchar, DailyRowNo,(50)) as DailySeqNumber

         if (Reg_no != "")
         {
             sql = sql + " and (PatRegID='" + Reg_no + "' or PPID='" + Reg_no + "'  or LabRegMediPro='" + Reg_no + "'  or Barcodeid='" + Reg_no + "')";
         }
         else
         {
             if (patientName != "")
             {
                 sql = sql + " and Patname like  N'%" + patientName + "%'";
             }
             else
             {
                 if (DoctorCode != "" && DoctorCode.ToString() != "0")
                 {
                     sql += " and CenterCode='" + DoctorCode.ToString() + "'";
                 }

             }
         }
         if (startDate != null && endDate != null)
         {
             sql = sql + " and Phrecdate between '" + Convert.ToDateTime(startDate.ToString()).ToString("dd/MMM/yyyy") + "' and '" + Convert.ToDateTime(endDate.ToString()).ToString("dd/MMM/yyyy") + "' ";
         }
         if (Barcode != "All")
         {

             sql += " and IspheboAccept = '" + Barcode + "'";
         }
         if (Mobno != "")
         {
             sql += " and Patphoneno = '" + Mobno + "' ";
         }

         sql += " order by PID desc ";//srno
         SqlDataAdapter da = new SqlDataAdapter(sql, conn);
         DataSet ds = new DataSet();
         try
         {

             conn.Open();
             da.Fill(ds);
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
         return ds;
     }

     public DataTable GetFirstTechnicanName1(int branchid)
     {
         DataAccess data = new DataAccess();
         SqlConnection conn = data.ConInitForDC1();
         SqlCommand sc = new SqlCommand("SP_GetoutsourceLab", conn);


         sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;

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


     public DataTable GetFirstTechnicanName()
     {
         SqlConnection conn = DataAccess.ConInitForDC();

         SqlDataAdapter sc = new SqlDataAdapter("SELECT  distinct   DRST.signatureid, DRST.Drsignature FROM CTuser INNER JOIN DRST ON CTuser.Drid = DRST.signatureid INNER JOIN Deptwiseuser ON CTuser.username = Deptwiseuser.username INNER JOIN SubDepartment ON Deptwiseuser.DeptCodeID = SubDepartment.subdeptid where CTuser.Usertype='Technician'  ", conn);
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

}













