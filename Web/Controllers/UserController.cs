using System.Web.Mvc;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Infrastructure.Data.Storage.Interfaces;
using Web.Models.UserModels;

namespace Web.Controllers{

	public class UserController : Controller {
	    private readonly IUserContext _userContext;
	    private readonly IUserStorage _userStorage;
	    private readonly IAvatarService _avatarService;

	    public UserController(IUserContext userContext, IUserStorage userStorage, IAvatarService avatarService)
	    {
	        _userContext = userContext;
	        _userStorage = userStorage;
	        _avatarService = avatarService;
	    }

		public ActionResult Details(string userName){
			_userContext.RequireUser();
			var user = _userStorage.GetUserByName(userName);
			if(user == null){
				throw new UserNotFoundException();
			}
			var model = new UserDetailsModel(_userContext.GetUser(), user, _avatarService);
			return View("Details", model);
		}

	}

}