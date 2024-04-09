using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.Design;
using WebMatrix.WebData;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using SIB.Web.Models.Auxiliares;
using SIB.Web.Relatorios;

namespace SIB.Web.Controllers
{
    public class ImpressaoChecklistController : Controller
    {
        private object model;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult geraRelatorioImpressao() //ActionResult
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            System.Data.DataTable dtSource = new System.Data.DataTable();

            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            string sqlQuery = @"SELECT 
                                chk.data_checklist AS DataInicio, 
                                chk.data_fim_checklist AS DataFim, 
                                chk.data_inicio_peacao AS InicioPeacao, 
                                chk.data_fim_peacao AS FimPeacao, 
                                chk.chk_DT, 
                                chk.tonelada, 
                                tv.desc_tipo_veiculo AS TipoVeiculo, 
                                chk.chk_numeroEntregas AS QtdeEntregas, 
                                chk.placa, 
                                tt.desc_tipo_transporte AS TipoTransporte, 
                                chk.chk_placa2, 
                                chk.chk_placa3, 
                                tf.desc_tipo_frete, 
                                CASE chk.chk_cargaDescarga WHEN 1 THEN 'Carga' WHEN 2 THEN 'Descarga' WHEN 3 THEN 'Troca de Nota' ELSE '' END AS CargaDescarga, 
                                mv.desc_marca_veiculo AS Marca, 
                                veic.modelo, 
                                cv.desc_cor_veiculo AS CorVeiculo, 
                                CASE WHEN veic.ano = 0 THEN 'Anterior a 1985' 
                                     WHEN veic.ano BETWEEN 1986 and 1990 THEN '1986 a 1990' 
                                     WHEN veic.ano BETWEEN 1991 and 1995 THEN '1991 a 1995' 
                                     WHEN veic.ano BETWEEN 1996 and 2000 THEN '1996 a 2000' 
                                     WHEN veic.ano BETWEEN 2001 and 2005 THEN '2001 a 2005' 
                                     WHEN veic.ano BETWEEN 2006 and 2010 THEN '2006 a 2010' 
                                     WHEN veic.ano BETWEEN 2011 and 2015 THEN '2011 a 2015' 
                                     WHEN veic.ano BETWEEN 2016 and 2020 THEN '2016 a 2020' 
                                END as Ano, 
                                CASE chk.chk_usinaDeposito WHEN 1 THEN 'Usina' WHEN 2 THEN 'Depósito' END AS UsinaDeposito, 
                                'Não' AS LiberadoRestricao, 
                                cond.nome_condutor AS Condutor, 
                                chk.cpf_condutor, 
                                tc.desc_tipo_carroceria AS TipoCarroceria, 
                                CASE chk.chk_carroceria WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS Carroceria, 
                                CASE chk.chk_epi WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS EPI, 
                                trans.nome_fantasia AS Transportadora, 
                                (select usu.nome_usuario from checklist_fase cf inner join usuario usu on usu.id_usuario = cf.id_usuario where cf.id_fase = 1 and cf.id_checklist = chk.id_checklist) AS VistoriadorInicial, 
                                (select usu.nome_usuario from checklist_fase cf inner join usuario usu on usu.id_usuario = cf.id_usuario where cf.id_fase = 2 and cf.id_checklist = chk.id_checklist) AS VistoriadorFinal, 
                                CASE chk.chk_capaceteCarnJugular WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS CapaceteCarnJugular, 
                                CASE chk.chk_oculosSeguranca WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS OculosSeguranca, 
                                CASE chk.chk_protetorAuditivo WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS ProtetorAuditivo, 
                                CASE chk.chk_perneira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS Perneira, 
                                CASE chk.chk_botina WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS Botina, 
                                CASE chk.chk_parabrisaTrincado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS ParabrisaTrincado,	 
                                CASE chk_trincaTolerancia WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS TrincaTolerancia, 
                                CASE chk.chk_retrovisorTrincado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS RetrovisorTrincado,	 
                                CASE chk_trincaRetrovisorTolerancia WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS TrincaRetrovisorTolerancia, 
                                CASE chk.chk_assoalho WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS Assoalho, 
                                CASE chk.chk_situacaoAssoalho WHEN 1 THEN 'Limpo' WHEN 2 THEN 'Sujo' END AS EstadoAssoalho, 
                                CASE chk.chk_pneusEstepe WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS PneusEstepe, 
                                CASE chk.chk_faroisLanterna WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS FaroisLanterna, 
                                CASE chk.chk_necesMontarBerco WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS MontarBerco, 
                                CASE chk_possui WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS PossuiBercoMetalico, 
                                CASE chk_liberado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS BercoMetalicoLiberado, 
                                CASE chk_metalico WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS Metalico, 
                                CASE chk_bercoProprio WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS BercoProrio, 
                                CASE chk_bobineira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS Bobineira, 
                                CASE chk.chk_tacografo WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS Tacografo, 
                                CASE chk_extintor WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS Extintor, 
                                CASE chk_chassi WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS Chassi, 
                                CASE chk_triangMacaco WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS TrianguloMacaco, 
                                CASE chk_condicoesBobineira WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS CondicoesBobineira, 
                                CASE chk_lona WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' ELSE '' END AS Lona, 
                                CASE chk.chk_materialMolhado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END MaterialMolhado, 
                                CASE chk.chk_retornouUsina WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END RetornouUsina, 
                                REPLACE(chk.chk_material, '/', ' | ') AS TipoMaterial, 
                                REPLACE(chk.chk_numLote, '/', ' / ') AS NumeroLote, 
                                UPPER(chk.chk_cliente) AS Cliente, 
                                UPPER(chk.chk_observacao) AS Observacao, 
                                CASE chk.chk_amarracaoCarreta WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS AmarracaoCarreta, 
                                CASE chk.chk_condicoes_embalagem_material WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS condicoesEmbalagemMaterial, 
                                CASE chk.chk_cintas WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' ELSE '' END AS Cintas, 
                                CASE chk.chk_devidamentePregado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS DevidamentePregado, 
                                CASE chk.chk_borrachas_assoalho_carga WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS BorrachasAssoalhoCarga, 
                                CASE chk.veiculo_liberado WHEN 1 THEN 'Aprovado' WHEN 2 THEN 'Reprovado' ELSE '' END AS StatusInspecao, 
                                CASE chk_cordasCintasCatracas WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS CordasCintasCatracas, 
                                CASE chk_alturaVeiculo WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS AlturaVeiculo, 
                                CASE chk_parafusoRodas WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS TodosParafusosRodas, 
                                CASE chk_drenoBobineira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS DrenoBobineira, 
                                CASE chk_setas WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS Setas, 
                                CASE chk_sinalSonoroRe WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS SinalSonoroRe, 
                                CASE chk_dobradicasBoasCondicoes WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS DobradicasBoasCondicoes, 
                                CASE chk_dormentesFixados WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS possuiBorrachasAssoalho, 
                                CASE chk_possui_borrachas_assoalho WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS DormentesFixados, 
                                CASE chk_todosPinos WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS TodosPinos, 
                                CASE chk_laudoGanchoAdaptado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS LaudoGanchoAdaptado, 
                                CASE chk_estruturaTampasFerroMadeira WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' ELSE '' END AS TampasFerroMadeira, 
                                CASE chk_avariaMaterial WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS AvariaMaterial, 
                                CASE chk_tmpGuardaLatTravad WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS TmpGuardaLatTrav, 
                                CASE chk_materialChovendo WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS Chovendo, 
                                CASE chk_bercoMetalicoBobineira WHEN 1 THEN 'Berço Metálico' WHEN 2 THEN 'Bobineira' WHEN 3 THEN 'NA' END AS BercoMetalicoBobineira, 
                                CASE chk.chk_fotosCondicoesBobineira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS FotosCondicoesBobineiras, 
                                CASE chk_cargaSecaGraneleiro WHEN 1 THEN 'Carga Seca' WHEN 2 THEN 'Graneleiro' END AS CargaSecaGraneleiro, 
                                CASE chk_catracaoTodaAmarracao WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS CatracaTodaAmarracao, 
                                CASE chk_precisouMovimentarTampas WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS PrecisouMovimentarTampas, 
                                chk_medidorFumacaNegra AS FumacaNegra, 
                                ta.desc_tipo_assoalho AS TipoAssoalho, 
                                CASE chk.chk_mangote WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS Mangote, 
                                CASE chk.chk_possuiLaudoOpacidade WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS PossuiLaudoOpacidade, 
                                CASE chk.chk_entregaAlavanca WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'Catracão' END AS AlavancaEntregue, 
                                CASE chk.chk_devolucaoAlavanca WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS AlavancaDevolvida, 
                                CASE chk.chk_placaLegivel WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS PlacaLegivel, 
                                CASE chk.chk_ganchoPatola WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS GanchoPatola, 
                                CASE chk.chk_estadoGeral WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS EstadoGeral, 
                                CASE chk.chk_vazamentoOleo WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS VazamentoOleo, 
                                CASE chk.chk_tipoAvaria WHEN 1 THEN 'Amassado' WHEN 2 THEN 'Oxidado' WHEN 3 THEN 'Molhado' WHEN 4 THEN 'Empoeirado' WHEN 5 THEN 'Empenada' WHEN 6 THEN 'Rebarba' WHEN 7 THEN 'Embalagem Danificada' END AS TipoAvaria,
                                CASE chk.chk_tipoLona WHEN 1 THEN 'Lonil' WHEN 2 THEN 'Encerado' ELSE '' END AS TipoLona, 
                                chk_qtdeCunha AS QtdeCunha, 
                                CASE chk.chk_cintamentoFardos WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS CintamentoFardos, 
                                CONVERT(VARCHAR, chk.data_realizacao_laudoOpacidade, 103) AS DataRealizacaoLaudo, 
                                CASE chk.chk_drenosDesobstruidos WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'Catracão' END AS DrenosDesobstruidos, 
                                CASE chk.chk_borrachaBobineiraInterica WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'Catracão' END AS BorrachaBobineiraInterica, 
                                CASE chk.chk_borrachaFixaBobineira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'Catracão' END AS BorrachaFixaBobineira,
                                CASE chk.chk_travaSegurancaTampas WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' END AS TravaSegTampas,
                                REPLACE(dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist), '<BR>', ' | ') AS Motivos 
                            FROM checklist chk 
                            LEFT JOIN tipo_veiculo tv ON tv.id_tipo_veiculo = chk.id_tipo_veiculo 
                            LEFT JOIN tipo_carroceria tc ON tc.id_tipo_carroceria = chk.id_tipo_carroceria 
                            LEFT JOIN tipo_assoalho ta ON ta.id_tipo_assoalho = chk.id_tipo_assoalho 
                            LEFT JOIN tipo_transporte tt ON tt.id_tipo_transporte = chk.id_tipo_transporte 
                            LEFT JOIN tipo_frete tf ON tf.id_tipo_frete = chk.id_tipo_frete 
                            INNER JOIN veiculo veic ON veic.placa = chk.placa 
                            LEFT JOIN marca_veiculo mv ON mv.id_marca_veiculo = veic.id_marca_veiculo 
                            LEFT JOIN cor_veiculo cv ON cv.id_cor_veiculo = veic.id_cor_veiculo 
                            INNER JOIN condutor cond ON cond.cpf_condutor = chk.cpf_condutor 
                            LEFT JOIN empresa trans ON trans.id_empresa = chk.id_emp_transportadora 
                            WHERE chk.id_checklist = " + multiCheck.check.id_checklist;

            sqlQuery +=
                            " ORDER BY " +
                                "chk.data_checklist DESC ";


            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 180;
            da.Fill(dtSource);
            conn.Close();
            conn.Dispose();
            
            return new CrystalReportPdfResult(Server.MapPath("~/Relatorios/rptChecklistImpressao.rpt"), dtSource);
        }

        private static byte[] StreamToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

    }
    public class CrystalReportPdfResult : ActionResult
    {
        private readonly byte[] _contentBytes;

        public CrystalReportPdfResult(string reportPath, object dataSet)
        {
            using (ReportDocument reportDocument = new ReportDocument())
            {
                reportDocument.Load(reportPath);
                reportDocument.SetDataSource(dataSet);
                _contentBytes = StreamToBytes(reportDocument.ExportToStream(ExportFormatType.PortableDocFormat));
                reportDocument.Close();
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = "Report.pdf",
                Inline = false
            };

            var response = context.HttpContext.ApplicationInstance.Response;
            response.Clear();
            response.Buffer = false;
            response.ClearContent();
            response.ClearHeaders();
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.AppendHeader("Content-Disposition", cd.ToString());
            response.ContentType = "application/pdf";

            using (var stream = new MemoryStream(_contentBytes))
            {
                stream.WriteTo(response.OutputStream);
                stream.Flush();
                stream.Close();
            }
        }

        private static byte[] StreamToBytes(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
