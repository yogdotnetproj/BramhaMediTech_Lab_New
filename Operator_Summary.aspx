 <%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Hospital.master" CodeFile="Operator_Summary.aspx.cs" Inherits="Operator_Summary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
   
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Operator wise  summery</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Operator wise  summery</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
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
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        
                                      
                <asp:dropdownlist id="ddlusername" runat="server" class="form-control" tabindex="3">
                </asp:dropdownlist>
                                    </div>
                                </div>
                               
                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row mb-3">
                                <div class="col-lg-12 text-center">
                                   
                                    
                    
<asp:button id="btnlist" runat="server" OnClientClick="return validate();" onclick="btnlist_Click" text="Click" Visible="false" class="btn btn-primary" />
                                        <asp:button id="btnreport" OnClientClick="return validate();" runat="server" class="btn btn-info" onclick="btnreport_Click"
                                           tooltip="Summary Report" Text="Summary Report" />
                                            
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body" runat="server" visible="false">
                            <div class="table-responsive" style="width:100%">
     <div class="rounded_corners" style="width:100%">
                <asp:gridview id="GV_OpSummary" runat="server" class="table table-responsive table-sm table-bordered" width="100%" onrowdatabound="GV_OpSummary_RowDataBound"
                    onrowediting="GV_OpSummary_RowEditing"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False" PageSize="25"
                    allowpaging="True" onpageindexchanging="GV_OpSummary_PageIndexChanging">
                    <columns>
                           
                            <asp:BoundField DataField="RecDate" HeaderText="Date" />
                              <asp:BoundField DataField="PatRegID" HeaderText="RegNo" />
                               <asp:BoundField DataField="PatName" HeaderText=" Name" />
                           
                         
                           
                          
                            
                            <asp:BoundField DataField="Patphoneno" HeaderText="Phone No" />
                            
                             <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                              <asp:BoundField DataField="NetPayment" HeaderText="Bill Amount" />

                              <asp:BoundField DataField="Discount" HeaderText="Discount" />
                              <asp:BoundField DataField="TaxAmount" HeaderText="Hst Amount" />
                             
                               <asp:BoundField DataField="AmtPaid" HeaderText="Amt Received" />
                               <asp:BoundField DataField="Mainbalance" HeaderText="Main Balance" />
                                 <asp:BoundField DataField="BTHbalance" HeaderText="BTH Balance" />
                               
                                <asp:BoundField DataField="username" HeaderText="UserName" />
                        </columns>
                       

                </asp:gridview>
                </div> 
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