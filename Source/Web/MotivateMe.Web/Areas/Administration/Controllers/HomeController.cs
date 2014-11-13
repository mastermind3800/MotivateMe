using MotivateMe.Data;
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
        private IMotivateMeData data;

        public HomeController()
        {
            
        }

        // GET: Administration/Home
        public ActionResult Navigation()
        {
            var stories = this.data.Stories.All();

            return View(stories);
        }
    }
}