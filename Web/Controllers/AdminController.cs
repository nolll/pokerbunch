using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.AdminModels;

namespace Web.Controllers
{
    public class AdminController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Admin.SendEmail)]
        public ActionResult SendEmail()
        {
            var result = UseCase.TestEmail.Execute(new TestEmail.Request(Identity.UserName));

            var model = new EmailModel(result);

            return View("Email", model);
        }

        [Authorize]
        [Route(WebRoutes.Admin.ClearCache)]
        public ActionResult ClearCache()
        {
            var result = UseCase.ClearCache.Execute();
            var model = new ClearCacheModel(result.ClearCount);

            return View("ClearCache", model);
        }
    }
}
