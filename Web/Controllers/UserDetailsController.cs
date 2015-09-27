using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.UserModels;

namespace Web.Controllers
{
    public class UserDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.UserDetails)]
        public ActionResult UserDetails(string userName)
        {
            var contextResult = GetAppContext();
            var userDetailsResult = UseCase.UserDetails.Execute(new UserDetails.Request(CurrentUserName, userName));
            var model = new UserDetailsPageModel(contextResult, userDetailsResult);
            return View("~/Views/Pages/UserDetails/UserDetails.cshtml", model);
        }
    }
}