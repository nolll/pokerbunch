using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Web.Controllers.Base;
using Web.Models.HomeModels;

namespace Web.Controllers
{
    public class HomeController : PokerBunchController
    {
        [Route("")]
        public ActionResult Index()
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest());
            var homeResult = UseCase.Home();
            var model = new HomePageModel(contextResult, homeResult);
            return View("Home/Index", model);
        }
    }
}