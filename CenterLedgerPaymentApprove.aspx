 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="CenterLedgerPaymentApprove.aspx.cs" EnableEventValidation="false" Inherits="CenterLedgerPaymentApprove" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
 
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Center wise Payment Approve</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Center wise Payment Approve</li> 
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
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
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
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                      <asp:dropdownlist id="ddlCenter" runat="server" class="form-control form-select" tabindex="3">
                </asp:dropdownlist>

                                    </div>
                                </div>
                                          <div class="col-sm-3">
                                                  <div class="form-group">
                                                   <asp:dropdownlist id="ddlUserName" runat="server" class="form-control form-select" tabindex="4">
                </asp:dropdownlist>
                                                      </div>
                                          </div>
                                               
                            </div>

                           
                             <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                         <asp:RadioButtonList id="rblApprove" runat="server"  RepeatDirection="Horizontal" >
                                           <asp:ListItem Selected="True" Value="3">All</asp:ListItem>
                                           <asp:ListItem Value="1">Approve</asp:ListItem>
                                           <asp:ListItem Value="0">Not Approve</asp:ListItem>
                                             <asp:ListItem Value="2">Reject</asp:ListItem>
                </asp:RadioButtonList>
                                        </div>
                                    </div>
                                  <div class="col-sm-8">
                                    <div class="form-group">
                                        <asp:Label ID="LblRecAmt" runat="server" ForeColor="Red"></asp:Label>
                                        </div>
                                      </div>
                                 <div class="col-sm-1">
                                    <div class="form-group">
                                        <asp:button id="btnList" runat="server"  text="Click" onclick="btnList_Click1" class="btn btn-primary" />
                                        </div>
                                     </div>
                                
                        </div>
                        <div class="box-footer">
                            <div class="row mb-3">
                                <div class="col-sm-12-center ">
                                   

                    <asp:button id="btnApprove" runat="server"  Text="Approve" OnClientClick="return ValidateDelete();"  class="btn btn-primary" OnClick="btnApprove_Click" />
                                                   
                                     <asp:Label id="lblMsg" Font-Bold="true" ForeColor="Red" runat="server" Text=""></asp:Label>   
                                </div> 
                            </div>
                        </div>
                    </div>
                        </div>
                    <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
          <asp:gridview id="GV_CenterLedger" datakeynames="Id" class="table table-responsive table-sm table-bordered"  runat="server" autogeneratecolumns="False"
                    width="100%"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   onrowdatabound="GV_CenterLedger_RowDataBound1" onpageindexchanging="GV_CenterLedger_PageIndexChanging"
                    pagesize="700" onrowcreated="GV_CenterLedger_RowCreated" OnRowCommand="GV_CenterLedger_RowCommand" >
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                    <columns>                    
                     <asp:BoundField DataField="CenterCode" HeaderText="Center "  />
                     <asp:BoundField DataField="Receivedate" HeaderText="Rec Date" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                     <asp:BoundField DataField="Paymenttype" HeaderText="Payment Type" />
                      <asp:BoundField DataField="Chequeno" HeaderText="Chequeno" Visible="false" />
                      
                     <asp:BoundField DataField="bankname" HeaderText="Bank Name " Visible="false" />
                      <asp:BoundField DataField="chequedate" HeaderText="Cheque Date " Visible="false" />
                        <asp:BoundField DataField="Discount" HeaderText="Discount " />
                         <asp:BoundField DataField="Receiveamount" HeaderText="Rec Amt " />  

                      <asp:BoundField DataField="Username" HeaderText="Received By" />
                       <asp:BoundField DataField="PaymentApprove" HeaderText="Payment Approve" />
                        <asp:BoundField DataField="PaymentApproveBy" HeaderText="Payment Approve By " />
                        <asp:BoundField DataField="PaymenyApproveOn" HeaderText="Paymeny Approve On" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>

                                                                <asp:DropDownList ID="Status"  runat="server">
                                                                    <asp:ListItem Text="Select" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="Accept" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Reject" Value="2"></asp:ListItem>
                                                                   
                                                                </asp:DropDownList>
                                                                
                                                                 <asp:HiddenField ID="hdnIsPaymentApprove" runat="server" Value='<%#Eval("IsPaymentApprove") %>' />
                                                                
                                                                 
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Check">
                                                            <ItemTemplate>
                                                                <asp:CheckBox runat="server" ID="ChkApprove" Checked="true" />
                                                                </ItemTemplate>
                             </asp:TemplateField>
                         <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>

                                                                <asp:TextBox runat="server" Width="150px" TextMode="MultiLine" Text='<%#Eval("Remark") %>' ID="txtApproveRemarks"></asp:TextBox>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:ButtonField   CommandName="Select" HeaderText="Approve"   Text="Approve" ControlStyle-CssClass="btn btn-primary" ButtonType="Button" ItemStyle-Width="80" >                                                    
                                                         <ControlStyle CssClass="btn btn-primary" />
                                                         <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                         </asp:ButtonField>
                                          
                     </columns>
                     

<HeaderStyle ForeColor="Black"></HeaderStyle>
                     

                </asp:gridview>      
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                    <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   

                    
                                                   <asp:button id="btnreport" runat="server" Visible="false" OnClientClick="return validate();" 
                                                       text="Report" class="btn btn-primary" onclick="btnreport_Click" />
                                                       <asp:button id="btnPdf" runat="server" Visible="false" OnClientClick="return validate();" 
                                                       text="Report PDF" class="btn btn-primary" onclick="btnPdf_Click"  />

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
