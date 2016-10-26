using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Buyin;
using Web.Routes;

namespace Web.Controllers
{
    public class CashgameBuyinController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Cashgame.Buyin)]
        public ActionResult Buyin_Post(string slug, BuyinPostModel postModel)
        {
            var request = new Buyin.Request(Identity.UserName, slug, postModel.PlayerId, postModel.AddedMoney, postModel.Stack, DateTime.UtcNow);
            UseCase.Buyin.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}