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
using System.Collections.Generic;


public class TestAssignLog_Bal_C
{
	public TestAssignLog_Bal_C()
	{
		
	}    

    public static string Get_Maintest_Code(string MTCode, int branchid)
    {
        string Maintestname = "";
        
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;
        sc = new SqlCommand("select Maintestname from MainTest where MTCode=@MTCode and branchid=" + branchid + "", conn);

        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 50)).Value = MTCode;
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null)
            {
                while (sdr.Read())
                {                    
                    Maintestname = sdr["Maintestname"].ToString(); 
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
        return Maintestname;
    }
   
}
