using System;
using System.Collections.Generic;
using WebMatrix.WebData;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootstrapMvcSample.Controllers
{
    public class ExampleLayoutsController : Controller
    {
        public ActionResult Starter()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            int id_user = 0;

            if (Session["usuario"] != null)
            {
                id_user = Session["usuario"] != null ? ((SIB.Data.usuario)Session["usuario"]).id_usuario : 0;
                string sessionID = Session.SessionID;
                SIB.Web.Controllers.Multiton multiCheck = SIB.Web.Controllers.Multiton.GetInstance(sessionID);
                multiCheck.check = null;
            }

            for (int i = 0; i < HttpContext.ApplicationInstance.Application.Contents.Count; i++)
            {
                var user = HttpContext.ApplicationInstance.Application.Contents[i];

                if (id_user == ((SIB.Data.usuario)user).id_usuario)
                {
                    HttpContext.ApplicationInstance.Application.RemoveAt(i);
                }
            }

            WebSecurity.Logout();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult Marketing()
        {
            return View();
        }

        public ActionResult Fluid()
        {
            return View();
        }

        public ActionResult Narrow()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult StickyFooter()
        {
            return View("TBD");
        }

        public ActionResult Carousel()
        {
            return View("TBD");
        }
    }
}
