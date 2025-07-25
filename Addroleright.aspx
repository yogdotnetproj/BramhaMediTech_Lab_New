 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Addroleright.aspx.cs" Inherits="Addroleright" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>

            <!-- Content Wrapper. Contains page content -->
          
                <!-- Content Header (Page header) -->
                 <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Role Right</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Role Right</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">                               
                               
                                <div class="col-lg-12">
                                    <div class="form-group d-flex">
                                        <label class="m-r-15">User Type</label>
                                       <asp:DropDownList ID="ddlUsertype" runat="server" CssClass="form-control form-select" Width="300px"
        AutoPostBack="True" onselectedindexchanged="ddlUsertype_SelectedIndexChanged">
                    </asp:DropDownList>
                                    </div>
                                </div>
                               
                                <div class="col-lg-12">
                                    <div class="form-group d-flex">
                                        <label class="m-r-15">All</label>
                                       <asp:CheckBox ID="chkall" runat="server" CssClass="form-check"
                                            oncheckedchanged="chkall_CheckedChanged"></asp:CheckBox>
                                    </div>
                                </div>
                                <div >
                                    <table>
                                    <tr>
                                    <td style="VERTICAL-ALIGN: top;  align="left">
                                     <div style="overflow: scroll; width: 548px; height: 520px" id="div">
                                   
<asp:TreeView ID="TR_RoleRight"  runat="server"   ExpandDepth="1"   
                onselectednodechanged="TR_RoleRight_SelectedNodeChanged" ShowCheckBoxes="Leaf" BackColor="AliceBlue" >
                <HoverNodeStyle Font-Underline="true" />
                <NodeStyle Font-Names ="Tahoma" Font-Size="8pt" ForeColor="Black"   HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" >
                </NodeStyle>
                <ParentNodeStyle Font-Bold="true" />
                <SelectedNodeStyle BackColor="#A1DCF2" ForeColor="#3AC0F2"  Font-Underline="false" HorizontalPadding="0px" 
                VerticalPadding="0px" />
                
                </asp:TreeView>
                 </div>
                                    </td>
                                    <TD style="VERTICAL-ALIGN: top; WIDTH: 274px" align="Right">
                                    
                            </TD>
                                    <td></td>
                                    </tr>
                                    
                                    </table>
                                    
                                     
                                      </div>
                                      
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                
                                   
                                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn btn-success" onclick="btnsave_Click" />                       
                                             </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                          <div class="table-responsive" style="width:100%">
                 <asp:GridView ID="GV_userrollright" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False" DataKeyNames="Rightid"
                    Width="700px" OnPageIndexChanging="GV_userrollright_PageIndexChanging" 
                    AllowPaging="True" PageSize="30"    HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White" onrowdeleting="GV_userrollright_RowDeleting"   >
                    <Columns>
                        <asp:BoundField DataField="ROLENAME" HeaderText="User Type" />
                         <asp:BoundField DataField="MenuName" HeaderText="Menu Name" />
                         <asp:BoundField DataField="FormName" HeaderText="Form Name" />
                         <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:Button ID="btndelete" CommandName="Delete" runat="server" ForeColor="Blue" Text="Delete" />                            
                            </ItemTemplate>                            
                            </asp:TemplateField>
                    </Columns>
                   

                </asp:GridView>
    
                </div>
                    </div>
                </section>
                <!-- /.content -->
            
            <!-- /.content-wrapper -->
           
        
    </asp:Content>
