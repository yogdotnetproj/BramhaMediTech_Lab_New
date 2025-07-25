<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="AddShortcut.aspx.cs" Inherits="AddShortcut" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>New Shortcut</h1>
                    <ol class="breadcrumb">
                        
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">New Shortcut</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-header with-border">
                            <span class="red pull-right">Fields marked with * are compulsory</span> 
                        </div>
                        <div class="box-body">
                             <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                         <asp:DropDownList ID="ddMaintest" class="form-control" runat="server" 
                                             OnSelectedIndexChanged="ddMaintest_SelectedIndexChanged"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                                         <asp:DropDownList ID="ddlParametercode" runat="server" class="form-control" >
                                        </asp:DropDownList>
                                        </div>
                                    </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                                         <asp:textbox id="txtShortform" runat="server" placeholder="Enter Short Form" class="form-control" tooltip="Insert Short Form">
                </asp:textbox>
                                        </div>
                                    </div>
                                 </div>
                            <div class="row">
                               
                                <div class="col-lg-12">
                                    <div class="form-group">
                                       
                                        <div class="row">
                                            <div class="col-lg-12 col-xs-12">
                                                
                                                 <asp:textbox id="txtDescription" runat="server" placeholder="EnterDescription"  tooltip="Insert Description"
                   class="form-control" textmode="MultiLine">
                </asp:textbox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                    <asp:button id="btnsave" runat="server" onclick="btnSave_Click" OnClientClick="return Validate();" text="Save" class="btn btn-primary" />
                                   
                                    <asp:button id="btnBack" runat="server" text="Back"  onclick="btnBack_Click" class="btn btn-primary" />
                                 
                                </div>
                            </div>
                        </div>
                         <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                       <asp:label id="lblnote" runat="server" Font-Bold="true"  width="220px" >
                </asp:label>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
            

</asp:Content>

