 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="CenterwisepaymentReceive.aspx.cs" Inherits="CenterwisepaymentReceive" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:ScriptManager id="ScriptManager2" runat="server">
        </asp:ScriptManager>
        
            <!-- Content Wrapper. Contains page content -->
            
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Center wise Payment Receive</h1>
                    <ol class="breadcrumb">
                  
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Center wise Payment Receive</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                          
                                </div>
                               

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
                                       
                                      
              
                <asp:TextBox ID="txtCenterName" runat="server" class="form-control"  placeholder="Center Name" TabIndex="1" 
                                            AutoPostBack="True" ontextchanged="txtCenterName_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txtCenterName" ServiceMethod="GetCenterName" CompletionListElementID="div1"></cc1:AutoCompleteExtender>
<div id="div1" style="width: 250px; overflow: scroll;display :none ; height: 250px;"></div>
                                    </div>
                                </div>
                            
                              

                              <div class="row">
                              <div class="col-lg-12">
                                    <div class="form-group">
                                   <label></label>
                                        
                                         <asp:Button ID="btnsave" runat="server" OnClick="Button1_Click"  Text="Report"  ValidationGroup="form" class="btn btn-primary" TabIndex="3" />
                                    
                                     <asp:Label ID="Label2" runat="server" Font-Bold="true"  ForeColor="Red" Style="position: relative" Text="" ></asp:Label>

                                   </div>
                                   </div>
                             </div>
                        </div>
                    </div>
                    
                </section>
                <!-- /.content -->
           
            <!-- /.content-wrapper -->
            
       
      </asp:Content>
