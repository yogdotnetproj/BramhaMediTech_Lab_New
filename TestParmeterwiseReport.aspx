 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="TestParmeterwiseReport.aspx.cs" Inherits="TestParmeterwiseReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:ScriptManager id="ScriptManager2" runat="server">
        </asp:ScriptManager>
      
            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Test Parameterwise Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Test Parameterwise Report</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                          
                                </div>
                                <div class="row mb-3">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                        
                                        
                                       
                <asp:TextBox ID="txttestname" runat="server" class="form-control"  placeholder="Test Name" TabIndex="1" 
                                            AutoPostBack="True" ontextchanged="txttestname_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" TargetControlID="txttestname" ServiceMethod="FillTestName" CompletionListElementID="div1"></cc1:AutoCompleteExtender>
<div id="div1" style="width: 250px; overflow: scroll;display :none ; height: 250px;"></div>
                                    </div>
                                </div>
                               
                                <div class="col-sm-6">
                                    <div class="form-group">
                                           <asp:Button ID="btnsave" runat="server" OnClick="Button1_Click"  Text="Report"  ValidationGroup="form" CssClass="btn btn-primary" TabIndex="3" />
                                    
                                     <asp:Label ID="Label2" runat="server" Font-Bold="true"  ForeColor="Red" Style="position: relative" Text="" ></asp:Label>

                                    </div>
                                    </div>
                                 

                            </div>

                            
                              

                           
                        </div>
                    </div>
                    
                </section>
                <!-- /.content -->
           
          
    
   </asp:Content>