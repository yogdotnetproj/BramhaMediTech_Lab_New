 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientReport.aspx.cs" Inherits="PatientReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <title>Patient Report </title>
        <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
        <link rel="stylesheet" href="plugins/bootstrap/css/bootstrap.min.css">
        <link rel="stylesheet" href="plugins/font-awesome/css/font-awesome.min.css">
        <link rel="stylesheet" href="bower_components/Ionicons/css/ionicons.min.css">
        <link rel="stylesheet" href="plugins/theme/css/theme.min.css">
        <link rel="stylesheet" href="plugins/theme/css/skins/_all-skins.min.css">
        <link rel="stylesheet" href="css/style.css">
        <link href="cssmain/bootstrap.min.css" rel="stylesheet" />
         <link href="cssmain/main.min.css" rel="stylesheet" />
     <link href="cssmain/master.css" rel="stylesheet" />

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
            <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
            <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
            <![endif]-->

       <!-- <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic">-->
       
         <link href="App_Themes/Default/GVCss.css" rel="stylesheet" type="text/css" />
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
    </head>
<body class="hold-transition skin-black sidebar-mini">
    <form id="form1" runat="server">
        <header class="header-patient">
                <div class="text-center"><h2>Patient Portal</h2> </div>
            </header>

    <div  class="container-fluid p-3">
        <div class="box">
                        <div class="box-body">
       <div class="row">
         <div id="div2" class="rounded_corners p-3">

           <div class="col-sm-2"><span class="btn btn-secondary"><strong>Reg # : <asp:Label ID="lblregno" runat="server" Text="RegNo"  Font-Bold="True"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Name :  <asp:Label ID="lblname" runat="server" Text="Name"  Font-Bold="True" ></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Age :  <asp:Label ID="lblage" runat="server" Text="Age"  Font-Bold="True"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Gender :  <asp:Label ID="lblsex" runat="server" Text="Sex"  Font-Bold="True" ></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-12">&nbsp;</div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Test Charges : <asp:Label ID="lbltestcharges" runat="server" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Center :  <asp:Label ID="lblpscname" runat="server" 
                                Font-Bold="True"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Date : <asp:Label ID="lbldate" runat="server"  Font-Bold="True" ></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
                                <div class="col-sm-2"><span class="btn btn-secondary"><strong>Ref Dr. :<asp:Label ID="LblRefDoc" Font-Bold="True" runat="server"></asp:Label></strong></span></div>
                                <div class="col-sm-1"></div>
            <asp:Label ID="Label12" runat="server" Text=" "  Font-Bold="True"></asp:Label>
                              
                              <div class="col-lg-12 text-center mt-3">
                             
                                    <asp:Button ID="btnBack" runat="server" CssClass="btn btn-primary"   Text="Log out" OnClick="btnBack_Click"  />
     

                              </div>
     
    </div>
    </div>

      
                           <div class="table-responsive mt-3" style="width:100%">
                            <asp:gridview id="GVTestentry" runat="server"   class="table table-responsive table-sm " datakeynames="PatRegID" autogeneratecolumns="False"
                                width="100%" onrowdatabound="GVTestentry_RowDataBound" allowpaging="True" onpageindexchanging="GVTestentry_PageIndexChanging"
                                pagesize="50" 
        HeaderStyle-ForeColor="Black"
  AlternatingRowStyle-BackColor="White"  
 onrowediting="GVTestentry_RowEditing" onselectedindexchanged="GVTestentry_SelectedIndexChanged">
                                <columns>
                               
                                <%--   <asp:HyperLinkField HeaderText="Reg.No" DataTextField="RegNo" DataNavigateUrlFields="RegNo,FID" DataNavigateUrlFormatString="Addresult.aspx?regno={0}&amp;FID={1}" />--%>
<asp:BoundField DataField="Patregdate" HeaderText=" Date" />
<asp:BoundField DataField="PatRegID" HeaderText="Reg.No" />
<asp:BoundField DataField="CenterCode" HeaderText="Center"  />

<asp:BoundField DataField="FullName" HeaderText=" Name" />
<asp:BoundField DataField="Sex" HeaderText="Gender" />
<asp:BoundField DataField="MDY" HeaderText="Age" />
<asp:BoundField DataField="Drname" HeaderText="Ref Doc" />



<asp:TemplateField HeaderText ="Test ">
    <ItemTemplate>
        <asp:Label ID="lbltestname" runat="server" Text='<%#Eval("TestName") %>'></asp:Label>
    </ItemTemplate>
</asp:TemplateField>




<asp:BoundField DataField="LabRegMediPro" HeaderText="Inv no" />

    <asp:CommandField EditText="Report" HeaderText="Report"  ShowEditButton="True" />
   
   <asp:TemplateField>
    <ItemTemplate>
     <asp:HiddenField ID="hdnFid" runat="server" Value='<%#Eval("FID") %>' /> 
      <asp:HiddenField ID="hdnRegNo" runat="server" Value='<%#Eval("PatRegID") %>' /> 
    </ItemTemplate>
    </asp:TemplateField>


</columns>


                            </asp:gridview>
                             <div class="Pager"></div>
                          </div>
                        </div>
                    </div>

        <asp:Label ID="Label44" runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Red"
            Height="23px"   Text="Label" Width="525px" Visible="False"></asp:Label><br />
     
        <asp:TreeView ID="tvGroupTree" runat="server" Visible="false"  ShowCheckBoxes="Leaf" >
            <ParentNodeStyle Font-Bold="False" />
            <RootNodeStyle BorderColor="Blue" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" />
        </asp:TreeView>
        <asp:HiddenField ID="hdReportno" runat="server" />
         <div class="box-footer">
                            <div class="row">
                                <div class="col-lg-12 text-center">
                                
                                <asp:Button ID="Button2"  runat="server" Visible="false" OnClick="Button2_Click" Text="Result Report" class="btn btn-primary" />
                                </div>
                                </div>
                                </div>
         
        </div>
    </form>
</body>
</html>
