 <%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Hospital.master" CodeFile="Servicewisesalereport.aspx.cs" Inherits="Servicewisesalereport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Testwise Sale report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Testwise Sale report</li> 
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
                                        
                                      
                 <asp:textbox id="txtregno" runat="server" placeholder="Enter Reg No" class="form-control" tabindex="3">
                </asp:textbox>
                                    </div>
                                </div>
                               </div>
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                     
                                      
                  <asp:TextBox id="txtCenter" tabIndex="6" placeholder="Enter Center" runat="server" class="form-control" ></asp:TextBox>
                <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 185px; HEIGHT: 100px" id="Div1"></DIV>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="Div2" ServiceMethod="Getcenter" TargetControlID="txtCenter" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                               <div class="col-sm-4">
                                    <div class="form-group">
                                       
                 
                <asp:dropdownlist id="ddlfyear" runat="server" class="form-control" tabindex="5">
                </asp:dropdownlist>
                                    </div>
                                </div>

                                <div class="col-sm-4">
                                    <div class="form-group">
                                       <asp:TextBox id="txttestname" tabIndex="6" placeholder="Enter Test" runat="server" class="form-control" ></asp:TextBox>
                <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 185px; HEIGHT: 100px" id="Div5"></DIV>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="Div5" ServiceMethod="GetTest" TargetControlID="txttestname" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                        
                                    </div>
                                </div>

                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row mb-3">
                                <div class="col-lg-12 text-center">
                                    <asp:button id="Button1" OnClientClick="return validate();" runat="server" text="Click" CssClass="btn btn-info" onclick="btnshow_Click" 
                    tabindex="6" />
                     <asp:Button ID="btnreport" runat="server" CssClass="btn btn-primary" onclick="btnreport_Click" Text="Report" />
                                </div> 
                            </div>
                        </div>

                    </div>
                    <div class="box">
                        <div class="box-body">
                       <div class="table-responsive" style="width:100%">
                              <asp:gridview id="GVBillFHosp" class="table table-responsive table-sm table-bordered" runat="server" width="100%" autogeneratecolumns="False"
                    allowsorting="True"  allowpaging="True" onpageindexchanging="GVBillFHosp_PageIndexChanging"
                    pagesize="25"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"    
                        OnRowDataBound="GVBillFHosp_RowDataBound" >
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                   

                    <columns>
                   
                     <asp:BoundField DataField="Maintestname" HeaderText="Service Name">
                                
                            </asp:BoundField>
                             <asp:BoundField DataField="TestCode" HeaderText="Service Count" />
                            
                          
                             <asp:BoundField DataField="amount" HeaderText="Amount"></asp:BoundField>
                           
                            
                         
                           
                             <asp:BoundField DataField="Discount" Visible="false" HeaderText="Discount"></asp:BoundField>
                          
                      <asp:BoundField DataField="Taxable" Visible="false" HeaderText=" Taxable"></asp:BoundField>
                       <asp:BoundField DataField="Tax" Visible="false" HeaderText=" Tax on Taxable"></asp:BoundField>
                             
                             <asp:BoundField DataField="Net" Visible="false" HeaderText=" Net Amount"></asp:BoundField>
                               
                        </columns>
                        


                        

<HeaderStyle ForeColor="Black"></HeaderStyle>
                        


                        

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
