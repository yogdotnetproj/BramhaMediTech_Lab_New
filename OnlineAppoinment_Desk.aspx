 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineAppoinment_Desk.aspx.cs" Inherits="OnlineAppoinment_Desk" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Online Appoinment Desk  </title>
          <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
        <link rel="stylesheet" href="plugins/bootstrap/css/bootstrap.min.css">
        <link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
    
        <link rel="stylesheet" href="plugins/theme/css/theme.min.css">
        <link rel="stylesheet" href="plugins/theme/css/skins/_all-skins.min.css">
        <link rel="stylesheet" href="css/style.css">

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
            <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
            <![endif]-->

     <!--   <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"> -->
      <style type="text/css">
		body{
			font-family: 'Source Sans Pro';
		}

		.extralight{
			font-family: 'Source Sans Pro';
			font-weight: 100;
		}

		.light{
			font-family: 'Source Sans Pro';
			font-weight: 300;
		}

		.regular{
			font-family: 'Source Sans Pro';
			font-weight: 400;
		}

		.semibold{
			font-family: 'Source Sans Pro';
			font-weight: 600;	
		}

		.bold{
			font-family: 'Source Sans Pro';
			font-weight: 700;	
		}

		.black{
			font-family: 'Source Sans Pro';
			font-weight: 900;		
		}
	</style>
    <style>

     
        .treeview a {
                font-size: 16px;
        }
        #TrMenu {
            line-height:30px;
        }
        #TrMenu div tr:hover {
                background-color: #028469;
        }
        #TrMenu div table tbody tr:nth-child(2) td:nth-child(3) {
        width:100%;
       
        }
        #TrMenu table tbody tr td:nth-child(2) {
                padding-left: 20px;
        }
         #TrMenu div table tbody tr td:nth-child(2) {
                padding-left: 0px;
        }
         .skin-black .sidebar a {
    color: #ffffff;
}
        .form-control {
         
            height: 29px !important;
            padding: 0px 7px !important;
            font-size: 13px;
        }
        .content-header > .breadcrumb {
            top: 5px !important;
                padding: 5px 5px;
        }
        .content-header {
  padding: 8px 15px 7px 16px !important;
}
        .skin-black .content-header {
    margin: 0px 15px;
        }

        .skin-black .content-header {
    background-color:#34bfa3;
        }

        .content {
            padding: 5px;
        }
        .box-header {
            padding: 0px;
        }
        .table > tbody > tr > th {
        padding:5px !important;
        }
        .navbar-nav > li > a {
            top: 0px !important;
            left: 0px !important;
        }
        .skin-black .main-header .navbar .nav>li>a {
    color: #fff !important;
    font-size: 21px !important;
    border:none !important;
}
        .content-header > .breadcrumb > li > a {
            color: #fff !important;
        }
        .skin-black .main-header .navbar .nav > li > a:hover {
            background: #1e6792 !important;
        }
        .table th {
            background-color: #367fa9 !important;
        }
        .skin-black .sidebar a {
    color: #ffffff !important;
}
        a[href="#TrMenu_SkipLink"] {
    display:none !important;
}
        
       .content table tr:nth-child(odd){
           background-color:#eee !important;
}
        .content table tr:nth-child(even) {
            background-color: #fff;
        }
        .content table tr:hover {
            background-color:#e2e2e2 !important;
        }
    </style>
        <link href="App_Themes/Default/GVCss.css" rel="stylesheet" type="text/css" />
         <link href="css/bootstrap-datepicker.css"  rel="stylesheet" />
    <link href="css1/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css1/bootstrap.min.css" rel="stylesheet" type="text/css" />
  
    <style type="text/css">
        /*@import url('https://fonts.googleapis.com/css?family=Pacifico|Open+Sans:300,400,600');*/
        
        *
        {
            box-sizing: border-box;
            font-family: 'Open Sans' , sans-serif;
            font-weight: 300;
        }
        
        a
        {
            text-decoration: none;
            color: white;
        }
        
        p
        {
            font-size: 1.1em;
            margin: 1em 0;
        }
        
        .description
        {
            margin: 1em auto 2.25em;
        }
        
        body
        {
            /*width: 40%;
            min-width: 300px;
            max-width: 400px;
            margin: 1.5em auto;*/
            color: white;
        }
        
        h1
        {
            font-family: 'Pacifico' , cursive;
            font-weight: 400;
            font-size: 2.5em;
        }
        
        /* .parent {
            display: block;
            background-color: black;
            color: white;
        }*/
        
        ul
        {
            list-style: none;
            padding: 0; /* display: block;*/
        }
        
        ul .inner
        {
            padding-left: 10em;
            overflow: hidden;
            display: none;
        }
        
        ul .inner.show
        {
            /*display: block;*/
        }
        
        ul li
        {
            margin: 0.5em 0;
            font-size: large;
            color: white;
        }
        
        ul li a.toggle
        {
            width: 100%;
            display: block; /* background: rgba(0, 0, 0, 0.78);*/
            color: #fefefe;
            padding: 0.75em;
            border-radius: 0.15em;
            transition: background 0.3s ease;
        }
        
        ul li a.toggle:hover
        {
            background: padding-box;
            background-color: aquamarine;
        }
        
        li .inner:hover
        {
            background: padding-box;
            background-color: aquamarine;
        }
    </style>
     <style type="text/css">
        ul {
            list-style: none;
            padding: 5px 5px 5px 5px;
            display: block;
        }

        li {
            padding: 5px 5px 5px 5px;
            color: white;
        }

        ul .parents {
            display: block;
            background: black;
            font-size: large;
            color: aquamarine;
        }

        ul .childs {
            display: block;
            background: black;
            font-size: large;
            color: white;
        }

        a {
            color: #fff;
        }
    </style>

 
    </head>
<body class="hold-transition skin-black sidebar-mini">
    <form id="form1" runat="server">
    <asp:scriptmanager Id="scriptmanager" runat="server">
    </asp:scriptmanager>
 <%--   <div  class="regular">--%>

<%--    <div class="wrapper">--%>
           <%-- <header class="main-header">--%>
                <!-- Logo -->
               <%-- <a href="dashboard.html" class="logo">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                   <span class="logo-mini"> <asp:Label ID="LblDCCode" runat="server" Text=" " > </asp:Label> </span>
                    <!-- logo for regular state and mobile devices -->
                   <span class="logo-lg"> <asp:Label ID="LblDCName" runat="server" Text=" " ></asp:Label>  </span>
                </a>--%>
                <!-- Header Navbar: style can be found in header.less -->
                <%--<nav class="navbar navbar-static-top">
                    <!-- Sidebar toggle button-->
                  
                    <div class="navbar-custom-menu">--%>
                       <%-- <ul class="nav navbar-nav">
                            <li class="dropdown messages-menu">
                                <a href="home.aspx">
                                    <i class="fa fa-fw fa-home"></i>
                                </a>
                            </li>
                         
                            <li class="dropdown tasks-menu">
                                <a href="Login.aspx">
                                    <i class="fa fa-fw fa-power-off"></i>
                                </a>
                            </li>
                        </ul>--%>
                   <%-- </div>
                </nav>--%>
           <%-- </header>--%>
            <!-- Left side column. contains the logo and sidebar -->
           <%-- <aside class="main-sidebar">
                <!-- sidebar: style can be found in sidebar.less -->
                <section class="sidebar">
                    <!-- Sidebar user panel -->
                    <div class="user-panel">
                        <div class="pull-left image">
                            <img src="images/profile.jpg" class="img-circle" alt="User Image">
                        </div>
                        <div class="pull-left info">
                            <p> <asp:Label ID="LUNAME" runat="server" Text="" ></asp:Label></p>
                            <a href="#"><i class="fa fa-circle text-success"></i> Admin</a>
                        </div>
                    </div>
                    <!-- sidebar menu: : style can be found in sidebar.less -->
                  
                </section>
                <!-- /.sidebar -->
            </aside>--%>
            <!-- Content Wrapper. Contains page content -->
           <%-- <div class="content-wrapper">--%>
                <!-- Content Header (Page header) -->
         <%-- <div class="wrapper">--%>
           <%-- <header class="main-header">
                 <nav class="navbar navbar-static-top">
          <div style="text-align:center;margin:auto;margin-top:13px " >
                                       <p style="color:#fff;font-size:18px" >LIS Management System</p></div>--%>
                <section class="content-header">
                    <h1>Appoinment Book</h1>
                    <%--<ol class="breadcrumb">
                    <li><a href="Login.aspx"><i class="fa fa-fw fa-lock"></i> Login</a></li>
                    <li><a href="Login.aspx"><i class="fa fa-fw fa-power-off"></i> Log out</a></li>
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">IPD Desk</li>
                    </ol>--%>
                </section>
                     <%--</nav>
                 </header>--%>
                <!-- Main content -->
              <%--  <section class="content">   --%>            
                                        
                
                         <div class="box" runat="server" id="Panel3">

                             <div class="box-header">
                                    <asp:Label ID="lblMessage" class="red pull-center"  runat="server" Text="" Font-Bold="true" ForeColor="green" ></asp:Label>
                          
                                   </div>
                            
                              
                                 <div class="box-body">  
                                     <div class="row">
                              
                                         <div id="Div3" class="col-lg-12" runat="server" >                                       
                                    <div class ="row"> 
                                        <div class="col-lg-2 text-right">
                                                    <div class ="form-group">
                                                        <asp:Label ID="Label2" runat="server" ForeColor="Black" Font-Bold="true" Text="Appoinment:" ></asp:Label>
                                                        </div>

                                        </div>
                                        <div class="col-lg-4 text-left">
                                                    <div class ="form-group">
                                                         <asp:ImageButton ID="btnAdmit1" runat="server" Width="50px" ImageUrl="~/Images0/Admit.png" ToolTip="Appoinment"   
                                                               Text="Appoinment" />
                                                        </div>
                                            </div>
                                        <div class="col-lg-3 text-right">
                                                    <div class ="form-group">
                                                        <asp:Label ID="Label3" runat="server" ForeColor="Black" Font-Bold="true" Text="Booked Appoinment:" ></asp:Label>
                                                        </div>

                                        </div>
                                        <div class="col-lg-3 text-left">
                                                    <div class ="form-group">
                                                        <asp:ImageButton ID="ImageButton1" runat="server"  Width="35px" ToolTip="Booked Appoinment" ImageUrl="~/Images0/images1.jpg" 
                                                               Text="Booked Appoinment" />
                                                        </div>
                                            </div>
                                       
                                    </div>   
                                             </div>
                                         </div>
                                          <div id="Div1" class="col-lg-12" runat="server" >
                                       
                                    <div class ="row">  

                                        <div class="col-lg-3 text-left">
                                                    <div class ="form-group">

                                                       <asp:dropdownlist id="ddlCenter" runat="server" class="form-control" tabindex="3">
                                                              </asp:dropdownlist>
                                                    </div>
                                         
                                                    </div>
                                                        
                                       
                                         <div class="col-lg-2">
                                                    <div class ="form-group">
                                                    <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                        <!--    <input type="text" class="form-control pull-right" id="todate">-->
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                                        </div>
                                             </div>
                                        <div class="col-lg-2">
                                                    <div class ="form-group">
                                                       
                                                        
                                                         <asp:Button ID="btnSearch" runat="server" Text="Search"  class="btn btn-primary btnSearch" OnClick="btnSearch_Click" />
                                                    </div>
                                                        
                                        </div>
                                        <div class="col-lg-4">
                                                    <div class ="form-group">
                                                        <asp:Label ID="Lmsg" runat="server" ForeColor="Red" Text="" Font-Bold="true" ></asp:Label>
                                                    </div>
                                                        
                                        </div>
                                              </div>
                                              </div>
                                    
                                   <div class="col-lg-12" runat="server" visible="false" >
                                       
                                    <div class ="row">  
                                        
                                       
                                               <div class="col-lg-12">
                                                    <div class ="form-group"  style="border:solid;color:orange;background-color:white;width:1200px;height:90px;">
                                   
                                                       
                                                                         
                                                                         <asp:RadioButtonList ID="RdbRoomType" runat="server" ForeColor="Black" Font-Size="Larger" Font-Bold="true"  RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RdbRoomType_SelectedIndexChanged" RepeatColumns="9"  >
                                                             
                                                                             </asp:RadioButtonList>  
                                                                        
                                                        </div>
                                               </div>
                                        </div>
                                       </div>
                                          <div class="col-lg-12" ">
                                    <div class ="row">                                  
                                      
                                        
                     <div class="box" >
                                    <div class="box-body" >

                                   
                   
                            <div  class="col-lg-12" style="border:thin;color:orange;width:1250px" >
                         <div class="row"> 
                             <asp:DataList ID="BedDataList" runat="server" Width="1250px"   RepeatColumns="11" RepeatDirection="Horizontal" OnEditCommand="BedDataList_EditCommand" OnItemDataBound="BedDataList_ItemDataBound" OnItemCommand="BedDataList_ItemCommand">
                                 <ItemStyle  Font-Bold="True" BorderColor="#0066FF" BorderStyle="Solid" BorderWidth="1px" />
                                 <ItemTemplate>
                                     <div class="box">
                                         <div class="box-body">
                                             <div class="col-lg-12" style="border:thin;color:black;width:70px;height:40px">
                                                 <div class="row">
                                                     <div class="col-lg-1 text-left" style="width:100px">
                                                         <div class="form-group">
                                                             <asp:Label ID="lblBedName" Font-Bold="true" runat="server" Text='<%# Eval("TimeSlot") %>' />
                                                            <asp:HiddenField ID="hdnstatus" runat="server" Value='<%# Eval("Status") %>' />
                                                               <asp:HiddenField ID="hdnStartTime" runat="server" Value='<%# Eval("StartTime") %>' />
                                                            <%--  <asp:HiddenField ID="hdnIpdNo" runat="server" Value='<%# Eval("IpdNo") %>' /> 
                                                              <asp:HiddenField ID="hdnIpdId" runat="server" Value='<%# Eval("IpdId") %>' />                                                              
                                                             <asp:Label ID="lblIsAdmited" runat="server" Visible="false" Text='<%# Eval("PatStatus") %>' />
                                                              <asp:HiddenField ID="hdn_IsUniversalPrecaution" runat="server" Value='<%# Eval("IsUniversalPrecaution") %>' /> 
                                                                <asp:Label ID="lblRegId" runat="server" Visible="false" Text = '<%# Eval("PatRegId") %>' /> 
                                                              <asp:Label ID="lblPatientName" runat="server" Width="200px" Text='<%# Eval("FullName") %>' />--%>
                                                          
                                                              
                                                              </div>
                                                     </div>
                                                     </div>
                                                
                                                    
                                                     <div class="row">
                                                         <div class="col-lg-1 text-left" style="width:70px" >
                                                            
                                                             
                                                              <asp:ImageButton ID="btnAdmit" runat="server" Width="50px" ImageUrl="~/Images0/Admit.png" ToolTip=" Book" CommandName="Edit"  
                                                               Text="Add" />
                                                            <asp:ImageButton ID="btnBooked" runat="server" Visible="false" Width="35px" ImageUrl="~/Images0/images1.jpg" ToolTip=" Booked" CommandName="Booked"  
                                                               Text="Booked" />
                                                            
                                                         </div>
                                                     </div>
                                                
                                             </div>
                                         </div>
                                     </div>
                                 </ItemTemplate>
                             </asp:DataList>
                         </div>
                         </div>
                                                        
                <div  class="col-lg-12" >
                         <div class="row">
                             </div>
                   </div>

                   </div>
                         </div>

               
                                        </div>
                                              </div>
                                        
                                        
                                     </div>
                                     </div>

                                         
                                     
                             <%--   </div>--%>
               
                 
                                 
                 
                       
                <!-- /.content -->
           <%-- </div>--%>
            <!-- /.content-wrapper -->
                
            <footer class="main-footer text-center">
                <strong>Copyright ©2019 Era InfoSoft.  <a href="#">All rights reserved.</a>.</strong> 
            </footer>
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
        <!-- ./wrapper -->
            <script src="js0/bootstrap.js" type="text/javascript"></script>
    <script src="js0/jquery-1.11.0.min.js" type="text/javascript"></script>
     
        <link href="css1/11.css" rel="stylesheet" type="text/css" />
        <link href="css1/12.css" rel="stylesheet" type="text/css" />
         <script src="js0/1.js" type="text/javascript"></script>
         <script src="js0/2.js" type="text/javascript"></script>
              <script language="javascript" type="text/javascript">
                  function OpenReport() {
                      window.open("Reports.aspx");
                  }
               </script>
    </form>
</body>
</html>
