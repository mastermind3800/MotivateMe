using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotivateMe.Web.Areas.Administration.Controllers
{
    public class StoriesController : AdminController
    {
        // GET: Administration/Stories
        public ActionResult Index()
        {
            return View();
        }
    }
}