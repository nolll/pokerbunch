using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Cache;
using Web.Controllers.Base;
using Web.Models.AdminModels;
using Web.Urls;

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
            var cacheContainer = new CacheContainer(new AspNetCacheProvider());
            var count = cacheContainer.ClearAll();

            var model = new ClearCacheModel(count);

            return View("ClearCache", model);
        }
    }
}
