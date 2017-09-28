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
        public ActionResult Details(string cashgameId)
        {
            var cashgameDetailsResult = UseCase.CashgameDetails.Execute(new CashgameDetails.Request(cashgameId));
            var contextResult = GetBunchContext(cashgameDetailsResult.Slug);
            var cashgameDetailsChartResult = UseCase.CashgameDetailsChart.Execute(new CashgameDetailsChart.Request(cashgameId));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult, cashgameDetailsChartResult);
            return View(model);
        }
    }
}