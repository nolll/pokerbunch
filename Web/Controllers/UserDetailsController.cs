using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.UserModels;

namespace Web.Controllers
{
    public class UserDetailsController : BaseController
    {
        [Authorize]
        [Route(UserDetailsUrl.Route)]
        public ActionResult UserDetails(string userName)
        {
            var contextResult = GetAppContext();
            var userDetailsResult = UseCase.UserDetails.Execute(new UserDetails.Request(Identity.UserName, userName));
            var model = new UserDetailsPageModel(contextResult, userDetailsResult);
            return View(model);
        }
    }
}