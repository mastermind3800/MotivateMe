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
    public class TipsController : BaseController
    {
        public TipsController(IMotivateMeData data)
            : base(data)
        {
            
        }

        // GET: Tips
        public ActionResult Index()
        {
            var tips = this.Data.Tips.All().Include(t => t.Author);
            return View(tips.ToList());
        }

        // GET: Tips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = this.Data.Tips.GetById(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            return View(tip);
        }

        // GET: Tips/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email");
            return View();
        }

        // POST: Tips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,AuthorId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                this.Data.Tips.Add(tip);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", tip.AuthorId);
            return View(tip);
        }

        // GET: Tips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = this.Data.Tips.GetById(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", tip.AuthorId);
            return View(tip);
        }

        // POST: Tips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,AuthorId,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                this.Data.Tips.Update(tip);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", tip.AuthorId);
            return View(tip);
        }

        // GET: Tips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = this.Data.Tips.GetById(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            return View(tip);
        }

        // POST: Tips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tip tip = this.Data.Tips.GetById(id);
            this.Data.Tips.Delete(tip);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Data.Tips.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
