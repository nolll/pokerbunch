using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.Cashout;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Cashout;

namespace Web.Controllers
{
    public class CashgameCashoutController : PokerBunchController
    {
        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/cashout")]
        public ActionResult Cashout_Post(string slug, CashoutPostModel postModel)
        {
            if (!IsPlayer(slug, postModel.PlayerId))
                throw new AccessDeniedException();
            var request = new CashoutRequest(slug, postModel.PlayerId, postModel.Stack);
            UseCase.Cashout(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}