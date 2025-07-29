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
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;

public partial class ReRunResult :BasePage
{
    string MTCode;
    Patmst_New_Bal_C ObjPNBC = new Patmst_New_Bal_C();
    DataTable dt = new DataTable();
    PatSt_Bal_C psnew = new PatSt_Bal_C();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Fill_Labels();
          
            BINDReRun();
            CreateGridView();
            
        }
    }

    public void BINDReRun()
    {
          DataTable dt = new DataTable();
          dt = ObjPNBC.Bind_RerunResult();
          if (dt.Rows.Count > 0)
          {
              ddlReRun.DataSource = dt;
              ddlReRun.DataTextField = "machineName";
              ddlReRun.DataValueField = "machineName";
              ddlReRun.DataBind();
              ddlReRun.Items.Insert(0, "-Select Machine Name-");
              ddlReRun.SelectedIndex = 0;
          }
        
    }
    private void CreateGridView()
    {
        GridView gv = new GridView();
        gv.ID = "EmployeeGridView";
        gv.AutoGenerateColumns = false;
        gv.AllowPaging = true;
        gv.EnableViewState = true;
        gv.PageSize = 100; // Default page Size
        gv.Width = 900;
        gv.CssClass="table table-responsive table-sm table-bordered";
        //Create EventHanfler for Paging.

        
        DataTable dt1 = new DataTable();
        dt1 = ObjPNBC.GET_BarcodeId(Request.QueryString["PatRegID"], Request.QueryString["MTCode"]);
        if (dt1.Rows.Count > 0)
        {
            psnew.AlterVW_Int_Barcode(Convert.ToInt32(Session["Branchid"]), Convert.ToString(dt1.Rows[0]["BarcodeID"]), "18");

            // dt = ObjPNBC.GetRerunResult(Convert.ToString( dt1.Rows[0]["BarcodeID"]));
            //We convert List view to DataTable because we use the 
            AlterView(Convert.ToString(dt1.Rows[0]["BarcodeID"]), Request.QueryString["MTCode"]);
            ViewState["BARCODEID"] = Convert.ToString(dt1.Rows[0]["BarcodeID"]);
        }
        dt = ObjPNBC.GetRerunResult(Convert.ToString(0));
    
        //DataTable Column Name as a GridView Column Header.
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    BoundField boundField = new BoundField();
                    boundField.DataField = dt.Columns[i].ColumnName.ToString();
                    boundField.HeaderText = dt.Columns[i].ColumnName.ToString();
                    gv.Columns.Add(boundField);
                }
            } 
        }

        PlaceHolder1.Controls.Add(gv);
        BindGridView(gv, dt);
    }
    private void BindGridView(GridView gv, DataTable dt)
    {
        gv.DataSource = dt;
        gv.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ObjPNBC.Update_rerun(Convert.ToString(ViewState["BARCODEID"]), txtRemark.Text, Request.QueryString["PatRegID"], "1", Convert.ToString(ddlReRun.SelectedItem.Text), Request.QueryString["MTCode"]);
        Label4.Text = "Re Run  order successfully";
    }
    public void Fill_Labels()
    {
        #region  Patient info
        Patmst_Bal_C CIT = null;
        try
        {

            CIT = new Patmst_Bal_C(Request.QueryString["PatRegID"], Convert.ToString(18), Convert.ToInt32(Session["Branchid"]));

            lblRegNo.Text = Convert.ToString(CIT.PatRegID);
           
            ViewState["PID"] = CIT.PID;
            lblName.Text = CIT.Initial.Trim() + "." + CIT.Patname;
            lblage.Text = Convert.ToString(CIT.Age) + "/" + CIT.MYD;
            lblSex.Text = CIT.Sex;

            LblMobileno.Text = CIT.Phone;
            Lblcenter.Text = CIT.CenterName;
            lbldate.Text = Convert.ToString(CIT.Patregdate);
            LblRefDoc.Text = CIT.Drname;
            
        }
        catch
        {
            lblRegNo.Visible = true;
            lblRegNo.Text = "Record not found";
        }
        #endregion
    }

    public void AlterView( string BarcodeID,string MTCode)
    {
        SqlConnection con = DataAccess.ConInitForDC();
        SqlCommand cmd1 = con.CreateCommand();

        string query = "ALTER VIEW [dbo].[VW_serialportuserparseddata] AS (SELECT distinct   serialportdataID, userID, machineName, medicalTestName as TestName, medicalTestResult,  cast (medicalTestResultUnit as nvarchar(4000)) as medicalTestResultUnit,cast (VW_Int.TestName as nvarchar(4000)) as medicalTestName " +
                       " FROM           serialportuserparseddata INNER JOIN VW_Int ON serialportuserparseddata.userID = VW_Int.BarcodeID AND "+
                       " serialportuserparseddata.medicalTestName = VW_Int.Mapcode  " +
                     " where   userID='" + BarcodeID + "' and VW_Int.MTCode='" + MTCode + "' ";


       


        cmd1.CommandText = query + ")";

        con.Open();
        cmd1.ExecuteNonQuery();
        con.Close(); con.Dispose();
    }
}