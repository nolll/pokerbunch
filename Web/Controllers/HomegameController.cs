using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Web.Models.HomegameModels.Details;
using Web.Models.HomegameModels.Listing;

namespace Web.Controllers{

	public class HomegameController : Controller
    {
	    private readonly IUserContext _userContext;
	    private readonly IHomegameRepository _homegameRepository;

	    public HomegameController(IUserContext userContext, IHomegameRepository homegameRepository)
	    {
	        _userContext = userContext;
	        _homegameRepository = homegameRepository;
	    }

	    public ActionResult Listing(){
			_userContext.RequireAdmin();
			var homegames = _homegameRepository.GetAll();
			var model = new HomegameListingPageModel(_userContext.GetUser(), homegames);
			return View("HomegameListing", model);
		}

        public ActionResult Details(string gamename){
			var homegame = _homegameRepository.GetByName(gamename);
			_userContext.RequirePlayer(homegame);
			var isInManagerMode = _userContext.IsInRole(homegame, Role.Manager);
			var model = new HomegameDetailsPageModel(_userContext.GetUser(), homegame, isInManagerMode);
			return View("HomegameDetails", model);
		}

    }

}