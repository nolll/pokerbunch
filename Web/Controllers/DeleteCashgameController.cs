using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Urls;

namespace Web.Controllers
{
    public class DeleteCashgameController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameDelete)]
        public ActionResult Delete(int id)
        {
            var request = new DeleteCashgame.Request(CurrentUserName, id);
            var result = UseCase.DeleteCashgame.Execute(request);
            return Redirect(new CashgameIndexUrl(result.Slug).Relative);
		}
    }
}