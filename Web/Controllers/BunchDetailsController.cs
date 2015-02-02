using System.Web.Mvc;
using Core.UseCases.BunchContext;
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
            RequirePlayer(slug);
            var bunchContextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var bunchDetailsResult = UseCase.BunchDetails.Execute(new BunchDetailsRequest(slug));

            var model = new BunchDetailsPageModel(bunchContextResult, bunchDetailsResult);
            return View("~/Views/Pages/BunchDetails/BunchDetails.cshtml", model);
        }
    }
}