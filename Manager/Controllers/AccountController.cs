using Manager.DataManagement;
using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Manager.Controllers
{
    public class AccountController : Controller
    {
        private UserManager mgr = new UserManager();

        //GET: Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Login
        [HttpPost]
        public ActionResult Login(User u)
        {
            try
            {
                var result = mgr.Login(u);
                switch (result.Result)
                {
                    case LoginResult.Success:
                        FormsAuthentication.SetAuthCookie(result.Token.ToString(), true);
                        return RedirectToAction(nameof(AccountProfile).Replace("Account", ""));

                    case LoginResult.Failed:
                    case LoginResult.Locked:
                        ViewBag.Error = result.Result;
                        return View(u);

                    default: return View(u);
                }
            }
            catch
            {
                return View(u);
            }
        }

        //GET: Logoff
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("action");
        }

        //GET: Register
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        public ActionResult Register(User u)
        {
            try
            {
                var result = mgr.Register(u);
                switch (result.Result)
                {
                    case LoginResult.Success:
                        FormsAuthentication.SetAuthCookie(result.Token.ToString(), true);
                        return RedirectToAction(nameof(AccountProfile).Replace("Account", ""));

                    case LoginResult.Failed:
                        ViewBag.Error = result.Result;
                        return View(u);

                    default: return View(u);
                }
            }
            catch
            {
                return View(u);
            }
        }

        //GET: Profile
        [ActionName("Profile"), Authorize]
        public ActionResult AccountProfile()
        {
            var Token = Guid.Parse(FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name);
            return View(mgr.GetUser(Token));
        }
    }
}