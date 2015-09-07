using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Buyin;
using Web.Urls;

namespace Web.Controllers
{
    public class CashgameBuyinController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(Routes.CashgameBuyin)]
        public ActionResult Buyin_Post(string slug, BuyinPostModel postModel)
        {
            var request = new Buyin.Request(CurrentUserName, slug, postModel.PlayerId, postModel.AddedMoney, postModel.Stack, DateTime.UtcNow);
            var result = UseCase.Buyin.Execute(request);
            Buster.CashgameUpdated(result.CashgameId);
            return JsonView(new JsonViewModelOk());
        }
    }
}