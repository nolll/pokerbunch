using System.Web.Mvc;
using Application.UseCases.UserDetails;
using Web.Models.UserModels;

namespace Web.Controllers
{
    public class UserDetailsController : ControllerBase
    {
        [Authorize]
        [Route("-/user/details/{userName}")]
        public ActionResult UserDetails(string userName)
        {
            var contextResult = UseCase.AppContext();
            var userDetailsResult = UseCase.UserDetails(new UserDetailsRequest(userName));
            var model = new UserDetailsPageModel(contextResult, userDetailsResult);
            return View("~/Views/Pages/UserDetails/UserDetails.cshtml", model);
        }
    }
}