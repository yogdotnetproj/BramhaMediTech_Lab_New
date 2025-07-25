 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Discountreport.aspx.cs" MasterPageFile="~/Hospital.master" Inherits="Discountreport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
     
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Discount Report</h1>
                    <ol class="breadcrumb">
                    
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Discount Report </li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-4">
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
                                 <div class="col-lg-2">
                                    <div class="form-group">
                                        <label></label>
               
                                    </div>
                                </div>

                                <div class="col-lg-4">
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
                               
                               
                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">    
                    
                                <asp:button id="btnlist" runat="server" OnClientClick="return validate();" onclick="btnlist_Click" text="Click" class="btn btn-primary"
                                            />
                                        <asp:button id="btnreport1" OnClientClick="return validate();" runat="server" class="btn btn-primary" onclick="btnreport1_Click"
                                            tooltip="Discountwise Report" Text="Discountwise Report"
                                            />
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
   <asp:gridview id="GV_Discount" runat="server" class="table table-responsive table-sm table-bordered" width="100%" onrowdatabound="GV_Discount_RowDataBound"
                    onrowediting="GV_Discount_RowEditing"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False" PageSize="25"
                    allowpaging="True" onpageindexchanging="GV_Discount_PageIndexChanging">
                    <columns>
                           
                            <asp:BoundField DataField="BillDate" HeaderText="Date" />
                              <asp:BoundField DataField="RegNo" HeaderText="RegNo" />
                               <asp:BoundField DataField="Patientname" HeaderText=" Name" />
                           
                         
                           
                          
                            
                            <asp:BoundField DataField="PatientPhoneNo" HeaderText="Phone No" />
                            
                             <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                              <asp:BoundField DataField="NetPayment" HeaderText="Bill Amount" />
                              <asp:BoundField DataField="Discount" HeaderText="Discount" />
                             
                               <asp:BoundField DataField="DocName" HeaderText="Ref Doc" />
                               <asp:BoundField DataField="Patienttest" HeaderText=" Test" />
                                <asp:BoundField DataField="username" HeaderText="UserName" />
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