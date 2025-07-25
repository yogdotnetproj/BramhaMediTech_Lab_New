 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTestRemark.aspx.cs" MasterPageFile="~/Hospital.master" Inherits="AddTestRemark" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
   

            <!-- Content Wrapper. Contains page content -->
        
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Test Remark</h1>
                    <ol class="breadcrumb">
                   
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Test Remark</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                      
                                        
                                     
                                          <asp:TextBox ID="txtRemark" runat="server" placeholder="Enter Remark" Height="172px" TextMode="MultiLine" class="form-control" Width="477px"></asp:TextBox>
                                    </div>
                                </div>
                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                  
                                     
                                  
                                 <asp:Button ID="btnsave" runat="server" class="btn btn-primary" OnClick="Button1_Click" Text="Submit"  />                    
                    <input id="Button1" type="button" value="Close" onclick="javascript:window.close();" style="border-color:#18afdf;background-color:Gray;" />
                    
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
            
            <!-- /.content-wrapper -->
            
        <script type="text/javascript">
            $(function () {
                // Replace the <textarea id="editor1"> with a CKEditor
                // instance, using default configuration.
                CKEDITOR.replace('editor');
            })            
        </script>
    </div>
   </asp:Content>
