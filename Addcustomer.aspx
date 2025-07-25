 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Addcustomer.aspx.cs" MasterPageFile="~/Hospital.master"  Inherits="Addcustomer" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
            <!-- Content Wrapper. Contains page content -->
        
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>New customer/Hosp</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">New customer/Hosp</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        
                                       
                <asp:DropDownList ID="ddlCenter" runat="server" AppendDataBoundItems="true"
                    OnSelectedIndexChanged="ddlCenter_SelectedIndexChanged">
                    
                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3 text-center">
                                    <div class="form-group pt25">
                                      
                                         
                                          <asp:Button ID="btnshow" runat="server" CausesValidation="False" 
                    OnClick="btnshow_Click"  Text="Click" ToolTip="Show List" class="btn btn-primary"  />
                                         <asp:Button ID="btnaddnew" runat="server" Text="Click new" 
                        onclick="btnaddnew_Click" class="btn btn-primary" />
                                    </div>
                                </div>

                                 <div class="col-lg-3 text-center">
                                    <div class="form-group pt25">
                                     <asp:Label ID="Label2" runat="server" Font-Bold="true"  ForeColor="Red" Style="position: relative" Text="" ></asp:Label>
                                    </div>
                                    </div>

                            </div>
                        </div>
                    </div>
                    <div class="box">
                      <div class="table-responsive" style="width:100%">
               <asp:GridView ID="CustomerGrid" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False" 
         Width="800px" OnSorting="CustomerGrid_Sorting" 
       HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   OnSelectedIndexChanged="CustomerGrid_SelectedIndexChanged">
        <Columns>
        <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFields="Code" DataNavigateUrlFormatString="CustomerEntry.aspx?code={0}" />
            <asp:BoundField DataField="Code" HeaderText="Code" SortExpression="DocCode" />
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="DoctorName" />
            <asp:BoundField DataField="Address1" HeaderText="Address" />
            <asp:BoundField DataField="city" HeaderText="City" Visible="false" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" Visible="false" />
            <asp:BoundField DataField="Email" HeaderText="Email" Visible="false" />
            <asp:BoundField DataField="Contact_person" HeaderText="Contact Person"  Visible="false" />            
        </Columns>
       

    </asp:GridView>
                </div>
                <asp:HiddenField id="hdnsort" runat="server"/>
                    </div>
                </section>
                <!-- /.content -->
           
            <!-- /.content-wrapper -->
           
       
     </asp:Content>
