using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.HomeModels;
using Web.Routes;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        [Route(WebRoutes.Home)]
        public ActionResult Index()
        {
            var contextResult = GetBunchContext();
            var bunchListResult = UseCase.BunchList.Execute(new BunchList.UserBunchesRequest(Identity.UserName));
            var model = new HomePageModel(contextResult, bunchListResult);
            return View(model);
        }
    }
}