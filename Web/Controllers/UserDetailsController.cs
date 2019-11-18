using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.UserModels;

namespace Web.Controllers
{
    public class UserDetailsController : CoreController
    {
        private readonly UserDetails _userDetails;

        public UserDetailsController(AppSettings appSettings, CoreContext coreContext, UserDetails userDetails)
            : base(appSettings, coreContext)
        {
            _userDetails = userDetails;
        }

        [Authorize]
        [Route(UserDetailsUrl.Route)]
        public ActionResult UserDetails(string userName)
        {
            var contextResult = GetAppContext();
            var userDetailsResult = _userDetails.Execute(new UserDetails.Request(Identity.UserName, userName));
            var model = new UserDetailsPageModel(AppSettings, contextResult, userDetailsResult);
            return View(model);
        }
    }
}