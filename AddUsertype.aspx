<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="AddUsertype.aspx.cs" Inherits="AddUsertype" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

          
            <!-- Left side column. contains the logo and sidebar -->
             <section class="d-flex justify-content-between content-header mb-2">
                    <h4>User Type</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">User Type</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
            
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group d-flex">
                                       
                <asp:TextBox ID="txtuserType" runat="server" placeholder="Enter User Type" class="form-control" TabIndex="1"></asp:TextBox><span style="color:Red;">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtuserType"
                    Display="Dynamic" ErrorMessage="This Field is required." ForeColor="Red"  SetFocusOnError="True"
                    ValidationGroup="form" Width="152px" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-3 text-center">
                                    <div class="form-group pt25">
                                    
                                         <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" CssClass="btn btn-primary" Text="Click"  TabIndex="2" />
                                       
                                         <asp:Button ID="btnsave" runat="server" OnClick="Button1_Click"  Text="Save"  ValidationGroup="form" CssClass="btn btn-success" TabIndex="3" />
                                    
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
                    <div class="box">
                     <div class="table-responsive" style="width:100%">
                <asp:GridView ID="GV_UserType" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False" DataKeyNames="ROLLID"
                    Width="592px" OnPageIndexChanging="GV_UserType_PageIndexChanging" OnRowEditing="GV_UserType_RowEditing"
                    AllowPaging="True" PageSize="15" OnRowDeleting="GV_UserType_RowDeleting"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   >
                    <Columns>
                        <asp:BoundField DataField="ROLENAME" HeaderText="User Type" />
                        <asp:CommandField HeaderText="Edit " ShowEditButton="True" EditImageUrl="~/Images0/edit.gif"
                            ButtonType="Image" />
                    </Columns>
                   

                </asp:GridView>
                </div>
                    </div>
               
               </section>
            <!-- /.content-wrapper -->
            
</asp:Content>

