using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Details;

namespace Web.Controllers
{
    public class PlayerDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.PlayerDetails)]
        public ActionResult Details(int id)
        {
            var detailsResult = UseCase.PlayerDetails.Execute(new PlayerDetails.Request(CurrentUserName, id));
            var contextResult = GetBunchContext(detailsResult.Slug);
            var factsResult = UseCase.PlayerFacts.Execute(new PlayerFacts.Request(CurrentUserName, id));
            var badgesResult = UseCase.PlayerBadges.Execute(new PlayerBadges.Request(CurrentUserName, id));
            var model = new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
            return View("~/Views/Pages/PlayerDetails/Details.cshtml", model);
        }
    }
}