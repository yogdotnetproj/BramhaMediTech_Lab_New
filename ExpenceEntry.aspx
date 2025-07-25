 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="ExpenceEntry.aspx.cs" Inherits="ExpenceEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager Id="scriptmanager" runat="server">
    </asp:scriptmanager>
   
            <!-- Content Wrapper. Contains page content -->
       
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Expence Entry</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Expence Entry</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
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

                                <div class="col-sm-6">
                                    <div class="form-group">
                                     
                                        
                                         <asp:TextBox ID="txtparticular" runat="server" class="form-control" placeholder="Particular"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                       
                                       
                                         <asp:textbox id="txtexpenceamount" runat="server" class="form-control" placeholder="Expence Amount">
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
                    AllowPaging="True" PageSize="500" OnRowDeleting="GV_ExpenceEntry_RowDeleting"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   >
                    <Columns>
                        <asp:BoundField DataField="UserName" HeaderText=" Name" />
                         <asp:BoundField DataField="Particular" HeaderText="Expence Name" />
                          <asp:BoundField DataField="ExpenceAmount" HeaderText="Expence Amount " />
                          <asp:BoundField DataField="ExpenceDetails" HeaderText="Expence Details" />
                            <asp:BoundField DataField="ExpenceDate" HeaderText="Expence Date"  DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                        <asp:CommandField HeaderText="Edit " Visible="false" ShowEditButton="True" EditImageUrl="~/Images0/edit.gif"
                            ButtonType="Image" />
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" DeleteImageUrl="~/Images0/delete.gif"
                            ButtonType="Image" />
                    </Columns>
                   

                </asp:GridView>
                                  <table width="100%">
                   <tr>
                       <td>
<asp:Label runat="server" ID="a" Visible="false" Font-Bold="true" ForeColor="Red" Text="Expence Amount"></asp:Label>
                       </td>
                       <td>
                           <asp:Label runat="server" ID="lblcharges" ></asp:Label>
                       </td>
                  
                       
                   </tr>

               </table>
                </div>
                    </div>
                </section>
                <!-- /.content -->
          
            <!-- /.content-wrapper -->
       
        </asp:Content>

