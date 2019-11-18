using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.ApiDocsModels;

namespace Web.Controllers
{
    public class ApiDocsController : CoreController
    {
        private readonly IUrlFormatter _urlFormatter;

        public ApiDocsController(AppSettings appSettings, CoreContext coreContext, IUrlFormatter urlFormatter)
            : base(appSettings, coreContext)
        {
            _urlFormatter = urlFormatter;
        }

        [Route(ApiDocsIndexUrl.Route)]
        public ActionResult Index()
        {
            var context = GetAppContext();
            var model = new ApiDocsIndexPageModel(AppSettings, context);
            return View(model);
        }

        [Route(ApiDocsAuthUrl.Route)]
        public ActionResult Auth()
        {
            var context = GetAppContext();
            var model = new ApiDocsAuthPageModel(AppSettings, context);
            return View(model);
        }

        [Route(ApiDocsBunchesUrl.Route)]
        public ActionResult Bunches()
        {
            var context = GetAppContext();
            var model = new ApiDocsBunchesPageModel(AppSettings, context);
            return View(model);
        }

        [Route(ApiDocsCashgamesUrl.Route)]
        public ActionResult CashgamesCurrent()
        {
            var context = GetAppContext();
            var model = new ApiDocsCashgamesPageModel(AppSettings, context, _urlFormatter);
            return View(model);
        }

        [Route(ApiDocsPlayersUrl.Route)]
        public ActionResult CashgamesPlayers()
        {
            var context = GetAppContext();
            var model = new ApiDocsPlayersPageModel(AppSettings, context);
            return View(model);
        }
    }
}