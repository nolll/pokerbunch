using System.Web.Mvc;
using Web.ModelServices;

namespace Web.Controllers{

	public class HomeController : Controller {
	    private readonly IHomeModelService _homeModelService;

	    public HomeController(IHomeModelService homeModelService)
        {
            _homeModelService = homeModelService;
        }

	    public ActionResult Index()
		{
		    var model = _homeModelService.GetIndexModel();
		    return View(model);
		}

	}
}