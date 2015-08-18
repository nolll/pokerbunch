using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.HomeModels;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        [Route(Routes.Home)]
        public ActionResult Index()
        {
            var contextResult = GetBunchContext();
            var bunchListResult = UseCase.BunchList.Execute(new BunchList.UserBunchesRequest(CurrentUserName));
            var model = new HomePageModel(contextResult, bunchListResult);
            return View("~/Views/Pages/Home/Index.cshtml", model);
        }
    }
}