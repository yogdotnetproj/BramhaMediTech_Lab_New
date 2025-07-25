 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestReportprinting.aspx.cs" Inherits="TestReportprinting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Print Report </title>
        <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
        <link rel="stylesheet" href="cssmain/bootstrap.min.css">
        <link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
        <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css">
        <link rel="stylesheet" href="plugins/theme/css/theme.min.css">
        <link rel="stylesheet" href="plugins/theme/css/skins/_all-skins.min.css">
        <!--<link rel="stylesheet" href="css/style.css">-->
        <link rel="stylesheet" href="cssmain/main.min.css">
        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
            <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
            <![endif]-->

      <!--  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"> -->
       <style type="text/css">
		
		.extralight{
			
			font-weight: 100;
		}

		.light{
			
			font-weight: 300;
		}

		.regular{
			
			font-weight: 400;
		}

		.semibold{
			
			font-weight: 600;	
		}

		.bold{
			
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
        .btn-blue, .btn-blue:hover {

background-color: #38c8dd !important;
    color: #fff;
    border: none;
    padding: 5px 10px !important;
    border-radius:0px  !important;                  ;
    border:none !important;
}
input[type="checkbox"] {
    width:22px;
    height:22px;
    margin-right:10px
}
        #tvGroupTree {
            max-height: 342px;
    overflow-y: scroll;
        }
    </style>
        <link href="App_Themes/Default/GVCss.css" rel="stylesheet" type="text/css" />
    </head>
<body >
    <form id="form1" runat="server">
     <%-- <asp:scriptmanager  id="scr" runat="server">
    </asp:scriptmanager>--%>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
</cc1:ToolkitScriptManager>
   
            
            <!-- Left side column. contains the logo and sidebar -->
          
            <!-- Content Wrapper. Contains page content -->
         
                <!-- Content Header (Page header) -->
                
                <!-- Main content -->
                <section class="content">
                    <div class="box">
                        <div class="box-body">
                                     <div class="bg-light mb-3 p-3">
                            <div class="row mb-2">
                                <div class="col-sm-3"><span><strong>Reg # : <asp:Label ID="lblRegNo" runat="server" Text="RegNo"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                               
                                <div class="col-sm-3"><span><strong>Name :  <asp:Label ID="lblName" runat="server" Text="Name"  Font-Bold="True" Width="160px"></asp:Label></strong></span></div>

                                <div class="col-sm-3"><span><strong>Age :  <asp:Label ID="lblage" runat="server" Text="Age"  Font-Bold="True" Width="51px"></asp:Label></strong></span></div>
                                
                                <div class="col-sm-3"><span><strong>Gender :  <asp:Label ID="lblSex" runat="server" Text="Sex"  Font-Bold="True" Width="70px"></asp:Label></strong></span></div>
                                                    </div>
                            <div class="row">
                                
                                <div class="col-sm-3"><span><strong>Mobile : <asp:Label ID="LblMobileno" runat="server" Width="96px" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                                
                                <div class="col-sm-3"><span><strong>Center :  <asp:Label ID="Lblcenter" runat="server" Width="100px" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                                
                                <div class="col-sm-3"><span><strong>Date : <asp:Label ID="lbldate" runat="server"  Font-Bold="True" Width="142px"></asp:Label></strong></span></div>
                                
                                <div class="col-sm-3"><span><strong>Ref Dr. :<asp:Label ID="LblRefDoc" Font-Bold="True" runat="server"  Width="150px"></asp:Label></strong></span></div>
                                  </div>
                                         </div>
            <asp:Label ID="Label1" runat="server" Text=" " Width="51px" Font-Bold="True"></asp:Label>
                                            <div class="row">
                            
                                <div class="col-md-4">
                                     
                                      <table class="table table-bordered">
                <tr  >
                   <td align="center" colspan="2" style="width: 100%; vertical-align: top; text-align: left; height: 170px;">
             <asp:TreeView ID="tvGroupTree" runat="server" ShowCheckBoxes="Leaf">        
                 <ParentNodeStyle Font-Bold="False" />
                 <RootNodeStyle BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" />
    </asp:TreeView>
                       <asp:TreeView ID="tvGroupTree_Temp" target="_blank" style="color:White; text-decoration:underline; font-size: small;  font-weight: bold;padding-left:10px;" runat="server" ForeColor="Red" ShowCheckBoxes="Leaf" Width="200px">        
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
                                                <div class="col-md-8">
                                                    <h5>Report Send To (Click Box)</h5>
                                                    <div class="row">
                                                          <div class="col-md-6" style=" display: flex;">
                                                                       <asp:CheckBox ID="Chkdocemail" Text="Ref Doc Email" CssClass="form-check"  runat="server" 
                                     AutoPostBack="True" oncheckedchanged="Chkdocemail_CheckedChanged"   />
                                                          </div>
                                                         
                                                        <div class="row">
                                                                 <div class="col-md-3" style="display: flex; ">
                                     <asp:CheckBox ID="Chkemailtopatient" Text="Patient Email"  runat="server" CssClass="form-check"  AutoPostBack="True" 
                                      oncheckedchanged="Chkemailtopatient_CheckedChanged" />
                                                                 </div>
                                                            <div class="col-md-3">
                                      <asp:TextBox ID="txtpatientmail" Text=""  runat="server" AutoPostBack="True" ontextchanged="txtpatientmail_TextChanged" style="width:100%"   />
                                                            </div>
                                                            <div class="col-md-6">
                                                                <asp:RadioButtonList ID="RepHType" runat="server"  RepeatDirection="Horizontal" CssClass="form-check" >
                                                                    <asp:ListItem Value="0" Selected="True">With Letter Head</asp:ListItem>
                                                                    <asp:ListItem Value="1">Without Letter Head</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                </div>
                                                        </div>
                                                        <div class="row">
                                                                       <div class="col-md-12"  style="display: flex; align-items: center;">
                                          <asp:CheckBox ID="chkispatientsms" Text="Patient SMS" runat="server" CssClass="form-check" AutoPostBack="True" oncheckedchanged="chkispatientsms_CheckedChanged" />
                                                                       </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12"  style="display: flex; ">
                                          <asp:CheckBox ID="chkcentermailLetter" Text="Center Mail withLetter" CssClass="form-check"
                                        runat="server" AutoPostBack="True" oncheckedchanged="chkcentermailLetter_CheckedChanged"  />
                                                            </div>
                                                        </div>
                                                           <div class="row">
                                                            <div class="col-md-12"  style="display: flex; ">
                                                                            
                                      <asp:CheckBox ID="chkcentermailwithotLetter" CssClass="form-check" Text="Center Mail withoutLetter" 
                                        runat="server"  AutoPostBack="True" oncheckedchanged="chkcentermailwithotLetter_CheckedChanged"  />
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6" style="padding-left:0px;    display: flex;  align-items: left;">
                                                        <asp:ImageButton ID="btnRefdrWhatapp" runat="server"  ImageUrl="~/Images0/Whatapp.jpg" ImageAlign="AbsMiddle" OnClick="btnRefdrWhatapp_Click" ></asp:ImageButton>
                                                                <label class="m-l-10">Report Whatsapp To Ref Dr</label>
                                                         </div>
                                                            
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-6" style="padding-left:0px;    display: flex;  align-items: left;">
                                                        <asp:ImageButton ID="btnWhatapp" runat="server"  ImageUrl="~/Images0/Whatapp.jpg" OnClick="btnWhatapp_Click" ImageAlign="AbsMiddle" ></asp:ImageButton>
                                                                <label class="m-l-10">Report Whatsapp To Patient</label>
                                                         </div>
                                                            
                                                        </div>
                                                        </div>

                                                             


                                                         <div class="row">
                                                            <div class="col-md-12"  style="display: flex; align-items: center;">
                                               <asp:Label ID="Label6" runat="server" SkinID="errmsg" Text=""
                    ></asp:Label>
                                                            </div>
                                                        </div>
                                                    <div class="row" style="padding-top:15px">     
                                <div id="Div5" class="col-lg-8" runat="server" >
                                    <div class="form-group" style="display:none;">
                                        
                                     
                                         <asp:TextBox ID="txturl" TextMode="MultiLine"   Height="75px" Width="650px"  runat="server" placeholder="Enter Url" 
                                            AutoPostBack="false"></asp:TextBox>
                                       <asp:Label ID="Label4" runat="server"  Visible="false" Enabled="false" Text="label"></asp:Label>
                                        <asp:TextBox type="text" Visible="false" runat="server" value="Hello World" id="myInput"/>

<!-- The button used to copy the text -->

                                    </div>
                                </div>
                                                        </div>

                                                         <div class="row" >
                                <div class="col-lg-12 text-left">
                                <asp:Button ID="btnBack" runat="server" OnClientClick="javaScript:window.close(); return false;"  OnClick="btnBack_Click" class="btn btn-secondary"  Text="Back" ToolTip="Preview" />
                                    <asp:Button ID="cmdPrint" runat="server"  OnClick="cmdPrint_Click2"  class="btn btn-info" Text="Print Report" ToolTip="Print" />
                                      <asp:Button ID="cmdPrint_Balance" runat="server"  class="btn btn-success" Text="Print Report" ToolTip="Print" OnClick="cmdPrint_Balance_Click"   />
                                    <asp:Button ID="btnprint_letterhead" class="btn btn-primary" runat="server" onclick="btnprint_letterhead_Click" Text="Report With LetterHead"  />
                                  <asp:Button ID="btnduepay" class="btn btn-warning"  runat="server" Text="Due Pay" OnClick="btnduepay_Click"  />

                                     <asp:Button ID="BtnGenerateURL"   runat="server" Text="Gen Url / SMS Report" class="btn btn-primary" OnClick="BtnGenerateURL_Click" 
                                         />
                                   
                                <button class="btn btn-info" style="display:none"   onclick="myFunction()">Copy text</button>
                                      </div>
                                                    </div>
                                             <!--   </div>-->
                            </div>
                        
                     
       
                        </div>
                        <div class="box-footer">
                            <div class="row">
                                <div class="col-sm-12 text-center">
                         <asp:Label ID="Label44" runat="server" Font-Bold="True" ForeColor="#009933" ></asp:Label>
                                                    
                         </div>
                       </div>
                       </div>
                         <div class="box-footer" runat="server" visible="false">
                            <div class="row">
                                <div class="col-sm-12 text-center">
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

             <cr:crystalreportviewer ID="CVDesc" runat="server" AutoDataBind="true" 
                                ReportSourceID="nnnn"  
                                HasToggleGroupTreeButton="False" Height="9051px" Width="763px" 
                                EnableTheming="True" ShowAllPageIds="True" OnInit="CVDesc_Init" 
                                ReuseParameterValuesOnRefresh="True" SeparatePages="False" 
                                DisplayToolbar="False" OnPreRender="CVDesc_PreRender" PrintMode="ActiveX" />
            <cr:crystalreportsource ID="nnnn" runat="server">
                <Report FileName="~//DiagnosticReport//Pateintreportdescriptive_Email.rpt">
                </Report>
            </cr:crystalreportsource>
           
                    <asp:HiddenField ID="hdReportno"   runat="server" />
        <asp:Label ID="Label3" runat="server" Text="Label" Visible="False" Font-Bold="True" ForeColor="#009933"></asp:Label>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                                </div>
                                </div>
                                </div>
                              <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="cmdPrint_Balance"
    CancelControlID="btnClose" BackgroundCssClass="modalBackground" >
</cc1:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none" BackColor="DimGray">
    <div style="height: 60px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
               &nbsp;
                <asp:Label ID="Label2" runat="server" Text="Bill is pending u want continue?" Font-Bold="True" ForeColor="Red" ></asp:Label>
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

        <script type="text/javascript">
          function myFunction() {
  /* Get the text field */
            var copyText = document.getElementById("txturl");

  /* Select the text field */
  copyText.select();
  copyText.setSelectionRange(0, 99999); /*For mobile devices*/

  /* Copy the text inside the text field */
  document.execCommand("copy");

  /* Alert the copied text */
 // alert("Copied the text: " + copyText.value);
        } 
                </script>
    </form>
</body>
</html>
