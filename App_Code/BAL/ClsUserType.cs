using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
    public class ClsUserType
    {
       
       #region Fields
        private clsDbDatabase objDBCon = null;
        private String userid;
        private String usertype;
        private String description;
        #endregion

        #region Properties

        public string getuserid
        {
            get
            {
                return userid;
            }
            set
            {
                userid = value;
            }
        }
        public string getusertype
        {
            get
            {
                return usertype;
            }
            set
            {
                usertype = value;
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
        public ClsUserType()
        {
            objDBCon = new clsDbDatabase();
            userid = "0";
            usertype = "";
            description = "";
            
        }
        #endregion

        #region Method
        public string returnconnectionstring()
        {
            return (objDBCon.connection());
        }
        public DataSet usertypeSP(Int32 mode)
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_USERTYPE";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@USERID", getuserid);
                cmd.Parameters.AddWithValue("@USERTYPE", getusertype);
                cmd.Parameters.AddWithValue("@DESCRIPTION", getdescription);
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
        public DataTable usertypetest(Int32 mode)
        {
            try
            {

                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_USERTYPE";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@USERID", getuserid);
                cmd.Parameters.AddWithValue("@USERTYPE", getusertype);
                cmd.Parameters.AddWithValue("@DESCRIPTION", getdescription);
                cmd.Parameters.AddWithValue("@MODE", mode);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                return dt;

            }
            catch (SqlException ex)
            {

                return null;
            }
            
        }
        }
    }
