 
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Text;

public class Patmst_Bal_C
{
    public Patmst_Bal_C()
    {
        
        this.PatRegID = "";
        this.FID = "";
        this.Patname = "";
      
        this.Initial = "";
        
       
        this.Phone = "";
        this.Patregdate = Date.getdate();
        
        this.ReportDate = Date.getdate();
        this.ReportTime = "";
        this.Emergencyflag = false;
        this.DoctorCode = "";
        this.Drname = "";
        this.Age = 0;
        this.Sex = "";
        this.MYD = "";
        this.Phone = "";
        this.CenterCode = "";
        this.CenterName = "";  
        

        this.DrType = ""; 

        this.flag = false;
        this.PF = false;
       
        this.branchid = 1;
        this.FinancialYearID = "";
       
        this.TestCharges = 0;
        this.SampleID = "";
       

        this.Username = "";
        this.Usertype = "";
        this.Tests = "";

        this.SampleStatus = "";
      
        this.PatientcHistory = "";
        this.PID = 0;
        
    }

    public void getPermentId_HM(string PPPID)
    {

        SqlConnection conn = DataAccess.ConInitForHM();
        SqlCommand sc = new SqlCommand("SELECT        RTRIM(Initial.Title) + ' ' + PatientInformation.FirstName + ' ' + ISNULL(PatientInformation.LastName, '') AS Patname, Initial.Title, " +
                 "   PatientInformation.PatientInfoId, PatientInformation.PatRegId, PatientInformation.BarcodeId, PatientInformation.TitleId, PatientInformation.FirstName, PatientInformation.MiddleName, PatientInformation.LastName, " +
                 "   PatientInformation.PatMainTypeId, PatientInformation.PatientInsuTypeId, PatientInformation.PolicyNo, PatientInformation.GenderId, PatientInformation.BirthDate, PatientInformation.IsConfirmBirthDate, " +
                 "   PatientInformation.BloodGroup, PatientInformation.MaritalStatus, PatientInformation.GuardianTitleId, PatientInformation.GuardianName, PatientInformation.MobileNo, PatientInformation.PhoneNo, " +
                 "   PatientInformation.PatientAddress, PatientInformation.CountryId, PatientInformation.StateId, PatientInformation.CityId, PatientInformation.Email, PatientInformation.EntryDate, PatientInformation.ImagePath, " +
                 "   PatientInformation.CancelReason, PatientInformation.IsActive, PatientInformation.CreatedBy, PatientInformation.CreatedOn, PatientInformation.UpdatedBy, PatientInformation.UpdatedOn, PatientInformation.Age, " +
                 "   PatientInformation.AgeType, PatientInformation.BranchId, PatientInformation.FId, PatientInformation.Nationality, PatientInformation.BirthPlace, Gender.GenderName " +
                 "   FROM            PatientInformation INNER JOIN " +
                 "   Initial ON PatientInformation.TitleId = Initial.TitleId INNER JOIN " +
                 "   Gender ON PatientInformation.GenderId = Gender.GenderId where PAtregid='" + PPPID + "'", conn);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {


                this.Initial = sdr["Title"].ToString();
                this.Patname = sdr["Patname"].ToString();

                this.Sex = sdr["GenderName"].ToString();
                this.Age = Convert.ToInt32(sdr["Age"]);
                this.Phone = sdr["MobileNo"].ToString();
                if (Convert.ToString(sdr["AgeType"]) == "Years")
                {
                    this.MYD = "Year";
                }
                if (Convert.ToString(sdr["AgeType"]) == "Months")
                {
                    this.MYD = "Month";
                }
                if (Convert.ToString(sdr["AgeType"]) == "Days")
                {
                    this.MYD = "Day";
                }
                // this.MYD = sdr["MDY"].ToString();
                // this.RefDr = sdr["ReferenceDoctor"].ToString();
                this.Pataddress = sdr["PatientAddress"].ToString();
                this.DOB1 = Convert.ToDateTime(sdr["BirthDate"]);
                //  this.PatientcHistory = sdr["PatHistory"].ToString();
                if (sdr["Email"] != DBNull.Value)
                    Email = sdr["Email"].ToString();
                else
                    Email = "";

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

        }
        //return al;
    } 
    public void get_PermentId()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from patmst where PID=" + PID + "", conn);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {                
                this.PPID = Convert.ToInt32(sdr["PPID"]);

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

        }
        //return al;
    } 


    public bool Update_EmailId(string  PatRegID,string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update patmst set " +
            "Email=@EmailID  where PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 207)).Value = (PatRegID);
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 255)).Value = FID;
        sc.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.NVarChar, 200)).Value = this.Email;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch
            {

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
        }
        return true;
    }


    public Patmst_Bal_C(object PatReg_ID, string FID1, int branchid)
    {
        this.PatRegID = PatReg_ID.ToString();
        this.FID = Convert.ToString(FID1);

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandText = "SP_phpatmainrc";
        sc.CommandType = CommandType.StoredProcedure;

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        SqlDataReader sdr = null;
        conn.Close();
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            if (sdr != null && sdr.Read())
            {
               
                this.PatRegID = sdr["PatRegID"].ToString();
                this.FID = sdr["FID"].ToString();             
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.Initial = sdr["intial"].ToString();
                this.Patname = sdr["Patname"].ToString();
               
                this.Sex = sdr["sex"].ToString();
                this.Age = Convert.ToInt16(sdr["Age"]);
                this.MYD = sdr["MDY"].ToString();
                this.Pataddress = sdr["Pataddress"].ToString();
                this.Email = sdr["Email"].ToString();
                this.Phone = sdr["Patphoneno"].ToString();
                this.telNo = sdr["TelNo"].ToString();
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                if (sdr["Phrecdate"] != DBNull.Value)
                    this.Phrecdate = Convert.ToDateTime(sdr["Phrecdate"]);
                else
                    this.Phrecdate = Date.getMinDate().Date;
                
                if (sdr["ReportDate"] != DBNull.Value)
                    this.ReportDate = Convert.ToDateTime(sdr["ReportDate"]);
                else
                    this.ReportDate = Date.getMinDate();

                this.SampleType = sdr["SampleType"].ToString(); 
                this.DoctorCode = sdr["DoctorCode"].ToString();
                this.Drname = sdr["Drname"].ToString();

              
                this.CenterCode = sdr["CenterCode"].ToString();
                this.CenterName = sdr["CenterName"].ToString();
                this.Emergencyflag = Convert.ToBoolean(sdr["Isemergency"]);

               
                if (sdr["flag"] is DBNull)
                    this.flag = false;
                else
                    this.flag = Convert.ToBoolean(sdr["flag"]);
                this.PF = Convert.ToBoolean(sdr["PF"]);
               
                this.branchid = Convert.ToInt32(sdr["branchid"]); ;
                this.FinancialYearID = sdr["FinancialYearID"].ToString();
              
                if (sdr["TestCharges"] != DBNull.Value)
                    this.TestCharges = Convert.ToSingle(sdr["TestCharges"]);
                else
                    this.TestCharges = 0.0f;

                this.SampleID = sdr["SampleID"].ToString();
               
                this.Username = sdr["Username"].ToString();
                this.Pataddress = sdr["Pataddress"].ToString();
                if (sdr["SampleStatus"] != DBNull.Value)
                    this.SampleStatus = sdr["SampleStatus"].ToString();
                else
                    this.SampleStatus = "";
               
                this.PatientcHistory = sdr["PatientcHistory"].ToString();

                if (sdr["PID"] != DBNull.Value)
                    this.PID = Convert.ToInt32(sdr["PID"]);
                else
                    this.PID = 0;

                this.P_Patusername = Convert.ToString(sdr["Patusername"]);
                this.P_Patpassword = Convert.ToString(sdr["Patpassword"]);
                if (sdr["smsevent"] is DBNull)
                    this.smsevent = false;
                else
                    this.smsevent = Convert.ToBoolean(sdr["smsevent"]);


                this.P_Weights = sdr["Weights"].ToString();
                this.P_Heights = sdr["Heights"].ToString();

                this.P_Disease = sdr["Disease"].ToString();
                this.P_LastPeriod = sdr["LastPeriod"].ToString();

                this.P_Symptoms = sdr["Symptoms"].ToString();

                this.P_FSTime = sdr["FSTime"].ToString();
                this.P_Therapy = sdr["Therapy"].ToString();
                this.P_Doctoremail =Convert.ToString( sdr["Doctoremail"]);

                if (sdr["PPID"] != DBNull.Value)
                    this.PPID = Convert.ToInt32(sdr["PPID"]);
                else
                    this.PPID = 0;

                this.P_RefDoctorPhoneno = sdr["DoctorPhoneno"].ToString();

                if (sdr["DOB"] != DBNull.Value)
                    this.DOB = Convert.ToString(sdr["DOB"]);
                else
                    this.DOB = Convert.ToString(sdr["PatDatOfBirth"]); ;

                this.Patregdate1 = Convert.ToString(sdr["Patregdate1"]);
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
    } //public DrMT_Bal_C

   
 
    public Patmst_Bal_C(int PID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from patmst where PID=@PID and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();            
            if (sdr != null & sdr.Read())
            {
                this.PatRegID = sdr["PatRegID"].ToString();
                this.Phone = sdr["Patphoneno"].ToString();
                this.FID = sdr["FID"].ToString();
                this.Tests = sdr["Tests"].ToString();
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.Initial = sdr["intial"].ToString();
                this.Patname = sdr["Patname"].ToString();
               
                this.Sex = sdr["sex"].ToString();
                this.Age = Convert.ToInt16(sdr["Age"]);
                this.MYD = sdr["MDY"].ToString();
                this.Pataddress = sdr["Pataddress"].ToString();
                this.Email = sdr["EmailID"].ToString();
                this.Phone = sdr["Patphoneno"].ToString();
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                if (sdr["Phrecdate"] != DBNull.Value)
                    this.Phrecdate = Convert.ToDateTime(sdr["Phrecdate"]);
                else
                    this.Phrecdate = Date.getMinDate().Date;
                
                if (sdr["ReportDate"] != DBNull.Value)
                    this.ReportDate = Convert.ToDateTime(sdr["ReportDate"]);
                else
                    this.ReportDate = Date.getMinDate();

                this.SampleType = sdr["SampleType"].ToString();   

                this.DoctorCode = sdr["DoctorCode"].ToString();
                this.Drname = sdr["Drname"].ToString();

                this.CenterCode = sdr["CenterCode"].ToString();
                this.CenterName = sdr["CenterName"].ToString();

                this.Emergencyflag = Convert.ToBoolean(sdr["Isemergency"]);

               
                if (sdr["flag"] is DBNull)
                    this.flag = false;
                else
                    this.flag = Convert.ToBoolean(sdr["flag"]);
                this.PF = Convert.ToBoolean(sdr["PF"]);
                
                this.branchid = Convert.ToInt32(sdr["branchid"]); ;
                this.FinancialYearID = sdr["FinancialYearID"].ToString();
               
                if (sdr["TestCharges"] != DBNull.Value)
                    this.TestCharges = Convert.ToSingle(sdr["TestCharges"]);
                else
                    this.TestCharges = 0.0f;

                this.SampleID = sdr["SampleID"].ToString();
               
                this.Username = sdr["Username"].ToString();
                this.Pataddress = sdr["Pataddress"].ToString();
                if (sdr["SampleStatus"] != DBNull.Value)
                    this.SampleStatus = sdr["SampleStatus"].ToString();
                else
                    this.SampleStatus = "";
             
                this.PatientcHistory = sdr["PatientcHistory"].ToString();
               
              
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
        //return al;
    } //for default
   
   
    public bool Update(int PID, int branchid)
    {

        this.PID = PID;

        SqlConnection conn = DataAccess.ConInitForDC();


        SqlCommand sc = new SqlCommand("" +
       "Update patmst set Username=@Username,Usertype=@Usertype,Tests=@Tests,PatRegID=@PatRegID,FID=@FID,Patregdate=@Patregdate,intial=@intial,Patname=@Patname,sex=@sex,Age=@Age,MDY=@MDY,RefDr=@RefDr," +
       "CenterName=@CenterName,PF=@PF,Reportdate=@Reportdate,Phrecdate=@Phrecdate,Report_time=@Report_time,Patphoneno=@Patphoneno,Pataddress=@Pataddress,Isemergencyflag=@emergencyflag," +
       "DoctorCode=@DoctorCode,Drname=@Drname,flag=@flag,DrType=@DrType,CenterCode=@CenterCode,FinancialYearID=@FinancialYearID,Email=@EmailID,TestCharges=@TestCharges,SampleID=@SampleID,branchid=@branchid,SampleStatus=@SampleStatus,PatientcHistory=@PatientcHistory,TestName=@TestName" +
       " where  PID=@PID and branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = this.PID;
        sc.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50)).Value = this.Username;
        if (this.Usertype != null)
            sc.Parameters.Add(new SqlParameter("@Usertype", SqlDbType.NVarChar, 250)).Value = this.Usertype;
        else
            sc.Parameters.Add(new SqlParameter("@Usertype", SqlDbType.NVarChar, 250)).Value = "";

        if (this.Tests != null)
            sc.Parameters.Add(new SqlParameter("@Tests", SqlDbType.NText)).Value = this.Tests;
        else
            sc.Parameters.Add(new SqlParameter("@Tests", SqlDbType.NText)).Value = "";
        sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 4000)).Value = this.TestName;
        
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = this.PatRegID;

        sc.Parameters.Add(new SqlParameter("@DrType", SqlDbType.NVarChar, 2)).Value = this.DrType;

        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@Patregdate", SqlDbType.DateTime)).Value = this.Patregdate;
        sc.Parameters.Add(new SqlParameter("@intial", SqlDbType.NVarChar, 10)).Value = this.Initial;
        sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 350)).Value = this.Patname;
        
        sc.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar, 6)).Value = this.Sex;
        sc.Parameters.Add(new SqlParameter("@Age", SqlDbType.Int)).Value = (int)this.Age;
        sc.Parameters.Add(new SqlParameter("@MDY", SqlDbType.NVarChar, 5)).Value = this.MYD;
        sc.Parameters.Add(new SqlParameter("@RefDr", SqlDbType.NVarChar, 50)).Value = this.DoctorCode;

        sc.Parameters.Add(new SqlParameter("@PF", SqlDbType.Bit)).Value = this.PF;
        sc.Parameters.Add(new SqlParameter("@flag", SqlDbType.Bit)).Value = this.flag;
        sc.Parameters.Add(new SqlParameter("@emergencyflag", SqlDbType.Bit)).Value = this.Emergencyflag;
        sc.Parameters.Add(new SqlParameter("@Reportdate", SqlDbType.DateTime)).Value = this.ReportDate;
        sc.Parameters.Add(new SqlParameter("@Report_time", SqlDbType.NVarChar)).Value = this.ReportTime;

        if (this.Phrecdate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@Phrecdate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@Phrecdate", SqlDbType.DateTime)).Value = this.Phrecdate;

               sc.Parameters.Add(new SqlParameter("@Patphoneno", SqlDbType.NVarChar, 255)).Value = this.Phone;

        sc.Parameters.Add(new SqlParameter("@Pataddress", SqlDbType.NVarChar, 255)).Value = this.Pataddress;
        sc.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.NVarChar, 50)).Value = this.Email;

        sc.Parameters.Add(new SqlParameter("@CenterName", SqlDbType.NVarChar, 50)).Value = this.CenterName;
        
        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = this.DoctorCode;

        sc.Parameters.Add(new SqlParameter("@Drname", SqlDbType.NVarChar, 50)).Value = this.Drname;

        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 50)).Value = this.CenterCode;
       
        sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = this.FinancialYearID;

        

        sc.Parameters.Add(new SqlParameter("@TestCharges", SqlDbType.Float)).Value = (float)this.TestCharges;

        sc.Parameters.Add(new SqlParameter("@SampleID", SqlDbType.NVarChar, 50)).Value = this.SampleID;
      

        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@SampleStatus", SqlDbType.NVarChar, 100)).Value = this.SampleStatus;
      
        sc.Parameters.Add(new SqlParameter("@PatientcHistory", SqlDbType.NText)).Value = this.PatientcHistory;
 
        conn.Close();
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
                //if (sdr != null) sdr.Close();
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

   
    public int Insert(int branchid)
    {
        int PID = 1;   
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SP_phpatrecinfofrm", conn);

        sc.CommandType = CommandType.StoredProcedure;
     
        if (this.username != null)
            sc.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50)).Value = this.Username;
        else
            sc.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50)).Value = "";

        if (this.Usertype != null)
            sc.Parameters.Add(new SqlParameter("@Usertype", SqlDbType.NVarChar, 250)).Value = this.Usertype;
        else
            sc.Parameters.Add(new SqlParameter("@Usertype", SqlDbType.NVarChar, 250)).Value = "";
        if (this.P_Patusername != null)
            sc.Parameters.Add(new SqlParameter("@Patusername", SqlDbType.NVarChar, 250)).Value = this.P_Patusername;
        else
            sc.Parameters.Add(new SqlParameter("@Patusername", SqlDbType.NVarChar, 250)).Value = "";
        if (this.P_Patpassword != null)
            sc.Parameters.Add(new SqlParameter("@Patpassword", SqlDbType.NVarChar, 250)).Value = this.P_Patpassword;
        else
            sc.Parameters.Add(new SqlParameter("@Patpassword", SqlDbType.NVarChar, 250)).Value = "";
        if (this.Tests != null)
            sc.Parameters.Add(new SqlParameter("@Tests", SqlDbType.NText)).Value = this.Tests;
        else
            sc.Parameters.Add(new SqlParameter("@Tests", SqlDbType.NText)).Value = "";

       
        if (this.PatRegID == null)
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;

        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@Patregdate", SqlDbType.DateTime)).Value = this.Patregdate;
        sc.Parameters.Add(new SqlParameter("@intial", SqlDbType.NVarChar, 10)).Value = this.Initial;
        sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 35)).Value = this.Patname;
       
        sc.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar, 6)).Value = this.Sex;
        sc.Parameters.Add(new SqlParameter("@Age", SqlDbType.Int, 4)).Value = this.Age;
        sc.Parameters.Add(new SqlParameter("@MDY", SqlDbType.NVarChar, 5)).Value = this.MYD;
        sc.Parameters.Add(new SqlParameter("@RefDr", SqlDbType.NVarChar, 50)).Value = this.DoctorCode;
               
        sc.Parameters.Add(new SqlParameter("@DrType", SqlDbType.NVarChar, 2)).Value = this.DrType;

        sc.Parameters.Add(new SqlParameter("@PF", SqlDbType.Bit)).Value = this.PF;
               sc.Parameters.Add(new SqlParameter("@emergencyflag", SqlDbType.Bit)).Value = this.Emergencyflag;
        sc.Parameters.Add(new SqlParameter("@Reportdate", SqlDbType.DateTime)).Value = this.ReportDate;
        sc.Parameters.Add(new SqlParameter("@Report_time", SqlDbType.NVarChar, 50)).Value = this.ReportTime;
        if (this.Phrecdate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@Phrecdate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@Phrecdate", SqlDbType.DateTime)).Value = this.Phrecdate;
       
        sc.Parameters.Add(new SqlParameter("@Patphoneno", SqlDbType.NVarChar, 255)).Value = this.Phone;

        sc.Parameters.Add(new SqlParameter("@Pataddress", SqlDbType.NVarChar, 255)).Value = this.Pataddress;
               
        sc.Parameters.Add(new SqlParameter("@CenterName", SqlDbType.NVarChar, 50)).Value = this.CenterName;
       
        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = this.DoctorCode;
        sc.Parameters.Add(new SqlParameter("@Drname", SqlDbType.NVarChar, 50)).Value = this.Drname;

        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 50)).Value = this.CenterCode;
     
        sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = this.FinancialYearID;        
       
        sc.Parameters.Add(new SqlParameter("@TestCharges", SqlDbType.Float)).Value = this.TestCharges;
        
        sc.Parameters.Add(new SqlParameter("@SampleID", SqlDbType.NVarChar, 50)).Value = this.SampleID;
      
       
        sc.Parameters.Add(new SqlParameter("@SampleStatus", SqlDbType.NVarChar, 100)).Value = this.SampleStatus;

        if (this.SampleType == null)
            sc.Parameters.Add(new SqlParameter("@SampleType", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@SampleType", SqlDbType.NVarChar, 50)).Value = this.SampleType;
        
             sc.Parameters.Add(new SqlParameter("@PatientcHistory", SqlDbType.NText)).Value = this.PatientcHistory;
        
             sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 4000)).Value = this.TestName;
        sc.Parameters.Add(new SqlParameter("@PPID", SqlDbType.Int)).Value = this.P_PPID;
        if (this.Email != null)
            sc.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50)).Value = this.Email;
        else
            sc.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50)).Value = "";


        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@RegistratonDateTime", SqlDbType.DateTime)).Value = this.RegistratonDateTime;
        
        conn.Close();
        try
        {
            conn.Open();
            object objectCid;

            objectCid = sc.ExecuteScalar();

            if (objectCid != null)
            {
                PID = Convert.ToInt32(objectCid);
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
      
        return PID;
    } 

    public bool Delete(int PID, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("DELETE FROM patmst " +
                         "WHERE PID=@PID and branchid=" + branchid + "", conn);
        
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;

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
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        
        return true;
    } 

    public bool UpdateTest(int PID, string test, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update patmst set " +
            "Tests='" + test + "' where PID=" + PID + " and Branchid=" + branchid + "", conn);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch
            {

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
        }
       
        return true;
    }
    
    #region autogenerate logic to be implemented in stored procedure
    public int AutogenerateRegNo(int branchid)
    {
        
        int autono;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        try
        {
            conn.Open();
            sc = new SqlCommand("select max(PatRegID) as PatRegID from patmst where branchid=" + branchid + "", conn);

            object o = sc.ExecuteScalar();
            if (o != DBNull.Value)
                autono = Convert.ToInt32(o);
            else
                autono = 0;

            // This is not a while loop. It only loops once.

            if (autono != 0)
                autono = autono + 1;
            else
                autono = 1;
           
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch (Exception)
            {
                throw new Exception("Record not Found");
            }
        }
        return autono;
    }
   
    
    #endregion
   
    public string getcomplimentamount(string PatRegID, string FID, int PID, string branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select sum(dramt) as dramt from patmstd where PID=" + PID + " and branchid=" + branchid + "", conn);
        object obj = null;
        string complimentcharges = "0";
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
            { complimentcharges = Convert.ToString(obj); }
            if (complimentcharges == "")
                complimentcharges = "0";

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
        return complimentcharges;
    }
    internal class drNameTableException : Exception
    {
        public drNameTableException(string msg) : base(msg) { }
    }
    public void getPermentId()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from PatMT where PPID=" + P_PPID + "", conn);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {
               

                this.Initial = sdr["intial"].ToString();
                this.Patname = sdr["Patname"].ToString();
               
                this.Sex = sdr["sex"].ToString();
                this.Age = Convert.ToInt32(sdr["Age"]);
                this.Phone = sdr["MobileNo"].ToString();
                this.MYD = sdr["MDY"].ToString();
                this.RefDr = sdr["ReferenceDoctor"].ToString();
                this.Pataddress = sdr["Pataddress"].ToString();
             
                this.PatientcHistory = sdr["PatHistory"].ToString();
                if (sdr["Email"] != DBNull.Value)
                    Email = sdr["Email"].ToString();
                else
                    Email = "";

                if (sdr["DateOfBirth"] != DBNull.Value)
                    DateOfBirth = Convert.ToDateTime( sdr["DateOfBirth"]);
                
                this.AccDateofBirth = Convert.ToInt32(sdr["AccDateofBirth"]);
                
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

        }
        //return al;
    }

    public void getPermentCardId(string PatientCardNo)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from PatMT where Mobileno='" + PatientCardNo + "'", conn);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {


                this.Initial = sdr["intial"].ToString();
                this.Patname = sdr["Patname"].ToString();

                this.Sex = sdr["sex"].ToString();
                this.Age = Convert.ToInt32(sdr["Age"]);
                this.Phone = sdr["MobileNo"].ToString();
                this.MYD = sdr["MDY"].ToString();
                this.RefDr = sdr["ReferenceDoctor"].ToString();
                this.Pataddress = sdr["Pataddress"].ToString();

                this.PatientcHistory = sdr["PatHistory"].ToString();
                if (sdr["Email"] != DBNull.Value)
                    Email = sdr["Email"].ToString();
                else
                    Email = "";

                if (sdr["DateOfBirth"] != DBNull.Value)
                    DateOfBirth = Convert.ToDateTime(sdr["DateOfBirth"]);

                this.AccDateofBirth = Convert.ToInt32(sdr["AccDateofBirth"]);

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

        }
        //return al;
    } 

    public void getpatientinfo(string PatRegID, string FID)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand("Select * from patmst where PatRegID=@PatRegID and FID=@FID", conn);
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 255)).Value = FID;
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {
                this.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                this.Initial = sdr["intial"].ToString();
                this.Patname = sdr["Patname"].ToString();
               
                this.Sex = sdr["sex"].ToString();
                this.Age = Convert.ToInt16(sdr["Age"]);
                this.MYD = sdr["MDY"].ToString();
                
               
                this.branchid = Convert.ToInt32(sdr["branchid"]); ;
                this.FinancialYearID = sdr["FinancialYearID"].ToString();
                this.Username = sdr["Username"].ToString();
                this.PatRegID = sdr["PatRegID"].ToString();
                this.FID = sdr["FID"].ToString();
                this.Tests = sdr["Tests"].ToString();
                this.ReportDate = Convert.ToDateTime(sdr["Reportdate"]);
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
     public bool Update(int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();//Username=@Username,
            SqlCommand sc = new SqlCommand("" +
        "Update patmst set  Usertype=@Usertype,Tests=@Tests,PatRegID=@PatRegID,FID=@FID,Patregdate=@Patregdate,intial=@intial,Patname=@Patname,sex=@sex,Age=@Age,MDY=@MDY,RefDr=@RefDr," +
       "CenterName=@CenterName,PF=@PF,Reportdate=@Reportdate,Patphoneno=@telNo,Pataddress=@Pataddress,Isemergency=@emergencyflag," + //Phrecdate=@Phrecdate,exam_time=@exam_time,
       "DoctorCode=@DoctorCode,Drname=@Drname,flag=@flag,CenterCode=@CenterCode,FinancialYearID=@FinancialYearID,EmailID=@EmailID,Email=@EmailID,TestCharges=@TestCharges,SampleID=@SampleID,branchid=@branchid,SampleStatus=@SampleStatus,PatientcHistory=@PatientcHistory,TelNo=@Patphoneno,TestName=@TestName" +
       " , OtherRefDoctor=@OtherRefDoctor,Weights=@Weights,Heights=@Heights,Disease=@Disease,LastPeriod=@LastPeriod,Symptoms=@Symptoms,FSTime=@FSTime,Therapy=@Therapy,SocialMedia=@SocialMedia,Remark=@Remark,ClientTestCharges=@ClientTestCharges,DOB=@DOB where  PID=@PID and branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = this.PID;
       // sc.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50)).Value = this.Username;
        if (this.Usertype != null)
            sc.Parameters.Add(new SqlParameter("@Usertype", SqlDbType.NVarChar, 250)).Value = this.Usertype;
        else
            sc.Parameters.Add(new SqlParameter("@Usertype", SqlDbType.NVarChar, 250)).Value = "";

        if (this.Tests != null)
            sc.Parameters.Add(new SqlParameter("@Tests", SqlDbType.NText)).Value = this.Tests;
        else
            sc.Parameters.Add(new SqlParameter("@Tests", SqlDbType.NText)).Value = "";
        sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 4000)).Value = this.TestName;
       
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;      

        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@Patregdate", SqlDbType.DateTime)).Value = this.Patregdate;
        sc.Parameters.Add(new SqlParameter("@intial", SqlDbType.NVarChar, 10)).Value = this.Initial;
        sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 35)).Value = this.Patname;
   
        sc.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar, 6)).Value = this.Sex;
        sc.Parameters.Add(new SqlParameter("@Age", SqlDbType.Int)).Value = (int)this.Age;
        sc.Parameters.Add(new SqlParameter("@MDY", SqlDbType.NVarChar, 5)).Value = this.MYD;
        sc.Parameters.Add(new SqlParameter("@RefDr", SqlDbType.NVarChar, 50)).Value = this.DoctorCode;

        sc.Parameters.Add(new SqlParameter("@PF", SqlDbType.Bit)).Value = this.PF;
        sc.Parameters.Add(new SqlParameter("@flag", SqlDbType.Bit)).Value = this.flag;
        sc.Parameters.Add(new SqlParameter("@emergencyflag", SqlDbType.Bit)).Value = this.Emergencyflag;
        sc.Parameters.Add(new SqlParameter("@Reportdate", SqlDbType.DateTime)).Value = this.ReportDate;
       
        if (this.Phrecdate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@Phrecdate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@Phrecdate", SqlDbType.DateTime)).Value = this.Phrecdate;

       
        sc.Parameters.Add(new SqlParameter("@Patphoneno", SqlDbType.NVarChar, 255)).Value = this.Phone;
        sc.Parameters.Add(new SqlParameter("@telNo", SqlDbType.NVarChar, 255)).Value = this.telNo;
       
        sc.Parameters.Add(new SqlParameter("@Pataddress", SqlDbType.NVarChar, 255)).Value = this.Pataddress;
        sc.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.NVarChar, 50)).Value = this.Email;

        
        sc.Parameters.Add(new SqlParameter("@CenterName", SqlDbType.NVarChar, 50)).Value = this.CenterName;
        
        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = this.DoctorCode;

        sc.Parameters.Add(new SqlParameter("@Drname", SqlDbType.NVarChar, 50)).Value = this.Drname;

        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 50)).Value = this.CenterCode;
      
        sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = this.FinancialYearID;       

        sc.Parameters.Add(new SqlParameter("@TestCharges", SqlDbType.Float)).Value = (float)this.TestCharges;       

        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@SampleStatus", SqlDbType.NVarChar, 100)).Value = this.SampleStatus;
    
        sc.Parameters.Add(new SqlParameter("@PatientcHistory", SqlDbType.NText)).Value = this.PatientcHistory;
       
       
        sc.Parameters.Add(new SqlParameter("@SampleID", SqlDbType.NVarChar, 50)).Value = this.SampleID;
        sc.Parameters.Add(new SqlParameter("@OtherRefDoctor", SqlDbType.NVarChar, 50)).Value = this.OtherRefDoctor;

        sc.Parameters.Add(new SqlParameter("@Weights", SqlDbType.NVarChar, 500)).Value = P_Weights;
        sc.Parameters.Add(new SqlParameter("@Heights", SqlDbType.NVarChar, 500)).Value = P_Heights;
        sc.Parameters.Add(new SqlParameter("@Disease", SqlDbType.NVarChar, 500)).Value = P_Disease;
        sc.Parameters.Add(new SqlParameter("@LastPeriod", SqlDbType.NVarChar, 500)).Value = P_LastPeriod;
        sc.Parameters.Add(new SqlParameter("@Symptoms", SqlDbType.NVarChar, 500)).Value = P_Symptoms;
        sc.Parameters.Add(new SqlParameter("@FSTime", SqlDbType.NVarChar, 500)).Value = P_FSTime;
        sc.Parameters.Add(new SqlParameter("@Therapy", SqlDbType.NVarChar, 500)).Value = P_Therapy;
        sc.Parameters.Add(new SqlParameter("@SocialMedia", SqlDbType.Int, 4)).Value = P_SocialMedia;
        sc.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 50)).Value = this.Remarks;

        sc.Parameters.Add(new SqlParameter("@ClientTestCharges", SqlDbType.Float)).Value = (float)this.ClientTestCharges;

        sc.Parameters.Add(new SqlParameter("@DOB", SqlDbType.DateTime)).Value = DOB;
        conn.Close();
        //SqlDataReader sdr = null;

        try
        {
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
                //if (sdr != null) sdr.Close();
                conn.Close();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }


       
        // Implement Update logic.

        return true;

    } //update End


    #region properties

     private string Weights;
     public string P_Weights
     {
         get { return Weights; }
         set { Weights = value; }
     }
     private string Heights;
     public string P_Heights
     {
         get { return Heights; }
         set { Heights = value; }
     }
     private string Disease;
     public string P_Disease
     {
         get { return Disease; }
         set { Disease = value; }
     }

     private string LastPeriod;
     public string P_LastPeriod
     {
         get { return LastPeriod; }
         set { LastPeriod = value; }
     }
     private string Symptoms;
     public string P_Symptoms
     {
         get { return Symptoms; }
         set { Symptoms = value; }
     }
     private string FSTime;
     public string P_FSTime
     {
         get { return FSTime; }
         set { FSTime = value; }
     }
     private string Therapy;
     public string P_Therapy
     {
         get { return Therapy; }
         set { Therapy = value; }
     }
     private string _ImagePath;
     public string ImagePath
     {
         get { return _ImagePath; }
         set { _ImagePath = value; }
     }
     private string PatCard;
     public string P_PatCard
     {
         get { return PatCard; }
         set { PatCard = value; }
     }
     private string PatCardExp;
     public string P_PatCardExp
     {
         get { return PatCardExp; }
         set { PatCardExp = value; }
     }

     private int AccBirthdate;
     public int P_AccBirthdate
     {
         get { return AccBirthdate; }
         set { AccBirthdate = value; }
     }

    private int _SrNo;
    public int SrNo
    {
        get { return _SrNo; }
        set { _SrNo = value; }
    }

    private string usertype;
    public string Usertype
    {
        get { return usertype; }
        set { usertype = value; }
    }

    private string username;
    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    private string _Tests;
    public string Tests
    {
        get { return _Tests; }
        set { _Tests = value; }
    }


    private string _PatRegID;
    public string PatRegID
    {
        get { return _PatRegID; }
        set { _PatRegID = value; }
    }

    private string _FID;
    public string FID
    {
        get { return _FID; }
        set { _FID = value; }
    }

    private string _Patname;
    public string Patname
    {
        get { return _Patname; }
        set { _Patname = value; }
    }

   
    

    private string _RegCode;

    public string RegCode
    {
        get { return _RegCode; }
        set { _RegCode = value; }
    }
    private string _UploadPrescription;

    public string UploadPrescription
    {
        get { return _UploadPrescription; }
        set { _UploadPrescription = value; }
    }

    private string _Remarks;

    public string Remarks
    {
        get { return _Remarks; }
        set { _Remarks = value; }
    }

    private string _Initial;

    public string Initial
    {
        get { return _Initial; }
        set { _Initial = value; }
    }

    private string _Pataddress, _City;

    public string Pataddress
    {
        get { return _Pataddress; }
        set { _Pataddress = value; }
    }
    private DateTime _DOB1;
    public DateTime DOB1
    {
        get { return _DOB1; }
        set { _DOB1 = value; }
    }

    private int _AccDateofBirth;
    public int AccDateofBirth
    {
        get { return _AccDateofBirth; }
        set { _AccDateofBirth = value; }
    }

    private DateTime _DateOfBirth;
    public DateTime DateOfBirth
    {
        get { return _DateOfBirth; }
        set { _DateOfBirth = value; }
    }

    private string _DOB;
    public string DOB
    {
        get { return _DOB; }
        set { _DOB = value; }
    }
    private string _Email;
    public string Email
    {
        get { return _Email; }
        set { _Email = value; }
    }

    private string _Phone;
    public string Phone
    {
        get { return _Phone; }
        set { _Phone = value; }
    }
    private string _telNo;
     public string telNo
    {
        get { return _telNo; }
        set { _telNo = value; }
    }
    
    private string _RefDr;
    public string RefDr
    {
        get { return _RefDr; }
        set { _RefDr = value; }
    }

    private DateTime _Patregdate;
    public DateTime Patregdate
    {
        get { return _Patregdate; }
        set { _Patregdate = value; }
    }

    private string _Patregdate1;
    public string Patregdate1
    {
        get { return _Patregdate1; }
        set { _Patregdate1 = value; }
    }

    private DateTime _Phrecdate;
    public DateTime Phrecdate
    {
        get { return _Phrecdate; }
        set { _Phrecdate = value; }
    }

    
    private DateTime _ReportDate;
    public DateTime ReportDate
    {
        get { return _ReportDate; }
        set { _ReportDate = value; }
    }

    private string _ReportTime;
    public string ReportTime
    {
        get { return _ReportTime; }
        set { _ReportTime = value; }
    }

    private bool _Emergencyflag;
    public bool Emergencyflag
    {
        get { return _Emergencyflag; }
        set { _Emergencyflag = value; }
    }

    private int _Age;
    public int Age
    {
        get { return _Age; }
        set { _Age = value; }
    }

    private string _Sex;
    public string Sex
    {
        get { return _Sex; }
        set { _Sex = value; }
    }

    private string _MYD;
    public string MYD
    {
        get { return _MYD; }
        set { _MYD = value; }
    }

    private string _DoctorCode;
    public string DoctorCode
    {
        get { return _DoctorCode; }
        set { _DoctorCode = value; }
    }
    private string _Drname;
    public string Drname
    {
        get { return _Drname; }
        set { _Drname = value; }
    }
    private string _CenterCode;
    public string CenterCode
    {
        get { return _CenterCode; }
        set { _CenterCode = value; }
    }
    private string _CenterName;
    public string CenterName
    {
        get { return _CenterName; }
        set { _CenterName = value; }
    }
    private string _OtherRefDoctor;
     public string OtherRefDoctor
    {
        get { return _OtherRefDoctor; }
        set { _OtherRefDoctor = value; }
    } 
    
    private string _DeptCode;
    public string DeptCode
    {
        get { return _DeptCode; }
        set { _DeptCode = value; }
    }
    private bool _PF;
    public bool PF
    {
        get { return _PF; }
        set { _PF = value; }
    }

    private int _branchid;
    public int branchid
    {
        get { return _branchid; }
        set { _branchid = value; }
    }

    private string _FinancialYearID;
    public string FinancialYearID
    {
        get { return _FinancialYearID; }
        set { _FinancialYearID = value; }
    }

    

    private Double _TestCharges;
    public Double TestCharges
    {
        get { return _TestCharges; }
        set { _TestCharges = value; }
    }

    private Double _ClientTestCharges;
    public Double ClientTestCharges
    {
        get { return _ClientTestCharges; }
        set { _ClientTestCharges = value; }
    }

    private string _SampleID;
    public string SampleID
    {
        get { return _SampleID; }
        set { _SampleID = value; }
    }
   

   

    private bool _flag;
    public bool flag
    {
        get { return _flag; }
        set { _flag = value; }
    }

    

    private string _DrType;
    public string DrType
    {
        get { return _DrType; }
        set { _DrType = value; }
    }

    private string sampleType;
    public string SampleType
    {
        get { return sampleType; }
        set { sampleType = value; }
    }

    private string sampleStatus;
    public string SampleStatus
    {
        get { return sampleStatus; }
        set { sampleStatus = value; }
    }

  
    private string _clinical_History;
    public string PatientcHistory
    {
        get { return _clinical_History; }
        set { _clinical_History = value; }
    }

    private int _PID;
    public int PID
    {
        get { return _PID; }
        set { _PID = value; }
    }
  
   
   
    private DateTime _RegistratonDateTime;
    public DateTime RegistratonDateTime
    {
        get { return _RegistratonDateTime; }
        set { _RegistratonDateTime = value; }
    }

   
    private string Patusername;
    public string P_Patusername
    {
        get { return Patusername; }
        set { Patusername = value; }
    }

    private string Patpassword;
    public string P_Patpassword
    {
        get { return Patpassword; }
        set { Patpassword = value; }
    }
    private string complimentcharges;
    public string P_complimentcharges
    {
        get { return complimentcharges; }
        set { complimentcharges = value; }
    }
    private int DigModule;
    public int P_DigModule
    {
        get { return DigModule; }
        set { DigModule = value; }
    }

   

    public string testname1;
    public string TestName
    {

        get { return testname1; }
        set { testname1 = value; }


    }

  
    private int PPID;
    public int P_PPID
    {
        get { return PPID; }
        set { PPID = value; }

    }

    private bool smsevent;
    public bool P_smsevent
    {
        get { return smsevent; }
        set { smsevent = value; }
    }

    private int SocialMedia;
    public int P_SocialMedia
    {
        get { return SocialMedia; }
        set { SocialMedia = value; }
    }

    private string  Doctoremail;
    public string P_Doctoremail
    {
        get { return Doctoremail; }
        set { Doctoremail = value; }
    }

    private string RefDoctorPhoneno;
    public string P_RefDoctorPhoneno
    {
        get { return RefDoctorPhoneno; }
        set { RefDoctorPhoneno = value; }
    }
    #endregion

  
    public static string getname(string DrCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select DoctorCode from DrMT where DoctorCode=N'" + DrCode + "' and branchid=" + branchid + " and DrType='CC'", conn);

        SqlDataReader sdr = null;
        string Drname = "";
        try
        {
            conn.Open();
            sdr = cmd.ExecuteReader();

            if (sdr != null & sdr.Read())
            {
                Drname = sdr["DoctorCode"].ToString();

            }
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
        return Drname;
    }

    public bool getallreadyRegister(DateTime Patregdate, string Patname, int Age, int CountryCode)
    {
        bool flag = true;
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand();
        if (CountryCode == 2)
        {
            sc = new SqlCommand(" SELECT  COUNT(*)  FROM  patmst WHERE  Patregdate between convert(varchar,   DATEADD(MINUTE,-1,GETDATE()), 21) and  convert(varchar,  DATEADD(MINUTE,1,GETDATE()), 21)  AND Patname=@Patname AND Age=@Age ", conn);

        }
        else
        {
            sc = new SqlCommand(" SELECT  COUNT(*)  FROM  patmst WHERE  Patregdate between convert(varchar,   DATEADD(second,-30,GETDATE()), 21) and  convert(varchar,  DATEADD(second,30,GETDATE()), 21)  AND Patname=@Patname AND Age=@Age ", conn);


        }
        //    sc.Parameters.Add(new SqlParameter("@Patregdate", SqlDbType.DateTime)).Value = Patregdate;
        sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 250)).Value = Patname;
        sc.Parameters.Add(new SqlParameter("@Age", SqlDbType.Int, 4)).Value = Age;

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

    public void UpdateSmsFlag(string PatRegID, string FID, int branchid, bool smsevent)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {
            SqlCommand sc = new SqlCommand();
            sc.Connection = conn;
            sc.CommandText = "SP_phsmrecmst";
            sc.CommandType = CommandType.StoredProcedure;
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = FID;
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@smsevent", SqlDbType.Int)).Value = smsevent;
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

  
    public float getcomplimentamount(string PatRegID, string FID, int PID, string branchid, string MTCode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand();
        string[] tcd = MTCode.Trim().Split(',');
        float complimentcharges = 0;
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
                    cmd = new SqlCommand("select dramt from patmstd where PID=" + PID + " and branchid=" + branchid + "and MTCode='" + tename + "'", conn);
                    obj = cmd.ExecuteScalar();
                }
                else
                {
                    cmd = new SqlCommand("select distinct dramt as dramt from patmstd where PID=" + PID + " and branchid=" + branchid + "and PackageCode='" + tename + "'", conn);
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
                            complimentcharges += TestAmount;
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
        return complimentcharges;
    }

    public int getPID(string PatRegID, string FID, int branchid)
    {
        int i = -1;
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {
            SqlCommand cmd = new SqlCommand(" select PID from patmst where PatRegID='" + PatRegID + "' and FID ='" + FID + "' and branchid=" + branchid + "", conn);
            object ob = "";
            conn.Open();
            ob = cmd.ExecuteScalar();
            if (ob != DBNull.Value)
            {
                if (ob != "")
                {
                    i = Convert.ToInt32(ob);
                }
            }
        }
        catch (Exception ex) { throw; }
        finally
        {
            conn.Close(); conn.Dispose();
        }
        return i;
    }
    public bool GetRadiologyTest(string SDCode, int branchid)
    {
        bool flag = true;
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand("select Count(*) from SubDepartment where (SDCode='BC' or SDCode='FN') ", conn);

        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 500)).Value = SDCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
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

  

    public static string GetConttpersonac(string DoctorCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select Contactperson from DrMT where DoctorCode=N'" + DoctorCode + "' and branchid=" + branchid + " and DrType='CC'", conn);

        SqlDataReader sdr = null;
        string Drname = "";
        try
        {
            conn.Open();
            sdr = cmd.ExecuteReader();

            if (sdr != null & sdr.Read())
            {
                Drname = sdr["Contactperson"].ToString();

            }
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
        return Drname;
    }

    public bool getallreadyRegister_RadMst_Histo(string Patregid, string MTCode)
    {
        bool flag = true;
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand(" SELECT  COUNT(*)  FROM  radmst_Histo WHERE   Patregid=@Patregid AND MTCode=@MTCode ", conn);


        sc.Parameters.Add(new SqlParameter("@Patregid", SqlDbType.NVarChar, 35)).Value = Patregid;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

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
    public bool getallreadyRegister_RadMst(string Patregid, string MTCode)
    {
        bool flag = true;
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand(" SELECT  COUNT(*)  FROM  radmst WHERE   Patregid=@Patregid AND MTCode=@MTCode ", conn);


        sc.Parameters.Add(new SqlParameter("@Patregid", SqlDbType.NVarChar, 35)).Value = Patregid;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

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


    public bool getallreadyRegister_RadMst_Cyto(string Patregid, string MTCode)
    {
        bool flag = true;
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand(" SELECT  COUNT(*)  FROM  radmst_Cyto WHERE   Patregid=@Patregid AND MTCode=@MTCode ", conn);


        sc.Parameters.Add(new SqlParameter("@Patregid", SqlDbType.NVarChar, 35)).Value = Patregid;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

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

    public Patmst_Bal_C(object PatReg_ID, string FID1, int branchid, int Id)
    {
        this.PatRegID = PatReg_ID.ToString();
        this.FID = Convert.ToString(FID1);

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandText = "SP_patmstd_Demography";
        sc.CommandType = CommandType.StoredProcedure;

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        SqlDataReader sdr = null;
        conn.Close();
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            if (sdr != null && sdr.Read())
            {

                this.P_Weights = sdr["Weights"].ToString();
                this.P_Heights = sdr["Heights"].ToString();

                this.P_Disease = sdr["Disease"].ToString();
                this.P_LastPeriod = sdr["LastPeriod"].ToString();

                this.P_Symptoms = sdr["Symptoms"].ToString();

                this.P_FSTime = sdr["FSTime"].ToString();
                this.P_Therapy = sdr["Therapy"].ToString();
               
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
    } //public DrMT_Bal_C


    public bool Update_PatientInfo(int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
    "Update patmst set  Username=@Username,Usertype=@Usertype,PatRegID=@PatRegID,FID=@FID,Patregdate=@Patregdate,intial=@intial,Patname=@Patname,sex=@sex,Age=@Age,MDY=@MDY,RefDr=@RefDr," +
   "CenterName=@CenterName,PF=@PF,Reportdate=@Reportdate,Patphoneno=@Patphoneno,Pataddress=@Pataddress,Isemergency=@emergencyflag," + //Phrecdate=@Phrecdate,exam_time=@exam_time,
   "DoctorCode=@DoctorCode,Drname=@Drname,flag=@flag,CenterCode=@CenterCode,FinancialYearID=@FinancialYearID,EmailID=@EmailID,Email=@EmailID,branchid=@branchid,SampleStatus=@SampleStatus,PatientcHistory=@PatientcHistory,TelNo=@Patphoneno " +
   " , OtherRefDoctor=@OtherRefDoctor,Weights=@Weights,Heights=@Heights,Disease=@Disease,LastPeriod=@LastPeriod,Symptoms=@Symptoms,FSTime=@FSTime,Therapy=@Therapy,SocialMedia=@SocialMedia where  PID=@PID and branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = this.PID;
        sc.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50)).Value = this.Username;
        if (this.Usertype != null)
            sc.Parameters.Add(new SqlParameter("@Usertype", SqlDbType.NVarChar, 250)).Value = this.Usertype;
        else
            sc.Parameters.Add(new SqlParameter("@Usertype", SqlDbType.NVarChar, 250)).Value = "";

        if (this.Tests != null)
            sc.Parameters.Add(new SqlParameter("@Tests", SqlDbType.NText)).Value = this.Tests;
        else
            sc.Parameters.Add(new SqlParameter("@Tests", SqlDbType.NText)).Value = "";
        sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 4000)).Value = this.TestName;

        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;

        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 255)).Value = this.FID;
        sc.Parameters.Add(new SqlParameter("@Patregdate", SqlDbType.DateTime)).Value = this.Patregdate;
        sc.Parameters.Add(new SqlParameter("@intial", SqlDbType.NVarChar, 10)).Value = this.Initial;
        sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 35)).Value = this.Patname;

        sc.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar, 6)).Value = this.Sex;
        sc.Parameters.Add(new SqlParameter("@Age", SqlDbType.Int)).Value = (int)this.Age;
        sc.Parameters.Add(new SqlParameter("@MDY", SqlDbType.NVarChar, 5)).Value = this.MYD;
        sc.Parameters.Add(new SqlParameter("@RefDr", SqlDbType.NVarChar, 50)).Value = this.DoctorCode;

        sc.Parameters.Add(new SqlParameter("@PF", SqlDbType.Bit)).Value = this.PF;
        sc.Parameters.Add(new SqlParameter("@flag", SqlDbType.Bit)).Value = this.flag;
        sc.Parameters.Add(new SqlParameter("@emergencyflag", SqlDbType.Bit)).Value = this.Emergencyflag;
        sc.Parameters.Add(new SqlParameter("@Reportdate", SqlDbType.DateTime)).Value = this.ReportDate;

        if (this.Phrecdate == Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@Phrecdate", SqlDbType.DateTime)).Value = DBNull.Value;
        else
            sc.Parameters.Add(new SqlParameter("@Phrecdate", SqlDbType.DateTime)).Value = this.Phrecdate;


        sc.Parameters.Add(new SqlParameter("@Patphoneno", SqlDbType.NVarChar, 255)).Value = this.Phone;

        sc.Parameters.Add(new SqlParameter("@Pataddress", SqlDbType.NVarChar, 255)).Value = this.Pataddress;
        sc.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.NVarChar, 50)).Value = this.Email;


        sc.Parameters.Add(new SqlParameter("@CenterName", SqlDbType.NVarChar, 50)).Value = this.CenterName;

        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = this.DoctorCode;

        sc.Parameters.Add(new SqlParameter("@Drname", SqlDbType.NVarChar, 50)).Value = this.Drname;

        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 50)).Value = this.CenterCode;

        sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = this.FinancialYearID;

        sc.Parameters.Add(new SqlParameter("@TestCharges", SqlDbType.Float)).Value = (float)this.TestCharges;

        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@SampleStatus", SqlDbType.NVarChar, 100)).Value = this.SampleStatus;

        sc.Parameters.Add(new SqlParameter("@PatientcHistory", SqlDbType.NText)).Value = this.PatientcHistory;


        sc.Parameters.Add(new SqlParameter("@SampleID", SqlDbType.NVarChar, 50)).Value = this.SampleID;
        sc.Parameters.Add(new SqlParameter("@OtherRefDoctor", SqlDbType.NVarChar, 50)).Value = this.OtherRefDoctor;

        sc.Parameters.Add(new SqlParameter("@Weights", SqlDbType.NVarChar, 500)).Value = P_Weights;
        sc.Parameters.Add(new SqlParameter("@Heights", SqlDbType.NVarChar, 500)).Value = P_Heights;
        sc.Parameters.Add(new SqlParameter("@Disease", SqlDbType.NVarChar, 500)).Value = P_Disease;
        sc.Parameters.Add(new SqlParameter("@LastPeriod", SqlDbType.NVarChar, 500)).Value = P_LastPeriod;
        sc.Parameters.Add(new SqlParameter("@Symptoms", SqlDbType.NVarChar, 500)).Value = P_Symptoms;
        sc.Parameters.Add(new SqlParameter("@FSTime", SqlDbType.NVarChar, 500)).Value = P_FSTime;
        sc.Parameters.Add(new SqlParameter("@Therapy", SqlDbType.NVarChar, 500)).Value = P_Therapy;
        sc.Parameters.Add(new SqlParameter("@SocialMedia", SqlDbType.Int, 4)).Value = P_SocialMedia;

        conn.Close();
        //SqlDataReader sdr = null;

        try
        {
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
                //if (sdr != null) sdr.Close();
                conn.Close();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }



        // Implement Update logic.

        return true;

    } //update End


    public bool getallreadyRegister_ReferenceDoctor(string ReferenceDoctor)
    {
        bool flag = true;
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand(" SELECT  COUNT(*)  FROM  patmt WHERE   ReferenceDoctor=@ReferenceDoctor  ", conn);


        sc.Parameters.Add(new SqlParameter("@ReferenceDoctor", SqlDbType.NVarChar, 50)).Value = ReferenceDoctor;
     
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


//class 

