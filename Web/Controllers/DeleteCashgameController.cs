using System.Web.Mvc;
using Core.UseCases.DeleteCashgame;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCashgameController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/delete/{dateStr}")]
        public ActionResult Delete(string slug, string dateStr)
        {
            RequireManager(slug);
            var request = new DeleteCashgameRequest(slug, dateStr);
            var result = UseCase.DeleteCashgame.Execute(request);
            return Redirect(result.ReturnUrl.Relative);
		}
    }
}