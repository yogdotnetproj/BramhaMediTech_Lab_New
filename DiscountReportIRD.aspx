<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="DiscountReportIRD.aspx.cs" Inherits="DiscountReportIRD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
 
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Discount Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Discount Report</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  CssClass="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                 
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  CssClass="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                               
                               <div class="col-sm-4">
                                    <div class="form-group">
                                     
                                          <asp:TextBox id="txtPatientname" placeholder="Enter Discount Remark" tabIndex="6" runat="server" class="form-control" ></asp:TextBox>
                <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 185px; HEIGHT: 100px" id="Div2"></DIV>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" runat="server" CompletionListElementID="Div2" ServiceMethod="GetPatientName1" TargetControlID="txtPatientname" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">    
                    
                                <asp:button id="btnlist" runat="server" OnClientClick="return validate();" onclick="btnlist_Click" text="Click" CssClass="btn btn-info"
                                            />
                                        <asp:button id="btnreport1" OnClientClick="return validate();" runat="server" CssClass="btn btn-primary" onclick="btnreport1_Click"
                                            tooltip="Discountwise Report" Text="Discountwise Report"
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
                           
                            <asp:BoundField DataField="RecDate" HeaderText="Date" />
                              <asp:BoundField DataField="PatRegID" HeaderText="RegNo" />
                               <asp:BoundField DataField="Patname" HeaderText=" Given Name" />
                          
                         <asp:BoundField DataField="Page" HeaderText="Age " />
                           
                          
                            <asp:BoundField DataField="LabGiven" HeaderText="Lab Dis amt" />
                        <asp:BoundField DataField="DrGiven" HeaderText="Dr Dis amt" />
                              <asp:BoundField DataField="Discount" HeaderText="Total Dis amt" />
                             
                               <asp:BoundField DataField="Drname" HeaderText="Doc Name" />
                           
                                <asp:BoundField DataField="username" HeaderText="User By" />
                                <asp:BoundField DataField="Comment" HeaderText="Dis Remark" />
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

