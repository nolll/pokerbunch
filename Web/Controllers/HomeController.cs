using System.Web.Mvc;
using Application.UseCases.BunchContext;
using Web.Models.HomeModels;

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