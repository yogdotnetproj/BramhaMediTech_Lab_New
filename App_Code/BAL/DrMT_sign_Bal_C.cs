using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public class DrMT_sign_Bal_C
{
    private char _flag_type;

    public char flag_type
    {
        get { return _flag_type; }
        set { _flag_type = value; }
    }
    public static string getcheckmonthlybill_Center(string UnitCode, int branchid, string Centercode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("SELECT     DrMT.cashbill FROM         DrMT INNER JOIN " +
           " CTuser ON DrMT.DoctorCode = CTuser.Centercode  where DrType='CC' and CTuser.branchid=" + branchid + " and DrMT.DoctorCode='" + Centercode + "' ", conn);
        object obj = null;
        string cashbill = "";
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                cashbill = obj.ToString();
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
        return cashbill;
    }
    public static string getcheckmonthlybill(string UnitCode, int branchid, string USERNAME)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        //SqlCommand cmd = new SqlCommand("SELECT     DrMT.cashbill FROM         DrMT INNER JOIN " +
        //   " CTuser ON DrMT.DoctorCode = CTuser.Centercode  where DrType='CC' and CTuser.branchid=" + branchid + " and CTuser.USERNAME='" + USERNAME + "' ", conn);
        SqlCommand cmd = new SqlCommand("SELECT     DrMT.cashbill FROM         DrMT  " +
          "   where DrType='CC' and branchid=" + branchid + " and DoctorCode=N'" + USERNAME + "' ", conn);
        object obj = null;
        string cashbill = "";
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                cashbill = obj.ToString();
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
        return cashbill;
    }


    public static DataSet doctorInformation(string collectioncenter, string drcode, int branchid)
    {
        string sql = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        sql = "SELECT * from  DrMT where DrType='DR'";
       
       if (collectioncenter != "All" && collectioncenter !="")
        {
            sql = sql + "and DoctorName='" + collectioncenter + "'";
        }
        if (drcode != "")
        {
            sql = sql + "and DoctorCode='"+drcode+"'";        
        }

        sql = sql + " order by DoctorName ";
        
        
        da = new SqlDataAdapter(sql, conn);
        try
        {
            //.conn.Open();
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
  

    public static ICollection getOption(int branchid)
    {
        ArrayList al = new ArrayList();

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" select distinct TestName from stformmst  where branchid=" + branchid + "", conn);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
                        
            if (sdr != null && sdr.Read())
            {
                Patientinitial_Bal_C cnt = new Patientinitial_Bal_C();
                cnt.prefixName = sdr["TestName"].ToString();
                al.Add(cnt);
                
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

       public static string getdrnameBydrcode(string drCode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" select Rtrim(DrInitial)+' '+DoctorName as DoctorName from DrMT where DoctorCode='" + drCode + "' and branchid=" + branchid + "", conn);

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
        {
            return "";
        }
        else
            return drName.ToString();
    }

    public static bool isDrCodeExists(string drCode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" select count(*) from DrMT where DoctorCode='" + drCode + "'and branchid=" + branchid + "", conn);

        int number = 0;

        try
        {
            conn.Open();
            number = Convert.ToInt16(sc.ExecuteScalar());
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
        if (number == 0)
        {
            return false;
        }
        else
            return true;
    }

    public static ICollection<ratetype_Bal_C> getSelectratemaster(int branchid, char RateFlag)
    {
        List<ratetype_Bal_C> al = new List<ratetype_Bal_C>();             

        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "SP_phmainrtmst";
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.Parameters.AddWithValue("@branchid", branchid);
        sc.Parameters.AddWithValue("@RateFlag", RateFlag);
               

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
                    ratetype_Bal_C cnt = new ratetype_Bal_C();
                    cnt.RatID = sdr["RatID"].ToString();
                    cnt.RateName = sdr["RateName"].ToString();
                    
                    al.Add(cnt);                   

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
                if (sdr != null)
                    sdr.Close();
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
    public static ICollection<ratetype_Bal_C> getSelectrateCompliment(int branchid, char flg)
    {
        List<ratetype_Bal_C> al = new List<ratetype_Bal_C>();

        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "SP_phshrerec";
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.Parameters.AddWithValue("@branchid", branchid);
        sc.Parameters.AddWithValue("@RateFlag", flg);       

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
                    ratetype_Bal_C cnt = new ratetype_Bal_C();
                    cnt.RatID = sdr["RatID"].ToString();
                    cnt.RateName = sdr["RateName"].ToString();
                 
                    al.Add(cnt);
                    
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
                if (sdr != null)
                    sdr.Close();
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

    public static bool isDrNameExists(string drName, string drCode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" select count(*) from DrMT where DoctorName='" + drName + "' and DoctorCode <> '" + drCode + "' and branchid=" + branchid + "", conn);

        int number = 0;

        try
        {
            conn.Open();
            number = Convert.ToInt16(sc.ExecuteScalar());
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
        if (number == 0)
        {
            return false;
        }
        else
            return true;
    }
    public static ICollection<DrMT_Bal_C> GetAll_Centers(string orderField, string drcode, string drname, string userName, object labCode, int branchid)
    {
        List<DrMT_Bal_C> al = new List<DrMT_Bal_C>();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        if (labCode != null)
        {
            sc.CommandText = "SELECT     dbo.DrMT.* ,RatT.RateName  FROM         DrMT INNER JOIN " + 
                   " RatT ON DrMT.ratetypeid = RatT.RatID AND DrMT.Branchid = RatT.Branchid LEFT OUTER JOIN "+
                   " CTuser ON DrMT.DoctorCode = CTuser.UnitCode AND DrMT.Branchid = CTuser.branchid  where DrMT.DrType='CC' and DrMT.UnitCode='" + labCode.ToString().Trim() + "' and (DrMT.DoctorCode is not null and DrMT.DoctorCode <> '') and DrMT.branchid=" + branchid + "";
        }
        else
        {
            sc.CommandText = "SELECT   distinct  dbo.DrMT.* , RatT.RateName " + 
               " FROM         DrMT INNER JOIN "+
                "  RatT ON DrMT.ratetypeid = RatT.RatID AND DrMT.Branchid = RatT.Branchid LEFT OUTER JOIN "+
                "  CTuser ON DrMT.DoctorCode = CTuser.UnitCode AND DrMT.Branchid = CTuser.branchid  "+
                  "  where DrMT.DrType='CC' and (DrMT.DoctorCode is not null and DrMT.DoctorCode <> '') and DrMT.branchid=" + branchid + "";
       }
        if (drcode != "")
        {
            sc.CommandText = sc.CommandText + " " + "and DrMT.DoctorCode like '" + drcode + "%' ";
        }
        if (drname != "")
        {
            sc.CommandText = sc.CommandText + " " + "and DrMT.DoctorName like '" + drname + "%' ";
        }
        if (orderField != "")
        {
            sc.CommandText = sc.CommandText + " " + " order by DrMT." + orderField.ToString();
        }
        else
        {
            sc.CommandText = sc.CommandText + " " + " order by DrMT.DoctorCode";
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
                    DrMT_Bal_C cnt = new DrMT_Bal_C();

                    cnt.DoctorCode = sdr["DoctorCode"].ToString();
                    cnt.Name = sdr["DoctorName"].ToString();
                    cnt.Address = sdr["address1"].ToString();
                 
                    cnt.Email = sdr["Doctoremail"].ToString();
                    cnt.City = sdr["city"].ToString();
                    cnt.Phone = sdr["DoctorPhoneno"].ToString();
                   
                    if (sdr["Dateof_entry"] is DBNull)
                        cnt.Dateof_entry = Date.getMinDate();
                    else
                        cnt.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);

                    cnt.DrType = sdr["DrType"].ToString();
                    cnt.Contact_person = sdr["contactperson"].ToString();

                    cnt.Prefix = sdr["DrInitial"].ToString();
                  

                    if (sdr["RateName"] is DBNull)
                        cnt.RateTypeName = "";
                    else
                        cnt.RateTypeName = Convert.ToString(sdr["RateName"]);

                   

                    al.Add(cnt);
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
   
    
    public static ICollection getAllDoctor(string orderField, object labcode, int branchid)
    {
        ArrayList al = new ArrayList();
        
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "select * from DrMT where DrType='DR' and (DoctorCode is not null and DoctorCode <> '') and branchid=" + branchid + "  ";
       
        if (orderField != "")
        {
            sc.CommandText = sc.CommandText + " and DoctorCode='"+orderField.ToString()+"'";
        }
       
        sc.CommandText = sc.CommandText + " order by DoctorName";
       
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
                    DrMT_Bal_C cnt = new DrMT_Bal_C();
                    
                    cnt.DoctorCode = sdr["DoctorCode"].ToString();
                    cnt.Prefix = sdr["DrInitial"].ToString();
                    cnt.Name = sdr["DoctorName"].ToString();
                    cnt.Address = sdr["address1"].ToString();

                    cnt.Email = sdr["Doctoremail"].ToString();
                    cnt.City = sdr["city"].ToString();
                    cnt.Phone = sdr["DoctorPhoneno"].ToString();
                  
                    al.Add(cnt);
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
    
    public static DataSet GetcenterDoctor1(string ccode, string orderField, int branchid)
    {
        string sql = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        sql = "select * from DrMT where DrType='DR' and CC_code='" + ccode + "' and (DoctorCode is not null and DoctorCode <> '') and branchid=" + branchid + "";
        if (orderField != "")
        {
            sql = sql + " " + "order by " + orderField.ToString();
        }

        da = new SqlDataAdapter(sql, conn);
        try
        {
           
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
    public static ICollection GetcenterDoctor(string ccode, string orderField, int branchid)
    {
        ArrayList al = new ArrayList();       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        sc.CommandText = "select * from DrMT where DrType='DR'  and (DoctorCode is not null and DoctorCode <> '') and branchid=" + branchid + "";
         if (orderField != "")
        {
            sc.CommandText = sc.CommandText + " " + "order by " + orderField.ToString();
        }
         if (ccode != "")
         {
             sc.CommandText = sc.CommandText + " and DoctorCode ='" + ccode + "'";
               //  sql = sql + " and state='" + statecode + "'";
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
                    DrMT_Bal_C cnt = new DrMT_Bal_C();
                    
                    cnt.DoctorCode = sdr["DoctorCode"].ToString();

                    cnt.Name = sdr["DrInitial"].ToString() + " " + sdr["DoctorName"].ToString();
                   
                    cnt.Email = sdr["Doctoremail"].ToString();
                    cnt.City = sdr["city"].ToString();
                    cnt.Phone = sdr["DoctorPhoneno"].ToString();
                   
                    al.Add(cnt);
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

    
    public static ICollection<DrMT_Bal_C> GetAll_CentersStateWise(string statecode, object labCode, int branchid)
    {
        List<DrMT_Bal_C> al = new List<DrMT_Bal_C>();

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        if (labCode != null)
        {
            sc.CommandText = "select * from DrMT where DrType='CC'  and (DoctorCode is not null and DoctorCode <> '')  and branchid=" + branchid + "";
        }
        else
        {
            sc.CommandText = "select * from DrMT where DrType='CC'and (DoctorCode is not null and DoctorCode <> '')  and branchid=" + branchid + "";
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
                    
                            DrMT_Bal_C cnt = new DrMT_Bal_C();                          
                            cnt.DoctorCode = sdr["DoctorCode"].ToString();
                            cnt.Name = sdr["DoctorName"].ToString();
                           
                            cnt.Email = sdr["Doctoremail"].ToString();
                            cnt.City = sdr["city"].ToString();
                            cnt.Phone = sdr["DoctorPhoneno"].ToString();
                            
                            if (sdr["Dateof_entry"] is DBNull)
                                cnt.Dateof_entry = Date.getMinDate();
                            else
                                cnt.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);

                            cnt.DrType = sdr["DrType"].ToString();
                            cnt.Contact_person = sdr["contactperson"].ToString();
                            cnt.Prefix = sdr["DrInitial"].ToString();                            
                           
                            if (sdr["Username"] is DBNull)
                                cnt.Username = "";
                            else
                                cnt.Username = Convert.ToString(sdr["Username"]);
                           
                          
                            al.Add(cnt);                       
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

    
    public static ICollection<DrMT_Bal_C> GetAll_CentersNotStateWise(object labCode, int branchid)
    {
        List<DrMT_Bal_C> al = new List<DrMT_Bal_C>();

        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand();
        if (labCode != null)
        {
            sc.CommandText = "select * from DrMT where DrType='CC'and UnitCode='" + labCode.ToString().Trim() + "' and (DoctorCode is not null and DoctorCode <> '')and branchid=" + branchid + "";
        }
        else
        {
            sc.CommandText = "select * from DrMT where DrType='CC'and (DoctorCode is not null and DoctorCode <> '')";
        }
        sc.Connection = conn;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            if (sdr != null)
            {
                while (sdr.Read())
                {
                   
                    DrMT_Bal_C cnt = new DrMT_Bal_C();
                  
                    cnt.DoctorCode = sdr["DoctorCode"].ToString();
                    cnt.Name = sdr["DoctorName"].ToString();
                  
                    cnt.Email = sdr["Doctoremail"].ToString();
                    cnt.City = sdr["city"].ToString();
                    cnt.Phone = sdr["DoctorPhoneno"].ToString();
                    
                    if (sdr["Dateof_entry"] is DBNull)
                        cnt.Dateof_entry = Date.getMinDate();
                    else
                        cnt.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);

                    cnt.DrType = sdr["DrType"].ToString();
                    cnt.Contact_person = sdr["contactperson"].ToString();

                    cnt.Prefix = sdr["DrInitial"].ToString();                   
                  
                    if (sdr["Username"] is DBNull)
                        cnt.Username = "";
                    else
                        cnt.Username = Convert.ToString(sdr["Username"]);
               
                    al.Add(cnt);
                   
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
    public static ICollection<DrMT_Bal_C> getloginPSC(string drcode, int brnchid)
    {
        List<DrMT_Bal_C> al = new List<DrMT_Bal_C>();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        sc.CommandText = "select * from DrMT where DrType='CC'and DoctorCode = '" + drcode + "' and branchid=" + brnchid + "";        
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
                    DrMT_Bal_C cnt = new DrMT_Bal_C();                    
                    cnt.DoctorCode = sdr["DoctorCode"].ToString();
                    cnt.Name = sdr["DoctorName"].ToString();
                   
                    cnt.Email = sdr["Doctoremail"].ToString();
                    cnt.City = sdr["city"].ToString();
                    cnt.Phone = sdr["DoctorPhoneno"].ToString();
                   
                    if (sdr["Dateof_entry"] is DBNull)
                        cnt.Dateof_entry = Date.getMinDate();
                    else
                        cnt.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);

                    cnt.DrType = sdr["DrType"].ToString();
                    cnt.Contact_person = sdr["contactperson"].ToString();
                    cnt.Prefix = sdr["DrInitial"].ToString();
                                     
                    if (sdr["Username"] is DBNull)
                        cnt.Username = "";
                    else
                        cnt.Username = Convert.ToString(sdr["Username"]);
                                                         
                    al.Add(cnt);

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

    public static ICollection<DrMT_Bal_C> GetAll_CentersStateWiseWithSorting(string orderField, string statecode, object labCode, int branchid)
    {
        List<DrMT_Bal_C> al = new List<DrMT_Bal_C>();
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand();
        string sql = "";
        sql = sql + "select * from DrMT where DrType='CC' and branchid=" + branchid + "";
        if (labCode != null)
        {
            sql = sql + " and unitcode='" + labCode.ToString().Trim() + "' and (DoctorCode is not null and DoctorCode <> '')";
        }
        else
        {
            sql = sql + " and (DoctorCode is not null and DoctorCode <> '')";
        }
        if (statecode != "")
        {
            sql = sql + " and state='" + statecode + "'";
        }
        if (orderField != "")
        {
            sql = sql + " order by " + orderField + "";
        }
        else
        {
            sql = sql + " order by  DoctorCode";
        }
        sc.CommandText = sql;
        sc.Connection = conn;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    
                            DrMT_Bal_C cnt = new DrMT_Bal_C();
                         
                            cnt.DoctorCode = sdr["DoctorCode"].ToString();
                            cnt.Name = sdr["DoctorName"].ToString();
                        
                            cnt.Email = sdr["Doctoremail"].ToString();
                            cnt.City = sdr["city"].ToString();
                            cnt.Phone = sdr["DoctorPhoneno"].ToString();
                           
                            if (sdr["Dateof_entry"] is DBNull)
                                cnt.Dateof_entry = Date.getMinDate();
                            else
                                cnt.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);

                            cnt.DrType = sdr["DrType"].ToString();
                            cnt.Contact_person = sdr["contactperson"].ToString();
                            cnt.Prefix = sdr["DrInitial"].ToString();
                           
                            
                            if (sdr["Username"] is DBNull)
                                cnt.Username = "";
                            else
                                cnt.Username = Convert.ToString(sdr["Username"]);
                           

                            al.Add(cnt);
                      
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

    public static DataSet GetAll_CentersStateWiseWithSorting1(string orderField, string statecode, object labCode, int branchid, string name)
    {
        string sql = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        DataSet ds = new DataSet();

        sql = sql + "select * from DrMT where DrType='CC' and branchid=" + branchid + "";
        
        if (name != "All")
        {
            sql = sql + "and DoctorName='" + name + "'";
        }
        if (labCode != null)
        {
            sql = sql + " and (DoctorCode is not null and DoctorCode <> '')";
        }
        else
        {
            sql = sql + " and (DoctorCode is not null and DoctorCode <> '')";
        }
       
        if (orderField != "")
        {
            sql = sql + " order by " + orderField + "";
        }
        else
        {
            sql = sql + " order by  DoctorCode";
        }
        da = new SqlDataAdapter(sql, conn);
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
   
    
    public static ICollection<DrMT_Bal_C> getAllLab(string orderField, int branchid)
    {
        List<DrMT_Bal_C> al = new List<DrMT_Bal_C>();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        sc.CommandText = "select * from DrMT where DrType='TE' and (DoctorCode is not null and DoctorCode <> '') and branchid=" + branchid + "";

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
                    DrMT_Bal_C cnt = new DrMT_Bal_C();
                    
                    cnt.DoctorCode = sdr["DoctorCode"].ToString();
                    cnt.Name = sdr["DoctorName"].ToString();
                   
                    cnt.Email = sdr["Doctoremail"].ToString();
                    cnt.Phone = sdr["DoctorPhoneno"].ToString();
                    cnt.City = sdr["city"].ToString();
                   
                    if (sdr["Dateof_entry"] is DBNull)
                        cnt.Dateof_entry = Date.getMinDate();
                    else
                        cnt.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);

                    cnt.DrType = sdr["DrType"].ToString();
                    cnt.Contact_person = sdr["contactperson"].ToString();
                    cnt.Prefix = sdr["DrInitial"].ToString();
                    
                   
                    if (sdr["Username"] is DBNull)
                        cnt.Username = "";
                    else
                        cnt.Username = Convert.ToString(sdr["Username"]);
                   
                    al.Add(cnt);
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
            
        }
        return al;
    }

    
    public static ICollection<DrMT_Bal_C> getLab(int branchid)
    {
        List<DrMT_Bal_C> al = new List<DrMT_Bal_C>();

        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "select * from DrMT where DrType='TE' and (DoctorCode is not null and DoctorCode <> '') and branchid=" + branchid + "  order by DoctorCode";

        sc.Connection = conn;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    DrMT_Bal_C cnt = new DrMT_Bal_C();
                    cnt.DoctorCode = sdr["DoctorCode"].ToString();
                    cnt.Name = sdr["DoctorName"].ToString();
                    al.Add(cnt);
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
    
    public static ICollection<DrMT_Bal_C> getAllCustomers(string orderField, object labcode,int branchid)
    {
        List<DrMT_Bal_C> al = new List<DrMT_Bal_C>();       
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "select * from DrMT where DrType='CM' and (DoctorCode is not null and DoctorCode <> '') and branchid=" + branchid + " order by DoctorName";
        if (labcode != null)
        {
            sc.CommandText = sc.CommandText + " " + "and CC_code IN(select DoctorCode from DrMT where UnitCode='" + labcode.ToString().Trim() + "') ";
        }
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
                    DrMT_Bal_C cnt = new DrMT_Bal_C();
                   
                    cnt.DoctorCode = sdr["DoctorCode"].ToString();
                    cnt.Name = sdr["Prefix"].ToString()+" "+sdr["DoctorName"].ToString();
                   
                    cnt.Email = sdr["Doctoremail"].ToString();
                    cnt.Phone = sdr["DoctorPhoneno"].ToString();
                    cnt.City = sdr["city"].ToString();
                   
                    if (sdr["Dateof_entry"] is DBNull)
                        cnt.Dateof_entry = Date.getMinDate();
                    else
                        cnt.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);

                    cnt.DrType = sdr["DrType"].ToString();
                    cnt.Contact_person = sdr["contactperson"].ToString();

                    cnt.Prefix = sdr["DrInitial"].ToString();                 
                   
                    if (sdr["Username"] is DBNull)
                        cnt.Username = "";
                    else
                        cnt.Username = Convert.ToString(sdr["Username"]);                   
                    
                    al.Add(cnt);
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
    public static ICollection<DrMT_Bal_C> getPSCCustomers(string cccode, string orderField, int branchid)
    {
        List<DrMT_Bal_C> al = new List<DrMT_Bal_C>();     
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "select * from DrMT where DrType='CM' and (DoctorCode is not null and DoctorCode <> '')  and branchid=" + branchid + " order by DoctorName";

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
                    DrMT_Bal_C cnt = new DrMT_Bal_C();
                    
                    cnt.DoctorCode = sdr["DoctorCode"].ToString();
                    cnt.Prefix = sdr["DrInitial"].ToString();
                    cnt.Name = sdr["DoctorName"].ToString();
               
                    cnt.Email = sdr["Doctoremail"].ToString();
                    cnt.Phone = sdr["DoctorPhoneno"].ToString();
                    cnt.City = sdr["city"].ToString();
                   
                    if (sdr["Dateof_entry"] is DBNull)
                        cnt.Dateof_entry = Date.getMinDate();
                    else
                        cnt.Dateof_entry = Convert.ToDateTime(sdr["Dateof_entry"]);

                    cnt.DrType = sdr["DrType"].ToString();
                    cnt.Contact_person = sdr["contactperson"].ToString();

                    cnt.Prefix = sdr["DrInitial"].ToString();                   
                   
                    if (sdr["Username"] is DBNull)
                        cnt.Username = "";
                    else
                        cnt.Username = Convert.ToString(sdr["Username"]);
                   

                    al.Add(cnt);
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
    
    public static ICollection Get_CenterDetails(string UnitCode, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;
        if (UnitCode != null)
        {
            sc = new SqlCommand("SELECT * FROM DrMT where DrType='CC'  and branchid=" + branchid + " order by DoctorName", conn);//and UnitCode='" + UnitCode.ToString().Trim() + "'
        }
        else
        {
            sc = new SqlCommand("SELECT * FROM DrMT_Bal_C where DrType='CC' and branchid=" + branchid + "", conn);
        }

        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    DrMT_Bal_C dnt = new DrMT_Bal_C();                   
                    dnt.Name = dr["DoctorCode"].ToString() + "=" + dr["DoctorName"].ToString();
                     dnt.DoctorCode = dr["DoctorCode"].ToString();
                    al.Add(dnt);

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



    public static ICollection GetPro( int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;

        sc = new SqlCommand("SELECT * FROM ctuser where Usertype='PRO' and branchid=" + branchid + "", conn);
        

        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    DrMT_Bal_C dnt = new DrMT_Bal_C();
                    dnt.Name = dr["CUId"].ToString() + "=" + dr["Name"].ToString();
                    dnt.DoctorCode = dr["CUId"].ToString();
                    al.Add(dnt);

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
    public static string Get_CenterDefault(string UnitCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select DoctorCode from DrMT where DrType='CC' and branchid=" + branchid + " and Mainflag='1' order by DoctorName", conn);
        object obj = null;
        string drcode = "";
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                drcode = obj.ToString();
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
        return drcode;
    }
    public static string getdefault_Collcenter(string UnitCode, int branchid, string USERNAME)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("SELECT     DrMT.DoctorCode, CTuser.USERNAME , DrMT.cashbill FROM         DrMT INNER JOIN " +
           " CTuser ON DrMT.DoctorCode = CTuser.CenterCode  where DrType='CC' and CTuser.branchid=" + branchid + " and CTuser.USERNAME='" + USERNAME + "' ", conn);
        object obj = null;
        string drcode = "";
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                drcode = obj.ToString();
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
        return drcode;
    }

    public static string Get_C_Code(string Name, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select DoctorCode from DrMT where DoctorCode=N'" + Name + "' and branchid=" + branchid + " and DrType='CC'", conn);
        object obj = null;
        string drcode = "";
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                drcode = obj.ToString();
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
        return drcode;
    }

  
    public static string GetCenterwithName(string UnitCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd;
       
        if (UnitCode == "")
        {
            cmd = new SqlCommand("select top(1) DoctorCode from DrMT where DrType='CC' and branchid=" + branchid + " and mainflag=1 ", conn);
        }
        else
        {
            cmd = new SqlCommand("select DoctorCode from DrMT where DrType='CC' and UnitCode='" + UnitCode + "' and branchid=" + branchid + "  order by DoctorName", conn);
        }
        object obj = null;
        string drcode = "";
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                drcode = obj.ToString();
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
        return drcode;
    }
    public static bool checkcashflag(string DoctorCode, string UnitCode, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select Cashbill  from DrMT where DoctorCode=N'" + DoctorCode + "'  and branchid=" + branchid + " and DrType='CC'", conn);
        object obj = null;
        bool cashflag = false;
        try
        {
            conn.Open();
            obj = cmd.ExecuteScalar();
            if (obj != null)
                cashflag = Convert.ToBoolean(obj);
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }
        return cashflag;
    }
    public static ICollection GetMainCen(object labCode, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        sc = new SqlCommand("SELECT * FROM DrMT where VW_cshbill=1 and DrType='CC' and branchid=" + branchid + "", conn);
        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    DrMT_Bal_C dnt = new DrMT_Bal_C();
                    
                    dnt.Name = dr["DoctorCode"].ToString() + "==" + dr["DoctorName"].ToString();
                    dnt.DoctorCode = dr["DoctorCode"].ToString();
                    al.Add(dnt);

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
            
        }

        return al;

    }

    
    public static ICollection Get_CenterDetails(object labCode, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;
        if (labCode != null)
        {
            sc = new SqlCommand("SELECT * FROM DrMT where DrType='CC'  and branchid=" + branchid + " order by DoctorName", conn);
        }
        else
        {
            sc = new SqlCommand("SELECT * FROM DrMT where DrType='CC' and branchid=" + branchid + " order by DoctorName", conn);
        }
        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    DrMT_Bal_C dnt = new DrMT_Bal_C();
                    
                    dnt.Name = dr["DoctorName"].ToString();
                    dnt.DoctorCode = dr["DoctorCode"].ToString();
                    al.Add(dnt);

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
                if (dr != null) dr.Close();

                conn.Close(); conn.Dispose();

            }
            catch (SqlException)
            {
                throw;
            }
            
        }

        return al;

    }

   
    public static string GetSingleCenter(string ccode, int branchid)
    {
        string CenterCode = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        sc = new SqlCommand("SELECT * FROM DrMT where DrType='CC' and DoctorCode='" + ccode + "' and branchid=" + branchid + "", conn);
        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    CenterCode = dr["DoctorCode"].ToString() + "==" + dr["DoctorName"].ToString();

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
                if (dr != null) dr.Close();

                conn.Close(); conn.Dispose();

            }
            catch (SqlException)
            {
                throw;
            }

        }

        return CenterCode;

    }
    public static string GetSingleDoctor(string DrCode, int branchid)
    {
        string CenterCode = "";
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;
        sc = new SqlCommand("SELECT * FROM DrMT where DoctorCode='" + DrCode + "' and branchid=" + branchid + "", conn);
        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {                    
                    CenterCode = dr["DoctorName"].ToString();
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
                if (dr != null) dr.Close();

                conn.Close(); conn.Dispose();

            }
            catch (SqlException)
            {
                throw;
            }
            
        }

        return CenterCode;

    }

    public static float GetDepositByDrcode(string drcode, int branchid)
    {
        float totamt = 0f;
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        sc = new SqlCommand("select DepositAmt from DrMT where DoctorCode=@DoctorCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@DoctorCode", SqlDbType.NVarChar, 50)).Value = drcode;


        try
        {
            conn.Open();
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                totamt = 0f;
            else
                totamt = Convert.ToSingle(o);

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
        
        return totamt;
    }

 
    public static bool Exists_Default_Center(string labcode, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select count(*) from DrMT where  Mainflag = 'True' and branchid=" + branchid + "", conn);

        int number = 0;
        try
        {
            conn.Open();
            number = Convert.ToInt16(sc.ExecuteScalar());
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
        if (number == 0)
        {
            return false;
        }
        else
            return true;
    }

   
    
    private static string CollCenterName; public static string P_CollCenterName { get { return CollCenterName; } set { CollCenterName = value; } }

    public static DataTable Get_CenterDetails(string UnitCode, int branchid,int i)
    {        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
          DataTable dr =new DataTable ();
          conn.Open();
          string Query = "";
          SqlDataAdapter da ;
        if (UnitCode != null)
        {
            Query = "SELECT * FROM DrMT where DrType='CC' and UnitCode='" + UnitCode.ToString().Trim() + "' and branchid=" + branchid + " order by DoctorName";
             da = new SqlDataAdapter(Query, conn);
        }
        else
        {
            Query = "SELECT * FROM DrMT where DrType='CC' and branchid=" + branchid + "";
           
             da = new SqlDataAdapter(Query, conn);
        }
        try
        {
            
            da.Fill(dr);
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }

        return dr;

    }


    public static ICollection<ratetype_Bal_C> get_mainDoctor(int branchid, char RateFlag)
    {
        List<ratetype_Bal_C> al = new List<ratetype_Bal_C>();

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "SP_getmaindoctor";
        sc.Connection = conn;
        sc.CommandType = CommandType.StoredProcedure;
        sc.Parameters.AddWithValue("@branchid", branchid);
        


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
                    ratetype_Bal_C cnt = new ratetype_Bal_C();
                    cnt.RatID = sdr["DRID"].ToString();
                    cnt.RateName = sdr["Name"].ToString();

                    al.Add(cnt);

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
                if (sdr != null)
                    sdr.Close();
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
    public static string GetReceiptno(string FID, int branchid)
    {
        string ReceiptNo = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;
        sc = new SqlCommand("SELECT isnull( max( ReceiptNo),0)+1 as ReceiptNo FROM RecM where  FID='" + FID + "' and branchid=" + branchid + "", conn);
        SqlDataReader dr = null;

        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                while (dr.Read())
                {
                    ReceiptNo = dr["ReceiptNo"].ToString();

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
                if (dr != null) dr.Close();

                conn.Close(); conn.Dispose();

            }
            catch (SqlException)
            {
                throw;
            }

        }

        return ReceiptNo;

    }

    public int Insert_Update_ReceiptNo(int branchid,int FID)
    {
        int RecNo = 0;
        SqlConnection con = DataAccess.ConInitForDC();

        SqlCommand cmd = new SqlCommand("SP_GetReceiptNo_Month", con);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
        cmd.Parameters.Add(new SqlParameter("@branchid", SqlDbType.NVarChar, 50)).Value = branchid;
        
        try
        {
            con.Open();
            object objectCid;
            cmd.CommandTimeout = 1200;

            objectCid = cmd.ExecuteScalar();

            if (objectCid != null)
            {
                RecNo = Convert.ToInt32(objectCid);
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
        return RecNo;
    }

}

