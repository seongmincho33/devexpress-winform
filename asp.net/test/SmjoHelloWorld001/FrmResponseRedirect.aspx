<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmResponseRedirect.aspx.cs" Inherits="SmjoHelloWorld001.FrmResponseRedirect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnGoogle"
                runat="server" Text="구글"
                OnClick="btnGoogle_Click" />
            <br />
            <asp:LinkButton ID="btnNaver" 
                runat="server"
                OnClick="btnNaver_Click">네이버</asp:LinkButton>
            <br />
            <asp:ImageButton ID="btnYouTube"
                runat="server"
                AlternateText="유튜브"
                ToolTip="유튜브 링크"
                OnClick="btnYouTube_Click"
                Style="height: 16px" />
            <br />
        </div>
    </form>
</body>
</html>
