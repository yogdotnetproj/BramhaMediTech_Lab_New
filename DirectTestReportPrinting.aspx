 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="DirectTestReportPrinting.aspx.cs" Inherits="DirectTestReportPrinting" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Print Report </title>
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
<body >
    <form id="form1" runat="server">
      <asp:scriptmanager  id="scr" runat="server">
    </asp:scriptmanager>
    
   
            
            <!-- Left side column. contains the logo and sidebar -->
          
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                        
                            <div class="row">
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Reg # : <asp:Label ID="lblRegNo" runat="server" Text="RegNo"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Name :  <asp:Label ID="lblName" runat="server" Text="Name"  Font-Bold="True" Width="186px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Age :  <asp:Label ID="lblage" runat="server" Text="Age"  Font-Bold="True" Width="51px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Gender :  <asp:Label ID="lblSex" runat="server" Text="Sex"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-12">&nbsp;</div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Mobile : <asp:Label ID="LblMobileno" runat="server" Width="96px" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Center :  <asp:Label ID="Lblcenter" runat="server" Width="175px" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Date : <asp:Label ID="lbldate" runat="server"  Font-Bold="True" Width="142px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Ref Dr. :<asp:Label ID="LblRefDoc" Font-Bold="True" runat="server"  Width="180px"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
            <asp:Label ID="Label1" runat="server" Text=" " Width="51px" Font-Bold="True"></asp:Label>
                              
                              <div class="col-lg-12">
                              &nbsp;
                              </div>
                                <div class="col-lg-12">
                                     
                                      <table   >
                <tr  >
                   <td align="center" colspan="2" style="width: 100%; vertical-align: top; text-align: left; height: 170px;">
             <asp:TreeView ID="tvGroupTree" runat="server" ShowCheckBoxes="Leaf">        
                 <ParentNodeStyle Font-Bold="False" />
                 <RootNodeStyle BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" />
    </asp:TreeView>
            </td>
            <td align="center" colspan="2" style="width: 100%; vertical-align: top; text-align: left; height: 170px;">
                <asp:TreeView ID="tvGroupGram" runat="server" Visible="false" ShowCheckBoxes="Leaf">
                    <RootNodeStyle BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" />
                </asp:TreeView>
             </td>
           
                </tr>
               
            </table>
                                   
                        
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                <asp:Button ID="btnBack" runat="server"  OnClick="btnBack_Click" class="btn btn-primary"  Text="Back" ToolTip="Preview" />
                                    <asp:Button ID="cmdPrint" runat="server"  OnClick="cmdPrint_Click2"  class="btn btn-primary" Text="Print Report" ToolTip="Print" />
                                    <asp:Button ID="btnprint_letterhead" class="btn btn-primary" runat="server" onclick="btnprint_letterhead_Click" Text="Report With LetterHead"  />
                                <asp:CheckBox ID="Chkdocemail" Text="Ref Doc Email"  Width="100px" runat="server" 
                                     AutoPostBack="True" oncheckedchanged="Chkdocemail_CheckedChanged"   />
                                       <asp:CheckBox ID="Chkemailtopatient" Text="Patient Email"  Width="100px" runat="server"  AutoPostBack="True" 
                                      oncheckedchanged="Chkemailtopatient_CheckedChanged" />
                                        <asp:TextBox ID="txtpatientmail" Text=""  Width="200px" runat="server" 
                                        AutoPostBack="True" ontextchanged="txtpatientmail_TextChanged"    />

                                      <asp:CheckBox ID="chkispatientsms" Text="Patient SMS" runat="server"    Width="100px"
                                     AutoPostBack="True" oncheckedchanged="chkispatientsms_CheckedChanged" />
                                     <asp:CheckBox ID="chkcentermailLetter" Text="Center Mail withLetter" 
                                        runat="server"    Width="100px"
                                     AutoPostBack="True" oncheckedchanged="chkcentermailLetter_CheckedChanged"  />
                                      <asp:CheckBox ID="chkcentermailwithotLetter" Text="Center Mail withoutLetter" 
                                        runat="server"    Width="100px"
                                     AutoPostBack="True" 
                                        oncheckedchanged="chkcentermailwithotLetter_CheckedChanged"  />
                                        <asp:Label ID="Label6" runat="server" SkinID="errmsg" Text=""
                    Width="239px"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                         <asp:Label ID="Label44" runat="server" Text="" ></asp:Label>
                         </div>
                       </div>
                       </div>
                         <div id="Div1" class="box-footer" runat="server" visible="false">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                   <cr:crystalreportviewer ID="CVTest" runat="server" AutoDataBind="true" 
                                ReportSourceID="mmmm"  
                                HasToggleGroupTreeButton="False" Height="9051px" Width="763px" 
                                EnableTheming="True" ShowAllPageIds="True" OnInit="CVTest_Init" 
                                ReuseParameterValuesOnRefresh="True" SeparatePages="False" 
                                DisplayToolbar="False" OnPreRender="CVTest_PreRender" PrintMode="ActiveX" />
            <cr:crystalreportsource ID="mmmm" runat="server">
                <Report FileName="~//DiagnosticReport//Pateintreportnondescriptive_email.rpt">
                </Report>
            </cr:crystalreportsource>

             <cr:crystalreportviewer ID="CVMemo" runat="server" AutoDataBind="true" 
                                ReportSourceID="nnnn"  
                                HasToggleGroupTreeButton="False" Height="9051px" Width="763px" 
                                EnableTheming="True" ShowAllPageIds="True" OnInit="CVMemo_Init" 
                                ReuseParameterValuesOnRefresh="True" SeparatePages="False" 
                                DisplayToolbar="False" OnPreRender="CVMemo_PreRender" PrintMode="ActiveX" />
            <cr:crystalreportsource ID="nnnn" runat="server">
                <Report FileName="~//DiagnosticReport//Pateintreportdescriptive_Email.rpt">
                </Report>
            </cr:crystalreportsource>
           
                    <asp:HiddenField ID="hdReportno"   runat="server" />
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                                </div>
                                </div>
                                </div>

                    </div>
                </section>
                <!-- /.content -->
          
            <!-- /.content-wrapper -->
           
      
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
        <script src="plugins/ckeditor/ckeditor.js"></script>
        <script src="plugins/theme/js/theme.min.js"></script>
       
        
   
     <script language="javascript" type="text/javascript">
         function OpenReport() {
             window.open("Reports.aspx");
         } 
               </script>   
    </form>
</body>
</html>
