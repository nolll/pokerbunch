using System.Web.Mvc;
using Application.UseCases.BunchContext;
using Application.UseCases.Home;
using Web.Models.HomeModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBunchContextInteractor _contextInteractor;
        private readonly IHomeInteractor _homeInteractor;

        public HomeController(
            IBunchContextInteractor contextInteractor,
            IHomeInteractor homeInteractor)
        {
            _contextInteractor = contextInteractor;
            _homeInteractor = homeInteractor;
        }

        public ActionResult Index()
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest());
            var homeResult = _homeInteractor.Execute();
            var model = new HomePageModel(contextResult, homeResult);
            return View(model);
        }
    }
}