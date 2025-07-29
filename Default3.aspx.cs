using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;
using System.IO;

public partial class Default3 :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();   
    DataTable dt = new DataTable();
    dbconnection dc = new dbconnection();
    Patmstd_Bal_C ObjPBC = new Patmstd_Bal_C(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Uniquemethod_Bal_C.isSessionSet())
        {
            string ss = System.Configuration.ConfigurationManager.AppSettings["LogOutURL"].Trim();
            Response.Redirect(ss);
        }
        if (!Page.IsPostBack)
        {

            try
            {
                LUNAME.Text = Convert.ToString(Session["username"]);
                LblDCName.Text = Convert.ToString(Session["Bannername"]);
                LblDCCode.Text = Convert.ToString(Session["BannerCode"]);
                todate.Text = Date.getdate().ToString("dd/MM/yyyy");
                fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
                BindGrid();
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
    }

    void BindGrid()
    {
        try
        {
            object fromDate = null;
            object Todate = null;

            if (fromdate.Text != "")
            {

                fromDate = DateTimeConvesion.getDateFromString(fromdate.Text.Trim()).ToString();

            }
            if (todate.Text != "")
            {
                Todate = DateTimeConvesion.getDateFromString(todate.Text.Trim()).ToString();
            }




            if (Request.QueryString["Type"] == "Default3")
            {

                RateGrid.DataSource = TreeviewBind_C.DefaultTest3(fromDate, Todate);
                RateGrid.DataBind();

                for (int i = 0; i < RateGrid.Rows.Count; i++)
                {
                    if (i > 0)
                    {
                        RateGrid.Rows[0].Cells[0].BackColor = System.Drawing.Color.Green;
                        RateGrid.Rows[0].Cells[1].BackColor = System.Drawing.Color.Green;
                        RateGrid.Rows[0].Cells[2].BackColor = System.Drawing.Color.Green;
                        RateGrid.Rows[0].Cells[0].ForeColor = System.Drawing.Color.White;
                        RateGrid.Rows[0].Cells[1].ForeColor = System.Drawing.Color.White;
                        RateGrid.Rows[0].Cells[2].ForeColor = System.Drawing.Color.White;
                        RateGrid.Rows[0].Cells[6].BackColor = System.Drawing.Color.Green;
                        CheckBox chkAuto = (RateGrid.Rows[i].FindControl("CheckBox1") as CheckBox);
                        if (RateGrid.DataKeys[i].Value.ToString().Trim() == RateGrid.DataKeys[i - 1].Value.ToString().Trim())
                        {
                            RateGrid.Rows[i].Cells[0].Text = "";
                            RateGrid.Rows[i].Cells[1].Text = "";
                            RateGrid.Rows[i].Cells[2].Text = "";
                            RateGrid.Rows[i].Cells[0].BackColor = System.Drawing.Color.Red;
                            RateGrid.Rows[i].Cells[1].BackColor = System.Drawing.Color.Red;
                            RateGrid.Rows[i].Cells[2].BackColor = System.Drawing.Color.Red;
                            RateGrid.Rows[i].Cells[6].BackColor = System.Drawing.Color.Red;

                        }
                        else
                        {
                          //  chkAuto.Enabled = false;
                            RateGrid.Rows[i].Cells[0].BackColor = System.Drawing.Color.Green;
                            RateGrid.Rows[i].Cells[1].BackColor = System.Drawing.Color.Green;
                            RateGrid.Rows[i].Cells[2].BackColor = System.Drawing.Color.Green;
                            RateGrid.Rows[i].Cells[6].BackColor = System.Drawing.Color.Green;
                            RateGrid.Rows[i].Cells[0].ForeColor = System.Drawing.Color.White;
                            RateGrid.Rows[i].Cells[1].ForeColor = System.Drawing.Color.White;
                            RateGrid.Rows[i].Cells[2].ForeColor = System.Drawing.Color.White;
                        }
                    }
                }
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void RateGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void RateGrid_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    protected void RateGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void RateGrid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            
                for (int i = 0; i < RateGrid.Rows.Count; i++)
                {
                    string RegNo = RateGrid.Rows[i].Cells[0].Text;
                    string TestCode = RateGrid.Rows[i].Cells[2].Text; 
                    string TestName = RateGrid.Rows[i].Cells[3].Text;
                       
                    string username = Convert.ToString(Session["username"]);
                    int amt = Convert.ToInt32((RateGrid.Rows[i].Cells[5].FindControl("txtamount") as TextBox).Text);
                    CheckBox Chk_Action = RateGrid.Rows[i].Cells[5].FindControl("CheckBox1") as CheckBox;
                    string RNo = (RateGrid.Rows[i].FindControl("hdnRegNo") as HiddenField).Value;
                    string PID = (RateGrid.Rows[i].FindControl("hdnPID") as HiddenField).Value;
                    string MTCode = (RateGrid.Rows[i].FindControl("hdnMTCode") as HiddenField).Value;
                    if (Chk_Action.Checked == true)
                    {
                        ObjPBC.PatRegID = RNo;
                        ObjPBC.MTCode = MTCode;
                        ObjPBC.PID = Convert.ToInt32( PID);
                        ObjPBC.AlterView_Defaulte(Convert.ToInt32(PID));
                        ObjPBC.Delete_Default( Convert.ToInt32( PID), 1, MTCode);
                        ObjPBC.Insert_Update_ForPmstDefault(1);
                    }
                    
                        
                try
                {
                    
                    Label4.Text = "Record updated successfully";

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
    protected void chkAction_CheckedChanged(object sender, EventArgs e)
    {
        LblAmt.Text = "0";
        for (int i = 0; i < RateGrid.Rows.Count; i++)
        {
            CheckBox chkAuto = (RateGrid.Rows[i].FindControl("CheckBox1") as CheckBox);
            TextBox Txt_amt = (RateGrid.Rows[i].FindControl("txtamount") as TextBox);
            if (chkAuto.Checked == true)
            {
                LblAmt.Text = Convert.ToString( Convert.ToSingle( LblAmt.Text) +  Convert.ToSingle( Txt_amt.Text));
            }
            
        }
    }
}