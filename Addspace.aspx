 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Addspace.aspx.cs" Inherits="Addspace" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     
            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Add / Edit Test Parameter Space</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Add / Edit Test Parameter Space</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group">
                                            <span><asp:Label ID="lblTestname" runat="server" placeholder="Enter Test Name" Font-Bold="True" Style="left: 0px; position: relative"
                    Text="Label"></asp:Label></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3" runat="server" visible="false">
                                    <div class="form-group">
                                       
                                        
                                         <asp:TextBox ID="txtTestName" class="form-control" placeholder="Parameter Name" runat="server"></asp:TextBox><span style="color:Red;">*</span>
           <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTestName"
                    Display="Dynamic" ErrorMessage="Name is Required ." ValidationGroup="form" 
                    Width="189px"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        
                                        <asp:TextBox ID="txttestorder" runat="server" class="form-control" placeholder="Add Space No" ></asp:TextBox><span style="color:Red;">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txttestorder"
                    ErrorMessage="Space no is Required ." ValidationGroup="form" Width="214px"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3 text-center pt25">
                                    <div class="form-group">
                                       
                                         <asp:Button ID="btnsave" runat="server" OnClick="Button1_Click" Text="Save" class="btn btn-primary" ValidationGroup="form" />
                                       
                                         <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" class="btn btn-primary" Text="Back" />
                                         <asp:Label ID="Label8" runat="server" ForeColor="#C04000"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
          
            <!-- /.content-wrapper -->
            
       
   </asp:Content>
