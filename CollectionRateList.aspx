 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="CollectionRateList.aspx.cs" Inherits="CollectionRateList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
     
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Collection Rate List</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Collection Rate List</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">                           
                               
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        
                                       <asp:dropdownlist id="ddlCenter" runat="server" CssClass="form-control form-select" tabindex="3">
                </asp:dropdownlist>

                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        
                                         <asp:button id="btnList" runat="server" OnClientClick="return validate();" text="Click" onclick="btnList_Click1" CssClass="btn btn-secondary" />
                                             <asp:button id="btnreport" runat="server" OnClientClick="return validate();" 
                                                       text="Report" CssClass="btn btn-info" onclick="btnreport_Click" />
                                                       <asp:button id="btnpdfreport" runat="server" OnClientClick="return validate();" 
                                                       text="PDF Report" CssClass="btn btn-primary" onclick="btnpdfreport_Click"  />
                                         </div>
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
                     <asp:BoundField DataField="RateName" HeaderText="Rate List Name" />
                     <asp:BoundField DataField="STCODE" HeaderText="Test Code" />
                     <asp:BoundField DataField="Maintestname" HeaderText="Test Name" />
                      <asp:BoundField DataField="Sampletype" HeaderText="Sample type" />
                      <asp:BoundField DataField="Amount" HeaderText="Amount" />
                      
                    
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
               


              
   