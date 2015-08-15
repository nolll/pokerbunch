using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Urls;

namespace Web.Controllers
{
    public class DeletePlayerController : BaseController
    {
        [Authorize]
        [Route(Routes.PlayerDelete)]
        public ActionResult Delete(string slug, int playerId)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            var request = new DeletePlayer.Request(slug, playerId);
            var result = UseCase.DeletePlayer.Execute(request);
            var returnUrl = CreateReturnUrl(result);
            return Redirect(returnUrl.Relative);
        }

        private static Url CreateReturnUrl(DeletePlayer.Result result)
        {
            if (result.Deleted)
                return new PlayerIndexUrl(result.Slug);
            return new PlayerDetailsUrl(result.PlayerId);
        }
    }
}