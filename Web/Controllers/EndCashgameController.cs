using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class EndCashgameController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(WebRoutes.CashgameEnd)]
        public ActionResult Post(string slug)
        {
            UseCase.EndCashgame.Execute(new EndCashgame.Request(CurrentUserName, slug));
            return JsonView(new JsonViewModelOk());
        }
    }
}