using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCashgameController : BaseController
    {
        [Authorize]
        [Route(DeleteCashgameUrl.Route)]
        public ActionResult Delete(string cashgameId)
        {
            var request = new DeleteCashgame.Request(cashgameId);
            var result = UseCase.DeleteCashgame.Execute(request);
            return Redirect(new CashgameIndexUrl(result.Slug).Relative);
		}
    }
}