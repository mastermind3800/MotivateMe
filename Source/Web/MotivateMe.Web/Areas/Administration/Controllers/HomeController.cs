using MotivateMe.Data.Common.Repository;
using MotivateMe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotivateMe.Web.Areas.Administration.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Story> stories;

        public HomeController(IRepository<Story> stories)
        {
            this.stories = stories;
        }

        // GET: Administration/Home
        public ActionResult Navigation()
        {
            var stories = this.stories.All();

            return View(stories);
        }
    }
}