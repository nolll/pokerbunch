using System.Web.Mvc;
using Core.Urls;
using Core.UseCases.BunchContext;
using Core.UseCases.EndCashgame;
using Web.Controllers.Base;
using Web.Models.CashgameModels.End;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EndCashgameController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/end")]
        public ActionResult End(string slug)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var model = new EndGamePageModel(contextResult);
            return View("~/Views/Pages/EndCashgame/End.cshtml", model);
        }

        [HttpPost]
        [AuthorizePlayer]
        [Route("{slug}/cashgame/end")]
        public ActionResult Post(string slug, EndGamePostModel postModel)
        {
            var endGameResult = UseCase.EndCashgame(new EndCashgameRequest(slug));
            return Redirect(endGameResult.ReturnUrl.Relative);
        }
    }
}