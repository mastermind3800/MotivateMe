using MotivateMe.Data;
using MotivateMe.Data.Common.Repository;
using MotivateMe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using MotivateMe.Web.ViewModels.Home;


namespace MotivateMe.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Story> stories;
       
        public HomeController(IRepository<Story> stories)
        {
            this.stories = stories;
        }

        public ActionResult Index()
        {
            var stories = this.stories.All().Project().To<IndexStoryViewModel>();

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