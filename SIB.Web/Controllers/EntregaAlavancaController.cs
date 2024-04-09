using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIB.Data;
using System.Data.Entity;
using WebMatrix.WebData;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;

namespace SIB.Web.Controllers
{
    public class lstEntregaAlavanca
    {
        public int ID { get; set; }
        public string Desc { get; set; }

        public List<lstEntregaAlavanca> getOptionList()
        {
            List<lstEntregaAlavanca> optionList = new List<lstEntregaAlavanca>()
            {
                new lstEntregaAlavanca() { ID = 1, Desc = "Sim" },
                new lstEntregaAlavanca() { ID = 2, Desc = "Não" },
                new lstEntregaAlavanca() { ID = 3, Desc = "Catracão" }
            };

            return optionList;
        }
    }

    public class EntregaAlavancaController : Controller
    {
        public ActionResult Index(Models.Empresa.Alavanca alavanca)
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

                    ViewBag.Alavanca = new SelectList(
                        new lstEntregaAlavanca().getOptionList().ToList(),
                        "ID",
                        "Desc"
                    );

                    alavanca.entregaAlavanca = multiCheck.check.chk_entregaAlavanca != null ? Convert.ToInt32(multiCheck.check.chk_entregaAlavanca) : 0;                    

                }

            }

            return View(alavanca);
        }

        [HttpPost, ActionName("Index")]
        [Models.Empresa.Submit(Models.Empresa.SubmitRequirement.StartsWith, "Proximo")]
        public ActionResult Proximo(Models.Empresa.Alavanca alavanca)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            Session["fromButton"] = 1;

            if (alavanca.entregaAlavanca == null)
                ModelState.AddModelError("", "- Informe se houve entrega de alavanca");

            if (ModelState.IsValid)
            {
                multiCheck.check.chk_entregaAlavanca = alavanca.entregaAlavanca;

                var checkAnterior = _dbContext.checklist.Where(r => r.id_checklist == multiCheck.check.id_checklist).ToList();
                var terminoPeacaoKey = _dbContext.checklist.Create().GetType().GetProperty("id_checklist").GetValue(checkAnterior[0]);
                _dbContext.Entry(_dbContext.Set<Data.checklist>().Find(terminoPeacaoKey)).CurrentValues.SetValues(multiCheck.check);

                //Salva os dados no banco
                _dbContext.SaveChanges();

                //Zera o Contexto Global
                multiCheck.check = null;

                //Remove usuario do Application
                GerenciaLogin objGerLogin = new GerenciaLogin();
                objGerLogin.deslogaUsuarioApp(Session.SessionID, this.HttpContext);

                //Redireciona para o Login
                WebSecurity.Logout();
                Session.Clear();

                return RedirectToAction("Index", "BuscaVeiculo");
            }
            else
            {
                ViewBag.Alavanca = new SelectList(
                        new lstEntregaAlavanca().getOptionList().ToList(),
                        "ID",
                        "Desc"
                    );

                return View(alavanca);
            }
        }

        [ActionName("Index")]
        [Models.Empresa.Submit("Voltar")]
        public ActionResult Voltar()
        {
            Session["fromButton"] = 1;
            return RedirectToAction("Index", "BuscaVeiculo");
        }

    }
}
