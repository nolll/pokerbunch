using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class AddUser
    {
        private readonly IUserService _userService;

        public AddUser(IUserService userService)
        {
            _userService = userService;
        }

        public void Execute(Request request)
        {
            var user = new User("", request.UserName, request.DisplayName, string.Empty, request.Email);
            _userService.Add(user, request.Password);
        }

        public class Request
        {
            public string UserName { get; }
            public string DisplayName { get; }
            public string Email { get; }
            public string Password { get; }

            public Request(string userName, string displayName, string email, string password)
            {
                UserName = userName;
                DisplayName = displayName;
                Email = email;
                Password = password;
            }
        }
    }
}