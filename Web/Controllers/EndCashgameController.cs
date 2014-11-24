using System.Web.Mvc;
using Core.UseCases.EndCashgame;
using Web.Controllers.Base;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EndCashgameController : PokerBunchController
    {
        [HttpPost]
        [AuthorizePlayer]
        [Route("{slug}/cashgame/end")]
        public ActionResult Post(string slug)
        {
            UseCase.EndCashgame(new EndCashgameRequest(slug));
            return JsonView(new JsonViewModelOk());
        }
    }
}