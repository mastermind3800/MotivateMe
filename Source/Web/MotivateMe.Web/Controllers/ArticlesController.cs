namespace MotivateMe.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using MotivateMe.Data;
    using MotivateMe.Data.Common;
    using MotivateMe.Data.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class ArticlesController : BaseController
    {
        public ArticlesController(IMotivateMeData data)
            : base(data)
        {
            
        }

        public ActionResult Index()
        {
            var articles = this.Data
                .Articles
                .All()
                .Include(a => a.Author);

            return View(articles.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = this.Data.Articles.GetById(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            return View(article);
        }

        public ActionResult Create()
        {
            //ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,AuthorId,Title,Content")] Article article)
        {
            if (ModelState.IsValid)
            {
                article.CreatedOn = DateTime.Now;
                article.IsDeleted = false;
                article.AuthorId = this.User.Identity.GetUserId();

                this.Data.Articles.Add(article);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", article.AuthorId);
            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = this.Data.Articles.GetById(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            var articleAuthorId = article.AuthorId;
            if (articleAuthorId != this.User.Identity.GetUserId() && !this.User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                TempData["Error"] = "You are not allowed to edit an article which is not yours!";
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", article.AuthorId);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AuthorId,Title,Content,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Article article)
        {
            if (ModelState.IsValid)
            {
                this.Data.Articles.Update(article);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(this.Data.Users.All(), "Id", "Email", article.AuthorId);
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = this.Data.Articles.GetById(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = this.Data.Articles.GetById(id);
            this.Data.Articles.Delete(article);

            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Data.Articles.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
