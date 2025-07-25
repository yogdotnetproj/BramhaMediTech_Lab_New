using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public class Subdepartment_Bal_C
    {
        public Subdepartment_Bal_C()
        {
            this.subdeptName = "";
            this.SDCode = "";           
            this.Patregdate = Date.getdate();
            this.SDOrderNo = 0;
            this.Remark = "";
            this.DigModule = 0;
        }

        public Subdepartment_Bal_C(string _Code, object branchid)
        {
            this.SDCode = _Code;
           
            
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand(" SELECT subdeptName, SDCode,   DateofEntry, SDOrderNo, Remark,   branchid, DigModule" +
                             " FROM SubDepartment " +
                             " WHERE SDCode=@SDCode and branchid=@branchid ", conn);//--SDCode

            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = (string)(this.SDCode);
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

            SqlDataReader sdr = null;
            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                // This is not a while loop. It only loops once.
                if (sdr != null && sdr.Read())
                {
                    // The IEnumerable contains DataRowView objects.
                    this.subdeptName = sdr["subdeptName"].ToString();
                    this.SDCode = sdr["SDCode"].ToString();

                    this.Patregdate = (DateTime)sdr["DateofEntry"];
                    this.SDOrderNo = Convert.ToInt32(sdr["SDOrderNo"]);
                    this.Remark = sdr["Remark"].ToString();

                    if (sdr["DigModule"] is DBNull)
                        this.P_DigModule = 0;
                    else
                        this.P_DigModule = Convert.ToInt32(sdr["DigModule"]);
                    

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
                
            }
        }

        internal class Subdepartment_Bal_CTableException : Exception
        {
            public Subdepartment_Bal_CTableException(string msg) : base(msg) { }
        }

        #region Properties

        private string subdeptName;
        public string SubdeptName
        {
            get { return subdeptName; }
            set { subdeptName = value; }
        }
        private int DigModule;
        public int P_DigModule
         {
        get { return DigModule; }
        set { DigModule = value; }
       }

        private int ID;
        public int P_ID
        {
            get { return ID; }
            set { ID = value; }
        }

        private string sDCode;
        public string SDCode
        {
            get { return sDCode; }
            set { sDCode = value; }
        }     

       

        private DateTime _Patregdate;
        public DateTime Patregdate
        {
            get { return _Patregdate; }
            set { _Patregdate = value; }
        }

        private int SDOrderNo;
        public int sDOrderNo
        {
            get { return SDOrderNo; }
            set { SDOrderNo = value; }
        }

        private string remark;
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

  
        private string username;
        public string P_username
        {
            get { return username; }
            set { username = value; }
        }
        private string _testname;
        public string testname
        {
            get { return _testname; }
            set { _testname = value; }
        }
        private string mTCode;
        public string MTCode
        {
            get { return mTCode; }
            set { mTCode = value; }
        } 

        #endregion

        public bool Insert(int branchid)
        {
            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = new SqlCommand("" +
            "INSERT INTO SubDepartment " +
            "(subdeptName, SDCode,   DateofEntry, SDOrderNo,DeptID, Remark,  branchid,DigModule,ID) " +
            "VALUES (@subdeptName, @SDCode,   @DateofEntry,@SDOrderNo,@SDOrderNo,@remark,@branchid,@DigModule,@ID)", conn);

            sc.Parameters.Add(new SqlParameter("@subdeptName", SqlDbType.NVarChar, 100)).Value = (string)(this.subdeptName);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = (string)(this.SDCode);          
           
            sc.Parameters.Add(new SqlParameter("@DateofEntry", SqlDbType.DateTime, 4)).Value = (DateTime)(this.Patregdate);
            sc.Parameters.Add(new SqlParameter("@SDOrderNo", SqlDbType.Int, 9)).Value = (int)(this.SDOrderNo);
            sc.Parameters.Add(new SqlParameter("@remark", SqlDbType.NVarChar, 250)).Value = (string)(this.Remark);          
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

            sc.Parameters.Add(new SqlParameter("@DigModule", SqlDbType.Int)).Value = P_DigModule;
            sc.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int)).Value = P_ID;
           

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
        }//End Insert

    public bool Delete(object branchid)
    {
           
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                " Delete from SubDepartment" +
                " Where SDCode=@SDCode and branchid=@branchid", conn);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = (string)(this.SDCode);
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
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
        }//End Delete

    public bool update(string sPrevCode, object branchid)
    {
           

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
            "Update SubDepartment " +
            "Set subdeptName=@subdeptName ,  DateofEntry=@DateofEntry,  Remark=@remark,DigModule=@DigModule " + //SDOrderNo=@SDOrderNo,
            "Where SDCode=@SDCodePrev and branchid=@branchid", conn);
            sc.Parameters.Add(new SqlParameter("@subdeptName", SqlDbType.NVarChar, 100)).Value = (string)(this.subdeptName);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = (string)(this.SDCode);

            sc.Parameters.Add(new SqlParameter("@DateofEntry", SqlDbType.DateTime, 4)).Value = (DateTime)(this.Patregdate);
            sc.Parameters.Add(new SqlParameter("@SDOrderNo", SqlDbType.Int, 9)).Value = (int)(this.SDOrderNo);
            sc.Parameters.Add(new SqlParameter("@remark", SqlDbType.NVarChar, 250)).Value = (string)(this.Remark);
          
            sc.Parameters.Add(new SqlParameter("@SDCodePrev", SqlDbType.NVarChar, 50)).Value = (string)(sPrevCode);
            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
            sc.Parameters.Add(new SqlParameter("@DigModule", SqlDbType.Int)).Value = P_DigModule;
            SqlDataReader sdr = null;
            try
            {
                conn.Open();
                sc.ExecuteNonQuery();
            }
            catch (Exception)
            {

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
        
    }//End Class



