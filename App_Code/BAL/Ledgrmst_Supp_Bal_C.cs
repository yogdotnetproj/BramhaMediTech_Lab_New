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


public class Ledgrmst_Supp_Bal_C
{
    public Ledgrmst_Supp_Bal_C()
    {
        //
        // TODO: Add constructor logic here
        //

    }  
    public static float getOpeningBalance(DateTime dfrom, string CenterCode, int branchid)
    {
        float totamt = 0f;
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = null;
        sc = new SqlCommand(" SELECT   isnull( case when  sum(DebitAmt)- sum(CreditAmt) <0 then sum(DebitAmt) else sum(DebitAmt)- sum(CreditAmt) end,0) FROM         VW_openingbal", conn);
       
        sc.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime)).Value = dfrom.Date;
        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 50)).Value = CenterCode;

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
        //tl.Sort();
        return totamt;
    }
    public static ICollection getLedgerTransactionByFromTo_Led(DateTime dfrom, DateTime dto, string CenterCode, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlCommand sc = null;
        SqlConnection conn = DataAccess.ConInitForDC();
        if (CenterCode != "Select ")
        {
            sc = new SqlCommand("select * from VW_FinalLedger order by CONVERT(datetime, RegDate, 103)  ", conn);

        }
        else
        {
            sc = new SqlCommand("select * from VW_FinalLedger order by CONVERT(datetime, RegDate, 103) ", conn);

        }

        sc.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime)).Value = dfrom.Date;
        sc.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime)).Value = dto.Date;
        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 50)).Value = CenterCode;

        SqlDataReader sdr = null;

        Ledgrmst_Bal_C LBC = new Ledgrmst_Bal_C();
        LBC.CenterCode = "";
        LBC.RegDate = Date.getMinDate();
        LBC.ParticularField = "Opening Balance";
        float amt1 = Ledgrmst_Supp_Bal_C.getOpeningBalance(dfrom.Date, CenterCode, branchid);

        if (amt1 >= 0)
        {
            LBC.DebitAmt = amt1;
            LBC.CreditAmt = 0;
        }
        else
        {
            LBC.CreditAmt = amt1;
            LBC.DebitAmt = 0;
        }

        al.Add(LBC);

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            // This is not a while loop. It only loops once.
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Ledgrmst_Bal_C LB = new Ledgrmst_Bal_C();
                    LB.CenterCode = Convert.ToString(sdr["CenterCode"]);
                    LB.RegDate = Convert.ToDateTime(sdr["RegDate"]);
                    LB.ParticularField = Convert.ToString(sdr["ParticularField"]);
                    LB.CreditAmt = Convert.ToSingle(sdr["CreditAmt"]);
                    LB.DebitAmt = Convert.ToSingle(sdr["DebitAmt"]);
                    LB.ModeOfPayment = sdr["ModeOfPayment"].ToString();
                    LB.BillNo = Convert.ToInt32(sdr["BillNo"]);
                    LB.Regno = Convert.ToString(sdr["PatRegID"]);
                    LB.Patname = Convert.ToString(sdr["FirstName"]);
                    LB.EUserName = Convert.ToString(sdr["EntryuserName"]);
                    al.Add(LB);
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
        Ledgrmst_Bal_C LegB = new Ledgrmst_Bal_C();
        LegB.CenterCode = "zzzzzzzz";
        LegB.RegDate = Date.getMinDate();
        LegB.ParticularField = "Closing Balance";
        LegB.CreditAmt = 0;
        LegB.DebitAmt = 0;
        al.Add(LegB);
        return al;
    }

    public static ICollection getLedgerTransactionByFromTo(DateTime dfrom, DateTime dto, string CenterCode, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlCommand sc = null;
        SqlConnection conn = DataAccess.ConInitForDC();
         if (CenterCode != "Select ")
        {
            sc = new SqlCommand("SELECT  convert(char(12),Patregdate,105)as RegDate,Centercode,centername as CenterCode,SUM( TestCharges)as DebitAmt,0 as CreditAmt,testname as ParticularField ,'' as ModeOfPayment,RegNo,BillNo, Patname ,VW_csmst1vw.username AS EntryuserName  from VW_csmst1vw where RegNo<>'' and Monthlybill=1   " + // Monthlybill=1  and
                 " and  (CAST(CAST(YEAR( Patregdate) AS varchar(4)) + '/' + CAST(MONTH( Patregdate) AS varchar(2)) + '/' + CAST(DAY( Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(dfrom).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(dto).ToString("MM/dd/yyyy") + "') and  centername =@CenterCode  "+            
                " group by Patregdate,Centercode,centername ,testname,RegNo,BillNo ,Patname,VW_csmst1vw.username " +
                              "  union all " +
                               " select convert(char(12),Receivedate,105) as RegDate,''as Centercode,Centercode as CenterCode, 0 as DebitAmt, sum(Receiveamount) as CreditAmt ,'' as ParticularField ,Paymenttype as ModeOfPayment ,0 as Regno,0 as BillNo,''as Patname,username AS EntryuserName from CPReceive " +
                            "  where  (CAST(CAST(YEAR( Receivedate) AS varchar(4)) + '/' + CAST(MONTH( Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(dfrom).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(dto).ToString("MM/dd/yyyy") + "')  and  Centercode =@CenterCode "+
                               " group by Receivedate,Centercode,Paymenttype,username    ", conn);

        }
        else
        {
            sc = new SqlCommand("SELECT  convert(char(12),Patregdate,105)as RegDate,Centercode,centername as CenterCode,SUM( TestCharges)as DebitAmt,0 as CreditAmt,testname as ParticularField ,'' as ModeOfPayment ,RegNo,BillNo,Patname, VW_csmst1vw.username AS EntryuserName from VW_csmst1vw where  RegNo<>''  and Monthlybill=1  " + //Monthlybill=1  and
                "  and  (CAST(CAST(YEAR( Patregdate) AS varchar(4)) + '/' + CAST(MONTH( Patregdate) AS varchar(2)) + '/' + CAST(DAY( Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(dfrom).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(dto).ToString("MM/dd/yyyy") + "')   "+            
                " group by Patregdate,Centercode,centername,testname ,RegNo,BillNo,Patname,VW_csmst1vw.username " +
                             "  union all " +
                              " select convert(char(12),Receivedate,105) as RegDate,''as Centercode,Centercode as CenterCode, 0 as DebitAmt, sum(Receiveamount) as CreditAmt ,'' as ParticularField ,Paymenttype as ModeOfPayment ,0 as Regno,0 as BillNo,'' as Patname,username AS EntryuserName from CPReceive " +
                              " group by Receivedate,Centercode,Paymenttype,username  having  (CAST(CAST(YEAR( Receivedate) AS varchar(4)) + '/' + CAST(MONTH( Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(dfrom).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(dto).ToString("MM/dd/yyyy") + "')   ", conn);

        }
       
        sc.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime)).Value = dfrom.Date;
        sc.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime)).Value = dto.Date;
        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar,50)).Value = CenterCode;

        SqlDataReader sdr = null;

        Ledgrmst_Bal_C LBC = new Ledgrmst_Bal_C();
        LBC.CenterCode = "";
        LBC.RegDate =Date.getMinDate();
        LBC.ParticularField = "Opening Balance";
        float amt1 = Ledgrmst_Supp_Bal_C.getOpeningBalance(dfrom.Date,CenterCode,branchid);
        
        if (amt1 >=0)
        {
            LBC.DebitAmt = amt1;
            LBC.CreditAmt = 0;
        }
        else
        {
            LBC.CreditAmt =amt1;
            LBC.DebitAmt = 0;
        }
        
        al.Add(LBC);

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            // This is not a while loop. It only loops once.
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Ledgrmst_Bal_C LB = new Ledgrmst_Bal_C();
                    LB.CenterCode = Convert.ToString(sdr["CenterCode"]);
                    LB.RegDate = Convert.ToDateTime(sdr["RegDate"]);
                    LB.ParticularField = Convert.ToString(sdr["ParticularField"]);
                    LB.CreditAmt = Convert.ToSingle(sdr["CreditAmt"]);
                    LB.DebitAmt = Convert.ToSingle(sdr["DebitAmt"]);
                    LB.ModeOfPayment = sdr["ModeOfPayment"].ToString();
                    LB.BillNo =Convert.ToInt32(sdr["BillNo"]);
                    LB.Regno = Convert.ToString(sdr["Regno"]);
                    LB.Patname = Convert.ToString(sdr["Patname"]);
                    LB.EUserName = Convert.ToString(sdr["EntryuserName"]);
                    al.Add(LB);
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
        Ledgrmst_Bal_C LegB = new Ledgrmst_Bal_C();
        LegB.CenterCode = "zzzzzzzz";
        LegB.RegDate = Date.getMinDate();
        LegB.ParticularField = "Closing Balance";
        LegB.CreditAmt = 0;
        LegB.DebitAmt = 0;
        al.Add(LegB);
        return al;
    }
    public static ICollection getLedgerTransactionByFromTo_New(DateTime dfrom, DateTime dto, string CenterCode, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlCommand sc = null;
        SqlConnection conn = DataAccess.ConInitForDC();
       if (CenterCode != "Select ")
        {
            sc = new SqlCommand("SELECT  convert(char(12),Patregdate,105)as RegDate,Centercode,centername as CenterCode,SUM( TestCharges)as DebitAmt,0 as CreditAmt,testname as ParticularField ,'' as ModeOfPayment,RegNo,BillNo, Patname ,VW_csmst1vw.username AS EntryuserName  from VW_csmst1vw where RegNo<>'' and Monthlybill=1   " + // Monthlybill=1  and
                 " and  (CAST(CAST(YEAR( Patregdate) AS varchar(4)) + '/' + CAST(MONTH( Patregdate) AS varchar(2)) + '/' + CAST(DAY( Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(dfrom).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(dto).ToString("MM/dd/yyyy") + "') and  centername =@CenterCode  " +
                " group by Patregdate,Centercode,centername ,testname,RegNo,BillNo ,Patname,VW_csmst1vw.username " +
                              "  union all " +
                               " select convert(char(12),Receivedate,105) as RegDate,''as Centercode,Centercode as CenterCode, 0 as DebitAmt, sum(Receiveamount) as CreditAmt ,'' as ParticularField ,Paymenttype as ModeOfPayment ,0 as Regno,0 as BillNo,''as Patname,username AS EntryuserName from CPReceive " +
                            "  where  (CAST(CAST(YEAR( Receivedate) AS varchar(4)) + '/' + CAST(MONTH( Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(dfrom).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(dto).ToString("MM/dd/yyyy") + "')  and  Centercode =@CenterCode " +
                               " group by Receivedate,Centercode,Paymenttype,username    ", conn);

        }
        else
        {
            sc = new SqlCommand("SELECT  convert(char(12),Patregdate,105)as RegDate,Centercode,centername as CenterCode,SUM( TestCharges)as DebitAmt,0 as CreditAmt,testname as ParticularField ,'' as ModeOfPayment ,RegNo,BillNo,Patname, VW_csmst1vw.username AS EntryuserName from VW_csmst1vw where  RegNo<>''  and Monthlybill=1  " + //Monthlybill=1  and
                "  and  (CAST(CAST(YEAR( Patregdate) AS varchar(4)) + '/' + CAST(MONTH( Patregdate) AS varchar(2)) + '/' + CAST(DAY( Patregdate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(dfrom).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(dto).ToString("MM/dd/yyyy") + "')   " +
                " group by Patregdate,Centercode,centername,testname ,RegNo,BillNo,Patname,VW_csmst1vw.username " +
                             "  union all " +
                              " select convert(char(12),Receivedate,105) as RegDate,''as Centercode,Centercode as CenterCode, 0 as DebitAmt, sum(Receiveamount) as CreditAmt ,'' as ParticularField ,Paymenttype as ModeOfPayment ,0 as Regno,0 as BillNo,'' as Patname,username AS EntryuserName from CPReceive " +
                              " group by Receivedate,Centercode,Paymenttype,username  having  (CAST(CAST(YEAR( Receivedate) AS varchar(4)) + '/' + CAST(MONTH( Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(dfrom).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(dto).ToString("MM/dd/yyyy") + "')   ", conn);

        }

        sc.Parameters.Add(new SqlParameter("@fromdate", SqlDbType.DateTime)).Value = dfrom.Date;
        sc.Parameters.Add(new SqlParameter("@todate", SqlDbType.DateTime)).Value = dto.Date;
        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 50)).Value = CenterCode;

        SqlDataReader sdr = null;

        Ledgrmst_Bal_C LBC = new Ledgrmst_Bal_C();
        LBC.CenterCode = "";
        LBC.RegDate = Date.getMinDate();
        LBC.ParticularField = "Opening Balance";
        float amt1 = Ledgrmst_Supp_Bal_C.getOpeningBalance(dfrom.Date, CenterCode, branchid);

        if (amt1 >= 0)
        {
            LBC.DebitAmt = amt1;
            LBC.CreditAmt = 0;
        }
        else
        {
            LBC.CreditAmt = amt1;
            LBC.DebitAmt = 0;
        }

        al.Add(LBC);

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            // This is not a while loop. It only loops once.
            if (sdr != null)
            {
                while (sdr.Read())
                {
                    Ledgrmst_Bal_C LB = new Ledgrmst_Bal_C();
                    LB.CenterCode = Convert.ToString(sdr["CenterCode"]);
                    LB.RegDate = Convert.ToDateTime(sdr["RegDate"]);
                    LB.ParticularField = Convert.ToString(sdr["ParticularField"]);
                    LB.CreditAmt = Convert.ToSingle(sdr["CreditAmt"]);
                    LB.DebitAmt = Convert.ToSingle(sdr["DebitAmt"]);
                    LB.ModeOfPayment = sdr["ModeOfPayment"].ToString();
                    LB.BillNo = Convert.ToInt32(sdr["BillNo"]);
                    LB.Regno = Convert.ToString(sdr["Regno"]);
                    LB.Patname = Convert.ToString(sdr["Patname"]);
                    LB.EUserName = Convert.ToString(sdr["EntryuserName"]);
                    al.Add(LB);
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
        Ledgrmst_Bal_C LegB = new Ledgrmst_Bal_C();
        LegB.CenterCode = "zzzzzzzz";
        LegB.RegDate = Date.getMinDate();
        LegB.ParticularField = "Closing Balance";
        LegB.CreditAmt = 0;
        LegB.DebitAmt = 0;
        al.Add(LegB);
        return al;
    }


    public  DataTable getLedgerTransaction_summary()
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT     Centercode, sum(DebitAmt)as DebitAmt, sum(CreditAmt) as CreditAmt,sum(DebitAmt)-sum(CreditAmt) as BalanceAmt "+
           " FROM         VW_Ledgersummary_Report where DebitAmt>0 or CreditAmt>0 " +
           " group by centercode ";


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

    public DataTable GetCenterLedgerApprovePayment(DateTime dfrom, DateTime dto, string CenterCode, string PaymentApprove)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlDataAdapter da;
        string query = "SELECT     * FROM         VW_CenterPaymentReceiveCheck ";//where  (CAST(CAST(YEAR( Receivedate) AS varchar(4)) + '/' + CAST(MONTH( Receivedate) AS varchar(2)) + '/' + CAST(DAY(Receivedate) AS varchar(2)) AS datetime))  between ('" + Convert.ToDateTime(dfrom).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(dto).ToString("MM/dd/yyyy") + "')  and  Centercode ='" + CenterCode + "'  

        //if (PaymentApprove == "0" )
        //{
        //    query += " and IsPaymentApprove=0";
        //}
        //if (PaymentApprove == "1")
        //{
        //    query += " and IsPaymentApprove=1";
        //}

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

    public string Accept_RejetPayment(int ApproveId, int ApproveStatus, string ApproveRemark, int Branchid, string CreatedBy)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("SP_AcceptRejectCenterPayment", con);
        cmd.CommandType = CommandType.StoredProcedure;
        con.Open();
        try
        {

            cmd.Parameters.AddWithValue("@ApproveId", ApproveId);
            cmd.Parameters.AddWithValue("@ApproveStatus", ApproveStatus);

            cmd.Parameters.AddWithValue("@ApproveRemark", ApproveRemark);

            cmd.Parameters.AddWithValue("@Branchid", Branchid);
            cmd.Parameters.AddWithValue("@ApproveBy", CreatedBy);

            object Result = cmd.ExecuteScalar();
            return Convert.ToString(Result);
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
    }

}
