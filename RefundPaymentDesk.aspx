 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="RefundPaymentDesk.aspx.cs" Inherits="RefundPaymentDesk" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    

            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Refund Bill Desk</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Refund Bill Desk </li>
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
                                         <asp:TextBox id="txtCenter" placeholder="Enter Center"  runat="server" class="form-control pull-right"></asp:TextBox>
                                    </div>
                                </div>
                                
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                      
                                        <asp:textbox id="txtname" runat="server" placeholder="Enter Patient Name" class="form-control pull-right" >     </asp:textbox>
                                    </div>
                                </div>
                                     </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        
                                      <asp:textbox id="txtmobileno" placeholder="Enter Mobile Number" runat="server" class="form-control pull-right" >   </asp:textbox>
                                    </div>
                                </div> 
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        
                                       
                                        <asp:textbox id="txtregno" runat="server" placeholder="Enter Reg number"  class="form-control pull-right">  </asp:textbox>
                                    </div>
                                </div>   
                                <div class="col-md-3">
                                            <asp:button id="btnshow" OnClientClick="return validate();" runat="server" class="btn btn-primary" text="Click" onclick="btnshow_Click" 
                    tabindex="6" />
                                </div>                 
                            </div>
                     
                     
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
                <asp:gridview id="GV_Billdesk" datakeynames="PID" class="table table-responsive table-sm table-bordered" runat="server" width="100%" autogeneratecolumns="False"
                    allowsorting="True" onrowediting="GV_Billdesk_RowEditing" allowpaging="True" onpageindexchanging="GV_Billdesk_PageIndexChanging"
                    pagesize="20"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   onrowdeleting="GV_Billdesk_RowDeleting" OnRowDataBound="GV_Billdesk_RowDataBound">
                   
                    <columns>
                     <asp:BoundField DataField="Patregdate" HeaderText="Date " />
                            <asp:BoundField DataField="PatRegID" HeaderText="Reg No" />
                            <asp:BoundField DataField="Fullname" HeaderText="Name">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Age" HeaderText="Age" />
                              <asp:BoundField DataField="sex" HeaderText="Gender" />
                             <asp:BoundField DataField="DrName" HeaderText="Refer Dr">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="Centername" HeaderText="Center " />
                            <asp:TemplateField HeaderText="Test ">
                                <ItemTemplate>
                                    <asp:Label ID="lbltestname" Text='<%#Bind("Testname")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TestCharges" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblTestCharges" Text='<%#Bind("TestCharges")%>' runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:BoundField DataField="TestCharges" HeaderText=" Charges"></asp:BoundField>
                              <asp:BoundField DataField="TaxPer" HeaderText=" Tax per"></asp:BoundField>
                              <asp:BoundField DataField="TaxAmount" HeaderText=" Tax Amt"></asp:BoundField>
                            <asp:BoundField DataField="AmtPaid" HeaderText="Amount Paid"></asp:BoundField>
                           
                            <asp:BoundField DataField="Discount" HeaderText="Discount"></asp:BoundField>
                             <asp:BoundField DataField="Balance" HeaderText="Balance"></asp:BoundField>
                           <asp:BoundField DataField="IsRefund" HeaderText="IsRefund"></asp:BoundField>
                            <asp:CommandField ButtonType="Button" FooterStyle-ForeColor="Black"  EditText="Pay" ShowEditButton="True">
                                <ItemStyle Width="50px" />
                                <ControlStyle Width="50px" />
                            </asp:CommandField>
                             <asp:CommandField ButtonType="Button" FooterStyle-ForeColor="Black"  DeleteText="Receipt" ShowDeleteButton="True">
                                <ItemStyle Width="70px" />
                                <ControlStyle Width="70px" />
                            </asp:CommandField>
                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdntest" runat="server" Value='<%#Bind("Tests")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnCencode" runat="server" Value='<%#Bind("Centercode")%>' />
                                     <asp:HiddenField ID="hdn_IsRefund" runat="server" Value='<%#Bind("IsRefund")%>' />
                                      <asp:HiddenField ID="Hdnfid" runat="server" Value='<%#Bind("FID")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                        </columns>
                        

                </asp:gridview>
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

