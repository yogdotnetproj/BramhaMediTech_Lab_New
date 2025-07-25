using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
//using System.Windows.Forms;
using System.Data.SqlClient;

public class Testspecialnote_Bal_C
{

    public Testspecialnote_Bal_C()
    {
        this.Name = "";
        this.SpecialNote = "";
        this.SNFlag = true;
        this.MTCode = "";

    }

    public Testspecialnote_Bal_C(string formatName, int branchid)
    {      
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * FROM inpmst " +
                         " WHERE MTCode = @MTCode and branchid=" + branchid + "", conn);


        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 255)).Value = formatName;

        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
              
                this.SpecialNote = sdr["SpecialNote"].ToString();
                this.MTCode = sdr["MTCode"].ToString().Trim();

               
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
            catch (Testspecialnote_Bal_CException)
            {
                throw new Testspecialnote_Bal_CException("Record not found");
            }
        }
    }
    public Testspecialnote_Bal_C(string MTCode, int i, int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * FROM inpmst " +
                         " WHERE MTCode = @MTCode and branchid=" + branchid + "", conn);


        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 5)).Value = MTCode;

        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null && sdr.Read())
            {
               
                this.SpecialNote = Convert.ToString( sdr["SpecialNote"]);
                this.MTCode = Convert.ToString( sdr["MTCode"]).Trim();

             

            }
            else
            {
                this.SpecialNote = "NA";
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
                throw;
            }
            catch (Testspecialnote_Bal_CException)
            {
                throw new Testspecialnote_Bal_CException("Record not found");
            }
        }
    }

    public bool updatrReportNote(int branchid, string SpecialNote)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_UpdateReportNote ]";
        
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@SpecialNote", SqlDbType.NText)).Value = SpecialNote;        
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NText, 50)).Value = this.MTCode;

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

   
    public bool Update(int branchid,string SNote)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("" +
            "UPDATE inpmst SET SpecialNote= '" + SNote + "', MTCode=@MTCode WHERE MTCode = @MTCode and branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.

    
        sc.Parameters.Add(new SqlParameter("@SpecialNote", SqlDbType.NVarChar, 5000)).Value = SNote;
       
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NText, 5)).Value = this.MTCode;

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
    public bool Delete(int branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("DELETE FROM inpmst WHERE Name =@Name and branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255)).Value = (string)(this.Name);

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
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
    } 
    public bool Insert(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("" +

        "INSERT INTO inpmst(SpecialNote,MTCode,branchid)" +
                " Values(@SpecialNote,@MTCode,@branchid)", conn);

      
        sc.Parameters.Add(new SqlParameter("@SpecialNote", SqlDbType.NText)).Value = this.SpecialNote;
      
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NText, 50)).Value = this.MTCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
       
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

    public void AlterRpt_InterPretationEntry(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {
            SqlCommand sc = new SqlCommand(" alter view VW_patnpvw as SELECT SpecialNote,  MTCode, branchid FROM dbo.inpmst where  MTCode='" + this.MTCode + "' and branchid=" + branchid + "", conn);
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex) { throw; }
        finally { conn.Close(); conn.Dispose(); }
    }

    internal class Testspecialnote_Bal_CException : Exception
    {
        public Testspecialnote_Bal_CException(string msg) : base(msg) { }
    }

    #region Properties



    private string _MTCode;
    public string MTCode
    {
        get { return _MTCode; }
        set { _MTCode = value; }
    }
    private string _Name;
    public string Name
    {
        get { return _Name; }
        set { _Name = value; }
    }
    private string _SpecialNote;
    public string SpecialNote
    {
        get { return _SpecialNote; }
        set { _SpecialNote = value; }
    }
    private bool _SNFlag;
    public bool SNFlag
    {
        get { return _SNFlag; }
        set { _SNFlag = value; }
    }
    private string username;
    public string P_username
    {
        get { return username; }
        set { username = value; }
    }

    #endregion


}

