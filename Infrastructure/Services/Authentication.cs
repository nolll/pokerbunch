using App.Services.Interfaces;
using Core.Classes;
using Core.Exceptions;
using Core.Repositories;
using Infrastructure.System;

namespace Infrastructure.Services
{
	public class Authentication : IAuthentication{

	    private readonly IWebContext _webContext;
	    private readonly IUserRepository _userRepository;

	    public Authentication(
            IWebContext webContext,
            IUserRepository userRepository)
	    {
	        _webContext = webContext;
	        _userRepository = userRepository;
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

        public void RequireUser()
        {
            if (!IsLoggedIn())
            {
                throw new NotLoggedInException();
            }
        }

        public bool IsAdmin()
        {
            return GetUser().IsAdmin;
        }

        public void RequireAdmin()
        {
            if (!IsAdmin())
            {
                throw new AccessDeniedException();
            }
        }

	}

}