using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Cashout;
using Web.Urls;

namespace Web.Controllers
{
    public class CashgameCashoutController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(Routes.CashgameCashout)]
        public ActionResult Cashout_Post(string slug, CashoutPostModel postModel)
        {
            var request = new Cashout.Request(CurrentUserName, slug, postModel.PlayerId, postModel.Stack, DateTime.UtcNow);
            UseCase.Cashout.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}