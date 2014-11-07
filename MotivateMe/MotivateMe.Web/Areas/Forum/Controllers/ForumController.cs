using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotivateMe.Web.Areas.Forum.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum/Forum
        public ActionResult Index()
        {
            return View();
        }
    }
}