 <%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Hospital.master" CodeFile="Assignsubdept.aspx.cs" Inherits="Assignsubdept" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
   
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Assign sub dept</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Assign sub dept</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">                              
                               
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        
                                       <asp:DropDownList ID="ddlUsertype" runat="server" CssClass="form-control form-select" Width="300px"
        AutoPostBack="True" onselectedindexchanged="ddlUsertype_SelectedIndexChanged">
                    </asp:DropDownList>
                                         <asp:CheckBox ID="chkAll" CssClass="form-check mt-3" runat="server" Text="All" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged"></asp:CheckBox>
                                    </div>
                                </div>
                                

                                <div>
                                    <table>
                                    <tr>
                                    <td style="VERTICAL-ALIGN: top;  align="left">
                                     <div style="overflow: scroll; width: 548px; height: 520px" id="div">
                                   
                                         
                                         
                                         <asp:CheckBoxList ID="chksubdept" CssClass="form-check" runat="server"></asp:CheckBoxList></div>
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

                 <asp:GridView ID="GV_userrollright" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False" 
                    Width="700px" OnPageIndexChanging="GV_userrollright_PageIndexChanging" 
                    AllowPaging="True" PageSize="30"    HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White" OnRowDeleting="GV_userrollright_RowDeleting1"   >
                    <Columns>
                        <asp:BoundField DataField="subdeptName" HeaderText="sub dept" />
                         <asp:BoundField DataField="DeptCodeID" HeaderText="sub dept ID" />
                        
                         <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images0/delete.gif" />
                    </Columns>
                   

                </asp:GridView>
    
                </div>
                    </div>
                </section>
                <!-- /.content -->
         
            <!-- /.content-wrapper -->
          
            
    </asp:Content>
