 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddReferingDoctor.aspx.cs" MasterPageFile="~/Hospital.master"  Inherits="AddReferingDoctor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <asp:scriptmanager Id="scriptmanager" runat="server">
    </asp:scriptmanager>
   

          
            <!-- Left side column. contains the logo and sidebar -->
          
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Doctor Registration</h4>
                    <ol class="breadcrumb">
                   
                        <li class="breadcrumb-item"><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="breadcrumb-item active">Doctor Registration</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                       
                                         <asp:textbox id="txtdoctorcode" placeholder="Enter Doctor Code" runat="server" CssClass="form-control" tabindex="1">
                </asp:textbox>
                <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" controltovalidate="txtdoctorcode"
                    display="Dynamic" errormessage="This field is required" font-bold="False" validationgroup="FieldValidation">*</asp:requiredfieldvalidator>
                <asp:label id="lblCodeError" runat="server" skinid="errmsg"></asp:label>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        
                                        <div class="row">
                                            <div class="col-lg-3 col-xs-4">
                                               
                                                 
                <asp:dropdownlist id="cmbInitial" runat="server" class="form-control"
                    tabindex="2">
                </asp:dropdownlist>
                                            </div>
                                            <div class="col-lg-9 col-xs-8">
											
                                               
                <asp:textbox id="txtdoctorname" runat="server" placeholder="Enter Name" class="form-control" 
                    tabindex="3"></asp:textbox>&nbsp;
                <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" controltovalidate="txtdoctorname"
                    display="Dynamic" errormessage="This field is required" font-bold="False" validationgroup="FieldValidation">*</asp:requiredfieldvalidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                       
                                        <asp:textbox id="txtCity" runat="server" placeholder="Enter City"  class="form-control"></asp:textbox>
                                    </div>
                                </div>


                                 <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                      
                                        
                                        <asp:TextBox ID="TxtEmail" runat="server" placeholder="Enter Email" class="form-control" >
                                        </asp:TextBox>
                                    </div>
                                </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                                                                            
                                        
                                        <asp:textbox id="txtdoctorphoneno" runat="server" placeholder="Enter Contact No " MaxLength="13" class="form-control">
                </asp:textbox>
                <cc1:FilteredTextBoxExtender  runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtdoctorphoneno" FilterType="Numbers" >
                </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                      
                                         <asp:dropdownlist id="ddlCenter" runat="server" class="form-control form-select">
                </asp:dropdownlist>
                <asp:requiredfieldvalidator id="RequiredFieldValidator4" runat="server" controltovalidate="ddlCenter"
                    errormessage="This Field is required">*</asp:requiredfieldvalidator>
                                       
                                    </div>
                                </div>

                                 <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                      
                                         <asp:dropdownlist id="ddldrrefratetype" runat="server" class="form-control form-select">
                </asp:dropdownlist>
                                        
                                    </div>
                                </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                      
                                            <asp:textbox id="txtAddress" runat="server" placeholder="Enter Address" class="form-control" textmode="MultiLine">
                  
                </asp:textbox>
                                    </div>
                                </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                      
                                         <asp:dropdownlist id="ddlPRO" runat="server" class="form-control form-select">
                </asp:dropdownlist>
                                    
                                    </div>
                                </div>

                              
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row mt-3">
                                <div class="col-lg-12 text-center">
                                    
                                    <asp:button id="btnsave" runat="server"  OnClientClick="return Validate();" onclick="cmdSave_Click" text="Save" tooltip="Save"
                               class="btn btn-primary" />
                                 <asp:hiddenfield id="ValueHiddenField" runat="server" value="" />
    <asp:label id="Label2" runat="server" text="Label" visible="False"></asp:label>
    <asp:label id="lblnote" runat="server" ForeColor="Red" Font-Bold="true" visible="False"></asp:label>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
          
            <!-- /.content-wrapper -->
          
       
        <!-- ./wrapper -->
      

 
   </asp:Content>
