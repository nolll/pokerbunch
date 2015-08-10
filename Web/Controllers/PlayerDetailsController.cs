using System.Web.Mvc;
using Core.UseCases;
using Core.UseCases.PlayerBadges;
using Core.UseCases.PlayerFacts;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Details;

namespace Web.Controllers
{
    public class PlayerDetailsController : BaseController
    {
        [Authorize]
        [Route("-/player/details/{playerId:int}")]
        public ActionResult Details(int playerId)
        {
            var contextResult = GetBunchContext(playerId);
            RequirePlayer(contextResult);
            var detailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request(playerId, CurrentUserName));
            var factsResult = UseCase.PlayerFacts.Execute(new PlayerFactsRequest(playerId));
            var badgesResult = UseCase.PlayerBadges.Execute(new PlayerBadgesRequest(playerId));
            var model = new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
            return View("~/Views/Pages/PlayerDetails/Details.cshtml", model);
        }
    }
}