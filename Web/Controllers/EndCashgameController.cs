using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Routes;

namespace Web.Controllers
{
    public class EndCashgameController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Cashgame.End)]
        public ActionResult Post(string slug)
        {
            UseCase.EndCashgame.Execute(new EndCashgame.Request(slug));
            return JsonView(new JsonViewModelOk());
        }
    }
}