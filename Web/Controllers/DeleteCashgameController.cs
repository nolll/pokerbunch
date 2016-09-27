using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCashgameController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.Delete)]
        public ActionResult Delete(int id)
        {
            var request = new DeleteCashgame.Request(Identity.UserName, id);
            var result = UseCase.DeleteCashgame.Execute(request);
            return Redirect(new CashgameIndexUrl(result.Slug).Relative);
		}
    }
}