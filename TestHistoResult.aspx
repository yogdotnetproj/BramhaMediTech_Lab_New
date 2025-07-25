 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestHistoResult.aspx.cs" Inherits="TestHistoResult" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Test Histo Result  </title>
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

     <!--   <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">-->
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
       <script src="ckeditor/ckeditor.js"></script>
    </head>
<body class="hold-transition skin-black sidebar-mini">
    <form id="form1" runat="server">
     <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
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
                    <h1>Test Histo Result</h1>
                    <ol class="breadcrumb">
                        <li><a href="Login.aspx"><i class="fa fa-fw fa-lock"></i> Login</a></li>
                        <li><a href="Login.aspx"><i class="fa fa-fw fa-power-off"></i> Log out</a></li>
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Test Histo Result</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                              
                               <div class="col-lg-12">
                                    <div class="form-group">
                                     <div id="div2" class="rounded_corners"  >
                                     <table   cellpadding="2" cellspacing="0"  style="width: 100%; border-right: threeddarkshadow 2px solid;
                    border-top: threeddarkshadow 2px solid; border-left: threeddarkshadow 2px solid;
                    border-bottom: threeddarkshadow 2px solid;">
                    <tr >
                        <td align="left" style="width:90px" valign="top">
                            <asp:Label ID="Label4" runat="server" Text="Reg. Number :" Width="90px" Font-Bold="True"></asp:Label></td>
                        <td align="left" style="width: 91px" valign="top">
                            <asp:Label ID="lblRegNo" runat="server" Text="RegNo"  Font-Bold="True" Width="70px"></asp:Label></td>
                        <td align="right" style="width: 79px" valign="top">
                            <asp:Label ID="Label2" runat="server" Text="Name :" Width="55px" Font-Bold="True"></asp:Label></td>
                        <td align="left" style="width: 100px" valign="top">
                            <asp:Label ID="lblName" runat="server" Text="Name"  Font-Bold="True" Width="186px"></asp:Label></td>
                        <td align="right" style="width: 84px" valign="top">
                            <asp:Label ID="Label3" runat="server" Text="Age :" Width="51px" Font-Bold="True"></asp:Label></td>
                        <td align="left" style="width: 90px" valign="top">
                            <asp:Label ID="lblage" runat="server" Text="Age"  Font-Bold="True" Width="51px"></asp:Label></td>
                        <td align="right" style="width: 101px" valign="top">
                            <asp:Label ID="Label43" runat="server" Text="Gender :" Width="54px" 
                                Font-Bold="True"></asp:Label></td>
                        <td align="left" style="width: 100px" valign="top">
                            <asp:Label ID="lblSex" runat="server" Text="Sex"  Font-Bold="True" Width="70px"></asp:Label></td>
                    </tr>
                    <tr  >
                        <td align="left" style="width: 91px" valign="top">
                            <asp:Label ID="Label45" runat="server" Text="Mobile Number :" Width="110px" 
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
                            <asp:Label ID="lbldate1" Font-Bold="True" runat="server" Text="Date:"  Width="51px"></asp:Label></td>
                        <td align="left" style="width: 90px" valign="top">
                            <asp:Label ID="lbldate" runat="server"  Font-Bold="True" Width="142px"></asp:Label></td>
                        <td align="right" style="width: 101px" valign="top">
                        <asp:Label ID="Label5" Font-Bold="True" runat="server" Text="Ref Doc:"  
                                Width="65px"></asp:Label>
                        </td>
                        <td align="left" style="width: 100px" valign="top">
                        <asp:Label ID="LblRefDoc" Font-Bold="True" runat="server"  Width="155px"></asp:Label>
                        </td>
                    </tr>
                </table>
                                  </div>
                                    </div>
                                  </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Format Name</label>
                                      
                                       
                     <asp:textbox id="txtTestcode" runat="server" class="form-control">  </asp:textbox>
                    <asp:requiredfieldvalidator id="TestCodeRequiredFieldValidator" runat="server" controltovalidate="txtTestcode"
                        errormessage="Please enter the subdeptName" style="position: relative" validationgroup="Format"
                        font-bold="True">
                    </asp:requiredfieldvalidator>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Assign Doctor</label>
                                        
                                        
                                <asp:DropDownList ID="ddlDoctor_new" runat="server" class="form-control" >
                                                <asp:ListItem Value="0">Select Doctor</asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                </div> 
                                 <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Authorize</label>
                                        
                                        </br>
                               <asp:checkbox id="chkAuthorize" runat="server"  />
                                   
                                    </div>
                                </div> 
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <label>Format</label>
                                       
                                          <asp:dropdownlist id="CmbFormatName" runat="server"  class="form-control" autopostback="True"
                                            onselectedindexchanged="CmbFormatName_SelectedIndexChanged"> </asp:dropdownlist>
                                           
                                            <asp:TextBox ID="txtFormatName"  runat="server"  class="form-control" 
                                        AutoPostBack="true" ontextchanged="txtFormatName_TextChanged"></asp:TextBox>
                                   
                                 
                                    </div>
                                </div>    
                                
                                
                                           
                                            <div class="col-lg-12">
                                    <div class="form-group">
                                     <table id="Table2" width="100%" runat="server">
                                    <tr>
                                    <td align="center" >
                                   
                                     <asp:label id="lblSelectDocError" runat="server" ></asp:label>
                                    <asp:label style="position: relative" id="lblValidate" runat="server" width="472px"
                        visible="False" ></asp:label>
                                   <!--   <cc:HtmlEditor ID="Editor" runat="server" Height="400px" Style="text-indent: 2px"  Width="800px" />-->

                                       <asp:TextBox ID="Editor1" runat="server" Height="2500px" TextMode="MultiLine"></asp:TextBox>
<script type="text/javascript" lang="javascript">    CKEDITOR.replace('<%=Editor1.ClientID%>');</script> 
                                      </td>
                                      </tr>
                                      </table>
                                    </div>
                                 </div> 
                                 
                                 
                                  <div class="col-lg-12">
                                    <div class="form-group">
                                     <table id="Table3" width="100%" runat="server">
                                    <tr>
                                    <td align="center" >
                                    <label>Image1</label>                                

                                       
                                      </td>
                                      <td>
                                      <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
                                       <asp:Image ID="Image1" runat="server"/>
                                      </td>
                                      </tr>
                                       <tr>
                                    <td align="center" >
                                    <label>Image2</label>                               

                                       
                                      </td>
                                      <td>
                                      <asp:FileUpload ID="FileUpload2" runat="server"></asp:FileUpload>
                                      </td>
                                      </tr>
                                       <tr>
                                    <td align="center" >
                                    <label>Image3</label>
                                                                        
                                      </td>
                                      <td>
                                      <asp:FileUpload ID="FileUpload3" runat="server"></asp:FileUpload>
                                      </td>
                                      </tr>
                                       <tr>
                                    <td align="center" >
                                    <label>Image4</label>
                                  
                                      </td>
                                      <td>
                                      <asp:FileUpload ID="FileUpload4" runat="server"></asp:FileUpload>
                                      </td>
                                      </tr>
                                       <tr>
                                    <td align="center" >
                                    <label>Image5</label>
                                  
                                      </td>
                                      <td>
                                      <asp:FileUpload ID="FileUpload5" runat="server"></asp:FileUpload>
                                      </td>
                                      </tr>
                                      </table>
                                    </div>
                                 </div> 
                                     
                            </div>
                        </div>
                       
                    </div>
                  
                    <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">   
                                   <asp:button id="btnsave" runat="server" class="btn btn-primary" text="Save And Close" onclick="cmdSaveClose_Click"   />
                                    <asp:button id="cmdClose" runat="server" text="Close"  class="btn btn-primary" causesvalidation="False" onclick="cmdClose_Click"  />
                                    <asp:button id="cmdClear" runat="server" text="Save Format" class="btn btn-primary" onclick="cmdClear_Click"
                                        validationgroup="Format" />
                                           <asp:button id="Button1" runat="server" text="Print" 
                                        class="btn btn-primary"     
                                        validationgroup="Format" onclick="Button1_Click" />
                                </div> 
                            </div>
                        </div>
                </section>
                <!-- /.content -->
            </div>
            <!-- /.content-wrapper -->
            <footer class="main-footer text-center">
                <strong>Copyright &copy; 2017 <a href="#">Phoenix infotech solutions</a>.</strong> All rights reserved.
            </footer>
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
               <asp:sqldatasource id="sqlFormat" runat="server" connectionstring="<%$ ConnectionStrings:myconnection %>"
        selectcommand="select [Name], Result,STCODE from dfrmst where STCODE=@STCODE and branchid=@branchid order by Name">
        <selectparameters>
            <asp:QueryStringParameter Name="STCODE" QueryStringField="STCODE" />
            <asp:SessionParameter DefaultValue="0" Name="branchid" SessionField="branchid" />
        </selectparameters>
    </asp:sqldatasource>
    <asp:hiddenfield id="hdnSort" runat="server" />
    </form>
</body>
</html>
