using System.Web.Mvc;
using Web.ModelFactories.HomeModelFactories;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomePageBuilder _homePageBuilder;

        public HomeController(IHomePageBuilder homePageBuilder)
        {
            _homePageBuilder = homePageBuilder;
        }

        public ActionResult Index()
        {
            var model = _homePageBuilder.Build();
            return View(model);
        }
    }
}