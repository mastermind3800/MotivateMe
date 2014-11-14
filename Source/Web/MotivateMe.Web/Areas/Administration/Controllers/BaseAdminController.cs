using MotivateMe.Data;
using MotivateMe.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MotivateMe.Web.Areas.Administration.Controllers
{
    
        // [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        // [ValidateInput(false)]
        public abstract class BaseAdminController : Controller
        {
            protected ApplicationDbContext context = new ApplicationDbContext();
        }
   
}