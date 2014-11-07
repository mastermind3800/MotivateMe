using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotivateMe.Web.Areas.Articles.Controllers
{
    public class ArticlesController : Controller
    {
        // GET: Articles/Articles
        public ActionResult Index()
        {
            return View();
        }
    }
}