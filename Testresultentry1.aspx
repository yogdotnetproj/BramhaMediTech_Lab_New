 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Testresultentry1.aspx.cs" Inherits="Testresultentry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Test Result Entry  </title>
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
        <link href="App_Themes/Default/GVCss.css" rel="stylesheet" type="text/css" />

   <!--     
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   <script src="ASPSnippets_Pager.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(function () { 
        GetCustomers1(1);
    });
    $(".Pager .page").live("click", function () {
        GetCustomers(parseInt($(this).attr('page')));
    });
    function GetCustomers1(pageIndex) {
        $.ajax({
            type: "POST",
            url: "Testresultentry.aspx/GetCustomers",
            data: '{pageIndex: ' + pageIndex + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });
    }

    function OnSuccess(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
        var customers = xml.find("Customers");
        var row = $("[id*=GVTestentry] tr:last-child").clone(true);
        $("[id*=GVTestentry] tr").not($("[id*=GVTestentry] tr:first-child")).remove();
        $.each(customers, function () {
            var customer = $(this);
            $("td", row).eq(1).html($(this).find("RegistratonDateTime").text());
            $("td", row).eq(2).html($(this).find("RegNo").text());
            $("td", row).eq(3).html($(this).find("CenterCode").text());

            $("td", row).eq(4).html($(this).find("BarcodeID").text());
            $("td", row).eq(5).html($(this).find("FullName").text());
            $("td", row).eq(6).html($(this).find("Sex").text());
            $("td", row).eq(7).html($(this).find("MYD").text());
            $("td", row).eq(8).html($(this).find("DrName").text());
            $("td", row).eq(9).html($(this).find("TestName").text());

            $("td", row).eq(10).html($(this).find("SampleStatusNew").text());
            $("td", row).eq(11).html($(this).find("p_remark").text());
            $("td", row).eq(12).html($(this).find("LabRegMediPro").text());

            $("td", row).eq(13).html($(this).find("Labno").text());
            $("td", row).eq(14).html($(this).find("FID").text());
            $("td", row).eq(15).html($(this).find("PID").text());
            $("td", row).eq(16).html($(this).find("maindeptid").text());
            $("td", row).eq(17).html($(this).find("Maintestname").text());
            $("td", row).eq(18).html($(this).find("MTCode").text());



            $("[id*=GVTestentry]").append(row);
            row = $("[id*=GVTestentry] tr:last-child").clone(true);
        });
        var pager = xml.find("Pager");
        $(".Pager").ASPSnippets_Pager({
            ActiveCssClass: "current",
            PagerCssClass: "pager",
            PageIndex: parseInt(pager.find("PageIndex").text()),
            PageSize: parseInt(pager.find("PageSize").text()),
            RecordCount: parseInt(pager.find("RecordCount").text())
        });
    };
</script>

<style type="text/css">
    body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    .Pager span
    {
        text-align: center;
        color: #999;
        display: inline-block;
        width: 20px;
        background-color: #A1DCF2;
        margin-right: 3px;
        line-height: 150%;
        border: 1px solid #3AC0F2;
    }
    .Pager a
    {
        text-align: center;
        display: inline-block;
        width: 20px;
        background-color: #3AC0F2;
        color: #fff;
        border: 1px solid #3AC0F2;
        margin-right: 3px;
        line-height: 150%;
        text-decoration: none;
    }
</style> -->

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
                    <h1>Test Result Entry</h1>
                    <ol class="breadcrumb">
                       <li><a href="Login.aspx"><i class="fa fa-fw fa-lock"></i> Login</a></li>
                        <li><a href="Login.aspx"><i class="fa fa-fw fa-power-off"></i> Log out</a></li>
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Test Result Entry</li>
                    </ol>
                </section>
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-lg-3">
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
                                <div class="col-lg-3">
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
                                <div class="col-lg-3">
                                    <div class="form-group">
                                      
                                      <!--  <select class="form-control">
                                            <option>All</option>
                                            <option>Another Center</option>
                                            <option>Other Center</option>
                                        </select> -->
                                       <!--   <asp:textbox id="txtCollCode" class="form-control" placeholder="Select Lab No" runat="server" AutoPostBack="false">
                            </asp:textbox>-->

                             <asp:textbox id="txtPPID" class="form-control" placeholder="Select PPID No" runat="server" AutoPostBack="false">
                            </asp:textbox>
                             <!-- <cc1:AutoCompleteExtender id="AutoCompleteExtender2" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txtCollCode" ServiceMethod="Getcenter" CompletionListElementID="Div2">
                            </cc1:AutoCompleteExtender> -->
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                        
                                            
                                            <asp:textbox id="txttestname" placeholder="select Dept Name" class="form-control" runat="server" >
                            </asp:textbox>
                            <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Div3">
                                                    </div>
                                         <cc1:AutoCompleteExtender id="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txttestname" ServiceMethod="GetDeptName" CompletionListElementID="Div3">
                            </cc1:AutoCompleteExtender>
                                        
                                      
                                      

                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <!--  <input type="text" class="form-control pull-right" placeholder="Enter Patient Name">-->
                                        <asp:textbox id="txtPatientName" runat="server" class="form-control pull-right" style="position: relative" tabindex="4" placeholder="Enter Patient Name">
                            </asp:textbox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                     <!--   <input type="text" class="form-control pull-right" placeholder="Enter Mobile Number"> -->
                                         <asp:textbox id="txtmobileno" runat="server" class="form-control pull-right" placeholder="Enter Ref Doc" tabindex="5">
                            </asp:textbox>
                              <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Div4">
                                                    </div>
                             <cc1:AutoCompleteExtender id="AutoCompleteExtender3" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txtmobileno" ServiceMethod="GetDoctor" CompletionListElementID="Div4">
                            </cc1:AutoCompleteExtender>
                                    </div>
                                </div> 
                                <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                       <!-- <input type="text" class="form-control pull-right" placeholder="Enter Registration Number"> -->
                                        <asp:textbox id="txtregno"  runat="server" class="form-control pull-right" tabindex="5" placeholder="Enter Registration Number">
                            </asp:textbox>
                                    </div>
                                </div> 
                                 <div class="col-lg-3">
                                    <div class="form-group">
                                       
                                       <!-- <input type="text" class="form-control pull-right" placeholder="Enter Registration Number"> -->
                                        <asp:textbox id="txtcentername_new" class="form-control" placeholder="Center Name" runat="server" AutoPostBack="false">
                            </asp:textbox>
                             <div style="display: none; overflow: scroll; width: 227px; height: 100px; text-align: right"
                                                        id="Div5">
                                                    </div>
                              <cc1:AutoCompleteExtender id="AutoCompleteExtender4" runat="server" MinimumPrefixLength="1" 
                            TargetControlID="txtcentername_new" ServiceMethod="Getcenter" CompletionListElementID="Div5">
                            </cc1:AutoCompleteExtender> 
                                    </div>
                                </div>   
                                


                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                        <asp:RadioButtonList ID="ddlStatus" runat="server" placeholder="select status" RepeatDirection="Horizontal" 
                                            AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
    <asp:ListItem >Pending</asp:ListItem>
    <asp:ListItem>Completed</asp:ListItem>
     <asp:ListItem>Tested</asp:ListItem>
      <asp:ListItem>Authorized</asp:ListItem>
       <asp:ListItem>Emergency</asp:ListItem>
    <asp:ListItem Selected="True">All</asp:ListItem>
   
                                        </asp:RadioButtonList>


                                       
                            <asp:sqldatasource id="sqlStatus" runat="server" connectionstring="<%$ ConnectionStrings:myconnection %>"
                                selectcommand="SELECT * FROM [DrMT]  where DrType='CC' order by DoctorName">
                            </asp:sqldatasource>
                                        </div>
                                        </div>
                                          
                                        <div class="col-lg-4">
                                    <div class="form-group">    
                                    <asp:Label ID="Label1" runat="server" class="btn btn-sm btn-primary" Font-Bold="true" ForeColor="white" Text="Total Test Count is: "></asp:Label>
<asp:Label ID="lblpcount" runat="server" Font-Bold="true" ForeColor="black" Text=""></asp:Label>
                                    </div>
                                    </div>  

                                    
                                     <div class="col-lg-4">
                                    <div class="form-group"> 
                                    <asp:button id="btnList" runat="server" onclick="btnList_Click" class="btn btn-primary" text="Click"
                                 onclientclick=" return validate();" tabindex="7" />
                                    </div>
                                    </div>         
                            </div>
                        </div>
                       
                    </div>
                    <div class="box">
                        <div class="box-body">
                           <div class="table-responsive" style="width:100%">
                            <asp:gridview id="GVTestentry" runat="server"   class="table table-responsive table-sm " datakeynames="PatRegID" autogeneratecolumns="False"
                                width="100%" onrowdatabound="GVTestentry_RowDataBound" allowpaging="True" onpageindexchanging="GVTestentry_PageIndexChanging"
                                pagesize="30" 
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 onrowediting="GVTestentry_RowEditing" onselectedindexchanged="GVTestentry_SelectedIndexChanged">
                                <columns>
                               
                                <asp:HyperLinkField HeaderText="Submit" Text="Submit" DataNavigateUrlFields="PatRegID,FID" DataNavigateUrlFormatString="Addresult.aspx?PatRegID={0}&amp;FID={1}" />
 <asp:BoundField DataField="RegistratonDateTime" HeaderText=" Date" />
<asp:BoundField DataField="PatRegID" HeaderText="Reg.No" />
<asp:BoundField DataField="CenterCode" HeaderText="Center"  />


  <asp:TemplateField HeaderText="FullName">
                            <ItemTemplate>
                            <asp:Label ID="lblfullname" runat="server" Text='<%#Eval("FullName") %>'></asp:Label>
                            <asp:ImageButton ID="btnEmergency"  class="flashingTextcss" ImageUrl="~/images/light-311119__340.png" runat="server"></asp:ImageButton>

                            </ItemTemplate>
                            </asp:TemplateField>

<asp:BoundField DataField="Sex" HeaderText="Gender" />
<asp:BoundField DataField="MYD" HeaderText="Age" />
<asp:BoundField DataField="DrName" HeaderText="Ref Doc" />



<asp:TemplateField HeaderText ="Test ">
    <ItemTemplate>
        <asp:Label ID="lbltestname" runat="server" Text='<%#Eval("TestName") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="SampleStatusNew" HeaderText=" Status" />


<asp:BoundField DataField="P_remark" HeaderText="Remark" />
<asp:BoundField DataField="LabRegMediPro" HeaderText="Inv no" />
<asp:BoundField DataField="Labno" HeaderText="Lab no" />
    <asp:CommandField EditText="Report" HeaderText="Report"  ShowEditButton="True" />
   
   <asp:BoundField DataField="ReRunType" HeaderText="Is Rerun" />
   <asp:BoundField DataField="MailStatus" HeaderText=" Mail Status" />
     
<asp:TemplateField>
    <ItemTemplate>
    <a href=""><i class="fa fa-download"    style="color:#fff"></i></a>
        <asp:HiddenField ID="hdnFID1" runat="server" Value='<%#Eval("FID") %>' /> 
        <asp:HiddenField ID="hdnis_emergency" runat="server" Value='<%#Eval("Isemergency") %>' />
        <asp:HiddenField ID="HDPID" runat="server" Value='<%#Eval("PID") %>' /> 
         <asp:HiddenField ID="hdnPatRegID" runat="server" Value='<%#Eval("PatRegID") %>' /> 
          <asp:HiddenField ID="hdnMaindept" runat="server" Value='<%#Eval("DigModule") %>' /> 
           <asp:HiddenField ID="hdn_Maintestname" runat="server" Value='<%#Eval("TestName") %>' /> 
           <asp:HiddenField ID="hdn_MTcode" runat="server" Value='<%#Eval("MTCode") %>' /> 
            
    </ItemTemplate>
</asp:TemplateField>


</columns>


                            </asp:gridview>
                             <div class="Pager"></div>
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
        <script language ="javascript" type ="text/javascript" >
            function jsPopup(jsURL) {
                var hdl; if (jsURL != "") {

                    var jsoption = "scrollbars=yes,resizable=yes,width=500,height=500,left=100,top=100,status=yes";
                    hdl = window.open(jsURL, "win01", jsoption);

                }

            }

 

</script>
<script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
        <script>
            $(document).ready(function () {
                var speed = 500;
                function effectFadeIn(classname) {
                    $("." + classname).fadeOut(speed).fadeIn(speed, effectFadeOut(classname))
                }
                function effectFadeOut(classname) {
                    $("." + classname).fadeIn(speed).fadeOut(speed, effectFadeIn(classname))
                }
                //Calling fuction on pageload
                $(document).ready(function () {
                    effectFadeIn('flashingTextcss');
                });
            });
  </script>
    </form>
</body>
</html>
