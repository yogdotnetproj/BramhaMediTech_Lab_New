using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BAL
{
   public class ClsSubMenuMaster
    {
        #region Fields
        private clsDbDatabase objDBCon = null;
        private String submenuid;
        private String menuid;
        private String submenuname;
        private String subnevigateurl;
        
        #endregion

        #region Properties
        public string getsubmenuid
        {
            get
            {
                return submenuid;
            }
            set
            {
                submenuid = value;
            }
        }
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
        public string getsubmenuname
        {
            get
            {
                return submenuname;
            }
            set
            {
                submenuname = value;
            }
        }

        public string getsubnevigateurl
        {
            get
            {
                return subnevigateurl;
            }
            set
            {
                subnevigateurl = value;
            }
        }
        #endregion

        #region Constructor
        public ClsSubMenuMaster()
        {
            objDBCon = new clsDbDatabase();
            submenuid = "0";
            menuid = "0";
            submenuname = "";
            subnevigateurl = "";
            
        }
        #endregion

        #region Method
        public string returnconnectionstring()
        {
            return (objDBCon.connection());
        }
        public DataSet submenu(Int32 mode)
        {
            try
            {
                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SUBMENUMASTER";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@SubMenuID", getsubmenuid);
                cmd.Parameters.AddWithValue("@MenuID", getmenuid);
                cmd.Parameters.AddWithValue("@SubMenuName", getsubmenuname);
                cmd.Parameters.AddWithValue("@SubMenuNavigateURL", getsubnevigateurl);
                
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
        public DataTable submenuSp(Int32 mode)
        {
            try
            {

                objDBCon.OpenConnection();
                SqlCommand cmd = objDBCon.GetCommandObject;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_SUBMENUMASTER";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@SubMenuID", getsubmenuid);
                cmd.Parameters.AddWithValue("@MenuID", getmenuid);
                cmd.Parameters.AddWithValue("@SubMenuName", getsubmenuname);
                cmd.Parameters.AddWithValue("@SubMenuNavigateURL", getsubnevigateurl);

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
