using Application.Exceptions;
using Core.Classes;
using Core.Repositories;

namespace Application.Services
{
	public class Authentication : IAuthentication
    {
	    private readonly IWebContext _webContext;
	    private readonly IUserRepository _userRepository;

	    public Authentication(
            IWebContext webContext,
            IUserRepository userRepository)
	    {
	        _webContext = webContext;
	        _userRepository = userRepository;
	    }

		public User GetUser()
        {
			var token = GetToken();
			return token != null ? _userRepository.GetByToken(token) : null;
		}

		public bool IsLoggedIn()
        {
			var user = GetUser();
			return user != null;
		}

		public string GetToken()
        {
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
	}
}