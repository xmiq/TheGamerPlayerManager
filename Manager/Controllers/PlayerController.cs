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

        // GET: Player/5?username=MyUser
        [AllowAnonymous]
        public ActionResult Index(int id, string username)
        {
            ViewBag.Story = id;
            ViewBag.Username = username;
            ViewBag.IsOwner = (User.Identity.IsAuthenticated) ? umgr.IsOwner(Token.Value, username) : false;
            return View(mgr.GetAllStoryPlayers(id, username));
        }

        // GET: Player/Details/5?username=MyUser&story=5
        [AllowAnonymous]
        public ActionResult Details(int id, string username, int story)
        {
            ViewBag.Story = story;
            ViewBag.Username = username;
            ViewBag.IsOwner = (User.Identity.IsAuthenticated) ? umgr.IsOwner(Token.Value, username) : false;
            return View(mgr.GetPlayer(id));
        }

        // GET: Player/Create/MyUser&story=5
        [OwnerOnly]
        public ActionResult Create(string id, int story)
        {
            ViewBag.Story = story;
            ViewBag.Username = id;
            return View();
        }

        // POST: Player/Create/MyUser&story=5
        [HttpPost, OwnerOnly]
        public ActionResult Create(string id, Player player, int story)
        {
            try
            {
                mgr.CreatePlayer(id, player);
                return RedirectToAction(nameof(Index), new { id = story, username = id });
            }
            catch
            {
                ViewBag.Story = story;
                ViewBag.Username = id;
                return View(player);
            }
        }

        // GET: Player/Edit/5?username=MyUser&story=5
        [OwnerOnly]
        public ActionResult Edit(int id, string username, int story)
        {
            ViewBag.Story = story;
            ViewBag.Username = username;
            return View(mgr.GetPlayer(id));
        }

        // POST: Player/Edit/5?username=MyUser&story=5
        [HttpPost, OwnerOnly]
        public ActionResult Edit(int id, Player player, string username, int story)
        {
            try
            {
                mgr.UpdatePlayer(username, player);
                return RedirectToAction(nameof(Index), new { id = story, username = username });
            }
            catch
            {
                ViewBag.Username = username;
                return View(player);
            }
        }

        // GET: Player/Delete/5?username=MyUser&story=5
        [OwnerOnly]
        public ActionResult Delete(int id, string username, int story)
        {
            ViewBag.Story = story;
            ViewBag.Username = username;
            return View(mgr.GetPlayer(id));
        }

        // POST: Player/Delete/5?username=MyUser&story=5
        [HttpPost, OwnerOnly]
        public ActionResult Delete(int id, Player p, string username, int story)
        {
            try
            {
                mgr.DeletePlayer(p);
                return RedirectToAction(nameof(Index), new { id = story, username = username });
            }
            catch
            {
                ViewBag.Story = story;
                ViewBag.Username = username;
                return View(p);
            }
        }
    }
}