using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCashgameController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameDelete)]
        public ActionResult Delete(string slug, string dateStr)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            var request = new DeleteCashgame.Request(slug, dateStr);
            var result = UseCase.DeleteCashgame.Execute(request);
            return Redirect(result.ReturnUrl.Relative);
		}
    }
}