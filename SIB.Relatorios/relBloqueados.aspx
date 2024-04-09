<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="relBloqueados.aspx.cs" Inherits="SIB.Relatorios.relBloqueados" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">

    <form id="frmBloqueados" runat="server">        
         <br />
            <div class="well" style="width:1120px">
                <table>
                    <tr>
                        <td colspan="4">
                            <strong><span style="font-size: 11pt; color:green">Veículos Bloqueados</span></strong>
                            <br /><br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data Inicial:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDatInicio" ClientIDMode="Static" data-mask="99/99/9999" runat="server" CssClass="input-small"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp; Tipo Veículo:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoVeiculo" ClientIDMode="Static" runat="server" CssClass="input-large">   
                                <asp:ListItem Value="1">Toco</asp:ListItem>                                
                                <asp:ListItem Value="2">Truck</asp:ListItem>
                                <asp:ListItem Value="3">Truck Trucado</asp:ListItem>                                
                                <asp:ListItem Value="4">Cavalo Mecânico Trucado</asp:ListItem>
                                <asp:ListItem Value="5">Cavalo Mecânico Simples</asp:ListItem>
                                <asp:ListItem Value="0">Todos</asp:ListItem>                 
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>     
                        <td>
                            Data Final:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDatFim" ClientIDMode="Static" data-mask="99/99/9999" runat="server" CssClass="input-small"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp; Transportadora:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTransportadora" ClientIDMode="Static" runat="server" CssClass="input-large">                                            
                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>     
                        <td>
                            Frota/Agregado/Particular:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoVinculo" ClientIDMode="Static" runat="server" CssClass="input-medium">                                                                    
                                <asp:ListItem Value="1">Frota</asp:ListItem>
                                <asp:ListItem Value="2">Agregado</asp:ListItem>                                
                                <asp:ListItem Value="3">Particular</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>                                                           
                        <td>
                            &nbsp; Carga/Descarga:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCargaDescarga" ClientIDMode="Static" runat="server" CssClass="input-medium">    
                                <asp:ListItem Value="1">Carga</asp:ListItem>
                                <asp:ListItem Value="2">Descarga</asp:ListItem>                                
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Placa:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPlaca" ClientIDMode="Static" data-mask="aaa-9z99" runat="server" CssClass="input-small" />
                        </td>
                        <td>
                           &nbsp; Tipo de Carroceria / Semi Reboque:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTipoCarroceria" ClientIDMode="Static" runat="server" CssClass="input-large">     
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
                        </td>
                    </tr>
                </table>  
            </div>
    </form>

    <script type="text/javascript">

        $("#btnFiltrar").click(function (e) {

            var obj = {
                "dtIni": "",
                "dtFim": "",
                "tipoVeiculo": "",
                "transportadora": "",
                "tipoVinculo": "",
                "cargaDescarga": "",
                "placa": "",
                "tipoCarroceria": "",
                "formato": ""
            };

            obj.dtIni = $("#txtDatInicio").val();
            obj.dtFim = $("#txtDatFim").val();
            obj.tipoVeiculo = $("#ddlTipoVeiculo").val();
            obj.transportadora = $("#ddlTransportadora").val();
            obj.tipoVinculo = $("#ddlTipoVinculo").val();
            obj.cargaDescarga = $("#ddlCargaDescarga").val();
            obj.placa = $("#txtPlaca").val();
            obj.tipoCarroceria = $("#ddlTipoCarroceria").val();

            obj.formato = $("#ddlFormato").val();

            $.ajax({
                url: 'genericReport.aspx/bloqueados',
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
