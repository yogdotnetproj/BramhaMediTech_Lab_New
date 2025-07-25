
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Data.Odbc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Web.Management;
using System.Data;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Management;

using System.Collections.Specialized;
using System.Text;
public class dbconnection
{
    DataAccess data = new DataAccess();
    public DataTable ReadTable(String SELSTR)
    {
        SqlConnection con = data.ConInitForDC1();//DataAccess.ConInitForDC(); 
        SELSTR = SELSTR.Replace("&amp;", "&");
        SqlDataAdapter DA = new SqlDataAdapter(SELSTR, con);
        DataTable DT = new DataTable();
        DA.Fill(DT);
        con.Close(); con.Dispose();
        return DT;
    }
    public DataSet Fill_CenterCode(string branchid, string id)
    {
        SqlConnection con = data.ConInitForDC1();
        string sql = "select DoctorCode,DoctorName from  DrMT where branchid=" + branchid + " and  (DrType = 'CC') ";

        sql += " order by DoctorName";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        con.Open();
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
        }
        return ds;
    }
    public DataSet FillDept_New(string branchid, string id)
    {
        SqlConnection con = data.ConInitForDC1();
        string sql = "select deptid,DeptName from maindepartment where deptid in(1,2) ";//branchid=" + branchid + " and 

        sql += " order by DeptName";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        con.Open();
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
        }
        return ds;
    }
  
    
    public DataSet FillDept(string branchid,string id)
    {
        SqlConnection con = data.ConInitForDC1();
        string sql = "select deptcode,DeptName from DeptMaster where branchid=" + branchid + "";
       
        sql+=" order by DeptName";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        con.Open();
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
        }
        return ds;
    }
  
  
 
    public DataSet FillUserMasterGrid(string branchid, string uname, string dept, string id)
    {
        SqlConnection con = data.ConInitForDC1();
        string query = "SELECT     BranchMaster.BranchName, Deptmaster.DeptName AS dept, CTuser.* "+
               " FROM         BranchMaster INNER JOIN "+
               " CTuser ON BranchMaster.branchid = CTuser.branchid INNER JOIN "+
               " Deptmaster ON CTuser.branchid = Deptmaster.branchid where CTuser.branchid=" + branchid + "  and CTuser.Usertype!='administrator'";
        if (uname != "")
        {
            query += " and  CTuser.USERNAME like '%" + uname + "%'";
        }
       
        SqlDataAdapter da = new SqlDataAdapter(query, con);
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
            con.Close(); con.Dispose();
        }
        return ds;
    }
  
   
   
    public DataSet FillLab(string branchid)
    {

        SqlConnection con = data.ConInitForDC1();
        string sql = "select DoctorCode,DoctorName from DrMT where branchid=" + branchid + " and DrType='ML'";

        sql += " order by DoctorCode";
        SqlDataAdapter da = new SqlDataAdapter(sql, con);
        DataSet ds = new DataSet();
        con.Open();
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
        }
        return ds;

    }
    public DataSet FillUserroles(string branchid, string id)
    {
        SqlConnection con = data.ConInitForDC1();
        SqlDataAdapter da = new SqlDataAdapter("select ROLLID,ROLENAME,ROLLDESCRIPTION from usr where branchid=" + branchid + "  order by ROLENAME ", con);
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
            con.Close(); con.Dispose();
        }
        return ds;
    }
 
    public string ExecuteSql(string SELSTR)
    {
        SqlConnection con = data.ConInitForDC1();
        SELSTR = SELSTR.Replace("\n", "");
        SqlTransaction tr = null;
        string ErrorString = "";       
        try
        {
            con.Open();
            tr = con.BeginTransaction();
            SqlCommand rs = new SqlCommand(SELSTR,con);
            
            rs.Transaction = tr;
            rs.ExecuteNonQuery();
            rs.Transaction.Commit();
            con.Close();
        }
        catch (Exception ex)
        {
            try
            {
                ErrorString = ErrorString + "ExecuteSql : " + ex.Message.Replace("\n", "");
                tr.Rollback();
            }
            catch (Exception ex1)
            {
                ErrorString = ErrorString + "ExecuteSql1 : " + ex1.Message.Replace("\n", "");
            }
        }

        return ErrorString;
    }
  
   
    public string UserSet(DataTable dt, System.Web.UI.HtmlControls.HtmlForm p)
    {
        try
        {
            if (dt == null)
            {
                return "0";
            }
            else
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow dr = dt.Rows[j];
                    switch (dr[0].ToString())
                    {                      
                        case "0": 
                            return "0";
                            break;
                        case "1": 
                            break;
                        case "2": 
                            for (int i = 0; i < p.Controls.Count; i++)
                                if (p.Controls[i].ID == "ContentPlaceHolder1")
                                {
                                    try
                                    {
                                        //p.Controls[i].FindControl("btnprint").Visible = true;
                                        p.Controls[i].FindControl("btnreport").Visible = true;
                                    }
                                    catch (Exception ex) { ex.Message.ToString(); }
                                    try
                                    {
                                        //p.Controls[i].FindControl("btnprint").Visible = true;
                                        p.Controls[i].FindControl("btnreport1").Visible = true;
                                    }
                                    catch (Exception ex) { ex.Message.ToString(); }
                                   
                                }
                            break;
                        case "3": 
                            for (int i = 0; i < p.Controls.Count; i++)
                                if (p.Controls[i].ID == "ContentPlaceHolder1")
                                    try
                                    {
                                        p.Controls[i].FindControl("btnexport").Visible = true;
                                    }
                                    catch (Exception ex) { ex.Message.ToString(); }
                            break;
                        case "4": 
                            for (int i = 0; i < p.Controls.Count; i++)
                                if (p.Controls[i].ID == "ContentPlaceHolder1")
                                {
                                    try
                                    {
                                        p.Controls[i].FindControl("btnsave").Visible = true;
                                    }
                                    catch (Exception ex) { ex.Message.ToString(); }
                                    try
                                    {
                                        p.Controls[i].FindControl("btnSubmit").Visible = true;
                                    }
                                    catch (Exception ex) { ex.Message.ToString(); }
                                }  
                                    
                            break;
                        case "5": 
                            for (int i = 0; i < p.Controls.Count; i++)
                                if (p.Controls[i].ID == "ContentPlaceHolder1")
                                {
                                    try
                                    {
                                        p.Controls[i].FindControl("btnadd").Visible = true;

                                    }
                                    catch (Exception ex) { ex.Message.ToString(); }
                                    try
                                    {
                                        p.Controls[i].FindControl("btnsave").Visible = true;

                                    }
                                    catch (Exception ex) { ex.Message.ToString(); }
                                }
                            break;
                        case "6": 
                            for (int i = 0; i < p.Controls.Count; i++)
                                if (p.Controls[i].ID == "ContentPlaceHolder1")
                                    try
                                    {
                                        //p.Controls[i].FindControl("btndelete").Visible = true;
                                        (p.Controls[i].FindControl("imgbtn") as ImageButton).Visible = true;                                        
                                    }
                                    catch (Exception ex) { ex.Message.ToString(); }
                            break;
                        case "7": 
                            for (int i = 0; i < p.Controls.Count; i++)
                                if (p.Controls[i].ID == "ContentPlaceHolder1")
                                    try
                                    {
                                        p.Controls[i].FindControl("btnapprove").Visible = true;
                                    }
                                    catch (Exception ex) { ex.Message.ToString(); }
                            break;                     
                    }
                }
            }
        }
        catch (SqlException)
        {
            throw;
        }
        return "1";
    }
    public void Allset(System.Web.UI.HtmlControls.HtmlForm p)
    {
        for (int i = 0; i < p.Controls.Count; i++)
        {
            string ss = p.Controls[i].ID;
            if (p.Controls[i].ID == "ContentPlaceHolder1")
            {
                try
                {
                    //p.Controls[i].FindControl("btnprint").Visible = false;
                    p.Controls[i].FindControl("btnreport").Visible = false;
                }
                catch (Exception ex) { ex.Message.ToString(); }
                try
                {
                    //p.Controls[i].FindControl("btnprint").Visible = false;
                    p.Controls[i].FindControl("btnreport1").Visible = false;//****
                }
                catch (Exception ex) { ex.Message.ToString(); }
                try
                {
                    p.Controls[i].FindControl("btnexport").Visible = false;
                }
                      
                catch (Exception ex) { ex.Message.ToString(); }
                try
                {
                  p.Controls[i].FindControl("btnsave").Visible = false;
                }
                catch (Exception ex) { ex.Message.ToString(); }
                try
                {
                    p.Controls[i].FindControl("btnSubmit").Visible = false;
                }
                catch (Exception ex) { ex.Message.ToString(); }
                try
                {
                    p.Controls[i].FindControl("btnadd").Visible = false;
                }
                catch (Exception ex) { ex.Message.ToString(); }
                try
                {
                    p.Controls[i].FindControl("btndelete").Visible = false;
                    //(p.Controls[i].FindControl("imgbtn") as ImageButton).Visible = true;
                }
                catch (Exception ex) { ex.Message.ToString(); }
                try
                {
                    p.Controls[i].FindControl("btnapprove").Visible = false;
                }
                catch (Exception ex) { ex.Message.ToString(); }
                break;
            }
        }
        
    }
   
    public string ExportingAndPrintingCrystalRpt(string Formatas, string rptname,string path, string formula,object rep,string FName)
    {
        //Give name of the report which you have created
        //        string exportFileName = "CrystalReport1.rpt";
        string exportPath = "";
        // for giving particular name to the exported file        
        try
        {
            if (Formatas == "pdf")
            {
               // string filename = "\\Exported";
                string filename = FName;
               // exportPath = path + filename + ".pdf";//"~/Reports\\" + filename + ".pdf";
                exportPath =  filename + ".pdf";//"~/Reports\\" + filename + ".pdf";
            }
            else
            {
                string filenm = "\\Exportedinxls";
                exportPath = path + filenm + ".xls"; //"~/Reports\\" + filename + ".xls";
            }
        }
        catch (Exception ex)
        { return "1 = " + ex.Message; }

        //where file will be exported
        ReportDocument crReportDocument = new ReportDocument();
        try
        {
            crReportDocument = (ReportDocument)rep;
        }
        catch (Exception ex)
        { return "2 = " + ex.Message + " " + rptname; }

        //Export the report to disk.
        ExportOptions crExportOptions;
        DiskFileDestinationOptions crDestOptions = new DiskFileDestinationOptions();
        crDestOptions.DiskFileName = exportPath;
        crExportOptions = crReportDocument.ExportOptions;
        crExportOptions.DestinationOptions = crDestOptions;
        //Specify export destination type
        crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        //Specify the format in which you want to export (like .doc,.pdf etc)
        try
        {
            crReportDocument.RecordSelectionFormula = formula;
        }
        catch (Exception ex)
        { return "3 = " + ex.Message; }

        try
        {
            if (Formatas == "pdf")
                crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            else
                crExportOptions.ExportFormatType = ExportFormatType.ExcelWorkbook;

            crReportDocument.Export();
        }
        catch (Exception ex)
        { return "4 = " + ex.Message; }
        finally
        {
            ((IDisposable)crReportDocument).Dispose();
            GC.Collect();
        }       
        return "";
    }


    public string ExportingAndPrintingCrystalRpt_Parameter(string Formatas, string rptname, string path, string formula, object rep, string Printdate,string PrintUserName,string FName) //string rptuser,string rptusername, string rptdate, string rptmodule)
    {
        //Give name of the report which you have created
        //        string exportFileName = "CrystalReport1.rpt";
        string exportPath = "";
        // for giving particular name to the exported file        
        try
        {
            if (Formatas == "pdf")
            {
                //string filename = "\\Exported";
                //exportPath = path + filename + ".pdf";//"~/Reports\\" + filename + ".pdf";

                string filename = FName;               
                exportPath = filename + ".pdf";//"~/Reports\\" + filename + ".pdf";
            }
            else
            {
                string filenm = "\\Exportedinxls";
                exportPath = path + filenm + ".xls"; //"~/Reports\\" + filename + ".xls";
            }
        }
        catch (Exception ex)
        { return "1 = " + ex.Message; }

        //where file will be exported
        ReportDocument crReportDocument = new ReportDocument();

        try
        {
            crReportDocument = (ReportDocument)rep;

        }
        catch (Exception ex)
        { return "2 = " + ex.Message + " " + rptname; }

        //Export the report to disk.
        ExportOptions crExportOptions;
        DiskFileDestinationOptions crDestOptions = new DiskFileDestinationOptions();
        crDestOptions.DiskFileName = exportPath;
        crExportOptions = crReportDocument.ExportOptions;
        crExportOptions.DestinationOptions = crDestOptions;
        //Specify export destination type
        crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
        //Specify the format in which you want to export (like .doc,.pdf etc)
        try
        {
            // crReportDocument.SetParameterValue(0, rptuser);
            // crReportDocument.SetParameterValue(1, rptmodule);
            crReportDocument.SetParameterValue(0, Printdate);
            crReportDocument.SetParameterValue(1, PrintUserName);

            crReportDocument.RecordSelectionFormula = formula;
        }
        catch (Exception ex)
        { return "3 = " + ex.Message; }

        try
        {
            if (Formatas == "pdf")
                crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            else
                crExportOptions.ExportFormatType = ExportFormatType.ExcelWorkbook;

            crReportDocument.Export();
        }
        catch (Exception ex)
        { return "4 = " + ex.Message; }
        finally
        {
            ((IDisposable)crReportDocument).Dispose();
            GC.Collect();
        }
        return "";
    }


    #region properties
    private string email;
    public string P_email
    {
        get { return email; }
        set { email = value; }
    }
    private string phoneno;
    public string P_phoneno
    {
        get { return phoneno; }
        set { phoneno = value; }
    }
    private string mobileno;
    public string P_mobileno
    {
        get { return mobileno; }
        set { mobileno = value; }
    }
    #endregion
}
