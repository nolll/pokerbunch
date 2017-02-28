using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.AdminModels;
using Web.Routes;

namespace Web.Controllers
{
    public class AdminController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Admin.SendEmail)]
        public ActionResult SendEmail()
        {
            var result = UseCase.TestEmail.Execute();

            var model = new EmailModel(result);

            return View("Email", model);
        }

        [Authorize]
        [Route(WebRoutes.Admin.ClearCache)]
        public ActionResult ClearCache()
        {
            var result = UseCase.ClearCache.Execute();
            var model = new ClearCacheModel(result.Message);

            return View("ClearCache", model);
        }
    }
}
