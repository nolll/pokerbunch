using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class UserDetails
    {
        private readonly IUserRepository _userRepository;

        public UserDetails(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var currentUser = _userRepository.GetByNameOrEmail(request.CurrentUserName);
            var displayUser = _userRepository.GetByNameOrEmail(request.UserName);

            var isViewingCurrentUser = displayUser.UserName == currentUser.UserName;

            var userName = displayUser.UserName;
            var displayName = displayUser.DisplayName;
            var realName = displayUser.RealName;
            var email = displayUser.Email;
            var avatarUrl = GravatarService.GetAvatarUrl(displayUser.Email);
            var canEdit = currentUser.IsAdmin || isViewingCurrentUser;
            var canChangePassword = isViewingCurrentUser;
            var canViewApps = isViewingCurrentUser;

            return new Result(userName, displayName, realName, email, avatarUrl, canEdit, canChangePassword, canViewApps);
        }

        public class Request
        {
            public string CurrentUserName { get; private set; }
            public string UserName { get; private set; }

            public Request(string currentUserName, string userName)
            {
                CurrentUserName = currentUserName;
                UserName = userName;
            }
        }

        public class Result
        {
            public string UserName { get; private set; }
            public string DisplayName { get; private set; }
            public string RealName { get; private set; }
            public string Email { get; private set; }
            public string AvatarUrl { get; private set; }
            public bool CanEdit { get; private set; }
            public bool CanChangePassword { get; private set; }
            public bool CanViewApps { get; private set; }

            public Result(string userName, string displayName, string realName, string email, string avatarUrl, bool canEdit, bool canChangePassword, bool canViewApps)
            {
                UserName = userName;
                DisplayName = displayName;
                RealName = realName;
                Email = email;
                AvatarUrl = avatarUrl;
                CanEdit = canEdit;
                CanChangePassword = canChangePassword;
                CanViewApps = canViewApps;
            }
        }
    }
}