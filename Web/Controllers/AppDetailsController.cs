using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.AppModels.Details;

namespace Web.Controllers
{
    public class AppDetailsController : BaseController
    {
        [Route(AppDetailsUrl.Route)]
        public ActionResult Details(string appId)
        {
            var context = GetAppContext();
            var appDetailsResult = UseCase.AppDetails.Execute(new AppDetails.Request(appId));
            var model = new AppDetailsPageModel(context, appDetailsResult);
            return View(model);
        }
    }
}