using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Routes;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeletePlayerController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Player.Delete)]
        public ActionResult Delete(string id)
        {
            var request = new DeletePlayer.Request(id);
            var result = UseCase.DeletePlayer.Execute(request);
            var returnUrl = CreateReturnUrl(result);
            return Redirect(returnUrl.Relative);
        }

        private static SiteUrl CreateReturnUrl(DeletePlayer.Result result)
        {
            if (result.Deleted)
                return new PlayerIndexUrl(result.Slug);
            return new PlayerDetailsUrl(result.PlayerId);
        }
    }
}