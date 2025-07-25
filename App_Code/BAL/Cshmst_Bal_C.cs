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


public class Cshmst_Bal_C
{
    
    public Cshmst_Bal_C()
    {
        this.P_Centercode = "";
        this.BillNo = 0;
        this.BillType = "";
        this.AmtReceived = 0;
        this.Paymenttype = "";
        this.BankName = "";
        this.Remark = "";
       
        this.Discount = "";
        this.NetPayment = 0F;
        this.patRegID = "";
        this.Patientname= "";
       
        this.Patienttest = "";
      
        this.ChqDate = Date.getMinDate();
        this.ExpiryDate = Date.getMinDate();
    }

    public Cshmst_Bal_C(int billno)
    {
        
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from Cshmst where billno=@billno", conn);
        sc.Parameters.Add(new SqlParameter("@billno", SqlDbType.Int)).Value = billno;
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {
                
                this.Patientname= sdr["Patname"].ToString();
                this.RecDate = Convert.ToDateTime(sdr["RecDate"]);
             
                this.AmtReceived = Convert.ToSingle(sdr["AmtReceived"]);
              
                this.NetPayment = Convert.ToSingle(sdr["NetPayment"]);
                this.Discount = sdr["Discount"].ToString();
                this.PID = Convert.ToInt32(sdr["PID"]);
                this.Remark = sdr["Comment"].ToString();
                this.Balance = Convert.ToSingle(sdr["Balance"]);
                this.AmtPaid = Convert.ToSingle(sdr["AmtPaid"]);
                this.P_Centercode = (sdr["CenterCode"].ToString());
                this.Paymenttype = sdr["Paymenttype"].ToString();
                this.BankName = sdr["BankName"].ToString();
                this.Othercharges = Convert.ToSingle(sdr["Othercharges"]);
                this.AccNo = sdr["AccNo"].ToString();
                
                this.Cardtype = sdr["Cardtype"].ToString();
                this.CardName = sdr["CardName"].ToString();
                this.CardNo = sdr["CardNo"].ToString();
                if (sdr["ChqDate"] != DBNull.Value)
                    this.ChqDate = Convert.ToDateTime(sdr["ChqDate"]);
                else
                    this.ChqDate = Date.getMinDate();
                this.ChqNo = sdr["ChqNo"].ToString();
                if (sdr["ExpiryDate"] != DBNull.Value)
                    this.ExpiryDate = Convert.ToDateTime(sdr["ExpiryDate"]);
                else
                    this.ExpiryDate = Date.getMinDate();
                this.City = sdr["City"].ToString();
                if (sdr["DisFlag"] != DBNull.Value)
                    this.DisFlag = Convert.ToBoolean(sdr["DisFlag"]);
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
        //return al;
    }

    public Cshmst_Bal_C(int billno, int branchid, string FID)
    {
       
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("SELECT     patmst.CenterCode,   RecM.PID, RecM.BillNo, 'Cash Bill' as BillType, patmst.Patregdate as RecDate, SUM(RecM.AmtPaid) AS AmtReceived,  patmst.TestCharges as NetPayment,sum( RecM.DisAmt) as Discount,  "+
              "  sum(isnull(RecM.OtherCharges,0)) as OtherCharges,  " +
              "  patmst.PatRegID, patmst.intial + ' ' + patmst.Patname AS Patname, patmst.DoctorCode,   patmst.sex, patmst.Age, patmst.MDY, patmst.RefDr, patmst.Patphoneno, patmst.Drname, "+
              "  patmst.TelNo,0 as Balance , '' as OtherchargeRemark " +
              "  FROM            RecM INNER JOIN "+
              "  patmst ON RecM.PID = patmst.PID where billno=@billno and patmst.FID=@FID and patmst.branchid=" + branchid + " GROUP BY patmst.Patregdate, patmst.sex, patmst.Age, patmst.MDY, patmst.RefDr, patmst.Patphoneno, patmst.Drname, patmst.TestCharges, patmst.TelNo,patmst.CenterCode,RecM.PID, RecM.BillNo " +
              "  ,patmst.PatRegID, patmst.intial , patmst.Patname,patmst.DoctorCode ", conn);
        sc.Parameters.Add(new SqlParameter("@billno", SqlDbType.Int)).Value = billno;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.NVarChar,(50))).Value = FID;
        SqlDataReader sdr = null;

        try  
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            if (sdr != null & sdr.Read())
            {
                if (sdr["Patname"] != DBNull.Value)
                    this.Patientname = sdr["Patname"].ToString();

                if (sdr["RecDate"] != DBNull.Value)
                    this.RecDate = Convert.ToDateTime(sdr["RecDate"]);
                else
                    this.RecDate = Date.getMinDate();

              
                if (sdr["AmtReceived"] != DBNull.Value)
                    this.AmtReceived = Convert.ToSingle(sdr["AmtReceived"]);

               
                if (sdr["NetPayment"] != DBNull.Value)
                    this.NetPayment = Convert.ToSingle(sdr["NetPayment"]);

                if (sdr["BillType"] != DBNull.Value)
                    this.BillType = sdr["BillType"].ToString();

                //if (sdr["username"] != DBNull.Value)
                //    this.username = sdr["username"].ToString();

                if (sdr["Discount"] != DBNull.Value)
                    this.Discount = sdr["Discount"].ToString();

                if (sdr["PID"] != DBNull.Value)
                    this.PID = Convert.ToInt32(sdr["PID"]);

                //if (sdr["Comment"] != DBNull.Value)
                //    this.Remark = sdr["Comment"].ToString();

                //if (sdr["RefundAmt"] != DBNull.Value)
                //    this.P_RefundAmt = Convert.ToSingle(sdr["RefundAmt"]);

                if (sdr["Balance"] != DBNull.Value)
                    this.Balance = Convert.ToSingle(sdr["NetPayment"]) - (Convert.ToSingle(sdr["AmtReceived"]) + Convert.ToSingle(sdr["Discount"]));//- Convert.ToSingle(sdr["RefundAmt"]));

                if (sdr["Balance"] != DBNull.Value)
                {
                    //this.Balance_Temp = Convert.ToSingle(sdr["NetPayment"]) * Convert.ToSingle(sdr["TaxPer"]) / 100;
                    this.Balance_Temp = Convert.ToSingle(sdr["AmtReceived"]) - Convert.ToSingle(sdr["NetPayment"]) - Balance_Temp;
                }

                if (sdr["AmtReceived"] != DBNull.Value)
                    this.AmtPaid = Convert.ToSingle(sdr["AmtReceived"]);

                if (sdr["CenterCode"] != DBNull.Value)
                    this.P_Centercode = (sdr["CenterCode"].ToString());

                //if (sdr["Paymenttype"] != DBNull.Value)
                //    this.Paymenttype = sdr["Paymenttype"].ToString();

                //if (sdr["BankName"] != DBNull.Value)
                //    this.BankName = sdr["BankName"].ToString();

                if (sdr["Othercharges"] != DBNull.Value)
                    this.Othercharges = Convert.ToSingle(sdr["Othercharges"]);
                if ( sdr["OtherchargeRemark"] != DBNull.Value)
                    this.OtherChargeRemark = Convert.ToString(sdr["OtherchargeRemark"]);

                //if (sdr["AccNo"] != DBNull.Value)
                //    this.AccNo = sdr["AccNo"].ToString();


                //if (sdr["Cardtype"] != DBNull.Value)
                //    this.Cardtype = sdr["Cardtype"].ToString();

                //if (sdr["CardName"] != DBNull.Value)
                //    this.CardName = sdr["CardName"].ToString();

                //if (sdr["CardNo"] != DBNull.Value)
                //    this.CardNo = sdr["CardNo"].ToString();

                //if (sdr["ChqDate"] != DBNull.Value)
                //    this.ChqDate = Convert.ToDateTime(sdr["ChqDate"]);
                //else
                //    this.ChqDate = Date.getMinDate();

                //if (sdr["ChqNo"] != DBNull.Value)
                //    this.ChqNo = sdr["ChqNo"].ToString();


                //if (sdr["ExpiryDate"] != DBNull.Value)
                //    this.ExpiryDate = Convert.ToDateTime(sdr["ExpiryDate"]);
                //else
                //    this.ExpiryDate = Date.getMinDate();

                //if (sdr["City"] != DBNull.Value)
                //    this.City = sdr["City"].ToString();  

                
                //if (sdr["DisFlag"] != DBNull.Value)
                //    this.DisFlag = Convert.ToBoolean(sdr["DisFlag"]);

                
                //if (sdr["TaxPer"] != DBNull.Value)
                //    this.TaxPer = Convert.ToSingle(sdr["TaxPer"]);
                //else
                //    this.TaxPer = 0;
                //if (sdr["TaxAmount"] != DBNull.Value)
                //    this.TaxAmount = Convert.ToSingle(sdr["TaxAmount"]);
                //else
                //    this.TaxAmount = 0;
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
        //return al;
    } //for default

    public Cshmst_Bal_C(int PID, int i, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from Cshmst where PID=" + PID + " and branchid=" + i + "", conn);
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {
               
                this.AmtReceived = Convert.ToSingle(sdr["AmtReceived"]);
                this.NetPayment = Convert.ToSingle(sdr["NetPayment"]);
                this.Discount = sdr["Discount"].ToString();
                this.Balance = Convert.ToSingle(sdr["Balance"]);
                this.AmtPaid = Convert.ToSingle(sdr["AmtPaid"]);
                this.P_Centercode = (sdr["CenterCode"].ToString());
                this.P_RefundAmt = Convert.ToSingle(sdr["RefundAmt"]);
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
        
    }

    public Cshmst_Bal_C(string PatRegID, string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from Cshmst where PatRegID='" + PatRegID + "' and branchid=" + branchid + "", conn);
        SqlDataReader sdr = null;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {
                this.Discount = sdr["Discount"].ToString();

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
    }
    public static int get_Existbillno(int branchid, int PID)
    {
        int iNum = 0;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select billno from Recm  where branchid=" + branchid + " and PID=" + PID + " ", conn);
        try
        {
            conn.Open();
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                iNum = 0;
            else
                iNum = Convert.ToInt32(o);
            // This is not a while loop. It only loops once.
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                //conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }

        }
        return iNum;
    }
    public static int getMaxNumber(int branchid,string FID)
    {
        int iNum = 0;
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select max(billno) from RecM where branchid=" + branchid + " and FID='" + FID + "' ", conn);     
        try
        {
            conn.Open();
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                iNum = 1;
            else
                iNum = Convert.ToInt32(o);
            // This is not a while loop. It only loops once.
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                //conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
           
        }
        return iNum;
    }

    public static string getPatRegNo(string billnumber, int branchid)
    {
        string iNum =null;
        SqlConnection conn = DataAccess.ConInitForDC();

        SqlCommand sc = new SqlCommand("select PatRegID from Cshmst where billno=@billno and billtype <> '' and branchid=" + branchid + "", conn);
        sc.Parameters.Add(new SqlParameter("@billno", SqlDbType.NVarChar,50)).Value = billnumber;

        try
        {
            conn.Open();
            string o = Convert.ToString(sc.ExecuteScalar());
            if (o == "")
                iNum = null;
            else
                iNum = Convert.ToString(o);
            // This is not a while loop. It only loops once.
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                //conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }
            
        }
        return iNum;
    }

    public bool delete(int billno, int branchid)
    {

        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
        "delete  from  Cshmst where BillNo=@BillNo and branchid=" + branchid + " ", conn);

        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int , 4)).Value =billno ;       
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sc.ExecuteNonQuery();

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
    } //delete End

    public bool Insert(int branchid)
    {        
        SqlConnection conn = DataAccess.ConInitForDC();
       
        // Add the employee ID parameter and set its value.
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_InsertTransactionReceiveAmt]";

        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 200)).Value = this.P_Centercode;

        if (this.PID != null)
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 55)).Value = this.PID;
        else
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 55)).Value = 0;

        if (this.BillNo != null)
            sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int, 55)).Value = this.BillNo;
        else
            sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int, 55)).Value = "";
               if (this.BillType != null)
            sc.Parameters.Add(new SqlParameter("@BillType", SqlDbType.NVarChar, 50)).Value = this.BillType;
        else
            sc.Parameters.Add(new SqlParameter("@BillType", SqlDbType.NVarChar, 50)).Value = "";

        if (this.RecDate != Date.getMinDate())
        {
            sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = this.RecDate;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = DBNull.Value;
        }
        sc.Parameters.Add(new SqlParameter("@AmtReceived", SqlDbType.Float, 8)).Value = this.AmtReceived;
        if (this.Paymenttype != null)
        {
            sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = this.Paymenttype;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        }
       
        sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 150)).Value = this.BankName;
        sc.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar, 4000)).Value = this.Remark;

       
        if (this.Discount != null)
            sc.Parameters.Add(new SqlParameter("@Discount", SqlDbType.NVarChar, 50)).Value = this.Discount;
        else
            sc.Parameters.Add(new SqlParameter("@Discount", SqlDbType.NVarChar, 50)).Value = "";

        if (this.NetPayment != null)
            sc.Parameters.Add(new SqlParameter("@NetPayment", SqlDbType.Float, 8)).Value = this.NetPayment;
        else
            sc.Parameters.Add(new SqlParameter("@NetPayment", SqlDbType.Float, 8)).Value = "";

        if (this.patRegID != null)
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.patRegID;
        else
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = "";

        if (this.Patientname!= null)
            sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 50)).Value = this.Patientname;
        else
            sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 50)).Value = "";        

              
        if (this.AmtPaid != null)
            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = this.AmtPaid;
        else
            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0f;
        
        if (this.Balance != null)
            sc.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Float)).Value = this.Balance;
        else
            sc.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Float )).Value = 0f;

        if(this.username!=null)
            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar,50)).Value = this.username;
        else
            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = "";

        if (this.Othercharges != null)
            sc.Parameters.Add(new SqlParameter("@Othercharges", SqlDbType.Float)).Value = this.Othercharges;
        else
            sc.Parameters.Add(new SqlParameter("@Othercharges", SqlDbType.Float)).Value = 0f;

        if (this.City != null)
            sc.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 50)).Value = this.City;
        else
            sc.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 50)).Value = "";

        if (this.AccNo != null)
            sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = this.AccNo;
        else
            sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = "";

        if (this.ChqNo != null)
            sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = this.ChqNo;
        else
            sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = "";

        if (this.ChqDate != Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = this.ChqDate;
        else
            sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime )).Value = DBNull.Value;
        
        if (this.CardNo != null)
            sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = this.CardNo;
        else
            sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = "";

        if (this.CardName != null)
            sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = this.CardName;
        else
            sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = "";

        if (this.Cardtype != null)
            sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = this.Cardtype;
        else
            sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = "";

       

        if (this.ExpiryDate != Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime)).Value = this.ExpiryDate;
        else
            sc.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime)).Value = DBNull.Value;

      
        if (this.DisFlag != null)
            sc.Parameters.Add(new SqlParameter("@DisFlag", SqlDbType.Bit)).Value = this.DisFlag;
        else
            sc.Parameters.Add(new SqlParameter("@DisFlag", SqlDbType.Bit)).Value = true;
        
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@DigModule", SqlDbType.Int)).Value = P_DigModule;
        sc.Parameters.Add(new SqlParameter("@TaxPer", SqlDbType.Float)).Value = P_Hstper;
        sc.Parameters.Add(new SqlParameter("@TaxAmount", SqlDbType.Float)).Value = P_Hstamount;

        sc.Parameters.Add(new SqlParameter("@LabGiven", SqlDbType.Float)).Value = P_LabGiven;
        sc.Parameters.Add(new SqlParameter("@DrGiven", SqlDbType.Float)).Value = P_DrGiven;
        sc.Parameters.Add(new SqlParameter("@DiscountPerformTo", SqlDbType.Int)).Value = P_DiscountPerformTo;
        sc.Parameters.Add(new SqlParameter("@OtherChargeRemark", SqlDbType.NVarChar, 250)).Value = P_OtherChargeRemark;

        if (this.CardTransID != null)
            sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = this.CardTransID;
        else
            sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = "";
        if (this.OnlineType != null)
            sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = this.OnlineType;
        else
            sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = "";
        if (this.OnlinetransID != null)
            sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = this.OnlinetransID;
        else
            sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = "";  

        conn.Close();
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
                //if (sdr!= null) sdr.Close();
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
  
   
    public bool Update(int billnumber, int branchid,string FID)
    {       
        SqlConnection conn = DataAccess.ConInitForDC(); 
        SqlCommand sc = new SqlCommand("" +
            "Update Cshmst " +
            " set CenterCode=@CenterCode,BillType=@BillType,AmtReceived=@AmtReceived,Paymenttype=@Paymenttype," +//RecDate=@RecDate,
            " BankName=@BankName,Comment=@Comment,Discount=@Discount," +
            " NetPayment=@NetPayment,PatRegID=@PatRegID,Patname=@Patname," +
            " Patienttest=@Patienttest,DisFlag=@DisFlag,PID=@PID,AmtPaid=@AmtPaid,Balance=@Balance,username=@username,Othercharges=@Othercharges,City=@City,AccNo=@AccNo,ChqNo=@ChqNo,ChqDate=@ChqDate,CardNo=@CardNo,CardName=@CardName,Cardtype=@Cardtype,ExpiryDate=@ExpiryDate where billno=@billno and branchid=" + branchid + " and FID='" + FID + "'", conn);

        // Add the employee ID parameter and set its value.
       
        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 200)).Value = (string)(this.P_Centercode);       
        sc.Parameters.Add(new SqlParameter("@billNo", SqlDbType.Int)).Value = billnumber;

        if (this.PID != null)
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = this.PID;
        else
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 4)).Value = 0;

        if (this.BillType != null)
            sc.Parameters.Add(new SqlParameter("@BillType", SqlDbType.NVarChar, 50)).Value = this.BillType;
        else
            sc.Parameters.Add(new SqlParameter("@BillType", SqlDbType.NVarChar, 50)).Value = "";

        if (this.RecDate != Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = this.RecDate;
        else
            sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = DBNull.Value;
       
        sc.Parameters.Add(new SqlParameter("@AmtReceived", SqlDbType.Float)).Value = (float)(this.AmtReceived);

        if (this.Paymenttype != null)
        {
            sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = this.Paymenttype;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        }
               
        sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 150)).Value = (string)(this.BankName);
        sc.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 4000)).Value = (string)(this.Remark);

     
        sc.Parameters.Add(new SqlParameter("@Discount", SqlDbType.NVarChar, 10)).Value = (string)(this.Discount);
        sc.Parameters.Add(new SqlParameter("@NetPayment", SqlDbType.Float)).Value = (float)(this.NetPayment);
        sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = (string)(this.patRegID);

        sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 250)).Value = (this.Patientname);       
        if (this.AmtPaid != null)
            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = this.AmtPaid;
        else
            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0f;

        if (this.Balance != null)
            sc.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Float)).Value = this.Balance;
        else
            sc.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Float)).Value = 0f;

        if (this.username != null)
            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = this.username;
        else
            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = "";

        if (this.Othercharges != null)
            sc.Parameters.Add(new SqlParameter("@Othercharges", SqlDbType.Float)).Value = this.Othercharges;
        else
            sc.Parameters.Add(new SqlParameter("@Othercharges", SqlDbType.Float)).Value = 0f;

        if (this.City != null)
            sc.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 50)).Value = this.City;
        else
            sc.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 50)).Value = "";

        if (this.AccNo != null)
            sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = this.AccNo;
        else
            sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = "";

        if (this.ChqNo != null)
            sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = this.ChqNo;
        else
            sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = "";

        if (this.ChqDate != Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = this.ChqDate;
        else
            sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = DBNull.Value;

        if (this.CardNo != null)
            sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = this.CardNo;
        else
            sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = "";

        if (this.CardName != null)
            sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = this.CardName;
        else
            sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = "";

        if (this.Cardtype != null)
            sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = this.Cardtype;
        else
            sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = "";

       

        if (this.ExpiryDate != Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime)).Value = this.ExpiryDate;
        else
            sc.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime)).Value = DBNull.Value;

        
        if (this.DisFlag != null)
            sc.Parameters.Add(new SqlParameter("@DisFlag", SqlDbType.NVarChar, 50)).Value = this.DisFlag;
        else
            sc.Parameters.Add(new SqlParameter("@DisFlag", SqlDbType.NVarChar, 50)).Value = "";

       

        

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

  
   
  
   
    public bool ReCalculate(string Doctorcode, string fromdate, string todate, int branchid, int fid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_phdscal]";
        sc.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(fromdate);
        sc.Parameters.Add(new SqlParameter("@Todate", SqlDbType.DateTime)).Value = Convert.ToDateTime(todate);
        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.VarChar, 50)).Value = username;
        sc.Parameters.Add(new SqlParameter("@DigModule", SqlDbType.Int)).Value = P_DigModule;
        sc.Parameters.Add(new SqlParameter("@FinancialYearID", SqlDbType.Int)).Value = fid;
        sc.Parameters.Add(new SqlParameter("@Branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@Doctorcode", SqlDbType.NVarChar, 50)).Value = Doctorcode;

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

    public void getBalance(string regno, string FID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select  sum(BillAmt)- (Sum(AmtPaid) +sum(DisAmt)) as Balance from RecM where PID='" + regno + "' and branchid=" + branchid + "", conn);
        SqlDataReader sdr = null;
        this.Balance = 1;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            if (sdr != null & sdr.Read())
            {
                this.Balance = Convert.ToSingle(sdr["Balance"].ToString());

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
    }

    public void getBalanceSMS(int PID, int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select  sum(BillAmt)- (Sum(AmtPaid) +sum(DisAmt)) as Balance from RecM where PID=" + PID + " and branchid=" + branchid + "", conn);
        SqlDataReader sdr = null;
        this.Balance = 1;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {
                this.Balance = Convert.ToSingle(sdr["Balance"].ToString());

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
    }

    public void getBalance(string regno, string FID, int branchid, int PID)
    {
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select sum(BillAmt)- (Sum(AmtPaid) +sum(DisAmt)) as Balance from RecM where branchid=" + branchid + " and PID=" + PID + "", conn);
        SqlDataReader sdr = null;
        this.Balance = 1;
        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();
            // This is not a while loop. It only loops once.
            if (sdr != null & sdr.Read())
            {

                this.Balance = Convert.ToSingle(sdr["Balance"].ToString());

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
    }





    public DataTable GetDiscountExist(int billno, int branchid,string FID)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select distinct sum(DisAmt) as Discount from RecM where billno=" + billno + " and branchid=" + branchid + " and FID='" + FID + "' ", conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception ex) { throw; }
        finally { conn.Close(); conn.Dispose(); }
        return dt;
    }

    public static string get_RegNo(int branchid ,int PID)
    {
        string  iNum = "0";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("select patRegID from patmst  where branchid=" + branchid + " and PID=" + PID + " ", conn);
        try
        {
            conn.Open();
            object o = sc.ExecuteScalar();
            if (o == DBNull.Value)
                iNum = "1";
            else
                iNum = Convert.ToString(o);
            // This is not a while loop. It only loops once.
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            try
            {
                //conn.Close(); conn.Dispose();
            }
            catch (SqlException)
            {
                // Log an event in the Application Event Log.
                throw;
            }

        }
        return iNum;
    }

    public void update_Fullbillcancel(int PID,  int branchid)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("update patmst set IsFreeze=1 where PID=" + PID + " and branchid=" + branchid + "", con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    public void update_RefundAmount(int PID, int branchid,float DisAmt,float RecAmt)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("update RecM set BalAmt=0,IsRefund=1,DisAmt=" + DisAmt + " ,RefundAmt=RefundAmt+ " + RecAmt * -1 + " where PID=" + PID + " and branchid=" + branchid + " and AmtPaid>0 ", con); // ,AmtReceived=AmtPaid -" + (RecAmt * -1) + "  ,AmtPaid=AmtPaid -" + (RecAmt * -1) + "
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }

    public void update_RefundAmount_RecMst(int PID, int branchid, float DisAmt,float RecAmt)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd = new SqlCommand("update RecM set DisAmt=" + DisAmt + "   where PID=" + PID + " and branchid=" + branchid + " and IsRefund=0 and AmtPaid>=" + (RecAmt * -1) + "", con);//,AmtPaid=AmtPaid -" + (RecAmt * -1) + "
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
    }
    public DataTable Get_Initial(string prefix, int branchid)
    {
        DataTable dt = new DataTable();
        SqlConnection conn = DataAccess.ConInitForDC();
        try
        {
            conn.Open();
            SqlCommand sc = new SqlCommand("SELECT sex from initial" +
                             " WHERE prefix = @prefix", conn);

            sc.Parameters.Add(new SqlParameter("@prefix", SqlDbType.NVarChar, 10)).Value = prefix;
            //SqlCommand cmd = new SqlCommand("select distinct Discount from Cshmst where billno=" + billno + " and branchid=" + branchid + "", conn);
            SqlDataAdapter da = new SqlDataAdapter(sc);
            da.Fill(dt);
        }
        catch (Exception ex) { throw; }
        finally { conn.Close(); conn.Dispose(); }
        return dt;
    }
    public bool Insert_Update(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        // Add the employee ID parameter and set its value.
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_InsertTransactionReceiveAmt_Edit]";

        sc.Parameters.Add(new SqlParameter("@CenterCode", SqlDbType.NVarChar, 200)).Value = this.P_Centercode;

        if (this.PID != null)
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 55)).Value = this.PID;
        else
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 55)).Value = 0;

        if (this.BillNo != null)
            sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int, 55)).Value = this.BillNo;
        else
            sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int, 55)).Value = "";
        if (this.BillType != null)
            sc.Parameters.Add(new SqlParameter("@BillType", SqlDbType.NVarChar, 50)).Value = this.BillType;
        else
            sc.Parameters.Add(new SqlParameter("@BillType", SqlDbType.NVarChar, 50)).Value = "";

        if (this.RecDate != Date.getMinDate())
        {
            sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = this.RecDate;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = DBNull.Value;
        }
        sc.Parameters.Add(new SqlParameter("@AmtReceived", SqlDbType.Float, 8)).Value = this.AmtReceived;
        if (this.Paymenttype != null)
        {
            sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = this.Paymenttype;
        }
        else
        {
            sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = DBNull.Value;
        }

        sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 150)).Value = this.BankName;
        sc.Parameters.Add(new SqlParameter("@Comment", SqlDbType.NVarChar, 4000)).Value = this.Remark;


        if (this.Discount != null)
            sc.Parameters.Add(new SqlParameter("@Discount", SqlDbType.NVarChar, 50)).Value = this.Discount;
        else
            sc.Parameters.Add(new SqlParameter("@Discount", SqlDbType.NVarChar, 50)).Value = "";

        if (this.NetPayment != null)
            sc.Parameters.Add(new SqlParameter("@NetPayment", SqlDbType.Float, 8)).Value = this.NetPayment;
        else
            sc.Parameters.Add(new SqlParameter("@NetPayment", SqlDbType.Float, 8)).Value = "";

        if (this.patRegID != null)
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.patRegID;
        else
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = "";

        if (this.Patientname != null)
            sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 50)).Value = this.Patientname;
        else
            sc.Parameters.Add(new SqlParameter("@Patname", SqlDbType.NVarChar, 50)).Value = "";


        if (this.AmtPaid != null)
            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = this.AmtPaid;
        else
            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0f;

        if (this.Balance != null)
            sc.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Float)).Value = this.Balance;
        else
            sc.Parameters.Add(new SqlParameter("@Balance", SqlDbType.Float)).Value = 0f;

        if (this.username != null)
            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = this.username;
        else
            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = "";

        if (this.Othercharges != null)
            sc.Parameters.Add(new SqlParameter("@Othercharges", SqlDbType.Float)).Value = this.Othercharges;
        else
            sc.Parameters.Add(new SqlParameter("@Othercharges", SqlDbType.Float)).Value = 0f;

        if (this.City != null)
            sc.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 50)).Value = this.City;
        else
            sc.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar, 50)).Value = "";

        if (this.AccNo != null)
            sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = this.AccNo;
        else
            sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = "";

        if (this.ChqNo != null)
            sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = this.ChqNo;
        else
            sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = "";

        if (this.ChqDate != Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = this.ChqDate;
        else
            sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = DBNull.Value;

        if (this.CardNo != null)
            sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = this.CardNo;
        else
            sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = "";

        if (this.CardName != null)
            sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = this.CardName;
        else
            sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = "";

        if (this.Cardtype != null)
            sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = this.Cardtype;
        else
            sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = "";



        if (this.ExpiryDate != Date.getMinDate())
            sc.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime)).Value = this.ExpiryDate;
        else
            sc.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime)).Value = DBNull.Value;


        if (this.DisFlag != null)
            sc.Parameters.Add(new SqlParameter("@DisFlag", SqlDbType.Bit)).Value = this.DisFlag;
        else
            sc.Parameters.Add(new SqlParameter("@DisFlag", SqlDbType.Bit)).Value = true;

        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@DigModule", SqlDbType.Int)).Value = P_DigModule;
        sc.Parameters.Add(new SqlParameter("@TaxPer", SqlDbType.Float)).Value = P_Hstper;
        sc.Parameters.Add(new SqlParameter("@TaxAmount", SqlDbType.Float)).Value = P_Hstamount;

        sc.Parameters.Add(new SqlParameter("@LabGiven", SqlDbType.Float)).Value = P_LabGiven;
        sc.Parameters.Add(new SqlParameter("@DrGiven", SqlDbType.Float)).Value = P_DrGiven;
        sc.Parameters.Add(new SqlParameter("@DiscountPerformTo", SqlDbType.Int)).Value = P_DiscountPerformTo;
        sc.Parameters.Add(new SqlParameter("@OtherChargeRemark", SqlDbType.NVarChar, 250)).Value = P_OtherChargeRemark;

        conn.Close();
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
                //if (sdr!= null) sdr.Close();
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

    public bool Insert_Update_Delete_Test(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        // Add the employee ID parameter and set its value.
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_Update_deleteTestAmt]";

       

        if (this.PID != null)
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 55)).Value = this.PID;
        else
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 55)).Value = 0;
        if (this.NetPayment != null)
            sc.Parameters.Add(new SqlParameter("@NetPayment", SqlDbType.Float, 8)).Value = this.NetPayment;
        else
            sc.Parameters.Add(new SqlParameter("@NetPayment", SqlDbType.Float, 8)).Value = "";

        if (this.patRegID != null)
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.patRegID;
        else
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = "";
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 55)).Value = branchid;
      

        conn.Close();
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
                //if (sdr!= null) sdr.Close();
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

    public bool Insert_Update_Prescription(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        // Add the employee ID parameter and set its value.
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_Update_PRescription]";



        if (this.PID != null)
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 55)).Value = this.PID;
        else
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 55)).Value = 0;
        if (this.NetPayment != null)
            sc.Parameters.Add(new SqlParameter("@UploadPrescription", SqlDbType.NVarChar, 550)).Value = P_UploadPrescription;
        else
            sc.Parameters.Add(new SqlParameter("@UploadPrescription", SqlDbType.NVarChar, 550)).Value = "";

        if (this.patRegID != null)
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
        else
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = "";
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 55)).Value = branchid;
     //   sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 250)).Value = MTCode;


        conn.Close();
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
                //if (sdr!= null) sdr.Close();
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

    public bool Insert_Update_OutLabReport(int branchid)
    {
        SqlConnection conn = DataAccess.ConInitForDC();

        // Add the employee ID parameter and set its value.
        SqlCommand sc = new SqlCommand();
        sc.CommandType = CommandType.StoredProcedure;
        sc.Connection = conn;
        sc.CommandTimeout = 1200;
        sc.CommandText = "[SP_Update_OutSourceReport]";



        if (this.PID != null)
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 55)).Value = this.PID;
        else
            sc.Parameters.Add(new SqlParameter("@PID", SqlDbType.Int, 55)).Value = 0;
        if (this.NetPayment != null)
            sc.Parameters.Add(new SqlParameter("@UploadOutSourceReport", SqlDbType.NVarChar, 550)).Value = P_UploadPrescription;
        else
            sc.Parameters.Add(new SqlParameter("@UploadOutSourceReport", SqlDbType.NVarChar, 550)).Value = "";

        if (this.patRegID != null)
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = this.PatRegID;
        else
            sc.Parameters.Add(new SqlParameter("@PatRegID", SqlDbType.NVarChar, 250)).Value = "";
        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int, 55)).Value = branchid;
        sc.Parameters.Add(new SqlParameter("@MTCode", SqlDbType.NVarChar, 250)).Value = MTCode;
        sc.Parameters.Add(new SqlParameter("@OutSideReport", SqlDbType.Bit)).Value = OutSideReport;


        conn.Close();
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
                //if (sdr!= null) sdr.Close();
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

    #region Properties
    private string mtcode;
    public string MTCode
    {
        get { return mtcode; }
        set { mtcode = value; }
    }


    private Boolean _OutSideReport;
    public Boolean OutSideReport
    {
        get { return _OutSideReport; }
        set { _OutSideReport = value; }
    }

    private string Centercode;
    public string P_Centercode
    {
        get { return Centercode; }
        set { Centercode = value; }
    }

    private int billno;
    public int BillNo
    {
        get { return billno; }
        set { billno = value; }
    }
    private int pID;
    public int PID
    {
        get { return pID; }
        set { pID = value; }
    }

    private string billtype;
    public string BillType
    {
        get { return billtype; }
        set { billtype = value; }
    }
    private DateTime _RecDate;
    public DateTime RecDate
    {
        get { return _RecDate; }
        set { _RecDate = value; }
    }
    private float amtreceived;
    public float AmtReceived
    {
        get { return amtreceived; }
        set { amtreceived = value; }
    }
    private string OtherChargeRemark;
    public string P_OtherChargeRemark
    {
        get { return OtherChargeRemark; }
        set { OtherChargeRemark = value; }
    }

    private string paymenttype;
    public string Paymenttype
    {
        get { return paymenttype; }
        set { paymenttype = value; }
    }
    private string bankname;
    public string BankName
    {
        get { return bankname; }
        set { bankname = value; }
    }
    private string remark;
    public string Remark
    {
        get { return remark; }
        set { remark = value; }
    }
    private float LabGiven;
    public float P_LabGiven
    {
        get { return LabGiven; }
        set { LabGiven = value; }
    }

    private float RefundAmt;
    public float P_RefundAmt
    {
        get { return RefundAmt; }
        set { RefundAmt = value; }
    }
    private float DrGiven;
    public float P_DrGiven
    {
        get { return DrGiven; }
        set { DrGiven = value; }
    }
    private float DiscountPerformTo;
    public float P_DiscountPerformTo
    {
        get { return DiscountPerformTo; }
        set { DiscountPerformTo = value; }
    }
   
    private string discount;
    public string Discount
    {
        get { return discount; }
        set { discount = value; }
    }
    private float netpayment;
    public float NetPayment
    {
        get { return netpayment; }
        set { netpayment = value; }
    }
    private string patregID;
    public string PatRegID
    {
        get { return patregID; }
        set { patregID = value; }
    }
   

    private string UploadPrescription;
    public string P_UploadPrescription
    {
        get { return UploadPrescription; }
        set { UploadPrescription = value; }
    }

    private string patientname;
    public string Patientname
    {
        get { return patientname; }
        set { patientname= value; }
    }

  

    private string patienttest;
    public string Patienttest
    {
        get { return patienttest; }
        set { patienttest = value; }
    }

    private float _AmtPaid;
    public float AmtPaid
    {
        get { return _AmtPaid; }
        set { _AmtPaid = value; }
    }

    private float _Balance;
    public float Balance
    {
        get { return _Balance; }
        set { _Balance = value; }
    }
    private float _Balance_Temp;
    public float Balance_Temp
    {
        get { return _Balance_Temp; }
        set { _Balance_Temp = value; }
    }
    private string _username;
    public string username
    {
        get { return _username; }
        set { _username = value; }
    }
    private DateTime _billTime;
    public DateTime billTime
    {
        get { return _billTime; }
        set { _billTime = value; }
    }
    private float _Othercharges;
    public float Othercharges
    {
        get { return _Othercharges; }
        set { _Othercharges = value; }
    }

    private DateTime _ExpiryDate;
    public DateTime ExpiryDate
    {
        get { return _ExpiryDate; }
        set { _ExpiryDate = value; }
    }



    private string _Cardtype;
    public string Cardtype
    {
        get { return _Cardtype; }
        set { _Cardtype = value; }
    }

    private string _CardName;
    public string CardName
    {
        get { return _CardName; }
        set { _CardName = value; }
    }
    private string _CardNo;
    public string CardNo
    {
        get { return _CardNo; }
        set { _CardNo = value; }
    }
    private string _CardTransID;
    public string CardTransID
    {
        get { return _CardTransID; }
        set { _CardTransID = value; }
    }
    private string _OnlineType;
    public string OnlineType
    {
        get { return _OnlineType; }
        set { _OnlineType = value; }
    }
    private string _OnlinetransID;
    public string OnlinetransID
    {
        get { return _OnlinetransID; }
        set { _OnlinetransID = value; }
    }
    private DateTime _ChqDate;
    public DateTime ChqDate
    {
        get { return _ChqDate; }
        set { _ChqDate = value; }
    }
    private string _ChqNo;
    public string ChqNo
    {
        get { return _ChqNo; }
        set { _ChqNo = value; }
    }
    private string _AccNo;
    public string AccNo
    {
        get { return _AccNo; }
        set { _AccNo = value; }
    }
    private string _City;
    public string City
    {
        get { return _City; }
        set { _City = value; }
    }

    private Boolean _DisFlag;
    public Boolean DisFlag
    {
        get { return _DisFlag; }
        set { _DisFlag = value; }
    }

    private string _patRegID;
    public string patRegID
    {
        get { return _patRegID; }
        set { _patRegID = value; }
    }
    private string fID;
    public string FID
    {
        get { return fID; }
        set { fID = value; }
    }
    private int DigModule;
    public int P_DigModule
    {
        get { return DigModule; }
        set { DigModule = value; }
    }
    private float TaxPer;
    public float P_Hstper
    {
        get { return TaxPer; }
        set { TaxPer = value; }
    }
    private float Hstamt;
    public float P_Hstamt
    {
        get { return Hstamt; }
        set { Hstamt = value; }
    }
    private float TaxAmount;
    public float P_Hstamount
    {
        get { return TaxAmount; }
        set { TaxAmount = value; }
    }
    #endregion
}
