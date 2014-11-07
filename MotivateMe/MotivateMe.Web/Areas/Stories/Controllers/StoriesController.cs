using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotivateMe.Web.Areas.Stories.Controllers
{
    public class StoriesController : Controller
    {
        // GET: Stories/Stories
        public ActionResult Index()
        {
            return View();
        }
    }
}