using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manager.Controllers
{
    public class HomeController : Controller
    {
        private DataManagement.Manager mgr = new DataManagement.Manager(DataManagement.ManagerClasses.Story | DataManagement.ManagerClasses.Player);

        public ActionResult Index()
        {
            ViewBag.Stories = mgr.Story.GetRandomStories();
            ViewBag.Players = mgr.Player.GetRandomPlayers();
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}