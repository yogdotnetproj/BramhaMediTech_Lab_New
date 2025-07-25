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
public class Materializedview_C
{
    DataAccess data = new DataAccess();
	public Materializedview_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataSet GetMaterializeviewdata(object tdate, object fdate)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT convert(char(12),Fiscal_Year,105) as BillDate,Bill_No, Customer_Name, Customer_PanNumber, Bill_Date, Amount, Discount, Taxable_Amount, Tax_Amount, case when Is_Printed =0 then'No' else 'Yes'end as Is_Printed, case when Is_Active =0 then'No' else 'Yes'end as Is_Active,  Printed_Time, Entered_By, Printed_By " +
        " FROM  tbl_MaterializeView  " +
        " where tbl_MaterializeView.Bill_Date between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' ";

      
        da = new SqlDataAdapter(query, conn);
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
            conn.Close(); conn.Dispose();
        }
        return ds;
    }

}