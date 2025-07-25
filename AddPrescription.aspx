<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddPrescription.aspx.cs" Inherits="AddPrescription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
 <Maintestname>Add Prescription</Maintestname>
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
         <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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
         <br />
         <br />
         <div class="row" style="padding-bottom: 10px">

                                                            <div style="font-weight: bold; margin-left: 15px;">
                                                                <asp:Label ID="LblbrowsePres" Text="Upload Prescription:" runat="server"></asp:Label>
                                                            </div>

                                                            <br />
                                                            <div class="col-md-6">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                             <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                                                    <ContentTemplate>
                                                                           <asp:FileUpload ID="FUBrowsePresc" runat="server"></asp:FileUpload>
                                                                            <asp:Label ID="LblFilename" Text="" Font-Bold="true" runat="server"></asp:Label>
                                                                        
                                                                         </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnupload" runat="server" Visible="true" Textclass="btn btn-primary" 
                                                                                Text="Upload" OnClick="btnupload_Click" class="btn-blue"></asp:Button>
                                                                            
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </div>
                                                           

                                                        </div>

         <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   <asp:Button ID="btnreceipt" runat="server" Visible="false" Textclass="btn btn-primary" 
                                                                                Text="Receipt"  OnClick="btnreceipt_Click"></asp:Button>
                                    
                     <input onclick="javascript: Close();" style="cursor: pointer" class="btn btn-primary"
                        type="button" value="Close" />
                                   
                                </div> 
                            </div>
                        </div>
    
    </div>
    </form>
</body>
</html>
