using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;


public class DrMT_Bal_C
{
    public DrMT_Bal_C()
    {
        this.DoctorCode = "";
        this.Dr_codeid = 0;
        this.Name = "";
     
        this.City = "";     
       
        this.Email = "";
        this.Phone = "";
       
        this.Dateof_entry = Date.getdate();
        this.DrType = "DR";
        this.Contact_person = "";
       
        this.Prefix = "";
        this.Flag = false;
       
        this.Username = "";
     
        this.RateTypeName = "";
       
        this.Cashbill = false;
       

    }

    public DrMT_Bal_C(object DoctorName, string DrType ,int branchid)
    {
        this.Name = Convert.ToString(DoctorName);
        this.DrType = DrType;
        
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand(" SELECT *" +
                         " FROM DrMT " +
                         " WHERE DoctorCode = @DoctorCode and DrType=@DrType and branchid=" + branchid + "", conn);
        
        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 255)).Value = this.name;
        sc.Parameters.Add(new SqlParameter("@DrType", SqlDbType.NVarChar, 50)).Value = this.DrType;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                // The IEnumerable contains DataRowView objects.
                this.DoctorCode = sdr["DoctorCode"].ToString();
                this.Name = sdr["DoctorName"].ToString();

                this.Address = sdr["address1"].ToString();
                this.Email = sdr["Doctoremail"].ToString();
                this.City = sdr["city"].ToString();
              
               
                if (sdr["Dateof_entry"] is DBNull)
                    this.Dateof_entry = Date.getMinDate();
                else
                    this.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);

                this.DrType = sdr["DrType"].ToString();
                this.Contact_person = sdr["contactperson"].ToString();
                this.Prefix = sdr["DrInitial"].ToString();
                this.Phone = sdr["DoctorPhoneno"].ToString(); 
                if (sdr["Mainflag"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(sdr["Mainflag"]) == false)
                        this.ChkIsCenter = false;
                    else
                        this.ChkIsCenter = true;
                }
                if (sdr["Cashbill"] != DBNull.Value)
                {
                    if (Convert.ToBoolean(sdr["Cashbill"]) == true)
                        this.Cashbill = true;
                    else
                        this.Cashbill = false;
                }
               
                
                if (sdr["ratetypeid"] is DBNull)
                    this.ratetypeid = 0;
                else
                    this.ratetypeid = Convert.ToInt32(sdr["ratetypeid"]);

                if (sdr["PRO"] is DBNull)
                    this.PRO = 0;
                else
                    this.PRO = Convert.ToInt32(sdr["PRO"]); 
                   
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
    public DrMT_Bal_C(string DoctorCode, string DrType, int i, int branchid)
    {
        this.DoctorCode = DoctorCode;
        this.DrType = DrType;
       
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand(" SELECT *" +
                         " FROM DrMT " +
                         " WHERE DoctorCode = @DoctorCode and DrType=@DrType and branchid=" + branchid + "", conn);

       
        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = DoctorCode;
        sc.Parameters.Add(new SqlParameter("@DrType", SqlDbType.NVarChar, 50)).Value = DrType;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                // The IEnumerable contains DataRowView objects.
                if (sdr["DoctorCode"] != DBNull.Value)
                    this.DoctorCode = sdr["DoctorCode"].ToString();
                else
                    this.DoctorCode = "";
                if (sdr["DoctorName"] != DBNull.Value)
                    this.Name = sdr["DoctorName"].ToString();
                else
                    this.Name = "";             

                this.City = sdr["city"].ToString();
              
                
                if (sdr["Dateof_entry"] == DBNull.Value)
                    this.Dateof_entry = Date.getMinDate();
                else
                    this.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);

                this.DrType = sdr["DrType"].ToString();
                this.Contact_person = sdr["contactperson"].ToString();               
                this.Prefix = sdr["DrInitial"].ToString();
               
                                         
                if (sdr["cashbill"] is DBNull)
                    this.Cashbill= false;
                else
                    this.Cashbill = Convert.ToBoolean(sdr["cashbill"]);

                if (sdr["Mainflag"] is DBNull)
                    this.ChkIsCenter = false;
                else
                    this.ChkIsCenter = Convert.ToBoolean(sdr["Mainflag"]);

                if (sdr["ratetypeid"] is DBNull)
                    this.ratetypeid = 0;
                else
                    this.ratetypeid = Convert.ToInt32(sdr["ratetypeid"]);
                   
            }
            else
            {
                throw new DrMT_Bal_cTableException("No Record Fetched.");
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
            catch (DrMT_Bal_cTableException)
            {
                throw new DrMT_Bal_cTableException("Record not found");
            }
        }
    } 

    public DrMT_Bal_C(object drCodetemp, int branchid)
    {
        this.Dr_codeid = Convert.ToInt32(drCodetemp);
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand(" SELECT *" +
                         " FROM DrMT " +
                         " WHERE Dr_codeid = @Dr_codeid and branchid="+branchid+"", conn);
        sc.Parameters.Add(new SqlParameter("@Dr_codeid", SqlDbType.Int, 4)).Value = this.Dr_codeid;
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                // The IEnumerable contains DataRowView objects.
                this.DoctorCode = sdr["DoctorCode"].ToString();
                this.Name = sdr["DoctorName"].ToString();
                this.Dr_codeid = Convert.ToInt32(sdr["Dr_codeid"]);
               

                this.City = sdr["city"].ToString();
               
                if (sdr["Dateof_entry"] is DBNull)
                    this.Dateof_entry = Date.getMinDate();
                else
                    this.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);
                this.DrType = sdr["DrType"].ToString();
                this.Contact_person = sdr["contactperson"].ToString();
              
                this.Prefix = sdr["DrInitial"].ToString();
                this.Username = Convert.ToString(sdr["Username"]);
                if (sdr["Mainflag"] is DBNull)
                    this.ChkIsCenter = false;
                else
                    this.ChkIsCenter = Convert.ToBoolean(sdr["Mainflag"]);

               

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

    
    public DrMT_Bal_C(string labcd, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand(" SELECT * " +
                         " FROM DrMT " +
                         " WHERE (Unitcode ='" + labcd + "') and (Mainflag = 1) and branchid=" + branchid + "", conn);
        
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                // The IEnumerable contains DataRowView objects.
                this.DoctorCode = sdr["DoctorCode"].ToString();
                this.Name = sdr["DoctorName"].ToString();
                this.Dr_codeid = Convert.ToInt32(sdr["Dr_codeid"]);
             

                this.City = sdr["city"].ToString();
                              
                if (sdr["Dateof_entry"] is DBNull)
                    this.Dateof_entry = Date.getMinDate();
                else
                    this.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);
                this.DrType = sdr["DrType"].ToString();
                this.Contact_person = sdr["contactperson"].ToString();
               
                this.Prefix = sdr["DrInitial"].ToString(); 
                           
             
            }
           
        }
        catch (SqlException)
        {
            // Log an event in the Application Event Log.
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
       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "Update DrMT " +
            "set DoctorName=@DoctorName,address1=@address1,Doctoremail=@Doctoremail,DoctorPhoneno=@DoctorPhoneno," +
            "city=@city, " +
            "DrInitial=@prefix,Dateof_entry=@Dateof_entry, " +
            "DrType=@DrType,contactperson=@contactperson, " +
            " Cashbill=@Cashbill,Mainflag=@Mainflag,ratetypeid=@ratetypeid ,PRO=@PRO where DoctorCode=@DoctorCode and DrType=@DrType and branchid=" + branchid + "", conn);

              

        sc.Parameters.Add(new SqlParameter("@Mainflag", SqlDbType.Bit)).Value = this.ChkIsCenter;
        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = (string)(this.DoctorCode);
        sc.Parameters.Add(new SqlParameter("@DoctorName", SqlDbType.NVarChar, 255)).Value = (string)(this.Name);
        sc.Parameters.Add(new SqlParameter("@address1", SqlDbType.NVarChar, 255)).Value = (string)(this.Address);
        sc.Parameters.Add(new SqlParameter("@city", SqlDbType.NVarChar, 100)).Value = (string)(this.City);
       
       
        sc.Parameters.Add(new SqlParameter("@Doctoremail", SqlDbType.NVarChar, 50)).Value = (string)(this.Email);
        sc.Parameters.Add(new SqlParameter("@DoctorPhoneno", SqlDbType.NVarChar, 255)).Value = (string)(this.Phone);
        sc.Parameters.Add(new SqlParameter("@prefix", SqlDbType.NVarChar, 50)).Value = (string)(this.Prefix);
       
        sc.Parameters.Add(new SqlParameter("@Dateof_entry", SqlDbType.DateTime)).Value = (this.Dateof_entry);
        sc.Parameters.Add(new SqlParameter("@DrType", SqlDbType.Char, 2)).Value = this.DrType;       
        sc.Parameters.Add(new SqlParameter("@contactperson", SqlDbType.NVarChar, 100)).Value = (string)(this.Contact_person);
       
        sc.Parameters.Add(new SqlParameter("@Cashbill", SqlDbType.Bit)).Value = this.Cashbill;
        sc.Parameters.Add(new SqlParameter("@ratetypeid", SqlDbType.Int)).Value = this.ratetypeid;
        sc.Parameters.Add(new SqlParameter("@PRO", SqlDbType.Int)).Value = this.PRO;
       
                
        conn.Close(); 
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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    } 

    
    public bool Update(string DCode, int branchid)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "Update DrMT " +
            "set Mainflag=0 where DoctorCode='" + DCode + "' and (Mainflag =1) and branchid=" + branchid + "", conn);

        
        conn.Close();
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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    } 
   
    public bool Update(object drCodeTemp,int branchid)
    {
        this.Dr_codeid = Convert.ToInt32(drCodeTemp);
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "Update DrMT " +
            "set DoctorCode=@DoctorCode,DoctorName=@DoctorName,Doctoremail=@Doctoremail,DoctorPhoneno=@DoctorPhoneno," +
            "city=@city, " +
            "DrInitial=@prefix,Dateof_entry=@Dateof_entry," +
            "DrType=@DrType,contactperson=@contactperson, " +
            " Cashbill=@Cashbill,Mainflag=@Mainflag where Dr_codeid=@Dr_codeid and DrType=@DrType and branchid=" + branchid + "", conn);

       
        sc.Parameters.Add(new SqlParameter("@Mainflag", SqlDbType.Bit)).Value = this.ChkIsCenter;

        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = (string)(this.DoctorCode);
        sc.Parameters.Add(new SqlParameter("@DoctorName", SqlDbType.NVarChar, 255)).Value = (string)(this.Name);
        sc.Parameters.Add(new SqlParameter("@Dr_codeid", SqlDbType.Int, 4)).Value = (this.Dr_codeid);

        sc.Parameters.Add(new SqlParameter("@city", SqlDbType.NVarChar, 100)).Value = (string)(this.City);
       
        sc.Parameters.Add(new SqlParameter("@Doctoremail", SqlDbType.NVarChar, 50)).Value = (string)(this.Email);
        sc.Parameters.Add(new SqlParameter("@DoctorPhoneno", SqlDbType.NVarChar, 255)).Value = (string)(this.Phone);
        sc.Parameters.Add(new SqlParameter("@prefix", SqlDbType.NVarChar, 50)).Value = (string)(this.Prefix);
    
        sc.Parameters.Add(new SqlParameter("@Dateof_entry", SqlDbType.DateTime)).Value = (this.Dateof_entry);
        sc.Parameters.Add(new SqlParameter("@DrType", SqlDbType.Char, 50)).Value = this.DrType;
       
        sc.Parameters.Add(new SqlParameter("@contactperson", SqlDbType.NVarChar, 100)).Value = (string)(this.Contact_person);           
       
        sc.Parameters.Add(new SqlParameter("@Cashbill", SqlDbType.Bit)).Value = this.Cashbill;

      

        conn.Close(); 

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch (Exception e) { }
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
        // Implement Update logic.
        return true;
    } //update End

    public bool Delete(int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from DrMT " +
            "where DoctorCode=@DoctorCode and branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = (string)(this.DoctorCode);

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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    } 
    public bool Insert(int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand("" +
        "insert into DrMT(DoctorCode,DoctorName,city,Doctoremail,DoctorPhoneno,DrInitial,Dateof_entry,DrType,contactperson,Cashbill,Mainflag,ratetypeid,branchid,Address1,PRO)" +
        "values(@DoctorCode,@DoctorName,@city,@Doctoremail,@DoctorPhoneno,@prefix,@Dateof_entry,@DrType,@contactperson,@Cashbill,@Mainflag,@ratetypeid,@branchid,@Address1,@PRO)", conn);

       
       
        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = (string)(this.DoctorCode);
        sc.Parameters.Add(new SqlParameter("@DoctorName", SqlDbType.NVarChar, 255)).Value = (string)(this.Name);
       
        sc.Parameters.Add(new SqlParameter("@Mainflag", SqlDbType.Bit)).Value = this.ChkIsCenter;
        sc.Parameters.Add(new SqlParameter("@city", SqlDbType.NVarChar, 100)).Value = (string)(this.City);
       
       
        sc.Parameters.Add(new SqlParameter("@Doctoremail", SqlDbType.NVarChar, 50)).Value = (string)(this.Email);
        sc.Parameters.Add(new SqlParameter("@DoctorPhoneno", SqlDbType.NVarChar, 255)).Value = (string)(this.Phone);
        sc.Parameters.Add(new SqlParameter("@prefix", SqlDbType.NVarChar, 50)).Value = (string)(this.Prefix);
         sc.Parameters.Add(new SqlParameter("@contactperson", SqlDbType.NVarChar, 255)).Value = (string)(this.Contact_person);
               
        sc.Parameters.Add(new SqlParameter("@Dateof_entry", SqlDbType.DateTime)).Value = (this.Dateof_entry);
        sc.Parameters.Add(new SqlParameter("@DrType", SqlDbType.NVarChar, 2)).Value = this.DrType;
       
        sc.Parameters.Add(new SqlParameter("@Cashbill", SqlDbType.Bit)).Value = this.Cashbill;

        if (this.ratetypeid != null)
            sc.Parameters.Add(new SqlParameter("@ratetypeid", SqlDbType.Int)).Value = (int)this.ratetypeid;
        else
            sc.Parameters.Add(new SqlParameter("@ratetypeid", SqlDbType.Int)).Value = 0;            
        
           sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
           sc.Parameters.Add(new SqlParameter("@Address1", SqlDbType.NVarChar, 50)).Value = (string)(this.Address);

           if (this.PRO != null)
               sc.Parameters.Add(new SqlParameter("@PRO", SqlDbType.Int)).Value = (int)this.PRO;
           else
               sc.Parameters.Add(new SqlParameter("@PRO", SqlDbType.Int)).Value = 0; 

        SqlDataReader sdr = null;
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
                if (sdr != null) sdr.Close();
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
    } //insert End

    //All properties
    public bool DeleteName(string drname, int branchid)
    {

        
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from DrMT " +
            "where DoctorName=@DoctorName and branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@DoctorName", SqlDbType.NVarChar, 255)).Value = drname;

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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    } //delete End

    public void SendMyMail1(string drcode, string PortNo,int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand(" SELECT * " +
                         " FROM  DrMT" +
                         " WHERE DoctorCode=@DoctorCode  and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = drcode;

        SqlDataReader sdr = null;
        string Subject, BodyMsg,email;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                // The IEnumerable contains DataRowView objects.
                email = sdr["Doctoremail"].ToString();
                Subject = "Your have successfully registered a Doctor.";
                BodyMsg = "Your have successfully registered a Doctor.";
                MailAddress to = new MailAddress(email);
                MailAddress from = new MailAddress("info@era.com");
                MailMessage msgmail = new MailMessage(from, to);
                msgmail.Subject = Subject;
                msgmail.Body = BodyMsg;
                SmtpClient smtp = new SmtpClient(PortNo);
                smtp.Send(msgmail);
            }

        }
        catch(Exception)
        {
            throw new Exception("Data Not Found"); 
        }

        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close();
            }
            catch (SqlException)
            {
                throw;
            }
           
        }
       
    }
  
    internal class DrMT_Bal_cTableException : Exception
    {
        public DrMT_Bal_cTableException(string msg) : base(msg) { }
    }
    public static bool getcashbillflag(string dcode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select VW_cashbillfrom DrMT where DoctorCode='" + dcode + "' and branchid=" + branchid + "", conn);
        object obj = null;
        bool flag;
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
           
            flag = Convert.ToBoolean(obj);
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
        return flag;
    }

    public static string GetEmailCenterNameTable(string PatRegID, string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SELECT     dbo.DrMT.Doctoremail FROM dbo.patmst INNER JOIN dbo.DrMT ON dbo.patmst.centercode = dbo.DrMT.DoctorCode WHERE patmst.PatRegID='" + PatRegID + "' and patmst.FID='" + FID + "' and (dbo.DrMT.DrType = 'CC') and DrMT.branchid=" + branchid + "", conn);

        SqlDataReader sdr = null;
        string Email = "";
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                Email = sdr["Doctoremail"].ToString();
            }
            else
            { Email = ""; }

        }
        catch (SqlException)
        {
            // Log an event in the Application Event Log.
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
        return Email;
    }

    public static string GetEmaildrNameTable(string PatRegID, string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SELECT     dbo.DrMT.Doctoremail FROM dbo.patmst INNER JOIN dbo.DrMT ON dbo.patmst.DoctorCode = dbo.DrMT.DoctorCode WHERE patmst.PatRegID='" + PatRegID + "' and patmst.FID='" + FID + "' and (dbo.DrMT.DrType = 'DR') and DrMT.branchid=" + branchid + "", conn);

        SqlDataReader sdr = null;
        string Email = "";
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                Email = sdr["Doctoremail"].ToString();
            }
            else
            { Email = ""; }

        }
        catch (SqlException)
        {
            // Log an event in the Application Event Log.
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
        return Email;
    }

    public void GetDoctorName(string code,int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from DrMT where DoctorCode='" + code + "' and branchid=" + branchid + "", conn);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
                // The IEnumerable contains DataRowView objects.
                this.DoctorCode = sdr["DoctorCode"].ToString();
                this.Name = sdr["DoctorName"].ToString();
                this.Dr_codeid = Convert.ToInt32(sdr["Dr_codeid"]);
               
                this.City = sdr["city"].ToString();
               
               
                this.Email = sdr["Doctoremail"].ToString();
                this.Phone = sdr["DoctorPhoneno"].ToString();
                 if (sdr["Dateof_entry"] is DBNull)
                    this.Dateof_entry = Date.getMinDate();
                else
                    this.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);
                this.DrType = sdr["DrType"].ToString();
                this.Contact_person = sdr["contactperson"].ToString();

                this.Prefix = sdr["DrInitial"].ToString();
               
                if (sdr["Mainflag"] is DBNull)
                    this.ChkIsCenter = false;
                else
                    this.ChkIsCenter = Convert.ToBoolean(sdr["Mainflag"]);                

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
    #region properties

    private string username;
    public string Username
    {
        get { return username; }
        set { username = value; }
    }
    private string uname;
    public string P_username
    {
        get { return uname; }
        set { uname = value; }
    }
   
    private string doctorCode;
    public string DoctorCode
    {
        get { return doctorCode; }
        set { doctorCode = value; }
    }

    private int _dr_codeTemp;
    public int Dr_codeid
    {
        get { return _dr_codeTemp; }
        set { _dr_codeTemp = value; }
    }

    private string name;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    private string _Address;

    public string Address
    {
        get { return _Address; }
        set { _Address = value; }
    }

  
    private string _city;
    public string City
    {
        get { return _city; }
        set { _city = value; }
    }

    
    private string email;
    public string Email
    {
        get { return email; }
        set { email = value; }
    }

    private string phone;
    public string Phone
    {
        get { return phone; }
        set { phone = value; }
    }

    private DateTime drname_dataofentry;
    public DateTime Dateof_entry
    {
        get { return drname_dataofentry; }
        set { drname_dataofentry = value; }
    }

    private string _DrType;
    public string DrType
    {
        get { return _DrType; }
        set { _DrType = value; }
    }

    private string contact_person;
    public string Contact_person
    {
        get { return contact_person; }
        set { contact_person = value; }
    }

    

    private string prefix;
    public string Prefix
    {
        get { return prefix; }
        set { prefix = value; }
    }

    private bool flag;
    public bool Flag
    {
        get { return flag; }
        set { flag = value; }
    }

 
    private bool _cashbill;
    public bool Cashbill
    {
        get { return _cashbill; }
        set { _cashbill = value; }
    }

    private bool _ChkIsCenter;
    public bool ChkIsCenter
    {
        get { return _ChkIsCenter; }
        set { _ChkIsCenter = value; }
    }

    private int _ratetypeid;
    public int ratetypeid
    {
        get { return _ratetypeid; }
        set { _ratetypeid = value; }
    }

    private int _PRO;
    public int PRO
    {
        get { return _PRO; }
        set { _PRO = value; }
    }

    private string ratetypeName;
    public string RateTypeName
    {
        get { return ratetypeName; }
        set { ratetypeName = value; }
    }
    #endregion

    public bool Insert(string drcode, string drname, int branchid, string username)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("" +
        "insert into DrMT(Username,DoctorCode,DoctorName,address1,address2,city,state,country,Zip,Doctoremail,DoctorPhoneno,prefix,Dateof_entry,DrType,contactperson,Cashbill,Mainflag,ratetypeid,branchid,uname)" +
        "values('','@DoctorCode,@DoctorName,'','','','','','','','','',getdate(),'DR','',0,0,0,@branchid,@uname)", conn);

        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = drcode;
        sc.Parameters.Add(new SqlParameter("@DoctorName", SqlDbType.NVarChar, 255)).Value = drname;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@uname", SqlDbType.NVarChar, 50)).Value = username;
             

        SqlDataReader sdr = null;
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
                if (sdr != null) sdr.Close();
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
    }
}

