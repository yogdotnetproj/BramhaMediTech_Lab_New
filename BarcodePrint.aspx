 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarcodePrint.aspx.cs" Title="Barcode Print" Inherits="BarcodePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <Maintestname>Barcode Print</Maintestname>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
        <link rel="stylesheet" href="plugins/bootstrap/css/bootstrap.min.css">
        <link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
        <link rel="stylesheet" href="plugins/theme/css/theme.min.css">
        <link rel="stylesheet" href="plugins/theme/css/skins/_all-skins.min.css">
        <link rel="stylesheet" href="css/style.css">
 <link href="App_Themes/Default/GVCss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
      <script language="javascript" type="text/javascript">
          function Close() {
              window.close();
          }    
</script>
  <script language="javascript" type="text/javascript">
      function OpenReport() {
          window.open("Reports.aspx");
      }    
   
   </script>
    <div>
    <br />

        <table width="1000px"    >
           <tr  >
            <td style="height: 19px">
            <div class="rounded_corners" style="width:100%">
                <table   cellpadding="2" cellspacing="0"  style="width: 1000px; border-right: threeddarkshadow 2px solid;
                    border-top: threeddarkshadow 2px solid; border-left: threeddarkshadow 2px solid;
                    border-bottom: threeddarkshadow 2px solid;">
                    <tr >
                        <td align="left" style="width:90px" valign="top">
                            <asp:Label ID="Label1" runat="server" Text="Reg. Number :" Width="114px" 
                                Font-Bold="True"></asp:Label></td>
                        <td align="left" style="width: 91px" valign="top">
                            <asp:Label ID="lblRegNo" runat="server" Text="RegNo"  Font-Bold="True" Width="70px"></asp:Label></td>
                        <td align="right" style="width: 79px" valign="top">
                            <asp:Label ID="Label6" runat="server" Text="Name :" Width="55px" Font-Bold="True"></asp:Label></td>
                        <td align="left" style="width: 100px" valign="top">
                            <asp:Label ID="lblName" runat="server" Text="Name"  Font-Bold="True" Width="186px"></asp:Label></td>
                        <td align="right" style="width: 84px" valign="top">
                            <asp:Label ID="Label8" runat="server" Text="Age :" Width="51px" Font-Bold="True"></asp:Label></td>
                        <td align="left" style="width: 90px" valign="top">
                            <asp:Label ID="lblage" runat="server" Text="Age"  Font-Bold="True" Width="51px"></asp:Label></td>
                        <td align="right" style="width: 101px" valign="top">
                            <asp:Label ID="Label9" runat="server" Text="Gender :" Width="63px" 
                                Font-Bold="True"></asp:Label></td>
                        <td align="left" style="width: 100px" valign="top">
                            <asp:Label ID="lblSex" runat="server" Text="Sex"  Font-Bold="True" Width="70px"></asp:Label></td>
                    </tr>
                    <tr  >
                        <td align="left" style="width: 91px" valign="top">
                            <asp:Label ID="Label45" runat="server" Text="Mobile Number :" Width="139px" 
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td align="left" style="width: 91px" valign="top">
                        <asp:Label ID="LblMobileno" runat="server" Width="96px" 
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td align="right" style="width: 79px" valign="top">
                            <asp:Label ID="Label46" runat="server" Text="Center :" Width="90px" 
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td align="left" style="width: 100px" valign="top">
                       <asp:Label ID="Lblcenter" runat="server" Width="175px" 
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td align="right" style="width: 84px" valign="top">
                            <asp:Label ID="lblIOPD" Font-Bold="True" runat="server" Text="Date:"  Width="51px"></asp:Label></td>
                        <td align="left" style="width: 90px" valign="top">
                            <asp:Label ID="Label13" runat="server"  Font-Bold="True" Width="142px"></asp:Label></td>
                        <td align="right" style="width: 101px" valign="top">
                        <asp:Label ID="Label14" Font-Bold="True" runat="server" Text="Ref Doc:"  
                                Width="66px"></asp:Label>
                        </td>
                        <td align="left" style="width: 100px" valign="top">
                        <asp:Label ID="LblRefDoc" Font-Bold="True" runat="server"  Width="196px"></asp:Label>
                        </td>
                    </tr>
                  
                </table>
                </div>
            </td>
        </tr>
            <tr>              
                <td >
                <center>
                  <div class="table-responsive" style="width:100%">
                    <asp:GridView ID="GVBarcode" class="table table-responsive table-sm table-bordered" runat="server" AutoGenerateColumns="False" Width="100%" 
                    CellPadding="4" ForeColor="#333333" GridLines="Both" DataKeyNames="Barcodeid"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   AllowPaging="True" 
                     PageSize="30" >
                    <Columns>
                      <asp:TemplateField HeaderText="Print"><ItemTemplate>
                     <asp:CheckBox ID="chkflag" CheckState="Indeterminate" runat="server" ToolTip="Check for print this sample"/>
                    </ItemTemplate></asp:TemplateField>
                    <asp:BoundField DataField="Barcodeid" HeaderText="Barcode" />
                    <asp:BoundField DataField="STCODE" HeaderText="ST Codes" />
                    <asp:BoundField DataField="TestNames" HeaderText="Test Names" />
                       
                      
                    </Columns>
                        
                    </asp:GridView>
                    </div>
                    </center>
                </td>
            </tr>
            <tr>
               
                <td style="width: 100px; text-align: left">
                    <table style="width: 304px">
                        <tr>
                            <td style="width: 127px">
                    <asp:Button ID="Button1" runat="server" Text="Print" class="btn btn-primary" Width="154px" OnClick="Button1_Click" /></td>
                            <td style="width: 100px">
                                <input onclick="javascript:Close();" style="cursor: pointer" class="btn btn-primary"
                        type="button" value="Close" /></td>
                         <td style="width: 127px">
                    <asp:Button ID="Button2" runat="server" Text="Print Deptwise" class="btn btn-primary" 
                                 Width="154px" onclick="Button2_Click" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
           
        </table>
    
    </div>
    
    </form>
</body>
</html>
