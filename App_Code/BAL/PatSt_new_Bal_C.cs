using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

    public class PatSt_new_Bal_C
    {
    
        public static ICollection Get_Patst_Authorized_(string PatRegID, string FID, int branchid)
        {            
            ArrayList al = new ArrayList();
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;
            sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized' and branchid=" + branchid + " order by SDCode", conn);
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
          
            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {                    
                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();
                      
                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
                       
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        al.Add(Obj_PBC);
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
                    throw;
                }
                
            }
            return al;
        }

        public static ICollection GetPatst_Authorized_notingroup_Without(string PatRegID, string FID, int branchid, int maindeptid, string subdept)
        {

            ArrayList al = new ArrayList();
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = null;
            if (subdept != "")
            {
                if (maindeptid == 0)
                    sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID   AND SDCode in (" + subdept + " ) and (PackageCode = '' ) and branchid=" + branchid + "  order by SDCode", conn);//and Patauthicante = 'Authorized'
                else
                    sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID    AND SDCode in (" + subdept + " ) and (PackageCode = '' ) and branchid=" + branchid + " and SDCode in (select SDCode from SubDepartment where DigModule=" + maindeptid + ") order by SDCode", conn); //and Patauthicante = 'Authorized'
            }
            else
            {
                if (maindeptid == 0)
                    sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID   and (PackageCode = '' OR PackageCode is null) and branchid=" + branchid + "  order by SDCode", conn); //
                else
                    sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID   and (PackageCode = '' OR PackageCode is null) and branchid=" + branchid + " and SDCode in (select SDCode from SubDepartment where DigModule=" + maindeptid + ") order by SDCode", conn); //and Patauthicante = 'Authorized'

            }
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {
                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();

                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);

                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        Obj_PBC.PackageCode = sdr["PackageCode"].ToString();
                        Obj_PBC.Patauthicante = sdr["Patauthicante"].ToString();
                        al.Add(Obj_PBC);
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
                    throw;
                }

            }
            return al;
        }

        public static ICollection GetPatst_Authorized_ingroup_Without(string PatRegID, string FID, int branchid, int maindeptid, string subdept)
        {

            ArrayList al = new ArrayList();

            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = null;

            if (subdept != "")
            {
                sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID    AND SDCode in (" + subdept + " ) and (PackageCode <> '') and branchid=" + branchid + " and SDCode in (select SDCode from SubDepartment where DigModule=" + maindeptid + ") order by SDCode", conn);
            }
            else
            {
                sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID  and PackageCode <> ''   and branchid=" + branchid + " order by PackageCode", conn);

            }
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {

                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();

                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);

                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        Obj_PBC.PackageCode = sdr["PackageCode"].ToString();
                        al.Add(Obj_PBC);
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
                    throw;
                }

            }
            return al;
        }

        public static ICollection GetPatst_Authorized_notingroup_Call(string PatRegID, string FID, int branchid, int maindeptid, string subdept)
        {

            ArrayList al = new ArrayList();
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = null;
            if (subdept != "")
            {
                if (maindeptid == 0)
                    sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID  AND SDCode in (" + subdept + " ) and (PackageCode = '' ) and branchid=" + branchid + "  order by SDCode", conn);//and Patauthicante = 'Authorized'
                else
                    sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID    AND SDCode in (" + subdept + " ) and (PackageCode = '' ) and branchid=" + branchid + " and SDCode in (select SDCode from SubDepartment where DigModule=" + maindeptid + ") order by SDCode", conn); //and Patauthicante = 'Authorized'
            }
            else
            {
                if (maindeptid == 0)
                    sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID   and (PackageCode = '' OR PackageCode is null) and branchid=" + branchid + "  order by SDCode", conn); //
                else
                    sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID   and (PackageCode = '' OR PackageCode is null) and branchid=" + branchid + " and SDCode in (select SDCode from SubDepartment where DigModule=" + maindeptid + ") order by SDCode", conn); //and Patauthicante = 'Authorized'

            }
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {
                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();
                     
                        Obj_PBC.Reason = sdr["MTCode"].ToString();
                       // Obj_PBC.PPID = Convert.ToInt32(sdr["PPID"]);
                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
                     
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        Obj_PBC.PackageCode = sdr["PackageCode"].ToString();
                        Obj_PBC.Patauthicante = sdr["Patauthicante"].ToString();
                        al.Add(Obj_PBC);
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
                    throw;
                }

            }
            return al;
        }

        public static ICollection GetPatst_Authorized_notingroup(string PatRegID, string FID, int branchid,int maindeptid,string subdept)
        {
            
            ArrayList al = new ArrayList();
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;
            if (subdept != "")
            {
                if (maindeptid == 0)
                    sc = new SqlCommand("select * from Patmstd where  OutSideReport=0 and PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized'  AND SDCode in (" + subdept + " ) and (PackageCode = '' ) and branchid=" + branchid + "  order by SDCode", conn);//and Patauthicante = 'Authorized'
                else
                    sc = new SqlCommand("select * from Patmstd where  OutSideReport=0 and PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized'   AND SDCode in (" + subdept + " ) and (PackageCode = '' ) and branchid=" + branchid + " and SDCode in (select SDCode from SubDepartment where DigModule=" + maindeptid + ") order by SDCode", conn); //and Patauthicante = 'Authorized'
            }
            else
            {
                if (maindeptid == 0)
                    sc = new SqlCommand("select * from Patmstd where  OutSideReport=0 and PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized'  and (PackageCode = '' OR PackageCode is null) and branchid=" + branchid + "  order by SDCode", conn); //
                else
                    sc = new SqlCommand("select * from Patmstd where  OutSideReport=0 and PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized'  and (PackageCode = '' OR PackageCode is null) and branchid=" + branchid + " and SDCode in (select SDCode from SubDepartment where DigModule=" + maindeptid + ") order by SDCode", conn); //and Patauthicante = 'Authorized'

            }
                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
          
            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {                  
                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();
                    
                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
                      
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        Obj_PBC.PackageCode = sdr["PackageCode"].ToString();
                        Obj_PBC.Patauthicante = sdr["Patauthicante"].ToString();
                        al.Add(Obj_PBC);
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
                    throw;
                }
                
            }
            return al;
        }

        public static ICollection GetPatst_Authorized_notingroup_new(string PatRegID, string FID, int branchid)
        {
            
            ArrayList al = new ArrayList();

            SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
            SqlCommand sc = null;
            sc = new SqlCommand("select * from patmstd where PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized' and (PackageCode = '' OR PackageCode is null) and branchid=" + branchid + " order by SDCode", conn);
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
            
            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {                    
                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();
                      
                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
                      
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        Obj_PBC.PackageCode = sdr["PackageCode"].ToString();
                        al.Add(Obj_PBC);
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
                    throw;
                }
                
            }
            return al;
        }


        public static ICollection GetPatst_Authorized_not_ingroup(string PatRegID, string FID, int branchid)
        {
            
            ArrayList al = new ArrayList();
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;
            sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID and (Patauthicante = 'Authorized' or Patauthicante='Tested') and (PackageCode = '' OR PackageCode is null)  and branchid=" + branchid + " order by SDCode", conn);
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
           
            SqlDataReader sdr = null;
            try
            {
                conn.Open();
                sdr = sc.ExecuteReader();

                if (sdr != null)
                {                    
                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();
                        
                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
                     
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        Obj_PBC.PackageCode = sdr["PackageCode"].ToString();
                        al.Add(Obj_PBC);
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
                    throw;
                }
               
            }
            return al;
        }

        public static ICollection GetPatst_Authorized_ingroup_new(string PatRegID, string FID, int branchid, int maindeptid, string subdept)
        {

            ArrayList al = new ArrayList();

            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = null;

            if (subdept != "")
            {
                sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized'   AND SDCode in (" + subdept + " ) and (PackageCode <> '') and branchid=" + branchid + " and SDCode in (select SDCode from SubDepartment where DigModule=" + maindeptid + ") order by SDCode", conn);
            }
            else
            {
                sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized' and PackageCode <> ''   and branchid=" + branchid + " order by PackageCode", conn);

            }
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {

                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();
                      
                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
                        
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        Obj_PBC.PackageCode = sdr["PackageCode"].ToString();
                        al.Add(Obj_PBC);
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
                    throw;
                }

            }
            return al;
        }


        public static ICollection GetPatst_Authorized_ingroup(string PatRegID, string FID, int branchid)
        {
           
            ArrayList al = new ArrayList();

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;
            sc = new SqlCommand("select * from Patmstd where PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized' and PackageCode <> '' and PackageCode is not null  and branchid=" + branchid + " order by PackageCode", conn);
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
           
            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {
                    
                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();
                   
                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
                     
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        Obj_PBC.PackageCode = sdr["PackageCode"].ToString();
                        al.Add(Obj_PBC);
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
                    throw;
                }
                
            }
            return al;
        }
        public static ICollection Get_Code_Patst(int PatRegID, string FID, int branchid)
        {
            ArrayList al = new ArrayList();

            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = conn.CreateCommand();


            sc.CommandText = "select DISTINCT MTCode, SDCode from Patmstd where PatRegID =@PatRegID and FID =@FID and Patrepstatus = 1 and branchid=" + branchid + " order by SDCode";

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.Int)).Value = Convert.ToInt32(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

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
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID  = (sdr["PatRegID"].ToString());
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode  = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE =sdr["MTCode"].ToString();
                       
                        Obj_PBC.Patrepstatus=Convert.ToBoolean(sdr["Patrepstatus"]);
                       
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        al.Add(Obj_PBC);
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
       
        public static ICollection Get_Code_Patst(DateTime d1, int PatRegID, string FID, int branchid)
        {
            ArrayList al = new ArrayList();

            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = conn.CreateCommand();


            sc.CommandText = "select * from Patmstd where Patregdate=@Patregdate and PatRegID =@PatRegID and FID =@FID and Patrepstatus = '1' and branchid=" + branchid + " order by SDCode";

            sc.Parameters.Add(new SqlParameter("@Patregdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(d1);
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.Int)).Value = Convert.ToInt32(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

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
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = (sdr["PatRegID"].ToString());
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();
                       
                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
                    
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        al.Add(Obj_PBC);
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
        public static ICollection Get_Code_Patst(int PatRegID, string FID, string SDCode, string MTCode, int branchid)
        {
            ArrayList al = new ArrayList();

            SqlConnection conn = DataAccess.ConInitForDC(); 

            SqlCommand sc = conn.CreateCommand();


            sc.CommandText = "select * from Patmstd where SDCode =@SDCode and MTCode =@MTCode and PatRegID =@PatRegID and FID =@FID and Patrepstatus = 1  and branchid=" + branchid + "";

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.Int)).Value = Convert.ToInt32(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(SDCode);
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(MTCode);

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
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = (sdr["PatRegID"].ToString());
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();
                     
                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
                      
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);

                        al.Add(Obj_PBC);
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
        public static string getStatus_Testwise(string PatRegID, string FID, int branchid, string MTCode)
        {
            string statusStr = "", Patauthicante = "";
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = conn.CreateCommand();
            
            sc.CommandText = "select  Patauthicante from VW_Countstatus where PatRegID =@PatRegID and MTCode=@MTCode ";

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;

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
                        if (statusStr == "")
                        {
                            statusStr = sdr["Patauthicante"].ToString();
                        }
                        else
                        {
                            statusStr = statusStr + "," + sdr["Patauthicante"].ToString();
                        }
                    }
                }
            }
            catch
            {
                statusStr = "";
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
            if (statusStr.Length > 0)
            {
                string[] statusArr = statusStr.Split(',');

                string testArrStr = "";
                string authArrStr = "";
                string regArrStr = "";
                string PriArrStr = "";

                foreach (string str in statusArr)
                {

                    if (str == "Tested")
                    {
                        if (testArrStr == "")
                        {
                            testArrStr = str;
                        }
                        else
                        {
                            testArrStr = testArrStr + "," + str;
                        }
                    }
                    if (str == "Authorized")
                    {
                        if (authArrStr == "")
                        {
                            authArrStr = str;
                        }
                        else
                        {
                            authArrStr = authArrStr + "," + str;
                        }
                    }
                    if (str == "Registered")
                    {
                        if (regArrStr == "")
                        {
                            regArrStr = str;
                        }
                        else
                        {
                            regArrStr = regArrStr + "," + str;
                        }
                    }
                    if (str == "Printed")
                    {
                        if (PriArrStr == "")
                        {
                            PriArrStr = str;
                        }
                        else
                        {
                            PriArrStr = PriArrStr + "," + str;
                        }
                    }
                }
                string[] regArr;
                string[] testArr;
                string[] authArr;
                string[] PriArr;
                if (regArrStr != "")
                    regArr = regArrStr.Split(',');
                else
                    regArr = new string[0];
                if (testArrStr != "")
                    testArr = testArrStr.Split(',');
                else
                    testArr = new string[0];
                if (authArrStr != "")
                    authArr = authArrStr.Split(',');
                else
                    authArr = new string[0];
                if (PriArrStr != "")
                    PriArr = PriArrStr.Split(',');
                else
                    PriArr = new string[0];


                if (regArr.Length == statusArr.Length)
                {
                    Patauthicante = "Registered";
                    return Patauthicante;
                }
                if (testArr.Length == statusArr.Length)
                {
                    Patauthicante = "Tested";
                    return Patauthicante;
                }
                if (authArr.Length == statusArr.Length)
                {
                    Patauthicante = "Authorized";
                    return Patauthicante;
                }
                if (authArr.Length != statusArr.Length && authArr.Length != 0)
                {
                    Patauthicante = "Partial Authorized";
                    return Patauthicante;
                }
                if (testArr.Length != statusArr.Length && testArr.Length != 0)
                {
                    Patauthicante = "Partial Tested";
                    return Patauthicante;
                }
                if (PriArr.Length == statusArr.Length)
                {
                    Patauthicante = "Printed";
                    return Patauthicante;
                }

                if (PriArr.Length != statusArr.Length && PriArr.Length != 0)
                {
                    Patauthicante = "Partially Printed";
                    return Patauthicante;
                }

            }
            return Patauthicante;

        }  


        public static string getStatus(string PatRegID, string FID, int branchid)
        {
            string statusStr = "", Patauthicante = "";
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = conn.CreateCommand();

            sc.CommandText = "select  Patauthicante from VW_Countstatus where PatRegID =@PatRegID  ";

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
          
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
                        if (statusStr == "")
                        {
                            statusStr = sdr["Patauthicante"].ToString();
                        }
                        else
                        {
                            statusStr = statusStr + "," + sdr["Patauthicante"].ToString();
                        }
                    }
                }
            }
            catch
            {
                statusStr = "";
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
            if (statusStr.Length > 0)
            {
                string[] statusArr = statusStr.Split(',');

                string testArrStr = "";
                string authArrStr = "";
                string regArrStr = "";
                string PriArrStr = "";

                foreach (string str in statusArr)
                {

                    if (str == "Tested")
                    {
                        if (testArrStr == "")
                        {
                            testArrStr = str;
                        }
                        else
                        {
                            testArrStr = testArrStr + "," + str;
                        }
                    }
                    if (str == "Authorized")
                    {
                        if (authArrStr == "")
                        {
                            authArrStr = str;
                        }
                        else
                        {
                            authArrStr = authArrStr + "," + str;
                        }
                    }
                    if (str == "Registered")
                    {
                        if (regArrStr == "")
                        {
                            regArrStr = str;
                        }
                        else
                        {
                            regArrStr = regArrStr + "," + str;
                        }
                    }
                    if (str == "Printed")
                    {
                        if (PriArrStr == "")
                        {
                            PriArrStr = str;
                        }
                        else
                        {
                            PriArrStr = PriArrStr + "," + str;
                        }
                    }
                }
                string[] regArr;
                string[] testArr;
                string[] authArr;
                string[] PriArr;
                if (regArrStr != "")
                    regArr = regArrStr.Split(',');
                else
                    regArr = new string[0];
                if (testArrStr != "")
                    testArr = testArrStr.Split(',');
                else
                    testArr = new string[0];
                if (authArrStr != "")
                    authArr = authArrStr.Split(',');
                else
                    authArr = new string[0];
                if (PriArrStr != "")
                    PriArr = PriArrStr.Split(',');
                else
                    PriArr = new string[0];


                if (regArr.Length == statusArr.Length)
                {
                    Patauthicante = "Registered";
                    return Patauthicante;
                }
                if (testArr.Length == statusArr.Length)
                {
                    Patauthicante = "Tested";
                    return Patauthicante;
                }
                if (authArr.Length == statusArr.Length)
                {
                    Patauthicante = "Authorized";
                    return Patauthicante;
                }
                if (authArr.Length != statusArr.Length && authArr.Length != 0)
                {
                    Patauthicante = "Partial Authorized";
                    return Patauthicante;
                }
                if (testArr.Length != statusArr.Length && testArr.Length != 0)
                {
                    Patauthicante = "Partial Tested";
                    return Patauthicante;
                }
                if (PriArr.Length == statusArr.Length)
                {
                    Patauthicante = "Printed";
                    return Patauthicante;
                }
               
                if (PriArr.Length != statusArr.Length && PriArr.Length != 0)
                {
                    Patauthicante = "Partially Printed";
                    return Patauthicante;
                }
                
            }
            return Patauthicante;

        }  

              
        public static bool isRecordExists(string PatRegID, string FID, string SDCode, string MTCode, int branchid)
        {           
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                             " FROM Patmstd" +
                             " WHERE PatRegID=@PatRegID and FID=@FID and SDCode=@SDCode and MTCode=@MTCode and branchid=" + branchid + "", conn);

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
            sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(SDCode);
            sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = Convert.ToString(MTCode);

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

  
        public static int GetTotalCount(string PatRegID, string FID, int branchid)
        {
            try
            {
                int print=0;

                SqlConnection conn = DataAccess.ConInitForDC(); 

                SqlCommand sc = null;

                sc = new SqlCommand("SELECT count(*)" +
                             " FROM Patmstd " +
                             " WHERE PatRegID=@PatRegID and FID=@FID and branchid=" + branchid + "", conn);

                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

                try
                {
                    conn.Open();
                    object o = sc.ExecuteScalar();
                    if (o != DBNull.Value)
                        print = Convert.ToInt32(o);
                    else
                        print = 0;
                   
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


                return print;
            }
            catch { return 0; }
        }
 
        public static int GetPrintCount(string PatRegID, string FID, int branchid)
        {
            try
            {
                int print = 0;

                SqlConnection conn = DataAccess.ConInitForDC(); 

                SqlCommand sc = null;

                sc = new SqlCommand("SELECT count(*)" +
                             " FROM Patmstd " +
                             " WHERE PatRegID=@PatRegID and FID=@FID and Patrepstatus=1 and branchid=" + branchid + "", conn);

                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

                try
                {
                    conn.Open();
                    object o = sc.ExecuteScalar();
                    if (o != DBNull.Value)
                        print = Convert.ToInt32(o);
                    else
                        print = 0;
                   
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


                return print;
            }
            catch { return 0; }
        }

        public static int GetPrintStatuscount(string PatRegID, string FID, int branchid)
        {
                int cnt= 0;
                SqlConnection conn = DataAccess.ConInitForDC(); 
                SqlCommand sc = null;
                sc = new SqlCommand("SELECT count(*)" +
                             " FROM VW_pattestcntvw " +
                             " WHERE VW_pattestcntvw.PatRegID=@PatRegID and VW_pattestcntvw.FID=@FID and branchid=" + branchid + "", conn);

                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

                try
                {
                    conn.Open();
                    object o = sc.ExecuteScalar();
                    if (o != DBNull.Value)
                        cnt  = Convert.ToInt32 (o);
                    else
                        cnt = 0;
                   
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

            return cnt;
        }

        public static int GetAuthorizedcount(string PatRegID, string FID, int branchid)
        {
            int cnt = 0;
            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;

            sc = new SqlCommand("SELECT count(*)" +
                         " FROM VW_autvwpat " +
                         " WHERE VW_autvwpat.PatRegID=@PatRegID and VW_autvwpat.FID=@FID and branchid=" + branchid + "", conn);

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

            try
            {
                conn.Open();
                object o = sc.ExecuteScalar();
                if (o != DBNull.Value)
                    cnt = Convert.ToInt32(o);
                else
                    cnt = 0;
                
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

            return cnt;
        }

        public int GetRegisteredcount(string PatRegID, string FID, int branchid)
        {
            int cnt = 0;

            SqlConnection conn = DataAccess.ConInitForDC(); 
            SqlCommand sc = null;
            sc = new SqlCommand("SELECT count(*)" +
                         " FROM Patmstd" +
                         " WHERE PatRegID=@PatRegID and FID=@FID and Patauthicante='Registered' and branchid=" + branchid + "", conn);

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

            try
            {
                conn.Open();
                object o = sc.ExecuteScalar();
                if (o != DBNull.Value)
                    cnt = Convert.ToInt32(o);
                else
                    cnt = 0;
                
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

            return cnt;
        }
        internal class PatSt_new_Bal_CTableException : Exception
        {
            public PatSt_new_Bal_CTableException(string msg) : base(msg) { }
        }
  
        public static DataSet getPendingReport(int branchid, string fromdate, string todate)
        {
            SqlConnection conn = DataAccess.ConInitForDC();

            SqlDataAdapter sc = new SqlDataAdapter("SELECT DISTINCT patmst.PatRegID, RTRIM(patmst.intial) + ' ' + patmst.Patname AS PateintName, patmst.Drname, dbo.GetPendingReoprtTestNames(patmst.PatRegID, patmst.FID) AS testname, patmstd.Patrepstatus FROM  patmstd LEFT OUTER JOIN patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID WHERE  (dbo.patmstd.Patrepstatus <> 'True') AND (dbo.patmstd.Patauthicante <> 'Authorized') and dbo.patmstd.Phrecdate between '" + Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy") + "'and'" + Convert.ToDateTime(todate).ToString("MM/dd/yyyy") + "'", conn);
             DataSet ds = new DataSet();
            try
            {
                sc.Fill(ds);
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
                catch (Exception)
                {
                    throw new Exception("Record not found");
                }
            }
            return ds;


        }

        public static DataSet getPendingReport(int branchid, string fromdate, string todate,string labcode)
        {
            SqlConnection conn = DataAccess.ConInitForDC();

            string SqlQuery = "SELECT DISTINCT patmst.PatRegID, RTRIM(patmst.intial) + ' ' + patmst.Patname AS PateintName, patmst.Drname, dbo.GetPendingReoprtTestNames(patmst.PatRegID, patmst.FID) AS testname, patmstd.Patrepstatus FROM  patmstd LEFT OUTER JOIN patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID WHERE  (dbo.patmstd.Printstatus <> 'True') AND (dbo.patmstd.Patauthicante <> 'Authorized') and dbo.patmstd.Phrecdate between '" + Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy") + "'and'" + Convert.ToDateTime(todate).ToString("MM/dd/yyyy") + "'";
            if (labcode != "" && labcode != null)
            {
                SqlQuery = SqlQuery + " and dbo.patmst.UnitCode = '" + labcode + "' ";
            }             
            SqlDataAdapter sc = new SqlDataAdapter(  SqlQuery, conn); 
            DataSet ds = new DataSet();

            try
            {
                sc.Fill(ds);
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
                catch (Exception)
                {
                    throw new Exception("Record not found");
                }
            }
            return ds;


        }

        public static DataSet getPendingReport(int branchid, string fromdate, string todate, string labcode,string CenterCode)
        {
            SqlConnection conn = DataAccess.ConInitForDC();
            string SqlQuery = "SELECT DISTINCT patmst.PatRegID, RTRIM(patmst.intial) + ' ' + patmst.Patname AS PateintName, patmst.Drname, dbo.GetPendingReoprtTestNames(patmst.PatRegID, patmst.FID) AS testname, patmstd.Patrepstatus FROM  patmstd LEFT OUTER JOIN patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID WHERE  (dbo.patmstd.Patrepstatus <> 'True') AND (dbo.patmstd.Patauthicante <> 'Authorized') and dbo.patmst.Phrecdate between '" + Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy") + "'and'" + Convert.ToDateTime(todate).ToString("MM/dd/yyyy") + "'";
            if (labcode != "" && labcode != null)
            {
                SqlQuery = SqlQuery + " and dbo.patmst.UnitCode = '" + labcode + "' ";
            }
            if (CenterCode != "" && CenterCode != "All")
            {
                SqlQuery = SqlQuery + " and dbo.patmst.CenterCode= '" + CenterCode + "' ";
            }
            SqlDataAdapter sc = new SqlDataAdapter(SqlQuery, conn);
             DataSet ds = new DataSet();

            try
            {
                sc.Fill(ds);
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
                catch (Exception)
                {
                    throw new Exception("Record not found");
                }
            }
            return ds;


        }

        public static DataSet getAuthorisedReport(int branchid, string fromdate, string todate)
        {

            SqlConnection conn = DataAccess.ConInitForDC();
            SqlDataAdapter sc = new SqlDataAdapter("SELECT DISTINCT patmst.PatRegID, RTRIM(patmst.intial) + ' ' + patmst.Patname AS PateintName, patmst.Drname, dbo.GetPendingReoprtTestNames(patmst.PatRegID, patmst.FID) AS testname, patmstd.Patrepstatus FROM  patmstd LEFT OUTER JOIN patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID WHERE  (dbo.patmstd.Patauthicante = 'Authorized') and dbo.patmstd.TestedDate between '" + Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(todate).ToString("MM/dd/yyyy") + "'", conn);
                                                            
            DataSet ds = new DataSet();

            try
            {
                sc.Fill(ds);
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
                catch (Exception)
                {
                    throw new Exception("Record not found");
                }
            }
            return ds;


        }
        public static DataSet getAuthorisedReport(int branchid, string fromdate, string todate, string labcode)
        {

            SqlConnection conn = DataAccess.ConInitForDC();
            string Query = "SELECT DISTINCT patmst.PatRegID, RTRIM(patmst.intial) + ' ' + patmst.Patname AS PateintName, patmst.Drname, dbo.GetPendingReoprtTestNames(patmst.PatRegID, patmst.FID) AS testname, patmstd.Patrepstatus FROM  patmstd LEFT OUTER JOIN patmst ON patmstd.PID = patmst.PID AND patmstd.PatRegID = patmst.PatRegID WHERE  (dbo.patmstd.Patauthicante = 'Authorized') and dbo.patmstd.TestedDate between '" + Convert.ToDateTime(fromdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(todate).ToString("MM/dd/yyyy") + "'";
            if (labcode != "" && labcode != null)
            {
                Query = Query + " and dbo.patmst.UnitCode = '" + labcode + "' ";
            }
             SqlDataAdapter sc = new SqlDataAdapter(Query, conn);

            DataSet ds = new DataSet();

            try
            {
                sc.Fill(ds);
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
                catch (Exception)
                {
                    throw new Exception("Record not found");
                }
            }
            return ds;


        }
        public static string getStatus(string PatRegID, string FID, int branchid, int Maindept)
        {
            string statusStr = "", Patauthicante = "";

            SqlConnection conn = new SqlConnection(ConnectionString.Connectionstring);
            SqlCommand sc = conn.CreateCommand();

            sc.CommandText = "SELECT patmstd.Patauthicante FROM patmstd INNER JOIN " +
                         " SubDepartment ON patmstd.SDCode = SubDepartment.SDCode AND patmstd.branchid = SubDepartment.branchid where patmstd.PatRegID =@PatRegID and patmstd.FID =@FID and SubDepartment.MainDeptid=@maindept and  patmstd.branchid=" + branchid + "";

            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = FID;
            sc.Parameters.Add(new SqlParameter("@maindept", SqlDbType.NVarChar, 50)).Value = Maindept;

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
                        if (statusStr == "")
                        {
                            statusStr = sdr["Patauthicante"].ToString();
                        }
                        else
                        {
                            statusStr = statusStr + "," + sdr["Patauthicante"].ToString();
                        }
                    }
                }
            }
            catch
            {
                statusStr = "";
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
            if (statusStr.Length > 0)
            {
                string[] statusArr = statusStr.Split(',');

                string testArrStr = "";
                string authArrStr = "";
                string regArrStr = "";

                foreach (string str in statusArr)
                {

                    if (str == "Tested")
                    {
                        if (testArrStr == "")
                        {
                            testArrStr = str;
                        }
                        else
                        {
                            testArrStr = testArrStr + "," + str;
                        }
                    }
                    if (str == "Authorized")
                    {
                        if (authArrStr == "")
                        {
                            authArrStr = str;
                        }
                        else
                        {
                            authArrStr = authArrStr + "," + str;
                        }
                    }
                    if (str == "Registered")
                    {
                        if (regArrStr == "")
                        {
                            regArrStr = str;
                        }
                        else
                        {
                            regArrStr = regArrStr + "," + str;
                        }
                    }
                }
                string[] regArr;
                string[] testArr;
                string[] authArr;
                if (regArrStr != "")
                    regArr = regArrStr.Split(',');
                else
                    regArr = new string[0];
                if (testArrStr != "")
                    testArr = testArrStr.Split(',');
                else
                    testArr = new string[0];
                if (authArrStr != "")
                    authArr = authArrStr.Split(',');
                else
                    authArr = new string[0];


                if (regArr.Length == statusArr.Length)
                {
                    Patauthicante = "Registered";
                    return Patauthicante;
                }
                if (testArr.Length == statusArr.Length)
                {
                    Patauthicante = "Tested";
                    return Patauthicante;
                }
                if (authArr.Length == statusArr.Length)
                {
                    Patauthicante = "Authorized";
                    return Patauthicante;
                }
                if (authArr.Length != statusArr.Length && authArr.Length != 0)
                {
                    Patauthicante = "Partial Authorized";
                    return Patauthicante;
                }
                if (testArr.Length != statusArr.Length && testArr.Length != 0)
                {
                    Patauthicante = "Partial Tested";
                    return Patauthicante;
                }
            }
            return Patauthicante;

        }

        public static ICollection getPrintStatusTableByAuthorizedhemogram(string PatRegID, string FID, int branchid,string SDCode)
        {
          
            ArrayList al = new ArrayList();

            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = null;
            sc = new SqlCommand("select * from patmstd where PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized' and branchid=" + branchid + " and SDCode='" + SDCode + "' order by SDCode", conn);
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);
           
            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {
                    
                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();
                      
                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);
                       
                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);

                        al.Add(Obj_PBC);
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
                    throw;
                }
               
            }
            return al;
        }

        public static int GetStatusCountAuto(string PatRegID, string FID, int branchid)
        {
            try
            {
                int print = 0;

                SqlConnection conn = DataAccess.ConInitForDC();

                SqlCommand sc = null;

                sc = new SqlCommand("SELECT count(*)" +
                             " FROM Patmstd " +
                             " WHERE PatRegID=@PatRegID and FID=@FID and Patauthicante='Authorized' and branchid=" + branchid + "", conn);

                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

                try
                {
                    conn.Open();
                    object o = sc.ExecuteScalar();
                    if (o != DBNull.Value)
                        print = Convert.ToInt32(o);
                    else
                        print = 0;

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


                return print;
            }
            catch { return 0; }
        }

        public static int GetStatusCountTes(string PatRegID, string FID, int branchid)
        {
            try
            {
                int print = 0;

                SqlConnection conn = DataAccess.ConInitForDC();

                SqlCommand sc = null;

                sc = new SqlCommand("SELECT count(*)" +
                             " FROM Patmstd " +
                             " WHERE PatRegID=@PatRegID and FID=@FID and Patauthicante='Tested' and branchid=" + branchid + "", conn);

                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

                try
                {
                    conn.Open();
                    object o = sc.ExecuteScalar();
                    if (o != DBNull.Value)
                        print = Convert.ToInt32(o);
                    else
                        print = 0;

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


                return print;
            }
            catch { return 0; }
        }

        public static int GetStatusCountReg(string PatRegID, string FID, int branchid)
        {
            try
            {
                int print = 0;

                SqlConnection conn = DataAccess.ConInitForDC();

                SqlCommand sc = null;

                sc = new SqlCommand("SELECT count(*)" +
                             " FROM Patmstd " +
                             " WHERE PatRegID=@PatRegID and FID=@FID and Patauthicante='Registered' and branchid=" + branchid + "", conn);

                sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = PatRegID;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

                try
                {
                    conn.Open();
                    object o = sc.ExecuteScalar();
                    if (o != DBNull.Value)
                        print = Convert.ToInt32(o);
                    else
                        print = 0;

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


                return print;
            }
            catch { return 0; }
        }


        public static ICollection GetPatst_OutLabReport(string PatRegID, string FID, int branchid, int maindeptid, string subdept)
        {

            ArrayList al = new ArrayList();
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = null;
            if (subdept != "")
            {
                if (maindeptid == 0)
                    sc = new SqlCommand("select * from Patmstd where isnull(uploadOutsourceReport,'')<>'' and PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized'  AND SDCode in (" + subdept + " ) and (PackageCode = '' ) and branchid=" + branchid + "  order by SDCode", conn);//and Patauthicante = 'Authorized'
                else
                    sc = new SqlCommand("select * from Patmstd where isnull(uploadOutsourceReport,'')<>'' and PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized'   AND SDCode in (" + subdept + " ) and (PackageCode = '' ) and branchid=" + branchid + " and SDCode in (select SDCode from SubDepartment where DigModule=" + maindeptid + ") order by SDCode", conn); //and Patauthicante = 'Authorized'
            }
            else
            {
                if (maindeptid == 0)
                    sc = new SqlCommand("select * from Patmstd where isnull(uploadOutsourceReport,'')<>'' and PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized'  and (PackageCode = '' OR PackageCode is null) and branchid=" + branchid + "  order by SDCode", conn); //
                else
                    sc = new SqlCommand("select * from Patmstd where isnull(uploadOutsourceReport,'')<>'' and PatRegID =@PatRegID and FID =@FID and Patauthicante = 'Authorized'  and (PackageCode = '' OR PackageCode is null) and branchid=" + branchid + " and SDCode in (select SDCode from SubDepartment where DigModule=" + maindeptid + ") order by SDCode", conn); //and Patauthicante = 'Authorized'

            }
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {
                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();

                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);

                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);
                        Obj_PBC.PackageCode = sdr["PackageCode"].ToString();
                        Obj_PBC.Patauthicante = sdr["Patauthicante"].ToString();
                        Obj_PBC.Patauthicante = sdr["UploadOutSourceReport"].ToString();
                        al.Add(Obj_PBC);
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
                    throw;
                }

            }
            return al;
        }

        public static ICollection getPrintStatusTableByAuthorizedhemogram_without(string PatRegID, string FID, int branchid, string SDCode)
        {

            ArrayList al = new ArrayList();

            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = null;
            sc = new SqlCommand("select * from patmstd where PatRegID =@PatRegID and FID =@FID  and branchid=" + branchid + " and SDCode='" + SDCode + "' order by SDCode", conn);
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = Convert.ToString(PatRegID);
            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 50)).Value = Convert.ToString(FID);

            SqlDataReader sdr = null;
            try
            {
                conn.Open();

                sdr = sc.ExecuteReader();

                if (sdr != null)
                {

                    while (sdr.Read())
                    {
                        PatSt_Bal_C Obj_PBC = new PatSt_Bal_C();

                        Obj_PBC.PatRegID = sdr["PatRegID"].ToString();
                        Obj_PBC.FID = sdr["FID"].ToString();
                        Obj_PBC.SDCode = sdr["SDCode"].ToString();
                        Obj_PBC.MTCode = sdr["MTCode"].ToString();
                        Obj_PBC.STCODE = sdr["MTCode"].ToString();

                        Obj_PBC.Patrepstatus = Convert.ToBoolean(sdr["Patrepstatus"]);

                        Obj_PBC.Patregdate = Convert.ToDateTime(sdr["Patregdate"]);

                        al.Add(Obj_PBC);
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
                    throw;
                }

            }
            return al;
        }
}
