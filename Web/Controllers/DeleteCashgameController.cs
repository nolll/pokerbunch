using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Routes;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCashgameController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Delete)]
        public ActionResult Delete(string id)
        {
            var request = new DeleteCashgame.Request(id);
            var result = UseCase.DeleteCashgame.Execute(request);
            return Redirect(new CashgameIndexUrl(result.Slug).Relative);
		}
    }
}