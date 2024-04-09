using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity.Infrastructure;
using SIB.Data;


namespace SIB.Web.Controllers
{
    public class DadosVeiculo2Controller : Controller
    {
        public ActionResult Index(Models.Empresa.DadosVeiculo2 dadosVeiculo2)
        {
            if (Session["usuario"] != null)
            {
                if (Session["fromButton"] != null)
                {
                    Session["fromButton"] = null;

                    UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
                    int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                    string sessionID = Session.SessionID;
                    Multiton multiCheck = Multiton.GetInstance(sessionID);

                    dadosVeiculo2.EPI = multiCheck.check.chk_epi != null ? Convert.ToInt32(multiCheck.check.chk_epi) : 1;
                    dadosVeiculo2.capaceteCarnJugular = multiCheck.check.chk_capaceteCarnJugular != null ? Convert.ToInt32(multiCheck.check.chk_capaceteCarnJugular) : 1;
                    dadosVeiculo2.oculosSeguranca = multiCheck.check.chk_oculosSeguranca != null ? Convert.ToInt32(multiCheck.check.chk_oculosSeguranca) : 1;
                    dadosVeiculo2.protetorAuditivo = multiCheck.check.chk_protetorAuditivo != null ? Convert.ToInt32(multiCheck.check.chk_protetorAuditivo) : 1;                        
                    dadosVeiculo2.perneira = multiCheck.check.chk_perneira != null ? Convert.ToInt32(multiCheck.check.chk_perneira) : 1;
                    dadosVeiculo2.botina = multiCheck.check.chk_botina != null ? Convert.ToInt32(multiCheck.check.chk_botina) : 1;
                    dadosVeiculo2.mangote = multiCheck.check.chk_mangote != null ? Convert.ToInt32(multiCheck.check.chk_mangote) : 1;                    

                }

            }

            return View(dadosVeiculo2);
        }

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Proximo(Models.Empresa.DadosVeiculo2 dadosVeiculo2)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            Session["fromButton"] = 1;

            if (dadosVeiculo2.EPI == 0 || dadosVeiculo2.capaceteCarnJugular == 0 || dadosVeiculo2.oculosSeguranca == 0 ||
               dadosVeiculo2.protetorAuditivo == 0 || dadosVeiculo2.perneira == 0 || dadosVeiculo2.botina == 0 ||
               dadosVeiculo2.mangote == 0
              )
            {
                ModelState.AddModelError("", "Todos os campos são obrigatórios");
            }

            if(ModelState.IsValid)
            {
                multiCheck.check.chk_epi = dadosVeiculo2.EPI;
                multiCheck.check.chk_capaceteCarnJugular = dadosVeiculo2.capaceteCarnJugular;
                multiCheck.check.chk_oculosSeguranca = dadosVeiculo2.oculosSeguranca;
                multiCheck.check.chk_protetorAuditivo = dadosVeiculo2.protetorAuditivo;            
                multiCheck.check.chk_perneira = dadosVeiculo2.perneira;
                multiCheck.check.chk_botina = dadosVeiculo2.botina;
                multiCheck.check.chk_mangote = dadosVeiculo2.mangote;

                return RedirectToAction("Index", "TipoVeiculoEcarroceria");                                     
            }
            else
            {
                return View(dadosVeiculo2);
            }
        }


        [ActionName("Index")]
        [Models.Empresa.Submit("Voltar")]
        public ActionResult Voltar(Models.Empresa.DadosVeiculo dadosVeiculo)
        {
            Session["fromButton"] = 1;
            return RedirectToAction("Index", "DadosVeiculo");
        }
    }
}
