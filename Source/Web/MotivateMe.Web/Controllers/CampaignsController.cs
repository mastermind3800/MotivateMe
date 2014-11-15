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
    public class CampaignsController : BaseController
    {
        public CampaignsController(IMotivateMeData data)
            : base(data)
        {
            this.Data = data;
        }

        // GET: Campaigns
        public ActionResult Index()
        {
            return View(this.Data.Campaigns.All().ToList());
        }

        // GET: Campaigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = this.Data.Campaigns.GetById(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // GET: Campaigns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,InitiatiorId,Title,Goal,Info,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                this.Data.Campaigns.Add(campaign);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(campaign);
        }

        // GET: Campaigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = this.Data.Campaigns.GetById(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Initiatior_Id,Title,Goal,Info,IsDeleted,DeletedOn,CreatedOn,ModifiedOn")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                this.Data.Campaigns.Update(campaign);
                this.Data.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(campaign);
        }

        // GET: Campaigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Campaign campaign = this.Data.Campaigns.GetById(id);
            if (campaign == null)
            {
                return HttpNotFound();
            }
            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campaign campaign = this.Data.Campaigns.GetById(id);
            this.Data.Campaigns.Delete(campaign);
            this.Data.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Data.Campaigns.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
