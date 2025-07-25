using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
    public class ClsAddUser
    {

        #region Fields
        private clsDbDatabase objDBCon = null;
        private String empid;
        private String empname;
        private String empmidname;
        private String emplastname;
        private String initial;
        private String username;
        private String password;
        private String dob;
        private String gender;
        private String address;
        private String country;
        private String state;
        private String city;
        private String mobileno;
        private String telno;
        private String emailid;
        private String zipcode;
        private String branchid;
        private String status;
        private int CountryId;
        #endregion

        #region Properties
        public string getempid
        {
            get
            {
                return empid;
            }
            set
            {
                empid = value;
            }
        }

        public string getempname
        {
            get
            {
                return empname;
            }
            set
            {
                empname = value;
            }
        }

        public string getempmidname
        {
            get
            {
                return empmidname;
            }
            set
            {
                empmidname = value;
            }
        }

        public string getemplastname
        {
            get
            {
                return emplastname;
            }
            set
            {
                emplastname = value;
            }
        }

        public string getinitial
        {
            get
            {
                return initial;
            }
            set
            {
                initial = value;
            }
        }

        public string getusername
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string getpassword
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public string getdob
        {
            get
            {
                return dob;
            }
            set
            {
                dob = value;
            }
        }
        public string getgender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
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
        public string getcountry
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
        public string getstate
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        public string getcity
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
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
        public string getzipcode
        {
            get
            {
                return zipcode;
            }
            set
            {
                zipcode = value;
            }
        }
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
        public string getstatus
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public int getCountryId
        {
            get
            {
                return CountryId;
            }
            set
            {
                CountryId = value;
            }
        }

        #endregion

        #region Constructor
        public ClsAddUser()
        {
            objDBCon = new clsDbDatabase();
            empid = "";
            empname = "";
            empmidname = "";
            emplastname = "";
            initial = "";
            username = "";
            password = "";
            dob = "";
            gender = "";
            address = "";
            country = "";
            state = "";
            city = "";
            mobileno = "0";
            telno = "0";
            emailid = "";
            zipcode = "0";
            branchid = "0";
            status = "Active";
        }
        #endregion

        #region Method
        public string returnconnectionstring()
        {
            return (objDBCon.connection());
        }
        public DataSet user(Int32 mode)
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_phusermst";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@SZEMPID", getempid);
                cmd.Parameters.AddWithValue("@SZEMPNAME", getempname);
                cmd.Parameters.AddWithValue("@SZEMPMIDNAME", getempmidname);
                cmd.Parameters.AddWithValue("@SZLASTNAME", getemplastname);
                cmd.Parameters.AddWithValue("@SZINITIAL", getinitial);
                cmd.Parameters.AddWithValue("@SZUSERNAME", getusername);
                cmd.Parameters.AddWithValue("@SZPASSWORD", getpassword);
                cmd.Parameters.AddWithValue("@DTPDOB", getdob);
                cmd.Parameters.AddWithValue("@SZGENDER", getgender);
                cmd.Parameters.AddWithValue("@SZADDRESS", getaddress);
                cmd.Parameters.AddWithValue("@SZCOUNTRY", getcountry);
                cmd.Parameters.AddWithValue("@SZSTATE", getstate);
                cmd.Parameters.AddWithValue("@SZCITY", getcity);
                cmd.Parameters.AddWithValue("@NUMMOBILENO", getmobileno);
                cmd.Parameters.AddWithValue("@NUMTELNO", gettelno);
                cmd.Parameters.AddWithValue("@SZEMAILID", getemailid);
                cmd.Parameters.AddWithValue("@NUMZIPCODE", getzipcode);
                cmd.Parameters.AddWithValue("@NUMBRANCHID", getbranchid);
                cmd.Parameters.AddWithValue("@SZSTATUS", getstatus);
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


        public DataTable BindCountry()
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BindCountry";
                cmd.Parameters.Clear();
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                return dt;
            }
            catch (SqlException ex)
            {

                return null;
            }
        }
        public DataTable BindStateWithCountry()
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BindStateWithCountry";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@NUMCOUNTRYID", getCountryId);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());

                return dt;
            }
            catch (SqlException ex)
            {

                return null;
            }
        }

        public DataTable EditUserDetails(Int32 mode)
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_phusermst";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@SZEMPID", getempid);
                cmd.Parameters.AddWithValue("@SZEMPNAME", getempname);
                cmd.Parameters.AddWithValue("@SZEMPMIDNAME", getempmidname);
                cmd.Parameters.AddWithValue("@SZLASTNAME", getemplastname);
                cmd.Parameters.AddWithValue("@SZINITIAL", getinitial);
                cmd.Parameters.AddWithValue("@SZUSERNAME", getusername);
                cmd.Parameters.AddWithValue("@SZPASSWORD", getpassword);
                cmd.Parameters.AddWithValue("@DTPDOB", getdob);
                cmd.Parameters.AddWithValue("@SZGENDER", getgender);
                cmd.Parameters.AddWithValue("@SZADDRESS", getaddress);
                cmd.Parameters.AddWithValue("@SZCOUNTRY", getcountry);
                cmd.Parameters.AddWithValue("@SZSTATE", getstate);
                cmd.Parameters.AddWithValue("@SZCITY", getcity);
                cmd.Parameters.AddWithValue("@NUMMOBILENO", getmobileno);
                cmd.Parameters.AddWithValue("@NUMTELNO", gettelno);
                cmd.Parameters.AddWithValue("@SZEMAILID", getemailid);
                cmd.Parameters.AddWithValue("@NUMZIPCODE", getzipcode);
                cmd.Parameters.AddWithValue("@NUMBRANCHID", getbranchid);
                cmd.Parameters.AddWithValue("@SZSTATUS", getstatus);
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
        #endregion
    }
}
