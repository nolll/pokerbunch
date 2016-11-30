using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Cashout;
using Web.Routes;

namespace Web.Controllers
{
    public class CashgameCashoutController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Cashgame.Cashout)]
        public ActionResult Cashout_Post(string slug, CashoutPostModel postModel)
        {
            var request = new Cashout.Request(Identity.UserName, slug, postModel.PlayerId, postModel.Stack, DateTime.UtcNow);
            var result = UseCase.Cashout.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}