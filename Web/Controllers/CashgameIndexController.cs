using System;
using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Index;

namespace Web.Controllers
{
    public class CashgameIndexController : BaseController
    {
        [Authorize]
        [Route(CashgameIndexUrl.Route)]
        public ActionResult Index(string bunchId)
        {
            var contextResult = GetCashgameContext(bunchId, DateTime.UtcNow, CashgameContext.CashgamePage.Overview);
            var statusResult = UseCase.CashgameStatus.Execute(new CashgameStatus.Request(bunchId));
            var currentRankingsResult = UseCase.CurrentRankings.Execute(new CurrentRankings.Request(bunchId));
            var model = new CashgameIndexPageModel(contextResult, statusResult, currentRankingsResult);
            return View(model);
        }
    }
}