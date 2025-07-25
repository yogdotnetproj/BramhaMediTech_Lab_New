 <%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Hospital.master" CodeFile="TestwiseDailyCount.aspx.cs" Inherits="TestwiseDailyCount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
     
            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Testwise Daily Count</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Testwise Daily Count </li>
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
                                       <asp:TextBox id="txtdoctor" tabIndex=3 runat="server" placeholder="Enter Test" class="form-control"></asp:TextBox>
                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" CompletionListElementID="div" TargetControlID="txtdoctor" ServiceMethod="FillTest" MinimumPrefixLength="1"></cc1:AutoCompleteExtender>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 250px; HEIGHT: 250px" id="div"></DIV>

                                    </div>
                                </div>
                                <div class="col-md-2">
                                                  <asp:RadioButtonList ID="RblTestCount" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Test</asp:ListItem>
                                <asp:ListItem Value="2">Package</asp:ListItem>
                                    </asp:RadioButtonList>
                    
                                </div>
                                <div class="col-md-1">
                                                 <asp:Button ID="btnreport" runat="server" 
                                onclientclick=" return validate();" Text="Report"  class="btn btn-primary" 
                                        onclick="btnreport_Click" />
                                </div>
                               
                                           
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
