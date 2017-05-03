using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class EditUser
    {
        private readonly IUserService _userService;

        public EditUser(IUserService userService)
        {
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var user = _userService.GetByNameOrEmail(request.UserName);
            var userToSave = GetUser(user, request);

            _userService.Update(userToSave);

            return new Result(userToSave.UserName);
        }

        private static User GetUser(User user, Request request)
        {
            return new User(
                user.Id,
                user.UserName,
                request.DisplayName,
                request.RealName,
                request.Email,
                user.Role);
        }

        public class Request
        {
            public string UserName { get; }
            public string DisplayName { get; }
            public string RealName { get; }
            public string Email { get; }

            public Request(string userName, string displayName, string realName, string email)
            {
                UserName = userName;
                DisplayName = displayName;
                RealName = realName;
                Email = email;
            }
        }

        public class Result
        {
            public string UserName { get; private set; }

            public Result(string userName)
            {
                UserName = userName;
            }
        }
    }
}
