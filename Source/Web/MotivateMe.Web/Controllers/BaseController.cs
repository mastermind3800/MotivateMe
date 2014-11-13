using MotivateMe.Data;
using MotivateMe.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotivateMe.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController(IMotivateMeData data)
        {
            this.Data = data;
        }

        protected IMotivateMeData Data { get; set; }

        protected ApplicationUser CurrentUser { get; set; }
    }
}