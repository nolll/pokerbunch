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
        public ActionResult Details(int id)
        {
            var contextResult = GetBunchContextByCashgameId(id);
            var cashgameDetailsResult = UseCase.CashgameDetails.Execute(new CashgameDetails.Request(CurrentUserName, id));
            var cashgameDetailsChartResult = UseCase.CashgameDetailsChart.Execute(new CashgameDetailsChart.Request(CurrentUserName, DateTime.UtcNow, id));
            var model = new CashgameDetailsPageModel(contextResult, cashgameDetailsResult, cashgameDetailsChartResult);
            return View("~/Views/Pages/CashgameDetails/DetailsPage.cshtml", model);
        }
    }
}