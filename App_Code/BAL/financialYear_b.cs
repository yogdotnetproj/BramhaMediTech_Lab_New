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
/// <summary>
/// Summary description for financialYear_b
/// </summary>
public class financialYear_b
{

    private string FinancialYearId; public string P_FinancialYearId { get { return FinancialYearId; } set { FinancialYearId = value; } }
    private DateTime StartDate; public DateTime P_StartDate { get { return StartDate; } set { StartDate = value; } }    
    private DateTime EndDate; public DateTime P_EndDate { get { return EndDate; } set { EndDate = value; } }
    

    public DataSet FillGrid(int branchid)
    {

        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter da = new SqlDataAdapter("select FinancialYearId,CONVERT(char(12), StartDate,105) as StartDate,CONVERT(char(12), EndDate,105) as EndDate from FIYR where branchid=" + branchid + "", con);
        DataSet ds = new DataSet();
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
            con.Close();
        }
        return ds;
    }

    public void FinacialInsert(int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Insert into FIYR (StartDate,EndDate,branchid) values(@StartDate,@EndDate,@branchid)", con);
        sc.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime)).Value = P_StartDate;
        sc.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime)).Value = P_EndDate;       
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;

        
        try
        {
            con.Open();
            sc.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }

    public void EditFill(int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select * from FIYR where FinancialYearId=" + P_FinancialYearId + "  and branchid=" + branchid + "", con);
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                P_StartDate =Convert.ToDateTime(sdr["StartDate"]);
                P_EndDate =Convert.ToDateTime(sdr["EndDate"]);
            }
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }
    public void FinacialEdit(int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("update FIYR set StartDate=@StartDate,EndDate=@EndDate where FinancialYearId=" + P_FinancialYearId + "  and branchid=" + branchid + "", con);
        sc.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime)).Value = P_StartDate;
        sc.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.DateTime)).Value = P_EndDate;
         
        try
        {
            con.Open();
            sc.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close(); con.Dispose();
        }
    }
    public bool isfinyrpresent(int branchid)
    {
        P_FinancialYearId = "0";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("select FinancialYearId from FIYR where branchid=" + branchid + " and getdate() between StartDate and EndDate", con);
        SqlDataReader sdr = null;

        try
        {
            con.Open();
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                P_FinancialYearId = sdr["FinancialYearId"].ToString();
            }
            
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        if (P_FinancialYearId == "0")
            return false;
        else
            return true;
    }

    public void GetMaxId(int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select max(FinancialYearId) from FIYR where branchid=" + branchid + "", con);
        con.Open();
        try
        {
            P_FinancialYearId =(string)sc.ExecuteScalar();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }
    
}
