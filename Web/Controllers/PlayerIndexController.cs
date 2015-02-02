using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.PlayerList;
using Web.Controllers.Base;
using Web.Models.PlayerModels.List;

namespace Web.Controllers
{
    public class PlayerIndexController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/player/index")]
        public ActionResult Index(string slug)
        {
            RequirePlayer(slug);
            var contextResult = UseCase.BunchContext.Execute(new BunchContextRequest(slug));
            var playerListResult = UseCase.PlayerList.Execute(new PlayerListRequest(slug));
            var model = new PlayerListPageModel(contextResult, playerListResult);
            return View("~/Views/Pages/PlayerList/List.cshtml", model);
        }
    }
}