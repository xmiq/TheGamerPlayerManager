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
    public class PlayerController : Controller
    {
        private PlayerManager mgr = new PlayerManager();
        private UserManager umgr = new UserManager();

        // GET: Player/MyUser
        [AllowAnonymous]
        public ActionResult Index(string id)
        {
            ViewBag.Username = id;
            ViewBag.IsOwner = (User.Identity.IsAuthenticated) ? umgr.IsOwner(Token.Value, id) : false;
            return View(mgr.GetAllPlayers(id));
        }

        // GET: Player/Details/5?username=MyUser
        [AllowAnonymous]
        public ActionResult Details(int id, string username)
        {
            ViewBag.Username = username;
            ViewBag.IsOwner = (User.Identity.IsAuthenticated) ? umgr.IsOwner(Token.Value, username) : false;
            return View(mgr.GetPlayer(id));
        }

        // GET: Player/Create/MyUser
        [OwnerOnly]
        public ActionResult Create(string id)
        {
            ViewBag.Username = id;
            return View();
        }

        // POST: Player/Create/MyUser
        [HttpPost, OwnerOnly]
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

        // GET: Player/Edit/5?username=MyUser
        [OwnerOnly]
        public ActionResult Edit(int id, string username)
        {
            ViewBag.Username = username;
            return View(mgr.GetPlayer(id));
        }

        // POST: Player/Edit/5?username=MyUser
        [HttpPost, OwnerOnly]
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

        // GET: Player/Delete/5?username=MyUser
        [OwnerOnly]
        public ActionResult Delete(int id, string username)
        {
            ViewBag.Username = username;
            return View(mgr.GetPlayer(id));
        }

        // POST: Player/Delete/5?username=MyUser
        [HttpPost, OwnerOnly]
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