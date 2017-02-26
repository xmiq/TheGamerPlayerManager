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
    public class StoryController : Controller
    {
        private Manager.DataManagement.Manager mgr = new DataManagement.Manager(ManagerClasses.Story | ManagerClasses.User);

        // GET: Story
        public ActionResult Index()
        {
            return PartialView(mgr.Story.GetAllStories());
        }

        // GET: Story
        [ChildActionOnly]
        public ActionResult MyIndex(string username)
        {
            ViewBag.Username = username;
            return PartialView(mgr.Story.GetAllStories(username));
        }

        // GET: Story/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id, string username)
        {
            ViewBag.Username = username;
            ViewBag.Owner = (Token.Value != Guid.Empty) ? mgr.User.IsOwner(Token.Value, mgr.User.GetUser(Token.Value).Username) : false;
            return View(mgr.Story.GetStory(id));
        }

        // GET: Story/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Story/Create
        [HttpPost]
        public ActionResult Create(Story s)
        {
            try
            {
                s.User = mgr.User.GetUser(Token.Value);
                mgr.Story.CreateStory(s);
                return RedirectToAction(nameof(AccountController.AccountProfile).Replace("Account", ""), nameof(AccountController).Replace("Controller", ""));
            }
            catch (Exception e)
            {
                ViewBag.Exception = e;
                return View(s);
            }
        }

        // GET: Story/Edit/5
        public ActionResult Edit(int id)
        {
            return View(mgr.Story.GetStory(id));
        }

        // POST: Story/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Story s)
        {
            try
            {
                s.ID = id;
                s.User = mgr.User.GetUser(Token.Value);
                mgr.Story.UpdateStory(s);
                return RedirectToAction(nameof(AccountController.AccountProfile).Replace("Account", ""), nameof(AccountController).Replace("Controller", ""));
            }
            catch
            {
                return View(s);
            }
        }

        // GET: Story/Delete/5
        public ActionResult Delete(int id)
        {
            return View(mgr.Story.GetStory(id));
        }

        // POST: Story/Delete/5
        [HttpPost]
        public ActionResult Delete(Story s)
        {
            try
            {
                mgr.Story.DeleteStory(s.ID);
                return RedirectToAction(nameof(AccountController.AccountProfile).Replace("Account", ""), nameof(AccountController).Replace("Controller", ""));
            }
            catch
            {
                return View(s);
            }
        }
    }
}