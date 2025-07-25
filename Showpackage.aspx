<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="Showpackage.aspx.cs" Inherits="Showpackage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Show Package</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Show Package</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    
                          <div class="row ">
                              <div class="col-lg-12 text-center">
                                   
                                     <asp:Button ID="Btn_Add_Dept" runat="server" CssClass="btn btn-light mb-2" 
                                        Text="Add Department" onclick="Btn_Add_Dept_Click"  />
                                        <asp:Button ID="Btn_Add_Test" runat="server" CssClass="btn btn-info mb-2" 
                                        Text="Add Test" onclick="Btn_Add_Test_Click"  />
                                         <asp:Button ID="btnedittest" runat="server" CssClass="btn btn-secondary mb-2" 
                                        Text="Edit Test" onclick="btnedittest_Click"  />
                                         <asp:Button ID="Btn_Add_NR" runat="server" CssClass="btn btn-primary mb-2" 
                                        Text="Add Referance Range" onclick="Btn_Add_NR_Click"  />
                                         <asp:Button ID="Btn_Add_PK" runat="server" CssClass="btn btn-light mb-2" 
                                        Text="Add Package" onclick="Btn_Add_PK_Click"   />
                                          <asp:Button ID="Btn_Add_Sample" runat="server" CssClass="btn btn-info mb-2" 
                                        Text="Add Sample Type" onclick="Btn_Add_Sample_Click"   />
                                        <asp:Button ID="Btn_Add_ShortCut" runat="server" CssClass="btn btn-secondary mb-2" 
                                        Text="Add Short Cut" onclick="Btn_Add_ShortCut_Click"  />
                                         <asp:Button ID="Btn_Add_Formula" runat="server" CssClass="btn btn-primary mb-2" 
                                        Text="Add Formula" onclick="Btn_Add_Formula_Click"    />
                                         <asp:Button ID="Btn_Add_RN" runat="server" CssClass="btn btn-info mb-2" 
                                        Text="Add Report Note" onclick="Btn_Add_RN_Click"  />
                                </div>
                               
                            </div>
                            <div class="row mb-3">
                                <div class="col-lg-12">
                                <asp:Label ID="Label2" runat="server" Text="."  ></asp:Label>
                                </br>
                                </div>
                                  </div>
                                  <div class="box" runat="server" id="List" >
                        <div class="box-header with-border">
                            <div class="row mb-3">
                                <div class="col-lg-12">
                                   
                                    <asp:Button ID="btnaddnew" runat="server" CssClass="btn btn-primary pull-right" Text="Add New" 
                        onclick="btnaddnew_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                           
                
                
                      <div class="table-responsive" style="width:100%">
                <asp:GridView ID="GV_ShowPackage" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False" OnRowEditing="GV_ShowPackage_RowEditing"
                    OnRowDataBound="GV_ShowPackage_RowDataBound" PageSize="20" OnPageIndexChanging="GV_ShowPackage_PageIndexChanging"
                    OnRowDeleting="GV_ShowPackage_RowDeleting" 
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"    Width="100%">
                    <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                    <Columns>
                        <asp:CommandField ShowEditButton="True" HeaderText="Edit" EditImageUrl="~/Images0/edit.gif"
                            ButtonType="Image" />
                       
                        <asp:BoundField DataField="PackageCode" HeaderText="Package Code" />
                        <asp:BoundField DataField="PackageName" HeaderText="Package Name" />
                       
                        <asp:TemplateField HeaderText="Test Name">
                            <ItemTemplate>
                                <asp:Label ID="lbltestNames" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                        <asp:BoundField DataField="PackageRateAmount" Visible="false" HeaderText="Package Rate" />
                         <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images0/delete.gif" />
                    </Columns>
                  

                </asp:GridView>
                </div>
                    
                        </div>
                       
                    </div>
                 <!--   ======================== -->
                  

                </section>
                <!-- /.content -->


           
</asp:Content>

