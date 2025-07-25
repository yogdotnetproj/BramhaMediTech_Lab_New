using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
public class Dfrmst_Bal_C
{
	
        internal class Dfrmst_Bal_CException : Exception
        {
            public Dfrmst_Bal_CException(string msg) : base(msg) { }
        }
        #region Properties
        string _STCODE = "", _SDCODE = "";
        public String STCODE    {  get  {   return _STCODE; }   set {   _STCODE = value; } }
        public String SDCODE { get { return _SDCODE; } set { _SDCODE = value; } }
        string _Result = "";
        public String Result
        {
            get
            {
                return _Result;
            }
            set
            {
                _Result = value;
            }
        }
        string _Name;
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        int _Number;
        public int Number
        {
            get
            {
                return _Number;
            }
            set
            {
                _Number = value;
            }
        }
        bool _DefaultFlag = false;
        public bool DefaultFlag
        {
            get
            {
                return _DefaultFlag;
            }
            set
            {
                _DefaultFlag = value;
            }
        }
    private bool flag;
    public bool P_flag
    {
        get { return flag; }
        set { flag = value; }
    }
        #endregion
        #region Constructors
        public Dfrmst_Bal_C()
        {
            this.Name = "";
            this.Number = 0;
            this.Result = "";
           
            this.STCODE = "";
            this.DefaultFlag = false;
        }
        public Dfrmst_Bal_C(String sTestCode, String sName)
        {

            SqlConnection conn = DataAccess.ConInitForDC();
            //SqlCommand sc = new SqlCommand(" SELECT * from dfrmst" +
            //                 " WHERE STCODE = @STCODE and Name=@Name", conn);
            SqlCommand sc = new SqlCommand(" SELECT *  FROM         dfrmst INNER JOIN  MainTest ON dfrmst.STCODE = MainTest.MTCode where MainTest.SDCode =@STCODE and  Name=@Name", conn);//MainTest.SDCode

            // Add the employee ID parameter and set its value.

            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 5)).Value = sTestCode;
            sc.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255)).Value = sName;
            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.Result = sdr["Result"].ToString();
                    this.Name = sdr["Name"].ToString();

                }
                else
                {
                    this.Name = "";
                    throw new Dfrmst_Bal_CException("No Record Fetched.");
                }
            }
            catch (Exception ex)
            {
               // throw;
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
                   // throw;
                }

            }
        }
        #endregion
        public bool Insert(int branchid, int Drsignature)
        {
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
            "INSERT INTO dfrmst " +
            "(STCODE, Result,Name, branchid,sdcode,Drsignature) " +
            "VALUES (@STCODE, @Result,@Name,@branchid,@sdcode,@Drsignature)", conn);

            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = this.STCODE;
            sc.Parameters.Add(new SqlParameter("@Result", SqlDbType.NText)).Value = this.Result;
            sc.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255)).Value = this.Name;
          
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@sdcode", SqlDbType.NVarChar, 50)).Value = this.SDCODE;
            sc.Parameters.Add(new SqlParameter("@Drsignature", SqlDbType.Int)).Value = Drsignature;
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
                    throw;
                }
            }
            return true;
        }//End Insert

        public bool Delete(int branchid)
        {
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                " Delete from dfrmst" +
                " Where STCODE=@STCODE and Name=@Name and branchid="+branchid+"", conn);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = this.STCODE;
            sc.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255)).Value = this.Name;
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
                    throw;
                }
            }
            return true;
        }//End Delete

     
        public bool update(string Name,int branchid)
        {
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand();
            if (this.STCODE == "cl")
            {
                sc = new SqlCommand("" +
            "Update dfrmst " +
            "Set Result=@Result" +
            " Where Name=@PrevName and SDCode=@STCODE and branchid=" + branchid + "", conn);
            }
            else if (this.STCODE == "HP")
            {
                sc = new SqlCommand("" +
            "Update dfrmst " +
            "Set Result=@Result" +
            " Where Name=@PrevName and SDCode=@STCODE and branchid=" + branchid + "", conn);
            }
            else
            {
                sc = new SqlCommand("" +
            "Update dfrmst " +
            "Set Result=@Result" +
            " Where Name=@PrevName and STCODE=@STCODE and branchid=" + branchid + "", conn);
            }
             
            sc.Parameters.Add(new SqlParameter("@Result", SqlDbType.NText)).Value = this.Result;
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar,50)).Value = this.STCODE;
            sc.Parameters.Add(new SqlParameter("@PrevName", SqlDbType.NVarChar, 255)).Value = Name.Trim();
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
                    throw;
                }
            }
            return true;
        }//End Update 
        public Dfrmst_Bal_C(String STCODE, String Name, String AA)
        {

            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand(" SELECT * from dfrmst" +
                             " WHERE STCODE = @STCODE and Name like '" + Name + "%' ", conn);

            // Add the employee ID parameter and set its value.

            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
            sc.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255)).Value = Name;
            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.Result = sdr["Result"].ToString();
                    this.Name = sdr["Name"].ToString();
                   
                }
                else
                {
                    this.STCODE = STCODE;
                    this.Result = "";
                    this.Name = "";
                    //throw new Dfrmst_Bal_CException("No Record Fetched.");
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

        public bool update_Cyto(string Name, int branchid)
        {
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand("" +
            "Update dfrmst_Cyto " +
            "Set Result=@Result" +
            " Where Name=@PrevName  and branchid=" + branchid + "", conn);
            sc.Parameters.Add(new SqlParameter("@Result", SqlDbType.NText)).Value = this.Result;
            sc.Parameters.Add(new SqlParameter("@PrevName", SqlDbType.NVarChar, 255)).Value = Name;
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
                    throw;
                }
            }
            return true;
        }//End Update 

        public Dfrmst_Bal_C(String STCODE, String Name, int A)
        {

            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand(" SELECT * from dfrmst_Cyto" +
                             " WHERE  Name=@Name", conn);

            // Add the employee ID parameter and set its value.

           // sc.Parameters.Add(new SqlParameter("@SdCODE", SqlDbType.NVarChar, 5)).Value = A;
            sc.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255)).Value = Name;
            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.Result = sdr["Result"].ToString();
                    this.Name = sdr["Name"].ToString();
                   
                }
                else
                {
                    this.STCODE = STCODE;
                    this.Result = "";
                    this.Name = "";
                    //throw new Dfrmst_Bal_CException("No Record Fetched.");
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

        public bool Insert_Cyto(int branchid)
        {
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("" +
            "INSERT INTO dfrmst_Cyto " +
            "(STCODE, Result,Name, branchid,SDCODE) " +
            "VALUES (@STCODE, @Result,@Name,@branchid,@SDCODE)", conn);

            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = this.STCODE;
            sc.Parameters.Add(new SqlParameter("@Result", SqlDbType.NText)).Value = this.Result;
            sc.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255)).Value = this.Name;

            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@SDCODE", SqlDbType.NVarChar, 255)).Value = this.SDCODE;
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
                    throw;
                }
            }
            return true;
        }//End Insert

        public Dfrmst_Bal_C(String STCODE, String Name, int A, int B)
        {

            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand(" SELECT * from dfrmst_Histo" +
                             " WHERE  Name=@Name", conn);//STCODE = @STCODE and

            // Add the employee ID parameter and set its value.

            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = STCODE;
            sc.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255)).Value = Name;
            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.Result = sdr["Result"].ToString();
                    this.Name = sdr["Name"].ToString();
                   
                }
                else
                {
                    this.STCODE = STCODE;
                    this.Result = "";
                    this.Name = "";
                    //throw new Dfrmst_Bal_CException("No Record Fetched.");
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

        public bool update_Histo(string Name, int branchid)
        {
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand("" +
            "Update dfrmst_Histo " +
            "Set Result=@Result" +
            " Where Name=@PrevName  and branchid=" + branchid + "", conn);
            sc.Parameters.Add(new SqlParameter("@Result", SqlDbType.NText)).Value = this.Result;
            
            sc.Parameters.Add(new SqlParameter("@PrevName", SqlDbType.NVarChar, 255)).Value = Name;
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
                    throw;
                }
            }
            return true;
        }//End Update 

        public bool Insert_Histo(int branchid)
        {
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("" +
            "INSERT INTO dfrmst_Histo " +
            "(STCODE, Result,Name, branchid,SDCODE) " +
            "VALUES (@STCODE, @Result,@Name,@branchid,@SDCODE)", conn);

            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = this.STCODE;
            sc.Parameters.Add(new SqlParameter("@Result", SqlDbType.NText)).Value = this.Result;
            sc.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 255)).Value = this.Name;

            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@SDCODE", SqlDbType.NVarChar, 255)).Value = this.SDCODE;
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
                    throw;
                }
            }
            return true;
        }//End Insert
}