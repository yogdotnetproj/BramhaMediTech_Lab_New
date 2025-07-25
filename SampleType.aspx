<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="SampleType.aspx.cs" Inherits="SampleType" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
                <!-- Content Header (Page header) -->
            <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Sample Type</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Sample Type</li> 
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
                                <asp:Label ID="Label1" runat="server" Text="."  ></asp:Label>
                                </br>
                                </div>
                                  </div>
                
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        
                                         <asp:TextBox ID="txtSampleType" runat="server" placeholder="Enter Sample Type" class="form-control" TabIndex="1"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSampleType"
                    Display="Dynamic" ErrorMessage="Sample Type is required." ForeColor="Red"  SetFocusOnError="True"
                    ValidationGroup="form" Width="152px" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                         <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" CssClass="btn btn-primary" Text="Click"  TabIndex="2" />
                                       
                                         <asp:Button ID="btnsave" runat="server" OnClick="Button1_Click"  Text="Save"  ValidationGroup="form" CssClass="btn btn-success" TabIndex="3" />
                                    
                                    </div>
                                </div>

                                 <div class="col-sm-3">
                                    <div class="form-group pt25">
                                     <asp:Label ID="Label2" runat="server" Font-Bold="true"  ForeColor="Red" Style="position: relative" Text="Label" ></asp:Label>
                                    </div>
                                    </div>

                            </div>
                        </div>
                    </div>
                    <div class="box">
                     <div class="table-responsive" style="width:592px">
                <asp:GridView ID="SampleGrid" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="sampleid"
                    Width="592px" OnPageIndexChanging="SampleGrid_PageIndexChanging" OnRowEditing="SampleGrid_RowEditing"
                    AllowPaging="True" PageSize="25" OnRowDeleting="SampleGrid_RowDeleting"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   >
                    <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="Sampletype" HeaderText="Sample Type" />
                        <asp:CommandField HeaderText="Edit " ShowEditButton="True" EditImageUrl="~/Images0/edit.gif"
                            ButtonType="Image" />
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images0/delete.gif" />
                    </Columns>
                   

                </asp:GridView>
                </div>
                    </div>
                </section>
                <!-- /.content -->
          
</asp:Content>

