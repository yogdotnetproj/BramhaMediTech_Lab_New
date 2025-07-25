<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="RoutinTest.aspx.cs" Inherits="RoutinTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Routin Test</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Routin Test</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">

                    <asp:TextBox ID="txtRoutinTest"  runat="server" placeholder="Enter Test Name" TabIndex="9" CssClass="form-control" 
                                        ></asp:TextBox>
                                    <div style="display: none; overflow: scroll; width: 348px; height: 120px" id="div">
                                    </div>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                        CompletionListElementID="div" ServiceMethod="FillTests" TargetControlID="txtRoutinTest"
                                        MinimumPrefixLength="1">
                                    </cc1:AutoCompleteExtender>
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
                 <asp:GridView ID="RoutinTest_Grid" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
                    Width="592px" OnPageIndexChanging="RoutinTest_Grid_PageIndexChanging" OnRowEditing="RoutinTest_Grid_RowEditing"
                    AllowPaging="True" PageSize="100" OnRowDeleting="RoutinTest_Grid_RowDeleting"   HeaderStyle-ForeColor="Black"  >
                      <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="RoutinTestCode" HeaderText="Test Code" />
                          <asp:BoundField DataField="RoutinTestName" HeaderText="Routin Test" />
                        <asp:CommandField HeaderText="Edit " ShowEditButton="True" 
                            ButtonType="Image" EditImageUrl="~/Images0/edit.gif" />
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" DeleteImageUrl="~/Images0/delete.gif" ButtonType="Image" />
                    </Columns>
                   

                </asp:GridView>
                </div>
                    </div>
                     <div class="box">
                     
                     </div>
                </section>
                <!-- /.content -->
            
</asp:Content>

