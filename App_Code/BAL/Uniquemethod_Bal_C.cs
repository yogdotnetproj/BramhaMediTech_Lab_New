using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;

public class Uniquemethod_Bal_C
{
	public Uniquemethod_Bal_C()
	{
		
	}
    public string ExportandPrint(string formats, string rptname, string filename,string selectionFormula,object repdoc)
    {        
        string exportedpath = "";
        if (formats == "pdf")
        {
            exportedpath = filename; //+ "\\exported.pdf";
        }
        else if (formats == "xls")
        {
            exportedpath = filename;
        }
        ReportDocument crReportDocument = null;
        if (repdoc != null)
        {
            crReportDocument = (ReportDocument)repdoc;
        }
        //ReportDocument crReportDocument = new ReportDocument();
        
        try
        {
            //crReportDocument.Load(rptname);
            //crReportDocument.SetDataSource(ds);
        }
        catch (Exception exx)
        { exx.Message.ToString(); }

        ExportOptions crExportOption;
        DiskFileDestinationOptions crDestOption = new DiskFileDestinationOptions();
        crDestOption.DiskFileName = exportedpath;
        //crExportOption = reportdoc.ExportOptions;
        crExportOption = crReportDocument.ExportOptions;
        crExportOption.ExportDestinationOptions = crDestOption;
        crExportOption.ExportDestinationType = ExportDestinationType.DiskFile;       
       
        if (selectionFormula != "")
        {
            crReportDocument.RecordSelectionFormula = selectionFormula;
        }
       

        try
        {
            if (formats == "pdf")
            {
                crExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
            }
            else if (formats == "xls")
            {
                crExportOption.ExportFormatType = ExportFormatType.Excel;
            }
        }
        catch (Exception exx)
        { exx.Message.ToString(); }
        
       crReportDocument.Export();
        return "";
    }
    public string ExportingAndPrintingCrystalRpt_Parameter(string Formatas, string rptname, string path, string formula, object rep, string Printdate, string PrintUserName, string FName) //string rptuser,string rptusername, string rptdate, string rptmodule)
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
                crExportOptions.ExportFormatType = ExportFormatType.Excel;

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

    public string ExportandPrintr_Parameter(string formats, string rptname, string filename, string selectionFormula, object repdoc,string Printdate, string PrintUserName)//,object dst)
    {

        string exportedpath = "";
        if (formats == "pdf")
        {
            exportedpath = filename; //+ "\\exported.pdf";
        }
        else if (formats == "xls")
        {
            exportedpath = filename;
        }
        ReportDocument crReportDocument = null;
        if (repdoc != null)
        {
            crReportDocument = (ReportDocument)repdoc;
        }


        try
        {

        }
        catch (Exception exx)
        { exx.Message.ToString(); }

        ExportOptions crExportOption;
        DiskFileDestinationOptions crDestOption = new DiskFileDestinationOptions();
        crDestOption.DiskFileName = exportedpath;
        //crExportOption = reportdoc.ExportOptions;
        crExportOption = crReportDocument.ExportOptions;
        crExportOption.ExportDestinationOptions = crDestOption;
        crExportOption.ExportDestinationType = ExportDestinationType.DiskFile;

        //if (selectionFormula != "")
        //{
            crReportDocument.SetParameterValue(0, Printdate);
            crReportDocument.SetParameterValue(1, PrintUserName);
            crReportDocument.RecordSelectionFormula = selectionFormula;
       // }

        try
        {
            if (formats == "pdf")
            {
                crExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
            }
            else if (formats == "xls")
            {
                crExportOption.ExportFormatType = ExportFormatType.Excel;
            }
        }
        catch (Exception exx)
        { exx.Message.ToString(); }
        //reportdoc.Export();
        crReportDocument.Export();
        return "";
    }

    public string ExportandPrintr(string formats, string rptname, string filename, string selectionFormula, object repdoc)//,object dst)
    {
        
        string exportedpath = "";
        if (formats == "pdf")
        {
            exportedpath = filename; //+ "\\exported.pdf";
        }
        else if (formats == "xls")
        {
            exportedpath = filename;
        }
        ReportDocument crReportDocument = null;
        if (repdoc != null)
        {
            crReportDocument = (ReportDocument)repdoc;
        }
        

        try
        {
            
        }
        catch (Exception exx)
        { exx.Message.ToString(); }

        ExportOptions crExportOption;
        DiskFileDestinationOptions crDestOption = new DiskFileDestinationOptions();
        crDestOption.DiskFileName = exportedpath;
        //crExportOption = reportdoc.ExportOptions;
        crExportOption = crReportDocument.ExportOptions;
        crExportOption.ExportDestinationOptions = crDestOption;
        crExportOption.ExportDestinationType = ExportDestinationType.DiskFile;

        if (selectionFormula != "")
        {
            crReportDocument.RecordSelectionFormula = selectionFormula;
        }
      
        try
        {
            if (formats == "pdf")
            {
                crExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
            }
            else if (formats == "xls")
            {
                crExportOption.ExportFormatType = ExportFormatType.Excel;
            }
        }
        catch (Exception exx)
        { exx.Message.ToString(); }
        //reportdoc.Export();
        crReportDocument.Export();
        return "";
    }
    public string ExportandPrint1(string formats, string rptname, string filename, string selectionFormula, object repdoc)//,object dst)
    {
        
        string exportedpath = "";
        if (formats == "pdf")
        {
            exportedpath = filename; //+ "\\exported.pdf";
        }
        else if (formats == "xls")
        {
            exportedpath = filename;
        }
        ReportDocument crReportDocument = null;
        if (repdoc != null)
        {
            crReportDocument = (ReportDocument)repdoc;
        }
       
        try
        {
            
        }
        catch (Exception exx)
        { exx.Message.ToString(); }

        ExportOptions crExportOption;
        DiskFileDestinationOptions crDestOption = new DiskFileDestinationOptions();
        crDestOption.DiskFileName = exportedpath;
       
        crExportOption = crReportDocument.ExportOptions;
        crExportOption.ExportDestinationOptions = crDestOption;
        crExportOption.ExportDestinationType = ExportDestinationType.DiskFile;

        if (selectionFormula != "")
        {
            crReportDocument.RecordSelectionFormula = selectionFormula;
        }
       

        try
        {
            if (formats == "pdf")
            {
                crExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
            }
            else if (formats == "xls")
            {
                crExportOption.ExportFormatType = ExportFormatType.Excel;
            }
        }
        catch (Exception exx)
        { exx.Message.ToString(); }
        //reportdoc.Export();
        crReportDocument.Export();
        return "";
    }
    public string ExportandPrint11(string formats, string rptname, string filename, string selectionFormula, object repdoc)//,object dst)
    {
       
        string exportedpath = "";
        if (formats == "pdf")
        {
            exportedpath = filename; //+ "\\exported.pdf";
        }
        else if (formats == "xls")
        {
            exportedpath = filename;
        }
        ReportDocument crReportDocument = null;
        if (repdoc != null)
        {
            crReportDocument = (ReportDocument)repdoc;
        }
       
        try
        {
            
        }
        catch (Exception exx)
        { exx.Message.ToString(); }

        ExportOptions crExportOption;
        DiskFileDestinationOptions crDestOption = new DiskFileDestinationOptions();
        crDestOption.DiskFileName = exportedpath;
      
        crExportOption = crReportDocument.ExportOptions;
        crExportOption.ExportDestinationOptions = crDestOption;
        crExportOption.ExportDestinationType = ExportDestinationType.DiskFile;

        if (selectionFormula != "")
        {
            crReportDocument.RecordSelectionFormula = selectionFormula;
        }
       

        try
        {
            if (formats == "pdf")
            {
                crExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
            }
            else if (formats == "xls")
            {
                crExportOption.ExportFormatType = ExportFormatType.Excel;
            }
        }
        catch (Exception exx)
        { exx.Message.ToString(); }
        //reportdoc.Export();
        crReportDocument.Export();
        return "";
    }
    public string ExportDoctor(string formats, string rptname, string filename, string selectionFormula, object repdoc)//,object dst)
    {
        
        string exportedpath = "";
        if (formats == "pdf")
        {
            exportedpath = filename; //+ "\\exported.pdf";
        }
        else if (formats == "xls")
        {
            exportedpath = filename;
        }
        ReportDocument crReportDocument = null;
        if (repdoc != null)
        {
            crReportDocument = (ReportDocument)repdoc;
        }
       
        try
        {
            
        }
        catch (Exception exx)
        { exx.Message.ToString(); }

        ExportOptions crExportOption;
        DiskFileDestinationOptions crDestOption = new DiskFileDestinationOptions();
        crDestOption.DiskFileName = exportedpath;
        //crExportOption = reportdoc.ExportOptions;
        crExportOption = crReportDocument.ExportOptions;
        crExportOption.ExportDestinationOptions = crDestOption;
        crExportOption.ExportDestinationType = ExportDestinationType.DiskFile;

        if (selectionFormula != "")
        {
            crReportDocument.RecordSelectionFormula = selectionFormula;
        }
       

        try
        {
            if (formats == "pdf")
            {
                crExportOption.ExportFormatType = ExportFormatType.PortableDocFormat;
            }
            else if (formats == "xls")
            {
                crExportOption.ExportFormatType = ExportFormatType.Excel;
            }
        }
        catch (Exception exx)
        { exx.Message.ToString(); }
        //reportdoc.Export();
        crReportDocument.Export();
        return "";
    }
    public static bool isSessionSet()
    {
        if (HttpContext.Current.Session["username"] == null && HttpContext.Current.Session["usertype"] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static bool isRefUserLogin()
    {
        if (HttpContext.Current.Session["UnitCode"] == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
  
}
