using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
   public class clsDbDatabase
    {
        SqlConnection con;
        SqlCommand cmd;
        string connectionstring;

        public DataSet ds = new DataSet();
        public DataTable dt = new DataTable();
        public clsDbDatabase()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
           // con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString);
           // connectionstring = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        }

        public void OpenConnection()
        {

            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

            }
            catch (SqlException ex)
            {


            }

        }//end of method

        public void closeConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
                con.Dispose();
            }
        }//end of method

        public SqlCommand GetCommandObject
        {
            get
            {
                if (cmd != null)
                    return cmd;
                else
                {
                    cmd = new SqlCommand();
                    cmd.Connection = con;
                    return cmd;
                }
            }

        }

        public string connection()
        {
            return connectionstring;
        }

        //public void databasebackup()
        //{
        //    try
        //    {
        //        string sqltxt = "";
        //        sqltxt = @"BACKUP DATABASE [DBFINALINVETORY] TO  DISK = N'C:\TESTDATABASE.bak' WITH NOFORMAT, NOINIT,  NAME = N'DBFINALLAXMIINVETORY-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10 ";
        //        SqlCommand sqlCm = new SqlCommand(sqltxt, con);
        //        con.Open();
        //        sqlCm.ExecuteNonQuery();
        //        con.Close();
        //    }
        //    catch
        //    {

        //    }

        //}

        //public void datarestore()
        //{
        //    string sqltxt = "";
        //    sqltxt = @"RESTORE DATABASE [DBFINALINVETORY] FROM  DISK = N'C:\TESTDATABASE.bak' WITH  FILE = 1,  NOUNLOAD,  STATS = 10";
        //    SqlCommand sqlCm = new SqlCommand(sqltxt, con);
        //    con.Open();
        //    sqlCm.ExecuteNonQuery();
        //    con.Close();

        //}

    }
}
