 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddFutureFormat.aspx.cs" MasterPageFile="~/Hospital.master"  Inherits="AddFutureFormat" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
     <script src="ckeditor/ckeditor.js"></script>

            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Add Future Format</h1>
                    <ol class="breadcrumb">
                      
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Future Format</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                              
                            
                                <div class="col-lg-4">
                                    <div class="form-group">
                                                                            
                                       
                     <asp:textbox id="txtTestcode"  placeholder="Format Name" runat="server" class="form-control">  </asp:textbox>
                    <asp:requiredfieldvalidator id="TestCodeRequiredFieldValidator" runat="server" controltovalidate="txtTestcode"
                        errormessage="Please enter the subdeptName" style="position: relative" validationgroup="Format"
                        font-bold="True">
                    </asp:requiredfieldvalidator>
                                    </div>
                                </div>
                               
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                       
                                          <asp:dropdownlist id="CmbFormatName" runat="server" class="form-control" autopostback="True"  onselectedindexchanged="CmbFormatName_SelectedIndexChanged"> </asp:dropdownlist>
                                    </div>
                                </div>    
                                
                                
                                           
                                            <div class="col-lg-12">
                                    <div class="form-group">
                                     <table id="Table2" width="100%" runat="server">
                                    <tr>
                                    <td align="center">
                                   
                                   <!--   <cc:HtmlEditor ID="Editor" runat="server" Height="400px" Style="text-indent: 2px"  Width="800px" />-->

                                       <asp:TextBox ID="Editor1"  placeholder="Format" runat="server" Height="1500px" TextMode="MultiLine"></asp:TextBox>
<script type="text/javascript" language="javascript">    CKEDITOR.replace('<%=Editor1.ClientID%>');</script> 
                                      </td>
                                      </tr>
                                      </table>
                                    </div>
                                 </div>     
                            </div>
                        </div>
                       
                    </div>
                  
                    <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center"> 
                                 <asp:label style="position: relative" id="lblValidate" runat="server" width="472px"
                        visible="False" skinid="errmsg"></asp:label>  
            
                                    <asp:button id="cmdClear" runat="server" text="Save Format" class="btn btn-primary" onclick="cmdClear_Click"
                                        validationgroup="Format" />
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
              
               <asp:sqldatasource id="sqlFormat" runat="server" connectionstring="<%$ ConnectionStrings:myconnection %>"
        selectcommand="select [Name], Result,STCODE from dfrmst where STCODE=@STCODE and branchid=@branchid order by Name">
        <selectparameters>
            <asp:QueryStringParameter Name="STCODE" QueryStringField="STCODE" />
            <asp:SessionParameter DefaultValue="0" Name="branchid" SessionField="branchid" />
        </selectparameters>
    </asp:sqldatasource>
    <asp:hiddenfield id="hdnSort" runat="server" />
      </asp:Content>
