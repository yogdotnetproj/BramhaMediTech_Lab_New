<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="CreatePackage.aspx.cs" Inherits="CreatePackage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Package</h1>
                    <ol class="breadcrumb">
                     
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Package</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                               
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                        
                                       <asp:TextBox id="txtPackagename" placeholder="Enter Package Name" tabIndex="1" runat="server" class="form-control"></asp:TextBox>
 <asp:RequiredFieldValidator style="POSITION: relative" id="RequiredFieldValidator2" runat="server" ForeColor="Red" Font-Bold="True" ValidationGroup="form"
 ControlToValidate="txtPackagename" Display="Dynamic" ErrorMessage="This is required field.">
 </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                      
                                       <asp:TextBox ID="txtPackagecode" placeholder="Enter Package Code" tabIndex="2" runat="server" class="form-control"  MaxLength="4" ontextchanged="txtCode_TextChanged" AutoPostBack="true" >
 </asp:TextBox>
 <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" TargetControlID="txtPackagecode"
                    FilterMode="ValidChars" ValidChars="A,B,C,D,E,F,E,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,0,1,2,3,4,5,6,7,8,9">
                </cc1:FilteredTextBoxExtender>
  <asp:RequiredFieldValidator  id="RequiredFieldValidator1" runat="server" ForeColor="Red"   Font-Bold="True" ValidationGroup="form" ControlToValidate="txtPackagecode" Display="Dynamic" ErrorMessage="This is required field."></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                      <asp:UpdatePanel id="UpdatePanel5" runat="server" >
 <ContentTemplate>
                                      <asp:TextBox ID="txttests" placeholder="Select Test"  runat="server" class="form-control" tabIndex="3"
                                        AutoPostBack="True" OnTextChanged="txttests_TextChanged"></asp:TextBox><br />
                                    <div style="display: none; overflow: scroll; width: 245px; height: 120px" id="div89">
                                    </div>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                        CompletionListElementID="div89" ServiceMethod="FillTests"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" TargetControlID="txttests"
                                        MinimumPrefixLength="1">
                                    </cc1:AutoCompleteExtender>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div  >
                                    <table>
                                    <tr>
                                    <td style="VERTICAL-ALIGN: top; WIDTH: 8274px">
                                    </td>
                                    <TD style="VERTICAL-ALIGN: top; WIDTH: 274px" align="left">
                                     <asp:UpdatePanel id="UpdatePanel3" runat="server" >
                                        <ContentTemplate>
                                     <DIV style="BORDER-RIGHT: 2px; BORDER-TOP: 2px; OVERFLOW: scroll;  BORDER-LEFT: 2px; WIDTH: 245px; BORDER-BOTTOM: 2px; HEIGHT: 325px">
                                            <asp:CheckBoxList id="chkselectedtest" runat="server"  tabIndex="4"  Width="444px" >
                                                   </asp:CheckBoxList>
                                             </DIV>
                                     </ContentTemplate>
                            </asp:UpdatePanel>
                                    <td></td>
                                    </tr>
                                    
                                    </table>
                                    
                                     
                                      </div>
                                       <div >
                                       <table>
                                    <tr>
                                    <td style="VERTICAL-ALIGN: top; WIDTH: 8274px">
                                    </td>
                                    <TD style="VERTICAL-ALIGN: top; WIDTH: 274px" align="left">
                                       <asp:TextBox ID="txtrateamt"  runat="server" Visible="false" tabIndex="5"></asp:TextBox>
                                       </TD>
                                       </tr>
                                       </table>
                                       </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                
                                     <asp:Button id="btnsave" onclick="Button2_Click"  runat="server"  class="btn btn-primary"  Text="Save" ValidationGroup="form"></asp:Button>
                                      <asp:Button id="btnBack"  onclick="btnBack_Click" class="btn btn-primary" runat="server" Text="Back" ></asp:Button>
                                      <asp:Label id="Label1" runat="server" ForeColor="Red" Font-Bold="true" ></asp:Label>
                                </div> 
                            </div>
                        </div>
                    </div>
                   
                </section>
                <!-- /.content -->
           
</asp:Content>

