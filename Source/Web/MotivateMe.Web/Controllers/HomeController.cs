  namespace MotivateMe.Web.Controllers
{
    using AutoMapper.QueryableExtensions;

    using MotivateMe.Data.Common.Repository;
    using MotivateMe.Data.Models;
    using MotivateMe.Web.ViewModels.Home;

    using System.Web.Mvc;
    using System.Linq;


    public class HomeController : Controller
    {
        private IRepository<Story> stories;

        public HomeController(IRepository<Story> stories)
        {
            this.stories = stories;
        }

        public ActionResult Index()
        {
            var stories = this.stories.All()
                .OrderByDescending(s => s.CreatedOn)
                .Take(6)
                .Project()
                .To<IndexStoryViewModel>()
                .ToList();

            return View(stories);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}