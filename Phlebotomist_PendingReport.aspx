<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="Phlebotomist_PendingReport.aspx.cs" Inherits="Phlebotomist_PendingReport" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Pending report</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Pending report</li> 
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
                                  
<asp:RadioButtonList ID="RblType" runat="server" CssClass="form-check" RepeatDirection="Horizontal">
    <asp:ListItem Value="1" Selected="True">Pathology</asp:ListItem>
    <asp:ListItem Value="2">Radiology</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div> 
                                                    <div class="col-sm-3">
                                                     <asp:button id="btnList" runat="server"  text="Click" onclick="btnList_Click1" class="btn btn-primary" />

                                                    </div>
                                               
                            </div>
                        </div>
                        
                     
                    </div>
                   
                     <div class="box">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
                   <asp:gridview id="GV_DueReport" runat="server" class="table table-responsive table-sm table-bordered" onrowediting="GV_DueReport_RowEditing" width="100%"
                    onrowdatabound="GV_DueReport_RowDataBound"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False"
                    onpageindexchanging="GV_DueReport_PageIndexChanging" 
                                    onrowcreated="GV_DueReport_RowCreated">
                       <AlternatingRowStyle BackColor="#95deff"></AlternatingRowStyle>
                    <columns>
                        
                                              
                          <asp:BoundField DataField="PatRegID" HeaderText=" Reg no" />
                           <asp:BoundField DataField="Patname" HeaderText=" Patient Name" /> 
                          
                            <asp:BoundField DataField="LabRegMediPro" HeaderText="Inv No" />
                             <asp:BoundField DataField="Drname" HeaderText="Ref Dr" />
                           <asp:BoundField DataField="Maintestname" HeaderText="Test" />
                           
                           
                            <asp:BoundField DataField="CenterCode" HeaderText="Center Name" />
                          <asp:BoundField DataField="Patregdate" HeaderText="Enter Date" />
                         
                            <asp:BoundField DataField="DateDiffA" HeaderText="Pending Time" />
                            
                           
                    </columns>
                   

                </asp:gridview>
            
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                     <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   

                    
                                                   <asp:button id="btnreport" runat="server" OnClientClick="return validate();" 
                                                       text="Report" class="btn btn-info" onclick="btnreport_Click" />

                                </div> 
                            </div>
                        </div>
                </section>
                <!-- /.content -->
            
</asp:Content>

