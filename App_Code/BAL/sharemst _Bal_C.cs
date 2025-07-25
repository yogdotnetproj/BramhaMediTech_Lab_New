using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;

internal class sharemst_Bal_CException : Exception
{
    public sharemst_Bal_CException(string msg) : base(msg) { }
}
public class sharemst_Bal_C
{
    //properties
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
    private string username;
    public string USERName
    {
        get { return username; }
        set { username = value; }
    }
    private int emergency;
    public int Emergency
    {
        get { return emergency; }
        set { emergency = value; }
    }
    private float percentage;
    public float Percentage
    {
        get { return percentage; }
        set { percentage = value; }
    }
    private int amount;
    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }
    private string testName;
    public string TestName
    {
        get { return testName; }
        set { testName = value; }
    }
    private string sTCODE;
    public string STCODE
    {
        get { return sTCODE; }
        set { sTCODE = value; }
    }
    private string rateCode;
    public string RateCode
    {
        get { return rateCode; }
        set { rateCode = value; }
    }
    private string rateName;
    public string RateName
    {
        get { return rateName; }
        set { rateName = value; }
    }
    
    public override string ToString()
    {
        return this.RateName;
    }
    public sharemst_Bal_C()
        {
            this.Amount = 0;
            this.RateCode = "";
            this.RateName = "";
            this.Emergency = 0;
            this.Percentage = 0;
            this.STCODE = "";
            this.TestName = "";
        }

  
    public sharemst_Bal_C(object testcode_check, object rate_code, int branchid)
        {
            this.STCODE = Convert.ToString(testcode_check);
            this.RateCode = Convert.ToString(rate_code);           
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("Select * from sharemst WHERE STCODE=@STCODE and RateCode=@RateCode  and branchid=" + branchid + "", conn);
        
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);
            sc.Parameters.Add(new SqlParameter("@RateCode", SqlDbType.NVarChar, 3)).Value = (string)(this.RateCode);

            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    // The IEnumerable contains DataRowView objects.
                    if(sdr["Amount"]!=DBNull.Value)
                        this.Amount = Convert.ToInt32(sdr["Amount"]);
                    this.RateCode = Convert.ToString(sdr["RateCode"]);
                    this.RateName = Convert.ToString(sdr["RateName"]);
                    if (sdr["Emergency"] != DBNull.Value)
                        this.Emergency = Convert.ToInt32(sdr["Emergency"]);
                    if (sdr["Percentage"] != DBNull.Value)
                        this.Percentage = Convert.ToSingle(sdr["Percentage"]);
                    this.STCODE = Convert.ToString(sdr["STCODE"]);
                    this.TestName = Convert.ToString(sdr["TestName"]);
                    this.USERName = Convert.ToString(sdr["Username"]);

                }
                else
                {
                    throw new sharemst_Bal_CException("No Record Fetched.");
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
                catch (sharemst_Bal_CException)
                {
                    throw new sharemst_Bal_CException("Record not found");
                }
            }
        }      
    public bool Insert(int branchid)
        {
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
            "Insert Into sharemst(RateCode,Ratename,STCODE,Testname,Amount,Percentage,Emergency,branchid,Username, Createdby )" +
            " VALUES(@RateCode,@Ratename,@STCODE,@Testname,@Amount,@Percentage,@Emergency,@branchid,@Username,@Username)", conn);

            // Add the employee ID parameter and set its value.

            sc.Parameters.Add(new SqlParameter("@RateCode", SqlDbType.NVarChar, 50)).Value = (string)(this.RateCode);
            sc.Parameters.Add(new SqlParameter("@Ratename", SqlDbType.NVarChar, 255)).Value = (string)(this.RateName);
            sc.Parameters.Add(new SqlParameter("@Testname", SqlDbType.NVarChar, 255)).Value = (string)(this.TestName);
            sc.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int, 9)).Value = (int)(this.Amount);
            sc.Parameters.Add(new SqlParameter("@Percentage", SqlDbType.Float, 8)).Value = (float)(this.Percentage);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(this.STCODE);
            sc.Parameters.Add(new SqlParameter("@Emergency", SqlDbType.Int, 9)).Value = (int)(this.Emergency);
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar,50)).Value = (string)(this.USERName);
            try
            {
                conn.Open();
                try
                {
                    sc.ExecuteNonQuery();
                }
                catch
                { throw; }

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
            // Implement Update logic.
            return true;
        } //insert End

                     
        //for update Implimentation
    public bool Update(int branchid)
         {

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand(""+
                "UPDATE sharemst SET RateCode =@RateCode,RateName =@RateName,TestName =@TestName,Amount =@Amount,Percentage =@Percentage,STCODE=@STCODE,Emergency=@Emergency , updatedby=@Username,updatedon=@updatedon   " +
                " where STCODE =@STCODE and RateCode =@RateCode  and branchid=" + branchid + "", conn);

            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@RateCode", SqlDbType.NVarChar, 50)).Value = (string)(this.RateCode);
            sc.Parameters.Add(new SqlParameter("@RateName", SqlDbType.NVarChar, 255)).Value = (string)(this.RateName);
            sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 255)).Value = (string)(this.TestName);
            sc.Parameters.Add(new SqlParameter("@Amount", SqlDbType.Int, 4)).Value = (int)(this.Amount);
            sc.Parameters.Add(new SqlParameter("@Percentage", SqlDbType.Float, 8)).Value = (float)(this.Percentage);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar , 50)).Value = (string)(this.STCODE);
            sc.Parameters.Add(new SqlParameter("@Emergency", SqlDbType.Int, 9)).Value = (int)(this.Emergency);
            sc.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar,50)).Value = (string)(this.USERName);
            sc.Parameters.Add(new SqlParameter("@Updatedon", SqlDbType.DateTime)).Value = this.Patregdate; 

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
                // Implement Update logic.
               
            return true;
        } //update End

    public bool Delete(int branchid)
        {
            
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand("Delete from sharemst where RateCode=@RateCode and STCODE=@STCODE  and branchid=" + branchid + "", conn);    

            sc.Parameters.Add(new SqlParameter("@RateCode", SqlDbType.NVarChar, 50)).Value = (this.RateCode);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = this.STCODE;

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
                    conn.Close(); conn.Dispose();
                }
                catch (SqlException)
                {
                    // Log an event in the Application Event Log.
                    throw;
                }
            }

            
            return true;
        } //delete End
}
