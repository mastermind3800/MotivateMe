using MotivateMe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotivateMe.Web.Areas.Administration.Controllers
{
    
        // [Authorize(Roles = GlobalConstants.AdministratorRoleName)] should be in MotivateMe.Common > GlobalConstants.cs
       // [Authorize(Roles = "Admin")]
       // [ValidateInput(false)]
        public abstract class BaseAdminController : Controller
        {
            protected ApplicationDbContext context = new ApplicationDbContext();
        }
   
}