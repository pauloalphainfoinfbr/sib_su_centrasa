<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="edtCliente.aspx.cs" Inherits="SIB.Relatorios.edtCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

    <br />
    <div class="well" style="width:1050px">
        <strong><span style="font-size: 11pt; color:green"">
            Lista de Clientes
        </span></strong>
    </div>
    
    <form id="frmCliente" runat="server">
        <div style="margin-left:15px;">
            <asp:Label ID="lblMensagem" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>        

                <asp:GridView ID="gvCliente" OnPageIndexChanging="gvCliente_PageIndexChanging" CssClass="grid-view" runat="server" OnRowCommand="gvCliente_RowCommand" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowCreated="gvCliente_RowCreated" BorderStyle="None" GridLines="None" HeaderStyle-BorderStyle="None" RowStyle-BorderStyle="None">
                    <Columns>                     
                        <asp:TemplateField>
                            <HeaderTemplate >Apagar</HeaderTemplate>
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
                
                        <asp:BoundField DataField="id_empresa" HeaderText="ID" ReadOnly="true" />     
                        <asp:BoundField DataField="nome_fantasia" HeaderText="Nome Fantasia" ReadOnly="true" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />                                           
                        <asp:BoundField DataField="codigo" HeaderText="C&#243;digo" ReadOnly="true" />                
                    </Columns>
            
                </asp:GridView>       
                <br />                                     
                <div>
                    <i>Página <%=gvCliente.PageIndex + 1 %></i> de <%=gvCliente.PageCount %>
                </div>

        </div>
    </form>

</asp:Content>
