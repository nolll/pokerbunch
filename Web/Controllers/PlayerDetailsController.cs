using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Routes;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Details;

namespace Web.Controllers
{
    public class PlayerDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Player.Details)]
        public ActionResult Details(string id)
        {
            var detailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request(id));
            var contextResult = GetBunchContext(detailsResult.Slug);
            var factsResult = UseCase.PlayerFacts.Execute(new PlayerFacts.Request(id));
            var badgesResult = UseCase.PlayerBadges.Execute(new PlayerBadges.Request(id));
            var model = new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
            return View(model);
        }
    }
}