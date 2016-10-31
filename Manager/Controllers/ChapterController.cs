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
        private DataManagement.Manager mgr = new DataManagement.Manager(ManagerClasses.Chapter | ManagerClasses.User);

        // GET: Chapter/5?username=MyUser
        [AllowAnonymous]
        public ActionResult Index(int id, string username)
        {
            ViewBag.IsOwner = (User.Identity.IsAuthenticated) ? mgr.User.IsOwner(Token.Value, username) : false;
            return PartialView(mgr.Chapter.GetAllChapters(id));
        }

        // GET: Chapter/Create/5?username=MyUser&story=5
        public ActionResult Create(int id, string username, int story)
        {
            ViewBag.ID = id;
            ViewBag.Username = username;
            ViewBag.Story = story;
            return View(mgr.Chapter.GetNextChapter(id));
        }

        // POST: Chapter/Create?username=MyUser&story=5
        [HttpPost]
        public ActionResult Create(string username, Chapter c, int story)
        {
            try
            {
                mgr.Chapter.CreateChapter(c);
                return RedirectToAction(nameof(Manager.Controllers.PlayerController.Details), nameof(Manager.Controllers.PlayerController).Replace("Controller", ""), new { id = c.Player.ID, username = username, story = story });
            }
            catch
            {
                ViewBag.ID = c.Player.ID;
                ViewBag.Username = username;
                ViewBag.Story = story;
                return View(c);
            }
        }
    }
}