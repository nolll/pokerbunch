using System;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Buyin;

namespace Web.Controllers
{
    public class CashgameBuyinController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/buyin")]
        public ActionResult Buyin_Post(string slug, BuyinPostModel postModel)
        {
            var bunchContext = GetBunchContext(slug);
            if(!bunchContext.IsCurrentPlayer(postModel.PlayerId))
                throw new AccessDeniedException();
            var request = new Buyin.Request(slug, postModel.PlayerId, postModel.AddedMoney, postModel.Stack, DateTime.UtcNow);
            UseCase.Buyin.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}