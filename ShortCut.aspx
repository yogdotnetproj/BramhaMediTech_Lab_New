<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="ShortCut.aspx.cs" Inherits="ShortCut" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Short Cut</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Short Cut</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                <div class="row mb-3">
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
                                    <asp:label id="Label1" runat="server" forecolor="#C04000"></asp:label>
                                    <asp:Button ID="btnAdd" runat="server" class="btn btn-primary pull-right" Text="Add New" 
                        onclick="btnAdd_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            
                
                <div class="table-responsive" style="width:100%">
                <asp:gridview id="GVShortcut" Width="100%" runat="server"  class="table table-responsive table-sm table-bordered"     autogeneratecolumns="False" emptydatatext="No Record  Found"
                    datakeynames="ShortFormID"  onrowdatabound="GVShortcut_RowDataBound"
                    onrowdeleting="GVShortcut_RowDeleting"  allowpaging="True" onpageindexchanging="GVShortcut_PageIndexChanging"
                    pagesize="20">
                    <columns>
                                  <asp:BoundField DataField="MainTest" HeaderText="Main Test" />                         
                               <asp:BoundField DataField="SubTest" HeaderText="SubTest" />
                             <asp:BoundField DataField="Shortform" HeaderText="Short Cut" />                         
                               <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images0/delete.gif" >
                                <ItemStyle HorizontalAlign="Left" />

                            </asp:CommandField>
                             <asp:HyperLinkField HeaderText="Edit" Text="Edit"    DataNavigateUrlFields="ShortFormID" DataNavigateUrlFormatString="AddShortcut.aspx?Shortformid={0}"/>
                        
                        </columns>
                   

                </asp:gridview>
                </div>
                    
                        </div>
                       
                    </div>
                 <!--   ======================== -->
                  

                </section>
                <!-- /.content -->


           
</asp:Content>

