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


public class createuserlogic_Bal_C
{
    #region properties
    private string email;
    public string P_email
    {
        get { return email; }
        set { email = value; }
    }

    private string Password;
    public string P_Password
    {
        get { return Password; }
        set { Password = value; }
    }

    private string displayname;
    public string P_displayname
    {
        get { return displayname; }
        set { displayname = value; }
    }
    private string WhatAppUrl;
    public string P_WhatAppUrl
    {
        get { return WhatAppUrl; }
        set { WhatAppUrl = value; }
    }
    private string WhatApp_Api;
    public string P_WhatApp_Api
    {
        get { return WhatApp_Api; }
        set { WhatApp_Api = value; }
    }
    private bool QRCodeRequired;
    public bool P_QRCodeRequired
    {
        get { return QRCodeRequired; }
        set { QRCodeRequired = value; }
    }
    

    private string LabWebsite;
    public string P_LabWebsite
    {
        get { return LabWebsite; }
        set { LabWebsite = value; }
    }

    private string LabSmsString;
    public string P_LabSmsString
    {
        get { return LabSmsString; }
        set { LabSmsString = value; }
    }

    private string LabSmsName;
    public string P_LabSmsName
    {
        get { return LabSmsName; }
        set { LabSmsName = value; }
    }

    public int Port;
    public int P_Port
    {
        get { return Port; }
        set { Port = value; }
    }
    #endregion

    public createuserlogic_Bal_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static ICollection get_All_Users(int branchid)
    {
        ArrayList al = new ArrayList();

        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();

        SqlCommand sc = new SqlCommand();
        //sc.CommandText = "select DISTINCT username from users where (Type <> 'Admin' and Type<>'administrator') and branchid=" + branchid + " ORDER BY username ";
        sc.CommandText = "select DISTINCT username from CTuser where  branchid=" + branchid + " ORDER BY username ";

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
                    createuserTable_Bal_C aut = new createuserTable_Bal_C();
                    aut.Username = sdr["username"].ToString();
                    al.Add(aut);
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
  
    public static ICollection getData(string uname, int branchid)
    {
        ArrayList al = new ArrayList();       
        SqlConnection conn = DataAccess.ConInitForDC(); 

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "select * from CTuser where username=@username and branchid=" + branchid + "";

        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar , 50)).Value = uname ;
       
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
                    createuserTable_Bal_C aut = new createuserTable_Bal_C();
                    aut.Usertype = sdr["Usertype"].ToString();
                    aut.Name = sdr["Name"].ToString();
                    aut.Username = sdr["Username"].ToString();
                    aut.Password = sdr["Password"].ToString();


                    if (sdr["Drid"] is DBNull)
                        aut.Drid = 0;
                    else
                        aut.Drid = Convert.ToInt32(sdr["Signid"]);
                    aut.UnitCode = sdr["UnitCode"].ToString();

                    al.Add(aut);
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

    public static ICollection getAllUsers_IRD(int branchid)
    {
        ArrayList al = new ArrayList();

        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();

        SqlCommand sc = new SqlCommand();
       
        sc.CommandText = "select DISTINCT username from CTuser where  DRid=0 and(UserType <> 'Admin' and UserType<>'administrator') and branchid=" + branchid + " ORDER BY username ";

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
                    createuserTable_Bal_C aut = new createuserTable_Bal_C();
                    aut.Username = sdr["username"].ToString();
                    al.Add(aut);
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
    public static ICollection getAllUsers(int branchid)
    {
        ArrayList al = new ArrayList();

        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();

        SqlCommand sc = new SqlCommand();

        sc.CommandText = "select DISTINCT username from CTuser where  branchid=" + branchid + " ORDER BY username ";//(UserType <> 'Admin' and UserType<>'administrator') and

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
                    createuserTable_Bal_C aut = new createuserTable_Bal_C();
                    aut.Username = sdr["username"].ToString();
                    al.Add(aut);
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
    public static ICollection getAllUsers_username(int branchid, string username)
    {
        ArrayList al = new ArrayList();

        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();

        SqlCommand sc = new SqlCommand();
       
        sc.CommandText = "select DISTINCT username from CTuser where username='" + username + "' and branchid=" + branchid + " ORDER BY username ";

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
                    createuserTable_Bal_C aut = new createuserTable_Bal_C();
                    aut.Username = sdr["username"].ToString();
                    al.Add(aut);
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

   
   
    public static ICollection getAllData(string orderField, object labcode, int branchid, int maindeptid)
    {
        ArrayList al = new ArrayList();
        
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "select * from CTuser where UserType <> 'CollectionCenter' and branchid=" + branchid + " and DigModule=" + maindeptid + "";//MainDept=" + MainDept + "
        if (labcode != null)
        {
            sc.CommandText = sc.CommandText + " and LBcode='" + labcode.ToString().Trim() + "'";
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
                    createuserTable_Bal_C aut = new createuserTable_Bal_C();
                    aut.Usertype = sdr["Usertype"].ToString();
                    aut.Name = sdr["Name"].ToString();
                    aut.Username = sdr["Username"].ToString();
                    aut.Password = sdr["Password"].ToString();



                    if (sdr["Drid"] != DBNull.Value)
                        aut.Drid = 0;
                    else
                        aut.Drid = Convert.ToInt32(sdr["Drid"]);
                    aut.UnitCode = sdr["UnitCode"].ToString();
                    
                    al.Add(aut);
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

    public static ICollection GetAllDatafor_Center(string usertype, string orderField, object labcode, int branchid, int maindeptid)
    {
        ArrayList al = new ArrayList();
       
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "select * from CTuser  where usertype ='" + usertype + "' and branchid=" + branchid + " and DigModule='" + maindeptid + "'";
       if (labcode != null)
        {
            sc.CommandText = sc.CommandText + " and UnitCode='" + labcode.ToString().Trim() + "'";
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
                    createuserTable_Bal_C aut = new createuserTable_Bal_C();
                    aut.Usertype = sdr["Usertype"].ToString();
                    aut.Name = sdr["Name"].ToString();
                    aut.Username = sdr["Username"].ToString();
                    aut.Password = sdr["Password"].ToString();
                  

                    if (sdr["Drid"] is DBNull)
                        aut.Drid = 0;
                    else
                        aut.Drid = Convert.ToInt32(sdr["Drid"]);
                    aut.UnitCode = sdr["UnitCode"].ToString();
                   
                   
                    al.Add(aut);
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

    public void getemail(string uname, int branchid)
    {
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
        SqlCommand sc = new SqlCommand("SP_phemail",conn);        
        sc.CommandType = CommandType.StoredProcedure;       
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
                    createuserTable_Bal_C aut = new createuserTable_Bal_C();
                    P_displayname = sdr["LabEmailDisplayName"].ToString();
                    P_Password = sdr["LabEmailPassword"].ToString();
                    P_email = sdr["LabEmailID"].ToString();//"admin@gmail.com";
                    P_LabSmsName = sdr["LabSmsName"].ToString();
                    P_LabSmsString = sdr["LabSmsString"].ToString();
                    P_LabWebsite = sdr["LabWebsite"].ToString();
                    if (sdr["Port"] != DBNull.Value)
                    {
                        P_Port = Convert.ToInt32(sdr["Port"]);
                    }
                    else
                    {
                        P_Port = 25;
                    }
                    P_WhatAppUrl = sdr["WhatAppUrl"].ToString();
                    P_WhatApp_Api = sdr["WhatApp_Api"].ToString();
                    P_QRCodeRequired = Convert.ToBoolean( sdr["QRCodeRequired"]);
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

    }

    public int GetExpiryDate(string uname, int branchid)
    {
        int expdays = 5000;
        SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);

        SqlCommand sc = new SqlCommand();
        sc.CommandText = "SELECT datediff(day, getdate(),ExpiryDate) as Expdays FROM  stmst";
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
                    createuserTable_Bal_C aut = new createuserTable_Bal_C();

                    if (sdr["Expdays"] != DBNull.Value)
                    {
                        expdays = Convert.ToInt32(sdr["Expdays"].ToString());

                    }

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
        return expdays;

    }

    public static string Get_Taxper()
    {
        string Taxper = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = null;

        sc = new SqlCommand("select Taxper from Tax_Master", conn);
        try
        {
            conn.Open();

            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                Taxper = "";
            else
                Taxper = Convert.ToString(o);
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
        //tl.Sort();
        return Taxper;
    }


}


