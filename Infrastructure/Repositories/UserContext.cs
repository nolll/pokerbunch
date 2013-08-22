using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.System;

namespace Infrastructure.Repositories{

	public class UserContext : IUserContext{

	    private readonly IWebContext _webContext;
	    private readonly IUserStorage _userStorage;
	    private readonly IHomegameStorage _homegameStorage;
	    private readonly IHomegameRepository _homegameRepository;

	    private User _user;
		private bool _fetchedUser;

	    public UserContext(IWebContext webContext, IUserStorage userStorage, IHomegameStorage homegameStorage, IHomegameRepository homegameRepository)
	    {
	        _webContext = webContext;
	        _userStorage = userStorage;
	        _homegameStorage = homegameStorage;
	        _homegameRepository = homegameRepository;
	        _fetchedUser = false;
	    }

		public User GetUser(){
			if(!_fetchedUser){
				var token = GetToken();
				if(token != null){
					_user = _userStorage.GetUserByToken(token);
				}
				_fetchedUser = true;
			}
			return _user;
		}

		public bool IsLoggedIn(){
			var user = GetUser();
			return user != null;
		}

		public string  GetToken(){
			return _webContext.GetCookie("token");
		}

		public Role GetRole(Homegame homegame){
			return _homegameRepository.GetHomegameRole(homegame, GetUser());
		}

		public bool IsInRole(Homegame homegame, Role roleToCheck){
			if(IsAdmin()){
				return true;
			}
			var role = GetRole(homegame);
			return (int)role >= (int)roleToCheck;
		}

		public bool IsGuest(Homegame homegame){
			return IsInRole(homegame, Role.Guest);
		}

		public bool IsPlayer(Homegame homegame){
			return IsInRole(homegame, Role.Player);
		}

		public bool IsManager(Homegame homegame){
			return IsInRole(homegame, Role.Manager);
		}

		public bool IsAdmin(){
			return GetUser().IsAdmin;
		}

		public void RequireUser(){
			if(!IsLoggedIn()){
				throw new NotLoggedInException();
			}
		}

		public void RequireRole(Homegame homegame, Role role){
			RequireUser();
			if(!IsInRole(homegame, role)){
				throw new AccessDeniedException();
			}
		}

		public void RequirePlayer(Homegame homegame){
			RequireUser();
			if(!IsPlayer(homegame)){
				throw new AccessDeniedException();
			}
		}

		public void RequireManager(Homegame homegame){
			RequireUser();
			if(!IsManager(homegame)){
				throw new AccessDeniedException();
			}
		}

		public void RequireAdmin(){
			RequireUser();
			if(!IsAdmin()){
				throw new AccessDeniedException();
			}
		}

	}

}