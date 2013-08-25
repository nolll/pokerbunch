using System.Web.Mvc;
using Web.ModelFactories;
using Web.ModelFactories.HomeModelFactories;
using Web.Models.HomeModels;

namespace Web.Controllers{

	public class HomeController : Controller {
	    private readonly IHomePageModelFactory _homePageModelFactory;

	    public HomeController(IHomePageModelFactory homePageModelFactory)
        {
            _homePageModelFactory = homePageModelFactory;
        }

	    public ActionResult Index()
		{
		    var model = _homePageModelFactory.Create();
		    return View(model);
		}

	}

}