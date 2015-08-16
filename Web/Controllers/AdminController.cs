using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.AdminModels;

namespace Web.Controllers
{
    public class AdminController : BaseController
    {
        [Route(Routes.AdminSendEmail)]
        public ActionResult SendEmail()
        {
            var result = UseCase.TestEmail.Execute(new TestEmail.Request(CurrentUserName));

            var model = new EmailModel(result);

            return View("Email", model);
        }

        [Route(Routes.AdminClearCache)]
        public ActionResult ClearCache()
        {
            var result = UseCase.ClearCache.Execute(new ClearCache.Request(CurrentUserName));

            var model = new ClearCacheModel(result);

            return View("ClearCache", model);
        }
    }
}
