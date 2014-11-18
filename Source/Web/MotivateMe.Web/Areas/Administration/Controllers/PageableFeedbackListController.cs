namespace MotivateMe.Web.Areas.Administration.Controllers
{
    using AutoMapper.QueryableExtensions;

    using MotivateMe.Data;
    using MotivateMe.Web.Areas.Administration.ViewModels;
    using System.Linq;
    using System.Web.Mvc;

    public class PageableFeedbackListController : BaseAdminController
    {
        public PageableFeedbackListController(IMotivateMeData data)
            : base(data)
        {
            
        }

        private int PageSize = 4;

        // GET: PageableFeedbackList
        public ActionResult ViewAll(int page = 1)
        {
            var feedbacks = this.Data.Feedbacks
                .All()
                .OrderByDescending(f => f.CreatedOn)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .Project()
                .To<PageableFeedbackListViewModel>()
                .ToList();

            int feedbacksCount = this.Data.Feedbacks.All().Count();
            int pagesCount = ((feedbacksCount - 1) / PageSize) + 1;
            ViewBag.pagesCount = pagesCount;
            ViewBag.currentPage = page;

            return View(feedbacks);
        }
    }
}