<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="edtEmpresa.aspx.cs" Inherits="SIB.Relatorios.edtEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

<br />
<div class="well" style="width:1050px">
    <strong><span style="font-size: 11pt; color:green">
        Lista de Empresas
    </span></strong>
</div>

<form id="frmEmpresa" runat="server">                 
        <div style="margin-left:15px;">

            <asp:Label ID="lblMensagem" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>        

            <asp:GridView ID="gvEmpresa" CssClass="grid-view" runat="server" OnRowCommand="gvEmpresa_RowCommand" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanged="gvEmpresa_PageIndexChanged" PageSize="1000" OnRowCreated="gvEmpresa_RowCreated" BorderStyle="None" GridLines="None" HeaderStyle-BorderStyle="None" RowStyle-BorderStyle="None">
                <Columns>                     
                    <asp:TemplateField>
                        <HeaderTemplate>Apagar</HeaderTemplate>
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
                
                    <asp:BoundField DataField="id_empresa" HeaderText="C&#243;digo" ReadOnly="true" />                                
                    <asp:BoundField DataField="nome_fantasia" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Nome Fantasia" ReadOnly="true" />
                    <asp:BoundField DataField="logradrouro" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Endereço" ReadOnly="true" />
                    <asp:BoundField DataField="desc_cidade" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Cidade" ReadOnly="true" />                
                </Columns>
            </asp:GridView>                                            
        </div>
    </form>

</asp:Content>
