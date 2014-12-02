using System.Web.Mvc;
using Core.UseCases.Buyin;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Buyin;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameBuyinController : PokerBunchController
    {
        [HttpPost]
        //[AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/buyin")]
        public ActionResult Buyin_Post(string slug, BuyinPostModel postModel)
        {
            var request = new BuyinRequest(slug, postModel.PlayerId, postModel.AddedMoney, postModel.Stack);
            UseCase.Buyin(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}