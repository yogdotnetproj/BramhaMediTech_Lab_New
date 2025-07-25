using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public class TreeviewBind_C
{
	public TreeviewBind_C()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public List<string> FillAllInsurance(string prefixtext)
    {

        SqlConnection con = DataAccess.ConInitForHM();
        con.Open();
        SqlCommand cmd = new SqlCommand("SP_FillAllInsurance_corporate", con);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cmd.Parameters.AddWithValue("@Search", prefixtext);
            List<string> PatientSubCategory = new List<string>();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                PatientSubCategory.Add(sdr["PatientSubCategory"].ToString());

            }
            return PatientSubCategory;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }

    }

    public DataTable GetSummaryExcel_Report()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        con.Open();
        SqlDataAdapter da = new SqlDataAdapter("sp_Summ_result", con);
        DataTable ds = new DataTable();

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
            con.Close();
            con.Dispose();
        }
        return ds;
    }
    public DataSet FillFY()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter da = new SqlDataAdapter("select FinancialYearId,CONVERT(char(12), StartDate,105) +'To '+CONVERT(char(12), EndDate,105) as FYDate from FIYR order by FinancialYearId desc", con);
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
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public DataTable Get_AuthorizeDoctorCount()
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_GetAuthorizeDoctorCount", conn);
        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            conn.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        finally
        {

            conn.Close(); conn.Dispose();

        }

    }

    public DataTable Get_TestwiseTotalDr_Perform_Amount(string TestName, int Test, string DrName)
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_TestwiseTotalDr_Perform", conn);
        sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 50)).Value = TestName;
        sc.Parameters.Add(new SqlParameter("@Test", SqlDbType.Int)).Value = Test;
        sc.Parameters.Add(new SqlParameter("@DrName", SqlDbType.NVarChar, 250)).Value = DrName;
        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            conn.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        finally
        {

            conn.Close(); conn.Dispose();

        }

    }
    public DataTable Get_TestwiseTotalCount(string TestName, int Test)
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_TestwiseTotalCount", conn);
        sc.Parameters.Add(new SqlParameter("@TestName", SqlDbType.NVarChar, 50)).Value = TestName;
        sc.Parameters.Add(new SqlParameter("@Test", SqlDbType.Int)).Value = Test;
        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            conn.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        finally
        {

            conn.Close(); conn.Dispose();

        }

    }
    public  DataTable Get_TotalPatientCount(object tdate, object fdate)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select COUNT(srno) as PatientCount from patmst " +
                  " where    (CAST(CAST(YEAR( patmst.Patregdate) AS varchar(4)) + '/' + CAST(MONTH( patmst.Patregdate) AS varchar(2)) + '/' + CAST(DAY( patmst.Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "')  ";



        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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

    public DataTable Get_TotalSalesummary(object tdate, object fdate)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT    sum( NetPayment)as amount,sum( AmtPaid)as AmtReceived, " +
           " sum(CAST(Discount AS float))as Discount, " +
           " sum(NetPayment) - (sum(CAST(Discount AS float))) AS Taxable,Round( sum(TaxAmount),2) AS Tax, " +
           " Round( sum(NetPayment) - ((sum(CAST(Discount AS float))) + sum(AmtPaid)),0) AS Balance   from Cshmst" +
        //" where  IsActive=1 and Cshmst.BillDate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and Cshmst.branchid=" + branchid + "";
        " where    IsActive=1 and (CAST(CAST(YEAR( Cshmst.RecDate) AS varchar(4)) + '/' + CAST(MONTH(Cshmst.RecDate) AS varchar(2)) + '/' + CAST(DAY(Cshmst.RecDate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "')  ";



        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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

    public DataTable Get_DefaultDoctor()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT rtrim(DrInitial)+' '+DoctorName +'='+ DoctorCode as name from  DrMT where DrType='DR' and DefaultDoctor=1  ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }
    public DataTable Bind_TestParameter(string prefixText,string MTCode,int  branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select STCODE as TestCode,TestName from SubTest where   MTCode='" + Convert.ToString(MTCode) + "'  and branchid=" + Convert.ToInt32(branchid) + " and  STCODE <>'H'", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }


    public DataTable BindMainMenu(string USERNAME, string Password)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter();
        if (USERNAME == "Admin")
        {
             sda = new SqlDataAdapter("select MenuID , MenuName   from TBL_MenuMAster ", con);
        }
        else
        {            
             sda = new SqlDataAdapter("SELECT DISTINCT    " +
                      "     TBL_MenuMaster.MenuID ,TBL_MenuMaster.MenuName  " +
                      "  FROM         Roleright INNER JOIN      " +
                      "  TBL_SubMenuMaster ON Roleright.FormId = TBL_SubMenuMaster.SubMenuID INNER JOIN  " +
                      "  TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID INNER JOIN   " +
                      "  CTuser ON Roleright.Usertypeid = CTuser.RollId      " +
                      "  WHERE     (CTuser.USERNAME = '" + USERNAME + "') AND (CTuser.password = '" + Password + "') and  TBL_SubMenuMaster.Isvisable=1  " +
                      "  order by MenuID   ", con);
        }
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }
    public DataTable Bind_MachinName()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select Instumentname from InstumentMaster ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public DataTable BindMainMenu_treeview()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select MenuID , MenuName   from TBL_MenuMAster ", con);
        
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public DataTable BindChildMenu1(string  Menuid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select SubMenuID as MenuID,SubMenuNavigateURL as MenuName from TBL_subMenuMAster where MenuID='" + Menuid + "' ", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
        return ds;
    }

    public DataTable BindChildMenu(string Menuid, string USERNAME, string Password)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter();
        if (USERNAME == "Admin")
        {
            sda = new SqlDataAdapter("select SubMenuID as MenuID,SubMenuName as MenuName from TBL_subMenuMAster where MenuID='" + Menuid + "'  and  TBL_SubMenuMaster.Isvisable=1 ", con);
        }
        else
        {           
             sda = new SqlDataAdapter(" SELECT DISTINCT TBL_SubMenuMaster.SubMenuID as MenuID, TBL_SubMenuMaster.SubMenuName  as MenuName  " +
                         "   FROM         Roleright INNER JOIN      " +
                         "   TBL_SubMenuMaster ON Roleright.FormId = TBL_SubMenuMaster.SubMenuID INNER JOIN   " +
                         "   TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID INNER JOIN    " +
                         "   CTuser ON Roleright.Usertypeid = CTuser.RollId      " +
                         "   WHERE     (CTuser.USERNAME = '" + USERNAME + "') AND (CTuser.PASSWord = '" + Password + "') and TBL_SubMenuMaster.MenuID='" + Menuid + "'   and  TBL_SubMenuMaster.Isvisable=1", con);
        }
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
        return ds;
    }


    public DataTable BindChildMenu_HMS(string Menuid, string USERNAME, string Password)
    {
        SqlConnection con = DataAccess.ConInitForHMS();
        SqlDataAdapter sda = new SqlDataAdapter();
        if (USERNAME == "Admin")
        {
            sda = new SqlDataAdapter("select SubMenuID as MenuID,SubMenuName as MenuName from TBL_subMenuMAster where MenuID='" + Menuid + "'  and  TBL_SubMenuMaster.Isvisible=1 ", con);
        }
        else if (USERNAME == "admin")
        {
            sda = new SqlDataAdapter("select SubMenuID as MenuID,SubMenuName as MenuName from TBL_subMenuMAster where MenuID='" + Menuid + "'  and  TBL_SubMenuMaster.Isvisible=1 ", con);
        }

        else
        {
            sda = new SqlDataAdapter(" SELECT DISTINCT TBL_SubMenuMaster.SubMenuID as MenuID, TBL_SubMenuMaster.SubMenuName  as MenuName  " +
                        "   FROM         Roleright INNER JOIN      " +
                        "   TBL_SubMenuMaster ON Roleright.FormId = TBL_SubMenuMaster.SubMenuID INNER JOIN   " +
                        "   TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID INNER JOIN    " +
                        "   CTuser ON Roleright.Usertypeid = CTuser.RollId      " +
                        "   WHERE     (CTuser.USERNAME = '" + USERNAME + "') AND (CTuser.PASSWord = '" + Password + "') and TBL_SubMenuMaster.MenuID='" + Menuid + "'   and  TBL_SubMenuMaster.Isvisible=1", con);
        }
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
        return ds;
    }

    public DataTable BindForm(int SubMenuid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select SubMenuNavigateURL from TBL_subMenuMAster where SubMenuID=" + SubMenuid + " ", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
        return ds;
    }
    public DataTable BindForm_HMS(int SubMenuid)
    {
        SqlConnection con = DataAccess.ConInitForHMS();
        SqlDataAdapter sda = new SqlDataAdapter("select SubMenuNavigateURL from TBL_subMenuMAster where SubMenuID=" + SubMenuid + " ", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
        return ds;
    }

    public void Insert_Roleright()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "SP_phrtroll";
        sc.Parameters.Add(new SqlParameter("@Usertypeid", SqlDbType.Int)).Value = P_Usertypeid;
        sc.Parameters.Add(new SqlParameter("@FormId", SqlDbType.Int)).Value = P_FormId;
        sc.Parameters.Add(new SqlParameter("@FormName", SqlDbType.NVarChar, 200)).Value = P_FormName;
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int)).Value = P_Branchid;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close(); conn.Dispose();
        }


    }

    public DataTable Get_Rollright(string Usertypeid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT     Roleright.Rightid, Roleright.Usertypeid, Roleright.FormId, Roleright.FormName, Roleright.Branchid, usr.ROLENAME, "+
                          "  TBL_SubMenuMaster.SubMenuNavigateURL, TBL_MenuMaster.MenuName "+
                          "  FROM         Roleright INNER JOIN "+
                          "  usr ON Roleright.Usertypeid = usr.ROLLID AND Roleright.Branchid = usr.branchid INNER JOIN "+
                          "  TBL_SubMenuMaster ON Roleright.FormId = TBL_SubMenuMaster.SubMenuID INNER JOIN "+
                          "  TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID where Usertypeid='" + Usertypeid + "' ", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
        return ds;
    }

    public DataTable BindChildMenu_treeview(string Menuid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select SubMenuID as MenuID,SubMenuName as MenuName from TBL_subMenuMAster where MenuID='" + Menuid + "'  and Isvisable=1 ", con);
               DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
        return ds;
    }
    public DataTable Bindbanner()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select BannerName, ID, Type, BalanceMail, PaymentTypeDefaultActive, BarCodeImageReq, isnull( DiscountPerValidate,0) as DiscountPerValidate  from BannerTable order by id asc ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }
    public DataTable Checkflag()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select distinct cevent from patmst where cevent=1 ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }
    public DataTable Bind_Shortcuttest()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        //SqlDataAdapter sda = new SqlDataAdapter(" select top(20) RoutinTestCode,RoutinTestCode+' - '+ RoutinTestName as Maintestname from RoutinTest  ", con);
        SqlDataAdapter sda = new SqlDataAdapter(" select top(100) RoutinTestCode, RoutinTestName as Maintestname from RoutinTest  ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public DataTable Checkflag_Result()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select PatientFlag from patmstd where PatientFlag=1 ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }
    public DataTable bindassignsubdept(string Uname)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT     Deptwiseuser.username, Deptwiseuser.DeptCodeID, Deptwiseuser.uname, Deptwiseuser.Createdby, Deptwiseuser.Createdon, Deptwiseuser.Updatedby, " +
          "  Deptwiseuser.Updatedon, Deptwiseuser.branchid, SubDepartment.subdeptName " +
          "  FROM         Deptwiseuser INNER JOIN "+
           " SubDepartment ON Deptwiseuser.DeptCodeID = SubDepartment.subdeptid AND Deptwiseuser.branchid = SubDepartment.Branchid  where Deptwiseuser.username='" + Uname + "' ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public bool UpdateAuthincate_result(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update patmstd set " +
            "PatientFlag=@PatientFlag  where   branchid=" + branchid + "", conn);


        sc.Parameters.Add(new SqlParameter("@PatientFlag", SqlDbType.Bit)).Value = 1;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch
            {

            }
        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        return true;
    }
    public bool UpdateAuthincate(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update patmst set " +
            "cevent=@cevent  where   branchid=" + branchid + "", conn);


        sc.Parameters.Add(new SqlParameter("@cevent", SqlDbType.Bit)).Value = 1;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch
            {

            }
        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        return true;
    }
    public bool Insert_Deptwiseuser(int DeptCodeID, string username, string uname, int branchid)
    {


        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
        "Insert into Deptwiseuser (username,DeptCodeID,[uname],[branchid]) " +
         " values(@username,@DeptCodeID,@uname,@branchid)", conn);
        // Add the employee ID parameter and set its value.
        sc.Parameters.Add(new SqlParameter("@DeptCodeID", SqlDbType.Int)).Value = DeptCodeID;
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = username;
        sc.Parameters.Add(new SqlParameter("@uname", SqlDbType.NVarChar, 50)).Value = uname;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;


        conn.Close();
        try
        {
            conn.Open();
            try
            {
                sc.ExecuteNonQuery();
            }
            catch { }

        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    } //insert End

    public DataTable Get_maindoctor()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select * from CTuser where Usertype='Main Doctor' or Usertype='MainDoctor' or Usertype='Technician' or Usertype='Reporting' ", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
        return ds;
    }
    public DataTable Get_subdepartment()
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT     subdeptid, subdeptName FROM         SubDepartment ", con);
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();

        }
        return ds;
    }
    public bool deletesubdept(string username)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_deleteDeptwiseuser]";
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 200)).Value = username;
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    }
    public  DataTable GetAllMachinData()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select * from  InstumentMaster ";


        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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

    public DataTable Bind_Machinecodemap(string MTCode, string MachinNAme)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("SELECT     Machinecodemap.Srno, Machinecodemap.Instname, Machinecodemap.MTCode, Machinecodemap.ParameterCode, Machinecodemap.Mapcode, " +
              "  SubTest.TestName, MainTest.Maintestname " +
              "  FROM         Machinecodemap INNER JOIN " +
               " MainTest ON Machinecodemap.MTCode = MainTest.MTCode LEFT OUTER JOIN " +
               " SubTest ON Machinecodemap.ParameterCode = SubTest.STCODE where Machinecodemap.Instname='" + MachinNAme + "' ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }
    public bool Insert_MachinMapCode(string InstName, string MTCode, string ParameterCode, string Mapcode)
    {
        SqlConnection conn = DataAccess.ConInitForDC();


        SqlCommand sc = new SqlCommand("" +
        "INSERT INTO Machinecodemap(InstName,MTCode,ParameterCode,Mapcode)" +
        "VALUES(@InstName,@MTCode,@ParameterCode,@Mapcode)", conn);

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@InstName", SqlDbType.NVarChar, 200)).Value = InstName;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 200)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@ParameterCode", SqlDbType.NVarChar, 200)).Value = ParameterCode;
        sc.Parameters.Add(new SqlParameter("@Mapcode", SqlDbType.NVarChar, 200)).Value = Mapcode;

        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    }
    public void delete_MapParameter(int Mapid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("delete from Machinecodemap where Srno='" + Mapid + "' ", conn);

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }

        }
    }
    public DataTable Check_ExistMachinparameter(string MTCode, string ParameterCode, string Instname)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter("select MTCode from Machinecodemap where Instname = '" + Instname + "' and MTCode ='" + MTCode + "' and ParameterCode='" + ParameterCode + "' ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }
    public DataTable GettestparameterName()
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_GettestparameterName", conn);

        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            conn.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        finally
        {

            conn.Close(); conn.Dispose();

        }

    }
    public DataTable GetDrRateList()
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("SP_GetDrList", conn);

        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            conn.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        finally
        {

            conn.Close(); conn.Dispose();

        }

    }

    public DataTable GetCenterwisePayment()
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("sp_getcenterwisepayment", conn);

        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            conn.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        finally
        {

            conn.Close(); conn.Dispose();

        }

    }

    public static DataSet DefaultTest3(object fdate, object tdate)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT "+
              "  TOP (99.99) PERCENT patmstd.PatRegID, patmstd.FID, patmstd.MTCode, patmstd.SDCode, patmstd.PackageCode, patmstd.PID, patmstd.UnitCode, patmstd.BarcodeID, "+
              "  patmstd.DigModule, patmstd.Patregdate, patmstd.Email, patmstd.Dramt, patmstd.cons, patmstd.MainRate, patmstd.Branchid, patmstd.Createdby, patmstd.Updatedby,  "+
              "  patmstd.IsActive, patmstd.Testdisc, patmstd.Testrecamt, patmstd.PatientFlag, patmstd.Labno, patmstd.ISReRun, patmstd.ReRunRemark, patmstd.MachinName,  "+
              "  patmstd.Patrepstatus, patmstd.PatRepDate, patmstd.Patauthicante, patmstd.TestedDate, patmstd.TestUser, patmstd.AunticateSignatureId, patmstd.PatientEmail,  "+
              "  patmstd.DoctorEmail, patmstd.ReportRemark, patmstd.LabTechnicianid, patmstd.SampleType, patmstd.Isoutsource, patmstd.OutLabName, patmstd.IspheboAccept,  "+
              "  MainTest.Maintestname, Cshmst.RecDate, Cshmst.AmtReceived, Cshmst.AmtPaid, patmstd.TestRate , patmst.intial+' '+ patmst.Patname as PatientName " +
              "  FROM         patmstd INNER JOIN "+
               " Cshmst ON patmstd.PatRegID = Cshmst.PatRegID AND patmstd.PID = Cshmst.PID INNER JOIN "+
               " MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN "+
               " patmst ON Cshmst.PID = patmst.PID where patmstd.Patregdate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).AddDays(+1).ToString("MM/dd/yyyy") + "' and  (Cshmst.Paymenttype = 'Cash')  ORDER BY patmstd.PatRegID asc", conn);//and DigModule=" + DigModule + "

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
    public static DataSet PatientTestDelete(int PID)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT " +
              "  TOP (99.99) PERCENT patmstd.PatRegID, patmstd.FID, patmstd.MTCode, patmstd.SDCode, patmstd.PackageCode, patmstd.PID, patmstd.UnitCode, patmstd.BarcodeID, " +
              "  patmstd.DigModule, patmstd.Patregdate, patmstd.Email, patmstd.Dramt, patmstd.cons, patmstd.MainRate, patmstd.Branchid, patmstd.Createdby, patmstd.Updatedby,  " +
              "  patmstd.IsActive, patmstd.Testdisc, patmstd.Testrecamt, patmstd.PatientFlag, patmstd.Labno, patmstd.ISReRun, patmstd.ReRunRemark, patmstd.MachinName,  " +
              "  patmstd.Patrepstatus, patmstd.PatRepDate, patmstd.Patauthicante, patmstd.TestedDate, patmstd.TestUser, patmstd.AunticateSignatureId, patmstd.PatientEmail,  " +
              "  patmstd.DoctorEmail, patmstd.ReportRemark, patmstd.LabTechnicianid, patmstd.SampleType, patmstd.Isoutsource, patmstd.OutLabName, patmstd.IspheboAccept,  " +
              "  MainTest.Maintestname, Cshmst.RecDate, Cshmst.AmtReceived, Cshmst.AmtPaid, patmstd.TestRate , patmst.intial+' '+ patmst.Patname as PatientName " +
              " , patmstd.TestDeActive FROM         patmstd INNER JOIN " +
               " Cshmst ON patmstd.PatRegID = Cshmst.PatRegID AND patmstd.PID = Cshmst.PID INNER JOIN " +
               " MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN " +
               " patmst ON Cshmst.PID = patmst.PID where patmstd.Patauthicante='Registered' and patmstd.PID= " + PID + " ORDER BY patmstd.PatRegID asc", conn);//and DigModule=" + DigModule + "

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
    public bool Delete(int branchid, string UserName, string DeptCodeID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            " Delete from Deptwiseuser " +
            " Where username=@username and DeptCodeID=@DeptCodeID and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 500)).Value = UserName;
        sc.Parameters.Add(new SqlParameter("@DeptCodeID", SqlDbType.NVarChar, 500)).Value = DeptCodeID;
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                throw;
            }
        }
        return true;
    }//End Delete


    public DataTable AithorizedTestCount(int PID)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        //SqlDataAdapter sda = new SqlDataAdapter(" select top(20) RoutinTestCode,RoutinTestCode+' - '+ RoutinTestName as Maintestname from RoutinTest  ", con);
        SqlDataAdapter sda = new SqlDataAdapter(" select distinct Patauthicante from patmstd where pid= " + PID + " and Patauthicante='Authorized' ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }


    public DataTable BindMainMenuHMS(string USERNAME, string Password)
    {
        SqlConnection con = DataAccess.ConInitForHMS();
        SqlDataAdapter sda = new SqlDataAdapter();
        if (USERNAME == "Admin")
        {
            sda = new SqlDataAdapter("select MenuID , MenuName   from TBL_MenuMAster where Moduleid=2 and isactive=1 ", con);
        }
       else if (USERNAME == "admin")
        {
            sda = new SqlDataAdapter("select MenuID , MenuName   from TBL_MenuMAster where Moduleid=2 and isactive=1 ", con);
        }
        else
        {
            sda = new SqlDataAdapter("SELECT DISTINCT    " +
                     "     TBL_MenuMaster.MenuID ,TBL_MenuMaster.MenuName  " +
                     "  FROM         Roleright INNER JOIN      " +
                     "  TBL_SubMenuMaster ON Roleright.FormId = TBL_SubMenuMaster.SubMenuID INNER JOIN  " +
                     "  TBL_MenuMaster ON TBL_SubMenuMaster.MenuID = TBL_MenuMaster.MenuID INNER JOIN   " +
                     "  CTuser ON Roleright.Usertypeid = CTuser.RollId      " +
                     "  WHERE    TBL_MenuMAster.Moduleid=2 and  (CTuser.USERNAME = '" + USERNAME + "') AND (CTuser.password = '" + Password + "') and  TBL_SubMenuMaster.Isvisible=1  " +
                     "  order by MenuID   ", con);
        }
        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public void Insert_DailyActivity()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "SP_ActivityDetails";
        sc.Parameters.Add(new SqlParameter("@Patregno", SqlDbType.NVarChar, 250)).Value = P_Patregno;
        sc.Parameters.Add(new SqlParameter("@FormName", SqlDbType.NVarChar, 250)).Value = P_FormName;
        sc.Parameters.Add(new SqlParameter("@EventName", SqlDbType.NVarChar, 250)).Value = P_EventName;
        sc.Parameters.Add(new SqlParameter("@CreatedBy", SqlDbType.NVarChar, 250)).Value =P_UserName;
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int)).Value = P_Branchid;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close(); conn.Dispose();
        }


    }


    public void Insert_CenterLedger_Report()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "SP_CenterLedgerReport";
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = P_Patregno;
        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.NVarChar, 250)).Value = P_BillNo;
        sc.Parameters.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar, 250)).Value = P_PatientName;

        sc.Parameters.Add(new SqlParameter("@Centercode1", SqlDbType.NVarChar, 250)).Value = P_CenterName;
        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 250)).Value = P_CenterName;
        sc.Parameters.Add(new SqlParameter("@RegDate", SqlDbType.NVarChar, 250)).Value = P_RegDate;
        sc.Parameters.Add(new SqlParameter("@DebitAmt", SqlDbType.Float)).Value = P_DebitAmt;
        sc.Parameters.Add(new SqlParameter("@CreditAmt", SqlDbType.Float)).Value = P_CreditAmt;

        sc.Parameters.Add(new SqlParameter("@BalanceAmount", SqlDbType.Float)).Value = P_Balance;
        sc.Parameters.Add(new SqlParameter("@ParticularField", SqlDbType.NVarChar, 250)).Value = P_Particular;
        sc.Parameters.Add(new SqlParameter("@ModeOfPayment", SqlDbType.NVarChar, 250)).Value = P_PaymentType;


        sc.Parameters.Add(new SqlParameter("@EntryuserName", SqlDbType.NVarChar, 250)).Value = P_UserName;
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int)).Value = P_Branchid;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            conn.Close(); conn.Dispose();
        }


    }

    public bool TruncateLedger()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[sp_CenterLedger_Reporttruncate]";


        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                if (sdr != null) sdr.Close();
                conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
        }
        // Implement Update logic.
        return true;
    }


    public DataTable Get_SerialKey()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "Select count(PPID) as Patientcount from Patmt ";



        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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

    public int GetDailyRegNo(int branchid,int FID)
    {
        int i = -1;
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {
            SqlCommand cmd = new SqlCommand(" select isnull(max(cast(Patregid as int)),0)+1  from patmst where   branchid=" + branchid + " and FID="+FID+" ", conn);
            object ob = "";
            conn.Open();
            ob = cmd.ExecuteScalar();
            if (ob != DBNull.Value)
            {
                if (ob != "")
                {
                    i = Convert.ToInt32(ob);
                }
            }
        }
        catch (Exception ex) { throw; }
        finally
        {
            conn.Close(); conn.Dispose();
        }
        return i;
    }

    public DataTable BindShortCut(string USERNAME, string Password)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlDataAdapter sda = new SqlDataAdapter();

        sda = new SqlDataAdapter("select distinct cast( Description as nvarchar(4000)) as Description FROM stformmst WHERE Description LIKE '" + USERNAME + "' + '%' ", con);

        DataTable ds = new DataTable();
        try
        {
            sda.Fill(ds);
        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return ds;
    }

    public DataTable Sp_VW_CenterledgerAll_Client(string centercode, DateTime FromDate, DateTime Todate)
    {
        DataAccess data = new DataAccess();
        SqlConnection conn = data.ConInitForDC1();
        SqlCommand sc = new SqlCommand("Sp_VW_CenterledgerAll_Client", conn);
        sc.Parameters.Add(new SqlParameter("@Initdt", SqlDbType.DateTime)).Value = FromDate;
        sc.Parameters.Add(new SqlParameter("@finaldt", SqlDbType.DateTime)).Value = Todate;
        sc.Parameters.Add(new SqlParameter("@centercode", SqlDbType.NVarChar, 250)).Value = centercode;
        sc.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter();
        da.SelectCommand = sc;

        try
        {
            conn.Open();
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;

        }
        catch (SqlException)
        {
            throw;
        }
        finally
        {

            conn.Close(); conn.Dispose();

        }

    }
    public DataTable GetAllFutureFormat( int DrId)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "";
        if (DrId > 0)
        {
            query = "select * from  dfrmst where DrSignature=" + DrId + " and isactive=1 order by name ";
        }
        else
        {
            query = "select * from  dfrmst where  isactive=1 order by name ";
        }


        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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
    public DataTable Get_TotalPatientVal()
    {

        SqlConnection conn = DataAccess.ConInitForHM();
        SqlDataAdapter da;
        string query = "  select * from patientinformation where patregid=1000 and (Firstname='Patricia Liverpool' or Firstname='Khushaan Parboo') ";



        da = new SqlDataAdapter(query, conn);
        DataTable ds = new DataTable();
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

    private int Usertypeid;    public int P_Usertypeid
    {
        get { return Usertypeid; }  set { Usertypeid = value; }
    }
    private int FormId;  public int P_FormId
    {
        get { return FormId; }
        set { FormId = value; }
    }



    private string CenterName;
    public string P_CenterName
    {
        get { return CenterName; }
        set { CenterName = value; }
    }
    private string Patregno;
    public string P_Patregno
    {
        get { return Patregno; }
        set { Patregno = value; }
    }
    private string FormName;
    public string P_FormName
    {
        get { return FormName; }
        set { FormName = value; }
    }
    private string EventName;
    public string P_EventName
    {
        get { return EventName; }
        set { EventName = value; }
    }

    private string UserName;
    public string P_UserName
    {
        get { return UserName; }
        set { UserName = value; }
    }
    
    private int Branchid;
    public int P_Branchid
    {
        get { return Branchid; }
        set { Branchid = value; }
    }



    private string RegDate;
    public string P_RegDate
    {
        get { return RegDate; }
        set { RegDate = value; }
    }
    private string Particular;
    public string P_Particular
    {
        get { return Particular; }
        set { Particular = value; }
    }

    private string BillNo;
    public string P_BillNo
    {
        get { return BillNo; }
        set { BillNo = value; }
    }

    private float DebitAmt;
    public float P_DebitAmt
    {
        get { return DebitAmt; }
        set { DebitAmt = value; }
    }

    private float CreditAmt;
    public float P_CreditAmt
    {
        get { return CreditAmt; }
        set { CreditAmt = value; }
    }

    private float Balance;
    public float P_Balance
    {
        get { return Balance; }
        set { Balance = value; }
    }

    private string PaymentType;
    public string P_PaymentType
    {
        get { return PaymentType; }
        set { PaymentType = value; }
    }
    private string PatientName;
    public string P_PatientName
    {
        get { return PatientName; }
        set { PatientName = value; }
    }
}