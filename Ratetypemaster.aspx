<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="false" CodeFile="Ratetypemaster.aspx.cs" Inherits="Ratetypemaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Rate Type Master</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Rate Type Master</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Master Type</label>
                                        <div class="radio1">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label>
                                                       <asp:RadioButtonList id="RbtnColl" tabIndex="1" runat="server" Width="370px" 
                        OnSelectedIndexChanged="RbtnColl_SelectedIndexChanged" AutoPostBack="True" CssClass="form-check"
                        RepeatDirection="Horizontal">
<asp:ListItem>Center Rate Type Master</asp:ListItem>

</asp:RadioButtonList> 
                                                    </label>
                                                </div>    
                                                <div class="col-sm-6">
                                                    <label>
                                                       <asp:RadioButtonList id="RbtnColl1"  runat="server" Width="370px" 
                        OnSelectedIndexChanged="RbtnColl1_SelectedIndexChanged" AutoPostBack="True" CssClass="form-check"
                        RepeatDirection="Horizontal">

<asp:ListItem>Refferal Rate Type Master</asp:ListItem>
</asp:RadioButtonList> 
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Rate Type</label>
                                        <div class="radio1">
                                            <label>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <label>
                                                            <asp:RadioButtonList ID="rblretecate" runat="server" Width="350px"
                RepeatDirection="Horizontal" CssClass="form-check">
                <asp:ListItem Selected="True" Value="0">General</asp:ListItem>
                <asp:ListItem Value="1">Insurance</asp:ListItem>
                <asp:ListItem Value="2">Corporate</asp:ListItem>
            </asp:RadioButtonList>
                                                        </label>
                                                    </div>    
                                                  
                                                </div>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label>Rate Type Name <span class="red">*</span></label>
                                      
                                         <asp:TextBox ID="txtRateType" TabIndex="2" placeholder="Enter Rate type Name" runat="server" class="form-control"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Width="181px"
                            Font-Bold="True" ForeColor="Red" ValidationGroup="form" 
                    SetFocusOnError="True" ErrorMessage="Rate type Name is required"
                            Display="Dynamic" ControlToValidate="txtRateType"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                             
                                      <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Click"  CssClass="btn btn-primary" />
                                    
                                    <asp:Button ID="btnsave" runat="server" OnClick="Button1_Click" Text="Save"  CssClass="btn btn-success" />
                                      <asp:Label ID="Label2" runat="server" Style="position: relative" Text="Label" ></asp:Label>
                                </div>
                            </div>
                        </div>
                     
                    </div>
                    <div class="box">
                     <div class="table-responsive" style="width:100%">
                        <asp:GridView id="RateTypeGrid" class="table table-responsive table-sm table-bordered" runat="server" Width="100%" DataKeyNames="RatID"  
                          HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   
                        OnRowEditing="RateTypeGrid_RowEditing" 
                        OnPageIndexChanging="RateTypeGrid_PageIndexChanging" PageSize="20" 
                        AutoGenerateColumns="False" AllowPaging="True" 
                        onrowdeleting="RateTypeGrid_RowDeleting"><Columns>

<asp:BoundField DataField="RatID" HeaderText="Ratetype Code" Visible="false"></asp:BoundField>
<asp:BoundField DataField="RateName" HeaderText="Ratetype Name"></asp:BoundField>

<asp:CommandField HeaderText="Edit" ShowEditButton="True" EditImageUrl="~/Images0/edit.gif" ButtonType="Image"></asp:CommandField>
<asp:CommandField HeaderText="Delete" ShowDeleteButton="True" DeleteImageUrl="~/Images0/delete.gif" ButtonType="Image"></asp:CommandField>
</Columns>


</asp:GridView>
</div>
                    </div>
                </section>
                <!-- /.content -->
           
</asp:Content>

