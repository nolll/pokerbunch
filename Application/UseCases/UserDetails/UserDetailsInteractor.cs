using Application.Services;
using Core.Repositories;

namespace Application.UseCases.UserDetails
{
    public class UserDetailsInteractor : IUserDetailsInteractor
    {
        private readonly IAuth _auth;
        private readonly IUserRepository _userRepository;
        private readonly IAvatarService _avatarService;

        public UserDetailsInteractor(
            IAuth auth,
            IUserRepository userRepository,
            IAvatarService avatarService)
        {
            _auth = auth;
            _userRepository = userRepository;
            _avatarService = avatarService;
        }

        public UserDetailsResult Execute(UserDetailsRequest request)
        {
            var currentUser = _auth.CurrentUser;
            var displayUser = _userRepository.GetByNameOrEmail(request.UserName);
            var avatarUrl = _avatarService.GetLargeAvatarUrl(displayUser.Email);

            return new UserDetailsResult(currentUser, displayUser, avatarUrl);
        }
    }
}