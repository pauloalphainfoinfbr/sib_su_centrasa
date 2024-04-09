<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cadEmpresa.aspx.cs" Inherits="SIB.Relatorios.cadEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

    <br />
    <div class="well" style="width:1050px">
        <strong><span style="font-size: 11pt; color:green"">
            Cadastro de Empresa
        </span></strong>
    </div>
   
    <form runat="server" id="frmEmpresa" CssClass="form-inline">                                         
        <div style="margin-left:15px;">
                <div class="control-group">                        
                   <label class="control-label">Razão Social:</label> 
                    <div class="controls">
                        <asp:TextBox runat="server" ID="txtRazaoSocial" CssClass="input-xlarge" placeholder="Razão Social"></asp:TextBox>                           
                    </div>
                </div>                    

                <div class="control-group">                                       
                   <label class="control-label">Nome Fantasia:</label> 
                    <div class="controls">
                        <asp:TextBox runat="server" ID="txtNomeFantasia" CssClass="input-xlarge" placeholder="Nome Fantasia"></asp:TextBox>
                    </div>
                </div>                            

            <div class="controls">
                <label class="control-label">Estado:</label>
                <div class="controls controls-rows">            
                    <asp:DropDownList runat="server" AutoPostBack="true" OnSelectedIndexChanged="cmbEstado_SelectedIndexChanged" ID="cmbEstado" CssClass="input-large">                    
                    </asp:DropDownList>
                    &nbsp;
                    <asp:DropDownList runat="server" ID="cmbCidade" CssClass="input-large">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>                
                </div>
            </div>

            <div class="control-group">            
                <label class="control-label">Logradouro:</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="txtEnder" CssClass="input-xlarge" placeholder="Endereço"></asp:TextBox> 
                    </div>
            </div>
            <div class="control-group">
                <label class="control-label">Bairro:</label>                       
                    <div class="controls">
                        <asp:TextBox runat="server" ID="txtBairro" CssClass="input-large" placeholder="Bairro"></asp:TextBox>     
                    </div>                    
            </div>                

            <div class="control-group">
                <label class="control-label">Cep:</label>
                    <div class="controls">
                        <asp:TextBox runat="server" ID="txtCep" data-mask="99.999-999" MaxLength="10" CssClass="input-large" placeholder="CEP"></asp:TextBox>       
                    </div>
            </div>

            <div class="controls">
                <label class="control-label">Telefone:</label>
                <div class="controls controls-row">
                    <asp:TextBox runat="server" ID="txtTelefone" data-mask="(99)9999-99999" MaxLength="14" CssClass="input-large" placeholder="Telefone"></asp:TextBox>                                   
                </div>
            </div>

            <div class="controls">
                <label class="control-label">E-mail:</label>
                <div class="controls controls-row">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="input-xlarge" placeholder="E-mail"></asp:TextBox>
                </div>
            </div>        

            <br />

            <asp:Label ID="lblMensagem" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>

            <br /><br />

            <asp:Button ID="gravar" Text="Gravar" runat="server" onClick="gravar_Click" CssClass="btn btn-primary" />
        </div>
    </form>                 
</asp:Content>
