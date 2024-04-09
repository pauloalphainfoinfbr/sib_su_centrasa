using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIB.Data;

namespace SIB.Web.Controllers
{
    public class RetornouUsinaController : Controller
    {
        public ActionResult Index(Models.Empresa.RetornouUsina retornouUsina)
        {
            if (Session["fromButton"] != null)
            {
                Session["fromButton"] = null;

                int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                string sessionID = Session.SessionID;
                Multiton multiCheck = Multiton.GetInstance(sessionID);

                if (multiCheck.check.chk_retornouUsina == null)
                    retornouUsina.retornouUsina = 2;
                else
                    retornouUsina.retornouUsina = Convert.ToInt32(multiCheck.check.chk_retornouUsina);
                
                retornouUsina.numeroLote = multiCheck.check.chk_numLote;
                retornouUsina.cliente = multiCheck.check.chk_cliente;
                retornouUsina.observacao = multiCheck.check.chk_observacao;
                //retornouUsina.material = multiCheck.check.chk_material;

                return View(retornouUsina);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Proximo(Models.Empresa.RetornouUsina dadosRetornouUsina)
        {
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            Int64 value = 0;
            

            if (dadosRetornouUsina.retornouUsina == 1)
            {
                if (string.IsNullOrWhiteSpace(dadosRetornouUsina.numeroLote) || string.IsNullOrWhiteSpace(dadosRetornouUsina.cliente)
                  )
                {
                    ModelState.AddModelError("", "Os campos \"Tipo Material\", \"Número do Lote\" e \"Cliente\" são obrigatórios. ");
                }

                if (dadosRetornouUsina.numeroLote != null)
                {
                    string[] lista = dadosRetornouUsina.numeroLote.Trim().Split('/');

                    for (int i = 0; i < lista.Length; i++)
                    {
                        if (lista[i].Length > 10)
                            ModelState.AddModelError("", "O campo \"Número do Lote\" deve conter no máximo 10 dígitos.");

                        if (!Int64.TryParse(lista[i], out value))
                        {
                            ModelState.AddModelError("", "O campo \"Número do Lote\" deve ser numérico e separado por \"/\" caso exista mais de 1.");
                        }
                    }
                }
            }

            if ((multiCheck.check.chk_materialMolhado == 1 || multiCheck.check.chk_avariaMaterial == 1) && string.IsNullOrWhiteSpace(dadosRetornouUsina.observacao))
            {
                string dano = "";

                if (multiCheck.check.chk_materialMolhado == 1 && multiCheck.check.chk_avariaMaterial == 1)
                    dano += " Material Esta Molhado e Avaria no material";   
                else if(multiCheck.check.chk_materialMolhado == 1 && multiCheck.check.chk_avariaMaterial != 1)
                    dano += " Material Esta Molhado ";   
                else
                    dano += " Avaria no material ";

                ModelState.AddModelError("", "Na conferência da carga, foi informado a(s) opção(ões) " + dano +  ", sendo assim, o campo observação deve ser informado.");
            }
            else if ((multiCheck.check.chk_materialMolhado == 1 || multiCheck.check.chk_avariaMaterial == 1) && dadosRetornouUsina.observacao.Length < 20)
            {
                ModelState.AddModelError("", "O campo observação deve ter no mínimo 20 caracteres.");
            }

            if (ModelState.IsValid)
            {
                Session["fromButton"] = 1;

                multiCheck.check.chk_retornouUsina = dadosRetornouUsina.retornouUsina;
                                    
                multiCheck.check.chk_numLote = dadosRetornouUsina.numeroLote;
                multiCheck.check.chk_cliente = dadosRetornouUsina.cliente;
                multiCheck.check.chk_observacao = dadosRetornouUsina.observacao;
                //multiCheck.check.chk_material = dadosRetornouUsina.material;
                

                return RedirectToAction("Index", "ConferenciaCarga2");
            }
            else
            {
                return View(dadosRetornouUsina);
            }
        }

        [ActionName("Index")]
        [Models.Empresa.Submit("Voltar")]
        public ActionResult Voltar(Models.Empresa.EstadoConservacao dadosEstadoConservacao)
        {
            Session["fromButton"] = 1;

            return RedirectToAction("Index", "ConferenciaCarga");
        }

    }
}
