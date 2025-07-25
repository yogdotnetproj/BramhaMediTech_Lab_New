 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="BankMaster.aspx.cs" Inherits="BankMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Bank Master </title>
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

 <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">        
 <link href="App_Themes/Default/GVCss.css" rel="stylesheet" type="text/css" />
    </head>
<body class="hold-transition skin-black sidebar-mini">
      <form id="form1" runat="server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
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
                    <h1>Bank Master</h1>
                    <ol class="breadcrumb">
                    <li><a href="Login.aspx"><i class="fa fa-fw fa-lock"></i> Login</a></li>
<li><a href="Login.aspx"><i class="fa fa-fw fa-power-off"></i> Log out</a></li>
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Bank Master</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box" id="tblcreatebranch" runat="server">
                        <div class="box-body">
                            <div class="row">
                                 <div class="box" id="LbMsg" runat="server" visible="false">
                        <div class="box-body">
                          <asp:Label ID="LBLValidation" runat="server"></asp:Label>
                        </div>
                        </div>
                                 <div class="col-lg-4">
                                    <div class="form-group">
                                        <label>Bank Name</label>
                                        
                          
  <asp:TextBox ID="txtbankname" runat="server" class="form-control" 
                                                        placeholder="Bank Name *" ></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label>Branch Name</label>
                                        
                             <asp:TextBox ID="txtbranchname" runat="server" class="form-control" 
                                                        placeholder="Branch Name *"  ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <label>IFSC Code</label>
                                      
                                         
                                            <asp:TextBox ID="txtifsc" runat="server" class="form-control" 
                                                    placeholder="IFSC Code *"  ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <label>Description</label>
                                      <asp:TextBox ID="txtdescription" runat="server" class="form-control" 
                                                    placeholder="Description *"  TextMode="MultiLine" ></asp:TextBox>
                                    </div>
                                </div>
                              
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   
                                
                                      <asp:Button ID="btnsave" runat="server" Text="Save"  class="btn btn-primary" 
                                                        onclick="btnsave_Click"/>&nbsp;&nbsp;<asp:Button ID="btncancel" 
                                                        runat="server" Text="Cancel" class="btn btn-primary" onclick="btncancel_Click"/>
                                                        &nbsp;&nbsp;<asp:Button ID="btnback" runat="server" Text="Back" 
                                                        class="btn btn-primary" onclick="btnback_Click"/>
                                </div> 
                            </div>
                        </div>
                    </div>
                     <div class="box" id="tblShow" runat="server">
                        <div class="box-body">
                             <div class="rounded_corners" style="width:100%">
               <asp:GridView ID="GVBankMaster"  runat="server" AutoGenerateColumns="False" 
                                                    DataKeyNames="BankId" 
                                                    onpageindexchanging="GVBankMaster_PageIndexChanging" 
                                                    onrowdeleting="GVBankMaster_RowDeleting" 
                                                    onrowediting="GVBankMaster_RowEditing">
                                                <Columns>
                                                 <asp:CommandField ShowEditButton="True" HeaderText="Edit" Visible="false" />
                                                 <asp:TemplateField HeaderText="Edit">
	<ItemTemplate>
	      <asp:ImageButton ID="btnEdit" CssClass="glyphicon-trash" ImageUrl="~/images/edit.png" CommandName="Edit" runat="server" />
        </ItemTemplate>
</asp:TemplateField>
                                               <%-- <asp:BoundField DataField="NUMBRANCHID" HeaderText="Branch Id" />--%>
                                               <asp:BoundField DataField="BankName" HeaderText="Bank Name" >                                                      
                                                  
                                                    <HeaderStyle Width="10%" />
                                                  
                                                    <ItemStyle Width="10%" />
                                                    </asp:BoundField>
                                                <asp:BoundField DataField="BranchName"  HeaderText="Branch Name" >
                                                
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                
                                                <asp:BoundField DataField="IFSCCode" HeaderText="IFSC Code" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                
                                                    <asp:BoundField DataField="Description" HeaderText="Description" >
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                
                                                <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" Visible="false" />
                                                <asp:TemplateField HeaderText="Delete" >
                                                    <ItemTemplate  >
                                                        <asp:ImageButton ID="ImageButton1" CssClass="glyphicon-trash"   
                                                            ImageUrl="~/images/delete.png"  OnClientClick="return ValidateDelete();" CommandName="Delete" runat="server" 
                                                            ImageAlign="Middle" />
                                                    
                                                    </ItemTemplate>
                                                    
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                                </asp:GridView>
                                                 <asp:Button ID="btnAddnew" runat="server" Text="Add New" class="btn btn-primary" 
                                                    onclick="btnAddnew_Click"/>
                  <asp:Label ID="LblMsg" runat="server" Font-Bold="true" ForeColor="Red" Text=""></asp:Label>
                </div>
                        </div>
                    </div>
                </section>
                <!-- /.content -->
            </div>
            <!-- /.content-wrapper -->
            <footer class="main-footer text-center">
                <strong>Copyright &copy; 2017 <a href="#">##</a>.</strong> All rights reserved.
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
                 function ValidateDelete() {
                     var Check = confirm('Are you sure you want to delete this record ?')
                     if (Check == true) {
                         return true;
                     }
                     else {
                         return false;
                     }
                 }
                
 </script> 
    </form>
</body>
</html>
