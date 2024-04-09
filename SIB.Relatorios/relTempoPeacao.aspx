<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="relTempoPeacao.aspx.cs" Inherits="SIB.Relatorios.relTempoPeacao" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

     <form id="frmPesoTotalEscoado" runat="server">        
         <br />
            <div class="well" style="width:792px">
                <table>
                    <tr>
                        <td colspan="4">
                            <strong><span style="font-size: 11pt; color:green"">Tempo de Peação</span></strong>
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
                            &nbsp; Transportadora:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlTransportadora" runat="server" CssClass="input-large">                    
                            </asp:DropDownList>
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
                            &nbsp; Hora Entrada
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlHoraPeacao" runat="server">                                
                                <asp:ListItem Value="00">00:00 &#224;s 01:00</asp:ListItem>
                                <asp:ListItem Value="01">01:00 &#224;s 02:00</asp:ListItem>
                                <asp:ListItem Value="02">02:00 &#224;s 03:00</asp:ListItem>
                                <asp:ListItem Value="03">03:00 &#224;s 04:00</asp:ListItem>
                                <asp:ListItem Value="04">04:00 &#224;s 05:00</asp:ListItem>
                                <asp:ListItem Value="05">05:00 &#224;s 06:00</asp:ListItem>
                                <asp:ListItem Value="06">06:00 &#224;s 07:00</asp:ListItem>
                                <asp:ListItem Value="07">07:00 &#224;s 08:00</asp:ListItem>
                                <asp:ListItem Value="08">08:00 &#224;s 09:00</asp:ListItem>
                                <asp:ListItem Value="09">09:00 &#224;s 10:00</asp:ListItem>
                                <asp:ListItem Value="10">10:00 &#224;s 11:00</asp:ListItem>
                                <asp:ListItem Value="11">11:00 &#224;s 12:00</asp:ListItem>
                                <asp:ListItem Value="12">12:00 &#224;s 13:00</asp:ListItem>
                                <asp:ListItem Value="13">13:00 &#224;s 14:00</asp:ListItem>
                                <asp:ListItem Value="14">14:00 &#224;s 15:00</asp:ListItem>
                                <asp:ListItem Value="15">15:00 &#224;s 16:00</asp:ListItem>
                                <asp:ListItem Value="16">16:00 &#224;s 17:00</asp:ListItem>
                                <asp:ListItem Value="17">17:00 &#224;s 18:00</asp:ListItem>
                                <asp:ListItem Value="18">18:00 &#224;s 19:00</asp:ListItem>
                                <asp:ListItem Value="19">19:00 &#224;s 20:00</asp:ListItem>
                                <asp:ListItem Value="20">20:00 &#224;s 21:00</asp:ListItem>
                                <asp:ListItem Value="21">21:00 &#224;s 22:00</asp:ListItem>
                                <asp:ListItem Value="22">22:00 &#224;s 23:00</asp:ListItem>
                                <asp:ListItem Value="23">23:00 &#224;s 00:00</asp:ListItem>
                                <asp:ListItem Value="-1" Selected="True">Todos</asp:ListItem>
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
                            &nbsp; Placa:
                        </td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtPlaca" data-mask="aaa-9z99" runat="server" CssClass="input-small" />                            
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
                "transportadora": "",
                "horaPeacao": "",
                "placa": "",

                "formato": ""
            };

            obj.dtIni = $("#txtDatInicio").val();
            obj.dtFim = $("#txtDatFim").val();
            obj.transportadora = $("#ddlTransportadora").val();
            obj.horaPeacao = $("#ddlHoraPeacao").val();
            obj.placa = $("#txtPlaca").val();

            obj.formato = $("#ddlFormato").val();

            $.ajax({
                url: 'genericReport.aspx/tempoPeacao',
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
