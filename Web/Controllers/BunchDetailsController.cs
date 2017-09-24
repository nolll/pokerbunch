using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Details;

namespace Web.Controllers
{
    public class BunchDetailsController : BaseController
    {
        [Authorize]
        [Route(BunchDetailsUrl.Route)]
        public ActionResult Details(string slug)
        {
            var bunchContextResult = GetBunchContext(slug);
            var bunchDetailsResult = UseCase.BunchDetails.Execute(new BunchDetails.Request(slug));

            var model = new BunchDetailsPageModel(bunchContextResult, bunchDetailsResult);
            return View(model);
        }
    }
}