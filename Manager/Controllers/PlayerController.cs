using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manager.Controllers
{
    public class PlayerController : Controller
    {
        private DataManagement.PlayerManager mgr = new DataManagement.PlayerManager();

        // GET: Player
        public ActionResult Index(string id)
        {
            ViewBag.Username = id;
            return View(mgr.GetAllPlayers(id));
        }

        // GET: Player/Details/5
        public ActionResult Details(int id, string username)
        {
            ViewBag.Username = username;
            return View(mgr.GetPlayer(id));
        }

        // GET: Player/Create
        public ActionResult Create(string id)
        {
            ViewBag.Username = id;
            return View();
        }

        // POST: Player/Create
        [HttpPost]
        public ActionResult Create(string id, Player player)
        {
            try
            {
                mgr.CreatePlayer(id, player);
                return RedirectToAction(nameof(Index), new { id = id });
            }
            catch
            {
                ViewBag.Username = id;
                return View(player);
            }
        }

        // GET: Player/Edit/5
        public ActionResult Edit(int id, string username)
        {
            ViewBag.Username = username;
            return View(mgr.GetPlayer(id));
        }

        // POST: Player/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Player player, string username)
        {
            try
            {
                mgr.UpdatePlayer(username, player);
                return RedirectToAction(nameof(Index), new { id = username });
            }
            catch
            {
                ViewBag.Username = username;
                return View(player);
            }
        }

        // GET: Player/Delete/5
        public ActionResult Delete(int id, string username)
        {
            ViewBag.Username = username;
            return View(mgr.GetPlayer(id));
        }

        // POST: Player/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Player p, string username)
        {
            try
            {
                mgr.DeletePlayer(p);
                return RedirectToAction(nameof(Index), new { id = username });
            }
            catch
            {
                ViewBag.Username = username;
                return View(p);
            }
        }
    }
}