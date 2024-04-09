using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIB.Web.Controllers
{
    [Authorize]
    public class BercoMetalicoController : Controller
    {
        public ActionResult Index(Models.Empresa.BercoMetalico bercoMetalico)
        {
            if (Session["fromButton"] != null)
            {
                Session["fromButton"] = null;

                int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                string sessionID = Session.SessionID;
                Multiton multiCheck = Multiton.GetInstance(sessionID);

                if (multiCheck.check.chk_necesMontarBerco == null)
                {
                    bercoMetalico.necessarioMontarBerco = 2;
                    bercoMetalico.possui = 1;
                    //bercoMetalico.metalico = 1;
                    bercoMetalico.bercoProprio = 1;
                    bercoMetalico.liberado = 1;
                    bercoMetalico.bobineira = 1;
                }
                else
                {
                    bercoMetalico.necessarioMontarBerco = Convert.ToInt32(multiCheck.check.chk_necesMontarBerco);
                    bercoMetalico.possui = Convert.ToInt32(multiCheck.check.chk_possui);
                    //bercoMetalico.metalico = Convert.ToInt32(multiCheck.check.chk_metalico);
                    bercoMetalico.bercoProprio = Convert.ToInt32(multiCheck.check.chk_bercoProprio);
                    bercoMetalico.liberado = Convert.ToInt32(multiCheck.check.chk_liberado);
                    bercoMetalico.bobineira = Convert.ToInt32(multiCheck.check.chk_bobineira);
                }
            }

            return View(bercoMetalico);
        }

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Proximo(Models.Empresa.BercoMetalico bercoMetalico)
        {
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            multiCheck.check.chk_necesMontarBerco = bercoMetalico.necessarioMontarBerco;
            multiCheck.check.chk_possui = bercoMetalico.possui;
            //multiCheck.check.chk_metalico = bercoMetalico.metalico;
            multiCheck.check.chk_bercoProprio = bercoMetalico.bercoProprio;
            multiCheck.check.chk_liberado = bercoMetalico.liberado;
            multiCheck.check.chk_bobineira = bercoMetalico.bobineira;

            Session["fromButton"] = 1;
            return RedirectToAction("Index", "VeiculoLiberado");            
            
        }

        [ActionName("Index")]
        [Models.Empresa.Submit("Voltar")]
        public ActionResult Voltar(Models.Empresa.BercoMetalico bercoMetalico)
        {
            Session["fromButton"] = 1;

            return RedirectToAction("Index", "EstadoConservacao");
        }

    }
}
