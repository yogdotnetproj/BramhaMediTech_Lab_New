 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="BillforHospital.aspx.cs" Inherits="BillforHospital" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Bill Payment For Hospital</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Bill Payment For Hospital</li>
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
                                           
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  >
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
                                            
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  >
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                                                            
                                         <asp:textbox id="txtregno" runat="server"  placeholder="Enter Reg No" class="form-control"> </asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                                                               
                                        <asp:TextBox id="txtCenter" runat="server"  placeholder="Enter Center" class="form-control"></asp:TextBox>
                                        
<DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 220px; HEIGHT: 100px" id="Div2"></DIV>
<cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" CompletionListElementID="Div2" ServiceMethod="FillCenter" TargetControlID="txtCenter" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                                                               
                                        <asp:dropdownlist id="ddlfyear" runat="server" class="form-control">
                </asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                      
                                         <asp:TextBox ID="txtDoctorName"  placeholder="Enter Doctor Name" runat="server" class="form-control"
                    AutoPostBack="True" ontextchanged="txtDoctorName_TextChanged"
                                                ></asp:TextBox>
                                                         <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Divdoc">
                                                    </div>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                        CompletionListElementID="Divdoc" ServiceMethod="FillDoctor" TargetControlID="txtDoctorName"
                                        MinimumPrefixLength="1">
                                    </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        
                                        <div class="radio1">
                                          
                                             
                                                    <label>
                                                        
                                                                <asp:RadioButtonList ID="Rbltype" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem>Paid</asp:ListItem>
                    <asp:ListItem Selected="True">Unpaid</asp:ListItem>
                </asp:RadioButtonList>
                                                    </label>
                                              
                                             
                                          
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                            <asp:button id="btnshow" OnClientClick="return validate();" runat="server" text="Click"  onclick="btnshow_Click" class="btn btn-primary" />
                                    
                                    <asp:Button ID="btnreport" runat="server" class="btn btn-primary" onclick="btnreport_Click" Text="Report" />
                                
                                </div>
                            </div>
                            <hr style="margin-top:0px"/>
                            <div class="row">
                                        <div class="col-lg-3">
                                    <div class="form-group">
                                      
                                      
                                         <asp:textbox id="txtchequeno" runat="server"  class="form-control" placeholder="Cheque Number"> </asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                           
                                            <asp:textbox id="chequedate"  placeholder="Enter Cheque Date" runat="server" data-date-format="dd/mm/yyyy" class="form-control pull-right">  </asp:textbox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        
                                       
                                         <asp:textbox id="txtbankno" runat="server" class="form-control" placeholder="Bank Name">                </asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-3 pt25">
                                    <div class="form-group">
                                      
                                           <asp:Button ID="btnsave" runat="server" Text="Save"  class="btn btn-primary" onclick="btnsave_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                      
                    </div>
                   
                    <div class="box">
                            <div class="table-responsive" style="width:100%">
                <asp:gridview id="GVBillFHosp" datakeynames="PID" class="table table-responsive table-sm table-bordered" runat="server" width="100%" autogeneratecolumns="False"
                    allowsorting="True"  allowpaging="True" onpageindexchanging="GVBillFHosp_PageIndexChanging"
                    pagesize="25"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"    
                        OnRowDataBound="GVBillFHosp_RowDataBound" 
                        onselectedindexchanged="GVBillFHosp_SelectedIndexChanged">
                   
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                   

                    <columns>
                           <asp:BoundField DataField="Patregdate" HeaderText="Date" />
                            <asp:BoundField DataField="PatRegID" HeaderText="Reg No" />
                             <asp:BoundField DataField="BillNo" HeaderText="BillNo"></asp:BoundField>
                            <asp:BoundField DataField="FirstName" HeaderText="Name">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="CenterName" HeaderText="Center " />
                            <asp:TemplateField HeaderText="Test ">
                                <ItemTemplate>
                                    <asp:Label ID="lbltestname" Text='<%#Bind("Testname")%>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                             <asp:BoundField DataField="TestCharges" HeaderText=" Charges"></asp:BoundField>
                            <asp:BoundField DataField="AmtPaid" HeaderText="Paid Amt"></asp:BoundField>
                            <asp:BoundField DataField="Discount" HeaderText="Discount"></asp:BoundField>
                             <asp:BoundField DataField="Balance" HeaderText="Balance"></asp:BoundField>
                            <asp:BoundField DataField="TaxAmount" HeaderText="Hst Amount"></asp:BoundField>
                            
                            
                           
                           <asp:TemplateField HeaderText="Payment" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtpayment"  Width="57px" MaxLength="5" runat="server" ></asp:TextBox>
                                </ItemTemplate>
                                 <ItemStyle Width="10px" />
                            </asp:TemplateField>

                            <asp:CommandField SelectText="Receipt " ShowSelectButton="True" HeaderText="Receipt" />
                           <asp:TemplateField HeaderText="Is Cash " >
                                <ItemTemplate>                                    
                                    <asp:CheckBox ID="Chkpayment" runat="server" />
                                </ItemTemplate>
                                 <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdntest" runat="server" Value='<%#Bind("Tests")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnCollcode" runat="server" Value='<%#Bind("CenterCode")%>' />
                                     <asp:HiddenField ID="hdn_PID" runat="server" Value='<%#Bind("PID")%>' />
                                      <asp:HiddenField ID="Hdn_BillNo" runat="server" Value='<%#Bind("BillNo")%>' />
                                      <asp:HiddenField ID="Hdn_FID" runat="server" Value='<%#Bind("FID")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                        </columns>
                        


                        

                </asp:gridview>
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
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
