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


public class Refrangemst_Bal_C
{
	public Refrangemst_Bal_C()
	{
		//
		// TODO: Add constructor logic here
        this.GreaterThanDays = 0;
        this.LessThanDays = 0;
        this.Sex = "Male";
        this.Unit = "";
        this.TestName = "";
        this.DescretiveRange = "";
        this.MTCode = "";
        this.STCODE = "";
        this.UpperRange = "";
        this.LowerRange = "";
    }
    public Refrangemst_Bal_C(int normid,int branchid)
        {
           
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand(" SELECT * FROM refrangemst WHERE ID=@ID  and branchid=" + branchid + "", conn);

            sc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int, 4)).Value = normid;
            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    if (sdr["GreaterThanDays"] != DBNull.Value)
                        this.GreaterThanDays = Convert.ToInt32(sdr["GreaterThanDays"]);
                    else
                        this.GreaterThanDays = 0;

                    if (sdr["LessThanDays"] != DBNull.Value)
                        this.LessThanDays = Convert.ToInt32(sdr["LessThanDays"]);
                    else
                        this.LessThanDays = 0;

                    if (sdr["UpperRange"] != DBNull.Value)
                        this.UpperRange = Convert.ToString(sdr["UpperRange"]);
                    else
                        this.UpperRange = "";

                    if (sdr["LowerRange"] != DBNull.Value)
                        this.LowerRange = Convert.ToString(sdr["LowerRange"]);
                    else
                        this.LowerRange = "";

                    if (sdr["MTCode"] != DBNull.Value)
                        this.MTCode = Convert.ToString(sdr["MTCode"]);
                    else
                        this.MTCode = "";

                    if (sdr["STCODE"] != DBNull.Value)
                        this.STCODE = Convert.ToString(sdr["STCODE"]);
                    else
                        this.STCODE = "";

                    if (sdr["Sex"] != DBNull.Value)
                        this.Sex = Convert.ToString(sdr["Sex"]);
                    else
                        this.Sex = "";

                    if (sdr["Unit"] != DBNull.Value)
                        this.Unit = Convert.ToString(sdr["Unit"]);
                    else
                        this.Unit = "";

                    if (sdr["TestName"] != DBNull.Value)
                        this.TestName = Convert.ToString(sdr["TestName"]);
                    else
                        this.TestName = "";

                    if (sdr["DescretiveRange"] != DBNull.Value)
                        this.DescretiveRange = Convert.ToString(sdr["DescretiveRange"]);
                    else
                        this.DescretiveRange = "";
                    if (sdr["OutLabName"] != DBNull.Value)
                        this.P_OutLabName = Convert.ToString(sdr["OutLabName"]);
                    else
                        this.P_OutLabName = "";
                    
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

   
    public bool Insert(int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand("" +
        "insert into refrangemst(GreaterThanDays,LessThanDays,Sex,TestName,Unit,DescretiveRange,MTCode," +
        "STCODE,UpperRange,LowerRange,branchid,username,Createdby,PanicUpperRange,PanicLowerRange,OutLabName)" +
        "values(@GreaterThanDays,@LessThanDays,@Sex,@TestName,@Unit,@DescretiveRange,@MTCode," +
        "@STCODE,@UpperRange,@LowerRange,@branchid,@username,@username,@PanicUpperRange,@PanicLowerRange,@OutLabName)", conn);       
        
        sc.Parameters.Add(new SqlParameter("@GreaterThanDays", SqlDbType.Int)).Value =(int)(this.greaterThanDays);
        sc.Parameters.Add(new SqlParameter("@LessThanDays", SqlDbType.Int)).Value =(int)(this.LessThanDays);
        sc.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar,50)).Value = this.Sex;
        
        if (this.UpperRange != null)
        {
            sc.Parameters.Add(new SqlParameter("@UpperRange", SqlDbType.NVarChar, 250)).Value = this.UpperRange;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@UpperRange", SqlDbType.NVarChar, 250)).Value = null;
        }

        if (this.LowerRange != null)
        {
            sc.Parameters.Add(new SqlParameter("@LowerRange", SqlDbType.NVarChar, 250)).Value = this.LowerRange;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@LowerRange", SqlDbType.NVarChar, 250)).Value = null;
        }

        if (this.TestName != null)
        {
            sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 255)).Value = this.TestName;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 255)).Value =null ;
        }
        if (this.Unit != null)
        {
            sc.Parameters.Add(new SqlParameter("@Unit", SqlDbType.NVarChar, 100)).Value = this.Unit;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@Unit", SqlDbType.NVarChar, 100)).Value =null;
        }
        if (this.DescretiveRange != null)
        {
            sc.Parameters.Add(new SqlParameter("@DescretiveRange", SqlDbType.NVarChar, 1000)).Value = this.DescretiveRange;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@DescretiveRange", SqlDbType.NVarChar, 1000)).Value = null;
        }
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = this.MTCode;
        
        if (this.STCODE != null)
        {
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = this.STCODE;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = null;
            
        }
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_username;


        if (this.PanicUpperRange != null)
        {
            sc.Parameters.Add(new SqlParameter("@PanicUpperRange", SqlDbType.NVarChar, 250)).Value = this.PanicUpperRange;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@PanicUpperRange", SqlDbType.NVarChar, 250)).Value = null;
        }

        if (this.PanicLowerRange != null)
        {
            sc.Parameters.Add(new SqlParameter("@PanicLowerRange", SqlDbType.NVarChar, 250)).Value = this.PanicLowerRange;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@PanicLowerRange", SqlDbType.NVarChar, 250)).Value = null;
        }

        sc.Parameters.Add(new SqlParameter("@OutLabName", SqlDbType.Int)).Value = P_OutLabName;
        conn.Close(); 
        try
        {
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

    public bool Update(int ID, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "Update refrangemst " +
            "set GreaterThanDays=@GreaterThanDays,LessThanDays=@LessThanDays,Sex=@Sex,Unit=@Unit,DescretiveRange=@DescretiveRange,UpperRange=@UpperRange,LowerRange=@LowerRange,Updatedby=@username, Updatedon=@Updatedon, PanicUpperRange=@PanicUpperRange,PanicLowerRange=@PanicLowerRange,OutLabName=@OutLabName where ID=@ID  and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = P_username;
        sc.Parameters.Add(new SqlParameter("@GreaterThanDays", SqlDbType.Int)).Value = (int)(this.greaterThanDays);
        sc.Parameters.Add(new SqlParameter("@LessThanDays", SqlDbType.Int)).Value = (int)(this.LessThanDays);
        sc.Parameters.Add(new SqlParameter("@Sex", SqlDbType.NVarChar, 50)).Value = this.Sex;
       
        if (this.Unit != null)
        {
            sc.Parameters.Add(new SqlParameter("@Unit", SqlDbType.NVarChar, 100)).Value = this.Unit;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@Unit", SqlDbType.NVarChar, 100)).Value = null;
        }
        if (this.DescretiveRange != null)
        {
            sc.Parameters.Add(new SqlParameter("@DescretiveRange", SqlDbType.NVarChar, 1000)).Value = this.DescretiveRange;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@DescretiveRange", SqlDbType.NVarChar, 1000)).Value = null;
        }

        sc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value =ID;

        if (this.UpperRange != null)
        {
            sc.Parameters.Add(new SqlParameter("@UpperRange", SqlDbType.NVarChar,250)).Value = this.UpperRange;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@UpperRange", SqlDbType.NVarChar,250)).Value = null;
        }

        if (this.LowerRange != null)
        {
            sc.Parameters.Add(new SqlParameter("@LowerRange", SqlDbType.NVarChar, 250)).Value = this.LowerRange;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@LowerRange", SqlDbType.NVarChar,250)).Value = null;
        }

        if (this.PanicUpperRange != null)
        {
            sc.Parameters.Add(new SqlParameter("@PanicUpperRange", SqlDbType.NVarChar, 250)).Value = this.PanicUpperRange;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@PanicUpperRange", SqlDbType.NVarChar, 250)).Value = null;
        }

        if (this.PanicLowerRange != null)
        {
            sc.Parameters.Add(new SqlParameter("@PanicLowerRange", SqlDbType.NVarChar, 250)).Value = this.PanicLowerRange;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@PanicLowerRange", SqlDbType.NVarChar, 250)).Value = null;
        }

        sc.Parameters.Add(new SqlParameter("@Updatedon", SqlDbType.DateTime)).Value = this.Patregdate; ;
        sc.Parameters.Add(new SqlParameter("@OutLabName", SqlDbType.Int)).Value = P_OutLabName;
        conn.Close(); 

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw;
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
       
        return true;
    }
    public bool Delete(int normid, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            " Delete from refrangemst " +
            " Where ID=@ID and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@ID",SqlDbType.Int)).Value = normid;
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
                throw;
            }
        }
        return true;
    }


    #region Properties
   
    private int iD;
    public int ID
    {
        get { return iD; }
        set { iD = value; }
    }

    private int greaterThanDays;
    public int GreaterThanDays
    {
        get { return greaterThanDays; }
        set { greaterThanDays=value; }
    }
    private int lessThanDays;
    public int LessThanDays
    {
        get { return lessThanDays; }
        set { lessThanDays = value; }
    }
    private string sex;
    public string Sex
    {
        get { return sex; }
        set { sex = value; }
    }
    private string testName;
    public string TestName
    {
        get { return testName; }
        set { testName = value; }
    }
    private string StestName;
     public string STestName
    {
        get { return StestName; }
        set { StestName = value; }
    }

    
    private string unit;
    public string Unit
    {
        get { return unit; }
        set { unit = value; }
    }
    private string descretiveRange;
    public string DescretiveRange
    {
        get { return descretiveRange; }
        set { descretiveRange = value; }
    }
    private string mTCode;
    public string MTCode
    {
        get { return mTCode; }
        set { mTCode = value; }
    }
    private string sTCODE;
    public string STCODE
    {
        get { return sTCODE; }
        set { sTCODE = value; }
    }

    private string _UpperRange;
    public string UpperRange
    {
        get { return _UpperRange; }
        set { _UpperRange = value; }
    }
    
    private string _LowerRange;
    public string LowerRange
    {
        get { return _LowerRange; }
        set { _LowerRange = value; }
    }


    private string _PanicUpperRange;
    public string PanicUpperRange
    {
        get { return _PanicUpperRange; }
        set { _PanicUpperRange = value; }
    }

    private string _PanicLowerRange;
    public string PanicLowerRange
    {
        get { return _PanicLowerRange; }
        set { _PanicLowerRange = value; }
    }

    private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }

    private string OutLabName;
    public string P_OutLabName
    {
        get { return OutLabName; }
        set { OutLabName = value; }
    }

    private string _TestName;
    public string __TestName
    {
        get { return _TestName; }
        set { _TestName = value; }
    }


    DateTime? _DateofEntry = null;
    public DateTime? Patregdate
    {
        get
        {
            return _DateofEntry;
        }
        set
        {
            _DateofEntry = value;
        }
    }

    #endregion
}
