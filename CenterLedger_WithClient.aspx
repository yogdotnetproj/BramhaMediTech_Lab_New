 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="CenterLedger_WithClient.aspx.cs" EnableEventValidation="false" Inherits="CenterLedger_WithClient" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
 
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Center Ledger</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Center Ledger</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date"  data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                        <div class="input-group date"  data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
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
                                       
                                       <asp:dropdownlist id="ddlCenter" runat="server" class="form-control form-select" tabindex="3">
                </asp:dropdownlist>

                                    </div>
                                </div>
                                          <div class="col-md-3">
                                                 
                                                   <asp:button id="btnList" runat="server" OnClientClick="return validate();" text="Click" onclick="btnList_Click1" class="btn btn-primary" />
                                                                       <asp:button id="btnreport" runat="server" OnClientClick="return validate();" 
                                                       text="Report" class="btn btn-warning" onclick="btnreport_Click" />
                                               <asp:button id="btnPDf" runat="server" OnClientClick="return validate();" 
                                                       text="PDF Report" class="btn btn-info" OnClick="btnPDf_Click"  />
                                          </div>
                                               
                            </div>
                        </div>
                       
                    </div>
                    <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
          <asp:gridview id="GV_CenterLedger"  class="table table-responsive table-sm table-bordered" runat="server" autogeneratecolumns="False"
                    width="100%"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   onrowdatabound="GV_CenterLedger_RowDataBound1" onpageindexchanging="GV_CenterLedger_PageIndexChanging"
                    pagesize="1500" onrowcreated="GV_CenterLedger_RowCreated">
                    <columns>                    
                     <asp:BoundField DataField="centername" HeaderText="Center Name"  />
                     <asp:BoundField DataField="tdate" HeaderText="Reg Date" />
                         <asp:BoundField DataField="patregid" HeaderText="Regno" />
                     <asp:BoundField DataField="patname" HeaderText="Patient name" />
                      <asp:BoundField DataField="testname" HeaderText="Testname" />
                      
                     <asp:BoundField DataField="debit" HeaderText="Debit Amount" />
                      <asp:BoundField DataField="credit" HeaderText="Credit Amount" />
                            <asp:BoundField DataField="discount" HeaderText="Discount" />
                     
                     <asp:BoundField DataField="Drname" HeaderText="drname" />
                      <asp:BoundField DataField="balance1" HeaderText="Balance" />
                       <asp:BoundField DataField="username" HeaderText="User Name" />
                    
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
