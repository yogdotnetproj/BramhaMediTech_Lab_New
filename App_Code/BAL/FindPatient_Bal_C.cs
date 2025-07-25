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



public class FindPatient_Bal_C
{
    public FindPatient_Bal_C()
    {
      
    }
    private string username;
    public string P_Username
    {
        get { return username; }
        set { username = value; }
    }

    private int PP_ID;
    public int P_PP_id
    {
        get { return PP_ID; }
        set { PP_ID = value; }

    }

    private string regno;
    public string P_regno
    {
        get { return regno; }
        set { regno = value; }
    }

    private string intial;
    public string P_intial
    {
        get { return intial; }
        set { intial = value; }
    }

    private string Patname;
    public string P_firstname
    {

        get { return Patname; }
        set { Patname = value; }

    }

    private string lastname;
    public string P_lastname
    {

        get { return lastname; }
        set { lastname = value; }
    }

    private string gender;
    public string P_gender
    {
        get { return gender; }
        set { gender = value; }
    }

    private int age;
    public int P_age
    {

        get { return age; }
        set { age = value; }


    }

    private string mobileno;
    public string P_mobileno
    {

        get { return mobileno; }
        set { mobileno = value; }
    }

    private string refcustomer;
    public string P_refcustomer
    {
        get { return refcustomer; }
        set { refcustomer = value; }


    }

    private string address;
    public string P_address
    {
        get { return address; }
        set { address = value; }

    }

    private string remark;
    public string P_remark
    {
        get { return remark; }
        set { remark = value; }
    }

    private string mdy;
    public string P_mdy
    {
        get { return mdy; }
        set { mdy = value; }


    }

    private string refdr;
    public string P_refdr
    {

        get { return refdr; }
        set { refdr = value; }


    }

    private string dateofentrymain;
    public string P_DateOfEntry
    {

        get { return dateofentrymain; }
        set { dateofentrymain = value; }


    }

    private string reportdate;
    public string P_ReportDate
    {

        get { return reportdate; }
        set { reportdate = value; }


    }
//rate varible
    private string source;
    public string P_source
    {

        get { return source; }
        set { source = value; }


    }
    private string TestName;
    public string P_TestName
    {
        get { return TestName; }
        set { TestName = value; }
    }

    private string target;
    public string P_target
    {
        get { return target; }
        set { target = value; }
    }

    private int Opertor;
    public int  P_Opertor
    {

        get { return Opertor; }
        set { Opertor = value; }
    }
    private string FromTime;
    public string P_FromTime
    {

        get { return FromTime; }
        set { FromTime = value; }
    }
    private string ToTime;
    public string P_ToTime
     {

         get { return ToTime; }
         set { ToTime = value; }
     }
    

    private int addvalue;
    public int P_addvalue
    {

        get { return addvalue; }
        set { addvalue = value; }


    }

   
    private string Sampletype;
    public string P_Sampletype
    {

        get { return Sampletype; }
        set { Sampletype = value; }

    }

    private DateTime DateTimeNow;
    public DateTime P_DateTime
    {
        get { return DateTimeNow;}
        set { DateTimeNow = value; }
    
    }

    private int RateTypeflag;
    public int P_RateTypeflag
    {
        get { return RateTypeflag; }
        set { RateTypeflag = value; }
    }

    

    private int _PID;
    public int PID
    {
        get { return _PID; }
        set { _PID = value; }
    }
    public DataSet FillGridActualTatCalc()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = "";
        SqlDataAdapter da = new SqlDataAdapter(" select DISTINCT TotalCount, subdeptName, testname, TatDuration, TatName, MTCode, SDCode, BelowTAT, AboveTAT,(100*BelowTAT/TotalCount)as AchivetatPer,(100*AboveTAT/TotalCount)as NotAchivetatPer from VW_TAT_Calculation ", con);//where RegNo='" + P_regno + "'
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
            con.Dispose();
        }
        return ds;

    }


    public void InsertPateint()
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Insert into PatMT(Perment_PateintID,intial,Patname,sex,Age,Mob_no,MDY,RefDr,Pateint_addr,Remark,RefCustomer) values(@Perment_PateintID,@intial,@Patname,@sex,@Age,@Mob_no,@MDY,@RefDr,@Pateint_addr,@Remark,@RefCustomer)", conn);

      
        sc.Parameters.Add(new SqlParameter("@Perment_PateintID", SqlDbType.NVarChar, 50)).Value = P_PP_id;
        sc.Parameters.Add(new SqlParameter("@intial", SqlDbType.NVarChar, 35)).Value = P_intial;
        sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 35)).Value = P_firstname;
        sc.Parameters.Add(new SqlParameter("@sex", SqlDbType.NVarChar, 6)).Value = P_age;
        sc.Parameters.Add(new SqlParameter("@Age", SqlDbType.Int)).Value = P_age;
        sc.Parameters.Add(new SqlParameter("@Mob_no", SqlDbType.NVarChar, 15)).Value = P_mobileno;
        sc.Parameters.Add(new SqlParameter("@MDY", SqlDbType.NVarChar, 5)).Value = P_mdy;
        sc.Parameters.Add(new SqlParameter("@RefDr", SqlDbType.NVarChar, 50)).Value = P_refdr;
        sc.Parameters.Add(new SqlParameter("@Pateint_addr", SqlDbType.NVarChar, 250)).Value = P_address;
        sc.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 50)).Value = P_remark;
        sc.Parameters.Add(new SqlParameter("@RefCustomer", SqlDbType.NVarChar, 50)).Value = P_refcustomer;

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close(); conn.Dispose();
        }

    }

  
    public void selectPateint()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "SP_Search_Pateintbyname";
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                P_intial = sdr["intial"].ToString();
                P_firstname = sdr["Patname"].ToString();
                P_gender = sdr["sex"].ToString();
                P_age = Convert.ToInt32(sdr["Age"]);
                P_mobileno = sdr["Mob_no"].ToString();
                P_refcustomer = sdr["RefCustomer"].ToString();
                P_remark = sdr["Remark"].ToString();
                P_address = sdr["Pateint_addr"].ToString();
                P_mdy = sdr["MDY"].ToString();
                P_refdr = sdr["RefDr"].ToString();

            }

        }
        catch (Exception e)
        {


        }

    }

   
   
    public DataSet Bindturnaroundtime()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = "";
        SqlDataAdapter da = new SqlDataAdapter("select TestName,Patregdate,PatRepDate from VW_tatstvw", con);
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

    public DataSet FillGrid()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = "";
        SqlDataAdapter da = new SqlDataAdapter("select distinct * ,substring(CONVERT(VARCHAR, Patregdate, 108),0,6) AS ActTime from VW_TAT_Calculation_patientwise where Patname = '" + P_firstname + "' order by Patregdate desc ", con);
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
    public DataSet FillGridRegNo_New()
    {
        SqlConnection con = DataAccess.ConInitForDC();

        string labcode = Convert.ToString(HttpContext.Current.Session["UnitCode"] );
        string sql = "";

        string SqlQuery = "select distinct * from VW_TAT_Calculation_patientwise where testname <>'' ";
        if (P_firstname != "" && P_firstname != null)
        {
            SqlQuery = SqlQuery + " and Patname = '" + P_firstname + "' ";
        }
        if (P_TestName != "" && P_TestName != null)
        {
            SqlQuery = SqlQuery + " and testname='" + P_TestName + "' ";
        }
        if (P_FromTime != "Select Time" && P_ToTime != "Select Time")
        {
            SqlQuery = SqlQuery + " and substring(CONVERT(VARCHAR, Patregdate, 108),0,3)>= " + P_FromTime + "  and  substring(CONVERT(VARCHAR, Patregdate, 108),0,3)<= " + P_ToTime + " ";
        }
        SqlQuery = SqlQuery + " order by Patregdate desc ";
        SqlDataAdapter da = new SqlDataAdapter(SqlQuery, con);
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


    public DataSet FillGridRegNo()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = "";
        SqlDataAdapter da = new SqlDataAdapter("select distinct * from VW_TAT_Calculation_patientwise order by Patregdate desc ", con);//where RegNo='" + P_regno + "'
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
    public DataSet FillGridRegNo_testname()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = "";
        SqlDataAdapter da = new SqlDataAdapter("select distinct * from VW_TAT_Calculation_patientwise where testname='" + P_TestName + "' order by Patregdate desc ", con);//where RegNo='" + P_regno + "'
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

    public void dtime()
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("Select Patregdate,PatRepDate from VW_tatstvw  where Patname='" + P_firstname + "'", conn);
        sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 50)).Value = P_firstname;
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {
                P_DateOfEntry = (sdr["Patregdate"]).ToString();
                P_ReportDate = sdr["PatRepDate"].ToString();

            }

        }catch(Exception exe)
        {
            throw;
        }
    }

    public void selectRateSetting(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "SP_phrtcalfrm";

        sc.Parameters.Add(new SqlParameter("@SourceId", SqlDbType.NVarChar, 50)).Value = P_source;
        sc.Parameters.Add(new SqlParameter("@TargetId", SqlDbType.NVarChar, 50)).Value = P_target;
        sc.Parameters.Add(new SqlParameter("@Opertor12", SqlDbType.Int)).Value = P_Opertor;
        sc.Parameters.Add(new SqlParameter("@extravalue", SqlDbType.Int)).Value = P_addvalue;
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_Username;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 18)).Value = branchid;

        sc.Parameters.Add(new SqlParameter("@RateTypeflag", SqlDbType.Int)).Value = P_RateTypeflag;
       

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close(); conn.Dispose();
        }

    }

    public void Deletetargetid(int RateTypeflag)
    {

        if (RateTypeflag == 1 || RateTypeflag == 4)
        {
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("Delete from TestCharges where DrCode='" + P_target + "'", conn);

            sc.Parameters.Add(new SqlParameter("@TargetId", SqlDbType.NVarChar, 50)).Value = P_target;
            try
            {
                conn.Open();
                sc.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn.Close(); conn.Dispose();
            }
        }
        else
        {

            SqlConnection conn1 = DataAccess.ConInitForDC();
            SqlCommand sc1 = new SqlCommand("Delete from sharemst where RateCode='" + P_target + "'", conn1);

            sc1.Parameters.Add(new SqlParameter("@TargetId", SqlDbType.NVarChar, 50)).Value = P_target;
            try
            {
                conn1.Open();
                sc1.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                conn1.Close(); conn1.Dispose();
            }
        }


    }

  

    public DataSet FillGridSampleType()
    { 
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = "";
        SqlDataAdapter da = new SqlDataAdapter("select * from patmst where PatRegID='" + P_regno + "'", con);
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

   
    public Boolean cheaktargetidexist(string targetid, int branchid)
    {
        string q = "select * from TestCharges where DrCode='" + targetid + "' and branchid=" + branchid + "";
      
        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand();
        sc.Connection = con;

        sc.CommandText = q;
        
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            sdr = sc.ExecuteReader();
            if (sdr.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            sdr.Close();
            con.Close(); con.Dispose();
        }
    }

    public Boolean cheaktargetidexist1(string targetid, int branchid)
    {

        string q = "select * from sharemst where RateCode='" + targetid + "' and branchid=" + branchid + "";
        
        SqlConnection con = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand();
        sc.Connection = con;

        sc.CommandText = q;
      
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            sdr = sc.ExecuteReader();
            if (sdr.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            sdr.Close();
            con.Close(); con.Dispose();
        }
    }
    public DataSet FillGridMNo(string Mno)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        string sql = "";
        SqlDataAdapter da = new SqlDataAdapter("select distinct * from VW_TAT_Calculation_patientwise where PatientPhoneNo='" + Mno + "'", con);
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
}

