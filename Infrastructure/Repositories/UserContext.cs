using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Infrastructure.System;

namespace Infrastructure.Repositories{

	public class UserContext : IUserContext{

	    private readonly IWebContext _webContext;
	    private readonly IUserRepository _userRepository;
	    private readonly IHomegameRepository _homegameRepository;

	    public UserContext(
            IWebContext webContext,
            IUserRepository userRepository,
            IHomegameRepository homegameRepository)
	    {
	        _webContext = webContext;
	        _userRepository = userRepository;
	        _homegameRepository = homegameRepository;
	    }

		public User GetUser(){
			var token = GetToken();
			if(token != null){
				return _userRepository.GetByToken(token);
			}
		    return null;
		}

		public bool IsLoggedIn(){
			var user = GetUser();
			return user != null;
		}

		public string GetToken(){
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