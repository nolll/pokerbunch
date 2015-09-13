using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Urls;

namespace Web.Controllers
{
    public class EndCashgameController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(Routes.CashgameEnd)]
        public ActionResult Post(string slug)
        {
            UseCase.EndCashgame.Execute(new EndCashgame.Request(CurrentUserName, slug));
            return JsonView(new JsonViewModelOk());
        }
    }
}