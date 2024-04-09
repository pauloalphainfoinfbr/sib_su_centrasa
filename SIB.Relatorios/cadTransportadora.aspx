<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cadTransportadora.aspx.cs" Inherits="SIB.Relatorios.cadTransportadora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

    <br />
    <div class="well" style="width:1050px">
        <strong><span style="font-size: 11pt; color:green"">
            Cadastro de Transportadora
        </span></strong>
    </div>
   
    <form id="frmCadTransportadora" runat="server" CssClass="form-inline">    
        <div style="margin-left:15px;">
            <div class="controls">
                <label class="control-label">Dados Pessoais:</label>                        
                <br />

                <div class="controls constrols-row">                        
                    <asp:TextBox runat="server" ID="txtRazaoSocial" CssClass="span3" placeholder="Razão Social"></asp:TextBox>            
                    <asp:TextBox runat="server" ID="txtNomeFantasia" CssClass="span3" placeholder="Nome Fantasia"></asp:TextBox>
                </div>        

                <div class="controls constrols-row">                        
                    <asp:TextBox runat="server" ID="txtCnpj" CssClass="input-large" MaxLength="18" data-mask="99.999.999/9999-99" placeholder="CNPJ"></asp:TextBox>            
                    <asp:TextBox runat="server" ID="txtInscricaoEstadual" CssClass="input-large" placeholder="Inscrição Estadual"></asp:TextBox>
                </div>        
            </div>

            <div class="controls">
                <label class="control-label">Endereço:</label>
                <div class="controls controls-rows">            
                    <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbEstado_SelectedIndexChanged" ID="cmbEstado" CssClass="input-large">                                        
                    </asp:DropDownList>
                    &nbsp;
                    <asp:DropDownList runat="server" ID="cmbCidade" CssClass="input-large">                    
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>                
                </div>
            </div>

            <div class="controls controls-row">            
                <asp:TextBox runat="server" ID="txtEnder" CssClass="input-large" placeholder="Endereço"></asp:TextBox>                        
                <asp:TextBox runat="server" ID="txtBairro" CssClass="input-large" placeholder="Bairro"></asp:TextBox>     
                <asp:TextBox runat="server" ID="txtCep" MaxLength="10" data-mask="99.999-999" CssClass="input-large" placeholder="CEP"></asp:TextBox>       
            </div>                

            <div class="controls">
                <label class="control-label">Contato:</label>
                <div class="controls controls-row">
                    <asp:TextBox runat="server" ID="txtTelefone" data-mask="(99)9999-99999" MaxLength="14" CssClass="input-large" placeholder="Telefone"></asp:TextBox>                   
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="input-large" placeholder="E-mail"></asp:TextBox>
                </div>
            </div>

            <asp:Label ID="lblMensagem" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>

            <br /><br />

            <asp:Button ID="gravar" Text="Gravar" runat="server" OnClick="gravar_Click" CssClass="btn btn-primary" />
        </div>
    </form>                 
</asp:Content>
