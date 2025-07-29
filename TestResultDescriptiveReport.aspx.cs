 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class TestResultDescriptiveReport :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
       
       // lblVialID.Font.Name = "FRE3OF9X";

        string MTCode = Request.QueryString["MTCode"].Trim();
        string RegNo = Request.QueryString["RegNo"].Trim();
        string FID = Request.QueryString["FID"].Trim();

        SqlConnection conn = DataAccess.ConInitForDC();
        SqlCommand sc = new SqlCommand("Select * from VW_desfiledata where MTCode='" + MTCode + "' and Patregid='" + RegNo + "' and FID='" + FID + "'", conn);
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = sc.ExecuteReader();

            if (sdr != null && sdr.Read())
            {


                lblRegNo.Text = sdr["Patregid"].ToString();
                lblPatName.Text = sdr["intial"].ToString() + " " + sdr["Patname"].ToString();
                lblAgeNSex.Text = sdr["Age"].ToString() + " " + sdr["MDY"].ToString() + "s / " + sdr["sex"].ToString();
                lblPSC.Text = sdr["CenterCode"].ToString();
                lblRefDoctor.Text = sdr["DrName"].ToString();
                lblReportDate.Text = Convert.ToDateTime(sdr["PatRepDate"]).ToString("dd/MM/yyyy");
                lblRegDate.Text = Convert.ToDateTime(sdr["RegistratonDateTime"]).ToString("dd/MM/yyyy");
                lblSampleDrawnDate.Text = Convert.ToDateTime(sdr["Phrecdate"]).ToString("dd/MM/yyyy");
                lblSampleName.Text = sdr["SampleType"].ToString();
                lblPSCName.Text = sdr["DoctorName"].ToString();

                lblDeptName.Text = sdr["subdeptName"].ToString().ToUpper() + " REPORT";
                lblMemoText.Text = sdr["TextDesc"].ToString();
                lblVialID.Text = sdr["BarCodeid"].ToString();
                lblsign.Text = sdr["Signature1"].ToString();
                lblDesignation.Text = sdr["Signature2"].ToString();
                int sid = Convert.ToInt32(sdr["SignatureID"]);
                imgsignid.Src = "getimage.aspx?id=" + sid;

                
            }
            else
            {
                throw new Exception("No Record Featch");
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
            catch
            {
                throw new Exception("Record not found");
            }
        }
    }
}