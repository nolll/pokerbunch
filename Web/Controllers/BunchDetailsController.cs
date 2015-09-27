using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Details;
using Web.Urls;

namespace Web.Controllers
{
    public class BunchDetailsController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.BunchDetails)]
        public ActionResult Details(string slug)
        {
            var bunchContextResult = GetBunchContext(slug);
            var bunchDetailsResult = UseCase.BunchDetails.Execute(new BunchDetails.Request(CurrentUserName, slug));

            var model = new BunchDetailsPageModel(bunchContextResult, bunchDetailsResult);
            return View("~/Views/Pages/BunchDetails/BunchDetails.cshtml", model);
        }
    }
}