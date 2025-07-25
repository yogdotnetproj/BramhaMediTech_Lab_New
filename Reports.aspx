 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <Maintestname></Maintestname>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="rptviewer" runat="server" AutoDataBind="true"   HasCrystalLogo="False" 
        HasDrillUpButton="False" Height="25px" ReportSourceID="rpts" ToolbarStyle-BackColor="Transparent" 
        Width="700px" ToolbarImagesFolderUrl="images/cry_images/" EnableParameterPrompt="False"
         HasRefreshButton="True" ShowAllPageIds="True" ReuseParameterValuesOnRefresh="True" 
         ToolbarStyle-BorderColor="White" ToolbarStyle-BorderWidth="0px" BorderColor="White" 
         HasExportButton="False" HasPrintButton="False" HasSearchButton="False" style="z-index: 10; 
         position: absolute" DisplayToolbar="False" />
        <CR:CrystalReportSource ID="rpts" runat="server">
        <Report FileName="">
            </Report>
        </CR:CrystalReportSource>
    </div>
    </form>
</body>
</html>
