 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestResultDescriptiveReport.aspx.cs" Inherits="TestResultDescriptiveReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Descriptive Report  </title>
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
</head>
<body class="hold-transition skin-black sidebar-mini">
    <form id="form1" runat="server">
    <div  class="regular">
    <div id="div2" class="rounded_corners"  >
   <table   cellpadding="2" cellspacing="0"  style="width: 100%; border-right: threeddarkshadow 2px solid;
                    border-top: threeddarkshadow 2px solid; border-left: threeddarkshadow 2px solid;
                    border-bottom: threeddarkshadow 2px solid;">

            <tr>
                <td style="width: 10px">
                </td>
                <td>
    <hr />
        <table style="width: 100%">
            <tr>
                <td style="width: 172px">
                    <div style="position: relative">
                        Patient Name</div>
                </td>
                <td style="width: 7px">
                    :</td>
                <td style="width: 221px">
                    <asp:Label ID="lblPatName" runat="server"></asp:Label></td>
                <td style="width: 10px">
                </td>
                <td style="width: 133px">
                    Reg No.</td>
                <td style="width: 9px">
                    :</td>
                <td>
                    <asp:Label ID="lblRegNo" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 172px">
                    <div style="position: relative">
                        Age Sex</div>
                </td>
                <td style="width: 7px">
                    :</td>
                <td style="width: 221px">
                    <asp:Label ID="lblAgeNSex" runat="server"></asp:Label></td>
                <td style="width: 10px">
                </td>
                <td style="width: 133px">
                    Client Code</td>
                <td style="width: 9px">
                    :</td>
                <td>
                    <asp:Label ID="lblPSC" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 172px">
                    <div style="position: relative">
                        Referring Doctor</div>
                </td>
                <td style="width: 7px">
                    :</td>
                <td style="width: 221px">
                    <asp:Label ID="lblRefDoctor" runat="server"></asp:Label></td>
                <td style="width: 10px">
                </td>
                <td style="width: 133px">
                    <div style="position: relative">
                        Sample Drawn Date</div>
                </td>
                <td style="width: 9px">
                    :</td>
                <td>
                    <asp:Label ID="lblSampleDrawnDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 172px">
                    <div style="position: relative">
                        Sample Name</div>
                </td>
                <td style="width: 7px">
                    :</td>
                <td style="width: 221px">
                    <asp:Label ID="lblSampleName" runat="server"></asp:Label></td>
                <td style="width: 10px">
                </td>
                <td style="width: 133px">
                    <div style="position: relative">
                       Reg. Date</div>
                </td>
                <td style="width: 9px">
                    :</td>
                <td>
                    <asp:Label ID="lblRegDate" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 172px">
                    <div style="position: relative">
                        Sample Drawn By</div>
                </td>
                <td style="width: 7px">
                    :</td>
                <td style="width: 221px">
                    <asp:Label ID="lblPSCName" runat="server"></asp:Label></td>
                <td style="width: 10px">
                </td>
                <td style="width: 133px">
                    <div style="position: relative">
                        Report Date</div>
                </td>
                <td style="width: 9px">
                    :</td>
                <td>
                    <asp:Label ID="lblReportDate" runat="server"></asp:Label></td>
            </tr>
        </table>
                    <br />
                    <hr />
                    &nbsp;</td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td align="center">
                    <asp:Label ID="lblDeptName" runat="server" Font-Bold="True"></asp:Label></td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="height: 306px; width: 10px;">
                </td>
                <td style="height: 306px" valign="top">
                    <asp:Label ID="lblMemoText" runat="server"></asp:Label></td>
                <td style="height: 306px">
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 10px; height: 21px;">
                </td>
                <td style="height: 21px">
                    </td>
                <td style="height: 21px">
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td>
                    <div >
                        <table style="width: 835px" >
                            <tr>
                                <td style="height: 55px" valign="bottom" >
                                    Barcode ID :
                                    <br />
                    <asp:Label ID="lblVialID" runat="server"></asp:Label></td>
                                <td align="center" style="height: 55px" valign="bottom">
                                <img runat="server" id="imgsignid" alt="" />&nbsp;<br />
                                    
                                    <asp:Label ID="lblsign" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblDesignation" runat="server" ></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </div>
    </form>
</body>
</html>
