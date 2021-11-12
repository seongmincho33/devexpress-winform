<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormHelloWorld.aspx.cs" Inherits="SmjoHelloWorld.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtDisplay" runat="server"></asp:TextBox>
            <asp:Button ID="btnClick" runat="server" OnClick="btnClick_Click" Text="클릭" />
        </div>
    </form>
</body>
</html>
