<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="DrReport_Summary.aspx.cs" Inherits="DrReport_Summary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
 
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>DR Summary Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">DR Summary Report</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
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
                                       
                                       <!-- <select class="form-control">
                                            <option>All</option>
                                            <option>Another Center</option>
                                            <option>Other Center</option>
                                        </select>-->
                                       <asp:TextBox id="txtdoctor" tabIndex="3" placeholder="Enter Doctor" runat="server" CssClass="form-control"></asp:TextBox>
                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender1"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" runat="server" CompletionListElementID="div" TargetControlID="txtdoctor" ServiceMethod="FillDoctor" MinimumPrefixLength="1"></cc1:AutoCompleteExtender>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 250px; HEIGHT: 250px" id="div"></DIV>

                                    </div>
                                </div>
                               
                                           
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                     
                                       <asp:TextBox id="txtCenter" tabIndex="4" placeholder="Enter Center" runat="server" CssClass="form-control"></asp:TextBox>
                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" CompletionListElementID="div1" TargetControlID="txtCenter" ServiceMethod="GetCenter" MinimumPrefixLength="1"></cc1:AutoCompleteExtender>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 250px; HEIGHT: 250px" id="div1"></DIV>

                                    </div>
                                </div>
                                <div class="col-sm-8 text-center">
                                   <asp:Button ID="btncalculate" runat="server" 
                                onclientclick=" return validate();" Text="Calculate"  CssClass="btn btn-info" 
                                        onclick="btncalculate_Click" />
                            
<asp:Button ID="btnlist" runat="server" Visible="false"  onclientclick=" return validate();" Text="Click"  CssClass="btn btn-primary"
                            onclick="btnlist_Click"  />
                    
                                   <asp:Button ID="btnPdfReport" runat="server" onclick="btnPdfReport_Click" 
                                onclientclick=" return validate();" CssClass="btn btn-secondary" Text="PDF REPORT" ToolTip="PDF REPORT" />
                                <asp:Button ID="btnreport" runat="server" Text="EXCEL REPORT" 
                                OnClientClick=" return validate();" CssClass="btn btn-info" OnClick="btnreport_Click" TabIndex="4" 
                                ToolTip="EXCEL REPORT" />
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
           <asp:gridview id="GV_Drcomp" runat="server"  class="table table-responsive table-sm table-bordered" onrowediting="GV_Drcomp_RowEditing" width="100%"
                    onrowdatabound="GV_Drcomp_RowDataBound" DataKeyNames="PID"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False"
                    onpageindexchanging="GV_Drcomp_PageIndexChanging">
                    <columns>
                        
                        <asp:BoundField DataField="RegistratonDateTime" HeaderText=" Date" />                        
                          <asp:BoundField DataField="PatRegID" HeaderText=" Reg no" />
                          <asp:BoundField DataField="Fname" HeaderText=" Name" />
                           <asp:BoundField DataField="DrName" HeaderText=" Dr.Name" />
                            <asp:BoundField DataField="CenterName" HeaderText=" Center" />
                          
                            <asp:BoundField DataField="TestName" HeaderText="Test" />
                           <asp:BoundField DataField="TestCharges" HeaderText="Charges" />
                            <asp:BoundField DataField="Expr1" HeaderText="Dr Amt" />
                         <asp:BoundField DataField="DrGiven" HeaderText="Dr Discount" />
                         <asp:BoundField DataField="LabGiven" HeaderText="Center Disc" />
                         <asp:BoundField DataField="Discount" HeaderText="Tot Discount" />
                         <asp:BoundField DataField="Balance" HeaderText="Balance" />
                         
                         <asp:BoundField DataField="FinalDrCharges" FooterStyle-Font-Bold="true" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Red" ControlStyle-ForeColor="Red" HeaderText="Final Dr Amt" />
                           <asp:TemplateField HeaderText="ViewPres">
                            <ItemTemplate>
<asp:HyperLink ID="Hyp_viewPres" runat="server" NavigateUrl='<%# Eval("UploadPrescription") %>'>View Pres</asp:HyperLink>
                            </ItemTemplate>
                            </asp:TemplateField>
                    </columns>
                   

                </asp:gridview>
               
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
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

