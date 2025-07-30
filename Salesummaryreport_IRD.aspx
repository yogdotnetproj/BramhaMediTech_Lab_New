 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Salesummaryreport_IRD.aspx.cs" Inherits="Salesummaryreport_IRD" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Sale RegisterIRD  </title>
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

       <!-- <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"> -->
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
                <a href="dashboard.html" class="logo">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                   <span class="logo-mini"> <asp:Label ID="LblDCCode" runat="server" Text=" " > </asp:Label> </span>
                    <!-- logo for regular state and mobile devices -->
                   <span class="logo-lg"> <asp:Label ID="LblDCName" runat="server" Text=" " ></asp:Label>  </span>
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
                                <a href="home.aspx">
                                    <i class="fa fa-fw fa-home"></i>
                                </a>
                            </li>
                            <li class="dropdown notifications-menu">
                                <a href="Login.aspx">
                                    <i class="fa fa-fw fa-lock"></i>
                                </a>
                            </li>
                            <li class="dropdown tasks-menu">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                    <i class="fa fa-fw fa-users"></i>
                                </a>
                            </li>
                            <li class="dropdown tasks-menu">
                                <a href="Login.aspx">
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
                            <p> <asp:Label ID="LUNAME" runat="server" Text="" ></asp:Label></p>
                            <a href="#"><i class="fa fa-circle text-success"></i> Admin</a>
                        </div>
                    </div>
                    <!-- sidebar menu: : style can be found in sidebar.less -->
  <ul class="sidebar-menu" data-widget="tree">
                        <li class="treeview">

                         <asp:TreeView ID="TrMenu"  Visible="true" runat="server"  ExpandDepth="1"   
                onselectednodechanged="TrMenu_SelectedNodeChanged" >
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
                    <h1>Sale SummaryIRD</h1>
                    <ol class="breadcrumb">
                    <li><a href="Login.aspx"><i class="fa fa-fw fa-lock"></i> Login</a></li>
                    <li><a href="Login.aspx"><i class="fa fa-fw fa-power-off"></i> Log out</a></li>
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Sale SummaryIRD</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                          
                         
                                <div class="col-lg-4">
                                    <div class="form-group">
                                      
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                               <div class="col-lg-2">
                                    <div class="form-group">
                                        <label></label>
               
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                             <asp:TextBox id="todate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="2">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                
                              
                                               
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                     <asp:button id="btnlist" runat="server" OnClientClick="return validate();" onclick="btnlist_Click"
                                            text="Click" class="btn btn-primary" />
                                      
                                            <asp:button id="btnreport1"  runat="server" class="btn btn-primary" onclick="btnreport1_Click"
                                            tooltip="Sale summary Report" Text="Sale summary Report"
                                            />
                     <asp:button id="btnexcel"  runat="server" class="btn btn-primary" 
                                            tooltip="Excel Sale summary Report" Text="Excel Sale summary Report" onclick="btnexcel_Click"
                                            />
                                   
                                </div> 
                            </div>
                        </div>
                    </div>
                    <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
                             <div style=" overflow: scroll; width: 1060px; height: 1000px; text-align: right"
                                                        id="Divdoc">
                   <asp:gridview id="GridView1" class="table table-responsive table-sm table-bordered" runat="server" width="100%" onrowdatabound="GridView1_RowDataBound"
                    onrowediting="GridView1_RowEditing"   HeaderStyle-ForeColor="Black"
                     AlternatingRowStyle-BackColor="White"   autogeneratecolumns="False" PageSize="55000"
                    allowpaging="True" onpageindexchanging="GridView1_PageIndexChanging" 
                                     onrowcreated="GridView1_RowCreated">
                    <columns>
                           
                            <asp:BoundField DataField="RecDate" HeaderText="Date" />
                              <asp:BoundField DataField="TotInv" HeaderText="Tot Inv" />
                              <asp:BoundField DataField="InvoiceNo" HeaderText=" Invoice" />
                               <asp:BoundField DataField="amount" HeaderText=" Amount" />  
                            
                            <asp:BoundField DataField="Discount" HeaderText="Discount" />
                            
                             <asp:BoundField DataField="Taxable" HeaderText="Taxable" />
                              <asp:BoundField DataField="Tax" HeaderText=" Tax" />
                              <asp:BoundField DataField="Net" HeaderText="Net" />
                              
                              
                        </columns>
                       

                </asp:gridview>
            </div>
                     <table id="Table1" width="100% " runat="server">
                <tr>
                <td>
               Amount:
                </td>
                 <td>
               <asp:label id="LblAmount" runat="server" Text="0"></asp:label>
                </td>
                 <td>
                Discount:
                </td>
                 <td>
                  <asp:label id="Lbldiscount" runat="server" Text=""></asp:label>
                </td>
                 <td>
               Taxable:
                </td>
                  <td>
                    <asp:label id="Lbltaxable" runat="server" Text=""></asp:label>
                </td>
                 <td>
               Tax on Taxable:
                </td>
                 <td>
                    <asp:label id="Lbltaxontaxable" runat="server" Text=""></asp:label>
                </td>
                 <td>
               Net Amount:

                </td>
                 <td>
                    <asp:label id="Lblnetamount" runat="server" Text=""></asp:label>
                </td>

                </tr>
                </table>
                </div>
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
         <script language="javascript" type="text/javascript">
             function OpenReport() {
                 window.open("Reports.aspx");
             } 
               </script>
              
    </form>
</body>
</html>
