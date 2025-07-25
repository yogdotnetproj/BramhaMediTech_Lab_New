using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;
using System.Drawing;

public partial class Paybilldesk : System.Web.UI.Page
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
  
       static string CenterCode;
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Ledgrmst_Bal_C led = new Ledgrmst_Bal_C();
    public bool discountflag;
    public string discountamount;
    Patmst_New_Bal_C ObjPBC = new Patmst_New_Bal_C();
    Patmstd_Main_Bal_C PNB = new Patmstd_Main_Bal_C();
    DrMT_sign_Bal_C Obj1 = new DrMT_sign_Bal_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        Label1.Text = Date.getdate().ToString("dd/MM/yyyy");
        
        if (!IsPostBack)
        {

            try
            {
                DataTable dtban = new DataTable();
        
        dtban = ObjTB.Bindbanner();
        if (dtban.Rows.Count > 0)
        {
            if (Convert.ToString(dtban.Rows[0]["PaymentTypeDefaultActive"]) == "1")
            {
                
            }
            else
            {
                RadioButtonList1.Items[0].Selected = true;
            }
        }
                ViewState["btnsave"] = "";
                CenterCode = Request.QueryString["CenterCode"];

                if (Convert.ToString(Session["usertype"]) != "Administrator" || Convert.ToString(Session["usertype"]) != "Admin")
                {
                    RadioButtonList2.Enabled = true;
                    //txtDiscnt.ReadOnly = true;
                    
                }
                if (Convert.ToString(Session["usertype"]) == "Administrator" || Convert.ToString(Session["usertype"]) == "Admin")
                {
                    txtDiscnt.Enabled = true;
                    RblDiscgiven.Enabled = true;
                }
                //RadioButtonList2.Enabled = false;
                //txtDiscnt.Enabled = false;

                if (Request.QueryString["PID"] != null)
                {
                    // btnsave.Enabled = false;
                    if (Cshmst_supp_Bal_C.ExistUniqueId(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"])))
                    {

                        int billnumber = Cshmst_supp_Bal_C.GetBillNo(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"].Trim()));
                        lblBillNo.Text = billnumber.ToString();
                        Cshmst_Bal_C ch = new Cshmst_Bal_C(billnumber, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"].Trim()));
                        txtBillDate.Text = ch.RecDate.ToShortDateString();
                        lblName.Text = ch.Patientname;
                        lblDiscountuser.Text = ch.username;
                        hdfielddiscontFlag.Value = ch.DisFlag.ToString();
                        if (ch.DisFlag == false)
                        {
                            RadioButtonList2.Items[0].Selected = true;
                           // RadioButtonList2.Items[1].Selected = false;
                        }
                        else
                        {
                           // RadioButtonList2.Items[1].Selected = true;
                            RadioButtonList2.Items[0].Selected = true;
                        }

                      //  txtCharges.Text = ch.Othercharges.ToString();

                        if (ch.Paymenttype == "Cash")
                        {
                           // RadioButtonList1.Items[0].Selected = true;
                        }
                        else if (ch.Paymenttype == "Cheque")
                        {
                          //  RadioButtonList1.Items[1].Selected = true;
                            lblNo.Visible = true;
                            txtNo.Visible = true;
                            lblNo.Text = "Cheque No";
                            lblBankName.Visible = true;
                            txtBankName.Visible = true;
                            // imgid.Visible = true;
                            lblDate.Visible = true;
                            txtdate.Visible = true;
                            lblDate.Text = "Cheque Date";
                            txtNo.Text = ch.ChqNo;
                            if (ch.ChqDate.ToShortDateString() == "01/01/0001")
                                txtdate.Text = "";
                            else
                                txtdate.Text = ch.ChqDate.ToShortDateString();
                            txtBankName.Text = ch.BankName;
                        }
                        else if (ch.Paymenttype == "Card")
                        {
                           // RadioButtonList1.Items[2].Selected = true;
                            txtNo.Visible = true;
                            lblNo.Visible = true;
                            lblNo.Text = "Credit Card No";
                            txtNo.Text = ch.CardNo;
                            lblBankName.Text = "Transaction Id";
                            txtBankName.Visible = true;
                        }
                        else if (ch.Paymenttype == "Online")
                        {
                            Onlinetran.Visible = true;
                        }
                       // RadioButtonList1.Items[2].Selected = true;
                        ViewState["CenterCode"] = ch.P_Centercode;

                        txtRemark.Text = ch.Remark;
                        // txtBalance.Text = Convert.ToString(ch.Balance + ch.P_Hstamount);
                        txtBalance.Text = Convert.ToString(ch.Balance + Math.Round(ch.P_Hstamount, 0));
                        txtOtherCharges.Text = Convert.ToString( ch.Othercharges);
                        ViewState["OtherCharges"] = Convert.ToString(ch.Othercharges);
                        txtOtherchargeRemark.Text = Convert.ToString(ch.P_OtherChargeRemark);
                        LblTestCharg.Text = Convert.ToString(Convert.ToSingle(ch.NetPayment));//- Convert.ToSingle(ch.Othercharges)
                        txtBalance.Enabled = true;
                        ViewState["amtPaid"] = ch.AmtPaid;
                        lblAdvance.Visible = true;
                        lblAdvanceAmt.Visible = true;
                        txtDiscnt.Text = ch.Discount;
                        hdfielddiscont.Value = ch.Discount;
                        txtNetPayment.Text = ch.NetPayment.ToString();
                        lblAdvanceAmt.Text = ch.AmtPaid.ToString();
                        if (Convert.ToSingle(lblAdvanceAmt.Text) > 0)
                        {
                            // RadioButtonList2.Enabled = false;
                            // txtDiscnt.Enabled = false;
                            if (ch.AmtPaid > ch.NetPayment)
                            {
                                float AmRef = Convert.ToSingle((ch.Balance_Temp * ch.P_Hstper) / 100);
                                // txtAmtPaid.Text = Convert.ToString(ch.Balance_Temp+Math.Round(AmRef, 0));
                                txtAmtPaid.Text = Convert.ToString(ch.Balance_Temp);
                                txtBalance.Text = "0";
                                txthstamount.Text = Convert.ToString((Convert.ToSingle(txtNetPayment.Text) * ch.P_Hstper) / 100);

                            }
                            else
                            {
                                txtAmtPaid.Text = Convert.ToString(0);
                                txtBalance.Text = Convert.ToString(ch.Balance);
                                txthstamount.Text = ch.P_Hstamount.ToString();
                            }
                            // txtAmtPaid.Enabled = false;
                        }
                        else
                        {
                            txtAmtPaid.Text = Convert.ToString(0);
                           // RadioButtonList2.Enabled = true;
                           // txtDiscnt.Enabled = true;
                            txtAmtPaid.Enabled = true;
                            txthstamount.Text = ch.P_Hstamount.ToString();
                        }

                       


                        int PID = ch.PID;
                        Patmst_Bal_C Obj_PBC = new Patmst_Bal_C(PID, Convert.ToInt32(Session["Branchid"]));
                        lblRegNo.Text = Obj_PBC.PatRegID;
                        ViewState["FID"] = Obj_PBC.FID;
                        Patmst_New_Bal_C cc = new Patmst_New_Bal_C();
                        GridView1.DataSource = cc.Get_PatientInfo(PID, Convert.ToInt32(Session["Branchid"]));
                        GridView1.DataBind();

                        float tamt = 0f;
                        string test = "";
                        foreach (GridViewRow gr in GridView1.Rows)
                        {
                            Label lbltestrate = gr.FindControl("lblTestRate") as Label;
                            tamt = tamt + Convert.ToSingle(lbltestrate.Text);
                            if (test == "")
                            {
                                test = gr.Cells[0].Text;
                            }
                            else
                            {
                                test = test + "," + gr.Cells[0].Text;
                            }
                        }
                        lbltestcharges.Text = tamt.ToString();
                        lbltestcharges.Text = Convert.ToString(Convert.ToSingle(lbltestcharges.Text) + Convert.ToSingle(ch.Othercharges));
                        txtDiscnt_TextChanged(null, null);
                        BalanceCalculation();
                        ViewState["TotalBillAmt"] = tamt.ToString();
                        ViewState["test"] = test;
                        ViewState["PID"] = ch.PID;
                        ViewState["SaveEdit"] = "Edit";
                        string vw = ViewState["SaveEdit"].ToString();
                        Bindgrid(billnumber);
                        if (Request.QueryString["Refund"] == "Yes")
                        {
                            ViewState["SaveEdit"] = "Save";
                            txtAmtPaid.Enabled = false;
                        }
                        if (Request.QueryString["Refund"] == "Yes")
                        {
                            if (txtDiscnt.Text == "")
                            {
                                txtDiscnt.Text = "0";
                            }
                            txtAmtPaid.Text = Convert.ToString(Convert.ToSingle(lblAdvanceAmt.Text) - (Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(txtDiscnt.Text) + Convert.ToSingle(ch.P_RefundAmt)));
                            //if (Convert.ToSingle( txtAmtPaid.Text) > 0)
                            //{
                            //    txtAmtPaid.Text = Convert.ToString(Convert.ToSingle(txtAmtPaid.Text) * -1);
                            //}
                            if (Convert.ToSingle(lblAdvanceAmt.Text) > 0)
                            {
                                //txtAmtPaid.Text = Convert.ToString(Convert.ToSingle(lblAdvanceAmt.Text) * -1);
                                txtAmtPaid.Text = Convert.ToString(Convert.ToSingle(txtAmtPaid.Text) * -1);
                                if (Convert.ToSingle(txtAmtPaid.Text) == 0)
                                {
                                    txtAmtPaid.Text = Convert.ToString(Convert.ToSingle(lbltestcharges.Text) * -1);
                                }
                            }
                            //txtAmtPaid.Enabled = false;
                            btnsave.Enabled = true;
                        }
                        else
                        {
                            txtAmtPaid.Enabled = true;
                        }

                    }
                    else
                    {

                        txtBillDate.Text = Date.getdate().ToString("dd/MM/yyyy");
                        Patmst_Bal_C coninfo = new Patmst_Bal_C(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]));
                        lblName.Text = coninfo.Patname;
                        lblRegNo.Text = coninfo.PatRegID;
                        ViewState["FID"] = coninfo.FID;
                        ViewState["STCODE"] = coninfo.Tests;
                        lbltestcharges.Text = coninfo.TestCharges.ToString();
                        ViewState["CenterCode"] = coninfo.CenterCode;
                        lblAdvance.Visible = false;
                        lblAdvanceAmt.Visible = false;
                        Patmst_New_Bal_C cc = new Patmst_New_Bal_C();
                        GridView1.DataSource = cc.Get_PatientInfo(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]));
                        GridView1.DataBind();
                        txtDiscnt.Text = "0";
                        txtNetPayment.Text = lbltestcharges.Text;
                        txtAmtPaid.Text = "0";
                        txtBalance.Text = lbltestcharges.Text;
                        ViewState["TotalBillAmt"] = lbltestcharges.Text;
                        ViewState["SaveEdit"] = "Save";
                        RadioButtonList2.Enabled = true;
                        txtDiscnt.ReadOnly = false;
                        ViewState["PID"] = Request.QueryString["PID"];

                    }
                }
                else if (Request.QueryString["billno"] != null)
                {
                    lblBillNo.Text = Request.QueryString["billno"].ToString();
                    Cshmst_Bal_C ch = new Cshmst_Bal_C(Convert.ToInt32(Request.QueryString["billno"]), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                    txtBillDate.Text = ch.RecDate.ToShortDateString();
                    lblName.Text = ch.Patientname;
                    if (ch.Paymenttype == "Cash")
                    {
                        RadioButtonList1.Items[0].Selected = true;
                    }
                    else if (ch.Paymenttype == "Cheque")
                    {
                        RadioButtonList1.Items[1].Selected = true;
                        lblNo.Visible = true;
                        txtNo.Visible = true;
                        lblNo.Text = "Cheque No";
                        lblBankName.Visible = true;
                        txtBankName.Visible = true;
                        // imgid.Visible = true;
                        lblDate.Visible = true;
                        txtdate.Visible = true;
                        lblDate.Text = "Cheque Date";
                        txtNo.Text = ch.ChqNo;
                        if (ch.ChqDate.ToShortDateString() == "01/01/0001")
                            txtdate.Text = "";
                        else
                            txtdate.Text = ch.ChqDate.ToShortDateString();
                        txtBankName.Text = ch.BankName;
                    }
                    else if (ch.Paymenttype == "Card")
                    {
                        RadioButtonList1.Items[2].Selected = true;
                        txtNo.Visible = true;
                        lblNo.Visible = true;
                        lblNo.Text = "Card No";

                        txtNo.Text = ch.ChqNo;
                    }
                    else if (ch.Paymenttype == "Online")
                    {
                        RadioButtonList1.Items[3].Selected = true;
                        txtNo.Visible = true;
                        lblNo.Visible = true;
                        lblNo.Text = "Card No";

                        txtNo.Text = ch.ChqNo;
                    }
                    if (ch.DisFlag == false)
                    {
                        RadioButtonList2.Items[0].Selected = true;
                       // RadioButtonList2.Items[1].Selected = false;
                    }
                    else
                    {
                       // RadioButtonList2.Items[1].Selected = true;
                        RadioButtonList2.Items[0].Selected = false;
                    }
                    this.RadioButtonList2_SelectedIndexChanged(this, null);

                    ViewState["CenterCode"] = ch.P_Centercode;
                    // txtCharges.Text = ch.Othercharges.ToString();
                    txtRemark.Text = ch.Remark;
                    txtBalance.Text = ch.Balance.ToString();
                    ViewState["amtPaid"] = ch.AmtPaid;
                    lblAdvance.Visible = true;
                    lblAdvanceAmt.Visible = true;
                    lblAdvanceAmt.Text = ch.AmtPaid.ToString();
                    txtAmtPaid.Text = Convert.ToString(0);
                    int PID = ch.PID;
                    Patmst_Bal_C Obj_PBC = new Patmst_Bal_C(PID, Convert.ToInt32(Session["Branchid"]));
                    lblRegNo.Text = Obj_PBC.PatRegID;
                    Patmst_New_Bal_C PNBC = new Patmst_New_Bal_C();
                    GridView1.DataSource = PNBC.Get_PatientInfo(PID, Convert.ToInt32(Session["Branchid"]));
                    GridView1.DataBind();
                    float tamt = 0f;
                    string test = "";
                    foreach (GridViewRow gr in GridView1.Rows)
                    {
                        Label lbltestrate = gr.FindControl("lblTestRate") as Label;
                        tamt = tamt + Convert.ToSingle(lbltestrate.Text);
                        if (test == "")
                        {
                            test = gr.Cells[0].Text;
                        }
                        else
                        {
                            test = test + "," + gr.Cells[0].Text;
                        }
                    }
                    lbltestcharges.Text = tamt.ToString();

                    ViewState["test"] = test;
                    ViewState["PID"] = ch.PID;
                    ViewState["SaveEdit"] = "Edit";
                    Bindgrid(Convert.ToInt32(Request.QueryString["billno"]));
                }
                if (Request.QueryString["Frname"] != null)
                {
                    string url = (Request.QueryString["Frname"]).ToString();
                    if (url == "Receipt")
                    {
                        Label2.Visible = false;


                    }
                }
                else
                {
                    Label24.Enabled = true;
                    if (lblAdvanceAmt.Text != "")
                    {
                        if (Convert.ToSingle(lblAdvanceAmt.Text) > 0)
                        {
                            // txtAmtPaid.Enabled = false; 
                        }
                        else
                        {
                            txtAmtPaid.Enabled = true;
                        }
                    }
                    //  btnlab.Visible = true;


                }
                Fill_Labels();
            }
            catch (Exception exc)
            {
                if (exc.Message.Equals("Exception aborted."))
                {
                    return;
                }
                else
                {
                    Response.Cookies["error"].Value = exc.Message;
                    Server.Transfer("~/ErrorMessage.aspx");
                }
            }
            if (lblAdvanceAmt.Text != "")
            {
                if (Convert.ToSingle(lblAdvanceAmt.Text) > 0)
                {
                    btnsave.Enabled = true;
                }
            }
            if (Lblcenter.Text == "IPD")
            {
                isbth.Enabled = true;
            }
            else
            {
               // isbth.Enabled = false;
            }
            if (Convert.ToString(Session["usertype"]) != "Administrator")
            {
                checkexistpageright("");
            }

        }
        txtBillDate.Text = Date.getdate().ToString("dd/MM/yyyy");
    }
  
    private void Bindgrid(int bno)
    {
       // lblReceipt.Visible = true;
        GridView2.DataSource = GetDataFromBillNo(bno, Convert.ToInt32(Session["Branchid"]));
        GridView2.DataBind();
        foreach (GridViewRow gr in GridView2.Rows)
        {
            string date = gr.Cells[0].Text;
            date = Convert.ToDateTime(date).ToShortDateString();
            gr.Cells[0].Text = date;
            //if (gr.Cells[1].Text == "0")
               // gr.Visible = false;
        }
       

    }

    public DataSet GetDataFromBillNo(int billno, int branchid)
    {
        ArrayList al = new ArrayList();
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from RecM where BillNo=@billno and branchid=" + branchid + " and FID=@FID ", conn);

        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = billno;
        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.Int)).Value = Convert.ToInt32(Request.QueryString["FID"]);
        SqlDataAdapter da = new SqlDataAdapter(sc);
        DataSet ds = new DataSet();
        SqlDataReader sdr = null;

        try
        {
            da.Fill(ds);
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
    }

    protected void txtDiscnt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int billnumber = Cshmst_supp_Bal_C.GetBillNo(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
           // Cshmst_Bal_C ch = new Cshmst_Bal_C(Convert.ToInt32( Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Request.QueryString["FID"]));

            if (txtDiscnt.Text == "")
            {
                txtDiscnt.Text = "0";
            }
            if (txtAmtPaid.Text == "")
            {
                txtAmtPaid.Text = "0";
            }
            txtDisdrgiven.Text = "0";
            txtDisLabgiven.Text = "0";
            if (Convert.ToSingle(txtAmtPaid.Text) < 0)
            { txtAmtPaid.Text = "0"; }
            float discnt = 0, total = 0;
            //if (RadioButtonList2.Items[0].Selected)
            //{
            //    if (Convert.ToSingle(txtDiscnt.Text) <= 100)
            //    {
            //        discnt = Convert.ToSingle(lbltestcharges.Text) * Convert.ToSingle(txtDiscnt.Text) / 100;
            //    }
            //    else
            //    {
            //        Label12.Visible = true;
            //        Label12.Text = "Discount Percentage Less or Equal to 100";
            //        return;
            //    }
            //}
            //else
            //{
                if ((Convert.ToSingle(txtDiscnt.Text)) > Convert.ToSingle(lbltestcharges.Text))
                {
                    //txtDiscnt.Text = "0";
                    Label12.Visible = true;
                    lblmsg.Visible = false;
                    Label12.Text = "Discount amount Less or Equal to TotalBill";
                    return;
                }
                else
                {
                    Label12.Visible = false;
                    discnt = Convert.ToSingle(txtDiscnt.Text);
                }
                if (Convert.ToString(Session["usertype"]) != "Administrator")
                {
                    //float ConvDisPer = ((Convert.ToSingle(txtDiscnt.Text) * 100) / Convert.ToSingle(lbltestcharges.Text));
                    //if (Convert.ToSingle(ConvDisPer) > 15)
                    //{
                    //    Label12.Visible = true;
                    //    Label12.Text = "Discount(per) not greater than 15%. pls contact Admin";

                    //    txtDiscnt.Focus();
                    //    txtDiscnt.BorderColor = Color.Red;
                    //    Label12.ForeColor = Color.Red;
                    //    return;
                    //}
                    //else
                    //{
                    //    txtDiscnt.BorderColor = Color.Black;
                    //}
                }
               // lbltestcharges.Text = Convert.ToString( Convert.ToSingle(LblTestCharg.Text) - Convert.ToSingle(txtDiscnt.Text));
           // }
            ViewState["Discnt"] = discnt;
            total = Convert.ToSingle(lbltestcharges.Text) - 0;
            //total = total - discnt;
            txtNetPayment.Text = Convert.ToString(total);
            if (lblAdvanceAmt.Text != "")
            {
               
                        //if (RadioButtonList2.Items[0].Selected)
                        //{
                        //    float NetPayment = 0;
                        //    if (Convert.ToSingle( txtDiscnt.Text )> 0)
                        //    {
                        //        NetPayment = Convert.ToSingle(txtNetPayment.Text) * Convert.ToSingle(txtDiscnt.Text) / 100;
                        //    }
                        //    else
                        //    {
                        //        NetPayment = Convert.ToSingle(txtNetPayment.Text);
                        //    }
                        //    txthstamount.Text = Convert.ToString(Convert.ToSingle(NetPayment) * Convert.ToSingle(Session["Taxper"]) / 100);
                        //    txtBalance.Text = Convert.ToString((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(lblAdvanceAmt.Text) - Convert.ToSingle(txtAmtPaid.Text) - Convert.ToSingle(discnt));
                        //}
                        //else
                        //{
                            txthstamount.Text = Convert.ToString((Convert.ToSingle(txtNetPayment.Text) - Convert.ToSingle(txtDiscnt.Text)) * Convert.ToSingle(Session["Taxper"]) / 100);
                            txtBalance.Text = Convert.ToString((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(lblAdvanceAmt.Text) - Convert.ToSingle(txtAmtPaid.Text) - Convert.ToSingle(discnt));

                       // }
                    
                
            }
            else
            {
                txtBalance.Text = Convert.ToString((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text),0)) - 0 - Convert.ToSingle(txtAmtPaid.Text));
            }
            if (Convert.ToSingle(txtBalance.Text) < 0)
            {
               // txtAmtPaid.Text = txtBalance.Text;
                if (Request.QueryString["Refund"] != "Yes")
                {
                    txtAmtPaid.Text = Convert.ToString(Convert.ToSingle(txtAmtPaid.Text) * -1);

                    if (Convert.ToSingle(txtBalance.Text) < 0)
                    {
                        txtAmtPaid.Text = txtBalance.Text;
                    }
                    txtBalance.Text = "0";
                    txtAmtPaid.Enabled = false;
                }
                else
                {
                    txtAmtPaid.Enabled = true;
                    txtBalance.Text = "0";
                    txtAmtPaid.Text = Convert.ToString(Convert.ToSingle(lbltestcharges.Text) - (Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(txtDiscnt.Text) ));
                }
            }
            else
            {
                txtAmtPaid.Enabled = true;
            }
            BalanceCalculation();
        }
        catch (Exception exc)
        {
            if (exc.Message.Equals("Exception aborted."))
            {
                return;
            }
            else
            {
                Response.Cookies["error"].Value = exc.Message;
                Server.Transfer("~/ErrorMessage.aspx");
            }
        }
    }

    protected void btnreport_Click(object sender, EventArgs e)
    {
        if (ViewState["btnsave"].ToString() != "true")
        {
          //  this.btnsave_Click(btnsave, null);
        }
        CalculateServicewise_Amt();
        string sql = "";

        if (lblBillNo.Text != "")
        {

           
            SqlConnection conn = DataAccess.ConInitForDC();
            SqlCommand sc = conn.CreateCommand();
            sc.CommandText = "ALTER VIEW [dbo].[VW_csrstvw] AS (SELECT     patmst.PatRegID, patmst.intial, patmst.Patname,  patmst.sex, patmst.Age,     patmst.Drname, patmst.TelNo, "+
                      "  DrMT.DoctorCode, DrMT.DoctorName, MainTest.Maintestname, MainTest.MTCode,     patmstd.TestRate, PackMst.PackageName, "+
                      "  patmstd.PackageCode, patmst.Patusername,     patmst.Patpassword, patmst.MDY, patmst.Remark AS PatientRemark, "+
                      "  patmst.Pataddress,     patmst.PPID, VW_billreceipt.PaymentType as UnitCode, VW_billreceipt.BillNo, VW_billreceipt.Paymenttype,  " +
                      "  VW_billreceipt.billdate,     VW_billreceipt.AmtPaid AS AmtPaid, VW_billreceipt.DisAmt, VW_billreceipt.TaxAmount   "+
                      "  ,VW_billreceipt.ReceiptNo FROM         patmst INNER JOIN    DrMT ON patmst.Centercode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid  " +
                      "  INNER JOIN    MainTest INNER JOIN    patmstd ON MainTest.MTCode = patmstd.MTCode AND  "+
                      "  MainTest.Branchid = patmstd.Branchid ON     patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid "+
                      "  INNER JOIN    VW_billreceipt ON patmstd.PID = VW_billreceipt.PID LEFT OUTER JOIN   "+
                      "  PackMst ON patmstd.Branchid = PackMst.branchid AND     patmstd.PackageCode = PackMst.PackageCode  where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and VW_billreceipt.BillNo=" + lblBillNo.Text + "  and VW_billreceipt.FID='" +Convert.ToString(Request.QueryString["FID"]) + "')";// 

            conn.Open();
            sc.ExecuteNonQuery();
            conn.Close(); conn.Dispose();
         
            Session.Add("rptsql", sql);
            Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_Receipt.rpt");
            Session["reportname"] = "CashBillReceipt";
            Session["RPTFORMAT"] = "pdf";

            ReportParameterClass.SelectionFormula = sql;
            string close = "<script language='javascript'>javascript:OpenReport();</script>";
            Type title1 = this.GetType();
            Page.ClientScript.RegisterStartupScript(title1, "", close);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        try
        {
            string C_Code = null;
            string Partstring = null;
            string BillFormatstr = null;
            string v = ViewState["SaveEdit"].ToString();
            if (ViewState["SaveEdit"].ToString() != "Edit")
            {
                if (Cshmst_supp_Bal_C.isBillNoExists(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"])))
                {
                    Label12.Text = "Bill no Already Exist";
                    return;
                }
                Cshmst_Bal_C Obj_CSH = new Cshmst_Bal_C();
                Obj_CSH.P_Centercode = ViewState["CenterCode"].ToString();
                Obj_CSH.BillNo = Convert.ToInt32(lblBillNo.Text);

                Obj_CSH.RecDate = DateTimeConvesion.getDateFromString(txtBillDate.Text);
                Obj_CSH.BillType = "Cash Bill";
                Obj_CSH.AmtReceived = Convert.ToSingle(lbltestcharges.Text);
                if (txtDiscnt.Text != "")
                {
                    Obj_CSH.NetPayment = Convert.ToSingle(txtNetPayment.Text);
                    Obj_CSH.Discount = txtDiscnt.Text;
                }
                else
                {
                    Obj_CSH.NetPayment = Convert.ToSingle(lbltestcharges.Text);
                    Obj_CSH.Discount = "0";
                }
                Obj_CSH.patRegID = lblRegNo.Text;
                Obj_CSH.Patientname = lblName.Text;
                Obj_CSH.Patienttest = ViewState["STCODE"].ToString();
                Obj_CSH.Remark = txtRemark.Text;
                Obj_CSH.PID = Convert.ToInt32(Request.QueryString["PID"]);
                if (txtAmtPaid.Text != "")
                    Obj_CSH.AmtPaid = Convert.ToSingle(txtAmtPaid.Text);
                else
                    Obj_CSH.AmtPaid = 0;
                if (txtBalance.Text != "")
                    Obj_CSH.Balance = Convert.ToSingle(txtBalance.Text);
                else
                    Obj_CSH.Balance = Convert.ToSingle(lbltestcharges.Text);
                Obj_CSH.Paymenttype = RadioButtonList1.SelectedItem.Text;

                if (RadioButtonList1.Items[1].Selected)
                {
                    Obj_CSH.ChqNo = txtNo.Text;
                    Obj_CSH.ChqDate = DateTimeConvesion.getDateFromString(txtdate.Text);
                    Obj_CSH.BankName = txtBankName.Text;
                }
                else if (RadioButtonList1.Items[2].Selected)
                {
                    Obj_CSH.CardNo = txtNo.Text;
                }

                Obj_CSH.username = Session["username"].ToString();
               
                    Obj_CSH.Othercharges = 0;

                if (RadioButtonList2.Items[0].Selected)
                {
                    Obj_CSH.DisFlag = false;
                }
                else
                {
                    Obj_CSH.DisFlag = true;
                }
                Obj_CSH.Insert(Convert.ToInt32(Session["Branchid"]));

                //ledger transaction Insert
                C_Code = ViewState["CenterCode"].ToString();
                Partstring = "(CB) " + System.Configuration.ConfigurationManager.AppSettings["compInitials"].Trim() + "/" + lblBillNo.Text + "/" + FinancialYearTableLogic.getCurrentFinancialYear().StartDate.Year + "-" + FinancialYearTableLogic.getCurrentFinancialYear().EndDate.Year + ":" + txtRemark.Text;
                BillFormatstr = "" + System.Configuration.ConfigurationManager.AppSettings["compInitials"].Trim() + "/" + lblBillNo.Text + "/" + FinancialYearTableLogic.getCurrentFinancialYear().StartDate.Year;

                // led.RegDate = DateTimeConvesion.getDateFromString(txtBillDate.Text);
                if (txtAmtPaid.Text != "")
                    led.CreditAmt = Convert.ToSingle(txtAmtPaid.Text);
                else
                    led.CreditAmt = 0;

                led.DebitAmt = 0;
               // led.ParticularField = Partstring.ToString();
                led.CenterCode = DrMT_sign_Bal_C.GetSingleCenter(C_Code, Convert.ToInt32(Session["Branchid"]));
                led.BillNo = Convert.ToInt32(lblBillNo.Text);
                led.BillFormat = "";

               // led.P_ReceiptNO = DrMT_sign_Bal_C.GetReceiptno(Convert.ToString( Request.QueryString["FID"]), Convert.ToInt32(Session["Branchid"]));
                 int RecNo = Obj1.Insert_Update_ReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Request.QueryString["FID"]));
                 led.P_ReceiptNO = Convert.ToString( RecNo);
                SqlConnection conn = DataAccess.ConInitForDC();
                SqlCommand sc = new SqlCommand("insert into RecM(BillNo,BillDate,AmtPaid,Paymenttype,BankName,FID,ReceiptNo)" +
                "values(@BillNo,@RecDate,@AmtPaid,@Paymenttype,@BankName,@FID,@ReceiptNo)", conn);
                sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = Convert.ToInt32(lblBillNo.Text);
                if (txtAmtPaid.Text != "")
                    sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = Convert.ToSingle(txtAmtPaid.Text);
                else
                    sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0;
                sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtBillDate.Text);
                sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = RadioButtonList1.SelectedItem.Text;
                sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 50)).Value = txtBankName.Text;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.Int)).Value = Convert.ToInt32(Request.QueryString["FID"]);
                sc.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.Int)).Value = Convert.ToString(led.P_ReceiptNO);

                conn.Open();
                sc.ExecuteNonQuery();

                Label12.Visible = true;
                Label12.Text = "Record save successfully.";
                //ClearControls();            
            }
            else
            {
                Cshmst_Bal_C Obj_CSH = new Cshmst_Bal_C();
                Obj_CSH.P_Centercode = ViewState["CenterCode"].ToString();
                Obj_CSH.BillNo = Convert.ToInt32(lblBillNo.Text);

                Obj_CSH.RecDate = DateTimeConvesion.getDateFromString(txtBillDate.Text);

                Obj_CSH.BillType = "Cash Bill";
                Obj_CSH.AmtReceived = Convert.ToSingle(lbltestcharges.Text);
                if (txtDiscnt.Text != "")
                {
                    Obj_CSH.NetPayment = Convert.ToSingle(txtNetPayment.Text);
                    Obj_CSH.Discount = txtDiscnt.Text;
                }
                else
                {
                    Obj_CSH.NetPayment = Convert.ToSingle(lbltestcharges.Text);
                    Obj_CSH.Discount = "0";
                }
                Obj_CSH.patRegID = lblRegNo.Text;
                Obj_CSH.Patientname = lblName.Text;
                Obj_CSH.Patienttest = ViewState["test"].ToString();
                Obj_CSH.Remark = txtRemark.Text;
                Obj_CSH.PID = Convert.ToInt32(ViewState["PID"]);
                if (Convert.ToSingle(txtBalance.Text) < 0)
                {
                    float ap = Convert.ToSingle(ViewState["amtPaid"]);
                    ap = ap + Convert.ToSingle(txtAmtPaid.Text) + Convert.ToSingle(txtBalance.Text);
                    Obj_CSH.AmtPaid = ap;
                }
                else
                {
                    float ap = Convert.ToSingle(ViewState["amtPaid"]);
                    ap = ap + Convert.ToSingle(txtAmtPaid.Text);
                    Obj_CSH.AmtPaid = ap;
                }
                if (txtBalance.Text != "")
                    Obj_CSH.Balance = Convert.ToSingle(txtBalance.Text);
                else
                    Obj_CSH.Balance = Convert.ToSingle(lbltestcharges.Text);

                Obj_CSH.Paymenttype = RadioButtonList1.SelectedItem.Text;

                if (RadioButtonList1.Items[1].Selected)
                {
                    Obj_CSH.ChqNo = txtNo.Text;
                    Obj_CSH.ChqDate = DateTimeConvesion.getDateFromString(txtdate.Text);
                    Obj_CSH.BankName = txtBankName.Text;
                }
                else if (RadioButtonList1.Items[2].Selected)
                {
                    Obj_CSH.CardNo = txtNo.Text;
                }

               
                    Obj_CSH.Othercharges = 0;
                Obj_CSH.username = Session["username"].ToString();

                if (RadioButtonList2.Items[0].Selected)
                {
                    Obj_CSH.DisFlag = false;
                }
                else
                {
                    Obj_CSH.DisFlag = true;
                }

                Obj_CSH.Update(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));

                //ledger transaction update
                C_Code = ViewState["CenterCode"].ToString();
                 Ledgrmst_Bal_C led = new Ledgrmst_Bal_C();
                led.RegDate = DateTimeConvesion.getDateFromString(txtBillDate.Text);

                if (txtAmtPaid.Text != "")
                    led.CreditAmt = Convert.ToSingle(txtAmtPaid.Text);
                else
                    led.CreditAmt = 0;

                led.DebitAmt = 0;
                //led.ParticularField = Partstring.ToString();
                led.CenterCode = DrMT_sign_Bal_C.GetSingleCenter(C_Code, Convert.ToInt32(Session["Branchid"]));
                led.BillNo = Convert.ToInt32(lblBillNo.Text);
                led.BillFormat = "";
                //led.P_ReceiptNO = DrMT_sign_Bal_C.GetReceiptno(Convert.ToString(Request.QueryString["FID"]), Convert.ToInt32(Session["Branchid"]));
                int RecNo = Obj1.Insert_Update_ReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Request.QueryString["FID"]));
                led.P_ReceiptNO = Convert.ToString( RecNo);
                SqlConnection conn = DataAccess.ConInitForDC();
                SqlCommand sc = new SqlCommand("insert into RecM(BillNo,BillDate,AmtPaid,Paymenttype,BankName,FID,ReceiptNo)" +
                "values(@BillNo,@RecDate,@AmtPaid,@Paymenttype,@BankName,@FID,@ReceiptNo)", conn);
                sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = Convert.ToInt32(lblBillNo.Text);
                if (txtAmtPaid.Text != "")
                    sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = Convert.ToSingle(txtAmtPaid.Text);
                else
                    sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0;
                sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtBillDate.Text);
                sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = RadioButtonList1.SelectedItem.Text;
                sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 50)).Value = txtBankName.Text;
                sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.Int)).Value = Convert.ToInt32(Request.QueryString["FID"]);
                sc.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.Int)).Value = Convert.ToString(led.P_ReceiptNO);
                conn.Open();
                sc.ExecuteNonQuery();

                Bindgrid(Convert.ToInt32(lblBillNo.Text));
                Label12.Visible = true;
                Label12.Text = "Record update successfully.";
                //ClearControls();
            }
        }
        catch (Exception exc)
        {
            if (exc.Message.Equals("Exception aborted."))
            {
                return;
            }
            else
            {
                Response.Cookies["error"].Value = exc.Message;
                Server.Transfer("~/ErrorMessage.aspx");
            }
        }
    }

    private void ClearControls()
    {
        lblBillNo.Text = "";
        lblName.Text = "";
        lblRegNo.Text = "";
        lbltestcharges.Text = "";
        txtAmtPaid.Text = "";
        txtDiscnt.Text = "";
        txtNetPayment.Text = "";
        txtBalance.Text = "";
        txtRemark.Text = "";
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Patientcashbill.aspx");
    }

    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.Items[2].Selected)
        {
            txtNo.Visible = true;
            lblNo.Visible = true;
            lblNo.Text = "Card No";
            //lblBankName.Visible = false;
            //txtBankName.Visible = false;
            // imgid.Visible = false;
            lblDate.Visible = false;
            txtdate.Visible = false;

            lblBankName.Visible = true;
            lblBankName.Text = "Transaction Id";
            txtBankName.Visible = true;
            Onlinetran.Visible = false;
        }
        else if (RadioButtonList1.Items[0].Selected)
        {
            lblNo.Visible = false;
            txtNo.Visible = false;
            lblBankName.Visible = false;
            txtBankName.Visible = false;
           // imgid.Visible = false;
            lblDate.Visible = false;
            txtdate.Visible = false;
            Onlinetran.Visible = false;
        }
        else  if (RadioButtonList1.Items[1].Selected)
        {
            lblNo.Visible = true;
            txtNo.Visible = true;
            lblNo.Text = "Cheque No";
            lblBankName.Visible = true;
            txtBankName.Visible = true;
            // imgid.Visible = true;
            lblDate.Visible = true;
            txtdate.Visible = true;
            lblDate.Text = "Cheque Date";
            Onlinetran.Visible = false;
        }
        else if (RadioButtonList1.Items[3].Selected)
        {
            Onlinetran.Visible = true;
            lblNo.Visible = false;
            txtNo.Visible = false;
            lblNo.Text = "Cheque No";
            lblBankName.Visible = false;
            txtBankName.Visible = false;
            // imgid.Visible = true;
            lblDate.Visible = false;
            txtdate.Visible = false;
            lblDate.Text = "Cheque Date";
        }

    }

    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.Items[0].Selected)
        {
            //lbldis.Text = "Discount(%) Perecent";
            lbldis.Text = "Discount Amount";
        }
        else
        {
            lbldis.Text = "Discount Amount";

        }
        this.txtDiscnt_TextChanged(null, null);

    }


    protected void btnsave_Click(object sender, EventArgs e)
    {

        PatientBillTransactionTableLogic_Bal_C objBilltrans = new PatientBillTransactionTableLogic_Bal_C();
        try
        {
            if (txtOtherCharges.Text.Trim() == "")
            {
                txtOtherCharges.Text = "0";
            }
            if (txtDiscnt.Text != "0")
            {
                if (txtRemark.Text == "")
                {
                    txtRemark.Focus();
                    txtRemark.BackColor = Color.Red;
                    return ;
                }
                else
                {
                    txtRemark.BackColor = Color.White;
                }
            }
            if (RadioButtonList1.Items[0].Selected || RadioButtonList1.Items[1].Selected || RadioButtonList1.Items[2].Selected || RadioButtonList1.Items[3].Selected)
            {
            }
            else
            {
                RadioButtonList1.Focus();
                RadioButtonList1.BackColor = Color.Red;
                string AA = "Please select payment type ";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "", "<script>alert('" + AA.ToString() + "');</script>", false);

                return ;
            }
            Ledgrmst_Bal_C led = new Ledgrmst_Bal_C();
                   DataTable dtother = new DataTable();
                   dtother = led.GetOther_Charges(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]));
                        float OtCharge = 0;
                        int Ocharge = 0;
                        if (dtother.Rows.Count > 0)
                        {
                            OtCharge = Convert.ToSingle( dtother.Rows[0]["Othercharges"]);
                            if (OtCharge != Convert.ToSingle(txtOtherCharges.Text))
                            {
                                Ocharge = 1;

                                led.Update_OtherCharges(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), Session["username"].ToString(), Convert.ToString(Request.QueryString["FID"]), Convert.ToSingle(txtOtherCharges.Text),Convert.ToString(txtOtherchargeRemark.Text));

                            }
                        }
             if (Convert.ToSingle(txtAmtPaid.Text) != 0 || Ocharge==1)
            {
                string C_Code = null;
                string Partstring = null;
                string BillFormatstr = null;
                string v = ViewState["SaveEdit"].ToString();
                if (ViewState["SaveEdit"].ToString() != "Edit")
                {
                    

                    if ((Convert.ToSingle(txtAmtPaid.Text)) > Convert.ToSingle(lbltestcharges.Text))
                    {
                        //txtAmtPaid.Text = "0";
                        lblmsg.Visible = true;
                        Label12.Visible = false;
                        return;
                    }
                    else
                    {
                        lblmsg.Visible = false;
                    }
                   
                    //if (RadioButtonList2.Items[0].Selected)
                    //{
                    //    if (Convert.ToSingle(txtDiscnt.Text) <= 100)
                    //    {
                    //        // discnt = Convert.ToInt32(lbltestcharges.Text) * Convert.ToInt32(txtDiscnt.Text) / 100;
                    //        Label12.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        Label12.Visible = true;
                    //        Label12.Text = "Discount Percentage  Less or Equal to 100 %";
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                        if ((Convert.ToSingle(txtDiscnt.Text)) > Convert.ToSingle(lbltestcharges.Text))
                        {
                            //txtDiscnt.Text = "0";
                            Label12.Visible = true;
                            lblmsg.Visible = false;
                            Label12.Text = "Discount amount Less or Equal to Total Bill";
                            return;
                        }
                        else
                        {
                            Label12.Visible = false;
                        }
                   // }
                  
                    if (objBilltrans.billnowithPID(Convert.ToInt32(Request.QueryString["PID"])))
                    {

                        Cshmst_Bal_C Obj_CSH = new Cshmst_Bal_C();
                        Obj_CSH.P_Centercode = ViewState["CenterCode"].ToString();
                        Obj_CSH.RecDate = Convert.ToDateTime(txtBillDate.Text);//System.DateTime.Now;                    
                        Obj_CSH.BillType = "Cash Bill";
                       // Obj_CSH.AmtReceived = Convert.ToSingle(lbltestcharges.Text);
                        Obj_CSH.AmtReceived = Convert.ToSingle(txtAmtPaid.Text);
                        if (txtDiscnt.Text != "")
                        {
                            Obj_CSH.NetPayment = Convert.ToSingle(txtNetPayment.Text);
                            Obj_CSH.Discount = txtDiscnt.Text;
                            Obj_CSH.Discount = Convert.ToString( ViewState["Discnt"]);
                        }
                        else
                        {
                            Obj_CSH.NetPayment = Convert.ToSingle(lbltestcharges.Text);
                            Obj_CSH.Discount = "0";
                        }

                        Obj_CSH.patRegID = lblRegNo.Text;
                        Obj_CSH.Patientname = lblName.Text;
                        Obj_CSH.Patienttest = ViewState["STCODE"].ToString();
                        Obj_CSH.Remark = txtRemark.Text;
                        Obj_CSH.PID = Convert.ToInt32(Request.QueryString["PID"]);
                        if (txtAmtPaid.Text != "")
                            Obj_CSH.AmtPaid = Convert.ToSingle(txtAmtPaid.Text);
                        else
                            Obj_CSH.AmtPaid = 0;
                        if (txtBalance.Text != "")
                            Obj_CSH.Balance = Convert.ToSingle(txtBalance.Text);
                        else
                            Obj_CSH.Balance = Convert.ToSingle(lbltestcharges.Text);
                        Obj_CSH.Paymenttype = RadioButtonList1.SelectedItem.Text;

                        if (RadioButtonList1.Items[1].Selected)
                        {
                            if (txtNo.Text.Trim() != "" && txtBankName.Text.Trim() != "" && txtdate.Text.Trim() != "")
                            {
                                Obj_CSH.ChqNo = txtNo.Text;
                                Obj_CSH.ChqDate = DateTimeConvesion.getDateFromString(txtdate.Text);
                                Obj_CSH.BankName = txtBankName.Text;
                                Label25.Visible = false;
                            }
                            else
                            {
                                Label25.Visible = true;
                                // return;
                            }

                        }
                        else if (RadioButtonList1.Items[2].Selected)
                        {
                            Obj_CSH.CardNo = txtNo.Text;
                            Obj_CSH.CardTransID = txtBankName.Text;
                        }
                        else if (RadioButtonList1.Items[3].Selected)
                        {
                            Obj_CSH.OnlineType = txtOnlineType.Text;
                            Obj_CSH.OnlinetransID = txtOnlineTraansId.Text;
                        }
                        Obj_CSH.username = Session["username"].ToString();

                        Obj_CSH.Othercharges = 0;

                        if (RadioButtonList2.Items[0].Selected)
                        {
                            Obj_CSH.DisFlag = false;
                        }
                        else
                        {
                            Obj_CSH.DisFlag = true;
                        }
                        Obj_CSH.P_DigModule = Convert.ToInt32(Session["DigModule"]);
                        if (lblBillNo.Text == "0")
                        {
                            int mno = Cshmst_Bal_C.getMaxNumber(Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                            Obj_CSH.BillNo = mno;
                            lblBillNo.Text = Convert.ToString(Obj_CSH.BillNo);
                        }
                        else
                        {

                        }

                        int bno = Convert.ToInt32(lblBillNo.Text);
                        Obj_CSH.BillNo = bno;


                        Obj_CSH.Insert(Convert.ToInt32(Session["Branchid"]));
                        //ledger transaction Insert
                        C_Code = ViewState["CenterCode"].ToString();
                     
                       
                        led.RegDate = DateTimeConvesion.getDateFromString(txtBillDate.Text);
                        if (txtAmtPaid.Text != "")
                            led.CreditAmt = Convert.ToSingle(txtAmtPaid.Text);
                        else
                            led.CreditAmt = 0;
                        led.DebitAmt = 0;

                        led.CenterCode = DrMT_sign_Bal_C.GetSingleCenter(C_Code, Convert.ToInt32(Session["Branchid"]));
                        led.BillNo = bno;// Convert.ToInt32(lblBillNo.Text);
                        led.BillFormat = "";

                      
                        DataTable dtgap = new DataTable();
                        dtgap = led.GetAmountPaid(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), bno);
                        float Billamount = 0;
                        if (dtgap.Rows.Count > 0)
                        {
                            led.UpdateBillPreviewBal(Convert.ToInt32(bno), Convert.ToInt32(Session["Branchid"]), Session["username"].ToString(), Convert.ToString(Request.QueryString["FID"]));
                            Billamount = 0;
                        }
                        else
                        {
                            Billamount = Convert.ToSingle(lbltestcharges.Text);
                        }
                       // led.P_ReceiptNO = DrMT_sign_Bal_C.GetReceiptno(Convert.ToString(Request.QueryString["FID"]), Convert.ToInt32(Session["Branchid"]));
                        int RecNo = Obj1.Insert_Update_ReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Request.QueryString["FID"]));
                        led.P_ReceiptNO = Convert.ToString( RecNo);
                        SqlConnection conn = DataAccess.ConInitForDC();
                        SqlCommand sc = new SqlCommand("insert into RecM(BillNo,BillDate,AmtPaid,Paymenttype,BankName,branchid,transdate,username,BillAmt,DisAmt,BalAmt,PID,PrevBal,FID,ReceiptNo,DiscountPerformTo,LabGiven,DrGiven,AccNo,ChqNo,ChqDate,CardNo,CardName,Cardtype,CardExpiryDate,CardTransactionID,OnlineTransType,OnlineTransID)" +
                        "values(@BillNo,@RecDate,@AmtPaid,@Paymenttype,@BankName,@branchid,@transdate,@username,@BillAmt,@DisAmt,@BalAmt,@PID,@PrevBal,@FID,@ReceiptNo,@DiscountPerformTo,@LabGiven,@DrGiven,@AccNo,@ChqNo,@ChqDate,@CardNo,@CardName,@Cardtype,@ExpiryDate,@CardTransactionID,@OnlineTransType,@OnlineTransID)", conn);
                        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;// Convert.ToInt32(lblBillNo.Text);
                        if (txtAmtPaid.Text != "")
                            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = Convert.ToSingle(txtAmtPaid.Text);
                        else
                            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0;
                        sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtBillDate.Text);
                        sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = RadioButtonList1.SelectedItem.Text;
                        sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 50)).Value = txtBankName.Text;
                        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                        sc.Parameters.Add(new SqlParameter("@transdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy"));//Convert.ToDateTime(System.DateTime.Now);
                        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = Session["username"].ToString();
                        sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = Convert.ToSingle(Billamount);
                        sc.Parameters.Add(new SqlParameter("@DisAmt", SqlDbType.Float)).Value = Convert.ToDouble(Obj_CSH.Discount);
                        sc.Parameters.Add(new SqlParameter("@BalAmt", SqlDbType.Float)).Value = Convert.ToDouble(txtBalance.Text);
                        sc.Parameters.AddWithValue("@PID", Convert.ToInt32(Request.QueryString["PID"]));

                        sc.Parameters.Add(new SqlParameter("@PrevBal", SqlDbType.Float)).Value = Convert.ToDouble(txtBalance.Text);
                        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.Int)).Value = Convert.ToInt32(Request.QueryString["FID"]);

                        sc.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.Int)).Value = Convert.ToString(led.P_ReceiptNO);

                        if (txtDisLabgiven.Text != "")
                            sc.Parameters.Add(new SqlParameter("@LabGiven", SqlDbType.Float)).Value = Convert.ToSingle(txtDisLabgiven.Text);
                        else
                            sc.Parameters.Add(new SqlParameter("@LabGiven", SqlDbType.Float)).Value = 0;
                        if (txtDisdrgiven.Text != "")
                            sc.Parameters.Add(new SqlParameter("@DrGiven", SqlDbType.Float)).Value = Convert.ToSingle(txtDisdrgiven.Text);
                        else
                            sc.Parameters.Add(new SqlParameter("@DrGiven", SqlDbType.Float)).Value = 0;
                       
                            sc.Parameters.Add(new SqlParameter("@DiscountPerformTo", SqlDbType.Int)).Value = Convert.ToInt32(RblDiscgiven.SelectedValue);

                           
                                sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = "";

                                if (txtNo.Text != "")
                                    sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = txtNo.Text;
                            else
                                sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = "";

                                if (Obj_CSH.ChqDate != Date.getMinDate())
                                    sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = Obj_CSH.ChqDate;
                            else
                                sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = DBNull.Value;

                                if (Obj_CSH.CardNo != null)
                                    sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = Obj_CSH.CardNo;
                            else
                                sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = "";

                           
                                sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = "";

                           
                                sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = "";
                        
                                sc.Parameters.Add(new SqlParameter("@CardExpiryDate", SqlDbType.DateTime)).Value = DBNull.Value;
                            if (Obj_CSH.CardTransID != null)
                                sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = Obj_CSH.CardTransID;
                            else
                                sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = "";
                            if (Obj_CSH.OnlineType != null)
                                sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = Obj_CSH.OnlineType;
                            else
                                sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = "";
                            if (Obj_CSH.OnlinetransID != null)
                                sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = Obj_CSH.OnlinetransID;
                            else
                                sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = ""; 

                        conn.Open();
                        sc.ExecuteNonQuery();

                        ObjTB.P_Patregno = lblRegNo.Text;
                        ObjTB.P_FormName = "Pay Bill Desk";
                        ObjTB.P_EventName = "Receive Payment";
                        ObjTB.P_UserName = Convert.ToString(Session["username"]);
                        ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                        ObjTB.Insert_DailyActivity();

                        Label12.Visible = true;
                        Label12.Text = "Record save successfully";
                        lblBillNo.Text = bno.ToString();
                        ViewState["btnsave"] = "true";
                        btnsave.Enabled = false;
                        lblAdvance.Visible = true;
                        Bindgrid(bno);
                        lblAdvanceAmt.Visible = true;
                        if (lblAdvanceAmt.Text != "")
                        {
                            lblAdvanceAmt.Text = Convert.ToString(Convert.ToSingle(lblAdvanceAmt.Text) + Obj_CSH.AmtPaid);
                        }
                        else
                        {
                            lblAdvanceAmt.Text = Obj_CSH.AmtPaid.ToString();
                        }
                        txtAmtPaid.Text = Convert.ToString(0);

                    }
                    if (Request.QueryString["Refund"] == "Yes")
                    {
                        Cshmst_Bal_C Obj_CSH = new Cshmst_Bal_C();
                       // Obj_CSH.update_Fullbillcancel(Convert.ToInt32(Request.QueryString["PID"]), 1);

                        int bno = Convert.ToInt32(lblBillNo.Text);
                        Obj_CSH.BillNo = bno;
                      //  led.P_ReceiptNO = DrMT_sign_Bal_C.GetReceiptno(Convert.ToString(Request.QueryString["FID"]), Convert.ToInt32(Session["Branchid"]));
                        int RecNo = Obj1.Insert_Update_ReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Request.QueryString["FID"]));
                        led.P_ReceiptNO = Convert.ToString( RecNo);
                        SqlConnection conn = DataAccess.ConInitForDC();
                        SqlCommand sc = new SqlCommand("insert into RecM(BillNo,BillDate,AmtPaid,Paymenttype,BankName,branchid,transdate,username,BillAmt,DisAmt,BalAmt,PID,PrevBal,IsRefund,FID,ReceiptNo)" +
                        "values(@BillNo,@RecDate,@AmtPaid,@Paymenttype,@BankName,@branchid,@transdate,@username,@BillAmt,@DisAmt,@BalAmt,@PID,@PrevBal,@IsRefund,@FID,@ReceiptNo)", conn);
                        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;// Convert.ToInt32(lblBillNo.Text);
                        if (txtAmtPaid.Text != "")
                            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = Convert.ToSingle(txtAmtPaid.Text);
                        else
                            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0;
                        sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtBillDate.Text);
                        sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = RadioButtonList1.SelectedItem.Text;
                        sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 50)).Value = txtBankName.Text;
                        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                        sc.Parameters.Add(new SqlParameter("@transdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy"));//Convert.ToDateTime(System.DateTime.Now);
                        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = Session["username"].ToString();
                        sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = Convert.ToSingle(0);
                        sc.Parameters.Add(new SqlParameter("@DisAmt", SqlDbType.Float)).Value = Convert.ToDouble(0);
                        sc.Parameters.Add(new SqlParameter("@BalAmt", SqlDbType.Float)).Value = Convert.ToDouble(0);
                        sc.Parameters.AddWithValue("@PID", Convert.ToInt32(Request.QueryString["PID"]));

                        sc.Parameters.Add(new SqlParameter("@PrevBal", SqlDbType.Float)).Value = Convert.ToDouble(0);
                        sc.Parameters.Add(new SqlParameter("@IsRefund", SqlDbType.Bit)).Value = Convert.ToBoolean(1);
                        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.Int)).Value = Convert.ToInt32(Request.QueryString["FID"]);

                        sc.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.Int)).Value = Convert.ToString(led.P_ReceiptNO);

                        sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = "";

                        if (txtNo.Text != "")
                            sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = txtNo.Text;
                        else
                            sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = "";

                        if (Obj_CSH.ChqDate != Date.getMinDate())
                            sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = Obj_CSH.ChqDate;
                        else
                            sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = DBNull.Value;

                        if (Obj_CSH.CardNo != null)
                            sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = Obj_CSH.CardNo;
                        else
                            sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = "";


                        sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = "";


                        sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = "";

                        sc.Parameters.Add(new SqlParameter("@CardExpiryDate", SqlDbType.DateTime)).Value = DBNull.Value;
                        if (Obj_CSH.CardTransID != null)
                            sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = Obj_CSH.CardTransID;
                        else
                            sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = "";
                        if (Obj_CSH.OnlineType != null)
                            sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = Obj_CSH.OnlineType;
                        else
                            sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = "";
                        if (Obj_CSH.OnlinetransID != null)
                            sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = Obj_CSH.OnlinetransID;
                        else
                            sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = ""; 
                        conn.Open();
                        sc.ExecuteNonQuery();

                        Obj_CSH.update_RefundAmount(Convert.ToInt32(Request.QueryString["PID"]), 1, Convert.ToSingle( txtDiscnt.Text), Convert.ToSingle(txtAmtPaid.Text));
                        //Obj_CSH.update_RefundAmount_RecMst(Convert.ToInt32(Request.QueryString["PID"]), 1, Convert.ToSingle(txtDiscnt.Text), Convert.ToSingle(txtAmtPaid.Text));
                        Label12.Visible = true;
                        Label12.Text = "Refund Payment successfully";
                        ObjTB.P_Patregno = lblRegNo.Text;
                        ObjTB.P_FormName = "Pay Bill Desk";
                        ObjTB.P_EventName = "Refund Payment";
                        ObjTB.P_UserName = Convert.ToString(Session["username"]);
                        ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                        ObjTB.Insert_DailyActivity();
                    }
                   

                }
                else
                {
                    if (Convert.ToSingle(txtAmtPaid.Text) > 0)
                    {
                        if (Convert.ToString(txtAmtPaid.Text).Contains("-"))
                        {
                            if ((Convert.ToSingle(txtAmtPaid.Text)) < ((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(lblAdvanceAmt.Text)))
                            {

                                lblmsg.Visible = true;
                                Label12.Visible = false;
                                return;
                            }
                            else
                            {
                                lblmsg.Visible = false;
                            }
                        }
                        else
                        {
                           // if ((Convert.ToSingle(txtAmtPaid.Text)) > ((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(lblAdvanceAmt.Text)))
                            if ((Convert.ToSingle(txtAmtPaid.Text)) > ((Convert.ToSingle(lbltestcharges.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(lblAdvanceAmt.Text)))
                                  {
                                //txtAmtPaid.Text = "0";
                                lblmsg.Visible = true;
                                Label12.Visible = false;
                                return;
                            }
                            else
                            {
                                lblmsg.Visible = false;
                            }
                        }
                    }
                   
                    //if (RadioButtonList2.Items[0].Selected)
                    //{
                    //    if (Convert.ToSingle(txtDiscnt.Text) <= 100)
                    //    {

                    //        Label12.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        Label12.Visible = true;
                    //        Label12.Text = "Discount Percentage  Less or Equal to 100 %";
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                        if ((Convert.ToSingle(txtDiscnt.Text)) > Convert.ToSingle(lbltestcharges.Text))
                        {
                            //txtDiscnt.Text = "0";
                            Label12.Visible = true;
                            lblmsg.Visible = false;
                            Label12.Text = "Discount amt Less or Equal to total bill amount";
                            return;
                        }
                        else
                        {
                            Label12.Visible = false;
                        }
                   // }
                    string CCount = objBilltrans.GetSMSString_CountryCode("Registration", Convert.ToInt16(Session["Branchid"]));
                    int CountryC = 0;
                    if (CCount.Length == 2)
                    {
                        CountryC = 2;
                    }
                    else
                    {
                        CountryC = 3;
                    }
                    if (objBilltrans.billtransactionwithdate(DateTime.Now.AddMinutes(-2), Convert.ToInt32(lblBillNo.Text), CountryC))
                    {
                        Cshmst_Bal_C Obj_CSH = new Cshmst_Bal_C();
                        Obj_CSH.P_Centercode = ViewState["CenterCode"].ToString();
                        Obj_CSH.BillNo = Convert.ToInt32(lblBillNo.Text);
                        if (txtBillDate.Text != "")
                        {
                            Obj_CSH.RecDate = Convert.ToDateTime(txtBillDate.Text);
                        }
                        else
                        { Obj_CSH.RecDate = System.DateTime.Now; }
                        Obj_CSH.BillType = "Cash Bill";
                        //Obj_CSH.AmtReceived = Convert.ToSingle(lbltestcharges.Text) + Convert.ToSingle(txthstamount.Text);
                        Obj_CSH.AmtReceived = Convert.ToSingle(txtAmtPaid.Text) + Convert.ToSingle(txthstamount.Text);
                        if (txtDiscnt.Text != "")
                        {
                            Obj_CSH.NetPayment = Convert.ToSingle(lbltestcharges.Text);//txtNetPayment
                            Obj_CSH.Discount = txtDiscnt.Text;
                            Obj_CSH.Discount = Convert.ToString(ViewState["Discnt"]);
                        }
                        else
                        {
                            Obj_CSH.NetPayment = Convert.ToSingle(lbltestcharges.Text);
                            Obj_CSH.Discount = "0";
                        }
                        Obj_CSH.patRegID = lblRegNo.Text;
                        Obj_CSH.Patientname = lblName.Text;
                        Obj_CSH.Patienttest = ViewState["test"].ToString();
                        Obj_CSH.Remark = txtRemark.Text;
                        Obj_CSH.PID = Convert.ToInt32(ViewState["PID"]);
                        if (Convert.ToSingle(txtBalance.Text) < 0)
                        {
                            float ap = Convert.ToSingle(ViewState["amtPaid"]);
                            ap = ap + Convert.ToSingle(txtAmtPaid.Text) + Convert.ToSingle(txtBalance.Text);
                            Obj_CSH.AmtPaid = ap;
                        }
                        else
                        {
                            float ap = Convert.ToSingle(ViewState["amtPaid"]);
                            ap = ap + Convert.ToSingle(txtAmtPaid.Text);
                            Obj_CSH.AmtPaid = ap;
                        }
                        if (txtBalance.Text != "")
                            Obj_CSH.Balance = Convert.ToSingle(txtBalance.Text);
                        else
                            Obj_CSH.Balance = Convert.ToSingle(lbltestcharges.Text);

                        Obj_CSH.Paymenttype = RadioButtonList1.SelectedItem.Text;

                        if (RadioButtonList1.Items[1].Selected)
                        {
                            Obj_CSH.ChqNo = txtNo.Text;
                            Obj_CSH.ChqDate = DateTimeConvesion.getDateFromString(txtdate.Text);
                            Obj_CSH.BankName = txtBankName.Text;
                        }
                        else if (RadioButtonList1.Items[2].Selected)
                        {
                            Obj_CSH.CardNo = txtNo.Text;
                            Obj_CSH.CardTransID = txtBankName.Text;
                        }
                        else if (RadioButtonList1.Items[3].Selected)
                        {
                            Obj_CSH.OnlineType = txtOnlineType.Text;
                            Obj_CSH.OnlinetransID = txtOnlineTraansId.Text;
                        }
                        if (txtOtherCharges.Text == "")
                        {
                            txtOtherCharges.Text = "0";
                        }
                        Obj_CSH.Othercharges = Convert.ToSingle( txtOtherCharges.Text);
                        Obj_CSH.P_OtherChargeRemark = Convert.ToString(txtOtherchargeRemark.Text);

                        Obj_CSH.username = Session["username"].ToString();

                        if (RadioButtonList2.Items[0].Selected)
                        {
                            Obj_CSH.DisFlag = false;
                        }
                        else
                        {
                            Obj_CSH.DisFlag = true;
                        }
                        bool disupdate = false;
                        if (hdfielddiscontFlag.Value != Obj_CSH.DisFlag.ToString() || hdfielddiscont.Value != txtDiscnt.Text)
                        {
                            disupdate = true;
                        }


                        DataTable dtdis = new DataTable();
                        dtdis = Obj_CSH.GetDiscountExist(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));

                     //   Ledgrmst_Bal_C led = new Ledgrmst_Bal_C();

                        float ChangedBillAmt = 0;
                        if (Convert.ToString(ViewState["TotalBillAmt"]) != null && Convert.ToString(ViewState["TotalBillAmt"]) != "")
                        {
                            float CurrentBillAmt = Convert.ToSingle(ViewState["TotalBillAmt"]);
                            float PaidBillAmt = led.GetAmtReceived(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                            float OtherCharges = led.GetOtherCharges(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
                            if (OtherCharges != 0)
                            {
                                PaidBillAmt = PaidBillAmt - OtherCharges;
                            }
                            if (CurrentBillAmt > PaidBillAmt)
                            {
                                ChangedBillAmt = CurrentBillAmt - PaidBillAmt;

                            }
                        }

                        Obj_CSH.P_Hstper = Convert.ToSingle(Session["Taxper"]);
                        Obj_CSH.P_Hstamount = Convert.ToSingle(txthstamount.Text);
                        if (Convert.ToSingle(txtAmtPaid.Text) < 0)
                        {
                            Obj_CSH.P_RefundAmt = Convert.ToSingle(txtAmtPaid.Text);
                        }
                        ObjTB.P_Patregno = lblRegNo.Text;
                        ObjTB.P_FormName = "Pay Bill Disk";
                        ObjTB.P_EventName = "Receive Payment";
                        ObjTB.P_UserName = Convert.ToString(Session["username"]);
                        ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                        ObjTB.Insert_DailyActivity();
                       // Obj_CSH.Update(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), disupdate,Convert.ToString(Request.QueryString["FID"]));

                        C_Code = ViewState["CenterCode"].ToString();
                       
                        led.ModeOfPayment = RadioButtonList1.SelectedItem.Text;
                        led.RegDate = DateTimeConvesion.getDateFromString(txtBillDate.Text);

                        if (txtAmtPaid.Text != "")
                            led.CreditAmt = Convert.ToSingle(txtAmtPaid.Text);
                        else
                            led.CreditAmt = 0;
                        led.DebitAmt = 0;
                        // led.ParticularField = Partstring.ToString();
                        led.CenterCode = DrMT_sign_Bal_C.GetSingleCenter(C_Code, Convert.ToInt32(Session["Branchid"]));
                        led.BillNo = Convert.ToInt32(lblBillNo.Text);
                        led.BillFormat = "";
                        string currentdate = Date.getdate().ToString("dd/MM/yyyy");

                        Ledgrmst_Bal_C ld1 = new Ledgrmst_Bal_C();
                        int billno2 = Convert.ToInt32(lblBillNo.Text);
                        int Branchid_T = Convert.ToInt32(Session["Branchid"]);

                       

                        led.UpdateBillTransBal(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), Session["username"].ToString(),Convert.ToString(Request.QueryString["FID"]));
                        if (dtdis.Rows.Count != 0)
                        {
                            if (dtdis.Rows[0]["Discount"].ToString() == txtDiscnt.Text)
                            {
                                Obj_CSH.Discount = "0";
                            }
                            if (Convert.ToSingle(txtDiscnt.Text) > Convert.ToSingle(dtdis.Rows[0]["Discount"].ToString()))
                            {
                                string ss = Convert.ToString(Convert.ToSingle(ViewState["Discnt"]) - Convert.ToSingle(dtdis.Rows[0]["Discount"].ToString()));
                                Obj_CSH.Discount = ss;
                                ObjTB.P_Patregno = lblRegNo.Text;
                                ObjTB.P_FormName = "Pay Bill Desk";
                                ObjTB.P_EventName = "Update Discount";
                                ObjTB.P_UserName = Convert.ToString(Session["username"]);
                                ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                                ObjTB.Insert_DailyActivity();
                            }

                            if (Convert.ToSingle(txtDiscnt.Text) < Convert.ToSingle(dtdis.Rows[0]["Discount"].ToString()))
                            {
                                string ss1 = Convert.ToString(Convert.ToSingle(ViewState["Discnt"]) - Convert.ToSingle(dtdis.Rows[0]["Discount"].ToString()));
                                Obj_CSH.Discount = ss1;
                                ObjTB.P_Patregno = lblRegNo.Text;
                                ObjTB.P_FormName = "Pay Bill Desk";
                                ObjTB.P_EventName = "Update Discount";
                                ObjTB.P_UserName = Convert.ToString(Session["username"]);
                                ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                                ObjTB.Insert_DailyActivity();
                            }
                        }
                        DataTable dtgap = new DataTable();
                        dtgap = led.GetAmountPaid(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(lblBillNo.Text));
                        float Billamount = 0;
                        if (dtgap.Rows.Count > 0)
                        {
                            led.UpdateBillPreviewBal(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), Session["username"].ToString(),Convert.ToString(Request.QueryString["FID"]));

                            Billamount = 0;
                        }
                        else
                        {
                            Billamount = Convert.ToSingle(lbltestcharges.Text);
                        }

                      //  led.P_ReceiptNO = DrMT_sign_Bal_C.GetReceiptno(Convert.ToString(Request.QueryString["FID"]), Convert.ToInt32(Session["Branchid"]));
                       
                        int RecNo = Obj1.Insert_Update_ReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Request.QueryString["FID"]));
                        led.P_ReceiptNO = Convert.ToString( RecNo);
                        SqlConnection conn = DataAccess.ConInitForDC();
                        SqlCommand sc = new SqlCommand("insert into RecM(BillNo,BillDate,AmtPaid,Paymenttype,BankName,branchid,transdate,username,BillAmt,DisAmt,BalAmt,PID,PrevBal,TaxPer,TaxAmount,FID,ReceiptNo,DiscountPerformTo,LabGiven,DrGiven)" +
                        "values(@BillNo,@RecDate,@AmtPaid,@Paymenttype,@BankName,@branchid,@transdate,@username,@BillAmt,@DisAmt,@BalAmt,@PID,@PrevBal,@TaxPer,@TaxAmount,@FID,@ReceiptNo,@DiscountPerformTo,@LabGiven,@DrGiven)", conn);
                        sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = Convert.ToInt32(lblBillNo.Text);
                        if (txtAmtPaid.Text != "")
                            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = Convert.ToDouble(txtAmtPaid.Text);
                        else
                            sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0;
                        sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtBillDate.Text);
                        sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = RadioButtonList1.SelectedItem.Text;
                        sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 50)).Value = txtBankName.Text;
                        sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                        sc.Parameters.Add(new SqlParameter("@transdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy")); //Convert.ToDateTime(System.DateTime.Now);
                        sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = Session["username"].ToString();
                        sc.Parameters.Add(new SqlParameter("@BalAmt", SqlDbType.Float)).Value = Convert.ToDouble(txtBalance.Text);
                        sc.Parameters.Add(new SqlParameter("@DisAmt", SqlDbType.Float)).Value = Convert.ToDouble(Obj_CSH.Discount);
                        sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = Billamount;// ChangedBillAmt;//Convert.ToSingle(lbltestcharges.Text)                   
                        //    sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = 0;//Convert.ToSingle(lbltestcharges.Text)                   

                        sc.Parameters.AddWithValue("@PID", Convert.ToInt32(Request.QueryString["PID"]));
                        sc.Parameters.Add(new SqlParameter("@PrevBal", SqlDbType.Float)).Value = Convert.ToDouble(txtBalance.Text);
                        sc.Parameters.Add(new SqlParameter("@TaxPer", SqlDbType.Float)).Value = Convert.ToDouble(Session["Taxper"]);
                        sc.Parameters.Add(new SqlParameter("@TaxAmount", SqlDbType.Float)).Value = Convert.ToDouble(txthstamount.Text);
                        sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.Int)).Value = Convert.ToInt32(Request.QueryString["FID"]);

                        sc.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.Int)).Value = Convert.ToString(led.P_ReceiptNO);
                        if (txtDisLabgiven.Text != "")
                            sc.Parameters.Add(new SqlParameter("@LabGiven", SqlDbType.Float)).Value = Convert.ToSingle(txtDisLabgiven.Text);
                        else
                            sc.Parameters.Add(new SqlParameter("@LabGiven", SqlDbType.Float)).Value = 0;
                        if (txtDisdrgiven.Text != "")
                            sc.Parameters.Add(new SqlParameter("@DrGiven", SqlDbType.Float)).Value = Convert.ToSingle(txtDisdrgiven.Text);
                        else
                            sc.Parameters.Add(new SqlParameter("@DrGiven", SqlDbType.Float)).Value = 0;

                        sc.Parameters.Add(new SqlParameter("@DiscountPerformTo", SqlDbType.Int)).Value = Convert.ToInt32(RblDiscgiven.SelectedValue);

                        sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = "";

                        if (txtNo.Text != "")
                            sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = txtNo.Text;
                        else
                            sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = "";

                        if (Obj_CSH.ChqDate != Date.getMinDate())
                            sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = Obj_CSH.ChqDate;
                        else
                            sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = DBNull.Value;

                        if (Obj_CSH.CardNo != null)
                            sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = Obj_CSH.CardNo;
                        else
                            sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = "";


                        sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = "";


                        sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = "";

                        sc.Parameters.Add(new SqlParameter("@CardExpiryDate", SqlDbType.DateTime)).Value = DBNull.Value;
                        if (Obj_CSH.CardTransID != null)
                            sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = Obj_CSH.CardTransID;
                        else
                            sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = "";
                        if (Obj_CSH.OnlineType != null)
                            sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = Obj_CSH.OnlineType;
                        else
                            sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = "";
                        if (Obj_CSH.OnlinetransID != null)
                            sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = Obj_CSH.OnlinetransID;
                        else
                            sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = ""; 

                        conn.Open();
                        sc.ExecuteNonQuery();

                        Bindgrid(Convert.ToInt32(lblBillNo.Text));
                        Label12.Visible = true;
                        Label12.Text = "Record updated successfully";
                        ViewState["btnsave"] = "true";
                        btnsave.Enabled = false;
                        lblAdvance.Visible = true;
                        lblAdvanceAmt.Visible = true;
                        if (lblAdvanceAmt.Text != "")
                        {
                            lblAdvanceAmt.Text = Convert.ToString(Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(txtAmtPaid.Text));
                        }
                        else
                        {
                            lblAdvanceAmt.Text = txtAmtPaid.Text;
                        }
                        txtAmtPaid.Text = Convert.ToString(0);
                        //ClearControls();
                    }
                }
                    
            }//
            else
            {
                if (Convert.ToSingle(txtDiscnt.Text )> 0)
                {
                    string C_Code = null;
                    string Partstring = null;
                    string BillFormatstr = null;
                    string v = ViewState["SaveEdit"].ToString();
                    if (ViewState["SaveEdit"].ToString() != "Edit")
                    {
                        

                        if ((Convert.ToSingle(txtAmtPaid.Text)) > Convert.ToSingle(lbltestcharges.Text))
                        {
                            //txtAmtPaid.Text = "0";
                            lblmsg.Visible = true;
                            Label12.Visible = false;
                            return;
                        }
                        else
                        {
                            lblmsg.Visible = false;
                        }
                       

                       
                        //if (RadioButtonList2.Items[0].Selected)
                        //{
                        //    if (Convert.ToSingle(txtDiscnt.Text) <= 100)
                        //    {
                        //        // discnt = Convert.ToInt32(lbltestcharges.Text) * Convert.ToInt32(txtDiscnt.Text) / 100;
                        //        Label12.Visible = false;
                        //    }
                        //    else
                        //    {
                        //        Label12.Visible = true;
                        //        Label12.Text = "Discount Percentage  Less or Equal to 100 %";
                        //        return;
                        //    }
                        //}
                        //else
                        //{
                            if ((Convert.ToSingle(txtDiscnt.Text)) > Convert.ToSingle(lbltestcharges.Text))
                            {
                                //txtDiscnt.Text = "0";
                                Label12.Visible = true;
                                lblmsg.Visible = false;
                                Label12.Text = "Discount amount Less or Equal to Total Bill";
                                return;
                            }
                            else
                            {
                                Label12.Visible = false;
                            }
                       // }
                      
                        if (objBilltrans.billnowithPID(Convert.ToInt32(Request.QueryString["PID"])))
                        {

                            Cshmst_Bal_C Obj_CSH = new Cshmst_Bal_C();
                            Obj_CSH.P_Centercode = ViewState["CenterCode"].ToString();
                            Obj_CSH.RecDate = Convert.ToDateTime(txtBillDate.Text);//System.DateTime.Now;                    
                            Obj_CSH.BillType = "Cash Bill";
                            //Obj_CSH.AmtReceived = Convert.ToSingle(lbltestcharges.Text);
                            Obj_CSH.AmtReceived = Convert.ToSingle(txtAmtPaid.Text);
                            if (txtDiscnt.Text != "")
                            {
                                Obj_CSH.NetPayment = Convert.ToSingle(txtNetPayment.Text);
                                Obj_CSH.Discount = txtDiscnt.Text;
                                Obj_CSH.Discount = Convert.ToString(ViewState["Discnt"]);
                            }
                            else
                            {
                                Obj_CSH.NetPayment = Convert.ToSingle(lbltestcharges.Text);
                                Obj_CSH.Discount = "0";
                            }

                            Obj_CSH.patRegID = lblRegNo.Text;
                            Obj_CSH.Patientname = lblName.Text;
                            Obj_CSH.Patienttest = ViewState["STCODE"].ToString();
                            Obj_CSH.Remark = txtRemark.Text;
                            Obj_CSH.PID = Convert.ToInt32(Request.QueryString["PID"]);
                            if (txtAmtPaid.Text != "")
                                Obj_CSH.AmtPaid = Convert.ToSingle(txtAmtPaid.Text);
                            else
                                Obj_CSH.AmtPaid = 0;
                            if (txtBalance.Text != "")
                                Obj_CSH.Balance = Convert.ToSingle(txtBalance.Text);
                            else
                                Obj_CSH.Balance = Convert.ToSingle(lbltestcharges.Text);
                            Obj_CSH.Paymenttype = RadioButtonList1.SelectedItem.Text;

                            if (RadioButtonList1.Items[1].Selected)
                            {
                                if (txtNo.Text.Trim() != "" && txtBankName.Text.Trim() != "" && txtdate.Text.Trim() != "")
                                {
                                    Obj_CSH.ChqNo = txtNo.Text;
                                    Obj_CSH.ChqDate = DateTimeConvesion.getDateFromString(txtdate.Text);
                                    Obj_CSH.BankName = txtBankName.Text;
                                    Label25.Visible = false;
                                }
                                else
                                {
                                    Label25.Visible = true;
                                    // return;
                                }

                            }
                            else if (RadioButtonList1.Items[2].Selected)
                            {
                                Obj_CSH.CardNo = txtNo.Text;
                                Obj_CSH.CardTransID = txtBankName.Text;
                            }
                            else if (RadioButtonList1.Items[3].Selected)
                            {
                                Obj_CSH.OnlineType = txtOnlineType.Text;
                                Obj_CSH.OnlinetransID = txtOnlineTraansId.Text;
                            }
                            Obj_CSH.username = Session["username"].ToString();

                            Obj_CSH.Othercharges = 0;

                            if (RadioButtonList2.Items[0].Selected)
                            {
                                Obj_CSH.DisFlag = false;
                            }
                            else
                            {
                                Obj_CSH.DisFlag = true;
                            }
                            Obj_CSH.P_DigModule = Convert.ToInt32(Session["DigModule"]);
                            if (lblBillNo.Text == "0")
                            {
                                int mno = Cshmst_Bal_C.getMaxNumber(Convert.ToInt32(Session["Branchid"]),Convert.ToString(Request.QueryString["FID"]));
                                Obj_CSH.BillNo = mno;
                                lblBillNo.Text = Convert.ToString(Obj_CSH.BillNo);
                            }
                            else
                            {

                            }

                            int bno = Convert.ToInt32(lblBillNo.Text);
                            Obj_CSH.BillNo = bno;


                            Obj_CSH.Insert(Convert.ToInt32(Session["Branchid"]));
                            //ledger transaction Insert
                            ObjTB.P_Patregno = lblRegNo.Text;
                            ObjTB.P_FormName = "Pay Bill Desk";
                            ObjTB.P_EventName = "Receive Payment";
                            ObjTB.P_UserName = Convert.ToString(Session["username"]);
                            ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                            ObjTB.Insert_DailyActivity();
                            C_Code = ViewState["CenterCode"].ToString();
                        
                           // Ledgrmst_Bal_C led = new Ledgrmst_Bal_C();
                            led.RegDate = DateTimeConvesion.getDateFromString(txtBillDate.Text);
                            if (txtAmtPaid.Text != "")
                                led.CreditAmt = Convert.ToSingle(txtAmtPaid.Text);
                            else
                                led.CreditAmt = 0;
                            led.DebitAmt = 0;
                            
                            led.CenterCode = DrMT_sign_Bal_C.GetSingleCenter(C_Code, Convert.ToInt32(Session["Branchid"]));
                            led.BillNo = bno;// Convert.ToInt32(lblBillNo.Text);
                            led.BillFormat = "";

                           
                            DataTable dtgap = new DataTable();
                            dtgap = led.GetAmountPaid(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), bno);
                            float Billamount = 0;
                            if (dtgap.Rows.Count > 0)
                            {
                                led.UpdateBillPreviewBal(Convert.ToInt32(bno), Convert.ToInt32(Session["Branchid"]), Session["username"].ToString(),Convert.ToString(Request.QueryString["FID"]));
                                Billamount = 0;
                            }
                            else
                            {
                                Billamount = Convert.ToSingle(lbltestcharges.Text);
                            }
                           // led.P_ReceiptNO = DrMT_sign_Bal_C.GetReceiptno(Convert.ToString(Request.QueryString["FID"]), Convert.ToInt32(Session["Branchid"]));
                             int RecNo = Obj1.Insert_Update_ReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Request.QueryString["FID"]));
                             led.P_ReceiptNO = Convert.ToString( RecNo);
                            SqlConnection conn = DataAccess.ConInitForDC();
                            SqlCommand sc = new SqlCommand("insert into RecM(BillNo,BillDate,AmtPaid,Paymenttype,BankName,branchid,transdate,username,BillAmt,DisAmt,BalAmt,PID,PrevBal,FID,ReceiptNo)" +
                            "values(@BillNo,@RecDate,@AmtPaid,@Paymenttype,@BankName,@branchid,@transdate,@username,@BillAmt,@DisAmt,@BalAmt,@PID,@PrevBal,@FID,@ReceiptNo)", conn);
                            sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = bno;// Convert.ToInt32(lblBillNo.Text);
                            if (txtAmtPaid.Text != "")
                                sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = Convert.ToSingle(txtAmtPaid.Text);
                            else
                                sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0;
                            sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtBillDate.Text);
                            sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = RadioButtonList1.SelectedItem.Text;
                            sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 50)).Value = txtBankName.Text;
                            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                            sc.Parameters.Add(new SqlParameter("@transdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy"));//Convert.ToDateTime(System.DateTime.Now);
                            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = Session["username"].ToString();
                            sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = Convert.ToSingle(Billamount);
                            sc.Parameters.Add(new SqlParameter("@DisAmt", SqlDbType.Float)).Value = Convert.ToDouble(Obj_CSH.Discount);
                            sc.Parameters.Add(new SqlParameter("@BalAmt", SqlDbType.Float)).Value = Convert.ToDouble(txtBalance.Text);
                            sc.Parameters.AddWithValue("@PID", Convert.ToInt32(Request.QueryString["PID"]));

                            sc.Parameters.Add(new SqlParameter("@PrevBal", SqlDbType.Float)).Value = Convert.ToDouble(txtBalance.Text);
                            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.Int)).Value = Convert.ToInt32(Request.QueryString["FID"]);

                            sc.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.Int)).Value = Convert.ToString(led.P_ReceiptNO);

                            sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = "";

                            if (txtNo.Text != "")
                                sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = txtNo.Text;
                            else
                                sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = "";

                            if (Obj_CSH.ChqDate != Date.getMinDate())
                                sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = Obj_CSH.ChqDate;
                            else
                                sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = DBNull.Value;

                            if (Obj_CSH.CardNo != null)
                                sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = Obj_CSH.CardNo;
                            else
                                sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = "";


                            sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = "";


                            sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = "";

                            sc.Parameters.Add(new SqlParameter("@CardExpiryDate", SqlDbType.DateTime)).Value = DBNull.Value;
                            if (Obj_CSH.CardTransID != null)
                                sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = Obj_CSH.CardTransID;
                            else
                                sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = "";
                            if (Obj_CSH.OnlineType != null)
                                sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = Obj_CSH.OnlineType;
                            else
                                sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = "";
                            if (Obj_CSH.OnlinetransID != null)
                                sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = Obj_CSH.OnlinetransID;
                            else
                                sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = ""; 

                            conn.Open();
                            sc.ExecuteNonQuery();

                           

                            Label12.Visible = true;
                            Label12.Text = "Record save successfully";
                            lblBillNo.Text = bno.ToString();
                            ViewState["btnsave"] = "true";
                            btnsave.Enabled = false;
                            lblAdvance.Visible = true;
                            Bindgrid(bno);
                            lblAdvanceAmt.Visible = true;
                            if (lblAdvanceAmt.Text != "")
                            {
                                lblAdvanceAmt.Text = Convert.ToString(Convert.ToSingle(lblAdvanceAmt.Text) + Obj_CSH.AmtPaid);
                            }
                            else
                            {
                                lblAdvanceAmt.Text = Obj_CSH.AmtPaid.ToString();
                            }
                            txtAmtPaid.Text = Convert.ToString(0);

                        }
                        if (Request.QueryString["Refund"] == "Yes")
                        {
                            Cshmst_Bal_C Obj_CSH = new Cshmst_Bal_C();
                            Obj_CSH.update_Fullbillcancel(Convert.ToInt32(Request.QueryString["PID"]), 1);
                            Label12.Visible = true;
                            Label12.Text = "Refund Payment successfully";
                            ObjTB.P_Patregno = lblRegNo.Text;
                            ObjTB.P_FormName = "Pay Bill Desk";
                            ObjTB.P_EventName = "Refund Payment";
                            ObjTB.P_UserName = Convert.ToString(Session["username"]);
                            ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                            ObjTB.Insert_DailyActivity();
                        }
                        

                    }
                    else
                    {
                        if (Convert.ToString(txtAmtPaid.Text).Contains("-"))
                        {
                            if ((Convert.ToSingle(txtAmtPaid.Text)) < ((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(lblAdvanceAmt.Text)))
                            {

                                lblmsg.Visible = true;
                                Label12.Visible = false;
                                return;
                            }
                            else
                            {
                                lblmsg.Visible = false;
                            }
                        }
                        else
                        {
                            if ((Convert.ToSingle(txtAmtPaid.Text)) > ((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(lblAdvanceAmt.Text)))
                            {
                                //txtAmtPaid.Text = "0";
                                lblmsg.Visible = true;
                                Label12.Visible = false;
                                return;
                            }
                            else
                            {
                                lblmsg.Visible = false;
                            }
                        }
                      
                        //if (RadioButtonList2.Items[0].Selected)
                        //{
                        //    if (Convert.ToSingle(txtDiscnt.Text) <= 100)
                        //    {

                        //        Label12.Visible = false;
                        //    }
                        //    else
                        //    {
                        //        Label12.Visible = true;
                        //        Label12.Text = "Discount Percentage  Less or Equal to 100 %";
                        //        return;
                        //    }
                        //}
                        //else
                        //{
                            if ((Convert.ToSingle(txtDiscnt.Text)) > Convert.ToSingle(lbltestcharges.Text))
                            {
                                //txtDiscnt.Text = "0";
                                Label12.Visible = true;
                                lblmsg.Visible = false;
                                Label12.Text = "Discount amt Less or Equal to total bill amount";
                                return;
                            }
                            else
                            {
                                Label12.Visible = false;
                            }
                      //  }
                        string CCount = objBilltrans.GetSMSString_CountryCode("Registration", Convert.ToInt16(Session["Branchid"]));
                        int CountryC = 0;
                        if (CCount.Length == 2)
                        {
                            CountryC = 2;
                        }
                        else
                        {
                            CountryC = 3;
                        }
                        if (objBilltrans.billtransactionwithdate(DateTime.Now.AddMinutes(-2), Convert.ToInt32(lblBillNo.Text), CountryC))
                        {
                            Cshmst_Bal_C Obj_CSH = new Cshmst_Bal_C();
                            Obj_CSH.P_Centercode = ViewState["CenterCode"].ToString();
                            Obj_CSH.BillNo = Convert.ToInt32(lblBillNo.Text);
                            if (txtBillDate.Text != "")
                            {
                                Obj_CSH.RecDate = Convert.ToDateTime(txtBillDate.Text);
                            }
                            else
                            { Obj_CSH.RecDate = System.DateTime.Now; }
                            Obj_CSH.BillType = "Cash Bill";
                           // Obj_CSH.AmtReceived = Convert.ToSingle(lbltestcharges.Text) + Convert.ToSingle(txthstamount.Text);
                            Obj_CSH.AmtReceived = Convert.ToSingle(txtAmtPaid.Text);
                            if (txtDiscnt.Text != "")
                            {
                                Obj_CSH.NetPayment = Convert.ToSingle(txtNetPayment.Text);
                               // Obj_CSH.Discount = txtDiscnt.Text;
                                Obj_CSH.Discount = Convert.ToString(ViewState["Discnt"]);
                            }
                            else
                            {
                                Obj_CSH.NetPayment = Convert.ToSingle(lbltestcharges.Text);
                                Obj_CSH.Discount = "0";
                            }
                            Obj_CSH.patRegID = lblRegNo.Text;
                            Obj_CSH.Patientname = lblName.Text;
                            Obj_CSH.Patienttest = ViewState["test"].ToString();
                            Obj_CSH.Remark = txtRemark.Text;
                            Obj_CSH.PID = Convert.ToInt32(ViewState["PID"]);
                            if (Convert.ToSingle(txtBalance.Text) < 0)
                            {
                                float ap = Convert.ToSingle(ViewState["amtPaid"]);
                                ap = ap + Convert.ToSingle(txtAmtPaid.Text) + Convert.ToSingle(txtBalance.Text);
                                Obj_CSH.AmtPaid = ap;
                            }
                            else
                            {
                                float ap = Convert.ToSingle(ViewState["amtPaid"]);
                                ap = ap + Convert.ToSingle(txtAmtPaid.Text);
                                Obj_CSH.AmtPaid = ap;
                            }
                            if (txtBalance.Text != "")
                                Obj_CSH.Balance = Convert.ToSingle(txtBalance.Text);
                            else
                                Obj_CSH.Balance = Convert.ToSingle(lbltestcharges.Text);

                            Obj_CSH.Paymenttype = RadioButtonList1.SelectedItem.Text;

                            if (RadioButtonList1.Items[1].Selected)
                            {
                                Obj_CSH.ChqNo = txtNo.Text;
                                Obj_CSH.ChqDate = DateTimeConvesion.getDateFromString(txtdate.Text);
                                Obj_CSH.BankName = txtBankName.Text;
                            }
                            else if (RadioButtonList1.Items[2].Selected)
                            {
                                Obj_CSH.CardNo = txtNo.Text;
                                Obj_CSH.CardTransID = txtBankName.Text;
                            }
                            else if (RadioButtonList1.Items[3].Selected)
                            {
                                Obj_CSH.OnlineType = txtOnlineType.Text;
                                Obj_CSH.OnlinetransID = txtOnlineTraansId.Text;
                            }

                            Obj_CSH.Othercharges = 0;
                            Obj_CSH.username = Session["username"].ToString();

                            if (RadioButtonList2.Items[0].Selected)
                            {
                                Obj_CSH.DisFlag = false;
                            }
                            else
                            {
                                Obj_CSH.DisFlag = true;
                            }
                            bool disupdate = false;
                            if (hdfielddiscontFlag.Value != Obj_CSH.DisFlag.ToString() || hdfielddiscont.Value != txtDiscnt.Text)
                            {
                                disupdate = true;
                            }


                            DataTable dtdis = new DataTable();
                            dtdis = Obj_CSH.GetDiscountExist(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]),Convert.ToString(Request.QueryString["FID"]));

                            //Ledgrmst_Bal_C led = new Ledgrmst_Bal_C();

                            float ChangedBillAmt = 0;
                            if (Convert.ToString(ViewState["TotalBillAmt"]) != null && Convert.ToString(ViewState["TotalBillAmt"]) != "")
                            {
                                float CurrentBillAmt = Convert.ToSingle(ViewState["TotalBillAmt"]);
                                float PaidBillAmt = led.GetAmtReceived(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]),Convert.ToString(Request.QueryString["FID"]));
                                float OtherCharges = led.GetOtherCharges(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]),Convert.ToString(Request.QueryString["FID"]));
                                if (OtherCharges != 0)
                                {
                                    PaidBillAmt = PaidBillAmt - OtherCharges;
                                }
                                if (CurrentBillAmt > PaidBillAmt)
                                {
                                    ChangedBillAmt = CurrentBillAmt - PaidBillAmt;

                                }
                            }

                            Obj_CSH.P_Hstper = Convert.ToSingle(Session["Taxper"]);
                            Obj_CSH.P_Hstamount = Convert.ToSingle(txthstamount.Text);
                           // Obj_CSH.Update(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), disupdate,Convert.ToString(Request.QueryString["FID"]));

                            C_Code = ViewState["CenterCode"].ToString();
                          
                            led.ModeOfPayment = RadioButtonList1.SelectedItem.Text;
                            led.RegDate = DateTimeConvesion.getDateFromString(txtBillDate.Text);

                            if (txtAmtPaid.Text != "")
                                led.CreditAmt = Convert.ToSingle(txtAmtPaid.Text);
                            else
                                led.CreditAmt = 0;
                            led.DebitAmt = 0;
                            // led.ParticularField = Partstring.ToString();
                            led.CenterCode = DrMT_sign_Bal_C.GetSingleCenter(C_Code, Convert.ToInt32(Session["Branchid"]));
                            led.BillNo = Convert.ToInt32(lblBillNo.Text);
                            led.BillFormat = "";
                            string currentdate = Date.getdate().ToString("dd/MM/yyyy");

                            Ledgrmst_Bal_C ld1 = new Ledgrmst_Bal_C();
                            int billno2 = Convert.ToInt32(lblBillNo.Text);
                            int Branchid_T = Convert.ToInt32(Session["Branchid"]);

                            
                                 Label12.Text = "Record saved successfully";
                                 ObjTB.P_Patregno = lblRegNo.Text;
                                 ObjTB.P_FormName = "Pay Bill Desk";
                                 ObjTB.P_EventName = "Receive Payment";
                                 ObjTB.P_UserName = Convert.ToString(Session["username"]);
                                 ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                                 ObjTB.Insert_DailyActivity();
                            led.UpdateBillTransBal(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), Session["username"].ToString(),Convert.ToString(Request.QueryString["FID"]));
                            if (dtdis.Rows.Count != 0)
                            {
                                if (dtdis.Rows[0]["Discount"].ToString() == txtDiscnt.Text)
                                {
                                    Obj_CSH.Discount = "0";
                                }
                                if (Convert.ToSingle(txtDiscnt.Text) > Convert.ToSingle(dtdis.Rows[0]["Discount"].ToString()))
                                {
                                    string ss = Convert.ToString(Convert.ToSingle(txtDiscnt.Text) - Convert.ToSingle(dtdis.Rows[0]["Discount"].ToString()));
                                    Obj_CSH.Discount = ss;
                                    ObjTB.P_Patregno = lblRegNo.Text;
                                    ObjTB.P_FormName = "Pay Bill Desk";
                                    ObjTB.P_EventName = "Update Discount";
                                    ObjTB.P_UserName = Convert.ToString(Session["username"]);
                                    ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                                    ObjTB.Insert_DailyActivity();
                                }

                                if (Convert.ToSingle(txtDiscnt.Text) < Convert.ToSingle(dtdis.Rows[0]["Discount"].ToString()))
                                {
                                    string ss1 = Convert.ToString(Convert.ToSingle(txtDiscnt.Text) - Convert.ToSingle(dtdis.Rows[0]["Discount"].ToString()));
                                    Obj_CSH.Discount = ss1;
                                    ObjTB.P_Patregno = lblRegNo.Text;
                                    ObjTB.P_FormName = "Pay Bill Desk";
                                    ObjTB.P_EventName = "Update Discount";
                                    ObjTB.P_UserName = Convert.ToString(Session["username"]);
                                    ObjTB.P_Branchid = Convert.ToInt32(Session["Branchid"]);
                                    ObjTB.Insert_DailyActivity();
                                }
                            }
                            DataTable dtgap = new DataTable();
                            dtgap = led.GetAmountPaid(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(lblBillNo.Text));
                            float Billamount = 0;
                            if (dtgap.Rows.Count > 0)
                            {
                                led.UpdateBillPreviewBal(Convert.ToInt32(lblBillNo.Text), Convert.ToInt32(Session["Branchid"]), Session["username"].ToString(),Convert.ToString(Request.QueryString["FID"]));

                                Billamount = 0;
                            }
                            else
                            {
                                Billamount = Convert.ToSingle(lbltestcharges.Text);
                            }

                            //led.P_ReceiptNO = DrMT_sign_Bal_C.GetReceiptno(Convert.ToString(Request.QueryString["FID"]), Convert.ToInt32(Session["Branchid"]));
                             int RecNo = Obj1.Insert_Update_ReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Request.QueryString["FID"]));
                             led.P_ReceiptNO = Convert.ToString( RecNo);
                            SqlConnection conn = DataAccess.ConInitForDC();
                            SqlCommand sc = new SqlCommand("insert into RecM(BillNo,Billdate,AmtPaid,Paymenttype,BankName,branchid,transdate,username,BillAmt,DisAmt,BalAmt,PID,PrevBal,TaxPer,TaxAmount,FID,ReceiptNo)" +
                            "values(@BillNo,@RecDate,@AmtPaid,@Paymenttype,@BankName,@branchid,@transdate,@username,@BillAmt,@DisAmt,@BalAmt,@PID,@PrevBal,@TaxPer,@TaxAmount,@FID,@ReceiptNo)", conn);
                            sc.Parameters.Add(new SqlParameter("@BillNo", SqlDbType.Int)).Value = Convert.ToInt32(lblBillNo.Text);
                            if (txtAmtPaid.Text != "")
                                sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = Convert.ToDouble(txtAmtPaid.Text);
                            else
                                sc.Parameters.Add(new SqlParameter("@AmtPaid", SqlDbType.Float)).Value = 0;
                            sc.Parameters.Add(new SqlParameter("@RecDate", SqlDbType.DateTime)).Value = Convert.ToDateTime(txtBillDate.Text);
                            sc.Parameters.Add(new SqlParameter("@Paymenttype", SqlDbType.NVarChar, 50)).Value = RadioButtonList1.SelectedItem.Text;
                            sc.Parameters.Add(new SqlParameter("@BankName", SqlDbType.NVarChar, 50)).Value = txtBankName.Text;
                            sc.Parameters.Add(new SqlParameter("@branchid", SqlDbType.Int)).Value = Convert.ToInt32(Session["Branchid"]);
                            sc.Parameters.Add(new SqlParameter("@transdate", SqlDbType.DateTime)).Value = Convert.ToDateTime(System.DateTime.Now.ToString("dd/MM/yyyy")); //Convert.ToDateTime(System.DateTime.Now);
                            sc.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50)).Value = Session["username"].ToString();
                            sc.Parameters.Add(new SqlParameter("@BalAmt", SqlDbType.Float)).Value = Convert.ToDouble(txtBalance.Text);
                            sc.Parameters.Add(new SqlParameter("@DisAmt", SqlDbType.Float)).Value = Convert.ToDouble(Obj_CSH.Discount);
                            sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = Billamount;// ChangedBillAmt;//Convert.ToSingle(lbltestcharges.Text)                   
                            //    sc.Parameters.Add(new SqlParameter("@BillAmt", SqlDbType.Float)).Value = 0;//Convert.ToSingle(lbltestcharges.Text)                   

                            sc.Parameters.AddWithValue("@PID", Convert.ToInt32(Request.QueryString["PID"]));
                            sc.Parameters.Add(new SqlParameter("@PrevBal", SqlDbType.Float)).Value = Convert.ToDouble(txtBalance.Text);
                            sc.Parameters.Add(new SqlParameter("@TaxPer", SqlDbType.Float)).Value = Convert.ToDouble(Session["Taxper"]);
                            sc.Parameters.Add(new SqlParameter("@TaxAmount", SqlDbType.Float)).Value = Convert.ToDouble(txthstamount.Text);
                            sc.Parameters.Add(new SqlParameter("@FID", SqlDbType.Int)).Value = Convert.ToInt32(Request.QueryString["FID"]);

                            sc.Parameters.Add(new SqlParameter("@ReceiptNo", SqlDbType.Int)).Value = Convert.ToString(led.P_ReceiptNO);
                            sc.Parameters.Add(new SqlParameter("@AccNo", SqlDbType.NVarChar, 50)).Value = "";

                            if (txtNo.Text != "")
                                sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = txtNo.Text;
                            else
                                sc.Parameters.Add(new SqlParameter("@ChqNo", SqlDbType.NVarChar, 50)).Value = "";

                            if (Obj_CSH.ChqDate != Date.getMinDate())
                                sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = Obj_CSH.ChqDate;
                            else
                                sc.Parameters.Add(new SqlParameter("@ChqDate", SqlDbType.DateTime)).Value = DBNull.Value;

                            if (Obj_CSH.CardNo != null)
                                sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = Obj_CSH.CardNo;
                            else
                                sc.Parameters.Add(new SqlParameter("@CardNo", SqlDbType.NVarChar, 50)).Value = "";


                            sc.Parameters.Add(new SqlParameter("@CardName", SqlDbType.NVarChar, 50)).Value = "";


                            sc.Parameters.Add(new SqlParameter("@Cardtype", SqlDbType.NVarChar, 50)).Value = "";

                            sc.Parameters.Add(new SqlParameter("@CardExpiryDate", SqlDbType.DateTime)).Value = DBNull.Value;
                            if (Obj_CSH.CardTransID != null)
                                sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = Obj_CSH.CardTransID;
                            else
                                sc.Parameters.Add(new SqlParameter("@CardTransactionID", SqlDbType.NVarChar, 50)).Value = "";
                            if (Obj_CSH.OnlineType != null)
                                sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = Obj_CSH.OnlineType;
                            else
                                sc.Parameters.Add(new SqlParameter("@OnlineTransType", SqlDbType.NVarChar, 50)).Value = "";
                            if (Obj_CSH.OnlinetransID != null)
                                sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = Obj_CSH.OnlinetransID;
                            else
                                sc.Parameters.Add(new SqlParameter("@OnlineTransID", SqlDbType.NVarChar, 50)).Value = ""; 
                            conn.Open();
                            sc.ExecuteNonQuery();

                            Bindgrid(Convert.ToInt32(lblBillNo.Text));
                            Label12.Visible = true;
                            Label12.Text = "Record updated successfully";
                            ViewState["btnsave"] = "true";
                            btnsave.Enabled = false;
                            lblAdvance.Visible = true;
                            lblAdvanceAmt.Visible = true;
                            if (lblAdvanceAmt.Text != "")
                            {
                                lblAdvanceAmt.Text = Convert.ToString(Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(txtAmtPaid.Text));
                            }
                            else
                            {
                                lblAdvanceAmt.Text = txtAmtPaid.Text;
                            }
                            txtAmtPaid.Text = Convert.ToString(0);
                            //ClearControls();
                        }
                    }
                }
            }

            CalculateServicewise_Amt();
        }
        catch (Exception exc)
        {
            if (exc.Message.Equals("Exception aborted."))
            {
                return;
            }
            else
            {
                Response.Cookies["error"].Value = exc.Message;
                Server.Transfer("~/ErrorMessage.aspx");
            }
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        Bindgrid(Convert.ToInt32(lblBillNo.Text));
    }

    protected void txtAmtPaid_TextChanged(object sender, EventArgs e)
    {
        int billnumber = Cshmst_supp_Bal_C.GetBillNo(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));
        Cshmst_Bal_C ch = new Cshmst_Bal_C(billnumber, Convert.ToInt32(Session["Branchid"]), Convert.ToString(Request.QueryString["FID"]));

        if (txtAmtPaid.Text == "")
        {
            txtAmtPaid.Text = "0";
        }
        if (lblAdvanceAmt.Text == "")
        {
            lblAdvanceAmt.Text = "0";
        }
        if (billnumber == 0)
        {
            if (txthstamount.Text == "")
            {
                txthstamount.Text = "0";
            }
            if ((Convert.ToSingle(txtAmtPaid.Text) + Convert.ToSingle(lblAdvanceAmt.Text)) > (Convert.ToSingle(lbltestcharges.Text) + Convert.ToSingle(txthstamount.Text)))
            {
                //txtAmtPaid.Text = "0";
                lblmsg.Visible = true;
                Label12.Visible = false;
                return;
            }
            else
            {
                lblmsg.Visible = false;
            }
        }
        else
        {
            if (Convert.ToString(txtAmtPaid.Text).Contains("-"))
            {
                if ((Convert.ToSingle(txtAmtPaid.Text)) < (Convert.ToSingle(txtNetPayment.Text) - Convert.ToSingle(lblAdvanceAmt.Text)))
                {
                    //txtAmtPaid.Text = "0";
                    lblmsg.Visible = true;
                    Label12.Visible = false;
                    return;
                }
                else
                {
                    lblmsg.Visible = false;
                }
            }
            else
            {
                if ((Convert.ToSingle(txtAmtPaid.Text)) > ((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(lblAdvanceAmt.Text)))
                {
                    //txtAmtPaid.Text = "0";
                    lblmsg.Visible = true;
                    Label12.Visible = false;
                    return;
                }
                else
                {
                    lblmsg.Visible = false;
                }
            }
        }

        if (txtBalance.Text == "")
        {
            txtBalance.Text = Convert.ToString((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(txtAmtPaid.Text));
        }
        else
        {

            if (billnumber == 0)
            {
                txtBalance.Text = Convert.ToString((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(txtAmtPaid.Text));
            }
            else
            {
                float othercharge = 0;
                if (Convert.ToDouble(0) > ch.Othercharges)
                {
                    othercharge = Convert.ToSingle(0) - ch.Othercharges;
                }
                else
                {
                    othercharge = ch.Othercharges - Convert.ToSingle(0);
                }
                lblamtbalance.Text = Convert.ToString((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - Convert.ToSingle(lblAdvanceAmt.Text));

                txtBalance.Text = Convert.ToString(Convert.ToSingle(lblamtbalance.Text) - Convert.ToSingle(txtAmtPaid.Text) + othercharge);
                if (Convert.ToString(ViewState["Discnt"]) == "")
                {
                    ViewState["Discnt"] = "0";
                }
                if (lblAdvanceAmt.Text == "")
                {
                    lblAdvanceAmt.Text = "0";
                }
                othercharge = 0;
                txtBalance.Text = Convert.ToString((Convert.ToSingle(txtNetPayment.Text) + Math.Round(Convert.ToSingle(txthstamount.Text), 0)) - (Convert.ToSingle(txtAmtPaid.Text) + othercharge + Convert.ToSingle(ViewState["Discnt"]) + Convert.ToSingle(lblAdvanceAmt.Text)));
            }
        }
        btnsave.Enabled = true;

    }

    protected void txtCharges_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToString(ViewState["TotalBillAmt"]) != "" && Convert.ToString(ViewState["TotalBillAmt"]) != null)
        {
            lbltestcharges.Text = Convert.ToString(Convert.ToSingle((ViewState["TotalBillAmt"])) + Convert.ToSingle(0));
        }

        txtDiscnt_TextChanged(null, null);


    }

    protected void btnlab_Click(object sender, EventArgs e)
    {
       
        string sql = "";
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = new SqlCommand();
        cmd1 = con.CreateCommand();
        cmd1.CommandText = "ALTER VIEW [dbo].[VW_patregform] AS (SELECT     patmst.Patname, patmst.PID, patmst.PatRegID, SubDepartment.subdeptName, MainTest.Maintestname,   "+
         "   patmst.Age, patmst.sex, patmst.MDY, MainTest.MTCode, patmst.Branchid,   "+
         "   patmst.Patregdate, patmst.intial, patmst.Drname, patmstd.TestRate,   "+
         "   patmst.CenterName, patmst.RefDr, patmst.Pataddress, patmst.PatientcHistory, patmst.Remark,   "+
         "   patmst.TelNo, patmst.Username, patmst.PPID, MainTest.Singleformat, SubDepartment.SDOrderNo,   "+
         "   MainTest.Testordno  "+
         "   FROM         patmst INNER JOIN  "+
         "   patmstd ON patmst.PID = patmstd.PID INNER JOIN  "+
         "   MainTest ON patmstd.MTCode = MainTest.MTCode INNER JOIN  "+
         "   SubDepartment ON patmstd.SDCode = SubDepartment.SDCode   WHERE     dbo.patmst.PID =" + ViewState["PID"].ToString() + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
       

        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/RegistrationReceipt.rpt");
        Session["reportname"] = "RegistrationReceipt";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
    }



    protected void Button2_Click1(object sender, EventArgs e)
    {
        if (lblBillNo.Text == "")
        { return; }

      
        string sql = "";
        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = conn.CreateCommand();

        sc.CommandText = "ALTER VIEW [dbo].[VW_cshbill] AS (SELECT    distinct "+
                          "  RecM.BillNo, patmst.Patregdate AS RecDate, 'Cash Bill' AS BillType, dbo.FUN_GetReceiveAmt(1, RecM.PID)  AS AmtReceived, dbo.FUN_GetReceiveAmt_Discount(1, RecM.PID) AS Discount, "+
                          "  patmst.TestCharges AS NetPayment, dbo.FUN_GetReceiveAmt(1, RecM.PID) AS AmtPaid,  dbo.FUN_GetReceiveAmt_Balance(1,RecM.PID ) AS Balance,dbo.FUN_GetReceiveAmtUser(1, RecM.PID)  as username,  " +
                          "  dbo.FUN_GetReceiveOtherAmt(1, RecM.PID)  AS OtherCharges, patmst.PatRegID, patmst.intial, patmst.Patname, patmst.sex, patmst.Age,   patmst.Drname, patmst.TelNo, DrMT.DoctorCode, "+
                          "  ISNULL(RecM.CardNo, N'') AS DoctorName, MainTest.Maintestname, MainTest.MTCode, patmstd.TestRate, PackMst.PackageName, patmstd.PackageCode, 0 AS DisFlag,   "+
                          "  patmst.Patusername, patmst.Patpassword, isnull(RecM.Comment,'') as Comment, patmst.MDY, patmst.Remark AS PatientRemark, patmst.Pataddress, patmst.PPID, 'CASH' AS UnitCode, " +
                          "  RecM.TaxAmount, RecM.TaxPer,   0 AS PrintCount, patmst.OtherRefDoctor, RecM.BillNo as ReceiptNo, dbo.FUN_GetReceiveOtherAmtRemark(1, RecM.PID) as OtherChargeRemark, RecM.PID , DrMT.address1 as CenterAddress,cast(patmst.QRImage  AS VARBINARY(8000)) as QRImage, cast(patmst.QRImageD AS VARBINARY(8000)) as QRImageD ,cast(Patmst.PatientcHistory as nvarchar(2500))as   PatientcHistory " +
 
                          "  FROM            patmst INNER JOIN   DrMT ON patmst.CenterCode = DrMT.DoctorCode AND patmst.Branchid = DrMT.Branchid INNER JOIN   MainTest INNER JOIN   "+
                          "  patmstd ON MainTest.MTCode = patmstd.MTCode AND MainTest.Branchid = patmstd.Branchid ON patmst.PID = patmstd.PID AND patmst.Branchid = patmstd.Branchid INNER JOIN   "+
                          "  RecM ON patmst.PID = RecM.PID LEFT OUTER JOIN   PackMst ON patmstd.Branchid = PackMst.branchid AND patmstd.PackageCode = PackMst.PackageCode  where DrMT.DrType='CC' and patmst.branchid=" + Session["Branchid"].ToString() + " and RecM.BillNo=" + lblBillNo.Text + "  and patmst.FID='" + Convert.ToString(Request.QueryString["FID"]) + "' )";//  "+

        conn.Open();
        sc.ExecuteNonQuery();
        conn.Close(); conn.Dispose();
       

        Session.Add("rptsql", sql);
        Session["rptname"] = Server.MapPath("~/DiagnosticReport/Rpt_PayReceipt.rpt");
        Session["reportname"] = "CashReceipt";
        Session["RPTFORMAT"] = "pdf";

        ReportParameterClass.SelectionFormula = sql;
        string close = "<script language='javascript'>javascript:OpenReport();</script>";
        Type title1 = this.GetType();
        Page.ClientScript.RegisterStartupScript(title1, "", close);
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex == -1)
            return;


        if (e.Row.Cells[3].Text != "" && e.Row.Cells[3].Text != "&nbsp;")
        {
            string date = e.Row.Cells[3].Text;
            date = Convert.ToDateTime(date).ToString("dd/MM/yyyy");
            e.Row.Cells[3].Text = date;
        }
        if (e.Row.Cells[4].Text != "" && e.Row.Cells[4].Text != "&nbsp;")
        {
            string date = e.Row.Cells[4].Text;
            date = Convert.ToDateTime(date).ToString("hh:mm tt");
            e.Row.Cells[4].Text = date;
        }
    }

    protected void btnSms_Click(object sender, EventArgs e)
    {
        Cshmst_Bal_C cashmain = new Cshmst_Bal_C();
        cashmain.getBalanceSMS(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]));

        if (cashmain.Balance > 1)
        {
            Patmst_Bal_C ObjPBC = new Patmst_Bal_C(Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]));
            string pname = ObjPBC.Initial.Trim() + " " + ObjPBC.Patname;
            string mobile = ObjPBC.Phone;
            string email = ObjPBC.Email;
            //DrMT_Bal_C drnm = new DrMT_Bal_C(ObjPBC.CenterCode, "CC", Convert.ToInt32(Session["Branchid"]));
            string msg = "";
            Patmstd_Bal_C PBCL =new Patmstd_Bal_C ();
            string CounCode = PBCL.GetSMSString_CountryCode("Registration", Convert.ToInt32(Session["Branchid"]));

            if (mobile != CounCode && mobile != "" && mobile != null)
            {


                createuserlogic_Bal_C aut = new createuserlogic_Bal_C();
                aut.getemail(Session["username"].ToString(), Convert.ToInt32(Session["Branchid"]));
                string Labname = aut.P_LabSmsName;
                string SMSapistring = aut.P_LabSmsString;
                string Labwebsite = aut.P_LabWebsite;

                // msg = "Dear  : " + pname + " ;Patient ID:" + lblregno.Text + " your balance due is Rs:- " + cashmain.Balance.ToString() + "  ";

                msg = "Dear Sir/Madam Balance Payment of " + pname + " (ID " + lblRegNo + ") is " + cashmain.Balance.ToString() + ". Kindly pay at the earliest.";

                SMSapistring = SMSapistring.ToString().Replace("#message#", msg);
                SMSapistring = SMSapistring.Replace("#Labname#", Labname);
                SMSapistring = SMSapistring.Replace("#phone#", mobile);
                try
                {
                    string url = apicall(SMSapistring);
                }
                catch (Exception exx)
                { }

            }
            else
            {
                Label12.Text = "Mobile no not found";
                Label12.Visible = true;
            }

        }
        else
        {

            Label12.Text = " Balance  Not Due....";
            Label12.Visible = true;


        }
    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);

        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();

            StreamReader sr = new StreamReader(httpres.GetResponseStream());

            string results = sr.ReadToEnd();

            sr.Close();
            return results;
        }
        catch
        {
            return "0";
        }
    }
    public void Fill_Labels()
    {
       
        Patmst_Bal_C CIT = null;
        try
        {

            CIT = new Patmst_Bal_C(lblRegNo.Text, Convert.ToString(ViewState["FID"]), Convert.ToInt32(Session["Branchid"]));
            lblRegNo.Text = Convert.ToString(CIT.PatRegID);
            ViewState["PID"] = CIT.PID;
            lblName.Text = CIT.Initial.Trim() + "." + CIT.Patname;
            lblage.Text = Convert.ToString(CIT.Age) + "/" + CIT.MYD;
            lblSex.Text = CIT.Sex;
            LblMobileno.Text = CIT.Phone;
            Lblcenter.Text = CIT.CenterName;
            lblDate.Text = Convert.ToString(CIT.Patregdate);
            LblRefDoc.Text = CIT.Drname;
            Label1.Text = Convert.ToString(CIT.Patregdate);

        }
        catch
        {
            lblRegNo.Visible = true;
            lblRegNo.Text = "Record not found";
        }
       
    }

    protected void isbth_CheckedChanged(object sender, EventArgs e)
    {
        if (isbth.Checked == true)
        {
            ObjPBC.UpdateCashtoHospital(Convert.ToString(ViewState["FID"]), lblRegNo.Text, Convert.ToInt32(Request.QueryString["PID"]), Convert.ToInt32(Session["Branchid"]));
        }

    }
    public void CalculateServicewise_Amt()
    {
        dt = new DataTable();
        dt=PNB.Getall_CalculateserviceAmt(Convert.ToInt32(Request.QueryString["PID"]));
        if (dt.Rows.Count > 0)
        {
            double PaidAmt = 0, PaidPer = 0, TeatAmt = 0, DisAmt = 0, DisPer = 0, DiaPaidAmt = 0;
            for (int j = 0; j < dt.Rows.Count; j++)
            {

                string Code = dt.Rows[j]["MTCode"].ToString();
                TeatAmt = Convert.ToSingle(dt.Rows[j]["TestRate"].ToString());
                if (Convert.ToSingle(txtDiscnt.Text) > 0)
                {
                    //DisAmt = Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(txtAmtPaid.Text) + Convert.ToSingle(txtBalance.Text);
                    DisAmt = Convert.ToSingle(lbltestcharges.Text) - (Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(txtAmtPaid.Text) + Convert.ToSingle(txtBalance.Text));
                    DisPer = 100 * Convert.ToSingle(DisAmt) / Convert.ToSingle(lbltestcharges.Text);
                    DiaPaidAmt = TeatAmt * Math.Round(DisPer, 4) / 100;
                }

                if (lblAdvanceAmt.Text == "")
                {
                    lblAdvanceAmt.Text = "0";
                }
                PaidPer = 100 * (Convert.ToSingle(txtAmtPaid.Text)+Convert.ToSingle(lblAdvanceAmt.Text)) / (Convert.ToSingle(lbltestcharges.Text) - DiaPaidAmt);
               // PaidAmt = TeatAmt * Math.Round(PaidPer, 4) / 100;
                PaidAmt = (Convert.ToSingle(lbltestcharges.Text) - DiaPaidAmt) * Math.Round(PaidPer, 4) / 100;
                
               // Patmstd_Main_Bal_C.Update_TestWiseAmount(Convert.ToInt32(ViewState["PID"]), Convert.ToInt32(Session["Branchid"]), Code, PaidAmt, DiaPaidAmt);
            }
        }    
    }
    protected void RblDiscgiven_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RblDiscgiven.SelectedValue == "3")
        {
            txtDisLabgiven.Visible = true;
            txtDisdrgiven.Visible = true;
        }
        else
        {
            txtDisLabgiven.Visible = false;
            txtDisdrgiven.Visible = false;
        }
    }
    protected void txtDisLabgiven_TextChanged(object sender, EventArgs e)
    {
        if (txtDisLabgiven.Text != "")
        {

            txtDisdrgiven.Text = Convert.ToString(Convert.ToSingle(txtDiscnt.Text) - Convert.ToSingle(txtDisLabgiven.Text));

        }
    }
    protected void txtDisdrgiven_TextChanged(object sender, EventArgs e)
    {
        if (txtDisdrgiven.Text != "")
        {

            txtDisLabgiven.Text = Convert.ToString(Convert.ToSingle(txtDiscnt.Text) - Convert.ToSingle(txtDisdrgiven.Text));

        }
    }
    protected void txtOtherCharges_TextChanged(object sender, EventArgs e)
    {
        BalanceCalculation();  
    }

    public void BalanceCalculation()
    {
       // txtAmtPaid.Text = "0";
        if (txtOtherCharges.Text != "0")
        {
            if (Convert.ToSingle(txtOtherCharges.Text) > Convert.ToSingle(ViewState["OtherCharges"]))
            {
                lbltestcharges.Text = Convert.ToString(Convert.ToSingle(LblTestCharg.Text) + Convert.ToSingle(txtOtherCharges.Text));
                if (Convert.ToSingle(txtAmtPaid.Text) < 0)
                {
                    txtAmtPaid.Text = Convert.ToString((Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(txtDiscnt.Text)) - Convert.ToSingle(lblAdvanceAmt.Text));
                    txtBalance.Text = "0";
                }
                else
                {
                    txtBalance.Text = Convert.ToString((Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(txtDiscnt.Text)) - (Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(txtAmtPaid.Text)));
                }
            }
            else if (Convert.ToSingle(txtOtherCharges.Text) < Convert.ToSingle(ViewState["OtherCharges"]))
            {
                lbltestcharges.Text = Convert.ToString(Convert.ToSingle(LblTestCharg.Text) + Convert.ToSingle(txtOtherCharges.Text));
                //txtBalance.Text = Convert.ToString(Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(txtAmtPaid.Text));
                if (Convert.ToSingle(txtAmtPaid.Text) < 0)
                {
                    txtAmtPaid.Text = Convert.ToString((Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(txtDiscnt.Text)) - Convert.ToSingle(lblAdvanceAmt.Text));
                    txtBalance.Text = "0";
                }
                else
                {
                    txtBalance.Text = Convert.ToString((Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(txtDiscnt.Text)) - (Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(txtAmtPaid.Text)));
                }
            }
            else
            {
                if (Convert.ToSingle(txtOtherCharges.Text) == Convert.ToSingle(ViewState["OtherCharges"]))
                {
                    lbltestcharges.Text = Convert.ToString(Convert.ToSingle(LblTestCharg.Text) + Convert.ToSingle(txtOtherCharges.Text));
                    //txtBalance.Text = Convert.ToString(Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(txtAmtPaid.Text));
                    if (Convert.ToSingle(txtAmtPaid.Text) < 0)
                    {
                        txtAmtPaid.Text = Convert.ToString((Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(txtDiscnt.Text)) - Convert.ToSingle(lblAdvanceAmt.Text));
                        txtBalance.Text = "0";
                    }
                    else
                    {
                        txtBalance.Text = Convert.ToString((Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(txtDiscnt.Text)) - (Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(txtAmtPaid.Text)));
                    }
                }
            }

        }
        else
        {
            lbltestcharges.Text = Convert.ToString(Convert.ToSingle(LblTestCharg.Text) + Convert.ToSingle(txtOtherCharges.Text));
            if (Convert.ToSingle(txtAmtPaid.Text) < 0)
            {
                txtAmtPaid.Text = Convert.ToString((Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(txtDiscnt.Text)) - Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(0));
                //txtAmtPaid.Text = txtBalance.Text;
            }
            else
            {
                if (lbltestcharges.Text == "0" && txtOtherCharges.Text == "0")
                {
                    txtAmtPaid.Text = Convert.ToString(Convert.ToSingle(lblAdvanceAmt.Text) * -1);
                    txtBalance.Text = "0";
                    txtAmtPaid.Enabled = false;
                }
                else
                {
                    txtBalance.Text = Convert.ToString((Convert.ToSingle(lbltestcharges.Text) - Convert.ToSingle(txtDiscnt.Text)) - (Convert.ToSingle(lblAdvanceAmt.Text) + Convert.ToSingle(txtAmtPaid.Text)));

                }
            }
        }
    }


    public void checkexistpageright(string PageName)
    {

        string MenuSQL = "";
        DataTable MenuDt = new DataTable();
        MenuSQL = String.Format(@"SELECT       * from  CTuser  where (CTuser.USERNAME = '" + Convert.ToString(Session["username"]) + "') AND (CTuser.password = '" + Convert.ToString(Session["password"]) + "')   " +
                               " order by CUId  ");



        string connectionString1 = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        SqlConnection con = new SqlConnection(connectionString1);

        SqlCommand cmd = new SqlCommand(MenuSQL, con);

        SqlDataAdapter Adp = new SqlDataAdapter(cmd);

        Adp.Fill(MenuDt);
        
            if (Convert.ToBoolean(MenuDt.Rows[0]["BillDiskDisc"]) == true)
            {
                txtDiscnt.Enabled = true;
                RblDiscgiven.Enabled = true;
               
            }
            else
            {
                txtDiscnt.Enabled = false;
                RblDiscgiven.Enabled = false;
               
            }
        
        con.Close();
        con.Dispose();

    }

}