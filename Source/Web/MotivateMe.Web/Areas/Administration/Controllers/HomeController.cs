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
    public class HomeController : BaseController
    {
        

        public HomeController(IMotivateMeData data)
            : base(data)
        {
            
        }

        // GET: Administration/Home
        public ActionResult Navigation()
        {
            var stories = this.Data.Stories.All();

            return View(stories);
        }
    }
}