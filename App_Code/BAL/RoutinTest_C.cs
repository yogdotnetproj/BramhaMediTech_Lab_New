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
public class RoutinTest_C
{
	public RoutinTest_C()
	{
		//
		// TODO: Add constructor logic here
		//
        this.routinTestName = "";
	}
    public RoutinTest_C(int id, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SELECT * from RoutinTest where id=@id  ", conn);

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {
                this.ID = Convert.ToInt32(sdr["id"]);
                this.routinTestName = sdr["RoutinTestName"].ToString();
                this.routinTestCode = sdr["RoutinTestCode"].ToString();
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
        "INSERT INTO RoutinTest(RoutinTestName,RoutinTestCode)" +
        "VALUES(@RoutinTestName,@RoutinTestCode)", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@RoutinTestName", SqlDbType.NVarChar, 200)).Value = this.routinTestName;
        sc.Parameters.Add(new SqlParameter("@RoutinTestCode", SqlDbType.NVarChar, 200)).Value = this.routinTestCode;
       
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
    public bool Update(int Bid, string RoutinTestName, int branchid)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "UPDATE RoutinTest " +
            "SET RoutinTestName=@RoutinTestName,RoutinTestCode=@RoutinTestCode WHERE ID=@ID ", conn);

        sc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = Bid;

        if (this.RoutinTestName != null)
            sc.Parameters.Add(new SqlParameter("@RoutinTestName", SqlDbType.NVarChar, 200)).Value = RoutinTestName;
        else
            sc.Parameters.Add(new SqlParameter("@RoutinTestName", SqlDbType.NVarChar, 200)).Value = "";
        if (this.RoutinTestName != null)
            sc.Parameters.Add(new SqlParameter("@RoutinTestCode", SqlDbType.NVarChar, 200)).Value = routinTestCode;
        else
            sc.Parameters.Add(new SqlParameter("@RoutinTestCode", SqlDbType.NVarChar, 200)).Value = "";

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

        SqlCommand sc = new SqlCommand("delete from RoutinTest where id='" + rrid + "' ", conn);

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

   
    public static ICollection getRoutinTest(int branchid)
    {
        
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        SqlDataReader sdr = null;

        sc.Connection = conn;
        sc.CommandText = "select * from RoutinTest ";

        try
        {
            conn.Open();

            sdr = sc.ExecuteReader();

            if (sdr != null)
            {
                while (sdr.Read())
                {
                    RoutinTest_C RT = new RoutinTest_C();
                    RT.routinTestName = sdr["RoutinTestName"].ToString();
                    RT.routinTestCode = sdr["RoutinTestCode"].ToString();
                    RT.ID = Convert.ToInt32(sdr["ID"]);
                    tl.Add(RT);
                   
                }
            }
            // This is not a while loop. It only loops once.
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

        return tl;
    }

    public bool Update_Machine(int Bid, string RoutinTestName, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE InstumentMaster " +
            "SET Instumentcode=@Instumentcode,Instumentname=@Instumentname WHERE ID=@ID ", conn);

        sc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = Bid;

        if (this.P_Instumentcode != null)
            sc.Parameters.Add(new SqlParameter("@Instumentcode", SqlDbType.NVarChar, 200)).Value = P_Instumentcode;
        else
            sc.Parameters.Add(new SqlParameter("@Instumentcode", SqlDbType.NVarChar, 200)).Value = "";
        if (this.P_Instumentname != null)
            sc.Parameters.Add(new SqlParameter("@Instumentname", SqlDbType.NVarChar, 200)).Value = P_Instumentname;
        else
            sc.Parameters.Add(new SqlParameter("@Instumentname", SqlDbType.NVarChar, 200)).Value = "";

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
    public bool Insert_Machin(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();


        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO InstumentMaster(Instumentname,Instumentcode)" +
        "VALUES(@Instumentname,@Instumentcode)", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@Instumentname", SqlDbType.NVarChar, 200)).Value = P_Instumentname;
        sc.Parameters.Add(new SqlParameter("@Instumentcode", SqlDbType.NVarChar, 200)).Value = P_Instumentcode;

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

    public void GetMAchinData(int id, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT * from InstumentMaster where id=@id  ", conn);

        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@id", SqlDbType.Int)).Value = id;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null && sdr.Read())
            {
                this.ID = Convert.ToInt32(sdr["id"]);
                P_Instumentcode = sdr["Instumentcode"].ToString();
                P_Instumentname = sdr["Instumentname"].ToString();
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
    public void delete_Machine(int rrid, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from InstumentMaster where id='" + rrid + "' ", conn);

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
    public static ICollection getMAchinName(int branchid)
    {

        ArrayList Al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        SqlDataReader sdr = null;

        sc.Connection = conn;
        sc.CommandText = "select * from InstumentMaster ";

        try
        {
            conn.Open();

            sdr = sc.ExecuteReader();

            if (sdr != null)
            {
                while (sdr.Read())
                {
                    RoutinTest_C RT = new RoutinTest_C();
                    RT.P_Instumentcode = sdr["Instumentcode"].ToString();
                    RT.P_Instumentname = sdr["Instumentname"].ToString();
                    RT.ID = Convert.ToInt32(sdr["ID"]);
                    Al.Add(RT);

                }
            }
            // This is not a while loop. It only loops once.
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

        return Al;
    }

    public bool DeleteFutureFormat(int Bid, int branchid, string DeletedBy)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "UPDATE dfrmst " +
            "SET IsActive=0,DeletedBy=@DeletedBy,DeletedOn=@DeletedOn WHERE ID=@ID ", conn);

        sc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = Bid;

        if (DeletedBy != "")
            sc.Parameters.Add(new SqlParameter("@DeletedBy", SqlDbType.NVarChar, 200)).Value = DeletedBy;
        else
            sc.Parameters.Add(new SqlParameter("@DeletedBy", SqlDbType.NVarChar, 200)).Value = "";
        
            sc.Parameters.Add(new SqlParameter("@DeletedOn", SqlDbType.DateTime)).Value = System.DateTime.Now;
        

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
   
    #region Properties
    private string RoutinTestName;
    public string routinTestName
    {
        get { return RoutinTestName; }
        set { RoutinTestName = value; }
    }
    private string RoutinTestCode;
    public string routinTestCode
    {
        get { return RoutinTestCode; }
        set { RoutinTestCode = value; }
    }
    private int id;
    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    

private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }

    private string Instumentcode;
    public string P_Instumentcode
    {
        get { return Instumentcode; }
        set { Instumentcode = value; }
    }
    private string Instumentname;
    public string P_Instumentname
    {
        get { return Instumentname; }
        set { Instumentname = value; }
    }
    #endregion
}