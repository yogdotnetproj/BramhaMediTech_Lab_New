 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestWiseStatusDashboard.aspx.cs" Inherits="TestWiseStatusDashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title></title>
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    
    <!-- GLOBAL MAINLY STYLES-->
    <link href="cssmain/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="cssmain/themify-icons.css" rel="stylesheet" />
    <!-- PLUGINS STYLES-->
    <link href="assets/vendors/jvectormap/jquery-jvectormap-2.0.3.css" rel="stylesheet" />
    <!-- THEME STYLES-->
    <link href="cssmain/main.min.css" rel="stylesheet" />
    <!-- PAGE LEVEL STYLES-->
     <link href="assets/css/StyleSheet.css" rel="stylesheet" />
    <link href="plugins/datepicker/css/bootstrap-datepicker3.css" rel="stylesheet" />

      <script src="amcharts/amcharts.js" type="text/javascript"></script>
    <script src="amcharts/serial.js" type="text/javascript"></script>
    <script type="text/javascript" src="amcharts/pie.js"></script>
    <script type="text/javascript" src="amcharts/themes/light.js"></script>
</head>
<body class="fixed-navbar has-animation bluetheme">
     <form id="form1" runat="server">
    <script src="fusioncharts/FusionCharts.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="regular">
        <ul class="color-theme">
                       <li><a href="#" class="dark-theme"></a></li>
                       <li><a href="#" class="light-theme"></a></li>
                    <li><a href="#" class="red-theme"></a></li>
                    <li><a href="#" class="blue-theme"></a></li>
                       <li><a href="#" class="darkblue-theme"></a></li>
                    <li><a href="#" class="green-theme"></a></li>
                    <li><a href="#" class="orange-theme"></a></li>
                </ul>
        <div class="wrapper">
          <header class="header">
            <div class="page-brand">
                <a class="link" href="index.html">
                    <span class="brand"> 
                        <span class="brand-tip">LIS Management System</span>
                    </span>
                    <span class="brand-mini">LIS</span>
                </a>
            </div>
            <div class="flexbox flex-1">
                <!-- START TOP-LEFT TOOLBAR-->
                <ul class="nav navbar-toolbar">
                    <li>
                        <a class="nav-link sidebar-toggler js-sidebar-toggler"><i class="fa fa-bars"></i></a>
                    </li>
                    <!--<li>
                        <form class="navbar-search" action="javascript:;">
                         <!-- Logo --
                <a href="dashboard.aspx" class="logo" style="width:194px">
                    <!-- mini logo for sidebar mini 50x50 pixels --
                </a>
                        </form>
                    </li>-->
                </ul>
                <!-- END TOP-LEFT TOOLBAR-->
                <div class="text-center"> <span class="logo-mini"><img src="images/logo.png" style="width:10rem;" /></span></div>
                <!-- START TOP-RIGHT TOOLBAR-->
                <ul class="nav navbar-toolbar">
                   
                 
                    <li class="dropdown dropdown-user">
                        <a class="nav-link dropdown-toggle link" data-toggle="dropdown">
                            <img src="./assets/img/admin-avatar.png" />
                            <span><asp:Label ID="UsernameLB" runat="server" Text=""></asp:Label></span><i class="fa fa-angle-down m-l-5"></i></a>
                        <ul class="dropdown-menu dropdown-menu-right">
                            <a class="dropdown-item" href="Home.aspx"><i class="fa fa-fw fa-home"></i>Home</a>
                            <a class="dropdown-item" href="javascript:;"><i class="fa fa-fw fa-key"></i>key</a>
                            <li class="dropdown-divider"></li>
                            <a class="dropdown-item" href="Login.aspx"><i class="fa fa-fw fa-power-off"></i>Logout</a>
                        </ul>
                    </li>
                </ul>
                <!-- END TOP-RIGHT TOOLBAR-->
            </div>
        </header>
            <!-- Left side column. contains the logo and sidebar -->
         <%-- <nav class="page-sidebar" id="sidebar">
            <div id="sidebar-collapse">
                <div class="admin-block d-flex">
                    <div>
                        <img src="./assets/img/admin-avatar.png" width="45px" />
                    </div>
                    <div class="admin-info">
                        <div class="font-strong"><asp:Label ID="UsernameLB2" runat="server" Text=""></asp:Label></div><small>Administrator</small></div>
                </div>
                <ul class="side-menu metismenu">
                    <li>
                        <a class="active" href="Home.aspx"><i class="sidebar-item-icon fa fa-th-large"></i>
                            <span class="nav-label">Dashboard</span>
                        </a>
                    </li>
                    <asp:PlaceHolder ID="SlidBarHolder" runat="server"></asp:PlaceHolder>
                </ul>
            </div>
        </nav>--%>
            <!-- Content Wrapper. Contains page content -->
            <div class="content-wrapper   ms-0">
                <!-- Main content -->
               <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        
                                       <div class="table-responsive" style="width: 100%; margin-bottom: 10px; max-height: 1550px; vertical-align: top; overflow: scroll;">
                                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Timer ID="Timer1" runat="server" OnTick="RefreshGridView" Interval="5000" />
                                      <asp:DataList ID="DLTestStatus" runat="server" Width="100%" RepeatDirection="Horizontal"  RepeatColumns = "5" 
                                            CellSpacing = "10" RepeatLayout="Table" 
                                            onitemdatabound="DLTestStatus_ItemDataBound" >
                                      <ItemTemplate>
                                       <div class="table-responsive" style="width:100%">
                                      <asp:Panel ID="Panel1" runat="server" BorderColor="#0099cc"
BorderWidth="2px" Height="80px" Width="240px">
                                      <table width="100%" runat="server" id="tbl">
                                      
                                      <tr>
                                        <th colspan="2" width="50%" style="color: black; font-weight: bold" align="left">
                                       <%-- <span style="color: Maroon; font-weight: bold;"> Status:</span>--%>
                                           
                                           <asp:Label ID="LblTestStatus" Visible="false" Text='<% #Eval("TestStatus") %>' runat="server" />
                                            <asp:Label ID="LblTestStatusN"  Text="" runat="server" />                                            
                                            <asp:HiddenField ID="isemergency" runat="server" Value='<%#Eval("Isemergency") %>' />

                                        </th>
                                          <th colspan="2" width="100%" style="color: black; font-weight: bold" align="center">
                                              <b><%# Eval("DailySeqNo")%></b>
                                              </th>
                                            <th colspan="2" width="100%" style="color: black; font-weight: bold" align="right">
                                              <b><%# Eval("PheboAccTime")%></b>
                                              </th>
                                        </tr>
                                        
                                        <tr>
                                         <th colspan="6" width="100%" style="color: black; font-weight: bolder" align="center">
                                       <%-- <span style="color: forestgreen; font-weight: bold;"> Name:</span>--%>
                                           <b><%# Eval("PatientName")%></b>
                                        </th>
                                        
                                        </tr>
                                        <tr>
                                         <th colspan="6" width="100%" style="color: black; font-weight: bolder" align="center">
                                       <%-- <span style="color: blueviolet; font-weight: bold;">Test Code :</span>--%>
                                           <b><%# Eval("ShortCode")%></b>
                                           <asp:ImageButton ID="btnEmergency"   class="flashingTextcss" ImageUrl="~/images/light-311119__340.png" runat="server"></asp:ImageButton>
                                           
                                        </th>
                                        </tr>
                                                                             </table>                                     
 </asp:Panel>
 </div>
                                      </ItemTemplate>
                                     
                                      </asp:DataList>
        </ContentTemplate>
                                               </asp:UpdatePanel>
                                    </div>
        </div>

                                </div>
                                
                            </div>

               
                <!-- /.content -->
            </div>
            <!-- /.content-wrapper -->
            <footer class="main-footer text-center">
                <strong>Copyright &copy; 2017 <a href="#">ERA Infotech </a>.</strong> All rights reserved.
            </footer>
        </div>
    </div>
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <!-- Core plugin JavaScript-->
    <script src="vendor/jquery-easing/jquery.easing.min.js"></script>
    <!-- Page level plugin JavaScript-->
    <script src="vendor/chart.js/Chart.min.js"></script>
    <!-- Custom scripts for all pages-->
    <script src="js9/sb-admin.min.js"></script>
    <!-- Custom scripts for this page-->
    <script src="js9/sb-admin-charts.min.js"></script>
    <!-- ./wrapper -->
    <script src="plugins/jquery/jquery.min.js"></script>
    <script src="plugins/jquery-ui/jquery-ui.min.js"></script>
    <!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
    <script>
        $.widget.bridge('uibutton', $.ui.button);
    </script>
    <!-- Bootstrap 3.3.7 -->
    <script src="plugins/bootstrap/js/bootstrap.min.js"></script>
    <!-- datepicker -->
    <script src="plugins/datepicker/js/bootstrap-datepicker.min.js"></script>
    <script src="plugins/theme/js/theme.min.js"></script>
    <script type="text/javascript">
        //Date picker
        $('#fromdate, #todate').datepicker({
            autoclose: true
        })
    </script>
    <script language="javascript" type="text/javascript">
        function OpenReport() {
            window.open("Reports.aspx");
        }
    </script>
    <script src="js0/bootstrap.js" type="text/javascript"></script>
    <script src="js0/jquery-1.11.0.min.js" type="text/javascript"></script>
    <script src="js0/5.js" type="text/javascript"></script>
    <script src="js0/6.js" type="text/javascript"></script>
    <script src="js0/7.js" type="text/javascript"></script>
     <!--<link href="css1/13.css" rel="stylesheet" type="text/css" />
      <link href="css1/14.css" rel="stylesheet" type="text/css" />-->

    <!--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css"
        rel="stylesheet" />
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"
        rel="stylesheet" /> -->
   

    </form>
      <!-- CORE PLUGINS-->
    <script src="jsmain/jquery.min.js" type="text/javascript"></script>
    <script src="assets/vendors/popper.js/dist/umd/popper.min.js" type="text/javascript"></script>
    <script src="jsmain/bootstrap.bundle.min.js" type="text/javascript"></script>
    <script src="assets/vendors/metisMenu/dist/metisMenu.min.js" type="text/javascript"></script>
    <script src="assets/vendors/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <!-- PAGE LEVEL PLUGINS-->
    <script src="assets/vendors/chart.js/dist/Chart.min.js" type="text/javascript"></script>
    <script src="assets/vendors/jvectormap/jquery-jvectormap-2.0.3.min.js" type="text/javascript"></script>
    <script src="assets/vendors/jvectormap/jquery-jvectormap-world-mill-en.js" type="text/javascript"></script>
    <script src="assets/vendors/jvectormap/jquery-jvectormap-us-aea-en.js" type="text/javascript"></script>
    <!-- CORE SCRIPTS-->
    <script src="assets/js/app.min.js" type="text/javascript"></script>
    <!-- PAGE LEVEL SCRIPTS-->
    <script src="./assets/js/scripts/dashboard_1_demo.js" type="text/javascript"></script>


      <script src="./assets/vendors/select2/dist/js/select2.full.min.js" type="text/javascript"></script>
    <script src="./assets/vendors/jquery-knob/dist/jquery.knob.min.js" type="text/javascript"></script>
    <script src="./assets/vendors/moment/min/moment.min.js" type="text/javascript"></script>
    <script src="./assets/vendors/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="./assets/vendors/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="./assets/vendors/jquery-minicolors/jquery.minicolors.min.js" type="text/javascript"></script>
     <script type="text/javascript">
         $('a.red-theme').on('click', function () {
             $('body').addClass('redtheme');
             $('body').removeClass('bluetheme greentheme orangetheme lighttheme darktheme darkbluetheme');
         });
         $('a.blue-theme').on('click', function () {
             $('body').addClass('bluetheme');
             $('body').removeClass('redtheme greentheme orangetheme lighttheme darktheme darkbluetheme');
         });
         $('a.green-theme').on('click', function () {
             $('body').addClass('greentheme');
             $('body').removeClass('redtheme bluetheme orangetheme lighttheme darktheme darkbluetheme');
         });
         $('a.orange-theme').on('click', function () {
             $('body').addClass('orangetheme');
             $('body').removeClass('redtheme bluetheme greentheme lighttheme darktheme darkbluetheme');
         });
         $('a.light-theme').on('click', function () {
             $('body').addClass('lighttheme');
             $('body').removeClass('redtheme bluetheme greentheme orangetheme darktheme darkbluetheme');
         });
         $('a.dark-theme').on('click', function () {
             $('body').addClass('darktheme');
             $('body').removeClass('redtheme bluetheme greentheme lighttheme orangetheme darkbluetheme');
         });
         $('a.darkblue-theme').on('click', function () {
             $('body').addClass('darkbluetheme');
             $('body').removeClass('redtheme bluetheme greentheme lighttheme darktheme orangetheme');
         });
</script>
</body>
</html>
