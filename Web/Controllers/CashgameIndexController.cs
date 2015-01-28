using System.Web.Mvc;
using Core.UseCases.CashgameHome;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class CashgameIndexController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/index")]
        public ActionResult Index(string slug)
        {
            RequirePlayer(slug);
            var indexResult = UseCase.CashgameHome(new CashgameHomeRequest(slug));
            return Redirect(indexResult.StartUrl.Relative);
        }
    }
}