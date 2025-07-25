<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital.master" AutoEventWireup="true" CodeFile="TestFormulasetting.aspx.cs" Inherits="TestFormulasetting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
                <!-- Content Header (Page header) -->
    <section class="d-flex justify-content-between content-header mb-2">
                    <h4>Add / Edit Formula</h4>
                    <ol class="breadcrumb">
                    <li class="breadcrumb-item"><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> 
                    <li class="breadcrumb-item active">Add / Edit Formula</li> 
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                <div class="row mb-3">
                               <div class="col-lg-12 text-center">
                                   
                                     <asp:Button ID="Btn_Add_Dept" runat="server" CssClass="btn btn-light mb-2" 
                                        Text="Add Department" onclick="Btn_Add_Dept_Click"  />
                                        <asp:Button ID="Btn_Add_Test" runat="server" CssClass="btn btn-info mb-2" 
                                        Text="Add Test" onclick="Btn_Add_Test_Click"  />
                                         <asp:Button ID="btnedittest" runat="server" CssClass="btn btn-secondary mb-2" 
                                        Text="Edit Test" onclick="btnedittest_Click"  />
                                         <asp:Button ID="Btn_Add_NR" runat="server" CssClass="btn btn-primary mb-2" 
                                        Text="Add Referance Range" onclick="Btn_Add_NR_Click"  />
                                         <asp:Button ID="Btn_Add_PK" runat="server" CssClass="btn btn-light mb-2" 
                                        Text="Add Package" onclick="Btn_Add_PK_Click"   />
                                          <asp:Button ID="Btn_Add_Sample" runat="server" CssClass="btn btn-info mb-2" 
                                        Text="Add Sample Type" onclick="Btn_Add_Sample_Click"   />
                                        <asp:Button ID="Btn_Add_ShortCut" runat="server" CssClass="btn btn-secondary mb-2" 
                                        Text="Add Short Cut" onclick="Btn_Add_ShortCut_Click"  />
                                         <asp:Button ID="Btn_Add_Formula" runat="server" CssClass="btn btn-primary mb-2" 
                                        Text="Add Formula" onclick="Btn_Add_Formula_Click"    />
                                         <asp:Button ID="Btn_Add_RN" runat="server" CssClass="btn btn-info mb-2" 
                                        Text="Add Report Note" onclick="Btn_Add_RN_Click"  />
                                </div>
                               
                            </div>
                            <div class="row mb-3">
                                <div class="col-lg-12">
                                <asp:Label ID="Label4" runat="server" Text="."  ></asp:Label>
                                </br>
                                </div>
                                  </div>
                    <div class="box">
                       <!-- <div class="box-header with-border">
                            <div class="row pull-right">
                                <div class="col-lg-12 red">
                                    Fields marked with * are mandatory
                                </div>
                            </div>
                        </div>-->
                        <div class="box-body">
                            <div class="row mb-3">
                               <asp:updatepanel id="UpdatePanel3" runat="server">
        <contenttemplate>
<TABLE style="WIDTH: 704px"   cellSpacing=0 cellPadding=2 border=0><TBODY>

<TR  >
<TD vAlign=top align=left colSpan=4 rowSpan=1>
<asp:Label id="Label5" runat="server" ForeColor="#C04000"></asp:Label>&nbsp;
</TD>
</TR>
<TR  >
<TD style="WIDTH: 94px; HEIGHT: 19px"   vAlign=top align=center>&nbsp;</TD>
<TD style="WIDTH: 94px; HEIGHT: 19px; TEXT-ALIGN: left"   vAlign=top align=center><asp:Label id="Label6" runat="server" Font-Bold="True" Text="Tests" ></asp:Label>
</TD>
<TD style="WIDTH: 94px; HEIGHT: 19px; TEXT-ALIGN: left"   vAlign=top align=center>&nbsp;</TD>
<TD style="WIDTH: 180px"   vAlign=top align=left rowSpan=2><asp:UpdatePanel id="UpdatePanel2" runat="server">
<ContentTemplate>
<TABLE style="WIDTH: 100%; HEIGHT: 176px" class="tbl2" cellSpacing=0 cellPadding=0 border=0><TBODY><TR><TD style="TEXT-ALIGN: center"   colSpan=5><asp:Label style="POSITION: static" id="Label2" runat="server" Font-Bold="True" Text=" Formula of Test" __designer:wfdid="w67"></asp:Label></TD></TR><TR><TD style="TEXT-ALIGN: center"   colSpan=5>&nbsp;<asp:TextBox id="txtFirstHalf" runat="server" Width="293px" __designer:wfdid="w68"></asp:TextBox></TD></TR><TR><TD style="TEXT-ALIGN: center"   colSpan=5><asp:Label style="POSITION: static" id="Label3" runat="server" Font-Bold="True" Text="Formula From List" __designer:wfdid="w69"></asp:Label></TD></TR><TR><TD style="TEXT-ALIGN: center"   colSpan=5>&nbsp;<asp:TextBox id="rtSecondHalf" runat="server" Width="271px" Height="123px" __designer:wfdid="w70" OnTextChanged="rtSecondHalf_TextChanged" AutoPostBack="True" TextMode="MultiLine"></asp:TextBox></TD></TR><TR>

<TD style="WIDTH: 100px"  >&nbsp;
<asp:Button   id="btnPlus" onclick="btnPlus_Click" runat="server" Text="+" Width="24px" ToolTip="Plus" Height="24px">
</asp:Button>
</TD>

<TD style="WIDTH: 100px"  >&nbsp;<asp:Button id="btnMinus" onclick="btnMinus_Click" runat="server" Text="-" Width="24px" ToolTip="Minus" Height="24px" ></asp:Button></TD>
<TD style="WIDTH: 100px"  >&nbsp;<asp:Button   id="btnDivide" onclick="btnDivide_Click" runat="server" Text="/" Width="24px" ToolTip="Divide" Height="24px" ></asp:Button></TD>
<TD   colSpan=2>&nbsp;<asp:Button  id="btnOpenbracket" onclick="btnOpenbracket_Click" runat="server"  Width="24px" ToolTip="Open Bracket" Height="24px" ></asp:Button></TD>
</TR>
<TR>
<TD style="WIDTH: 100px; HEIGHT: 24px"  >&nbsp; <asp:Button  id="btnMultiply" onclick="btnMultiply_Click" runat="server" text="*" Width="24px" ToolTip="Multiply" Height="24px" ></asp:Button>
</TD>
<TD style="WIDTH: 100px; HEIGHT: 24px"  >&nbsp;<asp:Button  id="btnPower" onclick="btnPower_Click" runat="server"  Width="24px" ToolTip="Power" Height="24px"></asp:Button>
</TD>
<TD style="WIDTH: 100px; HEIGHT: 24px"  >
</TD>
<TD style="HEIGHT: 24px"   colSpan="2">&nbsp;<asp:Button  id="btnCloseBracket" onclick="btnCloseBracket_Click" runat="server" Width="24px" ToolTip="Close Bracket" Height="24px" ></asp:Button>
</TD>
</TR>
<TR>
<TD style="WIDTH: 100px; HEIGHT: 35px"></TD><TD style="WIDTH: 100px; HEIGHT: 35px"></TD>
<TD style="WIDTH: 100px; HEIGHT: 35px"></TD>
<TD style="WIDTH: 100px; HEIGHT: 35px"></TD>
<TD style="WIDTH: 100px; HEIGHT: 35px"></TD>
</TR>
<TR>
<TD style="WIDTH: 100px"  >
    &nbsp;</TD><TD style="WIDTH: 100px"  >&nbsp;<asp:Button id="btnSave" onclick="btnSave_Click" runat="server" CssClass="btn btn-success"  Text="Save"></asp:Button></TD>
 
 <TD style="WIDTH: 100px"  >
 <asp:Button  id="btncheck" onclick="btncheck_Click" runat="server"  Text="Check" CssClass="btn btn-primary"
          ToolTip="Check" ></asp:Button>
 
 </TD><TD style="WIDTH: 100px"  >&nbsp;<asp:Button id="btnClear" onclick="btnClear_Click" runat="server" CssClass="btn btn-warning" Text="Clear" ></asp:Button></TD>
 
 <TD style="WIDTH: 100px"  >&nbsp;
 
 
 </TD>
 
 </TR><TR><TD style="WIDTH: 100px; HEIGHT: 35px"></TD><TD style="WIDTH: 100px; HEIGHT: 35px"></TD><TD style="WIDTH: 100px; HEIGHT: 35px"></TD><TD style="WIDTH: 100px; HEIGHT: 35px"></TD><TD style="WIDTH: 100px; HEIGHT: 35px"></TD></TR><TR><TD style="WIDTH: 100px"></TD><TD style="WIDTH: 100px"></TD><TD style="WIDTH: 100px"></TD><TD style="WIDTH: 100px"></TD><TD style="WIDTH: 100px"></TD></TR></TBODY></TABLE>
</ContentTemplate>
</asp:UpdatePanel>&nbsp; &nbsp;&nbsp; </TD></TR>
<TR  ><TD style="WIDTH: 94px"   vAlign=top align=left>

 </TD>
<TD style="WIDTH: 94px"   vAlign=top align=left>
 <asp:UpdatePanel id="UpdatePanel6" runat="server" >
 <ContentTemplate>

<asp:TextBox ID="txttests"  runat="server" Width="245px"   tabIndex="3"
                                        AutoPostBack="True" OnTextChanged="txttests_TextChanged"></asp:TextBox><br />
                                    <div style="display: none; overflow: scroll; width: 245px; height: 120px" id="div89">
                                    </div>
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" 
                                        CompletionListElementID="div89" ServiceMethod="FillTests" TargetControlID="txttests"
                                        MinimumPrefixLength="1">
                                    </cc1:AutoCompleteExtender>


</ContentTemplate>
</asp:UpdatePanel>
<asp:Label id="Label1" runat="server" Font-Bold="True" Text="Parameters" ></asp:Label>
<DIV style="BORDER-RIGHT: 2px; BORDER-TOP: 2px; OVERFLOW: scroll; BORDER-LEFT: 2px; WIDTH: 245px; 
BORDER-BOTTOM: 2px; HEIGHT: 250px"><asp:UpdatePanel id="UpdatePanel5" runat="server">
<ContentTemplate>
<asp:CheckBoxList id="chkparamlst" runat="server" Width="206px"  AutoPostBack="True" 
OnSelectedIndexChanged="chkparamlst_SelectedIndexChanged" CssClass="form-check" RepeatLayout="Flow"></asp:CheckBoxList> 
</ContentTemplate>
</asp:UpdatePanel></DIV>

</TD><TD style="WIDTH: 94px"   vAlign=top align=left>

        &nbsp;
</TD></TR></TBODY></TABLE>
</contenttemplate>
    </asp:updatepanel>
   
                            </div>
                        </div>
                         <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" class="table table-responsive table-sm table-bordered" DataKeyNames="ID" EmptyDataText="No Record  Found"
                    HeaderStyle-CssClass="GridHeaderRow" CssClass="GridActiveDataRow1" AlternatingRowStyle-CssClass="GridActiveDataRow2" 
                        OnRowDeleting="GridView1_RowDeleting"
                        Width="100%" OnRowDataBound="GridView1_RowDataBound" PageSize="30" style="text-align: left" OnPageIndexChanging="GridView1_PageIndexChanging"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   >
                        <Columns>
                            <asp:BoundField DataField="STCODE" HeaderText="Test Code" Visible="false" />
                            <asp:BoundField DataField="TestName" HeaderText="Test Name" />
                            <asp:BoundField HeaderText="Formula" DataField="Formula"  /> 
                            <asp:BoundField HeaderText="ID" DataField="ID" Visible="false" /> 
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                          
                       </Columns>
                       

                    </asp:GridView>   
                <asp:label id="Label12" runat="server" visible="False"></asp:label>
                </div>
                        </div>
                    </div>
                    </div>
                </section>
                <!-- /.content -->
           
</asp:Content>

