<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="NewPrintreport.aspx.cs" Inherits="NewPrintreport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
 
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Printing Report</h1>
                    <ol class="breadcrumb">
                  
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Printing Report </li>
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
                                        
                                      
                                        
                                        <asp:TextBox id="txtPatientName"  placeholder="Enter Patient Name"
                              runat="server" class="form-control pull-right" AutoPostBack="false"></asp:TextBox>
                              
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        
                                        <asp:TextBox ID="txtRegNo" runat="server" placeholder="Enter Reg No" class="form-control pull-right"></asp:TextBox>
                                    </div>
                                </div>
                                </div>
                            <div class="row">
                                
                                 
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        <asp:TextBox ID="TextBox1" runat="server" placeholder="." class="form-control pull-right"></asp:TextBox>
                                         <asp:DropDownList ID="ddlfyear" runat="server" Visible="false"  class="form-control">
                                </asp:DropDownList>
                                    </div>
                                </div> 
                               
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                       
                                         <asp:TextBox ID="txtbarcode" placeholder="Enter Barcode"  class="form-control pull-right" runat="server"></asp:TextBox>
                                    </div>
                                </div> 
                                <div class="col-lg-3">
                                    <div class="form-group">
                                                                              
                                                             
<asp:TextBox id="txtmobileno"  runat="server" placeholder="Enter Mobile No"   class="form-control pull-right"></asp:TextBox>
                                    </div>
                                </div> 
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                       
                                         <asp:TextBox id="ddlCenter" class="form-control pull-right" placeholder="Select Center" runat="server"  OnTextChanged="ddlCenter_TextChanged"
 AutoPostBack="True"></asp:TextBox>
 <div style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 200px; HEIGHT: 100px" id="Div2"></DIV>
 
 <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" CompletionListElementID="Div2"
  ServiceMethod="GetCenter" TargetControlID="ddlCenter" MinimumPrefixLength="1">
                               </cc1:AutoCompleteExtender> 
                                    </div>
                                </div>
                                </div>
                            <div class="row" style="margin-top:10px">
                                 
                                                                 <div class="col-lg-3">
                                    <div class="form-group">
                                    <asp:RadioButtonList ID="RblRepStatus" runat="server" RepeatDirection="Horizontal" 
                                            AutoPostBack="True" onselectedindexchanged="RblRepStatus_SelectedIndexChanged">
    <asp:ListItem>All</asp:ListItem>
    <asp:ListItem Selected="True">Completed</asp:ListItem>
    <asp:ListItem  >Pending</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    </div>
                                <div class="col-md-3">
                                      <asp:Button ID="btnList" runat="server" class="btn btn-primary" OnClientClick="return validate();" OnClick="btnList_Click"  Text="Click"
                                     />
                                </div>
                            </div>
                        </div>
                       
                    </div>
                      <div class="col-lg-3" style="display:none">
                                    <div class="form-group">
                                       
                                       <!-- <select class="form-control">
                                            <option>All</option>
                                            <option>Another Center</option>
                                            <option>Other Center</option>
                                        </select>-->
                                         <asp:TextBox id="txtCenter" placeholder=""   runat="server" class="form-control pull-right"></asp:TextBox>
                                    </div>
                                </div>
                    <div class="box">
                        <div class="box-body">
                       <div class="table-responsive" style="width:100%">
                             <asp:GridView ID="gridreport" 
                                 class="table table-responsive table-sm table-bordered" runat="server" 
                                 AutoGenerateColumns="False" OnRowDataBound="gridreport_RowDataBound"
                                    PageSize="50"    HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   Width="100%" OnPageIndexChanging="gridreport_PageIndexChanging" 
                                 onrowediting="gridreport_RowEditing">
                                    <Columns>
                                        <asp:BoundField DataField="PatRegID" HeaderText="RegNo" SortExpression="PatRegID" />
                                        <asp:BoundField DataField="NAME" HeaderText=" Name" ReadOnly="True" SortExpression="NAME" />
                                        <asp:BoundField DataField="TelNo" HeaderText="Mobile No" SortExpression="TelNo" />
                                        <asp:BoundField DataField="sex" HeaderText="Gender" SortExpression="Sex" />
                                        <asp:BoundField DataField="Age" HeaderText="Age" SortExpression="Age" />
                                        <asp:BoundField DataField="MDY" HeaderText="MDY" SortExpression="Mdy" />
                                        <asp:BoundField DataField="Drname" HeaderText="Ref.Doc" SortExpression="Drname" />
                                        <asp:BoundField DataField="CenterName" HeaderText="Center " SortExpression="CenterName" />
                                        <asp:BoundField DataField="Maintestname" HeaderText="Test Name" SortExpression="Maintestname">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                       
                                        <asp:BoundField DataField="Patauthicante" HeaderText="Status" SortExpression="Patauthicante" />
                                        <asp:CommandField EditText="Report" HeaderText="Report"  ShowEditButton="True" />
                                         <asp:BoundField DataField="testcharges" HeaderText="Charges" Visible="false" SortExpression="testcharges">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AmtReceived" HeaderText="Paid " Visible="false" SortExpression="AmtReceived">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="Discount" HeaderText="Discount " Visible="false" SortExpression="Discount">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="OutStAmt" HeaderText="Balance" Visible="false" SortExpression="OutStAmt">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LabRegMediPro" HeaderText="Mno" Visible="false" SortExpression="LabRegMediPro">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Printedby" HeaderText="Printed by" SortExpression="Printedby">
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        
                                         <asp:BoundField DataField="CreatedBy" HeaderText=" Created By" />
                                        <asp:BoundField DataField="TelNo" HeaderText=" Mobile No" Visible="false" />          
                                         <asp:BoundField DataField="Pataddress" HeaderText="Address" Visible="false" />         
                                                 
                                       
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                 <asp:HiddenField ID="hdnOutStAmt" runat="server" Value='<%#Eval("OutStAmt") %>' />
                                                <asp:HiddenField ID="hdntestcharges" runat="server" Value='<%#Eval("testcharges") %>' />
                                                <asp:HiddenField ID="Hdn_PatientEmail" runat="server" Value='<%#Eval("PatientEmail") %>' />
                                                  <asp:HiddenField ID="Hdn_printstatus" runat="server" Value='<%#Eval("Patrepstatus") %>' />
                                                   <asp:HiddenField ID="hdnRegNo" runat="server" Value='<%#Eval("PatRegID") %>' /> 
                                                   <asp:HiddenField ID="HdnFD" runat="server" Value='<%#Eval("FID") %>' />
                                                 <asp:HiddenField ID="HdnMonthlybill" runat="server" Value='<%#Eval("Monthlybill") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                   

                                </asp:GridView>
                                </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
           
</asp:Content>

