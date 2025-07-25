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


public class SendEmail_b
{
    private string member_id; public string P_member_id { get { return member_id; } set { member_id = value; } }
    private string email; public string P_email { get { return email; } set { email = value; } }
    private string username; public string P_username { get { return username; } set { username = value; } }
    private string cellno; public string P_cellno { get { return cellno; } set { cellno = value; } }
    private string Email1; public string P_Email1 { get { return Email1; } set { Email1 = value; } }

    public void EditFillFrom()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select Email from CTuser where username='" + P_username + "'", con);
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            sdr = sc.ExecuteReader();
            while (sdr.Read())
            {
                P_Email1 = sdr["Email"].ToString();
            }
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

    
}
