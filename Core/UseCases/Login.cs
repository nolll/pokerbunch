using Core.Exceptions;
using Core.Services;

namespace Core.UseCases
{
    public class Login
    {
        private readonly ITokenService _tokenService;

        public Login(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public Result Execute(Request request)
        {
            var token = _tokenService.Get(request.LoginName, request.Password);
            if(string.IsNullOrEmpty(token))
                throw new LoginException();

            return new Result(request.LoginName, token);
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