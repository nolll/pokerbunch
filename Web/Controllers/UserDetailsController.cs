using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.UserModels;
using Web.Routes;

namespace Web.Controllers
{
    public class UserDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.User.Details)]
        public ActionResult UserDetails(string userName)
        {
            var contextResult = GetAppContext();
            var userDetailsResult = UseCase.UserDetails.Execute(new UserDetails.Request(Identity.UserName, userName));
            var model = new UserDetailsPageModel(contextResult, userDetailsResult);
            return View("~/Views/Pages/UserDetails/UserDetails.cshtml", model);
        }
    }
}