using System.Web.Mvc;
using Core.UseCases.DeletePlayer;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeletePlayerController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/player/delete/{playerId:int}")]
        public ActionResult Delete(string slug, int playerId)
        {
            RequireManager(slug);
            var request = new DeletePlayerRequest(slug, playerId);
            var result = UseCase.DeletePlayer.Execute(request);
            return Redirect(result.ReturnUrl.Relative);
        }
    }
}