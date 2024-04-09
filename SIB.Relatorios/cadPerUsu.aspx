<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="cadPerUsu.aspx.cs" Inherits="SIB.Relatorios.cadPerUsu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">
    
<br />

<div class="well" style="width:1050px">
    <strong><span style="font-size: 11pt; color:green">
            Permissão de Usuários
    </span></strong>
</div>

<form id="FrmPermissaoUsuario" runat="server">

    <div style="margin-left:15px;">

        <div class="controls">
            <label class="control-label">Usuário</label>
            <div class="controls">
                <asp:DropDownList AutoPostBack="true" ID="ddlUsuario" OnSelectedIndexChanged="ddlUsuario_SelectedIndexChanged" runat="server" CssClass="input-large"></asp:DropDownList>
            </div>
        </div>            
    
        <asp:GridView ID="gvPermUsuario" CssClass="grid-view" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="gvPermUsuario_PageIndexChanging" PageSize="50" OnRowCreated="gvPermUsuario_RowCreated" BorderStyle="None" GridLines="None" HeaderStyle-BorderStyle="None" RowStyle-BorderStyle="None">
            <Columns>                        
                <asp:TemplateField>
                    <HeaderTemplate>Ativar</HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox Checked='<%#Bind("ativo") %>' runat="server"  ID="chk"></asp:CheckBox> 
                    </ItemTemplate>
                </asp:TemplateField>                                
                
                <asp:BoundField DataField="id_tela" HeaderText="C&#243;digo" ReadOnly="true" />                                
                <asp:BoundField DataField="desc_tela" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Descrição" ReadOnly="true" />
                <asp:BoundField DataField="hyperlink_nome" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Link" ReadOnly="true" />                
            </Columns>
        </asp:GridView>  
                                          
        
            <br /><br />

        <asp:Button ID="cadastrar" runat="server" OnClick="cadastrar_Click" CssClass="btn btn-primary" Text="Gravar" />
    </div>
</form>

</asp:Content>
