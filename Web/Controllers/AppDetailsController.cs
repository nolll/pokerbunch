using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.AppModels.Details;

namespace Web.Controllers
{
    public class AppDetailsController : CoreController
    {
        private readonly AppDetails _appDetails;

        public AppDetailsController(AppSettings appSettings, CoreContext coreContext, AppDetails appDetails) 
            : base(appSettings, coreContext)
        {
            _appDetails = appDetails;
        }

        [Route(AppDetailsUrl.Route)]
        public ActionResult Details(string appId)
        {
            var context = GetAppContext();
            var appDetailsResult = _appDetails.Execute(new AppDetails.Request(appId));
            var model = new AppDetailsPageModel(AppSettings, context, appDetailsResult);
            return View(model);
        }
    }
}