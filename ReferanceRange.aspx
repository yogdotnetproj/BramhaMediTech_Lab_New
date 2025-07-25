<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="ReferanceRange.aspx.cs" Inherits="ReferanceRange" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Add / Edit Reference Range</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Add / Edit Reference Range</li> 
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
                       <!-- <div class="box-header with-border">
                            <div class="row pull-right">
                                <div class="col-lg-12 red">
                                    Fields marked with * are compulsory
                                </div>
                            </div>
                        </div>-->
                        <div class="box-body">
                           
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Main Test</label>
                                      
                                         <asp:DropDownList ID="ddMaintest" Visible="false" CssClass="form-control form-select" runat="server" 
                                             OnTextChanged="ddMaintest_TextChanged"   OnSelectedIndexChanged="ddMaintest_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                         <asp:TextBox ID="txttests" placeholder="Main Test" runat="server" TabIndex="1" CssClass="form-control"
                                                                AutoPostBack="True" OnTextChanged="txttests_TextChanged"></asp:TextBox>
                                                            <div style="display: none; overflow: scroll; width: 348px; height: 120px; font-size:15pt;" id="div">
                                                            </div>
                                                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                                CompletionListElementID="div" ServiceMethod="FillTests"  BehaviorID="TTTTT"  TargetControlID="txttests"
                                                                MinimumPrefixLength="1">
                                                            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <label>Parameters</label>                                        
                                        
        <asp:DropDownList ID="ddlParametercode" runat="server" CssClass="form-control form-select"
                                             OnSelectedIndexChanged="ddlParametercode_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label>Gender</label>                                       
                                          <asp:DropDownList ID="ddlsex" runat="server" CssClass="form-control form-select">
                                            <asp:ListItem Value="Male"></asp:ListItem>
                                            <asp:ListItem Value="Female"></asp:ListItem>
                                               <asp:ListItem Value="Both"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                            <div class="form-group">
                                                <label>Lab Name <span class="red"></span></label>
                                              
                                                <asp:DropDownList ID="ddlOutLabNAme"  CssClass="form-control form-select" runat="server" >
                                            
                                        </asp:DropDownList>
                                            </div>
                                        </div>

                            </div>
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="row mb-3">
                                        <div class="col-lg-12 text-center">
                                            <h4><strong>Age</strong></h4>
                                        </div>
                                          <div class="col-sm-4">
                                            <div class="form-group">
                                                <label>Lower <span class="red">*</span></label>
                                               
                                                 <asp:TextBox  ID="txtLower"  runat="server"
                                           class="form-control" MaxLength="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator Style="position: relative" ForeColor="Red"    ID="RequiredFieldValidator1"
                                            runat="server" Font-Bold="True" ErrorMessage="This field is required." Display="Dynamic"
                                            ControlToValidate="txtUpper"  ValidationGroup="ValidateForm"></asp:RequiredFieldValidator>
                                        <asp:Label ID="lblErrorlwr" runat="server" ForeColor="Red"   SkinID="errmsg" ></asp:Label>
                                        <asp:CompareValidator Style="position: relative" ID="CompareValidator1" runat="server"
                                            Font-Bold="True" SetFocusOnError="True"  ForeColor="Red"   ErrorMessage="Please Enter Valid Age"
                                            Display="Dynamic" ControlToValidate="txtLower"  ValidationGroup="ValidateForm"
                                            Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label>Upper <span class="red">*</span></label>
                                               
                                                  <asp:TextBox  ID="txtUpper" runat="server"
                                           class="form-control"  MaxLength="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator Style="position: relative" ID="RequiredFieldValidator2"
                                            runat="server" Font-Bold="True" ForeColor="Red"  ErrorMessage="This field is required." Display="Dynamic"
                                            ControlToValidate="txtLower"  ValidationGroup="ValidateForm"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="vldcompAge" ForeColor="Red"  runat="server" Font-Bold="True" ErrorMessage="Upper limit should be greater. "
                                            Display="Dynamic" ControlToValidate="txtUpper" ValidationGroup="ValidateForm"
                                            Type="Double" Operator="GreaterThan" ControlToCompare="txtLower"></asp:CompareValidator>
                                        <asp:CompareValidator Style="position: relative" ForeColor="Red"   ID="CompareValidator2" runat="server"
                                            Font-Bold="True" SetFocusOnError="True" ErrorMessage="Please Enter Valid Age"
                                            Display="Dynamic" ControlToValidate="txtUpper"  ValidationGroup="ValidateForm"
                                            Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                                        <asp:Label ID="lblErrorupper" runat="server" ForeColor="Red"  SkinID="errmsg" ></asp:Label>
                                            </div>
                                        </div>
                                      
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label>Days <span class="red">*</span></label>
                                              
                                                <asp:DropDownList ID="ddlYear"  CssClass="form-control form-select" runat="server" >
                                            <asp:ListItem Value="Days"></asp:ListItem>
                                            <asp:ListItem Value="Month"></asp:ListItem>
                                            <asp:ListItem Value="Year"></asp:ListItem>
                                        </asp:DropDownList>
                                            </div>
                                        </div>
                                         
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="row mb-3">
                                        <div class="col-lg-12 text-center">
                                          <h4 ><strong >
                                             <asp:Label ID="Label8" ForeColor="Green" Font-Bold="true" runat="server"  Text="Normal Range"
                                            ></asp:Label></strong></h4>
                                        </div>
                                         <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:Label ID="Label9" ForeColor="Green" Font-Bold="true" runat="server"  Text="Lower(*)"
                                            ></asp:Label>
                                              
                                                <asp:TextBox ID="txtLowerRange" class="form-control" runat="server" 
                                            OnTextChanged="txtLowerRange_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:Label ID="Label13" runat="server" SkinID="errmsg" Visible="False" Text="Label"
                                            ></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <asp:Label ID="Label10" ForeColor="Green" Font-Bold="true" runat="server"  Text="Upper(*)"
                                            ></asp:Label>
                                               <asp:TextBox ID="txtUpperRange"  runat="server" CssClass="form-control"
                                            OnTextChanged="txtLowerRange_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:Label ID="Label14" runat="server" SkinID="errmsg" Visible="False" Text="Label"
                                            ></asp:Label>
                                            </div>
                                        </div>
                                       
                                        <div class="col-sm-4">
                                            <div class="form-group">
                                                <label>Unit <span class="red">*</span></label>
                                           
                                                   <asp:TextBox ID="txtUnit"  runat="server" CssClass="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>



                                 <div class="col-sm-4">
                                    <div class="row mb-3">
                                        <div class="col-lg-12 text-center">
                                            <h4 ><strong >
                                             <asp:Label ID="Label5" ForeColor="Red" Font-Bold="true" runat="server"  Text="Panic Range"
                                            ></asp:Label></strong></h4>
                                        </div>
                                         <div class="col-sm-6">
                                            <div class="form-group">
                                               

                                                 <asp:Label ID="Label6" ForeColor="Red" Font-Bold="true" runat="server"  Text="Lower(*)"
                                            ></asp:Label>
                                              
                                                <asp:TextBox ID="txtPanicLowerRange" CssClass="form-control" runat="server" 
                                            OnTextChanged="txtLowerRange_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:Label ID="Label3" runat="server" SkinID="errmsg" Visible="False" Text="Label"
                                            ></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                               
                                                  <asp:Label ID="Label7" ForeColor="Red" Font-Bold="true" runat="server"  Text="Upper(*)"
                                            ></asp:Label>
                                               <asp:TextBox ID="txtPanicUpperRange"  runat="server" CssClass="form-control"
                                            OnTextChanged="txtLowerRange_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        <asp:Label ID="Label4" runat="server" SkinID="errmsg" Visible="False" Text="Label"
                                            ></asp:Label>
                                            </div>
                                        </div>
                                       
                                       
                                    </div>
                                </div>




                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label>General</label>
                                       
                                         <asp:TextBox ID="txtNormalRange"  runat="server" CssClass="form-control"
                                             OnTextChanged="txtNormalRange_TextChanged" AutoPostBack="True"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row text-center">
                               <div class="col-lg-12">
                                  <asp:Button ID="btnsave" runat="server" CssClass="btn btn-success" OnClick="btnSubmit_Click" Text="Save"  />
                                   <asp:Label ID="Label1" runat="server" Style="position: relative" Text="Label" > </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box">
                          <div class="table-responsive" style="width:100%">
              
                        <asp:GridView ID="GVtestnormalvaluegrid" runat="server" class="table table-responsive table-sm table-bordered" 
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 Width="100%"  PageSize="30" OnPageIndexChanging="GVtestnormalvaluegrid_PageIndexChanging" AutoGenerateColumns="False"
                            OnRowEditing="GVtestnormalvaluegrid_RowEditing" OnRowDeleting="GVtestnormalvaluegrid_RowDeleting" 
                            >
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnnormalid" runat="server" Value='<%#Bind("ID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LessThanDays" HeaderText="Age Lower"></asp:BoundField>
                                <asp:BoundField DataField="GreaterThanDays" HeaderText="Age Upper"></asp:BoundField>
                                <asp:BoundField ReadOnly="True" DataField="STestName" HeaderText="Main Test Name">
                                </asp:BoundField>
                               
                                <asp:BoundField ReadOnly="True" DataField="TestName" HeaderText="Parameter Name">
                                </asp:BoundField>
                                <asp:BoundField ReadOnly="True" DataField="STCODE" HeaderText="Parameters"></asp:BoundField>
                                <asp:TemplateField HeaderText="Sex">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ComboSex" runat="server" Width="80px">
                                            <asp:ListItem Value="Male"></asp:ListItem>
                                            <asp:ListItem Value="Female"></asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsex" runat="server" Text='<%#Bind("Sex")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                <asp:BoundField DataField="Unit" HeaderText="Unit"></asp:BoundField>
                                <asp:BoundField DataField="DescretiveRange" HeaderText="Normal Range"></asp:BoundField>
                                <asp:BoundField DataField="LowerRange" HeaderText="Lower Range"></asp:BoundField>
                                <asp:BoundField DataField="UpperRange" HeaderText=" Upper Range"></asp:BoundField>
                                 <asp:BoundField DataField="PanicLowerRange" HeaderText="Panic Lower Range"></asp:BoundField>
                                <asp:BoundField DataField="PanicUpperRange" HeaderText="Panic Upper Range"></asp:BoundField>
                                <asp:CommandField EditImageUrl="~/Images0/edit.gif" ButtonType="Image"
                                    HeaderText="Edit" ShowEditButton="True" >
                                    <ControlStyle ForeColor="Blue"></ControlStyle>
                                </asp:CommandField>
                                <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="~/Images0/delete.gif" ButtonType="Image"
                                    HeaderText="Delete">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ControlStyle ForeColor="Blue"></ControlStyle>
                                </asp:CommandField>
                            </Columns>
                           

                        </asp:GridView>
                   
                </div>
                    </div>
                </section>
                <!-- /.content -->
            
</asp:Content>

