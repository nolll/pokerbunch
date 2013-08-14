using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Web.ModelFactories;
using Web.Models;

namespace Web.Controllers{

	public class HomeController : Controller {
	    private readonly IHomeModelFactory _homeModelFactory;

	    public HomeController(IHomeModelFactory homeModelFactory)
        {
            _homeModelFactory = homeModelFactory;
        }

	    public ActionResult Index()
		{
		    var model = _homeModelFactory.Create();
		    return View(model);
		}

	}

}