using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.Buyin;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Buyin;

namespace Web.Controllers
{
    public class CashgameBuyinController : PokerBunchController
    {
        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/buyin")]
        public ActionResult Buyin_Post(string slug, BuyinPostModel postModel)
        {
            if(!IsPlayer(slug, postModel.PlayerId))
                throw new AccessDeniedException();
            var request = new BuyinRequest(slug, postModel.PlayerId, postModel.AddedMoney, postModel.Stack);
            UseCase.Buyin(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}