<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="relVistoriadosECarregados.aspx.cs" Inherits="SIB.Relatorios.relVistoriadosECarregados" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cD" runat="server">
    
    <form id="frmVistoriadosEcarregados" runat="server">        
         <br />
            <div class="well" style="width:1120px">
                <table>
                    <tr>
                        <td colspan="4">
                            <strong><span style="font-size: 11pt; color:green">Veículos Vistoriados</span></strong>
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
                            &nbsp; Tipo Veículo:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlTipoVeiculo" runat="server" CssClass="input-large">   
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
                            <asp:TextBox ClientIDMode="Static" ID="txtDatFim" data-mask="99/99/9999" runat="server" CssClass="input-small"></asp:TextBox>
                        </td>                
                        <td>
                           &nbsp; Tipo Carregamento: 
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlTipoCarregamento" runat="server" CssClass="input-large">     
                                <asp:ListItem Value="Bobineira">Bobina</asp:ListItem>
                                <asp:ListItem Value="Chapa">Chapa</asp:ListItem>
                                <asp:ListItem Value="Outros">Outros</asp:ListItem>                                
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>   
                    </tr>            
                    <tr>                        
                        <td>
                            Tipo Assoalho:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlTipoAssoalho" runat="server" CssClass="input-medium">                                                                    
                                <asp:ListItem Value="1" Text="Bom">Ferro</asp:ListItem>                                
                                <asp:ListItem Value="2" Text="Bom">Madeira</asp:ListItem>                                
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>                
                        <td>
                           &nbsp; Ano veículo:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlAnoVeiculo" runat="server" CssClass="input-large">  
                               <asp:ListItem Value="0">Anterior a 1985</asp:ListItem>
                               <asp:ListItem Value="1986">1986 a 1990</asp:ListItem> 
                               <asp:ListItem Value="1991">1991 a 1995</asp:ListItem>
                               <asp:ListItem Value="1996">1996 a 2000</asp:ListItem>
                               <asp:ListItem Value="2001">2001 a 2005</asp:ListItem>
                               <asp:ListItem Value="2006">2006 a 2010</asp:ListItem>
                               <asp:ListItem Value="2011">2011 a 2015</asp:ListItem>
                               <asp:ListItem Value="2016">2016 a 2020</asp:ListItem>                                                                                                  
                               <asp:ListItem Value="-1" Selected="True">Todos</asp:ListItem>                                                                                                  
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>     
                        <td>
                            Tipo Transporte:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlTipoTransporte" runat="server" CssClass="input-medium">                                                                    
                                <asp:ListItem Value="1">CIF</asp:ListItem>
                                <asp:ListItem Value="2">FOB</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
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
                            Carregamento:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlUsinaDeposito" runat="server" CssClass="input-medium">                                                                    
                                <asp:ListItem Value="1">Usina</asp:ListItem>
                                <asp:ListItem Value="2">Deposito</asp:ListItem>                                
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>                                                           
                        <td>
                            &nbsp; Veículo Dedicado:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlDedicado" runat="server" CssClass="input-medium">    
                                <asp:ListItem Value="1">Sim</asp:ListItem>
                                <asp:ListItem Value="2">Não</asp:ListItem>                                
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr> 
                    <tr>     
                        <td>
                            Frota/Agregado/Particular:
                        </td>
                        <td>
                            <asp:DropDownList ClientIDMode="Static" ID="ddlTipoVinculo" runat="server" CssClass="input-medium">                                                                    
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
                            <asp:DropDownList ClientIDMode="Static" ID="ddlCargaDescarga" runat="server" CssClass="input-medium">    
                                <asp:ListItem Value="1">Carga</asp:ListItem>
                                <asp:ListItem Value="2">Descarga</asp:ListItem>     
                                <asp:ListItem Value="3">Troca de Nota</asp:ListItem>                            
                                <asp:ListItem Value="0" Selected="True">Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>                 
                    <tr>
                        <td>
                            Placa:
                        </td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtPlaca" data-mask="aaa-9z99" runat="server" CssClass="input-small" />
                        </td>
                        <td>
                           &nbsp; Tipo de Carroceria / Semi Reboque:
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
                           &nbsp; Movimentou Tampas? 
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
                           &nbsp; Número da DT:
                        </td>
                        <td>
                            <asp:TextBox ClientIDMode="Static" ID="txtDT" runat="server" CssClass="input-small"></asp:TextBox>
                        </td>                                   
                    </tr>
                    <tr>
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
                "tipoVeiculo": "",
                "tipoCarregamento": "",
                "tipoAssoalho": "",
                "ano": "",
                "tipoTransporte": "",
                "transportadora": "",
                "usinaDeposito": "",
                "dedicado": "",
                "tipoVinculo": "",
                "cargaDescarga": "",
                "placa": "",
                "tipoCarroceria": "",
                "catracaTodaAmarracao": "",
                "movimentouTampas": "",
                "bercoMetalicoBobineira": "",
                "dt": "",
                "formato": ""
            };

            obj.dtIni = $("#txtDatInicio").val();
            obj.dtFim = $("#txtDatFim").val();
            obj.tipoVeiculo = $("#ddlTipoVeiculo").val();
            obj.tipoCarregamento = $("#ddlTipoCarregamento").val();
            obj.tipoAssoalho = $("#ddlTipoAssoalho").val();
            obj.ano = $("#ddlAnoVeiculo").val();
            obj.tipoTransporte = $("#ddlTipoTransporte").val();
            obj.transportadora = $("#ddlTransportadora").val();
            obj.usinaDeposito = $("#ddlUsinaDeposito").val();
            obj.dedicado = $("#ddlDedicado").val();
            obj.tipoVinculo = $("#ddlTipoVinculo").val();
            obj.cargaDescarga = $("#ddlCargaDescarga").val();
            obj.placa = $("#txtPlaca").val();
            obj.tipoCarroceria = $("#ddlTipoCarroceria").val();
            obj.catracaTodaAmarracao = $("#ddlUtilizouCatraca").val();
            obj.movimentouTampas = $("#ddlPrecisouMovimentar").val();
            obj.bercoMetalicoBobineira = $("#ddlBercoMetalicoBobineira").val();
            obj.dt = $("#txtDT").val();
            obj.formato = $("#ddlFormato").val();

            $.ajax({
                url: 'genericReport.aspx/vistoriadosEcarregados',
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
