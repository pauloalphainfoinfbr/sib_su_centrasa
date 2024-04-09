using System;
using System.Collections.Generic;
using SIB.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIB.Web.Controllers
{
    public class LstAlavancaDevolvida
    {
        public int ID { get; set; }
        public string Desc { get; set; }

        public List<LstAlavancaDevolvida> getLstAlavancaDevolvida()
        {
            List<LstAlavancaDevolvida> lstAlavancaDevolvida = new List<LstAlavancaDevolvida>()
            {
                new LstAlavancaDevolvida() { ID = 1, Desc = "Sim" },
                new LstAlavancaDevolvida() { ID = 2, Desc = "Não" }
            };

            return lstAlavancaDevolvida;
        }
    }

    public class LstTipoMaterial
    {
        public string ID { get; set; }
        public string Desc { get; set; }

        public List<LstTipoMaterial> getLstTipoMaterial()
        {
            List<LstTipoMaterial> lstTipoMaterial = new List<LstTipoMaterial>()
            {
                new LstTipoMaterial() { ID = "Bobina", Desc = "Bobina" },
                new LstTipoMaterial() { ID = "Chapa", Desc = "Chapa" },
                new LstTipoMaterial() { ID = "Fardos", Desc = "Fardos" },
                new LstTipoMaterial() { ID = "Outros", Desc = "Outros" }
            };

            return lstTipoMaterial;
        }
    }

    [Authorize]
    public class VeiculoPossuiController : BaseController
    {        
        public ActionResult Index(Models.Empresa.VeiculoPossui dadosVeiculoPossui)
        {
            Int32 value = 0;

            if (Session["usuario"] != null)
            {
                if (Session["fromButton"] != null)
                {
                    Session["fromButton"] = null;

                    int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                    string sessionID = Session.SessionID;
                    Multiton multiCheck = Multiton.GetInstance(sessionID);

                    string[] selectedMaterials = multiCheck.check.chk_material != null ? multiCheck.check.chk_material.Split('/') : new string[] { };

                    ViewBag.TpMaterial = new MultiSelectList(
                        new LstTipoMaterial().getLstTipoMaterial(),
                        "ID",
                        "Desc",
                        selectedMaterials.ToArray<string>()
                    );

                    ViewBag.TpAlavancaDevolvida = new SelectList(
                        new LstAlavancaDevolvida().getLstAlavancaDevolvida(),
                        "ID",
                        "Desc",
                        selectedMaterials.ToArray<string>()
                    );

                    dadosVeiculoPossui.DT = multiCheck.check.chk_DT.ToString();
                    dadosVeiculoPossui.tonelagem = multiCheck.check.tonelada.ToString();
                    dadosVeiculoPossui.qtdeEntregas = multiCheck.check.chk_numeroEntregas.ToString();
                    //dadosVeiculoPossui.alavancaDevolvida = multiCheck.check.chk_devolucaoAlavanca != null ? (int)multiCheck.check.chk_devolucaoAlavanca : 0;
                    //dadosVeiculoPossui.entregaAlavancas = multiCheck.check.chk_entregaAlavanca == 1 ? "Sim" : multiCheck.check.chk_entregaAlavanca == 2 ? "Não" : "Catracão";


                    return View(dadosVeiculoPossui);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                ModelState.AddModelError("", "Os campos \"DT\", \"Tonelada\" e \" Qtde entrega\" devem ser numéricos.");
                return View(dadosVeiculoPossui);
            }
        }       

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Proximo(Models.Empresa.VeiculoPossui dadosVeiculoPossui)
        {            
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);            

            Int32 value = 0;

            if (string.IsNullOrWhiteSpace(dadosVeiculoPossui.DT) || string.IsNullOrWhiteSpace(dadosVeiculoPossui.tonelagem) || string.IsNullOrWhiteSpace(dadosVeiculoPossui.qtdeEntregas))
                ModelState.AddModelError("", "Os campos \"DT\", \"Peso\" e \"Qtde. Entrega\" devem ser preenchidos. ");
            else if (!Int32.TryParse(dadosVeiculoPossui.DT, out value) || !Int32.TryParse(dadosVeiculoPossui.tonelagem, out value) || !Int32.TryParse(dadosVeiculoPossui.qtdeEntregas, out value))
                ModelState.AddModelError("", "Os campos \"DT\", \"Peso\" e \"Qtde. Entrega\" devem ser numéricos. ");

            //if (multiCheck.check.chk_entregaAlavanca == 1 && dadosVeiculoPossui.alavancaDevolvida == null)
            //    ModelState.AddModelError("", "Informe se a alavanca foi devolvida");
            //else if (multiCheck.check.chk_entregaAlavanca == 1 && dadosVeiculoPossui.alavancaDevolvida == 2) {
            //    ModelState.AddModelError("", "Não é possível prosseguir com o checklist até a alavanca ser devolvida");
            //    ViewBag.Msg = "Não é possível prosseguir com o checklist até a alavanca ser devolvida";
            //}

            if (ModelState.IsValid)
            {
                Session["fromButton"] = 1;
                                              
                multiCheck.check.chk_material = "";

                if (dadosVeiculoPossui.materiais != null)
                    multiCheck.check.chk_material = String.Join("/", dadosVeiculoPossui.materiais);

                multiCheck.check.chk_DT = Convert.ToInt32(dadosVeiculoPossui.DT);
                multiCheck.check.tonelada = Convert.ToInt32(dadosVeiculoPossui.tonelagem);
                multiCheck.check.chk_numeroEntregas = Convert.ToInt32(dadosVeiculoPossui.qtdeEntregas);

                //Se foi entregue alavanca para o motorista (Caso tenha sido informado (Sim), 
                //nesta tela é informado se foi devolvida, se não o campo chk_devolucaoAlavanca permanece null
                //if (multiCheck.check.chk_entregaAlavanca == 1)
                //    multiCheck.check.chk_devolucaoAlavanca = (int)dadosVeiculoPossui.alavancaDevolvida;
                //else
                //    multiCheck.check.chk_devolucaoAlavanca = null;


                return RedirectToAction("Index", "ConferenciaCarga");
            }
            else
            {
                dadosVeiculoPossui.materiais = dadosVeiculoPossui.materiais.ToArray<string>();

                ViewBag.TpMaterial = new MultiSelectList(
                        new LstTipoMaterial().getLstTipoMaterial(),
                        "ID",
                        "Desc",
                        dadosVeiculoPossui.materiais
                    );

                //ViewBag.TpAlavancaDevolvida = new SelectList(
                //        new LstAlavancaDevolvida().getLstAlavancaDevolvida(),
                //        "ID",
                //        "Desc"
                //    );

                //dadosVeiculoPossui.entregaAlavancas = multiCheck.check.chk_entregaAlavanca == 1 ? "Sim" : multiCheck.check.chk_entregaAlavanca == 2 ? "Não" : "Catracão";

                return View(dadosVeiculoPossui);
            }
            
        }

        //[ActionName("Index")]
        //[Models.Empresa.Submit("Voltar")]
        //public ActionResult Voltar()
        //{
        //    Session["fromButton"] = 1;

        //    return RedirectToAction("Index", "BercoMetalico");
        //}

    }
}

