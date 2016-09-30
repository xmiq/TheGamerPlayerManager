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
                switch (result)
                {
                    case LoginResult.Success:
                        FormsAuthentication.SetAuthCookie(u.Username, true);
                        //TODO: Fix Once Profile is Implemented
                        return RedirectToAction("");

                    case LoginResult.Failed:
                    case LoginResult.Locked:
                        ViewBag.Error = result;
                        return View(u);

                    default: return View(u);
                }
            }
            catch
            {
                return View(u);
            }
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
                switch (result)
                {
                    case LoginResult.Success:
                        FormsAuthentication.SetAuthCookie(u.Username, true);
                        //TODO: Fix Once Profile is Implemented
                        return RedirectToAction("");

                    case LoginResult.Failed:
                        ViewBag.Error = result;
                        return View(u);

                    default: return View(u);
                }
            }
            catch
            {
                return View(u);
            }
        }
    }
}