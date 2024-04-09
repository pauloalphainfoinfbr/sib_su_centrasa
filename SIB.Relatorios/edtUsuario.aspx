<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="edtUsuario.aspx.cs" EnableEventValidation="true" Inherits="SIB.Relatorios.edtUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

    <br />
    <div class="well" style="width:1050px">
        <strong><span style="font-size: 11pt; color:green">
            Lista de Usuários
        </span></strong>
    </div>

    <form id="frmUsuarios" runat="server">                        
        <div style="margin-left:15px;">
            <asp:Label ID="lblMensagem" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>        

            <asp:GridView ID="gvUsuario" CssClass="grid-view" runat="server" OnRowCommand="gvUsuario_RowCommand" AutoGenerateColumns="false" AllowPaging="true" OnRowCreated="gvUsuario_RowCreated" BorderStyle="None" GridLines="None" HeaderStyle-BorderStyle="None" RowStyle-BorderStyle="None" OnPageIndexChanging="gvUsuario_PageIndexChanging">
                <Columns>                        
                    <asp:TemplateField>
                        <HeaderTemplate>Excluir</HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton CommandName="apagar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" ImageUrl="~/icones/delete.png" ID="btnDelete" ImageAlign="Middle" />                        
                        </ItemTemplate>
                    </asp:TemplateField>                                

                    <asp:TemplateField>
                        <HeaderTemplate>Editar</HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton CommandName="editar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" ImageUrl="~/icones/edit.jpg" ID="btnEditar" ImageAlign="Middle" />                        
                        </ItemTemplate>
                    </asp:TemplateField>                                
                
                    <asp:BoundField DataField="id_usuario" HeaderText="C&#243;digo" ReadOnly="true" />                                
                    <asp:BoundField DataField="nome_usuario" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Nome Usuário" ReadOnly="true" />
                    <asp:BoundField DataField="login" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Login" ReadOnly="true" />                
                    <asp:BoundField DataField="transportadora" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Transportadora" />
                </Columns>
            </asp:GridView>                                            
        </div>
    </form>

</asp:Content>
