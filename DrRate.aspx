<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="DrRate.aspx.cs" Inherits="DrRate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Dr Rate</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Dr Rate</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                        <asp:DropDownList ID="ddlRate" runat="server" CssClass="form-control form-select" TabIndex="1">
                    </asp:DropDownList>
                   
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlRate"
                        Display="Dynamic" ErrorMessage="This is required field." InitialValue="Select Rate Type"
                        SetFocusOnError="True" Style="position: relative" ForeColor="Red" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                               
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                       <asp:DropDownList ID="DdlTestname" runat="server"  CssClass="form-control form-select"  TabIndex="2">
                    </asp:DropDownList>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DdlTestname"
                        Display="Dynamic" ErrorMessage="This is required field." InitialValue="Select sub dept"
                        SetFocusOnError="True" Style="position: relative" ForeColor="Red" Font-Bold="True"></asp:RequiredFieldValidator>
                                      
                                    </div>
                               </div>
                                            <div class="col-sm-6">
                                                           <asp:Button ID="Btndsplay" runat="server"
                        Width="52px" OnClick="Btndsplay_Click" CssClass="btn btn-primary" Text="Click"  ToolTip="Display Rate" TabIndex="3" />
                                        <asp:Label ID="lblmsg" runat="server" Text="" Font-Bold="true" ForeColor="red"  >  </asp:Label>
                    
                                            </div>
                                               
                            </div>
                        </div>
                      
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
        <asp:GridView ID="GridRate" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False" 
                        Width="100%"  PageSize="15"    HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   OnPageIndexChanging="GridRate_PageIndexChanging" OnRowDeleting="GridRate_RowDeleting" TabIndex="4">
                    <Columns>
                    <asp:BoundField HeaderText="ST CODE" DataField="STCODE" />
                    <asp:BoundField HeaderText="Test Name" DataField="TestName" />
                    <asp:TemplateField HeaderText="Amount">
                    <ItemTemplate>
                    <asp:TextBox ID="txtamount" runat="server"   Text='<%#Bind("Amount")%>'></asp:TextBox>
                    </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Percentage (%)">
                         <ItemTemplate>
                    <asp:TextBox ID="txtPerc" runat="server"   Text='<%#Bind("Percentage")%>'></asp:TextBox>
                    </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images0/delete.gif" HeaderText="Clear" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                    </Columns>
                   

                    </asp:GridView>
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                     <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    
                                     <asp:Button ID="btnsave" runat="server"  CssClass="btn btn-success"  OnClientClick="return Validate();" Text="Save"
                        OnClick="btnSave_Click"  />
                                    
                                   
                                </div>
                            </div>
                        </div>
                </section>
                <!-- /.content -->
          
</asp:Content>

