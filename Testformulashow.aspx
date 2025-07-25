 <%@ Page Title="Test formulashow" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Testformulashow.aspx.cs" Inherits="Testformulashow" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <link href="App_Themes/Default/GVCss.css" rel="stylesheet" type="text/css" />
<div style="text-align: center">
        <table   border="0" cellpadding="2" cellspacing="0">
            <tr class="PageHeaderRow" >
                <td align="center" colspan="2" valign="middle" style="width: 774px">
                    <asp:Label ID="Label4" runat="server"  style="font-size: xx-large; font-weight: bold; font-style: inherit; color: #02618E;
                                font-variant: normal; text-transform: capitalize; font-family: calibri; text-align: center;" Text=" View Test Formula " ></asp:Label></td>
            </tr>
            <tr  >
                <td align="center" colspan="2" style="width: 774px; text-align: center">
                    <asp:Label ID="Label1" runat="server" ForeColor="#C04000"></asp:Label></td>
            </tr>
            <tr  >
                <td align="left" colspan="2" style="width: 774px; height: 2px; text-align: center"
                    valign="top">
                     <div class="rounded_corners" style="width:100%">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No Record  Found"
                    HeaderStyle-CssClass="GridHeaderRow" CssClass="GridActiveDataRow1" AlternatingRowStyle-CssClass="GridActiveDataRow2" 
                        OnRowDeleting="GridView1_RowDeleting"
                        Width="770px" OnRowDataBound="GridView1_RowDataBound" PageSize="30" style="text-align: left" OnPageIndexChanging="GridView1_PageIndexChanging"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   >
                        <Columns>
                            <asp:BoundField DataField="STCODE" HeaderText="Test Code" Visible="false" />
                            <asp:BoundField DataField="TestName" HeaderText="Test Name" />
                            <asp:BoundField HeaderText="Formula" DataField="Exp"  /> 
                            <asp:BoundField HeaderText="ID" DataField="ID" /> 
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                          
                       </Columns>
                       

                    </asp:GridView>
                    </div>
                    </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 774px; height: 3px; text-align: center"
                    valign="top">
                    <table id="tableButton" runat="server" style="width: 106%">
                        <tr class="tableHeader1">
                            <td align="center" colspan="2" style="height: 26px">
                                &nbsp; &nbsp;&nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;
            <input type="button"  onclick="history.back()" id="Button2" style="height:35px;width:35px;background-image:url(ButtonIcon/back.png);border-color:#18afdf;" />  
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        &nbsp; &nbsp; &nbsp;</div>
</asp:Content>

