<%@ Page Title="" Language="C#" MasterPageFile="~/UserManagement/UserMaster.master" AutoEventWireup="true" CodeFile="User Master.aspx.cs" Inherits="User_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 120px;
        }
        .style2
        {
            width: 101px;
        }
    </style>
</asp:Content>
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
    <script src="../js/UserValidation.js" type="text/javascript">

</script>
  <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="tt" runat="server">
<ContentTemplate>
   <table runat="server" width="30%" id="tblCreate" visible="false">
											<tr> 
												<td colspan="3" style="text-align:center; color:Black;"> <h4> User Master </h4></td>
											</tr>
												
										
											<tr runat="server" id="LbMsg" visible="false"> 
												<td colspan="5" style="text-align:center; color:Black;"> 
                                                
                                                 <asp:Label ID="LBLValidation" runat="server"></asp:Label>
                                                </td>
											</tr>
												
										
											<tr>
												<td class="style2">
                                                    <asp:Label ID="Label1" runat="server" Text="Initial"></asp:Label>
                                               
                                                
                                                </td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtinitial" runat="server" class="form-control" 
                                                        placeholder="Initial *" Width="178px" ></asp:TextBox>
												</td>
                                                <td> &nbsp;</td>
                                                <td class="style1"> 
                                                
                                                
                                                 <asp:Label ID="Label2" runat="server" Text=" First Name" Width="130%"></asp:Label>
                                               
                                                </td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtempname" runat="server" class="form-control" 
                                                        placeholder="First Name *" Width="178px" ></asp:TextBox>
													 
										  		</td>
											</tr>
                                            
                                            <tr>
												<td class="style2">  <asp:Label ID="Label3" runat="server" Text="  Middle Name" 
                                                        Width="98%"></asp:Label> </td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtmidname" runat="server" class="form-control" 
                                                        placeholder="Middle Name *" Width="178px" ></asp:TextBox>
												</td>
                                                <td> &nbsp;</td>
											<td class="style1"> <label> Last Name </label> </td>
											<td> &nbsp;</td>
											<td>
                                             
                                            <asp:TextBox ID="txtlastname" runat="server" class="form-control" 
                                                    placeholder="Last Name *" Width="178px" ></asp:TextBox>
												
											</td>

                                            </tr>

											
                                                <tr>
												<td class="style2"> <label> user Name </label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtusername" runat="server" class="form-control" 
                                                        placeholder="User Name *" Width="178px" ></asp:TextBox>
													 
										  		</td>
                                                <td> &nbsp;</td>
                                                <td class="style1"> <label> Password </label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtpassword" runat="server" class="form-control" 
                                                        placeholder="Password *" TextMode="Password" Width="178px" ></asp:TextBox>
													 
										  		</td>
											</tr>
											
                                            <tr>
												<td class="style2"> <label>Confirm Password </label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtconpass" runat="server" class="form-control" 
                                                        placeholder="Confirm Password *" TextMode="Password" Width="178px" ></asp:TextBox>
													 
										  		</td>
                                                <td> &nbsp;</td>
                                                <td class="style1"> <label> Date Of Birth </label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtdob" runat="server" class="form-control" 
                                                        placeholder="Date Of Birth *" Width="178px" ></asp:TextBox>
                                                  
													 <td>
										  		    <asp:ImageButton ID="imgPopup" runat="server" ImageAlign="Bottom" 
                                                        ImageUrl="~/images/calendar.png" />
                                                    <asp:CalendarExtender ID="Calendar1" runat="server" Format="dd/MM/yyyy" PopupButtonID="imgPopup" TargetControlID="txtdob" >
                                                    </asp:CalendarExtender>
													<%-- <asp:CalendarExtender ID="Calendar1" runat="server" Format="dd/MM/yyyy" 
                                                        PopupButtonID="imgPopup" TargetControlID="txtdate">
                                                    </asp:CalendarExtender>--%>
                                                    </td>
										  		</td>
											</tr>
											
                                            <tr>
												<td class="style2"> <label> Gender </label></td>
												<td> &nbsp;</td>
												<td>
                                                    <asp:RadioButtonList ID="RblGender" runat="server" 
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem>Male</asp:ListItem>
                                                        <asp:ListItem>Female</asp:ListItem>
                                                    </asp:RadioButtonList>
													 
										  		</td>
                                                <td> &nbsp;</td>
                                                <td class="style1"> <label> Address </label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtaddress" runat="server" class="form-control" 
                                                        placeholder="Address *" TextMode="MultiLine" Width="178px" ></asp:TextBox>
													 
										  		</td>
											</tr>
											
                                            <tr>
												<td class="style2"> <label> Country </label></td>
												<td> &nbsp;</td>
												<td>
                                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlCountry_SelectedIndexChanged" Width="182px">
                                                    </asp:DropDownList>
												</td>
                                                <td> &nbsp;</td>
                                                <td class="style1"> <label> State </label></td>
												<td> &nbsp;</td>
												<td>
                                                    <asp:DropDownList ID="ddlState" runat="server" Width="182px">
                                                    </asp:DropDownList>
													 
										  		</td>
											</tr>
											
                                            <tr>
												<td class="style2"> <label> City </label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtcity" runat="server" class="form-control" placeholder="City *" 
                                                        Width="178px" ></asp:TextBox>
													 
										  		</td>
                                                <td> &nbsp;</td>
                                                <td class="style1"> <label> Mobile No </label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtmobileno" runat="server" class="form-control" 
                                                        placeholder="Mobile No *" Width="178px" ></asp:TextBox>
													 
										  		</td>
											</tr>
											
                                            <tr>
												<td class="style2"> <label> Telephone No </label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txttelno" runat="server" class="form-control" 
                                                        placeholder="Telephone No *" Width="178px" ></asp:TextBox>
													 
										  		</td>
                                                <td> &nbsp;</td>
                                                <td class="style1"> <label> Email Id </label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtemailid" runat="server" class="form-control" 
                                                        placeholder="Email Id *" Width="178px" ></asp:TextBox>
													 
										  		</td>
											</tr>
											
                                            <tr>
												<td class="style2"> <label> Zip Code </label></td>
												<td> &nbsp;</td>
												<td>
                                                <asp:TextBox ID="txtzipcode" runat="server" class="form-control" 
                                                        placeholder="Zip Code *" Width="178px"></asp:TextBox>
													 
										  		</td>
                                                <td> &nbsp;</td>
                                                <td class="style1"> <label> Branch Name </label></td>
												<td> &nbsp;</td>
												<td>
                                                    <asp:DropDownList ID="txtbranch" runat="server" class="form-control" 
                                                        Width="182px">
                                                    </asp:DropDownList>
                                                	 
										  		</td>
											</tr>
											<tr> <td class="style2"> <h1> </h1> </td></tr>
											<tr> 
												<td colspan="3" align="right"> 
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnsave" runat="server" Text="Save" class="btn btn-primary" onclick="btnsave_Click" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btncancel" runat="server" Text="Cancel" class="btn btn-primary" 
                                                                    onclick="btncancel_Click"/>
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnback" runat="server" Text="Back" class="btn btn-primary" 
                                                                    onclick="btnback_Click" />
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </td>
                                                <%-- <asp:TemplateField HeaderText="Date Of Birth">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="lbldob" runat="server" Text='<%# Eval("DTPDOB") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldob" runat="server" Text='<%# Eval("DTPDOB") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                
											</tr>
												
											
										</table>
                                        
   <table id="tblShow" runat="server" width="30%"  >
											<tr> 
                                             <td> 
                                            &nbsp;&nbsp;
                                            </td>
                                           
                                            <td colspan="2"  >
                                                <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
                                            </td>

                                            </tr>
                                            <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="GVUserMaster" runat="server" AutoGenerateColumns="False" DataKeyNames="SZEMPID" 
                                                    onpageindexchanging="GVUserMaster_PageIndexChanging" 
                                                    onrowdeleting="GVUserMaster_RowDeleting" 
                                                    onselectedindexchanged="GVUserMaster_SelectedIndexChanged" onrowediting="GVUserMaster_RowEditing">
                                                    <Columns>
                                                    <asp:TemplateField HeaderText="Employee Id">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtempid" runat="server" Text='<%# Eval("SZEMPID") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblempid" runat="server" Text='<%# Eval("SZEMPID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="FullName" HeaderText="Employee Name" 
                                                            SortExpression="FullName" />
                                              <%--  <asp:TemplateField HeaderText="Employee Name">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtempname" runat="server" Text='<%# Eval("FullName") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblempname" runat="server" Text='<%# Eval("FullName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                  <asp:BoundField DataField="DTPDOB" HeaderText="Date Of Birth" 
                                                            SortExpression="DTPDOB" DataFormatString="{0:dd/MM/yyyy}" 
                                                            HtmlEncode="False" />
                                               <%-- <asp:TemplateField HeaderText="Date Of Birth">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="lbldob" runat="server" Text='<%# Eval("DTPDOB") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldob" runat="server" Text='<%# Eval("DTPDOB") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Gender">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtgender" runat="server" Text='<%# Eval("SZGENDER") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgender" runat="server" Text='<%# Eval("SZGENDER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="City">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtcity" runat="server" Text='<%# Eval("SZCITY") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcity" runat="server" Text='<%# Eval("SZCITY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Address">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="lbladd" runat="server" Text='<%# Eval("SZADDRESS") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladd" runat="server" Text='<%# Eval("SZADDRESS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtmobileno" runat="server" Text='<%# Eval("NUMMOBILENO") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmobileno" runat="server" Text='<%# Eval("NUMMOBILENO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Id">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtemailid" runat="server" Text='<%# Eval("SZEMAILID") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblemailid" runat="server" Text='<%# Eval("SZEMAILID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branch Name">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtbranchname" runat="server" Text='<%# Eval("SZBRANCHNAME") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbranchid" runat="server" Text='<%# Eval("SZBRANCHNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                               <%-- <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton Text="Select" ID="lnkSelect" runat="server" CommandName="Select" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                
                                                <asp:CommandField ShowDeleteButton="True" Visible="false" />
                                                <asp:TemplateField HeaderText="Delete" >
                                                    <ItemTemplate  >
                                                        <asp:ImageButton ID="ImageButton1" CssClass="glyphicon-trash"   
                                                            ImageUrl="~/images/delete.png"  OnClientClick="return ValidateDelete();" CommandName="Delete" runat="server" 
                                                            ImageAlign="Middle" />
                                                    
                                                    </ItemTemplate>
                                                    
                                                    </asp:TemplateField>
                                                <asp:CommandField ShowEditButton="True" HeaderText="Edit" Visible="false" />
                                                 <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnEdit" CssClass="glyphicon-trash" ImageUrl="~/images/edit.png" CommandName="Edit" runat="server" />
                                                    
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

