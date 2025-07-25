using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient; 

public partial class SqlBackup : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    dbconnection dc = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
               
            }
            catch (Exception exc)
            {
                if (exc.Message.Equals("Exception aborted."))
                {
                    return;
                }
                else
                {
                    Response.Cookies["error"].Value = exc.Message;
                    Server.Transfer("~/ErrorMessage.aspx");
                }
            }
            try
            {

            }
            catch (Exception exc)
            {
                if (exc.Message.Equals("Exception aborted."))
                {
                    return;
                }
                else
                {
                    Response.Cookies["error"].Value = exc.Message;
                    Server.Transfer("~/ErrorMessage.aspx");
                }
            }
        }
    }
 
    protected void btnSqlbackup_Click(object sender, EventArgs e)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand sqlcmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        //string backupDIR = "D:\\BackupDB";
        string backupDIR = Server.MapPath("~/BackupDB");
        if (!System.IO.Directory.Exists(backupDIR))
        {
            System.IO.Directory.CreateDirectory(backupDIR);
        }
        try
        {
            con.Open();
            //sqlcmd = new SqlCommand("backup database test to disk='" + backupDIR + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'", con);
            //sqlcmd.ExecuteNonQuery();
            //con.Close();
            //lblError.Text = "Backup database successfully";

            string ConnectionString = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
            sqlcmd = new SqlCommand("DECLARE @DataBaseName as nVarchar(60);SET @DataBaseName = (SELECT DB_NAME() AS DataBaseName); backup database @DataBaseName to disk='" + backupDIR + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'", con);
            sqlcmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            string AA = "SQL Backup save successfully!!!.";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);

        }
        catch (Exception ex)
        {
            // lblError.Text = "Error Occured During DB backup process !<br>" + ex.ToString();
        }
    }
}