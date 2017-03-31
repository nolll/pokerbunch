using Core.Services;

namespace Core.UseCases
{
    public class Login
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public Login(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var token = _authService.SignIn(request.LoginName, request.Password);
            var user = _userService.Current(token);

            return new Result(user.UserName, token);
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