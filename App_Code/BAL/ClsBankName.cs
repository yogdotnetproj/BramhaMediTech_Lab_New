using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
    public class ClsBankName
    {

        #region Fields
        private clsDbDatabase objDBCon = null;
        private String bankid;
        private String bankname;
        private String branchname;
        private String ifsccode;
        private String description;
        
        #endregion

        #region Properties
        public string getbankid
        {
            get
            {
                return bankid;
            }
            set
            {
                bankid = value;
            }
        }

        public string getbankname
        {
            get
            {
                return bankname;
            }
            set
            {
                bankname = value;
            }
        }

        public string getbranchname
        {
            get
            {
                return branchname;
            }
            set
            {
                branchname = value;
            }
        }

        public string getifsccode
        {
            get
            {
                return ifsccode;
            }
            set
            {
                ifsccode = value;
            }
        }

        public string getdescription
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

       

        #endregion

        #region Constructor
        public ClsBankName()
        {
            objDBCon = new clsDbDatabase();
            bankid = "0";
            bankname = "";
            branchname = "";
            ifsccode = "";
            description = "";
            
        }
        #endregion

        #region Method
        public string returnconnectionstring()
        {
            return (objDBCon.connection());
        }
        public DataSet bank(Int32 mode)
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BankMaster";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@BankId", getbankid);
                cmd.Parameters.AddWithValue("@BankName", getbankname);
                cmd.Parameters.AddWithValue("@BranchName", getbranchname);
                cmd.Parameters.AddWithValue("@IFSCCode", getifsccode);
                cmd.Parameters.AddWithValue("@Description", getdescription);
                cmd.Parameters.AddWithValue("@MODE", mode);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                DataSet ds = new DataSet();
                ds.Tables.Add(dt);
                return ds;
            }
            catch (SqlException ex)
            {

                return null;
            }
        }
        #endregion



        public DataTable bankTest(Int32 mode)
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BankMaster";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@BankId", getbankid);
                cmd.Parameters.AddWithValue("@BankName", getbankname);
                cmd.Parameters.AddWithValue("@BranchName", getbranchname);
                cmd.Parameters.AddWithValue("@IFSCCode", getifsccode);
                cmd.Parameters.AddWithValue("@Description", getdescription);
                cmd.Parameters.AddWithValue("@MODE", mode);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                //DataSet ds = new DataSet();
                //ds.Tables.Add(dt);
                return dt;
            }
            catch (SqlException ex)
            {

                return null;
            }
        }
    }
}
