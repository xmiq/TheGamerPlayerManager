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
    public class ChapterController : Controller
    {
        private ChapterManager mgr = new ChapterManager();
        private UserManager umgr = new UserManager();

        // GET: Chapter/5?username=MyUser
        [AllowAnonymous]
        public ActionResult Index(int id, string username)
        {
            ViewBag.IsOwner = (User.Identity.IsAuthenticated) ? umgr.IsOwner(Token.Value, username) : false;
            return PartialView(mgr.GetAllChapters(id));
        }

        // GET: Chapter/Create/5?username=MyUser
        public ActionResult Create(int id, string username)
        {
            ViewBag.ID = id;
            ViewBag.Username = username;
            return View(mgr.GetNextChapter(id));
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