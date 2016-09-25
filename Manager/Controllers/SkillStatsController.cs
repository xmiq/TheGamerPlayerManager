using Manager.DataManagement;
using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manager.Controllers
{
    public class SkillStatsController : Controller
    {
        private SkillStatsManager mgr = new SkillStatsManager();
        private SkillManager skmgr = new SkillManager();

        // GET: Skills
        public ActionResult Index(int id)
        {
            return PartialView(mgr.GetSkillStats(id));
        }

        // GET: Skills/Create
        public ActionResult Create(int id, string username, int player)
        {
            ViewBag.Username = username;
            ViewBag.Player = player;
            ViewBag.Skills = skmgr.GetAllSkills();
            return View(new SkillStats { Chapter = new Chapter { ID = id }, Level = 1, EXP = 0 });
        }

        // POST: Skills/Create
        [HttpPost]
        public ActionResult Create(string username, int player, SkillStats ss)
        {
            try
            {
                // TODO: Add insert logic here
                mgr.CreateSkillStat(ss);
                return RedirectToAction(nameof(Manager.Controllers.PlayerController.Details), nameof(Manager.Controllers.PlayerController).Replace("Controller", ""), new { id = player, Username = username });
            }
            catch
            {
                ViewBag.Username = username;
                ViewBag.Player = player;
                ViewBag.Skills = skmgr.GetAllSkills();
                return View(ss);
            }
        }

        // GET: Skills/Edit/5
        public ActionResult Edit(int id, int player, string username)
        {
            ViewBag.Player = player;
            ViewBag.Username = username;
            return View(mgr.GetSkillStat(id));
        }

        // POST: Skills/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, int player, string username, SkillStats ss)
        {
            try
            {
                // TODO: Add update logic here
                mgr.UpdateSkillStat(ss);
                return RedirectToAction(nameof(Manager.Controllers.PlayerController.Details), nameof(Manager.Controllers.PlayerController).Replace("Controller", ""), new { id = player, Username = username });
            }
            catch
            {
                ViewBag.Player = player;
                ViewBag.Username = username;
                return View(ss);
            }
        }

        // GET: Skills/Delete/5
        public ActionResult Delete(int id, int player, string username)
        {
            ViewBag.Player = player;
            ViewBag.Username = username;
            return View(mgr.GetSkillStat(id));
        }

        // POST: Skills/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, int player, string username, SkillStats ss)
        {
            try
            {
                // TODO: Add delete logic here
                mgr.DeleteSkillStat(ss);
                return RedirectToAction(nameof(Manager.Controllers.PlayerController.Details), nameof(Manager.Controllers.PlayerController).Replace("Controller", ""), new { id = player, Username = username });
            }
            catch
            {
                ViewBag.Player = player;
                ViewBag.Username = username;
                return View(ss);
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