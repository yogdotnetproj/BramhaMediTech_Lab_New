 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="BillCanceledDetails.aspx.cs" Inherits="BillCanceledDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Bill Cancel details</h1>
                    <ol class="breadcrumb">
                    
                      
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Bill Cancel details </li>
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
                                     
                                      
                 <asp:textbox id="txtname" runat="server" placeholder="Enter Name" class="form-control" tabindex="3">
                </asp:textbox>
                                    </div>
                                </div>
                               
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        
                                      
                 <asp:textbox id="txtregno" runat="server" placeholder="EnterReg No" class="form-control" tabindex="4">
                </asp:textbox>
                                    </div>
                                </div>
                                </div>
                            <div class="row">
                               <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                      
                 <asp:textbox id="txtmobileno" runat="server" placeholder="Enter Mobile No" class="form-control" tabindex="5">
                </asp:textbox>
                                    </div>
                                </div>

                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                      
                <asp:TextBox id="txtCenter" tabIndex="6" runat="server" placeholder="Enter Center" class="form-control" ></asp:TextBox>
                <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 185px; HEIGHT: 100px" id="Div2"></DIV>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="Div2" ServiceMethod="Getcenter" TargetControlID="txtCenter" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                       <div class="col-md-3">

                                       </div>
                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   <asp:button id="btnshow" OnClientClick="return validate();" runat="server" text="Click" onclick="btnshow_Click" class="btn btn-primary" tabindex="7" />
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                       <div class="table-responsive" style="width:100%">
                             <asp:gridview id="GV_BillCancelDet" datakeynames="PID" class="table table-responsive table-sm table-bordered" runat="server" width="100%" autogeneratecolumns="False"
                    allowsorting="True" onrowediting="GV_BillCancelDet_RowEditing" allowpaging="True" onpageindexchanging="GV_BillCancelDet_PageIndexChanging"
                    pagesize="20"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   onrowdeleting="GV_BillCancelDet_RowDeleting" OnRowDataBound="GV_BillCancelDet_RowDataBound">
                   
                    <columns>
                     <asp:BoundField DataField="Patregdate" HeaderText="Date " />
                            <asp:BoundField DataField="PatRegID" HeaderText="Reg No" />
                            <asp:BoundField DataField="Fullname" HeaderText="Name">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Age" HeaderText="Age" />
                              <asp:BoundField DataField="sex" HeaderText="Gender" />
                             <asp:BoundField DataField="Drname" HeaderText="Refer Dr">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="CenterName" HeaderText="Center " />
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
                            <asp:BoundField DataField="AmtPaid" HeaderText="Amount Paid"></asp:BoundField>
                           
                            <asp:BoundField DataField="Discount" HeaderText="Discount"></asp:BoundField>
                             <asp:BoundField DataField="Balance" HeaderText="Balance"></asp:BoundField>
                          
                              <asp:CommandField ButtonType="Button"  FooterStyle-ForeColor="Black"  DeleteText="Receipt" ShowDeleteButton="True">

<FooterStyle ForeColor="Black"></FooterStyle>

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
                                    <asp:HiddenField ID="hdnCollcode" runat="server" Value='<%#Bind("CenterCode")%>' />
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
