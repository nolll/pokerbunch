using System.Web.Mvc;
using Core.UseCases.DeletePlayer;
using Web.Controllers.Base;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class DeletePlayerController : PokerBunchController
    {
        [AuthorizeManager]
        [Route("{slug}/player/delete/{playerId:int}")]
        public ActionResult Delete(string slug, int playerId)
        {
            var request = new DeletePlayerRequest(slug, playerId);
            var result = UseCase.DeletePlayer(request);
            return Redirect(result.ReturnUrl.Relative);
        }
    }
}