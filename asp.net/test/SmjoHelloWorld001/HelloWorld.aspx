<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HelloWorld.aspx.cs" Inherits="SmjoHelloWorld001.HelloWorld" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <style type="text/css">
        #TextArea1 {
            height: 110px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="height: 189px">
            <textarea ID="TextArea1" cols="20"  runat="server" name="S1"></textarea><br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Hello" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="안녕?" />
        </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
