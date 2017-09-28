using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.PlayerModels.List;

namespace Web.Controllers
{
    public class PlayerListController : BaseController
    {
        [Authorize]
        [Route(PlayerIndexUrl.Route)]
        public ActionResult List(string bunchId)
        {
            var contextResult = GetBunchContext(bunchId);
            var playerListResult = UseCase.PlayerList.Execute(new PlayerList.Request(bunchId));
            var model = new PlayerListPageModel(contextResult, playerListResult);
            return View(model);
        }
    }
}