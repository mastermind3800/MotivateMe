namespace MotivateMe.Web.Controllers
{
    using AutoMapper.QueryableExtensions;

    using MotivateMe.Data.Common.Repository;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.ViewModels.Home;
    using MotivateMe.Data;

    using System.Linq;
    using System.Web.Mvc;


    public class HomeController : BaseController
    {
        public HomeController(IMotivateMeData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            var stories = this.Data.Stories.All()
                .OrderByDescending(s => s.CreatedOn)
                .Take(6)
                .Project()
                .To<IndexStoryViewModel>()
                .ToList();

            var campaigns= this.Data.Campaigns.All()
                .OrderByDescending(s => s.CreatedOn)
                .Take(6)
                .Project()
                .To<IndexCampaignViewModel>()
                .ToList();

            var articles = this.Data.Articles.All()
                .OrderByDescending(s => s.CreatedOn)
                .Take(6)
                .Project()
                .To<IndexArticleViewModel>()
                .ToList();

            var tips = this.Data.Tips.All()
                .OrderByDescending(s => s.CreatedOn)
                .Take(6)
                .Project()
                .To<IndexTipViewModel>()
                .ToList();

            ViewBag.Campaigns = campaigns;
            ViewBag.Stories = stories;
            ViewBag.Tips = tips;
            ViewBag.Articles = articles;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}