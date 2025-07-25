<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="DPRReport.aspx.cs" Inherits="DPRReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <Triggers>
              <asp:PostBackTrigger ControlID="btndailytrans" />
           </Triggers>
        <ContentTemplate>
                <!-- Content Header (Page header) -->
               
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>DPR Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">DPR Report</li> 
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
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
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
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="form-group">
                                        
                                      
                <asp:dropdownlist id="ddlusername" runat="server" Visible="false" CssClass="form-control form-select" tabindex="3">
                </asp:dropdownlist>
                <asp:dropdownlist id="ddlCenter" runat="server" CssClass="form-control form-select" tabindex="3">
                </asp:dropdownlist>
                                    </div>
                                </div>
                               
                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                    <asp:RadioButtonList ID="RblCashType" Visible="false" runat="server" 
                                        RepeatDirection="Horizontal" CssClass="form-check">
                                       <asp:ListItem>Cash</asp:ListItem>
                                       <asp:ListItem>Card</asp:ListItem>
                                      <asp:ListItem>Online Gateway</asp:ListItem>
                                         <asp:ListItem>Cheque</asp:ListItem>
                                        
                                       <asp:ListItem Selected="True">All</asp:ListItem>
                                        
                                    </asp:RadioButtonList>
                                    <asp:button id="btnlist" runat="server" OnClientClick="return validate();" onclick="btnlist_Click" text="Click" CssClass="btn btn-primary" tabindex="3" />
                                            <asp:button id="btndailytrans" OnClientClick="return validate();" 
                                            runat="server" 
                                            Text="Daily Transaction Report" CssClass="btn btn-info" onclick="btndailytrans_Click" />
                    
                                                  
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
        <asp:gridview id="GV_DprReport" class="table table-responsive table-sm table-bordered" runat="server" width="100%" onrowdatabound="GV_DprReport_RowDataBound"
                    onrowediting="GV_DprReport_RowEditing"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False" PageSize="25"
                    allowpaging="True" onpageindexchanging="GV_DprReport_PageIndexChanging">
  <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                    <columns>
                           
                            <asp:BoundField DataField="RecDate" HeaderText="Date" />
                              <asp:BoundField DataField="PatRegID" HeaderText="RegNo" >
                               <ItemStyle Width="20px" />
                            </asp:BoundField>
                               <asp:BoundField DataField="Patname" HeaderText=" Name" />
                           
                         
                           
                          
                            
                            <asp:BoundField DataField="Patphoneno" HeaderText="Phone No" />
                            
                             <asp:BoundField DataField="BillNo" HeaderText="Bill No" />
                              <asp:BoundField DataField="NetPayment" HeaderText="Bill Amount" />
                              <asp:BoundField DataField="Discount" HeaderText="Discount" />
                               <asp:BoundField DataField="TaxAmount" Visible="false" HeaderText="Tax Amount" />
                               <asp:BoundField DataField="AmtPaid" HeaderText="Amt Received" />
                               <asp:BoundField DataField="Mainbalance" HeaderText="Main Balance" />
                               <asp:BoundField DataField="BTHbalance" Visible="false" HeaderText="BTH Balance" />
                                <asp:BoundField DataField="username" HeaderText="UserName" />
                        </columns>
                       
<FooterStyle BackColor="White" ForeColor="#000066" />
                            <HeaderStyle BackColor="#38C8DD" Font-Bold="True" ForeColor="White" />
                            <PagerStyle CssClass="pagination" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                            <RowStyle ForeColor="#000066" />
                            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#007DBB" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#00547E" />
                       

                </asp:gridview>  
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
          
                <!-- /.content -->
                 <script language="javascript" type="text/javascript">
                     function OpenReport() {
                         window.open("Reports.aspx");
                     } 
               </script>
            </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>

