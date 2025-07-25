 <%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Hospital.master" CodeFile="Dept_salereport.aspx.cs" Inherits="Dept_salereport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Dept sale Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Dept sale Report</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
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
                                       
                                
                <asp:dropdownlist id="ddldeptname" runat="server" class="form-control">
                </asp:dropdownlist>
                                    </div>
                                </div>
                               
                                       <div class="col-sm-3">
                                                                
                    
 <asp:button id="btnlist" runat="server" OnClientClick="return validate();" CssClass="btn btn-info" onclick="btnlist_Click" text="Click"   />
 <asp:button id="btnreport1" OnClientClick="return validate();" runat="server"  onclick="btnreport1_Click"  tooltip="Sale Report" Text="Sale  Report" CssClass="btn btn-primary" />
                              
                                            
                            </div>
                        </div>
                      
                    </div>
                    <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
     <asp:gridview id="GV_SaleDept" runat="server" class="table table-responsive table-sm table-bordered" width="100%" onrowdatabound="GV_SaleDept_RowDataBound"
                    onrowediting="GV_SaleDept_RowEditing"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False" PageSize="25"
                    allowpaging="True" onpageindexchanging="GV_SaleDept_PageIndexChanging">
                    <columns>
                           
                            <asp:BoundField DataField="Patregdate" HeaderText="Date" />
                              <asp:BoundField DataField="subdeptName" HeaderText="Dept Name" />
                         <asp:BoundField DataField="DepCount" HeaderText="Dept Count" />
                               <asp:BoundField DataField="TestRate" HeaderText=" Amount" />
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

