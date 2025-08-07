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
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Web.Management;
using System.Net;
using System.IO;
//using ClosedXML.Excel;

public partial class MOHVoucherReport :BasePage
{
    TreeviewBind_C ObjTB = new TreeviewBind_C();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["InsuranceId"] = "0";
            FillddlCenter();
            todate.Text = Date.getdate().ToString("dd/MM/yyyy");
            fromdate.Text = Date.getdate().ToString("dd/MM/yyyy");
        }
    }
    [ScriptMethod()]
    [WebMethod]
    public static List<string> SearchInsurance(string prefixText, int count)
    {
        TreeviewBind_C objDALOpdReg = new TreeviewBind_C();
        return objDALOpdReg.FillAllInsurance(prefixText);
    }
    protected void txtInsuranceid_TextChanged(object sender, EventArgs e)
    {
        if (txtInsuranceid.Text.Trim() != "")
        {
            string[] InsuranceId = txtInsuranceid.Text.Split('-');
            txtInsuranceid.Text = InsuranceId[1];

            if (txtInsuranceid.Text != "")
            {
                ViewState["InsuranceId"] = InsuranceId[0];
            }
            else
            {
                ViewState["InsuranceId"] = 0;
            }
            // GetInsurancePaymentDetails();
            // LoadReceipts();

        }
        else
        {
            ViewState["InsuranceId"] = 0;
        }


    }
    public void AlterView()
    {
        if (ViewState["InsuranceId"] != "0")
        {

            string query = "ALTER VIEW [dbo].[Summ_result] AS select BranchName,PatRegID, CONVERT(varchar(10), patregdate,105)  AS Date,  Patname,LetterNo, " +
                          "  Age,sex,DOB,Patphoneno, Pataddress, ClinicalHistory, PatientInsuType,RaceName, [Total WBC Count],[Absolute Eosinophil Count],[MCHC],[Haemoglobin (Hb)],[Absolute Neutrophil Count],[Neutrophils],[Lymphocytes],[Monocytes],[Eosinophils],[Basophils],[PCV/HCT],[Platelet Count],[MCV],[RDW-CV],[MCH],[MPV],[RBC Count], " +
                          "  [Total Bilirubin],[SGPT (ALT)],[Alkaline Phosphatase],[Total Protein],[Globulin],[A/G Ratio],[SGOT (AST)],[Direct Bilirubin],[Albumin],[GGT], " +
                          "  [Creatinine],[BUN/CRE Ratio],[eGFR],[Chloride],[Potassium],[Sodium],[Blood Urea], " +
                          "  [Total Thyroxine (Total T4)],[Free Tri-iodothyronine (FreeT3)],[Total Tri-iodothyronine (Total T3)],[Free  Thyroxine (Free T4)],[TSH], " +
                          "  [Fasting Total Cholesterol],[HDL Cholesterol],[VLDL Cholesterol],[LDL Cholesterol],[Coronary Risk],[Fasting Triglycerides],[Coronary  Risk],[Non-Fasting Total Cholesterol],[LDL  Cholesterol],[VLDL  Cholesterol],[Non-Fasting Triglycerides],[HDL  Cholesterol],[PSA],[Result], " +
                          "  [Yeast Cells],[Bacteria],[Bile Salts],[RBC],[Bilirubin],[Epithelial Cells],[Colour],[Urobilinogen],[Appearence],[Specific gravity],[Trichomonas vaginalis],[Other],[PUS Cells],[Ketone],[Casts],[Leukocytes],[Glucose],[Nitrite],[Crystals],[PH],[Protein],[WBC],[Blood],[Glyco Haemoglobin (HbA1c)] " +
                         "   from " +
                         "   ( " +
                         "  SELECT        ResMst.testname, ResMst.ResultTemplate, patmst.PatRegID, patmst.Patregdate, patmst.Remark, patmst.intial, patmst.Patname, patmst.sex, patmst.Age, patmst.MDY, patmst.CenterName, BranchMaster.BranchName, " +
                          "   HMS_NEW.dbo.LabRegistration.LetterNo, HMS_NEW.dbo.LabRegistration.ClinicalHistory, patmst.DOB, patmst.Pataddress, " +
                          "   patmst.Patphoneno, HMS_NEW.dbo.PatientInsuType.PatientInsuType,HMS_NEW.dbo.Tbl_Race.RaceName " +
                          "   FROM ResMst INNER JOIN patmst ON ResMst.PID = patmst.PID    "+ " AND  CONVERT(date, patmst.Patregdate)  between('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')" +
                          " INNER JOIN BranchMaster ON patmst.Branchid = BranchMaster.branchid INNER JOIN " +
                          "  HMS_NEW.dbo.LabRegistration ON patmst.PPID = HMS_NEW.dbo.LabRegistration.PatRegId INNER JOIN "+
                          "  HMS_NEW.dbo.PatientInsuType ON HMS_NEW.dbo.LabRegistration.PatientSubCategoryId = HMS_NEW.dbo.PatientInsuType.PatientInsuTypeId INNER JOIN "+
                          "  HMS_NEW.dbo.PatientInformation ON HMS_NEW.dbo.LabRegistration.PatRegId = HMS_NEW.dbo.PatientInformation.PatRegId LEFT OUTER JOIN "+
                          "  HMS_NEW.dbo.Tbl_Race ON HMS_NEW.dbo.PatientInformation.RaceId = HMS_NEW.dbo.Tbl_Race.RaceID ";
                                        if (todate.Text != "" && fromdate.Text != "")
            {
                query += "and patmst.CenterName='MOH' and (CAST(CAST(YEAR( patmst.Patregdate) AS varchar(4)) + '/' + CAST(MONTH( patmst.Patregdate) AS varchar(2)) + '/' + CAST(DAY( patmst.Patregdate) AS varchar(2)) AS datetime)) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
            }
            if (Convert.ToString(ViewState["InsuranceId"]) != "0")
            {
                query = query + " and HMS_NEW.dbo.LabRegistration.PatientSubCategoryId  = '" + Convert.ToString(ViewState["InsuranceId"]).Trim() + "' ";
            }
            query = query + "  ) d " +
                         "   pivot " +
                         "   (  max(ResultTemplate)  for testname in (  " +
                         "  [Total WBC Count],[Absolute Eosinophil Count],[MCHC],[Haemoglobin (Hb)],[Absolute Neutrophil Count],[Neutrophils],[Lymphocytes],[Monocytes],[Eosinophils],[Basophils],[PCV/HCT],[Platelet Count],[MCV],[RDW-CV],[MCH],[MPV],[RBC Count], " +
                         "   [Total Bilirubin],[SGPT (ALT)],[Alkaline Phosphatase],[Total Protein],[Globulin],[A/G Ratio],[SGOT (AST)],[Direct Bilirubin],[Albumin],[GGT], " +
                         "  [Creatinine],[BUN/CRE Ratio],[eGFR],[Chloride],[Potassium],[Sodium],[Blood Urea], " +
                         "   [Total Thyroxine (Total T4)],[Free Tri-iodothyronine (FreeT3)],[Total Tri-iodothyronine (Total T3)],[Free  Thyroxine (Free T4)],[TSH], " +
                         "   [Fasting Total Cholesterol],[HDL Cholesterol],[VLDL Cholesterol],[LDL Cholesterol],[Coronary Risk],[Fasting Triglycerides],[Coronary  Risk],[Non-Fasting Total Cholesterol],[LDL  Cholesterol],[VLDL  Cholesterol],[Non-Fasting Triglycerides],[HDL  Cholesterol],[PSA],[Result], " +
                         "   [Yeast Cells],[Bacteria],[Bile Salts],[RBC],[Bilirubin],[Epithelial Cells],[Colour],[Urobilinogen],[Appearence],[Specific gravity],[Trichomonas vaginalis],[Other],[PUS Cells],[Ketone],[Casts],[Leukocytes],[Glucose],[Nitrite],[Crystals],[PH],[Protein],[WBC],[Blood],[Glyco Haemoglobin (HbA1c)])) as piv";
            //if (todate.Text != "" && fromdate.Text != "")
            //{
            //    query += " and (CAST(CAST(YEAR( RecM.TransDate) AS varchar(4)) + '/' + CAST(MONTH( RecM.TransDate) AS varchar(2)) + '/' + CAST(DAY( RecM.TransDate) AS varchar(2)) AS datetime)) between ('" + Convert.ToDateTime(fromdate.Text).ToString("MM/dd/yyyy") + "') and ('" + Convert.ToDateTime(todate.Text).ToString("MM/dd/yyyy") + "')";
            //}



            // string labcode = Convert.ToString(System.Web.HttpContext.Current.Session["UnitCode"]);



            SqlConnection con = DataAccess.ConInitForDC();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandText = query;

            con.Open();
            cmd1.ExecuteNonQuery();
            con.Close(); con.Dispose();
        }
    }
    public void GetSummaryReportData(string PageName)
    {

        string MenuSQL = "";
        DataTable MenuDt = new DataTable();
        //MenuSQL = String.Format(@"select  * from Summ_result ");



        //string connectionString1 = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
        //SqlConnection con = new SqlConnection(connectionString1);

        //SqlCommand cmd = new SqlCommand(MenuSQL, con);

        //SqlDataAdapter Adp = new SqlDataAdapter(cmd);

        MenuDt = ObjTB.GetSummaryExcel_Report();
       // Adp.Fill(MenuDt);
        if (MenuDt.Rows.Count >0)
        {
            //Response.Redirect("Login.aspx", false);
            ExportToExcel(MenuDt, "Summary");
        }
       // con.Close();
        //con.Dispose();

    }
    private void FillddlCenter()
    {
        try
        {
           
            //ddlCenter.SelectedIndex = 1;
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
    protected void ExportToExcel(DataTable dt, string filename)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";

        using (System.IO.StringWriter sw = new System.IO.StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                // Create a GridView to render the DataTable
                GridView gv = new GridView();
                gv.DataSource = dt;
                gv.DataBind();

                // Render the GridView to HTML
                gv.RenderControl(hw);

                // Write the HTML output to the response
                HttpContext.Current.Response.Output.Write(sw.ToString());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }
    }
    protected void btnlist_Click(object sender, EventArgs e)
    {
        if (Convert.ToString( ViewState["InsuranceId"]) != "0")
        {
            AlterView();
            GetSummaryReportData("");
        }
        else
        {
            lblMsg.Text = "Pls Select Insurance company";
        }
        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //string sql = "SELECT CustomerId, Name, Country FROM Customers";
        //using (SqlConnection con = new SqlConnection(constr))
        //{
        //    using (SqlDataAdapter sda = new SqlDataAdapter(sql, con))
        //    {
        //        using (DataTable dt = new DataTable())
        //        {
        //            sda.Fill(dt);
        //            using (XLWorkbook wb = new XLWorkbook())
        //            {
        //                wb.Worksheets.Add(dt, "Customers");

        //                Response.Clear();
        //                Response.Buffer = true;
        //                Response.Charset = "";
        //                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //                Response.AddHeader("content-disposition", "attachment;filename=Customers.xlsx");
        //                using (MemoryStream ms = new MemoryStream())
        //                {
        //                    wb.SaveAs(ms);
        //                    ms.WriteTo(Response.OutputStream);
        //                    Response.Flush();
        //                    Response.End();
        //                }
        //            }
        //        }
        //    }
        //}
    }
}