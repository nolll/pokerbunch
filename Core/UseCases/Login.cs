using Core.Entities;
using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class Login
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;

        public Login(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        public Result Execute(Request request)
        {
            var user = GetLoggedInUser(request.LoginName, request.Password);

            if (user == null)
                throw new LoginException();

            var token = _authService.GetToken(request.LoginName, request.Password);
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