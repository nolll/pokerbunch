using System.Web.Mvc;
using Core.UseCases.UserDetails;
using Web.Controllers.Base;
using Web.Models.UserModels;

namespace Web.Controllers
{
    public class UserDetailsController : PokerBunchController
    {
        [Authorize]
        [Route("-/user/details/{userName}")]
        public ActionResult UserDetails(string userName)
        {
            var contextResult = UseCase.AppContext.Execute();
            var userDetailsResult = UseCase.UserDetails.Execute(new UserDetailsRequest(userName));
            var model = new UserDetailsPageModel(contextResult, userDetailsResult);
            return View("~/Views/Pages/UserDetails/UserDetails.cshtml", model);
        }
    }
}