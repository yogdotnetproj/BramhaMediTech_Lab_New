<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="MachinMapCode.aspx.cs" Inherits="MachinMapCode" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:ScriptManager id="ScriptManager2" runat="server">
        </asp:ScriptManager>
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Machin Mapcode</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Machin Mapcode</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                            <div class="col-sm-3">
                                    <div class="form-group">  
                                       <asp:DropDownList ID="ddlMachinName"  CssClass="form-control form-select" runat="server" 
                                            AutoPostBack="True" onselectedindexchanged="ddlMachinName_SelectedIndexChanged"></asp:DropDownList> 
                                    </div>
                                </div>
                                </div>
                                <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">   
                                       
                <asp:TextBox ID="txttestname" runat="server" CssClass="form-control"  placeholder="Test Name" TabIndex="1" 
                                            AutoPostBack="True" ontextchanged="txttestname_TextChanged"></asp:TextBox>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" TargetControlID="txttestname" ServiceMethod="FillTestName" CompletionListElementID="div1"></cc1:AutoCompleteExtender>
<div id="div1" style="width: 250px; overflow: scroll;display :none ; height: 250px;"></div>
                                    </div>
                                </div>
                               

                                 <div class="col-sm-3 text-center">
                                    <div class="form-group pt25">
                                                                        </div>
                                    </div>

                            </div>

                             <div class="row mb-3">
                              <div class="col-sm-3">
                                    <div class="form-group">
                                  
                                
                                  <asp:DropDownList ID="txttestparameter" placeholder="Enter Test Parameter" CssClass="form-control form-select" runat="server"></asp:DropDownList>
                                   </div>
                                   </div>
                             </div>
                              <div class="row mb-3">
                              <div class="col-sm-3">
                                    <div class="form-group">
                                
                                   <asp:TextBox ID="txtMachintestparameter" runat="server" CssClass="form-control"  placeholder="Machin Test Parameter" TabIndex="3"></asp:TextBox>
                                   </div>
                                   </div>
                             </div>

                              <div class="row mb-3">
                              <div class="col-sm-3">
                                    <div class="form-group">
                                   <label></label>
                                  <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" CssClass="btn btn-primary" Text="Click"  TabIndex="2" />
                                       
                                         <asp:Button ID="btnsave" runat="server" OnClick="Button1_Click"  Text="Save"  ValidationGroup="form" CssClass="btn btn-success" TabIndex="3" />
                                    
                                     <asp:Label ID="Label2" runat="server" Font-Bold="true"  ForeColor="Red" Style="position: relative" Text="" ></asp:Label>

                                   </div>
                                   </div>
                             </div>
                        </div>
                    </div>
                    <div class="box">
                     <div class="table-responsive" style="width:592px">
                <asp:GridView ID="GV_UserType" runat="server" class="table table-responsive table-sm " AutoGenerateColumns="False" DataKeyNames="Srno"
                    Width="592px" OnPageIndexChanging="GV_UserType_PageIndexChanging" OnRowEditing="GV_UserType_RowEditing"
                    AllowPaging="True" PageSize="15" OnRowDeleting="GV_UserType_RowDeleting"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   >
                    <Columns>
                        <asp:BoundField DataField="Instname" HeaderText="Machin Name" />
                         <asp:BoundField DataField="Maintestname" HeaderText="MainTest Name" />
                          <asp:BoundField DataField="TestName" HeaderText="Test Name" />
                          <asp:BoundField DataField="Mapcode" HeaderText="Map Code" />
                        <asp:CommandField HeaderText="Edit " Visible="false" ShowEditButton="True" EditImageUrl="~/Images0/edit.gif"
                            ButtonType="Image" />
                            <asp:CommandField HeaderText="Delete " ShowDeleteButton="True" EditImageUrl="~/Images0/delete.gif"
                            ButtonType="Image" />
                    </Columns>
                   

                </asp:GridView>
                </div>
                    </div>
                </section>
                <!-- /.content -->
           
</asp:Content>

