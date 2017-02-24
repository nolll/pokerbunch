using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.PlayerModels.List;
using Web.Routes;

namespace Web.Controllers
{
    public class PlayerListController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Player.List)]
        public ActionResult List(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var playerListResult = UseCase.PlayerList.Execute(new PlayerList.Request(slug));
            var model = new PlayerListPageModel(contextResult, playerListResult);
            return View("~/Views/Pages/PlayerList/List.cshtml", model);
        }
    }
}