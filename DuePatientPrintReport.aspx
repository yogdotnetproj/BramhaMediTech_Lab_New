 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="DuePatientPrintReport.aspx.cs" Inherits="DuePatientPrintReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
        
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Due Patient print Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Due Patient print Report</li> 
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
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  CssClass="form-control pull-right"  tabindex="1">
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
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  CssClass="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                
                              <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                       <asp:dropdownlist id="ddlCenter" runat="server" CssClass="form-control form-select" tabindex="3">
                                       </asp:dropdownlist>

                                    </div>
                                </div>
                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row mb-3">
                                <div class="col-lg-12 text-center">
                                   
                                     <asp:button id="btnlist" runat="server" OnClientClick="return validate();" font-bold="False" onclick="btnlist_Click"
                                            text="Click" CssClass="btn btn-info" />
                                        <asp:button id="btnreport1" OnClientClick="return validate();" runat="server" font-bold="False" onclick="Button1_Click"
                                           tooltip=" Report" CssClass="btn btn-primary" Text=" Report "
                                            tabindex="5" />
                    
                                   
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
                   <asp:gridview id="GV_DueReport" runat="server" class="table table-responsive table-sm table-bordered" onrowediting="GV_DueReport_RowEditing" width="100%"
                    onrowdatabound="GV_DueReport_RowDataBound"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False"
                    onpageindexchanging="GV_DueReport_PageIndexChanging">
                    <columns>
                        
                        <asp:BoundField DataField="RecDate" HeaderText=" Date" />                        
                          <asp:BoundField DataField="PatRegID" HeaderText=" Reg no" />
                          <asp:BoundField DataField="Patname" HeaderText=" Name" />
                          
                            
                           <asp:BoundField DataField="NetPayment" HeaderText="Charges" />
                            <asp:BoundField DataField="TaxAmount" Visible="false" HeaderText="Tax Amount" />
                           
                            <asp:BoundField DataField="AmtPaid" HeaderText="Paid Amt" />
                          <asp:BoundField DataField="Mainbalance" HeaderText=" Balance" />
                           <asp:BoundField DataField="BTHbalance" Visible="false" HeaderText="BTH Balance" />
                           <asp:BoundField DataField="Username" HeaderText="Printed By" />
                            <asp:BoundField DataField="Patphoneno" HeaderText="Phone No" />
                             <asp:BoundField DataField="Drname" HeaderText="Ref Dr" />
                               <asp:BoundField DataField="CenterName" HeaderText="Center" />
                               <asp:BoundField DataField="Remark" HeaderText="Remark" />
                           
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