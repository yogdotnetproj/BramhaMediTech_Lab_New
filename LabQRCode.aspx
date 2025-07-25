<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabQRCode.aspx.cs" Inherits="LabQRCode" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="cssmain/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="cssmain/themify-icons.css" rel="stylesheet" />
    <!-- PLUGINS STYLES-->
    <link href="assets/vendors/jvectormap/jquery-jvectormap-2.0.3.css" rel="stylesheet" />
    <!-- THEME STYLES-->
    <link href="cssmain/main.min.css" rel="stylesheet" />
     <link href="cssmain/master.css" rel="stylesheet" />
    <!-- PAGE LEVEL STYLES-->
     <link href="assets/css/StyleSheet.css" rel="stylesheet" />
    <link href="plugins/datepicker/css/bootstrap-datepicker3.css" rel="stylesheet" />
     
      <script src="amcharts/amcharts.js" type="text/javascript"></script>
    <script src="amcharts/serial.js" type="text/javascript"></script>
    <script type="text/javascript" src="amcharts/pie.js"></script>
    <script type="text/javascript" src="amcharts/themes/light.js"></script>
</head>
<body>
    <form id="form1" runat="server">
     <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row mt-3">
                                 <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        </div>
                                     </div>
                                 <div class="col-lg-1">
                                    <div class="form-group">
                                        <strong>Birth Year</strong>
                                        </div>
                                     </div>
                                 <div class="col-lg-2">
                                    <div class="form-group">
                                        <asp:TextBox id="txtbirthYear" runat="server" AutoPostBack="true" placeholder="Enter Birth Year" class="form-control" OnTextChanged="txtbirthYear_TextChanged">
                </asp:TextBox>
                                        </div>
                                     </div>
                                </div>
                            <div class="row">
                                 <div class="col-lg-5">
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="LblMsg"></asp:Label>
                                        </div>
                                     </div>
                            </div>
                        </div>
                        </div>
         </section>
    </form>
</body>
</html>
