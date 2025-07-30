 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Banner_Master.aspx.cs" Inherits="Banner_Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Banner   </title>
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

      <!--  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">-->
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
<body >
    <form id="form1" runat="server">
         <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
        <script type="text/javascript">
            function CallConfirmBox() {
                if (confirm("Confirm Proceed Further?")) {
                    //OK – Do your stuff or call any callback method here..
                    alert("You pressed OK!");
                } else {
                    //CANCEL – Do your stuff or call any callback method here..
                    alert("You pressed Cancel!");
                }
            }
</script>
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
                    <h1>Banner</h1>
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Banner</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                            <div class="col-lg-3" id="a1" runat="server" visible="false" >
                                    <div class="form-group">

                                        <asp:FileUpload ID="FileUploader" runat="server" />
                                         <asp:FileUpload runat="server" ID="fu" />
                                    </div>
                                    </div>
                            <div class="col-lg-3" id="a" runat="server" visible="false" >
                                    <div class="form-group">
                                      
                                         <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter Banner"  Height="22px" Style="position: relative"
                    Width="303px" TabIndex="1"></asp:TextBox>
                                        </div>
                                        </div>

                                        
                                <div class="col-lg-3">
                                    <div class="form-group">                                      
                                        
                                         <asp:TextBox ID="txtBannername" placeholder="Enter Banner" runat="server" Height="22px" Style="position: relative"
                    Width="303px" TabIndex="1"></asp:TextBox><span style="color:Red;">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBannername"
                    Display="Dynamic" ErrorMessage="Banner is required." ForeColor="Red"  SetFocusOnError="True"
                    ValidationGroup="form" Width="152px" Font-Bold="True"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-lg-3 text-center">
                                    <div class="form-group pt25">
                                     
                                         <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" class="btn btn-primary" Text="Click"  TabIndex="2" />
                                          <asp:Button ID="Button1" runat="server"  class="btn btn-primary" Text="Click"  TabIndex="2" />
                                       
                                         <asp:Button ID="btnsave"  runat="server" OnClick="Button1_Click"  Text="Save"  ValidationGroup="form" class="btn btn-primary" TabIndex="3" />
                                    <input type="button" onclick="printDiv('printableArea')"  runat="server" visible="false" value="print a div!" />
                                   
                                       
                                    <asp:Image ID="Image2" runat="server"></asp:Image></div>
                                </div>

                                 <div class="col-lg-3 text-center">
                                    <div class="form-group pt25">
                                     <asp:Label ID="Label2" runat="server" Font-Bold="true"   ForeColor="Red" Style="position: relative" Text="Label" ></asp:Label>
                                    </div>
                                    </div>

                            </div>
                        </div>
                    </div>
                    <div class="box" id="Div1"  runat="server" visible="false">
                      <div class="rounded_corners" style="width:592px">
                      <asp:Image ID="Image1" runat="server" Height="156px"></asp:Image>
                      </div>
                      </div>
                    <div class="box" id="printableArea">
                     <div class="table-responsive" style="width:100%">
                 <asp:GridView ID="BannerGrid" runat="server" class="table table-responsive table-sm table-bordered" AutoGenerateColumns="False" DataKeyNames="ID"
                    Width="592px" OnPageIndexChanging="BannerGrid_PageIndexChanging" OnRowEditing="BannerGrid_RowEditing"
                    AllowPaging="True" PageSize="25" OnRowDeleting="BannerGrid_RowDeleting"   HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"   >
                    <Columns>
                        <asp:BoundField DataField="BannerName" HeaderText="Banner Name" />
                        <asp:CommandField HeaderText="Edit " ShowEditButton="True" EditImageUrl="~/Images0/edit.gif"
                            ButtonType="Image" />
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/Images0/delete.gif" />
                    </Columns>
                   

                </asp:GridView>
                </div>
                    </div>
                     <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="Button1"
    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none">
    <div style="height: 60px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                Do you like this product?&nbsp;
                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged">
                    <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:Button ID="btnClose" runat="server" Text="Close" />
</asp:Panel>
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

         <script language="javascript" type="text/javascript">
             function printDiv(divName) {
                 var printContents = document.getElementById(divName).innerHTML;
                 var originalContents = document.body.innerHTML;

                 document.body.innerHTML = printContents;

                 window.print();

                 document.body.innerHTML = originalContents;
             }
         </script>
    </form>
</body>
</html>
