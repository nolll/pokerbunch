using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.PlayerModels.List;
using Web.Urls;

namespace Web.Controllers
{
    public class PlayerIndexController : BaseController
    {
        [Authorize]
        [Route(Routes.PlayerIndex)]
        public ActionResult Index(string slug)
        {
            var contextResult = GetBunchContext(slug);
            var playerListResult = UseCase.PlayerList.Execute(new PlayerList.Request(slug, CurrentUserName));
            var model = new PlayerListPageModel(contextResult, playerListResult);
            return View("~/Views/Pages/PlayerList/List.cshtml", model);
        }
    }
}