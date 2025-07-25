 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master"
 CodeFile="AddTestParameter.aspx.cs" Inherits="AddTestParameter" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Parameter Master</h1>
                    <ol class="breadcrumb">
                  
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Parameter Master</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                      
                                        
                 <asp:Label ID="Label2" runat="server" Text="Label" placeholder="Enter Test Name" class="form-control" SkinID="BoldHead"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-6 text-center">
                                    <div class="form-group pt25">
                                      
                                            <asp:Button ID="btnAdd" runat="server" Text=" New Parameter"  class="btn btn-primary" OnClick="btnAdd_Click" ValidationGroup="AddNew" />
                                          <asp:Button ID="btnAddHead" runat="server" Text=" Add Sub Head" class="btn btn-primary"   ValidationGroup="AddNew" OnClick="btnAddHead_Click"  ToolTip="Add Sub Head " />

                                         <asp:Button ID="btnaddspace" runat="server" Text=" Add Space" class="btn btn-primary"    ToolTip="Add Space " OnClick="btnaddspace_Click" />

                                    </div>
                                </div>

                                 <div class="col-lg-3 text-center">
                                    <div class="form-group pt25">
                                     <asp:Label ID="Label3" runat="server" Font-Bold="true"  ForeColor="Red" Style="position: relative" Text="" ></asp:Label>
                                    </div>
                                    </div>

                            </div>
                        </div>
                    </div>
                    <div class="box">
                      <div class="table-responsive" style="width:100%">
               <asp:GridView ID="testgrid" runat="server" DataKeyNames="TestID" AutoGenerateColumns="False" class="table table-responsive table-sm table-bordered"  Width="835px" OnRowDeleting="testgrid_RowDeleting" OnRowUpdating="testgrid_RowUpdating" OnRowDataBound="testgrid_RowDataBound1" SkinID="webhms" OnPageIndexChanged="testgrid_PageIndexChanged" OnPageIndexChanging="testgrid_PageIndexChanging" OnSelectedIndexChanged="testgrid_SelectedIndexChanged" AllowPaging="True" OnRowEditing="testgrid_RowEditing" PageSize="35" >
        <Columns>        
       
            <asp:BoundField DataField="STCODE" HeaderText="Parameter Code" />
            <asp:BoundField DataField="TestName" HeaderText="Parameter Name" >
                <ItemStyle HorizontalAlign="Left" />
                <HeaderStyle HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:BoundField DataField="Testordno" HeaderText="Order NO" />
            <asp:HyperLinkField Text="Edit" HeaderText="Edit Record" DataNavigateUrlFields="TestID"   DataNavigateUrlFormatString="AddTest.aspx?TestID={0}" />
            <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" ButtonType="Image" DeleteImageUrl="~/Images0/delete.gif" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
                       
        </Columns>     
         </asp:GridView>
                </div>
                <asp:HiddenField id="hdnsort" runat="server"/>
                    </div>
                </section>
                <!-- /.content -->
         
            <!-- /.content-wrapper -->
            
       
     </asp:Content>
