s<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <style>
    paging {
        padding: 0px 6px;
        background-color: #38c8dd;
        color: #fff;
        border: 3px solid #fff;
    }
</style>
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

     <link rel="stylesheet" href="customTheme/css/fontawesome-min.css"/>
    <link rel="stylesheet" href="customTheme/css/customTheme.css"/>
    <link rel="stylesheet" href="customTheme/css/CustomGridstyle25.css"/>
</head>
<body class="fixed-navbar has-animation bluetheme" id="bodyWrapper">
    <form id="form1" runat="server">
    <script src="fusioncharts/FusionCharts.js" type="text/javascript"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="regular">
       <%-- <ul class="color-theme">
                       <li><a href="#" class="dark-theme"></a></li>
                       <li><a href="#" class="light-theme"></a></li>
                    <li><a href="#" class="red-theme"></a></li>
                    <li><a href="#" class="blue-theme"></a></li>
                       <li><a href="#" class="darkblue-theme"></a></li>
                    <li><a href="#" class="green-theme"></a></li>
                    <li><a href="#" class="orange-theme"></a></li>
                </ul>--%>
        <div class="wrapper">
        <header class="page-header row">
           <div class="logo-wrapper d-flex align-items-center col-auto">
             <a href="Home.aspx">
               <img class="light-logo img-fluid" src="customTheme/brahmaMedical.png" width="130px" alt="logo">
               <img class="dark-logo img-fluid" src="customTheme/brahmaMedical.png" width="130px" alt="logo"></a><a class="close-btn toggle-sidebar" href="javascript:void(0)">
               <svg class="svg-color" id="toggleMenu">
                 <use href="customTheme/svg/iconly-sprite.svg#Category"></use>
               </svg>
             </a>
           </div>
           <div class="page-main-header col">
             <div class="header-left">
               <div class="form-group-header d-lg-block d-none">
                 <div class="Typeahead Typeahead--twitterUsers">
                   <div class="u-posRelative d-flex align-items-center"> 
                     <input class="demo-input py-0 Typeahead-input form-control-plaintext w-100" type="text" placeholder="Type to Search..." name="q" title=""><i class="search-bg iconly-Search icli"></i>
                   </div>
                 </div>
               </div>
             </div>
             <div class="nav-right">
               <ul class="header-right"> 
                 <li class="search d-lg-none d-flex"> <a href="javascript:void(0)">
                   <svg>
                     <use href="customTheme/svg/iconly-sprite.svg#Search"></use>
                   </svg></a>
                 </li>
                 <li>
                   <a class="dark-mode" href="javascript:void(0)">
                     <svg>
                       <use href="customTheme/svg/iconly-sprite.svg#moondark"></use>
                     </svg></a>
                 </li>
                 <li class="custom-dropdown">
                     <a href="javascript:void(0)" id="notificationMenu">
                         <svg>
                             <use href="customTheme/svg/iconly-sprite.svg#notification"></use>
                         </svg>
                     </a>
                     <span class="badge rounded-pill badge-primary">4</span>
                     <div class="custom-menu notification-dropdown py-0 overflow-hidden" id="notificationMenuWrapper">
                         <h3 class="title bg-primary-light dropdown-title">Notification <span class="font-primary">View all</span></h3>
                         <ul class="activity-timeline">
                             <li class="d-flex align-items-start">
                               <div class="activity-line"></div>
                               <div class="activity-dot-primary"></div>
                               <div class="flex-grow-1">
                                 <h6 class="f-w-600 font-primary">30-04-2024<span>Today</span><span class="circle-dot-primary float-end">
                                     <svg class="circle-color">
                                       <use href="../assets/svg/iconly-sprite.svg#circle"></use>
                                     </svg></span></h6>
                                 <h5>Alice Goodwin</h5>
                                 <p class="mb-0">Fashion should be fun. It shouldn't be labelled intellectual.</p>
                               </div>
                             </li>
                             <li class="d-flex align-items-start">
                                 <div class="activity-dot-secondary"></div>
                                 <div class="flex-grow-1">
                                     <h6 class="f-w-600 font-secondary">28-06-2024<span>1 hour ago</span><span class="float-end circle-dot-secondary">
                                       <svg class="circle-color">
                                         <use href="../assets/svg/iconly-sprite.svg#circle"></use>
                                       </svg></span>
                                     </h6>
                                     <h5>Herry Venter</h5>
                                     <p>I am convinced that there can be luxury in simplicity.</p>
                                 </div>
                             </li>
                             <li class="d-flex align-items-start">
                                 <div class="activity-dot-primary"></div>
                                 <div class="flex-grow-1">
                                   <h6 class="f-w-600 font-primary">04-08-2024<span>Today</span><span class="float-end circle-dot-primary">
                                       <svg class="circle-color">
                                         <use href="../assets/svg/iconly-sprite.svg#circle"></use>
                                       </svg></span>
                                   </h6>
                                   <h5>Loain Deo</h5>
                                   <p>I feel that things happen for open new opportunities.</p>
                                 </div>
                             </li>
                             <li class="d-flex align-items-start">
                                 <div class="activity-dot-secondary"></div>
                                 <div class="flex-grow-1">
                                   <h6 class="f-w-600 font-secondary">12-11-2024<span>Yesterday</span><span class="float-end circle-dot-secondary">
                                       <svg class="circle-color">
                                         <use href="../assets/svg/iconly-sprite.svg#circle"></use>
                                       </svg></span>
                                   </h6>
                                   <h5>Fenter Jessy</h5>
                                   <p>Sometimes the simplest things are the most profound.</p>
                                 </div>
                             </li>
                         </ul>
                     </div>
                 </li>
                 <li>
                   <a class="full-screen" href="javascript:void(0)"> 
                     <svg>
                       <use href="customTheme/svg/iconly-sprite.svg#scanfull"></use>
                     </svg>
                   </a>
                 </li>
                 <li class="profile-nav custom-dropdown">
                   <div class="user-wrap">
                     <div class="user-img"><img src="customTheme/images/profile.png" alt="user"></div>
                     <div class="user-content" id="userMenu">
                       <h6>Ava Davis</h6>
                       <p class="mb-0">Admin<i class="fa-solid fa-chevron-down"></i></p>
                     </div>
                   </div>
                   <div class="custom-menu overflow-hidden" id="userMenuWrapper">
                     <ul class="profile-body">
                       <li class="d-flex"> 
                         <svg class="svg-color">
                           <use href="customTheme/svg/iconly-sprite.svg#Profile"></use>
                         </svg><a class="ms-2" href="Home.aspx">Home</a>
                       </li>
                       <li class="d-flex"> 
                         <svg class="svg-color">
                           <use href="customTheme/svg/iconly-sprite.svg#Document"></use>
                         </svg><a class="ms-2" href="ChangePassword.aspx">Change Password</a>
                       </li>
                       <li class="d-flex"> 
                         <svg class="svg-color">
                           <use href="customTheme/svg/iconly-sprite.svg#Login"></use>
                         </svg><a class="ms-2" href="login.aspx">Log Out</a>
                       </li>
                     </ul>
                   </div>
                 </li>
               </ul>
             </div>
           </div>
         </header>
            <!-- Left side column. contains the logo and sidebar -->
          <nav class="page-sidebar" id="sidebar">
            <div id="sidebar-collapse">
                <!--div class="admin-block d-flex">
                    <div>
                        <img src="./assets/img/admin-avatar.png" width="45px" />
                    </div>
                    <div class="admin-info">
                        <div class="font-strong"><asp:Label ID="UsernameLB2" runat="server" Text=""></asp:Label></div><small>Administrator</small></div>
                </div-->
                <ul class="subMenuWrapper nav-2-level" id="subMenuWrapper"></ul>
                <ul class="side-menu metismenu">
                    <li>
                        <a class="active" href="Home.aspx"><i class="sidebar-item-icon fa fa-th-large"></i>
                            <span class="nav-label">Dashboard</span>
                        </a>
                    </li>
                    <asp:PlaceHolder ID="SlidBarHolder" runat="server"></asp:PlaceHolder>
                </ul>
            </div>
        </nav>
            <!-- Content Wrapper. Contains page content -->
            <div class="content-wrapper">
                <div class="content px-3">
                    <!-- Main content -->
                    <div class="box p-3 bg-white mb-3">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                Total number of patients daywise</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body"">
                            <div class="row">
                                 <div class="col-md-5">
                                     </div>
                                <div class="col-md-3">
                                     <input type="text" class="form-control"  id="txtCenterName" />
                                </div>
                                <div class="col-md-2">
                                    <input type="text" class="form-control" id="datepicker11" />
                                </div>
                                <div class="col-md-2">
                                    <input type="text" class="form-control" id="datepicker1" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="chart">
                                        <div id="chartdiv11" style="width: 100%; height: 400px; background-color: #FFFFFF;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer" style="">
                        </div>
                    </div>
                    <div class="box p-3 bg-white mb-3" hidden="hidden">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                Total number of patients daywise</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body" style="background-color: white;">
                            <div class="row">
                                <div class="col-md-9">
                                </div>
                                <div class="col-md-2">
                                    <select name="months" class="months1 form-control" id="months2">
                                        <option value="1">January</option>
                                        <option value="2">February</option>
                                        <option value="3">March</option>
                                        <option value="4">April</option>
                                        <option value="5">May</option>
                                        <option value="6">June</option>
                                        <option value="7">July</option>
                                        <option value="8">August</option>
                                        <option value="9">September</option>
                                        <option value="10">October</option>
                                        <option value="11">November</option>
                                        <option value="12">December</option>
                                    </select>
                                </div>
                                <div class="col-md-1">
                                    <input type="text" class="form-control" id="datepicker2" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="chart">
                                        <div id="chartdiv12" style="width: 100%; height: 400px; background-color: #FFFFFF;">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer" style="">
                        </div>
                    </div>
                    <div class="box p-3 mb-3" style="background-color: white;">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                Total patients by department</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body" style="background-color: white;">
                            <div class="row">
                                <div class="col-md-9">
                                </div>
                                <div class="col-md-2">
                                    <select name="months3" class="months3 form-control" id="months3">
                                        <option value="1">January</option>
                                        <option value="2">February</option>
                                        <option value="3">March</option>
                                        <option value="4">April</option>
                                        <option value="5">May</option>
                                        <option value="6">June</option>
                                        <option value="7">July</option>
                                        <option value="8">August</option>
                                        <option value="9">September</option>
                                        <option value="10">October</option>
                                        <option value="11">November</option>
                                        <option value="12">December</option>
                                    </select>
                                </div>
                                <div class="col-md-1">
                                    <input type="text" class="form-control" id="datepicker3" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="chart">
                                        <div id="chartdiv13" style="width: 100%; height: 400px; background-color: #FFFFFF;">
                                        </div>
                                    </div>
                                    <!-- /.chart-responsive -->
                                </div>
                                <!-- /.col -->
                            </div>
                            <!-- /.row -->
                        </div>
                        <!-- ./box-body -->
                        <div class="box-footer" style="">
                        </div>
                        <!-- /.box-footer -->
                    </div>
                    <div class="box p-3 col-md-12" style="background-color: white;">
                        <div class="box-header with-border">
                            <h3 class="box-title">
                                Top 5 Referal Doctors</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i class="fa fa-minus"></i>
                                </button>
                            </div>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body" style="background-color: white;">
                            <div class="row">
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-3">
                                    <select name="months" class="months4 form-control" id="months4">
                                        <option value="1">January</option>
                                        <option value="2">February</option>
                                        <option value="3">March</option>
                                        <option value="4">April</option>
                                        <option value="5">May</option>
                                        <option value="6">June</option>
                                        <option value="7">July</option>
                                        <option value="8">August</option>
                                        <option value="9">September</option>
                                        <option value="10">October</option>
                                        <option value="11">November</option>
                                        <option value="12">December</option>
                                    </select>
                                </div>
                                <div class="col-md-3">
                                    <input type="text" class="form-control" id="datepicker4" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="chart">
                                        <div id="chartdiv14" style="width: 100%; height: 400px; background-color: #FFFFFF;">
                                        </div>
                                    </div>
                                 
                                </div>
                               
                            </div>
                          
                        </div>
                       
                        <div class="box-footer" style="">
                        </div>
                       
                    </div>
                    <!-- /.content -->
                </div>
            </div>
            <!-- /.content-wrapper -->
            <footer class="main-footer text-center">
                <strong>Copyright &copy; 2025 <a href="#">BramhaMediTech </a>.</strong> All rights reserved.
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
     


    <script type="text/javascript">
            $(document).ready(function () {

                $("#txtCenterName").change(function () {
                    debugger;
                    //alert('in');
                    var Centername = $("#txtCenterName").val();
                    var selectedMonth1 = toDate($("#datepicker11").val());
                    var selectedYear1 = toDate($("#datepicker1").val());
                    get1(selectedMonth1, selectedYear1, Centername);

                    var selectedMonth3 = $("select.months3").children("option:selected").val();
                    var selectedYear3 = $("#datepicker3").val();
                    get21(selectedMonth3, selectedYear3, Centername);

                    var selectedMonth4 = $("select.months4").children("option:selected").val();
                    var selectedYear4 = $("#datepicker4").val();
                    get4(selectedMonth4, selectedYear4, Centername);
                });

                $.ajax({
                    type: "GET",
                    url: "Graphs.asmx/MenusBind",
                    dataType: "json",
                    //contentType:'application/json; charset=utf-8',
                    success: function (res) {
                        //alert(JSON.stringify(res));

                        for (var i = 0; i < res.length; i++) {
                            var items = '';
                            debugger;
                            items += '<li class="parents">' + res[i]["MenuName"] + '<span class="caret"></span>';

                            var childs = '';
                            var arr = res[i]["ChildMenus"];
                            if (arr.length > 0) {
                                childs = '<ul class="childEle">';
                                for (var j = 0; j < arr.length; j++) {
                                    childs += '<li class="childs"><a href="' + arr[j]["Url"] + '" style="text-decoration:none;">' +
                                                arr[j]["ChildMenuName"] +
                                        '</a></li>';
                                }
                                childs += '</ul>';
                            }

                            items += childs;

                            items += '</li>';

                            $("#newlist").append(items);
                        }

                        $('.parentEle ul').hide();

                        $('.parents').click(function () {
                            $(this).find('ul').slideToggle(550);
                        });

                          $('.childs').hover(function () {

                                $(this).css("background-color","green");
                                
                            },
                            function()
                            {
                             $(this).css("background-color","black");
                        });

                        $('a').hover(function () {
                           
                        });


                        $('.toggle').click(function (e) {
                            e.preventDefault();

                            var $this = $(this);

                            if ($this.next().hasClass('show')) {
                                $this.next().removeClass('show');
                                $this.next().slideUp(350);
                            } else {
                                $this.parent().parent().find('li .inner').removeClass('show');
                                $this.parent().parent().find('li .inner').slideUp(350);
                                $this.next().toggleClass('show');
                                $this.next().slideToggle(350);
                            }
                        });

                       
                    },
                    failure: function () {
                        alert('fail');
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    },
                });

                $('.child').hide();

                //adding a click handlers to every all parents
                $('.parent').click(function () {

                    //slide up the children which are open already
                    $('.child').slideUp();

                    //find the child of clicked parent and toggle its visibility
                    $(this).find('ul').slideToggle();
                });

                //------------------------------------------------------------------------------------------------------------------------------------
                var currentMonth = new Date().getMonth() + 1;
                //alert(currentMonth);


                $("#months2").val(currentMonth);
                $("#months3").val(currentMonth);
                $("#months4").val(currentMonth);

                var today = new Date();
                var dateLimit = new Date(new Date().setDate(today.getDate() - 30));

                $("#datepicker1").datepicker({
                    format: "yyyy/mm/dd",
                }).datepicker("setDate", today);

                $("#datepicker11").datepicker({
                    format: "yyyy/mm/dd",
                }).datepicker("setDate", dateLimit);

                $("#datepicker2").datepicker({
                    format: "yyyy",
                    viewMode: "years",
                    minViewMode: "years",
                }).datepicker("setDate", today);

                $("#datepicker3").datepicker({
                    format: "yyyy",
                    viewMode: "years",
                    minViewMode: "years",
                }).datepicker("setDate", new Date());

                $("#datepicker4").datepicker({
                    format: "yyyy",
                    viewMode: "years",
                    minViewMode: "years",
                }).datepicker("setDate", new Date());

                ////////////////////////////////////////////////////////////////////////////////

                //var selectedMonth1 = $("select.months1").children("option:selected").val();
                var selectedYear11 = toDate($("#datepicker11").val());
                var selectedYear1 = toDate($("#datepicker1").val());
                var Centername = $("#txtCenterName").val();
                //alert(selectedYear11 + " - " +selectedYear1);
               // alert(Centername);
                get1(selectedYear11, selectedYear1, Centername);

                ////////////////////////////////////////////////////////////////////////////////
                //get2();

                ////////////////////////////////////////////////////////////////////////////////

                var selectedMonth3 = $("select.months3").children("option:selected").val();
                var selectedYear3 = $("#datepicker3").val();
                //alert(selectedMonth3, selectedYear3);
                get21(selectedMonth3, selectedYear3,null);

                ////////////////////////////////////////////////////////////////////////////////
                //get3();

                ////////////////////////////////////////////////////////////////////////////////

                var selectedMonth4 = $("select.months4").children("option:selected").val();
                var selectedYear4 = $("#datepicker4").val();
                get4(selectedMonth4, selectedYear4,null);

                //---------------------------------------////////////////////////////////////////

                //$("select.months1").change(function () {
                //    selectedMonth1 = $(this).children("option:selected").val();
                //    get1(selectedMonth1, selectedYear1);
                //});

                $("#datepicker11").change(function () {
                    selectedYear11 = toDate($("#datepicker11").val());
                    selectedYear1 = toDate($("#datepicker1").val());

                    if (selectedYear11 < selectedYear1) {
                        //alert(selectedYear11 + " " +  selectedYear1)
                        get1(selectedYear11, selectedYear1,null);
                    }
                    else {
                        alert("From date should be less than To date");
                    }
                });

                $("#datepicker1").change(function () {
                    selectedYear11 = toDate($("#datepicker11").val());
                    selectedYear1 = toDate($("#datepicker1").val());

                    if (selectedYear11 < selectedYear1) {
                        get1(selectedYear11, selectedYear1,null);
                    }
                    else {
                        alert("From date should be less than To date");
                    }
                });

                ////////////////////////////////////////////////////////////////////////////////

                $("select.months3").change(function () {
                    selectedMonth3 = $(this).children("option:selected").val();
                    get21(selectedMonth3, selectedYear3,null);
                });

                $("#datepicker3").change(function () {
                    selectedYear3 = $(this).val();
                    get21(selectedMonth3, selectedYear3,null);
                });

                ////////////////////////////////////////////////////////////////////////////////

                $("select.months4").change(function () {
                    selectedMonth4 = $(this).children("option:selected").val();
                    get4(selectedMonth4, selectedYear4,null);
                });

                $("#datepicker4").change(function () {
                    selectedYear4 = $(this).val();
                    get4(selectedMonth4, selectedYear4,null);
                });
            });


            function menuBind() {



            }

            function toDate(dateStr) {
                //var parts = dateStr.split("/")
                //return new Date(parts[2], parts[1] - 1, parts[0])
                return dateStr;
            }

            ////////////////////////////////////////////////////////////////////////////////

            function get1(selectedMonth1, selectedYear1, Centername) {
               // alert("You have selected month= " + selectedMonth1 + " year=" + selectedYear1);
               //alert('center:' + Centername);
                var dats = { month: "selectedMonth1", year: "selectedYear1", Center: "Centername" };
                $.ajax({
                    type: "POST",
                    url: "Graphs.asmx/Get_TotalNoOfPatients_Month",
                    dataType: "json",
                    data: { month: selectedMonth1, year: selectedYear1, Center: Centername },
                    //contentType:'application/json; charset=utf-8',
                    success: function (res) {
                        debugger;
                        //alert('success : ' + JSON.parse(res));
                        AmCharts.makeChart("chartdiv11",
				{
				    "type": "serial",
				    "categoryField": "RegDate",
				    "dataDateFormat": "DD/MM/YYYY",
				    "categoryAxis": {
				        "parseDates": true
				    },
				    "chartCursor": {
				        "enabled": true
				    },
				    "chartScrollbar": {
				        "enabled": true
				    },
				    "trendLines": [],
				    "graphs": [
						{
						    "fillAlphas": 0.7,
						    "fillColors": "#84C5EB",
						    "id": "AmGraph-1",
						    "lineAlpha": 0,
						    "title": "graph 1",
						    "valueField": "NoOfPatients",
						    "bullet": "bubble",
						    "bulletBorderColor": "",
						    "bulletColor": "#54D6EB",
						    "bulletSize": 11,
						    "labelText": "[[value]]",
						    "lineColor": "#0D338C",
						    "lineThickness": 2,
						    "showBulletsAt": "open",
						},
				    ],
				    "guides": [],
				    "valueAxes": [
						{
						    "id": "ValueAxis-1",
						    "title": "No. of Patients"
						}
				    ],
				    "allLabels": [],
				    //"balloon": {},
				    "legend": {
				        "enabled": false
				    },
				    //"titles": [
				    //	{
				    //	    "id": "Title-1",
				    //	    "size": 15,
				    //	    "text": "Total Number of Patients Daywise"
				    //	}
				    //],
				    "dataProvider": res,
				}
			);
                    },
                    failure: function () {
                        alert('fail');
                    },
                    error: function (jqXHR, exception) {
                        var msg = '';
                        if (jqXHR.status === 0) {
                            msg = 'Not connect.\n Verify Network.';
                        } else if (jqXHR.status == 404) {
                            msg = 'Requested page not found. [404]';
                        } else if (jqXHR.status == 500) {
                            msg = 'Internal Server Error [500].';
                        } else if (exception === 'parsererror') {
                            msg = 'Requested JSON parse failed.';
                        } else if (exception === 'timeout') {
                            msg = 'Time out error.';
                        } else if (exception === 'abort') {
                            msg = 'Ajax request aborted.';
                        } else {
                            msg = 'Uncaught Error.\n' + jqXHR.responseText;
                        }
                        alert(msg);
                    },
                });
            }

            function get2() {
                $.ajax({
                    type: "GET",
                    url: "Graphs.asmx/Get_TotalAmountReceived",
                    dataType: "json",
                    success: function (res) {
                        //alert('success' + JSON.stringify(res));
                        AmCharts.addInitHandler(function (chart) {
                            // check if there are graphs with autoColor: true set
                            for (var i = 0; i < chart.graphs.length; i++) {
                                var graph = chart.graphs[i];
                                if (graph.autoColor !== true)
                                    continue;
                                var colorKey = "autoColor-" + i;
                                graph.lineColorField = colorKey;
                                graph.fillColorsField = colorKey;
                                for (var x = 0; x < chart.dataProvider.length; x++) {
                                    var color = chart.colors[x]
                                    chart.dataProvider[x][colorKey] = color;
                                }
                            }

                        }, ["serial"]);
                        AmCharts.makeChart("chartdiv12",
				         {
				             "type": "serial",
				             "categoryField": "month",
				             "startDuration": 1,
				             "rotate": false,
				             "categoryAxis": {
				                 "gridPosition": "start"
				             },
				             "trendLines": [],
				             "graphs": [
                                 {
                                     "balloonText": "[[title]] of [[category]]:[[value]]",
                                     "fillAlphas": 1,
                                     "id": "AmGraph-1",
                                     "title": "Cash",
                                     "type": "column",
                                     "valueField": "cash"
                                 },
                                 {
                                     "balloonText": "[[title]] of [[category]]:[[value]]",
                                     "fillAlphas": 1,
                                     "id": "AmGraph-2",
                                     "title": "Card",
                                     "type": "column",
                                     "valueField": "card"
                                 },
                                 {
                                     "balloonText": "[[title]] of [[category]]:[[value]]",
                                     "fillAlphas": 1,
                                     "id": "AmGraph-3",
                                     "title": "Online",
                                     "type": "column",
                                     "valueField": "insurance"
                                 }
				             ],
				             "guides": [],
				             "valueAxes": [
                                 {
                                     "id": "ValueAxis-1",
                                     "stackType": "regular",
                                     "title": "Amount (Rs.)"
                                 }
				             ],
				             "allLabels": [],
				             "balloon": {},
				             "legend": {
				                 "enabled": true,
				                 //"useGraphSettings": true
				                 "align": "center",
				                 "position": "right"
				             },
				             "titles": [
                                 {
                                     "id": "Title-1",
                                     "size": 15,
                                     "text": "Total Amount Received per Month"
                                 }
				             ],
				             "dataProvider": res

				         }
			            );
                    },
                    failure: function () {
                        alert('fail');
                    },
                    error: function (error) {
                        alert('error: ' + error);
                    }
                });
            }

            function get21(selectedMonth3, selectedYear3, Centername) {
                $.ajax({
                    type: "POST",
                    url: "Graphs.asmx/Get_TotalPatientsByDept",
                    dataType: "json",
                    data: { month: selectedMonth3, year: selectedYear3, center: Centername },
                    success: function (res) {
                        //alert('success' + JSON.stringify(res));
                        AmCharts.addInitHandler(function (chart) {
                            // check if there are graphs with autoColor: true set
                            for (var i = 0; i < chart.graphs.length; i++) {
                                var graph = chart.graphs[i];
                                if (graph.autoColor !== true)
                                    continue;
                                var colorKey = "autoColor-" + i;
                                graph.lineColorField = colorKey;
                                graph.fillColorsField = colorKey;
                                graph.bulletColor = colorKey;
                                for (var x = 0; x < chart.dataProvider.length; x++) {
                                    var color = chart.colors[x]
                                    chart.dataProvider[x][colorKey] = color;
                                }
                            }

                        }, ["serial"]);
                        AmCharts.makeChart("chartdiv13",
				         {
				             "type": "serial",
				             "categoryField": "RegDate",
				             "startDuration": 1,
				             "rotate": false,
				             "categoryAxis": {
				                 "gridPosition": "start",
				                 "labelRotation": 45
				             },
				             "trendLines": [],
				             "graphs": [
                                 {
                                     "balloonText": "[[category]]:[[value]]",
                                     "fillAlphas": 1,
                                     "id": "AmGraph-1",
                                     "title": "depts",
                                     "type": "column",
                                     "valueField": "NoOfPatients",
                                     "autoColor": true,
                                     "bullet": "square",
                                     "bulletBorderColor": "",
                                     "bulletColor": "#54D6EB",
                                     "bulletSize": 11,
                                     "showBulletsAt": "open",
                                     "labelText": "[[value]]",
                                 },
				             ],
				             "guides": [],
				             "valueAxes": [
                                 {
                                     "id": "ValueAxis-1",
                                     "stackType": "regular",
                                     "title": "No. of patients"
                                 }
				             ],
				             "allLabels": [],
				             "balloon": {},
				             "legend": {
				                 "enabled": false,
				                 //"useGraphSettings": true
				                 "align": "center",
				                 "position": "right"
				             },
				             //"titles": [
				             //    {
				             //        "id": "Title-1",
				             //        "size": 15,
				             //        "text": "Total Patients by Deptartment"
				             //    }
				             //],
				             "dataProvider": res

				         }
			            );
                    },
                    failure: function () {
                        alert('fail');
                    },
                    error: function (error) {
                        alert('error: ' + error);
                    }
                });
            }

            function get3() {
                $.ajax({
                    type: "GET",
                    url: "Graphs.asmx/Get_TotalPatientsByDept",
                    dataType: "json",
                    success: function (res) {
                        //alert('success' + JSON.stringify(res));
                        AmCharts.makeChart("chartdiv14",
				{
				    "type": "pie",
				    "balloonText": "[[title]]</b> ([[percents]]%)</span>",
				    "gradientType": "linear",
				    "labelText": "[[percents]]%",
				    "maxLabelWidth": 100,
				    "tabIndex": 2,
				    "titleField": "RegDate",
				    "valueField": "NoOfPatients",
				    "autoDisplay": true,
				    "theme": "light",
				    "allLabels": [],
				    "balloon": {},
				    "legend": {
				        "enabled": true,
				        "align": "center",
				        "markerType": "circle",
				        "position": "right"
				    },
				    "titles": [
                        {
                            "id": "Title-1",
                            "size": 15,
                            "text": "Total no. of patients dept-wise"
                        }
				    ],
				    "dataProvider": res

				}
			);
                    },
                    failure: function () {
                        alert('fail');
                    },
                    error: function (error) {
                        alert('error: ' + error);
                    }
                });
            }

            function get4(selectedMonth4, selectedYear4,center) {
                //alert("You have selected month= " + selectedMonth4 + " year=" + selectedYear4);
                $.ajax({
                    type: "POST",
                    url: "Graphs.asmx/Get_Top5DoctorsByPatientRefered",
                    data: { month: selectedMonth4, year: selectedYear4 , center: center},
                    dataType: "json",
                    success: function (res) {
                        AmCharts.makeChart("chartdiv14",
				{
				    "type": "pie",
				    "balloonText": "[[title]]</b> ([[percents]]%)</span>",
				    "gradientType": "linear",
				    "labelText": "[[value]]",
				    "maxLabelWidth": 100,
				    "tabIndex": 2,
				    "titleField": "RegDate",
				    "valueField": "NoOfPatients",
				    "autoDisplay": true,
				    "theme": "light",
				    "allLabels": [],
				    "balloon": {},
				    "legend": {
				        "enabled": true,
				        "align": "center",
				        "markerType": "circle",
				        "position": "right"
				    },
				    //"titles": [
				    //    {
				    //        "id": "Title-1",
				    //        "size": 15,
				    //        "text": "Top 5 Referal Doctors"
				    //    }
				    //],
				    "dataProvider": res

				}
			);
                    },
                    failure: function () {
                        alert('fail');
                    },
                    error: function (error) {
                        alert('error: ' + error);
                    }
                });
            }
    </script>

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
         $('a.darkblue-theme').on('click', function(e) {
             $('body').addClass('darkbluetheme');
             $('body').removeClass('redtheme bluetheme greentheme lighttheme darktheme orangetheme');
         });
         $('#notificationMenu').on('click', function(e) {
             $('#notificationMenuWrapper').toggleClass('show');
             e.preventDefault();
         });
         $('#userMenu').on('click', function(e) {
             $('#userMenuWrapper').toggleClass('show');
             e.preventDefault();
         });
         $('#toggleMenu').on('click', function(e) {
             $('#bodyWrapper').toggleClass('collapsed');
             e.preventDefault();
         });
         $(".metismenu > li").on('click', function () {
             // console.log($('.metismenu li.active ul'));
             if ($('.metismenu li.active ul')[0]) {
                 $('#subMenuWrapper').html($('.metismenu li.active ul')[0].innerHTML);
                 if (($('.metismenu li.active ul').closest('li.active').offset().top + $('#subMenuWrapper').height()) > $(window).height()) {
                     $('#subMenuWrapper').css({
                         top: $('.metismenu li.active ul').closest('li.active').offset().top - 25 - $('#subMenuWrapper').height(),
                         left: $('.metismenu li.active ul').closest('li.active').offset().left + 105
                     });
                 } else {
                     $('#subMenuWrapper').css({
                         top: $('.metismenu li.active ul').closest('li.active').offset().top - 75,
                         left: $('.metismenu li.active ul').closest('li.active').offset().left + 105
                     });
                 }
             } else {
                 $('#subMenuWrapper').html('');
                 $('#subMenuWrapper').css({
                     top: 0,
                     left: 0
                 });
             }
             // e.preventDefault();
         });
         $(document).on('click', function (e) {
             if (!$(e.target).closest('#subMenuWrapper').length && !$(e.target).closest('.metismenu li.active').length && !$(e.target).closest('.logo-wrapper').length) {
                 $('#subMenuWrapper').html('');
                 $('#subMenuWrapper').css({
                     top: 0,
                     left: 0
                 });
                 $('.metismenu li.active').removeClass('active');
             }
             if (!$(e.target).closest('#notificationMenu').length) {
                 $('#notificationMenuWrapper').removeClass('show');
             }
             if (!$(e.target).closest('#userMenu').length) {
                 $('#userMenuWrapper').removeClass('show');
             }
         });
     </script>
</body>
</html>
