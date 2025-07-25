using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
/// <summary>
/// Summary description for Appointment_C
/// </summary>
public class Appointment_C
{
	public Appointment_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    private string _Initial; public string Initial { get { return _Initial; } set { _Initial = value; } }
    private string _Patname; public string Patname { get { return _Patname; } set { _Patname = value; } }
    private string _Gender; public string Gender { get { return _Gender; } set { _Gender = value; } }
    private string _Age; public string Age { get { return _Age; } set { _Age = value; } }
    private string _AgeType; public string AgeType { get { return _AgeType; } set { _AgeType = value; } }
    private string _Phone; public string Phone { get { return _Phone; } set { _Phone = value; } }
    private string _Email; public string Email { get { return _Email; } set { _Email = value; } }
    private string _Pataddress, _City; public string Pataddress { get { return _Pataddress; } set { _Pataddress = value; } }
    public string City { get { return _City; } set { _City = value; } }
    private string _State; public string State { get { return _State; } set { _State = value; } }
    private string _District; public string District { get { return _District; } set { _District = value; } }
    private string _RefDr; public string RefDr { get { return _RefDr; } set { _RefDr = value; } }
    private string _Note; public string Note { get { return _Note; } set { _Note = value; } }
    private string _HospitalName; public string HospitalName { get { return _HospitalName; } set { _HospitalName = value; } }
    private string _HospitalID; public string HospitalID { get { return _HospitalID; } set { _HospitalID = value; } }

    private bool _ILI; public bool ILI { get { return _ILI; } set { _ILI = value; } }
    private bool _Fever; public bool Fever { get { return _Fever; } set { _Fever = value; } }
    private string _FeverDuration; public string FeverDuration { get { return _FeverDuration; } set { _FeverDuration = value; } }
    private bool _Cough; public bool Cough { get { return _Cough; } set { _Cough = value; } }
    private string _CoughDuration; public string CoughDuration { get { return _CoughDuration; } set { _CoughDuration = value; } }
    private bool _SARI; public bool SARI { get { return _SARI; } set { _SARI = value; } }

    private string _CoMorbidity; public string CoMorbidity { get { return _CoMorbidity; } set { _CoMorbidity = value; } }
    private string _Tempetrature; public string Tempetrature { get { return _Tempetrature; } set { _Tempetrature = value; } }

    private string _Symptom; public string Symptom { get { return _Symptom; } set { _Symptom = value; } }
    private string _SymptomAddition; public string SymptomAddition { get { return _SymptomAddition; } set { _SymptomAddition = value; } }
    private string  _TravelLast; public string TravelLast { get { return _TravelLast; } set { _TravelLast = value; } }
    private string _TravelLastVisit; public string TravelLastVisit { get { return _TravelLastVisit; } set { _TravelLastVisit = value; } }
    private string _PatientAdmited; public string PatientAdmited { get { return _PatientAdmited; } set { _PatientAdmited = value; } }
    private string _SlotTime; public string SlotTime { get { return _SlotTime; } set { _SlotTime = value; } }
    private string _CenterCode; public string CenterCode { get { return _CenterCode; } set { _CenterCode = value; } }
    private string _CreatedBy; public string CreatedBy { get { return _CreatedBy; } set { _CreatedBy = value; } }

    private string _FID;    public string FID    {        get { return _FID; }        set { _FID = value; }    }
    private string _Patregdate; public string Patregdate { get { return _Patregdate; } set { _Patregdate = value; } }
    private int _CenterID; public int CenterID { get { return _CenterID; } set { _CenterID = value; } }
    private int _AppId; public int AppId { get { return _AppId; } set { _AppId = value; } }
    private int _SlotId; public int SlotId { get { return _SlotId; } set { _SlotId = value; } }
    private string _SlotEnd; public string SlotEnd { get { return _SlotEnd; } set { _SlotEnd = value; } }
    private string _PatregdateEnd; public string PatregdateEnd { get { return _PatregdateEnd; } set { _PatregdateEnd = value; } }

    private int _DirectApp; public int DirectApp { get { return _DirectApp; } set { _DirectApp = value; } }
    private int _HomeCollectionPerson; public int HomeCollectionPerson { get { return _HomeCollectionPerson; } set { _HomeCollectionPerson = value; } }

    
    //private Date dddd
    public  DataTable ReadDataSlot(string doctorId, string findDate, string start, string end)
    {
        DataTable dt = new DataTable();

        //string[] val = findDate.Split('-');

        //string newDate = val[2] + "-" + val[1] + "-" + val[0];

        string myconnection = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;

        using (SqlConnection connection =
                   new SqlConnection(myconnection))
        {
            using (SqlCommand cmd = new SqlCommand("usp_GetTimeSlotsByDoctorId", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@docId", SqlDbType.VarChar).Value = doctorId;
                cmd.Parameters.Add("@findDate", SqlDbType.Date).Value = Convert.ToDateTime(findDate);
                //cmd.Parameters.Add("@dtStart", SqlDbType.VarChar).Value = start;
                //cmd.Parameters.Add("@dtEnd", SqlDbType.VarChar).Value = end;

                connection.Open();

                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt);
                }
            }
        }

        return dt;
    }
    public bool Insert_RegisterAppoinment(int branchid)
    {

        SqlConnection con = DataAccess.ConInitForDC();
      //  Date dateofentrynew = Convert. this.Patregdate;
        SqlCommand cmd = new SqlCommand("SP_Insert_Appointment", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Initial", SqlDbType.NVarChar, 50)).Value = this.Initial;
        cmd.Parameters.Add(new SqlParameter("@PatientFirstName", SqlDbType.NVarChar, 250)).Value = this.Patname;

        cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 50)).Value = this.Gender;
        cmd.Parameters.Add(new SqlParameter("@Age", SqlDbType.NVarChar, 250)).Value = this.Age;
        cmd.Parameters.Add(new SqlParameter("@AgeType", SqlDbType.NVarChar, 50)).Value = this.AgeType;
        cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 250)).Value = this.Phone;
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50)).Value = this.Email;
        cmd.Parameters.Add(new SqlParameter("@Pataddress", SqlDbType.NVarChar, 550)).Value = this.Pataddress;
        cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 250)).Value = this.City;
        cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar, 250)).Value = this.State;

        cmd.Parameters.Add(new SqlParameter("@District", SqlDbType.NVarChar, 250)).Value = this.District;
        cmd.Parameters.Add(new SqlParameter("@RefDr", SqlDbType.NVarChar, 250)).Value = this.RefDr;
        cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar, 550)).Value = this.Note;
        cmd.Parameters.Add(new SqlParameter("@HospitalName", SqlDbType.NVarChar, 550)).Value = this.HospitalName;
        cmd.Parameters.Add(new SqlParameter("@HospitalID", SqlDbType.NVarChar, 50)).Value = this.HospitalID;
        cmd.Parameters.Add(new SqlParameter("@ILI", SqlDbType.Bit)).Value = this.ILI;
        cmd.Parameters.Add(new SqlParameter("@Fever", SqlDbType.Bit)).Value = this.Fever;
        cmd.Parameters.Add(new SqlParameter("@Cough", SqlDbType.Bit)).Value = this.Cough;
        cmd.Parameters.Add(new SqlParameter("@SARI", SqlDbType.Bit)).Value = this.SARI;
        cmd.Parameters.Add(new SqlParameter("@FeverDuration", SqlDbType.NVarChar, 250)).Value = this.FeverDuration;
        cmd.Parameters.Add(new SqlParameter("@CoughDuration", SqlDbType.NVarChar, 250)).Value = this.CoughDuration;

        cmd.Parameters.Add(new SqlParameter("@CoMorbidity", SqlDbType.NVarChar, 250)).Value = this.CoMorbidity;
        cmd.Parameters.Add(new SqlParameter("@Tempetrature", SqlDbType.NVarChar, 250)).Value = this.Tempetrature;

        cmd.Parameters.Add(new SqlParameter("@Symptom", SqlDbType.NVarChar, 250)).Value = this.Symptom;
        cmd.Parameters.Add(new SqlParameter("@SymptomAddition", SqlDbType.NVarChar, 250)).Value = this.SymptomAddition;
        cmd.Parameters.Add(new SqlParameter("@TravelLast", SqlDbType.NVarChar, 250)).Value = this.TravelLast;
        cmd.Parameters.Add(new SqlParameter("@TravelLastVisit", SqlDbType.NVarChar, 250)).Value = this.TravelLastVisit;

        cmd.Parameters.Add(new SqlParameter("@PatientAdmited", SqlDbType.NVarChar, 250)).Value = this.PatientAdmited;
        //cmd.Parameters.Add(new SqlParameter("@AppTime", SqlDbType.NVarChar, 250)).Value = this.SlotTime.Trim();
        cmd.Parameters.Add("@AppTime", SqlDbType.DateTime).Value = (Convert.ToDateTime(SlotTime)).ToString("hh:mm:ss tt", CultureInfo.CurrentCulture);
        cmd.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 250)).Value = this.CenterCode;
        cmd.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 250)).Value = this.CreatedBy;

        cmd.Parameters.Add(new SqlParameter("@AppDate", SqlDbType.Date)).Value = this.Patregdate;
        cmd.Parameters.Add(new SqlParameter("@DoctorId", SqlDbType.Int)).Value = this.CenterID;
        cmd.Parameters.Add(new SqlParameter("@DirectApp", SqlDbType.Int)).Value = this.DirectApp;
        cmd.Parameters.Add(new SqlParameter("@HomeCollectionPerson", SqlDbType.Int)).Value = this.HomeCollectionPerson;

        
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

    public DataTable Get_Patient_Appointmentt(string PatRegID, string fromdate, string Todate, string Call,string Centercode,string Appno)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = " select Initial+' '+PatientFirstName as PatientName,CONVERT(varchar(15),CAST(AppTime AS TIME),100) as AppoinmentTime, * from tbl_Appointment   ";
        if (fromdate != "")
        {
            sql = sql + " where  (CAST(CAST(YEAR(AppDate) AS varchar(4)) + '/' + CAST(MONTH(AppDate) AS varchar(2)) + '/' + CAST(DAY(AppDate) AS varchar(2))  AS datetime) between ('" + Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(Todate).ToString("MM/dd/yyyy") + "') )";
        }
        if (PatRegID != "")
        {
            sql = sql + " and PatientFirstName ='" + PatRegID + "' ";
        }
        if (Appno != "")
        {
            sql = sql + " and AppId ='" + Appno + "' ";
        }
        if (Centercode != "")
        {
            sql = sql + " and CenterCode ='" + Centercode + " '";
        }
        if (Call == "NotDone")
        {
            sql = sql + " and AppointAttend=0 ";
        }       
        else
        {
            sql = sql + " and AppointAttend=1 ";
        }
        sql = sql + "   ORDER BY AppId asc ";
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

  

    public bool Insert_DoctorSlot(int branchid)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        //  Date dateofentrynew = Convert. this.Patregdate;
        SqlCommand cmd = new SqlCommand("SP_InsertSlotInterval", con);
        cmd.CommandType = CommandType.StoredProcedure;


        cmd.Parameters.Add(new SqlParameter("@DoctorName", SqlDbType.NVarChar, 250)).Value = this.CenterCode;
        cmd.Parameters.Add(new SqlParameter("@SlotTime", SqlDbType.NVarChar, 250)).Value = this.SlotTime;
        cmd.Parameters.Add(new SqlParameter("@DoctorId", SqlDbType.Int)).Value = this.CenterID;
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

    public void delete_DrSlot(int SlotId, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from tblDoctor where SlotId='" + SlotId + "' ", conn);

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
                throw;
            }
            //catch ()
            //{

            //}
        }
    }
    public void Get_SlotInterval(int SlotId, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT * from tblDoctor where SlotId=@SlotId  ", conn);

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@SlotId", SqlDbType.Int)).Value = SlotId;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {
                this.SlotId = Convert.ToInt32(sdr["SlotId"]);
                 this.CenterID= Convert.ToInt32(sdr["DoctorId"]);
                 this.CenterCode = sdr["DoctorName"].ToString();
                this.SlotTime = sdr["SlotInterval"].ToString();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {

            conn.Close(); conn.Dispose();


        }
    }

    public bool Update_DoctorSlot(int branchid)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        //  Date dateofentrynew = Convert. this.Patregdate;
        SqlCommand cmd = new SqlCommand("SP_UpdateSlotInterval", con);
        cmd.CommandType = CommandType.StoredProcedure;


        cmd.Parameters.Add(new SqlParameter("@DoctorName", SqlDbType.NVarChar, 250)).Value = this.CenterCode;
        cmd.Parameters.Add(new SqlParameter("@SlotTime", SqlDbType.NVarChar, 250)).Value = this.SlotTime;
        cmd.Parameters.Add(new SqlParameter("@DoctorId", SqlDbType.Int)).Value = this.CenterID;
        cmd.Parameters.Add(new SqlParameter("@SlotId", SqlDbType.Int)).Value = this.SlotId;
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

    public DataTable GetAllDr_IntervalSlot()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select * from  tblDoctor ";


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

    public DataTable GetAllDr_Centert()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT dr_codeid, case when DoctorCode='MAIN LAB' then 'MAIN LAB' else 'MAIN LAB' end as DoctorCode FROM DrMT where DrType='CC' and mainflag=1 ";//DoctorCode='MAIN LAB'


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
    public DataTable GetAllDr_Centert_Online()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT dr_CodeId,case when DoctorCode='OPD' then 'Dr. Satav Pathology Laboratory' else  DoctorCode end as DoctorCode FROM DrMT where DrType='CC' and DoctorCode='OPD' ";


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


    public bool Insert_Doctorschedule(int branchid)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        //  Date dateofentrynew = Convert. this.Patregdate;
        SqlCommand cmd = new SqlCommand("SP_Insert_tbl_DoctorSchedule", con);
        cmd.CommandType = CommandType.StoredProcedure;
               
        cmd.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = (Convert.ToDateTime(SlotTime)).ToString("hh:mm:ss tt", CultureInfo.CurrentCulture);
        cmd.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = (Convert.ToDateTime(SlotEnd)).ToString("hh:mm:ss tt", CultureInfo.CurrentCulture);
        cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date)).Value = this.Patregdate;
        cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date)).Value = this.PatregdateEnd;
        cmd.Parameters.Add(new SqlParameter("@DoctorId", SqlDbType.Int)).Value = this.CenterID;
        cmd.Parameters.Add(new SqlParameter("@DrName", SqlDbType.NVarChar, 250)).Value = this.CenterCode;
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

    public bool Update_Doctorschedule(int branchid)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        //  Date dateofentrynew = Convert. this.Patregdate;
        SqlCommand cmd = new SqlCommand("SP_Update_tbl_DoctorSchedule", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add("@StartTime", SqlDbType.DateTime).Value = (Convert.ToDateTime(SlotTime)).ToString("hh:mm:ss tt", CultureInfo.CurrentCulture);
        cmd.Parameters.Add("@EndTime", SqlDbType.DateTime).Value = (Convert.ToDateTime(SlotEnd)).ToString("hh:mm:ss tt", CultureInfo.CurrentCulture);
        cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date)).Value = this.Patregdate;
        cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date)).Value = this.PatregdateEnd;
        cmd.Parameters.Add(new SqlParameter("@DoctorId", SqlDbType.Int)).Value = this.CenterID;
        cmd.Parameters.Add(new SqlParameter("@DrName", SqlDbType.NVarChar, 250)).Value = this.CenterCode;
        cmd.Parameters.Add(new SqlParameter("@ScheduleId", SqlDbType.Int)).Value = this.SlotId;
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
    public void Get_DrSchedule(int SlotId, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT * from tbl_DoctorSchedule where ScheduleId=@ScheduleId  ", conn);

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@ScheduleId", SqlDbType.Int)).Value = SlotId;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {
                this.SlotId = Convert.ToInt32(sdr["ScheduleId"]);
                this.CenterID = Convert.ToInt32(sdr["DoctorId"]);
                this.SlotTime = sdr["StartTime"].ToString();
                this.SlotEnd = sdr["EndTime"].ToString();
                this.Patregdate = sdr["StartDate"].ToString();
                this.PatregdateEnd = sdr["EndDate"].ToString();
            }

        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {

            conn.Close(); conn.Dispose();


        }
    }

    public void delete_Drschedule(int ScheduleId, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from tbl_DoctorSchedule where ScheduleId='" + ScheduleId + "' ", conn);

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
                throw;
            }
            //catch ()
            //{

            //}
        }
    }


    public DataTable GetAllDr_Schedule()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select CONVERT(varchar(15),CAST(StartTime AS TIME),100) as STime,CONVERT(varchar(15),CAST(EndTime AS TIME),100) as ETime,* from  tbl_DoctorSchedule ";


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

    public DataTable Get_Patient_Appointmentt_Check(string PatRegID, string fromdate, string Todate, string Time, string Centercode)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = " select Initial+' '+PatientFirstName as PatientName,CONVERT(varchar(15),CAST(AppTime AS TIME),100) as AppoinmentTime, * from tbl_Appointment   ";
        if (fromdate != "")
        {
            sql = sql + " where  (CAST(CAST(YEAR(AppDate) AS varchar(4)) + '/' + CAST(MONTH(AppDate) AS varchar(2)) + '/' + CAST(DAY(AppDate) AS varchar(2))  AS datetime) between ('" + Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(Todate).ToString("MM/dd/yyyy") + "') )";
        }

        sql = sql + " and AppTime  =('" + Time + "')  ";
        
       
        sql = sql + "   ORDER BY AppId asc ";
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


    public int Insert_Register_Appoinment(int branchid)
    {
        int Appid = 0;
        SqlConnection con = DataAccess.ConInitForDC();
       
       // SqlCommand cmd = new SqlCommand("SP_phpatrecfrm", con);
        SqlCommand cmd = new SqlCommand("SP_Insert_Appointment", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Initial", SqlDbType.NVarChar, 50)).Value = this.Initial;
        cmd.Parameters.Add(new SqlParameter("@PatientFirstName", SqlDbType.NVarChar, 250)).Value = this.Patname;

        cmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 50)).Value = this.Gender;
        cmd.Parameters.Add(new SqlParameter("@Age", SqlDbType.NVarChar, 250)).Value = this.Age;
        cmd.Parameters.Add(new SqlParameter("@AgeType", SqlDbType.NVarChar, 50)).Value = this.AgeType;
        cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 250)).Value = this.Phone;
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50)).Value = this.Email;
        cmd.Parameters.Add(new SqlParameter("@Pataddress", SqlDbType.NVarChar, 550)).Value = this.Pataddress;
        cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 250)).Value = this.City;
        cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar, 250)).Value = this.State;

        cmd.Parameters.Add(new SqlParameter("@District", SqlDbType.NVarChar, 250)).Value = this.District;
        cmd.Parameters.Add(new SqlParameter("@RefDr", SqlDbType.NVarChar, 250)).Value = this.RefDr;
        cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar, 550)).Value = this.Note;
        cmd.Parameters.Add(new SqlParameter("@HospitalName", SqlDbType.NVarChar, 550)).Value = this.HospitalName;
        cmd.Parameters.Add(new SqlParameter("@HospitalID", SqlDbType.NVarChar, 50)).Value = this.HospitalID;
        cmd.Parameters.Add(new SqlParameter("@ILI", SqlDbType.Bit)).Value = this.ILI;
        cmd.Parameters.Add(new SqlParameter("@Fever", SqlDbType.Bit)).Value = this.Fever;
        cmd.Parameters.Add(new SqlParameter("@Cough", SqlDbType.Bit)).Value = this.Cough;
        cmd.Parameters.Add(new SqlParameter("@SARI", SqlDbType.Bit)).Value = this.SARI;
        cmd.Parameters.Add(new SqlParameter("@FeverDuration", SqlDbType.NVarChar, 250)).Value = this.FeverDuration;
        cmd.Parameters.Add(new SqlParameter("@CoughDuration", SqlDbType.NVarChar, 250)).Value = this.CoughDuration;

        cmd.Parameters.Add(new SqlParameter("@CoMorbidity", SqlDbType.NVarChar, 250)).Value = this.CoMorbidity;
        cmd.Parameters.Add(new SqlParameter("@Tempetrature", SqlDbType.NVarChar, 250)).Value = this.Tempetrature;

        cmd.Parameters.Add(new SqlParameter("@Symptom", SqlDbType.NVarChar, 250)).Value = this.Symptom;
        cmd.Parameters.Add(new SqlParameter("@SymptomAddition", SqlDbType.NVarChar, 250)).Value = this.SymptomAddition;
        cmd.Parameters.Add(new SqlParameter("@TravelLast", SqlDbType.NVarChar, 250)).Value = this.TravelLast;
        cmd.Parameters.Add(new SqlParameter("@TravelLastVisit", SqlDbType.NVarChar, 250)).Value = this.TravelLastVisit;

        cmd.Parameters.Add(new SqlParameter("@PatientAdmited", SqlDbType.NVarChar, 250)).Value = this.PatientAdmited;
        //cmd.Parameters.Add(new SqlParameter("@AppTime", SqlDbType.NVarChar, 250)).Value = this.SlotTime.Trim();
        cmd.Parameters.Add("@AppTime", SqlDbType.DateTime).Value = (Convert.ToDateTime(SlotTime)).ToString("hh:mm:ss tt", CultureInfo.CurrentCulture);
        cmd.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 250)).Value = this.CenterCode;
        cmd.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 250)).Value = this.CreatedBy;

        cmd.Parameters.Add(new SqlParameter("@AppDate", SqlDbType.Date)).Value = this.Patregdate;
        cmd.Parameters.Add(new SqlParameter("@DoctorId", SqlDbType.Int)).Value = this.CenterID;
        cmd.Parameters.Add(new SqlParameter("@DirectApp", SqlDbType.Int)).Value = this.DirectApp;
        try
        {
            con.Open();
            object objectCid;
            cmd.CommandTimeout = 1200;

            objectCid = cmd.ExecuteScalar();

            if (objectCid != null)
            {
                Appid = Convert.ToInt32(objectCid);
            }
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
        return Appid;
    }

    public int Attend_RegisterAppoinment(int branchid)
    {
        int Appid = 0;
        SqlConnection con = DataAccess.ConInitForDC();

        // SqlCommand cmd = new SqlCommand("SP_phpatrecfrm", con);
        SqlCommand cmd = new SqlCommand("SP_Insert_AttendentAppointment", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@AppId", SqlDbType.Int)).Value = this._AppId;
        try
        {
            con.Open();
            object objectCid;
            cmd.CommandTimeout = 1200;

            objectCid = cmd.ExecuteScalar();

            if (objectCid != null)
            {
                Appid = Convert.ToInt32(objectCid);
            }
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
        return Appid;
    }



    public bool Attend_RegisterAppoinment11(int branchid)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        //  Date dateofentrynew = Convert. this.Patregdate;
        SqlCommand cmd = new SqlCommand("SP_Insert_AttendentAppointment", con);
        cmd.CommandType = CommandType.StoredProcedure;


        cmd.Parameters.Add(new SqlParameter("@AppId", SqlDbType.Int)).Value = this._AppId;
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


    public DataTable GetAll_CollectionPErson()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT * FROM Ctuser where UserType='Collection Person' ";


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

    public DataTable GetAll_CollectionPErson_details(int CollPerson)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT * FROM Ctuser where UserType='Collection Person'  and cuid='" + CollPerson + "'";


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