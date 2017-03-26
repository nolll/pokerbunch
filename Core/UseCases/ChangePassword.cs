using Core.Services;

namespace Core.UseCases
{
    public class ChangePassword
    {
        private readonly IUserService _userService;

        public ChangePassword(IUserService userService)
        {
            _userService = userService;
        }

        public void Execute(Request request)
        {
            _userService.ChangePassword(request.OldPassword, request.NewPassword, request.Repeat);
        }

        public class Request
        {
            public string OldPassword { get; }
            public string NewPassword { get; }
            public string Repeat { get; }

            public Request(string oldPassword, string newPassword, string repeat)
            {
                OldPassword = oldPassword;
                NewPassword = newPassword;
                Repeat = repeat;
            }
        }
    }
}
