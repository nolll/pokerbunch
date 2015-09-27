using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.HomeModels;
using Web.Urls;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        [Route(WebRoutes.Home)]
        public ActionResult Index()
        {
            var contextResult = GetBunchContext();
            var bunchListResult = UseCase.BunchList.Execute(new BunchList.UserBunchesRequest(CurrentUserName));
            var model = new HomePageModel(contextResult, bunchListResult);
            return View("~/Views/Pages/Home/Index.cshtml", model);
        }
    }
}