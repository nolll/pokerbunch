using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Details;

namespace Web.Controllers
{
    public class CashgameDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Details)]
        public ActionResult Details(int id)
        {
            var cashgameDetailsResult = UseCase.CashgameDetails.Execute(new CashgameDetails.Request(CurrentUserName, id));
            var contextResult = GetBunchContext(cashgameDetailsResult.Slug);
            var cashgameDetailsChartResult = UseCase.CashgameDetailsChart.Execute(new CashgameDetailsChart.Request(CurrentUserName, DateTime.UtcNow, id));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult, cashgameDetailsChartResult);
            return View("~/Views/Pages/CashgameDetails/DetailsPage.cshtml", model);
        }
    }
}