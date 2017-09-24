using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Details;

namespace Web.Controllers
{
    public class CashgameDetailsController : BaseController
    {
        [Authorize]
        [Route(CashgameDetailsUrl.Route)]
        public ActionResult Details(string id)
        {
            var cashgameDetailsResult = UseCase.CashgameDetails.Execute(new CashgameDetails.Request(id));
            var contextResult = GetBunchContext(cashgameDetailsResult.Slug);
            var cashgameDetailsChartResult = UseCase.CashgameDetailsChart.Execute(new CashgameDetailsChart.Request(id));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult, cashgameDetailsChartResult);
            return View(model);
        }
    }
}