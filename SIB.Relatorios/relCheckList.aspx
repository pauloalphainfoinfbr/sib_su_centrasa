<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="relCheckList.aspx.cs" Inherits="SIB.Relatorios.relCheckList" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

<form id="frmRelacaoChecklist" runat="server">        
         <br />
            <div class="well" style="width:815px">
                <table>
                    <tr>
                        <td colspan="4">
                            <strong><span style="font-size: 11pt; color:green"">Relação de Checklist</span></strong>
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data Inicial:
                        </td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtDatInicio" data-mask="99/99/9999" runat="server" CssClass="input-small"></asp:TextBox>
                        </td>
                        <td>
                            Placa:
                        </td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtPlaca" data-mask="aaa-9z99" runat="server" CssClass="input-small" />
                        </td>                
                    </tr>
                    <tr>
                        <td>
                            Data Final:
                        </td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtDatFim" data-mask="99/99/9999" runat="server" CssClass="input-small"></asp:TextBox>
                        </td>      
                        <td>
                           DT: 
                        </td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtDT" runat="server" CssClass="input-small"></asp:TextBox>
                        </td>                                   
                    </tr>            
                    <tr>
                        <td>
                            Transportadora:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlTransportadora" runat="server" CssClass="input-large">                                                                                                                    
                            </asp:DropDownList>
                        </td>
                        <td>
                           Motivo Reprovação:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlMotivo" runat="server" CssClass="input-xlarge">          
                                <asp:ListItem Value="Para Brisa trincado">Para Brisa trincado</asp:ListItem>
                                <asp:ListItem Value="Retrovisor trincado">Retrovisor trincado</asp:ListItem>
                                <asp:ListItem Value="Carroceria">Carroceria</asp:ListItem>
                                <asp:ListItem Value="Assoalho">Assoalho</asp:ListItem>
                                <asp:ListItem Value="Fumaça Negra > 2">Fumaça Negra > 2</asp:ListItem>
                                <asp:ListItem Value="Luz de freios">Luz de freios</asp:ListItem>
                                <asp:ListItem Value="Setas">Setas</asp:ListItem>
                                <asp:ListItem Value="Cordas">Cordas</asp:ListItem>
                                <asp:ListItem Value="Catracas">Catracas</asp:ListItem>
                                <asp:ListItem Value="Estepe">Estepe</asp:ListItem>
                                <asp:ListItem Value="Não possui Laudo de Opacidade de 100%">Não possui Laudo de Opacidade de 100%</asp:ListItem>
                                <asp:ListItem Value="Condição Externa Cavalo - Pontuação < 11">Condição Externa Cavalo - Pontuação < 11</asp:ListItem>
                                <asp:ListItem Value="Placa Legível">Placa Legível</asp:ListItem>
                                <asp:ListItem Value="Gancho tipo Patola">Gancho tipo Patola</asp:ListItem>
                                <asp:ListItem Value="Estado Geral">Estado Geral</asp:ListItem>
                                <asp:ListItem Value="Vazamento de Óleo">Vazamento de Óleo</asp:ListItem>
                                <asp:ListItem Value="Registro fotográfico das condições da bobineira">Registro fotográfico das condições da bobineira</asp:ListItem>
                                <asp:ListItem Value="Drenos desobstruídos">Drenos desobstruídos</asp:ListItem>
                                <asp:ListItem Value="Borracha sobre a bobineira é inteiriça">Borracha sobre a bobineira é inteiriça</asp:ListItem>
                                <asp:ListItem Value="Borracha está fixa sobre a bobineira">Borracha está fixa sobre a bobineira</asp:ListItem>
                                <asp:ListItem Value="Condições da Bobineira">Condições da Bobineira</asp:ListItem>
                                <asp:ListItem Value="Trava Seg. Tampas">Trava Seg. Tampas</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>                                              
                    </tr>                    
                    <tr>
                        <td>
                            Catraca em toda amarração?
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlUtilizouCatraca" runat="server" CssClass="input-large">                                                                                                                    
                                <asp:ListItem Value="1">Sim</asp:ListItem>
                                <asp:ListItem Value="2">Não</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>      
                        <td>
                           Movimentou Tampas? 
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlPrecisouMovimentar" runat="server" CssClass="input-large">                                                                                                                    
                                <asp:ListItem Value="1">Sim</asp:ListItem>
                                <asp:ListItem Value="2">Não</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>                                   
                    </tr>
                    <tr>
                        <td>
                            Berço metálico ou Bobineira?
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlBercoMetalicoBobineira" runat="server" CssClass="input-large">                                                                                                                    
                                <asp:ListItem Value="1">Berço Metálico</asp:ListItem>
                                <asp:ListItem Value="2">Bobineira</asp:ListItem>
                                <asp:ListItem Value="3">NA</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>      
                        <td>
                           Carga seca ou graneleiro? 
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlTipoCarroceria" runat="server" CssClass="input-large">                                                                                                                    
                                <asp:ListItem Value="1">Bobineira</asp:ListItem>
                                <asp:ListItem Value="3">Carga Seca</asp:ListItem>
                                <asp:ListItem Value="4">Graneleiro</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>                                   
                    </tr>
                    <tr>
                        <td style="color:green; font-weight:bold">
                            Formato:
                        </td>
                        <td>
                            <asp:DropDownList  ClientIDMode="Static" ID="ddlFormato" runat="server" CssClass="input-medium">
                                <asp:ListItem Value="0" Selected="True">PDF</asp:ListItem>
                                <asp:ListItem Value="1">Registros Excel</asp:ListItem>
                                <asp:ListItem Value="2">RTF Editável</asp:ListItem>
                                <asp:ListItem Value="3">Excel</asp:ListItem>
                                <asp:ListItem Value="4">Excel Workbook</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4" style="height: 17px">
                            <button type="button" id="btnFiltrar" name="btnFiltrar" class="btn btn-primary">Filtrar</button>
                            <%--<asp:Button ID="btnFiltrar" OnClick="btnFiltrar_Click" CssClass="btn btn-primary" runat="server" Text="Filtrar" />--%>
                        </td>
                    </tr>
                </table>     
            </div>      
        <hr />                
                 
        <%--<CR:CrystalReportViewer BorderStyle="None" HasDrillUpButton="False" HasDrilldownTabs="False" ToolbarStyle-BackColor="Transparent" ToolbarStyle-BorderColor="Transparent"  ToolbarStyle-BorderStyle="None"  GroupTreeStyle-ShowLines="False" DisplayToolbar="true" DisplayStatusbar="false" HasCrystalLogo="False" HasToggleGroupTreeButton="false" SkinID="none" BackColor="White" ID="Viewer" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" GroupTreeImagesFolderUrl="" Height="1269px" ToolPanelWidth="200px" Width="880px" />--%>
        
    </form>

    <script type="text/javascript">

        $("#btnFiltrar").click(function (e) {

            var obj = {
                "dtIni": "",
                "dtFim": "",
                "placa": "",
                "dt": "",
                "transportadora": "",
                "motivo": "",
                "catracaAmarracao": "",
                "movimentouTampas": "",
                "bercoMetalicoBobineira": "",
                "cargaSecaGraneleiro": "",
                "formato": ""
            };

            obj.dtIni = $("#txtDatInicio").val();
            obj.dtFim = $("#txtDatFim").val();
            obj.placa = $("#txtPlaca").val();
            obj.dt = $("#txtDT").val();
            obj.transportadora = $("#ddlTransportadora").val();
            obj.motivo = $("#ddlMotivo").val();
            obj.catracaAmarracao = $("#ddlUtilizouCatraca").val();
            obj.movimentouTampas = $("#ddlPrecisouMovimentar").val();
            obj.bercoMetalicoBobineira = $("#ddlBercoMetalicoBobineira").val();
            obj.cargaSecaGraneleiro = $("#ddlTipoCarroceria").val();

            obj.formato = $("#ddlFormato").val();

            $.ajax({
                url: 'genericReport.aspx/relacaoChecklists',
                data: JSON.stringify(obj),
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function () {

                    var params = [
                    'height=' + screen.height,
                    'width=' + screen.width,
                    'fullscreen=yes' // only works in IE, but here for completeness
                    ].join(',');

                    var popup = window.open('genericReport.aspx', '_blank');
                    //popup.moveTo(0, 0);
                },
                error: function (ex) {
                    alert("Não foi possível carregar o relatório.");
                }
            });
        });

    </script>

</asp:Content>
