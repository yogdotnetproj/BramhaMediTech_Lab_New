 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Testnormalresult.aspx.cs" Inherits="Testnormalresult" %>



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
<table id="tbldata" style="width: 770px"   cellpadding="2" cellspacing="0" border="0" >

        
        <tr>
        <td>
            <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="red" Font-Bold="true" ></asp:Label>
        
        </td>
        <td id="Td1" colspan="4" runat="server" >
            
            <CR:CrystalReportViewer ID="CVTest" runat="server" AutoDataBind="true" ReportSourceID="mmmm"  HasToggleGroupTreeButton="False" Height="9051px" Width="763px" EnableTheming="True" ShowAllPageIds="True" OnInit="CVTest_Init" ReuseParameterValuesOnRefresh="True" SeparatePages="False" DisplayToolbar="False" OnPreRender="CVTest_PreRender" PrintMode="ActiveX" />
            <CR:CrystalReportSource ID="mmmm" runat="server">
                <Report FileName="~//DiagnosticReport//Pateintreportnondescriptive.rpt">
                </Report>
            </CR:CrystalReportSource>
        </td>
           
        </tr>
       
</table>
    
    </div>
    </form>
</body>
</html>
