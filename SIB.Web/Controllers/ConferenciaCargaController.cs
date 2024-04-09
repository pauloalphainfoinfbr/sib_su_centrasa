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

namespace SIB.Web.Controllers
{
    [Authorize]
    public class ConferenciaCargaController : BaseController
    {
        public static DateTime data_inicio_2_fase { get; set; }

        public ActionResult Index(Models.Empresa.ConferenciaCarga dadosConferenciaCarga)
        {            
            if (Session["fromButton"] != null)
            {
                Session["fromButton"] = null;

                TempData["TpSimNaoNa"] = new SelectList(
                    new Models.Empresa.TpSimNaoNa().getTpSimNaoNa(),
                    "ID",
                    "Desc"
                );

                TempData["TpAvaria"] = new SelectList(
                    new Models.Empresa.TpAvariaMaterial().getTpAvariaMaterial(),
                    "ID",
                    "Desc"
                );

                int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                string sessionID = Session.SessionID;
                Multiton multiCheck = Multiton.GetInstance(sessionID);

                //dadosConferenciaCarga.materialMolhado = multiCheck.check.chk_materialMolhado != null ? multiCheck.check.chk_materialMolhado : 2;
                dadosConferenciaCarga.cintamentoFardos = multiCheck.check.chk_cintamentoFardos != null ? multiCheck.check.chk_cintamentoFardos : 2;
                dadosConferenciaCarga.chovendo = multiCheck.check.chk_materialChovendo != null ? multiCheck.check.chk_materialChovendo : 2;
                dadosConferenciaCarga.AvariaMaterial = multiCheck.check.chk_avariaMaterial != null ? multiCheck.check.chk_avariaMaterial : 2;
                dadosConferenciaCarga.tmpGuardaLatTravad = multiCheck.check.chk_tmpGuardaLatTravad != null ? multiCheck.check.chk_tmpGuardaLatTravad : 2;
                dadosConferenciaCarga.TipoAvariaMaterial = multiCheck.check.chk_tipoAvaria != null ? multiCheck.check.chk_tipoAvaria : 0;

                return View(dadosConferenciaCarga);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Proximo(Models.Empresa.ConferenciaCarga dadosConferenciaCarga)
        {            
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            if (dadosConferenciaCarga.cintamentoFardos == 0 || dadosConferenciaCarga.chovendo == 0 ||
               dadosConferenciaCarga.AvariaMaterial == 0 || dadosConferenciaCarga.AvariaMaterial == 0)
            {
                ModelState.AddModelError("", " - Todos os campos são obrigatórios.");
            }

            if(dadosConferenciaCarga.AvariaMaterial == 1 && dadosConferenciaCarga.TipoAvariaMaterial == 0)
                ModelState.AddModelError("", " - Deve ser informado o tipo da avaria do material.");

            if (ModelState.IsValid)
            {
                Session["fromButton"] = 1;

                //multiCheck.check.chk_materialMolhado = dadosConferenciaCarga.materialMolhado;
                multiCheck.check.chk_cintamentoFardos = dadosConferenciaCarga.cintamentoFardos;
                multiCheck.check.chk_materialChovendo = dadosConferenciaCarga.chovendo;
                multiCheck.check.chk_avariaMaterial = dadosConferenciaCarga.AvariaMaterial;
                multiCheck.check.chk_tmpGuardaLatTravad = dadosConferenciaCarga.tmpGuardaLatTravad;
                
                multiCheck.check.chk_tipoAvaria = dadosConferenciaCarga.AvariaMaterial == 1 ? dadosConferenciaCarga.TipoAvariaMaterial : null;

                return RedirectToAction("Index", "RetornouUsina");
            }
            else
            {
                TempData["TpSimNaoNa"] = new SelectList(
                    new Models.Empresa.TpSimNaoNa().getTpSimNaoNa(),
                    "ID",
                    "Desc"
                );

                TempData["TpAvaria"] = new SelectList(
                    new Models.Empresa.TpAvariaMaterial().getTpAvariaMaterial(),
                    "ID",
                    "Desc"
                );

                return View(dadosConferenciaCarga);
            }
        }

        [ActionName("Index")]
        [Models.Empresa.Submit("Voltar")]
        public ActionResult Voltar(Models.Empresa.ConferenciaCarga dadosConferenciaCarga)
        {
            Session["fromButton"] = 1; 

            return RedirectToAction("Index", "VeiculoPossui");
        }
    }
}
