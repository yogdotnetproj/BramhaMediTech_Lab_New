 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="TAT_Patientwise.aspx.cs" Inherits="TAT_Patientwise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
   
            <!-- Content Wrapper. Contains page content -->
           
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>TAT Report Patient wise</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">TAT Report Patient wise</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                      <div class="box">
                        <div class="box-body">
                            <div class="row mb-3">
                                <div class="col-sm-2">
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
                                <div class="col-sm-2">
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
                                    <div class="form-group d-flex flex-row">
                                       
                                       <asp:TextBox ID="txtname" runat="server" placeholder="Enter Patient Name" AutoPostBack="True" class="form-control pull-right"></asp:TextBox><span style="color:Red";>*</span>
                                                        <div id="Div1" 
                                                            style="display: none; overflow: scroll; width: 190px; height: 100px">
                                                        </div>
                                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" 
                                                             CompletionListElementID="Div1"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" MinimumPrefixLength="3" 
                                                            ServiceMethod="GetPatientName" TargetControlID="txtname">
                                                        </cc1:AutoCompleteExtender>
                                        
                                    </div>
                                </div>
                                          <div class="col-sm-2">
                                                     
                                        <asp:TextBox ID="txtRegNo" runat="server" placeholder="Enter Reg No" class="form-control pull-right" >
                                        </asp:TextBox>
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
                                
                                                 </div>
                            <div class="row mb-3">
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       
                                        <asp:TextBox ID="txttestname" runat="server" placeholder="Enter Test Name" class="form-control pull-right"></asp:TextBox>
                                      <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                          CompletionListElementID="div1" MinimumPrefixLength="1"   CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                          ServiceMethod="GetTestName" TargetControlID="txttestname">
                                      </cc1:AutoCompleteExtender>                               
<div id="div3" style="width: 190px; overflow: scroll;display :none ; height: 250px;"></div>
                                    </div>
                                </div> 
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                      
                                        
                                         <asp:DropDownList ID="ddlFromTime" runat="server" TabIndex="2" class="form-control form-select">
            <asp:ListItem Text="Select From Time" Value="Select Time"></asp:ListItem>
            <asp:ListItem Text="00" Value="00"></asp:ListItem>
            <asp:ListItem Text="01" Value="01"></asp:ListItem>
            <asp:ListItem Text="02" Value="02"></asp:ListItem>
            <asp:ListItem Text="03" Value="03"></asp:ListItem>
            <asp:ListItem Text="04" Value="04"></asp:ListItem>
            <asp:ListItem Text="05" Value="05"></asp:ListItem>
            <asp:ListItem Text="06" Value="06"></asp:ListItem>
            <asp:ListItem Text="07" Value="07"></asp:ListItem>
            <asp:ListItem Text="08" Value="08"></asp:ListItem>
            <asp:ListItem Text="09" Value="09"></asp:ListItem>
            <asp:ListItem Text="10" Value="10"></asp:ListItem>
            <asp:ListItem Text="11" Value="11"></asp:ListItem>
            <asp:ListItem Text="12" Value="12"></asp:ListItem>
            <asp:ListItem Text="13" Value="13"></asp:ListItem>
            <asp:ListItem Text="14" Value="14"></asp:ListItem>
            <asp:ListItem Text="15" Value="15"></asp:ListItem>
            <asp:ListItem Text="16" Value="16"></asp:ListItem>
            <asp:ListItem Text="17" Value="17"></asp:ListItem>
            <asp:ListItem Text="18" Value="18"></asp:ListItem>
            <asp:ListItem Text="19" Value="19"></asp:ListItem>
            <asp:ListItem Text="20" Value="20"></asp:ListItem>
            <asp:ListItem Text="21" Value="21"></asp:ListItem>
            <asp:ListItem Text="22" Value="22"></asp:ListItem>
            <asp:ListItem Text="23" Value="23"></asp:ListItem>
            <asp:ListItem Text="24" Value="24"></asp:ListItem>
       </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                      
                                        
                                       <asp:DropDownList ID="ddlToTime" runat="server" TabIndex="2" class="form-control form-select">
            <asp:ListItem Text="Select To Time" Value="Select Time"></asp:ListItem>
            <asp:ListItem Text="00" Value="00"></asp:ListItem>
            <asp:ListItem Text="01" Value="01"></asp:ListItem>
            <asp:ListItem Text="02" Value="02"></asp:ListItem>
            <asp:ListItem Text="03" Value="03"></asp:ListItem>
            <asp:ListItem Text="04" Value="04"></asp:ListItem>
            <asp:ListItem Text="05" Value="05"></asp:ListItem>
            <asp:ListItem Text="06" Value="06"></asp:ListItem>
            <asp:ListItem Text="07" Value="07"></asp:ListItem>
            <asp:ListItem Text="08" Value="08"></asp:ListItem>
            <asp:ListItem Text="09" Value="09"></asp:ListItem>
            <asp:ListItem Text="10" Value="10"></asp:ListItem>
            <asp:ListItem Text="11" Value="11"></asp:ListItem>
            <asp:ListItem Text="12" Value="12"></asp:ListItem>
            <asp:ListItem Text="13" Value="13"></asp:ListItem>
            <asp:ListItem Text="14" Value="14"></asp:ListItem>
            <asp:ListItem Text="15" Value="15"></asp:ListItem>
            <asp:ListItem Text="16" Value="16"></asp:ListItem>
            <asp:ListItem Text="17" Value="17"></asp:ListItem>
            <asp:ListItem Text="18" Value="18"></asp:ListItem>
            <asp:ListItem Text="19" Value="19"></asp:ListItem>
            <asp:ListItem Text="20" Value="20"></asp:ListItem>
            <asp:ListItem Text="21" Value="21"></asp:ListItem>
            <asp:ListItem Text="22" Value="22"></asp:ListItem>
            <asp:ListItem Text="23" Value="23"></asp:ListItem>
            <asp:ListItem Text="24" Value="24"></asp:ListItem>
       </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 ">
                                                                  <asp:Button ID="btnList" runat="server" class="btn btn-primary" OnClientClick="return validate();" OnClick="btnList_Click"  Text="Click"
                                     />
                                          <asp:Button ID="btnReport" runat="server" OnClick="btnReport_Click" 
                                                OnClientClick="return Validate();"  class="btn btn-success" Text="Report" 
                                                ToolTip="Report" />

                                     <asp:Button ID="btnExcel" runat="server" 
                                                OnClientClick="return Validate();"  class="btn btn-warning" Text="Excel" 
                                                ToolTip="Report" OnClick="btnExcel_Click" />
                                    </div>
                                        </div>
                                         
                                         <!-- ==============================================-->
                                           
                                        
                                        
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
                                                  <asp:BoundField DataField="PatRegID" HeaderText="PatRegID " />
                                                    <asp:BoundField DataField="Patname" HeaderText="Patient Name" />
                                                    <asp:BoundField DataField="Patregdate" HeaderText="Register Date Time" />
                                                    <asp:BoundField DataField="Report_Date" HeaderText="Report Date Time" />
                                                     <asp:BoundField DataField="testname" HeaderText="Test Name" />
                                                      <asp:BoundField DataField="TatDuration" HeaderText="TAT Duration" />
                                                       <asp:BoundField DataField="TatName" HeaderText="" />
                                                          <asp:BoundField DataField="TATDurationTime" HeaderText="Duration " />
                                                           <asp:BoundField DataField="TatStatus" HeaderText="TAT Status " />
                                                      <asp:BoundField DataField="subdeptName" HeaderText="Dept Name" />
                                                </Columns>
                                            </asp:GridView>
                                            
                                       
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
