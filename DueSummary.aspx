 <%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Hospital.master" CodeFile="DueSummary.aspx.cs" Inherits="DueSummary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
     
            <!-- Content Wrapper. Contains page content -->
       
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Due Summary</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Clinical History EditDue Summaryli> 
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
                                       
                               
                <asp:dropdownlist id="DdlCenter" runat="server" CssClass="form-control form-select">
                </asp:dropdownlist>
                                    </div>
                                </div>
                               
                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">    
                    
 <asp:button id="btnlist" runat="server" OnClientClick="return validate();" CssClass="btn btn-info" onclick="btnlist_Click" text="Click"   />
 <asp:button id="btnreport" OnClientClick="return validate();" runat="server"  onclick="Button2_Click"  tooltip="Summary Report" Text="Summary  Report" CssClass="btn btn-primary" />
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
    <asp:gridview id="GV_Duesummary" runat="server" class="table table-responsive table-sm table-bordered" onrowediting="GV_Duesummary_RowEditing" width="100%"
                    onrowdatabound="GV_Duesummary_RowDataBound"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False"
                    onpageindexchanging="GV_Duesummary_PageIndexChanging">
                    <columns>
                        
                        <asp:BoundField DataField="RecDate" HeaderText=" Date" />                        
                          <asp:BoundField DataField="PatRegID" HeaderText=" Reg no" />
                          <asp:BoundField DataField="Patname" HeaderText=" Name" />
                          
                           
                           <asp:BoundField DataField="NetPayment" HeaderText="Charges" />
                           <asp:BoundField DataField="TaxAmount" HeaderText="Hst Amount" />
                           
                            <asp:BoundField DataField="AmtPaid" HeaderText="Paid Amt" />
                          <asp:BoundField DataField="Balance" HeaderText="Balance" />
                           <asp:BoundField DataField="Username" HeaderText="User Name" />
                           
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