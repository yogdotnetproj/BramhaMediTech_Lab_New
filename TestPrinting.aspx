 <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestPrinting.aspx.cs" Inherits="TestPrinting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc" Namespace="Winthusiasm.HtmlEditor" Assembly="Winthusiasm.HtmlEditor" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <style type="text/css">
        .style4
        {
            width: 149px;
        }
        .style8
        {
            width: 249px;
        }
        </style>
          <script src="ckeditor/ckeditor.js"></script>
         <script>
             CKEDITOR.replace('editor1', {
                 height: 250,
                 // Remove the WebSpellChecker plugin.
                 removePlugins: 'wsc',
                 // Configure SCAYT to load on editor startup.
                 scayt_autoStartup: true,
                 // Limit the number of suggestions available in the context menu.
                 scayt_maxSuggestions: 3
             });
	</script>
    
<script language="javascript" type="text/javascript" >

    function printDiv(divID) {
        //Get the HTML of div
        var divElements = document.getElementById(divID).innerHTML;
        //Get the HTML of whole page
        var oldPage = document.body.innerHTML;

        //Reset the page's HTML with div's HTML only
        document.body.innerHTML =
              "<html><head><title></title></head><body><table><tr></tr><tr></tr><tr></tr><tr></tr></table>" +
              divElements + "</body>";

        //Print Page
        window.print();

        //Restore orignal HTML
        document.body.innerHTML = oldPage;
    }

    function getPrint(print_area) {
        //Creating new page
        var pp = window.open();
        //Adding HTML opening tag with <HEAD> … </HEAD> portion 
        pp.document.writeln('<HTML><HEAD><title>Print Preview</title>')
        pp.document.writeln('<LINK href=Styles.css type="text/css" rel="stylesheet">')
        pp.document.writeln('<LINK href=PrintStyle.css ' +
                        'type="text/css" rel="stylesheet" media="print">')
        pp.document.writeln('<base target="_self"></HEAD>')

        //Adding Body Tag
        pp.document.writeln('<body MS_POSITIONING="GridLayout" bottomMargin="0"');
        pp.document.writeln(' leftMargin="0" topMargin="0" rightMargin="0">');
        //Adding form Tag
        pp.document.writeln('<form method="post">');

        //Creating two buttons Print and Close within a HTML table
        pp.document.writeln('<TABLE width=100%><TR><TD></TD></TR><TR><TD align=right>');
        pp.document.writeln('<INPUT ID="PRINT" type="button" value="Print" ');
        pp.document.writeln('onclick="javascript:location.reload(true);window.print();">');
        pp.document.writeln('<INPUT ID="CLOSE" type="button" ' +
                        'value="Close" onclick="window.close();">');
        pp.document.writeln('</TD></TR><TR><TD></TD></TR></TABLE>');

        //Writing print area of the calling page
        pp.document.writeln(document.getElementById(print_area).innerHTML);
        //Ending Tag of </form>, </body> and </HTML>
        pp.document.writeln('</form></body></HTML>');
    }
</script>
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
                    <h1>Descriptive Result</h1>
                    <ol class="breadcrumb">
                        <li><a href="Login.aspx"><i class="fa fa-fw fa-lock"></i> Login</a></li>
                        <li><a href="Login.aspx"><i class="fa fa-fw fa-power-off"></i> Log out</a></li>
                        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Descriptive Result</li>
                    </ol>
                </section>
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
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        
                                      
                                       
                     <asp:textbox id="txtTestcode" placeholder="Enter Format Name"  runat="server" class="form-control">  </asp:textbox>
                    <asp:requiredfieldvalidator id="TestCodeRequiredFieldValidator" runat="server" controltovalidate="txtTestcode"
                        errormessage="Please enter the subdeptName" style="position: relative" validationgroup="Format"
                        font-bold="True">
                    </asp:requiredfieldvalidator>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       
                                        
                                <asp:DropDownList ID="ddlDoctor_new" runat="server" class="form-control" >
                                                <asp:ListItem Value="0">Select Doctor</asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                </div> 
                                <div class="col-lg-4">
                                    <div class="form-group">
                                      
                                       
                                          <asp:dropdownlist id="CmbFormatName" runat="server"  class="form-control" autopostback="True"
                                            onselectedindexchanged="CmbFormatName_SelectedIndexChanged"> </asp:dropdownlist>
                                           
                                            <asp:TextBox ID="txtFormatName"  runat="server"  class="form-control" 
                                        AutoPostBack="true" ontextchanged="txtFormatName_TextChanged"></asp:TextBox>
                                   
                                 
                                    </div>
                                </div>    
                                
                                 <div class="col-lg-12">
                                    <div class="form-group">
                                    <table id="Table1" width="100%" runat="server">
                                    <tr>
                                    <td align="center">
                                    
                                     <label>Authorize</label>
                                    <asp:checkbox id="chkAuthorize" runat="server" />
                                    <asp:label id="lblSelectDocError" runat="server" skinid="errmsg"></asp:label>
                                    <asp:label style="position: relative" id="lblValidate" runat="server" width="472px"
                        visible="False" skinid="errmsg"></asp:label>
                                    </td>
                                    
                                    </tr>
                                    </table>
                                   
                                    </div>
                                 </div>
                                           
                                            <div class="col-lg-12">
                                    <div class="form-group">
                                     <table id="Table2" width="100%" runat="server">
                                    <tr>
                                    <td align="center" >
                                    <label>Result</label>
                                   <!--   <cc:HtmlEditor ID="Editor" runat="server" Height="400px" Style="text-indent: 2px"  Width="800px" />-->

                                       <asp:TextBox ID="Editor1" runat="server" Height="2500px" TextMode="MultiLine"></asp:TextBox>
<script type="text/javascript" lang="javascript">    CKEDITOR.replace('<%=Editor1.ClientID%>');</script> 
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
                                           <asp:button id="Button1" runat="server" Visible="false" text="Print" OnClientClick="javascript:window.print();" class="btn btn-primary"     validationgroup="Format" />
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
</asp:Content>

