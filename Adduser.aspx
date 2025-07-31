 <%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Hospital.master" CodeFile="Adduser.aspx.cs" Inherits="Adduser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
 
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Add User</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Add User</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box" runat="server" id="List" >
                       <!-- <div class="box-header with-border">
                            <div class="row ">
                                <div class="col-lg-12">
                                    <asp:label id="Label1" runat="server" forecolor="#C04000"></asp:label>
                                    <asp:Button ID="btnAdd" runat="server" class="btn btn-primary pull-right" Text="Add New" 
                        onclick="btnAdd_Click" />
                                </div>
                            </div>
                        </div> -->
                        <div class="row mb-3">
                         <div class="col-sm-4">
                                    <div class="form-group">
                                         <asp:TextBox id="txtusername" runat="server" CssClass="form-control"  placeholder="User Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group pt25">
                                        
                                          <asp:Button ID="btnList" runat="server" Text="Click" CssClass="btn btn-info" 
                                            onclick="btnList_Click"   />
                                          <asp:Button ID="btnAddnew" runat="server" CssClass="btn btn-primary" 
                                            Text="Add New" onclick="btnAddnew_Click"  />
                                          
                                    </div>
                                </div>

                        </div>
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
<asp:GridView  id="userlist" runat="server" 
        Width="100%" OnSelectedIndexChanged="ItemList_SelectedIndexChanged" class="table table-responsive table-sm table-bordered" DataKeyNames="CUId" Font-Underline="False" OnRowDataBound="userlist_RowDataBound" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" OnPageIndexChanging="userlist_PageIndexChanging" OnRowDeleting="userlist_RowDeleting">
<PagerSettings Visible="true"></PagerSettings>
<Columns>
<asp:CommandField SelectImageUrl="~/Images0/edit.gif" SelectText="Edit" ButtonType="Image" ShowSelectButton="True" HeaderText="Edit">
<ItemStyle ForeColor="RoyalBlue"></ItemStyle>
</asp:CommandField>
<asp:BoundField DataField="USERNAME" SortExpression="USERNAME" HeaderText="User Name"></asp:BoundField>

<asp:BoundField DataField="dept" SortExpression="dept" HeaderText="Dept"></asp:BoundField>
<asp:BoundField DataField="UserType" SortExpression="UserType" HeaderText="User Type"></asp:BoundField>
<asp:TemplateField HeaderText="Lab Name">
<ItemTemplate>
<asp:Label ID="Labname" runat="server" Text="" />
                              
                        
</ItemTemplate>
</asp:TemplateField>
  <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btndelete" CommandName="Delete" runat="server"  CssClass="btn btn-danger" Text="Delete" />                            
                            </ItemTemplate>                            
                            </asp:TemplateField>
<asp:TemplateField>
<ItemTemplate>
 <asp:HiddenField ID="hdnuid" runat="server" Value='<%#Eval("CUid") %>' />                        
</ItemTemplate>
</asp:TemplateField>
</Columns>

</asp:GridView>&nbsp;&nbsp;
                    </div>
                        </div>
                       
                    </div>
                 <!--   ======================== -->
                  

                </section>
                <!-- /.content -->
            <!-- /.content-wrapper -->
          
     </asp:Content>
