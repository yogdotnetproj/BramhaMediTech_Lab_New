<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="PatientEdit.aspx.cs" Inherits="PatientEdit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
  <section class="d-flex justify-content-between content-header mb-2">
                    <h1>Clinical History Edit</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Clinical History Edit</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-lg-4">
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
                                <div class="col-lg-4">
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
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                       <asp:TextBox  id="txtCentername"  runat="server" class="form-control" placeholder="Center Name"
AutoPostBack="True" OnTextChanged="txtCentername_TextChanged"></asp:TextBox>
 <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 249px; HEIGHT: 100px" id="Div2">
</div>
<cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1"
 TargetControlID="txtCentername" ServiceMethod="GetCenterName" CompletionListElementID="Div2">
                  </cc1:AutoCompleteExtender>  
                                  </div>
                                </div>
                                </div>
                                 <div class="row mb-3">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        
                                         <asp:TextBox id="txtPatientName"  runat="server" class="form-control pull-right" placeholder="Enter Patient Name"
 AutoPostBack="false"></asp:TextBox>  
 
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                      
                                         <asp:textbox id="txtmobileno" runat="server" class="form-control pull-right" placeholder="Enter Mobile Number"></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                      
                                        <asp:textbox id="txtRegNo" runat="server"  class="form-control pull-right" placeholder="Enter Registration Number"></asp:textbox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                     <asp:button id="btnshow" runat="server" class="btn btn-primary" OnClientClick="return validate();"  onclick="btnshow_Click" text="Click"  />
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                          <div class="table-responsive" style="width:100%">
                
                <asp:gridview id="GridEditTest" runat="server" class="table table-responsive table-sm table-bordered" allowsorting="True" autogeneratecolumns="False"
                    datakeynames="PID" width="100%" onselectedindexchanged="GridEditTest_SelectedIndexChanged"
                    allowpaging="True" onpageindexchanging="GridEditTest_PageIndexChanging" pagesize="20"
                  HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  >
                    <columns>
                     <asp:CommandField SelectText="Show " ShowSelectButton="True" HeaderText="Show" />
                    <asp:BoundField DataField="PatRegID" HeaderText="Reg No" />                   
                    <asp:BoundField DataField="Patname" HeaderText="Name" />
                        <asp:BoundField DataField="CenterName" HeaderText="Center Name" />                         
                        <asp:TemplateField HeaderText="Test Name">
                            <ItemTemplate>
                                <asp:Label ID="lbltestname" runat="server" Text='<%#Bind("testname")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TestCharges" HeaderText="Charges" />
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lbltest" runat="server" Text='<%#Bind("Tests")%>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                       
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnFID" runat="server" Value='<%#Bind("FID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </columns>
                   

                </asp:gridview>
                </div>
                    </div>
                </section>
</asp:Content>

