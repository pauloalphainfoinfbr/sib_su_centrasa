using System;
using System.Collections.Generic;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using SIB.Data;
using WebMatrix.WebData;
using System.Text.RegularExpressions;
using System.Web.Routing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Data.SqlClient;
using CrystalDecisions.ReportSource;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.Design;

namespace SIB.Web.Controllers
{
    [Authorize]
    public class BuscaVeiculoController : BaseController
    {
        UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

        public ActionResult Index()
        {
            if (Session["fromButton"] != null)
            {
                Session["fromButton"] = null;

                return View();                
            }
            else
            {
                return RedirectToAction("Login", "Account");                        
            }
        }

        [HttpPost]
        public ActionResult Index(Models.BuscaPlaca busca)
        {
            string placa = "";

            busca.placa = busca.tipoPlaca == "brasil" ? busca.placa : busca.placaMercosul;

            if (busca.placa == null)
            {
                ModelState.AddModelError("", "Informe a placa do veículo. Ex: XXX-9999 ou XXX9999");
                return View("Index", busca);
            }
            else
            {                
                if (busca.placa.Length != 7 && busca.placa.Length != 8)
                {
                    ModelState.AddModelError("", "Placa teve conter o formato XXX9999 ou XXX-9999.");
                    return View("Index", busca);
                }
                else
                {
                    string[] array = new string[7];                    
                    
                    int numero;
                    string verifica = "^[0-9]";

                    if (busca.placa.Length == 8)
                    {
                        string traco = busca.placa.Substring(3, 1);

                        if (traco == "-")
                        {
                            placa = busca.placa.Replace(traco, "").ToUpper();
                        }
                        else
                        {
                            ModelState.AddModelError("", "Placa teve conter o formato XXX9999 ou XXX-9999.");
                            return View("Index", busca);
                        }
                    }
                    else
                    {
                        placa = busca.placa.ToUpper();
                    }

                    for (int i = 0; i < placa.Length; i++)
                    {
                        array[i] = placa.Substring(i, 1);
                    }
                    
                    if (Regex.IsMatch(array[0], verifica) || Regex.IsMatch(array[1], verifica) ||
                        Regex.IsMatch(array[2], verifica) || !int.TryParse(array[3], out numero) ||
                        //!int.TryParse(array[4], out numero) || 
                        !int.TryParse(array[5], out numero) || 
                        !int.TryParse(array[6], out numero)                        
                       )
                    {
                        ModelState.AddModelError("", " - Placa teve conter o formato XXX9999 ou XXX-9999.");
                    }                                               
                                                       
                }
            }

            if(ModelState.IsValid)
            {
                Session["fromButton"] = 1;

                if (busca != null)
                {
                    int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                    string sessionID = Session.SessionID;

                    if (!string.IsNullOrWhiteSpace(placa))
                    {
                        //Verifica se checklist expirou
                        string strSql = @"SELECT 
                                            DATEDIFF(hh, c.data_checklist, GETDATE()) 
                                        FROM checklist c 
                                        WHERE c.placa = '" + placa + "' " +
                                        @"AND c.veiculo_liberado = 1 
                                        AND c.data_checklist = (SELECT MAX(d.data_checklist) FROM checklist d WHERE d.placa = '" + placa + "') ";

                        string strExpirado = @"SELECT 
                                            *
                                        FROM checklist c 
                                        WHERE c.placa = '" + placa + "' " +
                                        @"AND c.veiculo_liberado = 1 
                                        AND c.data_checklist = (SELECT MAX(d.data_checklist) FROM checklist d WHERE d.placa = '" + placa + "') ";

                        int tempoChecklist = 0;//_dbContext.Database.SqlQuery<int>(strSql).ToList().Count > 0 ? _dbContext.Database.SqlQuery<int>(strSql).ToList()[0] : 0;
                        checklist checkExpirado = null;

                        if (tempoChecklist > 48)
                            checkExpirado = _dbContext.checklist.SqlQuery(strExpirado).First();
                            

                        bool veiculoAprovado = true;

                        if (_dbContext.checklist.Where(r => r.placa == placa).ToList().Count > 0)
                        {
                            DateTime dataUltimoCheck = _dbContext.checklist.Where(r => r.placa == placa).Max(r => r.data_checklist).Value;


                            string strAprovado = @"SELECT 
                                                    veiculo_liberado 
                                                   FROM checklist 
                                                   WHERE placa = '" + placa + "' " +
                                                   @"AND veiculo_liberado = 2 
                                                   AND CONVERT(VARCHAR(10), data_checklist, 103) + ' '  + CONVERT(VARCHAR(8), data_checklist, 14) = '" + dataUltimoCheck + "' ";

                            veiculoAprovado = _dbContext.Database.SqlQuery<int>(strAprovado).ToList().Count > 0 ? false : true;
                        }


                        //Verifica se existe um Checklist aberto para a placa

                        //Removido a regra de expirado e bloqueado
                        //string strChkList = "SELECT * FROM checklist c WHERE c.placa = '" + placa + "' AND (select max(id_fase) from checklist_fase cf where cf.id_checklist = c.id_checklist) < 2 and c.veiculo_liberado = 1 and c.data_fim_peacao is null AND DATEDIFF(hh, c.data_checklist, GETDATE()) < 49 ORDER BY c.data_checklist DESC";
                        string strChkList = "SELECT * FROM checklist c WHERE c.placa = '" + placa + "' AND (select max(id_fase) from checklist_fase cf where cf.id_checklist = c.id_checklist) < 2 and c.veiculo_liberado = 1 and c.data_fim_peacao is null ORDER BY c.data_checklist DESC";

                        var chkList = _dbContext.checklist.SqlQuery(strChkList).ToList();

                        //var chkList = _dbContext.checklist.Where(c => c.placa == placa && c.veiculo_liberado == true && c.data_registro_saida == null && tempoChecklist < 6).OrderByDescending(r => r.data_checklist).ToList();

                        Multiton Checklist = Multiton.GetInstance(sessionID);
                        //Multiton Checklist = Multiton.GetInstance(id_user);

                        //Se existe CheckList aberto, carrega os dados e inicia
                        if (chkList.Count > 0)
                        {
                            Checklist.check = chkList[0];

                            //Verifica se o checklist já parou em alguma fase
                            var listaFase = (
                                                from
                                                    fase
                                                in
                                                    Checklist.check.checklist_fase
                                                select new { fase.id_fase, fase.id_checklist }
                                                ).Where(r => r.id_checklist == Checklist.check.id_checklist).ToList();

                            //Verifica se já existe alguma fase, se existir, check_fase recebe a ultima fase em que o checklist parou
                            int check_fase = listaFase.Count > 0 ? listaFase.Max(r => r.id_fase) : 0;


                            //Se check_fase > 0 existe a fase.
                            if (check_fase > 0)
                            {
                                //if (check_fase == 1 && Checklist.check.chk_entregaAlavanca == null)
                                //    return RedirectToAction("Index", "EntregaAlavanca");
                                if (check_fase == 1 && Checklist.check.data_inicio_peacao == null)
                                    return RedirectToAction("Index", "RegistroInicioPeacao");
                                else if (check_fase == 1 && Checklist.check.data_inicio_peacao != null)
                                    return RedirectToAction("Index", "VeiculoPossui");
                            }
                        }

                        //Se não existe CheckList então cria um novo CheckList atribuindo os valores do formulário
                        Data.checklist chk = new Data.checklist();
                        chk.data_checklist = DateTime.Now;
                        chk.placa = busca.placa.ToUpper();
                        chk.veiculo_liberado = 1;

                        Checklist.check = chk;
                        Checklist.check.id_usuario = id_user;                                                

                        //Verifica se já existe o veículo com a placa procurada 
                        if (_dbContext.veiculo.Where(v => v.placa == placa).ToList().Count > 0)
                        {
                            Data.veiculo veiculo = new veiculo();
                            veiculo.ano = _dbContext.veiculo.Where(v => v.placa == placa).ToList()[0].ano;                            
                            veiculo.placa = busca.placa.ToUpper();

                            //Veiculo existe
                            Checklist.check.veiculo = veiculo;
                        }
                        else
                        {
                            //Veiculo não existe
                            Data.veiculo veic = new Data.veiculo();
                            veic.placa = placa.ToUpper();
                            Checklist.check.veiculo = veic;
                        }

                        if (tempoChecklist > 48)
                        {
                            if (checkExpirado.checklist_fase.Max(c => c.id_fase) == 1) {
                                if (checkExpirado.chk_bloqueado == null || checkExpirado.chk_bloqueado == 1) {
                                    if (checkExpirado.chk_bloqueado == null) {
                                        var updatedCheck = checkExpirado;
                                        updatedCheck.chk_bloqueado = 1;

                                        //Atualiza checklist                                         
                                        var entityKeyCheckList = _dbContext.checklist.Create().GetType().GetProperty("id_checklist").GetValue(checkExpirado);
                                        _dbContext.Entry(_dbContext.Set<Data.checklist>().Find(entityKeyCheckList)).CurrentValues.SetValues(updatedCheck);

                                        _dbContext.SaveChanges();
                                    }

                                    ModelState.AddModelError("", "Este veículo não finalizou a vistoria dentro de 24 horas e foi bloqueado.");

                                    return View("Index", busca);
                                }
                            }
                        }

                        if (tempoChecklist > 48 && checkExpirado.checklist_fase.Max(c => c.id_fase) == 1) {
                            if (!veiculoAprovado)
                                return RedirectToAction("Index", "DadosVeiculo", new { expirado = 1, reprovado = 1 });
                            else
                                return RedirectToAction("Index", "DadosVeiculo", new { expirado = 1 });
                        }
                        else {
                            if (!veiculoAprovado)
                                return RedirectToAction("Index", "DadosVeiculo", new { reprovado = 1 });
                            else
                                return RedirectToAction("Index", "DadosVeiculo");
                        }
                    }
                }
            }                        
             
            return View("Index", busca);                        
        }

        public ActionResult geraRelatorioImpressao()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnString"].ToString());
            conn.Open();

            System.Data.DataTable dtSource = new System.Data.DataTable();

            Int32 id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            Int32 idChecklist = _dbContext.checklist.Where(c => c.id_usuario == id_user).Max(c => c.id_checklist);

            string sqlQuery = "SELECT " +
                                "chk.data_checklist AS DataInicio, " +
                                "chk.data_fim_checklist AS DataFim, " +
                                "chk.data_inicio_peacao AS InicioPeacao, " +
                                "chk.data_fim_peacao AS FimPeacao, " +
                                "chk.chk_DT, " +
                                "chk.tonelada, " +
                                "tv.desc_tipo_veiculo AS TipoVeiculo, " +
                                "chk.chk_numeroEntregas AS QtdeEntregas, " +
                                "chk.placa, " +
                                "tt.desc_tipo_transporte AS TipoTransporte, " +
                                "chk.chk_placa2, " +
                                "chk.chk_placa3, " +
                                "tf.desc_tipo_frete, " +
                                "CASE chk.chk_cargaDescarga WHEN 1 THEN 'Carga' WHEN 2 THEN 'Descarga' WHEN 3 THEN 'Troca de Notas' ELSE '' END AS CargaDescarga, " +
                                "mv.desc_marca_veiculo AS Marca, " +
                                "veic.modelo, " +
                                "cv.desc_cor_veiculo AS CorVeiculo, " +
                                "CASE WHEN veic.ano = 0 THEN 'Anterior a 1985' " +
                                     "WHEN veic.ano BETWEEN 1986 and 1990 THEN '1986 a 1990' " +
                                     "WHEN veic.ano BETWEEN 1991 and 1995 THEN '1991 a 1995' " +
                                     "WHEN veic.ano BETWEEN 1996 and 2000 THEN '1996 a 2000' " +
                                     "WHEN veic.ano BETWEEN 2001 and 2005 THEN '2001 a 2005' " +
                                     "WHEN veic.ano BETWEEN 2006 and 2010 THEN '2006 a 2010' " +
                                     "WHEN veic.ano BETWEEN 2011 and 2015 THEN '2011 a 2015' " +
                                     "WHEN veic.ano BETWEEN 2016 and 2020 THEN '2016 a 2020'  " +
                                "END as Ano, " +
                                "CASE chk.chk_usinaDeposito WHEN 1 THEN 'Usina' WHEN 2 THEN 'Depósito' END AS UsinaDeposito, " +
                                "'Não' AS LiberadoRestricao, " +
                                "cond.nome_condutor AS Condutor, " +
                                "chk.cpf_condutor, " +
                                "tc.desc_tipo_carroceria AS TipoCarroceria, " +
                                "CASE chk.chk_carroceria WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS Carroceria, " +
                                "CASE chk.chk_epi WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS EPI, " +
                                "trans.nome_fantasia AS Transportadora, " +
                                "(select usu.nome_usuario from checklist_fase cf inner join usuario usu on usu.id_usuario = cf.id_usuario where cf.id_fase = 1 and cf.id_checklist = chk.id_checklist) AS VistoriadorInicial, " +
                                "(select usu.nome_usuario from checklist_fase cf inner join usuario usu on usu.id_usuario = cf.id_usuario where cf.id_fase = 2 and cf.id_checklist = chk.id_checklist) AS VistoriadorFinal, " +
                                "CASE chk.chk_capaceteCarnJugular WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS CapaceteCarnJugular, " +
                                "CASE chk.chk_oculosSeguranca WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS OculosSeguranca, " +
                                "CASE chk.chk_protetorAuditivo WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS ProtetorAuditivo, " +
                                "CASE chk.chk_perneira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS Perneira, " +
                                "CASE chk.chk_botina WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS Botina, " +
                                "CASE chk.chk_parabrisaTrincado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS ParabrisaTrincado,	 " +
                                "CASE chk_trincaTolerancia WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS TrincaTolerancia, " +
                                "CASE chk.chk_retrovisorTrincado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS RetrovisorTrincado,	 " +
                                "CASE chk_trincaRetrovisorTolerancia WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS TrincaRetrovisorTolerancia, " +
                                "CASE chk.chk_assoalho WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS Assoalho, " +
                                "CASE chk.chk_situacaoAssoalho WHEN 1 THEN 'Limpo' WHEN 2 THEN 'Sujo' END AS EstadoAssoalho, " +
                                "CASE chk.chk_pneusEstepe WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS PneusEstepe, " +
                                "CASE chk.chk_faroisLanterna WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS FaroisLanterna, " +
                                "CASE chk.chk_necesMontarBerco WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS MontarBerco, " +
                                "CASE chk_possui WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS PossuiBercoMetalico, " +
                                "CASE chk_liberado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS BercoMetalicoLiberado, " +
                                "CASE chk_metalico WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS Metalico, " +
                                "CASE chk_bercoProprio WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS BercoProrio, " +
                                "CASE chk_bobineira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS Bobineira, " +
                                "CASE chk.chk_tacografo WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS Tacografo, " +
                                "CASE chk_extintor WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS Extintor, " +
                                "CASE chk_chassi WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS Chassi, " +
                                "CASE chk_triangMacaco WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS TrianguloMacaco, " +
                                "CASE chk_lona WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' ELSE '' END AS Lona, " +
                                "CASE chk.chk_materialMolhado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END MaterialMolhado, " +
                                "CASE chk.chk_retornouUsina WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END RetornouUsina, " +
                                "REPLACE(chk.chk_material, '/', ' | ') AS TipoMaterial, " +
                                "REPLACE(chk.chk_numLote, '/', ' / ') AS NumeroLote, " +
                                "UPPER(chk.chk_cliente) AS Cliente, " +
                                "UPPER(chk.chk_observacao) AS Observacao, " +
                                "CASE chk.chk_amarracaoCarreta WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS AmarracaoCarreta, " +
                                "CASE chk.chk_condicoes_embalagem_material WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS CondicoesEmbalagemMaterial, " +
                                "CASE chk.chk_cintas WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' ELSE '' END AS Cintas, " +
                                "CASE chk.chk_devidamentePregado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS DevidamentePregado, " +
                                "CASE chk.chk_borrachas_assoalho_carga WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS BorrachasAssoalhoCarga, " +
                                "CASE chk.veiculo_liberado WHEN 1 THEN 'Aprovado' WHEN 2 THEN 'Reprovado' ELSE '' END AS StatusInspecao, " +
                                "CASE chk_cordasCintasCatracas WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' END AS CordasCintasCatracas, " +
                                "CASE chk_alturaVeiculo WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS AlturaVeiculo, " +
                                "CASE chk_parafusoRodas WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS TodosParafusosRodas, " +
                                "CASE chk_drenoBobineira WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS DrenoBobineira, " +
                                "CASE chk_setas WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS Setas, " +
                                "CASE chk_sinalSonoroRe WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS SinalSonoroRe, " +
                                "CASE chk_dobradicasBoasCondicoes WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS DobradicasBoasCondicoes, " +
                                "CASE chk_dormentesFixados WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS DormentesFixados, " +
                                "CASE chk_todosPinos WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS TodosPinos, " +
                                "CASE chk_laudoGanchoAdaptado WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' ELSE '' END AS LaudoGanchoAdaptado, " +
                                "CASE chk_estruturaTampasFerroMadeira WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' WHEN 3 THEN 'NA' ELSE '' END AS TampasFerroMadeira, " +
                                "CASE chk_avariaMaterial WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS AvariaMaterial, " +
                                "CASE chk_tmpGuardaLatTravad WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS TmpGuardaLatTrav, " +
                                "CASE chk_materialChovendo WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' ELSE '' END AS Chovendo, " +
                                "CASE chk_bercoMetalicoBobineira WHEN 1 THEN 'Berço Metálico' WHEN 2 THEN 'Bobineira' WHEN 3 THEN 'NA' END AS BercoMetalicoBobineira, " +
                                "CASE chk_cargaSecaGraneleiro WHEN 1 THEN 'Carga Seca' WHEN 2 THEN 'Graneleiro' END AS CargaSecaGraneleiro, " +
                                "CASE chk_catracaoTodaAmarracao WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS CatracaTodaAmarracao, " +
                                "CASE chk_precisouMovimentarTampas WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS PrecisouMovimentarTampas, " +
                                "chk_medidorFumacaNegra AS FumacaNegra, " +
                                "ta.desc_tipo_assoalho AS TipoAssoalho, " +
                                "CASE chk.chk_mangote WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS Mangote, " +
                                "CASE chk.chk_possuiLaudoOpacidade WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'NA' WHEN 4 THEN 'Bom' WHEN 5 THEN 'Ruim' END AS PossuiLaudoOpacidade, " +
                                "CASE chk.chk_entregaAlavanca WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' WHEN 3 THEN 'Catracão' END AS AlavancaEntregue, " +
                                "CASE chk.chk_devolucaoAlavanca WHEN 1 THEN 'Sim' WHEN 2 THEN 'Não' END AS AlavancaDevolvida, " +
                                "CONVERT(VARCHAR, chk.data_realizacao_laudoOpacidade, 103) AS DataRealizacaoLaudo, " +
                                "REPLACE(dbo.fn_ListaMotivosReprovacaoUsiminasIP(chk.id_checklist), '<BR>', ' | ') AS Motivos " +
                            "FROM checklist chk " +
                            "LEFT JOIN tipo_veiculo tv ON tv.id_tipo_veiculo = chk.id_tipo_veiculo " +
                            "LEFT JOIN tipo_carroceria tc ON tc.id_tipo_carroceria = chk.id_tipo_carroceria " +
                            "LEFT JOIN tipo_assoalho ta ON ta.id_tipo_assoalho = chk.id_tipo_assoalho " +
                            "LEFT JOIN tipo_transporte tt ON tt.id_tipo_transporte = chk.id_tipo_transporte " +
                            "LEFT JOIN tipo_frete tf ON tf.id_tipo_frete = chk.id_tipo_frete " +
                            "INNER JOIN veiculo veic ON veic.placa = chk.placa " +
                            "LEFT JOIN marca_veiculo mv ON mv.id_marca_veiculo = veic.id_marca_veiculo " +
                            "LEFT JOIN cor_veiculo cv ON cv.id_cor_veiculo = veic.id_cor_veiculo " +
                            "INNER JOIN condutor cond ON cond.cpf_condutor = chk.cpf_condutor " +
                            "LEFT JOIN empresa trans ON trans.id_empresa = chk.id_emp_transportadora " +
                            "WHERE chk.id_checklist = " + idChecklist;

            sqlQuery +=
                            " ORDER BY " +
                                "chk.data_checklist DESC ";


            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.SelectCommand.CommandTimeout = 180;
            da.Fill(dtSource);

            ReportClass rsRelacaoChecklist = new ReportClass();
            rsRelacaoChecklist.FileName = Server.MapPath("~/Relatorios/rptChecklistImpressao.rpt");
            rsRelacaoChecklist.Load(Server.MapPath("~/Relatorios/rptChecklistImpressao.rpt"));
            rsRelacaoChecklist.SetDataSource(dtSource);


            System.IO.Stream stream = rsRelacaoChecklist.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            return new FileStreamResult(stream, "application/pdf");
        }

    }
}
