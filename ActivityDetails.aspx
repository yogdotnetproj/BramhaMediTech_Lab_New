<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="ActivityDetails.aspx.cs" Inherits="ActivityDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
 
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Activity Details Report</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Activity Details Report </li>
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
                                     
                                          <asp:TextBox id="txtPatientname" placeholder="Enter Patient Name" tabIndex="6" runat="server" class="form-control" ></asp:TextBox>
                <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 185px; HEIGHT: 100px" id="Div2"></DIV>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" CompletionListElementID="Div2" ServiceMethod="GetPatientName"
                     CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txtPatientname" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                 <div class="col-lg-3">
                                    <div class="form-group">
                                     
                                          <asp:dropdownlist id="ddlusername" placeholder="Enter user Name" runat="server" class="form-control">  </asp:dropdownlist>
               
                                    </div>
                                </div>
                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">    
                    
                                <asp:button id="btnlist" runat="server" OnClientClick="return validate();" onclick="btnlist_Click" text="Click" class="btn btn-primary"
                                            />
                                        <asp:button id="btnreport1" OnClientClick="return validate();" runat="server" class="btn btn-primary" onclick="btnreport1_Click"
                                            tooltip="Activity Details Report" Text="Activity Details Report"
                                            />
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
       <asp:gridview id="GridView1" class="table table-responsive table-sm table-bordered" runat="server" width="100%" onrowdatabound="GridView1_RowDataBound"
                    onrowediting="GridView1_RowEditing"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False" PageSize="25"
                    allowpaging="True" onpageindexchanging="GridView1_PageIndexChanging">
                    <columns>
                            <asp:BoundField DataField="Patregno" HeaderText="RegNo" />
                            <asp:BoundField DataField="PatientName" HeaderText="PatientName" />
                             
                               <asp:BoundField DataField="FormName" HeaderText=" Form Name" />
                          
                         <asp:BoundField DataField="EventName" HeaderText="Activity Name " />
                           
                          
                            <asp:BoundField DataField="CreatedBy" HeaderText="Activity By" />
                        <asp:BoundField DataField="ActivityDateTime" HeaderText="Activity Time" />
                        </columns>
                       

                </asp:gridview>          
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
       <script language="javascript" type="text/javascript">
           function OpenReport() {
               window.open("Reports.aspx");
           }
               </script>
           
</asp:Content>

