namespace MotivateMe.Web.Areas.Administration.Controllers
{
    using MotivateMe.Data;
    using MotivateMe.Data.Common;
    using MotivateMe.Web.Controllers;

    using System.Web.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [ValidateInput(false)]
    public abstract class BaseAdminController : BaseController
    {
        public BaseAdminController(IMotivateMeData data)
            :base(data)
        {

        }
    }

}