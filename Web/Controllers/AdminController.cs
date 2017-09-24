using System.Web.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.AdminModels;

namespace Web.Controllers
{
    public class AdminController : BaseController
    {
        [Authorize]
        [Route(TestEmailUrl.Route)]
        public ActionResult SendEmail()
        {
            var result = UseCase.TestEmail.Execute();
            var model = new EmailModel(result);
            return View(model);
        }

        [Authorize]
        [Route(ClearCacheUrl.Route)]
        public ActionResult ClearCache()
        {
            var result = UseCase.ClearCache.Execute();
            var model = new ClearCacheModel(result.Message);
            return View(model);
        }
    }
}
