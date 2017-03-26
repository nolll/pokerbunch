using Core.Entities;
using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class Login
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public Login(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        public Result Execute(Request request)
        {
            var user = GetLoggedInUser(request.LoginName, request.Password);

            if (user == null)
                throw new LoginException();

            var token = _tokenService.Get(request.LoginName, request.Password);
            if(string.IsNullOrEmpty(token))
                throw new LoginException();

            return new Result(user.UserName, token);
        }

        private User GetLoggedInUser(string loginName, string password)
        {
            var user = _userService.GetByNameOrEmail(loginName);
            if (user == null)
                return null;
            var encryptedPassword = EncryptionService.Encrypt(password, user.Salt);
            return encryptedPassword == user.EncryptedPassword ? user : null;
        }

        public class Request 
        {
            public string LoginName { get; }
            public string Password { get; }

            public Request(string loginName, string password)
            {
                LoginName = loginName;
                Password = password;
            }
        }

        public class Result
        {
            public string UserName { get; }
            public string Token { get; }

            public Result(string userName, string token)
            {
                UserName = userName;
                Token = token;
            }
        }
    }
}