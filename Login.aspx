<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Log in  </title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <link rel="stylesheet" href="cssmain/bootstrap.min.css">
    <%--<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css">--%>
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="cssmain/master.css">
    <link rel="stylesheet" href="cssmain/login.css">
    <link rel="stylesheet" href="cssmain/common.scss">
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <!--  <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic"> -->
    <link rel="stylesheet" href="customTheme/css/style.css">
  </head>
  <body class="hold-transition login-page public-layout">
    <div class="app-loader main-loader" style="display:none;">
      <div class="loader-box">
        <div class="bounceball"></div>
        <div class="text">Lab<span>app</span></div>
      </div>
    </div>
    <form id="form1" runat="server">
      <div class="login-card login-dark">
        <div>
          <h1 class="text-center text-white" style="margin-bottom: 30px;">Laboratory<br>Management System</h1>
          <div class="login-main">
            <form class="p-3" action="dashboard.html" method="post">
                <div><a class="logo"><img width="170px" class="img-fluid for-light m-auto" src="customTheme/brahmaMedicalDark.png" alt="looginpage"><img width="170px" class="img-fluid for-dark" src="customTheme/brahmaMedicalDark.png" alt="logo"></a></div>
                <div class="form-group mb-1">
                    <label class="col-form-label mb-0">Login ID</label>
                    <!-- <input type="email" class="form-control" placeholder="Email"> -->
                    <span class="far fa-user mr-1"></span>
                    <asp:TextBox ID="txtUName" runat="server" class="form-control"></asp:TextBox>
                    <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" controltovalidate="txtUName" display="Dynamic" errormessage="*" setfocusonerror="True" validationgroup="loginGroup"></asp:requiredfieldvalidator>
                    <span class="glyphicon glyphicon-envelope form-control-feedback"></span>
                </div>
                <div class="form-group mb-1">
                    <label class="col-form-label mb-0">Password</label>
                    <!--  <input type="password" class="form-control" placeholder="Password">-->
                    <span class="fas fa-key mr-1"></span>
                    <asp:textbox id="txtPassword" runat="server" class="form-control" textmode="Password"></asp:textbox>
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                    <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" controltovalidate="txtPassword" display="Dynamic" errormessage="*" setfocusonerror="True" validationgroup="loginGroup"></asp:requiredfieldvalidator>
                </div>
                <div class="row">
                    <div class="col-xs-12">
                        <!-- <button type="submit" class="btn btn-primary btn-block btn-flat">Login</button> -->
                        <asp:button id="btnLogin" runat="server" class="btn btn-primary mt-3 w-100" OnClick="btnLogin_Click" text="Login" validationgroup="loginGroup"/>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-4"></div>
                    <div class="col-xs-4">
                      <asp:Label ID="lblerrorLogin" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="col-xs-4"></div>
                </div>
                <div class="row pt10">
                    <div class="col-xs-2"></div>
                    <div class="col-xs-8 text-center">
                        <!-- <a href="#">Forgot Password?</a-->
                    </div>
                <div class="col-xs-2"></div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </form>
    <!--div class="page-box">
      <div class="app-container page-sign-in">
        <div class="content-box">
          <div class="content-header">
            <div class="app-logo">
              <div class="logo-wrap">
                <img src="images/pis-logo.jpg" alt="" class="logo-img">
              </div>
            </div>
          </div>
          <div class="content-body">
            <div class="w-100">
              <h2 class="h4 mt-0 mb-1">Log in</h2>
              <p class="text-muted">Log in to access your Account</p>
              
            </div>
          </div>
        </div>
      </div>
    </div-->
    <script src="jsmain/jquery.min.js"></script>
    <script src="jsmain/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        if (window.history.forward(1) != null)
            window.history.forward(1);
    </script>
  </body>
</html>
