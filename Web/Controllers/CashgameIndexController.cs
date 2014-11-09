using System.Web.Mvc;
using Core.UseCases.CashgameHome;
using Web.Controllers.Base;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameIndexController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/index")]
        public ActionResult Index(string slug)
        {
            var indexResult = UseCase.CashgameHome(new CashgameHomeRequest(slug));
            return Redirect(indexResult.StartUrl.Relative);
        }
    }
}