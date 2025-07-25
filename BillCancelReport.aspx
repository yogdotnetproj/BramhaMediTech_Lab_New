 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="BillCancelReport.aspx.cs"  EnableEventValidation="false" Inherits="BillCancelReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Bill Cancel report</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Bill Cancel report</li>
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
                                        
                                      
                 <asp:textbox id="txtregno" runat="server" placeholder="Enter Reg No" class="form-control" tabindex="3">
                </asp:textbox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                                  <asp:button id="Button1" OnClientClick="return validate();" runat="server" text="Click" class="btn btn-primary" onclick="btnshow_Click" 
                    tabindex="6" />
                     <asp:Button ID="btnreport" runat="server" class="btn btn-primary" onclick="btnreport_Click" 
                    Text="Report" />
                     <asp:Button ID="btnexcelrpt" runat="server" class="btn btn-primary"  
                    Text="Excel Report" onclick="btnexcelrpt_Click" />
                                </div>
                               
                                <div class="col-lg-4" runat="server" visible="false">
                                    <div class="form-group">
                                                                            
                  <asp:TextBox id="txtCenter" tabIndex="6" runat="server" placeholder="Enter Center" class="form-control" ></asp:TextBox>
                <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 185px; HEIGHT: 100px" id="Div1"></DIV>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="Div2" ServiceMethod="Getcenter" TargetControlID="txtCenter" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                               <div class="col-lg-4" runat="server" visible="false">
                                    <div class="form-group">
                                                        
                <asp:dropdownlist id="ddlfyear" runat="server" class="form-control" tabindex="5">
                </asp:dropdownlist>
                                    </div>
                                </div>

                                <div class="col-lg-4">
                                    <div class="form-group">
                                     <label></label>
                                        
                                    </div>
                                </div>

                                               
                            </div>
                        </div>
                        

                    </div>
                    <div class="box">
                        <div class="box-body">
                        <div class="table-responsive" style="width:100%">
                              <asp:gridview id="GVBillFHosp" class="table table-responsive table-sm table-bordered" datakeynames="PID" runat="server" width="100%" autogeneratecolumns="False"
                    allowsorting="True"  allowpaging="True" onpageindexchanging="GVBillFHosp_PageIndexChanging"
                    pagesize="25"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"    
                        OnRowDataBound="GVBillFHosp_RowDataBound" onrowcreated="GVBillFHosp_RowCreated" >
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                   

                    <columns>
                    <asp:BoundField DataField="PatRegID" HeaderText="Reg No" />
                     <asp:BoundField DataField="PatientName" HeaderText="Name">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                           <asp:BoundField DataField="Billdate" HeaderText="Date" 
                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                              <asp:BoundField DataField="tdate" HeaderText="Bill Time" 
                            DataFormatString="{0:T}" HtmlEncode="False" />
                             <asp:BoundField DataField="tdate" HeaderText="Cancel Date" 
                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                            <asp:BoundField DataField="CancelReceiptNo" HeaderText="Cancel No"></asp:BoundField>
                             <asp:BoundField DataField="BillNo" HeaderText="Invoice No"></asp:BoundField>
                            
                            
                         
                           
                             <asp:BoundField DataField="BillAmt" HeaderText=" Amount"></asp:BoundField>
                          
                      <asp:BoundField DataField="DisAmt" HeaderText=" Discount"></asp:BoundField>
                       <asp:BoundField DataField="Taxable" HeaderText=" Taxable"></asp:BoundField>
                             <asp:BoundField DataField="Taxamount" HeaderText=" Tax on Taxable"></asp:BoundField>
                             <asp:BoundField DataField="NetAmount" HeaderText=" Net Amount"></asp:BoundField>
                               <asp:BoundField DataField="Username" HeaderText=" User Name"></asp:BoundField>
                                 <asp:BoundField DataField="Drname" HeaderText=" Doctor"></asp:BoundField>
                           
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnCollcode" runat="server" Value='<%#Bind("CenterCode")%>' />
                                     <asp:HiddenField ID="hdn_PID" runat="server" Value='<%#Bind("PID")%>' />
                                      <asp:HiddenField ID="Hdn_BillNo" runat="server" Value='<%#Bind("BillNo")%>' />
                                      
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                        </columns>
                        


                        

<HeaderStyle ForeColor="Black"></HeaderStyle>
                        


                        

                </asp:gridview>
                <asp:label id="Label12" runat="server" visible="False"></asp:label>

                </div>
                 <table id="Table1" width="100% " runat="server">
                <tr>
                <td>
               Amount:
                </td>
                 <td>
               <asp:label id="LblAmount" runat="server" Text="0"></asp:label>
                </td>
                 <td>
                Discount:
                </td>
                 <td>
                  <asp:label id="Lbldiscount" runat="server" Text=""></asp:label>
                </td>
                 <td>
               Taxable:
                </td>
                  <td>
                    <asp:label id="Lbltaxable" runat="server" Text=""></asp:label>
                </td>
                 <td>
               Tax on Taxable:
                </td>
                 <td>
                    <asp:label id="Lbltaxontaxable" runat="server" Text=""></asp:label>
                </td>
                 <td>
               Net Amount:

                </td>
                 <td>
                    <asp:label id="Lblnetamount" runat="server" Text=""></asp:label>
                </td>

                </tr>
                </table>
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
