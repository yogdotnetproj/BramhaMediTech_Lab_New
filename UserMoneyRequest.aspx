<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="UserMoneyRequest.aspx.cs" Inherits="UserMoneyRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager Id="scriptmanager" runat="server">
    </asp:scriptmanager>
   
            <!-- Content Wrapper. Contains page content -->
       
                <!-- Content Header (Page header) -->
                 <section class="d-flex justify-content-between content-header mb-2">
                    <h4>User Cash Request</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">User Cash Request</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                             <div class="row">
                                 </div>
                            <div class="row mb-1">
                               
                                  <div class="col-sm-3">
                                    <div class="form-group">
                                     
                                        
                                         <asp:Label ID="Label1" runat="server"  Text="Req Date" ></asp:Label>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                     
                                        
                                         <asp:Label ID="Label2" runat="server"  Text="Req From" ></asp:Label>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                     
                                        
                                         <asp:Label ID="Label3" runat="server"  Text="Req To" ></asp:Label>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                     
                                        
                                         <asp:Label ID="Label4" runat="server" Text="Expected Amt" ></asp:Label>
                                    </div>
                                </div>
                                </div>
                             <div class="row mb-3">
                                 <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right" >
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3">
                                    <div class="form-group">
                                     
                                        
                                         <asp:Label ID="txtparticular" runat="server" class="form-control" placeholder="Request From"></asp:Label>
                                    </div>
                                </div>
                                 <div class="col-sm-3">
                                    <div class="form-group">  
                                           <asp:DropDownList id="ddlRequestTo" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       
                                         <asp:textbox id="txtexpenceamount" runat="server" class="form-control" placeholder="Expected Amount">
                </asp:textbox>
            
               
                                    </div>
                                </div>
                                </div>
                            <div class="row mb-3">
                                <div class="col-sm-11">
                                    <div class="form-group">
                                       
                                        
                                           <asp:textbox id="txtparticularDet" runat="server" textmode="MultiLine" placeholder="Particulat details" class="form-control"></asp:textbox>
                                    </div>
                                </div>
                                        <div class="col-sm-1">
                                               
                                    <asp:button id="btnsave" runat="server"  OnClientClick="return Validate();"  
                                        text="Save" tooltip="Save"
                               class="btn btn-success" onclick="btnsave_Click" />
<asp:Label ID="LblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                        </div>
                            </div>
                        </div>
                      
                    </div>
                      <div class="box">
                       <div class="table-responsive" style="width:100%">
                <asp:GridView ID="GV_ExpenceEntry" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                    Width="99%" OnPageIndexChanging="GV_ExpenceEntry_PageIndexChanging"
                    AllowPaging="True" PageSize="15" OnRowDeleting="GV_ExpenceEntry_RowDeleting"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White" OnRowDataBound="GV_ExpenceEntry_RowDataBound"   >
                    <Columns>
                        <asp:BoundField DataField="RequestFrom" HeaderText=" Request From" />
                         <asp:BoundField DataField="RequestTo" HeaderText="Request To" />
                          <asp:BoundField DataField="ExpectedAmt" HeaderText="Expected Amount " />
                          
                            <asp:BoundField DataField="RequestDate" HeaderText="Request Date"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                        <asp:BoundField DataField="Remarks" HeaderText="Details" />
                         <asp:BoundField DataField="RequestApprove" HeaderText="Req Status" />
                         <asp:BoundField DataField="ReceiveAmt" HeaderText="Approve Amt" />
                        <asp:CommandField HeaderText="Edit " Visible="false" ShowEditButton="True" EditImageUrl="~/Images0/edit.gif"
                            ButtonType="Image" />
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" DeleteImageUrl="~/Images0/delete.gif"
                            ButtonType="Image" />
                        
                    </Columns>
                   

                </asp:GridView>
                </div>
                    </div>
                </section>
                <!-- /.content -->
          
            <!-- /.content-wrapper -->
           
         <asp:hiddenfield id="ValueHiddenField" runat="server" value="" />

        </asp:Content>

