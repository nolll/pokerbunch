using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.PlayerList;
using Web.Controllers.Base;
using Web.Models.PlayerModels.List;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class PlayerIndexController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/player/index")]
        public ActionResult Index(string slug)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var playerListResult = UseCase.PlayerList(new PlayerListRequest(slug));
            var model = new PlayerListPageModel(contextResult, playerListResult);
            return View("PlayerList/List", model);
        }
    }
}