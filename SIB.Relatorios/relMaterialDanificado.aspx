<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="relMaterialDanificado.aspx.cs" Inherits="SIB.Relatorios.relUtilizacaoLona" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

     <form id="frmPesoTotalEscoado" runat="server">        
         <br />
            <div class="well" style="width:792px">
                <table>
                    <tr>
                        <td colspan="4">
                            <strong><span style="font-size: 11pt; color:green"">Material Danificado</span></strong>
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
                            &nbsp; Nº Lote:
                        </td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtNumLote" runat="server" CssClass="input-small"></asp:TextBox>
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
                "numLote": "",
                "placa": "",
                
                "formato": ""
            };

            obj.dtIni = $("#txtDatInicio").val();
            obj.dtFim = $("#txtDatFim").val();
            obj.transportadora = $("#ddlTransportadora").val();
            obj.numLote = $("#txtNumLote").val();
            obj.placa = $("#txtPlaca").val();
            
            obj.formato = $("#ddlFormato").val();

            $.ajax({
                url: 'genericReport.aspx/materialDanificado',
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
