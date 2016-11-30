using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Details;
using Web.Routes;

namespace Web.Controllers
{
    public class BunchDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Bunch.Details)]
        public ActionResult Details(string slug)
        {
            var bunchContextResult = GetBunchContext(slug);
            var bunchDetailsResult = UseCase.BunchDetails.Execute(new BunchDetails.Request(Identity.UserName, slug));

            var model = new BunchDetailsPageModel(bunchContextResult, bunchDetailsResult);
            return View("~/Views/Pages/BunchDetails/BunchDetails.cshtml", model);
        }
    }
}