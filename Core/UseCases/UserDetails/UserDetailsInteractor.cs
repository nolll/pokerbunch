using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.UserDetails
{
    public static class UserDetailsInteractor
    {
        public static UserDetailsResult Execute(IAuth auth, IUserRepository userRepository, UserDetailsRequest request)
        {
            var displayUser = userRepository.GetByNameOrEmail(request.UserName);

            var isViewingCurrentUser = displayUser.UserName == auth.CurrentIdentity.UserName;

            var userName = displayUser.UserName;
            var displayName = displayUser.DisplayName;
            var realName = displayUser.RealName;
            var email = displayUser.Email;
            var avatarUrl = GravatarService.GetAvatarUrl(displayUser.Email);
            var canEdit = auth.CurrentIdentity.IsAdmin || isViewingCurrentUser;
            var canChangePassword = isViewingCurrentUser;
            var editUrl = new EditUserUrl(displayUser.UserName);
            var changePasswordUrl = new ChangePasswordUrl();

            return new UserDetailsResult(userName, displayName, realName, email, avatarUrl, canEdit, canChangePassword, editUrl, changePasswordUrl);
        }
    }
}