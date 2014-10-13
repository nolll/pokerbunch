using System.Web.Mvc;
using Core.Urls;
using Core.UseCases.BunchContext;
using Web.Commands.CashgameCommands;
using Web.Controllers.Base;
using Web.Models.CashgameModels.End;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EndCashgameController : PokerBunchController
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public EndCashgameController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

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
        public ActionResult End_Post(string slug, EndGamePostModel postModel)
        {
            var command = _cashgameCommandProvider.GetEndGameCommand(slug);
            command.Execute();
            return Redirect(new CashgameIndexUrl(slug).Relative);
        }
    }
}