<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="AuthorizeDoctorDailyCount.aspx.cs" Inherits="AuthorizeDoctorDailyCount" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Authorize Doctor Daily Count</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Authorize Doctor Daily Count</li> 
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
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  CssClass="form-control pull-right"  tabindex="1">
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
                                       <asp:TextBox id="txtdoctor" tabIndex=3 runat="server" placeholder="Enter Doctor" 
                                            CssClass="form-control" ontextchanged="txtdoctor_TextChanged"></asp:TextBox>
                                       <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" CompletionListElementID="div" TargetControlID="txtdoctor"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ServiceMethod="FillTest" MinimumPrefixLength="1"></cc1:AutoCompleteExtender>
                                        <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 250px; HEIGHT: 250px" id="div"></DIV>

                                    </div>
                                </div>
                                            <div class="col-sm-3">
                                                 <asp:button id="Button1" OnClientClick="return validate();" runat="server" text="Click" CssClass="btn btn-info" onclick="btnshow_Click"  />
                     <asp:Button ID="btnreport" runat="server" 
                                onclientclick=" return validate();" Text="Report"  CssClass="btn btn-primary" 
                                        onclick="btnreport_Click" />
                                            </div>
                                           
                            </div>
                          
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                  
                            

                    
                                 
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                      <div class="table-responsive" style="width:100%">
                         <div style=" overflow: scroll; width: 1400px; height: 1000px; text-align: left"
                                                        id="Divdoc">
                                                   
                              <asp:gridview id="GVBillFHosp" class="table table-responsive table-sm table-bordered" runat="server" width="100%" autogeneratecolumns="False"
                    allowsorting="True"  allowpaging="True" onpageindexchanging="GVBillFHosp_PageIndexChanging"
                    pagesize="55000"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"    
                        >
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                   

                    <columns>
                     
                    <asp:BoundField DataField="AuthoDate" HeaderText="Autho Date" />
                     <asp:BoundField DataField="DoctorNAme" HeaderText="Doctor Name">
                             
                            </asp:BoundField>
                          
                             <asp:BoundField DataField="Maintestname" HeaderText="Test Name"></asp:BoundField>
                             <asp:BoundField DataField="AuthorizeCount" HeaderText="Authorize Count"></asp:BoundField>  
                        </columns>
                        


                        

<HeaderStyle ForeColor="Black"></HeaderStyle>
                        


                        

                </asp:gridview>
                 </div>
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
               
                </div>
                        </div>
                    </div>
                </section>
</asp:Content>

