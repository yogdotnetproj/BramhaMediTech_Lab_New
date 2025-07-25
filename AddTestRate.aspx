<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="AddTestRate.aspx.cs" Inherits="AddTestRate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Test Rate</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Test Rate</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                       
                                              <asp:DropDownList ID="ddlRate" runat="server" CssClass="form-control form-select" TabIndex="1">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlRate"
                        Display="Dynamic" ErrorMessage="This is required field." InitialValue="Select Rate Type"
                        SetFocusOnError="True" ForeColor="Red" Style="position: relative" Font-Bold="True"></asp:RequiredFieldValidator>
                                            
                                       
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                                <asp:DropDownList ID="Ddlsubdept" runat="server" CssClass="form-control form-select"  TabIndex="2">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ControlToValidate="Ddlsubdept"
                        Display="Dynamic" ErrorMessage="This is required field." InitialValue="Select Dept"
                        SetFocusOnError="True" Style="position: relative" Font-Bold="True"></asp:RequiredFieldValidator>
                                        
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        
                                       <asp:TextBox ID="txttestname" placeholder="Enter Test Name " runat="server" AutoPostBack="True"  TabIndex="3"
                            ontextchanged="txttestname_TextChanged"  class="form-control" ></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" MinimumPrefixLength="1"
                         CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                            TargetControlID="txttestname" ServiceMethod="GetTestname" CompletionListElementID="Divdoc">
                           
                        </cc1:AutoCompleteExtender>
                        
                        <div style="display: none; overflow: scroll; width: 431px; height: 100px; text-align: right"
                                            id="Divdoc">
                                            </div>
                                      
                                    </div>
                                </div>
                               
                                             <div class="col-sm-3">
                                                                      
                                     <asp:button id="Button1" runat="server" onclick="Button1_Click" text="Click" CssClass="btn btn-info" tabindex="4" />
                                        <asp:Label ID="Label4" runat="server" Text="" Font-Bold="true" ForeColor="red"  >  </asp:Label >
                                        <asp:button id="btnreport" runat="server" 
                                         OnClientClick="return validate();"  text="Report" CssClass="btn btn-primary" 
                                         tabindex="5" onclick="btnreport_Click" />
                                             </div>  
                            </div>
                        </div>
                   
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
              <asp:GridView ID="RateGrid" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False" Width="100%"
                        OnRowDataBound="RateGrid_RowDataBound" OnPageIndexChanging="RateGrid_PageIndexChanging"
                        PageSize="25"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"    OnRowDeleting="RateGrid_RowDeleting" 
                                    onrowcreated="RateGrid_RowCreated">
                    <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                        <Columns>
                         <asp:BoundField HeaderText="Rate Type" DataField="RateType" />
                            <asp:BoundField HeaderText="ST CODE" DataField="STCODE" />
                            <asp:BoundField HeaderText="Test Name" DataField="TestName" />
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtamount" runat="server" Width="70PX" Text='<%#Bind("Amount")%>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images0/delete.gif"
                                HeaderText="Delete">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                        </Columns>
                       


                    </asp:GridView>
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                     <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    
                                     <asp:Button ID="btnsave" runat="server"  class="btn btn-primary"  OnClientClick="return Validate();" Text="Save"
                        OnClick="btnSave_Click"  />
                                    
                                   
                                </div>
                            </div>
                        </div>
                </section>
                <!-- /.content -->
           
</asp:Content>

