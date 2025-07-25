using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;


    public class Stformmst_Bal_C
    {

        public Stformmst_Bal_C()
        {
            this.STCODE = "";
            this.testname = "";
            this.TestResult_Format = "";
            this.PatRegID ="";
            this.FID = "";           
            this.TestNo = 0;
            this.MTCode = "";
            this.normalRange = "";
            this.unit = "";
            this.EntryDate = Date.getdate();
            this.testorderno = 0;
            this.FinancialYearID = "";
            this.Branchid = 1;
         
            this.Method = "";
           
            this.SDCode="";
         
            this.RemarkFlag = false;
            this.Remarks = "";
        }

       


        public Stformmst_Bal_C(string STCODE, string Patreg_ID, string FFID, int branchid)
        {
            this.PatRegID = Patreg_ID;
            this.FID = FFID; 

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "Select * from ResMst" +
               " WHERE STCODE=@STCODE and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (string)(STCODE);            
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar,50)).Value = this.PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;

            SqlDataReader sdr = null;

            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.testname = sdr["testname"].ToString();
                    this.Maintestname = sdr["Maintestname"].ToString();
                    this.TestResult_Format = sdr["ResultTemplate"].ToString();
                    this.PatRegID = sdr["PatRegID"].ToString();
                    this.FID = sdr["FID"].ToString();
                  
                    if (sdr["TestNo"] != DBNull.Value)
                        this.TestNo = Convert.ToInt32(sdr["TestNo"]);
                    this.MTCode = sdr["MTCode"].ToString();
                    this.normalRange = sdr["normalRange"].ToString();
                    this.unit = sdr["unit"].ToString();
                    this.EntryDate = Convert.ToDateTime(sdr["EntryDate"]);
                    this.testorderno = Convert.ToInt32(sdr["testorderno"]);
                    this.FinancialYearID = sdr["FinancialYearID"].ToString();
                    this.Branchid = Convert.ToInt32(sdr["branchid"]);
                  
                    this.Method = sdr["Method"].ToString();
                   

                    this.SDCode = sdr["SDCode"].ToString();
                  
                    if (sdr["RangeFlag"] != DBNull.Value)
                        this.RangeFlag = sdr["RangeFlag"].ToString();
                    else
                        this.RangeFlag = "";

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
            // Implement Update logic.

        }

        public Stformmst_Bal_C(string MTCode, string STCODE, string Patreg_ID, string FFID, int branchid) 
        {
            this.PatRegID = Patreg_ID;
            this.FID = FFID;

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "Select * from ResMst" +
               " WHERE MTCode=@MTCode and STCODE=@STCODE and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (MTCode);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (STCODE);

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.Int)).Value = this.PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
           

            SqlDataReader sdr = null;

            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.testname = sdr["testname"].ToString();
                    this.Maintestname = sdr["Maintestname"].ToString();
                    this.TestResult_Format = sdr["ResultTemplate"].ToString();
                    this.PatRegID = sdr["PatRegID"].ToString();
                    this.FID = sdr["FID"].ToString();
                  
                    if (sdr["TestNo"] != DBNull.Value)
                        this.TestNo = Convert.ToInt32(sdr["TestNo"]);
                    this.MTCode = sdr["MTCode"].ToString();
                    this.normalRange = sdr["normalRange"].ToString();
                    this.unit = sdr["unit"].ToString();
                    this.EntryDate = Convert.ToDateTime(sdr["EntryDate"]);
                    this.testorderno = Convert.ToInt32(sdr["testorderno"]);
                    this.FinancialYearID = sdr["FinancialYearID"].ToString();
                    this.Branchid = Convert.ToInt32(sdr["branchid"]);
                   
                    this.Method = sdr["Method"].ToString();
                   
                    this.SDCode = sdr["SDCode"].ToString();
                    


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
            // Implement Update logic.

        }
        public Stformmst_Bal_C(string MTCode, DateTime doe, string Patreg_ID, string FFID, int branchid)
        {
            this.PatRegID = (Patreg_ID);
            this.FID = FFID;

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "Select * from ResMst" +
               " WHERE MTCode=@MTCode and PatRegID=@PatRegID and FID=@FID and entrydate=@doe and branchid=" + branchid + "", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (string)(MTCode);            
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.Int)).Value = this.PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
            sc.Parameters.Add(new SqlParameter("@doe", SqlDbType.DateTime)).Value = doe;

            SqlDataReader sdr = null;

            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.testname = sdr["testname"].ToString();
                    this.TestResult_Format = sdr["ResultTemplate"].ToString();
                    this.PatRegID = sdr["PatRegID"].ToString();
                    this.FID = sdr["FID"].ToString();
                   
                    if (sdr["TestNo"] != DBNull.Value)
                        this.TestNo = Convert.ToInt32(sdr["TestNo"]);
                    this.MTCode = sdr["MTCode"].ToString();
                    this.normalRange = sdr["normalRange"].ToString();
                    this.unit = sdr["unit"].ToString();
                    this.EntryDate = Convert.ToDateTime(sdr["EntryDate"]);
                    this.testorderno = Convert.ToInt32(sdr["testorderno"]);
                    this.FinancialYearID = sdr["FinancialYearID"].ToString();
                    this.Branchid = Convert.ToInt32(sdr["branchid"]);
                   
                    this.Method = sdr["Method"].ToString();
                   

                    this.SDCode = sdr["SDCode"].ToString();
                 


                }
                else
                {
                    throw new Stformmst_Bal_C.Stformmst_Bal_CTableException("No Record Fetched.");
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
            // Implement Update logic.

        }


        public Stformmst_Bal_C(string MTCode, string STCODE, string Patreg_ID, string FFID, DateTime doe, int branchid) 
        {
            this.PatRegID = (Patreg_ID);
            this.FID = FFID;

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "Select * from ResMst" +
               " WHERE MTCode=@MTCode and STCODE=@STCODE and PatRegID=@PatRegID and FID=@FID and EntryDate=@doe and branchid=" + branchid + "", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (MTCode);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (STCODE);

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.Int)).Value = this.PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
            sc.Parameters.Add(new SqlParameter("@doe", SqlDbType.DateTime)).Value = doe;

            SqlDataReader sdr = null;

            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.testname = sdr["testname"].ToString();
                    this.TestResult_Format = sdr["ResultTemplate"].ToString();
                    this.PatRegID = sdr["PatRegID"].ToString();
                    this.FID = sdr["FID"].ToString();
                  
                    if (sdr["TestNo"] != DBNull.Value)
                        this.TestNo = Convert.ToInt32(sdr["TestNo"]);
                    this.MTCode = sdr["MTCode"].ToString();
                    this.normalRange = sdr["normalRange"].ToString();
                    this.unit = sdr["unit"].ToString();
                    this.EntryDate = Convert.ToDateTime(sdr["EntryDate"]);
                    this.testorderno = Convert.ToInt32(sdr["testorderno"]);
                    this.FinancialYearID = sdr["FinancialYearID"].ToString();
                    this.Branchid = Convert.ToInt32(sdr["branchid"]);
                  
                    this.Method = sdr["Method"].ToString();
                   
                    this.SDCode = sdr["SDCode"].ToString();
                   


                }
                else
                {
                    throw new Stformmst_Bal_C.Stformmst_Bal_CTableException("No Record Fetched.");
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
            // Implement Update logic.

        } //public formulatable


        public Stformmst_Bal_C(string MTCode, string PatRegID, string FID, int i, int branchid) 
        {
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "Select * from ResMst" +
               " WHERE MTCode=@MTCode and  PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (MTCode);           
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = (PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = (FID);

            SqlDataReader sdr = null;

            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.testname = sdr["testname"].ToString();
                    this.TestResult_Format = sdr["ResultTemplate"].ToString();
                    this.PatRegID =sdr["PatRegID"].ToString();
                    this.FID = sdr["FID"].ToString();
                  
                    if (sdr["TestNo"] != DBNull.Value)
                        this.TestNo = Convert.ToInt32(sdr["TestNo"]);
                    this.MTCode = sdr["MTCode"].ToString();
                    this.normalRange = sdr["normalRange"].ToString();
                    this.unit = sdr["unit"].ToString();
                    this.EntryDate = Convert.ToDateTime(sdr["EntryDate"]);
                    this.testorderno = Convert.ToInt32(sdr["testorderno"]);
                    this.FinancialYearID = sdr["FinancialYearID"].ToString();
                    this.Branchid = Convert.ToInt32(sdr["branchid"]);
                 
                    this.Method = sdr["Method"].ToString();
                   

                    this.SDCode = sdr["SDCode"].ToString();
                    

                }
                else
                {
                    throw new Stformmst_Bal_C.Stformmst_Bal_CTableException("No Record Fetched.");
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
            // Implement Update logic.

        } 

        public Stformmst_Bal_C(string MTCode, string SDCode, int i, int branchid) 
        {
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "Select * from ResMst" +
               " WHERE MTCode=@MTCode and SDCode=@SDCode and branchid=" + branchid + "", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (MTCode);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = (SDCode);
            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.testname = sdr["testname"].ToString();
                    this.TestResult_Format = sdr["ResultTemplate"].ToString();
                    this.PatRegID = sdr["PatRegID"].ToString();
                    this.FID = sdr["FID"].ToString();
                 
                    if (sdr["TestNo"] != DBNull.Value)
                        this.TestNo = Convert.ToInt32(sdr["TestNo"]);
                    this.MTCode = sdr["MTCode"].ToString();
                    this.normalRange = sdr["normalRange"].ToString();
                    this.unit = sdr["unit"].ToString();
                    this.EntryDate = Convert.ToDateTime(sdr["EntryDate"]);
                    this.testorderno = Convert.ToInt32(sdr["testorderno"]);
                    this.FinancialYearID = sdr["FinancialYearID"].ToString();
                    this.Branchid = Convert.ToInt32(sdr["branchid"]);
                   
                    this.Method = sdr["Method"].ToString();
                   
                    this.SDCode = sdr["SDCode"].ToString();
                    

                }
                else
                {
                    throw new Stformmst_Bal_C.Stformmst_Bal_CTableException("No Record Fetched.");
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
            // Implement Update logic.

        }


        public Stformmst_Bal_C(string PatRegID, string FID, int branchid) 
        {
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "Select * from ResMst" +
               " WHERE PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);           
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar,50)).Value = (PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = (FID);

            SqlDataReader sdr = null;

            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();
                if (sdr != null && sdr.Read())
                {
                    this.STCODE = sdr["STCODE"].ToString();
                    this.testname = sdr["testname"].ToString();
                    this.TestResult_Format = sdr["ResultTemplate"].ToString();
                    this.PatRegID = sdr["PatRegID"].ToString();
                    this.FID = sdr["FID"].ToString();
                    
                    if (sdr["TestNo"] != DBNull.Value)
                        this.TestNo = Convert.ToInt32(sdr["TestNo"]);
                    this.MTCode = sdr["MTCode"].ToString();
                    this.normalRange = sdr["normalRange"].ToString();
                    this.unit = sdr["unit"].ToString();
                    this.EntryDate = Convert.ToDateTime(sdr["EntryDate"]);
                    this.testorderno = Convert.ToInt32(sdr["testorderno"]);
                    this.FinancialYearID = sdr["FinancialYearID"].ToString();
                    this.Branchid = Convert.ToInt32(sdr["branchid"]);
                   
                    this.Method = sdr["Method"].ToString();
                   

                    this.SDCode = sdr["SDCode"].ToString();
                   

                }
                else
                {
                    throw new Stformmst_Bal_C.Stformmst_Bal_CTableException("No Record Fetched.");
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
            // Implement Update logic.

        } 
        public bool Update(SubTest_Bal_C SBC, string STCODE, int branchid)
        {          
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "UPDATE ResMst " +
                "SET STCODE=@testcodenew " +
               " WHERE STCODE=@TestCodeold and branchid=" + branchid + "", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@testcodenew", SqlDbType.NVarChar, 50)).Value = (string)(STCODE);
            sc.Parameters.Add(new SqlParameter("@TestCodeold", SqlDbType.NVarChar, 50)).Value = (string)(SBC.STCODE);

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
        public bool Update(string STCODE, string Patreg_ID, string FFID, int branchid)
        {
            this.PatRegID = (Patreg_ID);
            this.FID = FFID;
            this.STCODE = STCODE;

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "UPDATE ResMst " +
                " SET testname=@testname,ResultTemplate=@ResultTemplate,SpecialNote=@SpecialNote,TestNo=@TestNo,MTCode=@MTCode,normalRange=@normalRange,unit=@unit,EntryDate=@EntryDate,testorderno=@testorderno,FinancialYearID=@FinancialYearID,Branchid=@Branchid,Method=@Method,SDCode=@SDCode,Maintestname=@Maintestname,UnitCode=@UnitCode" +
               " WHERE STCODE=@STCODE and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);
           
            if (this.Maintestname != null)
                sc.Parameters.Add(new SqlParameter("@Maintestname", SqlDbType.NVarChar, 255)).Value = this.Maintestname;
            else
                sc.Parameters.Add(new SqlParameter("@Maintestname", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.STCODE != null)
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.VarChar, 50)).Value = this.STCODE;
            else
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            if (this.testname != null)
                sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = this.testname;
            else
                sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.TestResult_Format != null)
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.TestResult_Format;
            else
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.PatRegID != null)
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar,250)).Value = this.PatRegID;
            else
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = DBNull.Value;

          
            if (this.FID != null)
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = this.FID;
            else
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = DBNull.Value;

          
            if (this.TestNo != null)
                sc.Parameters.Add(new SqlParameter("@TestNo", SqlDbType.Int)).Value = Convert.ToInt32(this.TestNo);
            else
                sc.Parameters.Add(new SqlParameter("@TestNo", SqlDbType.Int)).Value = DBNull.Value;

            if (this.MTCode != null)
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 50)).Value = this.MTCode;
            else
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            if (this.normalRange != null)
                sc.Parameters.Add(new SqlParameter("@normalRange", SqlDbType.NVarChar, 255)).Value = this.normalRange;
            else
                sc.Parameters.Add(new SqlParameter("@normalRange", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.unit != null)
                sc.Parameters.Add(new SqlParameter("@unit", SqlDbType.NVarChar, 50)).Value = this.unit;
            else
                sc.Parameters.Add(new SqlParameter("@unit", SqlDbType.NVarChar, 50)).Value = DBNull.Value;


            sc.Parameters.Add(new SqlParameter("@EntryDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.EntryDate);
           
            if (this.testorderno != null)
                sc.Parameters.Add(new SqlParameter("@testorderno", SqlDbType.Int)).Value = Convert.ToInt32(this.testorderno);
            else
                sc.Parameters.Add(new SqlParameter("@testorderno", SqlDbType.Int)).Value = DBNull.Value;

            if (this.FinancialYearID != null)
                sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = this.FinancialYearID;
            else
                sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = DBNull.Value;

            if (this.Branchid != null)
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(this.Branchid);
            else
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = DBNull.Value;

           
            if (this.Method != null)
                sc.Parameters.Add(new SqlParameter("@Method", SqlDbType.VarChar, 500)).Value = this.Method;
            else
                sc.Parameters.Add(new SqlParameter("@Method", SqlDbType.VarChar, 500)).Value = DBNull.Value;

            
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.VarChar, 50)).Value = this.SDCode;
           

            if (this.UnitCode != null)
                sc.Parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar, 100)).Value = this.UnitCode;
            else
                sc.Parameters.Add(new SqlParameter("@UnitCode", SqlDbType.NVarChar,100)).Value = DBNull.Value;

            SqlDataReader sdr = null;

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


        public bool Update(string MTCode, int i, string Patreg_ID, string FFID, int branchid)
        {
            this.PatRegID = (Patreg_ID);
            this.FID = FFID;            
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "UPDATE ResMst " +
                " SET STCODE=@STCODE,testname=@testname,ResultTemplate=@ResultTemplate,PatRegID=@PatRegID,FID=@FID,SpecialNote=@SpecialNote,TestNo=@TestNo,MTCode=@MTCode,normalRange=@normalRange,unit=@unit,EntryDate=@EntryDate,testorderno=@testorderno,FinancialYearID=@FinancialYearID,Method=@Method,SDCode=@SDCode " +
               " WHERE MTCode=@MTCode and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);
           
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (MTCode);            

            if (this.STCODE != null)
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.VarChar, 50)).Value = this.STCODE;
            else
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            if (this.testname != null)
                sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = this.testname;
            else
                sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.TestResult_Format != null)
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.TestResult_Format;
            else
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.PatRegID != null)
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.Int)).Value = Convert.ToInt32(this.PatRegID);
            else
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.Int)).Value = DBNull.Value;

            if (this.FID != null)
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = this.FID;
            else
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = DBNull.Value;

         
            if (this.TestNo != null)
                sc.Parameters.Add(new SqlParameter("@TestNo", SqlDbType.Int)).Value = Convert.ToInt32(this.TestNo);
            else
                sc.Parameters.Add(new SqlParameter("@TestNo", SqlDbType.Int)).Value = DBNull.Value;


            if (this.normalRange != null)
                sc.Parameters.Add(new SqlParameter("@normalRange", SqlDbType.NVarChar, 255)).Value = this.normalRange;
            else
                sc.Parameters.Add(new SqlParameter("@normalRange", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.unit != null)
                sc.Parameters.Add(new SqlParameter("@unit", SqlDbType.NVarChar, 50)).Value = this.unit;
            else
                sc.Parameters.Add(new SqlParameter("@unit", SqlDbType.NVarChar, 50)).Value = DBNull.Value;


            sc.Parameters.Add(new SqlParameter("@EntryDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.EntryDate);
          
            if (this.testorderno != null)
                sc.Parameters.Add(new SqlParameter("@testorderno", SqlDbType.Int)).Value = Convert.ToInt32(this.testorderno);
            else
                sc.Parameters.Add(new SqlParameter("@testorderno", SqlDbType.Int)).Value = DBNull.Value;

            if (this.FinancialYearID != null)
                sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = this.FinancialYearID;
            else
                sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = DBNull.Value;

            if (this.Branchid != null)
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(this.Branchid);
            else
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = DBNull.Value;

         
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.VarChar, 50)).Value = this.SDCode;
           

            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                try
                {
                    sc.ExecuteNonQuery();
                }
                catch
                {
                    throw;
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
            // Implement Update logic.
            return true;
        } //update End


       
        public bool UpdateTestnameandOrdNo(string MTCode, string STCODE, string testname, int tordno, string oldtestname, int branchid)
        {
            
            SqlConnection conn = DataAccess.ConInitForDC(); 
            
            SqlCommand sc = new SqlCommand("" +
                "UPDATE ResMst " +
                "SET testname=@testname,testorderno=@testorderno" +
               " WHERE MTCode=@MTCode and STCODE=@STCODE and branchid=" + branchid + "", conn);
         
            if (this.STCODE != null)
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.VarChar, 50)).Value = STCODE;
            else
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            if (this.MTCode != null)
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 50)).Value = MTCode;
            else
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            if (this.testname != null)
                sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = testname;
            else
                sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.testorderno != null)
                sc.Parameters.Add(new SqlParameter("@testorderno", SqlDbType.Int)).Value = Convert.ToInt32(tordno);
            else
                sc.Parameters.Add(new SqlParameter("@testorderno", SqlDbType.Int)).Value = DBNull.Value;

            sc.Parameters.Add(new SqlParameter("@oldtestname", SqlDbType.NVarChar, 255)).Value = oldtestname;
            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                try
                {
                    sc.ExecuteNonQuery();
                }
                catch
                {
                    throw;
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
            // Implement Update logic.
            return true;
        }

    
  
        public bool Delete(string PatRegID, string FID, int branchid)
        {

            SqlConnection conn = DataAccess.ConInitForDC();

            SqlCommand sc = new SqlCommand("delete from ResMst " +
                "where PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;

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
                "Insert into ResMst(STCODE,testname,ResultTemplate,PatRegID,FID,TestNo,MTCode,normalRange,unit,EntryDate,testorderno,FinancialYearID,Branchid,Method,SDCode,Maintestname,UnitCode) " +
                "values(@STCODE,@testname,@ResultTemplate,@PatRegID,@FID,@TestNo,@MTCode,@normalRange,@unit,@EntryDate,@testorderno,@FinancialYearID,@BranchId,@Method,@SDCode,@Maintestname,@UnitCode)", conn);
           
            if (this.STCODE != null)
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.VarChar, 50)).Value = this.STCODE;
            else
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            if (this.testname != null)
                sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = this.testname;
            else
                sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.Maintestname != null)
                sc.Parameters.Add(new SqlParameter("@Maintestname", SqlDbType.NVarChar, 255)).Value = this.Maintestname;
            else
                sc.Parameters.Add(new SqlParameter("@Maintestname", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.TestResult_Format != null)
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = this.TestResult_Format;
            else
                sc.Parameters.Add(new SqlParameter("@ResultTemplate", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.PatRegID != null)
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.VarChar,250)).Value = (this.PatRegID);
            else
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.VarChar, 250)).Value = DBNull.Value;

           
            if (this.FID != null)
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = this.FID;
            else
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = DBNull.Value;

         
            if (this.TestNo != null)
                sc.Parameters.Add(new SqlParameter("@TestNo", SqlDbType.Int)).Value = Convert.ToInt32(this.TestNo);
            else
                sc.Parameters.Add(new SqlParameter("@TestNo", SqlDbType.Int)).Value = DBNull.Value;

            if (this.MTCode != null)
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 50)).Value = this.MTCode;
            else
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            if (this.normalRange != null)
                sc.Parameters.Add(new SqlParameter("@normalRange", SqlDbType.NVarChar, 255)).Value = this.normalRange;
            else
                sc.Parameters.Add(new SqlParameter("@normalRange", SqlDbType.NVarChar, 255)).Value = DBNull.Value;

            if (this.unit != null)
                sc.Parameters.Add(new SqlParameter("@unit", SqlDbType.NVarChar, 50)).Value = this.unit;
            else
                sc.Parameters.Add(new SqlParameter("@unit", SqlDbType.NVarChar, 50)).Value = DBNull.Value;


            sc.Parameters.Add(new SqlParameter("@EntryDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(this.EntryDate);
            
            if (this.testorderno != null)
                sc.Parameters.Add(new SqlParameter("@testorderno", SqlDbType.Int)).Value = Convert.ToInt32(this.testorderno);
            else
                sc.Parameters.Add(new SqlParameter("@testorderno", SqlDbType.Int)).Value = DBNull.Value;

            if (this.FinancialYearID != null)
                sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = this.FinancialYearID;
            else
                sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = DBNull.Value;

            if (this.Branchid != null)
                sc.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int)).Value = Convert.ToInt32(this.Branchid);
            else
                sc.Parameters.Add(new SqlParameter("@BranchId", SqlDbType.Int)).Value = DBNull.Value;

           
            if (this.Method != null)
                sc.Parameters.Add(new SqlParameter("@Method", SqlDbType.VarChar, 500)).Value = this.Method;
            else
                sc.Parameters.Add(new SqlParameter("@Method", SqlDbType.VarChar, 500)).Value = DBNull.Value;

         
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.VarChar, 50)).Value = this.SDCode;


            if (this.UnitCode != null)
                sc.Parameters.Add(new SqlParameter("@unitCode", SqlDbType.VarChar, 100)).Value = this.UnitCode;
            else
                sc.Parameters.Add(new SqlParameter("@unitcode", SqlDbType.VarChar, 100)).Value = DBNull.Value;
          
            
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
        } //Insert End

        public bool UpdateNormalFlag(string STCODE, string Patreg_ID, string FFID, string normalflag, int branchid)
        {
            this.PatRegID = (Patreg_ID);
            this.FID = FFID;
            this.STCODE = STCODE;

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "UPDATE ResMst " +
                "SET RangeFlag=@RangeFlag" +
               " WHERE STCODE=@STCODE and PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);
           
            if (this.STCODE != null)
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.VarChar, 50)).Value = this.STCODE;
            else
                sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            if (this.PatRegID != null)
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = this.PatRegID;
            else
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = DBNull.Value;

            if (this.FID != null)
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = this.FID;
            else
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            sc.Parameters.Add(new SqlParameter("@RangeFlag", SqlDbType.NChar,10)).Value = normalflag;
           

            SqlDataReader sdr = null;

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
        public bool Temp_StoreTest_FormatTableExist(string MTCode, string STCODE, string Patreg_ID, string FFID) 
        {
            this.PatRegID = Patreg_ID;
            this.FID = FFID;

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "Select Count(*) from ResMst " +
               " WHERE MTCode=@MTCode and STCODE=@STCODE and PatRegID=@PatRegID and FID=@FID", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (MTCode);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (STCODE);

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.Int)).Value = this.PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
            bool flag = true;
            try
            {
                conn.Open();
                int i = Convert.ToInt32(sc.ExecuteScalar());
                if (i > 0)
                { flag = false; }

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
           
            return flag;
        }
        public bool Temp_StoreTest_FormatTableExist(string MTCode, string STCODE, string testname, string Patreg_ID, string FFID, int branchid, string blank) 
        {
            this.PatRegID = Patreg_ID;
            this.FID = FFID;

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand("" +
                "Select Count(*) from ResMst" +
               " WHERE MTCode=@MTCode and STCODE=@STCODE and testname=@testname and PatRegID=@PatRegID and FID=@FID", conn);
            // Add the employee ID parameter and set its value.
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = (MTCode);
            sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 50)).Value = (STCODE);
            sc.Parameters.Add(new SqlParameter("@testname", SqlDbType.NVarChar, 50)).Value = (testname);
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.Int)).Value = this.PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = this.FID;
            bool flag = true;
            try
            {
                conn.Open();
                int i = Convert.ToInt32(sc.ExecuteScalar());
                if (i > 0)
                { flag = false; }

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
            return flag;
        } 
        internal class Stformmst_Bal_CTableException : Exception
        {
            public Stformmst_Bal_CTableException(string msg) : base(msg) { }
        }

      
        public bool InsertUpdate_Result()
        {
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand();
            sc.CommandType = CommandType.StoredProcedure;
            sc.Connection = conn;
            sc.CommandText = "[SP_phserdatarecfrm]";

            if (this.PatRegID != null)
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.VarChar, 250)).Value = (this.PatRegID);
            else
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.VarChar, 250)).Value = DBNull.Value;

            if (this.FID != null)
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = this.FID;
            else
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            if (this.MTCode != null)
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 50)).Value = this.MTCode;
            else
                sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.VarChar, 50)).Value = DBNull.Value;

            if (this.FinancialYearID != null)
                sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = this.FinancialYearID;
            else
                sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.NVarChar, 50)).Value = DBNull.Value;

            if (this.Branchid != null)
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(this.Branchid);
            else
                sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = DBNull.Value;

            if (this.UnitCode != null)
                sc.Parameters.Add(new SqlParameter("@UnitCode", SqlDbType.VarChar, 100)).Value = this.UnitCode;
            else
                sc.Parameters.Add(new SqlParameter("@UnitCode", SqlDbType.VarChar, 100)).Value = DBNull.Value;


            SqlDataReader sdr = null;

            try
            {
                conn.Open();
                sc.CommandTimeout = 5000;
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
        } 

        public bool Delete_ResMst_Code(string PatRegID, string FID, int branchid,string MTCode)
        {
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = new SqlCommand(" delete from ResMst" +
                " where PatRegID=@PatRegID and FID=@FID and branchid=@branchid and MTCode=@MTCode ", conn);
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 50)).Value = PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
            sc.Parameters.AddWithValue("@MTCode",MTCode);
            sc.Parameters.AddWithValue("@branchid",branchid);

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
        } 

        #region Properties
        private string testResult_Format;
        public string TestResult_Format
        {
            get { return testResult_Format; }
            set { testResult_Format = value; }
        }

        private int branchid;
        public int Branchid
        {
            get { return branchid; }
            set { branchid = value; }
        }

        private string testname1;
        public string testname
        {
            get { return testname1; }
            set { testname1 = value; }
        }

        private string maintestname;
        public string Maintestname
        {
            get { return maintestname; }
            set { maintestname = value; }
        }

        private string sTCODE;
        public string STCODE
        {
            get { return sTCODE; }
            set { sTCODE = value; }
        }

        private string normalRange1;
        public string normalRange
        {
            get { return normalRange1; }
            set { normalRange1 = value; }
        }

        private string fID;
        public string FID
        {
            get { return fID; }
            set { fID = value; }
        }
        private string _PatRegID;
        public string PatRegID
        {
            get { return _PatRegID; }
            set { _PatRegID = value; }
        }

        private string mTCode;
        public string MTCode
        {
            get { return mTCode; }
            set { mTCode = value; }
        }

        private string unit1;
        public string unit
        {
            get { return unit1; }
            set { unit1 = value; }
        }

        private DateTime entryDate;
        public DateTime EntryDate
        {
            get { return entryDate; }
            set { entryDate = value; }
        }

        private int testorderno1;
        public int testorderno
        {
            get { return testorderno1; }
            set { testorderno1 = value; }
        }

      

        private string method;
        public string Method
        {
            get { return method; }
            set { method = value; }
        }

       

        private string financialYearID;
        public string FinancialYearID
        {
            get { return financialYearID; }
            set { financialYearID = value; }
        }

       

        private string defaultTestMethod;
        public string DefaultTestMethod
        {
            get { return defaultTestMethod; }
            set { defaultTestMethod = value; }
        }

        private string testMethod;
        public string TestMethod
        {
            get { return testMethod; }
            set { testMethod = value; }
        }
        private string settestresult;
        public string Settestresult
        {
            get { return settestresult; }
            set { settestresult = value; }
        }

       
        private int testNo;
        public int TestNo
        {
            get { return testNo; }
            set { testNo = value; }
        }

        private string sDCode;
        public string SDCode
        {
            get { return sDCode; }
            set { sDCode = value; }
        }       

        private string _Remarks;
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        private bool _RemarkFlag;
        public bool RemarkFlag
        {
            get { return _RemarkFlag; }
            set { _RemarkFlag = value; }
        }

        private string _UnitCode;
        public string UnitCode
        {
            get { return _UnitCode; }
            set { _UnitCode = value; }
        }

        private string _RangeFlag;
        public string RangeFlag
        {
            get { return _RangeFlag; }
            set { _RangeFlag = value; }
        }

        #endregion

    }

