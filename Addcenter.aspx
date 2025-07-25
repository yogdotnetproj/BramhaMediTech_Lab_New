 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Addcenter.aspx.cs" Inherits="Addcenter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:ScriptManager id="ScriptManager2" runat="server">
        </asp:ScriptManager>
     
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>New Center</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">New Center</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-header with-border">
                         <asp:Label ID="Label11" runat="server" ForeColor="Red" Font-Bold="true"   Text="Label"></asp:Label>
                            <span class="red pull-right">Fields marked with * are compulsory</span> 
                        </div>
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                      
                                        
                                        <asp:TextBox ID="txtcentercode" class="form-control" placeholder="Enter Center Name" runat="server"  ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-sm-4" runat="server" visible="false">
                                    <div class="form-group">
                                     
                                       
                                          <asp:TextBox ID="txtcentername" runat="server" placeholder="Enter Center Name" class="form-control" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4" runat="server" visible="false">
                                    <div class="form-group">
                                      
                                         <asp:TextBox ID="txtcenterConper" runat="server" class="form-control"  placeholder="Contact Name"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                       
                                        <asp:TextBox ID="txtcenterLocation"  CssClass="form-control" TextMode="MultiLine" runat="server" placeholder="Location"></asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                       
                                         <asp:TextBox ID="txtcenterPincode" runat="server" class="form-control"  placeholder="Pin Code" MaxLength="6"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-sm-12">
                                               
                                                <asp:TextBox ID="txtcenteraddress" placeholder="Enter Address" runat="server"  TextMode="MultiLine"
                        class="form-control" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                               
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                       
                                         <asp:TextBox ID="txtcenterEmail" runat="server" class="form-control"  placeholder="Email"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtcenterEmail"
                        Display="Dynamic" ErrorMessage="Enter valid email address" Font-Bold="True"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="FieldValidation"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                     
                                       
                                        <asp:TextBox ID="txtcenterPhoneno" runat="server" MaxLength="12" class="form-control"  placeholder="Phone"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtcenterPhoneno"
                                FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9">
                            </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                      
                                       
                                        <asp:TextBox ID="txtcenterMobileno" runat="server"  class="form-control"  placeholder="Mobile" MaxLength="10"></asp:TextBox>
                   <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender3" TargetControlID="txtcenterMobileno"
                                FilterMode="ValidChars" ValidChars="0,1,2,3,4,5,6,7,8,9">
                            </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                                
                                <div class="col-sm-4">
                                    <div class="form-group">
                                     
                                      
                                        <asp:TextBox ID="txtDepAmt" runat="server" class="form-control" placeholder="Deposit Amount"></asp:TextBox>
                       <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender4" TargetControlID="txtDepAmt"
                                FilterMode="ValidChars" ValidChars=".,,,0,1,2,3,4,5,6,7,8,9">
                            </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                 <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                      
                                         <asp:DropDownList ID="ddlLab" runat="server" CssClass="form-control form-select" DataSourceID="sqlLab"
                        DataTextField="DoctorName" DataValueField="DoctorCode" AppendDataBoundItems="true"
                        OnSelectedIndexChanged="ddlLab_SelectedIndexChanged">
                        <asp:ListItem Text="Select Under Lab" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="sqlLab" runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
                        SelectCommand="SELECT * FROM [DrMT] WHERE (([DrType] = @DrType) AND ([branchid] = @branchid)) order by DoctorName">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="ML" Name="DrType" Type="String" />
                            <asp:SessionParameter DefaultValue="1" Name="branchid" SessionField="branchid"
                                Type="Decimal" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        
                                       
                                         <asp:DropDownList ID="ddlRateType" runat="server"  DataSourceID="SqlDataSource8"
                        DataTextField="RateName" DataValueField="RatID" AppendDataBoundItems="true"
                        CssClass="form-control form-select">
                        <asp:ListItem Text="Select RateType" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlDataSource8" runat="server" ConnectionString="<%$ ConnectionStrings:myconnection %>"
        SelectCommand="SELECT * FROM [RatT] WHERE ([branchid] = @branchid) and RateFlag='C' order by RateName">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="1" Name="Branchid" SessionField="branchid"
                Type="Decimal" />
        </SelectParameters>
    </asp:SqlDataSource>
                                    </div>
                                </div>
                            </div>
                            <div class="row mb-3">
                               
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        <div class="checkbox">
                                            
                                            <asp:CheckBox ID="ChkIsCenter" Text="Set Default" runat="server" CssClass="form-check" />
                                        </div>
                                    </div>
                                </div> 
                                 <div class="col-sm-4" id="flagid" runat="server" visible="false" >
                                    <div class="form-group">
                                        <div class="checkbox">
                                           
                                              <asp:CheckBox ID="Chkdirectcash" runat="server" Text="Daily Center" CssClass="form-check" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-sm-12 text-center">
                                   
                                     <asp:Button ID="btnsave" runat="server" OnClick="cmdSave_Click" OnClientClick="return Validate();"
                                    Text="Save" ToolTip="Save" ValidationGroup="FieldValidation" CssClass="btn btn-success" />
                                 
                                    <asp:Button ID="Btnback" runat="server" OnClick="Btnback_Click" Text="Back" class="btn btn-secondary"  />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
          
            <!-- /.content-wrapper -->
           
       
      </asp:Content>
