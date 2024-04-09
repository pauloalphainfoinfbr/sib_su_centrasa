using System;
using System.Collections.Generic;
using WebMatrix.WebData;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using SIB.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using System.Text;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.Design;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;


namespace SIB.Web.Controllers
{
    [Authorize]
    public class VeiculoLiberadoController : BaseController
    {
        [WebMethod]        
        public void liberarVeiculo(string url)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);
            
        }

        public ActionResult Index(Models.Empresa.VeiculoLiberado dadosVeiculoLiberado)
        {
            if (Session["fromButton"] != null)
            {
                Session["fromButton"] = null;

                int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                string sessionID = Session.SessionID;
                Multiton multiCheck = Multiton.GetInstance(sessionID);                

                    //Verifica se existe algum item com valor de reprovação, se sim atribui true ou false para veiculo_liberado
                    //Reprovação por ítens
                    if (multiCheck.check.chk_alturaVeiculo == 2 || multiCheck.check.chk_epi == 2 ||
                        multiCheck.check.chk_capaceteCarnJugular == 2 || multiCheck.check.chk_oculosSeguranca == 2 ||
                        multiCheck.check.chk_protetorAuditivo == 2 || multiCheck.check.chk_perneira == 2 ||
                        multiCheck.check.chk_botina == 2 || multiCheck.check.chk_mangote == 2 || multiCheck.check.chk_parafusoRodas == 2 ||
                        multiCheck.check.chk_dormentesFixados == 2 || multiCheck.check.chk_todosPinos == 2 ||
                        multiCheck.check.chk_dobradicasBoasCondicoes == 2 || multiCheck.check.chk_faroisLanterna == 2 ||
                        multiCheck.check.chk_setas == 2 || multiCheck.check.chk_sinalSonoroRe == 2 ||
                        (multiCheck.check.chk_parabrisaTrincado == 1 && multiCheck.check.chk_trincaTolerancia == 2) || 
                        //multiCheck.check.chk_devidamentePregado == 2 || 
                        multiCheck.check.chk_borrachas_assoalho_carga == 2 || 
                        multiCheck.check.chk_liberado == 2 ||                        
                        multiCheck.check.chk_tacografo == 5 || multiCheck.check.chk_chassi == 5 || multiCheck.check.chk_condicoesBobineira == 5 ||
                        multiCheck.check.chk_extintor == 5 || multiCheck.check.chk_triangMacaco == 5 ||
                        multiCheck.check.chk_carroceria == 5 || multiCheck.check.chk_pneusEstepe == 5 ||
                        multiCheck.check.chk_luzFreio == 5 || multiCheck.check.chk_cordasCintasCatracas == 5 ||                        
                        multiCheck.check.chk_estruturaTampasFerroMadeira == 5 || 
                        multiCheck.check.chk_amarracaoCarreta == 5 ||
                        multiCheck.check.chk_condicoes_embalagem_material == 5 ||
                        multiCheck.check.chk_lona == 5 || multiCheck.check.chk_cintas == 5 || multiCheck.check.chk_assoalho == 5 || multiCheck.check.chk_situacaoAssoalho == 2  || multiCheck.check.chk_drenoBobineira == 2 ||
                        multiCheck.check.chk_possuiLaudoOpacidade == 2 || multiCheck.check.chk_placaLegivel == 2 || multiCheck.check.chk_estadoGeral == 5 || multiCheck.check.chk_vazamentoOleo == 1 || multiCheck.check.chk_ganchoPatola == 2 ||
                        multiCheck.check.chk_drenosDesobstruidos == 2 || multiCheck.check.chk_borrachaBobineiraInterica == 2 || multiCheck.check.chk_borrachaFixaBobineira == 2 ||
                        (multiCheck.check.chk_retrovisorTrincado == 1 && multiCheck.check.chk_trincaRetrovisorTolerancia == 2 || multiCheck.check.chk_fotosCondicoesBobineira == 2 || multiCheck.check.chk_travaSegurancaTampas == 2)
                       )
                    {
                        multiCheck.check.veiculo_liberado = 2;
                    }
                    else
                    {
                        multiCheck.check.veiculo_liberado = 1;
                    }
                //
                    
                dadosVeiculoLiberado.veiculoLiberado = multiCheck.check.veiculo_liberado == 1 ? "Sim" : "Não";
                dadosVeiculoLiberado.motivos = motivosReprovacao();

                return View(dadosVeiculoLiberado);
            }
            else
            {
                return RedirectToAction("Login", "Account"); 
            }
        }

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Salvar(Models.Empresa.VeiculoLiberado dadosVeiculoLiberado)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            //Formata placa removendo o "-" e colocando em caixa alta. ex: HHH9999
            if(multiCheck.check.chk_placa2 != null)
                multiCheck.check.chk_placa2 = multiCheck.check.chk_placa2.Replace("-", "").ToUpper();

            if(multiCheck.check.chk_placa3 != null)
                multiCheck.check.chk_placa3 = multiCheck.check.chk_placa3.Replace("-", "").ToUpper();

            if (multiCheck.check.id_checklist == 0)
            {
                //Verifica se veiculo é recuperado
                string sqlQuery = "SELECT " +
                                    "c.id_checklist " +
                                "FROM " +
                                    "checklist c " +
                                "WHERE " +
                                    "c.placa = '" + multiCheck.check.placa + "' " +
                                "AND " +
                                    "c.veiculo_liberado = 2 " +
                                "AND " +
                                    "c.recuperado IS NULL " +
                                "ORDER BY " +
                                    "c.id_checklist DESC ";

                int idCheckVeiculoRecusado = _dbContext.Database.SqlQuery<int>(sqlQuery).ToList().Count > 0 ? _dbContext.Database.SqlQuery<int>(sqlQuery).ToList()[0] : 0;

                if (idCheckVeiculoRecusado > 0)
                {
                    if (multiCheck.check.veiculo_liberado == 1)
                    {
                        //Atualiza checklist anterior em que o veiculo foi recusado, e atribui como recuperado                  
                        checklist objChecklist = _dbContext.checklist.Where(r => r.id_checklist == idCheckVeiculoRecusado).ToList()[0];
                        objChecklist.recuperado = true;

                        var check_anterior = _dbContext.checklist.Where(r => r.id_checklist == idCheckVeiculoRecusado).ToList();
                        var entityKeyCheckList = _dbContext.checklist.Create().GetType().GetProperty("id_checklist").GetValue(check_anterior[0]);
                        _dbContext.Entry(_dbContext.Set<Data.checklist>().Find(entityKeyCheckList)).CurrentValues.SetValues(objChecklist);
                    }
                    else
                    {
                        //Atualiza checklist anterior em que o veiculo foi recusado, e atribui como recuperado                  
                        checklist objChecklist = _dbContext.checklist.Where(r => r.id_checklist == idCheckVeiculoRecusado).ToList()[0];                        

                        var check_anterior = _dbContext.checklist.Where(r => r.id_checklist == idCheckVeiculoRecusado).ToList();
                        var entityKeyCheckList = _dbContext.checklist.Create().GetType().GetProperty("id_checklist").GetValue(check_anterior[0]);
                        _dbContext.Entry(_dbContext.Set<Data.checklist>().Find(entityKeyCheckList)).CurrentValues.SetValues(objChecklist);
                    }
                }                

                multiCheck.check.data_fim_checklist = DateTime.Now;

                Data.checklist_fase check_fase = new Data.checklist_fase();                
                check_fase.id_fase = 1;
                check_fase.data_inicio_fase = multiCheck.check.data_checklist;
                check_fase.data_final_fase = multiCheck.check.data_fim_checklist;
                check_fase.id_usuario = ((SIB.Data.usuario)Session["usuario"]).id_usuario;

                multiCheck.check.checklist_fase.Add(check_fase);                

                Data.veiculo auxVeiculo = null;

                //Verifica se já existe Condutor com o CPF Informado, se existir atualiza, se não, insere novo
                if (_dbContext.condutor.Where(r => r.cpf_condutor == multiCheck.check.cpf_condutor).ToList().Count > 0)
                {                    
                    //Atualiza Condutor
                    var cond_anterior = _dbContext.condutor.Where(r => r.cpf_condutor == multiCheck.check.cpf_condutor).ToList();
                    var entityKeyCondutor = _dbContext.condutor.Create().GetType().GetProperty("cpf_condutor").GetValue(cond_anterior[0]);
                    _dbContext.Entry(_dbContext.Set<Data.condutor>().Find(entityKeyCondutor)).CurrentValues.SetValues(multiCheck.check.condutor);

                    //Apaga condutor da tabela Checklist para não salvar duplicado
                    multiCheck.check.condutor = null;
                }

                if (_dbContext.veiculo.Where(r => r.placa == multiCheck.check.veiculo.placa).ToList().Count > 0)
                {                    
                    multiCheck.check.veiculo = null;                    
                }                

                //Adiciona o novo checklist
                _dbContext.checklist.Add(multiCheck.check);                    

                //Salva as alterações no banco
                _dbContext.SaveChanges();
               
            }
            else
            {                
                var Check = _dbContext.checklist.Where(r => r.placa == multiCheck.check.placa && r.veiculo_liberado == 1).OrderByDescending(r => r.data_checklist).ToList();
                                    
                //Salva fase 2
                Data.checklist_fase check_fase = new Data.checklist_fase();
                check_fase.id_checklist = multiCheck.check.id_checklist;
                check_fase.id_fase = 2;                    
                check_fase.data_inicio_fase = DateTime.Now;                    
                check_fase.id_usuario = ((SIB.Data.usuario)Session["usuario"]).id_usuario;

                //multiCheck.check.checklist_fase.Add(check_fase); 
                _dbContext.checklist_fase.Add(check_fase);                    

                //Atualiza Checklist
                var check_anterior = _dbContext.checklist.Where(r => r.id_checklist == multiCheck.check.id_checklist).ToList();
                var entityKeyCheckList = _dbContext.checklist.Create().GetType().GetProperty("id_checklist").GetValue(check_anterior[0]);
                _dbContext.Entry(_dbContext.Set<Data.checklist>().Find(entityKeyCheckList)).CurrentValues.SetValues(multiCheck.check);

                //Salva as alterações no banco
                _dbContext.SaveChanges();
            }

            GerenciaLogin objGerLogin = new GerenciaLogin();
            objGerLogin.deslogaUsuarioApp(Session.SessionID, this.HttpContext);   

            WebSecurity.Logout();
            Session.Clear();

            return RedirectToAction("Login", "Account", new { sucesso = 1 });                        
        }        

        [ActionName("Index")]
        [Models.Empresa.Submit("Voltar")]
        public ActionResult Voltar()
        {
            Session["fromButton"] = 1;

            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);
            
            return RedirectToAction("Index", "BercoMetalico");
        }

        public string motivosReprovacao()
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            string motivos = "";

            if (multiCheck.check.chk_alturaVeiculo == 2) motivos += "# Altura do veículo não permite a entrada no galpão \n";
            if (multiCheck.check.chk_epi == 2) motivos += "# Não possui EPI \n";
            if (multiCheck.check.chk_capaceteCarnJugular == 2) motivos += "# Capacete com carneira e jugular \n";
            if (multiCheck.check.chk_oculosSeguranca == 2) motivos += "# Óculos de Segurança \n";
            if (multiCheck.check.chk_protetorAuditivo == 2) motivos += "# Protetor Auditivo \n";
            if (multiCheck.check.chk_perneira == 2) motivos += "# Perneira \n";
            if (multiCheck.check.chk_botina == 2) motivos += "# Botina \n";
            if (multiCheck.check.chk_parafusoRodas == 2) motivos += "# Possui todos os parafusos das Rodas \n";
            if (multiCheck.check.chk_dormentesFixados == 2) motivos += "# Possui ganchos adaptados \n";
            if (multiCheck.check.chk_todosPinos == 2) motivos += "# Todos os Pinos \n";
            if (multiCheck.check.chk_dobradicasBoasCondicoes == 2) motivos += "# Todas as Dobradiças em Boas Condições \n";
            if (multiCheck.check.chk_faroisLanterna == 2) motivos += "# Faróis e Lanternas Funcionando \n";
            if (multiCheck.check.chk_setas == 2) motivos += "# Setas Funcionando \n";
            if (multiCheck.check.chk_sinalSonoroRe == 2) motivos += "# Sinal de Ré (Luz e Sonoro) \n";
            if(multiCheck.check.chk_parabrisaTrincado == 1 && multiCheck.check.chk_trincaTolerancia == 2){
                motivos += "# Para-brisa Trincado \n";
            }
            if (multiCheck.check.chk_retrovisorTrincado == 1 && multiCheck.check.chk_trincaRetrovisorTolerancia == 2) {
                motivos += "# Retrovisor Trincado \n";
            }
            //if (multiCheck.check.chk_devidamentePregado == 2) motivos += "# Devidamente Pregado \n";
            if (multiCheck.check.chk_borrachas_assoalho_carga == 2) motivos += "# Borrachas no assoalho e carga posicionadas de forma correta \n";
            if (multiCheck.check.chk_liberado == 2) motivos += "# Berço Metalico Liberado\n";
            if (multiCheck.check.chk_tacografo == 5) motivos += "# Tacógrafo \n";
            if (multiCheck.check.chk_chassi == 5) motivos += "# Chassi \n";
            if (multiCheck.check.chk_extintor == 5) motivos += "# Extintor\n";
            if (multiCheck.check.chk_triangMacaco == 5) motivos += "# Triângulo/Macaco \n";
            if (multiCheck.check.chk_carroceria == 5) motivos += "# Carroceria \n";
            if (multiCheck.check.chk_pneusEstepe == 5) motivos += "# Pneus/Stepes \n";
            if (multiCheck.check.chk_luzFreio == 5) motivos += "# Luz de Freio \n";
            if (multiCheck.check.chk_cordasCintasCatracas == 5) motivos += "# Cordas/Cintas/Catracas \n";            
            if (multiCheck.check.chk_carroceriaLimpa == 5) motivos += "# Carroceria Limpa \n";
            if (multiCheck.check.chk_estruturaTampasFerroMadeira == 5) motivos += "# Estrutura das Tampas Ferro/Madeira \n";
            if (multiCheck.check.chk_amarracaoCarreta == 5) motivos += "# Amarração da Carreta \n";
            if (multiCheck.check.chk_condicoes_embalagem_material == 5) motivos += "# Condições da embalagem do material   \n";
            if (multiCheck.check.chk_lona == 5) motivos += "# Condições da Lona \n";
            if (multiCheck.check.chk_cintas == 5) motivos += "# Cintas \n";
            if (multiCheck.check.chk_assoalho == 5) motivos += "# Assoalho \n";
            if (multiCheck.check.chk_situacaoAssoalho == 2) motivos += "# Assoalho Sujo \n ";
            if (multiCheck.check.chk_drenoBobineira == 2) motivos += "# Dreno de Bobineira \n";
            if (multiCheck.check.chk_mangote == 2) motivos += "# Mangote \n";

            if (multiCheck.check.chk_drenosDesobstruidos == 2) motivos += "# Drenos desobstruídos \n";
            if (multiCheck.check.chk_borrachaBobineiraInterica == 2) motivos += "# Borracha sobre a bobineira é inteiriça \n";
            if (multiCheck.check.chk_borrachaFixaBobineira == 2) motivos += "# Borracha está fixa sobre a bobineira \n";
            if (multiCheck.check.chk_condicoesBobineira == 5) motivos += "# Condições da bobineira \n";
            if (multiCheck.check.chk_travaSegurancaTampas == 2) motivos += "# Trava de seg. tampas \n";

            return motivos;
        }

    }
}
