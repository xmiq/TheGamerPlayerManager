using Manager.DataManagement;
using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manager.Controllers
{
    public class StatsController : Controller
    {
        private StatsManager mgr = new StatsManager();

        // GET: Stats/Details/5
        public ActionResult Details(int id)
        {
            return PartialView(mgr.GetStats(id));
        }

        // GET: Stats/Edit/5
        public ActionResult Edit(int id, int player, string username)
        {
            ViewBag.Player = player;
            ViewBag.Username = username;
            return View(mgr.GetStats(id));
        }

        // POST: Stats/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, int player, string username, Stats s)
        {
            try
            {
                mgr.UpdateStats(s);
                return RedirectToAction(nameof(PlayerController.Details), nameof(PlayerController).Replace("Controller", ""), new { id = player, username = username });
            }
            catch
            {
                ViewBag.Player = player;
                ViewBag.Username = username;
                return View(s);
            }
        }

        // GET: Stats/AddXP/5
        public ActionResult AddXP(int id, int player, string username)
        {
            ViewBag.Player = player;
            ViewBag.Username = username;
            ViewBag.ID = id;
            return View(0);
        }

        // POST: Stats/AddXP/5
        [HttpPost]
        public ActionResult AddXP(int id, int player, string username, int XP)
        {
            try
            {
                mgr.AddXP(id, XP);
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