using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
   public class ClsMENUMASTER
    {
        #region Fields
        private clsDbDatabase objDBCon = null;
        private String menuid;
        private String menuname;
        private String nevigateurl;
        
        #endregion

        #region Properties

        public string getmenuid
        {
            get
            {
                return menuid;
            }
            set
            {
                menuid = value;
            }
        }
        public string getmenuname
        {
            get
            {
                return menuname;
            }
            set
            {
                menuname = value;
            }
        }

        public string getnevigateurl
        {
            get
            {
                return nevigateurl;
            }
            set
            {
                nevigateurl = value;
            }
        }
        #endregion

        #region Constructor
        public ClsMENUMASTER()
        {
            objDBCon = new clsDbDatabase();
            menuid = "0";
            menuname = "";
            nevigateurl = "";
            
        }
        #endregion

        #region Method
        public string returnconnectionstring()
        {
            return (objDBCon.connection());
        }
        public DataSet menumaster(Int32 mode)
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_MENUMASTER";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@MenuID", getmenuid);
                cmd.Parameters.AddWithValue("@MenuName", getmenuname);
                cmd.Parameters.AddWithValue("@NavigateURL", getnevigateurl);
                
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
            //finally
            //{
            //    objDBCon.closeConnection();
            //}
        }

        #endregion
        public DataTable menumasterSP(Int32 mode)
        {
            try
            {

                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_MENUMASTER";
                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@MenuID", getmenuid);
                cmd.Parameters.AddWithValue("@MenuName", getmenuname);
                cmd.Parameters.AddWithValue("@NavigateURL", getnevigateurl);
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
            //finally
            //{
            //    objDBCon.closeConnection();
            //}
        }


      
    }
}
