 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="DrReport_Summary_Temp.aspx.cs" Inherits="DrReport_Summary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
   
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>DR Report summary</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">DR Report summary </li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3">
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
                                <div class="col-lg-3">
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
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        
                                       <!-- <select class="form-control">
                                            <option>All</option>
                                            <option>Another Center</option>
                                            <option>Other Center</option>
                                        </select>-->
                                       <asp:TextBox id="txtdoctor" tabIndex=3 runat="server" placeholder="Enter Doctor" class="form-control"></asp:TextBox>
                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" CompletionListElementID="div" TargetControlID="txtdoctor" ServiceMethod="FillDoctor" MinimumPrefixLength="1"></cc1:AutoCompleteExtender>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 250px; HEIGHT: 250px" id="div"></DIV>

                                    </div>
                                </div>
                               
                                             <div class="col-md-3" style="padding-left:0px">
                                                  <asp:Button ID="btnlist" runat="server" 
                                onclientclick=" return validate();" Text="Click"  class="btn btn-primary"
                            onclick="btnlist_Click"  />
                    
                                   <asp:Button ID="btnPdfReport" runat="server" onclick="btnPdfReport_Click" 
                                onclientclick=" return validate();" class="btn btn-primary" Text="PDF Report" ToolTip="PDF REPORT" />
                                <asp:Button ID="btnreport" runat="server" Text="Excel Report" 
                                OnClientClick=" return validate();" class="btn btn-primary" OnClick="btnreport_Click" TabIndex="4" 
                                ToolTip="EXCEL REPORT" />
                                             </div>
                            </div>
                        </div>
                   
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
           <asp:gridview id="GV_Drcomp" runat="server" class="table table-responsive table-sm table-bordered" onrowediting="GV_Drcomp_RowEditing" width="100%"
                    onrowdatabound="GV_Drcomp_RowDataBound"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False"
                    onpageindexchanging="GV_Drcomp_PageIndexChanging">
                    <columns>
                        
                       
                           <asp:BoundField DataField="Drname" HeaderText=" Dr.Name" />
                           
                          
                            
                           <asp:BoundField DataField="TotalAmount" HeaderText="Charges" />
                            <asp:BoundField DataField="Discount" HeaderText="Discount" />
                          <asp:BoundField DataField="Drcompamt" HeaderText="Comp Amt" />
                          
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