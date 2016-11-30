using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Details;
using Web.Routes;

namespace Web.Controllers
{
    public class PlayerDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Player.Details)]
        public ActionResult Details(string id)
        {
            var detailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request(Identity.UserName, id));
            var contextResult = GetBunchContext(detailsResult.Slug);
            var factsResult = UseCase.PlayerFacts.Execute(new PlayerFacts.Request(Identity.UserName, id));
            var badgesResult = UseCase.PlayerBadges.Execute(new PlayerBadges.Request(Identity.UserName, id));
            var model = new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
            return View("~/Views/Pages/PlayerDetails/Details.cshtml", model);
        }
    }
}