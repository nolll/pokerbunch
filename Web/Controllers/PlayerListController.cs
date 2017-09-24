using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Routes;
using Web.Controllers.Base;
using Web.Models.PlayerModels.List;

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
            return View(model);
        }
    }
}