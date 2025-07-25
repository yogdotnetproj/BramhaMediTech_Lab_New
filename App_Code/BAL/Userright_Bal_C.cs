using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;

public class Userright_Bal_C
{
	public Userright_Bal_C()
	{
        this.Usertype = "";
    }
    public Userright_Bal_C(int id, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SELECT * from  usr  where RollId=@Rollid and branchid=" + branchid + " ", conn);//'"+uname+"'",conn);// +User_Type

        sc.Parameters.Add(new SqlParameter("@Rollid", SqlDbType.Int)).Value = id;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {
                this.Sampleid = Convert.ToInt32(sdr["Rollid"]);
                this.Usertype = sdr["Rolename"].ToString();
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

    public bool Insert(int branchid)
    {        
        SqlConnection conn = DataAccess.ConInitForDC(); 


        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO usr(RoleName,branchid)" +
        "VALUES(@RoleName,@branchid)", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.NVarChar, 200)).Value = this.Usertype;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
     
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
    public bool Update(int rid, string Usertype, int branchid)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "UPDATE usr " +
            "SET RoleName=@RoleName WHERE Rollid=@Rollid and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@Rollid", SqlDbType.Int)).Value = rid;

        if (this.Usertype != null)
            sc.Parameters.Add(new SqlParameter("@RoleName", SqlDbType.NVarChar, 200)).Value = Usertype;
        else
            sc.Parameters.Add(new SqlParameter("@RollName", SqlDbType.NVarChar, 200)).Value = "";

        SqlDataReader sdr = null;

        try
        {
            conn.Close();
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
        

        return true;
    } 

    public void delete(int rrid, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from usr where RollID='" + rrid + "' and branchid=" + branchid + "", conn);

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
           
        }
    }

    public static bool isUsertypepeeExists(string usertype, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM usr  " +
                         " WHERE ROLENAME=@ROLENAME and branchid=" + branchid + " ", conn); //User_Type     

        sc.Parameters.Add(new SqlParameter("@ROLENAME", SqlDbType.NVarChar, 200)).Value = usertype;

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

    public DataTable getuserType()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select * from usr order by ROLENAME", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch
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

    public DataTable getformright()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT     TBL_SubMenuMaster.SubMenuID, TBL_SubMenuMaster.MenuID, TBL_SubMenuMaster.SubMenuName, TBL_SubMenuMaster.SubMenuNavigateURL, "+
                                           " TBL_SubMenuMaster.Isvisable, TBL_MenuMaster.MenuName "+
                                           " FROM         TBL_SubMenuMaster INNER JOIN "+
                                           " TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch
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
    public void deleteUsers(string CUId)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from CTUser where CUId='" + CUId + "' ", conn);

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

        }
    }
    public static bool isUserName_Exists(string username, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM CTUser  " +
                         " WHERE Username=@username and branchid=" + branchid + " ", conn); //User_Type     

        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 200)).Value = username;

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
    public static bool isUserNamePassword_Exists(string username, string Password, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM CTUser  " +
                         " WHERE Username=@username and Password=@Password and branchid=" + branchid + " ", conn); //User_Type     

        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 200)).Value = username;
        sc.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 200)).Value = Password;

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

    public bool Update_Password(string UserName, string Oldpass, string NewPass)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE CTUser " +
            "SET Password=@NewPassword WHERE username=@username and Password=@Password ", conn);




        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 200)).Value = UserName;

        sc.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 200)).Value = Oldpass;
        sc.Parameters.Add(new SqlParameter("@NewPassword", SqlDbType.NVarChar, 200)).Value = NewPass;

        SqlDataReader sdr = null;

        try
        {
            conn.Close();
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


        return true;
    }

    public void deleteUsers_Rights(string Rightid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from Roleright where Rightid='" + Rightid + "' ", conn);

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

        }
    }
    private string usertype;
    public string Usertype
    {
        get { return usertype; }
        set { usertype = value; }
    }
    private int sampleid;
    public int Sampleid
    {
        get { return sampleid; }
        set { sampleid = value; }
    }


    private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }
}