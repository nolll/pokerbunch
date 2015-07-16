using System.Web.Mvc;
using Core.UseCases.BunchDetails;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Details;

namespace Web.Controllers
{
    public class BunchDetailsController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/homegame/details")]
        public ActionResult Details(string slug)
        {
            var bunchContextResult = GetBunchContext(slug);
            RequirePlayer(bunchContextResult);
            var bunchDetailsResult = UseCase.BunchDetails.Execute(new BunchDetailsRequest(slug, CurrentUserName));

            var model = new BunchDetailsPageModel(bunchContextResult, bunchDetailsResult);
            return View("~/Views/Pages/BunchDetails/BunchDetails.cshtml", model);
        }
    }
}