 <%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Hospital.master" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Change Password</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Change Password</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group d-flex">                                 
                                        
                                       
                <asp:TextBox ID="txtuserType" runat="server" placeholder="Enter User Name" Enabled="false" CssClass="form-control" TabIndex="1"></asp:TextBox><span style="color:Red;">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtuserType"
                    Display="Dynamic" ErrorMessage="This Field is required." ForeColor="Red"  SetFocusOnError="True"
                    ValidationGroup="form" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group d-flex">
                                                                               
                                       
                <asp:TextBox ID="txtoldpassword" runat="server" placeholder="Enter Old Password" CssClass="form-control" TextMode="Password" TabIndex="2"></asp:TextBox><span style="color:Red;">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtoldpassword"
                    Display="Dynamic" ErrorMessage="This Field is required." ForeColor="Red"  SetFocusOnError="True"
                    ValidationGroup="form" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group d-flex">
                                                                             
                                       
                <asp:TextBox ID="txtnewpassword" runat="server" class="form-control" placeholder="Enter New Password" TextMode="Password" TabIndex="3"></asp:TextBox><span style="color:Red;">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtnewpassword"
                    Display="Dynamic" ErrorMessage="This Field is required." ForeColor="Red"  SetFocusOnError="True"
                    ValidationGroup="form" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                                <div class="col-sm-1">
                                    <div class="form-group pt25">
                                                                              
                                         <asp:Button ID="btnsave" runat="server" OnClick="Button1_Click"  Text="Ok"  ValidationGroup="form" CssClass="btn btn-success" TabIndex="3" />
                                    
                                    </div>
                                </div>

                                 <div class="col-sm-3 text-center">
                                    <div class="form-group pt25">
                                     <asp:Label ID="Label2" runat="server" Font-Bold="true"  ForeColor="Red" Style="position: relative" Text="Label" ></asp:Label>
                                    </div>
                                    </div>

                            </div>
                        </div>
                    </div>
                    
                </section>
                <!-- /.content -->
        
            <!-- /.content-wrapper -->
            
               
    </asp:Content>
