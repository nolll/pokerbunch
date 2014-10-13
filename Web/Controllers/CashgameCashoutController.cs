using System.Web.Mvc;
using Core.Urls;
using Core.UseCases.BunchContext;
using Web.Commands.CashgameCommands;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Cashout;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameCashoutController : PokerBunchController
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public CashgameCashoutController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/cashout/{playerId:int}")]
        public ActionResult Cashout(string slug, int playerId)
        {
            var model = BuildCashoutModel(slug);
            return View("~/Views/Pages/CashgameCashout/Cashout.cshtml", model);
        }

        [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/cashout/{playerId:int}")]
        public ActionResult Cashout_Post(string slug, int playerId, CashoutPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetCashoutCommand(slug, playerId, postModel);
            if (command.Execute())
            {
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildCashoutModel(slug, postModel);
            return View("~/Views/Pages/CashgameCashout/Cashout.cshtml", model);
        }

        private CashoutPageModel BuildCashoutModel(string slug, CashoutPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            return new CashoutPageModel(contextResult, postModel);
        }
    }
}