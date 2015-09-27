using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Urls;

namespace Web.Controllers
{
    public class DeletePlayerController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.PlayerDelete)]
        public ActionResult Delete(int id)
        {
            var request = new DeletePlayer.Request(CurrentUserName, id);
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