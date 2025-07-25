<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="DepartmentWiseCount.aspx.cs" Inherits="DepartmentWiseCount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
  
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Department Wise Test Report </h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Department Wise Test Report </li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                             
                            
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                      
                <asp:dropdownlist id="ddlYear" runat="server" class="form-control form-select" tabindex="3">
                    <asp:ListItem Value="Year">Year</asp:ListItem>
                    <asp:ListItem Value="2022">2022</asp:ListItem>
                    <asp:ListItem Value="2023">2023</asp:ListItem>
                    <asp:ListItem Value="2024">2024</asp:ListItem>
                    <asp:ListItem Value="2025">2025</asp:ListItem>
                    <asp:ListItem Value="2026">2026</asp:ListItem>
                    <asp:ListItem Value="2027">2027</asp:ListItem>
                    <asp:ListItem Value="2028">2028</asp:ListItem>
                    <asp:ListItem Value="2029">2029</asp:ListItem>
                    <asp:ListItem Value="2030">2030</asp:ListItem>
                </asp:dropdownlist>
                                    </div>
                                </div>
                                
                               <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:button id="btndailytrans" OnClientClick="return validate();" 
                                            runat="server" 
                                            Text="Report" class="btn btn-info" onclick="btndailytrans_Click" />
                                        </div>
                                   </div>
                                               
                            </div>
                           
                        </div>
                       
                    </div>
                   
                </section>
                 <script language="javascript" type="text/javascript">
                     function OpenReport() {
                         window.open("Reports.aspx");
                     } 
               </script>
</asp:Content>

