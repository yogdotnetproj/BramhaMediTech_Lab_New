<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="PrintDescRep.aspx.cs" Inherits="PrintDescRep" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
     <script src="ckeditor/ckeditor.js"></script>
     <div class="col-lg-12">
                                    <div class="form-group">
                                     <table id="Table1" width="100%" runat="server">
                                    <tr>
                                    <td align="center" >
                                    <label>Result</label>
                                   <!--   <cc:HtmlEditor ID="Editor" runat="server" Height="400px" Style="text-indent: 2px"  Width="800px" />-->

                                       <asp:TextBox ID="Editor1" runat="server" Height="2500px" TextMode="MultiLine"></asp:TextBox>
<script type="text/javascript" lang="javascript">  CKEDITOR.replace('<%=Editor1.ClientID%>');</script> 
                                      </td>
                                      </tr>
                                      </table>
                                    </div>
                                 </div> 
</asp:Content>

