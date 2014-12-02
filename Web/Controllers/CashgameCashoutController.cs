using System.Web.Mvc;
using Core.UseCases.Cashout;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Cashout;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameCashoutController : PokerBunchController
    {
        [HttpPost]
        //[AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/cashout")]
        public ActionResult Cashout_Post(string slug, CashoutPostModel postModel)
        {
            var request = new CashoutRequest(slug, postModel.PlayerId, postModel.Stack);
            UseCase.Cashout(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}