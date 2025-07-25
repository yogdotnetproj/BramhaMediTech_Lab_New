 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReRunResult.aspx.cs" Inherits="ReRunResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <Maintestname>Re Run</Maintestname>
     <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
        <link rel="stylesheet" href="plugins/bootstrap/css/bootstrap.min.css">
        <link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
        <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css">
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
    <div class="col-sm-2"><span class="btn btn-secondary"><strong>Reg # : <asp:Label ID="lblRegNo" runat="server" Text="RegNo"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Name :  <asp:Label ID="lblName" runat="server" Text="Name"  Font-Bold="True" Width="186px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Age :  <asp:Label ID="lblage" runat="server" Text="Age"  Font-Bold="True" Width="51px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Gender :  <asp:Label ID="lblSex" runat="server" Text="Sex"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-12">&nbsp;</div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Mobile : <asp:Label ID="LblMobileno" runat="server" Width="96px" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Center :  <asp:Label ID="Lblcenter" runat="server" Width="175px" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Date : <asp:Label ID="lbldate" runat="server"  Font-Bold="True" Width="142px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Ref Dr. :<asp:Label ID="LblRefDoc" Font-Bold="True" runat="server"  Width="180px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
            <asp:Label ID="Label1" runat="server" Text=" " Width="51px" Font-Bold="True"></asp:Label>
                              
                              <div class="col-lg-12">
                              &nbsp;
                              </div>
        <table width="1000px"    >
          
            <tr>              
                <td >
                
                  <div class="rounded_corners" style="width:100%">
                       <asp:PlaceHolder ID="PlaceHolder1"   runat="server"></asp:PlaceHolder>
                    </div>
                    
                </td>
            </tr>
             <tr>              
                <td >
                <table>
                <tr>
                <td>
                 <asp:Label ID="Label2" Font-Bold="True" runat="server" Text="Re Run Machin"  Width="196px"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlReRun" runat="server">
                    </asp:DropDownList>
                </td>
                </tr>
                <tr>
                <td>
                 <asp:Label ID="Label3" Font-Bold="True" runat="server" Text="Re mark"  Width="196px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Height="45px" Width="527px"></asp:TextBox>
                   
                </td>
                </tr>
                </table>
                 
                </td>
                
                </tr>
            <tr>
               
                <td style="width: 100px; text-align: left">
                    <table style="width: 504px">
                        <tr>
                            <td style="width: 127px">
                    <asp:Button ID="Button1" runat="server" Text="Re Run" class="btn btn-primary" Width="154px" OnClick="Button1_Click" /></td>
                            <td style="width: 100px">
                                <input onclick="javascript:Close();" style="cursor: pointer" class="btn btn-primary"
                        type="button" value="Close" /></td>
                         <td style="width: 927px">
                             <asp:Label ID="Label4" Font-Bold="true" ForeColor="Green" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           
        </table>
    
    </div>
    </form>
</body>
</html>
