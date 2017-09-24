using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Buyin;

namespace Web.Controllers
{
    public class CashgameBuyinController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Cashgame.Buyin)]
        public ActionResult Buyin_Post(string slug, BuyinPostModel postModel)
        {
            var request = new Buyin.Request(slug, postModel.PlayerId, postModel.AddedMoney, postModel.Stack);
            UseCase.Buyin.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}