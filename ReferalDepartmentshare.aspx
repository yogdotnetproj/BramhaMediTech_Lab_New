 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="ReferalDepartmentshare.aspx.cs" Inherits="ReferalDepartmentshare" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Referal Department Share</h1>
                    <ol class="breadcrumb">
                  
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Referal Department Share </li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                       <!-- <select class="form-control">
                                            <option>All</option>
                                            <option>Another Center</option>
                                            <option>Other Center</option>
                                        </select>-->
                                       <asp:TextBox id="txtdoctor" tabIndex=3 runat="server" placeholder="Enter Doctor" class="form-control"></asp:TextBox>
                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" CompletionListElementID="div" TargetControlID="txtdoctor" ServiceMethod="FillDoctor" MinimumPrefixLength="1"></cc1:AutoCompleteExtender>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 250px; HEIGHT: 250px" id="div"></DIV>

                                    </div>
                                </div>
                                         <div class="col-md-3">
                                                <asp:Button ID="btncalculate" runat="server" 
                                onclientclick=" return validate();" Text="Calculate"  class="btn btn-primary" 
                                        onclick="btncalculate_Click" />
                            

                    
                                   <asp:Button ID="btnPdfReport" runat="server" onclick="btnPdfReport_Click" 
                                onclientclick=" return validate();" class="btn btn-primary" Text="REPORT" 
                                        ToolTip="REPORT" />
                                <asp:Button ID="btnreport" runat="server" Text="Summery " 
                                OnClientClick=" return validate();" class="btn btn-primary" 
                                        OnClick="btnreport_Click" TabIndex="4" 
                                ToolTip="Summery" />
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