<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="TestWiseDoctorPerformReport.aspx.cs" Inherits="TestWiseDoctorPerformReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Testwise Dr perform report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Testwise Dr perform report</li> 
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
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  CssClass="form-control pull-right"  tabindex="1">
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
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  CssClass="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                       <asp:TextBox id="txtdoctor" tabIndex=3 runat="server" placeholder="Enter Test" CssClass="form-control"></asp:TextBox>
                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" CompletionListElementID="div" TargetControlID="txtdoctor" ServiceMethod="FillTest" MinimumPrefixLength="1"></cc1:AutoCompleteExtender>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 250px; HEIGHT: 250px" id="div"></DIV>

                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                       <asp:TextBox id="txtPerformDr" tabIndex=4 runat="server" placeholder="Enter DrName" CssClass="form-control"></asp:TextBox>
                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" CompletionListElementID="div1" TargetControlID="txtPerformDr" ServiceMethod="GetDr" MinimumPrefixLength="1"></cc1:AutoCompleteExtender>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 250px; HEIGHT: 250px" id="div1"></DIV>

                                    </div>
                                </div>
                                           
                            </div>
                        </div>
                         <div class="box-footer">
                            <div class="row mb-3">
                                <div class="col-lg-12 text-center">
                                  
                            
                            <asp:RadioButtonList ID="RblTestCount" CssClass="form-check"  Visible="false" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Test</asp:ListItem>
                                <asp:ListItem Value="2">Package</asp:ListItem>
                                    </asp:RadioButtonList>
                    
                                 
                                </div> 
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Font-Bold="true" Text=""></asp:Label>
                                   <asp:Button ID="btnreport" runat="server" 
                                onclientclick=" return validate();" Text="Report"  class="btn btn-primary" 
                                        onclick="btnreport_Click" />
                            

                    
                                 
                                </div> 
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

