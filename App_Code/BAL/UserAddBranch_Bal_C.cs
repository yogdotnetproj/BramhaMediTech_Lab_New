using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
    public class UserAddBranch_Bal_C
    {
        #region Fields
        private clsDbDatabase objDBCon = null;
        private String branchid;
        private String branchname;
        private String branchcode;
        private String address;
        private String telno;
        private String mobileno;
        private String emailid;
        #endregion

        #region Properties
        public string getbranchid
        {
            get
            {
                return branchid;
            }
            set
            {
                branchid = value;
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

        public string getbranchcode
        {
            get
            {
                return branchcode;
            }
            set
            {
                branchcode = value;
            }
        }

        public string getaddress
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }

        public string gettelno
        {
            get
            {
                return telno;
            }
            set
            {
                telno = value;
            }
        }

        public string getmobileno
        {
            get
            {
                return mobileno;
            }
            set
            {
                mobileno = value;
            }
        }

        public string getemailid
        {
            get
            {
                return emailid;
            }
            set
            {
                emailid = value;
            }
        }

        #endregion

        #region Constructor
        public UserAddBranch_Bal_C()
        {
            objDBCon = new clsDbDatabase();
            branchid = "0";
            branchname = "";
            branchcode = "0";
            address = "";
            telno = "0";
            mobileno = "0";
            emailid = "";
        }
        #endregion

        #region Method
        public string returnconnectionstring()
        {
            return (objDBCon.connection());
        }
        public DataSet branch(Int32 mode)
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BranchMaster";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@NUMBRANCHID", getbranchid);
                cmd.Parameters.AddWithValue("@SZBRANCHNAME", getbranchname);
                cmd.Parameters.AddWithValue("@NUMBRANCHCODE", getbranchcode);
                cmd.Parameters.AddWithValue("@SZADDRESS", getaddress);
                cmd.Parameters.AddWithValue("@NUMTELNO", gettelno);
                cmd.Parameters.AddWithValue("@NUMMOBILENO", getmobileno);
                cmd.Parameters.AddWithValue("@SZEMAILID", getemailid);
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


        public DataTable branchTest(Int32 mode)
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BranchMaster";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@NUMBRANCHID", getbranchid);
                cmd.Parameters.AddWithValue("@SZBRANCHNAME", getbranchname);
                cmd.Parameters.AddWithValue("@NUMBRANCHCODE", getbranchcode);
                cmd.Parameters.AddWithValue("@SZADDRESS", getaddress);
                cmd.Parameters.AddWithValue("@NUMTELNO", gettelno);
                cmd.Parameters.AddWithValue("@NUMMOBILENO", getmobileno);
                cmd.Parameters.AddWithValue("@SZEMAILID", getemailid);
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
