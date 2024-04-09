using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using SIB.Web.Filters;
using SIB.Web.Models;
using SIB.Data;


namespace SIB.Web.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : BaseController
    {
        //
        // GET: /Account/Login

        SIB.Data.UsiminasIpatingaEntities _db = new UsiminasIpatingaEntities();

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Models.LoginModel model = new LoginModel();            

            if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
            {
                model.UserName = Request.Cookies["UserName"].Value;
                model.Password = Request.Cookies["Password"].Value;
                model.RememberMe = true;
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]        
        public ActionResult Login(LoginModel model, string returnUrl)
        {            
            if (ModelState.IsValid)
            {
                string senha = FormsAuthentication.HashPasswordForStoringInConfigFile(model.Password, "SHA1");
                //var usuario = _db.usuario.Where(u => model.UserName == u.login && senha == u.senha && u.id_tipo_usuario == 2).ToList().Count > 0 ? _db.usuario.SingleOrDefault(u => model.UserName == u.login && senha == u.senha && u.id_tipo_usuario == 2) : null;
                usuario usuario = _db.usuario.Where(u => model.UserName == u.login && senha == u.senha && u.id_tipo_usuario == 2).ToList().Count > 0 ? _db.usuario.SingleOrDefault(u => model.UserName == u.login && senha == u.senha && u.id_tipo_usuario == 2) : null;                

                GerenciaLogin objGerLogin = new GerenciaLogin();

                if(usuario != null)
                {
                    usuario.application_sessionID = Session.SessionID;

                    if (Session["usuario"] == null)
                    {
                        if (Session.SessionID != objGerLogin.retornaSessionID(Session.SessionID, this.HttpContext))
                        {
                            FormsAuthentication.SetAuthCookie(model.UserName, false);
                            
                            HttpContext.ApplicationInstance.Application.Add("usuario", Session.SessionID);
                            Session["usuario"] = usuario;
                            Session["fromButton"] = 1;

                            //Mantem a senha do usuário no campo
                            if (model.RememberMe)
                            {
                                if (Request.Cookies["UserName"] == null && Request.Cookies["Password"] == null)
                                {
                                    Response.Cookies["UserName"].Expires = DateTime.Now.AddHours(08);
                                    Response.Cookies["Password"].Expires = DateTime.Now.AddHours(08);
                                }
                            }
                            else
                            {
                                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                            }

                            Response.Cookies["Username"].Value = model.UserName;
                            Response.Cookies["Password"].Value = model.Password;

                            return RedirectToAction("Index", "BuscaVeiculo");
                                     
                        }                        
                        else
                        {
                            ModelState.AddModelError("", "O usuário " + usuario.nome_usuario + " já esta logado em outro dispositivo.");                            
                            return View(model);
                        }
                    }
                    else
                    {
                        if (((Data.usuario)Session["usuario"]).id_usuario == usuario.id_usuario)
                        {
                            Session["fromButton"] = 1;
                            return RedirectToAction("Index", "BuscaVeiculo");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Já existe um usuário logado nesta sessão.");
                            return View(model);
                        }
                    }                                     
                }
                else
                {
                    // If we got this far, something failed, redisplay form
                    ModelState.AddModelError("", "Nome de usuário ou senha inválido.");
                    return View(model);
                }
            }
            else
            {                
                return View(model);
            }
        }        

        [HttpPost]        
        public ActionResult LogOff()
        {
            int id_user = Session["usuario"] != null ? ((SIB.Data.usuario)Session["usuario"]).id_usuario : 0;

            GerenciaLogin objGerLogin = new GerenciaLogin();
            objGerLogin.deslogaUsuarioApp(Session.SessionID, this.HttpContext);            

            WebSecurity.Logout();

            return RedirectToAction("Account", "Login");
        }

        public ActionResult LogOut()
        {
            int id_user = Session["usuario"] != null ? ((SIB.Data.usuario)Session["usuario"]).id_usuario : 0;
            string sessionID = Session.SessionID;

            if (Session["usuario"] != null)
            {                
                SIB.Web.Controllers.Multiton multiCheck = SIB.Web.Controllers.Multiton.GetInstance(sessionID);                

                multiCheck.check = null;
            }

            GerenciaLogin objGerLogin = new GerenciaLogin();
            objGerLogin.deslogaUsuarioApp(Session.SessionID, this.HttpContext);

            Session.Abandon();
            WebSecurity.Logout();            
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/Register

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId)
        {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name)
            {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
                {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1)
                    {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Sua senha foi alterada."
                : message == ManageMessageId.SetPasswordSuccess ? "Seu password foi definido."
                : message == ManageMessageId.RemoveLoginSuccess ? "O login externo foi removido."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(LocalPasswordModel model)
        {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount)
            {
                if (ModelState.IsValid)
                {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try
                    {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    }
                    catch (Exception)
                    {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        ModelState.AddModelError("", "A senha atual está incorreta ou a nova senha é inválida.");
                    }
                }
            }
            else
            {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            }
            else
            {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext())
                {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null)
                    {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("UserName", "Nome de usuário já existe. Por favor, entre com um usuário diferente.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins()
        {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts)
            {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin
                {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult
        {
            public ExternalLoginResult(string provider, string returnUrl)
            {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
