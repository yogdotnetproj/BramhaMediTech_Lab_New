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
public class DeptsaleandIncome_Bal_C
{
	public DeptsaleandIncome_Bal_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable GetDeptsalereport(object tdate, object fdate,  int branchid, int subdeptid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        //string query = "SELECT      patmstd.FID, patmstd.SDCode, " +
        //               " convert(char(12),patmstd.Patregdate,105) as Patregdate  , " +
        //               " SUM(patmstd.TestRate) AS TestRate, SubDepartment.subdeptName  " +
        //          "  FROM         patmstd INNER JOIN " +
        //          "  SubDepartment ON patmstd.SDCode = SubDepartment.SDCode AND patmstd.Branchid = SubDepartment.Branchid " +
        //          " where    (CAST(CAST(YEAR( patmstd.Patregdate) AS varchar(4)) + '/' + CAST(MONTH( patmstd.Patregdate) AS varchar(2)) + '/' + CAST(DAY( patmstd.Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "') and  patmstd.FID<>'' and patmstd.branchid=" + branchid + " ";    
        //            if (subdeptid != 0)
        //            {
        //          query += " and SubDepartment.subdeptid=" + subdeptid + "";
        //             }
        //            query += " GROUP BY patmstd.FID, patmstd.SDCode, " +
        //            "    convert(char(12),patmstd.Patregdate,105) , "+
        //            "    SubDepartment.subdeptName,    patmstd.Branchid   ";

                    string query = "SELECT DISTINCT "+
                   " SubDepartment.subdeptName, SubDepartment.SDCode, SubDepartment.SDOrderNo, SubDepartment.DeptID, VW_DepartmentTestcount.Patregdate, " +
                   " ISNULL(VW_DepartmentTestcount.DepCount, 0) AS DepCount, ISNULL(VW_DepartmentTestcount.TestRate, 0) AS TestRate, "+
                   " isnull(VW_DepartmentTestcount.FID,0)as FID "+
                   " FROM            SubDepartment LEFT OUTER JOIN "+
                   " VW_DepartmentTestcount ON SubDepartment.SDCode = VW_DepartmentTestcount.SDCode ";
            
                    if (subdeptid != 0)
                    {
                        query += " WHERE SubDepartment.subdeptid=" + subdeptid + "";
                    }
                    
              
      
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


    public static DataTable Get_Allsubdept(int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select DISTINCT subdeptname,subdeptid from SubDepartment where  branchid =" + branchid + "";

      

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
  
    
  
 
}