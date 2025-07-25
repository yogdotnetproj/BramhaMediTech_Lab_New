 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddFinanceYear.aspx.cs" Inherits="AddFinanceYear" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Add Finance Year</title>
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
    color: #7d7d8e !important;
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
<body>
    <form id="form1" runat="server">
    <div>
    

     <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">

                            <div class="col-lg-12">
                                    <div class="form-group">
                                     <asp:Label ID="Label1" runat="server" Text="Add New Financial Year:"></asp:Label>
                                    </div>
                                    </div>
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
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
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
                                <div class="col-lg-4">
                                    <div class="form-group">
                                      
                                    

                            .
                             
                                    </div>
                                </div>
                                  <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-8 text-center">
                                    
                                    <asp:button id="btnsave" runat="server"   
                                        text="Save" tooltip="Save"
                               class="btn btn-primary" onclick="btnsave_Click"  />
<asp:Label ID="LblMsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                                </div>
                                </div>
                                </div>
    </div>
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
      <script src="js0/bootstrap.js" type="text/javascript"></script>
    <script src="js0/jquery-1.11.0.min.js" type="text/javascript"></script>
     
        <link href="css1/11.css" rel="stylesheet" type="text/css" />
        <link href="css1/12.css" rel="stylesheet" type="text/css" />
         <script src="js0/1.js" type="text/javascript"></script>
         <script src="js0/2.js" type="text/javascript"></script>
           <script src="js0/3.js" type="text/javascript"></script>
</body>
</html>
