<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="DailyDetailsReport.aspx.cs" Inherits="DailyDetailsReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
  <section class="content-header">
                    <h1>Daily Details Report </h1>
                    <ol class="breadcrumb">
                    
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Daily Details Report </li>
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
                                <div class="col-lg-6">
                                    <div class="form-group">
                                       <asp:button id="btndailytrans" OnClientClick="return validate();" 
                                            runat="server" 
                                            Text="Details Report" class="btn btn-primary" onclick="btndailytrans_Click" />
                                      
                <asp:dropdownlist id="ddlusername"  Visible="false" runat="server" class="form-control" tabindex="3">
                </asp:dropdownlist>
                                    </div>
                                </div>
                                <div class="col-lg-3" runat="server" visible="false">
                                    <div class="form-group">
                                      
                                      
                <asp:textbox id="txtexpenceName" placeholder="Expence Name" class="form-control" runat="server" >
                            </asp:textbox>
                                    </div>
                                </div>
                               
                                               
                            </div>
                            <div class="row" runat="server" visible="false">
                                 <div class="col-md-12 text-center">
                                   
                                    <asp:button id="btnlist" runat="server" OnClientClick="return validate();" Visible="false" onclick="btnlist_Click" text="Click" class="btn btn-primary" tabindex="3" />
                                           
                    
                                                  
                                </div> 
                            </div>
                        </div>
                       
                    </div>
                    <div class="box" runat="server" visible="false">
                        <div class="box-body">
                            <div class="table-responsive" style="width:100%">
<asp:GridView ID="GV_ExpenceEntry" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
                    Width="99%" OnPageIndexChanging="GV_ExpenceEntry_PageIndexChanging"
                    AllowPaging="True" PageSize="1500"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White" onrowcreated="GV_ExpenceEntry_RowCreated" OnRowDeleting="GV_ExpenceEntry_RowDeleting" OnRowEditing="GV_ExpenceEntry_RowEditing"   >
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                    <Columns>
                        <asp:BoundField DataField="UserName" HeaderText=" Name" />
                         <asp:BoundField DataField="Particular" HeaderText="Expence Name" />
                          <asp:BoundField DataField="ExpenceAmount" HeaderText="Expence Amount " />
                          <asp:BoundField DataField="ExpenceDetails" HeaderText="Expence Details" />
                            <asp:BoundField DataField="ExpenceDate" HeaderText="Expence Date" 
                            DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" />
                       <asp:CommandField ShowEditButton="True" HeaderText="Edit" EditImageUrl="~/Images0/edit.gif"
                                ButtonType="Image" />
                    </Columns>
                   

<HeaderStyle ForeColor="Black"></HeaderStyle>
                   

                </asp:GridView>
                                <table width="100%">
                   <tr>
                       <td align="center">
<asp:Label runat="server" ID="a" Visible="false" Font-Bold="true" ForeColor="Red" Text="Expence Amount :    "></asp:Label>
                            <asp:Label runat="server"  ID="lblcharges" ></asp:Label>
                       </td>
                      
                  
                       
                   </tr>

               </table>
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                </section>
                 <script language="javascript" type="text/javascript">
                     function OpenReport() {
                         window.open("Reports.aspx");
                     } 
               </script>
</asp:Content>

