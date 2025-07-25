<%@ Page Title="" Language="C#" MasterPageFile="~/UserManagement/UserMaster.master" AutoEventWireup="true" CodeFile="FormMaster.aspx.cs" Inherits="UserManagement_FormMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<head>

    <Maintestname></Maintestname>
       <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ModelpopUp.css" rel="stylesheet" />
</head>
<script language="javascript" src="../Scripts/Functions.js" type="text/javascript"></script>     
         <script language="javascript" type="text/javascript">
             function ValidateDelete() {
                 var Check = confirm('Are you sure you want to delete this record ?')
                 if (Check == true) {
                     return true;
                 }
                 else {
                     return false;
                 }
             }
 </script>  


<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="tt" runat="server">
<ContentTemplate>
                                    <table width="50%" runat="server" id="tblShow" visible="false">
											 <tr> 
												<td colspan="3" style="text-align:center; color:Black;"> <h4> Form Master </h4></td>
											</tr>
												
											<tr runat="server" id="LbMsg"> 
												<td colspan="3" style="text-align:center; color:Black;"> 
                                                    <asp:Label ID="LBLValidation" runat="server"></asp:Label>
                                                </td>
											</tr>
                                           
												
                                            <tr>
												<td class="style1"> <asp:Label ID="lblmenuname" runat="server" Text="Menu Name"></asp:Label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtmenuname" runat="server" class="form-control" 
                                                        placeholder="Menu Name *" Width="178px" ></asp:TextBox>
													 
										  		</td>
											</tr>
                                            
                                            <tr>
												<td class="style1"> <asp:Label ID="lblNavigateURL" runat="server" Text="Navigate URL"></asp:Label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtNavigateURL" runat="server" class="form-control" 
                                                        placeholder="Navigate URL *" Width="178px" ></asp:TextBox>
													 
										  		</td>
											</tr>
											
											
												
											<tr> 
												<td class="style1"> &nbsp;</td>
												<td> &nbsp;</td>
												<td class="submit"> 
                                                  
                                                        <table>
                                                        <tr>
                                                        <td>
                                                        <asp:Button ID="btnsave" runat="server" Text="Save" class="btn btn-primary" 
                                                        onclick="btnsave_Click"/>
                                                        </td>
                                                          <td>
                                                        <asp:Button ID="btncancel" 
                                                        runat="server" Text="Cancel" class="btn btn-primary" onclick="btncancel_Click"/>
                                                        </td>
                                                          <td>
                                                        <asp:Button ID="btnback" runat="server" Text="Back" class="btn btn-primary" 
                                                                  onclick="btnback_Click"/>
                                                        </td>
                                                        </tr>
                                                        
                                                        </table>
                                                </td>
                                                
											</tr>
												
											
										</table>	
                                        
                                        
                                       <table width="100%" runat="server" id="tblDisp">
                                       <tr> 
                                             <td> 
                                            &nbsp;&nbsp;
                                            </td>
                                           
                                            <td colspan="2"  >
                                                <asp:Label ID="LblMsg" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                                            </td>

                                            </tr>
                                       <tr>
                                       <td colspan="4" > 
                                           <asp:GridView ID="GvFormMaster" Width="50%" runat="server" DataKeyNames="MenuID" 
                                               AutoGenerateColumns="False" 
                                               onpageindexchanging="GvFormMaster_PageIndexChanging" 
                                               onrowdeleting="GvFormMaster_RowDeleting" 
                                               onselectedindexchanged="GvFormMaster_SelectedIndexChanged" onrowediting="GvFormMaster_RowEditing" >
                                               <Columns>
                                              <%-- <asp:BoundField DataField="NUMCOUNTRYID" HeaderText="Country Id" />--%>
                                               <asp:CommandField ShowEditButton="True" HeaderText="Edit" Visible="false" />
                                               
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" CssClass="glyphicon-trash" ImageUrl="~/images/edit.png" CommandName="Edit" runat="server" />
                                                    
                                                    </ItemTemplate>
                                                    
                                                    </asp:TemplateField>
                                               <asp:BoundField DataField="MenuName" HeaderText="Menu Name" />
                                               <asp:BoundField DataField="NavigateURL" HeaderText="Navigate URL" >
                                                <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                              <%-- <asp:BoundField DataField="SZDESCRIPTION" HeaderText="Description" />--%>
                                                   
                                                <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" Visible="false" />
                                                
                                <asp:TemplateField HeaderText="Delete" >
                                                    <ItemTemplate  >
                                                        <asp:ImageButton ID="ImageButton1" CssClass="glyphicon-trash"   
                                                            ImageUrl="~/images/delete.png"  OnClientClick="return ValidateDelete();" CommandName="Delete" runat="server" 
                                                            ImageAlign="Middle" />
                                                    
                                                    </ItemTemplate>
                                                    
                                                    </asp:TemplateField>
                                                    <asp:ButtonField Text="View" CommandName="Select" Visible="false" />
                                                    </Columns>
                                           </asp:GridView>
                                            <asp:LinkButton Text="" ID = "lnkFake" runat="server" />
                                                <asp:ModalPopupExtender ID="mp" runat="server" PopupControlID="pnlPopup" 
            TargetControlID="lnkFake" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                                </asp:ModalPopupExtender>
                                                  <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                                    <div class="header">
                                                       Details
                                                    </div>
                                                    <div class="body">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                        <td style = "width:60px">
                                                        <b>MenuName: </b>
                                                        </td>
                                                        <td>
                                                        <asp:Label ID="MenuName" runat="server" />
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                        <td>
                                                        <b>NavigateURL: </b>
                                                        </td>
                                                        <td>
                                                        <asp:Label ID="NavigateURL" runat="server" />
                                                        </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </div>
                                                <div class="footer" align="right">
                                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="button" />
                                                </div>
                                            </asp:Panel>
                                       </td>
                                       </tr>
                                       <tr>
                                       <td>
                                       
                                       </td>
                                       <td>
                                           <asp:Button ID="btnAddNew" runat="server" Text="Add New" class="btn btn-primary"
                                               onclick="btnAddNew_Click" />
                                       </td>
                                       </tr>
                                       </table>
                                       </ContentTemplate>

</asp:UpdatePanel>	

</asp:Content>

