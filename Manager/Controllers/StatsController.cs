using Manager.DataManagement;
using Manager.ManagerAttributes;
using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manager.Controllers
{
    [OwnerOnly]
    public class StatsController : Controller
    {
        private DataManagement.Manager mgr = new DataManagement.Manager(ManagerClasses.Stats | ManagerClasses.User);

        // GET: Stats/Details/5?username=myUser
        [AllowAnonymous]
        public ActionResult Details(int id, string username)
        {
            ViewBag.IsOwner = (User.Identity.IsAuthenticated) ? mgr.User.IsOwner(Token.Value, username) : false;
            return PartialView(mgr.Stats.GetStats(id));
        }

        // GET: Stats/Edit/5?player=5&username=myUser
        public ActionResult Edit(int id, int player, string username)
        {
            ViewBag.Player = player;
            ViewBag.Username = username;
            return View(mgr.Stats.GetStats(id));
        }

        // POST: Stats/Edit/5?player=5&username=myUser
        [HttpPost]
        public ActionResult Edit(int id, int player, string username, Stats s)
        {
            try
            {
                mgr.Stats.UpdateStats(s);
                return RedirectToAction(nameof(PlayerController.Details), nameof(PlayerController).Replace("Controller", ""), new { id = player, username = username });
            }
            catch
            {
                ViewBag.Player = player;
                ViewBag.Username = username;
                return View(s);
            }
        }

        // GET: Stats/AddXP/5?player=5&username=myUser
        public ActionResult AddXP(int id, int player, string username)
        {
            ViewBag.Player = player;
            ViewBag.Username = username;
            ViewBag.ID = id;
            return View(0);
        }

        // POST: Stats/AddXP/5?player=5&username=myUser
        [HttpPost]
        public ActionResult AddXP(int id, int player, string username, int XP)
        {
            try
            {
                mgr.Stats.AddXP(id, XP);
                return RedirectToAction(nameof(PlayerController.Details), nameof(PlayerController).Replace("Controller", ""), new { id = player, username = username });
            }
            catch
            {
                ViewBag.Player = player;
                ViewBag.Username = username;
                ViewBag.ID = id;
                return View(XP);
            }
        }
    }
}