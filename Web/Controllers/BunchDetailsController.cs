using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.HomegameModels.Details;

namespace Web.Controllers
{
    public class BunchDetailsController : BaseController
    {
        [Authorize]
        [Route(Routes.BunchDetails)]
        public ActionResult Details(string slug)
        {
            var bunchContextResult = GetBunchContext(slug);
            RequirePlayer(bunchContextResult);
            var bunchDetailsResult = UseCase.BunchDetails.Execute(new BunchDetails.Request(slug, CurrentUserName));

            var model = new BunchDetailsPageModel(bunchContextResult, bunchDetailsResult);
            return View("~/Views/Pages/BunchDetails/BunchDetails.cshtml", model);
        }
    }
}