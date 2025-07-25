<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="ShowTest.aspx.cs" Inherits="ShowTest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:ScriptManager id="ScriptManager2" runat="server">
        </asp:ScriptManager>
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Add / Edit Test</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Add / Edit Test</li> 
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
                                   <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                      
                                       
                                         <asp:DropDownList ID="ddlsubdept" runat="server" placeholder="XXXXXXXXXX"  CssClass="form-control form-select"
                        OnSelectedIndexChanged="ddlsubdept_SelectedIndexChanged" 
                        OnTextChanged="ddlsubdept_TextChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsubdept"
                        Display="Dynamic" ErrorMessage="Select department." InitialValue="Select Department" Style="position: relative" SetFocusOnError="True" Font-Bold="True" ValidationGroup="validation"></asp:RequiredFieldValidator>

                                    </div>
                                </div>


                                <div class="col-sm-5">
                                    <div class="form-group">
                                      
                                        
                                        <asp:TextBox id="txttestname" runat="server" class="form-control"  placeholder="Test Name"></asp:TextBox>

<cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" MinimumPrefixLength="1" TargetControlID="txttestname" ServiceMethod="FillTestName" CompletionListElementID="div1"></cc1:AutoCompleteExtender>
<div id="div1" style="width: 250px; overflow: scroll;display :none ; height: 250px;"></div>

                                    </div>
                                </div>
                                <div class="col-sm-1">
                                    <div class="form-group pt25">
                                     
<asp:Button id="btnsearch" onclick="btnsearch_Click" CssClass="btn btn-warning" runat="server" Text="Search"></asp:Button> 

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                          <div class="table-responsive" style="width:100%">
                    <asp:UpdatePanel id="UpdatePanel3" runat="server">
                        <contenttemplate>
                        <asp:Label ID="lblerror" runat="server"   style="text-align: left">   </asp:Label>
<asp:GridView id="GVMainTest" runat="server" class="table table-responsive table-sm table-bordered" Width="100%"    HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"    AllowSorting="True" AllowPaging="true" OnRowEditing="GVMainTest_RowEditing" OnRowDeleting="GVMainTest_RowDeleting" PageSize="25" OnPageIndexChanging="GVMainTest_PageIndexChanging" OnRowDataBound="GVMainTest_RowDataBound" AutoGenerateColumns="False" DataKeyNames="Maintestid">
    <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
    <Columns>

<asp:CommandField EditImageUrl="~/Images0/edit.gif" ButtonType="Image" HeaderText="Edit" ShowEditButton="True"></asp:CommandField>
<asp:CommandField ShowDeleteButton="True" DeleteImageUrl="~/Images0/delete.gif" ButtonType="Image" HeaderText="Delete">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:CommandField>
<asp:BoundField DataField="Testordno" HeaderText="Seq No"></asp:BoundField>
<asp:BoundField DataField="Maintestname" HeaderText="Test"></asp:BoundField>
<asp:BoundField DataField="MTCode" HeaderText="MT code"></asp:BoundField>
<asp:BoundField DataField="Singleformat" HeaderText="Parameter"></asp:BoundField>
<asp:BoundField DataField="samecontain" HeaderText="Sample Contain "></asp:BoundField>
<asp:BoundField DataField="TextDesc" HeaderText="Test Format"></asp:BoundField>
<asp:BoundField DataField="Sampletype" HeaderText="Sample Type"></asp:BoundField>

<asp:BoundField DataField="TestMethod_temp" Visible="false" HeaderText="Test Method"></asp:BoundField>

<asp:TemplateField><ItemTemplate>
                        <asp:HyperLink ID="lnkshowparam"  Visible="true" runat="server"></asp:HyperLink></ItemTemplate></asp:TemplateField>

   </Columns>
                       
                         <FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                        </asp:GridView>
                        
                        </contenttemplate></asp:UpdatePanel>&nbsp;
                        </div>
                        </div>
                       
                    </div>

                     <div class="col-sm-3 text-center">
                                    <div class="form-group pt20">
                                        
                                         <asp:Button ID="btnseq" runat="server" Text="Ord No" CssClass="btn btn-primary" OnClick="btnseq_Click"  />
                                        
                                      
                                    </div>
                                </div>
                                
                </section>
                <!-- /.content -->
           
</asp:Content>

