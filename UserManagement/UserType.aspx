<%@ Page Title="" Language="C#" MasterPageFile="~/UserManagement/UserMaster.master" AutoEventWireup="true" CodeFile="UserType.aspx.cs" Inherits="UserManagement_UserType" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="../css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/ModelpopUp.css" rel="stylesheet" />
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
    <table runat="server" width="30%" id="tblCreate" visible="false" >
	    <tr> 
		    <td colspan="3" style="text-align:center; color:Black"> <h4> User Type</h4></td>
		</tr>
        <tr runat="server" id="LbMsg" visible="false"> 
		    <td colspan="3" style="text-align:center; color:Black;"> 
                <asp:Label ID="LBLValidation" runat="server"></asp:Label>
            </td>
		</tr>
		<tr> 
		    <td> <asp:Label ID="lblusertype" runat="server" Text="User Type"></asp:Label></td>
			<td> &nbsp;</td>
			<td>
                <asp:TextBox ID="txtusertype" runat="server" class="form-control" 
                                                    placeholder="User Type *" MaxLength="25" 
                    Width="178px" ></asp:TextBox>
			</td>
		</tr>
        <tr> 
		    <td> <asp:Label ID="lbldescription" runat="server" Text="Description"></asp:Label></td>
			<td> &nbsp;</td>
			<td>
                <asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" class="form-control" placeholder="Description *" ></asp:TextBox>
			</td>
		</tr>
		<tr> 
		    <td> &nbsp;</td>
			<td> &nbsp;</td>
			<td class="submit"> 
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary" 
                            onclick="btnSave_Click" />
                                                </td>
                                                <td>
                                             &nbsp;
                                                </td>
                                                <td>
                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" class="btn btn-primary" 
                                                        onclick="btncancel_Click" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                <asp:Button ID="BtnBack" runat="server" Text="Back" class="btn btn-primary" 
                                                        onclick="BtnBack_Click"/>
                                                </td>
                                                </tr>
                                                
                                                </table>
                                                </td>
                                                
											</tr>
												
										</table>


                                        
                                        
                               <table id="tblShow" runat="server" width="30%"  >
											<tr> 
                                             <td> 
                                            &nbsp;&nbsp;
                                            </td>
                                           
                                            <td colspan="2"  >
                                                <asp:Label ID="LblMsg" Font-Bold="true" ForeColor="Green" runat="server" Text=""></asp:Label>
                                            </td>

                                            </tr>
                                            <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="GVUserType" runat="server" AutoGenerateColumns="False" DataKeyNames="USERID" 
                                                   
                                                    onrowdeleting="GVUserType_RowDeleting" 
                                                    onrowediting="GVUserType_RowEditing" 
                                                    onpageindexchanging="GVUserType_PageIndexChanging">
                                                     <Columns>
                                                    
                                                     <asp:CommandField ShowEditButton="True" HeaderText="Edit" Visible="false"  />
                                                     
                                                    <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" CssClass="glyphicon-trash" ImageUrl="~/images/edit.png" CommandName="Edit" runat="server" />
                                                    
                                                    </ItemTemplate>
                                                    
                                                    </asp:TemplateField>
                                                   <%--  <asp:BoundField DataField="USERID" HeaderText="User Id" />--%>
                                                     <asp:BoundField DataField="USERTYPE" HeaderText="User Type" />
                                                     <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" />
                                                      <asp:CommandField ShowDeleteButton="True" Visible="false" HeaderText="Delete" />
                                                      <asp:TemplateField HeaderText="Delete" >
                                                    <ItemTemplate  >
                                                        <asp:ImageButton ID="ImageButton1" CssClass="glyphicon-trash"   
                                                            ImageUrl="~/images/delete.png"  OnClientClick="return ValidateDelete();" CommandName="Delete" runat="server" 
                                                            ImageAlign="Middle" />
                                                    
                                                    </ItemTemplate>
                                                    
                                                    </asp:TemplateField>
                                                     </Columns>
                                                </asp:GridView>
                                            </td>
                                            </tr>
                                            <tr>
                                            <td> 
                                            &nbsp;&nbsp;
                                            </td>
                                            <td colspan="2">
                                              <asp:Button ID="btnAddnew" runat="server" Text="Add New" class="btn btn-primary" 
                                                    onclick="btnAddnew_Click"/>
                                            </td>
                                            </tr>
                                            </table>
                                        		
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

