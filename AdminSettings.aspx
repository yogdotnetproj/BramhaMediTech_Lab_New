 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminSettings.aspx.cs" MasterPageFile="~/Hospital.master" Inherits="AdminSettings" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Admin Settings</h1>
                    <ol class="breadcrumb">
                        
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Admin Settings</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-header with-border">
                            <span class="red pull-right">Fields marked with * are compulsory</span> 
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                      
                                      <asp:Label ID="Label1" runat="server" Text="Is Barcode/Register Interface"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                <asp:RadioButtonList ID="RblBarReg" runat="server" RepeatDirection="Horizontal" 
                                                    AutoPostBack="True" onselectedindexchanged="RblBarReg_SelectedIndexChanged">
                                                    <asp:ListItem Value="BarCode">Barcode</asp:ListItem>
                                                    <asp:ListItem Value="RegNo">Reg No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                              <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                      
                                      <asp:Label ID="Label2" runat="server" Text="Is Phlebotomist Not Required"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                <asp:RadioButtonList ID="isPhlebotomistReq" runat="server" 
                                                    RepeatDirection="Horizontal" AutoPostBack="True" 
                                                    onselectedindexchanged="isPhlebotomistReq_SelectedIndexChanged">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                      
                                      <asp:Label ID="Label3" runat="server" Text="Is Receipt Mail"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-8">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                <asp:RadioButtonList ID="IsReceiptMail" runat="server" 
                                                    RepeatDirection="Horizontal" AutoPostBack="True" 
                                                    onselectedindexchanged="IsReceiptMail_SelectedIndexChanged">
                                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        
                                      
                                      <asp:Label ID="Label4" runat="server" ForeColor="Black" Font-Bold="true" Text="Lab Email Details"></asp:Label>
                                    </div>
                                </div>
                                
                            </div>

                             <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group">                                       
                                      
                                      <asp:textbox id="txtLabEmailID" runat="server" placeholder="Email ID"  
                   class="form-control" ></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">                                       
                                      
                                      <asp:textbox id="txtLabEmailPassword" runat="server" placeholder="Email Password"  
                   class="form-control" ></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">                                       
                                      
                                      <asp:textbox id="txtLabEmailDisplayName" runat="server" placeholder="Lab Email Display Name"  
                   class="form-control" ></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">                                       
                                      
                                      <asp:textbox id="txtLabSmsName" runat="server" placeholder="Sms Sender Name"  
                   class="form-control" ></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">                                       
                                      
                                      <asp:textbox id="txtPort" runat="server" placeholder="Port"  
                   class="form-control" ></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">                                       
                                      
                                      <asp:textbox id="txtLabWebsite" runat="server" placeholder="Web site"  
                   class="form-control" ></asp:textbox>
                                    </div>
                                </div>
                                
                            </div>

                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        
                                      
                                      <asp:textbox id="txtLabSmsString" runat="server" placeholder="Sms String"  
                   class="form-control" ></asp:textbox>
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                      
                         <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                       <asp:label id="lblnote" runat="server" Font-Bold="true"  width="220px" >
                </asp:label>
                 <asp:button id="btnsave" runat="server" onclick="btnSave_Click"  text="Save" class="btn btn-primary" />

                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
            
           
  </asp:Content>
