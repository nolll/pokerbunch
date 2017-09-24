using System.Web.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.ApiDocsModels;

namespace Web.Controllers
{
    public class ApiDocsController : BaseController
    {
        [Route(ApiDocsIndexUrl.Route)]
        public ActionResult Index()
        {
            var context = GetAppContext();
            var model = new ApiDocsIndexPageModel(context);
            return View(model);
        }

        [Route(ApiDocsAuthUrl.Route)]
        public ActionResult Auth()
        {
            var context = GetAppContext();
            var model = new ApiDocsAuthPageModel(context);
            return View(model);
        }

        [Route(ApiDocsBunchesUrl.Route)]
        public ActionResult Bunches()
        {
            var context = GetAppContext();
            var model = new ApiDocsBunchesPageModel(context);
            return View(model);
        }

        [Route(ApiDocsCashgamesUrl.Route)]
        public ActionResult Cashgames()
        {
            var context = GetAppContext();
            var model = new ApiDocsCashgamesPageModel(context);
            return View(model);
        }
    }
}