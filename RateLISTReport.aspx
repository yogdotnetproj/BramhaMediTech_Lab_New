<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="RateLISTReport.aspx.cs" Inherits="RateLISTReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:ScriptManager id="ScriptManager2" runat="server">
        </asp:ScriptManager>
<section class="content-header">
                    <h1>Rate Type List</h1>
                    <ol class="breadcrumb">
                  
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Rate Type List</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                          
                                </div>
                                <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        
                                        
                                       
                <asp:TextBox ID="txtRateType" runat="server" class="form-control"  placeholder="Rate Type Name" TabIndex="1" 
                                            AutoPostBack="True" ontextchanged="txtRateType_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" TargetControlID="txtRateType" ServiceMethod="GetRateType" CompletionListElementID="div1"></cc1:AutoCompleteExtender>
<div id="div1" style="width: 250px; overflow: scroll;display :none ; height: 250px;"></div>
                                    </div>
                                </div>
                               
                                <div class="col-lg-6">
                                    <div class="form-group">
                                           
                                         <asp:Button ID="btnsave" runat="server" OnClick="Button1_Click"  Text="Report"  ValidationGroup="form" class="btn btn-primary" TabIndex="3" />
                                    
                                     <asp:Label ID="Label2" runat="server" Font-Bold="true"  ForeColor="Red" Style="position: relative" Text="" ></asp:Label>

                                    </div>
                                    </div>
                                 

                            </div>

                            
                              

                        </div>
                    </div>
                    
                </section>
</asp:Content>

