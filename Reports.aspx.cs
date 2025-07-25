using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using CrystalDecisions;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class Reports : System.Web.UI.Page
{
    dbconnection da = new dbconnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FetchReport();
        }
    }
    protected void FetchReport()
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument rep = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        ReportDataSet Objrds = new ReportDataSet();
        switch (Session["reportname"].ToString())
        {
            case "Rpt_TestWiseServiceReport":
                ReportDataSet.VW_TestwisesalereportDataTable Objhp12 = Objrds.VW_Testwisesalereport;
                ReportDataSetTableAdapters.VW_TestwisesalereportTableAdapter Objhp2 = new ReportDataSetTableAdapters.VW_TestwisesalereportTableAdapter();
                Objhp2.Fill(Objhp12);
                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_TestWiseServiceReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "Billforhospital":
                ReportDataSet.VW_bthDataTable Objhp1 = Objrds.VW_bth;
                ReportDataSetTableAdapters.VW_bthTableAdapter Objhp = new ReportDataSetTableAdapters.VW_bthTableAdapter();
                Objhp.Fill(Objhp1);
                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_Billforhosp.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "DoctorList":
                ReportDataSet.DrMTDataTable ObjDr1 = Objrds.DrMT;
                ReportDataSetTableAdapters.DrMTTableAdapter ObjDr = new ReportDataSetTableAdapters.DrMTTableAdapter();              
                ObjDr.Fill(ObjDr1);
                rep.Load(Server.MapPath("~//DiagnosticReport//DoctorList.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "CashReceipt_ClickSample":
                ReportDataSet.VW_cshbill_ClickSampleDataTable Objcbck1 = Objrds.VW_cshbill_ClickSample;
                ReportDataSetTableAdapters.VW_cshbill_ClickSampleTableAdapter Objcbck = new ReportDataSetTableAdapters.VW_cshbill_ClickSampleTableAdapter();
                Objcbck.Fill(Objcbck1);
                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_PayReceipt_clickSample.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                PatSt_Bal_C PBC1 = new PatSt_Bal_C();
                if (Convert.ToInt32(Session["RecNo_report"]) == 0)
                {
                    PBC1.Update_PrintCount(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["PID_report"]));
                }
                else
                {
                    PBC1.Update_PrintCount_ReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["PID_report"]), Convert.ToInt32(Session["RecNo_report"]));
                }
                Session["PID_report"] = 0;
                Session["RecNo_report"] = 0;
                break;
            case "CashReceipt":
                ReportDataSet.VW_cshbillDataTable Objcb1 = Objrds.VW_cshbill;
                ReportDataSetTableAdapters.VW_cshbillTableAdapter Objcb = new ReportDataSetTableAdapters.VW_cshbillTableAdapter();
                Objcb.Fill(Objcb1);
                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_PayReceipt.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                PatSt_Bal_C PBC = new PatSt_Bal_C();
                if (Convert.ToInt32( Session["RecNo_report"]) == 0)
                {
                    PBC.Update_PrintCount(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["PID_report"]));
                }
                else
                {
                    PBC.Update_PrintCount_ReceiptNo(Convert.ToInt32(Session["Branchid"]), Convert.ToInt32(Session["PID_report"]), Convert.ToInt32(Session["RecNo_report"]));
                }
                Session["PID_report"] = 0;
                Session["RecNo_report"] = 0;
                break;
            case "CashBillReceipt":
                ReportDataSet.VW_csrstvwDataTable Objcb11 = Objrds.VW_csrstvw;
                ReportDataSetTableAdapters.VW_csrstvwTableAdapter Objcb13 = new ReportDataSetTableAdapters.VW_csrstvwTableAdapter();
                Objcb13.Fill(Objcb11);
                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_Receipt.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "RegistrationReceipt":
                ReportDataSet.VW_patregformDataTable Objrr1 = Objrds.VW_patregform;
                ReportDataSetTableAdapters.VW_patregformTableAdapter Objrr = new ReportDataSetTableAdapters.VW_patregformTableAdapter();
                Objrr.Fill(Objrr1);
                rep.Load(Server.MapPath("~//DiagnosticReport//RegistrationReceipt.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "DailyCashSummary":
                ReportDataSet.VW_dsbillDataTable Objdr1 = Objrds.VW_dsbill;
                ReportDataSetTableAdapters.VW_dsbillTableAdapter Objdr = new ReportDataSetTableAdapters.VW_dsbillTableAdapter();
                Objdr.Fill(Objdr1);
                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_dailycashier_Summary.rpt"));//Rpt_dailycashierDet
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

            case "DailyCash":
                ReportDataSet.VW_dsbillDataTable Objdr11 = Objrds.VW_dsbill;
                ReportDataSetTableAdapters.VW_dsbillTableAdapter Objdr12 = new ReportDataSetTableAdapters.VW_dsbillTableAdapter();
                Objdr12.Fill(Objdr11);

                //ReportDataSet.VW_dsexpDataTable Objdce = Objrds.VW_dsexp;
                //ReportDataSetTableAdapters.VW_dsexpTableAdapter Objdce1 = new ReportDataSetTableAdapters.VW_dsexpTableAdapter();
                //Objdce1.Fill(Objdce);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_dailycashier.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "CenterWiseIncomeSummary":
                ReportDataSet.VW_dssummDataTable Objcwis1 = Objrds.VW_dssumm;
                ReportDataSetTableAdapters.VW_dssummTableAdapter Objcwis = new ReportDataSetTableAdapters.VW_dssummTableAdapter();
                Objcwis.Fill(Objcwis1);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_displayreport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "CenterWiseoutstandingDetail":
                ReportDataSet.VW_dsdueDataTable Objcwss1 = Objrds.VW_dsdue;
                ReportDataSetTableAdapters.VW_dsdueTableAdapter Objcwss = new ReportDataSetTableAdapters.VW_dsdueTableAdapter();
                Objcwss.Fill(Objcwss1);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_pendingpayment.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "ComplimentReport":
                ReportDataSet.VW_drspmainDataTable Objcr1 = Objrds.VW_drspmain;
                ReportDataSetTableAdapters.VW_drspmainTableAdapter Objcr = new ReportDataSetTableAdapters.VW_drspmainTableAdapter();
                Objcr.Fill(Objcr1);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_docshareRep.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "ComplimentReport_Sum":
                ReportDataSet.VW_drspmainDataTable Objcr15 = Objrds.VW_drspmain;
                ReportDataSetTableAdapters.VW_drspmainTableAdapter Objcr5 = new ReportDataSetTableAdapters.VW_drspmainTableAdapter();
                Objcr5.Fill(Objcr15);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_docshareRep_Sum.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "ComplimentDetail":
                ReportDataSet.VW_drspdtDataTable Objcrd1 = Objrds.VW_drspdt;
                ReportDataSetTableAdapters.VW_drspdtTableAdapter Objcrd = new ReportDataSetTableAdapters.VW_drspdtTableAdapter();
                Objcrd.Fill(Objcrd1);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_docshare.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "PrintBarcode":
                ReportDataSet.VW_patstkvwDataTable Objpbc1 = Objrds.VW_patstkvw;
                ReportDataSetTableAdapters.VW_patstkvwTableAdapter Objpbc = new ReportDataSetTableAdapters.VW_patstkvwTableAdapter();
                Objpbc.Fill(Objpbc1);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_PrintBarcode.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            //case "PateintGraphReport":
            //    ReportDataSet.VW_patrecvwDataTable Objpgr1 = Objrds.VW_patrecvw;
            //    ReportDataSetTableAdapters.VW_patrecvwTableAdapter Objpgr = new ReportDataSetTableAdapters.VW_patrecvwTableAdapter();
            //    Objpgr.Fill(Objpgr1);

            //    rep.Load(Server.MapPath("~//DiagnosticReport//Patientgraphicalreport.rpt"));
            //    rep.SetDataSource((System.Data.DataSet)Objrds);
            //    break;
            case "NonDesc":
                //ReportDataSet.rpt_testresultDataTable Objtr1 = Objrds.VW_patdatvwrecvw;
                //ReportDataSetTableAdapters.rpt_testresultTableAdapter Objtr = new ReportDataSetTableAdapters.rpt_testresultTableAdapter();
                //Objtr.Fill(Objtr1);
                ReportDataSet.VW_patdatvwrecvwDataTable Objtr1 = Objrds.VW_patdatvwrecvw;
                ReportDataSetTableAdapters.VW_patdatvwrecvwTableAdapter Objtr = new ReportDataSetTableAdapters.VW_patdatvwrecvwTableAdapter();
                Objtr.Fill(Objtr1);
            
                if (Convert.ToString(Session["usertype"]) == "Reference Doctor")
                {
                    rep.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive_email.rpt"));
                 }
                else
                {
                    rep.Load(Server.MapPath("~//DiagnosticReport//Pateintreportnondescriptive.rpt"));
                  
                }
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "DescType":
                ReportDataSet.VW_desfiledata_orgDataTable Objvmf1 = Objrds.VW_desfiledata_org;
                ReportDataSetTableAdapters.VW_desfiledata_orgTableAdapter Objvmf = new ReportDataSetTableAdapters.VW_desfiledata_orgTableAdapter();
                Objvmf.Fill(Objvmf1);

                rep.Load(Server.MapPath("~//DiagnosticReport//Pateintreportdescriptive.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

           
           
            case "Rpt_CenterWiseIncomeDetailWithDept":
                ReportDataSet.VW_dsduedeptDataTable ObjDRD2 = Objrds.VW_dsduedept;
                ReportDataSetTableAdapters.VW_dsduedeptTableAdapter ObjDRD1 = new ReportDataSetTableAdapters.VW_dsduedeptTableAdapter();
                ObjDRD1.Fill(ObjDRD2);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_CenInsummD.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "Rpt_CenterWiseIncomeSummary":
                ReportDataSet.VW_dssummDataTable ObjDsr = Objrds.VW_dssumm;
                ReportDataSetTableAdapters.VW_dssummTableAdapter ObjDsr1 = new ReportDataSetTableAdapters.VW_dssummTableAdapter();
                ObjDsr1.Fill(ObjDsr);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_CenInsumm.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "DailyAllDetails":
                ReportDataSet.VW_dprDataTable ObjDsr2 = Objrds.VW_dpr;
                ReportDataSetTableAdapters.VW_dprTableAdapter ObjDsr12 = new ReportDataSetTableAdapters.VW_dprTableAdapter();
                ObjDsr12.Fill(ObjDsr2);

                ReportDataSet.VW_dprdtDataTable ObjDsr3 = Objrds.VW_dprdt;
                ReportDataSetTableAdapters.VW_dprdtTableAdapter ObjDsr13 = new ReportDataSetTableAdapters.VW_dprdtTableAdapter();
                ObjDsr13.Fill(ObjDsr3);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_DailyAllDetails.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "Materializereport":
                ReportDataSet.VW_patmatvwDataTable Objmr = Objrds.VW_patmatvw;
                ReportDataSetTableAdapters.VW_patmatvwTableAdapter Objmr1 = new ReportDataSetTableAdapters.VW_patmatvwTableAdapter();
                Objmr1.Fill(Objmr);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_Materalizereport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            //case "Deptwisesalereport":
            //    ReportDataSet.VW_dpsaleDataTable Objdsr = Objrds.VW_dpsale;
            //    ReportDataSetTableAdapters.VW_dpsaleTableAdapter Objdsr1 = new ReportDataSetTableAdapters.VW_dpsaleTableAdapter();
            //    Objdsr1.Fill(Objdsr);

            //    rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_groupwisesale.rpt"));
            //    rep.SetDataSource((System.Data.DataSet)Objrds);
            //    break;
            case "Discountreport":
                ReportDataSet.VW_disdatavwDataTable Objgdr = Objrds.VW_disdatavw;
                ReportDataSetTableAdapters.VW_disdatavwTableAdapter Objgdr1 = new ReportDataSetTableAdapters.VW_disdatavwTableAdapter();
                Objgdr1.Fill(Objgdr);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_Discountreport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "DoctorIPReport":
                ReportDataSet.VW_drdatavwDataTable Objgdi = Objrds.VW_drdatavw;
                ReportDataSetTableAdapters.VW_drdatavwTableAdapter Objgdi1 = new ReportDataSetTableAdapters.VW_drdatavwTableAdapter();
                Objgdi1.Fill(Objgdi);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_DoctorIPReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "Deptwisesalereport_IR":
                ReportDataSet.VW_dpsalIRDataTable Objdsr15 = Objrds.VW_dpsalIR;
                ReportDataSetTableAdapters.VW_dpsalIRTableAdapter Objdsr115 = new ReportDataSetTableAdapters.VW_dpsalIRTableAdapter();
                Objdsr115.Fill(Objdsr15);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_groupwisesale_I.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
                //==========================================================
            case "saleregisterreport_IR":
                ReportDataSet.VW_salerpirvwDataTable Objdsr150 = Objrds.VW_salerpirvw;
                ReportDataSetTableAdapters.VW_salerpirvwTableAdapter Objdsr1150 = new ReportDataSetTableAdapters.VW_salerpirvwTableAdapter();
                Objdsr1150.Fill(Objdsr150);

                rep.Load(Server.MapPath("~//DiagnosticReport//saleregisterreport_IR.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
           
            case "TATCalcpatientwise":
                ReportDataSet.VW_TAT_Calculation_patientwiseDataTable Objcp1 = Objrds.VW_TAT_Calculation_patientwise;
                ReportDataSetTableAdapters.VW_TAT_Calculation_patientwiseTableAdapter Objcp = new ReportDataSetTableAdapters.VW_TAT_Calculation_patientwiseTableAdapter();
                Objcp.Fill(Objcp1);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_TATCalcpatientwise.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "TATCalcReport":
                ReportDataSet.VW_TAT_CalculationDataTable ObjVTC1 = Objrds.VW_TAT_Calculation;
                ReportDataSetTableAdapters.VW_TAT_CalculationTableAdapter ObjVTC = new ReportDataSetTableAdapters.VW_TAT_CalculationTableAdapter();
                ObjVTC.Fill(ObjVTC1);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_TATCalcReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "UserDetaila":
                ReportDataSet.VW_UserdetailsDataTable ObjUD = Objrds.VW_Userdetails;
                ReportDataSetTableAdapters.VW_UserdetailsTableAdapter ObjUD1 = new ReportDataSetTableAdapters.VW_UserdetailsTableAdapter();
                ObjUD1.Fill(ObjUD);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_UserDetaila.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "Rpt_SaleRegister_NP":
                 ReportDataSet.VW_BillCancel_ReportDataTable ObjBCR0 = Objrds.VW_BillCancel_Report;
                ReportDataSetTableAdapters.VW_BillCancel_ReportTableAdapter ObjBCR10 = new ReportDataSetTableAdapters.VW_BillCancel_ReportTableAdapter();
                ObjBCR10.Fill(ObjBCR0);
                ReportDataSet.VW_SaleReport_NPDataTable ObjSRNP = Objrds.VW_SaleReport_NP;
                ReportDataSetTableAdapters.VW_SaleReport_NPTableAdapter ObjSRNP1 = new ReportDataSetTableAdapters.VW_SaleReport_NPTableAdapter();
                ObjSRNP1.Fill(ObjSRNP);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_SaleRegister_NP.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "SaleSummaryRegister":
                ReportDataSet.VW_SalesummaryReport_NPDataTable ObjSuRNP = Objrds.VW_SalesummaryReport_NP;
                ReportDataSetTableAdapters.VW_SalesummaryReport_NPTableAdapter ObjSuRNP1 = new ReportDataSetTableAdapters.VW_SalesummaryReport_NPTableAdapter();
                ObjSuRNP1.Fill(ObjSuRNP);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_SaleSummaryRegister_NP.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "Bill_CancelReport":
                
                ReportDataSet.VW_BillCancel_ReportDataTable ObjBCR = Objrds.VW_BillCancel_Report;
                ReportDataSetTableAdapters.VW_BillCancel_ReportTableAdapter ObjBCR1 = new ReportDataSetTableAdapters.VW_BillCancel_ReportTableAdapter();
                ObjBCR1.Fill(ObjBCR);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_Bill_CancelReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "Materialize_report":

                ReportDataSet.VW_TblMaterializeViewDataTable ObjVTM = Objrds.VW_TblMaterializeView;
                ReportDataSetTableAdapters.VW_TblMaterializeViewTableAdapter ObjVTM1 = new ReportDataSetTableAdapters.VW_TblMaterializeViewTableAdapter();
                ObjVTM1.Fill(ObjVTM);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_Materialize_report.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "CancelReceipt":

                ReportDataSet.VW_billCancelInvoiceDataTable ObjVTM2 = Objrds.VW_billCancelInvoice;
                ReportDataSetTableAdapters.VW_billCancelInvoiceTableAdapter ObjVTM21 = new ReportDataSetTableAdapters.VW_billCancelInvoiceTableAdapter();
                ObjVTM21.Fill(ObjVTM2);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_CancelReceipt.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "DailyWorklist":
                ReportDataSet.VW_WorklistDataTable ObjVWL = Objrds.VW_Worklist;
                ReportDataSetTableAdapters.VW_WorklistTableAdapter ObjVWL1 = new ReportDataSetTableAdapters.VW_WorklistTableAdapter();
                ObjVWL1.Fill(ObjVWL);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_DailyWorklist.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "Rpt_LedgerSummary":
                ReportDataSet.VW_Get_Ledgersummary_ReportDataTable ObjLSR = Objrds.VW_Get_Ledgersummary_Report;
                ReportDataSetTableAdapters.VW_Get_Ledgersummary_ReportTableAdapter ObjLSR1 = new ReportDataSetTableAdapters.VW_Get_Ledgersummary_ReportTableAdapter();
                ObjLSR1.Fill(ObjLSR);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_LedgerSummary.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

            case "Rpt_CenterRecAmtreport":
                ReportDataSet.VW_CenterPAymentReceiveDataTable Objcpr = Objrds.VW_CenterPAymentReceive;
                ReportDataSetTableAdapters.VW_CenterPAymentReceiveTableAdapter Objcpr1 = new ReportDataSetTableAdapters.VW_CenterPAymentReceiveTableAdapter();
                Objcpr1.Fill(Objcpr);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_CenterRecAmtreport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
            case "ReferalDeptshare":
                ReportDataSet.VW_ReferalDepartmentShareDataTable Objreds = Objrds.VW_ReferalDepartmentShare;
                ReportDataSetTableAdapters.VW_ReferalDepartmentShareTableAdapter Objreds1 = new ReportDataSetTableAdapters.VW_ReferalDepartmentShareTableAdapter();
                Objreds1.Fill(Objreds);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_ReferalDeptshare.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
           
               case "ReferalDeptsharesum":
                ReportDataSet.VW_ReferalDepartmentShareDataTable Objreds2 = Objrds.VW_ReferalDepartmentShare;
                ReportDataSetTableAdapters.VW_ReferalDepartmentShareTableAdapter Objreds12 = new ReportDataSetTableAdapters.VW_ReferalDepartmentShareTableAdapter();
                Objreds12.Fill(Objreds2);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_ReferalDeptshare_Summery.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
               case "ComplimentReport_Summary":
                ReportDataSet.VW_drspmain_summaryDataTable Objreds22 = Objrds.VW_drspmain_summary;
                ReportDataSetTableAdapters.VW_drspmain_summaryTableAdapter Objreds122 = new ReportDataSetTableAdapters.VW_drspmain_summaryTableAdapter();
                Objreds122.Fill(Objreds22);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_docshareRep.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

               case "ComplimentReport_Summary1":
                ReportDataSet.VW_drspmain_summaryDataTable Objreds221 = Objrds.VW_drspmain_summary;
                ReportDataSetTableAdapters.VW_drspmain_summaryTableAdapter Objreds1221 = new ReportDataSetTableAdapters.VW_drspmain_summaryTableAdapter();
                Objreds1221.Fill(Objreds221);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_docshareRep_summary.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
               case "PrintBarcode_dept":
                ReportDataSet.VW_patstkvw_DeptwiseDataTable Objpbc12 = Objrds.VW_patstkvw_Deptwise;
                ReportDataSetTableAdapters.VW_patstkvw_DeptwiseTableAdapter Objpbc2 = new ReportDataSetTableAdapters.VW_patstkvw_DeptwiseTableAdapter();
                Objpbc2.Fill(Objpbc12);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_PrintBarcode_deptwise.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
               case "PatientCard":
                ReportDataSet.VW_PatientCardDataTable ObjPCR = Objrds.VW_PatientCard;
                ReportDataSetTableAdapters.VW_PatientCardTableAdapter ObjPCR1 = new ReportDataSetTableAdapters.VW_PatientCardTableAdapter();
                ObjPCR1.Fill(ObjPCR);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_PatientCard.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
               case "ExpenceReport":
                ReportDataSet.VW_ExpenceReportDataTable ObjER = Objrds.VW_ExpenceReport;
                ReportDataSetTableAdapters.VW_ExpenceReportTableAdapter ObjER1 = new ReportDataSetTableAdapters.VW_ExpenceReportTableAdapter();
                ObjER1.Fill(ObjER);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_ExpenceReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
               case "DeltaResult":
                ReportDataSet.VW_DeltaResultDataTable ObjDR = Objrds.VW_DeltaResult;
                ReportDataSetTableAdapters.VW_DeltaResultTableAdapter ObjDR1 = new ReportDataSetTableAdapters.VW_DeltaResultTableAdapter();
                ObjDR1.Fill(ObjDR);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_DeltaResult.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

               case "Rpt_CenetrRateList":
                ReportDataSet.VW_CeneterrateListDataTable ObjCRL = Objrds.VW_CeneterrateList;
                ReportDataSetTableAdapters.VW_CeneterrateListTableAdapter ObjCRL1 = new ReportDataSetTableAdapters.VW_CeneterrateListTableAdapter();
                ObjCRL1.Fill(ObjCRL);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_CenetrRateList.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

               case "Outstandingprintreport":
                ReportDataSet.VW_dsdueDataTable Objcwss12 = Objrds.VW_dsdue;
                ReportDataSetTableAdapters.VW_dsdueTableAdapter Objcwss2 = new ReportDataSetTableAdapters.VW_dsdueTableAdapter();
                Objcwss2.Fill(Objcwss12);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_Outstandingprintreport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
               case "DailyWorklistReport":
                ReportDataSet.VW_WorklistWithReportDataTable ObjVWL2 = Objrds.VW_WorklistWithReport;
                ReportDataSetTableAdapters.VW_WorklistWithReportTableAdapter ObjVWL12 = new ReportDataSetTableAdapters.VW_WorklistWithReportTableAdapter();
                ObjVWL12.Fill(ObjVWL2);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_DailyWorklistWithReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

               case "DailyDetailsReport":
                ReportDataSet.VW_DailyDetailsReportDataTable ObjER9 = Objrds.VW_DailyDetailsReport;
                ReportDataSetTableAdapters.VW_DailyDetailsReportTableAdapter ObjER19 = new ReportDataSetTableAdapters.VW_DailyDetailsReportTableAdapter();
                ObjER19.Fill(ObjER9);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_DailyDetailsReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

               case "Rpt_DrIncomeReport":
                ReportDataSet.VW_dsduedeptDataTable ObjDRD2r = Objrds.VW_dsduedept;
                ReportDataSetTableAdapters.VW_dsduedeptTableAdapter ObjDRD1r = new ReportDataSetTableAdapters.VW_dsduedeptTableAdapter();
                ObjDRD1r.Fill(ObjDRD2r);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_DrIncomeReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

               case "Rpt_ActivityDetails":
                ReportDataSet.VW_ActivityDetailsDataTable ObjAD = Objrds.VW_ActivityDetails;
                ReportDataSetTableAdapters.VW_ActivityDetailsTableAdapter ObjAD1 = new ReportDataSetTableAdapters.VW_ActivityDetailsTableAdapter();
                ObjAD1.Fill(ObjAD);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_ActivityDetails.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

               case "Rpt_PatientTestStatusReport":
                ReportDataSet.VW_PatientTestStatusReportDataTable ObjPTST = Objrds.VW_PatientTestStatusReport;
                ReportDataSetTableAdapters.VW_PatientTestStatusReportTableAdapter ObjPTST1 = new ReportDataSetTableAdapters.VW_PatientTestStatusReportTableAdapter();
                ObjPTST1.Fill(ObjPTST);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_PatientTestStatusReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

                 case "Rpt_CenterLedger_Report":
                ReportDataSet.VW_CenterLedger_ReporttDataTable ObjCLR = Objrds.VW_CenterLedger_Reportt;
                ReportDataSetTableAdapters.VW_CenterLedger_ReporttTableAdapter ObjCLR1 = new ReportDataSetTableAdapters.VW_CenterLedger_ReporttTableAdapter();
                ObjCLR1.Fill(ObjCLR);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_CenterLedger_Report.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

                 case "Compliment_Report_Sum_Rep":
                ReportDataSet.VW_drspmainDataTable Objcr15S = Objrds.VW_drspmain;
                ReportDataSetTableAdapters.VW_drspmainTableAdapter Objcr5S = new ReportDataSetTableAdapters.VW_drspmainTableAdapter();
                Objcr5S.Fill(Objcr15S);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_docshare_Sum_Rept.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

                 case "Rpt_ShowAppoinmentPatient":
                ReportDataSet.VW_GetAppoinmentBookDataTable ObjGAB = Objrds.VW_GetAppoinmentBook;
                ReportDataSetTableAdapters.VW_GetAppoinmentBookTableAdapter ObjGAB1 = new ReportDataSetTableAdapters.VW_GetAppoinmentBookTableAdapter();
                ObjGAB1.Fill(ObjGAB);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_ShowAppoinmentPatient.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;

                 case "Operator_Summary_withClient":
                ReportDataSet.VW_Operator_Summary_withClientDataTable ObjGAB2 = Objrds.VW_Operator_Summary_withClient;
                ReportDataSetTableAdapters.VW_Operator_Summary_withClientTableAdapter ObjGAB12 = new ReportDataSetTableAdapters.VW_Operator_Summary_withClientTableAdapter();
                ObjGAB12.Fill(ObjGAB2);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_Operator_Summary_withClient.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
                 case "DepartmentwiseTestCount":
                ReportDataSet.VW_GetDepartmentwiseCountDataTable ObjGDC1 = Objrds.VW_GetDepartmentwiseCount;
                ReportDataSetTableAdapters.VW_GetDepartmentwiseCountTableAdapter ObjGDC = new ReportDataSetTableAdapters.VW_GetDepartmentwiseCountTableAdapter();
                ObjGDC.Fill(ObjGDC1);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_DepartmentwiseTestCount.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
                 case "LabStatisticReport":
                ReportDataSet.VW_LabStatistics_ReportDataTable ObjLSRM = Objrds.VW_LabStatistics_Report;
                ReportDataSetTableAdapters.VW_LabStatistics_ReportTableAdapter ObjLSRM1 = new ReportDataSetTableAdapters.VW_LabStatistics_ReportTableAdapter();
                ObjLSRM1.Fill(ObjLSRM);

                ReportDataSet.VW_AllTestCountDataTable ObjLSRM5 = Objrds.VW_AllTestCount;
                ReportDataSetTableAdapters.VW_AllTestCountTableAdapter ObjLSRM15 = new ReportDataSetTableAdapters.VW_AllTestCountTableAdapter();
                ObjLSRM15.Fill(ObjLSRM5);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_LabStatisticReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
                 case "DrLabStatisticReport":
                ReportDataSet.VW_drLabStatistics_ReportDataTable ObjDLSRM = Objrds.VW_drLabStatistics_Report;
                ReportDataSetTableAdapters.VW_drLabStatistics_ReportTableAdapter ObjDLSRM1 = new ReportDataSetTableAdapters.VW_drLabStatistics_ReportTableAdapter();
                ObjDLSRM1.Fill(ObjDLSRM);

                rep.Load(Server.MapPath("~//DiagnosticReport//Rpt_DrLabStatisticReport.rpt"));
                rep.SetDataSource((System.Data.DataSet)Objrds);
                break;
                
        }
        string rptname = Session["rptname"].ToString();
        string path = Server.MapPath("~/Reports");
        string sql = Session["rptsql"].ToString();

        string rptuser = Convert.ToString(Session["rptusername"]);
        string rptmodule = Convert.ToString(Session["rptModule"]);
        string rptdate = Convert.ToString(Session["rptDate"]);
       

        if (Session["RPTFORMAT"] == "EXCEL")
        {
           // da.ExportingAndPrintingCrystalRpt("pdf1", rptname, path, sql, rep, rptuser, rptmodule, rptdate);
           // da.ExportingAndPrintingCrystalRpt("Excel", rptname, path, sql, rep);
            if (Convert.ToString(Session["Parameter"]) == "NO")
            {
                // da.ExportingAndPrintingCrystalRpt("pdf", rptname, path, sql, rep, rptuser, rptmodule, rptdate);
                da.ExportingAndPrintingCrystalRpt("Excel", rptname, path, sql, rep,"");
            }
            else
            {
                da.ExportingAndPrintingCrystalRpt_Parameter("Excel", rptname, path, sql, rep, rptdate, rptuser,"");
            }
        }
        else
        {
           // da.ExportingAndPrintingCrystalRpt("pdf", rptname, path, sql, rep, rptuser, rptmodule, rptdate);
            //da.ExportingAndPrintingCrystalRpt("pdf", rptname, path, sql, rep);
            if (Convert.ToString(Session["Parameter"]) == "NO")
            {
                // da.ExportingAndPrintingCrystalRpt("pdf", rptname, path, sql, rep, rptuser, rptmodule, rptdate);
                string Date1 = DateTime.Now.ToString("ddMMyyyy");
                string filename1 = Server.MapPath("Reports//" + Date1 + "-" + Session["reportname"] + " ");

               // System.IO.File.WriteAllText(filename1, "");
                da.ExportingAndPrintingCrystalRpt("pdf", rptname, path, sql, rep, filename1);
            }
            else
            {
                string Date1 = DateTime.Now.ToString("ddMMyyyy");
                string filename1 = Server.MapPath("Reports//" + Date1 + "-" + Session["reportname"] + " ");

               // System.IO.File.WriteAllText(filename1, "");
                da.ExportingAndPrintingCrystalRpt_Parameter("pdf", rptname, path, sql, rep, rptdate, rptuser, filename1);
            }
        }

        rep.Close();
        GC.SuppressFinalize(rep);
        rep.Dispose();
        GC.Collect();
        // Session.Abandon();
        // Session.Clear();
        // Session.RemoveAll();        
        Session["Parameter"] = "NO";
        if (Session["RPTFORMAT"] == "EXCEL")
        {
            Session["RPTFORMAT"] = "pdf";
            Response.Redirect("~/Reports\\exportedinxls.xls");
        }
        else
        {
            string Date1 = DateTime.Now.ToString("ddMMyyyy");
           // Response.Redirect("~/Reports\\Exported.pdf");
            Response.Redirect("~/Reports\\"+Date1 + "-" + Session["reportname"] + ".pdf");
        }
    }
}