using Manager.DataManagement;
using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manager.Controllers
{
    public class ChapterController : Controller
    {
        private ChapterManager mgr = new ChapterManager();

        // GET: Chapter
        public ActionResult Index(int id)
        {
            return PartialView(mgr.GetAllChapters(id));
        }

        // GET: Chapter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Chapter/Create
        public ActionResult Create(int id, string username)
        {
            ViewBag.ID = id;
            ViewBag.Username = username;
            return View();
        }

        // POST: Chapter/Create
        [HttpPost]
        public ActionResult Create(string username, Chapter c)
        {
            try
            {
                mgr.CreateChapter(c);
                return RedirectToAction(nameof(Manager.Controllers.PlayerController.Details), nameof(Manager.Controllers.PlayerController).Replace("Controller", ""), new { id = c.Player.ID, username = username });
            }
            catch
            {
                ViewBag.ID = c.Player.ID;
                ViewBag.Username = username;
                return View(c);
            }
        }
    }
}