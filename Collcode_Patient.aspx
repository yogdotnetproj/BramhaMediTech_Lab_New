 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Collcode_Patient.aspx.cs" Inherits="Collcode_Patient" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
            
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Sale Report</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Sale report</li>
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
                                <div class="col-lg-4">
                                    <div class="form-group">
                                      
                                      
                 <asp:textbox id="txtregno" runat="server" placeholder="Enter Reg No" class="form-control" tabindex="3">
                </asp:textbox>
                                    </div>
                                </div>
                               
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                      
                  <asp:TextBox id="txtCenter" tabIndex="6" placeholder="Enter Center" runat="server" class="form-control" ></asp:TextBox>
                <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 185px; HEIGHT: 100px" id="Div1"></DIV>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="Div2" ServiceMethod="Getcenter" TargetControlID="txtCenter" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                               <div class="col-lg-4">
                                    <div class="form-group">
                                       
                 
                <asp:dropdownlist id="ddlfyear" runat="server" class="form-control" tabindex="5">
                </asp:dropdownlist>
                                    </div>
                                </div>

                                <div class="col-lg-4">
                                    <div class="form-group">
                                     <label></label>
                                        
                                    </div>
                                </div>

                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    <asp:button id="Button1" OnClientClick="return validate();" runat="server" text="Click" class="btn btn-primary" onclick="btnshow_Click" 
                    tabindex="6" />
                     <asp:Button ID="btnreport" runat="server" class="btn btn-primary" onclick="btnreport_Click" 
                    Text="Report" />
                                </div> 
                            </div>
                        </div>

                    </div>
                    <div class="box">
                        <div class="box-body">
                        
 <div class="table-responsive" style="width:100%">
                              <asp:gridview id="GVBillFHosp" class="table table-responsive table-sm table-bordered" datakeynames="PID" runat="server" width="100%" autogeneratecolumns="False"
                    allowsorting="True"  allowpaging="True" onpageindexchanging="GVBillFHosp_PageIndexChanging"
                    pagesize="25"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"    
                        OnRowDataBound="GVBillFHosp_RowDataBound" >
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                   

                    <columns>
                           <asp:BoundField DataField="Patregdate" HeaderText="Date" />
                            <asp:BoundField DataField="RegNo" HeaderText="Reg No" />
                             <asp:BoundField DataField="BillNo" HeaderText="BillNo"></asp:BoundField>
                            <asp:BoundField DataField="Patname" HeaderText="Name">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="CenterName" HeaderText="Center " />
                            <asp:TemplateField HeaderText="Test ">
                                <ItemTemplate>
                                    <asp:Label ID="lbltestname" Text='<%#Bind("Testname")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                             <asp:BoundField DataField="TestCharges" HeaderText=" Charges"></asp:BoundField>
                          
                      
                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdntest" runat="server" Value='<%#Bind("Tests")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnCollcode" runat="server" Value='<%#Bind("CenterCode")%>' />
                                     <asp:HiddenField ID="hdn_PID" runat="server" Value='<%#Bind("PID")%>' />
                                      <asp:HiddenField ID="Hdn_BillNo" runat="server" Value='<%#Bind("BillNo")%>' />
                                      <asp:HiddenField ID="Hdn_FID" runat="server" Value='<%#Bind("FID")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                        </columns>
                        


                        

                </asp:gridview>
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
           
            
         <script language="javascript" type="text/javascript">
             function OpenReport() {
                 window.open("Reports.aspx");
             } 
               </script>
              
     </asp:Content>