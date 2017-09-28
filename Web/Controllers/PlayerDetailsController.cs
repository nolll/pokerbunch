using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Details;

namespace Web.Controllers
{
    public class PlayerDetailsController : BaseController
    {
        [Authorize]
        [Route(PlayerDetailsUrl.Route)]
        public ActionResult Details(string playerId)
        {
            var detailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request(playerId));
            var contextResult = GetBunchContext(detailsResult.Slug);
            var factsResult = UseCase.PlayerFacts.Execute(new PlayerFacts.Request(playerId));
            var badgesResult = UseCase.PlayerBadges.Execute(new PlayerBadges.Request(playerId));
            var model = new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
            return View(model);
        }
    }
}