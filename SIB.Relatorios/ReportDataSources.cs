using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using SIB.Data;
using System.Configuration;
using System.Globalization;

namespace SIB.Relatorios
{
    public class ReportDataSources
    {
        public CultureInfo culture = new CultureInfo("pt-BR");

        public DataTable dtsVistoriadosCarregados()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

            string sqlQuery = @"SELECT 
                                data_checklist AS DataInicio, 
                                data_fim_checklist AS DataFim, 
                                chk.placa, 
                                tv.desc_tipo_veiculo AS TipoVeiculo, 
                                tc.desc_tipo_carroceria AS TipoCarroceria, 
                                transp.nome_fantasia AS Transportadora, 
                                CASE veiculo_liberado WHEN 1 THEN 'Aprovado' WHEN 2 THEN 'Reprovado' ELSE '' END AS StatusInspecao, 
                                CASE WHEN veic.ano = 0 THEN 'Anterior a 1985' 
                                        WHEN veic.ano BETWEEN 1986 and 1990 THEN '1986 a 1990' 
                                        WHEN veic.ano BETWEEN 1991 and 1995 THEN '1991 a 1995' 
                                        WHEN veic.ano BETWEEN 1996 and 2000 THEN '1996 a 2000' 
                                        WHEN veic.ano BETWEEN 2001 and 2005 THEN '2001 a 2005' 
                                        WHEN veic.ano BETWEEN 2006 and 2010 THEN '2006 a 2010' 
                                        WHEN veic.ano BETWEEN 2011 and 2015 THEN '2011 a 2015' 
                                        WHEN veic.ano BETWEEN 2016 and 2020 THEN '2016 a 2020' 
                                END as Ano, 
                                CASE chk.chk_cargaDescarga WHEN 1 THEN 'Carga' WHEN 2 THEN 'Descarga' WHEN 3 THEN 'Troca de Nota' END AS CargaDescarga, 
                                CASE chk.chk_usinaDeposito WHEN 1 THEN 'Usina' WHEN 2 THEN 'Depósito' END AS UsinaDeposito, 
                                tf.desc_tipo_frete AS TipoFrete, 
                                usu.nome_usuario AS Vistoriador 
                            FROM checklist chk 
                            LEFT JOIN tipo_veiculo tv ON tv.id_tipo_veiculo = chk.id_tipo_veiculo 
                            LEFT JOIN tipo_carroceria tc ON tc.id_tipo_carroceria = chk.id_tipo_carroceria 
                            LEFT JOIN empresa transp ON transp.id_empresa = chk.id_emp_transportadora 
                            INNER JOIN veiculo veic ON veic.placa = chk.placa 
                            LEFT JOIN usuario usu ON usu.id_usuario = chk.id_usuario 
                            LEFT JOIN tipo_transporte tt ON tt.id_tipo_transporte = chk.id_tipo_transporte 
                            LEFT JOIN tipo_frete tf ON tf.id_tipo_frete = chk.id_tipo_frete 
                            WHERE chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"], culture).ToString("s") + "' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"], culture).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("s") + "' ";

            //Tipo Assoalho
            if (Convert.ToInt32(HttpContext.Current.Session["tipoAssoalho"]) > 0)
                sqlQuery += " AND chk.id_tipo_assoalho = " + HttpContext.Current.Session["tipoAssoalho"];

            //Tipo Transporte
            if (Convert.ToInt32(HttpContext.Current.Session["tipoTransporte"]) > 0)
                sqlQuery += " AND chk.id_tipo_frete = " + HttpContext.Current.Session["tipoTransporte"];

            //Número Ordem de Embarque
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["dt"].ToString()))
                sqlQuery += " AND CONVERT(VARCHAR, chk.chk_DT) like '" + HttpContext.Current.Session["dt"] + "' ";

            //Tipo Carregamento / Material
            if (HttpContext.Current.Session["tipoCarregamento"].ToString() != "0")
                sqlQuery += " AND chk.chk_material like '%" + HttpContext.Current.Session["tipoCarregamento"] + "%' ";

            //Tipo Veículo
            if (Convert.ToInt32(HttpContext.Current.Session["tipoVeiculo"]) > 0)
                sqlQuery += " AND chk.id_tipo_veiculo = " + HttpContext.Current.Session["tipoVeiculo"];

            //Ano veiculo
            if (Convert.ToInt32(HttpContext.Current.Session["ano"]) != -1)
                sqlQuery += " AND veic.ano BETWEEN " + HttpContext.Current.Session["ano"] + " AND " + (Convert.ToInt32(HttpContext.Current.Session["ano"]) + 4);

            //Tipo Carroceria
            if (Convert.ToInt32(HttpContext.Current.Session["tipoCarroceria"]) > 0)
                sqlQuery += " AND chk.id_tipo_carroceria = " + HttpContext.Current.Session["tipoCarroceria"];

            //Placa
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["placa"].ToString()))
                sqlQuery += " AND chk.placa = '" + HttpContext.Current.Session["placa"].ToString().Replace("-", "") + "' ";

            //Frota/Agregado/Particular
            if (Convert.ToInt32(HttpContext.Current.Session["tipoVinculo"]) > 0)
                sqlQuery += " AND chk.id_tipo_transporte = " + HttpContext.Current.Session["tipoVinculo"];

            //Carregamento
            if (Convert.ToInt32(HttpContext.Current.Session["usinaDeposito"]) > 0)
                sqlQuery += " AND chk.chk_usinaDeposito = " + HttpContext.Current.Session["usinaDeposito"];

            //Carga/Descarga            
            if (Convert.ToInt32(HttpContext.Current.Session["cargaDescarga"]) > 0)
                sqlQuery += " AND chk.chk_cargaDescarga = " + HttpContext.Current.Session["cargaDescarga"];

            //Veículo Dedicado            
            if (Convert.ToInt32(HttpContext.Current.Session["dedicado"]) > 0)
                sqlQuery += " AND chk.chk_veicDedicado = " + HttpContext.Current.Session["dedicado"];


            //Somente Usuários Que são da Transportadora
            Int32 id_usuario = Convert.ToInt32(HttpContext.Current.Session["id"]);
            var usuario = _dbContext.usuario.Where(r => r.id_usuario == id_usuario).ToList()[0];

            if (usuario.id_tipo_usuario == 3)
                sqlQuery += " AND chk.id_emp_transportadora = " + usuario.id_transportadora;
            else
            {
                if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                    sqlQuery += " AND chk.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];
            }

            if (HttpContext.Current.Session["catracaTodaAmarracao"].ToString() != "0")
                sqlQuery += " AND chk.chk_catracaoTodaAmarracao = " + HttpContext.Current.Session["catracaTodaAmarracao"];

            if (HttpContext.Current.Session["movimentouTampas"].ToString() != "0")
                sqlQuery += " AND chk.chk_precisouMovimentarTampas = " + HttpContext.Current.Session["movimentouTampas"];

            if (HttpContext.Current.Session["bercoMetalicoBobineira"].ToString() != "0")
                sqlQuery += " AND chk.chk_bercoMetalicoBobineira = " + HttpContext.Current.Session["bercoMetalicoBobineira"];

            sqlQuery += " ORDER BY chk.data_checklist DESC";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "rel_vistoriados_carregados");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["rel_vistoriados_carregados"];
        }

        public DataTable dtsRecusados()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

            string sqlQuery = @"SELECT 
                            chk.data_checklist AS [Data Inicio], 
                            chk.data_fim_checklist AS [Data Fim], 
                            chk.placa, 
                            tv.desc_tipo_veiculo AS [Tipo Veiculo], 
                            emp.nome_fantasia AS Empresa, 
                            usu.nome_usuario AS Usuario, 
                            REPLACE(dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist), '<BR>', ' | ') AS Motivos 
                        FROM checklist chk 
                        INNER JOIN tipo_veiculo tv ON tv.id_tipo_veiculo = chk.id_tipo_veiculo 
                        INNER JOIN empresa emp ON emp.id_empresa = chk.id_emp_transportadora 
                        INNER JOIN usuario usu ON usu.id_usuario = chk.id_usuario 
                        INNER JOIN veiculo veic ON veic.placa = chk.placa 
                        WHERE chk.veiculo_liberado = 2 
                        AND chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"], culture).ToString("s") + "' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"], culture).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("s") + "' ";

            //Motivo Reprovação
            if (HttpContext.Current.Session["motivo"].ToString() != "0")
                sqlQuery += " AND REPLACE(dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist), '<BR>', ' ') LIKE '%" + HttpContext.Current.Session["motivo"] + "%'";

            //Placa
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["placa"].ToString()))
                sqlQuery += " AND chk.placa = '" + HttpContext.Current.Session["placa"].ToString().Replace("-", "") + "' ";

            //Somente Usuários Que são da Transportadora
            Int32 id_usuario = Convert.ToInt32(HttpContext.Current.Session["id"]);
            var usuario = _dbContext.usuario.Where(r => r.id_usuario == id_usuario).ToList()[0];

            if (usuario.id_tipo_usuario == 3)
                sqlQuery += " AND chk.id_emp_transportadora = " + usuario.id_transportadora;
            else
            {
                if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                    sqlQuery += " AND chk.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];
            }

            sqlQuery += " ORDER  BY chk.data_checklist DESC";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "rel_veic_recusados");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["rel_veic_recusados"];
        }

        public DataTable dtsRecuperados()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

            string sqlQuery = @"SELECT 
                              chk.data_checklist AS [Data], 
                              chk.placa, 
                              tv.desc_tipo_veiculo AS [tipo veiculo], 
                              REPLACE(dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist), '<BR>', ' | ' ) as [Motivos reprovacao] 
                            FROM checklist chk 
                            LEFT JOIN tipo_veiculo tv ON tv.id_tipo_veiculo = chk.id_tipo_veiculo 
                            WHERE chk.recuperado = 1 
                            AND chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"], culture).ToString("s") + "' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"], culture).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("s") + "' ";

            //Motivo Reprovação
            if (HttpContext.Current.Session["motivo"].ToString() != "0")
            {
                sqlQuery += " AND REPLACE(dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist), '<BR>', ' ') LIKE '%" + HttpContext.Current.Session["motivo"] + "%'";
            }

            //Somente Usuários Que são da Transportadora
            Int32 id_usuario = Convert.ToInt32(HttpContext.Current.Session["id"]);
            var usuario = _dbContext.usuario.Where(r => r.id_usuario == id_usuario).ToList()[0];

            if (usuario.id_tipo_usuario == 3)
            {
                sqlQuery += " AND chk.id_emp_transportadora = " + usuario.id_transportadora;
            }
            else
            {
                if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                {
                    sqlQuery += " AND chk.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];
                }
            }

            sqlQuery += "ORDER BY " +
                            "chk.data_checklist DESC ";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "rel_veic_recusados");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["rel_veic_recusados"];
        }

        public DataTable dtsChecklists()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

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
                                (select TOP 1 usu.nome_usuario from checklist_fase cf inner join usuario usu on usu.id_usuario = cf.id_usuario where cf.id_fase = 1 and cf.id_checklist = chk.id_checklist) AS VistoriadorInicial, 
                                (select TOP 1 usu.nome_usuario from checklist_fase cf inner join usuario usu on usu.id_usuario = cf.id_usuario where cf.id_fase = 2 and cf.id_checklist = chk.id_checklist) AS VistoriadorFinal, 
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
                                CASE chk_dormentesFixados WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS DormentesFixados, 
                                CASE chk_todosPinos WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS TodosPinos, 
                                CASE chk_laudoGanchoAdaptado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS LaudoGanchoAdaptado, 
                                CASE chk_estruturaTampasFerroMadeira WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' ELSE '' END AS TampasFerroMadeira, 
                                CASE chk_avariaMaterial WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS AvariaMaterial, 
                                CASE chk_tmpGuardaLatTravad WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS TmpGuardaLatTrav, 
                                CASE chk_materialChovendo WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS Chovendo, 
                                CASE chk_bercoMetalicoBobineira WHEN 1 THEN 'Berço Metálico' WHEN 2 THEN 'Bobineira' WHEN 3 THEN 'NA' END AS BercoMetalicoBobineira, 
                                CASE chk_cargaSecaGraneleiro WHEN 1 THEN 'Carga Seca' WHEN 2 THEN 'Graneleiro' END AS CargaSecaGraneleiro, 
                                CASE chk_catracaoTodaAmarracao WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS CatracaTodaAmarracao, 
                                CASE chk_precisouMovimentarTampas WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS PrecisouMovimentarTampas, 
                                chk_medidorFumacaNegra AS FumacaNegra, 
                                CASE chk.chk_tipoAvaria WHEN 1 THEN 'Amassado' WHEN 2 THEN 'Oxidado' WHEN 3 THEN 'Molhado' WHEN 4 THEN 'Empoeirado' WHEN 5 THEN 'Empenada' WHEN 6 THEN 'Rebarba' WHEN 7 THEN 'Embalagem Danificada' END AS TipoAvaria,
                                ta.desc_tipo_assoalho AS TipoAssoalho, 
                                CASE chk.chk_mangote WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS Mangote, 
                                CASE chk.chk_possuiLaudoOpacidade WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS PossuiLaudoOpacidade, 
                                CASE chk.chk_entregaAlavanca WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'Catracão' END AS AlavancaEntregue, 
                                CASE chk.chk_devolucaoAlavanca WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS AlavancaDevolvida, 
                                CASE chk.chk_placaLegivel WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS PlacaLegivel, 
                                CASE chk.chk_ganchoPatola WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS GanchoPatola, 
                                CASE chk.chk_estadoGeral WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS EstadoGeral, 
                                CASE chk.chk_vazamentoOleo WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS VazamentoOleo, 
                                CASE chk.chk_tipoLona WHEN 1 THEN 'Lonil' WHEN 2 THEN 'Encerado' ELSE '' END AS TipoLona, 
                                CASE chk.chk_fotosCondicoesBobineira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS FotosCondicoesBobineiras, 
                                chk_qtdeCunha AS QtdeCunha,
                                CASE chk.chk_cintamentoFardos WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS CintamentoFardos,  
                                CASE chk_condicoesBobineira WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS CondicoesBobineira, 
                                CONVERT(VARCHAR, chk.data_realizacao_laudoOpacidade, 103) AS DataRealizacaoLaudo, 
                                CASE chk.chk_drenosDesobstruidos WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'Catracão' END AS DrenosDesobstruidos, 
                                CASE chk.chk_borrachaBobineiraInterica WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'Catracão' END AS BorrachaBobineiraInterica, 
                                CASE chk.chk_borrachaFixaBobineira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' END AS BorrachaFixaBobineira,
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
                            WHERE chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"], culture).ToString("s") + "' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"], culture).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("s") + "' ";

            //Número Ordem de Embarque
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["dt"].ToString()))
                sqlQuery += " AND CONVERT(VARCHAR, chk.chk_DT) like '" + HttpContext.Current.Session["dt"] + "' ";

            //Placa
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["placa"].ToString()))
                sqlQuery += " AND chk.placa = '" + HttpContext.Current.Session["placa"].ToString().Replace("-", "") + "' ";

            //Motivo Reprovação
            if (HttpContext.Current.Session["motivo"].ToString() != "0")
                sqlQuery += " AND dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist) LIKE '%" + HttpContext.Current.Session["motivo"] + "%' ";

            //Somente Usuários Que são da Transportadora
            Int32 id_usuario = Convert.ToInt32(HttpContext.Current.Session["id"]);
            var usuario = _dbContext.usuario.Where(r => r.id_usuario == id_usuario).ToList()[0];

            if (usuario.id_tipo_usuario == 3)
                sqlQuery += " AND chk.id_emp_transportadora = " + usuario.id_transportadora;
            else
            {
                if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                    sqlQuery += " AND chk.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];
            }

            if (HttpContext.Current.Session["catracaAmarracao"].ToString() != "0")
                sqlQuery += " AND chk.chk_catracaoTodaAmarracao = " + HttpContext.Current.Session["catracaAmarracao"];

            if (HttpContext.Current.Session["movimentouTampas"].ToString() != "0")
                sqlQuery += " AND chk.chk_precisouMovimentarTampas = " + HttpContext.Current.Session["movimentouTampas"];

            if (HttpContext.Current.Session["bercoMetalicoBobineira"].ToString() != "0")
                sqlQuery += " AND chk.chk_bercoMetalicoBobineira = " + HttpContext.Current.Session["bercoMetalicoBobineira"];

            if (Convert.ToInt32(HttpContext.Current.Session["cargaSecaGraneleiro"]) > 0)
                sqlQuery += " AND chk.id_tipo_carroceria = " + HttpContext.Current.Session["cargaSecaGraneleiro"];

            sqlQuery +=
                            "ORDER BY " +
                                "chk.data_checklist DESC ";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "rel_relacao_checklist");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["rel_relacao_checklist"];
        }

        public DataTable dtsMaterialDanificado()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

            //Monta o Select
            string sqlQuery = @"SELECT 
                                CONVERT(VARCHAR, chk.data_checklist, 103) AS Data, 
                                chk.placa AS Placa, 
                                chk_cliente AS Cliente, 
                                REPLACE(chk_material, '/', '  ') AS TipoMaterial, 
                                chk_observacao AS Observacao, 
                                tra.nome_fantasia AS Transportadora, 
                                REPLACE(chk_numLote, '/', '  |  ') AS NumeroLote, 
                                CASE chk_retornouUsina WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS Retorno 
                            FROM checklist chk 
                            INNER JOIN empresa tra ON tra.id_empresa = chk.id_emp_transportadora 
                            INNER JOIN checklist_fase cf ON cf.id_checklist = chk.id_checklist AND cf.id_fase = 2 
                            WHERE chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"], culture).ToString("s") + "' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"], culture).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("s") + @"' 
                            AND (chk_materialMolhado = 1 OR chk_materialChovendo = 1 OR (chk_observacao IS NOT NULL AND chk_observacao <> '') ) ";

            if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                sqlQuery += "AND chk.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];

            //Número Ordem de Embarque
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["numLote"].ToString()))
                sqlQuery += " AND REPLACE(chk.chk_numLote, '/', ' ') like '%" + HttpContext.Current.Session["numLote"] + "%' ";

            //Placa
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["placa"].ToString()))
                sqlQuery += " AND chk.placa = '" + HttpContext.Current.Session["placa"].ToString().Replace("-", "") + "' ";

            sqlQuery += " ORDER BY " +
                        "CONVERT(VARCHAR, chk.data_checklist, 103) DESC ";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "rel_material_danificado");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["rel_material_danificado"];
        }

        public DataTable dtsTempoPermanencia()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

            //Monta o Select
            string sqlQuery = @"SELECT 
                                chk.placa AS [Placa], 
                                transp.nome_fantasia AS [Transportadora], 
                                tv.desc_tipo_veiculo AS [Tipo Veiculo], 
                                chk.data_checklist AS [Inicio Vistoria], 
                                chk.data_inicio_peacao AS [Inicio Peacao], 
                                chk.data_fim_peacao AS [Fim Peacao], 
                                dbo.fn_DiferencaHoras(chk.data_checklist, chk.data_fim_checklist) AS TempodePermanencia, 
                                dbo.fn_DiferencaHoras2(chk.data_checklist, chk.data_fim_checklist) AS tempoPermanencia,
                                u.nome_usuario AS Vistoriador
                            FROM checklist chk 
                            INNER JOIN empresa transp ON transp.id_empresa = chk.id_emp_transportadora 
                            INNER JOIN tipo_veiculo tv ON tv.id_tipo_veiculo = chk.id_tipo_veiculo 
                            INNER JOIN checklist_fase cf ON cf.id_checklist = chk.id_checklist AND cf.id_fase = 1 
                            INNER JOIN usuario u ON u.id_usuario = chk.id_usuario
                            WHERE ";

            if (Convert.ToInt32(HttpContext.Current.Session["horaPeacao"]) > -1)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["horaPeacao"]) < 9)
                    sqlQuery += "chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"]).ToString("yyyy-MM-dd") + " " + HttpContext.Current.Session["horaPeacao"] + ":00:00' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"]).ToString("yyyy-MM-dd") + " 0" + (HttpContext.Current.Session["horaPeacao"].ToString() == "23" ? "00" : Convert.ToString(Convert.ToInt32(HttpContext.Current.Session["horaPeacao"]) + 1)) + ":00:00' ";
                else
                    sqlQuery += "chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"]).ToString("yyyy-MM-dd") + " " + HttpContext.Current.Session["horaPeacao"] + ":00:00' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"]).ToString("yyyy-MM-dd") + " " + (HttpContext.Current.Session["horaPeacao"].ToString() == "23" ? "00" : Convert.ToString(Convert.ToInt32(HttpContext.Current.Session["horaPeacao"]) + 1)) + ":00:00' ";
            }
            else
                sqlQuery += " chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"]).ToString("yyyy-MM-dd") + " 00:00:00' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"]).ToString("yyyy-MM-dd") + " 23:59:59' ";

            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["placa"].ToString()))
                sqlQuery += "AND chk.placa = '" + HttpContext.Current.Session["placa"].ToString().Replace("-", "") + "' ";

            if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                sqlQuery += "AND chk.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];

            sqlQuery += " ORDER BY chk.data_checklist DESC ";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "rel_tempo_permanencia");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["rel_tempo_permanencia"];
        }

        public DataTable dtsTempoPeacao()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

            //Monta o Select
            string sqlQuery = @"SELECT 
                                chk.placa AS [Placa], 
                                transp.nome_fantasia AS [Transportadora], 
                                tv.desc_tipo_veiculo AS [Tipo Veiculo], 
                                chk.data_checklist AS [Inicio Vistoria], 
                                chk.data_inicio_peacao AS [Inicio Peacao], 
                                chk.data_fim_peacao AS [Fim Peacao], 
                                dbo.fn_DiferencaHoras(chk.data_checklist, chk.data_inicio_peacao) AS TempodePermanencia, 
                                dbo.fn_DiferencaHoras2(chk.data_checklist, chk.data_inicio_peacao) AS tempoPermanencia,
                                u.nome_usuario AS Vistoriador 
                            FROM checklist chk 
                            INNER JOIN empresa transp ON transp.id_empresa = chk.id_emp_transportadora 
                            INNER JOIN tipo_veiculo tv ON tv.id_tipo_veiculo = chk.id_tipo_veiculo 
                            INNER JOIN checklist_fase cf ON cf.id_checklist = chk.id_checklist AND cf.id_fase = 2 
                            INNER JOIN usuario u ON u.id_usuario = cf.id_usuario
                            WHERE ";

            if (Convert.ToInt32(HttpContext.Current.Session["horaPeacao"]) > -1)
            {
                if (Convert.ToInt32(HttpContext.Current.Session["horaPeacao"]) < 9)
                    sqlQuery += "chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"]).ToString("yyyy-MM-dd") + " " + HttpContext.Current.Session["horaPeacao"] + ":00:00' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"]).ToString("yyyy-MM-dd") + " 0" + (HttpContext.Current.Session["horaPeacao"].ToString() == "23" ? "00" : Convert.ToString(Convert.ToInt32(HttpContext.Current.Session["horaPeacao"]) + 1)) + ":00:00' ";
                else
                    sqlQuery += "chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"]).ToString("yyyy-MM-dd") + " " + HttpContext.Current.Session["horaPeacao"] + ":00:00' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"]).ToString("yyyy-MM-dd") + " " + (HttpContext.Current.Session["horaPeacao"].ToString() == "23" ? "00" : Convert.ToString(Convert.ToInt32(HttpContext.Current.Session["horaPeacao"]) + 1)) + ":00:00' ";
            }
            else
                sqlQuery += " chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"]).ToString("yyyy-MM-dd") + " 00:00:00' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"]).ToString("yyyy-MM-dd") + " 23:59:59' ";

            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["placa"].ToString()))
                sqlQuery += "AND chk.placa = '" + HttpContext.Current.Session["placa"].ToString().Replace("-", "") + "' ";

            if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                sqlQuery += "AND chk.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];

            sqlQuery += " ORDER BY chk.data_checklist DESC ";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "rel_tempo_peacao");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["rel_tempo_peacao"];
        }

        public DataTable dtsFumacaNegra()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

            //Monta o Select
            string sqlQuery = @"SELECT 
                                chk.data_checklist AS [Data/Hora Medicao], 
                                chk.placa AS [Placa], 
                                chk.chk_medidorFumacaNegra AS [Nivel Medido], 
                                tra.nome_fantasia AS Transportadora 
                            FROM checklist chk 
                            INNER JOIN empresa tra ON tra.id_empresa = chk.id_emp_transportadora 
                            WHERE chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"], culture).ToString("s") + "' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"]).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("s") + "' ";

            if (HttpContext.Current.Session["status"].ToString() != "0")
                sqlQuery += "AND chk.chk_medidorFumacaNegra = '" + HttpContext.Current.Session["status"] + "'";

            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["placa"].ToString()))
                sqlQuery += "AND chk.placa = '" + HttpContext.Current.Session["placa"].ToString().Replace("-", "") + "' ";

            Int32 id_usuario = Convert.ToInt32(HttpContext.Current.Session["id"]);
            var usuario = _dbContext.usuario.Where(r => r.id_usuario == id_usuario).ToList()[0];

            if (usuario.id_tipo_usuario == 3)
                sqlQuery += "AND chk.id_emp_transportadora = " + usuario.id_transportadora;
            else {
                if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                    sqlQuery += "AND chk.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];
            }

            sqlQuery += " ORDER BY chk.data_checklist DESC ";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "rel_fumaca_negra");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["rel_fumaca_negra"];
        }

        public DataTable dtsDT()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

            //Monta o Select
            string sqlQuery = @"SELECT 
                                chk.data_checklist AS DataInicio, 
                                chk.data_fim_checklist AS DataFim, 
                                chk.placa, 
                                tv.desc_tipo_veiculo AS TipoVeiculo, 
                                chk.chk_DT AS DT, 
                                emp.nome_fantasia AS Transportadora, 
                                CASE chk.veiculo_liberado WHEN 1 THEN 'Aprovado' WHEN 2 THEN 'Reprovado' END AS StatusInspecao, 
                                CASE WHEN veic.ano = 0 THEN 'Anterior a 1985' 
                                     WHEN veic.ano BETWEEN 1986 and 1990 THEN '1986 a 1990' 
                                     WHEN veic.ano BETWEEN 1991 and 1995 THEN '1991 a 1995' 
                                     WHEN veic.ano BETWEEN 1996 and 2000 THEN '1996 a 2000' 
                                     WHEN veic.ano BETWEEN 2001 and 2005 THEN '2001 a 2005' 
                                     WHEN veic.ano BETWEEN 2006 and 2010 THEN '2006 a 2010' 
                                     WHEN veic.ano BETWEEN 2011 and 2015 THEN '2011 a 2015' 
                                     WHEN veic.ano BETWEEN 2016 and 2020 THEN '2016 a 2020' 
                                END as Ano, 
                                usu.nome_usuario AS Vistoriador 
                            FROM checklist chk 
                            INNER JOIN tipo_veiculo tv ON tv.id_tipo_veiculo = chk.id_tipo_veiculo 
                            INNER JOIN empresa emp ON emp.id_empresa = chk.id_emp_transportadora 
                            INNER JOIN usuario usu ON usu.id_usuario = chk.id_usuario 
                            INNER JOIN veiculo veic ON veic.placa = chk.placa 
                            WHERE chk.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"], culture).ToString("s") + "' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"], culture).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("s") + "' ";

            if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                sqlQuery += "AND chk.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];

            //Tipo Assoalho
            if (Convert.ToInt32(HttpContext.Current.Session["tipoAssoalho"]) > 0)
                sqlQuery += " AND chk.id_tipo_assoalho = " + HttpContext.Current.Session["tipoAssoalho"];

            //Carga/Descarga
            if (Convert.ToInt32(HttpContext.Current.Session["usinaDeposito"]) > 0)
                sqlQuery += " AND chk.chk_usinaDeposito = " + HttpContext.Current.Session["usinaDeposito"];

            //Tipo Transporte
            if (Convert.ToInt32(HttpContext.Current.Session["tipoTransporte"]) > 0)
                sqlQuery += " AND chk.id_tipo_frete = " + HttpContext.Current.Session["tipoTransporte"];

            //Número Ordem de Embarque
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["dt"].ToString()))
                sqlQuery += " AND chk.chk_DT like '" + HttpContext.Current.Session["dt"].ToString() + "' ";

            //Tipo Carregamento / Material
            if (HttpContext.Current.Session["tipoCarregamento"].ToString() != "0")
                sqlQuery += " AND chk.chk_material like '%" + HttpContext.Current.Session["tipoCarregamento"].ToString() + "%' ";

            //Tipo Veículo
            if (Convert.ToInt32(HttpContext.Current.Session["tipoVeiculo"]) > 0)
                sqlQuery += " AND chk.id_tipo_veiculo = " + HttpContext.Current.Session["tipoVeiculo"];

            //Ano veiculo
            if (Convert.ToInt32(HttpContext.Current.Session["ano"]) != -1)
                sqlQuery += " AND veic.ano BETWEEN " + HttpContext.Current.Session["ano"] + " AND " + (Convert.ToInt32(HttpContext.Current.Session["ano"]) + 4);

            //Tipo Carroceria
            if (Convert.ToInt32(HttpContext.Current.Session["tipoCarroceria"]) > 0)
                sqlQuery += " AND chk.id_tipo_carroceria = " + HttpContext.Current.Session["tipoCarroceria"];

            //Placa
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["placa"].ToString()))
                sqlQuery += " AND chk.placa = '" + HttpContext.Current.Session["placa"].ToString().Replace("-", "") + "' ";

            sqlQuery += " ORDER BY chk.data_checklist DESC ";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "rel_DT");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["rel_DT"];
        }

        public DataTable dtsPesoEscoado()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

            string sqlQuery = @"SELECT
	                            c.placa AS Placa,
	                            c.data_inicio_peacao AS DataInicio,
	                            c.data_fim_peacao AS DataFim,
	                            c.chk_DT AS OE,
	                            tv.desc_tipo_veiculo AS TipoVeiculo,
	                            e.nome_fantasia AS Empresa,
	                            CASE c.veiculo_liberado WHEN 1 THEN 'Aprovado' ELSE 'Reprovado' END AS StatusInspecao,
	                            c.tonelada AS Peso,
	                            u.nome_usuario AS Vistoriador
                            FROM checklist c
                            INNER JOIN empresa e ON c.id_emp_transportadora = e.id_empresa
                            INNER JOIN tipo_veiculo tv ON tv.id_tipo_veiculo = c.id_tipo_veiculo
                            INNER JOIN usuario u ON u.id_usuario = c.id_usuario 
                            INNER JOIN veiculo veic ON veic.placa = c.placa
                            INNER JOIN checklist_fase cf ON cf.id_checklist = c.id_checklist AND cf.id_fase = 2 
                            WHERE c.data_inicio_peacao BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"], culture).ToString("s") + "' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"], culture).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("s") + "' ";

            //Tipo Assoalho
            if (Convert.ToInt32(HttpContext.Current.Session["tipoAssoalho"]) > 0)
                sqlQuery += " AND c.id_tipo_assoalho = " + HttpContext.Current.Session["tipoAssoalho"];

            //Tipo Transporte
            if (Convert.ToInt32(HttpContext.Current.Session["tipoTransporte"]) > 0)
                sqlQuery += " AND c.id_tipo_frete = " + HttpContext.Current.Session["tipoTransporte"];

            //Número Ordem de Embarque
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["dt"].ToString()))
                sqlQuery += " AND CONVERT(VARCHAR, c.chk_DT) like '" + HttpContext.Current.Session["dt"] + "' ";

            //Tipo Carregamento / Material
            if (HttpContext.Current.Session["tipoCarregamento"].ToString() != "0")
                sqlQuery += " AND c.chk_material like '%" + HttpContext.Current.Session["tipoCarregamento"] + "%' ";

            //Tipo Veículo
            if (Convert.ToInt32(HttpContext.Current.Session["tipoVeiculo"]) > 0)
                sqlQuery += " AND c.id_tipo_veiculo = " + HttpContext.Current.Session["tipoVeiculo"];

            //Ano veiculo
            if (Convert.ToInt32(HttpContext.Current.Session["ano"].ToString()) != -1)
            {
                if(Convert.ToInt32(HttpContext.Current.Session["ano"].ToString()) == 0)
                    sqlQuery += " AND veic.ano <= 1985 ";
                else
                    sqlQuery += " AND veic.ano BETWEEN " + HttpContext.Current.Session["ano"] + " AND " + (Convert.ToInt32(HttpContext.Current.Session["ano"].ToString()) + 4);
            }

            //Tipo Carroceria
            if (Convert.ToInt32(HttpContext.Current.Session["tipoCarroceria"]) > 0)
                sqlQuery += " AND c.id_tipo_carroceria = " + HttpContext.Current.Session["tipoCarroceria"];

            //Placa
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["placa"].ToString()))
                sqlQuery += " AND c.placa = '" + HttpContext.Current.Session["placa"].ToString().Replace("-", "") + "' ";

            //Frota/Agregado/Particular
            if (Convert.ToInt32(HttpContext.Current.Session["frotaAgregPart"]) > 0)
                sqlQuery += " AND c.id_tipo_transporte = " + HttpContext.Current.Session["frotaAgregPart"];

            //Carregamento
            if (Convert.ToInt32(HttpContext.Current.Session["usinaDeposito"]) > 0)
                sqlQuery += " AND c.chk_usinaDeposito = " + HttpContext.Current.Session["usinaDeposito"];

            //Carga/Descarga            
            if (Convert.ToInt32(HttpContext.Current.Session["cargaDescarga"]) > 0)
                sqlQuery += " AND c.chk_cargaDescarga = " + HttpContext.Current.Session["cargaDescarga"];

            //Veículo Dedicado            
            if (Convert.ToInt32(HttpContext.Current.Session["veiculoDedicado"]) > 0)
                sqlQuery += " AND c.chk_veicDedicado = " + HttpContext.Current.Session["veiculoDedicado"];


            //Somente Usuários Que são da Transportadora
            Int32 id_usuario = Convert.ToInt32(HttpContext.Current.Session["id"]);
            var usuario = _dbContext.usuario.Where(r => r.id_usuario == id_usuario).ToList()[0];

            if (usuario.id_tipo_usuario == 3)
                sqlQuery += " AND c.id_emp_transportadora = " + usuario.id_transportadora;
            else {
                if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                    sqlQuery += " AND c.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];
            }

            if (HttpContext.Current.Session["catracaTodaAmarracao"].ToString() != "0")
                sqlQuery += " AND c.chk_catracaoTodaAmarracao = " + HttpContext.Current.Session["catracaTodaAmarracao"];

            if (HttpContext.Current.Session["movimentouTampas"].ToString() != "0")
                sqlQuery += " AND c.chk_precisouMovimentarTampas = " + HttpContext.Current.Session["movimentouTampas"];

            if (HttpContext.Current.Session["bercoMetalicoBobineira"].ToString() != "0")
                sqlQuery += " AND c.chk_bercoMetalicoBobineira = " + HttpContext.Current.Session["bercoMetalicoBobineira"];

            if (HttpContext.Current.Session["cargaSecaGraneleiro"].ToString() != "0")
                sqlQuery += " AND c.chk_cargaSecaGraneleiro = " + HttpContext.Current.Session["cargaSecaGraneleiro"];

            sqlQuery += " ORDER BY c.data_checklist DESC";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "rel_peso_escoado");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["rel_peso_escoado"];
        }

        public DataTable dtsBloquados()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            dadosRelatorios dadosRelatorio = new dadosRelatorios();

            string sqlQuery = @"SELECT
	                            c.placa AS Placa,
	                            c.data_checklist AS DataVistoria,
	                            e.nome_fantasia AS Transportadora,
	                            cond.nome_condutor AS Condutor,
	                            CASE 
		                            WHEN (DATEDIFF(MINUTE, c.data_checklist, GETDATE()) - 1440) % 60 > 0 THEN CONVERT(VARCHAR, (DATEDIFF(MINUTE, c.data_checklist, GETDATE()) - 1440) / 60) + ' Horas e ' + CONVERT(VARCHAR, (DATEDIFF(MINUTE, c.data_checklist, GETDATE()) - 1440) % 60) + ' Minutos'
		                            ELSE CONVERT(VARCHAR, (DATEDIFF(MINUTE, c.data_checklist, GETDATE()) - 1440) / 60) + ' Horas' 
	                            END AS HorasExpirads,
	                            u.nome_usuario AS Vistoriador
                            FROM checklist c
                            INNER JOIN condutor cond ON c.cpf_condutor = cond.cpf_condutor
                            LEFT JOIN Empresa e ON e.id_empresa = c.id_emp_transportadora
                            LEFT JOIN usuario u ON u.id_usuario = c.id_usuario
                            WHERE c.veiculo_liberado = 1
                            AND DATEDIFF(MINUTE, c.data_checklist, GETDATE()) > 1440 
                            AND (SELECT MAX(cf.id_fase) from checklist_fase cf WHERE cf.id_checklist = c.id_checklist) = 1
                            AND c.data_checklist BETWEEN '" + Convert.ToDateTime(HttpContext.Current.Session["dtIni"], culture).ToString("s") + "' AND '" + Convert.ToDateTime(HttpContext.Current.Session["dtFim"], culture).AddHours(23).AddMinutes(59).AddSeconds(59).ToString("s") + "' ";


            //Tipo Veículo
            if (Convert.ToInt32(HttpContext.Current.Session["tipoVeiculo"]) > 0)
            {
                sqlQuery += " AND c.id_tipo_veiculo = " + HttpContext.Current.Session["tipoVeiculo"];
            }

            //Tipo Carroceria
            if (Convert.ToInt32(HttpContext.Current.Session["tipoCarroceria"]) > 0)
            {
                sqlQuery += " AND c.id_tipo_carroceria = " + HttpContext.Current.Session["tipoCarroceria"];
            }

            //Placa
            if (!string.IsNullOrWhiteSpace(HttpContext.Current.Session["placa"].ToString()))
            {
                sqlQuery += " AND c.placa = '" + HttpContext.Current.Session["placa"].ToString().Replace("-", "") + "' ";
            }

            //Frota/Agregado/Particular
            if (Convert.ToInt32(HttpContext.Current.Session["tipoVinculo"]) > 0)
            {
                sqlQuery += " AND c.id_tipo_transporte = " + HttpContext.Current.Session["tipoVinculo"];
            }

            //Carga/Descarga            
            if (Convert.ToInt32(HttpContext.Current.Session["cargaDescarga"]) > 0)
            {
                sqlQuery += " AND c.chk_cargaDescarga = " + HttpContext.Current.Session["cargaDescarga"];
            }


            //Somente Usuários Que são da Transportadora
            Int32 id_usuario = Convert.ToInt32(HttpContext.Current.Session["id"]);
            var usuario = _dbContext.usuario.Where(r => r.id_usuario == id_usuario).ToList()[0];

            if (usuario.id_tipo_usuario == 3)
            {
                sqlQuery += " AND c.id_emp_transportadora = " + usuario.id_transportadora;
            }
            else
            {
                if (Convert.ToInt32(HttpContext.Current.Session["transportadora"]) > 0)
                {
                    sqlQuery += " AND c.id_emp_transportadora = " + HttpContext.Current.Session["transportadora"];
                }
            }

            sqlQuery += " ORDER BY c.data_checklist DESC";

            //
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 360;
            da.Fill(dadosRelatorio, "veiculos_bloqueados");
            conn.Close();
            conn.Dispose();

            return dadosRelatorio.Tables["veiculos_bloqueados"];
        }
    }
}