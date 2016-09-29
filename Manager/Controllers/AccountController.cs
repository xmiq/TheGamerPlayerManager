using Manager.DataManagement;
using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                mgr.Login(u);
                //TODO: Fix Once Profile is Implemented
                return RedirectToAction("");
            }
            catch
            {
                return View(u);
            }
        }
    }
}