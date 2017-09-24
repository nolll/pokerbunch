using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class EndCashgameController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(EndCashgameUrl.Route)]
        public ActionResult Post(string slug)
        {
            UseCase.EndCashgame.Execute(new EndCashgame.Request(slug));
            return JsonView(new JsonViewModelOk());
        }
    }
}