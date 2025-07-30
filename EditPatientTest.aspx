 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPatientTest.aspx.cs" Inherits="EditPatientTest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
 <head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Patient Test Edit </title>
        <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
        <link rel="stylesheet" href="plugins/bootstrap/css/bootstrap.min.css">
        <link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
        <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css">
        <link rel="stylesheet" href="plugins/theme/css/theme.min.css">
        <link rel="stylesheet" href="plugins/theme/css/skins/_all-skins.min.css">
        <link rel="stylesheet" href="css/style.css">

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
            <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
            <![endif]-->

<!-- <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">     -->
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
    </head>
<body class="hold-transition skin-black sidebar-mini">
    <form id="form1" runat="server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    <div  class="regular">

 <div class="wrapper">
            <header class="main-header">
                <!-- Logo -->
                <a href="home.aspx" class="logo">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                    <span class="logo-mini"><asp:Label ID="LblDCCode" runat="server" Text=" " > </asp:Label></span>
                    <!-- logo for regular state and mobile devices -->
                    <span class="logo-lg"><asp:Label ID="LblDCName" runat="server" Text=" " ></asp:Label> </span>
                </a>
                <!-- Header Navbar: style can be found in header.less -->
                <nav class="navbar navbar-static-top">
                    <!-- Sidebar toggle button-->
                    <a href="#" class="sidebar-toggle" data-toggle="push-menu" role="button">
                        <span class="sr-only">Toggle navigation</span>
                    </a>
                    <div class="navbar-custom-menu">
                        <ul class="nav navbar-nav">
                            <li class="dropdown messages-menu">
                            <a href="home.aspx" >
                                
                                    <i class="fa fa-fw fa-home"></i>

                                </a>
                              
                            </li>
                            <li class="dropdown notifications-menu">
                                <a href="Login.aspx ">
                                    <i class="fa fa-fw fa-lock"></i>
                                   
                                </a>

                            </li>
                            <li class="dropdown tasks-menu">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    <i class="fa fa-fw fa-users"></i>
                                </a>
                            </li>
                            <li class="dropdown tasks-menu">
                                <a href="Login.aspx" >
                                    <i class="fa fa-fw fa-power-off"></i>
                                </a>
                            </li>
                        </ul>
                    </div>
                </nav>
            </header>
            <!-- Left side column. contains the logo and sidebar -->
            <aside class="main-sidebar">
                <!-- sidebar: style can be found in sidebar.less -->
                <section class="sidebar">
                    <!-- Sidebar user panel -->
                    <div class="user-panel">
                        <div class="pull-left image">
                            <img src="images/profile.jpg" class="img-circle" alt="User Image">
                        </div>
                        <div class="pull-left info">
                            <p><asp:Label ID="LUNAME" runat="server" Text="" ></asp:Label></p>
                            <a href="#"><i class="fa fa-circle text-success"></i> Admin</a>
                        </div>
                    </div>
                    <!-- sidebar menu: : style can be found in sidebar.less -->
                 <ul class="sidebar-menu" data-widget="tree">
                        <li class="treeview">

                         <asp:TreeView ID="TrMenu"  Visible="true" runat="server"  ExpandDepth="1"   
                onselectednodechanged="TrMenu_SelectedNodeChanged"  >
                <HoverNodeStyle Font-Underline="false" />
                <NodeStyle Font-Names ="Tahoma" Font-Size="10pt"   HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" >
                </NodeStyle>
                <ParentNodeStyle  />
                <SelectedNodeStyle  Font-Underline="false" HorizontalPadding="0px" 
                VerticalPadding="0px" />
                
                </asp:TreeView>

                           
                           
                        </li>                    
                       
                        
                       
                    </ul>
                </section>
                <!-- /.sidebar -->
            </aside>
            <!-- Content Wrapper. Contains page content -->
            <div class="content-wrapper">
                <!-- Content Header (Page header) -->
                <section class="content-header">
                    <h1>Patient Test Edit</h1>
                    <ol class="breadcrumb">
                    <li><a href="Login.aspx"><i class="fa fa-fw fa-lock"></i> Login</a></li>
                       <li><a href="Login.aspx"><i class="fa fa-fw fa-power-off"></i> Log out</a></li>
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Patient Test Edit</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            
                                            <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        <div class="input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy" data-autoclose="true">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            
                                              <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                       <asp:TextBox  id="txtCentername"  runat="server" class="form-control" placeholder="Center Name"
AutoPostBack="True" OnTextChanged="txtCentername_TextChanged"></asp:TextBox>
 <DIV style="DISPLAY: none; OVERFLOW: scroll; WIDTH: 249px; HEIGHT: 100px" id="Div2">
</div>
<cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1"
 TargetControlID="txtCentername" ServiceMethod="GetCenterName"  CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="Div2">
                  </cc1:AutoCompleteExtender>  
                                  </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        
                                         <asp:TextBox id="txtPatientName"  runat="server" class="form-control pull-right" placeholder="Enter Patient Name"
 AutoPostBack="false"></asp:TextBox>  
 
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                      
                                         <asp:textbox id="txtmobileno" runat="server" class="form-control pull-right" placeholder="Enter Mobile Number"></asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                      
                                        <asp:textbox id="txtRegNo" runat="server"  class="form-control pull-right" placeholder="Enter Registration Number"></asp:textbox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                     <asp:button id="btnshow" runat="server" class="btn btn-primary" OnClientClick="return validate();"  onclick="btnshow_Click" text="Click"  />
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                          <div class="table-responsive" style="width:100%">
                
                <asp:gridview id="GridEditTest" runat="server" class="table table-responsive table-sm table-bordered" allowsorting="True" autogeneratecolumns="False"
                    datakeynames="PID" width="100%" onselectedindexchanged="GridEditTest_SelectedIndexChanged"
                    allowpaging="True" onpageindexchanging="GridEditTest_PageIndexChanging" pagesize="20"
                  HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  >
                    <columns>
                     <asp:CommandField SelectText="Show " ShowSelectButton="True" HeaderText="Show" />
                    <asp:BoundField DataField="PatRegID" HeaderText="Reg No" />                   
                    <asp:BoundField DataField="Patname" HeaderText="Name" />
                        <asp:BoundField DataField="CenterName" HeaderText="Center Name" />                         
                        <asp:TemplateField HeaderText="Test Name">
                            <ItemTemplate>
                                <asp:Label ID="lbltestname" runat="server" Text='<%#Bind("testname")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TestCharges" HeaderText="Charges" />
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lbltest" runat="server" Text='<%#Bind("Tests")%>' Visible="false" />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                       
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HiddenField ID="hdnFID" runat="server" Value='<%#Bind("FID")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </columns>
                   

                </asp:gridview>
                </div>
                    </div>
                </section>
                <!-- /.content -->
            </div>
            <!-- /.content-wrapper -->
            <footer class="main-footer text-center">
                <strong>Copyright &copy; 2025 <a href="#">##</a>.</strong> All rights reserved.
            </footer>
        </div>
        </div>
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
       
    </form>
</body>
</html>
