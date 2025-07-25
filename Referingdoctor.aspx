<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Referingdoctor.aspx.cs" Inherits="Referingdoctor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
 
                <!-- Content Header (Page header) -->
     <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Ref Dr</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Ref Dr</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                         <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                            <asp:TextBox ID="Txtcenter" TabIndex="1" placeholder="Enter Center" runat="server" class="form-control pull-right" OnTextChanged="Txtcenter_TextChanged"
                                            AutoPostBack="True"></asp:TextBox>
                                            <div style="display: none; overflow: scroll; width: 306px;" id="Div2">
                                        </div>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" CompletionListElementID="Div2"
                                            ServiceMethod="GetCenter" TargetControlID="Txtcenter" MinimumPrefixLength="1">
                                        </cc1:AutoCompleteExtender>
                                       
                                    </div>
                                </div>
                                
                                <div class="col-sm-3">
                                    <div class="form-group">    
                                        <asp:TextBox ID="txtDoctorName"  placeholder="Enter Ref Doc"  runat="server" class="form-control pull-right" ></asp:TextBox>
                                        <div style="display: none; overflow: scroll; width: 324px; height: 100px; text-align: right"
                                            id="Divdoc">
                                        </div>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" MinimumPrefixLength="1"
                            TargetControlID="txtDoctorName" ServiceMethod="FillDoctor" CompletionListElementID="Divdoc">
                            
                        </cc1:AutoCompleteExtender>

                                       
                                    </div>
                                </div>
                                <div class="col-sm-5">
                                            <asp:Button ID="btnshow" runat="server" CausesValidation="False" 
                    OnClick="btnshow_Click" TabIndex="1" Text="Click" 
                    ToolTip="Show List" class="btn btn-success" />
                    
                                   
                                   <asp:Button ID="btnaddnew" runat="server" class="btn btn-info" Text="Add refering doc" 
                onclick="btnaddnew_Click" />
                                    <asp:Button ID="btnreport" runat="server" class="btn btn-secondary" 
                                        Text="Rfering doc report" onclick="btnreport_Click" 
                />
                                  
                                </div>
                               
                               
                                               
                            </div>
                        </div>
                       
                    </div>
                    <div class="box">
                        <div class="box-body">
                             <div class="table-responsive" style="width:100%">
                <asp:GridView ID="gridMain" runat="server" class="table table-responsive table-sm table-bordered" AllowSorting="True" AutoGenerateColumns="False"
                    DataKeyNames="username" Height="123px" OnRowDataBound="gridMain_RowDataBound"
                    OnRowDeleting="gridMain_RowDeleting" Width="100%" EmptyDataText="No Record Found"
                    OnSorting="gridMain_Sorting1"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White" OnPageIndexChanging="gridMain_PageIndexChanging" 
                                     onrowcreated="gridMain_RowCreated">
                    <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="DoctorCode" DataNavigateUrlFormatString="AddReferingDoctor.aspx?DoctorCode={0}"
                            HeaderText="Edit " Text="Edit" />
                     
                        <asp:BoundField DataField="DoctorCode" HeaderText="Code" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Address" HeaderText="Address" />
                        <asp:BoundField DataField="City" HeaderText="City" />
                       
                       
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Image"
                            DeleteImageUrl="~/Images0/delete.gif">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
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
           <asp:Label ID="Label1" runat="server" Text="" Visible="False"></asp:Label><br />
    <asp:HiddenField ID="hdnSort" runat="server" />
                </div>
                        </div>
                         <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    
                                     
                                      <asp:Label ID="Label2" runat="server" Font-Bold="true"  ></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
                 <script language="javascript" type="text/javascript">
                     function OpenReport() {
                         window.open("Reports.aspx");
                     } 
               </script>
            
</asp:Content>

