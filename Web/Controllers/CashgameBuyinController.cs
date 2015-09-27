using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Buyin;

namespace Web.Controllers
{
    public class CashgameBuyinController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(WebRoutes.CashgameBuyin)]
        public ActionResult Buyin_Post(string slug, BuyinPostModel postModel)
        {
            var request = new Buyin.Request(CurrentUserName, slug, postModel.PlayerId, postModel.AddedMoney, postModel.Stack, DateTime.UtcNow);
            UseCase.Buyin.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}