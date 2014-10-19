using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.BunchDetails;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Details;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class BunchDetailsController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/homegame/details")]
        public ActionResult Details(string slug)
        {
            var bunchContextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var bunchDetailsResult = UseCase.BunchDetails(new BunchDetailsRequest(slug));

            var model = new BunchDetailsPageModel(bunchContextResult, bunchDetailsResult);
            return View("~/Views/Pages/BunchDetails/BunchDetails.cshtml", model);
        }
    }
}