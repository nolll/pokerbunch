using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.BunchContext;
using Core.UseCases.Cashout;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Cashout;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameCashoutController : PokerBunchController
    {
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/cashout/{playerId:int}")]
        public ActionResult Cashout(string slug, int playerId)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/cashout/{playerId:int}")]
        public ActionResult Cashout_Post(string slug, int playerId, CashoutPostModel postModel)
        {
            try
            {
                var request = new CashoutRequest(slug, playerId, postModel.StackAmount);
                var result = UseCase.Cashout(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            
            return ShowForm(slug, postModel);
        }

        private ActionResult ShowForm(string slug, CashoutPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var model = new CashoutPageModel(contextResult, postModel);
            return View("~/Views/Pages/CashgameCashout/Cashout.cshtml", model);
        }
    }
}