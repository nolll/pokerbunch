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
            var bunchContextRequest = new BunchContextRequest(slug);
            var bunchContextResult = UseCase.BunchContext(bunchContextRequest);

            var bunchDetailsRequest = new BunchDetailsRequest(slug);
            var bunchDetailsResult = UseCase.BunchDetails(bunchDetailsRequest);

            var model = new BunchDetailsPageModel(bunchContextResult, bunchDetailsResult);
            return View("BunchDetails/HomegameDetails", model);
        }
    }
}