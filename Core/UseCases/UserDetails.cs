using Core.Services;

namespace Core.UseCases
{
    public class UserDetails
    {
        private readonly IUserService _userService;

        public UserDetails(IUserService userService)
        {
            _userService = userService;
        }

        public Result Execute(Request request)
        {
            var currentUser = _userService.GetByNameOrEmail(request.CurrentUserName);
            var specified = _userService.GetByNameOrEmail(request.UserName);

            var isViewingCurrentUser = specified.UserName == currentUser.UserName;
            var displayUser = isViewingCurrentUser ? currentUser : specified;

            var userName = displayUser.UserName;
            var displayName = displayUser.DisplayName;
            var realName = displayUser.RealName;
            var email = displayUser.Email;
            var avatarUrl = GravatarService.GetAvatarUrl(displayUser.Email);
            var canEdit = currentUser.IsAdmin || isViewingCurrentUser;
            var canChangePassword = isViewingCurrentUser;

            return new Result(userName, displayName, realName, email, avatarUrl, canEdit, canChangePassword);
        }

        public class Request
        {
            public string CurrentUserName { get; }
            public string UserName { get; }

            public Request(string currentUserName, string userName)
            {
                CurrentUserName = currentUserName;
                UserName = userName;
            }
        }

        public class Result
        {
            public string UserName { get; }
            public string DisplayName { get; }
            public string RealName { get; }
            public string Email { get; }
            public string AvatarUrl { get; }
            public bool CanEdit { get; }
            public bool CanChangePassword { get; }

            public Result(string userName, string displayName, string realName, string email, string avatarUrl, bool canEdit, bool canChangePassword)
            {
                UserName = userName;
                DisplayName = displayName;
                RealName = realName;
                Email = email;
                AvatarUrl = avatarUrl;
                CanEdit = canEdit;
                CanChangePassword = canChangePassword;
            }
        }
    }
}