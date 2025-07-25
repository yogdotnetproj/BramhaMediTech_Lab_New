<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="MachinMaster.aspx.cs" Inherits="MachinMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server"> 

                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Machin Master</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Add Machine</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                    <asp:TextBox ID="txtMachinCode"  runat="server" placeholder="Enter Machin Code" TabIndex="1" CssClass="form-control" 
                                        ></asp:TextBox>
                                    
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group">
                    <asp:TextBox ID="txtMachinename" placeholder="Enter Machin Name"  runat="server" TabIndex="2" CssClass="form-control" 
                                        ></asp:TextBox>
                                   
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
                      <div class="table-responsive" style="width:592px">
                 <asp:GridView ID="RoutinTest_Grid" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False" DataKeyNames="ID"
                    Width="592px" OnPageIndexChanging="RoutinTest_Grid_PageIndexChanging" OnRowEditing="RoutinTest_Grid_RowEditing"
                    AllowPaging="True" PageSize="100" OnRowDeleting="RoutinTest_Grid_RowDeleting"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   >
                    <Columns>
                        <asp:BoundField DataField="Instumentcode" HeaderText="Machin Code" />
                          <asp:BoundField DataField="Instumentname" HeaderText="Machin Name" />
                        <asp:CommandField HeaderText="Edit " ShowEditButton="True" EditImageUrl="~/Images0/edit.gif"
                            ButtonType="Image" />
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images0/delete.gif" />
                    </Columns>
                   

                </asp:GridView>
                </div>
                    </div>
                     <div class="box">
                     
                     </div>
                </section>
                <!-- /.content -->
           

</asp:Content>

