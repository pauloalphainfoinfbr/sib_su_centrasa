<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="BloqueioExpirado.aspx.cs" Inherits="SIB.Relatorios.BloqueioExpirado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

    <br />
    <div class="well" style="width:1050px">
        <strong><span style="font-size: 11pt; color:green">
            Lista de Checklists Bloqueados
        </span></strong>
    </div>

    <form id="frmUsuarios" runat="server">                        
        <div style="margin-left:15px;">
            <asp:Label ID="lblMensagem" runat="server" Font-Size="Medium" ForeColor="#FF3300"></asp:Label>        

            <asp:GridView ID="gvChecklist" CssClass="grid-view" runat="server" OnRowCommand="gvChecklist_RowCommand" AutoGenerateColumns="false" AllowPaging="true" OnRowCreated="gvChecklist_RowCreated" BorderStyle="None" GridLines="None" HeaderStyle-BorderStyle="None" RowStyle-BorderStyle="None" OnPageIndexChanging="gvChecklist_PageIndexChanging">
                <Columns>

                    <asp:TemplateField>
                        <HeaderTemplate>Desbloquear</HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton CommandName="desbloquear" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" runat="server" ImageUrl='<%#Bind("Icon") %>' ID="btnBloquear" ImageAlign="Middle" />                        
                        </ItemTemplate>
                    </asp:TemplateField>                                
                
                    <asp:BoundField DataField="id_checklist" HeaderText="C&#243;digo" ReadOnly="true" />                                
                    <asp:BoundField DataField="placa" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Placa" ReadOnly="true" />
                    <asp:BoundField DataField="data_checklist" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Data Vistoria" ReadOnly="true" />
                    <asp:BoundField DataField="nome_condutor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Condutor" />
                    <asp:BoundField DataField="transportadora" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" HeaderText="Transportadora" />
                </Columns>
            </asp:GridView>
        </div>
    </form>

</asp:Content>
