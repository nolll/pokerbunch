using Core.Repositories;
using Core.Services;
using Core.Urls;

namespace Core.UseCases.UserDetails
{
    public class UserDetailsInteractor
    {
        private readonly IAuth _auth;
        private readonly IUserRepository _userRepository;

        public UserDetailsInteractor(IAuth auth, IUserRepository userRepository)
        {
            _auth = auth;
            _userRepository = userRepository;
        }

        public UserDetailsResult Execute(UserDetailsRequest request)
        {
            var displayUser = _userRepository.GetByNameOrEmail(request.UserName);

            var isViewingCurrentUser = displayUser.UserName == _auth.CurrentIdentity.UserName;

            var userName = displayUser.UserName;
            var displayName = displayUser.DisplayName;
            var realName = displayUser.RealName;
            var email = displayUser.Email;
            var avatarUrl = GravatarService.GetAvatarUrl(displayUser.Email);
            var canEdit = _auth.CurrentIdentity.IsAdmin || isViewingCurrentUser;
            var canChangePassword = isViewingCurrentUser;
            var editUrl = new EditUserUrl(displayUser.UserName);
            var changePasswordUrl = new ChangePasswordUrl();

            return new UserDetailsResult(userName, displayName, realName, email, avatarUrl, canEdit, canChangePassword, editUrl, changePasswordUrl);
        }
    }
}