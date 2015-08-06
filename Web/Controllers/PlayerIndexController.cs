using System.Web.Mvc;
using Core.UseCases.PlayerList;
using Web.Controllers.Base;
using Web.Models.PlayerModels.List;

namespace Web.Controllers
{
    public class PlayerIndexController : BaseController
    {
        [Authorize]
        [Route("{slug}/player/index")]
        public ActionResult Index(string slug)
        {
            var contextResult = GetBunchContext(slug);
            RequirePlayer(contextResult);
            var playerListResult = UseCase.PlayerList.Execute(new PlayerListRequest(slug, CurrentUserName));
            var model = new PlayerListPageModel(contextResult, playerListResult);
            return View("~/Views/Pages/PlayerList/List.cshtml", model);
        }
    }
}