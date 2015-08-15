using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Details;

namespace Web.Controllers
{
    public class PlayerDetailsController : BaseController
    {
        [Authorize]
        [Route(Routes.PlayerDetails)]
        public ActionResult Details(int playerId)
        {
            var contextResult = GetBunchContext(playerId);
            RequirePlayer(contextResult);
            var detailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request(playerId, CurrentUserName));
            var factsResult = UseCase.PlayerFacts.Execute(new PlayerFacts.Request(playerId));
            var badgesResult = UseCase.PlayerBadges.Execute(new PlayerBadges.Request(playerId));
            var model = new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
            return View("~/Views/Pages/PlayerDetails/Details.cshtml", model);
        }
    }
}