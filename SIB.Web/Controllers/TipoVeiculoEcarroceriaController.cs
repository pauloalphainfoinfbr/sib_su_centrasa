using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIB.Data;

namespace SIB.Web.Controllers
{
    [Authorize]
    public class TipoVeiculoEcarroceriaController : BaseController
    {        
        public ActionResult Index(Models.Empresa.TipoVeiculoEcarroceria dadosTipoVeicEcarroc)
        {
            if (Session["fromButton"] != null)
            {
                Session["fromButton"] = null;

                int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                string sessionID = Session.SessionID;
                Multiton multiCheck = Multiton.GetInstance(sessionID);

                if (multiCheck.check.id_tipo_veiculo == null && multiCheck.check.id_tipo_carroceria == null)
                {
                    dadosTipoVeicEcarroc.tipoVeiculo = 1;
                    dadosTipoVeicEcarroc.tipoCarroceria = 1;
                }
                else
                {
                    if (multiCheck.check.id_tipo_veiculo > 0)
                        dadosTipoVeicEcarroc.tipoVeiculo = (Int32)multiCheck.check.id_tipo_veiculo;

                    if (multiCheck.check.id_tipo_carroceria > 0)
                        dadosTipoVeicEcarroc.tipoCarroceria = (Int32)multiCheck.check.id_tipo_carroceria;                    
                }
                
                dadosTipoVeicEcarroc.placa2 = multiCheck.check.chk_placa2;
               
                dadosTipoVeicEcarroc.placa3 = multiCheck.check.chk_placa3;

                return View(dadosTipoVeicEcarroc);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Proximo(Models.Empresa.TipoVeiculoEcarroceria dadosTipoVeicEcarroc)
        {
            Session["fromButton"] = 1;

            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);            

            if (dadosTipoVeicEcarroc.tipoCarroceria == 0 || dadosTipoVeicEcarroc.tipoVeiculo == 0)
            {
                ModelState.AddModelError("", " - Os campos \"Tipo de Veiculo\" e \"Tipo de Carroceria\" são obrigatórios.");
            }

            if (ModelState.IsValid)
            {                
                multiCheck.check.id_tipo_veiculo = dadosTipoVeicEcarroc.tipoVeiculo;
                multiCheck.check.id_tipo_carroceria = dadosTipoVeicEcarroc.tipoCarroceria;

                multiCheck.check.chk_placa2 = dadosTipoVeicEcarroc.placa2;
                multiCheck.check.chk_placa3 = dadosTipoVeicEcarroc.placa3;

                return RedirectToAction("Index", "EstadoConservacao");                
            }
            else
            {
                return View(dadosTipoVeicEcarroc);
            }
        }

        [ActionName("Index")]
        [Models.Empresa.Submit("Voltar")]
        public ActionResult Voltar()
        {
            Session["fromButton"] = 1;

            return RedirectToAction("Index", "DadosVeiculo2");
        }

    }
}
