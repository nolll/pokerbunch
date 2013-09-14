using System.Web.Mvc;
using Core.Exceptions;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Web.ModelFactories.UserModelFactories;

namespace Web.Controllers{

	public class UserController : Controller {
	    private readonly IUserContext _userContext;
	    private readonly IUserStorage _userStorage;
	    private readonly IUserDetailsPageModelFactory _userDetailsPageModelFactory;

	    public UserController(
            IUserContext userContext,
            IUserStorage userStorage,
            IUserDetailsPageModelFactory userDetailsPageModelFactory)
	    {
	        _userContext = userContext;
	        _userStorage = userStorage;
	        _userDetailsPageModelFactory = userDetailsPageModelFactory;
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

	}

}