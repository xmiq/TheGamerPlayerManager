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
    public class SkillStatsController : Controller
    {
        private DataManagement.Manager mgr = new DataManagement.Manager(ManagerClasses.Skill | ManagerClasses.SkillStats | ManagerClasses.User);

        // GET: Skills/5?username=myUser
        [AllowAnonymous]
        public ActionResult Index(int id, string username)
        {
            ViewBag.IsOwner = (User.Identity.IsAuthenticated) ? mgr.User.IsOwner(Token.Value, username) : false;
            return PartialView(mgr.SkillStats.GetSkillStats(id));
        }

        // GET: Skills/Create?username=myUser&player=5
        public ActionResult Create(int id, string username, int player)
        {
            ViewBag.Username = username;
            ViewBag.Player = player;
            //ViewBag.Skills = mgr.Skill.GetAllSkills();
            return View(new SkillStats { Chapter = new Chapter { ID = id }, Level = 1, EXP = 0 });
        }

        // POST: Skills/Create?username=myUser&player=5
        [HttpPost]
        public ActionResult Create(string username, int player, SkillStats ss)
        {
            try
            {
                // TODO: Add insert logic here
                mgr.SkillStats.CreateSkillStat(ss);
                return RedirectToAction(nameof(Manager.Controllers.PlayerController.Details), nameof(Manager.Controllers.PlayerController).Replace("Controller", ""), new { id = player, Username = username });
            }
            catch
            {
                ViewBag.Username = username;
                ViewBag.Player = player;
                //ViewBag.Skills = mgr.Skill.GetAllSkills();
                return View(ss);
            }
        }

        // GET: Skills/Edit/5?username=myUser&player=5
        public ActionResult Edit(int id, int player, string username)
        {
            ViewBag.Player = player;
            ViewBag.Username = username;
            return View(mgr.SkillStats.GetSkillStat(id));
        }

        // POST: Skills/Edit/5?username=myUser&player=5
        [HttpPost]
        public ActionResult Edit(int id, int player, string username, SkillStats ss)
        {
            try
            {
                // TODO: Add update logic here
                mgr.SkillStats.UpdateSkillStat(ss);
                return RedirectToAction(nameof(Manager.Controllers.PlayerController.Details), nameof(Manager.Controllers.PlayerController).Replace("Controller", ""), new { id = player, Username = username });
            }
            catch
            {
                ViewBag.Player = player;
                ViewBag.Username = username;
                return View(ss);
            }
        }

        // GET: Skills/Delete/5?username=myUser&player=5
        public ActionResult Delete(int id, int player, string username)
        {
            ViewBag.Player = player;
            ViewBag.Username = username;
            return View(mgr.SkillStats.GetSkillStat(id));
        }

        // POST: Skills/Delete/5?username=myUser&player=5
        [HttpPost]
        public ActionResult Delete(int id, int player, string username, SkillStats ss)
        {
            try
            {
                // TODO: Add delete logic here
                mgr.SkillStats.DeleteSkillStat(ss);
                return RedirectToAction(nameof(Manager.Controllers.PlayerController.Details), nameof(Manager.Controllers.PlayerController).Replace("Controller", ""), new { id = player, Username = username });
            }
            catch
            {
                ViewBag.Player = player;
                ViewBag.Username = username;
                return View(ss);
            }
        }

        // GET: Stats/AddXP/5?username=myUser&player=5
        public ActionResult AddXP(int id, int player, string username)
        {
            ViewBag.Player = player;
            ViewBag.Username = username;
            ViewBag.ID = id;
            return View(0);
        }

        // POST: Stats/AddXP/5?username=myUser&player=5
        [HttpPost]
        public ActionResult AddXP(int id, int player, string username, int XP)
        {
            try
            {
                mgr.SkillStats.AddXP(id, XP);
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