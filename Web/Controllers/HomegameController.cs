using System.Web.Mvc;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Web.Models;
using Web.Models.HomegameModels.Details;

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
			var model = new HomegameListingModel(_userContext.GetUser(), homegames);
			return View(model);
		}

        public ActionResult Details(string game){
			var homegame = _homegameRepository.GetByName(game);
			_userContext.RequirePlayer(homegame);
			var isInManagerMode = _userContext.IsInRole(homegame, Role.Manager);
			var model = new HomegameDetailsModel(_userContext.GetUser(), homegame, isInManagerMode);
			return View(model);
		}

    }

}