<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="AddTest.aspx.cs" Inherits="AddTest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
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
                    <div class="box">
                        <!--<div class="box-header with-border">
                            <div class="row pull-right">
                                <div class="col-lg-12 red">
                                    Fields marked with * are mandatory
                                </div>
                            </div>
                        </div>-->
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                      
                                              <asp:DropDownList ID="ddlsubdept" runat="server" CssClass="form-control form-select" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlsubdept_SelectedIndexChanged1" TabIndex="1">
                </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlsubdept"
                    Display="Dynamic" ErrorMessage="This is required field." InitialValue="0" Style="position: relative;
                    left: 0px; top: -1px;" ValidationGroup="form" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                    
                                      
                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"  placeholder="Test Name"
                    TabIndex="2"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                      
                                         <asp:TextBox ID="txtCode" runat="server" 
                    MaxLength="5" ontextchanged="txtCode_TextChanged" AutoPostBack="true"  CssClass="form-control" placeholder="Test Code" TabIndex="3"
                    onkeypress="return alphanumeric_only(this);" ></asp:TextBox>
            
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                    
                                       
                                          <asp:TextBox ID="txtRate" runat="server" CssClass="form-control"  placeholder="Test Sample Contain" TabIndex="4" onkeypress="return numeric_only(this);"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                      
                                        <asp:TextBox ID="txtOrder" runat="server"  CssClass="form-control"  placeholder="Test Sequence Number" TabIndex="5"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                        
                                        <asp:TextBox ID="txtMethod" runat="server" CssClass="form-control"  placeholder="Test Method" TabIndex="6"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                      
                                         <asp:DropDownList ID="ddlSampleType" runat="server" CssClass="form-control form-select"
                    TabIndex="7">
                </asp:DropDownList>
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlSampleType"
                    Display="Dynamic" ErrorMessage="This is required field." Style="position: relative;
                    left: 0px; top: -3px;" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                  
                                           <asp:TextBox ID="txtshortform" runat="server" CssClass="form-control"  placeholder="Short Form" TabIndex="8"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-sm-6">
                                    <div class="form-group">
                                       
                                        <div class="radio">
                                            <div class="row mb-3">
                                                <div class="col-sm-6">
                                                    <label>
                                                       <asp:RadioButton ID="radioSingle" CssClass="form-check"  runat="server" Text="Single test"
                                        AutoPostBack="True" ProfileName="Value" OnCheckedChanged="radioSingle_CheckedChanged"
                                        Checked="True"></asp:RadioButton>
                                                                
                                                    </label>
                                                </div>    
                                                <div class="col-sm-6">
                                                    <label>
                                                          <asp:RadioButton ID="radioFormat" CssClass="form-check" runat="server" Text="Format test"
                                        AutoPostBack="True" ProfileName="Value" 
                                        OnCheckedChanged="radioFormat_CheckedChanged">
                                    </asp:RadioButton>
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-6">
                                    <div class="form-group">
                                      
                                        <div class="radio">
                                            <div class="row mb-3">
                                                <div class="col-sm-4">
                                                    <label>
                                                         <asp:CheckBox ID="radioMemo" runat="server" AutoPostBack="True" ProfileName="Memo"
                                OnCheckedChanged="radioMemo_CheckedChanged" Text="Word Format" CssClass="form-check"
                                />
                                                    </label>
                                                </div>    
                                                <div class="col-sm-4">
                                                    <label>
                                                          <asp:RadioButton ID="radioText" runat="server" Visible="false" AutoPostBack="True" ProfileName="Memo"
                                OnCheckedChanged="radioText_CheckedChanged" Text="Normal Format" CssClass="form-check"
                                 /> 
                                                        <asp:CheckBox ID="chkactive" Checked="true" Text="IS Test Active" runat="server" CssClass="form-check" />
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row mb-3">
                                <div class="col-lg-12 text-center">
                                    
                                      <table>
                                      <tr>
                                      <td>
                                      <asp:Label ID="Label2" runat="server" Text="TAT Time : "></asp:Label>
                                      </td>
                                      <td>
                                      <asp:RadioButtonList ID="rblTat" runat="server" RepeatDirection="Horizontal" CssClass="form-check">
                                          <asp:ListItem Selected="Day" Value="Day"></asp:ListItem>
                                          <asp:ListItem Selected="Hrs" Value="Hrs"></asp:ListItem>
                                          <asp:ListItem Selected="Min" Value="Min"></asp:ListItem>
                                          </asp:RadioButtonList>
                                      </td>
                                      <td>
                                      
                                          <asp:TextBox ID="txtTattime" runat="server"></asp:TextBox>
                                      
                                      </td>
                                     
                                      </tr>
                                      </table>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    
                                      <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnSave_Click"
                                ValidationGroup="form"  CssClass="btn btn-success" OnClientClick="return selectdd();" 
                                TabIndex="19"  />
                                   
                                    <asp:Button ID="Btnback" runat="server"  Text="Back" CssClass="btn btn-secondary" OnClick="Btnback_Click"
                             TabIndex="21" />
                                      <asp:Label ID="Label1" runat="server" Font-Bold="true"  ></asp:Label>
                                        <asp:Button ID="btntestformat" runat="server" Text="Test Format"   
                                              CssClass="btn btn-primary" onclick="btntestformat_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
            
</asp:Content>

