using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;

public class Cshmst_supp_Bal_C
{
	public Cshmst_supp_Bal_C()
	{
		//
		// TODO: Add constructor logic here
		//

	}
    public static DataTable GetDoctorcomplimentData_summary()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da = new SqlDataAdapter("SELECT * from VW_drspmain_summary ", conn);
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
 
    public static DataTable GetDoctorcomplimentData()
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter da = new SqlDataAdapter("SELECT *, case when Expr1=0 then 0 else dbo.FUN_GetCompAmtFull(VW_drspmain.Branchid,patmst.PID,'') - DrGiven  end  as FinalDrCharges,patmst.UploadPrescription  FROM            VW_drspmain INNER JOIN " +
                      "  patmst ON VW_drspmain.PID = patmst.PID AND VW_drspmain.PatRegID = patmst.PatRegID AND VW_drspmain.Branchid = patmst.Branchid "+
                      "  ORDER BY patmst.PID DESC ", conn);
        DataTable  ds = new DataTable();
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
 

    public static bool ExistUniqueId(int PID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM RecM" +
                         " WHERE PID=@PID and branchid=" + branchid + "  ", conn);
        
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;

        int cnt = 0;

        try
        {
            conn.Open();
            cnt = Convert.ToInt32(sc.ExecuteScalar());

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
                // Log an event in the Application Event Log.
                throw;
            }
           
        }
        if (cnt != 0)
            return true;
        else
            return false;
    }

    public DataSet GetTestFromBillNo(int billno,int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from Cshmst where billno=@billno and branchid=" + branchid + " ", conn);

        sc.Parameters.Add(new SqlParameter("@billno", SqlDbType.Int)).Value = billno;
        SqlDataAdapter da = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        SqlDataReader sdr = null;

        try
        {
            da.Fill(ds);
            int cnt = ds.Tables[0].Rows.Count;
            ds.Tables[0].Columns.Add("testname");
            ds.Tables[0].Columns.Add("testrate");
            string Maintestname = null;

            string testId = Convert.ToString(ds.Tables[0].Rows[0].ItemArray[15]);
            string Cname = Convert.ToString(ds.Tables[0].Rows[0].ItemArray[0]);

            string[] tid = testId.Split(',');
            for (int j = 0; j < tid.Length; j++)
            {
                DataRow row1 = ds.Tables[0].NewRow();
                string tename = tid[j].Trim();

                if (tename.Length == 4)
                {
                    Maintestname = Packagenew_Bal_C.getPNameByCode(tename, branchid);
                }
                else
                {
                    Maintestname = MainTestLog_Bal_C.GET_Maintest_name(tename, branchid);
                }
                DrMT_Bal_C drTable = new DrMT_Bal_C(Cname, "CC", branchid);
              
                SpeCh_Bal_C sp = new SpeCh_Bal_C(tename, 0, branchid);
                row1["Patienttest"] = tename;
                row1["testname"] = Maintestname;
                row1["testrate"] = sp.Amount;
                ds.Tables[0].Rows.Add(row1);
            }
            ds.Tables[0].Rows[0].Delete();

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
        return ds;
    } //for default

    public static ICollection GetAllDetailsfromBillNo(string billno, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;
        sc = new SqlCommand("select * from Cshmst where billno=@billno and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@billno", SqlDbType.NVarChar, 50)).Value = billno;
        SqlDataReader dr = null;


        try
        {
            conn.Open();
            dr = sc.ExecuteReader();

            if (dr != null)
            {
                if (dr.Read())
                {
                    Cshmst_Bal_C Obj_CBC = new Cshmst_Bal_C();
                   
                    Obj_CBC.AmtReceived = Convert.ToSingle(dr["AmtReceived"]);
                    Obj_CBC.P_Centercode = dr["CenterCode"].ToString();
                    Obj_CBC.RecDate = Convert.ToDateTime(dr["RecDate"]);
                    Obj_CBC.BankName = dr["BankName"].ToString();
                    Obj_CBC.BillType = dr["BillType"].ToString();
                    Obj_CBC.Discount = dr["Discount"].ToString();
                    Obj_CBC.Paymenttype = dr["Paymenttype"].ToString();
                    Obj_CBC.NetPayment = Convert.ToSingle(dr["NetPayment"].ToString());
                   
                    Obj_CBC.Patientname= dr["Patname"].ToString();
                    Obj_CBC.patRegID = dr["PatRegID"].ToString();
                 
                    Obj_CBC.Remark = dr["Comment"].ToString();
                    Obj_CBC.AccNo = dr["AccNo"].ToString();
                  
                    Obj_CBC.Cardtype = dr["Cardtype"].ToString();
                    Obj_CBC.CardName = dr["CardName"].ToString();
                    Obj_CBC.CardNo = dr["CardNo"].ToString();

                    if (dr["ChqDate"] != DBNull.Value)
                        Obj_CBC.ChqDate = Convert.ToDateTime(dr["ChqDate"]);
                    else
                        Obj_CBC.ChqDate = Date.getMinDate();

                    Obj_CBC.ChqNo = dr["ChqNo"].ToString();
                    Obj_CBC.City = dr["City"].ToString();

                    if (dr["ExpiryDate"] != DBNull.Value)
                        Obj_CBC.ExpiryDate = Convert.ToDateTime(dr["ExpiryDate"]);
                    else
                        Obj_CBC.ExpiryDate = Date.getMinDate();
                  
                    al.Add(Obj_CBC);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                if (dr != null) dr.Close();

                conn.Close(); conn.Dispose();

            }
            catch (SqlException)
            {
                throw;
            }
            
        }

        return al;

    }


    public static string GetLastCreditDate(string CenterCode, int branchid)
    {
        string totamt = "";
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        sc = new SqlCommand("SELECT top 1 RecDate FROM Cshmst where billtype='' and CenterCode=@CenterCode  and branchid=" + branchid + " order by RecDate desc", conn);

        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 50)).Value = CenterCode;

        try
        {
            conn.Open();
         
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                totamt = "";
            else
                totamt = Convert.ToString(o);
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
       
        return totamt;
    }



    public static ICollection getCashCreditMainTableByFromTo(object dfrom, object dto, string CenterCode, string BillType, string sortValue, int branchid)
    {
        ArrayList al = new ArrayList();
               
        SqlCommand sc = null;
        SqlConnection conn = DataAccess.ConInitForDC(); 
        sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandText = "Select * from Cshmst where BillType=@BillType and branchid=" + branchid + ""; ;
        sc.Parameters.Add(new SqlParameter("@BillType", SqlDbType.NVarChar, 50)).Value = BillType;
        if (CenterCode != "")
        {
            sc.CommandText = sc.CommandText + " and CenterCode = @CenterCode ";
            sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 200)).Value = CenterCode;
        }
        if (dfrom != null && dto != null)
        {
            sc.CommandText = sc.CommandText + " and RecDate >= @fromdate and RecDate <=@todate  ";
            sc.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(dfrom);
            sc.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime)).Value = Convert.ToDateTime(dto);
        }
        if (sortValue != "")
        {
            sc.CommandText = sc.CommandText + " order by " + sortValue;
        }
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            // This is not a while loop. It only loops once.
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Cshmst_Bal_C ccm = new Cshmst_Bal_C();
                    ccm.P_Centercode = Convert.ToString(sdr["CenterCode"]);
                    ccm.BillType = Convert.ToString( sdr["BillType"]);
                    ccm.BillNo = Convert.ToInt32(sdr["BillNo"]);
                    ccm.RecDate = Convert.ToDateTime(sdr["RecDate"]);
                  
                    ccm.AmtReceived = Convert.ToSingle(sdr["AmtReceived"]);
                    ccm.Remark =  Convert.ToString(sdr["Comment"]);
                    ccm.Paymenttype = Convert.ToString( sdr["Paymenttype"]);
                  
                    al.Add(ccm);
                }
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
        return al;
    } 

    public static ICollection getCashMainTableByFromTo(object dfrom, object dto, string CenterCode, string sortValue, int branchid)
    {
        ArrayList al = new ArrayList();

        SqlCommand sc = null;
        SqlConnection conn = DataAccess.ConInitForDC(); 
        sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandText = "Select * from Cshmst where (billtype='Cash Bill' OR BillType = 'Credit Note')  and branchid=" + branchid + ""; ;
        if (CenterCode != "")
        {
            sc.CommandText = sc.CommandText + " and CenterCode = @CenterCode ";
            sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 200)).Value = CenterCode;
        }
        if (dfrom != null && dto != null)
        {
            sc.CommandText = sc.CommandText + " and RecDate >= @fromdate and RecDate <=@todate  ";
            sc.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(dfrom);
            sc.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime)).Value = Convert.ToDateTime(dto);
        }
        if (sortValue != "")
        {
            sc.CommandText = sc.CommandText + " order by " + sortValue;
        }
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Cshmst_Bal_C ccm = new Cshmst_Bal_C();
                    ccm.P_Centercode = Convert.ToString(sdr["CenterCode"]);
                    ccm.BillType = Convert.ToString(sdr["BillType"]);
                    ccm.BillNo = Convert.ToInt32(sdr["BillNo"]);
                    ccm.RecDate = Convert.ToDateTime(sdr["RecDate"]);
                    ccm.AmtReceived = Convert.ToSingle(sdr["AmtReceived"]);
                    ccm.Remark = Convert.ToString(sdr["Comment"]);
                    ccm.Paymenttype = Convert.ToString(sdr["Paymenttype"]);
                  
                    ccm.NetPayment = Convert.ToSingle(sdr["NetPayment"]);
                    ccm.patRegID = sdr["PatRegID"].ToString();
                    ccm.Patientname= sdr["Patname"].ToString();
                    ccm.City = sdr["City"].ToString();
                    if (sdr["ChqNo"] != DBNull.Value)
                        ccm.ChqNo = sdr["ChqNo"].ToString();
                    else
                        ccm.ChqNo = "";
                    al.Add(ccm);
                }
            }
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
        return al;
    } //for default

    public static ICollection getCashDetailsByDate(object dfrom, string CenterCode, string sortValue, int branchid)
    {
        ArrayList al = new ArrayList();

        SqlCommand sc = null;
        SqlConnection conn = DataAccess.ConInitForDC(); 
        sc = new SqlCommand();
        sc.Connection = conn;
        sc.CommandText = "Select * from Cshmst where (billtype='Cash Bill') and branchid=" + branchid + ""; ;
   
        if (dfrom != null)
        {
            sc.CommandText = sc.CommandText + " and RecDate = @fromdate ";
            sc.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(dfrom);
        }
        if (sortValue != "")
        {
            sc.CommandText = sc.CommandText + " order by " + sortValue;
        }
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Cshmst_Bal_C ccm = new Cshmst_Bal_C();
                    ccm.P_Centercode = Convert.ToString(sdr["CenterCode"]);
                    ccm.BillType = Convert.ToString(sdr["BillType"]);
                    ccm.BillNo = Convert.ToInt32(sdr["BillNo"]);
                    ccm.RecDate = Convert.ToDateTime(sdr["RecDate"]);
                    ccm.AmtReceived = Convert.ToSingle(sdr["AmtReceived"]);
                    ccm.Remark = Convert.ToString(sdr["Comment"]);
                    ccm.Paymenttype = Convert.ToString(sdr["Paymenttype"]);
                  
                    ccm.NetPayment = Convert.ToSingle(sdr["NetPayment"]);
                    ccm.Patientname = sdr["Patname"].ToString();
                    ccm.username = sdr["username"].ToString();
                    al.Add(ccm);
                }
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
        return al;
    } 

    public static string GetPatientName(int billno, int branchid)
    {
        string pname = "";
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        sc = new SqlCommand("SELECT Patname FROM Cshmst where billno=" + billno + " and branchid=" + branchid + "", conn);

        try
        {
            conn.Open();
            //totamt = Convert.ToString(sc.ExecuteScalar());
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                pname = "";
            else
                pname = Convert.ToString(o);
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
            catch (Exception)
            {
                throw new Exception("Record not found");
            }
        }
       
        return pname;
    }
    public static int GetBillNo(int PID, int branchid,string FID)
    {
        int bno = 0;
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;

        sc = new SqlCommand("SELECT distinct BillNo FROM RecM where PID=" + PID + " and branchid=" + branchid + " and FID='"+FID+" ' ", conn);

        try
        {
            conn.Open();            
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                bno = 0;
            else
                bno = Convert.ToInt32(o);
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
        return bno;
    }

   
  
    public static DataSet getLoginfoDataForsummaryRemBalance_Center_outs(object fdate, object tdate, string user, int branchid, int DigModule, string Username,string CenterN)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

       // SqlDataAdapter da = new SqlDataAdapter("SELECT Cshmst.username,convert(char(12),RecDate,105) as RecDate,balance,Cshmst.PatRegID,Patname,( Cshmst.NetPayment)as NetPayment,AmtPaid , TaxAmount, TaxPer ,  Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount) as Mainbalance, case when patmst.IsbillBH=1 then Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount ) else 0 end as BTHbalance,  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, patmst.Drname, patmst.Centername, patmst.Comment from  Cshmst INNER JOIN  patmst ON Cshmst.PID = patmst.PID where  patmst.isactive=1  and (CAST(CAST(YEAR( RecDate) AS varchar(4)) + '/' + CAST(MONTH( RecDate) AS varchar(2)) + '/' + CAST(DAY( RecDate) AS varchar(2)) AS datetime)) between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).AddDays(+1).ToString("MM/dd/yyyy") + "' and Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount)>0 and Cshmst.branchid=" + branchid + "  and   patmst.Centercode ='" + CenterN + "' order by Cshmst.username", conn);//and DigModule=" + DigModule + " and Cshmst.username ='" + Username + "'
        SqlDataAdapter da = new SqlDataAdapter("SELECT RecM.username,convert(char(12),RecM.TransDate,105) as RecDate,  Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt)) as balance,Patmst.PatRegID, " +
                  "  Patmst.Patname,( Patmst.TestCharges)as NetPayment,sum(AmtPaid) as AmtPaid , " +
                  "  TaxAmount, TaxPer ,  Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt)) as Mainbalance,  " +
                  "  case when patmst.IsbillBH=1 then Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt) ) else 0 end as BTHbalance,  " +
                  "  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, patmst.DrName, patmst.Centername, patmst.Remark " +
                  "  from  RecM INNER JOIN  patmst ON RecM.PID = patmst.PID  group by RecM.username,convert(char(12),RecM.TransDate,105),Patmst.TestCharges,Patmst.Patname,TaxAmount, TaxPer,patmst.IsbillBH, " +
                  "  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, patmst.DrName, patmst.Centername, patmst.Remark,RecM.branchid,patmst.PatRegID,patmst.isactive,(CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime))  having  patmst.isactive=1  and  (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime))  between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt))>0   and   patmst.Centercode ='" + CenterN + "' and RecM.branchid=" + branchid + "   order by RecM.username", conn);//and DigModule=" + DigModule + " and Cshmst.username ='" + Username + "'
 
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


    public static DataSet getLoginfoDataForsummaryRemBalance_Center(object fdate, object tdate, string user, int branchid, int DigModule,string Username)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter da = new SqlDataAdapter("SELECT RecM.username,convert(char(12),RecM.TransDate,105) as RecDate,  Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt)) as balance,Patmst.PatRegID, "+
                  "  Patmst.Patname,( Patmst.TestCharges)as NetPayment,sum(AmtPaid) as AmtPaid , "+
                  "  TaxAmount, TaxPer ,  Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt)) as Mainbalance,  "+
                  "  case when patmst.IsbillBH=1 then Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt) ) else 0 end as BTHbalance,  "+
                  "  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, patmst.DrName, patmst.Centername, patmst.Remark "+
                  "  from  RecM INNER JOIN  patmst ON RecM.PID = patmst.PID  group by RecM.username,convert(char(12),RecM.TransDate,105),Patmst.TestCharges,Patmst.Patname,TaxAmount, TaxPer,patmst.IsbillBH, "+
                  "  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, patmst.DrName, patmst.Centername, patmst.Remark,RecM.branchid,patmst.PatRegID,patmst.isactive,(CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime))  having  patmst.isactive=1  and  (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime))  between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and  Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt))>0 and RecM.branchid=" + branchid + "   order by RecM.username", conn);//and DigModule=" + DigModule + " and Cshmst.username ='" + Username + "'
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

    public static DataSet getLoginfoDataForsummaryRemBalance_CenterDue(object fdate, object tdate, string user, int branchid, int DigModule,string center)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        SqlDataAdapter da = new SqlDataAdapter("SELECT Cshmst.username,convert(char(12),RecDate,105) as RecDate,balance,Cshmst.PatRegID,patmst.Patname,( Cshmst.NetPayment)as NetPayment,AmtPaid , TaxAmount, TaxPer , case when patmst.IsbillBH=0 then Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount) else 0 end as Mainbalance, case when patmst.IsbillBH=1 then Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount ) else 0 end as BTHbalance,  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, patmst.Drname, patmst.Centername, patmst.Remark  from  Cshmst INNER JOIN  patmst ON Cshmst.PID = patmst.PID where  patmst.isactive=1  and RecDate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).AddDays(+1).ToString("MM/dd/yyyy") + "' and balance>0 and Cshmst.branchid=" + branchid + " and patmst.Centercode='" + center + "'  order by Cshmst.username", conn);//and DigModule=" + DigModule + "

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
 

    public static DataSet getLoginfoDataForsummaryRemBalance(object fdate, object tdate, string user, int branchid, int DigModule)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC();

       // SqlDataAdapter da = new SqlDataAdapter("SELECT Cshmst.username,convert(char(12),RecDate,105) as RecDate,balance,Cshmst.PatRegID,patmst.Patname,( Cshmst.NetPayment)as NetPayment,AmtPaid , TaxAmount, TaxPer , case when patmst.IsbillBH=0 then Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount) else 0 end as Mainbalance, case when patmst.IsbillBH=1 then Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount ) else 0 end as BTHbalance,  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, patmst.Drname, patmst.Centername, patmst.Remark from  Cshmst INNER JOIN  patmst ON Cshmst.PID = patmst.PID where  patmst.isactive=1  and (CAST(CAST(YEAR( RecDate) AS varchar(4)) + '/' + CAST(MONTH( RecDate) AS varchar(2)) + '/' + CAST(DAY( RecDate) AS varchar(2)) AS datetime)) between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and balance>0 and Cshmst.branchid=" + branchid + "  order by Cshmst.username", conn);//and DigModule=" + DigModule + "
        SqlDataAdapter da = new SqlDataAdapter("SELECT RecM.username,convert(char(12),RecM.TransDate,105) as RecDate,0 as balance,Patmst.PatRegID,patmst.Patname,( Patmst.TestCharges)as NetPayment,sum(AmtPaid )as AmtPaid, "+
                          "  TaxAmount, TaxPer , case when patmst.IsbillBH=0 then Patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt)) else 0 end as Mainbalance, "+
                          "  case when patmst.IsbillBH=1 then Patmst.TestCharges - (sum(Recm.AmtPaid) +sum( RecM.DisAmt)) else 0 end as BTHbalance,  patmst.RefDr,patmst.Patphoneno,  "+
                          "  patmst.Centercode, patmst.Drname, patmst.Centername, patmst.Remark from  RecM INNER JOIN  patmst ON RecM.PID = patmst.PID "+
                          "  group by "+
                          "  RecM.username,convert(char(12),RecM.TransDate,105) ,Patmst.PatRegID,patmst.Patname,( Patmst.TestCharges), "+
                          "  TaxAmount, TaxPer , patmst.IsbillBH, "+
                          "  patmst.RefDr,patmst.Patphoneno,  "+
                          "  patmst.Centercode, patmst.Drname, patmst.Centername, patmst.Remark,patmst.isactive, "+
                          "  (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime)) ,RecM.branchid "+

                          "  having  patmst.isactive=1  "+
                          "  and (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime))  "+
                          "  between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and RecM.branchid=" + branchid + " and Patmst.TestCharges - (sum(Recm.AmtPaid) +sum( RecM.DisAmt))>0 " +
                          "  order by RecM.username", conn);//and DigModule=" + DigModule + "

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
  
    public static float GetBalance(int PID, int branchid)
    {
        float totamt = 0f;
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;
        sc = new SqlCommand("SELECT balance FROM Cshmst where PID=" + PID + " and branchid=" + branchid + "", conn);
        try
        {
            conn.Open();
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                totamt = 0f;
            else
                totamt = Convert.ToSingle(o);
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
      
        return totamt;
    }

       public static bool isBillNoExists(int billno,int branchid,string FID)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand(" SELECT count(*)" +
                         " FROM Cshmst " +
                         " WHERE billno=@billno and branchid=" + branchid + " and FID='" + branchid + "'", conn);

        
        sc.Parameters.Add(new SqlParameter("@billno", SqlDbType.Int)).Value = billno;

        int cnt = 0;

        try
        {
            conn.Open();
            cnt = Convert.ToInt32(sc.ExecuteScalar());

        }
            catch(Exception ex)
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
                // Log an event in the Application Event Log.
                throw;
            }
            catch
            {
                throw new Exception("Record not found");
            }
        }
        if (cnt != 0)
            return true;
        else
            return false;
    }


    public static DataSet Get_DPRRepotData_Operator(object tdate, object fdate, string user, string Centercode, int branchid, int DigModule, string UnitCode,string ModeOfPay)//
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT     convert(varchar(20), RecM.tdate,103)+' '+convert(varchar(20),convert(time,  RecM.tdate),100) AS RecDate, Patmst.TestCharges as NetPayment, RecM.username,  "+
                       " patmst.PatRegID, patmst.Patphoneno, RecM.BillNo,  sum( RecM.AmtPaid)-sum(RecM.refundamt) as AmtReceived, RecM.Paymenttype,sum( RecM.DisAmt) as Discount, "+
                       " patmst.intial + ' ' + Patmst.Patname AS Patname, sum( RecM.AmtPaid) -sum(RecM.refundamt)as AmtPaid, 0 as Balance,   RecM.TaxPer, ROUND(  RecM.TaxAmount,0) as TaxAmount, "+
                       " CASE WHEN patmst.IsbillBH = 0 THEN Patmst.TestCharges -(sum( RecM.AmtPaid)+sum( RecM.DisAmt)) ELSE 0 END AS Mainbalance,  CASE WHEN patmst.IsbillBH = 1 THEN Patmst.TestCharges -(sum( RecM.AmtPaid)+sum( RecM.DisAmt))  ELSE 0 END AS BTHbalance ,  "+
                       " RecM.Comment " + //BalAmt
           " from RecM INNER JOIN  patmst ON RecM.PID = patmst.PID   " +
           " where patmst.IsActive=1 and   (CAST(CAST(YEAR(RecM.transdate) AS varchar(4)) + '/' + CAST(MONTH(RecM.transdate) AS varchar(2)) + '/' + CAST(DAY(RecM.transdate) AS varchar(2)) AS datetime))  between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and RecM.branchid=" + branchid + "";

        //RecM.AmtPaid >0 and
        //if (DigModule != 0)
        //{
        //    query += " and DigModule=" + DigModule + "";
        //}
        if (Centercode != "" && Centercode != "0" && Centercode != null)
        {
            query += " and Patmst.CenterCode='" + Centercode + "'";
        }
        if (UnitCode != "" && UnitCode != null)
        {
            query += " and patmst.UnitCode='" + UnitCode + "'";
        }
        if (user != "" && user != null)
        {
            query += " and RecM.username='" + user + "'";
        }
        if (ModeOfPay != "All")
        {
            query += " and RecM.Paymenttype='" + ModeOfPay + "'";
        }
        query += " group by convert(varchar(20), RecM.tdate,103)+' '+convert(varchar(20),convert(time,  RecM.tdate),100) , Patmst.TestCharges , " +
                 "   RecM.username, patmst.PatRegID, patmst.Patphoneno, RecM.BillNo,     RecM.Paymenttype,  patmst.intial , Patmst.Patname ,   "+
                 "   RecM.TaxPer, ROUND(  RecM.TaxAmount,0) ,patmst.IsbillBH , RecM.Comment   ";

        query += "   union all " +
                " SELECT     convert(varchar(20), RecM.tdate,103)+' '+convert(varchar(20),convert(time,  RecM.tdate),100) AS RecDate, -Patmst.TestCharges as NetPayment, RecM.username, "+
                " patmst.PatRegID, patmst.Patphoneno, RecM.BillNo,  -sum( RecM.AmtPaid) as AmtReceived, RecM.Paymenttype,-sum( RecM.DisAmt) as Discount, "+
               " patmst.intial + ' ' + Patmst.Patname AS Patname, sum( RecM.AmtPaid) -sum(RecM.refundamt)as AmtPaid, 0 as Balance,   RecM.TaxPer, ROUND(  RecM.TaxAmount,0) as TaxAmount, "+
               " CASE WHEN patmst.IsbillBH = 0 THEN Patmst.TestCharges -(sum( RecM.AmtPaid)+sum( RecM.DisAmt)) ELSE 0 END AS Mainbalance,  CASE WHEN patmst.IsbillBH = 1 THEN Patmst.TestCharges -(sum( RecM.AmtPaid)+sum( RecM.DisAmt))  ELSE 0 END AS BTHbalance , "+
               " RecM.Comment  "+
               " FROM         RecM INNER JOIN  patmst ON RecM.PID = patmst.PID " +
            " where    (CAST(CAST(YEAR(RecM.transdate) AS varchar(4)) + '/' + CAST(MONTH(RecM.transdate) AS varchar(2)) + '/' + CAST(DAY(RecM.transdate) AS varchar(2)) AS datetime))  between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and RecM.branchid=" + branchid + " and  patmst.IsFreeze=1 ";
        //if (DigModule != 0)
        //{
        //    query += " and DigModule=" + DigModule + "";
        //}
        if (Centercode != "" && Centercode != "0" && Centercode != null)
        {
            query += " and patmst.CenterCode='" + Centercode + "'";
        }
        if (UnitCode != "" && UnitCode != null)
        {
            query += " and patmst.Unitcode='" + UnitCode + "'";
        }
      
        if (user != "" && user != null)
        {
            query += " and RecM.username='" + user + "'";
        }

        if (ModeOfPay != "All")
        {
            query += " and RecM.Paymenttype='" + ModeOfPay + "'";
        }
        query += "    GROUP by convert(varchar(20), RecM.tdate,103)+' '+convert(varchar(20),convert(time,  RecM.tdate),100) , Patmst.TestCharges , "+
                      "  RecM.username, patmst.PatRegID, patmst.Patphoneno, RecM.BillNo,     RecM.Paymenttype,  patmst.intial , Patmst.Patname ,  "+
                      "  RecM.TaxPer, ROUND(  RecM.TaxAmount,0) ,patmst.IsbillBH , RecM.Comment ";

        query += "   union all " +
                " SELECT     convert(varchar(20), RecM.tdate,103)+' '+convert(varchar(20),convert(time,  RecM.tdate),100) AS RecDate, -Patmst.TestCharges as NetPayment, RecM.username, "+
               " patmst.PatRegID, patmst.Patphoneno, RecM.BillNo,  -sum( RecM.AmtPaid) as AmtReceived, RecM.Paymenttype,-sum( RecM.DisAmt) as Discount, "+
               " patmst.intial + ' ' + Patmst.Patname AS Patname, sum( RecM.AmtPaid) -sum(RecM.refundamt)as AmtPaid, 0 as Balance,   RecM.TaxPer, ROUND(  RecM.TaxAmount,0) as TaxAmount, "+
               " CASE WHEN patmst.IsbillBH = 0 THEN Patmst.TestCharges -(sum( RecM.AmtPaid)+sum( RecM.DisAmt)) ELSE 0 END AS Mainbalance,  CASE WHEN patmst.IsbillBH = 1 THEN Patmst.TestCharges -(sum( RecM.AmtPaid)+sum( RecM.DisAmt))  ELSE 0 END AS BTHbalance ,  "+
               " RecM.Comment  "+
               " FROM         RecM INNER JOIN  patmst ON RecM.PID = patmst.PID " +
           " where   RecM.AmtPaid<0 and     patmst. IsActive=0 and (CAST(CAST(YEAR(RecM.transdate) AS varchar(4)) + '/' + CAST(MONTH(RecM.transdate) AS varchar(2)) + '/' + CAST(DAY(RecM.transdate) AS varchar(2)) AS datetime))  between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and RecM.branchid=" + branchid + "  ";
        //if (DigModule != 0)
        //{
        //    query += " and DigModule=" + DigModule + "";
        //}
        if (Centercode != "" && Centercode != "0" && Centercode != null)
        {
            query += " and Patmst.CenterCode='" + Centercode + "'";
        }
        if (UnitCode != "" && UnitCode != null)
        {
            query += " and patmst.Unitcode='" + UnitCode + "'";
        }

        if (user != "" && user != null)
        {
            query += " and RecM.username='" + user + "'";
        }
        if (ModeOfPay != "All")
        {
            query += " and RecM.Paymenttype='" + ModeOfPay + "'";
        }
        query += "    GROUP BY convert(varchar(20), RecM.tdate,103)+' '+convert(varchar(20),convert(time,  RecM.tdate),100) , Patmst.TestCharges , "+
               " RecM.username, patmst.PatRegID, patmst.Patphoneno, RecM.BillNo,     RecM.Paymenttype,  patmst.intial , Patmst.Patname ,  "+
               " RecM.TaxPer, ROUND(  RecM.TaxAmount,0) ,patmst.IsbillBH , RecM.Comment ";

        da = new SqlDataAdapter(query, conn);
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


    public static DataSet Get_DPRRepotData(object tdate, object fdate, string user, string Centercode, int branchid, int DigModule, string UnitCode)//
    {
      
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
               string query = "SELECT        CONVERT(char(12), RecM.billdate, 105) AS RecDate, patmst.TestCharges AS NetPayment, RecM.username, patmst.PatRegID, patmst.Patphoneno, RecM.BillNo, SUM(RecM.AmtPaid) AS AmtReceived, RecM.PaymentType, "+
                              "  SUM(RecM.DisAmt) AS Discount, patmst.intial + ' ' + patmst.Patname AS Patname, SUM(RecM.AmtPaid) AS AmtPaid, patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) AS Balance, RecM.TaxPer,  "+
                              "  ROUND(RecM.TaxAmount, 0) AS TaxAmount, CASE WHEN patmst.IsbillBH = 0 THEN patmst.TestCharges - (dbo.FUN_GetReceiveAmt(1, RecM.PID)) ELSE 0 END AS Mainbalance,  " +
                              "  CASE WHEN patmst.IsbillBH = 1 THEN patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) ELSE 0 END AS BTHbalance, RecM.Comment "+
                              "  FROM            patmst INNER JOIN "+
                              "  RecM ON patmst.PID = RecM.PID "+
           " where   RecM.IsActive=1 and  (CAST(CAST(YEAR(RecM.billdate) AS varchar(4)) + '/' + CAST(MONTH(RecM.billdate) AS varchar(2)) + '/' + CAST(DAY(RecM.billdate) AS varchar(2)) AS datetime)) between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and RecM.branchid=" + branchid + "";

       
        //if (DigModule != 0)
        //{
        //    query += " and DigModule=" + DigModule + "";
        //}
        if (Centercode != "" && Centercode != "0" && Centercode != null)
        {
            query += " and patmst.CenterCode='" + Centercode + "'";
        }
        if (UnitCode != "" && UnitCode != null)
        {
            query += " and patmst.UnitCode='" + UnitCode + "'";
        }
        if (user != "" && user != null)
        {
            query += " and patmst.CenterCode='" + user + "'";
        }
           query += " group by CONVERT(char(12), RecM.billdate, 105), patmst.TestCharges, RecM.username, patmst.PatRegID, patmst.Patphoneno, RecM.BillNo, RecM.PaymentType, RecM.DisAmt, patmst.intial, patmst.Patname, RecM.TaxPer, "+
                     " ROUND(RecM.TaxAmount, 0), patmst.IsbillBH, RecM.Comment,RecM.PID";

            query += "   union all "+
            " SELECT        CONVERT(char(12), RecM.billdate, 105) AS RecDate, patmst.TestCharges AS NetPayment, RecM.username, patmst.PatRegID, patmst.Patphoneno, RecM.BillNo, SUM(RecM.AmtPaid) AS AmtReceived, RecM.PaymentType, "+
             "   SUM(RecM.DisAmt) AS Discount, patmst.intial + ' ' + patmst.Patname AS Patname, SUM(RecM.AmtPaid) AS AmtPaid, patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) AS Balance, RecM.TaxPer, "+
             "   ROUND(RecM.TaxAmount, 0) AS TaxAmount, CASE WHEN patmst.IsbillBH = 0 THEN patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) ELSE 0 END AS Mainbalance, "+
             "   CASE WHEN patmst.IsbillBH = 1 THEN patmst.TestCharges - (SUM(RecM.AmtPaid) + SUM(RecM.DisAmt)) ELSE 0 END AS BTHbalance, RecM.Comment "+
             "   FROM            patmst INNER JOIN "+
             "   RecM ON patmst.PID = RecM.PID "+
            " where  RecM.IsActive=0 and  RecM.AmtPaid>0 and  (CAST(CAST(YEAR(RecM.billdate) AS varchar(4)) + '/' + CAST(MONTH(RecM.billdate) AS varchar(2)) + '/' + CAST(DAY(RecM.billdate) AS varchar(2)) AS datetime)) between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and RecM.branchid=" + branchid + "  ";//and  patmst.IsFreeze=1
        //if (DigModule != 0)
        //{
        //    query += " and DigModule=" + DigModule + "";
        //}
        if (Centercode != "" && Centercode != "0" && Centercode != null)
        {
            query += " and patmst.CenterCode='" + Centercode + "'";
        }
        if (UnitCode != "" && UnitCode != null)
        {
            query += " and patmst.UnitCode='" + UnitCode + "'";
        }
       
        if (user != "" && user != null)
        {
            query += " and patmst.CenterCode='" + user + "'";
        }

        query += " group by CONVERT(char(12), RecM.billdate, 105), patmst.TestCharges, RecM.username, patmst.PatRegID, patmst.Patphoneno, RecM.BillNo, RecM.PaymentType, RecM.DisAmt, patmst.intial, patmst.Patname, RecM.TaxPer, " +
                     " ROUND(RecM.TaxAmount, 0), patmst.IsbillBH, RecM.Comment";
        da = new SqlDataAdapter(query, conn); 
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

    public static DataSet GetDiscountwisereport(object tdate, object fdate, string user, string Name, int branchid, int DigModule, string UnitCode)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT        CONVERT(char(12), RecM.TransDate, 105) AS RecDate, Patmst.Testcharges as NetPayment, RecM.username, patmst.PatRegID, patmst.Patphoneno, RecM.BillNo, sum(RecM.Amtpaid) as AmtReceived, RecM.Paymenttype, RecM.Disamt as Discount,  " +
                       " patmst.intial + ' ' + patmst.Patname AS Patname,sum( RecM.AmtPaid) as AmtPaid, Patmst.Testcharges - (sum( RecM.AmtPaid)+sum( RecM.Disamt))as Balance, patmst.Drname, CAST(patmst.Age AS varchar(255)) + ' ' + patmst.MDY + '/' + patmst.sex AS Page, RecM.Comment, SUM(RecM.LabGiven)  " +
                       " AS LabGiven, SUM(RecM.DrGiven) AS DrGiven " +
                       " FROM            patmst INNER JOIN " +
                       " RecM ON patmst.PID = RecM.PID " +
        " where  (CAST(CAST(YEAR(RecM.transdate) AS varchar(4)) + '/' + CAST(MONTH(RecM.transdate) AS varchar(2)) + '/' + CAST(DAY(RecM.transdate) AS varchar(2)) AS datetime)) between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and Recm.branchid=" + branchid + " and CAST( RecM.Disamt AS float)>0  ";
       if (Name != "" && Name != "0" && Name != null)
        {
            query += " and RecM.Comment like '%" + Name + "%'";
        }
       query += "  GROUP BY CONVERT(char(12),  RecM.TransDate, 105), Patmst.Testcharges, recm.username, patmst.PatRegID, patmst.Patphoneno, recm.BillNo,  RecM.Paymenttype,RecM.Disamt, " +
         " patmst.intial + ' ' + Patmst.Patname,   patmst.Drname, CAST(patmst.Age AS varchar(255)), patmst.MDY, patmst.sex, RecM.Comment  ";
       
        if (UnitCode != "" && UnitCode != null)
        {
            query += " and patmst.UnitCode='" + UnitCode + "'";
        }
        da = new SqlDataAdapter(query, conn);
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
    public static DataSet getLoginfoMainData_saleregister(object tdate, object fdate, string user, string Centercode, int branchid, int DigModule, string UnitCode, string PatRegID, string Doctorcode, string Center)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT convert(char(12),Cshmst.RecDate,105) as RecDate,Cshmst.NetPayment,Cshmst.username ," +
          "  patmst.PatRegID, patmst.Patphoneno, Cshmst.BillNo, Cshmst.AmtReceived, Cshmst.Paymenttype,  " +
            " Cshmst.Discount,  patmst.intial+' '+Cshmst.Patname as Patname, Cshmst.AmtPaid, Cshmst.Balance , Cshmst.TaxPer,ROUND(  Cshmst.TaxAmount,0) as TaxAmount , " +
      "  Cshmst.PID,     case when Cshmst.AmtPaid >0  then ((Cshmst.NetPayment -Cshmst.Discount)+ROUND(  Cshmst.TaxAmount,0) ) else Cshmst.AmtPaid end as NetAmount,  Doctorcode,DocName,Cshmst.Centercode,Centername ,CONVERT(varchar(15),CAST(patmst.RegistratonDateTime AS TIME),100)as BillTime" +
    
        " FROM  Cshmst INNER JOIN patmst ON Cshmst.PatRegID = patmst.PatRegID AND Cshmst.PID = patmst.PID " +
        " where Cshmst.RecDate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and Cshmst.branchid=" + branchid + "";

        if (DigModule != 0)
        {
            query += " and DigModule=" + DigModule + "";
        }
        if (Centercode != "" && Centercode != "0" && Centercode != null)
        {
            query += " and Cshmst.CenterCode='" + Centercode + "'";
        }
        if (UnitCode != "" && UnitCode != null)
        {
            query += " and patmst.UnitCode='" + UnitCode + "'";
        }
        if (user != "" && user != null)
        {
            query += " and Cshmst.username='" + user + "'";
        }
        if (PatRegID != "" && PatRegID != null)
        {
            query += " and patmst.PatRegID='" + PatRegID + "'";
        }
        if (Doctorcode != "" && Doctorcode != null)
        {
            query += " and Doctorcode='" + Doctorcode + "'";
        }
        if (Center != "" && Center != null)
        {
            query += " and Centername='" + Center + "'";
        }
        da = new SqlDataAdapter(query, conn);
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

    public static DataSet getLoginfoMainData_sale_summary(object tdate, object fdate, string user, string Centercode, int branchid, int DigModule, string UnitCode)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        //string query = "SELECT     COUNT(RecDate) AS TotInv,dbo.FUN_GetInvoicenumber(Branchid,0,RecDate)as InvoiceNo, CONVERT(char(12), RecDate, 105) AS RecDate,sum( NetPayment)as amount, " +
        //   " sum(CAST(Discount AS float))as Discount, " +
        //   " sum(NetPayment) - (sum(CAST(Discount AS float))) AS Taxable,Round( sum(TaxAmount),2) AS Tax, " +
        //   " Round( sum(NetPayment) - (sum(CAST(Discount AS float))) + sum(TaxAmount),0) AS Net   from Cshmst" +
        //" where  IsActive=1 and Cshmst.RecDate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and Cshmst.branchid=" + branchid + "";
        string query = "SELECT COUNT(Phrecdate) AS TotInv,dbo.FUN_GetInvoicenumber(Branchid,0,Phrecdate)as InvoiceNo, "+
      "  CONVERT(char(12), Phrecdate, 105) AS RecDate,round(sum(Testcharges),0) as amount, " +
      "  sum(CAST(Discount AS float))as Discount, round(sum(Testcharges)-sum(CAST(Discount AS float)),2) as Taxable ,Round( sum(TaxAmount),2) AS Tax,  "+
      "  round(sum(Testcharges)-sum(CAST(Discount AS float))+sum(TaxAmount),0) as Net,' 'as InvType "+

       " from VW_csmst1vw where  IsActive=1 and Phrecdate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' and branchid=" + branchid + "";
       
        if (Centercode != "" && Centercode != "0" && Centercode != null)
        {
            query += " and CenterCode='" + Centercode + "'";
        }
        if (UnitCode != "" && UnitCode != null)
        {
            query += " and UnitCode='" + UnitCode + "'";
        }
        query += " group by Phrecdate ,Branchid ";
        da = new SqlDataAdapter(query, conn);
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
    public bool Insertcashierreport()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[sp_Insertcashierreport]";
        

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
    public bool Insertcashierreport_ref()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[sp_Insertcashierreport_new]";


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

    public bool Truncatecashierreport()
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[sp_truncatecashierreport]";


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

    public static DataSet GetMAterializeView_data(object tdate, object fdate, string Name, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT * from tbl_MaterializeView " +

             " where Bill_Date between '" + tdate + "' and '" + fdate + "'  ";

       
        if (Name != "" && Name != "0" && Name != null)
        {
            query += " and Customer_Name='" + Name + "'";
        }
       
        da = new SqlDataAdapter(query, conn);
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

    public static DataTable GetNepalie_data(object Fdate, object tdate)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "select bs_date from calendar " +

        " where ad_date = '" + Convert.ToDateTime(Fdate).ToString("MM/dd/yyyy") + "'  " +   
        " union all "+
        " select bs_date from calendar where ad_date =  '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "'  ";
       

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
    public static DataSet getLoginfoMainData_ActivityLog(object tdate, object fdate, string user, string Centercode, int branchid, int DigModule, string UnitCode)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
       
        
string query = " SELECT Count(PatRegID)as NoofPAtient, round((sum(Testcharges)-sum(CAST(Discount AS float)))+sum(TaxAmount),0) as NetAmount,sum(AmtPaid) as NetCashCollection, "+
          "  Username ,'YES' as BillIssue,0 saleReturnNo,0 SaleReturnAmount,CONVERT(date, Patregdate) as Entrydate, "+
          "  dbo.fun_Login_date_Time(CONVERT(date, Patregdate),Username)as LoginTime "+
          "  from VW_csmst1vw " + //--where IsActive=1
           " where  Patregdate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' "+
          "  group by Username,CONVERT(date, Patregdate) "+
          "  union all "+
          "  select COUNT(PID)as NoofPAtient,  round((sum(BillAmt)-sum(DisAmt))+sum(TaxAmount),0) as NetAmount "+

          "  ,0 as NetCashCollection,username, "+
          "  'YES' as BillIssue,CancelReceiptNo as saleReturnNo,sum(AmtPaid) as SaleReturnAmount,CONVERT(date, RecDate) as Entrydate "+
          "  ,dbo.fun_Login_date_Time(CONVERT(date, RecDate),Username)as LoginTime "+
          "   from SaleCancelDetails "+
         " where  RecDate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "' "+
        " group by username,CancelReceiptNo,CONVERT(date, RecDate) ";
        da = new SqlDataAdapter(query, conn);
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
   
    public bool Insert_Update_Barcode_Patmstd(string BarcodeID, int PID, int branchid, string STCODE,string PatRegID,string FID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_update_BarCode_Patmstd]";
       
        sc.Parameters.Add(new SqlParameter("@BarcodeID", SqlDbType.NVarChar, 250)).Value = BarcodeID;
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 1000)).Value = STCODE;
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 1000)).Value = PatRegID;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar, 1000)).Value = FID;


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
    public bool Insert_Update_Barcode_PhlebotomistReq(string BarcodeID, int PID, int branchid, string STCODE)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_update_BarCode_PhlebotomistReq]";
       
        sc.Parameters.Add(new SqlParameter("@BarcodeID", SqlDbType.NVarChar, 250)).Value = BarcodeID;
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@STCODE", SqlDbType.NVarChar, 1000)).Value = STCODE;


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

    public static DataSet getLoginfoDataForsummaryRemBalance_Center_Report(object fdate, object tdate, string user, int branchid, int DigModule, string Username)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

       // SqlDataAdapter da = new SqlDataAdapter("SELECT distinct patmstd.Printedby as username,convert(char(12),RecDate,105) as RecDate,balance,Cshmst.PatRegID,Patmst.Patname,( Cshmst.NetPayment)as NetPayment,AmtPaid , TaxAmount, TaxPer ,  Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount) as Mainbalance, case when patmst.IsbillBH=1 then Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount ) else 0 end as BTHbalance,  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, patmst.DrName, patmst.Centername, patmst.Remark from  Cshmst INNER JOIN patmst ON Cshmst.PID = patmst.PID INNER JOIN patmstd ON patmst.PID = patmstd.PID AND patmst.FID = patmstd.FID AND patmst.PatRegID = patmstd.PatRegID WHERE       patmstd.Printedby!='' and  patmst.isactive=1  and RecDate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).AddDays(+1).ToString("MM/dd/yyyy") + "' and  Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount)>0 and Cshmst.branchid=" + branchid + "   order by patmstd.Printedby", conn);//and DigModule=" + DigModule + " and Cshmst.username ='" + Username + "'
        SqlDataAdapter da = new SqlDataAdapter("SELECT distinct patmstd.Printedby as username,convert(char(12),Transdate,105) as RecDate,0 as balance,Patmst.PatRegID,Patmst.Patname,( patmst.TestCharges)as NetPayment,AmtPaid , "+
                       " TaxAmount, TaxPer ,  patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt)) as Mainbalance, "+
                       " case when patmst.IsbillBH=1 then patmst.TestCharges- (sum(RecM.AmtPaid) + sum(RecM.DisAmt)) else 0 end as BTHbalance,  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, "+
                       " patmst.DrName, patmst.Centername, patmst.Remark  "+
                       " from  RecM INNER JOIN patmst ON RecM.PID = patmst.PID INNER JOIN patmstd ON patmst.PID = patmstd.PID  "+
                       " AND patmst.FID = patmstd.FID AND patmst.PatRegID = patmstd.PatRegID "+
                       " group by "+
                       " patmstd.Printedby ,convert(char(12),Transdate,105) ,Patmst.PatRegID,Patmst.Patname,( patmst.TestCharges),AmtPaid , "+
                       " TaxAmount, TaxPer ,  patmst.TestCharges , "+
                       " patmst.RefDr,patmst.Patphoneno, patmst.Centercode,  "+
                       " patmst.DrName, patmst.Centername, patmst.Remark ,patmst.isactive,RecM.branchid,patmst.IsbillBH, "+
                       " (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime)) having       patmstd.Printedby!='' and  patmst.isactive=1  and (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime)) between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).AddDays(+1).ToString("MM/dd/yyyy") + "' and  patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt))>0  and RecM.branchid=" + branchid + "   order by patmstd.Printedby", conn);//and DigModule=" + DigModule + " and Cshmst.username ='" + Username + "'

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
    public static DataSet getLoginfoDataForsummaryRemBalance_Center_outs_report(object fdate, object tdate, string user, int branchid, int DigModule, string Username, string CenterN)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

       // SqlDataAdapter da = new SqlDataAdapter("SELECT distinct patmstd.Printedby as username,convert(char(12),RecDate,105) as RecDate,balance,Cshmst.PatRegID,Patname,( Cshmst.NetPayment)as NetPayment,AmtPaid , TaxAmount, TaxPer ,  Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount) as Mainbalance, case when patmst.IsbillBH=1 then Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount ) else 0 end as BTHbalance,  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, patmst.Drname, patmst.Centername, patmst.Comment from  Cshmst INNER JOIN patmst ON Cshmst.PID = patmst.PID INNER JOIN patmstd ON patmst.PID = patmstd.PID AND patmst.FID = patmstd.FID AND patmst.PatRegID = patmstd.PatRegID WHERE       patmstd.Printedby!='' and  patmst.isactive=1  and RecDate between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).AddDays(+1).ToString("MM/dd/yyyy") + "' and Cshmst.NetPayment - (Cshmst.AmtPaid + Cshmst.Discount)>0 and Cshmst.branchid=" + branchid + "  and   patmst.Centercode ='" + CenterN + "' order by patmstd.Printedby", conn);//and DigModule=" + DigModule + " and Cshmst.username ='" + Username + "'
        SqlDataAdapter da = new SqlDataAdapter("SELECT distinct patmstd.Printedby as username,convert(char(12),Transdate,105) as RecDate,0 as balance,Patmst.PatRegID,Patmst.Patname,( patmst.TestCharges)as NetPayment,AmtPaid , " +
                        " TaxAmount, TaxPer ,  patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt)) as Mainbalance, " +
                        " case when patmst.IsbillBH=1 then patmst.TestCharges- (sum(RecM.AmtPaid) + sum(RecM.DisAmt)) else 0 end as BTHbalance,  patmst.RefDr,patmst.Patphoneno, patmst.Centercode, " +
                        " patmst.DrName, patmst.Centername, patmst.Remark  " +
                        " from  RecM INNER JOIN patmst ON RecM.PID = patmst.PID INNER JOIN patmstd ON patmst.PID = patmstd.PID  " +
                        " AND patmst.FID = patmstd.FID AND patmst.PatRegID = patmstd.PatRegID " +
                        " group by " +
                        " patmstd.Printedby ,convert(char(12),Transdate,105) ,Patmst.PatRegID,Patmst.Patname,( patmst.TestCharges),AmtPaid , " +
                        " TaxAmount, TaxPer ,  patmst.TestCharges , " +
                        " patmst.RefDr,patmst.Patphoneno, patmst.Centercode,  " +
                        " patmst.DrName, patmst.Centername, patmst.Remark ,patmst.isactive,RecM.branchid,patmst.IsbillBH, " +
                        " (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime)) having       patmstd.Printedby!='' and  patmst.isactive=1  and (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime)) between '" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "' and '" + Convert.ToDateTime(tdate).AddDays(+1).ToString("MM/dd/yyyy") + "' and  patmst.TestCharges - (sum(RecM.AmtPaid) + sum(RecM.DisAmt))>0  and patmst.Centercode ='" + CenterN + "' and RecM.branchid=" + branchid + "   order by patmstd.Printedby", conn);//and DigModule=" + DigModule + " and Cshmst.username ='" + Username + "'

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



    public static DataSet Get_ActivityDetailsReport(object tdate, object fdate, string user, string Name, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT       ActivityDetails.Id, ActivityDetails.Patregno, ActivityDetails.FormName, ActivityDetails.EventName, ActivityDetails.CreatedBy, ActivityDetails.CreatedOn, "+
                       " convert(varchar(20),ActivityDetails.CreatedOn,103)+' '+convert(varchar(20),convert(time,ActivityDetails.CreatedOn),100) as ActivityDateTime, ActivityDetails.Branchid, patmst.intial, patmst.Patname, patmst.sex, "+
                       " patmst.Age, patmst.Patregdate, patmst.Patphoneno, patmst.Isemergency, patmst.Reportdate, patmst.RefDr, patmst.MDY, patmst.DoctorCode, patmst.CenterCode, patmst.CenterName, patmst.RegistratonDateTime, "+
                       " patmst.TelNo,patmst.intial+' '+ patmst.Patname as PatientName " +
                       " FROM            ActivityDetails INNER JOIN "+
                       " patmst ON ActivityDetails.Patregno = patmst.PatRegID  where    (CAST(CAST(YEAR( ActivityDetails.CreatedOn) AS varchar(4)) + '/' + CAST(MONTH( ActivityDetails.CreatedOn) AS varchar(2)) + '/' + CAST(DAY(ActivityDetails.CreatedOn) AS varchar(2)) AS datetime)) between ('" + Convert.ToDateTime(fdate).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(tdate).ToString("MM/dd/yyyy") + "')";
        if (Name != "" && Name != "0" && Name != null)
        {
            query += " and ActivityDetails.Patregno= '" + Name + "'";
        }
        if (user != "Select UserName" && user != "0" && user != null)
        {
            query += " and  ActivityDetails.CreatedBy= '" + user + "'";
        }
        da = new SqlDataAdapter(query, conn);
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


}
