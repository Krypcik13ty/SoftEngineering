<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SoftEngineering.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 23%;
            height: 307px;
        }
        .auto-style2 {
            width: 127px;
        }
        .auto-style3 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <h1>Zapomniałem hasła :(</h1>
        </div>
        <table align="center" class="auto-style1">
            <tr>
                <td class="auto-style2">Użytkownik</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="225px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">PESEL</td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Nowe hasło</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged" TextMode="Password" Width="224px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Powtórz nowe hasło</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" OnTextChanged="TextBox3_TextChanged" TextMode="Password" Width="220px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Pesel i użytkownik się nie zgadzają"></asp:Label>
                </td>
                <td class="auto-style3">
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Resetuj hasło" />
                </td>
            </tr>
        </table>
        <p class="auto-style3">
            &nbsp;</p>
    </form>
</body>
</html>
