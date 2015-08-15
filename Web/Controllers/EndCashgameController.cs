using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class EndCashgameController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(Routes.CashgameEnd)]
        public ActionResult Post(string slug)
        {
            var bunchContext = GetBunchContext(slug);
            if (!bunchContext.IsPlayer)
                throw new AccessDeniedException();
            UseCase.EndCashgame.Execute(new EndCashgame.Request(slug));
            return JsonView(new JsonViewModelOk());
        }
    }
}