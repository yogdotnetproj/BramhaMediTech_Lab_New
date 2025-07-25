 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="TAT.aspx.cs" Inherits="TAT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
   
            <!-- Content Wrapper. Contains page content -->
       
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>TAT Report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">TAT Report</li> 
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
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
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
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                      
                                    

                             <asp:textbox id="txtDepartment" class="form-control" placeholder="Select Department" runat="server" AutoPostBack="false">
                            </asp:textbox>
                              <div style="display: none; overflow: scroll; width: 200px; height: 100px" id="Div2">
                                        </div>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1"
                                            TargetControlID="txtDepartment"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" ServiceMethod="FillDepartment" CompletionListElementID="Div2">
                                        </cc1:AutoCompleteExtender>
                                    </div>
                                </div>
                                          <div class="col-sm-3">
                                               <asp:Button ID="btnList" runat="server" class="btn btn-primary" OnClientClick="return validate();" OnClick="btnList_Click"  Text="Click"
                                     />
                                          </div>
                               
                              
                              
                                                  
                            </div>
                        </div>
                        
                    </div>
                    <div class="box">
                        <div class="box-body">
                        <div class="table-responsive" style="width:100%">
                           
                                            <asp:GridView ID="GV_TAT" class="table table-responsive table-sm table-bordered" runat="server" AllowPaging="True" 
                                                AlternatingRowStyle-BackColor="White" AutoGenerateColumns="False" 
                                                  HeaderStyle-ForeColor="Black"  PageSize="25"
                                                OnPageIndexChanging="GV_TAT_PageIndexChanging" 
                                                    
                                                Style="text-transform: capitalize; position: static" TabIndex="5" Width="100%">
                                                <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:BoundField DataField="subdeptName" HeaderText="Dept Name" />
                                                    <asp:BoundField DataField="testname" HeaderText="Test Name" />
                                                    <asp:BoundField DataField="TatDuration" HeaderText="Tat Duration" />
                                                     <asp:BoundField DataField="TatName" HeaderText="" />
                                                    <asp:BoundField DataField="TotalCount" HeaderText="TotalCount" />
                                                    <asp:BoundField DataField="AboveTAT" HeaderText="TAT Not Achive" />
                                                     <asp:BoundField DataField="BelowTAT" HeaderText="TAT Achive" />
                                                      <asp:BoundField DataField="NotAchivetatPer" HeaderText="TAT Not Achive(%)" />
                                                     <asp:BoundField DataField="AchivetatPer" HeaderText="TAT Achive(%)" />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                            
                                       
                                </div>
                        </div>
                    </div>

                     <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">   
                                   <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" 
                                                OnClientClick="return Validate();"  class="btn btn-success" Text="Report" 
                                                ToolTip="Report" Width="77px" />
                                    <asp:Button ID="btnexcel" runat="server" 
                                                OnClientClick="return Validate();"  class="btn btn-warning" Text="Excel" 
                                                ToolTip="Report" Width="77px" OnClick="btnexcel_Click" />
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
