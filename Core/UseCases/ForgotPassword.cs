using Core.Services;

namespace Core.UseCases
{
    public class ForgotPassword
    {
        private readonly IUserService _userService;

        public ForgotPassword(IUserService userService)
        {
            _userService = userService;
        }

        public void Execute(Request request)
        {
            _userService.ResetPassword(request.Email);
        }

        public class Request
        {
            public string Email { get; }

            public Request(string email)
            {
                Email = email;
            }
        }
    }
}