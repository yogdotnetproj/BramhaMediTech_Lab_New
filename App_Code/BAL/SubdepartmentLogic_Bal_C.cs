using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public class SubdepartmentLogic_Bal_C
{
    public static ICollection getSubDepartment(int branchid, int DigModule)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "SP_phsubdp";
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        if (DigModule == 0)
            sc.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int)).Value = 0;
        else
            sc.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int)).Value = 1;
        sc.Parameters.Add(new SqlParameter("@DigModule", SqlDbType.Int)).Value = DigModule;
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Subdepartment_Bal_C SBC = new Subdepartment_Bal_C();
                    SBC.SubdeptName = sdr["subdeptName"].ToString();
                    SBC.SDCode = sdr["SDCode"].ToString();
                  
                  
                    SBC.Patregdate = Convert.ToDateTime(sdr["DateOfEntry"]);
                    if (sdr["SDOrderNo"] != DBNull.Value)
                        SBC.sDOrderNo = Convert.ToInt32(sdr["SDOrderNo"]);
                    SBC.Remark = sdr["Remark"].ToString();


                   

                    al.Add(SBC);
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
                throw;
            }
        }
        //al.Sort();
        return al;
    }

    public static ICollection getSubDepartment(int branchid, int p, int DigModule)
    {
        ArrayList al = new ArrayList();
       
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand();

        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "SP_phsubdp";
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        if (DigModule == 0)
            sc.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int)).Value = 0;
        else
            sc.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int)).Value = 1;
        sc.Parameters.Add(new SqlParameter("@DigModule", SqlDbType.Int)).Value = DigModule;
        // Add the employee ID parameter and set its value.

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
                    Subdepartment_Bal_C SBC = new Subdepartment_Bal_C();
                    SBC.SubdeptName = sdr["subdeptName"].ToString();
                    SBC.SDCode = sdr["SDCode"].ToString();

                    if (sdr["DateOfEntry"] != DBNull.Value)
                        SBC.Patregdate = Convert.ToDateTime(sdr["DateOfEntry"]);
                    else
                        SBC.Patregdate = DateTime.Now.Date;
                    if (sdr["SDOrderNo"] != DBNull.Value)
                        SBC.sDOrderNo = Convert.ToInt32(sdr["SDOrderNo"]);


                    SBC.Remark = sdr["Remark"].ToString();
                   
                        /****/
                        al.Add(SBC);
                }
                // The IEnumerable contains DataRowView objects.
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
        //al.Sort();
        return al;

    }
   
    public static ICollection GetSubdepartment_byCode(string SDCode, object branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT * from SubDepartment where SDCode=@SDCode and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar)).Value = SDCode;
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Subdepartment_Bal_C SBC = new Subdepartment_Bal_C();
                    SBC.SubdeptName = sdr["subdeptName"].ToString();
                    SBC.SDCode = sdr["SDCode"].ToString();
                    al.Add(SBC);
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
                throw;
            }

        }
        return al;
    }

    public static ICollection GetSubdepartment_bybranch(int branchid)
    {
        ArrayList al = new ArrayList();       

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * from SubDepartment where  branchid=" + branchid + " order by SDOrderNo", conn);
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        // Add the employee ID parameter and set its value.

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
                    Subdepartment_Bal_C SBC = new Subdepartment_Bal_C();
                    SBC.SubdeptName = sdr["subdeptName"].ToString();
                    SBC.SDCode = sdr["SDCode"].ToString();

                    al.Add(SBC);
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
        //al.Sort();
        return al;
    }

    public static ICollection GetsubdeptName_byDept(int iDeptID, int branchid)
    {
        ArrayList al = new ArrayList();

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT * from SubDepartment where DeptID=@DeptID and branchid=" + branchid + " order by SDOrderNo", conn);
        sc.Parameters.Add(new SqlParameter("@DeptID", SqlDbType.Int)).Value = iDeptID;
        // Add the employee ID parameter and set its value.

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
                    Subdepartment_Bal_C SBC = new Subdepartment_Bal_C();
                    SBC.SubdeptName = sdr["subdeptName"].ToString();
                    SBC.SDCode = sdr["SDCode"].ToString();

                    al.Add(SBC);
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
        //al.Sort();
        return al;
    }

    
    public static int MaxHeadOrder(object branchid, int DigModule)
    {
        
        int Fno = 0;
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("select isnull( max(SDOrderNo),0) from SubDepartment where branchid=" + branchid + " ", conn);//and DigModule=" + DigModule + "
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;        
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr.Read())
            {
                Fno = Convert.ToInt32(sdr.GetValue(0).ToString());
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return Fno;
    }

   

    public static void Update_Packageno(Subdepartment_Bal_C hnt, int iOrdNo, object branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        string s = "";
        
        if (hnt.sDOrderNo < iOrdNo)
            s = "Update SubDepartment Set SDOrderNo = SDOrderNo + 1 Where SDOrderNo>=@SDOrderNo and SDOrderNo <@SDOrderNoCurr and branchid=" + branchid + " ";
        else if (hnt.sDOrderNo > iOrdNo)
            s = "Update SubDepartment Set SDOrderNo = SDOrderNo - 1 Where SDOrderNo>@SDOrderNoCurr and SDOrderNo <=@SDOrderNo and branchid=" + branchid + "";
        SqlCommand sc = new SqlCommand(s, conn);
        sc.Parameters.Add(new SqlParameter("@SDOrderNo", SqlDbType.Int, 9)).Value = hnt.sDOrderNo;
        sc.Parameters.Add(new SqlParameter("@SDOrderNoCurr", SqlDbType.Int, 9)).Value = iOrdNo;
     
        try
        {
            conn.Open();
            if (s != "")
            {
                
                sc.ExecuteNonQuery();
            }
        }
        catch (Exception)
        {

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
    }//End Update 

   
    public static bool isSDCodeExists(string SDCode, object branchid)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM SubDepartment " +
                         " WHERE SDCode=@SDCode and branchid=" + branchid + " ", conn);
        sc.Parameters.Add(new SqlParameter("@SDCode", SqlDbType.NVarChar, 50)).Value = SDCode;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

        int SBC = 0;

        try
        {
            conn.Open();
            SBC = Convert.ToInt32(sc.ExecuteScalar());

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
        if (SBC != 0)
            return true;
        else
            return false;
    }

    internal class Subdepartment_Bal_CTableException : Exception
    {
        public Subdepartment_Bal_CTableException(string msg) : base(msg) { }
    }

  
    public static string GET_SDCode(string H_Name, object branchid)
    {
       
        string hcd;
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand(" SELECT SDCode" +
                         " FROM SubDepartment " +
                         " WHERE subdeptName=@subdeptName AND branchid=" + branchid + "", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@subdeptName", SqlDbType.NVarChar, 100)).Value = H_Name;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;

        try
        {
            conn.Open();
            hcd = Convert.ToString(sc.ExecuteScalar());

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

        return hcd;
    }


    public static void updatewhenhsort(string MTCode, int Testordno, int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("update SubDepartment set SDOrderNo=" + Testordno + " where SDCode='" + MTCode + "' and branchid=" + branchid + "", con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
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
    }

    public static ICollection GET_Hemetology_Test(string PatRegID, string FID, string SDCode) 
    {
        
        ArrayList al = new ArrayList();
       
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("SELECT dbo.patmst.PatRegID,dbo.patmst.FID," +
         "dbo.patmst.branchid,dbo.patmstd.MTCode,dbo.MainTest.Maintestname," +
         "dbo.MainTest.SDCode FROM   dbo.patmst INNER JOIN dbo.patmstd " +
         "ON dbo.patmst.PID = dbo.patmstd.PID INNER JOIN  dbo.MainTest ON " +
         "dbo.patmstd.MTCode = dbo.MainTest.MTCode " +
         "where dbo.patmst.PatRegID='" + PatRegID + "' and dbo.patmst.FID='" + FID + "'and dbo.MainTest.SDCode='" + SDCode + "'", conn);
        
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
                    Subdepartment_Bal_C tbw = new Subdepartment_Bal_C();

                    tbw.testname = sdr["Maintestname"].ToString();
                    tbw.SDCode = sdr["SDCode"].ToString();
                    tbw.MTCode = sdr["MTCode"].ToString();
                    al.Add(tbw);

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

    public static ICollection GET_subdeptName_bycode(string SDCode, int branchid)
    {
        ArrayList al = new ArrayList();

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand(" SELECT * from SubDepartment where branchid=" + branchid + " and SDCode='" + SDCode + "'", conn);

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null)
            {
                if (sdr.Read() != null)
                {
                    Subdepartment_Bal_C SBC = new Subdepartment_Bal_C();
                    SBC.SubdeptName = sdr["subdeptName"].ToString();
                    SBC.SDCode = sdr["SDCode"].ToString();

                    al.Add(SBC);
                }
            }
        }
        catch (Exception ex)
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
                // Log an event in the Application Event Log.
                throw;
            }
        }
        //al.Sort();
        return al;
    }//End 


    public static int MaxParameterOrder(object branchid, int DigModule)
    {

        int Fno = 0;
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("select isnull( max(TestID),0) from SubTest where branchid=" + branchid + " ", conn);//and DigModule=" + DigModule + "
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 4)).Value = branchid;
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr.Read())
            {
                Fno = Convert.ToInt32(sdr.GetValue(0).ToString());
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        return Fno;
    }

}
