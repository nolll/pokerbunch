using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteAppController : BaseController
    {
        [Authorize]
        [Route(DeleteAppUrl.Route)]
        public ActionResult Delete(string appId)
        {
            var request = new DeleteApp.Request(appId);
            UseCase.DeleteApp.Execute(request);
            var returnUrl = new UserAppsUrl();
            return Redirect(returnUrl.Relative);
        }
    }
}