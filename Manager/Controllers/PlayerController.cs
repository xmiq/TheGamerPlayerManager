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
        private DataManagement.Manager mgr = new DataManagement.Manager(ManagerClasses.Player | ManagerClasses.Story | ManagerClasses.User);

        // GET: Player/5?username=MyUser
        [AllowAnonymous]
        public ActionResult Index(int id, string username)
        {
            ViewBag.Story = id;
            ViewBag.Username = username;
            ViewBag.IsOwner = (User.Identity.IsAuthenticated) ? mgr.User.IsOwner(Token.Value, username) : false;
            return View(mgr.Player.GetAllStoryPlayers(id, username));
        }

        // GET: Player/Details/5?username=MyUser&story=5
        [AllowAnonymous]
        public ActionResult Details(int id, string username, int story)
        {
            ViewBag.Story = story;
            ViewBag.Username = username;
            ViewBag.IsOwner = (User.Identity.IsAuthenticated) ? mgr.User.IsOwner(Token.Value, username) : false;
            return View(mgr.Player.GetPlayer(id));
        }

        // GET: Player/Create/MyUser&story=5
        [OwnerOnly]
        public ActionResult Create(string id, int story)
        {
            ViewBag.Username = id;
            return View(new Player { Story = new Story { ID = story } });
        }

        // POST: Player/Create/MyUser&story=5
        [HttpPost, OwnerOnly]
        public ActionResult Create(string id, Player player, int story)
        {
            try
            {
                mgr.Player.CreatePlayer(id, player);
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
            ViewBag.StoryList = mgr.Story.GetAllStories(username).Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString(), Selected = x.ID == story }).ToArray();
            return View(mgr.Player.GetPlayer(id));
        }

        // POST: Player/Edit/5?username=MyUser&story=5
        [HttpPost, OwnerOnly]
        public ActionResult Edit(int id, Player player, string username, int story)
        {
            try
            {
                player.Story = new Story { ID = story };
                mgr.Player.UpdatePlayer(username, player);
                return RedirectToAction(nameof(Index), new { id = story, username = username });
            }
            catch
            {
                ViewBag.Story = story;
                ViewBag.Username = username;
                ViewBag.StoryList = mgr.Story.GetAllStories(username).Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString(), Selected = x.ID == story }).ToArray();
                return View(player);
            }
        }

        // GET: Player/Delete/5?username=MyUser&story=5
        [OwnerOnly]
        public ActionResult Delete(int id, string username, int story)
        {
            ViewBag.Story = story;
            ViewBag.Username = username;
            return View(mgr.Player.GetPlayer(id));
        }

        // POST: Player/Delete/5?username=MyUser&story=5
        [HttpPost, OwnerOnly]
        public ActionResult Delete(int id, Player p, string username, int story)
        {
            try
            {
                mgr.Player.DeletePlayer(p);
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