<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmResponseWrite.aspx.cs" Inherits="SmjoHelloWorld001.FrmResponseWrite" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Response 개체</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnClick" runat="server" Text="평범한 버튼" OnClick="btnClick_Click" />
            <br />
            <%="여기서 사용하는 건 Response.Write()메서드로 문자열 출력하는거랑 같은겁니다.<br />"%>
            <asp:Button ID="btnJavaScript" runat="server" Text="자바스크립트사용버튼" 
                OnClick="btnJavaScript_Click" />
        </div>
    </form>
</body>
</html>
