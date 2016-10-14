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
    public class StoryController : Controller
    {
        private StoryManager mgr = new StoryManager();

        // GET: Story
        public ActionResult Index()
        {
            return PartialView(mgr.GetAllStories());
        }

        // GET: Story/Details/5
        public ActionResult Details(int id)
        {
            return View(mgr.GetStory(id));
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
                mgr.CreateStory(s);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(s);
            }
        }

        // GET: Story/Edit/5
        public ActionResult Edit(int id)
        {
            return View(mgr.GetStory(id));
        }

        // POST: Story/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Story s)
        {
            try
            {
                s.ID = id;
                mgr.UpdateStory(s);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(s);
            }
        }

        // GET: Story/Delete/5
        public ActionResult Delete(int id)
        {
            return View(mgr.GetStory(id));
        }

        // POST: Story/Delete/5
        [HttpPost]
        public ActionResult Delete(Story s)
        {
            try
            {
                mgr.DeleteStory(s.ID);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(s);
            }
        }
    }
}