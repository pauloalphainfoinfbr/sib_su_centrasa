using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebMatrix.WebData;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SIB.Data;



namespace SIB.Web.Controllers
{
    [Authorize]
    public class RegistroInicioPeacaoController : Controller
    {       
        public ActionResult Index(Models.Empresa.RegistroInicioPeacao inicioPeacao)
        {
            if (Session["fromButton"] != null)
            {
                Session["fromButton"] = null;

                int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
                string sessionID = Session.SessionID;
                Multiton multiCheck = Multiton.GetInstance(sessionID);

                inicioPeacao.placa = multiCheck.check.placa;
                inicioPeacao.dataPeacao = DateTime.Now;                

                return View(inicioPeacao);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Salvar(Models.Empresa.RegistroInicioPeacao inicioPeacao)
        {
            UsiminasIpatingaEntities _dbContext = new UsiminasIpatingaEntities();
            int id_user = ((SIB.Data.usuario)Session["usuario"]).id_usuario;
            string sessionID = Session.SessionID;
            Multiton multiCheck = Multiton.GetInstance(sessionID);

            if (ModelState.IsValid)
            {
                Session["fromButton"] = 1;

                multiCheck.check.data_inicio_peacao = DateTime.Now;

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

                return RedirectToAction("Login", "Account", new { sucesso = 1 });
            }

            return View();
        }

    }
}
