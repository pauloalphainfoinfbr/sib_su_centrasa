<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="pesquisaTransportadora.aspx.cs" Inherits="SIB.Relatorios.pesquisaTransportadora" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

    <br />
    <div class="well" style="width:1050px">
        <strong><span style="font-size: 11pt; color:green">
            Pesquisa de Veículos
        </span></strong>
    </div>

     <form id="frmPesquisaVeiculos" runat="server">                
        <div style="margin-left:15px;">
             <div class="input-append">                          
                <asp:TextBox ID="txtPlaca" CssClass="input-small" runat="server" data-mask="aaa-9z99"></asp:TextBox>                      
                 <asp:Button runat="server" OnClick="btnPesquisar_Click" ID="btnPesquisar" CssClass="btn" Text="Buscar" />
             </div>                                           

            <asp:GridView ID="gvVeiculos" CssClass="grid-view" OnRowDataBound="gvVeiculos_RowDataBound" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanged="gvVeiculos_PageIndexChanged" OnRowCreated="gvVeiculos_RowCreated" BorderStyle="None" GridLines="None" HeaderStyle-BorderStyle="None" RowStyle-BorderStyle="None">
                <Columns>                                                                                        
                    <asp:BoundField DataField="placa" HeaderText="Placa" ReadOnly="true" />                                
                    <asp:BoundField DataField="desc_tipo_veiculo" HeaderText="Tipo de Veículo" ReadOnly="true" />               
                    <asp:BoundField DataField="berco_metalico" HeaderText="Berço Metalico" ReadOnly="true" />
                    <%--<asp:BoundField DataField="cinta_fina_etiqueta" HeaderText="Cinta Fina possui Etiqueta" ReadOnly="true" />--%>
                    <asp:BoundField DataField="bobineira" HeaderText="Bobineira" ReadOnly="true" />                
                    <asp:BoundField DataField="status" HeaderText="Status" ReadOnly="true" />                
                    <asp:BoundField DataField="motivos" HeaderText="Motivos" HtmlEncode="false" ReadOnly="true" />                
                    <asp:BoundField DataField="data" HeaderText="Data" />
                    <%--<asp:BoundField DataField="rastreador" HeaderText="Rastreador" />--%>
                    <asp:BoundField DataField="nome_fantasia" HeaderText="Transportadora" />
                    <asp:BoundField DataField="inicio_inspecao" HeaderText="Início Inspeção" />
                    <asp:BoundField DataField="inicio_peacao" HeaderText="Início Peação" />
                    <asp:BoundField DataField="termino_peacao" HeaderText="Termino Peação" />
                    <asp:BoundField DataField="manifestado" HeaderText="Veículo Manifestado" />
                    <asp:BoundField DataField="liberado" HeaderText="Liberado Para Viagem" />
                </Columns>
            </asp:GridView>                                            

             <br />
             <!--
             <div class="checkbox">
                 <label>
                    <asp:CheckBox ID="chkManifestado" runat="server" /> Veículo manifestado
                 </label>
             </div>

             <div class="checkbox">
                 <label>
                    <asp:CheckBox ID="chkLiberado" runat="server" /> Liberado para viagem
                 </label>
             </div>            
            -->
             <br /> <br /> <br />

             <asp:Button ID="btnGravar" OnClick="btnGravar_Click" CssClass="btn btn-primary" runat="server" Text="Salvar" />
         </div>
    </form>

</asp:Content>
