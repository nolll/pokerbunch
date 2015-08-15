using System;
using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Details;

namespace Web.Controllers
{
    public class CashgameDetailsController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameDetails)]
        public ActionResult Details(string slug, string dateStr)
        {
            var contextResult = GetBunchContext(slug);
            RequirePlayer(contextResult);
            var cashgameDetailsResult = UseCase.CashgameDetails.Execute(new CashgameDetails.Request(slug, CurrentUserName, dateStr));
            var cashgameDetailsChartResult = UseCase.CashgameDetailsChart.Execute(new CashgameDetailsChart.Request(slug, DateTime.UtcNow, dateStr));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult, cashgameDetailsChartResult);
            return View("~/Views/Pages/CashgameDetails/DetailsPage.cshtml", model);
        }
    }
}