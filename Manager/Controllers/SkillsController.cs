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
    [Authorize]
    public class SkillsController : Controller
    {
        private SkillManager mgr = new SkillManager();

        // GET: Skills
        public ActionResult Index(int story)
        {
            ViewBag.Story = story;
            return View(mgr.GetAllSkills(story));
        }

        // GET: Skills/Details/5
        public ActionResult Details(int id, int story)
        {
            ViewBag.Story = story;
            return View(mgr.GetSkill(id));
        }

        // GET: Skills/Create
        public ActionResult Create(int story)
        {
            return View(new Skill { Story = story });
        }

        // POST: Skills/Create
        [HttpPost]
        public ActionResult Create(Skill s)
        {
            try
            {
                mgr.CreateSkill(s);
                return RedirectToAction(nameof(Index), new { story = s.Story });
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
                return View(s);
            }
        }

        // GET: Skills/Edit/5
        public ActionResult Edit(int id)
        {
            return View(mgr.GetSkill(id));
        }

        // POST: Skills/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Skill s)
        {
            try
            {
                mgr.UpdateSkill(s);
                return RedirectToAction(nameof(Index), new { story = s.Story });
            }
            catch
            {
                return View(s);
            }
        }

        // GET: Skills/Delete/5
        public ActionResult Delete(int id)
        {
            return View(mgr.GetSkill(id));
        }

        // POST: Skills/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Skill s)
        {
            try
            {
                mgr.DeleteSkill(id);
                return RedirectToAction(nameof(Index), new { story = s.Story });
            }
            catch
            {
                return View();
            }
        }
    }
}