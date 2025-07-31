 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Create_Center.aspx.cs" Inherits="Create_Center" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

            <!-- Content Wrapper. Contains page content -->
        
                <!-- Content Header (Page header) -->
       <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
         
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Edit Center</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Edit Center</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       
                                        <asp:TextBox ID="txtName" runat="server" placeholder="Enter Center Name"  class="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                         <asp:Button ID="btnshow" runat="server" Text="Show" class="btn btn-info" OnClick="btnshow_Click"  />
                                        
                                         <asp:Button ID="btnaddnew" runat="server" Text="Add New" class="btn btn-primary"
                                    onclick="btnaddnew_Click" />
                                        
                                         <asp:Button ID="btnreport" runat="server" class="btn btn-warning" OnClick="btnreport_Click" Text="Report" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                              <div class="table-responsive" style="width:100%">
                <asp:GridView ID="GVCollcenter" runat="server" Width="100%" class="table table-responsive table-sm table-bordered" DataKeyNames="DoctorCode" AllowSorting="true"
                    AutoGenerateColumns="False" OnRowDataBound="GVCollcenter_RowDataBound" OnSorting="GVCollcenter_Sorting"
                    AllowPaging="True" OnPageIndexChanging="GVCollcenter_PageIndexChanging" 
                      HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White" 
                      PageSize="20">
                    <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                    <Columns>
                       
                        <asp:BoundField DataField="DoctorCode" HeaderText=" Code" />
                        <asp:BoundField DataField="Name" HeaderText="  Name" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                       
                        <asp:BoundField DataField="Phone" HeaderText="Mobile No" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="Contact_person" HeaderText="Contact Name" />
                         <asp:BoundField DataField="RateTypeName" HeaderText="Rate Type Name" />
                      
                        <asp:HyperLinkField Text="Edit" HeaderText="Edit " DataNavigateUrlFields="DoctorCode"
                            DataNavigateUrlFormatString="Addcenter.aspx?DoctorCode={0}" />
                            <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btndelete" runat="server" CssClass="btn btn-danger" Text="Delete" />                            
                            </ItemTemplate>                            
                            </asp:TemplateField>
                    </Columns>
                      <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#38C8DD" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                </asp:GridView>
                </div>
                        </div>
                       
                    </div>
                     <asp:HiddenField ID="hdnSort" runat="server" />
                </section>
                <!-- /.content -->
             </ContentTemplate>
             </asp:UpdatePanel>
     

     <script language="javascript" type="text/javascript">
         function OpenReport() {
             window.open("Reports.aspx");
         }    
   
   </script>
  
</asp:Content>
