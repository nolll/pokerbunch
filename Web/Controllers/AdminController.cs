using System.Web.Mvc;
using Core.Urls;
using Web.Controllers.Base;
using Web.Models.AdminModels;

namespace Web.Controllers
{
    public class AdminController : BaseController
    {
        [Route(Routes.AdminSendEmail)]
        public ActionResult SendEmail()
        {
            RequireAdmin(GetAppContext());
            var result = UseCase.TestEmail.Execute();

            var model = new EmailModel(result);

            return View("Email", model);
        }

        [Route(Routes.AdminClearCache)]
        public ActionResult ClearCache()
        {
            RequireAdmin(GetAppContext());
            var result = UseCase.ClearCache.Execute();

            var model = new ClearCacheModel(result);

            return View("ClearCache", model);
        }
    }
}
