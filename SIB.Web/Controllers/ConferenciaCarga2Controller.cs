using System;
using System.Collections.Generic;
using SIB.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebMatrix.WebData;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using System.Web.Mvc;
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
    public class ConferenciaCarga2Controller : BaseController
    {
        public ActionResult Index(Models.Empresa.ConferenciaCarga2 dadosConferenciaCarga2)
        {
            if (Session["fromButton"] != null)
            {
                Session["fromButton"] = null;

                int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                string sessionID = Session.SessionID;
                Multiton multiCheck = Multiton.GetInstance(sessionID);

                dadosConferenciaCarga2.amarracaoCarreta = multiCheck.check.chk_amarracaoCarreta != null ? multiCheck.check.chk_amarracaoCarreta : 4;
                dadosConferenciaCarga2.condicoesEmbalagemMaterial = multiCheck.check.chk_condicoes_embalagem_material != null ? multiCheck.check.chk_condicoes_embalagem_material : 4;
                dadosConferenciaCarga2.cintas = multiCheck.check.chk_cintas != null ? multiCheck.check.chk_cintas : 4;
                //dadosConferenciaCarga2.devidamentePregado = multiCheck.check.chk_devidamentePregado != null ? multiCheck.check.chk_devidamentePregado : 1;
                dadosConferenciaCarga2.lona = multiCheck.check.chk_lona != null ? Convert.ToInt32(multiCheck.check.chk_lona) : 4;
                dadosConferenciaCarga2.veiculoLiberado = multiCheck.check.veiculo_liberado == 1 ? dadosConferenciaCarga2.veiculoLiberado = "Sim" : dadosConferenciaCarga2.veiculoLiberado = "Não";
                dadosConferenciaCarga2.borrachasAssoalhoCarga = multiCheck.check.chk_borrachas_assoalho_carga != null ? multiCheck.check.chk_borrachas_assoalho_carga : 1;

                dadosConferenciaCarga2.bercoMetalicoBobineira = multiCheck.check.chk_bercoMetalicoBobineira != null ? Convert.ToInt32(multiCheck.check.chk_bercoMetalicoBobineira) : 1;
                dadosConferenciaCarga2.cargaSecaGraneleiro = multiCheck.check.chk_cargaSecaGraneleiro != null ? Convert.ToInt32(multiCheck.check.chk_cargaSecaGraneleiro) : 1;
                dadosConferenciaCarga2.catracaoAmarracao = multiCheck.check.chk_catracaoTodaAmarracao != null ? Convert.ToInt32(multiCheck.check.chk_catracaoTodaAmarracao) : 1;
                dadosConferenciaCarga2.precisouMovimentarTampas = multiCheck.check.chk_precisouMovimentarTampas != null ? Convert.ToInt32(multiCheck.check.chk_precisouMovimentarTampas) : 1;
                dadosConferenciaCarga2.tipoLona = multiCheck.check.chk_tipoLona != null ? Convert.ToInt32(multiCheck.check.chk_tipoLona) : 1;
                //dadosConferenciaCarga2.qtdeCunha = multiCheck.check.chk_qtdeCunha != null ? multiCheck.check.chk_qtdeCunha.ToString() : "";

                return View(dadosConferenciaCarga2);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Proximo(Models.Empresa.ConferenciaCarga2 dadosConferencia2)
        {
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            if (dadosConferencia2.lona == 0 || dadosConferencia2.cintas == 0 ||
               dadosConferencia2.amarracaoCarreta == 0 ||
               dadosConferencia2.condicoesEmbalagemMaterial == 0 || 
               //dadosConferencia2.devidamentePregado == 0 || 
               dadosConferencia2.borrachasAssoalhoCarga == 0 || 
               dadosConferencia2.tipoLona == 0)
            {
                ModelState.AddModelError("", " - Todos os campos são obrigatórios.");
            }

            /*int value;
            if (!string.IsNullOrWhiteSpace(dadosConferencia2.qtdeCunha))
            {
                if (!int.TryParse(dadosConferencia2.qtdeCunha, out value))
                    ModelState.AddModelError("", " - Insira um número válido para a qtde. de cunha.");
            }*/

            if (ModelState.IsValid)
            {
                Session["fromButton"] = 1;

                multiCheck.check.chk_amarracaoCarreta = dadosConferencia2.amarracaoCarreta;
                multiCheck.check.chk_condicoes_embalagem_material = dadosConferencia2.condicoesEmbalagemMaterial;
                multiCheck.check.chk_cintas = dadosConferencia2.cintas;
                //multiCheck.check.chk_devidamentePregado = dadosConferencia2.devidamentePregado;
                multiCheck.check.chk_borrachas_assoalho_carga = dadosConferencia2.borrachasAssoalhoCarga;
                multiCheck.check.chk_lona = dadosConferencia2.lona;
                multiCheck.check.data_fim_peacao = DateTime.Now;

                //1 = Berço Metálico  2 = Bobineira
                multiCheck.check.chk_bercoMetalicoBobineira = dadosConferencia2.bercoMetalicoBobineira;

                //1 = Carga Seca  2 = Graneleiro
                multiCheck.check.chk_cargaSecaGraneleiro = dadosConferencia2.cargaSecaGraneleiro;

                //1 = Sim  2 = Não
                multiCheck.check.chk_catracaoTodaAmarracao = dadosConferencia2.catracaoAmarracao;

                //1 = Sim  2 = Não
                multiCheck.check.chk_precisouMovimentarTampas = dadosConferencia2.precisouMovimentarTampas;

                multiCheck.check.chk_tipoLona = dadosConferencia2.tipoLona;
                //multiCheck.check.chk_qtdeCunha = !string.IsNullOrWhiteSpace(dadosConferencia2.qtdeCunha) ? Convert.ToInt32(dadosConferencia2.qtdeCunha) : (int?)null;

                UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();

                Data.checklist_fase check_fase = new Data.checklist_fase();
                check_fase.id_checklist = multiCheck.check.id_checklist;
                check_fase.id_fase = 2;
                check_fase.data_inicio_fase = DateTime.Now;
                check_fase.data_final_fase = DateTime.Now;
                check_fase.id_usuario = ((SIB.Data.usuario)Session["usuario"]).id_usuario;

                _dbContext.checklist_fase.Add(check_fase);

                var checkAnterior = _dbContext.checklist.Where(r => r.id_checklist == multiCheck.check.id_checklist).ToList();
                var terminoPeacaoKey = _dbContext.checklist.Create().GetType().GetProperty("id_checklist").GetValue(checkAnterior[0]);
                _dbContext.Entry(_dbContext.Set<Data.checklist>().Find(terminoPeacaoKey)).CurrentValues.SetValues(multiCheck.check);

                _dbContext.SaveChanges();

                //multiCheck.check = null;

                //GerenciaLogin objGerLogin = new GerenciaLogin();
                //objGerLogin.deslogaUsuarioApp(Session.SessionID, this.HttpContext);

                //WebSecurity.Logout();
                //Session.Clear();

                //return RedirectToAction("Login", "Account", new { sucesso = 1 });
                return RedirectToAction("Index", "ImpressaoChecklist", new { sucesso = 1 });
            }
            else
            {
                return View(dadosConferencia2);
            }
        }

        [ActionName("Index")]
        [Models.Empresa.Submit("Voltar")]
        public ActionResult Voltar(Models.Empresa.ConferenciaCarga dadosConferenciaCarga)
        {
            Session["fromButton"] = 1;

            return RedirectToAction("Index", "RetornouUsina");
        }
    }
}
