<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="DrReport.aspx.cs" Inherits="DrReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
 
                    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>DR Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">DR Report</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-3">
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
                                <div class="col-sm-3">
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
                                <div class="col-sm-3">
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

                                 <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       <!-- <select class="form-control">
                                            <option>All</option>
                                            <option>Another Center</option>
                                            <option>Other Center</option>
                                        </select>-->
                                       <asp:TextBox id="txtPro" tabIndex="3" placeholder="Enter PRO" runat="server" CssClass="form-control" OnTextChanged="txtPro_TextChanged"></asp:TextBox>
                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender3" runat="server" CompletionListElementID="div2" TargetControlID="txtPro" ServiceMethod="FillPRO" MinimumPrefixLength="1"></cc1:AutoCompleteExtender>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 250px; HEIGHT: 250px" id="div2"></DIV>

                                    </div>
                                </div>
                               
                                           
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row mb-3">
                                <div class="col-sm-4">
                                    <div class="form-group">
                                       
                                     
                                       <asp:TextBox id="txtCenter" tabIndex="4" placeholder="Enter Center" runat="server" class="form-control"></asp:TextBox>
                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" CompletionListElementID="div1" TargetControlID="txtCenter" ServiceMethod="GetCenter" MinimumPrefixLength="1"></cc1:AutoCompleteExtender>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 250px; HEIGHT: 250px" id="div1"></DIV>

                                    </div>
                                </div>
                                <div class="col-sm-8 text-center">
                                   <asp:Button ID="btncalculate" runat="server" 
                                onclientclick=" return validate();" Text="Calculate"  CssClass="btn btn-info" 
                                        onclick="btncalculate_Click" />
                            
<asp:Button ID="btnlist" runat="server" 
                                onclientclick=" return validate();" Text="Click"  CssClass="btn btn-primary"
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
                                 <table width="100%">
                   <tr>
                       <td>
<asp:Label runat="server" ID="a" Font-Bold="true" ForeColor="Red" Text="Charges"></asp:Label>
                       </td>
                       <td>
                           <asp:Label runat="server" ID="lblcharges" ></asp:Label>
                       </td>
                  
                       <td>
<asp:Label runat="server" ID="Label2" Font-Bold="true" ForeColor="Red" Text="Dr Amt"></asp:Label>
                       </td>
                       <td>
                           <asp:Label runat="server" ID="lbldramt" ></asp:Label>
                       </td>
                   
                       <td>
<asp:Label runat="server" ID="Label4" Font-Bold="true" ForeColor="Red" Text="Dr Disc"></asp:Label>
                       </td>
                       <td>
                           <asp:Label runat="server" ID="lbldrdisc" ></asp:Label>
                       </td>
                   
                       <td>
<asp:Label runat="server" ID="Label6" Font-Bold="true" ForeColor="Red" Text="Cen Disc"></asp:Label>
                       </td>
                       <td>
                           <asp:Label runat="server" ID="lblcendisc" ></asp:Label>
                       </td>
                  
                       <td>
<asp:Label runat="server" ID="Label8" Font-Bold="true" ForeColor="Red" Text="Tot Disc"></asp:Label>
                       </td>
                       <td>
                           <asp:Label runat="server" ID="lbltotdisc" ></asp:Label>
                       </td>
                  
                       <td>
<asp:Label runat="server" ID="Label10" Font-Bold="true" ForeColor="Red" Text="Balance"></asp:Label>
                       </td>
                       <td>
                           <asp:Label runat="server" ID="lblbalance" ></asp:Label>
                       </td>
                   
                       <td>
<asp:Label runat="server" ID="Label13" Font-Bold="true" ForeColor="Red" Text="Final Dr Amt"></asp:Label>
                       </td>
                       <td>
                           <asp:Label runat="server" ID="lblfdramt" ></asp:Label>
                       </td>
                   </tr>

               </table>
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
<asp:HyperLink ID="Hyp_viewPres" runat="server" target="_blank" style="color:White;font-family: MyriadPro,Arial, Helvetica, sans-serif;text-decoration:underline; font-size: small;  font-weight: bold;padding-left:10px;" NavigateUrl='<%# Eval("UploadPrescription") %>'>View Pres</asp:HyperLink>
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

