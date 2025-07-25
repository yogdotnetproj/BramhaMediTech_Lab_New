<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditorTest.aspx.cs" Inherits="EditorTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" src="ckeditor/ckeditor.js"></script>
<script type="text/javascript" src="ckeditor/adapters/jquery.js"></script>
<script type="text/javascript">
    $(function () {
        CKEDITOR.replace('<%=txtCkEditor.ClientID %>',
          { filebrowserImageUploadUrl: '/CKeditorDemo/Upload.ashx' }); //path to "Upload.ashx"
    });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="txtCkEditor" TextMode="MultiLine" runat="server"></asp:TextBox>
    </div>
    </form>
</body>
</html>
