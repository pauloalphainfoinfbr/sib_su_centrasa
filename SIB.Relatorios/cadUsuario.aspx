<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cadUsuario.aspx.cs" Inherits="SIB.Relatorios.cadUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">
    
    <br />
    <div class="well" style="width:1050px">
        <strong><span style="font-size: 11pt; color:green"">
            Cadastro de Usuário
        </span></strong>
    </div>
    
    <form runat="server">        
        <div style="margin-left:15px;">

            <div class="control-group">
                <label class="control-label">Tipo de Usuário:</label>
                <label class="checkbox">
                        <asp:RadioButton id="rdbTipoUsuarioEmpresa" AutoPostBack="true" OnCheckedChanged="rdbTipoUsuarioEmpresa_CheckedChanged" GroupName="tipoUsuario" runat="server" /> Empresa
                        <asp:RadioButton id="rdbTipoUsuarioTransportadora" AutoPostBack="true" OnCheckedChanged="rdbTipoUsuarioTransportadora_CheckedChanged" GroupName="tipoUsuario" runat="server" /> Transportadora
                        <asp:RadioButton id="rdbTipoUsuarioPocket" AutoPostBack="true" OnCheckedChanged="rdbTipoUsuarioPocket_CheckedChanged" GroupName="tipoUsuario" runat="server" /> Vistoriador
                </label>
            </div>

            <div class="controls">
                <label class="control-label">Transportadora:</label>
                    <div class="controls">
                        <asp:DropDownList ID="ddlTransportadora" runat="server" CssClass="input-large"></asp:DropDownList>
                    </div>        
            </div>

            <div class="controls">
                <label class="control-label">Nome:</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="txtNome" CssClass="input-large"></asp:TextBox>
                    </div>        
            </div>

            <div class="controls">
                <label class="control-label">Login:</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="txtLogin" CssClass="input-large"></asp:TextBox>
                    </div>        
            </div>

            <div class="controls">
                <label class="control-label">Senha:</label>
                    <div class="controls">
                        <asp:TextBox TextMode="Password" runat="server" ID="txtSenha" CssClass="input-large"></asp:TextBox>
                    </div>        
            </div>

            <div class="controls">
                <label class="control-label">Repita a senha:</label>
                    <div class="controls">
                        <asp:TextBox TextMode="Password" runat="server" ID="txtRepetirSenha" CssClass="input-large"></asp:TextBox>
                    </div>        
            </div>

            <br />

            <asp:Label ID="lblMensagem" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>

            <br /><br />

            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
        </div>
    </form>
</asp:Content>
