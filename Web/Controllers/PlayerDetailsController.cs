using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.PlayerBadges;
using Core.UseCases.PlayerDetails;
using Core.UseCases.PlayerFacts;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Details;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class PlayerDetailsController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/player/details/{playerId:int}")]
        public ActionResult Details(string slug, int playerId)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var detailsResult = UseCase.PlayerDetails(new PlayerDetailsRequest(slug, playerId));
            var factsResult = UseCase.PlayerFacts(new PlayerFactsRequest(slug, playerId));
            var badgesResult = UseCase.PlayerBadges(new PlayerBadgesRequest(slug, playerId));
            var model = new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
            return View("PlayerDetails/Details", model);
        }
    }
}