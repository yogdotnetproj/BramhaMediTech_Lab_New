 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="ActivityLog.aspx.cs" MasterPageFile="~/Hospital.master"  Inherits="ActivityLog" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Activity Log</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Activity Log</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                          
                         
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label>From Date:</label>
                                        <div class="input-group date">
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
                                        <label>To Date:</label>
                                        <div class="input-group date">
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
                                   
                                     <asp:button id="btnlist" runat="server" OnClientClick="return validate();" onclick="btnlist_Click"
                                            text="Click" class="btn btn-primary" />
                                      
                                            <asp:button id="btnreport1"  runat="server" Visible="false" class="btn btn-primary" onclick="btnreport1_Click"
                                            tooltip="Sale summary Report" Text="Sale summary Report"
                                            />
                     <asp:button id="btnexcel"  runat="server" class="btn btn-primary" 
                                            tooltip="Excel Activity Log Report"  Text="Excel Activity Log Report" onclick="btnexcel_Click"
                                            />
                                   
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="rounded_corners" style="width:100%">
                             <div style=" overflow: scroll; width: 1060px; height: 1000px; text-align: right"
                                                        id="Divdoc">
                   <asp:gridview id="GridView1" runat="server" width="100%" onrowdatabound="GridView1_RowDataBound"
                    onrowediting="GridView1_RowEditing"   HeaderStyle-ForeColor="Black"
                     AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False" PageSize="55000"
                    allowpaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
                                     onrowcreated="GridView1_RowCreated">
                    <columns>
                           
                            <asp:BoundField DataField="Username" HeaderText="User name" />
                              <asp:BoundField DataField="LoginTime" HeaderText="Login Time" />
                              <asp:BoundField DataField="NoofPAtient" HeaderText=" No of Patient" />
                               <asp:BoundField DataField="BillIssue" HeaderText=" Bill Issue" />  
                            
                            <asp:BoundField DataField="NetAmount" HeaderText="Net Amount" />
                            
                             <asp:BoundField DataField="NetCashCollection" HeaderText="Net Cash Collection" />
                              <asp:BoundField DataField="saleReturnNo" HeaderText=" sale Return No" />
                              <asp:BoundField DataField="SaleReturnAmount" HeaderText="Sale Return Amount" />
                              
                              
                        </columns>
                       

                </asp:gridview>
            </div>
                    <!--  <table id="Table1" width="100% " runat="server">
                <tr>
                <td>
               Amount:
                </td>
                 <td>
               <asp:label id="LblAmount" runat="server" Text="0"></asp:label>
                </td>
                 <td>
                Discount:
                </td>
                 <td>
                  <asp:label id="Lbldiscount" runat="server" Text=""></asp:label>
                </td>
                 <td>
               Taxable:
                </td>
                  <td>
                    <asp:label id="Lbltaxable" runat="server" Text=""></asp:label>
                </td>
                 <td>
               Tax on Taxable:
                </td>
                 <td>
                    <asp:label id="Lbltaxontaxable" runat="server" Text=""></asp:label>
                </td>
                 <td>
               Net Amount:

                </td>
                 <td>
                    <asp:label id="Lblnetamount" runat="server" Text=""></asp:label>
                </td>

                </tr>
                </table> -->
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