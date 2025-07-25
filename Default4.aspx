 <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default4.aspx.cs" Inherits="Default4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div
    <table>
     <tr> Name  
        <td>  
           
<asp:FileUpload ID="FileUpload1" runat="server" />
<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="UploadFile" />
<hr />
<asp:Image ID="Image1" runat="server" Height = "100" Width = "100" />

            </td>  
       
            
    </tr>  
    </table>  
   
    </div> 
    
    </form>
</body>
</html>
