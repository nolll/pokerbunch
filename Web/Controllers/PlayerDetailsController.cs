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
            var detailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request(CurrentUserName, playerId));
            var contextResult = GetBunchContext(detailsResult.Slug);
            var factsResult = UseCase.PlayerFacts.Execute(new PlayerFacts.Request(CurrentUserName, playerId));
            var badgesResult = UseCase.PlayerBadges.Execute(new PlayerBadges.Request(CurrentUserName, playerId));
            var model = new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
            return View("~/Views/Pages/PlayerDetails/Details.cshtml", model);
        }
    }
}