using System.Web.Mvc;
using Core.UseCases.DeleteCashgame;
using Web.Controllers.Base;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class DeleteCashgameController : PokerBunchController
    {
        [AuthorizeManager]
        [Route("{slug}/cashgame/delete/{dateStr}")]
        public ActionResult Delete(string slug, string dateStr)
        {
            var request = new DeleteCashgameRequest(slug, dateStr);
            var result = UseCase.DeleteCashgame(request);
            return Redirect(result.ReturnUrl.Relative);
		}
    }
}