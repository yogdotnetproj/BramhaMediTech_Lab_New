using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;


public class Ledgrmst_Bal_C
{
    public Ledgrmst_Bal_C()
    {
        
        this.CreditAmt = 0;
        this.DebitAmt = 0;
        this.ParticularField = "";
        this.CenterCode = "";
        this.BillFormat = "";
             
    }    
    public bool UpdateBillTransDis(int bno, int branchid, string Uname)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update RecM " +
            "set DisAmt=@DisAmt  where BillNo=@BillNo  and branchid=" + branchid + "  ", conn);//and username ='" + Uname + "'

        // Add the employee ID parameter and set its value.

        sc.Parameters.Add(new SqlParameter("@DisAmt", SqlDbType.Float)).Value = 0;
        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;

        conn.Close();

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
    } //update End

    public bool UpdateBillTransBal(int bno, int branchid, string Uname,string FID)
    {       
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update RecM " +
            "set BalAmt=@BalAmt where BillNo=@BillNo  and branchid=" + branchid + " and FID='" + FID + "'  ", conn);

        sc.Parameters.Add(new SqlParameter("@BalAmt", SqlDbType.Float)).Value = 0;      
        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;

        conn.Close();

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
    } //update End

    public bool UpadateBillTransBillAmt(int bno, int branchid, string Uname, float BillAmt)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update RecM " +
            "set BillAmt=@BillAmt where BillNo=@BillNo  and branchid=" + branchid + "  ", conn);//,BillAmt=@BillAmt
        sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = BillAmt;
        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;
        conn.Close();
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
    } //update End

    public float GetAmtReceived(int bno, int branchid,string FID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        float PrevBillAmt=0;
        object ob = null;
        SqlCommand sc = new SqlCommand("" +
            "select  sum(AmtPaid) as AmtReceived from RecM" +
            " where BillNo=@BillNo  and branchid=" + branchid + " and FID='"+FID+"'  ", conn);
        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;
        conn.Close();
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            ob=sc.ExecuteScalar();
            if(ob!=DBNull.Value)
            {
                PrevBillAmt=Convert.ToSingle(ob);
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
        // Implement Update logic.
        return PrevBillAmt;
    } //update End

    public float GetOtherCharges(int bno, int branchid,string FID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        float PrevotherCharges = 0;
        object ob = null;
        SqlCommand sc = new SqlCommand("" +
            "select  sum( otherCharges) as otherCharges from recm" +
            " where BillNo=@BillNo  and branchid=" + branchid + " and FID='"+FID+"'  ", conn);
        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;
        conn.Close();
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            ob = sc.ExecuteScalar();
            if (ob != DBNull.Value)
            {
                PrevotherCharges = Convert.ToSingle(ob);
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
        // Implement Update logic.
        return PrevotherCharges;
    } //update End
    public void readdate(int billno2, int branchid, string currdate)
    {

        SqlConnection conn = DataAccess.ConInitForDC();

        try
        {
            SqlCommand cmd = new SqlCommand("SP_phcentlegwise", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@billno", billno2);
            cmd.Parameters.AddWithValue("@branchid", branchid);
            cmd.Parameters.AddWithValue("@regdate", Convert.ToDateTime(currdate));
            // SqlDataReader dr;
            conn.Open();
            this.Count1 = Convert.ToInt32(cmd.ExecuteScalar());


        }
        catch (Exception ex)
        {
        }
        finally
        {
            conn.Close();
            conn.Dispose();
        }


    }
 
    public DataTable GetAmountPaid(int PID, int branchid,int BillNo)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select isnull(sum(Amtpaid),0)as Amtpaid,isnull(sum(BillAmt),0) as BillAmt,isnull(sum(BillAmt),0)-(isnull(sum(Amtpaid),0)+isnull(SUM(Disamt),0))as ActualBal,ReceiptNo from RecM where PID=" + PID + " and RecM.branchid=" + branchid + " and Billno=" + BillNo + " GROUP BY ReceiptNo ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception ex) { throw; }
        finally { conn.Close(); conn.Dispose(); }
        return dt;
    }


    public bool UpdateBillPreviewBal(int bno, int branchid, string Uname,string FID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update RecM " +
            "set PrevBal=@PrevBal,BalAmt=0 where BillNo=@BillNo  and branchid=" + branchid + " and FID='" + FID + "'  ", conn);

        sc.Parameters.Add(new SqlParameter("@PrevBal", SqlDbType.Float)).Value = 0;
        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;

        conn.Close();

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
    } //update End

    public bool UpdateBillPreviewBal_Editdemo11(int bno, int branchid, string Uname, float BillAmt)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update top(1) RecM " +
            "set PrevBal=@PrevBal,BalAmt=0, BillAmt=@BillAmt where BillNo=@BillNo  and branchid=" + branchid + "  ", conn);

        sc.Parameters.Add(new SqlParameter("@PrevBal", SqlDbType.Float)).Value = 0;
        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;
        sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = BillAmt;

        conn.Close();

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
    } //update End
    public bool UpdateBillPreviewBal_Editdemo(int bno, int branchid, string Uname, float BillAmt)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandText = "[sp_UpdateBillPreviewBal_Editdemo]";

        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;
        sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = BillAmt;
        try
        {
            conn.Open();
            sc.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
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
        return true;
    }


    public DataTable GetOther_Charges(int PID, int branchid)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select sum(isnull(RecM.OtherCharges,0)) as Othercharges from RecM where PID=" + PID + " and branchid=" + branchid + "  ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception ex) { throw; }
        finally { conn.Close(); conn.Dispose(); }
        return dt;
    }


    public bool Update_OtherCharges(int PID, int branchid, string Uname, string FID, float OtherCharges, string OtherChargeRemark)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("" +
            "Update top(1) RecM " +
            "set OtherCharges=@OtherCharges,OtherChargeRemark=@OtherChargeRemark where PID=@PID  and branchid=" + branchid + " and FID='" + FID + "'  ", conn);

        sc.Parameters.Add(new SqlParameter("@OtherCharges", SqlDbType.Float)).Value = OtherCharges;
        sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int)).Value = PID;

        sc.Parameters.Add(new SqlParameter("@OtherChargeRemark", SqlDbType.NVarChar, 250)).Value = OtherChargeRemark;

        conn.Close();

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
    } //update End



    #region Properties

    private int ledgerId;
    public int LedgerId
    {
        get { return ledgerId; }
        set { ledgerId = value; }
    }

    private string centercode;
    public string CenterCode
    {
        get { return centercode; }
        set { centercode = value; }
    }
    private string ReceiptNO;
    public string P_ReceiptNO
    {
        get { return ReceiptNO; }
        set { ReceiptNO = value; }
    }
    private string particularField;
     public string ParticularField
    {
        get { return particularField; }
        set { particularField = value; }
    }
    
    private DateTime regDate;
    public DateTime RegDate
    {
        get { return regDate; }
        set { regDate = value; }
    }

   

    private float creditAmt;
    public float CreditAmt
    {
        get { return creditAmt; }
        set { creditAmt = value; }
    }

    private float debitAmt;
    public float DebitAmt
    {
        get { return debitAmt; }
        set { debitAmt = value; }
    }

    private int billNo;
    public int BillNo
    {
        get { return billNo; }
        set { billNo = value; }
    }

    private string  billformat;
    public string  BillFormat
    {
        get { return billformat; }
        set { billformat = value; }
    }

    private string _ModeOfPayment;
    public string ModeOfPayment
    {
        get { return _ModeOfPayment; }
        set { _ModeOfPayment = value; }
    }

    private string _regno;
    public string Regno
    {
        get { return _regno; }
        set { _regno = value; }
    }

    private int _PID;
    public int PID
    {
        get { return _PID; }
        set { _PID = value; }
    }

    private int Count;

    public int Count1
    {
        get { return Count; }
        set { Count = value; }
    }

    private string _FirstName;
    public string Patname
    {
        get { return _FirstName; }
        set { _FirstName = value; }
    }
    private string _EUserName;
    public string EUserName
    {
        get { return _EUserName; }
        set { _EUserName = value; }
    }
    #endregion
}
