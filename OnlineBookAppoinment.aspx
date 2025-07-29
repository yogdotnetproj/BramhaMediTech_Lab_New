<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlineBookAppoinment.aspx.cs" Inherits="OnlineBookAppoinment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Registration  </title>
        <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
        <link rel="stylesheet" href="plugins/bootstrap/css/bootstrap.min.css">
       
        
        <link rel="stylesheet" href="plugins/theme/css/theme.min.css">
        <link rel="stylesheet" href="plugins/theme/css/skins/_all-skins.min.css">
        <link rel="stylesheet" href="css/style.css">

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
            <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
            <![endif]-->

      <!--  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"> -->
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

     

         <link href="App_Themes/Default/GVCss.css" rel="stylesheet" type="text/css" />

         
   
   
    <link href="css/bootstrap-datepicker.css"  rel="stylesheet" />
    <link href="css1/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="css1/bootstrap.min.css" rel="stylesheet" type="text/css" />
   
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
    </head>
<body class="hold-transition skin-black sidebar-mini">
    <form id="form1" runat="server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
   <%-- <div  class="regular">--%>
 <%-- <div class="wrapper">--%>
           <%-- <header class="main-header">
                <!-- Logo -->
                <a href="dashboard.html" class="logo">
                    <!-- mini logo for sidebar mini 50x50 pixels -->
                   <span class="logo-mini"> <asp:Label ID="LblDCCode" runat="server" Text=" " > </asp:Label> </span>
                    <!-- logo for regular state and mobile devices -->
                    <span class="logo-lg">
                    <asp:Label ID="LblDCName" runat="server" Text=" " ></asp:Label></span>
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
            </header>--%>
            <!-- Left side column. contains the logo and sidebar -->
           
            <%--<aside class="main-sidebar">
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
                     <ul class="parentEle" id="newlist"></ul>
                    <ul class="sidebar-menu" data-widget="tree">
                        <li class="treeview">

                         <asp:TreeView ID="TrMenu"  Visible="true" runat="server"  ExpandDepth="1"   
                onselectednodechanged="TrMenu_SelectedNodeChanged"  class="logo-lg" >
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
            </aside>--%>
           
         
            <!-- Content Wrapper. Contains page content -->
            <%--<div class="content-wrapper">--%>
                <!-- Content Header (Page header) -->
               
                <section class="content-header">
                    <h1>Appoinment Registration</h1>
                    <ol class="breadcrumb">

                    
                    <%--    <li><a href="Home.aspx"><i class="fa fa-dashboard"></i> Home</a></li> --%>
                       
                        <li class="active">Appoinment Registration</li>
                    </ol>
              </section>

                <!-- Main content -->
                <section class="content">
                 <div class="row">
                  <div class="col-lg-12">
                    <div class="box">                   
                        </div>
                            <div class="box-body">  
                                                 
                                <div class="row">                               
                                   


                                    <div class="col-lg-6">
                                        <div class="form-group">
                                          
                                            <div class="row">
                                                <div class="col-lg-3 col-xs-4">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbInitial" TabIndex="0" Height="30px" runat="server" class="form-control"
                 OnSelectedIndexChanged="cmbInitial_SelectedIndexChanged" 
                 AutoPostBack="True">
                                    </asp:DropDownList>
                                      <asp:HiddenField ID="hdnstatus" runat="server" Value=0 />
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-lg-9 col-xs-8">
                                                   <!-- <input type="text" class="form-control" id="" placeholder="Enter Name">-->
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                       <asp:TextBox ID="txtFname" runat="server" placeholder="Enter Name" Height="30px" TabIndex="1" class="form-control" 
                 AutoPostBack="false"></asp:TextBox>  
                              
                                  
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFname"
                                        Display="Dynamic" ErrorMessage="This field is required" SetFocusOnError="True"
                                        ValidationGroup="ValidateForm">
                                    </asp:RequiredFieldValidator>
                                      </ContentTemplate>
                </asp:UpdatePanel> 
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    
                                    <div class="col-lg-2">
                                        <div class="form-group">
                                           
                                            <div class="row">
                                                <div class="col-lg-12 col-xs-12">
                                                    <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlsex" runat="server" Height="30px" TabIndex="2" class="form-control">
            <asp:ListItem>Gender</asp:ListItem>
            <asp:ListItem>Male</asp:ListItem>
            <asp:ListItem>Female</asp:ListItem>
       </asp:DropDownList>
       </ContentTemplate>
       </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                     <div class="col-lg-2" runat="server" visible="false" id="Slo">
                                        <div class="form-group">
                                           
                                            <div class="row">
                                                <div class="col-lg-12 col-xs-12">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                        <asp:DropDownList ID ="ddlToTime" class="form-control" runat="server"></asp:DropDownList>

       </ContentTemplate>
       </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-lg-2" runat="server" visible="false" id="Slo1">
                                        <div class="form-group">
                                           
                                            <div class="row">
                                                <div class="col-lg-12 col-xs-12">
                                                    
                                                        <div class="input-group date">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            
                                            <asp:TextBox id="fromdate" runat="server" data-date-format="dd/mm/yyyy"  class="form-control pull-right"  tabindex="1">
                                      </asp:TextBox>
                                        </div>

      
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-lg-3">
                                        <div class="form-group">
                                           
                                            <div class="row">
                                                <div class="col-lg-6 col-xs-8">
                                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtAge" runat="server" onkeypress="return NumberOnly()" placeholder="Age" class="form-control" Height="30px" TabIndex="3" MaxLength="3" >
                                    </asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="col-lg-6 col-xs-4">
                                                       <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                                <ContentTemplate>
                                                     <asp:DropDownList ID="cmdYMD" runat="server" Height="30px" TabIndex="4" class="form-control">
                                
                                <asp:ListItem>Year</asp:ListItem>
                                <asp:ListItem>Month</asp:ListItem>
                                <asp:ListItem>Day</asp:ListItem>
                            </asp:DropDownList>  
                            </ContentTemplate>
                            </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txttelno" placeholder="Enter Mobile" runat="server" Height="30px" class="form-control" 
                                                                TabIndex="5" MaxLength="15" ></asp:TextBox>
                                                     </ContentTemplate>
                                                     </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group">
                                                    
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txtemail" placeholder="Enter Email" runat="server" Height="30px" TabIndex="6" class="form-control">     </asp:TextBox>
                                                      </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                  
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                                <ContentTemplate>
                                                              <asp:TextBox ID="txt_address" runat="server" placeholder="Enter address" TabIndex="7" Height="30px" TextMode="MultiLine" class="form-control">
                            </asp:TextBox>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div3" class="col-lg-6"  runat="server" visible="false">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           
                                                            <asp:TextBox ID="txtpanno" placeholder="Enter Pan No" runat="server" Height="50px" class="form-control" TextMode="MultiLine" >
                                    </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div4" class="col-lg-6"  runat="server" visible="false">
                                                <div class="form-group">
                                                    
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                           
                                                            <asp:TextBox ID="txt_clinicalhistory" placeholder="Enter clinical history" runat="server" Height="50px" class="form-control"  TextMode="MultiLine" >
                                    </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                     <div id="Div5" class="col-lg-4" runat="server" >
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            
                                                            <asp:TextBox ID="txtstate" placeholder="Enter State" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div6" class="col-lg-4" runat="server" >
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                            
                                                            
        <asp:TextBox ID="txtDistrict" runat="server" placeholder="Enter District" class="form-control">
                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="Div7" class="col-lg-4" runat="server" >
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                          
                                                             <asp:TextBox ID="txtcity" placeholder="Enter city/Village" runat="server" class="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                          <!--  <input type="text" class="form-control" id="" placeholder="Enter Doctor Name">-->
                                                            <asp:TextBox ID="txtDoctorName" placeholder="Ref Doctor" Height="30px" TabIndex="8"  runat="server" class="form-control"
                                                        ontextchanged="txtDoctorName_TextChanged"></asp:TextBox> 
                                                         <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Divdoc">
                                                    </div>
                                                     <cc1:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server"
                                        CompletionListElementID="Divdoc" ServiceMethod="FillDoctor" TargetControlID="txtDoctorName"
                                        MinimumPrefixLength="1">
                                    </cc1:AutoCompleteExtender>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                   
                                                    <div class="row">
                                                        <div class="col-lg-12 col-xs-12">
                                                              <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                                <ContentTemplate>
                                                            <asp:TextBox ID="txt_remark" placeholder="Passport No" TabIndex="9" Height="30px" runat="server" class="form-control"  >
                                    </asp:TextBox>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                           
                                           
                                           <!-------------------------------------------------------------->
                                                            
                                     <!-------------------------------------------------------------------->
                                     <%-- HospAdd ------------------- --%>
                                                            <div class="row" >
                                                                 <div id="Div9"  runat="server" class="col-md-6">
                                                                    <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txtadmitpatient" runat="server" TabIndex="20" placeholder="Nationality " Height="30px"  class="form-control"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div10"  runat="server" class="col-md-6">
                                                                    <asp:UpdatePanel ID="UpdatePanel38" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:TextBox ID="txthospID" runat="server" TabIndex="20" placeholder="Enter Remark " Height="30px" TextMode="MultiLine" class="form-control"></asp:TextBox>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                </div>
                                                              <%-- EndHospAdd ------------------- --%>
                                                             <%-- SympAdd ------------------- --%>
                                                            <div class="row" style="padding-bottom: 8px" runat="server" visible="false">
                                                                 <div id="Div11"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel39" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:CheckBox ID="ChkILI" Text="ILI" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div12"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel40" runat="server">
                                                                        <ContentTemplate>
                                                                    <asp:CheckBox ID="ChlFever" Text="Fever" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div13"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel41" runat="server">
                                                                        <ContentTemplate>
                                                                             <asp:TextBox ID="txtfeverduration" placeholder="Fever Duration " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div14"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel42" runat="server">
                                                                        <ContentTemplate>
                                                                              <asp:CheckBox ID="chkcough" Text="Cough" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div15"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel43" runat="server">
                                                                        <ContentTemplate>
                                                                              <asp:TextBox ID="txtcoughduration"  placeholder="Cough Duration " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div16"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel44" runat="server">
                                                                        <ContentTemplate>
                                                                             <asp:CheckBox ID="chksari" Text="SARI" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                </div>
                                                              <%-- EndSympAdd ------------------- --%>
                                                            <div class="row" style="padding-bottom: 8px" runat="server" visible="false">
                                                                 <div id="Div17"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:TextBox ID="txtComorbidity"  placeholder="Co morbidity " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div18"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel46" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:TextBox ID="txttempreco"  placeholder="Temp Recorded " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-md-1">
                                                                <p class="label-form">Sputum</p>
                                                            </div>
                                                                <div id="Div19"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                                                                        <ContentTemplate>
                                                                            
                                                                           <asp:RadioButtonList ID="ddlsputum" RepeatDirection="Horizontal" runat="server" Width="100px" TabIndex="7" class="form-control">

                                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Selected="True" Value="No"></asp:ListItem>
                                                                                
                                                                            </asp:RadioButtonList>
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div20"  runat="server" class="col-md-5">
                                                                    <asp:UpdatePanel ID="UpdatePanel48" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:TextBox ID="txtadditionalsymptoms"  placeholder="Additional symptoms?if any  " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                </div>
                                                            <div class="row" style="padding-bottom: 8px" runat="server" visible="false">
                                                                 <div id="Div21"  runat="server" class="col-md-5">
                                                                    <asp:UpdatePanel ID="UpdatePanel49" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:Label ID="TextBox1"  Text="Travel history in last 14 days?"   Font-Bold="true" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div id="Div22"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel50" runat="server">
                                                                        <ContentTemplate>
                                                                            
                                                                           <asp:RadioButtonList ID="rblVisites" RepeatDirection="Horizontal" runat="server" Width="100px" TabIndex="7" class="form-control">

                                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Selected="True" Value="No"></asp:ListItem>
                                                                                
                                                                            </asp:RadioButtonList>
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 <div id="Div23"  runat="server" class="col-md-5">
                                                                    <asp:UpdatePanel ID="UpdatePanel51" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:TextBox ID="txtcountryvisit"   placeholder="Country visited by you (if yes) " class="form-control" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                </div>
                                                             <div class="row" style="padding-bottom: 8px" runat="server" visible="false">
                                                                 <div id="Div24"  runat="server" class="col-md-5">
                                                                    <asp:UpdatePanel ID="UpdatePanel52" runat="server">
                                                                        <ContentTemplate>
                                                                           <asp:Label ID="Label2"  Text="Is the patient admited in isolation ward/unit in hospital?"   Font-Bold="true" runat="server" />
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                  <div id="Div25"  runat="server" class="col-md-2">
                                                                    <asp:UpdatePanel ID="UpdatePanel53" runat="server">
                                                                        <ContentTemplate>
                                                                            
                                                                           <asp:RadioButtonList ID="rblisolation" RepeatDirection="Horizontal" runat="server" Width="100px" TabIndex="7" class="form-control">

                                                                                <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                                                                <asp:ListItem Text="No" Selected="True" Value="No"></asp:ListItem>
                                                                                
                                                                            </asp:RadioButtonList>
                                                                             </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                 </div>
                                     <div class="row">
                                            <div class="col-lg-12 text-center">
                                                 <asp:Label  ID="Label1" runat="server" Font-Bold="true" ForeColor="Red"  ></asp:Label>
                                                </div>
                                         </div>
                                            <div class="row">
                                            <div class="col-lg-12 text-center">
                                                
                                              
                                             
                                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                                 <asp:Button ID="btnreset" runat="server" Text="Reset" />
                                              
                                             
                                       <asp:Label  ID="Label10" runat="server" Font-Bold="true" ForeColor="Red"  ></asp:Label>
                                            </div>
                                        </div>
                                     <div class="row">
                                         
                                            <div class="col-lg-12 text-left">
                                               
                                                   <asp:Label  ID="Label3" runat="server" Text="1.Fill your details." Font-Bold="true" ForeColor="Red"  ></asp:Label>
                                                  
                                                </div>
                                         </div>
                                     <div class="row">
                                           
                                            <div class="col-lg-12 text-left">
                                                   <asp:Label  ID="Label4" runat="server" Text="2.Get Your Token Number." Font-Bold="true" ForeColor="Red"  ></asp:Label>
                                                </div>
                                         </div>
                                     <div class="row">
                                           
                                            <div class="col-lg-12 text-left">
                                                   <asp:Label  ID="Label5" runat="server" Text="3.Visit Lab at your booked time." Font-Bold="true" ForeColor="Red"  ></asp:Label>
                                                </div>
                                         </div>
                                     <div class="row">
                                           
                                            <div class="col-lg-12 text-left">
                                                   <asp:Label  ID="Label6" runat="server" Text="4.Give your sample within 5 min." Font-Bold="true" ForeColor="Red"  ></asp:Label>
                                                </div>
                                         </div>
                                     <div class="row">
                                           
                                            <div class="col-lg-12 text-left">
                                                   <asp:Label  ID="Label7" runat="server" Text="5.Collect your report online." Font-Bold="true" ForeColor="Red"  ></asp:Label>
                                                </div>
                                         </div>
                                           </div>
                                           </div>

                                          
                                          </div>
                                        </div>
                                                              
                                      <%--  </div>--%>
                                       </section>
                                   <%-- </div>--%>
                                   
                                    <div class="box-footer">
                                        
                                    </div>
 <!--</form>
                            </div>
                </section>
                
            </div>-->
            <!-- /.content-wrapper -->
            <footer class="main-footer text-center">
                 <strong>Copyright ©2019 BramhaMediTech.  <a href="#">All rights reserved.</a>.</strong> 
            </footer>
           <%-- </div>--%>
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
   <script src="js0/bootstrap.js" type="text/javascript"></script>
    <script src="js0/jquery-1.11.0.min.js" type="text/javascript"></script>
     
        <link href="css1/11.css" rel="stylesheet" type="text/css" />
        <link href="css1/12.css" rel="stylesheet" type="text/css" />
         <script src="js0/1.js" type="text/javascript"></script>
         <script src="js0/2.js" type="text/javascript"></script>
           <script src="js0/3.js" type="text/javascript"></script>
 
<script type="text/javascript">
    function NumberOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
            event.returnValue = true;
        else
            event.returnValue = false;
    }
    </script>
    </body>
</html>
