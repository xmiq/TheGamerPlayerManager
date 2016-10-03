using Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Manager.Controllers
{
    public class StoryController : Controller
    {
        // GET: Story
        public ActionResult Index()
        {
            return View();
        }

        // GET: Story/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Story/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Story/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Story s)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Story/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Story/Delete/5
        [HttpPost]
        public ActionResult Delete(Story s)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}