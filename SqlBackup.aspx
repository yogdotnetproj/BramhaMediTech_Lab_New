<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="SqlBackup.aspx.cs" Inherits="SqlBackup" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>SQL Backup</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">SQL Backup</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">    
                    
                               
                                        <asp:button id="btnSqlbackup"  runat="server" CssClass="btn btn-primary" 
                                            tooltip="SQL Backup" Text="SQL Backup"  OnClientClick="return ValidateDelete();" onclick="btnSqlbackup_Click"
                                            />
                                </div> 
                            </div>
                        </div>
                    </div>
                    
                </section>
                <!-- /.content -->
           
</asp:Content>

