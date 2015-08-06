using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.EndCashgame;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class EndCashgameController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/end")]
        public ActionResult Post(string slug)
        {
            var bunchContext = GetBunchContext(slug);
            if (!bunchContext.IsPlayer)
                throw new AccessDeniedException();
            UseCase.EndCashgame.Execute(new EndCashgameRequest(slug));
            return JsonView(new JsonViewModelOk());
        }
    }
}