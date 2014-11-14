using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MotivateMe.Data;
using MotivateMe.Data.Models;

namespace MotivateMe.Web.Controllers
{
    public class StoriesController : BaseController
    {
        public StoriesController(IMotivateMeData data)
            : base(data)
        {
            this.Data = data;
        }

        // GET: Stories
        public ActionResult Index()
        {
            var stories = this.Data.Stories.All().Include(s => s.Author);
            return View(stories.ToList());
        }

        // GET: Stories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = this.Data.Stories.GetById(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // GET: Stories/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email");
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,StoryContent,AuthorId")] Story story)
        {
            if (ModelState.IsValid)
            {
                story.IsDeleted = false;
                story.CreatedOn = DateTime.Now;

                this.Data.Stories.Add(story);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", story.AuthorId);
            return View(story);
        }

        // GET: Stories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = this.Data.Stories.GetById(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", story.AuthorId);
            return View(story);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,StoryContent,AuthorId")] Story story)
        {
            if (ModelState.IsValid)
            {
                story.ModifiedOn = DateTime.Now;
                this.Data.Stories.Update(story);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", story.AuthorId);
            return View(story);
        }

        // GET: Stories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = this.Data.Stories.GetById(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Story story = this.Data.Stories.GetById(id);
            this.Data.Stories.Delete(story);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Data.Stories.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
