<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="UserMoneyRequestApproval.aspx.cs" Inherits="UserMoneyRequestApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager Id="scriptmanager" runat="server">
    </asp:scriptmanager>
   
            <!-- Content Wrapper. Contains page content -->
       
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>User Cash Request Approval</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">User Cash Request Approval</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                   
                      <div class="box">
                       <div class="table-responsive" style="width:100%">
                <asp:GridView ID="GV_ExpenceEntry" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                    Width="99%" OnPageIndexChanging="GV_ExpenceEntry_PageIndexChanging"
                    AllowPaging="True" PageSize="15" OnRowDeleting="GV_ExpenceEntry_RowDeleting"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White" OnRowDataBound="GV_ExpenceEntry_RowDataBound"   >
                    <Columns>
                        <asp:BoundField DataField="RequestFrom" HeaderText=" Request From" />
                         <asp:BoundField DataField="RequestTo" HeaderText="Request To" />
                          <asp:BoundField DataField="ExpectedAmt" HeaderText="Requested Amount " />
                          
                            <asp:BoundField DataField="RequestDate" HeaderText="Request Date"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                        <asp:BoundField DataField="Remarks" HeaderText="Details" />
                         <asp:BoundField DataField="RequestApprove" HeaderText="Req Status" />
                       <asp:TemplateField HeaderText="Approve Amt ">
                            <ItemTemplate >  
                            <asp:TextBox ID="txtApproveAmt" runat="server" Text='<%#Eval("ReceiveAmt") %>'  Width="100px"  ></asp:TextBox>
                            </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Approval" ShowDeleteButton="True" DeleteImageUrl="~/Images0/login-32.png"
                            ButtonType="Image" />
                        
                    </Columns>
                   

                </asp:GridView>
                           <asp:Label ID="LblMsg" runat="server"  Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                </div>
                    </div>
                </section>
                <!-- /.content -->
          
            <!-- /.content-wrapper -->
           
         <asp:hiddenfield id="ValueHiddenField" runat="server" value="" />

        </asp:Content>

