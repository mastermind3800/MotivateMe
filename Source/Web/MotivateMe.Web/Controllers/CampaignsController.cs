namespace MotivateMe.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using MotivateMe.Data;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.ViewModels.Campaigns;
    using MotivateMe.Web.ViewModels.Comments;
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    public class CampaignsController : BaseController
    {
        public CampaignsController(IMotivateMeData data)
            : base(data)
        {
           
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

            // Campaign campaign = this.Data.Campaigns.GetById(id);
            var campaign = this.Data
                .Campaigns
                .All()
                .Where(c => c.Id == id)
                .Project()
                .To<CampaignDetailsViewModel>()
                .FirstOrDefault();

            if (campaign == null)
            {
                return HttpNotFound("Campaign Not Found");
            }

            campaign.Comments = this.Data
                .Comments
                .All()
                .Where(c => c.CampaignId == campaign.Id)
                .OrderByDescending(c=>c.CreatedOn)
                .Project()
                .To<CommentViewModel>()
                .ToList();

            return View(campaign);
        }

        // GET: Campaigns/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCampaignViewModel campaign)
        {
            if (campaign != null && ModelState.IsValid)
            {
                var dbCampaign = Mapper.Map<Campaign>(campaign);
                dbCampaign.AuthorId = this.CurrentUser.Id;
                dbCampaign.CreatedOn = DateTime.Now;
                
                this.Data.Campaigns.Add(dbCampaign);
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
