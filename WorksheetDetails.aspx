 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="WorksheetDetails.aspx.cs" Inherits="WorksheetDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
   
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Work sheet Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Work sheet Report</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">                                      
                                        <div class="input-group date"  data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                        <div class="input-group date"  data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                               
                                <div class="col-sm-2">
                                    <div class="form-group">
                                       
                                        
                                             <asp:TextBox id="txtfromregno" placeholder="Enter From reg no" runat="server" class="form-control pull-right"  tabindex="3">
                                      </asp:TextBox>
                                       
                                    </div>
                                </div>

                                     <div class="col-sm-2">
                                    <div class="form-group">
                                       
                                        
                                             <asp:TextBox id="txttoregno" placeholder="Enter To reg no" runat="server" class="form-control pull-right"  tabindex="4">
                                      </asp:TextBox>
                                       
                                    </div>
                                </div>  
                                <div class="col-sm-2">
                                       <asp:button id="btndailytrans" OnClientClick="return validate();" 
                                            runat="server" 
                                            Text="Work Sheet Report" class="btn btn-primary" onclick="btndailytrans_Click" />
                                </div>
                               

                                    </div>

                                 <div  >
                                  
                                    <table>
                                    <tr>
                                  
                                    <td style="VERTICAL-ALIGN: top;  align="left">
                                   Select Department: 
                                     <div style="overflow: scroll; width: 548px; height: 520px" id="div">
                                   
                                         
                                         
                                         <asp:CheckBoxList ID="chksubdept" CssClass="form-check" runat="server"></asp:CheckBoxList></div>
                                    </td>
                                    <TD style="VERTICAL-ALIGN: top; WIDTH: 274px" align="Right">
                                    
                            </TD>
                                    <td></td>
                                    </tr>
                                    
                                    </table>
                                    
                                     
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