    <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cadCliente.aspx.cs" Inherits="SIB.Relatorios.CadCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

<br />
<div class="well" style="width:1050px">
    <strong><span style="font-size: 11pt; color:green"">
        Cadastro de Clientes
    </span></strong>
</div>

<form runat="server">
    <div style="margin-left:15px;">
        <div class="controls">
            <label class="control-label">Código:</label>
            <div class="controls">
                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="10" CssClass="input-small"></asp:TextBox>
            </div>
        </div>

        <div class="controls">
            <label class="control-label">Nome:</label>
            <div class="controls">
                <asp:TextBox ID="txtNome" runat="server" Text='<%# Bind("nome") %>' CssClass="input-xlarge"></asp:TextBox>            
            </div>
        </div>

        <br />

        <asp:Label ID="lblMensagem" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>

        <br /><br />

        <asp:Button ID="cadastrar" runat="server" OnClick="cadastrar_Click" CssClass="btn btn-primary" Text="Gravar" />
    </div>
</form>

</asp:Content>
