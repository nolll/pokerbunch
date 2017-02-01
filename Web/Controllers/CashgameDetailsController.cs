using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Details;
using Web.Routes;

namespace Web.Controllers
{
    public class CashgameDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Details)]
        public ActionResult Details(string id)
        {
            var cashgameDetailsResult = UseCase.CashgameDetails.Execute(new CashgameDetails.Request(id));
            var contextResult = GetBunchContext(cashgameDetailsResult.Slug);
            var cashgameDetailsChartResult = UseCase.CashgameDetailsChart.Execute(new CashgameDetailsChart.Request(id));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult, cashgameDetailsChartResult);
            return View("~/Views/Pages/CashgameDetails/DetailsPage.cshtml", model);
        }
    }
}