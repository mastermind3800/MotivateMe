using MotivateMe.Data;
using MotivateMe.Data.Common.Repository;
using MotivateMe.Data.Models;
using MotivateMe.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotivateMe.Web.Areas.Administration.Controllers
{
    public class HomeController : BaseAdminController
    {
        

        public HomeController(IMotivateMeData data)
            : base(data)
        {
            
        }
        public ActionResult Index()
        {
            var stories = this.Data.Stories.All();

            return View(stories);
        }

        // GET: Administration/Home
        public ActionResult Stories()
        {
            var stories = this.Data.Stories.All();

            return View(stories);
        }
    }
}