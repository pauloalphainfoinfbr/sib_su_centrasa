using System;
using System.Collections.Generic;
using SIB.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SIB.Web.Controllers
{
    [Authorize]
    public class EstadoConservacaoController : BaseController
    {        
        public ActionResult Index(Models.Empresa.EstadoConservacao estadoConservacao)
        {
            if (Session["fromButton"] != null)
            {
                Session["fromButton"] = null;

                int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                string sessionID = Session.SessionID;
                Multiton multiCheck = Multiton.GetInstance(sessionID);

                estadoConservacao.tacografo = multiCheck.check.chk_tacografo != null ? Convert.ToInt32(multiCheck.check.chk_tacografo) : 4;
                estadoConservacao.chassi = multiCheck.check.chk_chassi != null ? Convert.ToInt32(multiCheck.check.chk_chassi) : 4;
                estadoConservacao.extintor = multiCheck.check.chk_extintor != null ? Convert.ToInt32(multiCheck.check.chk_extintor) : 4;
                estadoConservacao.triangMacaco = multiCheck.check.chk_triangMacaco != null ? Convert.ToInt32(multiCheck.check.chk_triangMacaco) : 4;                
                estadoConservacao.carroceria = multiCheck.check.chk_carroceria != null ? Convert.ToInt32(multiCheck.check.chk_carroceria) : 4;
                //estadoConservacao.assoalho = 4;
                estadoConservacao.pneusEstepes = multiCheck.check.chk_pneusEstepe != null ? Convert.ToInt32(multiCheck.check.chk_pneusEstepe) : 4;                                    
                estadoConservacao.luzFreio = multiCheck.check.chk_luzFreio != null ? Convert.ToInt32(multiCheck.check.chk_luzFreio) : 4;                
                estadoConservacao.cordasCintasCatracas = multiCheck.check.chk_cordasCintasCatracas != null ? Convert.ToInt32(multiCheck.check.chk_cordasCintasCatracas) : 4;                                    
                estadoConservacao.carroceriaLimpa = multiCheck.check.chk_carroceriaLimpa != null ? Convert.ToInt32(multiCheck.check.chk_carroceriaLimpa) : 4;
                estadoConservacao.faroisLanterna = multiCheck.check.chk_faroisLanterna != null ? Convert.ToInt32(multiCheck.check.chk_faroisLanterna) : 1;
                estadoConservacao.setas = multiCheck.check.chk_setas != null ? Convert.ToInt32(multiCheck.check.chk_setas) : 1;
                estadoConservacao.sinalSonoroRe = multiCheck.check.chk_sinalSonoroRe != null ? Convert.ToInt32(multiCheck.check.chk_sinalSonoroRe) : 1;
                estadoConservacao.parabrisaTrincado = multiCheck.check.chk_parabrisaTrincado != null ? Convert.ToInt32(multiCheck.check.chk_parabrisaTrincado) : 2;
                estadoConservacao.trincaTolerancia = multiCheck.check.chk_trincaTolerancia != null ? Convert.ToInt32(multiCheck.check.chk_trincaTolerancia) : 0;
                estadoConservacao.retrovisorTrincado = multiCheck.check.chk_retrovisorTrincado != null ? Convert.ToInt32(multiCheck.check.chk_retrovisorTrincado) : 2;
                estadoConservacao.trincaRetrovisorTolerancia = multiCheck.check.chk_trincaRetrovisorTolerancia != null ? Convert.ToInt32(multiCheck.check.chk_trincaRetrovisorTolerancia) : 0;


                estadoConservacao.parafusoRodas = multiCheck.check.chk_parafusoRodas != null ? Convert.ToInt32(multiCheck.check.chk_parafusoRodas) : 1;                
                estadoConservacao.dormentesFixados = multiCheck.check.chk_dormentesFixados != null ? Convert.ToInt32(multiCheck.check.chk_dormentesFixados) : 1;
                estadoConservacao.possuiBorrachasAssoalho = multiCheck.check.chk_possui_borrachas_assoalho != null ? Convert.ToInt32(multiCheck.check.chk_possui_borrachas_assoalho) : 1;
                estadoConservacao.laudoGanchoAdaptado = multiCheck.check.chk_laudoGanchoAdaptado != null ? Convert.ToInt32(multiCheck.check.chk_laudoGanchoAdaptado) : 1;
                estadoConservacao.todosPinos = multiCheck.check.chk_todosPinos != null ? Convert.ToInt32(multiCheck.check.chk_todosPinos) : 1;
                estadoConservacao.dobradicasBoasCondicoes = multiCheck.check.chk_dobradicasBoasCondicoes != null ? Convert.ToInt32(multiCheck.check.chk_dobradicasBoasCondicoes) : 1;
                estadoConservacao.estruturaTampasFerroMadeira = multiCheck.check.chk_estruturaTampasFerroMadeira != null ? Convert.ToInt32(multiCheck.check.chk_estruturaTampasFerroMadeira) : 4;
                                                                
                estadoConservacao.medFumacaNegra = multiCheck.check.chk_medidorFumacaNegra != null ? multiCheck.check.chk_medidorFumacaNegra.ToString() : "-1";

                estadoConservacao.tipoAssoalho = multiCheck.check.id_tipo_assoalho != null ? Convert.ToInt32(multiCheck.check.id_tipo_assoalho) : 0;
                estadoConservacao.EstadoAssoalho = multiCheck.check.chk_assoalho != null ? Convert.ToInt32(multiCheck.check.chk_assoalho) : 0;
                estadoConservacao.SituacaoAssoalho = multiCheck.check.chk_situacaoAssoalho != null ? Convert.ToInt32(multiCheck.check.chk_situacaoAssoalho) : 0;
                estadoConservacao.drenoBobineira = multiCheck.check.chk_drenoBobineira != null ? Convert.ToInt32(multiCheck.check.chk_drenoBobineira) : 1;

                estadoConservacao.placaLegivel = multiCheck.check.chk_placaLegivel != null ? Convert.ToInt32(multiCheck.check.chk_placaLegivel) : 1;
                estadoConservacao.ganchoPatola = multiCheck.check.chk_ganchoPatola != null ? Convert.ToInt32(multiCheck.check.chk_ganchoPatola) : 1;

                estadoConservacao.estadoGeral = multiCheck.check.chk_estadoGeral != null ? Convert.ToInt32(multiCheck.check.chk_estadoGeral) : 4;
                estadoConservacao.vazamentoOleo = multiCheck.check.chk_vazamentoOleo != null ? Convert.ToInt32(multiCheck.check.chk_vazamentoOleo) : 2;
                estadoConservacao.fotosCondicoesBobineira = multiCheck.check.chk_fotosCondicoesBobineira != null ? Convert.ToInt32(multiCheck.check.chk_fotosCondicoesBobineira) : 1;

                estadoConservacao.drenosDesobstruidos = multiCheck.check.chk_drenosDesobstruidos != null ? Convert.ToInt32(multiCheck.check.chk_drenosDesobstruidos) : 1;
                estadoConservacao.borrachaBobineiraInteirica = multiCheck.check.chk_borrachaBobineiraInterica != null ? Convert.ToInt32(multiCheck.check.chk_borrachaBobineiraInterica) : 1;
                estadoConservacao.borrachaFixaBobineira = multiCheck.check.chk_borrachaFixaBobineira != null ? Convert.ToInt32(multiCheck.check.chk_borrachaFixaBobineira) : 1;
                estadoConservacao.CondicoesBobineira = multiCheck.check.chk_condicoesBobineira != null ? Convert.ToInt32(multiCheck.check.chk_condicoesBobineira) : 4;
                estadoConservacao.travaSegTampas = multiCheck.check.chk_travaSegurancaTampas != null ? Convert.ToInt32(multiCheck.check.chk_travaSegurancaTampas) : 1;
            }
            
            return View(estadoConservacao);            
        }
        

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Proximo(Models.Empresa.EstadoConservacao estadoConservacao)
        {            
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            if ( (estadoConservacao.parabrisaTrincado == 0 || estadoConservacao.carroceria == 0 || estadoConservacao.pneusEstepes == 0 || 
                estadoConservacao.faroisLanterna == 0 || estadoConservacao.luzFreio == 0 || estadoConservacao.setas == 0 || 
                estadoConservacao.placaLegivel == 0 || estadoConservacao.ganchoPatola == 0 || estadoConservacao.estadoGeral == 0 || estadoConservacao.vazamentoOleo == 0 ||
                estadoConservacao.cordasCintasCatracas == 0 || estadoConservacao.CondicoesBobineira == 0 || estadoConservacao.medFumacaNegra == "-1" || estadoConservacao.fotosCondicoesBobineira == 0 || estadoConservacao.travaSegTampas == 0)
             )
            {
                ModelState.AddModelError("", " - Todos os campos são obrigatórios");                
            }

            if (estadoConservacao.parabrisaTrincado == 1 && estadoConservacao.trincaTolerancia == 0)
            {
                ModelState.AddModelError("", " - Informe se a trinca do parabrisa tem tolerância.");
            }

            if (estadoConservacao.retrovisorTrincado == 1 && estadoConservacao.trincaRetrovisorTolerancia == 0)
            {
                ModelState.AddModelError("", " - Informe se a trinca do retrovisor tem tolerância.");
            }

            if (estadoConservacao.tipoAssoalho == 0 || estadoConservacao.EstadoAssoalho == 0 || estadoConservacao.SituacaoAssoalho == 0)
            {
                ModelState.AddModelError("", " - Os campos \"Tipo Assoalho\", \"Estado Assoalho\" e \"Situação Assoalho\" são obrigatórios.");
            }

            if (ModelState.IsValid)
            {
                Session["fromButton"] = 1;

                multiCheck.check.chk_tacografo = estadoConservacao.tacografo;
                multiCheck.check.chk_chassi = estadoConservacao.chassi;
                multiCheck.check.chk_extintor = estadoConservacao.extintor;
                multiCheck.check.chk_triangMacaco = estadoConservacao.triangMacaco;
                multiCheck.check.chk_parabrisaTrincado = estadoConservacao.parabrisaTrincado;
                multiCheck.check.chk_retrovisorTrincado = estadoConservacao.retrovisorTrincado;

                if(multiCheck.check.chk_parabrisaTrincado == 1)
                    multiCheck.check.chk_trincaTolerancia = estadoConservacao.trincaTolerancia;

                if (multiCheck.check.chk_retrovisorTrincado == 1)
                    multiCheck.check.chk_trincaRetrovisorTolerancia = estadoConservacao.trincaRetrovisorTolerancia;

                multiCheck.check.chk_carroceria = estadoConservacao.carroceria;
                //multiCheck.check.chk_assoalho = estadoConservacao.assoalho;
                multiCheck.check.chk_pneusEstepe = estadoConservacao.pneusEstepes;                
                multiCheck.check.chk_faroisLanterna = estadoConservacao.faroisLanterna;                
                multiCheck.check.chk_luzFreio = estadoConservacao.luzFreio;
                multiCheck.check.chk_setas = estadoConservacao.setas;
                multiCheck.check.chk_cordasCintasCatracas = estadoConservacao.cordasCintasCatracas;                                
                multiCheck.check.chk_carroceriaLimpa = estadoConservacao.carroceriaLimpa;
                multiCheck.check.chk_sinalSonoroRe = estadoConservacao.sinalSonoroRe;


                multiCheck.check.chk_parafusoRodas = estadoConservacao.parafusoRodas;                
                multiCheck.check.chk_dormentesFixados = estadoConservacao.dormentesFixados;
                multiCheck.check.chk_possui_borrachas_assoalho = estadoConservacao.possuiBorrachasAssoalho;
                multiCheck.check.chk_laudoGanchoAdaptado = estadoConservacao.laudoGanchoAdaptado;
                multiCheck.check.chk_todosPinos= estadoConservacao.todosPinos;
                multiCheck.check.chk_dobradicasBoasCondicoes = estadoConservacao.dobradicasBoasCondicoes;
                multiCheck.check.chk_estruturaTampasFerroMadeira = estadoConservacao.estruturaTampasFerroMadeira;
                multiCheck.check.chk_drenoBobineira = estadoConservacao.drenoBobineira;

                multiCheck.check.chk_medidorFumacaNegra = estadoConservacao.medFumacaNegra;

                multiCheck.check.id_tipo_assoalho = estadoConservacao.tipoAssoalho;
                multiCheck.check.chk_assoalho = estadoConservacao.EstadoAssoalho;
                multiCheck.check.chk_situacaoAssoalho = estadoConservacao.SituacaoAssoalho;

                multiCheck.check.chk_placaLegivel = estadoConservacao.placaLegivel;
                multiCheck.check.chk_estadoGeral = estadoConservacao.estadoGeral;
                multiCheck.check.chk_vazamentoOleo = estadoConservacao.vazamentoOleo;
                multiCheck.check.chk_ganchoPatola = estadoConservacao.ganchoPatola;
                multiCheck.check.chk_fotosCondicoesBobineira = estadoConservacao.fotosCondicoesBobineira;

                multiCheck.check.chk_drenosDesobstruidos = estadoConservacao.drenosDesobstruidos;
                multiCheck.check.chk_borrachaBobineiraInterica = estadoConservacao.borrachaBobineiraInteirica;
                multiCheck.check.chk_borrachaFixaBobineira = estadoConservacao.borrachaFixaBobineira;
                multiCheck.check.chk_condicoesBobineira = estadoConservacao.CondicoesBobineira;
                multiCheck.check.chk_travaSegurancaTampas = estadoConservacao.travaSegTampas;

                return RedirectToAction("Index", "BercoMetalico");
            }

            return View(estadoConservacao);
        }       

        [ActionName("Index")]
        [Models.Empresa.Submit("Voltar")]
        public ActionResult Voltar(Models.Empresa.EstadoConservacao dadosEstadoConservacao)
        {
            Session["fromButton"] = 1;

            return RedirectToAction("Index", "TipoVeiculoEcarroceria");
        }

    }
}
