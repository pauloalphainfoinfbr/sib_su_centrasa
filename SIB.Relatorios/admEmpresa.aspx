<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admEmpresa.aspx.cs" Inherits="SIB.Relatorios.admEmpresa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>- SIG - Sistema de Gest&atilde;o Integrada</title>
    <link href="css/estilo.css" type="text/css" rel="stylesheet" />
    <link href="Content/bootstrap/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="Content/bootstrap/bootstrap.theme.min.css" rel="stylesheet" media="screen" />
    <link href="jasny-bootstrap/css/jasny-bootstrap.min.css" rel="stylesheet" media="screen" />
    <link href="css/MasterPageStyle.css" rel="stylesheet" />    
</head>
<body>
<form id="Form1" runat="server">
<table width="500" border="0" cellspacing="4" cellpadding="0">
    <tr>
        <td class="style3" colspan="2" style="height: 5px">
        </td>
    </tr>
    <tr>
      <td colspan="2" class="style4"><B>- ESCOLHA A EMPRESA</B></td>
    </tr>
    <tr>
        <td style="height: 30px;" valign="middle" colspan="2">
            <span class="style1"><asp:Label ID="lblStatus" runat="server" Text="Todos os campos são obrigatórios"></asp:Label></span></td>
    </tr>
 <tr>
      <td style="width: 140px; height: 30px;" bgcolor="#f7f7f7"><div align="right" class="style2">
        <div align="right">
            Empresa:&nbsp;</div>
      </div></td>
      <td style="width: 456px; height: 30px;" bgcolor="#f7f7f7"><label>
      <div align="left">
          &nbsp;<asp:DropDownList ID="ddlEmp" runat="server" AutoPostBack="False" CssClass="TextBox">
              <asp:ListItem Text="CSN VR" Value="1" Selected="True">CSN VR</asp:ListItem>
          </asp:DropDownList>&nbsp;
        </div>
      </label></td>
    </tr> 
    <tr>
      <td style="width: 140px"><div align="right"></div></td>
      <td height="30" style="width: 456px"><label>
        <div align="left">
            &nbsp;<asp:Button ID="btnOK" OnClick="btnOK_Click" runat="server" CssClass="btn btn-primary" Text="OK" /></div>
      </label></td>
    </tr>
  </table>
  </form>
</body>
</html>
