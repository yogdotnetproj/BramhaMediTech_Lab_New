 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="DoctorAppoinment_Schedule.aspx.cs" Inherits="DoctorAppoinment_Schedule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Dr Appoinment Schedule Master</h1>
                    <ol class="breadcrumb">
                  
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Dr Appoinment Schedule Master</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                    <asp:dropdownlist id="ddlCenter" runat="server" class="form-control" tabindex="3">
                                                              </asp:dropdownlist>
                                    
                                    </div>
                                </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                   <%-- <asp:TextBox ID="txtMachinename" placeholder="Enter From Time"  runat="server" TabIndex="2" class="form-control" 
                                        ></asp:TextBox>--%>
                                   
                                        <asp:DropDownList ID ="ddlFromTime" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                  <asp:DropDownList ID ="ddlToTime" class="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                </div>
                                 <div class="row">
                                      <div class="col-lg-4">
                                    <div class="form-group">
                    <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon" >
                                                <i class="fa fa-calendar"></i>
                                            </div>
                        
                                        <!--    <input type="text" class="form-control pull-right" id="todate">-->
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                   
                                    </div>
                                </div>
                                      <div class="col-lg-4">
                                    <div class="form-group">
                  <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon" >
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                        <!--    <input type="text" class="form-control pull-right" id="todate">-->
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                   
                                    </div>
                                </div>
                                <div class="col-lg-4 text-center">
                                    <div class="form-group pt25">
                                      
                                         <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" class="btn btn-primary" Text="Click"  TabIndex="2" />
                                       
                                         <asp:Button ID="btnsave" runat="server" OnClick="Button1_Click"  Text="Save"  ValidationGroup="form" class="btn btn-primary" TabIndex="3" />
                                    
                                    </div>
                                </div>

                                 <div class="col-lg-3 text-center">
                                    <div class="form-group pt25">
                                     <asp:Label ID="Label2" runat="server" Font-Bold="true"  ForeColor="Red" Style="position: relative" Text="Label" ></asp:Label>
                                    </div>
                                    </div>

                            </div>
                        </div>
                    </div>
                    <div class="box">
                      <div class="table-responsive" style="width:100%">
                 <asp:GridView ID="RoutinTest_Grid" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False" DataKeyNames="ScheduleId"
                    Width="100%" OnPageIndexChanging="RoutinTest_Grid_PageIndexChanging" OnRowEditing="RoutinTest_Grid_RowEditing"
                    AllowPaging="True" PageSize="100" OnRowDeleting="RoutinTest_Grid_RowDeleting"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   >
                    <Columns>
                        <asp:BoundField DataField="DrName" HeaderText="Dr Name" />
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                          <asp:BoundField DataField="EndDate" HeaderText="End Date" />
                           <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
                          <asp:BoundField DataField="EndTime" HeaderText="End Time" />
                        <asp:CommandField HeaderText="Edit " ShowEditButton="True" EditImageUrl="~/Images0/edit.gif"
                            ButtonType="Image" />
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images0/delete.gif" />
                    </Columns>
                   

                </asp:GridView>
                </div>
                    </div>
                     <div class="box">
                     
                     </div>
                </section>
                <!-- /.content -->
            
    
        
         <script language="javascript" type="text/javascript">
             function OpenReport() {
                 window.open("Reports.aspx");
             }
               </script>
   
    </asp:Content>
