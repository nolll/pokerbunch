using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Web.Models.HomeModels;
using ControllerBase = Web.Controllers.Base.ControllerBase;

namespace Web.Controllers
{
    public class HomeController : ControllerBase
    {
        [Route("")]
        public ActionResult Index()
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest());
            var homeResult = UseCase.Home();
            var model = new HomePageModel(contextResult, homeResult);
            return View(model);
        }
    }
}