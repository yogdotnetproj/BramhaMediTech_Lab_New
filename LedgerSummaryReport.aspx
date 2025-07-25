 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="LedgerSummaryReport.aspx.cs" Inherits="LedgerSummaryReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
            
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Ledger Summary Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Ledger Summary Report</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                               
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                       <asp:dropdownlist id="ddlCenter" runat="server" CssClass="form-control form-select" tabindex="3">
                </asp:dropdownlist>

                                    </div>
                                </div>
                                       <div class="col-sm-3">
                                           
                                                   <asp:button id="btnList" runat="server" OnClientClick="return validate();" text="Click" onclick="btnList_Click1" class="btn btn-info" />
                                                             
                                                   <asp:button id="btnreport" runat="server" OnClientClick="return validate();" 
                                                       text="Report" class="btn btn-primary" onclick="btnreport_Click" />
                                       </div>
                                               
                            </div>
                        </div>
                     
                    </div>
                     <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
          <asp:gridview id="GV_CenterLedger" class="table table-responsive table-sm table-bordered"  runat="server" autogeneratecolumns="False"
                    width="100%"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   onrowdatabound="GV_CenterLedger_RowDataBound1" onpageindexchanging="GV_CenterLedger_PageIndexChanging"
                    pagesize="100" onrowcreated="GV_CenterLedger_RowCreated">
                    <columns>                    
                     <asp:BoundField DataField="Centercode" HeaderText="Center Name" />
                     <asp:BoundField DataField="DebitAmt" HeaderText="Bill Amt" />
                     <asp:BoundField DataField="CreditAmt" HeaderText="Rec Amt" />
                      <asp:BoundField DataField="BalanceAmt" HeaderText="Balance Amt" />
                      
                    
                     </columns>
                     

                </asp:gridview>      
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                    
                   
                </section>
                <!-- /.content -->
          
            <!-- /.content-wrapper -->
           
         <script language="javascript" type="text/javascript">
             function OpenReport() {
                 window.open("Reports.aspx");
             } 
               </script>
               
   
   </asp:Content>
