using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Buyin;

namespace Web.Controllers
{
    public class CashgameBuyinController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(CashgameBuyinUrl.Route)]
        public ActionResult Buyin_Post(string bunchId, BuyinPostModel postModel)
        {
            var request = new Buyin.Request(bunchId, postModel.PlayerId, postModel.Added, postModel.Stack);
            UseCase.Buyin.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}