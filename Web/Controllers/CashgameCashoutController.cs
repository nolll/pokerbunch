using System;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Cashout;

namespace Web.Controllers
{
    public class CashgameCashoutController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/cashout")]
        public ActionResult Cashout_Post(string slug, CashoutPostModel postModel)
        {
            var bunchContext = GetBunchContext(slug);
            if (!bunchContext.IsCurrentPlayer(postModel.PlayerId))
                throw new AccessDeniedException();
            var request = new Cashout.Request(slug, postModel.PlayerId, postModel.Stack, DateTime.UtcNow);
            UseCase.Cashout.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}