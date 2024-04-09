<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="relVeiculosRecusados.aspx.cs" Inherits="SIB.Relatorios.relatorios.relVeiculosRecusados" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">
    <form id="veiculosRecusados" runat="server">
        <br />
            <div class="well" style="width:792px">
                <table>
                    <tr>
                        <td colspan="4">
                            <strong><span style="font-size: 11pt; color:green">Veículos Recusados</span></strong>
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data Inicial:</td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtDatInicio" data-mask="99/99/9999" runat="server" CssClass="input-small"></asp:TextBox>
                        </td>
                        <td>
                           &nbsp; Data Final:</td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtDatFim" data-mask="99/99/9999" runat="server" CssClass="input-small"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Motivo reprovação:
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
                                <asp:ListItem Value="Drenos desobstruídos">Drenos desobstruídos</asp:ListItem>
                                <asp:ListItem Value="Borracha sobre a bobineira é inteiriça">Borracha sobre a bobineira é inteiriça</asp:ListItem>
                                <asp:ListItem Value="Borracha está fixa sobre a bobineira">Borracha está fixa sobre a bobineira</asp:ListItem>
                                <asp:ListItem Value="Condições da Bobineira">Condições da Bobineira</asp:ListItem>                                
                                <asp:ListItem Value="Trava Seg. Tampas">Trava Seg. Tampas</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                           &nbsp; Placa:
                        </td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtPlaca" data-mask="aaa-9z99" runat="server" CssClass="input-small" />
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
                        <td style="color:green; font-weight:bold">
                            Formato:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlFormato" runat="server" CssClass="input-medium">
                                <asp:ListItem Value="0" Selected="True">PDF</asp:ListItem>
                                <asp:ListItem Value="1">Registros Excel</asp:ListItem>
                                <asp:ListItem Value="2">RTF Editável</asp:ListItem>
                                <asp:ListItem Value="3">Excel</asp:ListItem>
                                <asp:ListItem Value="4">Excel Workbook</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="4" style="height: 17px">
                            <button type="button" id="btnFiltrar" name="btnFiltrar" class="btn btn-primary">Filtrar</button>
                            <%--<asp:Button ID="btnFiltrar" CssClass="btn btn-primary" OnClick="btnFiltrar_Click" runat="server" Text="Filtrar" />--%>
                        </td>
                    </tr>
                </table>
            </div>
        <br />        
        <hr />
        <%--<CR:CrystalReportViewer BorderStyle="None" HasDrillUpButton="False" HasDrilldownTabs="False" ToolbarStyle-BackColor="Transparent" ToolbarStyle-BorderColor="Transparent"  ToolbarStyle-BorderStyle="None"  GroupTreeStyle-ShowLines="False" DisplayToolbar="true" DisplayStatusbar="false" HasCrystalLogo="False" HasToggleGroupTreeButton="false" SkinID="none" BackColor="White" ID="Viewer" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" GroupTreeImagesFolderUrl="" Height="1269px" ToolPanelWidth="200px" Width="880px" />--%>
    </form>

    <script type="text/javascript">

        $("#btnFiltrar").click(function (e) {

            var obj = {
                "dtIni": "",
                "dtFim": "",
                "placa": "",
                "motivo": "",
                "transportadora": "",
                "formato": ""
            };

            obj.dtIni = $("#txtDatInicio").val();
            obj.dtFim = $("#txtDatFim").val();
            obj.placa = $("#txtPlaca").val();
            obj.motivo = $("#ddlMotivo").val();
            obj.transportadora = $("#ddlTransportadora").val();

            obj.formato = $("#ddlFormato").val();

            $.ajax({
                url: 'genericReport.aspx/veiculosRecusados',
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
