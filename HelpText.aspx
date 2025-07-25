 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="HelpText.aspx.cs" Inherits="HelpText" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
 
            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Help</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Help</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">    
                    
                               
                                        <asp:button id="btnreport1"  runat="server" CssClass="btn btn-primary" onclick="btnreport1_Click"
                                            tooltip="Help" Text="Help"
                                            />
                                </div> 
                               
                            </div>
                        </div>
                    </div>
                    
                </section>
                <!-- /.content -->
           
            <!-- /.content-wrapper -->
         
         <script language="javascript" type="text/javascript">
             function OpenReport() {
                 window.open("Reports.aspx");
             } 
               </script>
    </asp:Content>
