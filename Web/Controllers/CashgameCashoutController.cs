using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Cashout;

namespace Web.Controllers
{
    public class CashgameCashoutController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(CashgameCashoutUrl.Route)]
        public ActionResult Cashout_Post(string slug, CashoutPostModel postModel)
        {
            var request = new Cashout.Request(slug, postModel.PlayerId, postModel.Stack);
            var result = UseCase.Cashout.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}