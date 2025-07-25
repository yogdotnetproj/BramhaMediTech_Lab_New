 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="PatientCardReport.aspx.cs" Inherits="PatientCardReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Patient Card Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Patient Card Report</li> 
                    </ol>
                </section>

                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                               
                               
                               <div class="col-sm-4">
                                    <div class="form-group">
                                     
                                          <asp:TextBox id="txtPatientname" placeholder="Enter Patient Name" tabIndex="6" 
                                              runat="server" class="form-control" AutoPostBack="True" 
                                              ontextchanged="txtPatientname_TextChanged" ></asp:TextBox>
                <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 185px; HEIGHT: 100px" id="Div2"></DIV>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" CompletionListElementID="Div2" ServiceMethod="GetPatientName" TargetControlID="txtPatientname" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                                   <div class="col-sm-2">
                                                 <asp:button id="btnreport1" OnClientClick="return validate();" runat="server" class="btn btn-primary" onclick="btnreport1_Click"
                                            tooltip="Patient Card " Text="Patient Card"/>
                                                   </div>
                            </div>
                        </div>
                     
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
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
