using System.Web.Mvc;
using Core.Exceptions;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Web.ModelFactories.UserModelFactories;
using Web.Models.UserModels.Listing;

namespace Web.Controllers{

	public class UserController : Controller {
	    private readonly IUserContext _userContext;
	    private readonly IUserStorage _userStorage;
	    private readonly IUserDetailsPageModelFactory _userDetailsPageModelFactory;
	    private readonly IUserListingPageModelFactory _userListingPageModelFactory;

	    public UserController(
            IUserContext userContext,
            IUserStorage userStorage,
            IUserDetailsPageModelFactory userDetailsPageModelFactory,
            IUserListingPageModelFactory userListingPageModelFactory)
	    {
	        _userContext = userContext;
	        _userStorage = userStorage;
	        _userDetailsPageModelFactory = userDetailsPageModelFactory;
	        _userListingPageModelFactory = userListingPageModelFactory;
	    }

		public ActionResult Details(string name){
			_userContext.RequireUser();
			var user = _userStorage.GetUserByName(name);
			if(name == null){
				throw new UserNotFoundException();
			}
			var model = _userDetailsPageModelFactory.Create(_userContext.GetUser(), user);
			return View("Details", model);
		}

        public ActionResult Listing(){
			_userContext.RequireAdmin();
			var users = _userStorage.GetUsers();
			var model = _userListingPageModelFactory.Create(_userContext.GetUser(), users);

			return View("Listing/Listing", model);
		}

	}

}