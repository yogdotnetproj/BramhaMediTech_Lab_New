 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="DoctorIPReport.aspx.cs" Inherits="DoctorIPReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    
            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Doctor IP Report</h1>
                    <ol class="breadcrumb">
                 
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Doctor IP Report</li>
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
                                         
                              <asp:textbox id="txtregno" runat="server" placeholder="Enter Reg no" class="form-control">
                </asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                      
                                        <asp:TextBox id="txtCenter" placeholder="Enter Center Name"  runat="server" class="form-control"></asp:TextBox>
                <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 220px; HEIGHT: 100px" id="Div2"></DIV>
                <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" CompletionListElementID="Div2" ServiceMethod="GetCenterName" TargetControlID="txtCenter" MinimumPrefixLength="1">
            </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        
                                        <asp:DropDownList ID="ddlfyear" runat="server"  class="form-control">
                                </asp:DropDownList>
                                    </div>
                                </div> 
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        
                                       
                                          <asp:TextBox ID="txtDoctorName" placeholder="Enter Dcotor Name"  runat="server"  class="form-control"
                                                        
                    ontextchanged="txtDoctorName_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                         <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Divdoc">
                                                    </div>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                        CompletionListElementID="Divdoc" ServiceMethod="FillDoctor" TargetControlID="txtDoctorName"
                                        MinimumPrefixLength="1">
                                    </cc1:AutoCompleteExtender>
                                    </div>
                                </div>  
                                <div class="col-md-3">
                                                   <asp:Button ID="btnshow" runat="server" OnClientClick="return validate();" class="btn btn-primary" OnClick="btnshow_Click"
                    Text="Click" TabIndex="7" />
                                      <asp:Button ID="btnreport" runat="server" OnClick="btnreport_Click" 
                                                OnClientClick="return Validate();"  class="btn btn-primary" Text="Report" 
                                                ToolTip="Report" Width="63px" />
                                </div>                  
                            </div>
                        </div>
                        
                    </div>
                    <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
           <asp:gridview id="GridView1" class="table table-responsive table-sm table-bordered" datakeynames="PID" runat="server" width="100%" autogeneratecolumns="False"
                    allowsorting="True"  allowpaging="True" onpageindexchanging="GridView1_PageIndexChanging"
                    pagesize="20"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"    OnRowDataBound="GridView1_RowDataBound">
                   
                    <columns>
                     <asp:BoundField DataField="Patregdate" HeaderText="Date " />
                    
                            <asp:BoundField DataField="RegNo" HeaderText="Reg No" />
                         
                            <asp:BoundField DataField="Patname" HeaderText="Name">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                           
                            <asp:BoundField DataField="CenterName" HeaderText="Center " />
                               <asp:BoundField DataField="DocName" HeaderText="Ref Doc"/>
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
                            <asp:BoundField DataField="TestCharges" HeaderText="Total Charges"></asp:BoundField>
                            <asp:BoundField DataField="Discount" HeaderText="Discount"></asp:BoundField>
                            <asp:BoundField DataField="AmtPaid" HeaderText="Amount Paid"></asp:BoundField>
                            <asp:BoundField DataField="Balance" HeaderText="Balance"></asp:BoundField>
                            
                            
                            
                        
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdntest" runat="server" Value='<%#Bind("Tests")%>' />
                                </ItemTemplate>
                                <ItemStyle Width="0px" />
                            </asp:TemplateField>
                             <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnCollcode" runat="server" Value='<%#Bind("CenterCode")%>' />
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