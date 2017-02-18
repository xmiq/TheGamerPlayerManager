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
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager mgr = new UserManager();

        //GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        //POST: Login
        [HttpPost, AllowAnonymous]
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
            return RedirectToAction(nameof(Login));
        }

        //GET: Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost, AllowAnonymous]
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
        [ActionName("Profile")]
        public ActionResult AccountProfile()
        {
            return View(mgr.GetUser(Token.Value));
        }

        //GET: UpdateProfile
        public ActionResult UpdateProfile()
        {
            return View(mgr.GetUser(Token.Value));
        }

        //POST: UpdateProfile
        [HttpPost]
        public ActionResult UpdateProfile(User u)
        {
            try
            {
                mgr.UpateUser(Token.Value, u);
                return RedirectToAction(nameof(AccountProfile).Replace("Account", ""));
            }
            catch
            {
                return View(u);
            }
        }

        //GET: ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //POST: ChangePassword
        [HttpPost]
        public ActionResult ChangePassword(string oldPassword, string newPassword, string confrimPassword)
        {
            try
            {
                if (!newPassword.Equals(confrimPassword))
                {
                    ModelState.AddModelError("", "New Password and Confirm Password must be identical");
                    return View();
                }
                else
                {
                    var user = mgr.GetUser(Token.Value);
                    mgr.ChangePassword(Token.Value, new Password { User = user, Value = oldPassword }, new Password { User = user, Value = newPassword });
                    return RedirectToAction(nameof(AccountProfile).Replace("Account", ""));
                }
            }
            catch (Exception e)
            {
                if (e.Message.EndsWith("Password is invalid."))
                    ModelState.AddModelError("", "Current Password is invalid");
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult ForgotPassword(string username)
        {
            mgr.ForgotPassword(username);
            return View();
        }

        public ActionResult ForgotPassword(int ID, Guid Token)
        {
            return View();
        }
    }
}