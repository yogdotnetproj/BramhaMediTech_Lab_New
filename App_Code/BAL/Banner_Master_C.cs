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
public class Banner_Master_C
{
	public Banner_Master_C()
	{
		this.BannerName = "";
	}


    public static DataTable Get_TotalPatientCount(object tdate, object fdate)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select COUNT(srno) as PatientCount from patmst " +
                  " where    (CAST(CAST(YEAR( patmst.Patregdate) AS varchar(4)) + '/' + CAST(MONTH( patmst.Patregdate) AS varchar(2)) + '/' + CAST(DAY( patmst.Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "')  ";
        


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

    public Banner_Master_C(int id, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT * from BannerTable where id=@id  ", conn);//'"+uname+"'",conn);// +

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
                this.BannerName = sdr["BannerName"].ToString();
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
        "INSERT INTO BannerTable(BannerName)" +
        "VALUES(@BannerName)", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@BannerName", SqlDbType.NVarChar, 200)).Value = this.BannerName;
       
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
    public bool Update(int Bid, string BannerName, int branchid)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "UPDATE BannerTable " +
            "SET BannerName=@BannerName WHERE ID=@ID ", conn);

        sc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = Bid;

        if (this.BannerName != null)
            sc.Parameters.Add(new SqlParameter("@BannerName", SqlDbType.NVarChar, 200)).Value = BannerName;
        else
            sc.Parameters.Add(new SqlParameter("@BannerName", SqlDbType.NVarChar, 200)).Value = "";

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

        SqlCommand sc = new SqlCommand("delete from BannerTable where id='" + rrid + "' ", conn);

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

   
    public static ICollection getbanner(int branchid)
    {
        
        ArrayList tl = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand();
        SqlDataReader sdr = null;

        sc.Connection = conn;
        sc.CommandText = "select * from BannerTable ";

        try
        {
            conn.Open();

            sdr = sc.ExecuteReader();

            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Banner_Master_C sm = new Banner_Master_C();
                    sm.BannerName = sdr["BannerName"].ToString();
                    sm.ID = Convert.ToInt32(sdr["ID"]);
                    tl.Add(sm);
                   
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


  
    #region Properties
    private string bannername;
    public string BannerName
    {
        get { return bannername; }
        set { bannername = value; }
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
    #endregion
}